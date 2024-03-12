<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Create_Criteria.aspx.cs" Inherits="Projects_Create_Criteria" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
    <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>

    <style>
        .fa-plus {
            color: #17a2b8;
            border: 1px solid #17a2b8;
            border-radius: 50%;
            padding: 6px 8px;
        }
    </style>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                </div>

                <div class="box-body">
                    <div class="nav-tabs-custom">
                        <ul class="nav nav-tabs" role="tablist">
                            <li class="nav-item">
                                <a class="nav-link active" data-toggle="tab" href="#tab_1" tabindex="1">Create Criteria</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_2" tabindex="2">Create Rule</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_3" tabindex="3">Rule Allocation</a>
                            </li>
                        </ul>

                        <div class="tab-content">

                            <div class="tab-pane active" id="tab_1">
                                <div>

                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updCriteria"
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
                                <asp:UpdatePanel ID="updCriteria" runat="server">
                                    <ContentTemplate>
                                        <div class="col-12 mt-3">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <asp:Label ID="lblCriteriaName" runat="server" Font-Bold="true">Criteria Name</asp:Label>
                                                    </div>
                                                    <asp:TextBox ID="txtCriteriaName" runat="server" CssClass="form-control" TabIndex="4"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCriteriaName"
                                                        Display="None" ErrorMessage="Please Enter Criteria Name" ValidationGroup="btnCriteria" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <asp:Label ID="lblExamType" runat="server" Font-Bold="true"></asp:Label>
                                                        <%--<label>Exam Type</label>--%>
                                                    </div>
                                                    <asp:DropDownList ID="ddlExamType" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="5" AppendDataBoundItems="true" Enabled="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        <asp:ListItem Value="1">A/L</asp:ListItem>
                                                        <asp:ListItem Value="2">O/L</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlExamType"
                                                        Display="None" ErrorMessage="Please Select Exam Type" InitialValue="0" ValidationGroup="btnCriteria" />
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <asp:Label ID="lblExamination" runat="server" Font-Bold="true">Examination</asp:Label>
                                                        <%--<label>Examination</label>--%>
                                                    </div>
                                                    <asp:DropDownList ID="ddlExamination" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="7" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlExamination_SelectedIndexChanged" Enabled="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <%--  <asp:ListBox ID="lstbxExamination" runat="server" CssClass="form-control multi-select-demo" SelectionMode="Multiple" TabIndex="6"></asp:ListBox>--%>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlExamination"
                                                        Display="None" ErrorMessage="Please Select Examination" InitialValue="0" ValidationGroup="btnCriteria" />
                                                    <!-- Sri Lanka , Cambridge ,  Edexcel -->
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Min. Pass</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlMinPass" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="7" AppendDataBoundItems="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        <%-- <asp:ListItem Value="1">1</asp:ListItem>
                                                        <asp:ListItem Value="2">2</asp:ListItem>
                                                        <asp:ListItem Value="3">3</asp:ListItem>--%>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlMinPass"
                                                        Display="None" ErrorMessage="Please Select Min Pass" InitialValue="0" ValidationGroup="btnCriteria" />
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Combination Type</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlCombinationType" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="8" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlCombinationType_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        <asp:ListItem Value="1">Specific</asp:ListItem>
                                                        <asp:ListItem Value="2">Any</asp:ListItem>
                                                        <asp:ListItem Value="3">Both</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlCombinationType"
                                                        Display="None" ErrorMessage="Please Select Combination Type" InitialValue="0" ValidationGroup="btnCriteria" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12" runat="server" visible="false" id="DivSpecificSub">
                                            <div class="col-12" runat="server">
                                                <div class="row">
                                                    <div class="col-12">
                                                        <div class="sub-heading">
                                                            <h5>For Specific</h5>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Subject</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlSubject" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="9" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="ddlSubject"
                                                            Display="None" ErrorMessage="Please Select Subject" InitialValue="0" ValidationGroup="ADD" />
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Min. Grade</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlMinGrade" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="10" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="ddlMinGrade"
                                                            Display="None" ErrorMessage="Please Select Min Grade" InitialValue="0" ValidationGroup="ADD" />
                                                    </div>


                                                    <div class="form-group col-lg-2 col-md-2 col-2">
                                                        <div class="label-dynamic">
                                                            <label></label>
                                                        </div>
                                                        <asp:Button ID="btnAddEducation" runat="server" Text="Add" CssClass="btn btn-outline-primary" OnClick="btnAddEducation_Click" ValidationGroup="ADD" TabIndex="11" />
                                                        <asp:ValidationSummary ID="ValidationSummary4" runat="server" ValidationGroup="ADD"
                                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                    </div>
                                                </div>

                                            </div>
                                            <div id="Div2" class="col-12" runat="server">
                                                <div class="row">
                                                     <div class="col-12">
                                                        <div class="sub-heading">
                                                            <h5>For Specific Pass Grade</h5>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Pass Grade</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlgradeand" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="10" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="ddlgradeand"
                                                            Display="None" ErrorMessage="Please Select Pass Grade" InitialValue="0" ValidationGroup="ADDSecond" />
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Passes</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlPassess" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="10" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator188" runat="server" ControlToValidate="ddlPassess"
                                                            Display="None" ErrorMessage="Please Select Passes" InitialValue="0" ValidationGroup="ADDSecond" />
                                                    </div>
                                                    <div class="form-group col-lg-2 col-md-2 col-2">
                                                        <div class="label-dynamic">
                                                            <label></label>
                                                        </div>
                                                        <asp:Button ID="btnAddSecond" runat="server" Text="Add" CssClass="btn btn-outline-primary" OnClick="btnAddSecond_Click" ValidationGroup="ADDSecond" TabIndex="11" />
                                                        <asp:ValidationSummary ID="ValidationSummary6" runat="server" ValidationGroup="ADDSecond"
                                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <asp:Panel ID="Panel3" runat="server">
                                                    <asp:ListView ID="lvlEducationDetails" runat="server">
                                                        <LayoutTemplate>
                                                            <div class="sub-heading">
                                                                <h5>For Specific Subject</h5>
                                                            </div>
                                                            <table class="table table-striped table-bordered nowrap" id="mytable" style="width: 100%">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th style="text-align: center">Edit
                                                                        </th>
                                                                        <th style="text-align: center">Delete
                                                                        </th>
                                                                        <th style="text-align: center">Subject
                                                                        </th>
                                                                        <th style="text-align: center">Grade
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
                                                                <td style="text-align: center">
                                                                    <asp:HiddenField ID="hfsrno" runat="server" Value='<%# Container.DataItemIndex + 1 %>' />
                                                                    <asp:LinkButton ID="lnkEditEducation" runat="server" CssClass="fa fa-edit" OnClick="lnkEditEducation_Click" ToolTip="Edit" CommandName='<%# Container.DataItemIndex + 1 %>' CommandArgument='<%#Eval("CRITERIA_NO") %>'></asp:LinkButton>
                                                                    <asp:HiddenField ID="hfdValue" runat="server" Value="0" />
                                                                    <asp:HiddenField ID="hdfSrEdu" runat="server" Value='<%# Container.DataItemIndex + 1 %>' />
                                                                    <asp:HiddenField ID="hdfCRITERIA_NO" runat="server" Value='<%#Eval("CRITERIA_NO") %>' />
                                                                </td>
                                                                <td style="text-align: center">
                                                                    <asp:LinkButton ID="lnkDeleteEducation" runat="server" CssClass="fa fa-trash" Style="color: red" ToolTip="Delete" OnClick="lnkDeleteEducation_Click" CommandName='<%# Container.DataItemIndex + 1 %>' CommandArgument='<%#Eval("CRITERIA_NO") %>' OnClientClick="return confirm('Are you sure you want to delete this record ?')"></asp:LinkButton>

                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblSubjectName" runat="server" Text='<%#Eval("SUBJECT") %>'></asp:Label>
                                                                    <asp:Label ID="lblSubjectNo" runat="server" Text='<%#Eval("SUBJECTNO") %>' Visible="false"></asp:Label>
                                                                </td>
                                                                <td style="text-align: center">
                                                                    <asp:Label ID="lblGradeName" runat="server" Text='<%#Eval("GRADE") %>'></asp:Label>
                                                                    <asp:Label ID="lblGradeNo" runat="server" Text='<%#Eval("GRADENO") %>' Visible="false"></asp:Label>
                                                                </td>
                                                                <%-- <td style="text-align: center">
                                                                    <asp:Label ID="lblPasseName" runat="server" Text='<%#Eval("PASSESS") %>'></asp:Label>
                                                                    <asp:Label ID="lblPasseNo" runat="server" Text='<%#Eval("PASSESSNO") %>' Visible="false"></asp:Label>
                                                                </td>--%>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </asp:Panel>
                                            </div>
                                            <div class="col-md-12">
                                                <asp:Panel ID="Panel6" runat="server">
                                                    <asp:ListView ID="LvAndDetails" runat="server">
                                                        <LayoutTemplate>
                                                            <div class="sub-heading">
                                                                <h5>For Specific Pass Grade</h5>
                                                            </div>
                                                            <table class="table table-striped table-bordered nowrap" id="mytable" style="width: 100%">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th style="text-align: center">Edit
                                                                        </th>
                                                                        <th style="text-align: center">Delete
                                                                        </th>
                                                                        <th style="text-align: center">Pass Grade
                                                                        </th>
                                                                        <th style="text-align: center">Passes
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
                                                                <td style="text-align: center">
                                                                    <asp:HiddenField ID="hfsrno" runat="server" Value='<%# Container.DataItemIndex + 1 %>' />
                                                                    <asp:LinkButton ID="lnkEditEducationAdd" runat="server" CssClass="fa fa-edit"  OnClick="lnkEditEducationAdd_Click" ToolTip="Edit" CommandName='<%# Container.DataItemIndex + 1 %>' CommandArgument='<%#Eval("CRITERIA_NO") %>'></asp:LinkButton>
                                                                    <asp:HiddenField ID="hfdValue" runat="server" Value="0" />
                                                                    <asp:HiddenField ID="hdfSrEdu" runat="server" Value='<%# Container.DataItemIndex + 1 %>' />
                                                                    <asp:HiddenField ID="hdfCRITERIA_NO" runat="server" Value='<%#Eval("CRITERIA_NO") %>' />
                                                                </td>
                                                                <td style="text-align: center">
                                                                    <asp:LinkButton ID="lnkDeleteEducationAdd" runat="server" CssClass="fa fa-trash" Style="color: red" ToolTip="Delete"  OnClick="lnkDeleteEducationAdd_Click" CommandName='<%# Container.DataItemIndex + 1 %>' CommandArgument='<%#Eval("CRITERIA_NO") %>' OnClientClick="return confirm('Are you sure you want to delete this record ?')"></asp:LinkButton>

                                                                </td>
                                                                <td style="text-align: center">
                                                                    <asp:Label ID="lblGradeName" runat="server" Text='<%#Eval("ADDGRADE") %>'></asp:Label>
                                                                    <asp:Label ID="lblGradeNo" runat="server" Text='<%#Eval("ADDGRADENO") %>' Visible="false"></asp:Label>
                                                                </td>
                                                                 <td style="text-align: center">
                                                                    <asp:Label ID="lblPasseName" runat="server" Text='<%#Eval("PASSESS") %>'></asp:Label>
                                                                    <asp:Label ID="lblPasseNo" runat="server" Text='<%#Eval("PASSESSNO") %>' Visible="false"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </asp:Panel>
                                            </div>
                                        </div>

                                        <div class="col-12" runat="server" visible="false" id="DivForany">
                                            <div class="row">
                                                <div class="col-12">
                                                    <div class="sub-heading">
                                                        <h5>For Any</h5>
                                                    </div>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Min. Grade</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlMinGradeAny" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="12" AppendDataBoundItems="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="ddlMinGradeAny"
                                                        Display="None" ErrorMessage="Please Select Min Grade" InitialValue="0" ValidationGroup="btnCriteria" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-12 btn-footer">
                                            <asp:LinkButton ID="btnSubmitCriteria" runat="server" CssClass="btn btn-outline-primary" TabIndex="13" ValidationGroup="btnCriteria" OnClick="btnSubmitCriteria_Click">Submit</asp:LinkButton>
                                            <asp:LinkButton ID="btnCancelCriteria" runat="server" CssClass="btn btn-outline-danger" TabIndex="14" OnClick="btnCancelCriteria_Click">Cancel</asp:LinkButton>
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="btnCriteria"
                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                        </div>

                                        <div class="col-md-12">
                                            <asp:Panel ID="Panel1" runat="server">
                                                <asp:ListView ID="lvCriteriaRule" runat="server">
                                                    <LayoutTemplate>
                                                        <div class="sub-heading">
                                                            <h5>For Specific Subject</h5>
                                                        </div>
                                                          <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="myTable">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>Edit</th>
                                                                    <th>Criteria Name</th>
                                                                    <th>Exam Type</th>

                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </tbody>
                                                        </table>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td><%--CommandArgument='<%# Eval("IDNO") %>'--%>
                                                                <asp:LinkButton ID="btnEdit" runat="server" CssClass="fa fa-pencil-square-o" OnClick="btnEdit_Click" ToolTip="Edit" CommandName='<%# Container.DataItemIndex + 1 %>' CommandArgument='<%#Eval("CRIT_NO") %>'></asp:LinkButton>
                                                                <%--<asp:LinkButton ID="btnJobAnnouncement" class="btnEditX" runat="server" CssClass="fa fa-pencil-square-o" /></td>--%>
                                                            <td><%#Eval("CRITERIA_NAME") %></td>
                                                            <td><%#Eval("EXA_TYPE") %></td>

                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>


                                        <%--    <div class="col-12 mt-3">
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Edit</th>
                                                        <th>Criteria Name</th>
                                                        <th>Exam Type</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <asp:LinkButton ID="btnEdit" runat="server" CssClass="fa fa-pencil-square-o" OnClick="btnEdit_Click"></asp:LinkButton>
                                                         
                                                        <td>Criteria Name</td>
                                                        <td>AL </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>--%>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                                <div class="tab-pane fade" id="tab_2">
                                    <div>

                                        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updRule"
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
                                    <asp:UpdatePanel ID="updRule" runat="server">
                                        <ContentTemplate>
                                            <div class="col-12 mt-3">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Rule Name</label>
                                                        </div>
                                                        <asp:TextBox ID="txtRuleName" runat="server" CssClass="form-control" TabIndex="4"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtRuleName"
                                                            Display="None" ErrorMessage="Please Enter Rule Name" ValidationGroup="btnRule" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Criteria</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlCriteria" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="5" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlCriteria_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlCriteria"
                                                            Display="None" ErrorMessage="Please Select Criteria" InitialValue="0" ValidationGroup="ADDRULE" />
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            <label>AND / OR</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlAndOrRule" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="6" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            <asp:ListItem Value="1">AND</asp:ListItem>
                                                            <asp:ListItem Value="2">OR</asp:ListItem>
                                                        </asp:DropDownList>
                                                     <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlAndOrRule"
                                                            Display="None" ErrorMessage="Please Select AND / OR" InitialValue="0" ValidationGroup="ADDRULE" />--%>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                           <%-- <sup>* </sup>--%>
                                                            <label>Criteria</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlCriteriaPlus" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="7" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    <%--    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlCriteriaPlus"
                                                            Display="None" ErrorMessage="Please Select Criteria" InitialValue="0" ValidationGroup="ADDRULE" />--%>
                                                    </div>

                                                    <div class="form-group col-lg-2 col-md-2 col-2">
                                                        <div class="label-dynamic">
                                                            <label></label>
                                                        </div>
                                                        <asp:Button ID="btnAddcriteRule" runat="server" Text="Add" CssClass="btn btn-outline-primary" OnClick="btnAddcriteRule_Click" ValidationGroup="ADDRULE" TabIndex="8" />
                                                        <asp:ValidationSummary ID="ValidationSummary5" runat="server" ValidationGroup="ADDRULE"
                                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <asp:Panel ID="Panel2" runat="server">
                                                    <asp:ListView ID="lvCriteriaRulename" runat="server">
                                                        <LayoutTemplate>
                                                            <div class="sub-heading">
                                                                <h5>For Specific Subject</h5>
                                                            </div>
                                                            <table class="table table-striped table-bordered nowrap" id="mytable" style="width: 100%">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th style="text-align: center">Edit
                                                                        </th>
                                                                        <th style="text-align: center">Delete
                                                                        </th>
                                                                        <th style="text-align: center">Criteria
                                                                        </th>
                                                                        <th style="text-align: center">AND / OR 
                                                                        </th>
                                                                        <th style="text-align: center">Criteria
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
                                                                <td style="text-align: center">
                                                                    <asp:LinkButton ID="lnkEditEducation" runat="server" CssClass="fa fa-edit" ToolTip="Edit" CommandName='<%# Container.DataItemIndex + 1 %>' CommandArgument='<%#Eval("CRIT_RULE_NO") %>' OnClick="lnkEditEducation_Click1"></asp:LinkButton>
                                                                    <asp:HiddenField ID="hfdValue" runat="server" Value="0" />
                                                                    <asp:HiddenField ID="hdfSrEdu" runat="server" Value='<%# Container.DataItemIndex + 1 %>' />
                                                                    <asp:HiddenField ID="hdfCRIT_RULE_NO" runat="server" Value='<%#Eval("CRIT_RULE_NO") %>' />
                                                                </td>
                                                                <td style="text-align: center">
                                                                    <asp:LinkButton ID="lnkDeleteEducation" runat="server" CssClass="fa fa-trash" Style="color: red" ToolTip="Delete" CommandName='<%# Container.DataItemIndex + 1 %>' CommandArgument='<%#Eval("CRIT_RULE_NO") %>' OnClick="lnkDeleteEducation_Click1" OnClientClick="return confirm('Are you sure you want to delete this record ?')"></asp:LinkButton>

                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblCriteriaName" runat="server" Text=' <%#Eval("CRITERIANAME") %>'></asp:Label>
                                                                    <asp:Label ID="lblCriteriaNo" runat="server" Text=' <%#Eval("CRITERIANO") %>' Visible="false"></asp:Label>
                                                                </td>
                                                                <td style="text-align: center">
                                                                    <asp:Label ID="lblAndOrRuleName" runat="server" Text='<%#Eval("AND_OR_NAME") %>'></asp:Label>
                                                                    <asp:Label ID="lblAndOrRuleNo" runat="server" Text='<%#Eval("AND_OR_NO") %>' Visible="false"></asp:Label>
                                                                </td>
                                                                <td style="text-align: center">
                                                                    <asp:Label ID="lblCriteriaPlusName" runat="server" Text='<%#Eval("CRITERIA_PLUSNAME") %>'></asp:Label>
                                                                    <asp:Label ID="lblCriteriaPlusNo" runat="server" Text='<%#Eval("CRITERIA_PLUSNO") %>' Visible="false"></asp:Label>
                                                                </td>

                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </asp:Panel>
                                            </div>

                                            <div class="col-12 btn-footer">
                                                <asp:LinkButton ID="btnSubmitRule" runat="server" CssClass="btn btn-outline-primary" TabIndex="9" ValidationGroup="btnRule" OnClick="btnSubmitRule_Click">Submit</asp:LinkButton>
                                                <asp:LinkButton ID="btnCancelRule" runat="server" CssClass="btn btn-outline-danger" TabIndex="10" OnClick="btnCancelRule_Click">Cancel</asp:LinkButton>
                                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="btnRule"
                                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                            </div>

                                            <div class="col-md-12">
                                                <asp:Panel ID="Panel4" runat="server">
                                                    <asp:ListView ID="lvListRuleBind" runat="server">
                                                        <LayoutTemplate>
                                                            <div class="sub-heading">
                                                                <h5>For Specific Subject</h5>
                                                            </div>
                                                            <table class="table table-striped table-bordered nowrap display" id="mytable" style="width: 100%">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>Edit</th>
                                                                        <th>Rule Name</th>

                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:LinkButton ID="lnkEditEducation" runat="server" CssClass="fa fa-edit" ToolTip="Edit" CommandName='<%# Container.DataItemIndex + 1 %>' CommandArgument='<%#Eval("CRIT_RULE_NO") %>' OnClick="lnkEditEducation_Click2"></asp:LinkButton>

                                                                </td>

                                                                <td>
                                                                    <asp:Label ID="lblRuleName" runat="server" Text='<%#Eval("CRITERIA_RULE_NAME") %>'></asp:Label>

                                                                </td>

                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </asp:Panel>
                                            </div>
                                            <%-- <div class="col-12 mt-3">
                                                <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>Edit</th>
                                                            <th>Rule Name</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <asp:LinkButton ID="LinkButton1" class="btnEditX" runat="server" CssClass="fa fa-pencil-square-o" /></td>
                                                            <td>Rule Name A</td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>--%>
                                            </div>
                                        </ContentTemplate>

                                    </asp:UpdatePanel>

                                    <div class="tab-pane fade" id="tab_3">
                                        <div>

                                            <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="updAllocate"
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
                                        <asp:UpdatePanel ID="updAllocate" runat="server">

                                            <ContentTemplate>
                                                <div class="col-12 mt-3">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <asp:Label ID="lblDYSession" runat="server" Font-Bold="true"></asp:Label>
                                                                <%--<label>Intake</label>--%>
                                                            </div>
                                                            <asp:DropDownList ID="ddlIntake" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="4" AppendDataBoundItems="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlIntake"
                                                                Display="None" ErrorMessage="Please Select Intake" InitialValue="0" ValidationGroup="btnAllocation" />
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <asp:Label ID="lblDYCollege" runat="server" Font-Bold="true"></asp:Label>
                                                                <%--<label>Faculty</label>--%>
                                                            </div>
                                                            <asp:DropDownList ID="ddlFaculty" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="5" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged" AutoPostBack="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlFaculty"
                                                                Display="None" ErrorMessage="Please Select Faculty" InitialValue="0" ValidationGroup="btnAllocation" />
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <asp:Label ID="lblDYPrgmtype" runat="server" Font-Bold="true"></asp:Label>
                                                                <%--<label>Study Level</label>--%>
                                                            </div>
                                                            <asp:DropDownList ID="ddlStudyLevel" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="6" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlStudyLevel_SelectedIndexChanged" AutoPostBack="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlStudyLevel"
                                                                Display="None" ErrorMessage="Please Select Study Level" InitialValue="0" ValidationGroup="btnAllocation" />
                                                        </div>

                                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <asp:Label ID="lblDYDegree" runat="server" Font-Bold="true"></asp:Label>
                                                                <%--<label>Program</label>--%>
                                                            </div>
                                                            <asp:DropDownList ID="ddlProgram" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="7" AppendDataBoundItems="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="ddlProgram"
                                                                Display="None" ErrorMessage="Please Select Program" InitialValue="0" ValidationGroup="btnAllocation" />
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <asp:Label ID="lblCriteriaRule" runat="server" Font-Bold="true"></asp:Label>
                                                                <%--<label>Rule</label>--%>
                                                            </div>
                                                            <asp:DropDownList ID="ddlRule" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="8" AppendDataBoundItems="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlRule"
                                                                Display="None" ErrorMessage="Please Select Rule" InitialValue="0" ValidationGroup="btnAllocation" />
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-12 btn-footer">
                                                    <asp:LinkButton ID="btnSubmitAllocation" runat="server" CssClass="btn btn-outline-primary" OnClick="btnSubmitAllocation_Click" TabIndex="9" ValidationGroup="btnAllocation">Submit</asp:LinkButton>
                                                    <asp:LinkButton ID="btnCancelAllocation" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancelAllocation_Click" TabIndex="10">Cancel</asp:LinkButton>
                                                    <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="btnAllocation"
                                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                </div>

                                                <div class="col-md-12">
                                                    <asp:Panel ID="Panel5" runat="server">
                                                        <asp:ListView ID="lvRuleAllocation" runat="server">
                                                            <LayoutTemplate>
                                                                <div class="sub-heading">
                                                                </div>
                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="myTable11">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>Edit</th>
                                                                            <th>Intake</th>
                                                                            <th>Faculty</th>
                                                                            <th>Study Level</th>
                                                                            <th>Program</th>
                                                                            <th>Rule</th>

                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                                </table>
                                                            </LayoutTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td style="text-align: center">
                                                                        <asp:LinkButton ID="lnkEditEducation" runat="server" CssClass="fa fa-edit" ToolTip="Edit" CommandName='<%# Container.DataItemIndex + 1 %>' CommandArgument='<%#Eval("CRITERIA_ALLC_NO") %>' OnClick="lnkEditEducation_Click3"></asp:LinkButton>

                                                                    </td>

                                                                    <td>
                                                                        <asp:Label ID="lblIntake" runat="server" Text='<%#Eval("BATCHNAME") %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblCollege" runat="server" Text='<%#Eval("COLLEGE_NAME") %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblStudyLevel" runat="server" Text='<%#Eval("SHORTNAME") %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblProgram" runat="server" Text='<%#Eval("PROGRAM") %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblRule" runat="server" Text='<%#Eval("CRITERIA_RULE_NAME") %>'></asp:Label>
                                                                    </td>


                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:ListView>
                                                    </asp:Panel>
                                                </div>

                                                <%--   <div class="col-12 mt-3">
                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Edit</th>
                                                                <th>Intake</th>
                                                                <th>Faculty</th>
                                                                <th>Study Level</th>
                                                                <th>Program</th>
                                                                <th>Rule</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr>
                                                                <td>
                                                                    <asp:LinkButton ID="LinkButton4" class="btnEditX" runat="server" CssClass="fa fa-pencil-square-o" /></td>
                                                                <td>Reg-2022</td>
                                                                <td>Faculty A</td>
                                                                <td>Study Level A</td>
                                                                <td>Program A</td>
                                                                <td>Rule A</td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>--%>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <script type="text/javascript">
                    $(document).ready(function () {
                        $('.multi-select-demo').multiselect({
                            includeSelectAllOption: true,
                            maxHeight: 200,
                            enableFiltering: true,
                            filterPlaceholder: 'Search',
                        });
                    });
                    var parameter = Sys.WebForms.PageRequestManager.getInstance();
                    parameter.add_endRequest(function () {
                        $(document).ready(function () {
                            $('.multi-select-demo').multiselect({
                                includeSelectAllOption: true,
                                maxHeight: 200,
                                enableFiltering: true,
                                filterPlaceholder: 'Search',
                            });
                        });
                    });
                </script>
</asp:Content>

