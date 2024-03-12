<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="UaimsConfiguration.aspx.cs" Inherits="ACADEMIC_UaimsConfiguration"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
        //ONLY ONE CHECK BOX ALLOW FOR SELECTION
        function countCheked(chk, chk1, varst) {
            alert("1");
            if (varst == "Y") {
                if (chk1.checked == true) {
                    chk1.checked = false;
                }
                else {
                    if (chk.checked == true) {
                        chk1.checked = false;
                    }
                    else {
                        chk.checked = true;
                    }
                }
            }
            else if (varst == "N") {
                if (chk1.checked == true) {
                    chk.checked = false;
                }
            }
        }



    </script>

    <script src="../../Content/jquery.js" type="text/javascript"></script>

    <script src="../../Content/jquery.dataTables.js" language="javascript" type="text/javascript"></script>

    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>

    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td>
                <table class="vista_page_title_bar" width="100%">
                    <tr>
                        <td style="height: 30px">CONFIGURATION&nbsp;&nbsp
                            <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                                AlternateText="Page Help" ToolTip="Page Help" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center">
                <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF; border: solid 1px #D0D0D0;">
                </div>
                <!-- Info panel to be displayed as a flyout when the button is clicked -->
                <div id="info" style="display: none; width: 250px; z-index: 2; font-size: 12px; border: solid 1px #CCCCCC; background-color: #FFFFFF; padding: 5px;">
                    <div id="btnCloseParent" style="float: right;">
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

                <ajaxToolKit:AnimationExtender ID="OpenAnimation" runat="server" TargetControlID="btnHelp"
                    Enabled="True">
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
                <ajaxToolKit:AnimationExtender ID="CloseAnimation" runat="server" TargetControlID="btnClose"
                    Enabled="True">
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
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Style="color: Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <br />
                <fieldset class="fieldset">
                    <legend class="legend">Event List </legend>
                    <div style="padding: 20px">
                        <asp:ListView ID="lvConfiguration" runat="server">
                            <LayoutTemplate>
                                <div id="listviewgrid" class="vista-grid">
                                    <div class="titlebar">
                                        Programs
                                    </div>
                                    <table id="table1" class="datatable" cellspacing="0" cellpadding="0" runat="server">
                                        <tr class="header">
                                            <th>Yes
                                            </th>
                                            <th>No
                                            </th>
                                            <th>Event Name
                                            </th>
                                        </tr>
                                        <tr id="itemplaceholder" runat="server">
                                        </tr>
                                    </table>
                                </div>
                            </LayoutTemplate>
                            <EmptyDataTemplate>
                            </EmptyDataTemplate>
                            <ItemTemplate>
                                <tr class="item">
                                    <td>
                                        <asp:CheckBox ID="chkYes" runat="server" Checked='<%#Eval("STATUS").ToString() == "Y" ? true:false %>' />
                                        <asp:HiddenField ID="hidEventNoY" runat="server" Value='<%# Eval("EVENTNO") %>' />
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkNo" runat="server" Checked='<%#Eval("STATUS").ToString() == "N" ? true:false %>' />
                                        <asp:HiddenField ID="hidEventNoN" runat="server" Value='<%# Eval("EVENTNO") %>' />
                                    </td>
                                    <td>
                                        <%# Eval("EVENTNAME") %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <div align="center" class="data_label">
                                    -- No Student Record Found --
                                </div>
                            </EmptyDataTemplate>
                        </asp:ListView>
                    </div>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td align="center">
                <br />
                &nbsp;<asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" />
                &nbsp;
            </td>
        </tr>
    </table>
    <div id="divMsg" runat="server">
    </div>

</asp:Content>
