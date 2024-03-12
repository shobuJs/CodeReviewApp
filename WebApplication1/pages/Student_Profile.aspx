<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Student_Profile.aspx.cs" Inherits="Academic_Student_Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .nav-tabs .nav-item.show .nav-link, .nav-tabs .nav-link.active {
            border-color: #f3f8ff;
        }

        /*========= File Upload Styling ==========*/
        .logoContainers img {
            width: 100%;
            height: 45px;
        }

            .logoContainers img:focus {
                color: #495057;
                background-color: #fff;
                border-color: #80bdff;
                outline: 0;
                box-shadow: 0 0 0 0.2rem rgba(0,123,255,.25);
            }

        .fileContainers {
            position: relative;
            cursor: pointer;
        }

            .fileContainers span {
                overflow: hidden;
                font-weight: bold;
                display: block;
                white-space: nowrap;
                text-overflow: ellipsis;
                cursor: pointer;
            }

            .fileContainers input[type="file"] {
                opacity: 0;
                margin: 0;
                padding: 0;
                width: 100%;
                height: 100%;
                left: 0;
                top: 0;
                position: absolute;
                cursor: pointer;
                color: #495057;
            }
        /*========= File Upload Styling END ==========*/

        .nav-tabs-custom .nav-tabs .nav-item.show .nav-link, .nav-tabs-custom .nav-tabs .nav-link.active {
            color: #255282;
            background-color: #bcdcff;
            border-top: 1px solid #bcdcff;
            border-color: #bcdcff #bcdcff #bcdcff;
        }

        .nav-tabs-custom .nav-tabs .nav-link:focus, .nav-tabs-custom .nav-tabs .nav-link:hover {
            border-color: #bcdcff #bcdcff #bcdcff;
            background-color: #bcdcff;
        }

        .nav-tabs-custom .nav-tabs .nav-item {
            margin-bottom: 0px;
            border-bottom: 1px solid #eee;
        }

        .form-control {
            border-top: none;
            border-left: none;
            border-right: none;
            border-bottom: 1px solid #ccc;
        }
    </style>
    <%--<link href='<%=Page.ResolveClientUrl("~/plugin/custom/css/jquery-ui.css")%>' rel="stylesheet" />--%>
    <link href="../plugins/jQuery/jquery_ui_min/jquery-ui.css" rel="stylesheet" />
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <link href="<%=Page.ResolveClientUrl("~/plugins/sweetalert2-11.7.5/sweetalert2.min.css") %>" rel="stylesheet" />
    <link href="<%=Page.ResolveClientUrl("~/plugins/iziToast-master/css/iziToast.min.css") %>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/sweetalert2-11.7.5/sweetalert2.min.js")%>"></script>
    <script src="<%=Page.ResolveClientUrl("~/plugins/iziToast-master/js/iziToast.min.js")%>"></script>
    <div class="col-12" id="DivfetchingForLabels">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-body">
                        <div class="nav-tabs-custom">
                            <div class="row">
                                <div class="col-sm-3 col-lg-2 col-12">
                                    <div class="card mb-4">
                                        <!-- Page Main Heading -->
                                        <div class="card-header">
                                            <div class="heading">
                                                <asp:Label runat="server" ID="Label1">Student Profile</asp:Label>
                                                <asp:HiddenField ID="hdnClientId" runat="server" Value="0" />
                                            </div>
                                        </div>
                                        <div class="card-body p-0">
                                            <div class="std-profile-links">
                                                <ul class="nav nav-tabs list-group">
                                                    <%--  <li class="nav-item">
                                        <a class="list-group-item nav-link active" data-bs-toggle="tab" href="#SearchStd">Search Student</a>
                                    </li>--%>
                                                    <li class="nav-item">
                                                        <a class="list-group-item nav-link active" data-toggle="tab" href="#tab_1">Personal Details</a>
                                                    </li>
                                                    <li class="nav-item">
                                                        <a class="list-group-item nav-link" data-toggle="tab" href="#tab_2" onclick="TabStudentAddress(this)">Address Details</a>
                                                    </li>
                                                    <li class="nav-item d-none">
                                                        <a class="list-group-item nav-link" data-toggle="tab" href="#tab_3" onclick="TabCourseDetails(this)">Course Details</a>
                                                    </li>
                                                    <li class="nav-item d-none">
                                                        <a class="list-group-item nav-link" data-toggle="tab" href="#tab_4" onclick="TabPhotoSignature(this)">Photo Signature</a>
                                                    </li>
                                                    <li class="nav-item">
                                                        <a class="list-group-item nav-link" data-toggle="tab" href="#tab_6" onclick="TabEnlistedSubjects(this)">Enlisted Subjects</a>
                                                    </li>
                                                    <li class="nav-item">
                                                        <a class="list-group-item nav-link" data-toggle="tab" href="#tab_5" onclick="TabStudentGrades(this)">Grades</a>
                                                    </li>

                                                    <li class="nav-item d-none" style="display: none">
                                                        <a class="list-group-item nav-link" data-toggle="tab" href="#tab_7">Class Schedule</a>
                                                    </li>
                                                    <li class="nav-item d-none">
                                                        <a class="list-group-item nav-link" data-toggle="tab" href="#tab_8" onclick="TabStudentAttendance(this)">Attendance</a>
                                                    </li>
                                                    <li class="nav-item d-none">
                                                        <a class="list-group-item nav-link" data-toggle="tab" href="#tab_9" onclick="TabOverallResult(this)">Overall Result</a>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-9 col-lg-10 col-12">
                                    <div class="tab-content">
                                        <!-- Active Tab -->
                                        <div class="tab-pane " id="SearchStd">
                                        </div>

                                        <!-- Personal Details Tab -->
                                        <div class="tab-pane active" id="tab_1">

                                            <div class="card mb-4">
                                                <!-- Page Main Heading -->
                                                <div runat="server" visible="true" id="DivSearch">
                                                    <div class="card-header">
                                                        <div class="heading">
                                                            <asp:Label runat="server" ID="Label3">Search Student</asp:Label>
                                                        </div>
                                                    </div>

                                                    <!-- Page Main Body Content -->
                                                    <div class="card-body">
                                                        <div class="col-12">
                                                            <div class="row">
                                                                <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                                    <div class="form-group">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label>Search Student </label>
                                                                        </div>
                                                                        <input type="search" class="form-control search" tabindex="0" id="txtSearchStudent" />

                                                                    </div>
                                                                </div>
                                                                <div class="col-xl-3 col-lg-4 col-sm-6 col-12 mt-3">
                                                                    <div class="form-group">
                                                                        <input type="button" class="btn btn-sm btn-outline-primary" tabindex="0" value="Search" id="btnSearch" />
                                                                        <asp:Button ID="btnCancel" runat="server" class="btn btn-sm btn-outline-danger" TabIndex="0" Text="Cancel" OnClick="btnCancel_Click" />
                                                                        <%--<input type="button" class="btn btn-sm btn-outline-danger" tabindex="0" value="Cancel" id="btnCancel" />--%>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>
                                                <!-- Page Main Heading -->
                                                <div class="card-header">
                                                    <div class="heading">
                                                        <asp:Label runat="server" ID="lblDynamicPageTitle">Personal Details</asp:Label>
                                                    </div>
                                                </div>

                                                <!-- Page Main Body Content -->
                                                <div class="card-body">
                                                    <div class="row">
                                                        <div class="col-xl-6 col-lg-6 col-sm-6 col-12 border-right">
                                                            <div class="pb-1">
                                                                <label><span class="studentId">Student Id</span></label>
                                                                <label class="float-right text-primary" id="lblStuddentIDBind">-</label>
                                                            </div>
                                                            <div class="pb-1">
                                                                <label><span class="Campus">Campus</span></label>
                                                                <label class="float-right text-primary" id="lblCampusBind">-</label>
                                                            </div>
                                                            <div class="pb-1">
                                                                <label><span class="College">College</span></label>
                                                                <label class="float-right text-primary" id="lblCollegeBind">-</label>

                                                            </div>
                                                        </div>
                                                        <div class="col-xl-6 col-lg-6 col-sm-6 col-12">
                                                            <div class="pb-1">
                                                                <label><span class="Courses">Courses</span></label>
                                                                <label class="float-right text-primary" id="lblCourseBind">-</label>
                                                            </div>
                                                            <div class="pb-1">
                                                                <label><span class="Curriculum">Curriculum</span></label>
                                                                <label class="float-right text-primary" id="lblCurriculumBind">-</label>
                                                            </div>
                                                            <div class="pb-1">
                                                                <label><span class="Semester">Semester</span></label>
                                                                <label class="float-right text-primary" id="lblSemesterBind">-</label>
                                                            </div>
                                                        </div>
                                                        <%--<div class="col-xl-1 col-lg-1 col-sm-2 col-6">
                                            <div class="profile-image text-center mt-3 mt-lg-0">
                                                <img src="../image/nophoto.jpg" alt="image" tabindex="0" />
                                            </div>
                                        </div>--%>
                                                    </div>

                                                    <%--<div class="text-end mt-3">
                                        <input type="button" class="btn btn-sm btn-outline-primary" tabindex="0" value="Enable" id="btnEnable" />
                                    </div>--%>

                                                    <div class="col-12 mt-3 border rounded p-lg-3 pb-0 p-2">
                                                        <div class="row">
                                                            <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                                <div class="form-group">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label><span class="LastName">Last Name</span></label>
                                                                    </div>
                                                                    <input id="txtLastName" class="form-control restrict-Name-space" tabindex="0" spellcheck="true" maxlength="50" />
                                                                    <input type="hidden" id="hdnIdno" />
                                                                    <input type="hidden" id="hdnLastName" />
                                                                </div>
                                                            </div>

                                                            <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                                <div class="form-group">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label><span class="FirstName">First Name</span></label>
                                                                    </div>
                                                                    <input id="txtFirstName" class="form-control restrict-Name-space" tabindex="0" spellcheck="true" maxlength="50" />
                                                                    <input type="hidden" id="hdnRegno" />
                                                                    <input type="hidden" id="hdnFirstName" />
                                                                </div>
                                                            </div>

                                                            <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                                <div class="form-group">
                                                                    <div class="label-dynamic">
                                                                        <sup></sup>
                                                                        <label><span class="MiddleName">Middle Name</span></label>
                                                                    </div>
                                                                    <input id="txtMiddleName" class="form-control restrict-Name-space" tabindex="0" spellcheck="true" maxlength="50" />
                                                                    <input type="hidden" id="hdnMiddleName" />
                                                                </div>
                                                            </div>

                                                            <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                                <div class="form-group">
                                                                    <div class="label-dynamic">
                                                                        <sup></sup>
                                                                        <label><span class="ExtensionName">Extension Name</span></label>
                                                                    </div>
                                                                    <input id="txtExtensionName" class="form-control restrict-Name-space" tabindex="0" spellcheck="true" maxlength="50" />
                                                                </div>
                                                            </div>

                                                            <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                                <div class="form-group">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label><span class="Gender">Gender</span></label>
                                                                    </div>
                                                                    <select id="ddlGender" class="form-control" data-select2-enable="true" spellcheck="true" tabindex="0">
                                                                        <option value="0">Please Select</option>
                                                                    </select>
                                                                </div>
                                                            </div>

                                                            <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                                <div class="form-group">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label><span class="dob">Date Of Birth</span></label>
                                                                    </div>
                                                                    <input id="txtdob" class="form-control" tabindex="0" type="date"/>
                                                                </div>
                                                            </div>

                                                            <div class="col-xl-3 col-lg-4 col-sm-6 col-12" style="display: none">
                                                                <div class="form-group">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label>Age</label>
                                                                    </div>
                                                                    <input id="txtStudentAge" class="form-control restrict-numbers-only" tabindex="0" spellcheck="true" maxlength="2" />
                                                                </div>
                                                            </div>

                                                            <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                                <div class="form-group">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label>Place of Birth</label>
                                                                    </div>
                                                                    <input id="txtBirthPlace" class="form-control restrict-alphabet-space" tabindex="0" placeholder="e.g. Philippines" spellcheck="true" maxlength="50" />
                                                                </div>
                                                            </div>

                                                            <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                                <div class="form-group">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label><span class="CivilStatus">Civil Status</span></label>
                                                                    </div>
                                                                    <select id="ddlCivilStatus" class="form-control" data-select2-enable="true" spellcheck="true" tabindex="0" required>
                                                                        <option value="0">Please Select</option>
                                                                        <option value="1">Single </option>
                                                                        <option value="2">Married </option>
                                                                        <option value="3">Widowed </option>
                                                                        <option value="4">Separated </option>
                                                                    </select>
                                                                </div>
                                                            </div>

                                                            <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                                <div class="form-group">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label><span class="Citizenship">Citizenship</span></label>
                                                                    </div>
                                                                    <select id="ddlCitizenship" class="form-control" data-select2-enable="true" spellcheck="true" tabindex="0" required>
                                                                        <option value="0">Please Select</option>
                                                                        <%--   <option value="1">Foreign </option>
                                                                        <option value="2">Philippines </option>--%>
                                                                    </select>
                                                                </div>
                                                            </div>
                                                            <div id="divDualCitizenship" class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                                <div class="form-group">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label><span class="ClsDualCitizenship">Do you have Dual Citizenship ?</span></label>
                                                                    </div>
                                                                    <select id="ddlDualCitizenship" class="form-control" data-select2-enable="true" spellcheck="true" tabindex="0" required>

                                                                        <option value="1">Yes </option>
                                                                        <option value="0">No </option>
                                                                    </select>
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                                <div class="form-group">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label><span class="Religion">Religion</span></label>
                                                                    </div>
                                                                    <select id="ddlReligion" class="form-control" data-select2-enable="true" spellcheck="true" tabindex="0" required>
                                                                        <option value="0">Please Select</option>
                                                                    </select>
                                                                </div>
                                                            </div>

                                                            <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                                <div class="form-group phone-no-dropdown position-relative">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label><span class="MobileNo">Mobile No.</span></label>
                                                                    </div>
                                                                    <input id="txtMobileNo" type="text" class="form-control restrict-numbers-only" placeholder="e.g. 0987654321" tabindex="0" maxlength="15">
                                                                </div>
                                                            </div>

                                                            <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                                <div class="form-group">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label><span class="Email">Email</span></label>
                                                                    </div>
                                                                    <input id="txtEmail" class="form-control" tabindex="0" type="email" placeholder="e.g. abc@gmail.com" spellcheck="true" maxlength="75" />
                                                                    <input type="hidden" id="hdnEmail" />
                                                                </div>
                                                            </div>

                                                            <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                                <div class="form-group">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label><span class="FacebookName">Facebook Name</span></label>
                                                                    </div>
                                                                    <input id="txtFacebookName" class="form-control restrict-Name-space" placeholder="e.g. Abad" tabindex="0" spellcheck="true" maxlength="50" />
                                                                </div>
                                                            </div>
                                                            <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                                <div class="form-group">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                        <label><span class="ClsFacebookLink">Facebook Link</span></label>
                                                                    </div>
                                                                    <input id="txtFacebookLink" class="form-control restrict-numbers-alphabet-characters" tabindex="0" spellcheck="true" maxlength="50" placeholder="e.g. https://facebook.com" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row mt-3">
                                                        <div class="col-12">
                                                            <div class="sub-heading">
                                                                <h5>Family Details</h5>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-xl-3 col-lg-4 col-sm-6 col-12 mb-4">
                                                            <div class="form-inline">
                                                                <div class="form-group">
                                                                   <%-- <sup>* </sup>--%>
                                                                    <label><span class="ClsFatherDetails">Not to Specify</span></label>
                                                                </div>
                                                                <div class="form-check form-switch ml-2">
                                                                    <input class="form-check-input" type="checkbox" title="Not to Specify Father's Details" role="switch" id="StatusFatherDetails" onchange="fatherdetails()" tabindex="0">
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                            <div class="form-group">
                                                                <div class="label-dynamic">
                                                                    <sup class="fatherinfo">* </sup>
                                                                    <label><span class="FatherFullName">Father Full Name</span></label>
                                                                </div>
                                                                <input id="txtFatherName" class="form-control restrict-Name-space" tabindex="0" type="text" placeholder="e.g. Bautista" spellcheck="true" maxlength="50" />
                                                            </div>
                                                        </div>

                                                        <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                            <div class="form-group">
                                                                <div class="label-dynamic">
                                                                    <sup class="fatherinfo">* </sup>
                                                                    <label><span class="FatherOccupation">Father Occupation</span></label>
                                                                </div>
                                                                <select id="ddlFatherOccupation" class="form-control" data-select2-enable="true" tabindex="0">
                                                                    <option value="0">Please Select</option>
                                                                </select>
                                                            </div>
                                                        </div>

                                                        <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                            <div class="form-group">
                                                                <div class="label-dynamic">
                                                                    <sup class="fatherinfo">* </sup>
                                                                    <label><span class="FatherMobileNo">Father Mobile No.</span></label>
                                                                </div>
                                                                <input id="txtMobNoF" type="text" class="form-control restrict-numbers-only" tabindex="0" placeholder="e.g. 0987654321" maxlength="15" />
                                                            </div>
                                                        </div>

                                                    </div>

                                                    <div class="row">
                                                        <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                            <div class="form-group">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label><span class="MotherFullName">Mother Full Name</span></label>
                                                                </div>
                                                                <input id="txtMotherName" class="form-control restrict-Name-space" tabindex="0" placeholder="e.g. Nighara" type="text" spellcheck="true" maxlength="50" />
                                                            </div>
                                                        </div>

                                                        <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                            <div class="form-group">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label><span class="MotherOccupation">Mother Occupation</span></label>
                                                                </div>
                                                                <select id="ddlMotherOccupation" class="form-control" data-select2-enable="true" tabindex="0">
                                                                    <option value="0">Please Select</option>
                                                                </select>
                                                            </div>
                                                        </div>

                                                        <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                            <div class="form-group">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label><span class="MotherMobileNo">Mother Mobile No.</span></label>
                                                                </div>
                                                                <input id="txtMobNoM" type="text" class="form-control restrict-numbers-only" tabindex="0" placeholder="e.g. 0987654321" maxlength="15" />
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                            <div class="form-group">
                                                                <div class="label-dynamic">
                                                                    <sup class="SpouseVali">* </sup>
                                                                    <label><span class="NameofSpouse">Name of Spouse</span></label>
                                                                </div>
                                                                <input id="txtSpouseName" class="form-control restrict-Name-space" tabindex="0" spellcheck="true" maxlength="50" placeholder="e.g Rodriguez" />
                                                            </div>
                                                        </div>

                                                        <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                            <div class="form-group">
                                                                <div class="label-dynamic">
                                                                    <sup class="SpouseVali">* </sup>
                                                                    <label><span class="SpouseOccupation">Spouse Occupation</span></label>
                                                                </div>
                                                                <select id="ddlSpouseOccupation" class="form-control" data-select2-enable="true" tabindex="0">
                                                                    <option value="0">Please Select</option>
                                                                </select>
                                                            </div>
                                                        </div>
                                                        <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                            <div class="form-group">
                                                                <div class="label-dynamic">
                                                                    <sup class="SpouseVali">* </sup>
                                                                    <label><span class="NumberofChildren">Number of Children</span></label>
                                                                </div>
                                                                <input id="txtNumberofChildren" class="form-control restrict-numbers-only" tabindex="0" spellcheck="true" maxlength="2" placeholder="e.g. 2" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                            <div class="form-group">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label><span class="NoofBrothers">No.of Brothers</span></label>
                                                                </div>
                                                                <input id="txtNumberofBrothers" class="form-control restrict-numbers-only" tabindex="0" placeholder="e.g. 2" spellcheck="true" maxlength="2" />
                                                            </div>
                                                        </div>

                                                        <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                            <div class="form-group">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label><span class="NoofSisters">No.of Sisters</span></label>
                                                                </div>
                                                                <input id="txtNumberofSisters" class="form-control restrict-numbers-only" tabindex="0" placeholder="e.g. 2" spellcheck="true" maxlength="2" />
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row mt-4">
                                                        <div class="col-12">
                                                            <div class="sub-heading">
                                                                <h5>Emergency Person Contact Details</h5>
                                                            </div>
                                                        </div>
                                                        <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                            <div class="form-group">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label><span class="EmergencyPersonName">Person Name</span></label>
                                                                </div>
                                                                <input id="txtEmergencyPersonName" class="form-control restrict-Name-space" tabindex="0" placeholder="e.g. Dian" spellcheck="true" />
                                                            </div>
                                                        </div>

                                                        <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                            <div class="form-group">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label><span class="Relationship">Relationship</span></label>
                                                                </div>
                                                                <select id="ddlRelationship" class="form-control" data-select2-enable="true" spellcheck="true" tabindex="0">
                                                                    <option value="0">Please Select</option>
                                                                </select>
                                                            </div>
                                                        </div>

                                                        <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                            <div class="form-group ephone-no-dropdown position-relative">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label><span class="MobileNo">Mobile No.</span></label>
                                                                </div>
                                                                <input id="txtEmergencyMobileNo" type="text" class="form-control ephoneNo restrict-numbers-only" placeholder="e.g. 0987654321" tabindex="0" spellcheck="true" maxlength="15">
                                                            </div>
                                                        </div>

                                                        <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                            <div class="form-group">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label><span class="Address">Address</span></label>
                                                                </div>
                                                                <textarea id="txtAddress" class="form-control" tabindex="0" spellcheck="true" placeholder="Enter Address" rows="1"></textarea>

                                                            </div>
                                                        </div>

                                                    </div>
                                                    <asp:Panel ID="ForiegnStudent" runat="server">
                                                        <div class="mt-4">
                                                            <div class="col-12">
                                                                <div class="sub-heading">
                                                                    <h5>Foreign Students Only</h5>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                                    <div class="form-group">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label><span class="ClsPassportNo">Passport No.</span></label>
                                                                        </div>
                                                                        <input id="txtPassportNo" class="form-control" tabindex="0" spellcheck="true" maxlength="33" placeholder="e.g. PSGKPK000" />
                                                                    </div>
                                                                </div>

                                                                <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                                    <div class="form-group">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label><span class="ClsDateIssued">Issued Date</span></label>
                                                                        </div>
                                                                        <input id="txtDateIssued" class="form-control" tabindex="0" type="date" spellcheck="true" />
                                                                    </div>
                                                                </div>

                                                                <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                                    <div class="form-group ephone-no-dropdown position-relative">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label><span class="ClsPlaceIssued">Issued Country</span></label>
                                                                        </div>
                                                                        <select id="ddlCountry" class="form-control" data-select2-enable="true" tabindex="0">
                                                                            <option value="0">Please Select</option>
                                                                        </select>
                                                                    </div>
                                                                </div>

                                                                <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                                    <div class="form-group">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label><span class="ClsTypeofVisa">Type of Visa</span></label>
                                                                        </div>
                                                                        <select id="ddlTypeofVisa" class="form-control" data-select2-enable="true" tabindex="0">
                                                                            <option value="0">Please Select</option>
                                                                        </select>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="row">
                                                                <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                                    <div class="form-group">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label><span class="ClsVisaStatus">Visa Status</span></label>
                                                                        </div>
                                                                        <select id="ddlVisaStatus" class="form-control" data-select2-enable="true" tabindex="0">
                                                                            <option value="0">Please Select</option>
                                                                        </select>
                                                                    </div>
                                                                </div>

                                                                <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                                    <div class="form-group">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label><span class="ClsICardNo">I-Card No.</span></label>
                                                                        </div>
                                                                        <input id="txtICardNo" class="form-control " tabindex="0" spellcheck="true" maxlength="38" placeholder="e.g. IC0000" />
                                                                    </div>
                                                                </div>

                                                                <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                                    <div class="form-group ephone-no-dropdown position-relative">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label><span class="ClsAuthorizedStayFrom">Authorized Stay From</span></label>
                                                                        </div>
                                                                        <input id="txtAuthorizedStayFrom" class="form-control" tabindex="0" type="date"/>
                                                                    </div>
                                                                </div>

                                                                <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                                    <div class="form-group">
                                                                        <div class="label-dynamic">
                                                                            <sup>* </sup>
                                                                            <label><span class="ClsAuthorizedStayTo">Authorized Stay To</span></label>
                                                                        </div>
                                                                        <input id="txtAuthorizedStayTo" class="form-control" tabindex="0" type="date" spellcheck="true" />
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div>
                                                                <div class="row">
                                                                    <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                                        <div class="form-group">
                                                                            <div class="label-dynamic">
                                                                                <sup>* </sup>
                                                                                <label><span class="ClsRemark">Remark</span></label>
                                                                            </div>
                                                                            <input id="txtRemark" class="form-control" tabindex="0" spellcheck="true" maxlength="50" placeholder="Enter Remark" />
                                                                        </div>
                                                                    </div>


                                                                </div>
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                    <div class="text-center mt-3 mb-3">
                                                        <input type="button" class="btn btn-sm btn-outline-primary" tabindex="0" disabled="disabled" value="Submit" id="btnSubmit" />
                                                        <%--      <input type="button" class="btn btn-sm btn-outline-danger" tabindex="28" value="Cancel" id="btnClear"  />--%>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>

                                        <!-- Address Details Tab -->
                                        <div class="tab-pane fade" id="tab_2">
                                            <div class="card mb-4">
                                                <!-- Page Main Heading -->
                                                <div class="card-header">
                                                    <div class="heading">
                                                        <asp:Label runat="server" ID="Label2">Address Details</asp:Label>
                                                    </div>
                                                </div>
                                                <div class="card-body">
                                                    <%-- <div class="text-end">
                                        <input type="button" class="btn btn-sm btn-outline-primary" tabindex="0" value="Enable" id="btnEnableAdd" />
                                    </div>--%>

                                                    <div class="row">
                                                        <div class="col-12">
                                                            <div class="sub-heading">
                                                                <h5>
                                                                    <label><span class="ClsCurrentAddress">Current Address</span></label></h5>
                                                            </div>
                                                        </div>

                                                        <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                            <div class="form-group">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label><span class="Address">Address</span></label>
                                                                </div>
                                                                <textarea id="txtCaddressC" class="form-control" tabindex="0" spellcheck="true" rows="1" maxlength="256"></textarea>
                                                            </div>
                                                        </div>

                                                        <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                            <div class="form-group">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label><span class="Country">Country</span></label>

                                                                </div>
                                                                <select id="ddlCountryC" class="form-control" data-select2-enable="true" tabindex="0" onchange="CountryCChange(this)">
                                                                    <%--onchange="CountryCChange()"--%>
                                                                    <option value="0">Please Select</option>
                                                                </select>
                                                            </div>
                                                        </div>

                                                        <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                            <div class="form-group">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label><span class="Province">Province</span></label>

                                                                </div>
                                                                <select id="ddlProvinceC" class="form-control" data-select2-enable="true" tabindex="0" onchange="ProvinceCChange(this)">
                                                                    <%-- onchange="ProvinceCChange()"--%>
                                                                    <option value="0">Please Select</option>
                                                                </select>
                                                            </div>
                                                        </div>

                                                        <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                            <div class="form-group">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label><span class="City">City</span></label>
                                                                </div>
                                                                <select id="ddlMunicipalityC" class="form-control" data-select2-enable="true" tabindex="0" onchange="CityCChange(this)">
                                                                    <option value="0">Please Select</option>
                                                                </select>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row mt-4">
                                                        <div class="col-12">

                                                            <div class="sub-heading">
                                                                <h5>
                                                                    <label><span class="ClsPermanentAddress">Permanent Address</span></label></h5>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-xl-3 col-lg-4 col-sm-6 col-12 mb-4">
                                                            <div class="form-inline">
                                                                <div class="form-group">
                                                                    <div class="label-dynamic">
                                                                        <label><span class="ClsSameAddress">Same as Current Address ?</span></label>
                                                                    </div>
                                                                    <div class="form-check form-switch ml-2">
                                                                        <input class="form-check-input" type="checkbox" role="switch" id="Statussameaddress" onchange="sameaddress()" tabindex="0">
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                            <div class="form-group">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label><span class="Address">Address</span></label>
                                                                </div>
                                                                <textarea id="txtPAddressP" class="form-control" tabindex="0" spellcheck="true" rows="1" maxlength="256"></textarea>

                                                            </div>
                                                        </div>

                                                        <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                            <div class="form-group">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label><span class="Country">Country</span></label>
                                                                </div>
                                                                <select id="ddlCountryP" class="form-control" data-select2-enable="true" tabindex="0" onchange="CountryPChange(this)">
                                                                    <%--onchange="CountryPChange()"--%>
                                                                    <option value="0">Please Select</option>
                                                                </select>
                                                            </div>
                                                        </div>

                                                        <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                            <div class="form-group">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label><span class="Province">Province</span></label>
                                                                </div>
                                                                <select id="ddlProvinceP" class="form-control" data-select2-enable="true" tabindex="0" onchange="ProvincePChange(this)">
                                                                    <%-- onchange="ProvincePChange()"--%>
                                                                    <option value="0">Please Select</option>
                                                                </select>
                                                            </div>
                                                        </div>

                                                        <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                            <div class="form-group">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label><span class="City">City</span></label>
                                                                </div>
                                                                <select id="ddlMunicipalityP" class="form-control" data-select2-enable="true" tabindex="0">
                                                                    <option value="0">Please Select</option>
                                                                </select>
                                                            </div>
                                                        </div>


                                                    </div>

                                                    <div class="text-center mt-3 mb-3">
                                                        <input type="button" class="btn btn-sm btn-outline-primary" tabindex="0" disabled="disabled" value="Submit" id="btnSubmitAdd" />
                                                        <%--   <input type="button" class="btn btn-sm btn-outline-danger" tabindex="0" value="Cancel" id="btnCancelAdd" />--%>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <!-- Course Details Tab -->
                                        <div class="tab-pane fade" id="tab_3">
                                            <div class="card mb-4">
                                                <!-- Page Main Heading -->
                                                <div class="card-header">
                                                    <div class="heading">
                                                        <asp:Label runat="server" ID="Label4">Course Details</asp:Label>
                                                    </div>
                                                </div>
                                                <div class="card-body">

                                                    <div class="row">
                                                        <div class="col-xl-6 col-lg-6 col-sm-6 col-12 border-right">
                                                            <div class="pb-1">
                                                                <label><span class="Intake">Intake</span></label>
                                                                <label class="float-right text-primary" id="lblIntakeNa">-</label>
                                                            </div>
                                                            <div class="pb-1">
                                                                <label><span class="College">College</span></label>
                                                                <label class="float-right text-primary" id="lblCollegeNa">-</label>
                                                            </div>
                                                            <div class="pb-1">
                                                                <label><span class="Degree">Degree</span></label>
                                                                <label class="float-right text-primary" id="lblCourseNa">-</label>

                                                            </div>
                                                            <div class="pb-1">
                                                                <label><span class="Advisor">Advisor</span></label>
                                                                <label class="float-right text-primary" id="lblAdvisorNa">-</label>

                                                            </div>
                                                        </div>
                                                        <div class="col-xl-6 col-lg-6 col-sm-6 col-12">
                                                            <div class="pb-1">
                                                                <label><span class="Curriculums">Curriculums</span></label>
                                                                <label class="float-right text-primary" id="lblCurriculumsNa">-</label>
                                                            </div>
                                                            <div class="pb-1">
                                                                <label><span class="SemesterName">Semester Name</span></label>
                                                                <label class="float-right text-primary" id="lblSemesterNa">-</label>
                                                            </div>
                                                            <div class="pb-1">
                                                                <label><span class="LearningModality">Learning Modality</span></label>
                                                                <label class="float-right text-primary" id="lblLearningModalityNa">-</label>
                                                            </div>
                                                            <div class="pb-1">
                                                                <label><span class="MentorName">Mentor Name</span></label>
                                                                <label class="float-right text-primary" id="lblMentorNa">-</label>
                                                            </div>
                                                        </div>
                                                        <%--<div class="col-xl-1 col-lg-1 col-sm-2 col-6">
                                            <div class="profile-image text-center mt-3 mt-lg-0">
                                                <img src="../image/nophoto.jpg" alt="image" tabindex="0" />
                                            </div>
                                        </div>--%>
                                                    </div>
                                                    <%--  <div class="text-end">
                                        <input type="button" class="btn btn-sm btn-outline-primary" tabindex="0" value="Enable" id="btnEnabledCourse" />
                                    </div>--%>
                                                    <div class="row mt-4">
                                                        <div class="col-12">
                                                            <div class="sub-heading">
                                                                <h5>Educational Details</h5>
                                                            </div>
                                                        </div>
                                                        <div class="col-12">
                                                            <table class="table table-striped table-bordered nowrap" id="BindDynamicEducationTable"></table>
                                                        </div>
                                                    </div>

                                                    <div class="text-center mt-5 mb-3" id="DivCourseButton">
                                                        <input type="button" class="btn btn-sm btn-outline-primary" tabindex="0" value="Submit" id="btnSubmitCourse" />
                                                        <%--<input type="button" class="btn btn-sm btn-outline-danger" tabindex="0" value="Cancel" id="btnClearCourse" />--%>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <!-- Photo Signature Tab -->
                                        <div class="tab-pane fade" id="tab_4">
                                            <div class="card mb-4">
                                                <!-- Page Main Heading -->
                                                <div class="card-header">
                                                    <div class="heading">
                                                        <asp:Label runat="server" ID="Label5">Photo Signature Details</asp:Label>
                                                    </div>
                                                </div>
                                                <div class="card-body">
                                                    <%--<div class="text-end">
                                        <input type="button" class="btn btn-sm btn-outline-primary" tabindex="0" value="Enable" id="btnEnabledPhoto" />
                                    </div>--%>
                                                    <div class="row">
                                                        <div class="col-xl-3 col-lg-3 col-sm-6 col-12">
                                                            <div class="form-group">
                                                                <div class="label-dynamic pb-2">
                                                                    <sup>* </sup>
                                                                    <label><span>Photo</span></label>
                                                                </div>
                                                                <div id="Div1" class="logoContainer" runat="server">
                                                                    <img src="../IMAGES/nophoto.jpg" alt="upload image" runat="server" id="imgUpFile" tabindex="0" class="w-100 h-100" />
                                                                </div>
                                                                <div class="fileContainer sprite pt-2">
                                                                    <span runat="server" id="ufFile" tabindex="0">Upload File</span>
                                                                    <asp:FileUpload ID="FUFile" runat="server" ToolTip="Select file to upload" TabIndex="0" CssClass="pt-2" />
                                                                    <input type="file" id="fuCollegeLogo" runat="server" class="form-control" accept=".png,.jpeg,.jpg" />
                                                                </div>
                                                                <input id="hdfname" type="hidden" value="Null" />
                                                                <input id="hdfStudentPhoto" type="hidden" value="Null" />
                                                            </div>
                                                        </div>
                                                        <div class="col-xl-3 col-lg-3 col-sm-6 col-12 offset-lg-1">
                                                            <div class="form-group">
                                                                <div class="label-dynamic pb-2">
                                                                    <sup>* </sup>
                                                                    <label><span>Signature</span></label>
                                                                </div>
                                                                <div id="Div2" class="logoContainers" runat="server">
                                                                    <img src="../IMAGES/nophoto.jpg" alt="upload image" runat="server" id="imgUpFiles" tabindex="0" class="w-100 h-50" />
                                                                </div>
                                                                <div class="fileContainers sprite pt-2">
                                                                    <span runat="server" id="ufFiles" tabindex="0">Upload File</span>
                                                                    <asp:FileUpload ID="FUFiles" runat="server" ToolTip="Select file to upload" TabIndex="0" CssClass="pt-2" />
                                                                    <input type="file" id="fuCollegeLogos" runat="server" class="form-control" accept=".png,.jpeg,.jpg" />
                                                                </div>
                                                                <input id="hdfSignature" type="hidden" value="Null" />
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="text-center mt-3 mb-3">
                                                        <input type="button" class="btn btn-sm btn-outline-primary" tabindex="0" value="Submit" id="btnSubmitPhoto" />
                                                        <%--<input type="button" class="btn btn-sm btn-outline-danger" tabindex="0" value="Cancel" id="btnClearPhoto" />--%>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <!-- Grades Tab -->
                                        <div class="tab-pane fade" id="tab_5">
                                            <div class="card mb-4">
                                                <!-- Page Main Heading -->
                                                <div class="card-header">
                                                    <div class="heading">
                                                        <asp:Label runat="server" ID="Label7">Grades</asp:Label>
                                                    </div>
                                                </div>
                                                <div class="card-body">
                                                    <div class="row">
                                                        <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                            <div class="form-group">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label><span class="AcademicSession">Academic Session</span></label>

                                                                </div>
                                                                <asp:DropDownList ID="ddlGradeSession" runat="server" CssClass="form-control ddl-all" data-select2-enable="true" TabIndex="0" AppendDataBoundItems="true" onchange="GradeSessionChange(this)">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-12 mt-4">
                                                        <div class="col-12  mt-4">
                                                            <div class="table-responsive">
                                                                <table class="accordion-table table table-striped table-bordered nowrap" id="tblStudentGradeList"></table>
                                                            </div>
                                                        </div>

                                                    </div>

                                                </div>
                                            </div>
                                        </div>

                                        <!-- Enlisted Subjects Tab -->
                                        <div class="tab-pane fade" id="tab_6">
                                            <div class="card mb-4">
                                                <!-- Page Main Heading -->
                                                <div class="card-header">
                                                    <div class="heading">
                                                        <asp:Label runat="server" ID="Label6">Enlisted Subjects</asp:Label>
                                                    </div>
                                                </div>
                                                <div class="card-body">
                                                    <div class="row">
                                                        <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                            <div class="form-group">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label><span class="AcademicSession">Academic Session</span></label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlAcademicSession" runat="server" CssClass="form-control ddl-all" data-select2-enable="true" TabIndex="0" AppendDataBoundItems="true" onchange="EnlistlistBind(this)">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-xl-4 col-lg-4 col-sm-6 col-12 d-none">
                                                            <div class="form-group">
                                                                <div class="label-dynamic">
                                                                    <sup></sup>
                                                                    <label><span class="Advisor">Advisor</span></label>
                                                                </div>
                                                                <asp:TextBox runat="server" ID="txtAdvisorName" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-12">
                                                        <table class="table table-striped table-bordered nowrap" id="BindDynamicEnlistmentSunject"></table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <!-- Class Schedule Tab -->
                                        <div class="tab-pane fade" id="tab_7">
                                            <div class="card mb-4">
                                                <!-- Page Main Heading -->
                                                <div class="card-header">
                                                    <div class="heading">
                                                        <asp:Label runat="server" ID="Label10">Class Schedule</asp:Label>
                                                    </div>
                                                </div>
                                                <div class="card-body">
                                                    <div class="col-12 mt-2">
                                                        <div class="table-responsive class-schedule">
                                                            <table class="table table-striped table-bordered nowrap w-100">
                                                                <thead>
                                                                    <tr>
                                                                        <th>Time Slot</th>
                                                                        <th>Monday</th>
                                                                        <th>Tuesday</th>
                                                                        <th>Wednesday </th>
                                                                        <th>Thursday</th>
                                                                        <th>Friday</th>
                                                                        <th>Saturday</th>
                                                                        <th>Sunday</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>08:00AM - 09:00AM </td>
                                                                        <td><span class="text-primary">DBMS404 - Database Management System</span><br />
                                                                            Room No. : <span class="text-success">101</span> </td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>09:00AM - 10:00AM </td>
                                                                        <td></td>
                                                                        <td><span class="text-primary">CP401 - Computer Programming </span>
                                                                            <br />
                                                                            Room No. : <span class="text-success">101 </span></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>10:00AM - 11:00AM </td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td><span class="text-primary">OM402 - Oracle Management</span>
                                                                            <br />
                                                                            Room No. : <span class="text-success">Online</span> </td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>11:00AM - 12:00PM </td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td><span class="text-primary">OM402 - Oracle Management</span>
                                                                            <br />
                                                                            Room No. : <span class="text-success">Online</span> </td>
                                                                        <td></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>01:00PM - 02:00PM </td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td><span class="text-primary">DBMS404 - Database Management System</span><br />
                                                                            Room No. : <span class="text-success">101</span> </td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>02:00PM - 03:00PM </td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td><span class="text-primary">CP401 - Computer Programming </span>
                                                                            <br />
                                                                            Room No. : <span class="text-success">101 </span></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>03:00PM - 04:00PM </td>
                                                                        <td><span class="text-primary">CP401 - Computer Programming </span>
                                                                            <br />
                                                                            Room No. : <span class="text-success">101 </span></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        <td><span class="text-primary">OM402 - Oracle Management</span>
                                                                            <br />
                                                                            Room No. : <span class="text-success">Online</span> </td>
                                                                        <td></td>
                                                                        <td></td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>

                                        <!-- Attendance Tab -->
                                        <div class="tab-pane fade" id="tab_8">
                                            <div class="card mb-4">
                                                <!-- Page Main Heading -->
                                                <div class="card-header">
                                                    <div class="heading">
                                                        <asp:Label runat="server" ID="Label8">Attendance</asp:Label>
                                                    </div>
                                                </div>
                                                <div class="card-body">
                                                    <div class="row">
                                                        <div class="col-xl-3 col-lg-4 col-sm-6 col-12">
                                                            <div class="form-group">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label><span class="AcademicSession">Academic Session</span></label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlSessionForAtt" runat="server" CssClass="form-control ddl-all" data-select2-enable="true" TabIndex="0" AppendDataBoundItems="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row mt-4">
                                                        <div class="col-12">
                                                            <div class="table-responsive">
                                                                <table class="accordion-table table table-striped table-bordered nowrap" id="BindDynamicStudAtt">
                                                                    <%--<thead>
                                                        <tr>
                                                            <th>Sr.No.</th>
                                                            <th>Subject Code</th>
                                                            <th>Subject Name</th>
                                                            <th>Subject Type</th>
                                                            <th>Total Classes</th>
                                                            <th>Total Present</th>
                                                            <th>Attendance (%)</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr>
                                                            <td>1</td>
                                                            <td><a href="#" data-bs-toggle="modal" data-bs-target="#AttendanceModal">CS101</a></td>
                                                            <td>Introduction to Computer Programming</td>
                                                            <td>Lecture</td>
                                                            <td>10</td>
                                                            <td>10</td>
                                                            <td>100.00</td>
                                                        </tr>
                                                        <tr>
                                                            <td>2</td>
                                                            <td><a href="#" data-bs-toggle="modal" data-bs-target="#AttendanceModal">CS102</a></td>
                                                            <td>Computer Science </td>
                                                            <td>Lecture</td>
                                                            <td>11</td>
                                                            <td>10</td>
                                                            <td>90.91</td>
                                                        </tr>
                                                    </tbody>--%>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <!-- Overall Result Tab -->
                                        <div class="tab-pane fade" id="tab_9">
                                            <div class="card mb-4">
                                                <!-- Page Main Heading -->
                                                <div class="card-header">
                                                    <div class="heading">
                                                        <asp:Label runat="server" ID="Label9">Overall Result</asp:Label>
                                                    </div>
                                                </div>
                                                <div class="card-body">
                                                    <div class="col-12  mt-4" id="OverallResultDiv">
                                                        <div class="table-responsive">
                                                            <table class="accordion-tables table table-striped table-bordered nowrap" id="OverallResultTable"></table>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Attendance Modal -->
    <div class="modal" id="AttendanceModal">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Attendance Details</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="col-12">
                        <table class="accordion-table table table-striped table-bordered nowrap" id="BindDynamicAttendanceDetailsTable">
                        </table>
                    </div>
                </div>
                <div class="modal-footer">
