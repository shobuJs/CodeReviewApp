<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Semester_Promotion.aspx.cs" Inherits="ACADEMIC_Semester_Promotion" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        $(function () {
            $("#<%=txtRecDate.ClientID%>").datepicker({
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
    </script>
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td class="vista_page_title_bar" valign="top" style="height: 30px">SEMESTER PROMOTION
                <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>

        <tr>
            <td>
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
                            Edit Record
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

                <ajaxToolKit:AnimationExtender ID="AnimationExtender1" runat="server" TargetControlID="btnHelp">
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
            </td>
        </tr>
    </table>
    <br />
    <div style="color: Red; font-weight: bold">
        &nbsp;Note : * marked fields are Mandatory
    </div>
    <fieldset class="fieldset1">
        <legend class="legend">Semester Promotion</legend>
        <table width="100%" cellpadding="2" cellspacing="2" border="0">
            <tr>
                <td width="20%">&nbsp;</td>
                <td>
                    <asp:ValidationSummary ID="vsShow" runat="server" DisplayMode="List"
                        ShowMessageBox="true" ShowSummary="false" ValidationGroup="Show" />
                </td>
            </tr>
            <tr>
                <td width="20%">
                    <span class="validstar">*</span>Scheme Type :
                </td>
                <td>
                    <asp:DropDownList ID="ddlSType" runat="server" Width="200px"
                        AppendDataBoundItems="True">
                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvSchemetype" runat="server"
                        ErrorMessage="Please Select Scheme Type" ControlToValidate="ddlSType"
                        Display="None" ValidationGroup="Show" InitialValue="0"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td width="20%">
                    <span class="validstar">*</span>Enrollment No :
                </td>
                <td>
                    <asp:TextBox ID="txtEnrollmentNo" runat="server" TabIndex="1" Width="150px"></asp:TextBox>
                    &nbsp;<asp:Button ID="btnShow" runat="server" Text="Show" TabIndex="5" ValidationGroup="Show"
                        OnClick="btnShow_Click" />
                    <asp:RequiredFieldValidator ID="rfvEnrollmentno" runat="server" SetFocusOnError="true"
                        ControlToValidate="txtEnrollmentNo" Display="None" ErrorMessage="Please Enter Enrollment Number."
                        ValidationGroup="Show"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td width="20%">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
        <asp:Panel ID="infoPnl" runat="server" Visible="false">
            <table width="100%" cellpadding="2" cellspacing="2" border="0">
                <tr>
                    <td width="20%" style="height: 22px">Name :
                    </td>
                    <td style="height: 22px">
                        <asp:Label ID="lblName" runat="server" Font-Bold="True" Font-Italic="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="20%">Branch :
                    </td>
                    <td>
                        <asp:Label ID="lblBranch" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="20%">Session :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True"
                            TabIndex="2" Width="150px">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvSession" runat="server"
                            ControlToValidate="ddlSession" Display="None"
                            ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="true"
                            ValidationGroup="submit"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td width="20%">Semester :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSemester" runat="server" TabIndex="3" Width="100px"
                            AppendDataBoundItems="True">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvSemester" runat="server"
                            ControlToValidate="ddlSemester" Display="None"
                            ErrorMessage="Please Select Semester" InitialValue="0" SetFocusOnError="true"
                            ValidationGroup="submit"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td width="20%">Section : </td>
                    <td>
                        <asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="True"
                            TabIndex="9" Width="100px">
                        </asp:DropDownList>

                    </td>
                </tr>
                <tr>
                    <td width="20%">Roll No :</td>
                    <td>
                        <asp:TextBox ID="txtRollNo" runat="server" Width="150px" TabIndex="1" />
                    </td>
                </tr>
                <tr>
                    <td width="20%">Reciept No :
                    </td>
                    <td>
                        <asp:TextBox ID="txtRecNo" runat="server" TabIndex="1" Width="150px"></asp:TextBox>
                        &nbsp;<asp:RequiredFieldValidator ID="rfvreceiptNo" runat="server"
                            ControlToValidate="txtRecNo" Display="None"
                            ErrorMessage="Please Enter Receipt No" SetFocusOnError="true"
                            ValidationGroup="submit"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td width="20%">Reciept Date :
                    </td>
                    <td>
                        <asp:TextBox ID="txtRecDate" runat="server" TabIndex="7" Width="13%"
                            ToolTip="Please Enter Date Of Birth"></asp:TextBox>
                        <asp:Image ID="imgCalDateOfBirth" runat="server" Height="16px"
                            src="../images/calendar.png" Style="cursor: pointer" TabIndex="8" />

                        <ajaxToolKit:MaskedEditExtender ID="meeDateOfBirth" runat="server"
                            AcceptAMPM="True" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder=""
                            CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True"
                            ErrorTooltipEnabled="True" Mask="99/99/9999" MaskType="Date"
                            TargetControlID="txtRecDate" />
                        <ajaxToolKit:MaskedEditValidator ID="mevDateOfBirth" runat="server"
                            ControlExtender="meeDateOfBirth" ControlToValidate="txtRecDate" Display="None"
                            EmptyValueBlurredText="*" EmptyValueMessage="Please Enter Receipt Date"
                            ErrorMessage="Please Select Date" InvalidValueBlurredMessage="*"
                            InvalidValueMessage="Date is invalid" IsValidEmpty="False"
                            SetFocusOnError="True" TooltipMessage="Input a date" ValidationGroup="submit" />
                    </td>
                </tr>
                <tr>
                    <td width="20%">Reciept Amount :
                    
                    </td>
                    <td>
                        <asp:TextBox ID="txtRecAmt" runat="server" TabIndex="1" Width="150px"></asp:TextBox>
                        &nbsp;<asp:RequiredFieldValidator ID="rfvAmount" runat="server"
                            ControlToValidate="txtRecAmt" Display="None"
                            ErrorMessage="Please Enter Receipt Amount" SetFocusOnError="true"
                            ValidationGroup="submit"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td width="20%">
                        <asp:ValidationSummary ID="vsPayroll" runat="server" DisplayMode="List"
                            ShowMessageBox="true" ShowSummary="false" ValidationGroup="submit"
                            Width="143px" />
                    </td>
                    <td>
                        <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click"
                            TabIndex="5" Text="Update" ValidationGroup="submit" />
                        &nbsp;
                        <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click"
                            TabIndex="10" Text="Cancel" />
                    </td>
                </tr>
                <tr>
                    <td width="20%">&nbsp;</td>
                    <td>
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                            ValidationGroup="qq" />
                    </td>
                </tr>

            </table>
        </asp:Panel>
        <asp:Panel ID="pnlStud" runat="server" Visible="false">
            <fieldset class="fieldset1">
                <legend class="legend2">Student List</legend>
                <table cellpadding="1" cellspacing="1" width="100%">
                    <tr>
                        <td>
                            <asp:ListView ID="lvStudents" runat="server">
                                <LayoutTemplate>
                                    <div id="listViewGrid" class="vista-grid">
                                        <div class="titlebar">
                                            Students Information
                                        </div>
                                        <table id="tblSearchResults" cellpadding="0" cellspacing="0" class="datatable">
                                            <tr class="header">
                                                <th>Edit
                                                </th>
                                                <th>Roll No
                                                </th>
                                                <th>Name
                                                </th>
                                                <th>Branch
                                                </th>
                                                <th>Semester
                                                </th>
                                                <th>Section
                                                </th>
                                            </tr>
                                            <tr id="itemPlaceholder" runat="server" />
                                        </table>
                                    </div>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr class="item" onmouseout="this.style.backgroundColor='#FFFFFF'" onmouseover="this.style.backgroundColor='#FFFFAA'">
                                        <td>
                                            <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("PROMOTIONNO")%>'
                                                ImageUrl="~/images/edit.gif" OnClick="btnEdit_Click" />
                                        </td>
                                        <td>
                                            <%# Eval("ROLLNO")%>
                                        </td>
                                        <td>
                                            <%# Eval("STUDNAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("BRANCH")%>
                                        </td>
                                        <td>
                                            <%# Eval("SEMESTER")%>
                                        </td>
                                        <td>
                                            <%# Eval("SECTION")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>

    </fieldset>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>

