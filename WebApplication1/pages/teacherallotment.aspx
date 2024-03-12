<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="teacherallotment.aspx.cs" Inherits="ACADEMIC_teacherallotment" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title"><span>STUDENT'S TEACHER ALLOTMENT</span></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>College & Curriculum</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged"
                                            AutoPostBack="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvcollege" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" SetFocusOnError="true" ErrorMessage="Please Select College & Curriculum" InitialValue="0" ValidationGroup="teacherallot"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Session</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem>Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Session" ValidationGroup="teacherallot">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" ValidationGroup="teacherallot" CssClass="form-control" data-select2-enable="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Degree" ValidationGroup="teacherallot">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Branch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" ValidationGroup="teacherallot" CssClass="form-control" data-select2-enable="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Branch" ValidationGroup="teacherallot">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Curriculum</label>
                                        </div>
                                        <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            ValidationGroup="teacherallot" AutoPostBack="True" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Curriculum" ValidationGroup="teacherallot">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Semester/Year</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true" ValidationGroup="teacherallot" CssClass="form-control" data-select2-enable="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Semester" ValidationGroup="teacherallot">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Subject Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSubjectType" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            ValidationGroup="teacherallot" AutoPostBack="True" OnSelectedIndexChanged="ddlSubjectType_SelectedIndexChanged1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSubjectType" runat="server" ControlToValidate="ddlSubjectType"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Subject Type" ValidationGroup="teacherallot">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Subject</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCourse" runat="server" AppendDataBoundItems="true" ValidationGroup="teacherallot" CssClass="form-control" data-select2-enable="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCourse" runat="server" ControlToValidate="ddlCourse"
                                            Display="None" ErrorMessage="Please Select Subject" InitialValue="0" ValidationGroup="teacherallot">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Section</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="true" ValidationGroup="teacherallot" CssClass="form-control" data-select2-enable="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSection" runat="server" ControlToValidate="ddlSection"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Section" ValidationGroup="teacherallot">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="trthprteacher" runat="server">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Theory/Practical/Tutorial/Unaudit</label>
                                        </div>
                                        <asp:DropDownList ID="ddltheorypractical" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            ValidationGroup="teacherallot" AutoPostBack="True" OnSelectedIndexChanged="ddltheorypractical_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvThpr" runat="server" ControlToValidate="ddltheorypractical"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Theory or Practical or Tutorial Subject" ValidationGroup="teacherallot">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-6 col-md-6 col-12" runat="server">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Sort By</label>
                                        </div>
                                        <asp:RadioButtonList runat="server" ID="rbSortBy" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0" Selected="True" Text="&nbsp;Univ. Reg. No.&nbsp;"></asp:ListItem>
                                            <asp:ListItem Value="4" Text="&nbsp;TAN&nbsp;"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="&nbsp;PAN&nbsp;"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="&nbsp;Roll. No.&nbsp;"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="&nbsp;Name&nbsp;"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>

                                    <div id="dvBatch" runat="server" class="form-group col-lg-3 col-md-6 col-12" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Batch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlBatch" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" ValidationGroup="teacherallot"
                                            OnSelectedIndexChanged="ddlbatch_SelectedIndexChanged">
                                            <asp:ListItem Value="100">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBatch" runat="server" ControlToValidate="ddlBatch"
                                            Display="None" InitialValue="100" ErrorMessage="Please Select Batch" ValidationGroup="teacherallot">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none;">
                                        <div class="label-dynamic">
                                            <label></label>
                                        </div>
                                        <asp:RadioButton ID="rbAll" runat="server" GroupName="stud" Text="All Students" Checked="True" />&nbsp;&nbsp;
                                        <asp:RadioButton ID="rbRemaining" runat="server" GroupName="stud" Text="Remaining Students" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnFilter" runat="server" Text="Show" ValidationGroup="teacherallot"
                                    OnClick="btnFilter_Click" CssClass="btn btn-outline-info" />
                                <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" CssClass="btn btn-outline-danger" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="teacherallot"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>

                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Teacher</label>
                                        </div>
                                        <asp:DropDownList ID="ddlTeacher" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            ValidationGroup="teacherassign">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvTeacher" runat="server" ControlToValidate="ddlTeacher"
                                            Display="None" ErrorMessage="Please Select Teacher" InitialValue="0" ValidationGroup="teacherassign"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label></label>
                                        </div>
                                        <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="teacherassign"
                                            CssClass="btn btn-outline-info" OnClick="btnSave_Click" />
                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="teacherassign"
                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 mt-3">
                                <asp:Panel ID="Panel1" runat="server">
                                    <%--  OnItemDataBound="lvStudents_ItemDataBound"--%>

                                    <asp:ListView ID="lvStudents" runat="server">
                                        <LayoutTemplate>
                                            <div id="demo-grid">
                                                <div class="sub-heading">
                                                    <h5>Student List</h5>
                                                </div>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="myTable1">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>
                                                            <asp:CheckBox ID="cbHead" runat="server" onclick="totAllSubjects(this)" />
                                                        </th>
                                                        <th class="text-center">Univ. Reg. No.
                                                        </th>
                                                        <th class="text-center">TAN
                                                        </th>
                                                        <th class="text-center">PAN
                                                        </th>
                                                        <th class="text-center">Roll No.
                                                        </th>
                                                        <th class="text-center">Section
                                                        </th>
                                                        <th class="text-center">Student Name
                                                        </th>
                                                        <th class="text-center">Teacher Name [TH]
                                                        </th>
                                                        <th class="text-center">Teacher Name [PR]
                                                        </th>
                                                        <th class="text-center">Teacher Name [TU]
                                                        </th>
                                                        <th>Batch
                                                        </th>
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
                                                        <asp:CheckBox ID="cbRow" runat="server" ToolTip='<%# Eval("IDNO")%>' />
                                                    </td>
                                                    <td>
                                                        <%# Eval("REGNO")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("TANNO")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("ENROLLNO") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("ROLLNO")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("SECTIONNAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("STUDNAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("UA_FULLNAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("UA_FULLNAME_PRAC")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("UA_FULLNAME_TUTR")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("BATCHNAME")%>
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

    <script type="text/javascript" language="javascript">
        function totAllSubjects(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (headchk.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }
    </script>

</asp:Content>
