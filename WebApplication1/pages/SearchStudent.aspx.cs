using System;
using System.Data;
using System.Text;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;

public partial class Academic_SearchStudent : System.Web.UI.Page
{
    string _searchString = string.Empty;
    string _searchBy = string.Empty;
    string _orderBy = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        /// This page is not used as User Interface anywhere hense has no UI. 
        /// It is asyncronously (using AJAX) called from FeeCollection.aspx page
        /// to search students based on enrollment number or student name.
        
        if (Request.QueryString["searchStr"] != null && Request.QueryString["searchStr"].ToString() != null)
            _searchString = Request.QueryString["searchStr"].ToString();

        if (Request.QueryString["fieldName"] != null && Request.QueryString["fieldName"].ToString() != null)
        {
            _searchBy = Request.QueryString["fieldName"].ToString();
            _orderBy = Request.QueryString["fieldName"].ToString();
        }
        FeeCollectionController feeController = new FeeCollectionController();
        DataSet ds = feeController.SearchStudents(_searchString, _searchBy, _orderBy);
        StringBuilder responseData = new StringBuilder();

        if (ds != null && ds.Tables.Count > 0)
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                responseData.Append("<option value=\"" + dr["IDNO"].ToString() + "\">");

                if (_searchBy.Equals("studname"))
                    responseData.Append(dr["STUDNAME"].ToString() + " - " + dr["REGNO"].ToString() + " - " + dr["DEGREENAME"].ToString() + " - " + dr["SHORTNAME"].ToString() + " - " + dr["SEMESTERNAME"].ToString());
                else if (_searchBy.Equals("regno"))
                    responseData.Append(dr["REGNO"].ToString() + " - " + dr["STUDNAME"].ToString() + " - " + dr["DEGREENAME"].ToString() + " - " + dr["SHORTNAME"].ToString() + " - " + dr["SEMESTERNAME"].ToString());
                else
                    responseData.Append(dr["SHORTNAME"].ToString() + " - " + dr["REGNO"].ToString() + " - " + dr["STUDNAME"].ToString() + " - " + dr["DEGREENAME"].ToString() + " - " + dr["SEMESTERNAME"].ToString());
                responseData.Append("</option>*");
            }
        }
        Response.ContentType = "text/plain";
        Response.Write(responseData.ToString());
    }
}