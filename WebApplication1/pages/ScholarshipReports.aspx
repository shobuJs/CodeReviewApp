<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ScholarshipReports.aspx.cs" Inherits="ACADEMIC_ScholarshipReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                </div>
                <div class="box-body">
                    <div>
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updScholarshipApprove"
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
                    <asp:UpdatePanel ID="updScholarshipApprove" runat="server">
                        <ContentTemplate>
                            <div class="box-body">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <asp:Label ID="lblSessionName" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlSession" runat="server" TabIndex="0" CssClass="form-control" data-select2-enable="true"
                                                AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                                Display="None" ErrorMessage="Please Select Session Name" InitialValue="0"
                                                ValidationGroup="report" />
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <asp:Label ID="lblDYCollege" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlCollege" runat="server" TabIndex="0" CssClass="form-control" data-select2-enable="true"
                                                AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvCollege" runat="server" ControlToValidate="ddlCollege"
                                                Display="None" ErrorMessage="Please Select College" InitialValue="0"
                                                ValidationGroup="report" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <asp:Label ID="lblDyProgram" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlProgram" runat="server" TabIndex="0" CssClass="form-control" data-select2-enable="true"
                                                AppendDataBoundItems="True" AutoPostBack="true">
                                                <asp:ListItem Value="0">Please select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <asp:Label ID="lblDYSemester" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlSemester" runat="server" TabIndex="0" CssClass="form-control" data-select2-enable="true"
                                                AppendDataBoundItems="True" AutoPostBack="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                         
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <asp:Label ID="lblScholarshipType" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlScholarshipType" runat="server" TabIndex="0" CssClass="form-control" data-select2-enable="true"
                                                AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlScholarshipType_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please select</asp:ListItem>
                                            </asp:DropDownList>

                                            <asp:RequiredFieldValidator ID="rfvScholarshipType" runat="server" ControlToValidate="ddlScholarshipType"
                                                Display="None" ErrorMessage="Please Select Scholarship Type" InitialValue="0"
                                                ValidationGroup="report" />
                                        </div>



                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <asp:Label ID="lblConcessionType" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlConcessionType" runat="server" TabIndex="0" CssClass="form-control" data-select2-enable="true"
                                                AppendDataBoundItems="True" AutoPostBack="true">
                                                <asp:ListItem Value="0">Please select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <asp:Label ID="lblReportType" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlReportType" runat="server" TabIndex="0" CssClass="form-control" data-select2-enable="true"
                                                AppendDataBoundItems="True" AutoPostBack="true">
                                                <asp:ListItem Value="0">Please select</asp:ListItem>
                                                <asp:ListItem Value="1">Consolidated List Of Scholars</asp:ListItem>
                                                <asp:ListItem Value="2">USA Based Scholarship List</asp:ListItem>
                                                <asp:ListItem Value="3">Externally Funded Scholarship List</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvReportType" runat="server" ControlToValidate="ddlReportType"
                                                Display="None" ErrorMessage="Please Select Report Type" InitialValue="0"
                                                ValidationGroup="report" />
                                        </div>


                                    </div>
                                </div>
                                <div class="col-12 btn-footer">

                                    <asp:Button ID="btnReport" runat="server" Text="Report" TabIndex="11" ValidationGroup="report" CssClass="btn btn-outline-primary" OnClick="btnReport_Click" />
                                    <%-- OnClick="btnReport_Click" />--%>
                                    <%-- <asp:Button ID="Button1" runat="server" Text="USA Based Scholarship List" TabIndex="11" ValidationGroup="report" CssClass="btn btn-outline-primary" />
                                    <asp:Button ID="Button2" runat="server" Text="Externally Funded Scholarship List" TabIndex="11" ValidationGroup="report" CssClass="btn btn-outline-primary" />

                                      <asp:LinkButton ID="btnShow" runat="server" ToolTip="Show" TabIndex="0" ValidationGroup="submit" CssClass="btn btn-outline-info" OnClick="btnShow_Click">Show</asp:LinkButton>--%>

                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                        TabIndex="0" CssClass="btn btn-outline-danger" />

                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="report" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </div>

                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnReport" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div id="divMsg" runat="server">
        </div>
</asp:Content>

