<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="CourseWorkDetails.aspx.cs" Inherits="ACADEMIC_CourseWorkDetails"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!--  Calendar  -->
    <link href="../plugins/calendar/bootstrap-datetimepicker.min.css" rel="stylesheet" />
    <script src="../plugins/calendar/moment.min.js"></script>
    <script src="../plugins/calendar/bootstrap-datetimepicker.min.js"></script>
    <script src="../plugins/inputmask/jquery.inputmask.bundle.min.js"></script>


    <script>
        /* INPUT MASK */
        $(document).ready(function () {
            debugger;
            init_InputMask();// for fileupload control change event work after postback
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(init_InputMask);
        });

        function init_InputMask() {
            debugger;
            if (typeof ($.fn.inputmask) === 'undefined') { return; }
            console.log('init_InputMask');

            $(":input").inputmask();
        };
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            bindDataTable1();// for fileupload control change event work after postback
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable1);
        });

        function bindDataTable1() {
            debugger;

            $('#dtappearYear1,#dtappearYear2,#dtappearYr3,#dtappearYr4,#dtappearYr5,#dtappearYr6,#dtappearYr7,#dtappearYr8').datetimepicker({
                format: 'MM/YYYY',
            });
        }

        $('#dtappearYear1,#dtappearYear2,#dtappearYr3,#dtappearYr4,#dtappearYr5,#dtappearYr6,#dtappearYr7,#dtappearYr8').datetimepicker({
            format: 'MM/YYYY'
        });
    </script>

    <style>
        .tablesubject .input-group-addon {
            background-color: rgb(238, 238, 238);
            border-color: rgb(204, 204, 204);
        }
    </style>

    <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpnlExam" DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:UpdatePanel ID="updpnlExam" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">COURSE WORK DETAILS</h3>
                            <div class="pull-right">
                                <div style="color: Red; font-weight: bold;">
                                    Note : * Marked fields are mandatory
                                </div>
                            </div>
                        </div>

                        <div class="box-body">
                            <div class="col-md-12">
                                <fieldset>

                                    <div id="step-8" class="">
                                        <div class="container-fluid">
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel9">
                                                <ContentTemplate>

                                                    <div class="row col-md-12">
                                                        <div class="col-md-6">
                                                            <div class="col-md-8" style="margin-top: 10px">
                                                                <label><span style="color: red">*</span> Registration No./ Admission No. :</label>
                                                                <asp:TextBox ID="txtStudent" runat="server" CssClass="form-control" ToolTip="Enter Admission No." TabIndex="1"
                                                                    Style="width: 280px" placeholder="Registration No./ Admission No." />
                                                            </div>
                                                            <div class="box-footer col-md-3" style="border-top: none; margin-left: -20px; margin-top: 25px">
                                                                <asp:Button ID="btnShow" runat="server" Text="Search" TabIndex="2" CssClass="btn btn-outline-info" OnClick="btnShow_Click" ValidationGroup="search" />
                                                                <asp:RequiredFieldValidator ID="valSearchText" runat="server" ControlToValidate="txtStudent" Display="None"
                                                                    ErrorMessage="Please Enter Registration No. or Admission No.!" SetFocusOnError="true" ValidationGroup="search" Width="10%" />
                                                                <asp:ValidationSummary ID="valSummery" runat="server" DisplayMode="List" ShowMessageBox="true" ShowSummary="false"
                                                                    ValidationGroup="search" />
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row" id="divdata" runat="server" visible="false">

                                                        <div class="row col-md-12" style="padding-top: 20px"></div>

                                                        <div class="col-md-6">
                                                            <ul class="list-group list-group-unbordered">
                                                                <li class="list-group-item">
                                                                    <b>Student Name</b>
                                                                    <a class="pull-right">
                                                                        <asp:Label ID="lblName" runat="server" Font-Bold="True" />
                                                                    </a>
                                                                </li>
                                                                <li class="list-group-item">
                                                                    <b>Registration No. :</b>
                                                                    <a class="pull-right">
                                                                        <asp:Label ID="lblRegNo" runat="server" Font-Bold="True" />
                                                                    </a>
                                                                </li>
                                                            </ul>
                                                        </div>

                                                        <div class=" col-md-6">
                                                            <ul class="list-group list-group-unbordered">
                                                                <li class="list-group-item"  runat="server" visible="false">
                                                                    <b>Regulation :</b>
                                                                    <a class="pull-right">
                                                                        <asp:Label ID="lblScheme" runat="server" Font-Bold="True"></asp:Label>
                                                                    </a>
                                                                </li>
                                                                <li class="list-group-item">
                                                                    <b>Admission No. :</b>
                                                                    <a class="pull-right">
                                                                        <asp:Label ID="lblEnrollno" runat="server" Font-Bold="True"></asp:Label>
                                                                    </a>
                                                                </li>
                                                            </ul>
                                                        </div>
                                                    </div>

                                                    <div id="divlv" runat="server" visible="false">
                                                        <h4>Course Work List</h4>
                                                        <div class="row">
                                                            <table border="1" width="98%" class="tablesubject">

                                                                <thead>
                                                                    <th style="width: 40px" class="text-center">
                                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group">Sr.No.</div>
                                                                    </th>
                                                                    <th style="width: 160px" class="text-center">
                                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group">Subject Code</div>
                                                                    </th>
                                                                    <th style="width: 250px" class="text-center">
                                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group">Subject Title</div>
                                                                    </th>
                                                                    <th style="width: 200px" class="text-center">
                                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group">College Code and Name</div>
                                                                    </th>
                                                                    <th style="width: 160px" class="text-center">
                                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group">Exam Appearance Month & Year</div>
                                                                    </th>
                                                                    <th class="text-center">
                                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group">Result Awarded</div>
                                                                    </th>
                                                                </thead>

                                                                <tr>
                                                                    <td class="text-center">
                                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group"><span style="color: red">*</span> 1</div>
                                                                    </td>
                                                                    <td>
                                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group" style="padding-top: 5px;">
                                                                            <asp:TextBox runat="server" ID="txtccode1" class="form-control" placeholder="Enter CCODE" MaxLength="16" TabIndex="3"></asp:TextBox>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group" style="padding-top: 5px;">
                                                                            <asp:TextBox runat="server" ID="txtcourse1" class="form-control" placeholder="Enter Course" MaxLength="32" TabIndex="4"></asp:TextBox>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group" style="padding-top: 5px;">
                                                                            <asp:TextBox runat="server" ID="txtcollege1" class="form-control" placeholder="Enter College Name" MaxLength="64" TabIndex="5"></asp:TextBox>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group" style="padding-top: 5px;">
                                                                            <div class='input-group date' id='dtappearYear1'>
                                                                                <asp:TextBox runat="server" ID="txtAppearYr1" class="form-control" MaxLength="7" data-inputmask="'mask': '99/9999'" placeholder="MM/YYYY" TabIndex="6"></asp:TextBox>
                                                                                <span class="input-group-addon">
                                                                                    <span class="glyphicon glyphicon-calendar"></span>
                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group" style="padding-top: 5px;">
                                                                            <asp:TextBox runat="server" ID="txtResult1" class="form-control" placeholder="Enter Result Awarded" MaxLength="500" TabIndex="7"></asp:TextBox>
                                                                        </div>
                                                                    </td>
                                                                </tr>

                                                                <tr>
                                                                    <td class="text-center">
                                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group"><span style="color: red">*</span> 2</div>
                                                                    </td>
                                                                    <td>
                                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group" style="padding-top: 5px;">
                                                                            <asp:TextBox runat="server" ID="txtccode2" class="form-control" placeholder="Enter CCODE" MaxLength="16" TabIndex="8"></asp:TextBox>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group" style="padding-top: 5px;">
                                                                            <asp:TextBox runat="server" ID="txtcourse2" class="form-control" placeholder="Enter Course" MaxLength="32" TabIndex="9"></asp:TextBox>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group" style="padding-top: 5px;">
                                                                            <asp:TextBox runat="server" ID="txtcollege2" class="form-control" placeholder="Enter College Name" MaxLength="64" TabIndex="10"></asp:TextBox>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group" style="padding-top: 5px;">
                                                                            <div class='input-group date' id='dtappearYear2'>
                                                                                <asp:TextBox runat="server" ID="txtAppearYr2" class="form-control" MaxLength="7" data-inputmask="'mask': '99/9999'" placeholder="MM/YYYY" TabIndex="11"></asp:TextBox>
                                                                                <span class="input-group-addon">
                                                                                    <span class="glyphicon glyphicon-calendar"></span>
                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group" style="padding-top: 5px;">
                                                                            <asp:TextBox runat="server" ID="txtResult2" class="form-control" placeholder="Enter Result Awarded" MaxLength="500" TabIndex="12"></asp:TextBox>
                                                                        </div>
                                                                    </td>
                                                                </tr>

                                                                <tr>
                                                                    <td class="text-center">
                                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group"><sup></sup>3</div>
                                                                    </td>
                                                                    <td>
                                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group" style="padding-top: 5px;">
                                                                            <asp:TextBox runat="server" ID="txtccode3" class="form-control" placeholder="Enter CCODE" MaxLength="16" TabIndex="13"></asp:TextBox>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group" style="padding-top: 5px;">
                                                                            <asp:TextBox runat="server" ID="txtcourse3" class="form-control" placeholder="Enter Course" MaxLength="32" TabIndex="14"></asp:TextBox>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group" style="padding-top: 5px;">
                                                                            <asp:TextBox runat="server" ID="txtcollege3" class="form-control" placeholder="Enter College Name" MaxLength="64" TabIndex="15"></asp:TextBox>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group" style="padding-top: 5px;">
                                                                            <div class='input-group date' id='dtappearYr3'>
                                                                                <asp:TextBox runat="server" ID="txtAppearYr3" class="form-control" MaxLength="7" data-inputmask="'mask': '99/9999'" placeholder="MM/YYYY" TabIndex="16"></asp:TextBox>
                                                                                <span class="input-group-addon">
                                                                                    <span class="glyphicon glyphicon-calendar"></span>
                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group" style="padding-top: 5px;">
                                                                            <asp:TextBox runat="server" ID="txtResult3" class="form-control" placeholder="Enter Result Awarded" MaxLength="500" TabIndex="17"></asp:TextBox>
                                                                        </div>
                                                                    </td>
                                                                </tr>

                                                                <tr>
                                                                    <td class="text-center">
                                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group"><sup></sup>4</div>
                                                                    </td>
                                                                    <td>
                                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group" style="padding-top: 5px;">
                                                                            <asp:TextBox runat="server" ID="txtccode4" class="form-control" placeholder="Enter CCODE" MaxLength="16" TabIndex="18"></asp:TextBox>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group" style="padding-top: 5px;">
                                                                            <asp:TextBox runat="server" ID="txtcourse4" class="form-control" placeholder="Enter Course" MaxLength="32" TabIndex="19"></asp:TextBox>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group" style="padding-top: 5px;">
                                                                            <asp:TextBox runat="server" ID="txtcollege4" class="form-control" placeholder="Enter College Name" MaxLength="64" TabIndex="20"></asp:TextBox>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group" style="padding-top: 5px;">
                                                                            <div class='input-group date' id='dtappearYr4'>
                                                                                <asp:TextBox runat="server" ID="txtAppearYr4" class="form-control" MaxLength="7" data-inputmask="'mask': '99/9999'" placeholder="MM/YYYY" TabIndex="21"></asp:TextBox>
                                                                                <span class="input-group-addon">
                                                                                    <span class="glyphicon glyphicon-calendar"></span>
                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group" style="padding-top: 5px;">
                                                                            <asp:TextBox runat="server" ID="txtResult4" class="form-control" placeholder="Enter Result Awarded" MaxLength="500" TabIndex="22"></asp:TextBox>
                                                                        </div>
                                                                    </td>
                                                                </tr>

                                                                <tr>
                                                                    <td class="text-center">
                                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group"><sup></sup>5</div>
                                                                    </td>
                                                                    <td>
                                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group" style="padding-top: 5px;">
                                                                            <asp:TextBox runat="server" ID="txtccode5" class="form-control" placeholder="Enter CCODE" MaxLength="16" TabIndex="23"></asp:TextBox>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group" style="padding-top: 5px;">
                                                                            <asp:TextBox runat="server" ID="txtcourse5" class="form-control" placeholder="Enter Course" MaxLength="32" TabIndex="24"></asp:TextBox>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group" style="padding-top: 5px;">
                                                                            <asp:TextBox runat="server" ID="txtcollege5" class="form-control" placeholder="Enter College Name" MaxLength="64" TabIndex="25"></asp:TextBox>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group" style="padding-top: 5px;">
                                                                            <div class='input-group date' id='dtappearYr5'>
                                                                                <asp:TextBox runat="server" ID="txtAppearYr5" class="form-control" MaxLength="7" data-inputmask="'mask': '99/9999'" placeholder="MM/YYYY" TabIndex="26"></asp:TextBox>
                                                                                <span class="input-group-addon">
                                                                                    <span class="glyphicon glyphicon-calendar"></span>
                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group" style="padding-top: 5px;">
                                                                            <asp:TextBox runat="server" ID="txtResult5" class="form-control" placeholder="Enter Result Awarded" MaxLength="500" TabIndex="27"></asp:TextBox>
                                                                        </div>
                                                                    </td>
                                                                </tr>

                                                                <tr>
                                                                    <td class="text-center">
                                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group">6</div>
                                                                    </td>
                                                                    <td>
                                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group" style="padding-top: 5px;">
                                                                            <asp:TextBox runat="server" ID="txtccode6" class="form-control" placeholder="Enter CCODE" MaxLength="16" TabIndex="28"></asp:TextBox>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group" style="padding-top: 5px;">
                                                                            <asp:TextBox runat="server" ID="txtcourse6" class="form-control" placeholder="Enter Course" MaxLength="32" TabIndex="29"></asp:TextBox>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group" style="padding-top: 5px;">
                                                                            <asp:TextBox runat="server" ID="txtcollege6" class="form-control" placeholder="Enter College Name" MaxLength="64" TabIndex="30"></asp:TextBox>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group" style="padding-top: 5px;">
                                                                            <div class='input-group date' id='dtappearYr6'>
                                                                                <asp:TextBox runat="server" ID="txtAppearYr6" class="form-control" MaxLength="7" data-inputmask="'mask': '99/9999'" placeholder="MM/YYYY" TabIndex="31"></asp:TextBox>
                                                                                <span class="input-group-addon">
                                                                                    <span class="glyphicon glyphicon-calendar"></span>
                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group" style="padding-top: 5px;">
                                                                            <asp:TextBox runat="server" ID="txtResult6" class="form-control" placeholder="Enter Result Awarded" MaxLength="500" TabIndex="32"></asp:TextBox>
                                                                        </div>
                                                                    </td>
                                                                </tr>

                                                                <tr>
                                                                    <td class="text-center">
                                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group">7</div>
                                                                    </td>
                                                                    <td>
                                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group" style="padding-top: 5px;">
                                                                            <asp:TextBox runat="server" ID="txtccode7" class="form-control" placeholder="Enter CCODE" MaxLength="16" TabIndex="33"></asp:TextBox>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group" style="padding-top: 5px;">
                                                                            <asp:TextBox runat="server" ID="txtcourse7" class="form-control" placeholder="Enter Course" MaxLength="32" TabIndex="34"></asp:TextBox>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group" style="padding-top: 5px;">
                                                                            <asp:TextBox runat="server" ID="txtcollege7" class="form-control" placeholder="Enter College Name" MaxLength="64" TabIndex="35"></asp:TextBox>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group" style="padding-top: 5px;">
                                                                            <div class='input-group date' id='dtappearYr7'>
                                                                                <asp:TextBox runat="server" ID="txtAppearYr7" class="form-control" MaxLength="7" data-inputmask="'mask': '99/9999'" placeholder="MM/YYYY" TabIndex="36"></asp:TextBox>
                                                                                <span class="input-group-addon">
                                                                                    <span class="glyphicon glyphicon-calendar"></span>
                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group" style="padding-top: 5px;">
                                                                            <asp:TextBox runat="server" ID="txtResult7" class="form-control" placeholder="Enter Result Awarded" MaxLength="500" TabIndex="37"></asp:TextBox>
                                                                        </div>
                                                                    </td>
                                                                </tr>

                                                                <tr>
                                                                    <td class="text-center">
                                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group">8</div>
                                                                    </td>
                                                                    <td>
                                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group" style="padding-top: 5px;">
                                                                            <asp:TextBox runat="server" ID="txtccode8" class="form-control" placeholder="Enter CCODE" MaxLength="16" TabIndex="38"></asp:TextBox>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group" style="padding-top: 5px;">
                                                                            <asp:TextBox runat="server" ID="txtcourse8" class="form-control" placeholder="Enter Course" MaxLength="32" TabIndex="39"></asp:TextBox>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group" style="padding-top: 5px;">
                                                                            <asp:TextBox runat="server" ID="txtcollege8" class="form-control" placeholder="Enter College Name" MaxLength="64" TabIndex="40"></asp:TextBox>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group" style="padding-top: 5px;">
                                                                            <div class='input-group date' id='dtappearYr8'>
                                                                                <asp:TextBox runat="server" ID="txtAppearYr8" class="form-control" MaxLength="7" data-inputmask="'mask': '99/9999'" placeholder="MM/YYYY" TabIndex="41"></asp:TextBox>
                                                                                <span class="input-group-addon">
                                                                                    <span class="glyphicon glyphicon-calendar"></span>
                                                                                </span>
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div class="col-md-12 col-sm-12 col-xs-12 form-group" style="padding-top: 5px;">
                                                                            <asp:TextBox runat="server" ID="txtResult8" class="form-control" placeholder="Enter Result Awarded" MaxLength="500" TabIndex="42"></asp:TextBox>
                                                                        </div>
                                                                    </td>
                                                                </tr>

                                                            </table>
                                                        </div>
                                                    </div>

                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>

                                    </div>

                                </fieldset>
                            </div>
                        </div>

                        <div class="box-footer">
                            <p class="text-center">
                                <asp:Button runat="server" ID="btnSubmit" class="btn btn-outline-info" Text="Submit" OnClick="btnSubmit_Click" TabIndex="43" Visible="false" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel"  class="btn btn-outline-danger" OnClick="btnCancel_Click" Visible="false" TabIndex="44"/>
                                <asp:ValidationSummary ID="ValidationSummary6" runat="server" ValidationGroup="Submit" ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </p>
                        </div>

                    </div>
                </div>
            </div>

            <div id="divMsg" runat="server"></div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
