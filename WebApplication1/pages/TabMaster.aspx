<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TabMaster.aspx.cs" Inherits="Academic_TabMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:content id="Content1" contentplaceholderid="ContentPlaceHolder1" runat="Server">
    <link href="<%=Page.ResolveClientUrl("~/plugins/sweetalert2-11.7.5/sweetalert2.min.css") %>" rel="stylesheet" />
    <link href="<%=Page.ResolveClientUrl("~/plugins/iziToast-master/css/iziToast.min.css") %>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/sweetalert2-11.7.5/sweetalert2.min.js")%>"></script>
    <script src="<%=Page.ResolveClientUrl("~/plugins/iziToast-master/js/iziToast.min.js")%>"></script>
    <!-- Page Main Layout Content -->
     <div class="row" id="DivfetchingForLabels">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title"><label id="lblDynamicPageTitle" runat="server">Student Tab Configuration</label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                   <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                        <div class="form-group">
                            <div class="label-dynamic">
                                <sup>* </sup>
                                <label><span class="clsTabName">Tab Name</span></label>
                            </div>
                            <asp:DropDownList ID="ddlTabName" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="0">
                               <asp:ListItem Value="0">Please Select</asp:ListItem>
                            </asp:DropDownList>
                            <asp:HiddenField runat="server" Value="0" ID="hdnTabNos"/>
                        </div>
                    </div>
                    
                    <div class="col-xl-2 col-lg-2 col-sm-6 col-12">
                        <div class="form-group">
                            <div class="label-dynamic">
                                <sup>* </sup>
                                <label><span class="lblStatus">Status</span></label>
                            </div>
                            <div class="switch form-inline">
                                <input type="checkbox" id="Status" name="switch" class="switch" checked />
                                 <label data-on="Active" data-off="Inactive" for="Status"></label>
                            </div>
                        </div>
                    </div>

                </div>

                <div class="text-center mt-2 mb-3">
                    <input type="button" class="btn btn-sm btn-outline-primary" tabindex="0" value="Submit" id="btnSubmit" />
                    <input type="button" class="btn btn-sm btn-outline-danger" tabindex="0" value="Cancel" id="btnClear" />
                   
                </div>
            </div>

            <!-- Data Table -->
            <div class="col-12">
                <table class="table table-striped table-bordered nowrap" id="BindTab"></table>
            </div>
                            </div>
                        </div>

        </div>
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
                            <%--<iframe id="video-plays" class="video-plays" src="" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" allowfullscreen></iframe>--%>
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
    <script src="<%=Page.ResolveClientUrl("~/plugins/custom/js/Academic/TabMaster.js")%>"></script>


</asp:content>


