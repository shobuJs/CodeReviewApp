<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteMasterPage.master" CodeFile="CourseRegistrationModify.aspx.cs"
    Inherits="ACADEMIC_CourseRegistrationModify" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updReg"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div id="preloader">
                    <div id="loader-img">
                        <div id="loader">
                        </div>
                        <p class="saving">Loading<span>.</span><span>.</span><span>.</span></p>
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
                            <asp:ListItem Value="S" Text="Backlog"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <!--academic Calendar-->
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary" id="divCourses" runat="server" visible="false">
                        <div class="box-header with-border">
                            <h3 class="box-title"><%--MarkEntry By LIC--%>
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label>
                    </h3>
                        </div>

                        <!-- /.box-header -->
                        <!-- form start -->
                        <div id="tblSession" runat="server">
                            <div class="box-body">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-4 col-md-6 col-12" id="div1" runat="server" visible="false" style="padding: 8px 0px 8px 10px;">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Options</label>
                                            </div>
                                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                                                OnSelectedIndexChanged="rblOptions_SelectedIndexChanged" Font-Bold="true" CssClass="form-control">
                                                <asp:ListItem Value="M" Selected="True" Text="Regular"></asp:ListItem>
                                                <asp:ListItem Value="S" Text="Backlog"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Session Name</label>
                                            </div>
                                            <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Student ID</label>
                                            </div>
                                            <asp:TextBox ID="txtRollNo" runat="server" CssClass="form-control" MaxLength="18" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" Text="Show"
                                        Font-Bold="true" ValidationGroup="Show" CssClass="btn btn-outline-info" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Clear"
                                        Font-Bold="true" CssClass="btn btn-outline-danger"
                                        OnClick="btnCancel_Click" />
                                    <asp:RequiredFieldValidator ID="rfvRollNo" ControlToValidate="txtRollNo" runat="server"
                                        Display="None" ErrorMessage="Please enter Student Registration No." ValidationGroup="Show" />
                                    <asp:ValidationSummary ID="valSummery2" runat="server" DisplayMode="List" ShowMessageBox="true"
                                        ShowSummary="false" ValidationGroup="Show" />
                                </div>
                                <div class="col-12" id="tblInfo" runat="server" visible="false">
                                    <div class="row">
                                    <div class="col-lg-6 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item">
                                                <b>Student Name</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblName" runat="server" Font-Bold="True" /></a>
                                            </li>
                                            <li class="list-group-item">
                                                <b>Father Name</b><a class="pull-right"><asp:Label ID="lblFatherName" runat="server" Font-Bold="true" /></a>
                                            </li>
                                              <li class="list-group-item">
                                                <b>Student ID</b><a class="sub-label"><asp:Label ID="lblEnrollNo" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                             <li class="list-group-item">
                                            <%--<b>Degree / Branch</b><a class="pull-right"><asp:Label ID="lblBranch" runat="server" Font-Bold="True"></asp:Label></a>--%>
                                            <b>Program</b><a class="sub-label"><asp:Label ID="lblBranch" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                              <li class="list-group-item">
                                            <b>Semester</b><a class="sub-label"><asp:Label ID="lblSemester" runat="server" Font-Bold="True"></asp:Label></a>
                                        </li>
                                           
                                        </ul>
                                    </div>
                                    <div class="col-lg-6 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item">
                                                <b>Mother Name</b><a class="sub-label">
                                                    <asp:Label ID="lblMotherName" runat="server" Font-Bold="true" /></a>
                                            </li>
                                            <li class="list-group-item">
                                                <b>Faculty/School Name</b><a class="sub-label">
                                                    <asp:Label ID="lblCollege" runat="server" Font-Bold="True"></asp:Label></a>
                                            </li>
                                              <li class="list-group-item" style="display: none">
                                                <b>Reg No.</b><a class="sub-label"><asp:Label ID="lblRegNo" runat="server" Style="font-weight: 700"></asp:Label></a>
                                            </li>
                                             <li class="list-group-item">
                                        <b>Intake </b><a class="sub-label"><asp:Label ID="lblAdmBatch" runat="server" Font-Bold="True"></asp:Label>
                                        </a>
                                    </li>
                                             <li class="list-group-item" style="display:none">
                                            <b>PH. No.</b><a class="sub-label"><asp:Label ID="lblPH" runat="server" Style="font-weight: 700"></asp:Label></a>
                                        </li>
                                             <li class="list-group-item">
                                            <b>Curriculum</b><a class="sub-label"><asp:Label ID="lblScheme" runat="server" Font-Bold="True"></asp:Label></a>
                                        </li>
                                            
                                              <li class="list-group-item" style="display: none">
                                            <b>Total Subjects</b><a class="sub-label"><asp:TextBox ID="txtAllSubjects" runat="server" Enabled="false" Text="0"
                                                Style="text-align: center;"></asp:TextBox>
                                            </a>
                                        </li>
                                             <li class="list-group-item" style="display: none">
                                            <b>Total Credits</b><a class="sub-label"><asp:TextBox ID="TextBox1" runat="server" Enabled="false" Text="0"
                                                Style="text-align: center;"></asp:TextBox>
                                                <asp:HiddenField ID="hdfCredits" runat="server" Value="0" />
                                                <asp:HiddenField ID="hdfTot" runat="server" Value="0" />
                                            </a>
                                        </li>
                                        </ul>
                                    </div>
                                   
                                      </div>
                                    </div>                                
                                <div class="col-12 mt-4">
                                    <asp:ListView ID="lvCurrentSubjects" runat="server" Visible="false">
                                        <LayoutTemplate>
                                            <div>
                                                <div class="sub-heading">
	                                            <h5>Current Semester Module </h5>
                                            </div>

                                                <table id="tblCurrentSubjects" class="table table-striped table-bordered nowrap display" style="width:100%">
                                                    <thead>
                                                        <tr class="bg-light-blue">

                                                            <th>
                                                                <asp:CheckBox ID="cbHeadReg" runat="server" Text="Register" ToolTip="Register/Register all" onclick="SelectAll(this,1,'chkRegister');" />
                                                            </th>
                                                            <th>Module Code
                                                            </th>
                                                            <th>Module Name
                                                            </th>
                                                            <th>Semester
                                                            </th>                                                     
                                                            <th>Module Type
                                                            </th>
                                                             <th>Core/Elective
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
                                                    <asp:CheckBox ID="chkRegister" runat="server" Value='<%# Eval("EXAM_REGISTERED") %>' ToolTip="Click to select this subject for registration"
                                                      /> <%-- onclick="ChkHeader(1,'cbHeadReg','chkRegister');" --%>
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
                                                    <asp:Label ID="lblElective" runat="server" Text='<%# Eval("ELECT") %>'/>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>' />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                                <div class="col-12 mt-4">
                                    <asp:ListView ID="lvBacklogSubjects" runat="server">
                                        <LayoutTemplate>
                                            <div>
                                                <div class="sub-heading">
	                                                <h5>Backlog Module</h5>
                                                </div>
                                     
                                                <table id="tblBacklogSubjects" class="table table-striped table-bordered nowrap display" style="width:100%">
                                                    <thead>
                                                        <tr class="bg-light-blue">

                                                            <th>
                                                                <asp:CheckBox ID="cbHeadReg" runat="server" Text="Register" ToolTip="Register/Register all" onclick="SelectAll(this,2,'chkRegister');" />
                                                            </th>
                                                            <th>Module Code
                                                            </th>
                                                            <th>Module Name
                                                            </th>
                                                            <th>Semester
                                                            </th>
                                                            <th>Module Type
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
                                </div>

                                <div class="col-12">
                                    <asp:ListView ID="lvAuditSubjects" runat="server">
                                        <LayoutTemplate>
                                            <div>
                                                <div class="sub-heading">
	                                                <h5>Audit Module</h5>
                                                </div>
                                               

                                                <table id="tblAuditSubjects" class="table table-striped table-bordered nowrap display" style="width:100%">
                                                    <thead>
                                                        <tr class="bg-light-blue">

                                                            <th>
                                                                <asp:CheckBox ID="cbHeadReg" runat="server" Text="Register" ToolTip="Register/Register all" onclick="SelectAll(this,3,'chkRegister');" />
                                                            </th>
                                                            <th>Module Code
                                                            </th>
                                                            <th>Module Name
                                                            </th>
                                                            <th>Semester
                                                            </th>
                                                            <th>Module Type
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
                                </div>
                               <div class="col-12 btn-footer">
                                  
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-outline-info" OnClick="btnSubmit_Click"
                                            Enabled="false" ValidationGroup="SUBMIT" OnClientClick="return showConfirm();" />
                                        <asp:Button ID="btnPrintRegSlip" runat="server" Text="Registration Slip"
                                            OnClick="btnPrintRegSlip_Click" Enabled="false" CssClass="btn btn-outline-primary" />
                                        <asp:ValidationSummary ID="vssubmit" runat="server" DisplayMode="List" ShowMessageBox="true"
                                            ShowSummary="false" ValidationGroup="SUBMIT" />
                                    
                                </div>
                         
                            <div>
                                <asp:Label ID="lblmsg" runat="server" Style="color: Red; font-weight: bold" Text=""></asp:Label>
                            </div>
                               </div>
                        </div>
                        <!-- /.box-body -->
                        <div id="div2" runat="server">
                        </div>
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
