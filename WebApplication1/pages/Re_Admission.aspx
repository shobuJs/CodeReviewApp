<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Re_Admission.aspx.cs" Inherits="ACADEMIC_Re_Admission" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .list-group-item {
            height: 45px;
        }
    </style>
   <%-- <script src="../Content/jquery.js" type="text/javascript"></script>

    <script src="../Content/jquery.dataTables.js" language="javascript" type="text/javascript"></script>--%>

    <script type="text/javascript" charset="utf-8">
        $(document).ready(function () {
            $(".display").dataTable({
                "bJQueryUI": true,
                "sPaginationType": "full_numbers"
            });

        });
    </script>

    <div style="z-index: 1; position: absolute; top: 10%; left: 40%;">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updCourse"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel ID="updCourse" runat="server">
        <ContentTemplate>
            <div class="row">
                <!--academic Calendar-->
                <div class="col-md-12">
                    <div class="box box-primary" id="divCourses" runat="server" visible="false">
                        <div class="box-header with-border">
                            <h3 class="box-title">RE-ADMISSION PROCESS </h3>
                        </div>
                        <div style="color: Red; font-weight: bold">
                            &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div id="tblSession" runat="server">
                            <div class="box-body">
                                <div class="col-md-12">
                                    <div class="col-md-2"></div>
                                    <div class="form-group col-md-6" id="divOptions" runat="server" visible="false" style="padding: 8px 0px 8px 10px;">
                                        <label>Options</label>
                                        <asp:RadioButtonList ID="rblOptions" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                                            OnSelectedIndexChanged="rblOptions_SelectedIndexChanged" Font-Bold="true" CssClass="form-control">
                                            <asp:ListItem Value="M" Selected="True" Text="Regular"></asp:ListItem>
                                            <asp:ListItem Value="S" Text="Backlog"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>

                                    <div class="form-group col-md-4">
                                        <label><span style="color: red;">*</span> Session Name</label>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" CssClass="form-control">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-md-4">
                                        <label><span style="color: red;">*</span> Adm.No. / Univ.Reg.No.</label>
                                        <asp:TextBox ID="txtRollNo" runat="server" CssClass="form-control" MaxLength="15" />
                                    </div>


                                </div>

                            </div>
                            <div class="box-footer">
                                <p class="text-center">
                                    <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" Text="Show"
                                        Font-Bold="true" ValidationGroup="Show" CssClass="btn btn-outline-primary" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Clear"
                                        Font-Bold="true" CssClass="btn btn-outline-danger"
                                        OnClick="btnCancel_Click" />
                                    <asp:RequiredFieldValidator ID="rfvSession" ControlToValidate="ddlSession" runat="server"
                                        Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="Show" />
                                    <asp:RequiredFieldValidator ID="rfvRollNo" ControlToValidate="txtRollNo" runat="server"
                                        Display="None" ErrorMessage="Please enter Enroll No." ValidationGroup="Show" />
                                    <asp:ValidationSummary ID="valSummery2" runat="server" DisplayMode="List" ShowMessageBox="true"
                                        ShowSummary="false" ValidationGroup="Show" />
                                    <div id="tblInfo" runat="server" class="col-md-12" visible="false">
                                        <div class="col-md-6">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Student Name</b><a class="pull-right"><asp:Label ID="lblName" runat="server" Font-Bold="True" />
                                                </a></li>
                                                <li class="list-group-item"><b>Father Name</b><a class="pull-right"><asp:Label ID="lblFatherName" runat="server" Font-Bold="True" />
                                                </a></li>
                                                <li class="list-group-item"><b>Mother Name</b><a class="pull-right">
                                                    <asp:Label ID="lblMotherName" runat="server" Font-Bold="True" />
                                                </a></li>
                                                <li class="list-group-item"><b>Admission No.</b><a class="pull-right"><asp:Label ID="lblAdmno" runat="server" Font-Bold="True"></asp:Label>
                                                </a></li>
                                                <li class="list-group-item"><b>Univ.Reg.No.</b><a class="pull-right"><asp:Label ID="lblEnrollNo" runat="server" Font-Bold="True"></asp:Label>
                                                </a></li>
                                            </ul>
                                        </div>
                                        <div class=" col-md-6">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Degree / Branch</b><a class="pull-right"><asp:Label ID="lblBranch" runat="server" Font-Bold="True"></asp:Label>
                                                </a></li>
                                                <%-- <li class="list-group-item"><b>Semester</b><a class="pull-right"><asp:Label ID="lblSemester" runat="server" Font-Bold="True"></asp:Label>--%>
                                                <li class="list-group-item"><b>Semester</b><a class="pull-right"><asp:Label ID="lblSemester" runat="server" Font-Bold="True"></asp:Label>
                                                </a></li>
                                                <%-- <li class="list-group-item"><b>PH. No.</b><a class="pull-right"><asp:Label ID="lblPH" runat="server" Style="font-weight: 700"></asp:Label>
                                                </a></li>--%>
                                                <asp:Label ID="lblPH" runat="server" Style="font-weight: 700" Visible="false"></asp:Label>

                                                <%--<li class="list-group-item"><b>Scheme</b><a class="pull-right"><asp:Label ID="lblScheme" runat="server" Font-Bold="True"></asp:Label>--%>
                                                <asp:Label ID="lblScheme" runat="server" Font-Bold="True" Visible="false"></asp:Label>
                                                <li class="list-group-item" style="padding-top: 4px !important"><b><span style="color: red;">*</span>Scheme</b><a class="pull-right">
                                                    <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="True" CssClass="form-control" AutoPostBack="true" Width="330px">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </a></li>

                                                <%--<li class="list-group-item"><b>Admission Batch</b><a class="pull-right"><asp:Label ID="lblAdmBatch" runat="server" Font-Bold="True"></asp:Label>--%>
                                                <asp:Label ID="lblAdmBatch" runat="server" Font-Bold="True" Visible="false"></asp:Label>
                                                <li class="list-group-item" style="padding-top: 4px !important"><b><span style="color: red;">*</span>Re-Admission Batch</b><a class="pull-right">
                                                    <asp:DropDownList ID="ddlAdmBatch" runat="server" AppendDataBoundItems="True" CssClass="form-control" AutoPostBack="true" Width="330px">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </a></li>
                                                <li class="list-group-item" style="padding-top: 4px !important"><b><span style="color: red;">*</span> Re-Admission Semester</b><a class="pull-right">
                                                    <asp:DropDownList ID="ddlSem" runat="server" AppendDataBoundItems="True" CssClass="form-control"  AutoPostBack="true" Width="330px">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </a></li>

                                                <li class="list-group-item" style="display: none"><b>Total Subjects</b><a class="pull-right"><asp:TextBox ID="txtAllSubjects" runat="server" Enabled="false" Style="text-align: center;" Text="0"></asp:TextBox>
                                                </a></li>
                                                <li class="list-group-item" style="display: none"><b>Total Credits</b><a class="pull-right"><asp:TextBox ID="TextBox1" runat="server" Enabled="false" Style="text-align: center;" Text="0"></asp:TextBox>
                                                    <asp:HiddenField ID="hdfCredits" runat="server" Value="0" />
                                                    <asp:HiddenField ID="hdfTot" runat="server" Value="0" />
                                                </a></li>
                                            </ul>
                                        </div>
                                        <div class="box-footer col-md-12">
                                            <p class="text-center">
                                                <asp:Button ID="btnSubmitDetails" runat="server" CssClass="btn btn-outline-info" Enabled="true" OnClick="btnSubmitDetails_Click" OnClientClick="return showConfirm();" Text="PROCEED FOR RE-ADMISSION" ValidationGroup="SUBMIT" />
                                            </p>
                                        </div>
                                        <div class="col-md-12">
                                            <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
                                                <div style="margin-bottom: 10px;">
                                                    <span style="float: right; background-color: #fff; padding: 5px; color: #008d4c;">
                                                        <asp:Label ID="l1" runat="server" Text="Minimum Credit : " Visible="false" />
                                                        <asp:Label ID="lblfromcredit" runat="server" Visible="false" />&nbsp &nbsp &nbsp
                                                        <asp:Label ID="l2" runat="server" Text="Maximum Credit : " Visible="false" />
                                                        <asp:Label ID="lblfTocredit" runat="server" Visible="false" />
                                                    </span>
                                                </div>
                                                <asp:ListView ID="lvCurrentSubjects" runat="server" Visible="false">
                                                    <LayoutTemplate>
                                                        <div>
                                                            <h4>Offered Subjects </h4>
                                                            <table id="tblCurrentSubjects" class="table table-hover table-bordered">
                                                                <thead>
                                                                    <tr class="bg-light-blue">
                                                                        <th>
                                                                            <asp:CheckBox ID="cbHeadReg" runat="server" onclick="SelectAll(this,1,'chkRegister');" Text="Register" ToolTip="Register/Register all" Visible="false" />
                                                                        </th>
                                                                        <th>Subject Code </th>
                                                                        <th>Subjects </th>
                                                                        <th>Type </th>
                                                                        <th>Semester </th>
                                                                        <th>Sub. Type </th>
                                                                        <th>Credits </th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr id="trCurRow">
                                                            <td>
                                                                <%--<asp:CheckBox ID="chkAccept" runat="server" Enabled='<%# Eval("STATUS").ToString()=="1"? false:true %>' Checked='<%# Eval("EXAM_REGISTERED").ToString() == "1" ? true : false %>' AutoPostBack="true" OnCheckedChanged="chkAccept_CheckedChanged" />--%>
                                                                <asp:CheckBox ID="chkRegister" runat="server" onclick="ChkHeader(1,'cbHeadReg','chkRegister');" ToolTip="Click to select this subject for Re-Admission" Value='<%# Eval("EXAM_REGISTERED") %>' Checked='<%#Eval("RE_ADMITTED").ToString()=="1"?true:false %>'  Visible="false" />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("ELECTIVE") %>' />
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
                                        <div class="col-md-12">
                                            <asp:ListView ID="lvBacklogSubjects" runat="server">
                                                <LayoutTemplate>
                                                    <div>
                                                        <h4>Backlog Subjects</h4>
                                                        <table id="tblBacklogSubjects" class="table table-hover table-bordered">
                                                            <thead>
                                                                <tr class="bg-light-blue">
                                                                    <th>
                                                                        <asp:CheckBox ID="cbHeadReg" runat="server" onclick="SelectAll(this,2,'chkRegister');" Text="Register" ToolTip="Register/Register all" />
                                                                    </th>
                                                                    <th>Course Code </th>
                                                                    <th>Course Name </th>
                                                                    <th>Semester </th>
                                                                    <th>Subject Type </th>
                                                                    <th>Credits </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr id="trCurRow">
                                                        <td>
                                                            <asp:CheckBox ID="chkRegister" runat="server" onclick="ChkHeader(2,'cbHeadReg','chkRegister');" ToolTip="Click to select this subject for registration" Value='<%# Eval("EXAM_REGISTERED") %>' />
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
                                        </div>
                                        <div class="col-md-12">
                                            <asp:ListView ID="lvAuditSubjects" runat="server">
                                                <LayoutTemplate>
                                                    <div>
                                                        <h4>Audit Subjects</h4>
                                                        <table id="tblAuditSubjects" class="table table-hover table-bordered">
                                                            <thead>
                                                                <tr class="bg-light-blue">
                                                                    <th>
                                                                        <asp:CheckBox ID="cbHeadReg" runat="server" onclick="SelectAll(this,3,'chkRegister');" Text="Register" ToolTip="Register/Register all" Visible="false" />
                                                                    </th>
                                                                    <th>Course Code </th>
                                                                    <th>Course Name </th>
                                                                    <th>Semester </th>
                                                                    <th>Subject Type </th>
                                                                    <th>Credits </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr id="trCurRow">
                                                        <td>
                                                            <asp:CheckBox ID="chkRegister" runat="server" onclick="ChkHeader(3,'cbHeadReg','chkRegister');" ToolTip="Click to select this subject for registration" Value='<%# Eval("EXAM_REGISTERED") %>' Visible="false" />
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
                                        </div>
                                        <div class="box-footer col-md-12">
                                            <p class="text-center">
                                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-outline-info" Enabled="false" OnClick="btnSubmit_Click" Text="SUBMIT" ValidationGroup="SUBMIT" Visible="false" />
                                                <asp:Button ID="btnPrintRegSlip" runat="server" CssClass="btn btn-outline-primary" Enabled="false" OnClick="btnPrintRegSlip_Click" Text="REGISTRATION SLIP" Visible="false" />
                                                <asp:ValidationSummary ID="vssubmit" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="SUBMIT" />
                                            </p>
                                        </div>
                                    </div>
                                    <div>
                                        <asp:Label ID="lblmsg" runat="server" Style="color: Red; font-weight: bold" Text=""></asp:Label>
                                    </div>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                </p>

                            </div>
                            <!-- /.box-body -->
                            <div id="divMsg" runat="server">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


    <script type="text/javascript" language="javascript">
        //        //To change the colour of a row on click of check box inside the row..
        //        $("tr :checkbox").live("click", function() {
        //        $(this).closest("tr").css("background-color", this.checked ? "#FFFFD2" : "#FFFFFF");
        //        });

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
                //if (chkcnt > 0)
                //    document.getElementById(headid).checked = true;
                //else
                //    document.getElementById(headid).checked = false;
            }
            catch (e) {
                alert(e);
            }
        }
        function showConfirm() {
            //var ret = confirm('Do you Really want to Confirm/Submit this Student Details for Re- Admission?');
            var ret = confirm('Are you Really want to Proceed For Re-Admission?');
            if (ret == true)
                return true;
            else
                return false;
        }



    </script>

</asp:Content>


