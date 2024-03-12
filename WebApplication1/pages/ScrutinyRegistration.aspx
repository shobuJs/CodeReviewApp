<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="ScrutinyRegistration.aspx.cs" Inherits="ACADEMIC_ScrutinyRegistration"
    MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../Content/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" lang="javascript">

        $(document).ready(function () {

            $("[id$='cbHead']").live('click', function () {
                $("[id$='chkAccept']").attr('checked', this.checked);
            });
        });

    </script>

    <div id="divCourses" runat="server" visible="false">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title">SCRUTINY REGISTRATION</h3>
                    </div>

                    <div class="box-body">
                        <div class="col-md-12" id="tblSession" runat="server" visible="false">
                            <div class="col-md-2"></div>
                            <div class="col-md-4">
                                <label>Session Name</label>
                                <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-4">
                                <label>Roll No </label>
                                <asp:TextBox ID="txtRollNo" runat="server" MaxLength="15" />
                            </div>
                            <div class="col-md-12" style="margin-top: 25px">
                                <p class="text-center">
                                    <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" Text="Show" Font-Bold="true"
                                        ValidationGroup="Show" CssClass="btn btn-outline-info" />

                                    <asp:Button ID="btnCancel" runat="server" Text="Clear" Font-Bold="true"
                                        CausesValidation="false" OnClick="btnCancel_Click" CssClass="btn btn-outline-danger" />

                                    <asp:RequiredFieldValidator ID="rfvRollNo" ControlToValidate="txtRollNo" runat="server"
                                        Display="None" ErrorMessage="Please enter Student Roll No." ValidationGroup="Show" />
                                    <asp:ValidationSummary ID="valSummery2" runat="server" DisplayMode="List" ShowMessageBox="true"
                                        ShowSummary="false" ValidationGroup="Show" />
                                </p>
                            </div>
                        </div>

                        <div class="col-md-12" id="tblInfo" runat="server" visible="false">
                            <div class="col-md-6">
                                <ul class="list-group list-group-unbordered">
                                    <li class="list-group-item">
                                        <b>Student Name :</b><a class="pull-right">
                                            <asp:Label ID="lblName" runat="server" Font-Bold="True" /></a>
                                    </li>
                                    <li class="list-group-item">
                                        <b>Father's Name :</b><a class="pull-right">
                                            <asp:Label ID="lblFatherName" runat="server" Font-Bold="False" /></a>
                                    </li>
                                    <li class="list-group-item">
                                        <b>Mother's Name :</b><a class="pull-right">
                                            <asp:Label ID="lblMotherName" runat="server" Font-Bold="False" /></a>
                                    </li>
                                    <li class="list-group-item">
                                        <b>Enroll. No./ Roll No.:</b><a class="pull-right">
                                            <asp:Label ID="lblEnrollNo" runat="server" Font-Bold="True"></asp:Label></a>
                                    </li>
                                    <li class="list-group-item">
                                        <b>Admission Batch:</b><a class="pull-right">
                                            <asp:Label ID="lblAdmBatch" runat="server" Font-Bold="True"></asp:Label></a>
                                    </li>
                                    <li class="list-group-item">
                                        <b>Semester:</b><a class="pull-right">
                                            <asp:Label ID="lblSemester" runat="server" Font-Bold="True"></asp:Label></a>
                                    </li>
                                </ul>
                            </div>
                            <div class="col-md-6">
                                <ul class="list-group list-group-unbordered">
                                    <li class="list-group-item">
                                        <b>College Name :</b><a class="pull-right">
                                            <asp:Label ID="lblCollegeName" runat="server" Font-Bold="True"></asp:Label></a>
                                    </li>
                                    <li class="list-group-item">
                                        <b>Degree / Branch :</b><a class="pull-right">
                                            <asp:Label ID="lblBranch" runat="server" Font-Bold="True"></asp:Label></a>
                                    </li>
                                    <li class="list-group-item">
                                        <b>PH No. :</b><a class="pull-right">
                                            <asp:Label ID="lblPH" runat="server" Style="font-weight: 700"></asp:Label></a>
                                    </li>
                                    <li class="list-group-item">
                                        <b>Scheme:</b><a class="pull-right">
                                            <asp:Label ID="lblScheme" runat="server" Font-Bold="True"></asp:Label></a>
                                    </li>
                                    <li class="list-group-item">
                                        <b>Total Subjects :</b><a class="pull-right">
                                            <asp:TextBox ID="txtAllSubjects" runat="server" Enabled="false" Text="0"
                                                Style="text-align: center;"></asp:TextBox>
                                        </a>
                                    </li>
                                    <li class="list-group-item">
                                        <b>Total Credits :</b><a class="pull-right">
                                            <asp:TextBox ID="txtCredits" runat="server" Enabled="false" Text="0"
                                                Style="text-align: center;"></asp:TextBox></a>
                                        <asp:HiddenField ID="hdfCredits" runat="server" Value="0" />
                                        <asp:HiddenField ID="hdfTot" runat="server" Value="0" />
                                    </li>
                                </ul>
                            </div>

                            <div class="col-md-4"></div>
                            <div class="col-md-4">
                                <label>Apply For Semester</label>
                                <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="True"
                                    OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-12">
                            <asp:Label ID="lblErrorMsg" runat="server" Style="color: red; font-size: medium; font-weight: bold;" Text="">
                            </asp:Label>
                        </div>
                    </div>
                    <div class="box-footer">
                        <p class="text-center">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" Visible="false"
                                ValidationGroup="SUBMIT" OnClientClick="return showConfirm();" CssClass="btn btn-outline-info" />
                            <asp:Button ID="btnPrintRegSlip" runat="server" Text="Registration Slip" OnClick="btnPrintRegSlip_Click"
                                Visible="false" CssClass="btn btn-outline-primary" />
                            <asp:ValidationSummary ID="vssubmit" runat="server" DisplayMode="List" ShowMessageBox="true"
                                ShowSummary="false" ValidationGroup="SUBMIT" />
                        </p>

                        <div class="col-md-12">
                            <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
                                <asp:ListView ID="lvCurrentSubjects" runat="server">
                                    <LayoutTemplate>
                                        <div>
                                            <h4>Course List for Revaluation</h4>
                                            <table id="tblCurrentSubjects" class="table table-hover table-bordered table-responsive">
                                                <thead>
                                                    <tr class="bg-light-blue">
                                                        <th>Select
                                                        </th>
                                                        <th>Random No.
                                                        </th>
                                                        <th>Course Code
                                                        </th>
                                                        <th>Course Name
                                                        </th>
                                                        <th style="text-align: center;">Semester
                                                        </th>
                                                        <th style="text-align: center;">Semester Mark
                                                        </th>
                                                        <th style="text-align: center;">Oral Mark
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                        </div>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr id="trCurRow">
                                            <td>
                                                <asp:CheckBox ID="chkAccept" runat="server" ToolTip='<%# Eval("COURSENO")%>' ClientIDMode="Static"
                                                    Enabled='<%# Eval("FAIL_MORE_THAN_2_SUB").ToString()== "1" ? false : true %>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblRandomNo" runat="server" Text='<%# Eval("RANDOM_NO") %>' />
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
                                            <td>
                                                <asp:Label ID="LblOral" runat="server" Text='<%# Eval("S2MARK") %>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>
    <div id="divMsg" runat="server">
    </div>

    <div align="center" id="FreezePane" class="FreezePaneOff">
        <div id="InnerFreezePane" class="InnerFreezePane">
        </div>
    </div>
    <style type="text/css">
        .FreezePaneOff {
            visibility: hidden;
            display: none;
            position: absolute;
            top: -100px;
            left: -100px;
            opacity: .80;
        }

        .FreezePaneOn {
            position: fixed;
            top: 0px;
            left: 0px;
            visibility: visible;
            display: block;
            background-color: black;
            width: 100%;
            height: 100%;
            z-index: 999;
            -moz-opacity: 0.7;
            opacity: .70;
            filter: alpha(opacity=70);
            padding-top: 20%;
        }

        .InnerFreezePane {
            text-align: center;
            width: 66%;
            background-color: #171;
            color: White;
            font-size: large;
            border: dashed 2px #111;
            padding: 9px;
            opacity: .9;
        }
    </style>

    <script type="text/javascript" language="JavaScript">

        function FreezeScreen(msg) {
            scroll(0, 0);
            var outerPane = document.getElementById('FreezePane');
            var innerPane = document.getElementById('InnerFreezePane');
            if (outerPane) outerPane.className = 'FreezePaneOn';
            if (innerPane) innerPane.innerHTML = msg;
        }

        function showConfirm() {
            var ret = confirm('Do you Really want to Confirm/Submit this Courses for Scrutiny?\nOnce Submit it cannot be modified.');
            if (ret == true) {
                FreezeScreen('Please Wait, Your Data is Being Processed...');
                validate = true;
            }
            else
                validate = false;
            return validate;
        }
    </script>
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
    </script>

</asp:Content>
