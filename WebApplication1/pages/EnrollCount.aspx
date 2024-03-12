<%@ Page Title="" Language="C#" MasterPageFile="~/BlankMasterPage.master" AutoEventWireup="true" CodeFile="EnrollCount.aspx.cs" Inherits="ACADEMIC_EnrollCount" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        #ctl00_ContentPlaceHolder1_pnllst .dataTables_scrollHeadInner {
            width: max-content !important;
        }     
      hr {
            border-top: 1px solid #edeff1;
            margin: 3rem 0 2rem;
        }
    </style>
    <div>
        <asp:UpdateProgress ID="UpdateProgress5" runat="server" AssociatedUpdatePanelID="updCount"
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
    <asp:UpdatePanel ID="updCount" runat="server">
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
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Intake</label>
                                        </div>
                                        <asp:DropDownList ID="ddlIntake" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                            ControlToValidate="ddlIntake" Display="None" ErrorMessage="Please Select Intake" InitialValue="0" SetFocusOnError="True" ValidationGroup="Show">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:LinkButton ID="btnShow" runat="server" CssClass="btn btn-outline-info" ValidationGroup="Show" TabIndex="2" OnClick="btnShow_Click">Show</asp:LinkButton>
                                <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-outline-danger" TabIndex="3" OnClick="btnCancel_Click">Cancel</asp:LinkButton>
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Show" />
                            </div>
                            <div class="col-12">
                                <asp:Panel ID="pnllst" runat="server" Visible="false">
                                    <asp:ListView ID="LvenrollCount" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Search List</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="myTable">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th style="text-align: center">Sr.No.
                                                        </th>
                                                        <%--application--%>
                                                        <th>Batchname</th>
                                                         <th>Total Applied Count</th>
                                                        <th>0% Enroll Count</th>
                                                        <th>25% Enroll Count Offer Acceptance</th>
                                                        <th>100% Enroll Count</th>
                                                        <th>Document Upload Count</th>
                                                        <th>Mark Entry Count</th>
                                                        <th>Merits Count</th>
                                                        <th>Intake Transfer Count</th>
                                                        <%--enroll--%>
                                                        <th>Completed Payment(offline) Count</th>
                                                        <th>Incompleted Payment(offline) Count</th>
                                                        <th>Completed Payment(online) Count</th>
                                                        <th>Total Enrolled Count</th>
                                                        <th>Program Transfer Count</th>
                                                        <th>Direct Registered Count </th>
                                                        <th>Admission Withdrawal Req Approved</th>
                                                        <th>Postponement Req Approved Count</th>
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
                                                    <%# Container.DataItemIndex + 1 %>
                                                </td>
                                                  <td>
                                                    <%# Eval("BATCHNAME")%> 
                                                </td>
                                                 <td>
                                                    <%# Eval("TOTALAPPLIED")%> 
                                                </td>
                                              
                                                <td>
                                                    <%# Eval("ZEROPER")%>
                                                </td>
                                                <td>
                                                    <%# Eval("TWENTYFIVE")%> 
                                                </td>
                                                  <td>
                                                    <%# Eval("HUNDREAD")%> 
                                                </td>
                                                <td>
                                                    <%# Eval("DOCUPLOAD")%> 
                                                </td>
                                                 <td>
                                                    <%# Eval("MARKENTRY")%> 
                                                </td>
                                                 <td>
                                                    <%# Eval("MERITLIST")%> 
                                                </td>
                                                 <td>
                                                    <%# Eval("INTAKETRANSFER") %>
                                                </td>
                                                 <td>
                                                    <%# Eval("COMPAYOF")%> 
                                                </td>
                                                <td>
                                                    <%# Eval("INCPAYOF")%>                                                
                                                </td>                                            
                                                <td>
                                                    <%# Eval("COMPAYON")%> 
                                                </td>                                              
                                                <td>
                                                    <%# Eval("TOTALENROLLED") %>
                                                </td>
                                                <td>
                                                    <%# Eval("PGMTRANSFER") %>
                                                </td>
                                                 <td>
                                                    <%# Eval("DIRECTADM") %>
                                                </td>
                                                <td>
                                                    <%# Eval("ADMCANCEL") %>
                                                </td>                                                
                                                 <td>
                                                    <%# Eval("POSTPONEMENT") %>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                            <br />
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-md-6">
                                         <div id="DivStatus" style="border: groove;background-color:aliceblue;" runat="server" visible="false">
                                             <div style="margin-left: auto; margin-right: auto; text-align: center; font-weight:500; background-color:antiquewhite;">
                                                <label><span style="text-align: center; font-weight:bold;"></span>Application</label></label>
                                                 <hr />
                                            </div>                                        
                                             <div class="form-group col-md-12">
                                                <label>
                                                    <span style="color: red;"></span>1. Total Applied Count :</label>
                                                <asp:Label ID="lbltotalapp" runat="server"></asp:Label>
                                            </div>
                                            <div class="form-group col-md-12">
                                                <label>
                                                    <span style="color: red;"></span>2. 0% Count :</label>
                                                <asp:Label ID="lblzero" runat="server"></asp:Label>
                                            </div>
                                            <div class="form-group col-md-12">
                                                <label>
                                                    <span style="color: red;"></span>3. 25 % Count Offer Accepted :</label>
                                                <asp:Label ID="lbltwentyfive" runat="server"></asp:Label>
                                            </div>

                                            <div class="form-group col-md-12">
                                                <label>
                                                    <span style="color: red;"></span>4. 100 % Count :</label>
                                                <asp:Label ID="lblhund" runat="server"></asp:Label>
                                            </div>
                                            <div class="form-group col-md-12">
                                                <label>
                                                    <span style="color: red;"></span>5. Document Upload Count:</label>
                                                <asp:Label ID="lbldoc" runat="server"></asp:Label>
                                            </div>
                                            <div class="form-group col-md-12">
                                                <label>
                                                    <span style="color: red;"></span>6. Mark Entry Count :</label>
                                                <asp:Label ID="lblMark" runat="server"></asp:Label>
                                            </div>

                                            <div class="form-group col-md-12">
                                                <label>
                                                    <span style="color: red;"></span>7. Merit Count :</label>
                                                <asp:Label ID="lblmerit" runat="server"></asp:Label>
                                            </div>
                                             <div class="form-group col-md-12">
                                                <label>
                                                    <span style="color: red;"></span>8. Intake Tranfer Count :</label>
                                                <asp:Label ID="lblintaketransfer" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group col-md-6" >
                                        <div id="divenroll" style="border: groove;background-color:aliceblue;" runat="server" visible="false">
                                             <div style="margin-left: auto; margin-right: auto; text-align: center; font-weight:500; background-color:antiquewhite;">
                                                <label><span style="text-align: center;  font-weight:100"></span>Enrollment</label></label>
                                                 <hr style="font-size:x-large"/>
                                            </div>  
                                            <div class="form-group col-md-12">
                                                <label>
                                                    <span style="color: red;"></span>1. Completed Payment(Offline) :</label>
                                                <asp:Label ID="lblCompayof" runat="server"></asp:Label>
                                            </div>
                                            <div class="form-group col-md-12">
                                                <label>
                                                    <span style="color: red;"></span>2. Incomplete Payment(Offline) :</label>
                                                <asp:Label ID="lblincof" runat="server"></asp:Label>
                                            </div>
                                            <div class="form-group col-md-12">
                                                <label>
                                                    <span style="color: red;"></span>3. Completed Payment(Online) :</label>
                                                <asp:Label ID="lblComponn" runat="server"></asp:Label>
                                            </div>
                                            <div class="form-group col-md-12">
                                                <label>
                                                    <span style="color: red;"></span>4. Total Enrolled Count:</label>
                                                <asp:Label ID="lbltotalenroll" runat="server"></asp:Label>
                                            </div>
                                            <div class="form-group col-md-12">
                                                <label>
                                                    <span style="color: red;"></span>5. Program Transfer Count :</label>
                                                <asp:Label ID="lblpgmtransfer" runat="server"></asp:Label>
                                            </div>
                                            <div class="form-group col-md-12">
                                                <label>
                                                    <span style="color: red;"></span>6. Direct Registered Count :</label>
                                                <asp:Label ID="lbldirectreg" runat="server"></asp:Label>
                                            </div>
                                             <div class="form-group col-md-12">
                                                <label>
                                                    <span style="color: red;"></span>7. Admission Withdrawal Req Approved Count:</label>
                                                <asp:Label ID="lbladmcan" runat="server"></asp:Label>
                                            </div>
                                             <div class="form-group col-md-12">
                                                <label>
                                                    <span style="color: red;"></span>8. Postponement Req Approved Count :</label>
                                                <asp:Label ID="lblpost" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

