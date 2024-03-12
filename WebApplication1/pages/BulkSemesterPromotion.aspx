<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"
    CodeFile="BulkSemesterPromotion.aspx.cs" Inherits="ACADEMIC_BulkSemesterPromotion" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
    <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/FileSaver.js/2014-11-29/FileSaver.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.12.13/xlsx.full.min.js"></script>
    <style>
        .multiselect.dropdown-toggle {
            display: block;
            overflow: hidden;
            text-overflow: ellipsis;
            white-space: nowrap;
        }

        .multiselect-container.dropdown-menu {
            height: 200px;
            overflow: scroll;
        }
    </style>


    <script type="text/javascript" charset="utf-8">
        $(document).ready(function () {
            $(".display").dataTable({
                "bJQueryUI": true,
                "sPaginationType": "full_numbers"
            });

        });
    </script>


    <div>
        <asp:UpdateProgress ID="UpdateProgress5" runat="server" AssociatedUpdatePanelID="upddetails"
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
    <asp:UpdatePanel ID="upddetails" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
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
                                            <asp:Label ID="lblPAAcadSession" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" TabIndex="1" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem>Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvsession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Academic Session" ValidationGroup="teacherallot">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblfaculty" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlColg" runat="server" AppendDataBoundItems="true" TabIndex="2" ValidationGroup="teacherallot"
                                             CssClass="form-control" data-select2-enable="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlColg_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlColg"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Faculty/School Name" ValidationGroup="teacherallot">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                        <label><span style="color: red;">*</span> Degree </label>
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" TabIndex="3" ValidationGroup="teacherallot" CssClass="form-control" data-select2-enable="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Degree" ValidationGroup="teacherallot">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Program </label>
                                        </div>
                                        <asp:ListBox ID="ddlBranch" runat="server" AppendDataBoundItems="true" CssClass="form-control multi-select-demo"
                                            SelectionMode="Multiple" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch1_SelectedIndexChanged"></asp:ListBox>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" ErrorMessage="Please Select Program"  SetFocusOnError="True"
                                            ValidationGroup="teacherallot" meta:resourcekey="RequiredFieldValidator33Resource1"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Curriculum </label>
                                        </div>
                                        <asp:DropDownList ID="ddlSchemeold" runat="server" AppendDataBoundItems="true" TabIndex="5" CssClass="form-control" data-select2-enable="true"
                                            ValidationGroup="teacherallot" Visible="false">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:ListBox ID="ddlScheme" runat="server" AppendDataBoundItems="true" CssClass="form-control multi-select-demo"
                                            SelectionMode="multiple" AutoPostBack="true" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged1"></asp:ListBox>

                                        <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                            Display="None" ErrorMessage="Please Select Curriculum" ValidationGroup="teacherallot">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                        <label><span style="color: red;">*</span> Subject Level</label>

                                        <asp:DropDownList ID="ddlSubjectLevel" runat="server" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="ddlSubjectLevel_SelectedIndexChanged"
                                            ToolTip="Please Select Subject Level" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                            <asp:ListItem Value="0">Regular</asp:ListItem>
                                            <asp:ListItem Value="1">Honors</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSubjectLevel" runat="server" ErrorMessage="Please Select Subject Level"
                                            ControlToValidate="ddlSubjectLevel" Display="None" ValidationGroup="teacherallot" />
                                    </div>
                                    <%--        <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Semester </label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" runat="server"  TabIndex="6" AppendDataBoundItems="true" ValidationGroup="teacherallot" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                       <%-- <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                            Display="None" InitialValue="-1" ErrorMessage="Please Select Semester" ValidationGroup="teacherallot">
                                        </asp:RequiredFieldValidator>--%>
                                    <%--</div>--%>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Semester</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" runat="server" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="True" TabIndex="55"
                                            CssClass="form-control" data-select2-enable="true" meta:resourcekey="ddlSemesterResource1">
                                            <asp:ListItem Value="-1" meta:resourcekey="ListItemResource45">Please Select</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" ControlToValidate="ddlSemester"
                                            Display="None" ErrorMessage="Please Select Semester" InitialValue="-1" SetFocusOnError="True"
                                            ValidationGroup="teacherallot" meta:resourcekey="RequiredFieldValidator33Resource1"></asp:RequiredFieldValidator>
                                    </div>


                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Status </label>
                                        </div>
                                        <asp:DropDownList ID="ddlStatus" runat="server" AppendDataBoundItems="true" TabIndex="6" ValidationGroup="teacherallot" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            <asp:ListItem Value="1">With Rule</asp:ListItem>
                                            <asp:ListItem Value="2">Without Rule</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlStatus"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Status" ValidationGroup="teacherallot">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-12">
                                        <asp:Label runat="server" ID="lblmsg" Text="**Note: ARA's may be allowed to click the PROMOTION option provided that " ForeColor="Red" Font-Bold="true"></asp:Label>
                                        <br />
                                            <asp:Label runat="server" ID="lblmsg1" Text="a) Promotion would only be chosen if only grades and prerequisites are considered." ForeColor="Red" Font-Bold="true"></asp:Label>
                                          <br />
                                            <asp:Label runat="server" ID="lblmsg2" Text="b) If one of the aforementioned conditions is missing the student should not be verified." ForeColor="Red" Font-Bold="true"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server" TabIndex="7" Text="Show" ValidationGroup="teacherallot"
                                    CssClass="btn btn-outline-info" ToolTip="SHOW" OnClick="btnShow_Click" />
                                <asp:LinkButton ID="btnPromotedSem" runat="server" TabIndex="8" ValidationGroup="teacherallot"
                                    CssClass="btn btn-outline-info" ToolTip="PROMOTED STUDENT REPORT" OnClick="btnPromotedSem_Click">
                                        <i class="fa fa-file-pdf-o" aria-hidden="true"></i> Promoted Student</asp:LinkButton>
                                <asp:LinkButton ID="btnNotPromotedSem" runat="server" TabIndex="9" ValidationGroup="teacherallot"
                                    CssClass="btn btn-outline-info" ToolTip="PROMOTED STUDENT REPORT" OnClick="btnNotPromotedSem_Click">
                                         <i class="fa fa-file-pdf-o" aria-hidden="true"></i> Not Promoted Student</asp:LinkButton>
                                <asp:Button ID="btnSave" runat="server" TabIndex="10" Text="Promotion" ValidationGroup="teacherallot"
                                    CssClass="btn btn-outline-info" OnClick="btnSave_Click" ToolTip="SAVE" />
                                <asp:Button ID="btnDemoted" runat="server" TabIndex="11" Text="Demotion" ValidationGroup="teacherallot" Visible="false"
                                    CssClass="btn btn-outline-info" OnClick="btnDemoted_Click" ToolTip="Demotion" />
                                <asp:Button ID="btnClear" runat="server" TabIndex="12" Text="Clear" CssClass="btn btn-outline-danger" OnClick="btnClear_Click" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="teacherallot"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>

                            <div class="col-md-12 table table-responsive">
                                <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" Visible="false">
                                    <div class="col-md-6">
                                        <label id="lbltotal" runat="server" visible="false">
                                            Total Selected</label>
                                        <asp:TextBox ID="txtTotStud" runat="server" Enabled="False" Font-Bold="True" Font-Size="Small" ForeColor="Red" Style="text-align: center; width: 30px;" Text="0" Visible="false"></asp:TextBox>
                                        <%--  Reset the sample so it can be played again --%>
                                        <ajaxToolKit:TextBoxWatermarkExtender ID="text_water" runat="server" Enabled="True" TargetControlID="txtTotStud" WatermarkCssClass="watermarked" WatermarkText="0" />
                                        <asp:HiddenField ID="hftot" runat="server" />
                                    </div>
                                </asp:Panel>
                                <div class="col-md-12">
                                    <asp:Panel ID="Panel2" runat="server" Visible="false">
                                        <asp:ListView ID="lvStudent" runat="server">
                                            <LayoutTemplate>
                                                <div class="sub-heading">
                                                    <h5>Student List</h5>
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
                                                <div class="table-responsive" style="height: 500px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="MainLeadTable">
                                                        <thead class="bg-light-blue" style="position: sticky; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                            <tr>
                                                                <th>
                                                                    <asp:CheckBox ID="cbHeadReg" runat="server" OnClick="checkAllCheckbox(this);" Text="Promotion" ToolTip="Register/Register all" />
                                                                </th>
                                                                <th>

                                                                    <asp:CheckBox ID="chkDemotion" runat="server" OnClick="checkAllCheckboxDemo(this);" Text="Demotion" ToolTip="Demotion/Demotion all" />
                                                                </th>
                                                                <th>Student ID </th>
                                                                <th>Student Name </th>
                                                                <th>Current sem. </th>
                                                                <th>Promoted Sem. </th>
                                                                <th>Status</th>
                                                                <%--<th>A/L Verified</th>--%>
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
                                                        <asp:CheckBox ID="chkRegister" runat="server" CssClass="SelectAllReg" Enabled='<%# (Convert.ToInt32((Eval("PROMOTED_SEM"))) != 0  || (Eval("STATUS").ToString() == "NOT ELIGIBLE")? false :true) %>' Font-Bold="true" ForeColor="Green" onclick="totSubjects(this,1,'cbHeadReg','chkRegister');" Text='<%# (Convert.ToInt32((Eval("PROMOTED_SEM"))) == 0 ?  " " : "PROMOTED" )%>' ToolTip="Click to select this student for semester promotion" />
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkDem" runat="server" CssClass="SelectAllDemo" Visible='<%# (Convert.ToInt32((Eval("PROMOTED_SEM"))) == 0 ? false:true) %>' ToolTip="Click to select this student for semester Depromotion" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblregno" runat="server" Text='<%# Eval("regno")%>' ToolTip='<%# Eval("idno")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblstudname" runat="server" Text='<%# Eval("studname")%>' ToolTip='<%# Eval("sectionno")%>'></asp:Label>
                                                    </td>
                                                    <td><%# Eval("CURENT_SEM") %></td>
                                                    <td><%# Eval("PROMOT_SEM") %></td>
                                                    <td>
                                                        <asp:Label ID="lblStatus" runat="server" Font-Bold="true" ForeColor='<%# (Eval("STATUS").ToString() == "NOT ELIGIBLE" ? System.Drawing.Color.Red : System.Drawing.Color.Green)  %>' Text='<%# Eval("STATUS") %>'></asp:Label>
                                                    </td>
                                                    <%-- <td>
                                                        <asp:Label ID="lblalverified" runat="server" Font-Bold="true" ForeColor='<%# (Eval("ALVERIFIED").ToString() == "PENDING" ? System.Drawing.Color.Red : System.Drawing.Color.Green)  %>' Text='<%# Eval("ALVERIFIED") %>'>></asp:Label>
                                                    </td>--%>
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
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
            <asp:PostBackTrigger ControlID="btnClear" />
            <asp:PostBackTrigger ControlID="btnShow" />
            <asp:PostBackTrigger ControlID="btnPromotedSem" />
        </Triggers>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">

        function totSubjects(chkRegister) {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');
            var hdfTot = document.getElementById('<%= hftot.ClientID %>');
            if (chkRegister.checked == true) {
                txtTot.value = Number(txtTot.value) + 1;
                //hdfTot.value++;
            }
            else
                txtTot.value = Number(txtTot.value) - 1;
            //hdfTot.value--;

        }

        function SelectAll(headchk) {
            var frm = document.forms[0]
            var txtTotStud = document.getElementById('<%= txtTotStud.ClientID %>');
            var hdfTot = document.getElementById('<%= hftot.ClientID %>');
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {

                    if (headchk.checked == true) {

                        //e.checked = true;

                        if (e.disabled == false)
                            e.checked = true;
                        else
                            e.checked = false;



                        txtTotStud.value = hdfTot.value;
                        hdfTot.value++;
                    }
                    else {
                        e.checked = false;
                        txtTotStud.value = 0;
                        hdfTot.value--;
                    }
                }

            }

            if (headchk.checked == true) {
                txtTot.value = hdfTot.value;
                txtCredits.value = hdfCredits.value;
            }
            else {
                txtTot.value = 0;
                txtCredits.value = 0;
            }
        }
    </script>
    <script type="text/javascript" language="javascript">

        function functionConfirm() {
            alert("You can't select semester greater than or equal to student's final semester!!!");
        }
    </script>
    <script type="text/javascript" language="javascript">

        function checkAllCheckbox(headchk) {
            $('.SelectAllReg').each(function(index,value)
            {
                var td = $("td", $(this).closest("tr"));
                if(headchk.checked == true)
                {
                    
                    $("[id*=chkRegister]", td).prop('checked', true);
                }
                else
                {
                    $("[id*=chkRegister]", td).prop('checked', false);
                }
            });
        }

    </script>
    <script type="text/javascript" language="javascript">

        function checkAllCheckboxDemo(headchk) {
            $('.SelectAllDemo').each(function(index,value)
            {
                var td = $("td", $(this).closest("tr"));
                if(headchk.checked == true)
                {
                    
                    $("[id*=chkDem]", td).prop('checked', true);
                }
                else
                {
                    $("[id*=chkDem]", td).prop('checked', false);
                }
            });
        }

    </script>
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
    <div id="divMsg" runat="server">
    </div>
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
            XLSX.writeFile(workbook, "Data.xlsx");
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
