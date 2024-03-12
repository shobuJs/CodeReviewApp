<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="NewAdmBranchChange.aspx.cs" Inherits="ACADEMIC_NewAdmBranchChange" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">
        function RunThisAfterEachAsyncPostback() {
            Autocomplete();
        }

        function Autocomplete() {
            $(function () {
                $(".tb").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            url: "../HealthService.asmx/GetData_BranchChange",
                            data: "{'data': '" + request.term + "'}",
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            dataFilter: function (data) {; return data; },
                            success: function (data) {
                                response($.map(data.d, function (item) {
                                    return {
                                        value: item
                                    }
                                }))
                            },
                            error: function (XMLHttpRequest, textStatus, errorThrown) {
                                alert(textStatus);
                            }
                        });
                    },
                    minLength: 3
                });
            });
        }

        function cnt() {
            var a = document.getElementById("txtRemark").value;
            document.getElementById("lblCount").innerHTML = 15 - a.length;
        }

    </script>

    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>

    <style>
        .nav-tabs-custom {
            box-shadow: none !important;
        }
    </style>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                   <%-- <h3 class="box-title"><span>PERMANENT ADMISSION NO CREATION</span></h3>--%>
                     <h3 class="box-title"><asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                </div>
       
                <div class="box-body">
                    <div class="nav-tabs-custom">
                        <ul class="nav nav-tabs" role="tablist">
                            <li class="nav-item">
                                <a class="nav-link active" data-toggle="tab" href="#tab_1" tabindex="1">Program Change</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" data-toggle="tab" href="#tab_2" tabindex="2">Program Change Report</a>
                            </li>
                        </ul>

                        <div class="tab-content">
                            <div class="tab-pane active" id="tab_1">
                                <div>
                                    <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
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

                                        <div id="divCourses" runat="server">
                                            <div class="box-body">
                                                <div class="col-12">
                                                    <div class="row">
                                                        <div class="form-group col-lg-3 col-md-6 col-12" >
                                                            <div class="label-dynamic">
                                                                 
                                                                <sup>* </sup>
                                                                 <asp:Label ID="lblSearchBy" runat="server" Font-Bold="true"></asp:Label>
                                                              <%--  <label> Search By </label>--%>
                                                            </div>
                                                            <div class="radio">
                                                                <label>
                                                                    <asp:RadioButton ID="rbRegno" runat="server" GroupName="search" TabIndex="1" AutoPostBack="true" />Univ. Reg. No.
                                                                </label>
                                                                &nbsp; &nbsp;                                                                         
                                                                <label>
                                                                    <asp:RadioButton ID="rbEnrollno" runat="server" GroupName="search" TabIndex="2" AutoPostBack="true" Checked="true" />Enrollnment No
                                                                </label>
                                                            </div>
                                                        </div>
                                                        <div class="form-group col-lg-4 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <sup>* </sup>
                                                                  <asp:Label ID="lblenroll" runat="server" Font-Bold="true"></asp:Label>
                                                              <%--  <label> TAN/PAN </label>--%>
                                                            </div>
                                                            <span class="form-inline">
                                                            <asp:TextBox ID="txtStudent" runat="server" CssClass="form-control" ToolTip="Enter Enroll No" TabIndex="3"
                                                                placeholder="Please Enter Enroll No" />
                                                                
                                                                <asp:Button ID="btnShow" runat="server" Text="Search" TabIndex="4" CssClass="btn btn-outline-info m-top" OnClick="btnShow_Click" ValidationGroup="search" />
                                                                <asp:RequiredFieldValidator ID="valSearchText" runat="server" ControlToValidate="txtStudent" Display="None"
                                                                    ErrorMessage="Please Enter Enrollnment No!" SetFocusOnError="true" ValidationGroup="search" Width="10%" />
                                                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false"
                                                                    ValidationGroup="search" />
                                                            </span>
                                                        </div>

                                                        <div class="col-12">
                                                            <div class="row" id="divdata" runat="server" visible="false">

                                                                <div class="col-lg-6 col-md-6 col-12 pb-3">
                                                                    <ul class="list-group list-group-unbordered">
                                                                        <li class="list-group-item"><b>Student Name :</b>
                                                                            <a class="sub-label">
                                                                                <asp:Label ID="lblName" runat="server" Font-Bold="True" Style="color: green"></asp:Label>
                                                                                <asp:Label ID="lblAdmbatch" runat="server" Font-Bold="True" Visible="false"></asp:Label>
                                                                                <asp:Label ID="lblYear" runat="server" Font-Bold="True" Visible="false"></asp:Label>
                                                                            </a>
                                                                        </li>
                                                                        <li class="list-group-item"><b>Current Program :</b>
                                                                            <a class="sub-label"><asp:Label ID="lblBranch" runat="server" Style="color: green" Font-Bold="True"></asp:Label></a>
                                                                        </li>   
                                                                    </ul>
                                                                </div>

                                                                <div class="col-lg-6 col-md-6 col-12 pb-3">
                                                                    <ul class="list-group list-group-unbordered">
                                                                        <li class="list-group-item"><b>Univ. Reg. No. :</b>
                                                                            <a class="sub-label"><asp:Label ID="lblRegNo" runat="server" Font-Bold="True" Style="color: green"></asp:Label></a>
                                                                        </li>
                                                                        <li class="list-group-item"><b>Enrollnment No :</b>
                                                                            <a class="sub-label"><asp:Label ID="lblEnrollno" runat="server" Font-Bold="True" Style="color: green"></asp:Label></a>
                                                                        </li>        
                                                                    </ul>
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                          <asp:Label ID="lblDYDegree" runat="server" Font-Bold="true"></asp:Label>
                                                                      <%--  <label> Degree </label>--%>
                                                                    </div>
                                                                    <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                                        TabIndex="5" AutoPostBack="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlDegree"
                                                                        Display="None" ErrorMessage="Please Select Degree" InitialValue="0"
                                                                        ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                          <asp:Label ID="lblDYBranch" runat="server" Font-Bold="true"></asp:Label>
                                                                       <%-- <label> New Program </label>--%>
                                                                    </div>
                                                                    <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" placeholder="Select New Program"
                                                                        TabIndex="6" AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                                                        Display="None" ErrorMessage="Please Select Program" InitialValue="0"
                                                                        ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                                </div>

                                                                <div class="form-group col-lg-3 col-md-6 col-12" style="display: none;">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                          <asp:Label ID="lblunivreg" runat="server" Font-Bold="true"></asp:Label>
                                                                     <%--   <label> New Univ. Reg. No.</label>--%>
                                                                    </div>
                                                                    <asp:TextBox ID="txtNewRegNo" runat="server" CssClass="form-control"  placeholder="New Univ. Reg. No." TabIndex="7"></asp:TextBox>
                                                                </div>

                                                                <div class="form-group col-lg-6 col-md-6 col-12">
                                                                    <div class="label-dynamic">
                                                                        <sup>* </sup>
                                                                          <asp:Label ID="lblRemark" runat="server" Font-Bold="true"><span style="color: red"> (Max Characters: 100)</span></asp:Label>
                                                                       <%-- <label> Remark<span style="color: red"> (Max Characters: 100)</span></label>--%>
                                                                    </div>
                                                                    <asp:TextBox ID="txtRemark" runat="server" Height="70px" TextMode="MultiLine" TabIndex="7"
                                                                        CssClass="form-control" onkeypress="return this.value.length<=100">
                                                                    </asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rvfRemark" runat="server" ControlToValidate="txtRemark"
                                                                        ErrorMessage="Please Enter Remark" ValidationGroup="Submit" Display="None">
                                                                    </asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>                                          
                                                        </div>
                                                    </div>
                                                </div>
                
                                                <div class="col-12 btn-footer">
                                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-outline-info" TabIndex="8"
                                                        ValidationGroup="Submit" OnClick="btnSubmit_Click" />
                                                    <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" CssClass="btn btn-outline-danger" TabIndex="9" Text="Cancel" />
                                                    <asp:Label ID="lblMsg" runat="server" SkinID="lblmsg" Style="color: green"></asp:Label>
                                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="True"
                                                        ShowSummary="False" ValidationGroup="Show" />
                                                    <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List" ShowMessageBox="True"
                                                        ShowSummary="False" ValidationGroup="Submit" />
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                            <div class="tab-pane fade" id="tab_2">
                                <div>
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel2"
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

                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        
                                        <div class="box-body">
                                            <div class="col-12">
                                                <div class="row">

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                              <asp:Label ID="lblDYCollege" runat="server" Font-Bold="true"></asp:Label>
                                                          <%--  <label for="city">College/School Name </label>--%>
                                                        </div>
                                                        <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged"
                                                            TabIndex="3" AutoPostBack="True">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCollege"
                                                            Display="None" ErrorMessage="Please Select College/School Name" InitialValue="0"
                                                            ValidationGroup="Report"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                             <asp:Label ID="lblDYDegree1" runat="server" Font-Bold="true"></asp:Label>
                                                          <%--  <label for="city">Degree </label>--%>
                                                        </div>
                                                        <asp:DropDownList ID="ddlDegreeReport" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                            TabIndex="4" AutoPostBack="True">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                            	<asp:Label ID="lblAdmissionBatch" runat="server" Font-Bold="true"></asp:Label>
                                                           <%-- <label for="city">Admission Batch </label>--%>
                                                        </div>
                                                        <asp:DropDownList ID="ddlAdmBatch" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                            TabIndex="5" AutoPostBack="True">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                         <asp:HiddenField ID="hdnDate" runat="server" />
                                                    </div>
                                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                                        <div class="label-dynamic">
                                                           <%-- <sup>* </sup>--%>
                                                            <%--<label>Session Start End Date</label>--%>
                                                             <asp:Label ID="lblStartEndDate" runat="server" Font-Bold="true"></asp:Label>
                                                        </div>
                                                        <div id="picker" class="form-control" >
                                                            <i class="fa fa-calendar"></i>&nbsp;
                                                    <span id="date"></span>
                                                            <i class="fa fa-angle-down" aria-hidden="true" style="float: right; padding-top: 4px; font-weight: bold;"></i>
                                                        </div>
                                                    </div>
                                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display:none">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <asp:Label ID="lblFromDate" runat="server" Font-Bold="true"></asp:Label>
                                                           <%-- <label> From Date </label>--%>
                                                        </div>
                                                        <div class="input-group">
                                                            <div class="input-group-addon" id="imgStartDate" runat="server">
                                                                <i class="fa fa-calendar"></i>
                                                            </div>
                                                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" TabIndex="6" AutoPostBack="true" 
                                                                placeholder="DD/MM/YYYY"/>                                                     
                                                            <ajaxToolKit:CalendarExtender ID="ceStartDate" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txtFromDate" PopupButtonID="imgStartDate" Enabled="true"
                                                                EnableViewState="true">
                                                            </ajaxToolKit:CalendarExtender>
                                                            <ajaxToolKit:MaskedEditExtender ID="meeStartDate" runat="server" TargetControlID="txtFromDate"
                                                                Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                                MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True" />
                                                            <ajaxToolKit:MaskedEditValidator ID="mevStartDate" runat="server" EmptyValueMessage="Please Enter Start Date"
                                                                ControlExtender="meeStartDate" ControlToValidate="txtFromDate" IsValidEmpty="true"
                                                                InvalidValueMessage="Start Date is Invalid!" Display="None" TooltipMessage="Input a Date"
                                                                ErrorMessage="Please Enter Start Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                                SetFocusOnError="true" />
                                                        </div>
                                                    </div>

                                                    <div class="form-group col-lg-3 col-md-6 col-12" style="display:none">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                             <asp:Label ID="lblToDate" runat="server" Font-Bold="true"></asp:Label>
                                                    <%--        <label> To Date </label>--%>
                                                        </div>
                                                        <div class="input-group">
                                                            <div class="input-group-addon" id="imgToDate" runat="server">
                                                                <i class="fa fa-calendar"></i>
                                                            </div>
                                                            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" TabIndex="7" AutoPostBack="true" 
                                                                placeholder="DD/MM/YYYY"/>                                                     
                                                            <ajaxToolKit:CalendarExtender ID="ceToDate" runat="server" Format="dd/MM/yyyy"
                                                                TargetControlID="txtToDate" PopupButtonID="imgToDate" Enabled="true"
                                                                EnableViewState="true">
                                                            </ajaxToolKit:CalendarExtender>
                                                            <ajaxToolKit:MaskedEditExtender ID="meeToDate" runat="server" TargetControlID="txtToDate"
                                                                Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                                MaskType="Date" AcceptAMPM="True" ErrorTooltipEnabled="True" />
                                                            <ajaxToolKit:MaskedEditValidator ID="mevToDate" runat="server" EmptyValueMessage="Please Enter Start Date"
                                                                ControlExtender="meeStartDate" ControlToValidate="txtToDate" IsValidEmpty="true"
                                                                InvalidValueMessage="Start Date is Invalid!" Display="None" TooltipMessage="Input a Date"
                                                                ErrorMessage="Please Enter Start Date" EmptyValueBlurredText="*" InvalidValueBlurredMessage="*"
                                                                SetFocusOnError="true" />
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>

                                            <div class="col-12 btn-footer">
                                                <asp:Button ID="btnReport" runat="server" Text="Branch Change Details" OnClick="btnReport_Click" ValidationGroup="Report" TabIndex="8" CssClass="btn btn-outline-info" />
                                                <asp:Button ID="btnCancelReport" runat="server" Text="Clear" OnClick="btnCancelReport_Click" CssClass="btn btn-outline-danger" TabIndex="9" />
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True"
                                                ShowSummary="False" ValidationGroup="Report" />
                                            </div>
                                        </div>
                                       
                                        <div id="div8" runat="Server"></div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="divMsg" runat="server" />
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#picker').daterangepicker({
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
            $('#date').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
            document.getElementById('<%=hdnDate.ClientID%>').value = (start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'));
        });

            $('#date').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
            document.getElementById('<%=hdnDate.ClientID%>').value = (moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(document).ready(function () {
                $('#picker').daterangepicker({
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
                $('#date').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
                document.getElementById('<%=hdnDate.ClientID%>').value = (start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
            });

                $('#date').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
                document.getElementById('<%=hdnDate.ClientID%>').value = (moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
            });
        });

    </script>


    <script>


        function Setdate(date) {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $(document).ready(function () {
                    debugger;
                    var startDate = moment(date.split('-')[0], "MMM DD, YYYY");
                    var endtDate = moment(date.split('-')[1], "MMM DD, YYYY");
                    //$('#date').html(date);
                    $('#date').html(startDate.format("MMM DD, YYYY") + ' - ' + endtDate.format("MMM DD, YYYY"));
                    document.getElementById('<%=hdnDate.ClientID%>').value = date;
                    //$('#picker').daterangepicker({ startDate: startDate, endDate: endtDate});
                    $('#picker').daterangepicker({
                        startDate: startDate.format("MM/DD/YYYY"),
                        endDate: endtDate.format("MM/DD/YYYY"),
                        ranges: {
                        },
                    },
            function (start, end) {
                debugger
                $('#date').html(start.format('MMM DD, YYYY') + ' - ' + end.format('MMM DD, YYYY'))
                document.getElementById('<%=hdnDate.ClientID%>').value = (start.format('MMM DD, YYYY') + ' - ' + end.format('MMM DD, YYYY'))
            });

                });
            });
};

    </script>

</asp:Content>

