<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="CourseRegistrationForPhd.aspx.cs" Inherits="ACADEMIC_CourseRegistration"
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
            <td class="vista_page_title_bar" style="height: 30px">PHD COURSE REGISTRATION
                <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>
    </table>
    <div id="divCourses" runat="server" visible="false">
        <asp:Panel ID="pnlMain" runat="server">
            <fieldset class="fieldset">
                <legend class="legend">Phd Course Registration</legend>
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
                            rowspan="11" visible="false">
                            <fieldset class="fieldset" style="background-color: #B9FFB9">
                                <legend class="legend">Applicable Fees Structure</legend>
                                <table cellpadding="1" cellspacing="1" style="width: 100%">
                                    <tr>
                                        <td>Payment Type Category :
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlPayType" runat="server" AppendDataBoundItems="true" Width="95%" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Semester:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true" Width="95%" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Applicable Fees:
                                        </td>
                                        <td>
                                            <asp:Label ID="lblFeeAmount" runat="server" Font-Bold="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="text-align: center">
                                            <asp:Button ID="btnModifyPType" runat="server" Text="Modify Payment Type Category"
                                                OnClick="btnModifyPType_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
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
                        <td style="width: 15%; text-align: right; padding-top: 4px; padding-bottom: 4px;">Offered Courses :</td>
                        <td class="form_left_text">
                            <asp:DropDownList ID="ddlOfferedCourse" runat="server"
                                AppendDataBoundItems="True" Width="75%" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlOfferedCourse_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="trDeptCourse" runat="server" visible="false">
                        <td style="width: 15%; text-align: right; padding-top: 4px; padding-bottom: 4px;">Select Department Courses :</td>
                        <td class="form_left_text">
                            <asp:DropDownList ID="ddlSelectCourse" runat="server"
                                AppendDataBoundItems="True" Width="75%" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="trAddCourse" runat="server" visible="false">
                        <td style="text-align: right; padding-top: 4px; padding-bottom: 4px;" colspan="2">
                            <fieldset class="fieldset" style="background-color: #B9FFB9">
                                <legend class="legend">New course Entry</legend>
                                <table width="100%" style="background-color: #B9FFB9">
                                    <tr>
                                        <td width="15%" align="left">Code:</td>
                                        <td width="20%" align="left">
                                            <asp:TextBox ID="txtCCode" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvCode" runat="server"
                                                ControlToValidate="txtCCode" Display="None" ErrorMessage="Please Enter CCode"
                                                ValidationGroup="submit" />
                                        </td>
                                        <td width="15%" align="left">Course Name :</td>
                                        <td align="left">

                                            <asp:TextBox ID="txtCourseName" runat="server" Width="80%"></asp:TextBox>

                                            <asp:RequiredFieldValidator ID="rdfvCourseName" runat="server"
                                                ControlToValidate="txtCourseName" Display="None"
                                                ErrorMessage="Please Enter Course Name" ValidationGroup="submit" />

                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="10%" align="left">Subject Type :</td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlSubjectType" runat="server" Width="100%"
                                                AppendDataBoundItems="True">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left">BOS. Dept :</td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlBosDept" runat="server" Width="80%"
                                                AppendDataBoundItems="True">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="10%" align="left">Credits :</td>
                                        <td align="left">
                                            <asp:TextBox ID="txtCredit" runat="server" Width="20%" MaxLength="2" onkeyup="validateNumeric(this);"></asp:TextBox>
                                            <span style="color: #FF0000">
                                                <asp:RequiredFieldValidator ID="rfvCredits" runat="server"
                                                    ControlToValidate="txtCredit" Display="None" ErrorMessage="Please Enter Credits"
                                                    ValidationGroup="submit" />
                                            </span></td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td align="left" width="10%">&nbsp;</td>
                                        <td align="left">
                                            <asp:RequiredFieldValidator ID="rfvSubType" runat="server"
                                                ControlToValidate="ddlSubjectType" Display="None"
                                                ErrorMessage="Please Select Theory/Practical" InitialValue="0"
                                                ValidationGroup="submit" />
                                            <asp:RequiredFieldValidator ID="rfvBosDept" runat="server"
                                                ControlToValidate="ddlBosDept" Display="None"
                                                ErrorMessage="Please Select BOS Department" InitialValue="0"
                                                ValidationGroup="submit" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnSub" runat="server" Text="Course Submit"
                                                OnClick="btnSub_Click" ValidationGroup="submit" />
                                        </td>
                                        <td>
                                            <asp:ValidationSummary ID="rfvValidationSummary" runat="server"
                                                DisplayMode="List" ShowMessageBox="True" ShowSummary="False"
                                                ValidationGroup="submit" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                    <tr id="trProAdm" runat="server" visible="false">
                        <td style="width: 15%; text-align: right; padding-top: 4px; padding-bottom: 4px;">Provisional Admission :</td>
                        <td class="form_left_text">&nbsp<asp:RadioButton ID="rbYes" runat="server" Text="Yes"
                            GroupName="adm" AutoPostBack="True"
                            OnCheckedChanged="rbYes_CheckedChanged" />
                            &nbsp;
                            <asp:RadioButton ID="rbNo" runat="server" Text="No" Checked="true"
                                GroupName="adm" />
                        </td>
                    </tr>
                    <tr id="trPayStatus" runat="server" visible="false">
                        <td style="width: 15%; text-align: right; padding-top: 4px; padding-bottom: 4px;">Payment Status :</td>
                        <td class="form_left_text">
                            <asp:RadioButtonList ID="rbPaymentStatus" runat="server"
                                RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">Yes</asp:ListItem>
                                <asp:ListItem Selected="True" Value="1">No</asp:ListItem>
                            </asp:RadioButtonList>
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
                    <tr id="trEligible" runat="server" visible="false"
                        style="background-color: #00E171">
                        <td align="center" colspan="3" style="border: 1px solid #000000">
                            <asp:Image ID="imgEligible" runat="server" ImageUrl="~/IMAGES/success.png" />
                            The Student is Eligilbe for Course Registration.
                        </td>
                    </tr>
                    <tr id="trUnEligible" runat="server" visible="false" style="background-color: red">
                        <td align="center" colspan="3" style="border: 1px solid #000000">
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/IMAGES/error.png" />
                            The Student is UnEligilbe for Course Registration.
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="padding: 10px; text-align: center;">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="3" style="padding: 10px; text-align: center;">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" Width="80px" OnClick="btnSubmit_Click"
                                Enabled="false" />
                            &nbsp;
                        &nbsp;
                            <asp:Button ID="btnPrintRegSlip" runat="server" Text="Registration Slip" OnClick="btnPrintRegSlip_Click" />
                            &nbsp;
                            <asp:Button ID="btnPrePrintClallan" runat="server" Text="Re-Print Challan"
                                OnClick="btnPrePrintClallan_Click" Visible="false" />
                            &nbsp;
                           
                            &nbsp;&nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                OnClick="btnCancel_Click" />

                            &nbsp;&nbsp;<asp:Button ID="btnLock" runat="server" Text="Lock" Visible="false"
                                OnClientClick="return showLockConfirm();"
                                OnClick="btnLock_Click" />

                            &nbsp;
                           
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
                                                <th style="width: 10%">Credits
                                                </th>
                                            </tr>
                                            <tr id="itemPlaceholder" runat="server" />
                                            <thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td style="width: 10%">
                                            <asp:LinkButton ID="lbReport" runat="server" OnClick="lbReport_Click"><%# Eval("SESSION_NAME") %></asp:LinkButton>
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
                                        <td style="width: 10%">
                                            <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>' />
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
