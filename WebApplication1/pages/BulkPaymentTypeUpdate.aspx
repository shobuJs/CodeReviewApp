<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="BulkPaymentTypeUpdate.aspx.cs" Inherits="ACADEMIC_BulkPaymentTypeUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../Content/jquery.js" type="text/javascript"></script>

    <script src="../Content/jquery.dataTables.js" language="javascript" type="text/javascript"></script>

    <script type="text/javascript" charset="utf-8">
        $(document).ready(function () {
            $(".display").dataTable({
                "bJQueryUI": true,
                "sPaginationType": "full_numbers"
            });

        });
    </script>
    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">BULK PAYMENT TYPE UPDATE </h3>
                </div>

                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-4">
                                
                                <label>Adm. Batch<span style="color: red"> *</span></label>
                                <asp:DropDownList ID="ddladmbatch" runat="server" AppendDataBoundItems="true" CssClass="form-control" TabIndex="1">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvadmbatch" runat="server" ControlToValidate="ddladmbatch"
                                    Display="None" ErrorMessage="Please Select Admission Batch." InitialValue="0"
                                    SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-4">
                                <span style="color: red"></span>
                                <label>Degree </label>
                                <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                    TabIndex="2" AutoPostBack="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>

                            <div class="col-md-4 form-group">
                                <span style="color: red"></span>
                                <label>Branch</label>
                                <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" CssClass="form-control" TabIndex="3">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            
                             <div class="col-md-4 form-group">
                                <span style="color: red"></span>
                                <label>Student Type</label>
                                <asp:DropDownList ID="ddlStudenttype" runat="server" AppendDataBoundItems="true" CssClass="form-control" TabIndex="4">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>                         

                        </div>
                    </div>
                    <br />
                    &nbsp;
                                <div class="row text-center">
                                    <asp:Button ID="btnShow" runat="server" Text="Show" ValidationGroup="Submit" CssClass="btn btn-outline-info" OnClick="btnShow_Click" TabIndex="5" />
                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" CssClass="btn btn-outline-primary" TabIndex="6" Enabled="false" />
                                    <asp:Button ID="btnReport" runat="server" Text="Report" OnClick="btnReport_Click" CssClass="btn btn-outline-primary" TabIndex="7" Enabled="false" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" TabIndex="8" CssClass="btn btn-outline-danger" CausesValidation="false" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Submit" />
                                </div>
                    <div class="row">
                        <div class="container">
                            <div class="container-fluid">

                                <asp:Panel runat="server" ID="pnlStudentList">
                                    <asp:ListView ID="lvPaymenttype" runat="server" OnItemDataBound="lvPaymenttype_ItemDataBound">
                                        <LayoutTemplate>

                                            <div class="titlebar">
                                                Student List
                                            </div>
                                            <table class="table table-hovered table-bordered">
                                                <thead>
                                                    <tr class="bg-light-blue">
                                                        <th>Sr.No
                                                        </th>
                                                        <th style="width:10%;">Univ. Reg. No.
                                                        </th>
                                                        <th>Student Name
                                                        </th>
                                                        <th>Category
                                                        </th>
                                                        <th>Admission Quota
                                                        </th>
                                                        <th>Branch
                                                        </th>
                                                        <th>Year
                                                        </th>
                                                        <th>Payment Type
                                                        </th>
                                                    </tr>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </thead>
                                            </table>
                                            </div>
                              
                                             
                                            
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr class="item">
                                                <td>
                                                    <%# Container.DataItemIndex+1%>
                                                </td>
                                                <td>
                                                    <%# Eval("REGNO")%>
                                                    <asp:HiddenField runat="server" ID="hdfIdno" Value='<%# Eval("IDNO")%>' />
                                                </td>
                                                <td>
                                                    <%# Eval("STUDNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("CATEGORY")%>
                                                </td>
                                                <td>
                                                    <%#Eval("ADMQUOTA") %>
                                                </td>
                                                <td style="width:20%">
                                                    <%# Eval("BRANCH")%>
                                                </td>
                                                <td style="width:5%">
                                                    <%# Eval("YEAR")%>
                                                </td>
                                                <td>
                                                    <asp:DropDownList runat="server" ID="ddlPaymentType" CssClass="form-control" AppendDataBoundItems="true"
                                                        Width="60%" ToolTip='<%# Eval("PTYPE")%>'>
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
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
        </div>

        <div id="divMsg" runat="server">
        </div>
    </div>
</asp:Content>
