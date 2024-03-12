<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="EnrollmentReports.aspx.cs" Inherits="EnrollmentReports" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="<%=Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.css")%>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.js")%>"></script>

    <style>
        .switch {
            position: relative;
            display: inline-block;
            width: 40px;
            height: 19px;
        }

            .switch input {
                opacity: 0;
                width: 0;
                height: 0;
            }

        .slider {
            position: absolute;
            cursor: pointer;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background-color: #ccc;
            -webkit-transition: .4s;
            transition: .4s;
        }

            .slider:before {
                position: absolute;
                content: "";
                height: 15px;
                width: 15px;
                left: 1px;
                bottom: 2px;
                background-color: white;
                -webkit-transition: .4s;
                transition: .4s;
            }

        input:checked + .slider {
            background-color: #2196F3;
        }

        input:focus + .slider {
            box-shadow: 0 0 1px #2196F3;
        }

        input:checked + .slider:before {
            -webkit-transform: translateX(22px);
            -ms-transform: translateX(22px);
            transform: translateX(22px);
        }

        /* Rounded sliders */
        .slider.round {
            border-radius: 20px;
        }

            .slider.round:before {
                border-radius: 20px;
            }
    </style>
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
                                              <asp:Label ID="lblReportType" runat="server" Font-Bold="true"></asp:Label>
                                            
                                         <%--   <label>Report Type </label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlReportType" runat="server" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlReportType_SelectedIndexChanged" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlReportType"
                                            Display="None" ErrorMessage="Please Select Report Type" ValidationGroup="Report"
                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                             <asp:Label ID="lblDYSession" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlIntake" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlIntake"
                                            Display="None" ErrorMessage="Please Select Intake" ValidationGroup="Report"
                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                     <%--       <label>School/College </label>--%>
                                              <asp:Label ID="lblDYCollegeNew" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:ListBox ID="lstSchoolCollege" runat="server" CssClass="form-control multi-select-demo" SelectionMode="Multiple">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:ListBox>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="lstSchoolCollege"
                                            Display="None" ErrorMessage="Please Select School/College " ValidationGroup="Report"
                                            SetFocusOnError="True" InitialValue=""></asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Enable Date Range </label>
                                        </div>

                                        <label class="switch">
                                           <asp:CheckBox runat="server" ID="che" OnCheckedChanged="che_CheckedChanged" AutoPostBack="true" />
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="StartDate" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Start Date </label>
                                        </div>
                                        <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" type="date"></asp:TextBox>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtStartDate"
                                            Display="None" ErrorMessage="Please Enter Start Date  " ValidationGroup="Report"
                                            SetFocusOnError="True" InitialValue=""></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="EndDate" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>End Date </label>
                                        </div>
                                        <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" type="date"></asp:TextBox>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtEndDate"
                                            Display="None" ErrorMessage="Please Enter End Date  " ValidationGroup="Report"
                                            SetFocusOnError="True" InitialValue=""></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:LinkButton ID="btnReport" runat="server" CssClass="btn btn-outline-primary" TabIndex="1" OnClick="btnReport_Click" ValidationGroup="Report">Report</asp:LinkButton>
                                <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-outline-danger" TabIndex="1" OnClick="btnCancel_Click">Cancel</asp:LinkButton>
                                <asp:ValidationSummary ID="ValidationSummary4" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Report" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReport"/>
        </Triggers>
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

