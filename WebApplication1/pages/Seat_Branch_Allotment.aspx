<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Seat_Branch_Allotment.aspx.cs" Inherits="ACADEMIC_Seat_Branch_Allotment" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updAllot"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <img src="../images/ajax-loader.gif" alt="Loading" />
                Please Wait..
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel ID="updAllot" runat="server">
        <ContentTemplate>
            <table cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td class="vista_page_title_bar" style="height: 30px" colspan="2">ADMISSION - SEAT(BRANCH) ALLOTMENT
                        <!-- Button used to launch the help (animation) -->
                        <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                            AlternateText="Page Help" ToolTip="Page Help" />
                    </td>
                </tr>
                <%--PAGE HELP--%>
                <%--JUST CHANGE THE IMAGE AS PER THE PAGE. NOTHING ELSE--%>
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
            </table>
            <br />
            <table>
                <tr>
                    <td style="padding-left: 2px; padding-top: 2px; vertical-align: top">
                        <fieldset class="fieldset">
                            <legend class="legend">Seat Allotment</legend>
                            <table cellpadding="1" cellspacing="1" width="100%">
                                <tr>
                                    <td style="width: 30%">Session :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" Font-Bold="true"
                                            AutoPostBack="True" Width="150px">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ErrorMessage="Please Select Session"
                                            ControlToValidate="ddlSession" Display="None" ValidationGroup="allot" InitialValue="0" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Reg. No.:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRegistrationNo" runat="server" ToolTip="Please Enter Registration no"
                                            ValidationGroup="submit" AutoPostBack="True" OnTextChanged="txtRegistrationNo_TextChanged"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvRegistrationNo" runat="server" Display="None"
                                            ErrorMessage="Please Enter Registration No." ValidationGroup="submit" ControlToValidate="txtRegistrationNo"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="cvRegistrationNo" runat="server" ControlToValidate="txtRegistrationNo"
                                            Display="None" ErrorMessage="Please Enter Numbers Only" Operator="DataTypeCheck"
                                            SetFocusOnError="True" Type="Integer" ValueToCompare="submit"></asp:CompareValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Round :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlRound" runat="server" AppendDataBoundItems="true" Width="150px">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvRound" runat="server" ControlToValidate="ddlRound"
                                            Display="None" ErrorMessage="Please Select Round" InitialValue="0" ValidationGroup="allot" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Degree :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" Width="100%"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ErrorMessage="Please Select Degree"
                                            ControlToValidate="ddlDegree" Display="None" ValidationGroup="allot" InitialValue="0" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Branch :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                            Width="100%">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" ErrorMessage="Please Select Branch" InitialValue="0" ValidationGroup="allot" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Admission Batch :</td>
                                    <td valign="top">
                                        <asp:DropDownList ID="ddlAdmissionBatch" runat="server"
                                            AppendDataBoundItems="true" AutoPostBack="True" Width="100%">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Payment Type :</td>
                                    <td valign="top">
                                        <asp:DropDownList ID="ddlPaymentType" runat="server"
                                            AppendDataBoundItems="true" AutoPostBack="True" Width="100%">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Admission Type :</td>
                                    <td valign="top">
                                        <asp:DropDownList ID="ddlAdmissionType" runat="server"
                                            AppendDataBoundItems="true" AutoPostBack="True" Width="100%">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;
                                    </td>
                                    <td style="padding-top: 10px; padding-bottom: 10px;">
                                        <asp:Button ID="btnAllot" runat="server" Text="Allot" Width="80px" ValidationGroup="allot"
                                            OnClick="btnAllot_Click" Enabled="False" />&nbsp;&nbsp;
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="80px" OnClick="btnCancel_Click" />
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                            ShowSummary="false" DisplayMode="List" ValidationGroup="allot" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2"></td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <span style="font-weight: bold; color: #FF0000"><b>Note : </b><i>Branch once alloted
                                            from this page, cannot be alloted again.</i> </span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;
                                    </td>
                                    <td style="padding-top: 10px; padding-bottom: 10px;">&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:ListView ID="lvAdmission" runat="server">
                                            <LayoutTemplate>
                                                <div class="vista-grid">
                                                    <div class="titlebar">
                                                        Admission Status AIEEE
                                                    </div>
                                                    <table cellpadding="0" cellspacing="0" class="datatable" style="width: 100%;">
                                                        <thead>
                                                            <tr class="header">
                                                                <th>Branch
                                                                </th>
                                                                <th>Minority Seats
                                                                </th>
                                                                <th>Admission Count
                                                                </th>
                                                                <th>Remaining Seats
                                                                </th>
                                                            </tr>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </thead>
                                                    </table>
                                                </div>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr class="item" onmouseout="this.style.backgroundColor='#FFFFFF'" onmouseover="this.style.backgroundColor='#FFFFAA'">
                                                    <td>
                                                        <%# Eval("SHORTNAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("AIEEEACTUAL_INTAKE")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("ADMCOUNT")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("REMAININGSEAT")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:ListView ID="lvAdmissionMhcet" runat="server">
                                            <LayoutTemplate>
                                                <div class="vista-grid">
                                                    <div class="titlebar">
                                                        Admission Status MHCET
                                                    </div>
                                                    <table cellpadding="0" cellspacing="0" class="datatable" style="width: 100%;">
                                                        <thead>
                                                            <tr class="header">
                                                                <th>Branch
                                                                </th>
                                                                <th>Minority Seats
                                                                </th>
                                                                <th>Admission Count
                                                                </th>
                                                                <th>Remaining Seats
                                                                </th>
                                                            </tr>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </thead>
                                                    </table>
                                                </div>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr class="item" onmouseout="this.style.backgroundColor='#FFFFFF'" onmouseover="this.style.backgroundColor='#FFFFAA'">
                                                    <td>
                                                        <%# Eval("SHORTNAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("MHCETACTUAL_INTAKE")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("ADMCOUNT")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("REMAININGSEAT")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        &nbsp;
                    </td>
                    <td style="padding-left: 5px; padding-top: 5px; width: 50%; vertical-align: top">
                        <fieldset class="fieldset" style="vertical-align: top">
                            <legend class="legend">Student Detail</legend>
                            <table align="left" style="margin-left: 0px;" width="100%">
                                <tr>
                                    <td style="margin-left: 0px; width: 50%">Student Name
                                    </td>
                                    <td>:
                                    </td>
                                    <td style="width: 50%">
                                        <asp:Label ID="lblStudentname" runat="server" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Minority
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblMinority" runat="server" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Category
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCategory" runat="server" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblMhcet" runat="server" Font-Bold="False"></asp:Label>
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblMhcetScore" runat="server" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblPcm" runat="server" Font-Bold="False"></asp:Label>
                                    </td>
                                    <td>:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblPcmSCore" runat="server" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3"></td>
                                </tr>
                            </table>
                        </fieldset>
                        &nbsp;<br />
                        <br />
                        <br />
                        <div style="border: 1px solid #F47A00; padding: 5px; text-align: center;">
                            <asp:ListView ID="lvBranchPref" runat="server">
                                <LayoutTemplate>
                                    <div class="vista-grid">
                                        <div class="titlebar">
                                            Branch Preference(s)
                                        </div>
                                        <table cellpadding="0" cellspacing="0" class="datatable" style="width: 100%;">
                                            <thead>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </thead>
                                        </table>
                                    </div>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr class="item" onmouseout="this.style.backgroundColor='#FFFFFF'" onmouseover="this.style.backgroundColor='#FFFFAA'">
                                        <td>
                                            <%# Eval("BRANCHLNAME")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <EmptyDataTemplate>
                                    -- No Branch Preference(s) entered --
                                </EmptyDataTemplate>
                            </asp:ListView>
                        </div>
                    </td>
                </tr>
            </table>
            <div style="padding: 10px">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

