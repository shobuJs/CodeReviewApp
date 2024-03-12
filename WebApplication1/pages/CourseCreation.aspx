<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    EnableEventValidation="false"
    CodeFile="CourseCreation.aspx.cs" Inherits="ACADEMIC_CourseCreation" EnableViewState="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        /*--======= toggle switch css added by gaurav 29072021 =======--*/
        .switch input[type=checkbox] {
            height: 0;
            width: 0;
            visibility: hidden;
        }

        .switch label {
            cursor: pointer;
            width: 82px;
            height: 34px;
            background: #dc3545;
            display: block;
            border-radius: 4px;
            position: relative;
            transition: 0.35s;
            -webkit-transition: 0.35s;
            -moz-user-select: none;
            -webkit-user-select: none;
        }

            .switch label:hover {
                background-color: #c82333;
            }

            .switch label:before {
                content: attr(data-off);
                position: absolute;
                right: 0;
                font-size: 16px;
                padding: 4px 8px;
                font-weight: 400;
                color: #fff;
                transition: 0.35s;
                -webkit-transition: 0.35s;
                -moz-user-select: none;
                -webkit-user-select: none;
            }

        .switch input:checked + label:before {
            content: attr(data-on);
            position: absolute;
            left: 0;
            font-size: 16px;
            padding: 4px 15px;
            font-weight: 400;
            color: #fff;
            transition: 0.35s;
            -webkit-transition: 0.35s;
            -moz-user-select: none;
            -webkit-user-select: none;
        }

        .switch label:after {
            content: '';
            position: absolute;
            top: 1.5px;
            left: 1.7px;
            width: 10.2px;
            height: 31.5px;
            background: #fff;
            border-radius: 2.5px;
            transition: 0.35s;
            -webkit-transition: 0.35s;
            -moz-user-select: none;
            -webkit-user-select: none;
        }

        .switch input:checked + label {
            background: #28a745;
            transition: 0.35s;
            -webkit-transition: 0.35s;
            -moz-user-select: none;
            -webkit-user-select: none;
        }

            .switch input:checked + label:hover {
                background: #218838;
            }

            .switch input:checked + label:after {
                transform: translateX(68px);
                transition: 0.35s;
                -webkit-transition: 0.35s;
                -moz-user-select: none;
                -webkit-user-select: none;
            }

        td .fa-eye {
            font-size: 18px;
            color: #0d70fd;
            margin-left: 10px;
        }

        .dataTables_scrollHeadInner {
            width: max-content !important;
        }

        #ctl00_ContentPlaceHolder1_lvCourseCreation_DataPager1 a:first-child,
        #ctl00_ContentPlaceHolder1_lvCourseCreation_DataPager1 a:last-child {
            padding: 5px 10px;
            border-radius: 0%;
            background: white;
            margin: 0 0px;
            box-shadow: none;
        }

        #ctl00_ContentPlaceHolder1_lvCourseCreation_DataPager1 a {
            padding: 5px 10px;
            border-radius: 50%;
            background: white;
            margin: 0 0px;
            box-shadow: 0 1px 3px rgb(0 0 0 / 12%), 0 1px 3px rgb(0 0 0 / 24%);
        }

        #ctl00_ContentPlaceHolder1_lvCourseCreation_DataPager1 span {
            padding: 5px 10px;
            border-radius: 50%;
            background: #4183c4;
            color: #fff;
            margin: 0 0px;
            box-shadow: 0 1px 3px rgb(0 0 0 / 12%), 0 1px 3px rgb(0 0 0 / 24%);
        }
    </style>

    <style>
        #ctl00_ContentPlaceHolder1_Panel1 .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <style>
        .dynamic-nav-tabs li.active a {
            color: #255282;
            background-color: #fff;
            border-top: 3px solid #255282 !important;
            border-color: #255282 #255282 #fff !important;
        }

        .chiller-theme .nav-tabs.dynamic-nav-tabs > li.active {
            border-radius: 8px;
            background-color: transparent !important;
        }
    </style>
    <asp:UpdatePanel runat="server" ID="UPDROLE" UpdateMode="Conditional">
        <ContentTemplate>
            <%-- <asp:HiddenField ID="hidTAB" runat="server" ClientIDMode="Static" />--%>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <%--<h3 class="box-title">Course Creation </h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>
                        <asp:HiddenField ID="hdfAction" runat="server" />
                        <asp:HiddenField ID="hdnColumnNames" runat="server" />
                        <div id="Tabs" role="tabpanel">
                            <div class="box-body">
                                <div class="nav-tabs-custom">
                                    <ul class="nav nav-tabs" role="tablist">
                                        <li class="nav-item">
                                            <a class="nav-link active" data-toggle="tab" href="#tab_1" tabindex="1">Subject Creation</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" data-toggle="tab" href="#tab_2" tabindex="2" id="tab2">Import Subject
                                                Data</a>
                                        </li>

                                        <asp:HiddenField ID="hdnClientId" runat="server" Value="0" />
                                    </ul>
                                    <div class="tab-content" id="my-tab-content">
                                        <div class="tab-pane active" id="tab_1">
                                            <asp:HiddenField ID="hfdStat" runat="server" ClientIDMode="Static" />
                                            <div>
                                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updCourseCreation"
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
                                            <asp:UpdatePanel ID="updCourseCreation" runat="server">
                                                <ContentTemplate>
                                                    <div class="box-body">
                                                        <div class="col-12">
                                                            <div class="row">
                                                                <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <asp:Label ID="lblDYCollege" runat="server" Font-Bold="true"></asp:Label>
                                                                    </div>
                                                                    <asp:DropDownList ID="ddlFaculty" runat="server" TabIndex="0" CssClass="form-control"
                                                                        data-select2-enable="true"
                                                                        AppendDataBoundItems="True" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged"
                                                                        ToolTip="Please Select Faculty" AutoPostBack="true">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:HiddenField runat="server" ID="hdnEdit" Value="0" />
                                                                    <%--     <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlFaculty"
                                                                        Display="None" ErrorMessage="Please Select Faculty" InitialValue="0"
                                                                        ValidationGroup="submit" />--%>
                                                                </div>



                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <asp:Label ID="lblDYModuleCode" runat="server" Font-Bold="true"></asp:Label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtSubjectCode" runat="server" TabIndex="0" CssClass="form-control"
                                                                        ToolTip="Please Enter Module Code" />
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSubjectCode"
                                                                        Display="None" ErrorMessage="Please Enter Module Code"
                                                                        ValidationGroup="submit" />
                                                                </div>
                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <asp:Label ID="lblDYModuleName" runat="server" Font-Bold="true"></asp:Label>
                                                                    </div>
                                                                    <asp:TextBox ID="txtSubjectName" runat="server" TabIndex="0" CssClass="form-control"
                                                                        ToolTip="Please Enter Module Name" />
                                                                    <%--onkeyup="ToUpper(this)"--%>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSubjectName"
                                                                        Display="None" ErrorMessage="Please Enter Module Name"
                                                                        ValidationGroup="submit" />
                                                                </div>
                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <asp:Label ID="lblDYModuleType" runat="server" Font-Bold="true"></asp:Label>
                                                                    </div>
                                                                    <asp:DropDownList ID="ddlSubjectType" runat="server" TabIndex="0" CssClass="form-control"
                                                                        data-select2-enable="true"
                                                                        AppendDataBoundItems="True"
                                                                        ToolTip="Please Select Module Type">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlSubjectType"
                                                                        Display="None" ErrorMessage="Please Select Module type" InitialValue="0"
                                                                        ValidationGroup="submit" />
                                                                </div>


                                                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <asp:Label ID="lblDYSemester" runat="server" Font-Bold="true"></asp:Label>
                                                                    </div>
                                                                    <asp:DropDownList ID="ddlSemester" runat="server" TabIndex="0" CssClass="form-control"
                                                                        data-select2-enable="true"
                                                                        AppendDataBoundItems="True"
                                                                        ToolTip="Please Select Semester">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlSemester"
                                                                        Display="None" ErrorMessage="Please Select Semester" InitialValue="0"
                                                                        ValidationGroup="submit" />
                                                                </div>
                                                                <div class="form-group col-lg-2 col-md-3 col-6">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <asp:Label ID="lblLecUnit" runat="server" Font-Bold="true"></asp:Label>&nbsp;<span
                                                                            style="color: red; font-size: 10px; font: bold;">(Eg.1)</span>

                                                                    </div>
                                                                    <asp:TextBox ID="txtLec" runat="server" TabIndex="0" CssClass="form-control"
                                                                        ToolTip="Please Enter Credits" onblur="return FindData(this,event);" />
                                                                    <%--<asp:TextBox ID="txtLec" runat="server" TabIndex="0" CssClass="form-control"
                                                                        ToolTip="Please Enter Credits" onkeyup="return FindData(this,event);"  />--%>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtLec"
                                                                        Display="None" ErrorMessage="Please Enter Lec Unit"
                                                                        ValidationGroup="submit" />
                                                                </div>
                                                                <div class="form-group col-lg-2 col-md-3 col-6">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <asp:Label ID="lblLabUnit1" runat="server" Font-Bold="true">Lab (Hrs)</asp:Label>&nbsp;<span
                                                                            style="color: red; font-size: 10px; font: bold;">(Eg.3)</span>
                                                                    </div>
                                                                    <%--<asp:TextBox ID="txtLab" runat="server" TabIndex="0" CssClass="form-control"
                                                                        ToolTip="Please Enter Credits" />--%>
                                                                    <asp:TextBox ID="txtLab" runat="server" TabIndex="0" CssClass="form-control"
                                                                        ToolTip="Please Enter Credits" onkeyup="return FindData(this,event);" />
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtLab"
                                                                        Display="None" ErrorMessage="Please Enter Lab Unit"
                                                                        ValidationGroup="submit" />
                                                                </div>

                                                                <div class="form-group col-lg-2 col-md-3 col-6" runat="server" visible="false">
                                                                    <div class="label-dynamic">
                                                                        <%--  <sup>* </sup>--%>
                                                                        <asp:Label ID="lblDyCapacity" runat="server" Font-Bold="true"></asp:Label>
                                                                        &nbsp;<span style="color: red; font-size: 10px; font: bold;">(Eg.100)</span>

                                                                    </div>
                                                                    <asp:TextBox ID="txtCapacity" runat="server" TabIndex="0" CssClass="form-control"
                                                                        ToolTip="Please Enter Credits" onkeypress="return onlyDotsAndNumbers(this,event);" />
                                                                    <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtCapacity"
                                                Display="None" ErrorMessage="Please Enter Capacity" 
                                                ValidationGroup="submit" />--%>
                                                                </div>

                                                                <div class="form-group col-lg-2 col-md-3 col-6" runat="server">
                                                                    <div class="label-dynamic">
                                                                        <%--<sup>* </sup>--%>
                                                                        <asp:Label ID="lblDYLecture" runat="server" Font-Bold="true"></asp:Label>
                                                                        <small style="font-weight: 600;"></small><small style="color: red;">(Eg.1.11)</small>
                                                                    </div>
                                                                    <asp:TextBox ID="txtLecture" runat="server" TabIndex="0" CssClass="form-control"
                                                                        ToolTip="Please Enter Lecture" onblur="return FindData(this,event);" onkeypress="return onlyDotsAndNumbers(this,event);" />
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtLecture"
                                                                        Display="None" ErrorMessage="Please Enter Lecture" />
                                                                </div>
                                                                <div class="form-group col-lg-2 col-md-3 col-6" runat="server" visible="false">
                                                                    <div class="label-dynamic">
                                                                        <%--<sup>* </sup>--%>
                                                                        <asp:Label ID="lblDYTheory" runat="server" Font-Bold="true"></asp:Label>
                                                                        <small style="font-weight: 600;"></small><small style="color: red;">(Eg.1.11)</small>
                                                                    </div>
                                                                    <asp:TextBox ID="txtTheory" runat="server" TabIndex="8" CssClass="form-control"
                                                                        ToolTip="Please Enter Tutorial" onkeypress="return onlyDotsAndNumbers(this,event);" />
                                                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtTheory"
                                                Display="None" ErrorMessage="Please Enter Tutorial" --%>
                                                                </div>
                                                                <div class="form-group col-lg-2 col-md-3 col-6 pr-lg-0" runat="server">
                                                                    <div class="label-dynamic">
                                                                        <%--<sup>* </sup>--%>
                                                                        <asp:Label ID="lblDYPractical" runat="server" Font-Bold="true"></asp:Label>
                                                                        <small style="font-weight: 600;"></small><small style="color: red;">(Eg.1.11)</small><%--placeholder=" Eg:1.11"--%>
                                                                    </div>
                                                                    <asp:TextBox ID="txtPractical" runat="server" TabIndex="0" CssClass="form-control"
                                                                        ToolTip="Please Enter Practical" onblur="return FindData(this,event);" onkeypress="return onlyDotsAndNumbers(this,event);" />
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtPractical"
                                                                        Display="None" ErrorMessage="Please Enter Practical" />
                                                                </div>


                                                                <div class="form-group col-lg-2 col-md-3 col-6">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <asp:Label ID="lblDYCredits" runat="server" Font-Bold="true"></asp:Label>&nbsp;<span
                                                                            style="color: red; font-size: 10px; font: bold;">(Auto cal)</span>
                                                                    </div>
                                                                    <asp:TextBox ID="txtCreadits" runat="server" TabIndex="0" CssClass="form-control"
                                                                        ToolTip="Please Enter Credits" onblur="return FindData(this,event);" onkeypress="return onlyDotsAndNumbers(this,event);"
                                                                        Enabled="true" />
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtCreadits"
                                                                        Display="None" ErrorMessage="Please Enter Credits"
                                                                        ValidationGroup="submit" />
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup></sup>
                                                                        <asp:Label ID="lblDYDept" runat="server" Font-Bold="true"></asp:Label>
                                                                    </div>
                                                                    <asp:DropDownList ID="ddlDepartment" runat="server" TabIndex="0" CssClass="form-control"
                                                                        data-select2-enable="true"
                                                                        AppendDataBoundItems="True"
                                                                        ToolTip="Please Select Department" AutoPostBack="true">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>

                                                                    <%--               OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged"--%>

                                                                    <%--    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlDepartment"
                                                                        Display="None" ErrorMessage="Please Select Department" InitialValue="0"
                                                                        ValidationGroup="submit" />--%>
                                                                </div>

                                                                <div class="form-group col-lg-2 col-md-3 col-6">
                                                                    <div class="label-dynamic">
                                                                        <%--<sup>* </sup>--%>
                                                                        <asp:Label ID="lblStatus" runat="server" Font-Bold="true"></asp:Label>
                                                                    </div>
                                                                    <div class="switch form-inline">
                                                                        <input type="checkbox" id="switch" name="switch" class="switch" checked />
                                                                        <label data-on="Active" data-off="Inactive" for="switch"></label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-12 btn-footer">


                                                            <asp:LinkButton ID="btnSave" runat="server" ToolTip="Submit" ValidationGroup="submit"
                                                                CssClass="btn btn-outline-info" OnClick="btnSave_Click" TabIndex="0" ClientIDMode="Static">Submit</asp:LinkButton>


                                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                                                TabIndex="0" OnClick="btnCancel_Click" CssClass="btn btn-outline-danger" />

                                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                            <asp:HiddenField ID="HiddenField1" runat="server" />

                                                            <asp:Button ID="btnExportGridData" CssClass="btn btn-outline-info" runat="server"
                                                                Text="Export" OnClick="btnExportGridData_Click" ToolTip="Click to Export" ValidationGroup="report1" OnClientClick="getColumnNames();"/>
                                                            <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="True"
                                                                ShowSummary="False" ValidationGroup="report1" Style="text-align: center" />
                                                        </div>


                                                        <div class="col-12">
                                                            <asp:Panel ID="Panel1" runat="server">
                                                                <asp:ListView ID="lvCourseCreation" runat="server" OnPagePropertiesChanging="lvCourseCreation_PagePropertiesChanging">
                                                                    <LayoutTemplate>
                                                                        <%--<form id="form1" runat="server">--%>
                                                                        <div id="demo-grid">
                                                                            <div class="sub-heading">
                                                                                <h5>Subject List</h5>
                                                                            </div>
                                                                            <div class="mb-1">
                                                                                <label>Search:</label>
                                                                                <asp:TextBox ID="txtSearch" AutoPostBack="true" OnTextChanged="txtSearch_TextChanged"
                                                                                    runat="server"></asp:TextBox>
                                                                                <asp:Button ID="btnSearch" CssClass="btn btn-outline-info" runat="server" Text="Search"
                                                                                    OnClick="btnSearch_Click" />
                                                                                <asp:Button ID="btnExportGridData" CssClass="btn btn-outline-info" runat="server"
                                                                                    Text="Export" OnClick="btnExportGridData_Click" ToolTip="Click to Export" ValidationGroup="report1"
                                                                                    Enabled="false" Visible="false" />
                                                                                <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="True"
                                                                                    ShowSummary="False" ValidationGroup="report1" Style="text-align: center" Enabled="false"
                                                                                    Visible="false" />
                                                                                <%--<asp:Button ID="exporttable" CssClass="btn btn-outline-info" runat="server" Text="Export" />--%>

                                                                                <%--<button id="exporttable" class="btn btn-outline-info">Export</button>--%>
                                                                            </div>
                                                                            <div class="table-responsive" style="max-height: 320px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="mytable">
                                                                                    <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff!important;
                                                                                        box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                                        <tr>
                                                                                            <th style="text-align: center">Edit </th>
                                                                                            <th style="text-align: center">SrNo </th>

                                                                                            <th style="text-align: center">Subject Code </th>
                                                                                            <th style="text-align: center">Subject Name</th>
                                                                                            <th style="text-align: center">Subject Type</th>
                                                                                            <%--                         <th style="text-align: center">Semester</th>--%>
                                                                                            <th style="text-align: center">Lecture (Hrs) </th>
                                                                                            <th style="text-align: center">Lab (Hrs)</th>

                                                                                            <th style="text-align: center">Lecture (Unit)</th>
                                                                                            <%--                    <th style="text-align: center">Tutorial</th>--%>
                                                                                            <th style="text-align: center">Lab (Unit)</th>
                                                                                            <th style="text-align: center">Total Credits</th>
                                                                                            <th style="text-align: center">Department </th>
                                                                                            <th style="text-align: center">Active</th>

                                                                                        </tr>
                                                                                    </thead>
                                                                                    <tbody>
                                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                                    </tbody>
                                                                                </table>
                                                                            </div>
                                                                        </div>
                                                                        <%--</form>--%>
                                                                        <div class="col-12 pt-1 pb-2" id="Tfoot1" runat="server">
                                                                            <div class="float-right">
                                                                                <asp:DataPager ID="DataPager1" runat="server" PagedControlID="lvCourseCreation" PageSize="1000">
                                                                                    <Fields>
                                                                                        <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="false" ShowPreviousPageButton="true"
                                                                                            ShowNextPageButton="false" />
                                                                                        <asp:NumericPagerField ButtonType="Link" />
                                                                                        <asp:NextPreviousPagerField ButtonType="Link" ShowNextPageButton="true" ShowLastPageButton="false"
                                                                                            ShowPreviousPageButton="false" />
                                                                                    </Fields>
                                                                                </asp:DataPager>
                                                                            </div>
                                                                            <div class="float-left">
                                                                                <asp:DataPager ID="datapager2" runat="server" PagedControlID="lvCourseCreation" PageSize="1000">
                                                                                    <Fields>
                                                                                        <asp:TemplatePagerField>
                                                                                            <PagerTemplate>

                                                                                                <%--  <asp:label ID="Label2" runat="server"  text="Total Record :"></asp:label>
                                                                                                    <asp:label runat="server" id="lbltotalcount"  text="1236"></asp:label>--%>
                                                                                                <asp:Label ID="Label2" runat="server" Text="Showing "></asp:Label>
                                                                                                <asp:Label ID="lblStart" runat="server" Text=""></asp:Label>
                                                                                                <asp:Label ID="Label1" runat="server" Text=" to "></asp:Label>
                                                                                                <asp:Label ID="lblEnd" runat="server" Text=""></asp:Label>
                                                                                                <asp:Label ID="Label3" runat="server" Text=" of "></asp:Label>
                                                                                                <asp:Label ID="lblTotalCount" runat="server" Text=""></asp:Label>
                                                                                                <asp:Label ID="Label4" runat="server" Text=" entries"></asp:Label>
                                                                                            </PagerTemplate>
                                                                                        </asp:TemplatePagerField>
                                                                                    </Fields>
                                                                                </asp:DataPager>
                                                                            </div>
                                                                        </div>
                                                                    </LayoutTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/images/edit.gif"
                                                                                    CommandArgument='<%# Eval("COURSENO") %>' AlternateText="Edit Record"
                                                                                    OnClick="btnEdit_Click" />
                                                                            </td>
                                                                            <%-- <td>
                                                                                <asp:ImageButton ID="ntnedit" runat="server" ImageUrl="~/IMAGES/edit.png" CommandArgument='<%# Eval("COURSENO") %>'
                                                                                     AlternateText="Edit Record" ToolTip="Edit Record" OnClick="ntnedit_Click" />
                                                                            </td>--%>
                                                                            <td style="text-align: center">
                                                                                <%# Container.DataItemIndex + 1 %>
                                                                              
                                                                            </td>

                                                                            <td>
                                                                                <%# Eval("CCODE")%>
                                                                            </td>
                                                                            <td>
                                                                                <%# Eval("COURSE_NAME")%> 
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <%# Eval("SUBNAME")%>
                                                                            </td>
                                                                            <%--   <td style="text-align: center">
                                                                                <%# Eval("SEMESTERNAME")%>
                                                                            </td>--%>
                                                                            <td style="text-align: center">
                                                                                <%# Eval("LEC_UNIT")%>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <%# Eval("LAB_UNIT")%>
                                                                            </td>

                                                                            <td style="text-align: center">
                                                                                <%# Eval("LECTURE")%>
                                                                            </td>
                                                                            <%--  <td style="text-align: center">
                                                                                <%# Eval("THEORY")%>
                                                                            </td>--%>
                                                                            <td style="text-align: center">
                                                                                <%# Eval("PRACTICAL")%>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <%# Eval("CREDITS")%>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <%# Eval("DEPTNAME")%> 
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <%# Eval("ACTIVE")%>
                                                                            </td>

                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:ListView>


                                                            </asp:Panel>
                                                        </div>

                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="btnSave" />
                                                    <%--<asp:PostBackTrigger ControlID="ntnedit" />--%>

                                                    <asp:PostBackTrigger ControlID="btnExportGridData" />

                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>

                                        <div class="tab-pane fade" id="tab_2">
                                            <div>
                                                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updpnlImportData"
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
                                            <asp:UpdatePanel ID="updpnlImportData" runat="server">
                                                <ContentTemplate>
                                                    <div class="box-body">
                                                        <div class="col-12">
                                                            <div class="row">
                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label>Attach Excel File</label>
                                                                    </div>
                                                                    <asp:FileUpload ID="FUFile" runat="server" ToolTip="Select file to upload" TabIndex="0" />
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divRecords" runat="server" visible="false">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label>Already Saved Records</label>
                                                                    </div>
                                                                    <asp:Label ID="lblValue" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-12 btn-footer">
                                                            <asp:LinkButton ID="btnExport" runat="server" ValidationGroup="report" CssClass="btn btn-outline-info"
                                                                TabIndex="0"
                                                                Text="Upload Excel Sheet" ToolTip="Click to Upload" Enabled="true" OnClick="btnExport_Click"><i class="fa fa-upload" ></i> Download Blank Excel Sheet</asp:LinkButton>
                                                            <%--  <asp:Button ID="btnExport" runat="server" CssClass="btn btn-outline-info" TabIndex="2"
                                                            Text="Download Blank Excel Sheet" ToolTip="Click to download blank excel format file" OnClick="btnExport_Click" />--%>

                                                            <asp:LinkButton ID="btnUploadexcel" runat="server" ValidationGroup="report" CssClass="btn btn-outline-info"
                                                                TabIndex="0"
                                                                Text="Upload Excel Sheet" ToolTip="Click to Upload" Enabled="true" OnClick="btnUploadexcel_Click"><i class="fa fa-upload" ></i> Upload Excel</asp:LinkButton>
                                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                                                                ShowSummary="False" ValidationGroup="report" Style="text-align: center" />
                                                        </div>

                                                        <div class="form-group col-12" id="divNote" runat="server" visible="false">
                                                            <div class=" note-div">
                                                                <h5 class="heading">Note</h5>
                                                                <p>
                                                                    <i class="fa fa-star" aria-hidden="true"></i><span>Excel Sheet Data is not imported,
                                                                        Please correct following data and upload the Excel again.</span>
                                                                </p>
                                                            </div>
                                                        </div>

                                                        <div class="col-12">
                                                            <asp:ListView ID="lvStudData" runat="server">
                                                                <LayoutTemplate>
                                                                    <div class="sub-heading">
                                                                        <h5>Subject List</h5>
                                                                    </div>
                                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%"
                                                                        id="divsessionlist">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>Sr. No.</th>
                                                                                <th>Subject Name
                                                                                </th>
                                                                                <%--<th>ShortName
                                                                            </th>--%>
                                                                                <th>Subject Code
                                                                                </th>
                                                                                <th>Lecture(Hrs)
                                                                                </th>
                                                                                <th>Lab(Hrs)
                                                                                </th>
                                                                                <th>Credits
                                                                                </th>
                                                                                <th>Subject Type                                                      
                                                                                </th>
                                                                                <%--   <th>Semester                --%>                                      
                                                                                </th>
                                                                                <%-- <th>IsElective
                                                                            </th>
                                                                            <th>Scheme
                                                                            </th>--%>
                                                                                <th>Department Name
                                                                                </th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                        </tbody>
                                                                    </table>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td><%# Container.DataItemIndex +1 %></td>
                                                                        <td>
                                                                            <asp:Label ID="lblcourseName" runat="server" Text='<%# Eval("SUBJECT_NAME")%>'></asp:Label>
                                                                        </td>
                                                                        <%-- <td>
                                                                        <asp:Label ID="lblshortname" runat="server" Text='<%# Eval("SHORTNAME")%>'></asp:Label>
                                                                    </td>--%>
                                                                        <td>
                                                                            <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("SUBJECT_CODE")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblLec" runat="server" Text='<%# Eval("LECTURE_HRS")%>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblLab" runat="server" Text='<%# Eval("LAB_HRS")%>'></asp:Label>
                                                                        </td>

                                                                        <td>
                                                                            <asp:Label ID="lblcredits" runat="server" Text='<%# Eval("UNITS")%>'></asp:Label>
                                                                        </td>

                                                                        <td>
                                                                            <asp:Label ID="lblsubname" runat="server" Text='<%# Eval("SUBJECTTYPE")%>'></asp:Label>
                                                                        </td>
                                                                        <%-- <td>
                                                                            <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("SEMESTER")%>'></asp:Label>
                                                                        </td>--%>
                                                                        <%--  <td>
                                                                        <asp:Label ID="lblelec" runat="server" Text='<%# Eval("ISELECTIVE")%>'></asp:Label>
                                                                    </td>--%>
                                                                        <%-- <td>
                                                                        <asp:Label ID="lblscheme" runat="server" Text='<%# Eval("SCHEME")%>'></asp:Label>
                                                                    </td>--%>
                                                                        <td>
                                                                            <asp:Label ID="lbldept" runat="server" Text='<%# Eval("BOS_DEPT")%>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </div>

                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="btnUploadexcel" />
                                                    <asp:PostBackTrigger ControlID="btnExport" />
                                                    <%--<asp:PostBackTrigger ControlID="btnExportGridData" />--%>
                                                </Triggers>
                                            </asp:UpdatePanel>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
        <%-- <Triggers>
            <asp:PostBackTrigger ControlID="btnExportGridData" />
        </Triggers>--%>
    </asp:UpdatePanel>
    <script>
        function SetStat(val) {

            $('#switch').prop('checked', val);
        }

        var summary = "";
        $(function () {

            $('#btnSave').click(function () {

                localStorage.setItem("currentId", "#btnSave,Submit");
                debugger;
                //ShowLoader('#btnSave');


                if (summary != "") {
                    customAlert(summary);
                    summary = "";
                    return false
                }
                $('#hfdStat').val($('#switch').prop('checked'));
            });
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSave').click(function () {
                    localStorage.setItem("currentId", "#btnSave,Submit");
                    // ShowLoader('#btnSave');


                    if (summary != "") {
                        customAlert(summary);
                        summary = "";
                        return false
                    }
                    $('#hfdStat').val($('#switch').prop('checked'));
                });
            });
        });
    </script>
    <script>

        function onlyDotsAndNumbers(txt, event) {
            var charCode = (event.which) ? event.which : event.keyCode
            if (charCode == 46) {
                if (txt.value.indexOf(".") < 0)
                    return true;
                else
                    return false;
            }

            if (txt.value.indexOf(".") > 0) {
                var txtlen = txt.value.length;
                var dotpos = txt.value.indexOf(".");
                //Change the number here to allow more decimal points than 2
                if ((txtlen - dotpos) > 2)
                    return false;
            }

            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
    </script>
    <script type="text/javascript" charset="utf-8">

        function ToUpper(ctrl) {

            var t = ctrl.value;

            ctrl.value = t.toUpperCase();

        }
    </script>
    <script>
        function TabShow() {
            var tabName = "tab_2";
            $('#Tabs a[href="#' + tabName + '"]').tab('show');
            $("#Tabs a").click(function () {
                $("[id*=TabName]").val($(this).attr("href").replace("#", ""));
            });
        }
    </script>
    <script type="text/javascript">
        function FindData(txt) {
            // var rowCount = document.getElementById('PrefeTab').rows.length;
            if ($("#ctl00_ContentPlaceHolder1_hdnEdit").val() == "0") {
                var ValidChars = "0123456789.";
                var num = true;
                var mChar;
                cnt = 0

                for (i = 0; i < txt.value.length && num == true; i++) {
                    mChar = txt.value.charAt(i);

                    if (ValidChars.indexOf(mChar) == -1) {
                        num = false;
                        txt.value = '';
                        alert("Please enter Numeric values only")
                        txt.select();
                        txt.focus();
                    }
                }
                var LecHour = document.getElementById("<%=txtLec.ClientID %>").value;
                var LabHour = document.getElementById("<%=txtLab.ClientID %>").value;
                var LecUnit = parseFloat(document.getElementById("<%=txtLecture.ClientID %>").value) || 0;
                var LabUnit = parseFloat(document.getElementById("<%=txtPractical.ClientID %>").value) || 0;
                var totalCredits = document.getElementById("<%=txtCreadits.ClientID %>").value;

                if (LabHour.trim() !== "") {
                    document.getElementById("<%=txtLecture.ClientID %>").value = $("#ctl00_ContentPlaceHolder1_txtLec").val();
                }

                if (LecHour && LabHour) {
                    if (Number(LabHour) == 0 || Number(LabHour) == 1 || Number(LabHour) == 2) {
                        document.getElementById("<%=txtPractical.ClientID %>").value = 0;
                        //document.getElementById("<%=txtCreadits.ClientID %>").value = Number(Creadits);
                    }

                    else if (Number(LabHour) == 3 || Number(LabHour) == 4 || Number(LabHour) == 5) {
                        document.getElementById("<%=txtPractical.ClientID %>").value = 1;
                        //document.getElementById("<%=txtCreadits.ClientID %>").value = Number(Creadits);
                    }

                    else if (Number(LabHour) == 6 || Number(LabHour) == 7 || Number(LabHour) == 8) {
                        document.getElementById("<%=txtPractical.ClientID %>").value = 2;
                        //document.getElementById("<%=txtCreadits.ClientID %>").value = Number(Creadits);
                    }

                    else if (Number(LabHour) == 9 || Number(LabHour) == 10 || Number(LabHour) == 11) {
                        document.getElementById("<%=txtPractical.ClientID %>").value = 3;
                        //document.getElementById("<%=txtCreadits.ClientID %>").value = Number(Creadits);
                    }

                    else if (Number(LabHour) == 12 || Number(LabHour) == 13 || Number(LabHour) == 14) {
                        document.getElementById("<%=txtPractical.ClientID %>").value = 4;
                        //document.getElementById("<%=txtCreadits.ClientID %>").value = Number(Creadits);
                    }

                    else if (Number(LabHour) == 15 || Number(LabHour) == 16 || Number(LabHour) == 17) {
                        document.getElementById("<%=txtPractical.ClientID %>").value = 5;
                        //document.getElementById("<%=txtCreadits.ClientID %>").value = Number(Creadits);
                    }
                    else if (Number(LabHour) == 18 || Number(LabHour) == 19 || Number(LabHour) == 20) {
                        document.getElementById("<%=txtPractical.ClientID %>").value = 6;
                        //document.getElementById("<%=txtCreadits.ClientID %>").value = Number(Creadits);
                    }

                    else if (Number(LabHour) == 21 || Number(LabHour) == 22 || Number(LabHour) == 23) {
                        document.getElementById("<%=txtPractical.ClientID %>").value = 7;
                        //document.getElementById("<%=txtCreadits.ClientID %>").value = (Creadits);
                    }

                    else if (Number(LabHour) == 24 || Number(LabHour) == 25 || Number(LabHour) == 26) {
                        document.getElementById("<%=txtPractical.ClientID %>").value = 8;
                        //document.getElementById("<%=txtCreadits.ClientID %>").value = Number(Creadits);
                    }

                    else if (Number(LabHour) == 27 || Number(LabHour) == 28 || Number(LabHour) == 29) {
                        document.getElementById("<%=txtPractical.ClientID %>").value = 9;
                        // document.getElementById("<%=txtCreadits.ClientID %>").value = Number(Creadits);
                    }

                    else if (Number(LabHour) == 30 || Number(LabHour) == 31 || Number(LabHour) == 32) {
                        document.getElementById("<%=txtPractical.ClientID %>").value = 10;
                        //document.getElementById("<%=txtCreadits.ClientID %>").value = Number(Creadits);
                    }
                    else {
                        alert("Lab (Hrs) Limit is 30.")
                        //document.getElementById("<%=txtLab.ClientID %>").value = "";
                    }
                    document.getElementById("<%=txtCreadits.ClientID %>").value = parseFloat(document.getElementById("<%=txtLecture.ClientID %>").value) + parseFloat(document.getElementById("<%=txtPractical.ClientID %>").value);
                }

                return num;
            }
        }
    </script>
    <script type="text/javascript">
        function getColumnNames() {
            var columnNames = [];
            var table = document.getElementById('mytable');
            var headers = table.getElementsByTagName('th');

            for (var i = 0; i < headers.length; i++) {
                columnNames.push(headers[i].innerText.trim());
            }

            // Set the hidden field value
            document.getElementById('<%= hdnColumnNames.ClientID %>').value = columnNames.join(',');
        }
    </script>


    <script>
        if (window.history.replaceState) {
            window.history.replaceState(null, null, window.location.href);
        }
    </script>

</asp:Content>



