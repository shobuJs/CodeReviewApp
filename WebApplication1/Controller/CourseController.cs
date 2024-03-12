using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System;
using System.Data;
using System.Data.SqlClient;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            /// <summary>
            /// This CourseController is used to control Course table.
            /// </summary>
            public partial class CourseController
            {
                /// <summary>
                /// ConnectionStrings
                /// </summary>
                private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public CourseController()
                { }

                /// <summary>
                /// This method is used to add new course in the Course table.
                /// </summary>
                /// <param name="objCourse">objCourse is the object of Course class</param>
                /// <returns>Integer CustomStatus - Record Added or Error</returns>

                /// <summary>
                /// This method is used to update existing course from Course table.
                /// </summary>
                /// <param name="objCourse">objCourse is the object of Course class</param>
                /// <returns>Integer CustomStatus - Record Updated or Error</returns>

                /// <summary>
                /// This method is used to get courses according to scheme no.
                /// </summary>
                /// <param name="schemeno">Get courses as per this schemeno.</param>
                /// <returns>DataSet</returns>
                ///

                public DataSet GetCourseOffered(int schemeno, int semesterno, int to_Term, int sessionno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[1] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[2] = new SqlParameter("@P_TO_TERM", to_Term);
                        objParams[3] = new SqlParameter("@P_SESSIONNO", sessionno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_RET_OFFERED_COURSE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.GetStudentForFaculty-> " + ex.ToString());
                    }
                    return ds;
                }

                public int UpdateOfferedCourse(int SchemeNo, string offcourse, int sem, int sessionno, int userno, string offered_Status, int toTerm)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Edit Course
                        objParams = new SqlParameter[8];

                        objParams[0] = new SqlParameter("@P_SEMESTERNO", sem);
                        objParams[1] = new SqlParameter("@P_COURSENOS", offcourse);
                        objParams[2] = new SqlParameter("@P_SCHEMENO", SchemeNo);

                        objParams[3] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[4] = new SqlParameter("@P_OFFERED_STATUS", offered_Status);
                        objParams[5] = new SqlParameter("@P_UA_NO", userno);
                        objParams[6] = new SqlParameter("@P_TO_TERM", toTerm);
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_COURSE_SP_UPD_OFFERED_COURSE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.UpdateOfferedCourse -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public SqlDataReader GetSchemeSemesterByUser(int ua_idno)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UA_IDNO", ua_idno);
                        dr = objSQLHelper.ExecuteReaderSP("PKG_GET_SCHEME_SEMESTER_BY_USERID", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dr;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.GetShemeSemester-> " + ex.ToString());
                    }
                    return dr;
                }

                public int CopyCourseToNewScheme(int fromCourseNo, int fromSchemeNo, int toSchemeNo, int SemNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                            {
                                new SqlParameter("@P_FROMCOURSENO", fromCourseNo),
                                new SqlParameter("@P_FROMSCHEMENO", fromSchemeNo),

                                new SqlParameter("@P_TOSCHEMENO", toSchemeNo),
                                new SqlParameter("@P_TOSEMESTERNO", SemNo),

                                new SqlParameter("@P_OUT", SqlDbType.Int)
                            };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objDataAccess.ExecuteNonQuerySP("PROC_ACAD_COPY_COURSE_AND_INSERT", sqlParams, true);

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
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.CopyCourseToNewScheme-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int InsertTransaction(int feeitem, int Sessionno, string amount, int subjecttype, int feeitemtransid)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_FEEITEMID", feeitem);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", Sessionno);
                        objParams[2] = new SqlParameter("@P_AMOUNT", amount);
                        objParams[3] = new SqlParameter("@P_SUBTYPE", subjecttype);
                        objParams[4] = new SqlParameter("@P_FEEITEMTRANSID", feeitemtransid);
                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_UPDATE_FEES_TRANSACTION", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.AddCourse-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public SqlDataReader GetFeesDefinitionDetails(int FeeItemTransId)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_FeeItemTransId", FeeItemTransId);
                        dr = objSQLHelper.ExecuteReaderSP("PKG_SP_RET_FEES_DEFINITION", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dr;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.GetSingleSession-> " + ex.ToString());
                    }
                    return dr;
                }

                public DataSet GetFeeDetails()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        ds = objSQLHelper.ExecuteDataSet("PKG_INSERT_FEES_TRANSACTION_REPORT");
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetFirstYearCoursesBySchemeNo-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetCoursesBySchemeNo(int schemeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SCHEMENO", schemeno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_RET_COURSE_BYSCHEMENO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetCoursesBySchemeNo-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetDocsByCourseNo(int courseno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COURSENO", courseno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_RET_COURSEDOCS_BYCOURSENO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetCoursesBySchemeNo-> " + ex.ToString());
                    }

                    return ds;
                }

                /// <summary>
                /// This method is used to get courses according to coursecode & schemeno.
                /// </summary>
                /// <param name="coursecode">Used to retrieve course of current coursecode</param>
                /// <param name="schemeno">Used to retrieve course of current schemeno</param>
                /// <returns>SqlDataReader</returns>
                public SqlDataReader GetCourses(int coursecode, int schemeno)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_COURSENO", coursecode);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);

                        dr = objSQLHelper.ExecuteReaderSP("PKG_COURSE_SP_RET_COURSE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetCourses-> " + ex.ToString());
                    }

                    return dr;
                }

                /// <summary>
                /// This method is used to get Scheme According to CCode,degree,semno,vtype.
                /// </summary>
                /// <param name="ccode">Used to get scheme as per this coursecode </param>
                /// <param name="degree">Used to get scheme as per this degree </param>
                /// <param name="semno">Used to get scheme as per this schemeno</param>
                /// <param name="vtype">Used to get type</param>
                /// <returns>DataSet</returns>
                public DataSet GetSchemeNoByCCode(string ccode, string degree, int semno, int batchno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_CCODE", ccode);
                        objParams[1] = new SqlParameter("@P_DEGREE", degree);
                        objParams[2] = new SqlParameter("@P_SEM_NO", semno);
                        objParams[3] = new SqlParameter("@P_BATCHNO", batchno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_SCHEME_SP_RET_GELE_BYCCODE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetSchemeNoByCCode-> " + ex.ToString());
                    }

                    return ds;
                }

                /// <summary>
                /// This method is used to get Courses According to Schemeno.
                /// </summary>
                /// <param name="SchemeNo">Used to get Courses as per this schemeNo </param>
                /// <returns>DataSet</returns>

                /// <summary>
                /// This method is used to get global scheme.
                /// </summary>
                /// <param name="ccode">Used to get global scheme as per this coursecode </param>
                /// <param name="degree">Used to get global scheme as per this degree </param>
                /// <param name="schemeno">Used to get global scheme as per this </param>
                /// <param name="sem_no">Used to get global scheme as per this  scheme</param>
                /// <param name="vtype">Used to get global scheme as per this type</param>
                /// <returns>DataSet</returns>
                public DataSet GetGElecScheme(string ccode, string degree, int schemeno, int sem_no, int batchno)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_CCODE", ccode);
                        objParams[1] = new SqlParameter("@P_DEGREE", degree);
                        objParams[2] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[3] = new SqlParameter("@P_SEM_NO", sem_no);
                        objParams[4] = new SqlParameter("@P_BATCHNO", batchno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_SCHEME_SP_RET_GELECOURSE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetGElecScheme-> " + ex.ToString());
                    }

                    return ds;
                }

                // FOR INSERT THE COURSE DETAILS 2 FEB 2014
                public int UpdateCoursedetails(string rollno, int courseno, int status, int session, int semepro, int UANO, int IDNO, string ipaddress, int oldsem, int oldsession, string oldstatus, string oldgrade, string newgrade)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                            //Parameters for MARKS
                            new SqlParameter("@P_ROLLNO", rollno),
                            new SqlParameter("@P_PREVSTATUS", status),
                            new SqlParameter("@P_SESSIONNO", session),
                            new SqlParameter("@P_SEMEPRO", semepro),
                            new SqlParameter("@P_COURSENO", courseno),
                            new SqlParameter("@P_UANO", UANO),
                            new SqlParameter("@P_IDNO", IDNO),
                            new SqlParameter("@P_IPADDRESS", ipaddress),
                            new SqlParameter("@P_OLDSEM", oldsem),
                            new SqlParameter("@P_OLDSESSION", oldsession),
                            new SqlParameter("@P_OLDSTATUS", oldstatus),
                            new SqlParameter("@P_OLDGRADE", oldgrade),
                            new SqlParameter("@P_NEWGRADE", newgrade),
                            new SqlParameter("@P_OUT", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_STUDENT_COURSE_DETAILS", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.MarksEntryController.UpdateCoursedetails --> " + ex.ToString());
                    }
                    return retStatus;
                }

                //GET COURSE DETAILS IN LISVIEW 01/02/2014
                public DataSet GetCourseDetails(string rollno, int semesterno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_ROLLNO", rollno);
                        objParams[1] = new SqlParameter("@P_SEMESTERNO", semesterno);

                        ds = objSQLHelper.ExecuteDataSetSP("GET_STUDENT_COURSE_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetCourseDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetCourseAllotment(int session, int scheme, int semesterno, int Degreeno, int Branchno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", scheme);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[3] = new SqlParameter("@P_DEGREENO", Degreeno);
                        objParams[4] = new SqlParameter("@P_BRANCHNO", Branchno);
                        //  objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_RET_COURSE_ALLOT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetCourseAllotment-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetCourseAllotmentSectionwise(int session, int scheme, int semesterno, int Degreeno, int Branchno, int sectionno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", scheme);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[3] = new SqlParameter("@P_DEGREENO", Degreeno);
                        objParams[4] = new SqlParameter("@P_BRANCHNO", Branchno);
                        objParams[5] = new SqlParameter("@P_SECTIONNO", sectionno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_RET_COURSE_ALLOT_SECTIONWISE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetCourseAllotment-> " + ex.ToString());
                    }
                    return ds;
                }

                public int AddCourseAllot(Student_Acd objStudent)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", objStudent.SessionNo);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", objStudent.SchemeNo);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", objStudent.Sem);
                        objParams[3] = new SqlParameter("@P_SECTIONNO", objStudent.Sectionno);
                        objParams[4] = new SqlParameter("@P_UA_NO", objStudent.UA_No);
                        objParams[5] = new SqlParameter("@P_ADTEACHER", objStudent.AdTeacher);
                        objParams[6] = new SqlParameter("@P_SUBID", objStudent.Pract_Theory);
                        objParams[7] = new SqlParameter("@P_COURSENO", objStudent.CourseNo);
                        objParams[8] = new SqlParameter("@P_TH_PR", objStudent.Th_Pr);
                        //objParams[9] = new SqlParameter("@P_ALLOWMARKENTRY", allowmarkentry);
                        objParams[9] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_COURSE_SP_INS_COURSEBYALLOTMENT", objParams, true);
                        if (ret != null)
                            retStatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.AddCourseAllot-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int DeleteCourseAllot(Student_Acd objStudent)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Edit Course
                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", objStudent.SessionNo);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", objStudent.SchemeNo);
                        objParams[2] = new SqlParameter("@P_UA_NO", objStudent.UA_No);
                        objParams[3] = new SqlParameter("@P_COURSENO", objStudent.CourseNo);
                        objParams[4] = new SqlParameter("@P_SUBID", objStudent.sub_id);
                        objParams[5] = new SqlParameter("@P_TH_PR", objStudent.Th_Pr);
                        objParams[6] = new SqlParameter("@P_SECTIONNO", objStudent.Sectionno);
                        objParams[7] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_COURSE_SP_DEL_COURSEALLOT", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.DeleteCourseAllot -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetAllCourse(int schemeno, string coursenos)//, int semesterno
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];

                        objParams[0] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[1] = new SqlParameter("@P_COURSENOS", coursenos);
                        //objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_ALL_COURSE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetAllCourse-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetCourseOffered(int schemeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SCHEMENO", schemeno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_RET_OFFERED_COURSE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.GetStudentForFaculty-> " + ex.ToString());
                    }
                    return ds;
                }

                public int UpdateOfferedCourse(int SchemeNo, string offcourse, string sem, string sqno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Edit Course
                        objParams = new SqlParameter[5];

                        objParams[0] = new SqlParameter("@P_SEMESTERNOs", sem);
                        objParams[1] = new SqlParameter("@P_COURSENOS", offcourse);
                        objParams[2] = new SqlParameter("@P_SCHEMENO", SchemeNo);
                        objParams[3] = new SqlParameter("@P_SEQNOS", sqno);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_COURSE_SP_UPD_OFFERED_COURSE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.UpdateOfferedCourse -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int UpdateOfferedCourseTermScheme(int SessionNo, int SchemeNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Edit Course
                        objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", SessionNo);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", SchemeNo);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_COURSE_SP_INS_OFFERED_COURSE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.UpdateOfferedCourseTermScheme -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public String GetCourseByCourseno(string coursenos, int schemeno)
                {
                    String mCCode = string.Empty;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];

                        objParams[0] = new SqlParameter("@P_COURSENO", coursenos);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);

                        mCCode = objSQLHelper.ExecuteScalarSP("PKG_COURSE_SP_RET_COURSE_BY_COURSENO", objParams).ToString();
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetCourseByCourseno-> " + ex.ToString());
                    }
                    return mCCode;
                }

                public DataSet GetDeptWiseCourse(int deptno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_DEPTNO", deptno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_RPT_OFFERED_COURSES", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetDeptWiseCourse-> " + ex.ToString());
                    }
                    return ds;
                }

                public String GetCCodeByCourseno(int courseno)
                {
                    String ccode = string.Empty;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];

                        objParams[0] = new SqlParameter("@P_COURSENO", courseno);

                        ccode = objSQLHelper.ExecuteScalarSP("PKG_COURSE_SP_RET_CCODE_BY_COURSENO", objParams).ToString();
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetCCodeByCourseno-> " + ex.ToString());
                    }
                    return ccode;
                }

                public int AddAuditCourse(int idno, int schemeno, string regno, int courseno, int credit, string ccode, string term, bool detained)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add New Audit Course
                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_REGNO", regno);
                        objParams[2] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[3] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[4] = new SqlParameter("@P_CCODE", ccode);
                        objParams[5] = new SqlParameter("@P_CREDIT", credit);
                        objParams[6] = new SqlParameter("@P_TERM", term);
                        objParams[7] = new SqlParameter("@P_DETAINED", detained);

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_COURSE_SP_INS_AUDITCOURSE_REG", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.AddAuditCourse-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetFirstYearCoursesBySchemeNo(int schemeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SCHEMENO", schemeno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PREREGIST_SP_RET_COURSEOFFERED_FY", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetFirstYearCoursesBySchemeNo-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetCourseForCourseAllotment(int schemeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SCHEMENO", schemeno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_RET_COURSE_FOR_CA", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetCourseForCourseAllotment-> " + ex.ToString());
                    }

                    return ds;
                }

                public SqlDataReader GetCourseAllot(int courseNo)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COURSENO", courseNo);

                        dr = objSQLHelper.ExecuteReaderSP("PKG_COURSE_SP_RET_COURSE_TEACHER_ALLOT_BY_COURSENO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dr;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetCourseAllot-> " + ex.ToString());
                    }
                    return dr;
                }

                //Method Added for course level master on 05/03/2010
                public DataSet GetCoursesByLevel(int levelno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_LEVELNO", levelno),
                        };

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_GET_COURSE_BY_LEVEL", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetCoursesByLevel-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataTableReader GetCourseByCourseNo(int courseno)
                {
                    DataTableReader dtr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_COURSENO", courseno),
                        };

                        DataSet ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_RET_COURSE_BY_COURSENO", sqlParams);
                        if (ds != null && ds.Tables[0].Rows.Count > 0)
                            dtr = ds.Tables[0].CreateDataReader();
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetCourseByCCode-> " + ex.ToString());
                    }

                    return dtr;
                }

                public DataSet GetLevelName()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSH = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];

                        ds = objSH.ExecuteDataSetSP("PKG_ACAD_GET_LEVEL_NAME", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseControlle.GetLevelName-> " + ex.ToString());
                    }
                    return ds;
                }

                public int AddLevel(string leveldsc, int admbatch, int nocourse, int cpth, int cppr, int thmarks, int prmarks, string collegeCode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_LEVEL_DESC",leveldsc ),
                            new SqlParameter("@P_ADMBATCH", admbatch),
                            new SqlParameter("@P_NO_COURSES", nocourse),
                            new SqlParameter("@P_CP_TH",cpth),
                            new SqlParameter("@P_CP_PR", cppr),
                            new SqlParameter("@P_MARKS_TH",thmarks),
                            new SqlParameter("@P_MARKS_PR",prmarks),
                            new SqlParameter ("@P_COLLEGE_CODE",collegeCode),
                            new SqlParameter("@P_LEVELNO", SqlDbType.Int),
                        };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("[PKG_ACAD_INS_LEVEL]", sqlParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == -1001)
                            retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.AddLevel-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int UpdateLevel(int levelno, string leveldsc, int admbatch, int nocourse, int cpth, int cppr, int thmarks, int prmarks)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_LEVELNO", levelno);
                        objParams[1] = new SqlParameter("@P_LEVEL_DESC", leveldsc);
                        objParams[2] = new SqlParameter("@P_ADMBATCH", admbatch);
                        objParams[3] = new SqlParameter("@P_NO_COURSES", nocourse);
                        objParams[4] = new SqlParameter("@P_CP_TH", cpth);
                        objParams[5] = new SqlParameter("@P_CP_PR", cppr);
                        objParams[6] = new SqlParameter("@P_MARKS_TH", thmarks);
                        objParams[7] = new SqlParameter("@P_MARKS_PR", prmarks);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_UPD_LEVEL", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.UpdateLevel-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public SqlDataReader GetLevelNo(int levelno)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_LEVELNO", levelno);

                        dr = objSQLHelper.ExecuteReaderSP("PKG_ACAD_GET_LEVEL_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dr;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetLevelNo-> " + ex.ToString());
                    }
                    return dr;
                }

                public int UpdateExamMarks(Course objCourse)
                {
                    int retun_status = Convert.ToInt16(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[13];

                        objParams[0] = new SqlParameter("@P_S1MAX", objCourse.S1Max);
                        objParams[1] = new SqlParameter("@P_S2MAX", objCourse.S2Max);
                        objParams[2] = new SqlParameter("@P_S3MAX", objCourse.S3Max);
                        objParams[3] = new SqlParameter("@P_S4MAX", objCourse.S4Max);
                        objParams[4] = new SqlParameter("@P_S5MAX", objCourse.S5Max);
                        objParams[5] = new SqlParameter("@P_EXTERMARKMAX", objCourse.ExtermarkMax);

                        objParams[6] = new SqlParameter("@P_S1Min", objCourse.S1Min);
                        objParams[7] = new SqlParameter("@P_S2Min", objCourse.S2Min);
                        objParams[8] = new SqlParameter("@P_S3Min", objCourse.S3Min);
                        objParams[9] = new SqlParameter("@P_S4Min", objCourse.S4Min);
                        objParams[10] = new SqlParameter("@P_S5Min", objCourse.S5Min);
                        objParams[11] = new SqlParameter("@P_EXTERMARKMIN", objCourse.ExtermarkMin);

                        objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[12].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_UPDATE_EXAMMARKS", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retun_status = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == -1001)
                            retun_status = Convert.ToInt32(CustomStatus.DuplicateRecord);
                        else
                            retun_status = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retun_status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.UpdateExamMarks-> " + ex.ToString());
                    }

                    return retun_status;
                }

                /// <summary>
                /// This method is used to get courses marks
                /// </summary>
                /// <param name="schemeno">Get courses marks as per courseno.</param>
                /// <returns>DataSet</returns>
                public DataSet GetCoursesMarks(int courseno, int PatternNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_COURSENO", courseno);

                        objParams[1] = new SqlParameter("@P_PATTERNNO", PatternNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_EXAMNAME_COURSEMARKS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetCoursesMarks-> " + ex.ToString());
                    }

                    return ds;
                }

                //Bulk Lock unlock-testmark
                public DataSet GetCoursesForLockUnlock(int CollegeID, int SessionNo, int degreeno, int branchno, int schemeno, int semesterno, int sectionno, int studenttype, int Int_Ext)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_COLLEGE_ID", CollegeID);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", SessionNo);
                        objParams[2] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[3] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[4] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[5] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[6] = new SqlParameter("@P_PREV_STATUS", studenttype);
                        objParams[7] = new SqlParameter("@P_EXAMNO", Int_Ext);
                        objParams[8] = new SqlParameter("@P_SECTIONNO", sectionno);
                        //objParams[3] = new SqlParameter("@P_OP_ID", OP_ID);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_COURSES_BY_SCHEME_AND_OPERATOR", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.GetCoursesForLockUnlock-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetCoursesForLockUnlockStudentwise(int Sessionno, int examtype, int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", Sessionno);
                        objParams[1] = new SqlParameter("@P_EXAMNO", examtype);
                        objParams[2] = new SqlParameter("@P_IDNO", idno);

                        //objParams[3] = new SqlParameter("@P_OP_ID", OP_ID);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_COURSES_BY_STUDENT_LOCKUNLOCK", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.GetCoursesForLockUnlock-> " + ex.ToString());
                    }

                    return ds;
                }

                //ADD COURSE FOR THE PHD

                public int AddNewPhdCourse(Course objCourse)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add New Course
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_COURSE_NAME", objCourse.CourseName);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", objCourse.SchemeNo);
                        objParams[2] = new SqlParameter("@P_CCODE", objCourse.CCode);
                        objParams[3] = new SqlParameter("@P_SUBID", objCourse.SubID);
                        objParams[4] = new SqlParameter("@P_CREDITS", objCourse.Credits);
                        objParams[5] = new SqlParameter("@P_COLLEGE_CODE", objCourse.CollegeCode);
                        //objParams[42] = new SqlParameter("@P_SEMESTERNO", objCourse.SemesterNo);
                        objParams[6] = new SqlParameter("@P_COURSE_NO", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PHD_COURSE_SP_INS_ADD_NEW_COURSE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.AddCourse-> " + ex.ToString());
                    }

                    return retStatus;
                }

                #region Paper Setter

                /// <summary>
                /// This method is used to update the quantity of paper set & its reorder level
                /// </summary>
                /// <param name="ccode">ccode</param>
                /// <param name="bos_deptno">Board of studies Department</param>
                /// <param name="qty">qty of paper set</param>
                /// <param name="req">req</param>
                /// <returns>int</returns>
                public int UpdateCourseQTY(string ccode, int bos_deptno, int semesterno, int qty, int req)
                {
                    int retun_status = Convert.ToInt16(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[4];

                        objParams[0] = new SqlParameter("@P_CCODE", ccode);
                        objParams[1] = new SqlParameter("@P_BOS_DEPTNO", bos_deptno);
                        //objParams[2] = new SqlParameter("@P_SEMSTERNO", semesterno);
                        objParams[2] = new SqlParameter("@P_QTY", qty);
                        objParams[3] = new SqlParameter("@P_REORDER", req);

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_COURSE_QTY_REQ", objParams, true);
                        retun_status = Convert.ToInt16(ret);
                    }
                    catch (Exception ex)
                    {
                        retun_status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.UpdateCourseQTY-> " + ex.ToString());
                    }

                    return retun_status;
                }

                /// <summary>
                /// This method is used to apply for new paper set
                /// </summary>
                /// <param name="sessionno">sessionno</param>
                /// <param name="bos_deptno">Board of studies Department</param>
                /// <param name="semesterno">semesterno</param>
                /// <param name="ccode">ccode</param>
                /// <returns>int</returns>
                public int UpdateCourseBal(int sessionno, int bos_deptno, int semesterno, string ccode)
                {
                    int retun_status = Convert.ToInt16(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[4];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_BOS_DEPTNO", bos_deptno);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterno);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_PAPER_SET_DETAILS_INSERT", objParams, false) != null)
                            retun_status = Convert.ToInt16(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retun_status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.UpdateCourseBal-> " + ex.ToString());
                    }

                    return retun_status;
                }

                /// <summary>
                /// This method is used to delete the request
                /// </summary>
                /// <param name="sessionno">sessionno</param>
                /// <param name="bos_deptno">Board of studies Department</param>
                /// <param name="semesterno">semesterno</param>
                /// <param name="ccode">ccode</param>
                /// <returns>int</returns>
                public int DeleteCourseBal(int sessionno, int bos_deptno, int semesterno, string ccode)
                {
                    int retun_status = Convert.ToInt16(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[4];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_BOS_DEPTNO", bos_deptno);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterno);

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_PAPER_SET_DETAILS_DELETE", objParams, true);
                        retun_status = Convert.ToInt16(ret);
                    }
                    catch (Exception ex)
                    {
                        retun_status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.DeleteCourseBal-> " + ex.ToString());
                    }

                    return retun_status;
                }

                /// <summary>
                /// This method is used to add the paper set faculty by BOS
                /// </summary>
                /// <param name="sessionno">sessionno</param>
                /// <param name="bos_deptno">Board of studies Department</param>
                /// <param name="semesterno">semesterno</param>
                /// <param name="ccode">ccode</param>
                /// <param name="faculty1">faculty1</param>
                /// <param name="faculty2">faculty2</param>
                /// <param name="bos_lock">Lock status set by BOS(one time)</param>
                /// <returns>int</returns>
                public int AddPaperSetFaculty(int sessionno, int bos_deptno, int semesterno, string ccode, int faculty1, int qt1, int moi1, int faculty2, int qt2, int moi2, int faculty3, int qt3, int moi3, bool bos_lock)
                {
                    int retun_status = Convert.ToInt16(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[14];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_BOS_DEPTNO", bos_deptno);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[4] = new SqlParameter("@P_FACULTY1", faculty1);
                        objParams[5] = new SqlParameter("@P_QT1", qt1);
                        objParams[6] = new SqlParameter("@P_MOI1", moi1);
                        objParams[7] = new SqlParameter("@P_FACULTY2", faculty2);
                        objParams[8] = new SqlParameter("@P_QT2", qt2);
                        objParams[9] = new SqlParameter("@P_MOI2", moi2);
                        objParams[10] = new SqlParameter("@P_FACULTY3", faculty3);
                        objParams[11] = new SqlParameter("@P_QT3", qt3);
                        objParams[12] = new SqlParameter("@P_MOI3", moi3);
                        objParams[13] = new SqlParameter("@P_BOS_LOCK", bos_lock);

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_ADD_PAPERSET_FACULTY", objParams, true);
                        retun_status = Convert.ToInt16(ret);
                    }
                    catch (Exception ex)
                    {
                        retun_status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.AddPaperSetFaculty-> " + ex.ToString());
                    }

                    return retun_status;
                }

                /// <summary>
                /// This method is used to accept/change the paper set faculty by DEAN
                /// </summary>
                /// <param name="sessionno">sessionno</param>
                /// <param name="bos_deptno">Board of studies Department</param>
                /// <param name="semesterno">semesterno</param>
                /// <param name="ccode">ccode</param>
                /// <param name="faculty1">faculty1</param>
                /// <param name="faculty2">faculty2</param>
                /// <param name="accept">accept set by DEAN false if not accepted</param>
                /// <param name="bos_lock">Lock status set by DEAN(one time)</param>
                /// <returns>int</returns>
                /// <summary>
                public int AcceptPaperSetFaculty(int sessionno, int bos_deptno, int semesterno, string ccode, int fac_num, int accept, int qty, int moi, bool dean_lock)
                {
                    int retun_status = Convert.ToInt16(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[9];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_BOS_DEPTNO", bos_deptno);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[4] = new SqlParameter("@P_FACULTY_NUM", fac_num);
                        objParams[5] = new SqlParameter("@P_ACCEPT", accept);
                        objParams[6] = new SqlParameter("@P_QTY", qty);
                        objParams[7] = new SqlParameter("@P_MOI", moi);
                        objParams[8] = new SqlParameter("@P_DEAN_LOCK", dean_lock);

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_ACCEPT_PAPERSET_FACULTY", objParams, true);
                        retun_status = Convert.ToInt16(ret);
                    }
                    catch (Exception ex)
                    {
                        retun_status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.AcceptPaperSetFaculty-> " + ex.ToString());
                    }

                    return retun_status;
                }

                /// <summary>
                /// <summary>
                /// This method is used to accept the paper set faculty receive status
                /// </summary>
                /// <param name="sessionno">sessionno</param>
                /// <param name="bos_deptno">Board of studies Department</param>
                /// <param name="semesterno">semesterno</param>
                /// <param name="ccode">ccode</param>
                /// <param name="faculty">faculty no </param>
                /// <returns>int</returns>
                /// <summary>
                public int AddPaperSetReceivedStatus(int sessionno, int bos_deptno, int semesterno, int qt_rcvd, int moi_rcvd, int rcvd, string ccode, int faculty)
                {
                    int retun_status = Convert.ToInt16(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[8];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_BOS_DEPTNO", bos_deptno);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[4] = new SqlParameter("@P_QTYRCVD", qt_rcvd);
                        objParams[5] = new SqlParameter("@P_MOIRCVD", moi_rcvd);
                        objParams[6] = new SqlParameter("@P_RCVD", rcvd);
                        objParams[7] = new SqlParameter("@P_STAFFNO", faculty);

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_PAPERSET_RECEIVE_STATUS", objParams, true);
                        retun_status = Convert.ToInt16(ret);
                    }
                    catch (Exception ex)
                    {
                        retun_status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.AcceptPaperSetFaculty-> " + ex.ToString());
                    }

                    return retun_status;
                }

                /// <summary>
                /// This method is used to cancel the paper set entry and insert new
                /// </summary>
                /// <param name="sessionno">sessionno</param>
                /// <param name="bos_deptno">Board of studies Department</param>
                /// <param name="semesterno">semesterno</param>
                /// <param name="ccode">ccode</param>
                /// <param name="faculty">faculty no </param>
                /// <returns>int</returns>
                /// <summary>
                public int CancelPaperSetEntry(int sessionno, int bos_deptno, int semesterno, string ccode, int faculty)
                {
                    int retun_status = Convert.ToInt16(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[5];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_BOS_DEPTNO", bos_deptno);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[4] = new SqlParameter("@P_STAFFNO", faculty);

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_PAPERSET_CANCEL_STATUS", objParams, true);
                        retun_status = Convert.ToInt16(ret);
                    }
                    catch (Exception ex)
                    {
                        retun_status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.CancelPaperSetEntry-> " + ex.ToString());
                    }

                    return retun_status;
                }

                #endregion Paper Setter

                // Get Scheme, Semester, Photo by user idno
                public SqlDataReader GetShemeSemesterByUser(int ua_idno)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UA_IDNO", ua_idno);
                        dr = objSQLHelper.ExecuteReaderSP("PKG_GET_SCHEME_SEMESTER_BY_USERID", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dr;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.GetShemeSemester-> " + ex.ToString());
                    }
                    return dr;
                }

                //GET COURSE DETAILS OF FACULTY

                public DataSet GetCourseOfUanoDetails(int uano, int sessionno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_UANO", uano);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);

                        ds = objSQLHelper.ExecuteDataSetSP("GET_COURSES_OF_FACULTY", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetCourseDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public int InsertEquivalenceCourses(int idno, int oldschemeno, int newschemeno, int oldcourseno, int newcourseno, string oldccode, string newccode, int sessionno, string ip, string colcode, int uano)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                            {
                                new SqlParameter("@P_IDNO", idno),
                                new SqlParameter("@P_OLDSCHEMENO", oldschemeno),
                                new SqlParameter("@P_NEWSCHEMENO", newschemeno),
                                new SqlParameter("@P_OLDCOURSENO", oldcourseno),
                                new SqlParameter("@P_NEWCOURSENO", newcourseno),
                                new SqlParameter("@P_OLDCCODE", oldccode),
                                new SqlParameter("@P_NEWCCODE", newccode),
                                new SqlParameter("@P_SESSIONNO", sessionno),
                                new SqlParameter("@P_IPADDRESS", ip),
                                new SqlParameter("@P_COLLEGE_CODE", colcode),
                                new SqlParameter("@P_UA_NO", uano),
                                new SqlParameter("@P_OP", SqlDbType.Int)
                            };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objDataAccess.ExecuteNonQuerySP("PKG_INS_EQUIVALENCE_COURSES", sqlParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (Convert.ToInt32(ret) == -5)
                            retStatus = Convert.ToInt32(CustomStatus.RecordNotFound); //old course reg record not found
                        else
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.CopyCourseToNewScheme-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int InsertResultRulesDefinition(int schemeno, int subtype, string txtFinalCie, string txtFinalEse, string txtFinalCiePer, string txtFinalEsePer, string PassingCriteria)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[8];

                        objParams[0] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[1] = new SqlParameter("@P_SUBJECTTYPE", subtype);
                        objParams[2] = new SqlParameter("@P_CIE_SCALE", txtFinalCie);
                        objParams[3] = new SqlParameter("@P_ESE_SCALE", txtFinalEse);
                        objParams[4] = new SqlParameter("@P_CIE_PER", txtFinalCiePer);
                        objParams[5] = new SqlParameter("@P_ESE_PER", txtFinalEsePer);
                        objParams[6] = new SqlParameter("@P_PASSINGCRITERIA", PassingCriteria);
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_UPDATE_RESULT_RULE_ENGINE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.AddCourse-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetRuleEngineForScheme(int schemeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];

                        //Add New ExamName
                        //objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SCHEMENO", schemeno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_RESULT_RULE_ENGINE_EXAMINATION_DATA", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.GetGradeSchemeWise-> " + ex.ToString());
                    }
                    return ds;
                }

                //Added on 25-03-2019 For Security Patches

                //Added By PRiTiSH
                public DataSet GetAllAlert()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_ALERT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetAllAlert-> " + ex.ToString());
                    }
                    return ds;
                }

                //Added By PRiTiSH
                public int SetAlertType(int alertno, int sendthrough, int confirmalert, string uano, string user, string ip)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_ALERTSNO", alertno);
                        objParams[1] = new SqlParameter("@P_SEND_THROUGH", sendthrough);
                        objParams[2] = new SqlParameter("@P_CONFIRM_ALERT", confirmalert);
                        objParams[3] = new SqlParameter("@P_UA_NO", uano);
                        objParams[4] = new SqlParameter("@P_MODIFIEDBY", user);
                        objParams[5] = new SqlParameter("@IP_ADDRESS", ip);
                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteDataSetSP("PKG_ACD_APP_CONFIG", objParams);
                        if (ret != null)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.SetAlertType-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //Added By PRiTiSH on 01/03/2019
                public DataSet GetAllAlertInfo()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ALL_ALERTS_INFO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetAllAlertInfo-> " + ex.ToString());
                    }
                    return ds;
                }

                //Added By PRiTiSH on 01/03/2019
                public SqlDataReader GetAlertType(int alertno, int AlertTh)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_ALERTSNO", alertno);
                        objParams[1] = new SqlParameter("@P_SEND_THROUGH", AlertTh);

                        dr = objSQLHelper.ExecuteReaderSP("PKG_ACAD_SINGLE_ALERT_INFO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dr;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetAlertType-> " + ex.ToString());
                    }
                    return dr;
                }

                // End Security Patches Controllers

                // Added By Bhushan Patil On 11052019.
                public int UpdateOfferedCourse_Session_Wise(int SchemeNo, string offcourse, int sem, int sessionno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Edit Course
                        objParams = new SqlParameter[5];

                        objParams[0] = new SqlParameter("@P_SEMESTERNO", sem);
                        objParams[1] = new SqlParameter("@P_COURSENOS", offcourse);
                        objParams[2] = new SqlParameter("@P_SCHEMENO", SchemeNo);
                        objParams[3] = new SqlParameter("@P_SESSIONNO", sessionno);

                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_COURSE_SP_UPD_OFFERED_COURSE_SESSION_WISE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.UpdateOfferedCourse -> " + ex.ToString());
                    }

                    return retStatus;
                }

                // Added By Bhushan Patil On 11052019.

                public DataSet GetCourseOffered_Sessionwise(int schemeno, int semesterno, int sessionno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[1] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[2] = new SqlParameter("@P_SESSIONNO", sessionno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_RET_OFFERED_COURSE_SESSION_WISE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.GetStudentForFaculty-> " + ex.ToString());
                    }
                    return ds;
                }

                // Added By Bhushan Patil On 11052019.

                public int CheckOfferedCourse_Session(int sessionno, int courseno, int sem)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        //objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", sem);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_CHECK_OFFERED_COURSE_SESSION", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordFound);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordNotFound);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.CheckOfferedCourse-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetMandatoryCourseOffered(int schemeno, int semesterno, int sessionno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[1] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[2] = new SqlParameter("@P_SESSIONNO", sessionno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_MANDATORY_OFFERED_COURSE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.GetStudentForFaculty-> " + ex.ToString());
                    }
                    return ds;
                }

                public int UpdateMandatoryOfferedCourse(int SchemeNo, string offcourse, int sem, int sessionno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Edit Course
                        objParams = new SqlParameter[5];

                        objParams[0] = new SqlParameter("@P_SEMESTERNO", sem);
                        objParams[1] = new SqlParameter("@P_COURSENOS", offcourse);
                        objParams[2] = new SqlParameter("@P_SCHEMENO", SchemeNo);
                        objParams[3] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_COURSE_SP_UPD_MANDATORY_OFFERED_COURSE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.UpdateOfferedCourse -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int AcceptPaperSetFaculty(int sessionno, int bos_deptno, int semesterno, string ccode, int fac_num, string accept, int qty, int moi, bool dean_lock, string LastSubmitDate)
                {
                    int retun_status = Convert.ToInt16(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[10];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_BOS_DEPTNO", bos_deptno);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[4] = new SqlParameter("@P_FACULTY_NUM", fac_num);
                        objParams[5] = new SqlParameter("@P_ACCEPT", accept);
                        objParams[6] = new SqlParameter("@P_QTY", qty);
                        objParams[7] = new SqlParameter("@P_MOI", moi);
                        objParams[8] = new SqlParameter("@P_DEAN_LOCK", dean_lock);
                        objParams[9] = new SqlParameter("@P_LAST_SUBMIT_DATE", LastSubmitDate);

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_ACCEPT_PAPERSET_FACULTY", objParams, true);
                        retun_status = Convert.ToInt16(ret);
                    }
                    catch (Exception ex)
                    {
                        retun_status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.AcceptPaperSetFaculty-> " + ex.ToString());
                    }

                    return retun_status;
                }

                public int UpdateCourse(Course objCourse)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Edit Course
                        objParams = new SqlParameter[69];

                        objParams[0] = new SqlParameter("@P_SCHNO", objCourse.SchNo);
                        objParams[1] = new SqlParameter("@P_DELSCHNO", objCourse.DelSchNo);
                        objParams[2] = new SqlParameter("@P_SCHEMENO", objCourse.SchemeNo);
                        objParams[3] = new SqlParameter("@P_CCODE", objCourse.CCode);
                        objParams[4] = new SqlParameter("@P_COURSE_NAME", objCourse.CourseName);
                        objParams[5] = new SqlParameter("@P_SUBID", objCourse.SubID);
                        objParams[6] = new SqlParameter("@P_ELECT", objCourse.Elect);
                        objParams[7] = new SqlParameter("@P_GLOBALELE", objCourse.GlobalEle);
                        objParams[8] = new SqlParameter("@P_CREDITS", objCourse.Credits);
                        objParams[9] = new SqlParameter("@P_LECTURE", objCourse.Lecture);
                        objParams[10] = new SqlParameter("@P_THEORY", objCourse.Theory);
                        objParams[11] = new SqlParameter("@P_PRACTICAL", objCourse.Practical);
                        objParams[12] = new SqlParameter("@P_MAXMARKS_I", objCourse.MaxMarks_I);
                        objParams[13] = new SqlParameter("@P_MAXMARKS_E", objCourse.ExtermarkMax);
                        objParams[14] = new SqlParameter("@P_MINMARKS", objCourse.ExtermarkMin);
                        objParams[15] = new SqlParameter("@P_S1MAX", objCourse.S1Max);
                        objParams[16] = new SqlParameter("@P_S2MAX", objCourse.S2Max);
                        objParams[17] = new SqlParameter("@P_S3MAX", objCourse.S3Max);
                        objParams[18] = new SqlParameter("@P_S4MAX", objCourse.S4Max);
                        objParams[19] = new SqlParameter("@P_S5MAX", objCourse.S5Max);
                        objParams[20] = new SqlParameter("@P_S6MAX", objCourse.S6Max);
                        objParams[21] = new SqlParameter("@P_S7MAX", objCourse.S7Max);
                        objParams[22] = new SqlParameter("@P_S8MAX", objCourse.S8Max);
                        objParams[23] = new SqlParameter("@P_S9MAX", objCourse.S9Max);
                        objParams[24] = new SqlParameter("@P_S10MAX", objCourse.S10Max);
                        objParams[25] = new SqlParameter("@P_S1Min", objCourse.S1Min);
                        objParams[26] = new SqlParameter("@P_S2Min", objCourse.S2Min);
                        objParams[27] = new SqlParameter("@P_S3Min", objCourse.S3Min);
                        objParams[28] = new SqlParameter("@P_S4Min", objCourse.S4Min);
                        objParams[29] = new SqlParameter("@P_S5Min", objCourse.S5Min);
                        objParams[30] = new SqlParameter("@P_S6Min", objCourse.S6Min);
                        objParams[31] = new SqlParameter("@P_S7Min", objCourse.S7Min);
                        objParams[32] = new SqlParameter("@P_S8Min", objCourse.S8Min);
                        objParams[33] = new SqlParameter("@P_S9Min", objCourse.S9Min);
                        objParams[34] = new SqlParameter("@P_S10Min", objCourse.S10Min);
                        objParams[35] = new SqlParameter("@P_GRADE", objCourse.Grade);
                        objParams[36] = new SqlParameter("@P_MINGRADE", objCourse.MinGrade);
                        objParams[37] = new SqlParameter("@P_ASSIGNMAX", objCourse.AssignMax);
                        objParams[38] = new SqlParameter("@P_LEVELNO", objCourse.Levelno);
                        objParams[39] = new SqlParameter("@P_GROUPNO", objCourse.Groupno);
                        objParams[40] = new SqlParameter("@P_CGROUPNO", objCourse.CGroupno);
                        objParams[41] = new SqlParameter("@P_PREREQUISITE", objCourse.Prerequisite);
                        objParams[42] = new SqlParameter("@P_PREREQUISITE_CREDIT", objCourse.Prerequisite_cr);
                        objParams[43] = new SqlParameter("@P_COLLEGE_CODE", objCourse.CollegeCode);
                        objParams[44] = new SqlParameter("@P_SEMESTERNO", objCourse.SemesterNo);
                        objParams[45] = new SqlParameter("@P_SPECIALISATIONNO", objCourse.Specialisation);
                        objParams[46] = new SqlParameter("@P_BOS_DEPTNO", objCourse.Deptno);
                        objParams[47] = new SqlParameter("@P_PAPER_HRS", objCourse.Paper_hrs);
                        objParams[48] = new SqlParameter("@P_SCALING", objCourse.Scaling);
                        objParams[49] = new SqlParameter("@P_CATEGORYNO", objCourse.Categoryno);
                        objParams[50] = new SqlParameter("@P_SEMESTER_HRS", objCourse.Semester_Hrs);

                        //modified by neha 20/06/19
                        objParams[51] = (objCourse.FileName != "") ? new SqlParameter("@P_FILE_NAME", objCourse.FileName) : new SqlParameter("@P_FILE_NAME", DBNull.Value);
                        objParams[52] = (objCourse.FilePath != "") ? new SqlParameter("@P_FILE_PATH", objCourse.FilePath) : new SqlParameter("@P_FILE_PATH", DBNull.Value);

                        //Modified by Pritish on 15/01/2021
                        objParams[53] = new SqlParameter("@P_LECTUREHOURS", objCourse.LectureHours);
                        objParams[54] = new SqlParameter("@P_MINLECTUREHOURS", objCourse.MinLectureHours);
                        objParams[55] = new SqlParameter("@P_PRACTICALHOURS", objCourse.PracticalHours);
                        objParams[56] = new SqlParameter("@P_MINPRACTICALHOURS", objCourse.MinPracticalHours);
                        objParams[57] = new SqlParameter("@P_CLINICALHOURS", objCourse.ClinicalHours);
                        objParams[58] = new SqlParameter("@P_MINCLINICALHOURS", objCourse.MinClinicalHours);
                        objParams[59] = new SqlParameter("@P_INTEGRATED_HOURS", objCourse.IntegratedHours);
                        objParams[60] = new SqlParameter("@P_MININTEGRATED_HOURS", objCourse.MinIntegratedHours);
                        objParams[61] = new SqlParameter("@P_TOTALHOURS", objCourse.TotalHours);

                        // ADDED BY NARESH BEERLA ON 03/03/2021
                        objParams[62] = new SqlParameter("@P_JOURNAL_CLUB", objCourse.Journalclub);
                        objParams[63] = new SqlParameter("@P_CASE_DISCUSSION", objCourse.Casediscussion);
                        objParams[64] = new SqlParameter("@P_GUEST_LECTURE", objCourse.Guestlecture);
                        objParams[65] = new SqlParameter("@P_POSTER_PRESENTATION", objCourse.Posterpresentation);
                        objParams[66] = new SqlParameter("@P_ORAL_PRESENTATION", objCourse.Oralpresentation);
                        objParams[67] = new SqlParameter("@P_PUBLICATION", objCourse.Publication);

                        objParams[68] = new SqlParameter("@P_COURSE_NO", objCourse.CourseNo);

                        objParams[68].Direction = ParameterDirection.InputOutput;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_COURSE_SP_UPD_COURSE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.UpdateCourse -> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int AddCourse(Course objCourse)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add New Course
                        objParams = new SqlParameter[67];
                        objParams[0] = new SqlParameter("@P_COURSE_NAME", objCourse.CourseName);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", objCourse.SchNo);
                        objParams[2] = new SqlParameter("@P_CCODE", objCourse.CCode);
                        objParams[3] = new SqlParameter("@P_SUBID", objCourse.SubID);
                        objParams[4] = new SqlParameter("@P_ELECT", objCourse.Elect);
                        objParams[5] = new SqlParameter("@P_GLOBALELE", objCourse.GlobalEle);
                        objParams[6] = new SqlParameter("@P_CREDITS", objCourse.Credits);
                        objParams[7] = new SqlParameter("@P_LECTURE", objCourse.Lecture);
                        objParams[8] = new SqlParameter("@P_THEORY", objCourse.Theory);
                        objParams[9] = new SqlParameter("@P_PRACTICAL", objCourse.Practical);
                        objParams[10] = new SqlParameter("@P_MAXMARKS_I", objCourse.MaxMarks_I);
                        objParams[11] = new SqlParameter("@P_MAXMARKS_E", objCourse.ExtermarkMax);
                        objParams[12] = new SqlParameter("@P_MINMARKS", objCourse.ExtermarkMin);
                        objParams[13] = new SqlParameter("@P_S1MAX", objCourse.S1Max);
                        objParams[14] = new SqlParameter("@P_S2MAX", objCourse.S2Max);
                        objParams[15] = new SqlParameter("@P_S3MAX", objCourse.S3Max);
                        objParams[16] = new SqlParameter("@P_S4MAX", objCourse.S4Max);
                        objParams[17] = new SqlParameter("@P_S5MAX", objCourse.S5Max);
                        objParams[18] = new SqlParameter("@P_S6MAX", objCourse.S6Max);
                        objParams[19] = new SqlParameter("@P_S7MAX", objCourse.S7Max);
                        objParams[20] = new SqlParameter("@P_S8MAX", objCourse.S8Max);
                        objParams[21] = new SqlParameter("@P_S9MAX", objCourse.S9Max);
                        objParams[22] = new SqlParameter("@P_S10MAX", objCourse.S10Max);
                        objParams[23] = new SqlParameter("@P_S1Min", objCourse.S1Min);
                        objParams[24] = new SqlParameter("@P_S2Min", objCourse.S2Min);
                        objParams[25] = new SqlParameter("@P_S3Min", objCourse.S3Min);
                        objParams[26] = new SqlParameter("@P_S4Min", objCourse.S4Min);
                        objParams[27] = new SqlParameter("@P_S5Min", objCourse.S5Min);
                        objParams[28] = new SqlParameter("@P_S6Min", objCourse.S6Min);
                        objParams[29] = new SqlParameter("@P_S7Min", objCourse.S7Min);
                        objParams[30] = new SqlParameter("@P_S8Min", objCourse.S8Min);
                        objParams[31] = new SqlParameter("@P_S9Min", objCourse.S9Min);
                        objParams[32] = new SqlParameter("@P_S10Min", objCourse.S10Min);
                        objParams[33] = new SqlParameter("@P_ASSIGNMAX", objCourse.AssignMax);
                        objParams[34] = new SqlParameter("@P_DRAWING", objCourse.Drawing);
                        objParams[35] = new SqlParameter("@P_LEVELNO", objCourse.Levelno);
                        objParams[36] = new SqlParameter("@P_GROUPNO", objCourse.Groupno);
                        objParams[37] = new SqlParameter("@P_PREREQUISITE", objCourse.Prerequisite);
                        objParams[38] = new SqlParameter("@P_PREREQUISITE_CREDIT", objCourse.Prerequisite_cr);
                        objParams[39] = new SqlParameter("@P_GRADE", objCourse.Grade);
                        objParams[40] = new SqlParameter("@P_MINGRADE", objCourse.MinGrade);
                        objParams[41] = new SqlParameter("@P_COLLEGE_CODE", objCourse.CollegeCode);
                        objParams[42] = new SqlParameter("@P_SPECIALISATIONNO", objCourse.Specialisation);
                        objParams[43] = new SqlParameter("@P_PAPER_HRS", objCourse.Paper_hrs);
                        objParams[44] = new SqlParameter("@P_CGROUPNO", objCourse.CGroupno);
                        objParams[45] = new SqlParameter("@P_SCALING", objCourse.Scaling);
                        objParams[46] = new SqlParameter("@P_CATEGORYNO", objCourse.Categoryno);
                        objParams[47] = new SqlParameter("@P_SEMESTER_HRS", objCourse.Semester_Hrs);
                        objParams[48] = new SqlParameter("@P_BOS_DEPTNO", objCourse.Deptno);

                        //modified by neha 20/06/19
                        objParams[49] = (objCourse.FileName != "") ? new SqlParameter("@P_FILE_NAME", objCourse.FileName) : new SqlParameter("@P_FILE_NAME", DBNull.Value);
                        objParams[50] = (objCourse.FilePath != "") ? new SqlParameter("@P_FILE_PATH", objCourse.FilePath) : new SqlParameter("@P_FILE_PATH", DBNull.Value);

                        //Modified by Pritish on 15/01/2021
                        objParams[51] = new SqlParameter("@P_LECTUREHOURS", objCourse.LectureHours);
                        objParams[52] = new SqlParameter("@P_MINLECTUREHOURS", objCourse.MinLectureHours);
                        objParams[53] = new SqlParameter("@P_PRACTICALHOURS", objCourse.PracticalHours);
                        objParams[54] = new SqlParameter("@P_MINPRACTICALHOURS", objCourse.MinPracticalHours);
                        objParams[55] = new SqlParameter("@P_CLINICALHOURS", objCourse.ClinicalHours);
                        objParams[56] = new SqlParameter("@P_MINCLINICALHOURS", objCourse.MinClinicalHours);
                        objParams[57] = new SqlParameter("@P_INTEGRATED_HOURS", objCourse.IntegratedHours);
                        objParams[58] = new SqlParameter("@P_MININTEGRATED_HOURS", objCourse.MinIntegratedHours);
                        objParams[59] = new SqlParameter("@P_TOTALHOURS", objCourse.TotalHours);

                        // ADDED BY NARESH BEERLA ON 03/03/2021
                        objParams[60] = new SqlParameter("@P_JOURNAL_CLUB", objCourse.Journalclub);
                        objParams[61] = new SqlParameter("@P_CASE_DISCUSSION", objCourse.Casediscussion);
                        objParams[62] = new SqlParameter("@P_GUEST_LECTURE", objCourse.Guestlecture);
                        objParams[63] = new SqlParameter("@P_POSTER_PRESENTATION", objCourse.Posterpresentation);
                        objParams[64] = new SqlParameter("@P_ORAL_PRESENTATION", objCourse.Oralpresentation);
                        objParams[65] = new SqlParameter("@P_PUBLICATION", objCourse.Publication);

                        objParams[66] = new SqlParameter("@P_COURSE_NO", SqlDbType.Int);
                        objParams[66].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_COURSE_SP_INS_COURSE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.AddCourse-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int AddCourseC(Course objCourse)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add New Course
                        objParams = new SqlParameter[50];
                        objParams[0] = new SqlParameter("@P_COURSE_NAME", objCourse.CourseName);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", objCourse.SchNo);
                        objParams[2] = new SqlParameter("@P_CCODE", objCourse.CCode);
                        objParams[3] = new SqlParameter("@P_SUBID", objCourse.SubID);

                        objParams[4] = new SqlParameter("@P_ELECT", objCourse.Elect);
                        objParams[5] = new SqlParameter("@P_GLOBALELE", objCourse.GlobalEle);
                        objParams[6] = new SqlParameter("@P_CREDITS", objCourse.Credits);

                        objParams[7] = new SqlParameter("@P_LECTURE", objCourse.Lecture);
                        objParams[8] = new SqlParameter("@P_THEORY", objCourse.Theory);
                        objParams[9] = new SqlParameter("@P_PRACTICAL", objCourse.Practical);

                        objParams[10] = new SqlParameter("@P_MAXMARKS_I", objCourse.MaxMarks_I);
                        objParams[11] = new SqlParameter("@P_MAXMARKS_E", objCourse.ExtermarkMax);
                        objParams[12] = new SqlParameter("@P_MINMARKS", objCourse.ExtermarkMin);

                        objParams[13] = new SqlParameter("@P_S1MAX", objCourse.S1Max);
                        objParams[14] = new SqlParameter("@P_S2MAX", objCourse.S2Max);
                        objParams[15] = new SqlParameter("@P_S3MAX", objCourse.S3Max);
                        objParams[16] = new SqlParameter("@P_S4MAX", objCourse.S4Max);
                        objParams[17] = new SqlParameter("@P_S5MAX", objCourse.S5Max);
                        objParams[18] = new SqlParameter("@P_S6MAX", objCourse.S6Max);
                        objParams[19] = new SqlParameter("@P_S7MAX", objCourse.S7Max);
                        objParams[20] = new SqlParameter("@P_S8MAX", objCourse.S8Max);
                        objParams[21] = new SqlParameter("@P_S9MAX", objCourse.S9Max);
                        objParams[22] = new SqlParameter("@P_S10MAX", objCourse.S10Max);
                        objParams[23] = new SqlParameter("@P_S1Min", objCourse.S1Min);
                        objParams[24] = new SqlParameter("@P_S2Min", objCourse.S2Min);
                        objParams[25] = new SqlParameter("@P_S3Min", objCourse.S3Min);
                        objParams[26] = new SqlParameter("@P_S4Min", objCourse.S4Min);
                        objParams[27] = new SqlParameter("@P_S5Min", objCourse.S5Min);
                        objParams[28] = new SqlParameter("@P_S6Min", objCourse.S6Min);
                        objParams[29] = new SqlParameter("@P_S7Min", objCourse.S7Min);
                        objParams[30] = new SqlParameter("@P_S8Min", objCourse.S8Min);
                        objParams[31] = new SqlParameter("@P_S9Min", objCourse.S9Min);
                        objParams[32] = new SqlParameter("@P_S10Min", objCourse.S10Min);

                        objParams[33] = new SqlParameter("@P_ASSIGNMAX", objCourse.AssignMax);
                        objParams[34] = new SqlParameter("@P_DRAWING", objCourse.Drawing);

                        objParams[35] = new SqlParameter("@P_LEVELNO", objCourse.Levelno);
                        objParams[36] = new SqlParameter("@P_GROUPNO", objCourse.Groupno);

                        objParams[37] = new SqlParameter("@P_PREREQUISITE", objCourse.Prerequisite);
                        objParams[38] = new SqlParameter("@P_PREREQUISITE_CREDIT", objCourse.Prerequisite_cr);

                        objParams[39] = new SqlParameter("@P_GRADE", objCourse.Grade);
                        objParams[40] = new SqlParameter("@P_MINGRADE", objCourse.MinGrade);

                        objParams[41] = new SqlParameter("@P_COLLEGE_CODE", objCourse.CollegeCode);
                        objParams[42] = new SqlParameter("@P_SEMESTERNO", objCourse.SemesterNo);
                        objParams[43] = new SqlParameter("@P_SPECIALISATIONNO", objCourse.Specialisation);
                        objParams[44] = new SqlParameter("@P_PAPER_HRS", objCourse.Paper_hrs);
                        objParams[45] = new SqlParameter("@P_CGROUPNO", objCourse.CGroupno);
                        objParams[46] = new SqlParameter("@P_SCALING", objCourse.Scaling);
                        objParams[47] = new SqlParameter("@P_CATEGORYNO", objCourse.Categoryno);
                        objParams[48] = new SqlParameter("@P_BOS_DEPTNO", objCourse.Deptno);

                        objParams[49] = new SqlParameter("@P_COURSE_NO", SqlDbType.Int);
                        objParams[49].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_COURSE_SP_INS_COURSE_COVID", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.AddCourse-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int AddCourseCreation(Course objCourse, int UANO, decimal Lec_Unit, decimal Lab_Unit)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[15];
                        objParams[0] = new SqlParameter("@P_DEPARTMENT_NO", Convert.ToInt32(objCourse.Deptno));
                        objParams[1] = new SqlParameter("@P_SUBJECT_CODE", objCourse.CCode);
                        objParams[2] = new SqlParameter("@P_SUBJECT_NAME", objCourse.CourseName);
                        objParams[3] = new SqlParameter("@P_SUBJECT_TYPE", objCourse.SubID);
                        objParams[4] = new SqlParameter("@P_CAPACITY", objCourse.Capacity);
                        objParams[5] = new SqlParameter("@P_LECTURE", objCourse.Lecture);
                        objParams[6] = new SqlParameter("@P_THEORY", objCourse.Theory);
                        objParams[7] = new SqlParameter("@P_PRACTICAL", objCourse.Practical);
                        objParams[8] = new SqlParameter("@P_CREDITS", objCourse.Credits);
                        objParams[9] = new SqlParameter("@P_ACTIVE", objCourse.Active);
                        objParams[10] = new SqlParameter("@P_CREATEDBY", UANO);
                        objParams[11] = new SqlParameter("@P_SEMESTERNO", objCourse.SemesterNo);
                        objParams[12] = new SqlParameter("@P_LEC_UNIT", Lec_Unit); // Add By RoshanPatil 03-03-2023
                        objParams[13] = new SqlParameter("@P_LAB_UNIT", Lab_Unit);  // Add By RoshanPatil 03-03-2023

                        objParams[14] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[14].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_COURSE_CREATION", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.AddCourse-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //ADD BY AASHNA 24-09-21
                //public DataSet GetCourseCreation(int dept)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                //        SqlParameter[] objParams = new SqlParameter[1];

                //        objParams[0] = new SqlParameter("@P_DEPARTMENT_NO", dept);
                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_COURSE_CREATION", objParams);
                //    }
                //    catch (Exception ex)
                //    {
                //        return ds;
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetCourseCreation-> " + ex.ToString());
                //    }
                //    return ds;
                //}

                // Added by Sakshi Mohadikar 18-12-2023//
                public DataSet GetCourseCreation(int dept, int pageindex, int pagesize, string search)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[4];

                        objParams[0] = new SqlParameter("@P_DEPARTMENT_NO", dept);
                        objParams[1] = new SqlParameter("@P_PAGEINDEX", pageindex);
                        objParams[2] = new SqlParameter("@P_PAGESIZE", pagesize);
                        objParams[3] = new SqlParameter("@P_SEARCHTEXT", search);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_COURSE_CREATION", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetCourseCreation-> " + ex.ToString());
                    }
                    return ds;
                }

                // End// 
                //add by aashna get offered course
                public DataSet GetCourseOfferedCreation(int schemeNo, int sessionNo, int degreeno, int college_id, int Dept)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SCHEMENO", schemeNo);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionNo);
                        objParams[2] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[3] = new SqlParameter("@P_COLLEGE_ID", college_id);
                        objParams[4] = new SqlParameter("@P_DEPT", Dept);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_COURSE_OFFERED_COURSE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.GetStudentForFaculty-> " + ex.ToString());
                    }
                    return ds;
                }

                public int CopyOfferedCourses(int OldSessionNo, int Collegeid, string degreenos, int newSessionno, string coursenos, string Semesternos, int uano, string Schemenos)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_OLDSESSIONNO", OldSessionNo);
                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", Collegeid);
                        objParams[2] = new SqlParameter("@P_DEGREENOS", degreenos);
                        objParams[3] = new SqlParameter("@P_NEWSESSIONNO", newSessionno);
                        objParams[4] = new SqlParameter("@P_COURSENOS", coursenos);
                        objParams[5] = new SqlParameter("@P_SEMESTERNOS", Semesternos);
                        objParams[6] = new SqlParameter("@P_UANO", uano);
                        objParams[7] = new SqlParameter("@P_SCHEMENOS", Schemenos);
                        objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_COPY_OFFERED_COURSES", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.CopyOfferedCourses-> " + ex.ToString());
                    }

                    return retStatus;
                }
                public int InsertOfferedCourse(int sessionNo, int SchemeNo, string capacity, string credits, string semester, string elect, string group
, int collegeid, string offered, string internalw, string externalw, string TOTAL, int ua_no, string ipAdd, string CA_PER, string final_per, string overall_per, string modulelic, string ccode, string Special, string Regular, string CrossSchemes)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[23];
                        objParams[0] = new SqlParameter("@P_SEMESTERNOS", semester);
                        objParams[1] = new SqlParameter("@P_CREDITS", credits);
                        objParams[2] = new SqlParameter("@P_CAPACITY", capacity);
                        objParams[3] = new SqlParameter("@P_ELECTS", elect);
                        objParams[4] = new SqlParameter("@P_GROUPNOS", group);
                        objParams[5] = new SqlParameter("@P_COURSENOS", offered);
                        objParams[6] = new SqlParameter("@P_INTERNAL_WEIGHTAGE", internalw);
                        objParams[7] = new SqlParameter("@P_EXTERNAL_WEIGHTAGE", externalw);
                        objParams[8] = new SqlParameter("@P_TOTAL", TOTAL);
                        objParams[9] = new SqlParameter("@P_SESSIONNO", sessionNo);
                        objParams[10] = new SqlParameter("@P_SCHEMENO", SchemeNo);
                        objParams[11] = new SqlParameter("@P_COLLEGE_ID", collegeid);
                        objParams[12] = new SqlParameter("@P_UANO", ua_no);
                        objParams[13] = new SqlParameter("@P_IPADDDRESS", ipAdd);

                        objParams[14] = new SqlParameter("@P_CA_PERCENTAGE", CA_PER);
                        objParams[15] = new SqlParameter("@P_FINAL_PERCENTAGE", final_per);
                        objParams[16] = new SqlParameter("@P_OVERALL_PERCENTAGE", overall_per);
                        objParams[17] = new SqlParameter("@P_MODULELIC", modulelic);
                        objParams[18] = new SqlParameter("@P_CCODE", ccode);
                        objParams[19] = new SqlParameter("@P_SPECIAL", Special);
                        objParams[20] = new SqlParameter("@P_REGULAR", Regular);
                        objParams[21] = new SqlParameter("@P_CROSS_SCHEME", CrossSchemes);
                        objParams[22] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[22].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_COURSE_INSERT_UPD_OFFERED_COURSE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.UpdateOfferedCourse -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetCollegeRollList(int YEAR)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];

                        objParams[0] = new SqlParameter("@P_YEAR", YEAR);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_COLLEGE_LIST", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetCourseCreation-> " + ex.ToString());
                    }
                    return ds;
                }

                public int InsertCollegeroll(int year, int college, string startrange, string endrange)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_YEAR", year);
                        objParams[1] = new SqlParameter("@P_COLLEGE", college);
                        objParams[2] = new SqlParameter("@P_START_RANGE", startrange);
                        objParams[3] = new SqlParameter("@P_END_RANGE", endrange);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_COLLEGE_ROLLLIST", objParams, true);
                        if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.UpdateOfferedCourse -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int UpdateCourseCreation(Course objCourse, int UANO, int COURSENO, decimal Lec_Unit, decimal Lab_Unit)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[16];
                        objParams[0] = new SqlParameter("@P_DEPARTMENT_NO", Convert.ToInt32(objCourse.Deptno));
                        objParams[1] = new SqlParameter("@P_SUBJECT_CODE", objCourse.CCode);
                        objParams[2] = new SqlParameter("@P_SUBJECT_NAME", objCourse.CourseName);
                        objParams[3] = new SqlParameter("@P_SUBJECT_TYPE", objCourse.SubID);
                        objParams[4] = new SqlParameter("@P_CAPACITY", objCourse.Capacity);
                        objParams[5] = new SqlParameter("@P_LECTURE", objCourse.Lecture);
                        objParams[6] = new SqlParameter("@P_THEORY", objCourse.Theory);
                        objParams[7] = new SqlParameter("@P_PRACTICAL", objCourse.Practical);
                        objParams[8] = new SqlParameter("@P_CREDITS", objCourse.Credits);
                        objParams[9] = new SqlParameter("@P_ACTIVE", objCourse.Active);
                        objParams[10] = new SqlParameter("@P_MODIFIED_BY", UANO);
                        objParams[11] = new SqlParameter("@P_COURSENO", COURSENO);
                        objParams[12] = new SqlParameter("@P_SEMESTERNO", objCourse.SemesterNo);
                        objParams[13] = new SqlParameter("@P_LEC_UNIT", Lec_Unit);
                        objParams[14] = new SqlParameter("@P_LAB_UNIT", Lab_Unit);
                        objParams[15] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[15].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_UPDATE_COURSE_CREATION", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.AddCourse-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int InsertAssessment(int Session, int Course, string Ccode, int CA, int Final, int MinCa, int MinFinal, int OverAll, int Assessment, int OutOfMarks, int Weightage)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParam = null;
                        objParam = new SqlParameter[12];
                        objParam[0] = new SqlParameter("@P_SESSIONNO", Session);
                        objParam[1] = new SqlParameter("@P_COURSENO", Course);
                        objParam[2] = new SqlParameter("@P_CCODE  ", Ccode);
                        objParam[3] = new SqlParameter("@P_CA_PER", CA);
                        objParam[4] = new SqlParameter("@P_FINAL_PER", Final);
                        objParam[5] = new SqlParameter("@P_MIN_CA", MinCa);
                        objParam[6] = new SqlParameter("@P_MIN_FINAL  ", MinFinal);
                        objParam[7] = new SqlParameter("@P_OVERALL", OverAll);
                        objParam[8] = new SqlParameter("@P_ASSESSMENT_NO", Assessment);
                        objParam[9] = new SqlParameter("@P_OUTOFMARKS", OutOfMarks);
                        objParam[10] = new SqlParameter("@P_WEIGHTAGE", Weightage);
                        objParam[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParam[11].Direction = ParameterDirection.Output;
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INS_ASSESSMENT", objParam, true) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeesHeadController.InsertCourse-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetAssessment(int Assessment)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COURSENO", Assessment);

                        ds = objSQLHelper.ExecuteDataSetSP("[PKG_ACA_GET_ASSESSMENT]", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeesHeadController.GetCourse-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet COURSELISTHOD(int Session, int Courseno, int College_id, int Semesterno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", Session);
                        objParams[1] = new SqlParameter("@P_COURSENO", Courseno);
                        objParams[2] = new SqlParameter("@P_COLLEGE_ID", College_id);
                        objParams[3] = new SqlParameter("@P_SEMSETERNO", Semesterno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_FACULTY_WISE_SEMESTER_WISE_COURSE_LIST_HOD", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetAllAlertInfo-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataTableReader GetOfferdPer(int Course, int sessionno, string modulelic)
                {
                    DataTableReader ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_COURSENO", Course);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[2] = new SqlParameter("@P_MODULELIC", modulelic); 
                        ds = objSQLHelper.ExecuteDataSetSP("[PKG_ACA_GET_OFFERD_COURSE_PER]", objParams).Tables[0].CreateDataReader();
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeesHeadController.GetCourse-> " + ex.ToString());
                    }
                    return ds;
                }

                public int InsertCourseMapping(int College, int deptno, int program, int Curriculum, string module, int CoreElect, int sem, int Sub_classficaNo, int Status, string TotSemHours)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParam = null;
                        objParam = new SqlParameter[11];
                        objParam[0] = new SqlParameter("@P_COLLEGE_ID", College);
                        objParam[1] = new SqlParameter("@P_DEPTNO", deptno);
                        objParam[2] = new SqlParameter("@P_DEGREENO", program);
                        objParam[3] = new SqlParameter("@P_SCHEMENO", Curriculum);
                        objParam[4] = new SqlParameter("@P_COURSENO", module);
                        objParam[5] = new SqlParameter("@P_CORE_ELECTIVE", CoreElect);
                        objParam[6] = new SqlParameter("@P_SEMESTERNO", sem);
                        objParam[7] = new SqlParameter("@P_SUB_CLASSIFICATION", Sub_classficaNo);
                        objParam[8] = new SqlParameter("@P_STATUS", Status);
                        objParam[9] = new SqlParameter("@P_TOTSEMESTERHOURS", TotSemHours);
                        objParam[10] = new SqlParameter("P_OUT", SqlDbType.Int);
                        objParam[10].Direction = ParameterDirection.Output;
                        object obj = objSQLHelper.ExecuteNonQuerySP("ACD_INSERT_COURSE_MAPPING", objParam, true);
                        if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001" & obj.ToString() != "0")
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32((CustomStatus.Error));
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.ChangePassword-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetCourseM(int College_id, int Schemeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_COLLEGE_ID", College_id);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", Schemeno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_COURSE_LIST", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetCourseCreation-> " + ex.ToString());
                    }
                    return ds;
                }

                public int AddRuleName(string RollName, int chkAptitutes, int chkInterviews, string ALType, int StreamAL, int MinCourseAL, string MinGradesAL, int MaxCourseAL, string MaxGradesAL, string ALSubject, int StreamOL, int MinCourseOL, string MinGradesOL, int MaxCourseOL, string MaxGradesOL, string OlSubject, int Active)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[18];
                        objParams[0] = new SqlParameter("@P_RULENAME", RollName);
                        objParams[1] = new SqlParameter("@P_APTITUDE_TEST", chkAptitutes);
                        objParams[2] = new SqlParameter("@P_INTERVIEW_TEST", chkInterviews);
                        objParams[3] = new SqlParameter("@P_ALTYPENAME", ALType);
                        objParams[4] = new SqlParameter("@P_ALSTREAMNAME", StreamAL);
                        objParams[5] = new SqlParameter("@P_ALMIN_COURSE", MinCourseAL);
                        objParams[6] = new SqlParameter("@P_ALMIN_GRADES", MinGradesAL);
                        objParams[7] = new SqlParameter("@P_ALMAX_COURSE", MaxCourseAL);
                        objParams[8] = new SqlParameter("@P_ALMAX_GRADES", MaxGradesAL);
                        objParams[9] = new SqlParameter("@P_AL_SUBJECT", ALSubject);
                        objParams[10] = new SqlParameter("@P_OLSTREAMNAME", StreamOL);
                        objParams[11] = new SqlParameter("@P_OLMIN_COURSE", MinCourseOL);
                        objParams[12] = new SqlParameter("@P_OLMIN_GRADES", MinGradesOL);
                        objParams[13] = new SqlParameter("@P_OLMAX_COURSE", MaxCourseOL);
                        objParams[14] = new SqlParameter("@P_OLMAX_GRADES", MaxGradesOL);
                        objParams[15] = new SqlParameter("@P_OL_SUBJECT", OlSubject);
                        objParams[16] = new SqlParameter("@P_STATUS", Active);

                        objParams[17] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[17].Direction = System.Data.ParameterDirection.Output;

                        object obj = objSQLHelper.ExecuteNonQuerySP("ACD_INSERT_COURSE_RULE", objParams, true);

                        if (Convert.ToInt32(obj) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeesHeadController.InsertCourse-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetAllRuleCreation()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ALL_RULE_CREATION", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetAllAlertInfo-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetAllRuleAllocation()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ALL_RULE_ALLOCATION", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetAllAlertInfo-> " + ex.ToString());
                    }
                    return ds;
                }

                public int AddRuleAllocation(int Intake, int Discipline, int degree, int program, string RuleName)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_INTAKENO", Intake);
                        objParams[1] = new SqlParameter("@P_UA_SECTION", Discipline);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", program);
                        objParams[3] = new SqlParameter("@P_COURSERULE", RuleName);
                        objParams[4] = new SqlParameter("@P_DEGREENO ", degree);

                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = System.Data.ParameterDirection.Output;

                        object obj = objSQLHelper.ExecuteNonQuerySP("ACD_INSERT_COURSE_RULE_ALLOCATION", objParams, true);

                        if (Convert.ToInt32(obj) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeesHeadController.InsertCourse-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int UpdateRuleAllocation(int AllocationNo, int Intake, int Discipline, int Degree, int Program, string RuleName)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Edit Course
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_ALLOCATIONNO", AllocationNo);
                        objParams[1] = new SqlParameter("@P_INTAKENO", Intake);
                        objParams[2] = new SqlParameter("@P_UA_SECTION", Discipline);
                        objParams[3] = new SqlParameter("@P_BRANCHNO", Program);
                        objParams[4] = new SqlParameter("@P_DEGREENO", Degree);
                        objParams[5] = new SqlParameter("@P_COURSERULE", RuleName);
                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("ACD_UPDATE_COURSE_RULE_ALLOCATION", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.UpdateOfferedCourse -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetModuleName(int branch, int degree)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_BRANCHNO", branch);
                        objParams[1] = new SqlParameter("@P_DEGREENO", degree);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_MODULE_BY_DEPTNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetCourseCreation-> " + ex.ToString());
                    }
                    return ds;
                }

                public int InsertBridging(string RollName, int college, string program, int degree, string modulecode, string modulename, string stream, int CourseNo, int chks)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_RULENAME", RollName);
                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", college);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", program);
                        objParams[3] = new SqlParameter("@P_DEGREENO", degree);
                        objParams[4] = new SqlParameter("@P_CCODE", modulecode);
                        objParams[5] = new SqlParameter("@P_COURSE_ENAME", modulename);
                        objParams[6] = new SqlParameter("@P_STREAMNO", stream);
                        objParams[7] = new SqlParameter("@P_COURSENO", CourseNo);
                        objParams[8] = new SqlParameter("@P_CHECKED", chks);

                        objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[9].Direction = System.Data.ParameterDirection.Output;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_BRIDGING_ELIGIBILITY", objParams, true);

                        if (Convert.ToInt32(obj) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeesHeadController.InsertCourse-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int InsertBridgingAllocation(int intake, string RollName)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_INTAKE", intake);
                        objParams[1] = new SqlParameter("@P_RULENAME", RollName);

                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = System.Data.ParameterDirection.Output;

                        object obj = objSQLHelper.ExecuteNonQuerySP("ACD_INSERT_BRIDGING_ALLOCATION", objParams, true);

                        if (Convert.ToInt32(obj) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeesHeadController.InsertCourse-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetAllBridgingAllocationRule()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ALL_BRIDGING_ALLOCATION_RULE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetAllAlertInfo-> " + ex.ToString());
                    }
                    return ds;
                }

                public int UpdateRuleCreation(int Courseruleno, string RollName, int chkAptitutes, int chkInterviews, string ALType, int StreamAL, int MinCourseAL, string MinGradesAL, int MaxCourseAL, string MaxGradesAL, string ALSubject, int StreamOL, int MinCourseOL, string MinGradesOL, int MaxCourseOL, string MaxGradesOL, string OlSubject, int Active)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Edit Course
                        objParams = new SqlParameter[19];
                        objParams[0] = new SqlParameter("@P_COURSERULENO", Courseruleno);
                        objParams[1] = new SqlParameter("@P_RULENAME", RollName);
                        objParams[2] = new SqlParameter("@P_APTITUDE_TEST", chkAptitutes);
                        objParams[3] = new SqlParameter("@P_INTERVIEW_TEST", chkInterviews);
                        objParams[4] = new SqlParameter("@P_ALTYPENAME", ALType);
                        objParams[5] = new SqlParameter("@P_ALSTREAMNAME", StreamAL);
                        objParams[6] = new SqlParameter("@P_ALMIN_COURSE", MinCourseAL);
                        objParams[7] = new SqlParameter("@P_ALMIN_GRADES", MinGradesAL);
                        objParams[8] = new SqlParameter("@P_ALMAX_COURSE", MaxCourseAL);
                        objParams[9] = new SqlParameter("@P_ALMAX_GRADES", MaxGradesAL);
                        objParams[10] = new SqlParameter("@P_AL_SUBJECT", ALSubject);
                        objParams[11] = new SqlParameter("@P_OLSTREAMNAME", StreamOL);
                        objParams[12] = new SqlParameter("@P_OLMIN_COURSE", MinCourseOL);
                        objParams[13] = new SqlParameter("@P_OLMIN_GRADES", MinGradesOL);
                        objParams[14] = new SqlParameter("@P_OLMAX_COURSE", MaxCourseOL);
                        objParams[15] = new SqlParameter("@P_OLMAX_GRADES", MaxGradesOL);
                        objParams[16] = new SqlParameter("@P_OL_SUBJECT", OlSubject);
                        objParams[17] = new SqlParameter("@P_STATUS", Active);

                        objParams[18] = new SqlParameter("@P_OUT", SqlDbType.Int);

                        objParams[18].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("ACD_UPDATE_COURSE_RULE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.UpdateOfferedCourse -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetAssessmentComponet(int session)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                        ds = objSQLHelper.ExecuteDataSetSP("[PKG_GET_ASSESSMENT_COMPONET]", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetCourseCreation-> " + ex.ToString());
                    }
                    return ds;
                }

                public int InsertChangeElective(int idno, int AcademicSession, string ddModule, int Modulename, string oldmoduleCode, int oldmoduleName, DateTime date, int check
                                   , int semesterno, int uano, string coursename, string regno, int campusno, int weekno, int degreeno, int branchno, int college_id, int SCHEMENO, decimal CREDIT, int sectionno, string ipaddress)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[22];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", AcademicSession);
                        objParams[2] = new SqlParameter("@P_NEW_COURSENO", Modulename);
                        objParams[3] = new SqlParameter("@P_NEW_CCODE", ddModule);
                        objParams[4] = new SqlParameter("@P_OLD_COURSENO", oldmoduleName);
                        objParams[5] = new SqlParameter("@P_OLD_CCODE", oldmoduleCode);
                        objParams[6] = new SqlParameter("@P_DATE", date);
                        objParams[7] = new SqlParameter("@P_CHECKED", check);
                        objParams[8] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[9] = new SqlParameter("@P_UA_NO", uano);
                        objParams[10] = new SqlParameter("@P_COURSENAME", coursename);
                        objParams[11] = new SqlParameter("@P_REGNO", regno);
                        objParams[12] = new SqlParameter("@P_CAMPUSNO", campusno);
                        objParams[13] = new SqlParameter("@P_WEEKSNOS", weekno);
                        objParams[14] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[15] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[16] = new SqlParameter("@P_SCHEMENO", SCHEMENO);
                        objParams[17] = new SqlParameter("@P_CREDITS", CREDIT);
                        objParams[18] = new SqlParameter("@P_COLLEGE_ID", college_id);
                        objParams[19] = new SqlParameter("@P_SECTIONNO", sectionno);
                        objParams[20] = new SqlParameter("@P_IPADDRESS", ipaddress);
                        objParams[21] = new SqlParameter("@P_OUTPUT   ", SqlDbType.Int);
                        objParams[21].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("ACD_INSERT_CHANGE_ELECTIVE_MODULE", objParams, true);
                        if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.UpdateOfferedCourse -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetElectiveCourse(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        ds = objSQLHelper.ExecuteDataSetSP("[ACD_GET_CHANGE_ELECTIVE_MODULE]", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetCourseCreation-> " + ex.ToString());
                    }
                    return ds;
                }

                public int InsertExamIncharge(int College_ID, int Uano, int Stafno, int Status, int ExamNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_COLLEGE_ID", College_ID);
                        objParams[1] = new SqlParameter("@P_UA_NO", Uano);
                        objParams[2] = new SqlParameter("@P_STAFF_NO", Stafno);
                        objParams[3] = new SqlParameter("@P_STATUS", Status);
                        objParams[4] = new SqlParameter("@P_EXAM_NO", ExamNo);
                        objParams[5] = new SqlParameter("@P_OUTPUT   ", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_EXAM_UNIT_INCHARGE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 999)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.UpdateOfferedCourse -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetInchargeList()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objDataAccess.ExecuteDataSetSP("PKG_GET_EXAM_UNIT_INCHARGE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.GetSingleSession-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetexamDownloadSettings(string Semester, int session, int college, string program, int ExamNo, string ugpgot, string degreeNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_SEMESTER", Semester);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", session);
                        objParams[2] = new SqlParameter("@P_COLLEGE_ID", college);
                        objParams[3] = new SqlParameter("@P_BRANCHNO", program);
                        objParams[4] = new SqlParameter("@P_EXAMNO", ExamNo);
                        objParams[5] = new SqlParameter("@P_UG_PG", ugpgot);
                        objParams[6] = new SqlParameter("@P_DEGREENO", degreeNo);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_PAPER_SUBMISSION_DATA", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetCourseCreation-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetexamSettings(string Semester, int session, int college, string program, string UgPg, string Degreeno, int Uano)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_SEMESTER", Semester);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", session);
                        objParams[2] = new SqlParameter("@P_COLLEGE_ID", college);
                        objParams[3] = new SqlParameter("@P_BRANCHNO", program);
                        objParams[4] = new SqlParameter("@P_UGPG", UgPg);
                        objParams[5] = new SqlParameter("@P_DEGREENO", Degreeno);
                        objParams[6] = new SqlParameter("@P_UA_NO", Uano);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_PAPER_SETTING_DATA", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetCourseCreation-> " + ex.ToString());
                    }
                    return ds;
                }

                public int InsertExam_settings(int sessionno, int examno, DateTime date, string semisterno, string courseno, string branchno, int licname, string ugpgot, int college_id, string degreeno, string Coexami_uano)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[12];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_EXAM_NO", examno);

                        objParams[2] = new SqlParameter("@P_DUE_DATE", date);
                        objParams[3] = new SqlParameter("@P_SEMESTER", semisterno);
                        objParams[4] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[5] = new SqlParameter("@P_BRANCHNO", branchno);

                        objParams[6] = new SqlParameter("@P_UA_NO", licname);
                        objParams[7] = new SqlParameter("@P_UGPGOT", ugpgot);
                        objParams[8] = new SqlParameter("@P_COLLEGE_ID", college_id);
                        objParams[9] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[10] = new SqlParameter("@P_COEXAMIN_UANO", Coexami_uano);

                        objParams[11] = new SqlParameter("@P_OUTPUT   ", SqlDbType.Int);
                        objParams[11].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_PAPER_SETTING_DATA", objParams, true);
                        if (Convert.ToInt32(ret) == 999)
                            retStatus = Convert.ToInt32(CustomStatus.FileExists);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.UpdateOfferedCourse -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int UpdateUplodeFile(int CourseNo, int BRANCHNO, string filename)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[4];

                        objParams[0] = new SqlParameter("@P_COURSENO", CourseNo);
                        objParams[1] = new SqlParameter("@P_BRANCHNO", BRANCHNO);
                        objParams[2] = new SqlParameter("@P_FILE_NAME", filename);

                        objParams[3] = new SqlParameter("@P_OUTPUT   ", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("ACD_UPDATE_UPLODED_FILE", objParams, true);
                        if (Convert.ToInt32(ret) == 999)
                            retStatus = Convert.ToInt32(CustomStatus.FileExists);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.UpdateOfferedCourse -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int InsertInternalModiration(int college_id, string courseno, DateTime InternalDate, string Remark, int IntSession, int IntExam)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_COLLEGE_ID", college_id);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);

                        objParams[2] = new SqlParameter("@P_INTERNAL_MOD_DATE", InternalDate);
                        objParams[3] = new SqlParameter("@P_INTERNAL_REMARK", Remark);
                        objParams[4] = new SqlParameter("@P_SESSIONNO", IntSession);
                        objParams[5] = new SqlParameter("@P_EXAM_NO", IntExam);

                        objParams[6] = new SqlParameter("@P_OUTPUT   ", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_UPDATE_INTERNAL_MODIRATION", objParams, true);
                        if (Convert.ToInt32(ret) == 999)
                            retStatus = Convert.ToInt32(CustomStatus.FileExists);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.UpdateOfferedCourse -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int InsertExternalModiration(int college_id, string courseno, DateTime InternalDate, string Remark, int IntSession, int IntExam)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_COLLEGE_ID", college_id);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);

                        objParams[2] = new SqlParameter("@P_EXTERNAL_MOD_DATE", InternalDate);
                        objParams[3] = new SqlParameter("@P_EXTERNAL_REMARK", Remark);
                        objParams[4] = new SqlParameter("@P_SESSIONNO", IntSession);
                        objParams[5] = new SqlParameter("@P_EXAM_NO", IntExam);

                        objParams[6] = new SqlParameter("@P_OUTPUT   ", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_UPDATE_EXTERNAL_MODIRATION", objParams, true);
                        if (Convert.ToInt32(ret) == 999)
                            retStatus = Convert.ToInt32(CustomStatus.FileExists);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.UpdateOfferedCourse -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetPaperInternalExternalData(int session, int ExamNo, int UaNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                        objParams[1] = new SqlParameter("@P_EXAMNO", ExamNo);
                        objParams[2] = new SqlParameter("@P_UANO", UaNo);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_PAPER_INTERNAL_EXATERNALMODERATION_DATA", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetCourseCreation-> " + ex.ToString());
                    }
                    return ds;
                }

                //ROSHAN PATIL 12-03-2022 Convocation
                public int InsertInternalModiration(int college_id, string courseno, DateTime InternalDate, string Remark, int IntSession, int IntExam, string BranchNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_COLLEGE_ID", college_id);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);

                        objParams[2] = new SqlParameter("@P_INTERNAL_MOD_DATE", InternalDate);
                        objParams[3] = new SqlParameter("@P_INTERNAL_REMARK", Remark);
                        objParams[4] = new SqlParameter("@P_SESSIONNO", IntSession);
                        objParams[5] = new SqlParameter("@P_EXAM_NO", IntExam);
                        objParams[6] = new SqlParameter("@P_BRANCHNO", BranchNo);

                        objParams[7] = new SqlParameter("@P_OUTPUT   ", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_UPDATE_INTERNAL_MODIRATION", objParams, true);
                        if (Convert.ToInt32(ret) == 999)
                            retStatus = Convert.ToInt32(CustomStatus.FileExists);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.UpdateOfferedCourse -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int InsertConvocationfess(int Conv_no, string Program, int faculty, string StudyLevel, decimal Amount, int STATUS, int feesno, string Degreeno, decimal Refund)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_CONV_NO", Conv_no);
                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", faculty);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", Program);
                        objParams[3] = new SqlParameter("@P_UA_SECTION", StudyLevel);
                        objParams[4] = new SqlParameter("@P_AMOUNT", Amount);
                        objParams[5] = new SqlParameter("@P_STATUS", STATUS);
                        objParams[6] = new SqlParameter("@P_FEES_NO", feesno);
                        objParams[7] = new SqlParameter("@P_DEGREENO", Degreeno);
                        objParams[8] = new SqlParameter("@P_REFUNDFEES", Refund);

                        objParams[9] = new SqlParameter("@P_OUT   ", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("ACD_INSERT_CONVOCATION_FEES_MASTER", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else if (Convert.ToInt32(ret) == 2627)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.UpdateOfferedCourse -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetCovocationfees(int CollegeID, int Convono)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];

                        objParams[0] = new SqlParameter("@P_COLLEGE_ID", CollegeID);
                        objParams[1] = new SqlParameter("@P_CONV_NO", Convono);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_CONVOVATION_FEES", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetAllAlertInfo-> " + ex.ToString());
                    }
                    return ds;
                }

                public int InsertConvocation(string ConvovationName, DateTime dtStartDate, DateTime dtEndDate, DateTime LastDateApply, int Status, int CONV_NO)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_CONVOCATION_NAME", ConvovationName);
                        objParams[1] = new SqlParameter("@P_CONVO_STARTDATE", dtStartDate);
                        objParams[2] = new SqlParameter("@P_CONVO_ENDDATE", dtEndDate);
                        objParams[3] = new SqlParameter("@P_LASTDATEAPPLY", LastDateApply);
                        objParams[4] = new SqlParameter("@P_STATUS", Status);
                        objParams[5] = new SqlParameter("@P_CONV_NO", CONV_NO);
                        objParams[6] = new SqlParameter("@P_OUT   ", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("ACD_INSERT_CONVOCATION_MASTER", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2627)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.UpdateOfferedCourse -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetAllConvocation()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ALL_CONVOCATION", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetAllAlertInfo-> " + ex.ToString());
                    }
                    return ds;
                }

                public int InsertConvocationSession(int Convocation, DateTime DATE, string ExamDate, string SessionName, int status, int sessionno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_CONV_NO", Convocation);
                        objParams[1] = new SqlParameter("@P_SESSION_NAME", SessionName);
                        objParams[2] = new SqlParameter("@P_SESSION_DATE", DATE);
                        objParams[3] = new SqlParameter("@P_TIMESLOT", ExamDate);
                        objParams[4] = new SqlParameter("@P_STATUS", status);
                        objParams[5] = new SqlParameter("@P_SESSION_CONVNO", sessionno);
                        objParams[6] = new SqlParameter("@P_OUT   ", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("ACD_INSERT_CONVOCATION_SESSION", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2627)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.UpdateOfferedCourse -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetEligibilityCheck(int Conv_no, int Eligible)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_CONV_NO", Conv_no);
                        objParams[1] = new SqlParameter("@P_ELIGIBILITY", Eligible);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ELIGIBILITY_STUDENT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetAllAlertInfo-> " + ex.ToString());
                    }
                    return ds;
                }

                public int InsertELigibleStudent(string idno, string regNo, string College_id, string Program, string Semester, int Conv_no, int Status, string Remark, string Degree, string DegreeDiploma)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_REGNO", regNo);

                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", College_id);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", Program);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", Semester);
                        objParams[4] = new SqlParameter("@P_CONV_NO", Conv_no);
                        objParams[5] = new SqlParameter("@P_STATUS", Status);
                        objParams[6] = new SqlParameter("@P_REMARK", Remark);
                        objParams[7] = new SqlParameter("@P_IDNO", idno);
                        objParams[8] = new SqlParameter("@P_DEGREENO", Degree);
                        objParams[9] = new SqlParameter("@P_DEGREEDIPLOMA", DegreeDiploma);

                        objParams[10] = new SqlParameter("@P_OUT   ", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("ACD_INSERT_ELIGIBILITY_CHECK", objParams, true);
                        if (Convert.ToInt32(ret) == 999)
                            retStatus = Convert.ToInt32(CustomStatus.FileExists);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.UpdateOfferedCourse -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetSessionManagement(int Conv_no, int College_id, string Program, string Semister, string DegreeNo, int Aplistud)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_CONV_NO", Conv_no);
                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", College_id);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", Program);
                        objParams[3] = new SqlParameter("@P_SEMISTER", Semister);
                        objParams[4] = new SqlParameter("@P_DEGREENO", DegreeNo);
                        objParams[5] = new SqlParameter("@P_FILTER", Aplistud);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_SESSION_MANAGEMENT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetAllAlertInfo-> " + ex.ToString());
                    }
                    return ds;
                }

                public int InsertSessionAllot(string Idno, int Conv_no, string Convo_session, int Convo_Sessionno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[5];

                        objParams[0] = new SqlParameter("@P_CONV_NO", Conv_no);
                        objParams[1] = new SqlParameter("@P_CONVO_SESSION", Convo_session);
                        objParams[2] = new SqlParameter("@P_IDNO", Idno);
                        objParams[3] = new SqlParameter("@P_CONVO_SESSIONNO", Convo_Sessionno);

                        //objParams[8] = new SqlParameter("@P_SEAT_NO", SeatNo);

                        objParams[4] = new SqlParameter("@P_OUT   ", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("ACD_INSERT_CONVO_SESSION_MANAGEMENT", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.UpdateOfferedCourse -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int InsertGenaratePassNo(string Idno, int Conv_no, string Convo_session)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[4];

                        objParams[0] = new SqlParameter("@P_IDNO", Idno);
                        objParams[1] = new SqlParameter("@P_CONV_NO", Conv_no);

                        objParams[2] = new SqlParameter("@P_CONVO_SESSION", Convo_session);

                        objParams[3] = new SqlParameter("@P_OUT   ", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("ACD_INSERT_GENERATE_CONVOCATION_PASS", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.UpdateOfferedCourse -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int UpdateConvovationSeat(string Idno, int Conv_no, string Convo_session, string SeatNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[5];

                        objParams[0] = new SqlParameter("@P_IDNO", Idno);
                        objParams[1] = new SqlParameter("@P_CONV_NO", Conv_no);
                        objParams[2] = new SqlParameter("@P_CONVO_SESSION", Convo_session);
                        objParams[3] = new SqlParameter("@P_SEAT", SeatNo);
                        objParams[4] = new SqlParameter("@P_OUT   ", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("ACD_UPDATE_CONVO_SEAT_MANAGEMENT", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.UpdateOfferedCourse -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetConvocationLists(int CollegeID, string Program, string Convo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_COLLEGE_ID", CollegeID);
                        objParams[1] = new SqlParameter("@P_BRANCHNO", Program);
                        objParams[2] = new SqlParameter("@P_CONVO_NO", Convo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_CONVOCATION_READING_LIST", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetAllAlertInfo-> " + ex.ToString());
                    }
                    return ds;
                }

                public int InsertApplyConvocation(string Name, int conv_no, int PresentAbsent, int IDNO, string REGNO, int Status, string Name_Sinhala, string NameInEnglish, string Mobileno, string Email, string Address, int passno, string NicPassport, string PGDiploma_Degree, int Ugpg)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[16];
                        objParams[0] = new SqlParameter("@P_STUDENT_NAME", Name);
                        objParams[1] = new SqlParameter("@P_CONV_NO", conv_no);
                        objParams[2] = new SqlParameter("@P_PRESENCE_ABSENT", PresentAbsent);
                        objParams[3] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[4] = new SqlParameter("@P_REGNO", REGNO);
                        objParams[5] = new SqlParameter("@P_STATUS", Status);
                        objParams[6] = new SqlParameter("@P_NAME_SINHALA", Name_Sinhala);
                        objParams[7] = new SqlParameter("@P_NAMEINENGLISH", NameInEnglish);
                        objParams[8] = new SqlParameter("@P_MOBILENO", Mobileno);
                        objParams[9] = new SqlParameter("@P_EMAIL", Email);
                        objParams[10] = new SqlParameter("@P_ADDRESS", Address);
                        objParams[11] = new SqlParameter("@P_PASSNO", passno);
                        objParams[12] = new SqlParameter("@P_NICPASSPORT", NicPassport);
                        objParams[13] = new SqlParameter("@P_PGDIPLOMA_DEGREE", PGDiploma_Degree);

                        objParams[14] = new SqlParameter("@P_UGPG", Ugpg);

                        objParams[15] = new SqlParameter("@P_OUT   ", SqlDbType.Int);
                        objParams[15].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("ACD_INSERT_APPLY_CONVOCATION", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.UpdateOfferedCourse -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetApplyConvocation(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_APPLY_CONVOCATION", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetAllAlertInfo-> " + ex.ToString());
                    }
                    return ds;
                }

                public int InsertPayConvocationFees(int Idno, int SemesterNo, int DegreeNo, int Sessionno, int College, int Program, string StudentName, string branchName, string Regno, string BankName, string BranchName, string PaymentDate, string RecieptCode, string PAY_MODE_CODE, decimal Amount, string OrderId, int PaymentMode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[18];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", Sessionno);
                        objParams[1] = new SqlParameter("@P_AMOUNT", Amount);
                        objParams[2] = new SqlParameter("@P_TRANSDATE", PaymentDate);
                        objParams[3] = new SqlParameter("@P_BRANCHNO", Program);
                        objParams[4] = new SqlParameter("@P_BRANCHNAME", branchName);
                        objParams[5] = new SqlParameter("@P_ORDER_ID", OrderId);
                        objParams[6] = new SqlParameter("@P_STUDENTNAME", StudentName);
                        objParams[7] = new SqlParameter("@P_BANKNAME", BankName);
                        objParams[8] = new SqlParameter("@P_BANKBRANCH", BranchName);
                        objParams[9] = new SqlParameter("@P_REGNO", Regno);
                        objParams[10] = new SqlParameter("@P_RECIEPTCODE", RecieptCode);
                        objParams[11] = new SqlParameter("@P_PAY_MODE", PAY_MODE_CODE);
                        objParams[12] = new SqlParameter("@P_IDNO", Idno);
                        objParams[13] = new SqlParameter("@P_SEMESTERNO", SemesterNo);
                        objParams[14] = new SqlParameter("@P_DEGREENO", DegreeNo);
                        objParams[15] = new SqlParameter("@P_COLLEGE_ID", College);
                        objParams[16] = new SqlParameter("@P_PAYMENTTYPE", PaymentMode);

                        objParams[17] = new SqlParameter("@P_OUTPUT   ", SqlDbType.Int);
                        objParams[17].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_PAYCONVOCATION_FEES_LOG", objParams, true);
                        if (Convert.ToInt32(ret) == 2627)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.UpdateOfferedCourse -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int InsertPayConvocationOnline(int Idno, int SemesterNo, int DegreeNo, int Sessionno, int College, int Program, string StudentName, string branchName, string Regno, string RecieptCode, string PAY_MODE_CODE, decimal Amount, string OrderId, int PaymentMode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[15];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", Sessionno);
                        objParams[1] = new SqlParameter("@P_AMOUNT", Amount);
                        // objParams[2] = new SqlParameter("@P_TRANSDATE", PaymentDate);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", Program);
                        objParams[3] = new SqlParameter("@P_BRANCHNAME", branchName);
                        objParams[4] = new SqlParameter("@P_ORDER_ID", OrderId);
                        objParams[5] = new SqlParameter("@P_STUDENTNAME", StudentName);
                        // objParams[7] = new SqlParameter("@P_BANKNAME", BankName);
                        //  objParams[8] = new SqlParameter("@P_BANKBRANCH", BranchName);
                        objParams[6] = new SqlParameter("@P_REGNO", Regno);
                        objParams[7] = new SqlParameter("@P_RECIEPTCODE", RecieptCode);
                        objParams[8] = new SqlParameter("@P_PAY_MODE", PAY_MODE_CODE);
                        objParams[9] = new SqlParameter("@P_IDNO", Idno);
                        objParams[10] = new SqlParameter("@P_SEMESTERNO", SemesterNo);
                        objParams[11] = new SqlParameter("@P_DEGREENO", DegreeNo);
                        objParams[12] = new SqlParameter("@P_COLLEGE_ID", College);
                        objParams[13] = new SqlParameter("@P_PAYMENTTYPE", PaymentMode);

                        objParams[14] = new SqlParameter("@P_OUTPUT   ", SqlDbType.Int);
                        objParams[14].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_PAYCONVOCATION_ONLINEFEES_LOG", objParams, true);
                        if (Convert.ToInt32(ret) == 999)
                            retStatus = Convert.ToInt32(CustomStatus.FileExists);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.UpdateOfferedCourse -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet getConvocationPaymentDetails(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_CONVOCATION_FEElIST", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetAllAlertInfo-> " + ex.ToString());
                    }
                    return ds;
                }

                public int InsertModeleRevision(int deptno, int module, string old_CCode, string new_CCode, string old_ModuleName, string new_modulename, decimal oldCredit, decimal newCredit, int UANO, int Revno, int fromyear, int toyear, int SubId)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[14];
                        objParams[0] = new SqlParameter("@P_DEPTNO", deptno);
                        objParams[1] = new SqlParameter("@P_COURSENO", module);
                        objParams[2] = new SqlParameter("@P_OLD_CCODE", old_CCode);
                        objParams[3] = new SqlParameter("@P_NEW_CCODE", new_CCode);
                        objParams[4] = new SqlParameter("@P_OLD_COURSENAME", old_ModuleName);
                        objParams[5] = new SqlParameter("@P_NEW_COURSENAME", new_modulename);
                        objParams[6] = new SqlParameter("@P_OLD_CREDITS", oldCredit);
                        objParams[7] = new SqlParameter("@P_NEW_CREDITS", newCredit);
                        objParams[8] = new SqlParameter("@P_UA_NO", UANO);
                        objParams[9] = new SqlParameter("@P_REVNO", Revno);
                        objParams[10] = new SqlParameter("@P_FROM_YEAR", fromyear);
                        objParams[11] = new SqlParameter("@P_TO_YEAR", toyear);
                        objParams[12] = new SqlParameter("@P_SUBID", SubId);
                        objParams[13] = new SqlParameter("@P_OUTPUT   ", SqlDbType.Int);
                        objParams[13].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("ACD_INSERT_MODULE_REVISION", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.UpdateOfferedCourse -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetModuleRevision(int DEPTNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_DEPTNO", DEPTNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ALL_MODULE_REVISION", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetAllAlertInfo-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetModuleRevisionCoursewise(int DEPTNO, int COURSENO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_DEPTNO", DEPTNO);
                        objParams[1] = new SqlParameter("@P_COURSENO", COURSENO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ALL_MODULE_REVISION_COURSEWISE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetAllAlertInfo-> " + ex.ToString());
                    }
                    return ds;
                }

                public int InsertbankDetails(string BankName, string BankCode, string BankAddress, int College_Code, string Account, int bankno, int Active)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_BANKNAME", BankName);
                        objParams[1] = new SqlParameter("@P_BANKCODE", BankCode);

                        objParams[2] = new SqlParameter("@P_BANKADDRESS", BankAddress);
                        objParams[3] = new SqlParameter("@P_COLLEGE_CODE", College_Code);
                        objParams[4] = new SqlParameter("@P_BANK_ACCOUNT", Account);
                        objParams[5] = new SqlParameter("@P_BANKNO", bankno);
                        objParams[6] = new SqlParameter("@P_ACTIVE", Active);

                        objParams[7] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("ACD_INSERT_BANK_DETAILS", objParams, true);
                        if (Convert.ToInt32(ret) == 999)
                            retStatus = Convert.ToInt32(CustomStatus.FileExists);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.UpdateOfferedCourse -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int InsertSeatAllot(string Idno, int College_id, string Degreeno, string Branchno, int Convo_No, string Absentia_Student, int Flag)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_IDNO", Idno);
                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", College_id);
                        objParams[2] = new SqlParameter("@P_DEGREENO", Degreeno);
                        objParams[3] = new SqlParameter("@P_BRANCHNO", Branchno);
                        objParams[4] = new SqlParameter("@P_CONVNO", Convo_No);
                        objParams[5] = new SqlParameter("@P_ABSENTIA", Absentia_Student);
                        objParams[6] = new SqlParameter("@P_FLAG", Flag);

                        objParams[7] = new SqlParameter("@P_OUT   ", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("ACD_INSERT_CONVO_SEATALLOT_MANAGEMENT", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.UpdateOfferedCourse -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetBranchChangeData(int session, int College_id, int StudyLevel, string Branchno, string degreeno, int Semester)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_SESSION", session);
                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", College_id);
                        objParams[2] = new SqlParameter("@P_UA_SECTION", StudyLevel);
                        objParams[3] = new SqlParameter("@P_BRANCHNO", Branchno);
                        objParams[4] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[5] = new SqlParameter("@P_SEMESTER", Semester);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_BRANCH_CHANGE_DATA", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetCourseCreation-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetOnlineTrasactionOnlineOrderID(int idno, string orderid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_ORDER_ID", orderid);
                        ds = objDataAccess.ExecuteDataSetSP("PKG_ONLINE_ERP_ORDER_ID", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.GetSingleSession-> " + ex.ToString());
                    }
                    return ds;
                }

                public int InsertScholarshipUploadAmount(int Idno, string RegNo, int Recon)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_IDNO ", Idno);
                        objParams[1] = new SqlParameter("@P_REGNO", RegNo);
                        objParams[2] = new SqlParameter("@P_RECON", Recon);

                        objParams[3] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("[PKG_ACD_SCHOLARSHIP_UPDATE_AMOUNT]", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2627)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.UpdateOfferedCourse -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetElectiveCourse(int idno, int SESSIONNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", SESSIONNO);
                        ds = objSQLHelper.ExecuteDataSetSP("[ACD_GET_CHANGE_ELECTIVE_MODULE]", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetCourseCreation-> " + ex.ToString());
                    }
                    return ds;
                }

                public int InsertTransferModuleMapping(int SESSIONNO, string oldreg, string newreg, int Idno, int Old_degree, int New_degree, int Old_branch, int New_branch,
                                   int Old_CourseNo, int NewCourseNo, int Mapping_status, int Ua_No, int OLSCHEME, int newscheme, string oldccode, string newccode, decimal oldcredit, decimal newcredit,
                                   string ipadress, string collegecode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[21];
                        objParams[0] = new SqlParameter("@P_IDNO ", Idno);
                        objParams[1] = new SqlParameter("@P_OLD_DEGREENO", Old_degree);
                        objParams[2] = new SqlParameter("@P_NEW_DEGREENO", New_degree);
                        objParams[3] = new SqlParameter("@P_OLD_BRANCHNO ", Old_branch);
                        objParams[4] = new SqlParameter("@P_NEW_BRANCHNO", New_branch);
                        objParams[5] = new SqlParameter("@P_OLD_COURSE ", Old_CourseNo);
                        objParams[6] = new SqlParameter("@P_NEW_COURSE", NewCourseNo);
                        objParams[7] = new SqlParameter("@P_MAPPING_STATUS", Mapping_status);
                        objParams[8] = new SqlParameter("@P_SESSION", SESSIONNO);
                        objParams[9] = new SqlParameter("@P_UA_NO", Ua_No);
                        objParams[10] = new SqlParameter("@P_OLDREGNO", oldreg);
                        objParams[11] = new SqlParameter("@P_NEWREGNO", newreg);
                        objParams[12] = new SqlParameter("@P_OLD_SCHEMENO", OLSCHEME);
                        objParams[13] = new SqlParameter("@P_NEW_SCHEMENO", newscheme);
                        objParams[14] = new SqlParameter("@P_OLD_CCODE", oldccode);
                        objParams[15] = new SqlParameter("@P_NEW_CCODE", newccode);
                        objParams[16] = new SqlParameter("@P_OLD_CREDITS", oldcredit);
                        objParams[17] = new SqlParameter("@P_NEW_CREDITS", newcredit);
                        objParams[18] = new SqlParameter("@P_IPADDRESS", ipadress);
                        objParams[19] = new SqlParameter("@P_COLLEGE_CODE", collegecode);
                        objParams[20] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[20].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_TRANSFER_MODULE_MAPPING", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2627)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.UpdateOfferedCourse -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet CourseBindDropDownList(int DeptNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_DEPTNO", DeptNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ALL_COURSES_BINDLISTVIEW", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetAllAlertInfo-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet CourseBindTEXT_BOX(int COURSE)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COURSENO", COURSE);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ALL_COURSES_BIND_LABLE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetAllAlertInfo-> " + ex.ToString());
                    }
                    return ds;
                }

                public int InsertLicChangeCourse(int Sessionno, int OldLic, int NewLic, int CourseNo, int SemesterNo, string CCode, int Ua_no)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", Sessionno);
                        objParams[1] = new SqlParameter("@P_OLDLIC", OldLic);
                        objParams[2] = new SqlParameter("@P_NEWLIC", NewLic);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", SemesterNo);
                        objParams[4] = new SqlParameter("@P_COURSENO", CourseNo);
                        objParams[5] = new SqlParameter("@P_CCODE", CCode);
                        objParams[6] = new SqlParameter("@P_UANO", Ua_no);

                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_LIC_CHANGE_COURSE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else if (Convert.ToInt32(ret) == 2627)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.UpdateOfferedCourse -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int InsertModuleMapping(int deptno, string existingcourseno, string existingccode, string existingcouname, string mappcousrno, string mappccode, string mappcoursename, int createdby, int STATUS)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_DEPTNO", deptno);
                        objParams[1] = new SqlParameter("@P_OLDCOURSENO", existingcourseno);
                        objParams[2] = new SqlParameter("@P_OLDCCODE", existingccode);
                        objParams[3] = new SqlParameter("@P_OLDCOURSENAME", existingcouname);
                        objParams[4] = new SqlParameter("@P_NEWCOURSENO", mappcousrno);
                        objParams[5] = new SqlParameter("@P_NEWCCODE", mappccode);
                        objParams[6] = new SqlParameter("@P_NEWCOURSENAME", mappcoursename);
                        objParams[7] = new SqlParameter("@P_CREATED_BY", createdby);
                        objParams[8] = new SqlParameter("@P_STATUS", STATUS);
                        objParams[9] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_MODULE_MAPPING", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.UpdateOfferedCourse -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int InsertCritetriaName(string Criteria_Name, int AL_OL_TYPE, int Examination, int MinPass, string Combination_Type, int AnyMinGrade, DataTable SubjectGarde, DataTable AddSubjectGarde, int Crite_no)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_CRITERIA_NAME ", Criteria_Name);
                        objParams[1] = new SqlParameter("@P_AL_OL_TYPE", AL_OL_TYPE);
                        objParams[2] = new SqlParameter("@P_EXAMINATION", Examination);
                        objParams[3] = new SqlParameter("@P_MINPASS ", MinPass);
                        objParams[4] = new SqlParameter("@P_COMBINATION_TYPE", Combination_Type);
                        objParams[5] = new SqlParameter("@P_ANYMINGRADE", AnyMinGrade);
                        objParams[6] = new SqlParameter("@P_SUBJECTGRADE", SubjectGarde);
                        objParams[7] = new SqlParameter("@P_CRITE_NO", Crite_no);
                        objParams[8] = new SqlParameter("@P_ADDSUBJECTGRADE", AddSubjectGarde);
                        objParams[9] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_CRITERIA_RULE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2627)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.InsertCritetriaName -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int InsertCriteriaRuleName(string CriteriaRule_Name, DataTable criteriaRule, int CirteRuleNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_CRITERIA_RULE_NAME ", CriteriaRule_Name);
                        objParams[1] = new SqlParameter("@P_CRITE_NAME", criteriaRule);
                        objParams[2] = new SqlParameter("@P_CRITE_NO", CirteRuleNo);

                        objParams[3] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_CRITERIA_RULE_NAME", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2627)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.InsertCriteriaRuleName -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetRuleCreateData(int CriteNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_CRITE_RULENO", CriteNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_CRITERIA_RULE_ALLOCATION", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetRuleCreateData-> " + ex.ToString());
                    }
                    return ds;
                }

                public int InsertCriteriaRuleAllocation(int AdmBatch, int College_id, int UgPg, int DegreeNo, int Branchno, int CriteriaRule, int CriteAlloNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_ADMBATCH ", AdmBatch);
                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", College_id);
                        objParams[2] = new SqlParameter("@P_UGPG", UgPg);
                        objParams[3] = new SqlParameter("@P_DEGREENO ", DegreeNo);
                        objParams[4] = new SqlParameter("@P_BRANCHNO", Branchno);
                        objParams[5] = new SqlParameter("@P_CRITERIA_RULE", CriteriaRule);
                        objParams[6] = new SqlParameter("@P_CRITERIA_ALLC_NO", CriteAlloNo);

                        objParams[7] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_CRITERIA_RULE_ALLOCATION", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2627)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.InsertCriteriaRuleAllocation -> " + ex.ToString());
                    }

                    return retStatus;
                }

                //added by aashna 14-04-2022
                public int UpdateModuleMapping(int srno, int deptno, string existingcourseno, string existingccode, string existingcouname, int mappcousrno, string mappccode, string mappcoursename, int modifiedby,
                    int oldmapcourse, string oldmapccode, string oldmapcoursename, int STATUS)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[14];
                        objParams[0] = new SqlParameter("@P_DEPTNO", deptno);
                        objParams[1] = new SqlParameter("@P_OLDCOURSENO", existingcourseno);
                        objParams[2] = new SqlParameter("@P_OLDCCODE", existingccode);
                        objParams[3] = new SqlParameter("@P_OLDCOURSENAME", existingcouname);
                        objParams[4] = new SqlParameter("@P_NEWCOURSENO", mappcousrno);
                        objParams[5] = new SqlParameter("@P_NEWCCODE", mappccode);
                        objParams[6] = new SqlParameter("@P_NEWCOURSENAME", mappcoursename);
                        objParams[7] = new SqlParameter("@P_MODIFIED_BY", modifiedby);
                        objParams[8] = new SqlParameter("@P_SRNO", srno);
                        objParams[9] = new SqlParameter("@P_OLDMAPCOURSENO", oldmapcourse);
                        objParams[10] = new SqlParameter("@P_OLDMAPCCODE", oldmapccode);
                        objParams[11] = new SqlParameter("@P_OLDMAPCOURSENAME", oldmapcoursename);
                        objParams[12] = new SqlParameter("@P_STATUS", STATUS);
                        objParams[13] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[13].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_MODULE_MAPPING", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.UpdateOfferedCourse -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetModuleList()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_MODULE_MAPPING_DATA", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.GetStudentForFaculty-> " + ex.ToString());
                    }
                    return ds;
                }

                public int AddAcademicSession(string Academic_name, int status, int Academicno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_ACADEMIC_NAME", Academic_name);
                        objParams[1] = new SqlParameter("@P_STATUS", status);
                        objParams[2] = new SqlParameter("@P_ACADEMICNO", Academicno);

                        objParams[3] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_ACADEMIC_SESSION", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.UpdateOfferedCourse -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet COURSELISTHODEXCEL_REPORT(int Session, int Courseno, int College_id, int Semesterno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", Session);
                        objParams[1] = new SqlParameter("@P_COURSENO", Courseno);
                        objParams[2] = new SqlParameter("@P_COLLEGE_ID", College_id);
                        objParams[3] = new SqlParameter("@P_SEMSETERNO", Semesterno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_FACULTY_WISE_SEMESTER_WISE_COURSE_LIST_HOD_EXCEL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetAllAlertInfo-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet COURSELISTCOMMITTEE(int Session, int Courseno, int College_id, int Semesterno, int Campus)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", Session);
                        objParams[1] = new SqlParameter("@P_COURSENO", Courseno);
                        objParams[2] = new SqlParameter("@P_COLLEGE_ID", College_id);
                        objParams[3] = new SqlParameter("@P_SEMSETERNO", Semesterno);
                        objParams[4] = new SqlParameter("@P_CAMPUS", Campus);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_FACULTY_WISE_SEMESTER_WISE_COURSE_LIST_COMMITTEE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetAllAlertInfo-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet COURSELISTCOMMITTEE_MARKS(int Session, int Courseno, int College_id, int Semesterno, int Campus)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", Session);
                        objParams[1] = new SqlParameter("@P_COURSENO", Courseno);
                        objParams[2] = new SqlParameter("@P_COLLEGE_ID", College_id);
                        objParams[3] = new SqlParameter("@P_SEMSETERNO", Semesterno);
                        objParams[4] = new SqlParameter("@P_CAMPUS", Campus);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_FACULTY_WISE_SEMESTER_WISE_COURSE_LIST__COMMITTEE_MARKS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetAllAlertInfo-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetSeemesterPerformanceData(int Session, int College_id, int Degreeno, int Branchno, int Semesterno, int GradePoint, int Operator, string Value)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", Session);
                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", College_id);
                        objParams[2] = new SqlParameter("@P_DEGREENO", Degreeno);
                        objParams[3] = new SqlParameter("@P_BRANCHNO", Branchno);
                        objParams[4] = new SqlParameter("@P_SEMESTERNO", Semesterno);
                        objParams[5] = new SqlParameter("@P_GRADE_POINT", GradePoint);
                        objParams[6] = new SqlParameter("@P_OPERATOR", Operator);
                        objParams[7] = new SqlParameter("@P_VALUE", Value);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_SEMESTER_PERFORMANCE_DATA", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetAllAlertInfo-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataTableReader GetOfferdPerLic(int Course, int sessionno, string modulelic)
                {
                    DataTableReader ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_COURSENO", Course);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[2] = new SqlParameter("@P_MODULELIC", modulelic);
                        ds = objSQLHelper.ExecuteDataSetSP("[PKG_ACA_GET_OFFERD_COURSE_PER_ASSESSMENT_WISE]", objParams).Tables[0].CreateDataReader();
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeesHeadController.GetCourse-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetDegreeBranchCanpaign(int Intake)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UGPGOT", Intake);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_COURSE_DEGREE_CAMPAIGN", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetRuleCreateData-> " + ex.ToString());
                    }
                    return ds;
                }

                public int InsertCampaignData(int Intake, int UGPG, int College_id, int Degreeno, int Branchno, int Afilated, int TotalSIgnUp, int TotalAppFilled, int TotalPaid, int Admitted)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_INTAKE", Intake);
                        objParams[1] = new SqlParameter("@P_UGPG", UGPG);
                        objParams[2] = new SqlParameter("@P_COLLEGE_ID", College_id);
                        objParams[3] = new SqlParameter("@P_DEGREENO", Degreeno);
                        objParams[4] = new SqlParameter("@P_BRANCHNO", Branchno);
                        objParams[5] = new SqlParameter("@P_AFFILATED", Afilated);
                        objParams[6] = new SqlParameter("@P_TOTALSIGNUP", TotalSIgnUp);
                        objParams[7] = new SqlParameter("@P_TOTALAPPFILLED", TotalAppFilled);
                        objParams[8] = new SqlParameter("@P_TOTALPAID", TotalPaid);
                        objParams[9] = new SqlParameter("@P_ADMITTED", Admitted);

                        objParams[10] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_CAMPAIGN_DATA", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.UpdateOfferedCourse -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int InsertStudentAchievementData(int College_id, int Degreeno, int branchno, int Semesterno, string Idno, string Sportno, string SportType, string AchivementNo, int UA_NO)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_COLLEGE_ID", College_id);
                        objParams[1] = new SqlParameter("@P_DEGREENO", Degreeno);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", Semesterno);
                        objParams[4] = new SqlParameter("@P_IDNO", Idno);
                        objParams[5] = new SqlParameter("@P_SPORTS_NO", Sportno);
                        objParams[6] = new SqlParameter("@P_SPORTS_TYPE", SportType);
                        objParams[7] = new SqlParameter("@P_ACHIVEMENTNO", AchivementNo);
                        objParams[8] = new SqlParameter("@P_UANO", UA_NO);

                        objParams[9] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_STUDENT_ACHIEVEMENT", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (Convert.ToInt32(ret) == 2627)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.UpdateOfferedCourse -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int Insert_achivement_data(string Achivement, int AchivementNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_ACHIEVEMENT", Achivement);
                        objParams[1] = new SqlParameter("@P_ACHIEVEMENT_NO", AchivementNo);

                        objParams[2] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_ACHIEVEMENT_NAME", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.UpdateOfferedCourse -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int Insert_Sports_data(string Sport_Name, int SportNo, int SportType)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SPORTNAME", Sport_Name);
                        objParams[1] = new SqlParameter("@P_SPORT_NO", SportNo);
                        objParams[2] = new SqlParameter("@P_SPORT_TYPE", SportType);

                        objParams[3] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_SPORT_NAME", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.UpdateOfferedCourse -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetIntakeMappingData()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_INTAKE_MAPPING_DATA", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetAllAlertInfo-> " + ex.ToString());
                    }
                    return ds;
                }

                public int InsertIntakeMappingData(int AdmBatch, string StudyLevel, string ProgramInterest, int Mappint_ID)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_ADMBATCH", AdmBatch);
                        objParams[1] = new SqlParameter("@P_STUDY_LEVEL", StudyLevel);
                        objParams[2] = new SqlParameter("@P_PROGRAM_INTEREST", ProgramInterest);
                        objParams[3] = new SqlParameter("@P_MAPPING_ID", Mappint_ID);

                        objParams[4] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_INTAKE_MAPPING_DATA", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else if (Convert.ToInt32(ret) == 2627)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.UpdateOfferedCourse -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int UpdateOfferedCourseRequisite(int SchemeNo, string courseNo, int sem, int sessionno, int userno, string Select_status, int requisiteType, int OfferedCourse)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Edit Course
                        objParams = new SqlParameter[9];

                        objParams[0] = new SqlParameter("@P_SEMESTERNO", sem);
                        objParams[1] = new SqlParameter("@P_COURSENOS", courseNo);
                        objParams[2] = new SqlParameter("@P_SCHEMENO", SchemeNo);
                        objParams[3] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[4] = new SqlParameter("@P_SELECT_STATUS", Select_status);
                        objParams[5] = new SqlParameter("@P_UA_NO", userno);

                        objParams[6] = new SqlParameter("@P_REQUISITE_TYPE", requisiteType);
                        objParams[7] = new SqlParameter("@P_OFFERED_COURSE", OfferedCourse);
                        objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_COURSE_SP_UPD_REQUISITE_COURSE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.UpdateOfferedCourseRequisite -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetCourseByScheme(int schemeno, int course, int requisiteType, int semesterno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[1] = new SqlParameter("@P_COURSENO", course);
                        objParams[2] = new SqlParameter("@P_REQUISITE_TYPE", requisiteType);
                        objParams[3] = new SqlParameter("@P_SEMESTER", semesterno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_RET_COURSE_BY_SCHEME", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetCourseByScheme-> " + ex.ToString());
                    }
                    return ds;
                }

                public int InsertConvocationSeatSequence(int Convo, int College_id, int Degree_no, int BranchNo, string SeatSequence)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_CONVO_NO", Convo);
                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", College_id);
                        objParams[2] = new SqlParameter("@P_DEGREENO", Degree_no);
                        objParams[3] = new SqlParameter("@P_BRANCHNO", BranchNo);
                        objParams[4] = new SqlParameter("@P_SEATSEQUENCE", SeatSequence);

                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("ACD_INSERT_CONVOCATION_SEAT_SEQUENCE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.UpdateOfferedCourse -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetConvocationDegreeBranch(int Convo_No)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_CONVO_NO", Convo_No);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ALL_CONVOCATION_DEGREE_BRANCH", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetAllAlertInfo-> " + ex.ToString());
                    }
                    return ds;
                }

                public int InsertBankMapping(int COLLEGE_ID, string BANK_NO, int UA_NO)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParam = null;
                        objParam = new SqlParameter[4];
                        objParam[0] = new SqlParameter("@P_COLLEGE_ID", COLLEGE_ID);
                        objParam[1] = new SqlParameter("@P_BANK_NO", BANK_NO);
                        objParam[2] = new SqlParameter("@P_UA_NO", UA_NO);
                        objParam[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParam[3].Direction = ParameterDirection.Output;
                        object obj = objSQLHelper.ExecuteNonQuerySP("ACD_INSERT_BANK_MAPPING", objParam, true);
                        if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32((CustomStatus.Error));
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Course_Controller.InsertBankMapping-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllCollegeWiseBank(int college_id)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COLLEGE_ID", college_id);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_COLLEGEWISE_BANK", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetAllCollegeWiseBank-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetFacultyWiseBank()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_FACULTY_WISE_BANK", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetFacultyWiseBank-> " + ex.ToString());
                    }
                    return ds;
                }

                public int InsertConvocationSessionAllotmentbulk(int ConvoNo, int Degree, int Branch, int TotalStudent, int Session1, int Session2, int Session3, int Session4, int Session5, int Session6, int Session7, int ConvoSessionNo1, int ConvoSessionNo2, int ConvoSessionNo3, int ConvoSessionNo4, int ConvoSessionNo5, int ConvoSessionNo6, int ConvoSessionNo7)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[19];
                        objParams[0] = new SqlParameter("@P_CONV_NO", ConvoNo);
                        objParams[1] = new SqlParameter("@P_DEGREENO", Degree);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", Branch);
                        objParams[3] = new SqlParameter("@P_TOTALSTUDENT", TotalStudent);
                        objParams[4] = new SqlParameter("@P_SESSION1", Session1);
                        objParams[5] = new SqlParameter("@P_SESSION2", Session2);
                        objParams[6] = new SqlParameter("@P_SESSION3", Session3);
                        objParams[7] = new SqlParameter("@P_SESSION4", Session4);
                        objParams[8] = new SqlParameter("@P_SESSION5", Session5);
                        objParams[9] = new SqlParameter("@P_SESSION6", Session6);
                        objParams[10] = new SqlParameter("@P_SESSION7", Session7);
                        objParams[11] = new SqlParameter("@P_CONVOSESSIONNO1", ConvoSessionNo1);
                        objParams[12] = new SqlParameter("@P_CONVOSESSIONNO2", ConvoSessionNo2);
                        objParams[13] = new SqlParameter("@P_CONVOSESSIONNO3", ConvoSessionNo3);
                        objParams[14] = new SqlParameter("@P_CONVOSESSIONNO4", ConvoSessionNo4);
                        objParams[15] = new SqlParameter("@P_CONVOSESSIONNO5", ConvoSessionNo5);
                        objParams[16] = new SqlParameter("@P_CONVOSESSIONNO6", ConvoSessionNo6);
                        objParams[17] = new SqlParameter("@P_CONVOSESSIONNO7", ConvoSessionNo7);
                        objParams[18] = new SqlParameter("@P_OUT   ", SqlDbType.Int);
                        objParams[18].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_CONVOCATION_SESSION_BULK_ALLOT", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2627)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.UpdateOfferedCourse -> " + ex.ToString());
                    }

                    return retStatus;
                }

                //added by aashna 08-11-2022
                public int InsertGroupingCriteria(int Crt_no, string Crt_Name, string College_id, string Degreeno, string Branchno, string affliated, string campusno, string weeksno, int al_type, string al_stream, int allocation_no, int Uano)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[13];
                        objParams[0] = new SqlParameter("@P_CRIT_NO", Crt_no);
                        objParams[1] = new SqlParameter("@P_CRITERIA_NAME", Crt_Name);
                        objParams[2] = new SqlParameter("@P_COLLEGE_ID", College_id);
                        objParams[3] = new SqlParameter("@P_DEGREENO", Degreeno);
                        objParams[4] = new SqlParameter("@P_BRANCHNO", Branchno);
                        objParams[5] = new SqlParameter("@P_CAMPUSNO", campusno);
                        objParams[6] = new SqlParameter("@P_WEEKSNO", weeksno);
                        objParams[7] = new SqlParameter("@P_AL_TYPE_NO", al_type);
                        objParams[8] = new SqlParameter("@P_AL_STREAM_NO", al_stream);
                        objParams[9] = new SqlParameter("@P_ALLOCATION_NO", allocation_no);
                        objParams[10] = new SqlParameter("@P_AFFILIATED_NO", affliated);
                        objParams[11] = new SqlParameter("@P_UANO", Uano);
                        objParams[12] = new SqlParameter("@P_OUTPUT   ", SqlDbType.Int);
                        objParams[12].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_GROUPING_AUTOMATION_CRITERIA", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2627)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.UpdateOfferedCourse -> " + ex.ToString());
                    }

                    return retStatus;
                }

                //ADDED BY AASHNA 08-11-2022
                public DataSet GetGroupingCriteria()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_GROUPING_AUTOMATION_CRITERIA", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetCourseCreation-> " + ex.ToString());
                    }
                    return ds;
                }

                //ADDED BY AASHNA 22-04-2022

                public int UpdateGroupingCriteria(int criter, string name, string college_id, string degreeno, string branchno, string affliated, string campusno, string weekno, int altype, string stream, int ua_no, int allo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[13];

                        objParams[0] = new SqlParameter("@P_CRITERIA_NO", criter);
                        objParams[1] = new SqlParameter("@P_CRITERIA_NAME", name);
                        objParams[2] = new SqlParameter("@P_COLLEGE_ID", college_id);
                        objParams[3] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[4] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[5] = new SqlParameter("@P_CAMPUSNO", campusno);
                        objParams[6] = new SqlParameter("@P_WEEKSNO", weekno);
                        objParams[7] = new SqlParameter("@P_AL_TYPE_NO", altype);
                        objParams[8] = new SqlParameter("@P_AL_STREAM_NO", stream);
                        objParams[9] = new SqlParameter("@P_ALLOCATION_NO", allo);
                        objParams[10] = new SqlParameter("@P_AFFILIATED_NO", affliated);
                        objParams[11] = new SqlParameter("@P_UANO", ua_no);
                        objParams[12] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[12].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_GROUPING_AUTOMATION_CRITERIA", objParams, false);
                        if (ret != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent_TeachAllot-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //End

                public int InsertBranchChangeData(int idno, int Branch, string Branchno, string lblregno, string lblEnroll, int College_id, int DegreeNo, string degreeno, string IpAddress, int Uano, int Afiliate, int SemesterNo,
                                   int sessionno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[14];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_OLDBRANCHNO", Branch);
                        objParams[2] = new SqlParameter("@P_NEWBRANCHNO", Branchno);
                        objParams[3] = new SqlParameter("@P_REGNO", lblregno);
                        objParams[4] = new SqlParameter("@P_ENROLLNO", lblEnroll);
                        objParams[5] = new SqlParameter("@P_COLLEGE_ID", College_id);
                        objParams[6] = new SqlParameter("@P_OLD_DEGREENO", DegreeNo);
                        objParams[7] = new SqlParameter("@P_NEW_DEGREENO", degreeno);
                        objParams[8] = new SqlParameter("@P_IPADDRESS", IpAddress);
                        objParams[9] = new SqlParameter("@P_UANO", Uano);
                        objParams[10] = new SqlParameter("@P_AFFILIATED_NO", Afiliate);
                        objParams[11] = new SqlParameter("@P_SEMESTERNO", SemesterNo);
                        objParams[12] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[13] = new SqlParameter("@P_OUTPUT   ", SqlDbType.Int);
                        objParams[13].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("ACD_INSERT_BRANCH_CHANGE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2627)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.UpdateOfferedCourse -> " + ex.ToString());
                    }

                    return retStatus;
                }

                //addded by aashna 14-12-2022
                public DataSet GetExaminer(int Sessinno, int Courseno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", Sessinno);
                        objParams[1] = new SqlParameter("@P_COURSENO", Courseno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_EXAMINER_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetAllAlertInfo-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet getAbsentStudEntry(int Sessionno, int courseno, int Collegeid, int semesterno, int commandtype)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", Sessionno);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("@P_COLLEGE_ID", Collegeid);
                        objParams[3] = new SqlParameter("@P_SEMSETERNO", semesterno);
                        objParams[4] = new SqlParameter("@P_COMMAND_TYPE", commandtype);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_FACULTY_WISE_SEMESTER_WISE_COURSE_LIST_FOR_ABSENT_STUDENT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetCourseCreation-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet RetrieveCourseMasterDataForExcel()
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_COURSE_DATA_EXCEL_BLANKSHEET", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetAllSubModuleDetails-> " + ex.ToString());
                    }

                    return ds;
                }

                public int SaveExcelSheetCourseDataInDataBase(Course objc)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_COURSE_NAME", objc.CourseName);
                        objParams[1] = new SqlParameter("@P_SHORTNAME", objc.CourseShortName);
                        objParams[2] = new SqlParameter("@P_CCODE", objc.CCode);
                        objParams[3] = new SqlParameter("@P_LEC", objc.Lecture);
                        objParams[4] = new SqlParameter("@P_LAB", objc.Lab_Lecture);
                        objParams[5] = new SqlParameter("@P_CREDITS", objc.Credits);
                        objParams[6] = new SqlParameter("@P_ELECT", objc.Elect);
                        objParams[7] = new SqlParameter("@P_SUBID", objc.SubID);
                        //      objParams[6] = new SqlParameter("@P_SEMESTERNO", objc.SemesterNo);
                        objParams[8] = new SqlParameter("@P_SCHEMENO", objc.SchemeNo);
                        objParams[9] = new SqlParameter("@P_DEPTNO", objc.Deptno);
                        objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_UPLOAD_STUD_NEW_COURSE_DATA_EXCEL", objParams, true);
                        if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                        {
                            if (obj.ToString() == "1")
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            else if (obj.ToString() == "2")
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.SaveExcelSheetDataInDataBase() -> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAttendanceConfigData(int sessionno, int college_id, int userno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", college_id);
                        objParams[2] = new SqlParameter("@P_UA_NO", userno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_ATTENDANCE_CONFIG_DATE_DATA", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.GetAttendanceConfigData-> " + ex.ToString());
                    }
                    return ds;
                }

                public int InsertAdvisingDetails(string Pidno, string PSCode, string PSName, string P_Units, string P_Grade, string CourseNo, string Prev_no, string Aidno, string ASCode, string ASName, string A_Units, string A_Grade, string Addit_no, string Remarks, int UANO, int NotExempted)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Edit Course
                        objParams = new SqlParameter[17];

                        objParams[0] = new SqlParameter("@P_PIDNO", Pidno);
                        objParams[1] = new SqlParameter("@P_PCCODE", PSCode);
                        objParams[2] = new SqlParameter("@P_PCOURSENAME", PSName);
                        objParams[3] = new SqlParameter("@P_PUNIT", P_Units);
                        objParams[4] = new SqlParameter("@P_PGRADE", P_Grade);
                        objParams[5] = new SqlParameter("@P_COURSENO", CourseNo);
                        objParams[6] = new SqlParameter("@P_PREV_NO", Prev_no);

                        objParams[7] = new SqlParameter("@P_AIDNO", Aidno);
                        objParams[8] = new SqlParameter("@P_ACCODE", ASCode);
                        objParams[9] = new SqlParameter("@P_ACOURSENAME", ASName);
                        objParams[10] = new SqlParameter("@P_AUNIT", A_Units);
                        objParams[11] = new SqlParameter("@P_AGRADE", A_Grade);
                        objParams[12] = new SqlParameter("@P_ADDIT_NO", Addit_no);
                        objParams[13] = new SqlParameter("@P_REMARK", Remarks);

                        objParams[14] = new SqlParameter("@P_UANO", UANO);
                        objParams[15] = new SqlParameter("@P_NOTEXEMPTED", NotExempted);
                        objParams[16] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[16].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_ADVISING_STUDENT_DETAILS", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.InsertAdvisingDetails -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int InsertAdvisingShifteesDetails(string Pidno, string PSCourseNo, string PSName, string NSCourseNo, string NSName, int old_scheme, int new_scheme, int sessionno, string Prev_no, string Remarks, int UANO, string mapping_id)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Edit Course
                        objParams = new SqlParameter[13];

                        objParams[0] = new SqlParameter("@P_PIDNO", Pidno);
                        objParams[1] = new SqlParameter("@P_PCOURSENO", PSCourseNo);
                        objParams[2] = new SqlParameter("@P_PCOURSENAME", PSName);
                        objParams[3] = new SqlParameter("@P_NCOURSENO", NSCourseNo);
                        objParams[4] = new SqlParameter("@P_NCOURSENAME", NSName);
                        objParams[5] = new SqlParameter("@P_OLD_SCHEMENO", old_scheme);
                        objParams[6] = new SqlParameter("@P_NEW_SCHEMENO", new_scheme);

                        objParams[7] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[8] = new SqlParameter("@P_PREV_NO", Prev_no);
                        objParams[9] = new SqlParameter("@P_REMARK", Remarks);
                        objParams[10] = new SqlParameter("@P_UANO", UANO);
                        objParams[11] = new SqlParameter("@P_MAPPING_ID", mapping_id);

                        objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[12].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_ADVISING_SHIFTEE_STUDENT_DETAILS", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.InsertAdvisingDetails -> " + ex.ToString());
                    }

                    return retStatus;
                }
            }
        }//END: BusinessLayer.BusinessLogic
    }//END: UAIMS
}//END: IITMS