<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Student_Progression.aspx.cs" Inherits="Projects_Student_Progression" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
    <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>
    <style>
        .badge {
            display: inline-block;
            padding: 5px 10px 7px;
            border-radius: 15px;
            font-size: 12px;
            width: 100px;
            font-weight: 400;
        }

        .badge-warning {
            color: #fff !important;
        }
    </style>
    <style>
        .fa-plus {
            color: #17a2b8;
            border: 1px solid #17a2b8;
            border-radius: 50%;
            padding: 6px 8px;
        }

        .dataTables_scrollHeadInner {
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
    </style>
    <style>
        /*--======= toggle switch css added by gaurav 29072021 =======--*/
        .switch input[type=checkbox] {
            height: 0;
            width: 0;
            visibility: hidden;
        }

        .switch label {
            cursor: pointer;
            width: 82px;
            height: 34px;
            background: #dc3545;
            display: block;
            border-radius: 4px;
            position: relative;
            transition: 0.35s;
            -webkit-transition: 0.35s;
            -moz-user-select: none;
            -webkit-user-select: none;
        }

            .switch label:hover {
                background-color: #c82333;
            }

            .switch label:before {
                content: attr(data-off);
                position: absolute;
                right: 0;
                font-size: 16px;
                padding: 4px 8px;
                font-weight: 400;
                color: #fff;
                transition: 0.35s;
                -webkit-transition: 0.35s;
                -moz-user-select: none;
                -webkit-user-select: none;
            }

        .switch input:checked + label:before {
            content: attr(data-on);
            position: absolute;
            left: 0;
            font-size: 16px;
            padding: 4px 15px;
            font-weight: 400;
            color: #fff;
            transition: 0.35s;
            -webkit-transition: 0.35s;
            -moz-user-select: none;
            -webkit-user-select: none;
        }

        .switch label:after {
            content: '';
            position: absolute;
            top: 1.5px;
            left: 1.7px;
            width: 10.2px;
            height: 31.5px;
            background: #fff;
            border-radius: 2.5px;
            transition: 0.35s;
            -webkit-transition: 0.35s;
            -moz-user-select: none;
            -webkit-user-select: none;
        }

        .switch input:checked + label {
            background: #28a745;
            transition: 0.35s;
            -webkit-transition: 0.35s;
            -moz-user-select: none;
            -webkit-user-select: none;
        }

            .switch input:checked + label:hover {
                background: #218838;
            }

            .switch input:checked + label:after {
                transform: translateX(68px);
                transition: 0.35s;
                -webkit-transition: 0.35s;
                -moz-user-select: none;
                -webkit-user-select: none;
            }
    </style>
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
    <asp:HiddenField ID="hfdStat" runat="server" ClientIDMode="Static" />
    <%--  <asp:UpdatePanel ID="upd" runat="server">
        <ContentTemplate>--%>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="nav-tabs-custom mt-2 col-12" id="myTabContent">
                    <%-- <div class="box-body">
                    <div class="nav-tabs-custom">
                        <ul class="nav nav-tabs" role="tablist">
                            <li class="nav-item">
                                <a class="nav-link active" data-toggle="tab" href="#tab_1">Progression Rule</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_2">Rule Allocation</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_3">Bridging</a>
                            </li>
                        </ul>--%>
                    <ul class="nav nav-tabs dynamic-nav-tabs" role="tablist">
                        <li class="nav-item active" id="divlkprogression" runat="server">
                            <asp:LinkButton ID="lkprogression" runat="server" OnClick="lkprogression_Click" CssClass="nav-link" TabIndex="1">Progression Rule</asp:LinkButton></li>
                        <li class="nav-item" id="divlkallocation" runat="server">
                            <asp:LinkButton ID="lkallocation" runat="server" OnClick="lkallocation_Click" CssClass="nav-link" TabIndex="2">Rule Allocation</asp:LinkButton></li>
                        <li class="nav-item" id="divlkbrid" runat="server">
                            <asp:LinkButton ID="lkbrid" runat="server" OnClick="lkbrid_Click" CssClass="nav-link" TabIndex="3">Bridging Rule</asp:LinkButton></li>
                        <li class="nav-item" id="divlkallot" runat="server">
                            <asp:LinkButton ID="lkalot" runat="server" OnClick="lkalot_Click" CssClass="nav-link" TabIndex="4">Bridging Allot</asp:LinkButton></li>
                        <li class="nav-item" id="divlkbridresult" runat="server">
                            <asp:LinkButton ID="lkbridresult" runat="server" OnClick="lkbridresult_Click" CssClass="nav-link" TabIndex="4">Bridging Result</asp:LinkButton></li>
                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane fade show active" id="divprogress" role="tabpanel" runat="server" aria-labelledby="ALCourses-tab">
                            <div>
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updStudentProgression"
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
                            <asp:UpdatePanel ID="updStudentProgression" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="col-12 mt-3">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                     <asp:Label ID="lblrukename" runat="server" Font-Bold="true"></asp:Label>
                                                    <%--<label>Rule Name</label>--%>
                                                </div>
                                                <asp:TextBox ID="txtRuleName" runat="server" CssClass="form-control" onkeypress="allowAlphaNumericSpace(event)" TabIndex="1" ToolTip="Rule Name"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvPdpCode" runat="server" ControlToValidate="txtRuleName"
                                                    Display="None" ValidationGroup="submit"
                                                    ErrorMessage="Please Enter Rule Name." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                      <asp:Label ID="lblrulefac" runat="server" Font-Bold="true"></asp:Label>
                                                    <%--<label>Faculty/School Name</label>--%>
                                                </div>
                                                <asp:DropDownList ID="ddlFaculty" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="2"
                                                    AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlFaculty"
                                                    Display="None" ErrorMessage="Please Select Faculty." ValidationGroup="submit"
                                                    SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                      <asp:Label ID="lblrulestudy" runat="server" Font-Bold="true"></asp:Label>

                                                    <%--<label>Study Level</label>--%>
                                                </div>
                                                <asp:DropDownList ID="ddlStudyLevel" runat="server" CssClass="form-control" data-select2-enable="true"
                                                    AppendDataBoundItems="true" TabIndex="3" OnSelectedIndexChanged="ddlStudyLevel_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvStudyLevel" runat="server" ControlToValidate="ddlStudyLevel"
                                                    Display="None" ErrorMessage="Please Select Study Level." ValidationGroup="submit"
                                                    SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                      <asp:Label ID="lblStatus" runat="server" Font-Bold="true"></asp:Label>

                                                    <%--<label>Status</label>--%>
                                                    <%-- <asp:Label ID="lblStatus" runat="server" Font-Bold="true"></asp:Label>--%>
                                                </div>
                                                <div class="switch form-inline">
                                                    <input type="checkbox" id="switch" name="switch" class="switch" checked />
                                                    <label data-on="Active" data-off="Inactive" for="switch"></label>
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <asp:Label ID="lblrulepgm" runat="server" Font-Bold="true"></asp:Label>
                                                    <%--<label>Program</label>--%>
                                                </div>
                                                <asp:DropDownList ID="ddlProgram" runat="server" CssClass="form-control" data-select2-enable="true"
                                                    TabIndex="5" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvProgram" runat="server" ControlToValidate="ddlProgram"
                                                    Display="None" ErrorMessage="Please Select Program." ValidationGroup="submit"
                                                    SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-6 col-md-6 col-12" style="text-align: center" runat="server" id="rdoselection">
                                                <asp:RadioButtonList ID="rdioselect" runat="server" AppendDataBoundItems="true" RepeatDirection="Horizontal"
                                                    AutoPostBack="true" OnSelectedIndexChanged="rdioselect_SelectedIndexChanged">
                                                    <asp:ListItem Value="1">&nbsp;&nbsp; Module Wise&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                    <asp:ListItem Value="2">&nbsp;&nbsp;Credit Wise</asp:ListItem>
                                                    <asp:ListItem Value="3">&nbsp;&nbsp;CGPA Wise</asp:ListItem>
                                                </asp:RadioButtonList>

                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                     <asp:Label ID="lblrulefromsem" runat="server" Font-Bold="true"></asp:Label>
                                                    <%--<label>From Semester</label>--%>
                                                </div>
                                                <asp:DropDownList ID="ddlFromSemester" runat="server" CssClass="form-control" data-select2-enable="true"
                                                    OnSelectedIndexChanged="ddlFromSemester_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <%-- <asp:RequiredFieldValidator ID="rfvFromSemester" runat="server" ControlToValidate="ddlFromSemester"
                                                        Display="None" ErrorMessage="Please Select From Semester." ValidationGroup="semester"
                                                        SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <asp:Label ID="lblruletosem" runat="server" Font-Bold="true"></asp:Label>
                                                    <%--<label>To Semester</label>--%>
                                                </div>
                                                <asp:DropDownList ID="ddlToSemester" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlToSemester_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <%--  <asp:RequiredFieldValidator ID="rfvToSemester" runat="server" ControlToValidate="ddlToSemester"
                                                        Display="None" ErrorMessage="Please Select To Semester." ValidationGroup="semester"
                                                        SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12" id="failmodule" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <asp:Label ID="lblrulemaxmod" runat="server" Font-Bold="true"></asp:Label>
                                                    <%--<label>Max. No. of Fail Modules</label>--%>
                                                </div>
                                                <asp:TextBox ID="txtFailModule" runat="server" CssClass="form-control" MaxLength="2" onkeypress="return NumberOnly(event);" TabIndex="4" ToolTip="Max. No. of Fail Modules"></asp:TextBox>

                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12" id="Credit" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <asp:Label ID="lblrulecredits" runat="server" Font-Bold="true"></asp:Label>
                                                    <%--<label>Credit</label>--%>
                                                </div>
                                                <asp:TextBox ID="TxtCredit" runat="server" CssClass="form-control" MaxLength="2" onkeypress="return NumberOnly(event);" TabIndex="4" ToolTip="Credit"></asp:TextBox>

                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12" id="cgpa" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <asp:Label ID="lblrulecgpa" runat="server" Font-Bold="true"></asp:Label>
                                                    <%--<label>CGPA</label>--%>
                                                </div>
                                                <asp:TextBox ID="Txtcgpa" runat="server" CssClass="form-control" MaxLength="2" onkeypress="return NumberOnly(event);" TabIndex="4" ToolTip="CGPA"></asp:TextBox>

                                            </div>

                                            <div class="form-group col-lg-2 col-md-2 col-2">
                                                <div class="label-dynamic">
                                                    <label></label>
                                                </div>
                                                <%--<i class="fa fa-plus" aria-hidden="true" onclick="btnPlus"></i>--%>
                                                <asp:Button ID="Add" runat="server" CssClass="btn btn-outline-info" Text="Add" OnClick="Add_Click" ValidationGroup="submit" />
                                                <%-- <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                                ShowSummary="false" DisplayMode="List" ValidationGroup="semester" />--%>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-6 col-md-6 col-12" id="divyear" runat="server" visible="false">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <asp:Label ID="lblrulemanyear" runat="server" Font-Bold="true"></asp:Label>
                                                    <%--<label>Mandatory Completion of Year</label>--%>
                                                </div>
                                                <asp:CheckBoxList ID="chkyear" runat="server" AppendDataBoundItems="true" RepeatDirection="Horizontal"></asp:CheckBoxList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 pl-0 pr-0">
                                        <asp:Panel ID="pnllst" runat="server">
                                            <asp:HiddenField ID="hdnRuleSemId" runat="server" Value='<%# Eval("STUDENT_PROGRESSION_RULE_SEMID") %>' />
                                            <asp:ListView ID="lvSemester" runat="server" Visible="false">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Enter Semester Details</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap" id="mytable1" style="width: 100%">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th style="text-align: center">Edit
                                                                </th>
                                                                <th style="text-align: center">From Semester
                                                                </th>
                                                                <th style="text-align: center">To Semester
                                                                </th>
                                                                <th style="text-align: center">Option Wise Details
                                                                </th>
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
                                                            <asp:ImageButton ID="btnEdit1" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("STUDENT_PROGRESSION_RULE_SEMID") %>'
                                                                AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit1_Click" />
                                                            <%--    <asp:ImageButton ID="btnEdit1" runat="server" ImageUrl="~/images/edit.gif"  CommandArgument='<%# Eval("SrNo") %>'
                                                                    AlternateText="Edit Record" ToolTip='<%# Eval("SrNo") %>' OnClick="btnEdit1_Click" />--%>
                                                        </td>
                                                        <td style="text-align: center">
                                                            <asp:HiddenField runat="server" ID="hdnFromSemester" Value='<%# Eval("FromSemesterNo") %>' />
                                                            <%# Eval("FromSemester") %>
                                                        </td>
                                                        <td style="text-align: center">
                                                            <asp:HiddenField runat="server" ID="hdnToSemester" Value='<%# Eval("ToSemesterNo") %>' />
                                                            <%# Eval("ToSemester") %>
                                                        </td>
                                                        <td style="text-align: center">
                                                            <asp:HiddenField runat="server" ID="hdnFailModule" Value='<%# Eval("FailModule") %>' />
                                                            <%# Eval("FailModule")%>
                                                        </td>
                                                    </tr>

                                                </ItemTemplate>

                                            </asp:ListView>
                                            <%--<div class="row mt-4" id="DivAdd" runat="server" visible="true">
                                                                <div class="form-group col-12 text-center">
                                                                    <asp:Button ID="btnAddPolicySlab" runat="server" CssClass="btn btn-outline-info" Text="Add Policy Details" OnClick="btnAddPolicySlab_Click" />
                                                                </div>
                                                            </div>--%>
                                        </asp:Panel>
                                    </div>

                                    <div class="col-12 btn-footer">
                                        <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn btn-outline-primary" ClientIDMode="Static" ValidationGroup="submit" OnClick="btnSubmit_Click" TabIndex="6">Submit</asp:LinkButton>
                                        <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancel_Click" TabIndex="7">Cancel</asp:LinkButton>
                                        <asp:ValidationSummary ID="vsAllValidation" runat="server" ShowMessageBox="true"
                                            ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />
                                    </div>

                                    <div class="col-12 mt-3">
                                        <asp:Panel ID="Panel1" runat="server">
                                            <asp:ListView ID="lvStudentProgression" runat="server">
                                                <LayoutTemplate>
                                                    <div id="demo-grid">
                                                        <div class="sub-heading" id="dem">
                                                            <h5>Student Progression Details List</h5>
                                                        </div>
                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="myTable1">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>Edit</th>
                                                                    <th>Rule Name</th>
                                                                    <th>Faculty</th>
                                                                    <th>Study Level</th>
                                                                    <th>Program</th>
                                                                    <th>Options</th>
                                                                    <th>Year</th>
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
                                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("STUDENT_PROGRESSION_RULEID") %>'
                                                                AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                        </td>
                                                        <td><%# Eval("RULENAME")%></td>
                                                        <td><%# Eval("COLLEGE_NAME")%></td>
                                                        <td><%# Eval("UA_SECTIONNAME")%></td>
                                                        <td><%# Eval("PROGRAM")%></td>
                                                        <td><%# Eval("option1")%></td>
                                                        <td><%# Eval("year")%></td>
                                                        <td>
                                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status")%>' Font-Bold="true" ForeColor='<%# (Convert.ToInt32(Eval("Active") )== 1 ?  System.Drawing.Color.Green : System.Drawing.Color.Red )%>'></asp:Label>
                                                        </td>
                                                        <%--<td><%# Eval("Status")%></td>--%>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <%--<div class="col-12 mt-3">
                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                            <thead class="bg-light-blue">
                                <tr>
                                    <th>Edit</th>
                                    <th>Rule Name</th>
                                    <th>Faculty</th>
                                    <th>Study Level</th>
                                    <th>Program</th>
                                    <th>Max. No. of Fail Modules</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="btnJobAnnouncement" class="btnEditX" runat="server" CssClass="fa fa-pencil-square-o" /></td>
                                    <td>Rule Name A</td>
                                    <td>Faculty Name</td>
                                    <td>UG</td>
                                    <td>Program 1</td>
                                    <td>5</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>--%>
                        </div>
                        <div id="divallocation" runat="server" visible="false" role="tabpanel" aria-labelledby="Grade-tab">
                            <%--<div class="tab-pane fade" id="tab_2">--%>
                            <div>
                                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updRuleAllocation"
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
                            <asp:UpdatePanel ID="updRuleAllocation" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="col-12 mt-3">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                          <asp:Label ID="lblruleintake" runat="server" Font-Bold="true"></asp:Label>
                                                <%--    <label>Intake</label>--%>
                                                </div>
                                                <asp:DropDownList ID="ddlIntake" runat="server" CssClass="form-control" data-select2-enable="true"
                                                    AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <%--   <asp:RequiredFieldValidator ID="rfvIntake" runat="server" ControlToValidate="ddlIntake"
                                                        Display="None" ErrorMessage="Please Select From Semester." ValidationGroup="ruleName"
                                                        SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                            </div>

                                            <div class="form-group col-lg-9 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                          <asp:Label ID="lblrule" runat="server" Font-Bold="true"></asp:Label>
                                                  <%--  <label>Rule</label>--%>
                                                </div>
                                                <asp:DropDownList ID="lstbxRule" runat="server" CssClass="form-control" data-select2-enable="true"
                                                    AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <%--<asp:ListBox ID="lstbxRule" runat="server" CssClass="form-control multi-select-demo" SelectionMode="Multiple" AppendDataBoundItems="true"></asp:ListBox>--%>
                                                <%--  <asp:RequiredFieldValidator ID="rfvRule" runat="server" ControlToValidate="lstbxRule"
                                                        Display="None" ErrorMessage="Please Select From Semester." ValidationGroup="ruleName"
                                                        SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:LinkButton ID="btnSubmitRule" runat="server" CssClass="btn btn-outline-primary" OnClick="btnSubmitRule_Click" ValidationGroup="ruleName">Submit</asp:LinkButton>
                                        <asp:LinkButton ID="btnCancelRule" runat="server" CssClass="btn btn-outline-danger" TabIndex="7" OnClick="btnCancelRule_Click">Cancel</asp:LinkButton>
                                    </div>
                                    <%-- <asp:ValidationSummary ID="vsRule" runat="server" ShowMessageBox="true"
                                                ShowSummary="false" DisplayMode="List" ValidationGroup="ruleName" />--%>
                                    <div class="col-12">
                                        <asp:Panel ID="pnlRuleAllocation" runat="server">
                                            <asp:ListView ID="lvRuleAllocation" runat="server">
                                                <LayoutTemplate>
                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Edit</th>
                                                                <th>Intake</th>
                                                                <th>Rule</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/images/edit.png"
                                                                CommandArgument='<%# Eval("INTAKE_RULE_MAPPINGID")%>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                                OnClick="btnEdit_Click1" TabIndex="12" />
                                                        </td>
                                                        <td><%# Eval("BATCHNAME")%></td>
                                                        <td><%# Eval("RULENAME")%></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div id="divEmoji" runat="server" visible="false" role="tabpanel" aria-labelledby="Grade-tab">
                            <%--<div class="tab-pane fade" id="tab_3">--%>
                            <div>
                                <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="updbridging"
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
                            <asp:UpdatePanel ID="updbridging" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="col-12 mt-3">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                         <asp:Label ID="lblfacrule" runat="server" Font-Bold="true"></asp:Label>
                                                    <%--<label>Faculty/School Name</label>--%>
                                                </div>
                                                <asp:DropDownList ID="ddlclgbrid" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="2"
                                                    AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlclgbrid_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlclgbrid"
                                                    Display="None" ErrorMessage="Please Select Faculty." ValidationGroup="submit"
                                                    SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                       <asp:Label ID="lblDYPrgmtype" runat="server" Font-Bold="true"></asp:Label>
                                                   <%-- <label>Study Level</label>--%>
                                                </div>
                                                <asp:DropDownList ID="ddlstudylevelbrid" runat="server" CssClass="form-control" data-select2-enable="true"
                                                    AppendDataBoundItems="true" TabIndex="3" OnSelectedIndexChanged="ddlstudylevelbrid_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlstudylevelbrid"
                                                    Display="None" ErrorMessage="Please Select Study Level." ValidationGroup="submit"
                                                    SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                            </div>


                                        </div>
                                    </div>
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                       <asp:Label ID="lblpgmrule" runat="server" Font-Bold="true"></asp:Label>
                                                   <%-- <label>Program</label>--%>
                                                </div>
                                                <asp:DropDownList ID="ddlpgmbrid" runat="server" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlpgmbrid_SelectedIndexChanged" AutoPostBack="true"
                                                    TabIndex="5" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlpgmbrid"
                                                    Display="None" ErrorMessage="Please Select Program." ValidationGroup="submit"
                                                    SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                     <asp:Label ID="lblfromsemrule" runat="server" Font-Bold="true"></asp:Label>
                                                    <%--<label>From Semester</label>--%>
                                                </div>
                                                <asp:DropDownList ID="ddlfromsembrid" runat="server" CssClass="form-control" data-select2-enable="true"
                                                    AppendDataBoundItems="true" AutoPostBack="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlfromsembrid"
                                                    Display="None" ErrorMessage="Please Select From Semester." ValidationGroup="submit"
                                                    SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                     <asp:Label ID="lblbridsemrule" runat="server" Font-Bold="true"></asp:Label>
                                              <%--      <label>Bridging Program</label>--%>
                                                </div>
                                                <asp:DropDownList ID="ddlbridgingpgm" runat="server" CssClass="form-control" data-select2-enable="true"
                                                    TabIndex="5" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlbridgingpgm"
                                                    Display="None" ErrorMessage="Please Select Bridging Program." ValidationGroup="submit"
                                                    SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                      <asp:Label ID="lbltoSemrule" runat="server" Font-Bold="true"></asp:Label>
                                                 <%--   <label>To Semester</label>--%>
                                                </div>
                                                <asp:DropDownList ID="ddltosembrid" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddltosembrid"
                                                    Display="None" ErrorMessage="Please Select To Semester." ValidationGroup="submit"
                                                    SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                     <asp:Label ID="lblfailmodulerule" runat="server" Font-Bold="true"></asp:Label>
                                                    <%--<label>Max. No. of Fail Modules</label>--%>
                                                </div>
                                                <asp:TextBox ID="TxtModules" runat="server" CssClass="form-control" MaxLength="2" onkeypress="return NumberOnly(event);" TabIndex="4" ToolTip="Max. No. of Fail Modules"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="TxtModules"
                                                    Display="None" ErrorMessage="Please Enter Max. No. of Fail Modules." ValidationGroup="submit"
                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:LinkButton ID="btnSubmitbrid" runat="server" CssClass="btn btn-outline-primary" ValidationGroup="submit" OnClick="btnSubmitbrid_Click" TabIndex="6">Submit</asp:LinkButton>
                                        <asp:LinkButton ID="btnclearbrid" runat="server" CssClass="btn btn-outline-danger" OnClick="btnclearbrid_Click" TabIndex="7">Cancel</asp:LinkButton>
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                            ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />
                                    </div>
                                    <div class="col-12 mt-3">
                                        <asp:Panel ID="Panel2" runat="server" Visible="false">
                                            <asp:ListView ID="Lvbridlist" runat="server">
                                                <LayoutTemplate>
                                                    <div id="demo-grid">
                                                        <div class="sub-heading" id="dem">
                                                            <h5>Bridging Progress Rule  List</h5>
                                                        </div>

                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="myTable1">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>Edit</th>
                                                                    <th>Faculty</th>
                                                                    <th>Study Level</th>
                                                                    <th>Program</th>
                                                                    <th>Bridging Program</th>
                                                                    <th>From Semester</th>
                                                                    <th>To Semester</th>
                                                                    <th>No. of Fail Modules</th>
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
                                                            <asp:ImageButton ID="btnEditBrid" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("RULEID") %>'
                                                                AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEditBrid_Click" />
                                                        </td>
                                                        <td><%# Eval("COLLEGE_NAME")%></td>
                                                        <td><%# Eval("UA_SECTIONNAME")%></td>
                                                        <td><%# Eval("PGM")%></td>
                                                        <td><%# Eval("BRIDGINGPGM")%></td>
                                                        <td><%# Eval("SEMESTERNAME")%></td>
                                                        <td><%# Eval("SEMESTERNAME1")%></td>
                                                        <td><%# Eval("MAX_FAIL_MODULE")%></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>


                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </div>
                        <div id="divAlot" runat="server" visible="false" role="tabpanel" aria-labelledby="Grade-tab">
                            <div>
                                <asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="updalot"
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
                            <asp:UpdatePanel ID="updalot" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="col-12 mt-3">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <asp:Label ID="lblDYSession" runat="server" Font-Bold="true"></asp:Label>
                                                    <%--<label>Intake</label>--%>
                                                </div>
                                                <asp:DropDownList ID="ddlintakeAlot" runat="server" TabIndex="1" CssClass="form-control" data-select2-enable="true"
                                                    AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlintakeAlot"
                                                    Display="None" ErrorMessage="Please Select Intake." ValidationGroup="showalot"
                                                    SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <asp:Label ID="lblDYCollege" runat="server" Font-Bold="true"></asp:Label>
                                                    <%--<label>Faculty/School name</label>--%>
                                                </div>
                                                <asp:DropDownList ID="ddlfac" runat="server" TabIndex="2" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlfac_SelectedIndexChanged" AutoPostBack="true"
                                                    AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlfac"
                                                    Display="None" ErrorMessage="Please Select Faculty/School name." ValidationGroup="showalot"
                                                    SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <asp:Label ID="lblpgm" runat="server" Font-Bold="true"></asp:Label>
                                                    <%--<label>Program</label>--%>
                                                </div>
                                                <asp:DropDownList ID="ddlpgm" runat="server" TabIndex="3" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlpgm_SelectedIndexChanged" AutoPostBack="true"
                                                    AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlpgm"
                                                    Display="None" ErrorMessage="Please Select Program." ValidationGroup="showalot"
                                                    SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <asp:Label ID="lblcgangepgm" runat="server" Font-Bold="true"></asp:Label>
                                                    <%--<label>Program</label>--%>
                                                </div>
                                                <asp:DropDownList ID="ddlChangeProgram" runat="server" TabIndex="3" CssClass="form-control" data-select2-enable="true"
                                                    AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="ddlChangeProgram"
                                                    Display="None" ErrorMessage="Please Select Bridging Program." ValidationGroup="showalot"
                                                    SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <asp:Label ID="lblDYSemester" runat="server" Font-Bold="true"></asp:Label>
                                                    <%--<label>Semester</label>--%>
                                                </div>
                                                <asp:DropDownList ID="ddlsemester" runat="server" TabIndex="3" CssClass="form-control" data-select2-enable="true"
                                                    AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlsemester"
                                                    Display="None" ErrorMessage="Please Select Semester." ValidationGroup="showalot"
                                                    SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnShow" runat="server" CssClass="btn btn-outline-primary" TabIndex="4" Text="Show" OnClick="btnShow_Click" ValidationGroup="showalot" />
                                        <asp:Button ID="btnSubmitAlot" runat="server" CssClass="btn btn-outline-primary" TabIndex="5" Visible="false" Text="Submit" OnClick="btnSubmitAlot_Click" ValidationGroup="showalot" />
                                        <asp:Button ID="btnCancelAlot" runat="server" CssClass="btn btn-outline-danger" Text="Cancel" TabIndex="6" OnClick="btnCancelAlot_Click" />
                                        <asp:ValidationSummary ID="vsRule" runat="server" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" ValidationGroup="showalot" />
                                    </div>
                                    <div class="col-12">
                                        <asp:Panel ID="Panel3" runat="server" Visible="false">

                                            <asp:ListView ID="LvAllot" runat="server">
                                                <LayoutTemplate>
                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="mytable_int">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>
                                                                    <asp:CheckBox ID="cbHead" runat="server" OnClick="checkAllCheckbox(this)" /></th>
                                                                <th>Student ID</th>
                                                                <th>Name With Initial</th>
                                                                <th>Intake</th>
                                                                <th>Semester</th>
                                                                <th>Program</th>
                                                                <th>Status</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="chktransfer" runat="server" ToolTip='<%# Eval("IDNO")%>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblenroll" runat="server" Text=' <%# Eval("REGNO")%>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblname" runat="server" Text=' <%# Eval("NAME_WITH_INITIAL")%>'></asp:Label></td>
                                                        <td><%# Eval("BATCHNAME")%></td>
                                                        <td><%# Eval("SEMESTERNAME")%>
                                                            <asp:HiddenField ID="hdfSemester" runat="server" Value='<%# Eval("SEMESTERNO")%>' />
                                                        </td>
                                                        <td><%# Eval("PROGRAM")%></td>
                                                        <td>
                                                            <asp:Label runat="server" ID="lblStatus" Font-Bold="true" Text='<%# (Eval("STATUS"))%>' ForeColor='<%# (Convert.ToString(Eval("STATUS" ))== "ELIGIBLE" ?  System.Drawing.Color.Green : System.Drawing.Color.Red )%>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div id="divbridresult" runat="server" visible="false" role="tabpanel" aria-labelledby="Grade-tab">
                            <div>
                                <asp:UpdateProgress ID="UpdateProgress5" runat="server" AssociatedUpdatePanelID="updresult"
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
                            <asp:UpdatePanel ID="updresult" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="col-12 mt-3">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <asp:Label ID="lblSessionName" runat="server" Font-Bold="true"></asp:Label>
                                                    <%--<label>Intake</label>--%>
                                                </div>
                                                <asp:DropDownList ID="ddlsessionresult" runat="server" TabIndex="1" CssClass="form-control" data-select2-enable="true"
                                                    AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="ddlsessionresult"
                                                    Display="None" ErrorMessage="Please Select Session." ValidationGroup="showresult"
                                                    SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <asp:Label ID="lblfaculty" runat="server" Font-Bold="true"></asp:Label>
                                                    <%--<label>Faculty/School name</label>--%>
                                                </div>
                                                <asp:DropDownList ID="ddlfacresult" runat="server" TabIndex="2" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlfacresult_SelectedIndexChanged" AutoPostBack="true"
                                                    AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlfacresult"
                                                    Display="None" ErrorMessage="Please Select Faculty/School name." ValidationGroup="showresult"
                                                    SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <asp:Label ID="lblDyProgram" runat="server" Font-Bold="true"></asp:Label>
                                                    <%--<label>Program</label>--%>
                                                </div>
                                                <asp:DropDownList ID="ddlpgmresult" runat="server" TabIndex="3" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlpgmresult_SelectedIndexChanged" AutoPostBack="true"
                                                    AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="ddlpgmresult"
                                                    Display="None" ErrorMessage="Please Select Program." ValidationGroup="showresult"
                                                    SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <asp:Label ID="lblSemester" runat="server" Font-Bold="true"></asp:Label>
                                                    <%--<label>Semester</label>--%>
                                                </div>
                                                <asp:DropDownList ID="ddlsemresult" runat="server" TabIndex="3" CssClass="form-control" data-select2-enable="true"
                                                    AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="ddlsemresult"
                                                    Display="None" ErrorMessage="Please Select Semester." ValidationGroup="showresult"
                                                    SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnShowResult" runat="server" CssClass="btn btn-outline-primary" TabIndex="4" Text="Show" OnClick="btnShowResult_Click" ValidationGroup="showresult" />
                                        <asp:Button ID="btnSubmitResult" runat="server" CssClass="btn btn-outline-primary" TabIndex="5" Text="Submit" OnClick="btnSubmitResult_Click" Visible="false" />
                                        <asp:Button ID="btnCancelresult" runat="server" CssClass="btn btn-outline-danger" Text="Cancel" TabIndex="8" OnClick="btnCancelresult_Click" />
                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" ValidationGroup="showresult" />
                                    </div>
                                    <div class="col-12">
                                        <asp:Panel ID="Panel4" runat="server" Visible="false">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <asp:Label ID="lbltrprogram" runat="server" Font-Bold="true"></asp:Label>
                                                    <%--<label>Semester</label>--%>
                                                </div>
                                                <asp:DropDownList ID="ddltranpgmresults" runat="server" TabIndex="3" CssClass="form-control" data-select2-enable="true"
                                                    AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>

                                            </div>
                                            <asp:ListView ID="LvbridResult" runat="server">
                                                <LayoutTemplate>
                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="mytable_int">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th class="text-center">
                                                                    <asp:CheckBox ID="chkResultProcess" runat="server" Text="" onClick="totAll(this);"
                                                                        ToolTip="Select or Deselect All Records" />
                                                                </th>
                                                                <th>Student Id</th>
                                                                <th>Student Name</th>
                                                                <th>Program</th>
                                                                <th>Result Status</th>
                                                                <th>Publish Status</th>
                                                                <th>Process Date</th>
                                                                <th>Result</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="chkResultProcess" runat="server" Enabled='<%# Eval("PASSFAIL").ToString() == "PASS" ? true : false%>' />
                                                             <%--<asp:CheckBox ID="chkResultProcess" runat="server"  />--%>
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" ID="lblregno" Text='<%# Eval("REGNO")%>'></asp:Label>
                                                            <asp:HiddenField ID="hdfidno" runat="server" Value='<%# Eval("IDNO")%>' />
                                                             <asp:HiddenField ID="hdfstatus" runat="server" Value='<%# Eval("PASSFAIL")%>' />
                                                        </td>
                                                        <td><%# Eval("STUDNAME")%>
                                                            <asp:HiddenField ID="hdfname" runat="server" Value='<%# Eval("STUDNAME")%>' />
                                                        </td>
                                                        <td>
                                                            <%# Eval("PROGRAM")%>
                                                            <asp:Label runat="server" ID="lblDegreeNo" Text='<%# Eval("DEGREENO")%>' Visible="false"></asp:Label>
                                                            <asp:Label runat="server" ID="lblBranchNo" Text='<%# Eval("BRANCHNO")%>' Visible="false"></asp:Label>
                                                            <asp:Label runat="server" ID="lblSchemeNo" Text='<%# Eval("SCHEMENO")%>' Visible="false"></asp:Label>
                                                            <asp:Label runat="server" ID="lblbAffiNo" Text='<%# Eval("AFFILIATED_NO")%>' Visible="false"></asp:Label>
                                                        </td>
                                                        <td class="text-center">
                                                            <asp:Label runat="server" ID="lblRStatus" Text='<%# Eval("PROCESS_STATUS")%>'></asp:Label>
                                                            <asp:Label runat="server" ID="lblStaus" Text='<%# Eval("PSTATUS")%>' Visible="false"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label runat="server" ID="lblPublishStat" Text='<%# Eval("LOCK_STATUS") %>'></asp:Label>
                                                            <asp:Label runat="server" ID="lblLockStatus" Text='<%# Eval("LOCK") %>' Visible="false"></asp:Label>
                                                        </td>
                                                        <td><%#Eval("RESULTDATE") %></td>
                                                          <td>
                                                            <asp:Label ID="lblresultstatus" runat="server" Text='<% #Eval("PASSFAIL")%>' Font-Bold="true" ForeColor='<%# (Convert.ToString(Eval("PASSFAIL") )== "PASS" ?  System.Drawing.Color.Green : System.Drawing.Color.Red )%>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%--   </ContentTemplate>
    </asp:UpdatePanel>--%>
    <script type="text/javascript" language="javascript">

        function checkAllCheckbox(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                var s = e.name.split("ctl00$ContentPlaceHolder1$LvAllot$ctrl");
                var b = 'ctl00$ContentPlaceHolder1$LvAllot$ctrl';
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
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200,
                enableFiltering: true,
                filterPlaceholder: 'Search',
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
                });
            });
        });

        function NumberOnly(evt) {
            //alert('bbb');
            if (evt.charCode > 31 && (evt.charCode < 48 || evt.charCode > 57)) {
                //alert("Enter Only Numeric Value ");
                return false;
            }
        }
    </script>
    <script type="text/javascript">
        $(function () {
            $("#txtRuleName").keypress(function (e) {
                var keyCode = e.keyCode || e.which;


                //Regex for Valid Characters i.e. Alphabets and Numbers.
                var regex = /^[A-Za-z0-9]+$/;

                //Validate TextBox value against the Regex.
                var isValid = regex.test(String.fromCharCode(keyCode));
                if (!isValid) {
                    return false;
                }

                return isValid;
            });
        });
    </script>
    <script>
        function allowAlphaNumericSpace(e) {
            var code = ('charCode' in e) ? e.charCode : e.keyCode;
            if (!(code == 32) && // space
              !(code > 47 && code < 58) && // numeric (0-9)
              !(code > 64 && code < 91) && // upper alpha (A-Z)
              !(code > 96 && code < 123)) { // lower alpha (a-z)
                e.preventDefault();
                //    alert("Not Allowed Special Character..!");
                return true;
            }
            else
                return false;
        }
    </script>
    <script>
        function SetStat(val) {
            $('#switch').prop('checked', val);
        }
        var summary = "";
        $(function () {
            $('#btnSubmit').click(function () {
                localStorage.setItem("currentId", "#btnSubmit,Submit");
                debugger;
                ShowLoader('#btnSubmit');
                if (summary != "") {
                    customAlert(summary);
                    summary = "";
                    return false
                }
                $('#hfdStat').val($('#switch').prop('checked'));
            });
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmit').click(function () {
                    localStorage.setItem("currentId", "#btnSubmit,Submit");
                    ShowLoader('#btnSubmit');


                    if (summary != "") {
                        customAlert(summary);
                        summary = "";
                        return false
                    }
                    $('#hfdStat').val($('#switch').prop('checked'));
                });
            });
        });
    </script>
    <script>
        function totAll(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (headchk.checked == true)
                        e.checked = true;
                    else {
                        e.checked = false;
                        headchk.checked = false;
                    }
                }

            }

        }
        function totexamreportAll(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (headchk.checked == true)
                        e.checked = true;
                    else {
                        e.checked = false;
                        headchk.checked = false;
                    }
                }

            }

        }
    </script>
</asp:Content>

