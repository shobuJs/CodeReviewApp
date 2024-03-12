<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteMasterPage.master" CodeFile="OtherInformation.aspx.cs" Inherits="ACADEMIC_OtherInformation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="z-index: 1; position: absolute; top: 10px; left: 500px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updotherinformation"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px; text-align: center">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel ID="updotherinformation" runat="server">
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
                                    <div class="panel panel-heading"><b>Click To Open Respective Information</b></div>
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
                                                            ToolTip="Please Click Here For Address Details." OnClick="lnkAddressDetail_Click" Style="color: white; font-size: 16px;" onmouseover="this.style.color='yellow'" onmouseout="this.style.color='white'" Text="Address Details"> 
                                                        </asp:LinkButton>
                                                    </span>
                                                    </i>
                                                        <hr />
                                                    </li>

                                                    <div id="divadmissiondetailstreeview" runat="server">
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
                                                            ToolTip="Please Click Here For Other Information." OnClick="lnkotherinfo_Click" Style="color: yellow; font-size: 16px;" Text="Other Information"> 
                                                        </asp:LinkButton>
                                                    </span>
                                                    </i>
                                                        <p></p>
                                                        <hr />
                                                    </li>
                                                    <li class="treeview">&nbsp;<i class="glyphicon glyphicon-print"><span>
                                                        <asp:LinkButton runat="server" ID="lnkprintapp" ToolTip="Please Click Here For Admission Form Report" OnClick="lnkprintapp_Click" Style="color: white; font-size: 16px;" onmouseover="this.style.color='yellow'" onmouseout="this.style.color='white'" Text="Print"></asp:LinkButton>
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
                        <div class="col-md-9 form-group">
                            <div class="box box-info">
                                <div class="box-header with-border" id="Div1" runat="server">
                                    <h3 class="box-title"><b>Other Personal Information</b></h3>
                                    <div class="box-tools pull-right">
                                    </div>
                                </div>
                                <br />
                                <div style="color: Red; font-weight: bold; margin-top: -20px; margin-right: -5px;">
                                    &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                                </div>
                                <div class="box-body">
                                    <div id="divAboutStudent" style="display: block;">
                                        <div class="form-group col-md-3">
                                            <span style="color: red">*</span>
                                            <label>Birth Place</label>
                                            <asp:TextBox ID="txtBirthPlace" runat="server" ToolTip="Please Enter Birth Place"
                                                TabIndex="80" CssClass="form-control" onkeypress="return alphaOnly(event);" />
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server"
                                                TargetControlID="txtBirthPlace" FilterType="Custom" FilterMode="InvalidChars"
                                                InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                            <asp:RequiredFieldValidator ID="rfvbirth" runat="server" ControlToValidate="txtBirthPlace"
                                                Display="None" ErrorMessage="Please Enter Birth Place" SetFocusOnError="True"
                                                TabIndex="1" ValidationGroup="Academic"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-md-3">
                                            <span style="color: red">*</span>
                                            <label>Mother Tongue</label>
                                            <asp:DropDownList ID="ddlMotherToungeNo" CssClass="form-control" runat="server" AppendDataBoundItems="True"
                                                ToolTip="Please Select Mother Tounge" TabIndex="81">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-md-3">
                                            <label>Other Language</label>
                                            <asp:TextBox ID="txtOtherLangauge" CssClass="form-control" runat="server" ToolTip="Please Enter Other Language" onkeypress="return alphaOnly(event);"
                                                TabIndex="82"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server"
                                                TargetControlID="txtOtherLangauge" FilterType="Custom" FilterMode="InvalidChars"
                                                InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                        </div>
                                        <div class="form-group col-md-3">
                                            <label>Birth Village</label>
                                            <asp:TextBox ID="txtBirthVillage" runat="server" CssClass="form-control" ToolTip="Please Enter Birth Village" onkeypress="return alphaOnly(event);"
                                                TabIndex="83" />
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender33" runat="server"
                                                TargetControlID="txtBirthVillage" FilterType="Custom" FilterMode="InvalidChars"
                                                InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                        </div>
                                        <div class="form-group col-md-3">
                                            <label>Birth Tehsil </label>
                                            <asp:TextBox ID="txtBirthTaluka" runat="server" CssClass="form-control" ToolTip="Please Enter Birth Taluka" onkeypress="return alphaOnly(event);"
                                                TabIndex="84" />
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender34" runat="server"
                                                TargetControlID="txtBirthTaluka" FilterType="Custom" FilterMode="InvalidChars"
                                                InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                        </div>
                                        <div class="form-group col-md-3">
                                            <label>Birth District </label>
                                            <asp:TextBox ID="txtBirthDistrict" runat="server" CssClass="form-control" ToolTip="Please Enter Birth District" onkeypress="return alphaOnly(event);"
                                                TabIndex="85" />
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender35" runat="server"
                                                TargetControlID="txtBirthDistrict" FilterType="Custom" FilterMode="InvalidChars"
                                                InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                        </div>
                                        <div class="form-group col-md-3">
                                            <label>Birth State </label>
                                            <asp:TextBox ID="txtBirthState" runat="server" CssClass="form-control" ToolTip="Please Enter Birth State" onkeypress="return alphaOnly(event);"
                                                TabIndex="86" />
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender36" runat="server"
                                                TargetControlID="txtBirthState" FilterType="Custom" FilterMode="InvalidChars"
                                                InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                        </div>
                                        <div class="form-group col-md-3">
                                            <label>Pin Code</label>
                                            <asp:TextBox ID="txtBirthPinCode" runat="server" CssClass="form-control" TabIndex="87" ToolTip="Please Enter PIN code of birth place"
                                                MaxLength="93" onkeyup="validateNumeric(this);" />
                                        </div>
                                        <asp:Panel ID="pnlspecialization" runat="server" Visible="false">
                                            <div class="form-group col-md-3">
                                                <label>Specialization</label>
                                                <asp:TextBox ID="txtSpecialization" runat="server" CssClass="form-control" ToolTip="Please Enter Specialization" />
                                            </div>
                                        </asp:Panel>
                                        <div class="form-group col-md-3">
                                            <label>Height (In inch)</label>
                                            <asp:TextBox ID="txtHeight" runat="server" CssClass="form-control" onkeyup="validateNumeric(this);"
                                                ToolTip="Please Enter Height" MaxLength="3" TabIndex="88" />
                                        </div>
                                        <div class="form-group col-md-3">
                                            <label>Weight (In Kg)</label>
                                            <asp:TextBox ID="txtWeight" runat="server" CssClass="form-control" onkeyup="validateNumeric(this);"
                                                ToolTip="Please Enter Weight (In Kg)" MaxLength="3" TabIndex="89" />
                                        </div>
                                        <div class="form-group col-md-3">
                                            <span style="color: red">*</span>
                                            <label>Urban</label>
                                            <br />

                                            <asp:RadioButtonList ID="rdobtn_urban" runat="server" TabIndex="90" RepeatDirection="Horizontal">
                                                <asp:ListItem Text="&nbsp;Yes&nbsp;&nbsp;" Value="Y"></asp:ListItem>
                                                <asp:ListItem Text="&nbsp;No" Value="N"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="form-group col-md-3">
                                            <label>Identification Mark</label>
                                            <asp:TextBox ID="txtIdentiMark" runat="server" CssClass="form-control" ToolTip="Please Enter Identy Mark" onkeypress="return alphaOnly(event);"
                                                TabIndex="91" />
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender37" runat="server"
                                                TargetControlID="txtIdentiMark" FilterType="Custom" FilterMode="InvalidChars"
                                                InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" />
                                        </div>
                                        <div class="form-group col-md-3" style="display: none;">
                                            <label>Country Domicile</label>
                                            <asp:TextBox ID="txtCountryDomicile" runat="server" CssClass="form-control" ToolTip="Please Enter Country Domicile"
                                                TabIndex="92" />
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server"
                                                TargetControlID="txtCountryDomicile" FilterType="Custom" FilterMode="InvalidChars"
                                                InvalidChars="1234567890" />

                                        </div>
                                        <div class="form-group col-md-3">
                                            <label>Bank Name</label>
                                            <asp:DropDownList ID="ddlBank" runat="server" CssClass="form-control" AppendDataBoundItems="True"
                                                ToolTip="Please Select Bank" TabIndex="93">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group col-md-3">
                                            <label>Bank Account No</label>
                                            <asp:TextBox ID="txtAccNo" runat="server" CssClass="form-control" ToolTip="Please Enter Account No."
                                                TabIndex="94" MaxLength="20"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender38" runat="server"
                                                TargetControlID="txtAccNo" FilterType="Numbers" />
                                        </div>
                                        <div id="Div2" class="form-group col-md-3" runat="server" visible="false">
                                            <label>Goa/Non Goa</label>
                                            <asp:DropDownList ID="ddlGoaNonGoa" CssClass="form-control" runat="server" AppendDataBoundItems="True"
                                                ToolTip="Please Select Goa/Non Goa" Visible="false">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                <asp:ListItem Value="1">Goa</asp:ListItem>
                                                <asp:ListItem Value="2">Non Goa</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-md-3" id="Td1" width="16%" runat="server" visible="false">
                                            <label>Years of Study in Goa</label>
                                            <asp:TextBox ID="txtyears_study" runat="server" CssClass="form-control" onkeyup="validateNumeric(this);"
                                                Visible="false"></asp:TextBox>
                                        </div>


                                        <asp:Panel ID="pnlvisa" runat="server" Visible="false">
                                            <div class="form-group col-md-3">
                                                <label>Visa No.</label>
                                                <asp:TextBox ID="txtVisaNo" runat="server" CssClass="form-control" ToolTip="Please Enter Visa No."></asp:TextBox>
                                            </div>
                                        </asp:Panel>
                                        <div class="form-group col-md-3">
                                            <label>Passport No.</label>
                                            <asp:TextBox ID="txtPassportNo" runat="server" CssClass="form-control" ToolTip="Please Enter Passport No."
                                                TabIndex="95"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-md-8" style="display: none;">
                                            <label>Remark</label>
                                            <asp:TextBox ID="txtRemark" runat="server" Rows="3" CssClass="form-control" TextMode="MultiLine"
                                                Height="51px" ToolTip="Please Enter Remark" ValidationGroup="Academic"></asp:TextBox>
                                        </div>

                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">

                                        <div style="text-align: left; height: 50%; width: 100%; background-color: #d9edf7">
                                            <h4><b>Work Information</b></h4>
                                        </div>



                                        <br />
                                        <div class="form-group col-md-3">
                                            <label>Work Experience</label>
                                            <asp:TextBox ID="txtworkexp" runat="server" TabIndex="96" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-4">
                                            <label>Organization Last Worked For</label>
                                            <asp:TextBox ID="txtorgwork" runat="server" TabIndex="97" CssClass="form-control" ToolTip="Please Enter Organization Last Worked For"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-2">
                                            <label>Designation</label>
                                            <asp:TextBox ID="txtdesignation" runat="server" TabIndex="98" CssClass="form-control" ToolTip="Please Enter Designation."></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-3" style="display: none">
                                            <label>Total Work Experience</label>
                                            <asp:TextBox ID="txttotalexp" runat="server" CssClass="form-control" ToolTip="Please Enter Total Work Experience."></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-2">
                                            <label>EPF Number</label>
                                            <asp:TextBox ID="txtepfno" runat="server" CssClass="form-control" ToolTip="Please Enter EPF Number."></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row text-center">
                                        <asp:Button ID="btnadd" runat="server" Text="Add" TabIndex="99" CssClass="btn btn-outline-info" OnClick="btnadd_Click" />
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="container-fluid">
                                            <div class="col-md-12">
                                                <asp:ListView ID="lvExperience" runat="server">
                                                    <LayoutTemplate>
                                                        <div class="vista-grid">
                                                            <table class="table table-hover table-bordered table-responsive">
                                                                <thead>
                                                                    <tr class="bg-light-blue">
                                                                        <th style="text-align: center">Edit
                                                                        </th>
                                                                        <th style="text-align: center">Delete
                                                                        </th>
                                                                        <th>Work Experience
                                                                        </th>
                                                                        <th>Organization Last Worked For
                                                                        </th>
                                                                        <th>Designation
                                                                        </th>
                                                                        <th>EPF Number
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
                                                        <tr class="item">
                                                            <td style="text-align: center">
                                                                <asp:ImageButton ID="btnEditexpDetail" runat="server" OnClick="btnEditexpDetail_Click"
                                                                    CommandArgument='<%# Eval("EXP_INC") %>' ImageUrl="~/images/edit1.gif" />
                                                            </td>
                                                            <td style="text-align: center">
                                                                <asp:ImageButton ID="btnDeleteworkDetail" runat="server" OnClick="btnDeleteworkDetail_Click"
                                                                    CommandArgument='<%# Eval("EXP_INC") %>' ImageUrl="~/images/delete.gif" ToolTip='<%# Eval("IDNO") %>' />
                                                            </td>
                                                            <td id="qualifyno" runat="server">
                                                                <%# Eval("WORK_EXP")%>
                                                            </td>
                                                            <td id="year_of_exam" runat="server">
                                                                <%# Eval("ORG_LAST_WORK")%>
                                                            </td>
                                                            <td id="school_college_name" runat="server">
                                                                <%# Eval("DESIGNATION")%>
                                                            </td>
                                                            <td id="Td2" runat="server">
                                                                <%# Eval("DESIGNATION")%>
                                                            </td>

                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="box box-footer text-center">
                                    <asp:Button ID="btnSubmit" runat="server" TabIndex="100" Text="Submit" ToolTip="Click to Report"
                                        class="btn btn-outline-primary" OnClick="btnSubmit_Click" ValidationGroup="Academic" />
                                    &nbsp
                                                      &nbsp;<asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True"
                                                          ShowSummary="False" ValidationGroup="Academic" />

                                    <button runat="server" id="btnGohome" visible="false" tabindex="101" onserverclick="btnGohome_Click" class="btn btn-outline-danger btnGohome" tooltip="Click to Go Back Home">
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
    <script>
        function myFunction() {
            if (confirm("Are you sure!you want to delete !!!")) {
                return true;
            }
            else {
                return false;
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
    </script>
    <div id="divMsg" runat="server"></div>

</asp:Content>
