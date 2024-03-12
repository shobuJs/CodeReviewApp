<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="ModifyStudType.aspx.cs" Inherits="Academic_ModifyStudType" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="pnlFeeTable"
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

    <asp:UpdatePanel ID="pnlFeeTable" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title"><span>Cancel Approval (Student Type)</span></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnSearch">
                                    <div class="sub-heading">
                                        <h5>Search Student</h5>
                                    </div>
                                </asp:Panel>

                                <div id="dvdisplay" runat="server">
                                    <div class="row">
                                        <div class="form-group col-lg-6 col-md-12 col-12">
                                            <label>TAN/ PAN : </label>
                                            <span class="form-inline">
                                                <asp:TextBox ID="txtEnrollmentSearch" runat="server" ValidationGroup="Show" MaxLength="16" class="form-control" PlaceHolder="Please Enter TAN/ PAN"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvSearch" runat="server" ControlToValidate="txtEnrollmentSearch" Display="None" ErrorMessage="Please Enter Temp No." ValidationGroup="Show">
                                                </asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEnrollmentSearch" Display="None" ErrorMessage="Please Enter TAN/ PAN" ValidationGroup="StudType">
                                                </asp:RequiredFieldValidator>
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Show" />
                                                <%--<asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-outline-info m-top" ValidationGroup="Show" />--%>
                                                <asp:LinkButton ID="btnSearch" runat="server"  OnClick="btnSearch_Click" CssClass="btn btn-outline-info m-top" ValidationGroup="Show">Search</asp:LinkButton>
                                                <asp:Button ID="btncancel" runat="server" Text="Cancel " OnClick="btncancel_Click" CssClass="btn btn-outline-danger m-top" />
                                            </span>
                                        </div>

                                        <div class="form-group col-12" id="StudDetails" runat="server" visible="false">
                                            <div class="row">
                                                <div class="col-lg-4 col-md-6 col-12">
                                                    <ul class="list-group list-group-unbordered">
                                                        <li class="list-group-item"><b>Name Of Student :</b>
                                                            <a class="sub-label"><asp:Label ID="lblname" runat="server" Text="" Font-Bold="true"></asp:Label> </a>
                                                        </li>
                                                        <%--<li class="list-group-item"><b>Student Initial/Surname :</b>
                                                            <a class="pull-right"><asp:Label ID="lbllastname" runat="server" Text="" Font-Bold="true"></asp:Label> </a>
                                                        </li>--%>
                                                        <li class="list-group-item"><b>Degree :</b>
                                                            <a class="sub-label"><asp:Label ID="lbldegree" runat="server" Text="" Font-Bold="true"></asp:Label> </a>
                                                        </li>    
                                                    </ul>
                                                </div>

                                                <div class="col-lg-4 col-md-6 col-12">
                                                    <ul class="list-group list-group-unbordered">
                                                        <li class="list-group-item"><b>Branch :</b>
                                                            <a class="sub-label"><asp:Label ID="lblbranch" runat="server" Text="" Font-Bold="true"></asp:Label> </a>
                                                        </li>
                                                        <li class="list-group-item"><b>Semester/Year :</b>
                                                            <a class="sub-label"><asp:Label ID="lblsem" runat="server" Text="" ToolTip="1" Font-Bold="true"></asp:Label> </a>
                                                        </li> 
                                                    </ul>
                                                </div>

                                                <div class="col-lg-4 col-md-6 col-12">
                                                    <ul class="list-group list-group-unbordered">
                                                        <li class="list-group-item"><b>TAN/ PAN :</b>
                                                            <a class="sub-label"><asp:Label ID="lblapp" runat="server" Text="" Font-Bold="true"></asp:Label> </a>
                                                        </li>
                                                        <li class="list-group-item"><b>Need To Change Hostel/Transport :</b>
                                                            <a class="">
                                                                <br/>
                                                                <asp:RadioButton ID="rdbHostel" runat="server" Text="Hostel" GroupName="Link2" TabIndex="6" />
                                                                    &nbsp;&nbsp;&nbsp;
                                                                <asp:RadioButton ID="rdbTransport" runat="server" Text="Transport" GroupName="Link2" TabIndex="6" />
                                                                    &nbsp;&nbsp;&nbsp;
                                                                <asp:RadioButton ID="rdbNonTransport" runat="server" Text="Non Transport" GroupName="Link2" TabIndex="6" />
                                                            </a>
                                                        </li> 
                                                    </ul>
                                                </div>
                                            </div>
                                        
                                            <div class="form-group col-12">
                                                <div class="label-dynamic">
                                                    <label> Reason For Cancellation</label>
                                                </div>
                                                <asp:TextBox ID="txtRemarks" runat="server" Font-Bold="true" class="form-control" TextMode="MultiLine"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtRemarks"
                                                    Display="None" ErrorMessage="Please Enter Reason For Cancellation" ValidationGroup="StudType">
                                                </asp:RequiredFieldValidator>
                                            </div>

                                            <%--  <div class="form-group col-md-6" style="display: none">
                                                <li class="list-group-item">
                                                    <label><span style="color: red">*</span>Hostel Type :</label>
                                                    <asp:DropDownList ID="ddlhostel" runat="server" AppendDataBoundItems="true" CssClass="form-control">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </li>
                                            </div>
                                            <div class="form-group col-md-4">
                                                <li class="list-group-item">
                                                    <label>Transport Facility :</label>
                                                    <a class="pull-right">
                                                        <asp:RadioButton ID="rdbtransportyes" runat="server" Text="YES" GroupName="Link3" Enabled="false"
                                                            TabIndex="6" />
                                                          &nbsp;&nbsp;&nbsp;<asp:RadioButton ID="rdbtransportNo" runat="server" Text="NO" GroupName="Link3" Enabled="false"
                                                              TabIndex="6" />
                                              
                                                    </a>
                                                </li>
                                            </div>

                                            <div class="form-group col-md-6" style="display: none">
                                                <li class="list-group-item">
                                                    <label><span style="color: red">*</span>Route Type :</label>
                                                    <asp:DropDownList ID="ddlroute" runat="server" AppendDataBoundItems="true" CssClass="form-control">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </li>
                                            </div>
                                            <div class="form-group col-md-4">
                                                <li class="list-group-item">
                                                    <label>Tuition Fees :</label>
                                                    <a class="pull-right">
                                                        <asp:RadioButton ID="rdbtutionyes" runat="server" Text="YES" GroupName="Link1" Checked="true" Enabled="false"
                                                            TabIndex="6" />
                                                        &nbsp;&nbsp;&nbsp;<asp:RadioButton ID="rdbtutionNo" runat="server" Text="NO" GroupName="Link1" Enabled="false"
                                                            TabIndex="6" />
                                                    </a></li>
                                            </div>
                                            <div class="form-group col-md-4">
                                                <li class="list-group-item">
                                                    <label>Admission Quota :</label>
                                                   <br /> <a>
                                                        <asp:Label ID="lbladmquota" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </a></li>
                                            </div>

                                              <div class="form-group col-md-4">
                                                <li class="list-group-item">
                                                    <label><span style="color: red">*</span>Student Admission Date :</label>                                           
                                                       <div class='input-group date'>
                                                          <asp:TextBox runat="server" ID="txtAdmdate" class="form-control" data-inputmask="'mask': '99/99/9999'"></asp:TextBox>
                                                           <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                               TargetControlID="txtAdmdate" PopupButtonID="txtAdmdate" Enabled="true"
                                                               EnableViewState="true">
                                                           </ajaxToolKit:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="valStartDate" runat="server" ControlToValidate="txtAdmdate"
                                                        Display="None" ErrorMessage="Please Enter Student Admission Date." SetFocusOnError="true"
                                                        ValidationGroup="show" />
                                                            <span class="input-group-addon">
                                                                <span class="glyphicon glyphicon-calendar"></span>
                                                            </span>
                                                        </div>
                                                   </li>
                                            </div>

                                            <div class="form-group col-md-4">
                                                <li class="list-group-item">
                                                    <label><span style="color: red">*</span>Payment Type :</label>                                     
                                                    <asp:DropDownList ID="ddlpaytype" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control" 
                                                        OnSelectedIndexChanged="ddlpaytype_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlpaytype"
                                                        Display="None" ErrorMessage="Please Select Payment Type" InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlpaytype"
                                                        Display="None" ErrorMessage="Please Select Payment Type" InitialValue="0" SetFocusOnError="True"
                                                        ValidationGroup="show"></asp:RequiredFieldValidator>
                                                </li>
                                            </div>
                                   
                                            <div class="form-group col-md-6" style="display: none">
                                                <label>Late Fees :</label><a class="pull-right">
                                                    <asp:Label ID="lblLateFee" runat="server" Text="0.00" Font-Bold="true"></asp:Label></a></li>
                                            </div>--%>
                                                <%--<div class="form-group col-md-6">
                                                <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Text="" Font-Bold="true"></asp:Label>
                                            </div>--%>

                                                <%--  <li class="list-group-item">
                                               <label><span style="color:red"> *</span>Fee Type :</label>
                                                    <asp:CheckBox ID="chktution" runat="server" Text="Tuition Fees" Checked="true"  /> &nbsp;&nbsp;
                                               <asp:CheckBox ID="chkhostel" runat="server" Text="Hostel Fees"  />&nbsp;&nbsp;
                                                    <asp:CheckBox ID="chktransport" runat="server" Text="Transport Fees"  OnCheckedChanged="chktransport_CheckedChanged"  
                                                   AutoPostBack="True" />&nbsp;&nbsp;
                                             
                                                    <asp:Label ID="lblOrderID" runat="server" Text="" Font-Bold="true" Visible="false"></asp:Label>
                                                </li>--%>

                                                <%-- <div class="form-group col-md-6" style="display: none">
                                                <label>Total Fees :</label><a class="pull-right">
                                                    <asp:Label ID="lbltotalfees" runat="server" Text="0.00" Font-Bold="true"></asp:Label></a>
                                            </div>--%>
                                        </div>
                                
                                    </div>
                                </div>
                            </div>

                            <div class="col-12" id="dvaction" runat="server">
                                <div class="btn-footer">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="StudType"
                                        OnClick="btnSubmit_Click" CssClass="btn btn-outline-info" Visible="false" />
                                    <%-- <asp:HiddenField ID="hdfadmbatch" runat="server" />--%>

                                    <%--     <asp:Button ID="btnProcess" runat="server" Text="Fees Process "
                                    OnClick="btnProcess_Click" ValidationGroup="submit" CssClass="btn btn-outline-info" />

                                    <asp:Button ID="btnChallan" runat="server" Text="Tuition Challan Receipt" Visible="false"
                                        OnClick="btnChallan_Click" CssClass="btn btn-success" />
                         
                                    <asp:Button ID="btntransportChallan" runat="server" Text="Transport Challan Receipt" Visible="false"
                                        OnClick="btntransportChallan_Click" class="buttonStyle ui-corner-all btn btn-success" />
                                        <asp:Button ID="btnHostelChallan" runat="server" Text="Hostel Challan Receipt" Visible="false" OnClick="btnHostelChallan_Click"
                                        CssClass="btn btn-success" />--%>
                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="True"
                                        ShowSummary="False" ValidationGroup="StudType" />
                                </div>

                                <%--  <div class="form-group col-md-12"><asp:ListView ID="lvdemand" runat="server">
                                    <LayoutTemplate>
                                        <div class="vista-grid">
                                            <div class="titlebar">

                                                <h3>Fees Applicable</h3>
                                            </div>
                                            <table class="table table-bordered table-hover text-center">
                                                <thead>
                                                    <tr class="bg-light-blue">
                                                        <th>Action
                                                        </th>
                                                        <th>Transaction Date 
                                                        </th>
                                                        <th>Receipt Type 
                                                        </th>
                                                        <th>Pay Type 
                                                        </th>
                                                        <th>Amount 
                                                        </th>

                                                    </tr>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </thead>
                                            </table>
                                        </div>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr class="item">
                                            <td>
                                                <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/images/delete.gif"
                                                    CommandArgument='<%# Eval("DM_NO")%>' AlternateText="Delete Record" ToolTip="Delete Record"
                                                    OnClick="btnEdit_Click" TabIndex="12"
                                                    OnClientClick="return confirm ('Do you want to Delete this Demand!');" />
                                            </td>
                                            <td>
                                                <%# Eval("DEMAND_DATE")%>
                                            </td>
                                            <td>
                                                <%# Eval("RECIEPT_TITLE")%>
                                            </td>
                                            <td>
                                                <%# Eval("PAYTYPENAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("TOTAL_AMT")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView> </div>--%>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btncancel" />
        </Triggers>
    </asp:UpdatePanel>

    <div id="divMsg" runat="server">
    </div>

    <script type="text/javascript">
        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = '0';
                alert('Only Numeric Value Allowed!');
                txt.focus();
                return;
            }
        };
    </script>
    <%-- <script>
         function showpreview(input) {
             if (input.files && input.files[0]) {
                 var reader = new FileReader();
                 reader.onload = function (e) {
                     $('#imgpreview').css('visibility', 'visible');
                     $('#imgpreview').attr('src', e.target.result);
                 }
                 reader.readAsDataURL(input.files[0]);
             }
         }
        </script>--%>

    <script>
        $(document).ready(function () {
            $('.btn').on('click', function () {

                var textVal = $("#ctl00_ContentPlaceHolder1_txtEnrollmentSearch").val();

                if (textVal !== "") {

                    var $this = $(this);
                    // alert($(this).val());

                    //var loadingText = 'loading...';
                    //if ($(this).val() !== loadingText) {
                    //    $this.data('original-text', $(this).val());
                    //    $this.val(loadingText);
                    //}
                    var loadingText = '<i class="fa  fa-spinner fa-spin"></i> loading...';
                    if ($(this).html() !== loadingText) {
                        $this.data('original-text', $(this).html());
                        $this.html(loadingText);
                    }
                }
            });
        });

    </script>
</asp:Content>
