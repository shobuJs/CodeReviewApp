using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Text;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Web.UI.WebControls;
using System.IO;

public partial class ACADEMIC_sendtoairpay : System.Web.UI.Page
{
    string sendBack = "";
    string error = "";
    string id = "";
    string value = "";
    string action = "";

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public string EncryptSHA256Managed(String ClearString)
    {
        byte[] bytClearString = System.Text.ASCIIEncoding.UTF8.GetBytes(ClearString);

        HashAlgorithm sha = new SHA256Managed();

        byte[] hash = sha.ComputeHash(bytClearString);

        StringBuilder hexString = new StringBuilder(hash.Length);
        for (int i = 0; i < hash.Length; i++)
        {
            hexString.Append(hash[i].ToString("x2"));
        }
        return hexString.ToString();

    }

    public string validatepost(string sRegNo, string sEmail, string sPhone, string sFName, string sLName, string sAddress, string sCity, string sState, string sCountry, string sPincode, string sAmount, string sOrderId)
    {
        if (sRegNo == "" && sEmail == "" && sPhone == "" && sFName == "" && sLName == "" && sAmount == "")
        {
            createsendBack(error = "ALL", "error.aspx");
        }
        if (sEmail == "")
        {
            createsendBack(error = "E", "error.aspx");
        }
        if (Convert.ToInt32(sEmail.Length) > 50)
        {
            createsendBack(error = "VE", "error.aspx");
        }
        else
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(sEmail);
            if (!match.Success)
                createsendBack(error = "VE", "error.aspx");
        }
        if (sPhone == "")
        {
            createsendBack(error = "BP", "error.aspx");
        }
        else
        {
            Regex regex = new Regex(@"^([0-9]{8,15})$");
            Match match = regex.Match(sPhone);
            if (match.Success)
            { }
            else
            {
                createsendBack(error = "VBP", "error.aspx");
            }
        }
        if (sFName == "")
        {
            createsendBack(error = "FN", "error.aspx");
        }
        else
        {
            Regex regex = new Regex(@"^([a-zA-Z0-9]{1,50})$");
            Match match = regex.Match(sFName);
            if (match.Success)
            { }
            else
            {
                createsendBack(error = "VFN", "error.aspx");
            }
        }
        if (sLName == "")
        {
            createsendBack(error = "LN", "error.aspx");
        }
        else
        {
            Regex regex = new Regex(@"^([a-zA-Z0-9]{1,50})$");
            Match match = regex.Match(sLName);
            if (!match.Success)
                createsendBack(error = "VLN", "error.aspx");
        }
        if (sAddress != "")
        {
            Regex regex = new Regex(@"^([a-zA-Z0-9,;.#$/\( )-]{4,255})$");
            Match match = regex.Match(sAddress);
            if (!match.Success)
            {
                createsendBack(error = "VADD", "error.aspx");
            }
        }
        if (sCity != "")
        {
            Regex regex = new Regex(@"^([a-zA-Z0-9]{2,50})$");
            Match match = regex.Match(sCity);
            if (!match.Success)
            {
                createsendBack(error = "VCIT", "error.aspx");
            }
        }
        if (sState != "")
        {
            Regex regex = new Regex(@"^([a-zA-Z0-9]{2,50})$");
            Match match = regex.Match(sState);
            if (!match.Success)
            {
                createsendBack(error = "VSTA", "error.aspx");
            }
        }
        if (sCountry != "")
        {
            Regex regex = new Regex(@"^([a-zA-Z0-9]{2,50})$");
            Match match = regex.Match(sCountry);
            if (!match.Success)
            {
                createsendBack(error = "VCON", "error.aspx");
            }
        }
        if (sPincode != "")
        {
            Regex regex = new Regex(@"^([a-zA-Z0-9]{4,8})$");
            Match match = regex.Match(sPincode);
            if (!match.Success)
            {
                createsendBack(error = "VPIN", "error.aspx");
            }
        }
        if (sAmount == "")
        {
            createsendBack(error = "A", "error.aspx");
        }
        else
        {
            Regex regex = new Regex(@"^\d{1,6}\.\d{1,2}$");
            Match match = regex.Match(sAmount);
            if (!match.Success)
                createsendBack(error = "VA", "error.aspx");
        }
        return true.ToString();
    }

    public string createsendBack(string err, string action)
    {
        Response.Write("<!DOCTYPE HTML>");
        Response.Write("<html lang='en'>");
        Response.Write("<head>");
        Response.Write("<meta charset='utf-8' />");
        Response.Write("</head>");
        Response.Write("<body onLoad='javascript:submitForm()'>");
        Response.Write("<form name='errorform' id='errorform' method='post' action='" + action + "'>");
        Response.Write("<input type='hidden' id='status' name='status' value='" + err + "'>");
        Response.Write("</form>");
        Response.Write("</body>");
        Response.Write("</html>");
        return false.ToString();
    }

    public static string MD5Hash(string text)
    {
        byte[] bytClearString = System.Text.ASCIIEncoding.UTF8.GetBytes(text);

        MD5 md5 = new MD5CryptoServiceProvider();
        byte[] hash = md5.ComputeHash(bytClearString);


        byte[] result = md5.Hash;

        StringBuilder strBuilder = new StringBuilder(hash.Length);
        for (int i = 0; i < hash.Length; i++)
        {

            strBuilder.Append(hash[i].ToString("x2"));
        }

        return strBuilder.ToString();
    }


}