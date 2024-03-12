<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Slot_Monitoring.aspx.cs" Inherits="MockUps_Slot_Monitoring" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="<%=Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.css")%>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.js")%>"></script>
    <style>
        #ctl00_ContentPlaceHolder1_pnlSlot .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
         <script>
             $(document).ready(function () {
                 //-- Set time out function for dynamic column visibility --// 
    
                     var table = $('#mytable').DataTable({
                         responsive: true,
                         lengthChange: true,
                         scrollY: 320,
                         scrollX: true,
                          scrollCollapse: true,
                          paging: false,
                         dom: 'lBfrtip',
                         buttons: [
                             {
                                 extend: 'colvis',
                                 text: 'Column Visibility',
                                 columns: function (idx, data, node) {
                                     var arr = [8];
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

                                 extend: 'excelHtml5',
                                 exportOptions: {
                                     columns: function (idx, data, node) {
                                         var arr = [8];
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
                         ],
                         "bDestroy": true,
                     });
                 });
             var parameter = Sys.WebForms.PageRequestManager.getInstance();
             parameter.add_endRequest(function () {
                 $(document).ready(function () {
                     //-- Set time out function for dynamic column visibility --// 

                     var table = $('#mytable').DataTable({
                         responsive: true,
                         lengthChange: true,
                         scrollY: 320,
                         scrollX: true,
                         scrollCollapse: true,
                         paging: false,
                         dom: 'lBfrtip',
                         buttons: [
                             {
                                 extend: 'colvis',
                                 text: 'Column Visibility',
                                 columns: function (idx, data, node) {
                                     var arr = [8];
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

                                 extend: 'excelHtml5',
                                 exportOptions: {
                                     columns: function (idx, data, node) {
                                         var arr = [8];
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
                         ],
                         "bDestroy": true,
                     });
                 });
             });
    </script>
        <div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdRole"
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
    <asp:UpdatePanel runat="server" ID="UpdRole">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblRASession" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlAcademicSession" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                            ControlToValidate="ddlAcademicSession" Display="None" SetFocusOnError="true"
                                            ErrorMessage="Please Select Academic Session" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYCollege" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:ListBox ID="lstCollege" runat="server" OnSelectedIndexChanged="lstCollege_SelectedIndexChanged" AutoPostBack="true" SelectionMode="Multiple" AppendDataBoundItems="true" CssClass="form-control multi-select-demo" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:ListBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                            ControlToValidate="lstCollege" Display="None" SetFocusOnError="true"
                                            ErrorMessage="Please Select College" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDyCenter" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlCampus" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                            ControlToValidate="ddlCampus" Display="None" SetFocusOnError="true"
                                            ErrorMessage="Please Select Campus" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <asp:Label ID="lblDYBranch" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlCourse" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <asp:Label ID="lblDYSemester" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemster" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <%-- <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <asp:Label ID="lbllearning" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlModality" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>--%>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:LinkButton ID="btnShow" runat="server" CssClass="btn btn-outline-info" ValidationGroup="Show" OnClick="btnShow_Click" TabIndex="1">Show</asp:LinkButton>
                                <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancel_Click" TabIndex="1">Cancel</asp:LinkButton>
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Show"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>
                            <div class="col-12">
                                <asp:Panel ID="pnlSlot" runat="server" Visible="false">
                                    <asp:ListView ID="lvSlots" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <%--<h5><asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h5>--%>
                                                <h5>Slot Monitoring List</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap" id="mytable" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Program</th>
                                                        <th>Semester</th>
                                                        <th>Subject Code</th>
                                                        <th>Subject Name</th>
                                                        <th>Section</th>
                                                        <th>Faculty</th>
                                                        <th>Slot Capacity</th>
                                                        <th>Slot Taken</th>
                                                        <th>View Schedule</th>
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
                                                    <asp:Label ID="lblCoursename" runat="server" Text='<%#Eval("Course")%>'></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblsemestername" runat="server" Text='<%#Eval("Semester")%>'></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblccode" runat="server" Text='<%#Eval("SubjectCode")%>'></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblCourses" runat="server" Text='<%#Eval("SubjectName")%>'></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblsectionname" runat="server" Text='<%#Eval("Section")%>'></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lbluaname" runat="server" Text='<%#Eval("Faculty")%>'></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblslotcapa" runat="server" Text='<%#Eval("SlotCapacity")%>'></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblslottaken" runat="server" Text='<%#Eval("SlotTaken")%>'></asp:Label></td>
                                                <td class="text-center">
                                                    <%--<i class="fa fa-eye text-success" aria-hidden="true" data-toggle="modal" data-target="#ViewModal"></i>--%>
                                                    <asp:LinkButton ID="lnkview" runat="server" CommandArgument='<%#Eval("COURSENO") + "-" + Eval("SECTIONNO") %>' CommandName='<%# Eval("SESSIONNO") %>' ToolTip='<%# Eval("SEMESTERNO")%>'
                                                        OnClick="lnkview_Click"><i class="fa fa-eye text-success" aria-hidden="true" data-toggle="modal"></i></asp:LinkButton>
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

            <!-- The Modal -->
            <div class="modal fade" id="ViewModal">
                <div class="modal-dialog">
                    <div class="modal-content">

                        <!-- Modal Header -->
                        <div class="modal-header">
                            <h4 class="modal-title">Schedule</h4>
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                        </div>

                        <!-- Modal body -->
                        <div class="modal-body">
                            <div class="col-12">
                                <asp:Panel ID="PanelTimeSlot" runat="server" Visible="false">
                                    <asp:ListView ID="LvTimeSlot" runat="server">
                                        <LayoutTemplate>
                                            <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Day</th>
                                                        <th>Time Slot</th>
                                                        <th>Room</th>
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
                                                    <asp:Label ID="lblCoursename" runat="server" Text='<%#Eval("SLOT_DAY")%>'></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblsemestername" runat="server" Text='<%#Eval("TIMESLOT")%>'></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblccode" runat="server" Text='<%#Eval("ROOMNAME")%>'></asp:Label></td>
                                               
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                            
                        </div>

                        <!-- Modal footer -->
                        <div class="modal-footer">
                            <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                        </div>

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
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

