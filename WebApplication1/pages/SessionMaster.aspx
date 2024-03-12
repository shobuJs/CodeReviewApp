<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="SessionMaster.aspx.cs" Inherits="Academic_SessionCreate" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
    <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>
    <style>
        .dynamic-nav-tabs li.active a {
            color: #255282;
            background-color: #fff;
            border-top: 3px solid #255282 !important;
            border-color: #255282 #255282 #fff !important;
        }

        .chiller-theme .nav-tabs.dynamic-nav-tabs > li.active {
            border-radius: 8px;
            background-color: transparent !important;
        }
    </style>
    <asp:UpdatePanel ID="updCourseCreation" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="nav-tabs-custom mt-2 col-12 pb-4" id="myTabContent">
                            <ul class="nav nav-tabs dynamic-nav-tabs" role="tablist">
                                <li class="nav-item " id="divAcademic" runat="server">
                                    <asp:LinkButton ID="lkAcademic" runat="server" CssClass="nav-link" TabIndex="1" OnClick="lkAcademic_Click">Academic Session</asp:LinkButton></li>
                                <li class="nav-item active" id="divSessionCreation" runat="server">
                                    <asp:LinkButton ID="lkSessionCreation" runat="server" CssClass="nav-link" TabIndex="2" OnClick="lkSessionCreation_Click">Session Creation</asp:LinkButton></li>
                                <li class="nav-item" id="divMapping" runat="server">
                                    <asp:LinkButton ID="lkMapping" runat="server" CssClass="nav-link" TabIndex="3" OnClick="lkMapping_Click">Session College Mapping</asp:LinkButton></li>

                            </ul>
                        </div>
                        <div class="tab-content">
                            <div class="tab-pane fade show active" id="divAcademicSession" role="tabpanel" runat="server" aria-labelledby="TotalAplied-tab" visible="false">
                                <div>
                                    <asp:HiddenField ID="hfdStat" runat="server" ClientIDMode="Static" />
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updInvigilation"
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
                                <asp:UpdatePanel ID="updInvigilation" runat="server">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="col-md-12 col-sm-12 col-12">

                                                <div class="box-header with-border">
                                                    <h3 class="box-title">
                                                        <asp:Label ID="lblRAISession" runat="server">Academic Session </asp:Label></h3>
                                                </div>

                                                <div class="box-body">
                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="form-group col-lg-4 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <asp:Label ID="lblSession" runat="server" Font-Bold="true">Academic Session</asp:Label>
                                                                    <%--  <label>Department</label>--%>
                                                                </div>
                                                                <asp:TextBox ID="txtAcademicName" runat="server" CssClass="form-control" MaxLength="100" TabIndex="5"
                                                                    ToolTip="Please Enter Academic Session Name" />
                                                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtAcademicName"
                                            Display="None" ErrorMessage="Please Select Academic Session Name" InitialValue="" ValidationGroup="SubmitSession" />--%>
                                                            </div>
                                                            <div class="form-group col-lg-2 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <asp:Label ID="lblActive" runat="server" Font-Bold="true"></asp:Label>

                                                                </div>
                                                                <div class="switch form-inline">
                                                                    <input type="checkbox" id="switch" name="switch" class="switch" checked tabindex="8" />
                                                                    <label data-on="Active" data-off="Inactive" for="switch"></label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>



                                                    <div class="col-12 btn-footer">
                                                        <asp:LinkButton ID="btnSave" runat="server" CssClass="btn btn-outline-info" TabIndex="7" OnClick="btnSave_Click" ValidationGroup="SubmitSession" Visible="true" ClientIDMode="Static">Submit</asp:LinkButton>
                                                        <asp:LinkButton ID="btncanceldata" runat="server" CssClass="btn btn-outline-danger" TabIndex="8" Visible="true" OnClick="btncanceldata_Click">Cancel</asp:LinkButton>
                                                        <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="SubmitSession"
                                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                        <asp:ValidationSummary ID="ValidationSummary4" runat="server" ValidationGroup="Submits"
                                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />

                                                    </div>


                                                    <div class="col-12">
                                                        <asp:Panel ID="Panel1" runat="server">
                                                            <asp:ListView ID="lvAcademic" runat="server" Visible="true">
                                                                <LayoutTemplate>
                                                                    <div class="sub-heading">
                                                                        <h5>Academic Session  List</h5>
                                                                    </div>
                                                                    <table class="table table-striped table-bordered nowrap display" id="mytable" style="width: 100%">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>Edit </th>
                                                                                <th>Academic Session</th>
                                                                                <th>Status</th>


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
                                                                            <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/images/edit.gif"
                                                                                AlternateText="Edit Record" CommandArgument='<%# Eval("ACADEMIC_NO") %>' OnClick="btnEditAcademic_Click" />
                                                                        </td>
                                                                        <td><%# Eval("ACEDEMIC_NAME") %></td>
                                                                        <td>
                                                                            <asp:Label ID="status" runat="server" class="badge" Text='<%#Eval("STATUS") %>'></asp:Label>
                                                                        </td>


                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>

                                                        </asp:Panel>

                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <%-- <asp:AsyncPostBackTrigger ControlID="btnSave" />--%>
                                    </Triggers>
                                </asp:UpdatePanel>

                            </div>

                            <div class="tab-pane fade show active" id="divapliedtab" role="tabpanel" runat="server" aria-labelledby="TotalAplied-tab" visible="false">
                                <div>
                                    <asp:HiddenField ID="hdnStatus" runat="server" ClientIDMode="Static" />
                                    <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updSession"
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

                                <asp:UpdatePanel ID="updSession" runat="server">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="col-md-12 col-sm-12 col-12">

                                                <div id="div1" runat="server"></div>
                                                <div class="box-header with-border">
                                                    <%--  <h3 class="box-title">SESSION CREATION</h3>--%>
                                                    <h3 class="box-title">
                                                        <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label>

                                                        <div>
                                                            <button class="shortcut-btn"><i class="fa fa-keyboard-o" aria-hidden="true"></i>Shift ^ CN</button>
                                                        </div>
                                                    </h3>
                                                </div>

                                                <div class="box-body">
                                                    <div class="col-12">
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Session List</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                                    TabIndex="1">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                            
                                                            <%-- <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                             <asp:Label ID="lblDYCollege" runat="server" Font-Bold="true"></asp:Label>
                                           
                                        </div>
                                        <asp:DropDownList ID="ddlCollege" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged"
                                            ValidationGroup="teacherallot">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                      
                                    </div>--%>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <asp:Label ID="lblSessionLongName" runat="server" Font-Bold="true"></asp:Label>
                                                                    <%--<label>Session Long Name</label>--%>
                                                                </div>
                                                                <asp:TextBox ID="txtSLongName" runat="server" CssClass="form-control" MaxLength="100" TabIndex="2"
                                                                    ToolTip="Please Enter Session Long Name" />
                                                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtSLongName"
                                                                Display="None" ErrorMessage="Please Enter Session Long Name" InitialValue="" ValidationGroup="submits"/>
                                                            </div>


                                                            <div class="form-group col-lg-3 col-md-6 col-12" style="display: none;">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Session Start Date</label>
                                                                </div>
                                                                <div class="input-group date">
                                                                    <div class="input-group-addon">
                                                                        <i id="dvcal1" runat="server" class="fa fa-calendar"></i>
                                                                    </div>
                                                                    <asp:TextBox ID="txtStartDate" runat="server" ValidationGroup="ubmit" onpaste="return false;"
                                                                        TabIndex="3" ToolTip="Please Enter Session Start Date" CssClass="form-control" Style="z-index: 0;" />
                                                                    <ajaxToolKit:CalendarExtender ID="ceStartDate" runat="server" Format="dd/MM/yyyy"
                                                                        TargetControlID="txtStartDate" PopupButtonID="dvcal1" />
                                                                    <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ControlToValidate="txtStartDate"
                                                                        Display="None" ErrorMessage="Please Enter Session Start Date" SetFocusOnError="True"
                                                                        ValidationGroup="ubmit" />
                                                                    <ajaxToolKit:MaskedEditExtender ID="meeStartDate" runat="server" OnInvalidCssClass="errordate"
                                                                        TargetControlID="txtStartDate" Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date"
                                                                        DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                                                    <ajaxToolKit:MaskedEditValidator ID="mevStartDate" runat="server" ControlExtender="meeStartDate"
                                                                        ControlToValidate="txtStartDate" EmptyValueMessage="Please Enter Start Date"
                                                                        InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                                        TooltipMessage="Please Enter Start Date" EmptyValueBlurredText="Empty" InvalidValueBlurredMessage="Invalid Date"
                                                                        ValidationGroup="ubmit" SetFocusOnError="True" />
                                                                </div>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12" style="display: none;">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Session End Date</label>
                                                                </div>
                                                                <div class="input-group date">
                                                                    <div class="input-group-addon">
                                                                        <i id="dvcal2" runat="server" class="fa fa-calendar"></i>
                                                                    </div>
                                                                    <asp:TextBox ID="txtEndDate" runat="server" ValidationGroup="ubmit" TabIndex="4"
                                                                        ToolTip="Please Enter Session End Date" CssClass="form-control" Style="z-index: 0;" />
                                                                    <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                                                                        TargetControlID="txtEndDate" PopupButtonID="dvcal2" />
                                                                    <asp:RequiredFieldValidator ID="rfvEndDate" runat="server" SetFocusOnError="True"
                                                                        ErrorMessage="Please Enter Session End Date" ControlToValidate="txtEndDate" Display="None"
                                                                        ValidationGroup="ubmit" />
                                                                    <ajaxToolKit:MaskedEditExtender ID="meeEndDate" runat="server" OnInvalidCssClass="errordate"
                                                                        TargetControlID="txtEndDate" Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date"
                                                                        DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="True" />
                                                                    <ajaxToolKit:MaskedEditValidator ID="mevEndDate" runat="server" ControlExtender="meeEndDate"
                                                                        ControlToValidate="txtEndDate" EmptyValueMessage="Please Enter Session End Date"
                                                                        InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)" Display="None"
                                                                        TooltipMessage="Please Enter Session End Date" EmptyValueBlurredText="Empty"
                                                                        InvalidValueBlurredMessage="Invalid Date" ValidationGroup="ubmit" SetFocusOnError="True" />
                                                                </div>
                                                            </div>
                                                            <!--============================================-->

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <asp:Label ID="lblSessionSName" runat="server" Font-Bold="true"></asp:Label>
                                                                    <%-- <label>Session Short Name</label>--%>
                                                                </div>
                                                                <asp:TextBox ID="txtSShortName" runat="server" CssClass="form-control" MaxLength="100" TabIndex="5"
                                                                    ToolTip="Please Enter Session Short Name" />
                                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSShortName"
                                                                Display="None" ErrorMessage="Please Enter Session Short Name" InitialValue="" ValidationGroup="submits"/>
                                                                <asp:HiddenField ID="hdnDate" runat="server" />
                                                            </div>
                                                            <!--===== added by gaurav on 21-05-2021=====-->
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <%--<label>Session Start End Date</label>--%>
                                                                    <asp:Label ID="lblStartEndDate" runat="server" Font-Bold="true"></asp:Label>
                                                                </div>
                                                                <div id="picker" class="form-control">
                                                                    <i class="fa fa-calendar"></i>&nbsp;
                                    <span id="date"></span>
                                                                    <i class="fa fa-angle-down" aria-hidden="true" style="float: right; padding-top: 4px; font-weight: bold;"></i>
                                                                </div>
                                                            </div>

                                                            <!--============================================-->
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <asp:Label ID="lblOddEvenYear" runat="server" Font-Bold="true"></asp:Label>
                                                                    <%--   <label>Odd Year/Even Year</label>--%>
                                                                </div>
                                                                <asp:DropDownList ID="ddlOddEven" runat="server" TabIndex="6" ToolTip="Please Select Odd/Even" CssClass="form-control" data-select2-enable="true">
                                                                </asp:DropDownList>
                                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlOddEven"
                                                                Display="None" ErrorMessage="Please Select Odd Year/Even Year" InitialValue="0" ValidationGroup="submits"/>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12" style="display: none">
                                                                <div class="label-dynamic">
                                                                    <asp:Label ID="lblSessionNHindi" runat="server" Font-Bold="true"></asp:Label>
                                                                    <%--   <label>Session Name(Hindi):</label>--%>
                                                                </div>
                                                                <asp:TextBox ID="txtSessName_Hindi" runat="server" CssClass="form-control" MaxLength="25" TabIndex="8"
                                                                    ToolTip="Please Select Status"></asp:TextBox>
                                                            </div>

                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <asp:Label ID="lblStatus" runat="server" Font-Bold="true"></asp:Label>
                                                                    <%--       <label>Status</label>--%>
                                                                </div>
                                                                <asp:DropDownList ID="ddlStatus" runat="server" TabIndex="7" ToolTip="Please Select Status" CssClass="form-control" data-select2-enable="true">
                                                                </asp:DropDownList>
                                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlStatus"
                                                                Display="None" ErrorMessage="Please Select Status" InitialValue="0" ValidationGroup="submits"/>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Academic Session</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlAcademicSession" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                                    TabIndex="12">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlAcademicSession"
                                                                Display="None" ErrorMessage="Please Select Academic Session" InitialValue="0" ValidationGroup="submits"/>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <label>Year</label>
                                                                </div>
                                                                <asp:DropDownList ID="ddlYearName" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true"
                                                                    TabIndex="12">
                                                                   <%-- <asp:ListItem Value="0">Please Select</asp:ListItem>--%>
                                                                </asp:DropDownList>
                                                               <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlYearName"
                                                                Display="None" ErrorMessage="Please Select Year" InitialValue="0" ValidationGroup="submits"/>
                                                            </div>
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup></sup>
                                                                    <asp:Label ID="Label3" runat="server" Font-Bold="true"></asp:Label>

                                                                </div>
                                                                <div class="switch form-inline">
                                                                    <input type="checkbox" id="switchs" name="switchs" class="switchs" checked tabindex="8" />
                                                                    <label data-on="Start" data-off="Stop" for="switchs"></label>
                                                                </div>
                                                            </div>
                                                           <%-- <div class="col-lg-3 col-md-6 col-12 mt-lg-4 mt-2 mb-2">
                                                                <asp:CheckBox ID="chkFlock" Text="&nbsp;&nbsp;START" runat="server" />
                                                            </div>--%>

                                                        </div>
                                                    </div>
                                                    <div class="col-12 btn-footer">
                                                        <%--  <asp:LinkButton ID="lnkbtn1" runat="server" ValidationGroup="report" type="button" CssClass="btn btn-outline-primary">New Btn</asp:LinkButton> 
                                <asp:Label ID="lblStatus1" runat="server" SkinID="Errorlbl"></asp:Label>--%>

                                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ToolTip="Submit" ValidationGroup="submits" ClientIDMode="Static"
                                                            CssClass="btn btn-outline-info btnX" OnClick="btnSubmit_Click" TabIndex="10" />

                                                        <%--<asp:LinkButton ID="btnSubmit" runat="server" ValidationGroup="submit"
                                    OnClick="btnSubmit_Click" TabIndex="9" CssClass="btn btn-outline-primary">Submit</asp:LinkButton> <%--data-toggle="modal" data-target="#myModal-feedback" --%>
                                                        <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" Text="Report" ValidationGroup="report"
                                                            TabIndex="11" CssClass="btn btn-outline-primary" Visible="false" />
                                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                                            TabIndex="10" CssClass="btn btn-outline-danger" />
                                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                                            ShowSummary="false" DisplayMode="List" ValidationGroup="submits" />
                                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true"
                                                            ShowSummary="false" DisplayMode="List" ValidationGroup="report" />
                                                    </div>
                                                    <div class="col-12">
                                                        <asp:Panel ID="pnlSession" runat="server" Visible="false">
                                                            <asp:ListView ID="lvSession" runat="server">
                                                                <LayoutTemplate>
                                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>Edit</th>
                                                                                <%--    <th>Faculty /School Name</th>--%>
                                                                                <th>Start Date</th>
                                                                                <th>End Date</th>
                                                                                <th>Session Long Name</th>
                                                                                <th>Session Short Name</th>
                                                                                <th>Session Type</th>
                                                                                <th>Status</th>
                                                                                <th>Year</th>
                                                                                <th>Academic Session</th>

                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <tr id="itemPlaceholder" runat="server" />
                                                                        </tbody>
                                                                </LayoutTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/images/edit.png"
                                                                                CommandArgument='<%# Eval("SESSIONNO")%>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                                                OnClick="btnEdit_Click" TabIndex="12" />
                                                                        </td>
                                                                        <%-- <td>
                                                    <asp:Label ID="lblColName" runat="server" ToolTip='<%#Eval("SESSIONNO") %>' Text='<%# Eval("COLLEGE_NAME") %>'></asp:Label></td>--%>
                                                                        <td><%# Eval("SESSION_STDATE","{0:dd-MMM-yyyy}") %></td>
                                                                        <td><%# Eval("SESSION_ENDDATE", "{0:dd-MMM-yyyy}")%></td>
                                                                        <td><%# Eval("SESSION_PNAME")%></td>
                                                                        <td><%# Eval("SESSION_NAME")%></td>
                                                                        <td><%# Eval("ODD_EVEN")%></td>
                                                                        <td><%# Eval("FLOCK")%></td>
                                                                        <td><%# Eval("ACADEMIC_YEAR")%></td>
                                                                        <td><%# Eval("ACEDEMIC_NAME")%></td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                                <%--<FooterTemplate>
                                            </tbody></table>
                                        </FooterTemplate>--%>
                                                            </asp:ListView>
                                                        </asp:Panel>
                                                    </div>

                                                </div>
                                            </div>

                                        </div>

                                        <!-- Feedback Modal -->
                                        <div class="modal fade feedback-detail" id="myModal-feedback" role="dialog">
                                            <div class="modal-dialog modal-md">

                                                <!-- Modal content-->
                                                <div class="modal-content">
                                                    <div class="modal-header">
                                                        <div class="row">
                                                            <div class="col-sm-10 offset-sm-1 title-main">
                                                                <h2 class="modal-title">Feedback <i class="fa fa-commenting-o fedbck-icon" aria-hidden="true"></i></h2>
                                                                <h5 class="modal-sub-title">Your Feedback is Important to Us</h5>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="modal-body">
                                                        <div class="feedback">
                                                            <div class="row">
                                                                <div class="col-sm-10 offset-sm-1 feed-content">
                                                                    <div class="">
                                                                        <div class="col-md-12">
                                                                            <form class="feed-back">
                                                                                <div class="form-group">
                                                                                    <label for="comment1">1. What should we do future improve your experince?</label><br />
                                                                                    <label class="radio-answer">
                                                                                        <input type="radio" name="optradio">
                                                                                        Ease navigation</label>
                                                                                    <label class="radio-answer">
                                                                                        <input type="radio" name="optradio">
                                                                                        Fullfill Requirements</label>
                                                                                    <label class="radio-answer">
                                                                                        <input type="radio" name="optradio">
                                                                                        Others</label>
                                                                                </div>
                                                                            </form>
                                                                        </div>
                                                                        <div class="col-md-12">
                                                                            <form class="feed-back">
                                                                                <div class="form-group">
                                                                                    <label for="comment1">2. How satisfied are you overall with the support of our Product?</label><br />
                                                                                    <label class="radio-answer">
                                                                                        <input type="radio" name="optradio">
                                                                                        Easy</label>
                                                                                    <label class="radio-answer">
                                                                                        <input type="radio" name="optradio">
                                                                                        Normal</label>
                                                                                    <label class="radio-answer">
                                                                                        <input type="radio" name="optradio">
                                                                                        Difficult</label>
                                                                                    <label class="radio-answer">
                                                                                        <input type="radio" name="optradio">
                                                                                        Too Difficult</label>
                                                                                </div>
                                                                            </form>
                                                                        </div>
                                                                        <div class="col-md-12">
                                                                            <form class="feed-back">
                                                                                <div class="form-group">
                                                                                    <label for="comment1">3. How easy was it to use our Platform?</label><br />
                                                                                    <label class="radio-answer">
                                                                                        <input type="radio" name="optradio">
                                                                                        Easy</label>
                                                                                    <label class="radio-answer">
                                                                                        <input type="radio" name="optradio">
                                                                                        Normal</label>
                                                                                    <label class="radio-answer">
                                                                                        <input type="radio" name="optradio">
                                                                                        Difficult</label>
                                                                                    <label class="radio-answer">
                                                                                        <input type="radio" name="optradio">
                                                                                        Too Difficult</label>
                                                                                </div>
                                                                            </form>
                                                                        </div>
                                                                        <div class="col-md-12">
                                                                            <form class="feed-back">
                                                                                <div class="form-group">
                                                                                    <label for="comment1">4. How information was easy to understand?</label><br />
                                                                                    <label class="radio-answer">
                                                                                        <input type="radio" name="optradio">
                                                                                        Easy</label>
                                                                                    <label class="radio-answer">
                                                                                        <input type="radio" name="optradio">
                                                                                        Normal</label>
                                                                                    <label class="radio-answer">
                                                                                        <input type="radio" name="optradio">
                                                                                        Difficult</label>
                                                                                    <label class="radio-answer">
                                                                                        <input type="radio" name="optradio">
                                                                                        Too Difficult</label>
                                                                                </div>
                                                                            </form>
                                                                        </div>
                                                                        <div class="col-md-12">
                                                                            <form class="feed-back">
                                                                                <div class="form-group">
                                                                                    <label for="comment1">5. How easy was it to use our Platform?</label><br />
                                                                                    <label class="radio-answer">
                                                                                        <input type="radio" name="optradio">
                                                                                        Easy</label>
                                                                                    <label class="radio-answer">
                                                                                        <input type="radio" name="optradio">
                                                                                        Normal</label>
                                                                                    <label class="radio-answer">
                                                                                        <input type="radio" name="optradio">
                                                                                        Difficult</label>
                                                                                    <label class="radio-answer">
                                                                                        <input type="radio" name="optradio">
                                                                                        Too Difficult</label>
                                                                                </div>
                                                                            </form>
                                                                        </div>
                                                                        <div class="col-sm-10">
                                                                            <form class="feed-back">
                                                                                <div class="form-group" style="margin-bottom: 0px;">
                                                                                    <label for="comment1">Do you have any other comments, feedback?</label>
                                                                                    <textarea class="form-control" rows="2" id="comment1"></textarea>
                                                                                </div>
                                                                            </form>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="modal-footer">
                                                        <div class="buton-bottom">
                                                            <button type="button" class="btn btn-outline-primary">Submit</button>
                                                            <button type="button" class="btn btn-outline-danger mr-2" data-dismiss="modal">Cancel</button>
                                                        </div>
                                                    </div>


                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="tab-pane fade show active" id="divmap" role="tabpanel" runat="server" aria-labelledby="TotalAplied-tab" visible="false">
                                <div>
                                     <asp:HiddenField ID="hdnMappin" runat="server" ClientIDMode="Static" />
                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UPDCOLLEGEMAP"
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
                                <asp:UpdatePanel ID="UPDCOLLEGEMAP" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="col-md-12 col-sm-12 col-12">

                                                <div id="div2" runat="server"></div>
                                                <div class="box-header with-border">
                                                    <%--  <h3 class="box-title">SESSION CREATION</h3>--%>
                                                    <h3 class="box-title">
                                                        <asp:Label ID="Label1" runat="server">Session College Mapping</asp:Label>


                                                    </h3>
                                                </div>
                                                <div class="box-body">
                                                    <div class="col-12 mt-3">
                                                        <div class="row">
                                                            <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <asp:Label ID="lblsessionClg" runat="server" Font-Bold="true">Session</asp:Label>
                                                                    <%--  <label>Session </label>--%>
                                                                </div>
                                                                <asp:DropDownList ID="ddlSessionCollege" runat="server" TabIndex="3" CssClass="form-control" AppendDataBoundItems="true" ValidationGroup="submit2"  data-select2-enable="true">
                                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlSessionCollege"
                                                                    Display="None" ErrorMessage="Please Select Session" ValidationGroup="submit" InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                            </div>

                                                            <div class="form-group col-lg-5 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup>* </sup>
                                                                    <asp:Label ID="lblDYddlSchool" runat="server" Font-Bold="true">Faculty /School Name</asp:Label>
                                                                </div>
                                                                <asp:ListBox ID="ddlCollege" runat="server" SelectionMode="Multiple" TabIndex="4"
                                                                    CssClass="form-control multi-select-demo" AppendDataBoundItems="true"></asp:ListBox>
                                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlCollege"
                                                                    Display="None" ErrorMessage="Please Select Faculty /School Name" ValidationGroup="submit" InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>

                                                            </div>
                                                              <div class="form-group col-lg-3 col-md-6 col-12">
                                                                <div class="label-dynamic">
                                                                    <sup></sup>
                                                                    <asp:Label ID="Label4" runat="server" Font-Bold="true"></asp:Label>

                                                                </div>
                                                                <div class="switch form-inline">
                                                                    <input type="checkbox" id="switchM" name="switchM" class="switchM" checked tabindex="8" />
                                                                    <label data-on="Active" data-off="Inactive" for="switchM"></label>
                                                                </div>
                                                            </div>
                                                            

                                                        </div>
                                                    </div>

                                                    <div class="col-12 btn-footer">
                                                        <asp:Button ID="btnSubmit2" runat="server" Text="Submit" ValidationGroup="submitM" ClientIDMode="Static"
                                                            TabIndex="5" CssClass="btn btn-outline-info" OnClick="btnSubmit2_Click" />
                                                        <asp:Button ID="btnReport2" runat="server" Text="Report"
                                                            CssClass="btn btn-outline-info" Style="display: none;" />
                                                        <asp:Button ID="btnCancel2" runat="server" Text="Cancel"
                                                            TabIndex="6" CssClass="btn btn-outline-danger" OnClick="btnCancel2_Click" />
                                                        <asp:ValidationSummary ID="ValidationSummary5" runat="server" ValidationGroup="submitM"
                                                            ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                                    </div>

                                                    <div class="col-12">
                                                        <asp:Panel ID="pnlCollegeMap" runat="server">
                                                            <asp:ListView ID="lvCollegeMap" runat="server">
                                                                <LayoutTemplate>
                                                                    <table class="table table-striped table-bordered nowrap display" style="width: 80%" id="tblStudents">
                                                                        <thead class="bg-light-blue">
                                                                            <tr>
                                                                                <th>Edit
                                                                                </th>
                                                                                <th>Faculty /School Name
                                                                                </th>
                                                                                <th>Start Date
                                                                                </th>
                                                                                <th>End Date
                                                                                </th>
                                                                                <th>Session Long Name
                                                                                </th>
                                                                                <th>Session Short Name
                                                                                </th>
                                                                                <th>Academic Year
                                                                                </th>
                                                                                <th>Status
                                                                                </th>
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
                                                                            <asp:ImageButton ID="btnEditSess" runat="server" CausesValidation="false" ImageUrl="~/images/edit.png"
                                                                                CommandArgument='<%# Eval("SESSIONID")%>' AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEditSess_Click"
                                                                                TabIndex="12" />
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("COLLEGE_NAME") %>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("SDATE") %>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("EDATE")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("SESSION_PNAME")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("SESSION_NAME")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("ACADEMIC_YEAR")%>
                                                                        </td>

                                                                        <td>
                                                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("STATUS")%>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:ListView>
                                                        </asp:Panel>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                     <%--   <asp:PostBackTrigger ControlID="btnSubmit2" />--%>
                                        <asp:PostBackTrigger ControlID="btnCancel2" />
                                        <asp:PostBackTrigger ControlID="btnReport2" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
        </Triggers>
    </asp:UpdatePanel>

    <script type="text/javascript">

        function checkSessionList() {
            var ddl = document.getElementById('<%= ddlSession.ClientID %>');

            if (ddl.value == "0") {
                alert('Please Select Session from Session List for Modifying');
                return false;
            }
            else
                return true;
        }
    </script>

    <!-- ========= Daterange Picker With Full Functioning Script added by gaurav 21-05-2021 ========== -->
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

    <script>
        function SetStat(val) {
            $('#switch').prop('checked', val);

        }

        var summary = "";
        $(function () {

            $('#btnSave').click(function () {
                localStorage.setItem("currentId", "#btnSave,Submit");
                debugger;
                ShowLoader('#btnSave');

                if ($('#txtAcademicName').val() == "")
                    summary += '<br>Please Enter Academic Name';


                if (summary != "") {
                    customAlert(summary);
                    summary = "";
                    return false
                }
                $('#hfdStat').val($('#switch').prop('checked'));

            });
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSave').click(function () {
                    localStorage.setItem("currentId", "#btnSave,Submit");
                    ShowLoader('#btnSave');

                    if ($('#txtAcademicName').val() == "")
                        summary += '<br>Please Enter Academic Name';


                    if (summary != "") {
                        customAlert(summary);
                        summary = "";
                        return false
                    }
                    $('#hfdStat').val($('#switch').prop('checked'));

                });
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
  <%--  <script>
        function SetStatActive(val) {
            $('#rdActive').prop('checked', val);
        }
        function SetStatStart(val) {
            $('#rdStart').prop('checked', val);
        }

        function validate() {

            $('#hfdActive').val($('#rdActive').prop('checked'));
            $('#hfdStart').val($('#rdStart').prop('checked'));

            //var ddlCollege = $("[id$=ddlCollege]").attr("id");
            //var ddlCollege = document.getElementById(ddlCollege);
            //if (ddlCollege.value == 0) {
            //    alert('Please Select School/Institute Name.', 'Warning!');
            //    $(ddlCollege).focus();
            //    return false;
            //}

            var idtxtweb = $("[id$=txtSLongName]").attr("id");
            var txtweb = document.getElementById(idtxtweb);
            if (txtweb.value.length == 0) {
                alert('Please Enter Session Long Name', 'Warning!');
                //$(txtweb).css('border-color', 'red');
                $(txtweb).focus();
                return false;
            }

            var idtxtweb = $("[id$=txtSShortName]").attr("id");
            var txtweb = document.getElementById(idtxtweb);
            if (txtweb.value.length == 0) {
                alert('Please Enter Session Short Name', 'Warning!');
                //$(txtweb).css('border-color', 'red');
                $(txtweb).focus();
                return false;
            }

            var idtxtweb = $("[id$=txtacadyear]").attr("id");
            var txtweb = document.getElementById(idtxtweb);
            if (txtweb.value.length == 0) {
                alert('Please Enter Academic Year', 'Warning!');
                //$(txtweb).css('border-color', 'red');
                $(txtweb).focus();
                return false;
            }
        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmit').click(function () {
                    validate();
                });
            });
        });
    </script>--%>

    <script>
        function SetSessi(val) {
            $('#switchs').prop('checked', val);
        }
        var summary = "";
        $(function () {
            $('#btnSubmit').click(function () {
                localStorage.setItem("currentId", "#btnSubmit,Submit");
                debugger;
                if (summary != "") {
                    customAlert(summary);
                    summary = "";
                    return false
                }
                $('#hdnStatus').val($('#switchs').prop('checked'));
            });
        });
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $(function () {
                $('#btnSubmit').click(function () {
                    localStorage.setItem("currentId", "#btnSubmit,Submit");
                    if (summary != "") {
                        customAlert(summary);
                        summary = "";
                        return false
                    }
                    $('#hdnStatus').val($('#switchs').prop('checked'));
                });
            });
        });
    </script>

       <script>
           function SetSessi(val) {
               $('#switchs').prop('checked', val);
           }
           var summary = "";
           $(function () {
               $('#btnSubmit').click(function () {
                   localStorage.setItem("currentId", "#btnSubmit,Submit");
                   debugger;
                   if (summary != "") {
                       customAlert(summary);
                       summary = "";
                       return false
                   }
                   $('#hdnStatus').val($('#switchs').prop('checked'));
               });
           });
           var prm = Sys.WebForms.PageRequestManager.getInstance();
           prm.add_endRequest(function () {
               $(function () {
                   $('#btnSubmit').click(function () {
                       localStorage.setItem("currentId", "#btnSubmit,Submit");
                       if (summary != "") {
                           customAlert(summary);
                           summary = "";
                           return false
                       }
                       $('#hdnStatus').val($('#switchs').prop('checked'));
                   });
               });
           });
    </script>

        <script>
            function SetSessiMa(val) {
                $('#switchM').prop('checked', val);
            }
            var summary = "";
            $(function () {
                $('#btnSubmit2').click(function () {
                    localStorage.setItem("currentId", "#btnSubmit2,Submit");
                    debugger;
                    if (summary != "") {
                        customAlert(summary);
                        summary = "";
                        return false
                    }
                    $('#hdnMappin').val($('#switchM').prop('checked'));
                });
            });
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $(function () {
                    $('#btnSubmit2').click(function () {
                        localStorage.setItem("currentId", "#btnSubmit2,Submit");
                        if (summary != "") {
                            customAlert(summary);
                            summary = "";
                            return false
                        }
                        $('#hdnMappin').val($('#switchM').prop('checked'));
                    });
                });
            });
    </script>
</asp:Content>
