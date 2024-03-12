﻿<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="StudentRollList.aspx.cs" Inherits="Academic_StudentRoolist" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td>
                <table class="vista_page_title_bar" width="100%">
                    <tr>
                        <td style="height: 30px">STUDENT ROLL LIST REPORT&nbsp;&nbsp
                            <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                                AlternateText="Page Help" ToolTip="Page Help" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF; border: solid 1px #D0D0D0;">
                </div>
                <!-- Info panel to be displayed as a flyout when the button is clicked -->
                <div id="info" style="display: none; width: 250px; z-index: 2; font-size: 12px; border: solid 1px #CCCCCC; background-color: #FFFFFF; padding: 5px;">
                    <div id="btnCloseParent" style="float: right;">
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

                <ajaxToolKit:AnimationExtender ID="OpenAnimation" runat="server" TargetControlID="btnHelp"
                    Enabled="True">
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
                <ajaxToolKit:AnimationExtender ID="CloseAnimation" runat="server" TargetControlID="btnClose"
                    Enabled="True">
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
                <br />
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Label ID="lblMsg" runat="server" SkinID="Msglbl"></asp:Label>
            </td>
        </tr>
    </table>
    <div id="div1" runat="server" style="width: 95%">
        <fieldset class="fieldset">
            <legend class="legend">Student Roll List Report</legend>
            <br />
            <table cellpadding="2" cellspacing="2" width="100%">
                <tr>
                    <td width="10%">Term :</td>
                    <td width="90%">
                        <asp:DropDownList ID="ddlSessionNo" runat="server" AppendDataBoundItems="true"
                            Width="150px" ValidationGroup="Select sessionno"
                            ToolTip="Please Select SessionNo" TabIndex="1">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                            ControlToValidate="ddlSessionNo" Display="None"
                            ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr style="display: none">
                    <td width="10%">Department :</td>
                    <td width="90%">
                        <asp:DropDownList ID="ddlDepartment" runat="server" AppendDataBoundItems="true"
                            Width="50%"
                            ToolTip="Please Select Department" TabIndex="2" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvDepartment" runat="server"
                            ControlToValidate="ddlDepartment" Display="None"
                            ErrorMessage="Please Select Department" InitialValue="0"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td width="10%">Degree :
                    </td>
                    <td width="90%">
                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                            Width="50%" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td width="10%">Branch :</td>

                    <td width="90%">
                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                            Width="50%" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                            Display="None" ErrorMessage="Please Select Branch" InitialValue="0" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td width="10%">Scheme :</td>
                    <td width="90%">
                        <asp:DropDownList ID="ddlSchemeNo" runat="server" AppendDataBoundItems="true"
                            Width="50%" ValidationGroup="Select Schemeno"
                            ToolTip="Please Select Program" TabIndex="2">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                            ControlToValidate="ddlSchemeNo" Display="None"
                            ErrorMessage="Please Select Scheme" InitialValue="0" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td width="10%">Semester :</td>
                    <td width="90%">
                        <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                            Width="150px" ValidationGroup="Select sessionno"
                            ToolTip="Please Select Program" TabIndex="3"
                            OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 10%;">Section :
                    </td>
                    <td style="width: 90%;">
                        <asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="true" Width="150px">
                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                        </asp:DropDownList>

                    </td>

                </tr>
                <tr>
                    <td width="10%"></td>
                    <td width="90%">
                        <asp:Button ID="btnReport" runat="server" Text="Roll List Report"
                            OnClick="btnReport_Click"
                            ValidationGroup="Submit"
                            ToolTip="Show RollList under Selected Criteria." Width="120px"
                            TabIndex="4" />
                        &nbsp;
                        <asp:Button ID="btnSubjectReport" runat="server" Text="Subject Report"
                            OnClick="btnSubjectReport_Click"
                            ValidationGroup="Submit"
                            ToolTip="Show Subject Report under Selected Criteria." Width="120px"
                            TabIndex="5" />
                        &nbsp;
                 <asp:Button ID="btnExamReport" runat="server" Text="Exam Registered"
                     ValidationGroup="Submit"
                     ToolTip="Show Registered Students for Exam" Width="120px"
                     TabIndex="6" OnClick="btnExamReport_Click" />
                        &nbsp;        
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                        OnClick="btnCancel_Click" ValidationGroup="Cancel Button"
                        ToolTip="Cancel Field under Selected criteria." Width="80px"
                        TabIndex="7" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                            DisplayMode="List" ShowMessageBox="True" ShowSummary="False"
                            ValidationGroup="Submit" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
    <div id="divMsg" runat="server"></div>
</asp:Content>
