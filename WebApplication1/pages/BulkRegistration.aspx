<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="BulkRegistration.aspx.cs" Inherits="ACADEMIC_BulkRegistration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<script runat="server"></script>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updBulkReg"
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
    <script>
        $(document).ready(function () {
            var table = $('#tblHead').DataTable({
                responsive: true,
                lengthChange: true,
                scrollY: 320,
                scrollX: true,
                scrollCollapse: true,
                paging: false,

                aLengthMenu: [
                        [100, 200, 500, 1000, ],
                        [100, 200, 500, 1000, "All"]
                ],

                dom: 'lBfrtip',
                //Export functionality
                buttons: [
                {
                    extend: 'colvis',
                    text: 'Column Visibility',
                    columns: function (idx, data, node) {
                        var arr = [];
                        if (arr.indexOf(idx) !== -1) {
                            return false;
                        } else {
                            return $('#tblHead').DataTable().column(idx).visible();
                        }
                    }
                },
                {
                    extend: 'collection',
                    text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                    buttons: [
                    {
                        extend: 'copyHtml5',
                        exportOptions: {
                            columns: function (idx, data, node) {
                                var arr = [];
                                if (arr.indexOf(idx) !== -1) {
                                    return false;
                                } else {
                                    return $('#tblHead').DataTable().column(idx).visible();
                                }
                            }
                        }
                    },
                    {
                        extend: 'excelHtml5',
                        exportOptions: {
                            columns: function (idx, data, node) {
                                var arr = [];
                                if (arr.indexOf(idx) !== -1) {
                                    return false;
                                } else {
                                    return $('#tblHead').DataTable().column(idx).visible();
                                }
                            }
                        }
                    },
                    {
                        extend: 'pdfHtml5',
                        exportOptions: {
                            columns: function (idx, data, node) {
                                var arr = [];
                                if (arr.indexOf(idx) !== -1) {
                                    return false;
                                } else {
                                    return $('#tblHead').DataTable().column(idx).visible();
                                }
                            }
                        }
                    },
                    ]
                }
                ],
                "bDestroy": true,
                //order : "desc",
            });



            $('#ctl00_ContentPlaceHolder1_lvStudent_cbHead').on('click', function () {
                // Get all rows with search applied
                var rows = table.rows({ 'search': 'applied' }).nodes();
                // Check/uncheck checkboxes for all rows in the table
                $('input[type="checkbox"]', rows).prop('checked', this.checked);
            });



            // Handle click on checkbox to set state of "Select all" control
            $('#tblHead tbody').on('change', 'input[type="checkbox"]', function () {
                // If checkbox is not checked
                if (!this.checked) {
                    var el = $('#ctl00_ContentPlaceHolder1_lvStudent_cbHead').get(0);
                    // If "Select all" control is checked and has 'indeterminate' property
                    if (el && el.checked && ('indeterminate' in el)) {
                        // Set visual state of "Select all" control
                        // as 'indeterminate'
                        el.indeterminate = true;
                    }
                }
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                var table = $('#tblHead').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 320,
                    scrollX: true,
                    scrollCollapse: true,
                    paging: false,

                    aLengthMenu: [
                        [100, 200, 500, 1000, ],
                        [100, 200, 500, 1000, "All"]
                    ],

                    dom: 'lBfrtip',
                    //Export functionality
                    buttons: [
                    {
                        extend: 'colvis',
                        text: 'Column Visibility',
                        columns: function (idx, data, node) {
                            var arr = [];
                            if (arr.indexOf(idx) !== -1) {
                                return false;
                            } else {
                                return $('#tblHead').DataTable().column(idx).visible();
                            }
                        }
                    },
                    {
                        extend: 'collection',
                        text: '<i class="glyphicon glyphicon-export icon-share"></i> Export',
                        buttons: [
                        {
                            extend: 'copyHtml5',
                            exportOptions: {
                                columns: function (idx, data, node) {
                                    var arr = [];
                                    if (arr.indexOf(idx) !== -1) {
                                        return false;
                                    } else {
                                        return $('#tblHead').DataTable().column(idx).visible();
                                    }
                                }
                            }
                        },
                        {
                            extend: 'excelHtml5',
                            exportOptions: {
                                columns: function (idx, data, node) {
                                    var arr = [];
                                    if (arr.indexOf(idx) !== -1) {
                                        return false;
                                    } else {
                                        return $('#tblHead').DataTable().column(idx).visible();
                                    }
                                }
                            }
                        },
                        {
                            extend: 'pdfHtml5',
                            exportOptions: {
                                columns: function (idx, data, node) {
                                    var arr = [];
                                    if (arr.indexOf(idx) !== -1) {
                                        return false;
                                    } else {
                                        return $('#tblHead').DataTable().column(idx).visible();
                                    }
                                }
                            }
                        },
                        ]
                    }
                    ],
                    "bDestroy": true,
                    //order : "desc",
                });



                $('#ctl00_ContentPlaceHolder1_lvStudent_cbHead').on('click', function () {
                    // Get all rows with search applied
                    var rows = table.rows({ 'search': 'applied' }).nodes();
                    // Check/uncheck checkboxes for all rows in the table
                    $('input[type="checkbox"]', rows).prop('checked', this.checked);
                });



                // Handle click on checkbox to set state of "Select all" control
                $('#tblHead tbody').on('change', 'input[type="checkbox"]', function () {
                    // If checkbox is not checked
                    if (!this.checked) {
                        var el = $('#ctl00_ContentPlaceHolder1_lvStudent_cbHead').get(0);
                        // If "Select all" control is checked and has 'indeterminate' property
                        if (el && el.checked && ('indeterminate' in el)) {
                            // Set visual state of "Select all" control
                            // as 'indeterminate'
                            el.indeterminate = true;
                        }
                    }
                });
            });
        });
    </script>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                <%--    <h3 class="box-title"><span>BULK SUBJECT REGISTRATION</span>--%>
               <h3 class="box-title"><asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        <%--<small style="color: Red;"> (Disabled Checkboxes indicated that Students are already Registered..!!!) </small>--%>                  
                </div>
                <asp:UpdatePanel ID="updBulkReg" runat="server">
                    <ContentTemplate>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Faculty /School Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="true" TabIndex="1" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged"
                                            AutoPostBack="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvcollege" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" SetFocusOnError="true" ErrorMessage="Please Select College & Curriculum" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Intake</label>
                                        </div>
                                        <asp:DropDownList ID="ddlAdmBatch" TabIndex="2" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Session</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" TabIndex="3"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none;">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Department</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDept" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                            CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="4" AppendDataBoundItems="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                        <div class="label-dynamic">
                                            <label>Curriculum Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSchemeType" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                            AutoPostBack="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Branch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" TabIndex="5" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                            InitialValue="0" Display="None" ValidationGroup="submit" ErrorMessage="Please Select Branch"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Curriculum</label>
                                        </div>
                                        <asp:DropDownList ID="ddlScheme" runat="server" TabIndex="6" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                            InitialValue="0" Display="None" ValidationGroup="submit" ErrorMessage="Please Select Curriculum"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Semester/Year</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" runat="server" TabIndex="7" AppendDataBoundItems="true" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                        <div class="label-dynamic">
                                            <label>Student Status</label>
                                        </div>
                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                            <asp:ListItem Value="0">Regular Student</asp:ListItem>
                                            <asp:ListItem Value="1">Absorption Student</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Section</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSection" TabIndex="8" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="rfvsection" runat="server" ControlToValidate="ddlSection"
                                            Display="None" ErrorMessage="Please Select Section" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Status </label>
                                        </div>
                                        <asp:DropDownList ID="ddlRegisttype" TabIndex="8" runat="server" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Complete</asp:ListItem>
                                            <asp:ListItem Value="2">InComplete</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:RequiredFieldValidator ID="rfvsection" runat="server" ControlToValidate="ddlSection"
                                            Display="None" ErrorMessage="Please Select Section" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <asp:HiddenField ID="hftot" runat="server" />
                                            <label>Total Students Selected</label>
                                        </div>
                                        <asp:TextBox ID="txtTotStud" runat="server" Text="0" Enabled="False" CssClass="form-control" data-select2-enable="true"
                                            Style="text-align: center;" BackColor="#FFFFCC" Font-Bold="True"
                                            ForeColor="#000066"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-lg-6 col-md-12 col-12">
                                        <div class="label-dynamic">
                                            <label>Sort By</label>
                                        </div>
                                        <asp:RadioButtonList runat="server" ID="rbSortBy" RepeatDirection="Horizontal" TabIndex="9" AutoPostBack="true" OnSelectedIndexChanged="rbSortBy_SelectedIndexChanged">
                                            <asp:ListItem Value="1" Selected="True" Text="&nbsp;Student Id&nbsp;&nbsp;"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="&nbsp;Application No.&nbsp;&nbsp;"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="&nbsp;Student Name&nbsp;&nbsp;"></asp:ListItem>
                                        </asp:RadioButtonList>
                                         <%-- <asp:RadioButton ID="rbSortByEnrollno" runat="server" Text="Student Id" AutoPostBack="true" OnCheckedChanged="rbSortByEnrollno_CheckedChanged" GroupName="sortby" Checked="true" />&nbsp;&nbsp;&nbsp;
                                         <asp:RadioButton ID="rbSortByRegno" runat="server" Text="Application No." AutoPostBack="true" OnCheckedChanged="rbSortByRegno_CheckedChanged" GroupName="sortby" />--%>
                                    </div>

                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:LinkButton ID="btnShow" runat="server" OnClick="btnShow_Click" TabIndex="10"
                                    Text="Show Student" ValidationGroup="submit" class="btn btn-outline-info"><i class=" fa fa-eye"></i> Show</asp:LinkButton>
                                <asp:LinkButton ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" OnClientClick="return validateAssign();"
                                    TabIndex="11" Text="Submit" Enabled="false" ValidationGroup="submit" CssClass="btn btn-outline-info"><i class=" fa fa-save"></i> Submit</asp:LinkButton>
                                <asp:LinkButton ID="btnReport" runat="server" OnClick="btnReport_Click"
                                    TabIndex="12" Text="Registration Slip Report" ValidationGroup="submit" CssClass="btn btn-outline-primary"><i class="fa fa-file-pdf-o" aria-hidden="true"></i> Registration Slip Report</asp:LinkButton>

                                <asp:Button ID="btnCancel" runat="server" TabIndex="13" CausesValidation="False" Text="Cancel"
                                    CssClass="btn btn-outline-danger" OnClick="btnCancel_Click" Font-Bold="True" />

                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                    ValidationGroup="submit" ShowSummary="false" />
                            </div>

                            <div class="col-12">
                                <div class="row">
                                    <div class="col-lg-6 col-md-12 col-12 mt-3">
                                        <asp:Panel ID="pnlStudents" runat="server" Visible="true">
                                            <asp:ListView ID="lvStudent" runat="server">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Student List</h5>
                                                    </div>

                                                    <table class="table table-striped table-bordered nowrap " style="width: 100%" id="tblHead">
                                                        <thead class="bg-light-blue">
                                                            <tr id="trRow">
                                                                <th>
                                                                    <asp:CheckBox ID="cbHead" TabIndex="11" runat="server" onclick="SelectAll(this)" ToolTip="Select/Select all" />
                                                                </th>
                                                                <th>Student Id
                                                                </th>
                                                                <th>Application No.
                                                                </th>
                                                                <th>Student Name
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
                                                        <td>
                                                            <asp:CheckBox ID="cbRow" runat="server" TabIndex="12" ToolTip='<%# Eval("IDNO") %>' onClick="totStudents(this);" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblRollno" runat="server" Text='<%# Eval("ENROLLNO") %>' ToolTip='<%# Eval("IDNO") %>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblUSNno" runat="server" Text='<%# Eval("REGNO") %>' ToolTip='<%# Eval("IDNO") %>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblStudName" runat="server" Text='<%# Eval("NAME") %>' ToolTip='<%# Eval("REGISTERED") %>' />
                                                            <asp:Label ID="lblIDNo" runat="server" Text='<%# Eval("REGNO") %>' ToolTip='<%# Eval("IDNO") %>'
                                                                Visible="false" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>

                                        </asp:Panel>
                                    </div>
                                    <div class="col-lg-6 col-md-12 col-12 mt-3">
                                        <asp:Panel ID="pnlCourses" runat="server" Visible="true">
                                            <asp:ListView ID="lvCourse" runat="server" OnItemDataBound="lvCourse_ItemDataBound">
                                                <LayoutTemplate>
                                                    <div id="divlvPaidReceipts">
                                                        <div class="sub-heading">
                                                            <h5>Offered Subject (Core Subject)</h5>
                                                        </div>

                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblOffeCourse">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>
                                                                        <asp:CheckBox ID="cbHead1" runat="server" ToolTip="Select/Select all" onclick="SelectAll1(this,1,'cbRow');" Enabled="false" Checked="true" />
                                                                        <%-- Select--%>
                                                                    </th>
                                                                    <th>Subject Code - Subject Name
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
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="cbRow" TabIndex="13" runat="server" Checked="true" onclick="ChkHeader(1,'cbHead1','cbRow');" Enabled="false" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO") %>' />
                                                            &nbsp;
                                                            <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' ToolTip='<%# Eval("ELECT") %>' />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr class="altitem" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFD2'">
                                                        <td>
                                                            <asp:CheckBox ID="cbRow" runat="server" Checked="true" Enabled="false" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO") %>' />&nbsp;
                                                                            <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' ToolTip='<%# Eval("ELECT") %>' />
                                                        </td>
                                                    </tr>
                                                </AlternatingItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12">
                                <asp:Panel ID="pnlStudentsReamin" runat="server" Visible="true">
                                    <asp:ListView ID="lvStudentsRemain" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Student List (Demand Not Found)</h5>
                                            </div>

                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="">
                                                <thead class="bg-light-blue">
                                                    <tr id="trRow">
                                                        <th>HT No.
                                                        </th>
                                                        <th>Student Name
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
                                                <td>
                                                    <asp:Label ID="lblRollno" runat="server" Text='<%# Eval("ROLLNO") %>' ToolTip='<%# Eval("ROLLNO") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblStudName" runat="server" Text='<%# Eval("NAME") %>' />
                                                    <asp:Label ID="lblIDNo" runat="server" Text='<%# Eval("IDNO") %>' ToolTip='<%# Eval("REGNO") %>'
                                                        Visible="false" />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>

                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="ddlCollege" />
                        <asp:PostBackTrigger ControlID="ddlSession" />
                        <asp:PostBackTrigger ControlID="ddlSemester" />
                        <asp:PostBackTrigger ControlID="ddlSection" />
                        <asp:PostBackTrigger ControlID="btnShow" />  
                        <asp:PostBackTrigger ControlID="rbSortBy" /> 
                        <asp:PostBackTrigger ControlID="btnSubmit" />
                        <asp:PostBackTrigger ControlID="btnReport" />
                        

                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>

        <div id="divMsg" runat="server">
        </div>
    </div>

    <script type="text/javascript" language="javascript">

        function SelectAll1(headerid, headid1, chk) {
            var tbl = '';
            var list = '';
            if (headid1 == 1) {
                tbl = document.getElementById('tblOffeCourse');
                list = 'lvCourse';
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

        function SelectAll(headchk) {
            var i = 0;
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');
            var hftot = document.getElementById('<%= hftot.ClientID %>').value;
            var count = 0;
            for (i = 0; i < Number(hftot) ; i++) {

                var lst = document.getElementById('ctl00_ContentPlaceHolder1_lvStudent_ctrl' + i + '_cbRow');
                if (lst.type == 'checkbox') {
                    if (headchk.checked == true) {
                        if (lst.disabled == false) {
                            lst.checked = true;
                            count = count + 1;

                        }
                    }
                    else {
                        lst.checked = false;
                    }
                    txtTot.value = count;
                }
            }

            if (headchk.checked == true) {
                document.getElementById('<%= txtTotStud.ClientID %>').value = count;
            }
            else {
                document.getElementById('<%= txtTotStud.ClientID %>').value = 0;
            }
        }

        function validateAssign() {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>').value;
            if (txtTot == 0) {
                alert('Please Check atleast one student ');
                return false;
            }
            else
                return true;
        }

        function totStudents(chk) {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');

            if (chk.checked == true)
                txtTot.value = Number(txtTot.value) + 1;
            else
                txtTot.value = Number(txtTot.value) - 1;
        }

    </script>
</asp:Content>
