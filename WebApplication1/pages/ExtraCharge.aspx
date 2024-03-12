<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ExtraCharge.aspx.cs" Inherits="ACADEMIC_ExtraCharge" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
            completeCallback = "https://sims.sliit.lk/OnlineResponse.aspx"
        }
        //completeCallback = "http://localhost:55158/PresentationLayer/OnlineResponse.aspx"
        Checkout.configure({
            session: {
                id: '<%= Session["ERPPaymentSession"] %>'
            },
            interaction: {
                merchant: {
                    name: 'SLIIT',
                    address: {
                        line1: 'Malabe',
                        line2: 'SriLanka'
                    }
                }
            }
        });
    </script>
    <div>
        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row" id="ulShow" runat="server">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title"><asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>
                        <div class="col-12 mb-3">
                            <div class="row">
                                <div class="col-lg-4 col-md-6 col-12">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Application No. :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblRegNo" Font-Bold="true" runat="server" />
                                            </a>
                                        </li>
                                        <li class="list-group-item"><b>Student Name :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblStudName" Font-Bold="true" runat="server" /></a>
                                        </li>
                                        <li class="list-group-item"><b>Gender :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblSex" Font-Bold="true" runat="server" /></a>
                                        </li>
                                        <li class="list-group-item"><b>Faculty :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblFaculty" Font-Bold="true" runat="server" /></a>
                                        </li>
                                    </ul>
                                </div>
                                <div class="col-lg-4 col-md-6 col-12">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Year :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblYear" Font-Bold="true" runat="server" />
                                            </a>
                                        </li>
                                        <li class="list-group-item"><b>Intake :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblBatch" Font-Bold="true" runat="server" /></a>
                                        </li>
                                        <li class="list-group-item"><b>Semester :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblSemester" Font-Bold="true" runat="server" /></a>
                                        </li>
                                        <li class="list-group-item"><b>Specialization :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblBranch" Font-Bold="true" runat="server" /></a>
                                        </li>
                                    </ul>
                                </div>
                                <div class="col-lg-4 col-md-6 col-12">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Mobile No. :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblMobileNo" Font-Bold="true" runat="server" /></a>
                                        </li>
                                        <li class="list-group-item"><b>Email :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblEmailID" Font-Bold="true" runat="server" /></a>
                                        </li>
                                        <li class="list-group-item"><b>Degree :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblDegree" Font-Bold="true" runat="server" /></a>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>

                        <div class="col-12 mb-3 mt-5">
                            <div class="row">
                                <div class="col-lg-3 col-md-6 col-12 offset-md-3">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Service Charge Amount :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblAmount" runat="server" Font-Bold="true"></asp:Label>
                                                <asp:Label ID="lblOrderID" runat="server" CssClass="data_label" Visible="false"></asp:Label>
                                            </a>
                                        </li>
                                    </ul>
                                </div>

                                <div class="col-lg-3 col-md-6 col-12 ">
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-outline-info" Font-Bold="True" OnClick="btnSubmit_Click" Text="Pay" Visible="false" />
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>