<%--                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>--%>
                </div>
            </div>
        </div>
    </div>


    <%--<script src="../plugins/custom/js/Academic/Student_Profile.js"></script>--%>
    <script src="<%=Page.ResolveClientUrl("~/plugins/custom/js/common.js")%>"></script>
    <script src="<%=Page.ResolveClientUrl("~/plugins/custom/js/validation.js")%>"></script>
    <script src="<%=Page.ResolveClientUrl("../plugins/custom/js/Academic/Student_Profile.js")%>"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#txtSearchStudent").autocomplete({

                source: function (request, response) {
                    var obj = {};
                    obj.textsearch = request.term;
                    var searchText = request.term;
                    var message = "Hello, Web Service!";

                    if (searchText.length >= 4) {
                        $.ajax({
                            type: "POST",
                            //url: "SearchForm.aspx/GetSuggestions",
                            //url: "/PresentationLayer/WEB_API/SearchName.asmx/GetSuggestions",
                            url: "../WEB API/SearchName.asmx/GetSuggestions",
                            //data: JSON.stringify(obj),
                            data: JSON.stringify(obj),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (data) {
                                response($.map(data.d, function (item) {
                                    return {

                                        label: item['STUDNAME'],
                                        val: item['IDNO']
                                    }
                                }))

                            },
                            error: function (xhr, status, error) {
                                console.log("Error:", error);
                            }
                        });
                    }
                },
                select: function (e, i) {

                    $("#<%=hdnClientId.ClientID %>").val(i.item.val);
                },

                minLength: 1

            });

        });
    </script>
</asp:Content>

