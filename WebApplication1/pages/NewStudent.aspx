<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="NewStudent.aspx.cs" Inherits="ACADEMIC_AddStudent" meta:resourcekey="PageResource1" UICulture="auto" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../css/ImageViewer.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="../css/jquery.verySimpleImageViewer.css">
    <script src="../js/jquery.verySimpleImageViewer.js"></script>
    <style>
        #ctl00_ContentPlaceHolder1_imageViewerContainer {
            max-width: 800px;
            height: 500px;
            margin: 50px auto;
            border: 1px solid #000;
            border-radius: 3px;
        }

        .image_viewer_inner_container {
            overflow: scroll !important;
        }

        #ctl00_ContentPlaceHolder1_imageViewerContainer .jqvsiv_main_image_content {
            text-align: center !important;
        }

            #ctl00_ContentPlaceHolder1_imageViewerContainer .jqvsiv_main_image_content img {
                /*position: initial !important;*/
                z-index: 3;
                cursor: n-resize;
            }
    </style>
    <style>
        .card-header .title {
            font-size: 15px;
            color: #000;
        }

        .card-header .accicon {
            float: right;
            font-size: 20px;
            width: 1.2em;
        }

        .card-header {
            cursor: pointer;
            border-bottom: none;
            padding: .3rem 0.7rem;
        }

        .card {
            border: 1px solid #ddd;
        }

        .card-body {
            border-top: 1px solid #ddd;
            padding: 1.25rem 0rem;
        }

        .card-header:not(.collapsed) .rotate-icon {
            transform: rotate(180deg);
        }

        td .fa-eye {
            font-size: 18px;
            color: #0d70fd;
        }

        td .fa-download {
            font-size: 18px;
            color: green;
        }
    </style>
    <%--    <link href='<%=Page.ResolveUrl("~/plugins/newbootstrap/css/lead.css") %>' rel="stylesheet" />
    <script src="https://bankofceylon.gateway.mastercard.com/checkout/version/61/checkout.js" data-error="errorCallback" data-cancel="cancelCallback"></script>--%>
    <script type="text/javascript">
        function errorCallback(error) {
            console.log(JSON.stringify(error));
        }
        function cancelCallback() {
            //console.log('Payment cancelled');
        }

        cancelCallback = "https://admission.sliit.lk/OnlineResponse.aspx"
        Checkout.configure({
            session: {
                id: '<%= Session["PaymentSession"] %>'
            },
            interaction: {
                merchant: {
                    name: 'Your merchant name',
                    address: {
                        line1: '200 Sample St',
                        line2: '1234 Example Town'
                    }
                }
            }
        });
    </script>

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
    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <%--<h3 class="box-title">Applicant Preview</h3>--%>
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server" meta:resourcekey="lblDynamicPageTitleResource1"></asp:Label></h3>
                </div>
                <div class="col-12">
                    <div class="row">
                        <div class="form-group col-lg-12 col-md-12 col-12">
                            <div class="text-center search-icon">
                                <a href="#" title="Search Student for Modification" data-toggle="modal" data-target="#myModal2">
                                    <asp:Image ID="imgSearch" runat="server" ImageUrl="~/IMAGES/search-svg.png" TabIndex="1"
                                        AlternateText="Search Student by Name, Reg. No" Style="padding-left: -500px" ToolTip="Search Student by IDNo, Name, Reg. No, Branch, Semester" meta:resourcekey="imgSearchResource1" /></a>

                            </div>
                        </div>
                    </div>
                </div>
                <div class="accordion" id="accordionExample">
                    <div class="card">
                        <div class="card-header" data-toggle="collapse" data-target="#collapseOne" aria-expanded="true">
                            <span class="title">Personal Details </span>
                            <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                        </div>
                        <div id="collapseOne" class="collapse show">
                            <div>
                                <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="updfinalblock" DisplayAfter="0">
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
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="card-body">
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>First Name</label>
                                                    </div>
                                                    <asp:TextBox ID="txtFirstName" runat="server" TabIndex="1" ToolTip="Please Enter First Name" Placeholder="Enter First Name"
                                                        onkeypress="return alphaOnly(event);" MaxLength="100" CssClass="form-control" meta:resourcekey="txtFirstNameResource1" />
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtFirstName"
                                                        InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" FilterMode="InvalidChars" Enabled="True" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please Enter First Name"
                                                        ControlToValidate="txtFirstName" Display="None" SetFocusOnError="True" ValidationGroup="submit" meta:resourcekey="RequiredFieldValidator5Resource1" />
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Last Name</label>
                                                    </div>
                                                    <asp:TextBox ID="txtLastName" runat="server" TabIndex="2" ToolTip="Please Enter Last Name" Placeholder="Enter Last Name"
                                                        onkeypress="return alphaOnly(event);" MaxLength="100" CssClass="form-control" meta:resourcekey="txtLastNameResource1" />
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txtLastName"
                                                        InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" FilterMode="InvalidChars" Enabled="True" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Please Enter Last Name"
                                                        ControlToValidate="txtLastName" Display="None" SetFocusOnError="True" ValidationGroup="submit" meta:resourcekey="RequiredFieldValidator6Resource1" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Full Name</label>
                                                    </div>
                                                    <asp:TextBox ID="txtfullname" runat="server" TabIndex="3" ToolTip="Please Enter Full Name" Placeholder="Enter Full Name"
                                                        onkeypress="return alphaOnly(event);" MaxLength="250" CssClass="form-control" meta:resourcekey="txtfullnameResource1" />
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtfullname"
                                                        InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" FilterMode="InvalidChars" Enabled="True" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="Please Enter Full Name"
                                                        ControlToValidate="txtfullname" Display="None" SetFocusOnError="True" ValidationGroup="submit" meta:resourcekey="RequiredFieldValidator11Resource1" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Name with Initials</label>
                                                    </div>
                                                    <asp:TextBox ID="txtNameInitial" runat="server" TabIndex="4" ToolTip="Please Enter Name with Initials"
                                                        onkeypress="return alphaOnly(event);" MaxLength="100" CssClass="form-control" placeholder="Example:- PERERA S.A" meta:resourcekey="txtNameInitialResource1" />
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="ajfFullName" runat="server" TargetControlID="txtNameInitial"
                                                        InvalidChars="~`!@#$%^*()_+=,/:;<>?'{}[]\|-&&quot;'" FilterMode="InvalidChars" Enabled="True" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Please Enter Name with Initials"
                                                        ControlToValidate="txtNameInitial" Display="None" SetFocusOnError="True" ValidationGroup="submit" meta:resourcekey="RequiredFieldValidator7Resource1" />
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Email</label>
                                                    </div>
                                                    <asp:TextBox ID="txtEmail" runat="server" TabIndex="5" CssClass="form-control" Placeholder="Enter Email" ToolTip="Please Enter Email" meta:resourcekey="txtEmailResource1" />
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtEmail"
                                                        Display="None" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                        ErrorMessage="Please Enter Valid Email_ID" ValidationGroup="submit" meta:resourcekey="RegularExpressionValidator2Resource1"></asp:RegularExpressionValidator>
                                                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="Please Enter Email"
                                                        ControlToValidate="txtEmail" Display="None" SetFocusOnError="True" ValidationGroup="submit" meta:resourcekey="rfvEmailResource1" />
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Mobile No.</label>
                                                    </div>
                                                    <div class="input-group">
                                                        <span class="input-group-prepend" style="width: 130px! important;">
                                                            <asp:DropDownList ID="ddlMobileCode" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" TabIndex="6" onchange="return RemoveCountryName()" meta:resourcekey="ddlMobileCodeResource1">
                                                                <asp:ListItem Value="0" meta:resourcekey="ListItemResource1">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </span>
                                                        <asp:TextBox ID="txtMobile" runat="server" TabIndex="7"
                                                            MaxLength="10" ToolTip="Please Enter Mobile No." CssClass="form-control ml-2 pb-0 pl-0 pr-0" Placeholder="Enter Mobile No." meta:resourcekey="txtMobileResource1" />
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtMobile"
                                                            ValidChars="1234567890" Enabled="True" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator38" runat="server" ControlToValidate="ddlMobileCode"
                                                            ValidationGroup="submit" Display="None" InitialValue="0" SetFocusOnError="True" ErrorMessage="Please Select Mobile Code" meta:resourcekey="RequiredFieldValidator38Resource1"></asp:RequiredFieldValidator>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Enter Mobile No"
                                                            ControlToValidate="txtMobile" Display="None" SetFocusOnError="True" ValidationGroup="submit" meta:resourcekey="RequiredFieldValidator1Resource1" />
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                                            ControlToValidate="txtMobile" ErrorMessage="Invalid Mobile Number"
                                                            SetFocusOnError="True" ValidationExpression="[0-9]{10}" ValidationGroup="submit" meta:resourcekey="RegularExpressionValidator1Resource1"></asp:RegularExpressionValidator>

                                                    </div>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Home Telephone No.</label>
                                                    </div>
                                                    <div class="input-group">
                                                        <span class="input-group-prepend" style="width: 130px! important;">
                                                            <asp:DropDownList ID="ddlHomeMobileCode" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" TabIndex="8" onchange="return RemoveHomeCountryName()" meta:resourcekey="ddlHomeMobileCodeResource1">
                                                                <asp:ListItem Value="0" meta:resourcekey="ListItemResource2">Please Select</asp:ListItem>
                                                            </asp:DropDownList>

                                                        </span>
                                                        <asp:TextBox ID="txtHomeTel" runat="server" TabIndex="9" CssClass="form-control ml-2 pb-0 pl-0 pr-0" Placeholder="Home Telephone No." MaxLength="10" meta:resourcekey="txtHomeTelResource1" />
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="txtHomeTel"
                                                            ValidChars="1234567890" Enabled="True" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator37" runat="server" ControlToValidate="ddlHomeMobileCode"
                                                            ValidationGroup="submit" Display="None" InitialValue="0" SetFocusOnError="True" ErrorMessage="Please Select Home Telephone Code" meta:resourcekey="RequiredFieldValidator37Resource1"></asp:RequiredFieldValidator>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Please Enter Home Telephone No"
                                                            ControlToValidate="txtHomeTel" Display="None" SetFocusOnError="True" ValidationGroup="submit" meta:resourcekey="RequiredFieldValidator8Resource1" />
                                                    </div>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>NIC (National Identity card)</label>
                                                    </div>
                                                    <asp:TextBox ID="txtNIC" runat="server" TabIndex="10" ToolTip="Please Enter NIC" Placeholder="Enter NIC"
                                                        MaxLength="30" CssClass="form-control" onblur="return Validator();" meta:resourcekey="txtNICResource1" />
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtNIC"
                                                        InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" FilterMode="InvalidChars" Enabled="True" />
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Passport No</label>
                                                    </div>
                                                    <asp:TextBox ID="txtPassportNo" runat="server" TabIndex="11" ToolTip="Please Enter Passport No" Placeholder="Enter Passport No"
                                                        MaxLength="30" CssClass="form-control" onblur="return Validator();" meta:resourcekey="txtPassportNoResource1" />

                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" TargetControlID="txtPassportNo"
                                                        InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" FilterMode="InvalidChars" Enabled="True" />
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Date Of Birth (DD/MM/YY)</label>
                                                    </div>
                                                    <asp:TextBox ID="txtDateOfBirth" name="dob" runat="server" TabIndex="12" CssClass="form-control dobPersonal"
                                                        ToolTip="Please Enter Date Of Birth" placeholder="DD/MM/YYYY" meta:resourcekey="txtDateOfBirthResource1" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ErrorMessage="Please Enter DOB (DD/MM/YY)"
                                                        ControlToValidate="txtDateOfBirth" Display="None" SetFocusOnError="True" ValidationGroup="submit" meta:resourcekey="RequiredFieldValidator22Resource1" />
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Gender</label>
                                                    </div>
                                                    <asp:RadioButtonList ID="rdoGender" runat="server" TabIndex="13" RepeatDirection="Horizontal" meta:resourcekey="rdoGenderResource1">
                                                        <asp:ListItem Text="&nbsp;Male" Value="0" meta:resourcekey="ListItemResource3"></asp:ListItem>
                                                        <asp:ListItem Text="&nbsp;Female" Value="1" meta:resourcekey="ListItemResource4"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    <asp:RequiredFieldValidator ID="rfvGender" runat="server" ErrorMessage="Please Select Gender"
                                                        ControlToValidate="rdoGender" Display="None" ValidationGroup="submit" meta:resourcekey="rfvGenderResource1"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12" id="divFather" runat="server" visible="true">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Name of Parent/Guardian</label>
                                                    </div>
                                                    <asp:TextBox ID="txtMiddleName" runat="server" Width="100%" ToolTip="Please Enter Father Name"
                                                        onkeypress="return alphaOnly(event);" MaxLength="20" CssClass="form-control" Placeholder="Please Enter Father Name" TabIndex="0" />
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" TargetControlID="txtMiddleName"
                                                        InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}[]\|-&&quot;'" FilterMode="InvalidChars" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ErrorMessage="Please Enter Father Name"
                                                        ControlToValidate="txtMiddleName" Display="None" SetFocusOnError="true" ValidationGroup="submit" />
                                                    <asp:HiddenField runat="server" ID="HiddenField1" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Parent/Guardian Personal Email</label>
                                                    </div>
                                                    <asp:TextBox ID="txtPersonalEmail" runat="server" TabIndex="5" CssClass="form-control" Placeholder="Enter Email" ToolTip="Please Enter Email" />
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtPersonalEmail"
                                                        Display="None" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                        ErrorMessage="Please Enter Valid Email_ID" ValidationGroup="submit"></asp:RegularExpressionValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ErrorMessage="Please Enter Parent/Guardian Personal Email"
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
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ErrorMessage="Please select Mobile code"
                                                                ControlToValidate="ddlConCode" Display="None" SetFocusOnError="true" ValidationGroup="submit" InitialValue="0" />
                                                        </span>
                                                        <asp:TextBox ID="txtPMobNo" runat="server" TabIndex="10" onkeyup="validateNumeric(this)"
                                                            MaxLength="15" ToolTip="Please Enter Parent's Contact No." CssClass="form-control ml-2 pb-0 pl-0 pr-0" Placeholder="Enter Parent's Contact No." />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ErrorMessage="Please Enter Parent's Contact Number"
                                                            ControlToValidate="txtPMobNo" Display="None" SetFocusOnError="true" ValidationGroup="submit" />
                                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" TargetControlID="txtPMobNo"
                                                            ValidChars="1234567890" FilterMode="ValidChars" />
                                                    </div>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Citizen Type</label>
                                                    </div>
                                                    <asp:RadioButtonList ID="rdoCitizenType" runat="server" TabIndex="14" RepeatDirection="Horizontal" meta:resourcekey="rdoCitizenTypeResource1">
                                                        <asp:ListItem Text="&nbsp;Sri Lankan" Value="0" meta:resourcekey="ListItemResource5"></asp:ListItem>
                                                        <asp:ListItem Text="&nbsp;Foreign National" Value="1" meta:resourcekey="ListItemResource6"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    <asp:RequiredFieldValidator ID="rfvCitizenType" runat="server" ErrorMessage="Please Select Student sri Lankan or foreign national"
                                                        ControlToValidate="rdoCitizenType" Display="None" ValidationGroup="submit" meta:resourcekey="rfvCitizenTypeResource1"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Left / Right Handed</label>
                                                    </div>
                                                    <asp:RadioButtonList ID="rdoLeftRight" runat="server" TabIndex="15" RepeatDirection="Horizontal" meta:resourcekey="rdoLeftRightResource1">
                                                        <asp:ListItem Text="&nbsp;Left Handed" Value="0" meta:resourcekey="ListItemResource7"></asp:ListItem>
                                                        <asp:ListItem Text="&nbsp;Right Handed" Value="1" meta:resourcekey="ListItemResource8"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    <asp:RequiredFieldValidator ID="rfvLeftRight" runat="server" ErrorMessage="Please Select Left / Right Handed"
                                                        ControlToValidate="rdoLeftRight" Display="None" ValidationGroup="submit" meta:resourcekey="rfvLeftRightResource1"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12">
                                            <div class="row">

                                                <div class="col-12">
                                                    <div class="sub-heading">
                                                        <h5>Postal Address</h5>
                                                    </div>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Address (Max. Length 200)</label>
                                                    </div>
                                                    <asp:TextBox ID="txtPermAddress" runat="server" TabIndex="16" TextMode="MultiLine" Placeholder="Enter Address"
                                                        MaxLength="200" ToolTip="Please Enter Address" CssClass="form-control" onkeyup="return CountCharactersPerment();" meta:resourcekey="txtPermAddressResource1" />
                                                    <asp:RequiredFieldValidator ID="rfvAddress" runat="server" ControlToValidate="txtPermAddress"
                                                        ValidationGroup="submit" Display="None" SetFocusOnError="True" ErrorMessage="Please Enter Address" meta:resourcekey="rfvAddressResource1"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Country</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlCountry" runat="server" TabIndex="17" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" meta:resourcekey="ddlCountryResource1">
                                                        <asp:ListItem Value="0" meta:resourcekey="ListItemResource9">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvCountry" runat="server" ControlToValidate="ddlCountry"
                                                        ValidationGroup="submit" Display="None" InitialValue="0" SetFocusOnError="True" ErrorMessage="Please Select Country" meta:resourcekey="rfvCountryResource1"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Province</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlProvince" runat="server" Width="100%" TabIndex="18" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlProvince_SelectedIndexChanged" meta:resourcekey="ddlProvinceResource1">
                                                        <asp:ListItem Value="0" meta:resourcekey="ListItemResource10">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvProvince" runat="server" ControlToValidate="ddlProvince"
                                                        ValidationGroup="submit" Display="None" InitialValue="0" SetFocusOnError="True" ErrorMessage="Please Select Province" meta:resourcekey="rfvProvinceResource1"></asp:RequiredFieldValidator>

                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>District</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlDistrict" runat="server" TabIndex="19" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" meta:resourcekey="ddlDistrictResource1">
                                                        <asp:ListItem Value="0" meta:resourcekey="ListItemResource11">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvDistrict" runat="server" ControlToValidate="ddlDistrict"
                                                        ValidationGroup="submit" Display="None" InitialValue="0" SetFocusOnError="True" ErrorMessage="Please Select District" meta:resourcekey="rfvDistrictResource1"></asp:RequiredFieldValidator>
                                                </div>
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
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txtPerAddress"
                                                        ValidationGroup="submit" Display="None" SetFocusOnError="True" ErrorMessage="Please Enter Permanent Address" meta:resourcekey="rfvAddressResource1"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Country</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlPerContry" runat="server" TabIndex="17" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" meta:resourcekey="ddlCountryResource1">
                                                        <asp:ListItem Value="0" meta:resourcekey="ListItemResource9">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="ddlPerContry"
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
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="ddlPerProvince"
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
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ControlToValidate="ddlPerDisctrict"
                                                        ValidationGroup="submit" Display="None" InitialValue="0" SetFocusOnError="True" ErrorMessage="Please Select District" meta:resourcekey="rfvDistrictResource1"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>

                    <div class="card">
                        <div class="card-header" data-toggle="collapse" data-target="#collapseTwo" aria-expanded="true">
                            <span class="title">Education Details</span>
                            <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                        </div>
                        <div id="collapseTwo" class="collapse show">
                            <div>
                                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updfinalblock" DisplayAfter="0">
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
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="card-body">
                                        <div id="divUgEducationUG" runat="server">


                                            <div class="box-body" id="UG" runat="server">
                                                <div class="col-12">
                                                    <asp:RadioButtonList ID="alolselection" runat="server" AutoPostBack="True" ToolTip="Please select Option" OnSelectedIndexChanged="alolselection_SelectedIndexChanged" RepeatDirection="Horizontal" meta:resourcekey="alolselectionResource1">
                                                        <asp:ListItem Value="1" meta:resourcekey="ListItemResource12">A/L Details</asp:ListItem>
                                                        <asp:ListItem Value="2" meta:resourcekey="ListItemResource13">O/L Details</asp:ListItem>
                                                        <asp:ListItem Value="3" meta:resourcekey="ListItemResource14">Both</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>

                                                <div id="aldetails" runat="server">
                                                    <div class="col-12 mt-3">
                                                        <div class="row">
                                                            <div class="col-12">
                                                                <div class="sub-heading">
                                                                    <h5>A/L Details</h5>
                                                                </div>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>A/L Exam Type</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlALExamType" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="20" ToolTip="Please Select A/L  Exam Type"
                                                                    AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlALExamType_SelectedIndexChanged" meta:resourcekey="ddlALExamTypeResource1">
                                                                    <asp:ListItem Value="0" meta:resourcekey="ListItemResource15">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ControlToValidate="ddlALExamType"
                                                                    Display="None" ErrorMessage="Please Select A/L Exam Type" InitialValue="0" SetFocusOnError="True" ValidationGroup="submit" meta:resourcekey="RequiredFieldValidator30Resource1"></asp:RequiredFieldValidator>

                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>A/L Stream</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlALStream" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="21" ToolTip="Please Select A/L Stream" Width="100%" AppendDataBoundItems="True" meta:resourcekey="ddlALStreamResource1">
                                                                    <asp:ListItem Value="0" meta:resourcekey="ListItemResource16">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlALStream" Display="None"
                                                                    ErrorMessage="Please Select A/L Stream" InitialValue="0" SetFocusOnError="True" ValidationGroup="submit" meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>

                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>A/L Passes</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlALPasses" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="22" AutoPostBack="True" ToolTip="Please Select A/L Passes" AppendDataBoundItems="True" meta:resourcekey="ddlALPassesResource1">
                                                                    <asp:ListItem Value="0" meta:resourcekey="ListItemResource17">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlALPasses" Display="None"
                                                                    ErrorMessage="Please Select A/L Passes" InitialValue="0" SetFocusOnError="True" ValidationGroup="submit" meta:resourcekey="RequiredFieldValidator3Resource1"></asp:RequiredFieldValidator>

                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Subject 1</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlALSubject1" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="23" ToolTip="Please Select Subject 1"
                                                                    AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlALSubject1_SelectedIndexChanged" meta:resourcekey="ddlALSubject1Resource1">
                                                                    <asp:ListItem Value="0" meta:resourcekey="ListItemResource18">Please Select</asp:ListItem>
                                                                </asp:DropDownList>

                                                                <div style="display: none">
                                                                    <asp:TextBox ID="txtmathsvalidation" runat="server" meta:resourcekey="txtmathsvalidationResource1"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Grade</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlALGrade1" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="24" ToolTip="Please Select Subject 1 Grade" AppendDataBoundItems="True" meta:resourcekey="ddlALGrade1Resource1">
                                                                    <asp:ListItem Value="0" meta:resourcekey="ListItemResource19">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Subject 2</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlALSubject2" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="25" ToolTip="Please Select Subject 2" AppendDataBoundItems="True"
                                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlALSubject2_SelectedIndexChanged" meta:resourcekey="ddlALSubject2Resource1">
                                                                    <asp:ListItem Value="0" meta:resourcekey="ListItemResource20">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Grade</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlALGrade2" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="26" ToolTip="Please Select Subject 2 Grade" AppendDataBoundItems="True" meta:resourcekey="ddlALGrade2Resource1">
                                                                    <asp:ListItem Value="0" meta:resourcekey="ListItemResource21">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Subject 3</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlALSubject3" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="27" ToolTip="Please Select Subject 3" AppendDataBoundItems="True"
                                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlALSubject3_SelectedIndexChanged" meta:resourcekey="ddlALSubject3Resource1">
                                                                    <asp:ListItem Value="0" meta:resourcekey="ListItemResource22">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="div7" runat="server">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Grade</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlALGrade3" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="28" ToolTip="Please Select Subject 3 Grade" AppendDataBoundItems="True" meta:resourcekey="ddlALGrade3Resource1">
                                                                    <asp:ListItem Value="0" meta:resourcekey="ListItemResource23">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="divbiology" runat="server">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Subject 4</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlALSubject4" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="29" ToolTip="Please Select Subject 4" AppendDataBoundItems="True" meta:resourcekey="ddlALSubject4Resource1">
                                                                    <asp:ListItem Value="0" meta:resourcekey="ListItemResource24">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="div8" runat="server">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Grade</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlALGrade4" runat="server" CssClass="form-control" data-select2-enable="true" TabIndex="30" ToolTip="Please Select Subject 4 Grade" AppendDataBoundItems="True" meta:resourcekey="ddlALGrade4Resource1">
                                                                    <asp:ListItem Value="0" meta:resourcekey="ListItemResource25">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="div36" runat="server">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>A/L IndexNo</label>
                                                                </div>
                                                                <asp:TextBox ID="txtALIndex" runat="server" CssClass="form-control" TabIndex="31" onkeyup="IsNumeric(this);" meta:resourcekey="txtALIndexResource1"></asp:TextBox>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="div5" runat="server">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>School Name</label>
                                                                </div>
                                                                <asp:TextBox ID="txtalschool" runat="server" CssClass="form-control" TabIndex="32" meta:resourcekey="txtalschoolResource1"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div id="oldetails" runat="server">
                                                    <div class="col-12 mt-3">
                                                        <div class="row">
                                                            <div class="col-12">
                                                                <div class="sub-heading">
                                                                    <h5>O/L Details</h5>
                                                                </div>
                                                            </div>

                                                            <div class="form-group col-lg-12 col-md-12 col-12  d-none ">
                                                                <div class=" note-div">
                                                                    <h5 class="heading">Note </h5>
                                                                    <p><i class="fa fa-star" aria-hidden="true"></i><span>Entering O/L results is not mandatory for all programs, check the eligibility of program you wish to apply if needed then it is compulsory to fill the details of O/L.</span></p>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup></sup>
                                                                    <label>O/L Exam Type </label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlOLExamType" runat="server" CssClass="form-control select2 select-clik" TabIndex="33" ToolTip="Please Select O/L Exam Type"
                                                                    Width="100%" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlOLExamType_SelectedIndexChanged" meta:resourcekey="ddlOLExamTypeResource1">
                                                                    <asp:ListItem Value="0" meta:resourcekey="ListItemResource26">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="Div2" runat="server" visible="False">
                                                                <div class="label-dynamic">
                                                                    <sup></sup>
                                                                    <label>O/L Stream </label>
                                                                </div>

                                                                <asp:DropDownList ID="ddlOLStream" runat="server" CssClass="form-control select2 select-clik" TabIndex="34" ToolTip="Please Select O/L Stream" Width="100%" AppendDataBoundItems="True" meta:resourcekey="ddlOLStreamResource1">
                                                                    <asp:ListItem Value="0" meta:resourcekey="ListItemResource27">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="Div12" runat="server">
                                                                <div class="label-dynamic">
                                                                    <sup></sup>
                                                                    <label>O/L Passes </label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlOLPasses" runat="server" CssClass="form-control select2 select-clik" TabIndex="35" ToolTip="Please Select O/L Passes" Width="100%" AppendDataBoundItems="True" meta:resourcekey="ddlOLPassesResource1">
                                                                    <asp:ListItem Value="0" meta:resourcekey="ListItemResource28">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup></sup>
                                                                    <label>Subject 1 </label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlOLSubject1" runat="server" CssClass="form-control select2 select-clik" TabIndex="36" ToolTip="Please Select Subject 1" AppendDataBoundItems="True"
                                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlOLSubject1_SelectedIndexChanged" meta:resourcekey="ddlOLSubject1Resource1">
                                                                    <asp:ListItem Value="0" meta:resourcekey="ListItemResource29">Please Select</asp:ListItem>
                                                                </asp:DropDownList>

                                                                <div style="display: none">
                                                                    <asp:TextBox ID="TextBox1" runat="server" meta:resourcekey="TextBox1Resource1"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup></sup>
                                                                    <label>Grade </label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlOLGrade1" runat="server" CssClass="form-control select2 select-clik" TabIndex="37" ToolTip="Please Select Subject 1 Grade"
                                                                    AppendDataBoundItems="True" meta:resourcekey="ddlOLGrade1Resource1">
                                                                    <asp:ListItem Value="0" meta:resourcekey="ListItemResource30">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup></sup>
                                                                    <label>Subject 2 </label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlOLSubject2" runat="server" CssClass="form-control select2 select-clik" TabIndex="38" ToolTip="Please Select Subject 2"
                                                                    AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlOLSubject2_SelectedIndexChanged" meta:resourcekey="ddlOLSubject2Resource1">
                                                                    <asp:ListItem Value="0" meta:resourcekey="ListItemResource31">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup></sup>
                                                                    <label>Grade </label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlOLGrade2" runat="server" CssClass="form-control select2 select-clik" TabIndex="39" ToolTip="Please Select Subject 2 Grade"
                                                                    AppendDataBoundItems="True" meta:resourcekey="ddlOLGrade2Resource1">
                                                                    <asp:ListItem Value="0" meta:resourcekey="ListItemResource32">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup></sup>
                                                                    <label>Subject 3 </label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlOLSubject3" runat="server" CssClass="form-control select2 select-clik" TabIndex="40" ToolTip="Please Select Subject 3"
                                                                    AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlOLSubject3_SelectedIndexChanged" meta:resourcekey="ddlOLSubject3Resource1">
                                                                    <asp:ListItem Value="0" meta:resourcekey="ListItemResource33">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="div13" runat="server">
                                                                <div class="label-dynamic">
                                                                    <sup></sup>
                                                                    <label>Grade </label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlOLGrade3" runat="server" CssClass="form-control select2 select-clik" TabIndex="41" ToolTip="Please Select Subject 3 Grade"
                                                                    AppendDataBoundItems="True" meta:resourcekey="ddlOLGrade3Resource1">
                                                                    <asp:ListItem Value="0" meta:resourcekey="ListItemResource34">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="div14" runat="server">
                                                                <div class="label-dynamic">
                                                                    <sup></sup>
                                                                    <label>Subject 4 </label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlOLSubject4" runat="server" CssClass="form-control select2 select-clik" TabIndex="42" ToolTip="Please Select Subject 4"
                                                                    AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlOLSubject4_SelectedIndexChanged" meta:resourcekey="ddlOLSubject4Resource1">
                                                                    <asp:ListItem Value="0" meta:resourcekey="ListItemResource35">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="div15" runat="server">
                                                                <div class="label-dynamic">
                                                                    <sup></sup>
                                                                    <label>Grade </label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlOLGrade4" runat="server" CssClass="form-control select2 select-clik" TabIndex="43" ToolTip="Please Select Subject 4 Grade" AppendDataBoundItems="True" meta:resourcekey="ddlOLGrade4Resource1">
                                                                    <asp:ListItem Value="0" meta:resourcekey="ListItemResource36">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="div16" runat="server">
                                                                <div class="label-dynamic">
                                                                    <sup></sup>
                                                                    <label>Subject 5 </label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlOLSubject5" runat="server" CssClass="form-control select2 select-clik" TabIndex="44" ToolTip="Please Select Subject 5"
                                                                    AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlOLSubject5_SelectedIndexChanged" meta:resourcekey="ddlOLSubject5Resource1">
                                                                    <asp:ListItem Value="0" meta:resourcekey="ListItemResource37">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="div17" runat="server">
                                                                <div class="label-dynamic">
                                                                    <sup></sup>
                                                                    <label>Grade </label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlOLGrade5" runat="server" CssClass="form-control select2 select-clik" TabIndex="45" ToolTip="Please Select Subject 5 Grade" AppendDataBoundItems="True" meta:resourcekey="ddlOLGrade5Resource1">
                                                                    <asp:ListItem Value="0" meta:resourcekey="ListItemResource38">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="div19" runat="server">
                                                                <div class="label-dynamic">
                                                                    <sup></sup>
                                                                    <label>Subject 6 </label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlOLSubject6" runat="server" CssClass="form-control select2 select-clik" TabIndex="46" ToolTip="Please Select Subject 6"
                                                                    AppendDataBoundItems="True" meta:resourcekey="ddlOLSubject6Resource1">
                                                                    <asp:ListItem Value="0" meta:resourcekey="ListItemResource39">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="div35" runat="server">
                                                                <div class="label-dynamic">
                                                                    <sup></sup>
                                                                    <label>Grade </label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlOLGrade6" runat="server" CssClass="form-control select2 select-clik" TabIndex="47" ToolTip="Please Select Subject 6 Grade" AppendDataBoundItems="True" meta:resourcekey="ddlOLGrade6Resource1">
                                                                    <asp:ListItem Value="0" meta:resourcekey="ListItemResource40">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="div3" runat="server">
                                                                <div class="label-dynamic">

                                                                    <label>O/L IndexNo</label>
                                                                </div>
                                                                <asp:TextBox ID="Txtolindex" runat="server" CssClass="form-control" TabIndex="48" onkeyup="IsNumeric(this);" meta:resourcekey="TxtolindexResource1"></asp:TextBox>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12" id="div6" runat="server">
                                                                <div class="label-dynamic">

                                                                    <label>School Name</label>
                                                                </div>
                                                                <asp:TextBox ID="txtolschool" runat="server" CssClass="form-control" TabIndex="49" meta:resourcekey="txtolschoolResource1"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="card">
                        <div class="card-header" data-toggle="collapse" data-target="#collapsefaculty" aria-expanded="true">
                            <span class="title">Faculty Details</span>
                            <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                        </div>
                        <div id="collapsefaculty" class="collapse show">
                            <div>
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updfinalblock" DisplayAfter="0">
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
                            <asp:UpdatePanel ID="updfinalblock" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="card-body">
                                        <div class="col-12">
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Student ID</label>
                                                    </div>
                                                    <asp:TextBox ID="txtEnrollmentNo" runat="server" TabIndex="50" ToolTip="Enter Student ID." Placeholder="Enter Student ID"
                                                        MaxLength="50" CssClass="form-control" meta:resourcekey="txtEnrollmentNoResource1" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Intake</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlIntake" runat="server" AppendDataBoundItems="True" TabIndex="51"
                                                        CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlIntake_SelectedIndexChanged" AutoPostBack="True" meta:resourcekey="ddlIntakeResource1">
                                                        <asp:ListItem Value="0" meta:resourcekey="ListItemResource41">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator39" runat="server" ControlToValidate="ddlIntake"
                                                        Display="None" ErrorMessage="Please Select Intake" InitialValue="0" SetFocusOnError="True" ValidationGroup="submit" meta:resourcekey="RequiredFieldValidator39Resource1"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Study Level</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlStudyLevel" runat="server" AppendDataBoundItems="True" TabIndex="52" data-select2-enable="true"
                                                        CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlStudyLevel_SelectedIndexChanged" meta:resourcekey="ddlStudyLevelResource1">
                                                        <asp:ListItem Value="0" meta:resourcekey="ListItemResource42">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator40" runat="server" ControlToValidate="ddlStudyLevel"
                                                        Display="None" ErrorMessage="Please Select Study Level" InitialValue="0" SetFocusOnError="True" ValidationGroup="submit" meta:resourcekey="RequiredFieldValidator40Resource1"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <sup></sup>
                                                        <label>Faculty/School Name</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlFacultySchoolName" runat="server" AppendDataBoundItems="True" TabIndex="53" AutoPostBack="True"
                                                        CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlFacultySchoolName_SelectedIndexChanged" meta:resourcekey="ddlFacultySchoolNameResource1">
                                                        <asp:ListItem Value="0" meta:resourcekey="ListItemResource43">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvFaculty" runat="server" ControlToValidate="ddlFacultySchoolName"
                                                        Display="None" ErrorMessage="Please Select Faculty/School Name" InitialValue="0" SetFocusOnError="True" ValidationGroup="submit" meta:resourcekey="rfvFacultyResource1"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Program</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlProgram" runat="server" AppendDataBoundItems="True" TabIndex="54"
                                                        CssClass="form-control" data-select2-enable="true" AutoPostBack="True" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged" meta:resourcekey="ddlProgramResource1">
                                                        <asp:ListItem Value="0" meta:resourcekey="ListItemResource44">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvProgram" runat="server" ControlToValidate="ddlProgram"
                                                        Display="None" ErrorMessage="Please Select Program" InitialValue="0" SetFocusOnError="True" ValidationGroup="submit" meta:resourcekey="rfvProgramResource1"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Semester</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="True" TabIndex="55"
                                                        CssClass="form-control" data-select2-enable="true" AutoPostBack="True" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" meta:resourcekey="ddlSemesterResource1">
                                                        <asp:ListItem Value="0" meta:resourcekey="ListItemResource45">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" ControlToValidate="ddlSemester"
                                                        Display="None" ErrorMessage="Please Select Semester" InitialValue="0" SetFocusOnError="True" ValidationGroup="submit" meta:resourcekey="RequiredFieldValidator33Resource1"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Campus</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlCampus" runat="server" AppendDataBoundItems="True" TabIndex="56"
                                                        CssClass="form-control" data-select2-enable="true" meta:resourcekey="ddlCampusResource1">
                                                        <asp:ListItem Value="0" meta:resourcekey="ListItemResource46">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvCampus" runat="server" ControlToValidate="ddlCampus"
                                                        Display="None" ErrorMessage="Please Select Campus" InitialValue="0" SetFocusOnError="True" ValidationGroup="submit" meta:resourcekey="rfvCampusResource1"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Weekday/Weekend</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlWeekdayWeekend" runat="server" AppendDataBoundItems="True" TabIndex="57"
                                                        CssClass="form-control" data-select2-enable="true" meta:resourcekey="ddlWeekdayWeekendResource1">
                                                        <asp:ListItem Value="0" meta:resourcekey="ListItemResource47">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvWeekdayWeekend" runat="server" ControlToValidate="ddlWeekdayWeekend"
                                                        Display="None" ErrorMessage="Please Select Weekday/Weekend" InitialValue="0" SetFocusOnError="True" ValidationGroup="submit" meta:resourcekey="rfvWeekdayWeekendResource1"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <sup></sup>
                                                        <label>Sources</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlsource" runat="server" AppendDataBoundItems="True" TabIndex="58"
                                                        CssClass="form-control" data-select2-enable="true" meta:resourcekey="ddlsourceResource1">
                                                        <asp:ListItem Value="0" meta:resourcekey="ListItemResource48">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlsource"
                                                        Display="None" ErrorMessage="Please Select Sources" InitialValue="0" SetFocusOnError="True" ValidationGroup="submit" meta:resourcekey="RequiredFieldValidator9Resource1"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12" id="div4" runat="server">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Aptitude Marks</label>
                                                    </div>
                                                    <asp:TextBox ID="Txtaptimarks" runat="server" CssClass="form-control" TabIndex="59" onkeyup="IsNumeric(this);" meta:resourcekey="TxtaptimarksResource1"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="Txtaptimarks"
                                                        Display="None" ErrorMessage="Please Enter Aptitude Marks" SetFocusOnError="True" ValidationGroup="submit" meta:resourcekey="RequiredFieldValidator4Resource1"></asp:RequiredFieldValidator>
                                                </div>

                                            </div>
                                            <div id="paymentdetails" runat="server" visible="False">
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divdemand" runat="server" visible="False">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Receivable Amount</label>
                                                        </div>
                                                        <asp:TextBox ID="TxtDemand" runat="server" CssClass="form-control" TabIndex="60" meta:resourcekey="TxtDemandResource1" Enabled="false"></asp:TextBox>

                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="bank" runat="server" visible="False">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Bank</label>
                                                        </div>
                                                        <asp:DropDownList ID="ddlbankdetails" runat="server" AppendDataBoundItems="True" TabIndex="61"
                                                            CssClass="form-control" data-select2-enable="true" meta:resourcekey="ddlbankdetailsResource1">
                                                            <asp:ListItem Value="0" meta:resourcekey="ListItemResource49">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="ddlbankdetails"
                                                            Display="None" ErrorMessage="Please Select Bank" ValidationGroup="Add" InitialValue="0"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12" id="receipt" runat="server" visible="False">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Receipt No.</label>
                                                        </div>
                                                        <asp:TextBox ID="TxtReceipt" runat="server" CssClass="form-control" MaxLength="150" TabIndex="62" meta:resourcekey="TxtReceiptResource1"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="TxtReceipt"
                                                            Display="None" ErrorMessage="Please Enter Receipt No" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-2 col-md-6 col-12" id="amt" runat="server" visible="False">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>Amount Paid</label>
                                                        </div>
                                                        <asp:TextBox ID="Txtamt" runat="server" CssClass="form-control" MaxLength="150" TabIndex="63" onkeyup="IsNumeric(this);" meta:resourcekey="TxtamtResource1"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="Txtamt"
                                                            Display="None" ErrorMessage="Please Enter Amount Paid" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-2 col-md-6 col-12" id="fileupload" runat="server" visible="False">
                                                        <div class="label-dynamic">
                                                            <label>Upload Deposit Slip</label>
                                                        </div>
                                                        <asp:FileUpload ID="fileupload1" runat="server" TabIndex="64" meta:resourcekey="fileupload1Resource1" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="fileupload1"
                                                            Display="None" ErrorMessage="Please  Upload Deposit Slip" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                                        <br />
                                                        <span style="color: red; font-size: small">
                                                            <asp:Label ID="lblMsg" runat="server" Text="Image Size Not Greater Than 1MB and image format JPG,JPEG,PNG,PDF Allowed" meta:resourcekey="lblMsgResource1"></asp:Label>
                                                        </span>
                                                    </div>
                                                    <div class="form-group col-lg-2 col-md-2 col-2" id="btnadd" runat="server" visible="False">
                                                        <div class="label-dynamic">
                                                            <label></label>
                                                        </div>
                                                        <asp:Button ID="Add" runat="server" CssClass="btn btn-outline-info" Text="Add" OnClick="Add_Click" meta:resourcekey="AddResource1" ValidationGroup="Add" />
                                                        <asp:ValidationSummary ID="ValidationSummary5" runat="server" ValidationGroup="Add" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                    </div>
                                                    <div class="col-12" id="slipview" runat="server" visible="False">
                                                        <div class="col-12">
                                                            <asp:Panel ID="Panel3" runat="server" meta:resourcekey="Panel3Resource1">
                                                                <asp:ListView ID="Lvbank" runat="server">
                                                                    <LayoutTemplate>
                                                                        <div class="sub-heading">
                                                                            <h5>Deposit  Slips</h5>
                                                                        </div>
                                                                        <table class="table table-striped table-bordered nowrap" id="mytable1" style="width: 100%">
                                                                            <thead class="bg-light-blue">
                                                                                <tr>

                                                                                    <th style="text-align: center">Bank
                                                                                    </th>
                                                                                    <th style="text-align: center">Receipt No
                                                                                    </th>
                                                                                    <th style="text-align: center">Amount
                                                                                    </th>
                                                                                    <th style="text-align: center">Slip
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
                                                                                <asp:HiddenField runat="server" ID="hdfbank" Value='<%# Eval("BANKNAME") %>' />
                                                                                <%# Eval("BANKNAME") %>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:HiddenField runat="server" ID="hdnReceiptNo" Value='<%# Eval("REC_NO") %>' />
                                                                                <%# Eval("REC_NO") %>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:HiddenField runat="server" ID="hdnAmount" Value='<%# Eval("AMOUNT") %>' />
                                                                                <%# Eval("AMOUNT")%>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:HiddenField runat="server" ID="hdfReceipt" Value='<%# Eval("DOC_FILENAME") %>' />
                                                                                <asp:LinkButton ID="lnkViewslip" runat="server" CssClass="btn btn-outline-info" ToolTip='<%# Eval("DOC_FILENAME") %>' CommandArgument='<%# Eval("TEMP_DCR_NO") %>' CommandName='<%# Eval("DOC_FILENAME") %>' OnClick="lnkViewslip_Click" meta:resourcekey="lnkViewslipResource1"><i class="fa fa-eye"></i> View</asp:LinkButton>

                                                                            </td>
                                                                        </tr>

                                                                    </ItemTemplate>

                                                                </asp:ListView>
                                                            </asp:Panel>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-12 pl-0 pr-0">
                                                    <asp:Panel ID="pnllst" runat="server" meta:resourcekey="pnllstResource1">
                                                        <asp:ListView ID="lvSemester" runat="server" Visible="False">
                                                            <LayoutTemplate>
                                                                <div class="sub-heading">
                                                                    <h5>Deposit Slips</h5>
                                                                </div>
                                                                <table class="table table-striped table-bordered nowrap" id="mytable1" style="width: 100%">
                                                                    <thead class="bg-light-blue">
                                                                        <tr>

                                                                            <th style="text-align: center">Bank
                                                                            </th>
                                                                            <th style="text-align: center">Receipt No
                                                                            </th>
                                                                            <th style="text-align: center">Amount
                                                                            </th>
                                                                            <th style="text-align: center">Slip
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
                                                                        <asp:HiddenField runat="server" ID="hdnBank" Value='<%# Eval("ddlbank") %>' />
                                                                        <asp:HiddenField runat="server" ID="hdfbankno" Value='<%# Eval("Bank") %>' />
                                                                        <%# Eval("ddlbank") %>
                                                                    </td>
                                                                    <td style="text-align: center">
                                                                        <asp:HiddenField runat="server" ID="hdnReceiptNo" Value='<%# Eval("ReceiptNo") %>' />
                                                                        <%# Eval("ReceiptNo") %>
                                                                    </td>
                                                                    <td style="text-align: center">
                                                                        <asp:HiddenField runat="server" ID="hdnAmount" Value='<%# Eval("Amount") %>' />
                                                                        <%# Eval("Amount")%>
                                                                    </td>
                                                                    <td style="text-align: center">
                                                                        <asp:HiddenField runat="server" ID="hdfReceipt" Value='<%# Eval("Receipt") %>' />
                                                                        <asp:HiddenField runat="server" ID="hdfbyte" Value='<%# Eval("Byte") %>' />
                                                                        <%# Eval("Receipt")%>
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
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnSubmitStudentDetails" />
                                    <asp:PostBackTrigger ControlID="Lvbank" />
                                    <asp:PostBackTrigger ControlID="Add" />
                                    <asp:PostBackTrigger ControlID="lvSemester" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="card">
                        <div class="card-header" data-toggle="collapse" data-target="#collapseAdditionalDoc" aria-expanded="true">
                            <span class="title">Document Details</span>
                            <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                        </div>
                        <div id="collapseAdditionalDoc" class="collapse show">
                            <div class="card-body">
                                <div>
                                    <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updDocument" DisplayAfter="0">
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
                                <asp:UpdatePanel ID="updDocument" runat="server">
                                    <ContentTemplate>
                                        <div class="col-12" id="divAllCoursesFromHist" runat="server">
                                            <asp:Panel ID="Panel1" runat="server" meta:resourcekey="Panel1Resource1">
                                                <asp:ListView ID="lvDocument" runat="server">
                                                    <LayoutTemplate>
                                                        <div>
                                                            <div class="sub-heading">
                                                                <h6 style="color: red; font-size: small">* Please be ready with a scanned copy of the documents listed below. Select “Choose File” to upload a scanned copy of the relevant document. </h6>
                                                                <h6 style="color: red; font-size: small">* Press “Submit” after successful document upload.  </h6>
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
                                                                        <th>Uploaded
                                                                        </th>
                                                                        <th>View</th>


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
                                                                <asp:Label ID="lblDocNo" runat="server" Text='<%# Eval("DOCUMENTNO") %>' Visible="False" meta:resourcekey="lblDocNoResource1"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblname" runat="server" Text='<%# Eval("DOCUMENTNAME") %>' meta:resourcekey="lblnameResource1" /><br />
                                                                <asp:Label ID="lblImageFile" runat="server" Style="color: red" meta:resourcekey="lblImageFileResource1"></asp:Label>
                                                                <asp:Label ID="lblFileFormat" runat="server" Style="color: red" meta:resourcekey="lblFileFormatResource1"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:FileUpload ID="fuDocument" runat="server" CssClass="form-control" onchange="setUploadButtondoc(this)" TabIndex='<%# Container.DataItemIndex - 1 %>' meta:resourcekey="fuDocumentResource1" />
                                                            </td>

                                                            <td style="display: none">
                                                                <asp:LinkButton ID="lnkDocMappingDegree" runat="server" CssClass="btn btn-outline-info" CommandArgument='<%# Eval("DOCUMENTNO") %>' CommandName='<%# Eval("DEGREE_DOC") %>' Visible='<%# Eval("DEGREE_DOC").ToString() == string.Empty ? false : true %>' meta:resourcekey="lnkDocMappingDegreeResource1"><i class="fa fa-eye"></i> Download</asp:LinkButton>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lbluploadpic" runat="server" Visible="False" meta:resourcekey="lbluploadpicResource1"></asp:Label>
                                                                <asp:Label ID="lbluploadpdf" runat="server" Visible="False" meta:resourcekey="lbluploadpdfResource1"></asp:Label>
                                                            </td>
                                                            <th>
                                                                <asp:LinkButton ID="lnkViewDoc" runat="server" CssClass="btn btn-outline-info" CommandArgument='<%# Eval("DOCUMENTNO") %>' CommandName='<%# Eval("IDNO") %>' OnClick="lnkViewDoc_Click" Visible="False" meta:resourcekey="lnkViewDocResource1"><i class="fa fa-eye"></i> View</asp:LinkButton>

                                                            </th>


                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>

                                            </asp:Panel>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="lvDocument" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>

                </div>
                <div class="col-6 mt-3">
                    <div class="label-dynamic">
                        <sup></sup>
                        <label>Remark</label>
                    </div>
                    <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" CssClass="form-control" MaxLength="150" TabIndex="52" meta:resourcekey="txtRemarkResource1"></asp:TextBox>
                </div>
                <div class="col-12 btn-footer mt-3" runat="server" id="button">
                    <asp:Button ID="btnSubmitStudentDetails" runat="server" Text="Submit" CssClass="btn btn-outline-info" OnClick="btnSubmitStudentDetails_Click" TabIndex="53" ValidationGroup="submit" meta:resourcekey="btnSubmitStudentDetailsResource1" />
                    <asp:Button ID="btnreport" runat="server" Text="Report" CssClass="btn btn-outline-info" OnClick="btnreport_Click" Visible="False" TabIndex="54" meta:resourcekey="btnreportResource1" />
                    <asp:LinkButton ID="btncancel" runat="server" TabIndex="55" CssClass="btn btn-outline-danger" OnClick="btncancel_Click" meta:resourcekey="btncancelResource1">Cancel</asp:LinkButton>

                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="submit" DisplayMode="List" meta:resourcekey="ValidationSummary2Resource1" />
                </div>
                <div class="card d-none">
                    <div class="card-header collapsed" data-toggle="collapse" aria-expanded="false">
                        <span class="title">Payment Details</span>
                        <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                    </div>

                    <div class="collapse show">
                        <div class="card-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-2 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Amount </label>
                                        </div>
                                        <asp:TextBox ID="txtAmount" TabIndex="5" runat="server" MaxLength="10" CssClass="form-control" meta:resourcekey="txtAmountResource1"></asp:TextBox>
                                        <asp:HiddenField ID="hdfAmount" runat="server" Value="0" />
                                        <asp:HiddenField ID="hdfServiceCharge" runat="server" Value="0" />
                                        <asp:HiddenField ID="hdfTotalAmount" runat="server" Value="0" />
                                        <asp:Label ID="lblOrderIDERP" runat="server" Visible="False" meta:resourcekey="lblOrderIDERPResource1"></asp:Label>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Payment Option </label>
                                        </div>
                                        <asp:RadioButtonList ID="rdPaymentOption" runat="server" TabIndex="6" RepeatDirection="Horizontal" meta:resourcekey="rdPaymentOptionResource1">
                                            <asp:ListItem Text="&nbsp;Offline Payment &nbsp;&nbsp;" Value="0" meta:resourcekey="ListItemResource50"></asp:ListItem>
                                            <asp:ListItem Text="&nbsp;Online Payment" Value="1" meta:resourcekey="ListItemResource51"></asp:ListItem>
                                        </asp:RadioButtonList>

                                        <asp:Label ID="lblTotalFee" runat="server" Visible="False" meta:resourcekey="lblTotalFeeResource1"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Please Select Payment Option"
                                            ControlToValidate="rdPaymentOption" Display="None" ValidationGroup="btnpay" meta:resourcekey="RequiredFieldValidator10Resource1"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12 off-line-butn" id="divUploadgENChallan" runat="server">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Step 1 : </label>
                                        </div>
                                        <input type="button" id="btnGenerateChallan" tabindex="6" value="View Bank Details" class="btn btn-outline-info" />
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12 off-line-butn" id="divUploadChallan" runat="server">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Step 2 : </label>
                                        </div>
                                        <span id="myBtn" style="cursor: pointer" class="btn btn-outline-info BtnChallan">Upload Deposit Slip</span>
                                        <%--<asp:Button id="Button1" runat="server" style="cursor: pointer" Text="Upload Challan Copy" CssClass="btn-shadow btn-wide btn-pill btn-hover-shine btn btn-primary BtnChallan"/>--%>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnPayment" runat="server" TabIndex="6" ValidationGroup="btnpay" Text="Pay" CssClass="btn btn-outline-info" meta:resourcekey="btnPaymentResource1" />
                                <%--<asp:Button ID="btnSavePayLater" runat="server" TabIndex="6" Text="Save & Pay Later" CssClass="btn btn-outline-info" OnClientClick="return confirm('Are you sure you want to pay later?');" />--%>
                                <%--<asp:LinkButton ID="btnChallan" runat="server" TabIndex="7" Text="Print Offer Latter" CssClass="btn btn-outline-info d-none" />--%>
                                <%--<asp:LinkButton ID="btnApplicationForm" runat="server" TabIndex="8" Text="Print Application Form" CssClass="btn btn-outline-info" />--%>
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="btnpay" meta:resourcekey="ValidationSummary1Resource1" />
                            </div>
                            <div class="col-12">
                                <asp:ListView ID="lvPaymentDetails" runat="server">
                                    <LayoutTemplate>
                                        <div class="pt-2 table-responsive">
                                            <table class="table table-striped table-bordered nowrap mb-0" style="width: 100%;" id="testdata">
                                                <thead>
                                                    <tr>
                                                        <th scope="col">Action</th>
                                                        <th scope="col">Program</th>
                                                        <th scope="col">Aptitude Test</th>
                                                        <th scope="col">Application Fee</th>
                                                        <th scope="col">Order Id</th>
                                                        <th scope="col">Transaction Id</th>
                                                        <th scope="col">Paid Amount</th>
                                                        <th scope="col">Payment Date</th>
                                                        <th scope="col">Status</th>
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
                                                <asp:Label ID="lblRemove" runat="server" meta:resourcekey="lblRemoveResource1"></asp:Label>
                                                <asp:HiddenField ID="hdfRemoveDegree" runat="server" />

                                                <asp:Label ID="lblRemoveArch" runat="server" meta:resourcekey="lblRemoveArchResource1"></asp:Label>
                                                <asp:HiddenField ID="hdfRemoveArch" runat="server" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDegreeNames" runat="server" meta:resourcekey="lblDegreeNamesResource1"></asp:Label>

                                                <asp:Label ID="lblArchDegrees" runat="server" meta:resourcekey="lblArchDegreesResource1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblAptitutetest" runat="server" Text='<%# Eval("APTITUDE_TEST") %>' meta:resourcekey="lblAptitutetestResource1" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblAmount" runat="server" meta:resourcekey="lblAmountResource1"></asp:Label>
                                                <asp:Label ID="lblArchAmount" runat="server" meta:resourcekey="lblArchAmountResource1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblarea" runat="server" Text='<%# Eval("ORDER_ID") %>' meta:resourcekey="lblareaResource1" /></td>
                                            <td>
                                                <asp:Label ID="lblcollegename" runat="server" Text='<%# Eval("APTRANSACTIONID") %>' meta:resourcekey="lblcollegenameResource1" /></td>
                                            <td>
                                                <asp:Label ID="lbldegree" runat="server" Text='<%# Eval("AMOUNT") %>' meta:resourcekey="lbldegreeResource1" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblPayDate" runat="server" Text='<%# Eval("RECONDATE") %>' meta:resourcekey="lblPayDateResource1" />
                                            </td>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("PAY_STATUS") %>' ForeColor='<%# Eval("PAY_STATUS").ToString() == "PENDING" ? System.Drawing.Color.Red : System.Drawing.Color.Green %>' meta:resourcekey="Label1Resource1" />
                                            </td>
                                        </tr>

                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="myModal22" class="modal fade" role="dialog">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content" style="margin-top: -25px">
                            <div class="modal-body">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" style="margin-top: -18px">x</button>
                                </div>

                                <asp:Image ID="ImageViewer" runat="server" Width="100%" Height="500px" Visible="False" meta:resourcekey="ImageViewerResource1" />
                                <asp:Literal ID="ltEmbed" runat="server" meta:resourcekey="ltEmbedResource1" />
                                <%--  <iframe id="iframe1" runat="server" frameborder="0" width="100%" height="800px" visible="false"></iframe>--%>
                                <%--<iframe id="iframe1" runat="server" src="../FILEUPLOAD/Transcript Report.pdf" frameborder="0" width="100%" height="547px"></iframe>--%>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:UpdatePanel ID="updModelPopup" runat="server">
                    <ContentTemplate>
                        <div id="myModal33" class="modal fade" role="dialog" data-backdrop="static">
                            <div class="modal-dialog modal-lg">
                                <div class="modal-content" style="margin-top: -25px">
                                    <div class="modal-body">
                                        <div class="modal-header">
                                            <asp:LinkButton ID="lnkClose" runat="server" CssClass="close" Style="margin-top: -18px" OnClick="lnkClose_Click">x</asp:LinkButton>

                                            <%--<button type="button" class="close" data-dismiss="modal" style="margin-top: -18px">x</button>--%>
                                        </div>
                                        <asp:Image ID="ImageViewer1" runat="server" Width="100%" Height="500px" Visible="false" />
                                        <asp:Literal ID="ltEmbed1" runat="server" Visible="false" />
                                        <div id="imageViewerContainer" runat="server" visible="false"></div>
                                        <asp:HiddenField ID="hdfImagePath" runat="server" Visible="false" />
                                        <iframe style="width: 100%; height: 500px;" id="irm1" src="~/PopUp.aspx" runat="server"></iframe>
                                        <div class="modal-footer" style="height: 0px">
                                            <%--<button type="button" style="margin-top: -10px" class="btn btn-default" data-dismiss="modal">Close</button>--%>
                                            <asp:LinkButton ID="lnkCloseModel" runat="server" CssClass="btn btn-default" Style="margin-top: -10px" OnClick="lnkClose_Click">Close</asp:LinkButton>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="lnkClose" />
                        <asp:PostBackTrigger ControlID="lnkCloseModel" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <div id="modelBank" class="modal fade">
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
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
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
        <div class="modal-dialog modal-lg">

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
                                <asp:RadioButtonList ID="rdselect" runat="server" RepeatDirection="Horizontal" meta:resourcekey="rdselectResource1">
                                    <asp:ListItem Value="0" meta:resourcekey="ListItemResource52">Name</asp:ListItem>
                                    <asp:ListItem Value="1" meta:resourcekey="ListItemResource53">&amp;nbsp Emailid</asp:ListItem>
                                    <asp:ListItem Value="2" meta:resourcekey="ListItemResource54">&amp;nbsp Application No</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ErrorMessage="Please select Category"
                                    ControlToValidate="rdselect" Display="None" ValidationGroup="search" meta:resourcekey="RequiredFieldValidator24Resource1"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-md-12">
                                <div class="col-md-6">
                                    <label>Search String</label>
                                    <asp:TextBox ID="txtSearch" runat="server" meta:resourcekey="txtSearchResource1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ErrorMessage="Please Enter Deatils"
                                        ControlToValidate="txtSearch" Display="None" ValidationGroup="search" meta:resourcekey="RequiredFieldValidator25Resource1"></asp:RequiredFieldValidator>
                                </div>

                            </div>
                            <div class="col-md-12">
                                <p class="text-center">
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-outline-info" ValidationGroup="search" meta:resourcekey="btnSearchResource1" />

                                    <asp:Button ID="btnCancelModal" runat="server" Text="Cancel" OnClick="btnCancelModal_Click" CssClass="btn btn-outline-danger" meta:resourcekey="btnCancelModalResource1" />
                                    <button type="button" class="btn btn-outline-danger" data-dismiss="modal">Close</button>
                                    <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="search" meta:resourcekey="ValidationSummary3Resource1" />
                                    <div>
                                        <asp:Label ID="lblNoRecords" runat="server" SkinID="lblmsg" meta:resourcekey="lblNoRecordsResource1" />
                                    </div>
                                    <div>
                                        <asp:UpdateProgress ID="updProgress" runat="server" AssociatedUpdatePanelID="updEdit">
                                            <ProgressTemplate>
                                                <asp:Image ID="imgProg" runat="server" ImageUrl="~/images/ajax-loader.gif" meta:resourcekey="imgProgResource1" />
                                                Loading.. Please Wait!
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </div>
                                    <div class="col-md-12">
                                        <asp:Panel ID="Panel5" runat="server" ScrollBars="Auto" meta:resourcekey="Panel5Resource1">
                                            <asp:ListView ID="lvStudent" runat="server">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Login Details</h5>
                                                    </div>
                                                    <asp:Panel ID="Panel2" runat="server" Height="300px" ScrollBars="Auto" meta:resourcekey="Panel2Resource1">
                                                        <table class="table table-hover table-bordered">
                                                            <thead>
                                                                <tr class="bg-light-blue">
                                                                    <th>Student Name </th>
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
                                                            <asp:LinkButton ID="lnkId" runat="server" CommandArgument='<%# Eval("IDNo") %>' OnClick="lnkId_Click" Text='<%# Eval("Name") %>' ToolTip='<%# Eval("USERNO") %>'></asp:LinkButton>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblstuenrollno" runat="server" Text='<%# Eval("REGNO") %>' ToolTip='<%# Eval("IDNO") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblstudentfullname" runat="server" Text='<%# Eval("longname") %>' ToolTip='<%# Eval("USERNO") %>'></asp:Label>
                                                        </td>
                                                        <td><%# Eval("SEMESTERNAME")%></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>
                                    </div>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                </p>

                            </div>

                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="lvStudent" />
                        <%--<asp:AsyncPostBackTrigger ControlID="btnSearch" />--%>

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

    <div id="myModalChallan" class="modal fade" role="dialog">
        <div class="modal-dialog modal-xl">
            <!-- Modal content-->

            <%--<asp:UpdatePanel ID="updChallan" runat="server">
                <ContentTemplate>--%>
            <div class="modal-content">
                <div class="modal-header">
                    <h4>Upload Deposit</h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-4 col-md-6 col-12" id="divTransactionid" runat="server" visible="false">
                                <div class="label-dynamic">
                                    <label>Transaction Id</label>
                                </div>
                                <asp:TextBox ID="txtTransactionNo" TabIndex="1" runat="server" MaxLength="20" CssClass="form-control" meta:resourcekey="txtTransactionNoResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator75" runat="server" ErrorMessage="Please Enter Challan Transaction Id"
                                    ControlToValidate="txtTransactionNo" Display="None" ValidationGroup="SubmitChallan" meta:resourcekey="RequiredFieldValidator75Resource1"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-lg-4 col-md-6 col-12" id="DivOrderId" runat="server" visible="false">
                                <div class="label-dynamic">
                                    <label>Challan Order ID</label>
                                </div>
                                <label for="UserName"><sup></sup></label>
                                <asp:TextBox ID="txtChallanId" runat="server" MaxLength="20" CssClass="form-control" meta:resourcekey="txtChallanIdResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator76" runat="server" ErrorMessage="Please Enter Challan Order ID"
                                    ControlToValidate="txtChallanId" Display="None" ValidationGroup="SubmitChallan" meta:resourcekey="RequiredFieldValidator76Resource1"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-lg-4 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Bank</label>
                                </div>
                                <asp:DropDownList ID="ddlbank" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" meta:resourcekey="ddlbankResource1">
                                    <asp:ListItem Value="0" meta:resourcekey="ListItemResource55">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator77" runat="server" ErrorMessage="Please Select Bank Name"
                                    ControlToValidate="ddlbank" Display="None" InitialValue="0" ValidationGroup="SubmitChallan" meta:resourcekey="RequiredFieldValidator77Resource1"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-lg-4 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Branch</label>
                                </div>
                                <asp:TextBox ID="txtBranchName" runat="server" CssClass="form-control" TabIndex="3" MaxLength="50" meta:resourcekey="txtBranchNameResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator78" runat="server" ErrorMessage="Please Enter Bank Branch"
                                    ControlToValidate="txtBranchName" Display="None" ValidationGroup="SubmitChallan" meta:resourcekey="RequiredFieldValidator78Resource1"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-lg-4 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Amount</label>
                                </div>
                                <asp:TextBox ID="txtchallanAmount" TabIndex="4" runat="server" MaxLength="10" CssClass="form-control" meta:resourcekey="txtchallanAmountResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator79" runat="server" ErrorMessage="Please Enter Amount"
                                    ControlToValidate="txtchallanAmount" Display="None" ValidationGroup="SubmitChallan" meta:resourcekey="RequiredFieldValidator79Resource1"></asp:RequiredFieldValidator>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender24" runat="server" TargetControlID="txtchallanAmount" ValidChars="1234567890." Enabled="True" />
                            </div>
                            <div class="form-group col-lg-4 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Date of Payment</label>
                                </div>
                                <asp:TextBox ID="txtPaymentdate" TabIndex="5" runat="server" MaxLength="20" CssClass="form-control dob" meta:resourcekey="txtPaymentdateResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator80" runat="server" ErrorMessage="Please Enter Date of Payment"
                                    ControlToValidate="txtPaymentdate" Display="None" ValidationGroup="SubmitChallan" meta:resourcekey="RequiredFieldValidator80Resource1"></asp:RequiredFieldValidator>
                            </div>

                        </div>
                    </div>
                    <div class="col-12 text-center">
                        <asp:Button ID="btnChallanSubmit" runat="server" TabIndex="7" Text="Submit" CssClass="btn-shadow btn-wide btn-pill btn-hover-shine btn btn-outline-info" ValidationGroup="SubmitChallan" meta:resourcekey="btnChallanSubmitResource1" />
                        <asp:ValidationSummary ID="ValidationSummary10" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="SubmitChallan" meta:resourcekey="ValidationSummary10Resource1" />
                    </div>

                    <div class="col-12 mb-3">
                        <asp:ListView ID="lvDepositSlip" runat="server">
                            <LayoutTemplate>
                                <div class="sub-heading">
                                    <h5>Deposit Slip Detail</h5>
                                </div>
                                <div class="table table-responsive">
                                    <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                        <thead>
                                            <tr class="bg-light-blue">
                                                <td>SrNo</td>
                                                <td>Bank Name</td>
                                                <td>Bank Branch</td>
                                                <td>Amount</td>
                                                <td>Date of Payment</td>
                                                <td>View</td>
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
                                    <td><%# Eval("BANK_NAME") %></td>
                                    <td><%# Eval("BRANCH_NAME") %></td>
                                    <td><%# Eval("AMOUNT") %></td>
                                    <td><%# Eval("RECON_DATE") %></td>
                                    <td>
                                        <asp:LinkButton ID="lnkView" runat="server" CommandArgument='<%# Eval("DOC_FILENAME") %>' meta:resourcekey="lnkViewResource1"><i class="fa fa-eye"></i></asp:LinkButton></td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>

                    </div>
                </div>
            </div>
            <%--</ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnChallanSubmit" />
                </Triggers>
            </asp:UpdatePanel>--%>
        </div>
    </div>
    <script>
        $(document).ready(function () {
            var dateval = document.getElementById('<%=txtDateOfBirth.ClientID%>').value;
            var prev_date = new Date();
            prev_date.setDate(prev_date.getDate() - 1);
            $('.dobPersonal').daterangepicker({
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
                $('.dobPersonal').val('');
            }
        });

        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(function () {
                var dateval = document.getElementById('<%=txtDateOfBirth.ClientID%>').value;
                var prev_date = new Date();
                prev_date.setDate(prev_date.getDate() - 1);
                $('.dobPersonal').daterangepicker({
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
                    $('.dobPersonal').val('');
                }

            });
        });


    </script>
    <script>
        $(document).ready(function () {
            var curect_file_path = $('#ctl00_ContentPlaceHolder1_hdfImagePath').val();
            $("#ctl00_ContentPlaceHolder1_imageViewerContainer").verySimpleImageViewer({
                imageSource: curect_file_path,
                frame: ['100%', '100%'],
                maxZoom: '900%',
                zoomFactor: '10%',
                mouse: true,
                keyboard: true,
                toolbar: true,
                rotateToolbar: true
            });
        });
    </script>

    <script>
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                var curect_file_path = $('#ctl00_ContentPlaceHolder1_hdfImagePath').val();
                $("#ctl00_ContentPlaceHolder1_imageViewerContainer").verySimpleImageViewer({
                    imageSource: curect_file_path,
                    frame: ['100%', '100%'],
                    maxZoom: '900%',
                    zoomFactor: '10%',
                    mouse: true,
                    keyboard: true,
                    toolbar: true,
                    rotateToolbar: true
                });
            });
        });
    </script>

    <script>
        function RemoveHomeCountryName() {

            $("#select2-ddlHomeMobileCode-container").html($("#select2-ddlHomeMobileCode-container").html().split('-')[0]);
        }
    </script>
    <script>
        function RemoveCountryName() {
            $("#select2-ddlMobileCode-container").html($("#select2-ddlMobileCode-container").html().split('-')[0]);
        }
    </script>
    <script>
        $(document).ready(function () {

            $(".off-line-butn").hide();
            $("#myBtn").hide();
            $("#btnGenerateChallan").hide();
            $("#<%=btnPayment.ClientID%>").hide();

            $("#ctl00_ContentPlaceHolder1_rdPaymentOption").click(function () {
                var radioValue = $('#<%=rdPaymentOption.ClientID %> input[type=radio]:checked').val();
                if (radioValue == 0) {
                    $("#<%=btnPayment.ClientID%>").hide();
                    $("#btnGenerateChallan").show();
                    $("#myBtn").show();
                    $(".off-line-butn").show();
                }
                else {
                    $(".off-line-butn").hide();
                    $("#myBtn").hide();
                    $("#btnGenerateChallan").hide();
                    $("#<%=btnPayment.ClientID%>").show();
                }
            });
        });
    </script>
    <script>

        $(document).ready(function () {
            var parameter = Sys.WebForms.PageRequestManager.getInstance();
            parameter.add_endRequest(function () {
                $(".off-line-butn").hide();
                $("#myBtn").hide();
                $("#btnGenerateChallan").hide();
                $("#<%=btnPayment.ClientID%>").hide();
                if ($('#<%=rdPaymentOption.ClientID %> input[type=radio]:checked').val() == 0) {
                    $("#<%=btnPayment.ClientID%>").hide();
                    $("#btnGenerateChallan").show();
                    $("#myBtn").show();
                    $(".off-line-butn").show();
                }
                else {
                    $(".off-line-butn").hide();
                    $("#myBtn").hide();
                    $("#btnGenerateChallan").hide();
                    $("#<%=btnPayment.ClientID%>").show();
                }
                $("#ctl00_ContentPlaceHolder1_rdPaymentOption").click(function () {
                    var radioValue = $('#<%=rdPaymentOption.ClientID %> input[type=radio]:checked').val();
                    if (radioValue == 0) {
                        $("#<%=btnPayment.ClientID%>").hide();
                        $("#btnGenerateChallan").show();
                        $("#myBtn").show();
                        $(".off-line-butn").show();
                    }
                    else {
                        $(".off-line-butn").hide();
                        $("#myBtn").hide();
                        $("#btnGenerateChallan").hide();
                        $("#<%=btnPayment.ClientID%>").show();
                    }
                });
            });
        });
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#btnGenerateChallan").click(function () {
                $("#modelBank").modal();

            });
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            var parameter = Sys.WebForms.PageRequestManager.getInstance();
            parameter.add_endRequest(function () {
                $("#btnGenerateChallan").click(function () {
                    $("#modelBank").modal();

                });
            });
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".BtnChallan").click(function () {
                $("#myModalChallan").modal();

            });
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            var parameter = Sys.WebForms.PageRequestManager.getInstance();
            parameter.add_endRequest(function () {
                $(".BtnChallan").click(function () {
                    $("#myModalChallan").modal();

                });
            });
        });
    </script>

    <script>
        $(document).ready(function () {
            var dateval = document.getElementById('<%=txtPaymentdate.ClientID%>').value;
            var prev_date = new Date();
            prev_date.setDate(prev_date.getDate());
            $('.dob').daterangepicker({
                singleDatePicker: true,
                showDropdowns: true,
                minDate: '01/1/1975',
                maxDate: prev_date,
                locale: {
                    format: 'DD/MM/YYYY'
                },
                autoApply: true,
            });
            if (dateval == "") {
                $('.dob').val('');
            }
        });
    </script>
    <script type="text/javascript">

        function CountCharactersPerment() {
            var maxSize = 200;

            if (document.getElementById('<%= txtPermAddress.ClientID %>')) {
                var ctrl = document.getElementById('<%= txtPermAddress.ClientID %>');
                var len = document.getElementById('<%= txtPermAddress.ClientID %>').value.length;
                if (len <= maxSize) {
                    return;
                }
                else {
                    alert("Max Of length Should be only 200 Characters ");
                    ctrl.value = ctrl.value.substring(0, maxSize);

                }
            }

            return false;
        }
    </script>
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
            //alert(tabValue)
            if (tabValue == "-1") {
                if ($.inArray($('#ctl00_ContentPlaceHolder1_lvDocument_ctrl0_fuDocument').val().split('.').pop().toLowerCase(), fileExtension) == -1) {
                    alert("Only formats are allowed : " + fileExtension.join(', '));

                    $("#ctl00_ContentPlaceHolder1_lvDocument_ctrl0_fuDocument").val("");
                }
            }
            else {
                if ($.inArray($(chk).val().replace(',', '.').split('.').pop().toLowerCase(), fileExtension1) == -1) {
                    alert("Only formats are allowed : " + fileExtension1.join(', '));
                    //alert(tabValue)
                    $(chk).val("");
                }
            }
        }
    </script>
    <script>
        $(document).ready(function () {
            $(".right-container-button").hover(function () {
                $(".long-text").addClass("show-long-text");
            }, function () {
                $(".long-text").removeClass("show-long-text");
            });
        });
    </script>
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
    <script>
        $(document).ready(function () {
            $("[id*=ddlstatus]").bind("change", function () {
                var List = $(this).closest("table");
                var ddlValue = $(this).val();
                if (ddlValue == "1") {
                    var td = $("td", $(this).closest("tr"));
                    $("[id*=txtremark]", td).val("Verified");

                }

                else if (ddlValue == "2") {
                    var td = $("td", $(this).closest("tr"));
                    $("[id*=txtremark]", td).val("Not Verified");
                }
                else if (ddlValue == "3") {
                    var td = $("td", $(this).closest("tr"));
                    $("[id*=txtremark]", td).val("Pending");
                }
                else if (ddlValue == "4") {
                    var td = $("td", $(this).closest("tr"));
                    $("[id*=txtremark]", td).val("Incomplete");
                }
                else {
                    var td = $("td", $(this).closest("tr"));
                    $("[id*=txtremark]", td).val('');


                }
            });
        });
    </script>

    <script>
        $(document).ready(function () {
            var parameter = Sys.WebForms.PageRequestManager.getInstance();
            parameter.add_endRequest(function () {
                $("[id*=ddlstatus]").bind("change", function () {
                    var List = $(this).closest("table");
                    var ddlValue = $(this).val();
                    if (ddlValue == "1") {
                        var td = $("td", $(this).closest("tr"));
                        $("[id*=txtremark]", td).val("Verified");

                    }

                    else if (ddlValue == "2") {
                        var td = $("td", $(this).closest("tr"));
                        $("[id*=txtremark]", td).val("Not Verified");
                    }
                    else if (ddlValue == "3") {
                        var td = $("td", $(this).closest("tr"));
                        $("[id*=txtremark]", td).val("Pending");
                    }
                    else if (ddlValue == "4") {
                        var td = $("td", $(this).closest("tr"));
                        $("[id*=txtremark]", td).val("Incomplete");
                    }
                    else {
                        var td = $("td", $(this).closest("tr"));
                        $("[id*=txtremark]", td).val('');


                    }
                });
            });
        });
    </script>
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
    <script>
        function IsNumeric(textbox) {
            if (textbox != null && textbox.value != "") {
                if (isNaN(textbox.value)) {
                    //document.getElementById(textbox.id).value = 0;
                    alert("Enter Only Numeric Value")
                    document.getElementById(textbox.id).value = '';
                }
            }
        }
    </script>

    <script>
        var summary = "";

        var summary = "";
        $(function () {

            $('#btnSubmitStudentDetails').click(function () {
                localStorage.setItem("currentId", "#btnSubmitStudentDetails,Submit");
                debugger;
                ShowLoader('#btnSubmitStudentDetails');
                if ($('#alolselection').val() == "")
                    summary += '<br>Please Select Education Details';
                if ($('#txtFirstName').val() == "")
                    summary += '<br>Please Enter First Name';
                if ($('#txtLastName').val() == "")
                    summary += '<br>Please Enter Last Name';
                if ($('#txtNameInitial').val() == "")
                    summary += '<br>Please Enter Name With Initials';
                if ($('#txtEmail').val() == "")
                    summary += '<br>Please Enter Email';
                if ($('#ddlMobileCode').val() == "0")
                    summary += '<br>Please Select Mobile Code';
                if ($('#ddlHomeMobileCode').val() == "0")
                    summary += '<br>Please Select Home Telephone  Code';
                if ($('#txtHomeTel').val() == "")
                    summary += '<br>Please Enter Home Telephone No.';
                if ($('#ddlbankdetails').val() == "0")
                    summary += '<br>Please Select  Bank';
                if ($('#TxtReceipt').val() == "0")
                    summary += '<br>Please Enter Receipt No';
                if ($('#Txtamt').val() == "0")
                    summary += '<br>Please Enter Amount';
                //if ($('#txtNIC').val() == "")
                //    summary += '<br>Please Enter NIC (National Identity card)';
                //if ($('#txtPassportNo').val() == "")
                //    summary += '<br>Please Enter Passport No';
                if ($('#txtPermAddress').val() == "")
                    summary += '<br>Please Enter Address';
                if ($('#ddlCountry').val() == "0")
                    summary += '<br>Please Select Country';
                if ($('#ddlProvince').val() == "0")
                    summary += '<br>Please Select Province';
                if ($('#ddlDistrict').val() == "0")
                    summary += '<br>Please Select District';
                if ($('#ddlALExamType').val() == "0")
                    summary += '<br>Please Select A/L Exam Type';
                if ($('#ddlALStream').val() == "0")
                    summary += '<br>Please Select A/L Stream';
                if ($('#ddlALPasses').val() == "0")
                    summary += '<br>Please Select A/L Passes';
                if ($('#ddlALPasses').val() != "1") {
                    if ($('#ddlALSubject1').val() == "0")
                        summary += '<br>Please Select A/L Subject 1';
                    if ($('#ddlALGrade1').val() == "0")
                        summary += '<br>Please Select A/L Grade 1';
                    if ($('#ddlALSubject2').val() == "0")
                        summary += '<br>Please Select A/L Subject 2';
                    if ($('#ddlALGrade2').val() == "0")
                        summary += '<br>Please Select A/L Grade 2';
                    if ($('#ddlALSubject3').val() == "0")
                        summary += '<br>Please Select A/L Subject 3';
                    if ($('#ddlALGrade3').val() == "0")
                        summary += '<br>Please Select A/L Grade 3';
                    if ($('#ddlALSubject4').val() == "0")
                        summary += '<br>Please Select A/L Subject 4';
                    if ($('#ddlALGrade4').val() == "0")
                        summary += '<br>Please Select A/L Grade 4';

                }

                //if ($('#txtEnrollmentNo').val() == "")
                //    summary += '<br>Please Student ID';
                if ($('#ddlbankdetails').val() == "0")
                    summary += '<br>Please Select  Bank';
                if ($('#TxtReceipt').val() == "0")
                    summary += '<br>Please Enter Receipt No';
                if ($('#Txtamt').val() == "0")
                    summary += '<br>Please Enter Amount';
                if ($('#ddlFacultySchoolName').val() == "0")
                    summary += '<br>Please Select  Faculty/School Name';
                if ($('#ddlStudyLevel').val() == "0")
                    summary += '<br>Please Select  Study Level';
                if ($('#ddlProgram').val() == "0,0")
                    summary += '<br>Please Select  Program';
                if ($('#ddlCampus').val() == "0")
                    summary += '<br>Please Select  Campus';
                if ($('#ddlIntake').val() == "0")
                    summary += '<br>Please Select  Intake';
                if ($('#ddlWeekdayWeekend').val() == "0")
                    summary += '<br>Please Select  Weekday Weekend';
                if ($('#ddlSemester').val() == "0")
                    summary += '<br>Please Select  ddlSemester';
                if (summary != "") {
                    customAlert(summary);
                    summary = "";
                    return false
                }


            });
        });
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {

                $('#btnSubmitStudentDetails').click(function () {
                    localStorage.setItem("currentId", "#btnSubmitStudentDetails,Submit");
                    debugger;
                    ShowLoader('#btnSubmitStudentDetails');
                    if ($('#alolselection').val() == "")
                        summary += '<br>Please Select Education Details';
                    if ($('#txtFirstName').val() == "")
                        summary += '<br>Please Enter First Name';
                    if ($('#txtLastName').val() == "")
                        summary += '<br>Please Enter Last Name';
                    if ($('#txtNameInitial').val() == "")
                        summary += '<br>Please Enter Name With Initials';
                    if ($('#txtEmail').val() == "")
                        summary += '<br>Please Enter Email';
                    if ($('#ddlMobileCode').val() == "0")
                        summary += '<br>Please Select Mobile Code';
                    if ($('#ddlHomeMobileCode').val() == "0")
                        summary += '<br>Please Select Home Telephone  Code';
                    if ($('#txtHomeTel').val() == "")
                        summary += '<br>Please Enter Home Telephone No.';
                    //if ($('#txtNIC').val() == "")
                    //    summary += '<br>Please Enter NIC (National Identity card)';
                    //if ($('#txtPassportNo').val() == "")
                    //    summary += '<br>Please Enter Passport No';
                    if ($('#txtPermAddress').val() == "")
                        summary += '<br>Please Enter Address';
                    if ($('#ddlCountry').val() == "0")
                        summary += '<br>Please Select Country';

                    if ($('#ddlProvince').val() == "0")
                        summary += '<br>Please Select Province';
                    if ($('#ddlDistrict').val() == "0")
                        summary += '<br>Please Select District';
                    if ($('#ddlALExamType').val() == "0")
                        summary += '<br>Please Select A/L Exam Type';
                    if ($('#ddlALStream').val() == "0")
                        summary += '<br>Please Select A/L Stream';
                    if ($('#ddlALPasses').val() == "0")
                        summary += '<br>Please Select A/L Passes';
                    if ($('#ddlALPasses').val() != "1") {
                        if ($('#ddlALSubject1').val() == "0")
                            summary += '<br>Please Select A/L Subject 1';
                        if ($('#ddlALGrade1').val() == "0")
                            summary += '<br>Please Select A/L Grade 1';
                        if ($('#ddlALSubject2').val() == "0")
                            summary += '<br>Please Select A/L Subject 2';
                        if ($('#ddlALGrade2').val() == "0")
                            summary += '<br>Please Select A/L Grade 2';
                        if ($('#ddlALSubject3').val() == "0")
                            summary += '<br>Please Select A/L Subject 3';
                        if ($('#ddlALGrade3').val() == "0")
                            summary += '<br>Please Select A/L Grade 3';
                        if ($('#ddlALSubject4').val() == "0")
                            summary += '<br>Please Select A/L Subject 4';
                        if ($('#ddlALGrade4').val() == "0")
                            summary += '<br>Please Select A/L Grade 4';


                    }
                    //if ($('#txtEnrollmentNo').val() == "")
                    //    summary += '<br>Please Enter  Student ID';

                    if ($('#ddlFacultySchoolName').val() == "0")
                        summary += '<br>Please Select  Faculty/School Name';
                    if ($('#ddlStudyLevel').val() == "0")
                        summary += '<br>Please Select  Study Level';
                    if ($('#ddlProgram').val() == "0,0")
                        summary += '<br>Please Select  Program';
                    if ($('#ddlCampus').val() == "0")
                        summary += '<br>Please Select  Campus';
                    if ($('#ddlIntake').val() == "0")
                        summary += '<br>Please Select  Intake';
                    if ($('#ddlWeekdayWeekend').val() == "0")
                        summary += '<br>Please Select  Weekday Weekend';
                    if ($('#ddlSemester').val() == "0")
                        summary += '<br>Please Select  ddlSemester';

                    if (summary != "") {
                        customAlert(summary);
                        summary = "";
                        return false
                    }

                });
            });
        });
    </script>
    <script language="javascript" type="text/javascript">
        function alphaOnly(e) {
            var code;
            if (!e) var e = window.event;

            if (e.keyCode) code = e.keyCode;
            else if (e.which) code = e.which;

            if ((code >= 48) && (code <= 57)) { return false; }
            return true;
        }
    </script>

</asp:Content>
