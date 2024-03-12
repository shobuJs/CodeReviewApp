<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="OnlinePaymentAdminLogin.aspx.cs" Inherits="ACADEMIC_OnlinePaymentAdminLogin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%-- <script>
        $(document).ready(function () {
            $("#ctl00_ContentPlaceHolder1_OKButton").click(function () {
                $("#ctl00_ContentPlaceHolder1_divlkPayment").removeClass("active");
            });
        });
    </script>--%>

    <link href="<%=Page.ResolveClientUrl("~/css/ImageViewer.css")%>" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="<%=Page.ResolveClientUrl("~/css/jquery.verySimpleImageViewer.css")%>">
    <script src="<%=Page.ResolveClientUrl("~/js/jquery.verySimpleImageViewer.js")%>"></script>

    <style>
        #ctl00_ContentPlaceHolder1_imageViewerContainer {
            max-width: 200px;
            height: 200px;
            margin: 50px auto;
            border: 1px solid #000;
            border-radius: 3px;
        }

        .image_viewer_inner_container {
            overflow: scroll !important;
        }

        #ctl00_ContentPlaceHolder1_imageViewerContainer .jqvsiv_main_image_content {
            text-align: center !important;
        }

            #ctl00_ContentPlaceHolder1_imageViewerContainer .jqvsiv_main_image_content img {
                /*position: initial !important;*/
                z-index: 3;
                cursor: n-resize;
            }
    </style>

    <style>
        #ctl00_ContentPlaceHolder1_Panel1 .dataTables_scrollHeadInner {
            width: max-content !important;
        }

        .std-info {
            background: #fff;
            box-shadow: rgb(0 0 0 / 20%) 0px 2px 10px;
            border-radius: 5px;
            margin-bottom: 8px;
        }

        .std-dtl.std-info {
            box-shadow: rgb(0 0 0 / 20%) 0px 2px 5px;
        }

        .sbs-title {
            color: #c3c8cf;
            text-transform: uppercase;
            font-size: 0.625rem;
            margin: 0 0 1rem;
            padding: 1rem 0;
        }

            .sbs-title:first-of-type {
                padding-top: 0;
                margin-top: 0;
            }

        hr {
            border-top: 1px solid #edeff1;
            margin: 3rem 0 2rem;
        }

        .sbs--basic {
            margin: 0 -1rem;
            display: -webkit-box;
            display: -ms-flexbox;
            display: flex;
        }

            .sbs--basic li {
                -webkit-box-flex: 1;
                -ms-flex: 1;
                flex: 1;
                padding: 0 1rem;
            }



                .sbs--basic li .step {
                    padding: 2rem 0 0;
                    display: -webkit-box;
                    display: -ms-flexbox;
                    display: flex;
                    -webkit-box-orient: vertical;
                    -webkit-box-direction: normal;
                    -ms-flex-direction: column;
                    flex-direction: column;
                    position: relative;
                }

                    .sbs--basic li .step::before {
                        content: "";
                        position: absolute;
                        top: 0;
                        left: 0;
                        height: 0;
                        width: 100%;
                        border-top: 4px solid #dfe2e5;
                    }

                    .sbs--basic li .step .title {
                        margin-bottom: 0.5rem;
                        text-transform: uppercase;
                        font-size: 0.875rem;
                        font-weight: bold;
                        color: #8a94a1;
                    }

                    .sbs--basic li .step .description {
                        font-weight: bold;
                    }

        .sbs--border {
            display: -webkit-box;
            display: -ms-flexbox;
            display: flex;
            border: 1px solid #d1d5da;
            border-radius: 0.5rem;
        }

            .sbs--border li {
                -webkit-box-flex: 1;
                -ms-flex: 1;
                flex: 1;
            }

                .sbs--border li:last-of-type .step::before,
                .sbs--border li:last-of-type .step::after {
                    display: none;
                }

                .sbs--border li.finished > .step .indicator {
                    background-color: #257f3e;
                    border-color: #257f3e;
                    color: white;
                }

                .sbs--border li.finished > .step .description {
                    color: #2f363d;
                }

                .sbs--border li.active > .step .indicator {
                    border-color: #257f3e;
                    color: #257f3e;
                }

                .sbs--border li.active > .step .description {
                    color: #1f6c35;
                }

                .sbs--border li .step {
                    padding: 1rem 1.5rem;
                    display: -webkit-box;
                    display: -ms-flexbox;
                    display: flex;
                    -webkit-box-align: center;
                    -ms-flex-align: center;
                    align-items: center;
                    position: relative;
                }

                    .sbs--border li .step::before, .sbs--border li .step::after {
                        content: "";
                        height: 45px;
                        width: 1px;
                        background-color: #d1d5da;
                        position: absolute;
                        right: 0;
                        top: 50%;
                    }

                    .sbs--border li .step::before {
                        -webkit-transform-origin: center bottom;
                        transform-origin: center bottom;
                        -webkit-transform: translateY(-100%) rotate(-25deg);
                        transform: translateY(-100%) rotate(-25deg);
                    }

                    .sbs--border li .step::after {
                        -webkit-transform-origin: center top;
                        transform-origin: center top;
                        -webkit-transform: rotate(25deg);
                        transform: rotate(25deg);
                    }

                    .sbs--border li .step .indicator {
                        display: -webkit-box;
                        display: -ms-flexbox;
                        display: flex;
                        -webkit-box-align: center;
                        -ms-flex-align: center;
                        align-items: center;
                        -webkit-box-pack: center;
                        -ms-flex-pack: center;
                        justify-content: center;
                        font-weight: bold;
                        width: 3rem;
                        height: 3rem;
                        border-radius: 50%;
                        margin-right: 1rem;
                        border: 2px solid #d1d5da;
                        color: #8a94a1;
                    }

                    .sbs--border li .step .description {
                        font-weight: bold;
                        font-size: 0.875rem;
                        color: #8a94a1;
                    }

        .sbs--border-alt {
            display: -webkit-box;
            display: -ms-flexbox;
            display: flex;
            border: 1px solid #d1d5da;
            border-radius: 0.5rem;
        }

            .sbs--border-alt li {
                -webkit-box-flex: 1;
                -ms-flex: 1;
                flex: 1;
            }

                .sbs--border-alt li:last-of-type .step::before,
                .sbs--border-alt li:last-of-type .step::after {
                    display: none;
                }

                .sbs--border-alt li.finished > .step .indicator {
                    background-color: #257f3e;
                    border-color: #257f3e;
                    color: white;
                }

                .sbs--border-alt li.finished > .step .description {
                    color: #2f363d;
                }

                .sbs--border-alt li.active > .step .indicator {
                    border-color: #257f3e;
                    color: #257f3e;
                }

                .sbs--border-alt li.active > .step .description {
                    color: #1f6c35;
                }

                .sbs--border-alt li.active > .step .line {
                    background-color: green;
                }

                .sbs--border-alt li .step {
                    padding: 1rem 1.5rem;
                    display: -webkit-box;
                    display: -ms-flexbox;
                    display: flex;
                    -webkit-box-align: center;
                    -ms-flex-align: center;
                    align-items: center;
                    position: relative;
                }

                    .sbs--border-alt li .step::before {
                        content: "";
                        width: 1px;
                        height: 100%;
                        background-color: #d1d5da;
                        position: absolute;
                        right: 0;
                    }

                    .sbs--border-alt li .step::after {
                        content: "";
                        width: 1rem;
                        height: 1rem;
                        background-color: white;
                        position: absolute;
                        right: 0;
                        top: 50%;
                        border-top: 1px solid #d1d5da;
                        border-right: 1px solid #d1d5da;
                        -webkit-transform: translate(50%, -50%) rotate(45deg);
                        transform: translate(50%, -50%) rotate(45deg);
                        z-index: 1;
                    }

                    .sbs--border-alt li .step .indicator {
                        display: -webkit-box;
                        display: -ms-flexbox;
                        display: flex;
                        -webkit-box-align: center;
                        -ms-flex-align: center;
                        align-items: center;
                        -webkit-box-pack: center;
                        -ms-flex-pack: center;
                        justify-content: center;
                        font-weight: bold;
                        width: 3rem;
                        height: 3rem;
                        border-radius: 50%;
                        margin-right: 1rem;
                        border: 2px solid #d1d5da;
                        color: #8a94a1;
                    }

                    .sbs--border-alt li .step .description {
                        font-weight: bold;
                        font-size: 0.875rem;
                        color: #8a94a1;
                        display: -webkit-box;
                        display: -ms-flexbox;
                        display: flex;
                        -webkit-box-orient: vertical;
                        -webkit-box-direction: normal;
                        -ms-flex-direction: column;
                        flex-direction: column;
                    }

                        .sbs--border-alt li .step .description span:first-of-type {
                            margin-bottom: 0.25rem;
                            text-transform: uppercase;
                        }

                        .sbs--border-alt li .step .description span:last-of-type {
                            color: #b5bbc3;
                        }

                    .sbs--border-alt li .step .line {
                        position: absolute;
                        width: 100%;
                        height: 4px;
                        background-color: transparent;
                        bottom: 0;
                        left: 0;
                    }

        .sbs--circles {
            display: -webkit-box;
            display: -ms-flexbox;
            display: flex;
        }

            .sbs--circles li {
                -webkit-box-flex: 1;
                -ms-flex: 1;
                flex: 1;
                display: -webkit-box;
                display: -ms-flexbox;
                display: flex;
                -webkit-box-orient: vertical;
                -webkit-box-direction: normal;
                -ms-flex-direction: column;
                flex-direction: column;
            }

                .sbs--circles li:last-of-type .step .line {
                    display: none;
                }

                .sbs--circles li.finished > .step .indicator {
                    background-color: #257f3e;
                    border-color: #257f3e;
                    color: white;
                }

                .sbs--circles li.finished > .step .description {
                    color: #2f363d;
                }

                .sbs--circles li.finished > .step .line {
                    background-color: #257f3e;
                }

                .sbs--circles li.active > .step .indicator {
                    border-color: #257f3e;
                    color: #257f3e;
                }

                .sbs--circles li.active > .step .description {
                    color: #1f6c35;
                }

                .sbs--circles li .step {
                    display: -webkit-box;
                    display: -ms-flexbox;
                    display: flex;
                    -webkit-box-orient: vertical;
                    -webkit-box-direction: normal;
                    -ms-flex-direction: column;
                    flex-direction: column;
                    -webkit-box-align: center;
                    -ms-flex-align: center;
                    align-items: center;
                    position: relative;
                }

                    .sbs--circles li .step .indicator {
                        display: -webkit-box;
                        display: -ms-flexbox;
                        display: flex;
                        -webkit-box-align: center;
                        -ms-flex-align: center;
                        align-items: center;
                        -webkit-box-pack: center;
                        -ms-flex-pack: center;
                        justify-content: center;
                        font-weight: bold;
                        width: 3rem;
                        height: 3rem;
                        border-radius: 50%;
                        border: 2px solid #d1d5da;
                        color: #8a94a1;
                        position: relative;
                        z-index: 1;
                        background-color: white;
                    }

                    .sbs--circles li .step .description {
                        font-weight: bold;
                        font-size: 0.875rem;
                        color: #8a94a1;
                        position: absolute;
                        bottom: -1.5rem;
                    }

                    .sbs--circles li .step .line {
                        height: 4px;
                        background-color: #dfe2e5;
                        width: 100%;
                        position: absolute;
                        top: 50%;
                        left: 50%;
                        -webkit-transform: translateY(-50%);
                        transform: translateY(-50%);
                    }

        .sbs--dots {
            display: -webkit-box;
            display: -ms-flexbox;
            display: flex;
            -webkit-box-orient: vertical;
            -webkit-box-direction: normal;
            -ms-flex-direction: column;
            flex-direction: column;
            width: 200px;
            margin: 0 auto;
        }

            .sbs--dots li {
                padding: 1rem 0;
            }

                .sbs--dots li.finished > .step .indicator {
                    background-color: #257f3e;
                    border-color: #257f3e;
                    color: white;
                }

                .sbs--dots li.finished > .step .description {
                    color: #2f363d;
                }

                .sbs--dots li.finished > .step .line {
                    background-color: #257f3e;
                }

                .sbs--dots li.active > .step .indicator {
                    border-color: #cae4d1;
                    background-color: green;
                }

                .sbs--dots li.active > .step .description {
                    color: #1f6c35;
                }

                .sbs--dots li .step {
                    display: -webkit-box;
                    display: -ms-flexbox;
                    display: flex;
                    position: relative;
                }

                    .sbs--dots li .step .indicator {
                        display: -webkit-box;
                        display: -ms-flexbox;
                        display: flex;
                        -webkit-box-align: center;
                        -ms-flex-align: center;
                        align-items: center;
                        -webkit-box-pack: center;
                        -ms-flex-pack: center;
                        justify-content: center;
                        font-weight: bold;
                        width: 1.125rem;
                        height: 1.125rem;
                        border-radius: 50%;
                        border: 4px solid white;
                        color: #8a94a1;
                        position: absolute;
                        top: 50%;
                        left: -2rem;
                        -webkit-transform: translateY(-50%);
                        transform: translateY(-50%);
                        background-color: #d1d5da;
                        font-size: 0.625rem;
                    }

                    .sbs--dots li .step .description {
                        font-weight: bold;
                        font-size: 0.875rem;
                        color: #8a94a1;
                    }

        .sbs > li {
            cursor: pointer;
        }

            .sbs > li.active {
                cursor: default;
            }

        .sbs--basic li.finished .step::before {
            border-color: green;
        }

        .sbs--basic li.finished .step .title {
            color: #1f6c35;
        }

        .sbs--basic li.active .step::before {
            border-color: green;
            border-top-style: dotted;
        }

        .sbs--basic li.active .step .title {
            color: #1f6c35;
        }

        .dynamic-nav-tabs li.active a {
            color: #255282;
        }

        .nav-tabs-custom .nav-tabs .nav-link:focus, .nav-tabs-custom .nav-tabs .nav-link:hover {
            border-color: #fff #fff #fff;
        }

            .nav-tabs-custom .nav-tabs .nav-link:focus .description, .nav-tabs-custom .nav-tabs .nav-link:hover .description {
                color: #000;
            }
    </style>
    <style>
        .pay-opt {
            display: flex;
        }

        @media (max-width:767px) {
            .pay-opt {
                display: inline-block;
            }
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

    <div class="row" id="DivUpdate" runat="server" visible="true">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                    <%--   <h3 class="box-title">New Student Enrollment</h3>--%>
                </div>
                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">

                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <asp:Label ID="lblApplicationNo" runat="server" Font-Bold="true">Application No.</asp:Label>
                                </div>
                                <asp:TextBox ID="txtAplicationNo" runat="server" TabIndex="1" CssClass="form-control"
                                    MaxLength="100" ToolTip="Please Enter Application No." ClientIDMode="Static" />

                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">

                                <div class="label-dynamic">
                                    <sup></sup>
                                    <asp:Label ID="Label3" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <asp:Button runat="server" ID="btnsearch" Text="Search" CssClass="btn btn-outline-info" OnClick="btnsearch_Click" />

                            </div>
                        </div>
                    </div>
                </div>

                <div class="nav-tabs-custom mt-2 col-12 pb-4" id="myTabContent">
                    <ul class="nav nav-tabs dynamic-nav-tabs sbs sbs--basic" role="tablist">
                        <li class="nav-item" id="divacceptance" runat="server">
                            <asp:LinkButton ID="lkacceptance" runat="server" OnClick="lkacceptance_Click" CssClass="nav-link" TabIndex="1">
                                 <div class="step">
                                    <span class="title">Step 1</span>
                                    <span class="description">Conditional Offer of Acceptance</span>
                                </div>
                            </asp:LinkButton>
                        </li>
                        <li class="nav-item" id="divOnlineDetails" runat="server">
                            <asp:LinkButton ID="lnkOnlineDetails" runat="server" CssClass="nav-link" TabIndex="2" OnClick="lnkOnlineDetails_Click">
                                <div class="step">
                                    <span class="title">Step 2</span>
                                    <span class="description">Applicant Detail</span>
                                </div>
                            </asp:LinkButton>
                        </li>
                        <li class="nav-item" id="divlkuploaddocument" runat="server">
                            <asp:LinkButton ID="lkuploaddocumnet" runat="server" OnClick="lkuploaddocumnet_Click" CssClass="nav-link" TabIndex="3">
                                <div class="step">
                                    <span class="title">Step 3</span>
                                    <span class="description">Documents</span>
                                </div>
                            </asp:LinkButton>
                        </li>
                        <li class="nav-item" id="divlkModuleOffer" runat="server" visible="false">
                            <asp:LinkButton ID="lkModuleRegistration" runat="server" OnClick="lkModuleRegistration_Click" CssClass="nav-link" TabIndex="4">
                                <div class="step">
                                    <span class="title">Step 4</span>
                                    <span class="description">Subjects</span>
                                </div>
                            </asp:LinkButton>
                        </li>
                        <li class="nav-item active" id="divlkPayment" runat="server">
                            <asp:LinkButton ID="lkpayment" runat="server" OnClick="lkpayment_Click" CssClass="nav-link" TabIndex="5">
                                <div class="step">
                                    <span class="title">Step 4</span>
                                    <span class="description">Payment</span>
                                </div>
                            </asp:LinkButton>
                        </li>
                        <li class="nav-item" id="divlkstatus" runat="server">
                            <asp:LinkButton ID="lkstatus" runat="server" OnClick="lkstatus_Click" CssClass="nav-link" TabIndex="6">
                                <div class="step">
                                    <span class="title">Step 5</span>
                                    <span class="description">Status</span>
                                </div>
                            </asp:LinkButton>
                        </li>
                    </ul>
                    <div class="tab-content">

                        <div class="tab-pane fade show active" id="acceptance" role="tabpanel" runat="server" aria-labelledby="IntakeTransfer-tab">
                            <div>
                                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
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
                                    <div id="divaceptpayment" runat="server">
                                        <div class="box-body">
                                            <div class="col-12 mb-3">
                                                <div class="row">
                                                    <div class="col-lg-4 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Application No. :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblRegN" Font-Bold="true" runat="server" />
                                                                </a>
                                                            </li>
                                                            <li class="list-group-item"><b>
                                                                <asp:Label ID="lblStudName" Font-Bold="true" runat="server" />:</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblStudNam" Font-Bold="true" runat="server" /></a>
                                                            </li>
                                                            <li class="list-group-item"><b>Gender :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblSex" Font-Bold="true" runat="server" /></a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                    <div class="col-lg-4 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>
                                                                <asp:Label ID="lblYear" Font-Bold="true" runat="server" />:</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblYea" Font-Bold="true" runat="server" />
                                                                </a>
                                                            </li>
                                                            <li class="list-group-item"><b>
                                                                <asp:Label ID="lblBatch" Font-Bold="true" runat="server" />:</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblBatc" Font-Bold="true" runat="server" /></a>
                                                            </li>
                                                            <li class="list-group-item"><b>
                                                                <asp:Label ID="lblSemester" Font-Bold="true" runat="server" />:</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblSemeste" Font-Bold="true" runat="server" /></a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                    <div class="col-lg-4 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>
                                                                <asp:Label ID="lblMobileNo" Font-Bold="true" runat="server" />:</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblMobileN" Font-Bold="true" runat="server" /></a>
                                                            </li>
                                                            <li class="list-group-item"><b>Email :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblEmailID" Font-Bold="true" runat="server" /></a>
                                                            </li>
                                                            <li class="list-group-item"><b>
                                                                <asp:Label ID="lblPaymentType" Font-Bold="true" runat="server" />:</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblPaymentTy" Font-Bold="true" runat="server" /></a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divRecieptType" runat="server" visible="false">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Receipt Type</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlReceiptType" CssClass="form-control" data-select2-enable="true" runat="server" AppendDataBoundItems="true" Enabled="false">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divprgmname" runat="server" visible="false">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Program Name</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlProgramName" CssClass="form-control" data-select2-enable="true" runat="server" AppendDataBoundItems="true" Visible="false">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlProgramName" InitialValue="0"
                                                            Display="None" ErrorMessage="Please Select Program Name" ValidationGroup="Summary"></asp:RequiredFieldValidator>
                                                    </div>


                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divsemester" runat="server" visible="false">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Semester</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlSemester" CssClass="form-control" data-select2-enable="true" runat="server" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>


                                                    <div class="col-12">
                                                        <asp:Panel ID="pnlProgramName" runat="server" Visible="true">
                                                            <asp:ListView ID="lstProgramName" runat="server">
                                                                <LayoutTemplate>
                                                                    <div>
                                                                        <div class="sub-heading">
                                                                            <h5>Program Name</h5>
                                                                        </div>
                                                                        <div class="table-responsive">
                                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                                                <thead class="bg-light-blue">
                                                                                    <tr id="trRow">
                                                                                        <%--<th id="thHead" style="width: 5%"></th>--%>
                                                                                        <th>
                                                                                            <asp:Label ID="lblAction" runat="server"></asp:Label></th>
                                                                                        <th>
                                                                                            <asp:Label ID="lblClassification" runat="server"></asp:Label>
                                                                                        </th>
                                                                                        <th>
                                                                                            <asp:Label ID="lblfaculty" runat="server"></asp:Label>
                                                                                        </th>
                                                                                        <th>
                                                                                            <asp:Label ID="lblprogram" runat="server"></asp:Label>
                                                                                        </th>
                                                                                        <th>
                                                                                            <asp:Label ID="lblAwarding" runat="server"></asp:Label>
                                                                                        </th>
                                                                                        <th>Acceptance Date</th>
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
                                                                            <asp:CheckBox ID="chkRowsProgram" runat="server" Enabled='<%# (Convert.ToString(Eval("TEMP_DCR_NO")) == string.Empty || Convert.ToString(Eval("RECON")) == "0") ? true:false   %>' onclick="CheckUnchekCheckbox(this);" Checked='<%# Convert.ToInt32(Eval("PROGRAM_ACCEPTED")) > 0 ? true:false   %>' />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblarea" runat="server" Text='<%# Eval("AREA_INT_NAME") %>' ToolTip='<%# Eval("AREA_INT_NO") %>' />
                                                                            <%-- <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>'></asp:Label>
                                                                    <asp:HiddenField ID="hdfCourseNo" runat="server" Value='<%# Eval("COURSENO") %>' />
                                                                    <asp:HiddenField ID="hdfSubid" runat="server" Value='<%# Eval("SUBID") %>' />--%>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblcollegename" runat="server" Text='<%# Eval("COLLEGE_NAME") %>' ToolTip='<%# Eval("COLLEGE_ID") %>' /></td>

                                                                        <%-- <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>'></asp:Label></td>--%>
                                                                        <td>
                                                                            <asp:Label ID="lbldegree" runat="server" Text='<%# Eval("PROGRAM_NAME") %>' ToolTip='<%# Eval("DEGREENO") %>' />
                                                                            <asp:HiddenField ID="hdfdegree" runat="server" Value='<%# Eval("DEGREENO") %>' />
                                                                            <asp:HiddenField ID="hdfbranchno" runat="server" Value='<%# Eval("BRANCHNO") %>' />

                                                                        </td>

                                                                        <td>
                                                                            <asp:Label ID="lblAffiliated" runat="server" Text='<%# Eval("AFFILIATED_SHORTNAME") %>' ToolTip='<%# Eval("AFFILIATED_NO") %>' /></td>
                                                                        <td>
                                                                            <asp:Label ID="lblAcceptanceDate" runat="server" Text='<%# Eval("PROGRAM_ACCEPTED_DATE") %>' /></td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>

                                                        </asp:Panel>
                                                    </div>

                                                    <div class="col-12 btn-footer">
                                                        <%--<asp:Button ID="btnShowDetails" runat="server" Text="Accept" CssClass="btn btn-outline-info" OnClick="btnShowDetails_Click" ValidationGroup="Summary" />--%>

                                                        <asp:Button ID="btnShowDetails" runat="server" Text="Conditional Offer of Acceptance" CssClass="btn btn-outline-info" OnClick="btnShowDetails_Click" ValidationGroup="Summary" />
                                                        <asp:Button runat="server" Text="Cancel" ID="btnCanceld" OnClick="btnCanceld_Click" CssClass="btn btn-outline-danger" />
                                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                                            ShowMessageBox="True" ValidationGroup="Summary" ShowSummary="False" />
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12" visible="false" runat="server" id="divamount">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Amount </label>
                                                        </div>
                                                        <div class="input-group">
                                                            <div class="input-group-addon">
                                                                <div class="fa fa-inr text-green"></div>
                                                            </div>
                                                            <asp:TextBox ID="txtAmount" runat="server" TabIndex="3" CssClass="form-control" ToolTip="Please Enter Amount" onkeypress="CheckNumeric(event);" Width="90px" MaxLength="10">
                                                            </asp:TextBox>
                                                            <div class="input-group-addon">
                                                                <span>.00</span>
                                                            </div>
                                                        </div>
                                                        <%-- <asp:RequiredFieldValidator ID="rfvtxtamount" runat="server" ControlToValidate="txtAmount" ValidationExpression="(^([0-9]*|\d*\d{1}?\d*)$)" 
                                                Display="None" ErrorMessage="Please Enter Amount" ValidationGroup="save"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                                                    </div>
                                                    <div id="Div34" class="col-12" runat="server" visible="false">
                                                        <asp:ListView ID="lvOfferAccept" runat="server">
                                                            <LayoutTemplate>
                                                                <div>
                                                                    <div class="sub-heading">
                                                                        <h5>Offer Acceptance Details</h5>
                                                                    </div>
                                                                    <div class="table-responsive">
                                                                        <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                                            <thead class="bg-light-blue">
                                                                                <tr id="trRow">
                                                                                    <%--<th id="thHead" style="width: 5%"></th>--%>
                                                                                    <th>SrNo.
                                                                                    </th>
                                                                                    <th>Delete
                                                                                    </th>
                                                                                    <%--<th>Degree
                                                                                </th>--%>
                                                                                    <th>Program
                                                                                    </th>
                                                                                    <th>Acceptance Date
                                                                                    </th>
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
                                                                    <%--<td style="width: 10%"></td>--%>
                                                                    <td><%# Container.DisplayIndex + 1 %>
                                                                    </td>

                                                                    <%--<td>
                                                                     <asp:LinkButton ID="lnkOfferDelete" runat="server" CssClass="fa fa-trash" Style="color: red" ToolTip="Delete" CommandName='<%# Container.DataItemIndex + 1 %>' CommandArgument='<%#Eval("SRNO") %>'></asp:LinkButton>

                                                                    </td>--%>


                                                                    <td>
                                                                        <asp:ImageButton ID="btneditProgram" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("IDNO") %>'
                                                                            AlternateText="Delete Record" OnClick="btneditProgram_Click" OnClientClick="return confirm('Do you really want to delete this Program entry?');"
                                                                            TabIndex="14" ToolTip='<%# Eval("DM_NO") %>' />
                                                                        <asp:HiddenField ID="hdfDmNo" runat="server" Value='<%#Eval("DM_NO") %>' />


                                                                    </td>

                                                                    <td><%# Eval("DEGREENAME") %> - <%# Eval("LONGNAME") %></td>
                                                                    <%--<td>
                                                                        <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("LONGNAME") %>'></asp:Label></td>--%>
                                                                    <td><%# Eval("DEMAND_DATE") %></td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div id="diApplicantDetails" role="tabpanel" runat="server" aria-labelledby="IntakeTransfer-tab" visible="false">
                            <div>
                                <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="updApplicantDetails"
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
                            <asp:UpdatePanel ID="updApplicantDetails" runat="server">
                                <ContentTemplate>
                                    <div id="div5" runat="server">
                                        <div class="box-body">
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="col-12">
                                                        <div class="sub-heading">
                                                            <h5>Personal Details</h5>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <asp:Label ID="lblfirstname" Font-Bold="true" runat="server">First Name</asp:Label>
                                                            <%--    <label>First Name</label>--%>
                                                        </div>
                                                        <asp:TextBox ID="txtFirstName" runat="server" TabIndex="1" ToolTip="First Name "
                                                            onkeypress="return alphaOnly(event);" MaxLength="50" CssClass="form-control" />
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtFirstName"
                                                            InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" FilterMode="InvalidChars" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtFirstName"
                                                            Display="None" ErrorMessage="Please Enter First Name  " ValidationGroup="Submit"
                                                            SetFocusOnError="True" InitialValue=""></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="col-xl-3 col-md-4">

                                                        <sup>*</sup>
                                                        <%--   <label for="dsd"><sup>*</sup> Middle Name </label>--%>
                                                        <asp:Label ID="lblMiddleName" Font-Bold="true" runat="server">Middle Name</asp:Label>
                                                        <asp:TextBox ID="txtMiddleName" runat="server" Width="100%" ToolTip="Middle Name"
                                                            onkeypress="return alphaOnly(event);" MaxLength="20" CssClass="form-control" Placeholder="Please Enter Middle Name" TabIndex="2" />
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtMiddleName"
                                                            InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" FilterMode="InvalidChars" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtMiddleName"
                                                            Display="None" ErrorMessage="Please Enter Middle Name  " ValidationGroup="Submit"
                                                            SetFocusOnError="True" InitialValue=""></asp:RequiredFieldValidator>
                                                        <asp:HiddenField runat="server" ID="HiddenField1" />

                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <asp:Label ID="lblLastname" Font-Bold="true" runat="server">Last Name</asp:Label>
                                                            <%--               <label>Last Name</label>--%>
                                                        </div>
                                                        <asp:TextBox ID="txtPerlastname" runat="server" TabIndex="2" ToolTip="Last Name "
                                                            onkeypress="return alphaOnly(event);" MaxLength="50" CssClass="form-control" />
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txtPerlastname"
                                                            InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" FilterMode="InvalidChars" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtPerlastname"
                                                            Display="None" ErrorMessage="Please Enter Last Name  " ValidationGroup="Submit"
                                                            SetFocusOnError="True" InitialValue=""></asp:RequiredFieldValidator>
                                                    </div>

                                                    <%--  <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Name with Initials</label>
                                                    </div>
                                                    <asp:TextBox ID="txtNameInitial" runat="server" TabIndex="3" ToolTip="Name with Initials "
                                                        onkeypress="return alphaOnly(event);" MaxLength="50" CssClass="form-control" Enabled="true" placeholder="Example:- PERERA S.A" />
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="ajfFullName" runat="server" TargetControlID="txtNameInitial"
                                                        InvalidChars="~`!@#$%^*()_+=,/:;<>?'{}[]\|-&&quot;'" FilterMode="InvalidChars" />
                                                </div>--%>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <asp:Label ID="lblEmail" Font-Bold="true" runat="server">Email</asp:Label>
                                                            <%--   <label>Email</label>--%>
                                                        </div>
                                                        <asp:TextBox ID="txtEmail" runat="server" TabIndex="4" CssClass="form-control" Placeholder="Please Enter Email ID" Enabled="true" />
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtEmail"
                                                            Display="None" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                            ErrorMessage="Please Enter Valid Email_ID" ValidationGroup="login"></asp:RegularExpressionValidator>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Mobile No.</label>
                                                        </div>
                                                        <div class="input-group">
                                                            <span class="input-group-prepend" style="width: 130px! important;">
                                                                <asp:DropDownList ID="ddlOnlineMobileCode" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="4" onchange="return RemoveCountryName()">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="ddlOnlineMobileCode"
                                                                    Display="None" ErrorMessage="Please Select Mobile Code " ValidationGroup="Submit"
                                                                    SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                                            </span>
                                                            <asp:TextBox ID="txtMobile" runat="server" TabIndex="6"
                                                                MaxLength="12" ToolTip="Please Enter Mobile No." CssClass="form-control ml-2 pb-0 pl-0 pr-0" Placeholder="Please Enter Mobile No." />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtMobile"
                                                                ValidChars="1234567890" FilterMode="ValidChars" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtMobile"
                                                                Display="None" ErrorMessage="Please Enter Mobile No " ValidationGroup="Submit"
                                                                SetFocusOnError="True" InitialValue=""></asp:RequiredFieldValidator>

                                                        </div>
                                                    </div>

                                                    <div id="Div54" class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Home telephone No.</label>
                                                        </div>
                                                        <div class="input-group">
                                                            <span class="input-group-prepend" style="width: 130px! important;">
                                                                <asp:DropDownList ID="ddlHomeMobileCode" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="4" onchange="return RemoveHomeCountryName()">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </span>
                                                            <asp:TextBox ID="txtHomeTel" runat="server" TabIndex="8" CssClass="form-control ml-2 pb-0 pl-0 pr-0" Placeholder="Home telephone No." MaxLength="10" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="txtHomeTel"
                                                                ValidChars="1234567890" FilterMode="ValidChars" />
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>NIC (National Identity card)</label>
                                                        </div>
                                                        <asp:TextBox ID="txtOnlineNIC" runat="server" TabIndex="9" ToolTip="Please NIC "
                                                            MaxLength="30" CssClass="form-control" onblur="return Validator();" />
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtOnlineNIC"
                                                            InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" FilterMode="InvalidChars" />
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Passport No</label>
                                                        </div>
                                                        <asp:TextBox ID="txtPersonalPassprtNo" runat="server" TabIndex="10" ToolTip="Please Enter Passport No "
                                                            MaxLength="30" CssClass="form-control" onblur="return Validator();" />

                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" TargetControlID="txtPersonalPassprtNo"
                                                            InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" FilterMode="InvalidChars" />
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Date Of Birth (DD/MM/YY)</label>
                                                        </div>
                                                        <asp:TextBox ID="txtDateOfBirth" name="dob" runat="server" TabIndex="11" CssClass="form-control dob"
                                                            ToolTip="Please Enter Date Of Birth" placeholder="DD/MM/YYYY" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txtDateOfBirth"
                                                            Display="None" ErrorMessage="Please Enter Date Of Birth " ValidationGroup="Submit"
                                                            SetFocusOnError="True" InitialValue=""></asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Gender</label>
                                                        </div>
                                                        <asp:RadioButtonList ID="rdPersonalGender" runat="server" TabIndex="12" RepeatDirection="Horizontal">
                                                            <asp:ListItem Text="&nbsp;Male" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="&nbsp;Female" Value="1"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Citizen Type</label>
                                                        </div>
                                                        <asp:RadioButtonList ID="rdbQuestion" runat="server" TabIndex="18" RepeatDirection="Horizontal">
                                                            <asp:ListItem Text="&nbsp;Filipino" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="&nbsp;Foreign National" Value="2"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>

                                                    <div id="Div55" class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Left / Right Handed</label>
                                                        </div>
                                                        <asp:RadioButtonList ID="rdbLeftRight" runat="server" TabIndex="19" RepeatDirection="Horizontal">
                                                            <asp:ListItem Text="&nbsp;Left Handed" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="&nbsp;Right Handed" Value="2"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>
                                                                ACR No.</label>
                                                        </div>
                                                        <asp:TextBox ID="TxtACRNo" runat="server" Width="100%" TabIndex="13" ToolTip="ACR No." MaxLength="50" CssClass="form-control" />
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>
                                                                ACR Date Issue</label>
                                                        </div>

                                                        <asp:TextBox ID="txtACRDate" runat="server" name="dob1" TabIndex="14" CssClass="form-control dob1" Width="100%"
                                                            ToolTip="Please Enter ACR Date Issue" placeholder="DD/MM/YYYY" onkeypress="return onlyDotsAndNumbers(this,event);" />

                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divFather" runat="server">
                                                        <div class="md-form">
                                                            <label for="FatherName"><sup>*</sup> Father's Name </label>
                                                            <asp:TextBox ID="txtFatherName" runat="server" Width="100%" TabIndex="16" ToolTip="Please Enter Father's Name"
                                                                onkeypress="return alphaOnly(event);" MaxLength="20" CssClass="form-control" Placeholder="Please Enter Father's Name" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" TargetControlID="txtFatherName"
                                                                InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" FilterMode="InvalidChars" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtFatherName"
                                                                Display="None" ErrorMessage="Please Enter Father's Name " ValidationGroup="Submit"
                                                                SetFocusOnError="True" InitialValue=""></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divMother" runat="server">
                                                        <div class="md-form">
                                                            <sup>*</sup>
                                                            <asp:Label ID="lblmothername" Font-Bold="true" runat="server"> Mother's Name</asp:Label>

                                                            <asp:TextBox ID="txtMothersName" runat="server" Width="100%" TabIndex="18" ToolTip="Please Enter Mother's Name"
                                                                onkeypress="return alphaOnly(event);" MaxLength="20" CssClass="form-control" Placeholder="Please Enter Mother's Name" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftbemathername" runat="server" TargetControlID="txtMothersName"
                                                                InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" FilterMode="InvalidChars" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtMothersName"
                                                                Display="None" ErrorMessage="Please Enter Mother's Name" ValidationGroup="Submit"
                                                                SetFocusOnError="True" InitialValue=""></asp:RequiredFieldValidator>
                                                            <asp:HiddenField runat="server" ID="HiddenField2" />
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>

                                            <div class="col-12">
                                                <div class="row">

                                                    <div class="col-12">
                                                        <div class="sub-heading">
                                                            <h5>Postal Address</h5>
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Address (Max. Length 100)</label>
                                                        </div>
                                                        <asp:TextBox ID="txtPermAddress" runat="server" TabIndex="19" TextMode="MultiLine"
                                                            MaxLength="200" ToolTip="Please Enter Permenant Address" CssClass="form-control" onkeyup="return CountCharactersPerment();" />
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                          <asp:Label ID="lblDyCountry" Font-Bold="true" runat="server"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlPCon" runat="server" TabIndex="20" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlPCon_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>

                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <asp:Label ID="lblDyProvince" Font-Bold="true" runat="server"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlPermanentState" runat="server" Width="100%" TabIndex="21" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlPermanentState_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>

                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <asp:Label ID="lblCityVillage" Font-Bold="true" runat="server"></asp:Label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlPTahsil" runat="server" TabIndex="22" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>

                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                    <div id="div2" runat="server">

                                        <div class="col-12" id="Div7" runat="server">
                                            <div class="row">
                                                <div class="col-12">
                                                    <div class="sub-heading">
                                                        <h5>Educational Details</h5>
                                                    </div>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="md-form" style="pointer-events: none;" id="divddlprogrma" runat="server">
                                                        <label><sup></sup>Study Level</label>
                                                        <asp:DropDownList ID="ddlProgramTypes" runat="server" AppendDataBoundItems="true" TabIndex="1" Enabled="false"
                                                            Width="100%" CssClass="form-control select2 select-clik" AutoPostBack="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-12">
                                            <div class="table table-responsive">
                                                <asp:Panel ID="Panel8" runat="server">
                                                    <asp:ListView ID="lvLevellist" runat="server">
                                                        <LayoutTemplate>
                                                            <table class="table table-bordered nowrap" style="width: 100%" id="divdepartmentlist">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th> <asp:Label ID="lblDyLevel" runat="server"></asp:Label></th>
                                                                        <th><asp:Label ID="lblDYCollege" runat="server"> Name of School</asp:Label></th>
                                                                        <th><asp:Label ID="lblDYClgAddress" runat="server"></asp:Label></th>
                                                                        <th><asp:Label ID="lblDyRegion" runat="server"></asp:Label></th>
                                                                        <th><asp:Label ID="lblDyYearAttended" runat="server"></asp:Label></th>
                                                                        <th><asp:Label ID="lblDYClgType" runat="server"></asp:Label></th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>

                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td><%# Eval("UA_SECTIONNAME") %>
                                                                    <asp:Label runat="server" ID="lblSectionNo" Text='<%#Eval("UA_SECTION") %>' Visible="false"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtNameOfSchool" runat="server" Text='<%#Eval("SCHOOL_NAME") %>' TabIndex="1" CssClass="form-control" MaxLength="50" />
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtAddress" runat="server" Text='<%#Eval("SCHOOL_ADDRESS") %>' TabIndex="2" onblur="checkLength(this)" CssClass="md-textarea form-control" TextMode="MultiLine" Rows="1" MaxLength="100" />
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtRegion" Text='<%#Eval("SCHOOL_REGION") %>' runat="server" TabIndex="3" CssClass="form-control" MaxLength="20" />
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtYearAttended" Text='<%#Eval("YEAR_ATTENDED") %>' MaxLength="4" onblur="return IsNumeric(this);" runat="server" TabIndex="4" CssClass="form-control" />

                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlType" runat="server" TabIndex="1" CssClass="form-control">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        <asp:ListItem Value="1">Public </asp:ListItem>
                                                                        <asp:ListItem Value="2">Private</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:Label runat="server" ID="lblTypeNo" Text='<%#Eval("SCHOOL_TYPE_NO") %>' Visible="false"></asp:Label>
                                                                    <%--   <asp:TextBox ID="txtType" runat="server" TabIndex="1" CssClass="form-control" />--%>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnPersonalSubmit" runat="server" TabIndex="43" Text="Save & Next" CssClass="btn btn-outline-info" ValidationGroup="Submit" OnClick="btnPersonalSubmit_Click" />
                                        <asp:Button runat="server" Text="Cancel" ID="Button1" OnClick="btnCanceld_Click" CssClass="btn btn-outline-danger" />
                                        <asp:ValidationSummary ID="ValidationSummary5" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnPersonalSubmit" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <div id="dipayment" role="tabpanel" runat="server" aria-labelledby="IntakeTransfer-tab" visible="false">
                            <div>
                                <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updBulkReg"
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
                            <asp:UpdatePanel ID="updBulkReg" runat="server">
                                <ContentTemplate>
                                    <div id="divpayment" runat="server">
                                        <div class="box-body">
                                            <div class="col-12">
                                                <%--<div class="form-group col-lg-6 col-md-6 col-12" id="divShowPayment" runat="server">
                                                    <asp:Button ID="showOnlinePaymentDetails" runat="server" Text="Pay Now" class="btn btn-outline-info" OnClick="showOnlinePaymentDetails_Click" />
                                                </div>--%>
                                            </div>
                                            <div id="showpay" runat="server" visible="false">
                                                <div class="col-12 btn-footer">
                                                    <asp:Label ID="lblpaid" runat="server" Style="font-weight: bold; color: red;"></asp:Label>
                                                </div>
                                            </div>
                                            <div id="showunpay" runat="server" visible="false">
                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="showOnlinePaymentDetails" runat="server" Text="View Details" class="btn btn-outline-info" OnClick="showOnlinePaymentDetails_Click" />
                                                </div>
                                            </div>

                                            <div class="col-12" id="divViewpayment" runat="server" visible="false">
                                                <asp:UpdatePanel ID="up" runat="server">
                                                    <ContentTemplate>
                                                        <div class="row">
                                                            <div class="col-lg-6 col-12">
                                                                <asp:Panel ID="pnlStudentsFees" runat="server" Visible="false">
                                                                    <asp:ListView ID="lvStudentFees" runat="server" OnItemDataBound="lvStudentFees_ItemDataBound" OnPreRender="lvStudentFees_PreRender">
                                                                        <LayoutTemplate>
                                                                            <div>
                                                                                <div class="sub-heading">
                                                                                    <h5>Fees Details</h5>
                                                                                </div>
                                                                                <div class="table-responsive">
                                                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tblHead">
                                                                                        <thead class="bg-light-blue">
                                                                                            <tr id="trRow">
                                                                                                <%--<th id="thHead" style="width: 5%"></th>--%>
                                                                                                <th>SrNo.
                                                                                                </th>
                                                                                                <th>Fees Head
                                                                                                </th>
                                                                                                <th>Amount
                                                                                                </th>

                                                                                            </tr>

                                                                                        </thead>
                                                                                        <tbody>
                                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                                        </tbody>
                                                                                        <%--<tfoot><asp:Label ID="lbltotal" runat="server"  Text="0"></asp:Label></tfoot>--%>
                                                                                        <thead class="bg-light-blue">
                                                                                            <tr id="Tr1" runat="server">
                                                                                                <th></th>
                                                                                                <th><span class="pull-right">Total Amount</span></th>

                                                                                                <th id="Td1" runat="server">
                                                                                                    <asp:Label ID="lbltotal" CssClass="data_label" runat="server" Text="0"></asp:Label>
                                                                                                </th>
                                                                                            </tr>
                                                                                        </thead>
                                                                                    </table>
                                                                                </div>
                                                                            </div>
                                                                        </LayoutTemplate>
                                                                        <ItemTemplate>
                                                                            <tr>
                                                                                <%--<td style="width: 10%"></td>--%>
                                                                                <td><%# Eval("SRNO") %>
                                                                                </td>
                                                                                <td><%# Eval("FEE_LONGNAME") %></td>
                                                                                <td>
                                                                                    <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("AMOUNT") %>'></asp:Label></td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:ListView>

                                                                </asp:Panel>
                                                            </div>
                                                            <div class="col-lg-3 col-sm-6 col-12 pl-md-2 pr-md-2">
                                                                <div id="divOptionButtons" runat="server">
                                                                    <div class="col-12 std-info" id="divoflinepay" runat="server">
                                                                        <div class="sub-heading pt-3">
                                                                            <h5>Pay In Bank</h5>
                                                                        </div>

                                                                        <ul class="list-group list-group-unbordered mt-2">
                                                                            <li class="list-group-item"><b>Reg. Fees :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblFinalRegistrationFees" Font-Bold="true" runat="server" /></a>
                                                                            </li>
                                                                        </ul>
                                                                        <div class="row pb-3 pb-md-4 mt-2 mt-md-4 text-center">
                                                                            <div class="col-12" id="div3" runat="server">
                                                                                <asp:Button ID="btnGenerateChallan" runat="server" TabIndex="6" ValidationGroup="sub" Text="View Bank Details" CssClass="btn btn-outline-info" OnClick="btnGenerateChallan_Click" />
                                                                            </div>
                                                                            <div class="col-12 mt-2" id="div4" runat="server">
                                                                                <span id="myBtnDeposit" style="cursor: pointer" class="btn btn-outline-info" data-toggle="modal" data-target="#myModalChallan">Upload Deposit Slip</span>

                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-lg-3 col-sm-6 col-12 pl-md-2 pr-md-2">
                                                                <div id="div8" runat="server">
                                                                    <div class="col-12 std-info">
                                                                        <div class="sub-heading pt-3">
                                                                            <h5>Pay In Cash</h5>
                                                                        </div>
                                                                        <ul class="list-group list-group-unbordered mt-2">
                                                                            <li class="list-group-item"><b>Reg. Fees :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblOnlineRegFees" Font-Bold="true" runat="server" /></a>
                                                                            </li>

                                                                            <li class="list-group-item d-none"><b>Convenience Fee :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblOnlineConvenienceFees" Font-Bold="true" runat="server" /></a>
                                                                            </li>

                                                                            <li class="list-group-item d-none"><b>Total Payable :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblOnlineTotalPayable" Font-Bold="true" runat="server" /></a>
                                                                            </li>
                                                                        </ul>
                                                                        <div class="pb-2 mt-2 text-center">
                                                                            <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-outline-info" Font-Bold="True" OnClick="btnSubmit_Click" Text="Pay" ValidationGroup="submit" Visible="false" />
                                                                            <asp:Button ID="btnReport" runat="server" CssClass="btn btn-outline-primary" Enabled="false" Font-Bold="True" OnClick="btnReport_Click" Text="Print Challan" ValidationGroup="submit" Visible="false" />
                                                                            <asp:Button ID="btnCancel" runat="server" CausesValidation="False" CssClass="btn btn-outline-danger d-none" Font-Bold="True" OnClick="btnCancel_Click" Text="Cancel" Visible="false" />
                                                                            <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="submit" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-12" id="TRNote" runat="server" visible="false" style="display: none">

                                                            <blockquote><span class="text-danger">**Note: If Installment or Fees Not Matched,Contact To Accounts department!</span></blockquote>

                                                        </div>
                                                        <div class="col-md-12" runat="server" id="TRAmount" visible="false">
                                                            <li class="list-group-item">
                                                                <b>Total Amount :</b>
                                                                <a class="pull-right">
                                                                    <asp:Label ID="lblOrderID" runat="server" CssClass="data_label"></asp:Label>
                                                                    <asp:HiddenField ID="hdfAmount" runat="server" Value="0" />
                                                                    <asp:HiddenField ID="hdfServiceCharge" runat="server" Value="0" />
                                                                    <asp:HiddenField ID="hdfTotalAmount" runat="server" Value="0" />
                                                                </a>
                                                            </li>
                                                        </div>
                                                        <div class="col-md-12">
                                                            <asp:Label ID="lblStatus" runat="server" CssClass="data_label"></asp:Label>
                                                        </div>

                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>

                                            <div class="col-sm-12 col-xs-12 form-group d-flex" id="divOfflineNote" runat="server" visible="false">
                                                <label style="color: green; font-size: 20px">
                                                    Dear Candidate, We have received your Application and your Payment Request is submitted to the USA Finance Department for the Approval.
                                         Once it is approved, you will be notified with an Email.
                                                </label>
                                            </div>
                                            <div class="col-sm-12 col-xs-12 form-group d-flex" id="divOfflinePaymentdone" runat="server" visible="false">
                                                <label style="color: green; font-size: 20px">
                                                    <%-- Dear Candidate, We have received your Application payment & your Application submitted successfully.--%>
                                            Dear Candidate, Your payment is verified.
                                                </label>
                                            </div>

                                            <div class="col-12">
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                    <asp:Repeater ID="rpInstallment" runat="server">
                                                        <HeaderTemplate>
                                                            <div class="sub-heading">
                                                                <h5>Installment Details</h5>
                                                            </div>
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>Installment No.</th>
                                                                    <th>Amount</th>
                                                                    <th>DueDate</th>
                                                                    <th>ExtraCharge</th>
                                                                    <th>Total Paid</th>
                                                                    <th>Opration</th>
                                                                    <th>Remark</th>
                                                                    <%--<th>Status</th>--%>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <%--<tr id="itemPlaceholder" runat="server" />--%>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr class="item">
                                                                <td>
                                                                    <%# Container.ItemIndex + 1 %>
                                                                    <asp:Label ID="lblInstallmentno" runat="server" Text='<%# Eval("INSTALL_NO") %>' Visible="false"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("INSTALL_AMOUNT") %>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("DATE") %>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("ADDITIONAL_CHARGE_AMOUNT") %>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("TOTAL_AMT") %>
                                                                </td>
                                                                <td>
                                                                    <asp:LinkButton ID="lnkPay" runat="server" Visible="false" CommandName='<%# Eval("TOTAL_AMT") %>' CommandArgument='<%# Eval("INSTALL_NO") %>' CssClass="btn btn-outline-info" Text="Pay" Enabled='<%# Convert.ToInt32(Eval("DATE_STATUS")) == 1 ? false : true %>' ToolTip='<%# Eval("REMARK") %>' OnClick="lnkPay_Click"></asp:LinkButton>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("REMARK") %>'></asp:Label>
                                                                </td>
                                                                <%-- <td>
                                                                    <%# Eval("INSTALL_STATUS") %>
                                                                </td>--%>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            </tbody>
                                                        </FooterTemplate>
                                                    </asp:Repeater>
                                                </table>

                                            </div>
                                            <div class="col-12">
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                    <asp:Repeater ID="lvPaidReceipts" runat="server" OnItemDataBound="lvPaidReceipts_ItemDataBound">
                                                        <HeaderTemplate>
                                                            <div class="sub-heading">
                                                                <h5>Previous Receipts Information</h5>
                                                            </div>
                                                            <thead class="bg-light-blue">
                                                                <tr>

                                                                    <th>Receipt Type
                                                                    </th>
                                                                    <th>Receipt No
                                                                    </th>
                                                                    <th>Date
                                                                    </th>
                                                                    <th>Semester
                                                                    </th>
                                                                    <th>Pay Type
                                                                    </th>
                                                                    <th>Amount
                                                                    </th>
                                                                    <th>Payment Status
                                                                    </th>
                                                                    <th>Print
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <%--<tr id="itemPlaceholder" runat="server" />--%>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr class="item">

                                                                <td>
                                                                    <%# Eval("RECIEPT_TITLE") %>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("REC_NO") %>
                                                                </td>
                                                                <td>
                                                                    <%# (Eval("REC_DT").ToString() != string.Empty) ? ((DateTime)Eval("REC_DT")).ToShortDateString() : Eval("REC_DT") %>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("SEMESTERNAME") %>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("PAY_TYPE") %>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("TOTAL_AMT") %>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("PAY_STATUS") %>
                                                                </td>
                                                                <td>
                                                                    <asp:ImageButton ID="btnPrintReceipt" runat="server" OnClick="btnPrintReceipt_Click" Enabled='<%# (Convert.ToInt32(Eval("RECON")) == 0 ? false : true) %>'
                                                                        CommandArgument='<%# Eval("DCR_NO") %>' ImageUrl="~/images/print.gif" ToolTip='<%# Eval("RECIEPT_CODE")%>' CausesValidation="False" />
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            </tbody>
                                                        </FooterTemplate>
                                                    </asp:Repeater>
                                                </table>

                                            </div>
                                        </div>

                                    </div>
                                    <div id="divMinority" runat="server" visible="false">
                                        <label style="color: green; font-size: 20px">
                                            <%-- Dear Candidate, We have received your Application payment & your Application submitted successfully.--%>
                                            <%--   Dear Candidate,
                                                    Your application for the registration has been successfully accepted and you must follow the steps given below to settle the current semester fee. Kindly note that student only those who follow ALL the given steps given below will be accepted as registered students    
                                                    <br />
                                                    1.   Sign the upcoming semester consent list. This the document that is sent to bank and MOHE to process the semester payment<br />
                                                    2. Sign the loan receipt by either visiting the bank or officer will be made available in USA on a said date (instructions on how and when will be made available in the Course Web)
                                                    <br />
                                                    *** If there is a late fee then you have to pay ***--%>

                                              Dear Candidate,
                                            <br>
                                            Your application for the registration was successfully submitted. We Look forward to onboarding you with us.
                                        </label>
                                    </div>

                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnSubmit" />
                                    <%--  <asp:PostBackTrigger ControlID="btnSubmit" />--%>
                                    <asp:PostBackTrigger ControlID="btnReport" />
                                    <asp:PostBackTrigger ControlID="btnCancel" />
                                    <asp:PostBackTrigger ControlID="btnShowDetails" />
                                </Triggers>

                            </asp:UpdatePanel>
                        </div>

                        <div id="divuploaddoc" runat="server" visible="false" role="tabpanel" aria-labelledby="IntakeAllotment-tab">
                            <div id="document" runat="server" visible="false">

                                <div class="mt-2" id="updDocs" runat="server">

                                    <div class="col-12" id="divAllCoursesFromHist" runat="server">
                                        <asp:Panel ID="Panel1" runat="server">
                                            <asp:ListView ID="lvDocument" runat="server">
                                                <LayoutTemplate>
                                                    <div>
                                                        <div class="sub-heading">
                                                            <%--      <h6 style="color: red; font-size: small">* Please be ready with a scanned copy of the documents listed below. Select “Choose File” to upload a scanned copy of the relevant document. </h6>
                                                            <h6 style="color: red; font-size: small">* Download the Terms and Conditions and Indemnity Form by clicking “Download” button. Fill the details of the documents and upload. </h6>
                                                            <h6 style="color: red; font-size: small">* Press “Submit” and “Next” after successful document upload.  </h6>--%>

                                                            <h6 style="color: red; font-size: small">* Please be ready with Scanned Copies of the documents listed below. They should be in PDF format. Select “Choose File” to upload the scanned PDF copy of the relevant document. </h6>
                                                            <h6 style="color: red; font-size: small">* Student Photo should be uploaded in png, jpg, or jpeg formats </h6>
                                                            <h6 style="color: red; font-size: small">* Download the Terms and Conditions and the Indemnity Form by clicking the “Download” button. Fill all the details of the documents and upload.  </h6>
                                                            <h6 style="color: red; font-size: small">* After choosing all the files, Press the “Submit” button to successfully upload them. Press the “Next” button to go into the next Step.  </h6>

                                                        </div>

                                                        <div class="sub-heading">
                                                            <h5>Document List</h5>
                                                        </div>
                                                        <div class="table-responsive">
                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%" id="table1">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th><asp:Label ID="lblSr_no" runat="server"></asp:Label></th>
                                                                        <th><asp:Label ID="lblDYDocument" runat="server">Document Name</asp:Label>
                                                                        </th>
                                                                        <th>Mandatory</th>
                                                                        <th><asp:Label ID="lblDyChooseFile" runat="server"></asp:Label>
                                                                        </th>
                                                                        <th><asp:Label ID="lblDocUpload" runat="server"></asp:Label>
                                                                        </th>
                                                                        <th>View
                                                                        </th>
                                                                        <th><asp:Label ID="lblDyDefaultTemplate" runat="server"></asp:Label>
                                                                        </th>
                                                                        <th><asp:Label ID="lblDyUploadDate" runat="server"></asp:Label>
                                                                        </th>
                                                                        <th><asp:Label ID="lblDyVerifyStatus" runat="server"></asp:Label>
                                                                        </th>
                                                                        <th>Verify Remark
                                                                        </th>
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
                                                    <tr id="trCurRow">
                                                        <td>
                                                            <%#Container.DataItemIndex + 1 %>
                                                            <asp:Label ID="lblDocNo" runat="server" Text='<%#Eval("DOCUMENTNO") %>' Visible="false"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblname" runat="server" Text='<%#Eval("DOCUMENTNAME") %>' /><br />
                                                            <asp:Label ID="lblImageFile" runat="server" Style="color: red"></asp:Label>
                                                            <asp:Label ID="lblFileFormat" runat="server" Style="color: red"></asp:Label>
                                                            <asp:Label ID="lblmandatory" runat="server" Visible="false" Text='<%#Eval("MANDATORY") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <%#Eval("MANDATORY") %>
                                                        </td>
                                                        <td>
                                                            <asp:FileUpload ID="fuDocument" runat="server" onchange="setUploadButtondoc(this)" TabIndex='<%#Container.DataItemIndex + 1 %>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lbluploadpic" runat="server" Visible="false"></asp:Label>
                                                            <asp:Label ID="lbluploadpdf" runat="server" Visible="false"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:LinkButton ID="lnkViewDoc" runat="server" CssClass="btn btn-outline-info" CommandArgument='<%#Eval("DOCUMENTNO") %>' CommandName='<%#Eval("IDNO") %>' OnClick="lnkViewDoc_Click" Visible="false"><i class="fa fa-eye"></i> View</asp:LinkButton>
                                                        </td>
                                                        <td>
                                                            <asp:LinkButton ID="lnkDocMappingDegree" runat="server" CssClass="btn btn-outline-info" CommandArgument='<%#Eval("DOCUMENTNO") %>' CommandName='<%#Eval("DEGREE_DOC") %>' OnClick="lnkDocMappingDegree_Click" Visible='<%#Eval("DEGREE_DOC").ToString() == string.Empty ? false : true %>'><i class="fa fa-eye"></i> Download</asp:LinkButton>
                                                        </td>
                                                        <td>

                                                            <asp:Label ID="lblUploadDate" runat="server" />
                                                        </td>
                                                        <td>

                                                            <asp:Label ID="lblVerifyDocument" runat="server" />
                                                        </td>
                                                        <td>

                                                            <asp:Label ID="lblRemark" runat="server" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>

                                        </asp:Panel>
                                    </div>
                                    <div class="col-12">
                                        <asp:Label ID="lblfile" runat="server" Style="color: red"></asp:Label>
                                    </div>

                                    <div class="col-12 btn-footer">

                                        <asp:Button ID="btnSub" runat="Server" Text="SUBMIT" OnClick="btnSub_Click" ValidationGroup="degreeSub"
                                            CssClass="btn btn-outline-info" />
                                        <asp:Button ID="btnNextDoc" runat="server" Text="Next" CssClass="btn btn-outline-info" OnClick="btnNextDoc_Click" />
                                        <%--   <asp:LinkButton runat="server" ID="lnkCancelCourse" Font-Bold="true" Text="Cancel Course"
                                OnClick="lnkCancelCourse_Click" ForeColor="Blue"></asp:LinkButton>--%>
                                        <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                            ShowSummary="false" ValidationGroup="degreeSub" />
                                    </div>
                                </div>
                            </div>

                        </div>

                        <div id="divModuleRegistration" role="tabpanel" class="d-none" runat="server" aria-labelledby="IntakeTransfer-tab" visible="false">
                            <div>
                                <asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="updModuleRegistration"
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
                            <asp:UpdatePanel ID="updModuleRegistration" runat="server">
                                <ContentTemplate>
                                    <div id="div6" runat="server">
                                        <div class="box-body">
                                            <div class="col-12">

                                                <asp:Panel ID="Panel2" runat="server" Visible="false">
                                                    <asp:ListView ID="lvOfferedSubject" runat="server">
                                                        <LayoutTemplate>
                                                            <div>
                                                                <div class="sub-heading">
                                                                    <h5>Regular Subject</h5>
                                                                </div>
                                                                <div class="table-responsive">
                                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                                        <thead class="bg-light-blue">
                                                                            <tr id="trRow">
                                                                                <%--<th id="thHead" style="width: 5%"></th>--%>
                                                                                <th></th>
                                                                                <th>Subject Code
                                                                                </th>
                                                                                <th>Subject Name
                                                                                </th>
                                                                                <th>Subject Type
                                                                                </th>
                                                                                <th>Credits
                                                                                </th>
                                                                                <th>LIC Name
                                                                                </th>
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
                                                                    <asp:CheckBox ID="chkRows" runat="server" Checked='<%#Eval("EXAM_REGISTERED").ToString() == "1" ? true : false %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>'></asp:Label>
                                                                    <asp:HiddenField ID="hdfCourseNo" runat="server" Value='<%# Eval("COURSENO") %>' />
                                                                    <asp:HiddenField ID="hdfSubid" runat="server" Value='<%# Eval("SUBID") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>'></asp:Label></td>
                                                                <td><%# Eval("SUBNAME") %></td>
                                                                <td>
                                                                    <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblLicName" runat="server" Text='<%# Eval("UA_FULLNAME") %>'></asp:Label>
                                                                    <asp:Label ID="lbluano" runat="server" Text='<%# Eval("MODULELIC") %>' Visible="false"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>

                                                </asp:Panel>

                                                <asp:Panel ID="Panel3" runat="server" Visible="false">
                                                    <asp:ListView ID="lvcoursetwo" runat="server">
                                                        <LayoutTemplate>
                                                            <div>
                                                                <div class="sub-heading">
                                                                    <h5>Orientation Subject</h5>
                                                                </div>
                                                                <div class="table-responsive">
                                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                                        <thead class="bg-light-blue">
                                                                            <tr id="trRow">
                                                                                <%--<th id="thHead" style="width: 5%"></th>--%>
                                                                                <th></th>
                                                                                <th>Subject Code
                                                                                </th>
                                                                                <th>Subject Name
                                                                                </th>
                                                                                <th>Subject Type
                                                                                </th>
                                                                                <th>Credits
                                                                                </th>
                                                                                <th>LIC Name
                                                                                </th>
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
                                                                    <asp:CheckBox ID="chkRows" runat="server" Checked="true" Enabled="false" />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>'></asp:Label>
                                                                    <asp:HiddenField ID="hdfCourseNo" runat="server" Value='<%# Eval("COURSENO") %>' />
                                                                    <asp:HiddenField ID="hdfSubid" runat="server" Value='<%# Eval("SUBID") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>'></asp:Label></td>
                                                                <td><%# Eval("SUBNAME") %></td>
                                                                <td>
                                                                    <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblLicName" runat="server" Text='<%# Eval("UA_FULLNAME") %>'></asp:Label>
                                                                    <asp:Label ID="lbluano" runat="server" Text='<%# Eval("MODULELIC") %>' Visible="false"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>

                                                </asp:Panel>
                                                <asp:Panel ID="Panel4" runat="server" Visible="false">
                                                    <asp:ListView ID="lvcoursethree" runat="server">
                                                        <LayoutTemplate>
                                                            <div>
                                                                <div class="sub-heading">
                                                                    <h5>Bridging Subject</h5>
                                                                </div>
                                                                <div class="table-responsive">
                                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                                        <thead class="bg-light-blue">
                                                                            <tr id="trRow">
                                                                                <%--<th id="thHead" style="width: 5%"></th>--%>
                                                                                <th></th>
                                                                                <th>Subject Code
                                                                                </th>
                                                                                <th>Subject Name
                                                                                </th>
                                                                                <th>Subject Type
                                                                                </th>
                                                                                <th>Credits
                                                                                </th>
                                                                                <th>LIC Name
                                                                                </th>
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
                                                                    <asp:CheckBox ID="chkRows" runat="server" Checked='<%#Eval("EXAM_REGISTERED").ToString() == "1" ? true : false %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>'></asp:Label>
                                                                    <asp:HiddenField ID="hdfCourseNo" runat="server" Value='<%# Eval("COURSENO") %>' />
                                                                    <asp:HiddenField ID="hdfSubid" runat="server" Value='<%# Eval("SUBID") %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>'></asp:Label></td>
                                                                <td><%# Eval("SUBNAME") %></td>
                                                                <td>
                                                                    <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblLicName" runat="server" Text='<%# Eval("UA_FULLNAME") %>'></asp:Label>
                                                                    <asp:Label ID="lbluano" runat="server" Text='<%# Eval("MODULELIC") %>' Visible="false"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>

                                                </asp:Panel>
                                            </div>


                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="btnSubmitOffer" runat="server" Text="Submit & Next" CssClass="btn btn-outline-info" OnClick="btnSubmitOffer_Click" />
                                            </div>
                                        </div>

                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnSubmitOffer" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>

                        <div id="divstatus" runat="server" visible="false" role="tabpanel" aria-labelledby="HallTicket-tab">
                            <div>
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updBulkReg"
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

                            <%-- <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                <ContentTemplate>
                                    <div id="div1" runat="server">
                                        <div class="box-body">
                                            <div class="col-12 mb-3">
                                                <div class="row">
                                                    <div class="col-lg-3 col-md-6 col-12">
                                                        <div id="divlbsta" runat="server">
                                                            <ul class="list-group list-group-unbordered">
                                                                <li class="list-group-item"><b>Status :</b>
                                                                    <a class="sub-label">
                                                                        <asp:Label ID="lblstatuspayment" Font-Bold="true" runat="server" />
                                                                    </a>
                                                                </li>
                                                            </ul>
                                                        </div>
                                                    </div>
                                                </div>

                                                 <%--add by aashna
                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="rptstatus" runat="server" Text="Report" CssClass="btn btn-outline-info" OnClick="rptstatus_Click" />
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="rptstatus" />
                                </Triggers>
                            </asp:UpdatePanel>--%>
                            <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                <ContentTemplate>
                                    <div id="div1" runat="server">
                                        <div class="box-body">
                                            <div class="col-12 mb-3" id="divlbsta" runat="server">
                                                <div class="row">

                                                    <div class="col-lg-4 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Status :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblstatuspayment" Font-Bold="true" runat="server" />
                                                                </a>
                                                            </li>

                                                            <li class="list-group-item"><b>Intake :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblIntake" Font-Bold="true" runat="server" />
                                                                </a>
                                                            </li>

                                                            <li class="list-group-item"><b>Student Registration No:</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblEnrollmentn" Font-Bold="true" runat="server" />
                                                                </a>
                                                            </li>

                                                            <li class="list-group-item" style="display: none"><b>Name with Initial:</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblNamewithInitial" Font-Bold="true" runat="server" />
                                                                </a>
                                                            </li>

                                                            <li class="list-group-item"><b>Name in Full:</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblFullName" Font-Bold="true" runat="server" />
                                                                </a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                    <div class="col-lg-4 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>NIC / Passport:</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblnicpass" Font-Bold="true" runat="server" />
                                                                </a>
                                                            </li>
                                                            <li class="list-group-item"><b>Address:</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lbladdres" Font-Bold="true" runat="server" />
                                                                </a>
                                                            </li>

                                                            <li class="list-group-item"><b>Contact No.:</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblcontactn" Font-Bold="true" runat="server" />
                                                                </a>
                                                            </li>
                                                            <li class="list-group-item"><b>Email:</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblstudemail" Font-Bold="true" runat="server" />
                                                                </a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                    <div class="col-lg-4 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <%--<li class="list-group-item"><b>USA Email:</b>
                                                                    <a class="sub-label">
                                                                        <asp:Label ID="lblUSAemail" Font-Bold="true" runat="server" />
                                                                    </a>
                                                                </li>--%>

                                                            <li class="list-group-item"><b>Programme:</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblprograms" Font-Bold="true" runat="server" />
                                                                </a>
                                                            </li>
                                                            <li class="list-group-item"><b>Campus:</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblcampus" Font-Bold="true" runat="server" />
                                                                </a>
                                                            </li>
                                                            <li class="list-group-item"><b>Batch:</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblweekbatch" Font-Bold="true" runat="server" />
                                                                </a>
                                                            </li>
                                                            <li class="list-group-item"><b>Date Of Registration:</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lbldateofreg" Font-Bold="true" runat="server" />
                                                                </a>
                                                            </li>
                                                            <%-- <li class="list-group-item"><b>Orientation Group:</b>
                                                                    <a class="sub-label">
                                                                        <asp:Label ID="lblorigp" Font-Bold="true" runat="server" />
                                                                    </a>
                                                                </li>--%>
                                                        </ul>
                                                    </div>
                                                </div>
                                                <%--add by aashna--%>
                                                <div class="col-12 btn-footer mt-3">
                                                    <asp:Button ID="btnSummarySheet" runat="server" Text="Certificate of Admission" CssClass="btn btn-outline-info" OnClick="btnSummarySheet_Click" Visible="false" />
                                                    <asp:Button ID="rptstatus" runat="server" Text="Report" CssClass="btn btn-outline-info d-none" OnClick="rptstatus_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="rptstatus" />
                                    <asp:PostBackTrigger ControlID="btnSummarySheet" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="myModal22" class="modal fade" role="dialog" data-backdrop="static">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">

                <div class="modal-header">
                    <asp:UpdatePanel ID="updClose" runat="server">
                        <ContentTemplate>
                            <asp:LinkButton ID="lnkClose" runat="server" CssClass="close" Style="margin-top: -18px" OnClick="lnkClose_Click">x</asp:LinkButton>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="lnkClose" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <%--<button type="button" class="close" data-dismiss="modal" style="margin-top: -18px">x</button>--%>
                </div>
                <div class="modal-body text-center">
                    <asp:Image ID="ImageViewer" runat="server" Visible="false" />
                    <asp:Literal ID="ltEmbed" runat="server" Visible="false" />
                    <div id="imageViewerContainer" runat="server" visible="false"></div>
                    <asp:HiddenField ID="hdfImagePath" runat="server" />
                    <iframe style="width: 100%; height: 500px;" id="irm1" src="~/PopUp.aspx" runat="server"></iframe>
                    <%--  <iframe id="iframe1" runat="server" frameborder="0" width="100%" height="800px" visible="false"></iframe>--%>
                    <%--<iframe id="iframe1" runat="server" src="../FILEUPLOAD/Transcript Report.pdf" frameborder="0" width="100%" height="547px"></iframe>--%>
                </div>
            </div>
        </div>
    </div>
    <div id="myModalChallan" class="modal fade" role="dialog">
        <div class="modal-dialog modal-xl">
            <!-- Modal content-->


            <div class="modal-content">
                <div class="modal-header">
                    <h4>Upload Deposit</h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="updChallan" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="form-group col-md-3 col-12">
                                    <div class="label-dynamic">
                                        <label>Actual Amount To Be Paid</label>
                                    </div>
                                </div>
                                <div class="form-group col-md-3 col-12">
                                    <asp:TextBox ID="txtchallanAmount" TabIndex="4" runat="server" MaxLength="10" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Please Enter Amount"
                                        ControlToValidate="txtchallanAmount" Display="None" ValidationGroup="SubmitChallan"></asp:RequiredFieldValidator>
                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtchallanAmount" ValidChars="1234567890.-," FilterMode="ValidChars" />
                                </div>

                                <div class="form-group col-md-6 col-12">
                                    <p style="color: red; float: right;">
                                        <asp:Label ID="lblMsg" runat="server" Text="(Image Size Not Greater Than 1MB and image format JPG,JPEG,PNG & pdf Allowed)"></asp:Label>
                                    </p>
                                </div>
                                <div class="form-group col-xl-12 col-md-12" id="divTransactionid" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <label>Transaction Id</label>
                                    </div>
                                    <asp:TextBox ID="txtTransactionNo" TabIndex="1" runat="server" MaxLength="20" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please Enter Challan Transaction Id"
                                        ControlToValidate="txtTransactionNo" Display="None" ValidationGroup="SubmitChallan"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-xl-12 col-md-12" id="DivOrderId" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <label>Challan Order Id</label>
                                    </div>
                                    <label for="UserName"><sup></sup>ID </label>
                                    <asp:TextBox ID="txtChallanId" runat="server" MaxLength="20" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please Enter Challan Order ID"
                                        ControlToValidate="txtChallanId" Display="None" ValidationGroup="SubmitChallan"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-md-2 col-6">
                                    <div class="label-dynamic">
                                        <label>Bank</label>
                                    </div>
                                    <asp:DropDownList ID="ddlbank" runat="server" AppendDataBoundItems="true" CssClass="form-control select2 select-click" TabIndex="2">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Please Select Bank Name"
                                        ControlToValidate="ddlbank" InitialValue="0" Display="None" ValidationGroup="SubmitChallan"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-md-2 col-6">
                                    <div class="label-dynamic">
                                        <label>Branch</label>
                                    </div>
                                    <asp:TextBox ID="txtBranchName" runat="server" CssClass="form-control" TabIndex="3" MaxLength="50"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Please Enter Bank Branch"
                                        ControlToValidate="txtBranchName" Display="None" ValidationGroup="SubmitChallan"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-md-2 col-6">
                                    <div class="label-dynamic">
                                        <label>Deposit Amount</label>
                                    </div>
                                    <asp:TextBox ID="txtDepositAmount" TabIndex="5" runat="server" MaxLength="10" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Please Enter Deposit Amount"
                                        ControlToValidate="txtDepositAmount" Display="None" ValidationGroup="SubmitChallan"></asp:RequiredFieldValidator>
                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender29" runat="server" TargetControlID="txtDepositAmount" ValidChars="1234567890.," FilterMode="ValidChars" />
                                </div>

                                <div class="form-group col-md-2 col-6">
                                    <div class="label-dynamic">
                                        <label>Date of Payment</label>
                                    </div>
                                    <asp:TextBox ID="txtPaymentdate" TabIndex="6" runat="server" MaxLength="20" CssClass="form-control dob"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator36" runat="server" ErrorMessage="Please Enter Date of Payment"
                                        ControlToValidate="txtPaymentdate" Display="None" ValidationGroup="SubmitChallan"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-md-2 col-6">
                                    <div class="label-dynamic">
                                        <label>
                                            <%--   Upload Deposit Slip--%>
                                             Deposit Amount 1st attachment
                                        </label>
                                    </div>
                                    <asp:FileUpload ID="FuChallan" runat="server" onchange="setUploadButtonState();" Style="margin-top: 8px;" TabIndex="7" /><br />

                                </div>
                            </div>
                            <div class="d-none">
                                <h6 style="color: brown; font-size: 18px">***Please upload separate details, if you have paid the amount in multiple deposits with same bank or dffferent bank</h6>
                            </div>

                            <div class="col-12 mt-2 text-center">
                                <asp:Button ID="btnChallanSubmit" runat="server" TabIndex="8" Text="Submit" CssClass="btn btn-outline-info" ValidationGroup="SubmitChallan" OnClick="btnChallanSubmit_Click" />
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="SubmitChallan" />
                            </div>


                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-12 mb-3">
                                    <asp:ListView ID="lvDepositSlip" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Deposit Slip Detail</h5>
                                            </div>
                                            <div class="table table-responsive">
                                                <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <td>Action</td>
                                                            <td>Bank Name</td>
                                                            <td>Bank Branch</td>
                                                            <td>Amount</td>
                                                            <td>Date of Payment</td>
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
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/IMAGES/edit.png" CommandArgument='<%# Eval("TEMP_DCR_NO") %>'
                                                        AlternateText="Delete Record" OnClick="btnedit_Click"
                                                        TabIndex="14" ToolTip="Edit" />
                                                </td>
                                                <td><%# Eval("BANK_NAME") %></td>
                                                <td><%# Eval("BRANCH_NAME") %></td>
                                                <td><%# Eval("TOTAL_AMT") %></td>
                                                <td><%# Eval("REC_DT") %></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>

                                </div>
                            </div>

                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnChallanSubmit" />
                            <asp:AsyncPostBackTrigger ControlID="ddlbank" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    <div class="col-md-12">
        <div id="divMsg" runat="server">
        </div>
        <div id="myModal33" class="modal fade" role="dialog" data-backdrop="static">
            <div class="modal-dialog modal-lg">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4>Online Payment</h4>
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-xl-6 col-md-6">
                                <div class="md-form">
                                    <label for="UserName"><sup></sup>Order ID </label>
                                    <asp:TextBox ID="txtOrderid" TabIndex="1" runat="server" MaxLength="15" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                            </div>


                            <div class="col-xl-6 col-md-6">
                                <div class="md-form">
                                    <label for="UserName"><sup></sup>Total to be Paid</label>
                                    <asp:TextBox ID="txtTotalPayAmount" TabIndex="4" runat="server" MaxLength="15" CssClass="form-control" Enabled="false" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-xl-6 col-md-6">
                                <div class="md-form">
                                    <label for="UserName"><sup></sup>Service Charge</label>
                                    <asp:TextBox ID="txtServiceCharge" TabIndex="3" runat="server" MaxLength="15" CssClass="form-control" Enabled="false" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-xl-6 col-md-6">
                                <div class="md-form">
                                    <label for="UserName"><sup></sup>Amount to be Paid </label>
                                    <asp:TextBox ID="txtAmountPaid" TabIndex="2" runat="server" MaxLength="15" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                            </div>



                            <div class="col-12 btn-footer mt-3">
                                <input type="button" value="Pay with Lightbox" onclick="Checkout.showLightbox();" class="btn btn-outline-info d-none" />
                                <input type="button" value="Pay" onclick="Checkout.showPaymentPage();" class="btn btn-outline-info" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="modelBank" class="modal fade" role="dialog">
        <div class="modal-dialog modal-xl">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <h4>Bank Details</h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-12 mb-3">
                            <asp:ListView ID="lvBankDetails" runat="server">
                                <LayoutTemplate>
                                    <div class="sub-heading">
                                        <h5>Bank Details List</h5>
                                    </div>
                                    <div class="table table-responsive">
                                        <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <td>SrNo</td>
                                                    <td>Bank Code</td>
                                                    <td>Bank Name</td>
                                                    <td>Bank Branch</td>
                                                    <td>Bank Account No.</td>
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
                                        <td><%# Container.DataItemIndex + 1 %></td>
                                        <td><%# Eval("BANKCODE") %></td>
                                        <td><%# Eval("BANKNAME") %></td>
                                        <td><%# Eval("BANKADDR") %></td>
                                        <td><%# Eval("ACCOUNT_NO") %></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <style>
        .chiller-theme .nav-tabs.dynamic-nav-tabs > li.active {
            border-radius: 8px;
            background-color: transparent !important;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function CheckNumeric(e) {

            if (window.event) // IE 
            {
                if ((e.keyCode < 48 || e.keyCode > 57) & e.keyCode != 8) {
                    event.returnValue = false;
                    return false;

                }
            }
            else { // Fire Fox
                if ((e.which < 48 || e.which > 57) & e.which != 8) {
                    e.preventDefault();
                    return false;

                }
            }
        }

    </script>

    <script type="text/javascript">
        function MutExChkList(chk) {
            var chkList = chk.parentNode.parentNode.parentNode;
            var chks = chkList.getElementsByTagName("input");
            for (var i = 0; i < chks.length; i++) {
                if (chks[i] != chk && chk.checked) {
                    //  chks[i].checked = false;
                    chks[i].checked = true;
                }
            }
        }

        function setUploadButtonStatefEED() {

            var maxFileSize = 1000000;

            var fi = document.getElementById('ctl00_ContentPlaceHolder1_fuDocument');

            for (var i = 0; i <= fi.files.length - 1; i++) {

                var fsize = fi.files.item(i).size;

                if (fsize >= maxFileSize) {
                    alert("File Size should be less than 1 MB");
                    $("#ctl00_ContentPlaceHolder1_fuDocument").val("");

                }
            }

        }


    </script>

    <!-- file upload Start-->
    <script>
        $("#ctl00_ContentPlaceHolder1_photoupload").change(function () {
            //$('.fuCollegeLogo').on('change', function () {

            var maxFileSize = 1000000;
            var fi = document.getElementById('ctl00_ContentPlaceHolder1_photoupload');
            myfile = $(this).val();
            var ext = myfile.split('.').pop();
            var res = ext.toUpperCase();

            //alert(res)
            if (res != "JPG" && res != "JPEG" && res != "PNG") {
                alert("Please Select jpg,jpeg,JPG,JPEG,PNG File Only.");
                $('.logoContainer img').attr('src', "../IMAGES/default-fileupload.png");
                $(this).val('');
            }

            for (var i = 0; i <= fi.files.length - 1; i++) {
                var fsize = fi.files.item(i).size;
                if (fsize >= maxFileSize) {
                    alert("File Size should be less than 1 MB");
                    $('.logoContainer img').attr('src', "../IMAGES/default-fileupload.png");
                    $("#ctl00_ContentPlaceHolder1_photoupload").val("");

                }
            }

        });
    </script>
    <script>
        $("#ctl00_ContentPlaceHolder1_photoupload").change(function () {
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
                    $('.logoContainer img').attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
            }
        }
        $(".fileContainer input:file").change(function () {
            readURL(this);
        });
    </script>

    <script>
        $(document).ready(function () {
            $('.fuDocumentX').on('change', function () {
                myfile = $(this).val();
                var ext = myfile.split('.').pop();
                if (ext != "pdf") {
                    alert("Please Select PDF File Only.");
                    $(this).val('');
                }
            });
        });
    </script>
    <script>
        function RemoveCountryName() {

            $("#select2-ddlMobileCode-container").html($("#select2-ddlMobileCode-container").html().split('-')[0]);
            if ($("#ddlMobileCode").val().split('-')[0] != "212") {
                $("#txtMobileNo").val('');
            }
            else {
                $("#txtMobileNo").val('0');
            }
        }
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $("#ddlMobileCode").html($(".select2-selection__rendered").html().split('-')[0]);
                // alert('byee')
            });
        });
    </script>
    <script type="text/javascript">
        function Validator() {
            var pass = $('#txtPassport').val();
            var nic = $('#txtNIC').val();
            if (pass == '' && nic == '') {
                alert("Passport No. OR NIC(National Identity card) is Required !");
            }
        }
    </script>
    <script>
        function RemoveHomeCountryName() {

            $("#select2-ddlHomeTelMobileCode-container").html($("#select2-ddlHomeTelMobileCode-container").html().split('-')[0]);
            //if ($("#ddlHomeTelMobileCode").val().split('-')[0] != "212") {
            //    $("#txtHomeMobileNo").val('');
            //}
            //else {
            //    $("#txtHomeMobileNo").val('0');
            //}
        }
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $("#ddlHomeTelMobileCode").html($(".select2-selection__rendered").html().split('-')[0]);
                // alert('byee')
            });
        });
    </script>
    <script>
        $(document).ready(function () {
            //var dateval = document.getElementById('<%=txtDateOfBirth.ClientID%>').value;

            var prev_date = new Date();
            prev_date.setDate(prev_date.getDate());
            $('.dob').daterangepicker({
                singleDatePicker: true,
                showDropdowns: true,
                //minDate: '01/1/1975',
                maxDate: prev_date,
                locale: {
                    format: 'DD/MM/YYYY'
                },
                autoApply: true,
            });
            //if (dateval == "") {
            //    $('.dob').val('');
            //}
        });
    </script>
    <script>
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                //var dateval = document.getElementById('<%=txtDateOfBirth.ClientID%>').value;

                var prev_date = new Date();
                prev_date.setDate(prev_date.getDate());
                $('.dob').daterangepicker({
                    singleDatePicker: true,
                    showDropdowns: true,
                    //minDate: '01/1/1975',
                    maxDate: prev_date,
                    locale: {
                        format: 'DD/MM/YYYY'
                    },
                    autoApply: true,
                });
                //if (dateval == "") {
                //    $('.dob').val('');
                //}
            });
        });
    </script>
    <script type="text/javascript">

        function CountCharactersPerment() {
            var maxSize = 100;

            if (document.getElementById('<%= txtPermAddress.ClientID %>')) {
                var ctrl = document.getElementById('<%= txtPermAddress.ClientID %>');
                var len = document.getElementById('<%= txtPermAddress.ClientID %>').value.length;
                if (len <= maxSize) {
                    return;
                }
                else {
                    alert("Max Of length Should be only 100 Characters ");
                    ctrl.value = ctrl.value.substring(0, maxSize);

                }
            }

            return false;
        }
    </script>
    <script>
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            function CountCharactersPerment() {
                var maxSize = 100;

                if (document.getElementById('<%= txtPermAddress.ClientID %>')) {
                    var ctrl = document.getElementById('<%= txtPermAddress.ClientID %>');
                    var len = document.getElementById('<%= txtPermAddress.ClientID %>').value.length;
                    if (len <= maxSize) {
                        return;
                    }
                    else {
                        alert("Max Of length Should be only 100 Characters ");
                        ctrl.value = ctrl.value.substring(0, maxSize);

                    }
                }

                return false;
            }
        });
    </script>


    <%-- document upload validation start --%>
    <script type="text/javascript">
        function setUploadButtondoc(chk) {
            var maxFileSize = 1000000;
            var fi = document.getElementById(chk.id);
            var tabValue = $(chk).attr('TabIndex');

            for (var i = 0; i <= fi.files.length - 1; i++) {
                var fsize = fi.files.item(i).size;
                if (fsize >= maxFileSize) {
                    alert("Image Size Greater Than 1MB");
                    $(chk).val("");
                }
            }
            var fileExtension = ['jpeg', 'jpg', 'JPG', 'JPEG', 'PNG', 'png'];
            var fileExtension1 = ['pdf'];
            if (tabValue == "1") {
                if ($.inArray($('#ctl00_ContentPlaceHolder1_lvDocument_ctrl0_fuDocument').val().split('.').pop().toLowerCase(), fileExtension) == -1) {
                    alert("Only formats are allowed : " + fileExtension.join(', '));
                    $("#ctl00_ContentPlaceHolder1_lvDocument_ctrl0_fuDocument").val("");
                }
            }
            else {
                if ($.inArray($(chk).val().replace(',', '.').split('.').pop().toLowerCase(), fileExtension1) == -1) {
                    alert("Only formats are allowed : " + fileExtension1.join(', '));
                    $(chk).val("");
                }
            }
        }
    </script>

    <%-- document upload validation end --%>

    <script type="text/javascript">
        function setUploadButtonState() {

            var maxFileSize = 1000000;
            var fi = document.getElementById('ctl00_ContentPlaceHolder1_FuChallan');
            for (var i = 0; i <= fi.files.length - 1; i++) {

                var fsize = fi.files.item(i).size;

                if (fsize >= maxFileSize) {
                    alert("Image Size Greater Than 1MB");
                    $("#ctl00_ContentPlaceHolder1_FuChallan").val("");

                }
            }
            var fileExtension = ['jpeg', 'jpg', 'JPG', 'JPEG', 'PNG', 'png', 'PDF', 'pdf'];
            if ($.inArray($('#ctl00_ContentPlaceHolder1_FuChallan').val().split('.').pop().toLowerCase(), fileExtension) == -1) {
                alert("Only formats are allowed : " + fileExtension.join(', '));
                $("#ctl00_ContentPlaceHolder1_FuChallan").val("");
            }
        }

    </script>
    <script type="text/javascript">
        function CheckUnchekCheckbox(chk) {
            var chkList = chk.parentNode.parentNode.parentNode;
            var chks = chkList.getElementsByTagName("input");
            for (var i = 0; i < chks.length; i++) {
                if (chks[i] != chk && chk.checked) {
                    //  chks[i].checked = false;
                    chks[i].checked = false;
                }
                else {
                    chks[i].checked = false;
                }
            }
            chk.checked = true;
        }

        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            function CheckUnchekCheckbox(chk) {
                var chkList = chk.parentNode.parentNode.parentNode;
                var chks = chkList.getElementsByTagName("input");
                for (var i = 0; i < chks.length; i++) {
                    if (chks[i] != chk && chk.checked) {
                        //  chks[i].checked = false;
                        chks[i].checked = false;
                    }
                    else {
                        chks[i].checked = false;
                    }
                }
                chk.checked = true;
            }
        });
    </script>

    <script>
        $(document).ready(function () {
            var dateval = document.getElementById('<%=txtPaymentdate.ClientID%>').value;

            var prev_date = new Date();
            prev_date.setDate(prev_date.getDate());
            $('.PaymentDate').daterangepicker({
                singleDatePicker: true,
                showDropdowns: true,
                //minDate: '01/1/1975',
                maxDate: prev_date,
                locale: {
                    format: 'DD/MM/YYYY'
                },
                autoApply: true,
            });
            if (dateval == "") {
                $('.PaymentDate').val('');
            }
        });
    </script>
    <script>
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                var dateval = document.getElementById('<%=txtPaymentdate.ClientID%>').value;

                var prev_date = new Date();
                prev_date.setDate(prev_date.getDate());
                $('.PaymentDate').daterangepicker({
                    singleDatePicker: true,
                    showDropdowns: true,
                    //minDate: '01/1/1975',
                    maxDate: prev_date,
                    locale: {
                        format: 'DD/MM/YYYY'
                    },
                    autoApply: true,
                });
                if (dateval == "") {
                    $('.PaymentDate').val('');
                }
            });
        });
    </script>

    <script>
        $(document).ready(function () {
            var curect_file_path = $('#ctl00_ContentPlaceHolder1_hdfImagePath').val();
            $("#ctl00_ContentPlaceHolder1_imageViewerContainer").verySimpleImageViewer({
                imageSource: curect_file_path,
                frame: ['30%', '30%'],
                maxZoom: '900%',
                zoomFactor: '10%',
                mouse: true,
                keyboard: true,
                toolbar: true,
                rotateToolbar: true
            });
        });
    </script>

    <script>
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                var curect_file_path = $('#ctl00_ContentPlaceHolder1_hdfImagePath').val();
                $("#ctl00_ContentPlaceHolder1_imageViewerContainer").verySimpleImageViewer({
                    imageSource: curect_file_path,
                    frame: ['30%', '30%'],
                    maxZoom: '900%',
                    zoomFactor: '10%',
                    mouse: true,
                    keyboard: true,
                    toolbar: true,
                    rotateToolbar: true
                });
            });
        });
    </script>
    <script>
        $(document).ready(function () {
            //var dateval = document.getElementById('<%=txtDateOfBirth.ClientID%>').value;

            var prev_date = new Date();
            prev_date.setDate(prev_date.getDate());
            $('.dob').daterangepicker({
                singleDatePicker: true,
                showDropdowns: true,
                //minDate: '01/1/1975',
                maxDate: prev_date,
                locale: {
                    format: 'DD/MM/YYYY'
                },
                autoApply: true,
            });
            //if (dateval == "") {
            //    $('.dob').val('');
            //}
        });
    </script>
    <script>
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                //var dateval = document.getElementById('<%=txtDateOfBirth.ClientID%>').value;

                var prev_date = new Date();
                prev_date.setDate(prev_date.getDate());
                $('.dob').daterangepicker({
                    singleDatePicker: true,
                    showDropdowns: true,
                    //minDate: '01/1/1975',
                    maxDate: prev_date,
                    locale: {
                        format: 'DD/MM/YYYY'
                    },
                    autoApply: true,
                });
                //if (dateval == "") {
                //    $('.dob').val('');
                //}
            });
        });
    </script>

</asp:Content>

