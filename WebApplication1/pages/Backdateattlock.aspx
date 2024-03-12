<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master"
    AutoEventWireup="true" CodeFile="Backdateattlock.aspx.cs" Inherits="ACADEMIC_Backdateattlock" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">

        function showConfirm() {
            var ret = confirm('Do you Really want to Generate Univ. Reg. No.?');
            if (ret == true) {
                FreezeScreen('Please Wait, Your Data is Being Processed...');
                validate = true;
            }
            else
                validate = false;
            return validate;
        }
    </script>
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">LOCK ATTENDANCE DAYS</h3>
                            <div class="box-tools pull-right">
                                <span style="color: red;">Note : * marked fields are mandatory</span>
                            </div>
                            <div class="box-tools pull-right">
                            </div>
                        </div>
                        <div class="box-body">

                            <div class="form-group col-md-4">
                                <label><span style="color: red;">*</span> Degree </label>
                                <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control"
                                    ValidationGroup="SeatAllot">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                    Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="RegNoAllot">
                                </asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-md-4">
                                <label><span style="color: red;">*</span> No. Of Back Days </label>
                                <asp:TextBox ID="txtdays" runat="server" CssClass="form-control" MaxLength="3"></asp:TextBox>
                                <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtTutMarks" runat="server" FilterType="Custom"
                                    ValidChars="0123456789." TargetControlID="txtdays" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtdays"
                                    Display="None" ErrorMessage="Please Enter No. Of Back Days" ValidationGroup="RegNoAllot">
                                </asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group ">
                                <label>Alert Email </label>
                                <br />
                                <asp:CheckBox ID="chkAlertEmail" runat="server" Style="margin-top: 25px; margin-left: 15px" />
                            </div>

                        </div>

                        <div class="box-footer">
                            <p class="text-center">
                                <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" ValidationGroup="RegNoAllot" Text="Submit"
                                    CssClass="btn btn-outline-info" />

                                <asp:Button ID="btnClear" runat="server" Text="Cancel" OnClick="btnClear_Click"
                                    CssClass="btn btn-outline-danger" />

                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                    ShowMessageBox="true" ShowSummary="false" ValidationGroup="RegNoAllot" />
                                <div class="col-md-12 table table-responsive">
                                    <asp:ListView ID="lvlockatt" runat="server">
                                        <LayoutTemplate>
                                            <h4>No of Back Days Details </h4>
                                            <asp:Panel ID="pnlStudent" runat="server" Height="350px" ScrollBars="Auto">
                                                <table class="table table-hover table-bordered table-fixed table-stripped">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th>Sr No. </th>
                                                            <th>Edit
                                                            </th>
                                                            <th>Degree </th>
                                                            <th>No. Of Back Days </th>
                                                            <th>Alert Email </th>

                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </asp:Panel>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td><%# Container.DataItemIndex + 1 %></td>
                                                <td>
                                                    <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" CausesValidation="false" CommandName='<%# Eval("NO_BACK_DAYS") %>' CommandArgument='<%# Eval("degreeno") %>' ImageUrl="~/images/edit1.gif" OnClick="btnEdit_Click" ToolTip="Edit Record" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbldegree" runat="server" ToolTip='<%# Eval("degreeno") %>' Text='<%# Eval("DEGREENAME") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblnoday" runat="server" Text='<%# Eval("NO_BACK_DAYS") %>'></asp:Label>
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:CheckBox ID="chklvAlertEmail" runat="server" Enabled="false" Checked='<%# (Eval("ALERT_EMAIL").ToString() == "1"? true : false) %>' />
                                                </td>

                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                                <p>
                                </p>
                            </p>
                        </div>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>
    <script>
        $(document).ready(function () {
            $('#pnlStudent').DataTable({
                "scrollY": 200,
                "scrollX": true,
                "paging": false,
                "lengthChange": false,
                "searching": false,
                "ordering": false,
                "info": false,
                "autoWidth": false
            });
        });
    </script>
</asp:Content>
