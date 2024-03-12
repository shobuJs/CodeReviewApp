using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ACADEMIC_Online_Payment_transaction : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GetSessionValues();
    }

    private void GetSessionValues()
    {
        txtbuyerRegistrationNo.Text = Convert.ToString(Session["RegNo"]);
        txtbuyerFirstName.Text = Convert.ToString(Session["FirstName"]);
        txtbuyerLastName.Text = Convert.ToString(Session["LastName"]);
        txtbuyerEmail.Text = Convert.ToString(Session["EMAILID"]);
        txtbuyerPhone.Text = Convert.ToString(Session["MobileNo"]);
        txtorderid.Text = Convert.ToString(Session["OrderID"]);
        txtamount.Text = Convert.ToString(Session["TOTAL_AMT"]);
    }
}