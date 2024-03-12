<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AdmissionConfirmationReport.aspx.cs" Inherits="ACADEMIC_AdmissionConfirmationReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="z-index: 1; position: absolute; top: 10px; left: 550px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updSelection"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>


    <asp:UpdatePanel ID="updSelection" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-info">
                            <div class="box-header with-border">
                                <h3 class="box-title"><b>Admission Confirmation Report</b></h3>
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
                                            <asp:DropDownList ID="ddlAdmBatch" runat="server" AppendDataBoundItems="true" CssClass="form-control" TabIndex="1" Style="margin-left: 7px">
                                                <asp:ListItem Value="0">Please Select </asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlAdmBatch" runat="server" ControlToValidate="ddlAdmBatch"
                                                Display="None" ValidationGroup="Show" InitialValue="0"
                                                ErrorMessage="Please Select Admission Batch"></asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="rfvddlAdmBatch1" runat="server" ControlToValidate="ddlAdmBatch"
                                                Display="None" ValidationGroup="Report" InitialValue="0"
                                                ErrorMessage="Please Select Admission Batch"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-md-4">
                                            <label>Degree <span style="color: red;">*</span></label>
                                            <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                                TabIndex="2" Style="margin-left: 7px">
                                                <asp:ListItem Value="0">Please Select </asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlDegree" runat="server" ControlToValidate="ddlDegree"
                                                Display="None" ValidationGroup="Show" InitialValue="0"
                                                ErrorMessage="Please Select Degree"></asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="rfvddlDegree1" runat="server" ControlToValidate="ddlDegree"
                                                Display="None" ValidationGroup="Report" InitialValue="0"
                                                ErrorMessage="Please Select Degree"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-md-4">
                                            <label>Semester <span style="color: red;">*</span></label>
                                            <asp:DropDownList ID="ddlSem" runat="server" Width="300px" AppendDataBoundItems="true"
                                                TabIndex="3" Style="margin-left: 6px">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlSem" runat="server" ControlToValidate="ddlSem"
                                                Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="rfvddlSem1" runat="server" ControlToValidate="ddlSem"
                                                Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="Report"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="box box-footer">
                                <p class="text-center">
                                    <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-outline-info" TabIndex="4" ValidationGroup="Show" OnClick="btnShow_Click" />&nbsp;&nbsp;
                                 
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-outline-danger" OnClick="btnCancel_Click" TabIndex="5" />

                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Show"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Report"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </p>
                                <div class="col-md-12">
                                    <asp:Panel runat="server" ID="pnllist" ScrollBars="Auto" Visible="false">
                                        <asp:ListView ID="lvStudent" runat="server" OnItemCommand="lvStudent_ItemCommand">
                                            <EmptyDataTemplate>
                                                <br />
                                                <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="Student Not found" />
                                            </EmptyDataTemplate>
                                            <LayoutTemplate>
                                                <div id="demp_grid" class="vista-grid">
                                                    <h3 style="text-shadow: 2px 2px 3px #0b93f8;">Student List </h3>
                                                    <table class="table table-hover table-bordered table-responsive">
                                                        <thead>
                                                            <tr class="bg-light-blue">
                                                                <th>SrNo
                                                                </th>
                                                                <th>Name
                                                                </th>
                                                                <th>Degree
                                                                </th>
                                                                <th>Semester
                                                                </th>
                                                                <th>Admission Date
                                                                </th>

                                                                <th></th>
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
                                                        <%# Container.DataItemIndex + 1%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("NAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("DEGREENAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("SEMESTERNAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("admdate")%>
                                                    </td>
                                                    <td style="text-align: center">
                                                        <asp:Button ID="btnConfirm" runat="server" CssClass="btn btn-outline-primary" CommandName="Report" CommandArgument='<%# Eval("IDNO")%>' Text='<%# (Convert.ToInt32(Eval("admcan"))==1 ?  "Confirm" : "Print") %>' ToolTip='<%# (Convert.ToInt32(Eval("admcan"))==1 ?  "1" : "0") %>' />

                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <AlternatingItemTemplate>
                                                <tr class="altitem" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFD2'">
                                                    <td>
                                                        <%# Container.DataItemIndex + 1%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("NAME")%> 
                                                    </td>
                                                    <td>
                                                        <%# Eval("DEGREENAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("SEMESTERNAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("admdate")%>
                                                    </td>
                                                    <td style="text-align: center">
                                                        <asp:Button ID="btnConfirm" runat="server" CssClass="btn btn-outline-primary" CommandName="Report" CommandArgument='<%# Eval("IDNO")%>' Text='<%# (Convert.ToInt32(Eval("admcan"))==1 ?  "Confirm" : "Print") %>' ToolTip='<%# (Convert.ToInt32(Eval("admcan"))==1 ?  "1" : "0") %>' />

                                                    </td>
                                                </tr>
                                            </AlternatingItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>

