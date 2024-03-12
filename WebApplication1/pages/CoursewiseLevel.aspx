<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="CoursewiseLevel.aspx.cs" Inherits="ACADEMIC_CoursewiseLevel" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <%--Disable the button so it can't be clicked again --%>
        <tr>
            <td class="vista_page_title_bar" valign="top" style="height: 30px" colspan="2">LEVEL CREATION / UPDATION &nbsp;
                 <%-- Position the wire frame on top of the button and show it --%>
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>
        <%-- Move the info panel on top of the wire frame, fade it in, and hide the frame --%>
        <tr>
            <td colspan="2">
                <%--Flash the text/border red and fade in the "close" button --%>
                <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF; border: solid 1px #D0D0D0;">
                </div>
                <%--Shrink the info panel out of view --%>
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
                             <%--Disable the button so it can't be clicked again --%>
                            <EnableAction Enabled="false" />
                            
                            <%-- Position the wire frame on top of the button and show it --%>
                            <ScriptAction Script="Cover($get('ctl00$ContentPlaceHolder1$btnHelp'), $get('flyout'));" />
                            <StyleAction AnimationTarget="flyout" Attribute="display" Value="block"/>
                            
                            <%-- Move the info panel on top of the wire frame, fade it in, and hide the frame --%>
                            <ScriptAction Script="Cover($get('flyout'), $get('info'), true);" />
                            <StyleAction AnimationTarget="info" Attribute="display" Value="block"/>
                            <FadeIn AnimationTarget="info" Duration=".2"/>
                            <StyleAction AnimationTarget="flyout" Attribute="display" Value="none"/>
                            
                             <%--Flash the text/border red and fade in the "close" button --%>
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
                              <%--Shrink the info panel out of view --%>
                            <StyleAction Attribute="overflow" Value="hidden"/>
                            <Parallel Duration=".3" Fps="15">
                                <Scale ScaleFactor="0.05" Center="true" ScaleFont="true" FontUnit="px" />
                                <FadeOut />
                            </Parallel>
                            
                              <%--Reset the sample so it can be played again --%>
                            <StyleAction Attribute="display" Value="none"/>
                            <StyleAction Attribute="width" Value="250px"/>
                            <StyleAction Attribute="height" Value=""/>
                            <StyleAction Attribute="fontSize" Value="12px"/>
                            <OpacityAction AnimationTarget="btnCloseParent" Opacity="0" />
                            
                            <%-- Enable the button so it can be played again --%>
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
    <div style="padding-left: 10px;">
        <asp:UpdatePanel ID="pnlLevel" runat="server">
            <ContentTemplate>
                <%-- Add/Edit and Search Section --%>
                <table cellpadding="0" cellspacing="0" width="80%">
                    <tr>
                        <td>
                            <fieldset class="fieldset">
                                <legend class="legend">Level Creation</legend>
                                <table cellpadding="2" cellspacing="2" width="100%">
                                    <tr>
                                        <td class="form_left_label" style="width: 134px">Term :
                                        </td>
                                        <td class="form_left_text">
                                            <asp:Label ID="lblSession" runat="server" Font-Bold="true" Visible="false" />
                                            <asp:DropDownList ID="ddlTerm" runat="server" Width="100px"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvTerm" runat="server" ControlToValidate="ddlTerm"
                                                Display="None" ErrorMessage="Please Select Term" InitialValue="0" ValidationGroup="Submit" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label" style="width: 134px">Level Name :
                                        </td>
                                        <td class="form_left_text">
                                            <asp:TextBox ID="txtLevel" runat="server" Width="225px" TabIndex="1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rvfLevelName" runat="server"
                                                ControlToValidate="txtLevel" Display="None"
                                                ErrorMessage="Enter  Level Name" SetFocusOnError="True"
                                                ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label" style="width: 134px">Admission Batch:
                                        </td>
                                        <td class="form_left_text">
                                            <asp:TextBox ID="txtAdmBatch" runat="server" TabIndex="2" Width="75px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                ControlToValidate="txtAdmBatch" Display="None"
                                                ErrorMessage="Enter  Admission Batch" SetFocusOnError="True"
                                                ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="CvAdmBatch" runat="server"
                                                ControlToValidate="txtAdmBatch" Display="None"
                                                ErrorMessage="Admission Batch should be in  Integers only"
                                                Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer"
                                                ValidationGroup="Submit"></asp:CompareValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label" style="width: 134px">No Of Cousrses:
                                        </td>
                                        <td class="form_left_text">
                                            <asp:TextBox ID="txtNoOfCourses" runat="server" TabIndex="3" Width="75px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rvfNoOfCourses" runat="server"
                                                ControlToValidate="txtNoOfCourses" Display="None"
                                                ErrorMessage="Enter No of Courses" SetFocusOnError="True"
                                                ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                            &nbsp;<asp:CompareValidator ID="CvNoOfCourses" runat="server"
                                                ControlToValidate="txtNoOfCourses" Display="None"
                                                ErrorMessage="Number of Courses should be in  Integers only"
                                                Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer"
                                                ValidationGroup="Submit"></asp:CompareValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label" style="width: 134px">Total Theory Sub :
                                        </td>
                                        <td class="form_left_text">
                                            <asp:TextBox ID="txtTheory" runat="server" TabIndex="4" Width="75px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvTotalTheory" runat="server"
                                                ControlToValidate="txtTheory" Display="None"
                                                ErrorMessage="Enter Total Theory Subjects" SetFocusOnError="True"
                                                ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                            &nbsp;<asp:CompareValidator ID="CvTotalTheory" runat="server"
                                                ControlToValidate="txtTheory" Display="None"
                                                ErrorMessage="Total Theory Subjects should be in  Integers only"
                                                Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer"
                                                ValidationGroup="Submit"></asp:CompareValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label" style="height: 31px; width: 134px;">Total Practicals :&nbsp;
                                            <asp:Label ID="lblDivision" runat="server" Font-Bold="True" Font-Size="Small"
                                                Text="" />
                                        </td>
                                        <td>&nbsp;<asp:TextBox ID="txtPractical" runat="server" TabIndex="5" Width="75px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvTotalPracticals" runat="server"
                                                ControlToValidate="txtPractical" Display="None"
                                                ErrorMessage="Enter Total Practical Subjects" SetFocusOnError="True"
                                                ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="CvPracticals" runat="server"
                                                ControlToValidate="txtPractical" Display="None"
                                                ErrorMessage="Total Practicals should be in Integers Only"
                                                Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer"
                                                ValidationGroup="Submit"></asp:CompareValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 134px">Total Theory Marks:
                                        </td>
                                        <td>&nbsp;<asp:TextBox ID="txtThMarks" runat="server" MaxLength="3" TabIndex="6"
                                            Width="75px"></asp:TextBox>
                                            <asp:CompareValidator ID="CvTotThmarks" runat="server"
                                                ControlToValidate="txtThMarks" Display="None"
                                                ErrorMessage="Total theory Marks should be in integers only"
                                                Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer"
                                                ValidationGroup="Submit"></asp:CompareValidator>
                                            &nbsp;<asp:RequiredFieldValidator ID="rfvTotThMarks" runat="server"
                                                ControlToValidate="txtThMarks" Display="None"
                                                ErrorMessage="Enter Total Theory Marks" SetFocusOnError="True"
                                                ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="form_left_label" style="width: 134px">Total Practical Marks:
                                        </td>
                                        <td class="form_left_text">

                                            <asp:TextBox ID="txtPrMarks" runat="server" MaxLength="3" TabIndex="7"
                                                Width="75px"></asp:TextBox>

                                            <asp:CompareValidator ID="CvTotPracMarks" runat="server"
                                                ControlToValidate="txtPrMarks" Display="None"
                                                ErrorMessage="Total Practical marks should be in integers  only"
                                                Operator="DataTypeCheck" SetFocusOnError="True" Type="Integer"
                                                ValidationGroup="Submit"></asp:CompareValidator>
                                            <asp:RequiredFieldValidator ID="rfvTotPractmarks" runat="server"
                                                ControlToValidate="txtPrMarks" Display="None"
                                                ErrorMessage="Enter Total Practical Marks " SetFocusOnError="True"
                                                ValidationGroup="Submit"></asp:RequiredFieldValidator>

                                            <td class="form_left_text" valign="middle">&nbsp;</td>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 134px">&nbsp;
                                        </td>
                                        <td colspan="1">
                                            <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                ShowSummary="false" ValidationGroup="Submit" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>

                    </tr>
                    <tr>
                        <td style="height: 15px;">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" TabIndex="8" ValidationGroup="Submit"
                                OnClick="btnSubmit_Click" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                TabIndex="9" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                </table>
                <div style="height: 15px;">
                </div>
                <div style="width: 83%;">
                    <asp:ListView ID="lvLevelCreation" runat="server">
                        <EmptyDataTemplate>
                            <br />
                            <center>
                         <asp:Label ID="lblerr" SkinID="Errorlbl" runat="server" Text="No Records Found" /></center>
                        </EmptyDataTemplate>
                        <LayoutTemplate>
                            <div class="vista-grid">
                                <div class="titlebar">
                                    Level Creation List
                                </div>
                                <table class="datatable" cellpadding="0" cellspacing="0" width="100%">
                                    <thead>
                                        <tr class="header">
                                            <th width="10%">Action
                                            </th>
                                            <th width="25%">Level Name
                                            </th>
                                            <th width="20%">Admission Batch
                                            </th>
                                            <th width="20%">No. Of Courses
                                            </th>
                                            <th width="15%">Theory
                                            </th>
                                            <th width="15%">Practical
                                            </th>

                                        </tr>

                                        <thead>
                                </table>
                            </div>
                            <div class="listview-container">
                                <div id="demo-grid" class="vista-grid">
                                    <table class="datatable" cellpadding="0" cellspacing="0" style="width: 100%;">
                                        <tbody>
                                            <tr id="itemPlaceholder" runat="server" />
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                <td width="10%">
                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif"
                                        CommandArgument='<%# Eval("LEVELNO") %>' AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                </td>
                                <td width="30%">
                                    <%#Eval("LEVEL_DESC")%>
                                </td>
                                <td width="20%">
                                    <%#Eval("ADMBATCH")%>
                                </td>
                                <td width="16%">
                                    <%#Eval("NO_COURSES")%>
                                </td>
                                <td width="15%">
                                    <%#Eval("CP_TH")%>
                                </td>
                                <td width="15%">
                                    <%#Eval("CP_PR")%>
                                </td>

                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr class="altitem" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFD2'">
                                <td width="10%">
                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif"
                                        CommandArgument='<%# Eval("LEVELNO") %>' AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                </td>
                                <td width="30%">
                                    <%#Eval("LEVEL_DESC")%>
                                </td>
                                <td width="20%">
                                    <%#Eval("ADMBATCH")%>
                                </td>
                                <td width="16%">
                                    <%#Eval("NO_COURSES")%>
                                </td>
                                <td width="15%">
                                    <%#Eval("CP_TH")%>
                                </td>
                                <td width="15%">
                                    <%#Eval("CP_PR")%>
                                </td>

                            </tr>
                        </AlternatingItemTemplate>
                    </asp:ListView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

</asp:Content>

