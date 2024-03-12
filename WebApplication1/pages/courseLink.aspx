<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="courseLink.aspx.cs" Inherits="Registration_courseLink" Title=" " %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="Updpnldetails" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td colspan="2" class="vista_page_title_bar" valign="top" style="height: 30px">SectionWise Course&nbsp;
            <!-- Button used to launch the help (animation) -->
                        <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;" AlternateText="Page Help" ToolTip="Page Help" />
                    </td>
                </tr>

                <%--PAGE HELP--%>
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
                    <td colspan="2" style="padding: 10px">
                        <fieldset class="fieldset" style="width: 100%">
                            <legend class="legend">SectionWise Course Entry</legend>
                            <table cellpadding="0" cellspacing="0" width="70%">
                                <tr>
                                    <td class="form_left_label">Term : </td>
                                    <td class="form_left_text">
                                        <asp:Label ID="lblCurrentSession" runat="server" Font-Bold="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form_left_label">Program : </td>
                                    <td class="form_left_text">
                                        <asp:DropDownList ID="ddlScheme" runat="server" Width="450px"
                                            ValidationGroup="courseLink" AppendDataBoundItems="true"
                                            ToolTip="Select Scheme">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvScheme" runat="server"
                                            ControlToValidate="ddlScheme" Display="None" ErrorMessage="Please select scheme."
                                            ValidationGroup="courseLink" InitialValue="0"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form_left_label">Section : </td>
                                    <td class="form_left_text">
                                        <asp:DropDownList ID="ddlSection" runat="server" Width="110px"
                                            AppendDataBoundItems="true" ValidationGroup="courseLink"
                                            ToolTip="Select Section"
                                            OnSelectedIndexChanged="ddlSection_SelectedIndexChanged"
                                            AutoPostBack="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSection" runat="server"
                                            ControlToValidate="ddlSection" Display="None" ErrorMessage="Please select section."
                                            ValidationGroup="courseLink" InitialValue="0"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form_left_label">No. Of Subjects : </td>
                                    <td class="form_left_text">
                                        <asp:TextBox ID="txtTotChk" runat="server" Width="30px" Enabled="false" ValidationGroup="courseLink" ToolTip="Total Subject Selected" />
                                        <asp:HiddenField ID="hdfTot" runat="server" Value="0" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form_left_label"></td>
                                    <td class="form_left_text">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit"
                                            OnClick="btnSubmit_Click" ValidationGroup="courseLink" ToolTip="Submit"
                                            Width="80px" />&nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                        OnClick="btnCancel_Click" CausesValidation="False" ToolTip="Cancel"
                        Width="80px" />
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="courseLink" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form_left_label"></td>
                                    <td class="form_left_text">
                                        <asp:Label ID="lblstatus" runat="server" SkinID="Errorlbl" /></td>
                                </tr>

                            </table>
                        </fieldset>
                    </td>
                </tr>



                <tr>
                    <td align="center" colspan="2" style="padding: 10px">
                        <asp:ListView ID="lvCourseLink" runat="server" OnItemDataBound="lvCourseLink_ItemDataBound">
                            <LayoutTemplate>
                                <div id="demo-grid" class="vista-grid">
                                    <div class="titlebar">Course List</div>
                                    <table class="datatable" cellpadding="0" cellspacing="0" width="100%">
                                        <tr class="header">
                                            <th>
                                                <asp:CheckBox ID="cbHead" runat="server" onclick="totAllSubjects(this)" /></th>
                                            <th style="display: none">CCode</th>
                                            <th>Course Name</th>
                                            <th>Program Name</th>
                                        </tr>
                                        <tr id="itemPlaceholder" runat="server" />
                                    </table>
                                </div>
                            </LayoutTemplate>

                            <ItemTemplate>
                                <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                    <td>
                                        <asp:CheckBox ID="cbRow" runat="server" onclick="totSubjects(this)" ToolTip='<%# Eval("CNO") %>' /></td>
                                    <td style="display: none">
                                        <asp:Label ID="lblCCODE" runat="server" Text='<%# Eval("CCODE")%>' ToolTip='<%# Eval("COURSENO")%>' /></td>
                                    <td><%# Eval("COURSENAME")%></td>
                                    <td><%# Eval("SCHEMENAME")%></td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr class="altitem" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFD2'">
                                    <td>
                                        <asp:CheckBox ID="cbRow" runat="server" onclick="totSubjects(this)" ToolTip='<%# Eval("CNO") %>' /></td>
                                    <td style="display: none">
                                        <asp:Label ID="lblCCODE" runat="server" Text='<%# Eval("CCODE")%>' ToolTip='<%# Eval("COURSENO")%>' /></td>
                                    <td><%# Eval("COURSENAME")%></td>
                                    <td><%# Eval("SCHEMENAME")%></td>
                                </tr>
                            </AlternatingItemTemplate>
                        </asp:ListView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">

        function totSubjects(chk) {
            var txtTot = document.getElementById('<%= txtTotChk.ClientID %>');

        if (chk.checked == true)
            txtTot.value = Number(txtTot.value) + 1;
        else
            txtTot.value = Number(txtTot.value) - 1;

    }

    function totAllSubjects(headchk) {
        var txtTot = document.getElementById('<%= txtTotChk.ClientID %>');
	    var hdfTot = document.getElementById('<%= hdfTot.ClientID %>');

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

	    if (headchk.checked == true)
	        txtTot.value = hdfTot.value;
	    else
	        txtTot.value = 0;
	}
    </script>
</asp:Content>
