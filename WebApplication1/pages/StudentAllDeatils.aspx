<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteMasterPage.master" CodeFile="StudentAllDeatils.aspx.cs" Inherits="ACADEMIC_StudentAllDeatils" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

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
        //function completeCallback(resultIndicator, sessionVersion) {
        //    //handle payment completion
        //    completeCallback = "http://localhost:55158/PresentationLayer/OnlineResponse.aspx"
        //}
        //completeCallback = "http://localhost:55158/PresentationLayer/OnlineResponse.aspx"
        Checkout.configure({
            session: {
                id: '<%= Session["ERPPaymentSession"] %>'
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
    <%-- <script>
        $(document).ready(function () {
            $("#ctl00_ContentPlaceHolder1_OKButton").click(function () {
                $("#ctl00_ContentPlaceHolder1_divlkPayment").removeClass("active");
            });
        });
    </script>--%>

    <style>
        .search-icon #ctl00_ContentPlaceHolder1_imgSearch {
            box-shadow: 0px 0px 12px #ccc;
            padding: 8px;
            border-radius: 50%;
        }
    </style>
    <style>
        .sbs-title {
            color: #c3c8cf;
            text-transform: uppercase;
            font-size: 0.625rem;
            margin: 0 0 1rem;
            padding: 1rem 0;
        }

            .sbs-title:first-of-type {
                padding-top: 0;
                margin-top: 0;
            }

        hr {
            border-top: 1px solid #edeff1;
            margin: 3rem 0 2rem;
        }

        .sbs--basic {
            margin: 0 -1rem;
            display: -webkit-box;
            display: -ms-flexbox;
            display: flex;
        }

            .sbs--basic li {
                -webkit-box-flex: 1;
                -ms-flex: 1;
                flex: 1;
                padding: 0 1rem;
            }



                .sbs--basic li .step {
                    padding: 2rem 0 0;
                    display: -webkit-box;
                    display: -ms-flexbox;
                    display: flex;
                    -webkit-box-orient: vertical;
                    -webkit-box-direction: normal;
                    -ms-flex-direction: column;
                    flex-direction: column;
                    position: relative;
                }

                    .sbs--basic li .step::before {
                        content: "";
                        position: absolute;
                        top: 0;
                        left: 0;
                        height: 0;
                        width: 100%;
                        border-top: 4px solid #dfe2e5;
                    }

                    .sbs--basic li .step .title {
                        margin-bottom: 0.5rem;
                        text-transform: uppercase;
                        font-size: 0.875rem;
                        font-weight: bold;
                        color: #8a94a1;
                    }

                    .sbs--basic li .step .description {
                        font-weight: bold;
                    }

        .sbs--border {
            display: -webkit-box;
            display: -ms-flexbox;
            display: flex;
            border: 1px solid #d1d5da;
            border-radius: 0.5rem;
        }

            .sbs--border li {
                -webkit-box-flex: 1;
                -ms-flex: 1;
                flex: 1;
            }

                .sbs--border li:last-of-type .step::before,
                .sbs--border li:last-of-type .step::after {
                    display: none;
                }

                .sbs--border li.finished > .step .indicator {
                    background-color: #257f3e;
                    border-color: #257f3e;
                    color: white;
                }

                .sbs--border li.finished > .step .description {
                    color: #2f363d;
                }

                .sbs--border li.active > .step .indicator {
                    border-color: #257f3e;
                    color: #257f3e;
                }

                .sbs--border li.active > .step .description {
                    color: #1f6c35;
                }

                .sbs--border li .step {
                    padding: 1rem 1.5rem;
                    display: -webkit-box;
                    display: -ms-flexbox;
                    display: flex;
                    -webkit-box-align: center;
                    -ms-flex-align: center;
                    align-items: center;
                    position: relative;
                }

                    .sbs--border li .step::before, .sbs--border li .step::after {
                        content: "";
                        height: 45px;
                        width: 1px;
                        background-color: #d1d5da;
                        position: absolute;
                        right: 0;
                        top: 50%;
                    }

                    .sbs--border li .step::before {
                        -webkit-transform-origin: center bottom;
                        transform-origin: center bottom;
                        -webkit-transform: translateY(-100%) rotate(-25deg);
                        transform: translateY(-100%) rotate(-25deg);
                    }

                    .sbs--border li .step::after {
                        -webkit-transform-origin: center top;
                        transform-origin: center top;
                        -webkit-transform: rotate(25deg);
                        transform: rotate(25deg);
                    }

                    .sbs--border li .step .indicator {
                        display: -webkit-box;
                        display: -ms-flexbox;
                        display: flex;
                        -webkit-box-align: center;
                        -ms-flex-align: center;
                        align-items: center;
                        -webkit-box-pack: center;
                        -ms-flex-pack: center;
                        justify-content: center;
                        font-weight: bold;
                        width: 3rem;
                        height: 3rem;
                        border-radius: 50%;
                        margin-right: 1rem;
                        border: 2px solid #d1d5da;
                        color: #8a94a1;
                    }

                    .sbs--border li .step .description {
                        font-weight: bold;
                        font-size: 0.875rem;
                        color: #8a94a1;
                    }

        .sbs--border-alt {
            display: -webkit-box;
            display: -ms-flexbox;
            display: flex;
            border: 1px solid #d1d5da;
            border-radius: 0.5rem;
        }

            .sbs--border-alt li {
                -webkit-box-flex: 1;
                -ms-flex: 1;
                flex: 1;
            }

                .sbs--border-alt li:last-of-type .step::before,
                .sbs--border-alt li:last-of-type .step::after {
                    display: none;
                }

                .sbs--border-alt li.finished > .step .indicator {
                    background-color: #257f3e;
                    border-color: #257f3e;
                    color: white;
                }

                .sbs--border-alt li.finished > .step .description {
                    color: #2f363d;
                }

                .sbs--border-alt li.active > .step .indicator {
                    border-color: #257f3e;
                    color: #257f3e;
                }

                .sbs--border-alt li.active > .step .description {
                    color: #1f6c35;
                }

                .sbs--border-alt li.active > .step .line {
                    background-color: green;
                }

                .sbs--border-alt li .step {
                    padding: 1rem 1.5rem;
                    display: -webkit-box;
                    display: -ms-flexbox;
                    display: flex;
                    -webkit-box-align: center;
                    -ms-flex-align: center;
                    align-items: center;
                    position: relative;
                }

                    .sbs--border-alt li .step::before {
                        content: "";
                        width: 1px;
                        height: 100%;
                        background-color: #d1d5da;
                        position: absolute;
                        right: 0;
                    }

                    .sbs--border-alt li .step::after {
                        content: "";
                        width: 1rem;
                        height: 1rem;
                        background-color: white;
                        position: absolute;
                        right: 0;
                        top: 50%;
                        border-top: 1px solid #d1d5da;
                        border-right: 1px solid #d1d5da;
                        -webkit-transform: translate(50%, -50%) rotate(45deg);
                        transform: translate(50%, -50%) rotate(45deg);
                        z-index: 1;
                    }

                    .sbs--border-alt li .step .indicator {
                        display: -webkit-box;
                        display: -ms-flexbox;
                        display: flex;
                        -webkit-box-align: center;
                        -ms-flex-align: center;
                        align-items: center;
                        -webkit-box-pack: center;
                        -ms-flex-pack: center;
                        justify-content: center;
                        font-weight: bold;
                        width: 3rem;
                        height: 3rem;
                        border-radius: 50%;
                        margin-right: 1rem;
                        border: 2px solid #d1d5da;
                        color: #8a94a1;
                    }

                    .sbs--border-alt li .step .description {
                        font-weight: bold;
                        font-size: 0.875rem;
                        color: #8a94a1;
                        display: -webkit-box;
                        display: -ms-flexbox;
                        display: flex;
                        -webkit-box-orient: vertical;
                        -webkit-box-direction: normal;
                        -ms-flex-direction: column;
                        flex-direction: column;
                    }

                        .sbs--border-alt li .step .description span:first-of-type {
                            margin-bottom: 0.25rem;
                            text-transform: uppercase;
                        }

                        .sbs--border-alt li .step .description span:last-of-type {
                            color: #b5bbc3;
                        }

                    .sbs--border-alt li .step .line {
                        position: absolute;
                        width: 100%;
                        height: 4px;
                        background-color: transparent;
                        bottom: 0;
                        left: 0;
                    }

        .sbs--circles {
            display: -webkit-box;
            display: -ms-flexbox;
            display: flex;
        }

            .sbs--circles li {
                -webkit-box-flex: 1;
                -ms-flex: 1;
                flex: 1;
                display: -webkit-box;
                display: -ms-flexbox;
                display: flex;
                -webkit-box-orient: vertical;
                -webkit-box-direction: normal;
                -ms-flex-direction: column;
                flex-direction: column;
            }

                .sbs--circles li:last-of-type .step .line {
                    display: none;
                }

                .sbs--circles li.finished > .step .indicator {
                    background-color: #257f3e;
                    border-color: #257f3e;
                    color: white;
                }

                .sbs--circles li.finished > .step .description {
                    color: #2f363d;
                }

                .sbs--circles li.finished > .step .line {
                    background-color: #257f3e;
                }

                .sbs--circles li.active > .step .indicator {
                    border-color: #257f3e;
                    color: #257f3e;
                }

                .sbs--circles li.active > .step .description {
                    color: #1f6c35;
                }

                .sbs--circles li .step {
                    display: -webkit-box;
                    display: -ms-flexbox;
                    display: flex;
                    -webkit-box-orient: vertical;
                    -webkit-box-direction: normal;
                    -ms-flex-direction: column;
                    flex-direction: column;
                    -webkit-box-align: center;
                    -ms-flex-align: center;
                    align-items: center;
                    position: relative;
                }

                    .sbs--circles li .step .indicator {
                        display: -webkit-box;
                        display: -ms-flexbox;
                        display: flex;
                        -webkit-box-align: center;
                        -ms-flex-align: center;
                        align-items: center;
                        -webkit-box-pack: center;
                        -ms-flex-pack: center;
                        justify-content: center;
                        font-weight: bold;
                        width: 3rem;
                        height: 3rem;
                        border-radius: 50%;
                        border: 2px solid #d1d5da;
                        color: #8a94a1;
                        position: relative;
                        z-index: 1;
                        background-color: white;
                    }

                    .sbs--circles li .step .description {
                        font-weight: bold;
                        font-size: 0.875rem;
                        color: #8a94a1;
                        position: absolute;
                        bottom: -1.5rem;
                    }

                    .sbs--circles li .step .line {
                        height: 4px;
                        background-color: #dfe2e5;
                        width: 100%;
                        position: absolute;
                        top: 50%;
                        left: 50%;
                        -webkit-transform: translateY(-50%);
                        transform: translateY(-50%);
                    }

        .sbs--dots {
            display: -webkit-box;
            display: -ms-flexbox;
            display: flex;
            -webkit-box-orient: vertical;
            -webkit-box-direction: normal;
            -ms-flex-direction: column;
            flex-direction: column;
            width: 200px;
            margin: 0 auto;
        }

            .sbs--dots li {
                padding: 1rem 0;
            }

                .sbs--dots li.finished > .step .indicator {
                    background-color: #257f3e;
                    border-color: #257f3e;
                    color: white;
                }

                .sbs--dots li.finished > .step .description {
                    color: #2f363d;
                }

                .sbs--dots li.finished > .step .line {
                    background-color: #257f3e;
                }

                .sbs--dots li.active > .step .indicator {
                    border-color: #cae4d1;
                    background-color: green;
                }

                .sbs--dots li.active > .step .description {
                    color: #1f6c35;
                }

                .sbs--dots li .step {
                    display: -webkit-box;
                    display: -ms-flexbox;
                    display: flex;
                    position: relative;
                }

                    .sbs--dots li .step .indicator {
                        display: -webkit-box;
                        display: -ms-flexbox;
                        display: flex;
                        -webkit-box-align: center;
                        -ms-flex-align: center;
                        align-items: center;
                        -webkit-box-pack: center;
                        -ms-flex-pack: center;
                        justify-content: center;
                        font-weight: bold;
                        width: 1.125rem;
                        height: 1.125rem;
                        border-radius: 50%;
                        border: 4px solid white;
                        color: #8a94a1;
                        position: absolute;
                        top: 50%;
                        left: -2rem;
                        -webkit-transform: translateY(-50%);
                        transform: translateY(-50%);
                        background-color: #d1d5da;
                        font-size: 0.625rem;
                    }

                    .sbs--dots li .step .description {
                        font-weight: bold;
                        font-size: 0.875rem;
                        color: #8a94a1;
                    }

        .sbs > li {
            cursor: pointer;
        }

            .sbs > li.active {
                cursor: default;
            }

        .sbs--basic li.finished .step::before {
            border-color: green;
        }

        .sbs--basic li.finished .step .title {
            color: #1f6c35;
        }

        .sbs--basic li.active .step::before {
            border-color: green;
            border-top-style: dotted;
        }

        .sbs--basic li.active .step .title {
            color: #1f6c35;
        }

        .dynamic-nav-tabs li.active a {
            color: #255282;
        }

        .nav-tabs-custom .nav-tabs .nav-link:focus, .nav-tabs-custom .nav-tabs .nav-link:hover {
            border-color: #fff #fff #fff;
        }

            .nav-tabs-custom .nav-tabs .nav-link:focus .description, .nav-tabs-custom .nav-tabs .nav-link:hover .description {
                color: #000;
            }
    </style>
    <style>
        .pay-opt {
            display: flex;
        }

        @media (max-width:767px) {
            .pay-opt {
                display: inline-block;
            }
        }
    </style>

    <div class="row" id="ulShow" runat="server">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-12 col-md-12 col-12">
                                <div class="text-center search-icon">
                                    <a href="#" title="Search Student for Modification" data-toggle="modal" data-target="#myModal2">
                                        <asp:Image ID="imgSearch" runat="server" ImageUrl="~/IMAGES/search-svg.png" TabIndex="1"
                                            AlternateText="Search Student by Name, Reg. No" Style="padding-left: -500px" ToolTip="Search Student by IDNo, Name, Reg. No, Branch, Semester" /></a>

                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="upddetails" runat="server" visible="false">

                        <div class="nav-tabs-custom mt-2 col-12 pb-4" id="myTabContent">
                            <ul class="nav nav-tabs dynamic-nav-tabs sbs sbs--basic" role="tablist">
                                <li class="nav-item" id="divacceptance" runat="server">
                                    <asp:LinkButton ID="lkacceptance" runat="server" OnClick="lkacceptance_Click" CssClass="nav-link" TabIndex="1">
                                 <div class="step">
                                    <span class="title">Step 1</span>
                                    <span class="description">Conditional Offer of Acceptance</span>
                                </div>
                                    </asp:LinkButton>
                                </li>
                                <li class="nav-item" id="divOnlineDetails" runat="server">
                                    <asp:LinkButton ID="lnkOnlineDetails" runat="server" CssClass="nav-link" TabIndex="2" OnClick="lnkOnlineDetails_Click">
                                <div class="step">
                                    <span class="title">Step 2</span>
                                    <span class="description">Applicant Detail</span>
                                </div>
                                    </asp:LinkButton>
                                </li>
                                <li class="nav-item" id="divlkuploaddocument" runat="server">
                                    <asp:LinkButton ID="lkuploaddocumnet" runat="server" OnClick="lkuploaddocumnet_Click" CssClass="nav-link" TabIndex="3">
                                <div class="step">
                                    <span class="title">Step 3</span>
                                    <span class="description">Documents</span>
                                </div>
                                    </asp:LinkButton>
                                </li>
                                <li class="nav-item" id="divlkModuleOffer" runat="server">
                                    <asp:LinkButton ID="lkModuleRegistration" runat="server" OnClick="lkModuleRegistration_Click" CssClass="nav-link" TabIndex="4">
                                <div class="step">
                                    <span class="title">Step 4</span>
                                    <span class="description">Modules</span>
                                </div>
                                    </asp:LinkButton>
                                </li>
                                <li class="nav-item active" id="divlkPayment" runat="server">
                                    <asp:LinkButton ID="lkpayment" runat="server" OnClick="lkpayment_Click" CssClass="nav-link" TabIndex="5">
                                <div class="step">
                                    <span class="title">Step 5</span>
                                    <span class="description">Payment</span>
                                </div>
                                    </asp:LinkButton>
                                </li>
                                <li class="nav-item" id="divlkstatus" runat="server">
                                    <asp:LinkButton ID="lkstatus" runat="server" OnClick="lkstatus_Click" CssClass="nav-link" TabIndex="6">
                                <div class="step">
                                    <span class="title">Step 6</span>
                                    <span class="description">Status</span>
                                </div>
                                    </asp:LinkButton>
                                </li>
                            </ul>
                            <div class="tab-content">

                                <div class="tab-pane fade show active mt-3" id="acceptance" role="tabpanel" runat="server" aria-labelledby="IntakeTransfer-tab">
                                    <div>
                                        <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updBulkReg"
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
                                            <div id="divaceptpayment" runat="server">
                                                <div class="box-body">
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
                                                                    <li class="list-group-item"><b>Payment Type :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblPaymentType" Font-Bold="true" runat="server" /></a>
                                                                    </li>
                                                                </ul>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divRecieptType" runat="server" visible="false">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Receipt Type</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlReceiptType" CssClass="form-control" data-select2-enable="true" runat="server" AppendDataBoundItems="true" Enabled="false">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divprgmname" runat="server" visible="false">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Program Name</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlProgramName" CssClass="form-control" data-select2-enable="true" runat="server" AppendDataBoundItems="true" Visible="false">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlProgramName" InitialValue="0"
                                                                    Display="None" ErrorMessage="Please Select Program Name" ValidationGroup="Summary"></asp:RequiredFieldValidator>
                                                            </div>


                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divsemester" runat="server" visible="false">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Semester</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlSemester" CssClass="form-control" data-select2-enable="true" runat="server" AppendDataBoundItems="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <label>Campus</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlcamous" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlcamous_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ControlToValidate="ddlcamous" InitialValue="0"
                                                                    Display="None" ErrorMessage="Please Select Campus" ValidationGroup="Summary"></asp:RequiredFieldValidator>

                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <label>Week Day/Week End</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlweek" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ControlToValidate="ddlweek" InitialValue="0"
                                                                    Display="None" ErrorMessage="Please Select Week Day/Week End" ValidationGroup="Summary"></asp:RequiredFieldValidator>

                                                            </div>

                                                            <div class="col-12">
                                                                <asp:Panel ID="pnlProgramName" runat="server" Visible="true">
                                                                    <asp:ListView ID="lstProgramName" runat="server">
                                                                        <LayoutTemplate>
                                                                            <div>
                                                                                <div class="sub-heading">
                                                                                    <h5>Program Name</h5>
                                                                                </div>
                                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                                                    <thead class="bg-light-blue">
                                                                                        <tr id="trRow">
                                                                                            <%--<th id="thHead" style="width: 5%"></th>--%>
                                                                                            <th></th>
                                                                                            <th>Discipline
                                                                                            </th>
                                                                                            <th>Faculty/School
                                                                                            </th>
                                                                                            <th>Program
                                                                                            </th>
                                                                                            <th>Campus
                                                                                            </th>
                                                                                            <th>Awarding Institute
                                                                                            </th>
                                                                                            <th>Acceptance Date</th>
                                                                                        </tr>

                                                                                    </thead>
                                                                                    <tbody>
                                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                                    </tbody>
                                                                                </table>

                                                                            </div>
                                                                        </LayoutTemplate>
                                                                        <ItemTemplate>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:CheckBox ID="chkRowsProgram" runat="server" onclick="CheckUnchekCheckbox(this);" OnCheckedChanged="chkRowsProgram_CheckedChanged" AutoPostBack="true" Checked='<%# Convert.ToInt32(Eval("DM_NO")) > 0 ? true:false   %>' />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblarea" runat="server" Text='<%# Eval("AREA_INT_NAME") %>' ToolTip='<%# Eval("AREA_INT_NO") %>' /></td>
                                                                                <%-- <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>'></asp:Label>
                                                                    <asp:HiddenField ID="hdfCourseNo" runat="server" Value='<%# Eval("COURSENO") %>' />
                                                                    <asp:HiddenField ID="hdfSubid" runat="server" Value='<%# Eval("SUBID") %>' />--%>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblcollegename" runat="server" Text='<%# Eval("COLLEGE_NAME") %>' ToolTip='<%# Eval("COLLEGE_ID") %>' /></td>

                                                                                <%-- <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>'></asp:Label></td>--%>
                                                                                <td>
                                                                                    <asp:Label ID="lbldegree" runat="server" Text='<%# Eval("PROGRAM_NAME") %>' ToolTip='<%# Eval("DEGREENO") %>' />
                                                                                    <asp:HiddenField ID="hdfdegree" runat="server" Value='<%# Eval("DEGREENO") %>' />
                                                                                    <asp:HiddenField ID="hdfbranchno" runat="server" Value='<%# Eval("BRANCHNO") %>' />

                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="lblCampus" runat="server" Text='<%# Eval("CAMPUSNAME") %>' ToolTip='<%# Eval("CAMPUSNO") %>' /></td>
                                                                                <td>
                                                                                    <asp:Label ID="lblAffiliated" runat="server" Text='<%# Eval("AFFILIATED_SHORTNAME") %>' ToolTip='<%# Eval("AFFILIATED_NO") %>' /></td>
                                                                                <td>
                                                                                    <asp:Label ID="lblAcceptanceDate" runat="server" Text='<%# Eval("DEMAND_DATE") %>' /></td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:ListView>

                                                                </asp:Panel>
                                                            </div>
                                                            <div class="form-group col-lg-9 col-md-9 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Remark </label>
                                                                </div>
                                                                <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" placeholder="Remark" MaxLength="150"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ControlToValidate="txtRemark"
                                                                    Display="None" ErrorMessage="Please Enter Remark" ValidationGroup="Summary"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="col-12 btn-footer">
                                                                <%--<asp:Button ID="btnShowDetails" runat="server" Text="Accept" CssClass="btn btn-outline-info" OnClick="btnShowDetails_Click" ValidationGroup="Summary" />--%>

                                                                <asp:Button ID="btnShowDetails" runat="server" Text="Conditional Offer of Acceptance" CssClass="btn btn-outline-info" OnClick="btnShowDetails_Click" ValidationGroup="Summary" />

                                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                                                    ShowMessageBox="True" ValidationGroup="Summary" ShowSummary="False" />
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12" visible="false" runat="server" id="divamount">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Amount </label>
                                                                </div>
                                                                <div class="input-group">
                                                                    <div class="input-group-addon">
                                                                        <div class="fa fa-inr text-green"></div>
                                                                    </div>
                                                                    <asp:TextBox ID="txtAmount" runat="server" TabIndex="3" CssClass="form-control" ToolTip="Please Enter Amount" onkeypress="CheckNumeric(event);" Width="90px" MaxLength="10">
                                                                    </asp:TextBox>
                                                                    <div class="input-group-addon">
                                                                        <span>.00</span>
                                                                    </div>
                                                                </div>
                                                                <%-- <asp:RequiredFieldValidator ID="rfvtxtamount" runat="server" ControlToValidate="txtAmount" ValidationExpression="(^([0-9]*|\d*\d{1}?\d*)$)" 
                                                Display="None" ErrorMessage="Please Enter Amount" ValidationGroup="save"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                                                            </div>
                                                            <div id="Div34" class="col-12" runat="server" visible="false">
                                                                <asp:ListView ID="lvOfferAccept" runat="server">
                                                                    <LayoutTemplate>
                                                                        <div>
                                                                            <div class="sub-heading">
                                                                                <h5>Offer Acceptance Details</h5>
                                                                            </div>
                                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                                                <thead class="bg-light-blue">
                                                                                    <tr id="trRow">
                                                                                        <%--<th id="thHead" style="width: 5%"></th>--%>
                                                                                        <th>SrNo.
                                                                                        </th>
                                                                                        <th>Delete
                                                                                        </th>
                                                                                        <%--<th>Degree
                                                                                </th>--%>
                                                                                        <th>Program
                                                                                        </th>
                                                                                        <th>Acceptance Date
                                                                                        </th>
                                                                                    </tr>

                                                                                </thead>
                                                                                <tbody>
                                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                                </tbody>
                                                                            </table>

                                                                        </div>
                                                                    </LayoutTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <%--<td style="width: 10%"></td>--%>
                                                                            <td><%# Container.DisplayIndex + 1 %>
                                                                            </td>

                                                                            <%--<td>
                                                                     <asp:LinkButton ID="lnkOfferDelete" runat="server" CssClass="fa fa-trash" Style="color: red" ToolTip="Delete" CommandName='<%# Container.DataItemIndex + 1 %>' CommandArgument='<%#Eval("SRNO") %>'></asp:LinkButton>

                                                                    </td>--%>


                                                                            <td>
                                                                                <asp:ImageButton ID="btneditProgram" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%# Eval("IDNO") %>'
                                                                                    AlternateText="Delete Record" OnClick="btneditProgram_Click" OnClientClick="return confirm('Do you really want to delete this Program entry?');"
                                                                                    TabIndex="14" ToolTip='<%# Eval("DM_NO") %>' />
                                                                                <asp:HiddenField ID="hdfDmNo" runat="server" Value='<%#Eval("DM_NO") %>' />


                                                                            </td>

                                                                            <td><%# Eval("DEGREENAME") %> - <%# Eval("LONGNAME") %></td>
                                                                            <%--<td>
                                                                        <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("LONGNAME") %>'></asp:Label></td>--%>
                                                                            <td><%# Eval("DEMAND_DATE") %></td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:ListView>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div id="diApplicantDetails" role="tabpanel" runat="server" aria-labelledby="IntakeTransfer-tab" visible="false">
                                    <div>
                                        <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="updApplicantDetails"
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
                                    <asp:UpdatePanel ID="updApplicantDetails" runat="server">
                                        <ContentTemplate>
                                            <div id="div5" runat="server">
                                                <div class="box-body">
                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="col-12">
                                                                <div class="sub-heading">
                                                                    <h5>Personal Information</h5>
                                                                </div>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>First Name</label>
                                                                </div>
                                                                <asp:TextBox ID="txtFirstName" runat="server" TabIndex="1" ToolTip="First Name "
                                                                    onkeypress="return alphaOnly(event);" MaxLength="50" CssClass="form-control" />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="Please Enter First Name"
                                                                    ControlToValidate="txtFirstName" Display="None" SetFocusOnError="true" ValidationGroup="sub" />
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtFirstName"
                                                                    InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" FilterMode="InvalidChars" />
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Last Name</label>
                                                                </div>
                                                                <asp:TextBox ID="txtPerlastname" runat="server" TabIndex="2" ToolTip="Last Name "
                                                                    onkeypress="return alphaOnly(event);" MaxLength="50" CssClass="form-control" />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="Please Enter Last Name"
                                                                    ControlToValidate="txtPerlastname" Display="None" SetFocusOnError="true" ValidationGroup="sub" />
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txtPerlastname"
                                                                    InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" FilterMode="InvalidChars" />
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Full Name</label>
                                                                </div>
                                                                <asp:TextBox ID="txtFullName" runat="server" TabIndex="3" ToolTip="Full Name "
                                                                    onkeypress="return alphaOnly(event);" MaxLength="150" CssClass="form-control" />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Enter Full Name"
                                                                    ControlToValidate="txtFullName" Display="None" SetFocusOnError="true" ValidationGroup="sub" />
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtFullName"
                                                                    InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" FilterMode="InvalidChars" />
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Name with Initials (Ex: PERERA S.A)</label>
                                                                </div>
                                                                <asp:TextBox ID="txtNameInitial" runat="server" TabIndex="4" ToolTip="Name with Initials "
                                                                    onkeypress="return alphaOnly(event);" MaxLength="50" CssClass="form-control" Enabled="true" />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="Please Enter Name with Initials"
                                                                    ControlToValidate="txtNameInitial" Display="None" SetFocusOnError="true" ValidationGroup="sub" />
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="ajfFullName" runat="server" TargetControlID="txtNameInitial"
                                                                    InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" FilterMode="InvalidChars" />
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Email</label>
                                                                </div>
                                                                <asp:TextBox ID="txtEmail" runat="server" TabIndex="5" CssClass="form-control" Placeholder="Please Enter Email ID" />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="Please Enter Email ID"
                                                                    ControlToValidate="txtEmail" Display="None" SetFocusOnError="true" ValidationGroup="sub" />
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtEmail"
                                                                    Display="None" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                                    ErrorMessage="Please Enter Valid Email_ID" ValidationGroup="login"></asp:RegularExpressionValidator>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Mobile No.</label>
                                                                </div>
                                                                <div class="input-group">
                                                                    <span class="input-group-prepend" style="width: 130px! important;">
                                                                        <asp:DropDownList ID="ddlOnlineMobileCode" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="6" onchange="return RemoveCountryName()">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator62" runat="server" ErrorMessage="Please select Mobile Code"
                                                                            ControlToValidate="ddlOnlineMobileCode" Display="None" SetFocusOnError="true" ValidationGroup="sub" InitialValue="0" />
                                                                    </span>
                                                                    <asp:TextBox ID="txtMobile" runat="server" TabIndex="7"
                                                                        MaxLength="12" ToolTip="Please Enter Mobile No." CssClass="form-control ml-2 pb-0 pl-0 pr-0" Placeholder="Please Enter Mobile No." />
                                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtMobile"
                                                                        ValidChars="1234567890" FilterMode="ValidChars" />
                                                                    <asp:RegularExpressionValidator runat="server" ErrorMessage="Mobile No. is Invalid"
                                                                        ID="RegularExpressionValidator3" ControlToValidate="txtMobile" ValidationExpression=".{10}.*"
                                                                        Display="None" ValidationGroup="sub"></asp:RegularExpressionValidator>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ErrorMessage="Please Enter Mobile No."
                                                                        ControlToValidate="txtMobile" Display="None" SetFocusOnError="true" ValidationGroup="sub" />
                                                                </div>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Telephone.</label>
                                                                </div>
                                                                <div class="input-group">
                                                                    <span class="input-group-prepend" style="width: 130px! important;">
                                                                        <asp:DropDownList ID="ddlHomeMobileCode" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" TabIndex="8" onchange="return RemoveCountryName()">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator63" runat="server" ErrorMessage="Please select Telephone code"
                                                                            ControlToValidate="ddlHomeMobileCode" Display="None" SetFocusOnError="true" ValidationGroup="sub" InitialValue="0" />
                                                                    </span>
                                                                    <asp:TextBox ID="txtHomeTel" runat="server" TabIndex="9" CssClass="form-control ml-2 pb-0 pl-0 pr-0" Placeholder="Please Enter Telephone ." MaxLength="10" />
                                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="txtHomeTel"
                                                                        ValidChars="1234567890" FilterMode="ValidChars" />
                                                                    <asp:RegularExpressionValidator runat="server" ErrorMessage="Mobile No. is Invalid"
                                                                        ID="RegularExpressionValidator4" ControlToValidate="txtHomeTel" ValidationExpression=".{6}.*"
                                                                        Display="None" ValidationGroup="sub"></asp:RegularExpressionValidator>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ErrorMessage="Please Enter Telephone ."
                                                                        ControlToValidate="txtHomeTel" Display="None" SetFocusOnError="true" ValidationGroup="sub" />
                                                                </div>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <label>NIC (National Identity card)</label>
                                                                </div>
                                                                <asp:TextBox ID="txtOnlineNIC" runat="server" TabIndex="10" ToolTip="Please NIC "
                                                                    MaxLength="30" CssClass="form-control" />
                                                                <%--      <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ErrorMessage="Please Enter NIC "
                                                             ControlToValidate="txtNIC" Display="None" SetFocusOnError="true" ValidationGroup="sub" />      --%>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtOnlineNIC"
                                                                    InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" FilterMode="InvalidChars" />
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <label>Passport No</label>
                                                                </div>
                                                                <asp:TextBox ID="txtPersonalPassprtNo" runat="server" TabIndex="11" ToolTip="Please Enter Passport No "
                                                                    MaxLength="30" CssClass="form-control" />
                                                                <%--    <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ErrorMessage="Please Enter Passport "
                                                            ControlToValidate="txtPassport" Display="None" SetFocusOnError="true" ValidationGroup="sub" />      --%>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" TargetControlID="txtPersonalPassprtNo"
                                                                    InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" FilterMode="InvalidChars" />
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Date Of Birth (DD/MM/YY)</label>
                                                                </div>
                                                                <asp:TextBox ID="txtDateOfBirth" name="dob" runat="server" TabIndex="12" CssClass="form-control dob"
                                                                    ToolTip="Please Enter Date Of Birth" placeholder="DD/MM/YYYY" />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ErrorMessage="Please Enter DOB (DD/MM/YY)"
                                                                    ControlToValidate="txtDateOfBirth" Display="None" SetFocusOnError="true" ValidationGroup="sub" />
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Gender</label>
                                                                </div>
                                                                <asp:RadioButtonList ID="rdPersonalGender" runat="server" TabIndex="13" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Text="&nbsp;Male" Value="0"></asp:ListItem>
                                                                    <asp:ListItem Text="&nbsp;Female" Value="1"></asp:ListItem>
                                                                    <%--<asp:ListItem Text="&nbsp;Others" Value="2"></asp:ListItem>--%>
                                                                </asp:RadioButtonList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ErrorMessage="Please Select Gender"
                                                                    ControlToValidate="rdPersonalGender" Display="None" ValidationGroup="sub"></asp:RequiredFieldValidator>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divFather" runat="server" visible="true">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Name of Parent/Guardian</label>
                                                                </div>
                                                                <asp:TextBox ID="txtMiddleName" runat="server" Width="100%" ToolTip="Please Enter Father Name"
                                                                    onkeypress="return alphaOnly(event);" MaxLength="20" CssClass="form-control" Placeholder="Please Enter Father Name" TabIndex="0" />
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" TargetControlID="txtMiddleName"
                                                                    InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" FilterMode="InvalidChars" />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ErrorMessage="Please Enter Father Name"
                                                                    ControlToValidate="txtMiddleName" Display="None" SetFocusOnError="true" ValidationGroup="sub" />
                                                                <asp:HiddenField runat="server" ID="HiddenField1" />
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divMother" runat="server" visible="false">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Mother's Name</label>
                                                                </div>
                                                                <asp:TextBox ID="txtMothersName" runat="server" Width="100%" TabIndex="0" ToolTip="Please Enter Mother's Name"
                                                                    onkeypress="return alphaOnly(event);" MaxLength="20" CssClass="form-control" Placeholder="Please Enter Mother's Name" />
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftbemathername" runat="server" TargetControlID="txtMothersName"
                                                                    InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" FilterMode="InvalidChars" />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ErrorMessage="Please Enter Mother's Name"
                                                                    ControlToValidate="txtMothersName" Display="None" SetFocusOnError="true" ValidationGroup="sub" />
                                                                <asp:HiddenField runat="server" ID="HiddenField2" />
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Parent/Guardian Personal Email</label>
                                                                </div>
                                                                <asp:TextBox ID="txtPersonalEmail" runat="server" TabIndex="5" CssClass="form-control" Placeholder="Enter Email" ToolTip="Please Enter Email" />
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPersonalEmail"
                                                                    Display="None" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                                    ErrorMessage="Please Enter Valid Email_ID" ValidationGroup="submit"></asp:RegularExpressionValidator>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ErrorMessage="Please Enter Parent/Guardian Personal Email"
                                                                    ControlToValidate="txtPersonalEmail" Display="None" SetFocusOnError="true" ValidationGroup="submit" />
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divContact" runat="server" visible="true">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Parent/Guardian Contact Number</label>
                                                                </div>
                                                                <div class="input-group">
                                                                    <span class="input-group-prepend" style="width: 100px! important;">
                                                                        <asp:DropDownList ID="ddlConCode" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" Style="padding-right: 0px!important;" TabIndex="0" onchange="return RemoveCountryName()">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator37" runat="server" ErrorMessage="Please select Mobile code"
                                                                            ControlToValidate="ddlConCode" Display="None" SetFocusOnError="true" ValidationGroup="sub" InitialValue="0" />
                                                                    </span>
                                                                    <asp:TextBox ID="txtPMobNo" runat="server" TabIndex="10" onkeyup="validateNumeric(this)"
                                                                        MaxLength="15" ToolTip="Please Enter Parent's Contact No." CssClass="form-control ml-2 pb-0 pl-0 pr-0" Placeholder="Enter Parent's Contact No." />
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ErrorMessage="Please Enter Parent's Contact Number"
                                                                        ControlToValidate="txtPMobNo" Display="None" SetFocusOnError="true" ValidationGroup="sub" />
                                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" TargetControlID="txtPMobNo"
                                                                        ValidChars="1234567890" FilterMode="ValidChars" />
                                                                </div>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="DIVSpeciallyAbled" runat="server" style="display: block" visible="false">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Is Specially Abled</label>
                                                                </div>
                                                                <asp:RadioButtonList ID="rdobtnpwd" runat="server" TabIndex="0" RepeatDirection="Horizontal"
                                                                    onclick="GetSelectedItem();">
                                                                    <asp:ListItem Text="&nbsp;Yes" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="&nbsp;No" Value="0"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ErrorMessage="Please Select Is Specially Abled Yes or No"
                                                                    ControlToValidate="rdobtnpwd" Display="None" ValidationGroup="sub"></asp:RequiredFieldValidator>
                                                            </div>

                                                            <script type="text/javascript">
                                                                function GetSelectedItem() {
                                                                    var rb = document.getElementById("<%=rdobtnpwd.ClientID%>");
                                                                    var radio = rb.getElementsByTagName("input");
                                                                    for (var i = 0; i < radio.length; i++) {
                                                                        if (radio[0].checked) {
                                                                            document.getElementById('<%=DIVtYPE.ClientID%>').style.display = 'block'
                                                                            document.getElementById('<%=txtcheckdisability.ClientID%>').value = "";
                                                                            break;
                                                                        }
                                                                        else {
                                                                            document.getElementById('<%=DIVtYPE.ClientID%>').style.display = 'none'
                                                                            document.getElementById('<%=txtcheckdisability.ClientID%>').value = "1";
                                                                            document.getElementById('<%=ddlDisabilityType.ClientID%>').value = "0";

                                                                        }
                                                                    }

                                                                    return false;
                                                                }

                                                                function checkchange(ddl) {
                                                                    if (ddl.value == "0") {
                                                                        document.getElementById('<%=txtcheckdisability.ClientID%>').value = "";
                                                                    }
                                                                    else {
                                                                        document.getElementById('<%=txtcheckdisability.ClientID%>').value = "1";
                                                                    }
                                                                }
                                                            </script>

                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="DIVtYPE" runat="server" style="display: none" visible="false">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Abled Type</label>
                                                                </div>
                                                                <asp:DropDownList runat="server" ID="ddlDisabilityType" onchange="checkchange(this)" TabIndex="0" AppendDataBoundItems="true"
                                                                    ToolTip="Please Select Abled Type" CssClass="form-control" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <div style="display: none">
                                                                    <asp:TextBox ID="txtcheckdisability" runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Citizen Type</label>
                                                                </div>
                                                                <asp:RadioButtonList ID="rdbQuestion" runat="server" TabIndex="14" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Text="&nbsp;Sri Lankan" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="&nbsp;Foreign National" Value="2"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ErrorMessage="Please Select Student sri Lankan or foreign national"
                                                                    ControlToValidate="rdbQuestion" Display="None" ValidationGroup="sub"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="col-xl-3 col-md-6">
                                                                <div class="form-margin">
                                                                    <label><sup></sup>Left / Right Handed </label>
                                                                    <div>
                                                                        <asp:RadioButtonList ID="rdbLeftRight" runat="server" TabIndex="15" RepeatDirection="Horizontal">
                                                                            <asp:ListItem Text="&nbsp;Left Handed" Value="1"></asp:ListItem>
                                                                            <asp:ListItem Text="&nbsp;Right Handed" Value="2"></asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                        <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ErrorMessage="Please Select Left / Right Handed"
                                                                    ControlToValidate="rdbLeftRight" Display="None" ValidationGroup="sub"></asp:RequiredFieldValidator>--%>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="col-12">
                                                        <div class="row">
                                                            <%-- <div class="col-12">
                                                        <div class="sub-heading">
                                                            <h5>Address Details</h5>
                                                        </div>
                                                    </div>--%>
                                                            <div class="col-12">
                                                                <div class="sub-heading">
                                                                    <h5>Postal Address</h5>
                                                                </div>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Address (Max. Length 100)</label>
                                                                </div>
                                                                <asp:TextBox ID="txtPermAddress" runat="server" TabIndex="16" TextMode="MultiLine"
                                                                    MaxLength="200" ToolTip="Please Enter Permenant Address" CssClass="form-control" onkeyup="return CountCharactersPerment();" />
                                                                <asp:RequiredFieldValidator ID="rfvPerAdd" runat="server" ControlToValidate="txtPermAddress"
                                                                    ValidationGroup="sub" Display="None" SetFocusOnError="true" ErrorMessage="Please Enter Address">
                                                                </asp:RequiredFieldValidator>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Country</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlPCon" runat="server" TabIndex="17" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvCountry" runat="server" ControlToValidate="ddlPCon"
                                                                    ValidationGroup="sub" Display="None" InitialValue="0" SetFocusOnError="true" ErrorMessage="Please Select Country">
                                                                </asp:RequiredFieldValidator>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Province</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlPermanentState" runat="server" Width="100%" TabIndex="18" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlPermanentState_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvState" runat="server" ControlToValidate="ddlPermanentState"
                                                                    ValidationGroup="sub" Display="None" InitialValue="0" SetFocusOnError="true" ErrorMessage="Please Select Province">
                                                                </asp:RequiredFieldValidator>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>City/Village</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlPermanentCity" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <%--<asp:RequiredFieldValidator ID="rfvPermanentCity" runat="server" ControlToValidate="ddlPermanentCity"
                                                                ValidationGroup="Address" InitialValue="0" Display="none" SetFocusOnError="true" ErrorMessage="Please Select Permanent City/Village">
                                                            </asp:RequiredFieldValidator>--%>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>District</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlPTahsil" runat="server" TabIndex="19" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvTehsil" runat="server" ControlToValidate="ddlPTahsil"
                                                                    ValidationGroup="sub" Display="None" InitialValue="0" SetFocusOnError="true" ErrorMessage="Please Select District">
                                                                </asp:RequiredFieldValidator>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>ZIP/PIN </label>
                                                                </div>
                                                                <asp:TextBox ID="txtPermPIN" runat="server" ToolTip="Please Enter ZIP/PIN."
                                                                    MaxLength="6" onkeyup="validateNumeric(this);" CssClass="form-control" />
                                                                <%--<asp:RequiredFieldValidator ID="rfvPermPIN" runat="server" ControlToValidate="txtPermPIN"
                                                                ValidationGroup="Address" Display="None" SetFocusOnError="true" ErrorMessage="Please Enter Permanent ZIP/PIN">
                                                            </asp:RequiredFieldValidator>--%>
                                                                <ajaxToolKit:FilteredTextBoxExtender ID="ftrPermPIN" runat="server" FilterType="Numbers"
                                                                    InvalidChars="~!@#$%^&*()_+|?></?" TargetControlID="txtPermPIN">
                                                                </ajaxToolKit:FilteredTextBoxExtender>
                                                            </div>

                                                            <%-- <div class="col-12 btn-footer">
                                                        <asp:Button ID="btnPersonalSubmit" runat="server" TabIndex="23" Text="Save" CssClass="btn btn-outline-info" ValidationGroup="sub" OnClick="btnPersonalSubmit_Click" />
                                                        <asp:ValidationSummary ID="ValidationSummary5" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="sub" />
                                                    </div>--%>
                                                        </div>
                                                    </div>
                                                    <div class="col-12">
                                                        <div class="row">

                                                            <div class="col-12">
                                                                <div class="sub-heading">
                                                                    <h5>Permanent Address</h5>
                                                                </div>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Address (Max. Length 200)</label>
                                                                </div>
                                                                <asp:TextBox ID="txtPerAddress" runat="server" TabIndex="16" TextMode="MultiLine" Placeholder="Enter Address"
                                                                    MaxLength="200" ToolTip="Please Enter Address" CssClass="form-control" onkeyup="return CountCharactersPerment();" meta:resourcekey="txtPermAddressResource1" />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ControlToValidate="txtPerAddress"
                                                                    ValidationGroup="submit" Display="None" SetFocusOnError="True" ErrorMessage="Please Enter Address" meta:resourcekey="rfvAddressResource1"></asp:RequiredFieldValidator>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Country</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlPerContry" runat="server" TabIndex="17" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" meta:resourcekey="ddlCountryResource1">
                                                                    <asp:ListItem Value="0" meta:resourcekey="ListItemResource9">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" ControlToValidate="ddlPerContry"
                                                                    ValidationGroup="submit" Display="None" InitialValue="0" SetFocusOnError="True" ErrorMessage="Please Select Country" meta:resourcekey="rfvCountryResource1"></asp:RequiredFieldValidator>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Province</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlPerProvince" runat="server" Width="100%" TabIndex="18" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlPerProvince_SelectedIndexChanged" meta:resourcekey="ddlProvinceResource1">
                                                                    <asp:ListItem Value="0" meta:resourcekey="ListItemResource10">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ControlToValidate="ddlPerProvince"
                                                                    ValidationGroup="submit" Display="None" InitialValue="0" SetFocusOnError="True" ErrorMessage="Please Select Province" meta:resourcekey="rfvProvinceResource1"></asp:RequiredFieldValidator>

                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>District</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlPerDisctrict" runat="server" TabIndex="19" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" meta:resourcekey="ddlDistrictResource1">
                                                                    <asp:ListItem Value="0" meta:resourcekey="ListItemResource11">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" ControlToValidate="ddlPerDisctrict"
                                                                    ValidationGroup="submit" Display="None" InitialValue="0" SetFocusOnError="True" ErrorMessage="Please Select District" meta:resourcekey="rfvDistrictResource1"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="divUgEducationUG" runat="server">
                                                <div>
                                                    <asp:UpdateProgress ID="UpdateProgress10" runat="server" AssociatedUpdatePanelID="updEdcutationalDetailsUG"
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
                                                <asp:UpdatePanel ID="updEdcutationalDetailsUG" runat="server">
                                                    <ContentTemplate>
                                                        <div class="box-body" id="UG" runat="server">
                                                            <div class="col-12">
                                                                <div class="row">
                                                                    <div class="col-12">
                                                                        <div class="sub-heading">
                                                                            <h5>Educational Details</h5>
                                                                        </div>
                                                                        <div class="sub-heading">
                                                                            <h5>A/L Details</h5>
                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup></sup>
                                                                            <label>A/L Exam Type</label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlALTypeUG" runat="server" CssClass="form-control" Enabled="true" data-select2-enable="true" TabIndex="20" ToolTip="Please Select A/L Syllabus" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlALTypeUG_SelectedIndexChanged">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup></sup>
                                                                            <label>A/L passes</label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlALPassesUG" runat="server" CssClass="form-control" Enabled="true" data-select2-enable="true" TabIndex="22" AutoPostBack="true" OnSelectedIndexChanged="ddlALPassesUG_SelectedIndexChanged" ToolTip="Please Select A/L passes" AppendDataBoundItems="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup></sup>
                                                                            <label>A/L Stream</label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlStreamUG" runat="server" CssClass="form-control" Enabled="true" data-select2-enable="true" TabIndex="21" ToolTip="Please Select A/L Stream" Width="100%" AppendDataBoundItems="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup></sup>
                                                                            <label>Subject 1</label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlSubject1" runat="server" CssClass="form-control" Enabled="true" data-select2-enable="true" TabIndex="23" ToolTip="Please Select Subject 1" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSubject1_SelectedIndexChanged">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <div style="display: none">
                                                                            <asp:TextBox ID="txtmathsvalidation" runat="server"></asp:TextBox>
                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup></sup>
                                                                            <label>Grade</label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlGrade1" runat="server" CssClass="form-control" Enabled="true" data-select2-enable="true" TabIndex="24" ToolTip="Please Select Subject 1 Grade" AppendDataBoundItems="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup></sup>
                                                                            <label>Subject 2</label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlSubject2" runat="server" CssClass="form-control" Enabled="true" data-select2-enable="true" TabIndex="25" ToolTip="Please Select Subject 2" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSubject2_SelectedIndexChanged">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup></sup>
                                                                            <label>Grade</label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlGrade2" runat="server" CssClass="form-control" Enabled="true" data-select2-enable="true" TabIndex="26" ToolTip="Please Select Subject 2 Grade" AppendDataBoundItems="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup></sup>
                                                                            <label>Subject 3</label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlSubject3" runat="server" CssClass="form-control" Enabled="true" data-select2-enable="true" TabIndex="27" ToolTip="Please Select Subject 3" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSubject3_SelectedIndexChanged">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="div7" runat="server">
                                                                        <div class="label-dynamic">
                                                                            <sup></sup>
                                                                            <label>Grade</label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlGrade3" runat="server" CssClass="form-control" Enabled="true" data-select2-enable="true" TabIndex="28" ToolTip="Please Select Subject 3 Grade" AppendDataBoundItems="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divbiology" runat="server">
                                                                        <div class="label-dynamic">
                                                                            <sup></sup>
                                                                            <label>Subject 4</label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlSubject4" runat="server" CssClass="form-control" Enabled="true" data-select2-enable="true" TabIndex="29" ToolTip="Please Select Subject 4" AppendDataBoundItems="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="div8" runat="server">
                                                                        <div class="label-dynamic">
                                                                            <sup></sup>
                                                                            <label>Grade</label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlGrade4" runat="server" CssClass="form-control" Enabled="true" data-select2-enable="true" TabIndex="30" ToolTip="Please Select Subject 4 Grade" AppendDataBoundItems="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="div44" runat="server">
                                                                        <div class="label-dynamic">
                                                                            <sup></sup>
                                                                            <label>A/L IndexNo</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtALIndex" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                                                    </div>
                                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="div45" runat="server">
                                                                        <div class="label-dynamic">
                                                                            <sup></sup>
                                                                            <label>A/L Year</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtALyear" runat="server" CssClass="form-control" MaxLength="4"></asp:TextBox>
                                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtALyear"
                                                                            ValidChars="1234567890" FilterMode="ValidChars" />
                                                                    </div>
                                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="div46" runat="server">
                                                                        <div class="label-dynamic">
                                                                            <sup></sup>
                                                                            <label>Z Score</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtZScore" runat="server" CssClass="form-control" MaxLength="5"></asp:TextBox>
                                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" TargetControlID="txtZScore"
                                                                            ValidChars="1234567890." FilterMode="ValidChars" />
                                                                    </div>
                                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="div47" runat="server">
                                                                        <div class="label-dynamic">
                                                                            <sup></sup>
                                                                            <label>A/L School District</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtALSchoolDistrict" runat="server" CssClass="form-control" MaxLength="30"></asp:TextBox>
                                                                    </div>
                                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="div48" runat="server">
                                                                        <div class="label-dynamic">
                                                                            <sup></sup>
                                                                            <label>School</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtALSchool" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-12">
                                                                <div class="row">
                                                                    <div class="col-12">
                                                                        <div class="sub-heading">
                                                                            <h5>O/L Details</h5>
                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group col-lg-12 col-md-12 col-12">
                                                                        <div class=" note-div">
                                                                            <h5 class="heading">Note </h5>
                                                                            <p><i class="fa fa-star" aria-hidden="true"></i><span>Entering O/L results is not mandatory for all programs, check the eligibility of program you wish to apply if needed then it is compulsory to fill the details of O/L.</span></p>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="row">
                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <label>O/L Exam Type </label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlOLType" runat="server" CssClass="form-control select2 select-clik" TabIndex="12" ToolTip="Please Select A/L Exam Type" Width="100%" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlOLType_SelectedIndexChanged">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="Div2" runat="server" visible="false">
                                                                        <div class="label-dynamic">
                                                                            <label>O/L Stream </label>
                                                                        </div>

                                                                        <asp:DropDownList ID="ddlolStream" runat="server" CssClass="form-control select2 select-clik" TabIndex="13" ToolTip="Please Select O/L Stream" Width="100%" AppendDataBoundItems="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlolStream" Display="None" ErrorMessage="Please Select A/L Stream" InitialValue="0" SetFocusOnError="True" ValidationGroup="Exam"></asp:RequiredFieldValidator>--%>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="Div12" runat="server">
                                                                        <div class="label-dynamic">
                                                                            <label>O/L passes </label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlolpass" runat="server" CssClass="form-control select2 select-clik" TabIndex="14" ToolTip="Please Select O/L passes" Width="100%" AppendDataBoundItems="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlolpass" Display="None" ErrorMessage="Please Select A/L passes" InitialValue="0" SetFocusOnError="True" ValidationGroup="Exam"></asp:RequiredFieldValidator>--%>
                                                                    </div>
                                                                </div>


                                                                <div class="row">
                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <label>Subject 1 </label>
                                                                        </div>
                                                                        <asp:DropDownList ID="olddlsub1" runat="server" CssClass="form-control select2 select-clik" TabIndex="31" ToolTip="Please Select Subject 1" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="olddlsub1_SelectedIndexChanged">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <div style="display: none">
                                                                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                                        </div>
                                                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="olddlsub1"
                                                    Display="None" ErrorMessage="Please Select Subject 1" InitialValue="0" SetFocusOnError="True" ValidationGroup="Exam"></asp:RequiredFieldValidator>--%>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <label>Grade </label>
                                                                        </div>
                                                                        <asp:DropDownList ID="olddlgrade1" runat="server" CssClass="form-control select2 select-clik" TabIndex="32" ToolTip="Please Select Subject 1 Grade" AppendDataBoundItems="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>

                                                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="olddlgrade1"
                                                    Display="None" ErrorMessage="Please Select Subject 1 Grade" InitialValue="" SetFocusOnError="True" ValidationGroup="Exam"></asp:RequiredFieldValidator>--%>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <label>Subject 2 </label>
                                                                        </div>
                                                                        <asp:DropDownList ID="olddlsubj2" runat="server" CssClass="form-control select2 select-clik" TabIndex="33" ToolTip="Please Select Subject 2" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="olddlsubj2_SelectedIndexChanged">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="olddlsubj2"
                                                    Display="None" ErrorMessage="Please Select Subject 2" InitialValue="0" SetFocusOnError="True" ValidationGroup="Exam"></asp:RequiredFieldValidator>--%>
                                                                    </div>
                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <label>Grade </label>
                                                                        </div>
                                                                        <asp:DropDownList ID="olddlgrade2" runat="server" CssClass="form-control select2 select-clik" TabIndex="34" ToolTip="Please Select Subject 2 Grade" AppendDataBoundItems="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="olddlgrade2"
                                                    Display="None" ErrorMessage="Please Select Subject 2 Grade" InitialValue="" SetFocusOnError="True" ValidationGroup="Exam"></asp:RequiredFieldValidator>--%>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <label>Subject 3 </label>
                                                                        </div>
                                                                        <asp:DropDownList ID="olddlsub3" runat="server" CssClass="form-control select2 select-clik" TabIndex="35" ToolTip="Please Select Subject 3" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="olddlsub3_SelectedIndexChanged">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>

                                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="olddlsub3"
                                                    Display="None" ErrorMessage="Please Select Subject 3" InitialValue="0" SetFocusOnError="True" ValidationGroup="Exam"></asp:RequiredFieldValidator>--%>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="div13" runat="server">
                                                                        <div class="label-dynamic">
                                                                            <label>Grade </label>
                                                                        </div>
                                                                        <asp:DropDownList ID="olddlgrade3" runat="server" CssClass="form-control select2 select-clik" TabIndex="36" ToolTip="Please Select Subject 3 Grade" AppendDataBoundItems="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="olddlgrade3"
                                                    Display="None" ErrorMessage="Please Select Subject 3 Grade" InitialValue="" SetFocusOnError="True" ValidationGroup="Exam"></asp:RequiredFieldValidator>--%>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="div14" runat="server">
                                                                        <div class="label-dynamic">
                                                                            <label>Subject 4 </label>
                                                                        </div>
                                                                        <asp:DropDownList ID="olddlsub4" runat="server" CssClass="form-control select2 select-clik" TabIndex="37" ToolTip="Please Select Subject 4" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="olddlsub4_SelectedIndexChanged">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>

                                                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="ddlSubject4"
                                                    Display="None" ErrorMessage="Please Select Subject 4" InitialValue="0" SetFocusOnError="True" ValidationGroup="Exam"></asp:RequiredFieldValidator>
                                                                        --%>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="div15" runat="server">
                                                                        <div class="label-dynamic">
                                                                            <label>Grade </label>
                                                                        </div>
                                                                        <asp:DropDownList ID="olddlgrade4" runat="server" CssClass="form-control select2 select-clik" TabIndex="38" ToolTip="Please Select Subject 4 Grade" AppendDataBoundItems="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlGrade4"
                                                    Display="None" ErrorMessage="Please Select Subject 4 Grade" InitialValue="" SetFocusOnError="True" ValidationGroup="Exam"></asp:RequiredFieldValidator>
                                                                        --%>
                                                                    </div>

                                                                    <%-- o/l list end by aashna--%>

                                                                    <%-- oL List Roshan 5 Start--%>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="div16" runat="server">
                                                                        <div class="label-dynamic">
                                                                            <label>Subject 5 </label>
                                                                        </div>
                                                                        <asp:DropDownList ID="olddlsub5" runat="server" CssClass="form-control select2 select-clik" TabIndex="39" ToolTip="Please Select Subject 5" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="olddlsub5_SelectedIndexChanged">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>

                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="div17" runat="server">
                                                                        <div class="label-dynamic">
                                                                            <label>Grade </label>
                                                                        </div>
                                                                        <asp:DropDownList ID="olddlgrade5" runat="server" CssClass="form-control select2 select-clik" TabIndex="40" ToolTip="Please Select Subject 5 Grade" AppendDataBoundItems="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <%-- oL List Roshan 5 END--%>


                                                                    <%-- oL List Roshan 6 Start--%>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="div19" runat="server">
                                                                        <div class="label-dynamic">
                                                                            <label>Subject 6 </label>
                                                                        </div>
                                                                        <asp:DropDownList ID="olddlsub6" runat="server" CssClass="form-control select2 select-clik" TabIndex="41" ToolTip="Please Select Subject 6" AppendDataBoundItems="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="div35" runat="server">
                                                                        <div class="label-dynamic">
                                                                            <label>Grade </label>
                                                                        </div>
                                                                        <asp:DropDownList ID="olddlgrade6" runat="server" CssClass="form-control select2 select-clik" TabIndex="42" ToolTip="Please Select Subject 6 Grade" AppendDataBoundItems="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <%-- oL List Roshan 6 END--%>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>

                                                </asp:UpdatePanel>
                                            </div>
                                            <div id="divEducationPG" runat="server">
                                                <div>
                                                    <asp:UpdateProgress ID="UpdateProgress11" runat="server" AssociatedUpdatePanelID="updEducationDetailsPG"
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
                                                <asp:UpdatePanel ID="updEducationDetailsPG" runat="server">
                                                    <ContentTemplate>
                                                        <div class="box-body">
                                                            <div class="col-12" id="DivAcademic" runat="server">
                                                                <div class="row">
                                                                    <div class="col-12">
                                                                        <div class="sub-heading">
                                                                            <h5>Educational Details</h5>
                                                                        </div>
                                                                        <div class="sub-heading">
                                                                            <h5>Academic Details</h5>
                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup></sup>
                                                                            <label>Name of Qualification</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtHighestEducationPG" runat="server" TabIndex="1" Enabled="true" CssClass="form-control" MaxLength="100" onKeypress="return ValidWord(event);" autocomplete="off"></asp:TextBox>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup></sup>
                                                                            <label>University/Institute with Country</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtUniversityPG" runat="server" TabIndex="2" Enabled="true" CssClass="form-control" MaxLength="100" onKeypress="return ValidWord(event);" autocomplete="off"></asp:TextBox>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup></sup>
                                                                            <label>Year of Award</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtQualificationAwardPG" runat="server" TabIndex="3" Enabled="true" CssClass="form-control" MaxLength="7" onKeypress="return CheckAdmbatch(event);" autocomplete="off"></asp:TextBox>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup></sup>
                                                                            <label>Main Specialty/ Field</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtSpecializationPG" runat="server" CssClass="form-control" Enabled="true" TabIndex="4" ToolTip="Please Enter Main Specialty/ Field" MaxLength="100" onKeypress="return ValidWord(event);" autocomplete="off"></asp:TextBox>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="div9" runat="server">
                                                                        <div class="label-dynamic">
                                                                            <sup></sup>
                                                                            <label>Class/GPA</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtGPAPG" runat="server" CssClass="form-control" Enabled="true" TabIndex="5" ToolTip="Please Enter Class/GPA" MaxLength="100" autocomplete="off"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-12" id="DivProfessional" runat="server">
                                                                <div class="row">
                                                                    <div class="col-12">
                                                                        <div class="sub-heading">
                                                                            <h5>Professional Details </h5>
                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divPhysices" runat="server">
                                                                        <div class="label-dynamic">
                                                                            <%--<sup>* </sup>--%>
                                                                            <label>Professional Qualification</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtProfessionalPG" runat="server" CssClass="form-control" Enabled="true" TabIndex="6" ToolTip="Please Enter Professional Qualification" MaxLength="100" onKeypress="return ValidWord(event);" autocomplete="off"></asp:TextBox>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="div10" runat="server">
                                                                        <div class="label-dynamic">
                                                                            <%--<sup>* </sup>--%>
                                                                            <label>University/Institute</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtProfessionalUniversityPG" runat="server" Enabled="true" CssClass="form-control" TabIndex="7" ToolTip="Please Enter Professional University/Institute" MaxLength="100" onKeypress="return ValidWord(event);" autocomplete="off"></asp:TextBox>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divChemistry" runat="server">
                                                                        <div class="label-dynamic">
                                                                            <%--<sup>* </sup>--%>
                                                                            <label>Qualification Award of Date</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtAwardDatePG" runat="server" CssClass="form-control dob" Enabled="true" TabIndex="8" ToolTip="Please Enter Professional Qualification Award of Date" MaxLength="10" onKeypress="return CheckAdmbatch(event);" autocomplete="off" placeholder="DD/MM/YYYY"></asp:TextBox>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="div11" runat="server">
                                                                        <div class="label-dynamic">
                                                                            <%--<sup>* </sup>--%>
                                                                            <label>Specilization of Qualification</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtSpecilizationQualificationPG" runat="server" Enabled="true" CssClass="form-control" TabIndex="9" ToolTip="Please Enter Professional Specilization of Qualification " MaxLength="100" onKeypress="return ValidWord(event);" autocomplete="off"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div>
                                                                <div class="pt-2">

                                                                    <div class="row">
                                                                        <div class="col-md-12">
                                                                            <asp:Panel ID="Panel9" runat="server">
                                                                                <asp:ListView ID="lvRefreesPG" runat="server">
                                                                                    <LayoutTemplate>
                                                                                        <div class="sub-heading">
                                                                                            <h5>Referees Details</h5>
                                                                                        </div>
                                                                                        <table class="table table-striped table-bordered nowrap" id="mytable" style="width: 100%">
                                                                                            <thead class="bg-light-blue">
                                                                                                <tr>
                                                                                                    <th style="text-align: center">Name
                                                                                                    </th>
                                                                                                    <th style="text-align: center">Position
                                                                                                    </th>
                                                                                                    <th style="text-align: center">Address
                                                                                                    </th>
                                                                                                    <th style="text-align: center">Contact info
                                                                                                    </th>
                                                                                                </tr>
                                                                                            </thead>
                                                                                            <tbody>
                                                                                                <tr id="itemPlaceholder" runat="server" />
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </LayoutTemplate>
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:Label ID="lblRefreesNamePG" runat="server" Text='<%# Eval("NAME")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label ID="lblRefreesPositionPG" runat="server" Text='<%# Eval("POSITION")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label ID="lblRefreesAddressPG" runat="server" Text='<%# Eval("ADDRESS")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label ID="lblRefreesContactinfoPG" runat="server" Text='<%# Eval("CONTACT")%>'></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:ListView>
                                                                            </asp:Panel>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="mt-5">

                                                                <div class="pt-2">

                                                                    <div class="row">
                                                                        <div class="col-md-12">
                                                                            <asp:Panel ID="Panel10" runat="server">
                                                                                <asp:ListView ID="lvEmploymentHistoryPG" runat="server">
                                                                                    <LayoutTemplate>
                                                                                        <div class="sub-heading">
                                                                                            <h5>Employment History</h5>
                                                                                        </div>
                                                                                        <table class="table table-striped table-bordered nowrap" id="mytable" style="width: 100%">
                                                                                            <thead class="bg-light-blue">
                                                                                                <tr>
                                                                                                    <th style="text-align: center">Duration
                                                                                                    </th>
                                                                                                    <th style="text-align: center">Position
                                                                                                    </th>
                                                                                                    <th style="text-align: center">Details
                                                                                                    </th>
                                                                                                </tr>
                                                                                            </thead>
                                                                                            <tbody>
                                                                                                <tr id="itemPlaceholder" runat="server" />
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </LayoutTemplate>
                                                                                    <ItemTemplate>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:Label ID="lblDurationPG" runat="server" Text='<%# Eval("DURATION")%>'></asp:Label>
                                                                                                <asp:HiddenField ID="hdfStartPG" runat="server" Value='<%#Eval("START_DURATION") %>' />
                                                                                                <asp:HiddenField ID="hdfendPG" runat="server" Value='<%#Eval("END_DURATION") %>' />
                                                                                            </td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label ID="lblPositionPG" runat="server" Text='<%# Eval("POSITION")%>'></asp:Label>
                                                                                            </td>
                                                                                            <td style="text-align: center">
                                                                                                <asp:Label ID="lblDetailsPG" runat="server" Text='<%# Eval("DETAILS")%>'></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ItemTemplate>
                                                                                </asp:ListView>
                                                                            </asp:Panel>
                                                                        </div>
                                                                        <%--<div class="col-sm-12">
                                        <div class="clearfix gray-strip mt-0">
                                            <div class="d-flex justify-content-between">
                                                <asp:Button ID="btnEmpClear" runat="server" TabIndex="25" Text="Back" CssClass="btn-shadow float-left btn btn-outline-primary" OnClick="btnCancel_Click" />
                                                <asp:Button ID="btnEmpAdd" runat="server" Text="Save & Continue" ValidationGroup="EmpExam" TabIndex="26" CssClass="btn btn-primary" OnClick="btnAddExam_Click" />
                                                <asp:ValidationSummary ID="ValidationSummary4" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="EmpExam" />
                                            </div>
                                        </div>
                                    </div>--%>

                                                                        <div class="col-sm-12 text-center">
                                                                            <asp:ValidationSummary ID="ValidationSummary17" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="regsubmit" />
                                                                            <div id="div40" runat="server">
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div id="divEducationDetailsPHD" runat="server">
                                                <div>
                                                    <asp:UpdateProgress ID="UpdateProgress13" runat="server" AssociatedUpdatePanelID="updEducationDetailsPHD"
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
                                                <asp:UpdatePanel ID="updEducationDetailsPHD" runat="server">
                                                    <ContentTemplate>
                                                        <div class="box-body">
                                                            <div class="col-12" id="Div18" runat="server">
                                                                <div class="row">
                                                                    <div class="col-12">
                                                                        <div class="sub-heading">
                                                                            <h5>Education Details</h5>
                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup></sup>
                                                                            <label>Name of Qualification </label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtNameofQualificationPHD" Enabled="true" runat="server" TabIndex="1" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="div20" runat="server">
                                                                        <div class="label-dynamic">
                                                                            <sup></sup>
                                                                            <label>Year of Award </label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtYearofAwardPHD" runat="server" Enabled="true" TabIndex="2" CssClass="form-control" MaxLength="4"></asp:TextBox>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="div21" runat="server">
                                                                        <div class="label-dynamic">
                                                                            <sup></sup>
                                                                            <label>University/Institute with Country</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtUniversityPHD" runat="server" Enabled="true" TabIndex="3" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="div22" runat="server">
                                                                        <div class="label-dynamic">
                                                                            <sup></sup>
                                                                            <label>Main Specialty/ Field </label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtMainSpecialtyPHD" runat="server" Enabled="true" CssClass="form-control" TabIndex="4" ToolTip="Please Enter Main Specialty/ Field" MaxLength="50"></asp:TextBox>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="div23" runat="server">
                                                                        <div class="label-dynamic">
                                                                            <sup></sup>
                                                                            <label>Class/GPA </label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtGPAPHD" runat="server" Enabled="true" CssClass="form-control" TabIndex="5" ToolTip="Please Enter Class/GPA" MaxLength="15"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-12" id="Div24" runat="server">
                                                                <div class="row">
                                                                    <div class="col-12">
                                                                        <div class="sub-heading">
                                                                            <h5>Other Qualifications (Fellowships, scholorships,awards, membership in professional bodies etc.)</h5>
                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="div25" runat="server">
                                                                        <div class="label-dynamic">
                                                                            <label>Name of Qualification </label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtNameQualificationPHD" runat="server" Enabled="true" CssClass="form-control" TabIndex="6" ToolTip="Please Enter Name of Qualification" MaxLength="50"></asp:TextBox>
                                                                        <asp:HiddenField ID="hdnDate" runat="server" />
                                                                        <asp:HiddenField ID="HiddenField3" runat="server" />
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="div26" runat="server">
                                                                        <div class="label-dynamic">
                                                                            <label>Awarding Institute </label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtAwardingUniversityPHD" runat="server" Enabled="true" CssClass="form-control" TabIndex="7" ToolTip="Please Enter Awarding Institute" MaxLength="50"></asp:TextBox>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="div27" runat="server">
                                                                        <div class="label-dynamic">
                                                                            <label>Date of Award </label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtAwardDatePHD" runat="server" Enabled="true" CssClass="form-control dob" TabIndex="8" ToolTip="Please Enter Date of Award" MaxLength="12" placeholder="DD/MM/YYYY" onchange="return DateValidation(this);"></asp:TextBox>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="div28" runat="server">
                                                                        <div class="label-dynamic">
                                                                            <label>Specialization (if any) </label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtSpecilizationQualificationPHD" Enabled="true" runat="server" CssClass="form-control" TabIndex="9" ToolTip="Please Enter Specialization (if any)" MaxLength="50"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-12">
                                                                <div class="row">
                                                                    <div class="col-12">
                                                                        <div class="sub-heading">
                                                                            <h5>Research Publication/Experience</h5>
                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="div29" runat="server">
                                                                        <div class="label-dynamic">
                                                                            <label>Description </label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtDescriptionPHD" runat="server" Enabled="true" CssClass="form-control" TabIndex="10" ToolTip="Please Enter Description" MaxLength="100"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-12">
                                                                <div class="row">
                                                                    <div class="col-12">
                                                                        <div class="sub-heading">
                                                                            <h5>Mode of Registration </h5>
                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="div30" runat="server">
                                                                        <div class="label-dynamic">
                                                                            <label>Mode of Registration </label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlModePHD" runat="server" Enabled="true" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="11">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                            <asp:ListItem Value="1">Full-time</asp:ListItem>
                                                                            <asp:ListItem Value="2">Part-time</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-12">
                                                                <div class="row">
                                                                    <div class="col-12">
                                                                        <div class="sub-heading">
                                                                            <h5>Other Information</h5>
                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <label>Have you applied for admission to this programme previously </label>
                                                                        </div>
                                                                        <asp:RadioButtonList ID="rdbQuestion1PHD" runat="server" Enabled="true" TabIndex="12" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdbQuestion1PHD_SelectedIndexChanged" AutoPostBack="true">
                                                                            <asp:ListItem Text="&nbsp;Yes&nbsp;" Value="1"></asp:ListItem>
                                                                            <asp:ListItem Text="&nbsp;No&nbsp;" Value="2"></asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divDetails" runat="server" visible="false">
                                                                        <div class="label-dynamic">
                                                                            <label>Give Details</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtQuestionDetailsPHD" runat="server" Enabled="true" CssClass="form-control" TabIndex="13" ToolTip="Please Enter First Question Give Details" MaxLength="30"></asp:TextBox>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <label>
                                                                                Are you currently registered for another Degree/Diploma at SLIIT or any other University/Institute
                                                                            </label>
                                                                        </div>
                                                                        <asp:RadioButtonList ID="rdbQuestion2PHD" runat="server" Enabled="true" TabIndex="14" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdbQuestion2PHD_SelectedIndexChanged" AutoPostBack="true">
                                                                            <asp:ListItem Text="&nbsp;Yes&nbsp;" Value="1"></asp:ListItem>
                                                                            <asp:ListItem Text="&nbsp;No&nbsp;" Value="2"></asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divDetails1" runat="server" visible="false">
                                                                        <div class="label-dynamic">
                                                                            <sup>*</sup>
                                                                            <label>Give Details</label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtQuestion1DetailsPHD" runat="server" Enabled="true" CssClass="form-control" TabIndex="15" ToolTip="Please Enter Second Question Give Details" MaxLength="30"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-12">
                                                                <div class="row">
                                                                    <div class="col-12">
                                                                        <div class="sub-heading">
                                                                            <h5>Referees (at least two should be academic referees who will be sending recommendations)</h5>
                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup></sup>
                                                                            <label>Name </label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtRefereesName" runat="server" Enabled="true" CssClass="form-control" TabIndex="16" ToolTip="Please Enter Referees Name" MaxLength="30"></asp:TextBox>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup></sup>
                                                                            <label>Position </label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtRefereesPosition" runat="server" Enabled="true" CssClass="form-control" TabIndex="17" ToolTip="Please Enter Referees Position" MaxLength="25"></asp:TextBox>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup></sup>
                                                                            <label>Address </label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtRefereesAddress" runat="server" Enabled="true" CssClass="form-control" TabIndex="18" ToolTip="Please Enter Referees Address" MaxLength="30"></asp:TextBox>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup></sup>
                                                                            <label>Contact info </label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtRefereesContactinfo" runat="server" Enabled="true" CssClass="form-control" TabIndex="19" ToolTip="Please Enter Referees Contact info" MaxLength="15"></asp:TextBox>
                                                                    </div>

                                                                    <div class="col-12">
                                                                        <asp:Panel ID="Panel6" runat="server">
                                                                            <asp:ListView ID="lvRefrees" runat="server">
                                                                                <LayoutTemplate>
                                                                                    <div class="sub-heading">
                                                                                        <h5>Referees Details</h5>
                                                                                    </div>
                                                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                                        <thead class="bg-light-blue">
                                                                                            <tr>
                                                                                                <th style="text-align: center">Edit
                                                                                                </th>
                                                                                                <th style="text-align: center">Delete
                                                                                                </th>
                                                                                                <th>Name
                                                                                                </th>
                                                                                                <th>Position
                                                                                                </th>
                                                                                                <th>Address
                                                                                                </th>
                                                                                                <th>Contact info
                                                                                                </th>
                                                                                            </tr>
                                                                                        </thead>
                                                                                        <tbody>
                                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                                        </tbody>
                                                                                    </table>
                                                                                </LayoutTemplate>
                                                                                <ItemTemplate>
                                                                                    <tr>
                                                                                        <td style="text-align: center">
                                                                                            <asp:LinkButton ID="lnkRefreesEdit" Enabled="false" runat="server" CssClass="fa fa-edit" ToolTip="Edit" CommandName='<%# Container.DataItemIndex + 1 %>' CommandArgument='<%#Eval("SRNO") %>'></asp:LinkButton>
                                                                                            <asp:HiddenField ID="hfdRefreesValue" runat="server" Value="0" />
                                                                                            <asp:HiddenField ID="hdfSrno" runat="server" Value='<%#Eval("SRNO") %>' />
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:LinkButton ID="lnkRefreesDelete" runat="server" CssClass="fa fa-trash" Style="color: red" ToolTip="Delete" CommandName='<%# Container.DataItemIndex + 1 %>' CommandArgument='<%#Eval("SRNO") %>'></asp:LinkButton>

                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblRefreesName" runat="server" Text='<%# Eval("NAME")%>'></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblRefreesPosition" runat="server" Text='<%# Eval("POSITION")%>'></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblRefreesAddress" runat="server" Text='<%# Eval("ADDRESS")%>'></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblRefreesContactinfo" runat="server" Text='<%# Eval("CONTACT")%>'></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                            </asp:ListView>
                                                                        </asp:Panel>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-12">
                                                                <div class="row">
                                                                    <div class="col-12">
                                                                        <div class="sub-heading">
                                                                            <h5>Employment History (Including present employment)</h5>
                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="div31" runat="server">
                                                                        <div class="label-dynamic">
                                                                            <label>Duration </label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtDurationPHD" runat="server" Enabled="true" CssClass="datePicker PickerDate form-control" TabIndex="21" ToolTip="Please Enter Duration"></asp:TextBox>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="div32" runat="server">
                                                                        <div class="label-dynamic">
                                                                            <label>Position </label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtPositionPHD" runat="server" Enabled="true" CssClass="form-control" TabIndex="22" ToolTip="Please Enter Position" MaxLength="30"></asp:TextBox>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="div33" runat="server">
                                                                        <div class="label-dynamic">
                                                                            <label>Name, Address & Contact Details of the Employer </label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtDetailsPHD" runat="server" Enabled="true" CssClass="form-control" TabIndex="23" ToolTip="Please Enter Name, Address & Contact Details of the Employer" MaxLength="60"></asp:TextBox>
                                                                    </div>

                                                                    <div class="col-12">
                                                                        <asp:Panel ID="Panel7" runat="server">
                                                                            <asp:ListView ID="lvEmploymentHistory" runat="server">
                                                                                <LayoutTemplate>
                                                                                    <div class="sub-heading">
                                                                                        <h5>Employment History</h5>
                                                                                    </div>
                                                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                                        <thead class="bg-light-blue">
                                                                                            <tr>
                                                                                                <th style="text-align: center">Edit
                                                                                                </th>
                                                                                                <th style="text-align: center">Delete
                                                                                                </th>
                                                                                                <th>Duration
                                                                                                </th>
                                                                                                <th>Position
                                                                                                </th>
                                                                                                <th>Details
                                                                                                </th>
                                                                                            </tr>
                                                                                        </thead>
                                                                                        <tbody>
                                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                                        </tbody>
                                                                                    </table>
                                                                                </LayoutTemplate>
                                                                                <ItemTemplate>
                                                                                    <tr>
                                                                                        <td style="text-align: center">
                                                                                            <asp:LinkButton ID="lnkEdit" Enabled="false" runat="server" CssClass="fa fa-edit" ToolTip="Edit" CommandName='<%# Container.DataItemIndex + 1 %>' CommandArgument='<%#Eval("SRNO") %>'></asp:LinkButton>
                                                                                            <asp:HiddenField ID="hfdValue" runat="server" Value="0" />
                                                                                            <asp:HiddenField ID="hdfSrNoEmp" runat="server" Value='<%#Eval("SRNO") %>' />
                                                                                        </td>
                                                                                        <td style="text-align: center">
                                                                                            <asp:LinkButton ID="lnkDelete" runat="server" CssClass="fa fa-trash" Style="color: red" ToolTip="Delete" CommandName='<%# Container.DataItemIndex + 1 %>' CommandArgument='<%#Eval("SRNO") %>'></asp:LinkButton>

                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblDuration" runat="server" Text='<%# Eval("DURATION")%>'></asp:Label>
                                                                                            <asp:HiddenField ID="hdfStart" runat="server" Value='<%#Eval("START_DURATION") %>' />
                                                                                            <asp:HiddenField ID="hdfend" runat="server" Value='<%#Eval("END_DURATION") %>' />
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblPosition" runat="server" Text='<%# Eval("POSITION")%>'></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblDetails" runat="server" Text='<%# Eval("DETAILS")%>'></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                            </asp:ListView>
                                                                        </asp:Panel>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div id="DivEducationDEtailsPDP" runat="server">
                                                <div>
                                                    <asp:UpdateProgress ID="UpdateProgress12" runat="server" AssociatedUpdatePanelID="updEducationDetailsPDP"
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
                                                <asp:UpdatePanel ID="updEducationDetailsPDP" runat="server">
                                                    <ContentTemplate>
                                                        <div class="box-body" id="pdp" runat="server">
                                                            <div class="col-12">
                                                                <div class="row">
                                                                    <div class="col-12">
                                                                        <div class="sub-heading">
                                                                            <h5>GCE Ordinary Level</h5>
                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup></sup>
                                                                            <label>O/L Syllabus </label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlALTypePDP" runat="server" Enabled="false" CssClass="form-control" data-select2-enable="true" TabIndex="1" ToolTip="Please Select O/L Syllabus" Width="100%" AppendDataBoundItems="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup></sup>
                                                                            <label>Index No. </label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtIndexNoPDP" runat="server" Enabled="false" CssClass="form-control" data-select2-enable="true" TabIndex="2" ToolTip="Please Enter Index No." MaxLength="50"></asp:TextBox>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup></sup>
                                                                            <label>Medium </label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlMediumPDP" runat="server" Enabled="false" CssClass="form-control" data-select2-enable="true" TabIndex="3" ToolTip="Please Select Medium" Width="100%" AppendDataBoundItems="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup></sup>
                                                                            <label>Attempt </label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlAttemptPDP" runat="server" Enabled="false" CssClass="form-control" data-select2-enable="true" TabIndex="4" ToolTip="Please Select Attempt" Width="100%" AppendDataBoundItems="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <sup></sup>
                                                                            <label>O/L Results </label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlALPassesPDP" runat="server" Enabled="false" CssClass="form-control" data-select2-enable="true" TabIndex="5" ToolTip="Please Select O/L Results" Width="100%" AppendDataBoundItems="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-12">
                                                                <div class="row">
                                                                    <div class="col-12">
                                                                        <div class="sub-heading">
                                                                            <h5>Diploma (Only Applicable for the Software Quality Assurance Program) </h5>
                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <label>Institute </label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtInstitutePDP" runat="server" Enabled="false" CssClass="form-control" TabIndex="6" ToolTip="Please Enter Institute" MaxLength="100"></asp:TextBox>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <label>Registration Number </label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtRegistrationNoPDP" runat="server" Enabled="false" CssClass="form-control" TabIndex="7" ToolTip="Please Enter Registration Number" MaxLength="50"></asp:TextBox>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <label>Name of the Programme </label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtNameoftheProgrammePDP" runat="server" Enabled="false" CssClass="form-control" TabIndex="8" ToolTip="Please Enter Name of the Programme" MaxLength="50"></asp:TextBox>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <label>Stream </label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlStreamPDP" runat="server" Enabled="false" CssClass="form-control" data-select2-enable="true" TabIndex="9" ToolTip="Please Select Stream" Width="100%" AppendDataBoundItems="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <label>Grade Point Average </label>
                                                                        </div>
                                                                        <asp:TextBox ID="txtGradePointAveragePDP" runat="server" Enabled="false" CssClass="form-control" TabIndex="10" ToolTip="Please Enter Grade Point Average " MaxLength="10"></asp:TextBox>
                                                                    </div>

                                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                                        <div class="label-dynamic">
                                                                            <label>Subject Results </label>
                                                                        </div>
                                                                        <asp:DropDownList ID="ddlSubjectResultPDP" runat="server" Enabled="false" CssClass="form-control" data-select2-enable="true" TabIndex="11" ToolTip="Please Select Subject Results"
                                                                            Width="100%" AppendDataBoundItems="true">
                                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="btnPersonalSubmit" runat="server" TabIndex="43" Text="Save & Next" CssClass="btn btn-outline-info" ValidationGroup="sub" OnClick="btnPersonalSubmit_Click" />
                                                <asp:ValidationSummary ID="ValidationSummary5" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="sub" />
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btnPersonalSubmit" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                                <div id="dipayment" role="tabpanel" runat="server" aria-labelledby="IntakeTransfer-tab" visible="false">
                                    <div>
                                        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updBulkReg"
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
                                    <asp:UpdatePanel ID="updBulkReg" runat="server">
                                        <ContentTemplate>
                                            <div id="divpayment" runat="server">
                                                <div class="box-body">
                                                    <div class="col-12">
                                                        <%--<div class="form-group col-lg-6 col-md-6 col-12" id="divShowPayment" runat="server">
                                                    <asp:Button ID="showOnlinePaymentDetails" runat="server" Text="Pay Now" class="btn btn-outline-info" OnClick="showOnlinePaymentDetails_Click" />
                                                </div>--%>
                                                    </div>
                                                    <div id="showpay" runat="server" visible="false">
                                                        <div class="col-12 btn-footer">
                                                            <asp:Label ID="lblpaid" runat="server" Style="font-weight: bold; color: red;"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div id="showunpay" runat="server" visible="false">
                                                        <div class="col-12 btn-footer">
                                                            <asp:Button ID="showOnlinePaymentDetails" runat="server" Text="View Details" class="btn btn-outline-info" OnClick="showOnlinePaymentDetails_Click" />
                                                        </div>
                                                    </div>

                                                    <div class="col-12" id="divViewpayment" runat="server" visible="false">
                                                        <asp:UpdatePanel ID="up" runat="server">
                                                            <ContentTemplate>

                                                                <asp:Panel ID="pnlStudentsFees" runat="server" Visible="false">
                                                                    <asp:ListView ID="lvStudentFees" runat="server" OnItemDataBound="lvStudentFees_ItemDataBound" OnPreRender="lvStudentFees_PreRender">
                                                                        <LayoutTemplate>
                                                                            <div>
                                                                                <div class="sub-heading">
                                                                                    <h5>Fees Details</h5>
                                                                                </div>
                                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tblHead">
                                                                                    <thead class="bg-light-blue">
                                                                                        <tr id="trRow">
                                                                                            <%--<th id="thHead" style="width: 5%"></th>--%>
                                                                                            <th>SrNo.
                                                                                            </th>
                                                                                            <th>Fees Head
                                                                                            </th>
                                                                                            <th>Amount
                                                                                            </th>

                                                                                        </tr>

                                                                                    </thead>
                                                                                    <tbody>
                                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                                    </tbody>
                                                                                    <%--<tfoot><asp:Label ID="lbltotal" runat="server"  Text="0"></asp:Label></tfoot>--%>
                                                                                    <thead class="bg-light-blue">
                                                                                        <tr id="Tr1" runat="server">
                                                                                            <th></th>
                                                                                            <th><span class="pull-right">Total Amount</span></th>

                                                                                            <th id="Td1" runat="server">
                                                                                                <asp:Label ID="lbltotal" CssClass="data_label" runat="server" Text="0"></asp:Label>
                                                                                            </th>
                                                                                        </tr>
                                                                                    </thead>
                                                                                </table>

                                                                            </div>
                                                                        </LayoutTemplate>
                                                                        <ItemTemplate>
                                                                            <tr>
                                                                                <%--<td style="width: 10%"></td>--%>
                                                                                <td><%# Eval("SRNO") %>
                                                                                </td>
                                                                                <td><%# Eval("FEE_LONGNAME") %></td>
                                                                                <td>
                                                                                    <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("AMOUNT") %>'></asp:Label></td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:ListView>

                                                                </asp:Panel>
                                                                <div class="col-lg-3 col-md-6 col-8" id="payoption" runat="server" visible="false">
                                                                    <div class="label-dynamic">
                                                                        <label for="RdPay"><sup>*</sup> Payment Option : </label>
                                                                    </div>
                                                                    <asp:RadioButtonList ID="rdPaymentOption" runat="server" TabIndex="6" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdPaymentOption_SelectedIndexChanged">
                                                                        <asp:ListItem Text="&nbsp;Offline Payment &nbsp;&nbsp;" Value="0" Selected="True"></asp:ListItem>
                                                                        <asp:ListItem Text="&nbsp;Online Payment" Value="1"></asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Select Payment Option"
                                                                 ControlToValidate="rdPaymentOption" Display="None" ValidationGroup="Summary"></asp:RequiredFieldValidator>--%>
                                                                </div>
                                                                <div class="col-12 btn-footer">
                                                                    <div class="row">
                                                                        <div class="col-12 col-md-2" id="divUploadgENChallan" runat="server">
                                                                            <asp:Button ID="btnGenerateChallan" runat="server" TabIndex="6" ValidationGroup="sub" Text="View Bank Details" CssClass="btn btn-outline-info" OnClick="btnGenerateChallan_Click" />
                                                                        </div>
                                                                        <div class="col-12 col-md-3" id="divUploadChallan" runat="server">
                                                                            <span id="myBtnDeposit" style="cursor: pointer" class="btn btn-outline-info" data-toggle="modal" data-target="#myModalChallan">Upload Deposit Slip</span>

                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-md-12" id="TRNote" runat="server" visible="false" style="display: none">

                                                                    <blockquote><span class="text-danger">**Note: If Installment or Fees Not Matched,Contact To Accounts department!</span></blockquote>

                                                                </div>
                                                                <div class="col-md-12" runat="server" id="TRAmount" visible="false">
                                                                    <li class="list-group-item">
                                                                        <b>Total Amount :</b>
                                                                        <a class="pull-right">
                                                                            <asp:Label ID="lblOrderID" runat="server" CssClass="data_label"></asp:Label>
                                                                            <asp:HiddenField ID="hdfAmount" runat="server" Value="0" />
                                                                            <asp:HiddenField ID="hdfServiceCharge" runat="server" Value="0" />
                                                                            <asp:HiddenField ID="hdfTotalAmount" runat="server" Value="0" />
                                                                        </a>
                                                                    </li>
                                                                </div>
                                                                <div class="col-md-12">
                                                                    <asp:Label ID="lblStatus" runat="server" CssClass="data_label"></asp:Label>
                                                                </div>

                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>

                                                    <div class="col-12 btn-footer" id="divShowPay" runat="server" visible="false">
                                                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-outline-info" Font-Bold="True" OnClick="btnSubmit_Click" Text="Pay" ValidationGroup="submit" Visible="false" />
                                                        <asp:Button ID="btnReport" runat="server" CssClass="btn btn-outline-primary" Enabled="false" Font-Bold="True" OnClick="btnReport_Click" Text="Print Challan" ValidationGroup="submit" Visible="false" />
                                                        <asp:Button ID="btnCancel" runat="server" CausesValidation="False" CssClass="btn btn-outline-danger" Font-Bold="True" OnClick="btnCancel_Click" Text="Cancel" Visible="false" />
                                                        <asp:ValidationSummary ID="SUMMARY" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="submit" />
                                                        <%--OnClientClick="javascript: return fnConfirm();"--%>
                                                    </div>

                                                    <div class="col-12">
                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                            <asp:Repeater ID="rpInstallment" runat="server">
                                                                <HeaderTemplate>
                                                                    <div class="sub-heading">
                                                                        <h5>Installment Details</h5>
                                                                    </div>
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>Installment No.</th>
                                                                            <th>Amount</th>
                                                                            <th>DueDate</th>
                                                                            <th>ExtraCharge</th>
                                                                            <th>Total Paid</th>
                                                                            <th>Opration</th>
                                                                            <th>Remark</th>
                                                                            <%--<th>Status</th>--%>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <%--<tr id="itemPlaceholder" runat="server" />--%>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <tr class="item">
                                                                        <td>
                                                                            <%# Container.ItemIndex + 1 %>
                                                                            <asp:Label ID="lblInstallmentno" runat="server" Text='<%# Eval("INSTALL_NO") %>' Visible="false"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("INSTALL_AMOUNT") %>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("DATE") %>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("ADDITIONAL_CHARGE_AMOUNT") %>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("TOTAL_AMT") %>
                                                                        </td>
                                                                        <td>
                                                                            <asp:LinkButton ID="lnkPay" runat="server" Visible="false" CommandName='<%# Eval("TOTAL_AMT") %>' CommandArgument='<%# Eval("INSTALL_NO") %>' CssClass="btn btn-outline-info" Text="Pay" Enabled='<%# Convert.ToInt32(Eval("DATE_STATUS")) == 1 ? false : true %>' ToolTip='<%# Eval("REMARK") %>' OnClick="lnkPay_Click"></asp:LinkButton>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("REMARK") %>'></asp:Label>
                                                                        </td>
                                                                        <%-- <td>
                                                                    <%# Eval("INSTALL_STATUS") %>
                                                                </td>--%>
                                                                    </tr>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    </tbody>
                                                                </FooterTemplate>
                                                            </asp:Repeater>
                                                        </table>

                                                    </div>
                                                     <div class="col-12">
                                                <div class="row" id="divScholarship" runat="server" visible="false">
                                                    <div class="col-12">
                                                        <div class="sub-heading">
                                                            <h5>Scholarship Details</h5>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-9">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>
                                                                Total Scholarship Amount
                                                            </label>
                                                        </div>
                                                        <asp:TextBox ID="txtScholarshipAmount" runat="server" Enabled="false" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="form-group col-lg-6 col-md-8 col-8">
                                                        <asp:CheckBox ID="chkConfirmPayment" runat="server" TabIndex="4" Style="margin-top: 2px" onclick="return CheckValue(this);" />
                                                        <label style="margin-left: 5px">
                                                            Check if you want to utilize the Scholarship amount
                                                        </label>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-8 d-none" id="divAmountSchol">
                                                        <div class="label-dynamic">
                                                            <sup></sup>
                                                            <label>
                                                                Enter Amount
                                                            </label>
                                                        </div>
                                                        <asp:TextBox ID="txtScholarshipAmountAdd" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator34" runat="server" ControlToValidate="txtScholarshipAmountAdd"
                                                            Display="None" ErrorMessage="Please Enter Amount" ValidationGroup="SubmitScholarship"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="col-12 btn-footer d-none" id="divSubmitScholarship">
                                                        <asp:Button ID="btnSubmitScholorship" runat="server" CssClass="btn btn-outline-info" Text="Submit" OnClick="btnSubmitScholorship_Click" ValidationGroup="SubmitScholarship" />
                                                        <asp:Label ID="lblremark" Text="" runat="server" Visible="false"></asp:Label>
                                                        <asp:ValidationSummary ID="ValidationSummary4" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="SubmitScholarship" />
                                                    </div>
                                                </div>
                                            </div>
                                                    <div class="col-12">
                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                            <asp:Repeater ID="lvPaidReceipts" runat="server" OnItemDataBound="lvPaidReceipts_ItemDataBound">
                                                                <HeaderTemplate>
                                                                    <div class="sub-heading">
                                                                        <h5>Previous Receipts Information</h5>
                                                                    </div>
                                                                    <thead class="bg-light-blue">
                                                                        <tr>

                                                                            <th>Receipt Type
                                                                            </th>
                                                                            <th>Receipt No
                                                                            </th>
                                                                            <th>Date
                                                                            </th>
                                                                            <th>Semester
                                                                            </th>
                                                                            <th>Pay Type
                                                                            </th>
                                                                            <th>Amount
                                                                            </th>
                                                                            <th>Payment Status
                                                                            </th>
                                                                            <th>Print
                                                                            </th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <%--<tr id="itemPlaceholder" runat="server" />--%>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <tr class="item">

                                                                        <td>
                                                                            <%# Eval("RECIEPT_TITLE") %>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("REC_NO") %>
                                                                        </td>
                                                                        <td>
                                                                            <%# (Eval("REC_DT").ToString() != string.Empty) ? ((DateTime)Eval("REC_DT")).ToShortDateString() : Eval("REC_DT") %>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("SEMESTERNAME") %>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("PAY_TYPE") %>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("TOTAL_AMT") %>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("PAY_STATUS") %>
                                                                        </td>
                                                                        <td>
                                                                            <asp:ImageButton ID="btnPrintReceipt" runat="server" OnClick="btnPrintReceipt_Click" Enabled='<%# (Convert.ToInt32(Eval("RECON")) == 0 ? false : true) %>'
                                                                                CommandArgument='<%# Eval("DCR_NO") %>' ImageUrl="~/images/print.gif" ToolTip='<%# Eval("RECIEPT_CODE")%>' CausesValidation="False" />
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    </tbody>
                                                                </FooterTemplate>
                                                            </asp:Repeater>
                                                        </table>

                                                    </div>
                                                </div>

                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btnSubmit" />
                                            <%--  <asp:PostBackTrigger ControlID="btnSubmit" />--%>
                                            <asp:PostBackTrigger ControlID="btnReport" />
                                            <asp:PostBackTrigger ControlID="btnCancel" />
                                            <asp:PostBackTrigger ControlID="btnShowDetails" />
                                        </Triggers>

                                    </asp:UpdatePanel>
                                </div>

                                <div id="divuploaddoc" runat="server" visible="false" role="tabpanel" aria-labelledby="IntakeAllotment-tab">
                                    <div id="document" runat="server" visible="false">
                                        <div class="box-body">
                                            <div class="col-12">

                                                <div class="row">
                                                    <div id="Div1" class="col-lg-4 col-md-6 col-12" runat="server" visible="false">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Enrollment No. :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblEnroll" Font-Bold="true" runat="server" />
                                                                </a>
                                                            </li>
                                                            <li class="list-group-item"><b>Mother Name :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblm" Font-Bold="true" runat="server" /></a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                    <div id="Div3" class="col-lg-4 col-md-6 col-12" runat="server" visible="false">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Student Name :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblName" Font-Bold="true" runat="server" />
                                                                </a>
                                                            </li>
                                                            <li class="list-group-item"><b>Mobile Number :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblmn" Font-Bold="true" runat="server" /></a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                    <div id="Div4" class="col-lg-4 col-md-6 col-12" runat="server" visible="false">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Email :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lble" Font-Bold="true" runat="server" />
                                                                </a>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-12 mt-3">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divDegreeVisible" runat="server" visible="false">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Degree </label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSchClg" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                        TabIndex="2" AutoPostBack="true" OnSelectedIndexChanged="ddlSchClg_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select Degree"
                                                        ControlToValidate="ddlSchClg" Display="None" InitialValue="0" ValidationGroup="degreeSub"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="col-lg-9">
                                                    <div class="row">
                                                        <%--<div class="col-12">
                                        <div class="sub-heading"><h5>Upload Documents</h5></div>
                                    </div>--%>
                                                        <div id="Div6" class="form-group col-lg-4 col-md-6 col-12" runat="server" visible="false">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                <label>
                                                                    <asp:Label ID="lblDoc" runat="server" Visible="false">Document List</asp:Label>
                                                                    <asp:Label ID="lblUndertaking" runat="server" Visible="false">Upload Undertaking
                                                                    </asp:Label>
                                                                </label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlDocument" runat="server" AppendDataBoundItems="true"
                                                                TabIndex="1" AutoPostBack="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlDocument_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">Select value</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvdocument" runat="server" ControlToValidate="ddlDocument"
                                                                ValidationGroup="submit" Display="None" SetFocusOnError="true" InitialValue="0"
                                                                ErrorMessage="Please Select Document">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                        <div id="Div36" class="form-group col-lg-4 col-md-6 col-12" runat="server" visible="false">

                                                            <div class="label-dynamic">
                                                                <label>
                                                                    <asp:Label ID="lblDocUpload" runat="server"></asp:Label></label>
                                                            </div>
                                                            <asp:FileUpload ID="fuDocument" CssClass="fuDocumentX" runat="server" TabIndex="2" onchange="setUploadButtonStatefEED()" />
                                                            <asp:Button ID="btnSaveAndContinue" runat="server" TabIndex="4" Text="UPLOAD" OnClick="btnSaveAndContinue_Click" CssClass="btn btn-outline-info mt-2" />
                                                        </div>

                                                        <div id="Div37" class="form-group col-lg-4 col-md-6 col-12" runat="server" visible="false">
                                                            <div class="label-dynamic">
                                                                <label>
                                                                    <asp:Label ID="lblPhotoUpload" runat="server"></asp:Label></label>
                                                            </div>
                                                            <div id="Div38" class="logoContainer" runat="server">
                                                                <img src="../IMAGES/default-fileupload.png" alt="upload image" runat="server" id="img2" tabindex="6" />
                                                            </div>
                                                            <div class="fileContainer sprite pl-1">
                                                                <span runat="server" id="Span1"
                                                                    cssclass="form-control" tabindex="7">Upload Applicant Photo</span>
                                                                <asp:FileUpload ID="photoupload" runat="server" ToolTip="Select file to upload"
                                                                    CssClass="photo" onkeypress="" onchange="setUploadButtonState()" />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Select Photo"
                                                                    ControlToValidate="photoupload" Display="None" InitialValue="0" ValidationGroup="degreeSub"></asp:RequiredFieldValidator>

                                                            </div>
                                                            <script>
                                                                $(document).ready(function () {
                                                                    $(document).on("click", ".logoContainer", function () {
                                                                        $("#ctl00_ContentPlaceHolder1_photoupload").click();
                                                                    });
                                                                    $(document).on("keydown", ".logoContainer", function () {
                                                                        if (event.keyCode === 13) {
                                                                            // Cancel the default action, if needed
                                                                            event.preventDefault();
                                                                            // Trigger the button element with a click
                                                                            $("#ctl00_ContentPlaceHolder1_photoupload").click();
                                                                        }
                                                                    });
                                                                });
                                                            </script>


                                                            <div class="fileContainer sprite pl-1">

                                                                <%--<asp:FileUpload ID="" CssClass="photo" runat="server" TabIndex="2"  />--%>
                                                                <%--  <asp:Button ID="btnphoto" runat="server" TabIndex="5" Text="PHOTO UPLOAD"  OnClick="btnphoto_Click" CssClass="btn btn-outline-info mt-2" />--%>

                                                                <asp:Image ID="ImgPhoto" Height="50px" Width="80px" runat="server" Visible="false" Style="display: none;" />

                                                            </div>

                                                        </div>

                                                        <div class="form-group col-lg-4 col-md-12 col-12 d-none">
                                                            <div class="note-div" id="docmsg" runat="server">
                                                                <h5 class="heading">Note</h5>
                                                                <p><i class="fa fa-star" aria-hidden="true"></i><span>Uploading of All Listed Documents Is Mandatory</span> </p>
                                                            </div>
                                                            <div class="note-div" id="docUnder" runat="server">
                                                                <h5 class="heading">Note</h5>
                                                                <p><i class="fa fa-star" aria-hidden="true"></i><span>Uploading of Undertaking Is Mandatory</span> </p>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-12 d-none">
                                            <div class="row">
                                                <div class="col-lg-4 col-md-6 col-12" id="divLastAdmittedPrograme" runat="server" visible="false">
                                                    <ul class="list-group list-group-unbordered">
                                                        <li class="list-group-item"><b>Last Admitted Programe :</b>
                                                            <a class="sub-label">
                                                                <asp:Label ID="lblAdmittedProgram1" runat="server" Font-Bold="True" />
                                                            </a>
                                                        </li>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group col-md-4 d-none" id="trDegree" runat="server" visible="false">
                                            <label>
                                                <span style="color: red;">*</span> Degree And College</label>
                                            <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                                Display="None" ErrorMessage="Please Select Degree And College" SetFocusOnError="true" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                            <asp:ListBox ID="ddlDegree" runat="server" AppendDataBoundItems="true" TabIndex="3"
                                                CssClass="form-control multi-select-demo" SelectionMode="multiple"
                                                AutoPostBack="true"></asp:ListBox>

                                            <asp:ListBox ID="ddldegreeclg" runat="server" AppendDataBoundItems="true" TabIndex="3"
                                                CssClass="form-control multi-select-demo" SelectionMode="multiple"
                                                AutoPostBack="true"></asp:ListBox>
                                        </div>

                                        <div class="form-group d-none">
                                            <asp:CheckBoxList ID="CheckBoxUndertaking" runat="server" OnSelectedIndexChanged="CheckBoxUndertaking_SelectedIndexChanged" AutoPostBack="true" Visible="false">
                                                <asp:ListItem Text="Upload Documents" Value="1" onclick="MutExChkList(this);" />

                                            </asp:CheckBoxList>
                                        </div>

                                        <div class="mt-2" id="updDocs" runat="server">

                                            <asp:UpdatePanel ID="upd" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <div class="row">
                                                    </div>
                                                    <div class="row">
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="btnSaveAndContinue" />
                                                    <asp:PostBackTrigger ControlID="btnSub" />
                                                </Triggers>
                                            </asp:UpdatePanel>

                                            <div class="col-12" id="divAllCoursesFromHist" runat="server">
                                                <asp:Panel ID="Panel1" runat="server">
                                                    <asp:ListView ID="lvDocument" runat="server">
                                                        <LayoutTemplate>
                                                            <div>
                                                                <div class="sub-heading">
                                                                    <h6 style="color: red; font-size: small">* Please be ready with a scanned copy of the documents listed below. Select “Choose File” to upload a scanned copy of the relevant document. </h6>
                                                                    <h6 style="color: red; font-size: small">* Download the Terms and Conditions and Indemnity Form by clicking “Download” button. Fill the details of the documents and upload. </h6>
                                                                    <h6 style="color: red; font-size: small">* Press “Submit” and “Next” after successful document upload.  </h6>
                                                                </div>

                                                                <div class="sub-heading">
                                                                    <h5>Document List</h5>
                                                                </div>
                                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="example">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>
                                                                            <th>Sr. No.</th>
                                                                            <th>Document Name
                                                                            </th>
                                                                            <th>Choose File
                                                                            </th>
                                                                            <th>Uploded
                                                                            </th>
                                                                            <th>View
                                                                            </th>
                                                                            <th>Default Template
                                                                            </th>
                                                                            <th>Upload Date
                                                                            </th>
                                                                            <th>Verify Status
                                                                            </th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <tr id="trCurRow">
                                                                <td>
                                                                    <%#Container.DataItemIndex + 1 %>
                                                                    <asp:Label ID="lblDocNo" runat="server" Text='<%#Eval("DOCUMENTNO") %>' Visible="false"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblname" runat="server" Text='<%#Eval("DOCUMENTNAME") %>' /><br />
                                                                    <asp:Label ID="lblImageFile" runat="server" Style="color: red"></asp:Label>
                                                                    <asp:Label ID="lblFileFormat" runat="server" Style="color: red"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:FileUpload ID="fuDocument" runat="server" CssClass="form-control" onchange="setUploadButtondoc(this)" TabIndex='<%#Container.DataItemIndex + 1 %>' />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lbluploadpic" runat="server" Visible="false"></asp:Label>
                                                                    <asp:Label ID="lbluploadpdf" runat="server" Visible="false"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:LinkButton ID="lnkViewDoc" runat="server" CssClass="btn btn-outline-info" CommandArgument='<%#Eval("DOCUMENTNO") %>' CommandName='<%#Eval("IDNO") %>' OnClick="lnkViewDoc_Click" Visible="false"><i class="fa fa-eye"></i> View</asp:LinkButton>
                                                                </td>
                                                                <td>
                                                                    <asp:LinkButton ID="lnkDocMappingDegree" runat="server" CssClass="btn btn-outline-info" CommandArgument='<%#Eval("DOCUMENTNO") %>' CommandName='<%#Eval("DEGREE_DOC") %>' OnClick="lnkDocMappingDegree_Click" Visible='<%#Eval("DEGREE_DOC").ToString() == string.Empty ? false : true %>'><i class="fa fa-eye"></i> Download</asp:LinkButton>
                                                                </td>
                                                                <td>

                                                                    <asp:Label ID="lblUploadDate" runat="server" />
                                                                </td>
                                                                <td>

                                                                    <asp:Label ID="lblVerifyDocument" runat="server" />
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>

                                                </asp:Panel>
                                            </div>
                                            <div class="col-12">
                                                <asp:Label ID="lblfile" runat="server" Style="color: red"></asp:Label>
                                            </div>
                                            <div id="Div39" class="col-12 mt-4" runat="server" visible="false">
                                                <div class="row">
                                                    <div class="col-lg-4 col-md-6 col-12">
                                                        <asp:CheckBoxList ID="CheckUndertaking" runat="server" OnSelectedIndexChanged="CheckUndertaking_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Text=" Terms and Conditions" Value="2" />
                                                            <%--Text="Undertaking"--%>
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-12 btn-footer">

                                                <asp:Button ID="btnSub" runat="Server" Text="SUBMIT" OnClick="btnSub_Click" ValidationGroup="degreeSub"
                                                    CssClass="btn btn-outline-info" />
                                                <asp:Button ID="btnNextDoc" runat="server" Text="Next" CssClass="btn btn-outline-info" OnClick="btnNextDoc_Click" />
                                                <%--   <asp:LinkButton runat="server" ID="lnkCancelCourse" Font-Bold="true" Text="Cancel Course"
                                OnClick="lnkCancelCourse_Click" ForeColor="Blue"></asp:LinkButton>--%>
                                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                    ShowSummary="false" ValidationGroup="degreeSub" />
                                            </div>

                                            <div id="modalpopup11" class="modal fade" role="dialog">
                                                <asp:Panel ID="pnlOTP" runat="server">
                                                    <div class="modal-dialog">
                                                        <!-- Modal content-->
                                                        <div class="modal-content">
                                                            <asp:UpdatePanel ID="updModel" runat="server">
                                                                <ContentTemplate>
                                                                    <div class="modal-body">

                                                                        <div class="" runat="server" id="undertaking" visible="false">
                                                                            <div class="form-group col-lg-12 col-md-12 col-12">
                                                                                <div class="note-div">
                                                                                    <p><i class="fa fa-star" aria-hidden="true"></i><span>I certify that all the information given and documents uploaded are true to the best of my knowledge.</span> </p>
                                                                                    <p><i class="fa fa-star" aria-hidden="true"></i><span>Furthermore, I undertake to produce originals of all the documents on demand, failing which my admission is liable to be treated as cancelled.</span> </p>
                                                                                    <p>
                                                                                        <i class="fa fa-star" aria-hidden="true"></i><span>I hereby agree, if admitted, to abide by the rules and regulations in force, or that may hereafter be made for the administration of the college. 
                                                            I undertake that so long as I am student of the college, I will do nothing either inside or outside the college, that will interfere with its orderly working, discipline and anti-ragging policy. 
                                                            Passing and marks certificates of University Degree Examination / Board Examination / Transfer Certificate from the previous college or university / School leaving certificate / final eligibility certificate of the previous University, as the case may be, will be produced on demand. 
                                                            Otherwise, I am aware the admission shall be provisional or even cancelled. If my admission is cancelled as per the rules and regulations of the college, I shall not have any grievance whatsoever. 
                                                            I hereby undertake to attend minimum 75% of lectures in the college. I am aware that if I fail to do so, my term will not be granted.
                                                                                        </span>
                                                                                    </p>
                                                                                </div>
                                                                            </div>
                                                                            <div id="Div41" runat="server">
                                                                                <button type="button" class="close btn btn-outline-danger" data-dismiss="modal" title="Close">Close</button>
                                                                                <%-- <asp:Button ID="OKButton" runat="server" Text="Close" CssClass="btn btn-outline-danger"/>--%>
                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                                </ContentTemplate>

                                                            </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </div>

                                        </div>
                                    </div>

                                </div>

                                <div id="divModuleRegistration" role="tabpanel" runat="server" aria-labelledby="IntakeTransfer-tab" visible="false">
                                    <div>
                                        <asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="updModuleRegistration"
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
                                    <asp:UpdatePanel ID="updModuleRegistration" runat="server">
                                        <ContentTemplate>
                                            <div id="div42" runat="server">
                                                <div class="box-body">
                                                    <div class="col-12">

                                                        <asp:Panel ID="Panel2" runat="server" Visible="false">
                                                            <asp:ListView ID="lvOfferedSubject" runat="server">
                                                                <LayoutTemplate>
                                                                    <div>
                                                                        <div class="sub-heading">
                                                                            <h5>Regular Modules</h5>
                                                                        </div>
                                                                        <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                                            <thead class="bg-light-blue">
                                                                                <tr id="trRow">
                                                                                    <%--<th id="thHead" style="width: 5%"></th>--%>
                                                                                    <th></th>
                                                                                    <th>Module Code
                                                                                    </th>
                                                                                    <th>Module Name
                                                                                    </th>
                                                                                    <th>Module Type
                                                                                    </th>
                                                                                    <th>Credits
                                                                                    </th>
                                                                                    <th>LIC Name
                                                                                    </th>
                                                                                </tr>

                                                                            </thead>
                                                                            <tbody>
                                                                                <tr id="itemPlaceholder" runat="server" />
                                                                            </tbody>
                                                                        </table>

                                                                    </div>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:CheckBox ID="chkRows" runat="server" Checked="true" Enabled="false" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>'></asp:Label>
                                                                            <asp:HiddenField ID="hdfCourseNo" runat="server" Value='<%# Eval("COURSENO") %>' />
                                                                            <asp:HiddenField ID="hdfSubid" runat="server" Value='<%# Eval("SUBID") %>' />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>'></asp:Label></td>
                                                                        <td><%# Eval("SUBNAME") %></td>
                                                                        <td>
                                                                            <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>'></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="lblLicName" runat="server" Text='<%# Eval("UA_FULLNAME") %>'></asp:Label>
                                                                            <asp:Label ID="lbluano" runat="server" Text='<%# Eval("MODULELIC") %>' Visible="false"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>

                                                        </asp:Panel>

                                                        <asp:Panel ID="Panel3" runat="server" Visible="false">
                                                            <asp:ListView ID="lvcoursetwo" runat="server">
                                                                <LayoutTemplate>
                                                                    <div>
                                                                        <div class="sub-heading">
                                                                            <h5>Orientation Modules</h5>
                                                                        </div>
                                                                        <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                                            <thead class="bg-light-blue">
                                                                                <tr id="trRow">
                                                                                    <%--<th id="thHead" style="width: 5%"></th>--%>
                                                                                    <th></th>
                                                                                    <th>Module Code
                                                                                    </th>
                                                                                    <th>Module Name
                                                                                    </th>
                                                                                    <th>Module Type
                                                                                    </th>
                                                                                    <th>Credits
                                                                                    </th>
                                                                                    <th>LIC Name
                                                                                    </th>
                                                                                </tr>

                                                                            </thead>
                                                                            <tbody>
                                                                                <tr id="itemPlaceholder" runat="server" />
                                                                            </tbody>
                                                                        </table>

                                                                    </div>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:CheckBox ID="chkRows" runat="server" Checked="true" Enabled="false" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>'></asp:Label>
                                                                            <asp:HiddenField ID="hdfCourseNo" runat="server" Value='<%# Eval("COURSENO") %>' />
                                                                            <asp:HiddenField ID="hdfSubid" runat="server" Value='<%# Eval("SUBID") %>' />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>'></asp:Label></td>
                                                                        <td><%# Eval("SUBNAME") %></td>
                                                                        <td>
                                                                            <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>'></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="lblLicName" runat="server" Text='<%# Eval("UA_FULLNAME") %>'></asp:Label>
                                                                            <asp:Label ID="lbluano" runat="server" Text='<%# Eval("MODULELIC") %>' Visible="false"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>

                                                        </asp:Panel>
                                                        <asp:Panel ID="Panel4" runat="server" Visible="false">
                                                            <asp:ListView ID="lvcoursethree" runat="server">
                                                                <LayoutTemplate>
                                                                    <div>
                                                                        <div class="sub-heading">
                                                                            <h5>Bridging Modules</h5>
                                                                        </div>
                                                                        <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                                            <thead class="bg-light-blue">
                                                                                <tr id="trRow">
                                                                                    <%--<th id="thHead" style="width: 5%"></th>--%>
                                                                                    <th></th>
                                                                                    <th>Module Code
                                                                                    </th>
                                                                                    <th>Module Name
                                                                                    </th>
                                                                                    <th>Module Type
                                                                                    </th>
                                                                                    <th>Credits
                                                                                    </th>
                                                                                    <th>LIC Name
                                                                                    </th>
                                                                                </tr>

                                                                            </thead>
                                                                            <tbody>
                                                                                <tr id="itemPlaceholder" runat="server" />
                                                                            </tbody>
                                                                        </table>

                                                                    </div>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:CheckBox ID="chkRows" runat="server" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>'></asp:Label>
                                                                            <asp:HiddenField ID="hdfCourseNo" runat="server" Value='<%# Eval("COURSENO") %>' />
                                                                            <asp:HiddenField ID="hdfSubid" runat="server" Value='<%# Eval("SUBID") %>' />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>'></asp:Label></td>
                                                                        <td><%# Eval("SUBNAME") %></td>
                                                                        <td>
                                                                            <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>'></asp:Label></td>
                                                                        <td>
                                                                            <asp:Label ID="lblLicName" runat="server" Text='<%# Eval("UA_FULLNAME") %>'></asp:Label>
                                                                            <asp:Label ID="lbluano" runat="server" Text='<%# Eval("MODULELIC") %>' Visible="false"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>

                                                        </asp:Panel>
                                                    </div>


                                                    <div class="col-12 btn-footer">
                                                        <asp:Button ID="btnSubmitOffer" runat="server" Text="Submit & Next" CssClass="btn btn-outline-info" OnClick="btnSubmitOffer_Click" />
                                                    </div>
                                                </div>

                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btnSubmitOffer" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>

                                <div id="divstatus" runat="server" visible="false" role="tabpanel" aria-labelledby="HallTicket-tab">
                                    <div>
                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updBulkReg"
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

                                    <%-- <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                <ContentTemplate>
                                    <div id="div1" runat="server">
                                        <div class="box-body">
                                            <div class="col-12 mb-3">
                                                <div class="row">
                                                    <div class="col-lg-3 col-md-6 col-12">
                                                        <div id="divlbsta" runat="server">
                                                            <ul class="list-group list-group-unbordered">
                                                                <li class="list-group-item"><b>Status :</b>
                                                                    <a class="sub-label">
                                                                        <asp:Label ID="lblstatuspayment" Font-Bold="true" runat="server" />
                                                                    </a>
                                                                </li>
                                                            </ul>
                                                        </div>
                                                    </div>
                                                </div>

                                                 <%--add by aashna
                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="rptstatus" runat="server" Text="Report" CssClass="btn btn-outline-info" OnClick="rptstatus_Click" />
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="rptstatus" />
                                </Triggers>
                            </asp:UpdatePanel>--%>
                                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                        <ContentTemplate>
                                            <div id="div43" runat="server">
                                                <div class="box-body">
                                                    <div class="col-12 mb-3" id="divlbsta" runat="server">
                                                        <div class="row">

                                                            <div class="col-lg-4 col-md-6 col-12">
                                                                <ul class="list-group list-group-unbordered">
                                                                    <li class="list-group-item"><b>Status :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblstatuspayment" Font-Bold="true" runat="server" />
                                                                        </a>
                                                                    </li>

                                                                    <li class="list-group-item"><b>Intake :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblIntake" Font-Bold="true" runat="server" />
                                                                        </a>
                                                                    </li>

                                                                    <li class="list-group-item"><b>Student Registration No:</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblEnrollmentno" Font-Bold="true" runat="server" />
                                                                        </a>
                                                                    </li>

                                                                    <li class="list-group-item"><b>Name with Initial:</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblNamewithInitial" Font-Bold="true" runat="server" />
                                                                        </a>
                                                                    </li>

                                                                    <li class="list-group-item"><b>Name in Full:</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblFullName" Font-Bold="true" runat="server" />
                                                                        </a>
                                                                    </li>
                                                                </ul>
                                                            </div>
                                                            <div class="col-lg-4 col-md-6 col-12">
                                                                <ul class="list-group list-group-unbordered">
                                                                    <li class="list-group-item"><b>NIC / Passport:</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblnicpass" Font-Bold="true" runat="server" />
                                                                        </a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>Address:</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lbladdress" Font-Bold="true" runat="server" />
                                                                        </a>
                                                                    </li>

                                                                    <li class="list-group-item"><b>Contact No.:</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblcontactno" Font-Bold="true" runat="server" />
                                                                        </a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>Email:</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblstudemail" Font-Bold="true" runat="server" />
                                                                        </a>
                                                                    </li>
                                                                </ul>
                                                            </div>
                                                            <div class="col-lg-4 col-md-6 col-12">
                                                                <ul class="list-group list-group-unbordered">
                                                                    <%--<li class="list-group-item"><b>SLIIT Email:</b>
                                                                    <a class="sub-label">
                                                                        <asp:Label ID="lblsliitemail" Font-Bold="true" runat="server" />
                                                                    </a>
                                                                </li>--%>

                                                                    <li class="list-group-item"><b>Programme:</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblprogram" Font-Bold="true" runat="server" />
                                                                        </a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>Campus:</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblcampus" Font-Bold="true" runat="server" />
                                                                        </a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>Batch:</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblweekbatch" Font-Bold="true" runat="server" />
                                                                        </a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>Date Of Registration:</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lbldateofreg" Font-Bold="true" runat="server" />
                                                                        </a>
                                                                    </li>
                                                                    <%-- <li class="list-group-item"><b>Orientation Group:</b>
                                                                    <a class="sub-label">
                                                                        <asp:Label ID="lblorigp" Font-Bold="true" runat="server" />
                                                                    </a>
                                                                </li>--%>
                                                                </ul>
                                                            </div>
                                                        </div>
                                                        <%--add by aashna--%>
                                                        <div class="col-12 btn-footer">
                                                            <asp:Button ID="rptstatus" runat="server" Text="Report" CssClass="btn btn-outline-info" OnClick="rptstatus_Click" />

                                                            <asp:Button ID="btnOfferLetter" runat="server" Text="Offer Letter" CssClass="btn btn-outline-info" OnClick="btnOfferLetter_Click" />

                                                            <asp:Button ID="btnSummarySheet" runat="server" Text="Summary Sheet" Visible="false" CssClass="btn btn-outline-info" OnClick="btnSummarySheet_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="rptstatus" />
                                        </Triggers>
                                    </asp:UpdatePanel>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="myModal22" class="modal fade" role="dialog" data-backdrop="static">
        <div class="modal-dialog modal-lg">
            <div class="modal-content" style="margin-top: -25px">
                <div class="modal-body">
                    <div class="modal-header">
                        <asp:UpdatePanel ID="updClose" runat="server">
                            <ContentTemplate>
                                <asp:LinkButton ID="lnkClose" runat="server" CssClass="close" Style="margin-top: -18px" OnClick="lnkClose_Click">x</asp:LinkButton>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="lnkClose" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <%--<button type="button" class="close" data-dismiss="modal" style="margin-top: -18px">x</button>--%>
                    </div>

                    <asp:Image ID="ImageViewer" runat="server" Width="100%" Height="500px" Visible="false" />
                    <asp:Literal ID="ltEmbed" runat="server" Visible="false" />
                    <%--  <iframe id="iframe1" runat="server" frameborder="0" width="100%" height="800px" visible="false"></iframe>--%>
                    <%--<iframe id="iframe1" runat="server" src="../FILEUPLOAD/Transcript Report.pdf" frameborder="0" width="100%" height="547px"></iframe>--%>
                </div>
            </div>
        </div>
    </div>
    <div id="myModalChallan" class="modal" role="dialog">
        <div class="modal-dialog model-lg">
            <!-- Modal content-->

            <asp:UpdatePanel ID="updChallan" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4>Upload Deposit</h4>
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-xl-12 col-md-12" id="divTransactionid" runat="server" visible="false">
                                    <div class="md-form">
                                        <label for="UserName"><sup></sup>Transaction Id</label>
                                        <asp:TextBox ID="txtTransactionNo" TabIndex="1" runat="server" MaxLength="20" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please Enter Challan Transaction Id"
                                            ControlToValidate="txtTransactionNo" Display="None" ValidationGroup="SubmitChallan"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-xl-12 col-md-12" id="DivOrderId" runat="server" visible="false">
                                    <div class="md-form">
                                        <label for="UserName"><sup></sup>Challan Order ID </label>
                                        <asp:TextBox ID="txtChallanId" runat="server" MaxLength="20" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please Enter Challan Order ID"
                                            ControlToValidate="txtChallanId" Display="None" ValidationGroup="SubmitChallan"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-xl-12 col-md-12">
                                    <div class="md-form">
                                        <label for="UserName"><sup></sup>Bank</label>
                                        <asp:DropDownList ID="ddlbank" runat="server" AppendDataBoundItems="true" CssClass="form-control select2 select-click" TabIndex="2">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Please Select Bank Name"
                                            ControlToValidate="ddlbank" InitialValue="0" Display="None" ValidationGroup="SubmitChallan"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-xl-12 col-md-12">
                                    <div class="md-form">
                                        <label for="UserName"><sup></sup>Branch</label>
                                        <asp:TextBox ID="txtBranchName" runat="server" CssClass="form-control" TabIndex="3" MaxLength="50"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Please Enter Bank Branch"
                                            ControlToValidate="txtBranchName" InitialValue="0" Display="None" ValidationGroup="SubmitChallan"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-xl-12 col-md-12">
                                    <div class="md-form">
                                        <label for="UserName"><sup></sup>Amount</label>
                                        <asp:TextBox ID="txtchallanAmount" TabIndex="4" runat="server" MaxLength="10" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Please Enter Amount"
                                            ControlToValidate="txtchallanAmount" Display="None" ValidationGroup="SubmitChallan"></asp:RequiredFieldValidator>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtchallanAmount" ValidChars="1234567890." FilterMode="ValidChars" />
                                    </div>
                                </div>
                                <div class="col-xl-12 col-md-12">
                                    <div class="md-form">
                                        <label for="UserName"><sup></sup>Date of Payment</label>
                                        <asp:TextBox ID="txtPaymentdate" TabIndex="5" runat="server" MaxLength="20" CssClass="form-control PaymentDate"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Please Enter Date of Payment"
                                            ControlToValidate="txtPaymentdate" Display="None" ValidationGroup="SubmitChallan"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-xl-12 col-md-12">
                                    <div class="md-form">
                                        <label for="UserName"><sup></sup>Upload Deposit Slip</label><br />
                                        <asp:FileUpload ID="FuChallan" runat="server" onchange="setUploadButtonState();" Style="margin-top: 8px;" TabIndex="6" CssClass="form-control" /><br />
                                        <span style="color: red; font-size: small">
                                            <asp:Label ID="lblMsg" runat="server" Text="Image Size Not Greater Than 1MB and image format JPG,JPEG,PNG,PDF Allowed"></asp:Label>
                                        </span>
                                    </div>
                                </div>
                                <div class="col-12 text-center">
                                    <asp:Button ID="btnChallanSubmit" runat="server" TabIndex="7" Text="Submit" CssClass="btn btn-outline-info" ValidationGroup="SubmitChallan" OnClick="btnChallanSubmit_Click" />
                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="SubmitChallan" />
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-12 mb-3">
                                    <asp:ListView ID="lvDepositSlip" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Deposit Slip Detail</h5>
                                            </div>
                                            <div class="table table-responsive">
                                                <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <td>Action</td>
                                                            <td>Bank Name</td>
                                                            <td>Bank Branch</td>
                                                            <td>Amount</td>
                                                            <td>Date of Payment</td>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <%-- <td><%# Container.DataItemIndex + 1 %></td>--%>


                                                <%--<td style="text-align: center;">
                                                <asp:ImageButton ID="btnDel" runat="server" AlternateText="Delete Record" 
                                                CommandArgument='<%# Eval("TEMP_DCR_NO") %>' ImageUrl="~/IMAGES/delete.gif" 
                                                OnClick="btnDel_Click" OnClientClick="return UserDeleteConfirmation();" TabIndex="6" 
                                                ToolTip='<%# Eval("TEMP_DCR_NO") %>' />
                                                </td>--%>

                                                <td>
                                                    <asp:ImageButton ID="btnedit" runat="server" ImageUrl="~/IMAGES/edit.png" CommandArgument='<%# Eval("TEMP_DCR_NO") %>'
                                                        AlternateText="Delete Record" OnClick="btnedit_Click"
                                                        TabIndex="14" ToolTip="Edit" />
                                                </td>


                                                <td><%# Eval("BANK_NAME") %></td>
                                                <td><%# Eval("BRANCH_NAME") %></td>
                                                <td><%# Eval("TOTAL_AMT") %></td>
                                                <td><%# Eval("REC_DT") %></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>

                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnChallanSubmit" />
                    <asp:AsyncPostBackTrigger ControlID="ddlbank" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="col-md-12">
        <div id="divMsg" runat="server">
        </div>
        <div id="myModal33" class="modal fade" role="dialog" data-backdrop="static">
            <div class="modal-dialog modal-lg">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4>Online Payment</h4>
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-xl-6 col-md-6">
                                <div class="md-form">
                                    <label for="UserName"><sup></sup>Order ID </label>
                                    <asp:TextBox ID="txtOrderid" TabIndex="1" runat="server" MaxLength="15" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                            </div>


                            <div class="col-xl-6 col-md-6">
                                <div class="md-form">
                                    <label for="UserName"><sup></sup>Total to be Paid</label>
                                    <asp:TextBox ID="txtTotalPayAmount" TabIndex="4" runat="server" MaxLength="15" CssClass="form-control" Enabled="false" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-xl-6 col-md-6">
                                <div class="md-form">
                                    <label for="UserName"><sup></sup>Service Charge</label>
                                    <asp:TextBox ID="txtServiceCharge" TabIndex="3" runat="server" MaxLength="15" CssClass="form-control" Enabled="false" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-xl-6 col-md-6">
                                <div class="md-form">
                                    <label for="UserName"><sup></sup>Amount to be Paid </label>
                                    <asp:TextBox ID="txtAmountPaid" TabIndex="2" runat="server" MaxLength="15" CssClass="form-control" Enabled="false"></asp:TextBox>
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
        </div>
    </div>
    <div id="modelBank" class="modal fade" role="dialog">
        <div class="modal-dialog modal-xl">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <h4>Bank Details</h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-12 mb-3">
                            <asp:ListView ID="lvBankDetails" runat="server">
                                <LayoutTemplate>
                                    <div class="sub-heading">
                                        <h5>Bank Details List</h5>
                                    </div>
                                    <div class="table table-responsive">
                                        <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <td>SrNo</td>
                                                    <td>Bank Code</td>
                                                    <td>Bank Name</td>
                                                    <td>Bank Branch</td>
                                                    <td>Bank Account No.</td>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                        </table>
                                    </div>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td><%# Container.DataItemIndex + 1 %></td>
                                        <td><%# Eval("BANKCODE") %></td>
                                        <td><%# Eval("BANKNAME") %></td>
                                        <td><%# Eval("BANKADDR") %></td>
                                        <td><%# Eval("ACCOUNT_NO") %></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="myModal2" class="modal fade" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <h4>Search</h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <asp:UpdatePanel ID="updEdit" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">

                            <div class="form-group col-md-12">
                                <label>Search Criteria</label>
                                <br />
                                <asp:RadioButtonList ID="rdselect" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="0">Name</asp:ListItem>
                                    <%--  <asp:ListItem Value="1">IdNo</asp:ListItem>--%>
                                    <asp:ListItem Value="1">Email</asp:ListItem>
                                    <asp:ListItem Value="2">Application No</asp:ListItem>
                                </asp:RadioButtonList>
                                <%-- <asp:RadioButton ID="rbName" runat="server" Text="Name" GroupName="edit" />
                                <asp:RadioButton ID="rbIdNo" runat="server" Text="IdNo" GroupName="edit" />
                                <asp:RadioButton ID="rbSemester" runat="server" Text="Semester" GroupName="edit" />
                                <asp:RadioButton ID="rbRegNo" runat="server" Text="Rollno" GroupName="edit" Checked="True" />--%>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ErrorMessage="Please select Category"
                                    ControlToValidate="rdselect" Display="None" ValidationGroup="search"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-md-12">
                                <div class="col-md-6">
                                    <label>Search String</label>
                                    <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ErrorMessage="Please Enter Deatils"
                                        ControlToValidate="txtSearch" Display="None" ValidationGroup="search"></asp:RequiredFieldValidator>
                                </div>

                            </div>
                            <div class="col-md-12">
                                <p class="text-center">
                                    <%--<asp:Button ID="btnSearch" runat="server" Text="Search" OnClientClick="return submitPopup(this.name);" CssClass="btn btn-outline-info" />--%>
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-outline-info" ValidationGroup="search" />

                                    <asp:Button ID="btnCancelModal" runat="server" Text="Cancel" OnClick="btnCancelModal_Click" CssClass="btn btn-outline-danger" />
                                    <button type="button" class="btn btn-outline-danger" data-dismiss="modal">Close</button>
                                    <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="search" />
                                    <div>
                                        <asp:Label ID="lblNoRecords" runat="server" SkinID="lblmsg" />
                                    </div>
                                    <div>
                                        <asp:UpdateProgress ID="updProgress" runat="server" AssociatedUpdatePanelID="updEdit">
                                            <ProgressTemplate>
                                                <asp:Image ID="imgProg" runat="server" ImageUrl="~/images/ajax-loader.gif" />
                                                Loading.. Please Wait!
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </div>
                                    <div class="col-md-12">
                                        <asp:Panel ID="Panel5" runat="server" ScrollBars="Auto">
                                            <asp:ListView ID="lvStudent" runat="server">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Login Details</h5>
                                                    </div>
                                                    <%--   <h4>Login Details</h4>--%>
                                                    <asp:Panel ID="Panel2" runat="server" Height="300px" ScrollBars="Auto">
                                                        <table class="table table-hover table-bordered">
                                                            <thead>
                                                                <tr class="bg-light-blue">
                                                                    <th>Student Name </th>
                                                                    <%--  <th>IdNo
                                                                </th>--%>
                                                                    <th>Application No. </th>
                                                                    <th>Specialization </th>
                                                                    <th>Semester </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </tbody>
                                                        </table>
                                                    </asp:Panel>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:LinkButton ID="lnkId" runat="server" CommandArgument='<%# Eval("IDNo") %>' OnClick="lnkId_Click" Text='<%# Eval("Name") %>' ToolTip='<%# Eval("USERNO")%>'></asp:LinkButton>
                                                        </td>
                                                        <%--<td>
                                                        <%# Eval("idno")%>
                                                    </td>--%>
                                                        <td>
                                                            <asp:Label ID="lblstuenrollno" runat="server" Text='<%# Eval("REGNO")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>
                                                            <%-- <asp:Label ID="lblstuenrollno" runat="server" Text='<%# Eval("EnrollmentNo")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>--%></td>
                                                        <td>
                                                            <asp:Label ID="lblstudentfullname" runat="server" Text='<%# Eval("longname")%>' ToolTip='<%# Eval("USERNO")%>'></asp:Label>
                                                        </td>
                                                        <td><%# Eval("SEMESTERNAME")%></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>
                                </p>

                            </div>

                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="lvStudent" />
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" />

                        <%--<asp:AsyncPostBackTrigger ControlID="lnkId"  />--%>

                        <%--<asp:AsyncPostBackTrigger ControlID="lvStudent"  />--%>

                        <%-- <asp:PostBackTrigger ControlID="btnSearch" />--%>
                        <%--<asp:AsyncPostBackTrigger ControlID="lnkId"  />--%>
                    </Triggers>
                </asp:UpdatePanel>
                <div class="modal-footer">
                </div>
            </div>

        </div>
    </div>
    <style>
        .chiller-theme .nav-tabs.dynamic-nav-tabs > li.active {
            border-radius: 8px;
            background-color: transparent !important;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function CheckNumeric(e) {

            if (window.event) // IE 
            {
                if ((e.keyCode < 48 || e.keyCode > 57) & e.keyCode != 8) {
                    event.returnValue = false;
                    return false;

                }
            }
            else { // Fire Fox
                if ((e.which < 48 || e.which > 57) & e.which != 8) {
                    e.preventDefault();
                    return false;

                }
            }
        }

    </script>

    <script type="text/javascript">
        function MutExChkList(chk) {
            var chkList = chk.parentNode.parentNode.parentNode;
            var chks = chkList.getElementsByTagName("input");
            for (var i = 0; i < chks.length; i++) {
                if (chks[i] != chk && chk.checked) {
                    //  chks[i].checked = false;
                    chks[i].checked = true;
                }
            }
        }

        function setUploadButtonStatefEED() {

            var maxFileSize = 1000000;

            var fi = document.getElementById('ctl00_ContentPlaceHolder1_fuDocument');

            for (var i = 0; i <= fi.files.length - 1; i++) {

                var fsize = fi.files.item(i).size;

                if (fsize >= maxFileSize) {
                    alert("File Size should be less than 1 MB");
                    $("#ctl00_ContentPlaceHolder1_fuDocument").val("");

                }
            }

        }


    </script>

    <!-- file upload Start-->
    <script>
        $("#ctl00_ContentPlaceHolder1_photoupload").change(function () {
            //$('.fuCollegeLogo').on('change', function () {

            var maxFileSize = 1000000;
            var fi = document.getElementById('ctl00_ContentPlaceHolder1_photoupload');
            myfile = $(this).val();
            var ext = myfile.split('.').pop();
            var res = ext.toUpperCase();

            //alert(res)
            if (res != "JPG" && res != "JPEG" && res != "PNG") {
                alert("Please Select jpg,jpeg,JPG,JPEG,PNG File Only.");
                $('.logoContainer img').attr('src', "../IMAGES/default-fileupload.png");
                $(this).val('');
            }

            for (var i = 0; i <= fi.files.length - 1; i++) {
                var fsize = fi.files.item(i).size;
                if (fsize >= maxFileSize) {
                    alert("File Size should be less than 1 MB");
                    $('.logoContainer img').attr('src', "../IMAGES/default-fileupload.png");
                    $("#ctl00_ContentPlaceHolder1_photoupload").val("");

                }
            }

        });
    </script>
    <script>
        $("#ctl00_ContentPlaceHolder1_photoupload").change(function () {
            var fileName = $(this).val();

            newText = fileName.replace(/fakepath/g, '');
            var newtext1 = newText.replace(/C:/, '');
            //newtext2 = newtext1.replace('//', ''); 
            var result = newtext1.substring(2, newtext1.length);


            if (result.length > 0) {
                $(this).parent().children('span').html(result);
            }
            else {
                $(this).parent().children('span').html("Choose file");
            }
        });
        //file input preview
        function readURL(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('.logoContainer img').attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
            }
        }
        $(".fileContainer input:file").change(function () {
            readURL(this);
        });
    </script>

    <script>
        $(document).ready(function () {
            $('.fuDocumentX').on('change', function () {
                myfile = $(this).val();
                var ext = myfile.split('.').pop();
                if (ext != "pdf") {
                    alert("Please Select PDF File Only.");
                    $(this).val('');
                }
            });
        });
    </script>
    <script>
        function RemoveCountryName() {

            $("#select2-ddlMobileCode-container").html($("#select2-ddlMobileCode-container").html().split('-')[0]);
            if ($("#ddlMobileCode").val().split('-')[0] != "212") {
                $("#txtMobileNo").val('');
            }
            else {
                $("#txtMobileNo").val('0');
            }
        }
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(function () {
                $("#ddlMobileCode").html($(".select2-selection__rendered").html().split('-')[0]);
                // alert('byee')
            });
        });
    </script>
    <script type="text/javascript">
        function Validator() {
            var pass = $('#txtPassport').val();
            var nic = $('#txtNIC').val();
            if (pass == '' && nic == '') {
                alert("Passport No. OR NIC(National Identity card) is Required !");
            }
        }
    </script>
    <script>
        function RemoveHomeCountryName() {

            $("#select2-ddlHomeTelMobileCode-container").html($("#select2-ddlHomeTelMobileCode-container").html().split('-')[0]);
            //if ($("#ddlHomeTelMobileCode").val().split('-')[0] != "212") {
            //    $("#txtHomeMobileNo").val('');
            //}
            //else {
            //    $("#txtHomeMobileNo").val('0');
            //}
        }
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(function () {
                $("#ddlHomeTelMobileCode").html($(".select2-selection__rendered").html().split('-')[0]);
                // alert('byee')
            });
        });
    </script>
    <script>
        $(function () {
            //var dateval = document.getElementById('<%=txtDateOfBirth.ClientID%>').value;

            var prev_date = new Date();
            prev_date.setDate(prev_date.getDate());
            $('.dob').daterangepicker({
                singleDatePicker: true,
                showDropdowns: true,
                //minDate: '01/1/1975',
                maxDate: prev_date,
                locale: {
                    format: 'DD/MM/YYYY'
                },
                autoApply: true,
            });
            //if (dateval == "") {
            //    $('.dob').val('');
            //}
        });
    </script>
    <script>
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(function () {
                //var dateval = document.getElementById('<%=txtDateOfBirth.ClientID%>').value;

                var prev_date = new Date();
                prev_date.setDate(prev_date.getDate());
                $('.dob').daterangepicker({
                    singleDatePicker: true,
                    showDropdowns: true,
                    //minDate: '01/1/1975',
                    maxDate: prev_date,
                    locale: {
                        format: 'DD/MM/YYYY'
                    },
                    autoApply: true,
                });
                //if (dateval == "") {
                //    $('.dob').val('');
                //}
            });
        });
    </script>
    <script type="text/javascript">

        function CountCharactersPerment() {
            var maxSize = 100;

            if (document.getElementById('<%= txtPermAddress.ClientID %>')) {
                var ctrl = document.getElementById('<%= txtPermAddress.ClientID %>');
                var len = document.getElementById('<%= txtPermAddress.ClientID %>').value.length;
                if (len <= maxSize) {
                    return;
                }
                else {
                    alert("Max Of length Should be only 100 Characters ");
                    ctrl.value = ctrl.value.substring(0, maxSize);

                }
            }

            return false;
        }
    </script>
    <script>
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            function CountCharactersPerment() {
                var maxSize = 100;

                if (document.getElementById('<%= txtPermAddress.ClientID %>')) {
                    var ctrl = document.getElementById('<%= txtPermAddress.ClientID %>');
                    var len = document.getElementById('<%= txtPermAddress.ClientID %>').value.length;
                    if (len <= maxSize) {
                        return;
                    }
                    else {
                        alert("Max Of length Should be only 100 Characters ");
                        ctrl.value = ctrl.value.substring(0, maxSize);

                    }
                }

                return false;
            }
        });
    </script>
    <%--    <script>
        $(document).ready(function () {

            $(".off-line-butn").hide();
            $("#myBtnDeposit").hide();
            $("#ctl00_ContentPlaceHolder1_showOnlinePaymentDetails").hide();


            $("#ctl00_ContentPlaceHolder1_rdPaymentOption").click(function () {
                var radioValue = $('#<%=rdPaymentOption.ClientID %> input[type=radio]:checked').val();
                if (radioValue == 0) {

                    $("#myBtnDeposit").show();
                    $(".off-line-butn").show();
                    $("#ctl00_ContentPlaceHolder1_showOnlinePaymentDetails").hide();
                }
                else {
                    $(".off-line-butn").hide();
                    $("#myBtnDeposit").hide();
                    $("#ctl00_ContentPlaceHolder1_showOnlinePaymentDetails").show();

                }
            });
         });
    </script>
    <script>

        $(document).ready(function () {
            var parameter = Sys.WebForms.PageRequestManager.getInstance();
            parameter.add_endRequest(function () {
                // $(".off-line-butn").hide();
                // $("#myBtn").hide();


                $("#ctl00_ContentPlaceHolder1_rdPaymentOption").click(function () {
                    var radioValue = $('#<%=rdPaymentOption.ClientID %> input[type=radio]:checked').val();
                    if (radioValue == 0) {

                        $("#myBtnDeposit").show();
                        $(".off-line-butn").show();
                        $("#ctl00_ContentPlaceHolder1_showOnlinePaymentDetails").hide();
                    }
                    else {
                        $(".off-line-butn").hide();
                        $("#myBtnDeposit").hide();
                        $("#ctl00_ContentPlaceHolder1_showOnlinePaymentDetails").show();

                    }
                });
            });
        });
    </script>--%>
    <script>
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $("#ctl00_ContentPlaceHolder1_rdPaymentOption").click(function () {
                    var radioValue = $('#<%=rdPaymentOption.ClientID %> input[type=radio]:checked').val();
                    if (radioValue == 0) {

                    }
                    else {

                    }
                });
            });
        });
    </script>

    <script>
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $("#ctl00_ContentPlaceHolder1_rdPaymentOption").click(function () {
                    var radioValue = $('#<%=rdPaymentOption.ClientID %> input[type=radio]:checked').val();
                    if (radioValue == 0) {

                    }
                    else {

                    }
                });
            });
        });
    </script>

    <%-- document upload validation start --%>
    <script type="text/javascript">
        function setUploadButtondoc(chk) {
            var maxFileSize = 1000000;
            var fi = document.getElementById(chk.id);
            var tabValue = $(chk).attr('TabIndex');

            for (var i = 0; i <= fi.files.length - 1; i++) {
                var fsize = fi.files.item(i).size;
                if (fsize >= maxFileSize) {
                    alert("Image Size Greater Than 1MB");
                    $(chk).val("");
                }
            }
            var fileExtension = ['jpeg', 'jpg', 'JPG', 'JPEG', 'PNG', 'png'];
            var fileExtension1 = ['pdf'];
            if (tabValue == "1") {
                if ($.inArray($('#ctl00_ContentPlaceHolder1_lvDocument_ctrl0_fuDocument').val().split('.').pop().toLowerCase(), fileExtension) == -1) {
                    alert("Only formats are allowed : " + fileExtension.join(', '));
                    $("#ctl00_ContentPlaceHolder1_lvDocument_ctrl0_fuDocument").val("");
                }
            }
            else {
                if ($.inArray($(chk).val().replace(',', '.').split('.').pop().toLowerCase(), fileExtension1) == -1) {
                    alert("Only formats are allowed : " + fileExtension1.join(', '));
                    $(chk).val("");
                }
            }
        }
    </script>

    <%-- document upload validation end --%>

    <script type="text/javascript">
        function setUploadButtonState() {

            var maxFileSize = 1000000;
            var fi = document.getElementById('ctl00_ContentPlaceHolder1_FuChallan');
            for (var i = 0; i <= fi.files.length - 1; i++) {

                var fsize = fi.files.item(i).size;

                if (fsize >= maxFileSize) {
                    alert("Image Size Greater Than 1MB");
                    $("#ctl00_ContentPlaceHolder1_FuChallan").val("");

                }
            }
            var fileExtension = ['jpeg', 'jpg', 'JPG', 'JPEG', 'PNG', 'png', 'PDF', 'pdf'];
            if ($.inArray($('#ctl00_ContentPlaceHolder1_FuChallan').val().split('.').pop().toLowerCase(), fileExtension) == -1) {
                alert("Only formats are allowed : " + fileExtension.join(', '));
                $("#ctl00_ContentPlaceHolder1_FuChallan").val("");
            }
        }

    </script>
    <script type="text/javascript">
        function CheckUnchekCheckbox(chk) {
            var chkList = chk.parentNode.parentNode.parentNode;
            var chks = chkList.getElementsByTagName("input");
            for (var i = 0; i < chks.length; i++) {
                if (chks[i] != chk && chk.checked) {
                    //  chks[i].checked = false;
                    chks[i].checked = false;
                }
                else {
                    chks[i].checked = false;
                }
            }
            chk.checked = true;
        }

        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            function CheckUnchekCheckbox(chk) {
                var chkList = chk.parentNode.parentNode.parentNode;
                var chks = chkList.getElementsByTagName("input");
                for (var i = 0; i < chks.length; i++) {
                    if (chks[i] != chk && chk.checked) {
                        //  chks[i].checked = false;
                        chks[i].checked = false;
                    }
                    else {
                        chks[i].checked = false;
                    }
                }
                chk.checked = true;
            }
        });
    </script>

    <script>
        $(function () {
            var dateval = document.getElementById('<%=txtPaymentdate.ClientID%>').value;

            var prev_date = new Date();
            prev_date.setDate(prev_date.getDate());
            $('.PaymentDate').daterangepicker({
                singleDatePicker: true,
                showDropdowns: true,
                //minDate: '01/1/1975',
                maxDate: prev_date,
                locale: {
                    format: 'DD/MM/YYYY'
                },
                autoApply: true,
            });
            if (dateval == "") {
                $('.PaymentDate').val('');
            }
        });
    </script>
    <script>
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(function () {
                var dateval = document.getElementById('<%=txtPaymentdate.ClientID%>').value;

                var prev_date = new Date();
                prev_date.setDate(prev_date.getDate());
                $('.PaymentDate').daterangepicker({
                    singleDatePicker: true,
                    showDropdowns: true,
                    //minDate: '01/1/1975',
                    maxDate: prev_date,
                    locale: {
                        format: 'DD/MM/YYYY'
                    },
                    autoApply: true,
                });
                if (dateval == "") {
                    $('.PaymentDate').val('');
                }
            });
        });
    </script>
        <script>
            function CheckValue(chk) {
                if (chk.checked == true) {
                    $("#divAmountSchol").removeClass('d-none');
                    $("#divSubmitScholarship").removeClass('d-none');
                }
                else {
                    $("#divAmount").addClass('d-none');
                    $("#divSubmitScholarship").addClass('d-none');
                }
            }
    </script>
    <script>
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            function CheckValue(chk) {
                if (chk.checked == true) {
                    $("#divAmountSchol").removeClass('d-none');
                    $("#divSubmitScholarship").removeClass('d-none');
                }
                else {
                    $("#divAmountSchol").addClass('d-none');
                    $("#divSubmitScholarship").addClass('d-none');
                }
            }
        });
    </script>
</asp:Content>

