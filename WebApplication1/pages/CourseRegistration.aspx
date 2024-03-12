<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="CourseRegistration.aspx.cs" Inherits="ACADEMIC_CourseRegistration"
    MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%-- <script src="../Content/jquery.js" type="text/javascript"></script>
    <script src="../Content/jquery.dataTables.js" language="javascript" type="text/javascript"></script>--%>

    <%--<script type="text/javascript" charset="utf-8">
        $(document).ready(function () {
            $(".display").dataTable({
                "bJQueryUI": true,
                "sPaginationType": "full_numbers"
            });

        });
    </script>--%>

    <asp:HiddenField ID="HFElectiveCount1" runat="server" Value="0" />
    <asp:HiddenField ID="HFElectiveCount2" runat="server" Value="0" />
    <asp:HiddenField ID="HFElectiveCount3" runat="server" Value="0" />
    <asp:HiddenField ID="HFElectiveCount4" runat="server" Value="0" />

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updCollege"
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

    <asp:UpdatePanel ID="updCollege" runat="server">
        <ContentTemplate>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <%--<h3 class="box-title">Course Registration(Non CBCS) </h3>--%>
                    <h3 class="box-title">
                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                </div>

                <div class="box-body">
                    <div class="col-12">
                        <div class="col-12">
                            <marquee id="mrqSession" runat="server" style="color: Red; font-size: medium; font-weight: bold"
                                behavior="alternate" scrollamount="21" scrolldelay="500"></marquee>
                        </div>
                        <div id="divNote" runat="server" visible="true">
                            <div class="exam-note">
                                <h5 class="heading">Note (Steps To Follow For Modules Registration.)</h5>
                                <p><span>1.</span> Click on <b>Proceed to Modules Registration</b> Button.</p>
                                <p><span>2.</span> A Module list of current semester Modules will be shown. Compulsory Subjects are already checked , you can select required Elective Modules.</p>
                                <p><span>3.</span> After selection of the Modules from the list, click the <b>Submit</b> button.</p>
                                <p><span>4.</span> Modules registration Receipt will be generate after Submission of Module.</p>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnProceed" runat="server" Text="Proceed to Modules Registration" OnClick="btnProceed_Click" CssClass="btn btn-outline-info" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-12">
                        <div id="divOptions" runat="server" visible="false">
                            <div class="form-group col-lg-6 col-md-12 col-12">
                                <div class="label-dynamic">
                                    <sup>* </sup>
                                    <label>Options</label>
                                </div>
                                <asp:RadioButtonList ID="rblOptions" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                                    OnSelectedIndexChanged="rblOptions_SelectedIndexChanged" Font-Bold="true">
                                    <asp:ListItem Value="M" Selected="True" Text="">All Students&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                    <asp:ListItem Value="S" Text="">Single Student</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                    </div>

                    <div id="divCourses" runat="server" visible="false">
                        <div class="col-12" id="tblSession" runat="server" visible="false">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <label>Session Name</label>
                                    </div>
                                    <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="True" CssClass="form-control">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <label>Registration No.</label>
                                    </div>
                                    <asp:TextBox ID="txtRollNo" runat="server" CssClass="form-control" MaxLength="15" />
                                    <asp:RequiredFieldValidator ID="rfvRollNo" ControlToValidate="txtRollNo" runat="server"
                                        Display="None" ErrorMessage="Please enter Student Reg No." ValidationGroup="Show" />
                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:LinkButton ID="btnShow" runat="server" OnClick="btnShow_Click" TabIndex="10"
                                        Text="Show" ValidationGroup="showstud" CssClass="btn btn-outline-info"><i class=" fa fa-eye"></i> Show</asp:LinkButton>

                                    <asp:Button ID="btnCancel" runat="server" Text="Clear" Font-Bold="true" ValidationGroup="Show"
                                        CssClass="btn btn-outline-danger" OnClick="btnCancel_Click" />
                                    <asp:ValidationSummary ID="valSummery2" runat="server" DisplayMode="List" ShowMessageBox="true"
                                        ShowSummary="false" ValidationGroup="Show" />
                                </div>
                            </div>
                        </div>

                        <div class="col-12" id="tblInfo" runat="server" visible="false">
                            <div class="row">
                                <div class="col-lg-6 col-md-6 col-12">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Name With Initial :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblName" runat="server" Font-Bold="True" />
                                            </a>
                                        </li>
                                        <li class="list-group-item"><b>Father Name / Mother Name:</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblFatherName" runat="server" Font-Bold="False" /></a>
                                        </li>
                                        <li class="list-group-item" style="display: none"><b>Mother Name :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblMotherName" runat="server" Font-Bold="False" /></a>
                                        </li>
                                        <li class="list-group-item"><b>Registration No. :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblEnrollNo" runat="server" Font-Bold="True"></asp:Label></a>
                                        </li>
                                        <li class="list-group-item"><b>Total Register Credits :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblbTotalCredit" runat="server" Font-Bold="True"></asp:Label></a>
                                        </li>
                                        <li class="list-group-item d-none "><b>Offered Register Credit :</b>
                                            <a class="sub-label">
                                                <asp:TextBox ID="txtAllSubjects" runat="server"
                                                    Style="text-align: center;" CssClass="form-control"></asp:TextBox>
                                            </a>
                                        </li>
                                        <li class="list-group-item d-none"><b>Total Register Credit :</b>
                                            <a class="sub-label">
                                                <asp:TextBox ID="txtRcptNo" runat="server" MaxLength="40" CssClass="form-control"></asp:TextBox></a>
                                        </li>
                                        <li class="list-group-item d-none"><b>Backlog Rec. No. :</b>
                                            <a class="sub-label">
                                                <asp:TextBox ID="txtBckLogRcptNo" runat="server" MaxLength="40" CssClass="form-control"></asp:TextBox></a>
                                        </li>
                                        <li class="list-group-item" style="display: none"><b>Backlog Rec. Date :</b>
                                            <a class="sub-label">
                                                <asp:TextBox ID="txtBckLogRcptDt" runat="server" onpaste="return false;" TabIndex="3"
                                                    ToolTip="Please Enter BackLog Receipt Date" CssClass="form-control" />
                                                <asp:Image ID="imgBckLogDate" runat="server" ImageUrl="~/images/calendar.png" ToolTip="Select Date"
                                                    AlternateText="Select Date" Style="cursor: pointer" />
                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtBckLogRcptDt" PopupButtonID="imgBckLogDate" />

                                                <ajaxToolKit:MaskedEditExtender ID="meeBckLogRcptDt" runat="server" OnInvalidCssClass="errordate"
                                                    TargetControlID="txtBckLogRcptDt" Mask="99/99/9999" MessageValidatorTip="true"
                                                    MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                                <ajaxToolKit:MaskedEditValidator ID="mevBckLogRcptDt" runat="server" ControlExtender="meeBckLogRcptDt"
                                                    ControlToValidate="txtBckLogRcptDt" EmptyValueMessage="Please Enter BackLog Receipt Date"
                                                    InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                    TooltipMessage="Please Enter BackLog Receipt Date" EmptyValueBlurredText="Empty"
                                                    InvalidValueBlurredMessage="Invalid Date" ValidationGroup="SUBMIT" SetFocusOnError="True" />
                                            </a>
                                        </li>
                                    </ul>
                                </div>

                                <div class="col-lg-6 col-md-6 col-12">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Intake / Semester :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblAdmBatch" runat="server" Font-Bold="True"></asp:Label></a>
                                        </li>
                                        <li class="list-group-item" style="display: none"><b>Current Semester :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblSemester" runat="server" Font-Bold="True"></asp:Label></a>
                                        </li>
                                        <li class="list-group-item"><b>Program :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblBranch" runat="server" Font-Bold="True"></asp:Label></a>
                                        </li>
                                        <li class="list-group-item"><b>Regulation :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblScheme" runat="server" Font-Bold="True"></asp:Label></a>
                                        </li>
                                        <li class="list-group-item"><b>Offered Register Credits :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblofferedcredit" runat="server" Font-Bold="True"></asp:Label></a>
                                        </li>
                                        <li class="list-group-item d-none"><b>Total Credits :</b>
                                            <a class="sub-label">
                                                <asp:TextBox ID="txtCredits" runat="server" Enabled="false" Text="0" CssClass="form-control"
                                                    Style="text-align: center;"></asp:TextBox><asp:HiddenField ID="hdfCredits" runat="server" Value="0" />
                                                <asp:HiddenField ID="hdfTot" runat="server" Value="0" />
                                            </a>
                                        </li>
                                        <li class="list-group-item d-none"><b>Receipt Tot. Amount :</b>
                                            <a class="sub-label">
                                                <asp:TextBox ID="txtRcptAmt" runat="server" MaxLength="9" CssClass="form-control"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                    FilterType="Numbers,Custom" ValidChars="." TargetControlID="txtRcptAmt" />
                                            </a>
                                        </li>
                                        <li class="list-group-item d-none"><b>Backlog Rec. Tot. Amt. :</b>
                                            <a class="sub-label">
                                                <asp:TextBox ID="txtBckLogRcptAmt" runat="server" MaxLength="9" CssClass="form-control"></asp:TextBox>
                                                <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                    FilterType="Numbers,Custom" ValidChars="." TargetControlID="txtBckLogRcptAmt" />
                                            </a>
                                        </li>
                                        <li class="list-group-item d-none"><b>Recript Date :</b>
                                            <a class="sub-label">
                                                <asp:TextBox ID="txtRcptDt" runat="server" onpaste="return false;" TabIndex="3" ToolTip="Please Enter Session Start Date"
                                                    CssClass="form-control" />
                                                <asp:Image ID="imgStartDate" runat="server" ImageUrl="~/images/calendar.png" ToolTip="Select Date"
                                                    AlternateText="Select Date" Style="cursor: pointer" />
                                                <ajaxToolKit:CalendarExtender ID="ceStartDate" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtRcptDt" PopupButtonID="imgStartDate" />

                                                <ajaxToolKit:MaskedEditExtender ID="meeStartDate" runat="server" OnInvalidCssClass="errordate"
                                                    TargetControlID="txtRcptDt" Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date"
                                                    DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                                <ajaxToolKit:MaskedEditValidator ID="mevStartDate" runat="server" ControlExtender="meeStartDate"
                                                    ControlToValidate="txtRcptDt" EmptyValueMessage="Please Enter Start Date" InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)"
                                                    Display="None" TooltipMessage="Please Enter Start Date" EmptyValueBlurredText="Empty"
                                                    InvalidValueBlurredMessage="Invalid Date" ValidationGroup="SUBMIT" SetFocusOnError="True" />
                                            </a>
                                        </li>
                                    </ul>
                                </div>

                            </div>
                        </div>
                        <div class="col-12 mt-3">
                            <div class="row">
                                <div class="col-lg-4 col-md-6 col-12" runat="server"  visible="false">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Details :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblselect" Text="Selected Course Fee" runat="server" Font-Bold="True"></asp:Label></a>
                                        </li>
                                    </ul>
                                </div>

                                <div class="col-lg-4 col-md-6 col-12">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Credit/Subject :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblCourseFee" Text="0.00" runat="server" Font-Bold="True"></asp:Label>
                                                <asp:HiddenField ID="hdnSelectedCourseFeeBkg" runat="server" Value="0"></asp:HiddenField>
                                            </a>
                                        </li>
                                    </ul>
                                </div>

                                <div class="col-lg-4 col-md-6 col-12" runat="server" visible="false">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Total Fee :</b>
                                            <a class="sub-label">
                                                <asp:Label ID="lblTotalFee" Text="0.00" runat="server" Font-Bold="True"></asp:Label>
                                                <asp:HiddenField ID="hdnTotalFee" runat="server" Value="0"></asp:HiddenField>
                                            </a>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>

                        <div class="col-12 btn-footer">
                            <asp:Label ID="lblmsg" runat="server" Style="color: Red; font-weight: bold" Text=""></asp:Label>
                        </div>

                        <div class="col-12" id="trFailList" runat="server">
                            <asp:ListView ID="lvCurrentSubjects" runat="server" Visible="false">
                                <LayoutTemplate>
                                    <div class="sub-heading">
                                        <h5>Core Subjects <span style="color: green; font-size: 12px;"> (Mandatory-No need to select)</span></h5>
                                    </div>
                                    <table class="table table-striped table-bordered nowrap display" id="mytable" style="width: 100%">
                                        <thead class="bg-light-blue">
                                            <%-- <table id="tblCurrentSubjects" class="table table-hover table-bordered">
                                        <thead>--%>
                                            <tr>
                                                <th>
                                                    <asp:CheckBox ID="cbHead" runat="server" ToolTip="Select/Select all" onclick="checkAllCheckbox(this)" />
                                                </th>
                                                <th>Module Code
                                                </th>
                                                <th>Module Name
                                                </th>
                                                <th>Semester
                                                </th>
                                                <th>Sub. Type
                                                </th>
                                                <th>Credits
                                                </th>
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
                                            <asp:CheckBox ID="chkAccept" runat="server" onclick="CheckSelectionCount(this)" ToolTip="Click to select this Module for registration" />
                                            <asp:HiddenField ID="hdnRegistered" runat="server" Value='<%# Eval("ACCEPTED") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("SEMESTER") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblSub_Type" runat="server" Text='<%# Eval("SUBNAME") %>' ToolTip='<%# Eval("SUBID") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>' />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </div>
                        <asp:ListView ID="lvElectiveCourse" runat="server" Visible="false" OnItemDataBound="lvElectiveCourse_ItemDataBound">
                            <LayoutTemplate>
                                <div class="sub-heading">
                                    <h5>Elective Subjects <span style="color: green; font-size: 12px;"></span>></h5>
                                </div>

                                <table class="table table-striped table-bordered nowrap display" id="tblElectiveSubjects" style="width: 100%">
                                    <thead class="bg-light-blue">
                                        <tr>
                                            <th>
                                                <%-- <asp:CheckBox ID="cbHead" runat="server" ToolTip="Select/Select all" />--%>
                                            </th>

                                            <th>Module Code
                                            </th>
                                            <th>Module Name
                                            </th>
                                            <th>Semester
                                            </th>
                                            <th>Sub. Type
                                            </th>
                                            <th>Group
                                            </th>
                                            <th>Credits
                                            </th>
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
                                        <asp:CheckBox ID="chkAccept" runat="server" ToolTip="Click to select this Module for registration" Checked='<%# Eval("REGISTERED").ToString()=="1" ? true : false %>'
                                            onclick="ElectiveSubjects(this); " />
                                        <asp:HiddenField ID="hdnRegistered" runat="server" Value='<%# Eval("ACCEPTED") %>' />
                                    </td>

                                    <td>
                                        <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("SEMESTER") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblSub_Type" runat="server" Text='<%# Eval("SUBNAME") %>' ToolTip='<%# Eval("SUBID") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblGroup" runat="server" Text='<%# Eval("ELECTGROUP") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>' />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>

                        <asp:ListView ID="lvElectiveCourse2" runat="server" Visible="false" OnItemDataBound="lvElectiveCourse2_ItemDataBound">
                            <LayoutTemplate>
                                <div class="sub-heading">
                                    <h5>Elective Subjects [Group-2] <span style="color: green; font-size: 12px;">(Optional any 1)</span>></h5>
                                </div>

                                <table class="table table-striped table-bordered nowrap display" id="tblElectiveSubjects2" style="width: 100%">
                                    <thead class="bg-light-blue">
                                        <tr>
                                            <th>
                                                <%-- <asp:CheckBox ID="cbHead" runat="server" ToolTip="Select/Select all" />--%>
                                            </th>

                                            <th>Module Code
                                            </th>
                                            <th>Module Name
                                            </th>
                                            <th>Semester
                                            </th>
                                            <th>Sub. Type
                                            </th>
                                            <th>Group
                                            </th>
                                            <th>Credits
                                            </th>
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
                                        <asp:CheckBox ID="chkAccept" runat="server" ToolTip="Click to select this subject for registration" Checked='<%# Eval("REGISTERED").ToString()=="1" ? true : false %>'
                                            onclick="ElectiveSubjects2(this); " />
                                        <asp:HiddenField ID="hdnRegistered" runat="server" Value='<%# Eval("ACCEPTED") %>' />
                                    </td>

                                    <td>
                                        <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("SEMESTER") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblSub_Type" runat="server" Text='<%# Eval("SUBNAME") %>' ToolTip='<%# Eval("SUBID") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblGroup" runat="server" Text='<%# Eval("ELECTGROUP") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>' />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>

                        <asp:ListView ID="lvElectiveCourse3" runat="server" Visible="false" OnItemDataBound="lvElectiveCourse3_ItemDataBound">
                            <LayoutTemplate>
                                <div class="sub-heading">
                                    <h5>Elective Subjects [Group-3] <span style="color: green; font-size: 12px;">(Optional any 1)</span>></h5>
                                </div>

                                <table class="table table-striped table-bordered nowrap display" id="tblElectiveSubjects3" style="width: 100%">
                                    <thead class="bg-light-blue">
                                        <tr>
                                            <th>
                                                <%-- <asp:CheckBox ID="cbHead" runat="server" ToolTip="Select/Select all" />--%>
                                            </th>

                                            <th>Module Code
                                            </th>
                                            <th>Module Name
                                            </th>
                                            <th>Semester
                                            </th>
                                            <th>Sub. Type
                                            </th>
                                            <th>Group
                                            </th>
                                            <th>Credits
                                            </th>
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
                                        <asp:CheckBox ID="chkAccept" runat="server" ToolTip="Click to select this Module for registration" Checked='<%# Eval("REGISTERED").ToString()=="1" ? true : false %>'
                                            onclick="ElectiveSubjects3(this); " />
                                        <asp:HiddenField ID="hdnRegistered" runat="server" Value='<%# Eval("ACCEPTED") %>' />
                                    </td>

                                    <td>
                                        <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("SEMESTER") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblSub_Type" runat="server" Text='<%# Eval("SUBNAME") %>' ToolTip='<%# Eval("SUBID") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblGroup" runat="server" Text='<%# Eval("ELECTGROUP") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>' />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>

                        <asp:ListView ID="lvElectiveCourse4" runat="server" Visible="false" OnItemDataBound="lvElectiveCourse4_ItemDataBound">
                            <LayoutTemplate>
                                <div class="sub-heading">
                                    <h5>Elective Subjects [Open Elective] <span style="color: green; font-size: 12px;">(Optional any 1)</span>></h5>
                                </div>

                                <table class="table table-striped table-bordered nowrap display" id="tblElectiveSubjects4" style="width: 100%">
                                    <thead class="bg-light-blue">
                                        <tr>
                                            <th>
                                                <%-- <asp:CheckBox ID="cbHead" runat="server" ToolTip="Select/Select all" />--%>
                                            </th>

                                            <th>Module Code
                                            </th>
                                            <th>Module Name
                                            </th>
                                            <th>Semester
                                            </th>
                                            <th>Sub. Type
                                            </th>
                                            <th>Group
                                            </th>
                                            <th>Credits
                                            </th>
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
                                        <asp:CheckBox ID="chkAccept" runat="server" ToolTip="Click to select this Module for registration" Checked='<%# Eval("REGISTERED").ToString()=="1" ? true : false %>'
                                            onclick="ElectiveSubjects4(this); " />
                                        <asp:HiddenField ID="hdnRegistered" runat="server" Value='<%# Eval("ACCEPTED") %>' />
                                    </td>

                                    <td>
                                        <asp:Label ID="lblCCode" runat="server" Text='<%# Eval("CCODE") %>' ToolTip='<%# Eval("COURSENO")%>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("COURSE_NAME") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblSemester" runat="server" Text='<%# Eval("SEMESTER") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblSub_Type" runat="server" Text='<%# Eval("SUBNAME") %>' ToolTip='<%# Eval("SUBID") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblGroup" runat="server" Text='<%# Eval("ELECTGROUP") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCredits" runat="server" Text='<%# Eval("CREDITS") %>' />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>

                        <div class="col-12 btn-footer">
                            <asp:Button ID="btnEditReg" runat="server" Text="Edit Registration" OnClick="btnEditReg_Click"
                                Visible="false" CssClass="btn btn-outline-info" />
                            <%--  <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSubmit_Click"
                                    Visible="false" ValidationGroup="SUBMIT" OnClientClick="return validateAssign();" />--%>

                            <asp:LinkButton ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" OnClientClick="return validateAssign();"
                                Text="Submit" Visible="false" ValidationGroup="SUBMIT" CssClass="btn btn-outline-info"><i class=" fa fa-save"></i> Submit</asp:LinkButton>

                            <asp:LinkButton ID="btnPrintRegSlip" runat="server" OnClick="btnPrintRegSlip_Click"
                                Text="Registration Slip" Enabled="false" Visible="false" ValidationGroup="submit" CssClass="btn btn-outline-primary"><i class="fa fa-file-pdf-o" aria-hidden="true"></i> Registration Slip</asp:LinkButton>

                            <%--<asp:Button ID="btnPrintRegSlip" runat="server" Text="Registration Slip" OnClick="btnPrintRegSlip_Click"
                                    Enabled="false" Visible="false" CssClass="btn btn-outline-info" />--%>
                            <asp:ValidationSummary ID="vssubmit" runat="server" DisplayMode="List" ShowMessageBox="true"
                                ShowSummary="false" ValidationGroup="SUBMIT" />
                        </div>
                    </div>
                </div>


                <asp:HiddenField ID="hdfTotNoCourses" runat="server" Value="0" />
                <asp:Panel ID="pnlDept" runat="server" Visible="False">
                    <div class="col-12">
                        <div class="row">
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label>Session</label>
                                </div>
                                <asp:DropDownList ID="ddlSessionReg" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label></label>
                                </div>
                                <asp:Button ID="btnRegister" runat="server" OnClick="btnRegister_Click" Visible="true"
                                    Enabled="false" Text="Register All Students" Font-Bold="True" CssClass="btn btn-outline-info" />
                            </div>
                            <div class="form-group col-lg-3 col-md-6 col-12">
                                <div class="label-dynamic">
                                    <label></label>
                                </div>
                                <asp:Button ID="btnPrintStudList" runat="server" Text="Print Student List" Visible="true"
                                    OnClick="btnPrintStudList_Click" CssClass="btn btn-outline-primary" />
                            </div>

                            <div class="col-md-12">
                                <asp:ListView ID="lvStudent" runat="server" OnItemDataBound="lvStudents_ItemDataBound">
                                    <LayoutTemplate>
                                        <div class="sub-heading">
                                            <h5>Student List</h5>
                                        </div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="myTable">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Edit
                                                    </th>
                                                    <th>Reg No
                                                    </th>
                                                    <th>Student Name
                                                    </th>
                                                    <th>Module Codes
                                                    </th>
                                                    <th>Status
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
                                                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("REGNO") %>'
                                                    AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click" />
                                            </td>

                                            <td>
                                                <span>
                                                    <%# Eval("REGNO") %></span>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblIDNO" runat="server" Visible="false" ToolTip='<%# Eval("IDNO") %>'></asp:Label>
                                                <span>
                                                    <%# Eval("STUDNAME") %></span>
                                            </td>
                                            <td>
                                                <span style="font-size: 9pt">
                                                    <%# Eval("CCODES") %></span>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("STATUS")%>'></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                    </div>
                </asp:Panel>

                <div id="divMsg" runat="server">
                </div>

                <div style="text-align: center" id="FreezePane" class="FreezePaneOff">
                    <div id="InnerFreezePane" class="InnerFreezePane">
                    </div>
                </div>

            </div>
        </div>
    </div>


    <style type="text/css">
        .FreezePaneOff {
            visibility: hidden;
            display: none;
            position: absolute;
            top: -100px;
            left: -100px;
            opacity: .80;
        }

        .FreezePaneOn {
            position: fixed;
            top: 0px;
            left: 0px;
            visibility: visible;
            display: block;
            background-color: black;
            width: 100%;
            height: 100%;
            z-index: 999;
            -moz-opacity: 0.7;
            opacity: .70;
            lpha(opacity=70) adding-top: 20%;
            Inne text align widt bac ground clor Whit font-s ze: bo der p padding: 9px;
            opacity: .9;
        }
    </style>

    <script type="text/javascript" language="JavaScript">

        function FreezeScreen(msg) {
            scroll(0, 0);
            var outerPane = document.getElementById('FreezePane');
            var innerPane = document.getElementById('InnerFreezePane');
            if (outerPane) outerPane.className = 'FreezePaneOn';
            if (innerPane) innerPane.innerHTML = msg;
        }

        //show confirm box on submit
        function showConfirm() {
            var validate = false;
            if (Page_ClientValidate()) {
                if (ValidatorOnSubmit()) {


                    var ret = confirm('Do you Really want to Confirm/Submit this Module for Module Registration?\nOnce Submit it cannot be modified.');
                    if (ret == true) {

                        validate = true;
                    }
                    else
                        validate = false;

                }
            }
            return validate;
        }

        //validate atleast one checkbox from elective list
        function validateAssign() {
            var txtTot = document.getElementById('<%= HFElectiveCount1.ClientID %>').value;
            var txtTot2 = document.getElementById('<%= HFElectiveCount2.ClientID %>').value;
            var txtTot3 = document.getElementById('<%= HFElectiveCount3.ClientID %>').value;
            var txtTot4 = document.getElementById('<%= HFElectiveCount4.ClientID %>').value;

            //if (txtTot == 0) {
            //    alert('Please Select atleast one Module from Group-1');
            //    return false;
            //}
            //else if (txtTot2 == 0) {
            //    alert('Please Select atleast one Module from Group-2');
            //    return false;
            //}
            //else if (txtTot3 == 0) {
            //    alert('Please Select atleast one Module from Group-3');
            //    return false;
            //}
            //else if (txtTot4 == 0) {
            //    alert('Please Select atleast one Module from Open Elective');
            //    return false;
            //}
            //else
            //    //return true;
                return showConfirm();
        }



        function ValidateFeeDetail() {
            debugger;
            var alltbl = ["tblCurrentSubjects", "tblBacklogSubjects"];
            var tbl = '';
            var list = '';
            var valid = true;

            try {
                for (i = 0; i < alltbl.length; i++) {
                    tbl = document.getElementById(alltbl[i]);
                    if (tbl != null) {
                        var dataRows = tbl.getElementsByTagName('tr');
                        if (dataRows != null) {
                            if (alltbl[i] == 'tblCurrentSubjects') {
                                list = 'lvCurrentSubjects';
                            }
                            else {
                                list = 'lvBacklogSubjects';
                            }

                            var rcptno = '';
                            var rcptamt = '';
                            var rcptdt = '';

                            for (j = 0; j < dataRows.length - 1; j++) {
                                var chkid = 'ctl00_ContentPlaceHolder1_' + list + '_ctrl' + j + '_chkAccept';
                                if (document.getElementById(chkid).checked) {
                                    if (list == 'lvCurrentSubjects') {
                                        rcptno = document.getElementById('<%= txtRcptNo.ClientID %>').value;
                                        rcptamt = document.getElementById('<%= txtRcptAmt.ClientID %>').value;
                                        rcptdt = document.getElementById('<%= txtRcptDt.ClientID %>').value;

                                        if (rcptno.trim() == '' || rcptamt.trim() == '' || rcptdt.trim() == '') {
                                            valid = false;
                                            alert('Please current semester registration fees receipt no., receipt dt., and receipt amount.');
                                            document.getElementById('<%= txtRcptNo.ClientID %>').focus();
                                            return valid;
                                        }
                                    }
                                    else {
                                        rcptno = document.getElementById('<%= txtBckLogRcptNo.ClientID %>').value;
                                        rcptamt = document.getElementById('<%= txtBckLogRcptAmt.ClientID %>').value;
                                        rcptdt = document.getElementById('<%= txtBckLogRcptDt.ClientID %>').value;

                                        if (rcptno.trim() == '' || rcptamt.trim() == '' || rcptdt.trim() == '') {
                                            valid = false;
                                            alert('Please enter backlog registration fees receipt no., receipt dt., and receipt amount.');
                                            document.getElementById('<%= txtBckLogRcptNo.ClientID %>').focus();
                                            return valid;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                return valid;
            }
            catch (e) {
                alert(e);
            }
        }
    </script>

    <script type="text/javascript" language="javascript">

        function SelectAll(headerid, headid, chk) {
            var tbl = '';
            var list = '';
            if (headid == 1) {
                tbl = document.getElementById('tblCurrentSubjects');
                list = 'lvCurrentSubjects';
            }
            else if (headid == 2) {
                tbl = document.getElementById('tblBacklogSubjects');
                list = 'lvBacklogSubjects';
            }
            else {
                tbl = document.getElementById('tblAuditSubjects');
                list = 'lvAuditSubjects';
            }

            try {
                var dataRows = tbl.getElementsByTagName('tr');
                if (dataRows != null) {
                    for (i = 0; i < dataRows.length - 1; i++) {
                        var chkid = 'ctl00_ContentPlaceHolder1_' + list + '_ctrl' + i + '_' + chk;
                        if (headerid.checked) {
                            document.getElementById(chkid).checked = true;

                        }
                        else {
                            document.getElementById(chkid).checked = false;
                        }
                        chkid = '';
                    }
                }
            }
            catch (e) {
                alert(e);
            }
        }

        //elective course 1
        function ElectiveSubjects(chk) {
            debugger;
            try {

                var checkBoxid = chk.id;

                var group = checkBoxid.substring(0, 49) + 'lblGroup';

                var checkVal = checkBoxid.substr(47, 2);
                if (checkVal >= 10) {
                    var group = checkBoxid.substring(0, 50) + 'lblGroup';
                }

                var grpval = document.getElementById(group).innerText;



                var count1 = 0;
                var count2 = 0;
                var tbl = document.getElementById('tblElectiveSubjects');
                var elements = tbl.getElementsByTagName("input");


                if (grpval == 1) {

                    for (i = 0; i < elements.length; i++) {

                        if (elements[i].type == "checkbox") {
                            if (elements[i].checked) {
                                count1++;
                            }
                        }
                    }
                }



                if (grpval == 1) {
                    document.getElementById('<%= HFElectiveCount1.ClientID %>').value = count1;
                }


                if (count1 > 1 && grpval == 1) {

                    alert('Please select any one elective Subject from Group ' + grpval + '.');
                    chk.checked = false;


                    return;
                }





            }
            catch (e) {
                alert(e);
            }
        }

        //elective course group 2
        function ElectiveSubjects2(chk) {
            debugger;
            try {


                var checkBoxid = chk.id;
                var group = checkBoxid.substring(0, 50) + 'lblGroup';

                var checkVal = checkBoxid.substr(48, 2);
                if (checkVal >= 10) {
                    var group = checkBoxid.substring(0, 51) + 'lblGroup';
                }


                var grpval = document.getElementById(group).innerText;

                var count1 = 0;
                var count2 = 0;
                var tbl = document.getElementById('tblElectiveSubjects2');
                var elements = tbl.getElementsByTagName("input");


                if (grpval == 2) {
                    for (i = 0; i < elements.length; i++) {

                        if (elements[i].type == "checkbox") {
                            if (elements[i].checked) {
                                count2++;


                            }


                        }
                    }
                }


                if (grpval == 2) {

                    document.getElementById('<%= HFElectiveCount2.ClientID %>').value = count2;

                }



                if (count2 > 1 && grpval == 2) {

                    alert('Please select any one elective Subject from Group ' + grpval + '.');
                    chk.checked = false;


                    return;

                }




            }
            catch (e) {
                alert(e);
            }
        }

        //elective course group 3
        function ElectiveSubjects3(chk) {
            debugger;
            try {


                var checkBoxid = chk.id;
                var group = checkBoxid.substring(0, 50) + 'lblGroup';

                var checkVal = checkBoxid.substr(48, 2);
                if (checkVal >= 10) {
                    var group = checkBoxid.substring(0, 51) + 'lblGroup';
                }



                var group = checkBoxid.substring(0, 50) + 'lblGroup';

                var grpval = document.getElementById(group).innerText;

                var count1 = 0;
                var count2 = 0;
                var tbl = document.getElementById('tblElectiveSubjects3');
                var elements = tbl.getElementsByTagName("input");



                if (grpval == 3) {
                    for (i = 0; i < elements.length; i++) {

                        if (elements[i].type == "checkbox") {
                            if (elements[i].checked) {
                                count2++;


                            }


                        }
                    }
                }



                if (grpval == 3) {

                    document.getElementById('<%= HFElectiveCount3.ClientID %>').value = count2;

                }

                if (count2 > 1 && grpval == 3) {

                    alert('Please select any one elective Subject from Group ' + grpval + '.');
                    chk.checked = false;


                    return;

                }




            }
            catch (e) {
                alert(e);
            }
        }

        //elective course group 4
        function ElectiveSubjects4(chk) {
            debugger;
            try {


                var checkBoxid = chk.id;
                var group = checkBoxid.substring(0, 50) + 'lblGroup';

                var checkVal = checkBoxid.substr(48, 2);
                if (checkVal >= 10) {
                    var group = checkBoxid.substring(0, 51) + 'lblGroup';
                }



                var group = checkBoxid.substring(0, 50) + 'lblGroup';

                var grpval = document.getElementById(group).innerText;

                var count1 = 0;
                var count2 = 0;
                var tbl = document.getElementById('tblElectiveSubjects4');
                var elements = tbl.getElementsByTagName("input");



                if (grpval == 7) {
                    for (i = 0; i < elements.length; i++) {

                        if (elements[i].type == "checkbox") {
                            if (elements[i].checked) {
                                count2++;


                            }


                        }
                    }
                }



                if (grpval == 7) {

                    document.getElementById('<%= HFElectiveCount4.ClientID %>').value = count2;

               }

               if (count2 > 1 && grpval == 7) {

                   alert('Please select any one elective Subject from Open Elective.');
                   chk.checked = false;


                   return;

               }




           }
           catch (e) {
               alert(e);
           }
       }


    </script>
     <script type="text/javascript" language="javascript">

         function checkAllCheckbox(headchk) {
             var frm = document.forms[0]
             for (i = 0; i < document.forms[0].elements.length; i++) {
                 var e = frm.elements[i];
                 var s = e.name.split("ctl00$ContentPlaceHolder1$lvCurrentSubjects$ctrl");
                 var b = 'ctl00$ContentPlaceHolder1$lvCurrentSubjects$ctrl';
                 var g = b + s[1];
                 if (e.name == g) {
                     if (headchk.checked == true)
                         e.checked = true;
                     else
                         e.checked = false;
                 }
             }
         }

    </script>

    <script type="text/javascript" language="javascript">
        function CheckSelectionCount(chk) {
            debugger;
            var count = -2;
            var tbl = '';
            var list = '';
            var alltbl = ["mytable"];
            var countCheck1 = 0;
            var countCheck2 = 0;
            for (i = 0; i < alltbl.length; i++) {
                tbl = document.getElementById(alltbl[i]);
                if (tbl != null) {
                    var dataRows = tbl.getElementsByTagName('tr');
                    if (dataRows != null) {
                        list = 'lvCurrentSubjects';
                        for (j = 0; j < dataRows.length - 1 ; j++) {
                            var chkid = 'ctl00_ContentPlaceHolder1_' + list + '_ctrl' + j + '_chkAccept';
                            var lblCourseGroup1 = 'ctl00_ContentPlaceHolder1_' + list + '_ctrl' + j + '_lblCourseName';
                            ValidateFeeDetail();
                        }
                       
                    }
                }
            }
        }
        function ValidateFeeDetail() {
            debugger;
            var alltbl = ["trFailList"];
            var tbl = '';
            var list = '';
            var valid = true;
            var semesterCount = 0;
            var oldSemester = '';
            var currSemester = '';
            var totCoursefee = 0.0;
            var CheckCount = 0;
            var length = alltbl.length;
            var CreditAmt = document.getElementById('ctl00_ContentPlaceHolder1_lblbTotalCredit').innerHTML.trim();
            var lblText = document.getElementById('ctl00_ContentPlaceHolder1_lblselect').innerHTML.trim();
            //alert(CreditAmt)
            var listview = document.getElementById('mytable');
            try {
                for (j = 0; j < listview.rows.length - 1 ; j++) {
                    var chkid = 'ctl00_ContentPlaceHolder1_lvCurrentSubjects_ctrl' + j + '_chkAccept';
                    if (document.getElementById(chkid).checked) {
                        var selAmt = 0;
                        if (lblText == "Rupees Per Credit") {
                            selAmt = parseFloat(document.getElementById('ctl00_ContentPlaceHolder1_lvCurrentSubjects_ctrl' + j + '_lblCredits').innerHTML.trim());
                            totCoursefee = totCoursefee + selAmt;
                            document.getElementById('ctl00_ContentPlaceHolder1_lblCourseFee').innerHTML = totCoursefee;
                            document.getElementById('ctl00_ContentPlaceHolder1_hdnSelectedCourseFeeBkg').value = totCoursefee;
                            document.getElementById('ctl00_ContentPlaceHolder1_lblTotalFee').innerHTML = totCoursefee * CreditAmt;
                            document.getElementById('ctl00_ContentPlaceHolder1_hdnTotalFee').value = totCoursefee * CreditAmt;
                        }

                        if (lblText == "Rupees Per Subject") {
                            CheckCount++;
                            totCoursefee = parseFloat(totCoursefee) + parseFloat(CreditAmt);
                            document.getElementById('ctl00_ContentPlaceHolder1_lblCourseFee').innerHTML = CheckCount;
                            document.getElementById('ctl00_ContentPlaceHolder1_hdnSelectedCourseFeeBkg').value = CheckCount;
                            document.getElementById('ctl00_ContentPlaceHolder1_lblTotalFee').innerHTML = totCoursefee;
                            document.getElementById('ctl00_ContentPlaceHolder1_hdnTotalFee').value = totCoursefee;
                        }
                        //if (lblText == "Rupees Per Module") {
                        //    CheckCount++;
                        //    totCoursefee = parseFloat(totCoursefee);
                        //    document.getElementById('ctl00_ContentPlaceHolder1_lblCourseFee').innerHTML = CheckCount;
                        //    document.getElementById('ctl00_ContentPlaceHolder1_hdnSelectedCourseFeeBkg').value = CheckCount;
                        //    document.getElementById('ctl00_ContentPlaceHolder1_lblTotalFee').innerHTML = totCoursefee;
                        //    document.getElementById('ctl00_ContentPlaceHolder1_hdnTotalFee').value = totCoursefee;
                        //}

                    }
                    
                }
                if (lblText == "Rupees Per Credit") {
                    document.getElementById('ctl00_ContentPlaceHolder1_lblCourseFee').innerHTML = totCoursefee;
                    document.getElementById('ctl00_ContentPlaceHolder1_hdnSelectedCourseFeeBkg').value = totCoursefee;
                    document.getElementById('ctl00_ContentPlaceHolder1_lblTotalFee').innerHTML = totCoursefee * CreditAmt;
                    document.getElementById('ctl00_ContentPlaceHolder1_hdnTotalFee').value = totCoursefee * CreditAmt;
                }
                if (lblText == "Rupees Per Subject") {

                    //totCoursefee = totCoursefee + CreditAmt;
                    document.getElementById('ctl00_ContentPlaceHolder1_lblCourseFee').innerHTML = CheckCount;
                    document.getElementById('ctl00_ContentPlaceHolder1_hdnSelectedCourseFeeBkg').value = CheckCount;
                    document.getElementById('ctl00_ContentPlaceHolder1_lblTotalFee').innerHTML = totCoursefee;
                    document.getElementById('ctl00_ContentPlaceHolder1_hdnTotalFee').value = totCoursefee;
                }
                //if (lblText == "Rupees Per Subject") {

                //    //totCoursefee = totCoursefee + CreditAmt;
                //    document.getElementById('ctl00_ContentPlaceHolder1_lblCourseFee').innerHTML = CheckCount;
                //    document.getElementById('ctl00_ContentPlaceHolder1_hdnSelectedCourseFeeBkg').value = CheckCount;
                //    document.getElementById('ctl00_ContentPlaceHolder1_lblTotalFee').innerHTML = totCoursefee;
                //    document.getElementById('ctl00_ContentPlaceHolder1_hdnTotalFee').value = totCoursefee;
                //}
              
            }
            catch (e) {

                alert(e);
                valid = false;
            }
            return valid;
        }
    </script>
</asp:Content>
