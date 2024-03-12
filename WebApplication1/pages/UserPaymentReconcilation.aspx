<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="UserPaymentReconcilation.aspx.cs"
    Inherits="ACADEMIC_UserPaymentReconcilation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="z-index: 1; position: absolute; top: 10px; left: 550px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updReconcile"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel ID="updReconcile" runat="server">
        <ContentTemplate>
            <div class="row text-center">
                <asp:Label ID="lblMsg" runat="server" SkinID="Msglbl"></asp:Label>
            </div>
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-info">
                            <div class="box-header with-border">
                                <h3 class="box-title"><b>USER PAYMENT RECONCILIATION</b></h3>
                                <div class="box-tools pull-right">
                                </div>
                            </div>
                            <br />
                            <div style="color: Red; font-weight: bold; margin-top: -20px; margin-right: -5px;">
                                &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                            </div>
                            <div class="box-body">
                                <div class="col-md-12">
                                    <div class="row">
                                        <div class="form-group col-md-4">
                                            <label>Admission Batch <span style="color: red;">*</span></label>
                                            <asp:DropDownList ID="ddlAdmbatch" runat="server" CssClass="form-control" AppendDataBoundItems="True">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlAdmbatch" runat="server" ErrorMessage="Please Select Admission batch" ControlToValidate="ddlAdmbatch" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="Report"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-md-4">
                                            <label>Payment Type <span style="color: red;">*</span></label>
                                            <asp:DropDownList ID="ddlPaymentType" runat="server" CssClass="form-control" AppendDataBoundItems="True">
                                                <asp:ListItem Value="">Please Select</asp:ListItem>
                                                <asp:ListItem Value="0">DD PAYMENT</asp:ListItem>
                                                <asp:ListItem Value="1">CASH PAYMENT</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-md-4">
                                            <label>Search by Application ID</label>
                                            <asp:TextBox ID="txtAppliid" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="box box-footer">
                                <p class="text-center">
                                    <asp:Button ID="btnShow" runat="server" Text="Show Student" OnClick="btnApplystudList_Click" CssClass="btn btn-outline-primary" ValidationGroup="Report" />
                                    &nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="7" CssClass="btn btn-outline-danger" CausesValidation="false"
                                        OnClick="btnCancel_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Report" />
                                </p>
                                <div class="col-md-12">
                                    <asp:Panel runat="server" ID="pnlStudentList" Width="100%" Visible="False">

                                        <asp:ListView ID="lvStudent" runat="server">
                                            <EmptyDataTemplate>
                                                <br />
                                                <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="Student Not found" />
                                            </EmptyDataTemplate>
                                            <LayoutTemplate>
                                                <div id="demp_grid" class="demo-grid">
                                                    <h3 style="text-shadow: 2px 2px 3px #0b93f8;">Student List </h3>
                                                    <table class="table table-hover table-bordered table-responsive">
                                                        <thead>
                                                            <tr class="bg-light-blue">
                                                                <th>Name
                                                                </th>
                                                                <th>Application Id
                                                                </th>

                                                                <th>DD No
                                                                </th>
                                                                <th>DD Amount
                                                                </th>
                                                                <th>Transaction Date
                                                                </th>
                                                                <th>Bank Name
                                                                </th>
                                                                <th>Branch
                                                                </th>
                                                                <th>Remark
                                                                </th>
                                                                <th>Reconcile
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </div>

                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                    <td>
                                                        <%# Eval("FIRSTNAME")%>
                                                        <asp:HiddenField ID="hdfUserno" runat="server" Value='<%# Eval("USERNO")%>' />
                                                    </td>
                                                    <td>
                                                        <%# Eval("USERNAME")%>
                                                    </td>

                                                    <td>
                                                        <%# Eval("DD_CHEQUENO")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("AMOUNT")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("TRANSDATE")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("BANKNAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("BRANCHNAME")%>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtremark" Text='<%# Eval("REMARK")%>'>
                                                        </asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox runat="server" ID="chkRecon" ToolTip='<%# Eval("RECON")%>' Font-Bold="true" />
                                                        <asp:HiddenField runat="server" ID="reconval" Value='<%# Eval("RECON")%>' />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <AlternatingItemTemplate>
                                                <tr class="altitem" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFD2'">
                                                    <td>
                                                        <%# Eval("FIRSTNAME")%>
                                                        <asp:HiddenField ID="hdfUserno" runat="server" Value='<%# Eval("USERNO")%>' />
                                                    </td>
                                                    <td>
                                                        <%# Eval("USERNAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("DD_CHEQUENO")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("AMOUNT")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("TRANSDATE")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("BANKNAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("BRANCHNAME")%>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtremark" Text='<%# Eval("REMARK")%>'>
                                                        </asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox runat="server" ID="chkRecon" ToolTip='<%# Eval("RECON")%>' Font-Bold="true" />
                                                        <asp:HiddenField runat="server" ID="reconval" Value='<%# Eval("RECON")%>' />
                                                    </td>
                                                </tr>
                                            </AlternatingItemTemplate>
                                        </asp:ListView>

                                    </asp:Panel>
                                </div>
                                <div class="col-md-12">
                                    <asp:Panel runat="server" ID="pnlStudentlistCash" Width="100%"
                                        Visible="False">

                                        <asp:ListView ID="lvStudentCash" runat="server">
                                            <EmptyDataTemplate>
                                                <br />
                                                <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="Student Not found" />
                                            </EmptyDataTemplate>
                                            <LayoutTemplate>
                                                <div id="demp_grid" class="demo-grid">
                                                    <h3 style="text-shadow: 2px 2px 3px #0b93f8;">Student List </h3>
                                                    <table class="table table-hover table-bordered table-responsive">
                                                        <thead>
                                                            <tr class="bg-light-blue">
                                                                <th>Name
                                                                </th>
                                                                <th>Application Id
                                                                </th>
                                                                <th>Receipt No
                                                                </th>
                                                                <th>Amount
                                                                </th>
                                                                <th>Transaction Date
                                                                </th>
                                                                <th>Remark
                                                                </th>
                                                                <th>Reconcile
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                    <td>
                                                        <%# Eval("FIRSTNAME")%>
                                                        <asp:HiddenField ID="hdfUserno" runat="server" Value='<%# Eval("USERNO")%>' />
                                                    </td>
                                                    <td>
                                                        <%# Eval("USERNAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("RECEIPTNO")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("AMOUNT")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("TRANSDATE")%>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtremark" Text='<%# Eval("REMARK")%>'>
                                                        </asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox runat="server" ID="chkRecon" ToolTip='<%# Eval("RECON")%>' Font-Bold="true" />
                                                        <asp:HiddenField runat="server" ID="reconval" Value='<%# Eval("RECON")%>' />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <AlternatingItemTemplate>
                                                <tr class="altitem" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFD2'">
                                                    <td>
                                                        <%# Eval("FIRSTNAME")%>
                                                        <asp:HiddenField ID="hdfUserno" runat="server" Value='<%# Eval("USERNO")%>' />
                                                    </td>
                                                    <td>
                                                        <%# Eval("USERNAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("RECEIPTNO")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("AMOUNT")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("TRANSDATE")%>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtremark" Text='<%# Eval("REMARK")%>'>
                                                        </asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox runat="server" ID="chkRecon" ToolTip='<%# Eval("RECON")%>' Font-Bold="true" />
                                                        <asp:HiddenField runat="server" ID="reconval" Value='<%# Eval("RECON")%>' />
                                                    </td>
                                                </tr>
                                            </AlternatingItemTemplate>
                                        </asp:ListView>

                                    </asp:Panel>
                                </div>
                                <div class="row">
                                    <p class="text-center">
                                        <asp:Button runat="server" ID="btnRecon" CssClass="btn btn-success" Visible="false" TabIndex="5"
                                            Text="Reconcile" ValidationGroup="Submit" OnClick="btnRecon_Click" />
                                        &nbsp;<asp:Button runat="server" ID="btnReport" CssClass="btn btn-outline-info" Visible="false" TabIndex="6"
                                            Text="Report" ValidationGroup="report" OnClick="btnReport_Click" />
                                        &nbsp;<asp:Button runat="server" ID="btnexport" Visible="false" CssClass="btn btn-outline-primary" TabIndex="6"
                                            Text="ExportToExcel" ValidationGroup="report" OnClick="btnexport_Click" />
                                        <asp:ValidationSummary ID="valsub" runat="server" DisplayMode="List" CssClass="btn btn-outline-danger" ShowMessageBox="true"
                                            ShowSummary="false" ValidationGroup="Submit" />



                                    </p>
                                    </>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="divMsg" runat="server"></div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnShow" />
            <asp:PostBackTrigger ControlID="btnRecon" />
            <asp:PostBackTrigger ControlID="btnReport" />
            <asp:PostBackTrigger ControlID="btnexport" />
            <asp:PostBackTrigger ControlID="btnCancel" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>

