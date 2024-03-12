<%@ Page Language="C#" Title="" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="StudentNewEnquiry.aspx.cs" Inherits="ACADEMIC_StudentNewEnquiry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../../../plugins/multiselect/bootstrap-multiselect.css" rel="stylesheet" />

    <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
    <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updenquiry"
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
    <asp:UpdatePanel ID="updenquiry" runat="server">
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
                                            <asp:Label ID="lblfirstname" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" MaxLength="20" onblur="checkLength(this)" TabIndex="1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvfullname" runat="server" ControlToValidate="txtFullName"
                                            ErrorMessage="First Name Required !" Display="None" ValidationGroup="Register"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftbGrade" runat="server" TargetControlID="txtFullName"
                                            InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}0123456789[]\|-&&quot;'" FilterMode="InvalidChars" />

                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblLastname" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" MaxLength="20" onblur="checkLength(this)" TabIndex="2"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtLastName"
                                            ErrorMessage="Last Name Required !" Display="None" ValidationGroup="Register"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtLastName"
                                            InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}0123456789[]\|-&&quot;'" FilterMode="InvalidChars" />

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblEmail" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:TextBox ID="txtEmailid" runat="server" CssClass="form-control" TabIndex="3"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvemail" runat="server" ControlToValidate="txtEmailid"
                                            ErrorMessage="Email ID Required !" Display="None" ValidationGroup="Register"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="rfvuserEmail" runat="server" ControlToValidate="txtEmailid"
                                            Display="None" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                            ErrorMessage="Please Enter Valid EmailID" ValidationGroup="Register"></asp:RegularExpressionValidator>

                                        <ajaxToolKit:FilteredTextBoxExtender ID="ftbxemailid" runat="server" TargetControlID="txtEmailid"
                                            InvalidChars="ABCDEFGHIJKLNMOPQRSTUVWXYZ" FilterMode="InvalidChars" />

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblStudentMobileNumber" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon" style="border-color: transparent; padding: 0px; border-bottom: 1px solid transparent;">
                                                <asp:DropDownList ID="ddlMobileCode" runat="server" AppendDataBoundItems="true" CssClass="form-control select2 select-clik" Style="padding-right: 0px!important;" TabIndex="4" onchange="return RemoveCountryName()">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control ml-2" MaxLength="10" TabIndex="5"></asp:TextBox>

                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtMobileNo" ValidChars="1234567890" FilterMode="ValidChars" />
                                            <asp:RequiredFieldValidator ID="rfvmobileno" runat="server" ErrorMessage="Mobile No. Required !"
                                                ControlToValidate="txtMobileNo" Display="None" ValidationGroup="Register"></asp:RequiredFieldValidator>

                                            <asp:RegularExpressionValidator runat="server" ErrorMessage="Mobile No. is Invalid"
                                                ID="revMobile" ControlToValidate="txtMobileNo" ValidationExpression=".{10}.*"
                                                Display="None" ValidationGroup="Register"></asp:RegularExpressionValidator>
                                            <asp:RequiredFieldValidator ID="rfvmobilecode" runat="server" ErrorMessage="Mobile Code Required !"
                                                ControlToValidate="ddlMobileCode" Display="None" ValidationGroup="Register" InitialValue="0"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <asp:Label ID="lblhometel" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon" style="border-color: transparent; padding: 0px; border-bottom: 1px solid transparent;">
                                                <asp:DropDownList ID="ddlHomeTelMobileCode" runat="server" AppendDataBoundItems="true" CssClass="form-control select2 select-clik" Style="padding-right: 0px!important;" TabIndex="6" onchange="return RemoveHomeCountryName()">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <asp:TextBox ID="txtHomeMobileNo" runat="server" CssClass="form-control ml-2" MaxLength="10" TabIndex="7"></asp:TextBox>

                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtHomeMobileNo" ValidChars="1234567890" FilterMode="ValidChars" />
                                            <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Home Telephone No. Required !"
                                                            ControlToValidate="txtHomeMobileNo" Display="None" ValidationGroup="Register"></asp:RequiredFieldValidator>--%>

                                            <asp:RegularExpressionValidator runat="server" ErrorMessage="Home Telephone No. is Invalid"
                                                ID="RegularExpressionValidator1" ControlToValidate="txtHomeMobileNo" ValidationExpression=".{10}.*"
                                                Display="None" ValidationGroup="Register"></asp:RegularExpressionValidator>
                                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Home Telephone Code Required !"
                                                            ControlToValidate="ddlHomeTelMobileCode" Display="None" ValidationGroup="Register" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <asp:Label ID="lbldob" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i id="dvcal1" runat="server" class="fa fa-calendar"></i>
                                            </div>

                                            <asp:TextBox ID="txtDob" runat="server" CssClass="form-control dob" TabIndex="8"></asp:TextBox>

                                            <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Date of Birth Required !"
                                                            ControlToValidate="txtDob" Display="None" ValidationGroup="Register"></asp:RequiredFieldValidator>--%>
                                        </div>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblSelectGender" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <%--<label class="form-check-label">--%>
                                        <asp:RadioButtonList ID="rdGender" runat="server" RepeatDirection="Horizontal" class="form-check-label" TabIndex="9">
                                            <asp:ListItem Value="0">M &nbsp;</asp:ListItem>
                                            <asp:ListItem Value="1">F &nbsp;</asp:ListItem>
                                            <%-- <asp:ListItem Value="2">Other &nbsp;</asp:ListItem>--%>
                                        </asp:RadioButtonList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Gender Required !" ControlToValidate="rdGender" Display="None" ValidationGroup="Register"></asp:RequiredFieldValidator>
                                        <%--</label>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <%--  <sup>* </sup>  --%>
                                            <asp:Label ID="lblpassportno" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:TextBox ID="txtPassport" runat="server" CssClass="form-control" TabIndex="11" MaxLength="20" onblur="return Validator();"></asp:TextBox>


                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <%--  <sup>* </sup>  --%>
                                            <asp:Label ID="lblnic" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:TextBox ID="txtNIC" runat="server" CssClass="form-control" TabIndex="12" MaxLength="20" onblur="return Validator();"></asp:TextBox>


                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDyAdmissionType" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlStudyType" runat="server" AppendDataBoundItems="true" CssClass="form-control select2 select-clik" AutoPostBack="true" OnSelectedIndexChanged="ddlStudyType_SelectedIndexChanged" TabIndex="10">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Study level Required !"
                                            ControlToValidate="ddlStudyType" Display="None" ValidationGroup="Register" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblProgram" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:ListBox ID="ddlProgramIntrested" runat="server" CssClass="form-control multi-select-demo"
                                            SelectionMode="Multiple" AppendDataBoundItems="true" TabIndex="13" OnSelectedIndexChanged="ddlProgramIntrested_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Program Interested in Required !"
                                            ControlToValidate="ddlProgramIntrested" Display="None" ValidationGroup="Register"></asp:RequiredFieldValidator>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblPrPrefere" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:ListBox ID="ddlPreference" runat="server" CssClass="form-control multi-select-demo"
                                            SelectionMode="Multiple" TabIndex="14"></asp:ListBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Program Preference Required !"
                                            ControlToValidate="ddlPreference" Display="None" ValidationGroup="Register"></asp:RequiredFieldValidator>

                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblSource" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSource" runat="server" AppendDataBoundItems="true" CssClass="form-control select2 select-clik" Style="padding-right: 0px!important;" TabIndex="15">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Type of Source Required !"
                                            ControlToValidate="ddlSource" Display="None" ValidationGroup="Register" InitialValue="0"></asp:RequiredFieldValidator>

                                    </div>

                                    <div class="col-12">
                                        <div class="form-check">
                                            <asp:CheckBox ID="chkConfirm" type="checkbox" class="form-check-input" runat="server" TabIndex="15" />
                                            <label class="form-check-label" for="chkConfirm">I confirm that the above information is correct.</label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="btn btn-outline-info btnX" OnClick="btnRegister_Click" ValidationGroup="Register" TabIndex="16" />
                                <asp:ValidationSummary ID="val_summary" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Register" DisplayMode="List" />
                            </div>
                        </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="modal fade" id="confirm">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">

                <!-- Modal Header -->
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>

                <!-- Modal body -->
                <div class="modal-body text-center">
                    <asp:Label ID="lbluserMsg" runat="server" Visible="true"></asp:Label>
                    Account already exist
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
        });
    </script>
    <script>
        $(document).ready(function () {
            var dateval = document.getElementById('<%=txtDob.ClientID%>').value;
            var prev_date = new Date();
            prev_date.setDate(prev_date.getDate() - 1);
            $('.dob').daterangepicker({
                singleDatePicker: true,
                showDropdowns: true,
                maxDate: prev_date,
                locale: {
                    format: 'DD/MM/YYYY'
                },
                autoApply: true,
            });
            if (dateval == "") {
                $('.dob').val('');
            }
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                var dateval = document.getElementById('<%=txtDob.ClientID%>').value;
                var prev_date = new Date();
                prev_date.setDate(prev_date.getDate() - 1);
                $('.dob').daterangepicker({
                    singleDatePicker: true,
                    showDropdowns: true,
                    maxDate: prev_date,
                    locale: {
                        format: 'DD/MM/YYYY'
                    },
                    autoApply: true,
                });
                if (dateval == "") {
                    $('.dob').val('');
                }
            });
        });
    </script>
    
</asp:Content>



