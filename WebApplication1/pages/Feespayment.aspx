<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Feespayment.aspx.cs" Inherits="ACADEMIC_Feespayment" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:Panel ID="pnlTransferCredit" runat="server">
        <div class="col-md-12" id="fees" runat="server">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">Fees Payment</h3>                   
                </div>

                <div class="box-body">


                    <div class="form-group col-md-6">
                        <ul class="list-group list-group-unbordered" hidden="hidden">                           

                            <li class="list-group-item">
                                <label>Branch Name :</label><a class="pull-right">
                                    <asp:Label ID="lblbranch" runat="server" Text="" Font-Bold="false"></asp:Label></a></li>

                            <li class="list-group-item">
                                <label>Semester</label>

                                <asp:DropDownList ID="ddlsem" runat="server" AppendDataBoundItems="true" CssClass="form-control" OnSelectedIndexChanged="ddlsem_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlsem"
                                    Display="None" ErrorMessage="Please Select Semester" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="Show"></asp:RequiredFieldValidator></li>

                            <li class="list-group-item">
                                <b>
                                    <label>Session</label></b>
                                <a class="pull-right">
                                    <asp:Label ID="Label1" runat="server" Text="" Font-Bold="true"></asp:Label>
                                </a>

                                <%-- <asp:DropDownList ID="ddlsession" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlsession_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlsession"
                                    Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="Show"></asp:RequiredFieldValidator>--%>

                            </li>
                            <li class="list-group-item">
                                <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text="" Font-Bold="true"></asp:Label>
                            </li>

                        </ul>
                        <ul class="list-group list-group-unbordered">
                           <%-- <li class="list-group-item" hidden="hidden">
                                <label>Name :</label><a class="pull-right">
                                    <asp:Label ID="lblname" runat="server" Text="" Font-Bold="false"></asp:Label></a>
                            </li>--%>
                            <li class="list-group-item">
                                <label>Admission No :</label><a class="pull-right">
                                    <asp:Label ID="lblapp" runat="server" Text="" Font-Bold="false"></asp:Label></a>
                            </li>
                            <li class="list-group-item">
                                <label>Session :</label>
                                <a class="pull-right">
                                    <asp:Label ID="lblSession" runat="server" Text="" Font-Bold="true"></asp:Label>
                                </a>
                            </li>
                            <li class="list-group-item">
                                <label>Fee Type :</label><a class="pull-right">
                                    <asp:DropDownList ID="ddlReceiptType" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlReceiptType_SelectedIndexChanged" Width="200">
                                    </asp:DropDownList></a>

                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlReceiptType"
                                    Display="None" ErrorMessage="Please Select Fee Type" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="Show"></asp:RequiredFieldValidator>

                                <asp:Label ID="lblOrderID" runat="server" Text="" Font-Bold="true" Visible="false"></asp:Label>
                            </li>
                        </ul>
                    </div>
                    <div class="form-group col-md-6">
                        <ul class="list-group list-group-unbordered" hidden="hidden">
                            <li class="list-group-item">
                                <label>Current Semester :</label><a class="pull-right">
                                    <asp:Label ID="Label2" runat="server" Text="" Font-Bold="false"></asp:Label></a>
                            </li>


                            <li class="list-group-item">
                                <label>Email Id :</label><a class="pull-right">
                                    <asp:Label ID="lblEmail" runat="server" Text="" Font-Bold="false"></asp:Label></a>
                            </li>

                            <li class="list-group-item">
                                <label>Mobile Number :</label><a class="pull-right">
                                    <asp:Label ID="lblmobile" runat="server" Text="" Font-Bold="false"></asp:Label></a></li>

                            <li class="list-group-item">
                                <label>Fee Type :</label>
                                <asp:DropDownList ID="DropDownList1" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlReceiptType_SelectedIndexChanged">
                                </asp:DropDownList>

                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlReceiptType"
                                    Display="None" ErrorMessage="Please Select Fee Type" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="Show"></asp:RequiredFieldValidator>

                                <asp:Label ID="Label3" runat="server" Text="" Font-Bold="true" Visible="false"></asp:Label>
                            </li>
                            <%--<li class="list-group-item">
                                <label>Fee Type :</label>
                                <asp:DropDownList ID="ddlReceiptType" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlReceiptType_SelectedIndexChanged">
                                </asp:DropDownList>

                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlReceiptType"
                                    Display="None" ErrorMessage="Please Select Fee Type" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="Show"></asp:RequiredFieldValidator>

                                <asp:Label ID="lblOrderID" runat="server" Text="" Font-Bold="true" Visible="false"></asp:Label>
                            </li>--%>

                            <li class="list-group-item">
                                <label>Late Fees :</label><a class="pull-right">
                                    <asp:Label ID="lblLateFee" runat="server" Text="0.00" Font-Bold="false"></asp:Label></a></li>
                            <%--<li class="list-group-item">
                                <label>Total Fees :</label><a class="pull-right">
                                    <asp:Label ID="lbltotalfees" runat="server" Text="0.00" Font-Bold="false"></asp:Label></a></li>--%>
                        </ul>
                        <ul class="list-group list-group-unbordered">
                             <li class="list-group-item" hidden="hidden">
                                <label>Name :</label><a class="pull-right">
                                    <asp:Label ID="lblname" runat="server" Text="" Font-Bold="false"></asp:Label></a>
                            </li>                            
                            <li class="list-group-item">
                                <label>Semester/Year :</label><a class="pull-right">
                                    <asp:Label ID="lblSem" runat="server" Text="" Font-Bold="false"></asp:Label>
                                    <asp:Label ID="Label5" runat="server" Text="/" Font-Bold="false"></asp:Label>
                                    <asp:Label ID="lblYear" runat="server" Text="" Font-Bold="false"></asp:Label>
                                  </a>
                            </li>
                           <%-- <li class="list-group-item">--%>
                                <label hidden="hidden" >Total Fees :</label><%--<a class="pull-right">--%>
                                    <asp:Label ID="lbltotalfees" runat="server" Text="0.00" Font-Bold="false" Visible="false"></asp:Label><%--</a></li>--%>

                        </ul>
                    </div>

                    <div class="box-footer col-md-12 text-center" hidden="hidden">
                        <asp:Button ID="btnPAY" runat="server" Text="Pay Now"
                           ValidationGroup="Show" CssClass="btn btn-outline-info" />

                        <asp:Button ID="btnReports" runat="server" Text="Print Receipt" Visible="false"
                             class="buttonStyle ui-corner-all btn btn-success" />

                        <%-- <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                        OnClick="btnCancel_Click" TabIndex="15" CssClass="btn btn-outline-danger" />--%>
                        <%--<asp:Button ID="btnReport" runat="server" Text="Report" CausesValidation="false"
                                        OnClick="btnReport_Click" TabIndex="16" Enabled="false" CssClass="btn btn-outline-info"/>--%>
                        <asp:ValidationSummary ID="valSummery1" DisplayMode="List" runat="server" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="Show" />

                    </div>

                    <div class="box-footer col-md-12">
                        <%--<asp:Panel ID="pnl" runat="server" ScrollBars="Auto">--%>
                        <div style="margin-bottom: 10px;">
                            <span style="background-color: #808080; padding: 5px; color: #fff;font-weight:bold;">Installment Fees </span>
                            <span style="float: right; background-color: #fff; padding: 5px; color: #008d4c;display:none;">
                                <asp:Label ID="lblRecpttype" runat="server"></asp:Label>
                                <asp:Label ID="Label4" runat="server" Visible="false"> : </asp:Label>
                                <asp:Label ID="lblRecpttypeAmount" runat="server"></asp:Label>

                               

                            </span>
                        </div>
                        <p class="text-center">
                            <asp:ListView ID="lvFeesPayment" runat="server">
                                <LayoutTemplate>
                                    <div id="demo-grid">
                                        <table class="table table-hover table-bordered text-center">
                                            <tr class="bg-light-blue">
                                                <th>Installment </th>
                                                <th>Amount </th>
                                                 <th>Late Fee </th>
                                                 <th>Final Amount </th>
                                                <th>Receipt Type </th>
                                                <th>Due Date </th>
                                                <th>Transaction ID</th>
                                                <th>Pay Type</th>
                                                <th>Pay Status </th>
                                                <th>Online Pay</th>                                               
                                                <th>Challan </th>
                                                <th>Receipt</th>
                                            </tr>
                                            <tr id="itemPlaceholder" runat="server" />
                                        </table>
                                    </div>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td><asp:Label ID="lblInstallment" runat="server" Text='<%# Eval("Installment")%>'></asp:Label></td>
                                        <td><asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount")%>'></asp:Label> </td>
                                        <td><asp:Label ID="lblLateFee" runat="server" Text='<%# Eval("TOTAL_LATE_FEE")%>'></asp:Label></td>
                                         <td><asp:Label ID="lblFINAL_TOTAL_AMT" runat="server" Text='<%# Eval("FINAL_TOTAL_AMT")%>'></asp:Label> </td>
                                        <td><asp:Label ID="lblReceiptCode" runat="server" Text='<%# Eval("ReceiptCode")%>'></asp:Label> </td>
                                        <td><%# (Eval("DueDate").ToString() != string.Empty) ? (Eval("DueDate", "{0:dd-MMM-yyyy}")) : Eval("DueDate", "{0:dd-MMM-yyyy}")%></td>
                                        <%--<td><%# Eval("DueDate")%></td>--%>
                                         <td><asp:Label ID="lblOrder_id" runat="server" Text='<%# Eval("ORDER_ID")%>'></asp:Label></td>
                                         <td><asp:Label ID="lblPayType" runat="server" Text='<%# Eval("PAY_TYPE_Mode")%>' ToolTip='<%# Eval("PAY_TYPE")%>'></asp:Label>  
                                             <%--<asp:HiddenField ID="hdftransactionmode" runat="server"  Value='<%# Eval("PAY_TYPE")%>'/>--%>
                                         </td>
                                        <td>
                                            <asp:Label ID="lblReconVal" runat="server" Text='<%# Eval("RECON")%>' ToolTip='<%# Eval("reconval")%>'></asp:Label> 
                                            <%--<asp:HiddenField ID="hfrecon" runat="server"  Value='<%# Eval("reconval")%>'/>--%>
                                        </td>
                                        <td>
                                           <%--  <asp:HiddenField id="hfAmtWithoutLateFee" runat="server" Value='<%# Eval("Amount")%>' />
                                             <asp:HiddenField id="hfLateFee" runat="server" Value='<%# Eval("TOTAL_LATE_FEE")%>' />
                                             <asp:HiddenField id="hfFinalAmt" runat="server" Value='<%# Eval("FINAL_TOTAL_AMT")%>' />--%>

                                            <asp:Button ID="btnOnlinePay" runat="server" Text="Online Pay" class="buttonStyle ui-corner-all btn btn-success" 
                                               OnClientClick="javascript: return fnConfirm();"   OnClick="btnOnlinePay_Click" CommandArgument='<%# Eval("Installment") %>'
                                                 CommandName='<%# Eval("FINAL_TOTAL_AMT") %>'/>
                                        </td>                                       
                                        <td>
                                            
