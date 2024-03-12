<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sendtoairpay.aspx.cs" Inherits="ACADEMIC_sendtoairpay" %>



<%    

    string username = ConfigurationManager.AppSettings.Get("username").ToString();
    string password = ConfigurationManager.AppSettings.Get("pass").ToString();
    string secretKey = ConfigurationManager.AppSettings.Get("secret").ToString();
    string allParamValue = null;

    string sRegNo = Request.Form["buyerRegistrationNo"].Trim();
    string sEmail = Request.Form["buyerEmail"].Trim();
    string sPhone = Request.Form["buyerPhone"].Trim();
    string sFName = Request.Form["buyerFirstName"].Trim();
    string sLName = Request.Form["buyerLastName"].Trim();
    string sAddress = Request.Form["buyerAddress"].Trim();
    string sCity = Request.Form["buyerCity"].Trim();
    string sState = Request.Form["buyerState"].Trim();
    string sCountry = Request.Form["buyerCountry"].Trim();
    string sPincode = Request.Form["buyerPinCode"].Trim();
    string sAmount = "01.00";                                  //  Request.Form["amount"].Trim();
    string sOrderId = Request.Form["orderid"].Trim();

    // server side validation
    validatepost(sRegNo, sEmail, sPhone, sFName, sLName, sAddress, sCity, sState, sCountry, sPincode, sAmount, sOrderId);

    allParamValue = sEmail + sFName + sLName + sAddress + sCity + sState + sCountry + sAmount + sOrderId;
    DateTime now1 = DateTime.Today; // As DateTime
    string now = now1.ToString("yyyy-MM-dd"); // As String
    string allParamValue1 = allParamValue + now;
    string sTemp = secretKey + "@" + username + ":|:" + password;
    string str256Key = EncryptSHA256Managed(sTemp);
    string allParamValue12 = allParamValue1 + str256Key;
    string checksum1 = MD5Hash(allParamValue12);
    checksum.Text = checksum1;
    privatekey.Text = str256Key;
            
%>


<%--<!DOCTYPE html>--%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="Cache-Control" content="no-cache, no-store, must-revalidate" />
    <meta http-equiv="Cache-Control" content="post-check=0, pre-check=0', false" />
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Expires" content="Sat, 26 Jul 1997 05:00:00 GMT" />
    <meta http-equiv="Last-Modified" content='" + now1( D, d M Y H:i:s ) + "GMT' />

    <title>Airpay</title>
    <script type="text/javascript" src="resources/js/jquery.js"></script>
    <script type="text/javascript">
        function submitForm() {
            var form = document.forms[0];
            form.submit();
        }
    </script>
</head>


<body onload="javascript:submitForm()">

    <center>
        <table width="500px;">
            <tr>
                <td align="center" valign="middle">Do Not Refresh or Press Back
                    <br />
                    Redirecting to Airpay</td>
            </tr>
            <tr>
                <td align="center" valign="middle">
                    <form id="Form1" action="https://payments.airpay.co.in/pay/index.php" method="post" runat="server">

                        <input type="hidden" name="currency" value="356" />
                        <input type="hidden" name="isocurrency" value="INR" />

                        <input type="hidden" name="orderid" value="<%=Request.Form["orderid"]%>" />
                        <input type="hidden" name="buyerEmail" value="<%=Request.Form["buyerEmail"]%>" />
                        <input type="hidden" name="buyerPhone" value="<%=Request.Form["buyerPhone"]%>" />
                        <input type="hidden" name="buyerFirstName" value="<%=Request.Form["buyerFirstName"]%>" />
                        <input type="hidden" name="buyerLastName" value="<%=Request.Form["buyerLastName"]%>" />
                        <input type="hidden" name="buyerAddress" value="<%=Request.Form["buyerAddress"]%>" />
                        <input type="hidden" name="buyerCity" value="<%=Request.Form["buyerCity"]%>" />
                        <input type="hidden" name="buyerState" value="<%=Request.Form["buyerState"]%>" />
                        <input type="hidden" name="buyerCountry" value="<%=Request.Form["buyerCountry"]%>" />
                        <input type="hidden" name="buyerPinCode" value="<%=Request.Form["buyerPinCode"]%>" />
                        <input type="hidden" name="amount" value="01.00" />
                        <input type="hidden" name="chmod" value="" />

                        <asp:TextBox ID="checksum" runat="server" Style="display: none;"></asp:TextBox>
                        <asp:TextBox ID="privatekey" runat="server" Style="display: none;"></asp:TextBox>
                        <input type="hidden" name="mercid" value="<%= ConfigurationManager.AppSettings["mercid"].ToString() %>" />

                    </form>
                </td>

            </tr>

        </table>

    </center>
</body>
</html>
