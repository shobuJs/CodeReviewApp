<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Grouping_Automation_Criteria.aspx.cs" Inherits="Grouping_Automation_Criteria" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
    <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>

    <style>
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updGroupingAutomation"
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
    <asp:UpdatePanel ID="updGroupingAutomation" runat="server">
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
                                            <label>Criteria Name</label>
                                        </div>
                                        <asp:TextBox ID="txtCriteriaName" runat="server" CssClass="form-control" TabIndex="4"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCriteriaName"
                                            Display="None" ErrorMessage="Please Enter Criteria Name" ValidationGroup="btnCriteria" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Faculty</label>
                                        </div>
                                        <asp:ListBox ID="ddlfaculty" runat="server" AppendDataBoundItems="true" CssClass="form-control multi-select-demo" OnSelectedIndexChanged="ddlfaculty_SelectedIndexChanged" AutoPostBack="true" SelectionMode="multiple"></asp:ListBox>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Program</label>
                                        </div>
                                        <asp:ListBox ID="ddlprogram" runat="server" AppendDataBoundItems="true" CssClass="form-control multi-select-demo" SelectionMode="multiple"></asp:ListBox>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Campus/Center</label>
                                        </div>
                                        <asp:ListBox ID="ddlCampus" runat="server" AppendDataBoundItems="true" CssClass="form-control multi-select-demo" SelectionMode="multiple"></asp:ListBox>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Week Day / Week End</label>
                                        </div>
                                        <asp:ListBox ID="ddlWDWE" runat="server" AppendDataBoundItems="true" CssClass="form-control multi-select-demo" SelectionMode="multiple"></asp:ListBox>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>UG A/L Syllabus</label>
                                        </div>
                                        <asp:DropDownList ID="ddlUGSyllabus" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlUGSyllabus"
                                            Display="None" ErrorMessage="Please Select UG A/L Syllabus" ValidationGroup="btnCriteria" InitialValue="0" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>UG (A/L) Stream</label>
                                        </div>
                                        <asp:ListBox ID="ddlUGStream" runat="server" AppendDataBoundItems="true" CssClass="form-control multi-select-demo" SelectionMode="multiple"></asp:ListBox>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Allocation Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlAllocationType" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlAllocationType"
                                            Display="None" ErrorMessage="Please Select Allocation Type" ValidationGroup="btnCriteria" InitialValue="0" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn btn-outline-info" OnClick="btnSubmit_Click" ValidationGroup="btnCriteria">Submit</asp:LinkButton>
                                <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancel_Click">Cancel</asp:LinkButton>
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="btnCriteria" />
                            </div>


                            <div class="col-md-12">
                                <asp:Panel ID="Panel6" runat="server" Visible="false">
                                    <asp:ListView ID="LvAndDetails" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Search List</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" id="mytable" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Edit</th>
                                                        <th>Criteria Name</th>
                                                        <th>Faculty/School Name</th>
                                                        <th>Program</th>
                                                        <th>Center/Campus</th>
                                                        <th>Week Day/Week End</th>
                                                        <th>UG A/L Syllabus</th>
                                                        <th>UG (A/L) Stream </th>
                                                        <th>Allocation Type</th>
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
                                                <asp:LinkButton ID="lnkedit" runat="server" CssClass="fa fa-remove"  OnClick="lnkedit_Click" OnClientClick="return confirm('Are you sure you want to delete this record ?')"  ToolTip="Delete" CommandName='<%# Container.DataItemIndex + 1 %>' CommandArgument='<%#Eval("CRITERIA_NO") %>'></asp:LinkButton>

                                                </td>
                                                <td><%#Eval("CRITERIA_NAME") %></td>
                                                <td>
                                                 <%#Eval("COLLEGE_NAME") %>
                                                </td>
                                                <td>
                                                    <%#Eval("PROGRAM") %>
                                                </td>
                                                <td>
                                                    <%#Eval("CAMPUSNAME") %>
                                                </td>
                                                <td>
                                                    <%#Eval("WEEKDAYSNAME") %>
                                                </td>
                                                <td>
                                                    <%#Eval("ALTYPENAME") %>
                                                </td>
                                                <td>
                                                    <%#Eval("STREAMNAME") %>
                                                </td>
                                                <td>
                                                    <%#Eval("ORIANTATION_TYPE") %>
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
        </ContentTemplate>
    </asp:UpdatePanel>
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
</asp:Content>

