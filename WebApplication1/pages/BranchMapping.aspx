<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="BranchMapping.aspx.cs" Inherits="Academic_BranchEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <!-- jQuery library -->
    <%--    <link href="../plugins/multi-select/jquery.multiselect.css" rel="stylesheet" />
    <script src="../plugins/multi-select/jquery.multiselect.js"></script>--%>
    <link href="../plugins/multiselect/bootstrap-multiselect.css" rel="stylesheet" />

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updGradeEntry"
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

    <%--    <script type="text/javascript">

        $(document).ready(function () {

            //--*========Added by akhilesh on [2019-05-11]==========*--          
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(InitAutoCompl)

            //--*========Added by akhilesh on [2019-05-11]==========*--          
            // if you use jQuery, you can load them when dom is read.          
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_initializeRequest(InitializeRequest);
            prm.add_endRequest(EndRequest);

            // Place here the first init of the autocomplete
            InitAutoCompl();

            function InitializeRequest(sender, args) {
            }

            function EndRequest(sender, args) {
                // after update occur on UpdatePanel re-init the Autocomplete
                InitAutoCompl();
            }

            function InitAutoCompl() {
                $('select[multiple]').val('').multiselect({
                    columns: 2,     // how many columns should be use to show options            
                    search: true, // include option search box
                    searchOptions: {
                        delay: 250,                         // time (in ms) between keystrokes until search happens
                        clearSelection: true,
                        showOptGroups: false,                // show option group titles if no options remaining

                        searchText: true,                 // search within the text

                        searchValue: true,                // search within the value

                        onSearch: function (element) { } // fires on keyup before search on options happens
                    },

                    // plugin texts

                    texts: {

                        placeholder: 'Select department', // text to use in dummy input

                        search: 'Search',         // search input placeholder text

                        selectedOptions: ' selected',      // selected suffix text

                        selectAll: 'Select all department',     // select all text

                        unselectAll: 'Unselect all department',   // unselect all text

                        //noneSelected: 'None Selected'   // None selected text

                    },

                    // general options

                    selectAll: true, // add select all option

                    selectGroup: false, // select entire optgroup

                    minHeight: 200,   // minimum height of option overlay

                    maxHeight: null,  // maximum height of option overlay

                    maxWidth: null,  // maximum width of option overlay (or selector)

                    maxPlaceholderWidth: null, // maximum width of placeholder button

                    maxPlaceholderOpts: 10, // maximum number of placeholder options to show until "# selected" shown instead

                    showCheckbox: true,  // display the checkbox to the user

                    optionAttributes: [],

                });
            }

        });

    </script>--%>

    <script type="text/javascript" charset="utf-8">
        //$(document).ready(function () {
        //    $(".display").dataTable({
        //        "bJQueryUI": true,
        //        "sPaginationType": "full_numbers"
        //    });

        //});
        function ToUpper(ctrl) {

            var t = ctrl.value;

            ctrl.value = t.toUpperCase();

        }
        function ConfirmToDelete(me) {
            if (me != null) {
                var chk = confirm("Do you want to delete record......?");
                {
                    if (chk == true) {
                        return true;
                    }
                    else { return false; }
                }
            }

        }

        function ConfirmToEdit(me) {
            if (me != null) {
                var chk = confirm("Do you want to Update record......?");
                {
                    if (chk == true) {
                        return true;
                    }
                    else { return false; }
                }
            }
        }

    </script>

    <style>
        .multiselect-container > li > a > label {
            position: relative;
        }

        .dropdown-menu.show {
            width: 100%;
        }

        .multiselect.dropdown-toggle::after {
            display: none;
        }
        /*--======= input plus minus css added by gaurav 29072021 =======--*/
        .input-group-text.minus,
        .input-group-text.plus {
            display: -ms-flexbox;
            display: flex;
            -ms-flex-align: center;
            align-items: center;
            padding: .16rem .75rem;
            height: 30px;
            margin-bottom: 0;
            font-size: 20px;
            font-weight: 400;
            line-height: 1.3;
            color: #495057;
            text-align: center;
            white-space: nowrap;
            background-color: #d2d6de;
            border: 1px solid #d2d6de;
            border-radius: 0rem;
        }

        .duration {
            font-size: 18px;
            padding: .1rem .75rem;
            height: calc(1.1em + .75rem + 2px);
        }
    </style>

    <asp:HiddenField ID="hfdStat" runat="server" ClientIDMode="Static" />

    <asp:UpdatePanel ID="updGradeEntry" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <%-- <h3 class="box-title">Program Mapping</h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-lg-12 col-md-12 col-12">
                                        <div class="row">
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <asp:Label ID="lblDYCollege" runat="server" Font-Bold="true"></asp:Label>
                                                </div>
                                                <asp:DropDownList ID="ddlCollegeName" runat="server" TabIndex="1" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" ToolTip="Degree Name" AutoPostBack="True" OnSelectedIndexChanged="ddlCollegeName_SelectedIndexChanged" ClientIDMode="Static">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <asp:Label ID="lblDYDegree" runat="server" Font-Bold="true"></asp:Label>
                                                </div>
                                                <asp:DropDownList ID="ddlDegreeName" runat="server" TabIndex="2" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlDegreeName_SelectedIndexChanged" ToolTip="Degree Name" ClientIDMode="Static">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <asp:Label ID="lblDYDept" runat="server" Font-Bold="true"></asp:Label>
                                                </div>
                                                <asp:DropDownList ID="ddlDept" runat="server" TabIndex="3" AppendDataBoundItems="True" AutoPostBack="true" CssClass="form-control" data-select2-enable="true" ToolTip="Department" ClientIDMode="Static">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <asp:Label ID="lblDYBranch" runat="server" Font-Bold="true"></asp:Label>
                                                </div>
                                                <asp:DropDownList ID="ddlBranchName" runat="server" TabIndex="4" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" ToolTip="Branch Name" ClientIDMode="Static">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <asp:Label ID="lblDYBranchcode" runat="server" Font-Bold="true"></asp:Label>
                                                </div>
                                                <asp:TextBox ID="txtCode" runat="server" TabIndex="6" MaxLength="50" CssClass="form-control"
                                                    ToolTip="Code" ClientIDMode="Static" />
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtCode"
                                                    FilterType="Custom,LowerCaseLetters,UpperCaseLetters,Numbers" ValidChars="-()" FilterMode="ValidChars">
                                                </ajaxToolKit:FilteredTextBoxExtender>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <asp:Label ID="lblDYPrgmtype" runat="server" Font-Bold="true"></asp:Label>
                                                </div>
                                                <asp:DropDownList ID="ddlSection" runat="server" TabIndex="7" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                    ToolTip="Please Select Programme Type" ClientIDMode="Static">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="dvClassification">
                                                <div class="label-dynamic">

                                                    <sup>* </sup>
                                                    <asp:Label ID="lblClassification" runat="server" Font-Bold="true"></asp:Label>
                                                </div>
                                                <asp:DropDownList ID="ddlClassification" runat="server" TabIndex="7" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                    ToolTip="Please Select Classificaion" ClientIDMode="Static">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>


                                            </div>


                                            <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="dvSpecialization" visible="false">
                                                <div class="label-dynamic">
                                                    <%--<label >Is it Specialization</label>--%>
                                                    <sup>* </sup>
                                                    <asp:Label ID="lblSpecilization" runat="server" Font-Bold="true"></asp:Label>
                                                </div>
                                                <asp:RadioButtonList ID="rdoSpecilization" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="1"> YES &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                    <asp:ListItem Value="0"> NO &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                </asp:RadioButtonList>
                                                <%--       <asp:RequiredFieldValidator ID="rfvSpecialization" ErrorMessage="Please select Is it Specialization"
                                                    ControlToValidate="rdoSpecilization" runat="server" ValidationGroup="Branch" Display="None" />--%>
                                            </div>

                                            <!--======= input plus minus input added by gaurav 29072021 =======-->
                                            <div class="col-lg-3 col-md-6 col-12">
                                                <div class="row">
                                                    <div class="form-group col-lg-7 col-md-6 col-7">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <asp:Label ID="lblDYDuration" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <div class="input-group number">
                                                            <span class="input-group-text minus border-right-0">-</span>
                                                            <asp:TextBox ID="txtDuration" runat="server" TabIndex="8" ToolTip="Duration" CssClass="form-control font-weight-bold text-center duration" value="1" />
                                                            <%--<asp:RequiredFieldValidator ID="rfvDuration" runat="server" ControlToValidate="txtDuration"
                                                                Display="None" ErrorMessage="Please Enter Duration" ValidationGroup="Branch"
                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                                                            <asp:CompareValidator ID="cvDuration" runat="server" ErrorMessage="Please Enter Numeric Value for Duration"
                                                                ControlToValidate="txtDuration" Display="None" SetFocusOnError="true" Type="Double"
                                                                Operator="DataTypeCheck" ValidationGroup="Branch" />
                                                            <span class="input-group-text plus border-left-0">+</span>
                                                        </div>
                                                    </div>

                                                    <!--======= toggle switch added by gaurav 29072021 =======-->
                                                    <%--  <div class="form-group  col-lg-5 col-md-6 col-5">
                                                        <div class="label-dynamic">
                                                           <sup>* </sup>  <asp:Label ID="lblActive" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>--%>
                                                    <%--<asp:CheckBox ID="chkActive" runat="server" />--%>
                                                    <%-- <div class="switch form-inline">
                                                            <input type="checkbox" id="switch" name="switch" class="switch" checked/>
                                                            <label data-on="Active" data-off="Inactive" for="switch"></label>
                                                        </div>
                                                    </div>--%>
                                                </div>
                                            </div>
                                            <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <asp:Label ID="lblDyAffiliated" runat="server" Font-Bold="true"></asp:Label>
                                                </div>
                                                <asp:ListBox ID="ddlAffiliated" runat="server" AppendDataBoundItems="true" CssClass="form-control multi-select-demo"
                                                    SelectionMode="multiple" TabIndex="10"></asp:ListBox>
                                            </div>
                                            <div class="form-group col-lg-6 col-md-6 col-12">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <asp:Label ID="lblModulename" runat="server" Font-Bold="true">Program Title</asp:Label>
                                                </div>
                                                <asp:TextBox ID="txtModuleName" runat="server" TabIndex="6" MaxLength="250" CssClass="form-control"
                                                    ToolTip="Code" ClientIDMode="Static" />
                                                <%-- <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtCode"
                                                    FilterType="Custom,LowerCaseLetters,UpperCaseLetters,Numbers" ValidChars="-()" FilterMode="ValidChars">
                                                </ajaxToolKit:FilteredTextBoxExtender>--%>
                                            </div>
                                            <div class="form-group  col-lg-5 col-md-6 col-5">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <asp:Label ID="lblActive" runat="server" Font-Bold="true"></asp:Label>
                                                </div>
                                                <%--<asp:CheckBox ID="chkActive" runat="server" />--%>
                                                <div class="switch form-inline">
                                                    <input type="checkbox" id="switch" name="switch" class="switch" checked />
                                                    <label data-on="Active" data-off="Inactive" for="switch"></label>
                                                </div>
                                            </div>
                                        </div>



                                    </div>
                                </div>

                                <div class="col-12 btn-footer">


                                    <%--                                <asp:Button ID="btnSave" runat="server" Text="Submit" ToolTip="Submit" ValidationGroup="Branch" OnClientClick="return validate()"
                                    CssClass="btn btn-outline-info" OnClick="btnSave_Click" TabIndex="14" />--%>

                                    <asp:LinkButton ID="btnSave" runat="server" ToolTip="Submit" OnClientClick="return validate()"
                                        CssClass="btn btn-outline-info" OnClick="btnSave_Click" TabIndex="14" ClientIDMode="Static">Submit</asp:LinkButton>

                                    <asp:Button ID="btnReport" runat="server" Visible="false" TabIndex="15" Text="Report" OnClick="btnReport_Click"
                                        CssClass="btn btn-outline-primary" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                        CssClass="btn btn-outline-danger" OnClick="btnCancel_Click" TabIndex="16" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Branch"
                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                </div>

                                <div class="col-12 table table-responsive">
                                    <asp:Panel ID="Panel1" runat="server">
                                        <asp:ListView ID="lvBranch" runat="server">
                                            <LayoutTemplate>
                                                <div class="sub-heading" id="dem">
                                                    <h5><asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label> List</h5>
                                                </div>

                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="myTable1">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <%--<th>Delete
                                                        </th>--%>
                                                            
                                                            <th><asp:Label ID="lblAction" runat="server"></asp:Label>
                                                            </th>
                                                            <th><asp:Label ID="lblDYCollege" runat="server"></asp:Label>
                                                            </th>
                                                            <th><asp:Label ID="lblDYDept" runat="server"></asp:Label>
                                                            </th>
                                                            <th><asp:Label ID="lblProgram" runat="server"></asp:Label>
                                                            </th>
                                                            <th><asp:Label ID="lblDYBranch" runat="server"></asp:Label>
                                                            </th>
                                                            <th><asp:Label ID="lblDYBranchcode" runat="server"></asp:Label>
                                                            </th>
                                                            <th><asp:Label ID="lblDYPrgmtype" runat="server"></asp:Label>
                                                            </th>
                                                            <th><asp:Label ID="lblClassification" runat="server"></asp:Label>
                                                            </th>
                                                            <th><asp:Label ID="lblDYDuration" runat="server"></asp:Label>
                                                            </th>
                                                            <th><asp:Label ID="lblStatus" runat="server"></asp:Label>
                                                            </th>
                                                        </tr>
                                                        <thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <%--<td>
                                                    <asp:ImageButton ID="btnDel" runat="server" ImageUrl="~/IMAGES/delete.gif" CommandArgument='<%# Eval("CDBNO") %>'
                                                        AlternateText="Delete Record" OnClientClick="return ConfirmToDelete(this);" OnClick="btnDel_Click"
                                                        TabIndex="13" ToolTip="Delete" />
                                                </td>--%>
                                                    <td>
                                                        <asp:ImageButton ID="btnedit" runat="server" ImageUrl="~/IMAGES/edit.png" CommandArgument='<%# Eval("CDBNO") %>'
                                                            AlternateText="Delete Record" OnClick="btnedit_Click"
                                                            TabIndex="14" ToolTip="Edit" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCollege" runat="server" Text='<%# Eval("COLLEGE_NAME")%>' ToolTip='<%# Eval("COLLEGENAME")%>'></asp:Label>

                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblDepartment" runat="server" Text='<%# Eval("DEPTNAME")%>' ToolTip='<%# Eval("DEPT_NAME")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <%# Eval("DEGREENAME")%>
                                                    </td>

                                                    <td>
                                                        <%--    <%# Eval("LONGNAME") %>--%>
                                                        <%#Eval("SPECIALIZATION1") %>          <%--added by swapnil thakare on dated 09-07-2021--%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("CODE") %>
                                                    </td>

                                                    <td>
                                                        <%# Eval("UA_SECTIONNAME") %>
                                                    </td>

                                                    <td>
                                                        <%# Eval("AREA_INT_NAME") %>  <%--added by swapnil thakare on dated 28-07-2021--%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("DURATION") %>
                                                    </td>

                                                    <td>
                                                        <%# Eval("ACTIVE1") %>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>


                            </div>
                        </div>
                        <div id="divMsg" runat="server">
                        </div>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script src="../plugins/multiselect/bootstrap-multiselect.js" type="text/javascript"></script>

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

    <!--======= input plus minus script added by gaurav 29072021 =======-->
    <script>
        $(document).ready(function () {
            $('.minus').click(function () {
                var $input = $(this).parent().find('input');
                var count = parseInt($input.val()) - 1;
                count = count < 1 ? 1 : count;
                $input.val(count);
                $input.change();
                return false;
            });
            $('.plus').click(function () {
                var $input = $(this).parent().find('input');
                $input.val(parseInt($input.val()) + 1);
                $input.change();
                return false;
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $('.minus').click(function () {
                    var $input = $(this).parent().find('input');
                    var count = parseInt($input.val()) - 1;
                    count = count < 1 ? 1 : count;
                    $input.val(count);
                    $input.change();
                    return false;
                });
                $('.plus').click(function () {
                    var $input = $(this).parent().find('input');
                    $input.val(parseInt($input.val()) + 1);
                    $input.change();
                    return false;
                });
            });
        });
    </script>


    <script>
        function SetStat(val) {
            $('#switch').prop('checked', val);
        }

        var summary = "";

        $(function () {
            $('#btnSave').click(function () {
                localStorage.setItem("currentId", "#btnSave,Submit");
                ShowLoader('#btnSave');
                if ($('#ddlCollegeName').val() == "0")
                    summary += 'Please Select College.';
                if ($('#ddlDegreeName').val() == "0")
                    summary += '<br>Please Select Degree.';
                if ($('#ddlDept').val() == "0")
                    summary += '<br>Please Select Department.';
                if ($('#ddlBranchName').val() == "0")
                    summary += '<br>Please Select Program.';
                if ($('#txtCode').val() == "")
                    summary += '<br>Please Enter Program Code.';
                if ($('#ddlSection').val() == "0")
                    summary += '<br>Please Select Program Classification.';
                if ($('#ddlClassification').val() == "0")
                    summary += '<br>Please Select Classification.';
                if ($('#txtModuleName').val() == "")
                    summary += '<br>Please Insert Degree Full Name.';

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
                    ShowLoader('#btnSave');
                    if ($('#ddlCollegeName').val() == "0")
                        summary += 'Please Select College.';
                    if ($('#ddlDegreeName').val() == "0")
                        summary += '<br>Please Select Degree.';
                    if ($('#ddlDept').val() == "0")
                        summary += '<br>Please Select Department.';
                    if ($('#ddlBranchName').val() == "0")
                        summary += '<br>Please Select Program.';
                    if ($('#txtCode').val() == "")
                        summary += '<br>Please Enter Program Code.';
                    if ($('#ddlSection').val() == "0")
                        summary += '<br>Please Select Program Classification.';
                    if ($('#ddlClassification').val() == "0")
                        summary += '<br>Please Select Classification.';
                    if ($('#txtModuleName').val() == "")
                        summary += '<br>Please Insert Degree Full Name.';

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

</asp:Content>

