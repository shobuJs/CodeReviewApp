<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="CampusWiseIntake.aspx.cs"
    Inherits="ACADEMIC_CampusWiseIntake" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!-- jQuery library -->
    <style>
        .form-control {
            display: inline-block;
        }
    </style>

    <!--====== table grid view header fixed css added by gaurav 12082021 ======-->
    <style>
        .tableFixHead {
            overflow-y: auto;
            height: 350px;
        }

            .tableFixHead .first-tr th {
                position: sticky;
                top: 0;
                background-color: #255282 !important;
                color: #fff;
            }
    </style>
    <!--=========================-->

    <script>
        function getVal() {
            var id = (document.getElementById("<%=btnSave.ClientID%>"));
            ShowLoader(id);

            var alertmsg = '';

            if (document.getElementById("<%=ddlColg.ClientID%>").value == "0") {
                if (alertmsg == '') {
                    document.getElementById("<%=ddlColg.ClientID%>").focus();
                }
                alertmsg = 'Please Select ' + rfvCollege + ' \n';

            }
            if (document.getElementById("<%=ddlAdmBatch.ClientID%>").value == "0") {
                if (alertmsg == '') {
                    document.getElementById("<%=ddlAdmBatch.ClientID%>").focus();
                }
                alertmsg += 'Please Select ' + rfvAdmbatch + ' \n';

            }

            if (document.getElementById("<%=ddlProgClassification.ClientID%>").value == "0") {
                if (alertmsg == '') {
                    document.getElementById("<%=ddlProgClassification.ClientID%>").focus();
                }
                alertmsg += 'Please Select Study Level';

            }


            var array = []
            var deptNo;
            var checkboxes = document.querySelectorAll('input[type=checkbox]:checked')

            for (var i = 0; i < checkboxes.length; i++) {
                //array.push(checkboxes[i].value)    
                if (deptNo == undefined) {
                    deptNo = checkboxes[i].value + ',';
                }
                else {
                    deptNo += checkboxes[i].value + ',';
                }
            }
            //alert(degreeNo);
            $('#<%= hdndeptno.ClientID %>').val(deptNo);
            //document.getElementById(inpHide).value = "degreeNo";
            if (alertmsg != '') {
                alert(alertmsg);
                HideLoader(id, 'Submit');
                return false;
            }

            return true;


        }
    </script>

    <style>
        div.dd_chk_select {
            height: 35px;
            font-size: 14px !important;
            padding-left: 12px !important;
            line-height: 2.2 !important;
            width: 100%;
        }

            div.dd_chk_select div#caption {
                height: 35px;
            }
    </style>

    <%--<script type="text/javascript">
        document.onreadystatechange = function () {
            var state = document.readyState
            if (state == 'interactive') {
                document.getElementById('contents').style.visibility = "hidden";
            } else if (state == 'complete') {
                setTimeout(function () {
                    document.getElementById('interactive');
                    //document.getElementById('load').style.visibility = "hidden";
                    document.getElementById('contents').style.visibility = "visible";
                }, 1000);
            }
        }
    </script>--%>


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


    <%-- <style>
      div.dd_chk_select { height: 35px;font-size: 14px !important;padding-left: 12px !important;line-height: 2.2 !important; width:100%}
       div.dd_chk_select div#caption {
       height:35px
       }
  </style>--%>

    <%--===== Data Table Script added by gaurav =====--%>
    <script>
        $(document).ready(function () {
            //alert(arrOfHiddenColumns);
            var table = $('#myTable').DataTable({
                responsive: true,
                lengthChange: true,
                scrollY: 320,
                scrollX: true,
                scrollCollapse: true,
                "bPaginate": false,

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
                                            var arr = [];
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
                                                    //nodereturn = $(node).find("label").html() + " : ";
                                                    nodereturn = "Weekday : ";
                                                    nodereturn += $(node).find("input:text").eq(0).val();
                                                    nodereturn += ",";
                                                    nodereturn += "Weekend : ";
                                                    nodereturn += $(node).find("input:text").eq(1).val();
                                                }
                                                else if ($(node).find("span").length > 0) {
                                                    nodereturn = "";
                                                    $(node).find("span").each(function () {
                                                        nodereturn += $(this).html();
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
                                                return $('#myTable').DataTable().column(idx).visible();
                                            }
                                        },
                                        format: {
                                            body: function (data, column, row, node) {
                                                var nodereturn;
                                                if ($(node).find("input:text").length > 0) {
                                                    //nodereturn = $(node).find("label").html() + " : ";
                                                    nodereturn = "Weekday : ";
                                                    nodereturn += $(node).find("input:text").eq(0).val();
                                                    nodereturn += ",";
                                                    nodereturn += "Weekend : ";
                                                    nodereturn += $(node).find("input:text").eq(1).val();
                                                }
                                                else if ($(node).find("span").length > 0) {
                                                    nodereturn = "";
                                                    $(node).find("span").each(function () {
                                                        nodereturn += $(this).html();
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
                                                return $('#myTable').DataTable().column(idx).visible();
                                            }
                                        },
                                        format: {
                                            body: function (data, column, row, node) {
                                                var nodereturn;
                                                if ($(node).find("input:text").length > 0) {
                                                    //nodereturn = $(node).find("label").html() + " : ";
                                                    nodereturn = "Weekday : ";
                                                    nodereturn += $(node).find("input:text").eq(0).val();
                                                    nodereturn += ",";
                                                    nodereturn += "Weekend : ";
                                                    nodereturn += $(node).find("input:text").eq(1).val();
                                                }
                                                else if ($(node).find("span").length > 0) {
                                                    nodereturn = "";
                                                    $(node).find("span").each(function () {
                                                        nodereturn += $(this).html();
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
                //alert(arrOfHiddenColumns);
                var table = $('#myTable').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 320,
                    scrollX: true,
                    scrollCollapse: true,
                    "bPaginate": false,

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
                                                var arr = [];
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
                                                        //nodereturn = $(node).find("label").html() + " : ";
                                                        nodereturn = "Weekday : ";
                                                        nodereturn += $(node).find("input:text").eq(0).val();
                                                        nodereturn += ",";
                                                        nodereturn += "Weekend : ";
                                                        nodereturn += $(node).find("input:text").eq(1).val();
                                                    }
                                                    else if ($(node).find("span").length > 0) {
                                                        nodereturn = "";
                                                        $(node).find("span").each(function () {
                                                            nodereturn += $(this).html();
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
                                                    return $('#myTable').DataTable().column(idx).visible();
                                                }
                                            },
                                            format: {
                                                body: function (data, column, row, node) {
                                                    var nodereturn;
                                                    if ($(node).find("input:text").length > 0) {
                                                        //nodereturn = $(node).find("label").html() + " : ";
                                                        nodereturn = "Weekday : ";
                                                        nodereturn += $(node).find("input:text").eq(0).val();
                                                        nodereturn += ",";
                                                        nodereturn += "Weekend : ";
                                                        nodereturn += $(node).find("input:text").eq(1).val();
                                                    }
                                                    else if ($(node).find("span").length > 0) {
                                                        nodereturn = "";
                                                        $(node).find("span").each(function () {
                                                            nodereturn += $(this).html();
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
                                                    return $('#myTable').DataTable().column(idx).visible();
                                                }
                                            },
                                            format: {
                                                body: function (data, column, row, node) {
                                                    var nodereturn;
                                                    if ($(node).find("input:text").length > 0) {
                                                        //nodereturn = $(node).find("label").html() + " : ";
                                                        nodereturn = "Weekday : ";
                                                        nodereturn += $(node).find("input:text").eq(0).val();
                                                        nodereturn += ",";
                                                        nodereturn += "Weekend : ";
                                                        nodereturn += $(node).find("input:text").eq(1).val();
                                                    }
                                                    else if ($(node).find("span").length > 0) {
                                                        nodereturn = "";
                                                        $(node).find("span").each(function () {
                                                            nodereturn += $(this).html();
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
            //alert(arrOfHiddenColumns);
            var table = $('#myTable2').DataTable({
                responsive: true,
                lengthChange: true,
                scrollY: 320,
                scrollX: true,
                scrollCollapse: true,
                "bPaginate": false,

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
                                return $('#myTable2').DataTable().column(idx).visible();
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
                                                return $('#myTable2').DataTable().column(idx).visible();
                                            }
                                        },
                                        format: {
                                            body: function (data, column, row, node) {
                                                var nodereturn;
                                                if ($(node).find("input:text").length > 0) {
                                                    //nodereturn = $(node).find("label").html() + " : ";
                                                    nodereturn = "Weekday : ";
                                                    nodereturn += $(node).find("input:text").eq(0).val();
                                                    nodereturn += ",";
                                                    nodereturn += "Weekend : ";
                                                    nodereturn += $(node).find("input:text").eq(1).val();
                                                }
                                                else if ($(node).find("span").length > 0) {
                                                    nodereturn = "";
                                                    $(node).find("span").each(function () {
                                                        nodereturn += $(this).html();
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
                                                return $('#myTable2').DataTable().column(idx).visible();
                                            }
                                        },
                                        format: {
                                            body: function (data, column, row, node) {
                                                var nodereturn;
                                                if ($(node).find("input:text").length > 0) {
                                                    //nodereturn = $(node).find("label").html() + " : ";
                                                    nodereturn = "Weekday : ";
                                                    nodereturn += $(node).find("input:text").eq(0).val();
                                                    nodereturn += ",";
                                                    nodereturn += "Weekend : ";
                                                    nodereturn += $(node).find("input:text").eq(1).val();
                                                }
                                                else if ($(node).find("span").length > 0) {
                                                    nodereturn = "";
                                                    $(node).find("span").each(function () {
                                                        nodereturn += $(this).html();
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
                                                return $('#myTable2').DataTable().column(idx).visible();
                                            }
                                        },
                                        format: {
                                            body: function (data, column, row, node) {
                                                var nodereturn;
                                                if ($(node).find("input:text").length > 0) {
                                                    //nodereturn = $(node).find("label").html() + " : ";
                                                    nodereturn = "Weekday : ";
                                                    nodereturn += $(node).find("input:text").eq(0).val();
                                                    nodereturn += ",";
                                                    nodereturn += "Weekend : ";
                                                    nodereturn += $(node).find("input:text").eq(1).val();
                                                }
                                                else if ($(node).find("span").length > 0) {
                                                    nodereturn = "";
                                                    $(node).find("span").each(function () {
                                                        nodereturn += $(this).html();
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
            $('#ctl00_ContentPlaceHolder1_lvOfferCopy_chkCheckAll').on('click', function () {

                // Get all rows with search applied
                var rows = table.rows({ 'search': 'applied' }).nodes();
                // Check/uncheck checkboxes for all rows in the table
                $('input[type="checkbox"]', rows).prop('checked', this.checked);
            });

            // Handle click on checkbox to set state of "Select all" control
            $('#myTable2 tbody').on('change', 'input[type="checkbox"]', function () {
                // If checkbox is not checked
                if (!this.checked) {
                    var el = $('#ctl00_ContentPlaceHolder1_lvOfferCopy_chkCheckAll').get(0);
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
                var table = $('#myTable2').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 320,
                    scrollX: true,
                    scrollCollapse: true,
                    "bPaginate": false,

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
                                    return $('#myTable2').DataTable().column(idx).visible();
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
                                                    return $('#myTable2').DataTable().column(idx).visible();
                                                }
                                            },
                                            format: {
                                                body: function (data, column, row, node) {
                                                    var nodereturn;
                                                    if ($(node).find("input:text").length > 0) {
                                                        //nodereturn = $(node).find("label").html() + " : ";
                                                        nodereturn = "Weekday : ";
                                                        nodereturn += $(node).find("input:text").eq(0).val();
                                                        nodereturn += ",";
                                                        nodereturn += "Weekend : ";
                                                        nodereturn += $(node).find("input:text").eq(1).val();
                                                    }
                                                    else if ($(node).find("span").length > 0) {
                                                        nodereturn = "";
                                                        $(node).find("span").each(function () {
                                                            nodereturn += $(this).html();
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
                                                    return $('#myTable2').DataTable().column(idx).visible();
                                                }
                                            },
                                            format: {
                                                body: function (data, column, row, node) {
                                                    var nodereturn;
                                                    if ($(node).find("input:text").length > 0) {
                                                        //nodereturn = $(node).find("label").html() + " : ";
                                                        nodereturn = "Weekday : ";
                                                        nodereturn += $(node).find("input:text").eq(0).val();
                                                        nodereturn += ",";
                                                        nodereturn += "Weekend : ";
                                                        nodereturn += $(node).find("input:text").eq(1).val();
                                                    }
                                                    else if ($(node).find("span").length > 0) {
                                                        nodereturn = "";
                                                        $(node).find("span").each(function () {
                                                            nodereturn += $(this).html();
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
                                                    return $('#myTable2').DataTable().column(idx).visible();
                                                }
                                            },
                                            format: {
                                                body: function (data, column, row, node) {
                                                    var nodereturn;
                                                    if ($(node).find("input:text").length > 0) {
                                                        //nodereturn = $(node).find("label").html() + " : ";
                                                        nodereturn = "Weekday : ";
                                                        nodereturn += $(node).find("input:text").eq(0).val();
                                                        nodereturn += ",";
                                                        nodereturn += "Weekend : ";
                                                        nodereturn += $(node).find("input:text").eq(1).val();
                                                    }
                                                    else if ($(node).find("span").length > 0) {
                                                        nodereturn = "";
                                                        $(node).find("span").each(function () {
                                                            nodereturn += $(this).html();
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
                $('#ctl00_ContentPlaceHolder1_lvOfferCopy_chkCheckAll').on('click', function () {

                    // Get all rows with search applied
                    var rows = table.rows({ 'search': 'applied' }).nodes();
                    // Check/uncheck checkboxes for all rows in the table
                    $('input[type="checkbox"]', rows).prop('checked', this.checked);
                });

                // Handle click on checkbox to set state of "Select all" control
                $('#myTable2 tbody').on('change', 'input[type="checkbox"]', function () {
                    // If checkbox is not checked
                    if (!this.checked) {
                        var el = $('#ctl00_ContentPlaceHolder1_lvOfferCopy_chkCheckAll').get(0);
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
                    <%--<h3 class="box-title"><strong>ADMABATCH YEAR MONTH MAPPING</strong> </h3>--%>
                    <h3 class="box-title"><asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                </div>
                <div class="box-body">
                    <div class="nav-tabs-custom mt-2 col-12 pb-4" id="myTabContent">
                        <ul class="nav nav-tabs dynamic-nav-tabs" role="tablist">
                            <li class="nav-item active" id="divlkAnnouncement" runat="server">
                                <asp:LinkButton ID="lkProgramOffer" runat="server" OnClick="lkAnnouncement_Click" CssClass="nav-link" TabIndex="1">Program Offer</asp:LinkButton></li>
                            <%--    <li class="nav-item" id="divlkFeed" runat="server"><asp:LinkButton ID="lkFeedback" runat="server" OnClick="lkFeedback_Click" CssClass="nav-link" TabIndex="2">Status</asp:LinkButton></li>--%>
                            <li class="nav-item" id="divlkReports" runat="server">
                                <asp:LinkButton ID="lkProgramCopy" runat="server" OnClick="lkReports_Click" CssClass="nav-link" TabIndex="3">Program Copy </asp:LinkButton></li>
                        </ul>


                        <div class="tab-pane fade show active" id="divAnnounce" role="tabpanel" runat="server" aria-labelledby="ALCourses-tab">

                            <div id="contents">
                                <%--This is testing--%>
                            </div>

                            <div>
                                <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updGradeEntry"
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
                            <asp:UpdatePanel ID="updGradeEntry" runat="server">
                                <ContentTemplate>
                                    <div id="divMsg" runat="server"></div>

                                    <div class="col-12 mt-3">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <%--  <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label> College/School Name</label>
                                            </div>   --%>
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <asp:Label ID="lblDYCollege" runat="server" Font-Bold="true"></asp:Label>
                                                </div>
                                                <asp:DropDownList ID="ddlColg" runat="server" AppendDataBoundItems="True" CssClass="form-control select2 select-click" OnSelectedIndexChanged="ddlColg_SelectedIndexChanged" AutoPostBack="true"
                                                    ToolTip="Please Select Faculty/School">
                                                </asp:DropDownList>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <asp:Label ID="lblDYAdmbatch" runat="server" Font-Bold="true"></asp:Label>
                                                </div>
                                                <asp:DropDownList ID="ddlAdmBatch" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlAdmBatch_SelectedIndexChanged" AutoPostBack="true"
                                                    ToolTip="Please Select Intake">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divUGPG" runat="server">

                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <asp:Label ID="lblDyAdmissionType" runat="server" Font-Bold="true"></asp:Label>
                                                </div>
                                                <asp:DropDownList ID="ddlProgClassification" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlProgClassification_SelectedIndexChanged" AutoPostBack="true"
                                                    ToolTip="Please Select Study Level">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="Div1" style="border: double">
                                                <h6>Note :</h6>
                                                <%--<sup>Note :</sup>--%>
                                                <%--<br />--%>
                                                <sup style="font: bold;">WD = WEEKDAY </sup>
                                                <br />
                                                <sup>WE = WEEKEND </sup>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="dvClassification">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <asp:Label ID="lblClassification" runat="server" Font-Bold="true"></asp:Label>
                                                </div>
                                                <asp:DropDownList ID="ddlClassification" runat="server" TabIndex="7" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlClassification_SelectedIndexChanged" AutoPostBack="true"
                                                    ToolTip="Please Select Discipline" ClientIDMode="Static">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>


                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-12 btn-footer" id="divFooter" runat="server">
                                        <%--<asp:Button ID="btnSave" runat="server" Text="Submit" ToolTip="Submit" ValidationGroup="submit" OnClick="btnSave_Click"
                                                CssClass="btn btn-outline-info" OnClientClick="return getVal();" />--%>
                                        <asp:Button ID="btnShow" runat="server" Text="Show" ToolTip="Show" OnClick="btnShow_Click"
                                            CssClass="btn btn-outline-info" OnClientClick="return validateShow()" />
                                        <asp:LinkButton ID="btnSave" runat="server" ToolTip="Submit" ValidationGroup="submit" OnClientClick="return getVal()"
                                            CssClass="btn btn-outline-info btnX" OnClick="btnSave_Click" Visible="false">Submit</asp:LinkButton>
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                            CssClass="btn btn-outline-danger" OnClick="btnCancel_Click" Visible="false" />
                                        <asp:HiddenField ID="hdndeptno" runat="server" />

                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    </div>

                                    <div class="col-12">

                                        <asp:Panel ID="pnlSession" runat="server" Visible="true">


                                            <asp:ListView ID="lvCampusWiseIntake" runat="server" OnItemDataBound="lvCampusWiseIntake_ItemDataBound1">
                                                <LayoutTemplate>
                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="myTable">
                                                        <thead>
                                                            <tr class="bg-light-blue">
                                                                <th style="width: 15px;">Program</th>

                                                                <%--<th style='"<%#Eval("CAMPUSNO7") == "0" ? "display:none":"display:none"%>"'>--%>
                                                                <th>
                                                                    <asp:Label ID="lblCAMPUS1" runat="server" Text='' ToolTip=''
                                                                        Font-Size="9pt" />
                                                                </th>
                                                                <th>
                                                                    <asp:Label ID="lblCAMPUS2" runat="server" Text='' ToolTip=''
                                                                        Font-Size="9pt" />
                                                                </th>
                                                                <th>
                                                                    <asp:Label ID="lblCAMPUS3" runat="server" Text='' ToolTip=''
                                                                        Font-Size="9pt" />
                                                                </th>
                                                                <th>
                                                                    <asp:Label ID="lblCAMPUS4" runat="server" Text='' ToolTip=''
                                                                        Font-Size="9pt" />
                                                                </th>
                                                                <th>
                                                                    <asp:Label ID="lblCAMPUS5" runat="server" Text='' ToolTip=''
                                                                        Font-Size="9pt" />
                                                                </th>
                                                                <th>
                                                                    <asp:Label ID="lblCAMPUS6" runat="server" Text='' ToolTip=''
                                                                        Font-Size="9pt" />
                                                                </th>
                                                                <th>
                                                                    <asp:Label ID="lblCAMPUS7" runat="server" Text='' ToolTip=''
                                                                        Font-Size="9pt" />
                                                                </th>
                                                                <th>
                                                                    <asp:Label ID="lblCAMPUS8" runat="server" Text='' ToolTip=''
                                                                        Font-Size="9pt" />
                                                                </th>
                                                                <th>
                                                                    <asp:Label ID="lblCAMPUS9" runat="server" Text='' ToolTip=''
                                                                        Font-Size="9pt" />
                                                                </th>
                                                                <th>
                                                                    <asp:Label ID="lblCAMPUS10" runat="server" Text='' ToolTip=''
                                                                        Font-Size="9pt" />
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>



                                                        <td>
                                                            <asp:Label ID="lblDegreeno" runat="server" Text='<%# Bind("DEGREENAME") %>' ToolTip='<%# Bind("DEGREEFULLNAME") %>'
                                                                Font-Size="9pt" /><span> - </span>
                                                            <%-- <asp:Label ID="lblBranchno" runat="server" Text='<%# Bind("BRANCH") %>' ToolTip='<%# Bind("BRANCHNO") %>'--%>
                                                            <asp:Label ID="lblBranchno" runat="server" Text='<%# Bind("BRANCH_SHORT") %>' ToolTip='<%# Bind("BRANCH") %>'
                                                                Font-Size="9pt" />
                                                            <asp:Label ID="lblDegreeHidden" runat="server" Text='<%# Bind("DEGREENO") %>' Font-Size="9pt" Visible="false" />
                                                            <asp:Label ID="lblBranchHidden" runat="server" Text='<%# Bind("BRANCHNO") %>' Font-Size="9pt" Visible="false" />
                                                            <asp:HiddenField ID="hdnDegreeno" runat="server" Value='<%# Bind("DEGREENO") %>' />
                                                            <asp:HiddenField ID="hdnBranchNo" runat="server" Value='<%# Bind("BRANCHNO") %>' />
                                                        </td>
                                                        <td>
                                                            <%--<asp:CheckBox ID="chkWD1" runat="server" Text='<%# Bind("WEEKDAYSNAME1") %>' CssClass="clk-box" ToolTip='<%# Bind("WEEKDAYSNAMENO1") %>' />--%>
                                                            <asp:Label ID="lblWD1" runat="server" Text="WD" Style='font-weight: bold;' CssClass="clk-box" ToolTip='<%# Bind("WEEKDAYSNAMENO1") %>'></asp:Label>

                                                            <asp:TextBox ID="txtCampus1" runat="server" CssClass="form-control clk-text" Width="65px"
                                                                Font-Bold="true" Style="text-align: center" Text='<%# Bind("CAMPUSNOI1") %>' ToolTip='<%# Bind("CAMPUSNO1") %>' MaxLength="4" onblur="return Checkvalue(this);" />
                                                            <br />
                                                            <br />
                                                            <%-- <asp:CheckBox ID="chkWO1" runat="server" Text='<%# Bind("WEEKDAYSNAME2") %>' CssClass="clk-box" ToolTip='<%# Bind("WEEKDAYSNAMENO2") %>' />--%>
                                                            <asp:Label ID="lblWE1" runat="server" Text="WE " Style='margin-left: 2px; font-weight: bold;' CssClass="clk-box" ToolTip='<%# Bind("WEEKDAYSNAMENO2") %>'></asp:Label>
                                                            <asp:TextBox ID="txtCampus1N" runat="server" CssClass="form-control clk-text" Width="65px"
                                                                Font-Bold="true" Style="text-align: center" Text='<%# Bind("CAMPUSNOIN1") %>' ToolTip='<%# Bind("CAMPUSNO1") %>' MaxLength="4" onblur="return Checkvalue(this);" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtCampus1"
                                                                ValidChars="1234567890" FilterMode="ValidChars" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender19" runat="server" TargetControlID="txtCampus1N"
                                                                ValidChars="1234567890" FilterMode="ValidChars" />
                                                        </td>

                                                        <td>
                                                            <%--    <asp:CheckBox ID="chkWD2" runat="server" Text='<%# Bind("WEEKDAYSNAME1") %>' CssClass="clk-box" ToolTip='<%# Bind("WEEKDAYSNAMENO1") %>' />--%>
                                                            <asp:Label ID="lblWD2" runat="server" Text="WD" Style='font-weight: bold;' CssClass="clk-box" ToolTip='<%# Bind("WEEKDAYSNAMENO1") %>'></asp:Label>
                                                            <asp:TextBox ID="txtCampus2" runat="server" CssClass="form-control clk-text" Width="65px"
                                                                Font-Bold="true" Style="text-align: center" Text='<%# Bind("CAMPUSNOI2") %>' ToolTip='<%# Bind("CAMPUSNO2") %>' MaxLength="4" onblur="return Checkvalue(this);" />
                                                            <br />
                                                            <br />
                                                            <%-- <asp:CheckBox ID="chkWO2" runat="server" Text='<%# Bind("WEEKDAYSNAME2") %>' CssClass="clk-box" ToolTip='<%# Bind("WEEKDAYSNAMENO2") %>' />--%>
                                                            <asp:Label ID="lblWE2" runat="server" Text="WE" Style='margin-left: 2px; font-weight: bold;' CssClass="clk-box" ToolTip='<%# Bind("WEEKDAYSNAMENO2") %>'></asp:Label>
                                                            <asp:TextBox ID="txtCampus2N" runat="server" CssClass="form-control clk-text" Width="65px"
                                                                Font-Bold="true" Style="text-align: center" Text='<%# Bind("CAMPUSNOIN2") %>' ToolTip='<%# Bind("CAMPUSNO2") %>' MaxLength="4" onblur="return Checkvalue(this);" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtende21" runat="server" TargetControlID="txtCampus2"
                                                                ValidChars="1234567890" FilterMode="ValidChars" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender18" runat="server" TargetControlID="txtCampus2N"
                                                                ValidChars="1234567890" FilterMode="ValidChars" />

                                                        </td>

                                                        <td>
                                                            <%--<asp:CheckBox ID="chkWD3" runat="server" Text='<%# Bind("WEEKDAYSNAME1") %>' CssClass="clk-box" ToolTip='<%# Bind("WEEKDAYSNAMENO1") %>' />--%>
                                                            <asp:Label ID="lblWD3" runat="server" Text="WD" Style='font-weight: bold;' CssClass="clk-box" ToolTip='<%# Bind("WEEKDAYSNAMENO1") %>'></asp:Label>
                                                            <asp:TextBox ID="txtCampus3" runat="server" CssClass="form-control clk-text" Width="65px"
                                                                Font-Bold="true" Style="text-align: center" Text='<%# Bind("CAMPUSNOI3") %>' ToolTip='<%# Bind("CAMPUSNO3") %>' MaxLength="4" onblur="return Checkvalue(this);" />
                                                            <br />
                                                            <br />
                                                            <%--<asp:CheckBox ID="chkWO3" runat="server" Text='<%# Bind("WEEKDAYSNAME2") %>' CssClass="clk-box" ToolTip='<%# Bind("WEEKDAYSNAMENO2") %>' />--%>
                                                            <asp:Label ID="lblWE3" runat="server" Text="WE" Style='margin-left: 2px; font-weight: bold;' CssClass="clk-box" ToolTip='<%# Bind("WEEKDAYSNAMENO2") %>'></asp:Label>
                                                            <asp:TextBox ID="txtCampus3N" runat="server" CssClass="form-control clk-text" Width="65px"
                                                                Font-Bold="true" Style="text-align: center" Text='<%# Bind("CAMPUSNOIN3") %>' ToolTip='<%# Bind("CAMPUSNO3") %>' MaxLength="4" onblur="return Checkvalue(this);" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtCampus3"
                                                                ValidChars="1234567890" FilterMode="ValidChars" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" runat="server" TargetControlID="txtCampus3N"
                                                                ValidChars="1234567890" FilterMode="ValidChars" />
                                                        </td>

                                                        <td>
                                                            <%-- <asp:CheckBox ID="chkWD4" runat="server" Text='<%# Bind("WEEKDAYSNAME1") %>' CssClass="clk-box" ToolTip='<%# Bind("WEEKDAYSNAMENO1") %>' />--%>
                                                            <asp:Label ID="lblWD4" runat="server" Text="WD" Style='font-weight: bold;' CssClass="clk-box" ToolTip='<%# Bind("WEEKDAYSNAMENO1") %>'></asp:Label>
                                                            <asp:TextBox ID="txtCampus4" runat="server" CssClass="form-control clk-text" Width="65px"
                                                                Font-Bold="true" Style="text-align: center" Text='<%# Bind("CAMPUSNOI4") %>' ToolTip='<%# Bind("CAMPUSNO4") %>' MaxLength="4" onblur="return Checkvalue(this);" />
                                                            <br />
                                                            <br />
                                                            <%-- <asp:CheckBox ID="chkWO4" runat="server" Text='<%# Bind("WEEKDAYSNAME2") %>' CssClass="clk-box" ToolTip='<%# Bind("WEEKDAYSNAMENO2") %>' />--%>
                                                            <asp:Label ID="lblWE4" runat="server" Text="WE" Style='margin-left: 2px; font-weight: bold;' CssClass="clk-box" ToolTip='<%# Bind("WEEKDAYSNAMENO2") %>'></asp:Label>
                                                            <asp:TextBox ID="txtCampus4N" runat="server" CssClass="form-control clk-text" Width="65px"
                                                                Font-Bold="true" Style="text-align: center" Text='<%# Bind("CAMPUSNOIN4") %>' ToolTip='<%# Bind("CAMPUSNO4") %>' MaxLength="4" onblur="return Checkvalue(this);" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtCampus4"
                                                                ValidChars="1234567890" FilterMode="ValidChars" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" runat="server" TargetControlID="txtCampus4N"
                                                                ValidChars="1234567890" FilterMode="ValidChars" />
                                                        </td>

                                                        <td>
                                                            <%--<asp:CheckBox ID="chkWD5" runat="server" Text='<%# Bind("WEEKDAYSNAME1") %>' CssClass="clk-box" ToolTip='<%# Bind("WEEKDAYSNAMENO1") %>' />--%>
                                                            <asp:Label ID="lblWD5" runat="server" Text="WD" Style='font-weight: bold;' CssClass="clk-box" ToolTip='<%# Bind("WEEKDAYSNAMENO1") %>'></asp:Label>
                                                            <asp:TextBox ID="txtCampus5" runat="server" CssClass="form-control clk-text" Width="65px"
                                                                Font-Bold="true" Style="text-align: center" Text='<%# Bind("CAMPUSNOI5") %>' ToolTip='<%# Bind("CAMPUSNO5") %>' MaxLength="4" onblur="return Checkvalue(this);" />
                                                            <br />
                                                            <br />
                                                            <%--<asp:CheckBox ID="chkWO5" runat="server" Text='<%# Bind("WEEKDAYSNAME2") %>' CssClass="clk-box" ToolTip='<%# Bind("WEEKDAYSNAMENO2") %>' />--%>
                                                            <asp:Label ID="lblWE5" runat="server" Text="WE" Style='margin-left: 2px; font-weight: bold;' CssClass="clk-box" ToolTip='<%# Bind("WEEKDAYSNAMENO2") %>'></asp:Label>
                                                            <asp:TextBox ID="txtCampus5N" runat="server" CssClass="form-control clk-text" Width="65px"
                                                                Font-Bold="true" Style="text-align: center" Text='<%# Bind("CAMPUSNOIN5") %>' ToolTip='<%# Bind("CAMPUSNO5") %>' MaxLength="4" onblur="return Checkvalue(this);" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtCampus5"
                                                                ValidChars="1234567890" FilterMode="ValidChars" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server" TargetControlID="txtCampus5N"
                                                                ValidChars="1234567890" FilterMode="ValidChars" />
                                                        </td>

                                                        <td>
                                                            <%--<asp:CheckBox ID="chkWD6" runat="server" Text='<%# Bind("WEEKDAYSNAME1") %>' CssClass="clk-box" ToolTip='<%# Bind("WEEKDAYSNAMENO1") %>' />--%>
                                                            <asp:Label ID="lblWD6" runat="server" Text="WD" Style='font-weight: bold;' CssClass="clk-box" ToolTip='<%# Bind("WEEKDAYSNAMENO1") %>'></asp:Label>
                                                            <asp:TextBox ID="txtCampus6" runat="server" CssClass="form-control clk-text" Width="65px"
                                                                Font-Bold="true" Style="text-align: center" Text='<%# Bind("CAMPUSNOI6") %>' ToolTip='<%# Bind("CAMPUSNO6") %>' MaxLength="4" onblur="return Checkvalue(this);" />
                                                            <br />
                                                            <br />
                                                            <%--<asp:CheckBox ID="chkWO6" runat="server" Text='<%# Bind("WEEKDAYSNAME2") %>' CssClass="clk-box" ToolTip='<%# Bind("WEEKDAYSNAMENO2") %>' />--%>
                                                            <asp:Label ID="lblWE6" runat="server" Text="WE" Style='margin-left: 2px; font-weight: bold;' CssClass="clk-box" ToolTip='<%# Bind("WEEKDAYSNAMENO2") %>'></asp:Label>
                                                            <asp:TextBox ID="txtCampus6N" runat="server" CssClass="form-control clk-text" Width="65px"
                                                                Font-Bold="true" Style="text-align: center" Text='<%# Bind("CAMPUSNOIN6") %>' ToolTip='<%# Bind("CAMPUSNO6") %>' MaxLength="4" onblur="return Checkvalue(this);" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtCampus6"
                                                                ValidChars="1234567890" FilterMode="ValidChars" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server" TargetControlID="txtCampus6N"
                                                                ValidChars="1234567890" FilterMode="ValidChars" />
                                                        </td>

                                                        <td>
                                                            <%-- <asp:CheckBox ID="chkWD7" runat="server" Text='<%# Bind("WEEKDAYSNAME1") %>' CssClass="clk-box" ToolTip='<%# Bind("WEEKDAYSNAMENO1") %>' />--%>
                                                            <asp:Label ID="lblWD7" runat="server" Text="WD" Style='font-weight: bold;' CssClass="clk-box" ToolTip='<%# Bind("WEEKDAYSNAMENO1") %>'></asp:Label>
                                                            <asp:TextBox ID="txtCampus7" runat="server" CssClass="form-control clk-text" Width="65px"
                                                                Font-Bold="true" Style="text-align: center" Text='<%# Bind("CAMPUSNOI7") %>' ToolTip='<%# Bind("CAMPUSNO7") %>' MaxLength="4" onblur="return Checkvalue(this);" />
                                                            <br />
                                                            <br />
                                                            <%--<asp:CheckBox ID="chkWO7" runat="server" Text='<%# Bind("WEEKDAYSNAME2") %>' CssClass="clk-box" ToolTip='<%# Bind("WEEKDAYSNAMENO2") %>' />--%>
                                                            <asp:Label ID="lblWE7" runat="server" Text="WE" Style='margin-left: 2px; font-weight: bold;' CssClass="clk-box" ToolTip='<%# Bind("WEEKDAYSNAMENO2") %>'></asp:Label>
                                                            <asp:TextBox ID="txtCampus7N" runat="server" CssClass="form-control clk-text" Width="65px"
                                                                Font-Bold="true" Style="text-align: center" Text='<%# Bind("CAMPUSNOIN7") %>' ToolTip='<%# Bind("CAMPUSNO7") %>' MaxLength="4" onblur="return Checkvalue(this);" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txtCampus7"
                                                                ValidChars="1234567890" FilterMode="ValidChars" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" TargetControlID="txtCampus7N"
                                                                ValidChars="1234567890" FilterMode="ValidChars" />
                                                        </td>

                                                        <td>
                                                            <%--<asp:CheckBox ID="chkWD8" runat="server" Text='<%# Bind("WEEKDAYSNAME1") %>' CssClass="clk-box" ToolTip='<%# Bind("WEEKDAYSNAMENO1") %>' />--%>
                                                            <asp:Label ID="lblWD8" runat="server" Text="WD" Style='font-weight: bold;' CssClass="clk-box" ToolTip='<%# Bind("WEEKDAYSNAMENO1") %>'></asp:Label>
                                                            <asp:TextBox ID="txtCampus8" runat="server" CssClass="form-control clk-text" Width="65px"
                                                                Font-Bold="true" Style="text-align: center" Text='<%# Bind("CAMPUSNOI8") %>' ToolTip='<%# Bind("CAMPUSNO8") %>' MaxLength="4" onblur="return Checkvalue(this);" />
                                                            <br />
                                                            <br />
                                                            <%-- <asp:CheckBox ID="chkWO8" runat="server" Text='<%# Bind("WEEKDAYSNAME2") %>' CssClass="clk-box" ToolTip='<%# Bind("WEEKDAYSNAMENO2") %>' />--%>
                                                            <asp:Label ID="lblWE8" runat="server" Text="WE" Style='margin-left: 2px; font-weight: bold;' CssClass="clk-box" ToolTip='<%# Bind("WEEKDAYSNAMENO2") %>'></asp:Label>
                                                            <asp:TextBox ID="txtCampus8N" runat="server" CssClass="form-control clk-text" Width="65px"
                                                                Font-Bold="true" Style="text-align: center" Text='<%# Bind("CAMPUSNOIN8") %>' ToolTip='<%# Bind("CAMPUSNO8") %>' MaxLength="4" onblur="return Checkvalue(this);" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="txtCampus8"
                                                                ValidChars="1234567890" FilterMode="ValidChars" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" TargetControlID="txtCampus8N"
                                                                ValidChars="1234567890" FilterMode="ValidChars" />
                                                        </td>

                                                        <td>
                                                            <%-- <asp:CheckBox ID="chkWD9" runat="server" Text='<%# Bind("WEEKDAYSNAME1") %>' CssClass="clk-box" ToolTip='<%# Bind("WEEKDAYSNAMENO1") %>' />--%>
                                                            <asp:Label ID="lblWD9" runat="server" Text="WD" Style='font-weight: bold;' CssClass="clk-box" ToolTip='<%# Bind("WEEKDAYSNAMENO1") %>'></asp:Label>
                                                            <asp:TextBox ID="txtCampus9" runat="server" CssClass="form-control clk-text" Width="65px"
                                                                Font-Bold="true" Style="text-align: center" Text='<%# Bind("CAMPUSNOI9") %>' ToolTip='<%# Bind("CAMPUSNO9") %>' MaxLength="4" onblur="return Checkvalue(this);" />
                                                            <br />
                                                            <br />
                                                            <%--<asp:CheckBox ID="chkWO9" runat="server" Text='<%# Bind("WEEKDAYSNAME2") %>' CssClass="clk-box" ToolTip='<%# Bind("WEEKDAYSNAMENO2") %>' />--%>
                                                            <asp:Label ID="lblWE9" runat="server" Text="WE" Style='margin-left: 2px; font-weight: bold;' CssClass="clk-box" ToolTip='<%# Bind("WEEKDAYSNAMENO2") %>'></asp:Label>
                                                            <asp:TextBox ID="txtCampus9N" runat="server" CssClass="form-control clk-text" Width="65px"
                                                                Font-Bold="true" Style="text-align: center" Text='<%# Bind("CAMPUSNOIN9") %>' ToolTip='<%# Bind("CAMPUSNO9") %>' MaxLength="4" onblur="return Checkvalue(this);" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" TargetControlID="txtCampus9"
                                                                ValidChars="1234567890" FilterMode="ValidChars" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" TargetControlID="txtCampus9N"
                                                                ValidChars="1234567890" FilterMode="ValidChars" />
                                                        </td>

                                                        <td>
                                                            <%-- <asp:CheckBox ID="chkWD10" runat="server" Text='<%# Bind("WEEKDAYSNAME1") %>' CssClass="clk-box" ToolTip='<%# Bind("WEEKDAYSNAMENO1") %>' />--%>
                                                            <asp:Label ID="lblWD10" runat="server" Text="WD" Style='font-weight: bold;' CssClass="clk-box" ToolTip='<%# Bind("WEEKDAYSNAMENO1") %>'></asp:Label>
                                                            <asp:TextBox ID="txtCampus10" runat="server" CssClass="form-control clk-text" Width="65px"
                                                                Font-Bold="true" Style="text-align: center" Text='<%# Bind("CAMPUSNOI10") %>' ToolTip='<%# Bind("CAMPUSNO10") %>' MaxLength="4" onblur="return Checkvalue(this);" />
                                                            <br />
                                                            <br />
                                                            <%-- <asp:CheckBox ID="chkWO10" runat="server" Text='<%# Bind("WEEKDAYSNAME2") %>' CssClass="clk-box" ToolTip='<%# Bind("WEEKDAYSNAMENO2") %>' />--%>
                                                            <asp:Label ID="lblWE10" runat="server" Text="WE" Style='margin-left: 2px; font-weight: bold;' CssClass="clk-box" ToolTip='<%# Bind("WEEKDAYSNAMENO2") %>'></asp:Label>
                                                            <asp:TextBox ID="txtCampus10N" runat="server" CssClass="form-control clk-text" Width="65px"
                                                                Font-Bold="true" Style="text-align: center" Text='<%# Bind("CAMPUSNOIN10") %>' ToolTip='<%# Bind("CAMPUSNO10") %>' MaxLength="4" onblur="return Checkvalue(this);" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" TargetControlID="txtCampus10"
                                                                ValidChars="1234567890" FilterMode="ValidChars" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtCampus10N"
                                                                ValidChars="1234567890" FilterMode="ValidChars" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>

                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnShow" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnCancel" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlColg" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlAdmBatch" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlProgClassification" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlClassification" />
                                    <%-- <asp:PostBackTrigger ControlID="gvCampus" />--%>
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>

                        <div id="divReports" runat="server" visible="false" role="tabpanel" aria-labelledby="Grade-tab">
                            <div>
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updProgramCopy"
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

                            <div id="dvProgramOfferCopy">
                                <%--This is testing--%>
                            </div>
                            <asp:UpdatePanel ID="updProgramCopy" runat="server">
                                <ContentTemplate>
                                    <div id="div3" runat="server"></div>

                                    <div class="col-12 mt-3">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">

                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label id="lblfaculty" runat="server">Faculty/School Name</label>

                                                </div>
                                                <asp:DropDownList ID="ddlCollegeSchNameC" runat="server" AppendDataBoundItems="True" CssClass="form-control select2 select-click" OnSelectedIndexChanged="ddlColg_SelectedIndexChanged" AutoPostBack="true"
                                                    ToolTip="Please Select Institute">
                                                </asp:DropDownList>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Intake</label>

                                                </div>
                                                <asp:DropDownList ID="ddlIntakeC" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlAdmBatch_SelectedIndexChanged" AutoPostBack="true"
                                                    ToolTip="Please Select Intake">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12" id="div4" runat="server" visible="false">

                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Study Level</label>

                                                </div>
                                                <asp:DropDownList ID="ddlStudyLevelC" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlProgClassification_SelectedIndexChanged" AutoPostBack="true"
                                                    ToolTip="Please Select Program Classification">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>



                                            <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="Div6" visible="false">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Classification</label>
                                                    <%--  <asp:Label ID="Label5" runat="server" Font-Bold="true"></asp:Label>--%>
                                                </div>
                                                <asp:DropDownList ID="ddlClassificationC" runat="server" TabIndex="7" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlClassification_SelectedIndexChanged" AutoPostBack="true"
                                                    ToolTip="Please Select Classificaion" ClientIDMode="Static">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>To Intake</label>

                                                </div>
                                                <asp:DropDownList ID="ddlToIntake" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                                    ToolTip="Please Select Batch/Month/Year">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                            </div>


                                            <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="Div5" style="border: double">
                                                <h6>Note :</h6>

                                                <sup style="font: bold;">WD = WEEKDAY </sup>
                                                <br />
                                                <sup>WE = WEEKEND </sup>
                                            </div>



                                        </div>
                                    </div>

                                    <div class="col-12 btn-footer" id="div7" runat="server">
                                        <%--<asp:Button ID="btnSave" runat="server" Text="Submit" ToolTip="Submit" ValidationGroup="submit" OnClick="btnSave_Click"
                                                CssClass="btn btn-outline-info" OnClientClick="return getVal();" />--%>
                                        <asp:Button ID="btnShowCopy" runat="server" Text="Show" ToolTip="Show" OnClick="btnShowCopy_Click"
                                            CssClass="btn btn-outline-info" OnClientClick="return validateShowCopy()" />
                                        <asp:LinkButton ID="lnkSubmit" runat="server" ToolTip="Submit" ValidationGroup="submit" OnClientClick="return getVal()"
                                            CssClass="btn btn-outline-info btnX" OnClick="btnSaveCopy_Click" Visible="false">Submit</asp:LinkButton>
                                        <asp:Button ID="btnCancelCopy" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                            CssClass="btn btn-outline-danger" OnClick="btnCancelCopy_Click" Visible="false" />
                                        <asp:HiddenField ID="HiddenField1" runat="server" />

                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="submit"
                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    </div>

                                    <div class="col-12">

                                        <asp:Panel ID="Panel1" runat="server" Visible="true">
                                            <asp:ListView ID="lvOfferCopy" runat="server" OnItemDataBound="lvOfferCopy_ItemDataBound1">
                                                <LayoutTemplate>
                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="myTable2">
                                                        <thead>
                                                            <tr class="bg-light-blue">
                                                                <%-- <th>Select </th>--%>
                                                                <th class="re-move">
                                                                    <asp:CheckBox ID="chkCheckAll" runat="server" ToolTip="Select or Deselect All Records" />
                                                                </th>
                                                                <th style="width: 15px;">Program</th>


                                                                <th>
                                                                    <asp:Label ID="lblCAMPUS1" runat="server" Text='' ToolTip=''
                                                                        Font-Size="9pt" />
                                                                </th>
                                                                <th>
                                                                    <asp:Label ID="lblCAMPUS2" runat="server" Text='' ToolTip=''
                                                                        Font-Size="9pt" />
                                                                </th>
                                                                <th>
                                                                    <asp:Label ID="lblCAMPUS3" runat="server" Text='' ToolTip=''
                                                                        Font-Size="9pt" />
                                                                </th>
                                                                <th>
                                                                    <asp:Label ID="lblCAMPUS4" runat="server" Text='' ToolTip=''
                                                                        Font-Size="9pt" />
                                                                </th>
                                                                <th>
                                                                    <asp:Label ID="lblCAMPUS5" runat="server" Text='' ToolTip=''
                                                                        Font-Size="9pt" />
                                                                </th>
                                                                <th>
                                                                    <asp:Label ID="lblCAMPUS6" runat="server" Text='' ToolTip=''
                                                                        Font-Size="9pt" />
                                                                </th>
                                                                <th>
                                                                    <asp:Label ID="lblCAMPUS7" runat="server" Text='' ToolTip=''
                                                                        Font-Size="9pt" />
                                                                </th>
                                                                <th>
                                                                    <asp:Label ID="lblCAMPUS8" runat="server" Text='' ToolTip=''
                                                                        Font-Size="9pt" />
                                                                </th>
                                                                <th>
                                                                    <asp:Label ID="lblCAMPUS9" runat="server" Text='' ToolTip=''
                                                                        Font-Size="9pt" />
                                                                </th>
                                                                <th>
                                                                    <asp:Label ID="lblCAMPUS10" runat="server" Text='' ToolTip=''
                                                                        Font-Size="9pt" />
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="chkSelect" runat="server" CssClass="clk-box" /></td>
                                                        <td>
                                                            <asp:Label ID="lblDegreeno" runat="server" Text='<%# Bind("DEGREENAME") %>' ToolTip='<%# Bind("DEGREEFULLNAME") %>'
                                                                Font-Size="9pt" /><span> - </span>
                                                            <%-- <asp:Label ID="lblBranchno" runat="server" Text='<%# Bind("BRANCH") %>' ToolTip='<%# Bind("BRANCHNO") %>'--%>
                                                            <asp:Label ID="lblBranchno" runat="server" Text='<%# Bind("BRANCH_SHORT") %>' ToolTip='<%# Bind("BRANCH") %>'
                                                                Font-Size="9pt" />
                                                            <asp:HiddenField ID="hdnDegreeno" runat="server" Value='<%# Bind("DEGREENO") %>' />
                                                            <asp:HiddenField ID="hdnBranchNo" runat="server" Value='<%# Bind("BRANCHNO") %>' />
                                                        </td>
                                                        <td>

                                                            <asp:Label ID="lblWD1" runat="server" Text="WD" Style='font-weight: bold;' CssClass="clk-box" ToolTip='<%# Bind("WEEKDAYSNAMENO1") %>'></asp:Label>

                                                            <asp:TextBox ID="txtCampus1" runat="server" CssClass="form-control clk-text" Width="65px" Enabled="false"
                                                                Font-Bold="true" Style="text-align: center" Text='<%# Bind("CAMPUSNOI1") %>' ToolTip='<%# Bind("CAMPUSNO1") %>' MaxLength="3" onblur="return Checkvalue(this);" />
                                                            <br />
                                                            <br />

                                                            <asp:Label ID="lblWE1" runat="server" Text="WE " Style='margin-left: 2px; font-weight: bold;' CssClass="clk-box" ToolTip='<%# Bind("WEEKDAYSNAMENO2") %>'></asp:Label>
                                                            <asp:TextBox ID="txtCampus1N" runat="server" CssClass="form-control clk-text" Width="65px" Enabled="false"
                                                                Font-Bold="true" Style="text-align: center" Text='<%# Bind("CAMPUSNOIN1") %>' ToolTip='<%# Bind("CAMPUSNO1") %>' MaxLength="3" onblur="return Checkvalue(this);" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtCampus1"
                                                                ValidChars="1234567890" FilterMode="ValidChars" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender19" runat="server" TargetControlID="txtCampus1N"
                                                                ValidChars="1234567890" FilterMode="ValidChars" />
                                                        </td>

                                                        <td>

                                                            <asp:Label ID="lblWD2" runat="server" Text="WD" Style='font-weight: bold;' CssClass="clk-box" ToolTip='<%# Bind("WEEKDAYSNAMENO1") %>'></asp:Label>
                                                            <asp:TextBox ID="txtCampus2" runat="server" CssClass="form-control clk-text" Width="65px" Enabled="false"
                                                                Font-Bold="true" Style="text-align: center" Text='<%# Bind("CAMPUSNOI2") %>' ToolTip='<%# Bind("CAMPUSNO2") %>' MaxLength="3" onblur="return Checkvalue(this);" />
                                                            <br />
                                                            <br />

                                                            <asp:Label ID="lblWE2" runat="server" Text="WE" Style='margin-left: 2px; font-weight: bold;' CssClass="clk-box" ToolTip='<%# Bind("WEEKDAYSNAMENO2") %>'></asp:Label>
                                                            <asp:TextBox ID="txtCampus2N" runat="server" CssClass="form-control clk-text" Width="65px" Enabled="false"
                                                                Font-Bold="true" Style="text-align: center" Text='<%# Bind("CAMPUSNOIN2") %>' ToolTip='<%# Bind("CAMPUSNO2") %>' MaxLength="3" onblur="return Checkvalue(this);" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtende21" runat="server" TargetControlID="txtCampus2"
                                                                ValidChars="1234567890" FilterMode="ValidChars" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender18" runat="server" TargetControlID="txtCampus2N"
                                                                ValidChars="1234567890" FilterMode="ValidChars" />

                                                        </td>

                                                        <td>

                                                            <asp:Label ID="lblWD3" runat="server" Text="WD" Style='font-weight: bold;' CssClass="clk-box" ToolTip='<%# Bind("WEEKDAYSNAMENO1") %>'></asp:Label>
                                                            <asp:TextBox ID="txtCampus3" runat="server" CssClass="form-control clk-text" Width="65px" Enabled="false"
                                                                Font-Bold="true" Style="text-align: center" Text='<%# Bind("CAMPUSNOI3") %>' ToolTip='<%# Bind("CAMPUSNO3") %>' MaxLength="3" onblur="return Checkvalue(this);" />
                                                            <br />
                                                            <br />

                                                            <asp:Label ID="lblWE3" runat="server" Text="WE" Style='margin-left: 2px; font-weight: bold;' CssClass="clk-box" ToolTip='<%# Bind("WEEKDAYSNAMENO2") %>'></asp:Label>
                                                            <asp:TextBox ID="txtCampus3N" runat="server" CssClass="form-control clk-text" Width="65px" Enabled="false"
                                                                Font-Bold="true" Style="text-align: center" Text='<%# Bind("CAMPUSNOIN3") %>' ToolTip='<%# Bind("CAMPUSNO3") %>' MaxLength="3" onblur="return Checkvalue(this);" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtCampus3"
                                                                ValidChars="1234567890" FilterMode="ValidChars" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" runat="server" TargetControlID="txtCampus3N"
                                                                ValidChars="1234567890" FilterMode="ValidChars" />
                                                        </td>

                                                        <td>

                                                            <asp:Label ID="lblWD4" runat="server" Text="WD" Style='font-weight: bold;' CssClass="clk-box" ToolTip='<%# Bind("WEEKDAYSNAMENO1") %>'></asp:Label>
                                                            <asp:TextBox ID="txtCampus4" runat="server" CssClass="form-control clk-text" Width="65px" Enabled="false"
                                                                Font-Bold="true" Style="text-align: center" Text='<%# Bind("CAMPUSNOI4") %>' ToolTip='<%# Bind("CAMPUSNO4") %>' MaxLength="3" onblur="return Checkvalue(this);" />
                                                            <br />
                                                            <br />

                                                            <asp:Label ID="lblWE4" runat="server" Text="WE" Style='margin-left: 2px; font-weight: bold;' CssClass="clk-box" ToolTip='<%# Bind("WEEKDAYSNAMENO2") %>'></asp:Label>
                                                            <asp:TextBox ID="txtCampus4N" runat="server" CssClass="form-control clk-text" Width="65px" Enabled="false"
                                                                Font-Bold="true" Style="text-align: center" Text='<%# Bind("CAMPUSNOIN4") %>' ToolTip='<%# Bind("CAMPUSNO4") %>' MaxLength="3" onblur="return Checkvalue(this);" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtCampus4"
                                                                ValidChars="1234567890" FilterMode="ValidChars" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" runat="server" TargetControlID="txtCampus4N"
                                                                ValidChars="1234567890" FilterMode="ValidChars" />
                                                        </td>

                                                        <td>

                                                            <asp:Label ID="lblWD5" runat="server" Text="WD" Style='font-weight: bold;' CssClass="clk-box" ToolTip='<%# Bind("WEEKDAYSNAMENO1") %>'></asp:Label>
                                                            <asp:TextBox ID="txtCampus5" runat="server" CssClass="form-control clk-text" Width="65px" Enabled="false"
                                                                Font-Bold="true" Style="text-align: center" Text='<%# Bind("CAMPUSNOI5") %>' ToolTip='<%# Bind("CAMPUSNO5") %>' MaxLength="3" onblur="return Checkvalue(this);" />
                                                            <br />
                                                            <br />

                                                            <asp:Label ID="lblWE5" runat="server" Text="WE" Style='margin-left: 2px; font-weight: bold;' CssClass="clk-box" ToolTip='<%# Bind("WEEKDAYSNAMENO2") %>'></asp:Label>
                                                            <asp:TextBox ID="txtCampus5N" runat="server" CssClass="form-control clk-text" Width="65px" Enabled="false"
                                                                Font-Bold="true" Style="text-align: center" Text='<%# Bind("CAMPUSNOIN5") %>' ToolTip='<%# Bind("CAMPUSNO5") %>' MaxLength="3" onblur="return Checkvalue(this);" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtCampus5"
                                                                ValidChars="1234567890" FilterMode="ValidChars" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server" TargetControlID="txtCampus5N"
                                                                ValidChars="1234567890" FilterMode="ValidChars" />
                                                        </td>

                                                        <td>

                                                            <asp:Label ID="lblWD6" runat="server" Text="WD" Style='font-weight: bold;' CssClass="clk-box" ToolTip='<%# Bind("WEEKDAYSNAMENO1") %>'></asp:Label>
                                                            <asp:TextBox ID="txtCampus6" runat="server" CssClass="form-control clk-text" Width="65px" Enabled="false"
                                                                Font-Bold="true" Style="text-align: center" Text='<%# Bind("CAMPUSNOI6") %>' ToolTip='<%# Bind("CAMPUSNO6") %>' MaxLength="3" onblur="return Checkvalue(this);" />
                                                            <br />
                                                            <br />

                                                            <asp:Label ID="lblWE6" runat="server" Text="WE" Style='margin-left: 2px; font-weight: bold;' CssClass="clk-box" ToolTip='<%# Bind("WEEKDAYSNAMENO2") %>'></asp:Label>
                                                            <asp:TextBox ID="txtCampus6N" runat="server" CssClass="form-control clk-text" Width="65px" Enabled="false"
                                                                Font-Bold="true" Style="text-align: center" Text='<%# Bind("CAMPUSNOIN6") %>' ToolTip='<%# Bind("CAMPUSNO6") %>' MaxLength="3" onblur="return Checkvalue(this);" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtCampus6"
                                                                ValidChars="1234567890" FilterMode="ValidChars" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server" TargetControlID="txtCampus6N"
                                                                ValidChars="1234567890" FilterMode="ValidChars" />
                                                        </td>

                                                        <td>

                                                            <asp:Label ID="lblWD7" runat="server" Text="WD" Style='font-weight: bold;' CssClass="clk-box" ToolTip='<%# Bind("WEEKDAYSNAMENO1") %>'></asp:Label>
                                                            <asp:TextBox ID="txtCampus7" runat="server" CssClass="form-control clk-text" Width="65px" Enabled="false"
                                                                Font-Bold="true" Style="text-align: center" Text='<%# Bind("CAMPUSNOI7") %>' ToolTip='<%# Bind("CAMPUSNO7") %>' MaxLength="3" onblur="return Checkvalue(this);" />
                                                            <br />
                                                            <br />

                                                            <asp:Label ID="lblWE7" runat="server" Text="WE" Style='margin-left: 2px; font-weight: bold;' CssClass="clk-box" ToolTip='<%# Bind("WEEKDAYSNAMENO2") %>'></asp:Label>
                                                            <asp:TextBox ID="txtCampus7N" runat="server" CssClass="form-control clk-text" Width="65px" Enabled="false"
                                                                Font-Bold="true" Style="text-align: center" Text='<%# Bind("CAMPUSNOIN7") %>' ToolTip='<%# Bind("CAMPUSNO7") %>' MaxLength="3" onblur="return Checkvalue(this);" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txtCampus7"
                                                                ValidChars="1234567890" FilterMode="ValidChars" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" TargetControlID="txtCampus7N"
                                                                ValidChars="1234567890" FilterMode="ValidChars" />
                                                        </td>

                                                        <td>

                                                            <asp:Label ID="lblWD8" runat="server" Text="WD" Style='font-weight: bold;' CssClass="clk-box" ToolTip='<%# Bind("WEEKDAYSNAMENO1") %>'></asp:Label>
                                                            <asp:TextBox ID="txtCampus8" runat="server" CssClass="form-control clk-text" Width="65px" Enabled="false"
                                                                Font-Bold="true" Style="text-align: center" Text='<%# Bind("CAMPUSNOI8") %>' ToolTip='<%# Bind("CAMPUSNO8") %>' MaxLength="3" onblur="return Checkvalue(this);" />
                                                            <br />
                                                            <br />

                                                            <asp:Label ID="lblWE8" runat="server" Text="WE" Style='margin-left: 2px; font-weight: bold;' CssClass="clk-box" ToolTip='<%# Bind("WEEKDAYSNAMENO2") %>'></asp:Label>
                                                            <asp:TextBox ID="txtCampus8N" runat="server" CssClass="form-control clk-text" Width="65px" Enabled="false"
                                                                Font-Bold="true" Style="text-align: center" Text='<%# Bind("CAMPUSNOIN8") %>' ToolTip='<%# Bind("CAMPUSNO8") %>' MaxLength="3" onblur="return Checkvalue(this);" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="txtCampus8"
                                                                ValidChars="1234567890" FilterMode="ValidChars" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" TargetControlID="txtCampus8N"
                                                                ValidChars="1234567890" FilterMode="ValidChars" />
                                                        </td>

                                                        <td>

                                                            <asp:Label ID="lblWD9" runat="server" Text="WD" Style='font-weight: bold;' CssClass="clk-box" ToolTip='<%# Bind("WEEKDAYSNAMENO1") %>'></asp:Label>
                                                            <asp:TextBox ID="txtCampus9" runat="server" CssClass="form-control clk-text" Width="65px" Enabled="false"
                                                                Font-Bold="true" Style="text-align: center" Text='<%# Bind("CAMPUSNOI9") %>' ToolTip='<%# Bind("CAMPUSNO9") %>' MaxLength="3" onblur="return Checkvalue(this);" />
                                                            <br />
                                                            <br />

                                                            <asp:Label ID="lblWE9" runat="server" Text="WE" Style='margin-left: 2px; font-weight: bold;' CssClass="clk-box" ToolTip='<%# Bind("WEEKDAYSNAMENO2") %>'></asp:Label>
                                                            <asp:TextBox ID="txtCampus9N" runat="server" CssClass="form-control clk-text" Width="65px" Enabled="false"
                                                                Font-Bold="true" Style="text-align: center" Text='<%# Bind("CAMPUSNOIN9") %>' ToolTip='<%# Bind("CAMPUSNO9") %>' MaxLength="3" onblur="return Checkvalue(this);" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" TargetControlID="txtCampus9"
                                                                ValidChars="1234567890" FilterMode="ValidChars" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" TargetControlID="txtCampus9N"
                                                                ValidChars="1234567890" FilterMode="ValidChars" />
                                                        </td>

                                                        <td>

                                                            <asp:Label ID="lblWD10" runat="server" Text="WD" Style='font-weight: bold;' CssClass="clk-box" ToolTip='<%# Bind("WEEKDAYSNAMENO1") %>'></asp:Label>
                                                            <asp:TextBox ID="txtCampus10" runat="server" CssClass="form-control clk-text" Width="65px" Enabled="false"
                                                                Font-Bold="true" Style="text-align: center" Text='<%# Bind("CAMPUSNOI10") %>' ToolTip='<%# Bind("CAMPUSNO10") %>' MaxLength="3" onblur="return Checkvalue(this);" />
                                                            <br />
                                                            <br />

                                                            <asp:Label ID="lblWE10" runat="server" Text="WE" Style='margin-left: 2px; font-weight: bold;' CssClass="clk-box" ToolTip='<%# Bind("WEEKDAYSNAMENO2") %>'></asp:Label>
                                                            <asp:TextBox ID="txtCampus10N" runat="server" CssClass="form-control clk-text" Width="65px" Enabled="false"
                                                                Font-Bold="true" Style="text-align: center" Text='<%# Bind("CAMPUSNOIN10") %>' ToolTip='<%# Bind("CAMPUSNO10") %>' MaxLength="3" onblur="return Checkvalue(this);" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" TargetControlID="txtCampus10"
                                                                ValidChars="1234567890" FilterMode="ValidChars" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtCampus10N"
                                                                ValidChars="1234567890" FilterMode="ValidChars" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>
                                    
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="lnkSubmit" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnShowCopy" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnCancelCopy" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlCollegeSchNameC" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlIntakeC" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlStudyLevelC" />
                                    <%-- <asp:AsyncPostBackTrigger ControlID="ddlIntakeC"/>--%>
                                    <asp:AsyncPostBackTrigger ControlID="ddlClassificationC" />
                                    <%-- <asp:PostBackTrigger ControlID="gvCampus" />--%>
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>


                    </div>
                </div>
            </div>
        </div>
    </div>


    <script type="text/javascript">
        function UserDeleteConfirmation() {
            if (confirm("Are you sure you want to delete this Entry?"))
                return true;
            else
                return false;
        }
    </script>
    <script type="text/javascript">

        function validateShow() {
            var id = (document.getElementById("<%=btnSave.ClientID%>"));
            // ShowLoader(id);
            //  ShowLoader('#btnSave');
            var rfvAdmbatch = (document.getElementById("<%=lblDYAdmbatch.ClientID%>").innerHTML);
            var rfvCollege = (document.getElementById("<%=lblDYCollege.ClientID%>").innerHTML);

            var alertmsg = '';

            if (document.getElementById("<%=ddlColg.ClientID%>").value == "0") {
                if (alertmsg == '') {
                    document.getElementById("<%=ddlColg.ClientID%>").focus();
                }
                alertmsg = 'Please Select ' + rfvCollege + ' \n';

            }
            if (document.getElementById("<%=ddlAdmBatch.ClientID%>").value == "0") {
                if (alertmsg == '') {
                    document.getElementById("<%=ddlAdmBatch.ClientID%>").focus();
                }
                alertmsg += 'Please Select ' + rfvAdmbatch + ' \n';

            }

            if (document.getElementById("<%=ddlProgClassification.ClientID%>").value == "0") {
                if (alertmsg == '') {
                    document.getElementById("<%=ddlProgClassification.ClientID%>").focus();
                }
                alertmsg += 'Please Select Study Level';

            }


            if (alertmsg != '') {
                alert(alertmsg);
                //HideLoader('Submit');
                // HideLoader(id, 'Submit');
                return false;
            }

            return true;
        }

        function Checkvalue(id) {
            if (id.value > 0 || id.value == "") {
            }
            else {
                alert("Campus intake should be greater  than 0 !");
                id.value = '';
                id.focus();
                return false;
            }
        }

        $(function () {
            $('.ba').on('change', function () {
                var checked = $(this).prop('checked');
                $('.tex').prop('disabled', !checked);
            });
        });

    </script>


    <script type="text/javascript">

        //Offer Copy dated 13-10-2021
        function validateShowCopy() {
            var id = (document.getElementById("<%=lnkSubmit.ClientID%>"));
            //ShowLoader(id);
            //  ShowLoader('#btnSave');


            var alertmsg = '';

            // if (document.getElementById("<%=ddlCollegeSchNameC.ClientID%>").value == "0") {
            //     if (alertmsg == '') {
            //         document.getElementById("<%=ddlCollegeSchNameC.ClientID%>").focus();
            //     }
            //     alertmsg = 'Please Select Faculty/School Name \n';
            //
            // }
            if (document.getElementById("<%=ddlIntakeC.ClientID%>").value == "0") {
                if (alertmsg == '') {
                    document.getElementById("<%=ddlIntakeC.ClientID%>").focus();
                }
                alertmsg += 'Please Select Intake \n';

            }

            //  if (document.getElementById("<%=ddlStudyLevelC.ClientID%>").value == "0") {
            //      if (alertmsg == '') {
            //          document.getElementById("<%=ddlStudyLevelC.ClientID%>").focus();
            //      }
            //      alertmsg += 'Please Select Study Level \n';
            //      
            //  }

            if (document.getElementById("<%=ddlToIntake.ClientID%>").value == "0" || document.getElementById("<%=ddlToIntake.ClientID%>").value == "") {
                if (alertmsg == '') {
                    document.getElementById("<%=ddlToIntake.ClientID%>").focus();
                }
                alertmsg += 'Please Select To Intake';

            }


            if (alertmsg != '') {
                alert(alertmsg);
                //HideLoader('Submit');
                //   HideLoader(id, 'Submit');
                return false;
            }

            return true;
        }


        //$(function () {
        //    $('.ba').on('change', function () {
        //        var checked = $(this).prop('checked');
        //        $('.tex').prop('disabled', !checked);
        //    });
        //});


    </script>

    <script>
        $(document).ready(function () {

            $("td").each(function () {
                if ($(this).css(display) == "none") {
                    $(this).remove();
                }
            });

            $('.clk-box input[type="checkbox"]').each(function () {
                if ($(this).prop('checked')) {

                    $(this).parent().next(".clk-text").attr('disabled', false);
                }
                else {
                    $(this).parent().next(".clk-text").val('');
                    $(this).parent().next(".clk-text").attr('disabled', true);

                }
            });

            $(document).on("change", ".clk-box input[type='checkbox']", function () {

                if ($(this).prop('checked')) {
                    $(this).parent().next(".clk-text").attr('disabled', false);
                }
                else {
                    $(this).parent().next(".clk-text").val('');
                    $(this).parent().next(".clk-text").attr('disabled', true);

                }

                $('#myTable').on('draw.dt', function () {
                    $('.clk-box input[type="checkbox"]').each(function () {
                        if ($(this).prop('checked')) {
                            $(this).parent().next(".clk-text").attr('disabled', false);
                        }
                        else {
                            $(this).parent().next(".clk-text").val('');
                            $(this).parent().next(".clk-text").attr('disabled', true);

                        }
                    });
                });
            });
        });
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(document).ready(function () {
                $('.clk-box input[type="checkbox"]').each(function () {
                    if ($(this).prop('checked')) {
                        $(this).parent().next(".clk-text").attr('disabled', false);
                    }
                    else {
                        $(this).parent().next(".clk-text").val('');
                        $(this).parent().next(".clk-text").attr('disabled', true);

                    }
                });

                $(document).on("change", ".clk-box input[type='checkbox']", function () {

                    if ($(this).prop('checked')) {
                        $(this).parent().next(".clk-text").attr('disabled', false);
                    }
                    else {
                        $(this).parent().next(".clk-text").val('');
                        $(this).parent().next(".clk-text").attr('disabled', true);

                    }
                });

                $('#myTable').on('draw.dt', function () {
                    $('.clk-box input[type="checkbox"]').each(function () {
                        if ($(this).prop('checked')) {
                            $(this).parent().next(".clk-text").attr('disabled', false);
                        }
                        else {
                            $(this).parent().next(".clk-text").val('');
                            $(this).parent().next(".clk-text").attr('disabled', true);

                        }
                    });
                });
            });
        });
    </script>
</asp:Content>

