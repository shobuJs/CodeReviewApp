<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Branch_Change.aspx.cs" Inherits="Projects_Branch_Change" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
    <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>
    <style>
        .logoContainer1 img {
            width: 45px;
            height: 45px;
        }
    </style>
    <style>
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <style>
        .dynamic-nav-tabs li.active a {
            color: #255282;
            background-color: #fff;
            border-top: 3px solid #255282 !important;
            border-color: #255282 #255282 #fff !important;
        }

        .chiller-theme .nav-tabs.dynamic-nav-tabs > li.active {
            border-radius: 8px;
            background-color: transparent !important;
        }
    </style>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="nav-tabs-custom mt-2 col-12" id="myTabContent">
                    <ul class="nav nav-tabs dynamic-nav-tabs" role="tablist">
                        <li class="nav-item active" id="divlksingle" runat="server">
                            <asp:LinkButton ID="lksingle" runat="server" OnClick="lksingle_Click" CssClass="nav-link" TabIndex="1">Program Change Apply</asp:LinkButton></li>
                        <li class="nav-item" id="divlkbulk" runat="server">
                            <asp:LinkButton ID="lkbulk" runat="server" OnClick="lkbulk_Click" CssClass="nav-link" TabIndex="2">Bulk Program Change Apply</asp:LinkButton></li>
                    </ul>
                    <div class="tab-content">
                        <div class="tab-pane fade show active" id="divsingle" role="tabpanel" runat="server" aria-labelledby="ALCourses-tab">
                            <div>
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updbranchchange"
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
                            <asp:UpdatePanel ID="updbranchchange" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="box-body">
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <asp:Label ID="lblACAcademicSession" runat="server" Font-Bold="true"></asp:Label>
                                                        <%--<label>Academic Session</label>--%>
                                                    </div>
                                                    <asp:DropDownList ID="ddlAcademicSession" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="1" AppendDataBoundItems="true" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlAcademicSession_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <asp:Label ID="lblFaculty" runat="server" Font-Bold="true"></asp:Label>
                                                        <%--<label>Faculty</label>--%>
                                                    </div>
                                                    <asp:DropDownList ID="ddlFaculty" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="2" ClientIDMode="Static" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <asp:Label ID="lblDYPrgmtype" runat="server" Font-Bold="true"></asp:Label>
                                                        <%--<label>Study Level</label>--%>
                                                    </div>
                                                    <asp:DropDownList ID="ddlStudyLevel" runat="server" CssClass="form-control" TabIndex="3" data-select2-enable="true" ClientIDMode="Static" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlStudyLevel_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <asp:Label ID="lblDyProgram" runat="server" Font-Bold="true"></asp:Label>
                                                        <%--<label>Program</label>--%>
                                                    </div>
                                                    <asp:ListBox ID="lstbxProgram" runat="server" CssClass="form-control multi-select-demo" TabIndex="4" ClientIDMode="Static" SelectionMode="Multiple" AutoPostBack="true" OnSelectedIndexChanged="lstbxProgram_SelectedIndexChanged"></asp:ListBox>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <asp:Label ID="lblDYSemester" runat="server" Font-Bold="true"></asp:Label>
                                                        <%--<label>Semester</label>--%>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSemester" runat="server" CssClass="form-control" TabIndex="5" data-select2-enable="true" ClientIDMode="Static" AppendDataBoundItems="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>

                                                <div class="form-group col-lg-6 col-md-6 col-12" runat="server" visible="false" id="DivApply">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <%--  <asp:Label ID="Label4" runat="server" Font-Bold="true"></asp:Label>--%>
                                                        <label>Apply for Program</label>
                                                    </div>
                                                    <asp:ListBox ID="lstbxApplyProgram" runat="server" CssClass="form-control multi-select-demo" TabIndex="6" SelectionMode="Multiple"></asp:ListBox>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="col-12 btn-footer">
                                            <asp:LinkButton ID="btnShow" runat="server" CssClass="btn btn-outline-primary" OnClick="btnShow_Click" ClientIDMode="Static" TabIndex="7">Show</asp:LinkButton>
                                            <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn btn-outline-primary" TabIndex="8" OnClick="btnSubmit_Click">Submit</asp:LinkButton>
                                            <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-outline-danger" TabIndex="9" OnClick="btnCancel_Click">Cancel</asp:LinkButton>
                                        </div>

                                        <div class="col-12">
                                            <asp:Panel ID="Panel2" runat="server">
                                                <asp:ListView ID="lvchangebranch" runat="server" Visible="true">
                                                    <LayoutTemplate>
                                                        <div class="sub-heading">
                                                        </div>
                                                        <div class="table-responsive" style="height: 320px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                            <table class="table table-striped table-bordered nowrap mb-0" style="width: 100%;" id="tableOnline">
                                                                <%--  <table class="table table-striped table-bordered nowrap display" style="width: 100%">--%>
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>
                                                                            <asp:CheckBox ID="chkAll" runat="server" OnClick="checkAllCheckboxBulk(this)" /></th>
                                                                        <th>Student ID</th>
                                                                        <th>Student Name</th>
                                                                        <th>Existing Program</th>
                                                                        <th>Apply for Program</th>

                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                        </div>
                                                        </table>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>

                                                                <asp:CheckBox ID="chkapp" runat="server" />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblregno" runat="server" Text='<%# Eval("REGNO") %>'></asp:Label>
                                                                <asp:Label ID="lblEnroll" runat="server" Text='<%# Eval("ENROLLNO") %>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblIdno" runat="server" Text='<%# Eval("IDNO") %>' Visible="false"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblStudentname" runat="server" Text='<%# Eval("NAME_WITH_INITIAL") %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblExistingProgram" runat="server" Text='<%# Eval("PROGRAM") %>'></asp:Label>
                                                                <asp:Label ID="lbldegreeNo" runat="server" Text='<%# Eval("DEGREENO") %>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblBranchNo" runat="server" Text='<%# Eval("BRANCHNO") %>' Visible="false"></asp:Label>

                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblapllyforprogram" runat="server" Text='<%#Eval("APLLYPROGRAM") %>'></asp:Label>
                                                                <asp:Label ID="lblAfiliated" runat="server" Text='<%# Eval("AFFILIATED_NO") %>' Visible="false"></asp:Label>
                                                            </td>


                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>

                                            </asp:Panel>

                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div id="divbulk" runat="server" visible="false" role="tabpanel" aria-labelledby="Grade-tab">
                            <div>
                                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updBulkbranchchange"
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
                            <asp:UpdatePanel ID="updBulkbranchchange" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="box-body">
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <asp:Label ID="lblRASession" runat="server" Font-Bold="true"></asp:Label>
                                                        <%--<label>Academic Session</label>--%>
                                                    </div>
                                                    <asp:DropDownList ID="ddlbulksession" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="1" AppendDataBoundItems="true" ClientIDMode="Static" OnSelectedIndexChanged="ddlbulksession_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                  <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlbulksession"
                                                        Display="None" ErrorMessage="Please select Academic Session." InitialValue="0" ValidationGroup="EnrollSubmit"
                                                        SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <asp:Label ID="lblDYCollegeNew" runat="server" Font-Bold="true"></asp:Label>
                                                        <%--<label>Faculty</label>--%>
                                                    </div>
                                                    <asp:DropDownList ID="ddlbulkfaculty" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="2" ClientIDMode="Static" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlbulkfaculty_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlbulkfaculty"
                                                        Display="None" ErrorMessage="Please select Faculty." InitialValue="0" ValidationGroup="EnrollSubmit"
                                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <asp:Label ID="lblDyAdmissionType" runat="server" Font-Bold="true"></asp:Label>
                                                        <%--<label>Study Level</label>--%>
                                                    </div>
                                                    <asp:DropDownList ID="ddlbulkstudy" runat="server" CssClass="form-control" TabIndex="3" data-select2-enable="true" ClientIDMode="Static" AppendDataBoundItems="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlbulkstudy"
                                                        Display="None" ErrorMessage="Please select Study Level." InitialValue="0" ValidationGroup="EnrollSubmit"
                                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <asp:Label ID="lblSemester" runat="server" Font-Bold="true"></asp:Label>
                                                        <%--<label>Semester</label>--%>
                                                    </div>
                                                    <asp:DropDownList ID="ddlbulksem" runat="server" CssClass="form-control" TabIndex="5" data-select2-enable="true" ClientIDMode="Static" AppendDataBoundItems="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlbulksem"
                                                        Display="None" ErrorMessage="Please select Semester." InitialValue="0" ValidationGroup="EnrollSubmit"
                                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <asp:Label ID="lblDYUploadExcelFile" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <div id="Div1" class="logoContainer" runat="server">
                                                        <img src="../IMAGES/excel.png" alt="upload image" runat="server" id="imgUpFile" tabindex="2" />
                                                    </div>
                                                    <div class="fileContainer sprite pl-1">
                                                        <span runat="server" id="ufFile"
                                                            cssclass="form-control" tabindex="7">Upload File</span>
                                                        <asp:FileUpload ID="FileUpload1" runat="server" ToolTip="Select file to upload"
                                                            CssClass="form-control" onkeypress="" />
                                                        <asp:RequiredFieldValidator ID="rfvintake" runat="server" ControlToValidate="FileUpload1"
                                                            Display="None" ErrorMessage="Please select file to upload." ValidationGroup="Submit"
                                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12 btn-footer">
                                            <asp:HiddenField ID="hdfAdmission" runat="server" Value="0" />
                                            <asp:Button ID="btnEnrollDownload" runat="server" TabIndex="3"
                                                Text="Click To Download Pre-Requisite Format" OnClick="btnEnrollDownload_Click"
                                                CssClass="btn btn-outline-info" ToolTip="Click To Download Pre-Requisite Format" />
                                            <asp:Button ID="BtnProgram" runat="server" TabIndex="3"
                                                Text="Click To Download Program List" OnClick="BtnProgram_Click"
                                                CssClass="btn btn-outline-info" ToolTip="Click To Download Program List" />
                                            <asp:Button ID="btnEnrollVerify" runat="server" ValidationGroup="EnrollSubmit" TabIndex="4" OnClick="btnEnrollVerify_Click"
                                                Text="Upload and Verify" CssClass="btn btn-outline-primary" ToolTip="Click to Upload  & Verify" />
                                            <asp:Button ID="btnEnrollConfirm" runat="server" TabIndex="5" Visible="false"
                                                Text="Confirm" CssClass="btn btn-outline-primary" ToolTip="Confirm" OnClick="btnEnrollConfirm_Click" />
                                            <asp:Button ID="btnEnrollCancel" runat="server" Text="Cancel" ToolTip="Cancel" CausesValidation="false"
                                                TabIndex="6" OnClick="btnEnrollCancel_Click" CssClass="btn btn-outline-danger" />
                                            <asp:ValidationSummary ID="validationsummary1" runat="server" ValidationGroup="EnrollSubmit" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                        </div>
                                        <div class="col-md-12" runat="server" id="uploadapp" visible="false">
                                            <asp:Panel ID="Panel1" runat="server">
                                                <asp:ListView ID="LvTempApp" runat="server">
                                                    <LayoutTemplate>
                                                        <div class="sub-heading">
                                                            <h5>Uploded List</h5>
                                                        </div>
                                                        <table class="table table-striped table-bordered nowrap" style="width: 100%" id="table1">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                                    <th>
                                                                        <asp:CheckBox ID="cbHead" runat="server" OnClick="checkAllCheckbox(this)" />
                                                                    </th>
                                                                    <th>Application ID</th>
                                                                    <th>Student Name</th>
                                                                     <th>Session</th>
                                                                    <th>Current Program</th>
                                                                    <th>Apply For Program </th>

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
                                                                <asp:CheckBox ID="chktransfer" runat="server" ToolTip='<%# Eval("REGNO")%>' />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblUsername" runat="server" Text='<%# Eval("REGNO")%>'></asp:Label>
                                                                <asp:HiddenField ID="hdfolddegree" runat="server" Value='<%# Eval("OLD_DEGREENO")%>'/>
                                                                <asp:HiddenField ID="hdfnewdegree" runat="server" Value='<%# Eval("NEW_DEGREENO")%>'/>
                                                                <asp:HiddenField ID="hdfoldbranch" runat="server" Value='<%# Eval("OLDBRANCHNO")%>'/>
                                                                <asp:HiddenField ID="hdfnewbranch" runat="server" Value='<%# Eval("NEWBRANCHNO")%>'/>
                                                            </td>
                                                            <td>
                                                                <%# Eval("NAME_WITH_INITIAL")%>
                                                            </td>
                                                            <td><%# Eval("SESSION_NAME") %></td>
                                                            <td>
                                                                <%# Eval("OLDPROGRAM")%>                                        
                                                            </td>
                                                            <td>
                                                                <%# Eval("NEWPROGRAM")%> 
                                                            </td>

                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>
                                        <div class="col-md-12" runat="server" id="DivConfirm" visible="false">
                                            <asp:Panel ID="Panel3" runat="server">
                                                <asp:ListView ID="LvConfirm" runat="server">
                                                    <LayoutTemplate>
                                                        <div class="sub-heading">
                                                            <h5>Confirm List</h5>
                                                        </div>
                                                        <table class="table table-striped table-bordered nowrap" style="width: 100%" id="table1">
                                                            <thead class="bg-light-blue">
                                                                <tr>
                                                           
                                                                    <th>Application ID</th>
                                                                    <th>Student Name</th>
                                                                    <th>Semester</th>
                                                                     <th>Session</th>
                                                                    <th>Current Program</th>
                                                                    <th>Apply For Program </th>
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
                                                                <asp:Label ID="lblUsername" runat="server" Text='<%# Eval("REGNO")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <%# Eval("NAME_WITH_INITIAL")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("SEMESTERNAME")%>                                        
                                                            </td>
                                                             <td><%# Eval("SESSION_NAME") %></td>
                                                             <td>
                                                                <%# Eval("OLDPROGRAM")%> 
                                                            </td>
                                                            <td>
                                                                <%# Eval("NEWPROGRAM")%> 
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnEnrollDownload" />
                                    <asp:PostBackTrigger ControlID="btnEnrollVerify" />
                                    <asp:PostBackTrigger ControlID="BtnProgram" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript" language="javascript">

        function checkAllCheckboxBulk(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                var s = e.name.split("ctl00$ContentPlaceHolder1$LvTempApp$ctrl");
                var b = 'ctl00$ContentPlaceHolder1$LvTempApp$ctrl';
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
    </script>
    <script>

        var summary = "";
        $(function () {

            $('#btnShow').click(function () {
                localStorage.setItem("currentId", "#btnShow,Show");
                debugger;
                ShowLoader('#btnShow');
                if ($('#ddlAcademicSession').val() == "0")
                    summary += '<br>Please Select Academic Session';

                if ($('#ddlFaculty').val() == "0")
                    summary += '<br>Please Select Faculty';
                if ($('#ddlStudyLevel').val() == "0")
                    summary += '<br>Please Select Study Level';
                if ($('#lstbxProgram').val() == "")
                    summary += '<br>Please Select Program';
                if ($('#ddlSemester').val() == "0")
                    summary += '<br>Please Select Semester';

                if (summary != "") {
                    customAlert(summary);
                    summary = "";
                    return false
                }

            });
        });


    </script>
    <script type="text/javascript" language="javascript">

        function checkAllCheckbox(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                var s = e.name.split("ctl00$ContentPlaceHolder1$lvchangebranch$ctrl");
                var b = 'ctl00$ContentPlaceHolder1$lvchangebranch$ctrl';
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
        $(document).ready(function () {
            $(document).on("click", ".logoContainer", function () {
                $("#ctl00_ContentPlaceHolder1_FileUpload1").click();
            });
            $(document).on("keydown", ".logoContainer", function () {
                if (event.keyCode === 13) {
                    // Cancel the default action, if needed
                    event.preventDefault();
                    // Trigger the button element with a click
                    $("#ctl00_ContentPlaceHolder1_FileUpload1").click();
                }
            });
        });
    </script>

    <script type="text/javascript">
        function Focus() {
            //  alert("hii");
            document.getElementById("<%=imgUpFile.ClientID%>").focus();
        }
    </script>

    <script>
        $("input:file").change(function () {
            //$('.fuCollegeLogo').on('change', function () {

            var maxFileSize = 1000000;
            var fi = document.getElementById('ctl00_ContentPlaceHolder1_FileUpload1');
            myfile = $(this).val();
            var ext = myfile.split('.').pop();
            var res = ext.toUpperCase();

            //alert(res)
            //===========zip/rar file upload changes==========
            //if (res != "RAR" && res != "ZIP") {
            //  alert("Please Select rar,zip File Only.");

            if (res != "PDF" && res != "XLSX" && res != "XLS") {
                alert("Please Select xlsx,XLS File Only.");
                $('.logoContainer img').attr('src', "../IMAGES/excel.png");
                $(this).val('');
            }

            for (var i = 0; i <= fi.files.length - 1; i++) {
                var fsize = fi.files.item(i).size;
                if (fsize >= maxFileSize) {
                    alert("File Size should be less than 1 MB");
                    $('.logoContainer img').attr('src', "../IMAGES/excel.png");
                    $("#ctl00_ContentPlaceHolder1_FileUpload1").val("");

                }
            }

        });
    </script>

    <script>
        $("input:file").change(function () {
            var fileName = $(this).val();

            newText = fileName.replace(/fakepath/g, '');
            var newtext1 = newText.replace(/C:/, '');
            //newtext2 = newtext1.replace('//', ''); 
            var result = newtext1.substring(2, newtext1.length);


            if (result.length > 0) {
                $(this).parent().children('span').html(result);
            }
            else {
                $(this).parent().children('span').html("Choose file");
            }
        });
        //file input preview
        function readURL(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    //$('.logoContainer img').attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
            }
        }
        $("input:file").change(function () {
            readURL(this);
        });
    </script>
</asp:Content>

