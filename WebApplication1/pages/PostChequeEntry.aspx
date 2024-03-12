<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="PostChequeEntry.aspx.cs" Inherits="ACADEMIC_PostCheckEntry" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <%-- <script type="text/javascript" charset="utf-8">
         $(document).ready(function () {

             $(".display").dataTable({
                 "bJQueryUI": true,
                 "sPaginationType": "full_numbers"
             });

         });
    </script>

    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>--%>

    <%--<div id="myModal4" class="modal fade" role="dialog">
        <div class="modal-dialog">

            
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Student Search</h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="updEdit" runat="server">
                        <ContentTemplate>

                            <div class="form-group col-md-12">
                                <label>Search By :</label>&nbsp;&nbsp;
                                <asp:RadioButton ID="rdoTan" runat="server" Checked="true" Text="TAN" GroupName="edit" />&nbsp;&nbsp;                               
                                <asp:RadioButton ID="rdoEnrollmentNo" runat="server"  Text="PAN"
                                    GroupName="edit" />&nbsp;&nbsp;
                                 <asp:RadioButton ID="rdoName" runat="server" Text="Name" GroupName="edit" />&nbsp;&nbsp;
                            </div>
                            <div class="form-group col-md-12">
                                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group col-md-3" id="sfsdf" runat="server">
                                <label>Degree</label>
                                <asp:DropDownList ID="ddlDegree" AppendDataBoundItems="true" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" AutoPostBack="true" />
                            </div>
                            <div class="form-group col-md-3">
                                <label>Branch</label>
                                <asp:DropDownList ID="ddlBranch" AppendDataBoundItems="true" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                
                            </div>
                            <div class="form-group col-md-3">
                                <label>Year</label>
                                <asp:DropDownList ID="ddlYear" AppendDataBoundItems="true" runat="server" CssClass="form-control" />
                            </div>
                            <div class="form-group col-md-3">
                                <label>Semester</label>
                                <asp:DropDownList ID="ddlSem" AppendDataBoundItems="true" runat="server" CssClass="form-control" />
                            </div>

                            <div class="form-group col-md-12" style="margin-top: 25px">
                                <p class="text-center">
                                    <input id="btnSearch" type="button" value="Search" onclick="SubmitSearch(this.id);" class="btn btn-outline-info" />
                                    <input id="btnClear" type="button" value="Clear Text" class="btn btn-outline-danger"
                                        onclick="return ClearSearchBox(this.name)" />                                    
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                </p>

                            </div>


                            <div class="form-group col-md-12 table-responsive">
                                <asp:ListView ID="lvStudent" runat="server">
                                    <LayoutTemplate>
                                        <div>
                                            <h4>Search Results</h4>
                                            <table class="table table-bordered table-hover text-center" id="id1">
                                                <thead>
                                                    <tr class="bg-light-blue">
                                                        <th>Name
                                                        </th>
                                                        <th>TAN/PAN
                                                        </th>
                                                        <th>Degree
                                                        </th>
                                                        <th>Branch
                                                        </th>
                                                        <th>Year
                                                        </th>
                                                        <th>Semester
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                        </div>

                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="lnkId" runat="server" Text='<%# Eval("NAME") %>' CommandArgument='<%# Eval("IDNO") %>'
                                                    OnClick="lnkId_Click"></asp:LinkButton>
                                            </td>
                                            <td>
                                                <%# Eval("ENROLLNO")%>
                                            </td>                                            
                                            <td>
                                                <%# Eval("CODE")%>
                                            </td>
                                            <td>
                                                <%# Eval("SHORTNAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("YEARNAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("SEMESTERNAME")%>
                                            </td>
                                        </tr>

                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </ContentTemplate>

                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                </div>
            </div>

         
        </div>
    </div>--%>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title"><span>POST CHEQUE ENTRY</span></h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div id="divStudentSearch" runat="server">
                            <div class="row">
                                <div class="col-lg-6 col-md-12 col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Enter TAN/PAN</label>
                                            </div>
                                            <asp:TextBox ID="txtEnrollNo" runat="server" TabIndex="1" PlaceHolder="Please Enter TAN/PAN" CssClass="form-control" />
                                            <asp:RequiredFieldValidator ID="valEnrollNo" runat="server" ControlToValidate="txtEnrollNo"
                                                Display="None" ErrorMessage="Please Enter TAN/PAN Number" SetFocusOnError="true"
                                                ValidationGroup="studSearch" />
                                        </div>
                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label></label>
                                            </div>
                                            <asp:Button ID="btnShowInfo" runat="server" Text="Search Record" OnClick="btnShowInfo_Click"
                                                TabIndex="3" ValidationGroup="studSearch" CssClass="btn btn-outline-info" />
                                            <asp:ValidationSummary ID="valSummary3" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                ShowSummary="false" ValidationGroup="studSearch" />
                                        </div>
                                    </div>
                                </div>

                                <div class="col-lg-6 col-md-12 col-12" id="divMonthyear" runat="server">
                                    <div class="sub-heading">
                                        <h5>Report</h5>
                                    </div>
                                    <div class="row">
                                        <%-- <div class="form-group col-lg-6 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label> Month/Year</label>
                                            </div>
                                            <div class='input-group date' id='myDatepicker15'>
                                                    <asp:TextBox runat="server" ID="txtSSCYearofPassing" class="form-control" MaxLength="7" data-inputmask="'mask': '99/9999'" placeholder="MM/YYYY"></asp:TextBox>
                                                    <span class="input-group-addon">
                                                        <span class="glyphicon glyphicon-calendar"></span>
                                                    </span>
                                                </div>
                                        </div>--%>
                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Month</label>
                                            </div>
                                            <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="true" CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <label>Year</label>
                                            </div>
                                            <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="col-12 btn-footer">
                                            <asp:Button ID="btnMonthReport" runat="server" Text="Report" OnClick="btnMonthReport_Click" Visible="true" CssClass="btn btn-outline-primary" />
                                            <asp:Button ID="btnClear" runat="server" Text="Cancel" OnClick="btnClear_Click" Visible="true" CssClass="btn btn-outline-danger" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-12" id="divStudInfo" runat="server" style="display: none">
                        <div class="row">
                            <div class="col-lg-4 col-md-6 col-12">
                                <ul class="list-group list-group-unbordered ipad-view">
                                    <li class="list-group-item"><b>TAN :</b>
                                        <a class="sub-label"><asp:Label ID="lblRegNo" CssClass="data_label" runat="server" /> </a>
                                    </li>
                                    <li class="list-group-item"><b>Student's Name :</b>
                                        <a class="sub-label"><asp:Label ID="lblStudName" CssClass="data_label" runat="server" /></a>
                                    </li>
                                    <li class="list-group-item"><b>Degree :</b>
                                        <a class="sub-label"><asp:Label ID="lblDegree" CssClass="data_label" runat="server" /></a>
                                    </li>
                                    <li class="list-group-item"><b>Admission Batch :</b>
                                        <a class="sub-label"><asp:Label ID="lblBatch" CssClass="data_label" runat="server" /></a>
                                    </li>        
                                </ul>
                            </div>

                            <div class="col-lg-4 col-md-6 col-12">
                                <ul class="list-group list-group-unbordered">
                                    <%--<li class="list-group-item"><b>Gender :</b>
                                        <a class="sub-label"><asp:Label ID="lblSex" CssClass="data_label" runat="server" /></a>
                                    </li>--%>
                                    <li class="list-group-item"><b>Student Mobile :</b>
                                        <a class="sub-label"><asp:Label ID="lblStudMobile" CssClass="data_label" runat="server" /></a>
                                    </li>
                                    <li class="list-group-item"><b>PAN :</b>
                                        <a class="sub-label"><asp:Label ID="lblEnrollno" CssClass="data_label" runat="server" /></a>
                                    </li>
                                    <li class="list-group-item"><b>Father's Name :</b>
                                        <a class="sub-label"><asp:Label ID="lblFather" CssClass="data_label" runat="server" /></a>
                                    </li>        
                                </ul>
                            </div>

                            <div class="col-lg-4 col-md-6 col-12">
                                <ul class="list-group list-group-unbordered">
                                    <li class="list-group-item"><b>Branch :</b>
                                        <a class="sub-label"><asp:Label ID="lblBranch" CssClass="data_label" runat="server" /></a>
                                    </li>
                                    <%--<li class="list-group-item"><b>Year :</b>
                                        <a class="sub-label"><asp:Label ID="lblYear" CssClass="data_label" runat="server" /></a>
                                    </li>--%>
                                    <%--<li class="list-group-item"><b>Semester :</b>
                                        <a class="sub-label"><asp:Label ID="lblSemester" CssClass="data_label" runat="server" /></a>
                                    </li>--%>
                                    <li class="list-group-item"><b>Date of Admission :</b>
                                        <a class="sub-label"><asp:Label ID="lblDateOfAdm" CssClass="data_label" runat="server" /></a>
                                    </li> 
                                    <li class="list-group-item"><b>Student Email ID :</b>
                                        <a class="sub-label"><asp:Label ID="lblEmailid" CssClass="data_label" runat="server" /></a>
                                    </li>       
                                </ul>
                            </div>
                        </div>
                    </div>

                    <%--<div class="col-md-12 table table-responsive">
                                        <asp:Panel ID="pnlStudent" runat="server" ScrollBars="auto" Height="400px">
                                            <asp:ListView ID="lvStudents" runat="server">
                                                <LayoutTemplate>
                                                    <div id="demo-grid">
                                                        <h4>Posted Cheque List</h4>
                                                        <table class="table table-hover table-bordered">
                                                            <thead>
                                                                <tr class="bg-light-blue">
                                                                    <th>Cheque
                                                                    </th>
                                                                    <th>Received Date
                                                                    </th>
                                                                    <th>Amount
                                                                    </th>
                                                                    <th>Cheque No
                                                                    </th>
                                                                    <th>Bank Name
                                                                    </th>
                                                                    <th>Next Due Date 
                                                                    </th>
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
                                                            CHEQUE 1                                                      
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtReceivedDate" runat="server" TabIndex="3" CssClass="form-control" />
                                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MM-yyyy"
                                                        TargetControlID="txtReceivedDate" PopupButtonID="imgCalStartDate" Enabled="true"
                                                        EnableViewState="true">
                                                    </ajaxToolKit:CalendarExtender>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control" /> 
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtChequeNo" runat="server" CssClass="form-control" /> 
                                                        </td>
                                                        <td>
                                                               <asp:DropDownList ID="ddlBank" runat="server" AppendDataBoundItems="true" CssClass="form-control"></asp:DropDownList>
                                                        </td>                                                    
                                                        <td>                                                        
                                                             <asp:TextBox ID="txtDob" runat="server" TabIndex="3" CssClass="form-control" />
                                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MM-yyyy"
                                                        TargetControlID="txtDob" PopupButtonID="imgCalStartDate" Enabled="true"
                                                        EnableViewState="true">
                                                    </ajaxToolKit:CalendarExtender>                                                      
                                                        </td>
                                                        </tr>
                                                        <tr>
                                                        <td>
                                                            CHEQUE 2                                                      
                                                        </td>
                                                        </tr>
                                                        <tr>
                                                        <td>
                                                            CHEQUE 3                                                      
                                                        </td>
                                                        </tr>
                                                        <tr>
                                                        <td>
                                                            CHEQUE 4                                                      
                                                        </td>
                                                        </tr>
                                                        <tr>
                                                        <td>
                                                            CHEQUE 5                                                     
                                                        </td>
                                                            <tr>
                                                        <td>
                                                            CHEQUE 6                                                       
                                                        </td>
                                                        </tr>
                                                        </tr> 
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                            <div class="box-footer">
                            <p class="text-center">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="submit" Visible="false" CssClass="btn btn-outline-info" />                        
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" Visible="false" CausesValidation="false"
                                TabIndex="15" CssClass="btn btn-outline-danger" />
                                </p>
                        </div>
                                        </asp:Panel>
                                </div>--%>

                    <div class="col-12 mt-3">
                        <asp:Panel ID="pnlStudent" runat="server">
                            <asp:ListView ID="lvStudents" runat="server">
                                <%--OnItemDataBound="lvStudents_ItemDataBound">--%>
                                <LayoutTemplate>
                                    <div id="demo-grid">
                                        <div class="sub-heading">
                                            <h5>Posted Cheque List</h5>
                                        </div>
                                            
                                        <table class="table table-striped table-bordered nowrap display" style="width:100%" id="myTable1">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th style="text-align: center;">
                                                        <asp:CheckBox ID="chkheader" runat="server" onclick="return totAll(this);" TabIndex="10" />
                                                    </th>
                                                    <th>Cheque
                                                    </th>
                                                    <th>Received Date
                                                    </th>
                                                    <th>Amount
                                                    </th>
                                                    <th>Cheque No
                                                    </th>
                                                    <th>Bank Name
                                                    </th>
                                                    <th>Next Due Date 
                                                    </th>
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
                                            <asp:CheckBox ID="cbRow" runat="server" />
                                            <%--onclick="ischecked();" --%>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCheque" runat="server" Text='<%#Eval("CHEQUE") %>' ToolTip='<%#Eval("RECNO") %>'></asp:Label>

                                        </td>
                                        <td>
                                            <%--<asp:TextBox ID="txtReceivedDate" runat="server" Enabled="true" autocomplete="off" Text='<%#Eval("RECEIVED_DATE") %>'  TabIndex="3" CssClass="form-control" />
                                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MM-yyyy"
                                                        TargetControlID="txtReceivedDate" PopupButtonID="imgCalStartDate" Enabled="true"
                                                        EnableViewState="true">
                                                    </ajaxToolKit:CalendarExtender> --%>
                                            <asp:TextBox ID="txtReceivedDate" runat="server" CssClass="form-control" autocomplete="off" Text='<%#Eval("RECEIVED_DATE") %>' ToolTip="Please Select Received Date">
                                            </asp:TextBox>
                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                PopupButtonID="calfrom" TargetControlID="txtReceivedDate" />
                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                                                TargetControlID="txtReceivedDate" Mask="99/99/9999" MessageValidatorTip="true"
                                                MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"
                                                ErrorTooltipEnabled="True" />
                                            <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meeFromDate"
                                                ControlToValidate="txtReceivedDate" Display="None" EmptyValueBlurredText="Empty" EmptyValueMessage="Please Enter Received Date"
                                                InvalidValueBlurredMessage="Invalid Date" InvalidValueMessage="Received Date is Invalid (Enter dd-mm-yyyy Format)"
                                                TooltipMessage="Please Enter Received Date" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAmount" runat="server" MaxLength="10" Enabled="true" autocomplete="off" Text='<%#Eval("AMOUNT") %>' CssClass="form-control" />
                                            <ajaxToolKit:FilteredTextBoxExtender ID="fteGrant" runat="server" TargetControlID="txtAmount" FilterType="Numbers,Custom" ValidChars="."></ajaxToolKit:FilteredTextBoxExtender>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtChequeNo" runat="server" Enabled="true" autocomplete="off" Text='<%#Eval("CHEQUE_NO") %>' CssClass="form-control" />
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlBank" runat="server" CausesValidation="false" AppendDataBoundItems="true" CssClass="form-control">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <%-- <asp:TextBox ID="txtDueDate" runat="server" Enabled="true" TabIndex="3" autocomplete="off" Text='<%#Eval("DUE_DATE") %>' CssClass="form-control" />
                                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MM-yyyy"
                                                        TargetControlID="txtDueDate" PopupButtonID="imgCalStartDate" Enabled="true"
                                                        EnableViewState="true">
                                                    </ajaxToolKit:CalendarExtender>  --%>

                                            <asp:TextBox ID="txtDueDate" runat="server" CssClass="form-control" autocomplete="off" Text='<%#Eval("DUE_DATE") %>' ToolTip="Please Select Next Due Date">
                                            </asp:TextBox>
                                            <ajaxToolKit:CalendarExtender ID="ceFromdate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                PopupButtonID="calfrom" TargetControlID="txtDueDate" />
                                            <ajaxToolKit:MaskedEditExtender ID="meeFromDate" runat="server"
                                                TargetControlID="txtDueDate" Mask="99/99/9999" MessageValidatorTip="true"
                                                MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"
                                                ErrorTooltipEnabled="True" />
                                            <ajaxToolKit:MaskedEditValidator ID="mevFromDate" runat="server" ControlExtender="meeFromDate"
                                                ControlToValidate="txtDueDate" Display="None" EmptyValueBlurredText="Empty" EmptyValueMessage="Please Enter Next Due Date"
                                                InvalidValueBlurredMessage="Invalid Date" InvalidValueMessage="Next Due Date is Invalid (Enter dd-mm-yyyy Format)"
                                                TooltipMessage="Please Enter Next Due Date" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>
                    </div>

                    <div class="col-12 btn-footer mt-4">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="submit" OnClick="btnSubmit_Click" Visible="false" CssClass="btn btn-outline-info" />
                        <asp:Button ID="btnReport" runat="server" Text="Report" ValidationGroup="submit" OnClick="btnReport_Click" Visible="false" CssClass="btn btn-outline-primary" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" Visible="false" CausesValidation="false"
                            TabIndex="15" CssClass="btn btn-outline-danger" />
                    </div>
                </div>

            </div>
        </div>
    </div>

    <div id="divModalboxContent" style="display: none; height: 540px">
    </div>
    <div id="divMsg" runat="server">
    </div>

    <%--<script type="text/javascript" lang="javascript">
        function ShowModalbox() {
            try {
                Modalbox.show($('divModalboxContent'), { title: 'Search Student', width: 600, overlayClose: false, slideDownDuration: 0.1, slideUpDuration: 0.1, afterLoad: SetFocus });
            }
            catch (e) {
                alert("Error: " + e.description);
            }
            return;
        }

        function SetFocus() {
            try {
                document.getElementById('<%= txtSearch.ClientID %>').focus();
            }
            catch (e) {
                alert("Error: " + e.description);
            }
        }

        function SubmitSearch(btnsearch) {
            try {
                var searchParams = "";
                alert('hi');
                debugger;
                if (document.getElementById('<%= rdoName.ClientID %>').checked) {
                        searchParams = "Name=" + document.getElementById('<%= txtSearch.ClientID %>').value;
                        searchParams += ",EnrollNo=";
                    }
                    else if (document.getElementById('<%= rdoEnrollmentNo.ClientID %>').checked) {
                        searchParams = "EnrollNo=" + document.getElementById('<%= txtSearch.ClientID %>').value;
                        searchParams += ",IdNo=";
                    }
                    else if (document.getElementById('<%=rdoTan.ClientID%>').checked) {
                        searchParams = "TANNO=" + document.getElementById('<%= txtSearch.ClientID %>').value;
                        searchParams += ",EnrollNo=";

                    }

                    //    else if (document.getElementById('<=rdoIdNo.ClientID%>').checked) {
                    //       searchParams = "IdNo=" + document.getElementById('<= txtSearch.ClientID %>').value;
                    //   searchParams += ",Name=";

                    //}
                    //  else if(document.getElementById('=  rdnusn.ClientID %>').checked)
                    //  {
                    //    searchParams = "SRNO=" + document.getElementById('<= txtSearch.ClientID %>').value;
                    //     searchParams += ",Name=";
                    // }
            searchParams += ",DegreeNo=" + document.getElementById('<%= ddlDegree.ClientID %>').options[document.getElementById('<%= ddlDegree.ClientID %>').selectedIndex].value;
                    searchParams += ",BranchNo=" + document.getElementById('<%= ddlBranch.ClientID %>').options[document.getElementById('<%= ddlBranch.ClientID %>').selectedIndex].value;
                    searchParams += ",YearNo=" + document.getElementById('<%= ddlYear.ClientID %>').options[document.getElementById('<%= ddlYear.ClientID %>').selectedIndex].value;
                    searchParams += ",SemNo=" + document.getElementById('<%= ddlSem.ClientID %>').options[document.getElementById('<%= ddlSem.ClientID %>').selectedIndex].value;

                    __doPostBack(btnsearch, searchParams);
                }
                catch (e) {
                    alert("Error: " + e.description);
                }
                return;
            }

            function ClearSearchBox(btnClear) {
                document.getElementById('<%=txtSearch.ClientID %>').value = '';
                __doPostBack(btnClear, '');
                return true;
            }

            function ShowHideDiv(divId, img) {
                try {
                    if (document.getElementById(divId) != null &&
                        document.getElementById(divId).style.display == 'none') {
                        document.getElementById(divId).style.display = 'block';
                        img.src = '../images/action_up.gif';
                    }
                    else {
                        document.getElementById(divId).style.display = 'none';
                        img.src = '../images/action_down.gif';
                    }
                }
                catch (e) {
                    // alert("Error: " + e.description);
                }
            }
            function IsNumeric(textbox) {
                if (textbox != null && textbox.value != "") {
                    if (isNaN(textbox.value)) {
                        document.getElementById(textbox.id).value = '';
                    }
                }
            }         
            
            $(function () {
                $(':text').bind('keydown', function (e) {
                    //on keydown for all textboxes prevent from postback
                    if (e.target.className != "searchtextbox2") {
                        if (e.keyCode == 13) { //if this is enter key
                            e.preventDefault();
                            return false;
                        }
                        else
                            return true;
                    }
                    else
                        return true;
                });
            });


            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $(function () {
                    $(':text').bind('keydown', function (e) {
                        //on keydown for all textboxes
                        if (e.target.className != "searchtextbox2") {
                            if (e.keyCode == 13) { //if this is enter key
                                e.preventDefault();
                                return false;
                            }
                            else
                                return true;
                        }
                        else
                            return true;
                    });
                });

            });
            //end
    </script>--%>

    <%--<script type="text/javascript" language="javascript">

        $(function () {
            //Enable Disable all TextBoxes when Header Row CheckBox is checked.
            $("[id*=cbHead]").bind("click", function () {
                var cbHead = $(this);

                //Find and reference the GridView.
                var List = $(this).closest("table");

                //Loop through the CheckBoxes in each Row.
                $("td", List).find("input[type=checkbox]").each(function () {

                    //If Header CheckBox is checked.
                    //Then check all CheckBoxes and enable the TextBoxes.  doing
                    if (cbHead.is(":checked")) {
                        $(this).attr("checked", "checked");
                        var td = $("td", $(this).closest("tr"));
                        td.css({ "background-color": "#D8EBF2" });
                        $("input[type=text]", td).removeAttr("disabled");

                        $("input[type=checkbox]", td).removeAttr("disabled");

                        $("[id*=btnSubmit]").removeAttr("disabled");
                        $("select", td).removeAttr("disabled");
                    } else {
                        $(this).removeAttr("checked");
                        var td = $("td", $(this).closest("tr"));
                        td.css({ "background-color": "#FFF" });
                        $("input[type=text]", td).attr("disabled", "disabled");

                        $("input[type=checkbox]", td).attr("disabled", "disabled");

                        $("select", td).attr("disabled", "disabled");
                        $("input[type=text]", td).val('');

                        $("input[type=checkbox]", td).val('');

                        $("select", td).val('0');

                    }
                });
            });

            //Enable Disable TextBoxes in a Row when the Row CheckBox is checked.
            $("[id*=cbRow1]").bind("click", function () {

                //Find and reference the GridView.
                var List = $(this).closest("table");

                //Find and reference the Header CheckBox.
                var cbHead = $("[id*=cbHead]", List);

                //If the CheckBox is Checked then enable the TextBoxes in thr Row. bhu done.
                if (!$(this).is(":checked")) {
                    var td = $("td", $(this).closest("tr"));
                    td.css({ "background-color": "#FFF" });
                    $("input[type=text]", td).attr("disabled", "disabled");

                    //rf   $("input[type=checkbox]", td).attr("disabled", "disabled");
                    $("input[type=checkbox]", td).attr("", "");

                    $("input[type=text]", td).val('');

                    $("input[type=checkbox]", td).val('');

                    //rf  $("select", td).attr("disabled", "disabled");

                    $("select", td).attr("", "");
                    $("select", td).val('0');
                } else {
                    var td = $("td", $(this).closest("tr"));
                    td.css({ "background-color": "#D8EBF2" });
                    $("input[type=text]", td).removeAttr("disabled");

                    $("input[type=checkbox]", td).removeAttr("disabled");

                    $("select", td).removeAttr("disabled");



                    $("[id*=btnSubmit]").removeAttr("disabled");

                }

                //Enable Header Row CheckBox if all the Row CheckBoxes are checked and vice versa.
                if ($("[id*=cbRow1]", List).length == $("[id*=cbRow1]:checked", List).length) {
                    cbHead.attr("checked", "checked");
                } else {
                    cbHead.removeAttr("checked");
                }
            });
        });


        var prm = Sys.WebForms.PageRequestManager.getInstance();

        prm.add_endRequest(function () {
            //Enable Disable all TextBoxes when Header Row CheckBox is checked.
            $("[id*=cbHead]").bind("click", function () {
                var cbHead = $(this);

                //Find and reference the GridView.
                var List = $(this).closest("table");

                //Loop through the CheckBoxes in each Row.
                $("td", List).find("input[type=checkbox]").each(function () {

                    //If Header CheckBox is checked.
                    //Then check all CheckBoxes and enable the TextBoxes.doing
                    if (cbHead.is(":checked")) {
                        $(this).attr("checked", "checked");
                        var td = $("td", $(this).closest("tr"));
                        td.css({ "background-color": "#D8EBF2" });
                        $("input[type=text]", td).removeAttr("disabled");

                        $("input[type=checkbox]", td).removeAttr("disabled");

                        $("[id*=btnSubmit]").removeAttr("disabled");
                        $("select", td).removeAttr("disabled");
                    } else {
                        $(this).removeAttr("checked");
                        var td = $("td", $(this).closest("tr"));
                        td.css({ "background-color": "#FFF" });
                        $("input[type=text]", td).attr("disabled", "disabled");

                        $("input[type=checkbox]", td).attr("disabled", "disabled");

                        $("select", td).attr("disabled", "disabled");
                        $("input[type=text]", td).val('');

                        $("input[type=checkbox]", td).val('');

                        $("select", td).val('0');
                    }
                });
            });

            //Enable Disable TextBoxes in a Row when the Row CheckBox is checked.
            $("[id*=cbRow1]").bind("click", function () {

                //Find and reference the GridView.
                var List = $(this).closest("table");

                //Find and reference the Header CheckBox.
                var cbHead = $("[id*=cbHead]", List);

                //If the CheckBox is Checked then enable the TextBoxes in thr Row.bhu done
                if (!$(this).is(":checked")) {
                    var td = $("td", $(this).closest("tr"));
                    td.css({ "background-color": "#FFF" });
                    $("input[type=text]", td).attr("disabled", "disabled");

                    //rf  $("input[type=checkbox]", td).attr("disabled", "disabled");

                    //    $("input[type=text]", td).val('');

                    $("input[type=checkbox]", td).val('');

                    //rf   $("select", td).attr("disabled", "disabled");
                    $("select", td).val('0');
                } else {
                    var td = $("td", $(this).closest("tr"));
                    td.css({ "background-color": "#D8EBF2" });
                    $("input[type=text]", td).removeAttr("disabled");

                    $("input[type=checkbox]", td).removeAttr("disabled");

                    $("select", td).removeAttr("disabled");
                    $("[id*=btnSubmit]").removeAttr("disabled");

                }

                //edit

                //


                //Enable Header Row CheckBox if all the Row CheckBoxes are checked and vice versa.
                if ($("[id*=cbRow1]", List).length == $("[id*=cbRow1]:checked", List).length) {
                    cbHead.attr("checked", "checked");
                } else {
                    cbHead.removeAttr("checked");
                }
            });
        });
    </script>--%>

    <script language="javascript" type="text/javascript">
        function totAll(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (headchk.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }
    </script>

    <%--<script type="text/javascript">

         $(document).ready(function () {

             bindDataTable1();// for fileupload control change event work after postback
             Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable1);

         });

         function bindDataTable1() {
             $('#myDatepicker2, #myDatepicker3, #myDatepicker4, #myDatepicker5,#myDatepicker9,#myDatepicker10,#myDatepicker11,#myDatepicker12,#myDatepicker14,#pickMinorityCertIssueDate,#dateAllotmentOrderDt,#SportsDate').datetimepicker({
                 format: 'DD/MM/YYYY',
                 maxDate: moment(),
                 useCurrent: false
             });

             $('#myDatepicker8, #myDatepicker7, #myDatepicker6, #myDatepicker15,#myDatepicker16,#datePickerDiplomaYr').datetimepicker({
                 format: 'MM/YYYY',

             });
         }


         $('#myDatepicker2, #myDatepicker3, #myDatepicker4, #myDatepicker5,#myDatepicker9,#myDatepicker10,#myDatepicker11,#myDatepicker12,#myDatepicker14,#pickMinorityCertIssueDate,#dateAllotmentOrderDt,#SportsDate').datetimepicker({
             format: 'DD/MM/YYYY',
             maxDate: moment(),
             useCurrent: false
         });

         $('#myDatepicker8, #myDatepicker7, #myDatepicker6, #myDatepicker15,#myDatepicker16,#datePickerDiplomaYr').datetimepicker({
             format: 'MM/YYYY'
         });



    </script>

    <script type="text/javascript" language="javascript">
        function ischecked() {
            debugger;
            alert('hi');
            var lst = document.getElementById('ctl00_ContentPlaceHolder1_lvStudents_ctrl' + i + '_cbRow');
            if (lst.type == 'checkbox') {
                if (chk.checked == true) {
                    lst.checked = true;
                    alert('hello');

                }
                else {
                    lst.checked = false;

                }
            }
        }
    </script>--%>
</asp:Content>

