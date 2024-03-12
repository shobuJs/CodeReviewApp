<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Aptitute_Test_Mark_Entry.aspx.cs" Inherits="ACADEMIC_Aptitute_Test_Mark_Entry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="<%=Page.ResolveClientUrl("~/plugins/Sematic/CSS/myfilterOpt.css") %>" rel="stylesheet" />
    <link href="<%=Page.ResolveClientUrl("~/plugins/Sematic/CSS/semantic.min.css") %>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/Sematic/JS/semantic.min.js")%>"></script>

    <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
    <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/FileSaver.js/2014-11-29/FileSaver.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.12.13/xlsx.full.min.js"></script>

    <style>
        #getdate, #getdateexam {
            border-top: none;
            border-left: none;
            border-right: none;
            border-bottom: 1px solid #ccc;
            height: 30px !important;
        }

        #ctl00_ContentPlaceHolder1_divapti .dataTables_scrollHeadInner,
        #ctl00_ContentPlaceHolder1_divallotment .dataTables_scrollHeadInner {
            width: max-content !important;
        }

        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <style>
        .sea-rch i {
            color: #5b5b5b;
            cursor: pointer;
        }

            .sea-rch i:hover {
                color: red;
            }
    </style>
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
    </style>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="nav-tabs-custom mt-2 col-12 pb-4" id="myTabContent">

                    <ul class="nav nav-tabs" role="tablist">
                        <li class="nav-item">
                            <a class="nav-link active" data-toggle="tab" href="#tab_1" tabindex="1">Aptitude Test Mark Entry</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" data-toggle="tab" href="#tab_2" tabindex="2">Aptitude Manual Mark Entry</a>
                        </li>

                    </ul>
                    <div class="tab-content">
                        <%-- Aptitude Test Mark Entry Tab Start --%>

                        <div class="tab-pane active" id="tab_1">
                            <div>
                                <asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="updapptitutetest"
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

                            <asp:UpdatePanel ID="updapptitutetest" runat="server">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-md-12 col-sm-12 col-12">
                                            <div class="sub-heading mt-3">
                                                <h5>Aptitude Test Mark Entry</h5>
                                            </div>
                                            <div class="box-body">
                                                <div class="col-12">


                                                    <div class="col-md-12">
                                                        <div class="form-group col-md-8">
                                                            <asp:RadioButtonList ID="rdbFilter" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rdbFilter_SelectedIndexChanged" RepeatColumns="8" RepeatDirection="Horizontal">
                                                                <asp:ListItem Value="1"><span style="padding-left:5px">Download For Test Prep Data </span></asp:ListItem>
                                                                <asp:ListItem Value="2"><span style="padding-left:5px">Exam Marks Upload From Test Prep </span></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </div>


                                                    <div class="row">
                                                        <div id="Div1" class="form-group col-md-5" runat="server" visible="false">
                                                            <fieldset class="fieldset" style="text-align: center;">
                                                                <legend class="legend">Download Format</legend>
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:LinkButton ID="lbExcelFormat" runat="server" OnClick="lbExcelFormat_Click1" TabIndex="1" Font-Underline="true"
                                                                                ToolTip="Click Here For Downloading Sample Format" Style="font-weight: bold;" CssClass="stylink">
                                                                            <span style="color:green;">Pre-requisite excel format for upload</span></asp:LinkButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </fieldset>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="intake" visible="false">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <asp:Label ID="lblIntakeapt" runat="server" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlintakeapti" runat="server" TabIndex="3" CssClass="form-control select2 select-click"
                                                                AppendDataBoundItems="True" OnSelectedIndexChanged="ddlintakeapti_SelectedIndexChanged" AutoPostBack="true"
                                                                ToolTip="Please Select Intake">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlintakeapti"
                                                                Display="None" ErrorMessage="Please Select Intake" InitialValue="0"
                                                                ValidationGroup="submitapti" />

                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divintaketwo" visible="false">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <asp:Label ID="lblIntake" runat="server" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlintakeprep" runat="server" TabIndex="3" CssClass="form-control select2 select-click"
                                                                AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlintakeprep_SelectedIndexChanged"
                                                                ToolTip="Please Select Intake">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlintakeprep"
                                                                Display="None" ErrorMessage="Please Select Intake" InitialValue="0"
                                                                ValidationGroup="submitapti" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlintakeprep"
                                                                Display="None" ErrorMessage="Please Select Intake" InitialValue="0"
                                                                ValidationGroup="confirmed" />

                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divfaculty" visible="false">
                                                            <div class="label-dynamic">
                                                                <%--<sup>* </sup>--%>
                                                                <asp:Label ID="lblfaculty" runat="server" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlfaculty" runat="server" TabIndex="3" CssClass="form-control select2 select-click"
                                                                AppendDataBoundItems="True" OnSelectedIndexChanged="ddlfaculty_SelectedIndexChanged" AutoPostBack="true"
                                                                ToolTip="Please Select Faculty/School Name">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlfaculty"
                                                                        Display="None" ErrorMessage="Please Select Faculty/School Name" InitialValue="0"
                                                                        ValidationGroup="submitapti" />--%>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divdesfirst" visible="false">
                                                            <div class="label-dynamic">
                                                                <%--<sup>* </sup>--%>
                                                                <asp:Label ID="lbldesc" runat="server" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <asp:ListBox ID="ddldesrdiofir" runat="server" CssClass="form-control multi-select-demo" TabIndex="4"
                                                                SelectionMode="Multiple" AppendDataBoundItems="true"></asp:ListBox>

                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divMultiselectdate" visible="false">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                From/To Date Filter
                                                            </div>
                                                            <div id="pickerNew" class="form-control" tabindex="6">
                                                                <i class="fa fa-calendar"></i>&nbsp;
                                                                            <span id="dateNew"></span>
                                                                <i class="fa fa-angle-down" aria-hidden="true" style="float: right; padding-top: 4px; font-weight: bold;"></i>
                                                            </div>
                                                            <asp:HiddenField ID="hdfGetFromDate" runat="server" />
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divdesp" visible="false">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <asp:Label ID="lbldes" runat="server" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <asp:ListBox ID="ddldes" runat="server" CssClass="form-control multi-select-demo" OnSelectedIndexChanged="ddldesrdiofir_SelectedIndexChanged" TabIndex="4"
                                                                SelectionMode="Multiple" AppendDataBoundItems="true" AutoPostBack="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:ListBox>

                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divprogram" visible="false">
                                                            <div class="label-dynamic">
                                                                <%--<sup>* </sup>--%>
                                                                <asp:Label ID="lblprogram" runat="server" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlprgm" runat="server" TabIndex="1" CssClass="form-control select2 select-click"
                                                                AppendDataBoundItems="True" OnSelectedIndexChanged="ddlprgm_SelectedIndexChanged" AutoPostBack="true"
                                                                ToolTip="Please Select Program">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <%--    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlprgm"
                                                                        Display="None" ErrorMessage="Please Select Program" InitialValue="0"
                                                                        ValidationGroup="submitapti" />--%>
                                                        </div>


                                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="divdegree" visible="false">
                                                            <div class="label-dynamic">
                                                                <%--   <sup>* </sup>--%>
                                                                <asp:Label ID="lblDYDegree" runat="server" Font-Bold="true"></asp:Label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlDegree" runat="server" TabIndex="2" CssClass="form-control select2 select-click"
                                                                AppendDataBoundItems="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" AutoPostBack="true"
                                                                ToolTip="Please Select Degree">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <%--    <asp:RequiredFieldValidator ID="RequiredFieldValidator81" runat="server" ControlToValidate="ddlDegree"
                                                                        Display="None" ErrorMessage="Please Select Degree" InitialValue="0"
                                                                        ValidationGroup="submitapti" />--%>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="FileUpload" visible="false">
                                                            <sup>*</sup>
                                                            <asp:Label ID="lblDYUploadExcelFile" runat="server" Font-Bold="true"></asp:Label>
                                                            <asp:FileUpload ID="FileUpload1" runat="server" ToolTip="Please Select file to Import" TabIndex="3" />
                                                            <asp:RequiredFieldValidator ID="rfvintake" runat="server" ControlToValidate="FileUpload1"
                                                                Display="None" ErrorMessage="Please select file to upload." ValidationGroup="submitapti"
                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                            <%-- <asp:RegularExpressionValidator ID="revIntake" ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.xls|.xlsx)$"
                                                                        ControlToValidate="FileUpload1" runat="server" ValidationGroup="submitapti" ErrorMessage="Please select a valid excel file."
                                                                        Display="None" SetFocusOnError="true" />--%>
                                                        </div>

                                                        <div class="form-group col-md-12">
                                                            <asp:Label ID="lblTotalMsg" Style="font-weight: bold; color: red;" runat="server" TabIndex="3"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="box-footer">
                                                    <p class="text-center">

                                                        <asp:Button ID="btnexcelformat" runat="server" TabIndex="7"
                                                            Text="PreFormat Data Excel Sheet" OnClick="btnexcelformat_Click"
                                                            CssClass="btn btn-outline-info" ToolTip="Click Download PreFormat Data" Visible="false" />

                                                        <asp:Button ID="btnDownload" runat="server" TabIndex="6"
                                                            Text="TestPrep Data Excel Sheet" OnClick="btnDownload_Click"
                                                            CssClass="btn btn-outline-info" ToolTip="Click Download TestPrep Data" ValidationGroup="submitapti" Visible="false" />

                                                        <asp:Button ID="btnMarksPreData" runat="server" TabIndex="7" Visible="false"
                                                            Text="Download Excel" OnClick="btnMarksPreData_Click" CssClass="btn btn-outline-info" ToolTip="Click Download Excel" />

                                                        <asp:Button ID="btnView" runat="server" TabIndex="8" Visible="false" OnClick="btnView_Click"
                                                            Text="View" CssClass="btn btn-outline-info" ToolTip="Click Download Excel" />

                                                        <asp:Button ID="btnUpload" runat="server" ValidationGroup="submitapti" TabIndex="9" Visible="false"
                                                            Text="Upload & Verify Data" OnClick="btnUpload_Click" CssClass="btn btn-outline-info" ToolTip="Click to Upload & Verify Data" />
                                                        <%-- <asp:Button ID="btnverify" runat="server" ValidationGroup="submitapti" TabIndex="3" Visible="false" 
                                                                    Text="Verify Data" OnClick="btnverify_Click"  CssClass="btn btn-outline-info" ToolTip="Click to Verify Data" />--%>
                                                        <asp:Button ID="btnconfirmed" runat="server" ValidationGroup="confirmed" TabIndex="10" Visible="false"
                                                            Text="Confirmed" CssClass="btn btn-outline-info" ToolTip="Click to Confirmed Data" OnClick="btnconfirmed_Click" />

                                                        <asp:Button ID="btncanceltest" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                                            TabIndex="11" CssClass="btn btn-outline-danger" Visible="false" OnClick="btncanceltest_Click" />
                                                        <asp:ValidationSummary ID="validationsummary6" runat="server" ValidationGroup="confirmed" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                        <p>
                                                            &nbsp;<asp:ValidationSummary ID="validationsummary5" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="submitapti" />
                                                            <p>
                                                            </p>
                                                        </p>


                                                    </p>
                                                </div>
                                                <div class="col-md-12">
                                                    <div id="divapti" runat="server" visible="false">
                                                        <asp:Panel ID="Panel5" runat="server">
                                                            <asp:ListView ID="lvapti" runat="server">
                                                                <LayoutTemplate>
                                                                    <div>
                                                                        <div class="sub-heading">
                                                                            <h5>Verify Excel Records</h5>
                                                                        </div>


                                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%; overflow-x: hidden; overflow-y: auto;">
                                                                            <%--<table class="table table-striped table-bordered nowrap display" style="width: 100%" id="myTable">--%>
                                                                            <thead class="bg-light-blue">
                                                                                <tr id="trRow">
                                                                                    <th class="text-center">Sr.No. </th>
                                                                                    <th class="text-center">Exam Name </th>
                                                                                    <th class="text-center">SubjectName</th>
                                                                                    <th class="text-center">RollNo</th>
                                                                                    <th class="text-center">PRNNo </th>
                                                                                    <th class="text-center">CandidateName</th>
                                                                                    <th class="text-center">MobileNo</th>
                                                                                    <th class="text-center">MaxMarks</th>
                                                                                    <th class="text-center">MarksObtained</th>
                                                                                    <th class="text-center">ExamSubmitDate</th>
                                                                                    <th class="text-center">ExamStatus</th>
                                                                                    <th class="text-center">General</th>
                                                                                    <th class="text-center">English</th>

                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                                <tr id="itemPlaceholder" runat="server" />
                                                                            </tbody>
                                                                        </table>
                                                                    </div>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td class="text-center"><%#Container.DataItemIndex+1 %></td>

                                                                        <td class="text-center"><%# Eval("EXAMNAME") %> </td>
                                                                        <td class="text-center"><%# Eval("SUBJECTNAME") %> </td>
                                                                        <td class="text-center"><%# Eval("ROLLNO") %> </td>
                                                                        <td class="text-center"><%# Eval("PRNNO") %></td>
                                                                        <td class="text-center"><%# Eval("NAME") %></td>
                                                                        <td class="text-center"><%# Eval("MOBILENO") %></td>
                                                                        <td class="text-center"><%# Eval("MAXMARKS") %></td>
                                                                        <td class="text-center"><%# Eval("MARKSOBTAINED") %></td>
                                                                        <td class="text-center"><%# Eval("EXAMSUBMITDATE") %></td>
                                                                        <td class="text-center"><%# Eval("EXAMSTATUS") %></td>
                                                                        <td class="text-center"><%# Eval("GENERAL") %></td>
                                                                        <td class="text-center"><%# Eval("ENGLISH_MARKS") %></td>

                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </asp:Panel>
                                                    </div>

                                                </div>
                                                <div class="col-md-12">
                                                    <div id="DivViewData" runat="server" visible="false">
                                                        <asp:Panel ID="Panel1" runat="server">
                                                            <asp:ListView ID="ViewData" runat="server">
                                                                <LayoutTemplate>
                                                                    <div>
                                                                        <div class="sub-heading">
                                                                            <h5>Aptitude Test Mark Entry Records</h5>
                                                                        </div>

                                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                            <thead class="bg-light-blue">
                                                                                <tr id="trRow">
                                                                                    <th class="text-center">Sr.No. </th>
                                                                                    <th class="text-center">Exam Name </th>
                                                                                    <th class="text-center">SubjectName</th>
                                                                                    <th class="text-center">RollNo</th>
                                                                                    <th class="text-center">PRNNo </th>
                                                                                    <th class="text-center">CandidateName</th>
                                                                                    <th class="text-center">MobileNo</th>
                                                                                    <th class="text-center">MaxMarks</th>
                                                                                    <th class="text-center">MarksObtained</th>
                                                                                    <th class="text-center">ExamSubmitDate</th>
                                                                                    <th class="text-center">ExamStatus</th>
                                                                                    <th class="text-center">General</th>
                                                                                    <th class="text-center">English</th>

                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                                <tr id="itemPlaceholder" runat="server" />
                                                                            </tbody>
                                                                        </table>
                                                                    </div>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td class="text-center"><%#Container.DataItemIndex+1 %></td>

                                                                        <td class="text-center"><%# Eval("EXAMNAME") %> </td>
                                                                        <td class="text-center"><%# Eval("SUBJECTNAME") %> </td>
                                                                        <td class="text-center"><%# Eval("ROLLNO") %> </td>
                                                                        <td class="text-center"><%# Eval("PRNNO") %></td>
                                                                        <td class="text-center"><%# Eval("NAME") %></td>
                                                                        <td class="text-center"><%# Eval("MOBILENO") %></td>
                                                                        <td class="text-center"><%# Eval("MAXMARKS") %></td>
                                                                        <td class="text-center"><%# Eval("MARKSOBTAINED") %></td>
                                                                        <td class="text-center"><%# Eval("EXAMSUBMITDATE") %></td>
                                                                        <td class="text-center"><%# Eval("EXAMSTATUS") %></td>
                                                                        <td class="text-center"><%# Eval("GENERAL") %></td>
                                                                        <td class="text-center"><%# Eval("ENGLISH_MARKS") %></td>

                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </asp:Panel>
                                                    </div>

                                                </div>
                                                <div class="col-md-12">
                                                    <div id="divuploadexcel" runat="server" visible="false">
                                                        <asp:Panel ID="Panel7" runat="server">
                                                            <asp:ListView ID="lvuploexcel" runat="server">
                                                                <LayoutTemplate>
                                                                    <div>
                                                                        <div class="sub-heading">
                                                                            <h5>Confirmed Excel Records</h5>
                                                                        </div>
                                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%; overflow-x: hidden; overflow-y: auto;">
                                                                            <%--<table class="table table-striped table-bordered nowrap display" style="width: 100%" id="myTable">--%>
                                                                            <thead class="bg-light-blue">
                                                                                <tr id="trRow">
                                                                                    <th class="text-center">Sr.No. </th>
                                                                                    <th class="text-center">Exam Name </th>
                                                                                    <th class="text-center">SubjectName</th>
                                                                                    <th class="text-center">RollNo</th>
                                                                                    <th class="text-center">PRNNo </th>
                                                                                    <th class="text-center">CandidateName</th>
                                                                                    <th class="text-center">MobileNo</th>
                                                                                    <th class="text-center">MaxMarks</th>
                                                                                    <th class="text-center">MarksObtained</th>
                                                                                    <th class="text-center">ExamSubmitDate</th>
                                                                                    <th class="text-center">ExamStatus</th>
                                                                                    <th class="text-center">General</th>
                                                                                    <th class="text-center">English</th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                                <tr id="itemPlaceholder" runat="server" />
                                                                            </tbody>
                                                                        </table>
                                                                    </div>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td class="text-center"><%#Container.DataItemIndex+1 %></td>

                                                                        <td class="text-center"><%# Eval("EXAMNAME") %> </td>
                                                                        <td class="text-center"><%# Eval("SUBJECTNAME") %> </td>
                                                                        <td class="text-center"><%# Eval("ROLLNO") %> </td>
                                                                        <td class="text-center"><%# Eval("PRNNO") %></td>
                                                                        <td class="text-center"><%# Eval("NAME") %></td>
                                                                        <td class="text-center"><%# Eval("MOBILENO") %></td>
                                                                        <td class="text-center"><%# Eval("MAXMARKS") %></td>
                                                                        <td class="text-center"><%# Eval("MARKSOBTAINED") %></td>
                                                                        <td class="text-center"><%# Eval("EXAMSUBMITDATE") %></td>
                                                                        <td class="text-center"><%# Eval("EXAMSTATUS") %></td>
                                                                        <td class="text-center"><%# Eval("GENERAL") %></td>
                                                                        <td class="text-center"><%# Eval("ENGLISH_MARKS") %></td>

                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </asp:Panel>
                                                    </div>

                                                </div>
                                                <div class="col-md-12">
                                                    <div id="divtestprep" runat="server" visible="false">
                                                        <asp:Panel ID="Panel6" runat="server">
                                                            <asp:ListView ID="lvtestprep" runat="server">
                                                                <LayoutTemplate>
                                                                    <div>
                                                                        <div class="sub-heading">
                                                                            <h5>Testprep Data List</h5>
                                                                        </div>

                                                                        <table class="table table-striped table-bordered nowrap" style="width: 100%" id="my-Table">
                                                                            <thead class="bg-light-blue">
                                                                                <tr id="trRow">
                                                                                    <th class="text-center">Sr.No. </th>
                                                                                    <th class="text-center">FirstName </th>
                                                                                    <th class="text-center">MiddleName</th>
                                                                                    <th class="text-center">LastName </th>
                                                                                    <th class="text-center">RollNo</th>
                                                                                    <th class="text-center">MobileNo</th>
                                                                                    <th class="text-center">EmailID</th>
                                                                                    <th class="text-center">Gender</th>
                                                                                    <th class="text-center">PRNNo </th>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                                <tr id="itemPlaceholder" runat="server" />
                                                                            </tbody>
                                                                        </table>
                                                                    </div>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td class="text-center"><%#Container.DataItemIndex+1 %></td>

                                                                        <td class="text-center"><%# Eval("FirstName") %> </td>
                                                                        <td class="text-center"><%# Eval("MiddleName") %> </td>
                                                                        <td class="text-center"><%# Eval("LastName") %> </td>
                                                                        <td class="text-center"><%# Eval("RollNo") %></td>
                                                                        <td class="text-center"><%# Eval("MobileNo") %></td>
                                                                        <td class="text-center"><%# Eval("EmailID") %></td>
                                                                        <td class="text-center"><%# Eval("Gender") %></td>
                                                                        <td class="text-center"><%# Eval("PRNNo") %></td>

                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </asp:Panel>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="lbExcelFormat" />
                                    <asp:PostBackTrigger ControlID="btnUpload" />
                                    <asp:PostBackTrigger ControlID="btnDownload" />
                                    <asp:PostBackTrigger ControlID="btnMarksPreData" />
                                    <asp:PostBackTrigger ControlID="btnexcelformat" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <%-- Aptitude Test Mark Entry Tab End --%>

                        <%-- Aptitude Manual Mark Entry Tab Start --%>

                        <div class="tab-pane fade" id="tab_2">

                            <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updaptimark"
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
                            <style>
                                #ctl00_ContentPlaceHolder1_Panel1 .dataTables_scrollHeadInner {
                                    width: max-content !important;
                                }
                            </style>
                            <style>
                                #ctl00_ContentPlaceHolder1_Panel1 .form-control {
                                    padding: 0.15rem 0.15rem;
                                }
                            </style>
                            <asp:UpdatePanel ID="updaptimark" runat="server">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-md-12 col-sm-12 col-12">
                                            <div class="sub-heading mt-3">
                                                <h5>Aptitude Manual Mark Entry</h5>
                                            </div>
                                            <div class="box-body">
                                                <div class="col-12">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <%--<label>Intake</label>--%>
                                                                <asp:Label ID="lblDYSession" runat="server" Font-Bold="true"></asp:Label>

                                                            </div>
                                                            <asp:DropDownList ID="ddlIntake" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="1">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlIntake"
                                                                Display="None" ErrorMessage="Please Select Intake" InitialValue="0"
                                                                ValidationGroup="submit" />

                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <%--<label>Intake</label>--%>
                                                                <asp:Label ID="lblDYPrgmtype" runat="server" Font-Bold="true"></asp:Label>

                                                            </div>
                                                            <asp:DropDownList ID="ddlStudyLevel" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="1">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlStudyLevel"
                                                                Display="None" ErrorMessage="Please Select Study Level" InitialValue="0"
                                                                ValidationGroup="submit" />

                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <%--<label>Discipline</label>--%>
                                                                <asp:Label ID="lblDiscipline" runat="server" Font-Bold="true"></asp:Label>

                                                            </div>
                                                            <asp:ListBox ID="lstbxDiscipline" runat="server" CssClass="form-control multi-select-demo" SelectionMode="Multiple" TabIndex="2"></asp:ListBox>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">

                                                                <asp:Label ID="lblICCampus" runat="server" Font-Bold="true"></asp:Label>

                                                            </div>
                                                            <asp:DropDownList ID="ddlCampus" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="1">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>

                                                        </div>

                                                        <%-- <div class="form-group col-lg-3 col-md-6 col-12">
                                        <span style="color: Red">* </span><label><span style="font:bold;color:black;">Mark Entry Status </span></label>
                                        <div class="input-group">
                                            <asp:RadioButton ID="rbtnPending" runat="server" GroupName="SubType" Text="Pending" Checked="true" />&nbsp;&nbsp
                                            <asp:RadioButton ID="rbtnCompleted" runat="server" GroupName="SubType" Text="Completed" />
                                        </div>
                                    </div>--%>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">

                                                                <asp:Label ID="lblmark" runat="server" Text="Mark Entry Status" Font-Bold="true"></asp:Label>

                                                            </div>
                                                            <asp:DropDownList ID="ddlMarkStatus" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="1">
                                                                <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                                                <asp:ListItem Value="0">Pending</asp:ListItem>
                                                                <asp:ListItem Value="1">Completed</asp:ListItem>
                                                            </asp:DropDownList>

                                                        </div>
                                                        <div class="form-group col-lg-2 col-md-6 col-12" id="DivCountAll" runat="server" visible="false">
                                                            <div class="label-dynamic">
                                                                <br />
                                                                <asp:Label ID="lblName" runat="server" Font-Bold="true">All Student : </asp:Label><asp:Label ID="lblCountAll" runat="server" Font-Bold="true"></asp:Label>

                                                            </div>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="DivCountComplete" runat="server" visible="false">
                                                            <div class="label-dynamic">
                                                                <br />
                                                                <asp:Label ID="Label3" runat="server" Font-Bold="true">Completed Student : </asp:Label><asp:Label ID="lblCountCompleted" runat="server" Font-Bold="true"></asp:Label>

                                                            </div>
                                                        </div>
                                                        <div class="form-group col-lg-2 col-md-6 col-12" id="DivCountPending" runat="server" visible="false">
                                                            <div class="label-dynamic">
                                                                <br />
                                                                <asp:Label ID="Label1" runat="server" Font-Bold="true">Pending Student : </asp:Label><asp:Label ID="lblCountPending" runat="server" Font-Bold="true"></asp:Label>

                                                            </div>
                                                        </div>

                                                    </div>


                                                </div>
                                            </div>

                                            <div class="col-12 btn-footer">
                                                <asp:LinkButton ID="btnShow" runat="server" CssClass="btn btn-outline-info" OnClick="btnShow_Click" TabIndex="3" ValidationGroup="submit">Show</asp:LinkButton>
                                                <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn btn-outline-info" OnClick="btnSubmit_Click" Visible="false">Submit</asp:LinkButton>

                                                <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-outline-danger" TabIndex="4" OnClick="btnCancel_Click">Cancel</asp:LinkButton>
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

                                            </div>
                                            <div class="col-md-12">
                                                <div class="col-md-12" id="divlvaptimark" runat="server" visible="false">
                                                    <asp:Panel ID="Panel2" runat="server">
                                                        <%--Height="600px" ScrollBars="Auto" Width="100%"--%>
                                                        <asp:ListView ID="lvMark" runat="server">
                                                            <LayoutTemplate>
                                                                <div>
                                                                    <div class="sub-heading">
                                                                        <h5>Aptitute Manual Mark Entry List</h5>
                                                                    </div>
                                                                    <div class="row mb-1">
                                                                        <div class="col-lg-2 col-md-6 offset-lg-7">
                                                                            <button type="button" class="btn btn-outline-primary float-lg-right saveAsExcelOnline">Export Excel</button>
                                                                        </div>
                                                                        <div class="col-lg-3 col-md-6">
                                                                            <div class="input-group sea-rch">
                                                                                <input type="text" id="FilterDataOnline" class="form-control" placeholder="Search" />
                                                                                <div class="input-group-addon">
                                                                                    <i class="fa fa-search"></i>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="table-responsive" style="height: 320px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                                        <table class="table table-striped table-bordered nowrap mb-0" style="width: 100%;" id="tableOnline">
                                                                            <thead class="bg-light-blue" style="position: sticky; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                                <tr>
                                                                                    <th style="text-align: center">
                                                                                        <asp:CheckBox ID="cbHead" runat="server" OnClick="checkAllCheckbox(this)" />
                                                                                    </th>
                                                                                    <th>Application ID</th>
                                                                                    <th>Student Name</th>
                                                                                    <th>English</th>
                                                                                    <th>General</th>
                                                                                    <th>Total</th>
                                                                                    <th>Intake</th>
                                                                                    <th>Email ID</th>
                                                                                    <th>NIC</th>
                                                                                    <th>Program</th>

                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                                <tr id="itemPlaceholder" runat="server" />
                                                                            </tbody>
                                                                        </table>
                                                                    </div>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td style="text-align: center">
                                                                        <asp:CheckBox ID="chktransfer" runat="server" /></td>
                                                                    <td>
                                                                        <asp:Label ID="lblusername" runat="server" Text='<%# Eval("USERNAME")%>'></asp:Label>

                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblname" runat="server" Text='<%# Eval("NAME")%>'></asp:Label>
                                                                        <%--<%# Eval("NAME")%>--%> 
                                                                    </td>

                                                                    <td>
                                                                        <asp:TextBox ID="txtEnglish" runat="server" CssClass="form-control" Text='<%# Eval("ENGLISH_MARKS")%> ' onblur="return CheckMark(this);" /></td>
                                                                    </td>
                                                                          <td>
                                                                              <asp:TextBox ID="txtGeneral" runat="server" CssClass="form-control" Text='<%# Eval("GENERAL")%>' onblur="return CheckMark(this);" /></td>

                                                                    </td>
                                                                          <td>
                                                                              <asp:TextBox ID="txtTotal" runat="server" disabled="disabled" Style="width: 50px" CssClass="form-control" Text='<%# Eval("TOTAL_MARKS") %>' /></td>

                                                                    </td>
                                                                          <td>
                                                                              <asp:Label ID="lblintake" runat="server" Text='<%# Eval("BATCHNAME")%>'></asp:Label>

                                                                          </td>
                                                                    <td>
                                                                        <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("EMAILID")%>'></asp:Label>

                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblNic" runat="server" Text='<%# Eval("NIC")%>'></asp:Label>

                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblpgm" runat="server" Text='<%# Eval("PROGRAM_DETAIL") %>'></asp:Label>
                                                                        <asp:HiddenField ID="hdfPgm" runat="server" Value='<%# Eval("AREA_INT_NO") %>'></asp:HiddenField>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                    <div class="col-12">
                                                        <label class="" style="font-weight: 400;">
                                                            Showing
                                                            <asp:Label ID="lblTotalCount" runat="server" Visible="false"></asp:Label>
                                                            entries</label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    </div>
                                        </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </div>
                        <%-- Aptitude Manual Mark Entry Tab End --%>
                        <div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <%-- Aptitude Test Mark Entry Script Start --%>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
        });

    </script>
    <script type="text/javascript">
        function topFunction() {
            document.body.scrollTop = 0;
            document.documentElement.scrollTop = 0;
        }
    </script>
    <script>
        (function ($) {
            "use strict";
            $('.label.ui.dropdown')
         .dropdown();
            $('.no.label.ui.dropdown')
              .dropdown({
                  useLabels: false
              });
            $('.ui.button').on('click', function () {
                $('.ui.dropdown')
                  .dropdown('restore defaults')
            })
        })(jQuery);

        var prm1 = Sys.WebForms.PageRequestManager.getInstance();
        prm1.add_endRequest(function () {
            "use strict";
            $('.label.ui.dropdown')
         .dropdown();
            $('.no.label.ui.dropdown')
              .dropdown({
                  useLabels: false
              });
            $('.ui.button').on('click', function () {
                $('.ui.dropdown')
                  .dropdown('restore defaults')
            })
        })(jQuery);
    </script>


    <script>
        $(document).ready(function () {
            var check_panel_state = $('#ctl00_ContentPlaceHolder1_ddlMainLeadLabel option[selected="selected"]').length;
            if (check_panel_state > 0) {
                $(".pageright-wrapper").addClass("toggleed");
            }
            else {
                $(".pageright-wrapper").removeClass("toggleed");
            }
        });

        varprm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function (sender, e) {
            var check_panel_state = $('#ctl00_ContentPlaceHolder1_ddlMainLeadLabel option[selected="selected"]').length;
            if (check_panel_state > 0) {
                $(".pageright-wrapper").addClass("toggleed");
            }
            else {
                $(".pageright-wrapper").removeClass("toggleed");
            }
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#pickerNew').daterangepicker({
                startDate: moment().subtract(00, 'days'),
                endDate: moment(),
                locale: {
                    format: 'DD MMM, YYYY'
                },
                ranges: {
                },
            },
        function (start, end) {
            $('#dateNew').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
            document.getElementById('<%=hdfGetFromDate.ClientID%>').value = (start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'));
        });

            //$('#dateNew').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
            //document.getElementById('<%=hdfGetFromDate.ClientID%>').value = (moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(document).ready(function () {
                $('#pickerNew').daterangepicker({
                    startDate: moment().subtract(00, 'days'),
                    endDate: moment(),
                    locale: {
                        format: 'DD MMM, YYYY'
                    },
                    ranges: {
                    },
                },
            function (start, end) {
                debugger
                $('#dateNew').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
                document.getElementById('<%=hdfGetFromDate.ClientID%>').value = (start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
            });

                //$('#dateNew').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
                //document.getElementById('<%=hdfGetFromDate.ClientID%>').value = (moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
            });
        });

    </script>
    <%-- Aptitude Test Mark Entry Script End --%>


    <%-- Aptitude Manual Mark Entry Scrept Start --%>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
        });
    </script>
    <script type="text/javascript" language="javascript">

        function checkAllCheckbox(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                var s = e.name.split("ctl00$ContentPlaceHolder1$lvMark$ctrl");
                var b = 'ctl00$ContentPlaceHolder1$lvMark$ctrl';
                var g = b + s[1];
                if (e.name == g) {
                    if (headchk.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }

    </script>

    <%-- Aptitude Manual Mark Entry Script End --%>
    <script>
     
        function CheckMark(id) {
           
            var ValidChars = "0123456789.-";
         
            var num = true;
            var mChar;
            mChar = id.value.charAt(0);
            if (ValidChars.indexOf(mChar) == -1) {
                num = false;
                id.value = '';
                alert("Error! Only Numeric Values Are Allowed")
                id.select();
                id.focus();
                
            }

            var rowIndex = id.offsetParent.parentNode.rowIndex - 1;
            var cellIndex = id.offsetParent.cellIndex;
            //alert(rowIndex)
            var internal1 = 0; var general = 0;
                
            internal1 = document.getElementById("ctl00_ContentPlaceHolder1_lvMark_ctrl" + rowIndex + "_txtEnglish").value;
            //alert(internal1)
            if (internal1 > 100) {
                num = false;
                id.value = '';
                alert("Error! Marks Is Not Greater Than 100")
                id.select();
                id.focus();
                internal1 = '';
            }
            general = document.getElementById("ctl00_ContentPlaceHolder1_lvMark_ctrl" + rowIndex + "_txtGeneral").value;
            if (general > 100) {
                num = false;
                id.value = '';
                alert("Error! Marks Is Not Greater Than 100")
                id.select();
                id.focus();
                general = '';

            }
            if (general == '') {
                var general = 0;
            }
            if (internal1 == '') {
                var internal1 = 0;
            }
            ConvertMark = (Number(internal1) + Number(general))
            document.getElementById("ctl00_ContentPlaceHolder1_lvMark_ctrl" + rowIndex + "_txtTotal").value = ConvertMark;
        }

    </script>

    <script>
        function toggleSearch(searchBar, table) {
            var tableBody = table.querySelector('tbody');
            var allRows = tableBody.querySelectorAll('tr');
            var val = searchBar.value.toLowerCase();
            allRows.forEach((row, index) => {
                var insideSearch = row.innerHTML.trim().toLowerCase();
            //console.log('data',insideSearch.includes(val),'searchhere',insideSearch);
            if (insideSearch.includes(val)) {
                row.style.display = 'table-row';
            }
            else {
                row.style.display = 'none';
            }

        });
        }

        function test5() {
            var searchBar5 = document.querySelector('#FilterDataOnline');
            var table5 = document.querySelector('#tableOnline');

            //console.log(allRows);
            searchBar5.addEventListener('focusout', () => {
                toggleSearch(searchBar5, table5);
        });

        $(".saveAsExcelOnline").click(function () {
            var workbook = XLSX.utils.book_new();
            var allDataArray = [];
            allDataArray = makeTableArray(table5, allDataArray);
            var worksheet = XLSX.utils.aoa_to_sheet(allDataArray);
            workbook.SheetNames.push("Test");
            workbook.Sheets["Test"] = worksheet;
            XLSX.writeFile(workbook, "MarkEntryData.xlsx");
        });
        }

        function makeTableArray(table, array) {
            var allTableRows = table.querySelectorAll('tr');
            allTableRows.forEach(row => {
                var rowArray = [];
            if (row.querySelector('td')) {
                var allTds = row.querySelectorAll('td');
                allTds.forEach(td => {
                    if (td.querySelector('span')) {
                        rowArray.push(td.querySelector('span').textContent);
            }
            else if (td.querySelector('input')) {
                rowArray.push(td.querySelector('input').value);
            }
            else if (td.querySelector('select')) {
                rowArray.push(td.querySelector('select').value);
            }
            else if (td.innerText) {
                rowArray.push(td.innerText);
            }
            else{
                rowArray.push('');
            }
        });
        }
        if (row.querySelector('th')) {
            var allThs = row.querySelectorAll('th');
            allThs.forEach(th => {
                if (th.textContent) {
                    rowArray.push(th.textContent);
        }
        else {
            rowArray.push('');
        }
        });
        }
        // console.log(allTds);

        array.push(rowArray);
        });
        return array;
        }

    </script>

</asp:Content>

