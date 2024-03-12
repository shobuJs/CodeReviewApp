<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="StudentDetails.aspx.cs" Inherits="ACADEMIC_StudentDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updStudent"
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
    <asp:UpdatePanel ID="updStudent" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <%--      <h3 class="box-title"><strong>DOCUMENT MAPPING</strong> </h3>--%>
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>
                        <div class="col-12">
                            <div class="row">
                                <div class="form-group col-lg-12 col-md-12 col-12">
                                    <asp:RadioButtonList ID="rdDataOption" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdDataOption_SelectedIndexChanged">
                                        <asp:ListItem Text="&nbsp;Admission &nbsp;&nbsp;" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="&nbsp;Enrollment" Value="2"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:HiddenField ID="hdnDate" runat="server" />
                                </div>
                            </div>
                        </div>
                        <div class="col-12" id="divAdmission" runat="server" visible="false">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup> </sup>
                                        <asp:Label ID="lblDYAdmbatch" runat="server" Font-Bold="true"></asp:Label>
                                    </div>
                                    <asp:DropDownList ID="ddlAdmissionIntake" runat="server" AppendDataBoundItems="true" TabIndex="1" CssClass="form-control" data-select2-enable="true">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                        ControlToValidate="ddlAdmissionIntake" Display="None"
                                        ErrorMessage="Please Select Intake" InitialValue="0" SetFocusOnError="true"
                                        ValidationGroup="admShow"></asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup> </sup>
                                        <asp:Label ID="lblDYPrgmtype" runat="server" Font-Bold="true"></asp:Label>
                                    </div>
                                    <asp:DropDownList ID="ddlAdmissionStudyLevel" runat="server" AppendDataBoundItems="true" TabIndex="2" CssClass="form-control" data-select2-enable="true">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                        ControlToValidate="ddlAdmissionStudyLevel" Display="None"
                                        ErrorMessage="Please Select Study Level" InitialValue="0" SetFocusOnError="true"
                                        ValidationGroup="admShow"></asp:RequiredFieldValidator>--%>
                                </div>
                                 <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <asp:Label ID="lblApproveDate" runat="server" Font-Bold="true"></asp:Label>
                                    </div>
                                   <div class="form-control picker" >
                                            <i class="fa fa-calendar"></i>&nbsp;
                                    <span class="date"></span>
                                            <i class="fa fa-angle-down" aria-hidden="true" style="float: right; padding-top: 4px; font-weight: bold;"></i>
                                        </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <asp:Label ID="Label4" runat="server" Font-Bold="true" Text="Option"></asp:Label>
                                    </div>
                                    <asp:RadioButtonList ID="rdAdmissionOption" runat="server" TabIndex="3" RepeatDirection="Horizontal">
                                        <asp:ListItem Text="&nbsp;Customer Master &nbsp;&nbsp;" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="&nbsp;Cash Receipt Journal" Value="4"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                        ControlToValidate="rdAdmissionOption" Display="None"
                                        ErrorMessage="Please Select Option" SetFocusOnError="true"
                                        ValidationGroup="admShow"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnAdmissionShow" runat="Server" Text="Excel Report" ValidationGroup="admShow" OnClick="btnAdmissionShow_Click"
                                        CssClass="btn btn-outline-info" />

                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="true"
                                        ShowSummary="false" ValidationGroup="admShow" />
                                </div>
                            </div>
                        </div>
                        <div class="col-12" id="divEnrollment" runat="server" visible="false">
                            <div class="row">
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup> </sup>
                                        <asp:Label ID="lblAdmissionBatch" runat="server" Font-Bold="true"></asp:Label>
                                    </div>
                                    <asp:DropDownList ID="ddlIntake" runat="server" AppendDataBoundItems="true" TabIndex="1" CssClass="form-control" data-select2-enable="true">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                   <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                        ControlToValidate="ddlIntake" Display="None"
                                        ErrorMessage="Please Select Intake" InitialValue="0" SetFocusOnError="true"
                                        ValidationGroup="Show"></asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup> </sup>
                                        <asp:Label ID="lblDyAdmissionType" runat="server" Font-Bold="true"></asp:Label>
                                    </div>
                                    <asp:DropDownList ID="ddlStudyLevel" runat="server" AppendDataBoundItems="true" TabIndex="2" CssClass="form-control" data-select2-enable="true">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                   <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                        ControlToValidate="ddlStudyLevel" Display="None"
                                        ErrorMessage="Please Select Study Level" InitialValue="0" SetFocusOnError="true"
                                        ValidationGroup="Show"></asp:RequiredFieldValidator>--%>
                                </div>
                               <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <asp:Label ID="lblApproveDateEnroolment" runat="server" Font-Bold="true"></asp:Label>
                                    </div>
                                   <div class="form-control picker" >
                                            <i class="fa fa-calendar"></i>&nbsp;
                                    <span class="date"></span>
                                            <i class="fa fa-angle-down" aria-hidden="true" style="float: right; padding-top: 4px; font-weight: bold;"></i>
                                        </div>
                                </div>
                                <div class="form-group col-lg-3 col-md-6 col-12">
                                    <div class="label-dynamic">
                                        <sup>* </sup>
                                        <asp:Label ID="Label1" runat="server" Font-Bold="true" Text="Option"></asp:Label>
                                    </div>
                                    <asp:RadioButtonList ID="rdOption" runat="server" TabIndex="3" RepeatDirection="Horizontal">
                                        <asp:ListItem Text="&nbsp;Customer Master &nbsp;&nbsp;" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="&nbsp;Cash Receipt Journal" Value="2"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                        ControlToValidate="rdOption" Display="None"
                                        ErrorMessage="Please Select Option" SetFocusOnError="true"
                                        ValidationGroup="Show"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btnShow" runat="Server" Text="Excel Report" ValidationGroup="Show" OnClick="btnShow_Click"
                                        CssClass="btn btn-outline-info" />

                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="true"
                                        ShowSummary="false" ValidationGroup="Show" />
                                </div>
                            </div>
                        </div>
                        <%--<div class="row">
                            <div class="col-12">
                                <asp:Panel ID="pnStudentDetails" runat="server">
                                    <asp:ListView ID="lvStudentDetails" runat="server">
                                        <LayoutTemplate>
                                            <div>
                                                <div class="sub-heading">
                                                    <h5>Student Details</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%;">
                                                    <thead class="bg-light-blue">
                                                        <tr id="trRow">
                                                            <th>SrNo</th>
                                                            <th>Application No.</th>
                                                            <th>Short Name
                                                            </th>
                                                            <th>Full Name
                                                            </th>
                                                            <th>Permanent Address
                                                            </th>
                                                            <th>NIC No
                                                            </th>
                                                            <th>Gender
                                                            </th>
                                                            <th>Phone No</th>
                                                            <th>Mobile No</th>
                                                            <th>Office E-Mail</th>
                                                            <th>Faculty</th>
                                                            <th>Specialization</th>
                                                            <th>University</th>
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
                                                    <%# Container.DataItemIndex + 1 %>
                                                </td>
                                                <td><%# Eval("USERNAME") %>
                                                </td>
                                                <td><%# Eval("NAME_INITIAL") %>
                                                </td>
                                                <td><%# Eval("FULLNAME") %>
                                                </td>
                                                <td><%# Eval("PADDRESS") %>
                                                </td>
                                                <td><%# Eval("NIC") %>
                                                </td>
                                                <td><%# Eval("GENDER") %>
                                                </td>
                                                <td><%# Eval("HOME_MOBILENO") %>
                                                </td>
                                                <td><%# Eval("MOBILENO") %>
                                                </td>
                                                <td><%# Eval("COLLEGE_EMAIL") %>
                                                </td>
                                                <td><%# Eval("COLLEGE_NAME") %>
                                                </td>
                                                <td><%# Eval("LONGNAME") %>
                                                </td>
                                                <td><%# Eval("AFFILIATED_SHORTNAME") %>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>

                                </asp:Panel>
                            </div>
                        </div>--%>
                    </div>
                </div>
            </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnShow" />
            <asp:PostBackTrigger ControlID="btnAdmissionShow" /> 
        </Triggers>
    </asp:UpdatePanel>
     <script type="text/javascript">
         $(document).ready(function () {
             $('.picker').daterangepicker({
                 startDate: moment().subtract(00, 'days'),
                 endDate: moment(),
                 locale: {
                     format: 'DD MMM, YYYY'
                 },
                 //also comment "range" in daterangepicker.js('<div class="ranges"></div>' +)
                 ranges: {
                     //                    'Today': [moment(), moment()],
                     //                    'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                     //                    'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                     //                    'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                     //                    'This Month': [moment().startOf('month'), moment().endOf('month')],
                     //                    'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')] 
                 },
                 //<!-- ========= Disable dates after today ========== -->
                 //maxDate: new Date(),
                 //<!-- ========= Disable dates after today END ========== -->
             },
         function (start, end) {
             $('.date').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
             document.getElementById('<%=hdnDate.ClientID%>').value = (start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'));
        });

            $('.date').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
            document.getElementById('<%=hdnDate.ClientID%>').value = (moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(document).ready(function () {
                $('.picker').daterangepicker({
                    startDate: moment().subtract(00, 'days'),
                    endDate: moment(),
                    locale: {
                        format: 'DD MMM, YYYY'
                    },
                    //also comment "range" in daterangepicker.js('<div class="ranges"></div>' +)
                    ranges: {
                        //                    'Today': [moment(), moment()],
                        //                    'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                        //                    'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                        //                    'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                        //                    'This Month': [moment().startOf('month'), moment().endOf('month')],
                        //                    'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')] 
                    },
                    //<!-- ========= Disable dates after today ========== -->
                    //maxDate: new Date(),
                    //<!-- ========= Disable dates after today END ========== -->
                },
            function (start, end) {
                debugger
                $('.date').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
                document.getElementById('<%=hdnDate.ClientID%>').value = (start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
            });

                $('.date').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
                document.getElementById('<%=hdnDate.ClientID%>').value = (moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
            });
        });

    </script>
</asp:Content>

