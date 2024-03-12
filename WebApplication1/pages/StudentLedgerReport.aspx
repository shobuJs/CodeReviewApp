<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="StudentLedgerReport.aspx.cs" Inherits="Academic_StudentLedgerReport"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" cellpadding="2" cellspacing="2" border="0">
        <%-- Page Title --%>
        <tr>
            <td class="vista_page_title_bar" valign="top" style="height: 30px">STUDENT LEDGER&nbsp;
                <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>
        <%--PAGE HELP--%>
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
                            Edit Record
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

                <ajaxToolKit:AnimationExtender ID="AnimationExtender1" runat="server" TargetControlID="btnHelp">
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
            <td>
                <fieldset>
                    <legend>Search Student</legend>
                    <table width="100%">
                        <tr>
                            <td>Search by:&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;<asp:RadioButton ID="rdoEnrollmentNo" Text="Enrollment No"
                                Checked="true" runat="server" GroupName="stud" TabIndex="1" />
                                &nbsp;&nbsp;<asp:RadioButton ID="rdoStudentName" Text="Student Name" runat="server"
                                    GroupName="stud" TabIndex="2" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtSearchText" runat="server" Width="350px" TabIndex="3" />&nbsp;&nbsp;
                                <asp:Button ID="btnSearch" Text="Search" runat="server" OnClick="btnSearch_Click"
                                    TabIndex="4" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                                <asp:ListView ID="lvStudentRecords" runat="server">
                                    <LayoutTemplate>
                                        <div id="listViewGrid" class="vista-grid">
                                            <div class="titlebar">
                                                Student Search Result(s)
                                            </div>
                                            <table class="datatable" cellpadding="0" cellspacing="0">
                                                <tr class="header">
                                                    <th>Report
                                                    </th>
                                                    <th>Student Name
                                                    </th>
                                                    <th>Enrollment No.
                                                    </th>
                                                    <th>Degree
                                                    </th>
                                                    <th>Branch
                                                    </th>
                                                    <th>Year
                                                    </th>
                                                    <th>Semester
                                                    </th>
                                                    <th>Batch
                                                    </th>
                                                </tr>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </table>
                                        </div>
                                    </LayoutTemplate>
                                    <EmptyDataTemplate>
                                    </EmptyDataTemplate>
                                    <ItemTemplate>
                                        <tr class="item">
                                            <td>
                                                <asp:ImageButton ID="btnShowReport" runat="server" AlternateText="Show Report" CommandArgument='<%# Eval("IDNO") %>'
                                                    ImageUrl="~/images/print.gif" ToolTip="Show Report" OnClick="btnShowReport_Click" TabIndex="5" />
                                            </td>
                                            <td>
                                                <%# Eval("NAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("ENROLLMENTNO")%>
                                            </td>
                                            <td>
                                                <%# Eval("CODE")%>
                                            </td>
                                            <td>
                                                <%# Eval("SHORTNAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("YEARNAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("SEMESTERNAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("BATCHNAME")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <EmptyDataTemplate>
                                        <div align="center" class="data_label">
                                            -- No Student Record Found --
                                        </div>
                                    </EmptyDataTemplate>
                                </asp:ListView>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                    OnClick="btnCancel_Click" TabIndex="6" />
                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                    ShowSummary="false" ValidationGroup="report" />
            </td>
        </tr>
    </table>
    <br />
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