<%--                                            <asp:Button ID="Button1" runat="server" Text="Challan" class="buttonStyle ui-corner-all btn btn-success" OnClick="btnChallan_Click"  CommandArgument='<%# Eval("ORDER_ID") %>'/>--%>
                                            <asp:Button ID="btnChallan" runat="server" Text="Challan" class="buttonStyle ui-corner-all btn btn-success" 
                                                OnClick="btnChallan_Click"  CommandArgument='<%# Eval("Installment") %>' CommandName='<%# Eval("FINAL_TOTAL_AMT") %>'/>
                                        </td>

                                        <td>
                                            <asp:Button ID="btnReceipt" runat="server" Text="Receipt" class="buttonStyle ui-corner-all btn btn-success" 
                                                OnClick="btnReceipt_Click"  CommandArgument='<%# Eval("ORDER_ID") %>' CommandName='<%# Eval("ORDER_ID") %>'/>
                                        </td>
                                        <%--<asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record" CausesValidation="false" CommandArgument='<%# Eval("SESSION_ACTIVITY_NO") %>' ImageUrl="~/images/edit.gif" OnClick="btnEdit_Click" TabIndex="10" ToolTip="Edit Record" />--%>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </p>
                        <%--  </asp:Panel>--%>
                    </div>


                    <div id="divMsg" runat="server">
                    </div>


                      <script lang="javascript" type="text/javascript">
                          function fnConfirm() {
                              if (confirm("Do you really want to make payment?")) {
                                  return true;
                              }
                              else
                                  return false;
                          }

    </script>



                </div>

            </div>

        </div>

    </asp:Panel>


</asp:Content>

