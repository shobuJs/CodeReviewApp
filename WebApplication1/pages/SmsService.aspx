<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SmsService.aspx.cs" Inherits="SmsWebApps.SmsService"
    MasterPageFile="~/SiteMasterPage.master" Title="Sms Service" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="padding-left: 10px; width: 90%">
        <table width="100%">
            <tr>
                <td>
                    <ajaxToolKit:TabContainer ID="tcSmsService" runat="server" CssClass="ajax__tab_yuitabview-theme"
                        Width="100%" ActiveTabIndex="4">
                        <ajaxToolKit:TabPanel ID="tabAddSmsService" runat="server" HeaderText="Add Sms Service">
                            <HeaderTemplate>
                                Add Sms Service
                            </HeaderTemplate>
                            <ContentTemplate>
                                <fieldset class="fieldset">
                                    <legend class="legend">Add Sms Service</legend>
                                    <table width="100%">
                                        <tr>
                                            <td class="form_left_label" style="width: 25%">Service Name:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDisplayName" runat="server" Width="50%" TabIndex="1" CausesValidation="True"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvDisplayName" runat="server" ControlToValidate="txtDisplayName"
                                                    Display="None" ErrorMessage="Please Enter Service Name" SetFocusOnError="True"
                                                    ValidationGroup="smsService"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="form_left_label" style="width: 25%">Service Url :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtServiceUrl" runat="server" Width="50%" TabIndex="2" CausesValidation="True"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvServiceUrl" runat="server" ControlToValidate="txtServiceUrl"
                                                    Display="None" ErrorMessage="Please Enter Service Url" SetFocusOnError="True"
                                                    ValidationGroup="smsService"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="form_left_label" style="width: 25%">UserName :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtUsername" runat="server" Width="50%" TabIndex="3" CausesValidation="True"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ControlToValidate="txtUsername"
                                                    Display="None" ErrorMessage="Please Enter UserName" SetFocusOnError="True" ValidationGroup="smsService"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="form_left_label" style="width: 25%">Password :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPassword" runat="server" Width="50%" TabIndex="4" TextMode="Password"
                                                    CausesValidation="True"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvPass" runat="server" ControlToValidate="txtPassword"
                                                    Display="None" ErrorMessage="Please Enter Password" SetFocusOnError="True" ValidationGroup="smsService"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="form_left_label" style="width: 25%">Active
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="rbActive" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"
                                                    TabIndex="5">
                                                    <asp:ListItem Value="true">Yes</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="false">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="form_left_label" style="width: 25%">&nbsp;
                                                <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="True"
                                                    ShowSummary="False" ValidationGroup="smsService" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" TabIndex="6" OnClick="btnSubmit_Click"
                                                    ValidationGroup="smsService" />
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="7" OnClick="btnCancel_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="form_left_label" style="width: 25%">&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="form_left_label" colspan="2">
                                                <asp:ListView ID="lvSmsService" runat="server">
                                                    <LayoutTemplate>
                                                        <div class="vista-grid">
                                                            <div class="titlebar">
                                                                Available Sms Services
                                                            </div>
                                                            <table id="tblHead" class="datatable" cellpadding="0" cellspacing="0" style="width: 100%;">
                                                                <thead>
                                                                    <tr class="header">
                                                                        <th>Edit
                                                                        </th>
                                                                        <th style="width: 30%">Service Name
                                                                        </th>
                                                                        <th style="width: 60%">Service Url
                                                                        </th>
                                                                        <th style="width: 5%">Active
                                                                        </th>
                                                                    </tr>
                                                                </thead>
                                                            </table>
                                                        </div>
                                                        <div class="listview-container">
                                                            <div id="demo-grid" class="vista-grid">
                                                                <table class="datatable" cellpadding="0" cellspacing="0" style="width: 100%;">
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                            <td>
                                                                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/image/edit.gif" CommandName='<%# Eval("SERVICEID") %>' Width="20px" Height="20px"
                                                                    AlternateText="Edit Record" ToolTip="Edit Record" OnClick="btnEdit_Click1" />
                                                            </td>
                                                            <td style="width: 30%">
                                                                <%# Eval("DISPLAYNAME") %>
                                                                <asp:HiddenField ID="hdfIdNo" runat="server" Value='<%# Eval("SERVICEID") %>' />
                                                            </td>
                                                            <td style="width: 60%">
                                                                <%# Eval("SERVICENAME") %>
                                                            </td>
                                                            <td style="width: 10%">
                                                                <%# Eval("ACTIVE") %>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </ContentTemplate>
                        </ajaxToolKit:TabPanel>
                        <ajaxToolKit:TabPanel ID="tabMessageServer" runat="server" HeaderText="Add MessageServer">
                            <HeaderTemplate>
                                Add Message Server
                            </HeaderTemplate>
                            <ContentTemplate>
                                <fieldset class="fieldset">
                                    <legend class="legend">Add Message Server</legend>
                                    <table width="100%">
                                        <tr>
                                            <td class="form_left_label" style="width: 25%">MessageServer Name:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtMessageServerName" runat="server" Width="50%" TabIndex="1" AutoPostBack="True"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvServerName" runat="server" ControlToValidate="txtMessageServerName"
                                                    Display="None" ErrorMessage="Please Enter Message Server Name" SetFocusOnError="True"
                                                    ValidationGroup="showMsgServer"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="form_left_label" style="width: 25%">Service IP :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtIPAdd" runat="server" Width="50%" TabIndex="2" CausesValidation="True"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvIP" runat="server" ControlToValidate="txtIPAdd"
                                                    Display="None" ErrorMessage="Please Enter Server IP" SetFocusOnError="True" ValidationGroup="showMsgServer"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="form_left_label" style="width: 25%">Server Port :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPort" runat="server" Width="50%" TabIndex="3" CausesValidation="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="form_left_label" style="width: 25%">Web Service :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtWebService" runat="server" Width="50%" TabIndex="4" CausesValidation="True"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvWebService" runat="server" ControlToValidate="txtWebService"
                                                    Display="None" ErrorMessage="Please Enter Web Service" SetFocusOnError="True"
                                                    ValidationGroup="showMsgServer"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="form_left_label" style="width: 25%">Pending Sms :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPedingSms" runat="server" Width="50%" TabIndex="4" CausesValidation="True"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvPedingSms" runat="server" ControlToValidate="txtPedingSms"
                                                    Display="None" ErrorMessage="Please Enter Peding Sms no." SetFocusOnError="True"
                                                    ValidationGroup="showMsgServer"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="form_left_label" style="width: 25%">No of Try :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtNoofTry" runat="server" Width="50%" TabIndex="4" CausesValidation="True"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvNoofTry" runat="server" ControlToValidate="txtNoofTry"
                                                    Display="None" ErrorMessage="Please Enter No of try" SetFocusOnError="True" ValidationGroup="showMsgServer"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="form_left_label" style="width: 25%">Active
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="rbActiveFlag" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"
                                                    TabIndex="5">
                                                    <asp:ListItem Value="true">Yes</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="false">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="form_left_label" style="width: 25%">&nbsp;
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                                    ShowSummary="False" ValidationGroup="showMsgServer" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnAddMessageServer" runat="server" Text="Submit" TabIndex="6" OnClick="btnAddMessageServer_Click"
                                                    ValidationGroup="showMsgServer" />
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="btnCancelMsg" runat="server" Text="Cancel" TabIndex="7" OnClick="btnCancelMsg_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="form_left_label" style="width: 25%">&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="form_left_label" colspan="2">
                                                <asp:ListView ID="lvMsgServer" runat="server">
                                                    <LayoutTemplate>
                                                        <div class="vista-grid">
                                                            <div class="titlebar">
                                                                Available Message Server
                                                            </div>
                                                            <table id="tblHead" class="datatable" cellpadding="0" cellspacing="0" style="width: 100%;">
                                                                <thead>
                                                                    <tr class="header">
                                                                        <th style="width: 20%">Msg Server Name
                                                                        </th>
                                                                        <th style="width: 20%">IP
                                                                        </th>
                                                                        <th style="width: 10%">Port
                                                                        </th>
                                                                        <th style="width: 20%">Max Pending Sms
                                                                        </th>
                                                                        <th style="width: 10%">No. of try
                                                                        </th>
                                                                        <th style="width: 10%">Active
                                                                        </th>
                                                                    </tr>
                                                                </thead>
                                                            </table>
                                                        </div>
                                                        <div class="listview-container">
                                                            <div id="demo-grid" class="vista-grid">
                                                                <table class="datatable" cellpadding="0" cellspacing="0" style="width: 100%;">
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr class="item" onmouseover="this.style.backgroundColor='#FFFFAA'" onmouseout="this.style.backgroundColor='#FFFFFF'">
                                                            <td style="width: 20%">
                                                                <%# Eval("SERVERNAME") %>
                                                                <asp:HiddenField ID="hdfIdNo" runat="server" Value='<%# Eval("MSGSERVERID") %>' />
                                                            </td>
                                                            <td style="width: 20%">
                                                                <%# Eval("SERVERIP") %>
                                                            </td>
                                                            <td style="width: 10%">
                                                                <%# Eval("SERVERPORT") %>
                                                            </td>
                                                            <td style="width: 20%">
                                                                <%# Eval("PENDINGSMS") %>
                                                            </td>
                                                            <td style="width: 10%">
                                                                <%# Eval("NOOFRETRY") %>
                                                            </td>
                                                            <td style="width: 10%">
                                                                <%# Eval("ACTIVEFLAG") %>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </ContentTemplate>
                        </ajaxToolKit:TabPanel>

                        <ajaxToolKit:TabPanel ID="tabSendSmsDemo" runat="server" HeaderText="Send Demo Sms">
                            <HeaderTemplate>
                                Send Demo Sms
                            </HeaderTemplate>
                            <ContentTemplate>
                                <fieldset class="fieldset">
                                    <legend class="legend">Send Sms</legend>
                                    <table width="100%">
                                        <tr>
                                            <td class="form_left_label" style="width: 15%">Sms Code:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlSmsCode" runat="server" AutoPostBack="True" AppendDataBoundItems="True"
                                                    OnSelectedIndexChanged="ddlSmsCode_SelectedIndexChanged" CausesValidation="True">
                                                    <asp:ListItem Value="0">Please Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rvfCodeSMs" runat="server" ControlToValidate="ddlSmsCode"
                                                    Display="None" ErrorMessage="Please Enter Sms Code" InitialValue="0" SetFocusOnError="True"
                                                    ValidationGroup="showSmsSend"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="form_left_label" style="width: 15%">Parameter Count :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtSmsCodePara" runat="server" Width="50%" TabIndex="4" AutoPostBack="True"
                                                    Enabled="False"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="form_left_label" style="width: 15%">V1 :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtSV1" runat="server" Width="20%" TabIndex="3"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="Smstr1" runat="server" visible="False">
                                            <td id="Td1" class="form_left_label" style="width: 15%" runat="server">V2 :
                                            </td>
                                            <td id="Td2" runat="server">
                                                <asp:TextBox ID="txtSV2" runat="server" Width="20%" TabIndex="3"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="Smstr2" runat="server" visible="False">
                                            <td id="Td3" class="form_left_label" style="width: 15%" runat="server">V3 :
                                            </td>
                                            <td id="Td4" runat="server">
                                                <asp:TextBox ID="txtSV3" runat="server" Width="20%" TabIndex="3"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="Smstr3" runat="server" visible="False">
                                            <td id="Td5" class="form_left_label" style="width: 15%" runat="server">V4 :
                                            </td>
                                            <td id="Td6" runat="server">
                                                <asp:TextBox ID="txtSV4" runat="server" Width="20%" TabIndex="3"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="Smstr4" runat="server" visible="False">
                                            <td id="Td7" class="form_left_label" style="width: 15%" runat="server">V5 :
                                            </td>
                                            <td id="Td8" runat="server">
                                                <asp:TextBox ID="txtSV5" runat="server" Width="20%" TabIndex="3"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="Smstr5" runat="server" visible="False">
                                            <td id="Td9" class="form_left_label" style="width: 15%" runat="server">V6 :
                                            </td>
                                            <td id="Td10" runat="server">
                                                <asp:TextBox ID="txtSV6" runat="server" Width="20%" TabIndex="3"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="Smstr6" runat="server" visible="False">
                                            <td id="Td11" class="form_left_label" style="width: 15%" runat="server">V7 :
                                            </td>
                                            <td id="Td12" runat="server">
                                                <asp:TextBox ID="txtSV7" runat="server" TabIndex="3" Width="20%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="Smstr7" runat="server" visible="False">
                                            <td id="Td13" class="form_left_label" style="width: 15%" runat="server">V8 :
                                            </td>
                                            <td id="Td14" runat="server">
                                                <asp:TextBox ID="txtSV8" runat="server" TabIndex="3" Width="20%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="Smstr8" runat="server" visible="False">
                                            <td id="Td15" class="form_left_label" style="width: 15%" runat="server">V9 :
                                            </td>
                                            <td id="Td16" runat="server">
                                                <asp:TextBox ID="txtSV9" runat="server" TabIndex="3" Width="20%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="Smstr9" runat="server" visible="False">
                                            <td id="Td17" class="form_left_label" style="width: 15%" runat="server">V10 :
                                            </td>
                                            <td id="Td18" runat="server">
                                                <asp:TextBox ID="txtSV10" runat="server" TabIndex="3" Width="20%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Mobile No. :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtMobileNo" runat="server" TabIndex="3" Width="30%" CausesValidation="True"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="revMobileNo" runat="server" ControlToValidate="txtMobileNo"
                                                    Display="None" ErrorMessage="Please Enter Valid Mobile No" SetFocusOnError="True"
                                                    ValidationExpression="91\d{10}" ValidationGroup="showSmsSend"></asp:RegularExpressionValidator>
                                                <asp:RequiredFieldValidator ID="rfvMobileno" runat="server" ControlToValidate="txtMobileNo"
                                                    Display="None" ErrorMessage="Please Enter Mobile No" ValidationGroup="showSmsSend"></asp:RequiredFieldValidator>
                                                <br />
                                                (e.g : 91XXXXXXXXXX)
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="form_left_label" style="width: 15%">&nbsp;
                                                <asp:ValidationSummary ID="ValidationSummary4" runat="server" ShowMessageBox="True"
                                                    ShowSummary="False" ValidationGroup="showSmsSend" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnSendSms" runat="server" TabIndex="6" Text="Submit" ValidationGroup="showSmsSend"
                                                    OnClick="btnSendSms_Click" />
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="btnSendSmsCancel" runat="server" TabIndex="7" Text="Cancel" OnClick="btnSendSmsCancel_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="form_left_label" style="width: 15%">&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </ContentTemplate>
                        </ajaxToolKit:TabPanel>
                        <ajaxToolKit:TabPanel ID="tabSendPendingSms" runat="server" HeaderText="Send Pending Sms">
                            <HeaderTemplate>
                                Send Pending Sms[Manual]
                            </HeaderTemplate>
                            <ContentTemplate>
                                <fieldset class="fieldset">
                                    <legend class="legend">Send Pending Sms</legend>
                                    <table width="100%">
                                        <tr>
                                            <td class="form_left_label" colspan="2">
                                                <asp:Label ID="lblPendingSmsStatus" runat="server" Text="" Visible="false"></asp:Label>
                                                <asp:ListView ID="lvPendingSms" runat="server">
                                                    <LayoutTemplate>
                                                        <div class="vista-grid">
                                                            <div class="titlebar">
                                                                Available Pending Sms
                                                            </div>
                                                            <table id="tblHead" cellpadding="0" cellspacing="0" class="datatable" style="width: 100%;">
                                                                <thead>
                                                                    <tr class="header">
                                                                        <th style="width: 5%">Select
                                                                        </th>
                                                                        <th style="width: 60%">Sms
                                                                        </th>
                                                                        <th style="width: 20%">Mobileno
                                                                        </th>
                                                                        <th style="width: 15%">Send Date
                                                                        </th>
                                                                    </tr>
                                                                </thead>
                                                            </table>
                                                        </div>
                                                        <div class="listview-container">
                                                            <div id="demo-grid" class="vista-grid">
                                                                <table cellpadding="0" cellspacing="0" class="datatable" style="width: 100%;">
                                                                    <tbody>
                                                                        <tr id="itemPlaceholder" runat="server" />
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                        <tr class="item" onmouseout="this.style.backgroundColor='#FFFFFF'" onmouseover="this.style.backgroundColor='#FFFFAA'">
                                                            <td style="width: 20%">
                                                                <asp:CheckBox ID="chkSelect" runat="Server" ToolTip='<%# Eval("MSGID") %>' />
                                                                <asp:HiddenField ID="hdfMsgID" runat="server" Value='<%# Eval("MSGID") %>' />
                                                            </td>
                                                            <td style="width: 20%">
                                                                <asp:Label ID="lblSms" Text='<%# Eval("MSG_CONTENT") %>' runat="server"></asp:Label>
                                                            </td>
                                                            <td style="width: 60%">
                                                                <asp:Label ID="lblMobileno" Text='<%# Eval("MOBILENO") %>' runat="server"></asp:Label>
                                                            </td>
                                                            <td style="width: 20%">
                                                                <%# Eval("SENDING_DATE") %>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="form_left_label" style="width: 15%">&nbsp;
                                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                                                    ShowSummary="False" ValidationGroup="showSmsSend" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnSendPendingSms" runat="server" TabIndex="6" Text="Submit" ValidationGroup="showSmsSend"
                                                    OnClick="btnSendPendingSms_Click" />
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="btnCancel_pendingSms" runat="server" TabIndex="7" Text="Cancel" OnClick="btnCancel_pendingSms_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="form_left_label" style="width: 15%">&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </ContentTemplate>
                        </ajaxToolKit:TabPanel>
                        <ajaxToolKit:TabPanel ID="tabSendBulkSms" runat="server" HeaderText="Send Bulk Sms">
                            <HeaderTemplate>
                                Send Bulk Sms
                            </HeaderTemplate>
                            <ContentTemplate>
                                <fieldset class="fieldset">
                                    <legend class="legend">Send Bulk Sms</legend>
                                    <table width="100%">
                                        <tr>
                                            <td class="form_left_label" style="width: 15%">Sms
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtBulkSms" runat="server" Height="50px" MaxLength="160" TextMode="MultiLine"
                                                    Width="50%" CausesValidation="True"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvBulkSms" runat="server" ControlToValidate="txtBulkSms"
                                                    Display="None" ErrorMessage="Please Enter Sms" SetFocusOnError="True" ValidationGroup="BulkSms"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="form_left_label" style="width: 15%">Mobile No.
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtBulkMobileno" runat="server" Height="50px" MaxLength="160" TextMode="MultiLine"
                                                    Width="50%" CausesValidation="True"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvBulkMobileno" runat="server" ControlToValidate="txtBulkMobileno"
                                                    Display="None" ErrorMessage="Please Enter Mobile no(s)." SetFocusOnError="True"
                                                    ValidationGroup="BulkSms"></asp:RequiredFieldValidator>
                                                <br />
                                                (e.g : 91XXXXXXXXXX, 91XXXXXXXXXX)
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="form_left_label" style="width: 15%">&nbsp;
                                                <asp:ValidationSummary ID="ValidationSummary5" runat="server" ShowMessageBox="True"
                                                    ShowSummary="False" ValidationGroup="BulkSms" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnSubmitBulkSms" runat="server" TabIndex="6" Text="Submit" ValidationGroup="BulkSms"
                                                    OnClick="btnSubmitBulkSms_Click" />
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Button ID="btnCancelBulkSms" runat="server" TabIndex="7" Text="Cancel" OnClick="btnCancelBulkSms_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="form_left_label" style="width: 15%">&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </ContentTemplate>
                        </ajaxToolKit:TabPanel>
                    </ajaxToolKit:TabContainer>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
