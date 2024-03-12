<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="InternshipApproval.aspx.cs" Inherits="InternshipApproval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updinternship"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:UpdatePanel ID="updinternship" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">INTERNSHIP APPROVAL</h3>
                            <div style="color: Red; font-weight: bold" class="pull-right">
                                Note : * Marked fields are mandatory
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="col-md-12">
                                <fieldset>

                                    <div class="form-group col-md-4">
                                        <span style="color: Red">*</span>
                                        <label>College/School Name :</label>
                                        <asp:DropDownList ID="ddlCollege" runat="server" TabIndex="7" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" CssClass="form-control">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCollege" Display="None" InitialValue="0" ErrorMessage="Please Select College/School Name" ValidationGroup="Show">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-md-4">
                                        <span style="color: Red">*</span>
                                        <label>Degree :</label>
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                            ValidationGroup="report" AutoPostBack="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvAdmBatch" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Degree" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-md-4">
                                        <span style="color: Red">*</span>
                                        <label>Branch :</label>
                                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                            ValidationGroup="report" AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Branch" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-md-4">
                                        <span style="color: Red">*</span>
                                        <label>Semester/Year :</label>
                                        <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                                            ValidationGroup="report">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSemester" runat="server" ControlToValidate="ddlSemester"
                                            Display="None" InitialValue="0" ErrorMessage="Please Select Semester" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    </div>
                                </fieldset>
                            </div>
                        </div>

                        <div class="box-footer">
                            <p class="text-center">
                                <div style="text-align: center;">
                                    <asp:Button ID="btnShow" runat="server" OnClick="btnShow_Click" Text="Show" ValidationGroup="Show" CssClass="btn btn-outline-primary" />
                                    <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel" CssClass="btn btn-outline-danger" CausesValidation="False" />
                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="Show" />
                                </div>
                            </p>
                        </div>

                        <div class="container-fluid">
                            <asp:ListView ID="lvlInternship" runat="server">
                                <LayoutTemplate>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <h3>
                                                <label class="label label-default">Student Internship Details</label></h3>
                                        </div>
                                    </div>
                                    <div class="table-responsive">
                                        <table id="tblDD_Details" class="table table-hover table-bordered datatable">
                                            <tr class="bg-light-blue">
                                                <th></th>
                                                <th style="text-align: center">Registration No.</th>
                                                <th style="text-align: center">Industry Name</th>
                                                <th style="text-align: center">Internship Details</th>
                                                <th style="text-align: center">From Date</th>
                                                <th style="text-align: center">To Date</th>
                                                <th style="text-align: center">Duration</th>
                                                <th style="text-align: center">Stipend</th>
                                            </tr>
                                            <tr id="itemPlaceholder" runat="server" />
                                        </table>
                                    </div>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="cbRow" runat="server" ToolTip='<%# Eval("IDNO")%>' />
                                            <asp:HiddenField ID="hdfTempIdNo" runat="server" Value='<%# Eval("IDNO")%>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblitno" runat="server" Text='<%# Eval("REGNO") %>' ToolTip='<%# Eval("ITNO")%>' /></td>

                                        <td style="text-align: center"><%# Eval("INDUSTRY_NAME")%> </td>

                                        <td style="text-align: center"><%# Eval("INTERNSHIP_DETAILS") %> </td>

                                        <td style="text-align: center"><%# Eval("FROM_DATE")%> </td>

                                        <td style="text-align: center"><%# Eval("TO_DATE")%> </td>

                                        <td style="text-align: center"><%# Eval("DURATION")%> </td>

                                        <td style="text-align: center"><%# Eval("STIPEND")%> </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </div>

                        <div class="box-footer">
                            <p class="text-center">
                                <div style="text-align: center;">
                                    <asp:Button ID="btnApprove" runat="server" OnClick="btnApprove_Click" Text="Approve" ValidationGroup="Show" CssClass="btn btn-outline-primary" Visible="false" />
                                </div>
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


