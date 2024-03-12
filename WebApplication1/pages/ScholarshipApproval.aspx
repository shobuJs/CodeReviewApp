<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ScholarshipApproval.aspx.cs" Inherits="ACADEMIC_ScholarshipApproval" %>

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
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlSession"
                                                Display="None" ErrorMessage="Please Select Session Name" InitialValue="0"
                                                ValidationGroup="submit" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <asp:Label ID="lblDYSemester" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlSemester" runat="server" TabIndex="0" CssClass="form-control" data-select2-enable="true"
                                                AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSemester"
                                                Display="None" ErrorMessage="Please Select Semester" InitialValue="0"
                                                ValidationGroup="submit" />
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup> </sup>
                                                <asp:Label ID="lblDYCollege" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlCollege" runat="server" TabIndex="0" CssClass="form-control" data-select2-enable="true"
                                                AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup> </sup>
                                                <asp:Label ID="lblDyProgram" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                            <asp:DropDownList ID="ddlProgram" runat="server" TabIndex="0" CssClass="form-control" data-select2-enable="true"
                                                AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer">

                                    <asp:LinkButton ID="btnShow" runat="server" ToolTip="Show" TabIndex="0" ValidationGroup="submit" CssClass="btn btn-outline-info" OnClick="btnShow_Click">Show</asp:LinkButton>

                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                        TabIndex="0" CssClass="btn btn-outline-danger" />

                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </div>
                                                                    <div class="col-12 mt-3">
                                        <asp:ListView ID="lvBindDetails" runat="server">
                                            <LayoutTemplate>
                                                <table class="table table-striped table-bordered nowrap display" id="mytable">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>SrNo</th>
                                                            <th>Student ID</th>
                                                            <th>Student Name</th>
                                                            <th>Semester</th>
                                                            <th>Status</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%# Container.DataItemIndex + 1 %></td>
                                                    <td><a target="_blank" href='<%# Eval("SCHOLARSHIP_URL")%>'><%#Eval("ENROLLNO") %></a></td>
                                                    <td><%#Eval("STUDNAME") %></td>
                                                    <td><%#Eval("SEMESTERNAME") %></td>
                                                    <td><%#Eval("SCHOLARSHIP_STATUS") %></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

