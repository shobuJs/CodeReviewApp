<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ModifiedLead.aspx.cs" Inherits="ACADEMIC_ModifiedLead" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .card-header .title {
            font-size: 15px;
            color: #000;
        }

        .card-header .accicon {
            float: right;
            font-size: 20px;
            width: 1.2em;
        }

        .card-header {
            cursor: pointer;
            border-bottom: none;
            padding: .3rem 0.7rem;
        }

        .card {
            border: 1px solid #ddd;
        }

        .card-body {
            border-top: 1px solid #ddd;
            padding: 1.25rem 0rem;
        }

        .card-header:not(.collapsed) .rotate-icon {
            transform: rotate(180deg);
        }

        td .fa-eye {
            font-size: 18px;
            color: #0d70fd;
        }

        td .fa-download {
            font-size: 18px;
            color: green;
        }
    </style>
    <link href='<%=Page.ResolveUrl("~/plugins/newbootstrap/css/lead.css") %>' rel="stylesheet" />
    <script src="https://bankofceylon.gateway.mastercard.com/checkout/version/61/checkout.js" data-error="errorCallback" data-cancel="cancelCallback"></script>
    <script type="text/javascript">
        function errorCallback(error) {
            console.log(JSON.stringify(error));
        }
        function cancelCallback() {
            //console.log('Payment cancelled');
        }

        cancelCallback = "https://admission.sliit.lk/OnlineResponse.aspx"
        Checkout.configure({
            session: {
                id: '<%= Session["PaymentSession"] %>'
            },
            interaction: {
                merchant: {
                    name: 'Your merchant name',
                    address: {
                        line1: '200 Sample St',
                        line2: '1234 Example Town'
                    }
                }
            }
        });
    </script>
    <div>
        <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="updInterviewResult"
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
    <asp:UpdatePanel ID="updAllOnlineAdmDetails" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">Applicant Preview</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12 btn-footer">
                                <span id="SearchPopUp" class="btn btn-outline-info"><i class="fa fa-search"></i></span>
                            </div>

                            <div class="accordion" id="accordionExample">
                                <div class="card">
                                    <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseApplication" aria-expanded="false">
                                        <span class="title">Application Track</span>
                                        <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                                    </div>
                                    <div id="collapseApplication" class="collapse show">
                                        <div class="box-body mt-2">
                                            <div class="col-12 mt-3 mb-3">

                                                <section class="design-process-section" id="process-tab">

                                                    <ul class="process-model more-icon-preocess text-center" role="tablist">

                                                        <li role="presentation">
                                                            <a>
                                                                <i id="iOnlineAdmSub" runat="server"></i>
                                                            </a>
                                                            <p>Application Filled</p>

                                                        </li>

                                                        <li role="presentation check">
                                                            <a>
                                                                <i id="iStudDocUpload" runat="server"></i>
                                                            </a>
                                                            <p>Application Payment</p>

                                                        </li>

                                                        <li role="presentation">
                                                            <a>
                                                                <i id="iAdmSecAprvl" runat="server"></i>
                                                            </a>
                                                            <p>Aptitude Test Cleared</p>

                                                        </li>

                                                        <li role="presentation">
                                                            <a>
                                                                <i id="iFinSecAprvl" runat="server"></i>
                                                            </a>
                                                            <p>Registration Done</p>

                                                        </li>

                                                    </ul>

                                                </section>

                                            </div>
                                        </div>

                                        <div class="col-12">
                                            <asp:Panel ID="Panel4" runat="server">
                                                <asp:ListView ID="lvLeadDetails" runat="server">
                                                    <LayoutTemplate>
                                                        <div class="sub-heading">
                                                            <h5>Student List </h5>
                                                        </div>

                                                        <div class="table table-responsive">
                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                <thead>
                                                                    <tr>

                                                                        <th>Study Level
                                                                        </th>
                                                                        <th>Main counselor
                                                                        </th>
                                                                        <th>Sub counselor
                                                                        </th>
                                                                        <th>Lead Status
                                                                        </th>
                                                                        <th>Progress
                                                                        </th>
                                                                        <th>Lead Remark
                                                                        </th>
                                                                        <th>Enquiry Programme
                                                                        </th>
                                                                        <th>Applied Programme
                                                                        </th>
                                                                        <th>Aptitude marks
                                                                        </th>
                                                                        <th>Aptitude Status
                                                                        </th>
                                                                        <th>Interview Status

                                                                        </th>
                                                                        <th>Offer letter</th>

                                                                        <th>Aptitude Communication</th>

                                                                        <th>Aptitude Center</th>
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
                                                                <asp:Label ID="lblStudylevel" runat="server" Text='<%# Eval("UA_SECTIONNAME")%>'></asp:Label>

                                                                <asp:HiddenField ID="hdnEnqueryNo" runat="server" Value='<%# Eval("USERNO")%>' />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblAllottedLead" runat="server" Text='<%# Eval("MAIN_USER")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblSubCounsoller" runat="server" Text='<%# Eval("USER_NAMES")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("NEW_LEAD")%>'></asp:Label>
                                                            </td>

                                                            <td><%# Eval("PERCENTAGE_CALCULATION")%></td>

                                                            <td>
                                                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("NEW_REMARK")%>'></asp:Label></td>

                                                            <td>
                                                                <asp:Label ID="Label7" runat="server" Text='<%# Eval("PROGRAMS")%>'></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="Label15" runat="server" Text='<%# Eval("APPLIED_PROGRAMS")%>'></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="Label9" runat="server" Text='<%# Eval("APTITUDE_MARKS")%>'></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="Label10" runat="server" Text='<%# Eval("APTITUDE_RESULT")%>'></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="Label11" runat="server" Text='<%# Eval("INTERVIEW_RESULT")%>'></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="Label12" runat="server" Text='<%# Eval("SEND_OFFER_LETTER")%>'></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="Label13" runat="server" Text='<%# Eval("EXAM_RESULT") %>'></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="Label14" runat="server" Text='<%# Eval("CAMPUSNAME")%>'></asp:Label></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </div>
                                <%--  Start Status Roshan 03-02-2022--%>
                                <div class="card">
                                    <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseSix" aria-expanded="false">
                                        <span class="title">Status</span>
                                        <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                                    </div>
                                    <div id="collapseSix" class="collapse show">
                                        <div class="card-body">
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Application No</label>
                                                        </div>
                                                        <asp:TextBox ID="txtApplicatio" runat="server" TabIndex="1" ToolTip="Application No"
                                                            Style="color: #0d70fd; font-weight: bold" onkeypress="return alphaOnly(event);" MaxLength="50" CssClass="form-control" Enabled="false" />

                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Intake</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlIntake" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlIntake_SelectedIndexChanged">
                                                            <asp:ListItem Value="0"> Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <%--<asp:TextBox ID="txtIntake" runat="server" TabIndex="1" ToolTip="Intake "
                                                        Style="color: #0d70fd; font-weight: bold" onkeypress="return alphaOnly(event);" MaxLength="50" CssClass="form-control" Enabled="false" />--%>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label></label>
                                                        </div>
                                                        <asp:Button ID="btnSubmitIntake" runat="server" Text="Submit Intake" Visible="false" OnClick="btnSubmitIntake_Click" CssClass="btn btn-outline-info" />
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Lead Status</label>
                                                        </div>
                                                        <a id="ModelUp" style="cursor: pointer">
                                                            <span><i class="fa fa-edit" style="color: #0d70fd; font-size: 22px;"></i></span></a>
                                                    </div>
                                                    <%-- <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Remark</label>
                                                </div>
                                             <asp:TextBox ID="txtRema" runat="server" TabIndex="1" ToolTip="Intake "
                                                     style="color: #0d70fd;font-weight:bold"    onkeypress="return alphaOnly(event);" MaxLength="50" CssClass="form-control" Enabled="false" />

                                            </div>--%>

                                                    <%--  <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Weekday/Weekend</label>
                                                </div>
                                                <asp:DropDownList ID="DropDownList2" runat="server" AppendDataBoundItems="true" TabIndex="2"
                                                    CssClass="form-control" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="col-12">
                                                <div class="sub-heading">
                                                    <h5>Aptitude Test Details</h5>
                                                </div>
                                            </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>The center you wish to sit for the Aptitude Test</label>
                                                </div>
                                                <asp:DropDownList ID="DropDownList3" runat="server" AppendDataBoundItems="true" TabIndex="3"
                                                    CssClass="form-control" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>

                                            </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>The Mode for the Aptitude Test</label>
                                                </div>
                                                <asp:DropDownList ID="DropDownList4" runat="server" AppendDataBoundItems="true" TabIndex="4"
                                                    CssClass="form-control" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>

                                            </div>

                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>The medium for the Aptitude Test</label>
                                                </div>
                                                <asp:DropDownList ID="DropDownList5" runat="server" AppendDataBoundItems="true" TabIndex="5"
                                                    CssClass="form-control" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>

                                            </div>

                                            <div class="col-xl-12 col-md-12">
                                                <asp:Label ID="Label2" runat="server"></asp:Label>
                                            </div>--%>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <%--  END Status  --%>

                                <div class="card">
                                    <div class="card-header" data-toggle="collapse" data-target="#collapseOne" aria-expanded="true">
                                        <span class="title">Personal Details </span>
                                        <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                                    </div>
                                    <div id="collapseOne" class="collapse show">
                                        <div class="card-body">
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>First Name</label>
                                                        </div>
                                                        <asp:TextBox ID="txtFirstName" runat="server" TabIndex="1" ToolTip="First Name "
                                                            onkeypress="return alphaOnly(event);" MaxLength="50" CssClass="form-control" />
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtFirstName"
                                                            InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" FilterMode="InvalidChars" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtFirstName"
                                                            Display="None" ErrorMessage="Please Enter First Name  " ValidationGroup="Submit"
                                                            SetFocusOnError="True" InitialValue=""></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="col-xl-3 col-md-4">
                                                        <div class="md-form">
                                                            <label for="dsd"><sup>*</sup> Middle Name </label>
                                                            <asp:TextBox ID="txtMiddleName" runat="server" Width="100%" ToolTip="Middle Name"
                                                                onkeypress="return alphaOnly(event);" MaxLength="20" CssClass="form-control" Placeholder="Please Enter Middle Name" TabIndex="2" />
                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtMiddleName"
                                                                InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" FilterMode="InvalidChars" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtMiddleName"
                                                                Display="None" ErrorMessage="Please Enter Middle Name  " ValidationGroup="Submit"
                                                                SetFocusOnError="True" InitialValue=""></asp:RequiredFieldValidator>
                                                            <asp:HiddenField runat="server" ID="HiddenField1" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Last Name</label>
                                                        </div>
                                                        <asp:TextBox ID="txtPerlastname" runat="server" TabIndex="2" ToolTip="Last Name "
                                                            onkeypress="return alphaOnly(event);" MaxLength="50" CssClass="form-control" />
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txtPerlastname"
                                                            InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" FilterMode="InvalidChars" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtPerlastname"
                                                            Display="None" ErrorMessage="Please Enter Last Name  " ValidationGroup="Submit"
                                                            SetFocusOnError="True" InitialValue=""></asp:RequiredFieldValidator>
                                                    </div>

                                                    <%--  <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Name with Initials</label>
                                                    </div>
                                                    <asp:TextBox ID="txtNameInitial" runat="server" TabIndex="3" ToolTip="Name with Initials "
                                                        onkeypress="return alphaOnly(event);" MaxLength="50" CssClass="form-control" Enabled="true" placeholder="Example:- PERERA S.A" />
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="ajfFullName" runat="server" TargetControlID="txtNameInitial"
                                                        InvalidChars="~`!@#$%^*()_+=,/:;<>?'{}[]\|-&&quot;'" FilterMode="InvalidChars" />
                                                </div>--%>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Email</label>
                                                        </div>
                                                        <asp:TextBox ID="txtEmail" runat="server" TabIndex="4" CssClass="form-control" Placeholder="Please Enter Email ID" />
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtEmail"
                                                            Display="None" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                            ErrorMessage="Please Enter Valid Email_ID" ValidationGroup="login"></asp:RegularExpressionValidator>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Mobile No.</label>
                                                        </div>
                                                        <div class="input-group">
                                                            <span class="input-group-prepend" style="width: 130px! important;">
                                                                <asp:DropDownList ID="ddlOnlineMobileCode" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="4" onchange="return RemoveCountryName()">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="ddlOnlineMobileCode"
                                                                    Display="None" ErrorMessage="Please Select Mobile Code " ValidationGroup="Submit"
                                                                    SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                                            </span>
                                                            <asp:TextBox ID="txtMobile" runat="server" TabIndex="6"
                                                                MaxLength="12" ToolTip="Please Enter Mobile No." CssClass="form-control ml-2 pb-0 pl-0 pr-0" Placeholder="Please Enter Mobile No." />
                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtMobile"
                                                                ValidChars="1234567890" FilterMode="ValidChars" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtMobile"
                                                                Display="None" ErrorMessage="Please Enter Mobile No " ValidationGroup="Submit"
                                                                SetFocusOnError="True" InitialValue=""></asp:RequiredFieldValidator>

                                                        </div>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Home telephone No.</label>
                                                        </div>
                                                        <div class="input-group">
                                                            <span class="input-group-prepend" style="width: 130px! important;">
                                                                <asp:DropDownList ID="ddlHomeMobileCode" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="4" onchange="return RemoveHomeCountryName()">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </span>
                                                            <asp:TextBox ID="txtHomeTel" runat="server" TabIndex="8" CssClass="form-control ml-2 pb-0 pl-0 pr-0" Placeholder="Home telephone No." MaxLength="10" />
                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="txtHomeTel"
                                                                ValidChars="1234567890" FilterMode="ValidChars" />
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>NIC (National Identity card)</label>
                                                        </div>
                                                        <asp:TextBox ID="txtOnlineNIC" runat="server" TabIndex="9" ToolTip="Please NIC "
                                                            MaxLength="30" CssClass="form-control" onblur="return Validator();" />
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtOnlineNIC"
                                                            InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" FilterMode="InvalidChars" />
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>Passport No</label>
                                                        </div>
                                                        <asp:TextBox ID="txtPersonalPassprtNo" runat="server" TabIndex="10" ToolTip="Please Enter Passport No "
                                                            MaxLength="30" CssClass="form-control" onblur="return Validator();" />

                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" TargetControlID="txtPersonalPassprtNo"
                                                            InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" FilterMode="InvalidChars" />
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Date Of Birth (DD/MM/YY)</label>
                                                        </div>
                                                        <asp:TextBox ID="txtDateOfBirth" name="dob" runat="server" TabIndex="11" CssClass="form-control dobPersonal"
                                                            ToolTip="Please Enter Date Of Birth" placeholder="DD/MM/YYYY" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txtDateOfBirth"
                                                            Display="None" ErrorMessage="Please Enter Date Of Birth " ValidationGroup="Submit"
                                                            SetFocusOnError="True" InitialValue=""></asp:RequiredFieldValidator>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Gender</label>
                                                        </div>
                                                        <asp:RadioButtonList ID="rdPersonalGender" runat="server" TabIndex="12" RepeatDirection="Horizontal">
                                                            <asp:ListItem Text="&nbsp;Male" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="&nbsp;Female" Value="1"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Citizen Type</label>
                                                        </div>
                                                        <asp:RadioButtonList ID="rdbQuestion" runat="server" TabIndex="18" RepeatDirection="Horizontal">
                                                            <asp:ListItem Text="&nbsp;Filipino" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="&nbsp;Foreign National" Value="2"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Left / Right Handed</label>
                                                        </div>
                                                        <asp:RadioButtonList ID="rdbLeftRight" runat="server" TabIndex="19" RepeatDirection="Horizontal">
                                                            <asp:ListItem Text="&nbsp;Left Handed" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="&nbsp;Right Handed" Value="2"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>
                                                                ACR No.</label>
                                                        </div>
                                                        <asp:TextBox ID="TxtACRNo" runat="server" Width="100%" TabIndex="13" ToolTip="ACR No." MaxLength="50" CssClass="form-control" />
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>
                                                                ACR Date Issue</label>
                                                        </div>

                                                        <asp:TextBox ID="txtACRDate" runat="server" name="dob1" TabIndex="14" CssClass="form-control dob1" Width="100%"
                                                            ToolTip="Please Enter ACR Date Issue" placeholder="DD/MM/YYYY" onkeypress="return onlyDotsAndNumbers(this,event);" />

                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divFather" runat="server">
                                                        <div class="md-form">
                                                            <label for="FatherName"><sup>*</sup> Father's Name </label>
                                                            <asp:TextBox ID="txtFatherName" runat="server" Width="100%" TabIndex="16" ToolTip="Please Enter Father's Name"
                                                                onkeypress="return alphaOnly(event);" MaxLength="20" CssClass="form-control" Placeholder="Please Enter Father's Name" />
                                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" TargetControlID="txtFatherName"
                                                                InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" FilterMode="InvalidChars" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtFatherName"
                                                                Display="None" ErrorMessage="Please Enter Father's Name " ValidationGroup="Submit"
                                                                SetFocusOnError="True" InitialValue=""></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divMother" runat="server">
                                                        <div class="md-form">
                                                            <label for="MotherName"><sup>*</sup> Mother's Name </label>
                                                            <asp:TextBox ID="txtMothersName" runat="server" Width="100%" TabIndex="18" ToolTip="Please Enter Mother's Name"
                                                                onkeypress="return alphaOnly(event);" MaxLength="20" CssClass="form-control" Placeholder="Please Enter Mother's Name" />
                                                            <ajaxToolkit:FilteredTextBoxExtender ID="ftbemathername" runat="server" TargetControlID="txtMothersName"
                                                                InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" FilterMode="InvalidChars" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtMothersName"
                                                                Display="None" ErrorMessage="Please Enter Mother's Name" ValidationGroup="Submit"
                                                                SetFocusOnError="True" InitialValue=""></asp:RequiredFieldValidator>
                                                            <asp:HiddenField runat="server" ID="HiddenField2" />
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12">
                                            <div class="row">

                                                <div class="col-12">
                                                    <div class="sub-heading">
                                                        <h5>Postal Address</h5>
                                                    </div>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Address (Max. Length 100)</label>
                                                    </div>
                                                    <asp:TextBox ID="txtPermAddress" runat="server" TabIndex="19" TextMode="MultiLine" Enabled="false"
                                                        MaxLength="200" ToolTip="Please Enter Permenant Address" CssClass="form-control" onkeyup="return CountCharactersPerment();" />
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Country</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlPCon" runat="server" TabIndex="20"  Enabled="false"
                                                    
                                                        AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlPCon_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>Province</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlPermanentState" runat="server"   Enabled="false"  Width="100%" TabIndex="21" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlPermanentState_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <label>City/Village</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlPTahsil" runat="server" TabIndex="22"   Enabled="false" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="card">
                                    <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                                        <span class="title">Education Details</span>
                                        <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                                    </div>
                                    <div id="collapseTwo" class="collapse show">
                                        <div id="divEducationPG" runat="server">

                                            <div class="col-12" id="DivAcademic" runat="server">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="md-form" style="pointer-events: none;" id="divddlprogrma" runat="server">
                                                            <label><sup></sup>Study Level</label>
                                                            <asp:DropDownList ID="ddlProgramTypes" runat="server" AppendDataBoundItems="true" TabIndex="1" Enabled="false"
                                                                Width="100%" CssClass="form-control select2 select-clik" AutoPostBack="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-12">
                                                <div class="table table-responsive">
                                                    <asp:Panel ID="Panel8" runat="server">
                                                        <asp:ListView ID="lvLevellist" runat="server">
                                                            <LayoutTemplate>
                                                                <table class="table table-bordered nowrap" style="width: 100%" id="divdepartmentlist">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
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
                                                                        <%--   <asp:TextBox ID="txtType" runat="server" TabIndex="1" CssClass="form-control" />--%>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="card">
                                    <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseThree" aria-expanded="false">
                                        <span class="title">Apply Program</span>
                                        <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                                    </div>
                                    <div id="collapseThree" class="collapse show">
                                        <div class="card-body">
                                            <%--<div class="col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Study Level</label>
                                                </div>
                                                <asp:DropDownList ID="ddlProgramType" runat="server" AppendDataBoundItems="true" TabIndex="1" Enabled="false"
                                                    CssClass="form-control" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>

                                            </div>
                                        </div>
                                    </div>--%>
                                            <div class="col-12">
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
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="ddlPrefrence1"
                                                                            Display="None" ErrorMessage="Please Select Preference 1 " ValidationGroup="Submit"
                                                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
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
                                                                        <asp:DropDownList ID="ddlPrefrence3" runat="server" CssClass="form-control mb-0" data-select2-enable="true" AppendDataBoundItems="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="card">
                                    <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseFour" aria-expanded="false">
                                        <span class="title">Campus Details</span>
                                        <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                                    </div>
                                    <div id="collapseFour" class="collapse show">
                                        <div class="card-body">
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>Campus</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlCampusDetails" runat="server" AppendDataBoundItems="true" TabIndex="1"
                                                            CssClass="form-control" data-select2-enable="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>

                                                    </div>

                                                    <%--  <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup></sup>
                                                    <label>Weekday/Weekend</label>
                                                </div>
                                                <asp:DropDownList ID="ddlWeekDays" runat="server" AppendDataBoundItems="true" TabIndex="2"
                                                    CssClass="form-control" data-select2-enable="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>

                                            </div>--%>
                                                </div>
                                            </div>
                                            <div class="force-overflow col-12">
                                                <div class="sub-heading mt-3">
                                                    <h5>Exam Details</h5>
                                                </div>
                                                <div class="pt-2">
                                                    <div class="row">
                                                        <div class="col-xl-3 col-md-4">
                                                            <div class="md-form">
                                                                <label><sup>*</sup> Exam Date</label>
                                                                <asp:DropDownList ID="ddlExamDate" runat="server" TabIndex="1" Width="100%" CssClass="form-control select2 select-clik" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlExamDate_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlExamDate"
                                                                    ErrorMessage="Please Select Exam Date" Display="None" SetFocusOnError="true"
                                                                    ValidationGroup="Submit" InitialValue="0" Width="10%" />

                                                            </div>
                                                        </div>
                                                        <div class="col-xl-3 col-md-4">
                                                            <div class="md-form">
                                                                <label><sup>*</sup> Time Slot</label>
                                                                <asp:DropDownList ID="ddlTimeSlot" runat="server" TabIndex="1" Width="100%" CssClass="form-control select2 select-clik" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlTimeSlot_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlTimeSlot"
                                                                    ErrorMessage="Please Select Time Slot" Display="None" SetFocusOnError="true"
                                                                    ValidationGroup="Submit" InitialValue="0" Width="10%" />
                                                            </div>
                                                        </div>
                                                        <div class="col-xl-3 col-md-4">
                                                            <div class="md-form">
                                                                <label><sup>*</sup> Venue</label>
                                                                <asp:DropDownList ID="ddlVenue" runat="server" TabIndex="1" Width="100%" CssClass="form-control select2 select-clik" AppendDataBoundItems="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlVenue"
                                                                    ErrorMessage="Please Select Venue" Display="None" SetFocusOnError="true"
                                                                    ValidationGroup="Submit" InitialValue="0" Width="10%" />
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div id="Div9" class="row d-none" runat="server" visible="false">
                                                        <div class="col-xl-4 col-md-4">
                                                            <div class="md-form">
                                                                <label><sup>*</sup>The center you wish to sit for the Aptitude Test</label>
                                                                <asp:DropDownList ID="ddlApptituteCenter" runat="server" AppendDataBoundItems="true" TabIndex="3"
                                                                    Width="100%" CssClass="form-control select2 select-clik">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>

                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlApptituteCenter"
                                                                    ErrorMessage="Please Select the Center you wish to sit for the Aptitude Test" Display="None" SetFocusOnError="true"
                                                                    ValidationGroup="Submit" InitialValue="0" Width="10%" />

                                                            </div>
                                                        </div>
                                                        <div class="col-xl-4 col-md-4">
                                                            <div class="md-form">
                                                                <label><sup>*</sup>The Mode for the Aptitude Test</label>
                                                                <asp:DropDownList ID="ddlMode" runat="server" AppendDataBoundItems="true" TabIndex="4"
                                                                    Width="100%" CssClass="form-control select2 select-clik">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>

                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlMode"
                                                                    ErrorMessage="Please Select the Mode for the Aptitude Test" Display="None" SetFocusOnError="true"
                                                                    ValidationGroup="Branch" InitialValue="0" Width="10%" />

                                                            </div>
                                                        </div>
                                                        <div class="col-xl-4 col-md-4">
                                                            <div class="md-form">
                                                                <label><sup>*</sup>The medium for the Aptitude Test</label>
                                                                <asp:DropDownList ID="ddlApptituteMedium" runat="server" AppendDataBoundItems="true" TabIndex="5"
                                                                    Width="100%" CssClass="form-control select2 select-clik">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>

                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlApptituteMedium"
                                                                    ErrorMessage="Please Select the Medium for the Aptitude Test" Display="None" SetFocusOnError="true"
                                                                    ValidationGroup="Branch" InitialValue="0" Width="10%" />

                                                            </div>
                                                        </div>
                                                        <div class="col-xl-12 col-md-12">
                                                            <asp:Label ID="lblExamcenter" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="pt-2">
                                                    <div class="row">
                                                        <div class="col-xl-3 col-md-4">
                                                            <div class="form-margin">
                                                                <label><sup>*</sup> Has Special Needs?</label>
                                                                <div>
                                                                    <asp:RadioButtonList ID="rdoSpecialNeeds" runat="server" TabIndex="1" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdoSpecialNeeds_SelectedIndexChanged">
                                                                        <asp:ListItem Text="&nbsp;Yes &nbsp;&nbsp;" Value="0"></asp:ListItem>
                                                                        <asp:ListItem Text="&nbsp;No" Value="1" Selected="True"></asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="rdoSpecialNeeds"
                                                                        ErrorMessage="Please Select Has Special Needs?" Display="None" SetFocusOnError="true"
                                                                        ValidationGroup="Branch" Width="10%" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-xl-3 col-md-4" runat="server" visible="false" id="DivSpecial">
                                                            <div class="md-form">
                                                                <label><sup></sup>If Yes</label>
                                                                <asp:TextBox ID="txtYes" runat="server" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtYes"
                                                    ErrorMessage="Please Enter Special Needs If Yes" Display="None" SetFocusOnError="true"
                                                    ValidationGroup="Branch" InitialValue="" Width="10%" />--%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="card">
                                    <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseFive" aria-expanded="false">
                                        <span class="title">Payment Details</span>
                                        <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                                    </div>

                                    <div id="collapseFive" class="collapse show">
                                        <div class="card-body">
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="col-xl-3 col-md-4" style="display: none">
                                                        <div class="md-form">
                                                            <label for="UserName"><sup>*</sup> UserName </label>
                                                            <asp:TextBox ID="txtUserName" TabIndex="1" runat="server" MaxLength="15" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-xl-3 col-md-4" style="display: none">
                                                        <div class="md-form">
                                                            <label for="Email"><sup>*</sup> Email </label>
                                                            <asp:TextBox ID="TextBox2" TabIndex="2" runat="server" MaxLength="25" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-xl-3 col-md-4" style="display: none">
                                                        <div class="md-form">
                                                            <label for="MobileNumber"><sup>*</sup> Mobile No. </label>
                                                            <div class="input-group">
                                                                <span class="input-group-prepend" style="width: 40px! important;">
                                                                    <asp:TextBox ID="txtmobilecode" TabIndex="3" runat="server" MaxLength="5" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                                </span>
                                                                <asp:TextBox ID="TextBox3" runat="server" TabIndex="4"
                                                                    MaxLength="12" CssClass="form-control ml-2 pb-0 pl-0 pr-0" Enabled="false" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-2 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Amount </label>
                                                        </div>
                                                        <asp:TextBox ID="txtAmount" TabIndex="5" runat="server" MaxLength="10" CssClass="form-control" Enabled="false" ReadOnly="true"></asp:TextBox>
                                                        <asp:HiddenField ID="hdfAmount" runat="server" Value="0" />
                                                        <asp:HiddenField ID="hdfServiceCharge" runat="server" Value="0" />
                                                        <asp:HiddenField ID="hdfTotalAmount" runat="server" Value="0" />
                                                        <asp:Label ID="lblOrderID" runat="server" Visible="false"></asp:Label>
                                                        <asp:Label ID="lblOrderIDERP" runat="server" Visible="false"></asp:Label>
                                                    </div>
                                                    <%-- <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <label>Payment Option </label>
                                                </div>
                                                <asp:RadioButtonList ID="rdPaymentOption" runat="server" TabIndex="6" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="&nbsp;Offline Payment &nbsp;&nbsp;" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="&nbsp;Online Payment" Value="1"></asp:ListItem>
                                                </asp:RadioButtonList>

                                                <asp:Label ID="lblTotalFee" runat="server" Visible="false"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Please Select Payment Option"
                                                    ControlToValidate="rdPaymentOption" Display="None" ValidationGroup="btnpay"></asp:RequiredFieldValidator>
                                            </div>--%>
                                                    <div class="col-xl-3 col-md-8">
                                                        <div class="form-margin">
                                                            <label for="RdPay"><sup>*</sup> Payment Option : </label>
                                                            <asp:RadioButtonList ID="rdPaymentOption" runat="server" TabIndex="6" RepeatDirection="Horizontal">

                                                                <%--                                            <asp:ListItem Text="&nbsp;Cash Payment" Value="1"></asp:ListItem>--%>
                                                                <asp:ListItem Text="&nbsp;Pay in Cash" Value="2" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="&nbsp;Pay in Bank &nbsp;&nbsp;" Value="0"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="Please Select Payment Option"
                                                                ControlToValidate="rdPaymentOption" Display="None" ValidationGroup="sub"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div class="col-xl-3 col-md-4" id="divUploadgENChallan" runat="server">
                                                        <div class="off-line-butn form-margin">
                                                            <label>Step 1 : </label>
                                                            <asp:Button ID="btnGenerateChallan" runat="server" TabIndex="6" ValidationGroup="sub" Text="View Bank Details" CssClass="btn btn-outline-info" OnClick="btnGenerateChallan_Click" />
                                                        </div>
                                                    </div>
                                                    <div class="col-xl-3 col-md-4" id="divUploadChallan" runat="server">
                                                        <div class="off-line-butn form-margin">
                                                            <label>Step 2 : </label>
                                                            <span id="myBtn" style="cursor: pointer" class="btn-shadow btn-wide btn-pill btn-hover-shine btn btn-outline-info BtnChallan">Upload Deposit Slip</span>
                                                            <%--<asp:Button id="Button1" runat="server" style="cursor: pointer" Text="Upload Challan Copy" CssClass="btn-shadow btn-wide btn-pill btn-hover-shine btn btn-primary BtnChallan"/>--%>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-12 col-xs-12 form-group d-flex" id="divCheck" runat="server">
                                                        <asp:CheckBox ID="chkConfirmPayment" runat="server" TabIndex="4" Style="margin-top: 2px" Checked="true" />
                                                        <label style="margin-left: 5px">
                                                            I certify that the information given is true to the best of my knowledge. In case any discrepancy is found in the facts submitted,
                                        I accept the liability of my application being rejected and the decision of the Institute in this context is final.
                                                        </label>
                                                    </div>

                                                    <div class="col-sm-12 col-xs-12 form-group d-flex" id="divOfflineNote" runat="server" visible="false">
                                                        <label style="color: green; font-size: 20px">
                                                            Dear Candidate, We have received your Application and your Payment Request is submitted to the USA Finance Department for the Approval.
                                     Once it is approved, you will be notified with an Email.
                                                        </label>
                                                    </div>
                                                    <div class="col-sm-12 col-xs-12 form-group d-flex" id="divPayment" runat="server" visible="false">
                                                        <label style="color: green; font-size: 20px">
                                                            Dear Applicant, We have received the Application Payment.
                                                        </label>
                                                    </div>
                                                    <div class="col-sm-12 col-xs-12 form-group d-flex" id="divoff">
                                                        <label style="color: green; font-size: 20px" id="divOfflinePaymentdone" runat="server">
                                                            <%-- Dear Candidate, We have received your Application payment & your Application submitted successfully.--%>
                                                            <%-- Dear Candidate, Your application payment is verified, now you can download the Aptitude admit card.--%>
                                                            <%--    Dear Candidate, Please visit the Campus to make the Application Payment in Cash. After payment is made, your application will be approved and you can able to download the Exam Admit card.--%>
                                        Dear Applicant, please proceed to the Accounting Department to pay the Application payment in CASH. After payment is made, please return to the ASPO office to claim your ADMISSION TEST PERMIT.
                                                        </label>
                                                    </div>

                                                    <div class="col-sm-12 col-xs-12 form-group d-flex" id="divBanknote">
                                                        <label style="color: green; font-size: 20px" runat="server" id="divBank">
                                                            If you are applying ONLINE, please proceed to the Bank or Payment Center of your Choice and pay the Admission Test Amount. Upload the official receipt or Deposit slip on step 2 of the Payment.
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="btnPayment" runat="server" TabIndex="6" ValidationGroup="btnpay" Text="Pay" CssClass="btn btn-outline-info" OnClick="btnPayment_Click" />
                                                <asp:Button ID="btnSavePayLater" runat="server" TabIndex="6" Text="Pay"  CssClass="btn btn-outline-info" OnClientClick="return confirm('Are you sure you want to pay later?');" OnClick="btnSavePayLater_Click" />
                                                <asp:LinkButton ID="btnChallan" runat="server" TabIndex="7" Text="Print Offer Latter" Visible="false" CssClass="btn btn-outline-info d-none" OnClick="btnChallan_Click" />
                                                <asp:LinkButton ID="btnApplicationForm" runat="server" TabIndex="8" Text="Print Application Form" Visible="false" CssClass="btn btn-outline-info" OnClick="btnApplicationForm_Click" />
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="btnpay" />
                                            </div>
                                            <div class="col-12">
                                                <asp:ListView ID="lvPaymentDetails" runat="server" EnableModelValidation="True">
                                                    <LayoutTemplate>
                                                        <div class="pt-2 table-responsive">
                                                            <%--<div class="col-md-3 col-4" style="float:right;">
                                        <input id='search-box-tab' placeholder='Search' class="form-control" style="margin-bottom:5px;">
                                    </div>--%>
                                                            <table class="table table-bordered nowrap" id="testdata">
                                                                <thead>
                                                                    <tr>
                                                                        <%--<th scope="col">SrNo</th>--%>
                                                                        <th scope="col">Action</th>
                                                                        <th scope="col">Program</th>
                                                                        <th scope="col">Aptitude Test Admission</th>
                                                                        <th scope="col">Application Fee</th>
                                                                        <th scope="col">Order Id</th>
                                                                        <th scope="col">Transaction Id</th>
                                                                        <th scope="col">Paid Amount</th>
                                                                        <th scope="col">Payment Date</th>
                                                                        <th scope="col">Status</th>
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
                                                            <%--<td><%#Container.DataItemIndex + 1 %></td>--%>
                                                            <td>
                                                                <asp:Label ID="lblRemove" runat="server"></asp:Label>
                                                                <asp:HiddenField ID="hdfRemoveDegree" runat="server" />

                                                                <asp:Label ID="lblRemoveArch" runat="server"></asp:Label>
                                                                <asp:HiddenField ID="hdfRemoveArch" runat="server" />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblDegreeNames" runat="server"></asp:Label>

                                                                <asp:Label ID="lblArchDegrees" runat="server"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblAptitutetest" runat="server" Text='<%# Eval("APTITUDE_TEST") %>' />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblAmount" runat="server"></asp:Label>
                                                                <asp:Label ID="lblArchAmount" runat="server"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblarea" runat="server" Text='<%# Eval("ORDER_ID") %>' /></td>
                                                            <td>
                                                                <asp:Label ID="lblcollegename" runat="server" Text='<%# Eval("APTRANSACTIONID") %>' /></td>
                                                            <td>
                                                                <asp:Label ID="lbldegree" runat="server" Text='<%# Eval("AMOUNT") %>' />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblPayDate" runat="server" Text='<%# Eval("RECONDATE") %>' />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("PAY_STATUS") %>' ForeColor='<%# Eval("PAY_STATUS").ToString() == "PENDING" ? System.Drawing.Color.Red : System.Drawing.Color.Green %>' />
                                                            </td>
                                                        </tr>

                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12">
                                <div class="row">
                                    <div class="col-6 mt-3">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Remark</label>
                                        </div>
                                        <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" CssClass="form-control" MaxLength="150"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer mt-3">
                                <asp:Button ID="btnSubmitAllDetails" runat="server" Text="Submit" CssClass="btn btn-outline-info" OnClick="btnSubmitAllDetails_Click" ValidationGroup="Submit" />
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
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
                                            <asp:ListItem Value="1">&nbsp;Email&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="2">&nbsp;Mobile No.&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="3">&nbsp;Application No.&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="4">&nbsp;NIC&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="5">&nbsp;Passport No.&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="0">&nbsp;Student Name&nbsp;</asp:ListItem>
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
                                                                <th>Application No.
                                                                </th>
                                                                <th>Student Name
                                                                </th>
                                                                <th>Email
                                                                </th>
                                                                <th>Mobile No
                                                                </th>
                                                                <th>NIC
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
                                                    <asp:LinkButton ID="lnkUsername" runat="server" CommandName='<%# Eval("UGPGOT") %>' Text='<%# Eval("USERNAME") %>' CommandArgument='<%# Eval("USERNO") %>' OnClick="lnkUsername_Click"></asp:LinkButton>
                                                </td>
                                                <td>
                                                    <%# Eval("FIRSTNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("EMAILID")%>
                                                </td>
                                                <td>
                                                    <%# Eval("MOBILENO")%>
                                                </td>
                                                <td>
                                                    <%# Eval("NIC")%>
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
    <asp:UpdatePanel ID="updmodelpay" runat="server">
        <ContentTemplate>

            <div id="myModal33" class="modal fade" role="dialog" data-backdrop="static">
                <div class="modal-dialog">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4>Online Payment</h4>
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-xl-6 col-md-6">
                                    <div class="md-form">
                                        <label for="UserName"><sup></sup>Order ID </label>
                                        <asp:TextBox ID="txtOrderid" TabIndex="1" runat="server" MaxLength="15" CssClass="form-control" Enabled="false" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-xl-6 col-md-6">
                                    <div class="md-form">
                                        <label for="UserName"><sup></sup>Application Fees </label>
                                        <asp:TextBox ID="txtAmountPaid" TabIndex="2" runat="server" MaxLength="15" CssClass="form-control" Enabled="false" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-xl-6 col-md-6">
                                    <div class="md-form">
                                        <label for="UserName"><sup></sup>Service Charge</label>
                                        <asp:TextBox ID="txtServiceCharge" TabIndex="3" runat="server" MaxLength="15" CssClass="form-control" Enabled="false" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-xl-6 col-md-6">
                                    <div class="md-form">
                                        <label for="UserName"><sup></sup>Total to be Paid</label>
                                        <asp:TextBox ID="txtTotalPayAmount" TabIndex="4" runat="server" MaxLength="15" CssClass="form-control" Enabled="false" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-12 text-center">
                                    <input type="button" value="Pay Now" onclick="Checkout.showLightbox();" class="btn btn-primary" />
                                    <input type="button" value="Pay with Payment Page" onclick="Checkout.showPaymentPage();" class="btn btn-primary" style="display: none" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div id="modelBank" class="modal fade">
        <div class="modal-dialog modal-xl">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <h4>Bank Details</h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-12 mb-3">
                            <asp:ListView ID="lvBankDetails" runat="server">
                                <LayoutTemplate>
                                    <div class="sub-heading">
                                        <h5>Bank Details List</h5>
                                    </div>
                                    <div class="table table-responsive">
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <td>SrNo</td>
                                                    <td>Bank Code</td>
                                                    <td>Bank Name</td>
                                                    <td>Bank Branch</td>
                                                    <td>Bank Account No.</td>
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
                                        <td><%# Container.DataItemIndex + 1 %></td>
                                        <td><%# Eval("BANKCODE") %></td>
                                        <td><%# Eval("BANKNAME") %></td>
                                        <td><%# Eval("BANKADDR") %></td>
                                        <td><%# Eval("ACCOUNT_NO") %></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div id="myModalChallan" class="modal fade" role="dialog">
        <div class="modal-dialog modal-xl">
            <!-- Modal content-->

            <asp:UpdatePanel ID="updChallan" runat="server">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4>Upload Deposit</h4>
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                        </div>
                        <div class="modal-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-4 col-md-6 col-12" id="divTransactionid" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <label>Transaction Id</label>
                                        </div>
                                        <asp:TextBox ID="txtTransactionNo" TabIndex="1" runat="server" MaxLength="20" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator75" runat="server" ErrorMessage="Please Enter Challan Transaction Id"
                                            ControlToValidate="txtTransactionNo" Display="None" ValidationGroup="SubmitChallan"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-4 col-md-6 col-12" id="DivOrderId" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <label>Challan Order ID</label>
                                        </div>
                                        <label for="UserName"><sup></sup></label>
                                        <asp:TextBox ID="txtChallanId" runat="server" MaxLength="20" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator76" runat="server" ErrorMessage="Please Enter Challan Order ID"
                                            ControlToValidate="txtChallanId" Display="None" ValidationGroup="SubmitChallan"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Bank</label>
                                        </div>
                                        <asp:DropDownList ID="ddlbank" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="2">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator77" runat="server" ErrorMessage="Please Select Bank Name"
                                            ControlToValidate="ddlbank" Display="None" InitialValue="0" ValidationGroup="SubmitChallan"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Branch</label>
                                        </div>
                                        <asp:TextBox ID="txtBranchName" runat="server" CssClass="form-control" TabIndex="3" MaxLength="50"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator78" runat="server" ErrorMessage="Please Enter Bank Branch"
                                            ControlToValidate="txtBranchName" Display="None" ValidationGroup="SubmitChallan"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Amount</label>
                                        </div>
                                        <asp:TextBox ID="txtchallanAmount" TabIndex="4" runat="server" MaxLength="10" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator79" runat="server" ErrorMessage="Please Enter Amount"
                                            ControlToValidate="txtchallanAmount" Display="None" ValidationGroup="SubmitChallan"></asp:RequiredFieldValidator>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender24" runat="server" TargetControlID="txtchallanAmount" ValidChars="1234567890." FilterMode="ValidChars" />
                                    </div>
                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Date of Payment</label>
                                        </div>
                                        <asp:TextBox ID="txtPaymentdate" TabIndex="5" runat="server" MaxLength="20" CssClass="form-control dob"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator80" runat="server" ErrorMessage="Please Enter Date of Payment"
                                            ControlToValidate="txtPaymentdate" Display="None" ValidationGroup="SubmitChallan"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Upload Deposit Slip</label>
                                        </div>
                                        <asp:FileUpload ID="FuChallan" runat="server" onchange="setUploadButtonState();" TabIndex="6" /><br />
                                        <span style="color: red; font-size: small">
                                            <asp:Label ID="lblMsg" runat="server" Text="Image Size Not Greater Than 1MB and image format JPG,JPEG,PNG,PDF Allowed"></asp:Label>
                                        </span>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 text-center">
                                <asp:Button ID="btnChallanSubmit" runat="server" TabIndex="7" Text="Submit" CssClass="btn-shadow btn-wide btn-pill btn-hover-shine btn btn-outline-info" ValidationGroup="SubmitChallan" OnClick="btnChallanSubmit_Click" />
                                <asp:ValidationSummary ID="ValidationSummary10" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="SubmitChallan" />
                            </div>

                            <div class="col-12 mb-3">
                                <asp:ListView ID="lvDepositSlip" runat="server">
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                            <h5>Deposit Slip Detail</h5>
                                        </div>
                                        <div class="table table-responsive">
                                            <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                <thead>
                                                    <tr class="bg-light-blue">
                                                        <td>SrNo</td>
                                                        <td>Bank Name</td>
                                                        <td>Bank Branch</td>
                                                        <td>Amount</td>
                                                        <td>Date of Payment</td>
                                                        <td>View</td>
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
                                            <td><%# Container.DataItemIndex + 1 %></td>
                                            <td><%# Eval("BANK_NAME") %></td>
                                            <td><%# Eval("BRANCH_NAME") %></td>
                                            <td><%# Eval("AMOUNT") %></td>
                                            <td><%# Eval("RECON_DATE") %></td>
                                            <td>
                                                <asp:LinkButton ID="lnkView" runat="server" OnClick="lnkView_Click" CommandArgument='<%#Eval("DOC_FILENAME") %>'><i class="fa fa-eye"></i></asp:LinkButton></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>

                            </div>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnChallanSubmit" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>

    <asp:UpdatePanel ID="updtest" runat="server">
        <ContentTemplate>
            <div id="myModal22" class="modal fade" role="dialog">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content" style="margin-top: -25px">
                        <div class="modal-body">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" style="margin-top: -18px">x</button>
                            </div>

                            <asp:Image ID="ImageViewer" runat="server" Width="100%" Height="500px" />
                            <asp:Literal ID="ltEmbed" runat="server" />
                            <%--<iframe id="iframe1" runat="server" src="../FILEUPLOAD/Transcript Report.pdf" frameborder="0" width="100%" height="547px"></iframe>--%>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="modal fade beauty" id="myModalApp" role="dialog">
        <div class="modal-dialog modal-xl">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" title="Close">&times;</button>
                </div>
                <div class="modal-body clearfix">
                    <div class="nav-tabs-custom">
                        <ul class="nav nav-tabs" role="tablist">
                            <li class="nav-item">
                                <a class="nav-link active" data-toggle="tab" href="#tab_1">Lead Status</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_2">Lead Status Details</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_3">Lead Details</a>
                            </li>
                        </ul>

                        <div class="tab-content">

                            <div class="tab-pane active" id="tab_1">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress8" runat="server" AssociatedUpdatePanelID="updLeadStatus"
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
                                <asp:UpdatePanel ID="updLeadStatus" runat="server">
                                    <ContentTemplate>

                                        <div class="row">
                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <label>Lead Status </label>
                                                <%-- <asp:DropDownList ID="ddlLeadStatus" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>--%>

                                                <asp:DropDownList ID="ddlLeadStatus" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:HiddenField ID="hdnEnqueryno" runat="server" />
                                                <%--  <asp:HiddenField ID="" runat="server" />--%>
                                            </div>

                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <label>Remark </label>
                                                <asp:TextBox ID="txt_Remark" runat="server" TabIndex="9" CssClass="form-control" placeholder="Enter Remark" TextMode="MultiLine"></asp:TextBox>

                                                <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server"
                                                    TargetControlID="txt_Remark" WatermarkText="Enter Remark" />
                                            </div>
                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">

                                                    <label>Next Followup date</label>
                                                </div>

                                                <asp:TextBox ID="txtEndDate" runat="server" TabIndex="4"
                                                    ToolTip="Please Enter Next Followup Date" CssClass="form-control FollowDate" />
                                                <i class="fa fa-calendar input-prefix" aria-hidden="true" style="float: right; margin-top: -23px; margin-right: 10px;"></i>
                                                <asp:RequiredFieldValidator ID="rfvEndDate" runat="server" SetFocusOnError="True"
                                                    ErrorMessage="Please Enter Next Followup Date" ControlToValidate="txtEndDate" Display="None"
                                                    ValidationGroup="ubmit" />

                                            </div>

                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="btn_SubmitModal" runat="server" TabIndex="11" Text="Submit" CssClass="btn btn-outline-primary" OnClick="btn_SubmitModal_Click" />
                                                <asp:Button ID="btn_Cancel" runat="server" TabIndex="12" Text="Clear" CssClass="btn btn-outline-danger" OnClick="btn_Cancel_Click" />
                                            </div>

                                        </div>
                                        <div class="col-12">
                                            <asp:Panel ID="pnlSession" runat="server">
                                                <asp:ListView ID="lvLeadList" runat="server">
                                                    <LayoutTemplate>
                                                        <div class="sub-heading">
                                                            <h5>Application Status List</h5>
                                                        </div>
                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                            <thead>
                                                                <tr class="bg-light-blue">
                                                                    <th style="text-align: center;">Action </th>
                                                                    <th>Lead Stage</th>
                                                                    <th>Remark</th>
                                                                    <th>Date</th>
                                                                    <th>Next Folloup Date</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </tbody>
                                                        </table>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td style="text-align: center;">
                                                                <%--<asp:ImageButton ID="btnDel" runat="server" AlternateText="Delete Record" 
                                                                    CommandArgument='<%# Eval("ENQUIRYNO") %>' ImageUrl="~/IMAGES/delete.gif" 
                                                                    OnClick="btnDel_Click" OnClientClick="return UserDeleteConfirmation();" TabIndex="6" 
                                                                    ToolTip='<%# Eval("ENQUIRYNO") %>' /> --%>

                                                                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif"
                                                                    AlternateText="Edit Record" ToolTip='<%# Eval("ENQUIRYNO") %>' CommandArgument='<%# Eval("ENQUIRYNO") %>' OnClick="btnEdit_Click" />

                                                            </td>
                                                            <td><%# Eval("LEAD_STAGE_NAME") %></td>
                                                            <td><%# Eval("REMARKS") %></td>
                                                            <td><%# Eval("UPD_ENQUIRYSTATUS_DATE") %></td>
                                                            <td><%# Eval("NEXTDATE") %></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>

                                        <asp:ValidationSummary ID="ValidationSummary4" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="login1" DisplayMode="List" />

                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btn_Cancel" />
                                        <asp:AsyncPostBackTrigger ControlID="btn_SubmitModal" />
                                        <asp:AsyncPostBackTrigger ControlID="lvLeadList" />
                                        <%-- <asp:PostBackTrigger ControlID="btn_Cancel" />--%>
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                            <div class="tab-pane fade" id="tab_2">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updInterviewResult"
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
                                <asp:UpdatePanel ID="updInterviewResult" runat="server">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="col-md-12 col-sm-12 col-12">
                                                <div class="sub-heading mt-3">
                                                    <h5>
                                                        <asp:Label ID="Label1" runat="server"></asp:Label></h5>
                                                </div>

                                                <div class="row">
                                                    <div class="col-lg-12 col-md-12 col-12 mb-3">
                                                        <asp:ListView ID="lvlist" runat="server">
                                                            <LayoutTemplate>
                                                                <div class="sub-heading">
                                                                    <h5>Application Status List</h5>
                                                                </div>
                                                                <div class="table table-responsive">
                                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="divdepartmentlist">
                                                                        <thead>
                                                                            <tr class="bg-light-blue">
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
                                                                    <td style="text-align: left;">
                                                                        <h6 style="font: bold"><%# Eval("ENQUIRY_DATE") %> </h6>
                                                                        <b>
                                                                            <asp:Label runat="server" ID="lblLeadStatus" Text='<%# Eval("LEAD_STATUS") %>'></asp:Label><br />
                                                                        </b>
                                                                        <%--<span>Councellor: <b><%# Eval("UA_NAME") %> </b> Add Lead Stage <span style="color:blue"><b><%# Eval("LEAD_STAGE_NAME") %></b></span> And Remark is <span style="color:blue"><b><%# Eval("REMARKS") %></b></span></span></td>--%>
                                                                        <span>Councellor: <b><%# Eval("UA_NAME") %> </b><%# Eval("OLD_LEAD") %> <%# Eval("NEW_LEAD") %> <%# Eval("OLD_REMARK") %> <%# Eval("NEW_REMARK") %> <%# Eval("NEXTFOLLOUP_DATE") %></span>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="tab-pane fade" id="tab_3">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updGrades"
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

                                <asp:UpdatePanel ID="updGrades" runat="server">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="col-md-12 col-sm-12 col-12">
                                                <div class="sub-heading mt-3">
                                                    <h5>Application Details</h5>
                                                </div>
                                                <div class="col-12 pl-0 pr-0">
                                                    <asp:Panel ID="Panel5" runat="server">
                                                        <asp:ListView ID="lvDetails" runat="server">
                                                            <LayoutTemplate>
                                                                <div class="table table-responsive">
                                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                                        <thead>
                                                                            <tr class="bg-light-blue">
                                                                                <%-- <th style="text-align: center;">Action </th>--%>
                                                                                <th>Study Level</th>
                                                                                <th>Study Interest</th>
                                                                                <th>Program</th>
                                                                                <th>Payment Status</th>
                                                                                <th>Date</th>
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
                                                                    <td><%# Eval("STUDY_LEVEL") %> </td>
                                                                    <td><%# Eval("STUDY_INTEREST") %> </td>
                                                                    <td><%# Eval("PROGRAM") %> </td>
                                                                    <td><%# Eval("PAYMENT_STATUS") %> </td>
                                                                    <td><%# Eval("REGDATE") %> </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>

                                </asp:UpdatePanel>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#ModelUp").click(function () {
                //alert('hii')
                $("#myModalApp").modal();

            });
        });

        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {

                $("#ModelUp").click(function () {
                    // alert('bye')
                    $("#myModalApp").modal();
                });
            });
        });
    </script>
    <script>
        $(document).ready(function () {
            var dateval = document.getElementById('<%=txtPaymentdate.ClientID%>').value;
            var prev_date = new Date();
            prev_date.setDate(prev_date.getDate());
            $('.dob').daterangepicker({
                singleDatePicker: true,
                showDropdowns: true,
                minDate: '01/1/1975',
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
    </script>

    <script>
        $("#ctl00_ContentPlaceHolder1_lvPaymentDetails_ctrl0_lblRemove").click(function () {
            __doPostBack('RemoveNormalDegree');
        });
    </script>

    <script>
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $("#ctl00_ContentPlaceHolder1_lvPaymentDetails_ctrl0_lblRemove").click(function () {
                __doPostBack('RemoveNormalDegree');
            });
        });
    </script>

    <script>
        $("#ctl00_ContentPlaceHolder1_lvPaymentDetails_ctrl0_lblRemoveArch").click(function () {
            __doPostBack('RemoveNormalArchDegree');
        });
    </script>

    <script>
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $("#ctl00_ContentPlaceHolder1_lvPaymentDetails_ctrl0_lblRemoveArch").click(function () {
                __doPostBack('RemoveNormalArchDegree');
            });
        });
    </script>


    <script type="text/javascript">
        $(document).ready(function () {
            $("#btnGenerateChallan").click(function () {
                $("#modelBank").modal();

            });
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            var parameter = Sys.WebForms.PageRequestManager.getInstance();
            parameter.add_endRequest(function () {
                $("#btnGenerateChallan").click(function () {
                    $("#modelBank").modal();

                });
            });
        });
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            $(".BtnChallan").click(function () {
                $("#myModalChallan").modal();

            });
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            var parameter = Sys.WebForms.PageRequestManager.getInstance();
            parameter.add_endRequest(function () {
                $(".BtnChallan").click(function () {
                    $("#myModalChallan").modal();

                });
            });
        });
    </script>
    <script type="text/javascript">
        function setUploadButtonState() {

            var maxFileSize = 1000000;
            var fi = document.getElementById('ctl00_ContentPlaceHolder1_FuChallan');
            for (var i = 0; i <= fi.files.length - 1; i++) {

                var fsize = fi.files.item(i).size;

                if (fsize >= maxFileSize) {
                    alert("Image Size Greater Than 1MB");
                    $("#ctl00_ContentPlaceHolder1_FuChallan").val("");

                }
            }
            var fileExtension = ['jpeg', 'jpg', 'JPG', 'JPEG', 'PNG', 'png', 'pdf', 'PDF'];
            if ($.inArray($('#ctl00_ContentPlaceHolder1_FuChallan').val().split('.').pop().toLowerCase(), fileExtension) == -1) {
                alert("Only formats are allowed : " + fileExtension.join(', '));
                $("#ctl00_ContentPlaceHolder1_FuChallan").val("");
            }
        }

    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#SearchPopUp").click(function () {
                //alert('hii')
                $("#ModelSearchPopup").modal();

            });
        });

    </script>
    <script type="text/javascript">
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $("#SearchPopUp").click(function () {
                    //alert('hii')
                    $("#ModelSearchPopup").modal();

                });
            });
        })
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
    </script>

    <script>
        function RemoveHomeCountryName() {

            $("#select2-ddlHomeMobileCode-container").html($("#select2-ddlHomeMobileCode-container").html().split('-')[0]);
        }
    </script>
    <script>
        function RemoveCountryName() {
            $("#select2-ddlOnlineMobileCode-container").html($("#select2-ddlOnlineMobileCode-container").html().split('-')[0]);
        }
    </script>
    <%--<script type="text/javascript">
        function Validator() {
            var pass = $('#ctl00_ContentPlaceHolder1_txtPassport').val();
            var nic = $('#ctl00_ContentPlaceHolder1_txtNIC').val();
            if (pass == '' && nic == '') {
                alert("Passport No. OR NIC(National Identity card) is Required !");
            }
        }
    </script>--%>

    <script>
        function CheckAdmbatch(e) {

            // debugger;
            var keyCode = e.keyCode || e.which;
            //Regex for Valid Characters i.e. Alphabets.
            var regex = /^[0-9-/ ]+$/;
            //var regex = /^[A-Za-z ]+$/;
            //Validate TextBox value against the Regex.
            var isValid = regex.test(String.fromCharCode(keyCode));
            if (!isValid) {
                alert("Only Numbers allowed.");
            }

            return isValid;

        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.datePickerPG').daterangepicker({
                startDate: moment().subtract(00, 'days'),
                endDate: moment(),
                //also comment "range" in daterangepicker.js('<div class="ranges"></div>' +)
                ranges: {

                },
                //<!-- ========= Disable dates after today ========== -->
                //maxDate: new Date(),
                //<!-- ========= Disable dates after today END ========== -->
            },
        function (start, end) {
            $('.PickerDatePG').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
        });

            $('.PickerDatePG').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
        });

        var prm4 = Sys.WebForms.PageRequestManager.getInstance();
        prm4.add_endRequest(function () {
            $(document).ready(function () {
                $('.datePickerPG').daterangepicker({
                    startDate: moment().subtract(00, 'days'),
                    endDate: moment(),
                    //also comment "range" in daterangepicker.js('<div class="ranges"></div>' +)
                    ranges: {

                    },
                    //<!-- ========= Disable dates after today ========== -->
                    //maxDate: new Date(),
                    //<!-- ========= Disable dates after today END ========== -->
                },
            function (start, end) {
                $('.PickerDatePG').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))


            });

                $('.PickerDatePG').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))


            });
        });
    </script>
    <script>
        function SetdatePG(date) {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $(document).ready(function () {
                    debugger;

                    var startDate = moment(date.split('-')[0], "MMM DD, YYYY");
                    var endtDate = moment(date.split('-')[1], "MMM DD, YYYY");
                    //$('#date').html(date);
                    // alert(endtDate)
                    $('.PickerDatePG').html(startDate.format("MMM DD, YYYY") + ' - ' + endtDate.format("MMM DD, YYYY"));

                    //$('#picker').daterangepicker({ startDate: startDate, endDate: endtDate});
                    $('.datePickerPG').daterangepicker({
                        startDate: startDate.format("MM/DD/YYYY"),
                        endDate: endtDate.format("MM/DD/YYYY"),
                        ranges: {
                        },
                    },
            function (start, end) {
                debugger
                $('.PickerDatePG').html(start.format('MMM DD, YYYY') + ' - ' + end.format('MMM DD, YYYY'))

            });

                });
            });
        };

    </script>


    <script>

        function DateValidation(dateYear) {
            year = document.getElementById("ctl00_ContentPlaceHolder1_txtAwardDatePHD").value;
            var Data = [];
            Data = year.split('/');
            if (Data[2] < "2015") {
                alert("Date of award should not be less than 01/01/2015");
                document.getElementById("ctl00_ContentPlaceHolder1_txtAwardDatePHD").value = "";
            }
        }
    </script>

    <script>

        function yearValidation(year) {

            var yearval = year.value;
            if (yearval.length != 4) {
                alert("Year of Award should be only 4 digit");
                year.value = "";
            }
            else {
                var current_year = new Date().getFullYear();
                year = document.getElementById("ctl00_ContentPlaceHolder1_txtYearofAward").value;
                if ((year > current_year)) {
                    alert("Year should not be greater than current year");
                    document.getElementById("ctl00_ContentPlaceHolder1_txtYearofAward").value = "";
                    return false;
                }
                if (year < "2015") {
                    alert("Year should not be less than 2015");
                    document.getElementById("ctl00_ContentPlaceHolder1_txtYearofAward").value = "";
                    return false;
                }
                return true;
            }
        }
    </script>
    <script>


        function Setdate(date) {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $(document).ready(function () {
                    debugger;

                    var startDate = moment(date.split('-')[0], "MMM DD, YYYY");
                    var endtDate = moment(date.split('-')[1], "MMM DD, YYYY");
                    //$('#date').html(date);
                    // alert(endtDate)
                    $('.PickerDate').html(startDate.format("MMM DD, YYYY") + ' - ' + endtDate.format("MMM DD, YYYY"));

                    //$('#picker').daterangepicker({ startDate: startDate, endDate: endtDate});
                    $('.datePicker').daterangepicker({
                        startDate: startDate.format("MM/DD/YYYY"),
                        endDate: endtDate.format("MM/DD/YYYY"),
                        ranges: {
                        },
                    },
            function (start, end) {
                debugger
                $('.PickerDate').html(start.format('MMM DD, YYYY') + ' - ' + end.format('MMM DD, YYYY'))

            });

                });
            });
        };

    </script>
    <%--<script>
        function test5()
        {
            var searchBar = document.querySelector('#FilterDataOnline');
            var table = document.querySelector('#tableOnline');
            var tableBody = table.querySelector('tbody');
            var allRows = tableBody.querySelectorAll('tr');
            //console.log(allRows);
            searchBar.addEventListener('keyup',toggleSearch);
            var checkboxArray = [];
            function toggleSearch(){  
                var val = searchBar.value.toLowerCase();
                allRows.forEach((row,index) => {  
                    var checkbox = row.querySelector('td input[type="checkbox"]');
                if(checkbox.checked && !checkboxArray.includes(checkbox.id)){
                    checkboxArray.push(checkbox.id);
                }
                var insideSearch = row.innerHTML.trim().toLowerCase();
                //console.log('data',insideSearch.includes(val),'searchhere',insideSearch);
                if(insideSearch.includes(val)){
                    row.style.display = 'table-row';
                }
                else{
                    row.style.display = 'none';
                }                
                checkbox.addEventListener('change',()=>{
                    if(checkbox.checked && !checkboxArray.includes(checkbox.id)){
                        checkboxArray.push(checkbox.id);
            } 
        else{ 
                       checkboxArray = checkboxArray.filter(ele => ele != checkbox.id );
        }
        //console.log(checkboxArray);
        allRows.forEach(row => {
            row.style.display='table-row';
        var updateCheckbox = row.querySelector('td input[type="checkbox"]');
        var cbID = updateCheckbox.id;
        if(checkboxArray.includes(cbID)){
            updateCheckbox.checked;
        }
        });
        }) 
        });
        }
        }
    </script>--%>
    <script>
        $(document).ready(function () {
            var dateval = document.getElementById('<%=txtEndDate.ClientID%>').value;
            var prev_date = new Date();
            prev_date.setDate(prev_date.getDate());
            $('.FollowDate').daterangepicker({
                singleDatePicker: true,
                showDropdowns: true,
                minDate: '01/1/1975',
                maxDate:'01/1/2050',
                locale: {
                    format: 'DD/MM/YYYY'
                },
                autoApply: true,
            });
            if (dateval == "") {
                $('.dob').val('');
            }
        });
    </script>
    <script>
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(document).ready(function () {
                var dateval = document.getElementById('<%=txtEndDate.ClientID%>').value;
                var prev_date = new Date();
                prev_date.setDate(prev_date.getDate());
                $('.FollowDate').daterangepicker({
                    singleDatePicker: true,
                    showDropdowns: true,
                    minDate: '01/1/1975',
                    maxDate: '01/1/2050',
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
    <script type="text/javascript">
        function IsNumeric(txt) {
            debugger;
            var ValidChars = "0123456789.";
            var num = true;
            var mChar;
            cnt = 0

            for (i = 0; i < txt.value.length && num == true; i++) {
                mChar = txt.value.charAt(i);

                if (ValidChars.indexOf(mChar) == -1) {
                    num = false;
                    txt.value = '';
                    alert("Please enter Numeric values only")
                    txt.select();
                    txt.focus();
                }
                else {
                    if (txt.value.length == 4) {

                    } else {
                        num = false;
                        txt.value = '';
                        alert("Please enter 4 Digit ")
                        txt.select();
                        txt.focus();
                    }

                }
            }
            return num;
        }
    </script>
    <script type="text/javascript">
        // function CountCharacters11() {
        function checkLength(el) {
            if (el.value.length >= 100) {
                alert("Maximum Charectures Length is 100 Charecters");
                el.value = "";
            }
        }
    </script>


    <script>
        $(document).ready(function () {

            $(".off-line-butn").hide();
            $("#myBtn").hide();
            $("#<%=btnGenerateChallan.ClientID%>").hide();
            $("#<%=divBank.ClientID%>").hide();
            $("#<%=divOfflinePaymentdone.ClientID%>").show();
            $("#<%=btnPayment.ClientID%>").hide();
            $("#<%=btnSavePayLater.ClientID%>").hide(); 

            $("#ctl00_ContentPlaceHolder1_rdPaymentOption").click(function () {
                var radioValue = $('#<%=rdPaymentOption.ClientID %> input[type=radio]:checked').val();
                if (radioValue == 0) {
                    $("#<%=btnPayment.ClientID%>").hide();
                    $("#<%=btnGenerateChallan.ClientID%>").show();
                    $("#<%=divBank.ClientID%>").show();
                    $("#<%=divOfflinePaymentdone.ClientID%>").hide();
                    $("#<%=btnSavePayLater.ClientID%>").hide(); 
                    //divBank

                    $("#myBtn").show();
                    $(".off-line-butn").show();
                }
                else {
                    $(".off-line-butn").hide();
                    $("#myBtn").hide();
                    $("#<%=btnGenerateChallan.ClientID%>").hide();
                    $("#<%=divBank.ClientID%>").hide();
                    $("#<%=divOfflinePaymentdone.ClientID%>").show();
                    $("#<%=btnSavePayLater.ClientID%>").show(); 
                    //$("#<%=btnPayment.ClientID%>").show();
                }
            });
        });
    </script>
    <script>

        $(document).ready(function () {
            var parameter = Sys.WebForms.PageRequestManager.getInstance();
            parameter.add_endRequest(function () {
                $(".off-line-butn").hide();
                $("#myBtn").hide();
                $("#<%=btnGenerateChallan.ClientID%>").hide();
                $("#<%=divBank.ClientID%>").hide();
                $("#<%=divOfflinePaymentdone.ClientID%>").show();
                $("#<%=btnPayment.ClientID%>").hide();
                $("#<%=btnSavePayLater.ClientID%>").hide(); 
                if ($('#<%=rdPaymentOption.ClientID %> input[type=radio]:checked').val() == 0) {
                    $("#<%=btnPayment.ClientID%>").hide();
                    $("#<%=btnGenerateChallan.ClientID%>").show();
                    $("#<%=divBank.ClientID%>").show();
                    $("#<%=divOfflinePaymentdone.ClientID%>").hide();
                    $("#<%=btnSavePayLater.ClientID%>").hide(); 
                    
                    $("#myBtn").show();
                    $(".off-line-butn").show();
                }
                else {
                    $(".off-line-butn").hide();
                    $("#myBtn").hide();
                    $("#<%=btnGenerateChallan.ClientID%>").hide();
                    $("#<%=divBank.ClientID%>").hide();
                    $("#<%=divOfflinePaymentdone.ClientID%>").show();
                    $("#<%=btnSavePayLater.ClientID%>").show(); 
                    //$("#<%=btnPayment.ClientID%>").show();
                }
                $("#ctl00_ContentPlaceHolder1_rdPaymentOption").click(function () {
                    var radioValue = $('#<%=rdPaymentOption.ClientID %> input[type=radio]:checked').val();
                    if (radioValue == 0) {
                        $("#<%=btnPayment.ClientID%>").hide();
                        $("#<%=btnGenerateChallan.ClientID%>").show();
                        $("#<%=divBank.ClientID%>").show();
                        $("#<%=divOfflinePaymentdone.ClientID%>").hide();
                        $("#myBtn").show();
                        $(".off-line-butn").show();
                    }
                    else {
                        $(".off-line-butn").hide();
                        $("#myBtn").hide();
                        $("#<%=btnGenerateChallan.ClientID%>").hide();
                        $("#<%=divBank.ClientID%>").hide();
                        $("#<%=divOfflinePaymentdone.ClientID%>").show();
                        // $("#<%=btnPayment.ClientID%>").show();
                    }
                });
            });
        });
    </script>
    <script type="text/javascript">
        function FindData(ddl) {
            debugger;
            var rowCount = document.getElementById('PrefeTab').rows.length;


            var Preference1 = document.getElementById("<%=ddlPrefrence1.ClientID %>").value;
            var Preference2 = document.getElementById("<%=ddlPrefrence2.ClientID %>").value;
            var Preference3 = document.getElementById("<%=ddlPrefrence3.ClientID %>").value;
            if (Preference1 == "0") { Preference1 = "P1" }
            if (Preference2 == "0") { Preference2 = "P2" }
            if (Preference3 == "0") { Preference3 = "P3" }
            if (ddl == 1) {
                if (Preference1 == Preference2 || Preference1 == Preference3 || Preference2 == Preference3) {
                    document.getElementById("<%=ddlPrefrence1.ClientID %>").selectedIndex = '0';
                    document.getElementById("<%=ddlPrefrence2.ClientID %>").selectedIndex = '0';
                    document.getElementById("<%=ddlPrefrence3.ClientID %>").selectedIndex = '0';
                    alert("Alredy Selected Preference")
                }
                else {
                    if (Preference1 == "P1") {
                        document.getElementById("<%=ddlPrefrence2.ClientID %>").selectedIndex = '0';
                        document.getElementById("<%=ddlPrefrence3.ClientID %>").selectedIndex = '0';
                        alert("Please Select Preference1");
                    }}

            }
            if (ddl == 2) {
                if (Preference1 == Preference2 || Preference1 == Preference3 || Preference2 == Preference3) {
                    document.getElementById("<%=ddlPrefrence2.ClientID %>").selectedIndex = '0';
                    document.getElementById("<%=ddlPrefrence2.ClientID %>").selectedIndex = '0';
                    alert("Alredy Selected Preference")
                }
                else {
                    if (Preference1 == "P1") {
                        document.getElementById("<%=ddlPrefrence2.ClientID %>").selectedIndex = '0';
                        alert("Please Select Preference1");
                    }
                }

            }
            if (ddl == 3) {
                if (Preference1 == Preference2 || Preference1 == Preference3 || Preference2 == Preference3) {
                    document.getElementById("<%=ddlPrefrence3.ClientID %>").selectedIndex = '0';
                    alert("Alredy Selected Preference")
                }
                else {
                    if (Preference1 == "P1") {
                        document.getElementById("<%=ddlPrefrence3.ClientID %>").selectedIndex = '0';
                        alert("Please Select Preference1");

                    }
                    else if (Preference2 == "P2") {
                        document.getElementById("<%=ddlPrefrence3.ClientID %>").selectedIndex = '0';
                            alert("Please Select Preference2");
                        }

                }

            }
            return;
        }

    </script>
</asp:Content>

