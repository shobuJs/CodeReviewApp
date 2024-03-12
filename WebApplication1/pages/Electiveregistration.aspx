<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Electiveregistration.aspx.cs" Inherits="ACADEMIC_Electiveregistration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updBulkReg"
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

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title"><span>ELECTIVE COURSE REGISTRATION</span></h3>
                </div>

                <asp:UpdatePanel ID="updBulkReg" runat="server">
                    <ContentTemplate>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>College /School Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged"
                                            AutoPostBack="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvcollege" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select College" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Admission Batch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlAdmBatch" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                            
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Session</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" Font-Bold="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>     
                                                       
                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none;">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Department</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDept" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                            CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display:none;">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>College /School Name</label>
                                        </div>
                                        <label>Curriculum Type</label>
                                        <asp:DropDownList ID="ddlSchemeType" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                            AutoPostBack="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Branch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                            InitialValue="0" Display="None" ValidationGroup="submit" ErrorMessage="Please Select Branch"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Curriculum</label>
                                        </div>
                                        <asp:DropDownList ID="ddlScheme" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                                            InitialValue="0" Display="None" ValidationGroup="submit" ErrorMessage="Please Select Curriculum"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Semester/Year</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        &nbsp;
                                        <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                            Display="None" ErrorMessage="Please Select Semester/Year" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none;">
                                        <div class="label-dynamic">
                                            <label>Student Status</label>
                                        </div>
                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                            <asp:ListItem Value="0">Regular Student</asp:ListItem>
                                            <asp:ListItem Value="1">Absorption Student</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Section</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSection" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                            OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display: none;">
                                        <div class="label-dynamic">
                                            <asp:HiddenField ID="hftot" runat="server" />
                                            <label>Total Students Selected</label>
                                        </div>
                                        <asp:TextBox ID="txtTotStud" runat="server" Text="0" Enabled="False" CssClass="form-control" data-select2-enable="true"
                                            Style="text-align: center;" BackColor="#FFFFCC" Font-Bold="True"
                                            ForeColor="#000066"></asp:TextBox>
                                    </div>
                               
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:LinkButton ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" OnClientClick="return validateAssign();"
                                        Text="Submit" Enabled="false" ValidationGroup="submit" CssClass="btn btn-outline-info"><i class=" fa fa-save"></i> Submit</asp:LinkButton>
                                <asp:LinkButton ID="btnReport" runat="server" OnClick="btnReport_Click"
                                         Text="Registration Slip Report" ValidationGroup="submit" CssClass="btn btn-outline-primary"><i class="fa fa-file-pdf-o" aria-hidden="true"></i> Registration Slip Report</asp:LinkButton>
                                <asp:Button ID="btnCancel" runat="server" CausesValidation="False" Text="Cancel"
                                        CssClass="btn btn-outline-danger" OnClick="btnCancel_Click" Font-Bold="True" />                  
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                    ValidationGroup="submit" ShowSummary="false" />
                            </div>

                            <div class="col-12" id="divpanels" runat="server">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divcourse" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Elective Subjects</label>
                                        </div>
                                        <asp:DropDownList ID="ddlcourselist" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlcourselist_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-lg-9 col-md-12 col-12">
                                        <asp:Panel ID="pnlStudents" runat="server" Visible="false">
                                            <asp:ListView ID="lvStudent" runat="server">
                                                <LayoutTemplate>
                                                    <div>
                                                        <div class="sub-heading">
                                                            <h5>Student List</h5>
                                                        </div>
                                                        <table id="tblHead" class="table table-striped table-bordered nowrap display" style="width:100%">
                                                            <thead class="bg-light-blue">
                                                                <tr id="trRow">
                                                                    <th>
                                                                        <asp:CheckBox ID="cbHead" runat="server" onclick="SelectAll(this)" ToolTip="Select/Select all" />
                                                                    </th>
                                                                    <th>Univ. Reg. No.
                                                                    </th>
                                                                     <th>Adm. No.
                                                                    </th>
                                                                    <th>Student Name
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
                                                            <asp:CheckBox ID="cbRow" runat="server" ToolTip='<%# Eval("IDNO") %>' onClick="totStudents(this);"  />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblRollno" runat="server" Text='<%# Eval("REGNO") %>' ToolTip='<%# Eval("IDNO") %>' />
                                                        </td>
                                                         <td>
                                                            <%# Eval("ENROLLNO")%>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblStudName" runat="server" Text='<%# Eval("NAME") %>' ToolTip='<%# Eval("REGISTERED") %>' />
                                                            <asp:Label ID="lblIDNo" runat="server" Text='<%# Eval("REGNO") %>' ToolTip='<%# Eval("IDNO") %>'
                                                                Visible="false" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>

                                <div class="col-lg-6 col-md-12 col-12" style="display: none">
                                    <asp:Panel ID="pnlCourses" runat="server" Visible="true">
                                        <asp:ListView ID="lvCourse" runat="server" OnItemDataBound="lvCourse_ItemDataBound">
                                            <LayoutTemplate>
                                                <div id="divlvPaidReceipts">
                                                    <div class="sub-heading">
                                                        <h5>Offered Subjects</h5>
                                                    </div>

                                                    <table class="table table-striped table-bordered nowrap display" style="width:100%" id="myTable1">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Select
                                                                </th>
                                                                <th>Subject Code - Subject Name
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
                                                        <asp:CheckBox ID="cbRow" runat="server" Checked="true" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO") %>' />
                                                        &nbsp;
                                                                        <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' ToolTip='<%# Eval("ELECT") %>' />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <AlternatingItemTemplate>
                                                <tr class="altitem">
                                                    <td>
                                                        <asp:CheckBox ID="cbRow" runat="server" Checked="true" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO") %>' />&nbsp;
                                                        <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' ToolTip='<%# Eval("ELECT") %>' />
                                                    </td>
                                                </tr>
                                            </AlternatingItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>

                                <div class="col-12">
                                    <asp:Panel ID="pnlStudentsReamin" runat="server">
                                        <asp:ListView ID="lvStudentsRemain" runat="server">
                                            <LayoutTemplate>
                                                <div>
                                                    <div class="sub-heading">
                                                        <h5>Student List (Demand Not Found)</h5>
                                                    </div>

                                                    <table id="tblHead" class="table table-striped table-bordered nowrap display" style="width:100%">
                                                        <thead class="bg-light-blue">
                                                            <tr id="trRow">
                                                                <th>HT No.
                                                                </th>
                                                                <th>Student Name
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
                                                        <asp:Label ID="lblRollno" runat="server" Text='<%# Eval("ROLLNO") %>' ToolTip='<%# Eval("ROLLNO") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblStudName" runat="server" Text='<%# Eval("NAME") %>' />
                                                        <asp:Label ID="lblIDNo" runat="server" Text='<%# Eval("IDNO") %>' ToolTip='<%# Eval("REGNO") %>'
                                                            Visible="false" />
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
            </div>
        </div>

        <div id="divMsg" runat="server">
        </div>
    </div>

    <script type="text/javascript" language="javascript">
        function SelectAll(headchk) {
            var i = 0;
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');
            var hftot = document.getElementById('<%= hftot.ClientID %>').value;
            var count = 0;
            for (i = 0; i < Number(hftot) ; i++) {

                var lst = document.getElementById('ctl00_ContentPlaceHolder1_lvStudent_ctrl' + i + '_cbRow');
                if (lst.type == 'checkbox') {
                    if (headchk.checked == true) {
                        if (lst.disabled == false) {
                            lst.checked = true;
                            count = count + 1;
                        }
                    }
                    else {
                        lst.checked = false;
                    }
                }
            }

            if (headchk.checked == true) {
                document.getElementById('<%= txtTotStud.ClientID %>').value = count;
            }
            else {
                document.getElementById('<%= txtTotStud.ClientID %>').value = 0;
            }
        }

        function validateAssign() {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>').value;
            if (txtTot == 0) {
                alert('Please Check atleast one student ');
                return false;
            }
            else if (document.getElementById('<%= ddlcourselist.ClientID %>').value == 0) {
                alert('Please Select Elective Course ');
                return false;
            }
            else
                return true;
        }


        function totStudents(chk) {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');

            if (chk.checked == true)
                txtTot.value = Number(txtTot.value) + 1;
            else
                txtTot.value = Number(txtTot.value) - 1;
        }
    </script>
</asp:Content>
