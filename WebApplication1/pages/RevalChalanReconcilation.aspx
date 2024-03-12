<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="RevalChalanReconcilation.aspx.cs" Inherits="Academic_RevalChalanReconcilation"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
    <script type="text/javascript" charset="utf-8">
        $(document).ready(function () {
            $(".display").dataTable({
                "bJQueryUI": true,
                "sPaginationType": "full_numbers"
            });

        });
    </script>

    <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="pnlFeeTable"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>


    <asp:UpdatePanel ID="pnlFeeTable" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">CHALLAN RECONCILATION FOR EXAM</h3>
                        </div>
                        <form class="form-control">
                            <div class="box-body">

                                 <div class="form-group col-md-3">
                                     <span style="color: red">*</span> <label>Session : &nbsp;</label>
                                  <asp:DropDownList ID="ddlSession" runat="server"  AppendDataBoundItems="True" CssClass="form-control">
                                      <asp:ListItem Value="0">Please Select</asp:ListItem>
                                  </asp:DropDownList>
                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSession"
                                        Display="None" ErrorMessage="Please Select Session." SetFocusOnError="true" InitialValue="0"
                                        ValidationGroup="search" />
                                 </div>
                         
                                <div class="form-group col-md-3">
                                     <span style="color: red">*</span> <label>Reval Type : &nbsp;</label>
                                  <asp:DropDownList ID="ddlReceiptType" runat="server"  AppendDataBoundItems="True" CssClass="form-control">
                                      <asp:ListItem Value="0">Please Select</asp:ListItem>
                                  </asp:DropDownList>
                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlReceiptType"
                                        Display="None" ErrorMessage="Please Select Reval Type." SetFocusOnError="true" InitialValue="0"
                                        ValidationGroup="search" />
                                 </div>
                                 <div class="form-group col-md-3">
                                     <span style="color: red">*</span> <label>Univ. Reg. No. / Adm. No. :</label>
                                    <asp:TextBox ID="txtSearchText" runat="server" CssClass="form-control" ToolTip="Enter text to search." />
                                    <asp:RequiredFieldValidator ID="valSearchText" runat="server" ControlToValidate="txtSearchText"
                                        Display="None" ErrorMessage="Please Enter Univ. Reg. No. / Adm. No." SetFocusOnError="true"
                                        ValidationGroup="search" />
                                    <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                        ShowSummary="false" ValidationGroup="search" />
                                 </div>
                               
                                 <div class="form-group col-md-3" style="margin-top: 25px;">
                                     <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                                        ValidationGroup="search" CssClass="btn btn-outline-info" />
                                 </div>

                           </div>
                                
                            </div>
                        </form>
                        <div class="box footer col-md-12">
                            <div class="col-md-12" id="divinfo" runat="server" visible="false">
                                    <div class="col-md-6">
                                        <ul class="list-group list-group-unbordered">
                                             <li class="list-group-item">
                                                <b>Student Name :</b><a class="pull-right">
                                                    <asp:Label ID="lblStudName" CssClass="data_label" runat="server" /></a>
                                            </li>
                                            <li class="list-group-item">
                                                <b>Degree :</b><a class="pull-right">
                                                    <asp:Label ID="lblDegree" CssClass="data_label" runat="server" /></a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-md-6">
                                        <ul class="list-group list-group-unbordered">
                                             <li class="list-group-item">
                                                <b>Branch :</b><a class="pull-right">
                                                    <asp:Label ID="lblBranch" CssClass="data_label" runat="server" /></a>
                                            </li>
                                            <li class="list-group-item">
                                                <b>Current Semester :</b><a class="pull-right">
                                                    <asp:Label ID="lblSemester" CssClass="data_label" runat="server" /></a>
                                            </li>
                                        </ul>
                                    </div>
                                </div>




                               <asp:ListView ID="lvSearchResults" runat="server">
                                <LayoutTemplate>
                                    <div id="listViewGrid">

                                        <h4>Chalan Search Results</h4>

                                        <table id="tblSearchResults" class="table table-bordered table-hover table-fixed">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th>Select
                                                    </th>
                                                   <%-- <th>Challan No.
                                                    </th>--%>
                                                    <th>Challan Date
                                                    </th>
                                                    <th>Receipt Type
                                                    </th>
                                                   <%-- <th id="Sem">Semester
                                                    </th>--%>
                                                    <th>Pay Type
                                                    </th>
                                                    <th>Amount
                                                    </th>
                                                    <th>PAY Status
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                        </table>
                                    </div>
                                </LayoutTemplate>
                                <%-- DO NOT FORMAT FOLLOWING 5-6 LINES. JAVASCRIPT IS DEPENDENT ON ELEMENT HIERARCHY --%>
                                <EmptyDataTemplate>
                                </EmptyDataTemplate>
                                <ItemTemplate>

                                    <tr>
                                        <td >
                                            <label id ="lblreco" runat="server" >
                                            <input id="rdoSelectRecord" value='<%# Eval("IDNO") %>'  name="Receipts" type="radio"                               
                                                onclick="ShowRemark(this);" /></label>
                                            <asp:HiddenField ID="hidRemark" runat="server" Value='<%# Eval("REMARK") %>' />
                                            <asp:HiddenField ID="hidSessionNo" runat="server" Value='<%# Eval("SESSIONNO") %>' />
                                          <%-- <asp:HiddenField ID="hidDcrSemesterNo" runat="server" Value='<%# Eval("SEMESTERNO") %>' />--%>
                                            <asp:HiddenField ID="hidIdNo" runat="server" Value='<%# Eval("IDNO") %>' />
                                            <asp:HiddenField ID="hidRcypt" runat="server" Value='<%# Eval("RECIEPT_CODE") %>' />
                                            <asp:HiddenField ID="hidrecon" runat="server" Value='<%# Eval("RECON") %>' />
                                        </td>
                                       <%-- <td>
                                            <%# Eval("REC_NO") %>
                                        </td>--%>
                                        <td>
                                           <%# Eval("REC_DT") %>
                                        </td>
                                        <td>
                                            <%# Eval("RECIEPT_TITLE") %>
                                        </td>
                                       <%-- <td>
                                            <%# Eval("SEMESTERNAME")%>
                                        </td>--%>
                                        <td>
                                            <%# Eval("PAY_MODE_CODE")%>
                                        </td>
                                        <td>
                                            <%# Eval("TOTAL_AMT")%>
                                        </td>
                                          <td>
                                            <%# Eval("STATUS")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>


                        




                            <div class="col-md-12" id="divRemark" runat="server" visible="false" >
                               <b>Reason For Cancelling Challan :</b> <br />
                                <asp:TextBox ID="txtRemark" Rows="4" TextMode="MultiLine" Width="450px" MaxLength="400"
                                    runat="server" />
                            </div>

                            <div class="form-group col-md-12" id="divChallanButtonDetails" runat="server" visible="false">
                                <p class="text-center" style="margin-top: 12px">

                                    <input id="btnReconcile" onclick="ReconcileChalan();" runat="server" type="button"
                                        value="Reconcile Challan" disabled="disabled" class="btn btn-outline-info" />
                                  
                                      <input id="btnDelete" onclick="DeleteChalan();" runat="server" type="button" value="Cancel Challan"
                                        disabled="disabled" class="btn btn-outline-info" />

                                        <asp:Button ID="btncancel" runat="server" Text="Cancel"
                                    OnClick="btncancel_Click" CssClass="btn btn-outline-danger" />
                                </p>
                            </div>

                            <div id="divMsg" runat="server">
                            </div>

                        </div>


                    </div>
                </div>

            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSearch" />
            <asp:PostBackTrigger ControlID="btnReconcile" />
            <asp:PostBackTrigger ControlID="btnDelete" />
        </Triggers>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">

        function ReconcileChalan() {
            try {
                if (ValidateRecordSelection()) {
                    __doPostBack("ReconcileChalan", "");
                }
                else {
                    alert("Please select a challan to reconcile.");
                }
            }
            catch (e) {
                alert("Error: " + e.description);
            }
        }

        function DeleteChalan() {
            try {
                if (ValidateRecordSelection()) {
                    if (document.getElementById('<%= txtRemark.ClientID %>').value.trim() != "") {
                        if (confirm("If you cancel this challan, it will not be appear in the system.\n\nAre you sure you want to cancel this challan?")) {
                            __doPostBack("DeleteChalan", "");
                        }
                    }
                    else
                        alert('Please enter reason of cancelling challan.');
                }
                else {
                    alert("Please select a challan to cancel.");
                }
            }
            catch (e) {
                alert("Error: " + e.description);
            }
        }

        function ValidateRecordSelection() {
            var check = false;
            var gridView = document.getElementById("tblSearchResults");
            var checkBoxes = gridView.getElementsByTagName("input");
            for (var i = 0; i < checkBoxes.length; i++) {
                if (checkBoxes[i].type == "radio") {
                    if (checkBoxes[i].checked) {
                        check = true;
                    }

                }
            }
            return check;
        }
        function ShowRemark(rdoSelect) {
            try {
                if (rdoSelect != null && rdoSelect.nextSibling != null) {
                    var hidRemark = rdoSelect.nextSibling;
                    if (hidRemark != null)
                        document.getElementById('<%= txtRemark.ClientID %>').value = hidRemark.value;
                }
            }
            catch (e) {
                alert("Error: " + e.description);
            }
        }




    </script>

</asp:Content>
