<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="ReportTypeProcedure.aspx.cs" Inherits="Academic_ReportTypeProcedure" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="<%=Page.ResolveClientUrl("~/plugins/sweetalert2-11.7.5/sweetalert2.min.css") %>"
        rel="stylesheet" />
    <link href="<%=Page.ResolveClientUrl("~/plugins/iziToast-master/css/iziToast.min.css") %>"
        rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/sweetalert2-11.7.5/sweetalert2.min.js")%>"></script>
    <script src="<%=Page.ResolveClientUrl("~/plugins/iziToast-master/js/iziToast.min.js")%>"></script>


    <!-- Page Main Layout Content -->
    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">
                        <label id="lblDynamicPageTitle" runat="server">Report Master</label></h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <!-- Page Main Inputs Content -->
                        <div class="row">
                            <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                <div class="form-group">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label><span class="PageName">Page Name</span></label>
                                    </div>
                                    <asp:DropDownList ID="ddlPageName" runat="server" CssClass="form-control" data-select2-enable="true"
                                        TabIndex="0">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:HiddenField runat="server" Value="0" ID="hdnPageNos" />
                                </div>
                            </div>
                            <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                <div class="form-group">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label><span class="ReportName">Report Name</span></label>
                                    </div>
                                    <input type="text" class="form-control search" tabindex="0" id="txtReport" />
                                    <asp:HiddenField runat="server" Value="0" ID="hdnReport" />
                                </div>
                            </div>
                            <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                <div class="form-group">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label><span class="ProcedureName">Procedures</span></label>
                                    </div>
                                    <input type="text" class="form-control" tabindex="0" id="txtProcedure" />
                                </div>
                            </div>
                            <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                <div class="form-group">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label><span class="clSequence">Sequence Wise</span></label>
                                    </div>
                                    <input type="text" class="form-control" tabindex="0" id="txtSequence" />
                                    <%--<asp:HiddenField runat="server" Value="0" ID="hdnTxtSequence"/>--%>
                                </div>
                            </div>
                            <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                <div class="form-group">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Mandatory Field</label>
                                    </div>
                                    <asp:CheckBox ID="chkSession" runat="server" TabIndex="0" CssClass="ml-2" />
                                    Session
                            <%--<asp:CheckBox ID="chkCampus" runat="server" TabIndex="0" CssClass="ml-2" />
                                    Campus--%>
                                    <asp:CheckBox ID="chkCollege" runat="server" TabIndex="0" CssClass="ml-2" />
                                    College
                            <asp:CheckBox ID="chkCourse" runat="server" TabIndex="0" CssClass="ml-2" />
                                    Program
                            <asp:CheckBox ID="chkSemester" runat="server" TabIndex="0" CssClass="ml-2" />
                                    Semester
                            <asp:CheckBox ID="chkSubjectType" runat="server" TabIndex="0" CssClass="ml-2" />
                                    Subject Type 
                            <asp:CheckBox ID="chkSubject" runat="server" TabIndex="0" CssClass="ml-2" />
                                    Subject
                                </div>
                            </div>
                            <div class="col-xl-2 col-lg-2 col-sm-6 col-12">
                                <div class="form-group">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label><span class="Status">Status</span></label>
                                    </div>
                                    <div class="switch form-inline">
                                        <input type="checkbox" id="Status" name="switch" class="switch" checked />
                                        <label data-on="Active" data-off="Inactive" for="Status"></label>
                                    </div>
                                </div>
                            </div>

                        </div>

                        <div class="text-center mt-2 mb-3">
                            <input type="button" class="btn btn-sm btn-outline-primary" tabindex="6" value="Submit"
                                id="btnSubmit" />
                            <input type="button" class="btn btn-sm btn-outline-danger" tabindex="7" value="Cancel"
                                id="btnClear" />
                        </div>
                    </div>

                    <!-- Data Table -->
                    <div class="col-12">
                        <table class="table table-striped table-bordered nowrap" id="BindReportTypeProcedure">
                        </table>
                    </div>

                </div>
            </div>
        </div>
    </div>


    <script>
        var userno = '<%=Session["userno"].ToString()%>';
    </script>

    <script src="<%=Page.ResolveClientUrl("~/plugins/custom/js/common.js")%>"></script>
    <script src="<%=Page.ResolveClientUrl("~/plugins/custom/js/Academic/ReportTypeProcedure.js")%>"></script>


</asp:Content>



