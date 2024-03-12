<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Screening_Configuration.aspx.cs" Inherits="ACADEMIC_Screening_Configuration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/FileSaver.js/2014-11-29/FileSaver.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.12.13/xlsx.full.min.js"></script>
    <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
    <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>


    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updScreening"
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
    <asp:UpdatePanel ID="updScreening" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label>
                            </h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYSession" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlIntake" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlIntake"
                                            Display="None" ErrorMessage="Please Select Intake" InitialValue="0"
                                            ValidationGroup="Show" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlIntake"
                                            Display="None" ErrorMessage="Please Select Intake" InitialValue="0"
                                            ValidationGroup="Submit" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYCollege" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select College" InitialValue="0"
                                            ValidationGroup="Show" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select College" InitialValue="0"
                                            ValidationGroup="Submit" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" id="lstscreen" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblScreen" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:ListBox ID="lstScreeningRequired" runat="server" SelectionMode="Multiple" TabIndex="1" CssClass="form-control multi-select-demo" AppendDataBoundItems="true">
                                            <asp:ListItem Value="1">University</asp:ListItem>
                                            <asp:ListItem Value="2">Program</asp:ListItem>
                                        </asp:ListBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="lstScreeningRequired"
                                            Display="None" ErrorMessage="Please Select  Screening Required"
                                            ValidationGroup="Submit" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:LinkButton ID="btnShow" runat="server" CssClass="btn btn-outline-info" TabIndex="1" ValidationGroup="Show" OnClick="btnShow_Click">Show</asp:LinkButton>
                                <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn btn-outline-info" TabIndex="1" ValidationGroup="Submit" OnClick="btnSubmit_Click" Visible="false">Submit</asp:LinkButton>
                                <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-outline-danger" TabIndex="1" OnClick="btnCancel_Click">Cancel</asp:LinkButton>
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Show"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Submit"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>

                            <asp:Panel ID="Panel1" runat="server" Visible="false">
                                <asp:ListView ID="LvScreenList" runat="server">
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                            <h5>Screening Configuration List</h5>
                                        </div>
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
                                        <div class="table-responsive" style="max-height: 320px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                            <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="MainLeadTable">
                                                <thead class="bg-light-blue" style="position: sticky; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                    <tr>
                                                        <th>
                                                            <asp:CheckBox ID="cbHead" runat="server" OnClick="checkAllCheckbox(this)" /></th>
                                                      
                                                        <th>Program </th>
                                                          <th>Intake </th>
                                                        <th>Screening Required </th>
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
                                                <asp:CheckBox ID="chktransfer" runat="server" ToolTip='<%# Eval("ID")%>' />
                                                <asp:HiddenField ID="hdfDegreeno" runat="server" Value='<%# Eval("DEGREENO")%>' />
                                                <asp:HiddenField ID="hdfBranchno" runat="server" Value='<%# Eval("BRANCHNO")%>' />
                                            </td>
                  
                                            <td>
                                                <asp:Label ID="lblProgram" runat="server" Text='<%# Eval("PROGRAM")%>'></asp:Label>
                                            </td>
                                                                      <td> <%# Eval("BATCHNAME") %></td>
                                            <td>
                                                <asp:Label ID="lblScreening" runat="server" Text='<%# Eval("SCREEN")%>'></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


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
    <script type="text/javascript" language="javascript">
        function checkAllCheckbox(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                var s = e.name.split("ctl00$ContentPlaceHolder1$LvScreenList$ctrl");
                var b = 'ctl00$ContentPlaceHolder1$LvScreenList$ctrl';
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

