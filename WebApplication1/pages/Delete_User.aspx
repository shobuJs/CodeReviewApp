<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Delete_User.aspx.cs" Inherits="ACADEMIC_Delete_User" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <div>

        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updbranchchange"
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

    <style>
        .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>

    <asp:UpdatePanel ID="updbranchchange" runat="server">
        <ContentTemplate>
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                     <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                   <%-- <h3 class="box-title">Delete User</h3>--%>
                </div>

                   <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <div class="input-group">
                                            <asp:TextBox ID="txtEnrollmentSearch" runat="server" ValidationGroup="submit" ToolTip="Enter Exam Roll No." CssClass="form-control"  placeholder="Please Enter Application No." TabIndex="1"></asp:TextBox>
                                            <span class="input-group-btn"><i class="material-icons left"></i>
                                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-outline-primary ml-1" OnClick="btnSearch_Click" ValidationGroup="submit"/>
                                            </span>
                                            <asp:RequiredFieldValidator ID="rfvSearch" runat="server" ControlToValidate="txtEnrollmentSearch" Display="None" ErrorMessage="Please Enter Application No." ValidationGroup="submit" TabIndex="2"></asp:RequiredFieldValidator>
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True" Width="20%" ShowSummary="False" ValidationGroup="submit" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12">
                                <div id="divStudInfo" runat="server" visible="false" >
                                    <div class="sub-heading col-12">
                                        <h5>STUDENT INFORMATION</h5>
                                    </div>
                                    <div id="divStudentInfo" style="display: block;">
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="col-lg-6 col-md-6 col-12">
                                                    <ul class="list-group list-group-unbordered ipad-view">
                                                        <li class="list-group-item"><b>Application No. :</b>
                                                            <a class="sub-label"><asp:Label ID="lblRegNo" runat="server" Font-Bold="True"></asp:Label></a>
                                                        </li>
                                                        <li class="list-group-item"><b>Student Name :</b>
                                                            <a class="sub-label"><asp:Label ID="lblName" runat="server" Font-Bold="True"></asp:Label></a>
                                                        </li>
                                                        <li class="list-group-item"><b>Intake :</b>
                                                            <a class="sub-label"><asp:Label ID="lblbatch" runat="server" Font-Bold="True"></asp:Label></a>
                                                        </li>
                                                       
                                                    </ul>
                                                </div>
                                                <div class="col-lg-6 col-md-6 col-12">
                                                    <ul class="list-group list-group-unbordered">
                                                        <li class="list-group-item"><b>Program :</b>
                                                            <a class="sub-label"><asp:Label ID="lbldegree" runat="server" Font-Bold="True"></asp:Label></a>
                                                        </li>
                                                        <li class="list-group-item"><b>Study Level :</b>
                                                            <a class="sub-label"><asp:Label ID="lblStudyLevel" runat="server" Font-Bold="True"></asp:Label></a>
                                                        </li>
                                                        <li class="list-group-item"><b>Email:</b>
                                                            <a class="sub-label"><asp:Label ID="lblEmail" runat="server" Font-Bold="True"></asp:Label></a>
                                                        </li>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>
                                                        
                                        <div class="col-12 btn-footer mt-3">
                                                <%--<asp:Button ID="btnTranscriptNew" runat="server" Text="Transcript New" ValidationGroup="submit" OnClick="btnTranscriptNew_Click" Visible="false" CssClass="btn btn-outline-primary" />--%>
                                            <asp:Button ID="btnDelete" runat="server" Text="Delete" ValidationGroup="submit" OnClick="btnDelete_Click" CssClass="btn btn-outline-primary" TabIndex="3"/> 
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-outline-danger" TabIndex="4"/>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
            </div>
        </div>
    </div>
</ContentTemplate>
       
</asp:UpdatePanel>
</asp:Content>

