<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="BackCourseRegistrationByAdmin.aspx.cs" Inherits="ACADEMIC_BackCourseRegistrationByAdmin"
    MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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

    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="vista_page_title_bar" style="height: 30px">PREVIOUS COURSE REGISTRATION BY ADMIN
                <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>
    </table>
    <div id="divOptions" runat="server" visible="false" style="padding: 8px 0px 8px 10px;">
        <div style="width: 100px; font-weight: bold; float: left;">Options :</div>
        <div style="width: 500px; font-weight: bold;">
            <asp:RadioButtonList ID="rblOptions" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                OnSelectedIndexChanged="rblOptions_SelectedIndexChanged" Font-Bold="true">
                <asp:ListItem Value="M" Selected="True" Text="Regular"></asp:ListItem>
                <asp:ListItem Value="S" Text="Arrear"></asp:ListItem>
            </asp:RadioButtonList>
        </div>
    </div>
    <div id="divCourses" runat="server" visible="false">
        <%--visible="false"--%>
        <fieldset class="fieldset">
            <legend class="legend">Previous Course Registration By Admin</legend>
            <table id="tblSession" runat="server" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="width: 15%; text-align: right">Session Name :
                    </td>
                    <td class="form_left_text">
                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" Width="30%">
                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 15%; text-align: right">Examination Roll No :
                    </td>
                    <td class="form_left_text">
                        <asp:TextBox ID="txtRollNo" runat="server" Width="150px" MaxLength="15" />
                        &nbsp;
                        <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" Text="Show"
                            Font-Bold="true" ValidationGroup="Show" Width="80px" />
                        &nbsp;
                        <asp:Button ID="btnCancel" runat="server" Text="Clear"
                            Font-Bold="true" ValidationGroup="Show" Width="80px"
                            OnClick="btnCancel_Click" />
                        &nbsp;
                        <asp:RequiredFieldValidator ID="rfvRollNo" ControlToValidate="txtRollNo" runat="server"
                            Display="None" ErrorMessage="Please enter Student Roll No." ValidationGroup="Show" />
                        <asp:ValidationSummary ID="valSummery2" runat="server" DisplayMode="List" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="Show" />
                    </td>
                </tr>
            </table>
            <table cellpadding="0" cellspacing="0" width="100%" id="tblInfo" runat="server" visible="false">
                <tr>
                    <td style="width: 15%; text-align: right; padding-top: 4px; padding-bottom: 4px;">Student Name :
                    </td>
                    <td class="form_left_text">
                        <asp:Label ID="lblName" runat="server" Font-Bold="True" />
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

                    <td style="width: 15%; text-align: right; padding-top: 4px; padding-bottom: 4px;">College Name :
                    </td>
                    <td class="form_left_text">
                        <asp:Label ID="lblCollege" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 15%; text-align: right; padding-top: 4px; padding-bottom: 4px;">Roll No. :
                    </td>
                    <td class="form_left_text">
                        <asp:Label ID="lblEnrollNo" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 15%; text-align: right; padding-top: 4px; padding-bottom: 4px;">Registration No.:</td>
                    <td class="form_left_text">
                        <asp:Label ID="lblRegNo" runat="server" Style="font-weight: 700"></asp:Label>
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
                    <td style="width: 15%; text-align: right; padding-top: 4px; padding-bottom: 4px;">Regulation :
                    </td>
                    <td class="form_left_text">
                        <asp:Label ID="lblScheme" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr style="display: none">
                    <td style="width: 15%; text-align: right; padding-top: 4px; padding-bottom: 4px;" valign="top">Total Subjects :
                    </td>
                    <td class="form_left_text">
                        <asp:TextBox ID="txtAllSubjects" runat="server" Enabled="false" Text="0" Width="10%"
                            Style="text-align: center;"></asp:TextBox>
                        &nbsp;&nbsp; Total Credits :
                        <asp:TextBox ID="txtCredits" runat="server" Enabled="false" Text="0" Width="10%"
                            Style="text-align: center;"></asp:TextBox>
                        <asp:HiddenField ID="hdfCredits" runat="server" Value="0" />
                        <asp:HiddenField ID="hdfTot" runat="server" Value="0" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="padding: 10px; text-align: center;">
                        <asp:ListView ID="lvCurrentSubjects" runat="server" Visible="false">
                            <LayoutTemplate>
                                <div class="vista-grid">
                                    <div class="titlebar">
                                        Current Semester Subjects
                                    </div>
                                    <table id="tblCurrentSubjects" cellpadding="0" cellspacing="0" class="datatable" style="width: 100%;">
                                        <thead>
                                            <tr class="header">
                                                <th>
                                                    <asp:CheckBox ID="cbHeadReg" runat="server" Text="Register" ToolTip="Register/Register all" onclick="SelectAll(this,1,'chkRegister');" />
                                                </th>
                                                <th>Course Code
                                                </th>
                                                <th width="45%">Course Name
                                                </th>
                                                <th width="10%">Semester
                                                </th>
                                                <th width="10%">Sub. Type
                                                </th>
                                                <th width="10%">Credits
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
                                        <asp:CheckBox ID="chkRegister" runat="server" Value='<%# Eval("EXAM_REGISTERED") %>' ToolTip="Click to select this subject for registration"
                                            onclick="ChkHeader(1,'cbHeadReg','chkRegister');" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                    </td>
                                    <td width="45%">
                                        <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' />
                                    </td>
                                    <td width="10%">
                                        <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("SEMESTER") %>' />
                                    </td>
                                    <td width="10%">
                                        <asp:Label ID="lblSub_Type" runat="server" Text='<%# Eval("SUBNAME") %>' ToolTip='<%# Eval("SUBID") %>' />
                                    </td>
                                    <td width="10%">
                                        <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>' />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="padding: 10px; text-align: center;">
                        <asp:ListView ID="lvBacklogSubjects" runat="server">
                            <LayoutTemplate>
                                <div class="vista-grid">
                                    <div class="titlebar">
                                        Arrear Subjects
                                    </div>
                                    <table id="tblBacklogSubjects" cellpadding="0" cellspacing="0" class="datatable" style="width: 100%;">
                                        <thead>
                                            <tr class="header">
                                                <th>
                                                    <asp:CheckBox ID="cbHeadReg" runat="server" Text="Register" ToolTip="Register/Register all" onclick="SelectAll(this,2,'chkRegister');" />
                                                </th>
                                                <th>Course Code
                                                </th>
                                                <th width="45%">Course Name
                                                </th>
                                                <th width="10%">Semester
                                                </th>
                                                <th width="10%">Subject Type
                                                </th>
                                                <th width="10%">Credits
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
                                        <asp:CheckBox ID="chkRegister" runat="server" Value='<%# Eval("EXAM_REGISTERED") %>' ToolTip="Click to select this subject for registration"
                                            onclick="ChkHeader(2,'cbHeadReg','chkRegister');" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                    </td>
                                    <td width="45%">
                                        <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSENAME") %>' />
                                    </td>
                                    <td width="10%">
                                        <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("SEMESTER") %>' />
                                    </td>
                                    <td width="10%">
                                        <asp:Label ID="lblSub_Type" runat="server" Text='<%# Eval("SUBNAME") %>' ToolTip='<%# Eval("SUBID") %>' />
                                    </td>
                                    <td width="10%">
                                        <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>' />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="padding: 10px; text-align: center;">
                        <asp:ListView ID="lvAuditSubjects" runat="server">
                            <LayoutTemplate>
                                <div class="vista-grid">
                                    <div class="titlebar">
                                        Audit Subjects
                                    </div>
                                    <table id="tblAuditSubjects" cellpadding="0" cellspacing="0" class="datatable" style="width: 100%;">
                                        <thead>
                                            <tr class="header">
                                                <th>
                                                    <asp:CheckBox ID="cbHeadReg" runat="server" Text="Register" ToolTip="Register/Register all" onclick="SelectAll(this,3,'chkRegister');" />
                                                </th>
                                                <th>Course Code
                                                </th>
                                                <th width="45%">Course Name
                                                </th>
                                                <th width="10%">Semester
                                                </th>
                                                <th width="10%">Subject Type
                                                </th>
                                                <th width="10%">Credits
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
                                        <asp:CheckBox ID="chkRegister" runat="server" Value='<%# Eval("EXAM_REGISTERED") %>' ToolTip="Click to select this subject for registration"
                                            onclick="ChkHeader(3,'cbHeadReg','chkRegister');" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                    </td>
                                    <td width="45%">
                                        <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' />
                                    </td>
                                    <td width="10%">
                                        <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("SEMESTER") %>' />
                                    </td>
                                    <td width="10%">
                                        <asp:Label ID="lblSub_Type" runat="server" Text='<%# Eval("SUBNAME") %>' ToolTip='<%# Eval("SUBID") %>' />
                                    </td>
                                    <td width="10%">
                                        <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>' />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="padding: 10px; text-align: center;">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" Width="80px" OnClick="btnSubmit_Click"
                            Enabled="false" ValidationGroup="SUBMIT" OnClientClick="return showConfirm();" />&nbsp;
                        <asp:Button ID="btnPrintRegSlip" runat="server" Text="Registration Slip"
                            OnClick="btnPrintRegSlip_Click" Enabled="false" />&nbsp;
                        <asp:ValidationSummary ID="vssubmit" runat="server" DisplayMode="List" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="SUBMIT" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
    <asp:HiddenField ID="hdfTotNoCourses" runat="server" Value="0" />

    <div id="divMsg" runat="server">
    </div>

    <script type="text/javascript" language="javascript">
        function SelectAll(headerid, headid, chk) {
            var tbl = '';
            var list = '';
            if (headid == 1) {
                tbl = document.getElementById('tblCurrentSubjects');
                list = 'lvCurrentSubjects';
            }
            else if (headid == 2) {
                tbl = document.getElementById('tblBacklogSubjects');
                list = 'lvBacklogSubjects';
            }
            else {
                tbl = document.getElementById('tblAuditSubjects');
                list = 'lvAuditSubjects';
            }

            try {
                var dataRows = tbl.getElementsByTagName('tr');
                if (dataRows != null) {
                    for (i = 0; i < dataRows.length - 1; i++) {
                        var chkid = 'ctl00_ContentPlaceHolder1_' + list + '_ctrl' + i + '_' + chk;
                        if (headerid.checked) {
                            document.getElementById(chkid).checked = true;
                        }
                        else {
                            document.getElementById(chkid).checked = false;
                        }
                        chkid = '';
                    }
                }
            }
            catch (e) {
                alert(e);
            }
        }

        function ChkHeader(chklst, head, chk) {
            try {
                var headid = '';
                var tbl = '';
                var list = '';
                var chkcnt = 0;
                if (chklst == 1) {
                    tbl = document.getElementById('tblCurrentSubjects');
                    headid = 'ctl00_ContentPlaceHolder1_lvCurrentSubjects_' + head;
                    list = 'lvCurrentSubjects';
                }
                else if (chklst == 2) {
                    tbl = document.getElementById('tblBacklogSubjects');
                    headid = 'ctl00_ContentPlaceHolder1_lvBacklogSubjects_' + head;
                    list = 'lvBacklogSubjects';
                }
                else {
                    tbl = document.getElementById('tblAuditSubjects');
                    headid = 'ctl00_ContentPlaceHolder1_lvAuditSubjects_' + head;
                    list = 'lvAuditSubjects';
                }

                var dataRows = tbl.getElementsByTagName('tr');
                if (dataRows != null) {
                    for (i = 0; i < dataRows.length - 1; i++) {
                        var chkid = document.getElementById('ctl00_ContentPlaceHolder1_' + list + '_ctrl' + i + '_' + chk);
                        if (chkid.checked)
                            chkcnt++;
                    }
                }
                if (chkcnt > 0)
                    document.getElementById(headid).checked = true;
                else
                    document.getElementById(headid).checked = false;
            }
            catch (e) {
                alert(e);
            }
        }
        function showConfirm() {
            var ret = confirm('Do you Really want to Confirm/Submit this Courses for Course Registration?');
            if (ret == true)
                return true;
            else
                return false;
        }
    </script>
</asp:Content>
