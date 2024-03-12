<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="StudentIDCard.aspx.cs" Inherits="Academic_StudentIDCard" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td>
                <table class="vista_page_title_bar" width="100%">
                    <tr>
                        <td style="height: 30px">
                            STUDENT IDENTITY CARD&nbsp;&nbsp
                            <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                                AlternateText="Page Help" ToolTip="Page Help" />
                        </td>
                    </tr>
                </table>
            </td>
            
        </tr>
        </table>
        <br />
        <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td>
                <div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF;
                    border: solid 1px #D0D0D0;">
                </div>
                <!-- Info panel to be displayed as a flyout when the button is clicked -->
                <div id="info" style="display: none; width: 250px; z-index: 2; font-size: 12px; border: solid 1px #CCCCCC;
                    background-color: #FFFFFF; padding: 5px;">
                    <div id="btnCloseParent" style="float: right;">
                        <asp:LinkButton ID="btnClose" runat="server" OnClientClick="return false;" Text="X"
                            ToolTip="Close" Style="background-color: #666666; color: #FFFFFF; text-align: center;
                            font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
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
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Label ID="lblMsg" runat="server" SkinID="Msglbl"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <fieldset class="fieldset">
                    <legend class="legend">Select Criteria</legend>
                    <table width="100%" cellpadding="2" cellspacing="2" border="0">
                        <tr>
                            <td width="20%">
                                Degree:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlDegree" runat="server" Width="190px" AppendDataBoundItems="True"
                                    ToolTip="Please Select Degree" AutoPostBack="True" 
                                    onselectedindexchanged="ddlDegree_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Branch:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlBranch" runat="server" Width="300px" AppendDataBoundItems="True"
                                    ToolTip="Please Select Branch">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Semester:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlSemester" runat="server" Width="190px" AppendDataBoundItems="True"
                                    ToolTip="Please Select Semester">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Button ID="btnShow" runat="server" Text="Show Student" Width="100px" OnClick="btnShow_Click"
                                    ToolTip="SHows Students under Selected Criteria." />
                                &nbsp;
                                <asp:Button ID="btnPrintReport" runat="server" Text="Print ID Card" 
                                    Width="100px" OnClick="btnPrintReport_Click"
                                    ToolTip="Show Front Side of ID Card" />
                                &nbsp;
                                <asp:Button ID="btnBackSide" runat="server" Text="Back Side" Width="88px" OnClick="btnBackSide_Click"
                                    ToolTip="Show Back Side of ID Card" Visible="False" />
                                &nbsp;
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                    Width="65px" ToolTip="Cancel Selected under Selected Criteria." />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td style="padding-top:10px">
                <fieldset class="fieldset">
                    <legend class="legend">Add Student for ID Card Printing</legend>
                    <br />
                    <div>
                        &nbsp;
                        Enter Enrollment No.&nbsp;&nbsp;
                        <asp:TextBox ID="txtSearchText" runat="server" Width="150px" 
                            ToolTip="Enter Enrollment No."></asp:TextBox>
                        &nbsp;
                        <asp:Button ID="btnAdd" runat="server" Text="Add Enrollment to List" 
                            OnClick="btnAdd_Click" ValidationGroup="academic"
                            Width="150px" ToolTip="Add Enrollment No. into List." />
                        &nbsp;
                        <asp:RequiredFieldValidator ID="rfvSearchText" runat="server" ControlToValidate="txtSearchText"
                            ErrorMessage="Please Enter Enrollment No." ValidationGroup="academic" SetFocusOnError="true"
                            Display="None">
                        </asp:RequiredFieldValidator>
                        <asp:Label ID="lblEnrollmentNo" runat="server" SkinID="Msglbl">
                        </asp:Label>
                    </div>
                </fieldset>
                <table width="100%" cellpadding="2" cellspacing="2" border="0">
                    <tr>
                        <td colspan="2">
                            <asp:ListView ID="lvStudentRecords" runat="server">
                                <LayoutTemplate>
                                    <div id="listViewGrid" class="vista-grid">
                                        <div class="titlebar">
                                            Search Results
                                        </div>
                                        <table class="datatable" cellpadding="0" cellspacing="0">
                                            <tr class="header">
                                                <th width="5%">
                                                    <asp:CheckBox ID="chkIdentityCard" runat="server" onClick="SelectAll(this);" ToolTip="Select or Deselect All Records" />
                                                </th>
                                                <th width="35%">
                                                    Student Name
                                                </th>
                                                <th width="10%">
                                                    Enroll. No.
                                                </th>
                                                <th width="10%">
                                                    Degree
                                                </th>
                                                <th width="10%">
                                                    Branch
                                                </th>
                                                <th width="10%">
                                                    Year
                                                </th>
                                                <th width="10%">
                                                    Semester
                                                </th>
                                                <th width="10%">
                                                    Batch
                                                </th>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="listview-container">
                                        <div id="demo-grid" class="vista-grid">
                                            <table id="tblStudentRecords" class="datatable" cellpadding="0" cellspacing="0" style="width: 100%;">
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </LayoutTemplate>
                                <EmptyDataTemplate>
                                </EmptyDataTemplate>
                                <ItemTemplate>
                                    <tr class="item" >
                                        <td width="5%">
                                            <asp:CheckBox ID="chkReport" runat="server" />
                                            <asp:HiddenField ID="hidIdNo" runat="server" Value='<%# Eval("IDNO") %>' />
                                        </td>
                                        <td width="35%">
                                            <%# Eval("NAME")%>
                                        </td>
                                        <td width="10%">
                                            <%# Eval("ENROLLMENTNO")%>
                                        </td>
                                        <td width="10%">
                                            <%# Eval("CODE")%>
                                        </td>
                                        <td width="10%">
                                            <%# Eval("SHORTNAME")%>
                                        </td>
                                        <td width="10%">
                                            <%# Eval("YEARNAME")%>
                                        </td>
                                        <td width="10%">
                                            <%# Eval("SEMESTERNAME")%>
                                        </td>
                                        <td width="10%">
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
                <div id="divMsg" runat="server">
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                    ShowSummary="False" ValidationGroup="academic" />
            </td>
        </tr>
    </table>

    <script type="text/javascript">
    function SelectAll(chk)
    {
      var tbl = document.getElementById("tblStudentRecords");
      if(tbl != null && tbl.childNodes != null)
      {
        for(i=0; i < tbl.getElementsByTagName("tr").length; i++)
        {
            document.getElementById('ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl'+i+'_chkReport').checked = chk.checked;
        }        
      }
    }
    </script>

</asp:Content>
