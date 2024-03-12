<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ReStudyApproval.aspx.cs" Inherits="Academic_ReStudyApproval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="<%=Page.ResolveClientUrl("~/plugins/sweetalert2-11.7.5/sweetalert2.min.css") %>" rel="stylesheet" />
    <link href="<%=Page.ResolveClientUrl("~/plugins/iziToast-master/css/iziToast.min.css") %>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/sweetalert2-11.7.5/sweetalert2.min.js")%>"></script>
    <script src="<%=Page.ResolveClientUrl("~/plugins/iziToast-master/js/iziToast.min.js")%>"></script>

    <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
    <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>
    <!-- Page Main Layout Content -->
    <div class="row" id="DivfetchingForLabels">
        <div class="col-md-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <!-- Page Main Inputs Content -->
                        <div class="row">
                            <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                <div class="form-group">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <%--<label><span class="AcademicSession"></span></label>--%>
                                        <asp:Label ID="lblPAAcadSession" runat="server" Font-Bold="true"></asp:Label>
                                    </div>
                                    <asp:DropDownList ID="ddlAcdSession" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="0">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                <div class="form-group">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <%-- <label><span class="College"></span></label>--%>
                                        <asp:Label ID="lblfaculty" runat="server" Font-Bold="true"></asp:Label>
                                    </div>
                                    <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="true" data-select2-enable="true" CssClass="form-control" TabIndex="0" AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                            </div>

                            <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                <div class="form-group">
                                    <div class="label-dynamic">
                                        <%-- <sup>* </sup>--%>
                                        <%-- <label><span class="Program"></span></label>--%>
                                        <asp:Label ID="lblDyProgram" runat="server" Font-Bold="true"></asp:Label>
                                    </div>
                                    <asp:ListBox ID="lstProgram" runat="server" AppendDataBoundItems="true" TabIndex="0"
                                        CssClass="form-control multi-select-demo" SelectionMode="multiple" AutoPostBack="true" OnSelectedIndexChanged="lstProgram_SelectedIndexChanged">
                                        <%--         <asp:ListItem Value="0">select all</asp:ListItem>--%>
                                    </asp:ListBox>

                                </div>
                            </div>


                            <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                <div class="form-group">
                                    <div class="label-dynamic">
                                        <%--  <sup>* </sup>--%>
                                        <%-- <label><span class="Curriculum"></span></label>--%>
                                        <asp:Label ID="lblDYScheme" runat="server" Font-Bold="true"></asp:Label>
                                    </div>
                                    <asp:ListBox ID="lstCurriculum" runat="server" AppendDataBoundItems="true" TabIndex="0"
                                        CssClass="form-control multi-select-demo" SelectionMode="multiple"></asp:ListBox>
                                </div>
                            </div>

                            <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                <div class="form-group">
                                    <div class="label-dynamic">
                                        <%-- <sup>* </sup>--%>
                                        <label><span class="Semester"></span></label>
                                        <asp:Label ID="lblAcademicSemester" runat="server" Font-Bold="true"></asp:Label>
                                    </div>

                                    <asp:ListBox ID="lstSemester" runat="server" AppendDataBoundItems="true" TabIndex="0"
                                        CssClass="form-control multi-select-demo" SelectionMode="multiple"></asp:ListBox>
                                </div>
                            </div>
                        </div>

                        <div class="text-center mt-2 mb-3">
                            <input type="button" class="btn btn-outline-info" tabindex="0" value="Show" id="btnShow" />
                            <input type="button" class="btn btn-outline-info" tabindex="0" value="Approval" id="btnApprove" />
                            <input type="button" class="btn btn-outline-danger" tabindex="0" value="Reject" id="btnReject" />
                            <input type="button" class="btn btn-outline-danger" tabindex="0" value="Cancel" id="btnClear" />
                        </div>
                    </div>

                    <%--**************Popup Patch 17-11-2023***********--%>
                    <!-- The Modal -->
                    <!-- The Modal -->
                    <div class="modal fade" id="myModal_terms">
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">

                                <!-- Modal Header -->
                                <div class="modal-header">
                                    <h4 class="modal-title">Student List</h4>
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                </div>

                                <!-- Modal body -->
                                <div class="modal-body">
                                    <div class="col-12" id="div1">
                                        <table class="table table-striped table-bordered nowrap" id="tblStudentList"></table>
                                    </div>
                                </div>

                                <!-- Modal footer -->
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                                </div>

                            </div>
                        </div>
                    </div>
                    <%--****************End*************************--%>


                    <!-- Data Table -->
                    <div class="col-12" id="divCourseList">
                        <table class="table table-striped table-bordered nowrap" id="tblCoursesList"></table>
                    </div>

                </div>
            </div>
        </div>
    </div>
    <script>
        var userno = '<%=Session["userno"].ToString()%>';
    </script>


    <script src="<%=Page.ResolveClientUrl("~/plugins/custom/js/common.js")%>"></script>
    <script src="<%=Page.ResolveClientUrl("~/plugins/custom/js/Academic/ReStudyApproval.js")%>"></script>

</asp:Content>
