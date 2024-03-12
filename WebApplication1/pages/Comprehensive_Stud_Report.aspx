<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"
    CodeFile="Comprehensive_Stud_Report.aspx.cs" Inherits="ACADEMIC_Comprehensive_Stud_Report"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Content/jquery.js" type="text/javascript"></script>
    <script src="../Content/jquery.dataTables.js" language="javascript" type="text/javascript"></script>
    <script type="text/javascript" charset="utf-8">
        $(document).ready(function () {

            $(".display").dataTable({
                "bJQueryUI": true,
                "sPaginationType": "full_numbers"
            });

        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {

            //$('.btn_close').click(function () {
            //    $('#toggle_div').slideUp();
            //});

            $(".btn_close").click(function () {
                $('#toggle_div').hide();
            });
        });
    </script>
    <style>
        th {
            text-align: center;
        }
    </style>
    <style>
        col0 {
            display: none;
        }
    </style>
    <%--<div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updStudent"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>--%>
    <%--  <asp:UpdatePanel ID="updStudent" runat="server">
        <ContentTemplate>--%>

    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title"><b>COMPREHENSIVE STUDENT INFORMATION</b></h3>
                </div>
                <div class="box-body">

                    <asp:Label ID="lblMsg" runat="server" SkinID="Msglbl"></asp:Label>

                    <asp:Panel ID="pnlSearch" runat="server">
                        <div class="panel panel-info">
                            <div class="box-body">
                                <div class="col-xs-12 col-md-offset-2 col-sm-offset-2">
                                    <div class="col-md-6 col-xs-12 col-sm-3" style="margin-top: 5px; text-align: left;">
                                        <label>Search by:&nbsp;&nbsp;&nbsp;</label>
                                        <asp:RadioButton ID="rdoSearchAll" Text="Name Only" Checked="true" runat="server" GroupName="stud" />&nbsp;&nbsp;&nbsp;
                                <asp:RadioButton ID="rdoAll" Text="Univ. Reg. No. / TAN/PAN" runat="server"
                                    GroupName="stud" />&nbsp;&nbsp;&nbsp;                                        
                                    </div>
                                    <div class="col-md-3 col-xs-12 col-sm-4">
                                        <asp:TextBox ID="txtEnrollmentSearch" runat="server" ValidationGroup="submit" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3 col-xs-12 col-sm-5" style="float: left;">
                                        <%-- <button style="background-color:#3c8dbc;border:none;color:#fff;font-weight:bold;" ID="btnSearch" runat="server" c ><i class="fa fa-search" aria-hidden="true"></i>Search
                                        --%>
                                        <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" ValidationGroup="submit" Text="Search" CssClass="btn btn-outline-info" />
                                        <asp:RequiredFieldValidator ID="rfvSearch" runat="server" ControlToValidate="txtEnrollmentSearch"
                                            Display="None" ErrorMessage="Please Enter Univ. Reg. No. Or TAN/PAN" ValidationGroup="submit">
                                        </asp:RequiredFieldValidator>

                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True"
                                            ShowSummary="False" ValidationGroup="submit" />
                                        <%-- </button>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <div class="col-md-12" id="div1" runat="server" visible="false">
                        <asp:Panel ID="PaneldetailsSearch" runat="server" ScrollBars="Auto">
                            <asp:ListView ID="lvStudentRecords" runat="server">
                                <LayoutTemplate>
                                    <div id="listViewGrid">

                                        <h4>Search Results</h4>

                                        <table class="table table-hover table-bordered table-responsive">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th>Student Name
                                                    </th>
                                                    <th>TAN/PAN
                                                    </th>
                                                    <th>Degree
                                                    </th>
                                                    <th>Branch
                                                    </th>
                                                    <th>Year
                                                    </th>
                                                    <th>Semester
                                                    </th>
                                                    <th>Admission Batch
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                        </table>
                                    </div>
                                </LayoutTemplate>
                                <EmptyDataTemplate>
                                </EmptyDataTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <%-- <%# Eval("NAME")%>   --%>
                                            <asp:LinkButton ID="lnkNAME" runat="server" OnClick="lnkNAME_Click" Text='<%# Eval("NAME") %>' CommandName='<%# Eval("ENROLLMENTNO") %>'></asp:LinkButton>
                                        </td>
                                        <td>
                                            <%# Eval("ENROLLMENTNO")%>
                                        </td>
                                        <td>
                                            <%# Eval("DEGREENO")%>
                                        </td>
                                        <td>
                                            <%# Eval("SHORTNAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("YEARNAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("SEMESTERNAME")%>
                                        </td>
                                        <td>
                                            <%# Eval("BATCHNAME")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <EmptyDataTemplate>
                                    <div align="center" class="data_label">
                                        -- No Student Record Found --
                                    </div>
                                </EmptyDataTemplate>
                            </asp:ListView>
                        </asp:Panel>
                    </div>
                    <%--  THIS PANEL IS FOR SHOWING STUDENT INFORMATION--%>
                    <div class="row" id="div2" runat="server">
                        <div class="col-md-12 form-group">
                            <div class="box box-primary">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Student Information</h3>
                                    <div class="box-tools pull-right">
                                        <a id="menu-toggle" style="cursor: pointer; transition: 0.8s;" onclick="javascript:toggleExpansion(this,'divStudentInfo')">
                                            <i class="glyphicon glyphicon-minus text-blue"></i>
                                        </a>
                                    </div>
                                </div>

                                <div class="box-body">
                                    <div id="divStudentInfo" runat="server" style="display: block;" class="col-md-12">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-5 col-xs-12 form-group">
                                                    <ul class="list-group list-group-unbordered">
                                                        <li class="list-group-item"><b>Student Name :</b><a>
                                                            <asp:Label ID="lblName" runat="server" Font-Bold="True"></asp:Label></a> </li>

                                                        <li class="list-group-item"><b>Degree :</b><a>
                                                            <asp:Label ID="lblDegree" runat="server" Font-Bold="True"></asp:Label></a>
                                                            <br />
                                                        </li>
                                                        <li class="list-group-item"><b>Branch :</b><a>
                                                            <asp:Label ID="lblBranch" runat="server" Font-Bold="True" Style="font-size: 13px;"></asp:Label></a>
                                                            <br />
                                                        </li>
                                                        <li class="list-group-item"><b>Semester/Year :</b><a>
                                                            <asp:Label ID="lblSemester" runat="server" Font-Bold="True"></asp:Label></a>
                                                            <li class="list-group-item">
                                                                <strong>Local Address:</strong>
                                                                <asp:Label ID="lblLAdd" Style="color: #3c8dbc;" runat="server" Font-Bold="True"></asp:Label>
                                                            </li>
                                                        </li>
                                                    </ul>
                                                </div>


                                                <div class="col-md-5 col-xs-12 form-group">
                                                    <ul class="list-group list-group-unbordered">
                                                        <li class="list-group-item"><b>Univ. Reg. No. :</b><a>
                                                            <asp:Label ID="lblRegNo" runat="server" Font-Bold="True"></asp:Label></a> </li>
                                                        <li class="list-group-item"><b>TAN/PAN :</b><a>
                                                            <asp:Label ID="lblEnrollNo" runat="server" Font-Bold="True"></asp:Label></a>
                                                        </li>
                                                        <li class="list-group-item"><b>Father's Name :</b><a>
                                                            <asp:Label ID="lblMName" runat="server" Font-Bold="True"></asp:Label></a>
                                                        </li>
                                                        <li class="list-group-item"><b>Mother's Name :</b><a>
                                                            <asp:Label ID="lblMotherName" runat="server" Font-Bold="True"></asp:Label></a>
                                                        </li>
                                                        <li class="list-group-item">
                                                            <strong>Permanent Address :</strong>
                                                            <asp:Label ID="lblPAdd" Style="color: #3c8dbc;" runat="server" Font-Bold="True"></asp:Label>
                                                        </li>
                                                    </ul>
                                                </div>
                                                <div class="form-group col-md-2 col-xs-12 text-center">
                                                    <asp:Image ID="imgPhoto" runat="server" Height="120px" Width="128px" />
                                                    <div style="margin-top: 50px; border: 1px solid #000; padding: 5px;">
                                                        <asp:Image ID="imgSign" runat="server" Height="50px" Width="128px" />
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-5 col-xs-12 form-group">
                                                    <ul class="list-group list-group-unbordered">

                                                        <li class="list-group-item"><b>Local City:</b><a>
                                                            <asp:Label ID="lblCity" runat="server" Font-Bold="True"></asp:Label></a>
                                                        </li>

                                                        <li class="list-group-item"><b>Mobile No :</b><a>
                                                            <asp:Label ID="lblMobNo" runat="server" Font-Bold="True"></asp:Label></a>
                                                        </li>

                                                        <li class="list-group-item"><b>Aadhar No :</b><a>
                                                            <asp:Label ID="lblAadharNo" runat="server" Font-Bold="True"></asp:Label></a>
                                                        </li>

                                                        <li class="list-group-item"><b>Nationality :</b><a>
                                                            <asp:Label ID="lblNationality" runat="server" Text='<%# Eval("") %>' Font-Bold="True"></asp:Label></a>
                                                        </li>

                                                        <li class="list-group-item"><b>Caste :</b><a>
                                                            <asp:Label ID="lblCaste" runat="server" Font-Bold="True"></asp:Label></a>
                                                        </li>

                                                        <li class="list-group-item"><b>Blood Group :</b><a>
                                                            <asp:Label ID="lblBlood" runat="server" Font-Bold="True"></asp:Label></a>
                                                        </li>

                                                    </ul>
                                                </div>

                                                <div class="col-md-7 col-xs-12 form-group">
                                                    <ul class="list-group list-group-unbordered">


                                                        <li class="list-group-item"><b>Permanent City :</b><a>
                                                            <asp:Label ID="lblPCity" runat="server" Font-Bold="True"></asp:Label></a>
                                                        </li>

                                                        <li class="list-group-item"><b>Email ID :</b><a>
                                                            <asp:Label ID="lblMailID" runat="server" Font-Bold="True"></asp:Label></a>
                                                        </li>

                                                        <li class="list-group-item"><b>Date of Birth :</b><a>
                                                            <asp:Label ID="lblDOB" runat="server" Font-Bold="True"></asp:Label></a>
                                                        </li>

                                                        <li class="list-group-item"><b>Religion :</b><a>
                                                            <asp:Label ID="lblReligion" runat="server" Font-Bold="True"></asp:Label></a>
                                                        </li>
                                                        <li class="list-group-item" style="display: none;"><b>Physical Handicapped :</b><a>
                                                            <asp:Label ID="lblHandicap" runat="server" Font-Bold="True"></asp:Label></a>
                                                        </li>
                                                        <li class="list-group-item"><b>Category :</b><a>
                                                            <asp:Label ID="lblCategory" runat="server" Font-Bold="True"></asp:Label></a>
                                                        </li>
                                                    </ul>

                                                </div>



                                            </div>

                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <%--  THIS PANEL IS FOR SHOWING REGISTRATION STATUS--%>
                    <div class="row" id="div4" runat="server">
                        <div class="col-md-12 form-group">
                            <div class="box box-primary">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Semester/Year Registration</h3>
                                    <div class="box-tools pull-right">
                                        <a id="menu-toggle-1" style="cursor: pointer; transition: 0.8s;" onclick="javascript:toggleExpansion(this,'divRegistrationStatus')">
                                            <i class="glyphicon glyphicon-minus text-blue"></i>
                                        </a>
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div class="col-md-12 from-group" id="divinfo" runat="server" visible="false">
                                        <div class="col-md-8 from-group">
                                            <label>Session : </label>
                                            <asp:Label runat="server" Font-Bold="true" Style="color: #3c8dbc;" ID="lblsession"></asp:Label>
                                        </div>
                                        <div class="col-md-4 from-group">
                                            <%-- <label>Semester : </label>--%>
                                            <asp:Label ID="lblRegSemYear" runat="server" Font-Bold="true" Style="color: #333;"></asp:Label>
                                            <asp:Label runat="server" Font-Bold="true" Style="color: #3c8dbc;" ID="lblsmester"></asp:Label>
                                        </div>
                                    </div>
                                    <div id="divRegistrationStatus" runat="server" style="display: block; transition: 0.8s;" class="col-md-12">
                                        <asp:ListView ID="lvRegStatus" runat="server">
                                            <LayoutTemplate>
                                                <div>
                                                    <table class="table table-hover table-bordered table-striped">
                                                        <thead>
                                                            <%-- <tr style="background-color: aliceblue; text-align: center;">
                                                                        <th colspan="6" style="text-align: center">Current Semester Registration Details
                                                                        </th>
                                                                    </tr>--%>
                                                            <tr class="bg-light-blue">

                                                                <th>Subject Code -- Subject Name
                                                                </th>
                                                                <th>Subject Type
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </LayoutTemplate>
                                            <EmptyDataTemplate>
                                                <div align="left" class="info">
                                                    There are no records to display
                                                </div>
                                            </EmptyDataTemplate>
                                            <ItemTemplate>
                                                <tr>

                                                    <td>
                                                        <%# Eval("SUBJECTNAME") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("SUBJECTTYPE") %>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>


                    <%--  THIS PANEL IS FOR SHOWING STUDENT ATTENDANCE--%>
                    <div class="row" id="div5" runat="server">
                        <div class="col-md-12 form-group">
                            <div class="box box-primary">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Student Attendance</h3>
                                    <div class="box-tools pull-right">
                                        <a id="menu-toggle-2" style="cursor: pointer;" onclick="javascript:toggleExpansion(this,'divAttendance')">
                                            <i class="glyphicon glyphicon-minus text-blue"></i>
                                        </a>
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div class="col-md-12 from-group" id="divattinfo" runat="server" visible="false">
                                        <div class="col-md-8 from-group">
                                            <label>Session : </label>
                                            <asp:Label runat="server" Font-Bold="true" Style="color: #3c8dbc;" ID="lblattsession"></asp:Label>
                                        </div>
                                        <div class="col-md-4 from-group">
                                            <%--<label>Semester : </label>--%>
                                            <asp:Label ID="lblAttSemYear" runat="server" Font-Bold="true" Style="color: #333;"></asp:Label>
                                            <asp:Label runat="server" Font-Bold="true" Style="color: #3c8dbc;" ID="lblattsemester"></asp:Label>
                                        </div>
                                        <div class="col-md-4 from-group">
                                            <%--  <label>Semester : </label>--%>
                                            <asp:Label runat="server" Font-Bold="true" Style="color: #3c8dbc;" ID="lblattScheme" Enabled="false"></asp:Label>
                                        </div>
                                    </div>
                                    <div id="divAttendance" style="display: block" class="col-md-12">
                                        <div id="div3" style="display: block;">
                                            <asp:ListView ID="lvAttendance" runat="server">
                                                <LayoutTemplate>
                                                    <div>
                                                        <table class="table table-hover table-bordered table-striped">
                                                            <thead>
                                                                <%--<tr style="background-color: aliceblue; text-align: center">
                                                                    <th colspan="6" style="text-align: center">Attendance Details
                                                                    </th>
                                                                </tr>--%>
                                                                <tr class="bg-light-blue">
                                                                    <th>Subject Code -- Subject Name
                                                                    </th>
                                                                    <th>Faculty Name
                                                                    </th>
                                                                    <th>Total Classes
                                                                    </th>
                                                                    <th>Present
                                                                    </th>
                                                                    <th>Absent
                                                                    </th>
                                                                    <th>OD
                                                                    </th>
                                                                    <th>Percentage
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr id="itemPlaceholder" runat="server" />
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </LayoutTemplate>
                                                <EmptyDataTemplate>
                                                    <div align="left" class="info">
                                                        There are no records to display
                                                    </div>
                                                </EmptyDataTemplate>
                                                <ItemTemplate>
                                                    <tr>

                                                        <td>
                                                            <asp:LinkButton Text='<%# Eval("COURSENAME") %>' ID="lnkCourseWiseAttendance" CommandArgument='<%# Eval("COURSENO") %>'
                                                                OnCommand="lnkCourseWiseAttendance_Command" runat="server" ToolTip="Please Select Course to see Report" />
                                                        </td>
                                                        <td>
                                                            <%# Eval("UA_NAME") %>
                                                        </td>
                                                        <td>
                                                            <%# Eval("TOTAL_ATT") %>
                                                        </td>
                                                        <td>
                                                            <%# Eval("PRESENT") %>
                                                        </td>
                                                        <td>
                                                            <%# Eval("ABSENT") %>
                                                        </td>
                                                        <td>
                                                            <%# Eval("OD") %>
                                                        </td>
                                                        <td>
                                                            <%# Eval("ATT_PER") %>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <%--  THIS PANEL IS FOR  SHOWING EXAMINATION MARKS--%>
                    <div class="row" id="div6" runat="server">
                        <div class="col-md-12 form-group">
                            <div class="box box-primary">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Internal Marks</h3>
                                    <div class="box-tools pull-right">
                                        <a id="menu-toggle-3" style="cursor: pointer;" onclick="javascript:toggleExpansion(this,'divTestMark')">
                                            <i class="glyphicon glyphicon-minus text-blue"></i>
                                        </a>
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div class="col-md-12 from-group" id="divtestmarkinfo" runat="server" visible="false">
                                        <div class="col-md-8 from-group">
                                            <label>Session : </label>
                                            <asp:Label runat="server" Font-Bold="true" Style="color: #3c8dbc;" ID="lbltestsession"></asp:Label>
                                        </div>
                                        <div class="col-md-4 from-group">
                                            <%--  <label>Semester : </label>--%>
                                            <asp:Label ID="lblSemYear" runat="server" Font-Bold="true" Style="color: #333;"></asp:Label>
                                            <asp:Label runat="server" Font-Bold="true" Style="color: #3c8dbc;" ID="lbltestsemester"></asp:Label>
                                        </div>
                                    </div>
                                    <div id="divTestMark" style="display: block" class="col-md-12">
                                        <asp:GridView runat="server" Width="100%" OnRowDataBound="gvTestMark_RowDataBound" EmptyDataText="There are no records to display."
                                            OnRowCreated="gvTestMark_RowCreated" GridLines="None" CssClass="table table-hover table-bordered table-striped" ID="gvTestMark">
                                            <RowStyle HorizontalAlign="Center" ForeColor="Black"></RowStyle>
                                            <HeaderStyle HorizontalAlign="left" CssClass="bg-light-blue" Font-Bold="true" ForeColor="White" />

                                            <EmptyDataTemplate>
                                                <div align="left" class="info">
                                                    There are no records to display
                                                </div>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row" id="div7" runat="server">
                        <div class="col-md-12 form-group">
                            <div class="box box-primary">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Result Details</h3>
                                    <div class="box-tools pull-right">
                                        <a id="menu-toggle-4" style="cursor: pointer;" onclick="javascript:toggleExpansion(this,'divResult')">
                                            <i class="glyphicon glyphicon-minus text-blue"></i></a>
                                    </div>
                                </div>

                                <div class="box-body">
                                    <div id="divResult" style="display: block;">
                                        <asp:GridView ID="gvParentGrid" runat="server" DataKeyNames="TR_RESULT_NO" Width="100%"
                                            CssClass="table table-hover table-bordered" GridLines="None"
                                            BorderStyle="Solid" BorderWidth="1px" OnRowDataBound="gvParentGrid_RowDataBound"
                                            ShowFooter="false" Visible="true" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true">
                                            <HeaderStyle CssClass="bg-light-blue" Font-Bold="true" ForeColor="White" />

                                            <Columns>
                                                <asp:TemplateField ItemStyle-Width="25px" ItemStyle-HorizontalAlign="Center" HeaderText="VIEW DETAILS"
                                                    HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <div id="divc1" runat="server">
                                                            <a href="JavaScript:divexpandcollapse('div<%# Eval("TR_RESULT_NO") %>');">
                                                                <img alt="VIEW" src="../IMAGES/viewdetail.png" height="25px" id='CLOSE<%# Eval("TR_RESULT_NO") %>' border="0" title="VIEW DETAILS" />
                                                        </div>
                                                        <asp:HiddenField ID="hdfcollegeid" runat="server" Value='<%# Eval("college_id") %>' />
                                                        <asp:HiddenField ID="hdfSession" runat="server" Value='<%# Eval("SESSIONNO") %>' />
                                                        <asp:HiddenField ID="hdfdegreeno" runat="server" Value='<%# Eval("degreeno") %>' />
                                                        <asp:HiddenField ID="hdfbranchno" runat="server" Value='<%# Eval("branchno") %>' />
                                                        <asp:HiddenField ID="hdfSemester" runat="server" Value='<%# Eval("SEMESTERNO") %>' />
                                                        <asp:HiddenField ID="hdfIDNo" runat="server" Value='<%# Eval("IDNO") %>' />
                                                        <asp:HiddenField ID="hdfScheme" runat="server" Value='<%# Eval("SCHEMENO") %>' />
                                                        <asp:HiddenField ID="hdfstudType" runat="server" Value='<%# Eval("RESULT_TYPE") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>



                                                <asp:BoundField DataField="SEMESTERNO" HeaderText="SEMESTER" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center" />
                                                <%-- <asp:BoundField DataField="SESSION" HeaderText="SESSION" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center" />--%>
                                                <asp:BoundField DataField="REGD_CREDITS" HeaderText="TOTAL CREDITS" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="EARN_CREDITS" HeaderText="EARNED CREDITS" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="SGPA" HeaderText="GPA" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="CGPA" HeaderText="CGPA" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center" />

                                                <%-- <asp:BoundField DataField="PASSFAIL" ItemStyle-Font-Bold="true" HeaderText="RESULT" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center" />--%>


                                                <asp:TemplateField ItemStyle-Width="25px" ItemStyle-HorizontalAlign="Center" HeaderText="Print GC" Visible="false"
                                                    HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <div id="divc2" runat="server" style="display: none;">
                                                            <asp:LinkButton ID="lnkbtnStudGC" runat="server" OnClick="lnkbtnStudGC_Click"><i class="fa fa-file"></i></asp:LinkButton>
                                                        </div>
                                                        <asp:HiddenField ID="hdfIDNogc" runat="server" Value='<%# Eval("IDNO") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td colspan="100%" id="toggle_div">
                                                                <div id='div<%# Eval("TR_RESULT_NO") %>' style="display: none; position: relative; left: 30px; overflow: auto">

                                                                    <asp:GridView ID="gvChildGrid" runat="server" BorderStyle="Double"
                                                                        CssClass="table table-hover table-bordered table-striped" GridLines="None"
                                                                        Width="95%" ShowFooter="true" Visible="true" AutoGenerateColumns="false"
                                                                        ShowHeaderWhenEmpty="true">

                                                                        <HeaderStyle CssClass="bg-light-blue" Font-Bold="true" ForeColor="White" />
                                                                        <FooterStyle Font-Bold="true" ForeColor="White" />
                                                                        <RowStyle />
                                                                        <AlternatingRowStyle BackColor="White" />
                                                                        <HeaderStyle CssClass="bg-light-blue" Font-Bold="true" ForeColor="White" />
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="SR.NO." ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <%# Container.DataItemIndex+1 %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="CCODE" HeaderText="SUBJECT CODE" HeaderStyle-HorizontalAlign="Left" />
                                                                            <asp:BoundField DataField="COURSENAME" HeaderText="SUBJECT NAME" HeaderStyle-HorizontalAlign="Left" />
                                                                            <asp:TemplateField HeaderText="INTERNAL" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblciemark" runat="server" Text='<%# Eval("INTERMARKC") %>' HeaderStyle-HorizontalAlign="Left" />
                                                                                </ItemTemplate>
                                                                                <FooterStyle HorizontalAlign="Center" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="EXTERNAL" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblesemark" runat="server" Text='<%# Eval("EXTERMARKC") %>' HeaderStyle-HorizontalAlign="Left" />
                                                                                </ItemTemplate>
                                                                                <FooterStyle HorizontalAlign="Center" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="TOTAL" HeaderStyle-HorizontalAlign="Center"
                                                                                ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbltotmark" runat="server" Text='<%# Eval("MARKTOTAL") %>' HeaderStyle-HorizontalAlign="Left" />
                                                                                </ItemTemplate>
                                                                                <FooterStyle HorizontalAlign="Center" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="GRADE POINT" Visible="false" HeaderStyle-HorizontalAlign="Center"
                                                                                ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblgradepoint" runat="server" Text='<%# Eval("GDPOINT") %>' HeaderStyle-HorizontalAlign="Left" />
                                                                                </ItemTemplate>
                                                                                <FooterStyle HorizontalAlign="Center" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="LETTER GRADE" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblgrade" runat="server" Font-Bold="true" Text='<%# Eval("GRADE") %>' HeaderStyle-HorizontalAlign="Left" />
                                                                                </ItemTemplate>
                                                                                <FooterStyle HorizontalAlign="Center" />
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="PASSFAIL" HeaderText="RESULT" HeaderStyle-HorizontalAlign="Center" />
                                                                            <asp:BoundField DataField="MON&YEAR1" HeaderText="MONTH & YEAR OF PASSING" HeaderStyle-HorizontalAlign="Center" />
                                                                            <asp:BoundField DataField="course_credit" Visible="false" HeaderText=" COURSE CREDITS" HeaderStyle-HorizontalAlign="Center"
                                                                                ItemStyle-HorizontalAlign="Center" />
                                                                            <asp:TemplateField HeaderText="EARNED CREDITS" HeaderStyle-HorizontalAlign="Center"
                                                                                ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblAttendance" runat="server" Text='<%# Eval("CREDITS_EARNED") %>'
                                                                                        HeaderStyle-HorizontalAlign="Left" />
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="lblAttendancePercent" runat="server" HeaderStyle-HorizontalAlign="Left" />
                                                                                </FooterTemplate>

                                                                                <FooterStyle HorizontalAlign="Center" />

                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <EmptyDataRowStyle BackColor="LightBlue" ForeColor="Red" Font-Bold="true" />

                                                                        <EmptyDataTemplate>

                                                                            <div align="left" class="info">
                                                                                There are no records to display
                                                                            </div>
                                                                        </EmptyDataTemplate>

                                                                    </asp:GridView>
                                                                    <div style="float: right; margin-right: 53px;">
                                                                        <button class="btn_close" style="background-color: #d73925; border: none; color: #fff; border-radius: 5px; padding: 6px;">Close</button>
                                                                    </div>
                                                                </div>


                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>

                                            <EmptyDataTemplate>
                                                <div align="left" class="info">
                                                    There are no records to display
                                                </div>
                                            </EmptyDataTemplate>

                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row" id="div8" runat="server" style="display: none">
                        <div class="col-md-12 form-group">
                            <div class="box box-primary">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Cetificates Issued</h3>
                                    <div class="box-tools pull-right">
                                        <img id="img4" style="cursor: pointer;" src="../IMAGES/collapse_blue.jpg" alt=""
                                            onclick="javascript:toggleExpansion(this,'divCertificate')" />
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div id="divCertificate" style="display: none;">
                                        <asp:ListView ID="lvCertificate" runat="server">
                                            <LayoutTemplate>
                                                <div>
                                                    <h4>Certificates Issued Details</h4>
                                                    <table class="table table-hover table-bordered">
                                                        <thead>
                                                            <tr class="bg-light-blue">
                                                                <th>Certificate Name
                                                                </th>
                                                                <th>Certificate No
                                                                </th>
                                                                <th>Issued Date
                                                                </th>
                                                                <th>Issued By
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </LayoutTemplate>
                                            <EmptyDataTemplate>
                                                <div align="left" class="info">
                                                    There are no records to display
                                                </div>
                                            </EmptyDataTemplate>
                                            <ItemTemplate>
                                                <tr onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                    <td>
                                                        <%# Eval("CERTIFICATENAME") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("CERTNO") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("ISSUE_DATE", "{0:dd-MMM-yyyy}")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("ISSUEDBY") %>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <%--  THIS PANEL IS FOR SHOWING SEMESTER WISE FEE PAID--%>
                    <div class="row" id="div9" runat="server">
                        <div class="col-md-12 form-group">
                            <div class="box box-primary">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Fees Details</h3>
                                    <div class="box-tools pull-right">
                                        <%--<img id="img5" style="cursor: pointer;" src="../IMAGES/collapse_blue.jpg" alt=""
                                            onclick="javascript:toggleExpansion(this,'divFeePaid')" />--%>
                                        <a id="menu-toggle-5" style="cursor: pointer;" onclick="javascript:toggleExpansion(this,'divFeePaid')">
                                            <i class="glyphicon glyphicon-minus text-blue"></i></a>

                                    </div>
                                </div>
                                <div class="box-body">
                                    <div id="divFeePaid" style="display: block;" class="table table-responsive table-striped">
                                        <asp:ListView ID="lvFees" runat="server">
                                            <LayoutTemplate>
                                                <div>
                                                    <table class="table table-hover table-bordered table-striped">
                                                        <thead>
                                                            <tr style="background-color: aliceblue; text-align: center">
                                                                <th colspan="9" style="text-align: center">Fees Details
                                                                </th>
                                                            </tr>
                                                            <tr class="bg-light-blue">
                                                                <th>Session
                                                                </th>
                                                                <th>Semester/Year
                                                                </th>
                                                                <th>Receipt Type
                                                                </th>
                                                                <%-- <th>Rec. No
                                                                        </th>--%>
                                                                <th>Rec. Date
                                                                </th>
                                                                <th>Amount paid
                                                                </th>
                                                                <th>Remarks
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </LayoutTemplate>
                                            <EmptyDataTemplate>
                                                <div align="left" class="info">
                                                    There are no records to display
                                                </div>
                                            </EmptyDataTemplate>
                                            <ItemTemplate>
                                                <tr onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                    <td>
                                                        <%# Eval("SESSION") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("SEMESTER") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("RECIEPT")%>
                                                    </td>
                                                    <%-- <td>
                                                                <%# Eval("REC_NO") %>
                                                            </td>--%>
                                                    <td>
                                                        <%# Eval("REC_DT","{0:dd-MM-yyy}") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("TOTAL_AMT") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("REMARK") %>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <%--  THIS PANEL IS FOR SHOWING STUDENT REMARK--%>
                    <div class="row" id="div10" runat="server" style="display: none">
                        <div class="col-md-12 form-group">
                            <div class="box box-primary">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Student Remark</h3>
                                    <div class="box-tools pull-right">
                                        <img id="img7" style="cursor: pointer;" src="../IMAGES/collapse_blue.jpg" alt=""
                                            onclick="javascript:toggleExpansion(this,'divRemark')" />
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div id="divRemark" class="table table-responsive">
                                        <asp:ListView ID="lvRemark" runat="server">
                                            <LayoutTemplate>
                                                <div>
                                                    <h4>Remarks Details</h4>
                                                    <table class="table table-hover table-bordered">
                                                        <thead>
                                                            <tr class="bg-light-blue">
                                                                <th>Remarks
                                                                </th>
                                                                <th>Given By Faculty
                                                                </th>
                                                                <th>Date
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                </div>
                                            </LayoutTemplate>
                                            <EmptyDataTemplate>
                                                <div align="left" class="info">
                                                    There are no records to display
                                                </div>
                                            </EmptyDataTemplate>
                                            <ItemTemplate>
                                                <tr onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                    <td id="remak" runat="server">
                                                        <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("REMARK")%>'></asp:Label>
                                                    </td>
                                                    <td id="UANO" runat="server">
                                                        <asp:Label ID="lblUaNo" runat="server" Text='<%# Eval("UA_NO") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lbluaName" runat="server" Text='<%# Eval("UA_FULLNAME") %>'></asp:Label>
                                                    </td>
                                                    <td id="remarkDate" runat="server">
                                                        <asp:Label ID="lblRemarkDate" runat="server" Text='<%# Eval("REMARK_DATE","{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <%--  THIS PANEL IS FOR SHOWING STUDENT REFUND NOT USING IN JSS--%>
                    <div class="row" id="div11" runat="server" style="display: none">
                        <div class="col-md-12 form-group">
                            <div class="box box-primary">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Student Refund</h3>
                                    <div class="box-tools pull-right">
                                        <img id="img8" style="cursor: pointer;" src="../IMAGES/collapse_blue.jpg" alt=""
                                            onclick="javascript:toggleExpansion(this,'divRefund')" />
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div id="divRefund" style="display: none;">
                                        <asp:ListView ID="lvRefund" runat="server">
                                            <LayoutTemplate>
                                                <div>
                                                    <h4>Fee Details</h4>
                                                    <table class="table table-hover table-bordered">
                                                        <thead>
                                                            <tr class="bg-light-blue">
                                                                <th>Branch
                                                                </th>
                                                                <th>Sem.
                                                                </th>
                                                                <th>Payment Cat.
                                                                </th>
                                                                <th>Rec. Type
                                                                </th>
                                                                <th>Rec. No.
                                                                </th>
                                                                <th>Rec. Amt.
                                                                </th>
                                                                <th>Refunded Amt.
                                                                </th>
                                                                <th>Refundable Amt.
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </LayoutTemplate>
                                            <EmptyDataTemplate>
                                                <div align="left" class="info">
                                                    There are no records to display
                                                </div>
                                            </EmptyDataTemplate>
                                            <ItemTemplate>
                                                <tr onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                    <td>
                                                        <%# Eval("BRANCHSNAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("SEMESTER")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("PTYPENAME")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("RECIEPT_TITLE")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("REC_NO")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("DCR_AMT")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("REFUND_AMT")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("REFUNDABLE_AMT")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <%--  THIS PANEL IS FOR SHOWING sport details--%>
                    <div class="row" id="div12" runat="server">
                        <div class="col-md-12 form-group">
                            <div class="box box-primary">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Sports Details</h3>
                                    <div class="box-tools pull-right">
                                        <%--  <img id="img1" style="cursor: pointer;" src="../IMAGES/collapse_blue.jpg" alt=""
                                            onclick="javascript:toggleExpansion(this,'divsports')" />--%>
                                        <a id="menu-toggle-7" style="cursor: pointer;" onclick="javascript:toggleExpansion(this,'divsports')">
                                            <i class="glyphicon glyphicon-minus text-blue"></i></a>
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div id="divsports" style="display: block;" class="table table-responsive table-striped">
                                        <asp:ListView ID="lvSports" runat="server">
                                            <LayoutTemplate>
                                                <div>

                                                    <table class="table table-hover table-bordered">
                                                        <thead>
                                                            <tr class="bg-light-blue">
                                                                <th style="text-align: center">Sr No.
                                                                </th>
                                                                <th style="text-align: center">Game Name
                                                                </th>
                                                                <th style="text-align: center">Tournament Name
                                                                </th>
                                                                <th style="text-align: center">Match Date
                                                                </th>
                                                                <th style="text-align: center">Venue
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </LayoutTemplate>
                                            <EmptyDataTemplate>
                                                <div align="left" class="info">
                                                    There are no records to display
                                                </div>
                                            </EmptyDataTemplate>
                                            <ItemTemplate>
                                                <tr onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                    <td style="text-align: center">
                                                        <%#Container.DataItemIndex+1%>
                                                    </td>

                                                    <td style="text-align: center">
                                                        <%# Eval("GAME_NAME")%>
                                                    </td>


                                                    <td style="text-align: center">
                                                        <asp:Label ID="lblSPORTS_NAME" runat="server" Text='<%# Eval("SPORTS_NAME")%>'
                                                            ToolTip='<%# Eval("SPORTS_NO") %>'></asp:Label>
                                                    </td>

                                                    <td style="text-align: center">
                                                        <%# Eval("SPORTS_DATE")%>
                                                    </td>

                                                    <td style="text-align: center">
                                                        <%# Eval("SPORTS_VENUE")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <%--  THIS PANEL IS FOR SHOWING Scholarship  details--%>
                    <div class="row" id="div13" runat="server">
                        <div class="col-md-12 form-group">
                            <div class="box box-primary">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Scholarship  Details</h3>
                                    <div class="box-tools pull-right">
                                        <%--<img id="img2" style="cursor: pointer;" src="../IMAGES/collapse_blue.jpg" alt=""
                                            onclick="javascript:toggleExpansion(this,'divScholarship')" />--%>

                                        <a id="menu-toggle-10" style="cursor: pointer;" onclick="javascript:toggleExpansion(this,'divScholarship')">
                                            <i class="glyphicon glyphicon-minus text-blue"></i></a>

                                    </div>
                                </div>
                                <div class="box-body">
                                    <div id="divScholarship" style="display: block;" class="table table-responsive table-striped">
                                        <asp:ListView ID="lvScholarship" runat="server">
                                            <LayoutTemplate>
                                                <div>

                                                    <table class="table table-hover table-bordered">
                                                        <thead>
                                                            <tr class="bg-light-blue">
                                                                <th style="text-align: center">Sr No.
                                                                </th>
                                                                <th style="text-align: center">Name of the Scholarship
                                                                </th>
                                                                <th style="text-align: center">Organization Name 
                                                                </th>
                                                                <th style="text-align: center">Date of Scholarship Sanctioned 
                                                                </th>
                                                                <th style="text-align: center">Amount Sanctioned
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </LayoutTemplate>
                                            <EmptyDataTemplate>
                                                <div align="left" class="info">
                                                    There are no records to display
                                                </div>
                                            </EmptyDataTemplate>
                                            <ItemTemplate>
                                                <tr onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                    <td style="text-align: center">
                                                        <%#Container.DataItemIndex+1%>
                                                    </td>

                                                    <td style="text-align: center">
                                                        <%# Eval("SCHOLARSHIP_NAME")%>
                                                    </td>


                                                    <td style="text-align: center">
                                                        <%# Eval("ORGANIZATION_NAME")%>
                                                    </td>

                                                    <td style="text-align: center">
                                                        <%# Eval("SANCTIONED_DATE")%>
                                                    </td>

                                                    <td style="text-align: center">
                                                        <%# Eval("AMT_SANCTIONED")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <%--  THIS PANEL IS FOR SHOWING Disciplinary Action Details--%>
                    <div class="row" id="div14" runat="server">
                        <div class="col-md-12 form-group">
                            <div class="box box-primary">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Disciplinary Action Details</h3>
                                    <div class="box-tools pull-right">
                                        <%--<img id="img2" style="cursor: pointer;" src="../IMAGES/collapse_blue.jpg" alt=""
                                            onclick="javascript:toggleExpansion(this,'divScholarship')" />--%>

                                        <a id="menu-toggle-8" style="cursor: pointer;" onclick="javascript:toggleExpansion(this,'divDisciplinary')">
                                            <i class="glyphicon glyphicon-minus text-blue"></i></a>

                                    </div>
                                </div>
                                <div class="box-body">
                                    <div id="divDisciplinary" style="display: block;" class="table table-responsive table-striped">
                                        <asp:ListView ID="lvDisciplinary" runat="server">
                                            <LayoutTemplate>
                                                <div>

                                                    <table class="table table-hover table-bordered">
                                                        <thead>
                                                            <tr class="bg-light-blue">
                                                                <th style="text-align: center">Sr No.
                                                                </th>
                                                                <th style="text-align: center">Event Title
                                                                </th>
                                                                <th style="text-align: center">Event Date
                                                                </th>
                                                                <th style="text-align: center">Action Taken 
                                                                </th>
                                                                <th style="text-align: center">Event Details
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </LayoutTemplate>
                                            <EmptyDataTemplate>
                                                <div align="left" class="info">
                                                    There are no records to display
                                                </div>
                                            </EmptyDataTemplate>
                                            <ItemTemplate>
                                                <tr onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                    <td style="text-align: center">
                                                        <%#Container.DataItemIndex+1%>
                                                    </td>

                                                    <td style="text-align: center">
                                                        <%# Eval("EVENTCATENAME")%>
                                                    </td>


                                                    <td style="text-align: center">
                                                        <%# Eval("EVENTDATE")%>
                                                    </td>

                                                    <td style="text-align: center">
                                                        <%# Eval("ACTIONTAKEN")%>
                                                    </td>

                                                    <td style="text-align: center">
                                                        <%# Eval("EVENTDETAIL")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>




                    <%--  THIS PANEL IS FOR SHOWING Remark Details--%>
                    <div class="row" id="div16" runat="server">
                        <div class="col-md-12 form-group">
                            <div class="box box-primary">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Remark Details</h3>
                                    <div class="box-tools pull-right">
                                        <%--<img id="img2" style="cursor: pointer;" src="../IMAGES/collapse_blue.jpg" alt=""
                                            onclick="javascript:toggleExpansion(this,'divScholarship')" />--%>
                                        <a id="A2" style="cursor: pointer;" onclick="javascript:toggleExpansion(this,'divStudRemark')">
                                            <i class="glyphicon glyphicon-minus text-blue"></i></a>
                                    </div>
                                </div>
                                <div class="box-body">
                                    <div id="divStudRemark" style="display: block;" class="table table-responsive table-striped">
                                        <asp:ListView ID="lvStudRemark" runat="server">
                                            <LayoutTemplate>
                                                <div>
                                                    <table class="table table-hover table-bordered">
                                                        <thead>
                                                            <tr class="bg-light-blue">
                                                                <th style="text-align: center">Sr No.
                                                                </th>
                                                                <th style="text-align: center">Year
                                                                </th>
                                                                <th style="text-align: center">Remark Type
                                                                </th>
                                                                <th style="text-align: center">Remark
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </LayoutTemplate>
                                            <EmptyDataTemplate>
                                                <div align="left" class="info">
                                                    There are no records to display
                                                </div>
                                            </EmptyDataTemplate>
                                            <ItemTemplate>
                                                <tr onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                    <td style="text-align: center">
                                                        <%#Container.DataItemIndex+1%>
                                                    </td>

                                                    <td style="text-align: center">
                                                        <%# Eval("YEARNAME")%>
                                                    </td>

                                                    <td style="text-align: center">
                                                        <%# Eval("REMARK_TYPE")%>
                                                    </td>

                                                    <td style="text-align: center">
                                                        <%# Eval("REMARK")%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>




                    <!--Student Memo Download/Upload-->
                    <div class="row">
                        <div class="col-md-12">
                            <!--academic Calendar-->
                            <div class="col-md-12" id="div15" runat="server">

                                <div class="box box-primary">
                                    <div class="box-header with-border">
                                        <h3 class="box-title">Download and Upload Student Memo</h3>
                                        <div class="box-tools pull-right">
                                            <div class="box-tools pull-right">
                                                <a id="A1" style="cursor: pointer;" onclick="javascript:toggleExpansion(this,'div15')">
                                                    <i class="glyphicon glyphicon-minus text-blue"></i></a>

                                            </div>

                                        </div>
                                    </div>
                                    <!-- /.box-header -->
                                    <!-- form start -->
                                    <div class="col-md-6">
                                        <div class="box-body">
                                            <asp:ListView ID="lvStudentMemo" runat="server">
                                                <LayoutTemplate>
                                                    <table id="example2" class="table table-bordered table-hover table-fixed">
                                                        <thead>
                                                            <tr class="bg-light-blue">
                                                                <th style="text-align: center">Session
                                                                </th>
                                                                <th style="text-align: center">Semester
                                                                </th>
                                                                <th style="text-align: center">Print Memo
                                                                </th>
                                                                <th>Upload Date
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>
                                                    </table>
                                                </LayoutTemplate>
                                                <EmptyDataTemplate>
                                                    <div class="text-center">
                                                        --- Memo Not Generated ---
                                                    </div>
                                                </EmptyDataTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td style="text-align: center">
                                                            <asp:Label ID="lbReport" runat="server" Font-Bold="true" Text='<%# Eval("SESSION")%>'></asp:Label>
                                                            <asp:HiddenField ID="hdfSession" runat="server" Value='<%# Eval("SESSIONNO") %>' />
                                                            <asp:HiddenField ID="hdfDegree" runat="server" Value='<%# Eval("DEGREENO") %>' />
                                                            <asp:HiddenField ID="hdfBranch" runat="server" Value='<%# Eval("BRANCHNO") %>' />
                                                            <asp:HiddenField ID="hdfScheme" runat="server" Value='<%# Eval("SCHEMENO") %>' />
                                                            <asp:HiddenField ID="hdfIDNo" runat="server" Value='<%# Eval("IDNO") %>' />
                                                            <asp:HiddenField ID="hdfSemester" runat="server" Value='<%# Eval("SEMESTERNO") %>' />
                                                            <asp:HiddenField ID="hdfSection" runat="server" Value='<%# Eval("SECTIONNO") %>' />
                                                            <asp:HiddenField ID="hdfclgID" runat="server" Value='<%# Eval("COLLEGE_ID") %>' />
                                                        </td>
                                                        <td style="text-align: center">
                                                            <%# Eval("SEMESTER")%>
                                                        </td>
                                                        <td style="text-align: center">
                                                            <asp:LinkButton ID="lnkbtnMemo" ForeColor="Red" Font-Bold="true" runat="server" OnClick="lnkbtnMemo_Click"> <i class="fa fa-file-pdf-o" aria-hidden="true"></i></asp:LinkButton>
                                                        </td>
                                                        <td>
                                                            <%# Eval("UPLOAD_DATE")%>                                                         
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </div>
                                    </div>
                                    <div class="col-md-6" style="border-style: double; font-size: 14px; font-weight: bold; border-color: #452a2f;">

                                        <div class="form-group col-md-12">
                                            <label>
                                                <asp:Label ID="lblDocUpload" runat="server" Style="font-family: 'Times New Roman'; font-size: large;" Font-Bold="false" Text="Upload Memo:"></asp:Label>
                                                (Valid Format : PDF) 
                                            </label>
                                            <%-- <label>PDF Memo File</label>--%>
                                            <asp:FileUpload ID="fuDocUpload" Enabled="false" runat="server" onChange="LoadImage()" ToolTip="Select file to Import" />
                                        </div>

                                        <div class="form-group">
                                            <asp:LinkButton ID="btnUploadMemo" Enabled="false" runat="server" OnClientClick="return confirm ('Do you want to upload the selected memo!');" ValidationGroup="upload" OnClick="btnUploadMemo_Click" CssClass="btn btn-outline-info margin"
                                                TabIndex="4" Text="Upload Memo" ToolTip="Click to Upload"><i class="fa fa-upload"> Upload Memo</i></asp:LinkButton>
                                            <p style="font-size: 14px; font-weight: bold; color: #a61515">
                                                Note : Student or Parent must sign the document manually and then upload it.
                                            </p>
                                        </div>
                                    </div>
                                    <!-- /.box-body -->
                                </div>
                            </div>
                            <!--academic Calendar-->
                        </div>
                    </div>
                    <!--Student Memo Download/Upload-->
                </div>
            </div>
        </div>
    </div>
    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>


    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            bindDataTable();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
        });

        function bindDataTable() {
            /* To collapse and expand page sections */
            $('#menu-toggle').click(function () {
                $(this).find('i').toggleClass('glyphicon glyphicon-minus text-blue').toggleClass('glyphicon glyphicon-plus text-blue');
            });
            $('#menu-toggle-1').click(function () {
                $(this).find('i').toggleClass('glyphicon glyphicon-minus text-blue').toggleClass('glyphicon glyphicon-plus text-blue');
            });
            $('#menu-toggle-2').click(function () {
                $(this).find('i').toggleClass('glyphicon glyphicon-minus text-blue').toggleClass('glyphicon glyphicon-plus text-blue');
            });
            $('#menu-toggle-3').click(function () {
                $(this).find('i').toggleClass('glyphicon glyphicon-minus text-blue').toggleClass('glyphicon glyphicon-plus text-blue');
            });
            $('#menu-toggle-4').click(function () {
                $(this).find('i').toggleClass('glyphicon glyphicon-minus text-blue').toggleClass('glyphicon glyphicon-plus text-blue');
            });
            $('#menu-toggle-5').click(function () {
                $(this).find('i').toggleClass('glyphicon glyphicon-minus text-blue').toggleClass('glyphicon glyphicon-plus text-blue');
            });
            $('#menu-toggle-6').click(function () {
                $(this).find('i').toggleClass('glyphicon glyphicon-minus text-blue').toggleClass('glyphicon glyphicon-plus text-blue');
            });
            $('#menu-toggle-7').click(function () {
                $(this).find('i').toggleClass('glyphicon glyphicon-minus text-blue').toggleClass('glyphicon glyphicon-plus text-blue');
            });
            $('#menu-toggle-8').click(function () {
                $(this).find('i').toggleClass('glyphicon glyphicon-minus text-blue').toggleClass('glyphicon glyphicon-plus text-blue');
            });
            $('#menu-toggle-9').click(function () {
                $(this).find('i').toggleClass('glyphicon glyphicon-minus text-blue').toggleClass('glyphicon glyphicon-plus text-blue');
            });
            $('#menu-toggle-10').click(function () {
                $(this).find('i').toggleClass('glyphicon glyphicon-minus text-blue').toggleClass('glyphicon glyphicon-plus text-blue');
            });
            $('#menu-toggle-11').click(function () {
                $(this).find('i').toggleClass('glyphicon glyphicon-minus text-blue').toggleClass('glyphicon glyphicon-plus text-blue');
            });
        }

        function toggleExpansion(imageCtl, divId) {
            if (document.getElementById(divId).style.display == "block") {
                document.getElementById(divId).style.display = "none";
                // imageCtl.src = "../IMAGES/expand_blue.jpg";
            }
            else if (document.getElementById(divId).style.display == "none") {
                document.getElementById(divId).style.display = "block";
                // imageCtl.src = "../IMAGES/collapse_blue.jpg";
            }
        }

        function toggleExpansion1(imageCtl, divId) {
            if (document.getElementById(divId).style.display == "block") {
                document.getElementById(divId).style.display = "none";
                imageCtl.src = "../IMAGES/expand_blue.jpg";
            }
            else if (document.getElementById(divId).style.display == "none") {
                document.getElementById(divId).style.display = "block";
                imageCtl.src = "../IMAGES/collapse_blue.jpg";
            }
        }

    </script>

    <script language="javascript" type="text/javascript">
        function divexpandcollapse(divname) {
            var div = document.getElementById(divname);
            var img = document.getElementById('img' + divname);
            if (div.style.display == "none") {
                div.style.display = "inline";
                img.src = "../IMAGES/minus.png";
            }
            else {
                div.style.display = "none";
                img.src = "../IMAGES/plus.gif";
            }
        }
    </script>

    <div id="divMsg" runat="server">
    </div>

</asp:Content>
