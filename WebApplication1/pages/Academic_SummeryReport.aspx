<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteMasterPage.master" CodeFile="Academic_SummeryReport.aspx.cs"
    Inherits="ACADEMIC_Academic_SummeryReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">STUDENT STRENGTH SUMMARY REPORT</h3>
                </div>
                <div style="color: Red; font-weight: bold">
                    &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="box-body">
                            <div class="form-group col-md-3"></div>
                            <div class="form-group col-md-3">
                                <label><span style="color: red;">*</span> Session</label>
                                <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" Font-Bold="true"
                                    CssClass="form-control">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvddlSession" runat="server" ControlToValidate="ddlSession"
                                    Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="submit"
                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-md-3">
                                <label><span style="color: red;">*</span> Summary By</label>
                                <asp:CheckBoxList ID="chkReportType" runat="server" CellPadding="1" CellSpacing="8" RepeatColumns="2" AppendDataBoundItems="true" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="0">DEGREE</asp:ListItem>
                                    <asp:ListItem Value="1">BATCH</asp:ListItem>
                                    <asp:ListItem Value="2">BRANCH</asp:ListItem>
                                    <asp:ListItem Value="3">CATEGORY</asp:ListItem>
                                </asp:CheckBoxList>
                            </div>
                            <div class="form-group col-md-3"></div>
                        </div>
                        <div class="box-footer">
                            <p class="text-center">
                                <asp:Button ID="btnView" runat="server" Text="View"
                                    ValidationGroup="submit" CssClass="btn btn-success" OnClick="btnView_Click" />
                                <asp:Button ID="btnShowReports" runat="server" Text="Export In Excel" Visible="false"
                                    ValidationGroup="submit" TabIndex="9" CssClass="btn btn-outline-info" OnClick="btnShowReports_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                    TabIndex="10" CssClass="btn btn-outline-danger" OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="valSummery" DisplayMode="List" runat="server" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="submit" />
                            </p>
                            <div class="col-md-12" id="dvGrid" runat="server" visible="false">
                                <div style="height: 400px; overflow: auto;">
                                    <asp:GridView ID="lstDetails" runat="server" CellPadding="6" EmptyDataText="No Records Found" CssClass="Freezing" EnableModelValidation="True" ForeColor="#333333" GridLines="Horizontal" Height="105px">
                                        <AlternatingRowStyle BackColor="White" />
                                        <EditRowStyle BackColor="#2461BF" />
                                        <FooterStyle BackColor="#1b7a87" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#1B7A87" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#EFF3FB" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnView" />
                        <asp:PostBackTrigger ControlID="btnShowReports" />
                    </Triggers>
                </asp:UpdatePanel>
                <div id="divMsg" runat="server">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
