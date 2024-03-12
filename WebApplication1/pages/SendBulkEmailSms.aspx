<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteMasterPage.master" CodeFile="SendBulkEmailSms.aspx.cs" Inherits="ACADEMIC_SendBulkEmailSms" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="<%=Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.css") %>" rel="stylesheet" />
    <script src="<%=Page.ResolveClientUrl("~/plugins/multiselect/bootstrap-multiselect.js")%>"></script>

    <%--<style>
        div.dd_chk_select {
            height: 34px;
            font-size: 14px !important;
            padding-left: 12px !important;
            line-height: 2.2 !important;
            width: 100%;
        }

            div.dd_chk_select div#caption {
                height: 35px;
            }
    </style>--%>
    <div>
        <asp:UpdateProgress ID="updProg" runat="server" AssociatedUpdatePanelID="upduser"
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

    <asp:UpdatePanel ID="upduser" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-12">
                    <div class="box box-primary">
                        <div id="div1" runat="server"></div>
                        <div class="box-header with-border">
                            <%--  <h3 class="box-title">SESSION CREATION</h3>--%>
                            <h3 class="box-title"><span>Bulk Email & SMS</span></h3>
                        </div>

                        <div class="box-body">
                            <div class="col-12">
                                <div class="row">
                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Admission Batch</label>
                                        </div>
                                        <asp:DropDownList ID="ddlAdmBatch" runat="server" AppendDataBoundItems="True" CssClass="form-control" data-select2-enable="true" TabIndex="1">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvadmbatch" runat="server" ControlToValidate="ddlAdmBatch" Display="None"
                                            ErrorMessage="Please Select Admission Batch" InitialValue="0" ValidationGroup="show">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>College</label>
                                        </div>
                                        <asp:DropDownList ID="ddlColg" runat="server" TabIndex="2" CssClass="form-control" data-select2-enable="true" AppendDataBoundItems="True" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlColg_SelectedIndexChanged" ToolTip="Please Select Institute">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvcollege" runat="server" ControlToValidate="ddlColg" Display="None" ErrorMessage="Please Select College"
                                            ValidationGroup="show" SetFocusOnError="True" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Degree</label>
                                        </div>
                                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="True" AutoPostBack="true" CssClass="form-control" data-select2-enable="true" TabIndex="3" OnSelectedIndexChanged="ddlDegree_SelectedIndexChanged">
                                            <asp:ListItem Value="0">Please Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDegree" runat="server" ControlToValidate="ddlDegree" Display="None" ErrorMessage="Please Select Degree"
                                            InitialValue="0" ValidationGroup="show">
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Branch</label>
                                        </div>
                                        <asp:ListBox ID="ddlBranch" runat="server" AppendDataBoundItems="true" CssClass="form-control multi-select-demo"
                                            SelectionMode="multiple" TabIndex="4" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"></asp:ListBox>

                                        <%--  <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch" Display="None" ErrorMessage="Please Select Branch"
                                            InitialValue="0" ValidationGroup="show">
                                        </asp:RequiredFieldValidator>--%>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Sending Options</label>
                                        </div>
                                        <asp:RadioButton ID="rbEmail" runat="server" AutoPostBack="true" Style="padding-left: 0px" TabIndex="4" TextAlign="Right"
                                            GroupName="Board" Font-Bold="true" OnCheckedChanged="rbEmail_CheckedChanged" EnableViewState="true" Text="Email" />

                                        <asp:RadioButton ID="rbSMS" runat="server" AutoPostBack="true" TabIndex="5" TextAlign="Right" GroupName="Board" Font-Bold="true"
                                            OnCheckedChanged="rbSMS_CheckedChanged" EnableViewState="true" Text="SMS" />

                                        <asp:RadioButton ID="rbBoth" runat="server" AutoPostBack="true" TabIndex="6" TextAlign="Right" GroupName="Board" Font-Bold="true"
                                            OnCheckedChanged="rbBoth_CheckedChanged" EnableViewState="true" Checked="true" Text="Both" />
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12" id="divSubject" runat="server">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Subject</label>
                                        </div>
                                        <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control" TabIndex="7" AutoCompleteType="Disabled"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Message</label>
                                        </div>
                                        <asp:TextBox ID="txtMatter" runat="server" TextMode="MultiLine" CssClass="form-control" TabIndex="8"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-lg-3 col-md-6 col-12">
                                        <div class="label-dynamic">
                                            <sup>* </sup>
                                            <label>Attach files</label>
                                        </div>
                                        <asp:FileUpload ID="fuAttachment" runat="server" AllowMultiple="true" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 btn-footer">
                                <asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click" ValidationGroup="show" CssClass="btn btn-outline-info" TabIndex="9" />
                                <asp:Button ID="btnSendSMS" runat="server" Text="Send Email & SMS" OnClick="btnSendSMS_Click" OnClientClick="ShowProgress();" Enabled="false" CssClass="btn btn-outline-info" TabIndex="10" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="False" TabIndex="11" CssClass="btn btn-outline-danger" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="show" ShowMessageBox="true" ShowSummary="false"
                                    DisplayMode="List" />
                            </div>

                            <div id="dvListView" class="col-12">
                                <asp:Panel ID="Panel1" runat="server">
                                    <asp:ListView ID="lvStudents" runat="server" Visible="false">
                                        <LayoutTemplate>
                                            <div class="sub-heading">
                                                <h5>Student List</h5>
                                            </div>
                                            <table class="table table-striped table-bordered nowrap display" style="width: 100%" id="tblStudents">
                                                <thead class="bg-light-blue">
                                                    <tr>
                                                        <th style="text-align: center;">Sr. No.</th>
                                                        <th>
                                                            <asp:CheckBox ID="cbHead" runat="server" onclick="totAllSubjects(this)" Visible="true" TabIndex="12" />
                                                            Select </th>
                                                        <th class="text-center">Reg. No. </th>
                                                        <th class="text-center">Student Name </th>
                                                        <th class="text-center">Mobile No. </th>
                                                        <th class="text-center">Email ID </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server" />
                                                </tbody>
                                            </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td style="text-align: center;"><%# Container.DataItemIndex+1 %></td>

                                                <td class="text-center">
                                                    <asp:CheckBox ID="chkRow" runat="server" Checked="false" TabIndex="13" />
                                                </td>
                                                <td class="text-center">
                                                    <asp:Label ID="lblreg" runat="server" Text='<%# Eval("REGNO")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblstud" runat="server" Text='<%# Eval("STUDNAME")%>'></asp:Label>
                                                </td>
                                                <td class="text-center">
                                                    <asp:Label ID="lblMobileNo" runat="server" Text='<%# Eval("UA_MOBILE")%>' ToolTip='<%# Eval("UA_MOBILE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblEmailId" runat="server" Text='<%# Eval("UA_EMAIL")%>' ToolTip='<%# Eval("UA_EMAIL")%>'></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </asp:Panel>
                            </div>
                            <div id="div2" runat="server">
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSendSMS" />
            <asp:PostBackTrigger ControlID="btnShow" />
            <asp:PostBackTrigger ControlID="ddlColg" />
            <asp:PostBackTrigger ControlID="ddlDegree" />
            <asp:PostBackTrigger ControlID="ddlBranch" />
            <asp:PostBackTrigger ControlID="rbEmail" />
            <asp:PostBackTrigger ControlID="rbSMS" />
            <asp:PostBackTrigger ControlID="rbBoth" />
            <%-- <asp:AsyncPostBackTrigger ControlID="btnSendSMS" />--%>
        </Triggers>
    </asp:UpdatePanel>

    <div id="divMsg" runat="server"></div>

    <script type="text/javascript">
        function ShowProgress() {
            document.getElementById('<% Response.Write(updProg.ClientID); %>').style.display = "inline";
        }
    </script>

    <script type="text/javascript" language="javascript">
        function totAllSubjects(headchk) {
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
    <script type="text/javascript">
        $(document).ready(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
        });
        var parameter = Sys.WebForms.PageRequestManager;
        parameter.add_endRequest(function () {
            $('.multi-select-demo').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200
            });
        });

    </script>
</asp:Content>
