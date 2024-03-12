<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="LockFeeCollections.aspx.cs" Inherits="Academic_LockFeeCollections"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">

        <div class="col-md-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">FEE COLLECTION LOCKING</h3>
                    <div class="box-tools pull-right">
                        <span style="color: red;">Note : * marked fields are mandatory</span>
                    </div>
                    <div class="box-tools pull-right">
                    </div>
                </div>
                <form role="form">
                    <div class="box-body">
                        <legend class="legendPay">Date Range</legend>
                        <div class="col-md-3"></div>
                        <div class="form-group col-md-3">
                            <label>
                                <span style="color: red;">*</span> From Date
                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meeFromDate"
                                    ControlToValidate="txtFromDate" IsValidEmpty="False" EmptyValueMessage="From date is required"
                                    InvalidValueMessage="From date is invalid" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                    Display="Dynamic" ValidationGroup="lock" />
                            </label>
                            <div class="input-group">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar"></i>
                                </div>
                                <asp:TextBox ID="txtFromDate" runat="server" TabIndex="1" CssClass="form-control" />
                                <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtFromDate" PopupButtonID="imgCalFromDate" Enabled="true" EnableViewState="true" />
                                <ajaxToolKit:MaskedEditExtender ID="meeFromDate" runat="server" TargetControlID="txtFromDate"
                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                    AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate" />

                            </div>
                        </div>
                        <div class="form-group col-md-3">
                            <label>
                                <span style="color: red;">*</span> To Date

                                <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="meeToDate"
                                    ControlToValidate="txtToDate" IsValidEmpty="False" EmptyValueMessage="To date is required"
                                    InvalidValueMessage="To date is invalid" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                    Display="Dynamic" ValidationGroup="lock" />
                            </label>
                            <div class="input-group">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar"></i>
                                </div>
                                <asp:TextBox ID="txtToDate" runat="server" TabIndex="2" CssClass="form-control" />
                                <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="txtToDate" PopupButtonID="Image1" Enabled="true" EnableViewState="true" />
                                <ajaxToolKit:MaskedEditExtender ID="meeToDate" runat="server" TargetControlID="txtToDate"
                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                    AcceptNegative="Left" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate" />

                            </div>
                        </div>
                        <div class="box-footer col-md-12">
                            <p class="text-center">
                                <asp:Button ID="btnLock" runat="server" Text="Lock" CssClass="btn btn-outline-info" TabIndex="3"
                                    ValidationGroup="lock" OnClick="btnLock_Click" />
                                <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="lock" />
                            </p>
                        </div>
                        <div id="div2" runat="server">
                        </div>
                    </div>
                </form>
            </div>
        </div>
    </div>

    <br />
    <div id="divMsg" runat="server">
    </div>

    <script type="text/javascript">

        function ValidateSubmission(sender, args) {
            try {
                if (confirm("Are you sure you want to lock fee collection records having \nreceipt date between entered date range (including both dates)?")) {
                    document.getElementById('txtHiddenLogic').value = "something";
                    args.IsValid = true;
                }
            }
            catch (e) {
                alert("Error: " + e.description);
            }
        }
    </script>

</asp:Content>
