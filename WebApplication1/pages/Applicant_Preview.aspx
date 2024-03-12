<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Applicant_Preview.aspx.cs" Inherits="Applicant_Preview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div id="div1" runat="server"></div>
                <div class="box-header with-border">
                    <h3 class="box-title">Applicant Preview</h3>
                </div>

                <div class="box-body">
                    <div class="col-12 btn-footer d-none">
                        <asp:LinkButton ID="btnBack" runat="server" CssClass="btn btn-outline-danger">Back</asp:LinkButton>
                    </div>

                    <div class="accordion" id="accordionExample">
                        <div class="card">
                            <div class="card-header" data-toggle="collapse" data-target="#collapseOne" aria-expanded="true">
                                <span class="title">Offer Acceptance </span>
                                <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                            </div>
                            <div id="collapseOne" class="collapse show">
                                <div class="card-body">
                                    <div class="col-12">
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

                                                <div class="form-group col-lg-3 col-md-6 col-12" id="DIVPRGM" runat="server" visible="false">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <label>Program Name</label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlProgramName" CssClass="form-control" data-select2-enable="true" runat="server" AppendDataBoundItems="true">
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
                                            </div>
                                        </div>
                                    </div>

                                    <asp:Panel ID="pnlProgramName" runat="server" Visible="true">
                                        <asp:ListView ID="lstProgramName" runat="server">
                                            <LayoutTemplate>
                                                <div>
                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="col-12">
                                                                <div class="sub-heading">
                                                                    <h5>Program Name</h5>
                                                                </div>
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
                                                    </div>
                                                </div>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:CheckBox ID="chkRowsProgram" runat="server" Enabled="false" onclick="CheckUnchekCheckbox(this);" AutoPostBack="true" Checked='<%# Convert.ToInt32(Eval("DM_NO")) > 0 ? true:false   %>' />
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
                            </div>

                        </div>

                        <div class="card">
                            <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                                <span class="title">Application Details</span>
                                <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                            </div>
                            <div id="collapseTwo" class="collapse show">
                                <div class="card-body">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="col-12">
                                                <div class="sub-heading">
                                                    <h5>Personal Information</h5>
                                                </div>
                                            </div>
                                            <div class="col-lg-4 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>First Name :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblFirstNameP" Font-Bold="true" runat="server" />
                                                        </a>
                                                    </li>
                                                    <li class="list-group-item"><b>Last Name :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblLastNameP" Font-Bold="true" runat="server" /></a>
                                                    </li>
                                                    <li class="list-group-item" style="display: none"><b>Name with Initials :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblInitialName" Font-Bold="true" runat="server" /></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Email :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblEmailP" Font-Bold="true" runat="server" /></a>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div class="col-lg-4 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Mobile No. :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblMobileNoP" Font-Bold="true" runat="server" />
                                                        </a>
                                                    </li>
                                                    <li class="list-group-item" style="display: none"><b>NIC (National Identity card) :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblNIC" Font-Bold="true" runat="server" /></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Passport No :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblPassPortNo" Font-Bold="true" runat="server" /></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Semester :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblSemesterP" Font-Bold="true" runat="server" /></a>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div class="col-lg-4 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Date Of Birth :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblDOB" Font-Bold="true" runat="server" /></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Gender :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblGender" Font-Bold="true" runat="server" /></a>
                                                    </li>
                                                    <li class="list-group-item"><b>Citizen Type :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblCitizenType" Font-Bold="true" runat="server" /></a>
                                                    </li>
                                                    <li class="list-group-item" style="display: none"><b>Left / Right Handed :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblLRHanded" Font-Bold="true" runat="server" /></a>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>

                                        <div class="row mt-3">
                                            <div class="col-12">
                                                <div class="sub-heading">
                                                    <h5>Address Details</h5>
                                                </div>
                                            </div>
                                            <div class="col-lg-4 col-md-12 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Address :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblAddress" Font-Bold="true" runat="server" />
                                                        </a>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div class="col-lg-4 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>Country :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblCountry" Font-Bold="true" runat="server" />
                                                        </a>
                                                    </li>
                                                    <li class="list-group-item"><b>Province :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblProvince" Font-Bold="true" runat="server" />
                                                        </a>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div class="col-lg-4 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>District :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblDistrict" Font-Bold="true" runat="server" />
                                                        </a>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>

                                        <div id="divUgEducationUG" runat="server" class="mt-3">
                                            <%--<div>
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
                                            </div>--%>
                                            <%--<asp:UpdatePanel ID="updEdcutationalDetailsUG" runat="server">
                                                <ContentTemplate>--%>
                                            <div id="Div39" runat="server" visible="false">
                                                <div id="UG" runat="server" visible="false">
                                                    <div class="row">
                                                        <div class="col-12">
                                                            <div class="sub-heading">
                                                                <h5>Educational Details</h5>
                                                            </div>
                                                        </div>
                                                        <div class="col-12">
                                                            <div class="sub-heading">
                                                                <h5>A/L Details</h5>
                                                            </div>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>A/L Syllabus</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlALTypeUG" runat="server" AppendDataBoundItems="true" Enabled="false" CssClass="form-control" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>A/L Stream</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlStreamUG" runat="server" Enabled="false" CssClass="form-control" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>A/L passes</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlALPassesUG" runat="server" Enabled="false" CssClass="form-control" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Subject 1</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlSubject1" AppendDataBoundItems="true" runat="server" Enabled="false" CssClass="form-control" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Grade</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlGrade1" runat="server" AppendDataBoundItems="true" Enabled="false" CssClass="form-control" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Subject 2</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlSubject2" AppendDataBoundItems="true" runat="server" Enabled="false" CssClass="form-control" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Grade</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlGrade2" AppendDataBoundItems="true" runat="server" Enabled="false" CssClass="form-control" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Subject 3</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlSubject3" AppendDataBoundItems="true" runat="server" Enabled="false" CssClass="form-control" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="div7" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Grade</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlGrade3" AppendDataBoundItems="true" runat="server" Enabled="false" CssClass="form-control" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divbiology" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Subject 4</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlSubject4" AppendDataBoundItems="true" runat="server" Enabled="false" CssClass="form-control" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="div8" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Grade</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlGrade4" AppendDataBoundItems="true" runat="server" Enabled="false" CssClass="form-control" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="div3" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>A/L IndexNo</label>
                                                            </div>
                                                            <asp:TextBox ID="txtALIndex" runat="server" CssClass="form-control" MaxLength="20" Enabled="false"></asp:TextBox>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="div4" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>A/L Year</label>
                                                            </div>
                                                            <asp:TextBox ID="txtALyear" runat="server" CssClass="form-control" MaxLength="4" Enabled="false"></asp:TextBox>

                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="div5" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Z Score</label>
                                                            </div>
                                                            <asp:TextBox ID="txtZScore" runat="server" CssClass="form-control" MaxLength="5" Enabled="false"></asp:TextBox>

                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="div6" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>A/L School District</label>
                                                            </div>
                                                            <asp:TextBox ID="txtALSchoolDistrict" runat="server" CssClass="form-control" MaxLength="30" Enabled="false"></asp:TextBox>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="div41" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>School</label>
                                                            </div>
                                                            <asp:TextBox ID="txtALSchool" runat="server" CssClass="form-control" MaxLength="50" Enabled="false"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-12">
                                                            <div class="sub-heading">
                                                                <h5>O/L Details</h5>
                                                            </div>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>O/L Exam Type</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlOLType" AppendDataBoundItems="true" runat="server" Enabled="false" CssClass="form-control" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="Div2" runat="server" visible="false">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>O/L Stream</label>
                                                            </div>
                                                            <label for="stream"><sup></sup></label>
                                                            <asp:DropDownList ID="ddlolStream" AppendDataBoundItems="true" runat="server" Enabled="false" CssClass="form-control" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlolStream" Display="None" ErrorMessage="Please Select A/L Stream" InitialValue="0" SetFocusOnError="True" ValidationGroup="Exam"></asp:RequiredFieldValidator>--%>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="Div12" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>O/L passes </label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlolpass" AppendDataBoundItems="true" runat="server" Enabled="false" CssClass="form-control" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlolpass" Display="None" ErrorMessage="Please Select A/L passes" InitialValue="0" SetFocusOnError="True" ValidationGroup="Exam"></asp:RequiredFieldValidator>--%>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Subject 1</label>
                                                            </div>
                                                            <asp:DropDownList ID="olddlsub1" AppendDataBoundItems="true" runat="server" Enabled="false" CssClass="form-control" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Grade</label>
                                                            </div>
                                                            <asp:DropDownList ID="olddlgrade1" AppendDataBoundItems="true" runat="server" Enabled="false" CssClass="form-control" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Subject 2</label>
                                                            </div>
                                                            <asp:DropDownList ID="olddlsubj2" AppendDataBoundItems="true" runat="server" Enabled="false" CssClass="form-control" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Grade</label>
                                                            </div>
                                                            <asp:DropDownList ID="olddlgrade2" AppendDataBoundItems="true" runat="server" Enabled="false" CssClass="form-control" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Subject 3</label>
                                                            </div>
                                                            <asp:DropDownList ID="olddlsub3" AppendDataBoundItems="true" runat="server" Enabled="false" CssClass="form-control" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="div13" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Grade</label>
                                                            </div>
                                                            <asp:DropDownList ID="olddlgrade3" AppendDataBoundItems="true" runat="server" Enabled="false" CssClass="form-control" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="div14" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Subject 4</label>
                                                            </div>
                                                            <asp:DropDownList ID="olddlsub4" AppendDataBoundItems="true" runat="server" Enabled="false" CssClass="form-control" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="div15" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Grade</label>
                                                            </div>
                                                            <asp:DropDownList ID="olddlgrade4" AppendDataBoundItems="true" runat="server" Enabled="false" CssClass="form-control" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="div16" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Subject 5</label>
                                                            </div>
                                                            <asp:DropDownList ID="olddlsub5" AppendDataBoundItems="true" runat="server" Enabled="false" CssClass="form-control" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="div17" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Grade</label>
                                                            </div>
                                                            <asp:DropDownList ID="olddlgrade5" AppendDataBoundItems="true" runat="server" Enabled="false" CssClass="form-control" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="div19" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Subject 6</label>
                                                            </div>
                                                            <asp:DropDownList ID="olddlsub6" AppendDataBoundItems="true" runat="server" Enabled="false" CssClass="form-control" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="div35" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Grade</label>
                                                            </div>
                                                            <asp:DropDownList ID="olddlgrade6" AppendDataBoundItems="true" runat="server" Enabled="false" CssClass="form-control" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="div42" runat="server">
                                                            <div class="label-dynamic">
                                                                <label>Subject 7 </label>
                                                            </div>
                                                            <asp:DropDownList ID="olddlsub7" runat="server" CssClass="form-control select2 select-clik" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="olddlsub7_SelectedIndexChanged" TabIndex="43" ToolTip="Please Select Subject 7" AppendDataBoundItems="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="div43" runat="server">
                                                            <div class="label-dynamic">
                                                                <label>Grade </label>
                                                            </div>
                                                            <asp:DropDownList ID="olddlgrade7" runat="server" CssClass="form-control select2 select-clik" Enabled="false" TabIndex="44" ToolTip="Please Select Subject 7 Grade" AppendDataBoundItems="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="div44" runat="server">
                                                            <div class="label-dynamic">
                                                                <label>Subject 8 </label>
                                                            </div>
                                                            <asp:DropDownList ID="olddlsub8" runat="server" CssClass="form-control select2 select-clik" Enabled="false" TabIndex="45" AutoPostBack="true" OnSelectedIndexChanged="olddlsub8_SelectedIndexChanged" ToolTip="Please Select Subject 8" AppendDataBoundItems="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="div45" runat="server">
                                                            <div class="label-dynamic">
                                                                <label>Grade </label>
                                                            </div>
                                                            <asp:DropDownList ID="olddlgrade8" runat="server" CssClass="form-control select2 select-clik" Enabled="false" TabIndex="46" ToolTip="Please Select Subject 8 Grade" AppendDataBoundItems="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="div46" runat="server">
                                                            <div class="label-dynamic">
                                                                <label>Subject 9 </label>
                                                            </div>
                                                            <asp:DropDownList ID="olddlsub9" runat="server" CssClass="form-control select2 select-clik" Enabled="false" TabIndex="47" ToolTip="Please Select Subject 9" AppendDataBoundItems="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="div47" runat="server">
                                                            <div class="label-dynamic">
                                                                <label>Grade </label>
                                                            </div>
                                                            <asp:DropDownList ID="olddlgrade9" runat="server" CssClass="form-control select2 select-clik" Enabled="false" TabIndex="48" ToolTip="Please Select Subject 9 Grade" AppendDataBoundItems="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="btnPersonalSubmit" runat="server" TabIndex="23" Text="Save" CssClass="btn btn-outline-info d-none" />
                                                <%--<asp:ValidationSummary ID="ValidationSummary5" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="sub" />--%>
                                            </div>
                                            <%--</ContentTemplate>
                                            </asp:UpdatePanel>--%>
                                        </div>
                                        <div id="Div40" runat="server" visible="false">
                                            <div id="divEducationPG" runat="server" visible="false">

                                                <div class="col-12" id="DivAcademic" runat="server">
                                                    <div class="row">
                                                        <div class="col-12">

                                                            <div class="sub-heading">
                                                                <h5>Academic Details</h5>
                                                            </div>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Name of Qualification</label>
                                                            </div>
                                                            <asp:TextBox ID="txtHighestEducationPG" runat="server" TabIndex="1" CssClass="form-control" MaxLength="100" onKeypress="return ValidWord(event);" autocomplete="off"></asp:TextBox>

                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>University/Institute with Country</label>
                                                            </div>
                                                            <asp:TextBox ID="txtUniversityPG" runat="server" TabIndex="2" CssClass="form-control" MaxLength="100" onKeypress="return ValidWord(event);" autocomplete="off"></asp:TextBox>

                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Year of Award</label>
                                                            </div>
                                                            <asp:TextBox ID="txtQualificationAwardPG" runat="server" TabIndex="3" CssClass="form-control" MaxLength="7" onKeypress="return CheckAdmbatch(event);" autocomplete="off"></asp:TextBox>

                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Main Specialty/ Field</label>
                                                            </div>
                                                            <asp:TextBox ID="txtSpecializationPG" runat="server" CssClass="form-control" TabIndex="4" ToolTip="Please Enter Main Specialty/ Field" MaxLength="100" onKeypress="return ValidWord(event);" autocomplete="off"></asp:TextBox>

                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="div9" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Class/GPA</label>
                                                            </div>
                                                            <asp:TextBox ID="txtGPAPG" runat="server" CssClass="form-control" TabIndex="5" ToolTip="Please Enter Class/GPA" MaxLength="100" autocomplete="off"></asp:TextBox>

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
                                                                <label>Professional Qualification</label>
                                                            </div>
                                                            <asp:TextBox ID="txtProfessionalPG" runat="server" CssClass="form-control" TabIndex="6" ToolTip="Please Enter Professional Qualification" MaxLength="100" onKeypress="return ValidWord(event);" autocomplete="off"></asp:TextBox>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="div10" runat="server">
                                                            <div class="label-dynamic">
                                                                <label>University/Institute</label>
                                                            </div>
                                                            <asp:TextBox ID="txtProfessionalUniversityPG" runat="server" CssClass="form-control" TabIndex="7" ToolTip="Please Enter Professional University/Institute" MaxLength="100" onKeypress="return ValidWord(event);" autocomplete="off"></asp:TextBox>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divChemistry" runat="server">
                                                            <div class="label-dynamic">
                                                                <label>Qualification Award of Date</label>
                                                            </div>
                                                            <asp:TextBox ID="txtAwardDatePG" runat="server" CssClass="form-control dob" TabIndex="8" ToolTip="Please Enter Professional Qualification Award of Date" MaxLength="10" onKeypress="return CheckAdmbatch(event);" autocomplete="off" placeholder="DD/MM/YYYY"></asp:TextBox>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="div11" runat="server">
                                                            <div class="label-dynamic">
                                                                <label>Specilization of Qualification</label>
                                                            </div>
                                                            <asp:TextBox ID="txtSpecilizationQualificationPG" runat="server" CssClass="form-control" TabIndex="9" ToolTip="Please Enter Professional Specilization of Qualification " MaxLength="100" onKeypress="return ValidWord(event);" autocomplete="off"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div>
                                                    <div class="pt-2">
                                                        <div class="heading">
                                                            Referees (at least two should be academic referees who will be sending recommendations)
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-xl-3 col-md-3">
                                                                <div class="md-form">
                                                                    <label for="Research Publication"><sup>*</sup>Name</label>
                                                                    <asp:TextBox ID="txtRefereesNamePG" runat="server" CssClass="form-control" TabIndex="16" ToolTip="Please Enter Referees Name" MaxLength="30"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" ControlToValidate="txtRefereesNamePG" Display="None" ErrorMessage="Please Enter Referees Name" SetFocusOnError="True" ValidationGroup="RefereesAddPG"></asp:RequiredFieldValidator>

                                                                </div>
                                                            </div>
                                                            <div class="col-xl-3 col-md-3">
                                                                <div class="md-form">
                                                                    <label for="Research Publication"><sup>*</sup>Position</label>
                                                                    <asp:TextBox ID="txtRefereesPositionPG" runat="server" CssClass="form-control" TabIndex="17" ToolTip="Please Enter Referees Position" MaxLength="25"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator81" runat="server" ControlToValidate="txtRefereesPositionPG" Display="None" ErrorMessage="Please Enter Referees Position" SetFocusOnError="True" ValidationGroup="RefereesAddPG"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-3 col-md-3">
                                                                <div class="md-form">
                                                                    <label for="Research Publication"><sup>*</sup>Address</label>
                                                                    <asp:TextBox ID="txtRefereesAddressPG" runat="server" CssClass="form-control" TabIndex="18" ToolTip="Please Enter Referees Address" MaxLength="30"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator82" runat="server" ControlToValidate="txtRefereesAddressPG" Display="None" ErrorMessage="Please Enter Referees Address" SetFocusOnError="True" ValidationGroup="RefereesAddPG"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-3 col-md-3">
                                                                <div class="md-form">
                                                                    <label for="Research Publication"><sup>*</sup>Contact info</label>
                                                                    <asp:TextBox ID="txtRefereesContactinfoPG" runat="server" CssClass="form-control" TabIndex="19" ToolTip="Please Enter Referees Contact info" MaxLength="15"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator83" runat="server" ControlToValidate="txtRefereesContactinfoPG" Display="None" ErrorMessage="Please Enter Referees Contact info" SetFocusOnError="True" ValidationGroup="RefereesAddPG"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>

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
                                                    <div class="heading">
                                                        Employment History (Including present employment)
                                                    </div>
                                                    <div class="pt-2">

                                                        <div class="row">
                                                            <div class="col-xl-3 col-md-3" id="div36" runat="server">
                                                                <div class="md-form">
                                                                    <label for="Research Publication"><sup></sup>Duration  </label>
                                                                    <asp:TextBox ID="txtDurationPG" runat="server" CssClass="datePickerPG PickerDatePG form-control" TabIndex="21" ToolTip="Please Enter Duration"></asp:TextBox>

                                                                </div>
                                                            </div>
                                                            <div class="col-xl-3 col-md-3" id="div37" runat="server">
                                                                <div class="md-form">
                                                                    <label for="Research Publication"><sup></sup>Position </label>
                                                                    <asp:TextBox ID="txtPositionPG" runat="server" CssClass="form-control" TabIndex="22" ToolTip="Please Enter Position" MaxLength="30"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-4 col-md-4" id="div38" runat="server">
                                                                <div class="md-form">
                                                                    <label for="Research Publication"><sup></sup>Name, Address & Contact Details of the Employer </label>
                                                                    <asp:TextBox ID="txtDetailsPG" runat="server" CssClass="form-control" TabIndex="23" ToolTip="Please Enter Name, Address & Contact Details of the Employer" MaxLength="60"></asp:TextBox>

                                                                </div>
                                                            </div>

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
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="divEducationDetailsPHD" runat="server" visible="false">

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
                                                            <asp:TextBox ID="txtNameofQualificationPHD" runat="server" TabIndex="1" CssClass="form-control" MaxLength="50"></asp:TextBox>


                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="div20" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Year of Award </label>
                                                            </div>
                                                            <asp:TextBox ID="txtYearofAwardPHD" runat="server" TabIndex="2" CssClass="form-control" MaxLength="4" onblur="return yearValidation(this);"></asp:TextBox>


                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="div21" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>University/Institute with Country</label>
                                                            </div>
                                                            <asp:TextBox ID="txtUniversityPHD" runat="server" TabIndex="3" CssClass="form-control" MaxLength="50"></asp:TextBox>


                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="div22" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Main Specialty/ Field </label>
                                                            </div>
                                                            <asp:TextBox ID="txtMainSpecialtyPHD" runat="server" CssClass="form-control" TabIndex="4" ToolTip="Please Enter Main Specialty/ Field" MaxLength="50"></asp:TextBox>


                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="div23" runat="server">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Class/GPA </label>
                                                            </div>
                                                            <asp:TextBox ID="txtGPAPHD" runat="server" CssClass="form-control" TabIndex="5" ToolTip="Please Enter Class/GPA" MaxLength="15"></asp:TextBox>

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
                                                            <asp:TextBox ID="txtNameQualificationPHD" runat="server" CssClass="form-control" TabIndex="6" ToolTip="Please Enter Name of Qualification" MaxLength="50"></asp:TextBox>
                                                            <asp:HiddenField ID="hdnDate" runat="server" />
                                                            <asp:HiddenField ID="HiddenField3" runat="server" />

                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="div26" runat="server">
                                                            <div class="label-dynamic">
                                                                <label>Awarding Institute </label>
                                                            </div>
                                                            <asp:TextBox ID="txtAwardingUniversityPHD" runat="server" CssClass="form-control" TabIndex="7" ToolTip="Please Enter Awarding Institute" MaxLength="50"></asp:TextBox>

                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="div27" runat="server">
                                                            <div class="label-dynamic">
                                                                <label>Date of Award </label>
                                                            </div>
                                                            <asp:TextBox ID="txtAwardDatePHD" runat="server" CssClass="form-control dob" TabIndex="8" ToolTip="Please Enter Date of Award" MaxLength="12" placeholder="DD/MM/YYYY" onchange="return DateValidation(this);"></asp:TextBox>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="div28" runat="server">
                                                            <div class="label-dynamic">
                                                                <label>Specialization (if any) </label>
                                                            </div>
                                                            <asp:TextBox ID="txtSpecilizationQualificationPHD" runat="server" CssClass="form-control" TabIndex="9" ToolTip="Please Enter Specialization (if any)" MaxLength="50"></asp:TextBox>

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
                                                            <asp:TextBox ID="txtDescriptionPHD" runat="server" CssClass="form-control" TabIndex="10" ToolTip="Please Enter Description" MaxLength="100"></asp:TextBox>
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
                                                            <asp:DropDownList ID="ddlModePHD" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="11">
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
                                                            <asp:RadioButtonList ID="rdbQuestion1PHD" runat="server" TabIndex="12" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdbQuestion1PHD_SelectedIndexChanged" AutoPostBack="true">
                                                                <asp:ListItem Text="&nbsp;Yes&nbsp;" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="&nbsp;No&nbsp;" Value="2"></asp:ListItem>
                                                            </asp:RadioButtonList>

                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divDetails" runat="server" visible="false">
                                                            <div class="label-dynamic">
                                                                <label>Give Details</label>
                                                            </div>
                                                            <asp:TextBox ID="txtQuestionDetailsPHD" runat="server" CssClass="form-control" TabIndex="13" ToolTip="Please Enter First Question Give Details" MaxLength="30"></asp:TextBox>


                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>
                                                                    Are you currently registered for another Degree/Diploma at SLIIT or any other University/Institute
                                                                </label>
                                                            </div>
                                                            <asp:RadioButtonList ID="rdbQuestion2PHD" runat="server" TabIndex="14" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdbQuestion2PHD_SelectedIndexChanged" AutoPostBack="true">
                                                                <asp:ListItem Text="&nbsp;Yes&nbsp;" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="&nbsp;No&nbsp;" Value="2"></asp:ListItem>
                                                            </asp:RadioButtonList>

                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="divDetails1" runat="server" visible="false">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Give Details</label>
                                                            </div>
                                                            <asp:TextBox ID="txtQuestion1DetailsPHD" runat="server" CssClass="form-control" TabIndex="15" ToolTip="Please Enter Second Question Give Details" MaxLength="30"></asp:TextBox>


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
                                                            <asp:TextBox ID="txtRefereesName" runat="server" CssClass="form-control" TabIndex="16" ToolTip="Please Enter Referees Name" MaxLength="30"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator55" runat="server" ControlToValidate="txtRefereesName" Display="None" ErrorMessage="Please Enter Referees Name"
                                                                SetFocusOnError="True" ValidationGroup="RefereesAdd"></asp:RequiredFieldValidator>

                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Position </label>
                                                            </div>
                                                            <asp:TextBox ID="txtRefereesPosition" runat="server" CssClass="form-control" TabIndex="17" ToolTip="Please Enter Referees Position" MaxLength="25"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator56" runat="server" ControlToValidate="txtRefereesPosition" Display="None" ErrorMessage="Please Enter Referees Position"
                                                                SetFocusOnError="True" ValidationGroup="RefereesAdd"></asp:RequiredFieldValidator>

                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Address </label>
                                                            </div>
                                                            <asp:TextBox ID="txtRefereesAddress" runat="server" CssClass="form-control" TabIndex="18" ToolTip="Please Enter Referees Address" MaxLength="30"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator57" runat="server" ControlToValidate="txtRefereesAddress" Display="None" ErrorMessage="Please Enter Referees Address"
                                                                SetFocusOnError="True" ValidationGroup="RefereesAdd"></asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup></sup>
                                                                <label>Contact info </label>
                                                            </div>
                                                            <asp:TextBox ID="txtRefereesContactinfo" runat="server" CssClass="form-control" TabIndex="19" ToolTip="Please Enter Referees Contact info" MaxLength="15"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator58" runat="server" ControlToValidate="txtRefereesContactinfo" Display="None" ErrorMessage="Please Enter Referees Contact info"
                                                                SetFocusOnError="True" ValidationGroup="RefereesAdd"></asp:RequiredFieldValidator>

                                                        </div>
                                                        <div class="col-12 btn-footer">

                                                            <asp:ValidationSummary ID="ValidationSummary11" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="RefereesAdd" />
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
                                                            <asp:TextBox ID="txtDurationPHD" runat="server" CssClass="datePicker PickerDate form-control" TabIndex="21" ToolTip="Please Enter Duration"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator59" runat="server" ControlToValidate="txtDurationPHD" Display="None" ErrorMessage="Please Enter Duration"
                                                                SetFocusOnError="True" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="div32" runat="server">
                                                            <div class="label-dynamic">
                                                                <label>Position </label>
                                                            </div>
                                                            <asp:TextBox ID="txtPositionPHD" runat="server" Enabled="false" CssClass="form-control" TabIndex="22" ToolTip="Please Enter Position" MaxLength="30"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator60" runat="server" ControlToValidate="txtPositionPHD" Display="None" ErrorMessage="Please Enter Position"
                                                                SetFocusOnError="True" ValidationGroup="Add"></asp:RequiredFieldValidator>

                                                        </div>

                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="div33" runat="server">
                                                            <div class="label-dynamic">
                                                                <label>Name, Address & Contact Details of the Employer </label>
                                                            </div>
                                                            <asp:TextBox ID="txtDetailsPHD" runat="server" CssClass="form-control" TabIndex="23" ToolTip="Please Enter Name, Address & Contact Details of the Employer" MaxLength="60"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator61" runat="server" ControlToValidate="txtDetailsPHD" Display="None"
                                                                ErrorMessage="Please Enter Name, Address & Contact Details of the Employer" SetFocusOnError="True" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="div34" runat="server">
                                                            <div class="label-dynamic">
                                                                <label></label>
                                                            </div>

                                                            <asp:ValidationSummary ID="ValidationSummary12" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Add" />
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
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="card">
                            <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseThree" aria-expanded="false">
                                <span class="title">Documents Uploaded</span>
                                <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                            </div>
                            <div id="collapseThree" class="collapse show">
                                <div>
                                    <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updDocument"
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
                                <asp:UpdatePanel ID="updDocument" runat="server">
                                    <ContentTemplate>
                                        <div class="card-body">
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="col-12">
                                                        <asp:Panel ID="Panel1" runat="server">
                                                            <asp:ListView ID="lvDocument" runat="server">
                                                                <LayoutTemplate>
                                                                    <div>
                                                                        <div class="sub-heading">
                                                                            <h5>Document List</h5>
                                                                        </div>
                                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="example">
                                                                            <thead class="bg-light-blue">
                                                                                <tr>
                                                                                    <th>Sr. No.</th>
                                                                                    <th>Document Name</th>
                                                                                    <th>Mandatory</th>
                                                                                    <th>View</th>
                                                                                    <%--<th>Download</th>--%>
                                                                                    <th>Status</th>
                                                                                    <th>Remark</th>
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
                                                                            <asp:Label ID="lblDocNo" runat="server" Text='<%#Eval("DOCNO") %>' Visible="false"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblname" runat="server" Text='<%#Eval("DOCNAME") %>' /><br />
                                                                        </td>
                                                                        <td>
                                                                            <%#Eval("MANDATORY") %>
                                                                            <asp:HiddenField ID="hdfMandatory" runat="server" Value='<%#Eval("MANDATORY") %>' />
                                                                        </td>
                                                                        <td>
                                                                            <%--<i class="fa fa-eye" aria-hidden="true" data-toggle="modal" data-target="#veiw"></i>--%>
                                                                            <asp:LinkButton ID="lnkViewDoc" runat="server" CssClass="btn btn-outline-info" CommandArgument='<%#Eval("DOCNO") %>' CommandName='<%#Eval("DOC_FILENAME") %>' Visible='<%#Eval("DOC_FILENAME").ToString() != string.Empty ? true : false %>' OnClick="lnkViewDoc_Click"><i class="fa fa-eye"></i> View </asp:LinkButton>
                                                                        </td>
                                                                        <td class="d-none">
                                                                            <asp:LinkButton ID="lnkDownload" runat="server" CssClass="btn btn-outline-info" CommandArgument='<%#Eval("DOCNO") %>' CommandName='<%#Eval("DOC_FILENAME") %>' Visible='<%#Eval("DOC_FILENAME").ToString() != string.Empty ? true : false %>' OnClick="lnkDownload_Click"><i class="fa fa-download"></i> Download </asp:LinkButton>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlstatus" runat="server" CssClass="form-control" data-select2-enable="true">
                                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                                <asp:ListItem Value="1">Verified</asp:ListItem>
                                                                                <asp:ListItem Value="2">Not Verified</asp:ListItem>
                                                                                <asp:ListItem Value="3">Pending</asp:ListItem>
                                                                                <asp:ListItem Value="4">Incomplete</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("DOC_STATUS") %>' Visible="false"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtremark" runat="server" CssClass="form-control" MaxLength="150" Text='<%#Eval("DOC_REMARK") %>'></asp:TextBox>
                                                                        </td>
                                                                        <%--<td>STUDENT PHOTO</td>
                                                                <td class="text-center"><i class="fa fa-eye" aria-hidden="true" data-toggle="modal" data-target="#veiw"></i></td>
                                                                <td class="text-center"><i class="fa fa-download" aria-hidden="true"></i></td>
                                                                <td>
                                                                    <asp:DropDownList ID="lbl" runat="server" CssClass="form-control" data-select2-enable="true">
                                                                        <asp:ListItem Value="0">Approve</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                    <textarea class="form-control" rows="1"></textarea>
                                                                </td>--%>
                                                                    </tr>
                                                                </ItemTemplate>
                                                                <EmptyDataTemplate>
                                                                    No Record Found !!!
                                                                </EmptyDataTemplate>
                                                            </asp:ListView>

                                                        </asp:Panel>
                                                    </div>
                                                    <div class="col-12 btn-footer">
                                                        <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn btn-outline-info" OnClick="btnSubmit_Click">SUBMIT </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                        <div class="card" runat="server" visible="false">
                            <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseAdditionalDoc" aria-expanded="false">
                                <span class="title">Additional Documents Uploaded</span>
                                <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                            </div>
                            <div id="collapseAdditionalDoc" class="collapse show">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updAdditionalDoc"
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
                                <asp:UpdatePanel ID="updAdditionalDoc" runat="server">
                                    <ContentTemplate>
                                        <div class="card-body">
                                            <div class="col-12">
                                                <div class="row">
                                                    <div class="col-12">
                                                        <asp:Panel ID="Panel5" runat="server">
                                                            <asp:ListView ID="LvAdditionalDoc" runat="server">
                                                                <LayoutTemplate>
                                                                    <div>
                                                                        <div class="sub-heading">
                                                                            <h5>Additional Document List</h5>
                                                                        </div>
                                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="example">
                                                                            <thead class="bg-light-blue">
                                                                                <tr>
                                                                                    <th>Sr. No.</th>
                                                                                    <th>Document Name</th>
                                                                                    <th>Choose File</th>
                                                                                    <th>View</th>
                                                                                    <th>Remark</th>
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
                                                                        </td>
                                                                        <td>
                                                                            <asp:FileUpload ID="fuDocument" runat="server" CssClass="form-control" onchange="setUploadButtondoc(this)" TabIndex='<%#Container.DataItemIndex + 1 %>' />
                                                                        </td>
                                                                        <td>
                                                                            <%--<i class="fa fa-eye" aria-hidden="true" data-toggle="modal" data-target="#veiw"></i>--%>
                                                                            <asp:LinkButton ID="lnkViewAdditionalDoc" runat="server" CssClass="btn btn-outline-info" CommandArgument='<%#Eval("DOCUMENTNO") %>' CommandName='<%#Eval("DOC_FILENAME") %>' Visible='<%#Eval("DOC_FILENAME").ToString() == string.Empty ? false:true %>' OnClick="lnkViewAdditionalDoc_Click"><i class="fa fa-eye"></i> View </asp:LinkButton>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtremark" runat="server" CssClass="form-control" MaxLength="150" Text='<%#Eval("DOC_REMARK") %>'></asp:TextBox>
                                                                        </td>
                                                                        <%--<td>STUDENT PHOTO</td>
                                                                <td class="text-center"><i class="fa fa-eye" aria-hidden="true" data-toggle="modal" data-target="#veiw"></i></td>
                                                                <td class="text-center"><i class="fa fa-download" aria-hidden="true"></i></td>
                                                                <td>
                                                                    <asp:DropDownList ID="lbl" runat="server" CssClass="form-control" data-select2-enable="true">
                                                                        <asp:ListItem Value="0">Approve</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                    <textarea class="form-control" rows="1"></textarea>
                                                                </td>--%>
                                                                    </tr>
                                                                </ItemTemplate>
                                                                <EmptyDataTemplate>
                                                                    No Record Found !!!
                                                                </EmptyDataTemplate>
                                                            </asp:ListView>

                                                        </asp:Panel>
                                                    </div>
                                                    <div class="col-12 btn-footer">
                                                        <asp:LinkButton ID="btnAdditionalDoc" runat="server" CssClass="btn btn-outline-info" OnClick="btnAdditionalDoc_Click">SUBMIT </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnAdditionalDoc" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                        <div class="card" runat="server" visible="false">
                            <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseFour" aria-expanded="false">
                                <span class="title">Module Details</span>
                                <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                            </div>
                            <div id="collapseFour" class="collapse show">
                                <div class="card-body">
                                    <div class="col-12">
                                        <asp:Panel ID="Panel2" runat="server" Visible="false">
                                            <asp:ListView ID="lvOfferedSubject" runat="server">
                                                <LayoutTemplate>
                                                    <div>
                                                        <div class="sub-heading">
                                                            <h5>Core Module</h5>
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
                                                            <%--<asp:Label ID="LblSemNo" runat="server" Text='<% #Eval("REGISTERED")%>' ToolTip='<% #Eval("REGISTERED")%>' Visible="false"></asp:Label></td>--%>
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
                                                            <h5>Orientation Module</h5>
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
                                                            <h5>Bridging Module</h5>
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
                                        <asp:Button ID="btnSubmitOffer" runat="server" Text="Submit" CssClass="btn btn-outline-info d-none" OnClick="btnSubmitOffer_Click" Visible="false" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="card">
                            <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseFive" aria-expanded="false">
                                <span class="title">Payment Details</span>
                                <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                            </div>

                            <div id="collapseFive" class="collapse show">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updFees"
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
                                <asp:UpdatePanel ID="updFees" runat="server">
                                    <ContentTemplate>
                                        <div class="card-body">
                                            <div class="col-12">
                                                <asp:Panel ID="pnlStudentsFees" runat="server">
                                                    <asp:ListView ID="lvStudentFees" runat="server">
                                                        <LayoutTemplate>
                                                            <div>
                                                                <div class="sub-heading">
                                                                    <h5>Fees Details</h5>
                                                                </div>
                                                                <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tblHead">
                                                                    <thead class="bg-light-blue">
                                                                        <tr id="trRow">
                                                                            <th>UserName
                                                                            </th>
                                                                            <th>Payment Type
                                                                            </th>
                                                                            <th>Amount
                                                                            </th>
                                                                            <th>Date
                                                                            </th>
                                                                            <th>Order ID
                                                                            </th>
                                                                            <th>Transaction ID
                                                                            </th>
                                                                            <th>Payment Status
                                                                            </th>
                                                                            <th>View
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
                                                                    <asp:Label ID="lblEnrollmentno" runat="server" Text='<%#Eval("ENROLLNMENTNO") %>'></asp:Label>
                                                                    <asp:Label ID="lblIdno" runat="server" Text='<%#Eval("IDNO") %>' Visible="false"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblPaymentType" runat="server" Text='<%#Eval("PAY_TYPE") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblAmount" runat="server" Text='<%#Eval("TOTAL_AMT") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblDate" runat="server" Text='<%#Eval("REC_DT") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblOrderID" runat="server" Text='<%#Eval("ORDER_ID") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblTransactionId" runat="server" Text='<%#Eval("APTRANSACTIONID") %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("PAY_STATUS") %>' ForeColor='<%# Convert.ToInt32(Eval("RECON")) == 1 ? System.Drawing.Color.Green : System.Drawing.Color.Red %>'></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:LinkButton ID="lnkViewSlip" runat="server" CommandArgument='<%#Eval("TEMP_DCR_NO") %>' OnClick="lnkViewSlip_Click" CommandName='<%#Eval("DOC_FILENAME") %>' CssClass="btn btn-outline-info" Visible='<%#Eval("PAY_TYPE").ToString() == "OFFLINE" ? true:false %>'>
                                                                 <i class="fa fa-search" aria-hidden="true"></i> View  
                                                                    </asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>

                                                </asp:Panel>
                                                <div class="col-md-12" id="TRNote" runat="server" visible="false" style="display: none">

                                                    <blockquote><span class="text-danger">**Note: If Installment or Fees Not Matched,Contact To Accounts department!</span></blockquote>

                                                </div>
                                                <div class="col-md-12" runat="server" id="TRAmount" visible="false">
                                                    <li class="list-group-item">
                                                        <b>Total Amount :</b>
                                                        <a class="pull-right">
                                                            <asp:Label ID="lblOrderID" runat="server" CssClass="data_label"></asp:Label>
                                                            <asp:HiddenField ID="hdfAmount" runat="server" Value="0" />
                                                        </a>
                                                    </li>
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
                                                </div>

                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                    <div>
                        <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="updfinalblock"
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
                    <asp:UpdatePanel ID="updfinalblock" runat="server">
                        <ContentTemplate>
                            <div class="col-12 mt-4" runat="server" visible="false">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="sub-heading">
                                            <h5>Enrollment Confirmation</h5>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-6 col-12">
                                        <ul class="list-group list-group-unbordered">
                                            <li class="list-group-item"><b>Eligibility Status :</b>
                                                <a class="sub-label">
                                                    <asp:Label ID="lblstatus" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <label>Faculty/School Name </label>
                                        </div>
                                        <asp:DropDownList ID="ddlfaculty" runat="server" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <label>Study Level </label>
                                        </div>
                                        <asp:DropDownList ID="ddlstudylevel" runat="server" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <label>Intake </label>
                                        </div>
                                        <asp:DropDownList ID="ddlintake" runat="server" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <label>Program </label>
                                        </div>
                                        <asp:DropDownList ID="ddlpgm" runat="server" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12 d-none">
                                        <div class="label-dynamic">
                                            <label>Awarding Institute </label>
                                        </div>
                                        <asp:DropDownList ID="ddlawardinginst" runat="server" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Campus </label>
                                        </div>
                                        <asp:DropDownList ID="ddlcamous" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlcamous_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Please Select Campus" InitialValue="0"
                                            ControlToValidate="ddlcamous" Display="None" ValidationGroup="btnSubmit"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Week Day/Week End </label>
                                        </div>
                                        <asp:DropDownList ID="ddlweek" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select Week Day/Week End" InitialValue="0"
                                            ControlToValidate="ddlweek" Display="None" ValidationGroup="btnSubmit"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label>Section Name </label>
                                        </div>
                                        <asp:Label ID="lblSectionName" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:LinkButton ID="lnkFinalSubmit" runat="server" CssClass="btn btn-outline-info d-none" OnClick="lnkFinalSubmit_Click">Submit</asp:LinkButton>
                                <asp:LinkButton ID="lnkSendEmail" runat="server" CssClass="btn btn-outline-info" OnClick="lnkSendEmail_Click" ValidationGroup="btnSubmit">Send Admission Confirmation</asp:LinkButton>
                                <asp:LinkButton ID="lnkCertiAdmis" runat="server" CssClass="btn btn-outline-info" OnClick="lnkCertiAdmis_Click">Certificate of Admission</asp:LinkButton>
                                <asp:LinkButton ID="lnkGeneratereport" runat="server" CssClass="btn btn-outline-info d-none" OnClick="lnkGeneratereport_Click" Visible="false">Print Enrollment Form</asp:LinkButton>
                                <asp:LinkButton ID="lnkPrintReport" runat="server" CssClass="btn btn-outline-primary d-none" OnClick="lnkPrintReport_Click" Visible="false">Summary Sheet</asp:LinkButton>
                                <asp:LinkButton ID="btnRefundInitiated" runat="server" CssClass="btn btn-outline-primary d-none" OnClick="btnRefundInitiated_Click" Visible="false">Refund</asp:LinkButton>
                                <asp:Button ID="btnFrontBackReport" runat="server" Text="Print Front/Back ID Card" CssClass="btn btn-outline-primary d-none"
                                    ValidationGroup="show" Visible="false" OnClick="btnFrontBackReport_Click" TabIndex="9" />
                                <asp:LinkButton ID="LinkButton4" runat="server" CssClass="btn btn-outline-danger d-none">Back</asp:LinkButton>
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="btnSubmit" />
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="lnkPrintReport" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    <asp:UpdatePanel ID="updModel" runat="server">
        <ContentTemplate>
            <div id="myModal22" class="modal fade" role="dialog" data-backdrop="static">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content" style="margin-top: -25px">
                        <div class="modal-body">
                            <div class="modal-header">
                                <asp:LinkButton ID="lnkClose" runat="server" CssClass="close" Style="margin-top: -18px" OnClick="lnkClose_Click">x</asp:LinkButton>
                                <%--<button type="button" class="close" data-dismiss="modal" style="margin-top: -18px">x</button>--%>
                            </div>

                            <asp:Image ID="ImageViewer" runat="server" Width="100%" Height="500px" Visible="false" />
                            <asp:Literal ID="ltEmbed" runat="server" Visible="false" />
                            <div id="imageViewerContainer" runat="server" visible="false"></div>
                            <asp:HiddenField ID="hdfImagePath" runat="server" />
                            <iframe style="width: 100%; height: 500px;" id="irm1" src="~/PopUp.aspx" runat="server"></iframe>
                            <%--<iframe id="iframe1" runat="server" frameborder="0" width="100%" height="800px" visible="false"></iframe>--%>
                            <%--<iframe id="iframe1" runat="server" src="../FILEUPLOAD/Transcript Report.pdf" frameborder="0" width="100%" height="547px"></iframe>--%>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lnkClose" />
        </Triggers>
    </asp:UpdatePanel>
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
        function setUploadButtondoc(chk) {
            var maxFileSize = 1000000;
            var fi = document.getElementById(chk.id);
            var tabValue = $(chk).attr('TabIndex');

            for (var i = 0; i <= fi.files.length - 1; i++) {
                var fsize = fi.files.item(i).size;
                if (fsize >= maxFileSize) {
                    alert("Document Size Greater Than 1MB");
                    $(chk).val("");
                }
            }
            var fileExtension1 = ['pdf'];

            if ($.inArray($(chk).val().replace(',', '.').split('.').pop().toLowerCase(), fileExtension1) == -1) {
                alert("Only formats are allowed : " + fileExtension1.join(', '));
                $(chk).val("");
            }
        }
    </script>
</asp:Content>

