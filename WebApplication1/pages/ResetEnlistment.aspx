<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ResetEnlistment.aspx.cs" Inherits="ACADEMIC_ResetEnlistment" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updReset"
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
    <asp:UpdatePanel ID="updReset" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="nav-tabs-custom">
                                <ul class="nav nav-tabs" role="tablist">
                                    <li class="nav-item">
                                        <a class="nav-link active" data-toggle="tab" tabindex="1" href="#tab1">Bulk Reset</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" data-toggle="tab" tabindex="2" href="#tab2">Single Reset</a>
                                    </li>

                                </ul>
                                <div class="tab-content" id="my-tab-content">
                                    <div class="tab-pane active" id="tab1">
                                        <div>
                                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updBulk"
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
                                        <asp:UpdatePanel ID="updBulk" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div class="row">
                                                    <div class="col-md-12 col-sm-12 col-12">
                                                        <div class="box-body">
                                                            <div class="col-12">
                                                                <div class="row">
                                                                    <%-- Tab 1 --%>
                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <asp:Label ID="lblDYCollege" runat="server" Font-Bold="true"></asp:Label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" data-select2-enable="true"
                                                                            AppendDataBoundItems="True" AutoPostBack="True"
                                                                            ToolTip="Please Select College" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlCollege"
                                                                            Display="None" ErrorMessage="Please Select College" ValidationGroup="submit"
                                                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <asp:Label ID="lblDYddlSession" runat="server" Font-Bold="true"></asp:Label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlSession" runat="server" CssClass="form-control" data-select2-enable="true"
                                                                            AppendDataBoundItems="True" AutoPostBack="True"
                                                                            ToolTip="Please Select Session" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSession"
                                                                            Display="None" ErrorMessage="Please Select Session" ValidationGroup="submit"
                                                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <asp:Label ID="lblDYScheme" runat="server" Font-Bold="true"></asp:Label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlScheme" runat="server" CssClass="form-control" data-select2-enable="true"
                                                                            AppendDataBoundItems="True" AutoPostBack="True"
                                                                            ToolTip="Please Select Curriculum" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlScheme"
                                                                            Display="None" ErrorMessage="Please Select Curriculum" ValidationGroup="submit"
                                                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <asp:Label ID="lblDYSemester" runat="server" Font-Bold="true"></asp:Label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlsemester" runat="server" CssClass="form-control" data-select2-enable="true"
                                                                            AppendDataBoundItems="True" AutoPostBack="True"
                                                                            ToolTip="Please Select Curriculum" OnSelectedIndexChanged="ddlsemester_SelectedIndexChanged">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlScheme"
                                                                            Display="None" ErrorMessage="Please Select Semester" ValidationGroup="submit"
                                                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-12 btn-footer">
                                                                <%-- Buttons --%>

                                                                <%--   --%>
                                                                <asp:Button ID="btnResetBulk" runat="server" OnClientClick="return ValidationBulk();" Text="Reset" CssClass="btn btn-outline-primary" OnClick="btnResetBulk_Click" />
                                                                <asp:Button ID="btnCancelBulk" runat="server" Text="Cancel" CssClass="btn btn-outline-danger" OnClick="btnCancelBulk_Click" />
                                                            </div>
                                                            <div class="col-12">
                                                                <%-- List View --%>
                                                                <asp:Panel ID="pnllvBulk" runat="server">
                                                                    <asp:ListView ID="lvResetBulk" runat="server">
                                                                        <LayoutTemplate>
                                                                            <div class="sub-heading">
                                                                                <h5>Student Details</h5>
                                                                            </div>
                                                                            <table id="tblStudentBulk" class="table table-striped table-bordered dtable nowrap  dataTable no-footer">
                                                                                <thead>
                                                                                    <tr class="bg-light-blue">
                                                                                        <th>
                                                                                            <asp:CheckBox ID="chkStdBulk" onclick="totAll(this)" ToolTip="Select All" runat="server" /></th>
                                                                                        <th><asp:Label ID="lblStudentId" runat="server" Font-Bold="true"></asp:Label></th>
                                                                                        <th><asp:Label ID="lblStudName" runat="server" Font-Bold="true"></asp:Label></th>
                                                                                        <th><asp:Label ID="lblDYBranch" runat="server" Font-Bold="true"></asp:Label></th>

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
                                                                                    <asp:CheckBox ID="chkStd" runat="server" />
                                                                                    <asp:HiddenField ID="hdfidno" runat="server" Value='<%# Eval("IDNO") %>' />
                                                                                </td>
                                                                                <td>
                                                                                    <%# Eval("REGNO") %>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lbllvStudent" runat="server" Text='<%# Eval("STUDNAME") %>'></asp:Label>
                                                                                </td>
                                                                                <td><asp:Label ID="lbllvProgram" runat="server" Text='<%# Eval("PROGRAM")%>'></asp:Label></td>
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
                                    </div>

                                    <div class="tab-pane" id="tab2">

                                        <div>
                                            <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updSingle"
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

                                        <asp:UpdatePanel ID="updSingle" runat="server">
                                            <ContentTemplate>
                                                <div class="row">
                                                    <div class="col-md-12 col-sm-12 col-12">
                                                        <div class="box-body">
                                                            <div class="col-12">
                                                                <div class="row">
                                                                    <%-- Selections --%>
                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <asp:Label ID="lblDYddlSession1" runat="server" Font-Bold="true"></asp:Label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlSessionSingle" runat="server" CssClass="form-control" data-select2-enable="true"
                                                                            AppendDataBoundItems="True" AutoPostBack="True"
                                                                            ToolTip="Please Select Session" OnSelectedIndexChanged="ddlSessionSingle_SelectedIndexChanged">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlSessionSingle"
                                                                            Display="None" ErrorMessage="Please Select Session" ValidationGroup="submit2"
                                                                            SetFocusOnError="True" InitialValue="0"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <asp:Label ID="lblStudentId" runat="server" Font-Bold="true"></asp:Label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtID" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtID"
                                                                            Display="None" ErrorMessage="Please Enter Student ID" ValidationGroup="submit2"
                                                                            SetFocusOnError="True" InitialValue=""></asp:RequiredFieldValidator>
                                                                    </div>

                                                                </div>

                                                            </div>
                                                            <div class="col-12 btn-footer">
                                                                <%-- Buttons --%>
                                                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-outline-info" ValidationGroup="submit2" OnClick="btnSearch_Click" />
                                                                <asp:Button ID="btnResetSingle" runat="server" Text="Reset" ToolTip="Reset Enlistment" OnClientClick="return confirm('Are you sure you want to Reset Enlistment?');" Visible="false" CssClass="btn btn-outline-primary" ValidationGroup="submit2" OnClick="btnResetSingle_Click" />
                                                                <asp:Button ID="btnClear" runat="server" Text="Cancel" CssClass="btn btn-outline-danger" OnClick="btnClear_Click" />
                                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                                                    ShowSummary="false" DisplayMode="List" ValidationGroup="submit2" />
                                                            </div>
                                                            <asp:Panel ID="pnlDetails" runat="server" Visible="false">
                                                                <div class="col-12 mb-3">
                                                                    <div class="row">

                                                                        <div class="col-lg-6 col-md-6 col-12">
                                                                            <div class="col-12 pl-0">
                                                                                <div class="sub-heading">
                                                                                    <h5>Student Details</h5>
                                                                                </div>
                                                                            </div>
                                                                            <ul class="list-group list-group-unbordered">
                                                                                <li class="list-group-item"><b>Student Id :</b>
                                                                                    <a class="sub-label">
                                                                                        <asp:Label ID="lblStudentIdSingle" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                                        <asp:HiddenField ID="hdfsrid" runat="server" />
                                                                                        <asp:HiddenField ID="hdfclgid" runat="server" />
                                                                                        <asp:HiddenField ID="hdfscheme" runat="server" />
                                                                                        <asp:HiddenField ID="hdfsem" runat="server" />
                                                                                    </a>

                                                                                </li>
                                                                                <li class="list-group-item"><b>Student Name :</b>
                                                                                    <a class="sub-label">
                                                                                        <asp:Label ID="lblNameSingle" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                                    </a>
                                                                                </li>
                                                                                <li class="list-group-item"><b>College :</b>
                                                                                    <a class="sub-label">
                                                                                        <asp:Label ID="lblFacultySingle" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                                    </a>
                                                                                </li>
                                                                                <li class="list-group-item"><b>Program :</b>
                                                                                    <a class="sub-label">
                                                                                        <asp:Label ID="lblProgramSingle" runat="server" Text="" ToolTip="1" Font-Bold="true"></asp:Label>
                                                                                    </a>
                                                                                </li>
                                                                                <li class="list-group-item"><b>Semester :</b>
                                                                                    <a class="sub-label">
                                                                                        <asp:Label ID="lblSemesterSingle" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                                    </a>
                                                                                </li>

                                                                            </ul>
                                                                        </div>

                                                                        <div class="col-lg-6 col-md-6 col-12">
                                                                            <asp:Panel ID="pnlCourseDetailsSingle" runat="server">
                                                                                <asp:ListView ID="lvCourseDetailsSingle" runat="server">
                                                                                    <LayoutTemplate>
                                                                                        <div class="sub-heading">
                                                                                            <h5>Subjects</h5>
                                                                                        </div>
                                                                                        <table class="table table-hover table-bordered">
                                                                                            <thead>
                                                                                                <tr class="bg-light-blue">
                                                                                                    <th>SrNo.</th>

                                                                                                    <th><asp:Label ID="lblDYSubjectName" runat="server" Font-Bold="true"></asp:Label></th>

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
                                                                                                <%#: Container.DataItemIndex + 1 %> 
                                                                                            </td>
                                                                                            <td>
                                                                                                <asp:Label ID="lblsubjectlv" runat="server" Text='<%# Eval("COURSE")%>'></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:ListView>
                                                                            </asp:Panel>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </asp:Panel>
                                                            <div class="col-12">
                                                                <%-- List View --%>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <%--<asp:PostBackTrigger ControlID="btnClear" />--%>
                                                <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>


                                </div>
                            </div>

                        </div>



                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
    <script>

        function ValidationBulk() {
            debugger;
            var ret = false;
            
                if ($('#ctl00_ContentPlaceHolder1_ddlCollege').val() == 0) {
                    var lblcollege = $('#ctl00_ContentPlaceHolder1_lblDYCollege').text();
                    alert("Please Select " + lblcollege + ".");
                    $('#ctl00_ContentPlaceHolder1_ddlCollege').focus();
                    ret = false;
                }
                else if ($('#ctl00_ContentPlaceHolder1_ddlSession').val() == "0") {
                    var lblsession = $('#ctl00_ContentPlaceHolder1_lblDYddlSession').text();
                    alert("Please Select " + lblsession + ".");
                    $('#ctl00_ContentPlaceHolder1_ddlSession').focus();
                    ret = false;
                }
                else if ($('#ctl00_ContentPlaceHolder1_ddlScheme').val() == "0") {
                    var lblscheme = $('#ctl00_ContentPlaceHolder1_lblDYScheme').text();
                    alert("Please Select " + lblscheme + ".");
                    $('#ctl00_ContentPlaceHolder1_ddlScheme').focus();
                    ret = false;
                }
                else if ($('#ctl00_ContentPlaceHolder1_ddlsemester').val() == "0") {
                    var lblsemester = $('#ctl00_ContentPlaceHolder1_lblDYSemester').text();
                    alert("Please Select " + lblsemester + ".");
                    $('#ctl00_ContentPlaceHolder1_ddlsemester').focus();
                    ret = false;
                }
                else {
                    ret = confirm('Are you sure you want to Reset Enlistment?');
                }
                
            
            return ret;
        }
    </script>

    <script>
        function totAll(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];

                if (e.type == 'checkbox') {
                    if (headchk.checked == true) {
                        e.checked = true;

                    }
                    else {
                        e.checked = false;

                    }
                }
            }
        }
    </script>


</asp:Content>

