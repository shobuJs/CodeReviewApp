<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Other_Currency_Payment.aspx.cs" Inherits="ACADEMIC_Other_Currency_Payment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>

        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updmulticurrency"
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
    <asp:UpdatePanel ID="updmulticurrency" runat="server">
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
                                    <div class="col-lg-6 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item">
                                                <b>Student Name. :</b>
                                                <a class="pull-right">
                                                    <asp:Label ID="lblStudName" CssClass="data_label" Font-Bold="true" runat="server" />
                                                </a>
                                            </li>

                                            <li class="list-group-item">
                                                <b>Student ID. :</b>
                                                <a class="pull-right">
                                                    <asp:Label ID="lblRegNo" CssClass="data_label" Font-Bold="true" runat="server" />
                                                </a>
                                            </li>
                                            <li class="list-group-item">
                                                <b>Year :</b>
                                                <a class="pull-right">
                                                    <asp:Label ID="lblYear" CssClass="data_label" Font-Bold="true" runat="server" />
                                                </a>
                                            </li>
                                            <li class="list-group-item">
                                                <b>Intake :</b>
                                                <a class="pull-right">
                                                    <asp:Label ID="lblBatch" CssClass="data_label" Font-Bold="true" runat="server" />
                                                </a>
                                            </li>
                                            <li class="list-group-item">
                                                <b>Semester :</b>
                                                <a class="pull-right">
                                                    <asp:Label ID="lblSemester" CssClass="data_label" Font-Bold="true" runat="server" />
                                                </a>
                                            </li>
                                            <li class="list-group-item">
                                                <b>Payment Type :</b>
                                                <a class="pull-right">
                                                    <asp:Label ID="lblPaymentType" CssClass="data_label" Font-Bold="true" runat="server" />
                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-lg-6 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item">
                                                <b>Faculty/School Name  :</b>
                                                <a class="pull-right">
                                                    <asp:Label ID="lblCollege" CssClass="data_label" Font-Bold="true" runat="server" />
                                                    <asp:HiddenField ID="hdnCollege" Value="" runat="server" />
                                                </a>
                                            </li>

                                            <li class="list-group-item">
                                                <b>Program :</b>
                                                <a class="pull-right">
                                                    <asp:Label ID="lblDegree" CssClass="data_label" Font-Bold="true" runat="server" />
                                                </a>
                                            </li>
                                            <%-- <li class="list-group-item">
                                                <b>Branch :</b>
                                                <a class="pull-right">
                                                    <asp:Label ID="lblBranchs" CssClass="data_label" Font-Bold="true" runat="server" />

                                                </a>
                                            </li>--%>

                                            <li class="list-group-item">
                                                <b>Gender :</b>
                                                <a class="pull-right">
                                                    <asp:Label ID="lblSex" CssClass="data_label" Font-Bold="true" runat="server" />
                                                </a>
                                            </li>
                                            <li class="list-group-item">
                                                <b>Email ID :</b>
                                                <a class="pull-right">
                                                    <asp:Label ID="lblEmailID" CssClass="data_label" Font-Bold="true" runat="server" />
                                                </a>
                                            </li>
                                            <li class="list-group-item">
                                                <b>Mobile No. :</b>
                                                <a class="pull-right">
                                                    <asp:Label ID="lblMobileNo" CssClass="data_label" Font-Bold="true" runat="server" />
                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                    <br />
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblCurrency" runat="server" Font-Bold="true" Text="Currency"></asp:Label>
                                            <%--<label><b></b></label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlReceiptType" CssClass="form-control" runat="server" AppendDataBoundItems="true" data-select2-enable="true" OnSelectedIndexChanged="ddlReceiptType_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divsemester" runat="server">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYYear" runat="server" Font-Bold="true" Text="Year"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" visible="false" runat="server" id="divamount">
                                        <label>Amount <span style="color: red;">*</span></label>
                                        <div class="input-group">
                                            <div class="input-group-addon">
                                                <div class="fa fa-inr text-green"></div>
                                            </div>
                                            <asp:TextBox ID="txtAmount" runat="server" TabIndex="3" CssClass="form-control" ToolTip="Please Enter Amount" onkeypress="CheckNumeric(event);" Width="90px" MaxLength="10">
                                            </asp:TextBox>
                                            <div class="input-group-addon">
                                                <span>.00</span>
                                            </div>
                                        </div>
                                        <%-- <asp:RequiredFieldValidator ID="rfvtxtamount" runat="server" ControlToValidate="txtAmount" ValidationExpression="(^([0-9]*|\d*\d{1}?\d*)$)" 
                                            Display="None" ErrorMessage="Please Enter Amount" ValidationGroup="save"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12">
                                <asp:Panel ID="pnlStudentsFees" runat="server" Visible="false">
                                    <asp:ListView ID="lvStudentFees" runat="server" OnItemDataBound="lvStudentFees_ItemDataBound" OnPreRender="lvStudentFees_PreRender">
                                        <LayoutTemplate>
                                            <div>
                                                <div>
                                                    <b>Fees Details</b>
                                                </div>
                                                <table id="tblHead" class="table table-hover table-bordered">
                                                    <thead class="bg-light-blue">
                                                        <tr id="trRow">
                                                            <%--<th id="thHead" style="width: 5%"></th>--%>
                                                            <th>SrNo.
                                                            </th>
                                                            <th>Fees Head
                                                            </th>
                                                            <th>Amount
                                                            </th>
                                                            <%--<th>
                                                                Status
                                                            </th>--%>
                                                        </tr>

                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                    <%--<tfoot><asp:Label ID="lbltotal" runat="server"  Text="0"></asp:Label></tfoot>--%>
                                                    <thead class="bg-light-blue">
                                                        <tr id="Tr1" runat="server">
                                                            <th></th>
                                                            <th><span class="pull-right">Total Amount</span></th>

                                                            <th id="Td1" runat="server">
                                                                <asp:Label ID="lbltotal" CssClass="data_label" runat="server" Text="0"></asp:Label>
                                                            </th>
                                                             <%--<th>
                                                                 <asp:Label ID="lblstatus" CssClass="data_label" runat="server" Font-Bold="true" ForeColor="Red" ></asp:Label>

                                                            </th>--%>
                                                        </tr>
                                                    </thead>
                                                </table>

                                            </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <%--<td style="width: 10%"></td>--%>
                                                <td><%# Eval("SRNO") %>
                                                </td>
                                                <td><%# Eval("FEE_LONGNAME") %></td>
                                                <td>
                                                    <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("AMOUNT") %>'></asp:Label></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>

                                </asp:Panel>
                               <div class="col-md-12" id="divstats" runat="server" visible="false" >
                                   <asp:Label ID="lblstatus"  runat="server" Font-Bold="true" ForeColor="Red" Text="" ></asp:Label>
                               </div>
                                <div class="col-lg-3 col-md-6 col-8 d-none" id="payoption" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <label for="RdPay"><sup>*</sup> Payment Option : </label>
                                    </div>
                                    <asp:RadioButtonList ID="rdPaymentOption" runat="server" TabIndex="6" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdPaymentOption_SelectedIndexChanged">
                                        <asp:ListItem Text="&nbsp;Offline Payment &nbsp;&nbsp;" Value="0" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="&nbsp;Online Payment" Value="1"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Select Payment Option"
                                                                 ControlToValidate="rdPaymentOption" Display="None" ValidationGroup="Summary"></asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="col-12 btn-footer" id="divOfflinePay" runat="server" visible="false">
                                    <div class="row">
                                        <div class="col-12 col-md-2" id="divUploadgENChallan" runat="server">
                                            <span id="btnGenerateChallan" style="cursor: pointer" class="btn btn-outline-info" data-toggle="modal" data-target="#modelBank">View Bank Details</span>
                                            <%--  <asp:Button ID="btnGenerateChallan" runat="server" TabIndex="6" ValidationGroup="sub" Text="View Bank Details" CssClass="btn btn-outline-info"/>--%>
                                        </div>
                                        <div class="col-12 col-md-3" id="divUploadChallan" runat="server">
                                            <span id="myBtnDeposit" style="cursor: pointer" class="btn btn-outline-info" data-toggle="modal" data-target="#myModalChallan">Upload Deposit Slip</span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <asp:Label ID="divShowPay" runat="server" CssClass="data_label"></asp:Label>
                                </div>
                                <p class="text-center d-none">
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Font-Bold="True" OnClientClick="javascript: return fnConfirm();" Text="Pay" ValidationGroup="submit" Visible="false" />
                                    <asp:Button ID="btnReport" runat="server" CssClass="btn btn-primary" Enabled="false" Font-Bold="True" Text="Print Challan" ValidationGroup="submit" Visible="false" />
                                    <asp:Button ID="btnCancel" runat="server" CausesValidation="False" CssClass="btn btn-warning d-none" Font-Bold="True" Text="Cancel" Visible="false" />
                                    <asp:ValidationSummary ID="SUMMARY" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="submit" />
                                </p>
                            </div>
                        </div>
                        <div class="col-12">
                            <asp:Panel ID="pnlcurr" runat="server" Visible="false">
                                <asp:ListView ID="LvOtherCurrency" runat="server">
                                    <LayoutTemplate>
                                        <div class="sub-heading" id="dem">
                                            <h5>Previous Receipts Information</h5>
                                        </div>

                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="myTable1">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th>Receipt Type
                                                    </th>
                                                    <th>Receipt No
                                                    </th>
                                                    <th>Date
                                                    </th>
                                                    <th>Semester
                                                    </th>
                                                    <th>Pay Type
                                                    </th>
                                                    <th>Amount
                                                    </th>
                                                    <th>Payment Status
                                                    </th>
                                                    <th>Print
                                                    </th>
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
                                                <%# Eval("RECIEPT_TITLE") %>
                                            </td>
                                            <td>
                                                <%# Eval("REC_NO") %>
                                            </td>
                                            <td>
                                                <%# (Eval("REC_DT").ToString() != string.Empty) ? ((DateTime)Eval("REC_DT")).ToShortDateString() : Eval("REC_DT") %>
                                            </td>
                                            <td>
                                                <%# Eval("SEMESTERNAME") %>
                                            </td>
                                            <td>
                                                <%# Eval("PAY_TYPE") %>
                                            </td>
                                            <td>
                                                <%# Eval("TOTAL_AMT") %>
                                            </td>
                                            <td>
                                                <%# Eval("PAY_STATUS") %>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="btnPrintReceipt" runat="server" OnClick="btnPrintReceipt_Click" Enabled='<%# (Convert.ToInt32(Eval("RECON")) == 0 ? false : true) %>'
                                                    CommandArgument='<%# Eval("DCR_NO") %>' ImageUrl="~/images/print.gif" ToolTip='<%# Eval("RECIEPT_CODE")%>' CausesValidation="False" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="ddlSemester" />
            <asp:PostBackTrigger ControlID="rdPaymentOption" />
            <asp:PostBackTrigger ControlID="LvOtherCurrency" />
        </Triggers>
    </asp:UpdatePanel>
    <div class="col-md-12">
        <div id="divMsg" runat="server">
        </div>
    </div>
    <div id="myModalChallan" class="modal" role="dialog">
        <div class="modal-dialog model-lg">
            <!-- Modal content-->
            <asp:UpdatePanel ID="updChallan" runat="server">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4>Upload Deposit</h4>
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-xl-12 col-md-12" id="divTransactionid" runat="server" visible="false">
                                    <div class="md-form">
                                        <label for="UserName"><sup></sup>Transaction Id</label>
                                        <asp:TextBox ID="txtTransactionNo" TabIndex="1" runat="server" MaxLength="20" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please Enter Challan Transaction Id"
                                            ControlToValidate="txtTransactionNo" Display="None" ValidationGroup="SubmitChallan"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-xl-12 col-md-12" id="DivOrderId" runat="server" visible="false">
                                    <div class="md-form">
                                        <label for="UserName"><sup></sup>Challan Order ID </label>
                                        <asp:TextBox ID="txtChallanId" runat="server" MaxLength="20" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please Enter Challan Order ID"
                                            ControlToValidate="txtChallanId" Display="None" ValidationGroup="SubmitChallan"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-xl-12 col-md-12">
                                    <div class="md-form">
                                        <label for="UserName"><sup></sup>Bank</label>
                                        <asp:DropDownList ID="ddlbank" runat="server" AppendDataBoundItems="true" CssClass="form-control select2 select-click" TabIndex="2">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Please Select Bank Name"
                                            ControlToValidate="ddlbank" InitialValue="0" Display="None" ValidationGroup="SubmitChallan"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-xl-12 col-md-12">
                                    <div class="md-form">
                                        <label for="UserName"><sup></sup>Branch</label>
                                        <asp:TextBox ID="txtBranchName" runat="server" CssClass="form-control" TabIndex="3" MaxLength="50"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Please Enter Bank Branch"
                                            ControlToValidate="txtBranchName" Display="None" ValidationGroup="SubmitChallan"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-xl-12 col-md-12">
                                    <div class="md-form">
                                        <label for="UserName"><sup></sup>Amount</label>
                                        <asp:TextBox ID="txtchallanAmount" TabIndex="4" runat="server" MaxLength="10" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Please Enter Amount"
                                            ControlToValidate="txtchallanAmount" Display="None" ValidationGroup="SubmitChallan"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtchallanAmount" ValidChars="1234567890." FilterMode="ValidChars" />
                                    </div>
                                </div>
                                <div class="col-xl-12 col-md-12">
                                    <div class="md-form">
                                        <label for="UserName"><sup></sup>Date of Payment</label>
                                        <asp:TextBox ID="txtPaymentdate" TabIndex="5" runat="server" MaxLength="20" CssClass="form-control PaymentDate"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Please Enter Date of Payment"
                                            ControlToValidate="txtPaymentdate" Display="None" ValidationGroup="SubmitChallan"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-xl-12 col-md-12">
                                    <div class="md-form">
                                        <label for="UserName"><sup></sup>Upload Deposit Slip</label><br />
                                        <asp:FileUpload ID="FuChallan" runat="server" onchange="setUploadButtonState();" Style="margin-top: 8px;" TabIndex="6" CssClass="form-control" /><br />
                                        <span style="color: red; font-size: small">
                                            <asp:Label ID="lblMsg" runat="server" Text="Image Size Not Greater Than 1MB and image format JPG,JPEG,PNG,PDF Allowed"></asp:Label>
                                        </span>
                                    </div>
                                </div>
                                <div class="col-12 text-center">
                                    <asp:Button ID="btnChallanSubmit" runat="server" TabIndex="7" Text="Submit" CssClass="btn btn-outline-info" ValidationGroup="SubmitChallan" OnClick="btnChallanSubmit_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="SubmitChallan" />
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-12 mb-3">
                                    <asp:ListView ID="lvDepositSlip" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Deposit Slip Detail</h5>
                                            </div>
                                            <div class="table table-responsive">
                                                <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <td>Action</td>
                                                            <td>Bank Name</td>
                                                            <td>Bank Branch</td>
                                                            <td>Amount</td>
                                                            <td>Date of Payment</td>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="btnedit" runat="server" ImageUrl="~/IMAGES/edit.png" CommandArgument='<%# Eval("TEMP_DCR_NO") %>'
                                                        AlternateText="Delete Record" OnClick="btnedit_Click"
                                                        TabIndex="14" ToolTip="Edit" />
                                                </td>
                                                <td><%# Eval("BANK_NAME") %></td>
                                                <td><%# Eval("BRANCH_NAME") %></td>
                                                <td><%# Eval("TOTAL_AMT") %></td>
                                                <td><%# Eval("REC_DT") %></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>

                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnChallanSubmit" />
                    <%--<asp:AsyncPostBackTrigger ControlID="ddlbank" />--%>
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>


    <div id="modelBank" class="modal fade" role="dialog">
        <div class="modal-dialog modal-xl">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <h4>Bank Details</h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-12 mb-3">
                            <asp:ListView ID="lvBankDetails" runat="server">
                                <LayoutTemplate>
                                    <div class="sub-heading">
                                        <h5>Bank Details List</h5>
                                    </div>
                                    <div class="table table-responsive">
                                        <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <td>SrNo</td>
                                                    <td>Bank Code</td>
                                                    <td>Bank Name</td>
                                                    <td>Bank Branch</td>
                                                    <td>Bank Account No.</td>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                        </table>
                                    </div>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td><%# Container.DataItemIndex + 1 %></td>
                                        <td><%# Eval("BANKCODE") %></td>
                                        <td><%# Eval("BANKNAME") %></td>
                                        <td><%# Eval("BANKADDR") %></td>
                                        <td><%# Eval("ACCOUNT_NO") %></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        $(document).ready(function () {
            var dateval = document.getElementById('<%=txtPaymentdate.ClientID%>').value;

            var prev_date = new Date();
            prev_date.setDate(prev_date.getDate());
            $('.PaymentDate').daterangepicker({
                singleDatePicker: true,
                showDropdowns: true,
                //minDate: '01/1/1975',
                maxDate: prev_date,
                locale: {
                    format: 'DD/MM/YYYY'
                },
                autoApply: true,
            });
            if (dateval == "") {
                $('.PaymentDate').val('');
            }
        });
    </script>
    <script>
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                var dateval = document.getElementById('<%=txtPaymentdate.ClientID%>').value;

                var prev_date = new Date();
                prev_date.setDate(prev_date.getDate());
                $('.PaymentDate').daterangepicker({
                    singleDatePicker: true,
                    showDropdowns: true,
                    //minDate: '01/1/1975',
                    maxDate: prev_date,
                    locale: {
                        format: 'DD/MM/YYYY'
                    },
                    autoApply: true,
                });
                if (dateval == "") {
                    $('.PaymentDate').val('');
                }
            });
        });
    </script>
    <script type="text/javascript">
        function setUploadButtonState() {

            var maxFileSize = 1000000;
            var fi = document.getElementById('ctl00_ContentPlaceHolder1_FuChallan');
            for (var i = 0; i <= fi.files.length - 1; i++) {

                var fsize = fi.files.item(i).size;

                if (fsize >= maxFileSize) {
                    alert("Image Size Greater Than 1MB");
                    $("#ctl00_ContentPlaceHolder1_FuChallan").val("");

                }
            }
            var fileExtension = ['jpeg', 'jpg', 'JPG', 'JPEG', 'PNG', 'png', 'PDF', 'pdf'];
            if ($.inArray($('#ctl00_ContentPlaceHolder1_FuChallan').val().split('.').pop().toLowerCase(), fileExtension) == -1) {
                alert("Only formats are allowed : " + fileExtension.join(', '));
                $("#ctl00_ContentPlaceHolder1_FuChallan").val("");
            }
        }

    </script>
    <script type="text/javascript">
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            function setUploadButtonState() {

                var maxFileSize = 1000000;
                var fi = document.getElementById('ctl00_ContentPlaceHolder1_FuChallan');
                for (var i = 0; i <= fi.files.length - 1; i++) {

                    var fsize = fi.files.item(i).size;

                    if (fsize >= maxFileSize) {
                        alert("Image Size Greater Than 1MB");
                        $("#ctl00_ContentPlaceHolder1_FuChallan").val("");

                    }
                }
                var fileExtension = ['jpeg', 'jpg', 'JPG', 'JPEG', 'PNG', 'png', 'PDF', 'pdf'];
                if ($.inArray($('#ctl00_ContentPlaceHolder1_FuChallan').val().split('.').pop().toLowerCase(), fileExtension) == -1) {
                    alert("Only formats are allowed : " + fileExtension.join(', '));
                    $("#ctl00_ContentPlaceHolder1_FuChallan").val("");
                }
            }
        });
    </script>
</asp:Content>
