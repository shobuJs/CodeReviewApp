<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="changeStudSem.aspx.cs" Inherits="Academic_changeStudSem" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="70%" cellpadding="0" cellspacing="0">
        <tr>
            <td class="vista_page_title_bar" style="height: 30px" colspan="2">CHANGE STUDENT SEMESTER
           <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;" AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>

        <tr>
            <td colspan="2">
                <!-- "Wire frame" div used to transition from the button to the info panel -->
                <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF; border: solid 1px #D0D0D0;"></div>

                <!-- Info panel to be displayed as a flyout when the button is clicked -->
                <div id="info" style="display: none; width: 250px; z-index: 2; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0); font-size: 12px; border: solid 1px #CCCCCC; background-color: #FFFFFF; padding: 5px;">
                    <div id="btnCloseParent" style="float: right; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);">
                        <asp:LinkButton ID="btnClose" runat="server" OnClientClick="return false;" Text="X" ToolTip="Close"
                            Style="background-color: #666666; color: #FFFFFF; text-align: center; font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
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
                            <asp:Label ID="lblHelp" runat="server" Font-Names="Trebuchet MS" /></p>
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

        <tr>
            <td colspan="2">
                <fieldset class="fieldset">
                    <legend class="legend">Change Student Semester</legend>
                    <asp:UpdatePanel ID="upcCSS" runat="server">
                        <ContentTemplate>
                            <table width="70%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="form_left_label" style="width: 15%;">Enrollment No:</td>
                                    <td class="form_left_text" style="width: 35%">
                                        <asp:TextBox ID="txtEnrollmentNo" runat="server" Width="50%"
                                            ValidationGroup="show" />&nbsp;&nbsp;
                                    <asp:Button ID="btnShow" runat="server" Text="Show" Width="80px"
                                        OnClick="btnShow_Click" ValidationGroup="show" />
                                        <asp:RequiredFieldValidator ID="rfvEnrollmentNo" runat="server"
                                            ControlToValidate="txtEnrollmentNo" Display="None"
                                            ErrorMessage="Please Enter Enrollment No" SetFocusOnError="True"
                                            ValidationGroup="show"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>

                                <tr>
                                    <td class="form_left_label">Name :</td>
                                    <td class="form_left_text">
                                        <asp:Label ID="lblName" runat="server" Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>

                                <tr style="display: none">
                                    <td class="form_left_label">Branch :</td>
                                    <td class="form_left_text">
                                        <asp:Label ID="lblBranch" runat="server" Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>

                                <tr>
                                    <td class="form_left_label">Path/Branch :</td>
                                    <td class="form_left_text">
                                        <asp:Label ID="lblScheme" runat="server" Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form_left_label">Semester :</td>
                                    <td class="form_left_text">
                                        <asp:Label ID="lblSemester" runat="server" Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form_left_label">Change Semster To :</td>
                                    <td class="form_left_text">
                                        <asp:DropDownList ID="ddlSemester" runat="server" Enabled="false">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">I</asp:ListItem>
                                            <asp:ListItem Value="2">II</asp:ListItem>
                                            <asp:ListItem Value="3">III</asp:ListItem>
                                            <asp:ListItem Value="4">IV</asp:ListItem>
                                            <asp:ListItem Value="5">V</asp:ListItem>
                                            <asp:ListItem Value="6">VI</asp:ListItem>
                                            <asp:ListItem Value="7">VII</asp:ListItem>
                                            <asp:ListItem Value="8">VIII</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>

                                <tr>
                                    <td class="form_left_label">Change Year To :</td>
                                    <td class="form_left_text">
                                        <asp:DropDownList ID="ddlYear" runat="server" Enabled="false">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">I</asp:ListItem>
                                            <asp:ListItem Value="2">II</asp:ListItem>
                                            <asp:ListItem Value="3">III</asp:ListItem>
                                            <asp:ListItem Value="4">IV</asp:ListItem>
                                            <asp:ListItem Value="5">V</asp:ListItem>
                                            <asp:ListItem Value="6">VI</asp:ListItem>
                                            <asp:ListItem Value="7">VII</asp:ListItem>
                                            <asp:ListItem Value="8">VIII</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>

                                <tr>
                                    <td class="form_left_label">&nbsp;</td>
                                    <td class="form_left_text">
                                        <asp:Button ID="btnChangeSem" runat="server" Text="Submit" Width="80px"
                                            OnClick="btnChangeSem_Click" />&nbsp;
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="80px"
                                            OnClick="btnCancel_Click" />
                                    </td>
                                </tr>

                                <tr>
                                    <td class="form_left_label">&nbsp;</td>
                                    <td class="form_left_text">
                                        <asp:Label ID="lblMsg" runat="server" SkinID="Errorlbl"></asp:Label>
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                                            DisplayMode="List" ShowMessageBox="True" ShowSummary="False"
                                            ValidationGroup="show" />
                                    </td>
                                </tr>

                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </fieldset>
            </td>
        </tr>
    </table>
</asp:Content>
