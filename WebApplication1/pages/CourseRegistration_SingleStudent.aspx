<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="CourseRegistration_SingleStudent.aspx.cs" Inherits="ACADEMIC_CourseRegistration_SingleStudent"
    Title="" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="z-index: 1; position: absolute; top: 50%; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <%--<script src="../jquery/jquery-1.8.3.js" type="text/javascript"></script>

    <script src="../jquery/jquery-1.10.2.min.js" type="text/javascript"></script>

    <script src="../Content/jquery.js" type="text/javascript"></script>Degree/Branch

    <script src="../Content/jquery.dataTables.js" lang="javascript" type="text/javascript"></script>--%>

    <!-- jQuery 2.2.0 -->
    <script src="plugins/jQuery/jQuery-2.2.0.min.js"></script>
    <script src="plugins/jQuery/jquery.min.js"></script>
    <script src="bootstrap/js/bootstrap.min.js"></script>

    <!-- jQuery UI 1.11.4 -->

    <script src="plugins/jQuery/jquery_ui_min/jquery-ui.min.js"></script>

    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <!-- jQuery 2.2.0 -->
    <script src="plugins/jQuery/jQuery-2.2.0.min.js"></script>
    <script src="bootstrap/js/bootstrap.min.js"></script>

    <link rel="stylesheet" href="bootstrap/font-awesome-4.6.3/css/font-awesome.min.css" />
    <link href="dist/css/AdminLTE.min.css" rel="stylesheet" />
    <script src="dist/js/app.min.js"></script>
    <!-- DataTables -->
    <link rel="stylesheet" href="plugins/datatables/dataTables.bootstrap.css">
    <script src="plugins/datatables/jquery.dataTables.min.js"></script>
    <script src="plugins/datatables/dataTables.bootstrap.min.js"></script>

    <script src="plugins/slimScroll/jquery.slimscroll.min.js"></script>
    <script src="plugins/fastclick/fastclick.min.js"></script>

    <script type="text/javascript" charset="utf-8">
        $(document).ready(function () {
            $(".display").dataTable({
                "bJQueryUI": true,
                "sPaginationType": "full_numbers"
            });

        });
    </script>

    <script type="text/javascript">
        setTimeout(function () { f1.submit(); });
        $(document).bind("contextmenu", function (e) {
            e.preventDefault();
        });

        document.onmousedown = disableclick;
        status = "Right Click Disabled";

        $(document).keydown(function (e) {
            if (e.which === 123) {
                return false;
            }
        });
    </script>

    <script type="text/javascript">

        document.onkeydown = function (e) {
            if (e.keyCode == 123) {
                return false;
            }
            if (e.ctrlKey && e.shiftKey && e.keyCode == 'I'.charCodeAt(0)) {
                return false;
            }
            if (e.ctrlKey && e.shiftKey && e.keyCode == 'J'.charCodeAt(0)) {
                return false;
            }
            if (e.ctrlKey && e.keyCode == 'U'.charCodeAt(0)) {
                return false;
            }

            if (e.ctrlKey && e.shiftKey && e.keyCode == 'C'.charCodeAt(0)) {
                return false;
            }
        }
        document.body.onclick = disableclick()

    </script>

    <asp:UpdatePanel ID="updDiv" runat="server">
        <ContentTemplate>

            <div class="row">
                <!--academic Calendar-->
                <%--  <div class="col-md-12">--%>

                <div class="box box-info">
                    <div class="box-header with-border">
                        <h3 class="box-title">Subject Registration(CBCS)</h3>
                        <%--<span class="pull-right" id="btnRedirect" runat="server" visible="false"><a class="btn btn-success" href="FacultyAdvisorList.aspx?pageno=760" style="right: 0">Assign Students List</a></span>--%>
                    </div>

                    <div class="box-body" runat="server" id="divbody" visible="false">
                        <div class="col-md-12" runat="server" id="divSemPromotion">

                            <%--  ===================   --%>
                            <div class="box-header with-border">
                               <%-- <h3 class="box-title">Subject Registration(CBCS)</h3>--%>
                                <marquee id="mrqSession" runat="server" style="height: 22px; text-align: center; color: Red; font-size: medium; font-weight: bold"
                                    behavior="alternate" scrollamount="21"
                                    scrolldelay="500"></marquee>

                                <div id="divNote" runat="server" visible="true" style="border: 2px solid #C0C0C0; background-color: #FFFFCC; padding: 20px; color: #990000;">
                                    <b>Note : </b>Steps to follow for Subjects Registration.
        <div style="padding-left: 20px; padding-right: 20px">
            <p style="padding-top: 5px; padding-bottom: 5px;">
                <b>Core Courses Registration</b><br />
                1. Students are allowed to Select section [A/B/C], if more than one section available else he/she have to select the available section.<br />
                2. Maximum 70 [subject to be changed] can register per Section.<br />
                3. The allotment is based on first come first served basis.
            </p>
            <p style="padding-top: 5px; padding-bottom: 5px;">
                <b>Elective Courses</b><br />
                1. Students Can Select Course[s] from respective elective buckets.<br />
                2. The allotment is based on first come first served basis.<br />
                3. Maximum 70 [subject to be changed] can register per Staff<br />
                4. If Minimum no of students not registered for a elective the registered students may be asked to select one of the offered electives.
            </p>
            <p style="padding-top: 5px; padding-bottom: 5px;">
                <b>Movable Courses</b><br />
                1. Student can skip or add these courses.<br />
                2. Maximum 2 courses can be allowed to skip or add<br />
                3. Student can either skip or add courses but not for both operations are allowed.
            </p>
            <p style="padding-top: 5px; padding-bottom: 5px;">
                <b>Open elective</b><br />
                1. Will be allowed to register if it specified in curriculum.<br />
                2. Student can register Courses offered by other departments not from his/her own department.<br />
            </p>
            <p style="padding-top: 5px; padding-bottom: 5px;">
                1. Click on <b>Proceed </b>Button to <b>Subject Registration</b>
            </p>
            <p style="padding-top: 5px; padding-bottom: 5px; text-align: center;">
                <%-- <p class="text-center">--%>
                <asp:LinkButton ID="lnkSemPromotion" runat="server" OnClick="lnkSemPromotion_Click" CssClass="btn btn-outline-info"> Proceed <i class="fa fa-chevron-right"></i></asp:LinkButton>
                <%-- </p>--%>
            </p>
        </div>
                                </div>
                                <div class="box-tools pull-right">
                                </div>
                            </div>

                            <%--  ===================   --%>
                        </div>

                        <div class="col-md-6" id="tblInfo" runat="server" visible="false">

                            <div class="col-md-7">
                                <ul class="list-group list-group-unbordered">
                                    <li class="list-group-item">
                                        <b>Student Name</b><a class="pull-right">
                                            <asp:Label ID="lblName" runat="server" Font-Bold="True" /></a>
                                    </li>
                                    <li class="list-group-item">
                                        <b>Fathers Name</b><a class="pull-right">
                                            <asp:Label ID="lblFatherName" runat="server" Font-Bold="False" /></a>
                                    </li>
                                    <li class="list-group-item">
                                        <b>Mothers Name</b><a class="pull-right"><asp:Label ID="lblMotherName" runat="server" Font-Bold="False" /></a>
                                    </li>
                                    <li class="list-group-item">
                                        <b>Parent Section</b><a class="pull-right"><asp:Label ID="lblSection" runat="server" Font-Bold="False" /></a>
                                    </li>
                                    <li class="list-group-item">
                                        <b>Minimum credit limit</b><a class="pull-right"><asp:Label ID="lblMinCreditLimit" runat="server"></asp:Label></a>
                                    </li>
                                    <li class="list-group-item">
                                        <b>Total Subjects</b><span><asp:TextBox ID="txtAllSubjects" Width="30%" CssClass="pull-right text-bold" runat="server" Enabled="false" Text="0"></asp:TextBox></>
                                    </li>
                                    <%-- <li class="list-group-item">
                                        <b>Result</b><a class="pull-right"><asp:Label ID="lblResult" runat="server"></asp:Label></a>
                                    </li>--%>
                                </ul>
                            </div>
                            <div class="col-md-5">

                                <ul class="list-group list-group-unbordered">

                                    <li class="list-group-item">
                                        <b>Registration No.</b><a class="pull-right"><asp:Label ID="lblEnrollNo" runat="server"></asp:Label></a>
                                    </li>
                                    <li class="list-group-item">
                                        <b>ADM No.</b><a class="pull-right"><asp:Label ID="lblRollNo" runat="server"></asp:Label></a>
                                    </li>
                                    <li class="list-group-item">
                                        <b>Admission Year</b><a class="pull-right">
                                            <asp:Label ID="lblAdmBatch" runat="server"></asp:Label></a>
                                    </li>

                                    <li class="list-group-item">
                                        <b>Semester</b><a class="pull-right"><asp:Label ID="lblSemester" runat="server"></asp:Label></a>
                                    </li>



                                    <li class="list-group-item">
                                        <b>Maximum credit limit</b><a class="pull-right">
                                            <asp:Label ID="lblMaxCreditlimit" runat="server"></asp:Label></a>
                                    </li>

                                    <li class="list-group-item">
                                        <b>Total Credits</b><span><asp:TextBox ID="txtCredits" Width="30%" runat="server" CssClass="pull-right text-bold" Enabled="false" Text="0"></asp:TextBox></span>
                                        <asp:HiddenField ID="hdfCredits" runat="server" Value="0" />
                                        <asp:HiddenField ID="hdfTot" runat="server" Value="0" />
                                    </li>
                                    <%-- <li class="list-group-item">
                                        <b>Admission Type</b><a class="pull-right"><asp:Label ID="lblAdmType" runat="server"></asp:Label></a>

                                    </li>--%>
                                </ul>
                            </div>
                            <div class="col-md-12">
                                <ul class="list-group list-group-unbordered">

                                    <%-- <li class="list-group-item">
                                        <b>Degree</b><a class="pull-right">
                                            <asp:Label ID="lblDegree" runat="server" Font-Bold="True"></asp:Label>
                                        </a>
                                    </li>--%>
                                    <li class="list-group-item">
                                        <b>Program</b><a class="pull-right">
                                            <asp:Label ID="lblDegree" runat="server" Font-Bold="True"></asp:Label>-<asp:Label ID="lblBranch" runat="server" Font-Bold="True"></asp:Label></a>
                                    </li>

                                    <li class="list-group-item" id="trscheme" runat="server">
                                        <b>Regulation</b><a class="pull-right">
                                            <asp:Label ID="lblScheme" runat="server" Font-Bold="True"></asp:Label></a>
                                    </li>
                                </ul>
                            </div>
                        </div>

                        <div class="col-md-6 hidden">
                            <div class="form-group col-md-6" id="trSession_name" runat="server">
                                <label for="city">Session Name</label>
                                <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" CssClass="form-control">
                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession" Display="Dynamic" InitialValue="0" ValidationGroup="Show" ErrorMessage="Please Select Session"></asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group col-md-6" id="trRollNo" runat="server">
                                <label for="city">Registration No.</label>
                                <asp:TextBox ID="txtRollNo" runat="server" CssClass="form-control" placeholder="Enter Registration No." />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRollNo" Display="Dynamic" ValidationGroup="Show" ErrorMessage="Please Enter Registration No."></asp:RequiredFieldValidator>
                            </div>
                            <div class=" col-md-12">
                                <asp:LinkButton ID="btnShow" runat="server" OnClick="btnShow_Click" ValidationGroup="Show" CssClass="btn btn-default"><i class="fa fa-eye"></i> Show</asp:LinkButton>
                                <asp:Button ID="btnCancel" runat="server" Text="Clear" OnClick="btnCancel_Click" CssClass="btn btn-outline-danger" />

                            </div>
                            <div class=" col-md-12" runat="server" visible="false" id="divStudHistory">
                                <asp:ListView ID="lvStudentHistory" runat="server">
                                    <LayoutTemplate>
                                        <h4>
                                            <label class="label label-default">Student Information</label></h4>

                                        <table id="example2" class="table table-bordered table-hover table-striped">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th>Semester</th>
                                                    <th>SGPA</th>
                                                    <th>CGPA</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>

                                        </table>
                                    </LayoutTemplate>

                                    <ItemTemplate>
                                        <tr id="trCurRow">
                                            <td>
                                                <%# Eval("SEMESTERNAME") %>
                                            </td>
                                            <td>
                                                <%# Eval("SGPA") %>
                                            </td>
                                            <td>
                                                <%# Eval("CGPA") %>
                                            </td>

                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                        <!--/.col-md-6-->

                    </div>

                </div>



                <div class="row" runat="server" id="divCurrent_back" visible="false">
                    <div class=" col-md-12 col-sm-12 ">
                        <div class="box box-info">
                            <div class="box-header with-border">
                                <h3 class="box-title">Current Semester Core Subjects</h3>
                                <br />
                                <center>
                                    <div class="btn-group">
                                        <asp:DropDownList ID="ddlCoreSection" runat="server" AutoPostBack="true" CssClass="btn btn-outline-primary" OnSelectedIndexChanged="ddlCoreSection_SelectedIndexChanged"></asp:DropDownList>
                                        <div class="btn-group" style="margin-left:10px;border-left-color:#00c0ef;">
                                            <asp:LinkButton ID="lnkBtnTimeTable" runat="server" OnClick="lnkBtnTimeTable_Click" Text="Time Table" 
                                                CssClass="btn btn-outline-primary btn-sm" style="padding-top:6px;padding-bottom:6px;border-left-color:white;"><i class="fa fa-calendar" aria-hidden="true"></i> Time Table</asp:LinkButton>
                                        </div>
                                    </div>
                                </center>
                                <div class="box-tools pull-right">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                </div>
                            </div>
                            <div class="box-body">
                                <asp:ListView ID="lvCurrentSubjects" runat="server" OnItemDataBound="lvCurrentSubjects_ItemDataBound">
                                    <LayoutTemplate>
                                        <%--  OnClick="lnkbtnHistory_Click"--%>
                                        <table id="Table1" class="table table-bordered table-hover table-striped table-responsive">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th style="width: 5%">Select</th>
                                                    <th style="width: 45%">Subject Code and Name</th>
                                                    <th style="width: 5%">
                                                        <center>Type</center>
                                                    </th>
                                                    <th style="width: 5%">
                                                        <center>Credits</center>
                                                    </th>
                                                    <th style="width: 30%">Section and Faculty</th>
                                                    <th style="width: 5%">
                                                        <center>Intake</center>
                                                    </th>
                                                    <th style="width: 5%">
                                                        <center>Filled</center>
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                        </table>
                                    </LayoutTemplate>
                                    <EmptyDataTemplate>
                                        <tr>
                                            <td colspan="5">
                                                <p>
                                                    <em>No Data Found Of Current Semester Subjects.</em>
                                                </p>
                                            </td>
                                        </tr>
                                    </EmptyDataTemplate>

                                    <ItemTemplate>
                                        <tr id="trCurRow" class="item">
                                            <td>
                                                <center>
                                                    <asp:CheckBox ID="chkAccept" runat="server" Enabled='<%# Eval("STATUS").ToString()=="1"? false:true %>' Checked='<%# Eval("EXAM_REGISTERED").ToString() == "1" ? true : false %>' AutoPostBack="true" OnCheckedChanged="chkAccept_CheckedChanged" />
                                                    <asp:HiddenField ID="hfStatus" runat="server" Value='<%# Eval("STATUS")%>' />
                                                </center>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' />
                                                -
                                                <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' />
                                                <asp:HiddenField ID="hdfCCode" runat="server" Value='<%# Eval("COURSENO")%>' />
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblSub_Type" runat="server" Text='<%# Eval("SUBNAME") %>' />
                                                <%-- ToolTip='<%# Eval("SUBID") %>' />--%>
                                                <asp:HiddenField ID="hdfsubtype" runat="server" Value='<%# Eval("SUBID")%>' />
                                                </center>
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>' />
                                                <asp:HiddenField ID="hdfBatchno" runat="server" Value='<%# Eval("BATCHNO")%>' />
                                                </center>
                                            </td>
                                            <td style="width: 14%">
                                                <asp:DropDownList ID="ddlsection" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlsection_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:HiddenField ID="hdfSectionno" runat="server" Value='<%# Eval("SECTIONNO") %>' />
                                            </td>
                                            <td>
                                                <center>
                                                    <%--<asp:Label ID="lblIntake" runat="server" Text='<%# Eval("INTAKE") %>' CssClass="label label-info"></asp:Label>--%>
                                                    <asp:Label ID="lblIntake" runat="server" Text="" CssClass="label label-info"></asp:Label>
                                                </center>
                                            </td>
                                            <td>
                                                <center>
                                                    <%--<asp:Label ID="lblFilled" runat="server" Text='<%# Eval("FILLED") %>' CssClass="label label-success"></asp:Label>--%>
                                                    <asp:Label ID="lblFilled" runat="server" Text="" CssClass="label label-success"></asp:Label>
                                                </center>
                                            </td>


                                        </tr>
                                    </ItemTemplate>

                                </asp:ListView>
                            </div>
                        </div>
                    </div>
                    <%--  Hidding backlog courses  start--%>
                    <%--  <div class=" col-md-6 col-sm-6" runat="server">
                        <div class="box box-info">
                            <div class="box-header with-border">
                                <h3 class="box-title">Backlog Offered Courses</h3>
                                <div class="box-tools pull-right">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                </div>

                            </div>
                            <div class="box-body">                              
                                <div class="table-responsive">
                                    <asp:ListView ID="lvBacklogCourse" runat="server" InsertItemPosition="None" OnItemDataBound="lvBacklogCourse_ItemDataBound">
                                        <LayoutTemplate>
                                            <table id="Table2" class="table table-bordered table-hover table-striped  ">
                                                <thead>
                                                    <tr class="bg-light-blue">
                                                        <th>Select</th>
                                                        <th>Code</th>
                                                        <th>Name</th>
                                                        <th>Type</th>
                                                        <th>Sem</th>
                                                        <th>Credits</th>
                                                        <th>Attempts</th>
                                                        <th>Sec</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>

                                        <ItemTemplate>
                                            <tr id="trCurRow" class="item">
                                                <td>
                                                    <asp:CheckBox ID="chkBacklog" runat="server" Enabled="false" Checked='<%# Eval("REGIST").ToString() == "1" ? true : false %>' />
                                                                                                  </td>
                                                <td>
                                                    <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' />                                                 
                                                    <asp:HiddenField ID="hdfCCode" runat="server" Value='<%# Eval("COURSENO")%>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSub_Type" runat="server" Text='<%# Eval("SUBNAME") %>' />

                                                    <asp:HiddenField ID="hdfsubtype" runat="server" Value='<%# Eval("SUBID")%>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSemster" runat="server" Text='<%# Eval("SEMESTERNAME") %>' />

                                                    <asp:HiddenField ID="hdfsemno" runat="server" Value='<%# Eval("SEMESTERNO")%>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblAttempt" runat="server" Text='<%# Eval("ATTEMPT_CNT") %>' />
                                                    <asp:HiddenField ID="hdfBatchno" runat="server" Value='<%# Eval("BATCHNO")%>' />
                                                </td>
                                                <td style="width: 50%">
                                                    <asp:DropDownList ID="ddlsection" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>                                                         
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <EmptyDataTemplate>

                                            <tr class="bg-light-blue">
                                                <td colspan="5">
                                                    <p>
                                                        <em>No Data Found Of Backlog Courses </em>
                                                    </p>
                                                </td>
                                            </tr>

                                        </EmptyDataTemplate>
                                    </asp:ListView>
                                </div>                             
                            </div>
                        </div>
                    </div>--%>
                    <%--  Hidding backlog courses  end--%>

                    <div class=" col-md-12 col-sm-12 col-lg-12 ">
                        <div class="box box-info">
                            <div class="box-header with-border">
                                <h3 class="box-title">Detain  Offered Subjects </h3>
                                <div class="box-tools pull-right">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                </div>
                            </div>
                            <div class="box-body">
                                <asp:ListView ID="lvDetained" runat="server" OnItemDataBound="lvDetained_ItemDataBound">
                                    <LayoutTemplate>
                                        <table id="Table4" class="table table-bordered table-hover table-striped table-responsive">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th>Select</th>
                                                    <th>Code</th>
                                                    <th>Name</th>
                                                    <th>Type</th>
                                                    <th>Sem</th>
                                                    <th>Credits</th>
                                                    <th>Sec</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                        </table>
                                    </LayoutTemplate>
                                    <EmptyDataTemplate>
                                        <tr class="bg-light-blue">
                                            <td colspan="5">
                                                <p>
                                                    <em id="lblDMsg">No Data Available.</em>
                                                </p>
                                            </td>
                                        </tr>
                                    </EmptyDataTemplate>
                                    <ItemTemplate>
                                        <tr id="trCurRow" class="item">
                                            <td>
                                                <asp:CheckBox ID="chkDetain" runat="server" Enabled="false" Checked="true" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' />
                                                <asp:HiddenField ID="hdfCCode" runat="server" Value='<%# Eval("COURSENO")%>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblSub_Type" runat="server" Text='<%# Eval("SUBNAME") %>' />
                                                <asp:HiddenField ID="hdfsubtype" runat="server" Value='<%# Eval("SUBID")%>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblSemster" runat="server" Text='<%# Eval("SEMESTERNAME") %>' />
                                                <asp:HiddenField ID="hdfsemno" runat="server" Value='<%# Eval("SEMESTERNO")%>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>' />
                                                <asp:HiddenField ID="hdfBatchno" runat="server" Value='<%# Eval("BATCHNO")%>' />
                                            </td>
                                            <td style="width: 14%">
                                                <asp:DropDownList ID="ddlsection" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                                <asp:HiddenField ID="hdfSectionno" runat="server" Value='<%# Eval("SECTIONNO") %>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                    </div>
                </div>

                <!--Start region all offered elective courses-->

                <div class="row" runat="server" id="divelective" visible="false">
                    <div class="col-md-12 col-sm-12 col-lg-12">
                        <div class="box box-info">
                            <div class="box-header with-border">
                                <h3 class="box-title">Elective Offered Subjects</h3>
                                <span style="color: green; font-size: 12px;">(Optional)</span></h4>
                                <div class="box-tools pull-right">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                </div>
                            </div>
                            <div class="box-body">
                                <asp:ListView ID="lvElectiveCourse" runat="server" OnItemDataBound="lvElectiveCourse_ItemDataBound">
                                    <LayoutTemplate>
                                        <table id="Table3" class="table table-bordered table-hover table-striped  table-responsive">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th style="width: 5%">
                                                        <center>Select</center>
                                                    </th>
                                                    <th style="width: 40%">Subject Code and Name</th>
                                                    <th style="width: 5%">
                                                        <center>Group</center>
                                                    </th>
                                                    <th style="width: 5%">
                                                        <center>Type</center>
                                                    </th>
                                                    <th style="width: 5%">
                                                        <center>Credits</center>
                                                    </th>
                                                    <th style="width: 30%">Section and Faculty</th>
                                                    <th style="width: 5%">
                                                        <center>Intake</center>
                                                    </th>
                                                    <th style="width: 5%">
                                                        <center>Filled</center>
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tr id="itemplaceholder" runat="server">
                                            </tr>
                                            <tbody></tbody>
                                        </table>
                                        <tbody>
                                    </LayoutTemplate>
                                    <EmptyDataTemplate>
                                        <tr class="bg-light-blue">
                                            <td colspan="5" style="text-align: center">
                                                <p>
                                                    <em>----------- No Data Found Of Elective Subjects -----------</em>
                                                </p>
                                            </td>
                                        </tr>
                                    </EmptyDataTemplate>

                                    <ItemTemplate>
                                        <tr id="trCurRow" class="item">
                                            <td>
                                                <center>
                                                    <asp:CheckBox ID="chkElectiveGrp1" runat="server" Enabled='<%# Eval("STATUS").ToString()=="1"? false:true %>' Checked='<%# Eval("EXAM_REGISTERED").ToString() == "1" ? true : false %>'
                                                     AutoPostBack="true" OnCheckedChanged="chkElectiveGrp1_CheckedChanged" />   
                                                </center>
                                                <asp:HiddenField ID="hfStatus" runat="server" Value='<%# Eval("STATUS")%>' />
                                            </td>
                                            <td>

                                                <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' />
                                                -
                                                <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' />
                                                <asp:HiddenField ID="hdfCCode" runat="server" Value='<%# Eval("COURSENO")%>' />
                                            </td>
                                            <td>
                                                <center>
                                                    <center><asp:Label ID="lblGrupName" runat="server" Text='<%# Eval("GROUPNAME") %>' /></center>
                                                </center>
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblSub_Type" runat="server" Text='<%# Eval("SUBNAME") %>' />
                                               </center>
                                                <asp:HiddenField ID="hdfsubtype" runat="server" Value='<%# Eval("SUBID")%>' />
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>' />
                                                </center>
                                                <asp:HiddenField ID="hdfBatchno" runat="server" Value='<%# Eval("BATCHNO")%>' />
                                            </td>
                                            <td style="width: 14%">
                                                <asp:DropDownList ID="ddlsection" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlsection_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:HiddenField ID="hdfSectionno" runat="server" Value='<%# Eval("SECTIONNO") %>' />
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblIntake" runat="server" Text="" CssClass="label label-info"></asp:Label>
                                                </center>
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblFilled" runat="server" Text="" CssClass="label label-success"></asp:Label>
                                                </center>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row" runat="server" id="divelective2" visible="false">
                    <div class="col-md-12 col-sm-12 col-lg-12">
                        <div class="box box-info">
                            <div class="box-header with-border">
                                <h3 class="box-title">Elective Offered Subjects [Group-2]</h3>
                                <span style="color: green; font-size: 12px;">(Optional)</span></h4>
                                <div class="box-tools pull-right">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                </div>

                            </div>
                            <div class="box-body">
                                <asp:ListView ID="lvElectiveCourseGrp2" runat="server" OnItemDataBound="lvElectiveCourseGrp2_ItemDataBound">
                                    <LayoutTemplate>
                                        <table id="Table3" class="table table-bordered table-hover table-striped  table-responsive">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th style="width: 5%">
                                                        <center>Select</center>
                                                    </th>
                                                    <th style="width: 40%">Subject Code and Name</th>
                                                    <th style="width: 5%">
                                                        <center>Group</center>
                                                    </th>
                                                    <th style="width: 5%">
                                                        <center>Type</center>
                                                    </th>
                                                    <th style="width: 5%">
                                                        <center>Credits</center>
                                                    </th>
                                                    <th style="width: 30%">Section and Faculty</th>
                                                    <th style="width: 5%">
                                                        <center>Intake</center>
                                                    </th>
                                                    <th style="width: 5%">
                                                        <center>Filled</center>
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tr id="itemplaceholder" runat="server">
                                            </tr>
                                            <tbody></tbody>
                                        </table>
                                        <tbody>
                                    </LayoutTemplate>
                                    <EmptyDataTemplate>
                                        <tr class="bg-light-blue">
                                            <td colspan="5">
                                                <p>
                                                    <em>----------- No Data Found Of Elective Subjects [Group-2] -----------</em>
                                                </p>
                                            </td>
                                        </tr>
                                    </EmptyDataTemplate>
                                    <ItemTemplate>
                                        <tr id="trCurRow" class="item">

                                            <td>
                                                <center>
                                                    <asp:CheckBox ID="chkElectiveGrp2" runat="server" Enabled='<%# Eval("STATUS").ToString()=="1"? false:true %>' Checked='<%# Eval("EXAM_REGISTERED").ToString() == "1" ? true : false %>' 
                                                    AutoPostBack="true" OnCheckedChanged="chkElectiveGrp2_CheckedChanged" />
                                                </center>
                                                <asp:HiddenField ID="hfStatus" runat="server" Value='<%# Eval("STATUS")%>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' />
                                                -
                                                <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' />
                                                <asp:HiddenField ID="hdfCCode" runat="server" Value='<%# Eval("COURSENO")%>' />
                                            </td>
                                            <td>
                                                <center>
                                                    <center><asp:Label ID="lblGrupName" runat="server" Text='<%# Eval("GROUPNAME") %>' /></center>
                                                </center>
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblSub_Type" runat="server" Text='<%# Eval("SUBNAME") %>' />
                                               </center>
                                                <asp:HiddenField ID="hdfsubtype" runat="server" Value='<%# Eval("SUBID")%>' />
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>' />
                                                </center>
                                                <asp:HiddenField ID="hdfBatchno" runat="server" Value='<%# Eval("BATCHNO")%>' />
                                            </td>
                                            <td style="width: 14%">
                                                <asp:DropDownList ID="ddlsection" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlsection_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:HiddenField ID="hdfSectionno" runat="server" Value='<%# Eval("SECTIONNO") %>' />
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblIntake" runat="server" Text="" CssClass="label label-info"></asp:Label>
                                                </center>
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblFilled" runat="server" Text="" CssClass="label label-success"></asp:Label>
                                                </center>
                                            </td>

                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row" runat="server" id="divelective3" visible="false">
                    <div class="col-md-12 col-sm-12 col-lg-12">
                        <div class="box box-info">
                            <div class="box-header with-border">
                                <h3 class="box-title">Elective Offered Subjects [Group-3]</h3>
                                <span style="color: green; font-size: 12px;">(Optional)</span></h4>
                                <div class="box-tools pull-right">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                </div>
                            </div>
                            <div class="box-body">
                                <asp:ListView ID="lvElectiveCourseGrp3" runat="server" OnItemDataBound="lvElectiveCourseGrp3_ItemDataBound">
                                    <LayoutTemplate>
                                        <table id="Table3" class="table table-bordered table-hover table-striped  table-responsive">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th style="width: 5%">
                                                        <center>Select</center>
                                                    </th>
                                                    <th style="width: 40%">Subject Code and Name</th>
                                                    <th style="width: 5%">
                                                        <center>Group</center>
                                                    </th>
                                                    <th style="width: 5%">
                                                        <center>Type</center>
                                                    </th>
                                                    <th style="width: 5%">
                                                        <center>Credits</center>
                                                    </th>
                                                    <th style="width: 30%">Section and Faculty</th>
                                                    <th style="width: 5%">
                                                        <center>Intake</center>
                                                    </th>
                                                    <th style="width: 5%">
                                                        <center>Filled</center>
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tr id="itemplaceholder" runat="server">
                                            </tr>
                                            <tbody></tbody>
                                        </table>
                                        <tbody>
                                    </LayoutTemplate>
                                    <EmptyDataTemplate>
                                        <tr class="bg-light-blue">
                                            <td colspan="5">
                                                <p>
                                                    <em>----------- No Data Found Of Elective Subjects [Group-3] -----------</em>
                                                </p>
                                            </td>
                                        </tr>
                                    </EmptyDataTemplate>
                                    <ItemTemplate>
                                        <tr id="trCurRow" class="item">

                                            <td>
                                                <center>
                                                    <asp:CheckBox ID="chkElectiveGrp3" runat="server" Enabled='<%# Eval("STATUS").ToString()=="1"? false:true %>' Checked='<%# Eval("EXAM_REGISTERED").ToString() == "1" ? true : false %>' AutoPostBack="true" OnCheckedChanged="chkElectiveGrp3_CheckedChanged" />
                                                </center>
                                                <asp:HiddenField ID="hfStatus" runat="server" Value='<%# Eval("STATUS")%>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' />
                                                -
                                                <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' />
                                                <asp:HiddenField ID="hdfCCode" runat="server" Value='<%# Eval("COURSENO")%>' />
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblGrupName" runat="server" Text='<%# Eval("GROUPNAME") %>' />
                                                </center>
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblSub_Type" runat="server" Text='<%# Eval("SUBNAME") %>' />
                                                    <asp:HiddenField ID="hdfsubtype" runat="server" Value='<%# Eval("SUBID")%>' />
                                               </center>
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>' />
                                                    <asp:HiddenField ID="hdfBatchno" runat="server" Value='<%# Eval("BATCHNO")%>' />
                                                </center>
                                            </td>
                                            <td style="width: 14%">
                                                <asp:DropDownList ID="ddlsection" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlsection_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:HiddenField ID="hdfSectionno" runat="server" Value='<%# Eval("SECTIONNO") %>' />
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblIntake" runat="server" Text="" CssClass="label label-info"></asp:Label>
                                                </center>
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblFilled" runat="server" Text="" CssClass="label label-success"></asp:Label>
                                                </center>
                                            </td>

                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row" runat="server" id="divelective4" visible="false">
                    <div class="col-md-12 col-sm-12 col-lg-12">
                        <div class="box box-info">
                            <div class="box-header with-border">
                                <h3 class="box-title">Elective Offered Subjects [Group-4]</h3>
                                <span style="color: green; font-size: 12px;">(Optional)</span></h4>
                                <div class="box-tools pull-right">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                </div>
                            </div>
                            <div class="box-body">
                                <asp:ListView ID="lvElectiveCourseGrp4" runat="server" OnItemDataBound="lvElectiveCourseGrp4_ItemDataBound">
                                    <LayoutTemplate>
                                        <table id="Table3" class="table table-bordered table-hover table-striped  table-responsive">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th style="width: 5%">
                                                        <center>Select</center>
                                                    </th>
                                                    <th style="width: 40%">Subject Code and Name</th>
                                                    <th style="width: 5%">
                                                        <center>Group</center>
                                                    </th>
                                                    <th style="width: 5%">
                                                        <center>Type</center>
                                                    </th>
                                                    <th style="width: 5%">
                                                        <center>Credits</center>
                                                    </th>
                                                    <th style="width: 30%">Section and Faculty</th>
                                                    <th style="width: 5%">
                                                        <center>Intake</center>
                                                    </th>
                                                    <th style="width: 5%">
                                                        <center>Filled</center>
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tr id="itemplaceholder" runat="server">
                                            </tr>
                                            <tbody></tbody>
                                        </table>
                                        <tbody>
                                    </LayoutTemplate>
                                    <EmptyDataTemplate>
                                        <tr class="bg-light-blue">
                                            <td colspan="5">
                                                <p>
                                                    <em>----------- No Data Found Of Elective Subjects [Group-4] -----------</em>
                                                </p>
                                            </td>
                                        </tr>
                                    </EmptyDataTemplate>
                                    <ItemTemplate>
                                        <tr id="trCurRow" class="item">

                                            <td>
                                                <center>
                                                     <asp:CheckBox ID="chkElectiveGrp4" runat="server" Enabled='<%# Eval("STATUS").ToString()=="1"? false:true %>' Checked='<%# Eval("EXAM_REGISTERED").ToString() == "1" ? true : false %>' AutoPostBack="true" OnCheckedChanged="chkElectiveGrp4_CheckedChanged" />
                                                     <asp:HiddenField ID="hfStatus" runat="server" Value='<%# Eval("STATUS")%>' />
                                                </center>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' />
                                                -
                                                <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' />
                                                <asp:HiddenField ID="hdfCCode" runat="server" Value='<%# Eval("COURSENO")%>' />
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblGrupName" runat="server" Text='<%# Eval("GROUPNAME") %>' />
                                                </center>
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblSub_Type" runat="server" Text='<%# Eval("SUBNAME") %>' />
                                                    <asp:HiddenField ID="hdfsubtype" runat="server" Value='<%# Eval("SUBID")%>' />
                                               </center>
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>' />
                                                    <asp:HiddenField ID="hdfBatchno" runat="server" Value='<%# Eval("BATCHNO")%>' />
                                                </center>
                                            </td>
                                            <td style="width: 14%">
                                                <asp:DropDownList ID="ddlsection" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlsection_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:HiddenField ID="hdfSectionno" runat="server" Value='<%# Eval("SECTIONNO") %>' />
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblIntake" runat="server" Text="" CssClass="label label-info"></asp:Label>
                                                </center>
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblFilled" runat="server" Text="" CssClass="label label-success"></asp:Label>
                                                </center>
                                            </td>

                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row" runat="server" id="divelective5" visible="false">
                    <div class="col-md-12 col-sm-12 col-lg-12">
                        <div class="box box-info">
                            <div class="box-header with-border">
                                <h3 class="box-title">Elective Offered Subjects [Group-5]</h3>
                                <span style="color: green; font-size: 12px;">(Optional)</span></h4>
                                <div class="box-tools pull-right">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                </div>
                            </div>
                            <div class="box-body">
                                <asp:ListView ID="lvElectiveCourseGrp5" runat="server" OnItemDataBound="lvElectiveCourseGrp5_ItemDataBound">
                                    <LayoutTemplate>
                                        <table id="Table3" class="table table-bordered table-hover table-striped  table-responsive">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th style="width: 5%">
                                                        <center>Select</center>
                                                    </th>
                                                    <th style="width: 40%">Subject Code and Name</th>
                                                    <th style="width: 5%">
                                                        <center>Group</center>
                                                    </th>
                                                    <th style="width: 5%">
                                                        <center>Type</center>
                                                    </th>
                                                    <th style="width: 5%">
                                                        <center>Credits</center>
                                                    </th>
                                                    <th style="width: 30%">Section and Faculty</th>
                                                    <th style="width: 5%">
                                                        <center>Intake</center>
                                                    </th>
                                                    <th style="width: 5%">
                                                        <center>Filled</center>
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tr id="itemplaceholder" runat="server">
                                            </tr>
                                            <tbody></tbody>
                                        </table>
                                        <tbody>
                                    </LayoutTemplate>
                                    <EmptyDataTemplate>
                                        <tr class="bg-light-blue">
                                            <td colspan="5">
                                                <p>
                                                    <em>----------- No Data Found Of Elective Subjects [Group-5] -----------</em>
                                                </p>
                                            </td>
                                        </tr>
                                    </EmptyDataTemplate>
                                    <ItemTemplate>
                                        <tr id="trCurRow" class="item">

                                            <td>
                                                <center>
                                                     <asp:CheckBox ID="chkElectiveGrp5" runat="server" Enabled='<%# Eval("STATUS").ToString()=="1"? false:true %>' Checked='<%# Eval("EXAM_REGISTERED").ToString() == "1" ? true : false %>' AutoPostBack="true" OnCheckedChanged="chkElectiveGrp5_CheckedChanged" />
                                                     <asp:HiddenField ID="hfStatus" runat="server" Value='<%# Eval("STATUS")%>' />
                                                </center>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' />
                                                -
                                                <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' />
                                                <asp:HiddenField ID="hdfCCode" runat="server" Value='<%# Eval("COURSENO")%>' />
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblGrupName" runat="server" Text='<%# Eval("GROUPNAME") %>' />
                                                </center>
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblSub_Type" runat="server" Text='<%# Eval("SUBNAME") %>' />
                                                    <asp:HiddenField ID="hdfsubtype" runat="server" Value='<%# Eval("SUBID")%>' />
                                               </center>
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>' />
                                                    <asp:HiddenField ID="hdfBatchno" runat="server" Value='<%# Eval("BATCHNO")%>' />
                                                </center>
                                            </td>
                                            <td style="width: 14%">
                                                <asp:DropDownList ID="ddlsection" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlsection_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:HiddenField ID="hdfSectionno" runat="server" Value='<%# Eval("SECTIONNO") %>' />
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblIntake" runat="server" Text="" CssClass="label label-info"></asp:Label>
                                                </center>
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblFilled" runat="server" Text="" CssClass="label label-success"></asp:Label>
                                                </center>
                                            </td>

                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row" runat="server" id="divelective6" visible="false">
                    <div class="col-md-12 col-sm-12 col-lg-12">
                        <div class="box box-info">
                            <div class="box-header with-border">
                                <h3 class="box-title">Elective Offered Subjects [Group-6]</h3>
                                <span style="color: green; font-size: 12px;">(Optional)</span></h4>
                                <div class="box-tools pull-right">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                </div>
                            </div>
                            <div class="box-body">
                                <asp:ListView ID="lvElectiveCourseGrp6" runat="server" OnItemDataBound="lvElectiveCourseGrp6_ItemDataBound">
                                    <LayoutTemplate>
                                        <table id="Table3" class="table table-bordered table-hover table-striped  table-responsive">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th style="width: 5%">
                                                        <center>Select</center>
                                                    </th>
                                                    <th style="width: 40%">Subject Code and Name</th>
                                                    <th style="width: 5%">
                                                        <center>Group</center>
                                                    </th>
                                                    <th style="width: 5%">
                                                        <center>Type</center>
                                                    </th>
                                                    <th style="width: 5%">
                                                        <center>Credits</center>
                                                    </th>
                                                    <th style="width: 30%">Section and Faculty</th>
                                                    <th style="width: 5%">
                                                        <center>Intake</center>
                                                    </th>
                                                    <th style="width: 5%">
                                                        <center>Filled</center>
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tr id="itemplaceholder" runat="server">
                                            </tr>
                                            <tbody></tbody>
                                        </table>
                                        <tbody>
                                    </LayoutTemplate>
                                    <EmptyDataTemplate>
                                        <tr class="bg-light-blue">
                                            <td colspan="5">
                                                <p>
                                                    <em>----------- No Data Found Of Elective Subjects [Group-6] -----------</em>
                                                </p>
                                            </td>
                                        </tr>
                                    </EmptyDataTemplate>
                                    <ItemTemplate>
                                        <tr id="trCurRow" class="item">

                                            <td>
                                                <center>
                                                     <asp:CheckBox ID="chkElectiveGrp6" runat="server" Enabled='<%# Eval("STATUS").ToString()=="1"? false:true %>' Checked='<%# Eval("EXAM_REGISTERED").ToString() == "1" ? true : false %>' AutoPostBack="true" OnCheckedChanged="chkElectiveGrp6_CheckedChanged" />
                                                     <asp:HiddenField ID="hfStatus" runat="server" Value='<%# Eval("STATUS")%>' />
                                                </center>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' />
                                                -
                                                <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' />
                                                <asp:HiddenField ID="hdfCCode" runat="server" Value='<%# Eval("COURSENO")%>' />
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblGrupName" runat="server" Text='<%# Eval("GROUPNAME") %>' />
                                                </center>
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblSub_Type" runat="server" Text='<%# Eval("SUBNAME") %>' />
                                                    <asp:HiddenField ID="hdfsubtype" runat="server" Value='<%# Eval("SUBID")%>' />
                                               </center>
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>' />
                                                    <asp:HiddenField ID="hdfBatchno" runat="server" Value='<%# Eval("BATCHNO")%>' />
                                                </center>
                                            </td>
                                            <td style="width: 14%">
                                                <asp:DropDownList ID="ddlsection" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlsection_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:HiddenField ID="hdfSectionno" runat="server" Value='<%# Eval("SECTIONNO") %>' />
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblIntake" runat="server" Text="" CssClass="label label-info"></asp:Label>
                                                </center>
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblFilled" runat="server" Text="" CssClass="label label-success"></asp:Label>
                                                </center>
                                            </td>

                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row" runat="server" id="divelective7" visible="false">
                    <div class="col-md-12 col-sm-12 col-lg-12">
                        <div class="box box-info">
                            <div class="box-header with-border">
                                <h3 class="box-title">Elective Offered Subjects [Group-7]</h3>
                                <span style="color: green; font-size: 12px;">(Optional)</span></h4>
                                <div class="box-tools pull-right">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                </div>
                            </div>
                            <div class="box-body">
                                <asp:ListView ID="lvElectiveCourseGrp7" runat="server" OnItemDataBound="lvElectiveCourseGrp7_ItemDataBound">
                                    <LayoutTemplate>
                                        <table id="Table3" class="table table-bordered table-hover table-striped  table-responsive">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th style="width: 5%">
                                                        <center>Select</center>
                                                    </th>
                                                    <th style="width: 40%">Subject Code and Name</th>
                                                    <th style="width: 5%">
                                                        <center>Group</center>
                                                    </th>
                                                    <th style="width: 5%">
                                                        <center>Type</center>
                                                    </th>
                                                    <th style="width: 5%">
                                                        <center>Credits</center>
                                                    </th>
                                                    <th style="width: 30%">Section and Faculty</th>
                                                    <th style="width: 5%">
                                                        <center>Intake</center>
                                                    </th>
                                                    <th style="width: 5%">
                                                        <center>Filled</center>
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tr id="itemplaceholder" runat="server">
                                            </tr>
                                            <tbody></tbody>
                                        </table>
                                        <tbody>
                                    </LayoutTemplate>
                                    <EmptyDataTemplate>
                                        <tr class="bg-light-blue">
                                            <td colspan="5">
                                                <p>
                                                    <em>----------- No Data Found Of Elective Subjects [Group-7] -----------</em>
                                                </p>
                                            </td>
                                        </tr>
                                    </EmptyDataTemplate>
                                    <ItemTemplate>
                                        <tr id="trCurRow" class="item">

                                            <td>
                                                <center>
                                                     <asp:CheckBox ID="chkElectiveGrp7" runat="server" Enabled='<%# Eval("STATUS").ToString()=="1"? false:true %>' Checked='<%# Eval("EXAM_REGISTERED").ToString() == "1" ? true : false %>' AutoPostBack="true" OnCheckedChanged="chkElectiveGrp7_CheckedChanged" />
                                                     <asp:HiddenField ID="hfStatus" runat="server" Value='<%# Eval("STATUS")%>' />
                                                </center>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' />
                                                -
                                                <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' />
                                                <asp:HiddenField ID="hdfCCode" runat="server" Value='<%# Eval("COURSENO")%>' />
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblGrupName" runat="server" Text='<%# Eval("GROUPNAME") %>' />
                                                </center>
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblSub_Type" runat="server" Text='<%# Eval("SUBNAME") %>' />
                                                    <asp:HiddenField ID="hdfsubtype" runat="server" Value='<%# Eval("SUBID")%>' />
                                               </center>
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>' />
                                                    <asp:HiddenField ID="hdfBatchno" runat="server" Value='<%# Eval("BATCHNO")%>' />
                                                </center>
                                            </td>
                                            <td style="width: 14%">
                                                <asp:DropDownList ID="ddlsection" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlsection_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:HiddenField ID="hdfSectionno" runat="server" Value='<%# Eval("SECTIONNO") %>' />
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblIntake" runat="server" Text="" CssClass="label label-info"></asp:Label>
                                                </center>
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblFilled" runat="server" Text="" CssClass="label label-success"></asp:Label>
                                                </center>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                    </div>
                </div>



                <div class="row" runat="server" id="divelective8" visible="false">
                    <div class="col-md-12 col-sm-12 col-lg-12">
                        <div class="box box-info">
                            <div class="box-header with-border">
                                <h3 class="box-title">Elective Offered Subjects [Group-8]</h3>
                                <span style="color: green; font-size: 12px;">(Optional)</span></h4>
                                <div class="box-tools pull-right">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                </div>
                            </div>
                            <div class="box-body">
                                <asp:ListView ID="lvElectiveCourseGrp8" runat="server" OnItemDataBound="lvElectiveCourseGrp8_ItemDataBound">
                                    <LayoutTemplate>
                                        <table id="Table3" class="table table-bordered table-hover table-striped table-responsive">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th style="width: 5%">
                                                        <center>Select</center>
                                                    </th>
                                                    <th style="width: 40%">Subject Code and Name</th>
                                                    <th style="width: 5%">
                                                        <center>Group</center>
                                                    </th>
                                                    <th style="width: 5%">
                                                        <center>Type</center>
                                                    </th>
                                                    <th style="width: 5%">
                                                        <center>Credits</center>
                                                    </th>
                                                    <th style="width: 30%">Section and Faculty</th>
                                                    <th style="width: 5%">
                                                        <center>Intake</center>
                                                    </th>
                                                    <th style="width: 5%">
                                                        <center>Filled</center>
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tr id="itemplaceholder" runat="server">
                                            </tr>
                                            <tbody></tbody>
                                        </table>
                                        <tbody>
                                    </LayoutTemplate>
                                    <EmptyDataTemplate>
                                        <tr class="bg-light-blue">
                                            <td colspan="5">
                                                <p>
                                                    <em>----------- No Data Found Of Elective Subjects [Group-8] -----------</em>
                                                </p>
                                            </td>
                                        </tr>
                                    </EmptyDataTemplate>
                                    <ItemTemplate>
                                        <tr id="trCurRow" class="item">
                                            <td>
                                                <center>
                                                     <asp:CheckBox ID="chkElectiveGrp8" runat="server" Enabled='<%# Eval("STATUS").ToString()=="1"? false:true %>' Checked='<%# Eval("EXAM_REGISTERED").ToString() == "1" ? true : false %>' 
                                                         AutoPostBack="true" OnCheckedChanged="chkElectiveGrp8_CheckedChanged" />
                                                     <asp:HiddenField ID="hfStatus" runat="server" Value='<%# Eval("STATUS")%>' />
                                                </center>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' />
                                                -
                                                <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' />
                                                <asp:HiddenField ID="hdfCCode" runat="server" Value='<%# Eval("COURSENO")%>' />
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblGrupName" runat="server" Text='<%# Eval("GROUPNAME") %>' />
                                                </center>
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblSub_Type" runat="server" Text='<%# Eval("SUBNAME") %>' />
                                                    <asp:HiddenField ID="hdfsubtype" runat="server" Value='<%# Eval("SUBID")%>' />
                                               </center>
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>' />
                                                    <asp:HiddenField ID="hdfBatchno" runat="server" Value='<%# Eval("BATCHNO")%>' />
                                                </center>
                                            </td>
                                            <td style="width: 14%">
                                                <asp:DropDownList ID="ddlsection" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlsection_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:HiddenField ID="hdfSectionno" runat="server" Value='<%# Eval("SECTIONNO") %>' />
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblIntake" runat="server" Text="" CssClass="label label-info"></asp:Label>
                                                </center>
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblFilled" runat="server" Text="" CssClass="label label-success"></asp:Label>
                                                </center>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row" runat="server" id="divelective9" visible="false">
                    <div class="col-md-12 col-sm-12 col-lg-12">
                        <div class="box box-info">
                            <div class="box-header with-border">
                                <h3 class="box-title">Elective Offered Subjects [Group-9]</h3>
                                <span style="color: green; font-size: 12px;">(Optional)</span></h4>
                                <div class="box-tools pull-right">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                </div>
                            </div>
                            <div class="box-body">
                                <asp:ListView ID="lvElectiveCourseGrp9" runat="server" OnItemDataBound="lvElectiveCourseGrp9_ItemDataBound">
                                    <LayoutTemplate>
                                        <table id="Table3" class="table table-bordered table-hover table-striped table-responsive">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th style="width: 5%">
                                                        <center>Select</center>
                                                    </th>
                                                    <th style="width: 40%">Subject Code and Name</th>
                                                    <th style="width: 5%">
                                                        <center>Group</center>
                                                    </th>
                                                    <th style="width: 5%">
                                                        <center>Type</center>
                                                    </th>
                                                    <th style="width: 5%">
                                                        <center>Credits</center>
                                                    </th>
                                                    <th style="width: 30%">Section and Faculty</th>
                                                    <th style="width: 5%">
                                                        <center>Intake</center>
                                                    </th>
                                                    <th style="width: 5%">
                                                        <center>Filled</center>
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tr id="itemplaceholder" runat="server">
                                            </tr>
                                            <tbody></tbody>
                                        </table>
                                        <tbody>
                                    </LayoutTemplate>
                                    <EmptyDataTemplate>
                                        <tr class="bg-light-blue">
                                            <td colspan="5">
                                                <p>
                                                    <em>----------- No Data Found Of Elective Subjects [Group-9] -----------</em>
                                                </p>
                                            </td>
                                        </tr>
                                    </EmptyDataTemplate>
                                    <ItemTemplate>
                                        <tr id="trCurRow" class="item">
                                            <td>
                                                <center>
                                                     <asp:CheckBox ID="chkElectiveGrp9" runat="server" Enabled='<%# Eval("STATUS").ToString()=="1"? false:true %>' Checked='<%# Eval("EXAM_REGISTERED").ToString() == "1" ? true : false %>' 
                                                         AutoPostBack="true" OnCheckedChanged="chkElectiveGrp9_CheckedChanged" />
                                                     <asp:HiddenField ID="hfStatus" runat="server" Value='<%# Eval("STATUS")%>' />
                                                </center>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' />
                                                -
                                                <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' />
                                                <asp:HiddenField ID="hdfCCode" runat="server" Value='<%# Eval("COURSENO")%>' />
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblGrupName" runat="server" Text='<%# Eval("GROUPNAME") %>' />
                                                </center>
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblSub_Type" runat="server" Text='<%# Eval("SUBNAME") %>' />
                                                    <asp:HiddenField ID="hdfsubtype" runat="server" Value='<%# Eval("SUBID")%>' />
                                               </center>
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>' />
                                                    <asp:HiddenField ID="hdfBatchno" runat="server" Value='<%# Eval("BATCHNO")%>' />
                                                </center>
                                            </td>
                                            <td style="width: 14%">
                                                <asp:DropDownList ID="ddlsection" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlsection_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:HiddenField ID="hdfSectionno" runat="server" Value='<%# Eval("SECTIONNO") %>' />
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblIntake" runat="server" Text="" CssClass="label label-info"></asp:Label>
                                                </center>
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblFilled" runat="server" Text="" CssClass="label label-success"></asp:Label>
                                                </center>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row" runat="server" id="divelective10" visible="false">
                    <div class="col-md-12 col-sm-12 col-lg-12">
                        <div class="box box-info">
                            <div class="box-header with-border">
                                <h3 class="box-title">Elective Offered Subjects [Group-10]</h3>
                                <span style="color: green; font-size: 12px;">(Optional)</span></h4>
                                <div class="box-tools pull-right">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                </div>
                            </div>
                            <div class="box-body">
                                <asp:ListView ID="lvElectiveCourseGrp10" runat="server" OnItemDataBound="lvElectiveCourseGrp10_ItemDataBound">
                                    <LayoutTemplate>
                                        <table id="Table3" class="table table-bordered table-hover table-striped table-responsive">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th style="width: 5%">
                                                        <center>Select</center>
                                                    </th>
                                                    <th style="width: 40%">Subject Code and Name</th>
                                                    <th style="width: 5%">
                                                        <center>Group</center>
                                                    </th>
                                                    <th style="width: 5%">
                                                        <center>Type</center>
                                                    </th>
                                                    <th style="width: 5%">
                                                        <center>Credits</center>
                                                    </th>
                                                    <th style="width: 30%">Section and Faculty</th>
                                                    <th style="width: 5%">
                                                        <center>Intake</center>
                                                    </th>
                                                    <th style="width: 5%">
                                                        <center>Filled</center>
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tr id="itemplaceholder" runat="server">
                                            </tr>
                                            <tbody></tbody>
                                        </table>
                                        <tbody>
                                    </LayoutTemplate>
                                    <EmptyDataTemplate>
                                        <tr class="bg-light-blue">
                                            <td colspan="5">
                                                <p>
                                                    <em>----------- No Data Found Of Elective Subjects [Group-10] -----------</em>
                                                </p>
                                            </td>
                                        </tr>
                                    </EmptyDataTemplate>
                                    <ItemTemplate>
                                        <tr id="trCurRow" class="item">
                                            <td>
                                                <center>
                                                     <asp:CheckBox ID="chkElectiveGrp10" runat="server" Enabled='<%# Eval("STATUS").ToString()=="1"? false:true %>' Checked='<%# Eval("EXAM_REGISTERED").ToString() == "1" ? true : false %>' 
                                                         AutoPostBack="true" OnCheckedChanged="chkElectiveGrp10_CheckedChanged" />
                                                     <asp:HiddenField ID="hfStatus" runat="server" Value='<%# Eval("STATUS")%>' />
                                                </center>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' />
                                                -
                                                <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' />
                                                <asp:HiddenField ID="hdfCCode" runat="server" Value='<%# Eval("COURSENO")%>' />
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblGrupName" runat="server" Text='<%# Eval("GROUPNAME") %>' />
                                                </center>
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblSub_Type" runat="server" Text='<%# Eval("SUBNAME") %>' />
                                                    <asp:HiddenField ID="hdfsubtype" runat="server" Value='<%# Eval("SUBID")%>' />
                                               </center>
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>' />
                                                    <asp:HiddenField ID="hdfBatchno" runat="server" Value='<%# Eval("BATCHNO")%>' />
                                                </center>
                                            </td>
                                            <td style="width: 14%">
                                                <asp:DropDownList ID="ddlsection" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlsection_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:HiddenField ID="hdfSectionno" runat="server" Value='<%# Eval("SECTIONNO") %>' />
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblIntake" runat="server" Text="" CssClass="label label-info"></asp:Label>
                                                </center>
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblFilled" runat="server" Text="" CssClass="label label-success"></asp:Label>
                                                </center>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row" runat="server" id="divelective11" visible="false">
                    <div class="col-md-12 col-sm-12 col-lg-12">
                        <div class="box box-info">
                            <div class="box-header with-border">
                                <h3 class="box-title">Elective Offered Subjects [Group-11]</h3>
                                <span style="color: green; font-size: 12px;">(Optional)</span></h4>
                                <div class="box-tools pull-right">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                </div>
                            </div>
                            <div class="box-body">
                                <asp:ListView ID="lvElectiveCourseGrp11" runat="server" OnItemDataBound="lvElectiveCourseGrp11_ItemDataBound">
                                    <LayoutTemplate>
                                        <table id="Table3" class="table table-bordered table-hover table-striped table-responsive">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th style="width: 5%">
                                                        <center>Select</center>
                                                    </th>
                                                    <th style="width: 40%">Subject Code and Name</th>
                                                    <th style="width: 5%">
                                                        <center>Group</center>
                                                    </th>
                                                    <th style="width: 5%">
                                                        <center>Type</center>
                                                    </th>
                                                    <th style="width: 5%">
                                                        <center>Credits</center>
                                                    </th>
                                                    <th style="width: 30%">Section and Faculty</th>
                                                    <th style="width: 5%">
                                                        <center>Intake</center>
                                                    </th>
                                                    <th style="width: 5%">
                                                        <center>Filled</center>
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tr id="itemplaceholder" runat="server">
                                            </tr>
                                            <tbody></tbody>
                                        </table>
                                        <tbody>
                                    </LayoutTemplate>
                                    <EmptyDataTemplate>
                                        <tr class="bg-light-blue">
                                            <td colspan="5">
                                                <p>
                                                    <em>----------- No Data Found Of Elective Subjects [Group-11] -----------</em>
                                                </p>
                                            </td>
                                        </tr>
                                    </EmptyDataTemplate>
                                    <ItemTemplate>
                                        <tr id="trCurRow" class="item">
                                            <td>
                                                <center>
                                                     <asp:CheckBox ID="chkElectiveGrp11" runat="server" Enabled='<%# Eval("STATUS").ToString()=="1"? false:true %>' Checked='<%# Eval("EXAM_REGISTERED").ToString() == "1" ? true : false %>' 
                                                         AutoPostBack="true" OnCheckedChanged="chkElectiveGrp11_CheckedChanged" />
                                                     <asp:HiddenField ID="hfStatus" runat="server" Value='<%# Eval("STATUS")%>' />
                                                </center>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' />
                                                -
                                                <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' />
                                                <asp:HiddenField ID="hdfCCode" runat="server" Value='<%# Eval("COURSENO")%>' />
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblGrupName" runat="server" Text='<%# Eval("GROUPNAME") %>' />
                                                </center>
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblSub_Type" runat="server" Text='<%# Eval("SUBNAME") %>' />
                                                    <asp:HiddenField ID="hdfsubtype" runat="server" Value='<%# Eval("SUBID")%>' />
                                               </center>
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>' />
                                                    <asp:HiddenField ID="hdfBatchno" runat="server" Value='<%# Eval("BATCHNO")%>' />
                                                </center>
                                            </td>
                                            <td style="width: 14%">
                                                <asp:DropDownList ID="ddlsection" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlsection_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:HiddenField ID="hdfSectionno" runat="server" Value='<%# Eval("SECTIONNO") %>' />
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblIntake" runat="server" Text="" CssClass="label label-info"></asp:Label>
                                                </center>
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblFilled" runat="server" Text="" CssClass="label label-success"></asp:Label>
                                                </center>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row" runat="server" id="divelective12" visible="false">
                    <div class="col-md-12 col-sm-12 col-lg-12">
                        <div class="box box-info">
                            <div class="box-header with-border">
                                <h3 class="box-title">Elective Offered Subjects [Group-12]</h3>
                                <span style="color: green; font-size: 12px;">(Optional)</span></h4>
                                <div class="box-tools pull-right">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                </div>
                            </div>
                            <div class="box-body">
                                <asp:ListView ID="lvElectiveCourseGrp12" runat="server" OnItemDataBound="lvElectiveCourseGrp12_ItemDataBound">
                                    <LayoutTemplate>
                                        <table id="Table3" class="table table-bordered table-hover table-striped table-responsive">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th style="width: 5%">
                                                        <center>Select</center>
                                                    </th>
                                                    <th style="width: 40%">Subject Code and Name</th>
                                                    <th style="width: 5%">
                                                        <center>Group</center>
                                                    </th>
                                                    <th style="width: 5%">
                                                        <center>Type</center>
                                                    </th>
                                                    <th style="width: 5%">
                                                        <center>Credits</center>
                                                    </th>
                                                    <th style="width: 30%">Section and Faculty</th>
                                                    <th style="width: 5%">
                                                        <center>Intake</center>
                                                    </th>
                                                    <th style="width: 5%">
                                                        <center>Filled</center>
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tr id="itemplaceholder" runat="server">
                                            </tr>
                                            <tbody></tbody>
                                        </table>
                                        <tbody>
                                    </LayoutTemplate>
                                    <EmptyDataTemplate>
                                        <tr class="bg-light-blue">
                                            <td colspan="5">
                                                <p>
                                                    <em>----------- No Data Found Of Elective Subjects [Group-12] -----------</em>
                                                </p>
                                            </td>
                                        </tr>
                                    </EmptyDataTemplate>
                                    <ItemTemplate>
                                        <tr id="trCurRow" class="item">
                                            <td>
                                                <center>
                                                     <asp:CheckBox ID="chkElectiveGrp12" runat="server" Enabled='<%# Eval("STATUS").ToString()=="1"? false:true %>' Checked='<%# Eval("EXAM_REGISTERED").ToString() == "1" ? true : false %>' 
                                                         AutoPostBack="true" OnCheckedChanged="chkElectiveGrp12_CheckedChanged" />
                                                     <asp:HiddenField ID="hfStatus" runat="server" Value='<%# Eval("STATUS")%>' />
                                                </center>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' />
                                                -
                                                <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' />
                                                <asp:HiddenField ID="hdfCCode" runat="server" Value='<%# Eval("COURSENO")%>' />
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblGrupName" runat="server" Text='<%# Eval("GROUPNAME") %>' />
                                                </center>
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblSub_Type" runat="server" Text='<%# Eval("SUBNAME") %>' />
                                                    <asp:HiddenField ID="hdfsubtype" runat="server" Value='<%# Eval("SUBID")%>' />
                                               </center>
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>' />
                                                    <asp:HiddenField ID="hdfBatchno" runat="server" Value='<%# Eval("BATCHNO")%>' />
                                                </center>
                                            </td>
                                            <td style="width: 14%">
                                                <asp:DropDownList ID="ddlsection" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlsection_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:HiddenField ID="hdfSectionno" runat="server" Value='<%# Eval("SECTIONNO") %>' />
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblIntake" runat="server" Text="" CssClass="label label-info"></asp:Label>
                                                </center>
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblFilled" runat="server" Text="" CssClass="label label-success"></asp:Label>
                                                </center>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row" runat="server" id="divelective13" visible="false">
                    <div class="col-md-12 col-sm-12 col-lg-12">
                        <div class="box box-info">
                            <div class="box-header with-border">
                                <h3 class="box-title">Elective Offered Subjects [Group-13]</h3>
                                <span style="color: green; font-size: 12px;">(Optional)</span></h4>
                                <div class="box-tools pull-right">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                </div>
                            </div>
                            <div class="box-body">
                                <asp:ListView ID="lvElectiveCourseGrp13" runat="server" OnItemDataBound="lvElectiveCourseGrp13_ItemDataBound">
                                    <LayoutTemplate>
                                        <table id="Table3" class="table table-bordered table-hover table-striped table-responsive">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th style="width: 5%">
                                                        <center>Select</center>
                                                    </th>
                                                    <th style="width: 40%">Subject Code and Name</th>
                                                    <th style="width: 5%">
                                                        <center>Group</center>
                                                    </th>
                                                    <th style="width: 5%">
                                                        <center>Type</center>
                                                    </th>
                                                    <th style="width: 5%">
                                                        <center>Credits</center>
                                                    </th>
                                                    <th style="width: 30%">Section and Faculty</th>
                                                    <th style="width: 5%">
                                                        <center>Intake</center>
                                                    </th>
                                                    <th style="width: 5%">
                                                        <center>Filled</center>
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tr id="itemplaceholder" runat="server">
                                            </tr>
                                            <tbody></tbody>
                                        </table>
                                        <tbody>
                                    </LayoutTemplate>
                                    <EmptyDataTemplate>
                                        <tr class="bg-light-blue">
                                            <td colspan="5">
                                                <p>
                                                    <em>----------- No Data Found Of Elective Subjects [Group-13] -----------</em>
                                                </p>
                                            </td>
                                        </tr>
                                    </EmptyDataTemplate>
                                    <ItemTemplate>
                                        <tr id="trCurRow" class="item">
                                            <td>
                                                <center>
                                                     <asp:CheckBox ID="chkElectiveGrp13" runat="server" Enabled='<%# Eval("STATUS").ToString()=="1"? false:true %>' Checked='<%# Eval("EXAM_REGISTERED").ToString() == "1" ? true : false %>' 
                                                         AutoPostBack="true" OnCheckedChanged="chkElectiveGrp13_CheckedChanged" />
                                                     <asp:HiddenField ID="hfStatus" runat="server" Value='<%# Eval("STATUS")%>' />
                                                </center>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' />
                                                -
                                                <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' />
                                                <asp:HiddenField ID="hdfCCode" runat="server" Value='<%# Eval("COURSENO")%>' />
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblGrupName" runat="server" Text='<%# Eval("GROUPNAME") %>' />
                                                </center>
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblSub_Type" runat="server" Text='<%# Eval("SUBNAME") %>' />
                                                    <asp:HiddenField ID="hdfsubtype" runat="server" Value='<%# Eval("SUBID")%>' />
                                               </center>
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>' />
                                                    <asp:HiddenField ID="hdfBatchno" runat="server" Value='<%# Eval("BATCHNO")%>' />
                                                </center>
                                            </td>
                                            <td style="width: 14%">
                                                <asp:DropDownList ID="ddlsection" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlsection_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:HiddenField ID="hdfSectionno" runat="server" Value='<%# Eval("SECTIONNO") %>' />
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblIntake" runat="server" Text="" CssClass="label label-info"></asp:Label>
                                                </center>
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblFilled" runat="server" Text="" CssClass="label label-success"></asp:Label>
                                                </center>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row" runat="server" id="divelective14" visible="false">
                    <div class="col-md-12 col-sm-12 col-lg-12">
                        <div class="box box-info">
                            <div class="box-header with-border">
                                <h3 class="box-title">Elective Offered Subjects [Group-14]</h3>
                                <span style="color: green; font-size: 12px;">(Optional)</span></h4>
                                <div class="box-tools pull-right">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                </div>
                            </div>
                            <div class="box-body">
                                <asp:ListView ID="lvElectiveCourseGrp14" runat="server" OnItemDataBound="lvElectiveCourseGrp14_ItemDataBound">
                                    <LayoutTemplate>
                                        <table id="Table3" class="table table-bordered table-hover table-striped table-responsive">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th style="width: 5%">
                                                        <center>Select</center>
                                                    </th>
                                                    <th style="width: 40%">Subject Code and Name</th>
                                                    <th style="width: 5%">
                                                        <center>Group</center>
                                                    </th>
                                                    <th style="width: 5%">
                                                        <center>Type</center>
                                                    </th>
                                                    <th style="width: 5%">
                                                        <center>Credits</center>
                                                    </th>
                                                    <th style="width: 30%">Section and Faculty</th>
                                                    <th style="width: 5%">
                                                        <center>Intake</center>
                                                    </th>
                                                    <th style="width: 5%">
                                                        <center>Filled</center>
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tr id="itemplaceholder" runat="server">
                                            </tr>
                                            <tbody></tbody>
                                        </table>
                                        <tbody>
                                    </LayoutTemplate>
                                    <EmptyDataTemplate>
                                        <tr class="bg-light-blue">
                                            <td colspan="5">
                                                <p>
                                                    <em>----------- No Data Found Of Elective Subjects [Group-14] -----------</em>
                                                </p>
                                            </td>
                                        </tr>
                                    </EmptyDataTemplate>
                                    <ItemTemplate>
                                        <tr id="trCurRow" class="item">
                                            <td>
                                                <center>
                                                     <asp:CheckBox ID="chkElectiveGrp14" runat="server" Enabled='<%# Eval("STATUS").ToString()=="1"? false:true %>' Checked='<%# Eval("EXAM_REGISTERED").ToString() == "1" ? true : false %>' 
                                                         AutoPostBack="true" OnCheckedChanged="chkElectiveGrp14_CheckedChanged" />
                                                     <asp:HiddenField ID="hfStatus" runat="server" Value='<%# Eval("STATUS")%>' />
                                                </center>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' />
                                                -
                                                <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' />
                                                <asp:HiddenField ID="hdfCCode" runat="server" Value='<%# Eval("COURSENO")%>' />
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblGrupName" runat="server" Text='<%# Eval("GROUPNAME") %>' />
                                                </center>
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblSub_Type" runat="server" Text='<%# Eval("SUBNAME") %>' />
                                                    <asp:HiddenField ID="hdfsubtype" runat="server" Value='<%# Eval("SUBID")%>' />
                                               </center>
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>' />
                                                    <asp:HiddenField ID="hdfBatchno" runat="server" Value='<%# Eval("BATCHNO")%>' />
                                                </center>
                                            </td>
                                            <td style="width: 14%">
                                                <asp:DropDownList ID="ddlsection" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlsection_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:HiddenField ID="hdfSectionno" runat="server" Value='<%# Eval("SECTIONNO") %>' />
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblIntake" runat="server" Text="" CssClass="label label-info"></asp:Label>
                                                </center>
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblFilled" runat="server" Text="" CssClass="label label-success"></asp:Label>
                                                </center>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row" runat="server" id="divelective15" visible="false">
                    <div class="col-md-12 col-sm-12 col-lg-12">
                        <div class="box box-info">
                            <div class="box-header with-border">
                                <h3 class="box-title">Elective Offered Subjects [Group-15]</h3>
                                <span style="color: green; font-size: 12px;">(Optional)</span></h4>
                                <div class="box-tools pull-right">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                </div>
                            </div>
                            <div class="box-body">
                                <asp:ListView ID="lvElectiveCourseGrp15" runat="server" OnItemDataBound="lvElectiveCourseGrp15_ItemDataBound">
                                    <LayoutTemplate>
                                        <table id="Table3" class="table table-bordered table-hover table-striped table-responsive">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th style="width: 5%">
                                                        <center>Select</center>
                                                    </th>
                                                    <th style="width: 40%">Subject Code and Name</th>
                                                    <th style="width: 5%">
                                                        <center>Group</center>
                                                    </th>
                                                    <th style="width: 5%">
                                                        <center>Type</center>
                                                    </th>
                                                    <th style="width: 5%">
                                                        <center>Credits</center>
                                                    </th>
                                                    <th style="width: 30%">Section and Faculty</th>
                                                    <th style="width: 5%">
                                                        <center>Intake</center>
                                                    </th>
                                                    <th style="width: 5%">
                                                        <center>Filled</center>
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tr id="itemplaceholder" runat="server">
                                            </tr>
                                            <tbody></tbody>
                                        </table>
                                        <tbody>
                                    </LayoutTemplate>
                                    <EmptyDataTemplate>
                                        <tr class="bg-light-blue">
                                            <td colspan="5">
                                                <p>
                                                    <em>----------- No Data Found Of Elective Subjects [Group-15] -----------</em>
                                                </p>
                                            </td>
                                        </tr>
                                    </EmptyDataTemplate>
                                    <ItemTemplate>
                                        <tr id="trCurRow" class="item">
                                            <td>
                                              <%--  <center>--%>
                                                     <asp:CheckBox ID="chkElectiveGrp15" runat="server" Enabled='<%# Eval("STATUS").ToString()=="1"? false:true %>' Checked='<%# Eval("EXAM_REGISTERED").ToString() == "1" ? true : false %>' 
                                                         AutoPostBack="true" OnCheckedChanged="chkElectiveGrp15_CheckedChanged" />
                                                     <asp:HiddenField ID="hfStatus" runat="server" Value='<%# Eval("STATUS")%>' />
                                              <%--  </center>--%>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' />
                                                -
                                                <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' />
                                                <asp:HiddenField ID="hdfCCode" runat="server" Value='<%# Eval("COURSENO")%>' />
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblGrupName" runat="server" Text='<%# Eval("GROUPNAME") %>' />
                                                </center>
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblSub_Type" runat="server" Text='<%# Eval("SUBNAME") %>' />
                                                    <asp:HiddenField ID="hdfsubtype" runat="server" Value='<%# Eval("SUBID")%>' />
                                               </center>
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>' />
                                                    <asp:HiddenField ID="hdfBatchno" runat="server" Value='<%# Eval("BATCHNO")%>' />
                                                </center>
                                            </td>
                                            <td style="width: 14%">
                                                <asp:DropDownList ID="ddlsection" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlsection_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:HiddenField ID="hdfSectionno" runat="server" Value='<%# Eval("SECTIONNO") %>' />
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblIntake" runat="server" Text="" CssClass="label label-info"></asp:Label>
                                                </center>
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblFilled" runat="server" Text="" CssClass="label label-success"></asp:Label>
                                                </center>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                    </div>
                </div>



                <div class="row" runat="server" id="divMovableSubjects" visible="false">
                    <div class="col-md-12 col-sm-12 col-lg-12">
                        <div class="box box-info">
                            <div class="box-header with-border">
                                <h3 class="box-title">Movable Offered Subjects</h3><span style="color: green; font-size: 12px;">(Optional)</span></h4>
                                <div class="box-tools pull-right">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                </div>
                            </div>
                            <div class="box-body">
                                <asp:ListView ID="lvMovableSubjects" runat="server" OnItemDataBound="lvMovableSubjects_ItemDataBound">
                                    <LayoutTemplate>
                                        <table id="Table3" class="table table-bordered table-hover table-striped  table-responsive">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th style="width:5%"><center>Select</center></th>
                                                    <th style="width:40%">Subject Code and Name</th>
                                                    <th style="width:5%"><center>Group</center></th>
                                                    <th style="width:5%"><center>Type</center></th>
                                                    <th style="width:5%"><center>Credits</center></th>
                                                    <th style="width:30%">Section and Faculty</th>
                                                    <th style="width:5%"><center>Intake</center></th>
                                                    <th style="width:5%"><center>Filled</center></th>
                                                </tr>
                                            </thead>
                                            <tr id="itemplaceholder" runat="server">
                                            </tr>
                                            <tbody></tbody>
                                        </table>
                                        <tbody>
                                    </LayoutTemplate>
                                    <EmptyDataTemplate>
                                        <tr class="bg-light-blue">
                                            <td colspan="5">
                                                <p>
                                                    <em>----------- No Data Found Of Movable Subjects -----------</em>
                                                </p>
                                            </td>
                                        </tr>
                                    </EmptyDataTemplate>
                                    <ItemTemplate>
                                        <tr id="trCurRow" class="item">

                                            <td>  
                                                <center>
                                                     <asp:CheckBox ID="chkMovable" runat="server" Enabled='<%# Eval("STATUS").ToString()=="1"? false:true %>' Checked='<%# Eval("EXAM_REGISTERED").ToString() == "1" ? true : false %>' AutoPostBack="true" OnCheckedChanged="chkMovable_CheckedChanged" />
                                                     <asp:HiddenField ID="hfStatus" runat="server" Value='<%# Eval("STATUS")%>' />
                                                </%--center--%>                                             
                                            </td>
                                            <td>
                                               <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' /> - <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' />
                                                <asp:HiddenField ID="hdfCCode" runat="server" Value='<%# Eval("COURSENO")%>' />
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblGrupName" runat="server" Text='<%# Eval("GROUPNAME") %>' />
                                                </center>
                                            </td>
                                            <td>
                                               <center>
                                                    <asp:Label ID="lblSub_Type" runat="server" Text='<%# Eval("SUBNAME") %>' />
                                                    <asp:HiddenField ID="hdfsubtype" runat="server" Value='<%# Eval("SUBID")%>' />
                                               </center>
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>' />
                                                    <asp:HiddenField ID="hdfBatchno" runat="server" Value='<%# Eval("BATCHNO")%>' />
                                                </center>
                                            </td>
                                            <td style="width: 14%">
                                                <asp:DropDownList ID="ddlsection" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlsection_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:HiddenField ID="hdfSectionno" runat="server" Value='<%# Eval("SECTIONNO") %>' />
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblIntake" runat="server" Text="" CssClass="label label-info"></asp:Label>
                                                </center>
                                            </td>
                                            <td>
                                                <center>
                                                    <asp:Label ID="lblFilled" runat="server" Text="" CssClass="label label-success"></asp:Label>
                                                </center>
                                            </td>

                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                    </div>
                </div>

                <div>
                    <div class="box-footer" runat="server" visible="false" id="divButton" style="background-color: transparent">
                        <p class="text-center">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClientClick="return confirm ('Do you want to Submit Selected Subject!');" CssClass="btn btn-outline-info" OnClick="btnSubmit_Click" ValidationGroup="SUBMIT" Enabled="false" />
                            <asp:LinkButton ID="btnPrintRegSlip" runat="server" OnClick="btnPrintRegSlip_Click" Enabled="false" OnClientClick="return confirm ('Do you want to print registration slip!');" CssClass="btn btn-outline-primary"><i class="fa fa-file-pdf-o" aria-hidden="true"></i> Registration Slip</asp:LinkButton>
                            <asp:Button ID="btnPrePrintClallan" runat="server" Text="Re-Print Challan" OnClick="btnPrePrintClallan_Click" />
                        <p>
                    </div>
                </div>

                <!--End region all offered elective courses-->
            </div>

            <asp:HiddenField ID="HFCredit" runat="server" />

            <asp:HiddenField ID="hdfTotNoCourses" runat="server" Value="0" />

            <div id="divMsg" runat="server">
            </div>

            <asp:LinkButton ID="lnkDummy1" runat="server"></asp:LinkButton>
            <ajaxToolKit:ModalPopupExtender ID="mpe1" BehaviorID="mdlPopupDel"
                runat="server" TargetControlID="lnkDummy1" PopupControlID="divPreQuisite"
                CancelControlID="btnNoDel"
                BackgroundCssClass="bg-light-white" />
            <asp:Panel ID="divPreQuisite" runat="server" Style="display: none" CssClass="modalPopup">
                <div style="text-align: center; border: 2px solid gray; background-color: #fff;">
                    <table>
                        <tr>
                            <td align="center">
                                <asp:ListView ID="lvPreRequisiteHistory" runat="server">
                                    <LayoutTemplate>
                                        <h4>Prerequisite History</h4>

                                        <table id="Table7" class="table table-bordered table-hover table-striped">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th>Subject    
                                                    </th>
                                                    <th>Pre Requisite Subject
                                                    </th>
                                                    <th>Status
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemplaceholder" runat="server"></tr>
                                            </tbody>
                                        </table>
                                    </LayoutTemplate>

                                    <EmptyDataTemplate>
                                        <tr class="bg-light-blue">
                                            <td colspan="5">
                                                <p>
                                                    <em>No Prerequisite  Found <em>
                                                </p>
                                            </td>
                                        </tr>
                                    </EmptyDataTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>

                                                <asp:Label ID="lblSubject" runat="server" Text='<%# Eval("COURSE_NAME") %>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblRequisited" runat="server" Text='<%# Eval("RE_COURSE_NAME") %>' />

                                            </td>
                                            <td>
                                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("STATUS") %>' />
                                            </td>
                                        </tr>


                                    </ItemTemplate>
                                </asp:ListView>

                            </td>

                        </tr>
                        <tr>
                            <td align="center">
                                <%--  <asp:Button ID="btnOkDel" runat="server" Text="Yes"  Width="50px" />--%>

                                <asp:LinkButton ID="btnNoDel" runat="server" CssClass="btn btn-outline-primary btn-block"><i class="fa fa-check"></i> OK</asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center"></td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>

            <asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>
            <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mpe" runat="server"
                PopupControlID="pnlPopup" TargetControlID="lnkDummy" BackgroundCssClass="modalBackground" CancelControlID="btnHide">
            </ajaxToolKit:ModalPopupExtender>
            <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none">

                <div class="body panel panel-primary">
                    <div class="panel-heading text-center">Time Table</div>
                    <div class="panel-body">
                        <table id="Table2" runat="server">
                            <tr>
                                <td style="text-align: center; background-color: #f9f9f9;">
                                    <asp:ListView ID="lvTimeTable" runat="server">
                                        <LayoutTemplate>


                                            <table id="Table7" class="table table-bordered table-hover table-striped">
                                                <thead>
                                                    <tr>
                                                        <th class="text-center">PERIOD    
                                                        </th>
                                                        <th class="text-center">MONDAY
                                                        </th>
                                                        <th class="text-center">TUESDAY
                                                        </th>
                                                        <th class="text-center">WEDNESDAY
                                                        </th>
                                                        <th class="text-center">THURSDAY
                                                        </th>
                                                        <th class="text-center">FRIDAY
                                                        </th>
                                                        <th class="text-center">SATURDAY
                                                        </th>
                                                        <%-- <th>SUNDDAY
                                                    </th>--%>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemplaceholder" runat="server"></tr>
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <EmptyDataTemplate>
                                            <tr class="bg-light-blue">
                                                <td colspan="5">
                                                    <p>
                                                        <em>No Time Table Found For Selected Subjects <em>
                                                    </p>
                                                </td>
                                            </tr>
                                        </EmptyDataTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%#Eval("SLOT") %>

                                                <td>
                                                    <asp:Label ID="Label1" Text='<%#Eval("MONDAY") %>' ToolTip='<%#Eval("MONDAY1") %>' runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" ToolTip='<%#Eval("TUESDAY1") %>' Text='<%#Eval("TUESDAY") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label3" runat="server" ToolTip='<%#Eval("WEDNESDAY1") %>' Text='<%#Eval("WEDNESDAY") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label4" runat="server" ToolTip='<%#Eval("THURSDAY1") %>' Text='<%#Eval("THURSDAY") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label5" runat="server" ToolTip='<%#Eval("FRIDAY1") %>' Text='<%#Eval("FRIDAY") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label6" runat="server" ToolTip='<%#Eval("SATERDAY1") %>' Text=' <%#Eval("SATERDAY") %>'></asp:Label>


                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center"></td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <span style="color: #993366"><b>Overlapping Percentage :</b>
                                        <asp:Label ID="lblOverlap" runat="server" ForeColor="#993366" Text=""></asp:Label>

                                    </span>
                                    <%--<div id="olap" runat="server">

                                </div>  --%>  
                                
                                </td>
                            </tr>
                        </table>
                    </div>

                    <%-- <asp:Button ID="btnHide" runat="server"  CssClass="btn btn-outline-primary btn-block" Text="OK" />--%>
                    <div class="panel-footer text-center">
                        <asp:LinkButton ID="btnHide" runat="server" CssClass="btn btn-outline-info"><i class="fa fa-check"></i> OK</asp:LinkButton>
                    </div>

                </div>
            </asp:Panel>


        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>

    <script type="text/javascript" language="javascript">
        //        //To change the colour of a row on click of check box inside the row..
        //        $("tr :checkbox").live("click", function() {
        //        $(this).closest("tr").css("background-color", this.checked ? "#FFFFD2" : "#FFFFFF");
        //        });


        //function cancelDelClick() {
        //       //  find the confirm ModalPopup and hide it
        //    this._popup.hide();
        //    //  clear the event source
        //    this._source = null;
        //    this._popup = null;
        //}


        function MutExChkList(chk) {
            //var chkList = chk.parentNode.parentNode.parentNode;
            //var chks = chkList.getElementsByTagName("input");
            //for (var i = 0; i < chks.length; i++) {
            //    if (chks[i] != chk && chk.checked) {
            //        chks[i].checked = false;
            //    }
            //}

            //var count=0;
            //var tbl = document.getElementById('tblElectiveCourse');
            //var list = 'lvElectiveCourse';
            //var dataRows = tbl.getElementsByTagName('tr');
            //if (dataRows != null) {
            //    for (i = 0; i < dataRows.length - 1; i++) {

            //        if (chk.checked) {
            //            count++;
            //        }
            //    }
            //    if(count>1)
            //    {
            //        alert("Please select only one elective subject");
            //        for (i = 0; i < dataRows.length - 1; i++) {

            //            if (chk.checked) {
            //                document.getElementById(chkid).checked = false;
            //            }
            //        }
            //    }
        }


        function validateAssign() {
            var txtTot = document.getElementById('<%= hdfTot.ClientID %>').value;

            if (txtTot == 0) {
                alert('Please Select atleast one Subject selected in Subject list');
                return false;
            }
            else
                return true;
        }


        function MutualExclusive(radio) {
            var dvData = document.getElementById("dvData");
            var inputs = dvData.getElementsByTagName("input");
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].type == "radio") {
                    if (inputs[i] != radio) {
                        inputs[i].checked = false;

                    }
                }
            }
        }

        function MutualExclusiveGrp(radio) {
            var dvData = document.getElementById("dvDataGrp");
            var inputs = dvData.getElementsByTagName("input");
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].type == "radio") {
                    if (inputs[i] != radio) {
                        inputs[i].checked = false;

                    }
                }
            }
        }

        function CheckSelectionCount(chk) {
            var count = -2;
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (count == 2) {
                    chk.checked = false;
                    alert("You have reached maximum limit!");
                    return;
                }
                else if (count < 2) {
                    if (e.checked == true) {
                        count += 1;
                    }
                }
                else {
                    return;
                }
            }
        }

        function IsNumeric(textbox) {
            if (textbox != null && textbox.value != "") {
                if (isNaN(textbox.value)) {
                    document.getElementById(textbox.id).value = '';
                }
            }
        }

        function showConfirm() {
            var ret = confirm('Do you Really Confirm to Submit this Subjects for Subject Registration?');
            if (ret == true)
                return true;
            else
                return false;
        }




    </script>

</asp:Content>
