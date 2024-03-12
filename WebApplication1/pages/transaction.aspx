<%@ Page Language="C#" AutoEventWireup="true" CodeFile="transaction.aspx.cs" Inherits="_Default" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <title>Shri Vaishnav Vidyapeeth Vishwavidyalaya <img src="../IMAGES/indorebanner.png" style="width: 20%; display: block;" /></title>
    <script type="text/javascript" src="resources/js/jquery.js"></script>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">
    <meta name="author" content="">
</head>
<body>
    <table width="100%" cellspacing="0" cellpadding="0">
        <tbody>
            <tr>
                <td style="width: 70%; font-family: 'Times New Roman'; font-size: 16px; padding: 15px 0;">
                    <img src="../IMAGES/indorebanner.png" style="width: 80%; display: block;" />
                </td>
            </tr>
            <tr>
                <td>
                    <hr style="background-color: #1c8a99; border: 2px solid #1c8a99; width: 100%;">
                </td>
            </tr>
            <tr>
                <td></td>
            </tr>
            <tr>
                <th style="font-size: x-large;">Payment Confirmation
                </th>
            </tr>

        </tbody>
    </table>
    <form class="form-horizontal" runat="server" name="make_trans" action="sendtoairpay.aspx" method="post" onsubmit="return validate();">
        <center>
        <table style="height: 271px; width: 66%; margin-left: 101px; padding-top:1px;margin-top: 50px;">
            <tr>
                <td colspan="1" class="auto-style1">
                    <label>Registration No.<font color="red">*</font></label>
                </td>
                <td class="auto-style2">
                    <span id="Span1" name="buyerRegistrationNospan" style="display: none" class="makeRed"></span>
                    <asp:TextBox ID="buyerRegistrationNo" runat="server" ReadOnly="true" MaxLength="50" ></asp:TextBox><%--onkeypress="changecolor(this.id,'buyerRegistrationNo')"--%>
                </td>
                <td width="10%" colspan="3">
                    <label>Order ID<font color="red">*</font></label>
                </td>
                <td width="33%">
                    <span id="orderidspan" name="orderidspan" style="display: none" class="makeRed"></span>
                    <asp:TextBox ID="orderid" runat="server" onkeypress="changecolor(this.id,'orderidspan')"  ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="1" class="auto-style1">
                    <label>First Name<font color="red">*</font></label>
                </td>
                <td class="auto-style2">
                    <span id="buyerFirstNamespan" name="buyerFirstNamespan" style="display: none" class="makeRed"></span>
                    <asp:TextBox ID="buyerFirstName" runat="server" onkeypress="changecolor(this.id,'buyerFirstNamespan')" MaxLength="50" ReadOnly="true"></asp:TextBox>
                </td>
                <td width="10%" colspan="3">
                    <label>Last Name<font color="red">*</font></label>
                </td>
                <td width="33%">
                    <span id="buyerLastNamespan" name="buyerLastNamespan" style="display: none" class="makeRed"></span>
                    <asp:TextBox ID="buyerLastName" runat="server" onkeypress="changecolor(this.id,'buyerLastNamespan')" MaxLength="50" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="1" class="auto-style1">
                    <label>Email<font color="red">*</font></label>
                </td>
                <td class="auto-style2">
                    <span id="buyerEmailspan" name="buyerEmailspan" style="display: none" class="makeRed"></span>
                    <asp:TextBox ID="buyerEmail" runat="server" onkeypress="changecolor(this.id,'buyerEmailspan')" MaxLength="50" ReadOnly="true"></asp:TextBox>
                </td>

                <td width="10%" colspan="3">
                    <label>Phone<font color="red">*</font></label>
                </td>
                <td width="33%">
                    <span id="buyerPhonespan" name="buyerPhonespan" style="display: none" class="makeRed"></span>
                    <asp:TextBox ID="buyerPhone" runat="server" onkeypress="changecolor(this.id,'buyerPhonespan')" MaxLength="15" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="1" class="auto-style1">
                    <label>Amount (e.g. 99.50)<font color="red">*</font></label>
                </td>
                <td class="auto-style2"><span id="amountspan" name="amountspan" style="display: none" class="makeRed"></span>
                    <asp:TextBox ID="amount" runat="server" onkeypress="changecolor(this.id,'amountspan')" ReadOnly="true"></asp:TextBox>
                </td>
                <td width="10%" colspan="3">
                    <label>Address</label>
                </td>
                <td width="33%">
                    <span id="buyerAddressspan" name="buyerAddressspan" style="display: none" class="makeRed"></span>
                    <asp:TextBox ID="buyerAddress" runat="server" MaxLength="255" onkeypress="changecolor(this.id,'buyerAddressspan')" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="1" class="auto-style1">
                    <label>City</label>
                </td>
                <td class="auto-style2">
                    <span id="buyerCityspan" name="buyerCityspan" style="display: none" class="makeRed"></span>
                    <asp:TextBox ID="buyerCity" runat="server" MaxLength="50" onkeypress="changecolor(this.id,'buyerCityspan')" ReadOnly="true"></asp:TextBox>
                </td>
                <td width="10%" colspan="3">
                    <label>State</label>
                </td>
                <td width="33%">
                    <span id="buyerStatespan" name="buyerStatespan" style="display: none" class="makeRed"></span>
                    <asp:TextBox ID="buyerState" runat="server" MaxLength="50" onkeypress="changecolor(this.id,'buyerStatespan')" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="1" class="auto-style1">
                    <label>Country</label>
                </td>
                <td class="auto-style2">
                    <span id="buyerCountryspan" name="buyerCountryspan" style="display: none" class="makeRed"></span>
                    <asp:TextBox ID="buyerCountry" runat="server" MaxLength="50" onkeypress="changecolor(this.id,'buyerCountryspan')" ReadOnly="true"></asp:TextBox>
                </td>
                <td width="10%" colspan="3">
                    <label>Pin Code</label>
                </td>
                <td width="33%">
                    <span id="buyerPinCodespan" name="buyerPinCodespan" style="display: none" class="makeRed"></span>
                    <asp:TextBox ID="buyerPinCode" runat="server" MaxLength="8" onkeypress="changecolor(this.id,'buyerPinCodespan')" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
        </table>
