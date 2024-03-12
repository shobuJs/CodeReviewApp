<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Communication_Trigger.aspx.cs" Inherits="Communication_Trigger" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <link href="<%=Page.ResolveClientUrl("~/plugins/sweetalert2-11.7.5/sweetalert2.min.css") %>"
        rel="stylesheet" />
    <link href="<%=Page.ResolveClientUrl("~/plugins/iziToast-master/css/iziToast.min.css") %>"
        rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/sweetalert2-11.7.5/sweetalert2.min.js")%>"></script>
    <script src="<%=Page.ResolveClientUrl("~/plugins/iziToast-master/js/iziToast.min.js")%>"></script>
    <style>
        .swal2-html-container {
            max-height: 60vh;
            text-align: left;
        }

        .custom-title-class {
            font-weight: 500;
            font-size: 19px !important;
        }

        .hidden {
            display: none;
        }

        .font {
            color: black;
            font-weight: 800;
            font-size: small;
        }
    </style>
    <!-- Page Main Layout Content -->

    <div class="row" id="DivfetchingForLabels">
        <div class="col-md-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">
                        <label id="lblDynamicPageTitle" runat="server">Communication Trigger</label>
                    </h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                <div class="form-group">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label><span class="clsEvent">Event</span></label>
                                    </div>
                                    <asp:DropDownList ID="ddlEvent" runat="server" CssClass="form-control" data-select2-enable="true"
                                        tabindex="1">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:HiddenField runat="server" Value="0" ID="hdnddlEvent" />
                                </div>
                            </div>

                            <div class="col-xl-3 col-lg-4 col-sm-6 col-12 mt-3 d-none">
                                <div class="form-group">
                                    <button class="btn btn-sm btn-outline-success" id="btnEye" tabindex="1" title="View Template">
                                        <i class="bi bi-eye"></i>
                                    </button>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                <div class="form-group">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label><span class="ClsStartDate">Start Date</span></label>
                                    </div>
                                    <input type="date" id="txtStartDate" class="form-control" tabindex="1" />
                                </div>
                            </div>

                            <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                <div class="form-group">
                                    <div class="label-dynamic">
                                        <sup></sup>
                                        <label><span class="ClsEndDate">End Date</span></label>
                                    </div>
                                    <input type="date" id="txtEndDate" class="form-control" tabindex="1" />
                                </div>
                            </div>

                            <div class="col-xl-3 col-lg-4 col-sm-6 col-12 d-none">
                                <div class="form-group">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label><span class="clsScheduleTime">Schedule Time</span></label>
                                    </div>
                        
                                    <input type="time" id="txtScheTime" class="form-control timepicker" tabindex="1" visible="false"
                                        title="Enter a valid time (HH:MM)" />
                                    <small class="form-text text-muted">Format: HH:MM</small>
                                </div>
                            </div>

                            <!-- Status Switch -->
                            <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                <div class="form-group">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label><span class="Status">Status</span></label>
                                    </div>
                                    <div class="switch form-inline">
                                        <input type="checkbox" id="StatusTrigger" name="switch" class="switch" checked />
                                        <label data-on="Active" data-off="Inactive" for="StatusTrigger"></label>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row mt-3">
                            <div class="col-xl-4 col-lg-4 col-sm-6 col-12">
                                <div class="border rounded p-3">
                                    <div class="form-group">
                                        <div class="d-flex justify-content-between align-items-center mb-2">
                                            <div class="sub-heading mb-0">To </div>
                                            <div class="form-check form-switch d-none">
                                                <input class="form-check-input" disabled="disabled" type="checkbox" role="switch"
                                                    id="fetchdynamically"
                                                    tabindex="1" checked>
                                                <label class="form-check-label" class="clsFetchDynamically" for="fetchdynamically">
                                                    Fetch Dynamically</label>
                                            </div>
                                        </div>
                                        <textarea class="form-control" tabindex="1" id="fetchTOEmail" spellcheck="true" oninput="validateEmailTO('fetchTOEmail', 'emailErrorTO')"></textarea>
                                        <small class="form-text text-muted font">Enter valid email addresses, separated by commas</small>
                                        <div id="emailErrorTO" style="color: red;"></div>
                                    </div>

                                </div>
                            </div>
                            <div class="col-xl-4 col-lg-4 col-sm-6 col-12">
                                <div class="border rounded p-3">
                                    <div class="form-group">
                                        <div class="sub-heading">CC </div>
                                        <textarea class="form-control" tabindex="1" id="txtAreaCC" spellcheck="true" oninput="validateEmailCC('txtAreaCC', 'emailErrorCC')"></textarea>
                                        <small class="form-text text-muted font">Enter valid email addresses, separated by commas</small>
                                        <div id="emailErrorCC" style="color: red;"></div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xl-4 col-lg-4 col-sm-6 col-12">
                                <div class="border rounded p-3">
                                    <div class="form-group">
                                        <div class="sub-heading">BCC </div>
                                        <textarea class="form-control" tabindex="1" id="txtAreaBCC" spellcheck="true" oninput="validateEmailBCC('txtAreaBCC', 'emailErrorBCC')"></textarea>
                                        <small class="form-text text-muted font">Enter valid email addresses, separated by commas</small>
                                        <div id="emailErrorBCC" style="color: red;"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- Buttons -->
                        <div class="text-center mt-3 mb-3">
       
                            <input type="button" value="Submit" id="btnSubmitEmail" class="btn btn-outline-info"
                                tabindex="1" />
                            <input type="button" value="Cancel" id="btnCancelEmail" class="btn btn-outline-danger"
                                tabindex="1" />
                        </div>
                    </div>

                    <div class="col-12">
                        <table class="table table-striped table-bordered nowrap" id="BindDynamicTable">
                        </table>
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
        </div>
    </div>
    
    <script src="<%=Page.ResolveClientUrl("~/plugins/custom/js/common.js")%>"></script>
    <script src="<%=Page.ResolveClientUrl("~/plugins/custom/js/Academic/Communication_Trigger.js")%>"></script>
</asp:Content>
