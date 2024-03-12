<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="LeadAllotment.aspx.cs" Inherits="ACADEMIC_LeadAllotment" ClientIDMode="AutoID" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<script runat="server">

</script>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="https://bankofceylon.gateway.mastercard.com/checkout/version/61/checkout.js" data-error="errorCallback" data-cancel="cancelCallback"></script>
    <script type="text/javascript">
        function errorCallback(error) {
            console.log(JSON.stringify(error));
        }
        function cancelCallback() {
            //console.log('Payment cancelled');
        }
        cancelCallback = "http://admissiontest.sliit.lk/OnlineResponse.aspx"
        //function completeCallback(resultIndicator, sessionVersion) {
        //    //handle payment completion
        //    completeCallback = "http://localhost:55158/PresentationLayer/OnlineResponse.aspx"
        //}
        //completeCallback = "http://localhost:55158/PresentationLayer/OnlineResponse.aspx"
        Checkout.configure({
            session: {
                id: '<%= Session["PaymentSessionnewerp"] %>'
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
    <link href="<%=Page.ResolveClientUrl("~/plugins/Sematic/CSS/myfilterOpt.css") %>" rel="stylesheet" />
    <link href="<%=Page.ResolveClientUrl("~/plugins/Sematic/CSS/semantic.min.css") %>" rel="stylesheet" />


    <link href='<%=Page.ResolveUrl("~/plugins/newbootstrap/css/lead.css") %>' rel="stylesheet" />

    <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />

    <script src="https://cdnjs.cloudflare.com/ajax/libs/FileSaver.js/2014-11-29/FileSaver.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.12.13/xlsx.full.min.js"></script>
    <script src="<%=Page.ResolveClientUrl("~/plugins/Sematic/JS/semantic.min.js")%>"></script>
    <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>

    <style>
        .sea-rch i {
            color: #5b5b5b;
            cursor: pointer;
        }

            .sea-rch i:hover {
                color: red;
            }

        #ctl00_ContentPlaceHolder1_lvOnlineAdmissionDetails_DataPager1 a:first-child,
        #ctl00_ContentPlaceHolder1_lvOnlineAdmissionDetails_DataPager1 a:last-child {
            padding: 5px 10px;
            border-radius: 0%;
            background: white;
            margin: 0 0px;
            box-shadow: none;
        }

        #ctl00_ContentPlaceHolder1_lvOnlineAdmissionDetails_DataPager1 a {
            padding: 5px 10px;
            border-radius: 50%;
            background: white;
            margin: 0 0px;
            box-shadow: 0 1px 3px rgb(0 0 0 / 12%), 0 1px 3px rgb(0 0 0 / 24%);
        }

        #ctl00_ContentPlaceHolder1_lvOnlineAdmissionDetails_DataPager1 span {
            padding: 5px 10px;
            border-radius: 50%;
            background: #4183c4;
            color: #fff;
            margin: 0 0px;
            box-shadow: 0 1px 3px rgb(0 0 0 / 12%), 0 1px 3px rgb(0 0 0 / 24%);
        }
    </style>

    <%--===== Data Table Script added by gaurav =====--%>


    <div class="row">
        <div class="col-md-12 col-sm-12 col-12">
            <div class="box box-primary">
                <div class="nav-tabs-custom mt-2 col-12 mb-4" id="myTabContent">
                    <ul class="nav nav-tabs dynamic-nav-tabs" role="tablist">
                        <li class="nav-item active" id="divlkAnnouncement" runat="server">
                            <asp:LinkButton ID="lkAnnouncement" runat="server" OnClick="lkAnnouncement_Click" CssClass="nav-link" TabIndex="1">Lead Allotment</asp:LinkButton></li>
                        <li class="nav-item" id="divlkReports" runat="server">
                            <asp:LinkButton ID="lkReports" runat="server" OnClick="lkReports_Click" CssClass="nav-link" TabIndex="2">Reports</asp:LinkButton></li>
                        <li class="nav-item" id="divlkQuery" runat="server">
                            <asp:LinkButton ID="lkQuery" runat="server" OnClick="lkQuery_Click" CssClass="nav-link" TabIndex="3">Query</asp:LinkButton></li>
                        <li class="nav-item" id="divlkEnquiry" runat="server" visible="false">
                            <asp:LinkButton ID="lkenquiry" runat="server" OnClick="lkenquiry_Click" CssClass="nav-link" TabIndex="4">Enquiry</asp:LinkButton></li>
                        <li class="nav-item" id="DivlkRemark" runat="server">
                            <asp:LinkButton ID="lnkRemark" runat="server" CssClass="nav-link" TabIndex="5" OnClick="lnkRemark_Click">Remark</asp:LinkButton></li>
                    </ul>
                    <div class="tab-content">

                        <div class="tab-pane fade show active" id="divAnnounce" role="tabpanel" runat="server" aria-labelledby="ALCourses-tab">
                            <div>
                                <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
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
                                    <div class="pageright-wrapper chiller-theme d-none">

                                        <div id="sidebar" class="right-wrapper">
                                            <a id="showright-sidebar" class="sidebtn" style="cursor: pointer">
                                                <asp:Image ID="ImLogo" runat="server" ImageUrl="../IMAGES/right-arrow.png" class="btnsidebar" />
                                            </a>
                                            <div class="right-content" style="background-color: #fff;">
                                                <div class="right-brand">
                                                    <div class="filter-heading">
                                                        <a href="#">FILTERS</a>
                                                        <i class="fa fa-search filter-toggle filter-icon" id="filter-toggle"></i>

                                                        <div class="filter-text input-group  mt-3">
                                                            <div class="input-group-prepend input-filter">
                                                                <span class="input-group-text"><i class="fa fa-search"></i></span>
                                                                <input type="text" placeholder="Search fields" class="form-control filter-FC">
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="sidebar-header scrollbar-right">
                                                    <div class="form-group">
                                                        <label>Select Topics</label>
                                                        <%--<select name="skills" id="slMainLeadLabel" multiple="" class="label ui selection fluid dropdown" runat="server" onchange="CmbChange();" OnSelectedIndexChanged="slMainLeadLabel_SelectedIndexChanged" AutoPostBack="true">
                                                        <option value="">All</option>
                                                        <option value="1">Lead Allotment</option>
                                                        <option value="2">Status</option>
                                 
                                                    </select>--%>   <%--class="label ui selection fluid dropdown"--%>
                                                        <asp:ListBox ID="ddlMainLeadLabel" runat="server" AppendDataBoundItems="true" class="label ui selection fluid dropdown" OnSelectedIndexChanged="ddlMainLeadLabel_SelectedIndexChanged" AutoPostBack="true"
                                                            SelectionMode="multiple">
                                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            <asp:ListItem Value="1">Lead Allotment</asp:ListItem>
                                                            <asp:ListItem Value="2">Status</asp:ListItem>
                                                        </asp:ListBox>
                                                    </div>

                                                    <div class="form-group">
                                                        <label>Select Topics</label>
                                                        <%-- <select name="skills" id="slSecondHead" multiple="" class="label ui selection fluid dropdown" runat="server">
                                                        --%>
                                                        <%-- <option value="">All</option>
                                                    </select>--%>

                                                        <asp:ListBox ID="lstbSecondHead" runat="server" AppendDataBoundItems="true" class="form-control" OnSelectedIndexChanged="lstbSecondHead_SelectedIndexChanged" AutoPostBack="true"
                                                            data-select2-enable="true">
                                                            <%--<asp:ListItem>All</asp:ListItem>--%>
                                                        </asp:ListBox>
                                                    </div>
                                                    <div class="form-group" id="dvThirdListbox" runat="server">
                                                        <label>Select Topics</label>
                                                        <asp:ListBox ID="lstbThirdHead" runat="server" AppendDataBoundItems="true" class="form-control" data-select2-enable="true"
                                                            SelectionMode="single">
                                                            <%--<asp:ListItem>All</asp:ListItem>--%>
                                                        </asp:ListBox>
                                                    </div>

                                                    <div id="divdate" runat="server" visible="false">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <label>
                                                                <asp:Label ID="lblSessionStartEndDate" runat="server" Font-Bold="true"></asp:Label></label>
                                                        </div>
                                                        <asp:TextBox ID="txtstartdate" runat="server" TabIndex="4" CssClass="datePicker PickerDate form-control" />
                                                        <asp:RequiredFieldValidator ID="valstartdate" runat="server" ControlToValidate="txtstartdate"
                                                            Display="None" ErrorMessage="Please Enter Date." SetFocusOnError="true" ValidationGroup="Report" />
                                                    </div>

                                                    <div class="sidebar-footer">
                                                        <%-- <button  class="btn btn-info">Apply filter</button>&nbsp;&nbsp;
                                                    <button  class="btn btn-outline-danger">Clear filter</button>--%>
                                                        <asp:Button ID="btnApplyFilter" runat="Server" Text="Apply Filter" CssClass="btn btn-outline-info" OnClick="btnApplyFilter_Click" />&nbsp;&nbsp;
                                                    <asp:Button ID="btnClearFilter" runat="server" Text="Clear Filter" CssClass="btn btn-outline-danger" OnClick="btnClearFilter_Click" />
                                                    </div>
                                                </div>

                                            </div>
                                            <!-- sidebar-content  -->
                                        </div>
                                    </div>


                                    <div class="row">
                                        <div class="col-md-12 col-sm-12 col-12">

                                            <div class="sub-heading mt-3">
                                                <h5>
                                                    <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h5>
                                            </div>

                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-4">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <asp:Label ID="lblDYSession" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlAdmbatch" runat="server" CssClass="form-control" data-select2-enable="true"
                                                        AppendDataBoundItems="True"
                                                        ToolTip="Please Select Intake" TabIndex="1">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="ddlAdmbatch" InitialValue="0"
                                                        Display="None" ErrorMessage="Please Select Intake" SetFocusOnError="true" ValidationGroup="btnShowFilter" />
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-4">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <asp:Label ID="lblDYPrgmtype" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlugpg" runat="server" CssClass="form-control" data-select2-enable="true"
                                                        AppendDataBoundItems="True"
                                                        ToolTip="Please Select Study Level" TabIndex="2">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ControlToValidate="ddlugpg" InitialValue="0"
                                                        Display="None" ErrorMessage="Please Select Study Level" SetFocusOnError="true" ValidationGroup="btnShowFilter" />
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-4">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <asp:Label ID="Label18" runat="server" Font-Bold="true" Text="Progress Level"></asp:Label>


                                                    </div>
                                                    <asp:DropDownList ID="ddlProgressLavel" runat="server" CssClass="form-control" data-select2-enable="true"
                                                        AppendDataBoundItems="True"
                                                        ToolTip="Please Select Progress Level" TabIndex="3">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                        <asp:ListItem Value="1">0%</asp:ListItem>
                                                        <asp:ListItem Value="2">25%</asp:ListItem>
                                                        <asp:ListItem Value="3">50%</asp:ListItem>
                                                        <asp:ListItem Value="4">80%</asp:ListItem>
                                                        <asp:ListItem Value="5">100%</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator63" runat="server" ControlToValidate="ddlProgressLavel" InitialValue="0"
                                                        Display="None" ErrorMessage="Please Select Progress Level" SetFocusOnError="true" ValidationGroup="btnShowFilter" />--%>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-4">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <asp:Label ID="Label19" runat="server" Font-Bold="true" Text="Discipline"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlDiscipline" runat="server" CssClass="form-control" data-select2-enable="true"
                                                        AppendDataBoundItems="True"
                                                        ToolTip="Please Select Progress Level" TabIndex="4">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator63" runat="server" ControlToValidate="ddlProgressLavel" InitialValue="0"
                                                        Display="None" ErrorMessage="Please Select Progress Level" SetFocusOnError="true" ValidationGroup="btnShowFilter" />--%>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-4">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <asp:Label ID="Label20" runat="server" Font-Bold="true" Text="Campus"></asp:Label>

                                                    </div>
                                                    <asp:DropDownList ID="ddlCampus" runat="server" CssClass="form-control" data-select2-enable="true"
                                                        AppendDataBoundItems="True"
                                                        ToolTip="Please Select Progress Level" TabIndex="5">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator63" runat="server" ControlToValidate="ddlProgressLavel" InitialValue="0"
                                                        Display="None" ErrorMessage="Please Select Progress Level" SetFocusOnError="true" ValidationGroup="btnShowFilter" />--%>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-4">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <asp:Label ID="Label21" runat="server" Font-Bold="true" Text="Aptitude Test Centre"></asp:Label>

                                                    </div>
                                                    <asp:DropDownList ID="ddlAptitudeTestCentre" runat="server" CssClass="form-control" data-select2-enable="true"
                                                        AppendDataBoundItems="True"
                                                        ToolTip="Please Select Progress Level" TabIndex="6">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator63" runat="server" ControlToValidate="ddlProgressLavel" InitialValue="0"
                                                        Display="None" ErrorMessage="Please Select Progress Level" SetFocusOnError="true" ValidationGroup="btnShowFilter" />--%>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-4">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <asp:Label ID="Label22" runat="server" Font-Bold="true" Text="Aptitude Test Medium"></asp:Label>
                                                        <asp:HiddenField ID="hdnDate" runat="server" />
                                                    </div>
                                                    <asp:DropDownList ID="ddlAptitudeTestMedium" runat="server" CssClass="form-control" data-select2-enable="true"
                                                        AppendDataBoundItems="True"
                                                        ToolTip="Please Select Progress Level" TabIndex="7">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator63" runat="server" ControlToValidate="ddlProgressLavel" InitialValue="0"
                                                        Display="None" ErrorMessage="Please Select Progress Level" SetFocusOnError="true" ValidationGroup="btnShowFilter" />--%>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <asp:Label ID="Label23" runat="server" Font-Bold="true" Text="Date (From-To)"></asp:Label>

                                                    </div>
                                                    <div id="picker" class="form-control">
                                                        <i class="fa fa-calendar"></i>&nbsp;
                                    <span id="date"></span>
                                                        <i class="fa fa-angle-down" aria-hidden="true" style="float: right; padding-top: 4px; font-weight: bold;"></i>
                                                    </div>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-4 country">
                                                    <div id="divstudent" runat="server" visible="false">
                                                        <div class="label-dynamic">
                                                            <sup>* </sup>
                                                            <asp:Label ID="lbluser" runat="server" Font-Bold="true"></asp:Label>
                                                            <%--     <label>User</label>--%>
                                                        </div>
                                                        <%--   <asp:ListBox ID="ddlUser" runat="server" AppendDataBoundItems="true" CssClass="form-control multi-select-demo"
                                                                SelectionMode="multiple"></asp:ListBox>--%>
                                                        <asp:DropDownList ID="ddlUser" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvstudent" runat="server" ControlToValidate="ddlUser" InitialValue="0"
                                                            Display="None" ErrorMessage="Please Select User" SetFocusOnError="true" ValidationGroup="Show" />
                                                    </div>
                                                </div>

                                                <div class="col-12 btn-footer mt-4">
                                                    <asp:Button ID="btnshow" runat="server" Text="Show" CssClass="btn btn-outline-info" OnClick="btnshow_Click" ValidationGroup="btnShowFilter" />
                                                    <asp:Button ID="btnSubmit1" runat="Server" Text="Submit" CssClass="btn btn-outline-info" Visible="false" ValidationGroup="Show" OnClick="btnSubmit1_Click" />
                                                    <span runat="server" id="btnemail" visible="false">
                                                        <span id="btnBulkEmail" class="btn btn-outline-info">Send Email</span>
                                                    </span>

                                                    <asp:Button ID="btnClear1" runat="server" Text="Cancel" CssClass="btn btn-outline-danger" Visible="false" OnClick="btnClear1_Click" />


                                                    <span id="SpanVerify" style="cursor: pointer" class="btn btn-outline-info" data-toggle="modal" data-target="#divExcelVerify">Excel</span>
                                                    <span id="SpanVerifyCamparison" style="cursor: pointer" class="btn btn-outline-info" data-toggle="modal" data-target="#divCamparision">Lead Camparision Excel </span>
                                                    <asp:ValidationSummary ID="valsubmit" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                        ShowSummary="false" ValidationGroup="Show" />
                                                    <asp:ValidationSummary ID="ValidationSummary18" runat="server" DisplayMode="List" ShowMessageBox="true"
                                                        ShowSummary="false" ValidationGroup="btnShowFilter" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-12" id="DivMAinPanel" runat="server">
                                        <div id="showbar" runat="server" visible="false">
                                            <div class="bar-status mb-2">
                                                <i class="fa fa-circle" aria-hidden="true" style="color: #85aa25"></i><span>100% completed</span>
                                                <i class="fa fa-circle" aria-hidden="true" style="color: #ff607b"></i><span>In-Complete</span>
                                            </div>
                                        </div>

                                        <div class="col-12 pl-0 pr-0">
                                            <div id="divAllDemands" runat="server" visible="false">

                                                <%--<asp:Panel ID="Panel1" runat="server">--%>
                                                <asp:ListView ID="lvLeadDetails" runat="server">
                                                    <%--OnItemDataBound="lvLeadDetails_ItemDataBound"  ClientIDMode="AutoID" --%>
                                                    <LayoutTemplate>
                                                        <div class="sub-heading">
                                                            <h5>Student List </h5>
                                                        </div>
                                                        <div class="row mb-1">
                                                            <div class="col-lg-2 col-md-6 offset-lg-7">
                                                                <button type="button" class="btn btn-outline-primary float-lg-right saveAsExcel">Export Excel</button>
                                                            </div>

                                                            <div class="col-lg-3 col-md-6">
                                                                <div class="input-group sea-rch">
                                                                    <input type="text" id="FilterData1" class="form-control" placeholder="Search" />
                                                                    <div class="input-group-addon">
                                                                        <i class="fa fa-search"></i>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>
                                                        <div class="table-responsive" style="max-height: 500px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="MainLeadTable">
                                                                <thead class="bg-light-blue" style="position: sticky; top: 0; background: #fff !important; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                    <tr>
                                                                        <th class="re-move">
                                                                            <asp:CheckBox ID="chkCheckAll" runat="server" ToolTip="Select or Deselect All Records" />
                                                                        </th>

                                                                        <th>Application No
                                                                        </th>
                                                                        <th>Applicant Name
                                                                        </th>

                                                                        <th>Mobile No
                                                                        </th>
                                                                        <th>Email Id
                                                                        </th>
                                                                        <th>Study Level
                                                                        </th>
                                                                        <th>Progress
                                                                        </th>
                                                                        <th>Main counselor
                                                                        </th>
                                                                        <th>Sub counselor
                                                                        </th>
                                                                        <th>Lead Status
                                                                        </th>
                                                                        <th>Inquiry Source</th>
                                                                        <th>Date</th>
                                                                        <th>Time</th>

                                                                        <th>Intake
                                                                        </th>
                                                                        <th>Country
                                                                        </th>
                                                                        <th>Lead Remark
                                                                        </th>
                                                                        <th>NIC
                                                                        </th>
                                                                        <th>Last updated date
                                                                        </th>
                                                                        <th>Last updated by
                                                                        </th>
                                                                        <th>Enquiry Programme
                                                                        </th>
                                                                        <th>Applied Programme
                                                                        </th>
                                                                        <%--<th>Specialization
                                                                                </th>--%>
                                                                        <th>Aptitude marks
                                                                        </th>
                                                                        <th>Aptitude Status
                                                                        </th>
                                                                        <th>Interview Status

                                                                        </th>
                                                                        <th>Offer letter</th>

                                                                        <th>Aptitude Communication</th>

                                                                        <th>Aptitude Center</th>

                                                                        <th>Application update by</th>
                                                                        <th>Marks Entered by</th>
                                                                        <th>Cut off Set by</th>
                                                                        <th>Offer Letter sent by</th>
                                                                        <th>Registered by</th>
                                                                        <th>Remark</th>
                                                                        <th>Registered</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                                <asp:PlaceHolder runat="server" ID="groupPlaceHolder1"></asp:PlaceHolder>
                                                            </table>
                                                        </div>

                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:CheckBox ID="chkCheck" runat="server" EnableViewState="true" />
                                                            </td>
                                                            <%--<td><asp:Label ID="lblApplicationNo" runat="server" Text='<%# Eval("ENROLLNO")%>'></asp:Label>--%>
                                                            <td>
                                                                <asp:LinkButton ID="lnkApplicationNo" runat="server" Text='<%# Eval("ENROLLNO")%>' ToolTip='<%# Eval("MAIN_USER")%>' CommandArgument='<%# Eval("USERNO")%>' OnClick="lnkApplicationNo_Click"></asp:LinkButton>
                                                                <%-- <a href='<%# Eval("T_Link") %>' target="_blank"><%# Eval("ENROLLNO")%></a>--%>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblFirstname" runat="server" Text='<%# Eval("FIRSTNAME")%>'></asp:Label></td>

                                                            <td>
                                                                <asp:Label ID="lblMobileNo" runat="server" Text='<%# Eval("MOBILENO")%>'></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="lblEmailId" runat="server" Text='<%# Eval("EMAILID")%>'></asp:Label></td>

                                                            <td>
                                                                <asp:Label ID="lblStudylevel" runat="server" Text='<%# Eval("UA_SECTIONNAME")%>'></asp:Label>

                                                                <asp:HiddenField ID="hdnEnqueryNo" runat="server" Value='<%# Eval("USERNO")%>' />
                                                            </td>
                                                            <td><%# Eval("PERCENTAGE_CALCULATION")%></td>
                                                            <td>
                                                                <asp:Label ID="lblAllottedLead" runat="server" Text='<%# Eval("MAIN_USER")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblSubCounsoller" runat="server" Text='<%# Eval("USER_NAMES")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("NEW_LEAD")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label16" runat="server" Text='<%# Eval("SOURCETYPENAME")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblDate" runat="server" Text='<%# Eval("REGDATE")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label8" runat="server" Text='<%# Eval("REGTIME")%>'></asp:Label>
                                                            </td>

                                                            <td>
                                                                <asp:Label ID="lblIntake" runat="server" Text='<%# Eval("BATCHNAME")%>'></asp:Label></td>


                                                            <td>
                                                                <asp:Label ID="lblCountry" runat="server" Text='<%# Eval("LCOUNTRY")%>'></asp:Label></td>

                                                            <td>
                                                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("NEW_REMARK")%>'></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("NIC")%>'></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="Label5" runat="server" Text='<%# Eval("ENQUIRYSTATUS_DATE")%>'></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="Label6" runat="server" Text='<%# Eval("LEAD_UA_NAME")%>'></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="Label7" runat="server" Text='<%# Eval("PROGRAMS")%>'></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="Label15" runat="server" Text='<%# Eval("APPLIED_PROGRAMS")%>'></asp:Label></td>
                                                            <%--<td>
                                                                    <asp:Label ID="Label8" runat="server" Text='<%# Eval("SPECIALIZATION")%>'></asp:Label></td>--%>
                                                            <td>
                                                                <asp:Label ID="Label9" runat="server" Text='<%# Eval("APTITUDE_MARKS")%>'></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="Label10" runat="server" Text='<%# Eval("APTITUDE_RESULT")%>'></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="Label11" runat="server" Text='<%# Eval("INTERVIEW_RESULT")%>'></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="Label12" runat="server" Text='<%# Eval("SEND_OFFER_LETTER")%>'></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="Label13" runat="server" Text='<%# Eval("EXAM_RESULT") %>'></asp:Label></td>
                                                            <td>
                                                                <asp:Label ID="Label14" runat="server" Text='<%# Eval("CAMPUSNAME")%>'></asp:Label></td>
                                                            <td><%# Eval("APPLICATION_UPDATE_BY")%></td>
                                                            <td><%# Eval("MARK_ENTER_BY")%></td>
                                                            <td><%# Eval("CUT_OFF_SET_BY")%></td>
                                                            <td><%# Eval("OFFER_LETTER_SENT_BY")%></td>
                                                            <td><%# Eval("REGISTERED_BY")%></td>
                                                            <td><%# Eval("LEAD_REMARK")%></td>
                                                            <td><%# Eval("ENROLLED_STUDENTS")%></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                                <%--</asp:Panel>--%>
                                                <div class="col-12">
                                                    <label class="" style="font-weight: 400;">
                                                        Showing
                                                        <asp:Label ID="lblTotalCount" runat="server" Visible="false"></asp:Label>
                                                        entries</label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlMainLeadLabel" />
                                    <asp:AsyncPostBackTrigger ControlID="lstbSecondHead" />
                                    <asp:PostBackTrigger ControlID="btnApplyFilter" />
                                    <asp:PostBackTrigger ControlID="btnClearFilter" />
                                    <asp:AsyncPostBackTrigger ControlID="btnshow" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>


                        <div id="divReports" runat="server" visible="false" role="tabpanel" aria-labelledby="Grade-tab">
                            <div>
                                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updReports"
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
                            <asp:UpdatePanel ID="updReports" runat="server">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-md-12 col-sm-12 col-12">
                                            <div class="sub-heading mt-3">
                                                <h5>Reports</h5>
                                            </div>
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <asp:Label ID="lblDYAdmbatch" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlAdmissionBatch" runat="server" AutoPostBack="true" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlAdmissionBatch_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <%--<asp:RequiredFieldValidator ID="rfvddlAdm" runat="server" ControlToValidate="ddlAdmissionBatch" Display="None"
                                                                                    ErrorMessage="Please Select Admission Batch" SetFocusOnError="true" ValidationGroup="report" InitialValue="0" />

                                                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlAdmissionBatch" Display="None"
                                                                                    ErrorMessage="Please Select Admission Batch" SetFocusOnError="true" ValidationGroup="FillUpExcel" InitialValue="0" />--%>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <asp:Label ID="lblDYDegree" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlDegree" runat="server" AutoPostBack="true" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>


                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-12 btn-footer" id="DIVREPORT" runat="server" visible="false">

                                        <div class="dropdown dropleft">
                                            <a class="btn btn-outline-info dropdown-toggle btnXX" data-toggle="dropdown">Pre Admission</a>
                                            <div class="dropdown-menu dropdown-menu-custom">

                                                <asp:Button ID="btnReport" runat="server" Text="Pre Admission" ValidationGroup="report" CssClass="dropdown-item btn btn-outline-primary btnX"
                                                    TabIndex="1" Visible="false" />

                                                <asp:Button ID="btnShowReport" runat="server" Text="Show Report" OnClick="btnShowReport_Click"
                                                    ValidationGroup="report" TabIndex="1" CssClass="dropdown-item btn btn-outline-primary btnX" />

                                                <asp:Button ID="btnFillUpStatusReport" runat="server" Text="Fill Up Status Report" TabIndex="1" OnClick="btnFillUpStatusReport_Click"
                                                    CssClass="dropdown-item btn btn-outline-primary btnX" ValidationGroup="FillUpExcel" />

                                                <%-- <asp:Button ID="btnstate" runat="server" Text="State Wise Report" TabIndex="1" ValidationGroup="report" CssClass="dropdown-item btn btn-outline-primary btnX"
                                     />

                                <asp:Button ID="btnReligion" runat="server" Text="Religion Wise Report" TabIndex="1" ValidationGroup="report" CssClass="dropdown-item btn btn-outline-primary btnX"
                                     />--%>
                                            </div>
                                        </div>



                                        <div class="dropdown dropleft" id="DIVREPORT2" runat="server" visible="false">
                                            <a class="btn btn-outline-info dropdown-toggle btnXX" data-toggle="dropdown">Post Admission</a>
                                            <div class="dropdown-menu dropdown-menu-custom">

                                                <asp:Button ID="btnPostAdmission" runat="server" Text="Post Admission" ValidationGroup="report" CssClass="dropdown-item btn btn-outline-primary btnX"
                                                    TabIndex="1" Visible="false" />


                                                <asp:Button ID="btnAdmReport" runat="server" Text="Admission Report"
                                                    ValidationGroup="report" TabIndex="1" CssClass="dropdown-item btn btn-outline-primary btnX" OnClick="btnAdmReport_Click" />

                                                <asp:Button ID="btnStudentDataReport" runat="server" Text="Student Data"
                                                    ValidationGroup="StudentDataReport" TabIndex="1" CssClass="dropdown-item btn btn-outline-primary btnX" OnClick="btnStudentDataReport_Click" />

                                                <%--<asp:Button ID="btnCat" runat="server" Text="Category Wise Report" TabIndex="1" ValidationGroup="report" CssClass="dropdown-item btn btn-outline-primary btnX" />--%>

                                                <%-- <asp:Button ID="btnAdmDetailReport" runat="server" Text="Nominal List" CssClass="dropdown-item btn btn-outline-primary btnX" TabIndex="1"
                                     ValidationGroup="AdmDetail" />

                                <asp:Button ID="btnPTStudCount" runat="server" Visible="true" Text="PayType wise Student Count"  CssClass="dropdown-item btn btn-outline-primary btnX"
                                        TabIndex="1" ValidationGroup="report" />
                                    
                                <asp:Button ID="btnStudBlank" runat="server" Text="Student Blank Details Data" TabIndex="1"
                                    CssClass="dropdown-item btn btn-outline-primary btnX" ValidationGroup="StudExcel" />
                                    <asp:Button ID="btnStudExcelNew" runat="server" Text="Student Data Excel Report" TabIndex="1"
                                    CssClass="dropdown-item btn btn-outline-primary btnX" ValidationGroup="StudExcel" />   --%>
                                            </div>

                                        </div>

                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="1" CssClass="btn btn-outline-danger" OnClick="btnCancel_Click" />

                                        <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false"
                                            ValidationGroup="report" />
                                        <asp:ValidationSummary ID="valexcelsummary" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false" ValidationGroup="StudExcel" />

                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false"
                                            ValidationGroup="Academic" />

                                        <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false"
                                            ValidationGroup="AdmDetail" />
                                    </div>

                                    <div class="col-12">
                                        <asp:Panel ID="Panel8" runat="server">
                                            <asp:ListView ID="lvOnlineAdmissionDetails" runat="server" OnPagePropertiesChanging="lvOnlineAdmissionDetails_PagePropertiesChanging">
                                                <LayoutTemplate>
                                                    <div id="demo-grid">
                                                        <div class="sub-heading">
                                                            <h5>Online Admission List</h5>
                                                        </div>
                                                        <div class="row mb-1">
                                                            <div class="col-lg-2 col-md-6 offset-lg-7">
                                                                <button type="button" class="btn btn-outline-primary float-lg-right saveAsExcel2">Export Excel</button>
                                                            </div>


                                                            <div class="col-lg-3 col-md-6">
                                                                <div class="input-group sea-rch">
                                                                    <%--<input type="text" id="FilterData2" class="form-control" placeholder="Search" />--%>
                                                            <asp:TextBox ID="FilterData2" runat="server" TabIndex="1" CssClass="form-control" MaxLength="20" placeholder="Search" AutoPostBack="true" OnTextChanged="FilterData2_TextChanged"></asp:TextBox>

                                                                    <div class="input-group-addon">
                                                                        <i class="fa fa-search"></i>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>
                                                        <div class="table-responsive" style="max-height: 520px; overflow: scroll; border-top: 1px solid #e5e5e5;">
                                                            <table class="table table-striped table-bordered nowrap" style="width: 100%;" id="MainLeadTable2">
                                                                <thead class="bg-light-blue" style="position: sticky; z-index: 1; background: #fff!important; top: 0; box-shadow: rgba(0, 0, 0, 0.2) 0px 0px 1px;">
                                                                    <tr>
                                                                        <th>Sr No.</th>
                                                                        <th>Intake</th>
                                                                        <th>UserName</th>
                                                                        <th>First Name</th>
                                                                        <th>Last Name</th>
                                                                        <th>Initial Name</th>
                                                                        <th>Email</th>
                                                                        <th>Mobile Code</th>
                                                                        <th>Mobile No.</th>
                                                                        <th>ACR No.</th>
                                                                        <th>Passport No.</th>
                                                                        <th>NIC No.</th>
                                                                        <th>Home Telephone Code</th>
                                                                        <th>Home Telephone No.</th>
                                                                        <th>Date of Birth</th>
                                                                        <th>Gender</th>
                                                                        <th>Citizen No.</th>
                                                                        <th>Study Level</th>
                                                                        <th>Address</th>
                                                                        <th>Country Name</th>
                                                                        <th>State Name</th>
                                                                        <th>District</th>
                                                                        <th style="width:500px !important">Apply Program Details</th>
                                                                        <th>Order Id</th>
                                                                        <th>Fees Type</th>
                                                                        <th>Amount</th>
                                                                        <th>Transaction Id</th>
                                                                        <th>Recon Date</th>
                                                                        <th>Aptitute Center Name</th>
                                                                        <th>Campus Name</th>
                                                                        <th>Mode Name</th>
                                                                        <th>Medium Name</th>
                                                                        <th>Weekday Name</th>

                                                                        <th>PE School Name</th>
                                                                        <th>PE School Address</th>
                                                                        <th>PE School Region</th>
                                                                        <th>PE Year</th>
                                                                        <%--    <th>PE Type</th>--%>

                                                                        <th>Ele School Name</th>
                                                                        <th>Ele School Address</th>
                                                                        <th>Ele School Region</th>
                                                                        <th>Ele Year</th>
                                                                        <%--    <th>Ele Type</th>--%>

                                                                        <th>JR School Name</th>
                                                                        <th>JR School Address</th>
                                                                        <th>JR School Region</th>
                                                                        <th>JR Year</th>
                                                                        <%--   <th>JR Type</th>--%>

                                                                        <th>SR School Name</th>
                                                                        <th>SR School Address</th>
                                                                        <th>SR School Region</th>
                                                                        <th>SR Year</th>
                                                                        <%--     <th>SR Type</th>--%>

                                                                        <th>UG School Name</th>
                                                                        <th>UG School Address</th>
                                                                        <th>UG School Region</th>
                                                                        <th>UG Year</th>
                                                                        <%--    <th>UG Type</th>--%>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                    <div class="col-12 pt-1 pb-2" id="Tfoot1" runat="server">
                                                        <div class="float-right">
                                                            <asp:DataPager ID="DataPager1" runat="server" PagedControlID="lvOnlineAdmissionDetails" PageSize="1000">
                                                                <Fields>
                                                                    <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="false" ShowPreviousPageButton="true"
                                                                        ShowNextPageButton="false" />
                                                                    <asp:NumericPagerField ButtonType="Link" />
                                                                    <asp:NextPreviousPagerField ButtonType="Link" ShowNextPageButton="true" ShowLastPageButton="false" ShowPreviousPageButton="false" />
                                                                </Fields>
                                                            </asp:DataPager>
                                                        </div>
                                                    </div>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><%# Container.DataItemIndex + 1%></td>
                                                        <td><%# Eval("BATCHNAME")%> </td>
                                                        <td><%# Eval("USERNAME")%> </td>
                                                        <td><%# Eval("FIRSTNAME")%> </td>
                                                        <td><%# Eval("LASTNAME")%> </td>
                                                        <td><%# Eval("NAME_INITIAL")%> </td>
                                                        <td><%# Eval("EMAILID")%> </td>
                                                        <td><%# Eval("MOBILECODE")%> </td>
                                                        <td><%# Eval("MOBILENO")%> </td>
                                                        <td><%# Eval("ACRNO")%> </td>
                                                        <td><%# Eval("PASSPORTNO")%> </td>
                                                        <td><%# Eval("NIC")%> </td>
                                                        <td><%# Eval("HOME_TEL_CODE")%> </td>
                                                        <td><%# Eval("HOME_MOBILENO")%> </td>
                                                        <td><%# Eval("DOB")%> </td>
                                                        <td><%# Eval("GENDER")%> </td>
                                                        <td><%# Eval("CITIZEN")%> </td>
                                                        <td><%# Eval("UA_SECTIONNAME")%> </td>
                                                        <td><%# Eval("PADDRESS")%> </td>
                                                        <td><%# Eval("COUNTRYNAME")%> </td>
                                                        <td><%# Eval("STATENAME")%> </td>
                                                        <td><%# Eval("DISTRICTNAME")%> </td>

                                                        <td style="width:500px !important"><%# Eval("APPLY_PROGRAM_DETAILS")%> </td>
                                                        <td><%# Eval("ORDER_ID")%> </td>
                                                        <td><%# Eval("FEES_TYPE")%> </td>
                                                        <td><%# Eval("AMOUNT")%> </td>
                                                        <td><%# Eval("APTRANSACTIONID")%> </td>
                                                        <td><%# Eval("RECONDATE")%> </td>
                                                        <td><%# Eval("APTITUDE_CENTER_NAME")%> </td>
                                                        <td><%# Eval("CAMPUSNAME")%> </td>
                                                        <td><%# Eval("MODE_NAME")%> </td>
                                                        <td><%# Eval("MEDIUM_NAME")%> </td>
                                                        <td><%# Eval("WEEKDAYSNAME")%> </td>


                                                        <td><%# Eval("PRE_SCHOOL_NAME")%> </td>
                                                        <td><%# Eval("PRE_SCHOOL_ADDRESS")%> </td>
                                                        <td><%# Eval("PRE_REGION")%> </td>
                                                        <td><%# Eval("PRE_YEAR")%> </td>

                                                        <td><%# Eval("ELE_SCHOOL_NAME")%> </td>
                                                        <td><%# Eval("ELE_SCHOOL_ADDRESS")%> </td>
                                                        <td><%# Eval("ELE_REGION")%> </td>
                                                        <td><%# Eval("ELE_YEAR")%> </td>

                                                        <td><%# Eval("JR_SCHOOL_NAME")%> </td>
                                                        <td><%# Eval("JR_SCHOOL_ADDRESS")%> </td>
                                                        <td><%# Eval("JR_REGION")%> </td>
                                                        <td><%# Eval("JR_YEAR")%> </td>

                                                        <td><%# Eval("SR_SCHOOL_NAME")%> </td>
                                                        <td><%# Eval("SR_SCHOOL_ADDRESS")%> </td>
                                                        <td><%# Eval("SR_REGION")%> </td>
                                                        <td><%# Eval("SR_YEAR")%> </td>

                                                        <td><%# Eval("UG_SCHOOL_NAME")%> </td>
                                                        <td><%# Eval("UG_SCHOOL_ADDRESS")%> </td>
                                                        <td><%# Eval("UG_REGION")%> </td>
                                                        <td><%# Eval("UG_YEAR")%> </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>

                                            <%-- <div class="row mt-4" id="Div3" runat="server" visible="false">
                                                <div class="form-group col-12 text-center">
                                                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-info" Text="Add New Grades" />
                                                </div>
                                            </div>--%>
                                        </asp:Panel>
                                    </div>

                                    <div class="col-12">
                                        <asp:Panel ID="Panel3" runat="server">
                                            <asp:ListView ID="lvShowReport" runat="server">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Lead Status List</h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                        <%-- <table class="table table-striped table-bordered nowrap" id="mytable" style="width:100%">--%>
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Sr No.</th>
                                                                <th>User Name </th>
                                                                <th>Applicant Name </th>
                                                                <th>Mobile No </th>
                                                                <th>Email Id</th>
                                                                <th>Registration Date</th>
                                                                <th>Degree Name</th>
                                                                <%-- <th >Date</th> --%>
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
                                                            <%# Container.DataItemIndex + 1%>
                                                        </td>

                                                        <td>
                                                            <asp:Label ID="lblUserName" runat="server" Text='<%# Eval("USERNAME")%>'></asp:Label>
                                                        </td>

                                                        <td><span><%# Eval("FIRSTNAME")%> &nbsp <%# Eval("LASTNAME")%></span>
                                                            <%-- <asp:Label ID="lblName" runat="server" Text='<%# Eval("FIRSTNAME")%> '></asp:Label>  --%>
                                                        </td>

                                                        <td>
                                                            <asp:Label ID="lblDegree" runat="server" Text='<%# Eval("MOBILENO")%>'></asp:Label>
                                                        </td>

                                                        <td>
                                                            <asp:Label ID="lblStudyLevel" runat="server" Text='<%# Eval("EMAILID")%>'></asp:Label>
                                                        </td>

                                                        <td>
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("REGDATE")%>'></asp:Label>
                                                        </td>

                                                        <td>
                                                            <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("DEGREENAME")%>'></asp:Label>
                                                        </td>

                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>

                                            <%-- <div class="row mt-4" id="Div3" runat="server" visible="false">
                                                <div class="form-group col-12 text-center">
                                                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-info" Text="Add New Grades" />
                                                </div>
                                            </div>--%>
                                        </asp:Panel>
                                    </div>

                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnFillUpStatusReport" />
                                    <asp:AsyncPostBackTrigger ControlID="btnStudentDataReport" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>

                        <%-- Added BY Aashna 28-10-2021 started--%>
                        <div id="divQuery" runat="server" visible="false" role="tabpanel" aria-labelledby="Query-tab">
                            <div>
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updQuery"
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
                            <asp:UpdatePanel ID="updQuery" runat="server">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-md-12 col-sm-12 col-12">
                                            <div class="sub-heading mt-3">
                                                <h5>Query</h5>
                                            </div>

                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <asp:Label ID="lblCategoryofQueriess" runat="server" Font-Bold="true"></asp:Label>
                                                        <%-- <label>Category of Queries</label>--%>
                                                    </div>
                                                    <asp:DropDownList ID="ddlFormCategory" CssClass="form-control" data-select2-enable="true" runat="server" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlFormCategory_SelectedIndexChanged"
                                                        ValidationGroup="submit">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvFormCategory" runat="server" ErrorMessage="Please Select Form Category" InitialValue="0" ControlToValidate="ddlFormCategory" ValidationGroup="submit" Display="None">
                                                    </asp:RequiredFieldValidator>
                                                </div>

                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-12">
                                        <asp:Panel ID="Panel4" runat="server">
                                            <asp:ListView ID="lvStudentQuery" runat="server" OnItemDataBound="lvStudentQuery_ItemDataBound" OnPagePropertiesChanged="lvStudentQuery_PagePropertiesChanged">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                        <h5>Student Query List </h5>
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Application Id</th>
                                                                <th>Applicant Name </th>
                                                                <th>Date</th>
                                                                <th>Email Id</th>
                                                                <th>Contact Number</th>
                                                                <th>Query Status </th>

                                                                <th>Action</th>
                                                            </tr>

                                                        </thead>
                                                        <tbody>
                                                            <tr id="itemplaceholder" runat="server"></tr>
                                                        </tbody>
                                                    </table>
                                                </LayoutTemplate>

                                                <ItemTemplate>
                                                    <tr>

                                                        <td>
                                                            <asp:Label ID="lbluser" runat="server" Text='<%# Eval("username")%>' ToolTip='<%# Eval("userno")%>' />
                                                            <asp:HiddenField ID="hdnuser" runat="server" Value='<%# Eval("userno")%>' />
                                                        </td>

                                                        <td>
                                                            <asp:Label ID="lblfirstname" runat="server" Text='<%# Eval("firstname")%>' ToolTip='<%# Eval("firstname")%>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label17" runat="server" Text='<%# Eval("QUERYDATE")%>' ToolTip='<%# Eval("QUERYDATE")%>' />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblemailid" runat="server" Text='<%# Eval("emailid")%>' ToolTip='<%# Eval("emailid")%>' />
                                                        </td>

                                                        <td>
                                                            <asp:Label ID="lblmobile" runat="server" Text='<%# Eval("mobile")%>' ToolTip='<%# Eval("mobile")%>' />
                                                        </td>

                                                        <td>
                                                            <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("feedback_status")%>' ToolTip='<%# Eval("feedback_status")%>' />
                                                        </td>


                                                        <td>
                                                            <asp:Button runat="server" Text="Reply" CssClass="btn btn-outline-info" ID="btnPriview" CommandName='<%# Eval("userno")%>' ToolTip='<%# Eval("QUERY_CATEGORY")%>' OnClick="btnPriview_Click" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </asp:Panel>

                                    </div>


                                </ContentTemplate>

                            </asp:UpdatePanel>


                        </div>

                        <div id="divEnquiry" runat="server" visible="false" role="tabpanel" aria-labelledby="Enquiry-tab">
                            <div>
                                <asp:UpdateProgress ID="UpdateProgress6" runat="server" AssociatedUpdatePanelID="updEnquiry"
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
                            <asp:UpdatePanel ID="updEnquiry" runat="server">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-md-12 col-sm-12 col-12">
                                            <div class="sub-heading mt-3">
                                                <h5>Enquiry</h5>
                                            </div>
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <asp:Label ID="lblfirstname" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" MaxLength="20" onblur="checkLength(this)" TabIndex="1"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvfullname" runat="server" ControlToValidate="txtFullName"
                                                        ErrorMessage="First Name Required !" Display="None" ValidationGroup="Register"></asp:RequiredFieldValidator>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbGrade" runat="server" TargetControlID="txtFullName"
                                                        InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}0123456789[]\|-&&quot;'" FilterMode="InvalidChars" />

                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <asp:Label ID="lblLastname" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" MaxLength="20" onblur="checkLength(this)" TabIndex="2"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtLastName"
                                                        ErrorMessage="Last Name Required !" Display="None" ValidationGroup="Register"></asp:RequiredFieldValidator>
                                                    <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtLastName"
                                                        InvalidChars="~`!@#$%^*()_+=,./:;<>?'{}0123456789[]\|-&&quot;'" FilterMode="InvalidChars" />

                                                </div>


                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <asp:Label ID="lblEmail" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:TextBox ID="txtEmailid" runat="server" CssClass="form-control" TabIndex="3"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvemail" runat="server" ControlToValidate="txtEmailid"
                                                        ErrorMessage="Email ID Required !" Display="None" ValidationGroup="Register"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="rfvuserEmail" runat="server" ControlToValidate="txtEmailid"
                                                        Display="None" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                        ErrorMessage="Please Enter Valid EmailID" ValidationGroup="Register"></asp:RegularExpressionValidator>

                                                    <ajaxToolKit:FilteredTextBoxExtender ID="ftbxemailid" runat="server" TargetControlID="txtEmailid"
                                                        InvalidChars="ABCDEFGHIJKLNMOPQRSTUVWXYZ" FilterMode="InvalidChars" />

                                                </div>


                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <asp:Label ID="lblStudentMobileNumber" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon" style="border-color: transparent; padding: 0px; border-bottom: 1px solid transparent;">
                                                            <asp:DropDownList ID="ddlMobileCode" runat="server" AppendDataBoundItems="true" CssClass="form-control select2 select-clik" Style="padding-right: 0px!important;" TabIndex="4" onchange="return RemoveCountryName()">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control ml-2" MaxLength="10" TabIndex="5"></asp:TextBox>

                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtMobileNo" ValidChars="1234567890" FilterMode="ValidChars" />
                                                        <asp:RequiredFieldValidator ID="rfvmobileno" runat="server" ErrorMessage="Mobile No. Required !"
                                                            ControlToValidate="txtMobileNo" Display="None" ValidationGroup="Register"></asp:RequiredFieldValidator>

                                                        <asp:RegularExpressionValidator runat="server" ErrorMessage="Mobile No. is Invalid"
                                                            ID="revMobile" ControlToValidate="txtMobileNo" ValidationExpression=".{10}.*"
                                                            Display="None" ValidationGroup="Register"></asp:RegularExpressionValidator>
                                                        <asp:RequiredFieldValidator ID="rfvmobilecode" runat="server" ErrorMessage="Mobile Code Required !"
                                                            ControlToValidate="ddlMobileCode" Display="None" ValidationGroup="Register" InitialValue="0"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>


                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <asp:Label ID="lblhometel" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon" style="border-color: transparent; padding: 0px; border-bottom: 1px solid transparent;">
                                                            <asp:DropDownList ID="ddlHomeTelMobileCode" runat="server" AppendDataBoundItems="true" CssClass="form-control select2 select-clik" Style="padding-right: 0px!important;" TabIndex="6" onchange="return RemoveHomeCountryName()">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <asp:TextBox ID="txtHomeMobileNo" runat="server" CssClass="form-control ml-2" MaxLength="10" TabIndex="7"></asp:TextBox>

                                                        <ajaxToolKit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtHomeMobileNo" ValidChars="1234567890" FilterMode="ValidChars" />
                                                        <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Home Telephone No. Required !"
                                                            ControlToValidate="txtHomeMobileNo" Display="None" ValidationGroup="Register"></asp:RequiredFieldValidator>--%>

                                                        <asp:RegularExpressionValidator runat="server" ErrorMessage="Home Telephone No. is Invalid"
                                                            ID="RegularExpressionValidator1" ControlToValidate="txtHomeMobileNo" ValidationExpression=".{10}.*"
                                                            Display="None" ValidationGroup="Register"></asp:RegularExpressionValidator>
                                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Home Telephone Code Required !"
                                                            ControlToValidate="ddlHomeTelMobileCode" Display="None" ValidationGroup="Register" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                                    </div>
                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup></sup>
                                                        <asp:Label ID="lbldob" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <i id="dvcal1" runat="server" class="fa fa-calendar"></i>
                                                        </div>

                                                        <asp:TextBox ID="txtDob" runat="server" CssClass="form-control dob" TabIndex="8"></asp:TextBox>

                                                        <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Date of Birth Required !"
                                                            ControlToValidate="txtDob" Display="None" ValidationGroup="Register"></asp:RequiredFieldValidator>--%>
                                                    </div>
                                                </div>


                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <asp:Label ID="lblSelectGender" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <%--<label class="form-check-label">--%>
                                                    <asp:RadioButtonList ID="rdGender" runat="server" RepeatDirection="Horizontal" class="form-check-label" TabIndex="9">
                                                        <asp:ListItem Value="0">M &nbsp;</asp:ListItem>
                                                        <asp:ListItem Value="1">F &nbsp;</asp:ListItem>
                                                        <%-- <asp:ListItem Value="2">Other &nbsp;</asp:ListItem>--%>
                                                    </asp:RadioButtonList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Gender Required !" ControlToValidate="rdGender" Display="None" ValidationGroup="Register"></asp:RequiredFieldValidator>
                                                    <%--</label>--%>
                                                </div>


                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <%--  <sup>* </sup>  --%>
                                                        <asp:Label ID="lblpassportno" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:TextBox ID="txtPassport" runat="server" CssClass="form-control" TabIndex="11" MaxLength="20" onblur="return Validator();"></asp:TextBox>


                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <%--  <sup>* </sup>  --%>
                                                        <asp:Label ID="lblnic" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:TextBox ID="txtNIC" runat="server" CssClass="form-control" TabIndex="12" MaxLength="20" onblur="return Validator();"></asp:TextBox>


                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <asp:Label ID="lblDyAdmissionType" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlStudyType" runat="server" AppendDataBoundItems="true" CssClass="form-control select2 select-clik" AutoPostBack="true" OnSelectedIndexChanged="ddlStudyType_SelectedIndexChanged" TabIndex="10">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>

                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Study level Required !"
                                                        ControlToValidate="ddlStudyType" Display="None" ValidationGroup="Register" InitialValue="0"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <asp:Label ID="lblProgram" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:ListBox ID="ddlProgramIntrested" runat="server" CssClass="form-control multi-select-demo"
                                                        SelectionMode="Multiple" AppendDataBoundItems="true" TabIndex="13" OnSelectedIndexChanged="ddlProgramIntrested_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Program Interested in Required !"
                                                        ControlToValidate="ddlProgramIntrested" Display="None" ValidationGroup="Register"></asp:RequiredFieldValidator>

                                                </div>
                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <asp:Label ID="lblPrPrefere" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:ListBox ID="ddlPreference" runat="server" CssClass="form-control multi-select-demo"
                                                        SelectionMode="Multiple" TabIndex="14"></asp:ListBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Program Preference Required !"
                                                        ControlToValidate="ddlPreference" Display="None" ValidationGroup="Register"></asp:RequiredFieldValidator>

                                                </div>

                                                <div class="form-group col-lg-3 col-md-6 col-12">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <asp:Label ID="lblSource" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlSource" runat="server" AppendDataBoundItems="true" CssClass="form-control select2 select-clik" Style="padding-right: 0px!important;" TabIndex="15">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Type of Source Required !"
                                                        ControlToValidate="ddlSource" Display="None" ValidationGroup="Register" InitialValue="0"></asp:RequiredFieldValidator>

                                                </div>


                                                <div class="col-12">
                                                    <div class="form-check">
                                                        <asp:CheckBox ID="chkConfirm" type="checkbox" class="form-check-input" runat="server" TabIndex="15" />
                                                        <label class="form-check-label" for="chkConfirm">I confirm that the above information is correct.</label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-12 btn-footer">
                                        <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="btn btn-outline-info btnX" OnClick="btnRegister_Click" ValidationGroup="Register" TabIndex="16" />
                                        <asp:ValidationSummary ID="val_summary" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Register" DisplayMode="List" />
                                    </div>
                                    <%--    <p class="text-center mb-0">Already have an account ? <a href="signIn.aspx" class="hypr-link">Login</a></p>--%>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </div>

                        <div id="divRemark" runat="server" visible="false" role="tabpanel" aria-labelledby="Remark-tab">
                            <div>
                                <asp:UpdateProgress ID="UpdateProgress17" runat="server" AssociatedUpdatePanelID="updRemark"
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
                            <asp:UpdatePanel ID="updRemark" runat="server">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-md-12 col-sm-12 col-12">
                                            <div class="sub-heading mt-3">
                                                <h5>Remark</h5>
                                            </div>
                                            <div class="row">
                                                <div class="form-group col-lg-3 col-md-6 col-4">
                                                    <div class="label-dynamic">
                                                        <sup>* </sup>
                                                        <asp:Label ID="lblAdmissionBatch" runat="server" Font-Bold="true"></asp:Label>
                                                    </div>
                                                    <asp:DropDownList ID="ddlRemarkAdmBatch" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="true"
                                                        AppendDataBoundItems="True" ToolTip="Please Select Intake" TabIndex="1" OnSelectedIndexChanged="ddlRemarkAdmBatch_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="col-lg-12 col-md-12 col-12 mb-3">
                                                    <asp:ListView ID="lvRemarkList" runat="server">
                                                        <LayoutTemplate>
                                                            <div class="sub-heading">
                                                                <h5>Application Status List</h5>
                                                            </div>

                                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                <thead class="bg-light-blue">
                                                                    <tr>
                                                                        <th>User Name</th>
                                                                        <th>Student Name</th>
                                                                        <th>Email</th>
                                                                        <th>Mobile No.</th>
                                                                        <th>Enquiry Date</th>
                                                                        <th>Lead Stage</th>
                                                                        <th>Remark</th>
                                                                        <th>Added By</th>
                                                                        <th>Next Follow Up Date</th>

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
                                                                    <%# Eval("USERNAME") %>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("FIRSTNAME") %>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("EMAILID") %>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("MOBILENO") %>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("ENQUIRY_DATE") %>
                                                                </td>
                                                                <td>
                                                                    <span><%# Eval("OLD_LEAD") %> <%# Eval("NEW_LEAD") %></span>
                                                                </td>

                                                                <td>
                                                                    <span><%# Eval("OLD_REMARK") %> <%# Eval("NEW_REMARK") %></span>
                                                                </td>
                                                                <td>
                                                                    <b>
                                                                        <asp:Label runat="server" ID="lblLeadStatusRemark" Text='<%# Eval("LEAD_STATUS") %>'></asp:Label></b><span> Councellor: <b><%# Eval("UA_NAME") %> </b></span>
                                                                    <%--<span>Councellor: <b><%# Eval("UA_NAME") %> </b> Add Lead Stage <span style="color:blue"><b><%# Eval("LEAD_STAGE_NAME") %></b></span> And Remark is <span style="color:blue"><b><%# Eval("REMARKS") %></b></span></span></td>--%>
                                                                    <%-- <span>Councellor: <b><%# Eval("UA_NAME") %> </b><%# Eval("OLD_LEAD") %> <%# Eval("NEW_LEAD") %> <%# Eval("OLD_REMARK") %> <%# Eval("NEW_REMARK") %></span>--%>
                                                                    

                                                                </td>
                                                                <td>
                                                                    <%# Eval("NEXTFOLLOUP_DATE") %>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:ListView>

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
    </div>

    <div class="modal fade beauty" id="myApplicationModal">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">

                <div>
                    <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updGradeEntry"
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

                <asp:UpdatePanel ID="updGradeEntry" runat="server">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-md-12 col-sm-12 col-12">
                                <div class="modal-header">
                                    <h3 class="modal-title">Application Details</h3>
                                    <button type="button" class="close" data-dismiss="modal" title="Close">&times;</button>
                                </div>
                                <div id="divMsg" runat="server"></div>
                                <div class="box-body mt-2">
                                    <div class="col-12">
                                        <div class="row">
                                            <div class="col-lg-6 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>
                                                        <asp:Label ID="lblStudName" runat="server" Font-Bold="true"></asp:Label>
                                                        :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblName" runat="server"></asp:Label>
                                                        </a>
                                                    </li>
                                                    <li class="list-group-item"><b>
                                                        <asp:Label ID="lblEmailNew" runat="server" Font-Bold="true"></asp:Label>
                                                        :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblEmailId" runat="server"></asp:Label>
                                                        </a>
                                                    </li>
                                                    <li class="list-group-item"><b>
                                                        <asp:Label ID="lblPhoneNo" runat="server" Font-Bold="true"></asp:Label>
                                                        :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblPhone" runat="server"></asp:Label>
                                                        </a>
                                                    </li>
                                                    <li class="list-group-item"><b>NIC
                                                        :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblNICApp" runat="server"></asp:Label>
                                                        </a>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-12">
                                                <ul class="list-group list-group-unbordered">
                                                    <li class="list-group-item"><b>
                                                        <asp:Label ID="lblLeadStatus" runat="server" Font-Bold="true"></asp:Label>
                                                        :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblLeadStatusName" runat="server"></asp:Label>
                                                        </a>
                                                        <a data-toggle="modal" data-target="#myModalApp" id="lbtForgePass" tabindex="7" runat="server" style="cursor: pointer"><span>
                                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/edit.gif"
                                                                AlternateText="Edit Record" ToolTip="Edit Record" AutoPostBack="true" /></span>
                                                        </a>
                                                    </li>
                                                    <li class="list-group-item"><b>
                                                        <asp:Label ID="lblApplicationStage" runat="server" Font-Bold="true"></asp:Label>
                                                        :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblApplicationStageState" runat="server"></asp:Label>
                                                        </a>
                                                    </li>
                                                    <li class="list-group-item"><b>
                                                        <asp:Label ID="lblAddress" runat="server" Font-Bold="true"></asp:Label>
                                                        :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblApplAddress" runat="server"></asp:Label>
                                                        </a>
                                                    </li>
                                                    <li class="list-group-item"><b>Passport
                                                        :</b>
                                                        <a class="sub-label">
                                                            <asp:Label ID="lblPassportApp" runat="server"></asp:Label>
                                                        </a>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 mt-3 mb-3">
                                        <div class="sub-heading">
                                            <h5>Application Status</h5>
                                        </div>


                                        <section class="design-process-section" id="process-tab">
                                            <%-- <div class="container">
                                            <div class="row">
                                                <div class="col-md-12">--%>

                                            <ul class="process-model more-icon-preocess text-center" role="tablist">

                                                <li role="presentation">
                                                    <a>
                                                        <i id="iOnlineAdmSub" runat="server"></i>
                                                    </a>
                                                    <p>Application Filled</p>

                                                </li>

                                                <li role="presentation check">
                                                    <a>
                                                        <i id="iStudDocUpload" runat="server"></i>
                                                    </a>
                                                    <p>Application Payment</p>

                                                </li>

                                                <li role="presentation">
                                                    <a>
                                                        <i id="iAdmSecAprvl" runat="server"></i>
                                                    </a>
                                                    <p>Aptitude Test Cleared</p>

                                                </li>

                                                <li role="presentation">
                                                    <a>
                                                        <i id="iFinSecAprvl" runat="server"></i>
                                                    </a>
                                                    <p>Registration Done</p>

                                                </li>

                                                <%--  <li role="presentation">
                                                            <a>
                                                                <i id="iFeePayByStud" runat="server"></i>                                                               
                                                            </a>
                                                            <p>Payment</p>
                                                        </li>--%>
                                            </ul>

                                            <%--     </div>
                                            </div>
                                        </div>--%>
                                        </section>

                                    </div>

                                    <div class="col-12">
                                        <div class="nav-tabs-custom mt-2 col-12">
                                            <ul class="nav nav-tabs dynamic-nav-tabs" role="tablist">
                                                <li class="nav-item active" id="divlkApplication" runat="server">
                                                    <asp:LinkButton ID="lkAnnouncementApplication" runat="server" OnClick="lkAnnouncementApplication_Click" CssClass="nav-link" TabIndex="1">Lead Status</asp:LinkButton></li>
                                                <li class="nav-item" id="divlkFeed" runat="server">
                                                    <asp:LinkButton ID="lkFeedback" runat="server" CssClass="nav-link" OnClick="lkFeedback_Click" TabIndex="2">Lead Details</asp:LinkButton></li>
                                                <%--OnClick="lkFeedback_Click"--%>
                                            </ul>

                                            <div class="tab-content">
                                                <div class="tab-pane fade show active" id="DivLeadStatus" role="tabpanel" runat="server" aria-labelledby="ALCourses-tab">

                                                    <div>
                                                        <asp:UpdateProgress ID="UpdateProgress7" runat="server" AssociatedUpdatePanelID="UpdatePanel4"
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

                                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                        <ContentTemplate>
                                                            <div class="row">
                                                                <div class="col-md-12 col-sm-12 col-12">
                                                                    <div class="sub-heading mt-3">
                                                                        <h5>
                                                                            <asp:Label ID="Label1" runat="server"></asp:Label></h5>
                                                                    </div>

                                                                    <div class="row">
                                                                        <div class="col-lg-12 col-md-12 col-12 mb-3">
                                                                            <asp:ListView ID="lvlist" runat="server">
                                                                                <LayoutTemplate>
                                                                                    <div class="sub-heading">
                                                                                        <h5>Application Status List</h5>
                                                                                    </div>
                                                                                    <div class="table table-responsive">
                                                                                        <table class="table table-striped table-bordered nowrap" style="width: 100%" id="divdepartmentlist">
                                                                                            <thead>
                                                                                                <tr class="bg-light-blue">
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
                                                                                        <td style="text-align: left;">
                                                                                            <h6 style="font: bold"><%# Eval("ENQUIRY_DATE") %> </h6>
                                                                                            <b>
                                                                                                <asp:Label runat="server" ID="lblLeadStatus" Text='<%# Eval("LEAD_STATUS") %>'></asp:Label><br />
                                                                                            </b>
                                                                                            <%--<span>Councellor: <b><%# Eval("UA_NAME") %> </b> Add Lead Stage <span style="color:blue"><b><%# Eval("LEAD_STAGE_NAME") %></b></span> And Remark is <span style="color:blue"><b><%# Eval("REMARKS") %></b></span></span></td>--%>
                                                                                            <span>Councellor: <b><%# Eval("UA_NAME") %> </b><%# Eval("OLD_LEAD") %> <%# Eval("NEW_LEAD") %> <%# Eval("OLD_REMARK") %> <%# Eval("NEW_REMARK") %> <%# Eval("NEXTFOLLOUP_DATE") %></span>
                                                                                        </td>
                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                            </asp:ListView>

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>

                                                </div>

                                                <div id="divEmoji" runat="server" visible="false" role="tabpanel" aria-labelledby="Grade-tab">
                                                    <div>
                                                        <asp:UpdateProgress ID="UpdateProgress8" runat="server" AssociatedUpdatePanelID="updGrades"
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

                                                    <asp:UpdatePanel ID="updGrades" runat="server">
                                                        <ContentTemplate>
                                                            <div class="row">
                                                                <div class="col-md-12 col-sm-12 col-12">
                                                                    <div class="sub-heading mt-3">
                                                                        <h5>Application Details</h5>
                                                                    </div>


                                                                    <div class="col-12 pl-0 pr-0">
                                                                        <asp:Panel ID="Panel5" runat="server">
                                                                            <asp:ListView ID="lvDetails" runat="server">
                                                                                <LayoutTemplate>
                                                                                    <div class="table table-responsive">
                                                                                        <table class="table table-striped table-bordered nowrap" style="width: 100%">
                                                                                            <thead>
                                                                                                <tr class="bg-light-blue">
                                                                                                    <%-- <th style="text-align: center;">Action </th>--%>
                                                                                                    <th>Study Level</th>
                                                                                                    <th>Study Interest</th>
                                                                                                    <th>Program</th>
                                                                                                    <th>Payment Status</th>
                                                                                                    <th>Date</th>
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
                                                                                        <td><%# Eval("STUDY_LEVEL") %> </td>
                                                                                        <td><%# Eval("STUDY_INTEREST") %> </td>
                                                                                        <td><%# Eval("PROGRAM") %> </td>
                                                                                        <td><%# Eval("PAYMENT_STATUS") %> </td>
                                                                                        <td><%# Eval("REGDATE") %> </td>
                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                            </asp:ListView>


                                                                        </asp:Panel>
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
                            </div>


                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <div class="modal fade" id="myModal1" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content chat-app">
                <div class="modal-header">
                    <h4 class="modal-title"><b>Reply For Query</b> <%--<i class="fa fa-question-circle"></i>--%></h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <%-- <div class="modal-body" style="background-color:floralwhite">--%>
                        <div class="modal-body" style="background-color: floralwhite; height: 200px; overflow-x: auto;">
                            <div>
                                <asp:UpdateProgress ID="UpdateProgress5" runat="server" AssociatedUpdatePanelID="UpdatePanel3"
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
                            <div>
                                <asp:ListView ID="lvFeedbackReply" runat="server">
                                    <LayoutTemplate>
                                        <div id="listViewGrid">
                                            <div id="tblStudents">
                                                <div id="itemPlaceholder" runat="server" />
                                            </div>
                                        </div>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblUserReply" runat="server" Text='<%# Eval("FEEDBACK_DETAILS")%>' CssClass="chat-reply user-reply" />
                                        <asp:Label ID="lblAdminReply" runat="server" Text='<%# Eval("FEEDBACK_REPLY")%>' CssClass="chat-reply admin-reply" />
                                        <asp:HiddenField ID="hdfquery" runat="server" Value='<%# Eval("QUERYNO")%>' />
                                    </ItemTemplate>
                                </asp:ListView>

                            </div>
                        </div>

                        <div class="modal-footer">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-9 col-md-9 col-12 chat-message">
                                        <div class="label-dynamic">
                                        </div>
                                        <asp:TextBox ID="txtFeedback" runat="server" TextMode="MultiLine" CssClass="form-control" placeholder="Type your message here..."></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFeedback"
                                            ErrorMessage="Please Enter Your Answer" Display="None" ValidationGroup="submit"></asp:RequiredFieldValidator>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-3 col-12">
                                        <div class="label-dynamic">
                                        </div>
                                        <asp:DropDownList ID="ddlStatus" CssClass="form-control" data-select2-enable="true" runat="server">
                                            <asp:ListItem Selected="True" Value="1">Open</asp:ListItem>
                                            <asp:ListItem Value="2">Close</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Send" CssClass="btn btn-outline-info"
                                    ValidationGroup="submit" OnClick="btnSubmit_Click" />
                                <asp:Button ID="Button1" runat="server" Text="Cancel" CssClass="btn btn-outline-danger" data-dismiss="modal" OnClick="btnCancel_Click" />
                            </div>
                            <div class="col-12 btn-footer">
                                <strong>
                                    <asp:Label ID="lblStatus1" runat="server"></asp:Label>
                                </strong>
                            </div>

                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" Height="38px" ShowMessageBox="True"
                                ShowSummary="False" ValidationGroup="submit" DisplayMode="List" />

                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <!-- The Modal -->
    <div class="modal fade" id="confirm">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">

                <!-- Modal Header -->
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>

                <!-- Modal body -->
                <div class="modal-body text-center">
                    <asp:Label ID="lbluserMsg" runat="server" Visible="true"></asp:Label>
                    Account already exist
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="ModelEmailPopup">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">

                <!-- Modal Header -->
                <div class="modal-header">
                    <h4 class="modal-title">Send Mail</h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div>
                    <asp:UpdateProgress ID="UpdateProgress19" runat="server" AssociatedUpdatePanelID="updEmailSend"
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
                <asp:UpdatePanel ID="updEmailSend" runat="server">
                    <ContentTemplate>
                        <!-- Modal body -->
                        <div class="modal-body">

                            <asp:RadioButtonList ID="rbtodayselect" runat="server"
                                RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rbtodayselect_SelectedIndexChanged">
                                <asp:ListItem Value="1">Mannual &nbsp;&nbsp;&nbsp;&nbsp;
                                </asp:ListItem>
                                <asp:ListItem Value="2">Automatic  &nbsp;&nbsp;&nbsp;&nbsp;                               
                                </asp:ListItem>

                            </asp:RadioButtonList>

                            <div id="todaymau" runat="server" visible="false">
                                <div class="form-group col-lg-12 col-md-12 col-12">
                                    <label>Subject</label>
                                    <asp:TextBox ID="txtEmailSubject" runat="server" CssClass="form-control">
                                    </asp:TextBox>
                                    <asp:HiddenField ID="hdfFilename" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" SetFocusOnError="True"
                                        ErrorMessage="Please Enter Subject" ControlToValidate="txtEmailSubject" Display="None"
                                        ValidationGroup="EmailSubmit" />
                                </div>
                                <div class="form-group col-lg-12 col-md-12 col-12">
                                    <label>Message</label>
                                    <asp:TextBox ID="txtEmailMessage" runat="server" CssClass="form-control" TextMode="MultiLine" MaxLength="350">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" SetFocusOnError="True"
                                        ErrorMessage="Please Enter Message" ControlToValidate="txtEmailMessage" Display="None"
                                        ValidationGroup="EmailSubmit" />
                                </div>
                                <div class="form-group col-lg-12 col-md-12 col-12">
                                    <label>Add Attachment</label>
                                    <asp:FileUpload ID="fuAttachFile" runat="server" CssClass="form-control" />
                                </div>
                            </div>
                            <div id="todayauto" runat="server" visible="false">
                                <asp:RadioButtonList ID="rbtodaytemplate" runat="server"
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">Application Started &nbsp;&nbsp;&nbsp;&nbsp;
                                    </asp:ListItem>
                                    <asp:ListItem Value="2">Application Started but Payment Pending  &nbsp;&nbsp;&nbsp;&nbsp;                               
                                    </asp:ListItem>

                                </asp:RadioButtonList>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSendBulkEmail" runat="server" Visible="false" Text="Send Email" CssClass="btn btn-outline-info jqbtn" ValidationGroup="EmailSubmit" OnClick="btnSendBulkEmail_Click" />
                                <asp:ValidationSummary ID="emailsummary" runat="server" ValidationGroup="EmailSubmit" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" />
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnSendBulkEmail" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <div class="modal fade beauty" id="myModalApp" role="dialog">
        <asp:UpdatePanel ID="updLeadStatus" runat="server">
            <ContentTemplate>
                <div class="modal-dialog modal-xl">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">Lead Status</h4>
                            <button type="button" class="close" data-dismiss="modal" title="Close">&times;</button>
                        </div>
                        <div class="modal-body clearfix">
                            <div class="row">
                                <div class="form-group col-lg-6 col-md-6 col-12">
                                    <label>Lead Status </label>
                                    <%-- <asp:DropDownList ID="ddlLeadStatus" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>--%>

                                    <asp:DropDownList ID="ddlLeadStatus" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:HiddenField ID="hdnEnqueryno" runat="server" />
                                    <%--  <asp:HiddenField ID="" runat="server" />--%>
                                </div>

                                <div class="form-group col-lg-6 col-md-6 col-12">
                                    <label>Remark </label>
                                    <asp:TextBox ID="txt_Remark" runat="server" TabIndex="9" CssClass="form-control" placeholder="Enter Remark" TextMode="MultiLine"></asp:TextBox>

                                    <ajaxToolKit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server"
                                        TargetControlID="txt_Remark" WatermarkText="Enter Remark" />
                                </div>
                                <div class="form-group col-lg-6 col-md-6 col-12">
                                    <div class="label-dynamic">

                                        <label>Next Followup date</label>
                                    </div>

                                    <asp:TextBox ID="txtEndDate" runat="server" TabIndex="4"
                                        ToolTip="Please Enter Next Followup Date" CssClass="form-control" />
                                    <i class="fa fa-calendar input-prefix" aria-hidden="true" style="float: right; margin-top: -23px; margin-right: 10px;"></i>
                                    <asp:RequiredFieldValidator ID="rfvEndDate" runat="server" SetFocusOnError="True"
                                        ErrorMessage="Please Enter Next Followup Date" ControlToValidate="txtEndDate" Display="None"
                                        ValidationGroup="ubmit" />

                                </div>

                                <div class="col-12 btn-footer">
                                    <asp:Button ID="btn_SubmitModal" runat="server" TabIndex="11" Text="Submit" CssClass="btn btn-outline-primary" OnClick="btn_SubmitModal_Click" />
                                    <asp:Button ID="btn_Cancel" runat="server" TabIndex="12" Text="Clear" CssClass="btn btn-outline-danger" OnClick="btn_Cancel_Click" />
                                </div>

                            </div>
                            <div class="modal-footer">
                                <div class="col-12">
                                    <asp:Panel ID="pnlSession" runat="server">
                                        <asp:ListView ID="lvLeadList" runat="server">
                                            <LayoutTemplate>
                                                <div class="sub-heading">
                                                    <h5>Application Status List</h5>
                                                </div>
                                                <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th style="text-align: center;">Action </th>
                                                            <th>Lead Stage</th>
                                                            <th>Remark</th>
                                                            <th>Date</th>
                                                            <th>Next Folloup Date</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                                </table>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="text-align: center;">
                                                        <%--<asp:ImageButton ID="btnDel" runat="server" AlternateText="Delete Record" 
                                                        CommandArgument='<%# Eval("ENQUIRYNO") %>' ImageUrl="~/IMAGES/delete.gif" 
                                                        OnClick="btnDel_Click" OnClientClick="return UserDeleteConfirmation();" TabIndex="6" 
                                                        ToolTip='<%# Eval("ENQUIRYNO") %>' /> --%>

                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.gif"
                                                            AlternateText="Edit Record" ToolTip='<%# Eval("ENQUIRYNO") %>' CommandArgument='<%# Eval("ENQUIRYNO") %>' OnClick="btnEdit_Click" />

                                                    </td>
                                                    <td><%# Eval("LEAD_STAGE_NAME") %></td>
                                                    <td><%# Eval("REMARKS") %></td>
                                                    <td><%# Eval("UPD_ENQUIRYSTATUS_DATE") %></td>
                                                    <td><%# Eval("NEXTDATE") %></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>

                                <asp:ValidationSummary ID="ValidationSummary4" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="login1" DisplayMode="List" />
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_Cancel" />
                <asp:AsyncPostBackTrigger ControlID="btn_SubmitModal" />
                <asp:AsyncPostBackTrigger ControlID="lvLeadList" />
                <%-- <asp:PostBackTrigger ControlID="btn_Cancel" />--%>
            </Triggers>
        </asp:UpdatePanel>
    </div>


    <div class="modal fade" id="divCamparision">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">

                <!-- Modal Header -->
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>

                <!-- Modal body -->
                <div class="modal-body clearfix">
                    <div class="row">
                        <div class="form-group col-lg-12 col-md-12 col-12">
                            <label>Please Enter Password </label>
                            <asp:TextBox ID="TxtpassCamparision" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>

                    </div>
                    <div class="col-12 btn-footer">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="btncmpareexcel" runat="server" Text="Lead Camparision Excel" OnClick="btncmpareexcel_Click" CssClass="btn btn-outline-info" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btncmpareexcel" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div class="modal fade" id="divExcelVerify">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">

                <!-- Modal Header -->
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>

                <!-- Modal body -->
                <div class="modal-body clearfix">
                    <div class="row">
                        <div class="form-group col-lg-12 col-md-12 col-12">
                            <label>Please Enter Password </label>
                            <asp:TextBox ID="txtVerifyPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                        </div>

                    </div>
                    <div class="col-12 btn-footer">
                        <asp:UpdatePanel ID="updPassword" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="btnGenerateExcel" runat="server" Text="Excel" OnClick="btnGenerateExcel_Click" CssClass="btn btn-outline-info" ValidationGroup="btnShowFilter" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnGenerateExcel" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".jqbtn").click(function () {
                $("[id*=btnSendBulkEmail]").click();
            });
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#btnBulkEmail").click(function () {
                //alert('hii')
                $("#ModelEmailPopup").modal();

            });
        });

        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {

                $("#btnBulkEmail").click(function () {
                    // alert('bye')
                    $("#ModelEmailPopup").modal();
                });
            });
        });
    </script>

    <%--<script type="text/javascript">
        document.onreadystatechange = function () {
            var state = document.readyState
            if (state == 'interactive') {
                document.getElementById('contents').style.visibility = "hidden";
            } else if (state == 'complete') {
                setTimeout(function () {
                    document.getElementById('interactive');
                    document.getElementById('load').style.visibility = "hidden";
                    document.getElementById('contents').style.visibility = "visible";
                }, 1000);
            }
        }
    </script>--%>

    <script type="text/javascript">

        function topFunction() {
            document.body.scrollTop = 0;
            document.documentElement.scrollTop = 0;

        }
    </script>


    <script type="text/javascript">
        function showPopup() {
            $('#myModal1').modal('show');
            $('#myModal1').fadeIn();
        }
    </script>

    <%-- ENDED BY Aashna 28-10-2021 --%>
    <script>
        $(document).ready(function () {
            "use strict";
            $('.label.ui.dropdown')
                .dropdown();
            $('.no.label.ui.dropdown')
                .dropdown({
                    useLabels: false
                });
            $('.ui.button').on('click', function () {
                $('.ui.dropdown')
                    .dropdown('restore defaults')
            })
        });
        var prm1 = Sys.WebForms.PageRequestManager.getInstance();
        prm1.add_endRequest(function () {
            $(document).ready(function () {
                "use strict";
                $('.label.ui.dropdown')
                    .dropdown();
                $('.no.label.ui.dropdown')
                    .dropdown({
                        useLabels: false
                    });
                $('.ui.button').on('click', function () {
                    $('.ui.dropdown')
                        .dropdown('restore defaults')
                })
            });
        });
    </script>
    <script>
        //$("#close-sidebar").click(function() {
        //  $(".page-wrapper").toggleClass("toggled");
        //});
        $("#showright-sidebar").click(function () {
            // alert('hi');
            $(".pageright-wrapper").toggleClass("toggleed");
            // $(".btnsidebar").toggleClass('rotated')

        });
        var prm2 = Sys.WebForms.PageRequestManager.getInstance();
        prm2.add_endRequest(function () {
            $("#showright-sidebar").click(function () {
                // alert('hi');
                $(".pageright-wrapper").toggleClass("toggleed");
                //  $(".btnsidebar").toggleClass('rotated')
            });
        });
        //   $(".filter-text").hide();
        $("#filter-toggle").click(function () {
            $(".filter-text").toggle();
            $(".input-filter").addClass('inputfilter')

        });
        var prm3 = Sys.WebForms.PageRequestManager.getInstance();
        prm3.add_endRequest(function () {
            $("#filter-toggle").click(function () {
                $(".filter-text").toggle();
                $(".input-filter").addClass('inputfilter')

            });
        });

    </script>

    <script type="text/javascript">
        var prm3 = Sys.WebForms.PageRequestManager.getInstance();
        prm3.add_endRequest(function () {
            function SelectAll(chk) {
                var tbl = document.getElementsByClassName("testdata");
                if (tbl != null && tbl.childNodes != null) {
                    for (i = 0; i < tbl.getElementsByTagName("tr").length; i++) {
                        if (i < 10) {
                            document.getElementById('ctl00_ContentPlaceHolder1_lvLeadDetails_ctrl' + i + '_chkCheck').checked = chk.checked;
                        }
                        else {
                            document.getElementById('ctl00_ContentPlaceHolder1_lvLeadDetails_ctrl' + i + '_chkCheck').checked = chk.checked;
                        }
                    }
                }
            }
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.datePicker').daterangepicker({
                startDate: moment().subtract(00, 'days'),
                endDate: moment(),
                //also comment "range" in daterangepicker.js('<div class="ranges"></div>' +)
                ranges: {

                },
                //<!-- ========= Disable dates after today ========== -->
                //maxDate: new Date(),
                //<!-- ========= Disable dates after today END ========== -->
            },
                function (start, end) {
                    $('.PickerDate').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
                });

            $('.PickerDate').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))
        });

        var prm4 = Sys.WebForms.PageRequestManager.getInstance();
        prm4.add_endRequest(function () {
            $(document).ready(function () {
                $('.datePicker').daterangepicker({
                    startDate: moment().subtract(00, 'days'),
                    endDate: moment(),
                    //also comment "range" in daterangepicker.js('<div class="ranges"></div>' +)
                    ranges: {

                    },
                    //<!-- ========= Disable dates after today ========== -->
                    //maxDate: new Date(),
                    //<!-- ========= Disable dates after today END ========== -->
                },
                    function (start, end) {
                        $('.PickerDate').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))


                    });

                $('.PickerDate').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'))


            });
        });
    </script>
    <%--  <script>

        function CmbChange(obj) {
            var cmbValue = document.getElementById("slMainLeadLabel").value;
            __doPostBack('slMainLeadLabel', cmbValue);
        }

    </script>--%>
    <script>
        $(document).ready(function () {
            var check_panel_state = $('#ctl00_ContentPlaceHolder1_ddlMainLeadLabel option[selected="selected"]').length;
            if (check_panel_state > 0) {
                $(".pageright-wrapper").addClass("toggleed");
            }
            else {
                $(".pageright-wrapper").removeClass("toggleed");
            }
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                var check_panel_state = $('#ctl00_ContentPlaceHolder1_ddlMainLeadLabel option[selected="selected"]').length;
                if (check_panel_state > 0) {
                    $(".pageright-wrapper").addClass("toggleed");
                }
                else {
                    $(".pageright-wrapper").removeClass("toggleed");
                }
            });
        });
    </script>

    <script type="text/javascript">
        // function CountCharacters11() {
        function checkLength(el) {
            if (el.value.length >= 40) {
                alert("Maximum Charectures Length is 40 Charecters");
            }
        }
    </script>
    <script>
        $(document).ready(function () {
            var dateval = document.getElementById('<%=txtDob.ClientID%>').value;
            var prev_date = new Date();
            prev_date.setDate(prev_date.getDate() - 1);
            $('.dob').daterangepicker({
                singleDatePicker: true,
                showDropdowns: true,
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
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                var dateval = document.getElementById('<%=txtDob.ClientID%>').value;
                var prev_date = new Date();
                prev_date.setDate(prev_date.getDate() - 1);
                $('.dob').daterangepicker({
                    singleDatePicker: true,
                    showDropdowns: true,
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
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
        });
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
        });

    </script>
    <script>
        $(document).ready(function () {
            $('.select2').select2({
                dropdownAutoWidth: true,
                width: '100%',
                //allowClear: true
            })
        });

        $(document).ready(function () {
            $(document).on("click", ".select2-search-clear-icon", function () {
                var sel2id = localStorage.getItem("sel2id");
                $('#' + sel2id).select2('close');
                $('#' + sel2id).select2('open');
            });

            var parameter = Sys.WebForms.PageRequestManager.getInstance();
            parameter.add_endRequest(function () {
                $(document).ready(function () {
                    $('.select2').select2({
                        dropdownAutoWidth: true,
                        width: '100%',
                        //allowClear: true
                    })
                });
            });
            var parameter = Sys.WebForms.PageRequestManager.getInstance();
            parameter.add_endRequest(function () {
                $(document).on('click', '.select2', function () {
                    debugger
                    var key = $(this).parent().find('.select-clik').attr('id');
                    localStorage.setItem("sel2id", key);
                });
            });
        });
    </script>
    <script>
        function RemoveCountryName() {

            $("#select2-ddlMobileCode-container").html($("#select2-ddlMobileCode-container").html().split('-')[0]);
            if ($("#ddlMobileCode").val().split('-')[0] != "212") {
                $("#txtMobileNo").val('');
            }
            else {
                $("#txtMobileNo").val('0');
            }
        }
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $("#ddlMobileCode").html($(".select2-selection__rendered").html().split('-')[0]);
                // alert('byee')
            });
        });
    </script>
    <script type="text/javascript">
        function Validator() {
            var pass = $('#txtPassport').val();
            var nic = $('#txtNIC').val();
            if (pass == '' && nic == '') {
                alert("Passport No. OR NIC(National Identity card) is Required !");
            }
        }
    </script>
    <script>
        function RemoveHomeCountryName() {

            $("#select2-ddlHomeTelMobileCode-container").html($("#select2-ddlHomeTelMobileCode-container").html().split('-')[0]);
            //if ($("#ddlHomeTelMobileCode").val().split('-')[0] != "212") {
            //    $("#txtHomeMobileNo").val('');
            //}
            //else {
            //    $("#txtHomeMobileNo").val('0');
            //}
        }
        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $("#ddlHomeTelMobileCode").html($(".select2-selection__rendered").html().split('-')[0]);
                // alert('byee')
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
        $(".saveAsExcel2").click(function () {
            //alert("triggered");
            var table = document.querySelector('#MainLeadTable2');
            var workbook = XLSX.utils.book_new();
            var allDataArray = makeTableArray(table);
            var worksheet = XLSX.utils.aoa_to_sheet(allDataArray);
            workbook.SheetNames.push("Test");
            workbook.Sheets["Test"] = worksheet;
            XLSX.writeFile(workbook, "LeadData.xlsx");
        });

        function makeTableArray(table) {
            var allTableRows = table.querySelectorAll('tr');
            var array = [];

            allTableRows.forEach(row => {
                var rowArray = [];

            if (row.querySelector('td')) {
                var allTds = row.querySelectorAll('td');
                allTds.forEach(td => {
                    if (td.querySelector('span')) {
                        rowArray.push(td.querySelector('span').textContent);
            }
            else if (td.querySelector('input')) {
                rowArray.push(td.querySelector('input').value);
            }
            else if (td.querySelector('select')) {
                rowArray.push(td.querySelector('select').value);
            }
            else if (td.innerText) {
                rowArray.push(td.innerText);
            }
            else {
                rowArray.push('');
            }
        });
        }

        if (row.querySelector('th')) {
            var allThs = row.querySelectorAll('th');
            allThs.forEach(th => {
                if (th.textContent) {
                    rowArray.push(th.textContent);
        } else {
            rowArray.push('');
        }
        });
        }

        array.push(rowArray);
        });

        return array;
        }

        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () { 
            $(".saveAsExcel2").click(function () {
               // alert("triggered");
                var table = document.querySelector('#MainLeadTable2');
                var workbook = XLSX.utils.book_new();
                var allDataArray = makeTableArray(table);
                var worksheet = XLSX.utils.aoa_to_sheet(allDataArray);
                workbook.SheetNames.push("Test");
                workbook.Sheets["Test"] = worksheet;
                XLSX.writeFile(workbook, "LeadData.xlsx");
            });

            function makeTableArray(table) {
                var allTableRows = table.querySelectorAll('tr');
                var array = [];

                allTableRows.forEach(row => {
                    var rowArray = [];

                if (row.querySelector('td')) {
                    var allTds = row.querySelectorAll('td');
                    allTds.forEach(td => {
                        if (td.querySelector('span')) {
                            rowArray.push(td.querySelector('span').textContent);
                }
                else if (td.querySelector('input')) {
                    rowArray.push(td.querySelector('input').value);
                }
                else if (td.querySelector('select')) {
                    rowArray.push(td.querySelector('select').value);
                }
                else if (td.innerText) {
                    rowArray.push(td.innerText);
                }
                else {
                    rowArray.push('');
                }
            });
        }

        if (row.querySelector('th')) {
            var allThs = row.querySelectorAll('th');
            allThs.forEach(th => {
                if (th.textContent) {
                    rowArray.push(th.textContent);
            } else {
                rowArray.push('');
            }
            });
            }

            array.push(rowArray);
            });

            return array;
            }
        });
    </script>

    <script>
        $(".saveAsExcel").click(function () {
            //alert("triggered");
            var table = document.querySelector('#MainLeadTable');
            var workbook = XLSX.utils.book_new();
            var allDataArray = makeTableArray(table);
            var worksheet = XLSX.utils.aoa_to_sheet(allDataArray);
            workbook.SheetNames.push("Test");
            workbook.Sheets["Test"] = worksheet;
            XLSX.writeFile(workbook, "LeadData.xlsx");
        });

        function makeTableArray(table) {
            var allTableRows = table.querySelectorAll('tr');
            var array = [];

            allTableRows.forEach(row => {
                var rowArray = [];

            if (row.querySelector('td')) {
                var allTds = row.querySelectorAll('td');
                allTds.forEach(td => {
                    if (td.querySelector('span')) {
                        rowArray.push(td.querySelector('span').textContent);
            }
            else if (td.querySelector('input')) {
                rowArray.push(td.querySelector('input').value);
            }
            else if (td.querySelector('select')) {
                rowArray.push(td.querySelector('select').value);
            }
            else if (td.innerText) {
                rowArray.push(td.innerText);
            }
            else {
                rowArray.push('');
            }
        });
        }

        if (row.querySelector('th')) {
            var allThs = row.querySelectorAll('th');
            allThs.forEach(th => {
                if (th.textContent) {
                    rowArray.push(th.textContent);
        } else {
            rowArray.push('');
        }
        });
        }

        array.push(rowArray);
        });

        return array;
        }

        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () { 
            $(".saveAsExcel").click(function () {
                //alert("triggered");
                var table = document.querySelector('#MainLeadTable');
                var workbook = XLSX.utils.book_new();
                var allDataArray = makeTableArray(table);
                var worksheet = XLSX.utils.aoa_to_sheet(allDataArray);
                workbook.SheetNames.push("Test");
                workbook.Sheets["Test"] = worksheet;
                XLSX.writeFile(workbook, "LeadData.xlsx");
            });

            function makeTableArray(table) {
                var allTableRows = table.querySelectorAll('tr');
                var array = [];

                allTableRows.forEach(row => {
                    var rowArray = [];

                if (row.querySelector('td')) {
                    var allTds = row.querySelectorAll('td');
                    allTds.forEach(td => {
                        if (td.querySelector('span')) {
                            rowArray.push(td.querySelector('span').textContent);
                }
                else if (td.querySelector('input')) {
                    rowArray.push(td.querySelector('input').value);
                }
                else if (td.querySelector('select')) {
                    rowArray.push(td.querySelector('select').value);
                }
                else if (td.innerText) {
                    rowArray.push(td.innerText);
                }
                else {
                    rowArray.push('');
                }
            });
        }

        if (row.querySelector('th')) {
            var allThs = row.querySelectorAll('th');
        allThs.forEach(th => {
            if (th.textContent) {
                rowArray.push(th.textContent);
        } else {
            rowArray.push('');
        }
        });
        }

        array.push(rowArray);
        });

        return array;
        }
        });
    </script>

    <script type="text/javascript">
        function getCurrentYear() {
            var cDate = new Date();
            return cDate.getFullYear();
        }

        function CheckDate(sender, args) {
            if (sender._selectedDate > new Date()) {
                sender._selectedDate = new Date();
                alert("Future Date Not Accepted!");
                document.getElementById('ctl00_ContentPlaceHolder1_Txtfrommisc').value = '';
                document.getElementById('ctl00_ContentPlaceHolder1_Txttomisc').value = '';
                document.getElementById('ctl00_ContentPlaceHolder1_txtFromDate').value = '';
                document.getElementById('ctl00_ContentPlaceHolder1_txtToDate').value = '';

                //var startDate = new Date($('#Txtfrommisc').val());
                //var endDate = new Date($('#Txttomisc').val());

                //if (startDate < endDate) {
                //    alert("To date Should be Greater than From date!");
                //    endDate.value = '';
                //    endDate.value = '';
                //}
            }
        }

    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#picker').daterangepicker({
                startDate: moment().subtract(00, 'days'),
                endDate: moment(),
                locale: {
                    format: 'DD MMM, YYYY'
                },
                ranges: {
                },
            },
                function (start, end) {
                    $('#date').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'));
                    document.getElementById('<%=hdnDate.ClientID%>').value = (start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
                });

            //$('#date').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'));
        });
    </script>
    <script>
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(document).ready(function () {
                $('#picker').daterangepicker({
                    startDate: moment().subtract(00, 'days'),
                    endDate: moment(),
                    locale: {
                        format: 'DD MMM, YYYY'
                    },
                    ranges: {
                    },
                },
                    function (start, end) {
                        $('#date').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'));
                        document.getElementById('<%=hdnDate.ClientID%>').value = (start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
                    });

                //$('#date').html(moment().subtract(00, 'days').format('DD MMM, YYYY') + ' - ' + moment().format('DD MMM, YYYY'));
            });
        });
    </script>
    <script>
        function Setdate(date) {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $(document).ready(function () {
                    debugger;
                    var startDate = moment(date.split('-')[0], "DD MMM, YYYY");
                    var endtDate = moment(date.split('-')[1], "DD MMM, YYYY");
                    //$('#date').html(date);
                    $('#date').html(startDate.format("DD MMM, YYYY") + ' - ' + endtDate.format("DD MMM, YYYY"));
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
                            $('#date').html(start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
                            document.getElementById('<%=hdnDate.ClientID%>').value = (start.format('DD MMM, YYYY') + ' - ' + end.format('DD MMM, YYYY'))
                        });

                });
            });
            };
    </script>
</asp:Content>
