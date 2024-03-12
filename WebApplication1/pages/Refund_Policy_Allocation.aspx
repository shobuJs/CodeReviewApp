<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Refund_Policy_Allocation.aspx.cs" Inherits="Projects_Refund_Policy_Allocation" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
    <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>
    <style>
        #ctl00_ContentPlaceHolder1_pnlRefund .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updCourseCreation"
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
    <asp:UpdatePanel ID="updCourseCreation" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                <asp:Label ID="lblDynamicPageTitle" runat="server"></asp:Label></h3>
                        </div>
                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Faculty/School Name</label>
                                        </div>
                                        <asp:DropDownList ID="ddlFaculty" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlFaculty"
                                            Display="None" ErrorMessage="Please Select Faculty/School Name" ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Study Level </label>
                                        </div>
                                        <asp:DropDownList ID="ddlStudyLevel" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlStudyLevel"
                                            Display="None" ErrorMessage="Please Select Study Level" ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Awarding Institute</label>
                                        </div>
                                        <asp:DropDownList ID="ddlAwardingInstitute" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlAwardingInstitute"
                                            Display="None" ErrorMessage="Please Select Awarding Institute" ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                     <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                             <sup>* </sup>
                                            <label>Request Type</label>
                                        </div>
                                        <asp:DropDownList ID="ddlWithdrawalType" AppendDataBoundItems="true" runat="server" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlWithdrawalType"
                                            Display="None" ErrorMessage="Please Select Request Type" ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Program </label>
                                        </div>
                                        <asp:ListBox ID="lstbxProgram" runat="server" AppendDataBoundItems="true" CssClass="form-control multi-select-demo" SelectionMode="Multiple"></asp:ListBox>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Semester</label>
                                        </div>
                                        <asp:ListBox ID="lstbxSemester" runat="server" AppendDataBoundItems="true" CssClass="form-control multi-select-demo" SelectionMode="Multiple"></asp:ListBox>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Policy Name </label>
                                        </div>
                                        <asp:DropDownList ID="ddlPolicyName" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlPolicyName"
                                            Display="None" ErrorMessage="Please Select Policy Name" ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>

                                   
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn btn-outline-info" ValidationGroup="submit" OnClick="btnSubmit_Click">Submit</asp:LinkButton>
                                <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-outline-danger" OnClick="btnCancel_Click">Cancel</asp:LinkButton>
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="submit"
                                    ShowMessageBox="true" ShowSummary="false" DisplayMode="List" />
                            </div>

                            <div class="col-12 mt-3">
                                <asp:Panel ID="pnlRefund" runat="server" Visible="false">
                                    <asp:ListView ID="LvRefundAllocation" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading" id="dem">
                                                <h5>Refund Policy Allocation</h5>
                                            </div>

                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="myTable1">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Edit </th>
                                                        <th>Faculty/School Name</th>
                                                        <th>Study Level</th>
                                                        <th>Awarding Institute</th>
                                                        <th>Program</th>
                                                        <th>Semester</th>
                                                        <th>Policy Name</th>
                                                        <th>Request type</th>
                                                    </tr>
                                                    <thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/images/edit.gif"
                                                        CommandArgument='<%# Eval("SRNO") %>' ToolTip='<%# Eval("SRNO") %>' AlternateText="Edit Record"
                                                        OnClick="btnEdit_Click" />
                                                </td>
                                                <td><%# Eval("COLLEGE_NAME")%> </td>
                                                <td><%# Eval("UA_SECTIONNAME")%> </td>
                                                <td><%# Eval("AFFILIATED_LONGNAME")%> </td>
                                                <td><%# Eval("PRGM")%> </td>
                                                <td><%# Eval("SEMESTERNAME")%> </td>
                                                <td><%# Eval("REFUNDPOLICY_NAME")%> </td>
                                                <td><%# Eval("REQUESTTYPE_NAME")%> </td>
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
    <!-- MultiSelect Script -->
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

