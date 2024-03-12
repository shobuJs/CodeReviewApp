<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Course_Equivalance.aspx.cs" Inherits="ACADEMIC_Course_Equivalance"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td class="vista_page_title_bar" style="height: 30px">COURSE EQUIVALANCE
                <!-- Button used to launch the help (animation) -->
                <asp:ImageButton
                    ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
            <td>
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
            </td>
        </tr>
        <%-- Flash the text/border red and fade in the "close" button --%>        <%--  Shrink the info panel out of view --%>
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
                <script type="text/javascript">

                    function toggle_panel(chkbox) {
                        var panel = document.getElementById('<%=pnlRtmCourse.ClientID %>');
         var panel1 = document.getElementById('<%=lvNITCourse.ClientID %>');
         chkbox.checked ? panel.style.display = 'inline' : panel.style.display = 'none';
     }

                </script>
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
            <td style="padding-left: 15px;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <fieldset class="fieldset">
                            <legend class="legend">Course Equivalance</legend>
                            <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td class="form_left_label" style="width: 15%">&nbsp;
                                    </td>
                                    <td class="form_left_text">
                                        <%--  Reset the sample so it can be played again --%>
                                    </td>
                                    <td class="form_left_text">&nbsp;
                                    </td>
                                </tr>
                                <tr style="display: none;">
                                    <td class="form_left_label" style="width: 15%">Session :
                                    </td>
                                    <td class="form_left_text">
                                        <asp:DropDownList ID="ddlSession" AutoPostBack="true" runat="server" AppendDataBoundItems="true"
                                            Width="400px" Font-Bold="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please select</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form_left_label" style="width: 15%">Degree :
                                    </td>
                                    <td class="form_left_text">
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" Width="400px"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ErrorMessage="Please Select Degree"
                                            ControlToValidate="ddlDegree" Display="None" ValidationGroup="submit" InitialValue="0" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select Degree"
                                            ControlToValidate="ddlDegree" Display="None" ValidationGroup="report" InitialValue="0" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form_left_label">Branch :
                                    </td>
                                    <td class="form_left_text">
                                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" Width="400px"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ErrorMessage="Please Select Branch"
                                            ControlToValidate="ddlBranch" Display="None" ValidationGroup="submit" InitialValue="0" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Select Branch"
                                            ControlToValidate="ddlBranch" Display="None" ValidationGroup="report" InitialValue="0" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form_left_label">Scheme :
                                    </td>
                                    <td class="form_left_text">
                                        <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="true" Width="400px"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ErrorMessage="Please Select Scheme"
                                            ControlToValidate="ddlScheme" Display="None" ValidationGroup="submit" InitialValue="0" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Select Scheme"
                                            ControlToValidate="ddlScheme" Display="None" ValidationGroup="report" InitialValue="0" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form_left_label">Semester :
                                    </td>
                                    <td class="form_left_text">
                                        <asp:DropDownList ID="ddlSemester" runat="server" Width="400px" AppendDataBoundItems="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ErrorMessage="Please Select Semester"
                                            ControlToValidate="ddlSemester" Display="None" ValidationGroup="submit" InitialValue="0" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form_left_label">CSVTU Course:
                                    </td>
                                    <td class="form_left_text">
                                        <asp:DropDownList ID="ddlCourse_auto" runat="server" AppendDataBoundItems="true"
                                            AutoPostBack="True" Width="400px" OnSelectedIndexChanged="ddlCourse_auto_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCourse_auto" runat="server" ControlToValidate="ddlCourse_auto"
                                            Display="None" ErrorMessage="Please Select CSVTU Course" InitialValue="0"
                                            ValidationGroup="submit" />
                                        &nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form_left_label">&nbsp;</td>
                                    <td class="form_left_text">
                                        <asp:CheckBox ID="chkNonequi" runat="server" Text="Non Equivalence"
                                            onclick="toggle_panel(this);" AutoPostBack="True"
                                            OnCheckedChanged="chkNonequi_CheckedChanged" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form_left_label">NIT Courses :
                                    </td>
                                    <td class="form_left_text">
                                        <asp:Panel ID="pnlRtmCourse" runat="server" Height="300px"
                                            ScrollBars="Vertical" Visible="false"
                                            Width="95%">
                                            <asp:ListView ID="lvNITCourse" runat="server" Enabled="true">
                                                <LayoutTemplate>
                                                    <div id="demo-grid" class="vista-grid">
                                                        <div class="titlebar">
                                                            NIT COURSES
                                                        </div>
                                                        <table class="datatable" cellpadding="0" cellspacing="0" width="100%">
                                                            <tr class="header">
                                                                <th>Select
                                                                </th>
                                                                <th>Course Name
                                                                </th>
                                                                <th>Semester
                                                                </th>
                                                            </tr>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </table>
                                                    </div>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                        <td>
                                                            <asp:CheckBox ID="chkSelect" runat="server" ToolTip='<%# Eval("COURSENO" )%>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCourse" Text='<%# Eval("COURSE_NAME")%>' ToolTip='<%# Eval("COURSENO" )%>'
                                                                runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblSemester" Text='<%# Eval("SEMESTERNAME")%>' ToolTip='<%# Eval("SEMESTERNO" )%>'
                                                                runat="server"></asp:Label>
                                                            <asp:Label ID="lblScheme" Text='<%# Eval("SCHEMENO") %>' runat="server" Visible="false" ToolTip='<%# Eval("CCODE" )%>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr style="display: none;">
                                    <td class="form_left_label">Exempted :</td>
                                    <td class="form_left_text">
                                        <asp:RadioButtonList ID="rblExemted" runat="server" AutoPostBack="True"
                                            RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form_left_label">&nbsp;
                                    </td>
                                    <td class="form_left_text" style="padding-top: 10px; padding-bottom: 10px;">&nbsp;<asp:Button ID="btnSubmit" runat="server" Text="Submit" Width="80px" ValidationGroup="submit"
                                        OnClick="btnSubmit_Click" />
                                        &nbsp;<asp:Button ID="btnReport" runat="server" Text="Report" Width="80px"
                                            ValidationGroup="report" OnClick="btnReport_Click" />&nbsp;
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="80px" CausesValidation="false"
                                            OnClick="btnCancel_Click" />
                                        <asp:Button ID="btnDelete" runat="server" Text="Delete" Width="80px" OnClick="btnDelete_Click" />
                                        &nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                            ShowSummary="false" DisplayMode="List" ValidationGroup="submit" />
                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true"
                                            ShowSummary="false" DisplayMode="List" ValidationGroup="report" />

                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:ListView ID="lvEquivalanceCourse" runat="server" Enabled="true">
                                            <LayoutTemplate>
                                                <div id="demo-grid" class="vista-grid">
                                                    <div class="titlebar">
                                                        EQUIVALANCE COURSE LIST
                                                    </div>
                                                    <table class="datatable" cellpadding="0" cellspacing="0" width="100%">
                                                        <tr class="header">
                                                            <th>Select
                                                            </th>
                                                            <th>New Course
                                                            </th>
                                                            <th>New Scheme
                                                            </th>
                                                            <th>Old Course
                                                            </th>
                                                            <th>Old Scheme
                                                            </th>
                                                        </tr>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </table>
                                                </div>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                    <td>
                                                        <asp:CheckBox ID="chkCourse" runat="server" ToolTip='<%# Eval("OLD_SCHEMENO" )%>' />
                                                        <asp:Label ID="lbl" runat="server" Text='<%# Eval("OLD_COURSENO" )%>' Visible="false" />

                                                    </td>
                                                    <td>
                                                        <%# Eval("NEW_CCODE")%> - <%# Eval("NEW_COURSENAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("NEW_SCHEME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("OLD_CCODE")%> - <%# Eval("OLD_COURSENAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("OLD_SCHEME")%>                                                        
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <div id="divMsg" runat="server">
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>

</asp:Content>
