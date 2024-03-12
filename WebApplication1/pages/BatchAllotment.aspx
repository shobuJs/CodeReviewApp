<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="BatchAllotment.aspx.cs" Inherits="ACADEMIC_BatchAllotment" %>

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
                            <h3 class="box-title"><span>BATCH ALLOTMENT</span></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>College & Curriculum</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            ValidationGroup="teacherallot" AutoPostBack="True" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlCollege" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select College" ValidationGroup="teacherallot"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Session</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" ValidationGroup="teacherallot"
                                            CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Session" ValidationGroup="teacherallot"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            ValidationGroup="teacherallot" AutoPostBack="True" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Degree" ValidationGroup="teacherallot"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Branch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            ValidationGroup="teacherallot" AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Branch" ValidationGroup="teacherallot"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Curriculum</label>
                                        </div>
                                        <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            ValidationGroup="teacherallot" AutoPostBack="True" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Curriculum" ValidationGroup="teacherallot"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Semester / Year</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            ValidationGroup="teacherallot" AutoPostBack="True" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Semester / Year" ValidationGroup="teacherallot"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Subject Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSubjectType" runat="server" AppendDataBoundItems="true"
                                            CssClass="form-control" data-select2-enable="true" ValidationGroup="teacherallot" AutoPostBack="True" OnSelectedIndexChanged="ddlSubjectType_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSubjectType" runat="server" ControlToValidate="ddlSubjectType"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Subject Type" ValidationGroup="teacherallot"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Subject</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCourse" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            ValidationGroup="teacherallot" AutoPostBack="True" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCourse" runat="server" ControlToValidate="ddlCourse"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Subject" ValidationGroup="teacherallot"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Section</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            ValidationGroup="teacherallot">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSection" runat="server" ControlToValidate="ddlSection"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Section" ValidationGroup="teacherallot"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Sort By</label>
                                        </div>
                                        <asp:RadioButton ID="rbRegNo" runat="server" GroupName="sortBy" Text="&nbsp;Univ. Reg. No." />&nbsp;&nbsp;&nbsp;
                                        <asp:RadioButton ID="rbTAN" runat="server" GroupName="sortBy" Text="&nbsp;TAN" />&nbsp;&nbsp;&nbsp;
                                        <asp:RadioButton ID="rbAdmno" runat="server" GroupName="sortBy" Checked="true" Text="&nbsp;PAN" />&nbsp;&nbsp;&nbsp;                                                      
                                        <asp:RadioButton ID="rbRollNo" runat="server" GroupName="sortBy" Text="&nbsp;Roll. No." />&nbsp;&nbsp;&nbsp;
                                        <asp:RadioButton ID="rbUSNNo" runat="server" GroupName="sortBy" Text="&nbsp;Name" />&nbsp;&nbsp;&nbsp;
                                    </div>

                                    <div id="trFilter" runat="server" class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Filter By</label>
                                        </div>
                                    </div>

                                    <div id="trRollNo" runat="server" class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>By Univ. Reg. No.</label>
                                        </div>
                                        <asp:TextBox ID="txtFromRollNo" runat="server" CssClass="form-control" />
                                        To
                                        <asp:TextBox ID="txtToRollNo" runat="server" CssClass="form-control" />
                                        <asp:CompareValidator ID="svFromTo" runat="server" ControlToCompare="txtFromRollNo"
                                            ControlToValidate="txtTotStud" Display="None" ErrorMessage="Please Valid Range"
                                            Operator="LessThanEqual" Type="Integer" ValidationGroup="teacherallot"></asp:CompareValidator>
                                    </div>

                                    <div id="trRdo" runat="server" class="form-group col-lg-3 col-md-6 col-12">
                                        <asp:RadioButton ID="rbAll" runat="server" GroupName="stud" Text="All Students" Checked="True" />&nbsp;&nbsp;
                                        <asp:RadioButton ID="rbRemaining" runat="server" GroupName="stud" Text="Remaining Students" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnFilter" runat="server" Text="Show" ValidationGroup="teacherallot"
                                    OnClick="btnFilter_Click" CssClass="btn btn-outline-info" />
                                <asp:Button ID="btnReport" runat="server" Text="Report" CssClass="btn btn-outline-primary" ValidationGroup="teacherallot" OnClick="btnReport_Click" />
                                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-outline-danger" OnClick="btnClear_Click" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="teacherallot"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                           
                                <asp:Label ID="lblStatus" runat="server" SkinID="Errorlbl" />
                            </div>

                            <div class="col-12">
                                <div class="row">
                                    <div id="dvAttFor" runat="server" class="form-group col-lg-3 col-md-6 col-12" style="z-index: 0;" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Attendance For Tuitorial/Practical</label>
                                        </div>
                                        <asp:DropDownList ID="ddlAttfor" runat="server" AppendDataBoundItems="true" ValidationGroup="teacherassign"
                                            CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvAttfor" runat="server" ControlToValidate="ddlAttfor"
                                            Display="None" ErrorMessage="Please Select Attendance For Tutorial/Practical" InitialValue="0" ValidationGroup="teacherassign"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" style="z-index: 0;">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Batch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlBatch" runat="server" AppendDataBoundItems="true" ValidationGroup="teacherassign"
                                            CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBatch" runat="server" ControlToValidate="ddlBatch"
                                            Display="None" ErrorMessage="Please Select Batch" InitialValue="0" ValidationGroup="teacherassign"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" style="margin-top: 25px">
                                        <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="teacherassign"
                                            class="btn btn-outline-info" OnClick="btnSave_Click" />
                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="teacherassign"
                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Total Selected Students </label>
                                        </div>
                                        <asp:TextBox ID="txtTotStud" runat="server" Enabled="false" CssClass="form-control" />
                                        <asp:HiddenField ID="hdfTot" runat="server" Value="0" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-12">
                                <asp:Label ID="lblStatus2" runat="server" SkinID="Errorlbl" />
                            </div>

                            <div class="col-12 mt-3">
                                <asp:Panel ID="pnlStudent" runat="server">
                                    <div class="table table-responsive">
                                        <asp:ListView ID="lvStudents" runat="server" OnItemDataBound="lvStudents_ItemDataBound">
                                            <LayoutTemplate>
                                                <div id="demo-grid">
                                                    <div class="sub-heading">
                                                        <h5>Student List</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap display" style="width:100%" id="myTable1">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>
                                                                    <asp:CheckBox ID="cbHead" runat="server" onclick="totAllSubjects(this)" />
                                                                </th>
                                                                <th>Univ. Reg. No.
                                                                </th>
                                                                <th>TAN
                                                                </th>
                                                                <th>PAN
                                                                </th>
                                                                <th>Roll No.
                                                                </th>
                                                                <th>Name
                                                                </th>
                                                                <th>Tutorial Batch
                                                                </th>
                                                                <th>Practical Batch
                                                                </th>
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
                                                        <asp:CheckBox ID="cbRow" runat="server" onclick="totSubjects(this)" ToolTip='<%# Eval("IDNo")%>' />
                                                    </td>
                                                    <td>
                                                        <%# Eval("REGNO")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("TANNO")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("ENROLLNO")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("ROLLNO") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("STUDNAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("TH_BATCHNAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("PR_BATCHNAME")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript" language="javascript">

        function totSubjects(chk) {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');

            if (chk.checked == true)
                txtTot.value = Number(txtTot.value) + 1;
            else
                txtTot.value = Number(txtTot.value) - 1;
        }

        function totAllSubjects(headchk) {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');
            var hdfTot = document.getElementById('<%= hdfTot.ClientID %>');

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

            if (headchk.checked == true)
                txtTot.value = hdfTot.value;
            else
                txtTot.value = 0;
        }

        function validateAssign() {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>').value;

        if (txtTot == 0) {
            alert('Please Select at least one student from student list');
            return false;
        }
        else
            return true;
    }

    </script>

</asp:Content>
