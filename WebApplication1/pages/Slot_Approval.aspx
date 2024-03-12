<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Slot_Approval.aspx.cs" Inherits="Slot_Approval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
    <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/FileSaver.js/2014-11-29/FileSaver.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.12.13/xlsx.full.min.js"></script>

    <div>
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updApprove"
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
    <asp:UpdatePanel ID="updApprove" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title"><span>Slot Approval</span></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Intake</label>
                                        </div>
                                        <asp:DropDownList ID="ddlIntake" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlIntake"
                                            Display="None" ErrorMessage="Please Select Intake" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>From To Date</label>
                                        </div>
                                        <div id="picker" class="form-control" tabindex="3">
                                            <i class="fa fa-calendar"></i>&nbsp;
                                            <span id="date"></span>
                                            <i class="fa fa-angle-down" aria-hidden="true" style="float: right; padding-top: 4px; font-weight: bold;"></i>
                                        </div>
                                        <asp:HiddenField ID="hdfDate" runat="server" Value="0" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <label>Status</label>
                                        </div>
                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Approved</asp:ListItem>
                                            <asp:ListItem Value="2">Pending</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:LinkButton ID="btnShow" runat="server" CssClass="btn btn-outline-info" OnClick="btnShow_Click" ValidationGroup="Show">Show</asp:LinkButton>
                                <asp:LinkButton ID="btnApprove" runat="server" CssClass="btn btn-outline-info" OnClick="btnApprove_Click" ValidationGroup="Show">Approve</asp:LinkButton>
                                <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancel_Click">Cancel</asp:LinkButton>
                                <asp:ValidationSummary ID="ValidationSummary4" runat="server" DisplayMode="List" ShowMessageBox="True"
                                    ShowSummary="False" ValidationGroup="Show" />
                            </div>

                            <div class="col-12 mt-3">
                                <asp:ListView ID="lvListApv" runat="server">
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                            <h5>Student Detail</h5>
                                        </div>

                                        <div class="row mb-1">
                                            <div class="col-lg-3 col-md-6 offset-lg-9">
                                                <div class="input-group sea-rch">
                                                    <input type="text" id="FilterData" class="form-control" placeholder="Search" />
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-search"></i>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="table-responsive" style="height: 300px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                            <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="table1">
                                                <thead class="bg-light-blue" style="position: sticky; z-index: 1; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                    <tr>
                                                    <tr>
                                                        <th>
                                                            <asp:CheckBox ID="chkHead" runat="server" onclick="totAll(this)" /></th>
                                                        <th>Reg. No.</th>
                                                        <th>Student Name</th>
                                                        <th>Slot</th>
                                                        <th>Content</th>
                                                        <th>Status</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server"/>
                                                </tbody>
                                            </table>
                                        </div>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkRow" runat="server" Checked='<%# (Eval("USER_STATUS").ToString() == "0" || Eval("USER_STATUS").ToString() == "2") ? false : true %>' Enabled='<%# (Eval("USER_STATUS").ToString() == "0" || Eval("USER_STATUS").ToString() == "2") ? true : false %>'/>
                                                <asp:Label ID="lblSlotNo" runat="server" Visible="false" Text='<%#Eval("SLOT_BOOK_NO") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblRegno" runat="server" Text='<%# Eval("REGNO") %>' ToolTip='<%# Eval("IDNO") %>'></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lblName" runat="server" Text='<%# Eval("NAME_WITH_INITIAL") %>'></asp:Label></td>
                                            <td>
                                                <asp:Label ID="lblSlot" runat="server" Text='<%# Eval("SLOT_DATE")%>'></asp:Label></td>
                                            <td>
                                                <asp:ListBox ID="lstActivity" runat="server" SelectionMode="Multiple" CssClass="form-control multi-select-demo"></asp:ListBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblStatus" runat="server" Text='<%# (Eval("USER_STATUS").ToString() == "0" || Eval("USER_STATUS").ToString() == "2") ? "Pending" : "Approve" %>' ForeColor='<%# (Eval("USER_STATUS").ToString() == "0" || Eval("USER_STATUS").ToString() == "2") ? System.Drawing.Color.Red : System.Drawing.Color.Green %>'></asp:Label></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnShow" />
        </Triggers>
    </asp:UpdatePanel>
    <!-- ========= Daterange curriculam ========== -->

    <script type="text/javascript">
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200,
                enableFiltering: true,
                filterPlaceholder: 'Search',
                enableCaseInsensitiveFiltering: true,
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
                    enableCaseInsensitiveFiltering: true,
                });
            });
        });
    </script>

    <script>
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
    </script>
    .
    <script type="text/javascript">
        $(document).ready(function () {
            $('#picker').daterangepicker({
                startDate: moment().subtract(00, 'days'),
                endDate: moment(),
                locale: {
                    format: 'DD MMM, YYYY'
                },
                //also comment "range" in daterangepicker.js('<div class="ranges"></div>' +)
                ranges: {
                    //                    'Today': [moment(), moment()],
                    //                    'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                    //                    'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                    //                    'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                    //                    'This Month': [moment().startOf('month'), moment().endOf('month')],
                    //                    'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')] 
                },
                //<!-- ========= Disable dates after today ========== -->
                //maxDate: new Date(),
                //<!-- ========= Disable dates after today END ========== -->
            },
        function (start, end) {
            $('#date').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
            document.getElementById('<%=hdfDate.ClientID%>').value = (start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'));
        });

            //$('#date').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
            //document.getElementById('<%=hdfDate.ClientID%>').value = (moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(document).ready(function () {
                $('#picker').daterangepicker({
                    startDate: moment().subtract(00, 'days'),
                    endDate: moment(),
                    locale: {
                        format: 'DD MMM, YYYY'
                    },
                    //also comment "range" in daterangepicker.js('<div class="ranges"></div>' +)
                    ranges: {
                        //                    'Today': [moment(), moment()],
                        //                    'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                        //                    'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                        //                    'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                        //                    'This Month': [moment().startOf('month'), moment().endOf('month')],
                        //                    'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')] 
                    },
                    //<!-- ========= Disable dates after today ========== -->
                    //maxDate: new Date(),
                    //<!-- ========= Disable dates after today END ========== -->
                },
            function (start, end) {
                debugger
                $('#date').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
                document.getElementById('<%=hdfDate.ClientID%>').value = (start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
            });

                //$('#date').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
                //document.getElementById('<%=hdfDate.ClientID%>').value = (moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
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
            var table5 = document.querySelector('#table1');



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

