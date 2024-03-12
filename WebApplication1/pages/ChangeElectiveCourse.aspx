<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="ChangeElectiveCourse.aspx.cs" Inherits="ACADEMIC_CourseRegistration"
    Title="" MaintainScrollPositionOnPostback="true" %>

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
            <td class="vista_page_title_bar" style="height: 30px">CHANGE ELECTIVE COURSE
                <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>
    </table>
    <div id="divCourses" runat="server" visible="false">
        <asp:Panel ID="pnlMain" runat="server">
            <fieldset class="fieldset">
                <legend class="legend">Change Elective Course</legend>
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
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
                            &nbsp;<asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" Text="Show"
                                Font-Bold="true" ValidationGroup="Show" Width="80px" />
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
                        <td style="width: 25%; text-align: left; padding-top: 4px; padding-bottom: 4px; vertical-align: top;"
                            rowspan="9"></td>
                    </tr>
                    <tr>
                        <td style="width: 15%; text-align: right; padding-top: 4px; padding-bottom: 4px;">&nbsp;
                        </td>
                        <td class="form_left_text">
                            <asp:Label ID="lblFatherName" runat="server" Font-Bold="False" />
                            &nbsp;
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
                        <td style="width: 15%; text-align: right; padding-top: 4px; padding-bottom: 4px;">Degree/Branch :
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
                        <td style="width: 15%; text-align: right; padding-top: 4px; padding-bottom: 4px;">Alloted Groups:</td>

                        <td class="form_left_text">
                            <asp:DropDownList ID="ddlElectiveCourse" runat="server"
                                AppendDataBoundItems="True" Width="75%" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlElectiveCourse_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%; text-align: right; padding-top: 4px; padding-bottom: 4px;">Alloted Elective Courses :</td>

                        <td class="form_left_text">
                            <asp:DropDownList ID="ddlOfferedCourse" runat="server"
                                AppendDataBoundItems="True" Width="75%" AutoPostBack="True">
                                <asp:ListItem Value="0"> Please Select</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="trDeptCourse" runat="server" visible="true">
                        <td style="width: 15%; text-align: right; padding-top: 4px; padding-bottom: 4px;">Changed Elective Subject :</td>
                        <td class="form_left_text">
                            <asp:DropDownList ID="ddlSelectCourse" runat="server"
                                AppendDataBoundItems="True" Width="75%" AutoPostBack="True">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvElectiveCourse" runat="server"
                                ErrorMessage="Please Select Course" ControlToValidate="ddlSelectCourse"
                                Display="None" InitialValue="0" SetFocusOnError="True" ValidationGroup="Submit"></asp:RequiredFieldValidator>
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
                        <td colspan="3" style="padding: 10px; text-align: center;">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="3" style="padding: 10px; text-align: center;">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" Width="80px" OnClick="btnSubmit_Click"
                                Enabled="true" ValidationGroup="Submit" />
                            &nbsp; &nbsp; &nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                OnClick="btnCancel_Click" />
                            &nbsp;&nbsp;
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Submit" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="padding: 10px; text-align: center;">
                            <asp:Repeater ID="lvHistory" runat="server">
                                <HeaderTemplate>
                                    <table cellpadding="0" cellspacing="0" class="display" style="width: 100%;">
                                        <thead>
                                            <tr class="header">
                                                <th style="width: 10%">Session
                                                </th>
                                                <th style="width: 10%">Semester
                                                </th>
                                                <th style="width: 60%; text-align: left;">Course Code & Name
                                                </th>

                                            </tr>
                                            <tr id="itemPlaceholder" runat="server" />
                                            <thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td style="width: 10%">
                                            <asp:LinkButton ID="lbReport" runat="server"><%# Eval("SESSION_NAME") %></asp:LinkButton>
                                        </td>
                                        <td style="width: 10%">
                                            <asp:Label ID="lblSem" runat="server" Text='<%# Eval("SEMESTERNAME") %>' />
                                        </td>
                                        <td style="width: 60%; text-align: left;">
                                            <asp:Label ID="lblCourse" runat="server" Text='<%# Eval("CCODE") %>' />
                                            - 
                                                <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSENAME") %>' />
                                            <asp:HiddenField ID="hdfSession" runat="server" Value='<%# Eval("SESSIONNO") %>' />
                                            <asp:HiddenField ID="hdfIDNo" runat="server" Value='<%# Eval("IDNO") %>' />
                                            <asp:HiddenField ID="hdfScheme" runat="server" Value='<%# Eval("SCHEMENO") %>' />
                                            <asp:HiddenField ID="hdfSemester" runat="server" Value='<%# Eval("SEMESTERNO") %>' />
                                        </td>

                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </tbody>
                                        </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
    </div>
    <asp:HiddenField ID="hdfTotNoCourses" runat="server" Value="0" />
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
        function totSubjects(chk) {
        }

        function totSubjects(chk) {
            var txtTot = document.getElementById('<%= txtAllSubjects.ClientID %>');

            if (chk.checked == true)
                txtTot.value = Number(txtTot.value) + 1;
            else
                txtTot.value = Number(txtTot.value) - 1;
        }

        function SelectAll(headchk) {
            var txtTot = document.getElementById('<%= txtAllSubjects.ClientID %>');
            var txtCredits = document.getElementById('<%= txtCredits.ClientID %>');
            var hdfTot = document.getElementById('<%= hdfTot.ClientID %>');
            var hdfCredits = document.getElementById('<%= hdfCredits.ClientID %>');

            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (headchk.checked == true) {
                        e.checked = true;
                    }
                    else {
                        e.checked = false;
                    }
                }
            }

            if (headchk.checked == true) {
                txtTot.value = hdfTot.value;
                txtCredits.value = hdfCredits.value;
            }
            else {
                txtTot.value = 0;
                txtCredits.value = 0;
            }
        }




        function validateAssign() {
            var txtTot = document.getElementById('<%= hdfTot.ClientID %>').value;

            if (txtTot == 0) {
                alert('Please Select atleast one course selected in course list');
                return false;
            }
            else
                return true;
        }


        function MutualExclusive(radio) {
            var dvData = document.getElementById("dvData");
            var inputs = dvData.getElementsByTagName("input");
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].type == "radio") {
                    if (inputs[i] != radio) {
                        inputs[i].checked = false;

                    }
                }
            }
        }

        function MutualExclusiveGrp(radio) {
            var dvData = document.getElementById("dvDataGrp");
            var inputs = dvData.getElementsByTagName("input");
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].type == "radio") {
                    if (inputs[i] != radio) {
                        inputs[i].checked = false;

                    }
                }
            }
        }


        function CheckSelectionCount(chk) {
            var count = -2;
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
            function showLockConfirm() {
                var ret = confirm('Do you really want to lock marks for selected exam?');
                if (ret == true)
                    return true;
                else
                    return false;
            }
        }
    </script>

</asp:Content>
