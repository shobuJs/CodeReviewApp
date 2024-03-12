<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Data_Correction_Facility.aspx.cs" Inherits="Academic_Data_Correction_Facility" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UpdDataCorrection"
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

    <asp:UpdatePanel ID="UpdDataCorrection" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title"><span>Data Correction Facility</span></h3>
                        </div>

                        <div class="box-body">
                            <!-- Page Main Inputs Content -->

                            <div class="col-12 btn-footer">
                                <b><span class="description">Select Student : </span></b>
                                <asp:TextBox ID="txtStudentName" runat="server" ReadOnly="true" Width="300"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvStudentName" runat="server" ControlToValidate="txtStudentName"
                                    Display="None" ErrorMessage="Please Select Student Name" ValidationGroup="searchstudent" />
                                <span id="SearchPopUp" class="btn btn-outline-info"><i class="fa fa-search"></i></span>
                            </div>
                            <div class="row" id="Category" runat="server" visible="false">
                                <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                    <div class="form-group">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label><span>Category</span></label>
                                        </div>
                                        <asp:DropDownList ID="ddlCategory" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="true" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="0">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Personal Details</asp:ListItem>
                                            <asp:ListItem Value="2">Address Details</asp:ListItem>
                                            <asp:ListItem Value="3">Educational Details</asp:ListItem>
                                            <asp:ListItem Value="4">Program & Campus Details</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="row" id="Personal_Details" runat="server" visible="false">
                                <div class="sub-heading col-12">
                                    <h5>Personal Details</h5>
                                </div>

                                <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                    <div class="form-group">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label><span>First Name</span></label>
                                        </div>
                                        <asp:TextBox ID="TxtFirstName" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="TxtFirstName"
                                            Display="None" ErrorMessage="Please Enter First Name"
                                            ValidationGroup="Personal" />
                                    </div>
                                </div>

                                <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                    <div class="form-group">
                                        <div class="label-dynamic">
                                            <%--  <sup>* </sup>--%>
                                            <label><span>Middle Name</span></label>
                                        </div>
                                        <asp:TextBox ID="TxtMiddleName" runat="server" CssClass="form-control"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="TxtMiddleName"
                                            Display="None" ErrorMessage="Please Enter Middle Name"
                                            ValidationGroup="Personal" />--%>
                                    </div>
                                </div>

                                <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                    <div class="form-group">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label><span>Last Name</span></label>
                                        </div>
                                        <asp:TextBox ID="TxtLastName" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="TxtLastName"
                                            Display="None" ErrorMessage="Please Enter Last Name"
                                            ValidationGroup="Personal" />
                                    </div>
                                </div>

                                <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                    <div class="form-group">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label><span>Gender</span></label>
                                        </div>
                                        <asp:RadioButtonList ID="rdoGender" runat="server" TabIndex="13" RepeatDirection="Horizontal" meta:resourcekey="rdoGenderResource1">
                                            <asp:ListItem Text="&nbsp;Male" Value="1" meta:resourcekey="ListItemResource3"></asp:ListItem>
                                            <asp:ListItem Text="&nbsp;Female" Value="2" meta:resourcekey="ListItemResource4"></asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="rdoGender"
                                            Display="None" ErrorMessage="Please Select Gender" InitialValue=""
                                            ValidationGroup="Personal" />
                                    </div>
                                </div>

                                <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                    <div class="form-group">
                                        <div class="label-dynamic">
                                            <%--  <sup>* </sup>--%>
                                            <label><span>Religion</span></label>
                                        </div>
                                        <asp:DropDownList ID="ddlReligion" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="0">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="ddlReligion"
                                            Display="None" ErrorMessage="Please Select Religion" InitialValue="0"
                                            ValidationGroup="Personal" />--%>
                                    </div>
                                </div>

                                <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                    <div class="form-group">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label><span>Date of Birth</span></label>
                                        </div>
                                        <asp:TextBox ID="txtDateOfBirth" runat="server" TabIndex="12" CssClass="form-control"
                                            ToolTip="Please Enter Date Of Birth" placeholder="DD/MM/YYYY" type="date" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txtDateOfBirth"
                                            Display="None" ErrorMessage="Please Enter Date of Birth"
                                            ValidationGroup="Personal" />
                                    </div>
                                </div>

                                <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                    <div class="form-group">
                                        <div class="label-dynamic">
                                            <%--  <sup>* </sup>--%>
                                            <label><span>Place of Birth</span></label>
                                        </div>
                                        <asp:TextBox ID="TxtPlaceofBirth" runat="server" CssClass="form-control"></asp:TextBox>
                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="TxtPlaceofBirth"
                                            Display="None" ErrorMessage="Please Enter Place of Birth"
                                            ValidationGroup="Personal" />--%>
                                    </div>
                                </div>

                                <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                    <div class="form-group">
                                        <div class="label-dynamic">
                                            <%-- <sup>* </sup>--%>
                                            <label><span>Citizenship</span></label>
                                        </div>
                                        <asp:RadioButtonList ID="RadioCitizen" runat="server" TabIndex="13" RepeatDirection="Horizontal" meta:resourcekey="rdoGenderResource1">
                                            <asp:ListItem Text="&nbsp;PHILIPPINES" Value="2" meta:resourcekey="ListItemResource4"></asp:ListItem>
                                            <asp:ListItem Text="&nbsp;FOREIGN" Value="1" meta:resourcekey="ListItemResource3"></asp:ListItem>

                                        </asp:RadioButtonList>
                                        <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="RadioCitizen"
                                            Display="None" ErrorMessage="Please Select Citizenship" InitialValue=""
                                            ValidationGroup="Personal" />--%>
                                    </div>
                                </div>

                                <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                    <div class="form-group">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label><span>Mobile No.</span></label>
                                        </div>
                                        <div class="input-group">
                                            <span class="input-group-prepend" style="width: 100px! important;">
                                                <asp:DropDownList ID="ddlMobileCodeStudent" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="1">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ErrorMessage="Please Select Mobile Code."
                                                    ControlToValidate="ddlMobileCodeStudent" Display="None" SetFocusOnError="true" InitialValue="0" ValidationGroup="sub" />
                                                <asp:TextBox ID="txtmobilecode" TabIndex="9" runat="server" MaxLength="5" CssClass="form-control" Enabled="false" Visible="false"></asp:TextBox>
                                            </span>
                                            <asp:TextBox ID="TxtMobileNo" runat="server" CssClass="form-control ml-2 pb-0 pl-0 pr-0" MaxLength="15"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                    <div class="form-group">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label><span>Email</span></label>
                                        </div>
                                        <asp:TextBox ID="TxtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="TxtEmail"
                                            Display="None" ErrorMessage="Please Enter Email"
                                            ValidationGroup="Personal" />
                                    </div>
                                </div>

                                <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                    <div class="form-group">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label><span>Passport No.</span></label>
                                        </div>
                                        <asp:TextBox ID="TxtPassNo" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                    <div class="form-group">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label><span>ACR No.</span></label>
                                        </div>
                                        <asp:TextBox ID="TxtAcrNo" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                    <div class="form-group">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label><span>ACR Date Issue</span></label>
                                        </div>
                                        <asp:TextBox ID="TxtDateIssue" name="acr" runat="server" TabIndex="12" CssClass="form-control" type="date"
                                            placeholder="DD/MM/YYYY" />
                                    </div>
                                </div>

                                <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                    <div class="form-group">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label><span>ACR Place of Issue</span></label>
                                        </div>
                                        <asp:TextBox ID="TxtPlaceIssue" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                    <div class="form-group">
                                        <div class="label-dynamic">
                                            <%-- <sup>* </sup>--%>
                                            <label><span>Father's Name</span></label>
                                        </div>
                                        <asp:TextBox ID="TxtFathername" runat="server" CssClass="form-control"></asp:TextBox>
                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="TxtFathername"
                                            Display="None" ErrorMessage="Please Enter Father's Name"
                                            ValidationGroup="Personal" />--%>
                                    </div>
                                </div>

                                <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                    <div class="form-group">
                                        <div class="label-dynamic">
                                            <%--    <sup>* </sup>--%>
                                            <label><span>Father's Occupation</span></label>
                                        </div>
                                        <asp:DropDownList ID="ddlOccupationFather" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="0">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="ddlOccupationFather"
                                            Display="None" ErrorMessage="Please Select Father's Occupation" InitialValue="0"
                                            ValidationGroup="Personal" />--%>
                                    </div>
                                </div>

                                <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                    <div class="form-group">
                                        <div class="label-dynamic">
                                            <%--  <sup>* </sup>--%>
                                            <label><span>Mother's Name</span></label>
                                        </div>
                                        <asp:TextBox ID="TxtMotherName" runat="server" CssClass="form-control"></asp:TextBox>
                                        <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ControlToValidate="TxtMotherName"
                                            Display="None" ErrorMessage="Please Select Mother's Name"
                                            ValidationGroup="Personal" />--%>
                                    </div>
                                </div>

                                <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                    <div class="form-group">
                                        <div class="label-dynamic">
                                            <%--<sup>* </sup>--%>
                                            <label><span>Mother's Occupation</span></label>
                                        </div>
                                        <asp:DropDownList ID="ddlOccupationFMother" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="0">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ControlToValidate="ddlOccupationFMother"
                                            Display="None" ErrorMessage="Please Select Mother's Occupation" InitialValue="0"
                                            ValidationGroup="Personal" />--%>
                                    </div>
                                </div>

                                <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                    <div class="form-group">
                                        <div class="label-dynamic">
                                            <%-- <sup>* </sup>--%>
                                            <label><span>Annual Income</span></label>
                                        </div>
                                        <asp:DropDownList ID="ddlCapitaIncome" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="0">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ControlToValidate="ddlCapitaIncome"
                                            Display="None" ErrorMessage="Please Select Annual Income" InitialValue="0"
                                            ValidationGroup="Personal" />--%>
                                    </div>
                                </div>
                                <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                    <div class="form-group">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label><span>Student Type</span></label>
                                        </div>
                                        <asp:DropDownList ID="ddlStudenttype" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="0">
                                            <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                            <asp:ListItem Value="0">Freshman</asp:ListItem>
                                            <asp:ListItem Value="1">Transferee</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <!-- Buttons -->
                                <div class="col-12 btn-footer mt-2 mb-3">
                                    <asp:Button ID="btnSubmitPersonalDetails" runat="server" ValidationGroup="Personal" Text="Submit" ToolTip="Submit" OnClick="btnSubmitPersonalDetails_Click" CssClass="btn btn-outline-info" />
                                    <asp:Button ID="btnCancelPersonalDetails" runat="server" Text="Cancel" ToolTip="Cancel" OnClick="btnCancelPersonalDetails_Click" CssClass="btn btn-outline-danger" />
                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Personal"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </div>
                            </div>

                            <div class="row" id="Address_Details" runat="server" visible="false">
                                <div class="sub-heading col-12">
                                    <h5>Current Address</h5>
                                </div>
                                <div class="col-xl-6 col-lg-8 col-sm-6 col-12">
                                    <div class="form-group">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label><span>Address</span></label>
                                        </div>
                                        <asp:TextBox ID="txtCurrentAddress" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtCurrentAddress"
                                            Display="None" ErrorMessage="Please Enter Current Address"
                                            ValidationGroup="Address" />
                                    </div>
                                </div>

                                <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                    <div class="form-group">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label><span>Country</span></label>
                                        </div>
                                        <asp:DropDownList ID="ddlCurrentCountry" runat="server" OnSelectedIndexChanged="ddlCurrentCountry_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="true" CssClass="form-control" data-select2-enable="true" TabIndex="0">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCurrentCountry" InitialValue="0"
                                            Display="None" ErrorMessage="Please Select Current Country"
                                            ValidationGroup="Address" />
                                    </div>
                                </div>

                                <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                    <div class="form-group">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label><span>Province</span></label>
                                        </div>
                                        <asp:DropDownList ID="ddlCurrentProvince" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlCurrentProvince_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" data-select2-enable="true" TabIndex="0">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlCurrentProvince"
                                            Display="None" ErrorMessage="Please Select Current Province" InitialValue="0"
                                            ValidationGroup="Address" />
                                    </div>
                                </div>

                                <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                    <div class="form-group">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label><span>City/Village</span></label>
                                        </div>
                                        <asp:DropDownList ID="ddlCurrentCity" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="0">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlCurrentCity"
                                            Display="None" ErrorMessage="Please Select Current City/Village" InitialValue="0"
                                            ValidationGroup="Address" />
                                    </div>
                                </div>

                                <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                    <div class="form-group">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label><span>ZIP/PIN</span></label>
                                        </div>
                                        <asp:TextBox ID="txtCurrentPin" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtCurrentPin"
                                            Display="None" ErrorMessage="Please Enter Current ZIP/PIN"
                                            ValidationGroup="Address" />
                                    </div>
                                </div>

                                <div class="sub-heading col-12 mt-3">
                                    <h5>Permanent Address</h5>
                                </div>
                                <div class="col-12">
                                    <div class="form-group">
                                        <div class="label-dynamic">
                                            <%--        <sup>* </sup>--%>
                                            <label><span>Same as Current Address ?</span></label>
                                        </div>
                                        <asp:CheckBox ID="chkcopy" runat="server" OnCheckedChanged="chkcopy_CheckedChanged" AutoPostBack="true" />
                                    </div>
                                </div>
                                <div class="col-xl-6 col-lg-8 col-sm-6 col-12">
                                    <div class="form-group">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label><span>Address</span></label>
                                        </div>
                                        <asp:TextBox ID="txtPermAddress" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtPermAddress"
                                            Display="None" ErrorMessage="Please Enter Permanent Address"
                                            ValidationGroup="Address" />
                                    </div>
                                </div>

                                <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                    <div class="form-group">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label><span>Country</span></label>
                                        </div>
                                        <asp:DropDownList ID="ddlPCon" runat="server" OnSelectedIndexChanged="ddlPCon_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="0">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlPCon"
                                            Display="None" ErrorMessage="Please Select Permanent Country" InitialValue="0"
                                            ValidationGroup="Address" />
                                    </div>
                                </div>

                                <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                    <div class="form-group">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label><span>Province</span></label>
                                        </div>
                                        <asp:DropDownList ID="ddlPermanentState" OnSelectedIndexChanged="ddlPermanentState_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="0">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlPermanentState"
                                            Display="None" ErrorMessage="Please Select Permanent Province" InitialValue="0"
                                            ValidationGroup="Address" />
                                    </div>
                                </div>

                                <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                    <div class="form-group">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label><span>City/Village</span></label>
                                        </div>
                                        <asp:DropDownList ID="ddlPermanentCity" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="0">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlPermanentCity"
                                            Display="None" ErrorMessage="Please Select Permanent City/Village" InitialValue="0"
                                            ValidationGroup="Address" />
                                    </div>
                                </div>

                                <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                    <div class="form-group">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label><span>ZIP/PIN</span></label>
                                        </div>
                                        <asp:TextBox ID="txtPermPIN" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtPermPIN"
                                            Display="None" ErrorMessage="Please Enter Permanent ZIP/PIN"
                                            ValidationGroup="Address" />
                                    </div>
                                </div>

                                <!-- Buttons -->
                                <div class="col-12 btn-footer mt-2 mb-3">
                                    <asp:Button ID="btnSubmitAddress" runat="server" Text="Submit" ValidationGroup="Address" ToolTip="Submit" OnClick="btnSubmitAddress_Click" CssClass="btn btn-outline-info" />
                                    <asp:Button ID="btnCancelAddress" runat="server" Text="Cancel" ToolTip="Cancel" OnClick="btnCancelAddress_Click" CssClass="btn btn-outline-danger" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Address"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </div>
                            </div>

                            <div class="row" id="Educational_Details" runat="server" visible="false">
                                <div class="sub-heading col-12">
                                    <h5>Educational Details</h5>
                                </div>
                                <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                    <div class="form-group">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label><span>Study Level</span></label>
                                        </div>
                                        <asp:DropDownList ID="ddlProgramType" Enabled="false" OnSelectedIndexChanged="ddlProgramType_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="0">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="table table-responsive">
                                        <asp:Panel ID="pnlSession" runat="server">
                                            <asp:ListView ID="lvLevellist" runat="server">
                                                <LayoutTemplate>
                                                    <table class="table table-bordered nowrap" style="width: 100%" id="divdepartmentlist">
                                                        <thead>
                                                            <tr class="bg-light-blue">
                                                                <th>Level</th>
                                                                <th>Name of School</th>
                                                                <th>Address</th>
                                                                <th>Region</th>
                                                                <th>Year Attended</th>
                                                                <th>Type</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>

                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><%# Eval("UA_SECTIONNAME") %>
                                                            <asp:Label runat="server" ID="lblSectionNo" Text='<%#Eval("UA_SECTION") %>' Visible="false"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtNameOfSchool" runat="server" Text='<%#Eval("SCHOOL_NAME") %>' TabIndex="1" CssClass="form-control" MaxLength="50" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtAddress" runat="server" Text='<%#Eval("SCHOOL_ADDRESS") %>' TabIndex="2" onblur="checkLength(this)" CssClass="md-textarea form-control" TextMode="MultiLine" Rows="1" MaxLength="100" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtRegion" Text='<%#Eval("SCHOOL_REGION") %>' runat="server" TabIndex="3" CssClass="form-control" MaxLength="20" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtYearAttended" Text='<%#Eval("YEAR_ATTENDED") %>' MaxLength="4" onblur="return IsNumeric(this);" runat="server" TabIndex="4" CssClass="form-control" />
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlType" runat="server" TabIndex="1" CssClass="form-control">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                <asp:ListItem Value="1">Public </asp:ListItem>
                                                                <asp:ListItem Value="2">Private</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:Label runat="server" ID="lblTypeNo" Text='<%#Eval("SCHOOL_TYPE_NO") %>' Visible="false"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>
                                </div>
                                <!-- Buttons -->
                                <div class="col-12 btn-footer mt-3 mb-3">
                                    <asp:Button ID="btnSubmitEducationalDetails" runat="server" Text="Submit" ToolTip="Submit" OnClick="btnSubmitEducationalDetails_Click" CssClass="btn btn-outline-info" />
                                    <asp:Button ID="btnCancelEducationalDetails" runat="server" Text="Cancel" ToolTip="Cancel" OnClick="btnCancelEducationalDetails_Click" CssClass="btn btn-outline-danger" />
                                </div>
                            </div>

                            <div class="row" id="Program_Campus_Details" runat="server" visible="false">

                                <div class="sub-heading col-12">
                                    <h5>Program & Campus Details</h5>
                                </div>
                                <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                    <div class="form-group">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label><span>Study Level</span></label>
                                        </div>
                                        <asp:DropDownList ID="ddlProgramTypes" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="0" OnSelectedIndexChanged="ddlProgramTypes_SelectedIndexChanged" AutoPostBack="true" Enabled="false">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-12" id="divfreshman" runat="server" visible="false">
                                    <div class="table table-responsive">
                                        <table class="table table-bordered" style="width: 100%" id="PrefeTab">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th style="width: 20%">Preference No.</th>
                                                    <th style="width: 80%">Preferences </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td style="width: 20%"><sup>*</sup> Preference 1 </td>
                                                    <td style="width: 80%">
                                                        <div class="md-form mb-0" id="divPreference1" runat="server">
                                                            <asp:DropDownList ID="ddlPrefrence1" runat="server" CssClass="form-control mb-0" data-select2-enable="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlPrefrence1_SelectedIndexChanged" AutoPostBack="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 20%"><sup></sup>Preference 2 </td>
                                                    <td style="width: 80%">
                                                        <div class="md-form mb-0" id="divPreference2" runat="server">
                                                            <asp:DropDownList ID="ddlPrefrence2" runat="server" CssClass="form-control mb-0" data-select2-enable="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlPrefrence2_SelectedIndexChanged" AutoPostBack="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 20%"><sup></sup>Preference 3 </td>
                                                    <td style="width: 80%">
                                                        <div class="md-form mb-0" id="divPreference3" runat="server">
                                                            <asp:DropDownList ID="ddlPrefrence3" runat="server" CssClass="form-control mb-0" data-select2-enable="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlPrefrence3_SelectedIndexChanged" AutoPostBack="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>

                                <div class="col-12 Transferee" id="divTransferee" runat="server">
                                    <div class="row">
                                        <div class="col-xl-3 col-lg-4 col-sm-6 col-12" id="divPreSchool" runat="server">
                                            <div class="form-group">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Previous School</label>
                                                </div>
                                                <asp:TextBox runat="server" ID="txtPreSchool" CssClass="form-control" spellcheck="true" placeholder="e.g. GPK"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="Please Enter Previous School !!"
                                                    ControlToValidate="txtPreSchool" Display="None" ValidationGroup="Branch" InitialValue=""></asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="col-xl-3 col-lg-4 col-sm-6 col-12" id="divPreProgram" runat="server">
                                            <div class="form-group">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Previous Program </label>
                                                </div>

                                                <asp:TextBox runat="server" ID="txtPreProgram" CssClass="form-control" spellcheck="true" placeholder="e.g. Bachelors of Science"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ErrorMessage="Please Enter Previous Program !!"
                                                    ControlToValidate="txtPreProgram" Display="None" ValidationGroup="Branch" InitialValue=""></asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="col-xl-3 col-lg-4 col-sm-6 col-12" id="divSemester" runat="server">
                                            <div class="form-group">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Apply for Year/Semester</label>
                                                </div>
                                                <asp:DropDownList ID="ddlSemester" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="6" AppendDataBoundItems="true" AutoPostBack="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ErrorMessage="Please Select Apply for Year/Semester !!"
                                                    ControlToValidate="ddlSemester" Display="None" ValidationGroup="Branch" InitialValue="0"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                            <div class="form-group">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Apply for Program</label>
                                                </div>

                                                <asp:DropDownList ID="ddlProgram" runat="server" class="form-control select2Task" data-select2-enable="true" spellcheck="true" AppendDataBoundItems="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <!-- Buttons -->
                                <div class="col-12 btn-footer mt-3 mb-3">
                                    <asp:Button ID="btnSubmitProgramCampus" runat="server" Text="Submit" ToolTip="Submit" OnClick="btnSubmitProgramCampus_Click" CssClass="btn btn-outline-info" />
                                    <asp:Button ID="btnCancelProgramCampus" runat="server" Text="Cancel" ToolTip="Cancel" OnClick="btnCancelProgramCampus_Click" CssClass="btn btn-outline-danger" />
                                </div>
                            </div>

                        </div>

                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmitEducationalDetails" />
        </Triggers>
    </asp:UpdatePanel>

    <div class="modal fade" id="ModelSearchPopup">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">

                <!-- Modal Header -->
                <div class="modal-header">
                    <h4 class="modal-title">Search</h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div>
                    <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="updSearchStudent"
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
                <asp:UpdatePanel ID="updSearchStudent" runat="server">
                    <ContentTemplate>
                        <!-- Modal body -->
                        <div class="modal-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-12 mb-3">
                                        <asp:RadioButtonList ID="rdSearch" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0">&nbsp;Student Name&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="1">&nbsp;Email&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="2">&nbsp;Mobile No.&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="3">&nbsp;Reg No.&nbsp;</asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please select Search Option"
                                            ControlToValidate="rdSearch" Display="None" ValidationGroup="search"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Enter Search String" MaxLength="32"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Enter Search String"
                                            ControlToValidate="txtSearch" Display="None" ValidationGroup="search"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-outline-info" OnClick="btnSearch_Click" ValidationGroup="search" />
                                        <asp:Button ID="btnClearSearch" runat="server" Text="Clear" CssClass="btn btn-outline-danger" OnClick="btnClearSearch_Click" />
                                        <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="search" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 mt-3">
                                <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
                                    <asp:ListView ID="lvStudent" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Student Details</h5>
                                            </div>
                                            <asp:Panel ID="Panel2" runat="server">
                                                <div class="table-responsive" style="max-height: 320px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                    <table class="table table-striped table-bordered nowrap mb-0" style="width: 100%;">
                                                        <thead class="bg-light-blue" style="position: sticky; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                            <tr>
                                                                <th>Student ID
                                                                </th>
                                                                <th>Enroll ID
                                                                </th>
                                                                <th>Student Name
                                                                </th>
                                                                <th>Email
                                                                </th>
                                                                <th>Mobile No
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </asp:Panel>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:LinkButton ID="lnkUsername" runat="server" CommandName='<%# Eval("STUDNAME") %>' Text='<%# Eval("USERNAME") %>' CommandArgument='<%# Eval("USERNO") %>' OnClick="lnkUsername_Click"></asp:LinkButton>
                                                </td>
                                                <td>
                                                    <%# Eval("ENROLLNO")%>
                                                </td>
                                                <td>
                                                    <%# Eval("STUDENTNAME")%>
                                                </td>

                                                <td>
                                                    <%# Eval("EMAILID")%>
                                                </td>
                                                <td>
                                                    <%# Eval("MOBILENO")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                        <asp:AsyncPostBackTrigger ControlID="btnClearSearch" />
                        <asp:PostBackTrigger ControlID="lvStudent" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#SearchPopUp").click(function () {
                //alert('hii')
                $("#ModelSearchPopup").modal();

            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $("#SearchPopUp").click(function () {
                    //alert('hii')
                    $("#ModelSearchPopup").modal();

                });
            });
        });
    </script>
    <script>
        $(document).ready(function () {
            var dateval = document.getElementById('<%=txtDateOfBirth.ClientID%>').value;
            var prev_date = new Date();
            prev_date.setDate(prev_date.getDate() - 1);
            $('.dobPersonal').daterangepicker({
                singleDatePicker: true,
                showDropdowns: true,
                //minDate: '01/1/1975',
                maxDate: prev_date,
                locale: {
                    format: 'DD/MM/YYYY'
                },
                autoApply: true,
            });
            if (dateval == "") {
                $('.dobPersonal').val('');
            }
        });

        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(function () {
                var dateval = document.getElementById('<%=txtDateOfBirth.ClientID%>').value;
                var prev_date = new Date();
                prev_date.setDate(prev_date.getDate() - 1);
                $('.dobPersonal').daterangepicker({
                    singleDatePicker: true,
                    showDropdowns: true,
                    //minDate: '01/1/1975',
                    maxDate: prev_date,
                    locale: {
                        format: 'DD/MM/YYYY'
                    },
                    autoApply: true,
                });
                if (dateval == "") {
                    $('.dobPersonal').val('');
                }

            });
        });
    </script>
    <script>
        $(document).ready(function () {
            var dateval = document.getElementById('<%=txtDateOfBirth.ClientID%>').value;
            var prev_date = new Date();
            prev_date.setDate(prev_date.getDate() - 1);
            $('.dboacrdate').daterangepicker({
                singleDatePicker: true,
                showDropdowns: true,
                //minDate: '01/1/1975',
                maxDate: prev_date,
                locale: {
                    format: 'DD/MM/YYYY'
                },
                autoApply: true,
            });
            if (dateval == "") {
                $('.dboacrdate').val('');
            }
        });

        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(function () {
                var dateval = document.getElementById('<%=txtDateOfBirth.ClientID%>').value;
                var prev_date = new Date();
                prev_date.setDate(prev_date.getDate() - 1);
                $('.dboacrdate').daterangepicker({
                    singleDatePicker: true,
                    showDropdowns: true,
                    //minDate: '01/1/1975',
                    maxDate: prev_date,
                    locale: {
                        format: 'DD/MM/YYYY'
                    },
                    autoApply: true,
                });
                if (dateval == "") {
                    $('.dboacrdate').val('');
                }

            });
        });
    </script>
</asp:Content>

