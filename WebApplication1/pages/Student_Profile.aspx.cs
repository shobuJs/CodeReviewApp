//using Azure.Storage.Blobs.Models;
using DocumentFormat.OpenXml.Spreadsheet;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Academic_Student_Profile : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    StudentProfileController objController = new StudentProfileController();
    StudentProfile objEntity = new StudentProfile();
    static int Idno = 0;
    #region Page_Load
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                //Check Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    //Page Authorization
                    CheckPageAuthorization();
                    //Set the Page Title
                    if (Session["usertype"].ToString() == "2")
                    {
                        DivSearch.Visible = false;
                        Idno = Convert.ToInt32(Session["idno"]);
                        hdnClientId.Value = Session["idno"].ToString();

                    }
                    else
                    {
                        DivSearch.Visible = true;
                        Idno = 0;
                        // Search
                    }
                    Page.Title = Session["coll_name"].ToString();
                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                    }
                    //objCommon.SetLabelData(Convert.ToString(Request.QueryString["pageno"]));//for label
                    //objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"])); //for header

                }

            }
            // Page.Form.Attributes.Add("enctype", "multipart/form-data");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "Academic->Academic_Student_Profile.Page_Load() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=schememaster.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=schememaster.aspx");
        }
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static StudentProfile.AllDropDownBindMethod BindDropDown(int Id, string Type)
    {
        List<StudentProfile.GenderDropDown> GenderDropDown = new List<StudentProfile.GenderDropDown>();
        List<StudentProfile.ReligionDropDown> ReligionDropDown = new List<StudentProfile.ReligionDropDown>();
        List<StudentProfile.OccupationDropDown> OccupationDropDown = new List<StudentProfile.OccupationDropDown>();
        List<StudentProfile.RelationshipDropDown> RelationshipDropDown = new List<StudentProfile.RelationshipDropDown>();
        List<StudentProfile.CountryDropDown> CountryDropDown = new List<StudentProfile.CountryDropDown>();
        List<StudentProfile.VisaStatusDropDown> VisaStatusDropDown = new List<StudentProfile.VisaStatusDropDown>();
        List<StudentProfile.VisaTypeDropDown> VisaTypeDropDown = new List<StudentProfile.VisaTypeDropDown>();
        List<StudentProfile.CitizenshipDropDown> CitizenshipDropDown = new List<StudentProfile.CitizenshipDropDown>();

        StudentProfile.AllDropDownBindMethod AllDropDownBindMethod = new StudentProfile.AllDropDownBindMethod();
        StudentProfileController objController = new StudentProfileController();
        DataSet ds = new DataSet();
        try
        {
            ds = objController.Populate_DropDown_List(Id, Type);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GenderDropDown = (from DataRow dr in ds.Tables[0].Rows
                                  select new StudentProfile.GenderDropDown
                                  {
                                      GenderNo = Convert.ToInt32(dr[0].ToString()),
                                      GenderName = dr[1].ToString()
                                  }).ToList();
            }
            else { GenderDropDown = null; }

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
            {
                ReligionDropDown = (from DataRow dr in ds.Tables[1].Rows
                                    select new StudentProfile.ReligionDropDown
                                    {
                                        ReligionNo = Convert.ToInt32(dr[0].ToString()),
                                        ReligionName = dr[1].ToString()
                                    }).ToList();
            }
            else { ReligionDropDown = null; }


            if (ds != null && ds.Tables.Count > 0 && ds.Tables[2].Rows.Count > 0)
            {
                OccupationDropDown = (from DataRow dr in ds.Tables[2].Rows
                                      select new StudentProfile.OccupationDropDown
                                      {
                                          OccupationNo = Convert.ToInt32(dr[0].ToString()),
                                          OccupationName = dr[1].ToString()
                                      }).ToList();
            }
            else { OccupationDropDown = null; }

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[3].Rows.Count > 0)
            {
                RelationshipDropDown = (from DataRow dr in ds.Tables[3].Rows
                                        select new StudentProfile.RelationshipDropDown
                                        {
                                            RelationshipNo = Convert.ToInt32(dr[0].ToString()),
                                            RelationshipName = dr[1].ToString()
                                        }).ToList();
            }
            else { RelationshipDropDown = null; }
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[4].Rows.Count > 0)
            {
                CountryDropDown = (from DataRow dr in ds.Tables[4].Rows
                                   select new StudentProfile.CountryDropDown
                                   {
                                       CountryNo = Convert.ToInt32(dr[0].ToString()),
                                       CountryName = dr[1].ToString()
                                   }).ToList();
            }
            else { CountryDropDown = null; }
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[5].Rows.Count > 0)
            {
                VisaStatusDropDown = (from DataRow dr in ds.Tables[5].Rows
                                      select new StudentProfile.VisaStatusDropDown
                                      {
                                          VisaStatusID = Convert.ToInt32(dr[0].ToString()),
                                          VISASTATUS = dr[1].ToString()
                                      }).ToList();
            }
            else { VisaStatusDropDown = null; }
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[6].Rows.Count > 0)
            {
                VisaTypeDropDown = (from DataRow dr in ds.Tables[6].Rows
                                    select new StudentProfile.VisaTypeDropDown
                                    {
                                        VISATYPEID = Convert.ToInt32(dr[0].ToString()),
                                        VISATYPE = dr[1].ToString()
                                    }).ToList();
            }
            else { VisaTypeDropDown = null; }
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[7].Rows.Count > 0)
            {
                CitizenshipDropDown = (from DataRow dr in ds.Tables[7].Rows
                                       select new StudentProfile.CitizenshipDropDown
                                       {
                                           CitizenshipNo = Convert.ToInt32(dr[0].ToString()),
                                           CitizenshipName = dr[1].ToString()
                                       }).ToList();
            }
            else { CitizenshipDropDown = null; }

            AllDropDownBindMethod.GenderDropDown = GenderDropDown;
            AllDropDownBindMethod.OccupationDropDown = OccupationDropDown;
            AllDropDownBindMethod.RelationshipDropDown = RelationshipDropDown;
            AllDropDownBindMethod.ReligionDropDown = ReligionDropDown;
            AllDropDownBindMethod.CountryDropDown = CountryDropDown;
            AllDropDownBindMethod.VisaStatusDropDown = VisaStatusDropDown;
            AllDropDownBindMethod.VisaTypeDropDown = VisaTypeDropDown;
            AllDropDownBindMethod.CitizenshipDropDown = CitizenshipDropDown;
        }
        catch (Exception ex)
        {

        }
        return AllDropDownBindMethod;
    }
    #endregion

    #region Student Personal Details
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<StudentProfile.PersonalDetails> GetPersonalDetails(int Idnos)
    {
        StudentProfileController objConstroller = new StudentProfileController();
        List<StudentProfile.PersonalDetails> PersonalDetails = new List<StudentProfile.PersonalDetails>();
        DataSet ds = new DataSet();
        int Ua_No = Convert.ToInt32(HttpContext.Current.Session["userno"].ToString());
        try
        {
            ds = objConstroller.Get_Student_Profile_Details(Idnos, Ua_No);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                PersonalDetails = (from DataRow dr in ds.Tables[0].Rows
                                   select new StudentProfile.PersonalDetails
                                   {
                                       //Personal Details
                                       Idno = Convert.ToInt32(dr["IDNO"].ToString()),
                                       Regno = dr["REGNO"].ToString(),
                                       Student_FirstName = dr["STUDFIRSTNAME"].ToString() != string.Empty ? dr["STUDFIRSTNAME"].ToString() : string.Empty,
                                       Student_MiddleName = dr["STUDMIDDLENAME"].ToString() != "" ? dr["STUDMIDDLENAME"].ToString() : string.Empty,
                                       Student_LastName = dr["STUDLASTNAME"].ToString() != "" ? dr["STUDLASTNAME"].ToString() : string.Empty,
                                       Extension_Name = dr["EXTENSION_NAME"].ToString() != "" ? dr["EXTENSION_NAME"].ToString() : string.Empty,
                                       Gender_No = Convert.ToInt32(dr["GENDER_NO"].ToString()),
                                       //DOB = dr["DOB"].ToString() != "" ? Convert.ToDateTime(dr["DOB"].ToString()) : DateTime.MinValue,
                                       DOBString = dr["DOB"].ToString() != "" ? (dr["DOB"].ToString()) : string.Empty,
                                       Place_of_Birth = dr["PLACE_OF_BIRTH"].ToString() != "" ? dr["PLACE_OF_BIRTH"].ToString() : string.Empty,
                                       Civil_Status = dr["MARRIED"].ToString() != "" ? Convert.ToInt32(dr["MARRIED"].ToString()) : 0,
                                       Citizenship = dr["CITIZENSHIP"].ToString() != "" ? Convert.ToInt32(dr["CITIZENSHIP"].ToString()) : 0,
                                       Religion_No = dr["RELIGIONNO"].ToString() != "" ? Convert.ToInt32(dr["RELIGIONNO"].ToString()) : 0,
                                       Student_Mobile = dr["STUDENTMOBILE"].ToString() != "" ? dr["STUDENTMOBILE"].ToString() : string.Empty,
                                       Student_Email = dr["STUDENT_EMAILID"].ToString() != "" ? dr["STUDENT_EMAILID"].ToString() : string.Empty,
                                       Face_Book_Name = dr["FACE_BOOK_NAME"].ToString() != "" ? dr["FACE_BOOK_NAME"].ToString() : string.Empty,
                                       Face_Book_Link = dr["FACE_BOOK_LINK"].ToString() != "" ? dr["FACE_BOOK_LINK"].ToString() : string.Empty,
                                       Father_Name = dr["FATHERNAME"].ToString() != "" ? dr["FATHERNAME"].ToString() : string.Empty,
                                       Father_Occupation = dr["FATHER_OCCUPATION"].ToString() != "" ? Convert.ToInt32(dr["FATHER_OCCUPATION"].ToString()) : 0,
                                       Father_Mobile = dr["FATHERMOBILE"].ToString() != "" ? dr["FATHERMOBILE"].ToString() : string.Empty,
                                       Mother_Name = dr["MOTHERNAME"].ToString() != "" ? dr["MOTHERNAME"].ToString() : string.Empty,
                                       Mother_Occupation = dr["MOTHER_OCCUPATION"].ToString() != "" ? Convert.ToInt32(dr["MOTHER_OCCUPATION"].ToString()) : 0,
                                       Mother_Mobile = dr["MOTHERMOBILE"].ToString() != "" ? dr["MOTHERMOBILE"].ToString() : string.Empty,
                                       Spouse_Name = dr["Spouse_Name"].ToString() != "" ? dr["Spouse_Name"].ToString() : string.Empty,
                                       Spouse_Occupation = dr["SPOUSE_OCCUPATIONNO"].ToString() != "" ? Convert.ToInt32(dr["SPOUSE_OCCUPATIONNO"].ToString()) : 0,
                                       No_of_Children = dr["NO_OF_CHILDREN"].ToString() != "" ? Convert.ToInt32(dr["NO_OF_CHILDREN"].ToString()) : 0,
                                       Guardian_Name = dr["GUARDIANNAME"].ToString() != "" ? dr["GUARDIANNAME"].ToString() : string.Empty,
                                       Guardian_Relation = dr["RELATION_GUARDIAN"].ToString() != "" ? Convert.ToInt32(dr["RELATION_GUARDIAN"].ToString()) : 0,
                                       Guardian_Mobile = dr["GPHONE"].ToString() != "" ? dr["GPHONE"].ToString() : string.Empty,
                                       Guardian_Address = dr["GADDRESS"].ToString() != "" ? dr["GADDRESS"].ToString() : string.Empty,
                                       Number_of_Brothers = dr["NUMBER_OF_BROTHERS"].ToString() != "" ? Convert.ToInt32(dr["NUMBER_OF_BROTHERS"].ToString()) : 0,
                                       Number_of_Sisters = dr["NUMBER_OF_SISTERS"].ToString() != "" ? Convert.ToInt32(dr["NUMBER_OF_SISTERS"].ToString()) : 0,
                                       Status = dr["STATUS"].ToString() != "" ? Convert.ToInt32(dr["STATUS"].ToString()) : 0,
                                       StudentID = dr["REGNO"].ToString(),
                                       CampusName = dr["CAMPUSNAME"].ToString(),
                                       CollegeName = dr["COLLEGE_NAME"].ToString(),
                                       CourseName = dr["BRANCH_TITLE"].ToString(),
                                       CurriculumName = dr["SCHEMENAME"].ToString(),
                                       Semester = dr["SEMESTERNAME"].ToString(),
                                       StudentAge = Convert.ToInt32(dr["STUDENTAGE"].ToString()),
                                       AdvisorName = dr["UA_FULLNAME"].ToString(),

                                       //Foreign Students
                                       PassportNo = dr["PASSPORTNO"].ToString() != string.Empty ? dr["PASSPORTNO"].ToString() : string.Empty,
                                       IssuedDateString = dr["ISSUED_DATE"].ToString() != string.Empty ? dr["ISSUED_DATE"].ToString() : string.Empty,
                                       CountryNo = dr["ISSUED_COUNTRYNO"].ToString() != "" ? Convert.ToInt32(dr["ISSUED_COUNTRYNO"].ToString()) : 0,
                                       Visa_Type = dr["VISA_TYPEID"].ToString() != "" ? Convert.ToInt32(dr["VISA_TYPEID"].ToString()) : 0,
                                       Visa_Status = dr["VISA_STATUSID"].ToString() != "" ? Convert.ToInt32(dr["VISA_STATUSID"].ToString()) : 0,
                                       ICARD_NO = dr["ICARD_NO"].ToString() != string.Empty ? dr["ICARD_NO"].ToString() : string.Empty,
                                       Authorized_Stay_FromString = dr["AUTHORIZED_STAY_FROM"].ToString() != string.Empty ? dr["AUTHORIZED_STAY_FROM"].ToString() : string.Empty,
                                       Authorized_Stay_ToString = dr["AUTHORIZED_STAY_TO"].ToString() != string.Empty ? dr["AUTHORIZED_STAY_TO"].ToString() : string.Empty,
                                       Remark = dr["REMARK"].ToString() != string.Empty ? dr["REMARK"].ToString() : string.Empty,
                                       Dual_Citizenship = dr["DUAL_CITIZENSHIP"].ToString() != "" ? Convert.ToInt32(dr["DUAL_CITIZENSHIP"].ToString()) : 0,
                                       //Student Address

                                   }).ToList();
            }
        }
        catch (Exception Ex)
        {
        }
        return PersonalDetails;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static int SavePersonalDetail(List<StudentProfile.PersonalDetails> personalDetails)
    {
        int ret = 0;
        try
        {
            StudentProfileController objController = new StudentProfileController();
            List<StudentProfile.PersonalDetails> Personal_Details = new List<StudentProfile.PersonalDetails>();
            StudentProfile.PersonalDetails _personalDetails = new StudentProfile.PersonalDetails();
            string json1 = JsonConvert.SerializeObject(personalDetails);
            Personal_Details = JsonConvert.DeserializeObject<List<StudentProfile.PersonalDetails>>(json1);
            foreach (var item in Personal_Details)
            {
                _personalDetails.Idno = Convert.ToInt32(item.Idno);
                _personalDetails.Regno = item.Regno.ToString();
                _personalDetails.Student_FirstName = item.Student_FirstName.ToString() != string.Empty ? item.Student_FirstName.ToString() : string.Empty;
                _personalDetails.Student_MiddleName = item.Student_MiddleName.ToString() != "" ? item.Student_MiddleName.ToString() : string.Empty;
                _personalDetails.Student_LastName = item.Student_LastName.ToString() != "" ? item.Student_LastName.ToString() : string.Empty;
                _personalDetails.Extension_Name = item.Extension_Name.ToString() != "" ? item.Extension_Name.ToString() : string.Empty;
                _personalDetails.Gender_No = Convert.ToInt32(item.Gender_No);
                _personalDetails.DOB = item.DOB.ToString() != "" ? Convert.ToDateTime(item.DOB) : DateTime.MinValue;
                _personalDetails.Place_of_Birth = item.Place_of_Birth.ToString() != "" ? item.Place_of_Birth.ToString() : string.Empty;
                _personalDetails.Civil_Status = item.Civil_Status.ToString() != "" ? Convert.ToInt32(item.Civil_Status.ToString()) : 0;
                _personalDetails.Citizenship = item.Citizenship.ToString() != "" ? Convert.ToInt32(item.Citizenship.ToString()) : 0;
                _personalDetails.Religion_No = item.Religion_No.ToString() != "" ? Convert.ToInt32(item.Religion_No.ToString()) : 0;
                _personalDetails.Student_Mobile = item.Student_Mobile.ToString() != "" ? item.Student_Mobile.ToString() : string.Empty;
                _personalDetails.Student_Email = item.Student_Email.ToString() != "" ? item.Student_Email.ToString() : string.Empty;
                _personalDetails.Face_Book_Name = item.Face_Book_Name.ToString() != "" ? item.Face_Book_Name.ToString() : string.Empty;
                _personalDetails.Face_Book_Link = item.Face_Book_Link.ToString() != "" ? item.Face_Book_Link.ToString() : string.Empty;

                _personalDetails.Father_Name = item.Father_Name.ToString() != "" ? item.Father_Name.ToString() : string.Empty;
                _personalDetails.Father_Occupation = item.Father_Occupation.ToString() != "" ? Convert.ToInt32(item.Father_Occupation.ToString()) : 0;
                _personalDetails.Father_Mobile = item.Father_Mobile.ToString() != "" ? item.Father_Mobile.ToString() : string.Empty;
                _personalDetails.Mother_Name = item.Mother_Name.ToString() != "" ? item.Mother_Name.ToString() : string.Empty;
                _personalDetails.Mother_Occupation = item.Mother_Occupation.ToString() != "" ? Convert.ToInt32(item.Mother_Occupation.ToString()) : 0;
                _personalDetails.Mother_Mobile = item.Mother_Mobile.ToString() != "" ? item.Mother_Mobile.ToString() : string.Empty;
                _personalDetails.Spouse_Name = item.Spouse_Name.ToString() != "" ? item.Spouse_Name.ToString() : string.Empty;
                _personalDetails.Spouse_Occupation = item.Spouse_Occupation.ToString() != "" ? Convert.ToInt32(item.Spouse_Occupation.ToString()) : 0;
                _personalDetails.No_of_Children = item.No_of_Children.ToString() != "" ? Convert.ToInt32(item.No_of_Children.ToString()) : 0;
                _personalDetails.Guardian_Name = item.Guardian_Name.ToString() != "" ? item.Guardian_Name.ToString() : string.Empty;
                _personalDetails.Guardian_Relation = item.Guardian_Relation.ToString() != "" ? Convert.ToInt32(item.Guardian_Relation.ToString()) : 0;
                _personalDetails.Guardian_Mobile = item.Guardian_Mobile.ToString() != "" ? item.Guardian_Mobile.ToString() : string.Empty;
                _personalDetails.Guardian_Address = item.Guardian_Address.ToString() != "" ? item.Guardian_Address.ToString() : string.Empty;
                _personalDetails.Number_of_Brothers = item.Number_of_Brothers.ToString() != "" ? Convert.ToInt32(item.Number_of_Brothers.ToString()) : 0;
                _personalDetails.Number_of_Sisters = item.Number_of_Sisters.ToString() != "" ? Convert.ToInt32(item.Number_of_Sisters.ToString()) : 0;
                DateTime now = DateTime.Now;
                if (item.IssuedDateString.ToString() == string.Empty || item.IssuedDateString.ToString() == "")
                {
                    _personalDetails.Authorized_Stay_From = now;
                    _personalDetails.Authorized_Stay_To = now;
                    _personalDetails.IssuedDate = now;
                }
                else
                {
                    _personalDetails.IssuedDate = item.IssuedDate.ToString() != "" ? Convert.ToDateTime(item.IssuedDate) : DateTime.MinValue;
                    _personalDetails.Authorized_Stay_From = item.Authorized_Stay_From.ToString() != "" ? Convert.ToDateTime(item.Authorized_Stay_From) : DateTime.MinValue;
                    _personalDetails.Authorized_Stay_To = item.Authorized_Stay_To.ToString() != "" ? Convert.ToDateTime(item.Authorized_Stay_To) : DateTime.MinValue;
                }

                _personalDetails.PassportNo = item.PassportNo.ToString() != "" ? item.PassportNo.ToString() : string.Empty;
                _personalDetails.CountryNo = item.CountryNo.ToString() != "" ? Convert.ToInt32(item.CountryNo.ToString()) : 0;
                _personalDetails.Visa_Type = item.Visa_Type.ToString() != "" ? Convert.ToInt32(item.Visa_Type.ToString()) : 0;
                _personalDetails.Visa_Status = item.Visa_Status.ToString() != "" ? Convert.ToInt32(item.Visa_Status.ToString()) : 0;
                _personalDetails.ICARD_NO = item.ICARD_NO.ToString() != "" ? item.ICARD_NO.ToString() : string.Empty;

                _personalDetails.Remark = item.Remark.ToString() != "" ? item.Remark.ToString() : string.Empty;
                _personalDetails.Dual_Citizenship = item.Dual_Citizenship.ToString() != "" ? Convert.ToInt32(item.Dual_Citizenship.ToString()) : 0;

            }
            // Personal_Details = JsonConvert.DeserializeObject<StudentProfile.PersonalDetails>(json1);
            ret = objController.Save_Student_Personal_Details(_personalDetails);
        }
        catch (Exception ex)
        {
            throw;
        }
        return ret;
    }
    #endregion

    #region Student Address Details
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static StudentProfile.StudentAddressDetails GetAddressDetails(int Idno, int CommandType, int DropDownNo)
    {
        List<StudentProfile.AddressDetails> AddresDetail = new List<StudentProfile.AddressDetails>();
        List<StudentProfile.BindDropDown> AddressDrops = new List<StudentProfile.BindDropDown>();
        StudentProfile.StudentAddressDetails StudentAddressDetails = new StudentProfile.StudentAddressDetails();
        StudentProfileController objConstroller = new StudentProfileController();
        DataSet ds = new DataSet();
        try
        {
            ds = objConstroller.Get_Student_Address_Details(Idno, CommandType, DropDownNo);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                AddresDetail = (from DataRow dr in ds.Tables[0].Rows
                                select new StudentProfile.AddressDetails
                                {
                                    Idno = Convert.ToInt32(dr["IDNO"].ToString()),
                                    LAddress = dr["LADDRESS"].ToString(),
                                    LCountry = Convert.ToInt32(dr["LCOUNTRY"].ToString()),
                                    LState = Convert.ToInt32(dr["LSTATE"].ToString()),
                                    LCity = Convert.ToInt32(dr["LCITY"].ToString()),
                                    PAddress = (dr["PADDRESS"].ToString()),
                                    PCountry = Convert.ToInt32(dr["PCOUNTRY"].ToString()),
                                    PState = Convert.ToInt32(dr["PSTATE"].ToString()),
                                    PCity = Convert.ToInt32(dr["PCITY"].ToString()),
                                }).ToList();
            }
            else
            {
                AddresDetail = null;
            }

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
            {
                AddressDrops = (from DataRow dr in ds.Tables[1].Rows
                                select new StudentProfile.BindDropDown
                                {
                                    Id = Convert.ToInt32(dr[0].ToString()),
                                    Name = dr[1].ToString(),
                                }).ToList();
            }
            else
            {
                AddressDrops = null;
            }
            StudentAddressDetails.AddressDetails = AddresDetail;
            StudentAddressDetails.BindDropDown = AddressDrops;

        }
        catch (Exception ex)
        {
        }
        finally
        {
            // objConstroller.Dispose();
            ds.Dispose();
        }

        return StudentAddressDetails;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static int SaveStudentAddressDetails(int Idno, string Curre_Address, int Curre_Country, int Curre_Province, int Curre_City, string Perm_Address, int Perm_Country, int Perm_Province, int Perm_City)
    {
        int ret = 0;
        try
        {
            StudentProfileController objController = new StudentProfileController();
            //List<StudentProfile.AddressDetails> Address_Details = new List<StudentProfile.AddressDetails>();
            StudentProfile.AddressDetails Address_Details = new StudentProfile.AddressDetails();
            Address_Details.Idno = Idno;
            Address_Details.LAddress = Curre_Address;
            Address_Details.LCountry = Curre_Country;
            Address_Details.LState = Curre_Province;
            Address_Details.LCity = Curre_City;
            Address_Details.PAddress = Perm_Address;
            Address_Details.PCountry = Perm_Country;
            Address_Details.PState = Perm_Province;
            Address_Details.PCity = Perm_City;
            ret = objController.Save_Student_Address_Details(Address_Details);
        }
        catch (Exception ex)
        {

        }
        return ret;

    }



    #endregion 

    #region Enlisted Subjects Tab
    //-------------------------------------------------- Enlisted Subjects  Start-------------------------------------------------//
    //   Added By Roshan Patil On Date 06-09-2023  
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<StudentProfile.BindDropDown> BindEnlistedSubjectsDropDown(int Id, string Type, int Command_type)
    {
        List<StudentProfile.BindDropDown> EnlistedSubjectsList = new List<StudentProfile.BindDropDown>();
        StudentProfileController objController = new StudentProfileController();
        DataSet ds = new DataSet();
        try
        {
            ds = objController.EnlistedSubjectsList(Id, Type, Command_type);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                EnlistedSubjectsList = (from DataRow dr in ds.Tables[0].Rows
                                        select new StudentProfile.BindDropDown
                                        {
                                            Id = Convert.ToInt32(dr[0].ToString()),
                                            Name = dr[1].ToString()
                                        }).ToList();
            }
        }
        catch (Exception ex)
        {

        }
        return EnlistedSubjectsList;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<StudentProfile.EnlistedSubjects> BindEnlistedSubjectsList(int Id, string Type, int Command_type)
    {
        List<StudentProfile.EnlistedSubjects> EnlistedSubjectsList = new List<StudentProfile.EnlistedSubjects>();
        StudentProfileController objController = new StudentProfileController();
        DataSet ds = new DataSet();
        try
        {
            ds = objController.EnlistedSubjectsList(Id, Type, Command_type);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                EnlistedSubjectsList = (from DataRow dr in ds.Tables[0].Rows
                                        select new StudentProfile.EnlistedSubjects
                                        {
                                            SemesterName = dr[0].ToString(),
                                            Subject_Code = dr[1].ToString(),
                                            Subject_Name = dr[2].ToString(),
                                            Subject_Type = dr[3].ToString(),
                                            Units = Convert.ToDecimal(dr[4].ToString()),
                                            Section = dr[5].ToString(),
                                            Teacher = dr[6].ToString(),
                                            Advising_Status = dr[7].ToString()
                                        }).ToList();
            }
        }
        catch (Exception ex)
        {

        }
        return EnlistedSubjectsList;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    #endregion

    #region Student Grade and OverAllResult Start
    //-------------------------------------------------- Student Grade and OverAllResult Start-------------------------------------------------//
    //   Added By Roshan Patil On Date 08-09-2023  
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static StudentProfile.OverAllResult BindOverAllResultList(int Id, string Type, int Command_type)
    {
        List<StudentProfile.OverAllResultSession> OverAllResultSessionList = new List<StudentProfile.OverAllResultSession>();
        List<StudentProfile.OverAllResultCourses> OverAllResultCoursesList = new List<StudentProfile.OverAllResultCourses>();
        //List<StudentProfile.OverAllResult> OverAllResultList = new List<StudentProfile.OverAllResult>();
        StudentProfile.OverAllResult OverAllResultList = new StudentProfile.OverAllResult();
        StudentProfileController objController = new StudentProfileController();
        DataSet ds = new DataSet();
        try
        {
            ds = objController.EnlistedSubjectsList(Id, Type, Command_type);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    OverAllResultSessionList = (from DataRow dr in ds.Tables[0].Rows
                                                select new StudentProfile.OverAllResultSession
                                                {
                                                    SemesterName = dr[0].ToString(),
                                                    SemesterNo = Convert.ToInt32(dr[1].ToString()),
                                                    SessionName = dr[2].ToString(),
                                                    SessionNo = Convert.ToInt32(dr[3].ToString()),
                                                    Total_Subject = Convert.ToInt32(dr[4].ToString()),
                                                    GWA = dr[5].ToString().ToString(),
                                                    DateofResult = dr[6].ToString(),
                                                    GW_Status = dr[7].ToString(),
                                                }).ToList();
                }
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
                {
                    OverAllResultCoursesList = (from DataRow dr in ds.Tables[1].Rows
                                                select new StudentProfile.OverAllResultCourses
                                                {

                                                    SemesterNo = Convert.ToInt32(dr[0].ToString()),
                                                    Subject_Code = dr[1].ToString(),
                                                    Subject_Name = (dr[2].ToString()),
                                                    Subject_Type = (dr[3].ToString()),
                                                    Units =dr[4].ToString(),
                                                    FinalGrade = dr[5].ToString().ToString(),
                                                    Status = (dr[6].ToString()),
                                                    SessionNo = Convert.ToInt32(dr[7].ToString()),
                                                }).ToList();
                }

                OverAllResultList.OverAllResultSession = OverAllResultSessionList;
                OverAllResultList.OverAllResultCourses = OverAllResultCoursesList;

                return OverAllResultList;
            }
            else
            {
                return OverAllResultList;
            }
        }
        catch (Exception ex)
        {
            return OverAllResultList;
        }


    }

    

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static StudentProfile.StudentGrades BindStudentGradesList(int Idno, string SessionNo, int Command_type)
    {
        List<StudentProfile.StudentResultGrades> StudentResultGrades = new List<StudentProfile.StudentResultGrades>();
        List<StudentProfile.StudentExamGrades> StudentExamGrades = new List<StudentProfile.StudentExamGrades>();
        List<StudentProfile.StudentExamComponent> StudentExamComponent = new List<StudentProfile.StudentExamComponent>();
        StudentProfile.StudentGrades StudentGradesList = new StudentProfile.StudentGrades();
        StudentProfileController objController = new StudentProfileController();
        DataSet ds = new DataSet();
        try
        {
            ds = objController.StudentComponentGradeList(Idno, SessionNo, Command_type);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    StudentResultGrades = (from DataRow dr in ds.Tables[0].Rows
                                           select new StudentProfile.StudentResultGrades
                                           {
                                               SemesterName = dr[0].ToString(),
                                               SemesterNo = Convert.ToInt32(dr[1].ToString()),
                                               SubjectCode = dr[2].ToString(),
                                               SubjectName = dr[3].ToString(),
                                               CourseNo = Convert.ToInt32(dr[4].ToString()),
                                               SubjectType = (dr[5].ToString()),
                                               Final_Grade = dr[6].ToString(),
                                               GW_Status = dr[7].ToString(),
                                               Is_Audit = (dr[8].ToString()),

                                           }).ToList();
                }
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
                {
                    StudentExamGrades = (from DataRow dr in ds.Tables[1].Rows
                                         select new StudentProfile.StudentExamGrades
                                         {

                                             SemesterNo = Convert.ToInt32(dr[0].ToString()),
                                             CourseNo = Convert.ToInt32(dr[1].ToString()),
                                             ExamName = (dr[2].ToString()),
                                             ExamNo = Convert.ToInt32(dr[3].ToString()),
                                             Grade = (dr[4].ToString()),
                                             Total = (dr[5].ToString()),
                                             GW_Status = (dr[6].ToString()),
                                             

                                         }).ToList();
                }
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[2].Rows.Count > 0)
                {
                    StudentExamComponent = (from DataRow dr in ds.Tables[2].Rows
                                            select new StudentProfile.StudentExamComponent
                                            {

                                                SemesterNo = Convert.ToInt32(dr[0].ToString()),
                                                CourseNo = Convert.ToInt32(dr[1].ToString()),
                                                ExamName = (dr[2].ToString()),
                                                ExamNo = Convert.ToInt32(dr[3].ToString()),
                                                Asses_Component = dr[4].ToString(),
                                                ObtainedMarks = (dr[5].ToString()),
                                                GW_Status = (dr[6].ToString()),

                                            }).ToList();
                }

                StudentGradesList.StudentResultGrades = StudentResultGrades;
                StudentGradesList.StudentExamGrades = StudentExamGrades;
                StudentGradesList.StudentExamComponent = StudentExamComponent;


                return StudentGradesList;
            }
            else
            {
                return StudentGradesList;
            }
        }
        catch (Exception ex)
        {
            return StudentGradesList;
        }
    }
    /// <summary>
    /// Shahbaz Ahmad 06/01/2024
    /// Method used to check the outstanding amount of the student if amount is pending then 
    /// the grade will not be shown to the student other users(admin/faculty etc..) can see the grade if it is released
    /// </summary>
    /// <param name="idno">student idno</param>
    /// <param name="sessionno">selected session</param>
    /// <returns>message if amount is pending or not</returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string CheckOutstandingAmount(int idno, int sessionno)
    {
       // string sessionno="50";
        Common objCommon = new Common();
        string semesterno = objCommon.LookUp("ACD_STUDENT_RESULT", "DISTINCT TOP 1 isnull(SEMESTERNO,0)", "SESSIONNO=" + sessionno + " AND IDNO=" + idno + " AND ISNULL(CANCEL,0)=0");
        string SP_Name1 = "PKG_ACD_GET_OUTSTANDING_AMOUNT_STATUS_OS";
        string SP_Parameters1 = "@P_IDNO,@P_SEMESTERNO";
        string Call_Values1 = "" + Convert.ToInt32(idno) + "," + semesterno;
        string ret = "0";
        if (HttpContext.Current.Session["usertype"].ToString().Equals("2"))
        {
            DataSet que_out1 = objCommon.DynamicSPCall_Select(SP_Name1, SP_Parameters1, Call_Values1);

            if (que_out1.Tables == null && que_out1.Tables[0].Rows.Count == 0)
            {
                //this.objCommon.DisplayUserMessage(this.updBulkReg, "Your promissory request is not yet approved, please wait for approval !!", this);
                ret = "Your promissory request is not yet approved, please wait for approval !!";
                return ret;
            }
            else if (que_out1.Tables != null && que_out1.Tables[1].Rows.Count > 0)
            {
                if (que_out1.Tables[1].Rows[0]["SEMESTERNAME"].ToString() != string.Empty)
                {
                    //this.objCommon.DisplayUserMessage(this.updBulkReg, "Attention: Your Outstanding Amount is Due! Please clear your outstanding balance or Apply for Promissory Note.", this);
                    ret = "Attention: Your Outstanding Amount is Due! Please clear your outstanding balance or Apply for Promissory Note.";
                    return ret;
                }
                else
                {
                    return ret;
                }
            }
            else
            {
                return ret;
            }
        }
        else
        {
            return ret;
        }

    }



    //-------------------------------------------------- Student Grade End-------------------------------------------------//
    #endregion

    #region Student Photo and Sign
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string SavePhotoAndSign(string _photoName, string _signName, int _idno)
    {
        string retVal;
        Common objComm = new Common();
        string sp_name = "PR_UPLOAD_STUD_PHOTO_SIGN_BLOB_NAME";
        string sp_call = "" + _photoName + "," + _signName + "," + _idno + "," + 0;
        string sp_para = "@P_PHOTO,@P_SIGN,@P_IDNO,@P_OUT";
        retVal = objComm.DynamicSPCall_IUD(sp_name, sp_para, sp_call, true, 1);

        return retVal;
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GetPhotoName(int id)
    {

        Common objComm = new Common();
        string strPath = "none";
        string sp_name = "PR_GET_PHOTO_SIGN_STUD";
        string sp_call = "" + id;
        string sp_para = "@P_IDNO";
        DataSet retds = objComm.DynamicSPCall_Select(sp_name, sp_para, sp_call);
        if (retds != null || retds.Tables[0].Rows.Count > 0)
        {
            strPath = retds.Tables[0].Rows[0]["PhotosPath"].ToString();
        }
        return strPath;
    }
    #endregion

    #region Attendance Details

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<StudentProfile.AttDetailsByIDNO> GetAttDetailsSessionWise(int Idno, int SessionNo)
    {
        List<StudentProfile.AttDetailsByIDNO> objAttDetailsListByIDNO = new List<StudentProfile.AttDetailsByIDNO>();
        StudentProfileController objController = new StudentProfileController();
        DataSet ds = new DataSet();
        try
        {
            ds = objController.GetStudAttDetailsSessionWise(Idno, SessionNo);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                objAttDetailsListByIDNO = (from DataRow dr in ds.Tables[0].Rows
                                           select new StudentProfile.AttDetailsByIDNO
                                           {
                                               IDNO = Idno.ToString(),
                                               RegNo = dr["REGNO"].ToString(),
                                               RollNo = dr["ROLLNO"].ToString(),
                                               StudName = dr["STUDNAME"].ToString(),
                                               SemesterName = dr["SEMESTERNAME"].ToString(),
                                               SemesterNo = Convert.ToInt16(dr["SEMESTERNO"].ToString()),
                                               CCODE = dr["CCODE"].ToString(),
                                               CourseName = dr["COURSENAME"].ToString(),
                                               CourseNo = Convert.ToInt16(dr["COURSENO"].ToString()),
                                               SubType = dr["SUBJECT TYPE"].ToString(),
                                               SubID = dr["SUBID"].ToString(),
                                               Tot_Classes = Convert.ToInt32(dr["TOTAL CLASSES"].ToString()),
                                               Tot_Present = Convert.ToInt32(dr["TOTAL PRESENT"].ToString()),
                                               Tot_Absent = Convert.ToInt32(dr["TOTAL ABSENT"].ToString()),
                                               ATT_Percent = dr["ATTENDANCE%"].ToString(),
                                               FacultyName = dr["FACULTY NAME"].ToString(),

                                           }).ToList();
                return objAttDetailsListByIDNO;

            }
            else
                return objAttDetailsListByIDNO;
        }
        catch (Exception ex)
        {
            return objAttDetailsListByIDNO;
        }
        finally
        {
            ds.Dispose();
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<StudentProfile.AttDetailsCourseWise> GetAttDetailsSubjectWise(int Idno, int SessionNo, int CourseNo)
    {
        List<StudentProfile.AttDetailsCourseWise> objAttDetailsCourseWise = new List<StudentProfile.AttDetailsCourseWise>();
        StudentProfileController objController = new StudentProfileController();
        DataSet ds = new DataSet();
        try
        {
            ds = objController.GetStudAttDetailsSubjectWise(Idno, SessionNo, CourseNo);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                objAttDetailsCourseWise = (from DataRow dr in ds.Tables[0].Rows
                                           select new StudentProfile.AttDetailsCourseWise
                                           {
                                               IDNO = Idno.ToString(),
                                               RegNo = dr["REGNO"].ToString(),
                                              // RollNo = dr["ROLLNO"].ToString(),
                                               StudName = dr["STUDNAME"].ToString(),
                                               SemesterName = dr["SEMESTER"].ToString(),
                                               SemesterNo = Convert.ToInt16(dr["SEMESTERNO"].ToString()),
                                               CourseName = dr["LONGNAME"].ToString(),
                                               CourseNo = Convert.ToInt16(dr["COURSENO"].ToString()),
                                               ATT_Date = dr["ATT_DATE"].ToString(),
                                               ATT_Status = dr["ATT_STATUS"].ToString(),
                                               SlotNo = dr["SLOTNO"].ToString(),
                                               Period = dr["PERIOD"].ToString(),

                                           }).ToList();

                return objAttDetailsCourseWise;
            }
            else
                return objAttDetailsCourseWise;
        }
        catch (Exception ex)
        {
            return objAttDetailsCourseWise;
        }
        finally
        {
            ds.Dispose();
        }
    }
    #endregion

    #region Student Course Details Start
    //-------------------------------------------------- Student Course Details Start-------------------------------------------------//
    //   Added By Roshan Patil On Date 10-10-2023 

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static StudentProfile.StudentCourseDeatilAll BindCourseDeatilList(int Idno, int Command_type)
    {
        List<StudentProfile.StudentCourseDeatils> StudentCourseDeatils = new List<StudentProfile.StudentCourseDeatils>();
        List<StudentProfile.StudentEducationDeatils> StudentEducationDeatils = new List<StudentProfile.StudentEducationDeatils>();
        List<StudentProfile.NameOfSchoolDropDown> NameOfSchoolDropDown = new List<StudentProfile.NameOfSchoolDropDown>();
        List<StudentProfile.YearAttendedDropDown> YearAttendedDropDown = new List<StudentProfile.YearAttendedDropDown>();
        List<StudentProfile.TypeDropDown> TypeDropDown = new List<StudentProfile.TypeDropDown>();

        //List<StudentProfile.OverAllResultCourses> OverAllResultCoursesList = new List<StudentProfile.OverAllResultCourses>();
        //List<StudentProfile.OverAllResult> OverAllResultList = new List<StudentProfile.OverAllResult>();
        StudentProfile.StudentCourseDeatilAll StudentCourseDeatilAllList = new StudentProfile.StudentCourseDeatilAll();
        StudentProfileController objController = new StudentProfileController();
        DataSet ds = new DataSet();
        try
        {
            ds = objController.StudentCourseDeatilsList(Idno, Command_type);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    StudentCourseDeatils = (from DataRow dr in ds.Tables[0].Rows
                                            select new StudentProfile.StudentCourseDeatils
                                            {
                                                Intake = dr[0].ToString(),
                                                CollegeName = (dr[1].ToString()),
                                                CourseName = dr[2].ToString(),
                                                SchemeName = (dr[3].ToString()),
                                                SemesterName = (dr[4].ToString()),
                                                LearningModality = (dr[5].ToString()),
                                                Advisor = dr[6].ToString(),
                                                Mentor = dr[7].ToString(),
                                            }).ToList();
                }
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
                {
                    StudentEducationDeatils = (from DataRow dr in ds.Tables[1].Rows
                                               select new StudentProfile.StudentEducationDeatils
                                               {
                                                   QualificationLevelName = dr[0].ToString(),
                                                   QualificationLevelNo = Convert.ToInt32(dr[1].ToString()),
                                                   SchoolName = dr[2].ToString(),
                                                   SchoolNo = Convert.ToInt32(dr[3].ToString()),
                                                   YearAttended = (dr[4].ToString()),
                                                   YearAttendedNo = Convert.ToInt32(dr[5].ToString()),
                                                   TypeName = dr[6].ToString(),
                                                   TypeNo = Convert.ToInt32(dr[7].ToString()),

                                               }).ToList();
                }
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[2].Rows.Count > 0)
                {
                    NameOfSchoolDropDown = (from DataRow dr in ds.Tables[2].Rows
                                            select new StudentProfile.NameOfSchoolDropDown
                                            {
                                                SchoolNameNo = Convert.ToInt32(dr[0].ToString()),
                                                SchoolName = dr[1].ToString(),

                                            }).ToList();
                }
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[3].Rows.Count > 0)
                {
                    YearAttendedDropDown = (from DataRow dr in ds.Tables[3].Rows
                                            select new StudentProfile.YearAttendedDropDown
                                            {
                                                YearAttendedNo = Convert.ToInt32(dr[0].ToString()),
                                                YearAttended = dr[1].ToString(),

                                            }).ToList();
                }
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[4].Rows.Count > 0)
                {
                    TypeDropDown = (from DataRow dr in ds.Tables[4].Rows
                                    select new StudentProfile.TypeDropDown
                                    {
                                        TypeNo = Convert.ToInt32(dr[0].ToString()),
                                        TypeName = dr[1].ToString(),

                                    }).ToList();
                }
                StudentCourseDeatilAllList.StudentCourseDeatils = StudentCourseDeatils;
                StudentCourseDeatilAllList.StudentEducationDeatils = StudentEducationDeatils;
                StudentCourseDeatilAllList.NameOfSchoolDropDown = NameOfSchoolDropDown;
                StudentCourseDeatilAllList.YearAttendedDropDown = YearAttendedDropDown;
                StudentCourseDeatilAllList.TypeDropDown = TypeDropDown;


                return StudentCourseDeatilAllList;
            }
            else
            {
                return StudentCourseDeatilAllList;
            }
        }
        catch (Exception ex)
        {
            return StudentCourseDeatilAllList;
        }


    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static int InsertStudentEducationDetails(int Idno, string QualificationLevelNo, string NameOfSchool, string YearAttendedNo, string TypeNo)
    {
        int ret = 0;
        try
        {
            StudentProfileController objController = new StudentProfileController();
            ret = objController.InstUpdErpEducatuionDetails(Idno, QualificationLevelNo, NameOfSchool, YearAttendedNo, TypeNo);

            return ret;
        }
        catch (Exception ex)
        {
            return ret;
        }
    }
    //-------------------------------------------------- Student Course Details Start-------------------------------------------------//


    #endregion

}