<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="StudentInfo.aspx.cs" Inherits="ACADEMIC_StudentInfo" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">
    <link href="../plugins/studInfo/css/admin-style.css" rel="stylesheet" />
    <link href="../plugins/studInfo/css/smart_wizard.css" rel="stylesheet" />
    <!--  Loader  -->
    <link href="../plugins/studInfo/css/loader.css" rel="stylesheet" />
    <!--  Calendar  -->
    <link href="../plugins/studInfo/plugin/calendar/bootstrap-datetimepicker.min.css" rel="stylesheet" />

    <!--    font -->
    <link href="../plugins/studInfo/css/font-awesome.min.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css?family=Lato" rel="stylesheet">
    <link href="https://fonts.googleapis.com/css?family=Questrial" rel="stylesheet">

    <style type="text/css">
        .content {
            padding-top: 0px;
            margin-top: 20px;
        }
        .nav-tabs.step-anchor {
            background: #fff;
        }
        .sw-theme-default > ul.step-anchor > li.active > a {
            background-color: transparent !important;
        }
        .chiller-theme .nav-tabs > li.active {
            background-color: #fff;
            color: #fff;
            border: 1px solid transparent!important;
        }
        #smartwizard .nav > li > a {
            padding: 5px 5px !important;
        }
        small {
            padding-left: 8px;
        }


        div.tab-co
        ntent + nav {
            display: none;
        }

        .checkbox input[type="checkbox"], .checkbox-inline input[type="checkbox"], .radio input[type="radio"], .radio-inline input[type="radio"] {
            margin-left: 5px;
        }
    </style>
    <style>
        .box-color-blank {
            border-color: red;
        }

        .sw-theme-default .sw-container {
            min-height: 250px !important;
        }
    </style>

    <%--<script src="../newbootstrap/js/jquery-3.3.1.min.js"></script>
    <script src="../newbootstrap/js/bootstrap.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.4.1.js"></script>--%>
    <script type="text/javascript">
        function showModal() {
            $("#myModal1").modal('show');
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            //$("body").addClass("nav-sm");
        })
    </script>
    <script>
        $(document).ready(function () {
            $(".aspNetDisabled").addClass("form-control");
        });
    </script>

    <script src="../validation/buttonValidation.js"></script>
    <script src="../validation/validation.js"></script>
    <!-- page content -->

    <div class="right_col" role="main">
        <div class="container-fluid">
            <!-- SmartWizard html -->
            <div class="padd-t-20">

                <!--          Wizzard Code          -->
                <div id="smartwizard">
                    <ul>
                        <li id="liStudAdm" runat="server"><a href="#step-1">
                            <div class="steps"><span class="tab-no">1</span></div>
                            <small>Student/Admission Details</small></a></li>
                        <li id="liAddr" runat="server"><a href="#step-2">
                            <div class="steps"><span class="tab-no">2</span></div>
                            <small>Address Details</small></a></li>
                        <li id="liLastExam" runat="server"><a href="#step-3">
                            <div class="steps"><span class="tab-no">3</span></div>
                            <small>Last Exam Details</small></a></li>
                        <li id="liUploads" runat="server"><a href="#step-4">
                            <div class="steps"><span class="tab-no">4</span></div>
                            <small>Uploads</small></a></li>
                        <li id="liEntrExam" runat="server"><a href="#step-5">
                            <div class="steps"><span class="tab-no">5</span></div>
                            <small>Entrance Exam</small></a></li>
                        <li id="liParentDet" runat="server">
                            <a id="anchorParentDetails" runat="server" href="#step-6">
                                <div class="steps">
                                    <span class="tab-no">
                                        <asp:Label ID="lblStepParentDetails" runat="server"></asp:Label>
                                    </span>
                                </div>
                                <small>Parent Details</small>
                            </a>
                        </li>
                        <li id="liRemark" runat="server"><a href="#step-7">
                            <div class="steps"><span class="tab-no">7</span></div>
                            <small>Remark</small></a>
                        </li>

                        <%-- <li id="liExtraDet" runat="server"><a href="#step-7">Step 7<br />
                            <small>Extra Details</small></a></li>
                        <li id="liFeeDet" runat="server"><a href="#step-8">Step 8<br />
                            <small>Fees Details</small></a></li>
                        <li id="liSportDet" runat="server"><a href="#step-9">Step 9<br />
                            <small>Sports Details</small></a></li>--%>
                    </ul>

                    <div>
                        <!-- DONT DELETE THIS EXTRA DIV-->

                        <div id="step-1" class="">
                            <div class="container-fluid">
                                <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                                    <ContentTemplate>

                                        <div class="col-sm-4 col-sm-offset-4 form-group pt-5" runat="server" id="divSearch">
                                            <div class="input-group">
                                                <asp:TextBox ID="txtidno" runat="server" class="form-control" Enabled="false" placeholder="Search"></asp:TextBox>
                                                <span class="input-group-addon" data-target="#myModal2" data-toggle="modal">
                                                    <i class="glyphicon glyphicon-search"></i></span>
                                            </div>
                                        </div>

                                        <div class="clearfix"></div>

                                        <h4 class="sub-heading">Admission Details</h4>
                                        <div class="clearfix"></div>
                                        <div class="row padd-t-10">
                                            <div class="col-md-3 col-sm-4 col-xs-12 form-group">
                                                <label><sup>*</sup> Admission Quota :</label>
                                                <%--semester changed as admtype--%>
                                                <asp:DropDownList runat="server" ID="ddlSemester" AppendDataBoundItems="true" Enabled="false" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" AutoPostBack="true" class="form-control">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="col-md-3 col-sm-4 col-xs-12 form-group">
                                                <label><sup>*</sup> College/School Name :</label>
                                                <asp:DropDownList runat="server" ID="ddlCollege" Enabled="false" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" AutoPostBack="true" class="form-control">
                                                    <asp:ListItem Value="0">--Please Select--</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-3 col-sm-4 col-xs-12 form-group">
                                                <label><sup>*</sup> Degree :</label>
                                                <asp:DropDownList runat="server" ID="ddlDegree" Enabled="false" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" AutoPostBack="true" class="form-control">
                                                    <asp:ListItem Value="0">--Please Select--</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-3 col-sm-4 col-xs-12 form-group">
                                                <label><sup>*</sup> Branch :</label>
                                                <asp:DropDownList runat="server" ID="ddlBranch" Enabled="false" AppendDataBoundItems="true" class="form-control">
                                                    <asp:ListItem Value="0">--Please Select--</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-3 col-sm-4 col-xs-12 form-group">
                                                <label><sup>*</sup> Admission Type :</label>
                                                <asp:DropDownList runat="server" ID="ddlAdmissionType" Enabled="true" AppendDataBoundItems="true" class="form-control">
                                                    <asp:ListItem Value="0">--Please Select--</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="col-md-3 col-sm-4 col-xs-12 form-group">
                                                <label><sup>*</sup> Admission Batch :</label>
                                                <asp:DropDownList runat="server" ID="ddlAcademicYear" AppendDataBoundItems="true" Enabled="false" class="form-control">
                                                    <asp:ListItem Value="0">--Please Select--</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="col-md-3 col-sm-4 col-xs-12 form-group">
                                                <label><sup>*</sup> Semester/ Year :</label>
                                                <asp:DropDownList runat="server" ID="ddlAdmSemester" Enabled="false" AppendDataBoundItems="true" class="form-control">
                                                    <asp:ListItem Value="0">--Please Select--</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                        </div>

                                        <h4 class="sub-heading">Student Details</h4>
                                        <div class="row padd-t-10" style="margin-right: -10px">
                                            <div class="col-md-3 col-sm-6 col-xs-12 form-group">
                                                <label><sup>*</sup> Student Name (as per X Std. Mark Sheet) :</label>
                                                <asp:TextBox runat="server" ID="txtStudName" class="form-control onlycharacter" placeholder="Enter Student Name" MaxLength="30" onkeypress="return AlphaNumeric(this);" onkeyup="return AlphaNumeric(this);"></asp:TextBox>
                                            </div>

                                            <div class="col-md-3 col-sm-6 col-xs-12 form-group">
                                                <%--    <br />--%>
                                                <label><sup></sup>Student First Name :</label>
                                                <asp:TextBox runat="server" ID="txtFirstname" class="form-control onlycharacter" placeholder="Enter Student First Name" MaxLength="30" onkeypress="return AlphaNumeric(this);" onkeyup="return AlphaNumeric(this);"></asp:TextBox>
                                            </div>

                                            <div class="col-md-3 col-sm-6 col-xs-12 form-group">
                                                <%--          <br />--%>
                                                <label><sup></sup>Student Middle Name :</label>
                                                <asp:TextBox runat="server" ID="txtMiddlename" class="form-control onlycharacter" placeholder="Enter Student Middle Name" MaxLength="30" onkeypress="return AlphaNumeric(this);" onkeyup="return AlphaNumeric(this);"></asp:TextBox>
                                            </div>

                                            <div class="col-md-3 col-sm-6 col-xs-12 form-group">
                                                <%--                  <br />--%>
                                                <label><sup></sup>Student Last Name/Surname :</label>
                                                <asp:TextBox runat="server" ID="txtSurname" class="form-control onlycharacter" placeholder="Enter Last Name/Surname" MaxLength="30" onkeypress="return AlphaNumeric(this);" onkeyup="return AlphaNumeric(this);"></asp:TextBox>
                                            </div>

                                            <div class="col-md-3 col-sm-6 col-xs-12 form-group clearfix">
                                                <%--  <br />--%>
                                                <label><sup>*</sup> Date of Birth :</label>
                                                <div class='input-group date' id='myDatepicker2'>
                                                    <asp:TextBox runat="server" ID="txtDob" class="form-control" data-inputmask="'mask': '99/99/9999'"></asp:TextBox>
                                                    <span class="input-group-addon">
                                                        <span class="glyphicon glyphicon-calendar"></span>
                                                    </span>
                                                </div>
                                            </div>

                                            <div class="col-md-3 col-sm-4 col-xs-12 form-group">
                                                <%-- <br />--%>
                                                <label>Birth Place :</label>
                                                <asp:TextBox runat="server" ID="txtNativePlace" class="form-control" placeholder="Enter birth place" MaxLength="30"></asp:TextBox>
                                            </div>

                                            <div class="col-md-3 col-sm-6 col-xs-12 form-group">
                                                <%-- <br />--%>
                                                <label><sup>*</sup> Gender :</label><br />
                                                <asp:RadioButtonList runat="server" ID="rdoGender" RepeatDirection="Horizontal" RepeatColumns="3">
                                                    <asp:ListItem Value="1"> Male</asp:ListItem>
                                                    <asp:ListItem Value="2">Female</asp:ListItem>
                                                    <asp:ListItem Value="3">Transgender</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="clearfix"></div>
                                            <div class="col-md-3 col-sm-6 col-xs-12 form-group">
                                                <label><sup>*</sup>Aadhar Card Number :</label>
                                                <asp:TextBox runat="server" ID="txtAdhaarNo" class="form-control" onkeypress="return OnlyNumeric();" placeholder="Enter Aadhar Card Number" MaxLength="12"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-6 col-xs-12 form-group">
                                                <label><sup>*</sup> Blood Group :</label>
                                                <asp:DropDownList runat="server" ID="ddlBloodGroup" AppendDataBoundItems="true" class="form-control">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="col-md-3 col-sm-6 col-xs-12 form-group">
                                                <label>Willing to Donate Blood :</label>
                                                <br />
                                                <asp:RadioButtonList runat="server" ID="rdoIsBloodDonate" RepeatDirection="Horizontal">
                                                    <asp:ListItem Selected="False" Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="2">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>

                                            <div class="col-md-3 col-sm-6 col-xs-12 form-group">
                                                <label><sup>*</sup> Are You Physically Disabled :</label>
                                                <br />
                                                <asp:RadioButtonList runat="server" ID="rdoPH" OnSelectedIndexChanged="rdoPH_SelectedIndexChanged" AutoPostBack="true" RepeatDirection="Horizontal">
                                                    <asp:ListItem Selected="False" Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="2">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="clearfix"></div>
                                            <div class="col-md-3 col-sm-6 col-xs-12 form-group">
                                                <label>Type of Handicap :</label>
                                                <asp:DropDownList runat="server" ID="ddlTypeOfPH" AppendDataBoundItems="true" Enabled="false" class="form-control">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-6 col-sm-6 col-xs-12 form-group">
                                                <label>Student Allergy details / Medical History :</label>
                                                &nbsp;&nbsp;<label>Characters Limit : &nbsp</label>
                                                <label id="lblcount" style="background-color: #E2EEF1; color: Red; font-weight: bold;">200</label>
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtAlergyDetail" Rows="3" Columns="80" placeholder="Enter Allergy details" TextMode="MultiLine" data-toggle="tooltip" data-placement="top"
                                                    oncopy="return false;" onpaste="return false;" oncut="return false;" onkeyup="LimtCharactersAllergy(this,200,'lblcount')"></asp:TextBox>
                                            </div>
                                            <%--Added by Abhinay Lad [16-09-2019]--%>
                                            <div class="col-md-3 col-sm-6 col-xs-12 form-group" style="margin-left: -16px;">
                                                <label><sup>*</sup> Marital Status :</label>
                                                <br />
                                                <asp:RadioButtonList runat="server" ID="rbtn_MaritalStatus" OnSelectedIndexChanged="rbtn_MaritalStatus_SelectedIndexChanged" AutoPostBack="true" RepeatDirection="Horizontal">
                                                    <asp:ListItem Selected="False" Value="1">Married</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="2">Unmarried</asp:ListItem>
                                                    <asp:ListItem Selected="False" Value="3">Divorced</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>

                                            <div class="col-md-6 col-sm-6 col-xs-12 form-group">
                                                <%--         <label>Student Identification Mark 1 :</label>--%>
                                                <label><sup>*</sup> Student Identification Mark 1 :</label>
                                                &nbsp;&nbsp;<label>Characters Limit : &nbsp</label>
                                                <label id="lblcount2" style="background-color: #E2EEF1; color: Red; font-weight: bold;">100</label>
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtIdentityMark" Rows="2" Columns="80" placeholder="Enter Student Identification Mark 1" TextMode="MultiLine" data-toggle="tooltip" data-placement="top"
                                                    oncopy="return false;" onpaste="return false;" oncut="return false;" onkeyup="LimtCharactersAllergy(this,100,'lblcount2')"></asp:TextBox>
                                            </div>

                                            <div class="col-md-6 col-sm-6 col-xs-12 form-group">
                                                <label>Student Identification Mark 2 :</label>
                                                &nbsp;&nbsp;<label>Characters Limit : &nbsp</label>
                                                <label id="Label2" style="background-color: #E2EEF1; color: Red; font-weight: bold;">100</label>
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtIdentityMark2" Rows="2" Columns="80" placeholder="Enter Student Identification Mark 2" TextMode="MultiLine" data-toggle="tooltip" data-placement="top"
                                                    oncopy="return false;" onpaste="return false;" oncut="return false;" onkeyup="LimtCharactersAllergy(this,100,'lblcount2')"></asp:TextBox>
                                            </div>

                                        </div>

                                        <h4 class="sub-heading">Additional Information</h4>
                                        <div class="row padd-t-10">
                                            <div class="col-md-3 col-sm-4 col-xs-12 form-group">
                                                <label><sup>*</sup> Mother Tongue :</label>
                                                <asp:DropDownList runat="server" ID="ddlMtounge" AppendDataBoundItems="true" class="form-control">
                                                    <asp:ListItem Value="0">--Please Select--</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-3 col-sm-4 col-xs-12 form-group">
                                                <label><sup>*</sup> Nationality :</label>
                                                <%--citizenship changed to Nationality--%>
                                                <asp:DropDownList runat="server" ID="ddlCitizenship" AppendDataBoundItems="true" class="form-control">
                                                    <asp:ListItem Value="0">--Please Select--</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-3 col-sm-4 col-xs-12 form-group">
                                                <label><sup>*</sup> Religion :</label>
                                                <asp:DropDownList runat="server" ID="ddlReligion" AppendDataBoundItems="true" class="form-control">
                                                    <asp:ListItem Value="0">--Please Select--</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-3 col-sm-4 col-xs-12 form-group">
                                                <label><sup>*</sup> Community :</label>
                                                <asp:DropDownList runat="server" ID="ddlCommunity" AppendDataBoundItems="true" class="form-control">
                                                    <asp:ListItem Value="0">--Please Select--</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-3 col-sm-4 col-xs-12 form-group">
                                                <label><sup>*</sup> Caste Name :</label>
                                                <asp:DropDownList runat="server" ID="ddlCasteName" AppendDataBoundItems="true" class="form-control">
                                                    <asp:ListItem Value="0">--Please Select--</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-3 col-sm-4 col-xs-12 form-group">
                                                <label><sup></sup>Sub Caste :</label>
                                                <asp:TextBox runat="server" ID="txtSubcaste" class="form-control" placeholder="Enter Sub Caste" MaxLength="20"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-4 col-xs-12 form-group">
                                                <label><sup></sup>Caste Code :</label>
                                                <asp:TextBox runat="server" ID="txtCommunityCode" class="form-control" placeholder="Enter Caste Code" MaxLength="10"></asp:TextBox>
                                            </div>

                                            <div class="col-md-3 col-sm-4 col-xs-12 form-group">
                                                <label><sup>*</sup> Do You Need Hostel ?</label>
                                                <br />
                                                <asp:RadioButtonList runat="server" ID="RdoHosteller" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="RdoHosteller_SelectedIndexChanged">
                                                    <asp:ListItem Selected="False" Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="2">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                                <br />
                                            </div>
                                            <div class="col-md-3 col-sm-4 col-xs-12 form-group">
                                                <label><sup></sup>College/School to Home Distance (Km):</label>
                                                <asp:TextBox runat="server" ID="txtCollegeDistance" class="form-control" Enabled="false" placeholder="Enter Distance in Km" MaxLength="5"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-4 col-xs-12 form-group">
                                                <label><sup>*</sup> Known Languages (Enter with comma separated if multiple) :</label>
                                                <asp:TextBox runat="server" ID="txtKnownLanguage" class="form-control" placeholder="Enter Language" MaxLength="100"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-4 col-xs-12 form-group">
                                                <label>Known Foreign Languages (Enter with comma separated if multiple) :</label>
                                                <asp:TextBox runat="server" ID="txtKnownForeignLang" class="form-control" placeholder="Enter Language" MaxLength="100"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-4 col-xs-12 form-group">
                                                <label><sup>*</sup> Do you Need Transport ?</label><br />
                                                <asp:RadioButtonList runat="server" ID="rdotransPort" RepeatDirection="Horizontal">
                                                    <asp:ListItem Selected="False" Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="2">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>


                                        </div>


                                        <div class="text-center padd-tb-10">
                                            <%--<button type="button" class="btn btn-default">Previous</button>--%>

                                            <asp:Label runat="server" ID="lblApprovedmsg" Font-Bold="true" Text="Note : Your Application has been approved, you can't modify details please click link below to print application form" Style="color: red" Visible="false"></asp:Label>
                                            <br />
                                            <asp:Button runat="server" ID="btnSaveNext" class="btn btn-outline-info" Text="Save and Next" OnClick="btnSaveNext_Click" OnClientClick="return step1Click_validation()" />
                                            <%-- <button type="button" class="btn btn-danger">Cancel</button>--%>
                                            <asp:HiddenField runat="server" Value="0" ID="hdndegree" />
                                            <br />
                                            <div id="tab1Print" runat="server" visible="false" style="font-size: large;">
                                                <a href="PrintApplication.aspx">Print Application Form</a>
                                            </div>
                                        </div>
                                    </ContentTemplate>

                                </asp:UpdatePanel>
                            </div>
                        </div>

                        <div id="step-2" class="">
                            <div class="container-fluid">
                                <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="col-sm-12 col-xs-12">
                                                <h4 class="sub-heading mt-2">Temporary Address</h4>
                                                <div class="row padd-t-10">
                                                    <div class="col-sm-12 col-xs-12 form-group">
                                                        <label><sup>*</sup> Enter Address:</label>
                                                        &nbsp;&nbsp;<label>Characters Limit : &nbsp</label>
                                                        <label id="lblcountLAdd" style="background-color: #E2EEF1; color: Red; font-weight: bold;">200</label>
                                                        <asp:TextBox runat="server" ID="txtLAddressLine1" class="form-control" TextMode="MultiLine" placeholder="Enter Address" data-toggle="tooltip" data-placement="top" onkeyup="LimtCharactersLAdd(this,200,'lblcountLAdd')"></asp:TextBox>
                                                    </div>

                                                    <div class="clearfix"></div>

                                                    <div class="col-md-3 col-sm-4 col-xs-12 form-group">
                                                        <label><sup>*</sup> City/Town :</label>
                                                        &nbsp;&nbsp;
                                                        <asp:LinkButton ID="lbnLOtherCity" runat="server" ForeColor="Green" OnClick="lbnLOtherCity_Click"> Add Other City</asp:LinkButton></label>
                                                        <asp:DropDownList runat="server" ID="ddlLcity" AppendDataBoundItems="true" class="form-control" OnSelectedIndexChanged="ddlLcity_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:TextBox ID="txtLOtherCity" runat="server" Visible="false" placeholder="Enter City" CssClass="form-control"></asp:TextBox>

                                                    </div>
                                                    <div class="col-md-3 col-sm-4 col-xs-12 form-group">
                                                        <label><sup>*</sup> State :</label>
                                                        <asp:DropDownList runat="server" ID="ddlLState" AppendDataBoundItems="true" class="form-control">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-3 col-sm-4 col-xs-12 form-group">
                                                        <label><sup>*</sup> Pin Code :</label>

                                                        <asp:TextBox runat="server" ID="txtLPin" class="form-control" placeholder="Enter Pin Code" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3 col-sm-4 col-xs-12 form-group">
                                                        <label>STD Code :</label>

                                                        <asp:TextBox runat="server" ID="txtLSTDCode" class="form-control" placeholder="Enter STD Code" MaxLength="12"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3 col-sm-4 col-xs-12 form-group">
                                                        <label>Landline Number :</label>
                                                        <asp:TextBox runat="server" ID="txtLLandLineNo" class="form-control" placeholder="Enter Landline Number" MaxLength="12"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3 col-sm-4 col-xs-12 form-group">
                                                        <label><sup>*</sup> Student Email Id :</label>
                                                        <asp:TextBox runat="server" ID="txtLEmail" class="form-control" placeholder="Enter Email" MaxLength="50"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3 col-sm-4 col-xs-12 form-group">
                                                        <label><sup>*</sup> Student Mobile Number :</label>
                                                        <asp:TextBox runat="server" ID="txtLStudMobile" class="form-control" placeholder="Enter Mobile Number" MaxLength="10"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3 col-sm-4 col-xs-12 form-group">
                                                        <label><sup>*</sup> SMS to be Sent :</label>
                                                        <asp:DropDownList runat="server" ID="ddlSMSSend" AppendDataBoundItems="true" class="form-control">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            <asp:ListItem Value="1">Father</asp:ListItem>
                                                            <asp:ListItem Value="2">Mother</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>

                                                    <div class="col-md-3 col-sm-4 col-xs-12 form-group">
                                                        <label>Emergency Contact Number :</label>
                                                        <asp:TextBox runat="server" ID="txtEmergencyContactNum" class="form-control" onkeypress="return OnlyNumeric();" TextMode="Phone" placeholder="Enter Emergency Contact Number" MaxLength="10"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3 col-sm-4 col-xs-12 form-group">
                                                        <label>Emergency Contact Name :</label>
                                                        <asp:TextBox runat="server" ID="txtEmergencyContactName" TextMode="SingleLine" class="form-control onlycharacter" placeholder="Enter Emergency Contact Name" MaxLength="50"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3 col-sm-4 col-xs-12 form-group">
                                                        <label>Emergency Contact Relation :</label>
                                                        <asp:TextBox runat="server" ID="txtEmergencyContactRelation" TextMode="SingleLine" class="form-control" placeholder="Enter Emergency Contact Realtion" MaxLength="40"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3 col-sm-4 col-xs-12 form-group">
                                                        <label>Emergency Contact Email :</label>
                                                        <asp:TextBox runat="server" ID="txtEmergencyContactEmail" class="form-control" TextMode="Email" placeholder="Enter Emergency Contact Email" MaxLength="50"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 col-xs-12">
                                                <hr>
                                                <div class="row">
                                                    <div class="col-md-6 col-sm-6">
                                                        <div class="checkbox form-group">
                                                            <asp:CheckBox ID="chkCommToPermanent" runat="server" Text="Same as Temporary Address ?" OnCheckedChanged="chkCommToPermanent_CheckedChanged" AutoPostBack="true" />
                                                            <%--<input type="checkbox" id="x1" onclick="copyLocalAddr(this)">--%>
                                                            <%--<label for="x1">Same as Communication Address ?</label>--%>
                                                        </div>
                                                    </div>
                                                </div>

                                                <h4 class="sub-heading">Permanent Address</h4>
                                                <div class="row padd-t-10">
                                                    <div class="col-sm-12 col-xs-12 form-group">
                                                        <label>Enter Address:</label>
                                                        &nbsp;&nbsp;<label>Characters Limit : &nbsp</label>
                                                        <label id="lblcountPAdd" style="background-color: #E2EEF1; color: Red; font-weight: bold;">200</label>
                                                        <asp:TextBox runat="server" ID="txtPAddressLine1" class="form-control" TextMode="MultiLine" placeholder="Enter Address" data-toggle="tooltip" data-placement="top" onkeyup="LimtCharactersPAdd(this,200,'lblcountPAdd')"></asp:TextBox>
                                                    </div>


                                                    <div class="clearfix"></div>
                                                    <div class="col-md-3 col-sm-4 col-xs-12 form-group">
                                                        <label>City/Town :</label>&nbsp;&nbsp;
                                                        <asp:LinkButton ID="lbtnPOtherCity" runat="server" ForeColor="Green" OnClick="lbtnPOtherCity_Click"> Add Other City</asp:LinkButton></label>
                                                        <asp:DropDownList runat="server" ID="ddlPCity" class="form-control" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlPCity_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:TextBox ID="txtPOthercity" runat="server" Visible="false" placeholder="Enter City" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3 col-sm-4 col-xs-12 form-group">
                                                        <label>State :</label>

                                                        <asp:DropDownList runat="server" ID="ddlPState" class="form-control" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-3 col-sm-4 col-xs-12 form-group">
                                                        <label>Pin Code :</label>

                                                        <asp:TextBox runat="server" ID="txtPPin" class="form-control" placeholder="Enter Pin Code" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3 col-sm-4 col-xs-12 form-group">
                                                        <label>STD Code :</label>

                                                        <asp:TextBox runat="server" ID="txtPSTDCode" class="form-control" placeholder="Enter STD Code" MaxLength="12"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3 col-sm-4 col-xs-12 form-group">
                                                        <label>Landline Number :</label>

                                                        <asp:TextBox runat="server" ID="txtPLandLine" class="form-control" placeholder="Enter Landline Number" MaxLength="12"></asp:TextBox>
                                                    </div>


                                                </div>
                                            </div>
                                        </div>

                                        <hr>
                                        <div class="row">
                                            <div class="col-md-6 col-sm-6">
                                                <div class="checkbox form-group">
                                                    <asp:CheckBox ID="chkCommToGuardian" runat="server" AutoPostBack="true" Text="Same as Temporary Address ?" OnCheckedChanged="chkCommToGuardian_CheckedChanged" />
                                                    <%--<input type="checkbox" id="Checkbox2" onclick="copyLocalAddrtoG(this)">
                                                    <label for="Checkbox2">Same as Communication Address ?</label>--%>
                                                </div>
                                            </div>
                                        </div>
                                        <h4 class="sub-heading">Guardian Address</h4>
                                        <div class="row padd-t-10">
                                            <div class="col-sm-12 col-xs-12 form-group">
                                                <label><sup></sup>Enter Address:</label>
                                                &nbsp;&nbsp;<label>Characters Limit : &nbsp</label>
                                                <label id="lblcountGAdd" style="background-color: #E2EEF1; color: Red; font-weight: bold;">200</label>
                                                <asp:TextBox runat="server" ID="txtGaddressLine1" class="form-control" TextMode="MultiLine" placeholder="Enter Address" data-toggle="tooltip" data-placement="top" onkeyup="LimtCharactersGAdd(this,200,'lblcountGAdd')"></asp:TextBox>
                                            </div>

                                            <div class="clearfix"></div>

                                            <div class="col-md-3 col-xs-12 col-sm-6 form-group">
                                                <label><sup></sup>City/Town :</label>&nbsp;&nbsp;
                                                <asp:LinkButton ID="lnkbtnGothercity" runat="server" ForeColor="Green" OnClick="lnkbtnGothercity_Click"> Add Other City</asp:LinkButton></label>
                                                <asp:DropDownList runat="server" ID="ddlGCity" AppendDataBoundItems="true" class="form-control" OnSelectedIndexChanged="ddlGCity_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtGOthercity" runat="server" Visible="false" placeholder="Enter City" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-6 col-xs-12 form-group">
                                                <label><sup></sup>State :</label>
                                                <asp:DropDownList runat="server" ID="ddlGState" AppendDataBoundItems="true" class="form-control">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-3 col-sm-6 col-xs-12 form-group">
                                                <label><sup></sup>Pin Code :</label>
                                                <asp:TextBox runat="server" ID="txtGPin" class="form-control" placeholder="Enter Pin Code" MaxLength="6"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-6 col-xs-12 form-group">
                                                <label>STD Code :</label>
                                                <asp:TextBox runat="server" ID="txtGSTDCode" class="form-control" placeholder="Enter STD Code" MaxLength="12"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-6 col-xs-12 form-group">
                                                <label>Landline Number :</label>
                                                <asp:TextBox runat="server" ID="txtGLandLineNumber" class="form-control" placeholder="Enter Landline Number" MaxLength="12"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-6 col-xs-12 form-group">
                                                <label><sup></sup>Relation with Guardian :</label>
                                                <asp:TextBox runat="server" ID="txtGRelation" class="form-control" placeholder="Enter Relation with Guardian" MaxLength="50"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-6 col-xs-12 form-group">
                                                <label><sup></sup>Guardian Email Id :</label>
                                                <asp:TextBox runat="server" ID="txtGEmailId" class="form-control" placeholder="Enter Email" MaxLength="50"></asp:TextBox>
                                            </div>
                                            <div class=" col-md-3 col-sm-6 col-xs-12 form-group">
                                                <label><sup></sup>Guardian Mobile Number :</label>
                                                <asp:TextBox runat="server" ID="txtGMobile" class="form-control" placeholder="Enter Mobile Number" MaxLength="10"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="text-center padd-tb-10">
                                            <button type="button" class="btn btn-default" onclick="parent.location='#step-1'">Previous</button>

                                            <asp:Button runat="server" ID="btnSaveNext2" class="btn btn-outline-info" Text="Save and Next" OnClick="btnSaveNext2_Click" OnClientClick="return step2Click_validation()" />
                                            <%-- <button type="button" class="btn btn-danger">Cancel</button>--%>
                                            <div id="tab2Print" runat="server" visible="false" style="font-size: large;">
                                                <a href="PrintApplication.aspx">Print Application Form</a>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>
                        </div>

                        <div id="step-3" class="">
                            <div class="container-fluid">
                                <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                                    <ContentTemplate>

                                        <div class="row">
                                            <div class="col-md-12 col-sm-12">
                                                <br />
                                                <h4 class="padd-tb-10 sub-heading">SSC Marks</h4>
                                                <div class="row">

                                                    <div class="col-md-3 col-sm-4 col-xs-12 form-group">
                                                        <label><sup></sup>Transfer Certificate Number :</label>
                                                        <asp:TextBox runat="server" ID="txtSSCTransferCertNo" class="form-control" placeholder="Enter Transfer Certificate Number" MaxLength="15"></asp:TextBox>
                                                    </div>

                                                    <div class="col-md-3 col-sm-4 col-xs-12 form-group">
                                                        <label><sup>*</sup> Select Board Category for SSC :</label>
                                                        <asp:DropDownList runat="server" ID="ddlSSCBoardCategory" AppendDataBoundItems="true" class="form-control">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <%--Added by NARESH BEERLA [29092020]--%>
                                                    <div class="col-md-3 col-sm-4 col-xs-12 form-group">
                                                        <label><sup></sup>Other Board SSC :</label>
                                                        <asp:TextBox runat="server" ID="txtOtherBoardSSC" class="form-control enableother" MaxLength="20"></asp:TextBox>
                                                    </div>
                                                    <%--Added by NARESH BEERLA [29092020]--%>

                                                    <%--Added by NARESH BEERLA [29092020]--%>
                                                    <div class="col-md-3 col-sm-4 col-xs-12 form-group">
                                                        <label><sup></sup></label>
                                                        <asp:RadioButtonList CssClass="vertical-radio-btn" ID="rblInputSystem" runat="server" RepeatDirection="Horizontal" ToolTip="Select any one">
                                                            <asp:ListItem Value="1" Selected="True">Mark System</asp:ListItem>
                                                            <asp:ListItem Value="2">Grade System</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                        <br />
                                                    </div>
                                                    <%--Added by NARESH BEERLA [29092020]--%>
                                                </div>
                                            </div>
                                        </div>

                                        <h4 class="padd-tb-10">Language</h4>
                                        <div class="row">
                                            <div class="col-md-6 col-xs-12">
                                                <div class="row">
                                                    <div class="col-md-4 col-sm-4 col-xs-12 form-group">
                                                        <label><sup>*</sup> First Language :</label>
                                                        <asp:DropDownList runat="server" ID="ddlSSCLanguage" AppendDataBoundItems="true" class="form-control" OnSelectedIndexChanged="ddlSSCLanguage_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label><sup></sup>Mark Obtained :</label>
                                                        <asp:TextBox runat="server" ID="txtSSCLangObtMark" class="form-control obtainedssc disabledWhenCBSESSC" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label><sup></sup>Max Mark :</label>
                                                        <asp:TextBox runat="server" ID="txtSSCLangMaxMark" class="form-control maxssc disabledWhenCBSESSC" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6 col-xs-12">
                                                <div class="row">
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label><sup></sup>&#37; of Mark :</label>
                                                        <asp:TextBox runat="server" ID="txtSSCLangPer" class="form-control clearSSCPercent" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label>Grade Point :</label>
                                                        <asp:TextBox runat="server" ID="txtSSCLangGdPoint" class="form-control disabledSSCGrades" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label>Grade :</label>
                                                        <asp:TextBox runat="server" ID="txtSSCLangGrade" class="form-control disabledSSCGrades upper" MaxLength="4"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-6 col-xs-12">
                                                <div class="row">
                                                    <div class="col-md-4 col-sm-4 col-xs-12 form-group">
                                                        <label><sup></sup>Second Language :</label>
                                                        <asp:DropDownList runat="server" ID="ddlSSCLanguage2" AppendDataBoundItems="true" class="form-control">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label><sup></sup>Mark Obtained :</label>
                                                        <asp:TextBox runat="server" ID="txtSSCLang2ObtMark" class="form-control obtainedssc disabledWhenCBSESSC" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label><sup></sup>Max Mark :</label>
                                                        <asp:TextBox runat="server" ID="txtSSCLang2MaxMark" class="form-control maxssc disabledWhenCBSESSC" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6 col-xs-12">
                                                <div class="row">
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label><sup></sup>&#37; of Mark :</label>
                                                        <asp:TextBox runat="server" ID="txtSSCLang2Per" class="form-control clearSSCPercent" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label>Grade Point :</label>
                                                        <asp:TextBox runat="server" ID="txtSSCLang2GdPoint" class="form-control disabledSSCGrades" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label>Grade :</label>
                                                        <asp:TextBox runat="server" ID="txtSSCLang2Grade" class="form-control disabledSSCGrades upper" MaxLength="4"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>

                                        <h4 class="padd-tb-10">English</h4>
                                        <div class="row">
                                            <div class="col-md-6 col-xs-12">

                                                <div class="row">
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label><sup></sup>Mark Obtained :</label>
                                                        <asp:TextBox runat="server" ID="txtSSCEnglishObtMark" class="form-control obtainedssc disabledWhenCBSESSC" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label><sup></sup>Max Mark :</label>
                                                        <asp:TextBox runat="server" ID="txtSSCEnglishMaxMark" class="form-control maxssc disabledWhenCBSESSC" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label><sup></sup>&#37; of Mark :</label>
                                                        <asp:TextBox runat="server" ID="txtSSCEnglishPer" class="form-control clearSSCPercent" MaxLength="6"></asp:TextBox>
                                                    </div>

                                                </div>
                                            </div>
                                            <div class="col-md-6 col-xs-12">
                                                <div class="row">
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label>Grade Point :</label>
                                                        <asp:TextBox runat="server" ID="txtSSCEngGdpoint" class="form-control disabledSSCGrades" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label>Grade :</label>
                                                        <asp:TextBox runat="server" ID="txtSSCEngGrade" class="form-control disabledSSCGrades upper" MaxLength="4"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <h4 class="padd-tb-10">Maths</h4>
                                        <div class="row">
                                            <div class="col-md-6 col-xs-12">

                                                <div class="row">
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label><sup></sup>Mark Obtained :</label>
                                                        <asp:TextBox runat="server" ID="txtSSCMathsObtMark" class="form-control obtainedssc disabledWhenCBSESSC" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label><sup></sup>Max Mark :</label>
                                                        <asp:TextBox runat="server" ID="txtSSCMathsMaxMark" class="form-control maxssc disabledWhenCBSESSC" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label><sup></sup>&#37; of Mark :</label>
                                                        <asp:TextBox runat="server" ID="txtSSCMathsPer" class="form-control clearSSCPercent" MaxLength="6"></asp:TextBox>
                                                    </div>

                                                </div>
                                            </div>
                                            <div class="col-md-6 col-xs-12">
                                                <div class="row">
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label>Grade Point :</label>
                                                        <asp:TextBox runat="server" ID="txtSSCMathGdPoint" class="form-control disabledSSCGrades" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label>Grade :</label>
                                                        <asp:TextBox runat="server" ID="txtSSCMathGrade" class="form-control disabledSSCGrades upper" MaxLength="4"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <h4 class="padd-tb-10">Science</h4>
                                        <div class="row">
                                            <div class="col-md-6 col-xs-12">
                                                <div class="row">
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label><sup></sup>Mark Obtained :</label>
                                                        <asp:TextBox runat="server" ID="txtSSCScienceObtMark" class="form-control obtainedssc disabledWhenCBSESSC" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label><sup></sup>Max Mark :</label>
                                                        <asp:TextBox runat="server" ID="txtSSCScienceMaxMark" class="form-control maxssc disabledWhenCBSESSC" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label><sup></sup>&#37; of Mark :</label>
                                                        <asp:TextBox runat="server" ID="txtSSCSciencePer" class="form-control clearSSCPercent" MaxLength="6"></asp:TextBox>
                                                    </div>

                                                </div>
                                            </div>
                                            <div class="col-md-6 col-xs-12">
                                                <div class="row">
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label>Grade Point :</label>
                                                        <asp:TextBox runat="server" ID="txtSSCSciGdPoint" class="form-control disabledSSCGrades" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label>Grade :</label>
                                                        <asp:TextBox runat="server" ID="txtSSCSciGrade" class="form-control disabledSSCGrades upper" MaxLength="4"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <%--<h4 class="padd-tb-10">Social Science</h4>--%>
                                        <h4 class="padd-tb-10">Social/Environmental Science</h4>
                                        <div class="row">
                                            <div class="col-md-6 col-xs-12">

                                                <div class="row">
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label><sup></sup>Mark Obtained :</label>
                                                        <asp:TextBox runat="server" ID="txtSSCSocialScienceObtMark" class="form-control obtainedssc disabledWhenCBSESSC" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label><sup></sup>Max Mark :</label>
                                                        <asp:TextBox runat="server" ID="txtSSCSocialScienceMaxMark" class="form-control maxssc disabledWhenCBSESSC" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label><sup></sup>&#37; of Mark :</label>
                                                        <asp:TextBox runat="server" ID="txtSSCSocialSciencePer" class="form-control clearSSCPercent" MaxLength="6"></asp:TextBox>
                                                    </div>

                                                </div>
                                            </div>
                                            <div class="col-md-6 col-xs-12">
                                                <div class="row">
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label>Grade Point :</label>
                                                        <asp:TextBox runat="server" ID="txtSSCSocSciGdPoint" class="form-control disabledSSCGrades" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label>Grade :</label>
                                                        <asp:TextBox runat="server" ID="txtSSCSocSciGrade" class="form-control disabledSSCGrades upper" MaxLength="4"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <h4 class="padd-tb-10">Computer Application</h4>
                                        <div class="row">
                                            <div class="col-md-6 col-xs-12">
                                                <div class="row">
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label><sup></sup>Mark Obtained :</label>
                                                        <asp:TextBox runat="server" ID="txtISEComputerApplicationObtMark" class="form-control obtainedssc disabledWhenCBSESSC" MaxLength="6"></asp:TextBox>

                                                    </div>
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label><sup></sup>Max Mark :</label>

                                                        <asp:TextBox runat="server" ID="txtISEComputerApplicationMaxMark" class="form-control maxssc disabledWhenCBSESSC" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label><sup></sup>&#37; of Mark :</label>

                                                        <asp:TextBox runat="server" ID="txtISEComputerApplicationPer" class="form-control clearSSCPercent" MaxLength="6"></asp:TextBox>
                                                    </div>

                                                </div>
                                            </div>
                                            <div class="col-md-6 col-xs-12">
                                                <div class="row">
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label>Grade Point :</label>
                                                        <asp:TextBox runat="server" ID="txtiseComputerAppGdPoint" class="form-control disabledSSCGrades" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label>Grade :</label>
                                                        <asp:TextBox runat="server" ID="txtisecomputerAppGrade" class="form-control disabledSSCGrades upper" MaxLength="4"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <h4 class="padd-tb-10">History, Civics &amp; Geography</h4>
                                        <div class="row">
                                            <div class="col-md-6 col-xs-12">
                                                <div class="row">
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label><sup></sup>Mark Obtained :</label>

                                                        <asp:TextBox runat="server" ID="txtISEHistoryObtMark" class="form-control obtainedssc disabledWhenCBSESSC" MaxLength="6"></asp:TextBox>

                                                    </div>
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label><sup></sup>Max Mark :</label>

                                                        <asp:TextBox runat="server" ID="txtISEHistoryMaxMark" class="form-control maxssc disabledWhenCBSESSC" MaxLength="6"></asp:TextBox>

                                                    </div>
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label><sup></sup>&#37; of Mark :</label>

                                                        <asp:TextBox runat="server" ID="txtISEHistoryPer" class="form-control clearSSCPercent" MaxLength="6"></asp:TextBox>

                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6 col-xs-12">
                                                <div class="row">
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label>Grade Point :</label>
                                                        <asp:TextBox runat="server" ID="txtIseHistGdPoint" class="form-control disabledSSCGrades" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label>Grade :</label>
                                                        <asp:TextBox runat="server" ID="txtIseHistGrade" class="form-control disabledSSCGrades upper" MaxLength="4"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row padd-t-10">

                                            <div class="col-md-3 col-sm-4 col-xs-6 form-group">
                                                <label><sup></sup>Total Marks Scored :</label>
                                                <asp:TextBox runat="server" ID="txtSSCTotalMarkScore" class="form-control clearSSCPercent" Enabled="false" MaxLength="6"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-4 col-xs-6 form-group">
                                                <label><sup></sup>Total of Max Marks :</label>
                                                <asp:TextBox runat="server" ID="txtSSCTotalMaxMark" class="form-control clearSSCPercent" Enabled="false" MaxLength="6"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-4 col-xs-6 form-group">
                                                <label>Total Percentage &#37; :</label>
                                                <asp:TextBox runat="server" ID="txtSSCTotalPer" class="form-control clearSSCPercent" MaxLength="6"></asp:TextBox>
                                            </div>

                                            <div class="col-md-3 col-sm-4 col-xs-6 form-group">
                                                <label>Total Grade Point :</label>
                                                <asp:TextBox runat="server" ID="txtSSCtotalGradePoint" class="form-control disabledSSCGrades" MaxLength="6"></asp:TextBox>
                                            </div>

                                            <div class="col-md-3 col-sm-4 col-xs-6 form-group">
                                                <label>CGPA :</label>

                                                <asp:TextBox runat="server" ID="txtSSCCGPA" class="form-control disabledSSCGrades" MaxLength="6"></asp:TextBox>
                                            </div>

                                            <div class="col-md-3 col-sm-4 col-xs-6 form-group">
                                                <label><sup>*</sup> Month / Year of Passing :</label>
                                                <div class='input-group date' id='myDatepicker15'>
                                                    <asp:TextBox runat="server" ID="txtSSCYearofPassing" class="form-control" MaxLength="7" data-inputmask="'mask': '99/9999'" placeholder="MM/YYYY"></asp:TextBox>
                                                    <span class="input-group-addon">
                                                        <span class="glyphicon glyphicon-calendar"></span>
                                                    </span>
                                                </div>
                                            </div>
                                            <div class="col-md-3 col-sm-4 col-xs-6 form-group">
                                                <label><sup>*</sup>Medium of Instruction :</label>
                                                <asp:TextBox runat="server" ID="txtSSCMediumOfInstruction" class="form-control" placeholder="Enter Medium of Instruction" MaxLength="30"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-4 col-xs-6 form-group">
                                                <label>Mark Certificate Number :</label>
                                                <asp:TextBox runat="server" ID="txtSSCMarkCertNo" class="form-control" placeholder="Enter Mark Certificate Number" MaxLength="15"></asp:TextBox>

                                            </div>
                                            <div class="col-md-3 col-sm-4 col-xs-6 form-group">
                                                <label>Pass Certificate Number :</label>
                                                <asp:TextBox runat="server" ID="txtSSCPassCertNo" class="form-control onlyalphanum" placeholder="Enter Pass Certificate Number" MaxLength="15"></asp:TextBox>

                                            </div>
                                            <div class="col-md-3 col-sm-4 col-xs-6 form-group" id="divTMR" runat="server" visible="false">
                                                <label><sup></sup>TMR Number :</label>
                                                <asp:TextBox runat="server" ID="txtSSCTMRNo" class="form-control" placeholder="Enter TMR Number" MaxLength="15"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-4 col-xs-6 form-group">
                                                <label><sup>*</sup> Register Number :</label>
                                                <asp:TextBox runat="server" ID="txtSSCRegisterNo" class="form-control onlyalphanum" placeholder="Enter Register Number" MaxLength="15"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-4 col-xs-6 form-group">
                                                <label><sup>*</sup> Institute Name :</label>
                                                <asp:TextBox runat="server" ID="txtSSCInstituteName" class="form-control onlycharacter" placeholder="Enter Institute Name" MaxLength="100"></asp:TextBox>
                                            </div>

                                            <div class="col-md-6 col-sm-6 form-group">
                                                <label>Institute Address :</label>
                                                &nbsp;&nbsp;<label>Characters Limit : &nbsp</label>
                                                <label id="lblcountInsAdd" style="background-color: #E2EEF1; color: Red; font-weight: bold;">200</label>
                                                <asp:TextBox runat="server" ID="txtSSCInstituteAddress" class="form-control" TextMode="MultiLine" Rows="3" Columns="50" data-toggle="tooltip" data-placement="top" onkeyup="LimtCharactersInsAdd(this,200,'lblcountInsAdd')"></asp:TextBox>
                                            </div>

                                        </div>

                                        <hr />

                                        <div class="row">
                                            <div class="col-md-12 col-sm-12">
                                                <h4 class="padd-tb-10 sub-heading">HSC Marks</h4>
                                                <div class="row">

                                                    <div class="col-md-3 col-sm-4 col-xs-12 form-group">
                                                        <label><sup></sup>Transfer Certificate Number :</label>
                                                        <asp:TextBox runat="server" ID="txtHSCTransferCertNo" class="form-control" placeholder="Enter Transfer Certificate Number" MaxLength="15"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3 col-sm-4 col-xs-12 form-group">
                                                        <label><sup>*</sup>Select Board Category for HSC :</label>
                                                        <asp:DropDownList runat="server" ID="ddlHSCBoardCategory" class="form-control" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlHSCBoardCategory_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <%--Added by Naresh Beerla [29092020]--%>
                                                    <div class="col-md-3 col-sm-4 col-xs-12 form-group">
                                                        <label><sup></sup>Other Board HSC :</label>
                                                        <asp:TextBox runat="server" ID="txtOtherBoardHSC" class="form-control" Enabled="false" MaxLength="20"></asp:TextBox>
                                                    </div>
                                                    <%--Added by Naresh Beerla [29092020]--%>
                                                    <div class="col-md-3 col-sm-4 col-xs-12 form-group">
                                                        <label><sup>*</sup> First Language :</label>
                                                        <asp:DropDownList runat="server" ID="ddlHSCLanguage" class="form-control" AppendDataBoundItems="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <%--Added by Naresh Beerla [29092020]--%>
                                                    <div class="col-md-3 col-sm-4 col-xs-12 form-group">
                                                        <label><sup></sup></label>
                                                        <asp:RadioButtonList CssClass="vertical-radio-btn" ID="rblInputsystemHSC" runat="server" RepeatDirection="Horizontal" ToolTip="Select any one">
                                                            <asp:ListItem Value="1" Selected="True">Mark System</asp:ListItem>
                                                            <asp:ListItem Value="2">Grade System</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                    <%--Added by Naresh Beerla [29092020]--%>
                                                </div>
                                            </div>
                                        </div>
                                        <h4 class="padd-tb-10">Language</h4>
                                        <div class="row">
                                            <div class="col-md-6 col-xs-12">

                                                <div class="row">
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label><sup></sup>Mark Obtained :</label>
                                                        <asp:TextBox runat="server" ID="txtHSCLangObtMark" class="form-control obtainedhsc disabledWhenCBSEHSC" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label><sup></sup>Max Mark :</label>
                                                        <asp:TextBox runat="server" ID="txtHSCLangMaxMark" class="form-control maxhsc disabledWhenCBSEHSC" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label><sup></sup>&#37; of Mark :</label>
                                                        <asp:TextBox runat="server" ID="txtHSCLangPer" class="form-control clearHSCPercent" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6 col-xs-12">
                                                <div class="row">
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label>Grade Point :</label>
                                                        <asp:TextBox runat="server" ID="txtHSCLangGdPoint" class="form-control disabledHSCGrades" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label>Grade :</label>
                                                        <asp:TextBox runat="server" ID="txtHSCLangGrade" class="form-control disabledHSCGrades" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <h4 class="padd-tb-10">English</h4>
                                        <div class="row">
                                            <div class="col-md-6 col-sm-12 col-xs-12">
                                                <div class="row">
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label><sup></sup>Mark Obtained :</label>
                                                        <asp:TextBox runat="server" ID="txtHSCEnglishObtMark" class="form-control obtainedhsc disabledWhenCBSEHSC" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label><sup></sup>Max Mark :</label>
                                                        <asp:TextBox runat="server" ID="txtHSCEnglishMaxMark" class="form-control maxhsc disabledWhenCBSEHSC" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label><sup></sup>&#37; of Mark :</label>
                                                        <asp:TextBox runat="server" ID="txtHSCEnglishPer" class="form-control clearHSCPercent" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6 col-xs-12">
                                                <div class="row">
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label>Grade Point :</label>
                                                        <asp:TextBox runat="server" ID="txtHSCEngGdpoint" class="form-control disabledHSCGrades" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label>Grade :</label>
                                                        <asp:TextBox runat="server" ID="txtHSCEngGrade" class="form-control disabledHSCGrades upper" MaxLength="4"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <h4 class="padd-tb-10">Maths</h4>
                                        <div class="row">
                                            <div class="col-md-6 col-xs-12">

                                                <div class="row">
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label><sup></sup>Mark Obtained :</label>
                                                        <asp:TextBox runat="server" ID="txtHSCMathsObtMark" class="form-control obtainedhsc disabledWhenCBSEHSC" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label><sup></sup>Max Mark :</label>
                                                        <asp:TextBox runat="server" ID="txtHSCMathsMaxMark" class="form-control maxhsc disabledWhenCBSEHSC" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label><sup></sup>&#37; of Mark :</label>
                                                        <asp:TextBox runat="server" ID="txtHSCMathsPer" class="form-control clearHSCPercent" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6 col-xs-12">
                                                <div class="row">
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label>Grade Point :</label>
                                                        <asp:TextBox runat="server" ID="txtHSCMathGdpoint" class="form-control disabledHSCGrades" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label>Grade :</label>
                                                        <asp:TextBox runat="server" ID="txtHSCMathGrade" class="form-control disabledHSCGrades upper" MaxLength="4"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div runat="server" id="divNonVocational" class="NonVocational">
                                            <h4 class="padd-tb-10">Physics</h4>
                                            <div class="row">
                                                <div class="col-md-6 col-xs-12">
                                                    <div class="row">
                                                        <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                            <label><sup></sup>Mark Obtained :</label>
                                                            <asp:TextBox runat="server" ID="txtHSCPhysicsObtMark" class="form-control obtainedhsc disabledWhenCBSEHSC clearNonVocational" MaxLength="6"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                            <label><sup></sup>Max Mark :</label>
                                                            <asp:TextBox runat="server" ID="txtHSCPhysicsMaxMark" class="form-control maxhsc disabledWhenCBSEHSC clearNonVocational" MaxLength="6"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                            <label><sup></sup>&#37; of Mark :</label>
                                                            <asp:TextBox runat="server" ID="txtHSCPhysicsPer" class="form-control clearNonVocationalPercent" MaxLength="6"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-6 col-xs-12">
                                                    <div class="row">
                                                        <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                            <label>Grade Point :</label>
                                                            <asp:TextBox runat="server" ID="txtHSCPhyGdPoint" class="form-control disabledHSCGrades clearNonVocational" MaxLength="6"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                            <label>Grade :</label>
                                                            <asp:TextBox runat="server" ID="txtHSCPhyGrade" class="form-control disabledHSCGrades clearNonVocational upper" MaxLength="4"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <h4 class="padd-tb-10">Chemistry</h4>

                                            <div class="row">
                                                <div class="col-md-6 col-sm-12">
                                                    <div class="row">
                                                        <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                            <label><sup></sup>Mark Obtained :</label>
                                                            <asp:TextBox runat="server" ID="txtHSCChemObtMark" class="form-control obtainedhsc disabledWhenCBSEHSC clearNonVocational" MaxLength="6"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                            <label><sup></sup>Max Mark :</label>
                                                            <asp:TextBox runat="server" ID="txtHSCChemMaxMark" class="form-control maxhsc disabledWhenCBSEHSC clearNonVocational" MaxLength="6"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                            <label><sup></sup>&#37; of Mark :</label>
                                                            <asp:TextBox runat="server" ID="txtHSCChemPer" class="form-control clearNonVocationalPercent" MaxLength="6"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-6 col-sm-12 col-xs-12">
                                                    <div class="row">
                                                        <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                            <label>Grade Point :</label>
                                                            <asp:TextBox runat="server" ID="txtHSCChemGdPoint" class="form-control disabledHSCGrades clearNonVocational" MaxLength="6"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                            <label>Grade :</label>
                                                            <asp:TextBox runat="server" ID="txtHSCChemGrade" class="form-control disabledHSCGrades clearNonVocational upper" MaxLength="4"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row" id="divOptionalSub" runat="server" visible="false">
                                                <div class="col-md-3 col-xs-6 form-group">
                                                    <h4 class="padd-b-10 m0">&nbsp;</h4>
                                                    <label><sup>*</sup> Optional Subject :</label>
                                                    <asp:DropDownList runat="server" ID="ddlHSCOptionalSub" AppendDataBoundItems="true" class="form-control">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <h4 class="padd-tb-10">Biology </h4>
                                            <div class="row">
                                                <div class="col-md-6 col-sm-12">
                                                    <div class="row">
                                                        <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                            <label><sup></sup>Mark Obtained :</label>
                                                            <asp:TextBox runat="server" ID="txtHSCOptionalMarkObt" class="form-control obtainedhsc disabledWhenCBSEHSC clearNonVocational" MaxLength="6"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                            <label><sup></sup>Max Mark :</label>
                                                            <asp:TextBox runat="server" ID="txtHSCOptionalMaxMark" class="form-control maxhsc disabledWhenCBSEHSC clearNonVocational" MaxLength="6"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                            <label><sup></sup>&#37; of Mark :</label>
                                                            <asp:TextBox runat="server" ID="txtHSCOptionalPer" class="form-control clearNonVocationalPercent" MaxLength="6"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-6 col-xs-12">
                                                    <div class="row">
                                                        <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                            <label>Grade Point :</label>
                                                            <asp:TextBox runat="server" ID="txtHSCOptionalGdPoint" class="form-control disabledHSCGrades clearNonVocational" MaxLength="6"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                            <label>Grade :</label>
                                                            <asp:TextBox runat="server" ID="txtHSCOptionalGrade" class="form-control disabledHSCGrades clearNonVocational upper" MaxLength="4"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <h4 class="padd-tb-10">Botany </h4>
                                            <div class="row">
                                                <div class="col-md-6 col-sm-12">
                                                    <div class="row">
                                                        <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                            <label><sup></sup>Mark Obtained :</label>
                                                            <asp:TextBox runat="server" ID="txtBotMarkObt" class="form-control obtainedhsc disabledWhenCBSEHSC clearNonVocational" MaxLength="6"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                            <label><sup></sup>Max Mark :</label>
                                                            <asp:TextBox runat="server" ID="txtBotMaxMark" class="form-control maxhsc disabledWhenCBSEHSC clearNonVocational" MaxLength="6"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                            <label><sup></sup>&#37; of Mark :</label>
                                                            <asp:TextBox runat="server" ID="txtBotPerMark" class="form-control clearNonVocationalPercent" MaxLength="6"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-6 col-xs-12">
                                                    <div class="row">
                                                        <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                            <label>Grade Point :</label>
                                                            <asp:TextBox runat="server" ID="txtBotGdPt" class="form-control disabledHSCGrades clearNonVocational" MaxLength="6"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                            <label>Grade :</label>
                                                            <asp:TextBox runat="server" ID="txtBotGrade" class="form-control disabledHSCGrades clearNonVocational upper" MaxLength="4"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <h4 class="padd-tb-10">Zoology </h4>
                                            <div class="row">
                                                <div class="col-md-6 col-sm-12">
                                                    <div class="row">
                                                        <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                            <label><sup></sup>Mark Obtained :</label>
                                                            <asp:TextBox runat="server" ID="txtZooMarkObt" class="form-control obtainedhsc disabledWhenCBSEHSC clearNonVocational" MaxLength="6"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                            <label><sup></sup>Max Mark :</label>
                                                            <asp:TextBox runat="server" ID="txtZooMaxMark" class="form-control maxhsc disabledWhenCBSEHSC clearNonVocational" MaxLength="6"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                            <label><sup></sup>&#37; of Mark :</label>
                                                            <asp:TextBox runat="server" ID="txtZooPerMark" class="form-control clearNonVocationalPercent" MaxLength="6"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-6 col-xs-12">
                                                    <div class="row">
                                                        <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                            <label>Grade Point :</label>
                                                            <asp:TextBox runat="server" ID="txtZooGdPt" class="form-control disabledHSCGrades clearNonVocational" MaxLength="6"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                            <label>Grade :</label>
                                                            <asp:TextBox runat="server" ID="txtZooGrade" class="form-control disabledHSCGrades clearNonVocational" MaxLength="6"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>



                                            <h4 class="padd-tb-10">Computer Science </h4>
                                            <div class="row">
                                                <div class="col-md-6 col-sm-12">
                                                    <div class="row">
                                                        <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                            <label><sup></sup>Mark Obtained :</label>
                                                            <asp:TextBox runat="server" ID="txtCSMarkObt" class="form-control obtainedhsc disabledWhenCBSEHSC clearNonVocational" MaxLength="6"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                            <label><sup></sup>Max Mark :</label>
                                                            <asp:TextBox runat="server" ID="txtCSMaxMark" class="form-control maxhsc disabledWhenCBSEHSC clearNonVocational" MaxLength="6"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                            <label><sup></sup>&#37; of Mark :</label>
                                                            <asp:TextBox runat="server" ID="txtCSPerMark" class="form-control clearNonVocationalPercent" MaxLength="6"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-6 col-xs-12">
                                                    <div class="row">
                                                        <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                            <label>Grade Point :</label>
                                                            <asp:TextBox runat="server" ID="txtCSGdPt" class="form-control disabledHSCGrades clearNonVocational" MaxLength="6"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                            <label>Grade :</label>
                                                            <asp:TextBox runat="server" ID="txtCSGrade" class="form-control disabledHSCGrades clearNonVocational upper" MaxLength="4"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>



                                        </div>


                                        <div runat="server" id="divVocational" class="Vocational">
                                            <h4 class="padd-tb-10">Theory</h4>
                                            <div class="row">

                                                <div class="col-md-6 col-sm-12 col-xs-12">
                                                    <div class="row">
                                                        <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                            <label><sup></sup>Mark Obtained :</label>
                                                            <asp:TextBox runat="server" ID="txtVocationalTHObtMark" class="form-control obtainedhsc disabledWhenCBSEHSC clearVocational" MaxLength="6"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                            <label><sup></sup>Max Mark :</label>
                                                            <asp:TextBox runat="server" ID="txtVocationalTHMaxMark" class="form-control maxhsc disabledWhenCBSEHSC clearVocational" MaxLength="6"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                            <label><sup></sup>&#37; of Mark :</label>
                                                            <asp:TextBox runat="server" ID="txtVocationalTHPer" class="form-control clearHSCPercent clearVocational" Enabled="false" MaxLength="6"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-6 col-xs-12">
                                                    <div class="row">
                                                        <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                            <label>Grade Point :</label>
                                                            <asp:TextBox runat="server" ID="txtVocationalTHGdPoint" class="form-control disabledHSCGrades clearVocational" MaxLength="6"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                            <label>Grade :</label>
                                                            <asp:TextBox runat="server" ID="txtVocationalTHGrade" class="form-control disabledHSCGrades clearVocational upper" MaxLength="4"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>

                                            <h4 class="padd-tb-10">Practical 1</h4>
                                            <div class="row">
                                                <div class="col-md-6 col-xs-12">
                                                    <div class="row">
                                                        <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                            <label><sup></sup>Mark Obtained :</label>
                                                            <asp:TextBox runat="server" ID="txtVocationalPR1ObtMark" class="form-control obtainedhsc disabledWhenCBSEHSC clearVocational" MaxLength="6"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                            <label><sup></sup>Max Mark :</label>
                                                            <asp:TextBox runat="server" ID="txtVocationalPR1MaxMark" class="form-control maxhsc disabledWhenCBSEHSC clearVocational" MaxLength="6"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                            <label><sup></sup>&#37; of Mark :</label>
                                                            <%--<input type="text" class="form-control">--%>
                                                            <asp:TextBox runat="server" ID="txtVocationalPR1Per" class="form-control clearHSCPercent clearVocational" Enabled="false" MaxLength="6"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-6 col-xs-12">
                                                    <div class="row">
                                                        <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                            <label>Grade Point :</label>
                                                            <asp:TextBox runat="server" ID="txtVocationalPR1GradePoint" class="form-control disabledHSCGrades clearVocational" MaxLength="6"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                            <label>Grade :</label>
                                                            <asp:TextBox runat="server" ID="txtVocationalPR1Grade" class="form-control disabledHSCGrades clearVocational upper" MaxLength="4"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <h4 class="padd-tb-10">Practical 2</h4>
                                            <div class="row">
                                                <div class="col-md-6 col-xs-12">
                                                    <div class="row">
                                                        <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                            <label><sup></sup>Mark Obtained :</label>
                                                            <asp:TextBox runat="server" ID="txtVocationalPR2ObtMark" class="form-control obtainedhsc disabledWhenCBSEHSC clearVocational" MaxLength="6"></asp:TextBox>

                                                        </div>
                                                        <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                            <label><sup></sup>Max Mark :</label>

                                                            <asp:TextBox runat="server" ID="txtVocationalPR2MaxMark" class="form-control maxhsc disabledWhenCBSEHSC clearVocational" MaxLength="6"></asp:TextBox>

                                                        </div>
                                                        <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                            <label><sup></sup>&#37; of Mark :</label>
                                                            <asp:TextBox runat="server" ID="txtVocationalPR2Per" class="form-control clearHSCPercent clearVocational" Enabled="false" MaxLength="6"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-6 col-xs-12">
                                                    <div class="row">
                                                        <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                            <label>Grade Point :</label>
                                                            <asp:TextBox runat="server" ID="txtVocationalPR2Gdpoint" class="form-control disabledHSCGrades clearVocational" MaxLength="6"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                            <label>Grade :</label>
                                                            <asp:TextBox runat="server" ID="txtVocationalPR2Grade" class="form-control disabledHSCGrades clearVocational upper" MaxLength="4"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="row padd-t-10">

                                            <div class="col-md-3 col-xs-6 form-group">
                                                <label><sup></sup>Total Marks Scored :</label>
                                                <asp:TextBox runat="server" ID="txtHSCTotalMarkScore" class="form-control clearHSCPercent" MaxLength="6"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-xs-6 form-group">
                                                <label><sup></sup>Total of Max Marks :</label>
                                                <asp:TextBox runat="server" ID="txtHSCTotalMaxMark" class="form-control clearHSCPercent" MaxLength="6"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-xs-6 form-group">
                                                <label><sup></sup>Total Percentage &#37; :</label>
                                                <asp:TextBox runat="server" ID="txtHSCTotalPer" class="form-control clearHSCPercent" MaxLength="6"></asp:TextBox>
                                            </div>

                                            <div class="col-md-3 col-sm-4 col-xs-6 form-group">
                                                <label>Total Grade Point :</label>
                                                <asp:TextBox runat="server" ID="txtHSCtotalGradePoint" class="form-control disabledHSCGrades clearVocational" MaxLength="6"></asp:TextBox>
                                            </div>

                                            <div class="col-md-3 col-sm-4 col-xs-6 form-group">
                                                <label>CGPA :</label>
                                                <asp:TextBox runat="server" ID="txtHSCCGPA" class="form-control disabledHSCGrades clearVocational" MaxLength="6"></asp:TextBox>
                                            </div>

                                            <div class="col-md-3 col-xs-6 form-group">
                                                <label><sup>*</sup> Month / Year of Passing :</label>
                                                <div class='input-group date' id='myDatepicker16'>
                                                    <asp:TextBox runat="server" ID="txtHSCYearofPassing" class="form-control" MaxLength="7" data-inputmask="'mask': '99/9999'" placeholder="MM/YYYY"></asp:TextBox>
                                                    <span class="input-group-addon">
                                                        <span class="glyphicon glyphicon-calendar"></span>
                                                    </span>
                                                </div>
                                            </div>

                                            <div class="col-md-3 col-xs-6 form-group">
                                                <label><sup>*</sup>Medium of Instruction :</label>
                                                <asp:TextBox runat="server" ID="txtHSCMediumOfInstruction" class="form-control" placeholder="Enter Medium of Instruction" MaxLength="30"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-xs-6 form-group">
                                                <label>Mark Certificate Number :</label>
                                                <asp:TextBox runat="server" ID="txtHSCMarkCertificateNo" class="form-control" placeholder="Enter Mark Certificate Number" MaxLength="15"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-xs-6 form-group">
                                                <label>Pass Certificate Number :</label>
                                                <asp:TextBox runat="server" ID="txtHSCPassCertificateNo" class="form-control onlyalphanum" placeholder="Enter Pass Certificate Number" MaxLength="15"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-xs-6 form-group" id="divHSCTMR" runat="server" visible="false">
                                                <label><sup></sup>TMR Number :</label>
                                                <asp:TextBox runat="server" ID="txtHSCTMRNo" class="form-control" placeholder="Enter TMR Number" MaxLength="15"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-xs-6 form-group">
                                                <label><sup>*</sup> Register Number :</label>
                                                <asp:TextBox runat="server" ID="txtHSCRegisterNo" class="form-control onlyalphanum" placeholder="Enter Register Number" MaxLength="15"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-xs-6 form-group">
                                                <label><sup>*</sup> Institute Name :</label>
                                                <asp:TextBox runat="server" ID="txtHSCInstituteName" class="form-control onlycharacter" placeholder="Enter Institute Name" MaxLength="100"></asp:TextBox>
                                            </div>

                                            <div class="col-md-6 col-sm-6 col-xs-12 form-group">
                                                <label>Institute Address :</label>
                                                &nbsp;&nbsp;<label>Characters Limit : &nbsp</label>
                                                <label id="lblcountInsAdd7" style="background-color: #E2EEF1; color: Red; font-weight: bold;">200</label>
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtHSCInstituteAddress" TextMode="MultiLine" Columns="80" Rows="3" MaxLength="200" onkeyup="LimtCharactersInsAdd(this,200,'lblcountInsAdd7')"></asp:TextBox>
                                            </div>
                                        </div>

                                        <h4 class="padd-tb-10">Diploma/ Degree/ Intermediate Details</h4>
                                        <div class="row">
                                            <div class="col-md-12 col-xs-12">
                                                <div class="row">
                                                    <div class="col-md-3 col-sm-4 col-xs-6 form-group">
                                                        <label><sup></sup>Diploma/ Degree/ Intermediate :</label>
                                                        <asp:DropDownList runat="server" ID="ddlDipDegree" AppendDataBoundItems="true" CssClass="form-control">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            <asp:ListItem Value="1">Diploma</asp:ListItem>
                                                            <asp:ListItem Value="2">Degree</asp:ListItem>
                                                            <asp:ListItem Value="2">Intermediate</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-3 col-sm-4 col-xs-6 form-group">
                                                        <label><sup></sup>Specialization :</label>
                                                        <asp:TextBox runat="server" ID="txtSpecialization" class="form-control" MaxLength="60"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3 col-sm-4 col-xs-12 form-group">
                                                        <label><sup></sup>Transfer Certificate Number :</label>
                                                        <asp:TextBox runat="server" ID="txtDIPTransferCertNo" class="form-control" placeholder="Enter Transfer Certificate Number" MaxLength="15"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-3 col-sm-4 col-xs-6 form-group">
                                                <label><sup></sup>Name of Diploma/ Degree/ Inter. :</label>
                                                <asp:TextBox runat="server" ID="txtNameofDiploma" class="form-control" MaxLength="50"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-4 col-xs-6 form-group">
                                                <label><sup></sup>College/School Name :</label>
                                                <asp:TextBox runat="server" ID="txtDiplomaCollege" class="form-control onlycharacter" MaxLength="60"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2 col-sm-4 col-xs-6 form-group">
                                                <label><sup></sup>Board / University :</label>
                                                <asp:TextBox runat="server" ID="txtDiplomaBoard" class="form-control" MaxLength="50"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2 col-sm-4 col-xs-6 form-group">
                                                <label><sup></sup>Register Number :</label>
                                                <asp:TextBox runat="server" ID="txtDipRegisterNo" class="form-control onlyalphanum" MaxLength="15"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2 col-sm-4 col-xs-6 form-group">
                                                <label><sup></sup>Month / Year of Passing :</label>
                                                <div class='input-group date' id='datePickerDiplomaYr'>
                                                    <asp:TextBox runat="server" ID="txtDiplomaYear" class="form-control" MaxLength="7" data-inputmask="'mask': '99/9999'" placeholder="MM/YYYY"></asp:TextBox>
                                                    <span class="input-group-addon">
                                                        <span class="glyphicon glyphicon-calendar"></span>
                                                    </span>
                                                </div>
                                            </div>

                                        </div>

                                        <div class="clearfix"></div>
                                        <div class="row">
                                            <div class="col-md-6 col-xs-12">
                                                <div class="row">
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label><sup></sup>Sem I Mark Obtained  :</label>
                                                        <asp:TextBox runat="server" ID="txtSemIObtained" class="form-control obtained" MaxLength="6"></asp:TextBox>

                                                    </div>
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label><sup></sup>Sem I Max Mark :</label>
                                                        <asp:TextBox runat="server" ID="txtSemIMax" class="form-control maxdip" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label><sup></sup>Sem I &#37; of Mark :</label>
                                                        <asp:TextBox runat="server" ID="txtSemIPer" class="form-control clearNonVocationalPercent" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6 col-xs-12">
                                                <div class="row">
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label><sup></sup>Sem II Mark Obtained :</label>
                                                        <asp:TextBox runat="server" ID="txtSemIIObtained" class="form-control obtained" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label><sup></sup>Sem II Max Mark :</label>
                                                        <asp:TextBox runat="server" ID="txtSemIIMax" class="form-control maxdip" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label><sup></sup>Sem II &#37; of Mark :</label>
                                                        <asp:TextBox runat="server" ID="txtSemIIPer" class="form-control clearNonVocationalPercent" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-6 col-xs-12">
                                                <div class="row">

                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label><sup></sup>Sem III Mark Obtained :</label>
                                                        <asp:TextBox runat="server" ID="txtSemIIIObtained" class="form-control obtained" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label><sup></sup>Sem III Max Mark :</label>
                                                        <asp:TextBox runat="server" ID="txtSemIIIMax" class="form-control maxdip" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label><sup></sup>Sem III &#37; of Mark :</label>
                                                        <asp:TextBox runat="server" ID="txtSemIIIPer" class="form-control clearNonVocationalPercent" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6 col-xs-12">
                                                <div class="row">
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label><sup></sup>Sem IV Mark Obtained :</label>
                                                        <asp:TextBox runat="server" ID="txtSemIVObtained" class="form-control obtained" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label><sup></sup>Sem IV Max Mark :</label>
                                                        <asp:TextBox runat="server" ID="txtSemIVMax" class="form-control maxdip" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label><sup></sup>Sem IV &#37; of Mark :</label>
                                                        <asp:TextBox runat="server" ID="txtSemIVPer" class="form-control clearNonVocationalPercent" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-6 col-xs-12">
                                                <div class="row">

                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label><sup></sup>Sem V Mark Obtained :</label>
                                                        <asp:TextBox runat="server" ID="txtSemVObtained" class="form-control obtained" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label><sup></sup>Sem V Max Mark :</label>
                                                        <asp:TextBox runat="server" ID="txtSemVMax" class="form-control maxdip" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label><sup></sup>Sem V &#37 of Mark :</label>
                                                        <asp:TextBox runat="server" ID="txtSemVPer" class="form-control clearNonVocationalPercent" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6 col-xs-12">
                                                <div class="row">
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label><sup></sup>Sem VI Mark Obtained :</label>
                                                        <asp:TextBox runat="server" ID="txtSemVIObtained" class="form-control obtained" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label><sup></sup>Sem VI Max Mark :</label>
                                                        <asp:TextBox runat="server" ID="txtSemVIMax" class="form-control maxdip" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label><sup></sup>Sem VI &#37 of Mark :</label>
                                                        <asp:TextBox runat="server" ID="txtSemVIPer" class="form-control clearNonVocationalPercent" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">

                                            <div class="col-md-6 col-xs-12">
                                                <div class="row">
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label><sup></sup>Sem VII Obtained Mark :</label>
                                                        <asp:TextBox runat="server" ID="txtSemVIIObtained" class="form-control obtained" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label><sup></sup>Sem VII Max Mark :</label>
                                                        <asp:TextBox runat="server" ID="txtSemVIIMax" class="form-control maxdip" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label><sup></sup>Sem VII &#37 of Mark :</label>
                                                        <asp:TextBox runat="server" ID="txtSemVIIPer" class="form-control clearNonVocationalPercent" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="col-md-6 col-xs-12">
                                                <div class="row">
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label><sup></sup>Sem VIII Obtained Mark :</label>
                                                        <asp:TextBox runat="server" ID="txtSemVIIIObtained" class="form-control obtained" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label><sup></sup>Sem VIII Max Mark :</label>
                                                        <asp:TextBox runat="server" ID="txtSemVIIIMax" class="form-control maxdip" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label><sup></sup>Sem VIII &#37 of Mark :</label>
                                                        <asp:TextBox runat="server" ID="txtSemVIIIPer" class="form-control clearNonVocationalPercent" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>




                                            <div class="col-md-6 col-xs-12">
                                                <div class="row">

                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label><sup></sup>Total Marks Scored :</label>
                                                        <asp:TextBox runat="server" ID="txtTotalMarksScore" class="form-control" Enabled="false" MaxLength="6"></asp:TextBox>
                                                    </div>

                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label><sup></sup>Total Of Max Marks :</label>
                                                        <asp:TextBox runat="server" ID="txtTotalMaxMark" class="form-control" Enabled="false" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 col-sm-4 col-xs-6 form-group">
                                                        <label><sup></sup>Total Percentage (&#37) :</label>
                                                        <asp:TextBox runat="server" ID="txtTotalPer" class="form-control clearNonVocationalPercent" MaxLength="6"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="text-center padd-tb-10">
                                            <button type="button" class="btn btn-default" onclick="parent.location='#step-2'">Previous</button>
                                            <%--<button type="button" class="btn btn-outline-info">Next</button>--%>
                                            <asp:Button runat="server" ID="btnSaveNext3" class="btn btn-outline-info" Text="Save and Next" OnClick="btnSaveNext3_Click" OnClientClick="return step3Click_validation()" />
                                            <asp:HiddenField runat="server" ID="hdnAdmissionType" Value="0" />
                                            <%--  <button type="button" class="btn btn-danger">Cancel</button>--%>
                                            <div id="tab3Print" runat="server" visible="false" style="font-size: large;">
                                                <a href="PrintApplication.aspx">Print Application Form</a>
                                            </div>
                                        </div>
                                    </ContentTemplate>

                                    <%-- <Triggers>
                                        <asp:PostBackTrigger ControlID="ddlHSCBoardCategory" />
                                    </Triggers>--%>
                                </asp:UpdatePanel>

                            </div>
                        </div>

                        <div id="step-4" class="">
                            <div class="container-fluid" style="padding: 10px;">
                                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                    <ContentTemplate>
                                        <div class="flex-container">
                                            <div class="col-md-6 flexy-box">
                                                <h3>Student Photo</h3>
                                                <div class="flex-box-inner">
                                                    <asp:Image runat="server" ID="imgpreview" Height="147" Width="148" />
                                                </div>
                                                <div class="fileUpload blue-btn btn width100" style="margin-top: 10px">
                                                    <span>Click Here to browse photo</span>
                                                    <asp:FileUpload runat="server" class="uploadlogo" ID="fuStudentPhoto1" accept=".jpg,.jpeg,.png" />
                                                </div>
                                            </div>
                                            <div class="col-md-6 flexy-box">
                                                <h3>Student Signature</h3>
                                                <div class="flex-box-inner">
                                                    <asp:Image runat="server" ID="imgpreviewsign" Width="198" Height="37" />
                                                </div>
                                                <div class="fileUpload blue-btn btn width100" style="margin-top: 10px">
                                                    <span>Click Here to browse signature</span>
                                                    <asp:FileUpload runat="server" class="uploadlogo" ID="fuStudentSign" accept=".jpg,.jpeg,.png" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12 form-group text-center mt-10">
                                            <strong>
                                                <span style="color: red; font: large;">Note : </span><span style="color: red;">Only .jpg, .jpeg, .png format with max 50 KB size are allowed</span>
                                            </strong>
                                        </div>
                                        <hr />
                                        <div class="container-fluid">
                                            <asp:ListView ID="lvDocumentList" runat="server">
                                                <LayoutTemplate>
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <h3>Student Document List</h3>
                                                        </div>

                                                    </div>
                                                    <div class="table-responsive">
                                                        <table id="mylist" class="table table-hover table-bordered datatable">
                                                            <tr class="bg-light-blue">
                                                                <th style="text-align: center">Sr No.
                                                                </th>
                                                                <th style="text-align: center">Document List
                                                                </th>
                                                                <th>Upload Documents
                                                                </th>
                                                                <th style="text-align: center">Upload Status
                                                                </th>
                                                                <th style="text-align: center">Document Name
                                                                </th>
                                                                <%-- <th style="text-align: center">Download Document
                                                            </th>--%>
                                                            </tr>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </table>
                                                    </div>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td style="text-align: center">
                                                            <%#Container.DataItemIndex+1%>
                                                        </td>

                                                        <td style="text-align: center">
                                                            <asp:Label ID="lblDocument" runat="server" Text='<%# Eval("documentname") %>'></asp:Label>
                                                            <asp:HiddenField runat="server" ID="docid" Value='<%# Eval("DOCUMENTNO") %>' />
                                                        </td>
                                                        <td>
                                                            <asp:FileUpload ID="studentFileUpload" class="uploadlogo" TabIndex="3" runat="server" accept=".jpg,.jpeg,.pdf,.doc,.docx" />
                                                        </td>
                                                        <td style="text-align: center">

                                                            <asp:Label ID="chkOriCopy" runat="server"></asp:Label>
                                                            <%# Eval("STATUS")%>
                                                            <br />
                                                        </td>
                                                        <td style="text-align: center">
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("FILE_PATH") %>'></asp:Label>
                                                        </td>
                                                        <%--<td style="text-align: center">
                                                           <asp:LinkButton ID="lnkDownload" Text ='<%# Eval("FILENAME") %>' CommandArgument = '<%# Eval("FILE_PATH") %>' runat="server" OnClick = "DownloadFile"></asp:LinkButton>
                                                        </td>--%>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>

                                            <asp:HiddenField runat="server" ID="hdnDocCount" Value="0" />

                                        </div>

                                        <div class="col-md-12 text-center">
                                            <strong>
                                                <span style="color: red; font: large;">Note : </span><span style="color: red;">Only pdf, jpg, jpeg, doc, docx format with max 40 KB size are allowed</span>
                                            </strong>
                                        </div>

                                        <div class="col-md-12 col-xs-12 text-center">
                                            <div class="padd-tb-10">
                                                <%--<asp:Button runat="server" ID="document" class="btn btn-outline-info" Text="Save and Next" OnClick="uploadDocument" />--%>
                                                <button type="button" class="btn btn-default" onclick="parent.location='#step-3'">Previous</button>
                                                <%--<button type="button" class="btn btn-outline-info">Next</button>--%>
                                                <asp:Button runat="server" ID="btnSaveNext4" class="btn btn-outline-info" Text="Save and Next" OnClick="btnSaveNext4_Click" />
                                                <%-- <button type="button" class="btn btn-danger">Cancel</button>--%>
                                                <div id="tab4Print" runat="server" visible="false" style="font-size: large;">
                                                    <a href="PrintApplication.aspx">Print Application Form</a>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnSaveNext4" />
                                        <%-- <asp:PostBackTrigger ControlID="document" />--%>
                                    </Triggers>
                                </asp:UpdatePanel>

                                <asp:Label runat="server" ID="lblmsg" Text="" ForeColor="Red" Font-Bold="true"></asp:Label>
                            </div>
                        </div>

                        <div id="step-5" class="">
                            <div class="container-fluid">

                                <asp:UpdatePanel runat="server" ID="UpdatePanel4">
                                    <ContentTemplate>
                                        <br />
                                        <h4 class="padd-tb-10 sub-heading">Entrance Exam</h4>
                                        <%--<h4>Marks</h4>--%>

                                        <div class="col-md-5 col-sm-6 col-xs-12 form-group"></div>
                                        <div class="col-md-7 col-sm-6 col-xs-12 form-group">
                                            <label>Entrance Exam :</label>
                                            <br />
                                            <asp:RadioButtonList runat="server" ID="rbEntrance" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbEntrance_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Selected="True" Value="1">Yes</asp:ListItem>
                                                <asp:ListItem Value="2">No</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <br />
                                        <div id="divEntrance" runat="server">
                                            <div class="row padd-t-10">
                                                <div class="col-md-3 col-sm-6 col-xs-12 form-group">
                                                    <label><sup>*</sup> Exam Name :</label>
                                                    <asp:TextBox runat="server" ID="txtEntranceName" class="form-control" placeholder="Enter Entrance Exam Name" MaxLength="20"></asp:TextBox>
                                                </div>

                                                <div class="col-md-3 col-sm-6 col-xs-12 form-group">
                                                    <label><sup>*</sup> Exam Roll No. :</label>
                                                    <asp:TextBox runat="server" ID="txtEntranceRollno" class="form-control" placeholder="Enter Entrance Exam Roll No." MaxLength="10"></asp:TextBox>
                                                </div>

                                                <div class="col-md-3 col-sm-6 col-xs-12 form-group" id="divCutof" runat="server" visible="false">
                                                    <label><sup></sup>Cut of Marks :</label>
                                                    <asp:TextBox runat="server" ID="txtCutOffMarks" class="form-control" placeholder="Enter Cut off Marks" MaxLength="6"></asp:TextBox>
                                                </div>

                                                <div class="col-md-3 col-sm-6 col-xs-12 form-group">
                                                    <label><sup>*</sup> Score :</label>
                                                    <asp:TextBox runat="server" ID="txtOverallMark" class="form-control" placeholder="Enter Overall Marks" MaxLength="6"></asp:TextBox>
                                                </div>

                                                <div class="col-md-3 col-sm-6 col-xs-12 form-group" id="divCommunityRank" runat="server" visible="false">
                                                    <label>Community Rank :</label>
                                                    <asp:TextBox runat="server" ID="txtCommunityRank" class="form-control" placeholder="Enter Community Rank" MaxLength="6"></asp:TextBox>
                                                </div>

                                                <div class="col-md-3 col-sm-6 col-xs-12 form-group">
                                                    <label><sup>*</sup> Rank :</label>
                                                    <asp:TextBox runat="server" ID="txtOverAllRank" class="form-control" placeholder="Enter Overall Rank" MaxLength="6"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3 col-sm-6 col-xs-12 form-group">
                                                    <label>Percentile :</label>
                                                    <asp:TextBox runat="server" ID="txtPercentile" class="form-control" placeholder="Enter Percentile" MaxLength="6"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div id="Div1" runat="server" visible="false">
                                                <h4 class="sub-heading">TNEA Details (for TNEA Student)</h4>
                                                <div class="row">
                                                    <div class="col-md-3 col-sm-6 col-xs-12 form-group">
                                                        <label><sup></sup>TNEA Application Number :</label>
                                                        <asp:TextBox runat="server" ID="txtTNEAAppliactionNo" class="form-control" placeholder="Enter Application number" MaxLength="15"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3 col-sm-6 col-xs-12 form-group">
                                                        <label>TNEA Acknowledge Receipt Number :</label>
                                                        <asp:TextBox runat="server" ID="txtAknowledgeRecNo" class="form-control" placeholder="Enter Receipt Number" MaxLength="15"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3 col-sm-6 col-xs-12 form-group">
                                                        <label>TNEA Acknowledge Receipt Date :</label>
                                                        <div class='input-group date' id='myDatepicker9'>
                                                            <asp:TextBox runat="server" ID="txtAckRecDate" class="form-control" data-inputmask="'mask': '99/99/9999'"></asp:TextBox>
                                                            <span class="input-group-addon">
                                                                <span class="glyphicon glyphicon-calendar"></span>
                                                            </span>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3 col-sm-6 col-xs-12 form-group">
                                                        <label>TNEA Admission Order Number :</label>
                                                        <asp:TextBox runat="server" ID="txtAdmissionOrderNo" class="form-control" placeholder="Enter Admission Order Number" MaxLength="15"></asp:TextBox>

                                                    </div>
                                                    <div class="col-md-3 col-sm-6 col-xs-12 form-group">
                                                        <label><sup></sup>TNEA Admission Order Date :</label>
                                                        <div class='input-group date' id='myDatepicker10'>
                                                            <asp:TextBox runat="server" ID="txtAdmOrderDate" class="form-control" data-inputmask="'mask': '99/99/9999'"></asp:TextBox>
                                                            <span class="input-group-addon">
                                                                <span class="glyphicon glyphicon-calendar"></span>
                                                            </span>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3 col-sm-6 col-xs-12 form-group">
                                                        <label><sup></sup>TNEA Advance Payment Amount :</label>
                                                        <asp:TextBox runat="server" ID="txtAdvPaymentAmt" class="form-control" placeholder="Enter Admission Payment Amount" MaxLength="7"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3 col-sm-6 col-xs-12 form-group">
                                                        <label><sup></sup>Consortium No. :</label>
                                                        <asp:TextBox runat="server" ID="txtConsortiumNo" class="form-control" Visible='<%# Convert.ToBoolean(Eval("CATNO"))==true ? false : true %>' placeholder="Enter Consortium No."></asp:TextBox>
                                                    </div>
                                                </div>

                                                <h4 class="sub-heading">DOTE Details </h4>
                                                <div class="row">
                                                    <div class="col-md-3 col-sm-6 col-xs-12 form-group">

                                                        <label><sup></sup>Application Number :</label>
                                                        <asp:TextBox runat="server" ID="txtApplicationNo" class="form-control" placeholder="Application number" MaxLength="15"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3 col-sm-6 col-xs-12 form-group">
                                                        <label>Allotment Order Number :</label>
                                                        <asp:TextBox runat="server" ID="txtAllotmentOrderNo" class="form-control" placeholder="Allotment Order Number" MaxLength="15"></asp:TextBox>

                                                    </div>
                                                    <div class="col-md-3 col-sm-6 col-xs-12 form-group">
                                                        <label>Allotment Order Date :</label>
                                                        <div class='input-group date' id='dateAllotmentOrderDt'>
                                                            <asp:TextBox runat="server" ID="txtAllotmentOrderDate" class="form-control" data-inputmask="'mask': '99/99/9999'"></asp:TextBox>
                                                            <span class="input-group-addon">
                                                                <span class="glyphicon glyphicon-calendar"></span>
                                                            </span>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>

                                        <div class="text-center padd-tb-10">
                                            <button type="button" class="btn btn-default" onclick="parent.location='#step-4'">Previous</button>
                                            <asp:Button runat="server" ID="btnSaveNext6" class="btn btn-outline-info" Text="Save and Next" OnClick="btnSaveNext6_Click" OnClientClick="return step6Click_validation()" />
                                            <div id="tab5Print" runat="server" visible="false" style="font-size: large;">
                                                <a href="PrintApplication.aspx">Print Application Form</a>
                                            </div>
                                            <asp:HiddenField ID="hdnTNEANo" runat="server" Value="1" />
                                        </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                        <div id="step-6" class="">
                            <div class="container-fluid">
                                <asp:UpdatePanel runat="server" ID="UpdatePanel5">
                                    <ContentTemplate>

                                        <div class="row padd-t-10">
                                            <div class="col-sm-12">
                                                <div class="col-sm-4 col-xs-12 form-group">
                                                    <label><sup>*</sup> Single Parent ?</label><br />
                                                    <asp:RadioButtonList runat="server" ID="rdoSingleParent" OnSelectedIndexChanged="rdoSingleParent_SelectedIndexChanged" AutoPostBack="true" RepeatDirection="Horizontal" TabIndex="1">
                                                        <asp:ListItem Selected="False" Value="1">Yes</asp:ListItem>
                                                        <asp:ListItem Selected="True" Value="2">No</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>

                                                <div class="col-sm-4 col-xs-12 form-group" id="selectparent" style="display: block">
                                                    <label><sup></sup>Select Parent :</label>
                                                    <asp:DropDownList runat="server" ID="ddlSelectParent" AppendDataBoundItems="true" Enabled="false" class="form-control" TabIndex="2"
                                                        OnSelectedIndexChanged="ddlSelectParent_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        <asp:ListItem Value="1">Father</asp:ListItem>
                                                        <asp:ListItem Value="2">Mother</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="col-md-12 col-xs-12" id="divFather1" runat="server">

                                                <h4 class="sub-heading">Father Details</h4>

                                                <div class="row">
                                                    <div class="col-sm-4 col-xs-12 form-group">
                                                        <label><sup></sup>Father Name :</label>
                                                        <asp:TextBox runat="server" ID="txtFatherName" class="form-control onlycharacter" placeholder="Enter Father Name" MaxLength="20" TabIndex="3"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-4 col-xs-12 form-group">
                                                        <label>Father Initial/SurName :</label>
                                                        <asp:TextBox runat="server" ID="txtFatherInitial" class="form-control onlycharacter" placeholder="Enter Father Initial/Surname" MaxLength="20" TabIndex="4"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-4 col-xs-12 form-group">
                                                        <label><sup></sup>Qualification :</label>
                                                        <asp:LinkButton ID="lbtnFOtherQualification" runat="server" ForeColor="Green" OnClick="lbtnFOtherQualification_Click"> Add Other Qualification </asp:LinkButton>
                                                        <asp:DropDownList runat="server" ID="ddlFQualification" AppendDataBoundItems="true" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlFQualification_SelectedIndexChanged" TabIndex="5">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:TextBox runat="server" ID="txtFOtherQaulification" class="form-control" placeholder="Enter Qualification" Visible="false" MaxLength="20" TabIndex="6"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-4 col-xs-12 form-group">
                                                        <label><sup></sup>Occupation :</label>
                                                        <asp:DropDownList runat="server" ID="ddlFOccupation" AppendDataBoundItems="true" class="form-control" TabIndex="7">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-4 col-xs-12 form-group">
                                                        <label>Working Organisation Name :</label>
                                                        <asp:TextBox runat="server" ID="txtFOrganizationName" class="form-control" placeholder="Enter Organisation Name" MaxLength="50" TabIndex="8"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-4 col-xs-12 form-group">
                                                        <label>Designation :</label>
                                                        <asp:LinkButton ID="lbtnFOtherDesig" runat="server" ForeColor="Green" OnClick="lbtnFOtherDesig_Click"> Add Other Designation </asp:LinkButton></label>
                                                        <asp:DropDownList runat="server" ID="ddlFDesignation" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlFDesignation_SelectedIndexChanged" AutoPostBack="true" class="form-control" TabIndex="9">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:TextBox runat="server" ID="txtOtherDesignation" class="form-control" placeholder="Enter Designation" Visible="false" MaxLength="20" TabIndex="10"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-4 col-xs-12 form-group">
                                                        <label><sup></sup>Annual Income :</label>
                                                        <asp:TextBox runat="server" ID="txtFAnnualIncome" class="form-control" placeholder="Enter Annual Income" MaxLength="7" TabIndex="11"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-4 col-xs-12 form-group">
                                                        <label><sup></sup>Father Mobile Number :</label>
                                                        <asp:TextBox runat="server" ID="txtFMobileNo" class="form-control" placeholder="Enter Father Mobile Number" MaxLength="10" TabIndex="12"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 col-sm-4 col-xs-12 form-group">
                                                        <label><sup></sup>Father Email Id :</label>
                                                        <asp:TextBox runat="server" ID="txtFEmail" TextMode="Email" class="form-control" placeholder="Enter Father Email Id" MaxLength="50" TabIndex="13"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-4 col-xs-12 form-group">
                                                        <label><sup></sup>Father Aadhar Card Number :</label>
                                                        <asp:TextBox runat="server" ID="txtFatherAadhar" class="form-control" onkeypress="return OnlyNumeric();" placeholder="Enter Father Aadhar Card Number" MaxLength="12" TabIndex="14"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-4 col-xs-12 form-group">
                                                        <label><sup></sup>Father PAN Card Number :</label>
                                                        <asp:TextBox runat="server" ID="txtFPANNumber" class="form-control" placeholder="Enter Father PAN Card Number" MaxLength="10" TabIndex="15"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>

                                        <div class="row" id="divFather2" runat="server">

                                            <h4 class="padd-tb-10" id="lblFather2" runat="server" style="margin-left: 16px">Father Office Address</h4>

                                            <div class="col-sm-12 col-xs-12 form-group">
                                                <label><sup></sup>Address :</label>
                                                &nbsp;&nbsp;<label>Characters Limit : &nbsp</label>
                                                <label id="lblcountInsAdd2" style="background-color: #E2EEF1; color: Red; font-weight: bold;">200</label>
                                                <asp:TextBox runat="server" ID="txtFOfficeAddress" class="form-control" TextMode="MultiLine" placeholder="Enter Father office Address" MaxLength="500" onkeyup="LimtCharactersInsAdd(this,200,'lblcountInsAdd2')" TabIndex="16"></asp:TextBox>
                                            </div>

                                            <div class="clearfix"></div>

                                            <div class="col-md-3 col-sm-6 col-xs-12 form-group">
                                                <label><sup></sup>City/Town :</label>
                                                <asp:DropDownList runat="server" ID="ddlFofficeCity" AppendDataBoundItems="true" class="form-control" TabIndex="17">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-3 col-sm-6 col-xs-12 form-group">
                                                <label><sup></sup>State :</label>
                                                <asp:DropDownList runat="server" ID="ddlFofficeState" AppendDataBoundItems="true" class="form-control" TabIndex="18">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-3 col-sm-6 col-xs-12 form-group">
                                                <label><sup></sup>Pincode :</label>
                                                <asp:TextBox runat="server" ID="txtFofficePin" class="form-control" placeholder="Enter Pincode" MaxLength="6" TabIndex="19"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-6 col-xs-12 form-group">
                                                <label><sup></sup>STD code :</label>
                                                <asp:TextBox runat="server" ID="txtFofficeSTD" class="form-control" placeholder="Enter STD Code" MaxLength="8" TabIndex="20"></asp:TextBox>

                                            </div>
                                            <div class="col-md-3 col-sm-6 col-xs-12 form-group">
                                                <label><sup></sup>Landline Number :</label>
                                                <asp:TextBox runat="server" ID="txtFofficeLandline" class="form-control" placeholder="Enter Landline Number" MaxLength="12" TabIndex="21"></asp:TextBox>

                                            </div>
                                            <hr>
                                        </div>

                                        <div class="row">

                                            <div class="container-fluid" id="divMother1" runat="server" style="margin-top: 10px">
                                                <h4 class="padd-tb-10 sub-heading">Mother Details</h4>
                                                <div class="row">
                                                    <div class="col-md-4 col-sm-6 col-xs-12 form-group">
                                                        <label><sup></sup>Mother Name :</label>
                                                        <asp:TextBox runat="server" ID="txtMotherName" class="form-control onlycharacter" placeholder="Enter Mother Name" MaxLength="20" TabIndex="22"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 col-sm-6 col-xs-12 form-group">
                                                        <label>Mother Initial/Surname :</label>
                                                        <asp:TextBox runat="server" ID="txtMotherInitial" class="form-control onlycharacter" placeholder="Enter Initial/Surname" MaxLength="20" TabIndex="23"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 col-sm-6 col-xs-12 form-group">
                                                        <label><sup></sup>Qualification :</label>
                                                        <asp:LinkButton ID="lbtnMOtherQualification" runat="server" ForeColor="Green" OnClick="lbtnMOtherQualification_Click"> Add Other Qualification </asp:LinkButton>
                                                        <asp:DropDownList runat="server" ID="ddlMotherQualification" AppendDataBoundItems="true" Visible="false" class="form-control" AutoPostBack="true" TabIndex="25" OnSelectedIndexChanged="ddlMotherQualification_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:TextBox ID="txtMOtherQualification" runat="server" class="form-control" Detailsble="false" MaxLength="20" placeholder="Enter Qualification" TabIndex="26" VisiMother=""></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 col-sm-6 col-xs-12 form-group">
                                                        <label><sup></sup>Occupation :</label>
                                                        <asp:DropDownList runat="server" ID="ddlMOccupation" AppendDataBoundItems="true" class="form-control" TabIndex="27">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>

                                                    <%-- ADDED BY ABHINAY LAD [27-06-2019]--%>

                                                    <div class="col-sm-4 col-xs-12 form-group">
                                                        <label>Working Organisation Name :</label>
                                                        <asp:TextBox runat="server" ID="txtMOrganizationName" class="form-control" placeholder="Enter Organisation Name" MaxLength="50" TabIndex="28"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-4 col-xs-12 form-group">
                                                        <label>Designation :</label>
                                                        <asp:LinkButton ID="lbtnMOtherDesig" runat="server" ForeColor="Green" OnClick="lbtnMOtherDesig_Click"> Add Other Designation </asp:LinkButton></label>
                                                        <asp:DropDownList runat="server" ID="ddlMDesignation" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlMDesignation_SelectedIndexChanged" TabIndex="30" AutoPostBack="true" class="form-control">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:TextBox runat="server" ID="txtMOtherDesignation" class="form-control" placeholder="Enter Designation" Visible="false" MaxLength="20" TabIndex="31"></asp:TextBox>

                                                    </div>
                                                    <div class="col-sm-4 col-xs-12 form-group">
                                                        <label><sup></sup>Annual Income :</label>
                                                        <asp:TextBox runat="server" ID="txtMAnnualIncome" class="form-control" placeholder="Enter Annual Income" MaxLength="7" TabIndex="32"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 col-sm-6 col-xs-12 form-group">
                                                        <label>Mother Mobile Number :</label>
                                                        <asp:TextBox runat="server" ID="txtMotherMobile" class="form-control" placeholder="Enter Mother Mobile Number" MaxLength="10" TabIndex="33"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 col-sm-4 col-xs-12 form-group">
                                                        <label><sup></sup>Mother Email Id :</label>
                                                        <asp:TextBox runat="server" ID="txtMEmail" TextMode="Email" class="form-control" placeholder="Enter Mother Email Id" MaxLength="50" TabIndex="34"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-4 col-xs-12 form-group">
                                                        <label><sup></sup>Mother Aadhar Card Number :</label>
                                                        <asp:TextBox runat="server" ID="txtMotherAadhar" class="form-control" onkeypress="return OnlyNumeric();" placeholder="Enter Mother Aadhar Card Number" MaxLength="12" TabIndex="35"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-4 col-xs-12 form-group">
                                                        <label><sup></sup>Mother PAN Card Number :</label>
                                                        <asp:TextBox runat="server" ID="txtMPANNumber" class="form-control" placeholder="Enter Mother PAN Card Number" MaxLength="10" TabIndex="36"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="container-fluid" id="divMother2" runat="server">
                                                <h4 class="padd-tb-10">Mother Office Address</h4>
                                                <div class="row">

                                                    <div class="col-sm-12 col-xs-12 form-group">
                                                        <label>Address :</label>
                                                        &nbsp;&nbsp;<label>Characters Limit : &nbsp</label>
                                                        <label id="lblcountInsAdd3" style="background-color: #E2EEF1; color: Red; font-weight: bold;">200</label>
                                                        <asp:TextBox runat="server" ID="txtMofficeAdd" class="form-control" TextMode="MultiLine" placeholder="Enter Mother office Address" TabIndex="37" MaxLength="500" onkeyup="LimtCharactersInsAdd(this,200,'lblcountInsAdd3')"></asp:TextBox>
                                                    </div>

                                                    <div class="clearfix"></div>

                                                    <div class="col-md-3 col-sm-6 col-xs-12 form-group">
                                                        <label>City/Town :</label>
                                                        <asp:DropDownList runat="server" ID="ddlMofficeCity" AppendDataBoundItems="true" class="form-control" TabIndex="38">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-3 col-sm-6 col-xs-12 form-group">
                                                        <label>State :</label>
                                                        <asp:DropDownList runat="server" ID="ddlMofficeState" AppendDataBoundItems="true" class="form-control" TabIndex="39">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-3 col-sm-6 col-xs-12 form-group">
                                                        <label>Pincode :</label>
                                                        <asp:TextBox runat="server" ID="txtMofficePin" class="form-control" placeholder="Enter Pincode" MaxLength="6" TabIndex="40"></asp:TextBox>

                                                    </div>
                                                    <div class="col-md-3 col-sm-6 col-xs-12 form-group">
                                                        <label>STD code :</label>
                                                        <asp:TextBox runat="server" ID="txtMSTDCode" class="form-control" placeholder="Enter STD Code" MaxLength="12" TabIndex="41"></asp:TextBox>

                                                    </div>
                                                    <div class="col-md-3 col-sm-6 col-xs-12 form-group">
                                                        <label>Landline Number :</label>
                                                        <asp:TextBox runat="server" ID="txtMOfficeLandLine" class="form-control" placeholder="Enter Landline Number" MaxLength="12" TabIndex="42"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <hr>
                                            </div>

                                            <div class="col-md-12 col-xs-12">
                                                <h4 class="padd-tb-10 sub-heading">Guardian Details</h4>
                                                <div class="row">
                                                    <div class="col-md-4 col-sm-6 col-xs-12 form-group">
                                                        <label>Guardian Name :</label>

                                                        <asp:TextBox runat="server" ID="txtGuardianName" class="form-control onlycharacter" placeholder="Enter Guardian Name" MaxLength="20" TabIndex="43"></asp:TextBox>

                                                    </div>
                                                    <div class="col-md-4 col-sm-6 col-xs-12 form-group">
                                                        <label>Guardian Initial/Surname :</label>
                                                        <asp:TextBox runat="server" ID="txtGuardianInitial" class="form-control onlycharacter" placeholder="Enter Guardian Initial/Surname" MaxLength="20" TabIndex="44"></asp:TextBox>
                                                    </div>

                                                    <div class="col-md-4 col-sm-6 col-xs-12 form-group">
                                                        <label>Qualification :</label>
                                                        <asp:LinkButton ID="lbtnGOtherQualification" runat="server" ForeColor="Green" OnClick="lbtnGOtherQualification_Click"> Add Other Qualification </asp:LinkButton>
                                                        <asp:DropDownList runat="server" ID="ddlGQualification" AppendDataBoundItems="true" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlGQualification_SelectedIndexChanged" TabIndex="46">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:TextBox runat="server" ID="txtGOtherQualification" class="form-control" placeholder="Enter Qualification" Visible="false" MaxLength="20" TabIndex="47"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 col-sm-6 col-xs-12 form-group">
                                                        <label>Occupation :</label>
                                                        <asp:DropDownList runat="server" ID="ddlGOccupation" AppendDataBoundItems="true" class="form-control" TabIndex="48">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-4 col-sm-6 col-xs-12 form-group">
                                                        <label>Working Organization Name :</label>
                                                        <asp:TextBox runat="server" ID="txtGWorkingOrg" class="form-control" placeholder="Enter Working Organization Name" MaxLength="50" TabIndex="49"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 col-sm-6 col-xs-12 form-group">
                                                        <label>Designation :</label>
                                                        <asp:LinkButton ID="lbtnGOtherDesig" runat="server" ForeColor="Green" OnClick="lbtnGOtherDesig_Click"> Add Other Designation </asp:LinkButton>
                                                        <asp:DropDownList runat="server" ID="ddlGDesignation" AppendDataBoundItems="true" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlGDesignation_SelectedIndexChanged" TabIndex="51">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:TextBox runat="server" ID="txtGOtherDesignation" class="form-control" placeholder="Enter Designation" Visible="false" MaxLength="20" TabIndex="52"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-4 col-sm-6 col-xs-12 form-group">
                                                        <label>Annual Income :</label>
                                                        <asp:TextBox runat="server" ID="txtGAnnualIncome" class="form-control" placeholder="Enter Annual Income" MaxLength="7" TabIndex="53"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-4 col-xs-12 form-group hidden">
                                                        <label><sup></sup>Guardian Mobile Number :</label>
                                                        <asp:TextBox runat="server" ID="txtGMobileNo" class="form-control" placeholder="Enter Guardian Mobile Number" MaxLength="10" TabIndex="54"></asp:TextBox>
                                                    </div>

                                                </div>
                                            </div>

                                            <div class="col-md-12 col-xs-12">
                                                <h4 class="padd-tb-10">Guardian Office Address</h4>
                                                <div class="row">
                                                    <div class="col-sm-12 col-xs-12 form-group">
                                                        <label>Address :</label>
                                                        &nbsp;&nbsp;<label>Characters Limit : &nbsp</label>
                                                        <label id="lblcountInsAdd4" style="background-color: #E2EEF1; color: Red; font-weight: bold;">200</label>
                                                        <asp:TextBox runat="server" ID="txtGOfficeAddress" class="form-control" TextMode="MultiLine" placeholder="Enter Guardian office Address" TabIndex="55" MaxLength="500" onkeyup="LimtCharactersInsAdd(this,200,'lblcountInsAdd4')"></asp:TextBox>
                                                    </div>

                                                    <div class="clearfix"></div>

                                                    <div class="col-md-3 col-sm-6 col-xs-12 form-group">
                                                        <label>City/Town :</label>
                                                        <asp:DropDownList runat="server" ID="ddlGofficeCity" AppendDataBoundItems="true" class="form-control" TabIndex="56">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-3 col-sm-6 col-xs-12 form-group">
                                                        <label>State :</label>
                                                        <asp:DropDownList runat="server" ID="ddlGofficeState" AppendDataBoundItems="true" class="form-control" TabIndex="57">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-3 col-sm-6 col-xs-12 form-group">
                                                        <label>Pin Code :</label>
                                                        <asp:TextBox runat="server" ID="txtGofficePin" class="form-control" placeholder="Enter Pin Code" MaxLength="6" TabIndex="58"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3 col-sm-6 col-xs-12 form-group">
                                                        <label>STD Code :</label>

                                                        <asp:TextBox runat="server" ID="txtGofficeSTD" class="form-control" placeholder="Enter STD Code" MaxLength="12" TabIndex="59"></asp:TextBox>

                                                    </div>
                                                    <div class="col-md-3 col-sm-6 col-xs-12 form-group">
                                                        <label>Landline Number :</label>

                                                        <asp:TextBox runat="server" ID="txtGofficeLandline" class="form-control" placeholder="Enter Landline Number" MaxLength="12" TabIndex="60"></asp:TextBox>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="text-center padd-tb-10">
                                            <button type="button" class="btn btn-default" onclick="parent.location='#step-5'" tabindex="61">Previous</button>
                                            <%--<button type="button" class="btn btn-outline-info">Next</button>--%>
                                            <asp:Button runat="server" ID="btnSaveNext7" class="btn btn-outline-info" OnClick="btnSaveNext7_Click" OnClientClick="return step7Click_validation()" TabIndex="62" />
                                            <%--<asp:Button ID="btnReport" CssClass="btn btn-success" runat="server" Text="Print" OnClick="btnReport_Click" TabIndex="63" Visible="false" />--%>
                                            <div id="tab6Print" runat="server" visible="false" style="font-size: large;">
                                                <a href="PrintApplication.aspx">Print Application Form</a>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnSaveNext7" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                        <div id="step-7" class="">
                            <div class="container-fluid">
                                <asp:UpdatePanel runat="server" ID="UpdatePanel9">
                                    <ContentTemplate>

                                        <div class="row">
                                            <div class="col-md-12 col-sm-12">
                                                <br />
                                                <h4 class="padd-tb-10 sub-heading">Remark</h4>
                                                <div class="row">

                                                    <div class="col-md-3 col-sm-4 col-xs-12 form-group">
                                                        <label>Year :</label>
                                                        <asp:DropDownList runat="server" ID="ddlYear" AppendDataBoundItems="true" class="form-control" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>

                                                    <div class="col-md-3 col-sm-4 col-xs-12 form-group">
                                                        <label>Remark Type :</label>
                                                        <asp:DropDownList runat="server" ID="ddlRemarkType" AppendDataBoundItems="true" AutoPostBack="true" class="form-control" OnSelectedIndexChanged="ddlRemarkType_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>

                                                    <div class="col-md-6 col-sm-6 col-xs-12 form-group">
                                                        <label>Remark :</label>
                                                        &nbsp;&nbsp;<label>Characters Limit : &nbsp</label>
                                                        <label id="Label3" style="background-color: #E2EEF1; color: Red; font-weight: bold;">500</label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtRemark" Rows="3" Columns="80" placeholder="Enter Remark" TextMode="MultiLine" data-toggle="tooltip" data-placement="top"
                                                            oncopy="return false;" onpaste="return false;" oncut="return false;" onkeyup="LimtCharactersAllergy(this,500,'lblcount')"></asp:TextBox>
                                                    </div>

                                                    <div class="text-center padd-tb-10">
                                                        <button type="button" class="btn btn-default" onclick="parent.location='#step-6'" tabindex="61">Previous</button>
                                                        <asp:Button runat="server" ID="btnSaveNextRemark" class="btn btn-outline-info" Text="Save and Confirm" OnClick="btnSaveNextRemark_Click" TabIndex="62" />
                                                        <asp:Button ID="btnReport" CssClass="btn btn-success" runat="server" Text="Print" OnClick="btnReport_Click" TabIndex="63" Visible="false" />
                                                    </div>

                                                </div>
                                            </div>
                                        </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                        <div id="step-10" class="">
                            <div class="container-fluid">
                                <asp:UpdatePanel runat="server" ID="UpdatePanel6">
                                    <ContentTemplate>
                                        <%-- <h4 class="sub-heading">Additional Information</h4>--%>

                                        <div class="row padd-t-10" style="display: none">
                                            <div class="col-md-6 col-sm-6 col-xs-12 form-group">
                                                <label>Details of Relatives Studying in SVCE :</label>
                                                &nbsp;&nbsp;<label>Characters Limit : &nbsp</label>
                                                <label id="lblcountInsAdd5" style="background-color: #E2EEF1; color: Red; font-weight: bold;">250</label>
                                                <asp:TextBox runat="server" ID="txtRelativeDetails" TextMode="MultiLine" class="form-control" Columns="50" Rows="4" MaxLength="400" onkeyup="LimtCharactersInsAdd(this,200,'lblcountInsAdd5')"></asp:TextBox>
                                            </div>
                                            <div class="col-md-6 col-sm-6 col-xs-12 form-group">
                                                <label>I have chosen SVCE because of :</label>
                                                &nbsp;&nbsp;<label>Characters Limit : &nbsp</label>
                                                <label id="lblcountInsAdd6" style="background-color: #E2EEF1; color: Red; font-weight: bold;">250</label>
                                                <asp:TextBox runat="server" ID="txtReasonToChoose" TextMode="MultiLine" class="form-control" Columns="50" Rows="4" MaxLength="400" onkeyup="LimtCharactersInsAdd(this,200,'lblcountInsAdd6')"></asp:TextBox>
                                            </div>
                                        </div>

                                        <h4 class="sub-heading">Community Certificate details</h4>
                                        <div class="row padd-t-10">
                                            <div class="col-md-3 col-sm-4 col-xs-12 form-group">
                                                <label><sup></sup>Community Certificate Number :</label>

                                                <asp:TextBox runat="server" ID="txtCommunityCertNo" class="form-control" placeholder="Enter Community Certificate Number" MaxLength="20"></asp:TextBox>
                                            </div>

                                            <div class="col-md-3 col-sm-4 col-xs-12 form-group">
                                                <label><sup></sup>Issue Date :</label>
                                                <div class='input-group date' id='myDatepicker3'>
                                                    <asp:TextBox runat="server" ID="txtCommunityCertIssueDate" MaxLength="10" class="form-control" data-inputmask="'mask': '99/99/9999'"></asp:TextBox>

                                                    <span class="input-group-addon">
                                                        <span class="glyphicon glyphicon-calendar"></span>
                                                    </span>
                                                </div>
                                            </div>

                                            <div class="col-md-3 col-sm-4 col-xs-12 form-group">
                                                <label>Issuing Authority :</label>
                                                <asp:TextBox runat="server" ID="txtCommunityCertAuthority" class="form-control" MaxLength="50"></asp:TextBox>
                                            </div>
                                        </div>

                                        <h4 class="sub-heading">Minority Certificate details (if Minority)</h4>
                                        <div class="row padd-t-10">
                                            <div class="col-md-3 col-sm-4 col-xs-12 form-group">
                                                <label><sup></sup>Minority Certificate no :</label>
                                                <asp:TextBox runat="server" ID="txtMinorityCertificateNo" class="form-control" placeholder="Enter Minority Certificate no" MaxLength="15"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-4 col-xs-12 form-group">
                                                <label><sup></sup>Issue Date :</label>
                                                <div class='input-group date' id='pickMinorityCertIssueDate'>
                                                    <asp:TextBox runat="server" ID="txtMinorityCertIssueDate" MaxLength="10" class="form-control" data-inputmask="'mask': '99/99/9999'"></asp:TextBox>

                                                    <span class="input-group-addon">
                                                        <span class="glyphicon glyphicon-calendar"></span>
                                                    </span>
                                                </div>
                                            </div>

                                            <div class="col-md-3 col-sm-4 col-xs-12 form-group">
                                                <label>Issuing Authority :</label>
                                                <asp:TextBox runat="server" ID="txtMinorityCertAuthority" class="form-control" MaxLength="50"></asp:TextBox>

                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-6 col-xs-12">
                                                <h4 class="sub-heading">Transfer Certificate details</h4>
                                                <div class="row padd-t-10">
                                                    <div class="col-md-6 col-sm-6 col-xs-12 form-group">
                                                        <label><sup></sup>Transfer Certificate Number :</label>
                                                        <asp:TextBox runat="server" ID="txtTransferCertNo" class="form-control" placeholder="Enter Transfer Certificate Number" MaxLength="15"></asp:TextBox>

                                                    </div>
                                                    <div class="col-md-6 col-sm-6 col-xs-12 form-group">
                                                        <label><sup></sup>Transfer Certificate Date :</label>
                                                        <div class='input-group date' id='myDatepicker4'>
                                                            <asp:TextBox runat="server" ID="txtTransferCertDate" class="form-control" data-inputmask="'mask': '99/99/9999'"></asp:TextBox>
                                                            <span class="input-group-addon">
                                                                <span class="glyphicon glyphicon-calendar"></span>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6 col-xs-12">
                                                <h4 class="sub-heading">Conduct Certificate details</h4>
                                                <div class="row padd-t-10">
                                                    <div class="col-md-6 col-sm-6 col-xs-12 form-group">
                                                        <label><sup></sup>Conduct Certificate Number :</label>

                                                        <asp:TextBox runat="server" ID="txtConductCertNo" class="form-control" placeholder="Enter Conduct Certificate Number" MaxLength="15"></asp:TextBox>

                                                    </div>
                                                    <div class="col-md-6 col-sm-6 col-xs-12 form-group">
                                                        <label><sup></sup>Conduct Certificate Date :</label>
                                                        <div class='input-group date' id='myDatepicker5'>
                                                            <asp:TextBox runat="server" ID="txtConductCertDate" class="form-control" data-inputmask="'mask': '99/99/9999'"></asp:TextBox>
                                                            <span class="input-group-addon">
                                                                <span class="glyphicon glyphicon-calendar"></span>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 padd-t-10">
                                                <div class="row">
                                                    <div class="col-md-3 col-sm-6 col-xs-12 form-group">

                                                        <label><sup>*</sup> First Appearance Register Number(+2 / Diploma) :</label>

                                                        <asp:TextBox runat="server" ID="txtFirstAppearanceRegno" class="form-control onlyalphanum" placeholder="Enter Register Number" MaxLength="15"></asp:TextBox>

                                                    </div>
                                                    <div class="col-md-3 col-sm-6 col-xs-12 form-group">
                                                        <label><sup>*</sup> First Appearance Month &amp; Year(+2 / Diploma) :</label>
                                                        <div class='input-group date' id='myDatepicker6'>

                                                            <asp:TextBox runat="server" ID="txtFirstAppearanceYear" MaxLength="7" class="form-control" data-inputmask="'mask': '99/9999'" placeholder="MM/YYYY"></asp:TextBox>

                                                            <span class="input-group-addon">
                                                                <span class="glyphicon glyphicon-calendar"></span>
                                                            </span>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3 col-sm-6 col-xs-12 form-group">
                                                        <label>Second Appearance Register Number :</label>

                                                        <asp:TextBox runat="server" ID="txtSecondAppearanceNo" class="form-control onlyalphanum" placeholder="Enter Register Number" MaxLength="15"></asp:TextBox>

                                                    </div>
                                                    <div class="col-md-3 col-sm-6 col-xs-12 form-group">
                                                        <label>Second Appearance Month &amp; Year :</label>
                                                        <div class='input-group date' id='myDatepicker7'>

                                                            <asp:TextBox runat="server" ID="txtSecondAppearanceYear" MaxLength="7" class="form-control" data-inputmask="'mask': '99/9999'" placeholder="MM/YYYY"></asp:TextBox>

                                                            <span class="input-group-addon">
                                                                <span class="glyphicon glyphicon-calendar"></span>
                                                            </span>
                                                        </div>
                                                    </div>


                                                    <div class="col-md-3 col-sm-6 col-xs-12 form-group">

                                                        <label>Third Appearance Register Number :</label>

                                                        <asp:TextBox runat="server" ID="txtThirdAppearanceRegno" class="form-control onlyalphanum" placeholder="Enter Register Number" MaxLength="15"></asp:TextBox>

                                                    </div>
                                                    <div class="col-md-3 col-sm-6 col-xs-12 form-group">
                                                        <label>Third Appearance Month &amp; Year :</label>
                                                        <div class='input-group date' id='myDatepicker8'>

                                                            <asp:TextBox runat="server" ID="txtThirdAppearanceYear" MaxLength="7" class="form-control" data-inputmask="'mask': '99/9999'" placeholder="MM/YYYY"></asp:TextBox>

                                                            <span class="input-group-addon">
                                                                <span class="glyphicon glyphicon-calendar"></span>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="text-center padd-tb-10">
                                            <button type="button" class="btn btn-default" onclick="parent.location='#step-6'">Previous</button>
                                            <%--<button type="button" class="btn btn-outline-info">Next</button>--%>
                                            <asp:Button runat="server" ID="btnSaveNext8" class="btn btn-outline-info" Text="Save and Next" OnClick="btnSaveNext8_Click" OnClientClick="return step8Click_validation()" />
                                            <%--  <button type="button" class="btn btn-danger">Cancel</button>--%>
                                            <div id="tab7Print" runat="server" visible="false" style="font-size: la ge;">
                                                <a href="PrintApplication.aspx">Print Application Form</a>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                        </div>

                        <div id="step-8" class="">
                            <div class="container-fluid">
                                <asp:UpdatePanel runat="server" ID="UpdatePanel7">
                                    <ContentTemplate>
                                        <h4 class="padd-tb-10">Fee Concession (for TNEA)</h4>
                                        <div class="row">
                                            <div class="col-md-2 col-xs-6 form-group">
                                                <label><sup>*</sup> First Graduate :</label><br />

                                                <asp:RadioButtonList runat="server" ID="rbFirstGraduate" class="mylistofradiolists" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="2">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="col-md-3 col-sm-6 col-xs-12 form-group">
                                                <label><sup></sup>Furnish Certificate Number :</label>

                                                <asp:TextBox runat="server" ID="txtFirstGraduateCertNo" class="form-control" Enabled="false" placeholder="Enter Furnish Certificate No." MaxLength="15"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3 col-sm-6 col-xs-12 form-group">
                                                <label><sup></sup>Furnish Certificate Date :</label>
                                                <div class='input-group date' id='myDatepicker11'>

                                                    <asp:TextBox runat="server" ID="txtFirstGraduateCertDate" class="form-control" Enabled="false" MaxLength="10" data-inputmask="'mask': '99/99/9999'"></asp:TextBox>
                                                    <span class="input-group-addon">
                                                        <span class="glyphicon glyphicon-calendar"></span>
                                                    </span>
                                                </div>
                                            </div>
                                            <div class="col-md-3 col-sm-6 col-xs-12 form-group">
                                                <label><sup></sup>Furnish Certificate Issued by :</label>

                                                <asp:TextBox runat="server" ID="txtFirstGraduateCertAuth" class="form-control" Enabled="false" placeholder="Certificate Issued by" MaxLength="30"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2 col-sm-6 col-xs-12 form-group">
                                                <label><sup>*</sup> AICTE Fee Waiver :</label><br />

                                                <asp:RadioButtonList runat="server" ID="rbAICTEWaiver" class="mylistofradiolists" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="2">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="col-md-3 col-sm-6 col-xs-12 form-group">
                                                <label><sup></sup>Certificate Number :</label>

                                                <asp:TextBox runat="server" ID="txtAICTECertNo" class="form-control" Enabled="false" placeholder="Enter Certificate Number" MaxLength="15"></asp:TextBox>

                                            </div>
                                            <div class="col-md-3 col-sm-6 col-xs-12 form-group">
                                                <label><sup></sup>Certificate Date :</label>
                                                <div class='input-group date' id='myDatepicker12'>

                                                    <asp:TextBox runat="server" ID="txtAICTECertDate" class="form-control" Enabled="false" MaxLength="10" data-inputmask="'mask': '99/99/9999'"></asp:TextBox>

                                                    <span class="input-group-addon">
                                                        <span class="glyphicon glyphicon-calendar"></span>
                                                    </span>
                                                </div>
                                            </div>
                                            <div class="col-md-3 col-sm-6 col-xs-12 form-group">
                                                <label><sup></sup>Certificate Authority By :</label>

                                                <asp:TextBox runat="server" ID="txtAICTECertAuth" class="form-control" Enabled="false" placeholder="Enter Certificate Authority By" MaxLength="30"></asp:TextBox>

                                            </div>
                                            <div class="col-md-2 col-sm-6 col-xs-6 form-group">
                                                <label><sup>*</sup> Aadhi Dravidar Welfare :</label><br />

                                                <asp:RadioButtonList runat="server" ID="rbDravidarWelfare" class="mylistofradiolists" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="2">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="col-md-3 col-sm-6 col-xs-12 form-group">
                                                <label><sup></sup>Certificate Number :</label>

                                                <asp:TextBox runat="server" ID="txtWelfareCertNo" class="form-control" Enabled="false" placeholder="Enter Certificate Number" MaxLength="15"></asp:TextBox>

                                            </div>
                                            <div class="col-md-3 col-sm-6 col-xs-12 form-group">
                                                <label><sup></sup>Certificate Date :</label>
                                                <div class='input-group date' id='myDatepicker14'>

                                                    <asp:TextBox runat="server" ID="txtWelfareCertDate" class="form-control" Enabled="false" MaxLength="10" data-inputmask="'mask': '99/99/9999'"></asp:TextBox>

                                                    <span class="input-group-addon">
                                                        <span class="glyphicon glyphicon-calendar"></span>
                                                    </span>
                                                </div>
                                            </div>
                                            <div class="col-md-3 col-sm-6 col-xs-12 form-group">
                                                <label><sup></sup>Certificate Authority By :</label>

                                                <asp:TextBox runat="server" ID="txtWelfareCertAuth" class="form-control" Enabled="false" placeholder="Enter Certificate Authority By" MaxLength="30"></asp:TextBox>

                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="checkbox form-group">
                                                <style>
                                                    .checkbox.form-group label:before; {
                                                        border: 1px solid #0D9EDF;
                                                    }
                                                </style>
                                                <input type="checkbox" id="Checkbox1">
                                                <label for="Checkbox1"><b><code style="color: #0D9EDF">I here by declare that the above particulars furnished by me in this application are correct.</code></b></label>
                                            </div>
                                        </div>


                                        <div class="text-center padd-tb-10">
                                            <button type="button" class="btn btn-default" onclick="parent.location='#step-7'">Previous</button>
                                            <%--<button type="button" class="btn btn-outline-info">Next</button>--%>
                                            <asp:Button runat="server" ID="btnSaveNext9" class="btn btn-outline-info" Text="Save and Next" OnClick="btnSaveNext9_Click" OnClientClick="return step9Click_validation()" />
                                            <%--<asp:Button ID="btnReport" CssClass="btn btn-success" runat="server" Visible="false" Text="Print" OnClick="btnReport_Click" />--%>
                                            <%--  <button type="button" class="btn btn-danger">Cancel</button>--%>
                                            <div id="tab8Print" runat="server" visible="false" style="font-size: large;">
                                                <a href="PrintApplication.aspx">Print Application Form</a>
                                            </div>
                                        </div>

                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnReport" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                        <%-- Sports and Achievement--%>
                        <div id="step-9" class="">
                            <div class="container-fluid">
                                <asp:UpdatePanel runat="server" ID="updSports">
                                    <ContentTemplate>
                                        <h4 class="padd-tb-10">Sports And Achievement</h4>


                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-5 form-group">
                                                </div>
                                                <div class="col-md-4 form-group">
                                                    <%-- <label><sup>*</sup> Please Select :</label><br />--%>

                                                    <asp:RadioButtonList runat="server" ID="rdoType" class="mylistofradiolists" RepeatDirection="Horizontal"
                                                        OnSelectedIndexChanged="rdoType_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Value="1">Sports</asp:ListItem>
                                                        <%-- <asp:ListItem Value="2">Achievement</asp:ListItem>--%>
                                                    </asp:RadioButtonList>
                                                </div>
                                                <div class="col-md-4 form-group">
                                                </div>
                                            </div>
                                        </div>

                                        <br />


                                        <div class="row" id="divSportsDetails" runat="server" visible="false">

                                            <div class="col-md-6 col-sm-6 col-xs-12 form-group">
                                                <label><sup>*</sup><asp:Label ID="lblNameOf" runat="server"></asp:Label></label>
                                                <asp:TextBox runat="server" ID="txtNameOfGameOrAchievement" class="form-control" placeholder="Enter Name of Game / Achievement"
                                                    onkeypress="return isAlphabetKey(event)" MaxLength="15"></asp:TextBox>

                                            </div>

                                            <div class="col-md-6 col-sm-6 col-xs-12 form-group">
                                                <label><sup>*</sup><asp:Label ID="lblDate" runat="server"></asp:Label></label>
                                                <div class='input-group date' id='SportsDate'>

                                                    <asp:TextBox runat="server" ID="txtDate" class="form-control" MaxLength="10" data-inputmask="'mask': '99/99/9999'"></asp:TextBox>


                                                    <span class="input-group-addon">
                                                        <span class="glyphicon glyphicon-calendar"></span>
                                                    </span>

                                                </div>
                                            </div>


                                            <div class="col-md-6 col-sm-6 col-xs-12 form-group">
                                                <label><sup>*</sup><asp:Label ID="lblVenue" runat="server" Text="Venue"></asp:Label></label>
                                                <asp:TextBox runat="server" ID="txtVenue" class="form-control" placeholder="Enter Venue" MaxLength="30"></asp:TextBox>

                                            </div>

                                            <div class="col-md-6 col-sm-6 col-xs-12 form-group">
                                                <label><sup>*</sup> Level Of Participation :</label><br />
                                                <asp:DropDownList runat="server" ID="ddlLevelOfParticipation" AppendDataBoundItems="true"
                                                    class="form-control">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>

                                            </div>

                                            <div class="col-md-6 col-sm-6 col-xs-12 form-group">
                                                <label><sup>*</sup>Achievement :</label>
                                                <asp:DropDownList runat="server" ID="ddlAchievement" AppendDataBoundItems="true"
                                                    class="form-control">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>

                                            </div>


                                            <div class="col-md-6 col-sm-6 col-xs-12 form-group">
                                                <label for="city">Sports Certificate <span style="color: red; font-size: 10px;">(Only .jpg, .jpeg, .png , .pdf format with max 50 KB size are allowed)</span></label>
                                                <asp:FileUpload ID="fuSportsCertificate" runat="server" ToolTip="Select file to upload"
                                                    CssClass="form-control" accept=".jpg,.jpeg,.pdf,.JPG,.JPEG,.PNG,.pdf,.PDF" />

                                                <br />


                                                <asp:Panel ID="Panel3" runat="server" ScrollBars="Auto">
                                                    <asp:Repeater ID="lvSportsCertificate" runat="server">
                                                        <HeaderTemplate>
                                                            <%-- <div id="demo-grid">
                                                            <h4 class="sub-heading">Sports Certificate</h4>
                                                        </div>--%>
                                                            <table class="table table-bordered table-hover table-fixed">
                                                                <thead>
                                                                    <tr class="bg-light blue">
                                                                        <th>Sports Name
                                                                        </th>
                                                                        <th>Certificate Name
                                                                        </th>
                                                                        <th></th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>

                                                                    <asp:Label ID="lblName" runat="server" Text='<%#Eval("SPORTS_NAME") %>' ToolTip='<%#Eval("IDNO") %>'></asp:Label>
                                                                    <%-- <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" CommandArgument='<%#Eval("COURSENO") %>' ToolTip='<%# Eval("FILE_NAME") %>'
                                                                    OnClick="btnDelete_Click" />--%>
                                                                </td>
                                                                <td>
                                                                    <%#Eval("SPORTS_FILE_NAME") %>
                                                                </td>
                                                                <td>
                                                                    <asp:LinkButton ID="btnDownload" runat="server" Text="Download" CommandArgument='<%#Eval("SPORTS_FILE_NAME") %>'
                                                                        OnClick="btnDownload_Click"></asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            </tbody> </table>
                                                        </FooterTemplate>
                                                    </asp:Repeater>
                                                </asp:Panel>

                                            </div>

                                        </div>


                                        <div class="text-center padd-tb-10" id="divSportsBtnDetails" runat="server" visible="false">

                                            <button type="button" class="btn btn-default" onclick="parent.location='#step-8'">Previous</button>
                                            <%--<button type="button" class="btn btn-outline-info">Next</button>--%>
                                            <asp:Button runat="server" ID="btnSaveNext10" class="btn btn-outline-info" Text="Save and Confirm" OnClick="btnSaveNext10_Click"
                                                ValidationGroup="Sports" OnClientClick="return step10Click_validation()" />
                                            <%--   <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True"
                                                    ShowSummary="False" ValidationGroup="Sports" />--%>
                                            <%-- <asp:Button ID="Button3" CssClass="btn btn-success" runat="server" Visible="false" Text="Print" OnClick="btnReport_Click" />
                                     
                                            <div id="Div5" runat="server" visible="false" style="font-size: large;">
                                                <a href="PrintApplication.aspx">Print Application Form</a>
                                            </div>--%>
                                        </div>



                                        <div class="container-fluid">
                                            <asp:ListView ID="lvSportsDetails" runat="server">
                                                <LayoutTemplate>
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <h3>Student Sports Details</h3>
                                                        </div>

                                                    </div>
                                                    <div class="table-responsive">
                                                        <table id="mylist" class="table table-hover table-bordered datatable">
                                                            <tr class="bg-light-blue">
                                                                <th>Action</th>
                                                                <th style="text-align: center">Sr No.
                                                                </th>
                                                                <th style="text-align: center">Sports Name
                                                                </th>
                                                                <th>Sports Date
                                                                </th>
                                                                <th style="text-align: center">Venue
                                                                </th>
                                                                <th style="text-align: center">Participation Level
                                                                </th>
                                                                <th style="text-align: center">Achievement
                                                                </th>
                                                                <th style="text-align: center">FileName
                                                                </th>
                                                            </tr>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </table>
                                                    </div>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td style="text-align: center">
                                                            <asp:ImageButton ID="btnSportsEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("SPORTS_NO") %>'
                                                                AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnSportsEdit_Click" />
                                                        </td>

                                                        <td style="text-align: center">
                                                            <%#Container.DataItemIndex+1%>
                                                        </td>

                                                        <td style="text-align: center">
                                                            <%# Eval("SPORTS_NAME")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("SPORTS_DATE")%>
                                                        </td>
                                                        <td style="text-align: center">
                                                            <%# Eval("SPORTS_VENUE")%>
                                                        </td>
                                                        <td style="text-align: center">
                                                            <%# Eval("PARTICIPATION_LEVEL")%>
                                                        </td>
                                                        <td style="text-align: center">
                                                            <%# Eval("ACHIEVEMENT_NAME")%>
                                                        </td>
                                                        <td style="text-align: center">
                                                            <%# Eval("SPORTS_FILE_NAME")%>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </div>

                                    </ContentTemplate>

                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="lvSportsCertificate" />
                                        <asp:PostBackTrigger ControlID="btnSaveNext10" />
                                        <%--  <asp:AsyncPostBackTrigger ControlID="btnSaveNext10" EventName="Click"/>--%>
                                        <%--  <asp:PostBackTrigger ControlID="lvSportsDetails" />--%>
                                    </Triggers>

                                </asp:UpdatePanel>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- /page content -->

    <div id="myModal2" class="modal fade" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Search</h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="updEdit" runat="server">
                        <ContentTemplate>
                            <div class="form-group col-md-12">
                                <label>Search Criteria</label>
                                <br />
                                <asp:RadioButton ID="rbName" runat="server" Text="Name" GroupName="edit" />
                                <asp:RadioButton ID="rbIdNo" runat="server" Text="IdNo" GroupName="edit" />
                                <asp:RadioButton ID="rbTan" runat="server" Text="TAN" GroupName="edit" />
                                <asp:RadioButton ID="rbEnrollmentNo" runat="server" Text="PAN" GroupName="edit" />
                                <asp:RadioButton ID="rbRegNo" runat="server" Text="Univ. Reg. No." GroupName="edit" Checked="True" />
                            </div>
                            <div class="form-group col-md-12">
                                <div class="col-md-12">
                                    <label>Search String</label>
                                    <asp:TextBox ID="txtSearch" runat="server" class="form-control"></asp:TextBox>
                                </div>

                            </div>
                            <div class="col-md-12">
                                <p class="text-center">
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClientClick="return submitPopup(this.name);" CssClass="btn btn-outline-info" />
                                    <asp:Button ID="btnCancelModal" runat="server" Text="Cancel" OnClientClick="return ClearSearchBox(this.name)" CssClass="btn btn-outline-danger" />
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                </p>
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
                                    <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
                                        <asp:ListView ID="lvStudent" runat="server">
                                            <LayoutTemplate>
                                                <div>
                                                    <h4 class="sub-heading">Login Details</h4>
                                                    <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto" Height="300px">
                                                        <table class="table table-hover table-bordered">
                                                            <thead>
                                                                <tr class="bg-light-blue">
                                                                    <th>Name
                                                                    </th>
                                                                    <th>IdNo
                                                                    </th>
                                                                    <th>TAN/PAN
                                                                    </th>
                                                                    <th>Branch
                                                                    </th>
                                                                    <th>Semester/Year
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </tbody>
                                                    </asp:Panel>
                                                </div>

                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:LinkButton ID="lnkId" runat="server" Text='<%# Eval("Name") %>' CommandArgument='<%# Eval("IDNo") %>'
                                                            OnClick="lnkId_Click"></asp:LinkButton>
                                                    </td>
                                                    <td>
                                                        <%# Eval("idno")%>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblstuenrollno" runat="server" Text='<%# Eval("EnrollmentNo")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>

                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblstudentfullname" runat="server" Text='<%# Eval("longname")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>

                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblYearSem" runat="server" Text='<%# Eval("YEARWISE").ToString()=="1" ? Eval("YEARNAME") : Eval("SEMESTERNAME")%>' ToolTip='<%# Eval("IDNO")%>'></asp:Label>
                                                        <%-- <%# Eval("YEARSEM")%>--%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                </div>
            </div>

        </div>
    </div>


    <div class="modal fade" id="myModal1" role="dialog">
        <div class="modal-dialog" style="width: 30%">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                </div>
                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                    <ContentTemplate>
                        <div class="box-body modal-warning">
                            <div class="form-group" style="text-align: center">
                                <asp:Label ID="lblmessageShow" Style="font-weight: bold" runat="server" Text="Regno"></asp:Label>
                            </div>
                            <div class="box-footer">
                                <p class="text-center">
                                    <asp:Button ID="Button1" runat="server" Text="Close" CssClass="btn btn-default" data-dismiss="modal" />
                                </p>
                            </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <script src="../plugins/studInfo/js/admin-custom.js"></script>
    <script src="../plugins/studInfo/js/bootstrap.min.js"></script>
    <script src="../plugins/studInfo/js/admin.js"></script>

    <script src="../plugins/studInfo/js/jquery.smartWizard.min.js"></script>
    <!--   Calendar   -->
    <script src="../plugins/studInfo/plugin/calendar/moment.min.js"></script>
    <script src="../plugins/studInfo/plugin/calendar/bootstrap-datetimepicker.min.js"></script>
    <script src="../plugins/studInfo/plugin/inputmask/jquery.inputmask.bundle.min.js"></script>
    <!-- Bootstrap -->

    <script language="Javascript" type="text/javascript">
        function isAlphabetKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if ((charCode <= 93 && charCode >= 65) || (charCode <= 122 && charCode >= 97)) {

                return true;
            }
            alert("Only (A-Z or a-z) alphabets or Characters Allowed");
            return false;

        }
    </script>


    <%-- script for copy local address to permanent address--%>
    <script type="text/javascript">
        function copyLocalAddr(chk) {
            if (chk.checked) {
                document.getElementById('<%= txtPAddressLine1.ClientID %>').value = document.getElementById('<%= txtLAddressLine1.ClientID %>').value;
                document.getElementById('<%=ddlPCity.ClientID%>').value = document.getElementById('<%=ddlLcity.ClientID%>').value;
                document.getElementById('<%=ddlPState.ClientID%>').value = document.getElementById('<%=ddlLState.ClientID%>').value;
                document.getElementById('<%=txtPPin.ClientID%>').value = document.getElementById('<%=txtLPin.ClientID%>').value;
                document.getElementById('<%=txtPSTDCode.ClientID%>').value = document.getElementById('<%=txtLSTDCode.ClientID%>').value;
                document.getElementById('<%=txtPLandLine.ClientID%>').value = document.getElementById('<%=txtLLandLineNo.ClientID%>').value;


            }
            else {
                document.getElementById('<%= txtPAddressLine1.ClientID %>').value = '';
                document.getElementById('<%=ddlPCity.ClientID%>').value = '0';
                document.getElementById('<%=ddlPState.ClientID%>').value = '0';
                document.getElementById('<%=txtPPin.ClientID%>').value = '';
                document.getElementById('<%=txtPSTDCode.ClientID%>').value = '';
                document.getElementById('<%=txtPLandLine.ClientID%>').value = '';

            }
        }

        function copyLocalAddrtoG(chk) {
            if (chk.checked) {
                document.getElementById('<%= txtGaddressLine1.ClientID %>').value = document.getElementById('<%= txtLAddressLine1.ClientID %>').value;
                document.getElementById('<%=ddlGCity.ClientID%>').value = document.getElementById('<%=ddlLcity.ClientID%>').value;
                document.getElementById('<%=ddlGState.ClientID%>').value = document.getElementById('<%=ddlLState.ClientID%>').value;
                document.getElementById('<%=txtGPin.ClientID%>').value = document.getElementById('<%=txtLPin.ClientID%>').value;
                document.getElementById('<%=txtGSTDCode.ClientID%>').value = document.getElementById('<%=txtLSTDCode.ClientID%>').value;
                document.getElementById('<%=txtGLandLineNumber.ClientID%>').value = document.getElementById('<%=txtLLandLineNo.ClientID%>').value;


            }
            else {
                document.getElementById('<%= txtGaddressLine1.ClientID %>').value = '';
                document.getElementById('<%=ddlGCity.ClientID%>').value = '0';
                document.getElementById('<%=ddlGState.ClientID%>').value = '0';
                document.getElementById('<%=txtGPin.ClientID%>').value = '';
                document.getElementById('<%=txtGSTDCode.ClientID%>').value = '';
                document.getElementById('<%=txtGLandLineNumber.ClientID%>').value = '';

            }
        }
    </script>
    <%-- script for sign preview--%>

    <script>
        //$(document).ready(function () {

        //    bindDataTable1();// for fileupload control change event work after postback
        //    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable1);

        //});


        function showpreviewsign(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {

                    $('#<%= imgpreviewsign.ClientID%>').attr('src', e.target.result);

                }
                reader.readAsDataURL(input.files[0]);
                }
            }

            //function bindDataTable1() {
            $("#<%= fuStudentSign.ClientID%>").change(function () {
            //check file extension
            var fileExtension = ['jpeg', 'jpg', 'png'];
            if ($.inArray($(this).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
                alert("Only file formats are allowed : " + fileExtension.join(', '));
                $("#<%= fuStudentSign.ClientID%>").val("");
                return;

            }
                //check file file size
                var input = this;
                if (input.files && input.files[0]) {
                    var type = input.files[0].type;
                    var type_reg = /^image\/(jpg|png|jpeg)$/;
                    if (type_reg.test(type)) {
                        if (input.files[0].size > 51200) {

                            alert("Max size upto 50 Kb allowed.");
                            $("#<%= fuStudentSign.ClientID%>").val("");
                            //$('#Image_Profile').removeAttr('src');
                            return;
                        }
                        else {
                            showpreviewsign(this);
                        }
                    }
                }

                var prm = Sys.WebForms.PageRequestManager.getInstance();
                prm.add_endRequest(function () {


                    var fileExtension = ['jpeg', 'jpg', 'png'];
                    if ($.inArray($(this).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
                        alert("Only file formats are allowed : " + fileExtension.join(', '));
                        $("#<%= fuStudentSign.ClientID%>").val("");
                        return;

                    }
                    //check file file size
                    var input = this;
                    if (input.files && input.files[0]) {
                        var type = input.files[0].type;
                        var type_reg = /^image\/(jpg|png|jpeg)$/;
                        if (type_reg.test(type)) {
                            if (input.files[0].size > 51200) {

                                alert("Max size upto 50 Kb allowed.");
                                $("#<%= fuStudentSign.ClientID%>").val("");
                                //$('#Image_Profile').removeAttr('src');
                                return;
                            }
                            else {
                                showpreviewsign(this);
                            }
                        }
                    }


                });


            });


            //}
    </script>
    <%--script for photo preview--%>
    <script>
        //$(document).ready(function () {

        //    bindDataTable();// for fileupload control change event work after postback
        //    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);

        //});


        function showpreview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {

                    $('#<%= imgpreview.ClientID%>').attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
                }
            }



            $("#<%= fuStudentPhoto1.ClientID%>").change(function () {
            //check file extension
            var fileExtension = ['jpeg', 'jpg', 'png'];
            if ($.inArray($(this).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
                alert("Only file formats are allowed : " + fileExtension.join(', '));
                $("#<%= fuStudentPhoto1.ClientID%>").val("");
                return;

            }
                //check file file size
                var input = this;
                if (input.files && input.files[0]) {
                    var type = input.files[0].type;
                    var type_reg = /^image\/(jpg|png|jpeg)$/;
                    if (type_reg.test(type)) {
                        if (input.files[0].size > 51200) {

                            alert("Max size upto 50 Kb allowed.");
                            $("#<%= fuStudentPhoto1.ClientID%>").val("");
                            //$('#Image_Profile').removeAttr('src');
                            return;
                        }
                        else {
                            showpreview(this);
                        }
                    }
                }


                var prm = Sys.WebForms.PageRequestManager.getInstance();
                prm.add_endRequest(function () {

                    //check file extension
                    var fileExtension = ['jpeg', 'jpg', 'png'];
                    if ($.inArray($(this).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
                        alert("Only file formats are allowed : " + fileExtension.join(', '));
                        $("#<%= fuStudentPhoto1.ClientID%>").val("");
                        return;

                    }
                    //check file file size
                    var input = this;
                    if (input.files && input.files[0]) {
                        var type = input.files[0].type;
                        var type_reg = /^image\/(jpg|png|jpeg)$/;
                        if (type_reg.test(type)) {
                            if (input.files[0].size > 51200) {

                                alert("Max size upto 50 Kb allowed.");
                                $("#<%= fuStudentPhoto1.ClientID%>").val("");
                                //$('#Image_Profile').removeAttr('src');
                                return;
                            }
                            else {
                                showpreview(this);
                            }
                        }
                    }

                });


            });



    </script>

    <script>

        function LimtCharactersAllergy(txtAlergyDetail, CharLength, indicator) {
            chars = txtAlergyDetail.value.length;
            document.getElementById(indicator).innerHTML = CharLength - chars;
            if (document.getElementById(indicator).innerHTML < 0) {
                document.getElementById(indicator).innerHTML = 0;
            }
            if (chars > CharLength) {
                txtAlergyDetail.value = txtAlergyDetail.value.substring(0, CharLength);
            }
        }
    </script>

    <script>
        function LimtCharactersLAdd(txtLAddressLine1, CharLength, indicator) {
            chars = txtLAddressLine1.value.length;
            document.getElementById(indicator).innerHTML = CharLength - chars;
            if (document.getElementById(indicator).innerHTML < 0) {
                document.getElementById(indicator).innerHTML = 0;
            }
            if (chars > CharLength) {
                txtLAddressLine1.value = txtLAddressLine1.value.substring(0, CharLength);
            }
        }

    </script>

    <script>
        function LimtCharactersPAdd(txtPAddressLine1, CharLength, indicator) {
            chars = txtPAddressLine1.value.length;
            document.getElementById(indicator).innerHTML = CharLength - chars;
            if (document.getElementById(indicator).innerHTML < 0) {
                document.getElementById(indicator).innerHTML = 0;
            }
            if (chars > CharLength) {
                txtPAddressLine1.value = txtPAddressLine1.value.substring(0, CharLength);
            }
        }

    </script>
    <script>
        function LimtCharactersGAdd(txtGaddressLine1, CharLength, indicator) {
            chars = txtGaddressLine1.value.length;
            document.getElementById(indicator).innerHTML = CharLength - chars;
            if (document.getElementById(indicator).innerHTML < 0) {
                document.getElementById(indicator).innerHTML = 0;
            }
            if (chars > CharLength) {
                txtGaddressLine1.value = txtGaddressLine1.value.substring(0, CharLength);
            }
        }

    </script>
    <script>
        function LimtCharactersInsAdd(txtSSCInstituteAddress, CharLength, indicator) {
            chars = txtSSCInstituteAddress.value.length;
            document.getElementById(indicator).innerHTML = CharLength - chars;
            if (document.getElementById(indicator).innerHTML < 0) {
                document.getElementById(indicator).innerHTML = 0;
            }
            if (chars > CharLength) {
                txtSSCInstituteAddress.value = txtSSCInstituteAddress.value.substring(0, CharLength);
            }
        }

    </script>

    <script type="text/javascript" language="javascript">

        function submitPopup(btnsearch) {

            var rbText;
            var searchtxt;
            if (document.getElementById('<%=rbName.ClientID %>').checked == true)
                rbText = "name";
            else if (document.getElementById('<%=rbIdNo.ClientID %>').checked == true)
                rbText = "idno";
            else if (document.getElementById('<%=rbTan.ClientID %>').checked == true)
                rbText = "tanno";
            else if (document.getElementById('<%=rbEnrollmentNo.ClientID %>').checked == true)
                rbText = "ENROLLMENTNO";
            else if (document.getElementById('<%=rbRegNo.ClientID %>').checked == true)
                rbText = "regno";


    searchtxt = document.getElementById('<%=txtSearch.ClientID %>').value;

            __doPostBack(btnsearch, rbText + ',' + searchtxt);

            return true;
        }

        function ClearSearchBox(btncancelmodal) {
            document.getElementById('<%=txtSearch.ClientID %>').value = '';
            __doPostBack(btncancelmodal, '');
            return true;
        }
    </script>


    <script type="text/javascript">

        $(document).ready(function () {

            bindDataTable1();// for fileupload control change event work after postback
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable1);

        });

        function bindDataTable1() {
            $('#myDatepicker2, #myDatepicker3, #myDatepicker4, #myDatepicker5,#myDatepicker9,#myDatepicker10,#myDatepicker11,#myDatepicker12,#myDatepicker14,#pickMinorityCertIssueDate,#dateAllotmentOrderDt,#SportsDate').datetimepicker({
                format: 'DD/MM/YYYY',
                maxDate: moment(),
                useCurrent: false
            });

            $('#myDatepicker8, #myDatepicker7, #myDatepicker6, #myDatepicker15,#myDatepicker16,#datePickerDiplomaYr').datetimepicker({
                format: 'MM/YYYY',

            });
        }


        $('#myDatepicker2, #myDatepicker3, #myDatepicker4, #myDatepicker5,#myDatepicker9,#myDatepicker10,#myDatepicker11,#myDatepicker12,#myDatepicker14,#pickMinorityCertIssueDate,#dateAllotmentOrderDt,#SportsDate').datetimepicker({
            format: 'DD/MM/YYYY',
            maxDate: moment(),
            useCurrent: false
        });

        $('#myDatepicker8, #myDatepicker7, #myDatepicker6, #myDatepicker15,#myDatepicker16,#datePickerDiplomaYr').datetimepicker({
            format: 'MM/YYYY'
        });



    </script>


    <script>

        /* INPUT MASK */

        $(document).ready(function () {

            init_InputMask();// for fileupload control change event work after postback
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(init_InputMask);

        });



        function init_InputMask() {

            if (typeof ($.fn.inputmask) === 'undefined') { return; }
            console.log('init_InputMask');

            $(":input").inputmask();

        };
    </script>

    <!-- Loader Script -->
    <script type="text/javascript">
        $(document).ready(function () {
            $(".loader-area").fadeOut('slow');
        }, 2000);
    </script>

    <script language="JavaScript">
        function OnlyNumeric(evt) {
            var e = event || evt;
            var charCode = e.which || e.keyCode;
            if ((charCode >= 48) && (charCode <= 57))
                return true;
            return false;
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.upper').keyup(function () {
                $(this).val($(this).val().toUpperCase());
            });
        });

        $(function () {
            $('.onlycharacter').keydown(function (e) {
                if (e.ctrlKey || e.altKey) {
                    e.preventDefault();
                } else {
                    var key = e.keyCode;
                    if (!((key == 8) || (key == 9) || (key == 32) || (key == 46) || (key >= 35 && key <= 40) || (key >= 65 && key <= 90))) {
                        e.preventDefault();
                    }
                }
            });
        });

    </script>

    <script type="text/javascript">
        $(function () {
            $('.onlyalphanum').keydown(function (e) {
                debugger;
                if (e.shiftKey || e.ctrlKey || e.altKey) {
                    e.preventDefault();
                } else {
                    var key = e.keyCode;
                    if (!((key == 8) || (key == 32) || (key == 46) || (key >= 48 && key <= 57) || ((key >= 65 && key <= 90) || (key >= 96 && key <= 105)))) {
                        e.preventDefault();
                    }
                }
            });
        });
    </script>

    <script language="JavaScript">
        function AlphaNumeric(evt) {
            debugger;
            var e = event || evt;
            var charCode = e.which || e.keyCode;
            if ((charCode >= 48) && (charCode <= 57) || ((charCode >= 65) && (charCode <= 90)) || ((charCode >= 97) && (charCode <= 122))) {
                evt.value = evt.value.toUpperCase();
            }
            else {
                return false;
            }
        }
    </script>


    <div id="divMsg" runat="server">
    </div>
    <%--  bottom--%>
</asp:Content>
