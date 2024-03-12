using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class eror : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string status = null;
        Response.Write("<div align = 'center'><a href= 'transaction.aspx'>Back</a></div>");
        if (Request.Form["status"] != "")
        {
            status = Request.Form["status"];
        }
        if (status == "ALL")
        {
            Response.Write("All fields are mendatory.");
        }
        if (status == "E")
        {
            Response.Write("Please enter email address.");
        }
        if (status == "VE")
        {
            Response.Write("Please enter valid email.");
        }
        if (status == "BP")
        {
            Response.Write("Please enter phone number.");
        }
        if (status == "VBP")
        {
            Response.Write("Please enter valid phone number.");
        }
        if (status == "FN")
        {
            Response.Write("Please enter first name.");
        }
        if (status == "VFN")
        {
            Response.Write("Please enter valid first name.");
        }
        if (status == "LN")
        {
            Response.Write("Please enter last name.");
        }
        if (status == "VLN")
        {
            Response.Write("Please enter valid last name.");
        }
        if (status == "VADD")
        {
            Response.Write("Please enter valid address.");
        }
        if (status == "VCIT")
        {
            Response.Write("Please enter valid City Name.");
        }
        if (status == "VSTA")
        {
            Response.Write("Please enter valid State");
        }
        if (status == "VCON")
        {
            Response.Write("Please enter valid Country Name.");
        }
        if (status == "VADD")
        {
            Response.Write("Please enter valid address.");
        }
        if (status == "VPIN")
        {
            Response.Write("Please enter valid PIN.");
        }
        if (status == "A")
        {
            Response.Write("Please enter amount.");
        }
        if (status == "VA")
        {
            Response.Write("Please enter valid amount.");
        }
    }
}