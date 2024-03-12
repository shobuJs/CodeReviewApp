<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteMasterPage.master"
    CodeFile="SectionAllotmentFor_Backlog.aspx.cs" Inherits="SectionAllotmentFor_Backlog" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updSection"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:UpdatePanel ID="updSection" runat="server">
        <ContentTemplate>
            <div class="row">

                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">SECTION/CLASS ROLL NO ALLOTMENT -  ARREAR</h3>
                        </div>
                        <div style="color: Red; font-weight: bold">
                            &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                        </div>
                        <div class="box-body">
                            <div class="form-group col-md-12">
                                <div class="col-md-3">
                                    <label><span style="color: red;">*</span> Session</label>
                                    <asp:DropDownList ID="ddlsession" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                        ValidationGroup="teacherallot">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvAdmbatch" runat="server"
                                        ControlToValidate="ddlsession" Display="None"
                                        ErrorMessage="Please Select Session" InitialValue="0"
                                        ValidationGroup="teacherallot"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3">
                                    <label><span style="color: red;">*</span> Degree</label>
                                    <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control"
                                        ValidationGroup="teacherallot" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                        Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="teacherallot">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3">
                                    <label><span style="color: red;">*</span> Branch</label>
                                    <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                        ValidationGroup="teacherallot" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                        Display="None" ErrorMessage="Please Select Branch" InitialValue="0" ValidationGroup="teacherallot">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3">
                                    <label><span style="color: red;">*</span> Regulation</label>
                                    <asp:DropDownList ID="ddlscheme" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                        ValidationGroup="teacherallot" OnSelectedIndexChanged="ddlscheme_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlscheme"
                                        Display="None" ErrorMessage="Please Select Regulation" InitialValue="0" ValidationGroup="teacherallot">
                                    </asp:RequiredFieldValidator>
                                </div>


                            </div>
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <label><span style="color: red;">*</span> Semester</label>
                                    <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" AutoPostBack="true" ValidationGroup="teacherallot" CssClass="form-control">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Semester" ValidationGroup="teacherallot">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3">
                                    <label><span style="color: red;">*</span> Subject</label>
                                    <asp:DropDownList ID="ddlcourse" runat="server" AppendDataBoundItems="true" ValidationGroup="teacherallot" CssClass="form-control">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlcourse"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select Subject" ValidationGroup="teacherallot">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <br />
                                <br />

                                <div class="col-md-3">
                                    <asp:RadioButton ID="rbAll" runat="server" GroupName="stud" Text="All Students" Checked="True" />
                                    <asp:RadioButton ID="rbRemaining" runat="server" GroupName="stud" Text="Remain Students" />

                                </div>
                                <div class="col-md-3">
                                    <asp:RadioButton ID="rbRegNo" runat="server" GroupName="sort" Text="Univ. Reg. No." Checked="True" />
                                    <asp:RadioButton ID="rbStudName" runat="server" GroupName="sort" Text="Student Name" />
                                </div>
                            </div>

                        </div>

                        <div class="box-footer">
                            <p class="text-center">
                                <asp:Button ID="btnFilter" runat="server" Text="Show" ValidationGroup="teacherallot"
                                    CssClass="btn btn-outline-info" OnClick="btnFilter_Click" />
                                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-outline-danger" OnClick="btnClear_Click" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="teacherallot"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                <div class="col-md-3" style="display: none">
                                    <label>
                                        Total Selected Students</label>
                                    <asp:TextBox ID="txtTotStud" runat="server" CssClass="watermarked" Enabled="false" Style="text-align: center" />
                                    <asp:HiddenField ID="hdfTot" runat="server" Value="0" />
                                </div>
                                <div class="col-md-3" style="display: none">
                                    <label>
                                        Section</label>
                                    <asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="true">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="box-footer col-md-12 text-center" style="margin-top: 25px">
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-outline-info" OnClick="btnSubmit_Click" Text="Submit" />
                                </div>
                                <div class="col-md-12 table table-responsive">
                                    <div>
                                        <asp:Panel ID="pnlStudent" runat="server" Height="400px" ScrollBars="auto">
                                            <asp:ListView ID="lvStudents" runat="server" OnItemDataBound="lvStudents_ItemDataBound">
                                                <LayoutTemplate>
                                                    <div id="demo-grid">
                                                        <h4>Student List</h4>
                                                        <table class="table table-hover table-bordered">
                                                            <thead>
                                                                <tr class="bg-light-blue">
                                                                    <th>Univ. Reg. No.</th>
                                                                    <th>Name </th>
                                                                    <th>Section </th>
                                                                    <th>Roll No </th>
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
                                                        <td><%# Eval("REGNO")%>
                                                            <asp:CheckBox ID="cbRow" runat="server" onclick="totSubjects(this)" ToolTip='<%# Eval("IDNo")%>' Visible="false" />
                                                        </td>
                                                        <td><%# Eval("STUDNAME")%></td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlsec" runat="server" AppendDataBoundItems="true" ToolTip='<%# Eval("SECTIONNO")%>'>
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtRollNo" runat="server" Text='<%# Eval("ROLL_NO")%>' />
                                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftetxtTutMarks" runat="server" FilterType="Custom"
                                                                ValidChars="0123456789" TargetControlID="txtRollNo">
                                                            </ajaxToolKit:FilteredTextBoxExtender>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
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
	        alert('Please Select atleast one student/batch from student/batch list');
	        return false;
	    }
	    else
	        return true;
	}

    </script>

</asp:Content>

