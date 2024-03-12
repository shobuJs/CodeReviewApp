<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="StudentCompleteStatusList.aspx.cs" Inherits="ACADEMIC_StudentCompleteStatusList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/FileSaver.js/2014-11-29/FileSaver.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.12.13/xlsx.full.min.js"></script>
         <%--===== Data Table Script added by gaurav =====--%>
        <script>
            $(document).ready(function () {
                var table = $('#tblstdList').DataTable({
                    responsive: true,
                    lengthChange: true,
                    scrollY: 320,
                    scrollX: true,
                    scrollCollapse: true,
                    paging: false, // Added by Gaurav for Hide pagination

                    dom: 'lBfrtip',
                    buttons: [
                        {
                            extend: 'colvis',
                            text: 'Column Visibility',
                            columns: function (idx, data, node) {
                                var arr = [0];
                                if (arr.indexOf(idx) !== -1) {
                                    return false;
                                } else {
                                    return $('#tblstdList').DataTable().column(idx).visible();
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
                                                return $('#tblstdList').DataTable().column(idx).visible();
                                            }
                                        },
                                        format: {
                                            body: function (data, column, row, node) {
                                                var nodereturn;
                                                if ($(node).find("input:text").length > 0) {
                                                    nodereturn = "";
                                                    nodereturn += $(node).find("input:text").eq(0).val();
                                                }
                                                else if ($(node).find("input:checkbox").length > 0) {
                                                    nodereturn = "";
                                                    $(node).find("input:checkbox").each(function () {
                                                        if ($(this).is(':checked')) {
                                                            nodereturn += "On";
                                                        } else {
                                                            nodereturn += "Off";
                                                        }
                                                    });
                                                }
                                                else if ($(node).find("a").length > 0) {
                                                    nodereturn = "";
                                                    $(node).find("a").each(function () {
                                                        nodereturn += $(this).text();
                                                    });
                                                }
                                                else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                    nodereturn = "";
                                                    $(node).find("span").each(function () {
                                                        nodereturn += $(this).text();
                                                    });
                                                }
                                                else if ($(node).find("select").length > 0) {
                                                    nodereturn = "";
                                                    $(node).find("select").each(function () {
                                                        var thisOption = $(this).find("option:selected").text();
                                                        if (thisOption !== "Please Select") {
                                                            nodereturn += thisOption;
                                                        }
                                                    });
                                                }
                                                else if ($(node).find("img").length > 0) {
                                                    nodereturn = "";
                                                }
                                                else if ($(node).find("input:hidden").length > 0) {
                                                    nodereturn = "";
                                                }
                                                else {
                                                    nodereturn = data;
                                                }
                                                return nodereturn;
                                            },
                                        },
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
                                                return $('#tblstdList').DataTable().column(idx).visible();
                                            }
                                        },
                                        format: {
                                            body: function (data, column, row, node) {
                                                var nodereturn;
                                                if ($(node).find("input:text").length > 0) {
                                                    nodereturn = "";
                                                    nodereturn += $(node).find("input:text").eq(0).val();
                                                }
                                                else if ($(node).find("input:checkbox").length > 0) {
                                                    nodereturn = "";
                                                    $(node).find("input:checkbox").each(function () {
                                                        if ($(this).is(':checked')) {
                                                            nodereturn += "On";
                                                        } else {
                                                            nodereturn += "Off";
                                                        }
                                                    });
                                                }
                                                else if ($(node).find("a").length > 0) {
                                                    nodereturn = "";
                                                    $(node).find("a").each(function () {
                                                        nodereturn += $(this).text();
                                                    });
                                                }
                                                else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                    nodereturn = "";
                                                    $(node).find("span").each(function () {
                                                        nodereturn += $(this).text();
                                                    });
                                                }
                                                else if ($(node).find("select").length > 0) {
                                                    nodereturn = "";
                                                    $(node).find("select").each(function () {
                                                        var thisOption = $(this).find("option:selected").text();
                                                        if (thisOption !== "Please Select") {
                                                            nodereturn += thisOption;
                                                        }
                                                    });
                                                }
                                                else if ($(node).find("img").length > 0) {
                                                    nodereturn = "";
                                                }
                                                else if ($(node).find("input:hidden").length > 0) {
                                                    nodereturn = "";
                                                }
                                                else {
                                                    nodereturn = data;
                                                }
                                                return nodereturn;
                                            },
                                        },
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
                    var table = $('#tblstdList').DataTable({
                        responsive: true,
                        lengthChange: true,
                        scrollY: 320,
                        scrollX: true,
                        scrollCollapse: true,
                        paging: false, // Added by Gaurav for Hide pagination

                        dom: 'lBfrtip',
                        buttons: [
                            {
                                extend: 'colvis',
                                text: 'Column Visibility',
                                columns: function (idx, data, node) {
                                    var arr = [0];
                                    if (arr.indexOf(idx) !== -1) {
                                        return false;
                                    } else {
                                        return $('#tblstdList').DataTable().column(idx).visible();
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
                                                    return $('#tblstdList').DataTable().column(idx).visible();
                                                }
                                            },
                                            format: {
                                                body: function (data, column, row, node) {
                                                    var nodereturn;
                                                    if ($(node).find("input:text").length > 0) {
                                                        nodereturn = "";
                                                        nodereturn += $(node).find("input:text").eq(0).val();
                                                    }
                                                    else if ($(node).find("input:checkbox").length > 0) {
                                                        nodereturn = "";
                                                        $(node).find("input:checkbox").each(function () {
                                                            if ($(this).is(':checked')) {
                                                                nodereturn += "On";
                                                            } else {
                                                                nodereturn += "Off";
                                                            }
                                                        });
                                                    }
                                                    else if ($(node).find("a").length > 0) {
                                                        nodereturn = "";
                                                        $(node).find("a").each(function () {
                                                            nodereturn += $(this).text();
                                                        });
                                                    }
                                                    else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                        nodereturn = "";
                                                        $(node).find("span").each(function () {
                                                            nodereturn += $(this).text();
                                                        });
                                                    }
                                                    else if ($(node).find("select").length > 0) {
                                                        nodereturn = "";
                                                        $(node).find("select").each(function () {
                                                            var thisOption = $(this).find("option:selected").text();
                                                            if (thisOption !== "Please Select") {
                                                                nodereturn += thisOption;
                                                            }
                                                        });
                                                    }
                                                    else if ($(node).find("img").length > 0) {
                                                        nodereturn = "";
                                                    }
                                                    else if ($(node).find("input:hidden").length > 0) {
                                                        nodereturn = "";
                                                    }
                                                    else {
                                                        nodereturn = data;
                                                    }
                                                    return nodereturn;
                                                },
                                            },
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
                                                    return $('#tblstdList').DataTable().column(idx).visible();
                                                }
                                            },
                                            format: {
                                                body: function (data, column, row, node) {
                                                    var nodereturn;
                                                    if ($(node).find("input:text").length > 0) {
                                                        nodereturn = "";
                                                        nodereturn += $(node).find("input:text").eq(0).val();
                                                    }
                                                    else if ($(node).find("input:checkbox").length > 0) {
                                                        nodereturn = "";
                                                        $(node).find("input:checkbox").each(function () {
                                                            if ($(this).is(':checked')) {
                                                                nodereturn += "On";
                                                            } else {
                                                                nodereturn += "Off";
                                                            }
                                                        });
                                                    }
                                                    else if ($(node).find("a").length > 0) {
                                                        nodereturn = "";
                                                        $(node).find("a").each(function () {
                                                            nodereturn += $(this).text();
                                                        });
                                                    }
                                                    else if ($(node).find("span").length > 0 && !($(node).find(".select2").length > 0)) {
                                                        nodereturn = "";
                                                        $(node).find("span").each(function () {
                                                            nodereturn += $(this).text();
                                                        });
                                                    }
                                                    else if ($(node).find("select").length > 0) {
                                                        nodereturn = "";
                                                        $(node).find("select").each(function () {
                                                            var thisOption = $(this).find("option:selected").text();
                                                            if (thisOption !== "Please Select") {
                                                                nodereturn += thisOption;
                                                            }
                                                        });
                                                    }
                                                    else if ($(node).find("img").length > 0) {
                                                        nodereturn = "";
                                                    }
                                                    else if ($(node).find("input:hidden").length > 0) {
                                                        nodereturn = "";
                                                    }
                                                    else {
                                                        nodereturn = data;
                                                    }
                                                    return nodereturn;
                                                },
                                            },
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
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updSection"
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
    <asp:UpdatePanel ID="updSection" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                            <%--<h3 class="box-title"><span>SECTION/CLASS ROLLNO ALLOTMENT</span></h3>--%>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblICAcadSession" runat="server" Font-Bold="true"></asp:Label>
                                            <%--<label> College & Curriculum </label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlsession" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            ValidationGroup="teacherallot">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlsession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="Show">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                       <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                    
                                            <asp:Label ID="lblDYCollege" runat="server" Font-Bold="true"></asp:Label>
                                            <%--<label> College & Curriculum </label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlfaculty" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            ValidationGroup="teacherallot" OnSelectedIndexChanged="ddlfaculty_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                       
                                    </div>
                                       <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                  
                                            <asp:Label ID="lblprogram" runat="server" Font-Bold="true"></asp:Label>
                                            <%--<label> College & Curriculum </label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlpgm" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            ValidationGroup="teacherallot" >
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                     
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <%--  <label> Semester / Year </label>--%>
                                            <asp:Label ID="lblDYSemester" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true" ValidationGroup="teacherallot" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                      <%--  <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Semester"ValidationGroup="Show">
                                        </asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" Text="Show" CssClass="btn btn-outline-info" ValidationGroup="Show"/>
                                  <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-outline-danger" OnClick="btnCancel_Click" />
                                  <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Show"
                                                ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>
                            <div class="col-12">
                                <asp:Panel ID="pnlStudent" runat="server">
                                    <div runat="server" visible="false" id="divCount">
                                           <asp:Label ID="Label" runat="server" Font-Bold="true">Total Count :</asp:Label> <asp:Label ID="lblCount" runat="server" Font-Bold="true"></asp:Label>
                                    </div>
                                    <asp:ListView ID="lvStudents" runat="server">
                                        <LayoutTemplate>
                                            <div class="row mb-1">
                                                <div class="col-lg-2 col-md-6 offset-lg-7">
                                                    <button type="button" class="btn btn-outline-primary float-lg-right saveAsExcel">Export Excel</button>
                                                </div>


                                                <div class="col-lg-3 col-md-6">
                                                    <div class="input-group sea-rch">
                                                        <input type="text" id="FilterData" class="form-control" placeholder="Search" />
                                                        <div class="input-group-addon">
                                                            <i class="fa fa-search"></i>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                            <div class="table-responsive" style="height: 350px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="MainLeadTable">
                                                    <thead class="bg-light-blue" style="position: sticky; top: 0; z-index: 4; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                        <tr>
                                                            <th>Student ID</th>
                                                            <th>Name</th>
                                                            <th>Program</th>
                                                            <th>Register Type</th>
                                                            <th>Step 1 Student Details</th>
                                                            <th>Step 1 Date </th>
                                                            <th>Step 2 Module</th>
                                                        <%--    <th>Step 2 Date</th>--%>
                                                            <th>Step 3 Payment</th>
                                                            <th>Step 3 Date</th>
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
                                                    <%#Eval("STUDENT_ID")%>
                                                </td>
                                                <td>
                                                    <%#Eval("NAME_WITH_INITIAL")%>
                                                </td>
                                                <td><%#Eval("PGM")%></td>
                                                <td>
                                                    <%# Eval("STATUS")%>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblStatus" Font-Bold="true" Text='<%# (Eval("DEMAND_STATUS"))%>' ForeColor='<%# (Convert.ToInt32(Eval("DEMAND_STATUS_NO") )== 1 ?  System.Drawing.Color.Green : System.Drawing.Color.Red )%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <%# Eval("DEMAND_DATE")%> 
                                                </td>
                                                <td>
                                         
                                                    <asp:Label runat="server" ID="Label1" Font-Bold="true" Text='<%# (Eval("COURSE_REGISTER"))%>' ForeColor='<%# (Convert.ToInt32(Eval("COURSE_REGISTER_NO") )== 1 ?  System.Drawing.Color.Green : System.Drawing.Color.Red )%>'></asp:Label>
                                                </td>
                                    
                                                <td>
                                                   
                                                   <asp:Label runat="server" ID="Label2" Font-Bold="true" Text='<%# (Eval("RECON_STATUS"))%>' ForeColor='<%# (Convert.ToInt32(Eval("RECON_STATUS_NO") )== 1 ?  System.Drawing.Color.Green : System.Drawing.Color.Red )%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <%# Eval("RECON_DATE")%> 
                                                </td>
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
    </asp:UpdatePanel>
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
                var searchBar5 = document.querySelector('#FilterData');
                var table5 = document.querySelector('#MainLeadTable');



                //console.log(allRows);
                searchBar5.addEventListener('focusout', () => {
                    toggleSearch(searchBar5, table5);
            });



            $(".saveAsExcel").click(function () {

                //if (confirm('Do You Want To Apply for New Program?') == true) {
                // return false;
                //}
                var workbook = XLSX.utils.book_new();
                var allDataArray = [];
                allDataArray = makeTableArray(table5, allDataArray);
                var worksheet = XLSX.utils.aoa_to_sheet(allDataArray);
                workbook.SheetNames.push("Test");
                workbook.Sheets["Test"] = worksheet;
                XLSX.writeFile(workbook, "StudentCompleteStatus.xlsx");
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

