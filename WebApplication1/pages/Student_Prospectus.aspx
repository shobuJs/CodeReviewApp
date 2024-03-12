<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Student_Prospectus.aspx.cs" Inherits="ACADEMIC_Student_Prospectus" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        $(function () {
            $("#<%=txtSaleDate.ClientID%>").datepicker({
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
            <td class="vista_page_title_bar">PROSPECTUS SALE
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

                <ajaxToolKit:AnimationExtender ID="AnimationExtender1" runat="server"
                    TargetControlID="btnHelp">
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
                <ajaxToolKit:AnimationExtender ID="CloseAnimation" runat="server"
                    TargetControlID="btnClose">
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
    <br />
    <div style="color: Red; font-weight: bold">
        &nbsp;Note : * marked fields are Mandatory
    </div>

    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <fieldset class="fieldset">
                    <legend class="legend">Prospectus Sale</legend>

                    <asp:UpdatePanel ID="updProspectus" runat="server">
                        <ContentTemplate>
                            <table cellpadding="2" cellspacing="2" width="100%">
                                <tr>
                                    <td style="width: 25%;">
                                        <span class="validstar">&nbsp</span>Session :
                                    </td>
                                    <td style="width: 35%;" colspan="3">
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" Font-Bold="true"
                                            AutoPostBack="True" Width="400px" TabIndex="1">
                                            <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0"
                                            ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 10%;">
                                        <span class="validstar">*</span>Degree : 
                                    </td>
                                    <td style="width: 35%;" colspan="3">
                                        <asp:DropDownList ID="ddlDegree" runat="server" Width="400px" AppendDataBoundItems="true"
                                            AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" TabIndex="2"
                                            ToolTip="Please Select Degree">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                            InitialValue="0" Display="None" ValidationGroup="submit" ErrorMessage="Please Select Degree"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 10%;">
                                        <span class="validstar">*</span>Branch : 
                                    </td>
                                    <td style="width: 35%;" colspan="3">
                                        <asp:DropDownList ID="ddlBranch" runat="server" Width="400px" AppendDataBoundItems="true"
                                            AutoPostBack="True" TabIndex="3" ToolTip="Please Select Branch">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                            InitialValue="0" Display="None" ValidationGroup="submit" ErrorMessage="Please Select Branch"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 10%;">
                                        <span class="validstar">&nbsp</span>Admission Batch :
                                    </td>
                                    <td style="width: 35%;" colspan="3">
                                        <asp:DropDownList ID="ddlAdmBatch" runat="server" Width="400px" AppendDataBoundItems="true"
                                            AutoPostBack="True" TabIndex="4">

                                            <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvAdmBatch" runat="server" ControlToValidate="ddlAdmBatch"
                                            InitialValue="0" Display="None"
                                            ErrorMessage="Please Select Batch"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 10%;">
                                        <span class="validstar">*</span>Student Name : </td>
                                    <td style="width: 35%;" colspan="3" valign="top">
                                        <asp:TextBox ID="txtStudentName" runat="server" Width="395px" TabIndex="5"
                                            ValidationGroup="submit" AutoPostBack="True" class="watermarked"
                                            OnTextChanged="txtStudentName_TextChanged"
                                            ToolTip="Please Enter Student Name"></asp:TextBox>
                                        <ajaxToolKit:TextBoxWatermarkExtender ID="tbWaterMarkStudName" TargetControlID="txtStudentName"
                                            WatermarkText="Type Full Name Here" runat="server">
                                        </ajaxToolKit:TextBoxWatermarkExtender>
                                        <asp:RequiredFieldValidator ID="rfvStudName" runat="server" ControlToValidate="txtStudentName"
                                            Display="None" ValidationGroup="submit"
                                            ErrorMessage="Please enter student name"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 10%;">
                                        <span class="validstar">*</span>Receipt No :</td>
                                    <td style="width: 10%;">
                                        <asp:TextBox ID="txtReciptNo" runat="server" TabIndex="6" Width="60px"
                                            Enabled="False"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvReceiptNo" runat="server" ControlToValidate="txtReciptNo"
                                            Display="None" ValidationGroup="submit"
                                            ErrorMessage="Please enter receipt no."></asp:RequiredFieldValidator>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td style="width: 5%;">&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 10%;">
                                        <span class="validstar">*</span>
                                    Admission Form No:<td style="width: 9%;">
                                        <asp:TextBox ID="txtSerialNo" runat="server" Width="60px" TabIndex="7"
                                            ValidationGroup="submit" ToolTip="Please Enter Admission Form Number"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvSerialNo" runat="server" ControlToValidate="txtSerialNo"
                                            Display="None" ValidationGroup="submit"
                                            ErrorMessage="Please enter admission form no."></asp:RequiredFieldValidator>
                                    </td>
                                    <td>Sale Date : &nbsp;<asp:TextBox ID="txtSaleDate" runat="server" TabIndex="8"
                                        ValidationGroup="submit" Width="82px"></asp:TextBox>

                                    </td>
                                    <td style="width: 5%;">&nbsp;</td>
                                    <td>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 10%;">
                                        <span class="validstar">*</span>Amount :</td>
                                    <td style="width: 35%;" colspan="3">
                                        <asp:TextBox ID="txtAmount" runat="server" Width="60px" TabIndex="9"
                                            ToolTip="Please Enter Amount" ValidationGroup="submit" MaxLength="4"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvAmount" runat="server" ControlToValidate="txtAmount"
                                            Display="None" ValidationGroup="submit"
                                            ErrorMessage="Please enter amount"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="fteTxtAmount" runat="server" FilterType="Numbers"
                                            TargetControlID="txtAmount">
                                        </ajaxToolKit:FilteredTextBoxExtender>
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 10%;">&nbsp;
                                    </td>
                                    <td style="width: 35%;" colspan="3">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" Width="80px"
                                            OnClick="btnSubmit_Click" ValidationGroup="submit" Font-Bold="True"
                                            TabIndex="10" />&nbsp;
                                <asp:Button ID="btnCancel" runat="server" CausesValidation="False" Text="Cancel"
                                    Width="80px" OnClick="btnCancel_Click" Font-Bold="True" TabIndex="11" />
                                    </td>
                                    <td>
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                            ValidationGroup="submit" ShowSummary="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="padding: 10px; text-align: center; vertical-align: top"></td>
                                    <td></td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </fieldset>
            </td>
        </tr>
    </table>

    <div id="divMsg" runat="server">
    </div>
</asp:Content>

