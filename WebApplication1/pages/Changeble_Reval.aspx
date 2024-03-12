<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Changeble_Reval.aspx.cs" Inherits="ACADEMIC_Changeble_Reval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnlMain" runat="server" Visible="true">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title">
                            <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                    </div>
                    <div class="box-body">
                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Session</label>
                                    </div>
                                    <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" data-select2-enable="true" TabIndex="1"
                                        Font-Bold="true" ValidationGroup="show">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                        Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Faculty/School Name</label>
                                    </div>
                                    <asp:DropDownList ID="ddlfaculty" runat="server" AppendDataBoundItems="True" data-select2-enable="true" TabIndex="2"
                                        Font-Bold="true" ValidationGroup="show" OnSelectedIndexChanged="ddlfaculty_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlfaculty"
                                        Display="None" ErrorMessage="Please Select Faculty/School Name" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Student List</label>
                                    </div>
                                    <asp:DropDownList ID="ddlStudents" runat="server" AppendDataBoundItems="True" data-select2-enable="true" TabIndex="3"
                                        Font-Bold="true" ValidationGroup="show">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlStudents"
                                        Display="None" ErrorMessage="Please Select Students" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12" runat="server" visible="false">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <label>Module</label>
                                    </div>
                                    <asp:DropDownList ID="ddlmodule" runat="server" AppendDataBoundItems="True" data-select2-enable="true" TabIndex="3"
                                        Font-Bold="true" ValidationGroup="show">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlmodule"
                                        Display="None" ErrorMessage="Please Select Module" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" Text="Show"
                                    ValidationGroup="show" Font-Bold="True" CssClass="btn btn-outline-info" TabIndex="4" />
                                <asp:Button ID="btnSave" runat="server" Visible="false" Text="Save"
                                    OnClick="btnSave_Click" Font-Bold="True" ValidationGroup="val" CssClass="btn btn-outline-info" TabIndex="5" />
                                <asp:Button ID="btnReport" runat="server" Text="Report" OnClick="btnReport_Click" TabIndex="11"
                                    ValidationGroup="show" CssClass="btn btn-outline-primary" />
                                <asp:Button ID="btnCancel2" runat="server" Text="Cancel" OnClick="btnCancel2_Click"
                                    Font-Bold="True" CssClass="btn btn-outline-danger" TabIndex="6" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                    ShowSummary="False" ValidationGroup="show" />
                            </div>
                            <div class="col-md-12">
                                <asp:Panel ID="Panel1" runat="server" Visible="false">
                                    <asp:ListView ID="lvreval" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Reval Status List</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" id="mytable" style="width: 100%">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th style="text-align: center">Select
                                                        </th>
                                                        <th style="text-align: center">Module code-Name</th>
                                                        <th style="text-align: center">Module Credits</th>
                                                        <th style="text-align: center">Status</th>
                                                        <th style="text-align: center">View</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <td style="text-align: center">
                                                <asp:CheckBox ID="chkcheck" runat="server" />
                                                <asp:Label ID="lblchange" runat="server" Text='<%# Eval("CHANGEBLE")%>' Visible="false"></asp:Label>
                                            </td>
                                            <td style="text-align: center">
                                                <%# Eval("CCODE")%> - <%# Eval("COURSE_NAME")%>
                                                <asp:HiddenField ID="hdfcourseno" runat="server" Value='<%# Eval("COURSENO")%>' />
                                            </td>
                                            <td style="text-align: center">
                                                <%# Eval("CREDITS")%>
                                            </td>
                                            <td style="text-align: center">
                                                <asp:RadioButtonList ID="status" runat="server" RepeatDirection="Horizontal" ToolTip="Please select Option">
                                                    <asp:ListItem Value="1">Approved</asp:ListItem>
                                                    <asp:ListItem Value="2">Reject</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td style="text-align: center">
                                                <asp:LinkButton ID="lnkstuddoc" runat="server" CssClass="btn btn-outline-info" CommandArgument='<%#Eval("COURSENO") %>' OnClick="lnkstuddoc_Click"><i class="fa fa-eye"></i> View</asp:LinkButton>

                                            </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>


                                </asp:Panel>
                            </div>
                            <asp:UpdatePanel ID="updmodal" runat="server">
                                <ContentTemplate>
                                    <div class="modal" id="myModal22">
                                        <div class="modal-dialog  modal-lg">
                                            <div class="modal-content">
                                                <div class="modal-header">
                                                    <h4 class="modal-title">View Details</h4>
                                                    <button type="button" class="close" data-dismiss="modal" onclick="javascript:doButtonPostBack();">&times;</button>
                                                </div>
                                                <div class="modal-body pl-0 pr-0">
                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="col-lg-6 col-md-6 col-12">
                                                                <ul class="list-group list-group-unbordered">
                                                                    <li class="list-group-item"><b>Student ID :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblStudentID" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                                        </a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>Name :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblname" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                                    </li>
                                                                </ul>
                                                            </div>
                                                            <div class="col-lg-6 col-md-6 col-12">
                                                                <ul class="list-group list-group-unbordered">
                                                                    <li class="list-group-item"><b>Program :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblpgm" runat="server" Text="" Font-Bold="true"></asp:Label></a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>Faculty/School Name :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblclg" runat="server" Font-Bold="true"></asp:Label>
                                                                        </a>
                                                                    </li>
                                                                </ul>
                                                            </div>
                                                            <div class="col-lg-6 col-md-6 col-12">
                                                                <ul class="list-group list-group-unbordered">
                                                                    <li class="list-group-item"><b>Semester :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblsemester" runat="server" ToolTip="1" Font-Bold="true"></asp:Label></a>
                                                                    </li>
                                                                </ul>
                                                            </div>
                                                            <div class="col-lg-6 col-md-6 col-12">
                                                                <ul class="list-group list-group-unbordered">
                                                                    <li class="list-group-item"><b>Module Code-Name:</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblmodulename" runat="server" Font-Bold="true"></asp:Label></a>
                                                                    </li>
                                                                </ul>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="col-lg-6 col-md-6 col-12">
                                                                <ul class="list-group list-group-unbordered">
                                                                    <li class="list-group-item"><b>Overall % :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lbloveralper" runat="server" Font-Bold="true"></asp:Label></a>
                                                                    </li>
                                                                </ul>
                                                            </div>
                                                        </div>
                                                    </div>
                                                <div class="col-12 mt-3" id="marksdetails">
                                                    <div class="col-md-12" id="marks" runat="server" visible="false">
                                                        <asp:Panel ID="Panel2" runat="server">
                                                            <asp:ListView ID="lvmarksdetails" runat="server">
                                                                <LayoutTemplate>
                                                                    <div class="sub-heading">
                                                                        <h5>Marks Details</h5>
                                                                    </div>
                                                                    <table class="table table-striped table-bordered nowrap" style="width: 100%" id="myTable">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>Sr.No.
                                                                                </th>
                                                                                <th>Components</th>
                                                                                <th>Exam Name</th>
                                                                                <th>OutOfmarks</th>
                                                                                <th>Weightage</th>
                                                                                <th>Obtained</th>
                                                                                <th>Mark Converted</th>
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
                                                                            <%# Container.DataItemIndex + 1 %>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("COMPONENTNAME")%> 
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("EXAMNAME")%> 
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("OUTOFMARKS")%> 
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("WEIGHTAGE")%> 
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("MARKS")%> 
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("MARKS_CONVERSION")%> 
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
                                    </div>
                                </ContentTemplate>

                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="divMsg" runat="server">
        </div>
    </asp:Panel>
    <script type="text/javascript" language="javascript">

        function checkAllCheckbox(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                var s = e.name.split("ctl00$ContentPlaceHolder1$lvreval$ctrl");
                var b = 'ctl00$ContentPlaceHolder1$lvreval$ctrl';
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

</asp:Content>

