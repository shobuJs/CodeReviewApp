<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="TeacherNotAllot.aspx.cs" Inherits="Academic_REPORTS_MarksEntryNotDone" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updTeacher"
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

    <asp:UpdatePanel ID="updTeacher" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">TEACHER NOT ALLOTTED</h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>College/School Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCollege" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" AutoPostBack="true"
                                            AppendDataBoundItems="true" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCollege"
                                            Display="None" ErrorMessage="Please Select College/School Name" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="teacherreport"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Session</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSession" runat="server" CssClass="form-control"
                                            AppendDataBoundItems="true" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="teacherreport"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" CssClass="form-control"
                                            AppendDataBoundItems="true"
                                            OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged"
                                            AutoPostBack="True">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="teacherreport"></asp:RequiredFieldValidator>

                                        <asp:RequiredFieldValidator ID="rfvDegree2" runat="server" ControlToValidate="ddlDegree"
                                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="report"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Branch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" AutoPostBack="true"
                                            AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                            Display="None" ErrorMessage="Please Select Branch" InitialValue="0" SetFocusOnError="True"
                                            ValidationGroup="teacherreport"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Semester/Year </label>
                                        </div>
                                        <asp:DropDownList ID="ddlSemester" runat="server" CssClass="form-control"
                                            AppendDataBoundItems="true" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display:none;">
                                        <div class="label-dynamic">
                                            <label>Test</label>
                                        </div>
                                        <asp:DropDownList ID="ddlTest" runat="server" CssClass="form-control"
                                            AppendDataBoundItems="true" TabIndex="2">
                                            <asp:ListItem Value="-1">Please Select</asp:ListItem>
                                            <asp:ListItem Value="0">Test I & II</asp:ListItem>
                                            <asp:ListItem Value="1">Test I</asp:ListItem>
                                            <asp:ListItem Value="2">Test II</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvTest" runat="server" ControlToValidate="ddlTest"
                                            Display="None" ErrorMessage="Please Select Test" InitialValue="-1" SetFocusOnError="True"
                                            ValidationGroup="report"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnReport1" runat="server" Text="*Teacher Not Allotted" ValidationGroup="teacherreport"
                                    OnClick="btnReport1_Click" CssClass="btn btn-outline-primary" />
                                <asp:Button ID="btnReport2" runat="server" Text="**Marks Entry Not Done"
                                    ValidationGroup="report" OnClick="btnReport2_Click" Visible="false" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                    OnClick="btnCancel_Click" CssClass="btn btn-outline-danger" />
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="report" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="teacherreport" />
                            </div>

                            <div class="form-group col-lg-5 col-md-8 col-12" style="display:none;">
                                <div class=" note-div">
                                    <h5 class="heading">Note</h5>
                                    <p><i class="fa fa-star" aria-hidden="true"></i><span>Teacher Not Allotted - <span style="color: green;font-weight:bold">Session->Degree</span> </span> </p>
                                    <p><i class="fa fa-star" aria-hidden="true"></i><span>Mark Entry Not Done - <span style="color: green;font-weight:bold">Session->Degree->Term</span></span></p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div id="divMsg" runat="server">
    </div>
</asp:Content>
