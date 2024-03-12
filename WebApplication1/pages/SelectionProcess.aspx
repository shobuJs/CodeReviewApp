<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="SelectionProcess.aspx.cs"
    Inherits="ACADEMIC_SelectionProcess" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="z-index: 1; position: absolute; top: 70px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updSelection"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px; text-align: end">
                    <img src="../IMAGES/anim_loading_75x75.gif" alt="Loading" />
                    <span style="color: Blue; text-align: center"><b>In-Progrees</b></span>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel ID="updSelection" runat="server">
        <ContentTemplate>
            <table cellpadding="0" cellspacing="0" style="width: 100%;">
                <tr>
                    <td class="vista_page_title_bar" style="height: 30px">Selection Process
                        <!-- Button used to launch the help (animation) -->
                        <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                            AlternateText="Page Help" ToolTip="Page Help" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">

                        <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF; border: solid 1px #D0D0D0;">
                        </div>

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
                    <td style="padding: 10px">
                        <div style="color: Red; font-weight: bold">
                            &nbsp;Note : * marked fields are Mandatory
                        </div>
                        <fieldset class="fieldset" style="width: 70%">
                            <legend class="legend">Selection Process</legend>
                            <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td class="form_left_label" style="width: 97px">
                                        <span class="validstar">* </span>Session :
                                    </td>
                                    <td class="form_left_text">
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" Width="300px" TabIndex="1" Style="margin-left: 7px">
                                            <asp:ListItem Value="0">Please Select </asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ValidationGroup="Show" InitialValue="0"
                                            ErrorMessage="Please Select Session"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvddlSession1" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ValidationGroup="Report" InitialValue="0"
                                            ErrorMessage="Please Select Session"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form_left_label" style="width: 97px">
                                        <span class="validstar">* </span>Degree :</td>
                                    <td class="form_left_text">
                                        <asp:DropDownList ID="ddlDegree" runat="server" AutoPostBack="true" AppendDataBoundItems="true" Width="300px"
                                            TabIndex="2" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" Style="margin-left: 7px">
                                            <asp:ListItem Value="0">Please Select </asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ValidationGroup="Show" InitialValue="0"
                                            ErrorMessage="Please Select Degree"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvddlDegree1" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ValidationGroup="Report" InitialValue="0"
                                            ErrorMessage="Please Select Degree"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>

                                <tr>
                                    <td class="form_left_label" style="width: 97px">
                                        <span class="validstar">* </span>Branch :
                                    </td>
                                    <td class="form_left_text">
                                        <asp:DropDownList ID="ddlBranchName" runat="server" Width="300px" AppendDataBoundItems="true"
                                            TabIndex="3" Style="margin-left: 6px">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranchName"
                                            Display="None" ErrorMessage="Please Select Branch" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvBranch1" runat="server" ControlToValidate="ddlBranchName"
                                            Display="None" ErrorMessage="Please Select Branch" InitialValue="0" ValidationGroup="Report"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form_left_label" style="width: 97px">&nbsp;&nbsp; Report For :</td>
                                    <td class="form_left_text">
                                        <asp:DropDownList ID="ddlStudType" runat="server" OnSelectedIndexChanged="ddlStudType_SelectedIndexChanged"
                                            AppendDataBoundItems="true" Width="300px"
                                            TabIndex="2" Style="margin-left: 7px">
                                            <asp:ListItem Value="0">Please Select </asp:ListItem>
                                            <asp:ListItem Value="1">Selected students </asp:ListItem>
                                            <asp:ListItem Value="2">Waiting List Students </asp:ListItem>
                                            <asp:ListItem Value="3">Rejected Students</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlStudType" runat="server" ControlToValidate="ddlStudType"
                                            Display="None" ErrorMessage="Please Select Student Status" InitialValue="0" ValidationGroup="Report"></asp:RequiredFieldValidator>

                                    </td>

                                </tr>

                                <tr>
                                    <td style="width: 97px">&nbsp;
                                    </td>
                                    <td class="form_left_text">
                                        <asp:Button ID="btnShow" runat="server" Text="Show" TabIndex="5" ValidationGroup="Show" OnClick="btnShow_Click" />&nbsp;&nbsp;
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" TabIndex="6" OnClick="btnSubmit_Click" Width="115px" />&nbsp;&nbsp;
                                    <asp:Button ID="btnReport" runat="server" Text="Report" TabIndex="8" ValidationGroup="Report" OnClick="btnReport_Click" />&nbsp;&nbsp;
                                    <asp:Button ID="btnselectedletter" runat="server" Text="Offer letter" TabIndex="9" ValidationGroup="Show" Width="103px" OnClick="btnselectedletter_Click" />&nbsp;&nbsp;
                                    <asp:Button ID="btnSendMail" runat="server" Text="Send Offer letter by Mail" TabIndex="6" Width="150px" OnClick="btnSendMail_Click" />&nbsp;&nbsp;
                                    <asp:Button ID="btnWaitngletter" runat="server" Text="Offer letter for Waitng students" TabIndex="9" Visible="false" ValidationGroup="Show" Width="199px" OnClick="btnWaitngletter_Click" />&nbsp;&nbsp;
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="8" OnClick="btnCancel_Click" />

                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Show"
                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Report"
                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
            </table>
            <asp:Panel ID="pnlStudent" runat="server">
                <div style="width: 95%; padding: 10px">
                    <asp:ListView ID="lvStudents" runat="server" OnItemDataBound="lvStudents_ItemDataBound">
                        <LayoutTemplate>
                            <div id="demo-grid" class="vista-grid">
                                <div class="titlebar">
                                    Student List
                                </div>
                                <table class="datatable" cellpadding="0" cellspacing="0" width="100%">
                                    <tr class="header">
                                        <th>SrNo
                                        </th>
                                        <th>Application ID
                                        </th>
                                        <th>Student Name
                                        </th>
                                        <th>Degree
                                        </th>
                                        <th>Branch
                                        </th>
                                        <th>Mobile No
                                        </th>
                                        <th>Email ID
                                        </th>
                                        <th>Status
                                        </th>
                                        <th>Selected Merit No.
                                        </th>
                                        <th>Waiting No.
                                        </th>
                                    </tr>
                                    <tr id="itemPlaceholder" runat="server" />


                                </table>
                            </div>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                <td>
                                    <asp:Label ID="lblSrNo" runat="server" Text='<%# Container.DataItemIndex+1%>' Width="35px" />
                                    <asp:HiddenField ID="hdfUserno" runat="server" Value='<%# Eval("USERNO") %>' />

                                </td>
                                <td>
                                    <asp:Label ID="lblUserName" runat="server" Text='<%# Eval("USERNAME")%>' Width="35px" />
                                </td>
                                <td>
                                    <asp:Label ID="lblStudName" runat="server" Text='<%# Eval("STUDNAME")%>' Width="35px" />
                                </td>
                                <td>
                                    <%# Eval("DEGREENAME")%>
                                </td>
                                <td>
                                    <%# Eval("BRANCHNAME")%>
                                </td>
                                <td>
                                    <%# Eval("MOBILE") %>
                                </td>
                                <td>
                                    <%# Eval("EMAILID") %>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlStudType" runat="server" OnSelectedIndexChanged="ddlStudType_SelectedIndexChanged" AutoPostBack="true"
                                        ToolTip='Please select Student Status'>
                                        <asp:ListItem Text="Please Select" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Selected" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Waiting" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Rejected" Value="3"></asp:ListItem>
                                    </asp:DropDownList>

                                </td>
                                <td>
                                    <asp:TextBox ID="txtSelected" runat="server" Width="100" Height="17" ToolTip='Please Enter Merit No.' Text='<%# Bind("SELECTEDMERITNO") %>'></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtWaiting" runat="server" Width="100" Height="17" ToolTip='Please Enter Waiting No.' Text='<%# Bind("WAITNINGNO") %>'></asp:TextBox>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:ListView>
                </div>
            </asp:Panel>
            <div id="divMsg" runat="server">
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnselectedletter" />
            <asp:PostBackTrigger ControlID="btnWaitngletter" />
            <asp:PostBackTrigger ControlID="btnReport" />
            <asp:PostBackTrigger ControlID="btnShow" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>

