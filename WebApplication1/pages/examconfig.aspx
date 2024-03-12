<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="examconfig.aspx.cs" Inherits="Academic_examconfig" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table style="width: 70%;" cellpadding="0" cellspacing="0">
        <tr>
            <td class="vista_page_title_bar" valign="top" style="height: 30px">EXAM CONFIGURATION&nbsp;
            <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;" AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>

        <tr>
            <td>
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
            <td style="padding: 10px; text-align: left">
                <asp:UpdatePanel ID="Updpnldetails" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <fieldset class="fieldset">
                            <legend class="legend">Exam Configuration</legend>
                            <table cellpadding="0" cellspacing="0" width="70%">
                                <tr>
                                    <td class="form_left_label">Term :</td>
                                    <td class="form_left_text">
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true"
                                            Width="150px" />
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server"
                                            ControlToValidate="ddlSession" Display="None" ErrorMessage="Please Select Term"
                                            InitialValue="0" SetFocusOnError="True" ValidationGroup="Exam"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form_left_label">Exam Name :</td>
                                    <td class="form_left_text">
                                        <asp:DropDownList ID="ddlExamName" runat="server" AppendDataBoundItems="True" Width="150px">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="req_examname" runat="server"
                                            ControlToValidate="ddlExamName" ErrorMessage="Please Select Exam Name" ValidationGroup="Exam" Display="None" InitialValue="0" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form_left_label">Start Date :</td>
                                    <td class="form_left_text">
                                        <asp:TextBox ID="txtStartDate" runat="server" MaxLength="50" />&nbsp;
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtStartDate" PopupButtonID="Image1" />
                                        <asp:RequiredFieldValidator ID="req_startdate" runat="server" ControlToValidate="txtStartDate" Display="None" ErrorMessage="Please Enter Exam Start Date" ValidationGroup="Exam"></asp:RequiredFieldValidator>

                                        <ajaxToolKit:MaskedEditExtender ID="meeStartDate" runat="server" OnInvalidCssClass="errordate"
                                            TargetControlID="txtStartDate" Mask="99/99/9999" MessageValidatorTip="true"
                                            MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />

                                        <ajaxToolKit:MaskedEditValidator ID="mevStartDate" runat="server"
                                            ControlExtender="meeStartDate"
                                            ControlToValidate="txtStartDate"
                                            EmptyValueMessage="Please Enter Start Date"
                                            InvalidValueMessage="Start Date is Invalid (Enter dd/MM/yyyy Format)"
                                            Display="None"
                                            TooltipMessage="Please Start From Date"
                                            EmptyValueBlurredText="Empty"
                                            InvalidValueBlurredMessage="Invalid Date"
                                            ValidationGroup="Exam" SetFocusOnError="True" />
                                    </td>

                                </tr>
                                <tr>
                                    <td class="form_left_label">End Date :</td>
                                    <td class="form_left_text">
                                        <asp:TextBox ID="txtEndDate" runat="server" MaxLength="50" />&nbsp;
                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/images/calendar.png" Style="cursor: pointer" />
                                        <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtEndDate" PopupButtonID="Image2" />
                                        <asp:RequiredFieldValidator ID="req_enddate" runat="server" ControlToValidate="txtEndDate" Display="None"
                                            ErrorMessage="Please Enter Exam End Date" ValidationGroup="Exam"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:MaskedEditExtender ID="meeEndDate" runat="server" OnInvalidCssClass="errordate"
                                            TargetControlID="txtEndDate" Mask="99/99/9999" MessageValidatorTip="true"
                                            MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />

                                        <ajaxToolKit:MaskedEditValidator ID="mevEndDate" runat="server"
                                            ControlExtender="meeEndDate"
                                            ControlToValidate="txtEndDate"
                                            EmptyValueMessage="Please Enter End Date"
                                            InvalidValueMessage="End Date is Invalid (Enter dd/MM/yyyy Format)"
                                            Display="None"
                                            TooltipMessage="Please End From Date"
                                            EmptyValueBlurredText="Empty"
                                            InvalidValueBlurredMessage="Invalid Date"
                                            ValidationGroup="Exam" SetFocusOnError="True" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form_left_label"></td>
                                    <td class="form_left_text">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit"
                                            ValidationGroup="Exam" OnClick="btnSubmit_Click" Width="80px" />&nbsp;
                                    <asp:Button ID="btnReset" runat="server" Text="Cancel" CausesValidation="false"
                                        ValidationGroup="Exam" OnClick="btnReset_Click" Width="80px" />
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" DisplayMode="List" ValidationGroup="Exam" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form_left_label">&nbsp;</td>
                                    <td class="form_left_text">
                                        <asp:Label ID="lblSubmitStatus" runat="server" SkinID="lblmsg" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td class="form_button">
                                    <asp:ListView ID="lvExam" runat="server">
                                        <LayoutTemplate>
                                            <div id="demo-grid" class="vista-grid">
                                                <div class="titlebar">
                                                    Exam List
                                                </div>
                                                <table cellpadding="0" cellspacing="0" class="datatable" width="100%">
                                                    <tr class="header">
                                                        <th>Action</th>
                                                        <th>Term Name</th>
                                                        <th>Exam Name</th>
                                                        <th>Start Date</th>
                                                        <th>End Date</th>
                                                    </tr>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </table>
                                            </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr class="item" onmouseout="this.style.backgroundColor='#FFFFFF'" onmouseover="this.style.backgroundColor='#FFFFAA'">
                                                <td>
                                                    <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record"
                                                        CommandArgument='<%# Eval("ECONID") %>' ImageUrl="~/images/edit.gif"
                                                        OnClick="btnEdit_Click" ToolTip="Edit Record" />
                                                </td>
                                                <td><%# Eval("SESSION_PNAME") %></td>
                                                <td><%# Eval("EXAM_NAME")%></td>
                                                <td><%# Eval("FROMDATE","{0:dd-MMM-yyyy}")%></td>
                                                <td><%# Eval("TODATE", "{0:dd-MMM-yyyy}")%></td>
                                            </tr>
                                        </ItemTemplate>

                                        <AlternatingItemTemplate>
                                            <tr class="altitem" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFD2'">
                                                <td>
                                                    <asp:ImageButton ID="btnEdit" runat="server" AlternateText="Edit Record"
                                                        CommandArgument='<%# Eval("ECONID") %>' ImageUrl="~/images/edit.gif"
                                                        OnClick="btnEdit_Click" ToolTip="Edit Record" />
                                                </td>
                                                <td><%# Eval("SESSION_PNAME") %></td>
                                                <td><%# Eval("EXAM_NAME")%></td>
                                                <td><%# Eval("FROMDATE","{0:dd-MMM-yyyy}")%></td>
                                                <td><%# Eval("TODATE", "{0:dd-MMM-yyyy}")%></td>
                                            </tr>
                                        </AlternatingItemTemplate>


                                    </asp:ListView>
                                    <div class="vista-grid_datapager">
                                        <asp:DataPager ID="dpPager" runat="server" OnPreRender="dpPager_PreRender"
                                            PagedControlID="lvExam" PageSize="10">
                                            <Fields>
                                                <asp:NextPreviousPagerField
                                                    FirstPageText="<<" PreviousPageText="<" ButtonType="Link"
                                                    RenderDisabledButtonsAsLabels="true"
                                                    ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                                    ShowLastPageButton="false" ShowNextPageButton="false" />
                                                <asp:NumericPagerField ButtonType="Link"
                                                    ButtonCount="7" CurrentPageLabelCssClass="current" />
                                                <asp:NextPreviousPagerField
                                                    LastPageText=">>" NextPageText=">" ButtonType="Link"
                                                    RenderDisabledButtonsAsLabels="true"
                                                    ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                                    ShowLastPageButton="true" ShowNextPageButton="true" />
                                            </Fields>
                                        </asp:DataPager>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>

</asp:Content>

