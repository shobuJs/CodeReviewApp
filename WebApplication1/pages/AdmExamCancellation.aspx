<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="AdmExamCancellation.aspx.cs" Inherits="Academic_AdmExamCancellation" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Content/jquery.js" type="text/javascript"></script>
    <script src="../Content/jquery.dataTables.js" language="javascript" type="text/javascript"></script>

    <%--FOLLOWING SCRIPT USED FOR THE ONLY DATE--%>
    <script src="../JAVASCRIPTS/jquery-1.5.1.js" type="text/javascript"></script>

    <script src="../JAVASCRIPTS/jquery.ui.core.js" type="text/javascript"></script>

    <script src="../JAVASCRIPTS/jquery.ui.widget.js" type="text/javascript"></script>

    <script src="../JAVASCRIPTS/jquery.ui.datepicker.js" type="text/javascript"></script>

    <script type="text/javascript" charset="utf-8">
        $(document).ready(function () {

            $(".display").dataTable({
                "bJQueryUI": true,
                "sPaginationType": "full_numbers"
            });

        });

    </script>

    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>

    <table cellpadding="2" cellspacing="2" border="0" width="100%">
        <%--  Reset the sample so it can be played again --%>
        <tr>
            <td class="vista_page_title_bar" valign="top" style="height: 30px" colspan="2">ADMISSION CANCELLATION&nbsp;
                <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>
        <%--  Enable the button so it can be played again --%>
        <tr>
            <td colspan="2">
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
            <td valign="top">
                <fieldset style="width: 100%">

                    <table cellpadding="2" cellspacing="2" border="0" width="100%">

                        <tr>
                            <td>
                                <span class="validstar">*</span>
                                Registration no.</td>
                            <td>
                                <asp:TextBox ID="txtSearchText" runat="server" Width="280px"
                                    ToolTip="Enter text to search." TabIndex="3" />&nbsp;
                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                                    ValidationGroup="search" TabIndex="4" />
                                <asp:RequiredFieldValidator ID="valSearchText" runat="server" ControlToValidate="txtSearchText"
                                    Display="None" ErrorMessage="Please enter text to search." SetFocusOnError="true"
                                    ValidationGroup="search" Width="10%" />
                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="search" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td>
                <div runat="server" id="divInfo" visible="false">
                    <fieldset class="fieldset">
                        <legend class="legend">Basic Student Information</legend>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td style="width: 20%; text-align: right;">Student Name :
                                </td>
                                <td class="form_left_text">
                                    <asp:Label ID="lblName" runat="server" Font-Bold="True" />
                                </td>
                                <td rowspan="6">
                                    <asp:Image ID="imgStud" runat="server" Height="128px" ImageUrl="~/IMAGES/nophoto.jpg"
                                        Width="115px" BorderColor="Black" BorderWidth="1" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%; text-align: right;">Mobile No : :
                                </td>
                                <td class="form_left_text">

                                    <asp:Label ID="lblMobile" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%; text-align: right;">Admission Batch :
                                </td>
                                <td class="form_left_text">
                                    <asp:Label ID="lblAdmBatch" runat="server" Font-Bold="True"></asp:Label>
                                    &nbsp;&nbsp; Current Semester :
                                                <asp:Label ID="lblSemester" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%; text-align: right;">Degree / Branch :
                                </td>
                                <td class="form_left_text">
                                    <asp:Label ID="lblBranch" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%; text-align: right;">Regulation :
                                </td>
                                <td class="form_left_text">
                                    <asp:Label ID="lblScheme" runat="server" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2"></td>

        </tr>
        <tr runat="server" id="trcourse" visible="false">
            <td colspan="2">
                <fieldset class="fieldset">
                    <legend class="legend">Course list</legend>
                    <asp:ListView ID="lvCourseDetails" runat="server">
                        <LayoutTemplate>
                            <div id="listViewGrid" class="vista-grid">
                                <div class="titlebar">
                                    Registered Courses Details
                                </div>
                                <table id="tblCourseDetails" class="datatable" cellpadding="0" cellspacing="0">
                                    <tr class="header">

                                        <th>Coursename
                                        </th>
                                        <th>ccode
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
                                    <%# Eval("COURSENAME")%>
                                </td>
                                <td>
                                    <%# Eval("CCODE")%>
                                </td>

                            </tr>
                        </ItemTemplate>
                    </asp:ListView>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td valign="top" colspan="2">
                <div id="divRemark" runat="server" visible="false">
                    Remark of Canceling Status:<br />
                    <asp:TextBox ID="txtRemark" Rows="4" TextMode="MultiLine" Width="450px" MaxLength="400"
                        runat="server" /><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtRemark"
                            Display="None" ErrorMessage="Please enter text in Remark." SetFocusOnError="true"
                            ValidationGroup="can" Width="10%" />
                </div>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                    ValidationGroup="can" TabIndex="4" />
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true"
                    ShowSummary="false" ValidationGroup="can" />
            </td>
        </tr>
    </table>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>
