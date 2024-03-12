<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Transfer_Module_Mapping.aspx.cs" Inherits="Projects_Transfer_Module_Mapping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .fa-plus {
            color: #17a2b8;
            border: 1px solid #17a2b8;
            border-radius: 50%;
            padding: 6px 8px;
        }

        #ctl00_ContentPlaceHolder1_Panel2 .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updTrans"
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
    <asp:UpdatePanel ID="updTrans" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
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
                                            <asp:Label ID="lblDYCollege" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlcollege" runat="server" TabIndex="1" CssClass="form-control select2 select-click"
                                            AppendDataBoundItems="True" ToolTip="Please Select Faculty /School Name">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlcollege"
                                            Display="None" ErrorMessage="Please Select Faculty /School Name" InitialValue="0"
                                            ValidationGroup="ShowList" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYSemester" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlsemester" runat="server" TabIndex="2" CssClass="form-control select2 select-click"
                                            AppendDataBoundItems="True" ToolTip="Please Select Semester">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlsemester"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0"
                                            ValidationGroup="ShowList" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:LinkButton ID="btnShowList" runat="server" ToolTip="Show" ValidationGroup="ShowList"
                                    CssClass="btn btn-outline-info"  OnClick="btnShowList_Click" TabIndex="3" >Show</asp:LinkButton>
                                <asp:Button ID="btnCancel1" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                    TabIndex="4" OnClick="btnCancel_Click1" CssClass="btn btn-outline-danger" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="ShowList"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>
                            <div class="col-12">
                                <asp:Panel ID="Panel1" runat="server" Visible="false">
                                    <asp:ListView ID="lvTransModule" runat="server">
                                        <LayoutTemplate>

                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Student ID</th>
                                                        <th>Student Name</th>
                                                        <th>Previous Program</th>
                                                        <th>New Program</th>
                                                        <th>Action</th>
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
                                                    <asp:Label runat="server" ID="lblRegno" Text='<%#Eval("REGNNO") %>'></asp:Label></td>
                                                <td><%#Eval("NAME_WITH_INITIAL") %></td>
                                                <td><%#Eval("PREVIOUS_PROGRAM") %>
                                                </td>
                                                <td><%#Eval("NEW_PROGRAM") %></td>
                                                <td>
                                                    <asp:LinkButton ID="btnView" runat="server" CssClass="btn btn-outline-info" CommandArgument='<%# Eval("IDNO") %>' OnClick="btnView_Click">Map</asp:LinkButton>
                                                    <%--<asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-outline-info" CommandArgument='<%# Eval("IDNO") %>' data-toggle="modal" data-target="#Map_Modal" OnClick="btnView_Click">Map</asp:LinkButton>--%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>

                                </asp:Panel>

                            </div>
                        </div>
                    </div>

                    <!-- The Modal -->
                    <div class="modal fade" id="Map_Modal">
                        <div class="modal-dialog modal-xl">
                            <div class="modal-content">

                                <!-- Modal Header -->
                                <div class="modal-header">
                                    <h3 class="modal-title font-weight-bold">Module Mapping</h3>
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                </div>

                                <div class="col-12">
                                    <asp:Panel ID="Panel2" runat="server">
                                        <asp:ListView ID="lvModule" runat="server" Visible="true">
                                            <LayoutTemplate>
                                                <div class="sub-heading">
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                    <thead class="bg-light-blue">
                                                        <tr>

                                                            <th>New Module</th>
                                                            <th>Old Module</th>
                                                            <th>Mapping Status</th>
                                                            <%-- <th></th>--%>
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
                                                        <asp:DropDownList ID="ddlNewModuleName" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblOldModule" runat="server" Text='<%#Eval("COURSENAME") %>'></asp:Label>
                                                        <asp:Label ID="lblOldCourse" runat="server" Text='<%#Eval("COURSENO") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblNewCourse" runat="server" Text='<%#Eval("NEW_COURSENO") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblModuleStat" runat="server" Text='<%#Eval("MAPPING_STATUS") %>' Visible="false"></asp:Label>
                                                    </td>

                                                    <td>
                                                        <asp:DropDownList ID="ddlMappingStatus" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>

                                                        </asp:DropDownList>
                                                    </td>
                                                    <%--  <td></td>--%>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>

                                    </asp:Panel>

                                </div>

                                <!-- Modal footer -->
                                <div class="modal-footer">
                                    <div class="col-12 btn-footer">
                                        <asp:LinkButton ID="btnSave" runat="server" CssClass="btn btn-outline-primary" OnClick="btnSave_Click">Save</asp:LinkButton>
                                        <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancel_Click">Cancel</asp:LinkButton>
                                        <%--<button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>--%>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
            <asp:PostBackTrigger ControlID="btnCancel" />

        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

