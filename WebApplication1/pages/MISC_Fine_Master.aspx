<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="MISC_Fine_Master.aspx.cs" Inherits="ACADEMIC_ONLINEFEESCOLLECTION_MISC_Fine_Master" Title="" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <script type="text/javascript">
        RunThisAfterEachAsyncPostback();
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(RunThisAfterEachAsyncPostback);
    </script>
    <div style="z-index: 1; position: absolute; top: 10px; left: 550px;">
        <asp:UpdateProgress ID="updProg" runat="server"
            DynamicLayout="true" DisplayAfter="0" AssociatedUpdatePanelID="updBank">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:UpdatePanel ID="updBank" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-info">
                            <div class="box-header with-border">
                                <h3 class="box-title">MISC MASTER</h3>
                                <div class="box-tools pull-right">
                                </div>
                            </div>
                            <br />
                            <div style="color: Red; font-weight: bold; margin-top: -20px; margin-right: -5px;">
                                &nbsp;&nbsp;&nbsp;Note : * Marked fields are mandatory
                            </div>
                            <div class="box-body">
                                <div class="col-md-12">
                                    <div class="row">
                                        <div class="form-group col-md-4">
                                        </div>
                                        <div class="form-group col-md-4">
                                            <label>Cash Book <span style="color: red;">*</span></label>
                                            <asp:DropDownList ID="ddlCashBook" runat="server" AppendDataBoundItems="true" CssClass="form-control" ValidationGroup="show">
                                                <asp:ListItem Value="0">Please Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlCashBook"
                                                Display="None" ErrorMessage="Please Select Cash Book" InitialValue="0" ValidationGroup="show">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group col-md-4">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="box box-footer">
                                <p class="text-center">
                                    <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-outline-primary" OnClick="btnShow_Click"
                                        ValidationGroup="show" />

                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-success" OnClick="btnSubmit_Click"
                                        ValidationGroup="submit" />

                                    <asp:Button ID="btnClear" runat="server" Text="Cancel" CssClass="btn btn-outline-danger" OnClick="btnClear_Click" />
                                    <asp:ValidationSummary ID="valshow" runat="server" ValidationGroup="show" ShowMessageBox="true"
                                        DisplayMode="List" ShowSummary="false" />
                                    <asp:ValidationSummary ID="valSubmit" runat="server" ValidationGroup="submit" ShowMessageBox="true"
                                        DisplayMode="List" ShowSummary="false" />
                                </p>
                                <div class="col-md-12">
                                    <asp:Panel ID="pnlMisclst" runat="server" ScrollBars="auto">

                                        <asp:ListView ID="lvmiscCollection" runat="server">
                                            <EmptyDataTemplate>
                                                <div align="center" class="data_label">
                                                    -- No Student Record Found --
                                                </div>
                                            </EmptyDataTemplate>
                                            <LayoutTemplate>
                                                <div id="listViewGrid" class="vista-grid">
                                                    <h4 style="text-shadow: 2px 2px 3px #0b93f8;">Search Details</h4>
                                                    <table class="table table-hover table-bordered table-responsive">
                                                        <thead>
                                                            <tr class="bg-light-blue">
                                                                <th>Sl.No.
                                                                </th>
                                                                <th>Select
                                                                            <asp:CheckBox ID="chkIdentityCard" runat="server" onClick="totAll(this);"
                                                                                ToolTip="Select or Deselect All Records" />
                                                                </th>
                                                                <th>MISC HEAD CODE
                                                                </th>
                                                                <th>MISC HEAD
                                                                </th>
                                                                <th>Amount
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tr id="itemPlaceholder" runat="server">
                                                        </tr>
                                                    </table>
                                                </div>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                    <td>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkReport" runat="server" /><asp:HiddenField ID="hidIdNo" runat="server"
                                                            Value='<%# Eval("MISCHEADSRNO") %>' />
                                                    </td>
                                                    <td>
                                                        <%# Eval("MISCHEADCODE")%>
                                                    </td>
                                                    <td>
                                                        <%# Eval("MISCHEAD")%>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtAmount" CssClass="from-control" runat="server" AppendDataBoundItems="true" MaxLength="10" Text='<%# Eval("AMOUNT")%>' />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:ListView>

                                    </asp:Panel>
                                </div>
                                <div id='divMsg' runat="server">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>


    <script language="javascript" type="text/javascript">
        function totAll(headchk) {
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
