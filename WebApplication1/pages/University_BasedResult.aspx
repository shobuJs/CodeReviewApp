<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="University_BasedResult.aspx.cs" Inherits="ACADEMIC_University_BasedResult" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/FileSaver.js/2014-11-29/FileSaver.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.12.13/xlsx.full.min.js"></script>
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
        #ctl00_ContentPlaceHolder1_LvUniBased_DataPager1 a:first-child,
        #ctl00_ContentPlaceHolder1_LvUniBased_DataPager1 a:last-child {
            padding: 5px 10px;
            border-radius: 0%;
            background: white;
            margin: 0 0px;
            box-shadow: none;
        }


        #ctl00_ContentPlaceHolder1_LvUniBased_DataPager1 a {
            padding: 5px 10px;
            border-radius: 50%;
            background: white;
            margin: 0 0px;
            box-shadow: 0 1px 3px rgb(0 0 0 / 12%), 0 1px 3px rgb(0 0 0 / 24%);
        }

        #ctl00_ContentPlaceHolder1_LvUniBased_DataPager1 span {
            padding: 5px 10px;
            border-radius: 50%;
            background: #4183c4;
            color: #fff;
            margin: 0 0px;
            box-shadow: 0 1px 3px rgb(0 0 0 / 12%), 0 1px 3px rgb(0 0 0 / 24%);
        }
    </style>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UpdUniResult"
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
    <asp:UpdatePanel ID="UpdUniResult" runat="server">
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
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlIntake"
                                            Display="None" ErrorMessage="Please Select Intake" InitialValue="0"
                                            ValidationGroup="Show" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlIntake"
                                            Display="None" ErrorMessage="Please Select Intake" InitialValue="0"
                                            ValidationGroup="Submit" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlIntake"
                                            Display="None" ErrorMessage="Please Select Intake" InitialValue="0"
                                            ValidationGroup="Offer" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYPrgmtype" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlStudyLevel" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>

                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlStudyLevel"
                                            Display="None" ErrorMessage="Please Select Study Level" InitialValue="0"
                                            ValidationGroup="Show" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlStudyLevel"
                                            Display="None" ErrorMessage="Please Select Study Level" InitialValue="0"
                                            ValidationGroup="Submit" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlStudyLevel"
                                            Display="None" ErrorMessage="Please Select Study Level" InitialValue="0"
                                            ValidationGroup="Offer" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYCampusPref" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlPreference" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Preference 1</asp:ListItem>
                                            <asp:ListItem Value="2">Preference 2</asp:ListItem>
                                            <asp:ListItem Value="3">Preference 3</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlPreference"
                                            Display="None" ErrorMessage="Please Select Preference" InitialValue="0"
                                            ValidationGroup="Submit" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlPreference"
                                            Display="None" ErrorMessage="Please Select Preference" InitialValue="0"
                                            ValidationGroup="Offer" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblResult" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlResult" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">Pass</asp:ListItem>
                                            <asp:ListItem Value="2">Deferred</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlResult"
                                            Display="None" ErrorMessage="Please Select Result" InitialValue="0"
                                            ValidationGroup="Submit" />
                                    </div>

                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:LinkButton ID="btnShow" runat="server" ValidationGroup="Show" CssClass="btn btn-outline-info" OnClick="btnShow_Click" TabIndex="1">Show</asp:LinkButton>
                                <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn btn-outline-info" ValidationGroup="Submit" OnClick="btnSubmit_Click" Visible="false" TabIndex="1">Submit</asp:LinkButton>
                                <asp:LinkButton ID="btnOffer" ValidationGroup="Offer" Visible="false" OnClick="btnOffer_Click" runat="server" CssClass="btn btn-outline-info" TabIndex="1">Send Offer Letter</asp:LinkButton>
                                <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancel_Click" TabIndex="1">Cancel</asp:LinkButton>
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Show"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Submit"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                <asp:ValidationSummary ID="ValidationSummary5" runat="server" ValidationGroup="Offer"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>
                            <div class="col-lg-3 col-md-6 offset-lg-9 offset-md-6" runat="server" id="search" visible="false">
                                <div class="input-group sea-rch">
                                    <asp:TextBox ID="FilterData" runat="server" placeholder="Search" AutoPostBack="true" ClientIDMode="Static" OnTextChanged="FilterData_TextChanged" CssClass="form-control"></asp:TextBox>
                                    <%-- <input type="text" id="FilterData" class="form-control" placeholder="Search" />--%>
                                    <div class="input-group-addon">
                                        <i class="fa fa-search"></i>
                                    </div>
                                </div>

                            </div>
                            <asp:ListView ID="LvUniBased" runat="server" OnPagePropertiesChanging="OnPagePropertiesChanging">
                                <LayoutTemplate>
                                    <div class="sub-heading">
                                        <h5>University Based Result List</h5>
                                    </div>
                                    <div class="row mb-1">
                                        <div class="col-lg-2 col-md-6 offset-lg-10">
                                            <button type="button" class="btn btn-outline-primary float-lg-right saveAsExcel">Export Excel</button>
                                        </div>


                                    </div>
                                    <div class="table-responsive" style="max-height: 320px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                        <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="MainLeadTable">
                                            <thead class="bg-light-blue" style="position: sticky; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                <tr>
                                                    <th>
                                                        <asp:CheckBox ID="cbHead" runat="server" OnClick="checkAllCheckbox(this)" /></th>
                                                    <th>Sr No.</th>
                                                    <th>Student Id </th>
                                                    <th>Student Name </th>
                                                    <th>Preference 1 </th>
                                                    <th>Preference 1 Result </th>
                                                    <th>Preference 2 </th>
                                                    <th>Preference 2 Result </th>
                                                    <th>Preference 3 </th>
                                                    <th>Preference 3 Result</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                        </table>
                                    </div>
                                    <div class="col-12 pt-1 pb-2" id="Tfoot1" runat="server">
                                        <div class="float-right">
                                            <asp:DataPager ID="DataPager1" runat="server" PagedControlID="LvUniBased" PageSize="500">
                                                <Fields>
                                                    <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="false" ShowPreviousPageButton="true"
                                                        ShowNextPageButton="false" />
                                                    <asp:NumericPagerField ButtonType="Link" />
                                                    <asp:NextPreviousPagerField ButtonType="Link" ShowNextPageButton="true" ShowLastPageButton="false" ShowPreviousPageButton="false" />
                                                </Fields>
                                            </asp:DataPager>
                                        </div>
                                    </div>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="chktransfer" runat="server" ToolTip='<%# Eval("USERNO")%>' />
                                            <asp:HiddenField ID="hdfintake" runat="server" Value='<%# Eval("BATCHNAME")%>' />
                                            <asp:HiddenField ID="hdfEmailid" runat="server" Value='<%# Eval("EMAILID")%>' />
                                            <asp:HiddenField ID="hdfGetdate" runat="server" Value='<%# Eval("TODAYDATE")%>' />
                                            <asp:HiddenField ID="hdfCollege" runat="server" Value='<%# Eval("COLLEGE_NAME")%>' />
                                            <asp:HiddenField ID="hdfadmfee1" runat="server" Value='<%# Eval("REGULAR_PAYMENT_AMOUNT1")%>' />
                                            <asp:HiddenField ID="hdfadmfee2" runat="server" Value='<%# Eval("REGULAR_PAYMENT_AMOUNT2")%>' />
                                            <asp:HiddenField ID="hdfadmfee3" runat="server" Value='<%# Eval("REGULAR_PAYMENT_AMOUNT3")%>' />
                                            <asp:HiddenField ID="hdfdownfee1" runat="server" Value='<%# Eval("DOWN_PAYMENT1")%>' />
                                            <asp:HiddenField ID="hdfdownfee2" runat="server" Value='<%# Eval("DOWN_PAYMENT2")%>' />
                                            <asp:HiddenField ID="hdfdownfee3" runat="server" Value='<%# Eval("DOWN_PAYMENT3")%>' />
                                            <asp:HiddenField ID="hdfSrno" runat="server" Value='<%# Eval("SRNO")%>' />
                                            <%--                                              <asp:HiddenField ID="hdfScreen" runat="server" Value='<%# Eval("SCREENO")%>' />--%>
                                        </td>
                                        <td><%# Container.DataItemIndex + 1 %></td>
                                        <td>
                                            <asp:Label ID="lblUsername" runat="server" Text='<%# Eval("USERNAME")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblStudname" runat="server" Text='<%# Eval("STUDNAME")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblprogram1" runat="server" Text='<%# Eval("PROGRAM1")%>'></asp:Label>
                                        </td>
                                        <td class="text-center">
                                            <asp:Label ID="lblresult1" runat="server" Text='<%# Eval("RESULT1")%>'></asp:Label>
                                        <td>
                                            <asp:Label ID="lblprogram2" runat="server" Text='<%# Eval("PROGRAM2")%>'></asp:Label>
                                        </td>
                                        <td class="text-center">
                                            <asp:Label ID="lblresult2" runat="server" Text='<%# Eval("RESULT2")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblProgram3" runat="server" Text='<%# Eval("PROGRAM3")%>'></asp:Label>
                                        </td>
                                        <td class="text-center">
                                            <asp:Label ID="lblresult3" runat="server" Text='<%# Eval("RESULT3")%>'></asp:Label>
                                        </td>


                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">
        function checkAllCheckbox(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                var s = e.name.split("ctl00$ContentPlaceHolder1$LvUniBased$ctrl");
                var b = 'ctl00$ContentPlaceHolder1$LvUniBased$ctrl';
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

