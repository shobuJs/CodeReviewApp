<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master"
    AutoEventWireup="true" CodeFile="RegistraionNoGeneration.aspx.cs" Inherits="ACADEMIC_RegistraionNoGeneration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; background-color: Aqua; padding-left: 5px">
                    <img src="../IMAGES/ajax-loader.gif" alt="Loading" />
                    Please Wait..
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td class="vista_page_title_bar" style="height: 30px">REGISTRATION NO GENERATION
                        <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                            AlternateText="Page Help" ToolTip="Page Help" />
                    </td>
                </tr>
                <%--PAGE HELP--%>
                <%--JUST CHANGE THE IMAGE AS PER THE PAGE. NOTHING ELSE--%>
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

                            function showSubConfirm() {
                                Page_ClientValidate();
                                if (Page_IsValid) {
                                    var ret = confirm('Do you really Generate RegNo?');
                                    if (ret == true)
                                        return true;
                                    else
                                        return false;
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
            <div style="width: 98%; padding-left: 10px; padding: 10px">
                <div style="color: Red; font-weight: bold">
                    &nbsp;Note : * marked fields are Mandatory
                </div>
                <fieldset class="fieldset">
                    <legend class="legend">Registration No. Generation</legend>
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                        <tr>
                            <td class="form_left_label">
                                <span class="validstar">*</span>Admission Batch :
                            </td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlAdmBatch" runat="server" AppendDataBoundItems="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvAdmBatch" runat="server" ControlToValidate="ddlAdmBatch" ValidationGroup="RegNoAllot"
                                    Display="None" InitialValue="0" ErrorMessage="Please Select Admission Batch">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">
                                <span class="validstar">*</span>College Name :
                            </td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlClgname" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                    ValidationGroup="SeatAllot" Width="400px" OnSelectedIndexChanged="ddlClgname_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvCname" runat="server" ControlToValidate="ddlDegree"
                                    Display="None" ErrorMessage="Please Select College Name" InitialValue="0" ValidationGroup="RegNoAllot">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">
                                <span class="validstar">*</span>Degree :
                            </td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                    ValidationGroup="SeatAllot" Width="400px" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                    Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="RegNoAllot">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">
                                <span class="validstar">*</span>Branch :
                            </td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlBranch" AppendDataBoundItems="true" runat="server" ValidationGroup="RegNoAllot"
                                    Width="400px" AutoPostBack="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                    Display="None" ErrorMessage="Please Select Branch" InitialValue="0" ValidationGroup="RegNoAllot">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>

                        <tr>
                            <td class="form_left_label">&nbsp;<asp:CheckBox ID="chkContinueGeneration" runat="server" Text="Continue RegNo Generation" ToolTip="RollNO Generate In Continue" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                    ShowMessageBox="true" ShowSummary="false" ValidationGroup="RegNoAllot" />
                            </td>
                            <td class="form_left_text" style="padding: 10px">
                                <asp:Button ID="btnShow" runat="server" ValidationGroup="RegNoAllot" Text="Show" Width="80px"
                                    OnClick="btnShow_Click" />
                                &nbsp;
                            <asp:Button ID="btnGenerate" runat="server" ValidationGroup="RegNoAllot" Text="Generate" Width="80px"
                                OnClick="btnGenerate_Click" OnClientClick="return showSubConfirm();" />
                                &nbsp;<asp:Button ID="btnClear0" runat="server" Text="Cancel" Width="80px"
                                    OnClick="btnClear0_Click1" />
                                &nbsp;
                                <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click"
                                    Text="Report" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
            <br />
            <%--STUDENT LIST--%>
            <asp:Panel ID="pnlStudent" runat="server">
                <div style="width: 70%; padding: 10px;">
                    <asp:ListView ID="lvStudents" runat="server">
                        <LayoutTemplate>
                            <div id="demo-grid" class="vista-grid">
                                <div class="titlebar">
                                    Student List
                                </div>
                                <table class="datatable" cellpadding="0" cellspacing="0" width="100%">
                                    <tr class="header">
                                        <th style="width: 10%">Sr.No
                                        </th>
                                        <th style="width: 50%">Student Name
                                        </th>

                                        <th style="width: 30%">Registration No.
                                        </th>
                                    </tr>
                                    <tr id="itemPlaceholder" runat="server" />
                                </table>
                            </div>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                <td>
                                    <%# Container.DataItemIndex + 1 %>

                                </td>
                                <td style="width: 50%; text-transform: uppercase">
                                    <%# Eval("STUDNAME")%>
                                </td>

                                <td style="width: 30%">
                                    <%# Eval("REGNO")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:ListView>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
