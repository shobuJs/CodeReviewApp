<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="admissionprocess.aspx.cs" Inherits="ACADEMIC_admissionprocess" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--Following code used by date--%>>
 <script type="text/javascript">

     $(function () {
         $("#<%=txtFromDate.ClientID%>").datepicker({
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

 </script>

    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td colspan="2" class="vista_page_title_bar" style="height: 30px">ADMISSION PROCESS 
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
                <br />
            </td>
        </tr>
        <tr>
            <td class="form_left_label" style="width: 50%">
                <fieldset class="fieldset">
                    <legend class="legend">Year</legend>
                    <asp:RadioButton ID="rbAllYear" runat="server" Text="All Year" GroupName="year" Checked="true" TabIndex="1" />&nbsp;&nbsp;
                <asp:RadioButton ID="rbFirstYear" runat="server" Text="First Year" GroupName="year" TabIndex="2" />
                </fieldset>
            </td>
            <td class="form_left_text">
                <fieldset class="fieldset">
                    <legend class="legend">Degree Type</legend>
                    <asp:RadioButton ID="rbUG" runat="server" Text="UG" GroupName="degree" Checked="true" TabIndex="3" />&nbsp;&nbsp;
                <asp:RadioButton ID="rbPG" runat="server" Text="PG" GroupName="degree" TabIndex="4" />&nbsp;&nbsp;
                <asp:RadioButton ID="rbOT" runat="server" Text="OT" GroupName="degree" TabIndex="5" />
                </fieldset>
            </td>
        </tr>

        <tr>
            <td class="form_left_text" colspan="2">
                <fieldset class="fieldset">
                    <legend class="legend">Date</legend>
                    <div style="padding: 5px; vertical-align: middle">
                        From Date :
                    <asp:TextBox ID="txtFromDate" runat="server" ValidationGroup="submit" TabIndex="6" />&nbsp;
                    <asp:RequiredFieldValidator ID="rfvFromDate" runat="server"
                        ControlToValidate="txtFromDate" Display="None"
                        ErrorMessage="Please Enter From Date" SetFocusOnError="True"
                        ValidationGroup="submit" />
                        <ajaxToolKit:MaskedEditExtender ID="meeFromDate" runat="server"
                            TargetControlID="txtFromDate" Mask="99/99/9999" MessageValidatorTip="true"
                            MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />

                        <ajaxToolKit:MaskedEditValidator ID="mevFromDate" runat="server"
                            ControlExtender="meeFromDate"
                            ControlToValidate="txtFromDate"
                            EmptyValueMessage="Please Enter From Date"
                            InvalidValueMessage="From Date is Invalid (Enter mm-dd-yyyy Format)"
                            Display="None"
                            TooltipMessage="Please Enter From Date"
                            EmptyValueBlurredText="Empty"
                            InvalidValueBlurredMessage="Invalid Date"
                            ValidationGroup="submit" SetFocusOnError="True" />

                        &nbsp;&nbsp;
                    To :
                    <asp:TextBox ID="txtToDate" runat="server" ValidationGroup="submit" TabIndex="7" />&nbsp;
                    <asp:RequiredFieldValidator ID="rfvToDate" runat="server"
                        ControlToValidate="txtToDate" Display="None"
                        ErrorMessage="Please Enter to Date" SetFocusOnError="True"
                        ValidationGroup="submit" />
                        <ajaxToolKit:MaskedEditExtender ID="meeToDate" runat="server"
                            TargetControlID="txtToDate" Mask="99/99/9999" MessageValidatorTip="true"
                            MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />

                        <ajaxToolKit:MaskedEditValidator ID="mevToDate" runat="server"
                            ControlExtender="meeToDate"
                            ControlToValidate="txtToDate"
                            EmptyValueMessage="Please Enter To Date"
                            InvalidValueMessage="To Date is Invalid (Enter mm-dd-yyyy Format)"
                            Display="None"
                            TooltipMessage="Please Enter To Date"
                            EmptyValueBlurredText="Empty"
                            InvalidValueBlurredMessage="Invalid Date"
                            ValidationGroup="submit" />

                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" Width="80px" OnClick="btnSubmit_Click" ValidationGroup="submit" ToolTip="Submit" TabIndex="8" />
                        &nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="80px" OnClick="btnCancel_Click" CausesValidation="false" ToolTip="Cancel" TabIndex="9" />
                        &nbsp;<asp:Button ID="btnDownload" runat="server" Text="Export" Width="80px"
                            CausesValidation="false" ToolTip="Cancel" TabIndex="10"
                            OnClick="btnDownload_Click" />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                            ShowMessageBox="True" ShowSummary="False" ValidationGroup="submit" />
                    </div>
                </fieldset>
            </td>
        </tr>

        <tr>
            <td colspan="2" class="form_button">
                <asp:Label ID="lblStatus" runat="server" SkinID="Errorlbl" />
            </td>
        </tr>

        <tr>
            <td colspan="2" style="padding-left: 5px" align="left">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updList">
                    <ProgressTemplate>
                        <asp:Image ID="imgLoad" runat="server" ImageUrl="~/images/ajax-loader.gif" />
                        <span style="font-size: 8pt">Loading...</span>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </td>
        </tr>

        <tr>
            <td colspan="2" align="center" style="padding: 5px"></td>
        </tr>
        <asp:UpdatePanel ID="updList" runat="server">
            <ContentTemplate>
                <asp:Label ID="lblNote" runat="server" SkinID="lblmsg" Text="<i><b>Note : </b></i>Admission List will be refreshed after every 10 seconds" />
                <asp:PlaceHolder ID="phList" runat="server" />
                <asp:Timer ID="tmList" runat="server" Enabled="False" Interval="10000" OnTick="tmList_Tick">
                </asp:Timer>
            </ContentTemplate>
        </asp:UpdatePanel>
    </table>

</asp:Content>

