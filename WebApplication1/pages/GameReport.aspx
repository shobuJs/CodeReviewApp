<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="GameReport.aspx.cs" Inherits="GameReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpnlSports"
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
    <asp:UpdatePanel ID="updpnlSports" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row" id="myDiv">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Sports Report</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12"">
                                        <div class="label-dynamic">
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" CssClass="form-control" AppendDataBoundItems="true" TabIndex="1" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="RfvDegree" runat="server" ControlToValidate="ddldegree"
                                            Display="None" ErrorMessage="Please Select Degree" ValidationGroup="show"
                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12"">
                                        <div class="label-dynamic">
                                            <label>Branch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" TabIndex="2" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%-- <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" ErrorMessage="Please Select Branch" ValidationGroup="show"
                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12"">
                                        <div class="label-dynamic">
                                            <label>Name of the Sport</label>
                                        </div>
                                        <asp:DropDownList ID="ddlGame" runat="server" CssClass="form-control" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlGame_SelectedIndexChanged" TabIndex="3" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                       <%-- <asp:RequiredFieldValidator ID="rfvGame" runat="server" ControlToValidate="ddlGame"
                                            Display="None" ErrorMessage="Please Select Name of Sport" ValidationGroup="show"
                                            SetFocusOnError="true" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnGameReport" runat="server" Text="Sports Report" OnClick="btnGameReport_Click" ToolTip="Sports Report" ValidationGroup="show"
                                    TabIndex="4" CssClass="btn btn-outline-info" />
                                <%--<asp:Button ID="btnsubmitfrst" runat="server" Text="Submit" ToolTip="Submit" 
                                    TabIndex="5" CssClass="btn btn-outline-info" />--%>
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" ToolTip="Cancel" CausesValidation="false"
                                    TabIndex="6" CssClass="btn btn-outline-danger" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="show"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server" />
</asp:Content>

