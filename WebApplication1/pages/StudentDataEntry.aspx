<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="StudentDataEntry.aspx.cs" Inherits="StudentDataEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <table cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr>
                <td style="text-align: left; padding-left: 10px; font-weight: bold;" class="vista_page_title_bar" width="100%" colspan="2">STUDENT INFORMATION ENTRY
                    <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                        AlternateText="Page Help" ToolTip="Page Help" Height="12px" />
                </td>
            </tr>

            <tr>
                <td colspan="2">
                    <!-- "Wire frame" div used to transition from the button to the info panel -->
                    <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF; border: solid 1px #D0D0D0;">
                    </div>
                    <!-- Info panel to be displayed as a flyout when the button is clicked -->
                    <div id="info" style="display: none; width: 250px; z-index: 2; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0); font-size: 12px; border: solid 1px #CCCCCC; background-color: #FFFFFF; padding: 5px;">
                        <div id="btnCloseParent" style="float: right; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);">
                            <asp:LinkButton ID="btnClose" runat="server" OnClientClick="return false;" Text="X"
                                ToolTip="Close" Style="background-color: #666666; color: #FFFFFF; text-align: center; font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
                        </div>
                        <div>
                            <p class="page_help_head">
                                <span style="font-weight: bold; text-decoration: underline;">Page Help</span><br />
                                <asp:Image ID="imgEdit" runat="server" ImageUrl="~/images/edit.gif" AlternateText="Edit Record" />
                                Edit Record&nbsp;&nbsp;
                                <asp:Image ID="imgDelete" runat="server" ImageUrl="~/images/delete.gif" AlternateText="Delete Record" />
                                Delete Record
                            </p>
                            <p class="page_help_text">
                                <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" />
                            </p>
                        </div>
                    </div>

                    <script type="text/javascript" language="javascript">
                        // Move an element directly on top of another element (and optionally
                        // make it the same size)
                        function Cover(bottom, top, ignoreSize) {
                            var location = Sys.UI.DomElement.getLocation(bottom);
                            top.style.position = 'absolute';
                            top.style.top = location.y + 'px';
                            top.style.left = location.x + 'px';
                            if (!ignoreSize) {
                                top.style.height = bottom.offsetHeight + 'px';
                                top.style.width = bottom.offsetWidth + 'px';
                            }
                        }
                    </script>

                    <ajaxToolKit:AnimationExtender ID="OpenAnimation" runat="server" TargetControlID="btnHelp">
                        <Animations>
                                                    <OnClick>
                                                        <Sequence>
                                                            <%-- Disable the button so it can't be clicked again --%>
                                                            <EnableAction Enabled="false" />
                                                            
                                                            <%-- Position the wire frame on top of the button and show it --%>
                                                            <ScriptAction Script="Cover($get('ctl00$ContentPlaceHolder1$btnHelp'), $get('flyout'));" />
                                                            <StyleAction AnimationTarget="flyout" Attribute="display" Value="block"/>
                                                            
                                                            <%-- Move the info panel on top of the wire frame, fade it in, and hide the frame --%>
                                                            <ScriptAction Script="Cover($get('flyout'), $get('info'), true);" />
                                                            <StyleAction AnimationTarget="info" Attribute="display" Value="block"/>
                                                            <FadeIn AnimationTarget="info" Duration=".2"/>
                                                            <StyleAction AnimationTarget="flyout" Attribute="display" Value="none"/>
                                                            
                                                            <%-- Flash the text/border red and fade in the "close" button --%>
                                                            <Parallel AnimationTarget="info" Duration=".5">
                                                                <Color PropertyKey="color" StartValue="#666666" EndValue="#FF0000" />
                                                                <Color PropertyKey="borderColor" StartValue="#666666" EndValue="#FF0000" />
                                                            </Parallel>
                                                            <Parallel AnimationTarget="info" Duration=".5">
                                                                <Color PropertyKey="color" StartValue="#FF0000" EndValue="#666666" />
                                                                <Color PropertyKey="borderColor" StartValue="#FF0000" EndValue="#666666" />
                                                                <FadeIn AnimationTarget="btnCloseParent" MaximumOpacity=".9" />
                                                            </Parallel>
                                                        </Sequence>
                                                    </OnClick>
                        </Animations>
                    </ajaxToolKit:AnimationExtender>
                    <ajaxToolKit:AnimationExtender ID="CloseAnimation" runat="server" TargetControlID="btnClose">
                        <Animations>
                                                    <OnClick>
                                                        <Sequence AnimationTarget="info">
                                                            <%--  Shrink the info panel out of view --%>
                                                            <StyleAction Attribute="overflow" Value="hidden"/>
                                                            <Parallel Duration=".3" Fps="15">
                                                                <Scale ScaleFactor="0.05" Center="true" ScaleFont="true" FontUnit="px" />
                                                                <FadeOut />
                                                            </Parallel>
                                                            
                                                            <%--  Reset the sample so it can be played again --%>
                                                            <StyleAction Attribute="display" Value="none"/>
                                                            <StyleAction Attribute="width" Value="250px"/>
                                                            <StyleAction Attribute="height" Value=""/>
                                                            <StyleAction Attribute="fontSize" Value="12px"/>
                                                            <OpacityAction AnimationTarget="btnCloseParent" Opacity="0" />
                                                            
                                                            <%--  Enable the button so it can be played again --%>
                                                            <EnableAction AnimationTarget="btnHelp" Enabled="true" />
                                                        </Sequence>
                                                    </OnClick>
                                                    <OnMouseOver>
                                                        <Color Duration=".2" PropertyKey="color" StartValue="#FFFFFF" EndValue="#FF0000" />
                                                    </OnMouseOver>
                                                    <OnMouseOut>
                                                        <Color Duration=".2" PropertyKey="color" StartValue="#FF0000" EndValue="#FFFFFF" />
                                                    </OnMouseOut>
                        </Animations>
                    </ajaxToolKit:AnimationExtender>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <div style="color: Red; font-weight: bold">
                        &nbsp;Note : <span class="validstar">*</span> marked fields are Mandatory
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <fieldset>
                        <legend class="legend">Enter Student Data</legend>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td width="20%" class="form_left_text">
                                    <span class="validstar"></span>Admission Batch
                                </td>
                                <td width="30%" colspan="2" class="form_left_text">
                                    <asp:Label ID="lblAdmBatch" Font-Bold="true" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="20%" class="form_left_text">
                                    <span class="validstar">*</span>Degree Name :</td>
                                <td width="30%" colspan="2" class="form_left_text">

                                    <asp:DropDownList ID="ddlDegreesel" runat="server" AppendDataBoundItems="true"
                                        Width="25%">
                                        <asp:ListItem>Please Select </asp:ListItem>
                                    </asp:DropDownList>

                                    <asp:RequiredFieldValidator ID="rfvddlDegree" runat="server" ControlToValidate="ddlDegreesel"
                                        Display="None" ValidationGroup="Academic" InitialValue="0"
                                        ErrorMessage="Please Select Degree"></asp:RequiredFieldValidator>

                                </td>
                            </tr>
                            <tr>
                                <td class="form_left_text" style="width: 20%">
                                    <span class="validstar">*</span>Qualifiying. Exam:
                                </td>
                                <td width="30%" colspan="2" class="form_left_text">
                                    <asp:DropDownList ID="ddlExamName" runat="server" Width="25%" AppendDataBoundItems="false"
                                        AutoPostBack="false" TabIndex="1">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        <asp:ListItem Value="1">JEE(MAIN)</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddlExamName" runat="server" ControlToValidate="ddlExamName"
                                        Display="None" ValidationGroup="Academic" InitialValue="0" ErrorMessage="Please Select Exam name">
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td rowspan="2">
                                    <fieldset>
                                        <span style="padding: 5px; color: Red"><b>NOTE : Data can be entered only once</b>
                                        </span>
                                    </fieldset>
                                </td>
                            </tr>
                            <tr>
                                <td class="form_left_text" style="width: 20%">
                                    <span class="validstar">*</span>JEE(MAIN) Exam Roll No.:
                                </td>
                                <td width="30%" colspan="2" class="form_left_text">
                                    <asp:TextBox ID="txtStudentRoll" runat="server" Width="25%" TabIndex="2" ToolTip="Please Enter Student Roll No." />
                                    <asp:RequiredFieldValidator ID="rfvtxtStudentRoll" runat="server" ValidationGroup="Academic"
                                        ErrorMessage="Please Enter Name" Display="None" ControlToValidate="txtStudentRoll"
                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtStudentRoll" runat="server" TargetControlID="txtStudentRoll"
                                        FilterType="Numbers" />
                                </td>
                            </tr>
                            <tr>
                                <td class="form_left_text" style="width: 20%">
                                    <span class="validstar">*</span>Date of Birth:
                                </td>
                                <td width="30%" colspan="2" class="form_left_text">
                                    <asp:TextBox ID="txtDateOfBirth" runat="server" Width="25%" TabIndex="3"
                                        ToolTip="Please Enter Date Of Birth" />
                                    <asp:Image ID="imgCalDateOfBirth" runat="server" src="../IMAGES/calendar1.png" Style="cursor: pointer"
                                        Height="16px" TabIndex="4" />
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
                                    <ajaxToolKit:MaskedEditValidator ID="mevDateOfBirth" runat="server" ControlExtender="meeDateOfBirth"
                                        ControlToValidate="txtDateOfBirth" IsValidEmpty="False" InvalidValueMessage="Date is invalid"
                                        Display="None" TooltipMessage="Input a date" ErrorMessage="Please Select Date"
                                        EmptyValueBlurredText="*" InvalidValueBlurredMessage="*" ValidationGroup="Academic"
                                        SetFocusOnError="True" />
                                </td>
                                <td class="form_left_text">&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="form_left_text" style="width: 15%">
                                    <asp:Label runat="server" ID="lblRound" Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="form_left_text" style="width: 20%">&nbsp;
                                </td>
                                <td class="form_left_text" style="width: 30%">
                                    <asp:Button ID="btnShow" runat="server" ValidationGroup="Academic" OnClick="btnShow_Click"
                                        TabIndex="5" Text="Show" ToolTip="Show Form" />
                                    <asp:ValidationSummary ID="rfvValidationSummary" runat="server" ValidationGroup="Academic"
                                        DisplayMode="List" ShowMessageBox="True" ShowSummary="False" />
                                    <asp:Button ID="btnCancel" runat="server" TabIndex="6" Text="Cancel" ToolTip="Click To Cancel"
                                        OnClick="btnCancel_Click" />
                                    <br />
                                    <br />
                                </td>
                                <td class="form_left_text" style="width: 15%">&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:Panel ID="pnlStudent" Visible="false" runat="server">
                                        <table cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td colspan="4" class="section_header">Personal Details
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="15%" class="form_left_label">Candidate Name:
                                                </td>
                                                <td width="30%" class="form_left_text">
                                                    <asp:TextBox ID="txtStudentName" runat="server" Width="75%" TabIndex="7"
                                                        ToolTip="Please Enter Candidate's Name" />
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="fttxtStudentName" runat="server" TargetControlID="txtStudentName"
                                                        FilterType="Custom" FilterMode="InvalidChars" InvalidChars="1234567890~!@#$%^&*()_+|}{:|][',.:;\/" />
                                                    <asp:RequiredFieldValidator ID="rfvtxtStudentName1" runat="server" ControlToValidate="txtStudentName"
                                                        Display="None" ErrorMessage="Please Enter Candidate Name" ValidationGroup="academic"
                                                        SetFocusOnError="true">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td width="15%" class="form_left_label">Mobile Number:
                                                </td>
                                                <td width="30%" class="form_left_text">
                                                    <asp:TextBox ID="txtMobileNo" runat="server" TabIndex="8" ValidationGroup="academic"
                                                        MaxLength="20" ToolTip="Please Enter Contact Number" Width="50%" />
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="fteTxtContactNum" runat="server" FilterType="Numbers"
                                                        TargetControlID="txtMobileNo">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="15%" class="form_left_label">Father's Name:
                                                </td>
                                                <td class="style5">
                                                    <asp:TextBox ID="txtFatherName" runat="server" Width="75%" TabIndex="9"
                                                        ToolTip="Please Enter Father's Name" />
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                        TargetControlID="txtFatherName" FilterType="Custom" FilterMode="InvalidChars"
                                                        InvalidChars="1234567890~!@#$%^&*()_+|}{:|][',.:;\/" />
                                                    <asp:RequiredFieldValidator ID="rfvFatherName" runat="server" ControlToValidate="txtFatherName"
                                                        Display="None" ErrorMessage="Please Enter Father Name" ValidationGroup="academic"
                                                        SetFocusOnError="true">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td width="15%" class="form_left_label">Mother's Name:
                                                </td>
                                                <td class="style5">
                                                    <asp:TextBox ID="txtMotherName" runat="server" Width="75%" TabIndex="10"
                                                        ToolTip="Please Enter Mother's name" />
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                                        TargetControlID="txtMotherName" FilterType="Custom" FilterMode="InvalidChars"
                                                        InvalidChars="1234567890~!@#$%^&*()_+|}{:|][',.;\/{}-" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="15%" class="form_left_label">Email :
                                                </td>
                                                <td class="style5">
                                                    <asp:TextBox ID="txtStudEmail" runat="server" Width="55%" ToolTip="Please Enter Email"
                                                        TabIndex="11" />
                                                    <asp:RegularExpressionValidator ID="rfvStudEmail" runat="server" ControlToValidate="txtStudEmail"
                                                        Display="None" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                        ErrorMessage="Please Enter Valid EmailID" ValidationGroup="academic"></asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="15%" class="form_left_label">Gender:
                                                </td>
                                                <td class="form_left_label">
                                                    <asp:RadioButtonList ID="rdoGender" runat="server" RepeatDirection="Horizontal"
                                                        Width="182px" TabIndex="12">
                                                        <asp:ListItem Text="Male" Selected="True" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Female" Value="1"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td width="30%" class="form_left_label">Marital Status:
                                                </td>
                                                <td width="15%" class="form_left_label">
                                                    <asp:RadioButtonList ID="rdbMaritalStatus" runat="server"
                                                        RepeatDirection="Horizontal" TabIndex="13">
                                                        <asp:ListItem Text="Single" Selected="True" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Married" Value="1"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="15%" class="form_left_label">Nationality:
                                                </td>
                                                <td width="30%" class="form_left_text">
                                                    <asp:DropDownList ID="ddlNationality" runat="server" Width="70%" TabIndex="14" AppendDataBoundItems="True"
                                                        ToolTip="Please Select Nationality">
                                                        <asp:ListItem>Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td width="15%" class="form_left_label">Religion:
                                                </td>
                                                <td width="30%" class="form_left_text">
                                                    <asp:DropDownList ID="ddlReligion" runat="server" Width="70%" TabIndex="15" AppendDataBoundItems="true"
                                                        ToolTip="Please Select Religion">
                                                        <asp:ListItem>Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td width="15%" class="form_left_label">Blood Group:
                                                </td>
                                                <td width="30%" class="form_left_text">
                                                    <asp:DropDownList ID="ddlBloodGroupNo" runat="server" Width="70%" AppendDataBoundItems="True"
                                                        TabIndex="16">
                                                        <asp:ListItem>Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td width="15%" class="form_left_label">Physically Handicapped:
                                                </td>
                                                <td width="30%" class="form_left_text">
                                                    <asp:RadioButtonList ID="rdbPH" runat="server" RepeatDirection="Horizontal"
                                                        Width="176px" TabIndex="17">
                                                        <asp:ListItem Value="1" Selected="True">
                                                    No
                                                        </asp:ListItem>
                                                        <asp:ListItem Value="2">
                                                   Yes
                                                        </asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="15%" class="form_left_label">Admission Category:
                                                </td>
                                                <td width="30%" class="form_left_text">
                                                    <asp:DropDownList ID="ddlAdmissionCategory" runat="server" Width="70%"
                                                        TabIndex="18" AppendDataBoundItems="True"
                                                        ToolTip="Please Select Admission Category" Enabled="False">
                                                        <asp:ListItem>Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td width="15%" class="form_left_label">Actual Category :
                                                </td>
                                                <td width="30%" class="form_left_text">
                                                    <asp:DropDownList ID="ddlApplicantCategory" Enabled="false" runat="server" Width="70%"
                                                        TabIndex="19" AppendDataBoundItems="false"
                                                        ToolTip="Please Select Category">
                                                        <asp:ListItem>Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;
                                                </td>
                                                <td>&nbsp;
                                                </td>
                                            </tr>

                                            <tr>
                                                <td colspan="4" class="section_header">Address and Contact Details
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="15%" class="form_left_label">Permanent
                                                    <br />
                                                    Address:
                                                </td>
                                                <td width="30%" colspan="3" class="form_left_text">
                                                    <asp:TextBox ID="txtPermanentAddress" runat="server" Height="70px" TabIndex="19"
                                                        TextMode="MultiLine" ToolTip="Please Enter Permanent Address" Width="46%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="15%" class="form_left_label">City:
                                                </td>
                                                <td width="30%" class="form_left_text">
                                                    <asp:DropDownList ID="ddlCity" runat="server" Width="50%" AppendDataBoundItems="true"
                                                        ToolTip="Please Enter City" TabIndex="20">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td width="15%" class="form_left_label">State:
                                                </td>
                                                <td width="30%" class="form_left_text">
                                                    <asp:DropDownList ID="ddlState" AppendDataBoundItems="true" runat="server" Width="75%"
                                                        ToolTip="Please Enter State" TabIndex="21">
                                                        <asp:ListItem>Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="15%" class="form_left_label">PIN:
                                                </td>
                                                <td class="style5">
                                                    <asp:TextBox ID="txtPIN" runat="server" TabIndex="22" MaxLength="6" ToolTip="Please Enter PIN"
                                                        Width="35%" />
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="fteTxtPin" runat="server" FilterType="Numbers"
                                                        TargetControlID="txtPIN">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                    <asp:RequiredFieldValidator ID="rfvtxtPin" runat="server" ControlToValidate="txtPIN"
                                                        Display="None" ErrorMessage="Please Enter Pin" ValidationGroup="Academic"
                                                        SetFocusOnError="true">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                                <td width="15%" class="form_left_label">Contact Phone Number:
                                                </td>
                                                <td width="30%" class="form_left_text">
                                                    <asp:TextBox ID="txtConatctNo" runat="server" Width="35%" TabIndex="23" MaxLength="10"
                                                        ToolTip="Please Enter Parent's Number" />
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtConatctNo" runat="server" TargetControlID="txtConatctNo"
                                                        FilterType="Numbers" />
                                                    <asp:RequiredFieldValidator ID="rfvtxtConatctNo" runat="server" ControlToValidate="txtConatctNo"
                                                        Display="None" ErrorMessage="Please Enter Conatct Number" ValidationGroup="Academic"
                                                        SetFocusOnError="true">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style2">&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top" class="style1">Copy Permanent
                                                    <br />
                                                    Address:
                                                </td>
                                                <td colspan="3" class="form_left_text">
                                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/copy.png" OnClientClick="copyPermAddr(this)"
                                                        ToolTip="Copy Permanent Address" TabIndex="24" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="15%" class="form_left_label">Local<br />
                                                    Address:
                                                </td>
                                                <td colspan="3" class="form_left_text">
                                                    <asp:TextBox ID="txtPostalAddress" runat="server" Height="70px" TabIndex="25" TextMode="MultiLine"
                                                        ToolTip="Please Enter Postal Address" Width="46%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="15%" class="form_left_label">Local City:
                                                </td>
                                                <td class="form_left_text">
                                                    <asp:DropDownList ID="ddlLocalCity" runat="server" Width="50%" ToolTip="Please Enter Local City"
                                                        TabIndex="26" AppendDataBoundItems="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td width="15%" class="form_left_label">Local State:
                                                </td>
                                                <td width="30%" class="form_left_text">
                                                    <asp:DropDownList ID="ddlLocalState" runat="server" Width="75%" ToolTip="Please Enter Local State"
                                                        TabIndex="27" AppendDataBoundItems="true">
                                                        <asp:ListItem>Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="15%" class="form_left_label">Landline No:
                                                </td>
                                                <td class="style5">
                                                    <asp:TextBox ID="txtGuardianPhone" runat="server" Width="35%" ToolTip="Please Enter Guardian Phone No."
                                                        TabIndex="28" />
                                                </td>
                                                <td width="15%" class="form_left_label">Mobile No.:
                                                </td>
                                                <td width="30%" class="form_left_text">
                                                    <asp:TextBox ID="txtGuardianMobile" runat="server" Width="35%" ToolTip="Please Enter Guardian Mobile No."
                                                        TabIndex="29" />
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                                                        FilterType="Numbers" TargetControlID="txtGuardianMobile">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                    <asp:CompareValidator ID="rfvGuardianMobileNo" runat="server" ControlToValidate="txtGuardianMobile"
                                                        ErrorMessage="Please Numeric Number" Operator="DataTypeCheck" SetFocusOnError="True"
                                                        Type="Integer" ValidationGroup="Academic" Display="None"></asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="15%" class="form_left_label">Email:
                                                </td>
                                                <td class="style5">
                                                    <asp:TextBox ID="txtGuardianEmail" runat="server" TabIndex="30" ToolTip="Please Enter Email"
                                                        Width="35%" />
                                                    <asp:RegularExpressionValidator ID="rfvGuardianEmail" runat="server" ControlToValidate="txtGuardianEmail"
                                                        Display="None" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                        ErrorMessage="Please Enter Valid EmailID" ValidationGroup="academic"></asp:RegularExpressionValidator>
                                                </td>
                                                <td class="style3"></td>
                                                <td width="30%">&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" class="section_header">Other Details
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    <table cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td width="17%" class="form_left_text">All India Rank:
                                                            </td>
                                                            <td width="17%" class="form_left_text">
                                                                <asp:TextBox ID="txtAllIndiaRank" Enabled="false" runat="server" Width="35%" ToolTip="Please Enter All India Rank"
                                                                    TabIndex="27"></asp:TextBox>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="fteAllIndiaRank" runat="server" FilterType="Numbers"
                                                                    TargetControlID="txtAllIndiaRank">
                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                            </td>
                                                            <td width="17%" class="form_left_text">Exam Roll No:
                                                            </td>
                                                            <td width="15%" class="form_left_text">
                                                                <asp:TextBox ID="txtExamRollNo" Enabled="false" runat="server" Width="80%" ToolTip="Please Enter Qualifying Exam Roll No"
                                                                    TabIndex="31"></asp:TextBox>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftrQExamRollNo" runat="server" FilterType="Numbers"
                                                                    TargetControlID="txtExamRollNo">
                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                            </td>
                                                            <td width="17%" class="form_left_text">Year Of Exam
                                                            </td>
                                                            <td width="17%" class="form_left_text">
                                                                <asp:TextBox ID="txtExamYear" Enabled="false" runat="server" Width="20%" MaxLength="4"
                                                                    ToolTip="Please Enter Exam Year" TabIndex="31"></asp:TextBox>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                                    FilterType="Numbers" TargetControlID="txtExamYear">
                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="17%" class="form_left_text">Score
                                                            </td>
                                                            <td width="17%" class="form_left_text">
                                                                <asp:TextBox ID="txtExamScore" Enabled="false" runat="server" Width="20%" MaxLength="4"
                                                                    ToolTip="Please Enter Exam Score" TabIndex="31"></asp:TextBox>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtExamScore" runat="server" FilterType="Numbers"
                                                                    TargetControlID="txtExamScore">
                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                            </td>
                                                            <td width="15%" class="form_left_text">Quota:
                                                            </td>
                                                            <td style="width: 18%" class="form_left_text">
                                                                <asp:DropDownList Enabled="false" ID="ddlQuota" Width="60%" runat="server" AppendDataBoundItems="true"
                                                                    TabIndex="32">
                                                                    <asp:ListItem>Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td id="tdpaper" runat="server" width="17%" class="form_left_text">Paper(Only for Gate)
                                                            </td>
                                                            <td width="15%" class="form_left_text">
                                                                <asp:TextBox ID="txtExamPaper" Enabled="false" runat="server" Width="20%" ToolTip="Please Enter Gate Paper"
                                                                    TabIndex="31"></asp:TextBox>&nbsp;
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtExamPaper" runat="server" FilterType="Numbers"
                                                                    TargetControlID="txtExamPaper">
                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" class="section_header">Admission Details
                                                </td>
                                            </tr>

                                            <tr>

                                                <td width="10%" class="form_left_label">Branch :
                                                </td>
                                                <td width="30%" class="form_left_label">
                                                    <asp:DropDownList ID="ddlBranch" Enabled="false" runat="server" Width="100%">
                                                    </asp:DropDownList>
                                                </td>

                                            </tr>

                                            <tr>
                                                <td width="15%" class="form_left_label">Payment Type : </td>
                                                <td width="30%" class="form_left_text">
                                                    <asp:DropDownList ID="ddlPaymentType" runat="server"
                                                        AppendDataBoundItems="True" ToolTip="Please Select Payment Type"
                                                        Width="50%" TabIndex="31"
                                                        OnSelectedIndexChanged="ddlPaymentType_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        <asp:ListItem Value="1">General</asp:ListItem>
                                                        <asp:ListItem Value="2">SC/ST</asp:ListItem>
                                                        <asp:ListItem Value="3">DASA</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvddlpaytype" runat="server"
                                                        ControlToValidate="ddlPaymentType" Display="None"
                                                        ErrorMessage="Please Select Payment Type" InitialValue="0"
                                                        ValidationGroup="academic"></asp:RequiredFieldValidator>
                                                </td>
                                                <td width="15%" class="form_left_label">
                                                    <asp:CheckBox ID="chkHostel" Text="Hostel" runat="server" />
                                                </td>

                                            </tr>
                                            <tr>
                                                <td width="17%" class="form_left_label">Paid Amount By CSAB :
                                                </td>
                                                <td class="form_left_label">
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers"
                                                        TargetControlID="txtPIN">
                                                    </ajaxToolKit:FilteredTextBoxExtender>
                                                    <asp:TextBox ID="txtPaidCCB" runat="server" onkeyup="IsNumeric(this);"
                                                        Width="35%" TabIndex="32"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="CCBpaidAmt" runat="server"
                                                        ControlToValidate="txtPaidCCB" Display="None"
                                                        ErrorMessage="Please enter CSAB paid amount." SetFocusOnError="true"
                                                        ValidationGroup="academic" />
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txtPaidCCB"
                                                        FilterType="Numbers" />
                                                </td>
                                                <td width="15%" class="form_left_label"></td>
                                                <td width="30%" class="form_left_text">
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtConatctNo"
                                                        FilterType="Numbers" />
                                                </td>
                                            </tr>
                                            <tr style="display: none">
                                                <td class="form_left_label" width="15%">Pay Type(C/D):</td>
                                                <td class="form_left_label">
                                                    <asp:TextBox ID="txtPayType" runat="server" Width="35%"
                                                        onblur="ValidatePayType(this); UpdateCash_DD_Amount();" MaxLength="1"
                                                        ToolTip="Enter C for cash payment OR D for payment by demand draft."></asp:TextBox>
                                                </td>
                                                <td class="form_left_label" width="15%">&nbsp;</td>
                                                <td class="form_left_text" width="30%">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="form_left_label" width="15%" valign="top">Remark :</td>
                                                <td class="form_left_label" colspan="3">
                                                    <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Width="60%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="form_left_label" width="15%">&nbsp;</td>
                                                <td class="form_left_label">&nbsp;</td>
                                                <td class="form_left_label" width="15%">&nbsp;</td>
                                                <td class="form_left_text" width="30%">&nbsp;</td>
                                            </tr>
                                            <tr style="display: none">
                                                <td colspan="4">
                                                    <div id="divDDDetails" runat="server">
                                                        <fieldset class="fieldset">
                                                            <legend>Demand Draft Details</legend>
                                                            <table>
                                                                <tr>
                                                                    <td>DD/Check No.:
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtDDNo" runat="server" TabIndex="6" CssClass="data_label" />
                                                                        <asp:RequiredFieldValidator ID="valDDNo" ControlToValidate="txtDDNo" runat="server"
                                                                            Display="None" ErrorMessage="Please enter demand draft number." ValidationGroup="dd_info" />
                                                                    </td>
                                                                    <td>Amount:
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtDDAmount" onkeyup="IsNumeric(this);" runat="server" TabIndex="7"
                                                                            CssClass="data_label" />
                                                                        <asp:RequiredFieldValidator ID="valDdAmount" ControlToValidate="txtDDAmount" runat="server"
                                                                            Display="None" ErrorMessage="Please enter amount of demand draft." ValidationGroup="dd_info" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Date:
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtDDDate" runat="server" TabIndex="8" CssClass="data_label" />
                                                                        <asp:Image ID="imgCalDDDate" runat="server" src="../images/calendar.png" Style="cursor: hand" />
                                                                        <ajaxToolKit:CalendarExtender ID="ceDDDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDDDate"
                                                                            PopupButtonID="imgCalDDDate" />
                                                                        <ajaxToolKit:MaskedEditExtender ID="meeDDDate" runat="server" TargetControlID="txtDDDate"
                                                                            Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" ErrorTooltipEnabled="true"
                                                                            OnInvalidCssClass="errordate" />
                                                                        <ajaxToolKit:MaskedEditValidator ID="mevDDDate" runat="server" ControlExtender="meeDDDate"
                                                                            ControlToValidate="txtDDDate" IsValidEmpty="False" EmptyValueMessage="Demand draft date is required"
                                                                            InvalidValueMessage="Demand draft date is invalid" EmptyValueBlurredText="*"
                                                                            InvalidValueBlurredMessage="*" Display="Dynamic" ValidationGroup="dd_info" />
                                                                    </td>
                                                                    <td>City:
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtDDCity" runat="server" TabIndex="9" CssClass="data_label" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Bank:
                                                                    </td>
                                                                    <td colspan="2">
                                                                        <asp:DropDownList ID="ddlBank" AppendDataBoundItems="true" TabIndex="10" runat="server"
                                                                            Width="250px" />
                                                                        <asp:RequiredFieldValidator ID="valBankName" runat="server" ControlToValidate="ddlBank"
                                                                            Display="None" ErrorMessage="Please select bank name." ValidationGroup="dd_info"
                                                                            InitialValue="0" SetFocusOnError="true" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Button ID="btnSaveDD_Info" runat="server" Text="Save Demand Draft"
                                                                            ValidationGroup="dd_info" TabIndex="11" />
                                                                        <asp:ValidationSummary ID="valSummery2" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                                            ShowSummary="false" ValidationGroup="dd_info" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </fieldset>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3"></td>
                                            </tr>
                                            <tr>
                                                <td class="style2">&nbsp;
                                                </td>
                                            </tr>

                                            <tr>
                                                <td colspan="4" class="form_button">
                                                    <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click"
                                                        TabIndex="33" Text="Submit" ValidationGroup="academic"
                                                        ToolTip="Submit Info" />
                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="academic"
                                                        DisplayMode="List" ShowMessageBox="True" ShowSummary="False" />
                                                    <asp:Button ID="btnClear" runat="server" TabIndex="34" Text="Clear" ToolTip="Click To Clear"
                                                        OnClick="btnCancel_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>

                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
        </table>
        <div id="divMsg" runat="server">
        </div>

        <script type="text/javascript">
            function copyPermAddr(chk) {
                if (chk.click) {
                    var city = document.getElementById('<%= ddlCity.ClientID %>').value;
                    var state = document.getElementById('<%= ddlState.ClientID %>').value;
                    document.getElementById('<%= txtPostalAddress.ClientID %>').value = document.getElementById('<%= txtPermanentAddress.ClientID %>').value
                    document.getElementById('<%= ddlLocalCity.ClientID %>').value = city;
                    document.getElementById('<%= ddlLocalState.ClientID %>').value = state;

                }
                else {
                    document.getElementById('<%= txtPostalAddress.ClientID %>').value = '';
                }
            }

        </script>

    </div>
    </form>

</asp:Content>
