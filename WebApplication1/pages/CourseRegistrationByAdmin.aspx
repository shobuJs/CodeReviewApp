<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="CourseRegistrationByAdmin.aspx.cs" Inherits="ACADEMIC_CourseRegistrationByAdmin"
    MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updReg"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div id="preloader">
                    <div class="loader-container">
                        <div class="loader-container__bar"></div>
                        <div class="loader-container__bar"></div>
                        <div class="loader-container__bar"></div>
                        <div class="loader-container__bar"></div>
                        <div class="loader-container__bar"></div>
                        <div class="loader-container__ball"></div>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:UpdatePanel ID="updReg" runat="server">
        <ContentTemplate>
            <div class="row">
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
              
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary" id="divCourses" runat="server" visible="false">
                        <div class="box-header with-border">
                            <h3 class="box-title"><span>COURSE REGISTRATION BY ADMIN</span></h3>
                        </div>
                        
                        <div id="tblSession" runat="server">
                            <div class="box-body">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12" id="div1" runat="server" visible="false" style="padding: 8px 0px 8px 10px;">
                                            <div class="label-dynamic">
                                                <label>Options</label>
                                            </div>
                                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                                                OnSelectedIndexChanged="rblOptions_SelectedIndexChanged" Font-Bold="true" CssClass="form-control">
                                                <asp:ListItem Value="M" Selected="True" Text="Regular"></asp:ListItem>
                                                <asp:ListItem Value="S" Text="Arrear"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>College/School Name</label>
                                            </div>
                                            <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCollege"
                                                Display="None" ErrorMessage="Please Select College/School Name<." InitialValue="0" SetFocusOnError="True"
                                                ValidationGroup="Show"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Session Name</label>
                                            </div>
                                            <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                                Display="None" ErrorMessage="Please Select Session." InitialValue="0" SetFocusOnError="True"
                                                ValidationGroup="Show"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Univ. Reg. No./TAN/PAN</label>
                                            </div>
                                            <asp:TextBox ID="txtRollNo" runat="server" CssClass="form-control" MaxLength="15" />
                                            <asp:RequiredFieldValidator ID="rfvRegno" runat="server" ControlToValidate="txtRollNo"
                                                Display="None" ErrorMessage="Please Enter Univ. Reg. No. / TAN/PAN"
                                                SetFocusOnError="True" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                        </div>

                                    </div>
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" Text="Show"
                                        Font-Bold="true" ValidationGroup="Show" CssClass="btn btn-outline-info" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Clear"
                                        Font-Bold="true" CssClass="btn btn-outline-danger"
                                        OnClick="btnCancel_Click" />

                                    <asp:ValidationSummary ID="valSummery2" runat="server" DisplayMode="List" ShowMessageBox="true"
                                        ShowSummary="false" ValidationGroup="Show" />
                                </div>

                                <div class="col-12" id="tblInfo" runat="server" visible="false">
                                    <div class="row">
                                        <div class="col-lg-6 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered ipad-view">
                                                <li class="list-group-item"><b>Student Name :</b>
                                                    <a class="sub-label"><asp:Label ID="lblName" runat="server" Font-Bold="True" /> </a>
                                                </li>
                                                <li class="list-group-item"><b>Father Name :</b>
                                                    <a class="sub-label"><asp:Label ID="lblFatherName" runat="server" Font-Bold="true" /></a>
                                                </li>
                                                <li class="list-group-item"><b>Mother Name :</b>
                                                    <a class="sub-label"><asp:Label ID="lblMotherName" runat="server" Font-Bold="true" /></a>
                                                </li>
                                                <li class="list-group-item"><b>College/School Name :</b>
                                                    <a class="sub-label"><asp:Label ID="lblCollege" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li> 
                                                <li class="list-group-item"><b>Univ. Reg. No. :</b>
                                                    <a class="sub-label"><asp:Label ID="lblEnrollNo" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item"><b>TAN/PAN :</b>
                                                    <a class="sub-label"><asp:Label ID="lblRegNo" runat="server" Style="font-weight: 700"></asp:Label></a>
                                                </li>  
                                                <li class="list-group-item"><b>Admission Batch :</b>
                                                    <a class="sub-label"><asp:Label ID="lblAdmBatch" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>        
                                            </ul>
                                        </div>

                                        <div class="col-lg-6 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>Degree/Branch :</b>
                                                    <a class="sub-label"><asp:Label ID="lblBranch" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item"><b>Semester/Year :</b>
                                                    <a class="sub-label"><asp:Label ID="lblSemester" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item"><b>PH. No. :</b>
                                                    <a class="sub-label"><asp:Label ID="lblPH" runat="server" Style="font-weight: 700"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item"><b>Curriculum :</b>
                                                    <a class="sub-label"><asp:Label ID="lblScheme" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>
                                                <li class="list-group-item"><b>Total Subjects :</b>
                                                    <a class="sub-label"><asp:TextBox ID="txtAllSubjects" runat="server" Enabled="false" Text="0"
                                                        Style="text-align: center;"></asp:TextBox></a>
                                                </li>
                                                <li class="list-group-item"><b>Total Credits :</b>
                                                    <a class="sub-label">
                                                        <asp:TextBox ID="TextBox1" runat="server" Enabled="false" Text="0" Style="text-align: center;"></asp:TextBox>
                                                        <asp:HiddenField ID="hdfCredits" runat="server" Value="0" />
                                                        <asp:HiddenField ID="hdfTot" runat="server" Value="0" />
                                                    </a>
                                                </li> 
                                            </ul>
                                        </div>
                                    </div>

                                    <div class="mt-3">
                                        <asp:ListView ID="lvCurrentSubjects" runat="server" Visible="false">
                                            <LayoutTemplate>
                                                <div>
                                                    <div class="sub-heading">
                                                        <h5>Current Semester Subjects</h5>
                                                    </div>
                                            
                                                    <table class="table table-striped table-bordered nowrap display" style="width:100%" id="tblCurrentSubjects">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>
                                                                    <asp:CheckBox ID="cbHeadReg" runat="server" Text="Register" ToolTip="Register/Register all" onclick="SelectAll(this,1,'chkRegister');" Checked="false" />
                                                                </th>
                                                                <th>Subject Code
                                                                </th>
                                                                <th>Subject Name
                                                                </th>
                                                                <th>Semester/Year
                                                                </th>
                                                                <th>Sub. Type
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
                                                <tr id="trCurRow">
                                                    <td>
                                                        <asp:CheckBox ID="chkRegister" runat="server" Value='<%# Eval("REGISTERED") %>' ToolTip="Click to select this subject for registration"
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
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </div>

                                    <div class="mt-3">
                                        <asp:ListView ID="lvBacklogSubjects" runat="server">
                                            <LayoutTemplate>
                                                <div>
                                                    <div class="sub-heading">
                                                        <h5>Arrear Subjects</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap display" style="width:100%" id="tblBacklogSubjects">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>
                                                                    <asp:CheckBox ID="cbHeadReg" runat="server" Text="Register" ToolTip="Register/Register all" onclick="SelectAll(this,2,'chkRegister');" />
                                                                </th>
                                                                <th>Subject Code</th>
                                                                <th>Subject Name</th>
                                                                <th>Semester/Year</th>
                                                                <th>Subject Type</th>
                                                                <th>Credits</th>
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
                                                        <asp:CheckBox ID="chkRegister" runat="server" Value='<%# Eval("REGISTERED") %>' ToolTip="Click to select this subject for registration"
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
                                    </div>

                                    <div class="mt-3">
                                        <asp:ListView ID="lvAuditSubjects" runat="server">
                                            <LayoutTemplate>
                                                <div>
                                                    <div class="sub-heading">
                                                        <h5>Audit Subjects</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap display" style="width:100%" id="tblAuditSubjects">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>
                                                                    <asp:CheckBox ID="cbHeadReg" runat="server" Text="Register" ToolTip="Register/Register all" onclick="SelectAll(this,3,'chkRegister');" />
                                                                </th>
                                                                <th>Subject Code</th>
                                                                <th>Subject Name</th>
                                                                <th>Semester/Year</th>
                                                                <th>Subject Type</th>
                                                                <th>Credits</th>
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
                                                        <asp:CheckBox ID="chkRegister" runat="server" Value='<%# Eval("REGISTERED") %>' ToolTip="Click to select this subject for registration"
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
                                    </div>

                                    <div class="col-12 btn-footer mt-4">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-outline-info" OnClick="btnSubmit_Click"
                                            Enabled="false" ValidationGroup="SUBMIT" OnClientClick="return showConfirm();" />
                                        <asp:Button ID="btnPrintRegSlip" runat="server" Text="Registration Slip"
                                            OnClick="btnPrintRegSlip_Click" Enabled="false" CssClass="btn btn-outline-primary" />
                                        <asp:ValidationSummary ID="vssubmit" runat="server" DisplayMode="List" ShowMessageBox="true"
                                            ShowSummary="false" ValidationGroup="SUBMIT" />
                                    </div>

                                    <div class="col-12 btn-footer">
                                        <asp:Label ID="lblmsg" runat="server" Style="color: Red; font-weight: bold" Text=""></asp:Label>
                                    </div>
                                </div>

                                <div id="div2" runat="server">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:HiddenField ID="hdfTotNoCourses" runat="server" Value="0" />

            </div>
        </ContentTemplate>
        <Triggers>
            <%--<asp:AsyncPostBackTrigger ControlID="btnPrintRegSlip" />--%>
            <asp:PostBackTrigger ControlID="btnPrintRegSlip" />
        </Triggers>

    </asp:UpdatePanel>
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
