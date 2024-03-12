<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="StudentNCCDetails.aspx.cs" Inherits="ACADEMIC_StudentNCCDetails"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!--  Calendar  -->
<%--    <link rel="stylesheet" href="../studInfo/plugin/calendar/bootstrap-datetimepicker.min.css">
    <script src="../validation/buttonValidation.js"></script>
    <script src="../validation/validation.js"></script>--%>



 <%--   <script type="text/javascript">
      $(document).ready(function () {
          $('.div-collapse').hide();
                $('tr.first-div').find('.img-collapse').click(function () {
                    $(this).parents('.first-div').next('tr').slideToggle();
                    //$(this).parents('.first-div').next('tr').toggleClass('active');
     });
   });
   </script>--%>


      <style>
        .not-allowed {cursor:not-allowed !important;opacity:.4;}
        </style>

    <script type="text/javascript">
        $(document).ready(function () {
            $('.div-collapse').hide();
            $('tr.first-div').find('.img-collapse').click(function () {
                $(this).parents('.first-div').next('tr').slideToggle();
                //$(this).parents('.first-div').next('tr').toggleClass('active');
            });

            $('tr.first-div').find('.img-collapse').each(function () {
                var $uv = $(this).parents('.first-div').next('tr').find('td');
                if ($uv.contents().length == 1) {
                    $(this).parents('tr.first-div').find('.img-collapse').addClass('not-allowed');
                    $uv.hide();
                } else {
                    $(this).parents('tr.first-div').find('.img-collapse').removeClass('not-allowed');
                    $uv.show();
                }
            });

        });
   </script>


   <%-- <div style="z-index: 1; position: absolute; top: 10%; left: 40%;">
        <asp:UpdateProgress ID="UpdateProgress2" runat="server"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>--%>



    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title"><b>STUDENT NCC DETAILS</b></h3>
                </div>

                <div class="box-body">


                    <asp:Panel ID="pnlSearch" runat="server">
                        <div class="panel panel-info">
                            <div class="box-body">
                                <div class="col-xs-12 col-md-offset-2 col-sm-offset-2">
                                    <div class="col-md-3 col-xs-12 col-sm-3" style="margin-top: 5px; text-align: right;">
                                        <label><span style="color: red">* </span>Univ. Reg. No / Adm. No : </label>
                                    </div>
                                    <div class="col-md-3 col-xs-12 col-sm-3">
                                        <asp:TextBox ID="txtEnrollmentSearch" runat="server" ValidationGroup="sub" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6 col-xs-12 col-sm-6" style="float: left;">

                                        <asp:Button ID="btnSearch" runat="server"
                                            OnClick="btnSearch_Click" ValidationGroup="sub" TabIndex="2" Text="Search" CssClass="btn btn-outline-info" />
                                        <asp:RequiredFieldValidator ID="rfvSearch" runat="server" ControlToValidate="txtEnrollmentSearch"
                                            Display="None" ErrorMessage="Please Enter Univ. Reg. No. Or Adm. No." ValidationGroup="sub">
                                        </asp:RequiredFieldValidator>

                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True"
                                            ShowSummary="False" ValidationGroup="sub" />
                                        <%-- </button>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>


                    <%--  THIS PANEL IS FOR SHOWING STUDENT INFORMATION--%>
                    <div class="row">
                        <div class="col-md-12 form-group">

                            <div class="box-body">
                                <div class="container-fluid">

                                    <asp:UpdatePanel runat="server" ID="updNcc">
                                        <ContentTemplate>

                                            <div id="divStudentInfo" style="display: block;" class="col-md-12" runat="server" visible="false">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-5 col-xs-12 form-group">
                                                            <ul class="list-group list-group-unbordered">
                                                                <li class="list-group-item"><b>Student Name :</b><a>
                                                                    <asp:Label ID="lblName" runat="server" Font-Bold="True"></asp:Label></a> </li>


                                                                <li class="list-group-item"><b>Current Semester :</b><a>
                                                                    <asp:Label ID="lblSemester" runat="server" Font-Bold="True"></asp:Label></a>
                                                                </li>


                                                                <li class="list-group-item"><b>Degree :</b><a>
                                                                    <asp:Label ID="lblDegree" runat="server" Font-Bold="True"></asp:Label></a>
                                                                    <br />
                                                                </li>



                                                                <li class="list-group-item"><b>Mobile No :</b><a>
                                                                    <asp:Label ID="lblMobNo" runat="server" Font-Bold="True"></asp:Label></a>
                                                                </li>

                                                            </ul>
                                                        </div>


                                                        <div class="col-md-5 col-xs-12 form-group">
                                                            <ul class="list-group list-group-unbordered">
                                                                <li class="list-group-item">
                                                                    <b>Univ. Reg. No. :</b>
                                                                    <a>
                                                                        <asp:Label ID="lblRegNo" runat="server" Font-Bold="True"></asp:Label></a>

                                                                </li>

                                                                <li class="list-group-item"><b>Adm. No. :</b><a>
                                                                    <asp:Label ID="lblEnrollNo" runat="server" Font-Bold="True"></asp:Label></a>
                                                                </li>


                                                                <li class="list-group-item"><b>Branch :</b><a>
                                                                    <asp:Label ID="lblBranch" runat="server" Font-Bold="True" Style="font-size: 13px;"></asp:Label></a>
                                                                    <br />
                                                                </li>

                                                                <li class="list-group-item"><b>Email ID :</b><a>
                                                                    <asp:Label ID="lblMailID" runat="server" Font-Bold="True"></asp:Label></a>
                                                                </li>




                                                            </ul>
                                                        </div>

                                                        <div class="form-group col-md-2 col-xs-12 text-center">
                                                            <asp:Image ID="imgPhoto" runat="server" Height="120px" Width="128px" />
                                                            <%--  <div style="margin-top: 50px; border: 1px solid #000; padding: 5px;">
                                                                <asp:Image ID="imgSign" runat="server" Height="50px" Width="128px" />
                                                            </div>--%>
                                                        </div>

                                                    </div>

                                                </div>

                                            
                                            </div>


                                            <div class="row" id="divNCCDetails" runat="server" visible="false">

                                                <%--<div class="row">
                                                    <div class="col-md-5 form-group">
                                                    </div>
                                                    <div class="col-md-4 form-group">
                                                        <asp:RadioButtonList runat="server" ID="rdoType" class="mylistofradiolists" RepeatDirection="Horizontal"
                                                            OnSelectedIndexChanged="rdoType_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Value="1">NCC</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                    <div class="col-md-4 form-group">
                                                    </div>
                                                </div>--%>

                                                 <div class="col-md-4 col-sm-4 col-xs-12 form-group">
                                                    <label><span style="color: red">* </span>NCC Type :</label>
                                                    <asp:DropDownList runat="server" ID="ddlNCCType" AppendDataBoundItems="true"
                                                     TabIndex="3" class="form-control">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>

                                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlNCCType"
                                        Display="None" ErrorMessage="Please Select NCC Type" InitialValue="0" ValidationGroup="submit"
                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                </div>


                                                 <div class="col-md-4 col-sm-4 col-xs-12 form-group">
                                                    <label><span style="color: red">* </span>NCC Ration :</label><br />
                                                    <asp:DropDownList runat="server" ID="ddlNCCRation" AppendDataBoundItems="true"
                                                       TabIndex="4"  class="form-control">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>

                                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlNCCRation"
                                        Display="None" ErrorMessage="Please Select NCC Ration" InitialValue="0" ValidationGroup="submit"
                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                </div>



                                                <div class="col-md-4 col-sm-4 col-xs-12 form-group">
                                                    <label><span style="color: red">* </span>
                                                        Camp Name :</label>
                                                    <asp:TextBox runat="server" ID="txtCampName" class="form-control" placeholder="Enter Camp Name"
                                              TabIndex="5"   onkeypress="return isAlphabetKey(event)"></asp:TextBox>

                                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCampName"
                                        Display="None" ErrorMessage="Please Enter Camp Name" InitialValue="0" ValidationGroup="submit"
                                        SetFocusOnError="True"></asp:RequiredFieldValidator>

                                                </div>


                                                  <div class="col-md-4 col-sm-4 col-xs-12 form-group">
                                                    <label><span style="color: red">* </span>
                                                       Location :</label>
                                                    <asp:TextBox runat="server" ID="txtLocation" class="form-control" placeholder="Enter Location"
                                                         TabIndex="6"  ></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtLocation"
                                        Display="None" ErrorMessage="Please Enter Location" InitialValue="0" ValidationGroup="submit"
                                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                </div>




                                              
                                    <div class="col-md-4 col-sm-4 col-xs-12 form-group">
                                        <span style="color: red">*</span>
                                        <label>From Date :</label>
                                        <div class="input-group">
                                            <div class="input-group-addon" id="FromDate" runat="server">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control pull-right" 
                                             TabIndex="7" placeholder="From Date" ToolTip="Please Select From Date" />
                                            <ajaxToolKit:CalendarExtender ID="cefrmdate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtFromDate" PopupButtonID="FromDate" Enabled="True">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="meFromDate" runat="server" Mask="99/99/9999"
                                                MaskType="Date" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                                TargetControlID="txtFromDate" Enabled="True" />
                                            <ajaxToolKit:MaskedEditValidator ID="mvFromDate" runat="server" ControlExtender="meFromDate"
                                                ControlToValidate="txtFromDate" Display="None" EmptyValueMessage="Please Enter From Date"
                                                ErrorMessage="Please Enter From Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="SubPercentage" />
                                      
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFromDate"
                                                Display="None" ErrorMessage="Please Enter From Date" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        </div>

                                    </div>


                                    <div class="col-md-4 col-sm-4 col-xs-12 form-group">
                                        <span style="color: red">*</span>
                                        <label>To Date :</label>

                                        <div class="input-group">
                                            <div class="input-group-addon" id="ToDate" runat="server">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtTodate" runat="server"  ValidationGroup="submit" placeholder="To Date" 
                                              TabIndex="8"  ToolTip="Please Select To Date" CssClass="form-control pull-right"  />
                                            <ajaxToolKit:CalendarExtender ID="meetodate" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtTodate" PopupButtonID="ToDate" Enabled="True">
                                            </ajaxToolKit:CalendarExtender>
                                            <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" Mask="99/99/9999" MaskType="Date"
                                                OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtTodate" />
                                            <ajaxToolKit:MaskedEditValidator ID="mvToDate" runat="server" ControlExtender="meToDate"
                                                ControlToValidate="txtTodate" Display="None" EmptyValueMessage="Please Enter To Date"
                                                ErrorMessage="Please Enter To Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="SubPercentage" />
                                            <ajaxToolKit:MaskedEditValidator ID="rfvMonth" runat="server" ControlExtender="meToDate"
                                                ControlToValidate="txtTodate" Display="None" EmptyValueMessage="Please Enter To Date"
                                                ErrorMessage="Please Enter To Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="Daywise" />
                                          
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtTodate"
                                                Display="None" ErrorMessage="Please Enter To Date" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>


                    

                                                <div class="col-md-8 col-sm-8 col-xs-12 form-group">
                                                    <label for="city">Certificate : <span style="color: red; font-size: 10px;">1) Only .jpg, .jpeg, .png , .pdf format with max 100 KB size are allowed 
                                                        2) Please Select Multiple Certificates by pressing Ctrl Key
                                                        <br /> 3) Please Ensure all Certificates Available in one Folder Only </span></label>
                                                    <asp:FileUpload ID="fuNccCertificate" runat="server" ToolTip="Select file to upload" TabIndex="9"
                                                   allowmultiple="true" CssClass="form-control" accept=".jpg,.jpeg,.pdf,.JPG,.JPEG,.PNG,.pdf,.PDF" />

                                                    <br />


                                                    <asp:Panel ID="Panel3" runat="server" ScrollBars="Auto">
                                                        <asp:Repeater ID="lvNccCertificate" runat="server">
                                                            <HeaderTemplate>
                                                                <table class="table table-bordered table-hover table-fixed">
                                                                    <thead>
                                                                        <tr class="bg-light blue">
                                                                            <th>Action</th>
                                                                            <th>Camp Name</th>
                                                                            <th>Certificate Name</th>
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
                                                                          <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif"  AlternateText='<%# Eval("NCC_FILE_NAME") %>'
                                                                          CommandArgument='<%#Eval("NCC_DOCS_NO") %>' ToolTip='<%# Eval("CAMP_NAME") %>' 
                                                                           OnClick="btnDelete_Click"  OnClientClick="return UserDeleteConfirmation();" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblName" runat="server" Text='<%#Eval("CAMP_NAME") %>' ToolTip='<%#Eval("IDNO") %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <%#Eval("NCC_FILE_NAME") %>
                                                                    </td>
                                                                    <td>
                                                                        <asp:LinkButton ID="btnDownload" runat="server" Text="Download" CommandArgument='<%#Eval("NCC_FILE_NAME") %>'
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


                                        </ContentTemplate>

                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="lvNccCertificate" />
                                            <%-- <asp:PostBackTrigger ControlID="btnSaveNext10" />--%>
                                            <%--  <asp:PostBackTrigger ControlID="lvSportsDetails" />--%>
                                            <%--  <asp:AsyncPostBackTrigger ControlID="btnSaveNext10" EventName="Click"/>--%>
                                            <%--  <asp:AsyncPostBackTrigger ControlID="btnReport" EventName="Click"/>--%>
                                        </Triggers>

                                    </asp:UpdatePanel>



                                    <div class="text-center padd-tb-10" id="divNCCBtnDetails" runat="server" visible="false">

                                        <asp:Button runat="server" ID="btnSubmit" class="btn btn-outline-info" Text="Submit" OnClick="btnSubmit_Click"
                                     OnClientClick="return FileUplodationValidation();"    TabIndex="10"   ValidationGroup="submit"  />

                                        <asp:Button ID="btnReport" runat="server" Text="Report" CssClass="btn btn-outline-primary"
                                        TabIndex="11"    Enabled="false" OnClick="btnReport_Click" ValidationGroup="Report" OnClientClick="return true;" />

                                        <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click"
                                         TabIndex="12"   Text="Cancel" CssClass="btn btn-outline-danger" />
                                        
                                                    <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List"
                                                        ShowMessageBox="True" ShowSummary="False" ValidationGroup="submit" />
                                    </div>



                                    <div class="container-fluid">
                                        <asp:ListView ID="lvNccDetails" runat="server" OnItemDataBound="lvNccDetails_ItemDataBound">
                                            <LayoutTemplate>

                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <h3>
                                                            <label class="label label-default">Student NCC Details</label></h3>
                                                    </div>
                                                </div>

                                                <div class="table-responsive">
                                                    <table id="tblDD_Details" class="table table-hover table-bordered datatable">
                                                        <tr class="bg-light-blue">
                                                            <th>Action</th>
                                                            <th style="text-align: center">Sr No.
                                                            </th>
                                                            <th style="text-align: center">NCC Type
                                                            </th>
                                                            <th style="text-align: center">NCC Ration
                                                            </th>
                                                            <th style="text-align: center">Camp Name
                                                            </th>
                                                            <th style="text-align: center">Location
                                                            </th>
                                                             <th style="text-align: center">Duration
                                                                </th>
                                                              <th style="text-align: center">Camp From
                                                               </th>
                                                            <th style="text-align: center">Camp To
                                                               </th>
                                                            <th style="text-align: center">Upload Certificates
                                                            </th>
                                                        </tr>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </table>
                                                </div>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr class="first-div">
                                                    <td style="text-align: center">
                                                        <asp:ImageButton ID="btnNccEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("NCC_NO") %>'
                                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnNccEdit_Click" />
                                                    </td>

                                                    <td style="text-align: center">
                                                        <%#Container.DataItemIndex+1%>
                                                    </td>

                                                      <td style="text-align: center">
                                                        <%# Eval("NCC_TYPE")%>
                                                    </td>
                                                     <td style="text-align: center">
                                                        <%# Eval("NCC_RATION")%>
                                                    </td>



                                                     <td style="text-align: center">
                                                        <asp:Label ID="lblCAMP_NAME" runat="server" Text='<%# Eval("CAMP_NAME")%>'   
                                                            ToolTip='<%# Eval("NCC_NO") %>'></asp:Label>
                                                    </td>

                                                   

                                                     <td style="text-align: center">
                                                        <%# Eval("CAMP_LOCATION")%>
                                                    </td>


                                                     <td style="text-align: center">
                                                        <%# Eval("CAMP_DURATION")%>
                                                    </td>


                                                   

                                                    <td style="text-align: center">
                                                        <%# Eval("CAMP_FROM_DATE")%>
                                                    </td>

                                                    <td style="text-align: center">
                                                        <%# Eval("CAMP_TO_DATE")%>
                                                    </td>
                                                    <%-- <td style="text-align: center">
                                                         <%# Eval("PARTICIPATION_LEVEL")%>
                                                        </td>
                                                        <td style="text-align: center">
                                                         <%# Eval("ACHIEVEMENT_NAME")%>
                                                        </td>--%>
                                                    <td style="text-align: center"> 
                                                         <img alt="" class="img-collapse" style="cursor: pointer" src="../images/plus.gif" />
                                                    </td>

                                                    <tr class="div-collapse">
                                                        <td colspan="5">
                                                           
                                                                <asp:ListView ID="lvNccDocsDetails" runat="server">
                                                                    <LayoutTemplate>
                                                                        <div id="demo-grid">
                                                                            <table class="table table-hover table-bordered">
                                                                                <thead>
                                                                                    <tr class="bg-success">
                                                                                        <th style="text-align: center">
                                                                                            <center>Sr No.</center>
                                                                                        </th>

                                                                                        <th style="text-align: center">Certificate Name
                                                                                        </th>
                                                                                        <th style="text-align: center">Download
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
                                                                            <td style="text-align: center">
                                                                                <%# Container.DataItemIndex + 1%>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:Label ID="lblFileName" runat="server" Text='<%# Eval("NCC_FILE_NAME")%>'></asp:Label>

                                                                            </td>

                                                                            <td style="text-align: center">
                                                                                <asp:LinkButton ID="btnDownloadFile" runat="server" Text="Download"
                                                                                    CommandArgument='<%#Eval("CAMP_NAME") %>'
                                                                                    ToolTip='<%# Eval("NCC_FILE_NAME")%>' OnClick="btnDownloadFile_Click"></asp:LinkButton>

                                                                            </td>

                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:ListView>
                                                        </td>
                                                    </tr>

                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </div>


                                </div>

                            </div>


                        </div>
                    </div>



                </div>

            </div>
        </div>
    </div>





 <%--   <!--   Calendar   -->
    <script src="../studInfo/plugin/calendar/moment.min.js"></script>
    <script src="../studInfo/plugin/calendar/bootstrap-datetimepicker.min.js"></script>
    <script src="../studInfo/plugin/inputmask/jquery.inputmask.bundle.min.js"></script>
    <!-- Bootstrap -->
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

