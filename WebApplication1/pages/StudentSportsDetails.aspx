<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="StudentSportsDetails.aspx.cs" Inherits="ACADEMIC_StudentSportsDetails"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <!--  Calendar  -->
    <link rel="stylesheet" href="../studInfo/plugin/calendar/bootstrap-datetimepicker.min.css">
    <script src="../validation/buttonValidation.js"></script>
    <script src="../validation/validation.js"></script>



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



    <div style="z-index: 1; position: absolute; top: 10%; left: 40%;">
        <asp:UpdateProgress ID="UpdateProgress2" runat="server"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>



    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title"><b>STUDENT SPORTS DETAILS</b></h3>
                </div>

                <div class="box-body">


                    <asp:Panel ID="pnlSearch" runat="server">
                        <div class="panel panel-info">
                            <div class="box-body">
                                <div class="col-xs-12 col-md-offset-2 col-sm-offset-2">
                                    <div class="col-md-3 col-xs-12 col-sm-3" style="margin-top: 5px; text-align: right;">
                                        <label><span style="color: red">* </span>Univ. Reg. No / TAN/PAN : </label>
                                    </div>
                                    <div class="col-md-3 col-xs-12 col-sm-3">
                                        <asp:TextBox ID="txtEnrollmentSearch" runat="server" ValidationGroup="submit" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6 col-xs-12 col-sm-6" style="float: left;">

                                        <asp:Button ID="btnSearch" runat="server"
                                            OnClick="btnSearch_Click" ValidationGroup="submit" Text="Search" CssClass="btn btn-outline-info" />
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


                    <%--  THIS PANEL IS FOR SHOWING STUDENT INFORMATION--%>
                    <div class="row">
                        <div class="col-md-12 form-group">


                            <div class="box-body">
                                <div class="container-fluid">

                                    <asp:UpdatePanel runat="server" ID="updSports">
                                        <ContentTemplate>
                                            <div id="divStudentInfo" style="display: block;" class="col-md-12" runat="server" visible="false">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="col-md-5 col-xs-12 form-group">
                                                            <ul class="list-group list-group-unbordered">
                                                                <li class="list-group-item"><b>Student Name :</b><a>
                                                                    <asp:Label ID="lblName" runat="server" Font-Bold="True"></asp:Label></a> </li>


                                                                <li class="list-group-item"><b>Semester/Year :</b><a>
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

                                                                <li class="list-group-item"><b>TAN/PAN :</b><a>
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


                                            <div class="row" id="divSportsDetails" runat="server" visible="false">
                                                <div class="row">

                                                    <div class="col-md-5 form-group">
                                                    </div>
                                                    <div class="col-md-4 form-group">
                                                        <%-- <label><sup>*</sup> Please Select :</label><br />--%>

                                                        <asp:RadioButtonList runat="server" ID="rdoType" class="mylistofradiolists" RepeatDirection="Horizontal"
                                                            OnSelectedIndexChanged="rdoType_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Value="1">Sports</asp:ListItem>
                                                            <%-- <asp:ListItem Value="2">Achievement</asp:ListItem>--%>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                    <div class="col-md-4 form-group">
                                                    </div>

                                                </div>

                                                 <div class="col-md-4 col-sm-4 col-xs-12 form-group">
                                                    <label><span style="color: red">* </span>Name Of Game :</label>
                                                    <asp:DropDownList runat="server" ID="ddlGameName" AppendDataBoundItems="true"
                                                        class="form-control">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>


                                                <div class="col-md-4 col-sm-4 col-xs-12 form-group">
                                                    <label><span style="color: red">* </span>
                                                        <asp:Label ID="lblNameOf" runat="server"></asp:Label></label>
                                                    <asp:TextBox runat="server" ID="txtNameOfGameOrAchievement" class="form-control" placeholder="Enter Name of Game / Achievement"
                                              onkeypress="return isAlphabetKey(event)" MaxLength="15"></asp:TextBox>

                                                </div>

                                                <div class="col-md-4 col-sm-4 col-xs-12 form-group">
                                                    <label><span style="color: red">* </span>
                                                        <asp:Label ID="lblDate" runat="server"></asp:Label></label>
                                                    <div class='input-group date' id='SportsDate'>

                                                        <asp:TextBox runat="server" ID="txtDate" class="form-control" MaxLength="10" data-inputmask="'mask': '99/99/9999'"></asp:TextBox>


                                                        <span class="input-group-addon">
                                                            <span class="glyphicon glyphicon-calendar"></span>
                                                        </span>

                                                    </div>
                                                </div>



                            <%--  <div class="col-md-4 col-sm-4 col-xs-12 form-group">
                                <label><span style="color: red;">*</span> <asp:Label ID="lblDate" runat="server"></asp:Label> </label>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i id="dvcal1" runat="server" class="fa fa-calendar"></i>
                                    </div>
                                     <cc1:ToolkitScriptManager ID="toolScriptManageer1" runat="server"></cc1:ToolkitScriptManager> 
                                    <asp:TextBox ID="txtDate" runat="server" ValidationGroup="submit" onpaste="return false;"
                                        TabIndex="3" ToolTip="Please Enter Date" CssClass="form-control" Style="z-index: 0;" />
                                    <ajaxToolKit:CalendarExtender ID="ceStartDate" runat="server" Format="dd/MM/yyyy"
                                     OnClientDateSelectionChanged="checkDate"   TargetControlID="txtDate" PopupButtonID="dvcal1" />
                                    <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ControlToValidate="txtDate"
                                        Display="None" ErrorMessage="Please Enter Date" SetFocusOnError="True"
                                        ValidationGroup="submit" />
                                    <ajaxToolKit:MaskedEditExtender ID="meeStartDate" runat="server" OnInvalidCssClass="errordate"
                                        TargetControlID="txtDate" Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date"
                                        DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                    <ajaxToolKit:MaskedEditValidator ID="mevStartDate" runat="server" ControlExtender="meeStartDate"
                                        ControlToValidate="txtDate" EmptyValueMessage="Please Enter Date"
                                        InvalidValueMessage="Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                        TooltipMessage="Please Enter Date" EmptyValueBlurredText="Empty" 
                                        InvalidValueBlurredMessage="Invalid Date"
                                        ValidationGroup="submit" SetFocusOnError="True" />
                                </div>
                               </div>--%>



                                                <div class="col-md-4 col-sm-4 col-xs-12 form-group">
                                                    <label><span style="color: red">* </span>
                                                        <asp:Label ID="lblVenue" runat="server" Text="Venue :"></asp:Label></label>
                                                    <asp:TextBox runat="server" ID="txtVenue" class="form-control" placeholder="Enter Venue" MaxLength="150"></asp:TextBox>

                                                </div>

                                                <div class="col-md-4 col-sm-4 col-xs-12 form-group">
                                                    <label><span style="color: red">* </span>Level Of Participation :</label><br />
                                                    <asp:DropDownList runat="server" ID="ddlLevelOfParticipation" AppendDataBoundItems="true"
                                                        class="form-control">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>

                                                <div class="col-md-4 col-sm-4 col-xs-12 form-group">
                                                    <label><span style="color: red">* </span>Achievement :</label>
                                                    <asp:DropDownList runat="server" ID="ddlAchievement" AppendDataBoundItems="true"
                                                        class="form-control">
                                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>


                                                <div class="col-md-8 col-sm-8 col-xs-12 form-group">
                                                    <label for="city">Certificate <span style="color: red; font-size: 10px;">1) Only .jpg, .jpeg, .png , .pdf format with max 50 KB size are allowed 2) Please Select Multiple Certificates by pressing Ctrl Key
                                                        <br /> 3) Please Ensure all Certificates Available in one Folder Only </span></label>
                                                    <asp:FileUpload ID="fuSportsCertificate" runat="server" ToolTip="Select file to upload"
                                                   allowmultiple="true"     CssClass="form-control" accept=".jpg,.jpeg,.pdf,.JPG,.JPEG,.PNG,.pdf,.PDF" />

                                                    <br />


                                                    <asp:Panel ID="Panel3" runat="server" ScrollBars="Auto">
                                                        <asp:Repeater ID="lvSportsCertificate" runat="server">
                                                            <HeaderTemplate>
                                                                <table class="table table-bordered table-hover table-fixed">
                                                                    <thead>
                                                                        <tr class="bg-light blue">
                                                                            <th>Action</th>
                                                                            <th>Game Name
                                                                            </th>
                                                                            <th>Certificate Name
                                                                            </th>
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
                                                                          <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif"  AlternateText='<%# Eval("SPORTS_FILE_NAME") %>'
                                                                          CommandArgument='<%#Eval("SPORTS_DOCS_NO") %>' ToolTip='<%# Eval("SPORTS_NAME") %>' 
                                                                           OnClick="btnDelete_Click"  OnClientClick="return UserDeleteConfirmation();" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblName" runat="server" Text='<%#Eval("SPORTS_NAME") %>' ToolTip='<%#Eval("IDNO") %>'></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <%#Eval("SPORTS_FILE_NAME") %>
                                                                    </td>
                                                                    <td>
                                                                        <asp:LinkButton ID="btnDownload" runat="server" Text="Download" CommandArgument='<%#Eval("SPORTS_FILE_NAME") %>'
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
                                            <asp:PostBackTrigger ControlID="lvSportsCertificate" />
                                            <%-- <asp:PostBackTrigger ControlID="btnSaveNext10" />--%>
                                            <%--  <asp:PostBackTrigger ControlID="lvSportsDetails" />--%>
                                            <%--  <asp:AsyncPostBackTrigger ControlID="btnSaveNext10" EventName="Click"/>--%>
                                            <%--  <asp:AsyncPostBackTrigger ControlID="btnReport" EventName="Click"/>--%>
                                        </Triggers>

                                    </asp:UpdatePanel>



                                    <div class="text-center padd-tb-10" id="divSportsBtnDetails" runat="server" visible="false">

                                        <asp:Button runat="server" ID="btnSaveNext10" class="btn btn-outline-info" Text="Submit" OnClick="btnSaveNext10_Click"
                                            ValidationGroup="Sports" OnClientClick="return step10Click_validation()" />

                                        <asp:Button ID="btnReport" runat="server" Text="Report" CssClass="btn btn-outline-primary"
                                            Enabled="false" OnClick="btnReport_Click" ValidationGroup="Report" OnClientClick="return true;" />

                                        <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click"
                                            Text="Cancel" CssClass="btn btn-outline-danger" />

                                    </div>



                                    <div class="container-fluid">
                                        <asp:ListView ID="lvSportsDetails" runat="server" OnItemDataBound="lvSportsDetails_ItemDataBound">
                                            <LayoutTemplate>

                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <h3>
                                                            <label class="label label-default">Student Sports Details</label></h3>
                                                    </div>
                                                </div>

                                                <div class="table-responsive">
                                                    <table id="tblDD_Details" class="table table-hover table-bordered datatable">
                                                        <tr class="bg-light-blue">
                                                            <th>Action</th>
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
                                                            <%-- <th style="text-align: center">Participation Level
                                                                </th>
                                                              <th style="text-align: center">Achievement
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
                                                        <asp:ImageButton ID="btnSportsEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("SPORTS_NO") %>'
                                                            AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnSportsEdit_Click" />
                                                    </td>

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
                                                           
                                                                <asp:ListView ID="lvSportsDocsDetails" runat="server">
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
                                                                                <asp:Label ID="lblFileName" runat="server" Text='<%# Eval("SPORTS_FILE_NAME")%>'></asp:Label>

                                                                            </td>

                                                                            <td style="text-align: center">
                                                                                <asp:LinkButton ID="btnDownloadFile" runat="server" Text="Download"
                                                                                    CommandArgument='<%#Eval("SPORTS_NAME") %>'
                                                                                    ToolTip='<%# Eval("SPORTS_FILE_NAME")%>' OnClick="btnDownloadFile_Click"></asp:LinkButton>

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





    <!--   Calendar   -->
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


<%--    <script>
        function checkDate(sender, args) {
            if (sender._selectedDate < new Date()) {
                alert("You cannot select a day earlier than today!");
                sender._selectedDate = new Date();
                // set the date back to the current date
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        }

    </script>--%>



    <script>

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
    </script>

</asp:Content>
