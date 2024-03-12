<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Advising_Transferees.aspx.cs" Inherits="Advising_Transferees" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
                                    <sup>*</sup>
                                    <asp:Label ID="lblDYSession" runat="server" Font-Bold="true"></asp:Label>

                                </div>
                                <asp:DropDownList ID="ddlIntake" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="1" AppendDataBoundItems="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlIntake"
                                    Display="None" ErrorMessage="Please Select Intake" ValidationGroup="Show"
                                    SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>*</sup>
                                    <asp:Label ID="lblDYCollege" runat="server" Font-Bold="true"></asp:Label>
                                    <%--   <label>College</label>--%>
                                </div>
                                <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="1" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCollege"
                                    Display="None" ErrorMessage="Please Select College" ValidationGroup="Show"
                                    SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>*</sup>
                                    <asp:Label ID="lblDyProgram" runat="server" Font-Bold="true"></asp:Label>
                                    <%--                    <label>Program</label>--%>
                                </div>
                                <asp:DropDownList ID="ddlProgram" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="1" AppendDataBoundItems="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlProgram"
                                    Display="None" ErrorMessage="Please Select Program" ValidationGroup="Show"
                                    SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 btn-footer">
                        <asp:LinkButton ID="btnShow" runat="server" CssClass="btn btn-outline-info" TabIndex="1" ValidationGroup="Show" OnClick="btnShow_Click">Show</asp:LinkButton>
                        <asp:LinkButton ID="btncanceldata" runat="server" CssClass="btn btn-outline-danger" TabIndex="1" OnClick="btncanceldata_Click">Cancel</asp:LinkButton>
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Show"
                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                    </div>


                    <div class="col-12">
                        <asp:Panel ID="Panel1" runat="server">
                            <asp:ListView ID="lvStudent" runat="server">
                                <LayoutTemplate>
                                    <div id="demo-grid">
                                        <div class="sub-heading" id="dem">
                                            <h5>
                                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h5>
                                        </div>

                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="myTable1">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>
                                                        <asp:Label ID="lblStudentRId" runat="server"></asp:Label>
                                                    </th>
                                                    <th>
                                                        <asp:Label ID="lblStudName" runat="server"></asp:Label></th>
                                                    <td>
                                                        <asp:Label ID="lblDyProgram" runat="server"></asp:Label></td>
                                                    <th>
                                                        <asp:Label ID="lblDYSemesterOrYear" runat="server"></asp:Label></th>
                                                    <%--   <th><asp:Label ID="lblActive" runat="server"></asp:Label></th>--%>

                                                    <th>
                                                        <asp:Label ID="lblAct" runat="server"></asp:Label>Approval Status</th>
                                                    <th>
                                                        <asp:Label ID="lblDyTrasfree" runat="server">Advising Status</asp:Label></th>

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

                                            <a target="_blank" href='<%# Eval("SITE_URL")%>'>
                                                <asp:Label ID="lnkApplicationNo" CssClass="PopUp" runat="server" Text='<%# Eval("REGNO")%>' ToolTip='<%# Eval("REGNO")%>'></asp:Label>
                                            </a>
                                        </td>
                                        <td>
                                            <%# Eval("STUDNAME")%>
                                        </td>

                                        <td>
                                            <%# Eval("PROGRAM")%>
                                        </td>

                                        <td>
                                            <%# Eval("YEAR")%>
                                        </td>

                                        <td>
                                            <%# Eval("EVALUATION_NAME")%>
                                        </td>

                                        <td>
                                            <%# Eval("TRASFREESTATUS")%>
                                        </td>

                                        <%-- <td></td>--%>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>
                    </div>

                </div>
            </div>
        </div>
    </div>

</asp:Content>

