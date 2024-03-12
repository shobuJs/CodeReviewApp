<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="CertificateMaster.aspx.cs" Inherits="CertificateMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updCertificateMaster"
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
    <asp:UpdatePanel ID="updCertificateMaster" runat="server">
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
                                            <label>Document Name</label>
                                        </div>
                                        <asp:TextBox ID="txtDocumentName" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDocumentName"
                                            Display="None" ErrorMessage="Please Enter Document Name" InitialValue="" ValidationGroup="Submit" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlType"
                                            Display="None" ErrorMessage="Please Select Type" InitialValue="0" ValidationGroup="Submit" />
                                    </div>
                                    <div class="form-group col-lg-1 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Is Paid</label>
                                        </div>
                                        <asp:CheckBox ID="chkPaid" runat="server" OnCheckedChanged="chkPaid_CheckedChanged" AutoPostBack="true" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false" id="DivAmount">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Amount</label>
                                        </div>
                                        <asp:TextBox ID="txtAmt" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtAmt"
                                            Display="None" ErrorMessage="Please Enter Amount" InitialValue="" ValidationGroup="Submit" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn btn-outline-info" ValidationGroup="Submit" OnClick="btnSubmit_Click">Submit</asp:LinkButton>
                                <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancel_Click">Cancel</asp:LinkButton>
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Submit"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>
                            <div class="col-12">
                                <asp:Panel ID="Panel1" runat="server">
                                    <asp:ListView ID="lvDocument" runat="server" Visible="true">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display " style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Edit</th>
                                                        <th>Document Name</th>
                                                        <th>Type</th>
                                                        <th>Paid</th>
                                                        <th>Amount</th>
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
                                                    <asp:LinkButton ID="btnEdit" runat="server" OnClick="btnEdit_Click" CssClass="fa fa-pencil-square-o" CommandArgument='<%#Eval("DOC_NO") %>'></asp:LinkButton></td>
                                                <td><%#Eval("DOC_NAME") %></td>
                                                <td><%#Eval("DOC_TYPENAME") %></td>
                                                <td><%#Eval("STATUS") %></td>
                                                <td><%#Eval("AMOUNTS") %></td>
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
</asp:Content>

