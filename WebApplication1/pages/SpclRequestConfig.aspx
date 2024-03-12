<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="SpclRequestConfig.aspx.cs" Inherits="Academic_SpclRequestConfig" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="<%=Page.ResolveClientUrl("~/plugins/sweetalert2-11.7.5/sweetalert2.min.css") %>" rel="stylesheet" />
    <link href="<%=Page.ResolveClientUrl("~/plugins/iziToast-master/css/iziToast.min.css") %>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/sweetalert2-11.7.5/sweetalert2.min.js")%>"></script>
    <script src="<%=Page.ResolveClientUrl("~/plugins/iziToast-master/js/iziToast.min.js")%>"></script>
    <!-- Page Main Layout Content -->
    <div class="row" id="DivfetchingForLabels">
        <div class="col-md-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">
                        <label id="lblDynamicPageTitle" runat="server">Special Request Configuration</label></h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                <div class="form-group">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                       <%-- <label><span class="AcademicSession"></span></label>--%>
                                        <asp:Label ID="lblPAAcadSession" runat="server" Font-Bold="true"></asp:Label>
                                    </div>
                                    <asp:DropDownList ID="ddlAcdSession" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="0">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:HiddenField runat="server" Value="0" ID="hdnConfigIdPage" />
                                </div>
                            </div>
                            <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                <div class="form-group">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <%--<label><span class="Semester"></span></label>--%>
                                        <asp:Label ID="lblDYADSession" runat="server" Font-Bold="true"></asp:Label>
                                    </div>
                                    <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="true" data-select2-enable="true" CssClass="form-control" TabIndex="0">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                            </div>
                            <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                <div class="form-group">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <%--<label><span class="MinStudents"></span></label>--%>
                                        <asp:Label ID="MinStudents" runat="server" Font-Bold="true">Min. No. of Students</asp:Label>
                                        
                                    </div>
                                    <input type="text" id="txtMinStudents" maxlength="3" tabindex="0" class="form-control numeric" />
                                </div>
                            </div>

                            <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                <div class="form-group">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <%--<label><span class="StartDate"></span></label>--%>
                                        <asp:Label ID="lblDYStartDate" runat="server" Font-Bold="true"></asp:Label>
                                    </div>
                                    <input type="date" id="txtStartDate" tabindex="0" class="form-control" />
                                </div>
                            </div>

                            <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                <div class="form-group">
                                    <div class="label-dynamic">
                                        <sup>*</sup>
                                        <%--<label><span class="EndDate"></span></label>--%>
                                        <asp:Label ID="lblDYEndDate" runat="server" Font-Bold="true"></asp:Label>
                                    </div>
                                    <input type="date" id="txtEndDate" tabindex="0" class="form-control" />
                                </div>
                            </div>
                        </div>

                        <div class="text-center mt-2 mb-3">
                            <input type="button" class="btn btn-outline-info" tabindex="0" value="Submit" id="btnSubmit" />
                            <input type="button" class="btn btn-outline-danger" tabindex="0" value="Cancel" id="btnClear" />
                        </div>
                    </div>

                    <!-- Data Table -->
                    <div class="col-12">
                        <table class="table table-striped table-bordered nowrap" id="BindSpecialRequestConfig"></table>
                    </div>
                </div>
                <!--Add by sakshi mohadikar -->
                <!-- Tab panes END -->
                <!-- The Video Modal -->
                <div class="modal fade" id="VideoModal">
                    <div class="modal-dialog">
                        <div class="modal-content">

                            <div class="modal-header">
                                <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                            </div>

                            <div class="modal-body text-center">
                                <div class="table-responsive" id="Vframe">
                                    <%--<video autoplay controls id="myVideo" class="w-100 video-plays">
                            <source src="assets/video/BigZpoon Explainer Video.mp4" type="video/mp4">
                        </video>--%>
                                    <%--<iframe id="video-plays" class="video-plays" src="" title="YouTube video player" frameborder="0" allow=" celerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" allowfullscreen></iframe>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script>
        var userno = '<%=Session["userno"].ToString()%>';
    </script>

    <script src="<%=Page.ResolveClientUrl("~/plugins/custom/js/common.js")%>"></script>
    <script src="<%=Page.ResolveClientUrl("~/plugins/custom/js/Academic/SpecialRequestConfig.js")%>"></script>
</asp:Content>



