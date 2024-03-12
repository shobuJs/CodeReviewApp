<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage2.master" AutoEventWireup="true" CodeFile="Banktype.aspx.cs" Inherits="ACADEMIC_Banktype" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <div>

        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updsetting"
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

 
    <asp:UpdatePanel ID="updsetting" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                             <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                  
                                    <div id="Div1" class="form-group col-lg-3 col-md-6 col-12" runat="server" >
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Bank Type</label>
                                        </div>
                                         <asp:DropDownList ID="ddlbanktype" runat="server" TabIndex="1" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                             <asp:ListItem Value="0">Please Select </asp:ListItem>
                                             <asp:ListItem Value="1">Bank Account </asp:ListItem>
                                             <asp:ListItem Value="2">G/L Account </asp:ListItem>
                                         </asp:DropDownList>                                                                                                                   
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlbanktype"
                                            ValidationGroup="Submit" Display="None" ErrorMessage="Please Select Bank Type."
                                            InitialValue="0" />
                                    </div>
                                        
                                    <div id="Div2" class="form-group col-lg-3 col-md-6 col-12" runat="server" >
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Bank Code</label>
                                        </div>
                                        <asp:TextBox ID="txtbankCode" runat="server" CssClass="form-control" type="text" ClientIDMode="Static" TabIndex="2"/>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtbankCode"
                                            ValidationGroup="Submit" Display="None" ErrorMessage="Please Select Bank Code."
                                             />
                                    </div>
                                        
                                    <div id="Div3" class="form-group col-lg-3 col-md-6 col-12" runat="server" >
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Bank Name</label>
                                        </div>
                                        <asp:TextBox ID="txtBankNAme" runat="server" CssClass="form-control" type="text" ClientIDMode="Static" TabIndex="3" />
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtBankNAme"
                                            ValidationGroup="Submit" Display="None" ErrorMessage="Please Select Bank Name."
                                             />
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                              
                                <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn btn-outline-info" ValidationGroup="Submit" TabIndex="4"  OnClick="btnSubmit_Click">Submit</asp:LinkButton>
                                <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-outline-danger" TabIndex="5"  OnClick="btnCancel_Click">Cancel</asp:LinkButton>
                                  <asp:ValidationSummary ID="validationsummary2" runat="server" EnableTheming="true"
                                        ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />
                            </div>
                              <div class="col-12">
                                <asp:Panel ID="Panel2" runat="server">
                                    <asp:ListView ID="lvBankDetails" runat="server" Visible="true">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                               
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Action</th> 
                                                         <th>Bank Name</th>                                                      
                                                        <th>Bank Code</th>                                                      
                                                         <th>Bank Type</th>
                                                      
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
                                                    <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/images/edit.gif"
                                                      AlternateText="Edit Record"  OnClick="btnEdit_Click" CommandArgument='<%# Eval("BANK_NO") %>' />
                                                </td>                                                                                              
                                                 <td><%# Eval("BANK_NAME") %></td>
                                                  <td><%# Eval("BANK_CODE") %></td>
                                                <td><%# Eval("BANKTYPE_NAME")%></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>

                                </asp:Panel>

                             
  </ContentTemplate>
        </asp:UpdatePanel> 
</asp:Content>