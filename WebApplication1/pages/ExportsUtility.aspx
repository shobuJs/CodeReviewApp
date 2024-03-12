<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ExportsUtility.aspx.cs" Inherits="ACADEMIC_ExportsUtility" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .with-nav-tabs.panel-default .nav-tabs > li > a,
        .with-nav-tabs.panel-default .nav-tabs > li > a:hover,
        .with-nav-tabs.panel-default .nav-tabs > li > a:focus {
            color: #fff;
        }

            .with-nav-tabs.panel-default .nav-tabs > .open > a,
            .with-nav-tabs.panel-default .nav-tabs > .open > a:hover,
            .with-nav-tabs.panel-default .nav-tabs > .open > a:focus,
            .with-nav-tabs.panel-default .nav-tabs > li > a:hover,
            .with-nav-tabs.panel-default .nav-tabs > li > a:focus {
                color: #fff;
                background-color: #3071a9;
                border-color: transparent;
            }

        .with-nav-tabs.panel-default .nav-tabs > li.active > a,
        .with-nav-tabs.panel-default .nav-tabs > li.active > a:hover,
        .with-nav-tabs.panel-default .nav-tabs > li.active > a:focus {
            color: #428bca;
            background-color: #fff;
            border-color: #428bca;
            border-bottom-color: transparent;
        }

        .with-nav-tabs.panel-default .nav-tabs > li > a {
            color: navy;
            font-family: Cambria;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpnl"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="panel with-nav-tabs panel-default">
                <div class="panel-heading">
                    <ul class="nav nav-tabs">
                        <li class="active"><a href="#studentTab" data-toggle="tab" role="tab">STUDENTS</a></li>
                        <li><a href="#employeeTab" data-toggle="tab" role="tab">EMPLOYEES</a></li>
                        <li><a href="#courseTab" data-toggle="tab" role="tab">COURSES</a></li>
                        <li><a href="#degreeTab" data-toggle="tab" role="tab">DEGREES</a></li>
                        <li><a href="#branchTab" data-toggle="tab" role="tab">BRANCHES</a></li>
                        <li><a href="#departmentTab" data-toggle="tab" role="tab">DEPARTMENTS</a></li>
                        <%--<li><a href="#courseRegTab" data-toggle="tab" role="tab">COURSE REGISTRATION</a></li>--%>
                        <li><a href="#registrationTab" data-toggle="tab" role="tab">COURSE REGISTRATIONS</a></li>
                    </ul>
                </div>
                <div class="panel-body">
                    <asp:UpdatePanel ID="updpnl" runat="server">
                        <ContentTemplate>
                            <div class="tab-content">
                                <div class="tab-pane fade in active" id="studentTab">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="panel panel-default">
                                                <div class="panel-heading">
                                                    <h5 class="box-title" style="font-size: 18px; color: black">Export Students Data</h5>
                                                </div>
                                                <div class="panel-body">
                                                    <div style="color: Red; font-weight: bold">
                                                        &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                                                    </div>
                                                    <br />
                                                    <div class="form-group col-md-4">
                                                        <label><span style="color: red;">*</span> Degree</label>
                                                        <asp:DropDownList ID="ddlDegreeStud" runat="server" AppendDataBoundItems="true" Font-Bold="True" TabIndex="1" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDegreeStud_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvDegreeStud" runat="server" ControlToValidate="ddlDegreeStud"
                                                            Display="None" ErrorMessage="Please Select Degree." InitialValue="0" ValidationGroup="Students"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <label><span style="color: red;">*</span> Branch</label>
                                                        <asp:DropDownList ID="ddlBranchStud" runat="server" AppendDataBoundItems="true" Font-Bold="True" TabIndex="2" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlBranchStud_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvBranchStud" runat="server" ControlToValidate="ddlBranchStud"
                                                            Display="None" ErrorMessage="Please Select Branch." InitialValue="0" ValidationGroup="Students"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <label><%--<span style="color: red;">*</span>--%> Admission Batch</label>
                                                        <asp:DropDownList ID="ddlAdmBatchStud" runat="server" AppendDataBoundItems="true" Font-Bold="True" TabIndex="3" CssClass="form-control">
                                                        </asp:DropDownList>
                                                        <%-- <asp:RequiredFieldValidator ID="rfvAdmBatchStud" runat="server" ControlToValidate="ddlAdmBatchStud"
                                                            Display="None" ErrorMessage="Please Select Admission Batch." InitialValue="0" ValidationGroup="Students"></asp:RequiredFieldValidator>--%>
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <label><span style="color: red;">*</span> Scheme</label>
                                                        <asp:DropDownList ID="ddlSchemeStud" runat="server" AppendDataBoundItems="true" Font-Bold="True" TabIndex="4" CssClass="form-control">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvSchemeStud" runat="server" ControlToValidate="ddlSchemeStud"
                                                            Display="None" ErrorMessage="Please Select Scheme." InitialValue="0" ValidationGroup="Students"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <label><span style="color: red;">*</span> Semester</label>
                                                        <asp:DropDownList ID="ddlSemesterStud" runat="server" AppendDataBoundItems="true" Font-Bold="True" TabIndex="5" CssClass="form-control">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvSemesterStud" runat="server" ControlToValidate="ddlSemesterStud"
                                                            Display="None" ErrorMessage="Please Select Semester." InitialValue="0" ValidationGroup="Students"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <label><%--<span style="color: red;">*</span>--%> Section</label>
                                                        <asp:DropDownList ID="ddlSectionStud" runat="server" AppendDataBoundItems="true" Font-Bold="True" TabIndex="6" CssClass="form-control">
                                                        </asp:DropDownList>

                                                    </div>
                                                </div>
                                                <div class="box-footer">
                                                    <p class="text-center">
                                                        <asp:Button ID="btnExportStudents" runat="server" Text="Export To Excel" CssClass="btn btn-outline-info" ValidationGroup="Students"
                                                            TabIndex="6" OnClick="btnExportStudents_Click" />
                                                        <asp:Button ID="btnCancelStud" runat="server" Text="Clear"
                                                            CausesValidation="False" CssClass="btn btn-outline-danger" TabIndex="7" OnClick="btnCancelStud_Click" />
                                                        <asp:Label ID="lblStudentsError" runat="server" SkinID="Errorlbl"></asp:Label>
                                                        <asp:ValidationSummary ID="vsStudents" runat="server" ValidationGroup="Students"
                                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                    </p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="tab-pane fade" id="employeeTab">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="panel panel-default">
                                                <div class="panel-heading">
                                                    <h5 class="box-title" style="font-size: 18px; color: black">Export Employee Data</h5>
                                                </div>
                                                <div class="panel-body">
                                                    <div style="color: Red; font-weight: bold">
                                                        &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                                                    </div>
                                                    <br />
                                                    <div class="form-group col-md-4">
                                                        <label><span style="color: red;">*</span> Department</label>
                                                        <asp:DropDownList ID="ddlDepartmentEmp" runat="server" AppendDataBoundItems="true" Font-Bold="True" TabIndex="1" CssClass="form-control" OnSelectedIndexChanged="ddlDeptCourse_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvDepartmentEmp" runat="server" ControlToValidate="ddlDepartmentEmp"
                                                            Display="None" ErrorMessage="Please Select Department." InitialValue="0" ValidationGroup="Employees"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="box-footer">
                                                    <p class="text-center">
                                                        <asp:Button ID="btnExportEmployees" runat="server" Text="Export To Excel" CssClass="btn btn-outline-info" ValidationGroup="Employees"
                                                            TabIndex="2" OnClick="btnExportEmployees_Click" />
                                                        <asp:Button ID="btnCancelEmployees" runat="server" Text="Clear"
                                                            CausesValidation="False" CssClass="btn btn-outline-danger" TabIndex="3" OnClick="btnCancelEmployees_Click" />
                                                        <asp:ValidationSummary ID="vsEmployees" runat="server" ValidationGroup="Employees"
                                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                        <asp:Label ID="lblEmployeeError" runat="server" SkinID="Errorlbl"></asp:Label>
                                                    </p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="tab-pane fade" id="courseTab">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="panel panel-default">
                                                <div class="panel-heading">
                                                    <h5 class="box-title" style="font-size: 18px; color: black">Export Course Data</h5>
                                                </div>
                                                <div class="panel-body">
                                                    <div style="color: Red; font-weight: bold">
                                                        &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                                                    </div>
                                                    <br />
                                                    <div class="form-group col-md-4">
                                                        <label><span style="color: red;">*</span> Department</label>
                                                        <asp:DropDownList ID="ddlDeptCourse" runat="server" AppendDataBoundItems="true" Font-Bold="True" TabIndex="1" CssClass="form-control" OnSelectedIndexChanged="ddlDeptCourse_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvDepartmentCousre" runat="server" ControlToValidate="ddlDeptCourse"
                                                            Display="None" ErrorMessage="Please Select Department." InitialValue="0" ValidationGroup="course"></asp:RequiredFieldValidator>
                                                    </div>

                                                </div>
                                                <div class="box-footer">
                                                    <p class="text-center">
                                                        <asp:Button ID="btnExportCourse" runat="server" Text="Export To Excel" CssClass="btn btn-outline-info" ValidationGroup="course"
                                                            TabIndex="2" OnClick="btnExportCourse_Click" />
                                                        <asp:Button ID="btnCancelCourse" runat="server" Text="Clear"
                                                            CausesValidation="False" CssClass="btn btn-outline-danger" TabIndex="3" OnClick="btnCancelCourse_Click" />
                                                        <asp:ValidationSummary ID="vsCourse" runat="server" ValidationGroup="course"
                                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                        <div class="col-md-12">
                                                            <asp:Label ID="lblStatus" runat="server" SkinID="Errorlbl"></asp:Label>
                                                        </div>
                                                    </p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="tab-pane fade" id="degreeTab">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="panel panel-default">
                                                <div class="panel-heading">
                                                    <h5 class="box-title" style="font-size: 18px; color: black">Export Degree Data</h5>
                                                </div>
                                                <div class="panel-body">
                                                    <div class="form-group col-md-4">
                                                    </div>
                                                </div>
                                                <div class="box-footer">
                                                    <p class="text-center">
                                                        <asp:Button ID="btnExportDegree" runat="server" Text="Export To Excel" CssClass="btn btn-outline-info" ValidationGroup="Degree"
                                                            TabIndex="2" OnClick="btnExportDegree_Click" />
                                                        <%--<asp:Button ID="Button2" runat="server" Text="Clear"
                                                                CausesValidation="False" CssClass="btn btn-outline-danger" TabIndex="3" />--%>
                                                        <asp:Label ID="lblErrorDeg" runat="server" SkinID="Errorlbl"></asp:Label>
                                                    </p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="tab-pane fade" id="branchTab">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="panel panel-default">
                                                <div class="panel-heading">
                                                    <h5 class="box-title" style="font-size: 18px; color: black">Export Branch Data</h5>
                                                </div>
                                                <div class="panel-body">
                                                    <div class="form-group col-md-4">
                                                    </div>
                                                </div>
                                                <div class="box-footer">
                                                    <p class="text-center">
                                                        <asp:Button ID="btnExportBranch" runat="server" Text="Export To Excel" CssClass="btn btn-outline-info" ValidationGroup="Degree"
                                                            TabIndex="2" OnClick="btnExportBranch_Click" />
                                                        <%--<asp:Button ID="Button2" runat="server" Text="Clear"
                                                                CausesValidation="False" CssClass="btn btn-outline-danger" TabIndex="3" />--%>
                                                        <asp:Label ID="lblErrorBranch" runat="server" SkinID="Errorlbl"></asp:Label>
                                                    </p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="tab-pane fade" id="departmentTab">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="panel panel-default">
                                                <div class="panel-heading">
                                                    <h5 class="box-title" style="font-size: 18px; color: black">Export Department Data</h5>
                                                </div>
                                                <div class="panel-body">
                                                    <div class="form-group col-md-4">
                                                    </div>
                                                </div>
                                                <div class="box-footer">
                                                    <p class="text-center">
                                                        <asp:Button ID="btnExportDepartment" runat="server" Text="Export To Excel" CssClass="btn btn-outline-info" ValidationGroup="Degree"
                                                            TabIndex="2" OnClick="btnExportDepartment_Click" />
                                                        <%--<asp:Button ID="Button2" runat="server" Text="Clear"
                                                                CausesValidation="False" CssClass="btn btn-outline-danger" TabIndex="3" />--%>
                                                        <asp:Label ID="lblDepartmentError" runat="server" SkinID="Errorlbl"></asp:Label>
                                                    </p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="tab-pane fade" id="registrationTab">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="panel panel-default">
                                                <div class="panel-heading">
                                                    <h5 class="box-title" style="font-size: 18px; color: black">Export Course Registration Data</h5>
                                                </div>
                                               <div class="panel-body">
                                                      <div style="color: Red; font-weight: bold">
                                                        &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                                                    </div>
                                                    <br />
                                                    
                                                    <div class="form-group col-md-4">
                                                        <label><span style="color: red;">*</span> Session</label>
                                                        <asp:DropDownList ID="ddlSessionReg" runat="server" AppendDataBoundItems="true" Font-Bold="True" TabIndex="1" CssClass="form-control" >
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvSessionReg" runat="server" ControlToValidate="ddlSessionReg"
                                                            Display="None" ErrorMessage="Please Select Session." InitialValue="0" ValidationGroup="RegistrationGrp"></asp:RequiredFieldValidator>
                                                    </div>
                                                     <div class="form-group col-md-4">
                                                        <label><span style="color: red;">*</span> Degree</label>
                                                        <asp:DropDownList ID="ddlDegreeReg" runat="server" AppendDataBoundItems="true" Font-Bold="True" TabIndex="2" CssClass="form-control" AutoPostBack="true"  OnSelectedIndexChanged="ddlDegreeReg_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvDegreeReg" runat="server" ControlToValidate="ddlDegreeReg"
                                                            Display="None" ErrorMessage="Please Select Degree." InitialValue="0" ValidationGroup="RegistrationGrp"></asp:RequiredFieldValidator>

                                                         
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <label><span style="color: red;">*</span> Branch</label>
                                                        <asp:DropDownList ID="ddlBranchReg" runat="server" AppendDataBoundItems="true" Font-Bold="True" TabIndex="3" AutoPostBack="true" CssClass="form-control" 
                                                           OnSelectedIndexChanged="ddlBranchReg_SelectedIndexChanged" >
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvBranchReg" runat="server" ControlToValidate="ddlBranchReg"
                                                            Display="None" ErrorMessage="Please Select Branch." InitialValue="0" ValidationGroup="RegistrationGrp"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <label><span style="color: red;">*</span> Scheme</label>
                                                        <asp:DropDownList ID="ddlSchemeReg" runat="server" AppendDataBoundItems="true" Font-Bold="True" TabIndex="4" CssClass="form-control" >
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvSchemeReg" runat="server" ControlToValidate="ddlSchemeReg"
                                                            Display="None" ErrorMessage="Please Select Scheme." InitialValue="0" ValidationGroup="RegistrationGrp"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <label><span style="color: red;">*</span> Semester</label>
                                                        <asp:DropDownList ID="ddlSemesterReg" runat="server" AppendDataBoundItems="true" Font-Bold="True" TabIndex="5" CssClass="form-control" >
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvSemesterReg" runat="server" ControlToValidate="ddlSemesterReg"
                                                            Display="None" ErrorMessage="Please Select Semester." InitialValue="0" ValidationGroup="RegistrationGrp"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-md-4">
                                                        <label><%--<span style="color: red;">*</span> --%>Section</label>
                                                        <asp:DropDownList ID="ddlSectionReg" runat="server" AppendDataBoundItems="true" Font-Bold="True" TabIndex="6" CssClass="form-control" >
                                                        </asp:DropDownList>                                                        
                                                    </div>
                                                </div>
                                                <div class="box-footer">
                                                    <p class="text-center">
                                                        <asp:Button ID="btnExportRegistration" runat="server" Text="Export To Excel" 
                                                            CssClass="btn btn-outline-info" ValidationGroup="RegistrationGrp"
                                                            TabIndex="7" onclick="btnExportRegistration_Click" />
                                                        <asp:Button ID="btnClearReg" runat="server" Text="Clear"
                                                                CausesValidation="False" CssClass="btn btn-outline-danger" TabIndex="8" 
                                                            onclick="btnClearReg_Click" />
                                                        <asp:Label ID="lblStatusReg" runat="server" SkinID="Errorlbl"></asp:Label>
                                                    </p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>     
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnExportCourse" />
                            <asp:PostBackTrigger ControlID="btnExportDegree" />
                            <asp:PostBackTrigger ControlID="btnExportBranch" />
                            <asp:PostBackTrigger ControlID="btnExportDepartment" />
                            <asp:PostBackTrigger ControlID="btnExportStudents" />
                            <asp:PostBackTrigger ControlID="btnExportEmployees" />
                            <asp:PostBackTrigger ControlID="btnExportRegistration" />
                            <asp:PostBackTrigger ControlID="ddlDegreeStud" />
                            <asp:PostBackTrigger ControlID="ddlAdmBatchStud" />
                            <asp:PostBackTrigger ControlID="ddlDegreeReg" />
                            <asp:PostBackTrigger ControlID="ddlBranchReg" />
                            
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>

    </div>
    <div runat="server" id="divMsg"></div>

    <script type="text/javascript">
        //$(document).ready(function () {
        //    $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
        //        var target = $(e.target).attr("href")
        //        $(".loader-area, .loader").css("display", "block");
        //        if (target == "#courseTab") {
        //        } else if (target == "#degreeTab") {
        //        } else if (target == "#branchTab") {
        //        } else if (target == "#departmentTab") {
        //        }
        //        $(".loader-area, .loader").fadeOut('slow');
        //    });
        //});
    </script>
</asp:Content>

