<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Student_Industrial_Details.aspx.cs" Inherits="ACADEMIC_Student_Industrial_Details" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%-- <script src="../Content/jquery.js" type="text/javascript"></script> --%>

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
                // alert('hai');
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

    <script>
        $(document).ready(function () {
            $("#<%=txt_StuentParticipated.ClientID%>").keyup(function () {
                this.value = this.value.replace(/[^0-9\.]/g, '');
            });
            $("#<%=txt_TeacherParticipated.ClientID%>").keyup(function () {
                this.value = this.value.replace(/[^0-9\.]/g, '');
            });
        });
    </script>


    <div class="row">
        <div class="col-md-12">

            <!-- Custom Tabs -->
            <div class="nav-tabs-custom" id="Tabs" role="tabpanel">

                <ul class="nav nav-tabs" role="tablist">

                    <li><a href="#visit" aria-controls="visit" role="tab" data-toggle="tab">Industrial Visit</a></li>
                    <li><a href="#link" aria-controls="link" role="tab" data-toggle="tab">MoU Details</a></li>

                </ul>

                <div class="tab-content">

                    <div role="tabpanel" class="tab-pane active" id="visit">

                        <div style="z-index: 1; position: fixed; top: 50%; left: 600px;">
                            <asp:UpdateProgress ID="updProg" runat="server"
                                DynamicLayout="true" DisplayAfter="0">
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
                                <div class="box box-info">
                                    <div class="box-header with-border">
                                        <h3 class="box-title">INDUSTRIAL VISIT</h3>
                                    </div>
                                    <div style="color: Red; font-weight: bold">
                                        &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                                    </div>
                                    <!-- /.box-header -->
                                    <!-- form start -->

                                    <div class="box-body">
                                        <div class="col-md-12">
                                            <div class="form-group col-md-4" runat="server">
                                                <label><span style="color: red;">*</span> Name of the Industry</label>
                                                <asp:TextBox ID="txtNameIndustry" runat="server" TabIndex="1" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtNameIndustry"
                                                    Display="None" ErrorMessage="Please Enter Name of the Industry" SetFocusOnError="true" ValidationGroup="AddIndustrial">
                                                </asp:RequiredFieldValidator>
                                                <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List" ShowMessageBox="True"
                                                    ShowSummary="False" ValidationGroup="AddIndustrial" />
                                            </div>
                                            <div class="form-group col-md-4" id="trAddress" runat="server">
                                                <label><%--<span style="color: red;">*</span>--%> Address of the Industry</label>
                                                <asp:TextBox ID="txtAdresIndustry" TabIndex="2" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-md-4" id="trProjectdetails" runat="server">
                                                <label><%--<span style="color: red;">*</span>--%>Details/Purpose of Visit</label>
                                                <asp:TextBox ID="txtVisitDetails" TabIndex="3" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-md-4" runat="server">
                                                <label>
                                                    <%--<span style="color: red;">*</span>--%>Date of the Visit
                                  <%-- <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meeFromDate" ControlToValidate="txtFromDate"
                                       IsValidEmpty="False" EmptyValueMessage="Please Enter From date " InvalidValueMessage="From date is invalid"
                                       InvalidValueBlurredMessage="*" Display="Dynamic" ValidationGroup="report" />--%>
                                                </label>

                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtDateofVisit" TabIndex="4" runat="server" autocomplete="off" CssClass="form-control" />

                                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtDateofVisit" PopupButtonID="imgCalFromDate" Enabled="true" EnableViewState="true" />
                                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtDateofVisit"
                                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                        AcceptNegative="none" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDateofVisit"
                                                        ErrorMessage="Please Select Date of the Visit" ValidationGroup="Show" Display="None"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group col-md-4" runat="server">
                                                <label><%--<span style="color: red;">*</span>--%>No. of Students</label>
                                                <asp:TextBox ID="txtnoStudents" runat="server" TabIndex="5" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-md-4" runat="server">
                                                <label><%--<span style="color: red;">*</span>--%>No. of Faculties Visited</label>
                                                <asp:TextBox ID="txtnoFaculties" runat="server" TabIndex="6" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-md-4" runat="server" visible="false">
                                                <label><%--<span style="color: red;">*</span>--%> Session</label>
                                                <asp:DropDownList ID="ddlSession" runat="server" TabIndex="7" AppendDataBoundItems="True" AutoPostBack="true" CssClass="form-control">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlSession"
                                                    Display="None" ErrorMessage="Please Select Session" InitialValue="0" SetFocusOnError="True"
                                                    ValidationGroup="Show"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-md-4">
                                                <label>College/School Name</label>
                                                <asp:DropDownList ID="ddlCollege" runat="server" TabIndex="7" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" CssClass="form-control">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlCollege"
                                                    Display="None" ErrorMessage="Please Select College/School Name" InitialValue="0" SetFocusOnError="True"
                                                    ValidationGroup="Show"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-md-4">
                                                <label><%--<span style="color: red;">*</span>--%> Branch</label>
                                                <asp:DropDownList ID="ddlBranch" runat="server" TabIndex="7" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" CssClass="form-control">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlBranch"
                                                    Display="None" ErrorMessage="Please Select Branch" InitialValue="0" SetFocusOnError="True"
                                                    ValidationGroup="Show"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="form-group col-md-4">
                                                <label>Semester/Year</label>
                                                <asp:DropDownList ID="ddlSemester" runat="server" TabIndex="8" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" CssClass="form-control">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-md-4">
                                                <label>Section</label>
                                                <asp:DropDownList ID="ddlSection" runat="server" TabIndex="9" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" CssClass="form-control">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="form-group col-md-4">
                                                <label><%--<span style="color: red;">*</span>--%> Admission Batch</label>
                                                <asp:DropDownList ID="ddlAdmBatch" runat="server" TabIndex="10" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlAdmBatch_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlAdmBatch"
                                                    Display="None" ErrorMessage="Please Select Admission Batch" InitialValue="0"
                                                    SetFocusOnError="True" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-8 col-sm-8 col-xs-12 form-group">
                                            <%--<div class="col-md-12">--%>
                                            <label for="city">
                                                Supportive Documents Attachment <span style="color: red; font-size: 10px;">1) Only .jpg, .jpeg, .png , .pdf format with max 1 MB size are allowed                    
                                                        2) Please Select Multiple Certificates by pressing Ctrl Key
                                                        <br />
                                                    3) Please Ensure all Certificates Available in one Folder Only </span>
                                            </label>
                                            <%-- </div>--%>
                                            <asp:FileUpload ID="FuIndustrial" runat="server" TabIndex="11" ToolTip="Select file to upload"
                                                AllowMultiple="true" CssClass="form-control" accept=".jpg,.jpeg,.pdf,.JPG,.JPEG,.PNG,.pdf,.PDF" />

                                            <br />

                                            <asp:Panel ID="Panel4" runat="server" ScrollBars="Auto">
                                                <asp:Repeater ID="Repeater3" runat="server">
                                                    <HeaderTemplate>
                                                        <table class="table table-bordered table-hover table-fixed">
                                                            <thead>
                                                                <tr class="bg-light blue">
                                                                    <th>Action</th>
                                                                    <th>Project Name
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
                                                                <%--<asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif"  AlternateText='<%# Eval("SPORTS_FILE_NAME") %>'
                                                                          CommandArgument='<%#Eval("SPORTS_DOCS_NO") %>' ToolTip='<%# Eval("SPORTS_NAME") %>' 
                                                                           OnClick="btnDelete_Click"  OnClientClick="return UserDeleteConfirmation();" />--%>
                                                            </td>
                                                            <td>
                                                                <%-- <asp:Label ID="lblName" runat="server" Text='<%#Eval("SPORTS_NAME") %>' ToolTip='<%#Eval("IDNO") %>'></asp:Label>--%>
                                                            </td>
                                                            <td>
                                                                <%--  <%#Eval("SPORTS_FILE_NAME") %>--%>
                                                            </td>
                                                            <td>
                                                                <%--<asp:LinkButton ID="btnDownload" runat="server" Text="Download" CommandArgument='<%#Eval("SPORTS_FILE_NAME") %>'
                                                                            OnClick="btnDownload_Click"></asp:LinkButton>--%>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </tbody> </table>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                            </asp:Panel>

                                        </div>
                                        <div class="box-footer col-md-12 ">
                                            <p class="text-center">
                                                <asp:Button ID="btnSubmitvisit" runat="server" TabIndex="12" CssClass="btn btn-outline-info" Text="Submit" ValidationGroup="AddIndustrial" OnClick="btnSubmitvisit_Click" />
                                                <asp:Button ID="btnCancelvisit" runat="server" TabIndex="13" Text="Cancel" CssClass="btn btn-outline-danger" OnClick="btnCancelvisit_Click" />
                                            </p>

                                        </div>
                                        <div class="col-md-12">
                                            <asp:ListView ID="lvStudentList" runat="server">
                                                <LayoutTemplate>
                                                    <div>
                                                        <table class="table table-hover table-bordered" style="margin-bottom: 0px; width: 100%">
                                                            <thead>
                                                                <tr class="bg-light-blue">
                                                                    <th style="width: 30%">
                                                                        <asp:CheckBox ID="chkIdentityCard" runat="server" Text="Select All" onClick="totAll(this);" ToolTip="Select or Deselect All Records" />
                                                                    </th>
                                                                    <th style="width: 20%">Univ Reg. No.
                                                                    </th>
                                                                    <th style="width: 50%">Student Name
                                                                    </th>
                                                                    <th></th>
                                                                </tr>
                                                            </thead>
                                                            <%-- <tbody>
                                                            <tr id="itemPlaceholder" runat="server" />
                                                        </tbody>--%>
                                                        </table>
                                                    </div>
                                                    <div class="listview-container" style="overflow-y: scroll; overflow-x: hidden; height: 300px;">
                                                        <div id="demo-grid" class="vista-grid">
                                                            <table class="table table-hover table-bordered table-responsive">
                                                                <tbody>
                                                                    <tr id="itemPlaceholder" runat="server" />
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </LayoutTemplate>
                                                <EmptyDataTemplate>
                                                    <div align="left" class="info">
                                                        There are no Students to display
                                                    </div>
                                                </EmptyDataTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td style="width: 30%">
                                                            <asp:CheckBox ID="chkReport" runat="server" ToolTip='<%#Eval("IDNO") %>' />
                                                            <%-- <asp:HiddenField ID="hidIdNo" runat="server" Value='<%# Eval("IDNO") %>' />--%>
                                                        </td>
                                                        <td style="width: 20%">
                                                            <%#Eval("REGNO") %>
                                                        </td>
                                                        <td style="width: 50%">
                                                            <asp:Label ID="lblstudname" runat="server" Text='<%# Eval("STUDENTNAME") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>

                                        </div>
                                        <div id="divIndustrialVisit" runat="server" style="display: block; transition: 0.8s;" class="col-md-12">
                                            <asp:ListView ID="lvIndustrialVisit" runat="server" OnItemDataBound="lvIndustrialVisit_ItemDataBound">
                                                <LayoutTemplate>
                                                    <div>
                                                        <table class="table table-hover table-bordered table-striped">
                                                            <thead>

                                                                <tr class="bg-light-blue">
                                                                    <th>Edit</th>
                                                                    <th>Industry Name
                                                                    </th>
                                                                    <th>Industry Address
                                                                    </th>
                                                                    <th>Visit Date
                                                                    </th>
                                                                    <th>No of Faculties
                                                                    </th>
                                                                    <th>Branch
                                                                    </th>
                                                                    <th>Semester
                                                                    </th>
                                                                    <th>Admission year
                                                                    </th>
                                                                    <th>Upload Certificates
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
                                                    <tr class="first-div">
                                                        <%--<tr>--%>
                                                        <td>
                                                            <center>
                                                                <asp:ImageButton ID="btnEditforVisit" runat="server" AlternateText="Edit" CommandArgument='<%# Eval("VISITNO")+"$"+Eval("INDUSTRY_NAME")+"$"+Eval("ADDRESS_INDUSTRY")+"$"+Eval("VISIT_DETAILS")+"$"+Eval("VISIT_DATE")+"$"+Eval("STUDENTSNO_VISIT")+"$"+Eval("FACULTIESNO_VISIT")+"$"+Eval("BRANCHNO")+"$"+Eval("SEMESTERNO")+"$"+Eval("SECTIONNO")+"$"+Eval("ADM_YEAR")+"$"+Eval("DEGREENO")%>' ImageUrl="~/images/edit.gif" OnClick="btnEditforVisit_Click" ToolTip="Edit" />
                                                            </center>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblIndustryname" runat="server" Text='<%# Eval("INDUSTRY_NAME") %>'
                                                                ToolTip='<%# Eval("VISITNO") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <%# Eval("ADDRESS_INDUSTRY") %>
                                                        </td>
                                                        <td>
                                                            <%# Eval("VISIT_DATE") %>
                                                        </td>

                                                        <td>
                                                            <%# Eval("FACULTIESNO_VISIT") %>
                                                        </td>
                                                        <td>
                                                            <%# Eval("BRANCH") %>
                                                        </td>
                                                        <td>
                                                            <%# Eval("SEMESTERNAME") %>
                                                        </td>
                                                        <td>
                                                            <%# Eval("ADM_YEAR") %>
                                                        </td>
                                                        <td style="text-align: center">
                                                            <img alt="" class="img-collapse" style="cursor: pointer" src="../images/plus.gif" />
                                                        </td>

                                                        <tr class="div-collapse">
                                                            <td colspan="5">

                                                                <asp:ListView ID="lvIndustrialdocs" runat="server">
                                                                    <LayoutTemplate>
                                                                        <div id="demo-grid">
                                                                            <table class="table table-hover table-bordered">
                                                                                <thead>
                                                                                    <tr class="bg-success">
                                                                                        <th>Action
                                                                                        </th>
                                                                                        <th style="text-align: center">
                                                                                            <center>Sr No.</center>
                                                                                        </th>
                                                                                        <th style="text-align: center">File Name
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
                                                                            <td>
                                                                                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" AlternateText='<%# Eval("INDUSTRIAL_VISIT_FILE_NAME")%>'
                                                                                    CommandArgument='<%#Eval("INDUS_DOCS_NO") %>' ToolTip='<%#Eval("INDUSTRY_NAME") %>' CommandName='<%# Eval("VISITNO") %>'
                                                                                    OnClick="btnDelete_Click" OnClientClick="return UserDeleteConfirmation();" />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <%# Container.DataItemIndex + 1%>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:Label ID="lblFileName" runat="server" Text='<%# Eval("INDUSTRIAL_VISIT_FILE_NAME")%>'></asp:Label>

                                                                            </td>

                                                                            <td style="text-align: center">
                                                                                <asp:LinkButton ID="btnDownloadFile" runat="server" Text="Download"
                                                                                    CommandArgument='<%#Eval("INDUSTRY_NAME") %>'
                                                                                    ToolTip='<%# Eval("INDUSTRIAL_VISIT_FILE_NAME")%>' OnClick="btnDownloadFile_Click"></asp:LinkButton>

                                                                            </td>

                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:ListView>
                                                            </td>
                                                        </tr>
                                                        <%--</tr>--%>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="lvIndustrialVisit" />
                                <asp:PostBackTrigger ControlID="ddlBranch" />
                                <asp:PostBackTrigger ControlID="ddlSemester" />
                                <asp:PostBackTrigger ControlID="ddlSection" />
                                <asp:PostBackTrigger ControlID="ddlAdmBatch" />
                                <asp:PostBackTrigger ControlID="btnSubmitvisit" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>

                    <div role="tabpanel" class="tab-pane" id="link">

                        <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server"
                                DynamicLayout="true" DisplayAfter="0">
                                <ProgressTemplate>
                                    <div style="width: 120px; padding-left: 5px">
                                        <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                                        <p class="text-success"><b>Loading..</b></p>
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </div>

                        <asp:UpdatePanel ID="updlink" runat="server">
                            <ContentTemplate>
                                <div class="box box-info">
                                    <div class="box-header with-border">
                                        <h3 class="box-title">MoU Details</h3>
                                        <%-- <h3 class="box-title">INDUSTRIAL LINK</h3>--%>
                                    </div>
                                    <div style="color: Red; font-weight: bold">
                                        &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                                    </div>
                                    <!-- /.box-header -->
                                    <!-- form start -->

                                    <div class="box-body">
                                        <div class="col-md-12">
                                            <div class="form-group col-md-4" runat="server">
                                                <label><span style="color: red;">*</span> Name of the Company</label>
                                                <asp:TextBox ID="txtCompany" runat="server" TabIndex="1" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCompany"
                                                    Display="None" ErrorMessage="Please Enter Name of the Company" SetFocusOnError="true" ValidationGroup="AddIndustrialLink">
                                                </asp:RequiredFieldValidator>
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True"
                                                    ShowSummary="False" ValidationGroup="AddIndustrialLink" />
                                            </div>
                                            <div class="form-group col-md-4" runat="server">
                                                <label><%--<span style="color: red;">*</span>--%> Address of the Company</label>
                                                <asp:TextBox ID="txtAddresCompany" runat="server" TabIndex="2" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-md-4" runat="server">
                                                <label>
                                                    <%--<span style="color: red;">*</span>--%>MOU From
                                                </label>

                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtMouFrom" runat="server" TabIndex="3" autocomplete="off" CssClass="form-control" />

                                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtMouFrom" PopupButtonID="imgCalFromDate" Enabled="true" EnableViewState="true" />
                                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtMouFrom"
                                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                        AcceptNegative="none" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtMouFrom"
                                                        ErrorMessage="Please Select Date of the Visit" ValidationGroup="Show" Display="None"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group col-md-4" runat="server">
                                                <label>
                                                    <%--<span style="color: red;">*</span>--%>MOU To
                                                </label>

                                                <div class="input-group date">
                                                    <div class="input-group-addon">
                                                        <i class="fa fa-calendar text-blue"></i>
                                                    </div>
                                                    <asp:TextBox ID="txtMouTo" runat="server" TabIndex="4" autocomplete="off" onchange="CheckDate();" CssClass="form-control" />

                                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtMouTo" PopupButtonID="imgCalFromDate" Enabled="true" EnableViewState="true" />
                                                    <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txtMouTo"
                                                        Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                        AcceptNegative="none" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtMouTo"
                                                        ErrorMessage="Please Select Date of the Visit" ValidationGroup="Show" Display="None"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group col-md-4">
                                                <label><%--<span style="color: red;">*</span>--%> MOU Type</label>
                                                <asp:DropDownList ID="ddlMoutype" runat="server" TabIndex="5" AppendDataBoundItems="True" CssClass="form-control">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    <asp:ListItem Value="1">Industrial</asp:ListItem>
                                                    <asp:ListItem Value="2">Institute</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-md-4" runat="server">
                                                <label><%--<span style="color: red;">*</span>--%>Activities</label>
                                                <asp:TextBox ID="txtActivities" runat="server" TabIndex="6" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-md-4" runat="server">
                                                <label><%--<span style="color: red;">*</span>--%>Remarks</label>
                                                <asp:TextBox ID="txtRemarks" runat="server" TabIndex="7" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-md-4">
                                                <label>Live Status</label>
                                                <asp:DropDownList ID="ddlLive" runat="server" TabIndex="8" AppendDataBoundItems="True" CssClass="form-control">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="2">No</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group col-md-4">
                                                <label>Student Participated</label>
                                                <asp:TextBox ID="txt_StuentParticipated" runat="server" TabIndex="9" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-md-4">
                                                <label>Teacher Participated</label>
                                                <asp:TextBox ID="txt_TeacherParticipated" runat="server" TabIndex="10" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-md-4">
                                                <label>Link</label>
                                                <asp:TextBox ID="txt_Link" runat="server" TabIndex="11" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                            </div>

                                        </div>
                                        <div class="col-md-8 col-sm-8 col-xs-12 form-group">
                                            <%--<div class="col-md-12">--%>
                                            <label for="city">
                                                Supportive Documents Attachment <span style="color: red; font-size: 10px;">1) Only .jpg, .jpeg, .png , .pdf format with max 1 MB size are allowed                    
                                                        2) Please Select Multiple Certificates by pressing Ctrl Key
                                                        <br />
                                                    3) Please Ensure all Certificates Available in one Folder Only </span>
                                            </label>
                                            <%-- </div>--%>
                                            <asp:FileUpload ID="FuIndustrialLink" runat="server" TabIndex="12" ToolTip="Select file to upload"
                                                AllowMultiple="true" CssClass="form-control" accept=".jpg,.jpeg,.pdf,.JPG,.JPEG,.PNG,.pdf,.PDF" />

                                            <br />


                                            <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
                                                <asp:Repeater ID="Repeater1" runat="server">
                                                    <HeaderTemplate>
                                                        <table class="table table-bordered table-hover table-fixed">
                                                            <thead>
                                                                <tr class="bg-light blue">
                                                                    <th>Action</th>
                                                                    <th>Project Name
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
                                                                <%--<asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif"  AlternateText='<%# Eval("SPORTS_FILE_NAME") %>'
                                                                          CommandArgument='<%#Eval("SPORTS_DOCS_NO") %>' ToolTip='<%# Eval("SPORTS_NAME") %>' 
                                                                           OnClick="btnDelete_Click"  OnClientClick="return UserDeleteConfirmation();" />--%>
                                                            </td>
                                                            <td>
                                                                <%-- <asp:Label ID="lblName" runat="server" Text='<%#Eval("SPORTS_NAME") %>' ToolTip='<%#Eval("IDNO") %>'></asp:Label>--%>
                                                            </td>
                                                            <td>
                                                                <%--  <%#Eval("SPORTS_FILE_NAME") %>--%>
                                                            </td>
                                                            <td>
                                                                <%--<asp:LinkButton ID="btnDownload" runat="server" Text="Download" CommandArgument='<%#Eval("SPORTS_FILE_NAME") %>'
                                                                            OnClick="btnDownload_Click"></asp:LinkButton>--%>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </tbody> </table>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                            </asp:Panel>

                                        </div>

                                        <div class="box-footer col-md-12 ">
                                            <p class="text-center">
                                                <asp:Button ID="btnSubmit_Link" runat="server" TabIndex="13" CssClass="btn btn-outline-info" Text="Submit" ValidationGroup="AddIndustrialLink" OnClick="btnSubmit_Link_Click" />
                                                <asp:Button ID="btnCancel_Link" runat="server" TabIndex="14" Text="Cancel" CssClass="btn btn-outline-danger" OnClick="btnCancel_Link_Click" />
                                            </p>

                                        </div>

                                        <div id="div4" runat="server" style="display: block; transition: 0.8s;" class="col-md-12">
                                            <asp:ListView ID="LvIndustrialLink" runat="server" OnItemDataBound="LvIndustrialLink_ItemDataBound">
                                                <LayoutTemplate>
                                                    <div>
                                                        <table class="table table-hover table-bordered table-striped">
                                                            <thead>

                                                                <tr class="bg-light-blue">
                                                                    <th>Edit
                                                                    </th>
                                                                    <th>Company Name           
                                                                    </th>
                                                                    <th>Company Address
                                                                    </th>
                                                                    <th>Mou From
                                                                    </th>
                                                                    <th>Mou To
                                                                    </th>
                                                                    <th>Mou Type
                                                                    </th>
                                                                    <th>Activities
                                                                    </th>
                                                                    <th>Remarks
                                                                    </th>
                                                                    <th>Live Status
                                                                    </th>
                                                                    <th>Upload Certificates
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
                                                    <tr class="first-div">
                                                        <td style="text-align: center">
                                                            <asp:ImageButton ID="btnLinkEdit" runat="server" ImageUrl="~/images/edit.gif" CommandArgument='<%# Eval("INDLINKNO") %>'
                                                                AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnLinkEdit_Click" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCompany" runat="server" Text='<%# Eval("COMPANY_NAME") %>' ToolTip='<%#Eval("INDLINKNO") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <%# Eval("COMPANY_ADDRESS") %>
                                                        </td>
                                                        <td>
                                                            <%# Eval("MOU_FROM") %>
                                                        </td>
                                                        <td>
                                                            <%# Eval("MOU_TO") %>
                                                        </td>
                                                        <td>
                                                            <%# Eval("MOUTYPE") %>
                                                        </td>
                                                        <td>
                                                            <%# Eval("ACTIVITIES") %>
                                                        </td>

                                                        <td>
                                                            <%# Eval("REMARKS") %>
                                                        </td>
                                                        <td>
                                                            <%#Eval("LIVESTATUS") %>
                                                        </td>
                                                        <td style="text-align: center">
                                                            <img alt="" class="img-collapse" style="cursor: pointer" src="../images/plus.gif" />
                                                        </td>
                                                        <tr class="div-collapse">
                                                            <td colspan="5">

                                                                <asp:ListView ID="LvIndustrialCert" runat="server">
                                                                    <LayoutTemplate>
                                                                        <div id="demo-grid">
                                                                            <table class="table table-hover table-bordered">
                                                                                <thead>
                                                                                    <tr class="bg-success">
                                                                                        <th>Action
                                                                                        </th>
                                                                                        <th style="text-align: center">
                                                                                            <center>Sr No.</center>
                                                                                        </th>
                                                                                        <th style="text-align: center">File Name
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
                                                                            <td>
                                                                                <asp:ImageButton ID="btnIndustrialLnkDelete" runat="server" ImageUrl="~/images/delete.gif" AlternateText='<%# Eval("INDUSTRIAL_LINK_FILE_NAME")%>'
                                                                                    CommandArgument='<%#Eval("INDLINK_DOCS_NO") %>' ToolTip='<%#Eval("COMPANY_NAME") %>' CommandName='<%#Eval("INDLINKNO") %>'
                                                                                    OnClick="btnIndustrialLnkDelete_Click" OnClientClick="return UserDeleteConfirmation();" />
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <%# Container.DataItemIndex + 1%>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:Label ID="lblCertificationName" runat="server" Text='<%# Eval("INDUSTRIAL_LINK_FILE_NAME")%>'></asp:Label>

                                                                            </td>

                                                                            <td style="text-align: center">
                                                                                <asp:LinkButton ID="btnDownloadIndustrialLink" runat="server" Text="Download"
                                                                                    CommandArgument='<%#Eval("COMPANY_NAME") %>'
                                                                                    ToolTip='<%# Eval("INDUSTRIAL_LINK_FILE_NAME")%>' OnClick="btnDownloadIndustrialLink_Click"></asp:LinkButton>

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
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="LvIndustrialLink" />
                                <asp:PostBackTrigger ControlID="btnSubmit_Link" />
                                <%--  <asp:AsyncPostBackTrigger ControlID="LvIndustrialLink" />--%>
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>

                </div>
            </div>
            <asp:HiddenField ID="TabName" runat="server" />
        </div>
    </div>
    <div id="divMsg" runat="server">
    </div>

    <script type="text/javascript">
        function SelectAll(chk) {

            var tbl = document.getElementById("tblStudentRecords");

            if (tbl != null && tbl.childNodes != null) {
                for (i = 0; i < tbl.getElementsByTagName("tr").length; i++) {
                    document.getElementById('ctl00_ContentPlaceHolder1_tcLeaving_tabBC_lvStudentRecords_ctrl' + i + '_chkReport').checked = chk.checked;
                }
            }
        }

        function SelectAll_LC(chk) {
            var tbl = document.getElementById("tblStudentRecords_LC");
            if (tbl != null && tbl.childNodes != null) {
                for (i = 0; i < tbl.getElementsByTagName("tr").length; i++) {
                    document.getElementById('ctl00_ContentPlaceHolder1_tcLeaving_tabLC_lvStudentList_ctrl' + i + '_chkReport').checked = chk.checked;
                }
            }
        }
    </script>

    <script language="javascript" type="text/javascript">
        function totAll(headchk) {
            var frm = document.forms[0]
            for (i = 0; i < document.forms[0].elements.length; i++) {
                var e = frm.elements[i];
                if (e.type == 'checkbox') {
                    if (headchk.checked == true)
                        e.checked = true;
                    else
                        e.checked = false;
                }
            }
        }
    </script>

    <script type="text/javascript">
        function UserDeleteConfirmation() {
            if (confirm("Are you sure you want to Delete this File?"))
                return true;
            else
                return false;
        }
    </script>

    <script type="text/javascript">
        $(function () {
            var tabName = $("[id*=TabName]").val() != "" ? $("[id*=TabName]").val() : "visit";
            $('#Tabs a[href="#' + tabName + '"]').tab('show');
            $("#Tabs a").click(function () {
                $("[id*=TabName]").val($(this).attr("href").replace("#", ""));
            });
        });
    </script>

    <script type="text/javascript">
        function CheckDate() {
            var From = document.getElementById("<%=txtMouFrom.ClientID%>").value.split("/");
            var Fdate = new Date(From[2], From[1] - 1, From[0]);
            /// alert(Fdate);
            var To = document.getElementById("<%=txtMouTo.ClientID%>").value.split("/");
            var Tdate = new Date(To[2], To[1] - 1, To[0]);
            if (Tdate < Fdate) {
                alert('To Date ' + document.getElementById("<%=txtMouTo.ClientID%>").value + ' should not be less than From Date ' + document.getElementById("<%=txtMouFrom.ClientID%>").value + '');
                document.getElementById("<%=txtMouTo.ClientID%>").value = '';
            }

        }
    </script>

</asp:Content>


