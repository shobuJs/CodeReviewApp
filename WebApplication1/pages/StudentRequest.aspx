<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="StudentRequest.aspx.cs" Inherits="Academic_StudentRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="<%=Page.ResolveClientUrl("~/plugins/sweetalert2-11.7.5/sweetalert2.min.css") %>" rel="stylesheet" />
    <link href="<%=Page.ResolveClientUrl("~/plugins/iziToast-master/css/iziToast.min.css") %>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/sweetalert2-11.7.5/sweetalert2.min.js")%>"></script>
    <script src="<%=Page.ResolveClientUrl("~/plugins/iziToast-master/js/iziToast.min.js")%>"></script>

    <div class="row" id="divRequestforSubject">
        <div class="col-md-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                </div>

                <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                    <div class="form-group">
                        <div class="label-dynamic">
                            <sup>* </sup>
                            <%-- <label><span class="AcademicSession"></span></label>--%>
                            <asp:Label ID="lblPAAcadSession" runat="server" Font-Bold="true">Acadmic Session</asp:Label>
                        </div>
                        <asp:DropDownList ID="ddlAcdSession" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="0">
                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                        </asp:DropDownList>
                        <asp:HiddenField runat="server" Value="0" ID="hdnConfigIdPage" />
                    </div>
                </div>

                <asp:Panel ID="PanelCheck" runat="server">
                    <div class="box-body">
                        <div class="col-12">
                            <div class="form-group row">
                                <div class="col-xl-6 col-lg-6 col-sm-6 col-12 border-right">
                                    <div class="pb-4">
                                        <label class="float-left"><span class="AcademicSession">Academic Session</span></label>
                                        <label class="float-right text-primary" id="lblAcdSession"></label>
                                        <input type="hidden" id="hdnSession" />
                                    </div>
                                    <div class="pb-4">
                                        <label class="float-left"><span class="studentId">Student Id</span></label>
                                        <label class="float-right text-primary"><span id="spanRegno"></span></label>
                                    </div>
                                    <div class="pb-4">
                                        <label class="float-left"><span>Campus</span></label>
                                        <label class="float-right text-primary"><span id="spanCampus"></span></label>
                                    </div>
                                    <div class="pb-4">
                                        <label class="float-left"><span>College</span></label>
                                        <label class="float-right text-primary"><span id="spanCollege"></span></label>
                                    </div>
                                </div>
                                <div class="col-xl-6 col-lg-6 col-sm-6 col-12">
                                    <div class="pb-4">
                                        <label class="float-left"><span class="Program">Program</span></label>
                                        <label class="float-right text-primary"><span id="spanProgram"></span></label>
                                    </div>
                                    <div class="pb-4">
                                        <label class="float-left"><span>Curriculum</span></label>
                                        <label class="float-right text-primary"><span id="spanCurriculum"></span></label>
                                        <input type="hidden" id="hdnSchemeno" />
                                    </div>
                                    <div class="pb-4">
                                        <label class="float-left"><span>Semester</span></label>
                                        <label class="float-right text-primary"><span id="spanSemester"></span></label>
                                        <input type="hidden" id="hdnSemester" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-12" id="core" runat="server">
                            <div class="sub-heading">
                                <h5>Regular Subjects</h5>
                             </div>
                            <table class="table table-striped table-bordered nowrap" id="tblCourseList"></table>
                        </div>
                       

                        <div class="col-12 mt-5" id="restudy" runat="server">
                            <div class="sub-heading">
                                <h5>Re-Study Subjects</h5>
                            </div>
                            <table class="table table-striped table-bordered nowrap" id="tblRestudy"></table>
                        </div>
                        
                        <div class="col-12 mt-5" id="NotOffered" runat="server">
                            <div class="sub-heading">
                                <h5>Not Enlisted Subjects and Not Offered Subjects</h5>
                            </div>
                           <table class="table table-striped table-bordered nowrap" id="tblNotOffered"></table>
                        </div>
        
<%--                        <div class="col-12">
                            
                            <table class="table table-striped table-bordered nowrap" id="Table3"></table>
                            <table class="table table-striped table-bordered nowrap" id="Table4"></table>
                        </div>--%>
                       
                        <div class="text-center mt-2 mb-3">
                            <input type="button" class="btn btn-outline-info" tabindex="5" value="Submit" id="btnSubmit" />
                            <input type="button" class="btn btn-outline-danger" tabindex="6" value="Cancel" id="btnClear" />
                        </div>
                        <div class="col-12" id="CourseRequest" runat="server">
                            <table class="table table-striped table-bordered nowrap" id="tblRequestCourseList"></table>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>

    <script src="<%=Page.ResolveClientUrl("~/plugins/custom/js/common.js")%>"></script>
    <script src="<%=Page.ResolveClientUrl("~/plugins/custom/js/Academic/StudentRequest.js")%>"></script>
</asp:Content>
