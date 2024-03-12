<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"
    CodeFile="Student_details.aspx.cs" Inherits="ACADEMIC_Comprehensive_Stud_Report"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Content/jquery.js" type="text/javascript"></script>
    <script src="../Content/jquery.dataTables.js" language="javascript" type="text/javascript"></script>
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
    <%--<div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updStudent"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>--%>

    <script language="javascript" type="text/javascript">

        function checkDate(CHK) {
            var todate, fromdate;
            if (CHK == 1) {
                document.getElementById('<%=hdfduration.ClientID%>').value = '';
                 todate = document.getElementById('<%=txtProjectTodate.ClientID %>').value;
                 fromdate = document.getElementById('<%=txtProjectFromDate .ClientID %>').value;

             }
             else if (CHK == 3) {
                 //    alert('hai');
                 fromdate = document.getElementById('<%=txtMoufrom.ClientID%>').value;
                 todate = document.getElementById('<%=txtMouto.ClientID%>').value;
                 //     alert('done');

             }
             else {
                 document.getElementById('<%=hdfinternDuration.ClientID%>').value = '';
                 fromdate = document.getElementById('<%=txtFromDate.ClientID %>').value;
                 todate = document.getElementById('<%=txtToDate .ClientID %>').value;

             }
         document.getElementById('<%=hdfduration.ClientID%>').value = '';


             var date = fromdate.split("/");
             var day = date[0];
             var month = date[1] - 1;
             var year = date[2];

             var FromDate = new Date(year, month, day);

             var tdate = todate.split("/");
             var tday = tdate[0];
             var tmonth = tdate[1] - 1;
             var tyear = tdate[2];
             var TDate = new Date(tyear, tmonth, tday);
             if (FromDate > TDate) {
                 if (CHK == 1) {
                     alert('To Date should not be less than From Date');
                     document.getElementById('<%=hdfduration.ClientID%>').value = result;
                    document.getElementById('<%=txtPrjctduration.ClientID%>').value = '';
                    document.getElementById('<%=txtPrjctduration.ClientID%>').innerHTML = result;
                    document.getElementById('<%=hdfduration.ClientID%>').value = '';
                    document.getElementById('<%=txtPrjctduration.ClientID%>').value = '';
                    todate = document.getElementById('<%=txtProjectTodate.ClientID %>').value = '';
                    //    alert('done');
                }
                else if (CHK == 3) {
                    alert('Sanctioned Date should not be less than Applied Date');
                    //document.getElementById('<%=txtMoufrom.ClientID%>').value;
                    document.getElementById('<%=txtMouto.ClientID%>').value = '';
                }
                else {
                    alert('To Date should not be less than From Date');
                    todate = document.getElementById('<%=txtToDate .ClientID %>').value = '';
                    document.getElementById('<%=txtDuration.ClientID%>').value = '';
                    document.getElementById('<%=hdfinternDuration.ClientID%>').value = result;
                    document.getElementById('<%=txtDuration.ClientID%>').innerHTML = result;
                    document.getElementById('<%=txtDuration.ClientID%>').value = '';
                    //      alert('hai');
                }

        }

        if (TDate > FromDate) {
            //return
            //}
            //else
            //{
            var count = TDate - FromDate;
            var date1 = Math.abs(TDate - FromDate) / 1000;
            var result = Math.floor(date1 / 86400) + 1 + ' Days';
            //  alert(result);
            //  var weeks = Math.round(result) / 7;
            //  alert(weeks);
            //  var week = Math.floor(weeks);
            //  alert(week);
            //  var actualdays = (result - week) + 1 +' Days';
            // // var totalweekends = Math.round(result) * 2;
            // // alert(totalweekends);
            ////  var actualdays = Math.round(result) - totalweekends;
            //  alert(actualdays)+1+'Days';

            // alert(result);
            if (CHK == 1) {
                document.getElementById('<%=hdfduration.ClientID%>').value = result;    //result  actualdays
                        document.getElementById('<%=txtPrjctduration.ClientID%>').innerHTML = result;   //result    actualdays

                    }
                    else {
                        //  alert(result);
                        document.getElementById('<%=hdfinternDuration.ClientID%>').value = result;  //result    actualdays
                        document.getElementById('<%=txtDuration.ClientID%>').innerHTML = result;    //result    actualdays
                        //  alert(result);
                    }

                }
            }


    </script>

    <script language="javascript" type="text/javascript">
        function Checkamt() {
            var amtapplied = parseFloat(document.getElementById('<%=txtAmountApplied.ClientID %>').value);
            var amtsanction = parseFloat(document.getElementById('<%=txtAmountsanctioned.ClientID %>').value);
            //  alert(amtapplied);
            //  alert(amtsanction);

            if (amtapplied < amtsanction) {
                //     alert('hai');
                alert('Sanctioned Amount (' + amtsanction + ') Cannot be Greater than Applied Amount (' + (amtapplied) + ')');
                //alert(amtsanction);
                var amtsanction = document.getElementById('<%=txtAmountsanctioned.ClientID %>').value = '';
                document.getElementById('<%=txtAmountsanctioned.ClientID%>').focus();
            }

        }
    </script>

    <%-- <asp:UpdatePanel ID="updStudent" runat="server">
        <ContentTemplate>--%>

    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title"><b>STUDENT INFORMATION</b></h3>
                </div>
                <div class="box-body">

                    <asp:Label ID="lblMsg" runat="server" SkinID="Msglbl"></asp:Label>

                    <asp:Panel ID="pnlSearch" runat="server">
                        <div class="panel panel-info">
                            <div class="box-body">
                                <div class="col-xs-12 col-md-offset-2 col-sm-offset-2">
                                    <div class="col-md-3 col-xs-12 col-sm-3" style="margin-top: 5px; text-align: right;">
                                        <label>Univ. Reg. No. / TAN/PAN : </label>
                                    </div>
                                    <div class="col-md-4 col-xs-12 col-sm-4">
                                        <asp:TextBox ID="txtEnrollmentSearch" runat="server" ValidationGroup="submit" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-5 col-xs-12 col-sm-5" style="float: left;">

                                        <asp:Button ID="btnSearch" runat="server"
                                            OnClick="btnSearch_Click" ValidationGroup="submit" Text="Search" CssClass="btn btn-outline-info" />
                                        <asp:RequiredFieldValidator ID="rfvSearch" runat="server" ControlToValidate="txtEnrollmentSearch"
                                            Display="None" ErrorMessage="Please Enter Univ. Reg. No. Or TAN/PAN" ValidationGroup="submit">
                                        </asp:RequiredFieldValidator>

                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True"
                                            ShowSummary="False" ValidationGroup="submit" />

                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                    <%--  THIS PANEL IS FOR SHOWING STUDENT INFORMATION--%>
                    <div class="row">
                        <div class="col-md-12 form-group">
                            <div class="box box-primary">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Student Information</h3>
                                    <div class="box-tools pull-right">
                                        <a id="menu-toggle" style="cursor: pointer; transition: 0.8s;" onclick="javascript:toggleExpansion(this,'divStudentInfo')">
                                            <i class="glyphicon glyphicon-minus text-blue"></i>
                                        </a>
                                    </div>
                                </div>

                                <div class="box-body">
                                    <div id="divStudentInfo" runat="server" style="display: block;" class="col-md-12">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-5 col-xs-12 form-group">
                                                    <ul class="list-group list-group-unbordered">
                                                        <li class="list-group-item"><b>Student Name :</b><a>
                                                            <asp:Label ID="lblName" runat="server" Font-Bold="True"></asp:Label></a> </li>

                                                        <li class="list-group-item"><b>Degree :</b><a>
                                                            <asp:Label ID="lblDegree" runat="server" Font-Bold="True"></asp:Label></a>
                                                            <br />
                                                        </li>
                                                        <li class="list-group-item"><b>Branch :</b><a>
                                                            <asp:Label ID="lblBranch" runat="server" Font-Bold="True" Style="font-size: 13px;"></asp:Label></a>
                                                            <br />
                                                        </li>

                                                    </ul>
                                                </div>
                                                <div class="col-md-5 col-xs-12 form-group">
                                                    <ul class="list-group list-group-unbordered">
                                                        <li class="list-group-item"><b>Univ. Reg. No. :</b><a>
                                                            <asp:Label ID="lblRegNo" runat="server" Font-Bold="True"></asp:Label></a> </li>
                                                        <li class="list-group-item"><b>TAN/PAN :</b><a>
                                                            <asp:Label ID="lblEnrollNo" runat="server" Font-Bold="True"></asp:Label></a>
                                                        </li>
                                                        <li class="list-group-item"><b>Semester/Year :</b><a>
                                                            <asp:Label ID="lblSemester" runat="server" Font-Bold="True"></asp:Label></a>
                                                        </li>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <%--  THIS PANEL IS FOR SHOWING PROJECTS STATUS--%>
                    <div class="row">
                        <div class="col-md-12 form-group">
                            <div class="box box-primary">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Projects</h3>
                                    <div class="box-tools pull-right">
                                        <a id="menu-toggle-1" style="cursor: pointer; transition: 0.8s;" onclick="javascript:toggleExpansion(this,'divinfo')">
                                            <i class="glyphicon glyphicon-minus text-blue"></i>
                                        </a>
                                    </div>
                                </div>
                                <div class="box-body">

                                    <div class="col-md-12 from-group" id="divinfo" runat="server" visible="true">
                                        <div class="form-group col-md-12" style="padding-left: 12%">
                                            <asp:RadioButtonList ID="rdbProjects" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="1" Selected="True">Industry &nbsp;&nbsp;&nbsp;&nbsp;
                              &nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                <asp:ListItem Value="2">College &nbsp;&nbsp;&nbsp;&nbsp;
                              &nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                <asp:ListItem Value="3">NGO &nbsp;&nbsp;&nbsp;&nbsp;
                              &nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                <asp:ListItem Value="4">Others</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>

                                        <div class="form-group col-md-4" id="trNameProject" runat="server">
                                            <label><span style="color: red;">*</span> Name of the Project</label>
                                            <asp:TextBox ID="txtNameofPrjct" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvProject" runat="server" ControlToValidate="txtNameofPrjct"
                                                Display="None" ErrorMessage="Please Enter Name of the Project" SetFocusOnError="true" ValidationGroup="AddProject">
                                            </asp:RequiredFieldValidator>
                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List" ShowMessageBox="True"
                                                ShowSummary="False" ValidationGroup="AddProject" />
                                        </div>

                                        <div class="form-group col-md-4" id="trIndustry" runat="server">
                                            <label><%--<span style="color: red;">*</span>--%> Name of the Industry</label>
                                            <asp:TextBox ID="txtprjctNameindustry" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-4" id="trAddress" runat="server">
                                            <label><%--<span style="color: red;">*</span>--%> Address of the Industry</label>
                                            <asp:TextBox ID="txtAdresIndustry" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-4" id="trProjectdetails" runat="server">
                                            <label><%--<span style="color: red;">*</span>--%> Project Details</label>
                                            <asp:TextBox ID="txtPrjctDetails" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-4" id="div7" runat="server">
                                            <label>
                                                <%--<span style="color: red;">*</span>--%> From Date
                                  <%-- <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meeFromDate" ControlToValidate="txtFromDate"
                                       IsValidEmpty="False" EmptyValueMessage="Please Enter From date " InvalidValueMessage="From date is invalid"
                                       InvalidValueBlurredMessage="*" Display="Dynamic" ValidationGroup="report" />--%>
                                            </label>

                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtProjectFromDate" runat="server" CssClass="form-control" />

                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtProjectFromDate" PopupButtonID="imgCalFromDate" Enabled="true" EnableViewState="true" />
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtProjectFromDate"
                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                    AcceptNegative="none" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtProjectFromDate"
                                                    ErrorMessage="Please Select Project From Date" ValidationGroup="Show" Display="None"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="form-group col-md-4" id="div8" runat="server">
                                            <label>
                                                <%--<span style="color: red;">*</span>--%> To Date
                                   <%--  <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="meeToDate" ControlToValidate="txtToDate"
                                         IsValidEmpty="False" EmptyValueMessage="Please Enter To Date" InvalidValueMessage="To date is invalid"
                                         InvalidValueBlurredMessage="*" Display="Dynamic" ValidationGroup="report" />--%>
                                            </label>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtProjectTodate" runat="server" CssClass="form-control" onchange="checkDate(1);" />

                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender7" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtProjectTodate" PopupButtonID="imgCalToDate" Enabled="true" EnableViewState="true" />
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender5" runat="server" TargetControlID="txtProjectTodate"
                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                    AcceptNegative="none" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtProjectTodate"
                                                    ErrorMessage="Please Select Project To Date" ValidationGroup="Show" Display="None"></asp:RequiredFieldValidator>

                                            </div>
                                        </div>


                                        <div class="form-group col-md-4" id="trDuration" runat="server">
                                            <label><%--<span style="color: red;">*</span>--%> Duration</label>
                                            <asp:Label ID="txtPrjctduration" runat="server" CssClass="form-control"></asp:Label>
                                            <asp:HiddenField ID="hdfduration" runat="server" />
                                        </div>
                                        <div class="form-group col-md-4" id="trGrant" runat="server">
                                            <label><%--<span style="color: red;">*</span>--%> Grant Received</label>
                                            <asp:TextBox ID="txtGrantReceived" runat="server" MaxLength="10" CssClass="form-control"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="fteGrant" runat="server" TargetControlID="txtGrantReceived" FilterType="Numbers,Custom" ValidChars="."></ajaxToolKit:FilteredTextBoxExtender>
                                        </div>
                                        <div class="form-group col-md-4" id="trSupervisor" runat="server">
                                            <label><%--<span style="color: red;">*</span>--%> Supervisor/Guide Details</label>
                                            <asp:TextBox ID="txtSupervisor" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-4" id="trMentor" runat="server">
                                            <label><%--<span style="color: red;">*</span>--%> Mentor Alloted in College</label>
                                            <asp:TextBox ID="txtMentor" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-4" id="trProjectType" runat="server">
                                            <label><%--<span style="color: red;">*</span>--%> Project Type</label>
                                            <%--<asp:TextBox ID="TextBox8" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                            <asp:DropDownList ID="ddlProjectType" runat="server" CssClass="form-control" AppendDataBoundItems="true"></asp:DropDownList>
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
                                            <asp:FileUpload ID="fuProjectCertificate" runat="server" ToolTip="Select file to upload"
                                                AllowMultiple="true" CssClass="form-control" accept=".jpg,.jpeg,.pdf,.JPG,.JPEG,.PNG,.pdf,.PDF" />

                                            <br />


                                            <asp:Panel ID="Panel3" runat="server" ScrollBars="Auto">
                                                <asp:Repeater ID="lvProjectCertificate" runat="server">
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
                                                                <asp:Label ID="lblName" runat="server" Text='<%#Eval("SPORTS_NAME") %>' ToolTip='<%#Eval("IDNO") %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <%#Eval("SPORTS_FILE_NAME") %>
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
                                                <asp:Button ID="btnProjectAdd" runat="server" CssClass="btn btn-outline-info" Text="Add" ValidationGroup="AddProject" OnClick="btnProjectAdd_Click" />
                                                <asp:Button ID="btnProjectCancel" runat="server" Text="Cancel" CssClass="btn btn-outline-danger" OnClick="btnProjectCancel_Click" />
                                            </p>

                                        </div>
                                    </div>

                                    <div id="divProjectStatus" runat="server" style="display: block; transition: 0.8s;" class="col-md-12">
                                        <asp:ListView ID="lvProjectStatus" runat="server" OnItemDataBound="lvProjectStatus_ItemDataBound">
                                            <LayoutTemplate>
                                                <div>
                                                    <table class="table table-hover table-bordered table-striped">
                                                        <thead>

                                                            <tr class="bg-light-blue">

                                                                <th>Project Name
                                                                </th>
                                                                <th>Industry Name
                                                                </th>
                                                                <th>Industry Address
                                                                </th>
                                                                <th>Project Details
                                                                </th>
                                                                <th>Duration
                                                                </th>
                                                                <th>Grant Received
                                                                </th>
                                                                <th>Supervisor/Guide Details
                                                                </th>
                                                                <th>Mentor
                                                                </th>
                                                                <th>Project Type
                                                                </th>
                                                                <th>Industry Type
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
                                                        <asp:Label ID="lblProjectName" runat="server" Text='<%# Eval("PROJECT_NAME") %>'
                                                            ToolTip='<%# Eval("PTNO") %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <%# Eval("INDUSTRY_NAME") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("ADDRESS_INDUSTRY") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("PROJECT_DETAILS") %>
                                                    </td>

                                                    <td>
                                                        <%# Eval("DURATION") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("GRANT_RECEIVED") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("SUPERVISOR_DETAILS") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("MENTOR_COLLEGE") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("PROJECT_TYPE") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("INDUSTRY_TYPE") %>
                                                    </td>
                                                    <td style="text-align: center">
                                                        <img alt="" class="img-collapse" style="cursor: pointer" src="/images/plus.gif" />
                                                    </td>

                                                    <tr class="div-collapse">
                                                        <td colspan="5">

                                                            <asp:ListView ID="lvProjctDetails" runat="server">
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
                                                                            <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.gif" AlternateText='<%# Eval("PROJECT_FILE_NAME")%>'
                                                                                CommandArgument='<%#Eval("PROJECTS_DOCS_NO") %>' ToolTip='<%#Eval("PROJECT_NAME") %>' CommandName='<%# Eval("PTNO") %>'
                                                                                OnClick="btnDelete_Click" OnClientClick="return UserDeleteConfirmation();" />
                                                                        </td>
                                                                        <td style="text-align: center">
                                                                            <%# Container.DataItemIndex + 1%>
                                                                        </td>
                                                                        <td style="text-align: center">
                                                                            <asp:Label ID="lblFileName" runat="server" Text='<%# Eval("PROJECT_FILE_NAME")%>'></asp:Label>

                                                                        </td>

                                                                        <td style="text-align: center">
                                                                            <asp:LinkButton ID="btnDownloadFile" runat="server" Text="Download"
                                                                                CommandArgument='<%#Eval("PROJECT_NAME") %>'
                                                                                ToolTip='<%# Eval("PROJECT_FILE_NAME")%>' OnClick="btnDownloadFile_Click"></asp:LinkButton>

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
                        </div>
                    </div>

                    <%--  THIS PANEL IS FOR SHOWING INTERNSHIP STATUS--%>
                    <div class="row">
                        <div class="col-md-12 form-group">
                            <div class="box box-primary">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Internship</h3>
                                    <div class="box-tools pull-right">
                                        <a id="menu-toggle-2" style="cursor: pointer; transition: 0.8s;" onclick="javascript:toggleExpansion(this,'div1')">
                                            <i class="glyphicon glyphicon-minus text-blue"></i>
                                        </a>
                                    </div>
                                </div>
                                <div class="box-body">

                                    <div class="col-md-12 from-group" id="div1" runat="server" visible="true">

                                        <div class="form-group col-md-4" runat="server">
                                            <label><span style="color: red;">*</span> Name of the Company</label>
                                            <%--Name of the Industry--%>
                                            <asp:TextBox ID="txtNameIndustry" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtNameIndustry"
                                                Display="None" ErrorMessage="Please Enter Name of the Industry" SetFocusOnError="true" ValidationGroup="AddInternship">
                                            </asp:RequiredFieldValidator>
                                            <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List" ShowMessageBox="True"
                                                ShowSummary="False" ValidationGroup="AddInternship" />
                                        </div>
                                        <div class="form-group col-md-4" runat="server">
                                            <label><%--<span style="color: red;">*</span>--%> Address of the Company</label>
                                            <%--Address of the Industry--%>
                                            <asp:TextBox ID="txtAddressIndustry" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-4" runat="server">
                                            <label><%--<span style="color: red;">*</span>--%> Details of Internship</label>
                                            <asp:TextBox ID="txtDetailsInternship" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-4" id="divfrmdate" runat="server">
                                            <label>
                                                <%--<span style="color: red;">*</span>--%> From Date
                                  <%-- <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="meeFromDate" ControlToValidate="txtFromDate"
                                       IsValidEmpty="False" EmptyValueMessage="Please Enter From date " InvalidValueMessage="From date is invalid"
                                       InvalidValueBlurredMessage="*" Display="Dynamic" ValidationGroup="report" />--%>
                                            </label>

                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" />

                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtFromDate" PopupButtonID="imgCalFromDate" Enabled="true" EnableViewState="true" />
                                                <ajaxToolKit:MaskedEditExtender ID="meeFromDate" runat="server" TargetControlID="txtFromDate"
                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                    AcceptNegative="none" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFromDate"
                                                    ErrorMessage="Please Select From Date" ValidationGroup="Show" Display="None"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="form-group col-md-4" id="divtodate" runat="server">
                                            <label>
                                                <%--<span style="color: red;">*</span>--%> To Date
                                   <%--  <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="meeToDate" ControlToValidate="txtToDate"
                                         IsValidEmpty="False" EmptyValueMessage="Please Enter To Date" InvalidValueMessage="To date is invalid"
                                         InvalidValueBlurredMessage="*" Display="Dynamic" ValidationGroup="report" />--%>
                                            </label>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" onchange="checkDate(2);" />

                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtToDate" PopupButtonID="imgCalToDate" Enabled="true" EnableViewState="true" />
                                                <ajaxToolKit:MaskedEditExtender ID="meeToDate" runat="server" TargetControlID="txtToDate"
                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                    AcceptNegative="none" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtToDate"
                                                    ErrorMessage="Please Select To Date" ValidationGroup="Show" Display="None"></asp:RequiredFieldValidator>

                                            </div>
                                        </div>
                                        <div class="form-group col-md-4" runat="server">
                                            <label><%--<span style="color: red;">*</span>--%> Duration</label>
                                            <asp:HiddenField ID="hdfinternDuration" runat="server" />
                                            <asp:Label ID="txtDuration" runat="server" CssClass="form-control"></asp:Label>
                                        </div>
                                        <div class="form-group col-md-4" runat="server">
                                            <label><%--<span style="color: red;">*</span>--%> Remarks</label>
                                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-4" runat="server">
                                            <label><%--<span style="color: red;">*</span>--%> Stipend if any</label>
                                            <asp:TextBox ID="txtStipend" runat="server" CssClass="form-control"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="ftestipend" runat="server" FilterType="Custom,Numbers" ValidChars="." TargetControlID="txtStipend"></ajaxToolKit:FilteredTextBoxExtender>
                                        </div>

                                        <div class="form-group col-md-4" runat="server">
                                            <label>Name of the Technical person</label>
                                            <asp:TextBox ID="txtTPerson" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-4" runat="server">
                                            <label>Mobile No.</label>
                                            <asp:TextBox ID="txtmobile" runat="server" MaxLength="10" onchange="return ValidateNo(this);"
                                                CssClass="form-control" ToolTip='<%#Eval("IDNO")%>' onkeypress="return isNumber(event)" />
                                            <asp:RequiredFieldValidator ID="rfvmobile" runat="server" ErrorMessage="Please Enter Mobile Number" SetFocusOnError="True"
                                                ControlToValidate="txtmobile" ValidationGroup="mobile" Display="None">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-md-4" runat="server">
                                            <label>Email Id</label>
                                            <asp:TextBox ID="txtEmailid" runat="server" CssClass="form-control" onchange="return EmailValidation(this);"></asp:TextBox>
                                        </div>
                                        <div class="col-md-8 col-sm-8 col-xs-12 form-group">
                                            <label for="city">
                                                Supportive Documents Attachment <span style="color: red; font-size: 10px;">1) Only .jpg, .jpeg, .png , .pdf format with max 1 MB size are allowed 2) Please Select Multiple Certificates by pressing Ctrl Key
                                                        <br />
                                                    3) Please Ensure all Certificates Available in one Folder Only </span>
                                            </label>
                                            <asp:FileUpload ID="FuIntershipCertificate" runat="server" ToolTip="Select file to upload"
                                                AllowMultiple="true" CssClass="form-control" accept=".jpg,.jpeg,.pdf,.JPG,.JPEG,.PNG,.pdf,.PDF" />

                                            <br />


                                            <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
                                                <asp:Repeater ID="Repeater1" runat="server">
                                                    <HeaderTemplate>
                                                        <table class="table table-bordered table-hover table-fixed">
                                                            <thead>
                                                                <tr class="bg-light blue">
                                                                    <th>Action</th>
                                                                    <th>Industry Name
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
                                                                <asp:Label ID="lblName" runat="server" Text='<%#Eval("SPORTS_NAME") %>' ToolTip='<%#Eval("IDNO") %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <%#Eval("SPORTS_FILE_NAME") %>
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
                                                <asp:Button ID="btnInternship" runat="server" CssClass="btn btn-outline-info" Text="Add" ValidationGroup="AddInternship" OnClick="btnInternship_Click" />
                                                <asp:Button ID="btnInterncancel" runat="server" Text="Cancel" CssClass="btn btn-outline-danger" OnClick="btnInterncancel_Click" />
                                            </p>
                                        </div>
                                    </div>
                                    <div id="div3" runat="server" style="display: block; transition: 0.8s;" class="col-md-12">
                                        <asp:ListView ID="LvIntership" runat="server" OnItemDataBound="LvIntership_ItemDataBound">
                                            <LayoutTemplate>
                                                <div>
                                                    <table class="table table-hover table-bordered table-striped">
                                                        <thead>

                                                            <tr class="bg-light-blue">

                                                                <th>Company Name<%--Industry Name--%>
                                                                </th>
                                                                <th>Company Address<%--Address--%>
                                                                </th>
                                                                <th>Internship Details
                                                                </th>
                                                                <th>From Date
                                                                </th>
                                                                <th>To Date
                                                                </th>
                                                                <th>Duration
                                                                </th>
                                                                <th>Remarks
                                                                </th>
                                                                <th>Stipend
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

                                                    <td>
                                                        <%--<%# Eval("INDUSTRY_NAME") %>--%>
                                                        <asp:Label ID="lblIntern" runat="server" Text='<%# Eval("INDUSTRY_NAME") %>' ToolTip='<%#Eval("ITNO") %>'></asp:Label>
                                                        <%-- <asp:Label ID="lblIntern" runat="server" Text='<%# Eval("PROJECT_NAME") %>'   
                                                            ToolTip='<%# Eval("PTNO") %>'></asp:Label>--%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("ADDRESS_INDUSTRY") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("INTERNSHIP_DETAILS") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("FROM_DATE") %>
                                                    </td>

                                                    <td>
                                                        <%# Eval("TO_DATE") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("DURATION") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("REMARKS") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("STIPEND") %>
                                                    </td>
                                                    <td style="text-align: center">
                                                        <img alt="" class="img-collapse" style="cursor: pointer" src="/images/plus.gif" />
                                                    </td>
                                                    <tr class="div-collapse">
                                                        <td colspan="5">

                                                            <asp:ListView ID="LvInternshipCert" runat="server">
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
                                                                            <asp:ImageButton ID="btnInternDelete" runat="server" ImageUrl="~/images/delete.gif" AlternateText='<%# Eval("INTERNSHIP_FILE_NAME")%>'
                                                                                CommandArgument='<%#Eval("ITERN_DOCS_NO") %>' ToolTip='<%#Eval("INDUSTRY_NAME") %>' CommandName='<%# Eval("ITNO") %>'
                                                                                OnClick="btnInternDelete_Click" OnClientClick="return UserDeleteConfirmation();" />
                                                                        </td>
                                                                        <td style="text-align: center">
                                                                            <%# Container.DataItemIndex + 1%>
                                                                        </td>
                                                                        <td style="text-align: center">
                                                                            <asp:Label ID="lblFileName" runat="server" Text='<%# Eval("INTERNSHIP_FILE_NAME")%>'></asp:Label>

                                                                        </td>

                                                                        <td style="text-align: center">
                                                                            <asp:LinkButton ID="btnDownloadIntern" runat="server" Text="Download"
                                                                                CommandArgument='<%#Eval("INDUSTRY_NAME") %>'
                                                                                ToolTip='<%# Eval("INTERNSHIP_FILE_NAME")%>' OnClick="btnDownloadIntern_Click"></asp:LinkButton>

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

                    <%--  THIS PANEL IS FOR SHOWING AWARDS AND ACHIEVEMENTS STATUS--%>
                    <div class="row">
                        <div class="col-md-12 form-group">
                            <div class="box box-primary">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Awards And Achievements/Certification</h3>
                                    <div class="box-tools pull-right">
                                        <a id="menu-toggle-3" style="cursor: pointer; transition: 0.8s;" onclick="javascript:toggleExpansion(this,'divAward')">
                                            <i class="glyphicon glyphicon-minus text-blue"></i>
                                        </a>
                                    </div>
                                </div>
                                <div class="box-body">

                                    <div class="col-md-12 from-group" id="divAward" runat="server" visible="true">

                                        <div class="form-group col-md-4" runat="server">
                                            <label><span style="color: red;">*</span> Award/Completion/Certification Exam Name</label>
                                            <asp:TextBox ID="txtAwardExamName" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtAwardExamName"
                                                Display="None" ErrorMessage="Please Enter Name of Award/Completion/Certification Exam Name" SetFocusOnError="true" ValidationGroup="AddAwards">
                                            </asp:RequiredFieldValidator>
                                            <asp:ValidationSummary ID="ValidationSummary4" runat="server" DisplayMode="List" ShowMessageBox="True"
                                                ShowSummary="False" ValidationGroup="AddAwards" />
                                        </div>
                                        <div class="form-group col-md-4" runat="server">
                                            <label><%--<span style="color: red;">*</span>--%> Organization Name </label>
                                            <asp:TextBox ID="txtOrganName" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-4" runat="server">
                                            <label><%--<span style="color: red;">*</span>--%> Organization Address</label>
                                            <asp:TextBox ID="txtOrganAddress" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>

                                        <div class="form-group col-md-4 " id="divdateReceived" runat="server">
                                            <label>
                                                <%--<span style="color: red;">*</span>--%>  Date on Received
                                  <%-- <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator4" runat="server" ControlExtender="meeFromDate" ControlToValidate="txtDateReceived"
                                       IsValidEmpty="False" EmptyValueMessage="Please Enter Date on Received " InvalidValueMessage="Date on Received is invalid"
                                       InvalidValueBlurredMessage="*" Display="Dynamic" ValidationGroup="report" />--%>
                                            </label>

                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtDateReceived" runat="server" CssClass="form-control" />

                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtDateReceived" PopupButtonID="imgCalFromDate" Enabled="true" EnableViewState="true" />
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtDateReceived"
                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                    AcceptNegative="none" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtDateReceived"
                                                    ErrorMessage="Please Select Date on Received" ValidationGroup="Show" Display="None"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="form-group col-md-4" runat="server">
                                            <label><%--<span style="color: red;">*</span>--%> Details of the Award</label>
                                            <asp:TextBox ID="txtdetailaward" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-4" runat="server">
                                            <label><%--<span style="color: red;">*</span>--%> Amount Received</label>
                                            <asp:TextBox ID="txtAmountReceived" runat="server" MaxLength="10" CssClass="form-control"></asp:TextBox>
                                            <ajaxToolKit:FilteredTextBoxExtender ID="fteamtreceived" runat="server" FilterType="Numbers,Custom" TargetControlID="txtAmountReceived" ValidChars="."></ajaxToolKit:FilteredTextBoxExtender>
                                        </div>
                                        <div class="col-md-8 col-sm-8 col-xs-12 form-group">
                                            <label for="city">
                                                Supportive Documents Attachment <span style="color: red; font-size: 10px;">1) Only .jpg, .jpeg, .png , .pdf format with max 1 MB size are allowed 2) Please Select Multiple Certificates by pressing Ctrl Key
                                                        <br />
                                                    3) Please Ensure all Certificates Available in one Folder Only </span>
                                            </label>
                                            <asp:FileUpload ID="FuAchievements" runat="server" ToolTip="Select file to upload"
                                                AllowMultiple="true" CssClass="form-control" accept=".jpg,.jpeg,.pdf,.JPG,.JPEG,.PNG,.pdf,.PDF" />

                                            <br />


                                            <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto">
                                                <asp:Repeater ID="Repeater2" runat="server">
                                                    <HeaderTemplate>
                                                        <table class="table table-bordered table-hover table-fixed">
                                                            <thead>
                                                                <tr class="bg-light blue">
                                                                    <th>Action</th>
                                                                    <th>Industry Name
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
                                                                <asp:Label ID="lblName" runat="server" Text='<%#Eval("SPORTS_NAME") %>' ToolTip='<%#Eval("IDNO") %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <%#Eval("SPORTS_FILE_NAME") %>
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
                                                <asp:Button ID="btnAchievement" runat="server" CssClass="btn btn-outline-info" Text="Add" ValidationGroup="AddAwards" OnClick="btnAchievement_Click" />
                                                <asp:Button ID="btnAchievementClear" runat="server" Text="Cancel" CssClass="btn btn-outline-danger" OnClick="btnAchievementClear_Click" />
                                            </p>
                                        </div>
                                    </div>

                                    <div id="div6" runat="server" style="display: block; transition: 0.8s;" class="col-md-12">
                                        <asp:ListView ID="LvAchievements" runat="server" OnItemDataBound="LvAchievements_ItemDataBound">
                                            <LayoutTemplate>
                                                <div>
                                                    <table class="table table-hover table-bordered table-striped">
                                                        <thead>

                                                            <tr class="bg-light-blue">

                                                                <th>Certificate Name		
                                                                </th>
                                                                <th>Organization Name      
                                                                </th>
                                                                <th>Organization Address     		
                                                                </th>
                                                                <th>Date Received
                                                                </th>
                                                                <th>Award Details
                                                                </th>
                                                                <th>Amt Received
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

                                                    <td>
                                                        <%-- <%# Eval("CERTIFICATE_NAME") %>--%>
                                                        <asp:Label ID="lblAchievement" runat="server" Text='<%# Eval("CERTIFICATE_NAME") %>' ToolTip='<%#Eval("CERT_NO") %>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <%# Eval("ORGANIZATION_NAME") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("ORGANIZATION_ADDRESS") %>
                                                    </td>
                                                    <td style="text-align: center">
                                                        <%# Eval("DATE_RECEIVED") %>
                                                    </td>
                                                    <td>
                                                        <%# Eval("AWARD_DETAILS") %>
                                                    </td>

                                                    <td>
                                                        <%# Eval("AMT_RECEIVED") %>
                                                    </td>
                                                    <td style="text-align: center">
                                                        <img alt="" class="img-collapse" style="cursor: pointer" src="/images/plus.gif" />
                                                    </td>
                                                    <tr class="div-collapse">
                                                        <td colspan="5">

                                                            <asp:ListView ID="LvAchievementCert" runat="server">
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
                                                                            <asp:ImageButton ID="btnAchievementDelete" runat="server" ImageUrl="~/images/delete.gif" AlternateText='<%# Eval("ACHIEVEMENTS_FILE_NAME")%>'
                                                                                CommandArgument='<%#Eval("CERT_DOCS_NO") %>' ToolTip='<%#Eval("CERTIFICATE_NAME") %>' CommandName='<%# Eval("CERT_NO") %>'
                                                                                OnClick="btnAchievementDelete_Click" OnClientClick="return UserDeleteConfirmation();" />
                                                                        </td>
                                                                        <td style="text-align: center">
                                                                            <%# Container.DataItemIndex + 1%>
                                                                        </td>
                                                                        <td style="text-align: center">
                                                                            <asp:Label ID="lblCertificationName" runat="server" Text='<%# Eval("ACHIEVEMENTS_FILE_NAME")%>'></asp:Label>

                                                                        </td>

                                                                        <td style="text-align: center">
                                                                            <asp:LinkButton ID="btnDownloadAchievement" runat="server" Text="Download"
                                                                                CommandArgument='<%#Eval("CERTIFICATE_NAME") %>'
                                                                                ToolTip='<%# Eval("ACHIEVEMENTS_FILE_NAME")%>' OnClick="btnDownloadAchievement_Click"></asp:LinkButton>

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

                    <%--  THIS PANEL IS FOR SHOWING PUBLICATIONS STATUS--%>
                    <div class="row">
                        <div class="col-md-12 form-group">
                            <div class="box box-primary">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Publications</h3>
                                    <div class="box-tools pull-right">
                                        <a id="menu-toggle-4" style="cursor: pointer; transition: 0.8s;" onclick="javascript:toggleExpansion(this,'divPublications')">
                                            <i class="glyphicon glyphicon-minus text-blue"></i>
                                        </a>
                                    </div>
                                </div>
                                <div class="box-body">

                                    <div class="col-md-12 from-group" id="divPublications" runat="server" visible="true">

                                        <div class="form-group col-md-4" runat="server">
                                            <label><span style="color: red;">*</span> Title</label>
                                            <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtTitle"
                                                Display="None" ErrorMessage="Please Enter Title" SetFocusOnError="true" ValidationGroup="AddPublications">
                                            </asp:RequiredFieldValidator>
                                            <asp:ValidationSummary ID="ValidationSummary5" runat="server" DisplayMode="List" ShowMessageBox="True"
                                                ShowSummary="False" ValidationGroup="AddPublications" />
                                        </div>
                                        <div class="form-group col-md-4" runat="server">
                                            <label><%--<span style="color: red;">*</span>--%> Journal Name </label>
                                            <asp:TextBox ID="txtJournalname" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-4" runat="server">
                                            <label><%--<span style="color: red;">*</span>--%> Volume</label>
                                            <asp:TextBox ID="txtVolume" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-4" runat="server">
                                            <label><%--<span style="color: red;">*</span>--%> Page No</label>
                                            <asp:TextBox ID="txtPageNo" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-4" runat="server">
                                            <label><%--<span style="color: red;">*</span>--%> Issue No</label>
                                            <asp:TextBox ID="txtIssueno" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-4" runat="server">
                                            <label><%--<span style="color: red;">*</span>--%> Month</label>
                                            <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div id="Div10" class="form-group col-md-4" runat="server">
                                            <label><%--<span style="color: red;">*</span>--%> Year</label>
                                            <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group col-md-4" runat="server">
                                            <label><%--<span style="color: red;">*</span>--%> Authors</label>
                                            <asp:TextBox ID="txtAuthors" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-12 from-group">
                                        <%-- <span style="font-weight:bold">Role</span> --%>
                                        <div class="form-group col-md-4" runat="server">
                                            <label><%--<span style="color: red;">*</span>--%> Author Role</label>
                                            <asp:DropDownList ID="ddlAuthor" runat="server" AppendDataBoundItems="true" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                        <div class="form-group col-md-4" runat="server">
                                            <label><%--<span style="color: red;">*</span>--%> Author Name</label>
                                            <asp:TextBox ID="txtAuthorname" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-4" runat="server" style="margin-top: 25px;">
                                            <asp:Button ID="btnAddAuthor" runat="server" CssClass="btn btn-outline-info" Text="Add Author" OnClick="btnAddAuthor_Click" />
                                        </div>
                                        <div id="div9" runat="server" style="display: block; transition: 0.8s;" class="col-md-6">
                                            <asp:ListView ID="lvAuthor" runat="server" Visible="false">
                                                <LayoutTemplate>
                                                    <div>
                                                        <table class="table table-hover table-bordered table-striped">
                                                            <thead>

                                                                <tr class="bg-light-blue">

                                                                    <th>Author Role
                                                                    </th>
                                                                    <th>Author Name
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
                                                    <tr>

                                                        <td>
                                                            <%#Eval("AUTHOR_ROLE") %>
                                                        </td>
                                                        <td>
                                                            <%#Eval("AUTHOR_NAME") %>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
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
                                        <asp:FileUpload ID="FuPub" runat="server" ToolTip="Select file to upload"
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
                                                            <asp:Label ID="lblName" runat="server" Text='<%#Eval("SPORTS_NAME") %>' ToolTip='<%#Eval("IDNO") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <%#Eval("SPORTS_FILE_NAME") %>
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
                                            <asp:Button ID="btnPublications" runat="server" CssClass="btn btn-outline-info" Text="Add" ValidationGroup="AddPublications" OnClick="btnPublications_Click" />
                                            <asp:Button ID="btnPubCancel" runat="server" Text="Cancel" CssClass="btn btn-outline-danger" OnClick="btnPubCancel_Click" />
                                        </p>

                                    </div>
                                </div>
                                <div id="div4" runat="server" style="display: block; transition: 0.8s;" class="col-md-12">
                                    <asp:ListView ID="LvPublication" runat="server" OnItemDataBound="LvPublication_ItemDataBound">
                                        <LayoutTemplate>
                                            <div>
                                                <table class="table table-hover table-bordered table-striped">
                                                    <thead>

                                                        <tr class="bg-light-blue">

                                                            <th>Title Name           
                                                            </th>
                                                            <th>Journal Name
                                                            </th>
                                                            <th>Volume
                                                            </th>
                                                            <th>Page No
                                                            </th>
                                                            <th>Issue No
                                                            </th>
                                                            <th>Authors
                                                            </th>
                                                            <th>Month/Year
                                                            </th>
                                                            <th>Author Role
                                                            </th>
                                                            <th>Author Name
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

                                                <td>
                                                    <%-- <%# Eval("TITLE_NAME") %>--%>
                                                    <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("TITLE_NAME") %>' ToolTip='<%#Eval("PUBNO") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <%# Eval("JOURNAL_NAME") %>
                                                </td>
                                                <td>
                                                    <%# Eval("VOLUME") %>
                                                </td>
                                                <td>
                                                    <%# Eval("PAGE_NO") %>
                                                </td>
                                                <td>
                                                    <%# Eval("ISSUE_NO") %>
                                                </td>
                                                <td>
                                                    <%# Eval("AUTHORS") %>
                                                </td>

                                                <td style="text-align: center;">
                                                    <%# Eval("MONTH-YEAR") %>
                                                </td>
                                                <td>
                                                    <%#Eval("AUTHOR_ROLE") %>
                                                </td>
                                                <td>
                                                    <%#Eval("AUTHOR_NAME") %>
                                                </td>
                                                <td style="text-align: center">
                                                    <img alt="" class="img-collapse" style="cursor: pointer" src="/images/plus.gif" />
                                                </td>
                                                <tr class="div-collapse">
                                                    <td colspan="5">

                                                        <asp:ListView ID="LvPublicationCert" runat="server">
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
                                                                        <asp:ImageButton ID="btnPublicationsDelete" runat="server" ImageUrl="~/images/delete.gif" AlternateText='<%# Eval("PUBLICATIONS_FILE_NAME")%>'
                                                                            CommandArgument='<%#Eval("PUB_DOCS_NO") %>' ToolTip='<%#Eval("TITLE_NAME") %>' CommandName='<%#Eval("PUBNO") %>'
                                                                            OnClick="btnPublicationsDelete_Click" OnClientClick="return UserDeleteConfirmation();" />
                                                                    </td>
                                                                    <td style="text-align: center">
                                                                        <%# Container.DataItemIndex + 1%>
                                                                    </td>
                                                                    <td style="text-align: center">
                                                                        <asp:Label ID="lblCertificationName" runat="server" Text='<%# Eval("PUBLICATIONS_FILE_NAME")%>'></asp:Label>

                                                                    </td>

                                                                    <td style="text-align: center">
                                                                        <asp:LinkButton ID="btnDownloadPublication" runat="server" Text="Download"
                                                                            CommandArgument='<%#Eval("TITLE_NAME") %>'
                                                                            ToolTip='<%# Eval("PUBLICATIONS_FILE_NAME")%>' OnClick="btnDownloadPublication_Click"></asp:LinkButton>

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

                <%--  THIS PANEL IS FOR SHOWING STUDENT BANK DETAILS--%>
                <div class="row" style="display:none">
                    <div class="col-md-12 form-group">
                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title">Student Bank Details</h3>
                                <br />
                                <span runat="server" visible="false" style="color: red;">Note : Details to be provided by individual student.</span>
                                <div class="box-tools pull-right">
                                    <a id="A1" style="cursor: pointer; transition: 0.8s;" onclick="javascript:toggleExpansion(this,'divPublications')">
                                        <i class="glyphicon glyphicon-minus text-blue"></i>
                                    </a>
                                </div>
                            </div>
                            <div class="box-body">

                                <div class="col-md-12 from-group" id="div2" runat="server" visible="true">

                                    <div class="form-group col-md-4" runat="server">
                                        <label><span style="color: red;">*</span> Bank Name</label>
                                        <asp:DropDownList ID="ddlBank" runat="server" AppendDataBoundItems="true" CssClass="form-control">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBank" runat="server" ControlToValidate="ddlBank"
                                            Display="None" ErrorMessage="Please Select Bank" SetFocusOnError="true" InitialValue="0" ValidationGroup="AddBank">
                                        </asp:RequiredFieldValidator>
                                        <asp:ValidationSummary ID="ValidationSummary6" runat="server" DisplayMode="List" ShowMessageBox="True"
                                            ShowSummary="False" ValidationGroup="AddBank" />
                                    </div>
                                    <div class="form-group col-md-4" runat="server">
                                        <label><span style="color: red;">*</span> Account No</label>
                                        <asp:TextBox ID="txtAcc" runat="server" MaxLength="30" CssClass="form-control"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="ajxacc" runat="server" TargetControlID="txtAcc" FilterType="Numbers"></ajaxToolKit:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtAcc"
                                            Display="None" ErrorMessage="Please Enter Account No" SetFocusOnError="true" ValidationGroup="AddBank">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-4" runat="server">
                                        <label><span style="color: red;">*</span> Branch</label>
                                        <asp:TextBox ID="txtBankbranch" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtBankbranch"
                                            Display="None" ErrorMessage="Please Enter Branch" SetFocusOnError="true" ValidationGroup="AddBank">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-md-4" runat="server">
                                        <label><span style="color: red;">*</span> IFSC Code</label>
                                        <asp:TextBox ID="txtIfsc" runat="server" MaxLength="50" CssClass="form-control" Style="text-transform: uppercase;"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="ajxfIfsc" runat="server" TargetControlID="txtIfsc" FilterType="Numbers,LowercaseLetters"></ajaxToolKit:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtIfsc"
                                            Display="None" ErrorMessage="Please Enter IFSC Code" SetFocusOnError="true" ValidationGroup="AddBank">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="box-footer col-md-12 ">
                                        <p class="text-center">
                                            <asp:Button ID="btnBankDetails" runat="server" CssClass="btn btn-outline-info" Text="Add" ValidationGroup="AddBank" OnClick="btnBankDetails_Click" />
                                            <asp:Button ID="btnBankClear" runat="server" Text="Cancel" OnClick="btnBankClear_Click" CssClass="btn btn-outline-danger" />
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <%--  THIS PANEL IS FOR SHOWING SCHOLARSHIP STATUS--%>
                <div class="row" style="display:none">
                    <div class="col-md-12 form-group">
                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title">Scholarship Details</h3>
                                <div class="box-tools pull-right">
                                    <a id="menu-toggle-5" style="cursor: pointer; transition: 0.8s;" onclick="javascript:toggleExpansion(this,'divScholarship')">
                                        <i class="glyphicon glyphicon-minus text-blue"></i>
                                    </a>
                                </div>
                            </div>
                            <div class="box-body">

                                <div class="col-md-12 from-group" id="divScholarship" runat="server" visible="true">

                                    <div class="form-group col-md-4" runat="server">
                                        <label><span style="color: red;">*</span> Name of the Scholarship</label>
                                        <asp:TextBox ID="txtNameScholarship" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtNameScholarship"
                                            Display="None" ErrorMessage="Please Enter Name of the Scholarship" SetFocusOnError="true" ValidationGroup="AddScholarship">
                                        </asp:RequiredFieldValidator>
                                        <asp:ValidationSummary ID="ValidationSummary7" runat="server" DisplayMode="List" ShowMessageBox="True"
                                            ShowSummary="False" ValidationGroup="AddScholarship" />
                                    </div>
                                    <div class="form-group col-md-4" runat="server">
                                        <label><%--<span style="color: red;">*</span>--%> Organization Name </label>
                                        <asp:TextBox ID="txtOrganizationname" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-4 " id="divMoufrom" runat="server">
                                        <label>
                                            <%--<span style="color: red;">*</span>--%>  Date of Scholarship Applied
                                <%--   <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator5" runat="server" ControlExtender="meeFromDate" ControlToValidate="txtMoufrom"
                                       IsValidEmpty="False" EmptyValueMessage="Please Enter MOU From" InvalidValueMessage=" MOU From date is invalid"
                                       InvalidValueBlurredMessage="*" Display="Dynamic" ValidationGroup="report" />--%>
                                        </label>

                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtMoufrom" runat="server" CssClass="form-control" />

                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender5" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtMoufrom" PopupButtonID="imgCalFromDate" Enabled="true" EnableViewState="true" />
                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txtMoufrom"
                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                AcceptNegative="none" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtMoufrom"
                                                ErrorMessage="Please Select MOU From" ValidationGroup="Show" Display="None"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="form-group col-md-4 " id="divMouto" runat="server">
                                        <label>
                                            <%--<span style="color: red;">*</span>--%>  Date of Scholarship Sanctioned
                                  <%-- <ajaxToolKit:MaskedEditValidator ID="MaskedEditValidator6" runat="server" ControlExtender="meeFromDate" ControlToValidate="txtMouto"
                                       IsValidEmpty="False" EmptyValueMessage="Please Enter MOU To" InvalidValueMessage="MOU To date is invalid"
                                       InvalidValueBlurredMessage="*" Display="Dynamic" ValidationGroup="report" />--%>
                                        </label>

                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar text-blue"></i>
                                            </div>
                                            <asp:TextBox ID="txtMouto" runat="server" CssClass="form-control" onchange="checkDate(3);" />

                                            <ajaxToolKit:CalendarExtender ID="CalendarExtender6" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtMouto" PopupButtonID="imgCalFromDate" Enabled="true" EnableViewState="true" />
                                            <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender4" runat="server" TargetControlID="txtMouto"
                                                Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                AcceptNegative="none" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtMouto"
                                                ErrorMessage="Please Select MOU To" ValidationGroup="Show" Display="None"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="form-group col-md-4" runat="server">
                                        <label><%--<span style="color: red;">*</span>--%> Organization Type</label>
                                        <asp:DropDownList ID="ddlOrganization" runat="server" AppendDataBoundItems="true" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                    <div class="form-group col-md-4" runat="server">
                                        <label><%--<span style="color: red;">*</span>--%> Amount Applied</label>
                                        <asp:TextBox ID="txtAmountApplied" runat="server" CssClass="form-control"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="FteamtApplied" runat="server" FilterType="Numbers,custom" ValidChars="." TargetControlID="txtAmountApplied"></ajaxToolKit:FilteredTextBoxExtender>
                                    </div>
                                    <div class="form-group col-md-4" runat="server">
                                        <label><%--<span style="color: red;">*</span>--%> Amount Sanctioned</label>&nbsp;&nbsp;&nbsp;&nbsp;                                           
                                             <label>Full Fee Waiver </label>
                                        <asp:CheckBox ID="chkfeewavier" runat="server" />

                                        <asp:TextBox ID="txtAmountsanctioned" runat="server" CssClass="form-control" onchange="Checkamt();"></asp:TextBox>
                                        <ajaxToolKit:FilteredTextBoxExtender ID="atkamtsanction" runat="server" FilterType="Numbers,custom" ValidChars="." TargetControlID="txtAmountsanctioned"></ajaxToolKit:FilteredTextBoxExtender>
                                    </div>
                                    <div class="form-group col-md-4" runat="server">
                                        <label><%--<span style="color: red;">*</span>--%> Details of Payment</label>
                                        <asp:TextBox ID="txtDetailspayment" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>


                                </div>
                                <div class="col-md-12" runat="server" id="bankdetails" visible="false">
                                    <label><%--<span style="color: red;">*</span>--%> Student Bank Details :</label>
                                    <asp:Label ID="txtStudentBank" runat="server" Style="font-weight: bold"></asp:Label>

                                </div>


                                <div id="div5" runat="server" style="display: block; transition: 0.8s;" class="col-md-12">
                                    <asp:ListView ID="LvScholarship" runat="server">
                                        <LayoutTemplate>
                                            <div>
                                                <table class="table table-hover table-bordered table-striped">
                                                    <thead>

                                                        <tr class="bg-light-blue">

                                                            <th>Scholarship Name	     
                                                            </th>
                                                            <th>Organization Name			
                                                            </th>
                                                            <th>Applied Date
                                                            </th>
                                                            <th>Sanctioned Date
                                                            </th>
                                                            <th>Organization Type
                                                            </th>
                                                            <th>Amt Applied
                                                            </th>
                                                            <th>Amt Sanctioned	
                                                            </th>
                                                            <th>Full Fee Waiver
                                                            </th>
                                                            <th>Payment Details
                                                            </th>
                                                            <%-- <th>Student Bank Details
                                                                </th>--%>
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
                                            <tr>
                                                <td>
                                                    <%# Eval("SCHOLARSHIP_NAME") %>       
                                                </td>
                                                <td>
                                                    <%# Eval("ORGANIZATION_NAME") %>
                                                </td>
                                                <td>
                                                    <%# Eval("APPLIED_DATE") %>
                                                </td>
                                                <td>
                                                    <%# Eval("SANCTIONED_DATE") %>
                                                </td>
                                                <td>
                                                    <%# Eval("ORGANIZATION_TYPE") %>
                                                </td>
                                                <td>
                                                    <%# Eval("AMT_APPLIED") %>
                                                </td>
                                                <td>
                                                    <%# Eval("AMT_SANCTIONED") %>
                                                </td>
                                                <td>
                                                    <%#Eval("FEE_WAIVER") %>
                                                </td>
                                                <td>
                                                    <%# Eval("PAYMENT_DETAILS") %>
                                                </td>
                                                <%--<td>
                                                        <%#Eval("STUDENT_BANK_DETAILS") %>
                                                    </td>--%>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>

                <div class="box-footer col-md-12" style="display:none">
                    <p class="text-center">
                        <asp:Button ID="btnScholarship" runat="server" CssClass="btn btn-outline-info" Text="Add" ValidationGroup="AddScholarship" OnClick="btnScholarship_Click" />
                        <asp:Button ID="btnScholarshipCancel" runat="server" Text="Cancel" CssClass="btn btn-outline-danger" OnClick="btnScholarshipCancel_Click" />
                    </p>
                </div>

            </div>
        </div>
    </div>
    <%--</div>--%>



    <%--   </ContentTemplate>
          </asp:UpdatePanel>--%>



    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            bindDataTable();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(bindDataTable);
        });

        function bindDataTable() {
            /* To collapse and expand page sections */
            $('#menu-toggle').click(function () {
                $(this).find('i').toggleClass('glyphicon glyphicon-minus text-blue').toggleClass('glyphicon glyphicon-plus text-blue');
            });
            $('#menu-toggle-1').click(function () {
                $(this).find('i').toggleClass('glyphicon glyphicon-minus text-blue').toggleClass('glyphicon glyphicon-plus text-blue');
            });
            $('#menu-toggle-2').click(function () {
                $(this).find('i').toggleClass('glyphicon glyphicon-minus text-blue').toggleClass('glyphicon glyphicon-plus text-blue');
            });
            $('#menu-toggle-3').click(function () {
                $(this).find('i').toggleClass('glyphicon glyphicon-minus text-blue').toggleClass('glyphicon glyphicon-plus text-blue');
            });
            $('#menu-toggle-4').click(function () {
                $(this).find('i').toggleClass('glyphicon glyphicon-minus text-blue').toggleClass('glyphicon glyphicon-plus text-blue');
            });
            $('#menu-toggle-5').click(function () {
                $(this).find('i').toggleClass('glyphicon glyphicon-minus text-blue').toggleClass('glyphicon glyphicon-plus text-blue');
            });
        }

        function toggleExpansion(imageCtl, divId) {
            if (document.getElementById(divId).style.display == "block") {
                document.getElementById(divId).style.display = "none";
                // imageCtl.src = "../IMAGES/expand_blue.jpg";
            }
            else if (document.getElementById(divId).style.display == "none") {
                document.getElementById(divId).style.display = "block";
                // imageCtl.src = "../IMAGES/collapse_blue.jpg";
            }
        }

        function toggleExpansion1(imageCtl, divId) {
            if (document.getElementById(divId).style.display == "block") {
                document.getElementById(divId).style.display = "none";
                imageCtl.src = "../IMAGES/expand_blue.jpg";
            }
            else if (document.getElementById(divId).style.display == "none") {
                document.getElementById(divId).style.display = "block";
                imageCtl.src = "../IMAGES/collapse_blue.jpg";
            }
        }

    </script>

    <script language="javascript" type="text/javascript">
        function divexpandcollapse(divname) {
            var div = document.getElementById(divname);
            var img = document.getElementById('img' + divname);
            if (div.style.display == "none") {
                div.style.display = "inline";
                img.src = "../IMAGES/minus.png";
            }
            else {
                div.style.display = "none";
                img.src = "/images/plus.gif";
            }
        }
    </script>

    <script language="javascript" type="text/javascript">

        var dt1 = Convert.ToDateTime("txtProjectFromdate.Text");
        var dt2 = Convert.ToDateTime("04/03/2012");
        var ts = dt1.Subtract(dt2);
        var day = ts.Days;

    </script>

    <script type="text/javascript">
        function UserDeleteConfirmation() {
            if (confirm("Are you sure you want to Delete this File?"))
                return true;
            else
                return false;
        }
    </script>
    <script>
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                alert("Mobile No should be Only Digits.");

                return false;
            }
            return true;
        }

        function ValidateNo() {
            var phoneNo = document.getElementById('<%=txtmobile.ClientID%>').value;
            //alert(phoneNo);            
            if (phoneNo.length < 10 || phoneNo.length > 10) {
                alert('Entered Mobile No : ' + phoneNo + ' is not Valid, Please Enter 10 Digit Mobile No.');
                document.getElementById('<%=txtmobile.ClientID%>').value = '';
                document.getElementById('<%=txtmobile.ClientID%>').focus();
                return false;
            }
            // alert("Success ");
            return true;
        }

        function EmailValidation() {
            var email = document.getElementById('<%=txtEmailid.ClientID%>').value;

            var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;

            if (reg.test(email) == false) {
                alert('Entered Email Id : ' + email + ' is Invalid');
                document.getElementById('<%=txtEmailid.ClientID%>').value = '';
                document.getElementById('<%=txtEmailid.ClientID%>').focus();
                return false;
            }
            return true;
        }
    </script>



    <div id="divMsg" runat="server">
    </div>

</asp:Content>