--%>


<%--    <script>

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


    </script>--%>



    <script language="Javascript" type="text/javascript">
        function isAlphabetKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if ((charCode <= 93 && charCode >= 65) || (charCode <= 122 && charCode >= 97) || charCode == 32) {

                return true;
            }
            alert("Only (A-Z or a-z) alphabets or Characters Allowed");
            return false;

        }
    </script>



    <div id="divMsg" runat="server">
    </div>

    <script type="text/javascript">
        function UserDeleteConfirmation() {
            if (confirm("Are you sure you want to Delete this File?"))
                return true;
            else
                return false;
        }


        function FileUplodationValidation() {
            debugger;
            if (document.getElementById('ctl00_ContentPlaceHolder1_ddlNCCType').value == '0') {
                alert("Please Select NCC Type.");
                document.getElementById('ctl00_ContentPlaceHolder1_ddlNCCType').focus();
                return false;
            }
            else if (document.getElementById('ctl00_ContentPlaceHolder1_ddlNCCRation').value == '0') {
                alert('Please Select NCC Ration.');
                document.getElementById('ctl00_ContentPlaceHolder1_ddlNCCRation').focus();
                return false;
            }
            else if (document.getElementById('ctl00_ContentPlaceHolder1_txtCampName').value == '') {
                alert("Please Enter Camp Name.");
                document.getElementById('ctl00_ContentPlaceHolder1_txtCampName').focus();
                return false;
            }
            else if (document.getElementById('ctl00_ContentPlaceHolder1_txtLocation').value == '') {
                alert("Please Enter Location.");
                document.getElementById('ctl00_ContentPlaceHolder1_txtLocation').focus();
                return false;
            }
            else if (document.getElementById('ctl00_ContentPlaceHolder1_txtFromDate').value == '') {
                alert("Please Enter From Date.");
                document.getElementById('ctl00_ContentPlaceHolder1_txtFromDate').focus();
                return false;
            }
               
            else if (document.getElementById('ctl00_ContentPlaceHolder1_txtTodate').value == '') {
                alert("Please Enter To Date.");
                document.getElementById('ctl00_ContentPlaceHolder1_txtTodate').focus();
                return false;
            }

            else if (document.getElementById('ctl00_ContentPlaceHolder1_fuNccCertificate').value != '') {

                var fileName = document.getElementById('ctl00_ContentPlaceHolder1_fuNccCertificate').value;
                var ext = fileName.substr(fileName.lastIndexOf('.') + 1).toLowerCase();

                // var maxFileSize = 51200; // 50 kb
                var maxFileSize = 102400; // 100 kb
                var fileUpload = $(document.getElementById('ctl00_ContentPlaceHolder1_fuNccCertificate'));


                if (!(ext == "jpeg" || ext == "jpg" || ext == "png" || ext == "pdf")) {
                    alert("Invalid image file, must select a *.jpeg, *.jpg, *.png or *.pdf file.");
                    return false;
                }
                else {


                    if (fileUpload.val() == '') {
                        return false;
                    }
                    else {
                        if (fileUpload[0].files[0].size <= maxFileSize) {
                            //  $('#button_fileUpload').prop('disabled', false);
                            return true;
                        } else {
                            alert("Document size must not exceed 100 Kb.");
                            document.getElementById("ctl00_ContentPlaceHolder1_fuNccCertificate").value = '';
                            //  $('#lbl_uploadMessage').text('File too big !')
                            return false;
                        }
                    }

                }
            }
        }


    </script>

</asp:Content>
