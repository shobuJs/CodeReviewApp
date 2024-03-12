<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ProspectusCancellation.aspx.cs" Inherits="ACADEMIC_ProspectusCancellation" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../Content/jquery.js" type="text/javascript"></script>

    <script src="../Content/jquery.dataTables.js" language="javascript" type="text/javascript"></script>



    <script type="text/javascript">
        function RunThisAfterEachAsyncPostback() {
            RepeaterDiv();
        }

        function RepeaterDiv() {
            $(document).ready(function () {

                $(".display").dataTable({
                    "bJQueryUI": true,
                    "sPaginationType": "full_numbers"
                });

            });
        }

    </script>



    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);

    </script>
    <table cellpadding="2" cellspacing="2" border="0" width="100%">
        <%--  Reset the sample so it can be played again --%>
        <tr>
            <td class="vista_page_title_bar" valign="top" style="height: 30px">PROSPECTUS RECEIPT CANCELLATION&nbsp;
                <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>
        <%--  Enable the button so it can be played again --%>
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


                <fieldset class="fieldset">
                    <legend class="legend">Search Student</legend>

                    <asp:UpdatePanel ID="updSearchStudent" runat="server">
                        <ContentTemplate>
                            <table id="tblpick" border="0" cellpadding="2" cellspacing="2" width="100%">
                                <tr>
                                    <td>Search By:&nbsp;&nbsp;
                            <asp:RadioButton ID="rdoRecNo" runat="server" Checked="true"
                                GroupName="search" Text="Rec. No." AutoPostBack="True"
                                OnCheckedChanged="rdoRecNo_CheckedChanged" TabIndex="1" />
                                        &nbsp;&nbsp;
                            <asp:RadioButton ID="rdoStudName" runat="server" GroupName="search"
                                Text="Name" AutoPostBack="True"
                                OnCheckedChanged="rdoStudName_CheckedChanged" TabIndex="2" />
                                        &nbsp;&nbsp;
                             <asp:RadioButton ID="rdoDate" runat="server" GroupName="search"
                                 Text="Date" OnCheckedChanged="rdoDate_CheckedChanged"
                                 AutoPostBack="True" TabIndex="3" />
                                    </td>
                                </tr>
                                <tr id="trText" runat="server" visible="true">
                                    <td>
                                        <asp:TextBox ID="txtSearchText" runat="server" ToolTip="Enter text to search."
                                            Width="350px" TabIndex="4" />
                                        &nbsp;
                                <asp:Label ID="lblDateFormat" runat="server" Text="{dd/mm/yyyy}" Visible="false">
                                </asp:Label>
                                        <asp:RequiredFieldValidator ID="valSearchText" runat="server"
                                            ControlToValidate="txtSearchText" Display="None"
                                            ErrorMessage="Please enter text to search." SetFocusOnError="true"
                                            ValidationGroup="search" />
                                    </td>
                                </tr>

                                <tr id="trDate" runat="server" visible="false">
                                    <td>
                                        <div id="div1">
                                            <div id="dvDate">
                                                From Date :
                           
                            <asp:TextBox ID="txtFromDate" runat="server" TabIndex="5" Width="12%"
                                ToolTip="Please Enter Date"></asp:TextBox>
                                                <ajaxToolKit:CalendarExtender ID="cefrmdate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                    PopupButtonID="txtFromDate" TargetControlID="txtFromDate">
                                                </ajaxToolKit:CalendarExtender>

                                                <ajaxToolKit:MaskedEditExtender ID="meeFromDate" runat="server"
                                                    TargetControlID="txtFromDate" Mask="99/99/9999" MessageValidatorTip="true"
                                                    MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"
                                                    ErrorTooltipEnabled="True" />

                                                &nbsp;&nbsp; To Date :
                            <asp:TextBox ID="txtToDate" runat="server" TabIndex="6" Width="12%"
                                ToolTip="Please Enter Date"></asp:TextBox>

                                                <ajaxToolKit:CalendarExtender ID="cetodate" runat="server"
                                                    Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtToDate"
                                                    TargetControlID="txtToDate">
                                                </ajaxToolKit:CalendarExtender>

                                                <ajaxToolKit:MaskedEditExtender ID="meeToDate" runat="server"
                                                    TargetControlID="txtToDate" Mask="99/99/9999" MessageValidatorTip="true"
                                                    MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                            </div>
                                        </div>
                                    </td>

                                </tr>


                                <tr>
                                    <td>
                                        <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click"
                                            Text="Search" ValidationGroup="search" TabIndex="7" />

                                        <asp:RequiredFieldValidator ID="rfvToDate" runat="server"
                                            ControlToValidate="txtToDate" Display="None"
                                            ErrorMessage="Please Enter to Date" SetFocusOnError="True"
                                            ValidationGroup="search" />
                                        <ajaxToolKit:MaskedEditValidator ID="mevToDate" runat="server"
                                            ControlExtender="meeToDate" ControlToValidate="txtToDate" Display="None"
                                            EmptyValueBlurredText="Empty" EmptyValueMessage="Please Enter To Date"
                                            InvalidValueBlurredMessage="Invalid Date"
                                            InvalidValueMessage="To Date is Invalid (Enter mm-dd-yyyy Format)"
                                            TooltipMessage="Please Enter To Date" ValidationGroup="search" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </fieldset>

            </td>
        </tr>
        <tr>
            <td>

                <fieldset class="fieldset">
                    <legend class="legend">Cancel Student Report</legend>

                    <table border="0" cellpadding="2" cellspacing="2" width="100%">
                        <tr>
                            <td width="10%">&nbsp;Degree :&nbsp;</td>
                            <td>

                                <asp:DropDownList ID="ddlDegree" runat="server" Width="300px" AppendDataBoundItems="true"
                                    AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" TabIndex="8"
                                    ToolTip="Please Select Degree">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                    InitialValue="0" Display="None" ValidationGroup="submit" ErrorMessage="Please Select Degree"></asp:RequiredFieldValidator>

                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;Branch :</td>
                            <td>

                                <asp:DropDownList ID="ddlBranch" runat="server" Width="300px"
                                    AppendDataBoundItems="true" TabIndex="9" ToolTip="Please Select Branch">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                    InitialValue="0" Display="None" ValidationGroup="submit" ErrorMessage="Please Select Branch"></asp:RequiredFieldValidator>

                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>

                                <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click"
                                    Text="Report" ValidationGroup="submit" TabIndex="10" />

                                &nbsp;
                             
                                <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click1"
                                    Text="Cancel" TabIndex="11" />

                            </td>
                        </tr>
                    </table>

                </fieldset>

            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="updReapeter" runat="server">
                    <ContentTemplate>
                        <asp:Repeater ID="lvSearchResults" runat="server">
                            <HeaderTemplate>
                                <div class="vista-grid">
                                    <div class="titlebar">
                                        Search Results:
                                    </div>
                                </div>
                                <table cellpadding="0" cellspacing="0" class="display" style="width: 100%;">
                                    <thead>
                                        <tr class="header">
                                            <th>Action
                                            </th>
                                            <th>Rec.No.
                                            </th>
                                            <th>Name
                                            </th>
                                            <th>Degree
                                            </th>
                                            <th>Branch
                                            </th>
                                            <th>Sale Date
                                            </th>
                                            <th>Serial No.
                                            </th>
                                            <th>Status
                                            </th>

                                        </tr>
                                        <tr id="itemPlaceholder" runat="server" />
                                        <thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                    <td align="center">
                                        <asp:ImageButton ID="btnCancel" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("DEGREENO") %>'
                                            ToolTip='<%# Eval("PROSNO") %>' OnClick="btnCancel_Click"
                                            OnClientClick="showConfirmDel(this); return false;" />
                                    </td>
                                    <td>
                                        <%# Eval("REC_NO")%>
                                    </td>
                                    <td>
                                        <%# Eval("STUDENT_NAME") %>
                                    </td>
                                    <td>
                                        <%#Eval("DEGREENO")%>
                                    </td>
                                    <td>
                                        <%#Eval("BRANCHNO")%>
                                    </td>
                                    <td>
                                        <%# (Eval("SALE_DATE").ToString() != string.Empty) ? ((DateTime)Eval("SALE_DATE")).ToShortDateString() : "-"%>
                                    </td>
                                    <td>
                                        <%# Eval("SERIAL_NO")%>
                                    </td>
                                    <td>
                                        <%# GetStatus(Eval("PROS_CANCEL")) %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody></table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td align="center">

                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List"
                    ShowMessageBox="true" ShowSummary="false" ValidationGroup="search" />
            </td>
        </tr>
        <tr>
            <td>

                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                    ValidationGroup="submit" ShowSummary="false" />
            </td>
        </tr>
    </table>



    <div id="divMsg" runat="server">
    </div>

    <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel" runat="server"
        TargetControlID="div" PopupControlID="div"
        OkControlID="btnOkDel" OnOkScript="okDelClick();"
        CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();" BackgroundCssClass="modalBackground" />
    <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
        <div style="text-align: center">
            <table>
                <tr>
                    <td align="center">
                        <img align="middle" src="images/warning.gif" alt="" /></td>
                    <td>&nbsp;&nbsp;Are you sure you want to Cancel this record?
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnOkDel" runat="server" Text="Yes" Width="50px" />
                        <asp:Button ID="btnNoDel" runat="server" Text="No" Width="50px" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>

    <script type="text/javascript" language="javascript">

        function showConfirmDel(source) {
            this._source = source;
            this._popup = $find('mdlPopupDel');

            //  find the confirm ModalPopup and show it    
            this._popup.show();
        }
        function okDelClick() {
            //  find the confirm ModalPopup and hide it    
            this._popup.hide();
            //  use the cached button as the postback source
            __doPostBack(this._source.name, '');
        }

        function cancelDelClick() {
            //  find the confirm ModalPopup and hide it 
            this._popup.hide();
            //  clear the event source
            this._source = null;
            this._popup = null;
        }

    </script>
    <%# Eval("REC_NO")%>
</asp:Content>

