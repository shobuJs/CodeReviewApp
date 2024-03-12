<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PersonalDetails.aspx.cs" MasterPageFile="~/SiteMasterPage.master" Inherits="ACADEMIC_PersonalDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--END : FOLLOWING CODE ALLOWS THE AUTOCOMPLETE TO BE FIRED IN UPDATEPANEL--%>
    <div style="z-index: 1; position: absolute; top: 10px; left: 500px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpersonalinformation"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px; text-align: center">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel ID="updpersonalinformation" runat="server">
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
                                                    <li class="treeview">&nbsp; <i class="fa fa-user"><span>
                                                        <asp:LinkButton runat="server" ID="lnkPersonalDetail"
                                                            ToolTip="Please Click Here For Personal Details." OnClick="lnkPersonalDetail_Click" Style="color: yellow; font-size: 16px;" Text="Personal Details"> 

                                                        </asp:LinkButton>
                                                    </span>
                                                    </i>
                                                        <hr />
                                                    </li>

                                                    <li class="treeview">&nbsp <i class="fa fa-map-marker"><span>
                                                        <asp:LinkButton runat="server" ID="lnkAddressDetail"
                                                            ToolTip="Please Click Here For Address Details." OnClick="lnkAddressDetail_Click" Style="color: white; font-size: 16px;" onmouseover="this.style.color='yellow'" onmouseout="this.style.color='white'" Text="Address Details"> 
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
                                                            ToolTip="Please Click Here For Other Information." OnClick="lnkotherinfo_Click" Style="color: white; font-size: 15px;" onmouseover="this.style.color='yellow'" onmouseout="this.style.color='white'" Text="Other Information"> 
                                                        </asp:LinkButton>
                                                    </span>
                                                    </i>

                                                        <p></p>

                                                    </li>
                                                    <hr />
                                                    <li class="treeview">&nbsp;<i class="glyphicon glyphicon-print"><span>
                                                        <asp:LinkButton runat="server" ID="lnkprintapp" ToolTip="Please Click Here For Admission Form Report" OnClick="lnkprintapp_Click" Style="color: white; font-size: 16px; padding-left: -65px" onmouseover="this.style.color='yellow'" onmouseout="this.style.color='white'" Text="Print"></asp:LinkButton>
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
                        <style>
                            body
                            {
                                padding-top: 20px;
                                background-color: #F7F7F7;
                            }

                            .panel > .list-group
                            {
                                margin-bottom: 0;
                            }

                                .panel > .list-group .list-group-item
                                {
                                    border-width: 1px 0;
                                }

                                    .panel > .list-group .list-group-item:first-child
                                    {
                                        border-top-right-radius: 0;
                                        border-top-left-radius: 0;
                                    }

                                    .panel > .list-group .list-group-item:last-child
                                    {
                                        border-bottom: 0;
                                    }

                            .panel-heading + .list-group .list-group-item:first-child
                            {
                                border-top-width: 0;
                            }

                            .panel-default .list-group-item.active
                            {
                                color: black;
                                background-color: blue;
                                border-color: #DDD;
                            }

                            .panel-primary .list-group-item.active
                            {
                                color: black;
                                background-color: blue;
                                border-color: #428BCA;
                            }

                            .panel-success .list-group-item.active
                            {
                                color: black;
                                background-color: #12aae2;
                                border-color: #D6E9C6;
                            }

                            .panel-info .list-group-item.active
                            {
                                color: white;
                                background-color: #12aae2;
                                border-color: #BCE8F1;
                            }

                            .panel-warning .list-group-item.active
                            {
                                color: white;
                                background-color: #12aae2;
                                border-color: #FAEBCC;
                            }

                            .panel-danger .list-group-item.active
                            {
                                color: white;
                                background-color: #12aae2;
                                border-color: #EBCCD1;
                            }

                            .panel a.list-group-item.active:hover, a.list-group-item.active:focus
                            {
                                color: white;
                                background-color: #DDD;
                                border-color: #DDD;
                            }
                        </style>
                        <div class="form-group col-md-9" id="divGeneralInfo" style="display: block;">

                            <div class="box box-info">
                                <div class="box-header with-border">
                                    <span class="glyphicon glyphicon-user text-blue"></span>
                                    <h3 class="box-title"><strong>PERSONAL INFORMATION</strong></h3>
                                    <div class="box-tools pull-right">
                                    </div>
                                </div>
                                <br />
                                <div style="color: Red; font-weight: bold; margin-top: -20px; margin-right: -5px;">
                                    &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                                </div>
                                <div class="box-body">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div style="text-align: left; height: 50%; width: 100%; background-color: #d9edf7">
                                                <h4><b>Student Details</b></h4>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="col-md-2 form-group" runat="server" id="divtxtidno" visible="false">
                                                <label>ID No.</label>
                                                <asp:TextBox ID="txtIDNo" runat="server" CssClass="form-control" Enabled="False" Visible="false" />
                                            </div>
                                            <div class="col-md-2 form-group">
                                            </div>
                                            <div class="col-md-5 form-group" style="display: none">

                                                <label>Enrollment No.</label>
                                                <asp:TextBox ID="txtRegNo" CssClass="form-control" runat="server" ToolTip="Please Enter Roll No."
                                                    Enabled="false" />
                                                <asp:TextBox ID="txtEnrollno" runat="server" ToolTip="Please Enter EnRoll No."
                                                    Enabled="false" Visible="false" />
                                            </div>

                                            <div class="col-md-4 form-group">

                                                <label>SR NO.</label>
                                                <asp:TextBox ID="txtsrno" CssClass="form-control" runat="server" ToolTip="Please Enter SR No."
                                                    Enabled="false" />
                                                <asp:TextBox ID="TextBox2" runat="server" ToolTip="Please Enter SR No."
                                                    Enabled="false" Visible="false" />
                                            </div>



                                        </div>

                                        <div class="col-md-12">
                                            <div class="col-md-4 form-group">
                                                <label><span style="color: red;">&nbsp;</span> Student Full Name</label>
                                                <asp:TextBox ID="txtStudFullname" CssClass="form-control" runat="server" Enabled="false" TabIndex="1" ToolTip="Please Enter Student Full Name" />
                                            </div>
                                            <div class="col-md-4 form-group">
                                                <label><span style="color: red;">*</span> Student First Name</label>
                                                <asp:TextBox ID="txtStudentName" CssClass="form-control" runat="server" MaxLength="150" TabIndex="2" ToolTip="Please Enter Student First name" onblur="conver_uppercase(this);" onkeyup="conver_uppercase(this);" onkeypress="return alphaOnly(event);" />
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                    FilterMode="InvalidChars" FilterType="Custom" InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" TargetControlID="txtStudentName" />
                                                <asp:RequiredFieldValidator ID="rfvStudentName" runat="server" ControlToValidate="txtStudentName"
                                                    Display="None" ErrorMessage="Please Enter Student First Name" SetFocusOnError="True"
                                                    ValidationGroup="Academic"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-4 form-group">

                                                <label>Student Middle Name</label>
                                                <asp:TextBox ID="txtStudMiddleName" CssClass="form-control" runat="server" MaxLength="150" TabIndex="3" ToolTip="Please Enter Student Middle name" onblur="conver_uppercase(this);" onkeyup="conver_uppercase(this);" onkeypress="return alphaOnly(event);" />
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender20" runat="server"
                                                    FilterMode="InvalidChars" FilterType="Custom" InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" TargetControlID="txtStudMiddleName" />

                                            </div>

                                        </div>
                                        <div class="col-md-12">
                                            <div class="col-md-4 form-group">

                                                <label>Student Last Name</label>
                                                <asp:TextBox ID="txtStudLastName" CssClass="form-control" runat="server" MaxLength="150" TabIndex="4" ToolTip="Please Enter Student Last name" onblur="conver_uppercase(this);" onkeyup="conver_uppercase(this);" onkeypress="return alphaOnly(event);" />
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender22" runat="server"
                                                    FilterMode="InvalidChars" FilterType="Custom" InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" TargetControlID="txtStudLastName" />

                                            </div>
                                            <div class="form-group col-md-4">
                                                <label><span style="color: red;">*</span> Student Mobile No.</label>
                                                <asp:TextBox ID="txtStudMobile" CssClass="form-control" runat="server" MaxLength="12" TabIndex="5"
                                                    onkeyup="validateNumeric(this);" ToolTip="Please Enter Student Mobile No"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvtxtStudMobile" runat="server" ControlToValidate="txtStudMobile"
                                                    Display="None" ErrorMessage="Please Enter Student Mobile No " SetFocusOnError="True"
                                                    ValidationGroup="Academic"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-md-4">
                                                <label><span style="color: red;">*</span> Student Email Id</label>
                                                <asp:TextBox ID="txtStudentEmail" CssClass="form-control" runat="server" TabIndex="6" ToolTip="Please Enter Student Email Id"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="rfvStudentEmail" runat="server" ControlToValidate="txtStudentEmail"
                                                    Display="Dynamic" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                    ErrorMessage="Please Enter Valid Student Email Id" ValidationGroup="Academic">
                                                </asp:RegularExpressionValidator>
                                                <asp:RequiredFieldValidator ID="rfvtxtStudentEmail" runat="server" ControlToValidate="txtStudentEmail"
                                                    Display="None" ErrorMessage="Please Enter Student Email-Id" SetFocusOnError="True"
                                                    TabIndex="20" ValidationGroup="Academic"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-md-4" style="display: none">
                                                <label><span style="color: red;">&nbsp;</span> Student Indus Email</label>
                                                <asp:TextBox ID="txtInstituteEmail" CssClass="form-control" runat="server" TabIndex="200" ToolTip="Please enter Indus Email Id"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="rfvIndusEmail" runat="server" ControlToValidate="txtInstituteEmail"
                                                    Display="Dynamic" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                    ErrorMessage="Please Enter Valid Student Indus Email Id" ValidationGroup="Academic">
                                                </asp:RegularExpressionValidator>

                                            </div>
                                        </div>
                                        <%--Important Information--%>
                                        <div class="col-md-12">
                                            <div class="form-group col-md-4">
                                                <label><span style="color: red;">*</span> Date of Birth</label>
                                                <div class="input-group">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtDateOfBirth" CssClass="form-control" runat="server" TabIndex="7" ToolTip="Please Enter Date Of Birth" />

                                                    <asp:RequiredFieldValidator ID="rfvDateOfBirth" runat="server" ControlToValidate="txtDateOfBirth"
                                                        Display="None" ErrorMessage="Please Enter Date Of Birth" SetFocusOnError="True"
                                                        ValidationGroup="Academic"></asp:RequiredFieldValidator>
                                                    <ajaxToolKit:CalendarExtender ID="ceDateOfBirth" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtDateOfBirth" PopupButtonID="imgCalDateOfBirth" Enabled="True">
                                                    </ajaxToolKit:CalendarExtender>
                                                    <ajaxToolKit:MaskedEditExtender ID="meeDateOfBirth" runat="server" TargetControlID="txtDateOfBirth"
                                                        Mask="99/99/9999" MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True"
                                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                                        CultureTimePlaceholder="" Enabled="True" />
                                                    <ajaxToolKit:MaskedEditValidator ID="mevDateOfBirth" runat="server"
                                                        ControlExtender="meeDateOfBirth" ControlToValidate="txtDateOfBirth" IsValidEmpty="False"
                                                        InvalidValueMessage="Date is invalid" Display="None" TooltipMessage="Input a date"
                                                        ErrorMessage="Please Enter Valid Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                        ValidationGroup="Academic" SetFocusOnError="True" />
                                                </div>
                                            </div>
                                            <div class="form-group col-md-4">
                                                <label><span style="color: red;">*</span> Blood Group</label>
                                                <asp:DropDownList ID="ddlBloodGroupNo" CssClass="form-control" runat="server" AppendDataBoundItems="True"
                                                    TabIndex="8" ToolTip="Please Select Blood group">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlBloodGroupNo" runat="server" ControlToValidate="ddlBloodGroupNo"
                                                    Display="None" ErrorMessage="Please Select Blood Group" SetFocusOnError="True"
                                                    ValidationGroup="Academic" InitialValue="0"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-md-4">
                                                <label><span style="color: red;">*</span> Nationality</label>
                                                <asp:DropDownList ID="ddlNationality" CssClass="form-control" runat="server" TabIndex="9" AppendDataBoundItems="True"
                                                    ToolTip="Please Select Nationality" />
                                                <asp:RequiredFieldValidator ID="rfvddlNationality" runat="server" ControlToValidate="ddlNationality"
                                                    Display="None" ErrorMessage="Please Select Nationality" SetFocusOnError="True"
                                                    ValidationGroup="Academic" InitialValue="0"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group col-md-4" style="display: none">
                                                <label><span style="color: red;">*</span> Caste Category</label>
                                                <asp:DropDownList ID="ddlCasteCategory" runat="server" CssClass="form-control"
                                                    AppendDataBoundItems="True" ToolTip="Please Select Category" TabIndex="10" />

                                            </div>
                                            <div class="form-group col-md-4">
                                                <label><span style="color: red;">*</span> Caste</label>
                                                <asp:DropDownList ID="ddlCaste" CssClass="form-control" runat="server" AppendDataBoundItems="True"
                                                    ToolTip="Please Select  Caste" TabIndex="11">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlCaste" runat="server" ControlToValidate="ddlCaste"
                                                    Display="None" ErrorMessage="Please Enter Caste" SetFocusOnError="True"
                                                    ValidationGroup="Academic" InitialValue="0"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-md-4">
                                                <label>Sub Caste</label>
                                                <asp:TextBox ID="txtSubCaste" CssClass="form-control" runat="server" TabIndex="12" ToolTip="Please Enter Sub Caste" onkeypress="return alphaOnly(event);"
                                                    MaxLength="100" />
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender28" runat="server"
                                                    TargetControlID="txtSubCaste" FilterType="Custom" FilterMode="InvalidChars"
                                                    InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                            </div>
                                            <div class="form-group col-md-4">
                                                <label><span style="color: red;">*</span> Claimed Category</label>
                                                <asp:DropDownList ID="ddlClaimedcategory" CssClass="form-control" runat="server" AppendDataBoundItems="True"
                                                    ToolTip="Please Select Claimed category" TabIndex="13">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlClaimedcategory" runat="server" ControlToValidate="ddlClaimedcategory"
                                                    Display="None" ErrorMessage="Please Enter Claimed Category" SetFocusOnError="True"
                                                    ValidationGroup="Academic" InitialValue="0"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-12">

                                            <div class="form-group col-md-4">
                                                <label><span style="color: red;">*</span> Physically Handicapped</label>
                                                <asp:DropDownList ID="ddlHandicap" runat="server" CssClass="form-control" AppendDataBoundItems="True" ToolTip="Please Select Physical Handicap Status"
                                                    TabIndex="14">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfv_ddlHandicap" runat="server" ControlToValidate="ddlHandicap"
                                                    Display="None" ErrorMessage="Please Select Handicap Status" SetFocusOnError="True"
                                                    ValidationGroup="Academic" InitialValue="0"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-md-4">
                                                <label><span style="color: red;">*</span>&nbsp;Gender</label>
                                                <asp:RadioButtonList ID="rdobtn_Gender" runat="server" TabIndex="15" RepeatDirection="Horizontal" ToolTip="Please Select Gender">
                                                    <asp:ListItem Text="&nbsp;Male" Value="M"></asp:ListItem>
                                                    <asp:ListItem Text="&nbsp;Female" Value="F"></asp:ListItem>

                                                </asp:RadioButtonList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Select Gender"
                                                    ControlToValidate="rdobtn_Gender" Display="None" ValidationGroup="Academic"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-md-4">
                                                <label><span style="color: red;">*</span> Religion</label>
                                                <asp:DropDownList ID="ddlReligion" CssClass="form-control" runat="server" AppendDataBoundItems="True"
                                                    ToolTip="Please Select Religion" TabIndex="16" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlReligion"
                                                    Display="None" ErrorMessage="Please Select Religion" SetFocusOnError="True"
                                                    ValidationGroup="Academic" InitialValue="0"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-12">

                                            <div class="form-group col-md-4">
                                                <label><span style="color: red;">*</span> Aadhar No.</label>
                                                <asp:TextBox ID="txtAddharCardNo" CssClass="form-control" runat="server" ToolTip="Please Enter Aadhar Card No."
                                                    TabIndex="17" MaxLength="12"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender26" runat="server"
                                                    FilterMode="ValidChars" FilterType="Custom" ValidChars="0123456789" TargetControlID="txtAddharCardNo" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAddharCardNo"
                                                    Display="None" ErrorMessage="Please Enter Aadhar Card No." SetFocusOnError="True" TabIndex="8"
                                                    ValidationGroup="Academic"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator runat="server" ErrorMessage="Aadhar No. is Invalid" ID="RegularExpressionValidator6" ControlToValidate="txtAddharCardNo" ValidationExpression=".{12}.*"
                                                    Display="None" ValidationGroup="Academic"></asp:RegularExpressionValidator>
                                            </div>
                                            <div class="form-group col-md-4">
                                                <label><span style="color: red;">*</span> Marital Status</label>

                                                <asp:RadioButtonList ID="rdobtn_marital" runat="server" TabIndex="18" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="&nbsp;Single" Value="N"></asp:ListItem>
                                                    <asp:ListItem Text="&nbsp;Married" Value="Y"></asp:ListItem>
                                                </asp:RadioButtonList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Select Marital Status"
                                                    ControlToValidate="rdobtn_marital" Display="None" ValidationGroup="Academic"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div style="text-align: left; height: 50%; width: 100%; background-color: #d9edf7">
                                                <h4><b>Father Details</b></h4>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group col-md-4">
                                                <label><span style="color: red;">*</span> Father First Name</label>
                                                <asp:TextBox ID="txtFatherName" CssClass="form-control" runat="server" TabIndex="19" ToolTip="Please Enter Father's First Name" onblur="conver_uppercase(this);" onkeyup="conver_uppercase(this);" onkeypress="return alphaOnly(event);" Enabled="true" />
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                    FilterMode="InvalidChars" FilterType="Custom" InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" TargetControlID="txtFatherName" />
                                                <asp:RequiredFieldValidator ID="rfvtxtFatherName" runat="server" ControlToValidate="txtFatherName"
                                                    Display="None" ErrorMessage="Please Enter Father First Name" SetFocusOnError="True"
                                                    ValidationGroup="Academic"></asp:RequiredFieldValidator>

                                            </div>
                                            <div class="form-group col-md-4">
                                                <label>Father Middle Name</label>
                                                <asp:TextBox ID="txtFatherMiddleName" CssClass="form-control" runat="server" TabIndex="20" ToolTip="Please Enter Father Middle Name" onblur="conver_uppercase(this);" onkeyup="conver_uppercase(this);" onkeypress="return alphaOnly(event);" />
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender21" runat="server"
                                                    FilterMode="InvalidChars" FilterType="Custom" InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" TargetControlID="txtFatherMiddleName" />

                                            </div>
                                            <div class="form-group col-md-4">

                                                <label>Father Last Name</label>
                                                <asp:TextBox ID="txtFatherLastName" CssClass="form-control" runat="server" TabIndex="21" ToolTip="Please Enter Father Last Name" onblur="conver_uppercase(this);" onkeyup="conver_uppercase(this);" onkeypress="return alphaOnly(event);" />
                                                <%--<ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender23" runat="server"></ajaxToolKit:FilteredTextBoxExtender>--%>
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="form-group col-md-4" style="display: none">
                                                <label><span style="color: red;">&nbsp;</span> Father Full Name</label>
                                                <asp:TextBox ID="txtFatherFullName" CssClass="form-control" runat="server" TabIndex="22" ToolTip="Please Enter Father Full Name" onkeypress="return alphaOnly(event);" />

                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender27" runat="server"
                                                    FilterMode="InvalidChars" FilterType="Custom" InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" TargetControlID="txtFatherFullName" />
                                            </div>
                                            <div class="form-group col-md-4">
                                                <label><span style="color: red;">*</span> Father's Mobile No.</label>
                                                <asp:TextBox ID="txtFatherMobile" CssClass="form-control" runat="server" TabIndex="23" ToolTip="Please Enter Father's Mobile No"
                                                    MaxLength="12" />
                                                <ajaxToolKit:FilteredTextBoxExtender ID="fteFatherMobile" runat="server" FilterType="Numbers"
                                                    TargetControlID="txtFatherMobile">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                <asp:RequiredFieldValidator ID="rfvtxtMobileNo" runat="server" ControlToValidate="txtFatherMobile"
                                                    Display="None" ErrorMessage="Please Enter Father's Mobile No." SetFocusOnError="True"
                                                    ValidationGroup="Academic"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator runat="server" ErrorMessage="Father's Mobile No. is Invalid" ID="RegularExpressionValidator1" ControlToValidate="txtFatherMobile" ValidationExpression=".{10}.*"
                                                    Display="None" ValidationGroup="Academic"></asp:RegularExpressionValidator>
                                            </div>
                                            <div class="form-group col-md-4">
                                                <label><span style="color: red;">&nbsp;</span> Father's Office Phone No.</label>
                                                <asp:TextBox ID="txtFathersOfficeNo" CssClass="form-control" runat="server" TabIndex="24" MaxLength="15" ToolTip="Please Enter Father's Office Phone No"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="fteTxtPin" runat="server" FilterType="Numbers"
                                                    TargetControlID="txtFathersOfficeNo">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                <asp:RegularExpressionValidator runat="server" ErrorMessage="Father's Office Phone No. is Invalid" ID="revMobile" ControlToValidate="txtFathersOfficeNo" ValidationExpression=".{10}.*"
                                                    Display="None" ValidationGroup="Academic"></asp:RegularExpressionValidator>
                                            </div>
                                            <div class="form-group col-md-4">
                                                <label><span style="color: red;">&nbsp;</span> Father Qualification</label>
                                                <asp:TextBox ID="txtFatherDesignation" CssClass="form-control" runat="server" ToolTip="Please Enter Father's Qualification" onkeypress="return alphaOnly(event);"
                                                    TabIndex="25"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
                                                    TargetControlID="txtFatherDesignation" FilterType="Custom" FilterMode="InvalidChars"
                                                    InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                            </div>
                                        </div>
                                        <div class="col-md-12">

                                            <div class="form-group col-md-4">
                                                <label for="city"><span style="color: red;">&nbsp;</span> Father Occupation</label>
                                                <asp:DropDownList ID="ddlOccupationNo" CssClass="form-control" runat="server" AppendDataBoundItems="True"
                                                    ToolTip="Please Select Father's Occupation" TabIndex="26">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-md-4">
                                                <label for="city"><span style="color: red;">&nbsp;</span> Father's Email</label>

                                                <asp:TextBox ID="txtfatheremailid" CssClass="form-control" runat="server" TabIndex="27" ToolTip="Please Enter Father's Email" Enabled="true" />
                                                <asp:RegularExpressionValidator ID="rfvfathEmail" runat="server" ControlToValidate="txtfatheremailid"
                                                    Display="None" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                    ErrorMessage="Please Enter Valid EmailID" ValidationGroup="academic"></asp:RegularExpressionValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtfatheremailid"
                                                    Display="None" ErrorMessage="Please Enter Father's Email " ValidationGroup="academic"
                                                    SetFocusOnError="true">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-group col-md-4">
                                                <label for="city"><span style="color: red;">*</span> Annual Income</label>
                                                <asp:TextBox ID="txtAnnualIncome" CssClass="form-control" runat="server" TabIndex="28" MaxLength="12" ToolTip="Please Enter Annual Income"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvAnnualincome" runat="server" ControlToValidate="txtAnnualIncome"
                                                    Display="None" ErrorMessage="Please Enter Annual Income" SetFocusOnError="True"
                                                    ValidationGroup="Academic"></asp:RequiredFieldValidator>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="fteAnnualIncome" runat="server" FilterType="Numbers"
                                                    TargetControlID="txtAnnualIncome">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div style="text-align: left; height: 50%; width: 100%; background-color: #d9edf7">
                                                <h4><b>Mother Details</b></h4>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group col-md-4">
                                                <label><span style="color: red;">&nbsp;</span> Mother's Name</label>
                                                <asp:TextBox ID="txtMotherName" CssClass="form-control" runat="server" TabIndex="29" ToolTip="Please Enter Mother's Name" onblur="conver_uppercase(this);" onkeyup="conver_uppercase(this);" Enabled="true" />
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                                    TargetControlID="txtMotherName" FilterType="Custom" FilterMode="InvalidChars"
                                                    InvalidChars="1234567890" />

                                            </div>
                                            <div class="form-group col-md-4">
                                                <label><span style="color: red;">&nbsp;</span> Mother's Mobile No.</label>
                                                <asp:TextBox ID="txtMotherMobile" CssClass="form-control" runat="server" TabIndex="30" ToolTip="Please Enter Mother's Mobile No"
                                                    MaxLength="12" />
                                                <ajaxToolKit:FilteredTextBoxExtender ID="fteMotherMobile" runat="server" FilterType="Numbers"
                                                    TargetControlID="txtMotherMobile">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                <asp:RegularExpressionValidator runat="server" ErrorMessage="Mother's Mobile No. is Invalid" ID="RegularExpressionValidator2" ControlToValidate="txtMotherMobile" ValidationExpression=".{10}.*"
                                                    Display="Dynamic" ValidationGroup="Academic"></asp:RegularExpressionValidator>
                                            </div>
                                            <div class="form-group col-md-4">
                                                <label for="city"><span style="color: red;">&nbsp;</span> Mother's Email</label>
                                                <asp:TextBox ID="txtmotheremailid" CssClass="form-control" runat="server" TabIndex="31" ToolTip="Please Enter Mother's Email" Enabled="true" />

                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group col-md-4">
                                                <label><span style="color: red;">&nbsp;</span> Mother Qualification</label>
                                                <asp:TextBox ID="txtMotherDesignation" runat="server" TabIndex="32" CssClass="form-control" ToolTip="Please Enter Mother's Qualification" onkeypress="return alphaOnly(event);"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server"
                                                    TargetControlID="txtMotherDesignation" FilterType="Custom" FilterMode="InvalidChars"
                                                    InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                            </div>
                                            <div class="form-group col-md-4">
                                                <label for="city"><span style="color: red;">&nbsp;</span> Mother Occupation</label>
                                                <asp:DropDownList ID="ddlMotherOccupation" CssClass="form-control" runat="server" TabIndex="33" AppendDataBoundItems="True"
                                                    ToolTip="Please Select Mother's Occupation">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-md-4">
                                                <label><span style="color: red;">&nbsp;</span> Mother's Office Phone No</label>
                                                <asp:TextBox ID="txtMothersOfficeNo" CssClass="form-control" runat="server" TabIndex="34" MaxLength="15" ToolTip="Mother's Office Phone No"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="fteMotherOfficeNum" runat="server" FilterType="Numbers"
                                                    TargetControlID="txtMothersOfficeNo">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                <asp:RegularExpressionValidator runat="server" ErrorMessage="Mother's Office Phone No is Invalid" ID="RegularExpressionValidator3" ControlToValidate="txtMothersOfficeNo" ValidationExpression=".{10}.*"
                                                    Display="None" ValidationGroup="Academic"></asp:RegularExpressionValidator>
                                            </div>

                                        </div>

                                        <div class="col-md-12">
                                            <div style="text-align: left; height: 50%; width: 100%; background-color: #d9edf7">
                                                <h4><b>Photo & Signature Details</b></h4>
                                            </div>
                                        </div>
                                        <div class="form-group col-md-12" style="color: Red; font-weight: bold">

                                            <span style="color: black">Note :</span>  Only JPG,JPEG,PNG files are allowed upto 40 KB size For Photo and Signature
                                              
                                        </div>
                                        <br />
                                        <div class="form-group col-md-12">
                                            <div class="form-group col-md-6">
                                                <div class="panel panel-info">
                                                    <div class="panel-body" style="box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);">
                                                        <div style="color: Red; font-weight: bold">
                                                            Only Passport Size Photo Allowed
                                                        </div>
                                                        <br />
                                                        <label>Photo :</label>


                                                        <asp:Image ID="imgPhoto" runat="server" Width="128px" Height="128px" />
                                                        <asp:FileUpload ID="fuPhotoUpload" runat="server" TabIndex="35" onchange="previewFilePhoto()" />
                                                        <asp:Button ID="btnPhotoUpload" runat="server" CssClass="btn btn-outline-info" Text="Upload" TabIndex="35" OnClick="btnPhotoUpload_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group col-md-6">
                                                <br />
                                                <br />
                                                <br />
                                                <div class="panel panel-info">
                                                    <div class="panel-body" style="box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);">
                                                        <label>Signature :</label>
                                                        <asp:Image ID="ImgSign" runat="server" Width="150px" Height="40px" />
                                                        <asp:FileUpload ID="fuSignUpload" runat="server" onChange="previewFilePhoto2()" TabIndex="36" />
                                                        <asp:Button ID="btnSignUpload" CssClass="btn btn-outline-info" runat="server" Text="Upload" TabIndex="37" OnClick="btnSignUpload_Click" />
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="box box-footer text-center">
                                    <asp:Button ID="btnSubmit" runat="server" TabIndex="38" Text="Save & Continue >>" ToolTip="Click to Submit"
                                        class="btn btn-outline-primary" OnClick="btnSubmit_Click" ValidationGroup="Academic" />
                                    &nbsp
                       
                                          
                                    <button runat="server" id="btnGohome" visible="false" tabindex="39" onserverclick="btnGohome_Click" class="btn btn-outline-danger btnGohome" tooltip="Click to Go Back Home">
                                        <i class="fa fa-home"></i>Go Back Home
                                    </button>

                                    &nbsp;<asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True"
                                        ShowSummary="False" ValidationGroup="Academic" />

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btnPhotoUpload" />
            <asp:PostBackTrigger ControlID="btnSignUpload" />


        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript" language="javascript">
        function LoadImage() {
            document.getElementById("ctl00_ContentPlaceHolder1_imgPhoto").src = document.getElementById("ctl00_ContentPlaceHolder1_fuPhotoUpload").value;
            document.getElementById("ctl00_ContentPlaceHolder1_imgPhoto").height = '96px';
            document.getElementById("ctl00_ContentPlaceHolder1_imgPhoto").width = '96px';
        }
        function conver_uppercase(text) {
            text.value = text.value.toUpperCase();
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
    </script>
    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>

    <script type="text/javascript">
        function previewFilePhoto() {
            debugger;
            var preview = document.querySelector('#<%=imgPhoto.ClientID %>');
            var file = document.querySelector('#<%=fuPhotoUpload.ClientID %>').files[0];
            var reader = new FileReader();

            reader.onloadend = function () {
                preview.src = reader.result;
            }

            if (file) {
                reader.readAsDataURL(file);
            } else {
                preview.src = "";
            }
        }

        function previewFilePhoto2() {
            var preview = document.querySelector('#<%=ImgSign.ClientID %>');
            var file = document.querySelector('#<%=fuSignUpload.ClientID %>').files[0];
            var reader = new FileReader();

            reader.onloadend = function () {
                preview.src = reader.result;
            }

            if (file) {
                reader.readAsDataURL(file);
            } else {
                preview.src = "";
            }
        }
    </script>
    <script type="text/javascript">
        function pageLoad() {

            function previewFilePhoto() {
                var preview = document.querySelector('#<%=imgPhoto.ClientID %>');
                var file = document.querySelector('#<%=fuPhotoUpload.ClientID %>').files[0];
                var reader = new FileReader();

                reader.onloadend = function () {
                    preview.src = reader.result;
                }

                if (file) {
                    reader.readAsDataURL(file);
                } else {
                    preview.src = "";
                }
            }

            function previewFilePhoto2() {
                var preview = document.querySelector('#<%=ImgSign.ClientID %>');
                var file = document.querySelector('#<%=fuSignUpload.ClientID %>').files[0];
                var reader = new FileReader();

                reader.onloadend = function () {
                    preview.src = reader.result;
                }

                if (file) {
                    reader.readAsDataURL(file);
                } else {
                    preview.src = "";
                }
            }


        }
    </script>
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

    <style id="cssStyle" type="text/css" media="all">
        .CS
        {
            background-color: #abcdef;
            color: Yellow;
            border: 1px solid #AB00CC;
            font: Verdana 10px;
            padding: 1px 4px;
            font-family: Palatino Linotype, Arial, Helvetica, sans-serif;
        }
    </style>
    <div id="divMsg" runat="server">
    </div>

</asp:Content>
