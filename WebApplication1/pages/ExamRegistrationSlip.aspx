<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="ExamRegistrationSlip.aspx.cs" Inherits="ACADEMIC_ExamRegistrationSlip" Title=""
    MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="pnlStart" runat="server">

        <div class="row">
            <div class="col-md-12">
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title">SUPPLIMENTARY EXAM REGISTRATION SLIP</h3>
                    </div>

                    <div class="box-body">
                        <div class="col-md-12" id="divReceipts" runat="server" visible="True">
                            <fieldset>
                                <legend>
                                    <img alt="Show/Hide" src="../images/action_down.gif" onclick="ShowHideDiv('ctl00_ContentPlaceHolder1_divHidPreviousReceipts', this);" />
                                    &nbsp;&nbsp;Exam Receipts</legend>
                                <div id="divHidPreviousReceipts" runat="server" class="table table-responsive">
                                    <br />
                                    <%# Eval("DD_BANK")%>
                                    <asp:Repeater ID="lvPaidReceipts" runat="server">
                                        <HeaderTemplate>
                                            <div id="listViewGrid" class="vista-grid">
                                                <h4>Receipts Information</h4>
                                            </div>
                                            <table class="table table-hover table-bordered">
                                                <thead>
                                                    <tr class="bg-light-blue">
                                                        <th>Print
                                                        </th>
                                                        <th>Receipt Type
                                                        </th>
                                                        <th>Receipt No
                                                        </th>
                                                        <th>Date
                                                        </th>
                                                        <th>Semester
                                                        </th>
                                                        <th>Amount
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:ImageButton ID="btnPrintReceipt" runat="server" OnClick="btnPrintReceipt_Click"
                                                        CommandArgument='<%# Eval("DCR_NO") %>' ImageUrl="~/images/print.gif" ToolTip='<%# Eval("RECIEPT_CODE")%>' />
                                                </td>
                                                <td>


                                                    <%# (Eval("RECIEPT_TITLE").ToString()) == string.Empty ? "SUPPLEMENTARY EXAM FEES" : Eval("RECIEPT_TITLE")%>
                                                </td>
                                                <td>
                                                    <%# Eval("REC_NO") %>
                                                </td>
                                                <td>
                                                    <%# (Eval("REC_DT").ToString() != string.Empty) ? ((DateTime)Eval("REC_DT")).ToShortDateString() : Eval("REC_DT") %>
                                                </td>
                                                <td>
                                                    <%# Eval("SEMESTERNAME") %>
                                                </td>

                                                <td>
                                                    <%# Eval("TOTAL_AMT") %>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody></table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </div>
                            </fieldset>
                        </div>
                    </div>

                </div>
            </div>
        </div>

        <br />

        <div id="divMsg" runat="server">
        </div>
    </asp:Panel>

    <script type="text/javascript" language="javascript">
        function SelectAll(headchk) {
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
