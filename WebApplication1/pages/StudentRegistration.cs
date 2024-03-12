using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System;
using System.Data;
using System.Data.SqlClient;

namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{
    public class StudentRegistration
    {
        private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        #region CourseRegistration

        //Added By Raju - 10052019
        public int AddRegisteredSubjects_Stud(StudentRegist objSR, string module_type, int amount, int latefee)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO),
                            new SqlParameter("@P_IDNO", objSR.IDNO),
                            new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO),
                            new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO),
                            new SqlParameter("@P_COURSENOS", objSR.COURSENOS),
                            new SqlParameter("@P_BACK_COURSENOS", objSR.Backlog_course),
                            new SqlParameter("@P_AUDIT_COURSENOS", objSR.Audit_course),
                            new SqlParameter("@P_REGNO", objSR.REGNO),
                            new SqlParameter("@P_ROLLNO", objSR.ROLLNO),

                            new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS),
                            new SqlParameter("@P_UA_NO", objSR.UA_NO),
                            new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE),
                            new SqlParameter("@P_REGISTERED", objSR.EXAM_REGISTERED),
                            new SqlParameter("@P_MODULE_REG_TYPE", module_type),
                            new SqlParameter("@P_AMOUNT", amount),
                            new SqlParameter("@P_TOTALFEE", latefee),
                            new SqlParameter("@P_OUT", SqlDbType.Int)
                        };
                objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PREREGIST_SP_INS_REGIST_SUBJECTS_STUD", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddAddlRegisteredSubjects-> " + ex.ToString());
            }

            return retStatus;
        }

        //Added By Raju - 10052019
        public DataSet GetCourseRegStudentList_Stud(int idno, int utype, int userDec, int ua_no, int sessionno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                SqlParameter[] objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_TYPE", utype);
                objParams[2] = new SqlParameter("@P_DEC", userDec);
                objParams[3] = new SqlParameter("@P_UA_NO", ua_no);
                objParams[4] = new SqlParameter("@P_SESSIONNO", sessionno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_PREREGIST_SP_RET_STUDENTS_COURSE_REG", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistration.GetStudentList-> " + ex.ToString());
            }

            return ds;
        }

        public DataSet getCreditCountOfScheme(int branch, int schemeno, int semesterno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                SqlParameter[] objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_BRANCHNO", branch);

                objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_RPT_OFFERED_COURSES_COUNT", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetStudentList-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetCurrentCourseForReg(int idno, int semno, int sessionno, int schemeno, int user_type)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SEMESTERNO", semno);
                objParams[2] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[3] = new SqlParameter("@P_SCHEMENO", schemeno);

                objParams[4] = new SqlParameter("@P_USER_TYPE", user_type);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_CURRENT_OFFERED_COURSE_FOR_REG", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetStudentList-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet getBacklogCourses(int idno, int sessionno, int schemeno, int semesterno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_IDNO", idno);

                objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[2] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterno);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_BACKLOG_COURSES", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetStudentList-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet getOfferedBacklogCourses(int idno, int sessionno, int schemeno, int semesterno, int userType)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                SqlParameter[] objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_IDNO", idno);

                objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[2] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[3] = new SqlParameter("@P_USER_TYPE", userType);
                objParams[4] = new SqlParameter("@P_SEMSTERNO", semesterno);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_OFFERED_BACKLOG_COURSES", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetStudentList-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet getOfferedDetainCourses(int idno, int sessionno, int schemeno, int semesterno, int user_type)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                SqlParameter[] objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_IDNO", idno);

                objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[2] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[3] = new SqlParameter("@P_USER_TYPE", user_type);
                objParams[4] = new SqlParameter("@P_SEMESTERNO", semesterno);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_OFFERED_DETAIN_COURSES", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetStudentList-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet getDetainCourses(int idno, int sessionno, int schemeno, int SEMESTERNO)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_IDNO", idno);

                objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[2] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[3] = new SqlParameter("@P_SEMESTERNO", SEMESTERNO);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_DETAIN_COURSES", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetStudentList-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetGPCourseForReg(int idno, int semno, int sessionno, int schemeno, int user_type)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                SqlParameter[] objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SEMESTERNO", semno);
                objParams[2] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[3] = new SqlParameter("@P_SCHEMENO", schemeno);

                objParams[4] = new SqlParameter("@P_USER_TYPE", user_type);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_GP_OFFERED_COURSE_FOR_REG", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetStudentList-> " + ex.ToString());
            }

            return ds;
        }

        public DataSet GetAdditionalCoursesForReg(int idno, int term, int sessionno, int branchno, int flag, int userType, int courseNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                SqlParameter[] objParams = new SqlParameter[7];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_TERM", term);
                objParams[2] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[3] = new SqlParameter("@P_BRANCHNO", branchno);
                objParams[4] = new SqlParameter("@P_FLAG", flag);
                objParams[5] = new SqlParameter("@P_USER_TYPE", userType);
                objParams[6] = new SqlParameter("@P_COURSENO", courseNo);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ADDITIONAL_OFFERED_COURSE_FOR_REG", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetStudentList-> " + ex.ToString());
            }

            return ds;
        }

        #region All Elective Groups

        public DataSet GetElectiveCourseForReg(int idno, int semno, int sessionno, int schemeno, int user_type)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                SqlParameter[] objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SEMESTERNO", semno);
                objParams[2] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[3] = new SqlParameter("@P_SCHEMENO", schemeno);
                // objParams[4] = new SqlParameter("@P_FLAG", flag);
                objParams[4] = new SqlParameter("@P_USER_TYPE", user_type);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ELECTIVE_OFFERED_COURSE_FOR_REG", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetStudentList-> " + ex.ToString());
            }

            return ds;
        }

        //[ Elective Group-2 Courses]
        public DataSet GetElectiveGrp2CourseForReg(int idno, int semno, int sessionno, int schemeno, int user_type)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SEMESTERNO", semno);
                objParams[2] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[3] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[4] = new SqlParameter("@P_USER_TYPE", user_type);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ELECTIVE_OFFERED_COURSE_FOR_REG_Grp2", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetStudentList-> " + ex.ToString());
            }

            return ds;
        }

        //[ Elective Group-3 Courses]
        public DataSet GetElectiveGrp3CourseForReg(int idno, int semno, int sessionno, int schemeno, int user_type)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SEMESTERNO", semno);
                objParams[2] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[3] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[4] = new SqlParameter("@P_USER_TYPE", user_type);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ELECTIVE_OFFERED_COURSE_FOR_REG_Grp3", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetStudentList-> " + ex.ToString());
            }

            return ds;
        }

        //[ Elective Group-4 Courses]
        public DataSet GetElectiveGrp4CourseForReg(int idno, int semno, int sessionno, int schemeno, int user_type)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                SqlParameter[] objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SEMESTERNO", semno);
                objParams[2] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[3] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[4] = new SqlParameter("@P_USER_TYPE", user_type);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ELECTIVE_OFFERED_COURSE_FOR_REG_Grp4", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetStudentList-> " + ex.ToString());
            }

            return ds;
        }

        public DataSet GetMovableCourseForReg(int idno, int semno, int sessionno, int schemeno, int user_type)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                SqlParameter[] objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SEMESTERNO", semno);
                objParams[2] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[3] = new SqlParameter("@P_SCHEMENO", schemeno);
                // objParams[4] = new SqlParameter("@P_FLAG", flag);
                objParams[4] = new SqlParameter("@P_USER_TYPE", user_type);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_MOVABLE_OFFERED_COURSE_FOR_REG", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetStudentList-> " + ex.ToString());
            }

            return ds;
        }

        #endregion All Elective Groups

        public DataSet GetOpenElectCourseForReg(int idno, int semno, int sessionno, int schemeno, int user_type)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                SqlParameter[] objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SEMESTERNO", semno);
                objParams[2] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[3] = new SqlParameter("@P_SCHEMENO", schemeno);

                objParams[4] = new SqlParameter("@P_USER_TYPE", user_type);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_OPEN_ELECT_OFFERED_COURSE_FOR_REG", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetStudentList-> " + ex.ToString());
            }

            return ds;
        }

        public DataSet GetPrerequisiteHistory(int idno, int semno, int sessionno, int schemeno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SEMESTERNO", semno);
                objParams[2] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[3] = new SqlParameter("@P_SCHEMENO", schemeno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_PRE_REQUISITE_HISTORY_OF_OFFERED_COURSE_FOR_REG", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetStudentList-> " + ex.ToString());
            }

            return ds;
        }

        public int checkEligibilityForSemPromotion(StudentRegist objSR)
        {
            object ret;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[1] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[2].Direction = ParameterDirection.Output;
                ret = objSQLHelper.ExecuteNonQuerySP("PKG_CHECK_ELIGIBILITY_FOR_SEM_PROMOTION", objParams, true);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddAddlRegisteredSubjects-> " + ex.ToString());
            }

            return Convert.ToInt32(ret);
        }

        public int AddDropTermRecord(StudentRegist objSR, int degreeno)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[7];
                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_REGNO", objSR.REGNO);
                objParams[2] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[3] = new SqlParameter("@P_DEGREENO", degreeno);
                objParams[4] = new SqlParameter("@P_BRANCHNO", objSR.SCHEMENO);
                objParams[5] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[6].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_TERM_DROP_STUDENT", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddDropTermRecord-> " + ex.ToString());
            }
            return retStatus;
        }

        public DataSet getTimeTable(int sessionno, string courseno, string sectionno, string batchnos)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                SqlParameter[] objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_COURSENOS", courseno);
                objParams[2] = new SqlParameter("@P_SECTIONNO", sectionno);
                objParams[3] = new SqlParameter("@P_BATCHNOS", batchnos);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_TIMETABLE_COURSEWISE", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetStudentList-> " + ex.ToString());
            }
            return ds;
        }

        #endregion CourseRegistration

        /// <summary>
        ///
        /// </summary>
        /// <param name="idno"></param>
        /// <param name="utype"></param>
        /// <param name="userDec"></param>
        /// <param name="ua_no"></param>
        /// <returns></returns>
        public DataSet GetStudentList(int idno, int utype, int userDec, int ua_no)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                SqlParameter[] objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_TYPE", utype);
                objParams[2] = new SqlParameter("@P_DEC", userDec);
                objParams[3] = new SqlParameter("@P_UA_NO", ua_no);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_PREREGIST_SP_RET_STUDENTS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetStudentList-> " + ex.ToString());
            }

            return ds;
        }

        public int InsertStudentRegistrationByAdmin(StudentRegist objSR)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO),
                            new SqlParameter("@P_IDNO", objSR.IDNO),
                            new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO),
                            new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO),
                            new SqlParameter("@P_COURSENOS", objSR.COURSENOS),
                            new SqlParameter("@P_BACK_COURSENOS", objSR.Backlog_course),
                            new SqlParameter("@P_CREG_IDNO", objSR.UA_NO),
                            new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS),
                            new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE),
                            new SqlParameter("@P_REGNO", objSR.REGNO),
                            new SqlParameter("@P_ROLLNO", objSR.ROLLNO),
                            new SqlParameter("@P_OUT", SqlDbType.Int)
                        };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PREREGIST_SP_INS_STUDENT_REGISTRATION_BY_ADMIN", sqlParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistration.UdpateRegistrationByHOD-> " + ex.ToString());
            }
            return retStatus;
        }

        public int InsertStudentElective(StudentRegist objSR)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO),
                            new SqlParameter("@P_IDNO", objSR.IDNO),
                            new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO),
                            new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO),
                            new SqlParameter("@P_COURSENOS", objSR.COURSENOS),
                            new SqlParameter("@P_BACK_COURSENOS", objSR.Backlog_course),
                            new SqlParameter("@P_CREG_IDNO", objSR.UA_NO),
                            new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS),
                            new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE),
                            new SqlParameter("@P_REGNO", objSR.REGNO),
                            new SqlParameter("@P_ROLLNO", objSR.ROLLNO),
                            new SqlParameter("@P_OUT", SqlDbType.Int)
                        };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("ACD_INSERT_ELECTIVE_MODULE", sqlParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistration.UdpateRegistrationByHOD-> " + ex.ToString());
            }
            return retStatus;
        }

        public DataSet GetStud_SVETStud_Fetch(int session, int college, int degree, int branch, int semester)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                SqlParameter[] objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                objParams[1] = new SqlParameter("@P_COLLEGEID", college);
                objParams[2] = new SqlParameter("@P_DEGREENO", degree);
                objParams[3] = new SqlParameter("@P_BRANCHNO", branch);
                //objParams[4] = new SqlParameter("@P_SCHEMENO", scheme);
                objParams[4] = new SqlParameter("@P_SEMESTERNO", semester);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_FETCH_SVETADM_STUDENT", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetStud_CourseReg_ChangeSemester_Bulk-> " + ex.ToString());
            }
            return ds;
        }

        public int AddUpdateRevalPhotoCopyChallenegeRegisteration(StudentRegist objSR, int Status, int operation, string RECHECKORREASS, int checkreadpaper)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New eXAM Registered Subject Details
                objParams = new SqlParameter[15];
                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[2] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[3] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[4] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[5] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                objParams[7] = new SqlParameter("@P_EXREGIDNO", objSR.UA_NO);
                objParams[8] = new SqlParameter("@P_EXTERMARK", objSR.EXTERMARKS);
                objParams[9] = new SqlParameter("@P_CCODE", objSR.CCODES);
                objParams[10] = new SqlParameter("@P_APPROVE_FLAG", Status);
                objParams[11] = new SqlParameter("@P_OPERATION_FLAG", operation);
                objParams[12] = new SqlParameter("@P_RECHECKORREASS", RECHECKORREASS);
                objParams[13] = new SqlParameter("@P_CHECKSTATUS", checkreadpaper);
                objParams[14] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[14].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REGIST_SP_INS_EXAM_COURSE_FOR_REVALUATION", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(ret);
                else
                    retStatus = Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
            }
            return retStatus;
        }

        public int AddUpdatedropcourses(StudentRegist objSR, string reason)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New eXAM Registered Subject Details
                objParams = new SqlParameter[9];
                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[2] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[3] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[4] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                objParams[5] = new SqlParameter("@P_EXREGIDNO", objSR.UA_NO);
                objParams[6] = new SqlParameter("@P_CCODE", objSR.CCODES);
                objParams[7] = new SqlParameter("@P_REASON", reason);
                objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[8].Direction = ParameterDirection.Output;
                //object ret = objSQLHelper.ExecuteNonQuerySP("PKG_DROUP_COURSES", objParams, true);
                object ret = objSQL.ExecuteNonQuerySP("PKG_DROUP_COURSES", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(ret);
                else
                    retStatus = Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
            }
            return retStatus;
        }

        public int AddUpdatedropcoursesstatus(StudentRegist objSR, int courseno, string ccode, int userno, string remark)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New eXAM Registered Subject Details
                objParams = new SqlParameter[10];
                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[2] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[3] = new SqlParameter("@P_COURSENO", courseno);
                objParams[4] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                objParams[5] = new SqlParameter("@P_EXREGIDNO", objSR.UA_NO);
                objParams[6] = new SqlParameter("@P_CCODE", ccode);
                objParams[7] = new SqlParameter("@P_USERNO", userno);
                objParams[8] = new SqlParameter("@P_REMARK", remark);
                objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[9].Direction = ParameterDirection.Output;
                //object ret = objSQLHelper.ExecuteNonQuerySP("PKG_DROUP_COURSES", objParams, true);
                object ret = objSQL.ExecuteNonQuerySP("PKG_UPDATE_DROUP_COURSES_STATUS", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(ret);
                else
                    retStatus = Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
            }
            return retStatus;
        }

        public int AddUpdatewithdrawcoursesstatus(StudentRegist objSR, int courseno, string ccode, int userno, string remark)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New eXAM Registered Subject Details
                objParams = new SqlParameter[10];
                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[2] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[3] = new SqlParameter("@P_COURSENO", courseno);
                objParams[4] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                objParams[5] = new SqlParameter("@P_EXREGIDNO", objSR.UA_NO);
                objParams[6] = new SqlParameter("@P_CCODE", ccode);
                objParams[7] = new SqlParameter("@P_USERNO", userno);
                objParams[8] = new SqlParameter("@P_REMARK", remark);
                objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[9].Direction = ParameterDirection.Output;
                //object ret = objSQLHelper.ExecuteNonQuerySP("PKG_DROUP_COURSES", objParams, true);
                object ret = objSQL.ExecuteNonQuerySP("PKG_UPDATE_WITHDRAW_COURSES_STATUS", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(ret);
                else
                    retStatus = Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
            }
            return retStatus;
        }

        public DataSet GetDropCourseList(int idno, int semesterno, int sessionno)
        {
            DataSet ds = null;

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_COURSES_DROP_status", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetails->" + ex.ToString());
            }
            return ds;
        }

        public DataSet GetWITHDRAWCourseList(int idno, int semesterno, int sessionno)
        {
            DataSet ds = null;

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_COURSES_withdraw_status", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetails->" + ex.ToString());
            }
            return ds;
        }

        public DataSet GetStudInfoForChangeCoreSubject(int idno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                ds = objSQL.ExecuteDataSetSP("PKG_SHOW_STUDENT_INFO", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CoreSubjectChange.GetNewSchemesForRegistration-> " + ex.ToString());
            }
            return ds;
        }

        public int GetChangeCoreSubject(int idno, int core_SubID, int Uano)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_CORE_SUBID", core_SubID);
                objParams[2] = new SqlParameter("@P_UANO", Uano);
                objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[3].Direction = ParameterDirection.Output;
                object ret = objSQL.ExecuteNonQuerySP("PKG_ACD_CHANGE_CORE_SUBJECT", objParams, true);

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ChangeCoreSubject.GetChangeCoreSubject-> " + ex.ToString());
            }
            return retStatus;
        }

        public int InsertStudentRegistrationForUG(StudentRegist objSR, string ddlvalue)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO),
                            new SqlParameter("@P_IDNO", objSR.IDNO),
                            new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO),
                            new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO),
                            new SqlParameter("@P_COURSENOS", objSR.COURSENOS),
                            new SqlParameter("@P_BACK_COURSENOS", objSR.Backlog_course),
                            new SqlParameter("@P_CREG_IDNO", objSR.UA_NO),
                            new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS),
                            new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE),
                            new SqlParameter("@P_REGNO", objSR.REGNO),
                            new SqlParameter("@P_ROLLNO", objSR.ROLLNO),
                            new SqlParameter("@P_SUB_TYPE",ddlvalue),
                            new SqlParameter("@P_OUT", SqlDbType.Int)
                        };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PREREGIST_SP_INS_STUDENT_REGISTRATION_FOR_UG", sqlParams, true);

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistration.UdpateRegistrationByHOD-> " + ex.ToString());
            }
            return retStatus;
        }

        public int InsertStudentDetails(StudentRegist objSR)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_STUDNAME", objSR.STUDNAME),
                            new SqlParameter("@P_ROLLNO", objSR.ROLLNO),
                            new SqlParameter("@P_REGNO", objSR.REGNO),
                            new SqlParameter("@P_ADMBATCH", objSR.BATCHNO),
                            new SqlParameter("@P_COLLEGEID", objSR.COLLEGEID),
                            new SqlParameter("@P_DEGREENO", objSR.DEGREENO),
                            new SqlParameter("@P_BRANCHNO", objSR.BRANCHNO),
                            new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO),
                            new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO),
                           // new SqlParameter("@P_CSUBID", objSR.CORSUBID),
                            new SqlParameter("@P_OUT", SqlDbType.Int)
                        };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INS_STUDENT_REGISTRATION_DETAILS", sqlParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistration.InsertStudentDetails-> " + ex.ToString());
            }
            return retStatus;
        }

        public DataSet GetStudInfoForCourseRegi(int idno, int sessionno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                ds = objSQL.ExecuteDataSetSP("PKG_SHOW_STUDENT_INFO_FOR_STUD_REGI", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetNewSchemesForRegistration-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetStudentCoursesForBacklogRegistration(int sessionNo, int idno, int semesterno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
                objParams[1] = new SqlParameter("@P_IDNO", idno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);

                ds = objSQL.ExecuteDataSetSP("PR_ACD_GETFAIL_SUBJECT_FOR_BACKLOG_REGIST", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetAllCoursesForRegistration-> " + ex.ToString());
            }
            return ds;
        }

        public bool CheckStudentForFaculty(int idno, int ua_no)
        {
            bool ret = false;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_UA_NO", ua_no);

                object rt = objSQLHelper.ExecuteScalarSP("PKG_STUDENT_SP_CHECK_STUD_FOR_FAC", objParams);
                if (rt == null || rt == "")
                    ret = false;
                else
                    ret = true;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.CheckStudentForFaculty-> " + ex.ToString());
            }

            return ret;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idno"></param>
        /// <param name="sessionno"></param>
        /// <param name="schemeno"></param>
        /// <param name="sectionno"></param>
        /// <param name="flagStatus"></param>
        /// <returns></returns>
        public DataTable GetSubjectDetailsById(int idno, int sessionno, int schemeno, int sectionno, int flagStatus)
        {
            DataTable dt = null;
            try
            {
                SQLHelper objSH = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[2] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                objParams[4] = new SqlParameter("@P_FLAG", flagStatus);

                dt = objSH.ExecuteDataSetSP("PKG_PREREGIST_SP_RET_SUBJECTDETAILS", objParams).Tables[0];
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetSubjectDetailsById-> " + ex.ToString());
            }
            return dt;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public DataTableReader GetStudentDetails(int idno)
        {
            DataTableReader dtr = null;
            try
            {
                SQLHelper objSH = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                dtr = objSH.ExecuteDataSetSP("PKG_PREREGIST_SP_RET_STUDDETAILS", objParams).Tables[0].CreateDataReader();
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetStudentDetails-> " + ex.ToString());
            }
            return dtr;
        }

        public DataSet GetRegistTotalStudents(int sessionno, int schemeno, int rdbtn, int regstatus)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                SqlParameter[] objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[2] = new SqlParameter("@P_RD", rdbtn);
                objParams[3] = new SqlParameter("@P_REGSTATUS", regstatus);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_PREREGIST_SP_REPORT_COURSE_STATS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetRegistTotalStudents-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetCourseWiseStudents(int sessionno, int schemeno, int rdbtn, int subid)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                SqlParameter[] objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[2] = new SqlParameter("@P_RD", rdbtn);
                objParams[3] = new SqlParameter("@P_SUBID", subid);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_PREREGIST_SP_REPORT_ROLLLIST", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetCourseWiseStudents-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetFailedSubjects(string regno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@REGNO", regno);

                ds = objSQL.ExecuteDataSetSP("PKG_SUD_HISTORY_SP_GET_FAILSUBJ", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetNewSchemesForRegistration-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetDetainedSubjects(string regno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@REGNO", regno);

                ds = objSQL.ExecuteDataSetSP("PKG_SUD_HISTORY_SP_GET_DETSUBJ", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetNewSchemesForRegistration-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetSubjectHistory(int idno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@IDNO", idno);

                ds = objSQL.ExecuteDataSetSP("PKG_SUD_HISTORY_SP_GET_PASSSUBJ", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetNewSchemesForRegistration-> " + ex.ToString());
            }
            return ds;
        }

        public bool CheckCourseRegistered(int idno, int sessionno, int schemeno, int courseno)
        {
            bool ret = false;
            try
            {
                SQLHelper objSH = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[4];

                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[2] = new SqlParameter("@P_COURSENO", courseno);
                objParams[3] = new SqlParameter("@P_IDNO", idno);

                object rt = objSH.ExecuteScalarSP("PKG_PREREGIST_SP_CHK_COURSREGISTERD", objParams);
                if (rt == null || Convert.ToInt32(rt) == 0)
                    ret = false;
                else
                    ret = true;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.CheckCourseRegistered-> " + ex.ToString());
            }
            return ret;
        }

        public int AcceptRegisteredSubjects(StudentRegist objSR)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New Registered Subject Details
                objParams = new SqlParameter[14];

                objParams[0] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[1] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[2] = new SqlParameter("@P_REGNO", objSR.REGNO);
                objParams[3] = new SqlParameter("@P_ROLLNO", objSR.ROLLNO);
                objParams[4] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                objParams[5] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[6] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[7] = new SqlParameter("@P_DETAINCOURSES", objSR.detainNos);
                objParams[8] = new SqlParameter("@P_SECTIONNOS", objSR.SECTIONNOS);
                objParams[9] = new SqlParameter("@P_DET_SECTIONNOS", objSR.detainSectionos);
                objParams[10] = new SqlParameter("@P_UA_NO", objSR.UA_NO);
                objParams[11] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[12] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);

                //objParams[5] = new SqlParameter("@P_BACKCOURSENOS", objSR.BackCourseNos);
                //objParams[12] = new SqlParameter("@P_ADDITIONAL_COURSENOS_UG", objSR.ADDITIONAL_COURSENOS_UG);
                //objParams[13] = new SqlParameter("@P_ADDITIONAL_COURSENOS_PG", objSR.ADDITIONAL_COURSENOS_PG);
                //objParams[14] = new SqlParameter("@P_GPCOURSENOS", objSR.GPCOURSES);
                //objParams[15] = new SqlParameter("@P_GPNOS", objSR.GPNOS);
                //objParams[18] = new SqlParameter("@P_BACK_SECTIONNOS", objSR.BackSectionos);
                //objParams[19] = new SqlParameter("@P_ADD_UG_SECTIONNOS", objSR.ADDITIONAL_COURSENOS_UG_SECTIONNOS);
                //objParams[20] = new SqlParameter("@P_ADD_PG_SECTIONNOS", objSR.ADDITIONAL_COURSENOS_PG_SECTIONNOS);
                //objParams[21] = new SqlParameter("@P_OPEN_ELECT_COURSE", objSR.OpenElectiveCourseNo);
                objParams[13] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[13].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PREREGIST_SP_ACCEPT_REGIST_SUBJECTS", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddAddlRegisteredSubjects-> " + ex.ToString());
            }

            return retStatus;
        }

        public int AddAddlRegisteredSubjects(StudentRegist objSR)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New Registered Subject Details
                objParams = new SqlParameter[14];

                objParams[0] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[1] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                objParams[3] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[4] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[5] = new SqlParameter("@P_DETAINCOURSES", objSR.detainNos);
                objParams[6] = new SqlParameter("@P_REGNO", objSR.REGNO);
                objParams[7] = new SqlParameter("@P_ROLLNO", objSR.ROLLNO);
                objParams[8] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[9] = new SqlParameter("@P_UA_NO", objSR._STUD_UA_NO);
                objParams[10] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                objParams[11] = new SqlParameter("@P_SECTIONNOS", objSR.SECTIONNOS);
                objParams[12] = new SqlParameter("@P_DET_SECTIONNOS", objSR.detainSectionos);

                objParams[13] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[13].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PREREGIST_SP_INS_REGIST_SUBJECTS", objParams, true);
                retStatus = (Int32)ret;     //Updated by ABhinay Lad [21-06-2019]
            }
            catch (Exception ex)
            {
                retStatus = -99;        //Updated by ABhinay Lad [21-06-2019]
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddAddlRegisteredSubjects-> " + ex.ToString());
            }

            return retStatus;
        }

        public int CountCourseRegistered(int sessionno, int schemeno, int idno)
        {
            int count = 0;
            try
            {
                SQLHelper objSH = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];

                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[2] = new SqlParameter("@P_IDNO", idno);

                object rt = objSH.ExecuteScalarSP("PKG_PREREGIST_SP_COUNT_COURSREGISTERED", objParams);
                count = Convert.ToInt32(rt);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.CountCourseRegistered-> " + ex.ToString());
            }
            return count;
        }

        public DataSet GetFailedSubjects(int idno, int sessionno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);

                ds = objSQL.ExecuteDataSetSP("PKG_STUD_SP_RET_FAIL_COURSES", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetNewSchemesForRegistration-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetRegisteredSubjects(int idno, int sessionno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);

                ds = objSQL.ExecuteDataSetSP("PKG_STUD_SP_RET_REGISTERED_COURSES", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetNewSchemesForRegistration-> " + ex.ToString());
            }
            return ds;
        }

        public int ExamRegistationRegularSubjects(StudentRegist objSR)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add Fail Subject Details
                objParams = new SqlParameter[12];
                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[2] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[3] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                objParams[4] = new SqlParameter("@P_COURSENO", objSR.COURSENO);
                objParams[5] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[6] = new SqlParameter("@P_EXAM_REGISTERED", objSR.EXAM_REGISTERED);
                objParams[7] = new SqlParameter("@P_S1IND", objSR.S1IND);
                objParams[8] = new SqlParameter("@P_S2IND", objSR.S2IND);
                objParams[9] = new SqlParameter("@P_S3IND", objSR.S3IND);
                objParams[10] = new SqlParameter("@P_S4IND", objSR.S4IND);
                objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[11].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_EXAM_REGISTATION_REGULAR_SUBJECTS", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddFailSubjects-> " + ex.ToString());
            }

            return retStatus;
        }

        #region Pre-Admission

        public int AddRegisteredSubjectsThirdSem(StudentRegist objSR, int Prev_status, int seatno)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New Registered Subject Details
                objParams = new SqlParameter[13];

                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_REGNO", objSR.REGNO);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                objParams[3] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[4] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[5] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[6] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[7] = new SqlParameter("@P_ACCEPT", objSR.ACEEPTSUB);
                objParams[8] = new SqlParameter("@P_PREV_STATUS", Prev_status);
                objParams[9] = new SqlParameter("@P_SEATNO", seatno);
                objParams[10] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                objParams[11] = new SqlParameter("@P_GDPOINT", objSR.GDPOINT);
                objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[12].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PREREGIST_SP_INS_THIRD_SEM_SUBJECTS", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddRegisteredSubjects-> " + ex.ToString());
            }

            return retStatus;
        }

        public string PreAdmissionRegistration(int sessionno, long idno, string studname, string fathername, string mothername,
           string lastname, string gender, DateTime dob, int mtongueno, int pcity, string ptelephonestd,
           string ptelephone, string pmobile, string emailid, int stateno, int caste, int categoryno, int nationalityno, int minority,
           int qualifyno, int ssc_maths, int ssc_maths_max, decimal ssc_maths_per, int ssc_total, int ssc_outofmarks,
           int mhcet_score, int mhcet_maths_score, int mhcet_physics_score,
           int hsc_maths, int hsc_maths_max, int hsc_chem, int hsc_chem_max, int hsc_phy, int hsc_phy_max, int hsc_pcm, int hsc_pcm_max,
           decimal per, int hsc_total, int hsc_outofmarks, int aieee_score, int aieee_rank, int aieee_rollno, string branch_pref, DateTime REG_ENTRY_DATE)
        {
            //CustomStatus cs = CustomStatus.Error;
            string retStatus = string.Empty;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_STUDNAME", studname),
                            new SqlParameter("@P_MOTHERNAME", mothername),
                            new SqlParameter("@P_FATHERNAME", fathername),
                            new SqlParameter("@P_LASTNAME", lastname),
                            new SqlParameter("@P_GENDER", gender),
                            new SqlParameter("@P_DOB", dob),
                            new SqlParameter("@P_MTONGUENO", mtongueno),
                            new SqlParameter("@P_PCITY", pcity),
                            new SqlParameter("@P_PTELEPHONESTD", ptelephonestd),
                            new SqlParameter("@P_PTELEPHONE", ptelephone),
                            new SqlParameter("@P_PMOBILE", pmobile),
                            new SqlParameter("@P_EMAILID", emailid),
                            new SqlParameter("@P_STATENO", stateno),
                            new SqlParameter("@P_CASTE", caste),
                            new SqlParameter("@P_CATEGORYNO", categoryno),
                            new SqlParameter("@P_NATIONALITYNO", nationalityno),
                            new SqlParameter("@P_MINORITY", minority),
                            new SqlParameter("@P_QUALIFYNO", qualifyno),
                            new SqlParameter("@P_SSC_MATHS", ssc_maths),
                            new SqlParameter("@P_SSC_MATHS_MAX", ssc_maths_max),
                            new SqlParameter("@P_SSC_MATHS_PER", ssc_maths_per),
                            new SqlParameter("@P_SSC_TOTAL", ssc_total),
                            new SqlParameter("@P_SSC_OUTOF", ssc_outofmarks),
                            new SqlParameter("@P_MHCET_SCORE", mhcet_score),
                            new SqlParameter("@P_MHCET_MATHS_SCORE", mhcet_maths_score),
                            new SqlParameter("@P_MHCET_PHYSICS_SCORE", mhcet_physics_score),
                            new SqlParameter("@P_HSC_MAT", hsc_maths),
                            new SqlParameter("@P_HSC_MAT_MAX", hsc_maths_max),
                            new SqlParameter("@P_HSC_CHE", hsc_chem),
                            new SqlParameter("@P_HSC_CHE_MAX", hsc_chem_max),
                            new SqlParameter("@P_HSC_PHY", hsc_phy),
                            new SqlParameter("@P_HSC_PHY_MAX", hsc_phy_max),
                            new SqlParameter("@P_HSC_PCM", hsc_pcm),
                            new SqlParameter("@P_HSC_PCM_MAX", hsc_pcm_max),
                            new SqlParameter("@P_PER", per),
                            new SqlParameter("@P_HSC_TOTAL", hsc_total),
                            new SqlParameter("@P_HSC_OUTOF", hsc_outofmarks),
                            new SqlParameter("@P_AIEEE_SCORE", aieee_score),
                            new SqlParameter("@P_AIEEE_RANK", aieee_rank),
                            new SqlParameter("@P_AIEEE_ROLLNO", aieee_rollno),
                            new SqlParameter("@P_BRANCH_PREF", branch_pref),
                            //new SqlParameter("@P_REG_ENTRY_DATE", REG_ENTRY_DATE),
                            new SqlParameter("@P_IDNO", idno),
                        };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                sqlParams[sqlParams.Length - 1].SqlDbType = SqlDbType.BigInt;
                object ret = objDataAccess.ExecuteNonQuerySP("PKG_ACAD_INS_REGISTRATION", sqlParams, true);

                //if (Convert.ToInt32(ret) == -99)
                //    cs = CustomStatus.TransactionFailed;
                //else
                //    cs = CustomStatus.RecordSaved;
                //cs = ret.ToString();
                retStatus = ret.ToString();
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.StudentRegistration.PreAdmissionRegistration() --> " + ex.Message + " " + ex.StackTrace);
            }
            return retStatus;
        }

        public DataSet GetMeritList(int sessionno, int aieee_mhcet, int scorefrom, int scoreto, int hsc_pcm, int minority, int combined, string fromdate, string todate)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                SqlParameter[] objParams = new SqlParameter[9];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_AIEEE_MHCET", aieee_mhcet);
                objParams[2] = new SqlParameter("@P_SCOREFROM", scorefrom);
                objParams[3] = new SqlParameter("@P_SCORETO ", scoreto);
                objParams[4] = new SqlParameter("@P_HSC_PCM", hsc_pcm);
                objParams[5] = new SqlParameter("@P_MINORITY", minority);
                objParams[6] = new SqlParameter("@P_COMBINED", combined);
                objParams[7] = new SqlParameter("@P_FROMDATE", fromdate);
                objParams[8] = new SqlParameter("@P_TODATE", todate);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_MERIT_LIST", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetMeritList-> " + ex.ToString());
            }
            return ds;
        }

        public CustomStatus GenerateMeritList(int sessionno, int aieee_mhcet, int minority, int combined, string fromdate, string todate)
        {
            CustomStatus cs = CustomStatus.Others;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                SqlParameter[] objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_AIEEE_MHCET", aieee_mhcet);
                objParams[2] = new SqlParameter("@P_MINORITY", minority);
                objParams[3] = new SqlParameter("@P_COMBINED", combined);
                objParams[4] = new SqlParameter("@P_FROMDATE", fromdate);
                objParams[5] = new SqlParameter("@P_TODATE", todate);

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_GENERATE_MERIT_LIST", objParams, false);
                if (ret != null)
                    cs = CustomStatus.RecordSaved;
                else
                    cs = CustomStatus.Error;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GenerateMeritList-> " + ex.ToString());
            }
            return cs;
        }

        public CustomStatus AllotBranch(int sessionno, long idno, int branchno, int roundno, int batchno, int paytypeno, int idtypeno)
        {
            CustomStatus cs = CustomStatus.Error;
            //string retStatus = string.Empty;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_IDNO", idno),
                            new SqlParameter("@P_BRANCHNO", branchno),
                            new SqlParameter("@P_ROUNDNO", roundno),
                            new SqlParameter("@P_ADMBATCH",batchno),
                            new SqlParameter("@P_PTYPE",paytypeno),
                            new SqlParameter("@P_IDTYPE",idtypeno),
                            new SqlParameter("@P_OUT", SqlDbType.Int)
                        };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                sqlParams[sqlParams.Length - 1].SqlDbType = SqlDbType.Int;
                object ret = objDataAccess.ExecuteNonQuerySP("PKG_ACAD_ALLOT_BRANCH", sqlParams, true);

                if (Convert.ToInt32(ret) == -99)
                    cs = CustomStatus.TransactionFailed;
                else
                    cs = CustomStatus.RecordSaved;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.StudentRegistration.UpdateDocumentVerfication() --> " + ex.Message + " " + ex.StackTrace);
            }
            return cs;
        }

        public DataSet GetBranchPreferences(int sessionno, long idno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_IDNO", idno);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_REPORT_PROCESS_FORM_BRANCH_PREF", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetBranchPreferences-> " + ex.ToString());
            }
            return ds;
        }

        #endregion Pre-Admission

        public int UpdatePaymentCategory(string idno, string ptype, string semesterno)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_PTYPE", ptype);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[3].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_SP_UPD_PAYMENTTYPE", objParams, false);

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddFailSubjects-> " + ex.ToString());
            }

            return retStatus;
        }

        // Generate Enrollment no.

        public CustomStatus GenereateRegistrationNo(int admbatch, int clg, int degree, int branch)
        {
            CustomStatus cs = CustomStatus.Error;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_ADMBATCH", admbatch),
                          new SqlParameter("@P_COLLEGEID", clg),
                          new SqlParameter("@P_DEGREENO", degree),
                          new SqlParameter("@P_BRANCHNO", branch)
                           // new SqlParameter("@P_CONT", cont)
                        };

                object ret = objDataAccess.ExecuteNonQuerySP("PKG_ACD_BULK_REGNO_GENERATION", sqlParams, false);

                cs = CustomStatus.RecordSaved;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.StudentRegistration.GenereateRegistrationNo() --> " + ex.Message + " " + ex.StackTrace);
            }
            return cs;
        }

        public int CheckN4Rule(int idno, int sessionno, int semesterno, int degreeno)
        {
            int ret = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                SqlParameter[] objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[3] = new SqlParameter("@P_DEGREENO", degreeno);

                ret = Convert.ToInt32(objSQLHelper.ExecuteScalarSP("PKG_ACAD_STUD_SEM_DATA", objParams));
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetPreviousSemesterStud-> " + ex.ToString());
            }

            return ret;
        }

        public int GenereateEnrollmentNo(int admbatch, int clg, int degree, int branch, int idtype, int generationtype)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[8];
                objParams[0] = new SqlParameter("@P_ADMBATCH", admbatch);
                objParams[1] = new SqlParameter("@P_COLLEGEID", clg);
                objParams[2] = new SqlParameter("@P_DEGREENO", degree);
                objParams[3] = new SqlParameter("@P_BRANCHNO", branch);
                objParams[4] = new SqlParameter("@P_IDTYPE", idtype);
                objParams[5] = new SqlParameter("@P_GENERATIONTYPE", generationtype);
                objParams[6] = new SqlParameter("@P_FLAG", 2);
                objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[7].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_BULK_ENROLLMENT_GENERATION", objParams, false);

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddFailSubjects-> " + ex.ToString());
            }

            return retStatus;
        }

        public int UpdateSemesterPromotionNo(string idno, int semesterno)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[2].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_UPD_PROMOTION_SEMESTRENO", objParams, false);

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddFailSubjects-> " + ex.ToString());
            }

            return retStatus;
        }

        public int UpdateSemesterProAddPromotionNo(string idno, int semesterno, int sessionno)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[2] = new SqlParameter("@P_SESSIONNO", sessionno);
                //objParams[3] = new SqlParameter("@P_YEAR_OLD", oldyear);
                //objParams[4] = new SqlParameter("@P_SEMESTER_OLD", oldsem);
                objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[3].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_UPD_PROMOTION_SEMESTRENO_PROV", objParams, false);

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddFailSubjects-> " + ex.ToString());
            }

            return retStatus;
        }

        public int UpdateBranchCategory(string idno, string branchno, string admcategoryno)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_BRANCHNO", branchno);
                objParams[2] = new SqlParameter("@P_ADMCATEGORYNO", admcategoryno);
                objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[3].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_SP_UPD_BRANCHCAT", objParams, false);

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddFailSubjects-> " + ex.ToString());
            }

            return retStatus;
        }

        public int AddExamRegisteredSubjects(StudentRegist objSR)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New eXAM Registered Subject Details
                objParams = new SqlParameter[10];

                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_REGNO", objSR.REGNO);
                objParams[2] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[3] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[4] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[5] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[6] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                //objParams[6] = new SqlParameter("@P_PREV_STATUS", Prev_status);
                objParams[7] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                objParams[8] = new SqlParameter("@P_EXREGIDNO", objSR.UA_NO);
                objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[9].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REGIST_SP_INS_EXAM_COURSE", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
            }

            return retStatus;
        }

        public int AddRevalautionRegisteredSubjects(StudentRegist objSR)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New eXAM Registered Subject Details
                objParams = new SqlParameter[10];
                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[2] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[3] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[4] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[5] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                objParams[7] = new SqlParameter("@P_EXREGIDNO", objSR.UA_NO);
                objParams[8] = new SqlParameter("@P_EXTERMARK", objSR.EXTERMARKS);
                objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[9].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REGIST_SP_INS_EXAM_COURSE_FOR_REVALUATION", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
            }

            return retStatus;
        }

        public int AddRevalautionMarkEntry(StudentRegist objSR)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add revlauation entry
                objParams = new SqlParameter[14];
                objParams[0] = new SqlParameter("@P_IDNOS", objSR.IDNOS);
                objParams[1] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[2] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[3] = new SqlParameter("@P_COURSENOS", objSR.COURSENO);
                objParams[4] = new SqlParameter("@P_IPADDRESS_V1", objSR.IPADDRESS);
                objParams[5] = new SqlParameter("@P_IPADDRESS_V2", objSR.IPADDRESS);
                objParams[6] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                objParams[7] = new SqlParameter("@P_UA_NO_V1", objSR.UA_NO);
                objParams[8] = new SqlParameter("@P_UA_NO_V2", objSR.UA_NO);
                //valuer1 marks
                if (objSR.VALUER1_MKS == null)
                    objParams[9] = new SqlParameter("@P_VALUER1_MKS", DBNull.Value);
                else
                    objParams[9] = new SqlParameter("@P_VALUER1_MKS", objSR.VALUER1_MKS);
                //valuer2 marks
                if (objSR.VALUER2_MKS == null)
                    objParams[10] = new SqlParameter("@P_VALUER2_MKS", DBNull.Value);
                else
                    objParams[10] = new SqlParameter("@P_VALUER2_MKS", objSR.VALUER2_MKS);
                ////valuer1 marks
                //if (objSR.VALUER1_MKS == null)
                //    objParams[11] = new SqlParameter("@P_MARK_DIFFS", DBNull.Value);
                //else
                //    objParams[11] = new SqlParameter("@P_MARK_DIFFS", objSR.VALUER1_MKS);
                //marks diff
                if (objSR.MARKDIFFS == null)
                    objParams[11] = new SqlParameter("@P_MARK_DIFFS", DBNull.Value);
                else
                    objParams[11] = new SqlParameter("@P_MARK_DIFFS", objSR.MARKDIFFS);

                // new marks
                if (objSR.NEWMARKS == null)
                    objParams[12] = new SqlParameter("@P_NEW_MARKS", DBNull.Value);
                else
                    objParams[12] = new SqlParameter("@P_NEW_MARKS", objSR.NEWMARKS);

                objParams[13] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[13].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_REVALUATION_MARK_ENTRY", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
            }

            return retStatus;
        }

        public int AddAddlRegisteredSubjectsPhd(StudentRegist objSR)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New Registered Subject Details
                objParams = new SqlParameter[11];

                objParams[0] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[1] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                objParams[3] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[4] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[5] = new SqlParameter("@P_REGNO", objSR.REGNO);
                objParams[6] = new SqlParameter("@P_ROLLNO", objSR.ROLLNO);
                objParams[7] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[8] = new SqlParameter("@P_CREDITS", objSR.CREDITS);
                objParams[9] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[10].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PREREGIST_SP_INS_REGIST_SUBJECTS_PHD", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddAddlRegisteredSubjects-> " + ex.ToString());
            }

            return retStatus;
        }

        //for branch change
        public CustomStatus GenereateRegistrationNoBranch(int degreeno, int branchno, int admbatch, int idno)
        {
            CustomStatus cs = CustomStatus.Error;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_DEGREENO", degreeno),
                            new SqlParameter("@P_BRANCHNO", branchno),
                            new SqlParameter("@P_ADMBATCH", admbatch),
                            new SqlParameter("@P_IDNO", idno),
                        };

                object ret = objDataAccess.ExecuteNonQuerySP("PKG_ACAD_BULK_REGNO_GENERATION_BRCHANGE_NEW", sqlParams, false);

                cs = CustomStatus.RecordSaved;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.StudentRegistration.GenereateRegistrationNo() --> " + ex.Message + " " + ex.StackTrace);
            }
            return cs;
        }

        public int AddAddCoursesForPhd(StudentRegist objSR)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New Registered Subject Details
                objParams = new SqlParameter[3];

                objParams[0] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[1] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[2].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PHD_COURSE_SP_INS_COURSE", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddAddlRegisteredSubjects-> " + ex.ToString());
            }

            return retStatus;
        }

        public int GenereateSingleEnrollmentNo(int admbatch, int semesterno, int idno)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_ADMBATCH", admbatch);
                objParams[1] = new SqlParameter("@P_SEMESTER", semesterno);
                objParams[2] = new SqlParameter("@P_IDNO", idno);
                objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[3].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_SINGLE_ENROLLMENT_GENERATION", objParams, false);

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddFailSubjects-> " + ex.ToString());
            }

            return retStatus;
        }

        public int InsertBranchChange(StudentRegist objStudent)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]

                        {
                         new SqlParameter("@P_IDNO", objStudent.IDNO),
                         new SqlParameter("@P_OLD_BRANCH", objStudent.BRANCHNO),
                         new SqlParameter("@P_BR_PREF", objStudent.BRANCH_REF),
                         new SqlParameter("@P_UA_IDNO", objStudent.UA_NO),
                         new SqlParameter("@P_IPADDRESS", objStudent.IPADDRESS),
                         new SqlParameter("@P_COLLEGE_CODE", objStudent.COLLEGE_CODE),
                         new SqlParameter("@P_ABID", status)
                    };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_INS_APPLY_BRANCH_CHANGE", sqlParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.InsertBranchChange-> " + ex.ToString());
            }

            return status;
        }

        public int AddUpdElectiveSubject(StudentRegist objSR)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]

                        {
                         new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO),
                         new SqlParameter("@P_IDNO", objSR.IDNO),
                         new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO),
                         new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO),
                         new SqlParameter("@P_COURSENOS", objSR.COURSENO),
                         new SqlParameter("@P_SELECTCOURSENOS", objSR.SELECT_COURSE),
                         new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS),
                         new SqlParameter("@P_CREDITS", objSR.CREDITS),
                         new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE),
                         new SqlParameter("@P_UA_NO", objSR.UA_NO),
                         new SqlParameter("@P_OUT", retStatus)
                    };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_STUDENT_INS_UPD_ELECTIVE", sqlParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    retStatus = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddUpdElectiveSubject-> " + ex.ToString());
            }

            return retStatus;

            //catch (Exception ex)
            //{
            //    retStatus = Convert.ToInt32(CustomStatus.Error);
            //    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddUpdElectiveSubject-> " + ex.ToString());
            //}

            //return retStatus;
        }

        // INSERT CONVOCATION CERTIFICATE//27-FEB-2014//UMESH
        public int AddConvocation(StudentRegist objSR, string studnames, string degree, string branch, string regulation_date, string convocation_date, string ipaddress, string deptname, int certno, int Conv_no)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New Registered Subject Details
                objParams = new SqlParameter[14];
                //idnos new memeber add in studentregistration.cs page.(27/01/2012)
                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNOS);
                objParams[1] = new SqlParameter("@P_REGNO", objSR.REGNO);
                objParams[2] = new SqlParameter("@P_STUDENTNAME", studnames);
                objParams[3] = new SqlParameter("@P_DEGREE", degree);
                objParams[4] = new SqlParameter("@P_BRANCH", branch);
                objParams[5] = new SqlParameter("@P_REGULATION_DATE", regulation_date);
                objParams[6] = new SqlParameter("@P_CONVOCATION_DATE", convocation_date);
                objParams[7] = new SqlParameter("@P_UA_NO", objSR.UA_NO);
                objParams[8] = new SqlParameter("@P_IPADDRESS", ipaddress);
                objParams[9] = new SqlParameter("@P_DEPTNAME", deptname);
                objParams[10] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                objParams[11] = new SqlParameter("@P_CERTNO", certno);
                objParams[12] = new SqlParameter("@P_CONV_NO", Conv_no);
                objParams[13] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[13].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_CONVOCATION_CERTIFICATE_INSERT", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistration.AddRegisteredSubjects-> " + ex.ToString());
            }

            return retStatus;
        }

        public DataSet GetStudentCoursesForBacklogRegistrationBACK(int sessionNo, int idno, int semesterno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
                objParams[1] = new SqlParameter("@P_IDNO", idno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);

                ds = objSQL.ExecuteDataSetSP("PR_ACD_GETFAIL_SUBJECT_FOR_BACKLOG_REGIST_BACK", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetAllCoursesForRegistration-> " + ex.ToString());
            }
            return ds;
        }

        public int InsertStudentRegistrationByAdminBack(StudentRegist objSR)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO),
                            new SqlParameter("@P_IDNO", objSR.IDNO),
                            new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO),
                            new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO),
                            new SqlParameter("@P_COURSENOS", objSR.COURSENOS),
                            new SqlParameter("@P_BACK_COURSENOS", objSR.Backlog_course),
                            new SqlParameter("@P_CREG_IDNO", objSR.UA_NO),
                            new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS),
                            new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE),
                            new SqlParameter("@P_REGNO", objSR.REGNO),
                            new SqlParameter("@P_ROLLNO", objSR.ROLLNO),
                            new SqlParameter("@P_OUT", SqlDbType.Int)
                        };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PREREGIST_SP_INS_STUDENT_REGISTRATION_BY_ADMIN_BACK", sqlParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistration.UdpateRegistrationByHOD-> " + ex.ToString());
            }
            return retStatus;
        }

        public int AddRegisteredSubjectsBulk(StudentRegist objSR, int Prev_status)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New Registered Subject Details
                objParams = new SqlParameter[13];
                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_REGNO", objSR.REGNO);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                objParams[3] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[4] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[5] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[6] = new SqlParameter("@P_ELECTIVES", objSR.ELECTIVE);
                objParams[7] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[8] = new SqlParameter("@P_ACCEPT", objSR.ACEEPTSUB);
                objParams[9] = new SqlParameter("@P_PREV_STATUS", Prev_status);
                objParams[10] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                objParams[11] = new SqlParameter("@P_CREGIDNO", objSR.UA_NO);
                objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[12].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PREREGIST_SP_INS_REGIST_SUBJECTS_BULK", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddRegisteredSubjects-> " + ex.ToString());
            }

            return retStatus;
        }

        public int AddRegisteredSubjectsBulkBACK(StudentRegist objSR, int Prev_status)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New Registered Subject Details
                objParams = new SqlParameter[13];

                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_REGNO", objSR.REGNO);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                objParams[3] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[4] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[5] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[6] = new SqlParameter("@P_ELECTIVES", objSR.ELECTIVE);
                objParams[7] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[8] = new SqlParameter("@P_ACCEPT", objSR.ACEEPTSUB);
                objParams[9] = new SqlParameter("@P_PREV_STATUS", Prev_status);
                objParams[10] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                objParams[11] = new SqlParameter("@P_CREGIDNO", objSR.UA_NO);
                objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[12].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PREREGIST_SP_INS_REGIST_SUBJECTS_BULK_BACK", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddRegisteredSubjects-> " + ex.ToString());
            }

            return retStatus;
        }

        //Added by Mr.Manish Walde on date 05/04/2016
        public DataSet GetRevalRegStudentList(int idno, int utype, int userDec, int ua_no, int sessionno, int semesterno, int select, int User_for)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                SqlParameter[] objParams = new SqlParameter[8];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_TYPE", utype);
                objParams[2] = new SqlParameter("@P_DEC", userDec);
                objParams[3] = new SqlParameter("@P_UA_NO", ua_no);
                objParams[4] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[5] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[6] = new SqlParameter("@P_OPERATION_FLAG ", select);
                objParams[7] = new SqlParameter("@P_USE_FOR", User_for);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_LIST_FOR_REVALUATION", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistration.GetStudentList-> " + ex.ToString());
            }

            return ds;
        }

        //Added by Mr.Manish Walde on date 05/04/2016
        public int AddUpdateRevalRegisteration(StudentRegist objSR, int Status, int operation, string App_Type)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New eXAM Registered Subject Details
                objParams = new SqlParameter[14];
                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[2] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[3] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[4] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[5] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                objParams[7] = new SqlParameter("@P_EXREGIDNO", objSR.UA_NO);
                objParams[8] = new SqlParameter("@P_EXTERMARK", objSR.EXTERMARKS);
                objParams[9] = new SqlParameter("@P_Ccode", objSR.CCODES);
                objParams[10] = new SqlParameter("@P_APPROVE_FLAG", Status);
                objParams[11] = new SqlParameter("@P_OPERATION_FLAG", operation);
                objParams[12] = new SqlParameter("@P_APP_TYPE", App_Type);
                objParams[13] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[13].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REGIST_SP_INS_EXAM_COURSE_FOR_REVALUATION", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(ret);
                else
                    retStatus = Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
            }
            return retStatus;
        }

        //added by reena
        public DataSet GetStudentCoursesForRegularRegistration(int idno, int sessionNo, int schemeNo, int semesterno, int pageFlag)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SESSIONNO", sessionNo);
                objParams[2] = new SqlParameter("@P_SCHEMENO", schemeNo);
                objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[4] = new SqlParameter("@P_PAGEFLAG", pageFlag);

                ds = objSQL.ExecuteDataSetSP("PR_ACD_GET_REG_SUBJECT_FOR_REGIST", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetStudentCoursesForRegularRegistration-> " + ex.ToString());
            }
            return ds;
        }

        // added by sandeep
        public DataSet GetStud_CourseReg_ChangeSemester_Bulk(int session, int college, int degree, int branch, int scheme, int semester)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                SqlParameter[] objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                objParams[1] = new SqlParameter("@P_COLLEGEID", college);
                objParams[2] = new SqlParameter("@P_DEGREENO", degree);
                objParams[3] = new SqlParameter("@P_BRANCHNO", branch);
                objParams[4] = new SqlParameter("@P_SCHEMENO", scheme);
                objParams[5] = new SqlParameter("@P_SEMESTERNO", semester);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_SEM_PROMOTION", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetStud_CourseReg_ChangeSemester_Bulk-> " + ex.ToString());
            }
            return ds;
        }

        public int AddRegisteredSubjects(StudentRegist objSR)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO),
                            new SqlParameter("@P_IDNO", objSR.IDNO),
                            new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO),
                            new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO),
                            new SqlParameter("@P_COURSENOS", objSR.COURSENOS),
                            //new SqlParameter("@P_BACK_COURSENOS", objSR.Backlog_course),
                            new SqlParameter("@P_AUDIT_COURSENOS", objSR.Audit_course),
                            new SqlParameter("@P_RE_APPEARED",objSR.Re_Appeared),
                            new SqlParameter("@P_REGNO", objSR.REGNO),
                            new SqlParameter("@P_ROLLNO", objSR.ROLLNO),
                            new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS),
                            new SqlParameter("@P_UA_N0", objSR.UA_NO),
                            new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE),
                            new SqlParameter("@P_REGISTERED", objSR.EXAM_REGISTERED),
                            new SqlParameter("@P_OUT", SqlDbType.Int)
                        };
                objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PREREGIST_SP_INS_REGIST_SUBJECTS", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddAddlRegisteredSubjects-> " + ex.ToString());
            }

            return retStatus;
        }

        public DataSet GetStudentCoursesForAuditRegistration(int idno, int sessionNo, int pageFlag)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SESSIONNO", sessionNo);
                objParams[2] = new SqlParameter("@P_PAGEFLAG", pageFlag);

                ds = objSQL.ExecuteDataSetSP("PR_ACD_GET_AUDIT_SUBJECT_FOR_REGIST", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetStudentCoursesForRegularRegistration-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetStudentCoursesForReAppearedCourseRegistration(int sessionNo, int idno, int semesterno, int pageFlag)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
                objParams[1] = new SqlParameter("@P_IDNO", idno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[3] = new SqlParameter("@P_PAGEFLAG", pageFlag);

                ds = objSQL.ExecuteDataSetSP("PR_ACD_GETFAIL_SUBJECT_FOR_REAPPEARED_REGIST", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetAllCoursesForRegistration-> " + ex.ToString());
            }
            return ds;
        }

        public int UdpateRegistrationByHOD(int sessionno, string idnos, int cRegIdno, string ipaddress)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_IDNOS", idnos),
                            new SqlParameter("@P_CREG_IDNO", cRegIdno),
                            new SqlParameter("@P_IPADDRESS", ipaddress),
                            new SqlParameter("@P_OUT", SqlDbType.Int)
                        };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PREREGIST_SP_UPD_REGISTRATION_BY_HOD", sqlParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistration.UdpateRegistrationByHOD-> " + ex.ToString());
            }

            return retStatus;
        }

        public DataSet GetCourseRegStudentList(int idno, int utype, int userDec, int ua_no, int sessionno, int opt_Flag)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                SqlParameter[] objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_TYPE", utype);
                objParams[2] = new SqlParameter("@P_DEC", userDec);
                objParams[3] = new SqlParameter("@P_UA_NO", ua_no);
                objParams[4] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[5] = new SqlParameter("@P_OPT_FLAG", opt_Flag);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_PREREGIST_SP_RET_STUDENTS_NEW", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistration.GetStudentList-> " + ex.ToString());
            }

            return ds;
        }

        //[25-10-2016]
        public DataSet Get_Operator_Alloted_Details(int SESSIONNO, int SEMESTERNO, int SCHEMENO)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_SESSIONNO", SESSIONNO),
                    new SqlParameter("@P_SEMESTERNO", SEMESTERNO),
                    new SqlParameter("@P_SCHEMENO", SCHEMENO)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_OPERATOR_ALLOTED_DETAILS", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.StudentRegistration.GetStudentInfoById_ForExamRegistratio() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public int UpdateOperatorAllotment_ForEndSem(int SESSIONNO, int SCHEMENO, int SEMESTERNO, int SUBID, string COURSENO, int VALUER_UA_NO, int TH_CUM_PR)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[]
                        {
                        new SqlParameter("@P_SESSIONNO", SESSIONNO),
                        new SqlParameter("@P_SCHEMENO", SCHEMENO),
                        new SqlParameter("@P_SEMESTERNO", SEMESTERNO),
                        new SqlParameter("@P_SUBID", SUBID),
                        new SqlParameter("@P_COURSENO", COURSENO),
                        new SqlParameter("@P_VALUER_UA_NO", VALUER_UA_NO),
                        new SqlParameter("@P_TH_CUM_PR", TH_CUM_PR),
                        new SqlParameter("@P_OUT", SqlDbType.Int)
                        };

                objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UP_OPERATOR_ALLOTMENT_FOR_ENDSEM", objParams, true);

                if (ret != null && ret.ToString() != "-99")

                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                else
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.AllotStudentBranch-> " + ex.ToString());
            }
            return retStatus;
        }

        /********************************ExamRegistrationForStudentLogin[05-10-2016]***********************************/

        //Method for get student details for Exam Registration--student login
        public DataSet GetStudentInfoById_ForExamRegistration(int studentId)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_IDNO", studentId)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_GET_STUDENT_INFO_FOR_EXAM_REGISTRATION", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.StudentRegistration.GetStudentInfoById_ForExamRegistratio() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        //[06-10-2016]
        public DataSet GetCurrentSemesterCourseList_ForExamRegistration(int IDNO, int SESSIONNO, int SEMESTERNO, int DEGREENO, int SCHEMENO)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_IDNO", IDNO),
                    new SqlParameter("@P_SESSIONNO", SESSIONNO),
                    new SqlParameter("@P_SEMESTERNO", SEMESTERNO),
                    new SqlParameter("@P_DEGREENO", DEGREENO),
                    new SqlParameter("@P_SCHEMENO", SCHEMENO)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_CURRENT_SEMESTER_COURSELIST", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.StudentRegistration.GetStudentInfoById_ForExamRegistratio() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        //[15-10-2016]
        public DataSet GetBackLogSemesterCourseList_ForExamRegistration(int IDNO, int SESSIONNO, int SEMESTERNO, int DEGREENO, int SCHEMENO)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_IDNO", IDNO),
                    new SqlParameter("@P_SESSIONNO", SESSIONNO),
                    new SqlParameter("@P_SEMESTERNO", SEMESTERNO),
                    new SqlParameter("@P_DEGREENO", DEGREENO),
                    new SqlParameter("@P_SCHEMENO", SCHEMENO)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_BACKLOG_SEMESTER_COURSELIST", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.StudentRegistration.GetStudentInfoById_ForExamRegistratio() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        //[14-11-2016]
        public DataSet GetCourseList_ForLateFees(int IDNO, int SESSIONNO, int SEMESTERNO, int DEGREENO, int SCHEMENO)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_IDNO", IDNO),
                    new SqlParameter("@P_SESSIONNO", SESSIONNO),
                    new SqlParameter("@P_SEMESTERNO", SEMESTERNO),
                    new SqlParameter("@P_DEGREENO", DEGREENO),
                    new SqlParameter("@P_SCHEMENO", SCHEMENO)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_COURSELIST_FOR_LATE_FEES", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.StudentRegistration.GetStudentInfoById_ForExamRegistratio() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetLateFees(DateTime CURR_DATE, int DEGREENO, int SESSIONNO, string RECEIPT_CODE)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                    {
                    new SqlParameter("@P_CURR_DATE", CURR_DATE),
                    new SqlParameter("@P_DEGREENO", DEGREENO),
                    new SqlParameter("@P_SESSIONNO", SESSIONNO),
                    new SqlParameter("@P_RECEIPT_CODE", RECEIPT_CODE)
                };
                ds = objDataAccess.ExecuteDataSetSP("GET_LATE_FEES_DETAILS_FOR_EXAMREGISTRATION", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.StudentRegistration.GetStudentInfoById_ForExamRegistratio() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public int AddExamRegisteredSubjectsbackmake(StudentRegist objSR, string ExistCourses, string ddlValue)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New eXAM Registered Subject Details

                objParams = new SqlParameter[12];
                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_REGNO", objSR.REGNO);
                objParams[2] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[3] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[4] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[5] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[6] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                //objParams[6] = new SqlParameter("@P_PREV_STATUS", Prev_status);
                objParams[7] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                objParams[8] = new SqlParameter("@P_EXREGIDNO", objSR.UA_NO);
                objParams[9] = new SqlParameter("@P_CANCOURSENOS", ExistCourses);
                objParams[10] = new SqlParameter("@P_SUB_TYPE", ddlValue);
                objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[11].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REGIST_SP_INS_EXAM_COURSE_makeup", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
            }
            return retStatus;
        }

        public int AddExamRegisteredSubjectsback(StudentRegist objSR, string ExistCourses, string ddlValue)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New eXAM Registered Subject Details
                objParams = new SqlParameter[7];

                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_UANO", objSR.UA_NO);
                objParams[2] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[3] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[4] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                objParams[5] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[6].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REGIST_SP_INS_EXAM_COURSE", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
            }
            return retStatus;
        }

        public DataSet GetStudentFailExamSubjectsmake(int idno, int semesterno, int Sessionno)
        {
            DataSet ds = null;

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SESSIONNO", Sessionno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_FAIL_STUDENT_LIST_9242017_105_makeup", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetails->" + ex.ToString());
            }
            return ds;
        }

        public DataSet GetRevalRegisteredStudentLists(int SESSIONNO, int COLLEGEID, int DEGREENO, int BRANCHNO, int SEMESTERNO, int type)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_SESSIONNO",SESSIONNO),
                     new SqlParameter("@P_COLLEGEID", COLLEGEID),
                    new SqlParameter("@P_DEGREENO", DEGREENO),
                    new SqlParameter("@P_BRANCHNO", BRANCHNO),
                    new SqlParameter("@P_SEMESTERNO", SEMESTERNO),
                    new SqlParameter("@P_TYPE", type)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_REEVALUATION_AND_SCRUTINY_DETAILS", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetRevalRegisteredStudentLists() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetRevalRegisteredStudentLists_CourseWise(int SESSIONNO, int COLLEGEID, int DEGREENO, int BRANCHNO, int SEMESTERNO, int COURSENO, int type)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_SESSIONNO",SESSIONNO),
                     new SqlParameter("@P_COLLEGEID", COLLEGEID),
                    new SqlParameter("@P_DEGREENO", DEGREENO),
                    new SqlParameter("@P_BRANCHNO", BRANCHNO),
                    new SqlParameter("@P_SEMESTERNO", SEMESTERNO),
                    new SqlParameter("@P_COURSENO", COURSENO),
                    new SqlParameter("@P_TYPE", type)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_REEVALUATION_DETAILS_COURSEWISE", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetRevalRegisteredStudentLists() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetStudentFailExamSubjects(int idno, int semesterno, int Sessionno)
        {
            DataSet ds = null;

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SESSIONNO", Sessionno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_FAIL_STUDENT_LIST_SVCE", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetails->" + ex.ToString());
            }
            return ds;
        }

        public DataSet GetStudentDropSubjects(int idno, int semesterno, int Sessionno)
        {
            DataSet ds = null;

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SESSIONNO", Sessionno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_COURSES_DROP", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetails->" + ex.ToString());
            }
            return ds;
        }

        public DataSet GetStudentwithdrawSubjects(int idno, int semesterno, int Sessionno)
        {
            DataSet ds = null;

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SESSIONNO", Sessionno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_COURSES_WITHDRAW", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetails->" + ex.ToString());
            }
            return ds;
        }

        public DataSet GetStudentForEnrollGeneration(int admbatch, int collegeid, int degreeno, int branchno)
        {
            DataSet ds = null;

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_ADMBATCH", admbatch);
                objParams[1] = new SqlParameter("@P_COLLEGEID", collegeid);
                objParams[2] = new SqlParameter("@P_DEGREENO", degreeno);
                objParams[3] = new SqlParameter("@P_BRANCHNO", branchno);
                objParams[4] = new SqlParameter("@P_FLAG", 1);
                objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[5].Direction = ParameterDirection.Output;

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_BULK_ENROLLMENT_GENERATION", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentForEnrollGeneration->" + ex.ToString());
            }
            return ds;
        }

        public int AddRegisteredSubjectsBulkElective(StudentRegist objSR, int Prev_status)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New Registered Subject Details
                objParams = new SqlParameter[13];

                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_REGNO", objSR.REGNO);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                objParams[3] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[4] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[5] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[6] = new SqlParameter("@P_ELECTIVES", objSR.ELECTIVE);
                objParams[7] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[8] = new SqlParameter("@P_ACCEPT", objSR.ACEEPTSUB);
                objParams[9] = new SqlParameter("@P_PREV_STATUS", Prev_status);
                objParams[10] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                objParams[11] = new SqlParameter("@P_CREGIDNO", objSR.UA_NO);
                objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[12].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PREREGIST_SP_INS_REGIST_SUBJECTS_ELECTIVE", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddRegisteredSubjects-> " + ex.ToString());
            }

            return retStatus;
        }

        public DataSet DeleteRegisteredCourse(int sessionNo, int idno, string coursesrno, string remark, string ipaddress, int ua_no)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
                objParams[1] = new SqlParameter("@P_IDNO", idno);
                objParams[2] = new SqlParameter("@P_COURSESRNO", coursesrno);
                objParams[3] = new SqlParameter("@P_REMARK", remark);
                objParams[4] = new SqlParameter("@P_IPADDS", ipaddress);
                objParams[5] = new SqlParameter("@P_UANO", ua_no);

                ds = objSQL.ExecuteDataSetSP("PKG_ACD_STUDENT_DELETE_COURSE", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetAllCoursesForRegistration-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetStudentCourseforDelete(int sessionNo, int idno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
                objParams[1] = new SqlParameter("@P_IDNO", idno);
                ds = objSQL.ExecuteDataSetSP("PKG_ACD_STUDENT_DELETE_COURSE_LIST", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetAllCoursesForRegistration-> " + ex.ToString());
            }
            return ds;
        }

        public int AddCourseExamRule(StudentRegist objSR)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[14];

                objParams[0] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[1] = new SqlParameter("@P_DEGREENO", objSR.DEGREENO);
                objParams[2] = new SqlParameter("@P_BRANCHNO", objSR.BRANCHNO);
                objParams[3] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[4] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                objParams[5] = new SqlParameter("@P_COURSENO", objSR.COURSENNO.Remove(objSR.COURSENNO.Length - 1, 1));
                objParams[6] = new SqlParameter("@P_CCODE", objSR.CCODE.Remove(objSR.CCODE.Length - 1, 1));
                objParams[7] = new SqlParameter("@P_COURSENAME", objSR.COURSENAME.Remove(objSR.COURSENAME.Length - 1, 1));
                objParams[8] = new SqlParameter("@P_EXAMNO", objSR.CATEGORY3.Remove(objSR.CATEGORY3.Length - 1, 1));
                objParams[9] = new SqlParameter("@P_RULE1", objSR.Rule11.Remove(objSR.Rule11.Length - 1, 1));
                objParams[10] = new SqlParameter("@P_RULE2", objSR.Rule22.Remove(objSR.Rule22.Length - 1, 1));
                objParams[11] = new SqlParameter("@P_ISLOCK", objSR.START_NO);
                objParams[12] = new SqlParameter("@P_OTYPE", objSR.USERTTYPE);
                objParams[13] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[13].Direction = ParameterDirection.Output;
                //object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INS_EXAM_RULE", objParams, true);
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_EXAM_RULE_INSERT", objParams, true);
                retStatus = (int)ret;
            }
            catch (Exception ex)
            {
                retStatus = -99;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddCourseExamRule-> " + ex.ToString());
            }
            return retStatus;
        }

        //[ Elective Group-5 Courses]
        public DataSet GetElectiveGrp5CourseForReg(int idno, int semno, int sessionno, int schemeno, int user_type)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SEMESTERNO", semno);
                objParams[2] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[3] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[4] = new SqlParameter("@P_USER_TYPE", user_type);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ELECTIVE_OFFERED_COURSE_FOR_REG_Grp5", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetStudentList-> " + ex.ToString());
            }

            return ds;
        }

        //[ Elective Group-6 Courses]
        public DataSet GetElectiveGrp6CourseForReg(int idno, int semno, int sessionno, int schemeno, int user_type)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SEMESTERNO", semno);
                objParams[2] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[3] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[4] = new SqlParameter("@P_USER_TYPE", user_type);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ELECTIVE_OFFERED_COURSE_FOR_REG_Grp6", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetStudentList-> " + ex.ToString());
            }

            return ds;
        }

        //[ Elective Group-7 Courses]
        public DataSet GetElectiveGrp7CourseForReg(int idno, int semno, int sessionno, int schemeno, int user_type)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SEMESTERNO", semno);
                objParams[2] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[3] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[4] = new SqlParameter("@P_USER_TYPE", user_type);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ELECTIVE_OFFERED_COURSE_FOR_REG_Grp7", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetStudentList-> " + ex.ToString());
            }

            return ds;
        }

        #region "Photo Copy"

        //Added by Neha Baranwal on date 08/01/2020
        public int AddPhotoCopyRegisteration(StudentRegist objSR, string App_Type)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New eXAM Registered Subject Details
                objParams = new SqlParameter[12];
                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[2] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[3] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[4] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[5] = new SqlParameter("@P_SEMESTERNOS", objSR.SEMESTERNOS);
                objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                objParams[7] = new SqlParameter("@P_UA_NO", objSR.UA_NO);
                objParams[8] = new SqlParameter("@P_EXTERMARK", objSR.EXTERMARKS);
                objParams[9] = new SqlParameter("@P_CCODE", objSR.CCODES);
                objParams[10] = new SqlParameter("@P_APP_TYPE", App_Type);
                objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[11].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REGIST_SP_INS_EXAM_COURSE_FOR_PHOTOCOPY", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(ret);
                else
                    retStatus = Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AddPhotoCopyRegisteration-> " + ex.ToString());
            }
            return retStatus;
        }

        public int AddPhotoCopyRegisteration(StudentRegist objSR, string App_Type, string Total_Exter_Marks, int User_Type)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New eXAM Registered Subject Details
                objParams = new SqlParameter[14];
                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[2] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[3] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[4] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[5] = new SqlParameter("@P_SEMESTERNOS", objSR.SEMESTERNOS);
                objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                objParams[7] = new SqlParameter("@P_UA_NO", objSR.UA_NO);
                objParams[8] = new SqlParameter("@P_GRADES", objSR.EXTERMARKS);
                objParams[9] = new SqlParameter("@P_CCODE", objSR.CCODES);
                objParams[10] = new SqlParameter("@P_APP_TYPE", App_Type);
                objParams[11] = new SqlParameter("@P_EXTER_MARKS", Total_Exter_Marks);
                objParams[12] = new SqlParameter("@P_USER_TYPE", User_Type);
                objParams[13] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[13].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REGIST_SP_INS_EXAM_COURSE_FOR_PHOTOCOPY", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(ret);
                else
                    retStatus = Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AddPhotoCopyRegisteration-> " + ex.ToString());
            }
            return retStatus;
        }

        public int AddPhotoRevalRegByAdmin(StudentRegist objSR, string App_Type, string Total_Exter_Marks, int User_Type)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New eXAM Registered Subject Details
                objParams = new SqlParameter[14];
                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[2] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[3] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[4] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[5] = new SqlParameter("@P_SEMESTERNOS", objSR.SEMESTERNOS);
                objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                objParams[7] = new SqlParameter("@P_UA_NO", objSR.UA_NO);
                objParams[8] = new SqlParameter("@P_GRADES", objSR.EXTERMARKS);
                objParams[9] = new SqlParameter("@P_CCODE", objSR.CCODES);
                objParams[10] = new SqlParameter("@P_APP_TYPE", App_Type);
                objParams[11] = new SqlParameter("@P_EXTER_MARKS", Total_Exter_Marks);
                objParams[12] = new SqlParameter("@P_USER_TYPE", User_Type);
                objParams[13] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[13].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REGIST_SP_INS_FOR_PHOTOREVAL_ADMIN", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(ret);
                else
                    retStatus = Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AddPhotoRevalRegByAdmin-> " + ex.ToString());
            }
            return retStatus;
        }

        //added by neha
        public DataSet GetStudentFailExamSubjects(int idno, int semesterno, int Sessionno, int Schemeno)
        {
            DataSet ds = null;

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SESSIONNO", Sessionno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[3] = new SqlParameter("@P_SCHEMENO", Schemeno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_FAIL_STUDENT_LIST_SVCE", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentFailExamSubjects->" + ex.ToString());
            }
            return ds;
        }

        public int AddExamRegisteredSubjectsback(StudentRegist objSR, decimal Exam_Amt)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New eXAM Registered Subject Details
                objParams = new SqlParameter[8];

                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_UANO", objSR.UA_NO);
                objParams[2] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[3] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[4] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                objParams[5] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[6] = new SqlParameter("@P_EXAM_AMT", Exam_Amt);
                objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[7].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REGIST_SP_INS_EXAM_COURSE", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AddExamRegisteredSubjectsback-> " + ex.ToString());
            }
            return retStatus;
        }

        public DataSet GetStudentAllFailExamSubjects(int idno, int Sessionno, int Schemeno)
        {
            DataSet ds = null;

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SESSIONNO", Sessionno);
                objParams[2] = new SqlParameter("@P_SCHEMENO", Schemeno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_FAIL_STUDENT_LIST_SVCE_NEW", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentFailExamSubjects->" + ex.ToString());
            }
            return ds;
        }

        public int AddExamRegisteredSubjectsback(StudentRegist objSR, decimal Exam_Amt, string SemesterNos, string SubIdS)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New eXAM Registered Subject Details
                objParams = new SqlParameter[10];

                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_UANO", objSR.UA_NO);
                objParams[2] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[3] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[4] = new SqlParameter("@P_SEMESTERNOS", SemesterNos);
                objParams[5] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[6] = new SqlParameter("@P_SUBIDS", SubIdS);
                objParams[7] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[8] = new SqlParameter("@P_EXAM_AMT", Exam_Amt);
                objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[9].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REGIST_SP_INS_EXAM_COURSE", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AddExamRegisteredSubjectsback-> " + ex.ToString());
            }
            return retStatus;
        }

        #endregion "Photo Copy"

        #region Condonation Payment

        public int InsertDemandDcrCondSubjectsFees(StudentRegist objSR, decimal Exam_Amt, int SemesterNo)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New eXAM Registered Subject Details
                objParams = new SqlParameter[8];

                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_UANO", objSR.UA_NO);
                objParams[2] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[3] = new SqlParameter("@P_SEMESTERNO", SemesterNo);
                objParams[4] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[5] = new SqlParameter("@P_EXAM_AMT", Exam_Amt);
                objParams[6] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[7].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_INS_CONDONATION_COURSE_FEES", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.InsertDemandDcrCondSubjectsFees-> " + ex.ToString());
            }
            return retStatus;
        }

        #endregion Condonation Payment

        //added by pankaj nakhahle 5 march 2020 for re-admission page
        public int InsertStudent_Re_Admission(StudentRegist objSR)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO),
                            new SqlParameter("@P_IDNO", objSR.IDNO),
                            new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO),
                            new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO),
                            new SqlParameter("@P_COURSENOS", objSR.COURSENOS),
                           ////commented by pankaj 07 aprl 2020  new SqlParameter("@P_BACK_COURSENOS", objSR.Backlog_course),
                           //commented by pankaj 07 aprl 2020 new SqlParameter("@P_AUDIT_COURSENOS", objSR.Audit_course),
                            new SqlParameter("@P_CREG_IDNO", objSR.UA_NO),
                            new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS),
                            new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE),
                            new SqlParameter("@P_REGNO", objSR.REGNO),
                            //new SqlParameter("@P_ROLLNO", objSR.ROLLNO),
                            new SqlParameter("@P_OUT", SqlDbType.Int)
                        };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PREREGIST_SP_INS_STUDENT_RE_ADMISSION", sqlParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistration.InsertStudent_Re_Admission-> " + ex.ToString());
            }
            return retStatus;
        }

        //added by pankaj nakhahle 4 april 2020 for updating student information like regno , enroll , semester , admbatch , scheme for re-admission page
        public int Update_StudentDetails_For_Re_Admission(int admbatch, int scheme, int semesetr, string regno, int SESSIONNO, int IDNO, int UA_NO, int deegreeno)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SEMESTERNO", semesetr),
                            new SqlParameter("@P_SCHEMENO", scheme),
                            new SqlParameter("@P_REGNO", regno),
                            new SqlParameter("@P_ADMBATCH", admbatch),
                            new SqlParameter("@P_SESSIONNO", SESSIONNO),
                            new SqlParameter("@P_IDNO", IDNO),
                            new SqlParameter("@P_USERNO", UA_NO),
                            new SqlParameter("@P_DEGREENO", deegreeno),
                            new SqlParameter("@P_OUT", SqlDbType.Int)
                        };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_Update_Student_Info_for_ReAdmission", sqlParams, true); //PKG_Update_Details_For_Re_Admission_Student
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistration.InsertStudent_Re_Admission-> " + ex.ToString());
            }
            return retStatus;
        }

        //added by pankaj for showing student details 22/04/2020
        public DataSet GetStudInfoForCourseRegi_ForAdmission(int idno, int sessionno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                ds = objSQL.ExecuteDataSetSP("PKG_SHOW_STUDENT_INFO_FOR_STUD_REGI_READMISSION", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetNewSchemesForRegistration-> " + ex.ToString());
            }
            return ds;
        }

        //added by pankaj nakhahle 27 April 2020 for re-admission page
        public int InsertReviewApplyReg(StudentRegist objSR, decimal Amount)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        {
                             new SqlParameter("@P_IDNO", objSR.IDNO),
                            new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO),
                            new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNOS),
                            new SqlParameter("@P_COURSENOS", objSR.COURSENOS),
                            new SqlParameter("@P_CREG_IDNO", objSR.UA_NO),
                            new SqlParameter("@P_Amount", Amount),
                            new SqlParameter("@P_OUT", SqlDbType.Int)
                        };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REGIST_SP_INS_FOR_REVIEW_APPLY_ADMIN", sqlParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistration.InsertStudent_Re_Admission-> " + ex.ToString());
            }
            return retStatus;
        }

        //added by pankaj nakhahle 27 April 2020 for re-admission page
        public int InsertReviewApplyReg(StudentRegist objSR, decimal Amount, string DDNO)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        {
                             new SqlParameter("@P_IDNO", objSR.IDNO),
                            new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO),
                            new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNOS),
                            new SqlParameter("@P_COURSENOS", objSR.COURSENOS),
                            new SqlParameter("@P_CREG_IDNO", objSR.UA_NO),
                            new SqlParameter("@P_Amount", Amount),
                             new SqlParameter("@P_DD_NO", DDNO),
                            new SqlParameter("@P_OUT", SqlDbType.Int)
                        };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REGIST_SP_INS_FOR_REVIEW_APPLY_ADMIN", sqlParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistration.InsertStudent_Re_Admission-> " + ex.ToString());
            }
            return retStatus;
        }

        // Added By Pritish S. on 26/05/2020 to Add data to Result Table
        public int AddStudentResultData(string SESSIONNOS, StudentRegist objSR)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[13];
                objParams[0] = new SqlParameter("@P_SESSIONNOS", SESSIONNOS);
                objParams[1] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[2] = new SqlParameter("@P_REGNO", objSR.REGNO);
                objParams[3] = new SqlParameter("@P_ROLLNO", objSR.ROLLNO);
                objParams[4] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                objParams[5] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[6] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[7] = new SqlParameter("@P_SECTIONNOS", objSR.SECTIONNOS);
                objParams[8] = new SqlParameter("@P_UA_NO", objSR.UA_NO);
                objParams[9] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[10] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                objParams[11] = new SqlParameter("@P_GRADES", objSR.GRADE);

                objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[12].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_STUDENT_RESULT_DATA", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddAddlRegisteredSubjects-> " + ex.ToString());
            }
            return retStatus;
        }

        // Added By Pritish S. on 26/05/2020 to Store Data to Equivalent Courses
        public int AddTransferedStudentRecord(string SESSIONNOS, StudentRegist objSR, string equigrade, DateTime date)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[13];
                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_SESSIONNO", SESSIONNOS);
                objParams[2] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[3] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                objParams[4] = new SqlParameter("@P_COURSENO", objSR.COURSENOS);
                objParams[5] = new SqlParameter("@P_EQUI_CCODE", objSR.CCODES);
                objParams[6] = new SqlParameter("@P_EQUI_COURSE", objSR.COURSENAMES);
                objParams[7] = new SqlParameter("@P_EQUI_GRADE", equigrade);
                objParams[8] = new SqlParameter("@P_NEWGRADE", objSR.GRADE);
                objParams[9] = new SqlParameter("@P_UA_NO", objSR.UA_NO);
                objParams[10] = new SqlParameter("@P_TRANSDATE", date);
                objParams[11] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);

                objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[12].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_ADD_TRANSFERED_STUD_EQUI_RECORD", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddAddlRegisteredSubjects-> " + ex.ToString());
            }
            return retStatus;
        }

        public int AddUpdatewithdrwcourses(StudentRegist objSR, string reason)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New eXAM Registered Subject Details
                objParams = new SqlParameter[10];
                objParams[0] = new SqlParameter("@P_WDNO", objSR.WITHDRAWNO);
                objParams[1] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[2] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[3] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[4] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[5] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                objParams[6] = new SqlParameter("@P_EXREGIDNO", objSR.UA_NO);
                objParams[7] = new SqlParameter("@P_CCODE", objSR.CCODES);
                objParams[8] = new SqlParameter("@P_REASON", reason);
                objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[9].Direction = ParameterDirection.Output;
                //object ret = objSQLHelper.ExecuteNonQuerySP("PKG_DROUP_COURSES", objParams, true);
                object ret = objSQL.ExecuteNonQuerySP("PKG_WITHDRAW_COURSES", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(ret);
                else
                    retStatus = Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
            }
            return retStatus;
        }

        public int AddUpdatewithdrwcoursesNEW(StudentRegist objSR, string reason)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New eXAM Registered Subject Details
                objParams = new SqlParameter[9];
                // objParams[0] = new SqlParameter("@P_WDNO", objSR.WITHDRAWNO);
                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[2] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[3] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[4] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                objParams[5] = new SqlParameter("@P_EXREGIDNO", objSR.UA_NO);
                objParams[6] = new SqlParameter("@P_CCODE", objSR.CCODES);
                objParams[7] = new SqlParameter("@P_REASON", reason);
                objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[8].Direction = ParameterDirection.Output;
                //object ret = objSQLHelper.ExecuteNonQuerySP("PKG_DROUP_COURSES", objParams, true);
                object ret = objSQL.ExecuteNonQuerySP("PKG_WITHDRAW_COURSES", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(ret);
                else
                    retStatus = Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
            }
            return retStatus;
        }

        //Added By Pritish S. on 26/09/2020 for filling Student Result Details
        public DataSet GetResultDetails(int idno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_RESULT_DETAILS", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
            }
            finally
            {
                ds.Dispose();
            }
            return ds;
        }

        //Added By Pritish S. on 26/09/2020 for saving result modification remark.
        public int AddResultRemark(int idno, string remark, string ipaddress)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_REMARK", remark);
                objParams[2] = new SqlParameter("@P_IPADDRESS", ipaddress);
                objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[3].Direction = ParameterDirection.Output;

                object ret = objSQL.ExecuteNonQuerySP("PKG_ACAD_INSERT_RESULT_REMARK", objParams, true);
                if (Convert.ToInt32(ret) == 1)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
            }
            return retStatus;
        }

        public int AddResultRemark(int idno, string remark, string ipaddress, int applied)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_REMARK", remark);
                objParams[2] = new SqlParameter("@P_IPADDRESS", ipaddress);
                objParams[3] = new SqlParameter("@P_APPLIED", applied);
                objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[4].Direction = ParameterDirection.Output;

                object ret = objSQL.ExecuteNonQuerySP("PKG_ACAD_INSERT_RESULT_REMARK", objParams, true);
                if (Convert.ToInt32(ret) == 1)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
            }
            return retStatus;
        }

        //Added By Pritish S. on 16/10/2020
        //[ Elective Group-8 Courses]
        public DataSet GetElectiveGrp8CourseForReg(int idno, int semno, int sessionno, int schemeno, int user_type)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SEMESTERNO", semno);
                objParams[2] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[3] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[4] = new SqlParameter("@P_USER_TYPE", user_type);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ELECTIVE_OFFERED_COURSE_FOR_REG_Grp8", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetStudentList-> " + ex.ToString());
            }
            return ds;
        }

        //Added By Pritish S. on 16/10/2020
        //[ Elective Group-9 Courses]
        public DataSet GetElectiveGrp9CourseForReg(int idno, int semno, int sessionno, int schemeno, int user_type)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SEMESTERNO", semno);
                objParams[2] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[3] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[4] = new SqlParameter("@P_USER_TYPE", user_type);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ELECTIVE_OFFERED_COURSE_FOR_REG_Grp9", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetStudentList-> " + ex.ToString());
            }
            return ds;
        }

        //Added By Pritish S. on 16/10/2020
        //[ Elective Group-10 Courses]
        public DataSet GetElectiveGrp10CourseForReg(int idno, int semno, int sessionno, int schemeno, int user_type)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SEMESTERNO", semno);
                objParams[2] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[3] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[4] = new SqlParameter("@P_USER_TYPE", user_type);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ELECTIVE_OFFERED_COURSE_FOR_REG_Grp10", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetStudentList-> " + ex.ToString());
            }
            return ds;
        }

        //Added By Pritish S. on 16/10/2020
        //[ Elective Group-11 Courses]
        public DataSet GetElectiveGrp11CourseForReg(int idno, int semno, int sessionno, int schemeno, int user_type)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SEMESTERNO", semno);
                objParams[2] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[3] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[4] = new SqlParameter("@P_USER_TYPE", user_type);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ELECTIVE_OFFERED_COURSE_FOR_REG_Grp11", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetStudentList-> " + ex.ToString());
            }
            return ds;
        }

        //Added By Pritish S. on 16/10/2020
        //[ Elective Group-12 Courses]
        public DataSet GetElectiveGrp12CourseForReg(int idno, int semno, int sessionno, int schemeno, int user_type)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SEMESTERNO", semno);
                objParams[2] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[3] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[4] = new SqlParameter("@P_USER_TYPE", user_type);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ELECTIVE_OFFERED_COURSE_FOR_REG_Grp12", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetStudentList-> " + ex.ToString());
            }
            return ds;
        }

        //Added By Pritish S. on 16/10/2020
        //[ Elective Group-13 Courses]
        public DataSet GetElectiveGrp13CourseForReg(int idno, int semno, int sessionno, int schemeno, int user_type)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SEMESTERNO", semno);
                objParams[2] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[3] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[4] = new SqlParameter("@P_USER_TYPE", user_type);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ELECTIVE_OFFERED_COURSE_FOR_REG_Grp13", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetStudentList-> " + ex.ToString());
            }
            return ds;
        }

        //Added By Pritish S. on 16/10/2020
        //[ Elective Group-14 Courses]
        public DataSet GetElectiveGrp14CourseForReg(int idno, int semno, int sessionno, int schemeno, int user_type)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SEMESTERNO", semno);
                objParams[2] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[3] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[4] = new SqlParameter("@P_USER_TYPE", user_type);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ELECTIVE_OFFERED_COURSE_FOR_REG_Grp14", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetStudentList-> " + ex.ToString());
            }
            return ds;
        }

        //Added By Pritish S. on 16/10/2020
        //[ Elective Group-15 Courses]
        public DataSet GetElectiveGrp15CourseForReg(int idno, int semno, int sessionno, int schemeno, int user_type)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SEMESTERNO", semno);
                objParams[2] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[3] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[4] = new SqlParameter("@P_USER_TYPE", user_type);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ELECTIVE_OFFERED_COURSE_FOR_REG_Grp15", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetStudentList-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetOpenElectiveCourseForReg(int idno, int semno, int sessionno, int schemeno, int user_type)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SEMESTERNO", semno);
                objParams[2] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[3] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[4] = new SqlParameter("@P_USER_TYPE", user_type);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_OPEN_ELECTIVE_OFFERED_COURSE_FOR_REG", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetStudentList-> " + ex.ToString());
            }

            return ds;
        }

        // Added by Pritish on 11/02/2021
        // Added by Pritish on 11/02/2021
        public int BulkEnrollmentNoGenereation(int admbatch, int degree, int branch, int ptype)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_ADMBATCH", admbatch);
                objParams[1] = new SqlParameter("@P_DEGREENO", degree);
                objParams[2] = new SqlParameter("@P_BRANCHNO", branch);
                objParams[3] = new SqlParameter("@P_PTYPE", ptype);
                objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[4].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_BULK_ENROLLNO_GENERATION", objParams, false);

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddFailSubjects-> " + ex.ToString());
            }
            return retStatus;
        }

        public int AddUpdateScrutinyRegisterationthroughPayment(StudentRegist objSR, int Status, int operation, string RECHECKORREASS, int checkreadpaper, double totalamt, string receipttype, string CA_Marks, string AMOUNT)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New eXAM Registered Subject Details
                objParams = new SqlParameter[21];
                objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                objParams[1] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                objParams[2] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                objParams[3] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                objParams[4] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                objParams[5] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                objParams[7] = new SqlParameter("@P_EXREGIDNO", objSR.USERTTYPE);
                objParams[8] = new SqlParameter("@P_EXTERMARK", objSR.EXTERMARKS);
                objParams[9] = new SqlParameter("@P_CCODE", objSR.CCODES);
                objParams[10] = new SqlParameter("@P_APPROVE_FLAG", Status);
                objParams[11] = new SqlParameter("@P_OPERATION_FLAG", operation);
                objParams[12] = new SqlParameter("@P_RECHECKORREASS", RECHECKORREASS);
                objParams[13] = new SqlParameter("@P_CHECKSTATUS", checkreadpaper);
                objParams[14] = new SqlParameter("@P_GRADES", objSR.GRADES);
                objParams[15] = new SqlParameter("@P_APPROVE_UA_NO", objSR.UA_NO);
                objParams[16] = new SqlParameter("@P_TOTAL_AMT", totalamt);
                objParams[17] = new SqlParameter("@P_RECEIPT_FLAG", receipttype);
                objParams[18] = new SqlParameter("@P_CA_MARKS", CA_Marks);
                objParams[19] = new SqlParameter("@P_AMOUNT", AMOUNT);
                objParams[20] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[20].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REGIST_SP_INS_EXAM_COURSE_FOR_REVALUATION_THROUGH_PAYMENT", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(ret);
                else
                    retStatus = Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
            }
            return retStatus;
        }

        public int UpdateFinalDetend(StudentRegist objStud, string NUMBER)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[11];

                objParams[0] = new SqlParameter("@P_SESSIONNO", objStud.SESSIONNO);
                objParams[1] = new SqlParameter("@P_SCHEMENO", objStud.SCHEMENO);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", objStud.SEMESTERNO);
                objParams[3] = new SqlParameter("@P_IDNO", objStud.IDNOS);
                objParams[4] = new SqlParameter("@P_FINAL_DETEND", objStud.FINAL_DETEND);
                objParams[5] = new SqlParameter("@P_IPADDRESS", objStud.IPADDRESS);//IP FINAL
                objParams[6] = new SqlParameter("@P_UA_NO", objStud.UA_NO);//New Entity
                objParams[7] = new SqlParameter("@P_COLLEGE_CODE", objStud.COLLEGE_CODE);
                objParams[8] = new SqlParameter("@P_SUSPENSION_REASON", objStud.SUSPENSION_REASON);
                objParams[9] = new SqlParameter("@P_SUS_NUMBER", NUMBER);
                objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[10].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_FINAL_DETEND_UPDATE", objParams, false);

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.StudentRegistration.UpdateFinalDetend() --> " + ex.Message + " " + ex.StackTrace);
            }
            return retStatus;
        }

        public int UpdateCourseFinalDetend(StudentRegist objStud)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[11];
                objParams[0] = new SqlParameter("@P_SESSIONNO", objStud.SESSIONNO);
                objParams[1] = new SqlParameter("@P_SCHEMENO", objStud.SCHEMENO);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", objStud.SEMESTERNO);
                objParams[3] = new SqlParameter("@P_IDNO", objStud.IDNOS);
                objParams[4] = new SqlParameter("@P_FINAL_DETEND", objStud.FINAL_DETEND);
                objParams[5] = new SqlParameter("@P_IPADDRESS", objStud.IPADDRESS);
                objParams[6] = new SqlParameter("@P_UA_NO", objStud.UA_NO);
                objParams[7] = new SqlParameter("@P_COLLEGE_CODE", objStud.COLLEGE_CODE);
                objParams[8] = new SqlParameter("@P_COURSENO", objStud.COURSENO);
                objParams[9] = new SqlParameter("@P_SUSPENSION_REASON", objStud.SUSPENSION_REASON);

                objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[10].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_COURSE_FINAL_DETEND_UPDATE", objParams, false);

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.StudentRegistration.UpdateCourseFinalDetend() --> " + ex.Message + " " + ex.StackTrace);
            }
            return retStatus;
        }

        public DataSet Get_Final_Detain_StudentList(int sessionno, int semesterno, int college_id)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        {
                    new SqlParameter("@P_SESSIONNO", sessionno),
                    new SqlParameter("@P_SEMESTERNO", semesterno),
                    new SqlParameter("@P_COLLEGE_ID", college_id),
                    // new SqlParameter("@P_COURSENO", courseno)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_FINAL_DETENTION_STUDENT_LIST", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.WinAddIn.BusinessLogic.StudentController.Get_Final_Detain_StudentList() --> " + ex.Message);
            }
            return ds;
        }

        public DataSet GetSuspentionDataList(int sessionno, string Regno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_REGNO", Regno);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_SUSPENSTION_LIST_DATA", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistration.GetProvDetendInfo()-> " + ex.ToString());
            }

            return ds;
        }

        public int RemoveSuspensionStudent(int Sessionno, int idno, int CourseNo, int Semesterno, string Remark, int UaNo)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[7];
                objParams[0] = new SqlParameter("@P_SESSIONNO", Sessionno);
                objParams[1] = new SqlParameter("@P_IDNO", idno);
                objParams[2] = new SqlParameter("@P_COURSENO", CourseNo);
                objParams[3] = new SqlParameter("@P_SEMESTERNO", Semesterno);
                objParams[4] = new SqlParameter("@P_REMARK", Remark);
                objParams[5] = new SqlParameter("@P_UA_NO", UaNo);

                objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[6].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_COURSE_DEATIN_CANCEL_FLAG", objParams, false);

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.StudentRegistration.UpdateCourseFinalDetend() --> " + ex.Message + " " + ex.StackTrace);
            }
            return retStatus;
        }

        public DataSet GetProvDetendInfo(int sessionno, int schemeno, int semesterno, int courseno, int value, int College_id)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                SqlParameter[] objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[3] = new SqlParameter("@P_COURSENO", courseno);
                objParams[4] = new SqlParameter("@P_VALUE", value);
                objParams[5] = new SqlParameter("@P_COLLEGE_ID", College_id);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_STUD_SP_RET_DETEND_INFO", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistration.GetProvDetendInfo()-> " + ex.ToString());
            }

            return ds;
        }

        //ADDED BY AASHNA 16-08-2022

        public int InsertGeneralSuspension(StudentRegist objStud, string todate, string suspensiondate, int SUSNO)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[12];
                objParams[0] = new SqlParameter("@P_SESSIONNO", objStud.SESSIONNO);
                objParams[1] = new SqlParameter("@P_SCHEMENO", objStud.SCHEMENO);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", objStud.SEMESTERNO);
                objParams[3] = new SqlParameter("@P_IDNO", objStud.IDNOS);
                objParams[4] = new SqlParameter("@P_IPADDRESS", objStud.IPADDRESS);
                objParams[5] = new SqlParameter("@P_UA_NO", objStud.UA_NO);
                objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objStud.COLLEGE_CODE);
                objParams[7] = new SqlParameter("@P_SUSPENSION_REASON", objStud.SUSPENSION_REASON);
                objParams[8] = new SqlParameter("@P_TODATE", todate);
                objParams[9] = new SqlParameter("@P_SUSPENSION_DATE", suspensiondate);
                objParams[10] = new SqlParameter("@P_SUSPNO", SUSNO);
                objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[11].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_GENERAL_SUSPENSION", objParams, false);

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else if (Convert.ToInt32(ret) == -1001)
                    retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.StudentRegistration.UpdateCourseFinalDetend() --> " + ex.Message + " " + ex.StackTrace);
            }
            return retStatus;
        }

        #region "Exam Registration"

        public DataSet GetStudentAllExamSubjects(int idno, int Sessionno, int Schemeno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SESSIONNO", Sessionno);
                objParams[2] = new SqlParameter("@P_SCHEMENO", Schemeno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_EXAM_REGISTRATION_SUBJECTS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetStudentAllExamSubjects-> " + ex.ToString());
            }
            return ds;
        }

        #endregion "Exam Registration"
    }
}