<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteMasterPage.master" CodeFile="PaymentReconcilation.aspx.cs" Inherits="ACADEMIC_PaymentReconcilation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<script runat="server">

</script>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="../css/ImageViewer.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="../css/jquery.verySimpleImageViewer.css">
    <script src="../js/jquery.verySimpleImageViewer.js"></script>

    <link href="<%=Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.css")%>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.js")%>"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/FileSaver.js/2014-11-29/FileSaver.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.12.13/xlsx.full.min.js"></script>

    <style>
        #ctl00_ContentPlaceHolder1_imageViewerContainer {
            max-width: 800px;
            height: 500px;
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
        #ctl00_ContentPlaceHolder1_pnlCourse .form-control, #ctl00_ContentPlaceHolder1_PanelRoya .form-control {
            padding: 0.15rem 0.15rem;
        }
    </style>
    <style>
        #ctl00_ContentPlaceHolder1_PanelRoya .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <style>
        #ctl00_ContentPlaceHolder1_pnlCourse .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <style>
        #ctl00_ContentPlaceHolder1_Panel1 .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>

    <style>
        #ctl00_ContentPlaceHolder1_pnlreg .form-control {
            padding: 0.25rem 0.25rem;
        }
    </style>
    <style>
        #ctl00_ContentPlaceHolder1_pnlreg .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>

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

        #ctl00_ContentPlaceHolder1_divonlineadm .ajax__calendar_container,
        #ctl00_ContentPlaceHolder1_divlvreg .ajax__calendar_container {
            z-index: 99;
        }
    </style>


    <%--===== Data Table Script added by gaurav =====--%>

    <script>

        $(document).ready(function () {
           
            var table = $('#mytable_int').DataTable({
                responsive: true,
                lengthChange: true,
                scrollY: 320,
                scrollX: true,
                scrollCollapse: true,
                paging: false, // Added by Gaurav for Hide pagination

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
                                return $('#mytable_int').DataTable().column(idx).visible();
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
                 return $('#mytable_int').DataTable().column(idx).visible();
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
                 else if ($(node).find("a").length > 0) {
                     nodereturn = "";
                     $(node).find("a").each(function () {
                         nodereturn += $(this).text();
                     });
                 }
                 else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                     nodereturn = "";
                     $(node).find("span").each(function () {
                         nodereturn += $(this).text();
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
                 else if ($(node).find("input:hidden").length > 0) {
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
             var arr = [0];
             if (arr.indexOf(idx) !== -1) {
                 return false;
             } else {
                 return $('#mytable_int').DataTable().column(idx).visible();
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
                 else if ($(node).find("a").length > 0) {
                     nodereturn = "";
                     $(node).find("a").each(function () {
                         nodereturn += $(this).text();
                     });
                 }
                 else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                     nodereturn = "";
                     $(node).find("span").each(function () {
                         nodereturn += $(this).text();
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
                 else if ($(node).find("input:hidden").length > 0) {
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
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                var table = $('#mytable_int').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 320,
                    scrollX: true,
                    scrollCollapse: true,
                    paging: false, // Added by Gaurav for Hide pagination

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
                                    return $('#mytable_int').DataTable().column(idx).visible();
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
                   return $('#mytable_int').DataTable().column(idx).visible();
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
                   else if ($(node).find("a").length > 0) {
                       nodereturn = "";
                       $(node).find("a").each(function () {
                           nodereturn += $(this).text();
                       });
                   }
                   else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                       nodereturn = "";
                       $(node).find("span").each(function () {
                           nodereturn += $(this).text();
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
                   else if ($(node).find("input:hidden").length > 0) {
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
               var arr = [0];
               if (arr.indexOf(idx) !== -1) {
                   return false;
               } else {
                   return $('#mytable_int').DataTable().column(idx).visible();
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
                   else if ($(node).find("a").length > 0) {
                       nodereturn = "";
                       $(node).find("a").each(function () {
                           nodereturn += $(this).text();
                       });
                   }
                   else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                       nodereturn = "";
                       $(node).find("span").each(function () {
                           nodereturn += $(this).text();
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
                   else if ($(node).find("input:hidden").length > 0) {
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
            });
        });

    </script>

    <script>
        $(document).ready(function () {
            var table = $('#tblstd').DataTable({
                responsive: true,
                lengthChange: true,
                scrollY: 320,
                scrollX: true,
                scrollCollapse: true,
                paging: false, // Added by Gaurav for Hide pagination

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
                                return $('#tblstd').DataTable().column(idx).visible();
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
                                            return $('#tblstd').DataTable().column(idx).visible();
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
                                            else if ($(node).find("a").length > 0) {
                                                nodereturn = "";
                                                $(node).find("a").each(function () {
                                                    nodereturn += $(this).text();
                                                });
                                            }
                                            else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                nodereturn = "";
                                                $(node).find("span").each(function () {
                                                    nodereturn += $(this).text();
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
                                            else if ($(node).find("input:hidden").length > 0) {
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
                                        var arr = [0];
                                        if (arr.indexOf(idx) !== -1) {
                                            return false;
                                        } else {
                                            return $('#tblstd').DataTable().column(idx).visible();
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
                                            else if ($(node).find("a").length > 0) {
                                                nodereturn = "";
                                                $(node).find("a").each(function () {
                                                    nodereturn += $(this).text();
                                                });
                                            }
                                            else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                nodereturn = "";
                                                $(node).find("span").each(function () {
                                                    nodereturn += $(this).text();
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
                                            else if ($(node).find("input:hidden").length > 0) {
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
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                var table = $('#tblstd').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 320,
                    scrollX: true,
                    scrollCollapse: true,
                    paging: false, // Added by Gaurav for Hide pagination

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
                                    return $('#tblstd').DataTable().column(idx).visible();
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
                                                return $('#tblstd').DataTable().column(idx).visible();
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
                                                else if ($(node).find("a").length > 0) {
                                                    nodereturn = "";
                                                    $(node).find("a").each(function () {
                                                        nodereturn += $(this).text();
                                                    });
                                                }
                                                else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                    nodereturn = "";
                                                    $(node).find("span").each(function () {
                                                        nodereturn += $(this).text();
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
                                                else if ($(node).find("input:hidden").length > 0) {
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
                                            var arr = [0];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('#tblstd').DataTable().column(idx).visible();
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
                                                else if ($(node).find("a").length > 0) {
                                                    nodereturn = "";
                                                    $(node).find("a").each(function () {
                                                        nodereturn += $(this).text();
                                                    });
                                                }
                                                else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                    nodereturn = "";
                                                    $(node).find("span").each(function () {
                                                        nodereturn += $(this).text();
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
                                                else if ($(node).find("input:hidden").length > 0) {
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
            });
        });

    </script>

    <script>
        $(document).ready(function () {
            var table = $('#mytable_int2').DataTable({
                responsive: true,
                lengthChange: true,
                scrollY: 320,
                scrollX: true,
                scrollCollapse: true,
                paging: false, // Added by Gaurav for Hide pagination

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
                                return $('#mytable_int2').DataTable().column(idx).visible();
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
                return $('#mytable_int2').DataTable().column(idx).visible();
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
                else if ($(node).find("a").length > 0) {
                    nodereturn = "";
                    $(node).find("a").each(function () {
                        nodereturn += $(this).text();
                    });
                }
                else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                    nodereturn = "";
                    $(node).find("span").each(function () {
                        nodereturn += $(this).text();
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
                else if ($(node).find("input:hidden").length > 0) {
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
            var arr = [0];
            if (arr.indexOf(idx) !== -1) {
                return false;
            } else {
                return $('#mytable_int2').DataTable().column(idx).visible();
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
                else if ($(node).find("a").length > 0) {
                    nodereturn = "";
                    $(node).find("a").each(function () {
                        nodereturn += $(this).text();
                    });
                }
                else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                    nodereturn = "";
                    $(node).find("span").each(function () {
                        nodereturn += $(this).text();
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
                else if ($(node).find("input:hidden").length > 0) {
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
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                var table = $('#mytable_int2').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 320,
                    scrollX: true,
                    scrollCollapse: true,
                    paging: false, // Added by Gaurav for Hide pagination

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
                                    return $('#mytable_int2').DataTable().column(idx).visible();
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
                   return $('#mytable_int2').DataTable().column(idx).visible();
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
                   else if ($(node).find("a").length > 0) {
                       nodereturn = "";
                       $(node).find("a").each(function () {
                           nodereturn += $(this).text();
                       });
                   }
                   else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                       nodereturn = "";
                       $(node).find("span").each(function () {
                           nodereturn += $(this).text();
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
                   else if ($(node).find("input:hidden").length > 0) {
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
               var arr = [0];
               if (arr.indexOf(idx) !== -1) {
                   return false;
               } else {
                   return $('#mytable_int2').DataTable().column(idx).visible();
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
                   else if ($(node).find("a").length > 0) {
                       nodereturn = "";
                       $(node).find("a").each(function () {
                           nodereturn += $(this).text();
                       });
                   }
                   else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                       nodereturn = "";
                       $(node).find("span").each(function () {
                           nodereturn += $(this).text();
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
                   else if ($(node).find("input:hidden").length > 0) {
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
            });
        });

    </script>
    <asp:UpdatePanel ID="update" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">

                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>
                        <div class="box-body">
                            <div class="nav-tabs-custom mt-2 col-12 pb-4" id="myTabContent">
                                <ul class="nav nav-tabs dynamic-nav-tabs" role="tablist">
                                    <li class="nav-item active" id="divlkonlineadm" runat="server">
                                        <asp:LinkButton ID="lkonlineadm" runat="server" OnClick="lkonlineadm_Click" CssClass="nav-link" TabIndex="1">Application</asp:LinkButton></li>

                                    <li class="nav-item" id="divlkerpadm" runat="server" visible="false">
                                        <asp:LinkButton ID="lkerpadm" runat="server" OnClick="lkerpadm_Click" CssClass="nav-link" TabIndex="2">Admission Proper</asp:LinkButton></li>

                                    <%--added by ekansh--%>
                                    <li class="nav-item" id="divl_lk_Addmission_Proper" runat="server">
                                        <asp:LinkButton ID="lk_Addmission_Proper" runat="server" OnClick="lk_Addmission_Proper_Click" CssClass="nav-link" TabIndex="2">Admission Proper</asp:LinkButton></li>


                                    <li class="nav-item" id="divlksemester" runat="server">
                                        <asp:LinkButton ID="lksemester" runat="server" OnClick="lksemester_Click" CssClass="nav-link" TabIndex="3">Higher Semester</asp:LinkButton></li>
                                    <li class="nav-item" id="divlkroyality" runat="server" visible="false">
                                        <asp:LinkButton ID="lkroyality" runat="server" OnClick="lkroyality_Click" CssClass="nav-link" TabIndex="4">PHD</asp:LinkButton></li>
                                    <li class="nav-item" id="DivsLoanScheme" runat="server" visible="false">
                                        <asp:LinkButton ID="lkLoanScheme" runat="server" OnClick="lkLoanScheme_Click" CssClass="nav-link" TabIndex="4">Loan Scheme</asp:LinkButton></li>
                                </ul>
                                <div class="tab-content">
                                    <div class="tab-pane fade show active" id="divonlineadm" role="tabpanel" runat="server" aria-labelledby="OnlineAdmission-tab">
                                        <div>
                                            <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updonlineadm"
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
                                        <asp:UpdatePanel ID="updonlineadm" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div class="row">
                                                    <div class="col-md-12 col-sm-12 col-12">
                                                        <%-- <div class="sub-heading mt-3">
                                                        <h5>Online Admission</h5>
                                                    </div>--%>
                                                        <div class="box-body">
                                                            <div class="col-12">
                                                                <div class="row">
                                                                    <div class="form-group col-12" runat="server" id="Selection" visible="true">
                                                                        <asp:RadioButtonList ID="rblSelection" runat="server" TabIndex="1"
                                                                            RepeatDirection="Horizontal" AutoPostBack="True"
                                                                            OnSelectedIndexChanged="rblSelection_SelectedIndexChanged">
                                                                            <asp:ListItem Value="1">Offline &nbsp;&nbsp;&nbsp;&nbsp;
                                                                            </asp:ListItem>
                                                                            <asp:ListItem Value="2">Online  &nbsp;&nbsp;&nbsp;&nbsp;                               
                                                                            </asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </div>
                                                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divstudy" visible="false">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <asp:Label ID="lblDYPrgmtype" runat="server" Font-Bold="true"></asp:Label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlstudylevel" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                                                                            ControlToValidate="ddlstudylevel" Display="None"
                                                                            ErrorMessage="Please Select Study Level" InitialValue="0" SetFocusOnError="true"
                                                                            ValidationGroup="Show"></asp:RequiredFieldValidator>
                                                                    </div>

                                                                    <div id="offline" runat="server" class="form-group col-lg-3 col-md-6 col-12" visible="false">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <asp:Label ID="lblDYSession" runat="server" Font-Bold="true"></asp:Label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlintake" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                                                            ControlToValidate="ddlintake" Display="None"
                                                                            ErrorMessage="Please Select Intake" InitialValue="0" SetFocusOnError="true"
                                                                            ValidationGroup="Show"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divclg" visible="false">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <asp:Label ID="lblDYCollege" runat="server" Font-Bold="true"></asp:Label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlfaculty" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"
                                                                            ControlToValidate="ddlfaculty" Display="None"
                                                                            ErrorMessage="Please Select Faculty/School Name" InitialValue="0" SetFocusOnError="true"
                                                                            ValidationGroup="Show"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div id="online" runat="server" class="form-group col-lg-3 col-md-6 col-12" visible="false">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <asp:Label ID="lblIntakeapt" runat="server" Font-Bold="true"></asp:Label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlintaketwo" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <%-- <asp:TextBox ID="txtorderid" runat="server" TabIndex="2" CssClass="form-control"
                                                                        ToolTip="Please Enter Order Id" />--%>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                                            ControlToValidate="ddlintaketwo" Display="None" InitialValue="0"
                                                                            ErrorMessage="Please Select Intake" SetFocusOnError="true"
                                                                            ValidationGroup="Show"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="userselection" runat="server" visible="false">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <asp:Label ID="lblUserSelection" runat="server" Font-Bold="true"></asp:Label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddluserselection" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                            <asp:ListItem Value="1">Completed</asp:ListItem>
                                                                            <asp:ListItem Value="2">Incompleted</asp:ListItem>
                                                                            <asp:ListItem Value="3">Rejected</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                                            ControlToValidate="ddluserselection" Display="None"
                                                                            ErrorMessage="Please Select Payment Status" InitialValue="0" SetFocusOnError="true"
                                                                            ValidationGroup="Show"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divBank" runat="server" visible="false">
                                                                        <div class="label-dynamic">
                                                                            <sup></sup>
                                                                            <asp:Label ID="Label2" runat="server" Font-Bold="true" Text="Bank"></asp:Label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlBank" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:HiddenField runat="server" ID="hdnCapacity" Value="0" />

                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-12 btn-footer">
                                                                <asp:Button ID="btnShow" runat="Server" Text="Show" ValidationGroup="Show" OnClick="btnShow_Click"
                                                                    CssClass="btn btn-outline-info" Visible="false" />

                                                                <asp:Button ID="btnshoworder" runat="Server" Text="Show" ValidationGroup="Show" OnClick="btnshoworder_Click"
                                                                    CssClass="btn btn-outline-info" Visible="false" />
                                                                <asp:Button ID="btnSubmit" runat="Server" Text="Submit"
                                                                    CssClass="btn btn-outline-info" Visible="false" OnClick="btnSubmit_Click" />
                                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                                                    CssClass="btn btn-outline-danger" OnClick="btnCancel_Click" Visible="false" />
                                                                <asp:ValidationSummary ID="valpaymentsummary" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                                    ShowSummary="false" ValidationGroup="Show" />
                                                            </div>
                                                            <div class="col-md-12">
                                                                <asp:Panel ID="pnlCourse" runat="server" Visible="false">
                                                                    <asp:ListView ID="lvonlineadm" runat="server">
                                                                        <LayoutTemplate>
                                                                            <div class="sub-heading">
                                                                                <h5>Student List</h5>
                                                                            </div>
                                                                            <div class="table table-responsive">
                                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="mytable_int">
                                                                                    <thead class="bg-light-blue">
                                                                                        <tr>
                                                                                            <th style="text-align: center">
                                                                                                <asp:CheckBox ID="cbHead" runat="server" OnClick="checkAllCheckbox(this)" />
                                                                                            </th>
                                                                                            <th>SrNo.</th>
                                                                                            <th>Application Id</th>
                                                                                            <th>Receipt No</th>
                                                                                            <th style="white-space: normal !important;">Name</th>
                                                                                            <th>Bank Name &nbsp;&nbsp;</th>
                                                                                            <th>Payment Mode</th>
                                                                                            <th>View</th>
                                                                                            <th>Amount &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</th>
                                                                                            <th>Payment Date</th>
                                                                                            <th>Status</th>
                                                                                            <th>Remark</th>
                                                                                            <th>NicNo.</th>
                                                                                            <th>Email</th>
                                                                                            <th>Mobile</th>
                                                                                            <th>Modified By</th>
                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tbody>
                                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                                    </tbody>
                                                                                </table>
                                                                            </div>
                                                                        </LayoutTemplate>

                                                                        <ItemTemplate>
                                                                            <tr class="classlvonlineadm">
                                                                                <td style="text-align: center">
                                                                                    <asp:CheckBox ID="chkadm" runat="server" ToolTip='<%# Eval("USERNO")%>' OnClick="checkAllCheckboxchkadm(this)" />

                                                                                </td>
                                                                                <td>
                                                                                    <%# Container.DataItemIndex + 1 %>                                                                             
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblusername" Text='<%# Eval("USERNAME") %>' ToolTip='<%# Eval("USERNAME") %>' runat="server"></asp:Label>
                                                                                </td>
                                                                                <td><%# Eval("RECEIPTNO") %></td>
                                                                                <td style="white-space: normal !important;">
                                                                                    <asp:Label ID="lblname" Text='<%# Eval("NAME") %>' ToolTip='<%# Eval("NAME") %>' runat="server"></asp:Label>
                                                                                </td>

                                                                                <td>
                                                                                    <asp:DropDownList ID="ddlbank" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                    <asp:Label ID="lblbank" Text='<%# Eval("BANKNAME") %>' ToolTip='<%# Eval("BANKNAME") %>' runat="server" Visible="false"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <%# Eval("MODETYPE") %>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:LinkButton ID="lnkView" runat="server" OnClick="lnkView_Click" Enabled='<%# Eval("GATEWAYID").ToString() == "0" ? false : true %>' CommandArgument='<%# Eval("DOC_FILENAME") %>' ToolTip='<%# Eval("GATEWAYID").ToString() == "0" ? "Pay in Cash" : "Pay in Bank" %>' Visible="false"><image id="image" style="height:35px" src="../IMAGES/viewdetail.png"></image></asp:LinkButton>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtamount" runat="server" CssClass="form-control" Text='<% #Eval("AMOUNT")%>' Enabled="false"></asp:TextBox>
                                                                                    <asp:HiddenField ID="hdnDocno" runat="server" Value='<%# Eval("USERNO") %>' />
                                                                                    <asp:HiddenField ID="hdnDoc" runat="server" Value='<%# Eval("CHALLAN_COPY") %>' />
                                                                                    <asp:HiddenField ID="hdfpaymod" runat="server" Value='<%# Eval("FEES_TYPE") %>' />
                                                                                    <asp:HiddenField ID="hdfdegree" runat="server" Value='<%# Eval("DEGREENAME") %>' />
                                                                                    <asp:HiddenField ID="hdfintake" runat="server" Value='<%# Eval("BATCHNAME") %>' />

                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtdate" onchange="return RestrictDate(this)" runat="server" CssClass="form-control" Text='<%# Eval("RECONDATE") %>'></asp:TextBox>

                                                                                    <ajaxToolKit:CalendarExtender ID="ceStartDate" runat="server" Format="dd/MM/yyyy"
                                                                                        TargetControlID="txtdate" PopupButtonID="dvcal1" OnClientDateSelectionChanged="checkDate" />

                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="ddlstatus" runat="server" CssClass="form-control" data-select2-enable="true"
                                                                                        AppendDataBoundItems="True" OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged" onchange="return Enable_Disable_Amount(this,'txtamount');">
                                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Approved</asp:ListItem>
                                                                                        <asp:ListItem Value="2">Pending</asp:ListItem>
                                                                                        <asp:ListItem Value="3">Reject</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                    <asp:Label ID="lblstatus" runat="server" Text='<% #Eval("PROVSTATUS")%>' ToolTip='<% #Eval("PROVSTATUS")%>' Visible="false"></asp:Label></td>

                                                                                <td>
                                                                                    <asp:TextBox ID="txtremark" runat="server" CssClass="form-control" Text='<%# Eval("REMARK") %>'></asp:TextBox>

                                                                                </td>
                                                                                <td>
                                                                                    <%# Eval("NIC") %>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblemail" Text='<%# Eval("EMAILID") %>' ToolTip='<%# Eval("EMAILID") %>' runat="server"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblmobile" Text='<%# Eval("MOBILENO") %>' ToolTip='<%# Eval("MOBILENO") %>' runat="server"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <%# Eval("UA_FULLNAME") %>
                                                                                </td>
                                                                            </tr>

                                                                        </ItemTemplate>

                                                                    </asp:ListView>
                                                                </asp:Panel>
                                                            </div>
                                                            <div class="col-md-12 col-sm-12 col-12">
                                                                <asp:Panel ID="Panel2" runat="server" Visible="false">
                                                                    <asp:ListView ID="LvApplicationCount" runat="server">
                                                                        <LayoutTemplate>
                                                                            <div class="sub-heading">
                                                                                <h5>Student Count List For Application</h5>
                                                                            </div>
                                                                            <div class="table table-responsive">
                                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%;">
                                                                                    <thead class="bg-light-blue">
                                                                                        <tr>
                                                                                            <th>SrNo.</th>
                                                                                            <th>Type</th>
                                                                                            <th>Intake</th>
                                                                                            <th>Study Level</th>
                                                                                            <th>Total Confirmed</th>
                                                                                            <th>Total Pending</th>
                                                                                            <th>Total Rejected</th>

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
                                                                                    <%# Container.DataItemIndex + 1 %>                                                                             
                                                                                </td>
                                                                                <td><%# Eval("TYPE") %></td>
                                                                                <td><%# Eval("BATCHNAME") %></td>
                                                                                <td><%# Eval("UA_SECTIONNAME")%></td>
                                                                                <td><%# Eval("TOTALACONFIRMEDAPPLI") %></td>
                                                                                <td><%# Eval("TOTALAPENDINGAPPLI") %></td>
                                                                                <td><%# Eval("TOTALAREJECTAPPLI") %></td>
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

                                        </asp:UpdatePanel>
                                    </div>

                                    <div id="diverpadm" runat="server" visible="false" role="tabpanel" aria-labelledby="ERPAdmission-tab">
                                        <div>
                                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upderpadm"
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
                                        <asp:UpdatePanel ID="upderpadm" runat="server">
                                            <ContentTemplate>
                                                <div class="row">
                                                    <div class="col-md-12 col-sm-12 col-12">
                                                        <%-- <div class="sub-heading mt-3">
                                                        <h5>Online Admission</h5>
                                                    </div>--%>
                                                        <div class="box-body">
                                                            <div class="col-12">
                                                                <div class="row">
                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label><span id="spStude">Application ID</span></label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlIDChange" runat="server" AutoPostBack="true" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlIDChange_SelectedIndexChanged">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                            <asp:ListItem Value="1">Application ID</asp:ListItem>
                                                                            <asp:ListItem Value="2">Student ID</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="registration" visible="true">
                                                                        <div class="label-dynamic">
                                                                            <sup></sup>
                                                                            <label></label>
                                                                        </div>
                                                                        <asp:RadioButtonList ID="rdioregistration" runat="server"
                                                                            RepeatDirection="Horizontal" AutoPostBack="True"
                                                                            OnSelectedIndexChanged="rdioregistration_SelectedIndexChanged" Visible="false">
                                                                            <asp:ListItem Value="1">Offline &nbsp;&nbsp;&nbsp;&nbsp;
                                                                            </asp:ListItem>
                                                                            <%--<asp:ListItem Value="2">Online  &nbsp;&nbsp;&nbsp;&nbsp;                               
                                                                            </asp:ListItem>--%>
                                                                        </asp:RadioButtonList>

                                                                        <asp:RadioButtonList ID="rdioregistrationOnline" runat="server"
                                                                            RepeatDirection="Horizontal" AutoPostBack="True"
                                                                            OnSelectedIndexChanged="rdioregistrationOnline_SelectedIndexChanged" Visible="true">
                                                                            <asp:ListItem Value="1">Offline &nbsp;&nbsp;&nbsp;&nbsp;
                                                                            </asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divstudyleveltwo" visible="false">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <asp:Label ID="lblDyAdmissionType" runat="server" Font-Bold="true"></asp:Label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlstudytwo" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server"
                                                                            ControlToValidate="ddlstudytwo" Display="None"
                                                                            ErrorMessage="Please Select Study Level" InitialValue="0" SetFocusOnError="true"
                                                                            ValidationGroup="ShowReg"></asp:RequiredFieldValidator>
                                                                    </div>

                                                                    <div id="div1intake2" runat="server" class="form-group col-lg-3 col-md-6 col-12" visible="false">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <asp:Label ID="lblAdmissionBatch" runat="server" Font-Bold="true"></asp:Label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlintakeregistration" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                                            ControlToValidate="ddlintakeregistration" Display="None"
                                                                            ErrorMessage="Please Select Intake" InitialValue="0" SetFocusOnError="true"
                                                                            ValidationGroup="ShowReg"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div id="div2intake2" runat="server" class="form-group col-lg-3 col-md-6 col-12" visible="false">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <asp:Label ID="lblIntake" runat="server" Font-Bold="true"></asp:Label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlintakeregtwo" runat="server" ValidationGroup="ShowReg" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <%-- <asp:TextBox ID="txtorderid" runat="server" TabIndex="2" CssClass="form-control"
                                                                        ToolTip="Please Enter Order Id" />--%>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                                                            ControlToValidate="ddlintakeregtwo" Display="None" InitialValue="0"
                                                                            ErrorMessage="Please Select Intake" SetFocusOnError="true"
                                                                            ValidationGroup="ShowReg"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="div1paystatus2" runat="server" visible="false">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <asp:Label ID="lblregstatus" runat="server" Font-Bold="true"></asp:Label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlpaystatustwo" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                            <asp:ListItem Value="1">Completed</asp:ListItem>
                                                                            <asp:ListItem Value="2">Incompleted</asp:ListItem>
                                                                            <asp:ListItem Value="3">Rejected</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                                                            ControlToValidate="ddlpaystatustwo" Display="None"
                                                                            ErrorMessage="Please Select Payment Status" InitialValue="0" SetFocusOnError="true"
                                                                            ValidationGroup="ShowReg"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divBankEnrollment" runat="server" visible="false">
                                                                        <div class="label-dynamic">
                                                                            <sup></sup>
                                                                            <asp:Label ID="Label3" runat="server" Font-Bold="true" Text="Bank"></asp:Label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlBankEnrollnment" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:Label ID="lblbank" Text='<%# Eval("BANKNAME") %>' ToolTip='<%# Eval("BANKNAME") %>' runat="server" Visible="false"></asp:Label>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divfacultytwo" visible="false">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <asp:Label ID="lblfaculty" runat="server" Font-Bold="true"></asp:Label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlfacultytwo" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server"
                                                                            ControlToValidate="ddlfacultytwo" Display="None"
                                                                            ErrorMessage="Please Select Faculty/School Name" InitialValue="0" SetFocusOnError="true"
                                                                            ValidationGroup="ShowReg"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-12 btn-footer">
                                                                <asp:Button ID="btnshowregone" runat="Server" Text="Show" ValidationGroup="ShowReg" OnClick="btnshowregone_Click"
                                                                    CssClass="btn btn-outline-info" Visible="false" />
                                                                <asp:Button ID="btnShowAddmissionProper" runat="Server" Text="Show" ValidationGroup="ShowReg" OnClick="btnShowAddmissionProper_Click"
                                                                    CssClass="btn btn-outline-info" Visible="false" />
                                                                <asp:Button ID="btnshowregtwo" runat="Server" Text="Show" ValidationGroup="ShowReg" OnClick="btnshowregtwo_Click"
                                                                    CssClass="btn btn-outline-info" Visible="false" />
                                                                <asp:Button ID="btnsubreg" runat="Server" Text="Submit"
                                                                    CssClass="btn btn-outline-info" Visible="false" OnClick="btnsubreg_Click" />
                                                                <asp:Button ID="btnSubAddProper" runat="Server" Text="Submit"
                                                                    CssClass="btn btn-outline-info" Visible="false" OnClick="btnSubAddProper_Click" />
                                                                <asp:Button ID="btnsubregtwo" runat="Server" Text="Submit"
                                                                    CssClass="btn btn-outline-info" Visible="false" />
                                                                <asp:Button ID="btncancelreg" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                                                    CssClass="btn btn-outline-danger" OnClick="btncancelreg_Click" Visible="false" />
                                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                                    ShowSummary="false" ValidationGroup="ShowReg" />
                                                            </div>
                                                            <div class="col-12">
                                                                <asp:Panel ID="pnlreg" runat="server" Visible="false">
                                                                    <asp:ListView ID="lvreg" runat="server" OnPagePropertiesChanging="lvreg_PagePropertiesChanging">
                                                                        <LayoutTemplate>
                                                                            <div class="sub-heading">
                                                                                <h5>Student List</h5>
                                                                            </div>
                                                                            <%--  <div class="row mb-1">
                                                                                <div class="col-lg-2 col-md-6 offset-lg-7">
                                                                                    <button type="button" class="btn btn-outline-primary float-lg-right saveAsExcel">Export Excel</button>
                                                                                </div>

                                                                                <div class="col-lg-3 col-md-6">
                                                                                    <div class="input-group sea-rch">
                                                                                       
                                                                                        <asp:TextBox ID="txtSearch" runat="server" OnTextChanged="txtSearch_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                                        <div class="input-group-addon">
                                                                                            <i class="fa fa-search"></i>
                                                                                        </div>
                                                                                    </div>

                                                                                </div>
                                                                            </div>--%>
                                                                            <div class="table-responsive" style="max-height: 500px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="tblstd">
                                                                                    <thead class="bg-light-blue" style="position: sticky; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                                        <tr>
                                                                                            <td style="text-align: center">
                                                                                                <asp:CheckBox ID="cheadreg" runat="server" OnClick="checkAll(this)" />
                                                                                            </td>
                                                                                            <td>SrNo.</td>
                                                                                            <th>Application Id</th>
                                                                                            <th style="padding-right: 40px">Receipt No.</th>
                                                                                            <%--<th>Payment For</th>--%>
                                                                                            <th>Payment Mode</th>
                                                                                            <th style="white-space: normal !important;">Name</th>
                                                                                            <th>Bank Name &nbsp;&nbsp;</th>
                                                                                            <th>View</th>
                                                                                            <th style="padding-right: 40px">Amount &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</th>
                                                                                            <th style="padding-right: 40px">Payment Date</th>
                                                                                            <th>Status</th>
                                                                                            <th style="padding-right: 40px">Remark</th>
                                                                                            <%--<th>NicNo.</th>--%>
                                                                                            <th>Email</th>
                                                                                            <th>Mobile</th>
                                                                                            <%--<th>Modified By</th>--%>
                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tbody>
                                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                                    </tbody>
                                                                                </table>
                                                                            </div>
                                                                            <div class="col-12 pt-1 pb-2" id="Tfoot1" runat="server">
                                                                                <div class="float-right">
                                                                                    <asp:DataPager ID="DataPagerReg" runat="server" PagedControlID="lvreg" PageSize="1000">
                                                                                        <Fields>
                                                                                            <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="false" ShowPreviousPageButton="true"
                                                                                                ShowNextPageButton="false" />
                                                                                            <asp:NumericPagerField ButtonType="Link" />
                                                                                            <asp:NextPreviousPagerField ButtonType="Link" ShowNextPageButton="true" ShowLastPageButton="false" ShowPreviousPageButton="false" />
                                                                                        </Fields>
                                                                                    </asp:DataPager>
                                                                                </div>
                                                                            </div>
                                                                            <%--   Added By Abhijit Naik--%>
                                                                            <%--  <div class="col-12 pt-1 pb-2" id="div2" runat="server">--%>
                                                                            <%--  <div class="float-right">
                                                                                    <asp:DataPager ID="DataPager1" runat="server" PagedControlID="lvreg" PageSize="1000">
                                                                                        <Fields>
                                                                                            <asp:TemplatePagerField>
                                                                                                <PagerTemplate>
                                                                                                    <asp:LinkButton runat="server" OnClick="lnkPrevious_Click" ID="lnkPrevious" Text="Prevoius"></asp:LinkButton>
                                                                                                    <asp:LinkButton runat="server" OnClick="lnkNext_Click" ID="lnkNext" Text="Next"></asp:LinkButton>
                                                                                                </PagerTemplate>
                                                                                            </asp:TemplatePagerField>
                                                                                        </Fields>
                                                                                    </asp:DataPager>
                                                                                </div>--%>

                                                                            <%-- <div class="float-left">
                                                                                    <asp:DataPager ID="datapager2" runat="server" PagedControlID="lvreg" PageSize="1000">
                                                                                        <Fields>
                                                                                            <asp:TemplatePagerField>
                                                                                                <PagerTemplate>
                                                                                                    <asp:Label ID="Label2" runat="server" Text="Total Record :"></asp:Label>
                                                                                                    <asp:Label runat="server" ID="lbltotalcount"></asp:Label>
                                                                                                </PagerTemplate>
                                                                                            </asp:TemplatePagerField>
                                                                                        </Fields>
                                                                                    </asp:DataPager>
                                                                                </div>--%>
                                                                            <%--       </div>--%>
                                                                        </LayoutTemplate>




                                                                        <ItemTemplate>
                                                                            <tr>
                                                                                <td style="text-align: center">
                                                                                    <asp:CheckBox ID="chreg" runat="server" ToolTip='<%# Eval("IDNO")%>' />
                                                                                    <asp:Label ID="lblDcrTempNo" Text='<%# Eval("TEMP_DCR_NO") %>' ToolTip='<%# Eval("IDNO")  %>' runat="server" Visible="false"></asp:Label>
                                                                                    <asp:HiddenField ID="hdfdcr" Value='<%# Eval("DCR_NO")%>' runat="server" />
                                                                                    <asp:HiddenField ID="hdfusername" Value='<%# Eval("USERNAME")%>' runat="server" />
                                                                                </td>
                                                                                <td>
                                                                                    <%# Container.DataItemIndex + 1 %>                                                                             
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblregusername" Text='<%# Eval("USERNAME") %>' ToolTip='<%# Eval("USERNAME") %>' runat="server"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label1" Text='<%# Eval("REC_NO") %>' runat="server"></asp:Label>
                                                                                </td>
                                                                                <%-- <td></td>--%>
                                                                                <td>
                                                                                    <asp:Label ID="lblpaymentmodeenroll" Text='<%# Eval("PAYMENTMODE") %>' runat="server"></asp:Label>
                                                                                </td>
                                                                                <td style="white-space: normal !important;">
                                                                                    <asp:Label ID="lblregname" Text='<%# Eval("NAME") %>' ToolTip='<%# Eval("NAME") %>' runat="server"></asp:Label>
                                                                                </td>

                                                                                <td>
                                                                                    <asp:DropDownList ID="ddlbank" runat="server" AppendDataBoundItems="true" CssClass="form-group">
                                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                    <asp:Label ID="lblbank" Text='<%# Eval("BANKNAME") %>' ToolTip='<%# Eval("BANKNAME") %>' runat="server" Visible="false"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <%--<asp:Image ID="ImageViewer" runat="server" Width="100%" Height="500px"/>--%>
                                                                                    <asp:LinkButton ID="lnkreg" runat="server" OnClick="lnkreg_Click" Enabled='<%# Eval("BANKNAME").ToString() == "0" ? false : true %>' CommandArgument='<%# Eval("DOC_FILENAME") %>' CommandName='<%# Eval("TEMP_DCR_NO") %>' Visible="false"><image style="height:35px" src="../IMAGES/viewdetail.png"></image></asp:LinkButton>

                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtregamount" runat="server" CssClass="form-control" Text='<% #Eval("AMOUNT")%>' Enabled="false"></asp:TextBox>
                                                                                    <asp:HiddenField ID="hdnDocno" runat="server" Value='<%# Eval("USERNO")  %>' />
                                                                                    <asp:HiddenField ID="hdnDoc" runat="server" Value='<%# Eval("CHALLAN_COPY") %>' />
                                                                                    <asp:HiddenField ID="hdfpaymod" runat="server" Value='<%# Eval("FEES_TYPE") %>' />
                                                                                    <asp:HiddenField ID="hdfintake" runat="server" Value='<%# Eval("BATCHNAME") %>' />
                                                                                    <asp:HiddenField ID="hdfdegree" runat="server" Value='<%# Eval("DEGREENAME") %>' />

                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtregdate" onchange="return RestrictDate(this)" runat="server" CssClass="form-control" Text='<%# Eval("REC_DT") %>'></asp:TextBox>
                                                                                    <ajaxToolKit:CalendarExtender ID="ceStartDate" runat="server" Format="dd/MM/yyyy"
                                                                                        TargetControlID="txtregdate" PopupButtonID="dvcal1" OnClientDateSelectionChanged="checkDate" />

                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="ddlregstatus" runat="server" CssClass="form-group" AppendDataBoundItems="True" Enabled='<% #Eval("STATUS").ToString() == "1" ? false : Eval("STATUS").ToString() == "3" ? false : true%>'
                                                                                        OnSelectedIndexChanged="ddlregstatus_SelectedIndexChanged" onchange="return Enable_Disable_Amount(this,'txtregamount');">
                                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Approved</asp:ListItem>
                                                                                        <asp:ListItem Value="2">Pending</asp:ListItem>
                                                                                        <asp:ListItem Value="3">Reject</asp:ListItem>
                                                                                    </asp:DropDownList>

                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtregremark" runat="server" CssClass="form-control" Text='<%# Eval("REMARK") %>'></asp:TextBox>
                                                                                    <asp:Label ID="lblregstatus" runat="server" Text='<% #Eval("STATUS")%>' ToolTip='<% #Eval("STATUS")%>' Visible="false"></asp:Label>
                                                                                </td>
                                                                                <%-- <td>
                                                                                    <%# Eval("NIC") %>
                                                                                </td>--%>
                                                                                <td>
                                                                                    <asp:Label ID="lblregemail" Text='<%# Eval("EMAILID") %>' ToolTip='<%# Eval("EMAILID") %>' runat="server"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblregmobile" Text='<%# Eval("MOBILENO") %>' ToolTip='<%# Eval("MOBILENO") %>' runat="server"></asp:Label>
                                                                                </td>
                                                                            </tr>

                                                                        </ItemTemplate>

                                                                    </asp:ListView>
                                                                </asp:Panel>
                                                            </div>

                                                            <%--added by ekansh moundekar--%>

                                                            <div class="col-12">
                                                                <asp:Panel ID="pnlregOffline" runat="server" Visible="false">
                                                                    <asp:ListView ID="lvregOffline" runat="server" OnPagePropertiesChanging="lvregOffline_PagePropertiesChanging">
                                                                        <LayoutTemplate>
                                                                            <div class="sub-heading">
                                                                                <h5>Student List</h5>
                                                                            </div>
                                                                            <%--  <div class="row mb-1">
                                                                                <div class="col-lg-2 col-md-6 offset-lg-7">
                                                                                    <button type="button" class="btn btn-outline-primary float-lg-right saveAsExcel">Export Excel</button>
                                                                                </div>

                                                                                <div class="col-lg-3 col-md-6">
                                                                                    <div class="input-group sea-rch">
                                                                                       
                                                                                        <asp:TextBox ID="txtSearch" runat="server" OnTextChanged="txtSearch_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                                        <div class="input-group-addon">
                                                                                            <i class="fa fa-search"></i>
                                                                                        </div>
                                                                                    </div>

                                                                                </div>
                                                                            </div>--%>
                                                                            <div class="table-responsive" style="max-height: 500px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="tblstd">
                                                                                    <thead class="bg-light-blue" style="position: sticky; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                                        <tr>
                                                                                            <td style="text-align: center">
                                                                                                <asp:CheckBox ID="cheadreg" runat="server" OnClick="checkAll(this)" />
                                                                                            </td>
                                                                                            <td>SrNo.</td>
                                                                                            <th>Application Id</th>
                                                                                            <th style="padding-right: 40px">Receipt No.</th>
                                                                                            <%--<th>Payment For</th>--%>
                                                                                            <th>Payment Mode</th>
                                                                                            <th style="white-space: normal !important;">Name</th>
                                                                                            <th>Bank Name &nbsp;&nbsp;</th>
                                                                                            <th>View</th>
                                                                                            <th style="padding-right: 40px">Amount &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</th>
                                                                                            <th style="padding-right: 40px">Payment Date</th>
                                                                                            <th>Status</th>
                                                                                            <th style="padding-right: 40px">Remark</th>
                                                                                            <%--<th>NicNo.</th>--%>
                                                                                            <th>Email</th>
                                                                                            <th>Mobile</th>
                                                                                            <%--<th>Modified By</th>--%>
                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tbody>
                                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                                    </tbody>
                                                                                </table>
                                                                            </div>
                                                                            <div class="col-12 pt-1 pb-2" id="Tfoot1" runat="server">
                                                                                <div class="float-right">
                                                                                    <asp:DataPager ID="DataPagerReg" runat="server" PagedControlID="lvregOffline" PageSize="1000">
                                                                                        <Fields>
                                                                                            <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="false" ShowPreviousPageButton="true"
                                                                                                ShowNextPageButton="false" />
                                                                                            <asp:NumericPagerField ButtonType="Link" />
                                                                                            <asp:NextPreviousPagerField ButtonType="Link" ShowNextPageButton="true" ShowLastPageButton="false" ShowPreviousPageButton="false" />
                                                                                        </Fields>
                                                                                    </asp:DataPager>
                                                                                </div>
                                                                            </div>
                                                                            <%--   Added By Abhijit Naik--%>
                                                                            <%--  <div class="col-12 pt-1 pb-2" id="div2" runat="server">--%>
                                                                            <%--  <div class="float-right">
                                                                                    <asp:DataPager ID="DataPager1" runat="server" PagedControlID="lvreg" PageSize="1000">
                                                                                        <Fields>
                                                                                            <asp:TemplatePagerField>
                                                                                                <PagerTemplate>
                                                                                                    <asp:LinkButton runat="server" OnClick="lnkPrevious_Click" ID="lnkPrevious" Text="Prevoius"></asp:LinkButton>
                                                                                                    <asp:LinkButton runat="server" OnClick="lnkNext_Click" ID="lnkNext" Text="Next"></asp:LinkButton>
                                                                                                </PagerTemplate>
                                                                                            </asp:TemplatePagerField>
                                                                                        </Fields>
                                                                                    </asp:DataPager>
                                                                                </div>--%>

                                                                            <%-- <div class="float-left">
                                                                                    <asp:DataPager ID="datapager2" runat="server" PagedControlID="lvreg" PageSize="1000">
                                                                                        <Fields>
                                                                                            <asp:TemplatePagerField>
                                                                                                <PagerTemplate>
                                                                                                    <asp:Label ID="Label2" runat="server" Text="Total Record :"></asp:Label>
                                                                                                    <asp:Label runat="server" ID="lbltotalcount"></asp:Label>
                                                                                                </PagerTemplate>
                                                                                            </asp:TemplatePagerField>
                                                                                        </Fields>
                                                                                    </asp:DataPager>
                                                                                </div>--%>
                                                                            <%--       </div>--%>
                                                                        </LayoutTemplate>

                                                                        <ItemTemplate>
                                                                            <tr>
                                                                                <td style="text-align: center">
                                                                                    <asp:CheckBox ID="chreg" runat="server" ToolTip='<%# Eval("USERNO") %>' />
                                                                                    <asp:Label ID="lblDcrTempNo" Text='<%# Eval("TEMP_DCR_NO") %>' runat="server" Visible="false"></asp:Label>
                                                                                    <asp:HiddenField ID="hdfdcr" Value='<%# Eval("DCR_NO")%>' runat="server" />
                                                                                    <asp:HiddenField ID="hdfusername" Value='<%# Eval("USERNAME")%>' runat="server" />

                                                                                </td>
                                                                                <td>
                                                                                    <%# Container.DataItemIndex + 1 %>                                                                             
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblregusername" Text='<%# Eval("USERNAME") %>' ToolTip='<%# Eval("USERNAME") %>' runat="server"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label1" Text='<%# Eval("REC_NO") %>' runat="server"></asp:Label>
                                                                                </td>
                                                                                <%-- <td></td>--%>
                                                                                <td>
                                                                                    <asp:Label ID="lblpaymentmodeenroll" Text='<%# Eval("PAYMENTMODE") %>' runat="server"></asp:Label>
                                                                                </td>
                                                                                <td style="white-space: normal !important;">
                                                                                    <asp:Label ID="lblregname" Text='<%# Eval("NAME") %>' ToolTip='<%# Eval("NAME") %>' runat="server"></asp:Label>
                                                                                </td>

                                                                                <td>
                                                                                    <asp:DropDownList ID="ddlbank" runat="server" AppendDataBoundItems="true" CssClass="form-group">
                                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                    <asp:Label ID="lblbank" Text='<%# Eval("BANKNAME") %>' ToolTip='<%# Eval("BANKNAME") %>' runat="server" Visible="false"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <%--<asp:Image ID="ImageViewer" runat="server" Width="100%" Height="500px"/>--%>
                                                                                    <%--<asp:LinkButton ID="lnkreg" runat="server" OnClick="lnkreg_Click" Enabled='<%# Eval("BANKNAME").ToString() == "0" ? false : true %>' CommandArgument='<%# Eval("DOC_FILENAME") %>' CommandName='<%# Eval("TEMP_DCR_NO") %>' Visible="false"><image style="height:35px" src="../IMAGES/viewdetail.png"></image></asp:LinkButton>--%>
                                                                                    <asp:LinkButton ID="lnkregOnline" runat="server" OnClick="lnkregOnline_Click" Enabled='<%# Eval("BANKNAME").ToString() == "0" ? false : true %>' CommandArgument='<%# Eval("DOC_FILENAME") %>' CommandName='<%# Eval("TEMP_DCR_NO") %>' Visible="false"><image style="height:35px" src="../IMAGES/viewdetail.png"></image></asp:LinkButton>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtregamount" runat="server" CssClass="form-control" Text='<% #Eval("AMOUNT")%>'></asp:TextBox>
                                                                                    <asp:HiddenField ID="hdnDocno" runat="server" Value='<%# Eval("USERNO")  %>' />
                                                                                    <asp:HiddenField ID="hdnDoc" runat="server" Value='<%# Eval("CHALLAN_COPY") %>' />
                                                                                    <asp:HiddenField ID="hdfpaymod" runat="server" Value='<%# Eval("FEES_TYPE") %>' />
                                                                                    <asp:HiddenField ID="hdfintake" runat="server" Value='<%# Eval("BATCHNAME") %>' />
                                                                                    <asp:HiddenField ID="hdfdegree" runat="server" Value='<%# Eval("DEGREENAME") %>' />

                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtregdate" onchange="return RestrictDate(this)" runat="server" CssClass="form-control" Text='<%# Eval("REC_DT") %>'></asp:TextBox>
                                                                                    <ajaxToolKit:CalendarExtender ID="ceStartDate" runat="server" Format="dd/MM/yyyy"
                                                                                        TargetControlID="txtregdate" PopupButtonID="dvcal1" OnClientDateSelectionChanged="checkDate" />

                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="ddlregstatus" runat="server" CssClass="form-group" AppendDataBoundItems="True" Enabled='<% #Eval("STATUS").ToString() == "1" ? false : Eval("STATUS").ToString() == "3" ? false : true%>'
                                                                                        OnSelectedIndexChanged="ddlregstatus_SelectedIndexChanged" onchange="return Enable_Disable_Amount(this,'txtregamount');">
                                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Approved</asp:ListItem>
                                                                                        <asp:ListItem Value="2">Pending</asp:ListItem>
                                                                                        <asp:ListItem Value="3">Reject</asp:ListItem>
                                                                                    </asp:DropDownList>

                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtregremark" runat="server" CssClass="form-control" Text='<%# Eval("REMARK") %>'></asp:TextBox>
                                                                                    <asp:Label ID="lblregstatus" runat="server" Text='<% #Eval("STATUS")%>' ToolTip='<% #Eval("STATUS")%>' Visible="false"></asp:Label>
                                                                                </td>
                                                                                <%-- <td>
                                                                                    <%# Eval("NIC") %>
                                                                                </td>--%>
                                                                                <td>
                                                                                    <asp:Label ID="lblregemail" Text='<%# Eval("EMAILID") %>' ToolTip='<%# Eval("EMAILID") %>' runat="server"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblregmobile" Text='<%# Eval("MOBILENO") %>' ToolTip='<%# Eval("MOBILENO") %>' runat="server"></asp:Label>
                                                                                </td>
                                                                            </tr>

                                                                        </ItemTemplate>

                                                                    </asp:ListView>
                                                                </asp:Panel>
                                                            </div>





                                                            <div class="col-md-12 col-sm-12 col-12 mt-5">
                                                                <asp:Panel ID="Panel3" runat="server" Visible="false">
                                                                    <asp:ListView ID="LvEnrollmentCount" runat="server">
                                                                        <LayoutTemplate>
                                                                            <div class="sub-heading">
                                                                                <h5>Student Count List</h5>
                                                                            </div>
                                                                            <div class="table table-responsive">
                                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%;">
                                                                                    <thead class="bg-light-blue">
                                                                                        <tr>
                                                                                            <th>SrNo.</th>
                                                                                            <th>Type</th>
                                                                                            <th>Intake</th>
                                                                                            <th>Study Level</th>
                                                                                            <th>Total Confirmed</th>
                                                                                            <th>Total Pending</th>
                                                                                            <th>Total Rejected</th>

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
                                                                                    <%# Container.DataItemIndex + 1 %>                                                                             
                                                                                </td>
                                                                                <td><%# Eval("TYPE") %></td>
                                                                                <td><%# Eval("BATCHNAME") %></td>
                                                                                <td><%# Eval("UA_SECTIONNAME")%></td>
                                                                                <td><%# Eval("TOTALACONFIRMEDAPPLI") %></td>
                                                                                <td><%# Eval("TOTALAPENDINGAPPLI") %></td>
                                                                                <td><%# Eval("TOTALAREJECTAPPLI") %></td>
                                                                            </tr>

                                                                        </ItemTemplate>

                                                                    </asp:ListView>
                                                                </asp:Panel>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="rdioregistration" />
                                                <asp:PostBackTrigger ControlID="btnshowregone" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>

                                    <div id="divsemster" runat="server" visible="false" role="tabpanel" aria-labelledby="Semester-tab">
                                        <div>
                                            <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updsemster"
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
                                        <asp:UpdatePanel ID="updsemster" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div class="row">
                                                    <div class="col-md-12 col-sm-12 col-12">
                                                        <div class="box-body">
                                                            <div class="col-12">
                                                                <div class="row">
                                                                    <div class="form-group col-12" runat="server" id="semselection" visible="true">
                                                                        <asp:RadioButtonList ID="rdosemselection" runat="server"
                                                                            RepeatDirection="Horizontal" AutoPostBack="True"
                                                                            OnSelectedIndexChanged="rdosemselection_SelectedIndexChanged">
                                                                            <asp:ListItem Value="1">Offline &nbsp;&nbsp;&nbsp;&nbsp;
                                                                            </asp:ListItem>
                                                                            <asp:ListItem Value="2">Online  &nbsp;&nbsp;&nbsp;&nbsp;                               
                                                                            </asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </div>
                                                                    <div id="intakesem" class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <asp:Label ID="lblsession" runat="server" Font-Bold="true" Text="Session Name"></asp:Label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlintakethree" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server"
                                                                            ControlToValidate="ddlintakethree" Display="None"
                                                                            ErrorMessage="Please Select Session Name" InitialValue="0" SetFocusOnError="true"
                                                                            ValidationGroup="showsem"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="studysem" runat="server" visible="false">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <asp:Label ID="lblDYstudylevel" runat="server" Font-Bold="true"></asp:Label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlstudylevelsem" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server"
                                                                            ControlToValidate="ddlstudylevelsem" Display="None"
                                                                            ErrorMessage="Please Select Study Level" InitialValue="0" SetFocusOnError="true"
                                                                            ValidationGroup="showsem"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="form-group col-lg-6 col-md-6 col-12" id="divProgram" runat="server" visible="false">
                                                                        <div class="label-dynamic">
                                                                            <%--<sup>* </sup>--%>
                                                                            <label>Program</label>
                                                                        </div>
                                                                        <asp:ListBox ID="ddlProgram" runat="server" AppendDataBoundItems="true" CssClass="form-control multi-select-demo" SelectionMode="multiple"></asp:ListBox>

                                                                    </div>
                                                                    <div class="form-group col-lg-4 col-md-6 col-12" id="sem" runat="server" visible="false">
                                                                        <div class="label-dynamic">
                                                                            <%--<sup>* </sup>--%>
                                                                            <asp:Label ID="lblDYSemester" runat="server" Font-Bold="true"></asp:Label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlsemester" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server"
                                                                        ControlToValidate="ddlsemester" Display="None"
                                                                        ErrorMessage="Please Select Semester" InitialValue="0" SetFocusOnError="true"
                                                                        ValidationGroup="showsem"></asp:RequiredFieldValidator>--%>
                                                                    </div>
                                                                    <div class="form-group col-lg-4 col-md-6 col-12" id="divstatusem" runat="server" visible="false">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <asp:Label ID="lblsemstatus" runat="server" Font-Bold="true"></asp:Label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlstatus" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                            <asp:ListItem Value="1">Completed</asp:ListItem>
                                                                            <asp:ListItem Value="2">Incompleted</asp:ListItem>
                                                                            <asp:ListItem Value="3">Rejected</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server"
                                                                            ControlToValidate="ddlstatus" Display="None"
                                                                            ErrorMessage="Please Select Payment Status" InitialValue="0" SetFocusOnError="true"
                                                                            ValidationGroup="showsem"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="form-group col-lg-4 col-md-6 col-12" id="divHigherBank" runat="server" visible="false">
                                                                        <div class="label-dynamic">
                                                                            <sup></sup>
                                                                            <asp:Label ID="Label4" runat="server" Font-Bold="true" Text="Bank"></asp:Label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlBankHigherSem" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>

                                                                    </div>

                                                                </div>

                                                            </div>
                                                            <div class="col-12 btn-footer">
                                                                <asp:Button ID="btnshowsem" runat="Server" Text="Show" ValidationGroup="showsem"
                                                                    CssClass="btn btn-outline-info" OnClick="btnshowsem_Click" Visible="false" />
                                                                <asp:Button ID="btnshowsemoff" runat="Server" Text="Show" ValidationGroup="showsem"
                                                                    CssClass="btn btn-outline-info" OnClick="btnshowsemoff_Click" Visible="false" />


                                                                <asp:Button ID="btnsubsem" runat="Server" Text="Submit" ValidationGroup="showsem" CssClass="btn btn-outline-info" Visible="false" OnClick="btnsubsem_Click" />
                                                                <asp:Button ID="btncancelsem" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                                                    CssClass="btn btn-outline-danger" OnClick="btncancelsem_Click" Visible="false" />
                                                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                                    ShowSummary="false" ValidationGroup="showsem" />
                                                            </div>
                                                            <div class="col-md-12">
                                                                <asp:Panel ID="Panel1" runat="server" Visible="false">
                                                                    <asp:ListView ID="lvsemester" runat="server" OnPagePropertiesChanging="lvsemester_PagePropertiesChanging">
                                                                        <LayoutTemplate>
                                                                            <div class="sub-heading">
                                                                                <h5>Student List</h5>
                                                                            </div>
                                                                            <%--                         <div class="row mb-1">
                                                                                    <div class="col-lg-2 col-md-6 offset-lg-7">
                                                                                        <button type="button" class="btn btn-outline-primary float-lg-right saveAsExcel">Export Excel</button>
                                                                                    </div>

                                                                                    <div class="col-lg-3 col-md-6">
                                                                                        <div class="input-group sea-rch">
                                                                                            <input type="text" id="FilterData" class="form-control" placeholder="Search" />
                                                                                            <asp:TextBox ID="txtSearchSem" runat="server" OnTextChanged="txtSearchSem_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                                            <div class="input-group-addon">
                                                                                                <i class="fa fa-search"></i>
                                                                                            </div>
                                                                                        </div>

                                                                                    </div>
                                                                                </div>--%>
                                                                            <div class="table table-responsive">
                                                                                <%--   <table class="table table-striped table-bordered nowrap" style="width: 100%" id="mytable_int2">--%>
                                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="mytable_int2">
                                                                                    <thead class="bg-light-blue">
                                                                                        <tr>
                                                                                            <td style="text-align: center">
                                                                                                <asp:CheckBox ID="cheadreg" runat="server" OnClick="checkAllCheckboxsem(this)" />
                                                                                            </td>
                                                                                            <td>SrNo.</td>
                                                                                            <th>Reg. Id</th>

                                                                                            <th>Receipt No.</th>
                                                                                            <th>Payment For</th>
                                                                                            <th>Payment Mode</th>
                                                                                            <th style="white-space: normal !important;">Name</th>
                                                                                            <th>Bank Name &nbsp;&nbsp;</th>
                                                                                            <th>View</th>
                                                                                            <th>Amount &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</th>
                                                                                            <th>Payment Date</th>
                                                                                            <th>Status</th>
                                                                                            <th>Remark</th>
                                                                                            <th>NicNo.</th>
                                                                                            <th>Semester</th>
                                                                                            <th>Email</th>
                                                                                            <th>Mobile</th>
                                                                                            <th>Modified By</th>
                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tbody>
                                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                                    </tbody>
                                                                                </table>
                                                                            </div>

                                                                            <div class="col-12 pt-1 pb-2" id="Tfoot1" runat="server">
                                                                                <div class="float-right">
                                                                                    <asp:DataPager ID="DataPager1" runat="server" PagedControlID="lvsemester" PageSize="1000">
                                                                                        <Fields>
                                                                                            <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="false" ShowPreviousPageButton="true"
                                                                                                ShowNextPageButton="false" />
                                                                                            <asp:NumericPagerField ButtonType="Link" />
                                                                                            <asp:NextPreviousPagerField ButtonType="Link" ShowNextPageButton="true" ShowLastPageButton="false" ShowPreviousPageButton="false" />
                                                                                        </Fields>
                                                                                    </asp:DataPager>
                                                                                </div>
                                                                            </div>
                                                                            <%--   Added By Abhijit Naik--%>
                                                                            <%--   <div class="col-12 pt-1 pb-2" id="div3" runat="server">
                                                                                <div class="float-right">
                                                                                    <asp:DataPager ID="DataPagerSemBtn" runat="server" PagedControlID="lvsemester" PageSize="1000">
                                                                                        <Fields>
                                                                                            <asp:TemplatePagerField>
                                                                                                <PagerTemplate>
                                                                                                    <asp:LinkButton runat="server" ID="lnkPreviousSem" OnClick="lnkPreviousSem_Click" Text="Prevoius"></asp:LinkButton>
                                                                                                    <asp:LinkButton runat="server"  ID="lnkNextSem" OnClick="lnkNextSem_Click" Text="Next"></asp:LinkButton>
                                                                                                </PagerTemplate>
                                                                                            </asp:TemplatePagerField>
                                                                                        </Fields>
                                                                                    </asp:DataPager>
                                                                                </div>

                                                                                  <div class="float-left">
                                                                                    <asp:datapager id="datapagerSem" runat="server" pagedcontrolid="lvsemester" pagesize="1000">
                                                                                        <fields>
                                                                                            <asp:templatepagerfield>
                                                                                                <pagertemplate>
                                                                                                    <asp:label ID="Label2" runat="server"  text="Total Record :"></asp:label>
                                                                                                    <asp:label runat="server" id="lbltotalcountSem" ></asp:label>
                                                                                                </pagertemplate>
                                                                                            </asp:templatepagerfield>
                                                                                        </fields>
                                                                                    </asp:datapager>
                                                                                </div>
                                                                            </div>--%>
                                                                        </LayoutTemplate>

                                                                        <ItemTemplate>
                                                                            <tr>
                                                                                <td style="text-align: center">
                                                                                    <asp:CheckBox ID="chreg" runat="server" ToolTip='<%# Eval("IDNO")%>' />
                                                                                    <asp:Label ID="lblDcrTempNo" Text='<%# Eval("TEMP_DCR_NO") %>' ToolTip='<%# Eval("IDNO") %>' runat="server" Visible="false"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <%# Container.DataItemIndex + 1 %>                                                                             
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblsemusername" Text='<%# Eval("USERNAME") %>' ToolTip='<%# Eval("USERNAME") %>' runat="server"></asp:Label>
                                                                                    <asp:HiddenField ID="hfdsemdcr" Value='<%# Eval("DCR_NO") %>' runat="server" />
                                                                                    <asp:HiddenField ID="hdfsemregno" Value='<%# Eval("USERNAME") %>' runat="server" />
                                                                                </td>

                                                                                <td>
                                                                                    <asp:Label ID="lblrec" Text='<%# Eval("REC_NO") %>' runat="server"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblreceipttype" Text='<%# Eval("PAY_SERVICE_TYPE_NAME") %>' runat="server"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblpaymentmode" Text='<%# Eval("PAYMENTMODE") %>' runat="server"></asp:Label>
                                                                                </td>
                                                                                <td style="white-space: normal !important;">
                                                                                    <asp:Label ID="lblsemname" Text='<%# Eval("NAME") %>' ToolTip='<%# Eval("NAME") %>' runat="server"></asp:Label>
                                                                                </td>

                                                                                <td>
                                                                                    <asp:DropDownList ID="ddlbank" runat="server" AppendDataBoundItems="true" CssClass="form-group">
                                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                                    </asp:DropDownList>

                                                                                    <asp:Label ID="lblsembank" Text='<%# Eval("BANKNAME") %>' ToolTip='<%# Eval("BANKNAME") %>' runat="server" Visible="false"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <%--<asp:Image ID="ImageViewer" runat="server" Width="100%" Height="500px"/>--%>
                                                                                    <asp:LinkButton ID="lnksem" runat="server" OnClick="lnksem_Click" CommandArgument='<%# Eval("DOC_FILENAME") %>' CommandName='<%# Eval("TEMP_DCR_NO") %>' ToolTip='<%# Eval("IDNO") %>' Visible="false"><image style="height:35px" src="../IMAGES/viewdetail.png"></image></asp:LinkButton>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtsemamount" runat="server" CssClass="form-control" Text='<% #Eval("AMOUNT")%>' Style="padding: .375rem .25rem;"></asp:TextBox>
                                                                                    <asp:HiddenField ID="hdnDocno" runat="server" Value='<%# Eval("USERNO")  %>' />
                                                                                    <asp:HiddenField ID="hdnDoc" runat="server" Value='<%# Eval("CHALLAN_COPY") %>' />
                                                                                    <asp:HiddenField ID="hdfpaymod" runat="server" Value='<%# Eval("FEES_TYPE") %>' />
                                                                                    <asp:HiddenField ID="hdfintake" runat="server" Value='<%# Eval("BATCHNAME") %>' />
                                                                                    <asp:HiddenField ID="hdfdegree" runat="server" Value='<%# Eval("DEGREENAME") %>' />

                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtsemdate" onchange="return RestrictDate(this)" runat="server" CssClass="form-control" Text='<%# Eval("REC_DT") %>'></asp:TextBox>
                                                                                    <ajaxToolKit:CalendarExtender ID="ceStartDate" runat="server" Format="dd/MM/yyyy"
                                                                                        TargetControlID="txtsemdate" PopupButtonID="dvcal1" OnClientDateSelectionChanged="checkDate" />

                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="ddlstatussem" runat="server" CssClass="form-group" AppendDataBoundItems="True" Enabled='<% #Eval("STATUS").ToString() == "1" ? false : Eval("STATUS").ToString() == "3" ? false : true%>'
                                                                                        OnSelectedIndexChanged="ddlstatussem_SelectedIndexChanged" onchange="return Enable_Disable_Amount(this,'txtsemamount');">
                                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Approved</asp:ListItem>
                                                                                        <asp:ListItem Value="2">Pending</asp:ListItem>
                                                                                        <asp:ListItem Value="3">Reject</asp:ListItem>
                                                                                    </asp:DropDownList>

                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtsemremark" runat="server" CssClass="form-control" Text='<%# Eval("REMARK") %>'></asp:TextBox>
                                                                                    <asp:Label ID="lblsemstatus" runat="server" Text='<% #Eval("STATUS")%>' ToolTip='<% #Eval("STATUS")%>' Visible="false"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <%# Eval("NICNO") %>
                                                                                </td>
                                                                                <td>
                                                                                    <%#Eval("SEMESTERNAME") %>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblsememail" Text='<%# Eval("EMAILID") %>' ToolTip='<%# Eval("EMAILID") %>' runat="server"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblsemmobile" Text='<%# Eval("MOBILENO") %>' ToolTip='<%# Eval("MOBILENO") %>' runat="server"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <%# Eval("UA_FULLNAME") %>
                                                                                </td>
                                                                            </tr>

                                                                        </ItemTemplate>



                                                                    </asp:ListView>
                                                                </asp:Panel>
                                                            </div>
                                                            <div class="col-md-12 col-sm-12 col-12">
                                                                <asp:Panel ID="Panel4" runat="server" Visible="false">
                                                                    <asp:ListView ID="LvSemesterCount" runat="server">
                                                                        <LayoutTemplate>
                                                                            <div class="sub-heading">
                                                                                <h5>Student Count List For Semester</h5>
                                                                            </div>
                                                                            <div class="table table-responsive">
                                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%;">
                                                                                    <thead class="bg-light-blue">
                                                                                        <tr>
                                                                                            <th>SrNo.</th>
                                                                                            <th>Type</th>
                                                                                            <th>Academic Session</th>
                                                                                            <th>Semester</th>
                                                                                            <th>Study Level</th>
                                                                                            <th>Total Confirmed</th>
                                                                                            <th>Total Pending</th>
                                                                                            <th>Total Rejected</th>

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
                                                                                    <%# Container.DataItemIndex + 1 %>                                                                             
                                                                                </td>
                                                                                <td>OFFLINE</td>
                                                                                <td><%# Eval("SESSION_NAME") %></td>
                                                                                <td><%# Eval("SEMESTERNAME") %></td>
                                                                                <td><%# Eval("UA_SECTIONNAME")%></td>
                                                                                <td><%# Eval("TOTALACONFIRMEDAPPLI") %></td>
                                                                                <td><%# Eval("TOTALAPENDINGAPPLI") %></td>
                                                                                <td><%# Eval("TOTALAREJECTAPPLI") %></td>
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
                                    <div id="divroyalty" runat="server" visible="false" role="tabpanel" aria-labelledby="ERPAdmission-tab">
                                        <div>
                                            <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="updroyalty"
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
                                        <asp:UpdatePanel ID="updroyalty" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div class="row">
                                                    <div class="col-md-12 col-sm-12 col-12">
                                                        <%-- <div class="sub-heading mt-3">
                                                        <h5>Online Admission</h5>
                                                    </div>--%>
                                                        <div class="box-body">
                                                            <div class="col-12">
                                                                <div class="row">
                                                                    <div class="form-group col-12" runat="server" id="electroyalty" visible="true">
                                                                        <asp:RadioButtonList ID="rdoselectroyalty" runat="server"
                                                                            RepeatDirection="Horizontal" AutoPostBack="True"
                                                                            OnSelectedIndexChanged="rdoselectroyalty_SelectedIndexChanged">
                                                                            <asp:ListItem Value="1">Offline &nbsp;&nbsp;&nbsp;&nbsp;
                                                                            </asp:ListItem>
                                                                            <asp:ListItem Value="2">Online  &nbsp;&nbsp;&nbsp;&nbsp;                               
                                                                            </asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </div>
                                                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divroyastudy" visible="false">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Study Level</label>
                                                                            <%--<asp:Label ID="Label5" runat="server" Font-Bold="true"></asp:Label>--%>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlstyduroyalty" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server"
                                                                            ControlToValidate="ddlstyduroyalty" Display="None"
                                                                            ErrorMessage="Please Select Study Level" InitialValue="0" SetFocusOnError="true"
                                                                            ValidationGroup="ShowRoyal"></asp:RequiredFieldValidator>
                                                                    </div>

                                                                    <div id="intakeoneroya" runat="server" class="form-group col-lg-3 col-md-6 col-12" visible="false">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Intake</label>
                                                                            <%--<asp:Label ID="Label6" runat="server" Font-Bold="true"></asp:Label>--%>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlintakeroyalty" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server"
                                                                            ControlToValidate="ddlintakeroyalty" Display="None"
                                                                            ErrorMessage="Please Select Intake" InitialValue="0" SetFocusOnError="true"
                                                                            ValidationGroup="ShowRoyal"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div id="intaketworoya" runat="server" class="form-group col-lg-3 col-md-6 col-12" visible="false">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Intake</label>
                                                                            <%--<asp:Label ID="Label7" runat="server" Font-Bold="true"></asp:Label>--%>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlintaketworoyalty" runat="server" ValidationGroup="ShowRoyal" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <%-- <asp:TextBox ID="txtorderid" runat="server" TabIndex="2" CssClass="form-control"
                                                                        ToolTip="Please Enter Order Id" />--%>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" InitialValue="0"
                                                                            ControlToValidate="ddlintaketworoyalty" Display="None"
                                                                            ErrorMessage="Please Select Intake" SetFocusOnError="true"
                                                                            ValidationGroup="ShowRoyal"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="statusroya" runat="server" visible="false">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Payment Status</label>
                                                                            <%--<asp:Label ID="Label8" runat="server" Font-Bold="true"></asp:Label>--%>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlpaymentstsroya" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                            <asp:ListItem Value="1">Completed</asp:ListItem>
                                                                            <asp:ListItem Value="2">Incompleted</asp:ListItem>
                                                                            <asp:ListItem Value="3">Rejected</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server"
                                                                            ControlToValidate="ddlpaymentstsroya" Display="None"
                                                                            ErrorMessage="Please Select Payment Status" InitialValue="0" SetFocusOnError="true"
                                                                            ValidationGroup="ShowRoyal"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="bankroya" runat="server" visible="false">
                                                                        <div class="label-dynamic">
                                                                            <sup></sup>
                                                                            <label>Bank</label>
                                                                            <%--<asp:Label ID="Label9" runat="server" Font-Bold="true" Text="Bank"></asp:Label>--%>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlbankroyal" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:Label ID="lblbankroyal" Text='<%# Eval("BANKNAME") %>' ToolTip='<%# Eval("BANKNAME") %>' runat="server" Visible="false"></asp:Label>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="facuroya" visible="false">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Faculty/School Name</label>
                                                                            <%--<asp:Label ID="Label11" runat="server" Font-Bold="true"></asp:Label>--%>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlfacultyroyal" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server"
                                                                            ControlToValidate="ddlfacultyroyal" Display="None"
                                                                            ErrorMessage="Please Select Faculty/School Name" InitialValue="0" SetFocusOnError="true"
                                                                            ValidationGroup="ShowRoyal"></asp:RequiredFieldValidator>
                                                                    </div>


                                                                </div>
                                                            </div>
                                                            <div class="col-12 btn-footer">
                                                                <asp:Button ID="btnshowroyaloff" runat="Server" Text="Show" ValidationGroup="ShowRoyal" OnClick="btnshowroyaloff_Click"
                                                                    CssClass="btn btn-outline-info" Visible="false" />
                                                                <asp:Button ID="btnshowroyalon" runat="Server" Text="Show" ValidationGroup="ShowRoyal" OnClick="btnshowroyalon_Click"
                                                                    CssClass="btn btn-outline-info" Visible="false" />
                                                                <asp:Button ID="btnsubroya" runat="Server" Text="Submit"
                                                                    CssClass="btn btn-outline-info" Visible="false" OnClick="btnsubroya_Click" />
                                                                <asp:Button ID="Button4" runat="Server" Text="Submit"
                                                                    CssClass="btn btn-outline-info" Visible="false" />
                                                                <asp:Button ID="btnroyacancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                                                    CssClass="btn btn-outline-danger" OnClick="btnroyacancel_Click" Visible="false" />
                                                                <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                                    ShowSummary="false" ValidationGroup="ShowRoyal" />
                                                            </div>
                                                            <div class="col-md-12">
                                                                <asp:Panel ID="PanelRoya" runat="server" Visible="false">
                                                                    <asp:ListView ID="LvRoya" runat="server">
                                                                        <LayoutTemplate>
                                                                            <div class="sub-heading">
                                                                                <h5>Student List</h5>
                                                                            </div>
                                                                            <div class="table table-responsive">
                                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%" id="mytable_int2">
                                                                                    <thead class="bg-light-blue">
                                                                                        <tr>
                                                                                            <td style="text-align: center">
                                                                                                <asp:CheckBox ID="cheadoya" runat="server" OnClick="checkAllCheckboxroya(this)" />
                                                                                            </td>
                                                                                            <td>SrNo.</td>
                                                                                            <th>Application Id</th>
                                                                                            <th>Receipt No.</th>
                                                                                            <th style="white-space: normal !important;">Name</th>
                                                                                            <th>Bank Name &nbsp;&nbsp;</th>
                                                                                            <th>View</th>
                                                                                            <th>Amount &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</th>
                                                                                            <th>Payment Date</th>
                                                                                            <th>Status</th>
                                                                                            <th>Remark</th>
                                                                                            <th>NicNo.</th>
                                                                                            <th>Email</th>
                                                                                            <th>Mobile</th>
                                                                                            <th>Modified By</th>
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
                                                                                <td style="text-align: center">
                                                                                    <asp:CheckBox ID="chroya" runat="server" ToolTip='<%# Eval("IDNO")%>' />
                                                                                    <asp:Label ID="lblDcrTempNo" Text='<%# Eval("TEMP_DCR_NO") %>' ToolTip='<%# Eval("IDNO")  %>' runat="server" Visible="false"></asp:Label>
                                                                                    <asp:HiddenField ID="hdfdcr" Value='<%# Eval("DCR_NO")%>' runat="server" />
                                                                                    <asp:HiddenField ID="hdfusername" Value='<%# Eval("USERNAME")%>' runat="server" />
                                                                                </td>
                                                                                <td>
                                                                                    <%# Container.DataItemIndex + 1 %>                                                                             
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lbloyausername" Text='<%# Eval("USERNAME") %>' ToolTip='<%# Eval("USERNAME") %>' runat="server"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblrecnoroya" Text='<%# Eval("REC_NO") %>' runat="server"></asp:Label>
                                                                                </td>
                                                                                <td style="white-space: normal !important;">
                                                                                    <asp:Label ID="lbloyaname" Text='<%# Eval("NAME") %>' ToolTip='<%# Eval("NAME") %>' runat="server"></asp:Label>
                                                                                </td>

                                                                                <td>
                                                                                    <asp:DropDownList ID="ddlbank" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                    <asp:Label ID="lblbank" Text='<%# Eval("BANKNAME") %>' ToolTip='<%# Eval("BANKNAME") %>' runat="server" Visible="false"></asp:Label>
                                                                                    <asp:Label ID="lblemail" Text='<%# Eval("EMAILID") %>' ToolTip='<%# Eval("EMAILID") %>' runat="server" Visible="false"></asp:Label>

                                                                                </td>
                                                                                <td>
                                                                                    <%--<asp:Image ID="ImageViewer" runat="server" Width="100%" Height="500px"/>--%>
                                                                                    <asp:LinkButton ID="lnkroya" runat="server" OnClick="lnkroya_Click" CommandArgument='<%# Eval("DOC_FILENAME") %>' CommandName='<%# Eval("TEMP_DCR_NO") %>' ToolTip='<%# Eval("IDNO") %>' Visible="false"><image style="height:35px" src="../IMAGES/viewdetail.png"></image></asp:LinkButton>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtoyaamount" runat="server" CssClass="form-control" Text='<% #Eval("AMOUNT")%>'></asp:TextBox>
                                                                                    <asp:HiddenField ID="hdnDocno" runat="server" Value='<%# Eval("USERNO")  %>' />
                                                                                    <asp:HiddenField ID="hdnDoc" runat="server" Value='<%# Eval("CHALLAN_COPY") %>' />
                                                                                    <asp:HiddenField ID="hdfpaymod" runat="server" Value='<%# Eval("FEES_TYPE") %>' />
                                                                                    <asp:HiddenField ID="hdfintake" runat="server" Value='<%# Eval("BATCHNAME") %>' />
                                                                                    <asp:HiddenField ID="hdfdegree" runat="server" Value='<%# Eval("DEGREENAME") %>' />

                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtoyadate" onchange="return RestrictDate(this)" runat="server" CssClass="form-control" Text='<%# Eval("REC_DT") %>'></asp:TextBox>
                                                                                    <ajaxToolKit:CalendarExtender ID="ceStartDate" runat="server" Format="dd/MM/yyyy"
                                                                                        TargetControlID="txtoyadate" PopupButtonID="dvcal1" OnClientDateSelectionChanged="checkDate" />

                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="ddloyastatus" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True"
                                                                                        OnSelectedIndexChanged="ddloyastatus_SelectedIndexChanged" onchange="return Enable_Disable_Amount(this,'txtoyaamount');">
                                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Approved</asp:ListItem>
                                                                                        <asp:ListItem Value="2">Pending</asp:ListItem>
                                                                                        <asp:ListItem Value="3">Reject</asp:ListItem>
                                                                                    </asp:DropDownList>

                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtoyaremark" runat="server" CssClass="form-control" Text='<%# Eval("REMARK") %>'></asp:TextBox>
                                                                                    <asp:Label ID="lbloyastatus" runat="server" Text='<% #Eval("STATUS")%>' ToolTip='<% #Eval("STATUS")%>' Visible="false"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <%# Eval("NIC") %>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lbloyaemail" Text='<%# Eval("EMAILID") %>' ToolTip='<%# Eval("EMAILID") %>' runat="server"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lbloyamobile" Text='<%# Eval("MOBILENO") %>' ToolTip='<%# Eval("MOBILENO") %>' runat="server"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <%# Eval("UA_FULLNAME") %>
                                                                                </td>
                                                                            </tr>

                                                                        </ItemTemplate>

                                                                    </asp:ListView>
                                                                </asp:Panel>
                                                            </div>
                                                            <div class="col-md-12 col-sm-12 col-12">
                                                                <asp:Panel ID="Panelroyacout" runat="server" Visible="false">
                                                                    <asp:ListView ID="Lvroyacount" runat="server">
                                                                        <LayoutTemplate>
                                                                            <div class="sub-heading">
                                                                                <h5>Student Count List For PHD</h5>
                                                                            </div>
                                                                            <div class="table table-responsive">
                                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%;">
                                                                                    <thead class="bg-light-blue">
                                                                                        <tr>
                                                                                            <th>SrNo.</th>
                                                                                            <th>Type</th>
                                                                                            <th>Intake</th>
                                                                                            <th>Study Level</th>
                                                                                            <th>Total Confirmed</th>
                                                                                            <th>Total Pending</th>
                                                                                            <th>Total Rejected</th>

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
                                                                                    <%# Container.DataItemIndex + 1 %>                                                                             
                                                                                </td>
                                                                                <td><%# Eval("TYPE") %></td>
                                                                                <td><%# Eval("BATCHNAME") %></td>
                                                                                <td><%# Eval("UA_SECTIONNAME")%></td>
                                                                                <td><%# Eval("TOTALACONFIRMEDAPPLI") %></td>
                                                                                <td><%# Eval("TOTALAPENDINGAPPLI") %></td>
                                                                                <td><%# Eval("TOTALAREJECTAPPLI") %></td>
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
                                        </asp:UpdatePanel>
                                    </div>

                                    <div id="divLoanScheme" runat="server" visible="false" role="tabpanel" aria-labelledby="ERPAdmission-tab">
                                        <div>
                                            <asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="updLoanScheme"
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
                                        <asp:UpdatePanel ID="updLoanScheme" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div class="row">
                                                    <div class="col-md-12 col-sm-12 col-12">
                                                        <%-- <div class="sub-heading mt-3">
                                                        <h5>Online Admission</h5>
                                                    </div>--%>
                                                        <div class="box-body">
                                                            <div class="col-12">
                                                                <div class="row">
                                                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Study Level</label>
                                                                            <%--<asp:Label ID="Label5" runat="server" Font-Bold="true"></asp:Label>--%>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlLoanSchemeStudyLevel" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server"
                                                                            ControlToValidate="ddlstyduroyalty" Display="None"
                                                                            ErrorMessage="Please Select Study Level" InitialValue="0" SetFocusOnError="true"
                                                                            ValidationGroup="ShowRoyal"></asp:RequiredFieldValidator>
                                                                    </div>

                                                                    <div id="Div3" runat="server" class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Intake</label>
                                                                            <%--<asp:Label ID="Label6" runat="server" Font-Bold="true"></asp:Label>--%>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlLoanSchemeIntake" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server"
                                                                            ControlToValidate="ddlintakeroyalty" Display="None"
                                                                            ErrorMessage="Please Select Intake" InitialValue="0" SetFocusOnError="true"
                                                                            ValidationGroup="ShowRoyal"></asp:RequiredFieldValidator>
                                                                    </div>

                                                                </div>
                                                            </div>
                                                            <div class="col-12 btn-footer">
                                                                <asp:Button ID="btnShowLoanScheme" runat="Server" Text="Show" ValidationGroup="ShowLoan" OnClick="btnShowLoanScheme_Click"
                                                                    CssClass="btn btn-outline-info" />
                                                                <asp:Button ID="btnSubmitLoanScheme" runat="Server" Text="Submit" OnClick="btnSubmitLoanScheme_Click"
                                                                    CssClass="btn btn-outline-info" Visible="false" />
                                                                <asp:Button ID="btnCancelLoanScheme" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false" OnClick="btnCancelLoanScheme_Click"
                                                                    CssClass="btn btn-outline-danger" />
                                                                <asp:ValidationSummary ID="ValidationSummary4" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                                    ShowSummary="false" ValidationGroup="ShowRoyal" />
                                                            </div>
                                                            <div class="col-md-12">
                                                                <asp:Panel ID="PanelLoan" runat="server" Visible="false">
                                                                    <asp:ListView ID="LvLoanScheme" runat="server">
                                                                        <LayoutTemplate>
                                                                            <div class="sub-heading">
                                                                                <h5>Student List</h5>
                                                                            </div>
                                                                            <div class="table table-responsive">
                                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%" id="mytable_int2">
                                                                                    <thead class="bg-light-blue">
                                                                                        <tr>
                                                                                            <td style="text-align: center">
                                                                                                <asp:CheckBox ID="cheadoya" runat="server" OnClick="checkAllCheckboxLoan(this)" />
                                                                                            </td>
                                                                                            <td>SrNo.</td>
                                                                                            <th>Application Id</th>
                                                                                            <th style="display: none">Receipt No.</th>
                                                                                            <th style="white-space: normal !important;">Name</th>
                                                                                            <th style="display: none">Bank Name &nbsp;&nbsp;</th>
                                                                                            <th style="display: none">View</th>
                                                                                            <th>Amount &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</th>
                                                                                            <th>Consession &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</th>
                                                                                            <th>Net Payable &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</th>
                                                                                            <th>Payment Date</th>
                                                                                            <th>Status</th>
                                                                                            <th>Remark</th>
                                                                                            <th>NicNo.</th>
                                                                                            <th>Email</th>
                                                                                            <th>Mobile</th>
                                                                                            <th>Modified By</th>
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
                                                                                <td style="text-align: center">
                                                                                    <asp:CheckBox ID="chrLoan" runat="server" ToolTip='<%# Eval("IDNO")%>' />
                                                                                    <asp:Label ID="lblDcrTempNo" Text='<%# Eval("TEMP_DCR_NO") %>' ToolTip='<%# Eval("IDNO")  %>' runat="server" Visible="false"></asp:Label>
                                                                                    <asp:HiddenField ID="hdfdcr" Value='<%# Eval("DCR_NO")%>' runat="server" />
                                                                                    <asp:HiddenField ID="hdfusername" Value='<%# Eval("USERNAME")%>' runat="server" />
                                                                                </td>
                                                                                <td>
                                                                                    <%# Container.DataItemIndex + 1 %>                                                                             
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblLoanusername" Text='<%# Eval("USERNAME") %>' ToolTip='<%# Eval("USERNAME") %>' runat="server"></asp:Label>
                                                                                </td>
                                                                                <td style="display: none">
                                                                                    <asp:Label ID="lblrecnoroya" Text='<%# Eval("REC_NO") %>' runat="server"></asp:Label>
                                                                                </td>
                                                                                <td style="white-space: normal !important;">
                                                                                    <asp:Label ID="lbloyaname" Text='<%# Eval("NAME") %>' ToolTip='<%# Eval("NAME") %>' runat="server"></asp:Label>
                                                                                </td>

                                                                                <td style="display: none">
                                                                                    <asp:DropDownList ID="ddlbank" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" Visible="false">
                                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                    <asp:Label ID="lblbank" Text='<%# Eval("BANKNAME") %>' ToolTip='<%# Eval("BANKNAME") %>' runat="server" Visible="false"></asp:Label>
                                                                                    <asp:Label ID="lblemail" Text='<%# Eval("EMAILID") %>' ToolTip='<%# Eval("EMAILID") %>' runat="server" Visible="false"></asp:Label>

                                                                                </td>
                                                                                <td style="display: none">
                                                                                    <%--<asp:Image ID="ImageViewer" runat="server" Width="100%" Height="500px"/>--%>
                                                                                    <asp:LinkButton ID="lnkroya" runat="server" CommandArgument='<%# Eval("DOC_FILENAME") %>' CommandName='<%# Eval("TEMP_DCR_NO") %>' ToolTip='<%# Eval("IDNO") %>' Visible="false"><image style="height:35px" src="../IMAGES/viewdetail.png"></image></asp:LinkButton>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtTotalDemand" runat="server" CssClass="form-control" Text='<% #Eval("TOTAL_DEMAND")%>'></asp:TextBox>
                                                                                    <asp:HiddenField ID="hdnDocno" runat="server" Value='<%# Eval("USERNO")  %>' />
                                                                                    <asp:HiddenField ID="hdnDoc" runat="server" Value='<%# Eval("CHALLAN_COPY") %>' />
                                                                                    <asp:HiddenField ID="hdfpaymod" runat="server" Value='<%# Eval("FEES_TYPE") %>' />
                                                                                    <asp:HiddenField ID="hdfintake" runat="server" Value='<%# Eval("BATCHNAME") %>' />
                                                                                    <asp:HiddenField ID="hdfdegree" runat="server" Value='<%# Eval("DEGREENAME") %>' />

                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtComnsession" runat="server" CssClass="form-control" Text='<% #Eval("DISCOUNT_FEES")%>' Enabled="false"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtTotal_payble" runat="server" CssClass="form-control" Text='<% #Eval("TOTAL_PAYBLE")%>' Enabled="false"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtLoandate" onchange="return RestrictDate(this)" runat="server" CssClass="form-control" Text='<%# Eval("REC_DT") %>'></asp:TextBox>
                                                                                    <ajaxToolKit:CalendarExtender ID="ceStartDate" runat="server" Format="dd/MM/yyyy"
                                                                                        TargetControlID="txtLoandate" PopupButtonID="dvcal1" OnClientDateSelectionChanged="checkDate" />

                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="ddlLoanstatus" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True"
                                                                                        OnSelectedIndexChanged="ddloyastatus_SelectedIndexChanged" onchange="return Enable_Disable_Amount(this,'txtTotalDemand');">
                                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Approved</asp:ListItem>
                                                                                        <asp:ListItem Value="2">Pending</asp:ListItem>
                                                                                        <asp:ListItem Value="3">Reject</asp:ListItem>
                                                                                    </asp:DropDownList>

                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtLoanremark" runat="server" CssClass="form-control" Text='<%# Eval("REMARK") %>'></asp:TextBox>
                                                                                    <asp:Label ID="lblLoanstatus" runat="server" Text='<% #Eval("STATUS")%>' ToolTip='<% #Eval("STATUS")%>' Visible="false"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <%# Eval("NIC") %>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblLoanemail" Text='<%# Eval("EMAILID") %>' ToolTip='<%# Eval("EMAILID") %>' runat="server"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lbloyamobile" Text='<%# Eval("MOBILENO") %>' ToolTip='<%# Eval("MOBILENO") %>' runat="server"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <%# Eval("UA_FULLNAME") %>
                                                                                </td>
                                                                            </tr>

                                                                        </ItemTemplate>

                                                                    </asp:ListView>
                                                                </asp:Panel>
                                                            </div>
                                                            <div class="col-md-12 col-sm-12 col-12">
                                                                <asp:Panel ID="Panel6" runat="server" Visible="false">
                                                                    <asp:ListView ID="ListView2" runat="server">
                                                                        <LayoutTemplate>
                                                                            <div class="sub-heading">
                                                                                <h5>Student Count List For Royalt</h5>
                                                                            </div>
                                                                            <div class="table table-responsive">
                                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%;">
                                                                                    <thead class="bg-light-blue">
                                                                                        <tr>
                                                                                            <th>SrNo.</th>
                                                                                            <th>Type</th>
                                                                                            <th>Intake</th>
                                                                                            <th>Study Level</th>
                                                                                            <th>Total Confirmed</th>
                                                                                            <th>Total Pending</th>
                                                                                            <th>Total Rejected</th>

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
                                                                                    <%# Container.DataItemIndex + 1 %>                                                                             
                                                                                </td>
                                                                                <td><%# Eval("TYPE") %></td>
                                                                                <td><%# Eval("BATCHNAME") %></td>
                                                                                <td><%# Eval("UA_SECTIONNAME")%></td>
                                                                                <td><%# Eval("TOTALACONFIRMEDAPPLI") %></td>
                                                                                <td><%# Eval("TOTALAPENDINGAPPLI") %></td>
                                                                                <td><%# Eval("TOTALAREJECTAPPLI") %></td>
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
                                        </asp:UpdatePanel>
                                    </div>
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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="myModal11" class="modal fade" role="dialog">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content" style="margin-top: -25px">
                        <div class="modal-body">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" style="margin-top: -18px">x</button>
                            </div>
                            <asp:Image ID="image" runat="server" Width="100%" Height="500px" />
                            <asp:Literal ID="Literal1" runat="server" />

                            <%--.<iframe id="iframe2" runat="server" frameborder="0" width="100%" height="800px"></iframe>--%>
                            <%--<iframe id="iframe1" runat="server" src="../FILEUPLOAD/Transcript Report.pdf" frameborder="0" width="100%" height="547px"></iframe>--%>

                            <div class="modal-footer" style="height: 0px">
                                <button type="button" style="margin-top: -10px" class="btn btn-default" data-dismiss="modal">Close</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="updModelPopup" runat="server">
        <ContentTemplate>
            <div id="myModal22" class="modal fade" role="dialog" data-backdrop="static">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content" style="margin-top: -25px">
                        <div class="modal-body">
                            <div class="modal-header">
                                <asp:LinkButton ID="lnkClose" runat="server" CssClass="close" Style="margin-top: -18px" OnClick="lnkClose_Click">x</asp:LinkButton>
                                <%--<button type="button" class="close" data-dismiss="modal" style="margin-top: -18px">x</button>--%>
                            </div>
                            <asp:Image ID="ImageViewer" runat="server" Width="100%" Height="500px" Visible="false" />
                            <asp:Literal ID="ltEmbed" runat="server" Visible="false" />
                            <div id="imageViewerContainer" runat="server" visible="false"></div>
                            <asp:HiddenField ID="hdfImagePath" runat="server" />
                            <iframe style="width: 100%; height: 500px;" id="irm1" src="~/PopUp.aspx" runat="server"></iframe>
                            <div class="modal-footer" style="height: 0px">
                                <asp:LinkButton ID="lnkCloseModel" runat="server" CssClass="btn btn-default" Style="margin-top: -10px" OnClick="lnkClose_Click">Close</asp:LinkButton>
                                <%--<button type="button" style="margin-top: -10px" class="btn btn-default" data-dismiss="modal">Close</button>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lnkClose" />
            <asp:PostBackTrigger ControlID="lnkCloseModel" />
        </Triggers>
    </asp:UpdatePanel>

    <div class="modal fade" id="ModelEmailPopup">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <!-- Modal Header -->
                <div class="modal-header">
                    <h4 class="modal-title">Send Mail</h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div>
                    <asp:UpdateProgress ID="UpdateProgress19" runat="server" AssociatedUpdatePanelID="updEmailSend"
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
                <asp:UpdatePanel ID="updEmailSend" runat="server">
                    <ContentTemplate>
                        <!-- Modal body -->
                        <div class="modal-body">

                            <div id="auto" runat="server">
                                <div class="form-group col-lg-12 col-md-12 col-12">
                                    <label>Subject</label>
                                    <asp:TextBox ID="txtEmailSubject" runat="server" CssClass="form-control">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" SetFocusOnError="True"
                                        ErrorMessage="Please Enter Subject" ControlToValidate="txtEmailSubject" Display="None"
                                        ValidationGroup="EmailSubmit" />
                                </div>
                                <div class="form-group col-lg-12 col-md-12 col-12">
                                    <label>Message</label>
                                    <asp:TextBox ID="txtEmailMessage" runat="server" CssClass="form-control" TextMode="MultiLine" MaxLength="350">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" SetFocusOnError="True"
                                        ErrorMessage="Please Enter Message" ControlToValidate="txtEmailMessage" Display="None"
                                        ValidationGroup="EmailSubmit" />
                                </div>
                                <div class="form-group col-lg-12 col-md-12 col-12">
                                    <label>Add Attachment</label>
                                    <asp:FileUpload ID="fuAttachFile" runat="server" CssClass="form-control" />
                                </div>

                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSend" OnClick="btnSend_Click" runat="server" Text="Send Email" CssClass="btn btn-outline-info" />
                                <asp:ValidationSummary ID="emailsummary" runat="server" ValidationGroup="EmailSubmit" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" />
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnSend" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

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
        $(document).ready(function () {
            var curect_file_path = $('#ctl00_ContentPlaceHolder1_hdfImagePath').val();
            $("#ctl00_ContentPlaceHolder1_imageViewerContainer").verySimpleImageViewer({
                imageSource: curect_file_path,
                frame: ['100%', '100%'],
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
                    frame: ['100%', '100%'],
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
    <script type="text/javascript">

        function checkAllCheckboxroya(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                var s = e.name.split("ctl00$ContentPlaceHolder1$LvRoya$ctrl");
                var b = 'ctl00$ContentPlaceHolder1$LvRoya$ctrl';
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

        function checkAllCheckboxLoan(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                var s = e.name.split("ctl00$ContentPlaceHolder1$PanelLoan$ctrl");
                var b = 'ctl00$ContentPlaceHolder1$PanelLoan$ctrl';
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
        function checkAllCheckbox(headchk) {
            debugger;
            var frm = document.forms[0]
            var Count=0;
            var Capacity=$("#ctl00_ContentPlaceHolder1_hdnCapacity").val();
           
            $("tr.classlvonlineadm").each(function () {
                debugger;
                if (headchk.checked == true)
                {
                    if(Number(Capacity)>Count)
                    {
                        $("#ctl00_ContentPlaceHolder1_lvonlineadm_ctrl" + Count + "_chkadm").prop('checked', true);
                    }               
                }
                else{
                    $("#ctl00_ContentPlaceHolder1_lvonlineadm_ctrl" + Count + "_chkadm").prop('checked', false);
                }
                Count++;
            });            
        }

        function checkAllCheckboxchkadm(headchk) {
            debugger;
            var Capacity=$("#ctl00_ContentPlaceHolder1_hdnCapacity").val();
            var Count=0;
            var Check=0;
            $("tr.classlvonlineadm").each(function () {
                debugger;
                if ($("#ctl00_ContentPlaceHolder1_lvonlineadm_ctrl" + Count + "_chkadm").prop('checked'))
                {
                    Check++;
                    if(Number(Capacity)<Check)
                    {
                        $("#ctl00_ContentPlaceHolder1_lvonlineadm_ctrl" + Count + "_chkadm").prop('checked', false);
                        alert("Exam Capacity Full or Not Define.");
                    }
                              
                }
                Count++; 
            });            
        }
       

    </script>
    <script type="text/javascript">

        function checkAllCheckboxsem(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                var s = e.name.split("ctl00$ContentPlaceHolder1$lvsemester$ctrl");
                var b = 'ctl00$ContentPlaceHolder1$lvsemester$ctrl';
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

        function checkAll(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                var s = e.name.split("ctl00$ContentPlaceHolder1$lvreg$ctrl");
                var b = 'ctl00$ContentPlaceHolder1$lvreg$ctrl';
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

    <%--<script type="text/javascript">
     $(document).ready(function () {
         $("#ctl00_ContentPlaceHolder1_btnSendBulkEmail").click(function () {
             $("#ModelEmailPopup").modal();

         });
     });

     var parameter = Sys.WebForms.PageRequestManager.getInstance();
     parameter.add_endRequest(function () {
         $(function () {
             $("#ctl00_ContentPlaceHolder1_btnSendBulkEmail").click(function () {
                 $("#ModelEmailPopup").modal();

             });
         });
     });
    </script>--%>
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
            XLSX.writeFile(workbook, "Data.xlsx");
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
        function Enable_Disable_Amount(id,txtid)
        {
            var List = $(id).closest("table");
            if(id.value == 1)
            {
                var td = $("td", $(id).closest("tr")); 
                $("[id*="+txtid+"]", td).attr("disabled", "disabled");
            } 
            else {
                var td = $("td", $(id).closest("tr"));
                $("[id*="+txtid+"]", td).removeAttr("disabled"); 
            }
        }
    </script>

    <script type="text/javascript">
        function checkDate(sender, args) {
            // I change the < operator to >
            if (sender._selectedDate > new Date()) {
                alert("Unable to select future date !!!");
                sender._selectedDate = new Date();
                // set the date back to the current date
                sender._textbox.set_Value('')
            }

        }
    </script>

    <%-- <script>
        $('#ctl00_ContentPlaceHolder1_ddlIDChange').on('change',function() {
            debugger
            var selectedValue = $('#ctl00_ContentPlaceHolder1_ddlIDChange').val();
            
            if (selectedValue == 1) {
                $("#spStude").html("Application ID")
                $('#rdioregistrationOnline').visible = true
            }
            else {
                $("#spStude").html("Student ID")
                $('#rdioregistrationOnline').visible = false
                $('#rdioregistration').visible = true
            }
        });
    </script>--%>

    <%-- <script type="text/javascript">
         function RestrictDate(txt) {
             var today = new Date();
             var month = ('0' + (today.getMonth() + 1)).slice(-2);
             var day = ('0' + today.getDate()).slice(-2);
             var year = today.getFullYear();
             var date = day + '/' + month + '/' + year;
             if (date < txt.value) {
                 alert("Unable to select future date !!!");
                 txt.value = '';
             }
         }
    </script>--%>
</asp:Content>
