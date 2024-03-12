<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Activity_type_reports.aspx.cs" Inherits="ACADEMIC_Activity_type_reports" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="z-index: 100; position: absolute; top: 50%; left: 50%;">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Activity Type Reports</h3>
                        </div>
                        <div><span style="color: Red; font-weight: bold;">&nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory </span></div>
                        <div class="box-body">
                            <div class="col-md-12">
                                <div class="form-group col-md-4">
                                    <label><span style="color: red;">* </span>Activity Type :</label>

                                    <asp:DropDownList ID="ddlActivity" runat="server" AppendDataBoundItems="True"
                                        AutoPostBack="True" ValidationGroup="show" CssClass="form-control"
                                        OnSelectedIndexChanged="ddlActivity_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvActivity" runat="server" ControlToValidate="ddlActivity"
                                        Display="None" ErrorMessage="Please Select Activity Type" SetFocusOnError="true" InitialValue="0" ValidationGroup="show"></asp:RequiredFieldValidator>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlActivity"
                                        Display="None" ErrorMessage="Please Select Activity Type" SetFocusOnError="true" InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlActivity"
                                        Display="None" ErrorMessage="Please Select Activity Type" SetFocusOnError="true" InitialValue="0" ValidationGroup="Camp"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-md-4">
                                    <label><span style="color: red;">* </span>Degree</label>
                                    <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="True" CssClass="form-control"
                                        AutoPostBack="true" ValidationGroup="show" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree"
                                        Display="None" ErrorMessage="Please Select Degree" InitialValue="0" SetFocusOnError="True"
                                        ValidationGroup="show"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlDegree"
                                        Display="None" ErrorMessage="Please Select Degree" InitialValue="0" SetFocusOnError="True"
                                        ValidationGroup="Show"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group col-md-4">
                                    <label><span style="color: red;">* </span>Branch </label>

                                    <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="True" CssClass="form-control"
                                        AutoPostBack="True" ValidationGroup="show" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" >
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                        Display="None" ErrorMessage="Please Select Branch" InitialValue="0" SetFocusOnError="True"
                                        ValidationGroup="show"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlBranch"
                                        Display="None" ErrorMessage="Please Select Branch" InitialValue="0" SetFocusOnError="True"
                                        ValidationGroup="Show"></asp:RequiredFieldValidator>
                                </div>

                                
                               
                            </div>
                            <div class="col-md-12">

                                <div class="form-group col-md-4">
                                    <label><span style="color: red;">* </span>Semester</label>

                                    <asp:DropDownList ID="ddlSemester" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                        ValidationGroup="show" CssClass="form-control">
                                        <asp:ListItem Value="0">Please Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlSemester"
                                        Display="None" ErrorMessage="Please Select Semester" InitialValue="0" SetFocusOnError="True"
                                        ValidationGroup="show"></asp:RequiredFieldValidator>                                   
                                </div>

                                <div class="form-group col-md-4" runat="server">
                                            <label>
                                                <span style="color: red;">*</span> From Date
                                            </label>

                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" />

                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtFromDate" PopupButtonID="imgCalFromDate" Enabled="true" EnableViewState="true" />
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtFromDate"
                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                    AcceptNegative="none" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFromDate"
                                                    ErrorMessage="Please Select From Date" ValidationGroup="Show" Display="None"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtFromDate"
                                                    ErrorMessage="Please Select From Date" ValidationGroup="Camp" Display="None"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                <div class="form-group col-md-4" runat="server">
                                            <label>
                                               <span style="color: red;">*</span> To Date
                                            </label>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar text-blue"></i>
                                                </div>
                                                <asp:TextBox ID="txtTodate" runat="server" CssClass="form-control" onchange="checkDate(1);" />

                                                <ajaxToolKit:CalendarExtender ID="CalendarExtender7" runat="server" Format="dd/MM/yyyy"
                                                    TargetControlID="txtTodate" PopupButtonID="imgCalToDate" Enabled="true" EnableViewState="true" />
                                                <ajaxToolKit:MaskedEditExtender ID="MaskedEditExtender5" runat="server" TargetControlID="txtTodate"
                                                    Mask="99/99/9999" MessageValidatorTip="true" MaskType="Date" DisplayMoney="Left"
                                                    AcceptNegative="none" ErrorTooltipEnabled="true" OnInvalidCssClass="errordate" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtTodate"
                                                    ErrorMessage="Please Select To Date" ValidationGroup="Show" Display="None"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtTodate"
                                                    ErrorMessage="Please Select To Date" ValidationGroup="Camp" Display="None"></asp:RequiredFieldValidator>

                                            </div>
                                        </div>

                            </div>

                            <div class="box-footer col-md-12">

                                <p class="text-center">
                                    <asp:Button ID="btnNccReport" runat="server" OnClick="btnNccReport_Click" Text="Active Students Report" ValidationGroup="show" CssClass="btn btn-outline-primary" />
                                    &nbsp;<asp:Button ID="btnNssReport" runat="server" OnClick="btnNssReport_Click" CssClass="btn btn-outline-primary" ValidationGroup="Show" Text="Left Students Report" />
                                    &nbsp;<asp:Button ID="btnCampReport" runat="server" OnClick="btnCampReport_Click" CssClass="btn btn-outline-primary" Text="Camp Details Report" ValidationGroup="Camp" />
                                    &nbsp;<asp:Button ID="btnReport" runat="server" Text="Report"
                                        ValidationGroup="Show" Visible="false" CssClass="btn btn-outline-primary" />
                                    &nbsp;<asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel" CssClass="btn btn-outline-danger" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                        ShowSummary="False" ValidationGroup="show" />
                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                                        ShowSummary="False" ValidationGroup="Show" />
                                    <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="True"
                                        ShowSummary="False" ValidationGroup="Camp" />
                                </p>
                            </div>   
                        </div>
                    </div>
                    </div>
                </div>
            </ContentTemplate>
          <Triggers>
              <asp:PostBackTrigger ControlID="btnNccReport" />
              <asp:PostBackTrigger ControlID="btnNssReport" />
              <asp:PostBackTrigger ControlID="btnCampReport" />
          </Triggers>
          </asp:UpdatePanel>
    <div id="divMsg" runat="server">
    </div>
</asp:Content>

