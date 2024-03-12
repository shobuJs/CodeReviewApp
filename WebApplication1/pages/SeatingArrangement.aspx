<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="SeatingArrangement.aspx.cs" Inherits="ACADEMIC_SeatingArrangement" Title=" " %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="z-index: 1; position: absolute; top: 75px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updExamdate"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <img src="../../IMAGES/ajax-loader.gif" alt="Loading" />
                Loading..
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td class="vista_page_title_bar" style="height: 30px">SEATING ARRANGEMENT - ALLOCATION
                <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>
        <%--  Shrink the info panel out of view --%>
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
                        var ret = confirm('ARE YOU SURE TO DELETE THIS ENTRY');
                        if (ret == true)
                            return true;
                        else
                            return false;
                    }

                    function totSubjects(chk) {
                        var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');

                        if (chk.checked == true)
                            txtTot.value = Number(txtTot.value) + 1;
                        else
                            txtTot.value = Number(txtTot.value) - 1;
                    }

                    function totAllSubjects(headchk) {
                        var chkid = headchk.id;
                        var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');
                            var hdfTot = document.getElementById('<%= hdfTot.ClientID %>');
                        var frm = document.forms[0];
                        for (i = 0; i < document.forms[0].elements.length; i++) {
                            var e = frm.elements[i];
                            if (e.type == 'checkbox') {
                                if (headchk.checked == true) {
                                    e.checked = true;
                                    txtTot.value = hdfTot.value;
                                }
                                else {
                                    e.checked = false;
                                    txtTot.value = "0";
                                }
                            }
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
            <td>&nbsp;</td>
        </tr>
    </table>
    <br />
    <asp:UpdatePanel ID="updExamdate" runat="server">
        <ContentTemplate>
            <fieldset class="fieldset" style="width: 98%">
                <legend class="legend">Select Criteria</legend>
                <table cellpadding="2" cellspacing="2" width="100%">
                    <tr>
                        <td style="width: 16%">Session :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" Width="25%"
                                AutoPostBack="True"
                                OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" />
                            <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                ValidationGroup="Submit" Display="None" ErrorMessage="Please Select session"
                                InitialValue="0" SetFocusOnError="true" />
                        </td>

                        <td rowspan="10" style="vertical-align: top" width="20%"></td>

                    </tr>
                    <tr>
                        <td style="width: 16%">Exam Name :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlExTTType" runat="server" AppendDataBoundItems="true"
                                Width="25%" AutoPostBack="true"
                                TabIndex="2" OnSelectedIndexChanged="ddlExTTType_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvExTTType" runat="server" ControlToValidate="ddlExTTType"
                                ValidationGroup="Submit" Display="None" ErrorMessage="Please Select Exam Name"
                                SetFocusOnError="true" InitialValue="0" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 16%">Day :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDay" runat="server" AppendDataBoundItems="true"
                                Width="25%" AutoPostBack="true"
                                TabIndex="3" OnSelectedIndexChanged="ddlDay_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvDay" runat="server" ControlToValidate="ddlDay"
                                ValidationGroup="Submit" Display="None" ErrorMessage="Please Select Day"
                                SetFocusOnError="true" InitialValue="0" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 16%; height: 39px;">Exam Date :
                        </td>
                        <td style="height: 39px">
                            <asp:TextBox ID="txtExamDate" runat="server" TabIndex="4" ValidationGroup="submit"
                                Width="21%" />
                            <asp:Image ID="imgExamDate" runat="server" ImageUrl="~/images/calendar.png" />
                            <ajaxToolKit:CalendarExtender ID="ceExamDate" runat="server" Format="dd/MM/yyyy"
                                TargetControlID="txtExamDate" PopupButtonID="imgExamDate" />
                            <ajaxToolKit:MaskedEditExtender ID="meExamDate" runat="server" TargetControlID="txtExamDate"
                                Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                MaskType="Date" />
                            <ajaxToolKit:MaskedEditValidator ID="mvExamDate" runat="server" EmptyValueMessage="Please Enter Exam Date"
                                ControlExtender="meExamDate" ControlToValidate="txtExamDate" IsValidEmpty="false"
                                InvalidValueMessage="Exam Date is invalid" Display="None" ErrorMessage="Please Enter Exam Date"
                                InvalidValueBlurredMessage="*" ValidationGroup="Submit" SetFocusOnError="true" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 16%">Slot :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSlot" runat="server" AppendDataBoundItems="true" Width="25%"
                                TabIndex="5" OnSelectedIndexChanged="ddlSlot_SelectedIndexChanged"
                                AutoPostBack="True">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvSlot" runat="server" ControlToValidate="ddlSlot"
                                ValidationGroup="Submit" Display="None" ErrorMessage="Please Select Slot"
                                SetFocusOnError="true" InitialValue="0" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 16%">Course Name</td>
                        <td>
                            <asp:DropDownList ID="ddlCourse" runat="server" AppendDataBoundItems="true" Width="25%"
                                TabIndex="6" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvCourse" runat="server" ControlToValidate="ddlCourse"
                                ValidationGroup="Submit" Display="None" ErrorMessage="Please Select Slot"
                                SetFocusOnError="true" InitialValue="0" /></td>
                    </tr>
                    <tr>
                        <td style="width: 16%">Room Name :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlRoom" runat="server" AppendDataBoundItems="true" Width="25%"
                                TabIndex="7" OnSelectedIndexChanged="ddlRoom_SelectedIndexChanged"
                                AutoPostBack="True">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvRoom" runat="server" ControlToValidate="ddlRoom"
                                ValidationGroup="Submit" Display="None" ErrorMessage="Please Select Room"
                                SetFocusOnError="true" InitialValue="0" />
                        </td>

                    </tr>
                    <tr>
                        <td style="width: 16%">Total Selected Students :
                                            
                        </td>
                        <td>
                            <asp:TextBox ID="txtTotStud" runat="server" CssClass="watermarked" Enabled="false"
                                Style="text-align: center" Width="30px" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Room Capacity:
                            <asp:TextBox ID="txtRoomCapacity" runat="server" CssClass="watermarked" Width="30px" Enabled="false"> </asp:TextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp; Remaining Capacity:
                            <asp:TextBox ID="txtRemainCapacity" runat="server" CssClass="watermarked" Width="30px" Enabled="false"> </asp:TextBox>
                            <asp:HiddenField ID="hdfTot" runat="server" Value="0" />

                        </td>
                    </tr>
                    <tr>
                        <td style="width: 16%">View Report in:</td>
                        <td>

                            <asp:RadioButtonList ID="rdoReportType" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True" Value="pdf">Adobe Reader</asp:ListItem>
                                <asp:ListItem Value="xls">MS-Excel</asp:ListItem>
                                <asp:ListItem Value="doc">MS-Word</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 16%">&nbsp;
                        </td>
                        <td style="padding-top: 10px">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit"
                                Width="80px" OnClick="btnSubmit_Click" />
                            &nbsp;&nbsp;&nbsp;<asp:Button ID="btnCancel0" runat="server" Text="Cancel" Width="80px"
                                OnClick="btnCancel_Click" />
                            &nbsp;&nbsp;<asp:Button ID="btnReport" runat="server" ValidationGroup="Submit"
                                Text="Report" Width="80px" OnClick="btnReport_Click" />
                            &nbsp;&nbsp;<asp:Button ID="btnExamSheet" runat="server" ValidationGroup="Submit"
                                Text="Exam Sheet" Width="80px" OnClick="btnExamSheet_Click" />
                            &nbsp;<asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                ShowSummary="false" ValidationGroup="Submit" />
                        </td>
                    </tr>
                </table>
            </fieldset>
            <br />
            <tr>
                <td style="padding-top: 20px; text-align: center;" colspan="2">
                    <div style="width: 50%; text-align: center">
                        <asp:ListView ID="lvStudent" runat="server">
                            <LayoutTemplate>
                                <div id="demo-grid" class="vista-grid">
                                    <div class="titlebar">
                                        Student List
                                    </div>
                                    <table cellpadding="0" cellspacing="0" class="datatable" width="100%">
                                        <tr class="header">
                                            <th style="width: 5%">
                                                <asp:CheckBox ID="cbHead" runat="server" onclick="totAllSubjects(this)" Checked="false" />
                                            </th>
                                            <th>Roll No.
                                            </th>
                                            <th>Name
                                            </th>
                                        </tr>
                                        <tr id="itemPlaceholder" runat="server" />
                                    </table>
                                </div>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr class="item" onmouseout="this.style.backgroundColor='#FFFFFF'" onmouseover="this.style.backgroundColor='#FFFFAA'">
                                    <td style="width: 5%">
                                        <asp:CheckBox ID="cbRow" runat="server" onclick="totSubjects(this)" ToolTip='<%# Eval("IDNo")%>' />
                                    </td>
                                    <td>
                                        <asp:HiddenField ID="hdfIDNO" runat="server" Value='<%# Eval("IDNO")%>' />
                                        <%# Eval("REGNO")%>
                                        <asp:HiddenField ID="hdfRegno" Value='<%# Eval("REGNO")%>' runat="server" />
                                    </td>
                                    <td>
                                        <%# Eval("STUDNAME")%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                </td>
            </tr>
            <div id="divMsg" runat="Server">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
