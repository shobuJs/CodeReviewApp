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
            public class MarksEntryController
            {
                private string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public DataSet GetControlSheetNoByCourse(int sessionno, int ua_no, int subid, int courseno, string exam)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_connectionString);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        {
                        new SqlParameter("@P_SESSIONNO", sessionno),
                        new SqlParameter("@P_UA_NO", ua_no),
                        new SqlParameter("@P_SUBID", subid),
                        new SqlParameter("@P_COURSENO", courseno),
                        new SqlParameter("@P_EXAMNAME", exam)
                        };

                        ds = objDataAccess.ExecuteDataSetSP("PKG_COURSE_SP_GET_CONTROLSHEETNO", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.MarksEntryController.GetControlSheetNoByCourse() --> " + ex.Message + " " + ex.StackTrace);
                    }

                    return ds;
                }

                public string MarkEntryResultProc(Exam objExam, string ip, string idno)
                {
                    string status = null;
                    try
                    {
                        SqlParameter[] sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSIONNO",objExam.Sessionno),
                            new SqlParameter("@P_SCHEMENO",objExam.SchemeNo),
                            new SqlParameter("@P_SEMESTER_NO",objExam.SemesterNo),
                            new SqlParameter("@P_IDNO", idno),
                            new SqlParameter("@P_RESULTDATE",System.DateTime.Now),
                            new SqlParameter("@P_IPADDRSSS",ip),

                            new SqlParameter("@P_MSG", status)
                        };
                        sqlParams[sqlParams.Length - 1].SqlDbType = SqlDbType.NVarChar;
                        sqlParams[sqlParams.Length - 1].Size = 10000;
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                        SqlConnection conn = new SqlConnection(_connectionString);
                        //conn.ConnectionTimeout = 1000;
                        SqlCommand cmd = new SqlCommand("PKG_ACD_RESULTPROCESSING_OLD_NITGOA", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 1000;
                        int i;
                        for (i = 0; i < sqlParams.Length; i++)
                            cmd.Parameters.Add(sqlParams[i]);
                        try
                        {
                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            if (conn.State == ConnectionState.Open) conn.Close();
                        }
                        status = cmd.Parameters[6].Value.ToString();
                    }
                    catch (Exception ex)
                    {
                        status = "Error";
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.MarkEntryResultProc() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                public DataSet GetStudentsForMarkEntry(int sessiono, int ua_no, string ccode, int sectionno, int subid, int semesterno, string Exam, int COURSENO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                        objParams[4] = new SqlParameter("@P_SUBID", subid);
                        objParams[5] = new SqlParameter("@P_EXAM", Exam);
                        objParams[6] = new SqlParameter("@P_semesterno", semesterno);
                        objParams[7] = new SqlParameter("@P_COURSENO", COURSENO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUD_GET_STUD_FOR_MARKENTRY", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForMarkEntry-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetStudentsForMarkEntryadmin(int sessiono, int ua_no, string ccode, int sectionno, int subid, string Exam, int schemeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                        objParams[4] = new SqlParameter("@P_SUBID", subid);
                        objParams[5] = new SqlParameter("@P_EXAM", Exam);
                        objParams[6] = new SqlParameter("@P_SCHEMENO", schemeno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUD_GET_STUD_FOR_MARKENTRY_FOR_ADMIN_INTERNAL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForMarkEntry-> " + ex.ToString());
                    }

                    return ds;
                }

                //method to get marks entry status added on[14-sep-2016]

                public DataSet GetCourse_MarksEntryStatus(int sessionno, int ua_no, int subid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_SUBID", subid);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_GET_TEACHER_COURSES_MARKS_ENTRY_STATUS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourseForTeacher-> " + ex.ToString());
                    }

                    return ds;
                }

                public int UpdateMarkEntryTA(int sessionno, int courseno, string ccode, string idnos, string marks, int lock_status, string exam, int th_pr, int ua_no, string ipaddress, string examtype, string title)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                            //Parameters for MARKS
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_COURSENO", courseno),
                            new SqlParameter("@P_CCODE", ccode),
                            new SqlParameter("@P_STUDIDS", idnos),
                            //Mark Fields
                            new SqlParameter("@P_MARKS", marks),
                            //Parameters for Final Lock
                            new SqlParameter("@P_LOCK", lock_status),
                            new SqlParameter("@P_EXAM", exam),
                            //Parameters for ACD_LOCKTRAC TABLE
                            new SqlParameter("@P_TH_PR", th_pr),
                            new SqlParameter("@P_UA_NO", ua_no),
                            new SqlParameter("@P_IPADDRESS", ipaddress),
                            new SqlParameter("@P_EXAMTYPE", examtype),
                            new SqlParameter("@P_TITLE", title),
                            new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUD_INSERT_MARKS_TA", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateMarkEntry(int sessionno, string ccode, string idnos, string marks, int lock_status, string exam, int th_pr, int ua_no, string ipaddress, string examtype, string to_email, string from_email, string smsmobile, int flag, string sms_text, string email_text)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                            //Parameters for MARKS
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            //new SqlParameter("@P_COURSENO", courseno),
                            new SqlParameter("@P_CCODE", ccode),
                            new SqlParameter("@P_STUDIDS", idnos),
                            //Mark Fields
                            new SqlParameter("@P_MARKS", marks),
                            //Parameters for Final Lock
                            new SqlParameter("@P_LOCK", lock_status),
                            new SqlParameter("@P_EXAM", exam),
                            //Parameters for ACD_LOCKTRAC TABLE
                            new SqlParameter("@P_TH_PR", th_pr),
                            new SqlParameter("@P_UA_NO", ua_no),
                            new SqlParameter("@P_IPADDRESS", ipaddress),
                            new SqlParameter("@P_EXAMTYPE", examtype),
                            new SqlParameter("@P_TO_EMAIL", to_email),
                            new SqlParameter("@P_FROM_EMAIL", from_email),
                            new SqlParameter("@P_SMSMOB", smsmobile),
                            new SqlParameter("@P_FLAG", flag),
                            new SqlParameter("@P_SMS_TEXT", sms_text),
                            new SqlParameter("@P_EMAIL_TEXT", email_text),
                            new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUD_INSERT_MARKS", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateMarkEntry(int sessionno, int courseno, string ccode, string idnos, string marks, int lock_status, string exam, int th_pr, int ua_no, string ipaddress, string examtype, int FlagReval, string to_email, string from_email, string smsmobile, int flag, string sms_text, string email_text, string subExam_Name, int SemesterNo, int SectionNo)
                {
                    int retStatus;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                            //Parameters for MARKS
                            new SqlParameter("@P_SESSIONNO", sessionno),

                            //Added By Abhinay Lad [17-07-2019]
                            new SqlParameter("@P_COURSENO", courseno),

                            new SqlParameter("@P_CCODE", ccode),
                            new SqlParameter("@P_STUDIDS", idnos),
                            //Mark Fields
                            new SqlParameter("@P_MARKS", marks),
                            //Parameters for Final Lock
                            new SqlParameter("@P_LOCK", lock_status),
                            new SqlParameter("@P_EXAM", exam),
                            //Parameters for ACD_LOCKTRAC TABLE
                            new SqlParameter("@P_TH_PR", th_pr),
                            new SqlParameter("@P_UA_NO", ua_no),
                            new SqlParameter("@P_IPADDRESS", ipaddress),
                            new SqlParameter("@P_EXAMTYPE", examtype),

                            //Added By Abhinay Lad [17-07-2019]
                            new SqlParameter("@P_FLAGREVAL", FlagReval),

                            new SqlParameter("@P_TO_EMAIL", to_email),
                            new SqlParameter("@P_FROM_EMAIL", from_email),
                            new SqlParameter("@P_SMSMOB", smsmobile),
                            new SqlParameter("@P_FLAG", flag),
                            new SqlParameter("@P_SMS_TEXT", sms_text),
                            new SqlParameter("@P_EMAIL_TEXT", email_text),
                            new SqlParameter("@P_SUB_EXAM", subExam_Name),
                            new SqlParameter("@P_SEMESTERNO", SemesterNo),
                            new SqlParameter("@P_SECTIONNO", SectionNo),
                            new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        ////object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUD_INSERT_MARKS", objParams, true);

                        //retStatus = (int)objSQLHelper.ExecuteNonQuerySP("PKG_STUD_INSERT_MARK_WITH_RULE_abhinay_03092019", objParams, true);
                        retStatus = (int)objSQLHelper.ExecuteNonQuerySP("PKG_STUD_INSERT_MARK_WITH_RULE", objParams, true);
                    }
                    catch (Exception ex)
                    {
                        retStatus = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet CourseEligibilityCheck(int sessionno, string courseccode, int courseno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_CCODE", courseccode),
                            new SqlParameter("@P_COURSENO", courseno),
                        };
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_SP_COURSE_ELIGIBILITY_CHK_MARKENTRY", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.LockUnLockMarkEntry --> " + ex.ToString());
                    }
                    return ds;
                }

                //for course update
                public string MarkEntryResultProcStudent(int session, int schemeno, int semester, string ip, int idno)
                {
                    string status = null;
                    try
                    {
                        SqlParameter[] sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSIONNO",session),
                            new SqlParameter("@P_SCHEMENO",schemeno),
                            new SqlParameter("@P_SEMESTER_NO",semester),
                            new SqlParameter("@P_IDNO", idno),
                            new SqlParameter("@P_RESULTDATE",System.DateTime.Now),
                         new SqlParameter("@P_IPADDRSSS",ip),
                            new SqlParameter("@P_MSG", status)
                        };
                        sqlParams[sqlParams.Length - 1].SqlDbType = SqlDbType.NVarChar;
                        sqlParams[sqlParams.Length - 1].Size = 10000;
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                        SqlConnection conn = new SqlConnection(_connectionString);
                        //conn.ConnectionTimeout = 1000;
                        SqlCommand cmd = new SqlCommand("PKG_ACD_RESULTPROCESSING_NITGOA", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 1000;
                        int i;
                        for (i = 0; i < sqlParams.Length; i++)
                            cmd.Parameters.Add(sqlParams[i]);
                        try
                        {
                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            if (conn.State == ConnectionState.Open) conn.Close();
                        }
                        status = cmd.Parameters[6].Value.ToString();
                    }
                    catch (Exception ex)
                    {
                        status = "Error";
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.MarkEntryResultProc() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                public int GradeAllotment(int sessionno, int courseno, string examtype, string absoluterelative, int degreeno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        //SqlParameter[] objParams = new SqlParameter[] { };
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                            //Parameters for MARKS
                            new SqlParameter("@P_SESIONNO", sessionno),
                            new SqlParameter("@P_COURSENO", courseno),
                            new SqlParameter("@P_DEGREENO", degreeno),
                            new SqlParameter("@P_ABSOLUTERELATIVE",absoluterelative),
                            new SqlParameter("@P_EXAMTYPE", examtype),
                            new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_GRADE_ALLOTMENT_NEW_RELATIVE_13012011", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GradeAllotment --> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int CutOffALlotment(int sessionno, int courseno, string examtype, string absoluterelative, int degreeno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        //SqlParameter[] objParams = new SqlParameter[] { };
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                            //Parameters for MARKS
                            new SqlParameter("@P_SESIONNO", sessionno),
                            new SqlParameter("@P_COURSENO", courseno),
                            new SqlParameter("@P_DEGREENO", degreeno),
                            new SqlParameter("@P_ABSOLUTERELATIVE",absoluterelative),
                            new SqlParameter("@P_EXAMTYPE", examtype),
                            new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_GRADE_CUTTOFF_CAL_RELATIVE", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GradeAllotment --> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetEndExamMarksDataExcel(int sessionno, int courseno, int sectionno, int prev_status, int uano)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("@P_SECTIONNO", sectionno);
                        objParams[3] = new SqlParameter("@P_PREV_STATUS", prev_status);
                        objParams[4] = new SqlParameter("@P_UA_NO ", uano);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_REPORT_GET_STUD_FOR_MARKENTRY_ENDSEM_EXCEL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetEndExamMarksDataExcel-> " + ex.ToString());
                    }

                    return ds;
                }

                public int ProcessResult(int sessionno, int schemeno, int semesterno, int idno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        //SqlParameter[] objParams = new SqlParameter[] { };
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                            //Parameters for MARKS
                            new SqlParameter("@P_SESSION_NO", sessionno),
                            new SqlParameter("@P_SCHEME_NO", schemeno),
                            new SqlParameter("@P_SEMESTER_NO", semesterno),
                            new SqlParameter("@P_IDNO",idno),
                            new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_RESULTPROCESSING_GRADE_NEW", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.ProcessResult --> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetCourseForTeacher(int sessionno, int ua_no, int subid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_SUBID", subid);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_GET_TEACHER_COURSES", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourseForTeacher-> " + ex.ToString());
                    }

                    return ds;
                }

                //added on [10-sep-2016] for marks entry form.
                public DataSet GetONExams_NEW(int COURSENO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COURSENO", COURSENO);
                        ds = objSQLHelper.ExecuteDataSetSP("PR_GET_EXAM_NAMES", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetONExams_NEW-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetCourseDetailsForMarkEntry(int sessionno, int ua_type, int ua_dec, int ua_no, int page_link, int courseno, string ccode)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_UA_TYPE", ua_type);
                        objParams[2] = new SqlParameter("@P_UA_DEC", ua_dec);
                        objParams[3] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[4] = new SqlParameter("@P_PAGE_LINK", page_link);
                        objParams[5] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[6] = new SqlParameter("@P_CCODE", ccode);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_GET_COURSE_FOR_MARKENTRY", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourseDetailsForMarkEntry-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetONExams(int sessionno, int ua_type, int page_link, int courseno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_UA_TYPE", ua_type);
                        objParams[2] = new SqlParameter("@P_PAGE_LINK", page_link);
                        objParams[3] = new SqlParameter("@P_COURSENO", courseno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_EXAM_GET_ON_EXAMS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetONExams-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetCourses_By_ON_EXAMS(int sessiono, int schemeno, int semesterno, string exam, int uano)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[3] = new SqlParameter("@P_EXAM", exam);
                        objParams[4] = new SqlParameter("@P_UA_NO", uano);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_COURSES_BY_ONEXAMS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourseForMarkEntry_Admin-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetCourses_By_ON_EXAMS_MarkEntry(int sessiono, int schemeno, int semesterno, string exam, int uano)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[3] = new SqlParameter("@P_EXAM", exam);
                        objParams[4] = new SqlParameter("@P_UA_NO", uano);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_COURSES_BY_ONEXAMS_MARKENTRY", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourses_By_ON_EXAMS_MarkEntry-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetTeacher_Unlock_Marks(int sessionno, int subid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_SUBID", subid);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_TEACHER_FOR_UNLOCK_MARKS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetTeacher_Unlock_Marks-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetScheme_Unlock_Marks(int sessionno, int subid, int ua_no)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_SUBID", subid);
                        objParams[2] = new SqlParameter("@P_UA_NO", ua_no);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_SCHEME_FOR_UNLOCK_MARKS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetScheme_Unlock_Marks-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetCourse_Unlock_Marks(int sessionno, int subid, int ua_no, int schemeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_SUBID", subid);
                        objParams[2] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[3] = new SqlParameter("@P_SCHEMENO", schemeno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_COURSE_FOR_UNLOCK_MARKS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourse_Unlock_Marks-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetCourse_LockDetails(int sessionno, int courseno, int ua_no, int subid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[3] = new SqlParameter("@P_SUBID", subid);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_EXAM_GET_ON_EXAMS_FOR_COURSE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourse_LockDetails-> " + ex.ToString());
                    }

                    return ds;
                }

                public int LockUnLockMarkEntry(int sessionno, int schemeno, int courseno, int th_pr, int ua_no, bool lock_status, string exams, string ipaddress)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_SCHEMENO", schemeno),
                            new SqlParameter("@P_COURSENO", courseno),
                            new SqlParameter("@P_TH_PR", th_pr),
                            new SqlParameter("@P_UA_NO", ua_no),
                            new SqlParameter("@P_LOCK", lock_status),
                            new SqlParameter("@P_EXAMS", exams),
                            new SqlParameter("@P_IPADDRESS", ipaddress),
                            new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_SP_LOCK_UNLOCK_MARKENTRY", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.LockUnLockMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetONExams(int sessionno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_SP_GET_ONEXAMS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetONExams-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetExamCourses(int sessionno, int schemeno, string exam)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[2] = new SqlParameter("@P_EXAM", exam);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GETEXAM_COURSES", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetExamCourses-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetMarksList(int sessionno, int schemeno, int courseno, int controlsheetno, string exam)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[2] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[3] = new SqlParameter("@P_CONTROLSHEET_NO", controlsheetno);
                        objParams[4] = new SqlParameter("@P_EXAM", exam);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_MARKSHEET_REPORT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetMarksList-> " + ex.ToString());
                    }

                    return ds;
                }

                #region Admin Mark Entry Methods

                //===============================

                public DataSet GetStudentsForMarkEntry_AdminNew(int sessiono, int schemeno, int semesterno, int sectionno, int courseno, string ccode, string exam)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                        objParams[4] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[5] = new SqlParameter("@P_CCODE", ccode);
                        objParams[6] = new SqlParameter("@P_EXAM", exam);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUD_GET_STUD_FOR_MARKENTRY_FOR_ADMIN_NEW", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForMarkEntry_Admin-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetStudentsForMarkEntry_Admin(int sessiono, int courseno, string ccode, string exam)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        objParams[3] = new SqlParameter("@P_EXAM", exam);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUD_GET_STUD_FOR_MARKENTRY_FOR_ADMIN", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForMarkEntry_Admin-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetSchemeForMarkEntry_Admin(int sessiono, int deptno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_DEPTNO", deptno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_SCHEME_FOR_MARKENTRY_ADMIN", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetSchemForMarkEntry_Admin-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetCourseForTeacherEndSem(int sessionno, int ua_no)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        //objParams[2] = new SqlParameter("@P_SUBID", subid);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_GET_TEACHER_COURSES_END_SEM", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourseForTeacherEndSem-> " + ex.ToString());
                    }

                    return ds;
                }

                //Modify Grade
                public DataSet GetStudentsForModifyGradeEntry(int sessiono, int courseno, string ccode, string exam, int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        objParams[3] = new SqlParameter("@P_EXAM", exam);
                        objParams[4] = new SqlParameter("@P_IDNO", idno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUD_GET_STUD_FOR_MARKENTRY_FOR_ADMIN_OLDGRADE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForMarkEntry_Admin-> " + ex.ToString());
                    }

                    return ds;
                }

                //UNLOCK CHANGE GRADE
                public int UnlockChangeGrade(int sessionno, int courseno, string idnos, string ccode, string exam, int th_pr, int ua_no, string ipaddress, string examtype, int opNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                            //Parameters for MARKS
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_COURSENO", courseno),
                            new SqlParameter("@P_STUDIDS",idnos),
                            new SqlParameter("@P_CCODE", ccode),
                            new SqlParameter("@P_EXAM", exam),
                            new SqlParameter("@P_TH_PR", th_pr),
                            new SqlParameter("@P_UA_NO", ua_no),
                            new SqlParameter("@P_IPADDRESS", ipaddress),
                            new SqlParameter("@P_EXAMTYPE", examtype),
                            new SqlParameter("@P_WIN_OPERATOR_NO",opNo)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.InputOutput;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUD_UNLOCK_GRADE_ENTRY_CHANGE", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }

                //MODIFY SAVE PROC UPDATE GRADE ENTRY

                public int UpdateNewGrade(int sessionno, int courseno, int idnos, string grade, string newgrade, int lock_status, int ua_no, string ipaddress)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                            //Parameters for MARKS
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_COURSENO", courseno),
                           // new SqlParameter("@P_CCODE", ccode),
                            new SqlParameter("@P_STUDIDS", idnos),
                            new SqlParameter("@P_OLDGRADE", grade),
                            new SqlParameter("@P_NEWGRADE",newgrade),
                            new SqlParameter("@P_LOCK", lock_status),
                            new SqlParameter("@P_UA_NO", ua_no),
                            new SqlParameter("@P_IPADDRESS", ipaddress),
                            new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_GRADE_CHANGE", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetCourseForMarkEntry_Admin(int sessiono, int schemeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GETEXAM_COURSES_ADMIN", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourseForMarkEntry_Admin-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetExamsForCourse_Admin(int sessiono, int courseno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_EXAM_GET_ON_EXAMS_FOR_COURSE_ADMIN", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetExamsforCourse_Admin-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetControlSheetNoByCourse_Admin(int sessionno, int courseno, string exam)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_connectionString);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        {
                        new SqlParameter("@P_SESSIONNO", sessionno),
                        new SqlParameter("@P_COURSENO", courseno),
                        new SqlParameter("@P_EXAMNAME", exam)
                        };

                        ds = objDataAccess.ExecuteDataSetSP("PKG_COURSE_SP_GET_CONTROLSHEETNO_ADMIN", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.MarksEntryController.GetControlSheetNoByCourse_Admin() --> " + ex.Message + " " + ex.StackTrace);
                    }

                    return ds;
                }

                public DataSet GetMarkEntryIncompleteSubjOpr1(int firstOprNo, int secondOprNo, int sessionNo, int schemeNo, string examName)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_connectionString);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_OPERATOR1", firstOprNo),
                            new SqlParameter("@P_OPERATOR2", secondOprNo),
                            new SqlParameter("@P_SESSION_NO", sessionNo),
                            new SqlParameter("@P_SCHEME_NO", schemeNo),
                            new SqlParameter("@P_EXAM_NAME", examName)
                        };

                        ds = objDataAccess.ExecuteDataSetSP("PKG_WIN_MARK_ENTRY_GET_INCOMPLETE_SUBJ1", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.MarksEntryController.GetMarkEntryIncompleteSubjOpr1() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public DataSet GetMarkEntryIncompleteSubjOpr2(int firstOprNo, int secondOprNo, int sessionNo, int schemeNo, string examName)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_connectionString);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_OPERATOR1", firstOprNo),
                            new SqlParameter("@P_OPERATOR2", secondOprNo),
                            new SqlParameter("@P_SESSION_NO", sessionNo),
                            new SqlParameter("@P_SCHEME_NO", schemeNo),
                            new SqlParameter("@P_EXAMNAME", examName)
                        };
                        ds = objDataAccess.ExecuteDataSetSP("PKG_WIN_MARK_ENTRY_GET_INCOMPLETE_SUBJ2", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.MarksEntryController.GetMarkEntryIncompleteSubjOpr2() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public int LockAB_CC(int sessionno, int courseno, int lck)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_COURSENO", courseno),
                            new SqlParameter("@P_LOCK", lck)
                        };

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_LOCK_ABSENT_COPYCASE", objParams, false);

                        if (ret != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.LockAB_CC --> " + ex.ToString());
                    }
                    return retStatus;
                }

                #endregion Admin Mark Entry Methods

                public DataSet GetCourseForTeacherForAttendance(int sessionno, int ua_no, int subid, int thpr, int sectionNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_SUBID", subid);
                        objParams[3] = new SqlParameter("@P_THPR", thpr);
                        objParams[4] = new SqlParameter("@P_SECTIONNO", sectionNo);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_GET_TEACHER_COURSES_ATTENDANCE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourseForTeacher-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetCourseForTeacherForAttendancealt(int sessionno, int ua_no, int subid, int thpr, int sectionNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_SUBID", subid);
                        objParams[3] = new SqlParameter("@P_THPR", thpr);
                        objParams[4] = new SqlParameter("@P_SECTIONNO", sectionNo);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_GET_TEACHER_COURSES_ATTENDANCE_altcourses", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourseForTeacher-> " + ex.ToString());
                    }

                    return ds;
                }

                //LOCK UNLOCK MARKS --testmark
                /// <summary>
                /// This controller is used to Lock or Unlock Test marks entry.
                /// Page : LockMarksByScheme.aspx
                /// </summary>
                /// <param name="sessionno"></param>
                /// <param name="exam"></param>
                /// <param name="test"></param>
                /// <param name="schemeno"></param>
                /// <param name="semesterno"></param>
                /// <param name="sectionno"></param>
                /// <param name="courseno"></param>
                /// <param name="subid"></param>
                /// <param name="un_no"></param>
                /// <param name="lock_status"></param>
                /// <param name="ipaddress"></param>
                /// <param name="ldate"></param>
                /// <returns></returns>

                public int LockUnLockMarkEntryByAdmin(int sessionno, int semester, int schemeno, int examtype, int courseno, int section, int facultynotheory, int facultynopractical, int lockunlock, string ipaddress, int lock_by, int studtype)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                            //new SqlParameter("@P_COLLEGEID",college_id),
                            new SqlParameter("@P_SESSIONNO",sessionno),
                            new SqlParameter("@P_SEMESTER",semester),
                            new SqlParameter("@P_SCHEMENO",schemeno),
                            new SqlParameter("@P_EXAMTYPE",examtype),
                            new SqlParameter("@P_COURSENO",courseno),
                            new SqlParameter("@P_SECTION",section),
                            new SqlParameter("@P_UA_NO",facultynotheory),
                            new SqlParameter("@P_UA_NO_PRAC",facultynopractical),
                            new SqlParameter("@P_LOCK",lockunlock),
                            new SqlParameter("@P_IPADDRESS",ipaddress),
                           // new SqlParameter("@P_LDATE",ldate),
                           // new SqlParameter("@P_EXAMNO",examno),
                            //new SqlParameter("@P_OPID",opid),
                            new SqlParameter("@P_LOCKBY",lock_by),
                            new SqlParameter("@P_STUDTYPE",studtype)
                        };

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_MARK_ENTRY_LOCK_UNLOCK", objParams, false);
                        if (ret != null)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.MarksEntryController.LockUnLockMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int LockUnLockMarkEntryByAdminstudentwise(int sessionno, int semester, int schemeno, int examtype, int courseno, int lockunlock, string ipaddress, int lock_by, int idno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                            //new SqlParameter("@P_COLLEGEID",college_id),
                            new SqlParameter("@P_SESSIONNO",sessionno),
                            new SqlParameter("@P_SEMESTER",semester),
                            new SqlParameter("@P_SCHEMENO",schemeno),
                            new SqlParameter("@P_EXAMTYPE",examtype),
                            new SqlParameter("@P_COURSENO",courseno),
                            new SqlParameter("@P_LOCK",lockunlock),
                            new SqlParameter("@P_IPADDRESS",ipaddress),
                           // new SqlParameter("@P_LDATE",ldate),
                           // new SqlParameter("@P_EXAMNO",examno),
                            //new SqlParameter("@P_OPID",opid),
                            new SqlParameter("@P_LOCKBY",lock_by),
                             new SqlParameter("@P_IDNO",idno)
                        };

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_MARK_ENTRY_LOCK_UNLOCK_STUDENT_WISE", objParams, false);
                        if (ret != null)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.MarksEntryController.LockUnLockMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }

                /// <summary>
                /// This controller is used to Get Student for Marks entry.
                /// Page : MarkEntryByAdmin.aspx
                /// </summary>
                /// <param name="sessiono"></param>
                /// <param name="courseno"></param>
                /// <param name="ccode"></param>
                /// <param name="exam"></param>
                /// <param name="sectionno"></param>
                /// <returns></returns>

                public DataSet GetStudentsForMarkEntry_Admin(int sessiono, int courseno, string ccode, string exam, int sectionno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        objParams[3] = new SqlParameter("@P_EXAM", exam);
                        objParams[4] = new SqlParameter("@P_SECTIONNO", sectionno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUD_GET_STUD_FOR_MARKENTRY_FOR_ADMIN", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForMarkEntry_Admin-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetAllGrades(int sessionno, int subid, int courseno, int ua_no, int degreeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONO", sessionno);
                        objParams[1] = new SqlParameter("@P_SUBID", subid);
                        objParams[2] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[3] = new SqlParameter("@P_UANO", ua_no);
                        objParams[4] = new SqlParameter("@P_DEGREENO", degreeno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ALL_GRADES", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourseForTeacher-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetCourses_By_ON_EXAMS_MarkEntry_Revaluation(int sessiono, int schemeno, int semesterno, int courseno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[3] = new SqlParameter("@P_COURSENO", courseno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_COURSES_BY_ONEXAMS_MARKENTRY_REVALUATION", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourses_By_ON_EXAMS_MarkEntry_Revaluation-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetCourse_LockStatus(int sessionno, int schemeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_LOCKUNLOCK_STATUS_BY_SCHEME", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourse_LockDetails-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetCourseStatusByUano(int sessionno, int ua_no, int subid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_SUBID", subid);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_GET_FACULTY_MARK_ENTRY_CHECK_BY_UANO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourseForTeacher-> " + ex.ToString());
                    }

                    return ds;
                }

                //UPDATE MARK ENTRY FS ON 11 MAY 2013
                public int UpdateFsMarkEntry(int sessionno, int courseno, int idno, decimal s1mark)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                            //Parameters for MARKS
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_COURSENO", courseno),
                            new SqlParameter("@P_IDNO", idno),
                            //Mark Fields
                            new SqlParameter("@P_S1MARK", s1mark),
                            new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_UPDATE_MARKS_FS", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateFsMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }

                //LOCK MARK ENTRY FS ON 11 MAY 2013
                public int UpdateLockMarkEntryFs(int session, int course, int idno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                            //Parameters for MARKS
                            new SqlParameter("@P_SESSIONNO", session),
                            new SqlParameter("@P_COURSENO", course),
                            new SqlParameter("@P_IDNO", idno),
                            //Mark Fields
                            new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_UPDATE_MARKSLOCK_FS", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateFsMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }

                //Check SPI CPI status
                public DataSet Get_SPI_CPI_Status(int sessionno, int degreeno, int branchno, int schemeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[3] = new SqlParameter("@P_SCHEMENO", schemeno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_EXAM_CPI_SPI_REPORT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.Get_SPI_CPI_Status-> " + ex.ToString());
                    }

                    return ds;
                }

                //Check SPI CPI Semesterwise status
                public DataSet Get_SPI_CPI_Sem_Status(int sessionno, int degreeno, int branchno, int schemeno, int semesterno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[3] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[4] = new SqlParameter("@P_SEMESTERNO", semesterno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_EXAM_CPI_SPI_REPORT_NEW", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.Get_SPI_CPI_Sem_Status-> " + ex.ToString());
                    }

                    return ds;
                }

                public int AddAuditCourse(MarkEntry objMarkEntry, string grade, string idnos, string courseno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_IDNO", idnos);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", objMarkEntry.SessionNo);
                        objParams[2] = new SqlParameter("@P_SCHEMENO", objMarkEntry.SchemeNo);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", objMarkEntry.SemesterNo);
                        objParams[4] = new SqlParameter("@P_COURSENOS", courseno);
                        objParams[5] = new SqlParameter("@P_GRADES", grade);
                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_SP_UPDATE_AUDIT_COURSE_GRADE", objParams, false);
                        if (ret != null)
                            if (ret.ToString() != "-99")
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.MarkEntry.AddAuditCourse-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetAllIncompleteMarkEntry(int sessionno, int schemeno, int semesterno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_connectionString);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        {
                    new SqlParameter("@P_SESSIONNO", sessionno),
                    new SqlParameter("@P_SCHEMENO", schemeno) ,
                    new SqlParameter("@P_SEMESTERNO", semesterno)
                 };
                        ds = objDataAccess.ExecuteDataSetSP("PKG_EXAM_ALL_MARKS_ENTRY_NOT_DONE_RESULT", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.WinAddIn.BusinessLogic.ControllerBase.GetAllIncompleteMarkEntry --> " + ex.Message);
                    }
                    return ds;
                }

                public DataSet GetAllIncompleteLockStatus(int sessionno, int schemeno, int semesterno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_connectionString);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        {
                    new SqlParameter("@P_SESSIONNO", sessionno),
                    new SqlParameter("@P_SCHEMENO", schemeno) ,
                    new SqlParameter("@P_SEMESTERNO", semesterno)
                 };

                        ds = objDataAccess.ExecuteDataSetSP("PKG_EXAM_ALL_ENDSEM_LOCK_ENTRY_NOT_DONE_RESULT", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.WinAddIn.BusinessLogic.ControllerBase.GetAllIncompleteLockStatus --> " + ex.Message);
                    }
                    return ds;
                }

                public DataSet GetStudentData(int sessionno, int degreeno, int schemeno, int semesterno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_connectionString);
                        SqlParameter[] sqlParams = new SqlParameter[] {
                    new SqlParameter("@P_SESSIONNO", sessionno),
                    new SqlParameter("@P_DEGREENO", degreeno),
                    new SqlParameter("@P_SCHEMENO", schemeno),
                    new SqlParameter("@P_SEMESTERNO", semesterno),
                };
                        ds = objDataAccess.ExecuteDataSetSP("PKG_GET_STUDENT_DATA", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.WinAddIn.BusinessLogic.ControllerBase.GetStudentData() --> " + ex.Message);
                    }
                    return ds;
                }

                public int ProcessResultAll(int sessionno, int schemeno, int semesterno, string idno, int exam, string ipAdddress, int colg, int STUDENTTYPE, int ua_no, int Type, string to_email, string from_email, string cc_emails, string smsmobile, int flag, string sms_text, string email_text)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        //SqlParameter[] objParams = new SqlParameter[] { };
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                            //Parameters for MARKS
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_SCHEMENO", schemeno),
                            new SqlParameter("@P_SEMESTER_NO", semesterno),
                            new SqlParameter("@P_IDNO",idno),
                            new SqlParameter("@P_EXAM", exam),
                            new SqlParameter("@P_RESULTDATE",System.DateTime.Now),
                            new SqlParameter("@P_IPADDRSSS",ipAdddress),
                            new SqlParameter("@P_COLLEGE_ID",colg),
                            //new SqlParameter("@P_COLLEGE_CODE",colg),
                            new SqlParameter("@P_RESULT_TYPE",STUDENTTYPE),
                            new SqlParameter("@P_TYPE",Type),
                            new SqlParameter("@P_UA_NO",ua_no),
                            //------added For ACD_RESULTPROCESSING_LOG by Hemanth on [2019-03-25]
                            new SqlParameter("@P_TO_EMAIL", to_email),
                            new SqlParameter("@P_FROM_EMAIL", from_email),
                            new SqlParameter("@P_CC_EMAILS", cc_emails),
                            new SqlParameter("@P_SMSMOBILE", smsmobile),
                            new SqlParameter("@P_FLAG", flag),
                            new SqlParameter("@P_SMS_TEXT", sms_text),
                            new SqlParameter("@P_EMAIL_TEXT", email_text),
                            //---------------------------------------------------
                            new SqlParameter("@P_MSG", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_RESULTPROCESSING_JSS", objParams, true); //Added by roshan 28-12-2016

                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.ProcessResult --> " + ex.ToString());
                    }
                    return retStatus;
                }

                // FOR RESULT PROCESS OF PC
                public int ProcessResultPCAll(int sessionno, int schemeno, int semesterno, int exam, string idno, string ipAdddress, int colg)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        //SqlParameter[] objParams = new SqlParameter[] { };
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                            //Parameters for MARKS
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_SCHEMENO", schemeno),
                            new SqlParameter("@P_SEMESTER_NO", semesterno),
                            new SqlParameter("@P_EXAM", exam),
                            new SqlParameter("@P_IDNO",idno),
                             new SqlParameter("@P_RESULTDATE",System.DateTime.Now),
                             new SqlParameter("@P_IPADDRSSS",ipAdddress),
                             new SqlParameter("@P_COLLEGE_ID",colg),
                            new SqlParameter("@P_MSG", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_RESULTPROCESSING_MZU_PC", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.ProcessResult --> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int ProcessResultAllSem(int idno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        //SqlParameter[] objParams = new SqlParameter[] { };
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                            //Parameters for MARKS
                              new SqlParameter("@P_IDNO",idno),

                            new SqlParameter("@P_MSG", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_RESULTPROCESSING_ALLSEM_NITGOA", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.ProcessResult --> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int LockAuditCourse(MarkEntry objMarkEntry, string idnos, string courseno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_IDNO", idnos);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", objMarkEntry.SessionNo);
                        objParams[2] = new SqlParameter("@P_SCHEMENO", objMarkEntry.SchemeNo);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", objMarkEntry.SemesterNo);
                        objParams[4] = new SqlParameter("@P_COURSENOS", courseno);

                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_SP_UPDATE_AUDIT_COURSE_LOCK", objParams, false);

                        if (ret != null)
                            if (ret.ToString() != "-99")
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException

                           ("IITMS.NITPRM.BusinessLayer.BusinessLogic.MarkEntry.LockAuditCourse-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int UnlockAuditCourse(MarkEntry objMarkEntry, string idnos, string courseno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_IDNO", idnos);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", objMarkEntry.SessionNo);
                        objParams[2] = new SqlParameter("@P_SCHEMENO", objMarkEntry.SchemeNo);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", objMarkEntry.SemesterNo);
                        objParams[4] = new SqlParameter("@P_COURSENOS", courseno);

                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_SP_UPDATE_AUDIT_COURSE_UNLOCK", objParams, false);

                        if (ret != null)
                            if (ret.ToString() != "-99")
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException

                           ("IITMS.NITPRM.BusinessLayer.BusinessLogic.MarkEntry.UnlockAuditCourse-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int UpdateMinnorMidLockMarkEntry(int courseno, int sessionno, int schemeno, int uano, int sectionno, string exam, string ipAddress)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                            //Parameters for MARKS
                            new SqlParameter("@P_COURSENO", courseno),
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_SCHEMENO", schemeno),
                            new SqlParameter("@P_UA_NO", uano),
                            new SqlParameter("@P_SECTIONNO", sectionno),
                            new SqlParameter("@P_EXAM", exam),
                            new SqlParameter("@P_IPADDRESS", ipAddress)
                        };
                        //objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_LOCK_MINNOR_MID_MARK_ENTRY", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMinnorMidLockMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetStudentExtmarkDetails(int opid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_connectionString);
                        SqlParameter[] sqlParams = new SqlParameter[] {
                    new SqlParameter("@P_OPID", opid),
                };
                        ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_EXTERNAL_STUD_MARKS", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.WinAddIn.BusinessLogic.ControllerBase.GetStudentData() --> " + ex.Message);
                    }
                    return ds;
                }

                public DataSet GetStudentmarkDetails(int opid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_connectionString);
                        SqlParameter[] sqlParams = new SqlParameter[] {
                    new SqlParameter("@P_OPID", opid),
                };
                        ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GET_STUD_MARKS", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.WinAddIn.BusinessLogic.ControllerBase.GetStudentData() --> " + ex.Message);
                    }
                    return ds;
                }

                public int MarkEntrybyOperator(int opid, string studname, string examname, string marks)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_OPID", opid);
                        objParams[1] = new SqlParameter("@P_STUDIDS", studname);
                        objParams[2] = new SqlParameter("@P_EXAMNAME", examname);
                        objParams[3] = new SqlParameter("@P_MARKS", marks);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_MARK_ENTRY_BY_OPERATOR", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.MarksEntryController.GetControlSheetNoByCourse() --> " + ex.Message + " " + ex.StackTrace);
                    }

                    return retStatus;
                }

                public int LockEntrybyOperator(int opid)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_OPID", opid);

                        objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_LOCK_ENTRY_BY_OPERATOR", objParams, true);
                        if (ret != null && ret.ToString() == "2")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.MarksEntryController.GetControlSheetNoByCourse() --> " + ex.Message + " " + ex.StackTrace);
                    }

                    return retStatus;
                }

                public DataSet GetMatchMarkCompare(int courseno, int status, int DEGREE, int branch, int colg, int sem)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_connectionString);
                        SqlParameter[] sqlParams = new SqlParameter[] {
                        new SqlParameter("@P_courseno", courseno),
                        new SqlParameter("@P_INT_EXT", status),
                        new SqlParameter("@P_DEGREENO", DEGREE),
                        new SqlParameter("@P_BRANCHNO", branch),
                        new SqlParameter("@P_COLLEGENO", colg),
                        new SqlParameter("@P_SEMNO", sem),
                };
                        ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_MARKS_MATCH_BY_OPERATOR", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.WinAddIn.BusinessLogic.ControllerBase.GetStudentData() --> " + ex.Message);
                    }
                    return ds;
                }

                public DataSet GetMarkCompareReport(int courseno, int status, int DEGREE, int branch, int colg, int sem)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_connectionString);
                        SqlParameter[] sqlParams = new SqlParameter[] {
                        new SqlParameter("@P_courseno", courseno),
                        new SqlParameter("@P_INT_EXT", status),
                        new SqlParameter("@P_DEGREENO", DEGREE),
                        new SqlParameter("@P_BRANCHNO", branch),
                        new SqlParameter("@P_COLLEGENO", colg),
                        new SqlParameter("@P_SEMNO", sem),
                };
                        ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_MARKS_MISMATCH_BY_OPERATOR_report", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.WinAddIn.BusinessLogic.ControllerBase.GetStudentData() --> " + ex.Message);
                    }
                    return ds;
                }

                public int UnLockOperatorMarkEntry(string OPID, string IDNOS)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_OPID", OPID),
                            new SqlParameter("@P_IDNOS", IDNOS)
                        };
                        //;objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_MARKS_UNLOCK", objParams, true);
                        if (ret != null)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.LockUnLockMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int MoveOperatorMarkEntry(int sessionno, int opid, int intext)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSIONNO", sessionno),

                            new SqlParameter("@P_opid", opid),
                             new SqlParameter("@P_INT_EXT", intext),
                             new SqlParameter("@P_OUT",SqlDbType.Int)
                         };
                        ; objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPD_STUD_MARKS_RESULT", objParams, true);
                        if (ret != null)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.LockUnLockMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetMarkCompare(int courseno, int COLGID, int status)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_connectionString);
                        SqlParameter[] sqlParams = new SqlParameter[] {
                        new SqlParameter("@P_courseno", courseno),
                        new SqlParameter("@P_COLLEGE_ID", COLGID),
                        new SqlParameter("@P_INT_EXT", status),
                        new SqlParameter("@P_OUTPUT", SqlDbType.Int)
                        };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                        ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_MARKS_MISMATCH_BY_OPERATOR", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.WinAddIn.BusinessLogic.ControllerBase.GetStudentData() --> " + ex.Message);
                    }
                    return ds;
                }

                public int InsertMarkEntryLock(int opid, int LOCKBY, string ipadress)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                            //Parameters for MARKS
                            new SqlParameter("@P_OPID", opid),
                            new SqlParameter("@P_LOCKBY", LOCKBY),
                            new SqlParameter("@P_IPADDRESS", ipadress),

                            new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_MARKS_ENTRY_LOCK_LOG", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetMarkComparewithlock(int courseno, int COLGID, int status)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_connectionString);
                        SqlParameter[] sqlParams = new SqlParameter[] {
                        new SqlParameter("@P_courseno", courseno),
                        new SqlParameter("@P_COLLEGE_ID", COLGID),
                        new SqlParameter("@P_INT_EXT", status),
                    };
                        ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_MARKS_MISMATCH_BY_OPERATOR_lock", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.WinAddIn.BusinessLogic.ControllerBase.GetStudentData() --> " + ex.Message);
                    }
                    return ds;
                }

                public DataSet Getopidstring(int sessiono, int status, int courseno, int colg)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", colg);
                        objParams[2] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[3] = new SqlParameter("@P_INT_EXT", status);

                        //objParams[6] = new SqlParameter("@P_CONTROLSHEET_NO", controlsheetno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_acd_opid_string", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForMarkEntry-> " + ex.ToString());
                    }

                    return ds;
                }

                public int LockAB_CC(int sessionno, int courseno, string ExamNo, string idno, int lck)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_COURSENO", courseno),
                            new SqlParameter("@P_EXAMNO", ExamNo),
                            new SqlParameter("@P_IDNOS", idno),
                            new SqlParameter("@P_LOCK", lck),
                             new SqlParameter("@P_OUT", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_SP_INS_ABSENT_STUDENT_RECORD", objParams, false);

                        if (ret != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.LockAB_CC --> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetresultprocessStudentData(int sessionno, int degreeno, int schemeno, int semesterno, int colgid, int StudentType)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_connectionString);
                        SqlParameter[] sqlParams = new SqlParameter[] {
                    new SqlParameter("@P_SESSIONNO", sessionno),
                    new SqlParameter("@P_DEGREENO", degreeno),
                    new SqlParameter("@P_SCHEMENO", schemeno),
                    new SqlParameter("@P_SEMESTERNO", semesterno),
                    new SqlParameter("@P_collegeid", colgid),
                    new SqlParameter("@P_StudentType", StudentType),
                };
                        ds = objDataAccess.ExecuteDataSetSP("PKG_GET_result_process_STUDENT_DATA", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.WinAddIn.BusinessLogic.ControllerBase.GetStudentData() --> " + ex.Message);
                    }
                    return ds;
                }

                public int PGProcessResultAll(int sessionno, int schemeno, int semesterno, int exam, string idno, string ipAdddress, int colg)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        //SqlParameter[] objParams = new SqlParameter[] { };
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                            //Parameters for MARKS
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_SCHEMENO", schemeno),
                            new SqlParameter("@P_SEMESTER_NO", semesterno),
                            new SqlParameter("@P_EXAM", exam),
                            new SqlParameter("@P_IDNO",idno),
                             new SqlParameter("@P_RESULTDATE",System.DateTime.Now),
                             new SqlParameter("@P_IPADDRSSS",ipAdddress),
                             new SqlParameter("@P_COLLEGE_ID",colg),
                            new SqlParameter("@P_MSG", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_RESULTPROCESSING_MZU_PG", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.ProcessResult --> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetCourseForTeacherForAttendance1(int sessionno, int ua_no, int subid, int sectionNo, int querytype)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_SUBID", subid);
                        objParams[3] = new SqlParameter("@P_SECTIONNO", sectionNo);
                        objParams[4] = new SqlParameter("@P_QUERYTYPE ", querytype);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_GET_TEACHER_COURSES_ATTENDANCE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourseForTeacher-> " + ex.ToString());
                    }

                    return ds;
                }

                public int UpdateExamRegStatus(string idnos, int sess, int scheme, int degree, int branch, int semester, int colgid)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                            //Parameters for MARKS
                            new SqlParameter("@P_IDNOS", idnos),
                            new SqlParameter("@P_SESSIONNO", sess),
                            new SqlParameter("@P_SCHEMENO", scheme),
                            new SqlParameter("@P_DEGREENO", degree),
                            new SqlParameter("@P_BRANCHNO", branch),
                            new SqlParameter("@P_SEMESTERNO", semester),
                            new SqlParameter("@P_COLLEGE_ID", colgid),
                            new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REGIST_UPD_EXAM_REGIST_STATUS", objParams, true);
                        if (ret != null)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateResultlockStatus(string idno, int schemeno, int semester, int sessionno, string username, string ipaddress, int flag, int STUDENTTYPE)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                            //Parameters for MARKS
                            new SqlParameter("@P_IDNO", idno),
                            new SqlParameter("@P_SCHEMENO", schemeno),
                            new SqlParameter("@P_SEMESTERNO", semester),
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_LOCK_UNLOCK_BY", username),
                            new SqlParameter("@P_IPADDRESS", ipaddress),
                            new SqlParameter("@FLAG", flag),
                            new SqlParameter("@P_STUDENTTYPE", STUDENTTYPE),
                            new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_UPD_EXAM_LOCK_STATUS", objParams, true);
                        if (ret != null)
                        {
                            //retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                            retStatus = Convert.ToInt32(ret);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int CheckMarksLocked(int sessionno, string idnos, int schemeno, int semesterno)
                {
                    int Status = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[5];
                        {
                            objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                            objParams[1] = new SqlParameter("@P_IDNO", idnos);
                            objParams[2] = new SqlParameter("@P_SCHEMENO", schemeno);
                            objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterno);
                            objParams[4] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                            objParams[4].Direction = ParameterDirection.Output;
                        };
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_GET_STATUS_MARK_ENTRY_LOCK", objParams, true);
                        Status = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.MarksEntryController.LockUnLockMarkEntry --> " + ex.ToString());
                    }
                    return Status;
                }

                public int UpdateMoveStatus(int sess, int colg, int course, int intext)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                            //Parameters for MARKS
                            new SqlParameter("@P_SESSIONNO", sess),
                            new SqlParameter("@P_COLLEGE_ID", colg),
                             new SqlParameter("@P_COURSENO", course),
                            new SqlParameter("@P_INT_EXT", intext),

                            new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REGIST_UPD_MOVE_STATUS", objParams, true);
                        if (ret != null)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetStudentsForRevalMarksEntry(int sessiono, int courseno, string ccode, string exam, string Status, string valuer)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        objParams[3] = new SqlParameter("@P_EXAM", exam);
                        //objParams[4] = new SqlParameter("@P_IDNO", idno);
                        objParams[4] = new SqlParameter("@P_FLAG", Status);
                        objParams[5] = new SqlParameter("@P_VALUER", valuer);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_STUD_FOR_REVAL_MARK_ENTRY", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForMarkEntry_Admin-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetStudentsForRevalMarksEntry(int sessiono, int courseno, string ccode)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_STUD_FOR_REVAL_MARK_ENTRY", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForMarkEntry_Admin-> " + ex.ToString());
                    }

                    return ds;
                }

                public int ChangeRevalMarks(int sessionno, int courseno, int idnos, string marks,
                               int ua_no, string ipaddress, string remark, string docname, string assmentno, string assmentname)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_COURSENO", courseno),
                            new SqlParameter("@P_STUDIDS", idnos),
                            new SqlParameter("@P_MARKS", marks),
                            new SqlParameter("@P_UA_NO", ua_no),
                            new SqlParameter("@P_IPADDRESS", ipaddress),
                            new SqlParameter("@P_REMARK", remark),
                            new SqlParameter("@P_DOCNAME", docname),
                            new SqlParameter("@P_ASSESSMENT_NO", assmentno),
                            new SqlParameter("@P_ASSESSMENT_COMPONENT_NAME", assmentname),
                            new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_UPD_REVAL_MARK_UG", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UnlockRevalMark(int sessionno, int courseno, string idno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
                         {
                            //Parameters for AUDIT POINTS
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_COURSENO", courseno),
                            new SqlParameter("@P_STUDIDS", idno),
                            new SqlParameter("@P_OUT", SqlDbType.Int)
                         };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_UNLOCK_REVAL_MARK", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.MarksEntryController.UpdateAuditPoint --> " + ex.ToString());
                    }
                    return retStatus;
                }

                //added on [21-10-2016] for marks entry form BY OPERATOR.
                public DataSet GetONExams_NEW_FOR_OPERATOR(int COURSENO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COURSENO", COURSENO);

                        ds = objSQLHelper.ExecuteDataSetSP("PR_GET_EXAM_NAMES_FOR_OPERATOR", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetONExams_NEW-> " + ex.ToString());
                    }
                    return ds;
                }

                //method to get marks entry status added on[21-10-2016]
                public DataSet GetCourse_MarksEntryStatus_FOR_OPERATOR(int sessionno, int ua_no, int subid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_SUBID", subid);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_GET_TEACHER_COURSES_MARKS_ENTRY_STATUS_FOR_OPERATOR", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourseForTeacher-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetCourseForTeacher_FOR_OPERATOR(int sessionno, int ua_no, int subid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_SUBID", subid);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_GET_TEACHER_COURSES_FOR_OPERATOR", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourseForTeacher-> " + ex.ToString());
                    }

                    return ds;
                }

                //Added on [21-10-2016]
                public DataSet GetStudentsForMarkEntry_For_Operator(int sessiono, int ua_no, string ccode, int sectionno, int subid, string Exam)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                        objParams[4] = new SqlParameter("@P_SUBID", subid);
                        objParams[5] = new SqlParameter("@P_EXAM", Exam);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUD_GET_STUD_FOR_MARKENTRY_BY_OPERATOR", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForMarkEntry-> " + ex.ToString());
                    }

                    return ds;
                }

                public int UpdateMarkEntryTA1(int sessionno, int courseno, string idnos, string t1marks, string t2marks, string t3marks, string t4marks, int lock_status, int th_pr, int ua_no, string exam, string ipaddress, string examtype)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                            //Parameters for MARKS
                            new SqlParameter("@IDNOS", idnos),

                            new SqlParameter("@SESSIONNO", sessionno),
                            new SqlParameter("@COURSENO", courseno),

                            //Mark Fields
                            new SqlParameter("@T1MARK", t1marks),
                            new SqlParameter("@T2MARK", t2marks),
                            new SqlParameter("@T3MARK", t3marks),
                            new SqlParameter("@T4MARK", t4marks),

                          // new SqlParameter("@P_CCODE",ccode),
                             new SqlParameter("@P_EXAM",exam),
                            //Parameters for Final Lock
                            new SqlParameter("@P_LOCK", lock_status),
                            new SqlParameter("@UA_NO",ua_no),
                            new SqlParameter("@P_IPADDRESS", ipaddress),
                            new SqlParameter("@P_TH_PR", th_pr),
                            new SqlParameter("@P_EXAMTYPE", examtype),
                            new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUD_UPDATE_MARK_TA", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetStudentsForMarkEntryTA(int sessiono, int ua_no, int courseno, int sectionno, int subid, string Exam, string ipaddress)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                        objParams[4] = new SqlParameter("@P_SUBID", subid);
                        objParams[5] = new SqlParameter("@P_EXAM", Exam);
                        objParams[6] = new SqlParameter("@P_IPADDRESS", ipaddress);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUD_GET_STUD_FOR_MARKENTRY_TA", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForMarkEntry-> " + ex.ToString());
                    }

                    return ds;
                }

                //added new method on date 04/03/2019 to maintain mark unlock alert log;
                public int InsertLogUnlockMark(string smstext, string emailtext, string toemail, string emailfrom, string ccemails, string smsMobnos, int uano, string IpAddress)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_TO_EMAIL", toemail);
                        objParams[1] = new SqlParameter("@P_FROM_EMAIL", emailfrom);
                        objParams[2] = new SqlParameter("@P_CC_EMAILS", ccemails);
                        objParams[3] = new SqlParameter("@P_SMS_CONFIGMOB", smsMobnos);
                        objParams[4] = new SqlParameter("@P_UA_NO", uano);
                        objParams[5] = new SqlParameter("@P_IPADDRESS", IpAddress);
                        objParams[6] = new SqlParameter("@P_SMSTEXT", smstext);
                        objParams[7] = new SqlParameter("@P_EMAILTEXT", emailtext);
                        objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_ALERT_LOG", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.MarksEntryController.InsertLogUnlockMark-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int UpdateMarkEntry(int sessionno, int courseno, string ccode, string idnos, string marks, int lock_status, string exam, int th_pr, int ua_no, string ipaddress, string examtype, int FlagReval, string to_email, string from_email, string smsmobile, int flag, string sms_text, string email_text, string subExam_Name, int SemesterNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                            //Parameters for MARKS
                            new SqlParameter("@P_SESSIONNO", sessionno),

                            //Added By Abhinay Lad [17-07-2019]
                            new SqlParameter("@P_COURSENO", courseno),

                            new SqlParameter("@P_CCODE", ccode),
                            new SqlParameter("@P_STUDIDS", idnos),
                            //Mark Fields
                            new SqlParameter("@P_MARKS", marks),
                            //Parameters for Final Lock
                            new SqlParameter("@P_LOCK", lock_status),
                            new SqlParameter("@P_EXAM", exam),
                            //Parameters for ACD_LOCKTRAC TABLE
                            new SqlParameter("@P_TH_PR", th_pr),
                            new SqlParameter("@P_UA_NO", ua_no),
                            new SqlParameter("@P_IPADDRESS", ipaddress),
                            new SqlParameter("@P_EXAMTYPE", examtype),

                            //Added By Abhinay Lad [17-07-2019]
                            new SqlParameter("@P_FLAGREVAL", FlagReval),

                            new SqlParameter("@P_TO_EMAIL", to_email),
                            new SqlParameter("@P_FROM_EMAIL", from_email),
                            new SqlParameter("@P_SMSMOB", smsmobile),
                            new SqlParameter("@P_FLAG", flag),
                            new SqlParameter("@P_SMS_TEXT", sms_text),
                            new SqlParameter("@P_EMAIL_TEXT", email_text),
                            new SqlParameter("@P_SUB_EXAM", subExam_Name),
                            new SqlParameter("@P_SEMESTERNO", SemesterNo),
                            new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        //object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUD_INSERT_MARKS", objParams, true);
                        retStatus = (int)objSQLHelper.ExecuteNonQuerySP("PKG_STUD_INSERT_MARK_WITH_RULE", objParams, true);
                    }
                    catch (Exception ex)
                    {
                        retStatus = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetStudentsForMarkEntry(int sessiono, int ua_no, string ccode, int sectionno, int subid, int semesterno, string Exam, int COURSENO, string Sub_Exam)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                        objParams[4] = new SqlParameter("@P_SUBID", subid);
                        objParams[5] = new SqlParameter("@P_EXAM", Exam);
                        objParams[6] = new SqlParameter("@P_semesterno", semesterno);
                        objParams[7] = new SqlParameter("@P_COURSENO", COURSENO);
                        objParams[8] = new SqlParameter("@P_SUB_EXAM", Sub_Exam);   // ADDED BY ABHINAY LAD [22-07-2019]

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUD_GET_STUD_FOR_MARKENTRY", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForMarkEntry-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetAllIncompleteLockStatus(int sessionno, int degreeno, int schemeno, int semesterno, int StudentType)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_connectionString);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        {
                     new SqlParameter("@P_SESSIONNO", sessionno),
                    new SqlParameter("@P_DEGREENO", degreeno),
                    new SqlParameter("@P_SCHEMENO", schemeno),
                    new SqlParameter("@P_SEMESTERNO", semesterno),
                    //new SqlParameter("@P_collegeid", colgid),
                    new SqlParameter("@P_StudentType", StudentType),
                 };

                        ds = objDataAccess.ExecuteDataSetSP("PKG_EXAM_ALL_SEM_LOCK_ENTRY_NOT_DONE_RESULT", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.WinAddIn.BusinessLogic.ControllerBase.GetAllIncompleteLockStatus --> " + ex.Message);
                    }
                    return ds;
                }

                // Added by Pritish S. on 29/05/2020

                public DataSet GetCourseForTeacher_Regulationwise(int sessionno, int ua_no, int subid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_SUBID", subid);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_GET_TEACHER_COURSES_REGULATIONWISE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourseForTeacher-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetCourse_MarksEntryStatus_Regulationwise(int sessionno, int ua_no, int subid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_SUBID", subid);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_GET_TEACHER_COURSES_MARKS_ENTRY_STATUS_REGULATIONWISE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetCourseForTeacher-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetONExams_Regulationwise(int sessionno, int ua_type, int page_link, int courseno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_UA_TYPE", ua_type);
                        objParams[2] = new SqlParameter("@P_PAGE_LINK", page_link);
                        objParams[3] = new SqlParameter("@P_COURSENO", courseno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_EXAM_GET_ON_EXAMS_REGULATIONWISE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetONExams-> " + ex.ToString());
                    }
                    return ds;
                }

                public int UpdateMarkEntry_Regulationwise(int sessionno, int courseno, string ccode, string idnos, string marks, int lock_status, string exam, int th_pr, int ua_no, string ipaddress, string examtype, int FlagReval, string to_email, string from_email, string smsmobile, int flag, string sms_text, string email_text, string subExam_Name, int SemesterNo, int SectionNo)
                {
                    int retStatus;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                            //Parameters for MARKS
                            new SqlParameter("@P_SESSIONNO", sessionno),

                            //Added By Abhinay Lad [17-07-2019]
                            new SqlParameter("@P_COURSENO", courseno),

                            new SqlParameter("@P_CCODE", ccode),
                            new SqlParameter("@P_STUDIDS", idnos),
                            //Mark Fields
                            new SqlParameter("@P_MARKS", marks),
                            //Parameters for Final Lock
                            new SqlParameter("@P_LOCK", lock_status),
                            new SqlParameter("@P_EXAM", exam),
                            //Parameters for ACD_LOCKTRAC TABLE
                            new SqlParameter("@P_TH_PR", th_pr),
                            new SqlParameter("@P_UA_NO", ua_no),
                            new SqlParameter("@P_IPADDRESS", ipaddress),
                            new SqlParameter("@P_EXAMTYPE", examtype),

                            //Added By Abhinay Lad [17-07-2019]
                            new SqlParameter("@P_FLAGREVAL", FlagReval),

                            new SqlParameter("@P_TO_EMAIL", to_email),
                            new SqlParameter("@P_FROM_EMAIL", from_email),
                            new SqlParameter("@P_SMSMOB", smsmobile),
                            new SqlParameter("@P_FLAG", flag),
                            new SqlParameter("@P_SMS_TEXT", sms_text),
                            new SqlParameter("@P_EMAIL_TEXT", email_text),
                            new SqlParameter("@P_SUB_EXAM", subExam_Name),
                            new SqlParameter("@P_SEMESTERNO", SemesterNo),
                            new SqlParameter("@P_SECTIONNO", SectionNo),
                            new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        retStatus = (int)objSQLHelper.ExecuteNonQuerySP("PKG_STUD_INSERT_MARK_WITH_RULE_REGULATIONWISE", objParams, true);
                    }
                    catch (Exception ex)
                    {
                        retStatus = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetStudentsForMarkEntry_Regulationwise(int sessiono, int ua_no, string ccode, int sectionno, int subid, int semesterno, string Exam, int COURSENO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                        objParams[4] = new SqlParameter("@P_SUBID", subid);
                        objParams[5] = new SqlParameter("@P_EXAM", Exam);
                        objParams[6] = new SqlParameter("@P_semesterno", semesterno);
                        objParams[7] = new SqlParameter("@P_COURSENO", COURSENO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUD_GET_STUD_FOR_MARKENTRY_REGULATIONWISE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForMarkEntry-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetStudentsForMarkEntry_Regulationwise(int sessiono, int ua_no, string ccode, int sectionno, int subid, int semesterno, string Exam, int COURSENO, string Sub_Exam)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                        objParams[4] = new SqlParameter("@P_SUBID", subid);
                        objParams[5] = new SqlParameter("@P_EXAM", Exam);
                        objParams[6] = new SqlParameter("@P_semesterno", semesterno);
                        objParams[7] = new SqlParameter("@P_COURSENO", COURSENO);
                        objParams[8] = new SqlParameter("@P_SUB_EXAM", Sub_Exam);   // ADDED BY ABHINAY LAD [22-07-2019]

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUD_GET_STUD_FOR_MARKENTRY_REGULATIONWISE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForMarkEntry-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetStudentsForImprovementMarkEntry(int sessiono, int ua_no, string ccode, int sectionno, int subid, int semesterno, string Exam, int COURSENO, string Sub_Exam)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                        objParams[4] = new SqlParameter("@P_SUBID", subid);
                        objParams[5] = new SqlParameter("@P_EXAM", Exam);
                        objParams[6] = new SqlParameter("@P_semesterno", semesterno);
                        objParams[7] = new SqlParameter("@P_COURSENO", COURSENO);
                        objParams[8] = new SqlParameter("@P_SUB_EXAM", Sub_Exam);   // ADDED BY ABHINAY LAD [22-07-2019]

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_STUD_FOR_IMPROVEMENT_MARKENTRY", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForMarkEntry-> " + ex.ToString());
                    }

                    return ds;
                }

                public int UpdateImprovementMarkEntry(int sessionno, int courseno, string ccode, string idnos, string marks, int lock_status, string exam, int th_pr, int ua_no, string ipaddress, string examtype, int FlagReval, string to_email, string from_email, string smsmobile, int flag, string sms_text, string email_text, string subExam_Name, int SemesterNo, int SectionNo)
                {
                    int retStatus;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                            //Parameters for MARKS
                            new SqlParameter("@P_SESSIONNO", sessionno),

                            //Added By Abhinay Lad [17-07-2019]
                            new SqlParameter("@P_COURSENO", courseno),

                            new SqlParameter("@P_CCODE", ccode),
                            new SqlParameter("@P_STUDIDS", idnos),
                            //Mark Fields
                            new SqlParameter("@P_MARKS", marks),
                            //Parameters for Final Lock
                            new SqlParameter("@P_LOCK", lock_status),
                            new SqlParameter("@P_EXAM", exam),
                            //Parameters for ACD_LOCKTRAC TABLE
                            new SqlParameter("@P_TH_PR", th_pr),
                            new SqlParameter("@P_UA_NO", ua_no),
                            new SqlParameter("@P_IPADDRESS", ipaddress),
                            new SqlParameter("@P_EXAMTYPE", examtype),

                            //Added By Abhinay Lad [17-07-2019]
                            new SqlParameter("@P_FLAGREVAL", FlagReval),

                            new SqlParameter("@P_TO_EMAIL", to_email),
                            new SqlParameter("@P_FROM_EMAIL", from_email),
                            new SqlParameter("@P_SMSMOB", smsmobile),
                            new SqlParameter("@P_FLAG", flag),
                            new SqlParameter("@P_SMS_TEXT", sms_text),
                            new SqlParameter("@P_EMAIL_TEXT", email_text),
                            new SqlParameter("@P_SUB_EXAM", subExam_Name),
                            new SqlParameter("@P_SEMESTERNO", SemesterNo),
                            new SqlParameter("@P_SECTIONNO", SectionNo),
                            new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        ////object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUD_INSERT_MARKS", objParams, true);

                        //retStatus = (int)objSQLHelper.ExecuteNonQuerySP("PKG_STUD_INSERT_MARK_WITH_RULE_abhinay_03092019", objParams, true);
                        retStatus = (int)objSQLHelper.ExecuteNonQuerySP("PKG_STUD_INSERT_IMPROVEMENT_MARK_WITH_RULE", objParams, true);
                    }
                    catch (Exception ex)
                    {
                        retStatus = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int CompareInterMarksWithOldMark(int sessionno, int semester, int courseno, int idno)
                {
                    int retStatus;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                            //Parameters for MARKS
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_SEMESTERNO", semester),
                            new SqlParameter("@P_COURSENO", courseno),
                            new SqlParameter("@P_IDNO", idno),
                            new SqlParameter("@P_OUP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        retStatus = (int)objSQLHelper.ExecuteNonQuerySP("PKG_CHECK_IMPROVEMENT_MARKS", objParams, true);
                    }
                    catch (Exception ex)
                    {
                        retStatus = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.CompareInterMarks --> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllSeatPlan1(int session, int examno, int course, int collegeid, int scheme, int semester, int exdtschedno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@p_SESSION", session);
                        objParams[1] = new SqlParameter("@P_EXAMNO", examno);
                        objParams[2] = new SqlParameter("@p_COURSENO", course);
                        //  objParams[3] = new SqlParameter("@P_SLOTNO", slotno);
                        objParams[3] = new SqlParameter("@P_COLLEGE_ID", collegeid); //
                        objParams[4] = new SqlParameter("@P_SCHEMENO", scheme);      //
                        objParams[5] = new SqlParameter("@P_EXAM_SEM", semester);    //
                        objParams[6] = new SqlParameter("@P_EXDT_SCHED_NO", exdtschedno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_SEATING_PLAN_CONFIGURATION_FOR_ABSENT_STUDENT_1", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamController.GetAllExamName-> " + ex.ToString());
                    }
                    return ds;
                }

                public int UNLOCKABSENT(Exam objExam, int idno, int lck, int start, int unlock, int examno, int uano, string ipaddress)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSIONNO", objExam.Sessionno),
                            new SqlParameter("@P_COURSENO",  objExam.Courseno),
                            new SqlParameter("@P_EXAMNO", examno),
                            new SqlParameter("@P_IDNOS", idno),
                            new SqlParameter("@P_LOCK", lck),
                            new SqlParameter("@P_stat", start),
                            new SqlParameter("@P_UNLOCK", unlock),
                            new SqlParameter("@P_UA_NO",uano),
                            new SqlParameter("@P_IPADDRESS",ipaddress),
                            new SqlParameter("@P_OUT", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_SP_INS_ABSENT_STUDENT_RECORD_UNLOCK", objParams, false);

                        if (ret != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.LockAB_CC --> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int LockAB_CC1(Exam objExam, int idno, int lck, int start, int unlock, int examno, int ua_no, string ipadress)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSIONNO", objExam.Sessionno),
                            new SqlParameter("@P_COURSENO",  objExam.Courseno),
                            new SqlParameter("@P_EXAMNO", examno),
                            new SqlParameter("@P_IDNOS", idno),
                            new SqlParameter("@P_LOCK", lck),
                            new SqlParameter("@P_stat", start),
                            new SqlParameter("@P_UNLOCK", unlock),
                            new SqlParameter("@P_UA_NO",ua_no),
                            new SqlParameter("@P_IPADDRESS",ipadress),
                            new SqlParameter("@P_OUT", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_SP_INS_ABSENT_STUDENT_RECORD_1", objParams, false);

                        if (ret != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.LockAB_CC --> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int AddAbsentStudRecord(Exam objExam, int idno, int lck, int stat, int unlock, int examno, int ua_no, string ipadress)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", objExam.Sessionno);

                        objParams[1] = new SqlParameter("@P_EXAMNO", examno);

                        objParams[2] = new SqlParameter("@P_COURSENO", objExam.Courseno);
                        objParams[3] = new SqlParameter("@P_IDNOS", idno);
                        objParams[4] = new SqlParameter("@P_LOCK", lck);
                        objParams[5] = new SqlParameter("@P_stat", stat);
                        objParams[6] = new SqlParameter("@P_UNLOCK", unlock);
                        objParams[7] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[8] = new SqlParameter("@P_IPADDRESS", ipadress);

                        objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);

                        objParams[9].Direction = ParameterDirection.Output;

                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_SP_INS_ABSENT_STUDENT_RECORD_1", objParams, true));
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.AddAbsentStudentsRecord-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetStudentsForRevalReport(int sessiono, int courseno, string ccode, int college_id)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        objParams[3] = new SqlParameter("@P_COLLEGE_ID", college_id);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_STUDENT_FOR_REVAL_REPORT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForMarkEntry_Admin-> " + ex.ToString());
                    }

                    return ds;
                }

                //ADDED BY AASHNA 17-08-2022
                public DataSet GetStudentsForChangeble(int sessiono, int idno, int college_id)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_IDNO", idno);
                        objParams[2] = new SqlParameter("@P_COLLEGE_ID", college_id);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_STUDENT_FOR_REVAL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForMarkEntry_Admin-> " + ex.ToString());
                    }

                    return ds;
                }

                //ADDED BY AASHNA 23-08-2022
                public DataSet GetmarksForrevalStatus(int sessiono, int idno, int courseno, int semesterno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_IDNO", idno);
                        objParams[2] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_MARKS_FOR_REVAL_STATUS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForMarkEntry_Admin-> " + ex.ToString());
                    }

                    return ds;
                }

                public int InsertChangeble(int idno, string status, string courseno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_IDNO", idno),
                            new SqlParameter("@P_CHANGEBLE", status),
                            new SqlParameter("@P_COURESNO", courseno),
                            new SqlParameter("@P_OUTPUT", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_REVAL_STATUS", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }

                //ADDED BY AASHNA 24-08-2022
                public DataSet GetStudentsForMarkChange(int sessiono, int idno, int courseno, string ccode)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_IDNO", idno);
                        objParams[2] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[3] = new SqlParameter("@P_CCODE", ccode);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_MASRKS_ENTRY_DATA", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForMarkEntry_Admin-> " + ex.ToString());
                    }

                    return ds;
                }

                public int LockRevalMarks(int sessionno, int courseno, int idnos, string ccode, int ua_no, string ipaddress, string assname, string assno, string marks, string REMARK)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_COURSENO", courseno),
                            new SqlParameter("@P_CCODE", ccode),
                            new SqlParameter("@P_IDNOS", idnos),
                            new SqlParameter("@P_ENTRY_BY_UANO", ua_no),
                            new SqlParameter("@P_IPADDRESS", ipaddress),
                            new SqlParameter("@P_ASSESSMENT_NO", assno),
                            new SqlParameter("@P_REMARK", REMARK),
                            new SqlParameter("@P_ASSESSMENT_COMPONENT_NAME", assname),
                            new SqlParameter("@P_MARKS", marks),
                            new SqlParameter("@P_OUT", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_SP_GRADE_ALLOTMENT_STUD_WISE_SLIIT_REVAL", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }

                #region Value Added Courses

                // ADDED BY NARESH BEERLA [14-07-2020] FOR VALUE ADDED COURSES

                public DataSet GetStudentsForValueAddedCourses(int sessionno, int degreeno, int branchno, int schemeno, int semesterno, int courseno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[3] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[4] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[5] = new SqlParameter("@P_COURSENO", courseno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_STUD_GET_FOR_VALUE_ADDED_GRADE_ENTRY", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.WinAddIn.BusinessLogic.MarksEntryController.GetStudentsForValueAddedCourses-> " + ex.ToString());
                    }

                    return ds;
                }

                public int SaveStudentGradeForValueAddedCourse(int sessionno, int schemeno, int semesterno, int courseno, string idnos, string grade, int lock_status, int ua_no, string ipaddress)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                            //Parameters for MARKS
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_SCHEMENO", schemeno),
                            new SqlParameter("@P_SEMESTERNO", semesterno),
                            new SqlParameter("@P_COURSENO",courseno),
                            new SqlParameter("@P_GRADE",grade),
                            new SqlParameter("@P_LOCK",lock_status),
                            new SqlParameter("@P_IDNO",idnos),
                            new SqlParameter("@P_UA_NO", ua_no),
                            new SqlParameter("@P_IPADDRESS", ipaddress),
                            new SqlParameter("@P_OUT", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_STUD_SAVE_LOCK_VALUE_ADDED_GRADE_ENTRY", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.MarksEntryController.SaveStudentGradeForValueAddedCourse --> " + ex.ToString());
                    }
                    return retStatus;
                }

                // ADDED BY NARESH BEERLA [14-07-2020] FOR VALUE ADDED COURSES

                #endregion Value Added Courses
            }
        }
    }
}