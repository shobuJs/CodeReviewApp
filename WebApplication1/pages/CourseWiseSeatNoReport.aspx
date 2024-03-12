<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="CourseWiseSeatNoReport.aspx.cs" Inherits="ACADEMIC_CourseWiseSeatNoReport" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <table cellpadding="0" cellspacing="0" width="100%">

        <tr>
            <td class="vista_page_title_bar" style="height: 30px">CourseWise Student Seat No Report
                            <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>
        <tr>
            <td>
                <br />
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
    <div style="width: 80%; padding-left: 10px">
        <fieldset class="fieldset">
            <legend class="legend">Report Selection Criteria</legend>
            <asp:UpdatePanel ID="updSeatNo" runat="server">
                <ContentTemplate>
                    <table cellpadding="0" cellspacing="0" border="0" width="80%">
                        <tr>
                            <td>
                                <br />
                            </td>
                        </tr>

                        <tr style="display: none;">
                            <td class="form_left_label">Department :
                            </td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlDepartment" runat="server" AppendDataBoundItems="true"
                                    Width="400px" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">Session</td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true"
                                    Width="400px">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvSession" runat="server"
                                    ControlToValidate="ddlSession" Display="None"
                                    ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="Save"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">Scheme :
                            </td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="true"
                                    Width="400px"
                                    OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                    Display="None" InitialValue="0" ErrorMessage="Please Select Scheme"
                                    ValidationGroup="Save"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">Semester :
                            </td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true"
                                    Width="400px" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                    Display="None" InitialValue="0" ErrorMessage="Please Select Semester"
                                    ValidationGroup="Save"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">Course Name:</td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlCourseName" runat="server" AutoPostBack="True" Width="400px">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCourseName"
                                    Display="None" InitialValue="0" ErrorMessage="Please Select Course Name"
                                    ValidationGroup="Save"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="form_left_label">Exam Type:</td>
                            <td class="form_left_text">
                                <asp:DropDownList ID="ddlExamType" runat="server" AutoPostBack="True" Width="400px">
                                    <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                    <asp:ListItem Value="0">Regular Exam</asp:ListItem>
                                    <asp:ListItem Value="1">Repeat Exam</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlExamType"
                                    Display="None" InitialValue="-1" ErrorMessage="Please Select Exam Type"
                                    ValidationGroup="Save"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>

            <div style="text-align: center;">
                <table cellpadding="0" cellspacing="0" border="0" width="80%">
                    <tr>
                        <td>&nbsp;
                        </td>
                        <td class="form_left_text">
                            <asp:Button ID="btnReport" runat="server"
                                OnClick="btnReport_Click" Text="Report" Width="100px" ValidationGroup="Save" />
                            &nbsp;<asp:Button ID="btnClear" runat="server" CausesValidation="False"
                                OnClick="btnClear_Click" Text="Clear" Width="100px" />
                        </td>
                    </tr>
                    <tr>
                        <td class="form_left_label">&nbsp;</td>
                        <td class="form_left_text">
                            <asp:ValidationSummary ID="ValidationSummary2" runat="server"
                                DisplayMode="List" ShowMessageBox="true" ShowSummary="false"
                                ValidationGroup="Save" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="form_button">
                            <asp:Label ID="lblCurSession" runat="server" Visible="false" />
                        </td>
                    </tr>
                </table>
            </div>
        </fieldset>
    </div>
    <div id="divMsg" runat="server" />
</asp:Content>

