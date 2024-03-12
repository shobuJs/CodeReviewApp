<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Lockunlockstudentwise.aspx.cs" Inherits="ACADEMIC_Lockunlockstudentwise"
    MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updReg"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:UpdatePanel ID="updReg" runat="server">
        <ContentTemplate>
            <div class="row">

                <!--academic Calendar-->
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">LOCK/UNLOCK STUDENT WISE</h3>
                        </div>
                        <div style="color: Red; font-weight: bold">
                            &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->

                        <div class="box-body">
                            <div class="col-md-12">
                                <div class="form-group col-md-4">
                                    <label><span style="color: red;">*</span>Session</label>
                                    <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" CssClass="form-control">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                        Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="Show"
                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-md-4">
                                    <label><span style="color: red;">*</span>Univ. Reg. No.</label>
                                    <asp:TextBox ID="txtRollNo" runat="server" OnTextChanged="txtRollNo_TextChanged" AutoPostBack="true" CssClass="form-control" TabIndex="2" ValidationGroup="show" MaxLength="20" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRollNo"
                                        Display="None" ErrorMessage="Please Enter Univ. Reg. No." ValidationGroup="Show"
                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-md-4">
                                    <label><span style="color: red;">*</span>Exam Name</label>
                                    <asp:DropDownList ID="ddlExamType" runat="server" AppendDataBoundItems="True" TabIndex="3"
                                        ValidationGroup="show" CssClass="form-control">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvExamType" runat="server" ControlToValidate="ddlExamType"
                                        Display="None" ErrorMessage="Please Select Exam Name" InitialValue="0" SetFocusOnError="True"
                                        ValidationGroup="Show"></asp:RequiredFieldValidator>
                                </div>

                            </div>

                        </div>
                        <div class="box-footer">
                            <p class="text-center">
                                <asp:Button ID="btnShow" runat="server" TabIndex="4" OnClick="btnShow_Click" Text="Show"
                                    Font-Bold="true" ValidationGroup="Show" CssClass="btn btn-outline-primary" />
                                <asp:Button ID="btnlockunlock" runat="server" OnClick="btnlockunlock_Click" TabIndex="5" Text="Lock/Unlock"
                                    Font-Bold="true" ValidationGroup="Show" CssClass="btn btn-outline-primary" />
                                <asp:Button ID="btnCancel" runat="server" TabIndex="6" Text="Clear"
                                    Font-Bold="true" CssClass="btn btn-outline-danger"
                                    OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="valSummery2" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="Show" />
                            </p>
                            <div class="col-md-12" id="tblInfo" runat="server">
                                <div class="col-md-12 table table-responsive">
                                    <asp:ListView ID="lvlockunlock" runat="server">
                                        <LayoutTemplate>
                                            <div>
                                                <h4>Lock/Unlock Marks </h4>
                                                <table id="tbllockunlock" class="table table-hover table-bordered">
                                                    <thead>
                                                        <tr class="bg-light-blue">

                                                            <th>SL.No
                                                            </th>
                                                            <th>Subject Code
                                                            </th>
                                                            <th>Subject Name
                                                            </th>
                                                            <th>Semester
                                                            </th>
                                                            <th>Sub. Type
                                                            </th>
                                                            <th>Lock/Unlock
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
                                            <tr id="trCurRow">

                                                <td>
                                                    <%#Container.DataItemIndex+1 %>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' ToolTip='<%# Eval("semesterno") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("SEMESTERNAME") %>' ToolTip='<%# Eval("SCHEMENO") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSub_Type" runat="server" Text='<%# Eval("SUBNAME") %>' ToolTip='<%# Eval("SUBID") %>' />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chklock" runat="server" TabIndex="10" ToolTip='Select to Lock' Checked='<%# Eval("LOCK").ToString() == "True" ? true : false %>' />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                            </div>
                            <div>
                                <asp:Label ID="lblmsg" runat="server" Style="color: Red; font-weight: bold" Text=""></asp:Label>
                            </div>

                        </div>
                        <!-- /.box-body -->
                        <div id="div2" runat="server">
                        </div>

                    </div>
                </div>
                <asp:HiddenField ID="hdfTotNoCourses" runat="server" Value="0" />
                <div id="divMsg" runat="server">
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
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
