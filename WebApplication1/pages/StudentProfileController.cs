using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.WebControls;

namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{
    public class StudentProfileController
    {
        private string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ToString();
        private StudentProfile objEntity = new StudentProfile();

        public DataSet Get_Student_Profile_Details(int Idno, int Ua_type)
        {
            DataSet ds = new DataSet();
            try
            {
                SQLHelper sqlHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_IDNO", Idno);
                objParams[1] = new SqlParameter("@P_UA_NO", Ua_type);
                ds = sqlHelper.ExecuteDataSetSP("PKG_GET_STUDENT_PROFILE_DETAILS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentProfileController.Get_Student_Profile_Details --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet Get_Student_Address_Details(int Idno, int CommandType, int DropDownNo)
        {
            DataSet ds = new DataSet();
            try
            {
                SQLHelper sqlHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_IDNO", Idno);
                objParams[1] = new SqlParameter("@P_COMMAND_TYPE", CommandType);
                objParams[2] = new SqlParameter("@P_DROP_DOWNNO", DropDownNo);
                ds = sqlHelper.ExecuteDataSetSP("PKG_GET_STUDENT_ADDRESS_DETAILS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentProfileController.Get_Student_Profile_Details --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet Populate_DropDown_List(int Id, string Type)
        {
            DataSet ds = null;
            try
            {
                SQLHelper sqlHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_TYPE", Type);
                objParams[1] = new SqlParameter("@P_ID", Id);
                ds = sqlHelper.ExecuteDataSetSP("PKG_GET_STUDENT_PROFILE_DROPDOWN_LIST", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentProfileController.Populate_DropDown_List --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public void Bind_ListView(int Idno, ListView lv)
        {
            DataSet ds = null;
            try
            {
                SQLHelper sqlHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_IDNO", Idno);
                ds = sqlHelper.ExecuteDataSetSP("PKG_GET_STUDENT_PROFILE_DETAILS", objParams);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
                {
                    lv.DataSource = ds.Tables[1];
                    lv.DataBind();
                }
                else
                {
                    lv.DataSource = null;
                    lv.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentProfileController.Populate_DropDown_List --> " + ex.Message + " " + ex.StackTrace);
            }
        }

        public int Save_Student_Personal_Details(StudentProfile.PersonalDetails objEntity)
        {
            int ret = 0;
            try
            {
                SQLHelper sqlHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[41];
                objParams[0] = new SqlParameter("@P_IDNO", objEntity.Idno);
                objParams[1] = new SqlParameter("@P_LAST_NAME", objEntity.Student_LastName);
                objParams[2] = new SqlParameter("@P_FIRST_NAME", objEntity.Student_FirstName);
                objParams[3] = new SqlParameter("@P_MIDDLE_NAME", objEntity.Student_MiddleName);
                objParams[4] = new SqlParameter("@P_EXTENSION_NAME", objEntity.Extension_Name);
                objParams[5] = new SqlParameter("@P_GENDER_NO", objEntity.Gender_No);
                objParams[6] = new SqlParameter("@P_DOB", objEntity.DOB);
                objParams[7] = new SqlParameter("@P_PLACE_OF_BIRTH", objEntity.Place_of_Birth);
                objParams[8] = new SqlParameter("@P_CIVIL_STATUS", objEntity.Civil_Status);
                objParams[9] = new SqlParameter("@P_CITIZENSHIP", objEntity.Citizenship);
                objParams[10] = new SqlParameter("@P_RELIGIONNO", objEntity.Religion_No);
                objParams[11] = new SqlParameter("@P_STUDMOBILENO", objEntity.Student_Mobile);
                objParams[12] = new SqlParameter("@P_EMAILID", objEntity.Student_Email);
                objParams[13] = new SqlParameter("@P_FACE_BOOK_NAME", objEntity.Face_Book_Name);
                objParams[14] = new SqlParameter("@P_FACE_BOOK_LINK", objEntity.Face_Book_Link);
                objParams[15] = new SqlParameter("@P_FATHER_FULL_NAME", objEntity.Father_Name);
                objParams[16] = new SqlParameter("@P_FATHER_OCCUPATION", objEntity.Father_Occupation);
                objParams[17] = new SqlParameter("@P_FATHER_MOBILENO", objEntity.Father_Mobile);
                objParams[18] = new SqlParameter("@P_MOTHER_FULL_NAME", objEntity.Mother_Name);
                objParams[19] = new SqlParameter("@P_MOTHER_OCCUPATION", objEntity.Mother_Occupation);
                objParams[20] = new SqlParameter("@P_MOTHER_MOBILENO", objEntity.Mother_Mobile);
                objParams[21] = new SqlParameter("@P_SPOUSE_NAME", objEntity.Spouse_Name);
                objParams[22] = new SqlParameter("@P_SPOUSE_OCCUPATION", objEntity.Spouse_Occupation);
                objParams[23] = new SqlParameter("@P_NO_OF_CHILDREN", objEntity.No_of_Children);
                objParams[24] = new SqlParameter("@P_NUMBER_OF_BROTHERS", objEntity.Number_of_Brothers);
                objParams[25] = new SqlParameter("@P_NUMBER_OF_SISTERS", objEntity.Number_of_Sisters);
                objParams[26] = new SqlParameter("@P_PERSON_NAME", objEntity.Guardian_Name);
                objParams[27] = new SqlParameter("@P_RELATIONSHIP", objEntity.Guardian_Relation);
                objParams[28] = new SqlParameter("@P_MOBILENO", objEntity.Guardian_Mobile);
                objParams[29] = new SqlParameter("@P_ADDRESS", objEntity.Guardian_Address);
                objParams[30] = new SqlParameter("@P_PASSPORTNO", objEntity.PassportNo);
                objParams[31] = new SqlParameter("@P_ISSUED_DATE", objEntity.IssuedDate);
                objParams[32] = new SqlParameter("@P_ISSUED_COUNTRYNO", objEntity.CountryNo);
                objParams[33] = new SqlParameter("@P_VISA_TYPE", objEntity.Visa_Type);
                objParams[34] = new SqlParameter("@P_VISA_STATUS", objEntity.Visa_Status);
                objParams[35] = new SqlParameter("@P_ICARD_NO", objEntity.ICARD_NO);
                objParams[36] = new SqlParameter("@P_AUTHORIZED_STAY_FROM", objEntity.Authorized_Stay_From);
                objParams[37] = new SqlParameter("@P_AUTHORIZED_STAY_TO", objEntity.Authorized_Stay_To);
                objParams[38] = new SqlParameter("@P_REMARK", objEntity.Remark);
                objParams[39] = new SqlParameter("@P_DUAL_CITIZENSHIP", objEntity.Dual_Citizenship);
                objParams[40] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[40].Direction = ParameterDirection.Output;
                object result = sqlHelper.ExecuteNonQuerySP("PKG_SAVE_STUDENT_PERSONAL_DETAILS", objParams, true);
                if (Convert.ToInt32(result) == 1)
                {
                    ret = 1;
                }
                else
                {
                    ret = 0;
                }
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentProfileController.Save_Student_Personal_Details --> " + ex.Message + " " + ex.StackTrace);
            }
            return ret;
        }

        public int Save_Student_Address_Details(StudentProfile.AddressDetails objEntity)
        {
            int ret = 0;
            try
            {
                SQLHelper sqlHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[11];
                objParams[0] = new SqlParameter("@P_IDNO", objEntity.Idno);
                objParams[1] = new SqlParameter("@P_CADDRESS", objEntity.LAddress);
                objParams[2] = new SqlParameter("@P_CCOUNTRY", objEntity.LCountry);
                objParams[3] = new SqlParameter("@P_CPROVINCE", objEntity.LState);
                objParams[4] = new SqlParameter("@P_CCITY", objEntity.LCity);
                objParams[5] = new SqlParameter("@P_PADDRESS", objEntity.PAddress);
                objParams[6] = new SqlParameter("@P_PCOUNTRY", objEntity.PCountry);
                objParams[7] = new SqlParameter("@P_PPROVINCE", objEntity.PState);
                objParams[8] = new SqlParameter("@P_PCITY", objEntity.PCity);
                objParams[9] = new SqlParameter("@P_UA_NO", Convert.ToInt32(HttpContext.Current.Session["userno"].ToString()));
                objParams[10] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[10].Direction = ParameterDirection.Output;
                object result = sqlHelper.ExecuteNonQuerySP("PKG_SAVE_STUDENT_ADDRESS_DETAILS", objParams, true);
                if (Convert.ToInt32(result) == 1)
                {
                    ret = 1;
                }
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentProfileController.Save_Student_Address_Details --> " + ex.Message + " " + ex.StackTrace);
            }
            return ret;
        }

        public int Save_Student_Education_Details(StudentProfile.EducationDetails objEntity)
        {
            int ret = 0;
            try
            {
                SQLHelper sqlHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[10];
                objParams[0] = new SqlParameter("@P_IDNO", objEntity.Idno);
                objParams[1] = new SqlParameter("@P_CLASSIFIED", objEntity.Classified);
                objParams[2] = new SqlParameter("@P_COURSE", objEntity.Course);
                objParams[3] = new SqlParameter("@P_PREFFERED_MODALITY", objEntity.PrefferedModality);
                objParams[4] = new SqlParameter("@P_LEVELNO", objEntity.LevelNo);
                objParams[5] = new SqlParameter("@P_SCHOOLNO", objEntity.SchoolNo);
                objParams[6] = new SqlParameter("@P_REGIONNO", objEntity.Region);
                objParams[7] = new SqlParameter("@P_YEAR_ATTEND", objEntity.YearAttend);
                objParams[8] = new SqlParameter("@P_TYPE", objEntity.Type);
                objParams[9] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[9].Direction = ParameterDirection.Output;
                object result = sqlHelper.ExecuteNonQuerySP("PKG_SAVE_STUDENT_EDUCATION_DETAILS", objParams, true);
                if (Convert.ToInt32(result) == 1)
                {
                    ret = 1;
                }
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentProfileController.Save_Student_Address_Details --> " + ex.Message + " " + ex.StackTrace);
            }
            return ret;
        }

        public int Save_Student_Photo_Signature(StudentProfile.PhotoSignature objEntity)
        {
            int ret = 0;
            try
            {
                SQLHelper sqlHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_IDNO", objEntity.Idno);
                objParams[1] = new SqlParameter("@P_PHOTO", objEntity.Photo);
                objParams[2] = new SqlParameter("@P_SIGNATURE", objEntity.Signature);
                objParams[3] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[3].Direction = ParameterDirection.Output;
                object result = sqlHelper.ExecuteNonQuerySP("PKG_SAVE_STUDENT_PHOTO_SIGNATURE", objParams, true);
                if (Convert.ToInt32(result) == 1 || Convert.ToInt32(result) == 2)
                {
                    ret = Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentProfileController.Save_Student_Photo_Signature --> " + ex.Message + " " + ex.StackTrace);
            }
            return ret;
        }

        //Added By Roshan Patil
        public DataSet EnlistedSubjectsList(int Id, string Type, int Command_type)
        {
            DataSet ds = null;
            try
            {
                SQLHelper sqlHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_TYPE", Type);
                objParams[1] = new SqlParameter("@P_ID", Id);
                objParams[2] = new SqlParameter("@P_COMMAND_TYPE", Command_type);
                ds = sqlHelper.ExecuteDataSetSP("PKG_GET_STUDENT_PROFILE_ENLISTED_SUBJECTS_LIST", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentProfileController.Populate_DropDown_List --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        //Added By Roshan Patil
        public DataSet StudentComponentGradeList(int Idno, string SessionNo, int Command_type)
        {
            DataSet ds = null;
            try
            {
                SQLHelper sqlHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_SESSIONNO", SessionNo);
                objParams[1] = new SqlParameter("@P_IDNO", Idno);
                objParams[2] = new SqlParameter("@P_COMMAND_TYPE", Command_type);
                ds = sqlHelper.ExecuteDataSetSP("PKG_GET_STUDENT_PROFILE_COMPONENT_GRADE_LIST", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentProfileController.Populate_DropDown_List --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        #region Attendance Details

        public DataSet GetStudAttDetailsSessionWise(int Idno, int SessionNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper sqlHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_SESSIONNO", SessionNo);
                objParams[1] = new SqlParameter("@P_IDNO", Idno);
                ds = sqlHelper.ExecuteDataSetSP("PKG_SP_GET_ATTENDANCE_DETAILS_BY_IDNO", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentProfileController.GetStudAttDetailsSessionWise --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetStudAttDetailsSubjectWise(int Idno, int SessionNo, int CourseNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper sqlHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_SESSIONNO", SessionNo);
                objParams[1] = new SqlParameter("@P_IDNO", Idno);
                objParams[2] = new SqlParameter("@P_COURSENO", CourseNo);
                ds = sqlHelper.ExecuteDataSetSP("PKG_ACD_GET_REPORT_STUDENT_ATTENDANCE", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentProfileController.GetStudAttDetailsSubjectWise --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        #endregion Attendance Details

        #region Course Deatil

        //Added By Roshan Patil
        public DataSet StudentCourseDeatilsList(int Idno, int Command_type)
        {
            DataSet ds = null;
            try
            {
                SQLHelper sqlHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_IDNO", Idno);
                objParams[1] = new SqlParameter("@P_COMMAND_TYPE", Command_type);
                ds = sqlHelper.ExecuteDataSetSP("PKG_GET_STUDENT_COURSE_DETAILS_AND_EDUCATION_DETAILS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentProfileController.StudentCourseDeatilsList --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public int InstUpdErpEducatuionDetails(int Idno, string levelno, string SchoolNameNo, string YearAttended, string TypeNo)
        {
            SQLHelper objSQLHelper = new SQLHelper(_connectionString);
            string message = "";
            try
            {
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_IDNO", Idno);
                objParams[1] = new SqlParameter("@P_LEVELNO", levelno);
                objParams[2] = new SqlParameter("@P_SCHOOLNAMENO", SchoolNameNo);
                objParams[3] = new SqlParameter("@P_YEARATTENDED", YearAttended);
                objParams[4] = new SqlParameter("@P_TYPENO", TypeNo);
                objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[5].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PR_ACD_INSTUPD_ERP_EDUCATUION_DETAILS", objParams, true);
                message = ret.ToString();
                if (message == "1")
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SubjectTaggingController.InstUpdSubjectTagging-> " + ex.ToString());
            }
        }

        #endregion Course Deatil
    }
}