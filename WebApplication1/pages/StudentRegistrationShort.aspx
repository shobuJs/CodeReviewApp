<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="StudentRegistrationShort.aspx.cs" Inherits="ACADEMIC_StudentRegistrationShort" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">Student Basic Details Registration</h3>
                    <div class="box-tools pull-right">
                    </div>
                </div>
                <asp:UpdatePanel ID="updStudent" runat="server">
                    <ContentTemplate>
                        <div class="box-body">
                            <div class="form-group col-md-4">
                                <label>Student Name</label>
                                <asp:TextBox ID="txtStudname" runat="server" MaxLength="100"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvDept" runat="server" ControlToValidate="txtStudname"
                                    Display="None" ErrorMessage="Please Enter Student Name" SetFocusOnError="true"
                                    ValidationGroup="Show"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-md-4">
                                <label>Exam Roll Number</label>
                                <asp:TextBox ID="txtRollno" runat="server" MaxLength="100"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRollno"
                                    Display="None" ErrorMessage="Please Enter Exam Roll Number" SetFocusOnError="true"
                                    ValidationGroup="Show"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-md-4">
                                <label>Registration Number</label>
                                <asp:TextBox ID="txtRegno" runat="server" MaxLength="100"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtRegno"
                                    Display="None" ErrorMessage="Please Enter Registration Number" SetFocusOnError="true"
                                    ValidationGroup="Show"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-md-4">
                                <label>Admission Batch</label>
                                <asp:DropDownList ID="ddlAdmBatch" runat="server" AppendDataBoundItems="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlAdmBatch"
                                    Display="None" ErrorMessage="Please Select Admissiom Batch" InitialValue="0" SetFocusOnError="true"
                                    ValidationGroup="Show"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-md-4">
                                <label>College Name </label>
                                <asp:DropDownList ID="ddlCollegeName" runat="server" TabIndex="1" AppendDataBoundItems="True"
                                    ValidationGroup="Show" ToolTip="College Name" AutoPostBack="True" OnSelectedIndexChanged="ddlCollegeName_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvcollege" runat="server" ControlToValidate="ddlCollegeName"
                                    Display="None" ErrorMessage="Please Select College Name" InitialValue="0" SetFocusOnError="true"
                                    ValidationGroup="Show"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="rfvcollegelv" runat="server" ControlToValidate="ddlCollegeName"
                                    Display="None" ErrorMessage="Please Select College Name" InitialValue="0" SetFocusOnError="true"
                                    ValidationGroup="Showlv"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-md-4">
                                <label>Degree</label>
                                <asp:DropDownList ID="ddlDegree" ToolTip="Degree Name" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                    Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="rfvDegreelv" runat="server" ControlToValidate="ddlDegree" Display="None"
                                    ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="Showlv"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-md-4">
                                <label>Branch</label>
                                <asp:DropDownList ID="ddlBranch" runat="server" ToolTip="Branch Name" AppendDataBoundItems="True" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                    Display="None" ErrorMessage="Please Select Branch" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="rfvBranchlv" runat="server" ControlToValidate="ddlBranch" Display="None"
                                    ErrorMessage="Please Select Branch" InitialValue="0" ValidationGroup="Showlv"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-md-4">
                                <label>Scheme</label>
                                <asp:DropDownList ID="ddlScheme" runat="server" ToolTip="Scheme Name" AppendDataBoundItems="True"
                                    AutoPostBack="true">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvProgram" runat="server" InitialValue="0" SetFocusOnError="true"
                                    ControlToValidate="ddlScheme" Display="None" ErrorMessage="Please Select Scheme"
                                    ValidationGroup="Show"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-md-4">
                                <label>Semester</label>
                                <asp:DropDownList ID="ddlSemester" runat="server" ToolTip="Semester Name" AppendDataBoundItems="True"
                                    ValidationGroup="Show">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                    Display="None" ErrorMessage="Please Select Semester" InitialValue="0" SetFocusOnError="true"
                                    ValidationGroup="Show"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="rfvSemesterlv" runat="server" ControlToValidate="ddlSemester" Display="None"
                                    ErrorMessage="Please Select Semester" InitialValue="0" SetFocusOnError="true" ValidationGroup="Showlv"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-md-4" id="trr" runat="server" visible="false">
                                <label>Core Subject</label>
                                <asp:DropDownList ID="ddlCore" runat="server" ToolTip="Core Subject" AppendDataBoundItems="True" ValidationGroup="Show">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvCourse" runat="server" ControlToValidate="ddlCore"
                                    Display="None" ErrorMessage="Please Select Core Subject" InitialValue="0" SetFocusOnError="true"
                                    ValidationGroup="Show"></asp:RequiredFieldValidator>
                            </div>

                        </div>
                        <div class="box-footer">
                            <p class="text-center">
                                <asp:Button ID="btnSubmit" ToolTip="Submit Details" runat="Server" Text="Submit" ValidationGroup="Show"
                                    CssClass="btn btn-outline-info" OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnCancel" runat="server" ToolTip="Cancel" Text="Cancel" CssClass="btn btn-outline-danger" OnClick="btnCancel_Click" />&nbsp;&nbsp;
                                        <asp:Button ID="btnStudlist" ToolTip="View Student List" runat="Server" Text="View Student list"
                                            CssClass="btn btn-outline-primary" OnClick="btnStudlist_Click" ValidationGroup="Showlv" />
                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="Show" Width="136px" />
                                <asp:ValidationSummary ID="valSummerylv" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Showlv" />
                            </p>
                            <div>
                                <asp:Label ID="lblStatus" runat="server" SkinID="Errorlbl" /></div>
                            <div class="col-md-12 table table-responsive">
                                <asp:ListView ID="lvstudDetails" runat="server">
                                    <LayoutTemplate>
                                        <div id="divlvPaidReceipts">
                                            <h4>Student Details</h4>
                                            <table class="table table-hover table-bordered">
                                                <thead>
                                                    <tr class="bg-light-blue">
                                                        <th>Sr.No.
                                                        </th>
                                                        <th>Student Name
                                                        </th>
                                                        <th>College Name
                                                        </th>
                                                        <th>Roll No.
                                                        </th>
                                                        <th>Registration No.
                                                        </th>
                                                        <th>Degree    
                                                        </th>
                                                        <th>Branch    
                                                        </th>
                                                        <th>Semester 
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                        </div>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <%# Container.DataItemIndex + 1 %>
                                            </td>

                                            <td>
                                                <%# Eval("STUDNAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("COLLEGE_NAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("REGNO")%>
                                            </td>
                                            <td>
                                                <%# Eval("ENROLLNO")%>
                                            </td>
                                            <td>
                                                <%# Eval("DEGREENAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("LONGNAME")%>
                                            </td>
                                            <td>
                                                <%# Eval("SEMESTERNAME")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div id="divMsg" runat="server">
                </div>
            </div>
        </div>
    </div>
</asp:Content>

