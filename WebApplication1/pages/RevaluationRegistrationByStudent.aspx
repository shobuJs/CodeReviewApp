<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="RevaluationRegistrationByStudent.aspx.cs" Inherits="ACADEMIC_RevaluationRegistrationByStudent" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnlStart" runat="server">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="vista_page_title_bar" style="height: 30px">REVALUATION REGISTRATION
                    <!-- Button used to launch the help (animation) -->
                    <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                        AlternateText="Page Help" ToolTip="Page Help" />
                </td>
            </tr>
        </table>
        <br />
        <div id="divNote" runat="server" visible="true" style="border: 2px solid #C0C0C0; background-color: #FFFFCC; padding: 20px; color: #990000;">
            <b>Note : </b>Steps to follow for Revaluation Registration.
            <div style="padding-left: 20px; padding-right: 20px">
                <p style="padding-top: 5px; padding-bottom: 5px;">
                    1. Please click Proceed to revaluation Registration button.
                </p>
                <p style="padding-top: 5px; padding-bottom: 5px;">
                    2. Revaluation process student can be apply for only two subject's in every semester.
                </p>
                <p style="padding-top: 5px; padding-bottom: 5px;">
                    3. Only theory paper will be consider in revaluation process.
                </p>
                <p style="padding-top: 5px; padding-bottom: 5px;">
                    4. Revaluation fees per subject is Rs. 200/-
                </p>
                <p style="padding-top: 5px; padding-bottom: 5px;">
                    5. Finally Click the Submit Button.
                </p>
                <p style="padding-top: 5px; padding-bottom: 5px; text-align: center;">
                    <asp:Button ID="btnProceed" runat="server" Text="Proceed to Revaluation Registration" OnClick="btnProceed_Click" />
                </p>
            </div>
        </div>
        <div id="divStud" runat="server" visible="false">
            <fieldset class="fieldset">
                <legend class="legend">Revaluation Registration</legend>
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr style="display: none">
                        <td style="width: 15%; text-align: right">Session Name :
                        </td>
                        <td class="form_left_text">
                            <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" Width="50%">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%; text-align: right">Roll No :
                        </td>
                        <td class="form_left_text">
                            <asp:TextBox ID="txtRollNo" runat="server" Width="150px" />
                            &nbsp;<asp:Button ID="btnShowstud" runat="server" Text="Show"
                                Font-Bold="true" ValidationGroup="Show" Width="80px"
                                OnClick="btnShowstud_Click" />
                            &nbsp<asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                Font-Bold="true" ValidationGroup="Show" Width="80px" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
        <br />
        <div id="divCourses" runat="server" visible="false">
            <fieldset class="fieldset">
                <legend class="legend">Student Information</legend>
                <table cellpadding="0" cellspacing="0" width="100%" id="tblInfo" runat="server">
                    <tr>
                        <td style="width: 15%; text-align: right; padding-top: 4px; padding-bottom: 4px;">Student Name :
                        </td>
                        <td class="form_left_text">
                            <asp:Label ID="lblName" runat="server" Font-Bold="True" />
                        </td>
                        <td rowspan="9" width="20%">

                            <asp:Image ID="imgPhoto" runat="server" Width="70%" Height="100%" />

                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%; text-align: right; padding-top: 4px; padding-bottom: 4px;">&nbsp;
                        </td>
                        <td class="form_left_text">
                            <asp:Label ID="lblFatherName" runat="server" Font-Bold="False" />&nbsp;
                            <asp:Label ID="lblMotherName" runat="server" Font-Bold="False" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%; text-align: right; padding-top: 4px; padding-bottom: 4px;">Enrollment No. :
                        </td>
                        <td class="form_left_text">
                            <asp:Label ID="lblEnrollNo" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%; text-align: right; padding-top: 4px; padding-bottom: 4px;">Admission Batch :
                        </td>
                        <td class="form_left_text">
                            <asp:Label ID="lblAdmBatch" runat="server" Font-Bold="True"></asp:Label>
                            &nbsp;&nbsp; Semester :
                            <asp:Label ID="lblSemester" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%; text-align: right; padding-top: 4px; padding-bottom: 4px;">Degree / Branch :
                        </td>
                        <td class="form_left_text">
                            <asp:Label ID="lblBranch" runat="server" Font-Bold="True"></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; PH :
                            <asp:Label ID="lblPH" runat="server" Style="font-weight: 700"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%; text-align: right; padding-top: 4px; padding-bottom: 4px;">Scheme :
                        </td>
                        <td class="form_left_text">
                            <asp:Label ID="lblScheme" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%; text-align: right; padding-top: 4px; padding-bottom: 4px;">Arrear Semester :</td>
                        <td class="form_left_text">
                            <asp:DropDownList ID="ddlBackLogSem" runat="server" Width="20%"
                                AppendDataBoundItems="True"
                                ValidationGroup="backsem">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvddlBackLogSem" runat="server" ControlToValidate="ddlBackLogSem"
                                Display="None" InitialValue="0" ErrorMessage="Please Select Semester" ValidationGroup="backsem"></asp:RequiredFieldValidator>

                            <asp:HiddenField ID="hdfCategory" runat="server" />

                            <asp:HiddenField ID="hdfDegreeno" runat="server" />

                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%; text-align: right; padding-top: 4px; padding-bottom: 4px;">&nbsp;</td>
                        <td valign="bottom">&nbsp;<asp:Button ID="btnShow" runat="server"
                            OnClick="btnShow_Click1" Text="Show" ValidationGroup="backsem" Width="15%" />
                            &nbsp;<asp:Button ID="bntCancel" runat="server" OnClick="btnCancel_Click1" Text="Back"
                                Width="80px" />
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="backsem"
                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="3" style="padding: 10px; text-align: center;">
                            <table width="100%" cellpadding="1" cellspacing="1">
                                <tr>
                                    <td valign="top">
                                        <asp:ListView ID="lvFailCourse" runat="server">
                                            <LayoutTemplate>
                                                <div class="vista-grid">
                                                    <div class="titlebar">
                                                        Revaluation Course List
                                                    </div>
                                                    <table cellpadding="0" cellspacing="0" class="datatable" style="width: 100%;">
                                                        <thead>
                                                            <tr class="header">
                                                                <th>Select
                                                                </th>
                                                                <th>Course Code
                                                                </th>
                                                                <th>Course Name
                                                                </th>
                                                                <th>Semester
                                                                </th>
                                                                <th>Mark Obtain
                                                                </th>
                                                            </tr>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </thead>
                                                    </table>
                                                </div>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr id="trCurRow" class="item">
                                                    <td>
                                                        <asp:CheckBox ID="chkAccept" runat="server" ToolTip='<%# Eval("COURSENO")%>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblSEMSCHNO" runat="server" Text='<%# Eval("SEMESTER") %>' ToolTip='<%# Eval("SCHEMENO") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblExtermark" runat="server" Text='<%# Eval("EXTERMARK") %>' />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>

                                        </asp:ListView>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>

                    <tr>
                        <td colspan="3" style="padding: 10px; text-align: center;">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="3" style="padding: 10px; text-align: center;">
                            <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" Font-Bold="true"
                                Width="80px" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="padding: 10px; text-align: center;">&nbsp;
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
        <asp:HiddenField ID="hdfTotNoCourses" runat="server" Value="0" />
        <div id="divMsg" runat="server">
        </div>
    </asp:Panel>

    <script type="text/javascript" language="javascript">

        function SelectAll(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (headchk.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }


        function CheckSelectionCount(chk) {
            var count = -1;
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (count == 2) {
                    chk.checked = false;
                    alert("You have reached maximum limit!");
                    return;
                }
                else if (count < 2) {
                    if (e.checked == true) {
                        count += 1;
                    }
                }
                else {
                    return;
                }
            }
        }

    </script>
</asp:Content>

