<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="StudentTempRegistration.aspx.cs" Inherits="ACADEMIC_StudentTempRegistration" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../JAVASCRIPTS/jquery.min_1.js" type="text/javascript"></script>

    <script src="../JAVASCRIPTS/jquery-ui.min_1.js" type="text/javascript"></script>
    <script type="text/javascript">
        function RunThisAfterEachAsyncPostback() {
            $(function () {
                $("#<%=txtDateOfBirth.ClientID%>").datepicker({
			        changeMonth: true,
			        changeYear: true,
			        dateFormat: 'dd/mm/yy',
			        yearRange: '1975:' + getCurrentYear()
			    });
			});

            function getCurrentYear() {
                var cDate = new Date();
                return cDate.getFullYear();
            }
        }

    </script>

    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>

    <script type="text/javascript" language="javascript">

        //Registration No.
        $(function () {
            $(".tbReg").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "../WebService.asmx/GetData",
                        data: "{ 'data': '" + request.term + "' ,'tablename': 'ACD_REGISTRATION','col1': 'IDNO','col2': 'IDNO','col3': '' }",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataFilter: function (data) {; return data; },
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    value: item
                                }
                            }))
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert(textStatus);
                        }
                    });
                },
                minLength: 1
            });
        });

        //City
        $(function () {
            $(".tbCity").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "../WebService.asmx/GetData",
                        data: "{ 'data': '" + request.term + "' ,'tablename': 'ACD_STAFF','col1': 'STAFFNO','col2': 'STAFF_NAME','col3': '' }",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataFilter: function (data) {; return data; },
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    value: item
                                }
                            }))
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert(textStatus);
                        }
                    });
                },
                minLength: 1
            });
        });

        //State
        $(function () {
            $(".tbState").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "../WebService.asmx/GetData",
                        data: "{ 'data': '" + request.term + "' ,'tablename': 'ACD_STATE','col1': 'STATENO','col2': 'STATENAME','col3': '' }",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataFilter: function (data) {; return data; },
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    value: item
                                }
                            }))
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert(textStatus);
                        }
                    });
                },
                minLength: 1
            });
        });

        //Caste
        $(function () {
            $(".tbCaste").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "../WebService.asmx/GetData",
                        data: "{ 'data': '" + request.term + "' ,'tablename': 'ACD_CASTE','col1': 'CASTENO','col2': 'CASTE','col3': '' }",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataFilter: function (data) {; return data; },
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    value: item
                                }
                            }))
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert(textStatus);
                        }
                    });
                },
                minLength: 1
            });
        });

        //Category
        $(function () {
            $(".tbCategory").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "../WebService.asmx/GetData",
                        data: "{ 'data': '" + request.term + "' ,'tablename': 'ACD_CATEGORY','col1': 'CATEGORYNO','col2': 'CATEGORY','col3': '' }",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataFilter: function (data) {; return data; },
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    value: item
                                }
                            }))
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert(textStatus);
                        }
                    });
                },
                minLength: 1
            });
        });

        //Nationality
        $(function () {
            $(".tbNationality").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "../WebService.asmx/GetData",
                        data: "{ 'data': '" + request.term + "' ,'tablename': 'ACD_NATIONALITY','col1': 'NATIONALITYNO','col2': 'NATIONALITY','col3': '' }",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataFilter: function (data) {; return data; },
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    value: item
                                }
                            }))
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert(textStatus);
                        }
                    });
                },
                minLength: 1
            });
        });

        //Mother Tongu
        $(function () {
            $(".tbMTongue").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "../WebService.asmx/GetData",
                        data: "{ 'data': '" + request.term + "' ,'tablename': 'ACD_MTONGUE','col1': 'MTONGUENO','col2': 'MTONGUE','col3': '' }",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataFilter: function (data) {; return data; },
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    value: item
                                }
                            }))
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert(textStatus);
                        }
                    });
                },
                minLength: 1
            });
        });

        //VALIDATIONS
        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = '';
                alert('Only Numeric Characters Allowed!');
                txt.focus();
                return;
            }
        }

        //calculate Percentage
        function calPercentage(txt, perType) {
            if (perType == "ssc") {
                if (document.getElementById('<%= txtSSCTotal.ClientID %>').value != "" && txt.value != "") {
                    document.getElementById('<%= txtSSCPer.ClientID %>').value = fixedTo(((parseInt(document.getElementById('<%= txtSSCTotal.ClientID %>').value) / parseInt(txt.value)) * 100), 2);
                }
            }
            else if (perType == "hsc") {
                if (document.getElementById('<%= txtHSCTotal.ClientID %>').value != "" && txt.value != "") {
                    document.getElementById('<%= txtHSCPer.ClientID %>').value = fixedTo(((parseInt(document.getElementById('<%= txtHSCTotal.ClientID %>').value) / parseInt(txt.value)) * 100), 2);
                }
            }
    }
    //Round to two digits
    fixedTo = function (number, n) {
        var k = Math.pow(10, n);
        return (Math.round(number * k) / k);
    }

    //calculate HSC PCM Total
    function calPCMTotal() {
        var phy = (document.getElementById('<%= txtHSCPhysics.ClientID %>').value == "" ? 0 : document.getElementById('<%= txtHSCPhysics.ClientID %>').value);
            var chem = (document.getElementById('<%= txtHSCChemistry.ClientID %>').value == "" ? 0 : document.getElementById('<%= txtHSCChemistry.ClientID %>').value);
            var maths = (document.getElementById('<%= txtHSCMaths.ClientID %>').value == "" ? 0 : document.getElementById('<%= txtHSCMaths.ClientID %>').value);

            if (parseInt(phy) > 100 || parseInt(chem) > 100 || parseInt(maths) > 100) {
                alert('Please Enter Marks out of 100!');
            }
            else
                document.getElementById('<%= txtHSCPCM.ClientID %>').value = parseInt(phy) + parseInt(chem) + parseInt(maths);
        }

        //validate Max Marks for HSCPCM Total
        function validateMaxMarks(txt) {
            if (parseInt(txt.value) > 300) {
                alert('Please Enter Marks in Range of 0 to 300!');
                txt.value = '';
            }
        }

        //validate Max Marks for HSCPCM Total
        function validateMaxMarksSSC(txt) {
            if (parseInt(txt.value) > 150) {
                alert('Please Enter Marks in Range of 0 to 150!');
                txt.value = '';
            }
        }

        //validate Max Marks for MHCET MATHS SCORE
        function validateMhcetMaths(txt) {
            if (parseInt(txt.value) > 100) {
                alert('Please Enter Marks in Range of 0 to 100!');
                txt.value = '';
            }
        }

        //validate Max Marks for MHCET SCORE
        function validateMhcetScore(txt) {
            if (parseInt(txt.value) > 200) {
                alert('Please Enter Marks in Range of 0 to 200!');
                txt.value = '';
            }
        }

        //validate Max Marks for MHCET PHYSICS
        function validateMhcetPhysics(txt) {
            if (parseInt(txt.value) > 50) {
                alert('Please Enter Marks in Range of 0 to 50!');
                txt.value = '';
            }
        }

        //validate SSC TOTAL MARKS CAMPARISION
        function validateSscMarksCam() {
            var totalMarks = (document.getElementById('<%= txtSSCTotal.ClientID %>').value == "" ? 0 : document.getElementById('<%= txtSSCTotal.ClientID %>').value);
            var outOfMarks = (document.getElementById('<%= txtSSCOutOfMarks.ClientID %>').value == "" ? 0 : document.getElementById('<%= txtSSCOutOfMarks.ClientID %>').value);

            if (parseInt(totalMarks) > parseInt(outOfMarks)) {
                alert('Please Enter SSC Total marks is less than SSC out of marks');
                document.getElementById('<%= txtSSCPer.ClientID %>').value = "";
            }
        }

        //validate HSC TOTAL MARKS CAMPARISION
        function validateHscMarksCam() {
            var totalMarks = (document.getElementById('<%= txtHSCTotal.ClientID %>').value == "" ? 0 : document.getElementById('<%= txtHSCTotal.ClientID %>').value);
            var outOfMarks = (document.getElementById('<%= txtHSCOutofMarks.ClientID %>').value == "" ? 0 : document.getElementById('<%= txtHSCOutofMarks.ClientID %>').value);

            if (parseInt(totalMarks) > parseInt(outOfMarks)) {
                alert('Please Enter HSC Total marks is less than HSC out of marks');
                document.getElementById('<%= txtHSCPer.ClientID %>').value = "";
            }
        }
    </script>

    <table class="vista_page_title_bar" width="100%">
        <tr>
            <td style="height: 30px">ADMISSION - STUDENT REGISTRATION&nbsp;
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>
    </table>
    <table style="width: 100%">
        <tr>
            <td style="border: 1px solid #C0C0C0; font-size: 11pt; font-weight: bold; text-align: center; background-color: #E1E1E1; color: #993300;">REGISTRATION FOR THE SESSION -
                <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" Font-Bold="true">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvSession" runat="server" ErrorMessage="Please Select Session"
                    ControlToValidate="ddlSession" InitialValue="0" Display="None" SetFocusOnError="true"
                    ValidationGroup="regsubmit" />
            </td>
        </tr>
    </table>
    <div style="color: Red; font-weight: bold">
        &nbsp;Note : * marked fields are Mandatory<br />
        &nbsp;Note : + marked indicate auto showbox please press space bar. 
    </div>
    <fieldset class="fieldset">
        <legend class="legend">Personal Details</legend>
        <table cellspacing="2" style="width: 100%">
            <tr>
                <td align="center" colspan="4">
                    <asp:Label ID="Label1" runat="server" Font-Bold="True"
                        Font-Size="Medium" ForeColor="#FF3300"></asp:Label>
                </td>
            </tr>
            <tr id="trRegNo" runat="server" visible="false">
                <td style="width: 15%; text-align: left">
                    <span class="validstar">&nbsp</span>Registration No. :
                </td>
                <td style="width: 35%; text-align: left; vertical-align: middle">
                    <asp:TextBox ID="txtRegNo" runat="server" Width="80%" TabIndex="0" onkeyup="validateNumeric(this)" class="tbReg"
                        CssClass="unwatermarked" />
                    <ajaxToolKit:TextBoxWatermarkExtender ID="text_water" runat="server" TargetControlID="txtRegNo"
                        WatermarkText="Enter Registration No. to Modify" WatermarkCssClass="watermarked" />
                    <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/IMAGES/search.png" ToolTip="Search by Registration No."
                        OnClick="btnSearch_Click" />
                </td>
                <td style="width: 15%; text-align: left">&nbsp;
                </td>
                <td style="width: 35%; text-align: left">&nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 15%; text-align: left">
                    <span style="color: red; font-weight: bold;">*</span> First Name :
                </td>
                <td style="width: 35%; text-align: left">
                    <asp:TextBox ID="txtFirstName" runat="server" Width="90%" TabIndex="1"
                        ToolTip="Please Enter First Name" />
                    <ajaxToolKit:FilteredTextBoxExtender ID="fteFirstName" runat="server"
                        TargetControlID="txtFirstName" FilterType="Custom" FilterMode="InvalidChars"
                        InvalidChars="1234567890" />
                    <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ErrorMessage="Please Enter First Name"
                        ControlToValidate="txtFirstName" Display="None" SetFocusOnError="true" ValidationGroup="regsubmit" />
                </td>
                <td style="width: 15%; text-align: left">
                    <span class="validstar">&nbsp</span>Father's Name :
                </td>
                <td style="width: 35%; text-align: left">
                    <asp:TextBox ID="txtFatherName" runat="server" Width="90%" TabIndex="2"
                        ToolTip="Please Enter Fathers Name" />
                    <ajaxToolKit:FilteredTextBoxExtender ID="fteFatherName" runat="server"
                        TargetControlID="txtFatherName" FilterType="Custom" FilterMode="InvalidChars"
                        InvalidChars="1234567890" />
                </td>
            </tr>
            <tr>
                <td style="width: 15%; text-align: left">
                    <span class="validstar">*</span>Last Name :
                </td>
                <td style="width: 35%; text-align: left">
                    <asp:TextBox ID="txtLastName" runat="server" Width="90%" TabIndex="3"
                        ToolTip="Please Enter Last Name" />
                    <ajaxToolKit:FilteredTextBoxExtender ID="fteLastName" runat="server"
                        TargetControlID="txtLastName" FilterType="Custom" FilterMode="InvalidChars"
                        InvalidChars="1234567890" />
                    <asp:RequiredFieldValidator ID="rfvLname" runat="server" ErrorMessage="Please Enter Last Name"
                        ControlToValidate="txtLastName" Display="None" SetFocusOnError="true" ValidationGroup="regsubmit" />
                </td>
                <td style="width: 15%; text-align: left">
                    <span class="validstar">&nbsp</span>Mother&#39;s Name :
                </td>
                <td style="width: 35%; text-align: left">
                    <asp:TextBox ID="txtMothersName" runat="server" Width="90%" TabIndex="4"
                        ToolTip="Please Enter Mothers Name" />
                    <ajaxToolKit:FilteredTextBoxExtender ID="fteMotherName" runat="server"
                        TargetControlID="txtMothersName" FilterType="Custom" FilterMode="InvalidChars"
                        InvalidChars="1234567890" />
                </td>
            </tr>
            <tr>
                <td style="width: 15%; text-align: left">
                    <span class="validstar">&nbsp</span>Gender :
                </td>
                <td style="width: 35%; text-align: left">
                    <asp:RadioButton ID="rbMale" runat="server" Text="Male" TabIndex="5"
                        GroupName="gender" Checked="True" />&nbsp;&nbsp;
                    <asp:RadioButton ID="rbFemale" runat="server" Text="Female" TabIndex="6" GroupName="gender" />
                </td>
                <td style="width: 15%; text-align: left">
                    <span class="validstar">*</span>DOB :&nbsp;
                </td>
                <td style="width: 35%; text-align: left">
                    <asp:TextBox ID="txtDateOfBirth" runat="server" Width="55%" TabIndex="7" ToolTip="Please Enter Date Of Birth" />
                    <ajaxToolKit:TextBoxWatermarkExtender ID="wteDateOfBirth" runat="server" TargetControlID="txtDateOfBirth"
                        WatermarkText="dd/mm/yyyy" WatermarkCssClass="watermarked" />
                    <asp:Image ID="imgCalDateOfBirth" runat="server" src="../images/calendar.png" Style="cursor: pointer"
                        Visible="false" TabIndex="8" Height="16px" />
                    <asp:RequiredFieldValidator ID="rfvDateOfBirth" runat="server" ControlToValidate="txtDateOfBirth"
                        Display="None" ErrorMessage="Please Enter Date Of Birth" SetFocusOnError="True"
                        ValidationGroup="regsubmit"></asp:RequiredFieldValidator>
                    <ajaxToolKit:CalendarExtender ID="ceDateOfBirth" runat="server" Format="dd/MM/yyyy"
                        TargetControlID="txtDateOfBirth" PopupButtonID="imgCalDateOfBirth" Enabled="True">
                    </ajaxToolKit:CalendarExtender>
                    <ajaxToolKit:MaskedEditExtender ID="meeDateOfBirth" runat="server" TargetControlID="txtDateOfBirth"
                        Mask="99/99/9999" MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True"
                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                        CultureTimePlaceholder="" Enabled="True" />
                    <ajaxToolKit:MaskedEditValidator ID="mevDateOfBirth" runat="server" EmptyValueMessage="Please Enter Date Of Birth"
                        ControlExtender="meeDateOfBirth" ControlToValidate="txtDateOfBirth" IsValidEmpty="False"
                        InvalidValueMessage="Date of Birth is Invalid" Display="None" TooltipMessage="Input a date in dd/MM/yyyy format"
                        ErrorMessage="Please Select Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                        ValidationGroup="regsubmit" SetFocusOnError="True" />
                </td>
            </tr>
            <tr>
                <td style="width: 15%; text-align: left">
                    <span class="validstar">*</span>Mother Tongue :
                </td>
                <td style="width: 35%; text-align: left">
                    <asp:TextBox ID="txtMTongue" runat="server" Width="90%" TabIndex="8" class="tbMTongue"
                        ToolTip="Please Enter Mother Tongue"
                        OnTextChanged="txtMTongue_TextChanged" /><span class="validstar">+</span>


                    <asp:RequiredFieldValidator ID="rfvMTongue" runat="server" ErrorMessage="Please Enter Mother Tongue"
                        ControlToValidate="txtMTongue" Display="None" SetFocusOnError="true" ValidationGroup="regsubmit" />

                </td>
                <td style="width: 15%; text-align: left">
                    <span class="validstar">*</span>City :
                </td>
                <td style="width: 35%; text-align: left">
                    <asp:TextBox ID="txtCity" runat="server" Width="90%" TabIndex="9" class="tbCity"
                        ToolTip="Please Enter City" /><span class="validstar">+</span>
                    <asp:RequiredFieldValidator ID="rfvCity" runat="server" ErrorMessage="Please Enter City"
                        ControlToValidate="txtCity" Display="None" SetFocusOnError="true" ValidationGroup="regsubmit" />
                </td>
            </tr>
            <tr>
                <td style="width: 15%; text-align: left">
                    <span class="validstar">&nbsp</span>Phone :
                </td>
                <td style="width: 35%; text-align: left">
                    <asp:TextBox ID="txtStdPhone" runat="server" Width="15%" TabIndex="10" onkeyup="validateNumeric(this)"
                        CssClass="unwatermarked" MaxLength="8" />
                    -
                    <asp:TextBox ID="txtPhone" runat="server" Width="70%" TabIndex="11" onkeyup="validateNumeric(this)"
                        CssClass="unwatermarked" MaxLength="10" />
                    <ajaxToolKit:TextBoxWatermarkExtender ID="wteStdPhone" runat="server" TargetControlID="txtStdPhone"
                        WatermarkText="STD" WatermarkCssClass="watermarked" />
                    <ajaxToolKit:TextBoxWatermarkExtender ID="wtetPhone" runat="server" TargetControlID="txtPhone"
                        WatermarkText="Phone No." WatermarkCssClass="watermarked" />
                </td>
                <td style="width: 15%; text-align: left">
                    <span class="validstar">&nbsp</span>Mobile :
                </td>
                <td style="width: 35%; text-align: left">
                    <asp:TextBox ID="txtMobile" runat="server" Width="90%" TabIndex="12" onkeyup="validateNumeric(this)"
                        MaxLength="12" ToolTip="Please Enter Mobile No." />
                </td>
            </tr>
            <tr>
                <td style="width: 15%; text-align: left">
                    <span class="validstar">&nbsp</span>Email :
                </td>
                <td style="width: 35%; text-align: left">
                    <asp:TextBox ID="txtEmail" runat="server" Width="90%" TabIndex="13" />
                    <asp:RegularExpressionValidator ID="rfvStudentEmail" runat="server" ControlToValidate="txtEmail"
                        Display="None" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                        ErrorMessage="Please Enter Valid EmailID"
                        ValidationGroup="regsubmit"></asp:RegularExpressionValidator>
                </td>
                <td style="width: 15%; text-align: left">
                    <span style="color: red; font-weight: bold;">*</span>State of Domicile :
                </td>
                <td style="width: 35%; text-align: left">
                    <asp:TextBox ID="txtState" runat="server" Width="90%" TabIndex="14" class="tbState"
                        ToolTip="Please Enter State" /><span class="validstar">+</span>
                    <asp:RequiredFieldValidator ID="rfvState" runat="server" ErrorMessage="Please Enter State"
                        ControlToValidate="txtState" Display="None" SetFocusOnError="true" ValidationGroup="regsubmit" />
                </td>
            </tr>
            <tr>
                <td style="width: 15%; text-align: left">
                    <span style="color: red; font-weight: bold;">*</span>Category :
                </td>
                <td style="width: 35%; text-align: left">
                    <asp:TextBox ID="txtCategory" runat="server" Width="90%" TabIndex="15" class="tbCategory"
                        ToolTip="Please Enter Category" /><span class="validstar">+</span>
                    <asp:RequiredFieldValidator ID="rfvCategory" runat="server" ErrorMessage="Please Enter Category"
                        ControlToValidate="txtCategory" Display="None" SetFocusOnError="true" ValidationGroup="regsubmit" />
                </td>
                <td style="width: 15%; text-align: left">
                    <span class="validstar">&nbsp</span>Caste :
                </td>
                <td style="width: 35%; text-align: left">
                    <asp:TextBox ID="txtCaste" runat="server" Width="90%" TabIndex="16"
                        class="tbCaste" /><span class="validstar">+</span>
                </td>
            </tr>
            <tr>
                <td style="width: 15%; text-align: left">
                    <span style="color: red; font-weight: bold;">*</span>Nationality :
                </td>
                <td style="width: 35%; text-align: left">
                    <asp:TextBox ID="txtNationality" runat="server" Width="90%" TabIndex="17" class="tbNationality"
                        ToolTip="Please Enter Nationality" /><span class="validstar">+</span>
                </td>
                <td style="width: 15%; text-align: left">
                    <span style="color: red; font-weight: bold;">*</span>Linguistic Minority :
                </td>
                <td style="width: 35%; text-align: left">
                    <asp:RadioButton ID="rbMinorityYes" runat="server" Text="Yes" GroupName="Minority"
                        TabIndex="18" />&nbsp;
                    <asp:RadioButton ID="rbMinorityNo" runat="server" Text="No" GroupName="Minority"
                        TabIndex="19" />
                </td>
            </tr>
        </table>
    </fieldset>
    <br />
    <fieldset class="fieldset">
        <legend class="legend">Academic Details</legend>
        <asp:UpdatePanel ID="updAcadDet" runat="server">
            <ContentTemplate>
                <table cellspacing="2" style="width: 100%">
                    <tr>
                        <td style="width: 15%; text-align: left">
                            <span style="color: red; font-weight: bold;">*</span>Exam Type :
                        </td>
                        <td style="width: 35%; text-align: left">
                            <asp:RadioButton ID="rbAIEEE" runat="server" Text="AIEEE" GroupName="ExamType" TabIndex="20"
                                AutoPostBack="True" OnCheckedChanged="rbAIEEE_CheckedChanged" />&nbsp;
                            <asp:RadioButton ID="rbMHCET" runat="server" Text="MHCET" GroupName="ExamType" TabIndex="21"
                                AutoPostBack="True" OnCheckedChanged="rbMHCET_CheckedChanged" />
                        </td>
                        <td style="width: 15%; text-align: left">&nbsp;
                        </td>
                        <td style="width: 35%; text-align: left">&nbsp;
                        </td>
                    </tr>
                    <tr id="trAIEEE" runat="server" visible="true">
                        <td style="width: 15%; text-align: left">
                            <span style="color: red; font-weight: bold;">*</span>AIEEE Score :
                        </td>
                        <td style="width: 35%; text-align: left">
                            <asp:TextBox ID="txtAIEEEScore" runat="server" Width="50%" TabIndex="22" onkeyup="validateNumeric(this)"
                                ToolTip="Please Enter AIEEE Score" MaxLength="3" />
                            <asp:RequiredFieldValidator ID="rfvAIEEEScore" runat="server" ErrorMessage="Please Enter AIEEE Score"
                                ControlToValidate="txtAIEEEScore" Display="None" SetFocusOnError="true" ValidationGroup="regsubmit" />
                        </td>
                        <td style="width: 15%; text-align: left">
                            <span style="color: red; font-weight: bold;">*</span>AIEEE - All India Rank :
                        </td>
                        <td style="width: 35%; text-align: left">
                            <asp:TextBox ID="txtAIEEERank" runat="server" Width="50%" TabIndex="23" onkeyup="validateNumeric(this)"
                                ToolTip="Please Enter AIEEE Rank" />
                            <asp:RequiredFieldValidator ID="rfvAIEEERank" runat="server" ErrorMessage="Please Enter AIEEE Rank"
                                ControlToValidate="txtAIEEERank" Display="None" SetFocusOnError="true" ValidationGroup="regsubmit" />
                        </td>
                    </tr>
                    <tr id="trAIEEE1" runat="server" visible="true">
                        <td style="width: 15%; text-align: left">
                            <span style="color: red; font-weight: bold;">*</span>AIEEE Roll No. :
                        </td>
                        <td style="width: 35%; text-align: left">
                            <asp:TextBox ID="txtAIEEERollNo" runat="server" Width="50%" TabIndex="24" onkeyup="validateNumeric(this)"
                                ToolTip="Please Enter AIEEE Roll No." />
                            <asp:RequiredFieldValidator ID="rfvAIEEERollNo" runat="server" ErrorMessage="Please Enter AIEEE Roll no."
                                ControlToValidate="txtAIEEEScore" Display="None" SetFocusOnError="true" ValidationGroup="regsubmit" />
                        </td>
                        <td style="width: 15%; text-align: left"></td>
                        <td style="width: 35%; text-align: left"></td>
                    </tr>
                    <tr id="trMHCET" runat="server" visible="true">
                        <td style="width: 15%; text-align: left">
                            <span style="color: red; font-weight: bold;">*</span>MHCET Score (out of 200) :
                        </td>
                        <td style="width: 35%; text-align: left">
                            <asp:TextBox ID="txtMHCETScore" runat="server" Width="50%" TabIndex="25" onkeyup="validateNumeric(this)"
                                onblur="validateMhcetScore(this);" ToolTip="Please Enter MHCET Score" MaxLength="3" />

                            <asp:RequiredFieldValidator ID="rfvMHCETScore" runat="server" ErrorMessage="Please Enter MHCET Score"
                                ControlToValidate="txtMHCETScore" Display="None" SetFocusOnError="true" ValidationGroup="regsubmit" />
                            <asp:RangeValidator ID="rvMHCETScore" runat="server"
                                ControlToValidate="txtMHCETScore" Display="None"
                                ErrorMessage="Please Enter MHCET Score Out of 200" MaximumValue="200"
                                MinimumValue="0" SetFocusOnError="True" Type="Integer"
                                ValidationGroup="regsubmit"></asp:RangeValidator>
                        </td>
                        <td style="width: 15%; text-align: left">
                            <span style="color: red; font-weight: bold;">*</span>MHCET Maths (out of 100) :
                        </td>
                        <td style="width: 35%; text-align: left">
                            <asp:TextBox ID="txtMHCETMaths" runat="server" Width="50%" TabIndex="26" onkeyup="validateNumeric(this)"
                                onblur="validateMhcetMaths(this);" ToolTip="Please Enter MHCET Maths" MaxLength="3" />
                            <asp:RequiredFieldValidator ID="rfvMHCETMaths" runat="server" ErrorMessage="Please Enter MHCET Maths"
                                ControlToValidate="txtMHCETMaths" Display="None" SetFocusOnError="true" ValidationGroup="regsubmit" />
                            <asp:RangeValidator ID="rvMHCETMaths" runat="server" ControlToValidate="txtMHCETMaths"
                                Display="None" ErrorMessage="Please Enter MHCET Maths Marks Out of 100" MaximumValue="100"
                                MinimumValue="0" SetFocusOnError="True" Type="Integer" ValidationGroup="regsubmit"></asp:RangeValidator>
                        </td>
                    </tr>
                    <tr id="trMHCET1" runat="server" visible="true">
                        <td style="width: 15%; text-align: left">
                            <span style="color: red; font-weight: bold;">*</span>MHCET Physics (out of 50) :
                        </td>
                        <td style="width: 35%; text-align: left">
                            <asp:TextBox ID="txtMHCETPhysics" runat="server" Width="50%" TabIndex="27" onkeyup="validateNumeric(this)"
                                onblur="validateMhcetPhysics(this);" ToolTip="Please Enter MHCET Physics" MaxLength="2" />
                            <asp:RequiredFieldValidator ID="rfvMHCETPhysics" runat="server" ErrorMessage="Please Enter MHCET Physics"
                                ControlToValidate="txtMHCETPhysics" Display="None" SetFocusOnError="true" ValidationGroup="regsubmit" />
                            <asp:RangeValidator ID="rvMHCETPhysics" runat="server" ControlToValidate="txtMHCETPhysics"
                                Display="None" ErrorMessage="Please Enter MHCET Physics Marks Out of 50" MaximumValue="50"
                                MinimumValue="0" SetFocusOnError="True" Type="Integer" ValidationGroup="regsubmit"></asp:RangeValidator>
                        </td>
                        <td style="width: 15%; text-align: left"></td>
                        <td style="width: 35%; text-align: left"></td>
                    </tr>
                    <tr>
                        <td style="width: 15%; text-align: left;">&nbsp;
                        </td>
                        <td style="width: 35%; text-align: left;">&nbsp;
                        </td>
                        <td style="width: 15%; text-align: left;">&nbsp;
                        </td>
                        <td style="width: 35%; text-align: left;">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%; text-align: left;">
                            <span style="color: red; font-weight: bold;">*</span>SSC Maths (Out of 150):
                        </td>
                        <td style="width: 35%; text-align: left;">
                            <asp:TextBox ID="txtSSCMaths" runat="server" MaxLength="3" onkeyup="validateNumeric(this)"
                                onblur="validateMaxMarksSSC(this);" TabIndex="28" ToolTip="Please Enter SSC Maths"
                                Width="50%" />
                            <asp:RequiredFieldValidator ID="rfvSSCMaths" runat="server" ControlToValidate="txtSSCMaths"
                                Display="None" ErrorMessage="Please Enter SSC Maths" SetFocusOnError="true" ValidationGroup="regsubmit" />
                        </td>
                        <td style="width: 15%; text-align: left;">
                            <span style="color: red; font-weight: bold;">*</span>SSC Total Obtained :
                        </td>
                        <td style="width: 35%; text-align: left;">
                            <asp:TextBox ID="txtSSCTotal" runat="server" MaxLength="3" onkeyup="validateNumeric(this)"
                                TabIndex="29" ToolTip="Please Enter SSC Total" Width="50%" />
                            <asp:RequiredFieldValidator ID="rfvSSCTotal" runat="server" ControlToValidate="txtSSCTotal"
                                Display="None" ErrorMessage="Please Enter SSC Total" SetFocusOnError="true" ValidationGroup="regsubmit" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%; text-align: left;">
                            <span style="color: red; font-weight: bold;">*</span>SSC Out Of Marks :
                        </td>
                        <td style="width: 35%; text-align: left;">
                            <asp:TextBox ID="txtSSCOutOfMarks" runat="server" MaxLength="4" onblur="calPercentage(this,'ssc'),validateSscMarksCam(this)"
                                onkeyup="validateNumeric(this)" TabIndex="30" ToolTip="Please Enter SSC Out Of Marks"
                                Width="50%" />
                            <asp:RequiredFieldValidator ID="rfvSSCOutOfMarks" runat="server" ControlToValidate="txtSSCOutOfMarks"
                                Display="None" ErrorMessage="Please Enter SSC Out Of Marks" SetFocusOnError="true"
                                ValidationGroup="regsubmit" />
                        </td>
                        <td style="width: 15%; text-align: left;">
                            <span style="color: red; font-weight: bold;">*</span>SSC Agg. %age. :
                        </td>
                        <td style="width: 35%; text-align: left;">
                            <asp:TextBox ID="txtSSCPer" runat="server" MaxLength="6" onkeyup="validateNumeric(this)"
                                TabIndex="31" Width="50%" Enabled="False" />
                            <asp:RequiredFieldValidator ID="rfvSSCPer" runat="server" ControlToValidate="txtSSCPer"
                                Display="None" ErrorMessage="Please Enter SSC Aggregate Percentage" SetFocusOnError="true"
                                ToolTip="Please Enter SSC Aggregate Percentage" ValidationGroup="regsubmit" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%; text-align: left">&nbsp;
                        </td>
                        <td style="width: 35%; text-align: left">&nbsp;
                        </td>
                        <td style="width: 15%; text-align: left">&nbsp;
                        </td>
                        <td style="width: 35%; text-align: left">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%; text-align: left">
                            <span style="color: red; font-weight: bold;">*</span>HSC Physics (out of 100):
                        </td>
                        <td style="width: 35%; text-align: left">
                            <asp:TextBox ID="txtHSCPhysics" runat="server" MaxLength="3" onkeyup="validateNumeric(this)"
                                TabIndex="32" onblur="calPCMTotal()" ToolTip="Please Enter HSC Physics" Width="50%" />
                            <asp:RequiredFieldValidator ID="rfvHSCPhysics" runat="server" ControlToValidate="txtHSCPhysics"
                                Display="None" ErrorMessage="Please Enter HSC Physics Marks" SetFocusOnError="true"
                                ValidationGroup="regsubmit" />
                            <asp:RangeValidator ID="rvHSCPhysics" runat="server" ControlToValidate="txtHSCPhysics"
                                Display="None" ErrorMessage="Please Enter HSC Physics Marks Out of 100" MaximumValue="100"
                                MinimumValue="0" SetFocusOnError="True" Type="Integer" ValidationGroup="regsubmit"></asp:RangeValidator>
                        </td>
                        <td style="width: 15%; text-align: left">
                            <span style="color: red; font-weight: bold;">*</span>HSC Chemistry (out of 100):
                        </td>
                        <td style="width: 35%; text-align: left">
                            <asp:TextBox ID="txtHSCChemistry" runat="server" MaxLength="3" onkeyup="validateNumeric(this)"
                                TabIndex="33" onblur="calPCMTotal()" ToolTip="Please Enter HSC Chemistry" Width="50%" />
                            <asp:RequiredFieldValidator ID="rfvHSCChemistry" runat="server" ControlToValidate="txtHSCChemistry"
                                Display="None" ErrorMessage="Please Enter HSC Chemistry" SetFocusOnError="true"
                                ValidationGroup="regsubmit" />
                            <asp:RangeValidator ID="rvHSCChemistry" runat="server" ControlToValidate="txtHSCChemistry"
                                Display="None" ErrorMessage="Please Enter HSC Chemistry Marks out of 100" MaximumValue="100"
                                MinimumValue="0" SetFocusOnError="True" Type="Integer" ValidationGroup="regsubmit"></asp:RangeValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%; text-align: left">
                            <span style="color: red; font-weight: bold;">*</span>HSC Maths (out of 100):
                        </td>
                        <td style="width: 35%; text-align: left">
                            <asp:TextBox ID="txtHSCMaths" runat="server" Width="50%" TabIndex="34" onkeyup="validateNumeric(this)"
                                ToolTip="Please Enter HSC Maths" onblur="calPCMTotal()" MaxLength="3" />
                            <asp:RequiredFieldValidator ID="rfvHSCMaths" runat="server" ErrorMessage="Please Enter HSC Maths"
                                ControlToValidate="txtHSCMaths" Display="None" SetFocusOnError="true" ValidationGroup="regsubmit" />
                            <asp:RangeValidator ID="rvHSCMaths" runat="server" ControlToValidate="txtHSCMaths"
                                Display="None" ErrorMessage="Please Enter HSC Maths Marks Out of 100" MaximumValue="100"
                                MinimumValue="0" SetFocusOnError="True" Type="Integer" ValidationGroup="regsubmit"></asp:RangeValidator>
                        </td>
                        <td style="width: 15%; text-align: left">
                            <span style="color: red; font-weight: bold;">*</span>HSC PCM Total (out of 300)
                            :
                        </td>
                        <td style="width: 35%; text-align: left">
                            <asp:TextBox ID="txtHSCPCM" runat="server" Width="50%" TabIndex="35" onkeyup="validateNumeric(this)"
                                ToolTip="Please Enter HSC PCM" MaxLength="3" onblur="validateMaxMarks(this);" />
                            <asp:RequiredFieldValidator ID="rfvHSCPCM" runat="server" ErrorMessage="Please Enter HSC PCM"
                                ControlToValidate="txtHSCPCM" Display="None" SetFocusOnError="true" ValidationGroup="regsubmit" />
                            <asp:RangeValidator ID="rvtHSCPCM" runat="server" ControlToValidate="txtHSCPCM" Display="None"
                                ErrorMessage="Please Enter HSC PCM Marks Out of 300" MaximumValue="300" MinimumValue="0"
                                SetFocusOnError="True" Type="Integer" ValidationGroup="regsubmit"></asp:RangeValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%; text-align: left">
                            <span style="color: red; font-weight: bold;">*</span>HSC Total Obtained :
                        </td>
                        <td style="width: 35%; text-align: left">
                            <asp:TextBox ID="txtHSCTotal" runat="server" Width="50%" TabIndex="36" onkeyup="validateNumeric(this)"
                                ToolTip="Please Enter HSC Total" MaxLength="3" />
                            <asp:RequiredFieldValidator ID="rfvHSCTotal" runat="server" ErrorMessage="Please Enter HSC Total"
                                ControlToValidate="txtHSCTotal" Display="None" SetFocusOnError="true" ValidationGroup="regsubmit" />
                        </td>
                        <td style="width: 15%; text-align: left">
                            <span style="color: red; font-weight: bold;">*</span>HSC Out of Marks:
                        </td>
                        <td style="width: 35%; text-align: left">
                            <asp:TextBox ID="txtHSCOutofMarks" runat="server" Width="50%" TabIndex="37" onkeyup="validateNumeric(this)"
                                ToolTip="Please Enter HSC Out of Marks" MaxLength="3" onblur="calPercentage(this,'hsc'),validateHscMarksCam(this)" />
                            <asp:RequiredFieldValidator ID="rfvHSCOutofMarks" runat="server" ErrorMessage="Please Enter HSC Out of Marks"
                                ControlToValidate="txtHSCOutofMarks" Display="None" SetFocusOnError="true" ValidationGroup="regsubmit" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%; text-align: left">
                            <span style="color: red; font-weight: bold;">*</span>HSC Agg. %age. :
                        </td>
                        <td style="width: 35%; text-align: left">
                            <asp:TextBox ID="txtHSCPer" runat="server" Width="50%" TabIndex="38" onkeyup="validateNumeric(this)"
                                ToolTip="Please Enter HSC Agg. %age." MaxLength="6" Enabled="False" />
                            <asp:RequiredFieldValidator ID="rfvHSCPer" runat="server" ErrorMessage="Please Enter HSC Agg. %age."
                                ControlToValidate="txtHSCPer" Display="None" SetFocusOnError="true" ValidationGroup="regsubmit" />
                        </td>
                        <td style="width: 15%; text-align: left">&nbsp;
                        </td>
                        <td style="width: 35%; text-align: left">&nbsp;
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </fieldset>
    <br />
    <fieldset class="fieldset">
        <legend class="legend">Branch Preferences</legend>
        <asp:UpdatePanel ID="updBranch" runat="server">
            <ContentTemplate>
                <table cellspacing="2" style="width: 100%">
                    <tr>
                        <td style="width: 20%; text-align: right;">Select Branch Preferences. Pref. 1.</td>
                        <td>
                            <asp:DropDownList ID="ddlBranch1" runat="server" AppendDataBoundItems="true"
                                Width="50%" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlBranch1_SelectedIndexChanged" TabIndex="39" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%; text-align: right;">Pref. 2.</td>
                        <td>
                            <asp:DropDownList ID="ddlBranch2" runat="server" AppendDataBoundItems="true"
                                Width="50%" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlBranch2_SelectedIndexChanged" TabIndex="40">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%; text-align: right;">Pref. 3.</td>
                        <td>
                            <asp:DropDownList ID="ddlBranch3" runat="server" AppendDataBoundItems="true"
                                Width="50%" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlBranch3_SelectedIndexChanged" TabIndex="41">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%; text-align: right;">Pref. 4.</td>
                        <td>
                            <asp:DropDownList ID="ddlBranch4" runat="server" AppendDataBoundItems="true"
                                Width="50%" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlBranch4_SelectedIndexChanged" TabIndex="42">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%; text-align: right;">Pref. 5.</td>
                        <td>
                            <asp:DropDownList ID="ddlBranch5" runat="server" AppendDataBoundItems="true"
                                Width="50%" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlBranch5_SelectedIndexChanged" TabIndex="43">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%; text-align: right;">Pref. 6.</td>
                        <td>
                            <asp:DropDownList ID="ddlBranch6" runat="server" AppendDataBoundItems="true"
                                Width="50%" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlBranch6_SelectedIndexChanged" TabIndex="44">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%; text-align: right;">Pref. 7.</td>
                        <td>
                            <asp:DropDownList ID="ddlBranch7" runat="server" AppendDataBoundItems="true"
                                Width="50%" TabIndex="45">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </fieldset>
    <div style="text-align: center; padding-top: 10px; padding-bottom: 5px">
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="regsubmit"
            Width="80px" TabIndex="46" Font-Bold="true" OnClick="btnSubmit_Click" />&nbsp;
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
            Width="80px" TabIndex="47" Font-Bold="true" OnClick="btnCancel_Click" />
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="regsubmit"
        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

    <div id="divMsg" runat="server">
    </div>
</asp:Content>

