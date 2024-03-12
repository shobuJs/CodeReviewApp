<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="InvigilationDutyEntry.aspx.cs" Inherits="ACADEMIC_InvigilationDutyEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="z-index: 1; position: absolute; top: 75px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updInvigDuty"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 189px; padding-left: 5px">
                    <img src="../../IMAGES/ajax-loader.gif" alt="Loading" />
                    Please Wait..
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <table cellpadding="0" cellspacing="0" width="90%">
        <tr>
            <td class="vista_page_title_bar" style="height: 30px" colspan="2">INVIGILATION DUTY ENTRY
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

                    function ConfirmSubmit() {
                        var ret = confirm('Are You Sure to Generate Invigilator Duty..!!');
                        if (ret == true)
                            return true;
                        else
                            return false;
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
    <asp:UpdatePanel ID="updInvigDuty" runat="server">
        <ContentTemplate>
            <fieldset class="fieldset" style="width: 99%;">
                <legend class="legend">Invigilator Duty Entry</legend>
                <table cellpadding="2" cellspacing="2" style="width: 90%;">
                    <tr>
                        <td class="form_left_label" style="width: 10%">Session :
                        </td>
                        <td class="form_left_text" style="width: 90%">
                            <asp:DropDownList ID="ddlSession" Width="25%" AppendDataBoundItems="true" runat="server"
                                AutoPostBack="true" ValidationGroup="Show" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                Display="None" ValidationGroup="Show" InitialValue="0" ErrorMessage="Please Select Session">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="form_left_label" style="width: 10%">Exam Name :</td>
                        <td class="form_left_text" style="width: 90%">
                            <asp:DropDownList ID="ddlExTTType" runat="server" AppendDataBoundItems="true"
                                Width="25%" AutoPostBack="true"
                                TabIndex="2" OnSelectedIndexChanged="ddlExTTType_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvExTTType" runat="server" ControlToValidate="ddlExTTType"
                                ValidationGroup="Show" Display="None" ErrorMessage="Please Select Exam Name"
                                SetFocusOnError="true" InitialValue="0" />
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr id="trdayno" runat="server" visible="false">
                        <td class="form_left_label" style="width: 10%">Day No :
                        </td>
                        <td class="form_left_text" style="width: 90%">
                            <asp:DropDownList ID="ddlDay" Width="25%" AppendDataBoundItems="true" runat="server"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlDay_SelectedIndexChanged">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvDay" runat="server" ControlToValidate="ddlDay"
                                ValidationGroup="Show" Display="None" ErrorMessage="Please Select Day"
                                SetFocusOnError="true" InitialValue="0" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="form_left_label" style="width: 10%">Exam Date :
                        </td>
                        <td class="form_left_text" style="width: 90%">
                            <asp:Label ID="lblExamDate" Font-Bold="true" runat="server">
                            </asp:Label>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="form_left_label" style="width: 10%">Slot :
                        </td>
                        <td class="form_left_text" style="width: 90%">
                            <asp:DropDownList ID="ddlSlot" AppendDataBoundItems="true" Width="25%" AutoPostBack="true"
                                runat="server" OnSelectedIndexChanged="ddlSlot_SelectedIndexChanged">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvSlot" runat="server" ControlToValidate="ddlSlot"
                                ValidationGroup="Show" Display="None" ErrorMessage="Please Select Slot"
                                SetFocusOnError="true" InitialValue="0" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="form_left_label" style="width: 10%">Extra Invigilator : </td>
                        <td class="form_left_text" style="width: 90%">
                            <asp:TextBox ID="txtExtraInv" runat="server" Text="0" Width="4%" onblur="IsNumeric(this)" Style="text-align: center"></asp:TextBox></td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="form_left_label" style="width: 10%">View Report In :</td>
                        <td class="form_left_text" style="width: 90%">
                            <asp:RadioButtonList ID="rdoReportType" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True" Value="pdf">Adobe Reader</asp:ListItem>
                                <asp:ListItem Value="xls">MS-Excel</asp:ListItem>
                                <asp:ListItem Value="doc">MS-Word</asp:ListItem>
                            </asp:RadioButtonList></td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="form_left_label" colspan="2">
                            <table width="75%">

                                <tr>
                                    <td style="width: 10%">
                                        <asp:ValidationSummary ID="vsShow" runat="server" DisplayMode="List" ShowMessageBox="true"
                                            ShowSummary="false" ValidationGroup="Show" />
                                    </td>
                                    <td class="form_left_text">
                                        <asp:Button ID="btnGenerate" runat="server" OnClick="btnGenerate_Click" Text="Generate Duty"
                                            ValidationGroup="Show" Width="120px" OnClientClick="return ConfirmSubmit();" />
                                        &nbsp;
                                        <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" Text="Report*"
                                            Width="80px" />
                                        &nbsp;
                                        <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel"
                                            ValidationGroup="none" Width="80px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 10%">&nbsp;</td>
                                    <td class="form_left_text">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 10%">&nbsp;</td>
                                    <td class="form_left_text">

                                        <b>Note :</b>
                                        <ol>
                                            <li><strong>*</strong> - Selection - Session-&gt; Exam Name</li>

                                        </ol>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </fieldset>

        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">
        function IsNumeric(txt) {
            if (txt != null && txt.value != "") {
                if (isNaN(txt.value)) {
                    alert("Please Enter only Numeric Characters");
                    txt.value = "";
                    txt.focus();
                }
            }
        }
        function Total() {
            var txtInvig = document.getElementById("ctl00_ContentPlaceHolder1_txtInvig");
            var txtReliver = document.getElementById("ctl00_ContentPlaceHolder1_txtReliver");
            var txtTotal = document.getElementById("ctl00_ContentPlaceHolder1_txtTotal");
            txtTotal.value = Number(txtInvig.value) + Number(txtReliver.value);
        }
    </script>

</asp:Content>
