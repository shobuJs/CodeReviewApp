<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Student_Information.aspx.cs" Inherits="ACADEMIC_Student_Information" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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

    <style>
        .std-info {
            background: #fff;
            box-shadow: rgb(0 0 0 / 20%) 0px 2px 10px;
            border-radius: 5px;
        }
    </style>

    <asp:UpdatePanel ID="updAllOnlineAdmDetails" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"> Student Information </asp:Label>
                            </h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12 btn-footer" runat="server" id="Search">
                                <span id="SearchPopUp" class="btn btn-outline-info"><i class="fa fa-search"></i></span>
                            </div>
                        </div>


                        <div class="box-body">
                            <%--                    <div class="col-12 mt-3">
                                <div class="row">
                   
                                          <asp:Panel ID="panelsearch" runat="server" Visible="true">
                                        <div class="form-group">
                                            <div class="input-group">
                                                <div class="label-dynamic">
                                                    <sup>* </sup>
                                                    <asp:Label ID="lblRegno" runat="server" Font-Bold="true" >Reg.No.</asp:Label>
                                                </div>
                                                <asp:TextBox ID="txtSearchCandidateProgram" runat="server" CssClass="form-control" ToolTip="Enter Registration Number" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                    ControlToValidate="txtSearchCandidateProgram" Display="None"
                                                    ErrorMessage="Please Enter Reg No."
                                                    SetFocusOnError="True" ValidationGroup="search">
                                                </asp:RequiredFieldValidator>
                                                <span class="input-group-btn">
                                                    <asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="btn btn-outline-info" OnClick="btnsearch_Click" ValidationGroup="search" />
                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                                                        DisplayMode="List" ShowMessageBox="True" ShowSummary="False"
                                                        ValidationGroup="search" />
                                                </span>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                    </div>
                        </div>--%>


                            <div id="DivMain" runat="server">

                                <div class="col-12 btn-footer d-none">
                                    <asp:LinkButton ID="btnBack" runat="server" CssClass="btn btn-outline-danger">Back</asp:LinkButton>
                                </div>
                                <div class="accordion" id="accordionExample">
                                    <div class="card" id="DivShow" runat="server">
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
                                                                        <asp:Label ID="lblRegNo1" Font-Bold="true" runat="server" />
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
                                                <div class="col-lg-9 col-12 ml-3 std-info">
                                                    <div class="row">
                                                        <div class="col-lg-9 col-md-9 col-12">
                                                            <ul class="list-group list-group-unbordered">
                                                                <li class="list-group-item"><b>Student ID :</b>
                                                                    <a class="sub-label">
                                                                        <asp:Label ID="lblregnoIII" Font-Bold="true" runat="server" /></a>
                                                                </li>
                                                                <li class="list-group-item"><b>Student Full Name :</b>
                                                                    <a class="sub-label">
                                                                        <asp:Label ID="lblInitialName" Font-Bold="true" runat="server" /></a>
                                                                </li>
                                                                <li class="list-group-item"><b>Program :</b>
                                                                    <a class="sub-label">
                                                                        <asp:Label ID="lblDegree" Font-Bold="true" runat="server" />
                                                                        <asp:Label ID="lblProgramName" Font-Bold="true" runat="server" />
                                                                    </a>
                                                                </li>
                                                                <li class="list-group-item"><b>Campus:</b>
                                                                    <a class="sub-label">
                                                                        <asp:Label ID="lblCampus" Font-Bold="true" runat="server" /></a>
                                                                </li>
                                                                <li class="list-group-item"><b>Semester :</b>
                                                                    <a class="sub-label">
                                                                        <asp:Label ID="lblSemesterP" Font-Bold="true" runat="server" /></a>
                                                                </li>
                                                            </ul>
                                                        </div>
                                                        <div class="col-lg-3 col-md-3 col-12 mt-2">
                                                            <asp:Image ID="imgPhoto" runat="server" Width="140px" Height="140px" />
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-12 mt-3">
                                                    <div class="row">
                                                        <div class="col-lg-6 col-md-12 col-12">
                                                            <div class="std-info pl-3 pr-3 pb-2">
                                                                <div class="sub-heading pt-3">
                                                                    <h5>Personal Information</h5>
                                                                </div>
                                                                <ul class="list-group list-group-unbordered mt-2">
                                                                    <li class="list-group-item"><b>Student Name :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblStdname" Font-Bold="true" runat="server" /></a>
                                                                    </li>


                                                                    <li class="list-group-item"><b>Date Of Birth :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblDOBName" Font-Bold="true" runat="server" /></a>
                                                                    </li>
                                                                    <li class="list-group-item d-none"><b>NIC (National Identity card) :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblNICNO" Font-Bold="true" runat="server" /></a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>Passport No :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblPassPortNum" Font-Bold="true" runat="server" /></a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>Gender :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblGender" Font-Bold="true" runat="server" /></a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>Citizen Type :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblCitizenType" Font-Bold="true" runat="server" /></a>
                                                                    </li>
                                                                    <li class="list-group-item d-none"><b>Left / Right Handed :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblLRHanded" Font-Bold="true" runat="server" /></a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>Email :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblEmailP" Font-Bold="true" runat="server" /></a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>College Email :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblSliitEmail" Font-Bold="true" runat="server" /></a>
                                                                    </li>
                                                                    <li class="list-group-item"><b>Mobile No. :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblMobileNoP" Font-Bold="true" runat="server" />
                                                                        </a>
                                                                    </li>
                                                                    <li class="list-group-item d-none"><b>Enrollment ID :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblEnrollNOIII" Font-Bold="true" runat="server" /></a>
                                                                    </li>
                                                                    <li class="list-group-item d-none"><b>First Name :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblFirstNameP" Font-Bold="true" runat="server" />
                                                                        </a>
                                                                    </li>
                                                                    <li class="list-group-item d-none"><b>Last Name :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblLastNameP" Font-Bold="true" runat="server" /></a>
                                                                    </li>
                                                                </ul>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-6 col-md-12 col-12">
                                                            <div class="std-info pl-3 pr-3 pb-2">
                                                                <div class="sub-heading pt-3">
                                                                    <h5>Address Details</h5>
                                                                </div>
                                                                <ul class="list-group list-group-unbordered mt-2">
                                                                    <li class="list-group-item"><b>Address :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblAddressName" Font-Bold="true" runat="server" />
                                                                        </a>
                                                                    </li>
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
                                                                    <li class="list-group-item"><b>District :</b>
                                                                        <a class="sub-label">
                                                                            <asp:Label ID="lblDistrict" Font-Bold="true" runat="server" />
                                                                        </a>
                                                                    </li>
                                                                </ul>
                                                            </div>

                                                            <div class="std-info pl-3 pr-3 pb-2 mt-3">
                                                                <div>
                                                                    <asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="updAdmission"
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
                                                                <asp:UpdatePanel ID="updAdmission" runat="server">
                                                                    <ContentTemplate>
                                                                        <div class="sub-heading pt-3">
                                                                            <h5>Admission Details</h5>
                                                                        </div>
                                                                        <ul class="list-group list-group-unbordered">
                                                                            <li class="list-group-item"><b>Intake :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblIntakeName" Font-Bold="true" runat="server" /></a>
                                                                            </li>


                                                                            <li class="list-group-item"><b>Weekday/Weekend :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblWeekdayWeekend" Font-Bold="true" runat="server" />
                                                                                </a>
                                                                            </li>

                                                                            <li class="list-group-item"><b>Faculty :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblFacultyName" Font-Bold="true" runat="server" /></a>
                                                                            </li>
                                                                            <li class="list-group-item"><b>Awarding :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblAwardingName" Font-Bold="true" runat="server" /></a>
                                                                            </li>
                                                                            <li class="list-group-item"><b>Enrollment Date :</b>
                                                                                <a class="sub-label">
                                                                                    <asp:Label ID="lblEnrollmentDate" Font-Bold="true" runat="server" /></a>
                                                                            </li>


                                                                        </ul>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>

                                                        <div id="divUgEducationUG" runat="server" class="mt-3">
                                                            <div id="UG" runat="server">
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
                                                                </div>
                                                            </div>

                                                            <div class="col-12 btn-footer">
                                                                <asp:Button ID="btnPersonalSubmit" runat="server" TabIndex="23" Text="Save" CssClass="btn btn-outline-info d-none" />
                                                                <%--<asp:ValidationSummary ID="ValidationSummary5" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="sub" />--%>
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
                                                                        <asp:ListView ID="lvDocument" runat="server" OnSelectedIndexChanged="lvDocument_SelectedIndexChanged">
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
                                                                                                <th>View</th>
                                                                                                <%--<th>Download</th>--%>
                                                                                                <th style="display: none">Status</th>
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
                                                                                        <%--<i class="fa fa-eye" aria-hidden="true" data-toggle="modal" data-target="#veiw"></i>--%>
                                                                                        <asp:LinkButton ID="lnkViewDoc" runat="server" CssClass="btn btn-outline-info" CommandArgument='<%#Eval("DOCNO") %>' CommandName='<%#Eval("DOC_FILENAME") %>' OnClick="lnkViewDoc_Click"><i class="fa fa-eye"></i> View </asp:LinkButton>
                                                                                    </td>
                                                                                    <td class="d-none">
                                                                                        <asp:LinkButton ID="lnkDownload" runat="server" CssClass="btn btn-outline-info" CommandArgument='<%#Eval("DOCNO") %>' CommandName='<%#Eval("DOC_FILENAME") %>' OnClick="lnkDownload_Click"><i class="fa fa-download"></i> Download </asp:LinkButton>
                                                                                    </td>
                                                                                    <td style="display: none">
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
                                                                                        <asp:TextBox ID="txtremark" runat="server" CssClass="form-control" MaxLength="150" Text='<%#Eval("DOC_REMARK") %>' Enabled="false"></asp:TextBox>
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
                                                                <div class="col-12 btn-footer" id="DivSubmit" runat="server">
                                                                    <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn btn-outline-info" OnClick="btnSubmit_Click">SUBMIT </asp:LinkButton>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>

                                <div class="card" id="Div4" runat="server">
                                    <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseFour" aria-expanded="false">
                                        <span class="title">Examination Scheduling</span>
                                        <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                                    </div>
                                    <div id="collapseFour" class="collapse show">
                                        <div>
                                            <asp:UpdateProgress ID="UpdateProgress5" runat="server" AssociatedUpdatePanelID="updexamDetails"
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
                                        <asp:UpdatePanel ID="updexamDetails" runat="server">
                                            <ContentTemplate>
                                                <div class="card-body">
                                                    <div class="col-12">
                                                        <asp:Panel ID="Panel8" runat="server" ScrollBars="Auto">
                                                            <asp:ListView ID="lvStudentRecords" runat="server" OnItemDataBound="lvStudentRecords_DataBound">
                                                                <LayoutTemplate>
                                                                    <%-- <div class="sub-heading">
                                                                        <h5>Exam Time Table List</h5>
                                                                    </div>--%>
                                                                    <table class="table table-striped table-bordered nowrap" id="mytable" style="width: 100%">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>VIEW DETAILS</th>
                                                                                <th class="text-center">SESSION NAME</th>
                                                                                <th class="text-center">PROGRAM</th>
                                                                                <th class="text-center">SEMESTER</th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                        </tbody>
                                                                    </table>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <tr id="trCurRow1" class="first-div">
                                                                        <td>
                                                                            <a href="JavaScript:DivExpandCollese('div1<%# Container.DataItemIndex + 1 %>')">
                                                                                <i class="fa fa-eye">View </i>
                                                                            </a>
                                                                        </td>
                                                                        <td class="text-center">
                                                                            <%# Eval("SESSION_NAME")%>
                                                                            <asp:Label ID="lblsessionno" runat="server" Visible="false" Text='<%# Eval("SESSIONNO")%>'></asp:Label>
                                                                        </td>
                                                                        <td class="text-center">
                                                                            <%# Eval("CODE")%> - <%# Eval("SHORTNAME")%>
                                                                            <asp:Label ID="lbldegree" runat="server" Visible="false" Text='<%# Eval("DEGREENO")%>'></asp:Label>
                                                                            <asp:Label ID="lblbranch" runat="server" Visible="false" Text='<%# Eval("BRANCHNO")%>'></asp:Label>
                                                                        </td>

                                                                        <td class="text-center">
                                                                            <%# Eval("SEMESTERNAME")%>
                                                                            <asp:Label ID="lblSemester" runat="server" Visible="false" Text='<%# Eval("SEMESTERNO")%>'></asp:Label>
                                                                        </td>
                                                                        <tr class="hideTr1" id="div1<%# Container.DataItemIndex + 1 %>" style="display: none">
                                                                            <td colspan="7" style="width: 100%">
                                                                                <asp:ListView ID="lvStudentTimeTable" runat="server">
                                                                                    <LayoutTemplate>
                                                                                        <div id="demo-grid" style="padding: 5px;">
                                                                                            <%--<table class="table table-hover table-bordered">--%>
                                                                                            <table style="width: 100%; border-bottom: 2px solid #fff">
                                                                                                <thead class="header-border">
                                                                                                    <tr>
                                                                                                        <th style="text-align: center">Sr. No.
                                                                                                        </th>
                                                                                                        <th class="text-center">Date</th>
                                                                                                        <th class="text-center">Time</th>
                                                                                                        <th class="text-center">Semester</th>
                                                                                                        <th class="text-center">Module Code</th>
                                                                                                        <th class="text-center">Module Name</th>
                                                                                                    </tr>
                                                                                                </thead>
                                                                                                <tbody>
                                                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                                                </tbody>
                                                                                            </table>
                                                                                        </div>
                                                                                    </LayoutTemplate>
                                                                                    <ItemTemplate>
                                                                                        <tr style="height: 50px;">
                                                                                            <td class="text-center">
                                                                                                <%# Eval("SRNO")%>
                                                                                            </td>
                                                                                            <td class="text-center">
                                                                                                <%# Eval("EXAMDATE")%>
                                                                                            </td>
                                                                                            <td class="text-center">
                                                                                                <%# Eval("SLOTNAME")%>
                                                                                            </td>
                                                                                            <td class="text-center">
                                                                                                <%# Eval("SEMESTERNO")%>
                                                                                            </td>
                                                                                            <td class="text-center">
                                                                                                <%# Eval("CCODE")%>
                                                                                            </td>
                                                                                            <td>
                                                                                                <%# Eval("COURSE_NAME")%>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </ItemTemplate>

                                                                                </asp:ListView>
                                                                            </td>
                                                                        </tr>
                                                                    </tr>
                                                                </ItemTemplate>
                                                                <EmptyDataTemplate>
                                                                    No Record Found !!!
                                                                </EmptyDataTemplate>
                                                            </asp:ListView>
                                                        </asp:Panel>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>


                                <div class="card" id="DivResult" runat="server">
                                    <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseFive" aria-expanded="false">
                                        <span class="title">Result Details</span>
                                        <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                                    </div>

                                    <div id="collapseFive" class="collapse show">
                                        <div>
                                            <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updResultDetails"
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
                                        <asp:UpdatePanel ID="updResultDetails" runat="server">
                                            <ContentTemplate>
                                                <div class="card-body">
                                                    <div class="col-12">
                                                        <asp:Panel ID="Panel5" runat="server">
                                                            <asp:ListView ID="lvResult" runat="server" OnItemDataBound="lvResult_ItemDataBound">
                                                                <LayoutTemplate>
                                                                    <div>
                                                                        <table class="table table-striped table-bordered nowrap" style="width: 100%" id="example">
                                                                            <tr>
                                                                                <th>VIEW DETAILS</th>
                                                                                <th>SEMESTER</th>
                                                                                <th>SESSION NAME</th>
                                                                                <th>TOTAL CREDITS</th>
                                                                                <%--<th>EARNED CREDITS</th>--%>
                                                                                <th>SGPA</th>
                                                                                <th>EGP</th>
                                                                            </tr>
                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                        </table>
                                                                    </div>
                                                                </LayoutTemplate>


                                                                <ItemTemplate>
                                                                    <tr id="trCurRow1" class="first-div">
                                                                        <td>
                                                                            <a href="JavaScript:DivExpandCollese('div<%# Container.DataItemIndex + 1 %>')">
                                                                                <i class="fa fa-eye">View </i>
                                                                            </a>

                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblSemester" runat="server" Text='<%#Eval("SEMESTER") %>' /><br />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblSession" runat="server" Text='<%#Eval("SESSION_NAME") %>' /><br />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblttlCredits" runat="server" Text='<%#Eval("REGD_CREDITS") %>' /><br />
                                                                        </td>

                                                                        <%-- <td>
                                                                            <asp:Label ID="lblERNCredits" runat="server" Text='<%#Eval("EARN_CREDITS") %>' /><br />
                                                                        </td>--%>
                                                                        <td>
                                                                            <asp:Label ID="lblSGPA" runat="server" Text='<%#Eval("SGPA") %>' /><br />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblEGP" runat="server" Text='<%#Eval("EGP") %>' /><br />
                                                                            <asp:HiddenField ID="hdfSemester" runat="server" Value='<%# Eval("SEMESTERNO") %>' />
                                                                            <asp:HiddenField ID="hdfSession" runat="server" Value='<%# Eval("SESSIONNO") %>' />
                                                                        </td>

                                                                    </tr>
                                                                    <tr class="hideTr1" id="div<%# Container.DataItemIndex + 1 %>" style="display: none">
                                                                        <td colspan="7" style="width: 100%">
                                                                            <asp:ListView ID="lvDocs1" runat="server" OnItemDataBound="lvDocs1_ItemDataBound">
                                                                                <LayoutTemplate>
                                                                                    <div id="demo-grid" style="padding: 5px;">
                                                                                        <%--<table class="table table-hover table-bordered">--%>
                                                                                        <table style="width: 100%; border-bottom: 2px solid #fff">
                                                                                            <thead class="header-border">
                                                                                                <tr>
                                                                                                    <th>VIEW DETAILS</th>
                                                                                                    <th>MODULE CODE
                                                                                                    </th>
                                                                                                    <th>MODULE NAME
                                                                                                    </th>
                                                                                                    <th>MODULE TYPE
                                                                                                    </th>
                                                                                                    </th>
                                                                                                     <th>GRADE
                                                                                                     </th>
                                                                                                    <th>EARNED CREDITS
                                                                                                    </th>
                                                                                                    <th>CREDIT POINTS
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
                                                                                    <tr id="trCurRow1" class="second-div">
                                                                                        <td>
                                                                                            <a href="JavaScript:DivExpandCollese('divresult<%# Container.DataItemIndex + 1 %>')">
                                                                                                <i class="fa fa-eye">View </i>
                                                                                            </a>

                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblCode" runat="server" Text='<%#Eval("CCODE") %>'></asp:Label>
                                                                                        </td>
                                                                                        <td><%# Eval("COURSENAME")%></td>
                                                                                        <td>
                                                                                            <%#Eval("SUBJECTTYPE") %>
                                                                                            <asp:HiddenField ID="hdfSemester" runat="server" Value='<%# Eval("SEMESTERNO") %>' />
                                                                                            <asp:HiddenField ID="hdfSession" runat="server" Value='<%# Eval("SESSIONNO") %>' />
                                                                                            <asp:HiddenField ID="hdfcourseno" runat="server" Value='<%# Eval("COURSENO") %>' />
                                                                                        </td>
                                                                                        <td>
                                                                                            <%#Eval("GRADE") %>
                                                                                        </td>
                                                                                        <td>
                                                                                            <%#Eval("EARN_CREDITS") %>
                                                                                        </td>
                                                                                        <td>
                                                                                            <%#Eval("CREDITS_EARNED") %>
                                                                                        </td>
                                                                                    </tr>

                                                                                    <tr class="hideTr1" id="divresult<%# Container.DataItemIndex + 1 %>" style="display: none">
                                                                                        <td colspan="7" style="width: 100%">
                                                                                            <asp:ListView ID="lvdetailsresult" runat="server">
                                                                                                <LayoutTemplate>
                                                                                                    <div id="demo-grid" style="padding: 5px;">
                                                                                                        <table style="width: 100%; border-bottom: 2px solid #fff">
                                                                                                            <thead class="header-border">
                                                                                                                <tr>
                                                                                                                    <th style="text-align: center">Sr. No.
                                                                                                                    </th>
                                                                                                                    <th>COMPONENT NAME
                                                                                                                    </th>
                                                                                                                    <th>EXAM NAME
                                                                                                                    </th>
                                                                                                                    <th>PERCENTAGE
                                                                                                                    </th>
                                                                                                                    <th>REMARK
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
                                                                                                    <tr id="trCurRow1" class="third-div">
                                                                                                        <td style="text-align: center">
                                                                                                            <%# Container.DataItemIndex + 1%>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <%#Eval("COMPONENTNAME") %>

                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <%#Eval("EXAMNAME") %>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <%#Eval("PERCENTAGE") %>
                                                                                                        </td>

                                                                                                        <td>
                                                                                                            <%#Eval("REMARK") %>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </ItemTemplate>

                                                                                            </asp:ListView>
                                                                                        </td>
                                                                                    </tr>
                                                                                </ItemTemplate>

                                                                            </asp:ListView>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>

                                                                <EmptyDataTemplate>
                                                                    No Record Found !!!
                                                                </EmptyDataTemplate>
                                                            </asp:ListView>

                                                        </asp:Panel>
                                                    </div>
                                                </div>

                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>

                                <div class="card" id="Divexternal" runat="server">
                                    <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseSix" aria-expanded="false">
                                        <span class="title">Result Process</span>
                                        <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                                    </div>

                                    <div id="collapseSix" class="collapse show">
                                        <div>
                                            <asp:UpdateProgress ID="UpdateProgress6" runat="server" AssociatedUpdatePanelID="updExternalResultDetails"
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
                                        <asp:UpdatePanel ID="updExternalResultDetails" runat="server">
                                            <ContentTemplate>
                                                <div class="card-body">

                                                    <div class="col-12">
                                                        <asp:Panel ID="Panel9" runat="server">
                                                            <asp:ListView ID="Lvexternalresult" runat="server" OnItemDataBound="Lvexternalresult_ItemDataBound">
                                                                <LayoutTemplate>
                                                                    <div>
                                                                        <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                                            <thead class="bg-light-blue">
                                                                                <tr>
                                                                                    <th>VIEW DETAILS</th>
                                                                                    <th>SEMESTER</th>
                                                                                    <th>MODULE CODE
                                                                                    </th>
                                                                                    <th>MODULE NAME
                                                                                    </th>
                                                                                    <th>MODULE TYPE
                                                                                    </th>
                                                                                    <th>CA(%)</th>
                                                                                    <%-- <th>FINAL(%)</th>
                                                                                    <th>OVERALL(%)</th>--%>
                                                                                    <th>ATTEMPTS
                                                                                    </th>
                                                                                    <th>GRADE
                                                                                    </th>
                                                                                    <th>STATUS</th>
                                                                                    <%-- <th>EARNED CREDITS
                                                                                    </th>
                                                                                    <th>CREDIT POINTS
                                                                                    </th>--%>
                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                                <tr id="itemPlaceholder" runat="server" />
                                                                            </tbody>
                                                                        </table>
                                                                    </div>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <tr id="trCurRow1" class="first-div">
                                                                        <td>
                                                                            <a href="JavaScript:DivExpandCollese('div<%# Container.DataItemIndex + 1 %>')">
                                                                                <i class="fa fa-eye">View </i>
                                                                            </a>
                                                                        </td>
                                                                        <td><%#Eval("SEMESTER") %></td>
                                                                        <td>
                                                                            <asp:Label ID="lblCode" runat="server" Text='<%#Eval("CCODE") %>'></asp:Label>
                                                                        </td>
                                                                        <td><%# Eval("COURSENAME")%></td>
                                                                        <td>
                                                                            <%#Eval("SUBJECTTYPE") %>
                                                                            <asp:HiddenField ID="hdfSemester" runat="server" Value='<%# Eval("SEMESTERNO") %>' />
                                                                            <asp:HiddenField ID="hdfSession" runat="server" Value='<%# Eval("SESSIONNO") %>' />
                                                                            <asp:HiddenField ID="hdfcourseno" runat="server" Value='<%# Eval("COURSENO") %>' />
                                                                        </td>
                                                                        <td><%#Eval("INTERMARK") %></td>
                                                                        <%-- <td><%#Eval("EXTERMARK") %></td>
                                                                        <td>
                                                                            <%#Eval("MARKTOT") %>
                                                                        </td>--%>
                                                                        <td><%#Eval("ATTEMPT") %></td>
                                                                        <td>
                                                                            <%#Eval("GRADE1") %>
                                                                        </td>
                                                                        <td><%#Eval("STATUS") %></td>
                                                                        <%--<td>
                                                                            <%#Eval("EARN_CREDITS") %>
                                                                        </td>
                                                                        <td>
                                                                            <%#Eval("CREDITS_EARNED") %>
                                                                        </td>--%>
                                                                    </tr>
                                                                    <tr class="hideTr1" id="div<%# Container.DataItemIndex + 1 %>" style="display: none">
                                                                        <td colspan="11" style="width: 100%">
                                                                            <asp:ListView ID="Lvexternal" runat="server" OnItemDataBound="Lvexternal_ItemDataBound">
                                                                                <LayoutTemplate>
                                                                                    <div id="demo-grid" style="padding: 5px;">
                                                                                        <%--<table class="table table-hover table-bordered">--%>
                                                                                        <table style="width: 100%; border-bottom: 2px solid #fff">
                                                                                            <thead class="header-border">
                                                                                                <tr>
                                                                                                    <th>VIEW</th>
                                                                                                    <th style="text-align: center">Sr. No.
                                                                                                    </th>

                                                                                                    <th>EXAM TYPE
                                                                                                    </th>
                                                                                                    <th>CA(%)
                                                                                                    </th>
                                                                                                    <%--<th>FINAL(%)
                                                                                                        </th>
                                                                                                        <th>OVERALL(%)</th>--%>
                                                                                                    <th>GRADE
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
                                                                                    <tr id="trCurRow1" class="second-div">
                                                                                        <td>
                                                                                            <a href="JavaScript:DivExpandCollese('divcomp<%# Eval("squenceno") %>')">
                                                                                                <i class="fa fa-eye">View </i>
                                                                                            </a>
                                                                                        </td>
                                                                                        <td style="text-align: center">
                                                                                            <%# Container.DataItemIndex + 1%>
                                                                                        </td>
                                                                                        <td>
                                                                                            <%#Eval("SESSION_NAME") %>
                                                                                            <asp:HiddenField ID="hdfSemester" runat="server" Value='<%# Eval("SEMESTERNO") %>' />
                                                                                            <asp:HiddenField ID="hdfSession" runat="server" Value='<%# Eval("SESSIONNO") %>' />
                                                                                            <asp:HiddenField ID="hdfcourseno" runat="server" Value='<%# Eval("COURSENO") %>' />
                                                                                        </td>

                                                                                        <td><%#Eval("INTERMARK") %></td>
                                                                                        <%-- <td><%#Eval("EXTERMARK") %></td>
                                                                                            <td>
                                                                                                <%#Eval("MARKTOT") %>
                                                                                            </td>--%>
                                                                                        <td><%#Eval("GRADE") %></td>

                                                                                    </tr>
                                                                                    <tr class="hideTr1" id="divcomp<%# Eval("squenceno") %>" style="display: none">
                                                                                        <td colspan="11" style="width: 100%">
                                                                                            <asp:ListView ID="lvdetailsresult" runat="server">
                                                                                                <LayoutTemplate>
                                                                                                    <div id="demo-grid" style="padding: 5px;">
                                                                                                        <table style="width: 100%; border-bottom: 2px solid #fff">
                                                                                                            <thead class="header-border">
                                                                                                                <tr>
                                                                                                                    <th style="text-align: center">Sr. No.
                                                                                                                    </th>
                                                                                                                    <th>COMPONENT NAME
                                                                                                                    </th>
                                                                                                                    <th>EXAM NAME
                                                                                                                    </th>
                                                                                                                    <th>PERCENTAGE
                                                                                                                    </th>
                                                                                                                    <th>REMARK
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
                                                                                                    <tr id="trCurRow1" class="third-div">
                                                                                                        <td style="text-align: center">
                                                                                                            <%# Container.DataItemIndex + 1%>
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <%#Eval("COMPONENTNAME") %>

                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <%#Eval("EXAMNAME") %>

                                                                                                        </td>

                                                                                                        <td>
                                                                                                            <%#Eval("PERCENTAGE") %>

                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <%#Eval("REMARK") %>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </ItemTemplate>

                                                                                            </asp:ListView>
                                                                                        </td>
                                                                                    </tr>
                                                                                </ItemTemplate>

                                                                            </asp:ListView>
                                                                        </td>
                                                                    </tr>


                                                                </ItemTemplate>
                                                                <EmptyDataTemplate>
                                                                    No Record Found !!!
                                                                </EmptyDataTemplate>
                                                            </asp:ListView>
                                                        </asp:Panel>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="card" id="DivSemReg" runat="server">
                                    <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseSeven" aria-expanded="false">
                                        <span class="title">Semester Registration Status</span>
                                        <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                                    </div>
                                    <div id="collapseSeven" class="collapse show">
                                        <div>
                                            <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="updSemReg"
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
                                        <asp:UpdatePanel ID="updSemReg" runat="server">
                                            <ContentTemplate>
                                                <div class="card-body">
                                                    <div class="col-12">
                                                        <asp:Panel ID="Panel6" runat="server">
                                                            <asp:ListView ID="lvReg" runat="server" OnItemDataBound="lvStudents_ItemDataBound">
                                                                <LayoutTemplate>
                                                                    <div>
                                                                        <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                                            <thead class="bg-light-blue">
                                                                                <tr>
                                                                                    <th>VIEW DETAILS</th>
                                                                                    <th>SEMESTER</th>
                                                                                    <th>SESSION</th>
                                                                                    <th>MODULE STATUS</th>

                                                                                </tr>
                                                                            </thead>
                                                                            <tbody>
                                                                                <tr id="itemPlaceholder" runat="server" />
                                                                            </tbody>
                                                                        </table>
                                                                    </div>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <tr id="trCurRow1" class="first-div">

                                                                        <td style="background-color: #ffffff !important">
                                                                            <a href="JavaScript:DivExpandCollese('div2<%# Container.DataItemIndex + 1 %>')">
                                                                                <i class="fa fa-eye">View </i>
                                                                            </a>
                                                                        </td>
                                                                        <td style="background-color: #ffffff !important">

                                                                            <%--<asp:ImageButton ImageUrl="~/images/print.gif" ID="btnPrint" runat="server"
                                                                        CausesValidation="False" CommandArgument="gvParentGrid1_RowCommand" ToolTip="PRINT" />
                                                                    </a>--%>
                                                                            <asp:Label ID="lblSem" runat="server" Text='<%#Eval("SEMESTER_NAME") %>' /><br />
                                                                            <asp:Label ID="lblsemno" runat="server" Visible="false" Text='<%#Eval("SEMESTERNO") %>' />

                                                                        </td>
                                                                        <td style="background-color: #ffffff !important">
                                                                            <asp:Label ID="lblSession" runat="server" Text='<%#Eval("SESSION") %>' /><br />
                                                                        </td>
                                                                        <td style="background-color: #ffffff !important">
                                                                            <asp:Label ID="lblCStatus" runat="server" Text='<%#Eval("REGSTATUS") %>' /><br />
                                                                        </td>


                                                                        <%-- **************** new ****************--%>
                                                                    </tr>


                                                                    <tr class="hideTr" id="div2<%# Container.DataItemIndex + 1 %>" style="display: none;">
                                                                        <td colspan="4" style="width: 100%">
                                                                            <asp:ListView ID="lvDocs" runat="server">
                                                                                <LayoutTemplate>
                                                                                    <div id="demogrid" style="padding: 5px;">
                                                                                        <%--<table class="table table-hover table-bordered">--%>
                                                                                        <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                                                            <thead class="bg-light-blue">
                                                                                                <tr>
                                                                                                    <th style="text-align: center">Sr. No.
                                                                                                    </th>
                                                                                                    <th>MODULE CODE
                                                                                                    </th>
                                                                                                    <th>MODULE NAME
                                                                                                    </th>
                                                                                                    <th>MODULE TYPE
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
                                                                                        <td style="text-align: center">
                                                                                            <%# Container.DataItemIndex + 1%>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:Label ID="lblCode" runat="server" Text='<%#Eval("CCODE") %>'></asp:Label>
                                                                                        </td>
                                                                                        <td><%# Eval("COURSENAME")%></td>
                                                                                        <td>
                                                                                            <%#Eval("SUBJECTTYPE") %>
                                                                                        </td>
                                                                                    </tr>
                                                                                </ItemTemplate>

                                                                            </asp:ListView>
                                                                        </td>



                                                                    </tr>
                                                                    <%-- ***************************--%>
                                                                </ItemTemplate>
                                                                <EmptyDataTemplate>
                                                                    No Record Found !!!
                                                                </EmptyDataTemplate>
                                                            </asp:ListView>
                                                        </asp:Panel>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>






                                <div class="card" id="DivMod" runat="server">
                                    <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseEight" aria-expanded="false">
                                        <span class="title">Module Details</span>
                                        <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                                    </div>
                                    <div id="collapseEight" class="collapse show">
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

                                <div class="card" id="DivPayment" runat="server">
                                    <div class="card-header collapsed" data-toggle="collapse" data-target="#collapseNine" aria-expanded="false">
                                        <span class="title">Payment Details</span>
                                        <span class="accicon"><i class="fa fa-angle-down rotate-icon"></i></span>
                                    </div>

                                    <div id="collapseNine" class="collapse show">
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
                                                                        <%--<div class="sub-heading">
                                                                            <h5>Fees Details</h5>
                                                                        </div>--%>
                                                                        <table class="table table-striped table-bordered nowrap" style="width: 100%" id="tblHead">
                                                                            <thead class="bg-light-blue">
                                                                                <tr id="trRow">
                                                                                    <th>StudentName
                                                                                    </th>
                                                                                    <th>Payment Type
                                                                                    </th>
                                                                                    <th>Amount
                                                                                    </th>
                                                                                    <th>Semester
                                                                                    </th>
                                                                                    <th>Date
                                                                                    </th>
                                                                                    <th>Order ID
                                                                                    </th>
                                                                                    <th>Transaction ID
                                                                                    </th>
                                                                                    <th>Receipt No
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
                                                                            <asp:Label ID="lblEnrollmentno" runat="server" Text='<%#Eval("ENROLLNMENTNO") %>' Visible="false"></asp:Label>
                                                                            <asp:Label ID="lblIdno" runat="server" Text='<%#Eval("IDNO") %>' Visible="false"></asp:Label>
                                                                            <asp:Label ID="lblUsername" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblPaymentType" runat="server" Text='<%#Eval("PAY_TYPE") %>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblAmount" runat="server" Text='<%#Eval("TOTAL_AMT") %>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblSemester" runat="server" Text='<%#Eval("SEMESTERNAME") %>'></asp:Label>
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
                                                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("REC_NO") %>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Button ID="btnReport" runat="server" Text="Report" CssClass="btn btn-outline-info" OnClick="btnReport_Click" />
                                                                        </td>
                                                                        <%--    <td>
                                                                <asp:LinkButton ID="lnkViewSlip" runat="server" CommandArgument='<%#Eval("TEMP_DCR_NO") %>' OnClick="lnkViewSlip_Click" CommandName='<%#Eval("DOC_FILENAME") %>' CssClass="btn btn-outline-info" Visible='<%#Eval("PAY_TYPE").ToString() == "OFFLINE" ? true:false %>'>
                                                                 <i class="fa fa-search" aria-hidden="true"></i> View  
                                                                </asp:LinkButton>
                                                            </td>--%>
                                                                    </tr>
                                                                </ItemTemplate>
                                                                <EmptyDataTemplate>
                                                                    No Record Found !!!
                                                                </EmptyDataTemplate>

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

                                <asp:UpdatePanel ID="updfinalblock" runat="server">
                                    <ContentTemplate>
                                        <div class="col-12 mt-4">
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

                                            </div>
                                        </div>

                                        <div class="col-12 btn-footer">

                                            <asp:LinkButton ID="lnkFinalSubmit" runat="server" CssClass="btn btn-outline-info d-none" OnClick="lnkFinalSubmit_Click">Submit</asp:LinkButton>
                                            <asp:LinkButton ID="lnkSendEmail" runat="server" CssClass="btn btn-outline-info" OnClick="lnkSendEmail_Click" ValidationGroup="btnSubmit">Send Enrollment Confirmation</asp:LinkButton>
                                            <asp:LinkButton ID="lnkGeneratereport" runat="server" CssClass="btn btn-outline-info d-none" OnClick="lnkGeneratereport_Click" Visible="false">Print Enrollment Form</asp:LinkButton>
                                            <asp:LinkButton ID="lnkPrintReport" runat="server" CssClass="btn btn-outline-primary" OnClick="lnkPrintReport_Click" Visible="false">Summary Sheet</asp:LinkButton>
                                            <asp:LinkButton ID="LinkButton4" runat="server" CssClass="btn btn-outline-danger d-none">Back</asp:LinkButton>

                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="btnSubmit" />

                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>


                                <asp:UpdatePanel ID="updModel" runat="server">
                                    <ContentTemplate>
                                        <div id="myModal22" class="modal fade" role="dialog">
                                            <div class="modal-dialog modal-lg">
                                                <div class="modal-content" style="margin-top: -25px">
                                                    <div class="modal-body">
                                                        <div class="modal-header">
                                                            <button type="button" class="close" data-dismiss="modal" style="margin-top: -18px">x</button>
                                                        </div>

                                                        <asp:Image ID="ImageViewer" runat="server" Width="100%" Height="500px" Visible="false" />
                                                        <asp:Literal ID="ltEmbed" runat="server" />
                                                        <%--<iframe id="iframe1" runat="server" frameborder="0" width="100%" height="800px" visible="false"></iframe>--%>
                                                        <%--<iframe id="iframe1" runat="server" src="../FILEUPLOAD/Transcript Report.pdf" frameborder="0" width="100%" height="547px"></iframe>--%>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <%-- Search Start--%>
    <div class="modal fade" id="ModelSearchPopup">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">

                <!-- Modal Header -->
                <div class="modal-header">
                    <h4 class="modal-title">Search</h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div>
                    <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="updSearchStudent"
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
                <asp:UpdatePanel ID="updSearchStudent" runat="server">
                    <ContentTemplate>
                        <!-- Modal body -->
                        <div class="modal-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-12 mb-3">
                                        <asp:RadioButtonList ID="rdSearch" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1">&nbsp;Email&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="2">&nbsp;Mobile No.&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="4">&nbsp;Registration No.&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="3">&nbsp;NIC&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="5">&nbsp;Passport No.&nbsp;</asp:ListItem>
                                            <asp:ListItem Value="0">&nbsp;Student Name&nbsp;</asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please select Search Option"
                                            ControlToValidate="rdSearch" Display="None" ValidationGroup="search"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Enter Search String" MaxLength="32"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Enter Search String"
                                            ControlToValidate="txtSearch" Display="None" ValidationGroup="search"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-outline-info" OnClick="btnSearch_Click" ValidationGroup="search" />
                                        <asp:Button ID="btnClearSearch" runat="server" Text="Clear" CssClass="btn btn-outline-danger" OnClick="btnClearSearch_Click" />
                                        <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="search" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 mt-3">
                                <asp:Panel ID="Panel7" runat="server" ScrollBars="Auto">
                                    <asp:ListView ID="lvStudent" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Student Details</h5>
                                            </div>
                                            <asp:Panel ID="Panel2" runat="server">
                                                <div class="table-responsive" style="height: 320px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                    <table class="table table-striped table-bordered nowrap mb-0" style="width: 100%;">
                                                        <thead class="bg-light-blue" style="position: sticky; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                            <tr>
                                                                <th>Registration No.
                                                                </th>
                                                                <th>Student Name
                                                                </th>
                                                                <th>Email
                                                                </th>
                                                                <th>Mobile No
                                                                </th>
                                                                <th>NIC
                                                                </th>
                                                                <th>Passport No
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </asp:Panel>
                                        </LayoutTemplate>
                                        <ItemTemplate>

                                            <tr>
                                                <td>
                                                    <asp:LinkButton ID="lnkUsername" runat="server" Text='<%# Eval("REGNO") %>' CommandArgument='<%# Eval("IDNO") %>' OnClick="lnkUsername_Click"></asp:LinkButton>
                                                </td>
                                                <td>
                                                    <%# Eval("STUDFIRSTNAME")%>
                                                </td>
                                                <td>
                                                    <%# Eval("EMAILID")%>
                                                </td>
                                                <td>
                                                    <%# Eval("STUDENTMOBILE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("NIC")%>
                                                </td>
                                                <td>
                                                    <%# Eval("PASSPORTNO")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>

                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                        <asp:AsyncPostBackTrigger ControlID="btnClearSearch" />
                        <asp:PostBackTrigger ControlID="lvStudent" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <%-- Search End--%>



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
    <script>
        function DivExpandCollese(divName) {
            var div = document.getElementById(divName);

            //var image = document.getElementById('img', +divName);
            if (div.style.display == "none") {
                div.style.display = "contents";
            }
            else {
                div.style.display = "none"
            }
        }

    </script>
    <script>
        function DivExpandCollese1(divName) {
            var div = document.getElementById(divName);
            //var image = document.getElementById('img', +divName);
            if (div.style.display == "none") {
                div.style.display = "contents";
            }
            else {
                div.style.display = "none"
            }
        }

    </script>


    <script type="text/javascript">
        $(document).ready(function () {
            $("#SearchPopUp").click(function () {
                //alert('hii')
                $("#ModelSearchPopup").modal();

            });
        });

    </script>
    <script type="text/javascript">
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $("#SearchPopUp").click(function () {
                    //alert('hii')
                    $("#ModelSearchPopup").modal();

                });
            });
        })
    </script>





</asp:Content>

