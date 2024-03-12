<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Student_Achievement.aspx.cs" Inherits="Student_Achievement" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
          .dataTables_scrollHeadInner {
              width: max-content !important;
          }
      </style>
      <%--===== Data Table Script added by gaurav =====--%>
        <script>
            $(document).ready(function () {
                var table = $('#mytable_detail').DataTable({
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
                                var arr = [];
                                if (arr.indexOf(idx) !== -1) {
                                    return false;
                                } else {
                                    return $('#mytable_detail').DataTable().column(idx).visible();
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
                                                return $('#mytable_detail').DataTable().column(idx).visible();
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
                                                return $('#mytable_detail').DataTable().column(idx).visible();
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
                    var table = $('#mytable_detail').DataTable({
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
                                        return $('#mytable_detail').DataTable().column(idx).visible();
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
                                                    return $('#mytable_detail').DataTable().column(idx).visible();
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
                                                var arr = [];
                                                if (arr.indexOf(idx) !== -1) {
                                                    return false;
                                                } else {
                                                    return $('#mytable_detail').DataTable().column(idx).visible();
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

  <%--<div>
        <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
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
    </div>--%>
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>
                         <div class="nav-tabs-custom mt-2 col-12 pb-4" id="myTabContent">
                               <ul class="nav nav-tabs dynamic-nav-tabs" role="tablist">
                                    <li class="nav-item active" runat="server" id="liTab1">
                                        <asp:LinkButton ID="lnkTab1" runat="server" OnClick="lnkTab1_Click" CssClass="nav-link" TabIndex="1">Achievement</asp:LinkButton>
                         
                                    </li>
                                    <li class="nav-item" runat="server" id="liTab2">
                                        <asp:LinkButton ID="lnkTab2" runat="server" OnClick="lnkTab2_Click" CssClass="nav-link" TabIndex="2">Sports</asp:LinkButton>
                                       
                                    </li>
                                    <li class="nav-item" runat="server" id="liTab3">
                                        <asp:LinkButton ID="lnkTab3" runat="server" OnClick="lnkTab3_Click" CssClass="nav-link" TabIndex="3">Student Achievement</asp:LinkButton>
                    
                                    </li>
                                </ul>
                             <div class="tab-content">
                                 <div class="tab-pane active" id="tab_1" runat="server" role="tabpanel">
                                     <div>

                                         <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updSport"
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
                                     <asp:UpdatePanel ID="updSport" runat="server">
                                         <ContentTemplate>
                                             <div class="col-12 mt-3">
                                                 <div class="row">
                                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                                         <div class="label-dynamic">
                                                             <sup>* </sup>
                                                             <asp:Label ID="Label1" runat="server" Font-Bold="true">Achievement Name</asp:Label>
                                                             <%--<label>Faculty </label>--%>
                                                         </div>
                                                         <asp:TextBox runat="server" ID="txtAchivement" data-select2-enable="true" CssClass="form-control"></asp:TextBox>
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtAchivement"
                                                             Display="None" ErrorMessage="Please Enter Achievement Name" ValidationGroup="btnSubmit" InitialValue="" />
                                                     </div>

                                                 </div>
                                             </div>
                                              <div id="Div1" class="col-12 btn-footer mt-4" runat="server" >
                                                 <asp:LinkButton ID="btnAchivement" runat="server" CssClass="btn btn-outline-info" OnClick="btnAchivement_Click" ValidationGroup="btnSubmit">Submit</asp:LinkButton>
                                                 <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn btn-outline-danger" OnClick="LinkButton2_Click">Cancel</asp:LinkButton>
                                                 <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="btnSubmit"
                                                     ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

                                             </div>
                                             <div class="col-md-12">
                                                 <asp:Panel ID="Panel1" runat="server">
                                                     <asp:ListView runat="server" ID="lvAchivement">

                                                         <LayoutTemplate>
                                                             <table class="table table-striped table-bordered nowrap" style="width: 100%" id="mytable_detail">
                                                                 <thead class="bg-light-blue">
                                                                     <tr>
                                                                         <th>Edit</th>
                                                                         <th>Achievement Name</th>
                                                                       
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
                                                                     <asp:ImageButton ID="btnEdits" runat="server" CausesValidation="false" ImageUrl="~/images/edit.gif"
                                                                         AlternateText="Edit Record" OnClick="btnEdits_Click" CommandArgument='<%# Eval("ACHIEVEMENT_NAME") %>' ToolTip=<%#Eval("ACHIEVEMENT_NO") %>/>
                                                                 </td>
                                                                 <td>
                                                                        <%#Eval("ACHIEVEMENT_NAME") %>
                                                                 </td>
                                                             </tr>
                                                         </ItemTemplate>
                                                     </asp:ListView>
                                                 </asp:Panel>
                                             </div>
                                         </ContentTemplate>
                                         <Triggers>
                                             <asp:PostBackTrigger ControlID="btnAchivement" />
                                              <asp:PostBackTrigger ControlID="LinkButton2" />
                                              <asp:PostBackTrigger ControlID="lvAchivement" />
                                            


                                         </Triggers>
                                     </asp:UpdatePanel>
                                 </div>
                                 <div id="tab_2" runat="server" visible="false" role="tabpanel">
                                     <div>

                                         <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updAchivement"
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
                                     <asp:UpdatePanel ID="updAchivement" runat="server">
                                         <ContentTemplate>
                                             <div class="col-12 mt-3">
                                                 <div class="row">
                                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                                         <div class="label-dynamic">
                                                             <sup>* </sup>
                                                             <asp:Label ID="Label3" runat="server" Font-Bold="true">Sport Name</asp:Label>
                                                             <%--<label>Faculty </label>--%>
                                                         </div>
                                                         <asp:TextBox runat="server" ID="txtSportName" data-select2-enable="true" CssClass="form-control"></asp:TextBox>
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtSportname"
                                                             Display="None" ErrorMessage="Please Enter Sport Name" ValidationGroup="btnSubmit" InitialValue="" />
                                                     </div>

                                                     <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                                         <div class="label-dynamic">
                                                             <sup>* </sup>
                                                             <asp:Label ID="Label4" runat="server" Font-Bold="true">Sports Type</asp:Label>
                                                             <%--            <label>Degree </label>lblDYDegree--%>
                                                         </div>
                                                         <asp:DropDownList ID="ddlSportType" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                             <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                         </asp:DropDownList>
                                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlSportType"
                                                             Display="None" ErrorMessage="Please Select Sport Type" ValidationGroup="btnSubmit" InitialValue="0" />--%>
                                                     </div>
                                                 </div>
                                             </div>
                                              <div id="Div2" class="col-12 btn-footer mt-4" runat="server" >
                                                 <asp:LinkButton ID="btnSoprtData" runat="server" CssClass="btn btn-outline-info" ValidationGroup="btnSubmit" OnClick="btnSoprtData_Click" >Submit</asp:LinkButton>
                                                 <asp:LinkButton ID="btncancel1" runat="server" CssClass="btn btn-outline-danger" OnClick="btncancel1_Click">Cancel</asp:LinkButton>
                                                 <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="btnSubmit"
                                                     ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

                                             </div>
                                               <div class="col-md-12">
                                                 <asp:Panel ID="Panel2" runat="server">
                                                     <asp:ListView runat="server" ID="lvSportType">

                                                         <LayoutTemplate>
                                                             <table class="table table-striped table-bordered nowrap" style="width: 100%" id="mytable_detail">
                                                                 <thead class="bg-light-blue">
                                                                     <tr>
                                                                         <th>Edit</th>
                                                                         <th>Sport Name</th>
                                                                       
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
                                                                     <asp:ImageButton ID="btnEditSport" runat="server" CausesValidation="false" ImageUrl="~/images/edit.gif"
                                                                         AlternateText="Edit Record"  OnClick="btnEditSport_Click" CommandArgument='<%# Eval("SPORTS_NAME") %>' ToolTip=<%#Eval("SPORTS_NO") %> CommandName=<%#Eval("SPORTS_TYPE") %>/>
                                                                 </td>
                                                                 <td>
                                                                        <%#Eval("SPORTS_NAME") %>
                                                                 </td>
                                                             </tr>
                                                         </ItemTemplate>
                                                     </asp:ListView>
                                                 </asp:Panel>
                                             </div>
                                         </ContentTemplate>
                                          <Triggers>
                                             <asp:PostBackTrigger ControlID="btnSoprtData" />
                                              <asp:PostBackTrigger ControlID="btncancel1" />
                                              <asp:PostBackTrigger ControlID="lvSportType" />
                                            


                                         </Triggers>
                                     </asp:UpdatePanel>
                                 </div>
                                 <div id="tab_3" runat="server" visible="false" role="tabpanel">
                                     <div>

                                         <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updeinternalStudentAchivement"
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
                                     <asp:UpdatePanel ID="updeinternalStudentAchivement" runat="server">
                                         <ContentTemplate>
                                             <div class="col-12 mt-3">
                                                 <div class="row">
                                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                                         <div class="label-dynamic">
                                                             <sup>* </sup>
                                                             <asp:Label ID="lblDYCollege" runat="server" Font-Bold="true"></asp:Label>
                                                             <%--<label>Faculty </label>--%>
                                                         </div>
                                                         <asp:DropDownList ID="ddlFaculty" runat="server" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                                             <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                         </asp:DropDownList>
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlFaculty"
                                                             Display="None" ErrorMessage="Please Select Faculty" ValidationGroup="btnSubmit" InitialValue="0" />
                                                     </div>

                                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                                         <div class="label-dynamic">
                                                             <sup>* </sup>
                                                             <asp:Label ID="lblDYDegree" runat="server" Font-Bold="true"></asp:Label>
                                                             <%--            <label>Degree </label>lblDYDegree--%>
                                                         </div>
                                                         <asp:DropDownList ID="ddlDegree" runat="server" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                                             <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                         </asp:DropDownList>
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlDegree"
                                                             Display="None" ErrorMessage="Please Select Degree" ValidationGroup="btnSubmit" InitialValue="0" />
                                                     </div>

                                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                                         <div class="label-dynamic">
                                                             <sup>* </sup>
                                                             <asp:Label ID="lblDyProgram" runat="server" Font-Bold="true"></asp:Label>
                                                             <%--                         <label>Program</label>--%>
                                                         </div>
                                                         <asp:DropDownList ID="ddlProgram" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged" AutoPostBack="true">
                                                             <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                         </asp:DropDownList>
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlProgram"
                                                             Display="None" ErrorMessage="Please Select Program" ValidationGroup="btnSubmit" InitialValue="0" />
                                                     </div>

                                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                                         <div class="label-dynamic">
                                                             <sup>* </sup>
                                                             <asp:Label ID="lblDYSemester" runat="server" Font-Bold="true"></asp:Label>
                                                             <%--                      <label>Program</label>	--%>
                                                         </div>
                                                         <asp:DropDownList ID="ddlSemester" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                             <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                         </asp:DropDownList>
                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlSemester"
                                                             Display="None" ErrorMessage="Please Select Semester" ValidationGroup="btnSubmit" InitialValue="0" />
                                                     </div>
                                                 </div>
                                             </div>

                                             <div class="col-md-12">
                                                 <asp:Panel ID="Panel" runat="server">
                                                     <asp:ListView runat="server" ID="lvStudentAchievement">

                                                         <LayoutTemplate>
                                                             <table class="table table-striped table-bordered nowrap" style="width: 100%" id="mytable_detail">
                                                                 <thead class="bg-light-blue">
                                                                     <tr>
                                                                         <th>
                                                                             <asp:CheckBox ID="headchk" runat="server" onclick="return totAll(this);" /></th>

                                                                         <th>Student Id</th>
                                                                         <th>Student Name</th>
                                                                         <th>Semester</th>
                                                                         <th>Sport</th>
                                                                         <th>Achievement</th>
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
                                                                     <asp:CheckBox ID="chkRow" runat="server" /></td>
                                                                 <td>
                                                                     <asp:Label runat="server" ID="lvlRegno" Text='<%#Eval("REGNO") %>'></asp:Label>

                                                                 </td>
                                                                 <td>
                                                                     <asp:Label runat="server" ID="lblName" Text='<%#Eval("NAME_WITH_INITIAL") %>'></asp:Label>
                                                                     <asp:Label runat="server" ID="lblIdno" Text='<%#Eval("IDNO") %>' Visible="false"></asp:Label>
                                                                     <asp:Label runat="server" ID="lblSportno" Text='<%#Eval("SPORTS") %>' Visible="false"></asp:Label>
                                                                     <asp:Label runat="server" ID="lblAchivement" Text='<%#Eval("ACHIEVEMENT_NO") %>' Visible="false"></asp:Label>


                                                                 </td>
                                                                 <td>
                                                                     <asp:Label runat="server" ID="lblSemester" Text='<%#Eval("SEMESTERNAME") %>'></asp:Label>
                                                                 </td>
                                                                 <td>
                                                                     <asp:DropDownList ID="ddlSport" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                                         <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                     </asp:DropDownList>
                                                                     <!--cricket,football,etc-->
                                                                 </td>
                                                                 <td>
                                                                     <asp:DropDownList ID="ddlAchievement" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                                         <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                     </asp:DropDownList>
                                                                     <!--Winner, RunnerUp, etc-->
                                                                 </td>


                                                             </tr>
                                                         </ItemTemplate>
                                                     </asp:ListView>
                                                 </asp:Panel>
                                             </div>

                                             <div class="col-12 btn-footer mt-4" runat="server" visible="false" id="Divbutton">
                                                 <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn btn-outline-info" OnClick="btnSubmit_Click" ValidationGroup="btnSubmit">Submit</asp:LinkButton>
                                                 <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancel_Click">Cancel</asp:LinkButton>
                                                 <asp:ValidationSummary ID="ValidationSummary4" runat="server" ValidationGroup="btnSubmit"
                                                     ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

                                             </div>
                                         </ContentTemplate>
                                     </asp:UpdatePanel>
                                 </div>
                             </div>
                        </div>
                    </div>
                </div>
            </div>
<%--        </ContentTemplate>
    </asp:UpdatePanel>--%>
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
</asp:Content>

