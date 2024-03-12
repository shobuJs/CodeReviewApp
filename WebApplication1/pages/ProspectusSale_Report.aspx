<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ProspectusSale_Report.aspx.cs" Inherits="ACADEMIC_ProspectusSale_Report" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function RunThisAfterEachAsyncPostback() {
            $(function () {
                $("#<%=txtRecDate.ClientID%>").datepicker({
			        changeMonth: true,
			        changeYear: true,
			        dateFormat: 'dd/mm/yy',
			        yearRange: '1975:' + getCurrentYear()
			    });
			});

            $(function () {
                $("#<%=txtToDate.ClientID%>").datepicker({
			        changeMonth: true,
			        changeYear: true,
			        dateFormat: 'dd/mm/yy',
			        yearRange: '1975:' + getCurrentYear()
			    });
			});

            function getCurrentYear() {
                var cDate = new Date();
                return cDate.getFullYear();
            }
        }
    </script>

    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>

    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <%-- Flash the text/border red and fade in the "close" button --%>
        <tr>
            <td class="vista_page_title_bar" valign="top" style="height: 30px">PROSPECTUS SALE REPORT
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
            </td>
        </tr>
    </table>
    <br />
    <fieldset class="fieldset1">
        <legend class="legend">Prospectus Sale Report</legend>
        <asp:UpdatePanel ID="updPnl" runat="server">
            <ContentTemplate>
                <table cellpadding="2" cellspacing="2" border="0" width="100%">
                    <tr>
                        <td width="5%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Report Type :
                        </td>
                        <td colspan="3">
                            <asp:RadioButtonList ID="rdolistReport" runat="server" OnSelectedIndexChanged="rdolistReport_SelectedIndexChanged"
                                AutoPostBack="True" TabIndex="1">
                                <asp:ListItem Value="1" Selected="True">User ID wise Report</asp:ListItem>
                                <asp:ListItem Value="2">Datewise Report</asp:ListItem>
                                <asp:ListItem Value="3">Branch wise Report</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr id="trDegree" runat="server" visible="false">
                        <td width="20%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Degree :
                        </td>
                        <td colspan="3">
                            <asp:DropDownList ID="ddlDegree" runat="server" TabIndex="4" Width="200px" AppendDataBoundItems="True"
                                AutoPostBack="true"
                                OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged"
                                ToolTip="Please Select Degree">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvDegree" runat="server"
                                ControlToValidate="ddlDegree" Display="None"
                                ErrorMessage="Please Select degree" InitialValue="0" SetFocusOnError="true"
                                ValidationGroup="submit" />
                        </td>
                    </tr>
                    <tr id="trBranch" runat="server" visible="false">
                        <td width="20%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Branch :
                        </td>
                        <td colspan="3">
                            <asp:DropDownList ID="ddlBranch" runat="server" TabIndex="5" Width="200px"
                                AppendDataBoundItems="True" ToolTip="Please Select Branch">
                                <asp:ListItem>Please Select</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="trdate" runat="server" visible="false">
                        <td width="20%" align="left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; From Date :</td>
                        <td width="20%">
                            <asp:TextBox ID="txtRecDate" runat="server" TabIndex="1" ToolTip="Please Enter Date "
                                Width="80%" />

                            <ajaxToolKit:MaskedEditExtender ID="meeFromDate" runat="server"
                                TargetControlID="txtRecDate" Mask="99/99/9999" MessageValidatorTip="true"
                                MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"
                                ErrorTooltipEnabled="True" />
                        </td>

                        <td width="5%">To :</td>
                        <td>
                            <asp:TextBox ID="txtToDate" runat="server" TabIndex="3"
                                ToolTip="Please Enter Date" Width="30%" />


                            <ajaxToolKit:MaskedEditExtender ID="meeToDate" runat="server"
                                TargetControlID="txtToDate" Mask="99/99/9999" MessageValidatorTip="true"
                                MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                        </td>

                    </tr>
                    <tr id="trUser" runat="server" visible="false">
                        <td width="20%" align="left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; User name :
                        </td>
                        <td colspan="3">
                            <asp:DropDownList ID="ddlUser" runat="server" TabIndex="3" Width="200px"
                                AppendDataBoundItems="True" ToolTip="Please Select User Name ">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvUser" runat="server" ControlToValidate="ddlUser"
                                Display="None" ErrorMessage="Please Select User." InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">
                            <asp:ValidationSummary ID="vsPayroll" runat="server" DisplayMode="List" ShowMessageBox="true"
                                ShowSummary="false" ValidationGroup="submit" Width="143px" />
                        </td>
                        <td colspan="3">
                            <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" TabIndex="6"
                                Text="Report" ValidationGroup="submit" />
                            &nbsp;
                        <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" TabIndex="7"
                            Text="Cancel" />
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">&nbsp;</td>
                        <td colspan="3">
                            <asp:RequiredFieldValidator ID="rfvFromDate" runat="server"
                                ControlToValidate="txtRecDate" Display="None"
                                ErrorMessage="Please Enter From Date" SetFocusOnError="True"
                                ValidationGroup="submit" />
                            <asp:RequiredFieldValidator ID="rfvToDate" runat="server"
                                ControlToValidate="txtToDate" Display="None"
                                ErrorMessage="Please Enter to Date" SetFocusOnError="True"
                                ValidationGroup="submit" />

                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>

    </fieldset>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>

