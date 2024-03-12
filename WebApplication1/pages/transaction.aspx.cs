using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GetSessionValues();
    }


    private void GetSessionValues()
    {
        buyerRegistrationNo.Text = Convert.ToString(Session["RegNo"]);
        buyerFirstName.Text = Convert.ToString(Session["FirstName"]);
        buyerLastName.Text = Convert.ToString(Session["LastName"]);
        buyerEmail.Text = Convert.ToString(Session["EMAILID"]);
        buyerPhone.Text = Convert.ToString(Session["MobileNo"]);
        orderid.Text = Convert.ToString(Session["OrderID"]);
        amount.Text = Convert.ToString(Session["TOTAL_AMT"]);
    }
}