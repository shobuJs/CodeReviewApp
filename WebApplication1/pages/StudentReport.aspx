<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="StudentReport.aspx.cs" Inherits="Academic_StudentReport" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">STUDENT REPORT</h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div>
                            <asp:Label ID="lblMsg" runat="server" SkinID="Msglbl"></asp:Label>
                        </div>
                        <div class="row">
                            <div class="form-group col-lg-6 col-md-6 col-12">
                                <div class="sub-heading">
                                    <h5>Report Type</h5>
                                </div>       
                                <asp:RadioButton ID="rdoVerticalReport" runat="server" Text="Vertical Report"
                                    Checked="True" GroupName="A" />&nbsp;&nbsp;&nbsp;
                                <asp:RadioButton ID="rdoHorizontalReport" runat="server"
                                    Text="Horizontal Report" GroupName="A" />
                            </div>

                            <div class="form-group col-lg-6 col-md-6 col-12">
                                <div class="sub-heading">
                                    <h5>Single Student Record</h5>
                                </div> 
                                <div class="col-12">
                                    <div class="form-inline">
                                        <div class="label-dynamic mt-1">
                                            <label>Search by :</label>
                                        </div>&nbsp;&nbsp;&nbsp;  
                                        <asp:RadioButton ID="rdoRegno" Text="Univ.Reg.No."
                                             runat="server" Checked="true" GroupName="stud" />&nbsp;&nbsp;&nbsp;
                                        <asp:RadioButton ID="rdoTan" Text="TAN"
                                             runat="server" GroupName="stud" />&nbsp;&nbsp;&nbsp;
                                        <asp:RadioButton ID="rdoEnrollmentNo" Text="PAN"
                                             runat="server" GroupName="stud" />&nbsp;&nbsp;&nbsp;
                                        <asp:RadioButton ID="rdoStudentName" Text="Student Name" runat="server"
                                            GroupName="stud" />&nbsp;&nbsp;&nbsp;
                                        <%--<asp:RadioButton ID="rdoRollNo" Text="Roll No" runat="server" GroupName="stud" />--%>
                                        <%--<asp:RadioButton ID="rdoIdNo" Text="Student Id" runat="server" GroupName="stud" />--%>
                                    </div>
                                </div>
                                <div class="form-group col-12 mt-2">
                                    <div class="form-inline">
                                        <asp:TextBox ID="txtSearchText" runat="server" CssClass="form-control" />
                                        <asp:RequiredFieldValidator ID="rfvSearchText" runat="server" ControlToValidate="txtSearchText"
                                            ErrorMessage="Please Enter Value" ValidationGroup="Search" SetFocusOnError="true" Display="None">
                                        </asp:RequiredFieldValidator>
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                                            ShowMessageBox="True" ShowSummary="False" ValidationGroup="Search" />

                                        <asp:Button ID="btnSearch" Text="Search" runat="server" OnClick="btnSearch_Click"
                                        ValidationGroup="Search" CssClass="btn btn-outline-info m-top" />
                                    </div>
                                </div>
                    </div>
                        </div>
                    </div>

                    <div class="col-12">
                        <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
                            <asp:ListView ID="lvStudentRecords" runat="server">
                                <LayoutTemplate>
                                    <div id="listViewGrid">
                                        <div class="sub-heading">
                                            <h5>Search Results</h5>
                                        </div>
                                            
                                        <table class="table table-striped table-bordered nowrap display" style="width:100%" id="myTable1">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Report
                                                    </th>

                                                    <th>Student Name
                                                    </th>
                                                    <th>TAN/PAN
                                                    </th>
                                                    <th>Degree
                                                    </th>
                                                    <th>Branch
                                                    </th>
                                                    <th>Year
                                                    </th>
                                                    <th>Semester
                                                    </th>
                                                    <th>Batch
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                        </table>
                                    </div>
                                </LayoutTemplate>
                                <EmptyDataTemplate>
                                </EmptyDataTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="btnShowReport" runat="server" AlternateText="Show Report" CommandArgument='<%# Eval("IDNO") %>'
                                                ImageUrl="~/images/print.gif" ToolTip="Show Report" OnClick="btnShowReport" />
                                        </td>
                                        <td>
                                            <%# Eval("NAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("ENROLLMENTNO")%>
                                        </td>
                                        <td>
                                            <%# Eval("DEGREENO")%>
                                        </td>
                                        <td>
                                            <%# Eval("SHORTNAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("YEARNAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("SEMESTERNAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("BATCHNAME")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <EmptyDataTemplate>
                                    <div align="center" class="data_label">
                                        -- No Student Record Found --
                                    </div>
                                </EmptyDataTemplate>
                            </asp:ListView>
                        </asp:Panel>
                    </div>

                    <div class="col-12 btn-footer">
                        <asp:Button ID="btnReport" runat="server" Text="Report" OnClick="btnReport_Click"
                            TabIndex="10" ValidationGroup="report" Visible="False" CssClass="btn btn-outline-primary" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                            OnClick="btnCancel_Click" TabIndex="11" Visible="False" CssClass="btn btn-outline-danger" />
                        <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="report" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="divMsg" runat="server">
    </div>
</asp:Content>
