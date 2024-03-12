<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="StudentForRegistration.aspx.cs" Inherits="ACADEMIC_StudentForRegistration" Title="" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table class="vista_page_title_bar" width="100%">
        <tr>
            <td style="height: 30px">
                ADMISSION - STUDENT FOR REGISTRATION &nbsp;
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>
        <tr>
            <td>
                <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF;
                    border: solid 1px #D0D0D0;">
                </div>
                <div id="info" style="display: none; width: 250px; z-index: 2; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);
                    font-size: 12px; border: solid 1px #CCCCCC; background-color: #FFFFFF; padding: 5px;">
                    <div id="btnCloseParent" style="float: right; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);">
                        <asp:LinkButton ID="btnClose" runat="server" OnClientClick="return false;" Text="X"
                            ToolTip="Close" Style="background-color: #666666; color: #FFFFFF; text-align: center;
                            font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
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
    </table>
    <br />
    
    <fieldset class="fieldset">
        <legend class="legend">Student For Registration</legend>
        <asp:UpdatePanel ID="updMeritList" runat="server">
            <ContentTemplate>
                <table cellpadding="2" cellspacing="2" style="width: 100%" align="left">
                    <tr>
                        <td width="50%">
                            <table cellpadding="2" cellspacing="2" align="left" width="100%">
                                <tr>
                                    <td colspan="3">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Session
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" 
                                            ValidationGroup="submit" Font-Bold="True">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session." InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Registration No.
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRegistrationNo" runat="server" ToolTip="Please Enter Registration no"
                                            ValidationGroup="submit" OnTextChanged="txtRegistrationNo_TextChanged" AutoPostBack="True"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvRegistrationNo" runat="server" Display="None"
                                            ErrorMessage="Please Enter Registration No." ValidationGroup="submit" ControlToValidate="txtRegistrationNo"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="cvRegistrationNo" runat="server" ControlToValidate="txtRegistrationNo"
                                            Display="None" ErrorMessage="Please Enter Numbers Only" Operator="DataTypeCheck"
                                            SetFocusOnError="True" Type="Integer" ValueToCompare="submit"></asp:CompareValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Register
                                    </td>
                                    <td>
                                        :
                                    </td>
                                    <td>
                                        <asp:RadioButton ID="rbtYes" runat="server" Text="Yes" GroupName="Register" />
                                        &nbsp;
                                        <asp:RadioButton ID="rbtNo" runat="server" Text="No" GroupName="Register" Checked="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                            ShowMessageBox="True" ValidationGroup="submit" ShowSummary="False" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="text-align: center">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="submit"
                                            OnClick="btnSubmit_Click" Enabled="False" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td valign="top" width="50%">
                            <fieldset class="fieldset" id="tdStudInfo" runat="server" visible="false">
                                <legend class="legend">Student Detail</legend>
                                <table align="left" style="margin-left: 0px;" width="100%">
                                    <tr>
                                        <td style="margin-left: 0px;">
                                            Student Name
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:Label ID="lblStudentname" runat="server" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Minority
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:Label ID="lblMinority" runat="server" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Category
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCategory" runat="server" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblMhcet" runat="server" Font-Bold="False"></asp:Label>
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:Label ID="lblMhcetScore" runat="server" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblPcm" runat="server" Font-Bold="False"></asp:Label>
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPcmSCore" runat="server" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </fieldset>

</asp:Content>

