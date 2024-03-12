<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="RegisterStudentforNCC.aspx.cs" Inherits="ACADEMIC_RegisterStudentforNCC" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updactivity"
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

    <asp:UpdatePanel ID="updactivity" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">STUDENT REGISTRATION</h3>
                            <div style="color: RED; font-weight: bold" class="pull-right">
                                <span>Note : * marked fields are mandatory</span>
                            </div>
                        </div>
                        <!-- form start -->
                        <div class="box-body">
                            <div class="form-group col-md-12" id="trstype" runat="server">
                                <asp:RadioButtonList ID="rdoactivity" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                                    AppendDataBoundItems="True" OnSelectedIndexChanged="rdoactivity_SelectedIndexChanged">
                                    <asp:ListItem Value="1" Selected="True" style="margin: 20px">NCC</asp:ListItem>
                                    <asp:ListItem Value="2" style="margin: 20px">NSS</asp:ListItem>
                                    <asp:ListItem Value="3" style="margin: 20px">CLUB</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                            <div class="col-md-12 form-group">
                                <%-- <div class="form-group col-md-2"></div>--%>
                                <div class="form-group col-md-4" runat="server" id="divncc">
                                    <span style="color: red">*</span>
                                    <label for="city">NCC Type</label>
                                    <asp:DropDownList ID="ddlncctype" runat="server" AppendDataBoundItems="True" CssClass="form-control"
                                        AutoPostBack="True" TabIndex="1" OnSelectedIndexChanged="ddlncctype_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvncc" runat="server" ControlToValidate="ddlncctype"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select NCC Type" ValidationGroup="Submit">
                                    </asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-md-4" runat="server" id="divnss" visible="false">
                                    <span style="color: red">*</span>
                                    <label for="city">NSS Type</label>
                                    <asp:DropDownList ID="ddlnsstype" runat="server" AppendDataBoundItems="True" CssClass="form-control"
                                        AutoPostBack="True" TabIndex="2" OnSelectedIndexChanged="ddlnsstype_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvnsstype" runat="server" ControlToValidate="ddlnsstype"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select NSS Type" ValidationGroup="Submit">
                                    </asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-md-4" runat="server" id="divclub" visible="false">
                                    <span style="color: red">*</span>
                                    <label for="city">CLUB Type</label>
                                    <asp:DropDownList ID="ddlclubtype" runat="server" AppendDataBoundItems="True" CssClass="form-control"
                                        AutoPostBack="True" TabIndex="3" OnSelectedIndexChanged="ddlclubtype_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvclubtype" runat="server" ControlToValidate="ddlclubtype"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select CLUB Type" ValidationGroup="Submit">
                                    </asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-md-4" runat="server" id="divration">
                                    <span style="color: red">*</span>
                                    <label for="city">NCC Ration</label>
                                    <asp:DropDownList ID="ddlration" runat="server" AppendDataBoundItems="True" CssClass="form-control"
                                        TabIndex="4">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvration" runat="server" ControlToValidate="ddlration"
                                        Display="None" InitialValue="0" ErrorMessage="Please Select NCC Ration" ValidationGroup="Submit">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <br />
                            <div class="col-md-12 form-group">
                                <span style="font-weight: bold; padding-left: 11px">Note :</span> Please enter comma(,) separated Registration No. in Student Involved textbox to add multiple students.
                            </div>
                            <br />
                            <div class="col-md-12 form-group">
                                <div class="form-group col-md-4">
                                    <span style="color: red">*</span>
                                    <label>Student Involved</label>
                                    <asp:TextBox ID="txtStudInvol" runat="server" CssClass="form-control" AutoCompleteType="Disabled" TabIndex="5" TextMode="MultiLine" MaxLength="50" placeholder="Enter Registration No."></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtStudInvol" Display="None"
                                        ErrorMessage="Please Enter Student Registration Number." ValidationGroup="add" SetFocusOnError="True">
                                    </asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-md-1" style="padding-top: 25px">
                                    <asp:Button ID="btnAdd" runat="server" CssClass=" btn btn-outline-info" Text="Add" ValidationGroup="add" TabIndex="6" OnClick="btnAdd_Click" />

                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                        ShowMessageBox="True" ShowSummary="False" ValidationGroup="add" />
                                </div>

                                <div class=" form-group col-md-3">
                                    <span style="color: red">*</span>
                                    <label>Date </label>
                                    <div class="input-group">
                                        <div class="input-group-addon" id="FromDate" runat="server">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <asp:TextBox ID="txtDate" runat="server" CssClass="form-control pull-right"
                                            TabIndex="7" placeholder="From Date" ToolTip="Please Select Date" />
                                        <ajaxToolKit:CalendarExtender ID="cefrmdate" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtDate" PopupButtonID="FromDate" Enabled="True">
                                        </ajaxToolKit:CalendarExtender>
                                        <ajaxToolKit:MaskedEditExtender ID="meFromDate" runat="server" Mask="99/99/9999"
                                            MaskType="Date" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate"
                                            TargetControlID="txtDate" Enabled="True" />
                                        <ajaxToolKit:MaskedEditValidator ID="mvFromDate" runat="server" ControlExtender="meFromDate"
                                            ControlToValidate="txtDate" Display="None" EmptyValueMessage="Please Enter Date"
                                            ErrorMessage="Please Enter Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                            IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="SubPercentage" />

                                        <asp:RequiredFieldValidator ID="rfvdate" runat="server" ControlToValidate="txtDate"
                                            Display="None" ErrorMessage="Please Enter Date" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                    </div>
                                </div>

                            </div>

                            <div class="col-md-12 table table-responsive" id="divstuddata" runat="server" visible="false">
                                <asp:ListView ID="lvStudInvol" runat="server">
                                    <LayoutTemplate>
                                        <div>
                                            <h3>
                                                <label class="label label-default">STUDENTS INVOLVED</label>
                                            </h3>
                                        </div>
                                        <table class="table table-hover table-bordered datatable text-center">
                                            <tr class="bg-light-blue">
                                                <th>REG NO
                                                </th>
                                                <th>STUDENT NAME
                                                </th>
                                                <th>BRANCH
                                                </th>
                                                <th>SEMESTER
                                                </th>
                                            </tr>
                                            <tr id="itemPlaceholder" runat="server" />
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblIDNO" runat="server" Text=' <%# Eval("REGNO")%>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblStudName" runat="server" Text=' <%# Eval("Studname")%>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblLong" runat="server" Text='<%# Eval("longname")%>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblSem" runat="server" Text=' <%# Eval("semestername")%>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>

                                    <AlternatingItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblIDNO" runat="server" Text=' <%# Eval("REGNO")%>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblStudName" runat="server" Text=' <%# Eval("Studname")%>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblLong" runat="server" Text='<%# Eval("longname")%>' />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblSem" runat="server" Text=' <%# Eval("semestername")%>' />
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                </asp:ListView>

                            </div>

                            <div class="col-md-12 form-group text-center">
                                <asp:Button ID="btnsubmit" runat="server" Text="Submit" OnClick="btnsubmit_Click" CssClass="btn btn-outline-info" Enabled="false" ValidationGroup="Submit" />

                                <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="Submit" />
                            </div>

                            <div class="col-md-12">
                                <asp:Panel ID="pnlStudentreg" runat="server">
                                    <asp:ListView ID="lvregdata" runat="server">
                                        <LayoutTemplate>
                                            <div id="demo-grid">
                                                <h3>
                                                    <label class="label label-default">STUDENT LIST</label></h3>
                                                <table class="table table-hover table-bordered table-striped text center" id="tablehead" style="width: 100%">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th>Sr.No</th>
                                                            <th>Registration No.</th>
                                                            <th>Student Name</th>
                                                            <th>Joined Date</th>
                                                            <th>Date & Remark</th>
                                                            <th>View</th>
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
                                                <%--<td><%# Eval("REG_ID")%></td>--%>
                                                <td><%# Container.DataItemIndex+1 %></td>
                                                <td><%# Eval("REGNO")%></td>
                                                <td><%# Eval("STUDNAME")%></td>
                                                <td><%# Eval("ADD_DATE")%></td>
                                                <td style="display: flex">
                                                    <asp:Button ID="btnremove" runat="server" Text="Remove" OnClick="btnremove_Click" CssClass="btn btn-outline-danger" ValidationGroup="Remove"
                                                        CommandArgument='<%# Eval("REG_ID") %>' OnClientClick="return UserDeleteConfirmation();" />

                                                    <div class="input-group" style="width: 30%; padding-left: 10px">
                                                        <div class="input-group-addon" id="ToDate" runat="server">
                                                            <i class="fa fa-calendar"></i>
                                                        </div>
                                                        <asp:TextBox ID="txtDueDate" runat="server" class="form-control"
                                                            ondrop="return false;" placeholder="DD/MM/YYYY" onpaste="return false;" onkeypress="return RestrictCommaSemicolon(event);" onkeyup="ConvertEachFirstLetterToUpper(this.id)">
                                                        </asp:TextBox>
                                                        <ajaxToolKit:CalendarExtender ID="meetodate" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtDueDate" PopupButtonID="DueDate" Enabled="True">
                                                        </ajaxToolKit:CalendarExtender>
                                                        <ajaxToolKit:MaskedEditExtender ID="meToDate" runat="server" Mask="99/99/9999" MaskType="Date"
                                                            OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="errordate" TargetControlID="txtDueDate" />
                                                        <ajaxToolKit:MaskedEditValidator ID="mvToDate" runat="server" ControlExtender="meToDate"
                                                            ControlToValidate="txtDueDate" Display="None" EmptyValueMessage="Please Enter Due Date"
                                                            ErrorMessage="Please Enter Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                            IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="SubPercentage" />
                                                        <ajaxToolKit:MaskedEditValidator ID="rfvMonth" runat="server" ControlExtender="meToDate"
                                                            ControlToValidate="txtDueDate" Display="None" EmptyValueMessage="Please Enter Due Date"
                                                            ErrorMessage="Please Enter Date" InvalidValueBlurredMessage="*" InvalidValueMessage="Date is invalid"
                                                            IsValidEmpty="false" SetFocusOnError="true" ValidationGroup="Daywise" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please Enter DueDate."
                                                            ControlToValidate="txtDueDate" Display="None" SetFocusOnError="True" ValidationGroup="Remove"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txtremark" runat="server" placeholder="Remark" MaxLength="30" class="form-control" Style="width: 100%; margin-left: 10px;"></asp:TextBox>
                                                    </div>
                                                    <asp:ValidationSummary ID="vldsremove" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Remove" />

                                                </td>
                                                <td>
                                                    <asp:Button ID="btnview" Text="View" CssClass="btn btn-outline-primary" runat="server" ValidationGroup="View" data-toggle="modal"
                                                        data-target="#studinfo" OnClick="btnview_Click" OnClientClick="StudDetails();" CommandArgument='<%# Eval("IDNO") %>' />

                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <!-- Modal -->
    <div class="modal fade" id="studinfo" role="dialog">
        <div class="modal-dialog modal-lg" style="background-color: #fff">
            <asp:UpdatePanel ID="updmodal" runat="server">
                <ContentTemplate>
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h3 class="modal-title"><b>Student Information</b></h3>
                        </div>
                        <div class="modal-body">
                            <div class="col-md-12">
                                <div class="col-md-5 col-xs-12 form-group">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Student Name :</b><a>
                                            <asp:Label ID="lblName" runat="server" Font-Bold="True"></asp:Label></a> </li>

                                        <li class="list-group-item"><b>Current Semester :</b><a>
                                            <asp:Label ID="lblsemester" runat="server" Font-Bold="True"></asp:Label></a>
                                        </li>
                                        <li class="list-group-item"><b>Mobile No :</b><a>
                                            <asp:Label ID="lblMobNo" runat="server" Font-Bold="True"></asp:Label></a>
                                        </li>
                                        <li class="list-group-item"><b>Blood Group :</b><a>
                                            <asp:Label ID="lblBlood" runat="server" Font-Bold="True"></asp:Label></a>
                                        </li>
                                        <li class="list-group-item">
                                            <strong>Communication Address :</strong>
                                            <asp:Label ID="lblcaddr" Style="color: #3c8dbc;" runat="server" Font-Bold="True"></asp:Label>
                                        </li>
                                    </ul>
                                </div>
                                <div class="col-md-5 col-xs-12 form-group">
                                    <ul class="list-group list-group-unbordered">
                                        <li class="list-group-item"><b>Registration No. :</b><a>
                                            <asp:Label ID="lblRegNo" runat="server" Font-Bold="True"></asp:Label></a>
                                        </li>
                                        <li class="list-group-item"><b>Adm. No. :</b><a>
                                            <asp:Label ID="lblEnrollNo" runat="server" Font-Bold="True"></asp:Label></a>
                                        </li>
                                        <li class="list-group-item"><b>Branch :</b><a>
                                            <asp:Label ID="lblbranch" runat="server" Font-Bold="True" Style="font-size: 13px;"></asp:Label></a>
                                            <br />
                                        </li>
                                        <li class="list-group-item"><b>Email ID :</b><a>
                                            <asp:Label ID="lblMailID" runat="server" Font-Bold="True"></asp:Label></a>
                                        </li>

                                    </ul>
                                </div>

                                <div class="form-group col-md-2 col-xs-12 text-center">
                                    <asp:Image ID="imgPhoto" runat="server" Height="120px" Width="128px" />
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
    </div>

    <script type="text/javascript">
        function UserDeleteConfirmation() {
            if (confirm("Are you sure you want to Remove this Student?"))
                return true;
            else
                return false;
        }
    </script>
</asp:Content>

