<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ReservationConfiguration.aspx.cs" Inherits="ACADEMIC_ReservationConfiguration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div style="z-index: 1; position: absolute; top: 10px; left: 550px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updLists"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:UpdatePanel ID="updLists" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-info">
                            <div class="box-header with-border">
                                <h3 class="box-title">Reservation Configuration</h3>
                                <div class="box-tools pull-right">
                                </div>
                            </div>
                            <br />
                            <div style="color: Red; font-weight: bold; margin-top: -20px; margin-right: -5px;">
                                &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                            </div>

                            <div class="box-body">
                                <h4>Selection Criteria</h4>
                                <hr />
                                <div class="col-md-12">
                                    <div class="row">
                                        <div class="form-group col-md-4">
                                            <label>Degree <span style="color: red;">*</span></label>
                                            <asp:DropDownList ID="ddlDegree" runat="server" AutoPostBack="true" AppendDataBoundItems="true" CssClass="form-control" TabIndex="1" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                                <asp:ListItem>Please Select </asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlDegree" runat="server" ControlToValidate="ddlDegree"
                                                Display="None" ValidationGroup="submit" InitialValue="0"
                                                ErrorMessage="Please Select Degree"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-md-4">
                                            <label>Branch <span style="color: red;">*</span></label>
                                            <asp:DropDownList ID="ddlBranchName" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                                TabIndex="2">
                                                <asp:ListItem Value="0">Please Select </asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvBranchName" runat="server" ControlToValidate="ddlBranchName"
                                                InitialValue="0" Display="None" ValidationGroup="submit" ErrorMessage="Please Select Branch"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-md-4">
                                            <label>Total Intake <span style="color: red;">*</span></label>
                                            <asp:TextBox ID="txtIntake" runat="server" CssClass="form-control" ToolTip="Please Enter Intake Capacity" MaxLength="3" TabIndex="3" />
                                            <asp:RequiredFieldValidator ID="rfvIntake" runat="server" ControlToValidate="txtIntake"
                                                Display="None" ValidationGroup="submit" ErrorMessage="Please Enter Intake Capacity"></asp:RequiredFieldValidator>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="fteIntake" runat="server" FilterType="Custom"
                                                ValidChars="0123456789" TargetControlID="txtIntake">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>

                                    </div>
                                    <div class="row">
                                        <div style="text-align: left; height: 50%; width: 100%; background-color: #d9edf7">
                                            <h3>Reservation Quota </h3>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-md-3">
                                            <label>SC <span style="color: red;">*</span></label>
                                            <asp:TextBox ID="txtSC" runat="server" CssClass="form-control" ToolTip="Please Enter Quota for SC Category" MaxLength="3" TabIndex="4" />
                                            <asp:RequiredFieldValidator ID="rfvSC" runat="server" ControlToValidate="txtSC"
                                                Display="None" ValidationGroup="submit" ErrorMessage="Please Enter Quota for SC Category"></asp:RequiredFieldValidator>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="fteSC" runat="server" FilterType="Custom"
                                                ValidChars="0123456789" TargetControlID="txtSC">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>
                                        <div class="form-group col-md-3">
                                            <label>ST <span style="color: red;">*</span></label>
                                            <asp:TextBox ID="txtST" runat="server" CssClass="form-control" ToolTip="Please Enter Quota for ST Category" MaxLength="3" TabIndex="5" />
                                            <asp:RequiredFieldValidator ID="rfvST" runat="server" ControlToValidate="txtST"
                                                Display="None" ValidationGroup="submit" ErrorMessage="Please Enter Quota for ST Category"></asp:RequiredFieldValidator>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="fteST" runat="server" FilterType="Custom"
                                                ValidChars="0123456789" TargetControlID="txtST">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>
                                        <div class="form-group col-md-3">
                                            <label>General <span style="color: red;">*</span></label>
                                            <asp:TextBox ID="txtGen" runat="server" CssClass="form-control" ToolTip="Please Enter Quota for General Category" MaxLength="3" TabIndex="6" />
                                            <asp:RequiredFieldValidator ID="rfvGeneral" runat="server" ControlToValidate="txtGen"
                                                Display="None" ValidationGroup="submit" ErrorMessage="Please Enter Quota for General Category"></asp:RequiredFieldValidator>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="fteGeneral" runat="server" FilterType="Custom"
                                                ValidChars="0123456789" TargetControlID="txtGen">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>
                                        <div class="form-group col-md-3">
                                            <label>OBC <span style="color: red;">*</span></label>
                                            <asp:TextBox ID="txtOBC" runat="server" CssClass="form-control" ToolTip="Please Enter Quota for OBC Category" MaxLength="3" TabIndex="6" />
                                            <asp:RequiredFieldValidator ID="rfvOBC" runat="server" ControlToValidate="txtOBC"
                                                Display="None" ValidationGroup="submit" ErrorMessage="Please Enter Quota for OBC Category"></asp:RequiredFieldValidator>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="fteOBC" runat="server" FilterType="Custom"
                                                ValidChars="0123456789" TargetControlID="txtOBC">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="box-footer">
                                <p class="text-center">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" TabIndex="10" ValidationGroup="submit" CssClass="btn btn-success" OnClick="btnSubmit_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="11" CssClass="btn btn-outline-danger" OnClick="btnCancel_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                        ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />
                                </p>
                                <div class="col-md-12">
                                    <asp:Panel ID="pnlconfiguration" runat="server" ScrollBars="Auto">
                                        <asp:ListView ID="lvConfiguration" runat="server">
                                            <LayoutTemplate>

                                                <div id="demo-grid">
                                                    <h4 style="text-shadow: 2px 2px 3px #0b93f8;">Reservation Configuration List</h4>
                                                    <div>
                                                        <table class="table table-hover table-bordered table-responsive">
                                                            <thead>
                                                                <tr class="bg-light-blue">
                                                                    <th>Edit
                                                                    </th>
                                                                    <th>Degree
                                                                    </th>
                                                                    <th>Branch
                                                                    </th>
                                                                    <th>Intake
                                                                    </th>
                                                                    <th>SC Quota
                                                                    </th>
                                                                    <th>ST Quota
                                                                    </th>
                                                                    <th>General Quota
                                                                    </th>
                                                                    <th>OBC Quota
                                                                    </th>
                                                                </tr>

                                                            </thead>
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr class="item" onmouseout="this.style.backgroundColor='#FFFFFF'" onmouseover="this.style.backgroundColor='#FFFFAA'">
                                                    <td style="width: 5%">
                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/IMAGES/edit1.gif" CommandArgument='<%# Eval("CNFNO") %>'
                                                            AlternateText="Edit Record" OnClick="btnEdit_Click"
                                                            ToolTip='<%# Eval("CNFNO") %>' />
                                                    </td>
                                                    <td>
                                                        <%# Eval("DEGREE") %>   
                                                    </td>
                                                    <td>
                                                        <%# Eval("BRANCH") %>   
                                                    </td>
                                                    <td>
                                                        <%# Eval("INTAKE") %>   
                                                    </td>
                                                    <td>
                                                        <%# Eval("SC") %>   
                                                    </td>
                                                    <td>
                                                        <%# Eval("ST") %>   
                                                    </td>
                                                    <td>
                                                        <%# Eval("GEN") %>   
                                                    </td>
                                                    <td>
                                                        <%# Eval("OBC") %>   
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
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />

            <asp:PostBackTrigger ControlID="btnCancel" />
        </Triggers>


    </asp:UpdatePanel>

</asp:Content>

