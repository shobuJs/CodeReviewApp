<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="MigrationReport.aspx.cs" Inherits="ACADEMIC_REPORTS_MigrationReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updpnlExam" runat="server">
        <ContentTemplate>
            <table cellpadding="0" cellspacing="0" width="90%">
                <tr>
                    <td class="vista_page_title_bar" style="height: 30px">Migration Detail Report
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
                <tr>
                    <td style="height: 10px;"></td>
                </tr>
                <tr>
                    <td>
                        <div style="z-index: 1; position: absolute; top: 50px; left: 600px;">
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updpnlExam">
                                <ProgressTemplate>
                                    <asp:Image ID="imgLoad" runat="server" ImageUrl="~/images/ajax-loader.gif" />
                                    <span style="font-size: 8pt">Loading...</span>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </div>
                    </td>
                </tr>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td width="50%">
                            <fieldset class="fieldset">
                                <legend class="legend">Selection</legend>
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td class="form_left_label" style="width: 148px">
                                            <span class="validstar">*</span>Year : </td>
                                        <td class="form_left_text">
                                            <asp:DropDownList ID="ddlAdmBatch" runat="server" AppendDataBoundItems="True" Width="200px">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvAdmBatch" runat="server" ControlToValidate="ddlAdmBatch" Display="None" ErrorMessage="Please Select Year" InitialValue="0" SetFocusOnError="True" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label" style="width: 148px">
                                            <span class="validstar">*</span>College/School Name : </td>
                                        <td class="form_left_text">
                                            <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged"
                                                Width="273px">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlCollege" runat="server" ControlToValidate="ddlCollege" Display="None" ErrorMessage="Please Select College" InitialValue="0" SetFocusOnError="True" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label" style="width: 148px">
                                            <span class="validstar">*</span>Degree : </td>
                                        <td class="form_left_text">
                                            <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" Width="200px">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree" Display="None" ErrorMessage="Please Select Degree" InitialValue="0" SetFocusOnError="True" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label" style="width: 148px">
                                            <span class="validstar">*</span>Branch : </td>
                                        <td class="form_left_text">
                                            <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="True" Width="200px">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch" Display="None" ErrorMessage="Please Select Branch" InitialValue="0" SetFocusOnError="True" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td class="form_left_text" style="width: 148px"></td>
                                        <td class="form_left_text">
                                            <asp:Button ID="btnReport" runat="server" Text="Print Report" OnClick="btnReport_Click" ValidationGroup="Show"
                                                Width="106px" />&nbsp;
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                        Width="106px" OnClick="btnCancel_Click" />&nbsp;
                                     <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Show" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="divMsg" runat="server" />

    <script language="javascript" type="text/javascript">
        function totAll(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (headchk.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }
    </script>
</asp:Content>

