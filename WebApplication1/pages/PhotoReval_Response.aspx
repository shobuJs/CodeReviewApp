<%@ Page Title="" Language="C#"  AutoEventWireup="true" CodeFile="PhotoReval_Response.aspx.cs" Inherits="PhotoReval_Response" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <style>

        input {
  background-color: #4CAF50; /* Green */
  border: none;
  color: white;
  padding: 8px 10px;
  text-align: center;
  text-decoration: none;
  display: inline-block;
  font-size: 16px;
}

    </style>


</head>
    <body>
          <form id="form1" runat="server">
 <div class="box box-primary">
        <div class="box-body">
              <center>

                   <div style="width: 600px; margin: 0px auto; border: groove; margin-top: 30px; height: 329px;">
                <h1 style="text-align: center">PAYMENT STATUS</h1>
                <asp:Literal ID="LiteralMessage" runat="server"></asp:Literal><br />
            </div>
            <div>
                <asp:Label ID="lblStatus" runat="server" style="font-size:x-large">Transaction Id :</asp:Label><br />
            </div>
            <br />
                                     <asp:Label runat="server" ID="LBL_DisplayResult"></asp:Label>
                                    <asp:Label ID="lblTXN_STATUS" runat="server" Visible="false"></asp:Label>
                                    <asp:Label ID="lblCLNT_TrackingTXN_REF" runat="server" Visible="false"></asp:Label>
                                    <asp:Label ID="lblTPSL_BANKREFNO_CD" runat="server" Visible="false"></asp:Label>

                                    <asp:Label ID="lblTXN_AMT" runat="server" Visible="false"></asp:Label>
                                    <asp:Label ID="lblTPSL_TXN_TIME" runat="server" Visible="false"></asp:Label>
                                    <asp:Label ID="lblCardName" runat="server" Visible="false"></asp:Label>
                                    <asp:Label ID="lblPaymentMode" runat="server" Visible="false"></asp:Label>
                                    <asp:Label ID="lblDatetime" runat="server" Visible="false"></asp:Label>

                                    <asp:Label ID="lblRec_Code" runat="server" Visible="false"></asp:Label>

                                    <asp:Label ID="lblIdno" runat="server" Visible="false"></asp:Label>

                Please note down the Transaction ID. 
                <asp:Label ID="lblTPSL_ORDER_TXN_ID" runat="server"></asp:Label>
                 for future reference.


              </center>

    <table style="text-align:center" width="100%">
       <%-- <tr style="text-align:center">
            <td colspan="3" style="padding-left: -12%; margin-top: 30px; text-align:center">
                <asp:Label ID="lblmessage" Font-Size="200%" runat="server"></asp:Label>
                <asp:HiddenField ID="hdnReceiptCode" runat="server" />
            </td>
        </tr>--%>
        <tr style="margin-top:10px;">
            <td style="text-align:center">
                <asp:Button ID="btnReports" runat="server" Text="Print Receipt" Visible="false" OnClick="btnReports_Click" class="buttonStyle ui-corner-all btn btn-success" /> &nbsp;&nbsp;&nbsp;
            
                <asp:Button ID="btnRegistrationSlip" runat="server" Text="Print Registration Slip" Visible="false" OnClick="btnRegistrationSlip_Click" class="buttonStyle ui-corner-all btn btn-success"/>
           
             <%--  <i class="fa fa-home text-primary" ></i>--%> <asp:LinkButton ID="lbtnGoBack" runat="server" Text="Go To Back" OnClick="lbtnGoBack_Click" Visible="false" Font-Bold="true" style="font-weight:bold;margin-left: 35px;"></asp:LinkButton>
            </td>
         
        </tr>
         
    </table>
    <div id="divMsg" runat="server">
    </div>

     </div>
     </div>
               </form>
    </body>
    </html>

