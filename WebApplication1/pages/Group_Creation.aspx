<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Group_Creation.aspx.cs" Inherits="Projects_Group_Creation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
    <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>

    <%--===== Data Table Script added by gaurav =====--%>
        <script>
            $(document).ready(function () {
                var table = $('#tab_creation').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 320,
                    scrollX: true,
                    scrollCollapse: true,
                    paging: false,

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
                                    return $('#tab_creation').DataTable().column(idx).visible();
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
                                                    return $('#tab_creation').DataTable().column(idx).visible();
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
                                                    return $('#tab_creation').DataTable().column(idx).visible();
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
                                                    return $('#tab_creation').DataTable().column(idx).visible();
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
                    var table = $('#tab_creation').DataTable({
                        responsive: true,
                        lengthChange: true,
                        scrollY: 320,
                        scrollX: true,
                        scrollCollapse: true,
                        paging:false,

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
                                        return $('#tab_creation').DataTable().column(idx).visible();
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
                                                        return $('#tab_creation').DataTable().column(idx).visible();
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
                                                        return $('#tab_creation').DataTable().column(idx).visible();
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
                                                        return $('#tab_creation').DataTable().column(idx).visible();
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

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">Group Creation</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <%--<label>Intake</label>--%>
                                   <b><asp:Label ID="lblDYAdmbatch" runat="server"></asp:Label></b> 
                                </div>
                                <asp:DropDownList ID="ddlIntake" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlIntake_SelectedIndexChanged" AutoPostBack="true" TabIndex="1">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlIntake"
                                                                Display="None" ErrorMessage="Please Select Intake." ValidationGroup="GroupCreation"
                                                                SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <%--<label>Faculty</label>--%>
                                   <b> <asp:Label ID="lblFaculty" runat="server"></asp:Label></b>
                                </div>
                                <asp:DropDownList ID="ddlFaculty" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlFaculty"
                                                                Display="None" ErrorMessage="Please Select Faculty /School Name." ValidationGroup="GroupCreation"
                                                                SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                               <%-- <asp:ListBox ID="lstbxFaculty" runat="server" CssClass="form-control multi-select-demo" SelectionMode="Multiple" AppendDataBoundItems="true" OnSelectedIndexChanged="lstbxFaculty_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>--%>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <%--<label>Study Level</label>--%>
                                   <b> <asp:Label ID="lblDYstudylevel" runat="server"></asp:Label></b>
                                </div>
                                <%--<asp:ListBox ID="lstbxStudyLevel" runat="server" CssClass="form-control multi-select-demo" SelectionMode="Multiple" AppendDataBoundItems="true" OnSelectedIndexChanged="lstbxStudyLevel_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>--%>
                           <asp:DropDownList ID="ddlStudyLevel" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlStudyLevel_SelectedIndexChanged" AutoPostBack="true" TabIndex="3">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlStudyLevel"
                                                                Display="None" ErrorMessage="Please Select Study Level." ValidationGroup="GroupCreation"
                                                                SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                 </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <%--<label>Program</label>--%>
                                    <b><asp:Label ID="lblDYDegree" runat="server" ></asp:Label></b>
                                </div>
                                <asp:ListBox ID="lstbxProgram" runat="server" CssClass="form-control multi-select-demo" SelectionMode="Multiple" AppendDataBoundItems="true" TabIndex="4"></asp:ListBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="lstbxProgram"
                                                                Display="None" ErrorMessage="Please Select Program." ValidationGroup="GroupCreation"
                                                                SetFocusOnError="True" InitialValue=""></asp:RequiredFieldValidator>
                            </div>
                            
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <asp:Panel ID="pnlGroupName" runat="server" Visible="false">
                                <div class="label-dynamic">
                                    <label>Group Name </label>
                                </div>
                                <asp:DropDownList ID="ddlGroupName" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    <%--<asp:ListItem Value="0">FOC-B-A1</asp:ListItem>
                                    <asp:ListItem Value="0">FOC-B-A2</asp:ListItem>--%>
                                </asp:DropDownList>
                                    </asp:Panel>
                            </div>
                            
                        </div>
                    </div>

                    <div class="col-12 btn-footer">
                        <asp:LinkButton ID="btnShow" runat="server" CssClass="btn btn-outline-primary" OnClick="btnShow_Click" ValidationGroup="GroupCreation" TabIndex="5">Show</asp:LinkButton>
                        <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn btn-outline-primary" OnClick="btnSubmit_Click" TabIndex="6">Submit</asp:LinkButton>
                         <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="GroupCreation" />
                        <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancel_Click" TabIndex="7">Cancel</asp:LinkButton>
                    </div>

                    <div class="col-12 mt-3">
                        <%-- <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                            <thead class="bg-light-blue">
                                <tr>
                                    <th></th>
                                    <th>Student ID</th>
                                    <th>Student Name</th>
                                    <th>Faculty</th>
                                    <th>Study Level</th>
                                    <th>Program</th>
                                    <th>Group Name</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td><asp:CheckBox ID="chkapp" runat="server" /></td>
                                    <td>SL000004</td>
                                    <td>Ajanta Mendis</td>
                                    <td>Faculty Name</td>
                                    <td>Study Level 1</td>
                                    <td>Program 1</td>
                                    <td>
                                        <asp:DropDownList ID="ddlGrupName" runat="server" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="0">FOC-B-A1</asp:ListItem>
                                            <asp:ListItem Value="0">FOC-B-A2</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </tbody>
                        </table>--%>
                        <asp:ListView ID="lvGroupCreation" runat="server" OnItemDataBound="lvGroupCreation_ItemDataBound">
                            <LayoutTemplate>
                                <table class="table table-striped table-bordered" style="width: 100%" id="tab_creation">
                                    <thead class="bg-light-blue">
                                        <tr>
                                            <th class="text-center"><asp:CheckBox ID="chkIdentityCard" runat="server" Text="" onClick="totAll(this);"
                                                                                  ToolTip="Select or Deselect All Records" /></th>
                                            <th>Stud Reg No</th>
                                            <th>Student Name</th>
                                            <th>Faculty</th>
                                            <th>Study Level</th>
                                            <th>Program</th>
                                            <th>Group Name</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr id="itemPlaceholder" runat="server" />
                                    </tbody>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>                                    
                                  <td> <asp:CheckBox ID="chkReport" runat="server" /></td>
                                    <td>
                                        <asp:Label ID="lblIDNO" runat="server" Text='<%# Eval("REGNO")%>'></asp:Label>
                                        <asp:HiddenField ID="hdnIDNO" runat="server" Value='<%# Eval("IDNO")%>' />
                                    </td>
                                    <td><%# Eval("STUDNAME")%></td>
                                    <td><%# Eval("COLLEGE_NAME")%></td>
                                    <td><%# Eval("UA_SECTIONNAME")%></td>
                                    <td><%# Eval("PROGRAM")%></td>
                                    <td><asp:DropDownList ID="ddlGrupName" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"></asp:DropDownList>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlGrupName"
                                                                Display="None" ErrorMessage="Please Select GroupName." ValidationGroup="GroupCreation"
                                                                SetFocusOnError="True" InitialValue=""></asp:RequiredFieldValidator>
                                    </td> 
                                    <asp:HiddenField ID="hdnGroupName" runat="server" Value='<%#Eval("BGROUPID") %>' />  
                                     <%--<asp:HiddenField ID="hdnGroupName" runat="server" Value='<%#Eval("ADMBATCH") %>' />     --%>                                                               
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
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


        function totAll(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (headchk.checked == true)
                        e.checked = true;
                    else {
                        e.checked = false;
                        headchk.checked = false;
                    }
                }

            }

        }

        //function test() {
        //    var searchBar = document.querySelector('#FilterData');
        //    var table = document.querySelector('#myTable1');

        //    //console.log(allRows);
        //    searchBar.addEventListener('keyup',() => {
        //        toggleSearch(searchBar, table);
        //});

    </script>

</asp:Content>

