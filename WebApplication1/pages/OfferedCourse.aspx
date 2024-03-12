<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="OfferedCourse.aspx.cs" Inherits="ACADEMIC_OfferedCourse"
    Title=" " %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updpnl"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>

    <asp:UpdatePanel ID="updpnl" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">OFFERED COURSE</h3>
                        </div>
                        <form role="form">
                            <div class="box-body">
                                <div class="col-md-2"></div>
                                <div class="col-md-4">
                                    <label>Session</label>
                                    <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" AutoPostBack="true" CssClass="form-control">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvSession" runat="server"
                                        ControlToValidate="ddlSession" Display="None" InitialValue="0"
                                        ErrorMessage="Please Select Session" ValidationGroup="offered"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-4">
                                    <label>Regulation</label>
                                    <asp:DropDownList ID="ddlScheme" runat="server" CssClass="form-control"
                                        AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvScheme" runat="server"
                                        ControlToValidate="ddlScheme" Display="None" InitialValue="0"
                                        ErrorMessage="Please Select Regulation" ValidationGroup="offered"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </form>
                        <div class="box-footer">
                            <p class="text-center">
                                <asp:Button ID="btnAd" runat="server" OnClick="btnAd_Click" Text="Submit" ValidationGroup="offered"
                                    CssClass="btn btn-outline-info" />

                                <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel"
                                    CssClass="btn btn-outline-danger" />
                                <asp:Button ID="btnPrint" runat="server" OnClick="btnPrint_Click" Text="Report"
                                    CssClass="btn btn-outline-primary" />
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
                                    ShowMessageBox="true" ShowSummary="false" ValidationGroup="offered" />
                            </p>

                            <div class="col-md-12">
                                <asp:Panel ID="pnlCourse" runat="server" ScrollBars="Auto">
                                    <asp:ListView ID="lvCourse" runat="server"
                                        OnItemDataBound="lvCourse_ItemDataBound">
                                        <LayoutTemplate>
                                            <div id="demo-grid">
                                                <h4>Offered Course</h4>
                                                <table class="table table-hover table-bordered">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th>Ccode</th>
                                                            <th>CourseName</th>
                                                            <th>Credits</th>
                                                            <th>Offered</th>
                                                            <th>Sem</th>
                                                            <th style="width: 10%">Seq No</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr id="itemPlaceholder" runat="server" />
                                                    </tbody>
                                            </div>
                                        </LayoutTemplate>

                                        <ItemTemplate>
                                            <tr>

                                                <td><%# Eval("CCODE")%></td>
                                                <td><%# Eval("COURSE_NAME")%></td>
                                                <td><%# Eval("CREDITS") %></td>
                                                <td>
                                                    <asp:CheckBox ID="chkoffered" runat="server" ToolTip='<%# Eval("COURSENO") %>' /></td>
                                                <td>
                                                    <asp:DropDownList ID="ddlsem" runat="server"></asp:DropDownList>
                                                    <asp:Label ID="LblSemNo" runat="server" Visible="false" Text='<% #Eval("SEMESTERNO")%>' ToolTip='<% #Eval("OFFERED")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtseqno" runat="server" Text='<% #Eval("SRNO")%>'></asp:TextBox>
                                                    <asp:Label ID="lblseqno" runat="server" Visible="false" Text='<% #Eval("SRNO")%>'></asp:Label>
                                                    <asp:RequiredFieldValidator ID="rfysqno" runat="server" ControlToValidate="txtseqno" Display="None"
                                                        InitialValue="0" ErrorMessage="Please selecr seq.no" ValidationGroup="val"></asp:RequiredFieldValidator>
                                                </td>

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

    <%--  Shrink the info panel out of view --%> <%--  Reset the sample so it can be played again --%>
    <ajaxToolKit:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mdlPopupDel" runat="server"
        TargetControlID="div" PopupControlID="div"
        OkControlID="btnOkDel" OnOkScript="okDelClick();"
        CancelControlID="btnNoDel" OnCancelScript="cancelDelClick();" BackgroundCssClass="modalBackground" />
    <asp:Panel ID="div" runat="server" Style="display: none" CssClass="modalPopup">
        <div style="text-align: center">
            <table>
                <tr>
                    <td align="center">
                        <asp:Image ID="imgWarning" runat="server" ImageUrl="~/images/warning.gif" AlternateText="Warning" /></td>
                    <td>&nbsp;&nbsp;Are you sure you want to delete this record?
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnOkDel" runat="server" Text="Yes" Width="50px" />
                        <asp:Button ID="btnNoDel" runat="server" Text="No" Width="50px" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <script type="text/javascript">
        //  keeps track of the delete button for the row
        //  that is going to be removed
        var _source;
        // keep track of the popup div
        var _popup;

        function showConfirmDel(source) {
            this._source = source;
            this._popup = $find('mdlPopupDel');

            //  find the confirm ModalPopup and show it    
            this._popup.show();
        }

        function okDelClick() {
            //  find the confirm ModalPopup and hide it    
            this._popup.hide();
            //  use the cached button as the postback source
            __doPostBack(this._source.name, '');
        }

        function cancelDelClick() {
            //  find the confirm ModalPopup and hide it 
            this._popup.hide();
            //  clear the event source
            this._source = null;
            this._popup = null;
        }
    </script>
</asp:Content>



