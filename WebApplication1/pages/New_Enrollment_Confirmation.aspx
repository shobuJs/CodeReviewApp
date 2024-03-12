<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="New_Enrollment_Confirmation.aspx.cs" Inherits="Enrollment_Configuration" %>

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
    <style>
        td .fa-eye {
            font-size: 18px;
            color: #0d70fd;
            margin-left: 10px;
        }

        .dataTables_scrollHeadInner {
            width: max-content !important;
        }

        #ctl00_ContentPlaceHolder1_lvEnrollmentConfirmation_DataPager1 a:first-child,
        #ctl00_ContentPlaceHolder1_lvEnrollmentConfirmation_DataPager1 a:last-child {
            padding: 5px 10px;
            border-radius: 0%;
            background: white;
            margin: 0 0px;
            box-shadow: none;
        }

        #ctl00_ContentPlaceHolder1_lvEnrollmentConfirmation_DataPager1 a {
            padding: 5px 10px;
            border-radius: 50%;
            background: white;
            margin: 0 0px;
            box-shadow: 0 1px 3px rgb(0 0 0 / 12%), 0 1px 3px rgb(0 0 0 / 24%);
        }

        #ctl00_ContentPlaceHolder1_lvEnrollmentConfirmation_DataPager1 span {
            padding: 5px 10px;
            border-radius: 50%;
            background: #4183c4;
            color: #fff;
            margin: 0 0px;
            box-shadow: 0 1px 3px rgb(0 0 0 / 12%), 0 1px 3px rgb(0 0 0 / 24%);
        }
    </style>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <%--   <h3 class="box-title">Enrollment Confirmation</h3>  --%>
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
                                <%--<asp:DropDownList ID="ddlIntake" runat="server" CssClass="form-control" data-select2-enable="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>--%>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Study Level</label>
                                </div>

                                <%--                                <asp:DropDownList ID="ddlProgramType" runat="server" AppendDataBoundItems="true" TabIndex="1"
                                                                                        Width="100%" CssClass="form-control select2 select-clik" AutoPostBack="true">
                                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                                    </asp:DropDownList>--%>

                                <asp:DropDownList ID="ddlStudyType" runat="server" AppendDataBoundItems="true" CssClass="form-control select2 select-clik" AutoPostBack="true" OnSelectedIndexChanged="ddlStudyType_SelectedIndexChanged" TabIndex="2">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>

                                <%--<asp:ListBox ID="level" runat="server" CssClass="form-control multi-select-demo" SelectionMode="Multiple"></asp:ListBox>--%>
                            </div>

                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Program</label>
                                </div>

                                <asp:ListBox ID="ddlProgramIntrested" runat="server" CssClass="form-control multi-select-demo"
                                    SelectionMode="Multiple" AppendDataBoundItems="true" TabIndex="3"></asp:ListBox>

                                <%-- <asp:ListBox ID="ListBox1" runat="server" CssClass="form-control multi-select-demo" SelectionMode="Multiple"></asp:ListBox>--%>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
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
                                <asp:LinkButton ID="lnkButtonShow" runat="server" CssClass="btn btn-outline-info" OnClick="lnkButtonShow_Click">Show</asp:LinkButton>
                                <asp:LinkButton ID="lnkButtonCancel" runat="server" CssClass="btn btn-outline-danger" OnClick="lnkButtonCancel_Click">Cancel</asp:LinkButton>
                            </div>

                            <div class="form-group col-lg-4 col-md-12 col-12" style="display: none">
                                <div class=" note-div">
                                    <h5 class="heading">Note </h5>
                                    <p><i class="fa fa-star" aria-hidden="true"></i><i class="fa fa-square" style="color: #007bff;" aria-hidden="true"></i><span>Eligible as per entry criteria </span></p>
                                    <p><i class="fa fa-star" aria-hidden="true"></i><i class="fa fa-square" style="color: #dc3545;" aria-hidden="true"></i><span>Not Eligible as per criteria </span></p>
                                    <p><i class="fa fa-star" aria-hidden="true"></i><i class="fa fa-square" style="color: #28a745;" aria-hidden="true"></i><span>Enrollment Confirmation</span> </p>
                                </div>
                            </div>

                            <div class="col-12">
                                <asp:Panel ID="Panel1" runat="server">
                                    <asp:ListView ID="lvEnrollmentConfirmation" runat="server" Visible="true" OnPagePropertiesChanging="OnPagePropertiesChanging">
                                        <LayoutTemplate>
                                            <div>
                                                <%--<h4>Current Semester Subjects</h4>--%>
                                                <div class="row mb-1">
                                                    <div class="col-lg-2 col-md-6 offset-lg-7">
                                                        <button type="button" class="btn btn-outline-primary float-lg-right saveAsExcel">Export Excel</button>
                                                    </div>

                                                    <div class="col-lg-3 col-md-6">
                                                        <div class="input-group sea-rch">
                                                            <asp:TextBox ID="txtFilter" runat="server" TabIndex="1" CssClass="form-control" MaxLength="20" placeholder="Search" OnTextChanged="txtFilter_TextChanged" AutoPostBack="true" />
                                                            <%--<input type="text" id="FilterData" class="form-control" placeholder="Search" />--%>
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
                                                                <th>Admission Status</th>
                                                                <th>Student ID No.</th>
                                                                <th>Progress Level</th>
                                                                <th>Payment Status</th>
                                                                <th>Student Type</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </div>
                                                <div class="col-12 pt-1 pb-2" id="Tfoot1" runat="server">
                                                    <div class="float-right">
                                                        <asp:DataPager ID="DataPager1" runat="server" PagedControlID="lvEnrollmentConfirmation" PageSize="1000">
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
                                            <tr id="trCurRow" class="item">

                                                <td>
                                                    <a target="_blank" href='<%# Eval("USER_URL")%>'>

                                                        <asp:Label ID="lnkApplicationNo" CssClass="PopUp" runat="server" Text='<%# Eval("APPLICATION_ID")%>' ToolTip='<%# Eval("APPLICATION_ID")%>'></asp:Label>
                                                    </a>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("APPLICANT_NAME") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("EMAIL_ID") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("MOBILE_NO") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("PROGRAM") %>' />
                                                </td>
                                                <%--<td>
                                                    <asp:Label ID="lblSub_Type" runat="server" Text='<%# Eval("APTITUDE_SCORE") %>' />
                                                </td>--%>
                                                <td>
                                                    <asp:Label ID="lblEligible" runat="server" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblEnrollmentNo" runat="server" Text='<%# Eval("STUDENT_ID") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("PROGRESS_LEVEL") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("PAY_STATUS") %>' />
                                                </td>
                                                <td>
                                                    <%# Eval("STUDENT_TYPE") %>
                                                </td>
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

                            <%-- <div class="col-12 mt-3">

                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                    <thead class="bg-light-blue">
                                        <tr>
                                            <th>Application ID</th>
                                            <th>Applicant Name</th>
                                            <th>Program</th>
                                            <th>Aptitude Score</th>
                                            <th>Eligibility Status</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td><a href="#" data-toggle="modal" data-target="#myModal_id">SL0004</a> </td>
                                            <td>Andrew Mendis</td>
                                            <td>Program 1</td>
                                            <td>80</td>
                                            <td class="text-center"><span class="badge badge-primary">Selected</span></td>
                                        </tr>
                                        <tr>
                                            <td><a href="#" data-toggle="modal" data-target="#myModal_id">SL0004</a> </td>
                                            <td>Rahul Patel</td>
                                            <td>Program 1</td>
                                            <td>80</td>
                                            <td class="text-center"><span class="badge badge-danger">Rejected</span></td>
                                        </tr>
                                        <tr>
                                            <td><a href="#" data-toggle="modal" data-target="#myModal_id">SL0004</a> </td>
                                            <td>Rahul Patel</td>
                                            <td>Program 1</td>
                                            <td>80</td>
                                            <td class="text-center"><span class="badge badge-success">Enrolled</span></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>--%>
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
        $(".saveAsExcel").click(function () {
            var table5 = document.querySelector('#MainLeadTable');
            var workbook = XLSX.utils.book_new();
            var allDataArray = makeTableArray(table5);
            allDataArray.forEach(row => {
                row.shift(); // Remove the 0th index element
        });

        var worksheet = XLSX.utils.aoa_to_sheet(allDataArray);
        workbook.SheetNames.push("Test");
        workbook.Sheets["Test"] = worksheet;
        XLSX.writeFile(workbook, "SubjectCurriculumList.xlsx");
        });

        function makeTableArray(table) {
            var allTableRows = table.querySelectorAll('tr');
            var array = [];
            allTableRows.forEach(row => {
                var rowArray = [];
            if (row.querySelector('td')) {
                var allTds = row.querySelectorAll('td');
                allTds.forEach(td => {
                    var cellContent = '';
                if (td.querySelector('span')) {
                    cellContent = td.querySelector('span').textContent;
                } else if (td.querySelector('input')) {
                    cellContent = td.querySelector('input').value;
                } else if (td.querySelector('select')) {
                    cellContent = td.querySelector('select').value;
                } else if (td.innerText) {
                    cellContent = td.innerText;
                }
                rowArray.push(cellContent);
            });
        }
        if (row.querySelector('th')) {
            var allThs = row.querySelectorAll('th');
            allThs.forEach(th => {
                var cellContent = th.textContent ? th.textContent : '';
            rowArray.push(cellContent);
        });
        }
        array.push(rowArray);
        });
        return array;
        }
    </script>
</asp:Content>