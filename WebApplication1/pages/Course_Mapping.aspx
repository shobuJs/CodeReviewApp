<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Course_Mapping.aspx.cs" Inherits="ACADEMIC_Course_Mapping" ViewStateEncryptionMode="Always" EnableViewStateMac="true" ValidateRequest="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- jQuery library -->
    <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
    <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/FileSaver.js/2014-11-29/FileSaver.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.12.13/xlsx.full.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true, maxHeight: 200, enableFiltering: true, filterPlaceholder: 'Search',
                enableCaseInsensitiveFiltering: true,
            });

        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $('.multi-select-demo').multiselect({
                    includeSelectAllOption: true, maxHeight: 200, enableFiltering: true, filterPlaceholder: 'Search',
                    enableCaseInsensitiveFiltering: true,
                });
            });
        });    </script>
    <style>
        td .fa-eye {
            font-size: 18px;
            color: #0d70fd;
            margin-left: 10px;
        }

        .dataTables_scrollHeadInner {
            width: max-content !important;
        }

        #ctl00_ContentPlaceHolder1_lvCourse_DataPager1 a:first-child,
        #ctl00_ContentPlaceHolder1_lvCourse_DataPager1 a:last-child {
            padding: 5px 10px;
            border-radius: 0%;
            background: white;
            margin: 0 0px;
            box-shadow: none;
        }

        #ctl00_ContentPlaceHolder1_lvCourse_DataPager1 a {
            padding: 5px 10px;
            border-radius: 50%;
            background: white;
            margin: 0 0px;
            box-shadow: 0 1px 3px rgb(0 0 0 / 12%), 0 1px 3px rgb(0 0 0 / 24%);
        }

        #ctl00_ContentPlaceHolder1_lvCourse_DataPager1 span {
            padding: 5px 10px;
            border-radius: 50%;
            background: #4183c4;
            color: #fff;
            margin: 0 0px;
            box-shadow: 0 1px 3px rgb(0 0 0 / 12%), 0 1px 3px rgb(0 0 0 / 24%);
        }
    </style>
    <script>
        function getVal() {
            // debugger;
            var id = (document.getElementById("<%=btnSave.ClientID%>"));
            ShowLoader(id);
            var array = []
            var deptNo;
            var checkboxes = document.querySelectorAll('input[type=checkbox]:checked')

            for (var i = 0; i < checkboxes.length; i++) {
                //array.push(checkboxes[i].value)    
                if (deptNo == undefined) {
                    deptNo = checkboxes[i].value + ',';
                }
                else {
                    deptNo += checkboxes[i].value + ',';
                }
            }
            //alert(degreeNo);
            $('#<%= hdndeptno.ClientID %>').val(deptNo);
            //document.getElementById(inpHide).value = "degreeNo";


            if (alertmsg != '') {
                alert(alertmsg);
                HideLoader(id, 'Submit');
                return false;
            }

            return true;

        }
    </script>

    <style>
        div.dd_chk_select {
            height: 35px;
            font-size: 14px !important;
            padding-left: 12px !important;
            line-height: 2.2 !important;
            width: 100%;
        }

            div.dd_chk_select div#caption {
                height: 35px;
            }
    </style>

    <script type="text/javascript">
        document.onreadystatechange = function () {
            var state = document.readyState
            if (state == 'interactive') {
                document.getElementById('contents').style.visibility = "hidden";
            } else if (state == 'complete') {
                setTimeout(function () {
                    document.getElementById('interactive');
                    document.getElementById('load').style.visibility = "hidden";
                    document.getElementById('contents').style.visibility = "visible";
                }, 1000);
            }
        }
    </script>


    <div>

        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updRule"
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

    <div id="contents">
    </div>
    <asp:UpdatePanel ID="updRule" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">

                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div id="divMsg" runat="server"></div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYCollege" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollege" runat="server" TabIndex="1" data-select2-enable="true"
                                            AppendDataBoundItems="True"
                                            ToolTip="Please Select Name" AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" ClientIDMode="Static">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select College" ValidationGroup="Submit"
                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select College" ValidationGroup="Report"
                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                    <div id="Div1" class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYIDept" runat="server" Font-Bold="true">Department</asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlDepartment" runat="server" data-select2-enable="true" AppendDataBoundItems="true" AutoPostBack="true"
                                            ToolTip="Please select Department" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" ClientIDMode="Static">
                                            <asp:ListItem Value="0">Please select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlDepartment"
                                            Display="None" ErrorMessage="Please Select Department" ValidationGroup="Submit"
                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>

                                    </div>
                                    <div id="Div2" class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYProgram" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" TabIndex="3" data-select2-enable="true"
                                            AppendDataBoundItems="True" ClientIDMode="Static"
                                            ToolTip="Please Select Degree">
                                            <asp:ListItem Value="0">Please select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Program" ValidationGroup="Submit"
                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYSchemes" runat="server" Font-Bold="true">Curriculum</asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlScheme" runat="server" TabIndex="4" data-select2-enable="true"
                                            AppendDataBoundItems="True" ClientIDMode="Static" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged" AutoPostBack="true"
                                            ToolTip="Please Select Regulation">
                                            <asp:ListItem Value="0">Please select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlScheme"
                                            Display="None" ErrorMessage="Please Select Curriculum" ValidationGroup="Submit"
                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlScheme"
                                            Display="None" ErrorMessage="Please Select Curriculum" ValidationGroup="Report"
                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-6 col-md-6 col-12">

                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDyModule" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <%-- <asp:DropDownList ID="ddlDeptMultiCheck" runat="server" multiple="multiple" >
                                </asp:DropDownList>--%>
                                        <asp:ListBox ID="ddlSubjectAL" runat="server" AppendDataBoundItems="true" CssClass="form-control multi-select-demo"
                                            SelectionMode="multiple" TabIndex="8"></asp:ListBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSubjectAL"
                                            Display="None" ErrorMessage="Please Select Subject" ValidationGroup="Submit"
                                            SetFocusOnError="True" InitialValue=""></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYSubjectType" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlCoreElective" runat="server" data-select2-enable="true" AppendDataBoundItems="true" ClientIDMode="Static">
                                            <asp:ListItem Value="0">Please select</asp:ListItem>
                                            <asp:ListItem Value="1">Core</asp:ListItem>
                                            <asp:ListItem Value="2">Elective</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCoreElective"
                                            Display="None" ErrorMessage="Please Select  Subject Category" ValidationGroup="Submit"
                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYSemester" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemister" runat="server" data-select2-enable="true" AppendDataBoundItems="true" ClientIDMode="Static">
                                            <asp:ListItem Value="0">Please select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlSemister"
                                            Display="None" ErrorMessage="Please Select Semester" ValidationGroup="Submit"
                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblSubjectClassi" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:DropDownList ID="ddlSubjectClassification" runat="server" data-select2-enable="true" AppendDataBoundItems="true" ClientIDMode="Static">
                                            <asp:ListItem Value="0">Please select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlSubjectClassification"
                                            Display="None" ErrorMessage="Please Select Subject Classification" ValidationGroup="Submit"
                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>

                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <asp:Label ID="lblDYSemesterHours" runat="server" Font-Bold="true"></asp:Label>
                                        </div>
                                        <asp:TextBox ID="txtSemHours" runat="server" ValidationGroup="Submit" ToolTip="Enter Subject Total Semester Hours" CssClass="form-control" placeholder="Enter Subject Total Semester Hours"></asp:TextBox>
                                        <span class="input-group-addon">
                                            <%--<asp:RegularExpressionValidator ID="constr" runat="server" ValidationExpression="^\d*(\.\d+)?$"
                                                ErrorMessage="Please enter valid decimal number with any decimal places"
                                                ControlToValidate="txtSemHours" ForeColor="Red"></asp:RegularExpressionValidator>--%>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="fte" runat="server" TargetControlID="txtSemHours"
                                                FilterMode="ValidChars" FilterType="Custom" ValidChars="012345689.">
                                            </ajaxToolKit:FilteredTextBoxExtender>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblStatus" runat="server" Font-Bold="true"></asp:Label>

                                        </div>
                                        <div class="switch form-inline">
                                            <input type="checkbox" id="switch" name="switch" class="switch" checked tabindex="8" />
                                            <label data-on="Active" data-off="Inactive" for="switch"></label>
                                        </div>
                                        <asp:HiddenField ID="hfdStat" runat="server" ClientIDMode="Static" />
                                    </div>
                                </div>
                            </div>
                            <asp:UpdatePanel ID="updGradeEntry" runat="server">
                                <ContentTemplate>
                                    <div class="col-12 btn-footer">


                                        <asp:LinkButton ID="btnSave" runat="server" ToolTip="Submit" ValidationGroup="Submit" ClientIDMode="Static"
                                            CssClass="btn btn-outline-info btnX" OnClick="btnSave_Click">Submit</asp:LinkButton>

                                        <asp:LinkButton ID="btnReport" runat="server" ToolTip="Submit" ValidationGroup="Report" OnClick="btnReport_Click"
                                            CssClass="btn btn-outline-info btnX">Report</asp:LinkButton>

                                        <asp:LinkButton ID="btnPrReport" runat="server" ToolTip="Submit" ValidationGroup="Report" OnClick="btnPrReport_Click"
                                            CssClass="btn btn-outline-info btnX">Prospectus Report</asp:LinkButton>

                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                            CssClass="btn btn-outline-danger" OnClick="btnCancel_Click" />
                                        <asp:HiddenField ID="hdndeptno" runat="server" />
                                        <asp:ValidationSummary ID="ValidationSummary4" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Submit" />
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Report" />
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnSave" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlCollege" />
                                    <%--  <asp:AsyncPostBackTrigger ControlID="ddlColg" EventName="SelectedIndexChanged"/>--%>
                                </Triggers>
                            </asp:UpdatePanel>

                            <div class="col-12">

                                <asp:Panel ID="pnlSession" runat="server">
                                    <asp:ListView ID="lvCourse" runat="server" OnPagePropertiesChanging="OnPagePropertiesChanging">
                                        <LayoutTemplate>
                                            <div id="demo-grid">
                                                <div class="sub-heading">
                                                    <h5>
                                                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label>
                                                        List</h5>
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
                                                        <thead class="bg-light-blue" style="position: sticky; z-index: 1; background: #fff!important; top: 0; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                            <tr>
                                                                <th>
                                                                    <asp:Label ID="lblAction" runat="server" Font-Bold="true"></asp:Label></th>
                                                                <%--  <th>
                                                                <asp:Label ID="lblDYIDept" runat="server">Department</asp:Label></th>--%>
                                                                <th>
                                                                    <asp:Label ID="lblDYSemester" runat="server" Font-Bold="true"></asp:Label></th>

                                                                <th style="text-align: center;">Subject Code</th>
                                                                <th>
                                                                    <asp:Label ID="lblDyModule" runat="server" Font-Bold="true"></asp:Label></th>
                                                                <th>
                                                                    <asp:Label ID="lblDYCredits" runat="server" Font-Bold="true"></asp:Label></th>

                                                                <th>
                                                                    <asp:Label ID="lblDYModuleType" runat="server" Font-Bold="true"></asp:Label></th>
                                                                <th>
                                                                    <asp:Label ID="lblDYSubjectType" runat="server" Font-Bold="true"></asp:Label></th>

                                                                <th>
                                                                    <asp:Label ID="lblSubjectClassi" runat="server" Font-Bold="true"></asp:Label></th>

                                                                <th style="text-align: center;">Pre-requisites</th>
                                                                <th style="text-align: center;">Co-requisites</th>

                                                                <th>
                                                                    <asp:Label ID="lblDYScheme" runat="server" Font-Bold="true"></asp:Label></th>
                                                                <th>
                                                                    <asp:Label ID="lblDYSemesterHours" runat="server" Font-Bold="true"></asp:Label></th>
                                                                <th>
                                                                    <asp:Label ID="lblStatus" runat="server" Font-Bold="true"></asp:Label></th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                            <div class="col-12 pt-1 pb-2" id="Tfoot1" runat="server">
                                                <div class="float-right">
                                                    <asp:DataPager ID="DataPager1" runat="server" PagedControlID="lvCourse" PageSize="1000">
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
                                                <%--<td style="text-align: center">
                                                    <%# Container.DataItemIndex + 1 %>
                                                    <asp:HiddenField ID="hdfvalue" runat="server" />
                                                </td>--%>
                                                <td>
                                                    <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/images/edit.gif"
                                                        CommandArgument='<%# Eval("COURSEN_MAPPING_NO") %>' CommandName='<%# Eval("COURSENO") %>' AlternateText="Edit Record"
                                                        OnClick="btnEdit_Click" /></td>
                                                <%--  <td><%# Eval("DEPTNAME")%></td>--%>
                                                <td><%# Eval("SEMFULLNAME")%></td>
                                                <td><%# Eval("CCODE")%> </td>
                                                <td><%# Eval("COURSE_NAME")%></td>
                                                <td><%# Eval("CREDITS")%></td>
                                                <td><%# Eval("SUBNAME")%></td>
                                                <td><%# Eval("ELECTIVENAME")%></td>
                                                <td><%# Eval("SUBCLASSIFIC_NAME")%></td>

                                                <td><%# Eval("PRE_REQUISITES")%></td>

                                                <td><%# Eval("CO_REQUISITES")%></td>

                                                <td><%# Eval("SCHEMENAME")%></td>
                                                <td><%# Eval("TOTSEMESTERHOURS")%></td>

                                                <td>
                                                    <asp:Label runat="server" ID="lblActive" Text='<%# Eval("STATUS")%>' Font-Bold="true"></asp:Label></td>

                                            </tr>
                                        </ItemTemplate>
                                        <EmptyDataTemplate></EmptyDataTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            </span>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReport" />
        </Triggers>
    </asp:UpdatePanel>
    <%--Module Mapping--%>
    <script type="text/javascript">
        function UserDeleteConfirmation() {
            if (confirm("Are you sure you want to delete this Entry?"))
                return true;
            else
                return false;
        }
    </script>

    <script>
        function SetActive(val) {
            debugger;
            $('#switch').prop('checked', val);
        }
        var summary = "";
        $(function () {
            debugger;
            $('#btnSave').click(function () {
                localStorage.setItem("currentId", "#btnSave,Submit");

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
            debugger;
            $(function () {
                $('#btnSave').click(function () {
                    localStorage.setItem("currentId", "#btnSave,Submit");
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
            //console.log(allDataArray.shift())
            allDataArray.forEach(row => {
                row.shift(); // Remove the 0th index element
        });


        var worksheet = XLSX.utils.aoa_to_sheet(allDataArray);
        workbook.SheetNames.push("Test");
        workbook.Sheets["Test"] = worksheet;
        XLSX.writeFile(workbook, "SubjectCurriculmList.xlsx");
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

