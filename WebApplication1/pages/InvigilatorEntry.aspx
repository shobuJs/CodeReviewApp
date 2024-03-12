<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="InvigilatorEntry.aspx.cs" Inherits="ACADEMIC_InvigilatorEntry" Title=" " %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="z-index: 1; position: absolute; top: 75px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updInv"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <img src="../../IMAGES/ajax-loader.gif" alt="Loading" />
                Loading..
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td class="vista_page_title_bar" style="height: 30px">INVIGILATOR ENTRY
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>
        <%--  PAGE HELP --%>
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

                    function ConfirmSubmit() {
                        var ret = confirm('ARE YOU SURE TO DELETE THIS ENTRY');
                        if (ret == true)
                            return true;
                        else
                            return false;
                    }

                    function totAllSubjects(headchk) {
                        var chkid = headchk.id;
                        var frm = document.forms[0];
                        for (i = 0; i < document.forms[0].elements.length; i++) {
                            var e = frm.elements[i];
                            if (e.type == 'checkbox') {
                                if (headchk.checked == true) {
                                    e.checked = true;
                                    txtTot.value = hdfTot.value;
                                }
                                else {
                                    e.checked = false;
                                    txtTot.value = "0";
                                }
                            }
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
    <br />
    <asp:UpdatePanel ID="updInv" runat="server">
        <ContentTemplate>


            <table style="width: 80%">
                <tr>
                    <td colspan="2">
                        <asp:Panel ID="pnlList" runat="server" Width="80%" Height="350px" ScrollBars="Vertical"
                            Style="margin-right: 0px">

                            <asp:ListView ID="lvInvigilator" runat="server">
                                <LayoutTemplate>
                                    <div id="demo-grid" class="vista-grid" style="width: 80%; height: 350px">
                                        <div class="titlebar">
                                            Room Master
                                        </div>
                                        <table id="tblroom" class="datatable" cellpadding="0" cellspacing="0">
                                            <tr class="header">
                                                <th style="width: 5%">Select
                                                </th>
                                                <th style="width: 50%">Invigilator Name
                                                </th>

                                                <th style="width: 10%">No. of Duties
                                                </th>
                                            </tr>
                                            <tr id="itemPlaceholder" runat="server" />
                                        </table>
                                    </div>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                        <td style="width: 5%">
                                            <asp:CheckBox ID="cbRow" runat="server" onclick="totSubjects(this)" ToolTip='<%# Eval("UA_NO")%>' Checked='<%#Eval("[STATUS]").ToString() == "" ? false : true %>' />
                                        </td>
                                        <td style="width: 50%">
                                            <%# Eval("UA_FULLNAME")%>
                                        </td>

                                        <td style="width: 5%">
                                            <asp:TextBox ID="txtDuties" runat="server" Width="25%" Text='<%# Eval("NO_OF_DUTIES")%>'></asp:TextBox>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td style="width: 268px">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 268px"></td>
                    <td></td>
                </tr>
                <tr>
                    <td style="width: 268px">&nbsp;</td>
                    <td>
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit"
                            OnClick="btnSubmit_Click" />
                        &nbsp;
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                OnClick="btnCancel_Click" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="divMsg" runat="Server">
    </div>
</asp:Content>
