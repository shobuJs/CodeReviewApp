<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="TransfereeConfirmation.aspx.cs" Inherits="ACADEMIC_TransfereeConfirmation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href='<%=Page.ResolveUrl("~/plugins/newbootstrap/css/lead.css") %>' rel="stylesheet" />
    <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
    <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/FileSaver.js/2014-11-29/FileSaver.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.12.13/xlsx.full.min.js"></script>

    <style>
        .badge {
            display: inline-block;
            padding: 5px 10px 7px;
            border-radius: 15px;
            font-size: 100%;
            width: 80px;
        }

        .badge-warning {
            color: #fff !important;
        }
    </style>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">

                    <%--         <h3 class="box-title">Enrollment Confirmation</h3>  --%>
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Intake</label>
                                </div>
                                <asp:DropDownList ID="ddlIntake" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true" TabIndex="1">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="show" runat="server" ControlToValidate="ddlIntake"
                                    Display="None" ErrorMessage="Please Select Intake" InitialValue="0" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Study Level</label>
                                </div>
                                <asp:DropDownList ID="ddlStudyType" runat="server" AppendDataBoundItems="true" CssClass="form-control select2 select-clik" 
                                    TabIndex="2">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="show" runat="server" ControlToValidate="ddlStudyType"
                                    Display="None" ErrorMessage="Please Select Study Level" InitialValue="0" />
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                <div class="label-dynamic">
                                    <label>Program</label>
                                </div>
                                <asp:ListBox ID="ddlProgramIntrested" runat="server" CssClass="form-control multi-select-demo"
                                    SelectionMode="Multiple" AppendDataBoundItems="true" TabIndex="3"></asp:ListBox>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                <div class="label-dynamic">
                                    <label>Progress Level</label>
                                </div>
                                <asp:DropDownList ID="ddlProgreeLevel" runat="server" AppendDataBoundItems="true" CssClass="form-control select2 select-clik" TabIndex="4">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    <asp:ListItem Value="1">0%</asp:ListItem>
                                    <asp:ListItem Value="2">25%</asp:ListItem>
                                    <asp:ListItem Value="3">50%</asp:ListItem>
                                    <asp:ListItem Value="4">75%</asp:ListItem>
                                    <asp:ListItem Value="5">100%</asp:ListItem>
                                </asp:DropDownList>

                            </div>
                            <div class="col-12 btn-footer">
                                <asp:LinkButton ID="lnkButtonShow" runat="server" CssClass="btn btn-outline-info" ValidationGroup="show" OnClick="lnkButtonShow_Click">Show</asp:LinkButton>
                                <asp:LinkButton ID="lnkButtonCancel" runat="server" CssClass="btn btn-outline-danger" OnClick="lnkButtonCancel_Click">Cancel</asp:LinkButton>
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="show"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>

                            <div class="form-group col-lg-4 col-md-12 col-12">
                                <div class=" note-div">
                                    <h5 class="heading">Note </h5>
                                    <p><i class="fa fa-star" aria-hidden="true"></i><i class="fa fa-square" style="color: #007bff;" aria-hidden="true"></i><span>Eligible as per entry criteria </span></p>
                                    <p><i class="fa fa-star" aria-hidden="true"></i><i class="fa fa-square" style="color: #dc3545;" aria-hidden="true"></i><span>Not Eligible as per criteria </span></p>
                                    <p><i class="fa fa-star" aria-hidden="true"></i><i class="fa fa-square" style="color: #28a745;" aria-hidden="true"></i><span>Enrollment Confirmation</span> </p>
                                </div>
                            </div>

                            <div class="col-md-12 table table-responsive">
                                <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
                                    <asp:ListView ID="lvEnrollmentConfirmation" runat="server" Visible="true">
                                        <LayoutTemplate>
                                            <div>
                                                <%--<h4>Current Semester Subjects</h4>--%>
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
                                                <div class="table-responsive" style="max-height: 500px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="MainLeadTable">
                                                        <thead class="bg-light-blue" style="position: sticky; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                            <tr class="bg-light-blue">

                                                                <th>Application ID</th>
                                                                <th>Applicant Name</th>
                                                                <th>Emailid</th>
                                                                <th>Mobile No.</th>
                                                                <th>Program</th>
                                                                <%--<th>Aptitude Score</th>--%>
                                                                <%--<th>Admission Status</th>
                                                                <th>Student ID No.</th>
                                                                <th>Progress Level</th>
                                                                <th>Payment Status</th>--%>
                                                                <th>Evaluation Result</th>
                                                                <th>Test Recommended</th>
                                                                <th>Remarks</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr id="trCurRow" class="item">


                                                <td>
                                                    <a target="_blank" href='<%# Eval("USER_URL")%>'>

<%-- <a target="_blank" href="http://localhost:59566/PresentationLayer/ACADEMIC/TransfreePreview.aspx?pageno=3282&userno=1">--%>
                                                        <asp:Label ID="lnkApplicationNo" CssClass="PopUp" runat="server" Text='<%# Eval("USERNAME")%>' ToolTip='<%# Eval("USERNAME")%>'></asp:Label>
                                                    </a>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("FULLNAME") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("EMAILID") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("MOBILENO") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("PROGRMNAME") %>' />
                                                </td>
                                               <%-- <td>
                                                    <asp:Label ID="lblSub_Type" runat="server" Text='<%# Eval("TOTAL_MARKS") %>' />
                                                </td>--%>
                                                <%--<td>
                                                    <asp:Label ID="lblEligible" runat="server" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblEnrollmentNo" runat="server" Text='<%# Eval("ENROLLNO") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("PERCENTAGE_CALCULATION") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("PAY_STATUS") %>' />
                                                </td>--%>
                                                <td>  <asp:Label ID="Label1" runat="server" Text='<%# Eval("EVALUATION_NAME") %>' /> </td>
                                                <td>  <asp:Label ID="Label4" runat="server" Text='<%# Eval("TEST_RECOM") %>' /> </td>
                                                <td>  <asp:Label ID="Label5" runat="server" Text='<%# Eval("REVAL_REMARK") %>' /> </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                            <div class="col-12" id="divCount" runat="server" visible="false">
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

    <!-- MultiSelect Script -->
    <script type="text/javascript">
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200,
                enableFiltering: true,
                filterPlaceholder: 'Search'
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200,
                enableFiltering: true,
                filterPlaceholder: 'Search'
            });
        });

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
            var searchBar5 = document.querySelector('#FilterData');
            var table5 = document.querySelector('#MainLeadTable');

            //console.log(allRows);
            searchBar5.addEventListener('focusout', () => {
                toggleSearch(searchBar5, table5);
        });

        $(".saveAsExcel").click(function () {
               
            //if (confirm('Do You Want To Apply for New Program?') == true) {
            //    return false;
            //}
            var workbook = XLSX.utils.book_new();
            var allDataArray = [];
            allDataArray = makeTableArray(table5, allDataArray);
            var worksheet = XLSX.utils.aoa_to_sheet(allDataArray);
            workbook.SheetNames.push("Test");
            workbook.Sheets["Test"] = worksheet;
            XLSX.writeFile(workbook, "LeadData.xlsx");
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

