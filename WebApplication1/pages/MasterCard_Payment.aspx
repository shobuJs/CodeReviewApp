<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="MasterCard_Payment.aspx.cs" Inherits="ACADEMIC_MasterCard_Payment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


    <asp:Panel ID="pnlTransferCredit" runat="server">
        <div class="col-md-12" id="fees" runat="server">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">Fees Payment</h3>                   
                </div>
                <div class="box-tools pull-right">
                                
                 <div style="color: Red; font-weight: bold; margin-top: -30px; margin-right: 1px;">
                                &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                            </div>
                    </div>
                <div class="box-body">


                    <div class="form-group col-md-6" runat="server" visible="false">
                        <ul class="list-group list-group-unbordered" hidden="hidden">                           

                            <li class="list-group-item">
                                <label>Branch Name :</label><a class="pull-right">
                                    <asp:Label ID="lblbranch" runat="server" Text="" Font-Bold="false"></asp:Label></a></li>

                           <%-- <li class="list-group-item">
                                <label>Semester</label>

                                <asp:DropDownList ID="ddlsem" runat="server" AppendDataBoundItems="true" CssClass="form-control" OnSelectedIndexChanged="ddlsem_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlsem"
                                    Display="None" ErrorMessage="Please Select Semester" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="Show"></asp:RequiredFieldValidator></li>--%>

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
                                <label>Reg Number :</label><a class="pull-right">
                                    <asp:Label ID="lblapp" runat="server" Text="" Font-Bold="false"></asp:Label></a>
                            </li>
                            <%-- <li class="list-group-item">
                                <label><span style="color:red">*</span> Session :</label>
                                <a class="pull-right">
                                  <asp:DropDownList ID="ddlsession" runat="server" AppendDataBoundItems="true" AutoPostBack="True" Width="200" OnSelectedIndexChanged="ddlsession_SelectedIndexChanged" CssClass="form-control">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                    </a>
                                <a class="pull-right">
                                    <asp:Label ID="lblSession" runat="server" Text="" Visible="false" Font-Bold="true"></asp:Label>
                                </a>
                                
                                <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlsession"
                                    Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="Show"></asp:RequiredFieldValidator>
                            </li>--%>
                            <%--<li class="list-group-item">
                                <label><span style="color:red">*</span> Fee Type :</label><a class="pull-right">
                                    <asp:DropDownList ID="ddlReceiptType" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlReceiptType_SelectedIndexChanged" Width="200">
                                    </asp:DropDownList></a>

                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlReceiptType"
                                    Display="None" ErrorMessage="Please Select Fee Type" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="Show"></asp:RequiredFieldValidator>

                                <asp:Label ID="lblOrderID" runat="server" Text="" Font-Bold="true" Visible="false"></asp:Label>
                            </li>         --%>                   
                        </ul>
                    </div>
                    <div class="form-group col-md-6" runat="server" visible="false">
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

                          <%--  <li class="list-group-item">
                                <label>Fee Type :</label>
                                <asp:DropDownList ID="DropDownList1" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlReceiptType_SelectedIndexChanged">
                                </asp:DropDownList>

                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlReceiptType"
                                    Display="None" ErrorMessage="Please Select Fee Type" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="Show"></asp:RequiredFieldValidator>

                                <asp:Label ID="Label3" runat="server" Text="" Font-Bold="true" Visible="false"></asp:Label>
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
                                <label>Semester Name :</label><a class="pull-right">
                                    <asp:Label ID="lblSem" runat="server" Text="" Font-Bold="false"></asp:Label></a>
                            </li>
                           
                           <%--<li class="list-group-item">
                                <label >Total Fees :</label>
                                    <asp:Label ID="lbltotalfees" runat="server" Text="0.00" Font-Bold="false" Visible="false"></asp:Label>
                                        </li>--%>

                        </ul>
                    </div>
                    
                                <label >Total Fees :</label>
                                    <asp:Label ID="lbltotalfees" runat="server" Text="500.00" Font-Bold="false" Visible="true"></asp:Label>
                                        
                    <div class="box-footer col-md-12 text-center" >
                        <asp:Button ID="btnPay" runat="server" Text="Pay Now" OnClick="btnPay_Click"
                            ValidationGroup="Show" CssClass="btn btn-outline-info" />

                        <asp:Button ID="btnReports" runat="server" Text="Print Receipt" Visible="false"
                             class="buttonStyle ui-corner-all btn btn-success" />                      
                        <asp:ValidationSummary ID="valSummery1" DisplayMode="List" runat="server" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="Show" />

                    </div>                 


                    <div id="divMsg" runat="server">
                    </div>
                </div>

            </div>

        </div>

    </asp:Panel>


</asp:Content>

