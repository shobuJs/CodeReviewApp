<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="DefineCounter.aspx.cs" Inherits="DefineCounter" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<script src="../../INCLUDES/prototype.js" type="text/javascript"></script>

    <script src="../../INCLUDES/scriptaculous.js" type="text/javascript"></script>

    <script src="../../INCLUDES/modalbox.js" type="text/javascript"></script>--%>

    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updplRoom"
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

    <asp:UpdatePanel ID="updplRoom" runat="server">
        <ContentTemplate>

            <div id="Div2" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF; border: solid 1px #D0D0D0;">
            </div>
            <!-- Info panel to be displayed as a flyout when the button is clicked -->
            <div id="Div3" style="display: none; width: 250px; z-index: 2; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0); font-size: 12px; border: solid 1px #CCCCCC; background-color: #FFFFFF; padding: 5px;">
                <div id="Div4" style="float: right; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);">
                    <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="return false;" Text="X"
                        ToolTip="Close" Style="background-color: #666666; color: #FFFFFF; text-align: center; font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
                </div>
                <div>
                    <p class="page_help_head">
                        <span style="font-weight: bold; text-decoration: underline;">Page Help</span><br />
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/edit.gif" AlternateText="Edit Record" />
                        Edit Record
                    </p>
                    <p class="page_help_text">
                        <asp:Label ID="Label1" runat="server" Font-Names="Trebuchet MS" />
                    </p>
                </div>
            </div>

            <script type="text/javascript" language="javascript">
                // Move an element directly on top of another element (and optionally
                // make it the same size)
                function Cover(bottom, top, ignoreSize) {
                    var location = Sys.UI.DomElement.getLocation(bottom);
                    top.style.position = 'absolute';
                    top.style.top = location.y + 'px';
                    top.style.left = location.x + 'px';
                    if (!ignoreSize) {
                        top.style.height = bottom.offsetHeight + 'px';
                        top.style.width = bottom.offsetWidth + 'px';
                    }
                }
            </script>

            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border"> 
                             <h3 class="box-title">Define Fees Counter</h3>
                            <asp:Label ID="lblmessage" runat="server" Text=""></asp:Label>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Counter Name</label>
                                        </div>
                                        <asp:TextBox ID="txtCounterName" runat="server"  CssClass="form-control"/>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCounterName"
                                            ValidationGroup="Submit" Display="None" ErrorMessage="Please enter counter name." />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Short Print Name</label>
                                        </div>
                                        <asp:TextBox ID="txtPrintName" runat="server"  CssClass="form-control"/>
                                        <asp:RequiredFieldValidator ID="valPrintName" runat="server" ControlToValidate="txtPrintName"
                                            ValidationGroup="Submit" Display="None" ErrorMessage="Please enter print name." />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Counter User</label>
                                        </div>
                                        <asp:DropDownList ID="ddlCounterUser" runat="server" AppendDataBoundItems="true" CssClass="form-control" data-select2-enable="true" />
                                        <asp:RequiredFieldValidator ID="valCounterUser" runat="server" ControlToValidate="ddlCounterUser"
                                            ValidationGroup="Submit" Display="None" ErrorMessage="Please enter counter user."
                                            InitialValue="0" />
                                    </div>

                                    <div class="form-group col-lg-4 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Permission for Receipts</label>
                                        </div>
                                        <asp:RadioButtonList ID="chkListReceiptTypes" runat="server" CellPadding="3" CellSpacing="2" RepeatColumns="6">
                                        </asp:RadioButtonList>
                                        <asp:RequiredFieldValidator ID="rfvchklist" runat="server" ControlToValidate="chkListReceiptTypes"
                                            ValidationGroup="Submit" Display="None" ErrorMessage="Please Select Permission Receipt Type."/>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-outline-info" ValidationGroup="Submit" OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-outline-danger" Text="Cancel" OnClick="btnCancel_Click" />
                                <asp:ValidationSummary ID="valSummary" runat="server" DisplayMode="List" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="Submit" />
                            </div>

                            <div class="col-12">
                                <asp:ListView ID="lvCounters" runat="server">
                                    <LayoutTemplate>
                                        <div class="sub-heading"><h5>Present Counters</h5></div>
                                        <table class="table table-striped table-bordered nowrap display" style="width: 100%">
                                            <thead class="bg-light-blue">
                                                <tr>
                                                    <th>Edit </th>
                                                    <th>Counter Name </th>
                                                    <th>Print Name</th>
                                                    <th>Counter User</th>
                                                    <th>Permission for Receipts </th>
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
                                                <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="false" ImageUrl="~/IMAGES/edit.png"
                                                    CommandArgument='<%# Eval("COUNTERNO") %>' AlternateText="Edit Record" ToolTip="Edit Record"
                                                    OnClick="btnEdit_Click" TabIndex="10" />
                                            </td>
                                            <td>
                                                <%# ((Eval("COUNTERNAME").ToString() != string.Empty)? Eval("COUNTERNAME") : "--") %>
                                            </td>
                                            <td>
                                                <%# ((Eval("PRINTNAME").ToString() != string.Empty) ? Eval("PRINTNAME") : "--") %>
                                            </td>
                                            <td>
                                                <%# ((Eval("UA_FULLNAME").ToString() != string.Empty) ? Eval("UA_FULLNAME") : "--") %>
                                            </td>
                                            <td>
                                                <%# ((Eval("RECEIPT_PERMISSION").ToString() != string.Empty) ? Eval("RECEIPT_PERMISSION") : "--")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="divMsg" runat="server">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
