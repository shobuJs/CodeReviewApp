<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="BranchChange.aspx.cs" Inherits="ACADEMIC_BranchChange" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">

        $(function () {
            $("#<%=txtAdmDate.ClientID%>").datepicker({
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
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="vista_page_title_bar" style="height: 30px">STUDENT BRANCH CHANGE
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

                <ajaxToolKit:AnimationExtender ID="AnimationExtender1" runat="server" TargetControlID="btnHelp">
                    <Animations>
                    <OnClick>
                        <Sequence>
                            <%-- Disable the button so it can't be clicked again --%>
                            <EnableAction Enabled="false" />
                            
                            <%-- Position the wire frame on top of the button and show it --%>
                            <ScriptAction Script="Cover($get('ctl00$ContentPlaceHolder1$btnHelp'), $get('flyout'));" />
                            <StyleAction AnimationTarget="flyout" Attribute="display" Value="block"/>
                            
                            <%-- Move the wire frame from the button's bounds to the info panel's bounds --%>
                            <Parallel AnimationTarget="flyout" Duration=".3" Fps="25">
                                <Move Horizontal="150" Vertical="-50" />
                                <Resize Width="260" Height="280" />
                                <Color PropertyKey="backgroundColor" StartValue="#AAAAAA" EndValue="#FFFFFF" />
                            </Parallel>
                            
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
    </table>
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td style="padding-left: 10px; padding-right: 10px;">

                <br />
                <div style="color: Red; font-weight: bold">
                    &nbsp;Note : * marked fields are Mandatory
                </div>
                <fieldset class="fieldset">
                    <legend class="legend">Branch Change</legend>
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td>
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td colspan="2">
                                            <asp:RadioButtonList ID="rblBranchChange" runat="server"
                                                RepeatDirection="Horizontal" AutoPostBack="True"
                                                OnSelectedIndexChanged="rblBranchChange_SelectedIndexChanged">
                                                <asp:ListItem Value="1" Selected="True">First Year Branch Change</asp:ListItem>
                                                <asp:ListItem Value="2">Other year Branch Change</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="5%"></td>
                                        <td class="form_left_text" style="width: 25%; height: 20px;"></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 5%;"></td>
                                        <td class="form_left_text" style="width: 25%;">
                                            <asp:Label ID="lblPreSession" runat="server" Font-Bold="true" Visible="false" />
                                        </td>
                                    </tr>
                                    <tr id="trEnrollno" runat="server">
                                        <td class="form_left_text" style="width: 5%;">
                                            <span class="validstar">*</span>Enroll No. :</td>
                                        <td class="form_left_text" style="width: 25%;">
                                            <asp:TextBox ID="txtEnrollNo" Width="20%" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="trRegNo" runat="server" visible="false">
                                        <td class="form_left_text" style="width: 15%;">
                                            <span class="validstar">*</span>Roll Number:
                                        </td>
                                        <td class="form_left_text" style="width: 35%">
                                            <asp:TextBox ID="txtStudent" runat="server" Width="20%" onkeypress="return getRegNo(event)" />
                                            &nbsp;<asp:RequiredFieldValidator ID="rfvRollNumber" runat="server"
                                                ControlToValidate="txtStudent" Display="None"
                                                ErrorMessage="Please Enter Roll Number" ValidationGroup="Show"></asp:RequiredFieldValidator><asp:RequiredFieldValidator ID="rfvRollNumber_submit" runat="server"
                                                    ControlToValidate="txtStudent" Display="None"
                                                    ErrorMessage="Please Enter Roll Number" ValidationGroup="Submit"></asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_text">
                                            <span class="validstar">&nbsp</span>Student Name :
                                        </td>
                                        <td class="form_left_text">
                                            <asp:Label ID="lblName" runat="server" Font-Bold="True"></asp:Label>
                                            <asp:Label ID="lblAdmbatch" runat="server" Font-Bold="True" Visible="false"></asp:Label>
                                            <asp:Label ID="lblYear" runat="server" Font-Bold="True" Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_text">
                                            <span class="validstar">&nbsp</span>Current Branch :
                                        </td>
                                        <td class="form_left_text">
                                            <asp:Label ID="lblBranch" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_text">
                                            <span class="validstar">&nbsp</span>Roll No. :
                                        </td>
                                        <td class="form_left_text">
                                            <asp:Label ID="lblRegNo" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="form_left_text" style="display: none;">
                                        <td>
                                            <span class="validstar">&nbsp</span>Admission Round :</td>
                                        <td class="form_left_text">
                                            <asp:DropDownList ID="ddlAdmRound" runat="server" Width="130px" AppendDataBoundItems="true" AutoPostBack="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvAdmRound" runat="server" Display="None" ControlToValidate="ddlAdmRound"
                                                ErrorMessage="Please Select Admission Round" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>

                                    <tr style="display: none;">
                                        <td class="form_left_text">
                                            <span class="validstar">&nbsp</span>Admission Date :
                                        </td>
                                        <td class="form_left_text">
                                            <asp:TextBox ID="txtAdmDate" runat="server" Width="110px" />
                                            <asp:Image ID="imgAdmDate" runat="server" src="../images/calendar.png" Width="16px" />

                                            <ajaxToolKit:MaskedEditExtender ID="meeAdmDate" runat="server" TargetControlID="txtAdmDate"
                                                Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True" />
                                            <ajaxToolKit:MaskedEditValidator ID="mevAdmDate" runat="server" EmptyValueMessage="Please Enter Admission Date"
                                                ControlExtender="meeAdmDate" ControlToValidate="txtAdmDate" IsValidEmpty="true"
                                                InvalidValueMessage="Date is invalid" Display="None" TooltipMessage="Input a date"
                                                ErrorMessage="Please Select Admission Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                ValidationGroup="Submit" SetFocusOnError="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label">
                                            <span class="validstar">&nbsp</span>Select New Branch:
                                        </td>
                                        <td class="form_left_text">
                                            <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true"
                                                Width="70%" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"
                                                AutoPostBack="True">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                                Display="None" ErrorMessage="Please Select Branch" InitialValue="0"
                                                ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr id="trNewRegNo" runat="server" visible="false">
                                        <td class="form_left_label">
                                            <span class="validstar">&nbsp</span>Registration No. :</td>
                                        <td class="form_left_text">
                                            <asp:TextBox ID="txtRegno" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None" ControlToValidate="txtRegno"
                                                ErrorMessage="Please Enter Registration No." ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr id="trRemark" runat="server" visible="false">
                                        <td class="form_left_label">
                                            <span class="validstar">&nbsp</span>Remark :</td>
                                        <td class="form_left_text">
                                            <asp:TextBox ID="txtRemark" runat="server" Height="100px" TextMode="MultiLine"
                                                Width="250px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rvfRemark" runat="server"
                                                ControlToValidate="txtRemark" ErrorMessage="Please Enter Remark"
                                                ValidationGroup="Submit" Display="None"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr style="display: none;">
                                        <td class="form_left_label">
                                            <span class="validstar">&nbsp</span>Payment Type :</td>
                                        <td class="form_left_text">
                                            <asp:RadioButton ID="rbDDPayment" runat="server" GroupName="RB"
                                                Text="DD Payment" Checked="True" />
                                            &nbsp;<asp:RadioButton ID="rbCashPayment" runat="server" GroupName="RB"
                                                Text="Cash Payment" />
                                        </td>

                                    </tr>
                                    <tr>
                                        <td class="form_left_label" align="center" colspan="2">
                                            <asp:Label ID="lblMsg" runat="server" SkinID="lblmsg"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" class="form_left_label" colspan="2">
                                            <asp:Button ID="btnShow" runat="server" Text="Show Registration Details" Width="180px"
                                                OnClick="btnShow_Click" ValidationGroup="Show" />&nbsp;&nbsp;
                                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" Width="80px"
                                                    ValidationGroup="Submit" OnClick="btnSubmit_Click" />&nbsp;&nbsp;
                                            <asp:Button ID="btnPrint" runat="server" Text="Report" Width="80px" Visible="false"
                                                OnClick="btnPrint_Click" />
                                            &nbsp;
                                            <asp:Button ID="btnReport" runat="server" Text="Admission Slip" Visible="false"
                                                OnClick="btnReport_Click" />
                                            &nbsp;
                                            <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click"
                                                Text="Cancel" Width="10%" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                                ShowMessageBox="True" ShowSummary="False" ValidationGroup="Show" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List"
                                                ShowMessageBox="True" ShowSummary="False" ValidationGroup="Submit" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td valign="top" runat="server" id="ph">
                                <asp:Image ID="imgEmpPhoto" runat="server" ImageUrl="~/IMAGES/nophoto.jpg" Height="128px" Width="128px" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
    <%--  Enable the button so it can be played again --%>
    <div id="divMsg" runat="server" />

</asp:Content>

