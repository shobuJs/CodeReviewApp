<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="CertificateApply.aspx.cs" Inherits="CertificateApply" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="https://bankofceylon.gateway.mastercard.com/checkout/version/61/checkout.js" data-error="errorCallback" data-cancel="cancelCallback"></script>
    <script type="text/javascript">
        function errorCallback(error) {
            console.log(JSON.stringify(error));
        }
        function cancelCallback() {
            //console.log('Payment cancelled');
        }
        cancelCallback = "https://sims.sliit.lk/OnlineResponse.aspx"
        function completeCallback(resultIndicator, sessionVersion) {
            //handle payment completion
            completeCallback = "http://localhost:59567/PresentationLayer/OnlineResponse.aspx"
        }
        //completeCallback = "http://localhost:55158/PresentationLayer/OnlineResponse.aspx"
        Checkout.configure({
            session: {
                id: '<%= Session["ERPConvocationPaymentSession"] %>'
            },
            interaction: {
                merchant: {
                    name: 'SLIIT',
                    address: {
                        line1: 'TEST',
                        line2: 'DONE'
                    }
                }
            }
        });
    </script>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updApply"
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
    <asp:UpdatePanel ID="updApply" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title"><span>Apply For Document </span></h3>
                        </div>

                        <div class="box-body" runat="server" id="DivData" visible="false">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Student Information</h5>
                                        </div>
                                    </div>
                                    <div class="col-lg-6 col-md-12 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Student Name :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblStudentName" runat="server" Text="Manoj Rahul Patil" Font-Bold="true"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Student ID :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblStdID" runat="server" Text="SLR00004" Font-Bold="true"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Date Of Birth :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblDOB" runat="server" Text="31/08/2000" Font-Bold="true"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item"><b>Gender :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblGender" runat="server" Text="Male" Font-Bold="true"></asp:Label></a>
                                            </li>
                                            <li class="list-group-item"><b>Mobile No. :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblMobileNo" runat="server" Text="0123456789" Font-Bold="true"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Email ID :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblEmailID" runat="server" Text="abc@abcd.abc" Font-Bold="true"></asp:Label>
                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-lg-6 col-md-12 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Program :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblProgram" runat="server" Text="Bachelor of Science Honours - Information Technology - Cyber Security" Font-Bold="true"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item"><b>Semester :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblSem" runat="server" Text="Y2S1" Font-Bold="true"></asp:Label>
                                                </a>
                                            </li>
                                            <li class="list-group-item text-center">
                                                <asp:Image ID="imgPhoto" runat="server" class="img-circle" Style="width: 120px; height: 130px;" alt="User Image" />
                                                <%--   <asp:Image ID="imgphoto" runat="server" ImageUrl="~/IMAGES/nophoto.jpg" Style="width: 120px; height: 130px;" />--%>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 mt-3">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Document</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDocument" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlDocument_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlDocument"
                                            Display="None" ErrorMessage="Please Select Document" InitialValue="0" ValidationGroup="Submit" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer" runat="server" visible="false" id="DivButton">
                                <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn btn-outline-info" OnClick="btnSubmit_Click">Submit</asp:LinkButton>
                                <asp:LinkButton ID="btnSubmitPay" runat="server" CssClass="btn btn-outline-info" OnClick="btnSubmitPay_Click">Submit & Pay</asp:LinkButton>
                                <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-outline-danger">Cancel</asp:LinkButton>
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Submit"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>
                            <div class="box-body">
                                <div class="col-12">
                                    <asp:Panel ID="Panel2" runat="server">
                                        <asp:ListView ID="lvDocument" runat="server" Visible="true">
                                            <LayoutTemplate>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                    <thead class="bg-light-blue">
                                                        <tr>
                                                            <th>REQUEST_ID</th>
                                                            <th>Document Name</th>
                                                            <th>Amount</th>
                                                            <th>Status</th>
                                                            <th>Download</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%#Eval("REQUEST_ID") %></td>
                                                    <td><%#Eval("DOC_NAME") %></td>
                                                    <td><%#Eval("PAID_FEES") %></td>
                                                    <td><%#Eval("AP_STATUS") %></td>
                                                    <td>
                                                        <asp:LinkButton ID="btnDownload" OnClick="btnDownload_Click" runat="server" Enabled='<%#Eval("FILE_NAMES").ToString() == "" ? false : true %>'  CommandArgument='<%#Eval("IDNO") %>' CommandName='<%#Eval("FILE_NAMES") %>'> <i class="fa fa-download" aria-hidden="true" style="color: #28a745; font-size: 20px;"></i></asp:LinkButton></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>

                                    </asp:Panel>

                                </div>
                                <%--           <div class="col-12 mt-3">
                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                        <thead class="bg-light-blue">
                                            <tr>
                                                <th>Request ID</th>
                                                <th>Document Name</th>
                                                <th>Paid Amount</th>
                                                <th>Status</th>
                                                <th>Download</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td>RQID01234</td>
                                                <td>Document Name</td>
                                                <td>2000</td>
                                                <td>Approved</td>
                                                <td><i class="fa fa-download" aria-hidden="true" style="color: #28a745; font-size: 20px;"></i></td>
                                            </tr>
                                            <tr>
                                                <td>RQID98765</td>
                                                <td>Document Name</td>
                                                <td>1000</td>
                                                <td>Pending</td>
                                                <td>-</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>--%>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal fade" id="myModalPay">
                    <div class="modal-dialog">
                        <div class="modal-content">

                            <!-- Modal Header -->
                            <div class="modal-header">
                                <h4 class="modal-title">Online Payment</h4>
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                            </div>

                            <!-- Modal body -->
                            <div class="modal-body">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="form-group col-lg-12 col-md-12 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Order ID </label>
                                            </div>
                                            <asp:TextBox ID="txtOrderID" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-12 col-md-12 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Amount To Be Paid </label>
                                            </div>
                                            <asp:TextBox ID="txtAmountPaid" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-12 col-md-12 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Service Charge </label>
                                            </div>
                                            <asp:TextBox ID="txtServiceCharge" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-lg-12 col-md-12 col-12">
                                            <div class="label-dynamic">
                                                <sup></sup>
                                                <label>Total To Be Paid </label>
                                            </div>
                                            <asp:TextBox ID="txtTotal" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 btn-footer mt-3">
                                    <input type="button" value="Pay with Lightbox" onclick="Checkout.showLightbox();" class="btn btn-outline-info d-none" />
                                    <input type="button" value="Pay" onclick="Checkout.showPaymentPage();" class="btn btn-outline-info" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="myModal22" class="modal fade" role="dialog">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content" style="margin-top: -25px">
                            <div class="modal-body">
                                <div class="modal-header">
                                    <h4 class="modal-title" style="font: bold">Document</h4>
                                    <button type="button" class="close" data-dismiss="modal" style="margin-top: -18px">x</button>
                                </div>
                                <asp:Image ID="ImageViewer" runat="server" Width="100%" Height="500px" />
                                <asp:Literal ID="ltEmbed" runat="server" />
                                <div id="imageViewerContainer" runat="server"></div>
                                <asp:HiddenField ID="hdfImagePath" runat="server" />

                                <div class="modal-footer" style="height: 0px">
                                    <button type="button" style="margin-top: -10px" class="btn btn-default" data-dismiss="modal">Close</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

