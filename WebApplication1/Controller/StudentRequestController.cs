using IITMS.SQLServer.SQLDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{
    public class StudentRequestController
    {
        private string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ToString();

        public DataSet Get_Student_List_For_Subject_Request(int Idno, int Sessionno)
        {
            DataSet ds = new DataSet();
            try
            {
                SQLHelper objHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_IDNO", Idno);
                objParams[1] = new SqlParameter("@P_SESSIONNO", Sessionno);
                ds = objHelper.ExecuteDataSetSP("PKG_GET_STUDENT_DETAILS_FOR_REQUEST", objParams);
                return ds;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Get_Student_List_For_Subject_Request --> " + ex.Message + " " + ex.StackTrace);
            }
        }


        public DataSet Get_Courses_For_Subject_Request(int Idno, int Sessionno, int Semesterno, int Schemeno)
        {
            DataSet ds = new DataSet();
            try
            {
                SQLHelper objHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_IDNO", Idno);
                objParams[1] = new SqlParameter("@P_SESSIONNO", Sessionno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", Semesterno);
                objParams[3] = new SqlParameter("@P_SCHEMENO", Schemeno);
                ds = objHelper.ExecuteDataSetSP("PKG_GET_COURSES_FOR_REQUESTING", objParams);
                return ds;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Get_Courses_For_Subject_Request --> " + ex.Message + " " + ex.StackTrace);
            }
        }

        public int Save_Student_Subject_Request(DataTable dt, int AcademicSession, int Idno, int Semesterno, string IpAddress)
        {
            int ret = 0;
            try
            {
                SQLHelper objHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_COURSE_LIST", dt);
                objParams[1] = new SqlParameter("@P_ACADEMIC_SESSIONNO", AcademicSession);
                objParams[2] = new SqlParameter("@P_IDNO", Idno);
                objParams[3] = new SqlParameter("@P_SEMESTERNO", Semesterno);
                objParams[4] = new SqlParameter("@P_IPADDRESS", IpAddress);
                objParams[5] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[5].Direction = ParameterDirection.Output;
                object result = objHelper.ExecuteNonQuerySP("PKG_SAVE_STUDENT_REQUEST", objParams, true);
                if (Convert.ToInt32(result) == 1)
                {
                    ret = Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Save_Student_Subject_Request --> " + ex.Message + " " + ex.StackTrace);
            }
            return ret;
        }

        public DataSet Get_Requested_Courses(int Idno, int Sessionno, int Semesterno)
        {
            DataSet ds = new DataSet();
            try
            {
                SQLHelper objHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_IDNO", Idno);
                objParams[1] = new SqlParameter("@P_SESSIONNO", Sessionno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", Semesterno);
                ds = objHelper.ExecuteDataSetSP("PKG_GET_STUDENT_REQUESTED_COURSES", objParams);
                return ds;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Get_Courses_For_Subject_Request --> " + ex.Message + " " + ex.StackTrace);
            }
        }


        public DataSet CheckActivityStatus(int Sessionno, int Semesterno)
        {
            DataSet ds = new DataSet();
            try
            {
                SQLHelper objHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_SESSIONNO", Sessionno);
                objParams[1] = new SqlParameter("@P_SEMESTERNO", Semesterno);
                ds = objHelper.ExecuteDataSetSP("PKG_CHECK_SPECIAL_REQUEST_CONFIG", objParams);
                return ds;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Get_Courses_For_Subject_Request --> " + ex.Message + " " + ex.StackTrace);
            }
        }
    }


}
