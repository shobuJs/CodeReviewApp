<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Withdrawal_Approval_By_Finance.aspx.cs" Inherits="EXAMINATION_Projects_Withdrawal_Approval_By_Finance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
     <style>
        #ctl00_ContentPlaceHolder1_Panel2 .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updfinance"
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
    <asp:UpdatePanel ID="updfinance" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>
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
                                            <%-- <asp:ListItem Value="1">Admission Withdrawal</asp:ListItem>
                                            <asp:ListItem Value="2">Semester Registration</asp:ListItem>
                                            <asp:ListItem Value="3">Postponement</asp:ListItem>
                                            <asp:ListItem Value="4">Pro-Rata</asp:ListItem>--%>
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
                                        <asp:DropDownList ID="ddlfaculty" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnshow" runat="server" Text="Show" ToolTip="Show"
                                    TabIndex="8" CssClass="btn btn-outline-info" OnClick="btnshow_Click" />
                                <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn btn-outline-info"
                                    ToolTip="Show" OnClick="btnSubmit_Click" Visible="false">Submit</asp:LinkButton>
                                <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-outline-danger"
                                    CausesValidation="false" OnClick="btnCancel_Click">Cancel</asp:LinkButton>
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                <asp:HiddenField ID="HiddenField1" runat="server" />
                            </div>

                            <div class="col-md-12" id="divwithapti" runat="server" visible="false">
                                <asp:Panel ID="Panel2" runat="server">
                                    <asp:ListView ID="lvwithap" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Withdrawal Approval List</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="myTable">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>
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
                                                        <th>Uploaded Document</th>
                                                        <th>Refund Details</th>
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
                                                    <%--<asp:CheckBox ID="chktransfer" runat="server" ToolTip='<%# Eval("IDNO")%>' />--%>
                                                    <%# Container.DataItemIndex + 1 %>

                                                </td>
                                                <td>
                                                    <%# Eval("SRNO")%> 
                                                </td>
                                                <td>
                                                    <%# Eval("STATUS")%> 
                                                </td>
                                                <td>
                                                    <%# Eval("REGNO")%> 
                                                </td>
                                                <td>
                                                    <%# Eval("STUDFIRSTNAME")%> 
                                                </td>
                                                <td>
                                                    <%# Eval("APPLIED_DATE")%>
                                                    <%--<%# Eval("EMAILID")%>--%> 
                                                </td>
                                                <%--<td>
                                                    <%# Eval("STUDENTMOBILE")%> 
                                                </td>--%>

                                                <td class="text-center">
                                                    <%--<i class="fa fa-eye" aria-hidden="true" data-toggle="modal" data-target="#view"></i>--%>
                                                    <asp:LinkButton ID="lnkViewDoc" runat="server" CommandArgument='<%#Eval("IDNO") %>' CommandName='<%# Eval("SRNO") %>' ToolTip='<%# Eval("REGNO")%>' 
                                                        OnClick="lnkViewDoc_Click"><i class="fa fa-eye" aria-hidden="true" style="color: #0d70fd; font-size: 24px;"></i></asp:LinkButton>
                                                    <asp:HiddenField ID="status" runat="server" Value='<%#Eval("STATUS") %>' />
                                                </td>
                                                <td class="text-center"><span class="badge badge-success"><%#Eval("FINANCE_APPROVAL") %></span></td>

                                                <td>
                                                    <asp:LinkButton ID="lnkstuddoc" runat="server" CssClass="btn btn-outline-info" CommandArgument='<%#Eval("SRNO") %>' CommandName='<%#Eval("DOCUMENT") %>' ToolTip='<%#Eval("DOCUMENT") %>' OnClick="lnkstuddoc_Click"><i class="fa fa-eye"></i> View</asp:LinkButton>
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="lnkdetails" runat="server" CssClass="btn btn-outline-info" CommandArgument='<%#Eval("SRNO") %>' CommandName='<%#Eval("IDNO") %>' ToolTip='<%#Eval("REGNO") %>' OnClick="lnkdetails_Click"><i class="fa fa-eye"></i> View</asp:LinkButton>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>

                        </div>
                        <asp:UpdatePanel ID="updModelPopup" runat="server">
                            <ContentTemplate>
                                <div id="financemodal" class="modal fade" role="dialog" data-backdrop="static">
                                    <div class="modal-dialog modal-lg">
                                        <div class="modal-content" style="margin-top: -25px">
                                            <div class="modal-body">
                                                <div class="modal-header">
                                                    <%--<button type="button" class="close" data-dismiss="modal" style="margin-top: -18px"  >x</button>--%>
                                                    <%--<button type="button" class="close" data-dismiss="modal">&times;</button>--%>
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
                        <!-- View Modal -->
                        <asp:UpdatePanel ID="updmodal" runat="server">
                            <ContentTemplate>
                                <div class="modal" id="myModal22">
                                    <div class="modal-dialog  modal-xl">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <h4 class="modal-title">View Details</h4>
                                                <button type="button" class="close" data-dismiss="modal" onclick="javascript:doButtonPostBack();">&times;</button>
                                            </div>
                                            <div class="modal-body pl-0 pr-0">
                                                <div class="col-12">
                                                    <div class="row">
                                                        <div class="col-lg-6 col-md-6 col-12">
                                                            <ul class="list-group list-group-unbordered">
                                                                <li class="list-group-item"><b>Application ID :</b>
                                                                    <a class="sub-label">
                                                                        <asp:Label ID="lblStudentID" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                        <asp:HiddenField ID="hdfSrnoWithDrwal" runat="server" />
                                                                        <asp:HiddenField ID="hdfIdnoWithDrwal" runat="server" />
                                                                    </a>
                                                                </li>
                                                                <li class="list-group-item"><b>Submission Date :</b>
                                                                    <a class="sub-label">
                                                                        <asp:Label ID="lblDate" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                                </li>
                                                            </ul>
                                                        </div>
                                                        <div class="col-lg-6 col-md-6 col-12">
                                                            <ul class="list-group list-group-unbordered">
                                                                <li class="list-group-item"><b>Student Name :</b>
                                                                    <a class="sub-label">
                                                                        <asp:Label ID="lblStudentName" runat="server" Text="" Font-Bold="true"></asp:Label></a>
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
                                                            <asp:TextBox ID="txtRequestDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Enabled="false" />
                                                        </div>
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
                                                        <div class="col-lg-6 col-md-6 col-12 pl-md-0">
                                                            <ul class="list-group list-group-unbordered">
                                                                <li class="list-group-item"><b>Remark By Operator :</b>
                                                                    <a class="sub-label">
                                                                        <asp:Label ID="lblFeesPaid" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                    </a>
                                                                </li>
                                                            </ul>
                                                        </div>

                                                        <div class="col-lg-6 col-md-6 col-12 pl-md-0">
                                                            <ul class="list-group list-group-unbordered">
                                                                <li class="list-group-item"><b>Operator Approve Date   :</b>
                                                                    <a class="sub-label">
                                                                        <asp:Label ID="lblopratordate" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                    </a>
                                                                </li>
                                                            </ul>
                                                        </div>
                                                        <div class="col-lg-6 col-md-6 col-12 pl-md-0">
                                                            <ul class="list-group list-group-unbordered">
                                                                <li class="list-group-item"><b>Remark By Director :</b>
                                                                    <a class="sub-label">
                                                                        <asp:Label ID="lblExpectedRefund" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                        <asp:Label ID="reqtype" runat="server" Text="" Font-Bold="true" Visible="false"></asp:Label>
                                                                    </a>
                                                                </li>
                                                            </ul>
                                                        </div>
                                                        <div class="col-lg-6 col-md-6 col-12 pl-md-0">
                                                            <ul class="list-group list-group-unbordered">
                                                                <li class="list-group-item"><b>Manager Approve Date   :</b>
                                                                    <a class="sub-label">
                                                                        <asp:Label ID="lblmandate" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                    </a>
                                                                </li>
                                                            </ul>
                                                        </div>

                                                        <div class="col-lg-6 col-md-6 col-12 pl-md-0">
                                                            <ul class="list-group list-group-unbordered">
                                                                <li class="list-group-item"><b>Remark By Manager :</b>
                                                                    <a class="sub-label">
                                                                        <asp:Label ID="lblFeesBalance" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                    </a>
                                                                </li>
                                                            </ul>
                                                        </div>
                                                        <div class="col-lg-6 col-md-6 col-12 pl-md-0">
                                                            <ul class="list-group list-group-unbordered">
                                                                <li class="list-group-item"><b>Director Approve Date   :</b>
                                                                    <a class="sub-label">
                                                                        <asp:Label ID="lbldirectordate" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                    </a>
                                                                </li>
                                                            </ul>
                                                        </div>
                                                    </div>
                                                </div>

                                                <%-- <div class="col-12 mt-3" id="Withdrawal_Approval" style="display: none">
                                                    <div class="row">
                                                        <div class="col-lg-12 col-md-6 col-12">
                                                            <ul class="list-group list-group-unbordered">
                                                                <li class="list-group-item"><b>Remark :</b>
                                                                    <a class="sub-label">
                                                                        <asp:Label ID="lblRemark" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                    </a>
                                                                </li>
                                                            </ul>
                                                        </div>
                                                    </div>
                                                </div>--%>
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
                                                <div class="col-12 mt-3" id="refunddetails">
                                                    <div class="col-md-12" id="refund" runat="server" visible="false">
                                                        <asp:Panel ID="Panel1" runat="server">
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
                                                                            <asp:CheckBox ID="chktransfer" runat="server" ToolTip='<%# Eval("IDNO")%>' Checked='<%#(Eval("REFUND_AMOUNT").ToString())== string.Empty ?  false : true %>' Enabled='<%#(Eval("REFUND_AMOUNT").ToString())== string.Empty ?  true  : false %>' />
                                                                            <asp:HiddenField ID="hdftempdcr_no" runat="server" Value=<%# Eval("TEMP_DCR_NO")%> />
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
                                                        <div class="form-group col-lg-4 col-md-4 col-12" style="display: none">
                                                            <asp:LinkButton ID="btnCalculateRefund" runat="server" CssClass="btn btn-outline-info" OnClick="btnCalculateRefund_Click">Calculate Refund</asp:LinkButton>
                                                        </div>
                                                        <div class="form-group col-lg-4 col-md-4 col-12" style="display: none">
                                                            <div class="label-dynamic">
                                                                <label>Calculated Amount</label>
                                                            </div>
                                                            <asp:TextBox ID="txtCalculateRefund" runat="server" CssClass="form-control" />
                                                        </div>
                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Total Amount to Refund</label>
                                                            </div>
                                                            <asp:TextBox ID="txtAmountRefund" runat="server" CssClass="form-control" />
                                                        </div>
                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Refund Date</label>
                                                            </div>
                                                            <asp:TextBox ID="txtRequestDate" runat="server" CssClass="form-control" Enabled="false" />
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label style="color: black;">Upload Transaction Details</label>
                                                            </div>
                                                            <div class="logoContainer">
                                                                <img src='<%=Page.ResolveClientUrl("~/IMAGES/default-fileupload.png")%>' alt="upload image" tabindex="2" />
                                                            </div>
                                                            <div class="fileContainer sprite pl-1">
                                                                <span runat="server" id="ufFile"
                                                                    cssclass="form-control" tabindex="7">Upload File</span>
                                                                <asp:FileUpload ID="fuDocument" runat="server" ToolTip="Select file to upload"
                                                                    CssClass="form-control" />
                                                                <asp:RequiredFieldValidator ID="rfvintake" runat="server" ControlToValidate="fuDocument"
                                                                    Display="None" ErrorMessage="Please select file to upload." ValidationGroup="Submit"
                                                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>Approve/Reject</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlstabyfinance" runat="server" CssClass="form-control" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                <asp:ListItem Value="1">Approved</asp:ListItem>
                                                                <asp:ListItem Value="2">Reject</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlstabyfinance"
                                                                Display="None" ErrorMessage="Please select Approve/Reject Status" ValidationGroup="submitfinance" InitialValue="0"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <%-- <sup>*</sup>--%>
                                                                <label>Remark</label>
                                                            </div>
                                                            <asp:TextBox ID="txtremarkfi" runat="server" CssClass="form-control" TextMode="MultiLine" />
                                                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtremarkfi"
                                                                Display="None" ErrorMessage="Please Enter Remark" ValidationGroup="submitfinance"></asp:RequiredFieldValidator>--%>
                                                        </div>
                                                        <div class="col-12 btn-footer">
                                                            <asp:LinkButton ID="btnSubmitWithdrawalApprovalFinance" ValidationGroup="submitfinance" runat="server" CssClass="btn btn-outline-info" OnClick="btnSubmitWithdrawalApprovalFinance_Click">Submit</asp:LinkButton>
                                                            <asp:LinkButton ID="btnCancelWithdrawalApprovalFinance" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancelWithdrawalApprovalFinance_Click">Close</asp:LinkButton>
                                                            <asp:ValidationSummary ID="ValidationSummary4" runat="server" ValidationGroup="submitfinance"
                                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <%--<asp:AsyncPostBackTrigger ControlID="btnCancelWithdrawalApprovalFinance" />--%>
                                <asp:PostBackTrigger ControlID="btnCancelWithdrawalApprovalFinance" />
                                <asp:PostBackTrigger ControlID="btnSubmitWithdrawalApprovalFinance" />
                                <%--<asp:PostBackTrigger ControlID="btnCalculateRefund" />--%>
                                <%--<asp:AsyncPostBackTrigger ControlID="btnCalculateRefund" />--%>
                            </Triggers>
                        </asp:UpdatePanel>

                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script>
        $(document).ready(function () {
            $(document).on("click", ".logoContainer", function () {
                $("#ctl00_ContentPlaceHolder1_FileUpload1").click();
            });
            $(document).on("keydown", ".logoContainer", function () {
                if (event.keyCode === 13) {
                    // Cancel the default action, if needed
                    event.preventDefault();
                    // Trigger the button element with a click
                    $("#ctl00_ContentPlaceHolder1_FileUpload1").click();
                }
            });
        });
    </script>
    <script>
        function IsNumeric(textbox) {
            if (textbox != null && textbox.value != "") {
                if (isNaN(textbox.value)) {
                    document.getElementById(textbox.id).value = '';
                }
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

    <!-- Start End Date Script -->
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
        $("input:file").change(function () {
            var fileName = $(this).val();

            newText = fileName.replace(/fakepath/g, '');
            var newtext1 = newText.replace(/C:/, '');
            //newtext2 = newtext1.replace('//', ''); 
            var result = newtext1.substring(2, newtext1.length);


            if (result.length > 0) {
                $(this).parent().children('span').html(result);
            }
            else {
                $(this).parent().children('span').html("Choose file");
            }
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $("input:file").change(function () {
                var fileName = $(this).val();

                newText = fileName.replace(/fakepath/g, '');
                var newtext1 = newText.replace(/C:/, '');
                //newtext2 = newtext1.replace('//', ''); 
                var result = newtext1.substring(2, newtext1.length);


                if (result.length > 0) {
                    $(this).parent().children('span').html(result);
                }
                else {
                    $(this).parent().children('span').html("Choose file");
                }
            });
        });

    </script>

    <script>
        $("input:file").change(function () {
            //$('.fuCollegeLogo').on('change', function () {

            var maxFileSize = 1000000;
            var fi = document.getElementById('ctl00_ContentPlaceHolder1_fuDocument');
            myfile = $(this).val();
            var ext = myfile.split('.').pop();
            var res = ext.toUpperCase();

            //alert(res)
            if (res != "JPG" && res != "JPEG" && res != "PNG" && res != "PDF") {
                alert("Please Select PDF,PNG,JPEG,JPG File Only.");
                //$('.logoContainer img').attr('src', "../IMAGES/default-fileupload.png");
                $(this).val('');
            }

            for (var i = 0; i <= fi.files.length - 1; i++) {
                var fsize = fi.files.item(i).size;
                if (fsize >= maxFileSize) {
                    alert("File Size should be less than 1 MB");
                    //$('.logoContainer img').attr('src', "../IMAGES/default-fileupload.png");
                    $("#ctl00_ContentPlaceHolder1_fuDocument").val("");

                }
            }

        });
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $("input:file").change(function () {
                //$('.fuCollegeLogo').on('change', function () {

                var maxFileSize = 1000000;
                var fi = document.getElementById('ctl00_ContentPlaceHolder1_fuDocument');
                myfile = $(this).val();
                var ext = myfile.split('.').pop();
                var res = ext.toUpperCase();

                //alert(res)
                if (res != "JPG" && res != "JPEG" && res != "PNG" && res != "PDF") {
                    alert("Please Select PDF,PNG,JPEG,JPG File Only.");
                    //$('.logoContainer img').attr('src', "../IMAGES/default-fileupload.png");
                    $(this).val('');
                }

                for (var i = 0; i <= fi.files.length - 1; i++) {
                    var fsize = fi.files.item(i).size;
                    if (fsize >= maxFileSize) {
                        alert("File Size should be less than 1 MB");
                        //$('.logoContainer img').attr('src', "../IMAGES/default-fileupload.png");
                        $("#ctl00_ContentPlaceHolder1_fuDocument").val("");

                    }
                }

            });
        });
    </script>
    <script type="text/javascript">
        function doButtonPostBack() {
            __doPostBack('Withdrawal_Approval_By_Finance.aspx', '');
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
</asp:Content>

