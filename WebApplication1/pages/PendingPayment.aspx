<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="PendingPayment.aspx.cs" Inherits="ACADEMIC_PendingPayment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <link href='<%=Page.ResolveClientUrl("~/css/jquery-ui.css")%>' rel="stylesheet" />
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updFeesDetails"
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
    <asp:UpdatePanel ID="updFeesDetails" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                               <asp:HiddenField ID="hdnClientId" runat="server" Value="0" />
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row" id="DivSearch" runat="server" visible="false">
                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Search Student</label>
                                        </div>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtEnrollNo" runat="server" ValidationGroup="submit" ToolTip="Enter Student ID/Registration ID/Student Name" CssClass="form-control search" placeholder="Enter Student ID/Registration ID/Student Name"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="valEnrollNo" runat="server" ControlToValidate="txtEnrollNo"
                                                Display="None" ErrorMessage="Please Enter Student ID/Registration ID/Student Name." SetFocusOnError="true"
                                                ValidationGroup="studSearch" />
                                        </div>
                                    </div>
                                    <div class="form-group col-lg-2 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label></label>
                                        </div>
                                        <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-outline-info m-top" ValidationGroup="studSearch" OnClick="btnShow_Click" />
                                        <asp:ValidationSummary ID="valSummary3" runat="server" DisplayMode="List" ShowMessageBox="true"
                                            ShowSummary="false" ValidationGroup="studSearch" />
                                    </div>
                                </div>
                                <!-- Modal -->
                                <asp:HiddenField ID="hdfOrderID" runat="server" Value="0" />
                                <asp:HiddenField ID="hdfServiceCharge" runat="server" Value="0" />

                                <div class="row" id="divstudinfo" runat="server" visible="false">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="col-12">
                                                <div class="sub-heading">
                                                    <h5>STUDENT INFORMATION</h5>
                                                </div>
                                            </div>
                                            <div class="col-12">
                                                <div id="divStudentInfo" style="display: block;">
                                                    <div class="row">
                                                        <div class="col-lg-4 col-md-6 col-12">
                                                            <ul class="list-group list-group-unbordered ipad-view">
                                                                <%--<b>Admission No. :</b><a class="pull-right">
                                                                        <asp:Label ID="lblRegNo" runat="server" Font-Bold="True"></asp:Label></a>
                                                                    <p></p>--%>
                                                                <li class="list-group-item"><b>Student ID. :</b>
                                                                    <a class="sub-label">
                                                                        <asp:Label ID="lblenroll" runat="server" Font-Bold="True"></asp:Label>
                                                                    </a>
                                                                </li>
                                                                <li class="list-group-item">
                                                                    <b>Name With Initial:</b>
                                                                    <a class="sub-label">
                                                                        <asp:Label ID="lblStudName" runat="server" Font-Bold="True"></asp:Label>
                                                                    </a>
                                                                </li>
                                                                <li class="list-group-item">
                                                                    <b>Mobile No. :</b><a class="pull-right">
                                                                        <asp:Label ID="lblMobileNo" runat="server" Font-Bold="True"></asp:Label></a>
                                                                </li>

                                                            </ul>
                                                        </div>
                                                        <div class="col-lg-4 col-md-6 col-12">
                                                            <ul class="list-group list-group-unbordered">
                                                                <li class="list-group-item"><b>Registration No. :</b>
                                                                    <a class="sub-label">
                                                                        <asp:Label ID="lblRegNo" runat="server" Font-Bold="True"></asp:Label>
                                                                    </a>
                                                                </li>
                                                                <li class="list-group-item">
                                                                    <b>Semester :</b><a class="pull-right">
                                                                        <asp:Label ID="lblSemester" runat="server" Font-Bold="True"></asp:Label></a>
                                                                </li>
                                                                <li class="list-group-item" style="display: none">
                                                                    <b>Date of Admission :</b><a class="pull-right">
                                                                        <asp:Label ID="lblDateOfAdm" runat="server" Font-Bold="True"></asp:Label></a>
                                                                </li>
                                                                <li class="list-group-item">
                                                                    <b>Year :</b><a class="pull-right">
                                                                        <asp:Label ID="lblYear" runat="server" Font-Bold="True"></asp:Label></a>
                                                                </li>
                                                            </ul>
                                                        </div>
                                                        <div class="col-lg-4 col-md-6 col-12">
                                                            <ul class="list-group list-group-unbordered">
                                                                <li class="list-group-item">
                                                                    <b>College :</b><a class="pull-right">
                                                                        <asp:Label ID="lblCollege" runat="server" Font-Bold="True"></asp:Label></a>
                                                                </li>
                                                                <li class="list-group-item">
                                                                    <b>Program :</b><a class="pull-right">
                                                                        <asp:Label ID="lblDegree" runat="server" Font-Bold="True"></asp:Label></a>
                                                                </li>
                                                                <%-- <li class="list-group-item">
                                                                        <b>Specialization :</b><a class="pull-right">
                                                                            <asp:Label ID="lblBranch" runat="server" Font-Bold="True"></asp:Label></a>
                                                                    </li>--%>
                                                                <li class="list-group-item">
                                                                    <b>Gender :</b><a class="pull-right">
                                                                        <asp:Label ID="lblSex" runat="server" Font-Bold="True"></asp:Label></a>
                                                                    <br />
                                                                </li>
                                                                <li class="list-group-item" style="display: none">
                                                                    <b>Payment Type :</b><a class="pull-right">
                                                                        <asp:Label ID="lblPaymentType" runat="server" Font-Bold="True"></asp:Label></a>
                                                                </li>
                                                            </ul>
                                                            <br />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group col-md-12 table table-responsive">
                                        <asp:Panel ID="Panel1" runat="server">
                                            <asp:ListView ID="lvFeesDetails" runat="server">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Fees Details</h5>
                                                    </div>
                                                    <%--<h4>Fees Details</h4>--%>
                                                    <table id="mytable" class="table table-hover table-bordered" width="100%">
                                                        <thead>
                                                            <tr class="bg-light-blue">
                                                                <th>Semester</th>
                                                                <th>Payment For</th>
                                                                <th>Percentage</th>
                                                                <th>Total Payable</th>
                                                                <th>Paid Amount</th>
                                                                <th>Remaining Amount</th>
                                                                <th>DueDate</th>
                                                                <th>Remark</th>
                                                                <th>Pay In Cash</th>
                                                                <th>Pay In Bank</th>
                                                                <th>Promissory</th>
                                                                <th>Promissory Status</th>
                                                                <th>Promissory Remark</th>
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
                                                            <%# Eval("SEMESTER_NAME") %>
                                                            <asp:Label ID="lblInstallmentno" runat="server" Text='<%# Eval("INSTALL_NO") %>' Visible="false"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <%# Eval("INSTALL_NAME") %>
                                                        </td>
                                                        <td>
                                                            <%# Eval("PER") %><asp:HiddenField ID="hdfSemesterNo" runat="server" Value='<%#Eval("SEMESTERNO") %>' />
                                                        </td>

                                                        <td>
                                                            <asp:Label ID="lblFinalAmount" runat="server" Text='<%# Eval("TOTAL_AMT") %>'></asp:Label>
                                                            <asp:HiddenField ID="hdfdate" runat="server" Value='<%# Eval("DATE") %>' />
                                                        </td>
                                                         <td>
                                                            <%# Eval("PAID_AMOUNT") %>
                                                        </td>
                                                         <td>
                                                            <%# Eval("REMAINING_AMOUNT") %>
                                                        </td>
                                                        <td>
                                                            <%# Eval("DATE") %>
                                                        </td>

                                                        <td>
                                                            <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("REMARK") %>'></asp:Label>
                                                        </td>
                                                       <%-- <td>
                                                            <asp:LinkButton ID="lnkOffline" runat="server" Visible='<%# Eval("RECON").ToString() == "1" ? false : true %>' CommandName='<%# Eval("TOTAL_AMT") + "$" + Eval("SEMESTERNO") + "$" + Eval("SESSIONNO") %>' CommandArgument='<%# Eval("FEES_INSTALL_NO") + "$" + Eval("INSTALL_AMOUNT") %>' CssClass="btn btn-outline-info" Text="Pay In Cash" ToolTip='<%# Eval("REMARK") %>' OnClick="btnPayOnline_Click" OnClientClick="return confirm('Are you sure you want to confirm ?')"></asp:LinkButton>
                                                        </td>
                                                        <td>
                                                            <asp:LinkButton ID="lnkPay" runat="server" Visible='<%# Eval("RECON").ToString() == "1" ? false : true %>' CommandName='<%# Eval("TOTAL_AMT") + "$" + Eval("SEMESTERNO") + "$" + Eval("SESSIONNO") %>' CommandArgument='<%# Eval("FEES_INSTALL_NO") + "$" + Eval("INSTALL_AMOUNT") %>' CssClass="btn btn-outline-info" Text="Pay In Bank" ToolTip='<%# Eval("REMARK") %>' OnClick="btnPayOffline_Click"></asp:LinkButton>
                                                        </td>--%>

                                                         <td>
                                                            <asp:LinkButton ID="lnkOffline" runat="server" Visible='<%# Eval("RECON").ToString() == "1" ? false : true %>' CommandName='<%# Eval("REMAINING_AMOUNT") + "$" + Eval("SEMESTERNO") + "$" + Eval("SESSIONNO") %>' CommandArgument='<%# Eval("FEES_INSTALL_NO") + "$" + Eval("INSTALL_AMOUNT") %>' CssClass="btn btn-outline-info" Text="Pay In Cash" ToolTip='<%# Eval("REMARK") %>' OnClick="btnPayOnline_Click" OnClientClick="return confirm('Are you sure you want to confirm ?')"></asp:LinkButton>
                                                        </td>
                                                        <td>
                                                            <asp:LinkButton ID="lnkPay" runat="server" Visible='<%# Eval("RECON").ToString() == "1" ? false : true %>' CommandName='<%# Eval("REMAINING_AMOUNT") + "$" + Eval("SEMESTERNO") + "$" + Eval("SESSIONNO") %>' CommandArgument='<%# Eval("FEES_INSTALL_NO") + "$" + Eval("INSTALL_AMOUNT") %>' CssClass="btn btn-outline-info" Text="Pay In Bank" ToolTip='<%# Eval("REMARK") %>' OnClick="btnPayOffline_Click"></asp:LinkButton>
                                                        </td>
                                                        <td>
                                                            <asp:LinkButton ID="lnkPromissorynote" runat="server" Visible="false" CommandName='<%# Eval("TOTAL_AMT") + "$" + Eval("SEMESTERNO") + "$" + Eval("SESSIONNO") %>' CommandArgument='<%# Eval("FEES_INSTALL_NO") + "$" + Eval("TOTAL_AMT") + "$" + Eval("PROMISSORY_REASON") + "$" + Eval("PROMISSORY_DATE") %>' CssClass="btn btn-outline-info" Text="Promissory" OnClick="lnkPromissorynote_Click"></asp:LinkButton>
                                                        </td>
                                                        <td>
                                                            <%# Eval("PROMISSORY_STATUS") %>
                                                        </td>
                                                        <td>
                                                            <%# Eval("PROMISSORY_REMARK") %>
                                                        </td>
                                                        <%--<td>
                                                            <%# Container.DataItemIndex + 1%>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("SEMESTERNAME") %>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblDemandAmount" runat="server" Text='<%# Eval("DEMAND_AMOUNT") %>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblPaidAmount" runat="server" Text='<%# Eval("PAID_AMOUNT") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblRemainingAmount" runat="server" Text='<%# Eval("BALAMT") %>' />
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="btnPayOffline" runat="server" CommandArgument='<%# Eval("SEMESTERNO") + "-" + Eval("SESSIONNO") %>' CommandName='<%# Eval("REMAINING_AMOUNT") %>' Text="Pay Offline" CssClass="btn btn-outline-info" OnClick="btnPayOffline_Click" />
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="btnPayOnline" runat="server" CommandArgument='<%# Eval("SEMESTERNO") + "-" + Eval("SESSIONNO") %>' CommandName='<%# Eval("REMAINING_AMOUNT") %>' Text="Pay Online" CssClass="btn btn-outline-info" OnClick="btnPayOnline_Click" />
                                                        </td>--%>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>

                                    <div class="col-12">
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                    <asp:ListView ID="lvPaidReceipts" runat="server">
                                                        <LayoutTemplate>
                                                            <div class="sub-heading">
                                                                <h5>Previous Receipts Information</h5>
                                                            </div>
                                                            <thead class="bg-light-blue">
                                                                <tr>
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
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr class="item">

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
                                                                    <asp:Label ID="AllReceipts" runat="server" Text='<%# Eval("TOTAL_AMT") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="AllReceiptsStatus" runat="server" Text='<%# Eval("PAY_STATUS") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:ImageButton ID="btnPrintReceipt" runat="server" OnClick="btnPrintReceipt_Click" Enabled='<%# (Convert.ToInt32(Eval("RECON")) == 0 ? false : true) %>' Visible='<%# (Convert.ToString(Eval("PAY_TYPE")) == "OFFLINE" || Convert.ToString(Eval("PAY_TYPE")) == "ONLINE" ? true : false) %>'
                                                                        CommandArgument='<%# Eval("DCR_NO") %>' ImageUrl="~/images/print.gif" ToolTip='<%# Eval("RECIEPT_CODE")%>' CausesValidation="False" />
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </table>

                                            </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="myModalChallan" class="modal fade" role="dialog">
                    <div class="modal-dialog modal-xl">
                        <!-- Modal content-->


                        <div class="modal-content">
                            <div class="modal-header">
                                <h4>Upload Deposit</h4>
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                            </div>
                            <div class="modal-body">

                                <asp:UpdatePanel ID="updChallan" runat="server">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="form-group col-md-3 col-12">
                                                <div class="label-dynamic">
                                                    <label>Actual Amount To Be Paid</label>
                                                </div>
                                            </div>
                                            <div class="form-group col-md-3 col-12">
                                                <asp:TextBox ID="txtchallanAmount" TabIndex="4" runat="server" MaxLength="10" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Please Enter Amount"
                                                    ControlToValidate="txtchallanAmount" Display="None" ValidationGroup="SubmitChallan"></asp:RequiredFieldValidator>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtchallanAmount" ValidChars="1234567890.-," FilterMode="ValidChars" />
                                            </div>

                                            <div class="form-group col-md-6 col-12">
                                                <p style="color: red; float: right;">
                                                    <asp:Label ID="lblMsg" runat="server" Text="(Image Size Not Greater Than 1MB and image format JPG,JPEG,PNG & pdf Allowed)"></asp:Label>
                                                </p>
                                            </div>
                                            <div class="form-group col-md-2 col-6">
                                                <div class="label-dynamic">
                                                    <label>Bank</label>
                                                </div>
                                                <asp:DropDownList ID="ddlbank" runat="server" AppendDataBoundItems="true" CssClass="form-control select2 select-click" TabIndex="2">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Please Select Bank Name"
                                                    ControlToValidate="ddlbank" InitialValue="0" Display="None" ValidationGroup="SubmitChallan"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-md-2 col-6">
                                                <div class="label-dynamic">
                                                    <label>Branch</label>
                                                </div>
                                                <asp:TextBox ID="txtBranchName" runat="server" CssClass="form-control" TabIndex="3" MaxLength="50"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Please Enter Bank Branch"
                                                    ControlToValidate="txtBranchName" Display="None" ValidationGroup="SubmitChallan"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-md-2 col-6">
                                                <div class="label-dynamic">
                                                    <label>Deposit Amount</label>
                                                </div>
                                                <asp:TextBox ID="txtDepositAmount" TabIndex="5" runat="server" MaxLength="10" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Enter Deposit Amount"
                                                    ControlToValidate="txtDepositAmount" Display="None" ValidationGroup="SubmitChallan"></asp:RequiredFieldValidator>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtDepositAmount" ValidChars="1234567890.," FilterMode="ValidChars" />
                                            </div>

                                            <div class="form-group col-md-2 col-6">
                                                <div class="label-dynamic">
                                                    <label>Date of Payment</label>
                                                </div>
                                                <asp:TextBox ID="txtPaymentdate" TabIndex="6" runat="server" MaxLength="20" CssClass="form-control dob"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Please Enter Date of Payment"
                                                    ControlToValidate="txtPaymentdate" Display="None" ValidationGroup="SubmitChallan"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-md-2 col-6">
                                                <div class="label-dynamic">
                                                    <label>
                                                        Upload Deposit Slip
                                                    </label>
                                                </div>
                                                <asp:FileUpload ID="FuChallan" runat="server" onchange="setUploadButtonState();" Style="margin-top: 8px;" TabIndex="7" /><br />

                                            </div>
                                        </div>

                                        <div class="col-12 mt-2 text-center">
                                            <asp:Button ID="btnChallanSubmit" runat="server" TabIndex="8" Text="Submit" CssClass="btn btn-outline-info" ValidationGroup="SubmitChallan" OnClick="btnChallanSubmit_Click" />
                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="SubmitChallan" />
                                        </div>
                                        <asp:TextBox ID="txtOrderid" runat="server" Visible="false"></asp:TextBox>

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
                                                                <asp:UpdatePanel ID="updEdit" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/IMAGES/edit.png" CommandArgument='<%# Eval("TEMP_DCR_NO") %>'
                                                                            TabIndex="14" ToolTip="Edit" OnClick="btnEdit_Click" />
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:PostBackTrigger ControlID="btnEdit" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>
                                                            </td>
                                                            <td><%# Eval("BANK_NAME") %></td>
                                                            <td><%# Eval("BRANCH_NAME") %></td>
                                                            <td><%# Eval("AMOUNT") %></td>
                                                            <td><%# Eval("CHALAN_DATE") %></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>

                                            </div>
                                        </div>

                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnChallanSubmit" />
                                        <asp:AsyncPostBackTrigger ControlID="ddlbank" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="DivPromissory" class="modal fade" role="dialog">
                    <div class="modal-dialog modal-xl">
                        <!-- Modal content-->
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                            </div>
                            <div class="modal-body">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
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
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="form-group col-md-4 col-4">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Actual Amount To Be Paid</label>
                                                </div>
                                           
                                                <asp:TextBox ID="txtPromissoryAmount" TabIndex="4" runat="server" MaxLength="10" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Enter Actual Amount To Be Paid"
                                                    ControlToValidate="txtPromissoryAmount" Display="None" ValidationGroup="SubmitPromissory"></asp:RequiredFieldValidator>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtPromissoryAmount" ValidChars="1234567890.-," FilterMode="ValidChars" />
                                            </div>

                                            <div class="form-group col-md-4 col-4">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Promissory Reason</label>
                                                </div>
                                                <textarea class="form-control" runat="server" rows="2" id="txtPromissoryReason" tabindex="0"></textarea>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please Enter Promissory Reason"
                                                    ControlToValidate="txtPromissoryReason" Display="None" ValidationGroup="SubmitPromissory"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-md-4 col-4">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Date of Payment</label>
                                                </div>
                                                <asp:TextBox ID="txtPromissoryDate" TabIndex="6" runat="server" MaxLength="20" CssClass="form-control dob"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Please Enter Date of Payment"
                                                    ControlToValidate="txtPromissoryDate" Display="None" ValidationGroup="SubmitPromissory"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="col-12 mt-2 text-center">
                                            <asp:Button ID="btnSubmitPromissory" runat="server" TabIndex="8" Text="Submit" CssClass="btn btn-outline-info" ValidationGroup="SubmitPromissory" OnClick="btnSubmitPromissory_Click" />
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="SubmitPromissory" />
                                        </div>

                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnSubmitPromissory" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
       
       <script type="text/javascript">
           $(document).ready(function () {
               $("#ctl00_ContentPlaceHolder1_txtEnrollNo").autocomplete({

                   source: function (request, response) {
                       var obj = {};
                       obj.textsearch = request.term;
                       var searchText = request.term;
                       var message = "Hello, Web Service!";

                       if (searchText.length >= 4) {
                           $.ajax({
                               type: "POST",
                               //url: "SearchForm.aspx/GetSuggestions",
                               url: "/WEB API/SearchName.asmx/RemaingPayment",
                               // url: "../WEB API/SearchName.asmx/RemaingPayment",
                               //data: JSON.stringify(obj),
                               data: JSON.stringify(obj),
                               contentType: "application/json; charset=utf-8",
                               dataType: "json",
                               success: function (data) {
                                   response($.map(data.d, function (item) {
                                       return {

                                           label: item['STUDNAME'],
                                           val: item['IDNO']
                                       }
                                   }))

                               },
                               error: function (xhr, status, error) {
                                   console.log("Error:", error);
                               }
                           });
                       }
                   },
                   select: function (e, i) {

                       $("#<%=hdnClientId.ClientID %>").val(i.item.val);
                  },

                  minLength: 1

              });

          });
    </script>
    <script>
        $(document).ready(function () {
            var prev_date = new Date();
            prev_date.setDate(prev_date.getDate());
            $('.dob').daterangepicker({
                singleDatePicker: true,
                showDropdowns: true,
                //maxDate: prev_date,
                locale: {
                    format: 'DD/MM/YYYY'
                },
                autoApply: true,
            });
        });
    </script>
    <script>
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                var prev_date = new Date();
                prev_date.setDate(prev_date.getDate());
                $('.dob').daterangepicker({
                    singleDatePicker: true,
                    showDropdowns: true,
                    //maxDate: prev_date,
                    locale: {
                        format: 'DD/MM/YYYY'
                    },
                    autoApply: true,
                });
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

    <script>
        $(document).ready(function () {
            var table = $('#mytable').DataTable({
                responsive: true,
                lengthChange: true,
                scrollY: 450,
                scrollX: true,
                scrollCollapse: true,
                paging: false,
                dom: 'lBfrtip',
                buttons: [
                    {
                        extend: 'colvis',
                        text: 'Column Visibility',
                        columns: function (idx, data, node) {
                            var arr = [];
                            if (arr.indexOf(idx) !== -1) {
                                return false;
                            } else {
                                return $('#mytable').DataTable().column(idx).visible();
                            }
                        }
                    },
                    {
                        extend: 'collection',
                        text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                        buttons: [
                                {
                                    extend: 'copyHtml5',
                                    exportOptions: {
                                        columns: function (idx, data, node) {
                                            var arr = [];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('#mytable').DataTable().column(idx).visible();
                                            }
                                        },
                                        format: {
                                            body: function (data, column, row, node) {
                                                var nodereturn;
                                                if ($(node).find("input:text").length > 0) {
                                                    nodereturn = "";
                                                    nodereturn += $(node).find("input:text").eq(0).val();
                                                }
                                                else if ($(node).find("input:checkbox").length > 0) {
                                                    nodereturn = "";
                                                    $(node).find("input:checkbox").each(function () {
                                                        if ($(this).is(':checked')) {
                                                            nodereturn += "On";
                                                        } else {
                                                            nodereturn += "Off";
                                                        }
                                                    });
                                                }
                                                else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                    nodereturn = "";
                                                    $(node).find("span").each(function () {
                                                        nodereturn += $(this).html();
                                                    });
                                                }
                                                else if ($(node).find("select").length > 0) {
                                                    nodereturn = "";
                                                    $(node).find("select").each(function () {
                                                        var thisOption = $(this).find("option:selected").text();
                                                        if (thisOption !== "Please Select") {
                                                            nodereturn += thisOption;
                                                        }
                                                    });
                                                }
                                                else {
                                                    nodereturn = data;
                                                }
                                                return nodereturn;
                                            },
                                        },
                                    }
                                },
                                {
                                    extend: 'excelHtml5',
                                    exportOptions: {
                                        columns: function (idx, data, node) {
                                            var arr = [];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('#mytable').DataTable().column(idx).visible();
                                            }
                                        },
                                        format: {
                                            body: function (data, column, row, node) {
                                                var nodereturn;
                                                if ($(node).find("input:text").length > 0) {
                                                    nodereturn = "";
                                                    nodereturn += $(node).find("input:text").eq(0).val();
                                                }
                                                else if ($(node).find("input:checkbox").length > 0) {
                                                    nodereturn = "";
                                                    $(node).find("input:checkbox").each(function () {
                                                        if ($(this).is(':checked')) {
                                                            nodereturn += "On";
                                                        } else {
                                                            nodereturn += "Off";
                                                        }
                                                    });
                                                }
                                                else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                    nodereturn = "";
                                                    $(node).find("span").each(function () {
                                                        nodereturn += $(this).html();
                                                    });
                                                }
                                                else if ($(node).find("select").length > 0) {
                                                    nodereturn = "";
                                                    $(node).find("select").each(function () {
                                                        var thisOption = $(this).find("option:selected").text();
                                                        if (thisOption !== "Please Select") {
                                                            nodereturn += thisOption;
                                                        }
                                                    });
                                                }
                                                else {
                                                    nodereturn = data;
                                                }
                                                return nodereturn;
                                            },
                                        },
                                    }
                                },
                                {
                                    extend: 'pdfHtml5',
                                    exportOptions: {
                                        columns: function (idx, data, node) {
                                            var arr = [];
                                            if (arr.indexOf(idx) !== -1) {
                                                return false;
                                            } else {
                                                return $('#mytable').DataTable().column(idx).visible();
                                            }
                                        },
                                        format: {
                                            body: function (data, column, row, node) {
                                                var nodereturn;
                                                if ($(node).find("input:text").length > 0) {
                                                    nodereturn = "";
                                                    nodereturn += $(node).find("input:text").eq(0).val();
                                                }
                                                else if ($(node).find("input:checkbox").length > 0) {
                                                    nodereturn = "";
                                                    $(node).find("input:checkbox").each(function () {
                                                        if ($(this).is(':checked')) {
                                                            nodereturn += "On";
                                                        } else {
                                                            nodereturn += "Off";
                                                        }
                                                    });
                                                }
                                                else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                    nodereturn = "";
                                                    $(node).find("span").each(function () {
                                                        nodereturn += $(this).html();
                                                    });
                                                }
                                                else if ($(node).find("select").length > 0) {
                                                    nodereturn = "";
                                                    $(node).find("select").each(function () {
                                                        var thisOption = $(this).find("option:selected").text();
                                                        if (thisOption !== "Please Select") {
                                                            nodereturn += thisOption;
                                                        }
                                                    });
                                                }
                                                else {
                                                    nodereturn = data;
                                                }
                                                return nodereturn;
                                            },
                                        },
                                    }
                                },
                        ]
                    }
                ],
                "bDestroy": true,
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                var table = $('#mytable').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 450,
                    scrollX: true,
                    scrollCollapse: true,
                    paging: false,
                    dom: 'lBfrtip',
                    buttons: [
                        {
                            extend: 'colvis',
                            text: 'Column Visibility',
                            columns: function (idx, data, node) {
                                var arr = [];
                                if (arr.indexOf(idx) !== -1) {
                                    return false;
                                } else {
                                    return $('#mytable').DataTable().column(idx).visible();
                                }
                            }
                        },
                        {
                            extend: 'collection',
                            text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                            buttons: [
                                    {
                                        extend: 'copyHtml5',
                                        exportOptions: {
                                            columns: function (idx, data, node) {
                                                var arr = [];
                                                if (arr.indexOf(idx) !== -1) {
                                                    return false;
                                                } else {
                                                    return $('#mytable').DataTable().column(idx).visible();
                                                }
                                            },
                                            format: {
                                                body: function (data, column, row, node) {
                                                    var nodereturn;
                                                    if ($(node).find("input:text").length > 0) {
                                                        nodereturn = "";
                                                        nodereturn += $(node).find("input:text").eq(0).val();
                                                    }
                                                    else if ($(node).find("input:checkbox").length > 0) {
                                                        nodereturn = "";
                                                        $(node).find("input:checkbox").each(function () {
                                                            if ($(this).is(':checked')) {
                                                                nodereturn += "On";
                                                            } else {
                                                                nodereturn += "Off";
                                                            }
                                                        });
                                                    }
                                                    else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                        nodereturn = "";
                                                        $(node).find("span").each(function () {
                                                            nodereturn += $(this).html();
                                                        });
                                                    }
                                                    else if ($(node).find("select").length > 0) {
                                                        nodereturn = "";
                                                        $(node).find("select").each(function () {
                                                            var thisOption = $(this).find("option:selected").text();
                                                            if (thisOption !== "Please Select") {
                                                                nodereturn += thisOption;
                                                            }
                                                        });
                                                    }
                                                    else {
                                                        nodereturn = data;
                                                    }
                                                    return nodereturn;
                                                },
                                            },
                                        }
                                    },
                                    {
                                        extend: 'excelHtml5',
                                        exportOptions: {
                                            columns: function (idx, data, node) {
                                                var arr = [];
                                                if (arr.indexOf(idx) !== -1) {
                                                    return false;
                                                } else {
                                                    return $('#mytable').DataTable().column(idx).visible();
                                                }
                                            },
                                            format: {
                                                body: function (data, column, row, node) {
                                                    var nodereturn;
                                                    if ($(node).find("input:text").length > 0) {
                                                        nodereturn = "";
                                                        nodereturn += $(node).find("input:text").eq(0).val();
                                                    }
                                                    else if ($(node).find("input:checkbox").length > 0) {
                                                        nodereturn = "";
                                                        $(node).find("input:checkbox").each(function () {
                                                            if ($(this).is(':checked')) {
                                                                nodereturn += "On";
                                                            } else {
                                                                nodereturn += "Off";
                                                            }
                                                        });
                                                    }
                                                    else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                        nodereturn = "";
                                                        $(node).find("span").each(function () {
                                                            nodereturn += $(this).html();
                                                        });
                                                    }
                                                    else if ($(node).find("select").length > 0) {
                                                        nodereturn = "";
                                                        $(node).find("select").each(function () {
                                                            var thisOption = $(this).find("option:selected").text();
                                                            if (thisOption !== "Please Select") {
                                                                nodereturn += thisOption;
                                                            }
                                                        });
                                                    }
                                                    else {
                                                        nodereturn = data;
                                                    }
                                                    return nodereturn;
                                                },
                                            },
                                        }
                                    },
                                    {
                                        extend: 'pdfHtml5',
                                        exportOptions: {
                                            columns: function (idx, data, node) {
                                                var arr = [];
                                                if (arr.indexOf(idx) !== -1) {
                                                    return false;
                                                } else {
                                                    return $('#mytable').DataTable().column(idx).visible();
                                                }
                                            },
                                            format: {
                                                body: function (data, column, row, node) {
                                                    var nodereturn;
                                                    if ($(node).find("input:text").length > 0) {
                                                        nodereturn = "";
                                                        nodereturn += $(node).find("input:text").eq(0).val();
                                                    }
                                                    else if ($(node).find("input:checkbox").length > 0) {
                                                        nodereturn = "";
                                                        $(node).find("input:checkbox").each(function () {
                                                            if ($(this).is(':checked')) {
                                                                nodereturn += "On";
                                                            } else {
                                                                nodereturn += "Off";
                                                            }
                                                        });
                                                    }
                                                    else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                        nodereturn = "";
                                                        $(node).find("span").each(function () {
                                                            nodereturn += $(this).html();
                                                        });
                                                    }
                                                    else if ($(node).find("select").length > 0) {
                                                        nodereturn = "";
                                                        $(node).find("select").each(function () {
                                                            var thisOption = $(this).find("option:selected").text();
                                                            if (thisOption !== "Please Select") {
                                                                nodereturn += thisOption;
                                                            }
                                                        });
                                                    }
                                                    else {
                                                        nodereturn = data;
                                                    }
                                                    return nodereturn;
                                                },
                                            },
                                        }
                                    },
                            ]
                        }
                    ],
                    "bDestroy": true,
                });
            });
        });
    </script>
    <script type="text/ecmascript">
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $("#ctl00_ContentPlaceHolder1_txtEnrollNo").autocomplete({

                    source: function (request, response) {
                        var obj = {};
                        obj.textsearch = request.term;
                        var searchText = request.term;
                        var message = "Hello, Web Service!";

                        if (searchText.length >= 4) {
                            $.ajax({
                                type: "POST",
                                //url: "SearchForm.aspx/GetSuggestions",
                                url: "/WEB API/SearchName.asmx/RemaingPayment",
                                // url: "../WEB API/SearchName.asmx/RemaingPayment",
                                //data: JSON.stringify(obj),
                                data: JSON.stringify(obj),
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (data) {
                                    response($.map(data.d, function (item) {
                                        return {

                                            label: item['STUDNAME'],
                                            val: item['IDNO']
                                        }
                                    }))

                                },
                                error: function (xhr, status, error) {
                                    console.log("Error:", error);
                                }
                            });
                        }
                    },
                    select: function (e, i) {

                        $("#<%=hdnClientId.ClientID %>").val(i.item.val);
                   },

                   minLength: 1

                });

             });

        });
    </script>
</asp:Content>

