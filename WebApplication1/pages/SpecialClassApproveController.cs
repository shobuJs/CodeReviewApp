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
    public class SpecialClassApproveController
    {
        private string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ToString();

        public DataSet Get_DropDown_List(string Type, int CollegeId, int DegreeNO, int BranchNO)
        {
            DataSet ds = new DataSet();
            try
            {
                SQLHelper objHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_TYPE", Type);
                objParams[1] = new SqlParameter("@P_COLLEGE_ID", CollegeId);
                objParams[2] = new SqlParameter("@P_DEGREENO", DegreeNO);
                objParams[3] = new SqlParameter("@P_BRANCHNO", BranchNO);
                ds = objHelper.ExecuteDataSetSP("PKG_GET_DROPDOWN_SPECIAL_CLASS_APPROVE", objParams);
                return ds;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Get_Student_List_For_Subject_Request --> " + ex.Message + " " + ex.StackTrace);
            }
        }

        public DataSet Get_Course_List_For_Special_Class(int Sessionno, int College_Id, string DegreeNo, string BranchNo, string Curriculum, string Semester)
        {
            DataSet ds = new DataSet();
            try
            {
                SQLHelper objHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_SESSIONNO", Sessionno);
                objParams[1] = new SqlParameter("@P_COLLEGE_ID", College_Id);
                //objParams[2] = new SqlParameter("@P_PROGRAM", Program);
                objParams[2] = new SqlParameter("@P_DEGREENO", DegreeNo);
                objParams[3] = new SqlParameter("@P_BRANCHNO", BranchNo);
                objParams[4] = new SqlParameter("@P_CURRICULUM", Curriculum);
                objParams[5] = new SqlParameter("@P_SEMESTER", Semester);
                ds = objHelper.ExecuteDataSetSP("PKG_GET_SUBJECTS_FOR_SPECIAL_CLASS_APPROVAL", objParams);
                return ds;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Get_Student_List_For_Subject_Request --> " + ex.Message + " " + ex.StackTrace);
            }
        }

        //public DataSet Save_Specail_Class_Approve(int Sessionno, string CollegeId, DataTable dt_Courses, string IpAddress, int Uano, string IsApproved)
        //{
        //    DataSet ds = new DataSet();
        //    try
        //    {
        //        SQLHelper objHelper = new SQLHelper(_connectionString);
        //        SqlParameter[] objParams = new SqlParameter[6];
        //        objParams[0] = new SqlParameter("@P_SESSIONNO", Sessionno);
        //        objParams[1] = new SqlParameter("@P_COURSE_LIST", dt_Courses);
        //        objParams[2] = new SqlParameter("@P_COLLEGEID", CollegeId);
        //        objParams[3] = new SqlParameter("@P_IPADDRESS", IpAddress);
        //        objParams[4] = new SqlParameter("@P_UANO", Uano);
        //        objParams[5] = new SqlParameter("@P_ISAPPROVED", IsApproved);
        //        ds = objHelper.ExecuteDataSetSP("PKG_SAVE_SPECIAL_CLASS_APPROVAL", objParams);
        //        return ds;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Get_Student_List_For_Subject_Request --> " + ex.Message + " " + ex.StackTrace);
        //    }
        //}
        // Added by Tanmay Sahare Date 21-12-2023//
        public DataSet Save_Specail_Class_Approve(DataTable dt_Courses, string IpAddress, int Uano)
        {
            DataSet ds = new DataSet();
            try
            {
                SQLHelper objHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_COURSE_LIST", dt_Courses);
                objParams[1] = new SqlParameter("@P_IPADDRESS", IpAddress);
                objParams[2] = new SqlParameter("@P_UANO", Uano);
                ds = objHelper.ExecuteDataSetSP("PKG_SAVE_SPECIAL_CLASS_APPROVAL", objParams);
                return ds;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Get_Student_List_For_Subject_Request --> " + ex.Message + " " + ex.StackTrace);
            }
        }
        // End Date 21-12-2023//

        //public DataSet Get_Requested_Student_List(int Sessionno, int CourseNo)
        //{
        //    DataSet ds = new DataSet();
        //    try
        //    {
        //        SQLHelper objHelper = new SQLHelper(_connectionString);
        //        SqlParameter[] objParams = new SqlParameter[2];
        //        objParams[0] = new SqlParameter("@P_SESSIONNO", Sessionno);
        //        objParams[1] = new SqlParameter("@P_COURSENO", CourseNo);
        //        ds = objHelper.ExecuteDataSetSP("PKG_GET_REQUESTED_STUDENT_LIST", objParams);
        //        return ds;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Get_Requested_Student_List --> " + ex.Message + " " + ex.StackTrace);
        //    }
        //}
        //Added By Tanmay Sahare Date 29-12-2023//
       public DataSet Get_Requested_Student_List(int Sessionno, int CourseNo, int IsApproved)
        {
            DataSet ds = new DataSet();
            try
            {
                SQLHelper objHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_SESSIONNO", Sessionno);
                objParams[1] = new SqlParameter("@P_COURSENO", CourseNo);
                objParams[2] = new SqlParameter("@P_IsApproved", IsApproved);
                ds = objHelper.ExecuteDataSetSP("PKG_GET_REQUESTED_STUDENT_LIST", objParams);
                return ds;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Get_Requested_Student_List --> " + ex.Message + " " + ex.StackTrace);
            }
        }
            //End//
        }
    }

