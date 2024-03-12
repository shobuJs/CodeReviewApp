<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="CourseRegistrationForUG.aspx.cs" Inherits="ACADEMIC_CourseRegistrationByAdmin"
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
        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title">COURSE REGISTRATION UG</h3>
                        <div class="box-tools pull-right">
                        </div>
                    </div>

                    <div class="box-body">
                        <div class="col-md-3">
                            <label>Session Name</label>
                            <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True">
                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                            <label>Examination Roll No.</label>
                            <asp:TextBox ID="txtRollNo" runat="server" MaxLength="15" />
                        </div>
                        <div class="col-md-3" style="margin-top: 25px">
                            <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" Text="Show"
                                Font-Bold="true" ValidationGroup="Show" CssClass="btn btn-outline-info" />
                            <asp:Button ID="btnCancel" runat="server" Text="Clear"
                                Font-Bold="true" ValidationGroup="Show" CssClass="btn btn-outline-danger"
                                OnClick="btnCancel_Click" />
                            <asp:RequiredFieldValidator ID="rfvRollNo" ControlToValidate="txtRollNo" runat="server"
                                Display="None" ErrorMessage="Please enter Student Roll No." ValidationGroup="Show" />
                            <asp:ValidationSummary ID="valSummery2" runat="server" DisplayMode="List" ShowMessageBox="true"
                                ShowSummary="false" ValidationGroup="Show" />
                        </div>
                        <div class="col-md-12" id="tblInfo" runat="server" visible="false">
                            <div class="col-md-6">
                                <ul class="list-group list-group-unbordered">
                                    <li class="list-group-item">
                                        <b>Student Name:</b><a class="pull-right">
                                            <asp:Label ID="lblName" runat="server" Font-Bold="True" /></a>
                                    </li>
                                    <li class="list-group-item">
                                        <b>Father's Name:</b><a class="pull-right">
                                            <asp:Label ID="lblFatherName" runat="server" Font-Bold="False" /></a>
                                    </li>
                                    <li class="list-group-item">
                                        <b>Mother's Name:</b><a class="pull-right">
                                            <asp:Label ID="lblMotherName" runat="server" Font-Bold="False" /></a>
                                    </li>
                                    <li class="list-group-item">
                                        <b>College Name:</b><a class="pull-right">
                                            <asp:Label ID="lblCollege" runat="server" Font-Bold="True"></asp:Label></a>
                                    </li>
                                    <li class="list-group-item">
                                        <b>Roll No.:</b><a class="pull-right">
                                            <asp:Label ID="lblEnrollNo" runat="server" Font-Bold="True"></asp:Label></a>
                                    </li>
                                    <li class="list-group-item">
                                        <b>Registration No.:</b><a class="pull-right">
                                            <asp:Label ID="lblRegNo" runat="server" Font-Bold="True"></asp:Label></a>
                                    </li>
                                </ul>
                            </div>
                            <div class="col-md-6">
                                <ul class="list-group list-group-unbordered">
                                    <li class="list-group-item">
                                        <b>Admission Batch :</b><a class="pull-right">
                                            <asp:Label ID="lblAdmBatch" runat="server" Font-Bold="True"></asp:Label></a>
                                    </li>
                                    <li class="list-group-item">
                                        <b>Semester:</b><a class="pull-right">
                                            <asp:Label ID="lblSemester" runat="server" Font-Bold="True"></asp:Label></a>
                                    </li>
                                    <li class="list-group-item">
                                        <b>Degree / Branch:</b><a class="pull-right">
                                            <asp:Label ID="lblBranch" runat="server" Font-Bold="True"></asp:Label></a>
                                    </li>
                                    <li class="list-group-item">
                                        <b>Phone No. :</b><a class="pull-right">
                                            <asp:Label ID="lblPH" runat="server" Style="font-weight: 700"></asp:Label></a>
                                    </li>
                                    <li class="list-group-item">
                                        <b>Scheme :</b><a class="pull-right">
                                            <asp:Label ID="lblScheme" runat="server" Font-Bold="True"></asp:Label></a>
                                    </li>
                                    <li class="list-group-item">
                                        <b>Core Subject:</b><a class="pull-right">
                                            <asp:Label ID="lblCoreSub" runat="server" Font-Bold="True"></asp:Label></a>
                                    </li>

                                </ul>
                            </div>

                            <div class="col-md-3" style="display: none">
                                <label>Total Subject</label>
                                <asp:TextBox ID="txtAllSubjects" runat="server" Enabled="false" Text="0" Width="30%"
                                    Style="text-align: center;"></asp:TextBox>
                            </div>
                            <div class="col-md-3" style="display: none">
                                <label>Total Credits</label>
                                <asp:TextBox ID="txtCredits" runat="server" Enabled="false" Text="0" Width="30%"
                                    Style="text-align: center;"></asp:TextBox>
                                <asp:HiddenField ID="hdfCredits" runat="server" Value="0" />
                                <asp:HiddenField ID="hdfTot" runat="server" Value="0" />
                            </div>
                            <div class="col-md-12 table table-responsive">
                                <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
                                    <asp:ListView ID="lvCurrentSubjects" runat="server" Visible="false" OnItemDataBound="lvCurrentSubjects_ItemDataBound">
                                        <LayoutTemplate>
                                            <div>
                                                <h4>Current Semester Subjects</h4>
                                                <table id="tblCurrentSubjects" class="table table-hover table-bordered">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th>
                                                                <asp:CheckBox ID="cbHeadReg" runat="server" Text="Register" ToolTip="Register/Register all" onclick="SelectAll(this,1,'chkRegister');" />
                                                            </th>
                                                            <th>Course Code
                                                            </th>
                                                            <th>Course Name
                                                            </th>
                                                            <th>Semester
                                                            </th>
                                                            <th>Sub. Type
                                                            </th>
                                                            <th>Credits
                                                            </th>
                                                            <th>Course Category
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
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
                                                <td>
                                                    <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("SEMESTER") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSub_Type" runat="server" Text='<%# Eval("SUBNAME") %>' ToolTip='<%# Eval("SUBID") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>' />
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlCourseCategory" runat="server" AppendDataBoundItems="true" ToolTip='Please select Student Course Category'>
                                                        <asp:ListItem Text="Please Select" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>

                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>

                            <div class="col-md-12 table table-responsive">
                                <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto">
                                    <asp:ListView ID="lvBacklogSubjects" runat="server">
                                        <LayoutTemplate>
                                            <div>
                                                <h4>Arrear Subjects</h4>
                                                <table id="tblBacklogSubjects" class="table table-hover table-bordered">
                                                    <thead>
                                                        <tr class="bg-light-blue">

                                                            <th>
                                                                <asp:CheckBox ID="cbHeadReg" runat="server" Text="Register" ToolTip="Register/Register all" onclick="SelectAll(this,2,'chkRegister');" />
                                                            </th>
                                                            <th>Course Code
                                                            </th>
                                                            <th>Course Name
                                                            </th>
                                                            <th>Semester
                                                            </th>
                                                            <th>Subject Type
                                                            </th>
                                                            <th>Credits
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
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
                                                <td>
                                                    <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSENAME") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("SEMESTER") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSub_Type" runat="server" Text='<%# Eval("SUBNAME") %>' ToolTip='<%# Eval("SUBID") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>' />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>

                            <div class="col-md-12  table table-responsive">
                                <asp:Panel ID="Panel3" runat="server" ScrollBars="Auto">
                                    <asp:ListView ID="lvAuditSubjects" runat="server">
                                        <LayoutTemplate>
                                            <div>

                                                <h4>Audit Subjects</h4>
                                                <table id="tblAuditSubjects" class="table table-hover table-bordered">
                                                    <thead>
                                                        <tr class="bg-light-blue">

                                                            <th>
                                                                <asp:CheckBox ID="cbHeadReg" runat="server" Text="Register" ToolTip="Register/Register all" onclick="SelectAll(this,3,'chkRegister');" />
                                                            </th>
                                                            <th>Course Code
                                                            </th>
                                                            <th>Course Name
                                                            </th>
                                                            <th>Semester
                                                            </th>
                                                            <th>Subject Type
                                                            </th>
                                                            <th>Credits
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
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
                                                <td>
                                                    <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("SEMESTER") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSub_Type" runat="server" Text='<%# Eval("SUBNAME") %>' ToolTip='<%# Eval("SUBID") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>' />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                    <div class="box-footer">
                        <p class="text-center">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-outline-info" OnClick="btnSubmit_Click"
                                Enabled="false" ValidationGroup="SUBMIT" OnClientClick="return showConfirm();" />
                            <asp:Button ID="btnPrintRegSlip" runat="server" Text="Registration Slip"
                                OnClick="btnPrintRegSlip_Click" Enabled="false" CssClass="btn btn-outline-primary" />
                            <asp:ValidationSummary ID="vssubmit" runat="server" DisplayMode="List" ShowMessageBox="true"
                                ShowSummary="false" ValidationGroup="SUBMIT" />
                        </p>
                        <div id="divMsg" runat="server">
                        </div>

                    </div>
                </div>
            </div>
        </div>
        <tr>
            <td colspan="2" style="padding: 10px; text-align: center;"></td>
        </tr>
        <tr>
            <td colspan="2" style="padding: 10px; text-align: center;"></td>
        </tr>
        <tr>
            <td colspan="2" style="padding: 10px; text-align: center;"></td>
        </tr>
        </table>
        </fieldset>
    </div>
    <asp:HiddenField ID="hdfTotNoCourses" runat="server" Value="0" />


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
