<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Section_Update.aspx.cs" Inherits="ACADEMIC_Section_Update" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updSection"
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
    <asp:UpdatePanel ID="updSection" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                            <%--  <h3 class="box-title">Module Registration </h3>--%>
                        </div>

                        <div class="box-body">
                            <div class="col-12" runat="server" visible="true" id="DivSerch">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>*</sup>
                                            <asp:Label ID="lblRStudentId" runat="server" Font-Bold="true">Student ID</asp:Label>
                         <%--                   <label>Student ID</label>--%>
                                        </div>
                                        <asp:TextBox ID="txtStudId" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtStudId"
                                            Display="None" ErrorMessage="Please Enter Date Of Presentation" InitialValue="" ValidationGroup="Submit" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup></sup>
                                            <asp:Label ID="Label1" runat="server" Visible="false"></asp:Label>
                                        </div>
                                        <asp:LinkButton ID="btnSerchStud" runat="server" CssClass="btn btn-outline-info" ValidationGroup="Submit" OnClick="btnSerchStud_Click">Search</asp:LinkButton>
                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Submit"
                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    </div>
                                </div>
                            </div>
                            <div runat="server" id="DivData" visible="false">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12 col-md-12 col-lg-6 mt-3">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b>  <asp:Label ID="lblStudentId" runat="server">Student ID</asp:Label></b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblStudeId" runat="server" Text="Faculty Name" Font-Bold="true"></asp:Label>
                                                    </a>
                                                </li>
                                                <li class="list-group-item"><b>  <asp:Label ID="lblDYCollege" runat="server">Faculty</asp:Label></b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblFaculty" runat="server" Text="PHD CS" Font-Bold="true"></asp:Label>
                                                    </a>
                                                </li>
                                                <li class="list-group-item"><b> <asp:Label ID="lblDYSemester" runat="server">Semester</asp:Label></b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblSemester" runat="server" Text="PHD CS" Font-Bold="true"></asp:Label>
                                                    </a>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="col-12 col-md-12 col-lg-6 mt-3">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item"><b><asp:Label ID="lblStudName" runat="server">Student Name</asp:Label></b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lvlStudeName" runat="server" Text="Faculty Name" Font-Bold="true"></asp:Label>
                                                    </a>
                                                </li>
                                                <li class="list-group-item"><b><asp:Label ID="lblDyProgram" runat="server">Program</asp:Label></b>
                                                    <a class="sub-label">
                                                        <asp:Label ID="lblProgram" runat="server" Text="PHD CS" Font-Bold="true"></asp:Label>
                                                    </a>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="form-group col-lg-3 col-md-6 col-12">

                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <asp:Label ID="lblSectionGroup" runat="server" Font-Bold="true"></asp:Label>
                                             <%--   <label>S</label> --%>
                                            </div>
                                              <asp:DropDownList ID="ddlSection" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-12 btn-footer mt-4" runat="server" visible="false" id="DivButton">
                                        <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn btn-outline-info" OnClick="btnSubmit_Click">Submit</asp:LinkButton>
                                        <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancel_Click">Cancel</asp:LinkButton>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

