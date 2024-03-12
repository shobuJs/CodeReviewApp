<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Multi_currency.aspx.cs" Inherits="ACADEMIC_Multi_currency" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.css") %>' rel="stylesheet" />
    <script src='<%=Page.ResolveUrl("~/plugins/multi-select/bootstrap-multiselect.js") %>'></script>
    <style>
        #ctl00_ContentPlaceHolder1_pnlcurr .dataTables_scrollHeadInner {
            width: max-content !important;
        }
    </style>
    <div>

        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updmulticurrency"
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
    <asp:UpdatePanel ID="updmulticurrency" runat="server">
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
                                            <asp:Label ID="lblDYAdmbatch" runat="server" Font-Bold="true"></asp:Label>
                                            <%--<label>Intake</label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlintake" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlintake"
                                            Display="None" ErrorMessage="Please Select Intake" ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYCollege" runat="server" Font-Bold="true"></asp:Label>
                                            <%--<label>Faculty/School Name</label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlFaculty" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlFaculty"
                                            Display="None" ErrorMessage="Please Select Faculty/School Name" ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-6 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDyProgram" runat="server" Font-Bold="true"></asp:Label>
                                            <%--<label>Program</label>--%>
                                        </div>
                                        <asp:ListBox ID="lstbxProgram" runat="server" AppendDataBoundItems="true" CssClass="form-control multi-select-demo" SelectionMode="Multiple"></asp:ListBox>

                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblCurrency" runat="server" Font-Bold="true"></asp:Label>
                                            <%--<label>Currency Type</label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlcurrency" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlcurrency"
                                            Display="None" ErrorMessage="Please Select Currency" ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblDYYear" runat="server" Font-Bold="true"></asp:Label>
                                            <%--<label>Year</label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlYear"
                                            Display="None" ErrorMessage="Please Select Year" ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblReceiptType" runat="server" Font-Bold="true"></asp:Label>
                                            <%--<label>Receipt Type</label>--%>
                                        </div>
                                        <asp:DropDownList ID="ddlreceipt" runat="server" CssClass="form-control" AppendDataBoundItems="true" data-select2-enable="true">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlreceipt"
                                            Display="None" ErrorMessage="Please Select Receipt Type" ValidationGroup="submit" InitialValue="0"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <asp:Label ID="lblAmount" runat="server" Font-Bold="true"></asp:Label>
                                            <%--<label>Amount</label>--%>
                                        </div>
                                        <asp:TextBox ID="TextAmot" runat="server" CssClass="form-control" data-select2-enable="true" onkeyup="IsNumeric(this);">                                          
                                        </asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TextAmot"
                                            Display="None" ErrorMessage="Please Enter Amount"   ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                            ControlToValidate="TextAmot" runat="server"
                                            ErrorMessage="Only Numbers allowed" 
                                            ValidationExpression="\1234567890">
                                        </asp:RegularExpressionValidator>--%>
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
                                <asp:Panel ID="pnlcurr" runat="server" Visible="false">
                                    <asp:ListView ID="LvOtherCurrency" runat="server">
                                        <LayoutTemplate>
                                            <div class="sub-heading" id="dem">
                                                <h5>Other Currency List</h5>
                                            </div>

                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="myTable1">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th>Edit </th>
                                                        <th>Intake</th>
                                                        <th>Faculty/Schnool Name</th>
                                                        <th>Program</th>
                                                        <th>Awarding Institute</th>
                                                        <th>Year</th>
                                                        <th>Currency</th>
                                                        <th>Receipt Type</th>
                                                        <th>Amount</th>
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
                                                <td><%# Eval("BATCHNAME")%> </td>
                                                <td><%# Eval("COLLEGE_NAME")%> </td>
                                                <td><%# Eval("PGM")%> </td>
                                                <td><%# Eval("AFFILIATED_SHORTNAME")%> </td>
                                                <td><%# Eval("YEARNAME")%> </td>
                                                <td><%# Eval("CUR_NAME")%> </td>
                                                <td><%# Eval("RECIEPT_TITLE")%> </td>
                                                <td><%# Eval("AMOUNT")%> </td>
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
        <Triggers>
            <asp:PostBackTrigger ControlID="ddlFaculty" />
            <asp:PostBackTrigger ControlID="btnCancel" />
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="LvOtherCurrency" />
        </Triggers>
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
    <script type="text/javascript" lang="javascript">
        function IsNumeric(textbox) {
            if (textbox != null && textbox.value != "") {
                if (isNaN(textbox.value)) {
                    document.getElementById(textbox.id).value = 0;
                    return true;
                }
            }
        }
    </script>
    <script>

        function onlyDotsAndNumbers(txt, event) {
            var charCode = (event.which) ? event.which : event.keyCode
            if (charCode == 46) {
                if (txt.value.indexOf(".") < 0)
                    return true;
                else
                    return false;
            }

            if (txt.value.indexOf(".") > 0) {
                var txtlen = txt.value.length;
                var dotpos = txt.value.indexOf(".");
                //Change the number here to allow more decimal points than 2
                if ((txtlen - dotpos) > 2)
                    return false;
            }

            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
    </script>
</asp:Content>
