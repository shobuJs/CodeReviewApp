<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="LateFeeReport.aspx.cs" Inherits="ACADEMIC_LateFeeReport" Title=" " %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="pnlFeeTable"
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

    <asp:UpdatePanel ID="pnlFeeTable" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">SELECTED FEES REPORT</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">

                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Selection Criteria</h5>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>College/School Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSchClg" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                            TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="ddlSchClg_SelectedIndexChanged" />
                                        <asp:RequiredFieldValidator ID="rfvSchClg" runat="server" ControlToValidate="ddlSchClg"
                                            Display="None" ErrorMessage="Please select College/School Name" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="show" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Session</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged"
                                            TabIndex="2">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="teacherreport"></asp:RequiredFieldValidator>
                                    </div>
                           
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                            AutoPostBack="True" TabIndex="3"
                                            OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="teacherreport"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvDegree2" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="report"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Branch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" TabIndex="4" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Semester/Year</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" runat="server" CssClass="form-control" AppendDataBoundItems="true" TabIndex="5">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Receipt type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlRecType" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                            ValidationGroup="Show" CssClass="form-control" TabIndex="6"
                                            OnSelectedIndexChanged="ddlRecType_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvReceiptType" runat="server" ControlToValidate="ddlRecType"
                                            Display="None" ErrorMessage="Please Select Receipt Type" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="teacherreport"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Fees Head</label>
                                        </div>
                                        <asp:DropDownList ID="ddlFeesHead" runat="server" AppendDataBoundItems="True" AutoPostBack="True" TabIndex="7"
                                            ValidationGroup="Show" CssClass="form-control">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvFeesHead" runat="server" ControlToValidate="ddlFeesHead"
                                            Display="None" ErrorMessage="Please Select Fees Head" InitialValue="0" SetFocusOnError="true"
                                            ValidationGroup="teacherreport"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>From Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon" id="imgCalFromDate">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtFromDate" runat="server" TabIndex="8" CssClass="form-control" />
                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtFromDate" PopupButtonID="imgCalFromDate" Enabled="true" EnableViewState="true">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="meeFromDate" runat="server" TargetControlID="txtFromDate"
                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                AcceptNegative="Left" ErrorTooltipEnabled="true" />

                                            <asp:RequiredFieldValidator ID="valFromDate" runat="server" ControlToValidate="txtFromDate"
                                                Display="None" ErrorMessage="Please Select From Date" SetFocusOnError="true"
                                                ValidationGroup="teacherreport" />
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>To Date</label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon" id="imgCalToDate">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtToDate" runat="server" TabIndex="9" CssClass="form-control" />
                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtToDate" PopupButtonID="imgCalToDate" Enabled="true" EnableViewState="true">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="meeToDate" runat="server" TargetControlID="txtToDate"
                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                AcceptNegative="Left" ErrorTooltipEnabled="true" />

                                            <asp:RequiredFieldValidator ID="valToDate" runat="server" ControlToValidate="txtToDate"
                                                Display="None" ErrorMessage="Please Select To Date" SetFocusOnError="true"
                                                ValidationGroup="teacherreport" />
                                        </div>
                                    </div>

                                </div>
                            </div>

                            <div id="div2" runat="server">
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnReport" runat="server" Text="Report" CssClass="btn btn-outline-primary"
                                    ValidationGroup="teacherreport" OnClick="btnReport_Click" />

                                <asp:Button ID="btnExcelSheet" runat="server" Text="Excel Sheet" CssClass="btn btn-outline-primary"
                                    ValidationGroup="teacherreport" OnClick="btnExcelSheet_Click" />

                                <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                    Width="70px" OnClick="btnCancel_Click" CssClass="btn btn-outline-danger" />

                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="report" />
                                
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="teacherreport" />
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReport" />
            <asp:PostBackTrigger ControlID="btnExcelSheet" />
            <asp:PostBackTrigger ControlID="btnCancel" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
