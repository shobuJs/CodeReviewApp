<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Generate_RollNo.aspx.cs" Inherits="ACADEMIC_Generate_RollNo" Title="" %>

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
    </script>

    <script src="../Content/jquery.js" type="text/javascript"></script>

    <script src="../Content/jquery.dataTables.js" language="javascript" type="text/javascript"></script>

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

    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <%# Eval("ROLLNO")%>
        <tr>
            <td class="vista_page_title_bar" valign="top" style="height: 30px">CLASS ROLL NUMBER GENERATION
                <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>
        <%# Eval("STUDNAME")%>
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
    <div style="color: Red; font-weight: bold">
        &nbsp;Note : * marked fields are Mandatory
    </div>
    <fieldset class="fieldset">
        <legend class="legend">Generate Roll Number</legend>
        <asp:UpdatePanel ID="updPnl" runat="server">
            <ContentTemplate>
                <table width="100%" cellpadding="2" cellspacing="2" border="0">
                    <tr>
                        <td width="20%">
                            <span class="validstar">*</span>Degree :
                        </td>
                        <td width="80%">
                            <asp:DropDownList ID="ddlDegree" runat="server" TabIndex="1" Width="250px" AppendDataBoundItems="True"
                                OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged"
                                AutoPostBack="True" ToolTip="Please Select Degree">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvDegree" runat="server" InitialValue="0" SetFocusOnError="true"
                                ControlToValidate="ddlDegree" Display="None" ErrorMessage="Please Select Degree."
                                ValidationGroup="submit"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">
                            <span class="validstar">*</span>Branch :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlBranch" runat="server" TabIndex="2" Width="250px" AppendDataBoundItems="True"
                                OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"
                                AutoPostBack="True" ToolTip="Please Select Branch">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvBranch" runat="server" InitialValue="0" SetFocusOnError="true"
                                ControlToValidate="ddlBranch" Display="None" ErrorMessage="Please Select Branch"
                                ValidationGroup="submit"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">
                            <span class="validstar">*</span>Semester :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSemester" runat="server" TabIndex="3" Width="150px" AppendDataBoundItems="True"
                                OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged"
                                AutoPostBack="True" ToolTip="Please Select Semester">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvSemester" runat="server" InitialValue="0" SetFocusOnError="true"
                                ControlToValidate="ddlSemester" Display="None" ErrorMessage="Please Select Semester."
                                ValidationGroup="submit"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">
                            <span class="validstar">*</span>Section :
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSection" runat="server" TabIndex="4" Width="150px" AppendDataBoundItems="True"
                                AutoPostBack="True"
                                OnSelectedIndexChanged="ddlSection_SelectedIndexChanged"
                                ToolTip="Please Select Section">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvSection" runat="server" InitialValue="0" SetFocusOnError="true"
                                ControlToValidate="ddlSection" Display="None" ErrorMessage="Please Select Section."
                                ValidationGroup="submit"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">
                            <span class="validstar">&nbsp</span>Gender :
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rdoSex" runat="server" RepeatDirection="Horizontal" TabIndex="5"
                                OnSelectedIndexChanged="rdoSex_SelectedIndexChanged" AutoPostBack="True"
                                ToolTip="Please Select Gender">
                                <asp:ListItem Value="M">Male</asp:ListItem>
                                <asp:ListItem Selected="True" Value="F">Female</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">
                            <span class="validstar">*</span>Roll No. From :
                        </td>
                        <td>
                            <asp:TextBox ID="txtFrmRollno" runat="server" TabIndex="6" onkeyup="validateNumeric(this);"
                                Style="text-align: center;" Width="5%" MaxLength="3"></asp:TextBox>
                            &nbsp;<asp:RequiredFieldValidator ID="rfvFrmRollno" runat="server" SetFocusOnError="true"
                                ControlToValidate="txtFrmRollno" Display="None" ErrorMessage="Please Enter Roll No. From"
                                ValidationGroup="submit"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">
                            <span class="validstar">*</span>Roll No. To :
                        </td>
                        <td>
                            <asp:TextBox ID="txtToRollno" runat="server" Width="5%" TabIndex="7" MaxLength="3"
                                Style="text-align: center;" onkeyup="validateNumeric(this);"
                                ToolTip="Please Enter Roll No." />
                            &nbsp;<asp:RequiredFieldValidator ID="rfvToRollno" runat="server" SetFocusOnError="true"
                                ControlToValidate="txtToRollno" Display="None" ErrorMessage="Please Enter Roll No. To"
                                ValidationGroup="submit"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="cvRoll" runat="server" ControlToCompare="txtFrmRollno"
                                ControlToValidate="txtToRollno" Display="None" ErrorMessage="RollNo. From can not be grater than RollNo. To"
                                Operator="GreaterThan" Type="Integer" ValidationGroup="submit"></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">
                            <asp:ValidationSummary ID="vsGenRoll" runat="server" DisplayMode="List" ShowMessageBox="true"
                                ShowSummary="false" ValidationGroup="submit" Width="143px" />
                        </td>
                        <td>
                            <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click"
                                Text="Show Students" TabIndex="8" />
                            &nbsp;
                            <asp:Button ID="btnGenerate" runat="server" Text="Generate" TabIndex="9" ValidationGroup="submit"
                                Font-Bold="True" OnClick="btnGenerate_Click" />
                            &nbsp;
                            <asp:Button ID="btnReport" runat="server" Text="Report" TabIndex="10" CausesValidation="False"
                                OnClick="btnReport_Click" Visible="False" />
                            &nbsp;
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="11" CausesValidation="False"
                                OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td width="20%">&nbsp;
                        </td>
                        <td>&nbsp;
                            <asp:Label ID="lblCount" runat="server" Style="color: Red; font-weight: bold;"></asp:Label>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" width="20%">
                            <asp:Panel ID="pnlStud" runat="server" Visible="false">
                                <fieldset class="fieldset">
                                    <legend class="legend">Student List</legend>
                                    <table cellpadding="1" cellspacing="1" width="100%">
                                        <tr>
                                            <td>
                                                <asp:Repeater ID="lvStudents" runat="server">
                                                    <HeaderTemplate>
                                                        <div id="listViewGrid" class="vista-grid" style="width: 100%;">
                                                            <div class="titlebar">
                                                                List of Students
                                                            </div>
                                                        </div>
                                                        <table id="tblSearchResults" cellpadding="0" cellspacing="0" class="display" style="width: 100%;">
                                                            <thead>
                                                                <tr class="header">
                                                                    <th style="width: 10%; text-align: center;">Roll No.
                                                                    </th>
                                                                    <th style="width: 15%; text-align: center;">Enrollment No.
                                                                    </th>
                                                                    <th style="width: 30%;">Name
                                                                    </th>
                                                                    <th style="width: 20%;">Branch
                                                                    </th>
                                                                    <th style="width: 15%; text-align: center;">Semester
                                                                    </th>
                                                                    <th style="width: 15%; text-align: center;">Section
                                                                    </th>
                                                                </tr>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                                <thead>
                                                            <tbody>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr class="item">
                                                            <td style="width: 10%; text-align: center;">
                                                                <%# Eval("ROLLNO")%>
                                                            </td>
                                                            <td style="width: 15%; text-align: center;">
                                                                <%# Eval("ENROLLMENTNO")%>
                                                            </td>
                                                            <td style="width: 30%;">
                                                                <%# Eval("STUDNAME")%>
                                                            </td>
                                                            <td align="center">
                                                                <%# Eval("BRANCH")%>
                                                            </td>
                                                            <td style="width: 15; text-align: center;">
                                                                <%# Eval("SEMESTER")%>
                                                            </td>
                                                            <td style="width: 15%; text-align: center;">
                                                                <%# Eval("SECITON")%>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </tbody></table>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </asp:Panel>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </fieldset>
    <div id="divMsg" runat="server">
    </div>

    <script type="text/javascript" language="javascript">

        function validateNumeric(txt) {
            if (isNaN(txt.value)) {
                txt.value = '';
                alert('Only Numeric Characters Allowed!');
                txt.focus();
                return;
            }
        }
    </script>

</asp:Content>
