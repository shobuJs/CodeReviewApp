<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="OnlineAdmissionReportPortal.aspx.cs" Inherits="ACADEMIC_OnlineAdmissionReportPortal" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="z-index: 1; position: absolute; top: 10px; left: 550px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpnlUser"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>



    <asp:UpdatePanel ID="updpnlUser" runat="server">
        <ContentTemplate>
            <div class="row text-center">
                <asp:Label ID="lblMsg" runat="server" SkinID="Msglbl"></asp:Label>
            </div>
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-info">
                            <div class="box-header with-border">
                                <h3 class="box-title">Online Admission Portal Reports</h3>
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
                                            <asp:RequiredFieldValidator ID="rfvAdmbatch" runat="server" ErrorMessage="Please Select Admission batch" ControlToValidate="ddlAdmbatch" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="StatasticsReport"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-md-4">
                                            <label>Degree <span style="color: red;">*</span></label>
                                            <asp:DropDownList ID="ddlDegree" runat="server" CssClass="form-control" AppendDataBoundItems="True"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select Degree" ControlToValidate="ddlDegree" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="Report"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-md-4">
                                            <label>Branch </label>
                                            <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control" AppendDataBoundItems="True">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="box box-footer">
                                <div class="row">
                                    <p class="text-center">

                                        <asp:Button ID="btnApplystudList" runat="server" Text="Apply Student List" CssClass="btn btn-outline-primary" OnClick="btnApplystudList_Click" ValidationGroup="Report" />&nbsp;
                                        <asp:Button ID="btnRecStudList" runat="server" Text="Received Student List" CssClass="btn btn-outline-primary" OnClick="btnRecStudList_Click" ValidationGroup="Report" />&nbsp;
                                        <asp:Button ID="btnPendingStudList" runat="server" Text="Pending Student List" CssClass="btn btn-outline-primary" OnClick="btnPendingStudList_Click" ValidationGroup="Report" />&nbsp;
                                        <asp:Button ID="btnExport" runat="server" Text="Export to ExcelSheet" CssClass="btn btn-outline-primary" ValidationGroup="Report" OnClick="btnExport_Click" />&nbsp;
                                        <asp:Button ID="btnRegCountinExcel" runat="server" Text="Registration Count in Excel" CssClass="btn btn-outline-primary" OnClick="btnRegCountinExcel_Click" />&nbsp;
                                     <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True" CssClass="btn btn-outline-primary" ShowSummary="False" ValidationGroup="Report" />
                                    </p>
                                </div>
                                <div class="row">
                                    <p class="text-center">
                                        <asp:Button ID="btnApplirecvCount" runat="server" Text="Application Received Statastics" CssClass="btn btn-outline-primary" ValidationGroup="Report" OnClick="btnApplirecvCount_Click" />&nbsp;
                                        <asp:Button ID="btnDeptStudList" runat="server" Text="Departmentwise Student List" ValidationGroup="Report" CssClass="btn btn-outline-primary" OnClick="btnDeptStudList_Click" />&nbsp;
                                        <asp:Button ID="btnRegCount" runat="server" Text="Registration Count" CssClass="btn btn-outline-primary" OnClick="btnRegCount_Click" />&nbsp;
                                        <asp:Button ID="btnFeeCount" runat="server" Text="Registration Fees Count" CssClass="btn btn-outline-primary" OnClick="btnFeeCount_Click" />&nbsp;
                                        <asp:Button ID="btnRegCountDatewise" runat="server" Text="Reg Count Datewise" CssClass="btn btn-outline-primary" OnClick="btnRegCountDatewise_Click" />&nbsp;
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-outline-danger" />

                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="StatasticsReport" />

                                    </p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnApplystudList" />
            <asp:PostBackTrigger ControlID="btnRecStudList" />
            <asp:PostBackTrigger ControlID="btnPendingStudList" />
            <asp:PostBackTrigger ControlID="btnExport" />
            <asp:PostBackTrigger ControlID="btnApplirecvCount" />
            <asp:PostBackTrigger ControlID="btnDeptStudList" />
            <asp:PostBackTrigger ControlID="btnRegCount" />
            <asp:PostBackTrigger ControlID="btnRegCountDatewise" />
            <asp:PostBackTrigger ControlID="btnCancel" />
            <asp:PostBackTrigger ControlID="btnRegCountinExcel" />
            <asp:PostBackTrigger ControlID="btnFeeCount" />
        </Triggers>
    </asp:UpdatePanel>


    <div id="divMsg" runat="server"></div>
</asp:Content>

