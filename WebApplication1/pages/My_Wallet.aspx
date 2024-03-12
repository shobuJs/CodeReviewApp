<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="My_Wallet.aspx.cs" Inherits="Projects_My_Wallet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        #ctl00_ContentPlaceHolder1_Panel2 .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <style>
        .wallet-sub-amount {
            position: relative;
            background: #fff;
            padding: 15px;
            box-shadow: 0 0 5px #ccc;
            border-radius: 8px;
        }

            .wallet-sub-amount img {
                position: absolute;
                top: 34%;
                left: 65%;
                width: auto;
                height: 60px;
            }

        .wallet-amount {
            position: relative;
            background: #fff;
            padding: 15px;
            box-shadow: 0 0 5px #ccc;
            border-radius: 8px;
        }

            .wallet-amount img {
                position: absolute;
                top: 34%;
                right: 8%;
                width: auto;
                height: 60px;
            }

        .badge {
            display: inline-block;
            padding: 5px 10px 7px;
            border-radius: 15px;
            font-size: 100%;
            width: 80px;
        }
        /*#ctl00_ContentPlaceHolder1_lvwithap_ctrl0_labelstatus {
            display: inline-block;
            padding: 5px 10px 7px;
            border-radius: 15px;
            font-size: 100%;
            width: 80px;
        }
        #ctl00_ContentPlaceHolder1_lvwithap_ctrl0_labelstatus {
            color: #fff;
            background-color: #28a745;
        }*/
        .badge-warning {
            color: #fff !important;
        }

        @media (max-width: 991px) {
            .wallet-amount img {
                right: 20%;
            }
        }
    </style>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">My Wallet</h3>
                    <%--<h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label>
                    </h3>--%>
                </div>
                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="wallet-sub-amount">
                                    <div class="label-dynamic">
                                        <label style="font-size: 16px; color: #9a9a9a;">Payable</label>
                                    </div>
                                    <asp:Label ID="lblpayble" runat="server" Style="font-size: 30px; font-weight: 600;">0</asp:Label>
                                    <%--<label id="lblpayble" class="" style="font-size: 30px; font-weight: 600;">0</label>--%>
                                    <%--<label class="" style="font-size: 30px; font-weight: 600;">3000</label>--%>
                                    <asp:Image ID="img1" runat="server" ImageUrl="~/IMAGES/srilanka_currency.png" />
                                    <%--<img src="../IMAGES/srilanka_currency.png" />--%>
                                </div>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="wallet-sub-amount">
                                    <div class="label-dynamic">

                                        <label style="font-size: 16px; color: #9a9a9a;">Excess Amount</label>
                                    </div>
                                    <asp:Label ID="lblexcamt" runat="server" Style="font-size: 30px; font-weight: 600;">0</asp:Label><br />
                                    <asp:Button ID="btnexwith" runat="server"  Text="Withdrawal" CssClass="btn btn-outline-info" OnClick="btnexwith_Click" Enabled="false" />
                                    <%--<label id="lblexcamt" class="" style="font-size: 30px; font-weight: 600;">0</label>--%>
                                    <%--<label class="" style="font-size: 30px; font-weight: 600;">3000</label>--%>
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/IMAGES/srilanka_currency.png" />
                                </div>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="wallet-amount">
                                    <div class="label-dynamic">
                                        <label style="font-size: 16px; color: #9a9a9a;">Balance Scholarship</label>
                                    </div>
                                    <asp:Label ID="lblbalanceschol" runat="server" Style="font-size: 30px; font-weight: 600;">0</asp:Label>
                                    <%--<label id="lblbalanceschol" class="" style="font-size: 30px; font-weight: 600;">0</label>--%>
                                    <%--<label class="" style="font-size: 30px; font-weight: 600;">3000</label>--%>
                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/IMAGES/srilanka_currency.png" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-12" id="divViewpayment" runat="server" visible="false">
                        <asp:UpdatePanel ID="up" runat="server">
                            <ContentTemplate>
                                <asp:Panel ID="pnlStudentsFees" runat="server" Visible="false">
                                    <asp:ListView ID="lvStudentFees" runat="server" OnItemDataBound="lvStudentFees_ItemDataBound" OnPreRender="lvStudentFees_PreRender">
                                        <LayoutTemplate>
                                            <div>
                                                <div class="sub-heading">
                                                    <h5>Fees Details</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tblHead">
                                                    <thead class="bg-light-blue">
                                                        <tr id="trRow">
                                                            <th>Sr No.
                                                            </th>
                                                            <th>Fees Head
                                                            </th>
                                                            <th>Amount
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>

                                                    <thead class="bg-light-blue">
                                                        <tr id="Tr1" runat="server">
                                                            <th></th>
                                                            <th><span class="pull-right">Total Amount</span></th>

                                                            <th id="Td1" runat="server">
                                                                <asp:Label ID="lbltotal" CssClass="data_label" runat="server" Text="0"></asp:Label>
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                </table>

                                            </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%# Container.DisplayIndex + 1 %>
                                                </td>
                                                <td><%# Eval("YEAR") %> - <%# Eval("FEE_LONGNAME") %>
                                                    <asp:HiddenField ID="hdfFieldName" runat="server" Value='<%# Eval("FEE_LONGNAME") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("AMOUNT") %>'></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>

                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-md-12" id="divwithapti" runat="server" visible="false">
                        <asp:Panel ID="Panel2" runat="server">
                            <asp:ListView ID="lvwithap" runat="server">
                                <LayoutTemplate>
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="myTable">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>Sr.No.</th>
                                                <th>Request ID</th>
                                                <th>Refund For</th>
                                                <th>Approval Date</th>
                                                <th>Refundable Amount</th>
                                                <th>Non-Refundable Amount</th>
                                                <th>Apply Refund</th>
                                                <th>Status</th>
                                                <th>Transaction Details</th>
                                                <th>Remark</th>
                                              <%--  <th>Remark By Director</th>
                                                <th>Remark By Finance</th>--%>

                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr id="itemPlaceholder" runat="server" />
                                        </tbody>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td style="text-align: center"><%# Container.DataItemIndex + 1 %>
                                        </td>
                                        <td><%# Eval("SRNO") %></td>
                                        <td>
                                            <%# Eval("STATUS")%> 
                                        </td>
                                        <td>
                                            <%# Eval("APPROVAL_DATE")%> 
                                        </td>
                                        <td>
                                            <asp:Label ID="lblrefund" runat="server" Text='<%# Eval("REFUND_AMOUNT")%> '></asp:Label>
                                            <asp:Label ID="lblrefundwithstatus" runat="server" Text='<%# Eval("REFUND_WIHDRWAL_STATUS")%> ' Visible="false"></asp:Label>
                                            
                                        </td>
                                        <td>
                                            <%# Eval("NONREFUND_AMOUNT")%> 
                                        </td>
                                        <td style="text-align: center">
                                            <%--<asp:LinkButton ID="lnkViewDoc" runat="server" CssClass="btn btn-outline-info" CommandArgument='<%#Eval("IDNO") %>' CommandName='<%#Eval("DOCUMENT") %>' OnClick="lnkViewDoc_Click"><i class="fa fa-eye"></i> View</asp:LinkButton>--%>
                                            <asp:LinkButton ID="lnkViewDoc" class="btn btn-outline-info" runat="server" CommandArgument='<%#Eval("IDNO") %>' CommandName='<%# Eval("SRNO") %>' ToolTip='<%# Eval("STATUS_NO")%>' OnClick="lnkViewDoc_Click">Apply Refund</asp:LinkButton>
                                            <asp:Label ID="refundstatus" runat="server" Visible="false" Text="(Refund Not Applicable)"></asp:Label>
                                        </td>
                                        <td class="text-center">
                                            <asp:Label ID="labelstatus" runat="server" Visible="false" CssClass="badge badge-success"><%#Eval("ADMIN_APPROVAL") %></asp:Label></td>
                                        <asp:HiddenField ID="hdfsts" runat="server" Value='<%#Eval("STUDNAMEBANK") %>' />
                                        <asp:HiddenField ID="lblbtnena" runat="server" Value='<%#Eval("ADMIN_APPROVAL") %>' />
                                        <asp:HiddenField ID="hdfreqtype" runat="server" Value='<%#Eval("STATUS") %>' />
                                        <asp:HiddenField ID="hdfdocview" runat="server" Value='<%#Eval("UPLOAD_TRANSACTION_DETAIL") %>' />
                                        <td>
                                            <asp:LinkButton ID="lnkfinancedoc" runat="server" CssClass="btn btn-outline-info" CommandArgument='<%#Eval("SRNO") %>' CommandName='<%#Eval("UPLOAD_TRANSACTION_DETAIL") %>' OnClick="lnkfinancedoc_Click"><i class="fa fa-eye"></i> View</asp:LinkButton>
                                        </td>
                                        <td>
                                            <%# Eval("STUDENT_REMARK_BY_O_M_D_F")%> 
                                        </td>
                                        <%--<td>
                                            <%# Eval("REMARK_BY_DIRECTOR")%> 
                                        </td>
                                        <td>
                                            <%# Eval("FINANCE_REMARK")%> 
                                        </td>--%>

                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>
                    </div>
                </div>
                <div id="myModal22" class="modal fade" role="dialog">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content" style="margin-top: -25px">
                            <div class="modal-body">
                                <div class="modal-header">
                                    <%--<button type="button" class="close" data-dismiss="modal" style="margin-top: -18px"  >x</button>--%>
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                </div>
                                <asp:Image ID="ImageViewer" runat="server" Width="100%" Height="500px" Visible="false" />
                                <asp:Literal ID="ltEmbed" runat="server" />
                                <%--<iframe id="iframe1" runat="server" src="../FILEUPLOAD/Transcript Report.pdf" frameborder="0" width="100%" height="547px"></iframe>--%>

                                <%--<div class="modal-footer" style="height: 0px">
                                                <button type="button" style="margin-top: -10px" class="btn btn-default" data-dismiss="modal">Close</button>
                                            </div>--%>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- The Modal ADD Money-->
                <asp:UpdatePanel ID="updmodal" runat="server">
                    <ContentTemplate>
                        <div class="modal fade" id="apply_refund">
                            <div class="modal-dialog modal-lg">
                                <div class="modal-content">

                                    <!-- Modal Header -->
                                    <div class="modal-header">
                                        <h4 class="modal-title">Refund</h4>
                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    </div>

                                    <!-- Modal body -->
                                    <div class="modal-body">
                                        <div class="row">
                                            <div class="col-lg-6 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Refundable Amount :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="refundamount" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                        <asp:HiddenField ID="hdfSrnoWithDrwal" runat="server" />
                                                        <asp:HiddenField ID="hdfIdnoWithDrwal" runat="server" />
                                                    </li>
                                                </ul>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-12 pl-0">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Non-Refundable Amount :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="nonrefundamount" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                        <div class="row mt-3">
                                            <div class="form-group col-12 col-md-6">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Name of the Bank</label>
                                                </div>
                                                <asp:TextBox ID="txtBankName" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ErrorMessage="Please Enter Name of the Bank"
                                                    ControlToValidate="txtBankName" Display="None" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-12 col-md-6">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Account Holder Name</label>
                                                </div>
                                                <asp:TextBox ID="txtAccHolderName" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Enter Account Holder Name"
                                                    ControlToValidate="txtAccHolderName" Display="None" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-12 col-md-6">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Account Number</label>
                                                </div>
                                                <asp:TextBox ID="txtAccNumber" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Enter Account Number"
                                                    ControlToValidate="txtAccNumber" Display="None" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-12 col-md-6">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Bank Branch</label>
                                                </div>
                                                <asp:TextBox ID="txtBranch" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Enter Bank Branch"
                                                    ControlToValidate="txtBranch" Display="None" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-12 col-md-6">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Branch Code</label>
                                                </div>
                                                <asp:TextBox ID="txtBranchCode" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please Enter Branch Code"
                                                    ControlToValidate="txtBranchCode" Display="None" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-12 btn-footer">
                                            <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn btn-outline-info" ValidationGroup="submit" OnClick="btnSubmit_Click">Submit</asp:LinkButton>
                                            <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-outline-danger">Cancel</asp:LinkButton>
                                            <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="submit" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnSubmit" />
                        <asp:PostBackTrigger ControlID="btnCancel" />
                    </Triggers>
                </asp:UpdatePanel>
                <!-- excess withdrwal modal  -->
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="modal fade" id="Withdrawl">
                            <div class="modal-dialog">
                                <div class="modal-content">

                                    <!-- Modal Header -->
                                    <div class="modal-header">
                                        <h4 class="modal-title">Excess Withdrawal Details</h4>
                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    </div>

                                    <!-- Modal body -->
                                    <div class="modal-body">
                                        <div class="row mt-3">
                                            <div class="form-group col-12">
                                                <div class="label-dynamic">
                                                    <label>Excess Amount</label>
                                                </div>
                                                <asp:Label ID="lblexcessamt" runat="server">0</asp:Label>
                                            </div>
                                            <div class="form-group col-12 col-md-6" >
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Amount</label>
                                                </div>
                                                <asp:TextBox ID="txtamt" runat="server" MaxLength="8" onkeypress="return NumberOnly(event);" onblur="UpdateTotalAndBalance();" class="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Please Enter Amount"
                                                 ControlToValidate="txtamt" Display="None" ValidationGroup="submitexce"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-12 col-md-6">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Name of the Bank</label>
                                                </div>
                                                <asp:TextBox ID="Txtbanknameex" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please Enter Name of the Bank"
                                                    ControlToValidate="Txtbanknameex" Display="None" ValidationGroup="submitexce"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-12 col-md-6">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Account Holder Name</label>
                                                </div>
                                                <asp:TextBox ID="TxtStudNameex" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Please Enter Account Holder Name"
                                                    ControlToValidate="TxtStudNameex" Display="None" ValidationGroup="submitexce"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-12 col-md-6">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Account Number</label>
                                                </div>
                                                <asp:TextBox ID="TxtAccNuex" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Please Enter Account Number"
                                                    ControlToValidate="TxtAccNuex" Display="None" ValidationGroup="submitexce"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-12 col-md-6">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Bank Branch</label>
                                                </div>
                                                <asp:TextBox ID="Txtbranchex" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Please Enter Bank Branch"
                                                    ControlToValidate="Txtbranchex" Display="None" ValidationGroup="submitexce"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-12 col-md-6">
                                                <div class="label-dynamic">
                                                    <sup>*</sup>
                                                    <label>Branch Code</label>
                                                </div>
                                                <asp:TextBox ID="branchcodeex" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Please Enter Branch Code"
                                                    ControlToValidate="branchcodeex" Display="None" ValidationGroup="submitexce"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Purpose</label>
                                                </div>
                                                <asp:TextBox ID="txtpurpose" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                               <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="Please Enter Purpose"
                                                    ControlToValidate="txtpurpose" Display="None" ValidationGroup="submitexce"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnsubmitexce" runat="server" class="btn btn-outline-info" Text="Submit Request" OnClick="btnsubmitexce_Click" ValidationGroup="submitexce"  />
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="submitexce" />

                                    </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnsubmitexce" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <script>
        function NumberOnly(evt) {
            if (evt.charCode > 31 && (evt.charCode < 48 || evt.charCode > 57)) {
                //alert("Enter Only Numeric Value ");
                return false;
            }
        }
    </script>
    <script type="text/javascript">
        function UpdateTotalAndBalance() {
            //alert("hii")
            var Amount = 0;
            var excessamt = 0;
            Amount = document.getElementById('ctl00_ContentPlaceHolder1_txtamt').value;
            //alert("hii")
            excessamt = document.getElementById('ctl00_ContentPlaceHolder1_lblexcessamt').innerHTML;

            if (parseFloat(Amount) > parseFloat(excessamt)) {
                alert('Entered Amount should not be greater then Excess Amount');
                document.getElementById('ctl00_ContentPlaceHolder1_txtamt').value = "";
                return false;
            }


        }
    </script>
</asp:Content>

