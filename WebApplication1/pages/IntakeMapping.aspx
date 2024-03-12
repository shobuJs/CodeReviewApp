<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="IntakeMapping.aspx.cs" Inherits="ACADEMIC_IntakeMapping" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
         <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
    <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>

     <div>
       
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updAppyCon"
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
    <asp:UpdatePanel ID="updAppyCon" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                           <h3 class="box-title">
                            <asp:Label ID="lblDynamicPageTitle" runat="server" Font-Bold="true"></asp:Label></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">

                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Intake</label>
                                        </div>
                                        <asp:DropDownList ID="ddlAdmbatch" runat="server" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="true" TabIndex="1" Enabled="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlAdmbatch"
                                            Display="None" ErrorMessage="Please Select Intake" InitialValue="0" ValidationGroup="Submitss" />
                                    </div>
                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Study Level</label>
                                            <asp:ListBox ID="lstbxStudyLevel" runat="server" CssClass="form-control multi-select-demo" SelectionMode="Multiple" TabIndex="2"></asp:ListBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="lstbxStudyLevel"
                                                Display="None" ErrorMessage="Please Select Study Level" InitialValue="" ValidationGroup="Submitss" />
                                        </div>

                                    </div>
                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Programs Interested </label>
                                            <asp:ListBox ID="lstbxProgramInterest" runat="server" CssClass="form-control multi-select-demo" SelectionMode="Multiple" TabIndex="3"></asp:ListBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="lstbxProgramInterest"
                                                Display="None" ErrorMessage="Please Select Programs Interested " InitialValue="" ValidationGroup="Submitss" />
                                        </div>

                                    </div>
                                    <div class="col-12">
                                        <asp:Panel ID="Panel1" runat="server">
                                            <asp:ListView ID="lvIntakeMapping" runat="server" Visible="true">
                                                <LayoutTemplate>
                                                    <div class="sub-heading">
                                                    </div>
                                                    <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                                        <thead class="bg-light-blue">
                                                            <tr>
                                                                <th>Edit</th>
                                                                <th>Intake</th>
                                                                <th>Study Level</th>
                                                                <th>Programs Interested</th>
                                                               

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
                                                            <asp:ImageButton ID="btnEdits" runat="server" CausesValidation="false" ImageUrl="~/images/edit.gif" OnClick="btnEdits_Click" 
                                                                AlternateText="Edit Record" ToolTip='<%#Eval("MAPPING_ID") %>' CommandArgument='<%#Eval("BATCHNO") %>' CommandName='<%#Eval("AREA_INT_NO") %>'   />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblIntake" runat="server" Text='<%#Eval("BATCHNAME") %>'></asp:Label>
                                                             

                                                        </td>

                                                        <td>
                                                            <asp:Label ID="lblUgPg" runat="server" Text='<%#Eval("UGPG") %>'></asp:Label>

                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblAreaname" runat="server" Text='<%#Eval("AREA_NAME") %>'></asp:Label>
                                                        </td>

                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>

                                        </asp:Panel>

                                    </div>
                                    <div class="col-12 btn-footer mt-3">
                                        <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn btn-outline-info" TabIndex="7" ValidationGroup="Submitss" OnClick="btnSubmit_Click">Submit</asp:LinkButton>
                                        <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-outline-danger" TabIndex="7" OnClick="btnCancel_Click">Cancel</asp:LinkButton>
                                         <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Submitss"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
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
</asp:Content>

