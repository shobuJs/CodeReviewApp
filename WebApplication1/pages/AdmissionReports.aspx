<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteMasterPage.master" CodeFile="AdmissionReports.aspx.cs" Inherits="ACADEMIC_AdmissionReports" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<script src="../Content/jquery.js" type="text/javascript"></script>--%>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="pnlAdmReport"
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

    <asp:UpdatePanel ID="pnlAdmReport" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">ADMISSION REPORTS</h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label> College/School Name</label>
                                        </div>
                                        <asp:DropDownList CssClass="form-control" data-select2-enable="true" runat="server" ID="ddlCollege" AppendDataBoundItems="true"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlCollege_SelectedIndexChanged" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfcollege" runat="server" ControlToValidate="ddlCollege" Display="None" ErrorMessage="Please Select College/School Name" SetFocusOnError="True" ValidationGroup="Report" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                        <%--  <asp:RequiredFieldValidator ID="rfdcollege" runat="server" ControlToValidate="ddlCollege" Display="None" ErrorMessage="Please Select College" SetFocusOnError="True" ValidationGroup="vgDetails" InitialValue="0">
                                        </asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divAdmYear" runat="server" visible="True">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label> Admission Batch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlAdmYear" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True" TabIndex="2" ToolTip="Please Select Admission Batch.">
                                            <asp:ListItem Text="Please Select" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvAdmYear" runat="server" ControlToValidate="ddlAdmYear" Display="None" ErrorMessage="Please Select Admission Batch" SetFocusOnError="True" ValidationGroup="Report" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                        <%--    <asp:RequiredFieldValidator ID="rfvDAdmYear" runat="server" ControlToValidate="ddlAdmYear" Display="None" ErrorMessage="Please Select Admission Year" SetFocusOnError="True" ValidationGroup="vgDetails" InitialValue="0">
                                        </asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divDegree" runat="server" visible="True">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label> Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True" TabIndex="3" ToolTip="Please Select Degree."
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                            <asp:ListItem Text="Please Select" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree" Display="None" ErrorMessage="Please Select Degree" SetFocusOnError="True" ValidationGroup="Report" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                        <%--<asp:RequiredFieldValidator ID="rfvDDegree" runat="server" ControlToValidate="ddlDegree" Display="None" ErrorMessage="Please Select Degree" SetFocusOnError="True" ValidationGroup="vgDetails" InitialValue="0">
                                        </asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divBranch" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label> Branch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True" TabIndex="4" ToolTip="Please Select Branch.">
                                            <asp:ListItem Text="Please Select" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch" Display="None" ErrorMessage="Please Select Branch" SetFocusOnError="True" ValidationGroup="vgShow" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="rfvDBranch" runat="server" ControlToValidate="ddlBranch" Display="None" ErrorMessage="Please Select Branch" SetFocusOnError="True" ValidationGroup="vgDetails" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divSem" runat="server" visible="false">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label> Semester</label>
                                        </div>
                                        <asp:DropDownList ID="ddlSem" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True" TabIndex="5" ToolTip="Please Select Semester.">
                                            <asp:ListItem Text="Please Select" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSem" runat="server" ControlToValidate="ddlSem" Display="None" ErrorMessage="Please Select Semester" SetFocusOnError="True" ValidationGroup="vgShow" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divIdType" runat="server">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label> Student Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlIdType" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True" TabIndex="6" ToolTip="Please Select Student Type.">
                                            <asp:ListItem Text="Please Select" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvIdType" runat="server" ControlToValidate="ddlIdType" Display="None" ErrorMessage="Please Select Student Type" SetFocusOnError="True" ValidationGroup="Report" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divQuota" runat="server">
                                        <div class="label-dynamic">
                                            <label> Quota</label>
                                        </div>
                                        <asp:DropDownList ID="ddlQuota" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True" TabIndex="7" ToolTip="Please Select Admission Quota.">
                                            <asp:ListItem Text="Please Select" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12" runat="server" visible="false">
                                <div class="btn-footer">
                                    <asp:Button ID="btnOrigCertSub" runat="server" Text="Admission Report" OnClick="btnOrigCertSub_Click" TabIndex="8" CssClass="btn btn-outline-info" ToolTip="Admission Report" ValidationGroup="vgShow" CausesValidation="true" />
                                    <asp:Button ID="btnFstGenGrad" runat="server" Text="Community Categorised DOTE Report" OnClick="btnFstGenGrad_Click" TabIndex="9" CssClass="btn btn-outline-info" ToolTip="Community Categorised DOTE Report" ValidationGroup="vgShow" CausesValidation="true" />
                                    <asp:Button ID="btnHSCMarkSheet" runat="server" Text="HSC Mark Sheet Report" OnClick="btnHSCMarkSheet_Click" TabIndex="10" CssClass="btn btn-outline-info" ToolTip="HSC Mark Sheet" CausesValidation="true" ValidationGroup="vgShow" />
                                
                                    <asp:Button ID="btnStudInfo" runat="server" Text="Student Information Report" OnClick="btnStudInfo_Click" TabIndex="11" CssClass="btn btn-outline-info" ToolTip="Student Information Report" CausesValidation="true" ValidationGroup="vgShow" />
                                    <asp:ValidationSummary ID="vsShow" ValidationGroup="vgShow" ShowSummary="False" runat="server" ShowMessageBox="True" />
                                    <asp:ValidationSummary ID="vsdetails" ValidationGroup="vgDetails" ShowSummary="False" runat="server" ShowMessageBox="True" />
                                </div>
                                <div class="btn-foter">
                                    <asp:Button Visible="false" ID="btnStudTcCC" runat="server" Text="Student TC & CC Details" OnClick="btnStudTcCC_Click" TabIndex="12" CssClass="btn btn-outline-info" ToolTip="Student TC & CC Details Report" CausesValidation="true" ValidationGroup="vgDetails" />
                                    <asp:Button Visible="false" ID="btnTransferStudent" runat="server" Text="Transfer Student" OnClick="btnTransferStudent_Click" TabIndex="12" CssClass="btn btn-outline-info" ToolTip="Transfer Student Report" CausesValidation="true" ValidationGroup="vgDetails" />
                                </div>
                            </div>

                            <div id="DivButtons" class="col-12">
                                <div class="btn-footer">
                                    <asp:Button ID="btnoperatorentry" runat="server" Text="Operator Entry Student" OnClick="btnoperatorentry_Click" TabIndex="8" CssClass="btn btn-outline-info" ToolTip="Operator Entry Student" ValidationGroup="Report" CausesValidation="true" />
                                    <asp:Button ID="btnPrincipalApproval" runat="server" Text="Principal Approval" OnClick="btnPrincipalApproval_Click" TabIndex="9" CssClass="btn btn-outline-info" ToolTip="Principal Approval" ValidationGroup="Report" CausesValidation="true" />
                                    <asp:Button ID="btnAdmConfirm" runat="server" Text="Final Admission Confirm Student" OnClick="btnAdmConfirm_Click" TabIndex="10" CssClass="btn btn-outline-info" ToolTip="Final Admission Confirm Student" CausesValidation="true" ValidationGroup="Report" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" TabIndex="11" CssClass="btn btn-outline-danger" ToolTip="Cancel" CausesValidation="false" />
                                    <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="Report" ShowSummary="False" runat="server" ShowMessageBox="True" />
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
                <div id="divMsg" runat="server">
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <%--<asp:AsyncPostBackTrigger ControlID="btnOrigCertSub" EventName="Click" />--%>
            <asp:PostBackTrigger ControlID="btnoperatorentry" />
            <asp:PostBackTrigger ControlID="btnPrincipalApproval" />
            <asp:PostBackTrigger ControlID="btnAdmConfirm" />
        </Triggers>
    </asp:UpdatePanel>


</asp:Content>
