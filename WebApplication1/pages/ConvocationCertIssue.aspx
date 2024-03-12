<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="ConvocationCertIssue.aspx.cs" Inherits="ACADEMIC_ConvocationCertIssue"
    Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-12">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">CONVOCATION CERTIFICATE ISSUE</h3>
                </div>

                <div class="box-body">
                    <div class="form-group col-md-3">
                        <label>Session</label>
                        <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" CssClass="form-control"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvSession" runat="server" ControlToValidate="ddlSession"
                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>

                           <asp:RequiredFieldValidator ID="rfvddlSession" runat="server" ControlToValidate="ddlSession"
                            Display="None" ErrorMessage="Please Select Session" InitialValue="0" ValidationGroup="award"></asp:RequiredFieldValidator>

                    </div>
                    <div class="form-group col-md-3">
                        <label>Convocation</label>
                        <asp:DropDownList ID="ddlConvocation" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlConvocation"
                            Display="None" ErrorMessage="Please Select Convocation" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group col-md-3" hidden="hidden">
                        <label>Certificate Name </label>
                        <asp:DropDownList ID="ddlCertificateNo" runat="server" AppendDataBoundItems="True" AutoPostBack="True" CssClass="form-control"
                            OnSelectedIndexChanged="ddlCertificateNo_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvCertificateNo" runat="server" ControlToValidate="ddlCertificateNo"
                            Display="None" ErrorMessage="Please Select Certificate" InitialValue="0" ValidationGroup="report1"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group col-md-3">
                        <label>Degree</label>
                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control"
                            OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>

                           <asp:RequiredFieldValidator ID="rfvddlDegree" runat="server" ControlToValidate="ddlDegree"
                            Display="None" ErrorMessage="Please Select Degree" InitialValue="0" ValidationGroup="award"></asp:RequiredFieldValidator>

                    </div>
                    <div class="form-group col-md-3" id="trDept" runat="server" visible="false">
                        <label>Department</label>
                        <asp:DropDownList ID="ddlDept" runat="server" AppendDataBoundItems="True">
                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvDept" runat="server" ControlToValidate="ddlDept"
                            Display="None" ErrorMessage="Please Select Department" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group col-md-3" id="trBranch" runat="server">
                        <label>Branch</label>
                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control"
                            OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                            Display="None" ErrorMessage="Please Select Branch" InitialValue="0" ValidationGroup="report"></asp:RequiredFieldValidator>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlBranch"
                            Display="None" ErrorMessage="Please Select Branch" InitialValue="0" ValidationGroup="award"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group col-md-3" id="trScheme" runat="server">
                        <label>Regulation</label>
                        <asp:DropDownList ID="ddlScheme" runat="server" AppendDataBoundItems="true" CssClass="form-control"
                            AutoPostBack="True" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvScheme" runat="server" ControlToValidate="ddlScheme"
                            Display="None" InitialValue="0" ErrorMessage="Please Select Regulation" ValidationGroup="report"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group col-md-3" id="trSem" runat="server">
                        <label>Semester</label>
                        <asp:DropDownList ID="ddlSem" runat="server" AppendDataBoundItems="True" CssClass="form-control">
                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvSem" runat="server" ControlToValidate="ddlSem"
                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="report">
                        </asp:RequiredFieldValidator>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSem"
                            Display="None" ErrorMessage="Please Select Semester" InitialValue="0" ValidationGroup="award">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group col-md-3" hidden="hidden">
                        <label>Regulation Date</label><asp:TextBox ID="lblRegulationDate" runat="server" BackColor="#FF99FF" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-3">
                        <label>Convocation Date</label>
                        <div class="input-group">
                            <div class="input-group-addon">
                                <i class="fa fa-calendar"></i>
                            </div>
                            <asp:TextBox ID="txtConvocationDate" runat="server" ValidationGroup="submit" CssClass="form-control" />
                            <ajaxToolKit:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True"
                                Format="dd/MM/yyyy" PopupButtonID="imgEDate" TargetControlID="txtConvocationDate" />
                            <asp:RequiredFieldValidator ID="rfvEndDate" runat="server" ControlToValidate="txtConvocationDate"
                                Display="None" ErrorMessage="Please Enter Convocation Date" SetFocusOnError="True"
                                ValidationGroup="report" />
                            <ajaxToolKit:MaskedEditExtender ID="meeEndDate" runat="server" AcceptNegative="Left"
                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                                CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder=""
                                CultureTimePlaceholder="" DisplayMoney="Left" Enabled="True" ErrorTooltipEnabled="True"
                                Mask="99/99/9999" MaskType="Date" OnInvalidCssClass="errordate" TargetControlID="txtConvocationDate" />
                            <ajaxToolKit:MaskedEditValidator ID="mevEndDate" runat="server" ControlExtender="meeEndDate"
                                ControlToValidate="txtConvocationDate" Display="None" EmptyValueBlurredText="Empty"
                                EmptyValueMessage="Please Enter Convocation Date" ErrorMessage="mevEndDate" InvalidValueBlurredMessage="Invalid Date"
                                InvalidValueMessage="From Date is Invalid (Enter dd/MM/yyyy Format)" SetFocusOnError="True"
                                ValidationGroup="Show" />
                        </div>
                    </div>
                    <div class="col-md-6">
                        <legend>Convocation Report Type</legend>
                        <asp:RadioButtonList ID="rdoReportType" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True" Value="pdf">Adobe Reader</asp:ListItem>
                            <asp:ListItem Value="xls">MS-Excel</asp:ListItem>
                            <asp:ListItem Value="doc">MS-Word</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>

                </div>
                <div class="box-footer">
                    <p class="text-center">
                        <asp:Button ID="btnShowData" runat="server" Text="Filter" ValidationGroup="report"
                            CssClass="btn btn-outline-info" OnClick="btnShowData_Click" />
                        <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit"
                            CssClass="btn btn-outline-info" ValidationGroup="report" />
                        <asp:Button ID="btnReport" runat="server" Text="Certificate" CssClass="btn btn-outline-primary" OnClick="btnReport_Click" />
                        <asp:Button ID="btnStudReport" runat="server" Text="Student List" CssClass="btn btn-outline-primary" OnClick="btnStudReport_Click" Visible="false"/>
                        <asp:Button ID="btnPassoutStudent" runat="server" Text="Passout Student List"
                            CssClass="btn btn-outline-primary" OnClick="btnPassoutStudent_Click" />
                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                            ShowMessageBox="true" ShowSummary="false" ValidationGroup="report" />
                    </p>

                    <p class="text-center">
                        <asp:Button ID="btnConvoReport" runat="server" Text="Convocation List" CssClass="btn btn-outline-primary" OnClick="btnConvoReport_Click" />
                        <asp:Button ID="btnConvReport" runat="server" Text="Without Photo List" CssClass="btn btn-outline-primary" OnClick="btnConvReport_Click" />
                          <asp:Button ID="btnAwardReport" runat="server" Text="Eligible Degree Award Report"  ValidationGroup="award" CssClass="btn btn-outline-primary" OnClick="btnAwardReport_Click" />
                        <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Clear" CssClass="btn btn-outline-danger" />
                    
                     <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                            ShowMessageBox="true" ShowSummary="false" ValidationGroup="award" />
                    </p>
                    <div class="col-md-6">
                        <fieldset style="padding: 5px; color: Green">
                            <legend>Note</legend>Please Select<br />
                            <span style="font-weight: bold; color: Red;">Passout Student List : </span>
                            Convocation->Certificate Name->Degree->Branch<br />
                        </fieldset>
                    </div>
                    <%--<div class="col-md-6">
                        <legend>Convocation Report Type</legend>
                        <asp:RadioButtonList ID="rdoReportType" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True" Value="pdf">Adobe Reader</asp:ListItem>
                            <asp:ListItem Value="xls">MS-Excel</asp:ListItem>
                            <asp:ListItem Value="doc">MS-Word</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>--%>
                    <div class="col-md-12">
                        <asp:Panel ID="pnlConvocation" runat="server" Visible="false" ScrollBars="Auto">
                            <asp:ListView ID="lvConvocationCertificate" runat="server">
                                <LayoutTemplate>
                                    <div id="demo-grid">
                                        <h4>Student Eligible for Issue Certificate</h4>
                                        <table class="table table-hover table-bordered table-responsive">
                                            <thead>
                                                <tr class="bg-light-blue">
                                                    <th>
                                                        <asp:CheckBox ID="cbHead" runat="server" onclick="totAllSubjects(this)" ToolTip="Select All" />
                                                    </th>
                                                    <th>Reg No
                                                    </th>
                                                    <th>Student Name
                                                    </th>
                                                    <th>Degree
                                                    </th>
                                                    <th>Branch
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server" />
                                            </tbody>
                                    </div>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="cbRow" runat="server" />
                                            <asp:Label ID="lblIDNo" runat="server" Text='<%# Eval("IDNO") %>' Visible="false" />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblRegno" runat="server" Text='<%# Eval("REGNO")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblStudName" runat="server" Text='<%# Eval("STUDNAME")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDegree" runat="server" Text='<%# Eval("DEGREE")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblBranch" runat="server" Text='<%# Eval("BRANCH_SHORT")%>' ToolTip='<%# Eval("BRANCH_LONG")%>'></asp:Label>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>
                    </div>
                    <div id="divMsg" runat="server">
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript" language="javascript">
        function totAllSubjects(headchk) {
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
</asp:Content>
