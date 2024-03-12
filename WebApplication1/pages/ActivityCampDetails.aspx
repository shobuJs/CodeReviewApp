<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="ActivityCampDetails.aspx.cs" Inherits="ACADEMIC_ActivityCampDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .not-allowed {
            cursor: not-allowed !important;
            opacity: .4;
        }
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

    <div>
        <asp:UpdateProgress ID="updProg" runat="server"
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

    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title"><b>STUDENT CAMP DETAILS</b></h3>
                    <div style="color: RED; font-weight: bold" class="pull-right">
                        <span>Note : * marked fields are mandatory</span>
                    </div>
                </div>

                <div class="box-body">

                    <div class="box-body">
                        <div class="form-group col-md-12" id="trstype" runat="server">
                            <asp:RadioButtonList ID="rdoactivity" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                                AppendDataBoundItems="True" OnSelectedIndexChanged="rdoactivity_SelectedIndexChanged">
                                <asp:ListItem Value="1" Selected="True" style="margin: 20px">NCC</asp:ListItem>
                                <asp:ListItem Value="2" style="margin: 20px">NSS</asp:ListItem>
                                <asp:ListItem Value="3" style="margin: 20px">CLUB</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12 form-group">
                            <div class="box-body">
                                <div class="container-fluid">
                                    <div class="row" id="divActivityDetails" runat="server" visible="false">

                                        <div class="col-md-4 col-sm-4 col-xs-12 form-group" runat="server" id="divncc">
                                            <label><span style="color: red">* </span>NCC Type :</label>
                                            <asp:DropDownList runat="server" ID="ddlNCCType" AppendDataBoundItems="true"
                                                TabIndex="3" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlNCCType_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlNCCType"
                                                Display="None" ErrorMessage="Please Select NCC Type" ValidationGroup="submit"></asp:RequiredFieldValidator>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlNCCType"
                                                Display="None" ErrorMessage="Please Select NCC Type" InitialValue="0" ValidationGroup="submit"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="col-md-4 col-sm-4 col-xs-12 form-group" runat="server" visible="false" id="divnss">
                                            <label><span style="color: red">* </span>NSS Type :</label>
                                            <asp:DropDownList runat="server" ID="ddlNSSType" AppendDataBoundItems="true"
                                                TabIndex="3" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlNSSType_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlNSSType"
                                                Display="None" ErrorMessage="Please Select NSS Type" InitialValue="0" ValidationGroup="submit"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="col-md-4 col-sm-4 col-xs-12 form-group" runat="server" visible="false" id="divclub">
                                            <label><span style="color: red">* </span>CLUB Type :</label>
                                            <asp:DropDownList runat="server" ID="ddlCLUBType" AppendDataBoundItems="true"
                                                TabIndex="3" class="form-control" OnSelectedIndexChanged="ddlCLUBType_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlCLUBType"
                                                Display="None" ErrorMessage="Please Select CLUB Type" InitialValue="0" ValidationGroup="submit"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="col-md-4 col-sm-4 col-xs-12 form-group" runat="server" id="divration" visible="false">
                                            <label><span style="color: red">* </span>NCC Ration :</label><br />
                                            <asp:DropDownList runat="server" ID="ddlNCCRation" AppendDataBoundItems="true"
                                                TabIndex="4" class="form-control" OnSelectedIndexChanged="ddlNCCRation_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlNCCRation"
                                                Display="None" ErrorMessage="Please Select NCC Ration" InitialValue="0" ValidationGroup="submit"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="col-md-4 col-sm-4 col-xs-12 form-group">
                                            <label>
                                                <span style="color: red">* </span>
                                                Camp Name :</label>
                                            <asp:TextBox runat="server" ID="txtCampName" class="form-control" placeholder="Enter Camp Name"
                                                TabIndex="5" onkeypress="return isAlphabetKey(event)"></asp:TextBox>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtCampName"
                                                Display="None" ErrorMessage="Please Enter Camp Name" ValidationGroup="submit"></asp:RequiredFieldValidator>

                                        </div>

                                        <div class="col-md-4 col-sm-4 col-xs-12 form-group">
                                            <label>
                                                <span style="color: red">* </span>
                                                Location :</label>
                                            <asp:TextBox runat="server" ID="txtLocation" class="form-control" placeholder="Enter Location"
                                                TabIndex="6"></asp:TextBox>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtLocation"
                                                Display="None" ErrorMessage="Please Enter Camp Location" ValidationGroup="submit"></asp:RequiredFieldValidator>

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
                                                <asp:TextBox ID="txtTodate" runat="server" ValidationGroup="submit" placeholder="To Date"
                                                    TabIndex="8" ToolTip="Please Select To Date" CssClass="form-control pull-right" />
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

                                        <div class="col-md-4 col-sm-4 col-xs-12 form-group">
                                            <label>
                                                <span style="color: red">* </span>
                                                Camp/Event Details :</label>
                                            <asp:TextBox runat="server" ID="txtcampdetail" class="form-control" placeholder="Enter Event Details"
                                                TabIndex="6" TextMode="MultiLine" MaxLength="50"></asp:TextBox>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtcampdetail"
                                                Display="None" ErrorMessage="Please Enter Camp/Event Details" ValidationGroup="submit"></asp:RequiredFieldValidator>

                                        </div>
                                        <br />

                                        <div class="col-md-8 col-sm-8 col-xs-12 form-group">
                                            <label for="city">
                                                Certificate : <span style="color: red; font-size: 10px;">1) Only .jpg, .jpeg, .png , .pdf format with max 100 KB size are allowed 
                                                        2) Please Select Multiple Certificates by pressing Ctrl Key
                                                        <br />
                                                    3) Please Ensure all Certificates Available in one Folder Only </span>
                                            </label>
                                            <asp:FileUpload ID="fuNccCertificate" runat="server" ToolTip="Select file to upload" TabIndex="9"
                                                allowmultiple="true" CssClass="form-control" accept=".jpg,.jpeg,.pdf,.JPG,.JPEG,.PNG,.pdf,.PDF" />
                                        </div>


                                        <div class="col-md-12">
                                            <asp:Panel ID="pnlstuddata" runat="server">
                                                <asp:ListView ID="lvstudactivdata" runat="server">
                                                    <LayoutTemplate>
                                                        <div id="demo-grid">
                                                            <h4>
                                                                <label class="label label-default">ACTIVITY WISE STUDENT LIST</label></h4>
                                                            <table class="table table-bordered table-hover table-fixed">
                                                                <thead>
                                                                    <tr class="bg-light-blue">
                                                                        <th>
                                                                            <asp:CheckBox ID="cbHead" runat="server" onclick="return totAll(this)" /></th>
                                                                        <th style="text-align: center">Sr.No</th>
                                                                        <th style="text-align: center">Registration No.</th>
                                                                        <th style="text-align: center">Student Name</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server"></tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <%--<asp:CheckBox ID="cbRow"  Enabled='<%# Eval("ACTIVITY_TYPE").ToString()== null ?true : false %>'  Checked='<%# Eval("ACTIVITY_TYPE").ToString() == null ?false : true %>'  runat="server" ToolTip='<%# Eval("IDNO")%>' />--%>
                                                                <asp:CheckBox ID="cbRow" runat="server" ToolTip='<%# Eval("IDNO")%>' />
                                                                <asp:HiddenField ID="hdfTempIdNo" runat="server" Value='<%# Eval("IDNO")%>' />

                                                            </td>
                                                            <td style="text-align: center;"><%# Container.DataItemIndex+1 %></td>
                                                            <td style="text-align: center"><%# Eval("REGNO")%></td>
                                                            <td style="text-align: center"><%# Eval("STUDNAME")%></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </asp:Panel>
                                        </div>

                                        <div class="col-md-12 form-group text-center">
                                            <asp:Button runat="server" ID="btnSubmit" class="btn btn-outline-info" Text="Submit" OnClick="btnSubmit_Click" ValidationGroup="submit" Enabled="false" />
                                            <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel" CssClass="btn btn-outline-danger" />
                                            <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List"
                                                ShowMessageBox="True" ShowSummary="False" ValidationGroup="submit" />
                                        </div>

                                        <div class="container-fluid">
                                            <asp:ListView ID="lvCampDetails" runat="server" OnItemDataBound="lvCampDetails_ItemDataBound1">
                                                <LayoutTemplate>
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <h4>
                                                                <label class="label label-default">Student Camp Activity Details</label></h4>
                                                        </div>
                                                    </div>
                                                    <div class="table-responsive">
                                                        <table id="tblDD_Details" class="table table-hover table-bordered datatable">
                                                            <tr class="bg-light-blue">
                                                                <th>Action</th>
                                                                <th style="text-align: center">Registration No.
                                                                </th>
                                                                <th style="text-align: center">Activity Type
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
                                                                <%-- <th style="text-align: center">Camp Details
                                                            </th>--%>
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
                                                            <asp:ImageButton ID="btnNccEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("IDNO") %>'
                                                                AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnNccEdit_Click" CommandName='<%# Eval("ACTIVITY_TYPE_NO") %>' />
                                                        </td>

                                                        <td style="text-align: center">                       
                                                            <%# Eval("REGNO")%>                                                           
                                                        </td>

                                                        <td style="text-align: center">
                                                            <%# Eval("ACTIVITY_TYPE_NAME")%>
                                                        </td>

                                                        <td style="text-align: center">
                                                            <asp:Label ID="lblCAMP_NAME" runat="server" Text='<%# Eval("CAMP_NAME") %>'
                                                                ToolTip='<%# Eval("IDNO") %>'></asp:Label>
                                                        </td>

                                                        <td style="text-align: center">
                                                            <%# Eval("CAMP_LOCATION")%>
                                                        </td>

                                                        <td style="text-align: center">
                                                            <%# Eval("CAMP_DURATION")%>
                                                        </td>

                                                        <td style="text-align: center">
                                                            <%# Eval("FROM_DATE")%>
                                                        </td>

                                                        <td style="text-align: center">
                                                            <%# Eval("TO_DATE")%>
                                                        </td>

                                                        <%-- <td style="text-align: center">
                                                        <%# Eval("CAMP_DETAILS")%>
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
                                                                                <asp:Label ID="lblFileName" runat="server" Text='<%# Eval("ACTIVITY_FILE_NAME")%>'></asp:Label>

                                                                            </td>

                                                                            <td style="text-align: center">
                                                                                <asp:LinkButton ID="btnDownloadFile" runat="server" Text="Download"
                                                                                    CommandArgument='<%#Eval("CAMP_NAME") %>' CommandName='<%#Eval("IDNO") %>'
                                                                                    ToolTip='<%# Eval("ACTIVITY_FILE_NAME")%>' OnClick="btnDownloadFile_Click"></asp:LinkButton>
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
    </div>

    <%--Checkbox--%>

    <script language="javascript" type="text/javascript">
        function totAll(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (headchk.checked == true) {
                        if (e.disabled == false)
                            e.checked = true;
                    }
                    else
                        e.checked = false;
                }
            }
        }
    </script>


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


