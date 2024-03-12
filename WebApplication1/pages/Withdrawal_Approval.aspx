<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Withdrawal_Approval.aspx.cs" Inherits="EXAMINATION_Projects_Withdrawal_Approval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        #ctl00_ContentPlaceHolder1_Panel3 .dataTables_scrollHeadInner, #ctl00_ContentPlaceHolder1_Panel1 .dataTables_scrollHeadInner, #ctl00_ContentPlaceHolder1_Panel2 .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <style>
        input[type=checkbox], input[type=radio] {
            margin: 2px 3px 0;
        }

        .badge {
            display: inline-block;
            padding: 5px 10px 7px;
            border-radius: 15px;
            font-size: 100%;
            width: 80px;
        }

        .badge-warning {
            color: #fff !important;
        }

        td .fa-eye {
            font-size: 18px;
            color: #0d70fd;
        }
    </style>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label>
                    </h3>
                </div>
                <div id="divoperator" runat="server" visible="false">
                    <div class="box-body">
                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <asp:Label ID="Label2" runat="server" Font-Bold="true" Text="Status"></asp:Label>
                                    </div>
                                    <asp:RadioButtonList ID="rdooperator" runat="server" RepeatColumns="8" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="1"><span style="padding-left:5px">Pending</span></asp:ListItem>
                                        <asp:ListItem Value="2"><span style="padding-left:5px">Completed</span></asp:ListItem>
                                        <asp:ListItem Value="3"><span style="padding-left:5px">All</span></asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="rdooperator"
                                        Display="None" ErrorMessage="Please select Status" ValidationGroup="showop" InitialValue="0"></asp:RequiredFieldValidator>
                                    <asp:HiddenField ID="dateoprator" runat="server" />
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Request Type</label>
                                    </div>
                                    <asp:DropDownList ID="ddltypeop" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        <%-- <asp:ListItem Value="1">Admission Withdrawal</asp:ListItem>
                                        <asp:ListItem Value="2">Semester Registration</asp:ListItem>
                                        <asp:ListItem Value="3">Postponement</asp:ListItem>
                                        <asp:ListItem Value="4">Pro-Rata</asp:ListItem>
                                        <asp:ListItem Value="10">Excess Withdrawal</asp:ListItem>--%>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Date (From-To)</label>
                                    </div>
                                    <div id="pickerop" class="form-control">
                                        <i class="fa fa-calendar"></i>&nbsp;
                                    <span id="dateop"></span>
                                        <i class="fa fa-angle-down" aria-hidden="true" style="float: right; padding-top: 4px; font-weight: bold;"></i>
                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Faculty School Name</label>
                                    </div>
                                    <asp:DropDownList ID="ddlfaculty" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-12 btn-footer">
                            <asp:Button ID="btnshowope" runat="server" ValidationGroup="showop" Text="show" ToolTip="Show"
                                TabIndex="8" CssClass="btn btn-outline-info" OnClick="btnshowope_Click" />
                            <asp:Button ID="btncancelop" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                TabIndex="8" CssClass="btn btn-outline-danger" OnClick="btncancelop_Click" />
                            <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="showop"
                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            <asp:HiddenField ID="HiddenField4" runat="server" />
                        </div>

                        <div class="col-md-12" id="divop" runat="server" visible="false">
                            <asp:Panel ID="Panel3" runat="server">
                                <asp:ListView ID="lvop" runat="server">
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                            <h5>Refund Approval List</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="myTable">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th style="text-align: center">
                                                        <%--<asp:CheckBox ID="cbHead" runat="server" OnClick="checkAllCheckbox(this)" />--%>
                                                    Sr.No.
                                                    </th>
                                                    <th>Request ID</th>
                                                    <th>Request Type</th>
                                                    <th>Student ID</th>
                                                    <th>Registration No.</th>
                                                    <th>Student Name</th>
                                                    <th>Submission Date</th>
                                                    <th>Details</th>
                                                    <th>Status</th>
                                                    <th>Uploded Document</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>

                                            <td style="text-align: center">
                                                <%--<asp:CheckBox ID="chktransfer" runat="server" ToolTip='<%# Eval("IDNO")%>'  /></td>--%>
                                                <%# Container.DataItemIndex + 1 %>

                                            </td>
                                            <td>
                                                <%# Eval("SRNO")%> 
                                            </td>
                                            <td>
                                                <%# Eval("REQUEST_TYPE")%> 
                                            </td>
                                            <td>
                                                <%# Eval("REGNO")%> 
                                            </td>
                                            <td>
                                                <%# Eval("ENROLLNO")%> 
                                            </td>
                                            <td>
                                                <%# Eval("STUDFIRSTNAME")%> 
                                            </td>
                                            <td>

                                                <%# Eval("APPLIED_DATE")%> 
                                            </td>
                                            <td style="text-align: center">
                                                <%--<asp:LinkButton ID="lnkViewDoc" runat="server" CssClass="btn btn-outline-info" CommandArgument='<%#Eval("IDNO") %>' CommandName='<%#Eval("DOCUMENT") %>' OnClick="lnkViewDoc_Click"><i class="fa fa-eye"></i> View</asp:LinkButton>--%>
                                                <asp:LinkButton ID="lnkViewDocOp" runat="server" CommandArgument='<%#Eval("IDNO") %>' CommandName='<%# Eval("SRNO") %>' ToolTip='<%# Eval("REGNO")%>' OnClick="lnkViewDocOp_Click"><i class="fa fa-eye" aria-hidden="true" style="color: #0d70fd; font-size: 24px;"></i></asp:LinkButton>

                                            </td>
                                            <td class="text-center"><span class="badge badge-success"><%#Eval("OPERATOR_APPROVAL") %></span></td>
                                            <td>
                                                <asp:LinkButton ID="lnkstuddoc" runat="server" CssClass="btn btn-outline-info" CommandArgument='<%#Eval("SRNO") %>' CommandName='<%#Eval("DOCUMENT") %>' ToolTip='<%#Eval("DOCUMENT") %>' OnClick="lnkstuddoc_Click"><i class="fa fa-eye"></i> View</asp:LinkButton>

                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </asp:Panel>
                        </div>
                    </div>
                    <asp:UpdatePanel ID="updModelPopup" runat="server">
                        <ContentTemplate>
                            <div id="myModal22" class="modal fade" role="dialog" data-backdrop="static">
                                <div class="modal-dialog modal-lg">
                                    <div class="modal-content" style="margin-top: -25px">
                                        <div class="modal-body">
                                            <div class="modal-header">
                                                <%--<button type="button" class="close" data-dismiss="modal" style="margin-top: -18px">x</button>--%>
                                                <asp:LinkButton ID="lnkClose" runat="server" CssClass="close" Style="margin-top: -18px" OnClick="lnkClose_Click">x</asp:LinkButton>

                                            </div>

                                            <asp:Image ID="ImageViewer" runat="server" Width="100%" Height="500px" Visible="false" />
                                            <asp:Literal ID="ltEmbed" runat="server" />
                                            <iframe style="width: 100%; height: 500px;" id="irm1" src="~/PopUp.aspx" runat="server"></iframe>
                                            <asp:LinkButton ID="lnkCloseModel" runat="server" CssClass="btn btn-default" Style="margin-top: -10px" OnClick="lnkClose_Click">Close</asp:LinkButton>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="lnkClose" />
                            <asp:PostBackTrigger ControlID="lnkCloseModel" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <!-- View Modal Finance-->
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div class="modal" id="view_operator">
                                <div class="modal-dialog  modal-xl">
                                    <div class="modal-content">

                                        <div class="modal-header">
                                            <h4 class="modal-title">View Details</h4>
                                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                                        </div>

                                        <div class="modal-body pl-0 pr-0">
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="col-lg-6 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Application ID :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblappop" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                </a>
                                                            </li>
                                                            <li class="list-group-item"><b>Student Name :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblappname" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                    <div class="col-lg-6 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Submission Date :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lbldateop" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                                <asp:HiddenField ID="hdfidnoop" runat="server" />
                                                                <asp:HiddenField ID="hdfsrnoop" runat="server" />
                                                            </li>
                                                            <li class="list-group-item"><b>Bank Name :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblbanknameop" runat="server" Font-Bold="true"></asp:Label>
                                                                </a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                    <div class="col-lg-6 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Branch Name :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblbranchop" runat="server" ToolTip="1" Font-Bold="true"></asp:Label></a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                    <div class="col-lg-6 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Account Number :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblaccnuop" runat="server" Font-Bold="true"></asp:Label></a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                    <div class="col-lg-6 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Branch Code :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblcodeop" runat="server" Font-Bold="true"></asp:Label></a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12 mt-3">
                                                <div class="row">
                                                    <div class="col-lg-6 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Refundable Amount :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="refundamount" runat="server" Text="" Font-Bold="true"></asp:Label></a>

                                                            </li>
                                                        </ul>
                                                    </div>
                                                    <div class="col-lg-6 col-md-6 col-12 pl-md-0">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Non-Refundable Amount :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="nonrefundamount" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12 mt-3">
                                                <div class="row">
                                                    <%--  <div class="form-group col-lg-12 col-md-12 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Request Description</label>
                                                    </div>
                                                    <asp:TextBox ID="txtde" runat="server" CssClass="form-control" TextMode="MultiLine" />
                                                </div>--%>
                                                    <div class="form-group col-lg-4 col-md-4 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Fees Paid</label>
                                                        </div>
                                                        <asp:TextBox ID="txtfeesop" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-4 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Fees Balance</label>
                                                        </div>
                                                        <asp:TextBox ID="txtbalanceop" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-4 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Expected Refund</label>
                                                        </div>
                                                        <asp:TextBox ID="txtrefundop" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                    </div>

                                                </div>
                                            </div>
                                            <div class="col-12 mt-3" id="Div4">
                                                <div class="col-md-12" id="DivPayment" runat="server" visible="false">
                                                    <asp:Panel ID="Panel7" runat="server">
                                                        <asp:ListView ID="LvPayment" runat="server">
                                                            <LayoutTemplate>
                                                                <div class="sub-heading">
                                                                    <h5>Refund Details</h5>
                                                                </div>
                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%" id="myTable">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>Sr.No.
                                                                            </th>
                                                                            <th>Receipt No</th>
                                                                            <th>Refund Amount</th>
                                                                            <th>Request type</th>
                                                                            <th>Status</th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                                </table>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <%# Container.DataItemIndex + 1%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("REC_NO")%> 
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("REFUND_AMOUNT")%> 
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("REQUEST_TYPE")%> 
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("FINANCE_APPROVAL")%> 
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                            <div class="col-12 mt-3" id="refunddetails">
                                                <div class="col-md-12" id="refund" runat="server" visible="false">
                                                    <asp:Panel ID="Panel4" runat="server">
                                                        <asp:ListView ID="lvrefunddetails" runat="server">
                                                            <LayoutTemplate>
                                                                <div class="sub-heading">
                                                                    <h5>Fees Details</h5>
                                                                </div>
                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%" id="myTable">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>Select
                                                                                <%--<asp:CheckBox ID="cbHead" runat="server" OnClick="checkAllCheckboxdire(this)" />--%>
                                                                            </th>
                                                                            <th>Amount</th>
                                                                            <th>Receipt No</th>
                                                                            <th>Refund Amount</th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                                </table>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <asp:CheckBox ID="chktransfer" runat="server" ToolTip='<%# Eval("IDNO")%>' Checked='<%#(Eval("REFUND_AMOUNT").ToString())== string.Empty ?  false : true %>' />
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("TOTAL_AMT")%> 
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("REC_NO")%> 
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtrefundamt" onblur="return UpdateTotalAndBalance(this);" onkeyup="return validation_2Absent(this);" runat="server" Text='<%# Eval("REFUND_AMOUNT")%>' Enabled='<%# Eval("FINANCE_APPROVAL").ToString() == "1" ? false : true%>'></asp:TextBox>
                                                                        <asp:HiddenField ID="hdfdcr" runat="server" Value='<%# Eval("DCR_NO")%>' />
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                            <div class="col-12 mt-3" id="Withdrawal_Approval_By_Finance">
                                                <div class="row">
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Status</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlstatusop" runat="server" CssClass="form-control" data-select2-enable="true" Enabled="false" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            <asp:ListItem Value="1">Forward To Manager</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlstatusop"
                                                            Display="None" ErrorMessage="Please select Status" ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Remark</label>
                                                        </div>
                                                        <asp:TextBox ID="txtremarkop" runat="server" CssClass="form-control" TextMode="MultiLine" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtremarkop"
                                                            Display="None" ErrorMessage="Please Enter Remark" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Total Amount to Refund</label>
                                                        </div>
                                                        <asp:TextBox ID="txtAmountRefund" runat="server" CssClass="form-control" />
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                        <div class="label-dynamic">
                                                            <%--<sup>* </sup>--%>
                                                            <label>Policy Name </label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlPolicyName" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlPolicyName"
                                                            Display="None" ErrorMessage="Please Select Policy Name" ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                                    </div>
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <%-- <sup>*</sup>--%>
                                                            <label>Remark For Student</label>
                                                        </div>
                                                        <asp:TextBox ID="TxtreStudent" runat="server" CssClass="form-control" TextMode="MultiLine" />
                                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="TxtreStudent"
                                                            Display="None" ErrorMessage="Please Enter Remark" ValidationGroup="submit"></asp:RequiredFieldValidator>--%>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12 btn-footer">
                                                <asp:LinkButton ID="btnsubop" runat="server" CssClass="btn btn-outline-info" ValidationGroup="submit" OnClick="btnsubop_Click">Submit</asp:LinkButton>
                                                <asp:LinkButton ID="btncloseop" runat="server" CssClass="btn btn-outline-danger">Close</asp:LinkButton>
                                                <asp:ValidationSummary ID="ValidationSummary5" runat="server" ValidationGroup="submit" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnsubop" />
                            <asp:PostBackTrigger ControlID="btncloseop" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div id="manager" runat="server" visible="false">
                    <div class="box-body">
                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <asp:Label ID="Label1" runat="server" Font-Bold="true" Text="Status"></asp:Label>
                                    </div>
                                    <asp:RadioButtonList ID="rdooperatmana" runat="server" RepeatColumns="8" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="1"><span style="padding-left:5px">Pending</span></asp:ListItem>
                                        <asp:ListItem Value="2"><span style="padding-left:5px">Completed</span></asp:ListItem>
                                        <asp:ListItem Value="3"><span style="padding-left:5px">All</span></asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="rdooperatmana"
                                        Display="None" ErrorMessage="Please select Status" ValidationGroup="showop" InitialValue="0"></asp:RequiredFieldValidator>
                                    <asp:HiddenField ID="hdfdatemana" runat="server" />
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Request Type</label>
                                    </div>
                                    <asp:DropDownList ID="ddltypemana" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        <%-- <asp:ListItem Value="1">Admission Withdrawal</asp:ListItem>
                                        <asp:ListItem Value="2">Semester Registration</asp:ListItem>
                                        <asp:ListItem Value="3">Postponement</asp:ListItem>
                                        <asp:ListItem Value="4">Pro-Rata</asp:ListItem>
                                        <asp:ListItem Value="10">Excess Withdrawal</asp:ListItem>--%>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Date (From-To)</label>
                                    </div>
                                    <div id="Div3" class="form-control">
                                        <i class="fa fa-calendar"></i>&nbsp;
                                    <span id="Span1"></span>
                                        <i class="fa fa-angle-down" aria-hidden="true" style="float: right; padding-top: 4px; font-weight: bold;"></i>
                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Faculty School Name</label>
                                    </div>
                                    <asp:DropDownList ID="ddlfacultymana" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-12 btn-footer">
                            <asp:Button ID="btnshowoperatmana" runat="server" ValidationGroup="showop" Text="show" ToolTip="Show"
                                TabIndex="8" CssClass="btn btn-outline-info" OnClick="btnshowoperatmana_Click" />
                            <asp:Button ID="btncancelomana" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                TabIndex="8" CssClass="btn btn-outline-danger" OnClick="btncancelomana_Click" />
                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="showop"
                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            <asp:HiddenField ID="HiddenField3" runat="server" />
                        </div>

                        <div class="col-md-12" id="divmana" runat="server" visible="false">
                            <asp:Panel ID="Panel1" runat="server">
                                <asp:ListView ID="lvmanager" runat="server">
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                            <h5>Refund Approval List</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="myTable">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th style="text-align: center">
                                                        <%--<asp:CheckBox ID="cbHead" runat="server" OnClick="checkAllCheckbox(this)" />--%>
                                                    Sr.No.
                                                    </th>
                                                    <th>Request ID</th>
                                                    <th>Request Type</th>
                                                    <th>Student ID</th>
                                                    <th>Registration No</th>
                                                    <th>Student Name</th>
                                                    <th>Submission Date</th>
                                                    <th>Details</th>
                                                    <th>Remark By Operator</th>
                                                    <th>Status</th>
                                                    <th>Uploaded Document</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>

                                            <td style="text-align: center">
                                                <%--<asp:CheckBox ID="chktransfer" runat="server" ToolTip='<%# Eval("IDNO")%>'  /></td>--%>
                                                <%# Container.DataItemIndex + 1 %>

                                            </td>
                                            <td>
                                                <%# Eval("SRNO")%> 
                                            </td>
                                            <td>
                                                <%# Eval("REQUEST_TYPE")%> 
                                            </td>
                                            <td>
                                                <%# Eval("REGNO")%> 
                                            </td>
                                            <td>
                                                <%# Eval(" ENROLLNO")%> 
                                            </td>
                                            <td>
                                                <%# Eval("STUDFIRSTNAME")%> 
                                            </td>
                                            <td>

                                                <%# Eval("APPLIED_DATE")%> 
                                            </td>

                                            <td style="text-align: center">
                                                <%--<asp:LinkButton ID="lnkViewDoc" runat="server" CssClass="btn btn-outline-info" CommandArgument='<%#Eval("IDNO") %>' CommandName='<%#Eval("DOCUMENT") %>' OnClick="lnkViewDoc_Click"><i class="fa fa-eye"></i> View</asp:LinkButton>--%>
                                                <asp:LinkButton ID="lnkViewDocMana" runat="server" CommandArgument='<%#Eval("IDNO") %>' CommandName='<%# Eval("SRNO") %>' ToolTip='<%# Eval("REGNO")%>' OnClick="lnkViewDocMana_Click"><i class="fa fa-eye" aria-hidden="true" style="color: #0d70fd; font-size: 24px;"></i></asp:LinkButton>

                                            </td>
                                            <td>

                                                <%# Eval("OPERATOR_REMARK")%> 
                                            </td>
                                            <td class="text-center"><span class="badge badge-success"><%#Eval("ADMIN_APPROVAL") %></span></td>
                                            <td>
                                                <asp:LinkButton ID="lnkstuddocmana" runat="server" CssClass="btn btn-outline-info" CommandArgument='<%#Eval("SRNO") %>' CommandName='<%#Eval("DOCUMENT") %>' ToolTip='<%#Eval("DOCUMENT") %>' OnClick="lnkstuddocmana_Click"><i class="fa fa-eye"></i> View</asp:LinkButton>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </asp:Panel>
                        </div>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <div id="manamodal" class="modal fade" role="dialog" data-backdrop="static">
                                <div class="modal-dialog modal-lg">
                                    <div class="modal-content" style="margin-top: -25px">
                                        <div class="modal-body">
                                            <div class="modal-header">
                                                <%--<button type="button" class="close" data-dismiss="modal" style="margin-top: -18px">x</button>--%>
                                                <asp:LinkButton ID="btnClose" runat="server" CssClass="close" Style="margin-top: -18px" OnClick="btnClose_Click">x</asp:LinkButton>
                                            </div>

                                            <asp:Image ID="Image1" runat="server" Width="100%" Height="500px" Visible="false" />
                                            <asp:Literal ID="ltEmbedmana" runat="server" Visible="false" />
                                            <iframe style="width: 100%; height: 500px;" id="Iframe1" src="~/PopUp.aspx" runat="server"></iframe>
                                            <asp:LinkButton ID="btnclose2" runat="server" CssClass="btn btn-default" Style="margin-top: -10px" OnClick="btnClose_Click">Close</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnClose" />
                            <asp:PostBackTrigger ControlID="btnclose2" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <!-- View Modal Finance-->
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="modal" id="divmanager">
                                <div class="modal-dialog modal-xl">
                                    <div class="modal-content">

                                        <div class="modal-header">
                                            <h4 class="modal-title">View Details</h4>
                                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                                        </div>
                                        <div class="modal-body pl-0 pr-0">
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="col-lg-6 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Application ID :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblappmana" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                </a>
                                                            </li>
                                                            <li class="list-group-item"><b>Student Name :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblstumana" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                    <div class="col-lg-6 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Submission Date :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lbldatemana" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                                <asp:HiddenField ID="hdfidnomana" runat="server" />
                                                                <asp:HiddenField ID="hdfsrnomana" runat="server" />
                                                            </li>
                                                            <li class="list-group-item"><b>Bank Name :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblbankmana" runat="server" Font-Bold="true"></asp:Label>
                                                                </a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                    <div class="col-lg-6 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Branch Name :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblbranchmana" runat="server" ToolTip="1" Font-Bold="true"></asp:Label></a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                    <div class="col-lg-6 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Account Number :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblaccopmana" runat="server" Font-Bold="true"></asp:Label></a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                    <div class="col-lg-6 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Branch Code :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblcodemana" runat="server" Font-Bold="true"></asp:Label></a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12 mt-3">
                                                <div class="row">
                                                    <div class="col-lg-6 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Refundable Amount :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblrefundmana" runat="server" Text="" Font-Bold="true"></asp:Label></a>

                                                            </li>
                                                        </ul>
                                                    </div>
                                                    <div class="col-lg-6 col-md-6 col-12 pl-md-0">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Non-Refundable Amount :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblnonrefundmana" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12 mt-3">
                                                <div class="row">
                                                    <div class="form-group col-lg-12 col-md-12 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Request Description</label>
                                                        </div>
                                                        <asp:TextBox ID="txtdesmana" runat="server" CssClass="form-control" TextMode="MultiLine" Enabled="false" />
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-4 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Fees Paid</label>
                                                        </div>
                                                        <asp:TextBox ID="lblfeesmana" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-4 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Fees Balance</label>
                                                        </div>
                                                        <asp:TextBox ID="lblbalancemana" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-4 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Expected Refund</label>
                                                        </div>
                                                        <asp:TextBox ID="lblexpectedrefmana" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Status</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlstatusmana" runat="server" CssClass="form-control" data-select2-enable="true" Enabled="false" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            <asp:ListItem Value="1">Forward To Manager</asp:ListItem>
                                                            <asp:ListItem Value="2">Forward to Director</asp:ListItem>
                                                            <asp:ListItem Value="3">Forward to Finance</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlstatusmana"
                                                            Display="None" ErrorMessage="Please select Status" ValidationGroup="submitmanava" InitialValue="0"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>*</sup>
                                                            <label>Remark</label>
                                                        </div>
                                                        <asp:TextBox ID="remarkmana" runat="server" CssClass="form-control" TextMode="MultiLine" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="remarkmana"
                                                            Display="None" ErrorMessage="Please Enter Remark" ValidationGroup="submitmanava"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12 mt-3" id="Div6">
                                                <div class="col-md-12" id="divpayma" runat="server" visible="false">
                                                    <asp:Panel ID="Panel8" runat="server">
                                                        <asp:ListView ID="Lvpayma" runat="server">
                                                            <LayoutTemplate>
                                                                <div class="sub-heading">
                                                                    <h5>Refund Details</h5>
                                                                </div>
                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%" id="myTable">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>Sr.No.
                                                                            </th>
                                                                            <th>Receipt No</th>
                                                                            <th>Refund Amount</th>
                                                                            <th>Request type</th>
                                                                            <th>Status</th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                                </table>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <%# Container.DataItemIndex + 1%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("REC_NO")%> 
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("REFUND_AMOUNT")%> 
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("REQUEST_TYPE")%> 
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("FINANCE_APPROVAL")%> 
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                            <div class="col-12 mt-3" id="divrefundmanage">
                                                <div class="col-md-12" id="divmanref" runat="server" visible="false">
                                                    <asp:Panel ID="Panel5" runat="server">
                                                        <asp:ListView ID="lvrefundmana" runat="server">
                                                            <LayoutTemplate>
                                                                <div class="sub-heading">
                                                                    <h5>Fees Details</h5>
                                                                </div>
                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%" id="myTable">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>Select
                                                                                <%--<asp:CheckBox ID="cbHead" runat="server" OnClick="checkAllCheckboxdire(this)" />--%>
                                                                            </th>
                                                                            <th>Amount</th>
                                                                            <th>Receipt No</th>
                                                                            <th>Refund Amount</th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                                </table>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <asp:CheckBox ID="chktransfer" runat="server" ToolTip='<%# Eval("IDNO")%>' Checked='<%#(Eval("REFUND_AMOUNT").ToString())== string.Empty ?  false : true %>' Enabled='<%#(Eval("REFUND_AMOUNT").ToString())== string.Empty ?  true  : false %>' />
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("TOTAL_AMT")%> 
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("REC_NO")%> 
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtrefundamt" onblur="return UpdateTotalAndBalancemana(this);" onkeyup="return validation_2Absentmanager(this);" runat="server" Text='<%# Eval("REFUND_AMOUNT")%>' Enabled='<%# Eval("FINANCE_APPROVAL").ToString() == "1" ? false : true%>'></asp:TextBox>
                                                                        <asp:HiddenField ID="hdfdcr" runat="server" Value='<%# Eval("DCR_NO")%>' />
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                            <div class="col-12 mt-3" id="Div5">
                                                <div class="row">
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <%--  <sup>*</sup>--%>
                                                            <label>Remark For Student</label>
                                                        </div>
                                                        <asp:TextBox ID="TxtRemarkstmana" runat="server" CssClass="form-control" TextMode="MultiLine" />
                                                        <%--      <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="TxtRemarkstmana"
                                                            Display="None" ErrorMessage="Please Enter Remark" ValidationGroup="submitmanava"></asp:RequiredFieldValidator>--%>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Total Amount to Refund</label>
                                                        </div>
                                                        <asp:TextBox ID="Txttotalrefundmanage" runat="server" CssClass="form-control" />
                                                    </div>

                                                </div>
                                            </div>
                                            <div class="col-12 btn-footer">
                                                <asp:LinkButton ID="submitmana" runat="server" ValidationGroup="submitmanava" CssClass="btn btn-outline-info" OnClick="submitmana_Click">Submit</asp:LinkButton>
                                                <asp:LinkButton ID="cancelmana" runat="server" CssClass="btn btn-outline-danger">Close</asp:LinkButton>
                                                <asp:ValidationSummary ID="ValidationSummary6" runat="server" ValidationGroup="submitmanava" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="submitmana" />
                            <asp:PostBackTrigger ControlID="cancelmana" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div id="divdirector" runat="server" visible="false">
                    <div class="box-body">
                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <asp:Label ID="lblStatus" runat="server" Font-Bold="true"></asp:Label>
                                    </div>
                                    <asp:RadioButtonList ID="rdbFilter" runat="server" RepeatColumns="8" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="1"><span style="padding-left:5px">Pending</span></asp:ListItem>
                                        <asp:ListItem Value="2"><span style="padding-left:5px">Completed</span></asp:ListItem>
                                        <asp:ListItem Value="3"><span style="padding-left:5px">All</span></asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="rdbFilter"
                                        Display="None" ErrorMessage="Please select Status" ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator>
                                    <asp:HiddenField ID="hdnDate" runat="server" />
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Request Type</label>
                                    </div>
                                    <asp:DropDownList ID="ddltypedire" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Date (From-To)</label>
                                    </div>
                                    <div id="picker" class="form-control">
                                        <i class="fa fa-calendar"></i>&nbsp;
                                    <span id="date"></span>
                                        <i class="fa fa-angle-down" aria-hidden="true" style="float: right; padding-top: 4px; font-weight: bold;"></i>
                                    </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Faculty School Name</label>
                                    </div>
                                    <asp:DropDownList ID="ddlfacultyDire" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-12 btn-footer">
                            <asp:Button ID="btnshow" runat="server" Text="Show" ToolTip="Show"
                                TabIndex="8" CssClass="btn btn-outline-info" OnClick="btnshow_Click" />
                            <asp:LinkButton ID="btnSave" runat="server" ToolTip="Submit"
                                CssClass="btn btn-outline-info" OnClick="btnSave_Click" ValidationGroup="submit" Visible="false">Submit</asp:LinkButton>
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                TabIndex="8" CssClass="btn btn-outline-danger" OnClick="btnCancel_Click" />
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            <asp:HiddenField ID="HiddenField1" runat="server" />
                        </div>

                        <div class="col-md-12" id="divwithapti" runat="server" visible="false">
                            <asp:Panel ID="Panel2" runat="server">
                                <asp:ListView ID="lvwithap" runat="server">
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                            <h5>Refund Approval List</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="myTable">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th style="text-align: center">
                                                        <%--<asp:CheckBox ID="cbHead" runat="server" OnClick="checkAllCheckbox(this)" />--%>
                                                    Sr.No.
                                                    </th>
                                                    <th>Request ID</th>
                                                    <th>Request Type</th>
                                                    <th>Application ID</th>
                                                    <th>Student Name</th>
                                                    <th>Submission Date</th>
                                                    <th>Details</th>
                                                    <th>Status</th>
                                                    <th>Remark By Manager</th>
                                                    <th>Uploaded Document</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>

                                            <td style="text-align: center">
                                                <%--<asp:CheckBox ID="chktransfer" runat="server" ToolTip='<%# Eval("IDNO")%>'  /></td>--%>
                                                <%# Container.DataItemIndex + 1 %>

                                            </td>
                                            <td>
                                                <%# Eval("SRNO")%> 
                                            </td>
                                            <td><%# Eval("REQUEST_TYPE")%> </td>
                                            <td>
                                                <%# Eval("REGNO")%> 
                                            </td>
                                            <td>
                                                <%# Eval("STUDFIRSTNAME")%> 
                                            </td>
                                            <td>

                                                <%# Eval("APPLIED_DATE")%> 
                                            </td>
                                            <td style="text-align: center">
                                                <%--<asp:LinkButton ID="lnkViewDoc" runat="server" CssClass="btn btn-outline-info" CommandArgument='<%#Eval("IDNO") %>' CommandName='<%#Eval("DOCUMENT") %>' OnClick="lnkViewDoc_Click"><i class="fa fa-eye"></i> View</asp:LinkButton>--%>
                                                <asp:LinkButton ID="lnkViewDoc" runat="server" CommandArgument='<%#Eval("IDNO") %>' CommandName='<%# Eval("SRNO") %>' ToolTip='<%# Eval("REGNO")%>' OnClick="lnkViewDoc_Click"><i class="fa fa-eye" aria-hidden="true" style="color: #0d70fd; font-size: 24px;"></i></asp:LinkButton>

                                            </td>
                                            <td class="text-center"><span class="badge badge-success"><%#Eval("DIRECTOR_APPROVAL") %></span></td>
                                            <td><%# Eval("REMARK_BY_ADMIN")%></td>
                                            <td>
                                                <asp:LinkButton ID="lnkstuddocdire" runat="server" CssClass="btn btn-outline-info" CommandArgument='<%#Eval("SRNO") %>' CommandName='<%#Eval("DOCUMENT") %>' ToolTip='<%#Eval("DOCUMENT") %>' OnClick="lnkstuddocdire_Click"><i class="fa fa-eye"></i> View</asp:LinkButton>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </asp:Panel>
                        </div>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <div id="diremodal" class="modal fade" role="dialog" data-backdrop="static">
                                <div class="modal-dialog modal-lg">
                                    <div class="modal-content" style="margin-top: -25px">
                                        <div class="modal-body">
                                            <div class="modal-header">
                                                <asp:LinkButton ID="btnClose11" runat="server" CssClass="close" style="margin-top: -18px" OnClick="btnClose11_Click" >x</asp:LinkButton>
                                            </div>

                                            <asp:Image ID="Image2" runat="server" Width="100%" Height="500px" Visible="false" />
                                            <asp:Literal ID="Literal1" runat="server" Visible="false" />
                                            <iframe style="width: 100%; height: 500px;" id="Iframe2" src="~/PopUp.aspx" runat="server"></iframe>
                                            <asp:LinkButton ID="btnClose22" runat="server" CssClass="btn btn-default" Style="margin-top: -10px" OnClick="btnClose11_Click">Close</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnClose11" />
                            <asp:PostBackTrigger ControlID="btnClose22" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <!-- View Modal Finance-->
                    <asp:UpdatePanel ID="updmodal" runat="server">
                        <ContentTemplate>
                            <div class="modal" id="Veiw_Finance">
                                <div class="modal-dialog modal-xl">
                                    <div class="modal-content">

                                        <div class="modal-header">
                                            <h4 class="modal-title">View Details</h4>
                                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                                        </div>

                                        <div class="modal-body pl-0 pr-0">
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="col-lg-6 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Application ID :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblStdID" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                </a>
                                                            </li>
                                                            <li class="list-group-item"><b>Student Name :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblStdName" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                    <div class="col-lg-6 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Submission Date :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="fbfgb" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                                <asp:HiddenField ID="hdfSrnoWithDrwal" runat="server" />
                                                                <asp:HiddenField ID="hdfIdnoWithDrwal" runat="server" />
                                                            </li>
                                                            <li class="list-group-item"><b>Bank Name :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblBankName" runat="server" Font-Bold="true"></asp:Label>
                                                                </a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                    <div class="col-lg-6 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Branch Name :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblBranchName" runat="server" ToolTip="1" Font-Bold="true"></asp:Label></a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                    <div class="col-lg-6 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Account Number :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblAccountNumber" runat="server" Font-Bold="true"></asp:Label></a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                    <div class="col-lg-6 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Branch Code :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblIFSCCode" runat="server" Font-Bold="true"></asp:Label></a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12 mt-3">
                                                <div class="row">
                                                    <div class="form-group col-lg-12 col-md-12 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Request Description</label>
                                                        </div>
                                                        <asp:TextBox ID="txtdfdef" runat="server" CssClass="form-control" TextMode="MultiLine" Enabled="false" />
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-4 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Fees Paid</label>
                                                        </div>
                                                        <asp:TextBox ID="txtFeesPaid" runat="server" CssClass="form-control" Enabled="false" />
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-4 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Fees Balance</label>
                                                        </div>
                                                        <asp:TextBox ID="txtFeesBalance" runat="server" CssClass="form-control" Enabled="false" />
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-4 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Expected Refund</label>
                                                        </div>
                                                        <asp:TextBox ID="txtExpectedRefund" runat="server" CssClass="form-control" Enabled="false" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12 mt-3">
                                                <div class="row">
                                                    <div class="col-lg-6 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Refundable Amount :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblrefunddire" runat="server" Text="" Font-Bold="true"></asp:Label></a>

                                                            </li>
                                                        </ul>
                                                    </div>
                                                    <div class="col-lg-6 col-md-6 col-12 pl-md-0">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Non-Refundable Amount :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="nonlblrefunddire" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12 mt-3" id="Div7">
                                                <div class="col-md-12" id="divpaydire" runat="server" visible="false">
                                                    <asp:Panel ID="Panel9" runat="server">
                                                        <asp:ListView ID="LvpayDire" runat="server">
                                                            <LayoutTemplate>
                                                                <div class="sub-heading">
                                                                    <h5>Refund Details</h5>
                                                                </div>
                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%" id="myTable">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>Sr.No.
                                                                            </th>
                                                                            <th>Receipt No</th>
                                                                            <th>Refund Amount</th>
                                                                            <th>Request type</th>
                                                                            <th>Status</th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                                </table>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <%# Container.DataItemIndex + 1%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("REC_NO")%> 
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("REFUND_AMOUNT")%> 
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("REQUEST_TYPE")%> 
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("FINANCE_APPROVAL")%> 
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                            <div class="col-12 mt-3" id="Div2">
                                                <div class="col-md-12" id="divrefunddire" runat="server" visible="false">
                                                    <asp:Panel ID="Panel6" runat="server">
                                                        <asp:ListView ID="lvrefunddire" runat="server">
                                                            <LayoutTemplate>
                                                                <div class="sub-heading">
                                                                    <h5>Fees Details</h5>
                                                                </div>
                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%" id="myTable">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>Select
                                                                                <%--<asp:CheckBox ID="cbHead" runat="server" OnClick="checkAllCheckboxdire(this)" />--%>
                                                                            </th>
                                                                            <th>Amount</th>
                                                                            <th>Receipt No</th>
                                                                            <th>Refund Amount</th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                                </table>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td>
                                                                        <asp:CheckBox ID="chktransfer" runat="server" ToolTip='<%# Eval("IDNO")%>' Checked='<%#(Eval("REFUND_AMOUNT").ToString())== string.Empty ?  false : true %>' Enabled='<%#(Eval("REFUND_AMOUNT").ToString())== string.Empty ?  true  : false %>' />
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("TOTAL_AMT")%> 
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("REC_NO")%> 
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtrefundamt" onblur="return UpdateTotalAndBalancedire(this);" onkeyup="return validation_2Absentdire(this);" runat="server" Text='<%# Eval("REFUND_AMOUNT")%>' Enabled='<%# Eval("FINANCE_APPROVAL").ToString() == "1" ? false : true%>'></asp:TextBox>
                                                                        <asp:HiddenField ID="hdfdcr" runat="server" Value='<%# Eval("DCR_NO")%>' />
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                            <div class="col-12 mt-3">
                                                <div class="row">

                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Status</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlstatus" runat="server" CssClass="form-control" data-select2-enable="true" Enabled="false" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            <asp:ListItem Value="1">Forward To Manager</asp:ListItem>
                                                            <asp:ListItem Value="2">Forward to Director</asp:ListItem>
                                                            <asp:ListItem Value="3">Forward to Finance</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Approve/Reject</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlstabydirector" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            <asp:ListItem Value="1">Approved</asp:ListItem>
                                                            <asp:ListItem Value="2">Reject</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlstabydirector"
                                                            Display="None" ErrorMessage="Please select Approved/Reject Status" ValidationGroup="submitdire" InitialValue="0"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Remark</label>
                                                        </div>
                                                        <asp:TextBox ID="txtsasdf" runat="server" CssClass="form-control" TextMode="MultiLine" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtsasdf"
                                                            Display="None" ErrorMessage="Please Enter Remark" ValidationGroup="submitdire"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Total Amount to Refund</label>
                                                        </div>
                                                        <asp:TextBox ID="TxtTotalredire" runat="server" CssClass="form-control" />
                                                    </div>


                                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <%--   <sup>*</sup>--%>
                                                            <label>Remark For Student</label>
                                                        </div>
                                                        <asp:TextBox ID="TxtRemarkstuddire" runat="server" CssClass="form-control" TextMode="MultiLine" />
                                                        <%--     <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="TxtRemarkstuddire"
                                                            Display="None" ErrorMessage="Please Enter Remark" ValidationGroup="submitdire"></asp:RequiredFieldValidator>--%>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-12 btn-footer">
                                                <asp:LinkButton ID="btnSubmit1" runat="server" CssClass="btn btn-outline-info" ValidationGroup="submitdire" OnClick="btnSubmit1_Click">Submit</asp:LinkButton>
                                                <asp:ValidationSummary ID="ValidationSummary4" runat="server" ValidationGroup="submitdire"
                                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                <asp:LinkButton ID="btnCancel1" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancel1_Click">Close</asp:LinkButton>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnSubmit1" />
                            <asp:PostBackTrigger ControlID="btnCancel1" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <!-- Start End Date Script -->
    <%--<script type="text/javascript">
        $(document).ready(function () {
            $('#picker').daterangepicker({
                startDate: moment().subtract(00, 'days'),
                endDate: moment(),
                locale: {
                    format: 'DD MMM, YYYY'
                },
                ranges: {
                },
            },
        function (start, end) {
            $('#date').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'));
        });

            $('#date').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'));
        });
    </script>--%>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#picker').daterangepicker({
                startDate: moment().subtract(00, 'days'),
                endDate: moment(),
                locale: {
                    format: 'DD MMM, YYYY'
                },
                ranges: {
                },
            },
        function (start, end) {
            $('#date').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'));
            document.getElementById('<%=hdnDate.ClientID%>').value = (start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
        });

            //$('#date').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'));
        });
    </script>
    <script>
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(document).ready(function () {
                $('#picker').daterangepicker({
                    startDate: moment().subtract(00, 'days'),
                    endDate: moment(),
                    locale: {
                        format: 'DD MMM, YYYY'
                    },
                    ranges: {
                    },
                },
            function (start, end) {
                $('#date').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'));
                document.getElementById('<%=hdnDate.ClientID%>').value = (start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
            });

                //$('#date').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'));
            });
        });
    </script>
    <script type="text/javascript" language="javascript">

        function checkAllCheckbox(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                var s = e.name.split("ctl00$ContentPlaceHolder1$lvwithap$ctrl");
                var b = 'ctl00$ContentPlaceHolder1$lvwithap$ctrl';
                var g = b + s[1];
                if (e.name == g) {
                    if (headchk.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }

    </script>
    <script type="text/javascript" language="javascript">

        function checkAllCheckboxdire(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                var s = e.name.split("ctl00$ContentPlaceHolder1$lvrefunddire$ctrl");
                var b = 'ctl00$ContentPlaceHolder1$lvrefunddire$ctrl';
                var g = b + s[1];
                if (e.name == g) {
                    if (headchk.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }

    </script>
    <script type="text/javascript" language="javascript">

        function checkAllCheckboxmana(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                var s = e.name.split("ctl00$ContentPlaceHolder1$lvrefundmana$ctrl");
                var b = 'ctl00$ContentPlaceHolder1$lvrefundmana$ctrl';
                var g = b + s[1];
                if (e.name == g) {
                    if (headchk.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }

    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#Div3').daterangepicker({
                startDate: moment().subtract(00, 'days'),
                endDate: moment(),
                locale: {
                    format: 'DD MMM, YYYY'
                },
                ranges: {
                },
            },
        function (start, end) {
            $('#Span1').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'));
            document.getElementById('<%=hdfdatemana.ClientID%>').value = (start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
        });

            //$('#date').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'));
        });
    </script>
    <script>
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(document).ready(function () {
                $('#Div3').daterangepicker({
                    startDate: moment().subtract(00, 'days'),
                    endDate: moment(),
                    locale: {
                        format: 'DD MMM, YYYY'
                    },
                    ranges: {
                    },
                },
            function (start, end) {
                $('#Span1').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'));
                document.getElementById('<%=hdfdatemana.ClientID%>').value = (start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
            });

                //$('#date').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'));
            });
        });
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#pickerop').daterangepicker({
                startDate: moment().subtract(00, 'days'),
                endDate: moment(),
                locale: {
                    format: 'DD MMM, YYYY'
                },
                ranges: {
                },
            },
        function (start, end) {
            $('#dateop').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'));
            document.getElementById('<%=dateoprator.ClientID%>').value = (start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
        });

            //$('#date').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'));
        });
    </script>
    <script>
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(document).ready(function () {
                $('#pickerop').daterangepicker({
                    startDate: moment().subtract(00, 'days'),
                    endDate: moment(),
                    locale: {
                        format: 'DD MMM, YYYY'
                    },
                    ranges: {
                    },
                },
            function (start, end) {
                $('#dateop').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'));
                document.getElementById('<%=dateoprator.ClientID%>').value = (start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
            });

                //$('#date').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'));
            });
        });
    </script>

    <script>
        function UpdateTotalAndBalancedire() {
            try {
                var totalFeeAmt = 0.00;
                var dataRows = null;
                list = 'lvrefunddire';
                var dataRows = document.getElementsByTagName('tr');
                var FinalAmount = 0;
                if (dataRows != null) {
                    for (i = 0; i < dataRows.length - 1; i++) {
                        var Amount = 0;
                        var refundamt = 0;
                        Amount = document.getElementById('ctl00_ContentPlaceHolder1_' + list + '_' + 'ctrl' + i + '_' + 'txtrefundamt').value;
                        refundamt = document.getElementById('ctl00_ContentPlaceHolder1_lblrefunddire').innerHTML;
                        if (Amount == null || Amount == 'NaN' || Amount == "") {
                            //alert(Amount)
                            Amount = 0;
                        }
                        //refundamt = $("#refundamount").text()
                        //alert(refundamt)
                        //if (parseFloat(Amount) > parseFloat(refundamt)) {
                        //    alert("Amount Is not Greater Than Refundable Amount")
                        //    document.getElementById('ctl00_ContentPlaceHolder1_' + list + '_' + 'ctrl' + i + '_' + 'txtrefundamt').value = "";
                        //    document.getElementById('ctl00_ContentPlaceHolder1_txtAmountRefund').value = "";
                        //    return false;
                        //}
                        //else {
                        //alert(Amount)
                        FinalAmount = parseFloat(FinalAmount) + parseFloat(Amount.replace(/,/g, ''));
                        document.getElementById('ctl00_ContentPlaceHolder1_TxtTotalredire').value = FinalAmount;
                        //}
                    }
                }
                //alert("hii") 

            }
            catch (e) {
            }
        }

    </script>
    <script>
        function UpdateTotalAndBalance() {
            try {
                var totalFeeAmt = 0.00;
                var dataRows = null;
                list = 'lvrefunddetails';
                var dataRows = document.getElementsByTagName('tr');
                var FinalAmount = 0;
                if (dataRows != null) {
                    for (i = 0; i < dataRows.length - 1; i++) {
                        var Amount = 0;
                        var refundamt = 0;
                        Amount = document.getElementById('ctl00_ContentPlaceHolder1_' + list + '_' + 'ctrl' + i + '_' + 'txtrefundamt').value;
                        refundamt = document.getElementById('ctl00_ContentPlaceHolder1_refundamount').innerHTML;
                        if (Amount == null || Amount == 'NaN' || Amount == "") {
                            //alert(Amount)
                            Amount = 0;
                        }
                        //refundamt = $("#refundamount").text()
                        //alert(refundamt)
                        //if (parseFloat(Amount) > parseFloat(refundamt)) {
                        //    alert("Amount Is not Greater Than Refundable Amount")
                        //    document.getElementById('ctl00_ContentPlaceHolder1_' + list + '_' + 'ctrl' + i + '_' + 'txtrefundamt').value = "";
                        //    document.getElementById('ctl00_ContentPlaceHolder1_txtAmountRefund').value = "";
                        //    return false;
                        //}
                        //else {
                        //alert(Amount)
                        FinalAmount = parseFloat(FinalAmount) + parseFloat(Amount.replace(/,/g, ''));
                        document.getElementById('ctl00_ContentPlaceHolder1_txtAmountRefund').value = FinalAmount;
                        //}
                    }
                }
                //alert("hii") 

            }
            catch (e) {
            }
        }

    </script>
    <script>
        function UpdateTotalAndBalancemana() {
            try {
                var totalFeeAmt = 0.00;
                var dataRows = null;
                list = 'lvrefundmana';
                var dataRows = document.getElementsByTagName('tr');
                var FinalAmount = 0;
                if (dataRows != null) {
                    for (i = 0; i < dataRows.length - 1; i++) {
                        var Amount = 0;
                        var refundamt = 0;
                        Amount = document.getElementById('ctl00_ContentPlaceHolder1_' + list + '_' + 'ctrl' + i + '_' + 'txtrefundamt').value;
                        refundamt = document.getElementById('ctl00_ContentPlaceHolder1_lblrefundmana').innerHTML;
                        if (Amount == null || Amount == 'NaN' || Amount == "") {
                            //alert(Amount)
                            Amount = 0;
                        }

                        FinalAmount = (parseFloat(FinalAmount) + parseFloat(Amount.replace(/,/g, '')));
                        document.getElementById('ctl00_ContentPlaceHolder1_Txttotalrefundmanage').value = FinalAmount;
                        //}
                    }
                }
                //alert("hii") 

            }
            catch (e) {
            }
        }

    </script>
    <script type="text/javascript" language="javascript">

        function checkAllCheckbox(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                var s = e.name.split("ctl00$ContentPlaceHolder1$lvrefunddetails$ctrl");
                var b = 'ctl00$ContentPlaceHolder1$lvrefunddetails$ctrl';
                var g = b + s[1];
                if (e.name == g) {
                    if (headchk.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }

    </script>
    <script>
        function validation_2Absent(txtMarks) {
            var FinalAmount = 0;
            var txtMaxMark = document.getElementById('ctl00_ContentPlaceHolder1_refundamount').innerHTML;
            // alert(txtMaxMark)

            if (txtMarks.value != "") {

                if (txtMarks != null && txtMarks.value != "") {
                    if (isNaN(txtMarks.value)) {
                        document.getElementById(txtMarks.id).value = '';
                    }
                }
                if (Number(txtMarks.value) > Number(txtMaxMark)) {
                    alert("Amount should not be Greater Than Refundable Amount " + txtMaxMark + "");
                    txtMarks.value = '';
                    return;
                }
                //FinalAmount = parseFloat(FinalAmount) + parseFloat(txtMaxMark);
                //alert(FinalAmount)
                //document.getElementById('ctl00_ContentPlaceHolder1_txtAmountRefund').value = FinalAmount;

            }
        }
    </script>
    <script>
        function validation_2Absentdire(txtMarks) {
            var FinalAmount = 0;
            var txtMaxMark = document.getElementById('ctl00_ContentPlaceHolder1_lblrefunddire').innerHTML;
            // alert(txtMaxMark)

            if (txtMarks.value != "") {

                if (txtMarks != null && txtMarks.value != "") {
                    if (isNaN(txtMarks.value)) {
                        document.getElementById(txtMarks.id).value = '';
                    }
                }
                if (Number(txtMarks.value) > Number(txtMaxMark)) {
                    alert("Amount should not be Greater Than Refundable Amount " + txtMaxMark + "");
                    txtMarks.value = '';
                    return;
                }
                //FinalAmount = parseFloat(FinalAmount) + parseFloat(txtMaxMark);
                //alert(FinalAmount)
                //document.getElementById('ctl00_ContentPlaceHolder1_txtAmountRefund').value = FinalAmount;

            }
        }
    </script>
    <script>
        function validation_2Absentmanager(txtMarks) {
            var FinalAmount = 0;
            var txtMaxMark = document.getElementById('ctl00_ContentPlaceHolder1_lblrefundmana').innerHTML;
            // alert(txtMaxMark)

            if (txtMarks.value != "") {

                if (txtMarks != null && txtMarks.value != "") {
                    if (isNaN(txtMarks.value)) {
                        document.getElementById(txtMarks.id).value = '';
                    }
                }
                if (Number(txtMarks.value) > Number(txtMaxMark)) {
                    alert("Amount should not be Greater Than Refundable Amount " + txtMaxMark + "");
                    txtMarks.value = '';
                    return;
                }
                //FinalAmount = parseFloat(FinalAmount) + parseFloat(txtMaxMark);
                //alert(FinalAmount)
                //document.getElementById('ctl00_ContentPlaceHolder1_txtAmountRefund').value = FinalAmount;

            }
        }
    </script>
</asp:Content>

