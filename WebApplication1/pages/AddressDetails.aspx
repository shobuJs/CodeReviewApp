<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddressDetails.aspx.cs" MasterPageFile="~/SiteMasterPage.master" Inherits="ACADEMIC_AddressDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div style="z-index: 1; position: absolute; top: 10px; left: 500px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updAddressDetails"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px; text-align: center">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel ID="updAddressDetails" runat="server">
        <ContentTemplate>

            <div class="box box-info">
                <div class="box-header with-border">
                    <span class="glyphicon glyphicon-user text-blue"></span>
                    <h3 class="box-title">STUDENT INFORMATION</h3>
                    <div class="box-tools pull-right">
                    </div>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-3" id="divtabs" runat="server">
                            <div class="col-md-12">
                                <div class="panel panel-info" style="box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);">
                                    <div class="panel panel-heading"><b>Click To Open Respective Information </b></div>
                                    <div class="panel-body">
                                        <aside class="sidebar">

                                            <!-- sidebar: style can be found in sidebar.less -->
                                            <section class="sidebar" style="background-color: #12aae2">
                                                <ul class="sidebar-menu">
                                                    <!-- Optionally, you can add icons to the links -->
                                                    <br />
                                                    <div id="divhome" runat="server">

                                                        <li class="treeview">&nbsp; <i class="fa fa-search"><span>
                                                            <asp:LinkButton runat="server" ID="lnkGoHome"
                                                                ToolTip="Please Click Here To Go To Home" OnClick="lnkGoHome_Click" Style="color: white; font-size: 16px;" onmouseover="this.style.color='yellow'" onmouseout="this.style.color='white'" Text="Search New Student"> 

                                                            </asp:LinkButton>
                                                        </span>
                                                        </i>
                                                            <hr />
                                                        </li>
                                                    </div>
                                                    <li class="treeview">&nbsp <i class="fa fa-user"><span>
                                                        <asp:LinkButton runat="server" ID="lnkPersonalDetail"
                                                            ToolTip="Please Click Here For Personal Details." OnClick="lnkPersonalDetail_Click" Style="color: white; font-size: 16px;" onmouseover="this.style.color='yellow'" onmouseout="this.style.color='white'" Text="Personal Details"> 

                                                        </asp:LinkButton>
                                                    </span>
                                                    </i>
                                                        <hr />
                                                    </li>

                                                    <li class="treeview">&nbsp <i class="fa fa-map-marker"><span>
                                                        <asp:LinkButton runat="server" ID="lnkAddressDetail"
                                                            ToolTip="Please Click Here For Address Details." OnClick="lnkAddressDetail_Click" Style="color: yellow; font-size: 16px;" Text="Address Details"> 
                                                        </asp:LinkButton>
                                                    </span>
                                                    </i>

                                                        <hr />
                                                    </li>

                                                    <div id="divadmissiondetails" runat="server">
                                                        <li class="treeview">&nbsp<i class="fa fa-university"><span>
                                                            <asp:LinkButton runat="server" ID="lnkAdmissionDetail"
                                                                ToolTip="Please Click Here For Personal Details." OnClick="lnkAdmissionDetail_Click" Style="color: white; font-size: 16px;" onmouseover="this.style.color='yellow'" onmouseout="this.style.color='white'" Text="Admission Details"> 
                                                            </asp:LinkButton>
                                                        </span>
                                                        </i>

                                                            <hr />
                                                        </li>
                                                    </div>
                                                    <li class="treeview">
                                                        <i class="fa fa-info-circle"><span>
                                                            <asp:LinkButton runat="server" ID="lnkDasaStudentInfo"
                                                                ToolTip="Please Click Here For DASA Student Information." OnClick="lnkDasaStudentInfo_Click" Style="color: white; font-size: 16px;" onmouseover="this.style.color='yellow'" onmouseout="this.style.color='white'" Text="DASA Student Information"> 
                                                            </asp:LinkButton>
                                                        </span>
                                                        </i>

                                                        <hr />
                                                    </li>
                                                    <li class="treeview">&nbsp<i class="fa fa-graduation-cap"><span>
                                                        <asp:LinkButton runat="server" ID="lnkQualificationDetail"
                                                            ToolTip="Please Click Here For Qualification Details." OnClick="lnkQualificationDetail_Click" Style="color: white; font-size: 16px;" onmouseover="this.style.color='yellow'" onmouseout="this.style.color='white'" Text="Qualification Details"> 
                                                        </asp:LinkButton>
                                                    </span>
                                                    </i>

                                                        <hr />
                                                    </li>
                                                    <li class="treeview">&nbsp<i class="fa fa-link"><span>
                                                        <asp:LinkButton runat="server" ID="lnkotherinfo"
                                                            ToolTip="Please Click Here For Other Information." OnClick="lnkotherinfo_Click" Style="color: white; font-size: 16px;" onmouseover="this.style.color='yellow'" onmouseout="this.style.color='white'" Text="Other Information"> 
                                                        </asp:LinkButton>
                                                    </span>
                                                    </i>
                                                        <hr />
                                                    </li>
                                                    <li class="treeview">&nbsp;<i class="glyphicon glyphicon-print"><span>
                                                        <asp:LinkButton runat="server" ID="lnkprintapp" OnClick="lnkprintapp_Click" Style="color: white; font-size: 16px;" onmouseover="this.style.color='yellow'" onmouseout="this.style.color='white'" Text="Print"></asp:LinkButton>
                                                    </span>

                                                    </i>
                                                        <p></p>
                                                    </li>
                                                </ul>
                                            </section>
                                        </aside>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-9">
                            <div class="box box-info">
                                <div class="box-header with-border">
                                    <h1 class="box-title"><b>Address Details</b></h1>
                                    <div class="box-tools pull-right">
                                    </div>
                                </div>
                                <br />
                                <div style="color: Red; font-weight: bold; margin-top: -20px; margin-right: -5px;">
                                    &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                                </div>
                                <div class="box-body">
                                    <div class="col-md-12" id="divAddressAndContactDetails" style="display: block;">
                                        <asp:UpdatePanel ID="upAddressDetails" runat="server">
                                            <ContentTemplate>
                                                <div class="row">
                                                    <div style="text-align: left; height: 50%; width: 100%; background-color: #d9edf7">
                                                        <strong>
                                                            <h4><b>Permanent Address</b></h4>
                                                        </strong>
                                                    </div>


                                                    <div class="form-group col-md-6">
                                                        <label><span style="color: red;">*</span>Address Details </label>
                                                        <asp:ImageButton ID="imgbToCopyLocalAddress" Visible="false" runat="server" ImageUrl="~/images/copy.png"
                                                            OnClientClick="copyLocalAddr(this)" ToolTip="Copy Local Address" TabIndex="11" />
                                                        <asp:TextBox ID="txtPermAddress" CssClass="form-control" runat="server" TextMode="MultiLine"
                                                            Rows="3" MaxLength="200" ToolTip="Please Enter Permanent Address"
                                                            TabIndex="12" />
                                                        <asp:RequiredFieldValidator ID="rfvPermAddress" runat="server" ControlToValidate="txtPermAddress"
                                                            Display="None" ErrorMessage="Please Enter Permanent Address" SetFocusOnError="True"
                                                            ValidationGroup="Academic"></asp:RequiredFieldValidator>

                                                        <asp:TextBox ID="txtPdistrict" CssClass="form-control" runat="server" Visible="False"></asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                                                            FilterMode="InvalidChars" FilterType="Custom" InvalidChars="1234567890" TargetControlID="txtPdistrict" />
                                                    </div>
                                                    <div class="form-group col-md-3" style="margin-top: 13px">
                                                        <label><span style="color: red;">*</span> City/Village</label>
                                                        <asp:DropDownList ID="ddlPermCity" CssClass="form-control" runat="server" TabIndex="12" AppendDataBoundItems="True"
                                                            ToolTip="Please Select City">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvPermCity" runat="server" ControlToValidate="ddlPermCity"
                                                            Display="None" ErrorMessage="Please select Permanent City" SetFocusOnError="True"
                                                            ValidationGroup="Academic" InitialValue="0"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-md-3" style="margin-top: 13px">
                                                        <label><span style="color: red;">*</span> State</label>
                                                        <asp:DropDownList ID="ddlPermState" CssClass="form-control" runat="server" AppendDataBoundItems="True" TabIndex="13"
                                                            ToolTip="Please Select State" AutoPostBack="True" OnSelectedIndexChanged="ddlPermState_SelectedIndexChanged" />
                                                        <asp:RequiredFieldValidator ID="rfvPermState" runat="server" ControlToValidate="ddlPermState"
                                                            Display="None" ErrorMessage="Please Select Permanent State" SetFocusOnError="True"
                                                            ValidationGroup="Academic" InitialValue="0"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <label><span style="color: red;">*</span> District</label>
                                                        <asp:DropDownList ID="ddlPdistrict" CssClass="form-control" runat="server" AppendDataBoundItems="True" AutoPostBack="true"
                                                            ToolTip="Please select district" TabIndex="14">
                                                        </asp:DropDownList>
                                                        <asp:HiddenField ID="hdn_Pdistrict" runat="server" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlPdistrict"
                                                            Display="None" ErrorMessage="Please Select Permanent District" SetFocusOnError="True"
                                                            ValidationGroup="Academic" InitialValue="0"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <label><span style="color: red;">*</span> ZIP/PIN</label>
                                                        <asp:TextBox ID="txtPermPIN" CssClass="form-control" runat="server" TabIndex="15" ToolTip="Please Enter PIN"
                                                            MaxLength="6" onkeyup="validateNumeric(this);" />
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftrPermPIN" runat="server" FilterType="Numbers"
                                                            TargetControlID="txtPermPIN">
                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtPermPIN"
                                                            Display="None" ErrorMessage="Please Enter Permanent Pin Code" SetFocusOnError="True"
                                                            ValidationGroup="Academic"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <label>Landline No.</label>
                                                        <asp:TextBox ID="txtLocalNo" CssClass="form-control" runat="server" ToolTip="Please Enter Number"
                                                            ValidationGroup="Academic" TabIndex="16" MaxLength="12" />
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="fteLoaclno" runat="server" FilterType="Numbers"
                                                            TargetControlID="txtLocalNo">
                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                    </div>
                                                    <div class="form-group col-md-4" id="divlocalmobileaddress" runat="server" visible="false">
                                                        <label>Mobile No.</label>
                                                        <asp:TextBox ID="txtMobileNo" CssClass="form-control" runat="server" ToolTip="Please Enter Mobile No." TabIndex="17"
                                                            MaxLength="12" />
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="fteMobilenum" runat="server" FilterType="Numbers"
                                                            TargetControlID="txtMobileNo">
                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                        <asp:CompareValidator ID="rfvMobileNo" runat="server" ControlToValidate="txtMobileNo"
                                                            ErrorMessage="Please Numeric Number" Operator="DataTypeCheck" SetFocusOnError="True"
                                                            Type="Integer" ValidationGroup="Academic" Display="None" Visible="False"></asp:CompareValidator>
                                                    </div>
                                                    <div class="form-group col-md-4" style="display: none">
                                                        <label>Parent Email Id</label>
                                                        <asp:TextBox ID="txtPermEmailId" CssClass="form-control" runat="server" ToolTip="Please Enter E-Mail Address" TabIndex="59"
                                                            ValidationGroup="Academic" />
                                                        <asp:RegularExpressionValidator ID="revPermEmailId" runat="server" ControlToValidate="txtPermEmailId"
                                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                            ErrorMessage="Please Enter Valid EmailID" ValidationGroup="Academic"></asp:RegularExpressionValidator>
                                                    </div>
                                                    <div class="form-group col-md-4" id="divbirthtehsil" runat="server" visible="false">
                                                        <label><%--<span style="color: red;">*</span>--%> Sub Division/Tehsil</label>
                                                        <asp:TextBox ID="txtTehsil" CssClass="form-control" runat="server" MaxLength="150" TabIndex="17" ToolTip="Please Enter Tehsil" onkeypress="return alphaOnly(event);" />
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender30" runat="server"
                                                            TargetControlID="txtTehsil" FilterType="Custom" FilterMode="InvalidChars"
                                                            InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <label>Area Post Office</label>
                                                        <asp:TextBox ID="txtpermpostOff" CssClass="form-control" runat="server" MaxLength="150" TabIndex="18" ToolTip="Please Enter Post Office" onkeypress="return alphaOnly(event);" />
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender56" runat="server"
                                                            TargetControlID="txtpermpostOff" FilterType="Custom" FilterMode="InvalidChars"
                                                            InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <label>Area Police Station</label>
                                                        <asp:TextBox ID="txtPermPoliceStation" CssClass="form-control" runat="server" MaxLength="150" TabIndex="19" onkeypress="return alphaOnly(event);"
                                                            ToolTip="Please Enter Police Station" />
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender31" runat="server"
                                                            TargetControlID="txtPermPoliceStation" FilterType="Custom" FilterMode="InvalidChars"
                                                            InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                                    </div>
                                                    <div class="col-md-3" style="display: none;">
                                                        <label>Sub Division/Tehsil</label>
                                                        <asp:TextBox ID="txtPermTehsil" CssClass="form-control" runat="server" MaxLength="150" ToolTip="Please Enter Tehsil" TabIndex="20" />
                                                    </div>

                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div style="text-align: left; height: 50%; width: 100%; background-color: #d9edf7">
                                                        <strong>
                                                            <h4><b>Local Address  (Copy Permanent)</b></h4>
                                                        </strong>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div style="text-align: left; height: 50%; width: 100%;">
                                                            <asp:CheckBox ID="chkcopypermanentadress" onclick="copyPermanentlAddr(this)" runat="server" TabIndex="10" Text="&nbsp;&nbsp;&nbsp;&nbsp;Same as Permanent Address" />
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="form-group col-md-6">
                                                        <label><span style="color: red;">*</span>Address Details</label>
                                                        <asp:TextBox ID="txtLocalAddress" CssClass="form-control" runat="server" TextMode="MultiLine"
                                                            Rows="3" MaxLength="200" ToolTip="Please Enter Local ddress" TabIndex="1" />
                                                        <asp:TextBox ID="txtLdistrict" runat="server" Visible="False"></asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                                            FilterMode="InvalidChars" FilterType="Custom" InvalidChars="1234567890" TargetControlID="txtLdistrict" />

                                                        <asp:RequiredFieldValidator ID="rfvtxtLocalAddress" runat="server" ControlToValidate="txtLocalAddress"
                                                            Display="None" ErrorMessage="Please Enter Local Address" SetFocusOnError="True"
                                                            ValidationGroup="Academic"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <label><span style="color: red;">*</span> City/Village</label>
                                                        <asp:DropDownList ID="ddlLocalCity" CssClass="form-control" runat="server" TabIndex="2" AppendDataBoundItems="true"
                                                            ToolTip="Please Select City">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvddlLocalCity" runat="server" ControlToValidate="ddlLocalCity"
                                                            Display="None" ErrorMessage="Please Select Local City" SetFocusOnError="True"
                                                            TabIndex="23" ValidationGroup="Academic" InitialValue="0"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <label><span style="color: red;">*</span> State</label>
                                                        <asp:DropDownList ID="ddlLocalState" CssClass="form-control" runat="server" AppendDataBoundItems="True"
                                                            ToolTip="Please Select State" TabIndex="3" AutoPostBack="True" OnSelectedIndexChanged="ddlLocalState_SelectedIndexChanged" />
                                                        <asp:RequiredFieldValidator ID="rfvLocalState" runat="server" ControlToValidate="ddlLocalState"
                                                            Display="None" ErrorMessage="Please Select Local State" SetFocusOnError="True"
                                                            ValidationGroup="Academic" InitialValue="0"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <label><span style="color: red;">*</span> District</label>
                                                        <asp:DropDownList ID="ddlLdistrict" CssClass="form-control" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                                            TabIndex="4" ToolTip="Please Select District">
                                                        </asp:DropDownList>
                                                        <asp:HiddenField ID="hdnldistrict" runat="server" />
                                                        <asp:CompareValidator ID="rfvLocalPIN" runat="server" ControlToValidate="txtLocalPIN"
                                                            Display="None" ErrorMessage="Please Enter Numeric Value" Operator="DataTypeCheck"
                                                            SetFocusOnError="True" Type="Integer" ValidationGroup="Academic" Visible="False"></asp:CompareValidator>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="ddlLdistrict"
                                                            Display="None" ErrorMessage="Please Select Local District" SetFocusOnError="True"
                                                            ValidationGroup="Academic" InitialValue="0"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <label><span style="color: red;">*</span> ZIP/PIN</label>
                                                        <asp:TextBox ID="txtLocalPIN" CssClass="form-control" runat="server" TabIndex="5" MaxLength="6" ToolTip="Please Enter PIN" />
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftbPINLoal" runat="server" FilterType="Numbers"
                                                            TargetControlID="txtLocalPIN">
                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                        <asp:RequiredFieldValidator ID="rfvtxtLocalPIN" runat="server" ControlToValidate="txtLocalPIN"
                                                            Display="None" ErrorMessage="Please Enter Local Pin Code" SetFocusOnError="True"
                                                            ValidationGroup="Academic"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <label>Landline No.</label>
                                                        <asp:TextBox ID="txtLocalLandlineNo" CssClass="form-control" runat="server" ToolTip="Please Enter Landline Number"
                                                            TabIndex="6" MaxLength="12" />
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="fteLocalLandline" runat="server" FilterType="Numbers"
                                                            TargetControlID="txtLocalLandlineNo">
                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                    </div>
                                                    <div class="form-group col-md-4" id="divPermanentaddress" runat="server" visible="false">
                                                        <label>Mobile No.</label>
                                                        <asp:TextBox ID="txtLocalMobileNo" CssClass="form-control" runat="server" ToolTip="Please Enter Mobile No."
                                                            TabIndex="7" MaxLength="12" />
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="fteLocalMobile" runat="server" FilterType="Numbers"
                                                            TargetControlID="txtLocalMobileNo">
                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                        <asp:CompareValidator ID="rfvLocalMobileNo" runat="server" ControlToValidate="txtLocalMobileNo"
                                                            ErrorMessage="Please Numeric Number" Operator="DataTypeCheck" SetFocusOnError="True"
                                                            Type="Integer" ValidationGroup="Academic" Display="None"></asp:CompareValidator>
                                                        <asp:RegularExpressionValidator runat="server" ErrorMessage="Mobile No. is Invalid" ID="RegularExpressionValidator4" ControlToValidate="txtLocalMobileNo" ValidationExpression=".{10}.*"
                                                            Display="None" ValidationGroup="Academic"></asp:RegularExpressionValidator>
                                                    </div>
                                                    <div class="form-group col-md-4" style="display: none">
                                                        <label>Parent Email Id</label>
                                                        <asp:TextBox ID="txtLocalEmail" CssClass="form-control" runat="server" ToolTip="Please Enter E-Mail Address" />
                                                        <asp:RegularExpressionValidator ID="rfvLocalEmail" runat="server" ControlToValidate="txtLocalEmail"
                                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                            ErrorMessage="Please Enter Valid Local EmailId" ValidationGroup="Academic"></asp:RegularExpressionValidator>
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <label>Area Post Office</label>
                                                        <asp:TextBox ID="txtpostoff" CssClass="form-control" runat="server" MaxLength="150" TabIndex="8" ToolTip="Please Enter Post Office" onkeypress="return alphaOnly(event);" />
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender55" runat="server"
                                                            TargetControlID="txtpostoff" FilterType="Custom" FilterMode="InvalidChars"
                                                            InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />

                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <label>Area Police Station</label>
                                                        <asp:TextBox ID="txtpolicestation" CssClass="form-control" runat="server" MaxLength="150" TabIndex="9" ToolTip="Please Enter Police Station" onkeypress="return alphaOnly(event);" />
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender29" runat="server"
                                                            TargetControlID="txtpolicestation" FilterType="Custom" FilterMode="InvalidChars"
                                                            InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />

                                                    </div>
                                                    <asp:Panel ID="pnlvisible" Visible="false" runat="server">
                                                        <div class="form-group col-md-12">
                                                            <label>Correspondance Address</label>
                                                            <asp:TextBox ID="txtCorresAddress" CssClass="form-control" runat="server" MaxLength="150" ToolTip="Please Enter Correspondace Address"
                                                                TextMode="MultiLine" TabIndex="48" />
                                                        </div>
                                                    </asp:Panel>
                                                    <asp:Panel ID="pnlcorrenspondence" runat="server" Visible="false">
                                                        <div class="form-group col-md-3">
                                                            <label>Correspondace Pincode</label>
                                                            <asp:TextBox ID="txtCorresPin" CssClass="form-control" runat="server" MaxLength="6" TabIndex="49" ToolTip="Please Enter Correspondace Pin" />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender24" runat="server" FilterType="Numbers"
                                                                TargetControlID="txtCorresPin">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                        </div>
                                                        <div class="form-group col-md-3">
                                                            <label>Correspondance Mob No.</label>
                                                            <asp:TextBox ID="txtCorresMob" CssClass="form-control" runat="server" MaxLength="10" TabIndex="50" ToolTip="Please Enter Correspondace Mob No." />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender25" runat="server" FilterType="Numbers"
                                                                TargetControlID="txtCorresMob">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                        </div>
                                                    </asp:Panel>

                                                </div>
                                                <div class="row">
                                                    <div style="text-align: left; height: 50%; width: 100%; background-color: #d9edf7">
                                                        <strong>
                                                            <h4><b>Local Guardian's Address</b></h4>
                                                        </strong>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div style="text-align: left; height: 50%; width: 100%;">
                                                            <asp:CheckBox ID="chkcopyperaddress" onclick="copyPermAddr(this)" TabIndex="21" runat="server" Text="&nbsp;&nbsp;&nbsp;&nbsp;Same as Permanent Address" />
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-md-6">
                                                        <label>Address Details(Copy Permanent) </label>
                                                        <asp:ImageButton ID="imgCopyPermAddress" Visible="false" runat="server" ImageUrl="~/images/copy.png"
                                                            OnClientClick="copyPermAddr(this)" ToolTip="Copy Permanent Address" TabIndex="64" />
                                                        <asp:TextBox ID="txtGuardianAddress" CssClass="form-control" runat="server" TextMode="MultiLine"
                                                            Rows="3" MaxLength="200" ToolTip="Please Enter Guardian Address" TabIndex="22"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <label for="city">Guardian Name</label>
                                                        <asp:TextBox ID="txtGuardianName" CssClass="form-control" runat="server" ToolTip="Please Enter Guardian Name" onkeypress="return alphaOnly(event);"
                                                            TabIndex="23"></asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server"
                                                            TargetControlID="txtGuardianName" FilterType="Custom" FilterMode="InvalidChars"
                                                            InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <label>Guardian Ph. No.</label>
                                                        <asp:TextBox ID="txtGuardianLandline" CssClass="form-control" runat="server" TabIndex="24" MaxLength="15" ToolTip="Please Enter Guardian Ph. No."></asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="fteGuardianLandline" runat="server" FilterType="Numbers"
                                                            TargetControlID="txtGuardianLandline">
                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                        <asp:RegularExpressionValidator runat="server" ErrorMessage="Guardian Ph. No. is Invalid" ID="RegularExpressionValidator5" ControlToValidate="txtGuardianLandline" ValidationExpression=".{10}.*"
                                                            Display="None" ValidationGroup="Academic"></asp:RegularExpressionValidator>
                                                    </div>
                                                    <div class="form-group col-md-3" style="display: none">
                                                        <label>Annual Income</label>
                                                        <asp:TextBox ID="txtAnnualIncome" CssClass="form-control" runat="server" TabIndex="25" MaxLength="12" ToolTip="Please Enter Annual Income"></asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="fteAnnualIncome" runat="server" FilterType="Numbers"
                                                            TargetControlID="txtAnnualIncome">
                                                        </ajaxToolKit:FilteredTextBoxExtender>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <label for="city">Guardian's Relation</label>
                                                        <asp:TextBox ID="txtRelationWithGuardian" CssClass="form-control" runat="server" ToolTip="Please Enter Relation" onkeypress="return alphaOnly(event);"
                                                            TabIndex="26"></asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server"
                                                            TargetControlID="txtRelationWithGuardian" FilterType="Custom" FilterMode="InvalidChars"
                                                            InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <label>Occupation</label>
                                                        <asp:TextBox ID="txtGoccupationName" CssClass="form-control" runat="server" ToolTip="Please Enter GOccupation"
                                                            ValidationGroup="Academic" TabIndex="27" onkeypress="return alphaOnly(event);"></asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender32" runat="server"
                                                            TargetControlID="txtGoccupationName" FilterType="Custom" FilterMode="InvalidChars"
                                                            InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                                    </div>
                                                    <div class="form-group col-md-3" style="display: none">
                                                        <label>Guardian Email Address</label>
                                                        <asp:TextBox ID="txtguardianEmail" CssClass="form-control" runat="server" TabIndex="70" ToolTip="Please Enter E-Mail"
                                                            ValidationGroup="Academic" />
                                                        <asp:RegularExpressionValidator ID="revGuardianEmail" runat="server" ControlToValidate="txtguardianEmail"
                                                            Display="Dynamic" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                            ErrorMessage="Please Enter Valid EmailID" ValidationGroup="Academic"></asp:RegularExpressionValidator>
                                                    </div>
                                                    <div class="form-group col-md-3">
                                                        <label>Guardian Qualification</label>
                                                        <asp:TextBox ID="txtGDesignation" CssClass="form-control" runat="server" ToolTip="Please Enter Guardian Qualification"
                                                            ValidationGroup="Academic" TabIndex="28" onkeypress="return alphaOnly(event);"></asp:TextBox>
                                                        <ajaxToolKit:FilteredTextBoxExtender ID="fteGDesignation" runat="server" TargetControlID="txtGDesignation"
                                                            FilterType="Custom" FilterMode="InvalidChars" InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="box box-footer text-center">
                                    <asp:Button ID="btnSubmit" runat="server" TabIndex="29" Text="Save & Continue >>" ToolTip="Click to Report"
                                        class="btn btn-outline-primary" OnClick="btnSubmit_Click" ValidationGroup="Academic" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True"
                                        ShowSummary="False" ValidationGroup="Academic" />
                                    &nbsp
                           
                                    <button runat="server" id="btnGohome" visible="false" tabindex="30" onserverclick="btnGohome_Click" class="btn btn-outline-danger btnGohome" tooltip="Click to Go Back Home">
                                        <i class="fa fa-home"></i>Go Back Home
                                    </button>




                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />

        </Triggers>
    </asp:UpdatePanel>

    <script type="text/javascript">
        function alphaOnly(e) {
            var code;
            if (!e) var e = window.event;

            if (e.keyCode) code = e.keyCode;
            else if (e.which) code = e.which;

            if ((code >= 48) && (code <= 57)) { return false; }
            return true;
        }
    </script>
    <script type="text/javascript" language="javascript">
        function toggleExpansion(imageCtl, divId) {
            if (document.getElementById(divId).style.display == "block") {
                document.getElementById(divId).style.display = "none";
                imageCtl.src = "../images/expand_blue.jpg";
            }
            else if (document.getElementById(divId).style.display == "none") {
                document.getElementById(divId).style.display = "block";
                imageCtl.src = "../images/collapse_blue.jpg";
            }
        }

        function copyLocalAddr(chk) {
            debugger;
            if (chk.checked) {
                document.getElementById('<%= txtPermAddress.ClientID %>').value = document.getElementById('<%= txtLocalAddress.ClientID %>').value;
                document.getElementById('<%= ddlPermCity.ClientID %>').selectedIndex = document.getElementById('<%= ddlLocalCity.ClientID %>').selectedIndex;
                document.getElementById('<%= ddlPermState.ClientID %>').selectedIndex = document.getElementById('<%= ddlLocalState.ClientID %>').selectedIndex;
                document.getElementById('<%= hdn_Pdistrict.ClientID %>').value = document.getElementById('<%= ddlLdistrict.ClientID %>').selectedIndex;
                document.getElementById('<%= ddlPdistrict.ClientID %>').selectedIndex = document.getElementById('<%= ddlLdistrict.ClientID %>').selectedIndex;
                document.getElementById('<%= txtPermPIN.ClientID %>').value = document.getElementById('<%= txtLocalPIN.ClientID %>').value;

                document.getElementById('<%= txtLocalNo.ClientID %>').value = document.getElementById('<%= txtLocalLandlineNo.ClientID %>').value;
                document.getElementById('<%= txtpermpostOff.ClientID %>').value = document.getElementById('<%= txtpostoff.ClientID %>').value;
                document.getElementById('<%= txtPermPoliceStation.ClientID %>').value = document.getElementById('<%= txtpolicestation.ClientID %>').value;
            }
            else {
                document.getElementById('<%= txtPermAddress.ClientID %>').value = '';
                document.getElementById('<%= ddlPermCity.ClientID %>').selectedIndex = 0;
                document.getElementById('<%= ddlPermState.ClientID %>').selectedIndex = 0;
                document.getElementById('<%= txtPermPIN.ClientID %>').value = '';
                document.getElementById('<%= ddlPermState.ClientID %>').selectedIndex = 0;
                document.getElementById('<%= ddlPdistrict.ClientID %>').selectedIndex = 0;
                document.getElementById('<%= txtLocalNo.ClientID %>').value = '';
                document.getElementById('<%= txtpermpostOff.ClientID %>').value = '';
                document.getElementById('<%= txtPermPoliceStation.ClientID %>').value = '';
            }
        }


        function copyPermanentlAddr(chk) {
            debugger;
            if (chk.checked) {

                document.getElementById('<%= txtLocalAddress.ClientID %>').value = document.getElementById('<%= txtPermAddress.ClientID %>').value;
                document.getElementById('<%= txtLocalPIN.ClientID %>').value = document.getElementById('<%=txtPermPIN.ClientID %>').value;
                document.getElementById('<%= ddlLocalCity.ClientID %>').selectedIndex = document.getElementById('<%=ddlPermCity.ClientID %>').selectedIndex;
                document.getElementById('<%= ddlLocalState.ClientID %>').selectedIndex = document.getElementById('<%=ddlPermState.ClientID %>').selectedIndex;
                document.getElementById('<%= ddlLdistrict.ClientID %>').value = document.getElementById('<%= ddlPdistrict.ClientID %>').value;
                document.getElementById('<%= hdnldistrict.ClientID %>').value = document.getElementById('<%= ddlLdistrict.ClientID %>').selectedIndex;
                document.getElementById('<%= txtpostoff.ClientID %>').value = document.getElementById('<%=txtpermpostOff.ClientID %>').value;
                document.getElementById('<%= txtpolicestation.ClientID %>').value = document.getElementById('<%=txtPermPoliceStation.ClientID %>').value;
                document.getElementById('<%= txtLocalLandlineNo.ClientID %>').value = document.getElementById('<%=txtLocalNo.ClientID %>').value;
                document.getElementById('<%= txtTehsil.ClientID %>').value = document.getElementById('<%= txtPermTehsil.ClientID %>').value;
                document.getElementById('<%= txtLocalEmail.ClientID %>').value = document.getElementById('<%=txtPermEmailId.ClientID %>').value;
                document.getElementById('<%= txtLocalMobileNo.ClientID %>').value = document.getElementById('<%=txtMobileNo.ClientID %>').value;

            }
            else {
                document.getElementById('<%= txtLocalAddress.ClientID %>').value = '';

                document.getElementById('<%= ddlLocalCity.ClientID %>').selectedIndex = 0;

                document.getElementById('<%= ddlLdistrict.ClientID %>').selectedIndex = 0;

                document.getElementById('<%= ddlLocalState.ClientID %>').selectedIndex = 0;

                document.getElementById('<%= txtLocalPIN.ClientID %>').value = '';

                document.getElementById('<%= txtLocalLandlineNo.ClientID %>').value = '';

                document.getElementById('<%= txtpostoff.ClientID %>').value = '';

                document.getElementById('<%= txtpolicestation.ClientID %>').value = '';

            }
        }

        function copyPermAddr(chk) {
            if (chk.checked) {
                document.getElementById('<%= txtGuardianAddress.ClientID %>').value = document.getElementById('<%= txtPermAddress.ClientID %>').value;
                if (document.getElementById('<%= ddlPermCity.ClientID %>').selectedIndex > 0) {
                    document.getElementById('<%= txtGuardianAddress.ClientID %>').value =
                document.getElementById('<%= txtGuardianAddress.ClientID %>').value + ', ' + '\n' +
                document.getElementById('<%= ddlPermCity.ClientID %>').options[document.getElementById('<%= ddlPermCity.ClientID %>').selectedIndex].text;
                }
                if (document.getElementById('<%= ddlPermState.ClientID %>').selectedIndex > 0) {
                    document.getElementById('<%= txtGuardianAddress.ClientID %>').value =
                document.getElementById('<%= txtGuardianAddress.ClientID %>').value + ', ' + '\n' +
               document.getElementById('<%= ddlPermState.ClientID %>').options[document.getElementById('<%= ddlPermState.ClientID %>').selectedIndex].text;
                }
            }
            else {
                document.getElementById('<%= txtGuardianAddress.ClientID %>').value = '';
            }
        }

        function validateAlphabet(txt) {
            var expAlphabet = /^[A-Za-z]+$/;
            if (txt.value.search(expAlphabet) == -1) {
                txt.value = txt.value.substring(0, (txt.value.length) - 1);
                txt.focus();
                alert('Only Alphabets Allowed');
                return false;
            }
            else
                return true;

        }
        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = '';
                alert('Only Numeric Characters Allowed!');
                txt.focus();
                return;
            }
        }

        function validateAlphaNumeric(txt) {
            var expAN = /[$\\@\\\#%\^\&\*\(\)\[\]\+\_popup\{\}|`\~\=\|]/;
            var strPass = txt.value;
            var strLength = strPass.length;
            var lchar = txt.value.charAt((strLength) - 1);

            if (lchar.search(expAN) != -1) {
                txt.value(txt.value.substring(0, (strLength) - 1));
                txt.focus();
                alert('Only Alpha-Numeric Characters Allowed!');
            }
            return true;
        }

        function LoadImage() {
            document.getElementById("ctl00_ContentPlaceHolder1_imgPhoto").src = document.getElementById("ctl00_ContentPlaceHolder1_fuPhotoUpload").value;
            document.getElementById("ctl00_ContentPlaceHolder1_imgPhoto").height = '96px';
            document.getElementById("ctl00_ContentPlaceHolder1_imgPhoto").width = '96px';
        }
        function conver_uppercase(text) {
            text.value = text.value.toUpperCase();
        }

    </script>
    <div id="divMsg" runat="server"></div>
</asp:Content>
