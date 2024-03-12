<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="WGPA_Configuration.aspx.cs" Inherits="Projects_WGPA_Configuration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
    <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>


    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title"><span>WGPA Configuration</span></h3>
                </div>

                <div class="box-body">
                    <div class="nav-tabs-custom">
                        <ul class="nav nav-tabs" role="tablist">
                            <li class="nav-item">
                                <a class="nav-link active" data-toggle="tab" href="#tab_1">WGPA Rule</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_2">WGPA Rule Allocation</a>
                            </li>

                        </ul>

                        <div class="tab-content">
                            <div class="tab-pane active" id="tab_1">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updRuleName"
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
                                <asp:UpdatePanel ID="updRuleName" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <%-- WGPA Rule TAB--%>
                                        <div class="col-12 mt-3">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>WGPA Rule Name</label>
                                                    </div>
                                                    <asp:TextBox ID="txtRuleName" runat="server" CssClass="form-control" onkeypress="allowAlphaNumericSpace(event)"/>
                                                    <asp:RequiredFieldValidator ID="rfvPdpCode" runat="server" ControlToValidate="txtRuleName"
                                                        Display="None" ValidationGroup="WGPARule"
                                                        ErrorMessage="Please Enter WGPA Rule Name." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-2 col-md-2 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Y1</label>
                                                    </div>
                                                    <asp:TextBox ID="txtY1" runat="server" CssClass="form-control" MaxLength="2" onkeypress="return functionx(event)"/>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtY1"
                                                        Display="None" ValidationGroup="WGPARule"
                                                        ErrorMessage="Please Enter Y1." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-2 col-md-2 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Y2</label>
                                                    </div>
                                                    <asp:TextBox ID="txtY2" runat="server" CssClass="form-control" MaxLength="2" onkeypress="return functionx(event)" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtY2"
                                                        Display="None" ValidationGroup="WGPARule"
                                                        ErrorMessage="Please Enter Y2." SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-2 col-md-2 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Y3</label>
                                                    </div>
                                                    <asp:TextBox ID="txtY3" runat="server" CssClass="form-control" MaxLength="2" onkeypress="return functionx(event)"/>
                                                </div>
                                                <div class="form-group col-lg-2 col-md-2 col-12">
                                                    <div class="label-dynamic">
                                                        <label>Y4</label>
                                                    </div>
                                                    <asp:TextBox ID="txtY4" runat="server" CssClass="form-control" MaxLength="2" />
                                                </div>

                                                <div class="col-12 btn-footer">
                                                    <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn btn-outline-info" OnClick="btnSubmit_Click" ValidationGroup="WGPARule">Submit</asp:LinkButton>
                                                    <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancel_Click">Cancel</asp:LinkButton>
                                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="WGPARule"
                                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                </div>
                                            </div>
                                        </div>

                                        <%--<div class="col-12 mt-3">
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>WGPA Rule Name</th>
                                                        <th>Y1</th>
                                                        <th>Y2</th>
                                                        <th>Y3</th>
                                                        <th>Y4</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <td>WGPA Rule Name</td>
                                                        <td>10%</td>
                                                        <td>20%</td>
                                                        <td>30%</td>
                                                        <td>40%</td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>--%>

                                        <div class="col-12">
                                            <asp:Panel ID="Panel1" runat="server">
                                                <asp:ListView ID="lvWGPARule" runat="server">
                                                    <LayoutTemplate>
                                                        <div id="demo-grid">
                                                            <div class="sub-heading" id="dem">
                                                                <h5>Details List</h5>
                                                            </div>

                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="my_Table">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>Edit</th>
                                                                        <th>WGPA Rule Name</th>
                                                                        <th>Y1</th>
                                                                        <th>Y2</th>
                                                                        <th>Y3</th>
                                                                        <th>Y4</th>
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
                                                                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("WGPA_RULE_ID") %>'
                                                                    AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                                            </td>
                                                            <td><%# Eval("WGPA_RULE_NAME")%></td>
                                                            <td><%# Eval("Y1")%></td>
                                                            <td><%# Eval("Y2")%></td>
                                                            <td><%# Eval("Y3")%></td>
                                                            <td><%# Eval("Y4")%></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>


                                        </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <%-- END --%>
                            </div>



                            <div class="tab-pane fade" id="tab_2">
                                <%-- WGPA Rule Allocation TAB--%>
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updWGPAAllocation"
                                        DynamicLayout="true" DisplayAfter="0">
                                        <ProgressTemplate>
                                            <div id="preloader">
                                                <div class="loader-container">
                                                    <div class="loader-container__bar"></div>
                                                    <div class="loader-container__bar"></div>
                                                    <div class="loader-container__bar"></div>
                                                    <div class="loader-container__bar"></div>
                                                    <div class="loader-container__bar"></div>
                                                    <div class="loader-container__bar"></div>
                                                </div>

                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                                <asp:UpdatePanel ID="updWGPAAllocation" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="col-12 mt-3">

                                            <div class="row">

                                                <div class="form-group col-lg-3 col-md-3 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Intake</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlIntake" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvIntake" runat="server" ControlToValidate="ddlIntake"
                                                        Display="None" ErrorMessage="Please Select Intake" InitialValue="0"
                                                        ValidationGroup="Allocation" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-3 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Faculty</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlFaculty" runat="server" CssClass="form-control" data-select2-enable="true"
                                                        AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvFaculty" runat="server" ControlToValidate="ddlFaculty"
                                                        Display="None" ErrorMessage="Please Select Faculty" InitialValue="0"
                                                        ValidationGroup="Allocation" />
                                                    <%--<asp:ListBox ID="lstbxFaculty" runat="server" CssClass="form-control multi-select-demo" SelectionMode="Multiple"></asp:ListBox>--%>
                                                </div>
                                                <%-- <div class="form-group col-lg-3 col-md-3 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Program</label>
                                                    </div>
                                                    <asp:ListBox ID="lstbxProgram" runat="server" CssClass="form-control multi-select-demo" SelectionMode="Multiple"></asp:ListBox>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-3 col-12">
                                                    <div class="label-dynamic">
                                                        <label>WGPA Rule</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlWGPARule" runat="server" CssClass="form-control" data-select2-enable="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>--%>

                                                <div class="col-12 btn-footer">
                                                    <asp:LinkButton ID="lnkCSubmit" runat="server" CssClass="btn btn-outline-info" OnClick="lnkCSubmit_Click" ValidationGroup="Allocation">Submit</asp:LinkButton>
                                                    <asp:LinkButton ID="lnkCCancel" runat="server" CssClass="btn btn-outline-danger" OnClick="lnkCCancel_Click">Cancel</asp:LinkButton>
                                                    <asp:ValidationSummary ID="vsAllocation" runat="server" ValidationGroup="Allocation"
                                                        ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                </div>
                                            </div>
                                        </div>

                                        <%--<div class="col-12 mt-3">
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Intake</th>
                                                        <th>Faculty</th>
                                                        <th>Program</th>
                                                        <th>WGPA Rule</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                </tbody>
                                            </table>
                                        </div>--%>

                                        <div class="col-12">
                                            <asp:Panel ID="Panel2" runat="server">
                                                <asp:ListView ID="lvWGPAAllocation" runat="server">
                                                    <LayoutTemplate>
                                                        <div id="demo-grid">
                                                            <div class="sub-heading" id="dem">
                                                                <h5>Details List</h5>
                                                            </div>

                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="my_Table">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <%--  <th>Edit</th>--%>
                                                                        <th>Program</th>
                                                                        <th>WGPA Rule</th>

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
                                                            <%--<td>
                                                                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("ProgramId") %>'
                                                                    AlternateText="Edit Record" ToolTip="Edit Record" />
                                                            </td>--%>
                                                            <%--  <td><%# Eval("SRNO")%></td>--%>


                                                            <td>
                                                                <asp:HiddenField ID="hdnSrNo" runat="server" Value='<%# Eval("SRNO")%>' />
                                                                <%# Eval("PROGRAMNAME")%>
                                                                <asp:HiddenField ID="hdnProgramId" runat="server" Value='<%# Eval("PROGRAMID")%>' />

                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlRuleName" runat="server" CssClass="form-control select2 select-click" AppendDataBoundItems="True">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList></td>
                                                            <asp:Label ID="lblRuleName" runat="server" Text='<% #Eval("WGPA_RULE_ID")%>' Visible="false"></asp:Label></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>


                                        </div>

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
        <script>
            $(document).ready(function () {

                var table = $('#my_Table').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 320,
                    scrollX: true,
                    scrollCollapse: true,

                    dom: 'lBfrtip',

                    //Export functionality
                    buttons: [
                        {
                            extend: 'colvis',
                            text: 'Column Visibility',
                            columns: function (idx, data, node) {
                                var arr = [0];
                                if (arr.indexOf(idx) !== -1) {
                                    return false;
                                } else {
                                    return $('#my_Table').DataTable().column(idx).visible();

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
                                                var arr = [0];
                                                if (arr.indexOf(idx) !== -1) {
                                                    return false;
                                                } else {
                                                    return $('#my_Table').DataTable().column(idx).visible();
                                                }
                                            }
                                        }
                                    },
                                    {
                                        extend: 'excelHtml5',
                                        exportOptions: {
                                            columns: function (idx, data, node) {
                                                var arr = [0];
                                                if (arr.indexOf(idx) !== -1) {
                                                    return false;
                                                } else {
                                                    return $('#my_Table').DataTable().column(idx).visible();
                                                }
                                            }
                                        }
                                    },
                                    {
                                        extend: 'pdfHtml5',
                                        exportOptions: {
                                            columns: function (idx, data, node) {
                                                var arr = [0];
                                                if (arr.indexOf(idx) !== -1) {
                                                    return false;
                                                } else {
                                                    return $('#my_Table').DataTable().column(idx).visible();
                                                }
                                            }
                                        }
                                    },
                            ]
                        }
                    ],
                    "bDestroy": true,
                });
            });
            var parameter = Sys.WebForms.PageRequestManager.getInstance();
            parameter.add_endRequest(function () {
                $(document).ready(function () {

                    var table = $('#my_Table').DataTable({
                        responsive: true,
                        lengthChange: true,
                        scrollY: 320,
                        scrollX: true,
                        scrollCollapse: true,

                        dom: 'lBfrtip',

                        //Export functionality
                        buttons: [
                            {
                                extend: 'colvis',
                                text: 'Column Visibility',
                                columns: function (idx, data, node) {
                                    var arr = [0];
                                    if (arr.indexOf(idx) !== -1) {
                                        return false;
                                    } else {
                                        return $('#my_Table').DataTable().column(idx).visible();
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
                                                    var arr = [0];
                                                    if (arr.indexOf(idx) !== -1) {
                                                        return false;
                                                    } else {
                                                        return $('#my_Table').DataTable().column(idx).visible();
                                                    }
                                                }
                                            }
                                        },
                                        {
                                            extend: 'excelHtml5',
                                            exportOptions: {
                                                columns: function (idx, data, node) {
                                                    var arr = [0];
                                                    if (arr.indexOf(idx) !== -1) {
                                                        return false;
                                                    } else {
                                                        return $('#my_Table').DataTable().column(idx).visible();
                                                    }
                                                }
                                            }
                                        },
                                        {
                                            extend: 'pdfHtml5',
                                            exportOptions: {
                                                columns: function (idx, data, node) {
                                                    var arr = [0];
                                                    if (arr.indexOf(idx) !== -1) {
                                                        return false;
                                                    } else {
                                                        return $('#my_Table').DataTable().column(idx).visible();
                                                    }
                                                }
                                            }
                                        },
                                ]
                            }
                        ],
                        "bDestroy": true,
                    });
                });
            });
        </script>
        <script>
            function allowAlphaNumericSpace(e) {
                var code = ('charCode' in e) ? e.charCode : e.keyCode;
                if (!(code == 32) && // space
                  !(code > 47 && code < 58) && // numeric (0-9)
                  !(code > 64 && code < 91) && // upper alpha (A-Z)
                  !(code > 96 && code < 123)) { // lower alpha (a-z)
                    e.preventDefault();
                    //    alert("Not Allowed Special Character..!");
                    return true;
                }

                else

                    return false;

            }

        </script>
        <script>
         function functionx(evt) {
                if (evt.charCode > 31 && (evt.charCode < 48 || evt.charCode > 57)) {
                //    alert("Enter Only Numeric Value ");
                    return false;
                }
         }
       </script>
</asp:Content>

