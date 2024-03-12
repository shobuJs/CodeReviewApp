<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SMSLogReport.aspx.cs" Inherits="ACADEMIC_SendBySMS" MasterPageFile="~/SiteMasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="cntSendBySMS" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">

        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">SMS Log Report</h3>
                        </div>

                        <div class="box-body">
                            <div class="form-group col-md-6">
                                <label>From Date</label>
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtFromDate" runat="server" TabIndex="1" ValidationGroup="submit" />
                                    <ajaxToolKit:CalendarExtender ID="ceExamDate" runat="server" Format="dd/MM/yyyy"
                                        TargetControlID="txtFromDate" PopupButtonID="imgExamDate" />
                                    <ajaxToolKit:MaskedEditExtender ID="meFromDate" runat="server" TargetControlID="txtFromDate"
                                        Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                        MaskType="Date" />
                                    <ajaxToolKit:MaskedEditValidator ID="mvFromDate" runat="server" EmptyValueMessage="Please Enter Start Date"
                                        ControlExtender="meFromDate" ControlToValidate="txtFromDate" IsValidEmpty="false"
                                        InvalidValueMessage="Start Date is invalid" Display="None" ErrorMessage="Please Enter Start Date"
                                        InvalidValueBlurredMessage="*" ValidationGroup="Report" SetFocusOnError="true" />
                                </div>
                            </div>
                            <div class="form-group col-md-6">
                                <label>To Date</label>
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtToDate" runat="server" TabIndex="2" ValidationGroup="Report" />
                                    <ajaxToolKit:CalendarExtender ID="ceToDate" runat="server" Format="dd/MM/yyyy"
                                        TargetControlID="txtToDate" PopupButtonID="imgExamDate" />
                                    <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" TargetControlID="txtToDate"
                                        Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                        MaskType="Date" />
                                    <ajaxToolKit:MaskedEditValidator ID="mvToDate" runat="server" EmptyValueMessage="Please Enter End Date"
                                        ControlExtender="meToDate" ControlToValidate="txtToDate" IsValidEmpty="false"
                                        InvalidValueMessage="End Date is invalid" Display="None" ErrorMessage="Please Enter End Date"
                                        InvalidValueBlurredMessage="*" ValidationGroup="Report" SetFocusOnError="true" />
                                </div>
                            </div>

                            <div class="panel-footer text-center">
                                <asp:Button ID="btnSendSMS" runat="server" Text="SMS Log Report" CssClass="btn btn-outline-info" OnClick="btnSendSMS_Click" ValidationGroup="Report" />
                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="Report" />
                            </div>
                        </div>
                    </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSendSMS" />
        </Triggers>
    </asp:UpdatePanel>


</asp:Content>
