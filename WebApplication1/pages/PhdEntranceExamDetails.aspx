<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="PhdEntranceExamDetails.aspx.cs" Inherits="ACADEMIC_PhdEntranceExamDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="z-index: 1; position: absolute; top: 10px; left: 600px;">
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="updtime"
            DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div style="width: 120px; padding-left: 5px">
                    <i class="fa fa-refresh fa-spin" style="font-size: 50px"></i>
                    <p class="text-success"><b>Loading..</b></p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel ID="updtime" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">PHD Entrance Exam Hall Ticket Generation</h3>
                        </div>

                        <div class="box-body">
                            <br />
                            <br />
                            <p class="text-center">
                                <asp:Button ID="btnPrintReport" runat="server" Text="Print Admit Card" TabIndex="1" CssClass="btn btn-outline-info"
                                    OnClick="btnPrintReport_Click" ToolTip="Print Card under Selected Criteria." ValidationGroup="show" />


                                <asp:Button ID="btnSendEmail" runat="server" Text="Send To Email" TabIndex="2" CssClass="btn btn-outline-primary"
                                    OnClick="btnSendEmail_Click" ToolTip="Send Card By Email" ValidationGroup="show" OnClientClick="if ( !confirm('Are you sure you want to Send Mail to selected Student(s)?')) return false;" />


                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" Visible="false" OnClick="btnCancel_Click" TabIndex="10"
                                    ToolTip="Cancel Selected under Selected Criteria." CssClass="btn btn-outline-danger" />
                            </p>
                            <div class="col-md-2">
                                <label>Total Selected</label>
                                <asp:TextBox ID="txtTotStud" runat="server" Text="0" Enabled="False" CssClass="form-control"
                                    Style="text-align: center;" Font-Bold="True" Font-Size="Small"></asp:TextBox>
                                <%--  Reset the sample so it can be played again --%>
                                <ajaxToolKit:TextBoxWatermarkExtender ID="text_water" runat="server" TargetControlID="txtTotStud"
                                    WatermarkText="0" WatermarkCssClass="watermarked" Enabled="True" />
                                <asp:HiddenField ID="hftot" runat="server" />
                            </div>
                            <div class="col-md-12 table table-responsive">
                                <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
                                    <asp:ListView ID="lvStudentRecords" runat="server">
                                        <LayoutTemplate>
                                            <div id="listViewGrid" class="vista-grid">
                                                <div class="titlebar">
                                                    <h4>Phd Student List </h4>
                                                </div>
                                                <table class="table table-hover table-bordered table-responsive table-striped">
                                                    <thead>
                                                        <tr class="bg-light-blue">
                                                            <th>Sl.No.
                                                            </th>
                                                            <th style="text-align: center;">Select
                                                                <asp:CheckBox ID="chkIdentityCard" TabIndex="3" runat="server" onClick="SelectAll(this);" ToolTip="Select or Deselect All Records" />
                                                            </th>
                                                            <th>SeatNo
                                                            </th>
                                                            <th>Name of Candidate
                                                            </th>
                                                            <th>Category
                                                            </th>
                                                            <th>Gender
                                                            </th>
                                                            <th>Department
                                                            </th>
                                                            <th>Stream
                                                            </th>
                                                            <th>EmailID
                                                            </th>
                                                            <th>MobileNo
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
                                                    <%#Container.DataItemIndex+1 %>
                                                </td>
                                                <td style="text-align: center;">
                                                    <asp:CheckBox ID="chkReport" TabIndex="4" runat="server" onClick="totSubjects(this);" ToolTip='<%# Eval("SeatNo") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblseatno" Text='<%# Eval("SeatNo") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblstudname" Text='<%# Eval("NameofCandidate") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <%# Eval("Category") %>
                                                </td>
                                                <td>
                                                    <%# Eval("Gender") %>
                                                </td>
                                                <td>
                                                    <%# Eval("Department") %>
                                                </td>
                                                <td>
                                                    <%# Eval("Stream") %>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" Text='<%# Eval("EmailID") %>' ID="lblemailid"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" Text='<%# Eval("MobileNo") %>' ID="lblmobileno"></asp:Label>

                                                </td>

                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                            <p>
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                    ShowSummary="False" ValidationGroup="show" DisplayMode="List" />
                            </p>
                            <div id="divMsg" runat="server">
                            </div>
                        </div>
                    </div>
                </div>
            </div>




        </ContentTemplate>

    </asp:UpdatePanel>
    <script type="text/javascript">
        function SelectAll(chk) {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');
            var hftot = document.getElementById('<%= hftot.ClientID %>');
            for (i = 0; i < hftot.value; i++) {

                var lst = document.getElementById('ctl00_ContentPlaceHolder1_lvStudentRecords_ctrl' + i + '_chkReport');
                if (lst.type == 'checkbox') {
                    if (chk.checked == true) {
                        lst.checked = true;
                        txtTot.value = hftot.value;
                    }
                    else {
                        lst.checked = false;
                        txtTot.value = 0;
                    }
                }

            }
        }

        function totSubjects(chk) {
            var txtTot = document.getElementById('<%= txtTotStud.ClientID %>');

            if (chk.checked == true)
                txtTot.value = Number(txtTot.value) + 1;
            else
                txtTot.value = Number(txtTot.value) - 1;

        }


    </script>

</asp:Content>


