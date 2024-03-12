<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="FeesDetails.aspx.cs" Inherits="ACADEMIC_FeesRefundReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../css/ImageViewer.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="../css/jquery.verySimpleImageViewer.css">
    <script src="../js/jquery.verySimpleImageViewer.js"></script>

    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <link href='<%=Page.ResolveClientUrl("~/css/jquery-ui.css")%>' rel="stylesheet" />



    <%--<script type="text/javascript" charset="utf-8">
        $(document).ready(function () {

            $(".display").dataTable({
                "bJQueryUI": true,
                "sPaginationType": "full_numbers"
            });

        });
    </script>--%>
    <style>
        .badge {
            display: inline-block;
            padding: 5px 10px 7px;
            border-radius: 15px;
            font-size: 100%;
            width: 80px;
        }

        .badge-warning {
            color: #fff !important;
        }

        td .fa-eye {
            font-size: 18px;
            color: #0d70fd;
        }
    </style>
    <script lang="javascript" type="text/javascript">

        $("[src*=plus]").on("click", function () {
            
            $(this).attr("src", "../images/minus.png");
            $(this).closest("tr").after("<tr><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
            $(this).closest("tr").next().hide().fadeIn("slow");

        });

        $("[src*=minus]").on("click", function () {
            $(this).attr("src", "../images/plus.png");

            //        $(this).closest("tr").next().remove();
            $(this).closest("tr").next().fadeOut("slow");

        });
    </script>

    <div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updFeesDetails"
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
    <asp:UpdatePanel ID="updFeesDetails" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                            <asp:HiddenField ID="hdnClientId" runat="server" Value="0" />
                            <%--<h3 class="box-title">Search Student</h3>--%>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12"  runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Enter Student ID/Registration ID</label>
                                        </div>
                                        <div class="input-group" >
                                            <asp:TextBox ID="txtEnrollNo" runat="server" ValidationGroup="submit" ToolTip="Enter Student ID/Registration ID" CssClass="form-control" placeholder="Enter Student ID/Registration ID"></asp:TextBox>
                                            <span class="input-group-addon">
                                                <a href="#" title="Search Student for Fee Payment" data-toggle="modal" data-target="#myModal4">
                                                    <i class="fa fa-search" aria-hidden="true"></i>
                                                </a>
                                            </span>
                                        </div>
                                    </div>
                                    <div class="col-lg-5 col-md-6 col-12">
                                        <div class="form-group">
                                            <div class="label-dynamic">
                                                <sup>* </sup>
                                                <label>Search Student </label>
                                            </div>
                                            <input type="text" class="form-control search" tabindex="0" id="txtSearchStudent" />

                                        </div>
                                    </div>
                                    <div class="form-group col-lg-2 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <label></label>
                                        </div>
                                       <%-- <input type="button" class="btn btn-sm btn-outline-primary" tabindex="0" value="Search" id="btnSearchDY" />--%>
                                        <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-outline-info m-top" OnClick="btnShow_Click" value="Search"
                                            ValidationGroup="studSearch" />
                                        <%--  Shrink the info panel out of view --%>
                                    </div>
                                    <%--<div class="col-md-1" style="margin-left: -10px; margin-top: 5px">
                                                <a href="#" title="Search Student for Fee Payment" data-toggle="modal" data-target="#myModal4"
                                                    style="background-color: White">
                                                    <asp:Image ID="imgSearch" runat="server" ImageUrl="~/images/search.png" TabIndex="3" />
                                                </a>
                                            </div>--%>
                                </div>
                            </div>
                            <asp:RequiredFieldValidator ID="valEnrollNo" runat="server" ControlToValidate="txtEnrollNo"
                                Display="None" ErrorMessage="Please Enter Student ID/Registration ID." SetFocusOnError="true"
                                ValidationGroup="studSearch" />
                            <asp:ValidationSummary ID="valSummary3" runat="server" DisplayMode="List" ShowMessageBox="true"
                                ShowSummary="false" ValidationGroup="studSearch" />
                            <!-- Modal -->

                            <div id="myModal4" class="modal fade" role="dialog">
                                <div class="modal-dialog modal-lg">
                                    <!-- Modal content-->
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h4 class="modal-title">Student Search</h4>
                                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                                        </div>
                                        <div class="modal-body">
                                            <asp:UpdatePanel ID="updEdit" runat="server">
                                                <ContentTemplate>
                                                    <div class="row">
                                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                                            <div class="label-dynamic">
                                                                <label>Search By</label>
                                                            </div>
                                                            <%-- <asp:RadioButtonList ID="rdselect" runat="server" RepeatDirection="Horizontal">
                                                                        <asp:ListItem Value="0">Name</asp:ListItem>
                                                                        <%--  <asp:ListItem Value="1">IdNo</asp:ListItem>--%>
                                                            <%--<asp:ListItem Value="1">Application No</asp:ListItem>
                                                                    </asp:RadioButtonList>--%>
                                                            <asp:RadioButton ID="rdoRegNo" runat="server" Checked="true" Text="Student ID."
                                                                GroupName="edit" />&nbsp;&nbsp;
                                                                     <asp:RadioButton ID="rdoTan" runat="server" Text="Registration ID" GroupName="edit" />&nbsp;&nbsp;                               
                                                                     <asp:RadioButton ID="rdoEnrollmentNo" runat="server" Text="Emailid" GroupName="edit" />&nbsp;&nbsp; 
                                                                     <asp:RadioButton ID="rdoNic" runat="server" Text="Nic" GroupName="edit" />&nbsp;&nbsp; 
                                                                    <asp:RadioButton ID="rdoName" runat="server" Text="Name" GroupName="edit" />&nbsp;&nbsp;  
                                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please select Category"
                                                                        ControlToValidate="rdselect" Display="None" ValidationGroup="search"></asp:RequiredFieldValidator>--%>
                                                        </div>
                                                        <div class="form-group col-lg-6 col-md-6 col-12">
                                                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-3" id="Div1" runat="server" visible="false">
                                                            <label>College</label>
                                                            <asp:DropDownList ID="ddlCollege" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" />
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" id="sfsdf" runat="server" visible="false">
                                                            <div class="label-dynamic">
                                                                <label>Degree</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlDegree" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged" AutoPostBack="true" />
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" style="display: none;">
                                                            <div class="label-dynamic">
                                                                <label>Specialization</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlBranch" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true">
                                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" style="display: none;">
                                                            <div class="label-dynamic">
                                                                <label>Year</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlYear" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true" />
                                                        </div>
                                                        <div class="form-group col-lg-3 col-md-6 col-12" style="display: none;">
                                                            <div class="label-dynamic">
                                                                <label>Semester</label>
                                                            </div>
                                                            <asp:DropDownList ID="ddlSem" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true" />
                                                        </div>
                                                        <div class="col-12 btn-footer">

                                                            <%-- <asp:Button ID="btnsearch" runat="server" OnClick="btnsearch_Click" CssClass="btn btn-outline-info" Text="Search" />
                                                                    <asp:Button ID="btnclear"  runat="server" OnClick="btnclear_Click" CssClass="btn btn-outline-danger" Text="Cancel"/>--%>
                                                            <input id="btnSearch" type="button" value="Search" onclick="SubmitSearch(this.id);" class="btn btn-outline-info" />
                                                            <%--   <input id="btnClear" type="button" value="Clear Text" class="btn btn-outline-danger"
                                                                onclick="return ClearSearchBox(this.name)" />--%>
                                                            <asp:Button runat="server" ID="btnClose" class="btn btn-danger" Text="Close" OnClick="btnClose_Click" />
                                                            <%--<button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>--%>
                                                        </div>
                                                        <div>
                                                            <asp:Label ID="lblNoRecords" runat="server" SkinID="lblmsg" />
                                                        </div>
                                                    </div>

                                                    <div class="col-12">
                                                        <asp:Panel ID="pnllist" runat="server" CssClass="table-responsive">
                                                            <asp:ListView ID="lvStudent" runat="server">
                                                                <LayoutTemplate>
                                                                    <div>
                                                                        <div class="sub-heading">
                                                                            <h5>Search Results</h5>
                                                                        </div>
                                                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="myTable1">
                                                                            <thead class="bg-light-blue">
                                                                                <tr>
                                                                                    <th>Name
                                                                                    </th>
                                                                                    <th>Username
                                                                                    </th>
                                                                                    <%-- <th>Identity.No
                                                                                    </th>--%>
                                                                                    <th>Degree
                                                                                    </th>
                                                                                    <th>Specialization
                                                                                    </th>
                                                                                    <th>Year
                                                                                    </th>
                                                                                    <th>Semester
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
                                                                            <asp:LinkButton ID="lnkId" runat="server" Text='<%# Eval("NAME") %>'
                                                                                CommandName='<%# Eval("USERNO") %>' CommandArgument='<%# Eval("IDNO") %>'
                                                                                OnClick="lnkId_Click"></asp:LinkButton>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("ENROLLNO")%>
                                                                        </td>
                                                                        <%--<td>
                                                                            <%# Eval("IDNO")%>
                                                                        </td>--%>
                                                                        <td>
                                                                            <%# Eval("CODE")%>
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
                                                                    </tr>

                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </asp:Panel>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row" id="divstudinfo" runat="server" visible="false">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="sub-heading">
                                                <h5>STUDENT INFORMATION</h5>
                                            </div>
                                        </div>

                                        <div class="col-12">
                                            <div id="divStudentInfo" style="display: block;">
                                                <div class="row">
                                                    <div class="col-lg-4 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered ipad-view">
                                                            <%--<b>Admission No. :</b><a class="pull-right">
                                                                        <asp:Label ID="lblRegNo" runat="server" Font-Bold="True"></asp:Label></a>
                                                                    <p></p>--%>
                                                            <li class="list-group-item"><b>Student ID. :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblRegNo" runat="server" Font-Bold="True"></asp:Label>
                                                                </a>
                                                            </li>
                                                            <li class="list-group-item">
                                                                <b>Name With Initial:</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblStudName" runat="server" Font-Bold="True"></asp:Label>

                                                                </a>
                                                            </li>

                                                            <li class="list-group-item">
                                                                <b>Mobile No. :</b><a class="pull-right">
                                                                    <asp:Label ID="lblMobileNo" runat="server" Font-Bold="True"></asp:Label></a>
                                                            </li>
                                                            <li class="list-group-item">
                                                                <b>Status :</b><a class="pull-right">
                                                                    <asp:Label ID="lblenrolled" runat="server" Font-Bold="True"></asp:Label></a>
                                                            </li>
                                                        </ul>
                                                    </div>

                                                    <div class="col-lg-4 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item"><b>Registration No. :</b>
                                                                <a class="sub-label">
                                                                    <asp:Label ID="lblenroll" runat="server" Font-Bold="True"></asp:Label>
                                                                </a>
                                                            </li>
                                                            <li class="list-group-item">
                                                                <b>Semester :</b><a class="pull-right">
                                                                    <asp:Label ID="lblSemester" runat="server" Font-Bold="True"></asp:Label></a>
                                                            </li>
                                                            <li class="list-group-item" style="display: none">
                                                                <b>Date of Admission :</b><a class="pull-right">
                                                                    <asp:Label ID="lblDateOfAdm" runat="server" Font-Bold="True"></asp:Label></a>
                                                            </li>
                                                            <li class="list-group-item">
                                                                <b>Year :</b><a class="pull-right">
                                                                    <asp:Label ID="lblYear" runat="server" Font-Bold="True"></asp:Label></a>
                                                            </li>
                                                            <li class="list-group-item">
                                                                <b>Intake :</b><a class="pull-right">
                                                                    <asp:Label ID="lblBatch" runat="server" Font-Bold="True"></asp:Label></a>
                                                            </li>


                                                        </ul>
                                                    </div>
                                                    <div class="col-lg-4 col-md-6 col-12">
                                                        <ul class="list-group list-group-unbordered">
                                                            <li class="list-group-item">
                                                                <b>Faculty/school Name :</b><a class="pull-right">
                                                                    <asp:Label ID="lblCollege" runat="server" Font-Bold="True"></asp:Label></a>
                                                            </li>
                                                            <li class="list-group-item">
                                                                <b>Program :</b><a class="pull-right">
                                                                    <asp:Label ID="lblDegree" runat="server" Font-Bold="True"></asp:Label></a>
                                                            </li>
                                                            <%-- <li class="list-group-item">
                                                                        <b>Specialization :</b><a class="pull-right">
                                                                            <asp:Label ID="lblBranch" runat="server" Font-Bold="True"></asp:Label></a>
                                                                    </li>--%>
                                                            <li class="list-group-item">
                                                                <b>Gender :</b><a class="pull-right">
                                                                    <asp:Label ID="lblSex" runat="server" Font-Bold="True"></asp:Label></a>
                                                                <br />
                                                            </li>
                                                            <li class="list-group-item" style="display: none">
                                                                <b>Payment Type :</b><a class="pull-right">
                                                                    <asp:Label ID="lblPaymentType" runat="server" Font-Bold="True"></asp:Label></a>
                                                            </li>
                                                        </ul>
                                                        <br />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group col-md-12 table table-responsive">
                                    <asp:Panel ID="Panel1" runat="server" Visible="false">
                                        <asp:ListView ID="lvFeesDetails" runat="server">
                                            <LayoutTemplate>
                                                <div class="sub-heading">
                                                    <h5>Fees Details</h5>
                                                </div>
                                                <%--<h4>Fees Details</h4>--%>
                                                <table id="tblDD_Details" class="table table-hover table-bordered">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th>Sr No.
                                                            </th>
                                                            <th>Receipt Type
                                                            </th>
                                                            <th>Semester
                                                            </th>
                                                            <th>Demand
                                                            </th>
                                                            <th>Paid Amount
                                                            </th>
                                                            <th>Late Fee
                                                            </th>
                                                            <th>Total Paid
                                                            </th>
                                                            <th>Receipt No
                                                            </th>
                                                            <th>Receipt Date
                                                            </th>
                                                            <th>Balanced Amount
                                                            </th>
                                                            <th>Pay Type
                                                            </th>
                                                            <th>Status 
                                                            </th>
                                                            <th>Print
                                                            </th>
                                                            <th>Deposit Date</th>
                                                            <th>Deposit Slip</th>
                                                            <th>Statement Of Account</th>
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
                                                        <asp:Label ID="lblRecieptCode" runat="server" Text='<%# Eval("RECIEPT_CODE") %>' />
                                                    </td>
                                                    <td style="text-align: center">
                                                        <%# Eval("SEMESTERNAME") %>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblDemand" runat="server" Text='<%# Eval("DEMAND") %>'></asp:Label>
                                                        <%--<%# Eval("DEMAND") %>--%>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblTotPaid" runat="server" Text='<%# Eval("PAID_AMT") %>' />
                                                    </td>
                                                    <td>
                                                        <%# Eval("LATE_FEE") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("AMOUNT") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("REC_NO") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("REC_DT","{0:dd/MM/yyyy}") %>
                                                    </td>

                                                    <td>

                                                        <asp:Label ID="lblBalAmt" runat="server" Text='<%# Eval("BALAMT")%>'></asp:Label>
                                                    </td>
                                                    <td style="text-align: center">
                                                        <%# Eval("PAY_MODE_CODE") %>
                                                    </td>
                                                    <td class="text-center">
                                                        <span class="badge badge-success"><%#Eval("PAY_STATUS") %></span></>
                                                               <asp:Label ID="lblpaystatus" runat="server" Text='<%#Eval("PAY_STATUS") %>' Visible="false"></asp:Label>
                                                    </td>


                                                    <td>
                                                        <asp:UpdatePanel ID="updviewprint" runat="server">
                                                            <ContentTemplate>
                                                                <asp:ImageButton ID="ImgPrintReceipt" Visible="false" runat="server" ImageUrl="~/IMAGES/print.gif"
                                                                    CommandArgument='<%# Eval("DCR_NO") %>' CommandName='<%# Eval("IDNO") %>'
                                                                    ToolTip='<%# Eval("COLLEGE_ID")%>' CausesValidation="False" OnClick="ImgPrintReceipt_Click" />

                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:PostBackTrigger ControlID="ImgPrintReceipt" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                    <td>
                                                        <%# Eval("CHALAN_DATE","{0:dd/MM/yyyy}") %>
                                                    </td>
                                                    <td style="text-align: center">
                                                        <asp:ImageButton ID="btnDownloadFile" Style="width: 18px;" Visible="false" runat="server" ImageUrl="~/IMAGES/view.gif"
                                                            CommandArgument='<%# Eval("IDNO") %>' CommandName='<%# Eval("TEMP_DCR_NO") %>'
                                                            ToolTip='<%# Eval("DOC_FILENAME")%>' OnClick="btnDownloadFile_Click" />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="ReportButton" runat="server" Text="Report" CssClass="btn btn-outline-primary"
                                                            CommandArgument='<%# Eval("SEMESTERNO") + "-" + Eval("ADMBATCH") + "-" + Eval("PAYTYPENO") + "-" + Eval("RECIEPT_CODE") %>' CommandName='<%# Eval("IDNO") %>'
                                                            ToolTip='<%# Eval("SESSIONNO")%>' CausesValidation="False" OnClick="ReportButton_Click" />

                                                    </td>
                                                </tr>
                                            </ItemTemplate>

                                        </asp:ListView>

                                    </asp:Panel>

                                </div>
                                <div class="form-group col-md-12 table table-responsive">
                                    <asp:Panel ID="Panel3" runat="server">
                                        <asp:ListView ID="lvMisc" runat="server">
                                            <LayoutTemplate>
                                                <div class="sub-heading">
                                                    <h5>Misc Fees Details</h5>
                                                </div>
                                                <%--<h4>Fees Details</h4>--%>
                                                <table id="tblDD_Details" class="table table-hover table-bordered">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th>Sr No.
                                                            </th>
                                                            <th>Receipt Type
                                                            </th>
                                                            <th>Semester
                                                            </th>
                                                            <th>Paid Amount
                                                            </th>
                                                            <th>Receipt No
                                                            </th>
                                                            <th>Receipt Date
                                                            </th>
                                                            <th>Pay Type
                                                            </th>
                                                            <th>Status 
                                                            </th>
                                                            <th>Print
                                                            </th>
                                                            <th>Deposit Date</th>

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
                                                        <asp:Label ID="lblRecieptCode" runat="server" Text='<%# Eval("RECIEPT_CODE") %>' />
                                                    </td>
                                                    <td style="text-align: center">
                                                        <%# Eval("SEMESTERNAME") %>
                                                    </td>

                                                    <td>
                                                        <asp:Label ID="lblTotPaid" runat="server" Text='<%# Eval("PAID_AMT") %>' />
                                                    </td>
                                                    <td>
                                                        <%# Eval("REC_NO") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("REC_DT","{0:dd/MM/yyyy}") %>
                                                    </td>

                                                    <td style="text-align: center">
                                                        <%# Eval("PAY_MODE_CODE") %>
                                                    </td>
                                                    <td class="text-center">
                                                        <span class="badge badge-success"><%#Eval("PAY_STATUS") %></span></>
                                                               <asp:Label ID="lblpaystatus" runat="server" Text='<%#Eval("PAY_STATUS") %>' Visible="false"></asp:Label>
                                                    </td>


                                                    <td>
                                                        <asp:UpdatePanel ID="updviewprintmisc" runat="server">
                                                            <ContentTemplate>
                                                                <asp:ImageButton ID="ImgPrintReceiptisc" runat="server" ImageUrl="~/IMAGES/print.gif"
                                                                    CommandArgument='<%# Eval("DCR_NO") %>' CommandName='<%# Eval("IDNO") %>'
                                                                    ToolTip='<%# Eval("COLLEGE_ID")%>' CausesValidation="False" OnClick="ImgPrintReceiptisc_Click" />

                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:PostBackTrigger ControlID="ImgPrintReceiptisc" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                    <td>
                                                        <%# Eval("CHALAN_DATE","{0:dd/MM/yyyy}") %>
                                                    </td>

                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>
                                <div class="form-group col-md-12 table table-responsive">
                                    <asp:Panel ID="Panel2" runat="server" Visible="false">
                                        <asp:ListView ID="lvApplicationFee" runat="server">
                                            <LayoutTemplate>
                                                <div class="sub-heading">
                                                    <h5>Application Fee Details</h5>
                                                </div>
                                                <%--<h4>Application Fee Details</h4>--%>
                                                <table id="tblDD_Details" class="table table-hover table-bordered">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th>Sr No.
                                                            </th>
                                                            <th>Application No.
                                                            </th>
                                                            <th>Student Name
                                                            </th>
                                                            <th>Paid Amount
                                                            </th>
                                                            <th>Receipt No
                                                            </th>
                                                            <th>Order Id
                                                            </th>
                                                            <th>Transaction Id
                                                            </th>
                                                            <th>Status
                                                            </th>
                                                            <th>Print
                                                            </th>
                                                            <th>Deposit Slip</th>
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
                                                        <asp:Label ID="lblApplicationNo" runat="server" Text='<%# Eval("ENROLLNO") %>' />
                                                    </td>
                                                    <td>
                                                        <%# Eval("STUDNAME") %>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblAmt" runat="server" Text='<%# Eval("PAID_AMT") %>' />
                                                    </td>
                                                    <td>
                                                        <%# Eval("REC_NO") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("ORDER_ID") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("APTRANSACTIONID") %>
                                                    </td>

                                                    <td class="text-center">
                                                        <span class="badge badge-success"><%#Eval("DOCUPLODED") %></span></>
                                                    </td>
                                                    <td>
                                                        <asp:UpdatePanel ID="updadmprint" runat="server">
                                                            <ContentTemplate>
                                                                <asp:ImageButton ID="ImgPrintReceiptApp" runat="server" ImageUrl="~/IMAGES/print.gif"
                                                                    CommandArgument='<%# Eval("USERNO") %>'
                                                                    CausesValidation="False" OnClick="ImgPrintReceiptApp_Click" />
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:PostBackTrigger ControlID="ImgPrintReceiptApp" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>

                                                    </td>
                                                    <td style="text-align: center">
                                                        <asp:ImageButton ID="BtnDownload" runat="server" ImageUrl="~/IMAGES/view.gif" Style="width: 18px;"
                                                            CommandArgument='<%# Eval("USERNO") %>' ToolTip='<%# Eval("DOC_FILENAME") %>'
                                                            CausesValidation="False" OnClick="BtnDownload_Click" />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </asp:Panel>
                                </div>

                                <div class="col-12" runat="server" id="div5" visible="false">
                                    <div class="row">
                                        <div class="col-lg-4 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item">
                                                    <b>Total Demand Amount :</b><a class="pull-right">
                                                        <asp:Label ID="lbltotalDemand" runat="server" Font-Bold="True"></asp:Label></a>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="col-lg-4 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item">
                                                    <b>Total Semester Paid Amount :</b><a class="pull-right">
                                                        <asp:Label ID="lblTotalPaid" runat="server" Font-Bold="True"> </asp:Label></a>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="col-lg-4 col-md-6 col-12">
                                            <ul class="list-group list-group-unbordered">
                                                <li class="list-group-item">
                                                    <b>Total Balance Amount   :</b> <a class="pull-right">
                                                        <asp:Label ID="lblTotalBalance" runat="server" Font-Bold="True"> </asp:Label></a>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>

                                <div style="margin-left: 420px" id="divtotamt" runat="server" visible="false">
                                    <span><b>Total Paid Amount :&nbsp;&nbsp;&nbsp;</b></span>
                                    <b style="color: green">
                                        <asp:Label ID="lblPaidAmt" runat="server"> </asp:Label></b>
                                </div>

                                <div class="form-group col-md-12 table table-responsive">
                                    <asp:ListView ID="lvpetycash" runat="server" OnItemDataBound="lvpetycash_ItemDataBound">
                                        <LayoutTemplate>
                                            <div id="demo-grid">
                                                <h4>Petty Cash Collection Details</h4>
                                                <table id="tblDD_Details" class="table table-hover table-bordered " style="text-align: center">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th>
                                                                <center>Sr No.</center>
                                                            </th>
                                                            <th>
                                                                <center>Admission No.</center>
                                                            </th>
                                                            <th>
                                                                <center>Student Name</center>
                                                            </th>
                                                            <th>
                                                                <center>Total Amount</center>
                                                            </th>
                                                            <th>
                                                                <center>Reciept Details</center>
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
                                                    <%# Container.DataItemIndex + 1%>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblenroll" runat="server" Text='<%# Eval("ENROLLNO") %>' ToolTip='<%# Eval("IDNO") %>' />
                                                </td>
                                                <td>
                                                    <%# Eval("STUDNAME") %>
                                                </td>
                                                <td>
                                                    <%# Eval("TOTAL") %>
                                                </td>
                                                <td>

                                                    <a href="#raju" data-toggle="collapse">
                                                        <img alt="" style="cursor: pointer" src="../images/plus.gif" /></a>
                                                </td>
                                                <tr>
                                                    <td colspan="5">
                                                        <div id="raju" class="collapse">
                                                            <asp:ListView ID="lvpetycashdetails" runat="server">
                                                                <LayoutTemplate>
                                                                    <div id="demo-grid">
                                                                        <table id="tblDD_Details" class="table table-hover table-bordered">
                                                                            <thead>
                                                                                <tr class="bg-success">
                                                                                    <th>
                                                                                        <center>Sr No.</center>
                                                                                    </th>
                                                                                    <th>
                                                                                        <center>Reciept No.</center>
                                                                                    </th>
                                                                                    <th>
                                                                                        <center>Fees Type</center>
                                                                                    </th>
                                                                                    <th>
                                                                                        <center>Date</center>
                                                                                    </th>
                                                                                    <th>
                                                                                        <center>Amount</center>
                                                                                    </th>
                                                                                    <th>
                                                                                        <center>Pay Type</center>
                                                                                    </th>
                                                                                    <th>
                                                                                        <center> Print</center>
                                                                                        <%--  <asp:Button ID="btnDownloadall" runat="server" Text="Print All"
                                                                                  OnClick="btnDownloadall_Click"
                                                                                    CssClass="btn btn-info" ToolTip='<%# Eval("IDNO")%>' />--%>
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
                                                                            <%# Container.DataItemIndex + 1%>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbltrno" runat="server" Text='<%# Eval("COUNTR") %>' ToolTip='<%# Eval("IDNO") %>' />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblhead" runat="server" Text='<%# Eval("MISCHEAD") %>'
                                                                                ToolTip='<%# Eval("MISCDCRSRNO") %>' />
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("AUDITDATE") %>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("MISCAMT") %>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("PAYSTATUS") %>
                                                                        </td>
                                                                        <td>
                                                                            <asp:ImageButton ID="ImgPrintReceipt1" runat="server" ImageUrl="~/IMAGES/print.gif"
                                                                                CommandArgument='<%# Eval("MISCDCRSRNO") %>'
                                                                                ToolTip='<%# Eval("IDNO")%>' CausesValidation="False" OnClick="ImgPrintReceipt1_Click" />
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </div>
                                                    </td>
                                                </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                            </div>
                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnStudentLedgerReport" runat="server" Text="Student Ledger Report" CausesValidation="false"
                                    OnClick="btnStudentLedgerReport_Click" TabIndex="15" CssClass="btn btn-outline-primary" />
                                <asp:Button ID="btnReport" runat="server" Text="Report" CausesValidation="false" Visible="false"
                                    OnClick="btnReport_Click" TabIndex="16" CssClass="btn btn-outline-primary" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false"
                                    OnClick="btnCancel_Click" TabIndex="17" CssClass="btn btn-outline-danger" />
                                <asp:ValidationSummary ID="valSummery1" DisplayMode="List" runat="server" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="submit" />
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnShow" />
            <asp:PostBackTrigger ControlID="btnReport" />
            <asp:PostBackTrigger ControlID="btnStudentLedgerReport" />
        </Triggers>
    </asp:UpdatePanel>

    <div id="myModal22" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content" style="margin-top: -25px">
                <div class="modal-body">
                    <asp:UpdatePanel ID="updview" runat="server">
                        <ContentTemplate>
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" style="margin-top: -18px">x</button>
                            </div>
                            <asp:Image ID="ImageViewer" runat="server" Width="100%" Height="500px" />
                            <asp:Literal ID="ltEmbed" runat="server" />
                            <div id="imageViewerContainer" runat="server"></div>
                            <asp:HiddenField ID="hdfImagePath" runat="server" />

                            <div class="modal-footer" style="height: 0px">
                                <button type="button" style="margin-top: -10px" class="btn btn-default" data-dismiss="modal">Close</button>
                            </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <div id="divMsg" runat="server">
    </div>
    <script type="text/javascript">

        function ShowModalbox() {
            try {
                Modalbox.show($('divModalboxContent'), { title: 'Search Student for Refund', width: 700, overlayClose: false, slideDownDuration: 0.1, slideUpDuration: 0.1, afterLoad: SetFocus });
            }
            catch (e) {
                alert("Error: " + e.description);
            }
            return;
        }

        function SetFocus() {
            try {
                document.getElementById('<%= txtSearch.ClientID %>').focus();
            }
            catch (e) {
                alert("Error: " + e.description);
            }
        }

        function SubmitSearch(btnsearch) {
            try {
                var searchParams = "";
                if (document.getElementById('<%= rdoName.ClientID %>').checked) {
                    searchParams = "Name=" + document.getElementById('<%= txtSearch.ClientID %>').value;
                    searchParams += ",REGNO=";
                }
                else if (document.getElementById('<%= rdoRegNo.ClientID %>').checked) {
                    searchParams = "REGNO=" + document.getElementById('<%= txtSearch.ClientID %>').value;
                    searchParams += ",Name=";
                }
                else if (document.getElementById('<%= rdoTan.ClientID %>').checked) {
                    searchParams = "ENROLLNO=" + document.getElementById('<%= txtSearch.ClientID %>').value;
                    searchParams += ",emailid=";
                }
                else if (document.getElementById('<%= rdoEnrollmentNo.ClientID %>').checked) {
                    searchParams = "emailid=" + document.getElementById('<%= txtSearch.ClientID %>').value;
                    searchParams += ",ENROLLNO=";
                }
                else if (document.getElementById('<%= rdoNic.ClientID %>').checked) {
                    searchParams = "NICNO=" + document.getElementById('<%= txtSearch.ClientID %>').value;
                    searchParams += ",ENROLLNO=";
                }

    __doPostBack(btnsearch, searchParams);
}
    catch (e) {
        alert("Error: " + e.description);
    }
    return;
}
function ClearSearchBox(btnClear) {
    document.getElementById('<%=txtSearch.ClientID %>').value = '';
    __doPostBack(btnClear, '');
    return true;
}
    </script>
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
    <script type="text/javascript">
        $(document).ready(function () {
           
            $("#txtSearchStudent").autocomplete({

                source: function (request, response) {
                    if (request.term.length > 4)
                    {
                    var obj = {};
                    obj.textsearch = request.term;

                    var searchText = request.term;
                    var message = "Hello, Web Service!";
                    $.ajax({
                        type: "POST",
                        //url: "SearchForm.aspx/GetSuggestions",
                        url: "/WEB API/SearchName.asmx/GetSuggestions",
                        //url: "/WEB_API/SearchName.asmx/GetSuggestions",
                        //data: JSON.stringify(obj),
                        data: JSON.stringify(obj),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            if (data.d != '') {
                                response($.map(data.d, function (item) {
                                    return {

                                        label: item['STUDNAME'],
                                        val: item['IDNO']
                                    }
                                }))
                            }
                            else {
                                return;
                            }
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

        var parameter = Sys.WebForms.PageRequestManager.getInstance();
        parameter.add_endRequest(function () {
            $(document).ready(function () {
                $("#txtSearchStudent").autocomplete({

                    source: function (request, response) {
                        if (request.term.length > 4) {
                            var obj = {};
                            obj.textsearch = request.term;

                            var searchText = request.term;
                            var message = "Hello, Web Service!";
                            $.ajax({
                                type: "POST",
                                //url: "SearchForm.aspx/GetSuggestions",
                                url: "/WEB API/SearchName.asmx/GetSuggestions",
                                //url: "/WEB_API/SearchName.asmx/GetSuggestions",
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

        });
    </script>
</asp:Content>
