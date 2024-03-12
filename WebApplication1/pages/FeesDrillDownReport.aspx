<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FeesDrillDownReport.aspx.cs" Inherits="ACADEMIC_FeesDrillDownReport" MasterPageFile="~/SiteMasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div style="z-index: 1; position: absolute; top: 100px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updTeacher" DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:UpdatePanel ID="updTeacher" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">ADMISSION DRILL DOWN REPORT</h3>
                            <div class="pull-right">
                                <label>Note :</label><b><span style="color: #FF0000"> * Marked Fields are Mandatory!</span></b>
                            </div>
                        </div>

                        <div class="box-body">

                            <div class="form-group col-md-3">
                                <label><span style="color: red;">*</span> Session </label>
                                <asp:DropDownList ID="ddlSession" runat="server" CssClass="form-control" AppendDataBoundItems="true" TabIndex="1" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession" Display="None" ErrorMessage="Please Select Session"
                                    InitialValue="0" SetFocusOnError="True" ValidationGroup="teacherreport">
                                </asp:RequiredFieldValidator>
                            </div>                                                 

                            <div class="form-group col-md-3">
                                <label><span style="color: red;">*</span> Admission Batch</label>
                                <asp:DropDownList ID="ddlAdmBatch" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="True" TabIndex="2" 
                                    OnSelectedIndexChanged="ddlAdmBatch_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvAdmBatch" runat="server" ControlToValidate="ddlAdmBatch" Display="None" ErrorMessage="Please Select Admission Batch"
                                    InitialValue="0" SetFocusOnError="True" ValidationGroup="teacherreport">
                                </asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group col-md-3">
                                <label> Degree Type</label>
                                <asp:DropDownList ID="ddlDegree" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" TabIndex="3" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>                            
                                </asp:DropDownList>                       
                            </div>

                            <div class="form-group col-md-3">
                                <label><span style="color: red;">*</span> Receipt Type</label>
                                <asp:DropDownList ID="ddlRecpType" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="True" TabIndex="4" 
                                    OnSelectedIndexChanged="ddlRecpType_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvRecp" runat="server" ControlToValidate="ddlRecpType" Display="None" ErrorMessage="Please Select Receipt Type"
                                    InitialValue="0" SetFocusOnError="True" ValidationGroup="teacherreport">
                                </asp:RequiredFieldValidator>
                            </div>

                        </div>

                        <div class="box-footer">
                            <p class="text-center">
                                <asp:Button ID="btnShow" runat="server" Text="Show" ValidationGroup="teacherreport" CssClass="btn btn-outline-info" OnClick="btnShow_Click" TabIndex="4" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-outline-danger" OnClick="btnCancel_Click" TabIndex="5" />
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="report" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="teacherreport" />
                            </p>

                            <div class="col-md-12" id="divlv" runat="server" visible="false">
                                <asp:ListView ID="lvFeeDetails" runat="server" ShowFooter="true" AutoGenerateColumns="false">
                                    <LayoutTemplate>
                                        <div>
                                            <h3>
                                                <label class="label label-default">
                                                    Student Details
                                                </label>
                                            </h3>
                                            <table class="table table-bordered table-hover table-fixed" id="tblCanGradeCard" runat="server">
                                                <thead>
                                                    <tr class="bg-light-blue">
                                                        <th style="text-align: center;">Sr. No.</th>
                                                        <th style="text-align: center;">Branch</th>
                                                        <th style="text-align: center">Applied Students</th>
                                                        <th style="text-align: center">Joined Students</th>
                                                        <th style="text-align: center">Student Paid Fees</th>
                                                        <th style="text-align: center">Student Not Paid Fees</th>
                                                        <th style="text-align: center;">Students Left</th>
                                                        <th style="text-align: center;">Fees Paid Students Left</th>
                                                        <th style="text-align: center;">Present Student Strength</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </div>
                                    </LayoutTemplate>

                                    <EmptyDataTemplate>
                                    </EmptyDataTemplate>

                                    <ItemTemplate>
                                        <tr>
                                            <td class="text-center">
                                                <%# Container.DataItemIndex+1 %>
                                            </td>
                                            <td class="text-center">
                                                <asp:Label ID="lblBranch" runat="server" Text='<%# Eval("BRANCH")%>' ToolTip='<%# Eval("DEGREE")%>' CommandArgument='<%# Eval("BRANCHNO")%>'></asp:Label>
                                            </td>
                                            <td class="text-center">
                                                <asp:LinkButton ID="btnAppStudents" runat="server" Font-Underline="false" Text='<%# Eval("APP_STUD")%>' ToolTip='<%# Eval("DEGREENO")%>' CommandName="1" OnClick="btnAppStudents_Click" TabIndex="6" CommandArgument='<%# Eval("BRANCHNO")%>'></asp:LinkButton>
                                            </td>
                                            <td class="text-center">
                                                <asp:LinkButton ID="btnJoinedStud" runat="server" Font-Underline="false" Text='<%# Eval("JOIN_STUD")%>' ToolTip='<%# Eval("DEGREENO")%>' CommandName="2" OnClick="lblJoinedStud_Click" TabIndex="7" CommandArgument='<%# Eval("BRANCHNO")%>'></asp:LinkButton>
                                            </td>
                                            <td class="text-center">
                                                <asp:LinkButton ID="btnStudPaidFees" runat="server" Font-Underline="false" Text='<%# Eval("STUD_PAID_FEES")%>' ToolTip='<%# Eval("DEGREENO")%>' CommandName="3" OnClick="lblStudPaidFees_Click" TabIndex="8" CommandArgument='<%# Eval("BRANCHNO")%>'></asp:LinkButton>
                                            </td>
                                            <td class="text-center">
                                                <asp:LinkButton ID="btnStudNotPaidFees" runat="server" Font-Underline="false" Text='<%# Eval("STUD_NOTPAID_FEES")%>' ToolTip='<%# Eval("DEGREENO")%>' CommandName="4" OnClick="btnStudNotPaidFees_Click" TabIndex="9" CommandArgument='<%# Eval("BRANCHNO")%>'></asp:LinkButton>
                                            </td>
                                            <td class="text-center">
                                                <asp:LinkButton ID="btnStudLeft" runat="server" Font-Underline="false" Text='<%# Eval("STUD_LEFT")%>' CommandName="5" ToolTip='<%# Eval("DEGREENO")%>' OnClick="btnStudLeft_Click" TabIndex="10" CommandArgument='<%# Eval("BRANCHNO")%>'></asp:LinkButton>
                                            </td>
                                            <td class="text-center">
                                                <asp:LinkButton ID="btnFeePaidStudLeft" runat="server" Font-Underline="false" Text='<%# Eval("STUD_PAID_LEFT")%>' ToolTip='<%# Eval("DEGREENO")%>' CommandName="6" OnClick="btnFeePaidStudLeft_Click" TabIndex="11" CommandArgument='<%# Eval("BRANCHNO")%>'></asp:LinkButton>
                                            </td>
                                            <td class="text-center">
                                                <asp:LinkButton ID="btnCurStudStrength" runat="server" Font-Underline="false" Text='<%# Eval("CUR_STUD_STRENGTH")%>' ToolTip='<%# Eval("DEGREENO")%>' CommandName="7" OnClick="btnCurStudStrength_Click" TabIndex="12" CommandArgument='<%# Eval("BRANCHNO")%>'></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
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
