<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true"
    CodeFile="Online_Payment_transaction.aspx.cs" Inherits="ACADEMIC_Online_Payment_transaction" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Content/jquery.js" type="text/javascript"></script>
    <script src="../Content/jquery.dataTables.js" language="javascript" type="text/javascript"></script>
    <script type="text/javascript" src="resources/js/jquery.js"></script>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="vista_page_title_bar">ONLINE PAYMENT TRANSACTION
                <!-- Button used to launch the help (animation) -->
                <asp:ImageButton ID="btnHelp" runat="server" ImageUrl="~/images/help.gif" OnClientClick="return false;"
                    AlternateText="Page Help" ToolTip="Page Help" />
            </td>
        </tr>
        <tr>
            <td></td>
        </tr>
    </table>
    <div style="color: Red; font-weight: bold; height: 18px;">
        &nbsp;&nbsp; Note : * marked fields are Mandatory
    </div>
    <div></div>
    <fieldset class="fieldset">
        <legend class="legend">Buyer Information</legend>
        <form class="form-horizontal" name="make_trans" action="sendtoairpay.aspx" method="post" onsubmit="return validate();">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td width="12%" colspan="1">
                        <label>Buyer Registration No.<font color="red">*</font></label>
                    </td>
                    <td width="33%">
                        <span id="Span1" name="buyerRegistrationNo" style="display: none" class="makeRed"></span>
                        <asp:TextBox ID="txtbuyerRegistrationNo" runat="server" onkeypress="changecolor(this.id,'buyerRegistrationNo')" MaxLength="50"></asp:TextBox>
                    </td>
                    <td width="10%" colspan="3">
                        <label>Order ID<font color="red">*</font></label>
                    </td>
                    <td width="33%">
                        <span id="orderidspan" name="orderidspan" style="display: none" class="makeRed"></span>
                        <asp:TextBox ID="txtorderid" runat="server" onkeypress="changecolor(this.id,'orderidspan')"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="10%" colspan="1">
                        <label>Buyer First Name<font color="red">*</font></label>
                    </td>
                    <td width="33%">
                        <span id="buyerFirstNamespan" name="buyerFirstNamespan" style="display: none" class="makeRed"></span>
                        <asp:TextBox ID="txtbuyerFirstName" runat="server" onkeypress="changecolor(this.id,'buyerFirstNamespan')" MaxLength="50"></asp:TextBox>
                    </td>
                    <td width="10%" colspan="3">
                        <label>Buyer Last Name<font color="red">*</font></label>
                    </td>
                    <td width="33%">
                        <span id="buyerLastNamespan" name="buyerLastNamespan" style="display: none" class="makeRed"></span>
                        <asp:TextBox ID="txtbuyerLastName" runat="server" onkeypress="changecolor(this.id,'buyerLastNamespan')" MaxLength="50"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="10%" colspan="1">
                        <label>Buyer Email<font color="red">*</font></label>
                    </td>
                    <td width="33%">
                        <span id="buyerEmailspan" name="buyerEmailspan" style="display: none" class="makeRed"></span>
                        <asp:TextBox ID="txtbuyerEmail" runat="server" onkeypress="changecolor(this.id,'buyerEmailspan')" MaxLength="50"></asp:TextBox>
                    </td>

                    <td width="10%" colspan="3">
                        <label>Buyer Phone<font color="red">*</font></label>
                    </td>
                    <td width="33%">
                        <span id="buyerPhonespan" name="buyerPhonespan" style="display: none" class="makeRed"></span>
                        <asp:TextBox ID="txtbuyerPhone" runat="server" onkeypress="changecolor(this.id,'buyerPhonespan')" MaxLength="15"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="10%" colspan="1">
                        <label>Amount (e.g. 99.50)<font color="red">*</font></label>
                    </td>
                    <td width="33%"><span id="amountspan" name="amountspan" style="display: none" class="makeRed"></span>
                        <asp:TextBox ID="txtamount" runat="server" onkeypress="changecolor(this.id,'amountspan')"></asp:TextBox>
                    </td>
                    <td width="10%" colspan="3">
                        <label>Buyer Address</label>
                    </td>
                    <td width="33%">
                        <span id="buyerAddressspan" name="buyerAddressspan" style="display: none" class="makeRed"></span>
                        <asp:TextBox ID="txtbuyerAddress" runat="server" MaxLength="255" onkeypress="changecolor(this.id,'buyerAddressspan')"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="10%" colspan="1">
                        <label>Buyer City</label>
                    </td>
                    <td width="33%">
                        <span id="buyerCityspan" name="buyerCityspan" style="display: none" class="makeRed"></span>
                        <asp:TextBox ID="txtbuyerCity" runat="server" MaxLength="50" onkeypress="changecolor(this.id,'buyerCityspan')"></asp:TextBox>
                    </td>
                    <td width="10%" colspan="3">
                        <label>Buyer State</label>
                    </td>
                    <td width="33%">
                        <span id="buyerStatespan" name="buyerStatespan" style="display: none" class="makeRed"></span>
                        <asp:TextBox ID="txtbuyerState" runat="server" MaxLength="50" onkeypress="changecolor(this.id,'buyerStatespan')"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="10%" colspan="1">
                        <label>Buyer Country</label>
                    </td>
                    <td width="33%">
                        <span id="buyerCountryspan" name="buyerCountryspan" style="display: none" class="makeRed"></span>
                        <asp:TextBox ID="txtbuyerCountry" runat="server" MaxLength="50" onkeypress="changecolor(this.id,'buyerCountryspan')"></asp:TextBox>
                    </td>
                    <td width="10%" colspan="3">
                        <label>Buyer Pin Code</label>
                    </td>
                    <td width="33%">
                        <span id="buyerPinCodespan" name="buyerPinCodespan" style="display: none" class="makeRed"></span>
                        <asp:TextBox ID="txtbuyerPinCode" runat="server" MaxLength="8" onkeypress="changecolor(this.id,'buyerPinCodespan')"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="3" style="padding-left: 40%;">
                        <button type="submit" class="btn">Pay Here</button>
                    </td>
                </tr>
            </table>
        </form>
    </fieldset>
    <script type="text/javascript">
        function validate() {
            var rt_type;
            if (document.getElementById('txtbuyerEmail').value == "") {
                document.getElementById('txtbuyerEmail').style.borderColor = 'red';
                document.getElementById('buyerEmailspan').style.display = '';
                document.getElementById('buyerEmailspan').innerHTML = 'Please enter email address.';
                rt_type = false;
            }
            else {
                var reg = /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/
                if (reg.test(document.getElementById('txtbuyerEmail').value)) {
                    rt_type;
                } else {
                    document.getElementById('txtbuyerEmail').style.borderColor = 'red';
                    document.getElementById('buyerEmailspan').style.display = '';
                    document.getElementById('buyerEmailspan').innerHTML = 'Please enter valid email.';
                    rt_type = false;
                }
            }
            if (document.getElementById('txtbuyerPhone').value == "") {
                document.getElementById('txtbuyerPhone').style.borderColor = 'red';
                document.getElementById('buyerPhonespan').style.display = '';
                document.getElementById('buyerPhonespan').innerHTML = 'Please enter phone number.';
                rt_type = false;
            }
            else {
                var specials = /^[\d\s\.\-]+$/
                if (!specials.test(document.getElementById('txtbuyerPhone').value)) {
                    document.getElementById('txtbuyerPhone').style.borderColor = 'red';
                    document.getElementById('buyerPhonespan').style.display = '';
                    document.getElementById('buyerPhonespan').innerHTML = 'Please enter valid phone number.';
                    rt_type = false;
                }
                else {
                    var phone = document.getElementById('txtbuyerPhone').value;
                    if (phone.length < 8) {
                        document.getElementById('txtbuyerPhone').style.borderColor = 'red';
                        document.getElementById('buyerPhonespan').style.display = '';
                        document.getElementById('buyerPhonespan').innerHTML = 'Phone number should be minimum 8 digit.';
                        rt_type = false;
                    }
                }
            }
            if (document.getElementById('txtbuyerFirstName').value == "") {
                document.getElementById('txtbuyerFirstName').style.borderColor = 'red';
                document.getElementById('buyerFirstNamespan').style.display = '';
                document.getElementById('buyerFirstNamespan').innerHTML = 'Please enter first name.';
                rt_type = false;
            }
            else {
                var reg = /^[A-Za-z\d\s]+$/;
                if (!reg.test(document.getElementById('txtbuyerFirstName').value)) {
                    document.getElementById('txtbuyerFirstName').style.borderColor = 'red';
                    document.getElementById('buyerFirstNamespan').style.display = '';
                    document.getElementById('buyerFirstNamespan').innerHTML = 'Please enter valid first name.';
                    rt_type = false;
                }
                else {
                    var fname = document.getElementById('txtbuyerFirstName').value;
                    if (fname.length < 1) {
                        document.getElementById('txtbuyerFirstName').style.borderColor = 'red';
                        document.getElementById('buyerFirstNamespan').style.display = '';
                        document.getElementById('buyerFirstNamespan').innerHTML = 'First name should be minimum 1 character.';
                        rt_type = false;
                    }
                }
            }
            if (document.getElementById('txtbuyerLastName').value == "") {
                document.getElementById('txtbuyerLastName').style.borderColor = 'red';
                document.getElementById('buyerLastNamespan').style.display = '';
                document.getElementById('buyerLastNamespan').innerHTML = 'Please enter last name.';
                rt_type = false;
            }
            else {
                var reg = /^[A-Za-z\d\s]+$/;
                if (!reg.test(document.getElementById('txtbuyerLastName').value)) {
                    document.getElementById('txtbuyerLastName').style.borderColor = 'red';
                    document.getElementById('buyerLastNamespan').style.display = '';
                    document.getElementById('buyerLastNamespan').innerHTML = 'Please enter valid last name.';
                    rt_type = false;
                }
                else {
                    var fname = document.getElementById('txtbuyerLastName').value;
                    if (fname.length < 1) {
                        document.getElementById('txtbuyerLastName').style.borderColor = 'red';
                        document.getElementById('buyerLastNamespan').style.display = '';
                        document.getElementById('buyerLastNamespan').innerHTML = 'Last name should be minimum 1 character.';
                        rt_type = false;
                    }
                }
            }
            if (document.getElementById('txtbuyerAddress').value != "") {
                var reg = /^[A-Za-z. ,;#$()-_\/]*$/;
                if (!reg.test(document.getElementById('txtbuyerAddress').value)) {
                    document.getElementById('txtbuyerAddress').style.borderColor = 'red';
                    document.getElementById('buyerAddressspan').style.display = '';
                    document.getElementById('buyerAddressspan').innerHTML = 'Please enter valid address.';
                    rt_type = false;
                }
            }
            if (document.getElementById('txtbuyerPinCode').value != "") {

                var reg = /^[A-Za-z\d]+$/;
                if (!reg.test(document.getElementById('txtbuyerPinCode').value)) {
                    document.getElementById('txtbuyerPinCode').style.borderColor = 'red';
                    document.getElementById('buyerPinCodespan').style.display = '';
                    document.getElementById('buyerPinCodespan').innerHTML = 'Please enter valid pincode.';
                    rt_type = false;
                }
            }
            var reg = /^[A-Za-z\d]+$/;
            if (!reg.test(document.getElementById('txtorderid').value)) {
                document.getElementById('txtorderid').style.borderColor = 'red';
                document.getElementById('orderidspan').style.display = '';
                document.getElementById('orderidspan').innerHTML = 'Please enter valid order id.';
                rt_type = false;
            }
            if (document.getElementById('txtamount').value == "") {
                document.getElementById('txtamount').style.borderColor = 'red';
                document.getElementById('amountspan').style.display = '';
                document.getElementById('amountspan').innerHTML = 'Please enter amount.';
                rt_type = false;
            }
            else {
                val = document.getElementById('txtamount').value;
                if (!val.match(/^(\d{1,6})(\.\d{2})$/)) {
                    document.getElementById('txtamount').style.borderColor = 'red';
                    document.getElementById('amountspan').style.display = '';
                    document.getElementById('amountspan').innerHTML = 'Please enter valid amount e.g. 99.50';
                    rt_type = false;
                }
            }
            return rt_type;
        }
        function changecolor(txtid, spanid) {
            document.getElementById(txtid).style.borderColor = '';
            document.getElementById(spanid).style.display = 'none';
        }
    </script>
    <style>
        .makeRed {
            color: red;
        }
    </style>
</asp:Content>

