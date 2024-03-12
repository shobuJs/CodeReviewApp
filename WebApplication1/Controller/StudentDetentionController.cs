using IITMS.SQLServer.SQLDAL;
using System;
using System.Data;
using System.Data.SqlClient;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class StudentDetentionController
            {
                private string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public DataSet GetStudentsForDetention(int session, int scheme, int courseno, int subid, int batch)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);

                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", scheme);
                        objParams[2] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[3] = new SqlParameter("@P_SUBID", subid);
                        objParams[4] = new SqlParameter("@P_BATCHNO", batch);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_SP_GET_STUD_DETENTION", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentDetentionController.GetStudentsForDetention-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetSchemeByCourse(int sessionno, int schemeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);

                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_DROPDOWN_SP_RET_COURSE_TEACHER_ALLOT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentDetentionController.GetSchemeByCourse-> " + ex.ToString());
                    }

                    return ds;
                }

                public int AddDetainedStudents(int sessionno, int ua_no, int schemeno, int courseno, string batchnos, string idnos, string tot_class, string attend_class, string tw_submit, string prov_detain, string college_code)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_connectionString);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_UA_NO", ua_no),
                            new SqlParameter("@P_SCHEMENO", schemeno),
                            new SqlParameter("@P_COURSENO", courseno),
                            //new SqlParameter("@P_CCODE", ccode),
                            //new SqlParameter("@P_SUBID", subid),
                            new SqlParameter("@P_IDNOS", idnos),
                            new SqlParameter("@P_BATCHNOS", batchnos),
                            new SqlParameter("@P_TOT_CLASS", tot_class),
                            new SqlParameter("@P_ATTEND_CLASS", attend_class),
                            new SqlParameter("@P_TW_SUBMIT", tw_submit),
                            new SqlParameter("@P_PROV_DETAIN", prov_detain),
                            new SqlParameter("@P_COLLEGE_CODE", college_code),
                            new SqlParameter("@P_OUT", SqlDbType.Int)
                        };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objDataAccess.ExecuteNonQuerySP("PKG_ACAD_SP_INS_DETAINED_STUDENTS", sqlParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentDetentionController.AddDetainedStudents-> " + ex.ToString());
                    }

                    return retStatus;
                }

                // for Detention Student Attendance Report
                //28/12/2009
                public DataSet GetDetentionStudent(int schemeNo, int courseNo, int subId, int batchNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SCHEMENO", schemeNo);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseNo);
                        objParams[2] = new SqlParameter("@P_SUBID", subId);
                        objParams[3] = new SqlParameter("@P_BATCHNO", batchNo);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_STUDENT_FOR_DETENTION", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentDetentionController.GetDetentionStudent-> " + ex.ToString());
                    }
                    return ds;
                }
            }
        }
    }
}