</center>
        <div>
            <br>
            <div style="padding-left: 500px;">
                <button type="submit" style="font-family: Verdana; font-size: 8pt; font-weight: bold; width: 123px; background-color: rgb(27, 122, 135); color: white; height: 31px;" class="buttonStyle ui-corner-all">Pay Here</button>
            </div>
        </div>
    </form>
    <style>
        .makeRed {
            color: red;
        }
        .auto-style1 {
            width: 13%;
        }
        .auto-style2 {
            width: 17%;
        }
    </style>
    <script type="text/javascript">
        function validate() {
            var rt_type;
            if (document.getElementById('buyerEmail').value == "") {
                document.getElementById('buyerEmail').style.borderColor = 'red';
                document.getElementById('buyerEmailspan').style.display = '';
                document.getElementById('buyerEmailspan').innerHTML = 'Please enter email address.';
                rt_type = false;
            }
            else {
                var reg = /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/
                if (reg.test(document.getElementById('buyerEmail').value)) {
                    rt_type;
                } else {
                    document.getElementById('buyerEmail').style.borderColor = 'red';
                    document.getElementById('buyerEmailspan').style.display = '';
                    document.getElementById('buyerEmailspan').innerHTML = 'Please enter valid email.';
                    rt_type = false;
                }
            }
            if (document.getElementById('buyerPhone').value == "") {
                document.getElementById('buyerPhone').style.borderColor = 'red';
                document.getElementById('buyerPhonespan').style.display = '';
                document.getElementById('buyerPhonespan').innerHTML = 'Please enter phone number.';
                rt_type = false;
            }
            else {
                var specials = /^[\d\s\.\-]+$/
                if (!specials.test(document.getElementById('buyerPhone').value)) {
                    document.getElementById('buyerPhone').style.borderColor = 'red';
                    document.getElementById('buyerPhonespan').style.display = '';
                    document.getElementById('buyerPhonespan').innerHTML = 'Please enter valid phone number.';
                    rt_type = false;
                }
                else {
                    var phone = document.getElementById('buyerPhone').value;
                    if (phone.length < 8) {
                        document.getElementById('buyerPhone').style.borderColor = 'red';
                        document.getElementById('buyerPhonespan').style.display = '';
                        document.getElementById('buyerPhonespan').innerHTML = 'Phone number should be minimum 8 digit.';
                        rt_type = false;
                    }
                }
            }
            if (document.getElementById('buyerFirstName').value == "") {
                document.getElementById('txtbuyerFirstName').style.borderColor = 'red';
                document.getElementById('buyerFirstNamespan').style.display = '';
                document.getElementById('buyerFirstNamespan').innerHTML = 'Please enter first name.';
                rt_type = false;
            }
            else {
                var reg = /^[A-Za-z\d\s]+$/;
                if (!reg.test(document.getElementById('buyerFirstName').value)) {
                    document.getElementById('buyerFirstName').style.borderColor = 'red';
                    document.getElementById('buyerFirstNamespan').style.display = '';
                    document.getElementById('buyerFirstNamespan').innerHTML = 'Please enter valid first name.';
                    rt_type = false;
                }
                else {
                    var fname = document.getElementById('buyerFirstName').value;
                    if (fname.length < 1) {
                        document.getElementById('buyerFirstName').style.borderColor = 'red';
                        document.getElementById('buyerFirstNamespan').style.display = '';
                        document.getElementById('buyerFirstNamespan').innerHTML = 'First name should be minimum 1 character.';
                        rt_type = false;
                    }
                }
            }
            if (document.getElementById('buyerLastName').value == "") {
                document.getElementById('buyerLastName').style.borderColor = 'red';
                document.getElementById('buyerLastNamespan').style.display = '';
                document.getElementById('buyerLastNamespan').innerHTML = 'Please enter last name.';
                rt_type = false;
            }
            else {
                var reg = /^[A-Za-z\d\s]+$/;
                if (!reg.test(document.getElementById('buyerLastName').value)) {
                    document.getElementById('buyerLastName').style.borderColor = 'red';
                    document.getElementById('buyerLastNamespan').style.display = '';
                    document.getElementById('buyerLastNamespan').innerHTML = 'Please enter valid last name.';
                    rt_type = false;
                }
                else {
                    var fname = document.getElementById('buyerLastName').value;
                    if (fname.length < 1) {
                        document.getElementById('buyerLastName').style.borderColor = 'red';
                        document.getElementById('buyerLastNamespan').style.display = '';
                        document.getElementById('buyerLastNamespan').innerHTML = 'Last name should be minimum 1 character.';
                        rt_type = false;
                    }
                }
            }
            if (document.getElementById('buyerAddress').value != "") {
                var reg = /^[A-Za-z. ,;#$()-_\/]*$/;
                if (!reg.test(document.getElementById('buyerAddress').value)) {
                    document.getElementById('buyerAddress').style.borderColor = 'red';
                    document.getElementById('buyerAddressspan').style.display = '';
                    document.getElementById('buyerAddressspan').innerHTML = 'Please enter valid address.';
                    rt_type = false;
                }
            }
            if (document.getElementById('buyerPinCode').value != "") {

                var reg = /^[A-Za-z\d]+$/;
                if (!reg.test(document.getElementById('buyerPinCode').value)) {
                    document.getElementById('buyerPinCode').style.borderColor = 'red';
                    document.getElementById('buyerPinCodespan').style.display = '';
                    document.getElementById('buyerPinCodespan').innerHTML = 'Please enter valid pincode.';
                    rt_type = false;
                }
            }
            var reg = /^[A-Za-z\d]+$/;
            if (!reg.test(document.getElementById('orderid').value)) {
                document.getElementById('orderid').style.borderColor = 'red';
                document.getElementById('orderidspan').style.display = '';
                document.getElementById('orderidspan').innerHTML = 'Please enter valid order id.';
                rt_type = false;
            }
            if (document.getElementById('amount').value == "") {
                document.getElementById('amount').style.borderColor = 'red';
                document.getElementById('amountspan').style.display = '';
                document.getElementById('amountspan').innerHTML = 'Please enter amount.';
                rt_type = false;
            }
            else {
                val = document.getElementById('amount').value;
                if (!val.match(/^(\d{1,6})(\.\d{2})$/)) {
                    document.getElementById('amount').style.borderColor = 'red';
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
</body>
</html>
