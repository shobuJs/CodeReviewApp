using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;

//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : STUDENT FEEDBACK CONTROLLER
// CREATION DATE : 24-SEPT-2009
// CREATED BY    : SANJAY RATNAPARKHI
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================
using System;
using System.Data;
using System.Data.SqlClient;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class StudentFeedBackController
            {
                private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public int AddFeedBack(StudentFeedBack SFB)
                {
                    int retStatus = -1;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_IDNO", SFB.Idno);
                        objParams[1] = new SqlParameter("@P_FEEDBACK", SFB.FeedBack);
                        objParams[2] = new SqlParameter("@P_FEEDBACK_DATE", SFB.FeedbackDate);
                        objParams[3] = new SqlParameter("@P_STATUS", SFB.Status);
                        objParams[4] = new SqlParameter("@P_COLLEGE_CODE", SFB.CollegeCode);
                        objParams[5] = new SqlParameter("@P_TOKENNO", SFB.TokenNo);
                        objParams[5].Direction = ParameterDirection.Output;

                        object ret = (objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_FEEDBACK_INSERT", objParams, true));
                        retStatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentFeedBackController.AddFeedBack-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetFeedBackDetailsById(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GETFEEDBACKDETAILSBYID", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.BranchController.GetAllBranchTypes-> " + ex.ToString());
                    }
                    return ds;
                }

                public SqlDataReader GetCourseSelected(int sessionno, int idno)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_IDNO", idno);
                        dr = objSQLHelper.ExecuteReaderSP("PKG_STUDENT_FEEDBACK_COURSE_SELECT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dr;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistration.GetFeedBackQuestion-> " + ex.ToString());
                    }
                    return dr;
                }

                public DataSet GetFeedBackQuestionForMaster(StudentFeedBack SFB)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_CTID", SFB.CTID);
                        objParams[1] = new SqlParameter("@P_SUBID", SFB.SubId);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", SFB.SemesterNo);
                        objParams[3] = new SqlParameter("@P_COLLEGE_ID", SFB.Collegeid);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_STUDENT_GET_FEEDBACK_QUESTION_LIST", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistration.GetFeedBackQuestion-> " + ex.ToString());
                    }
                    return ds;
                }

                public int GetStudentTokenNo(string regno)
                {
                    int idno = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_REGNO", regno);

                        idno = Convert.ToInt32(objSQLHelper.ExecuteScalarSP("PKG_STUDENT_SP_RET_STUDID_BY_REGNO", objParams));
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentFeedBackController.GetStudentIDByRegNo-> " + ex.ToString());
                    }

                    return idno;
                }

                public int AddStudentFeedBackAns(StudentFeedBack SFB)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[24];
                        objParams[0] = new SqlParameter("@P_IDNO", SFB.Idno);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", SFB.SessionNo);
                        objParams[2] = new SqlParameter("@P_IPADDRESS", SFB.Ipaddress);
                        objParams[3] = new SqlParameter("@P_QUESTIONID", SFB.QuestionIds);
                        objParams[4] = new SqlParameter("@P_ANSWERID", SFB.AnswerIds);
                        objParams[5] = new SqlParameter("@P_DATE", SFB.Date);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", SFB.CollegeCode);
                        objParams[7] = new SqlParameter("@P_REMARK", SFB.Remark);
                        objParams[8] = new SqlParameter("@P_COURSENO", SFB.CourseNo);
                        objParams[9] = new SqlParameter("@P_STATUS", SFB.FB_Status);
                        objParams[10] = new SqlParameter("@P_UA_NO", SFB.UA_NO);
                        objParams[11] = new SqlParameter("@P_SUGGESTION_A", SFB.Suggestion_A);
                        objParams[12] = new SqlParameter("@P_SUGGESTION_B", SFB.Suggestion_B);
                        objParams[13] = new SqlParameter("@P_SUGGESTION_C", SFB.Suggestion_C);
                        objParams[14] = new SqlParameter("@P_SUGGESTION_D", SFB.Suggestion_D);
                        objParams[15] = new SqlParameter("@P_OVERALL_IMPRESSION", SFB.OverallImpression);
                        objParams[16] = new SqlParameter("@P_CTID", SFB.CTID);
                        objParams[17] = new SqlParameter("@P_CTID_COMMENTS", SFB.CidComments);
                        objParams[18] = new SqlParameter("@P_FEEDBACK_NO", SFB.FeedbackNo);
                        objParams[19] = new SqlParameter("@P_EXITQUESTIONBESTTEACHER", SFB.ExitQuestionBestTeacher);
                        objParams[20] = new SqlParameter("@P_FROMDEPARTMENT", SFB.FromDepartment);
                        objParams[21] = new SqlParameter("@P_OTHERDEPARTMENT", SFB.OtherDepartment);
                        objParams[22] = new SqlParameter("@P_EXAMNO", SFB.ExamNo);
                        objParams[23] = new SqlParameter("@P_OUT", SFB.Out);
                        objParams[23].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_FEEDBACK_ANSWER_INSERT", objParams, true);
                        retStatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentFeedBackController.AddFeedBack-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllFeedBackQuestionForMaster(int CTID, int SubId, string SemesterNos)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_CTID", CTID);
                        objParams[1] = new SqlParameter("@P_SUBID", SubId);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", SemesterNos);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_STUDENT_GET_ALL_FEEDBACK_QUESTION_MASTER", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistration.GetFeedBackQuestion-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetFeedBackQuestion(StudentFeedBack SFB)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SUBID", SFB.SubId);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_FEEDBACK_ANS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistration.GetFeedBackQuestion-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetEditFeedBack(StudentFeedBack SFB)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_QUESTIONID", SFB.QuestionId);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_FEEDBACK_QUESTION_EDIT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistration.GetFeedBackQuestion-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet RetrieveStudentFeedbackStatus(int idNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idNo);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_STUDENT_FEEDBACK_STATUS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentFeedbackStatus()-> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// Added By Manish on dt. 23-Sept-2015
                /// </summary>
                /// <param name="sessionno"></param>
                /// <param name="idno"></param>
                /// <param name="semesterno"></param>
                /// <param name="schemeno"></param>
                /// <returns></returns>
                public DataSet GetCourseSelected(int sessionno, int idno, int semesterno, int schemeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSIONNO",sessionno),
                            new SqlParameter("@P_IDNO", idno),
                            new SqlParameter("@P_SEMESTERNO", semesterno),
                            new SqlParameter("@P_SCHEMENO",schemeno)
                        };
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_FEEDBACK_COURSE_SELECT_NEXT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistration.GetFeedBackQuestion-> " + ex.ToString());
                    }
                    return ds;
                }

                public int FeedbackCount(int IDNO, int SESSIONMAX)
                {
                    int count = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", SESSIONMAX);
                        objParams[1] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        count = Convert.ToInt16((objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_FEECOLLECTION_FEEDBACK", objParams, true)));
                    }
                    catch (Exception ex)
                    {
                        //retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentFeedBackController.AddFeedbackQuestion-> " + ex.ToString());
                    }
                    return count;
                }

                //Added by Neha Baranwal 22July19

                #region "FeedbackMaster"

                public int AddFeedbackMaster(string feedbackName, string collegeCode, int FEEDBACK)
                {
                    int status = -99;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_FEEDBACK_NAME", feedbackName),
                            new SqlParameter("@P_COLLEGE_CODE", collegeCode),
                            new SqlParameter("@P_FEEDBACK_TYPE", FEEDBACK),
                            new SqlParameter("@P_OUTPUT", status)
                        };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_FEEDBACK_MASTER_INSERT", sqlParams, true);
                        status = (Int32)obj;
                    }
                    catch (Exception ex)
                    {
                        status = -99;            //Added By Abhinay Lad [14-06-2019]
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentFeedBackController.AddFeedbackMaster() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                public int UpdateFeedbackMaster(int feedbackNo, string feedbackName, string collegeCode, int FEEDBACK)
                {
                    int status = -99;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] sqlParams = new SqlParameter[]
                            {
                                 new SqlParameter("@P_FEEDBACK_NO", feedbackNo),
                                 new SqlParameter("@P_FEEDBACK_NAME", feedbackName),
                                 new SqlParameter("@P_COLLEGE_CODE", collegeCode),
                                 new SqlParameter("@P_FEEDBACK_TYPE", FEEDBACK),
                                 new SqlParameter("@P_OUTPUT",status)
                            };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_FEEDBACK_MASTER_UPDATE", sqlParams, true);
                        status = (Int32)obj; //Added By Abhinay Lad [14-06-2019]
                    }
                    catch (Exception ex)
                    {
                        status = -99; //Added By Abhinay Lad [14-06-2019]
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentFeedBackController.UpdateFeedbackMaster() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                public DataSet GetAllFeedback()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_FEEDBACK_MASTER_GET_ALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentFeedBackController.GetAllFeedback() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public SqlDataReader GetFeedbackNo(int feedbackNo)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[] { new SqlParameter("@P_OUTPUT", feedbackNo) };

                        dr = objSQLHelper.ExecuteReaderSP("PKG_ACD_FEEDBACK_MASTER_GET_BY_FEEDBACK_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentFeedBackController.GetFeedbackNo() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return dr;
                }

                #endregion "FeedbackMaster"

                #region "Student Attendnace Conditions For Feedback"

                ///Added by Neha Baranwal 11Oct19
                public DataSet GetCourseWiseAttPercent(int sessionno, int schemeno, int semesterno, int courseno, int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSIONNO",sessionno),
                            new SqlParameter("@P_IDNO", idno),
                            new SqlParameter("@P_SEMESTERNO", semesterno),
                            new SqlParameter("@P_SCHEMENO",schemeno),
                             new SqlParameter("@P_COURSENO",courseno)
                        };
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_COURSE_WISE_STUD_ATTENDANCE_PERCENTAGE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentFeedbackController.GetCourseWiseAttPercent-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetOverallAttPercent(int sessionno, int schemeno, int semesterno, int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSIONNO",sessionno),
                            new SqlParameter("@P_IDNO", idno),
                            new SqlParameter("@P_SEMESTERNO", semesterno),
                            new SqlParameter("@P_SCHEMENO",schemeno),
                        };
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_OVERALL_STUD_ATTENDANCE_FEEDBACK", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentFeedbackController.GetOverallAttPercent-> " + ex.ToString());
                    }
                    return ds;
                }

                public int AddFacultyFeedBackAns(StudentFeedBack SFB)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[11];
                        //objParams[0] = new SqlParameter("@P_IDNO", SFB.Idno);
                        objParams[0] = new SqlParameter("@P_SESSIONNO", SFB.SessionNo);
                        objParams[1] = new SqlParameter("@P_IPADDRESS", SFB.Ipaddress);
                        objParams[2] = new SqlParameter("@P_QUESTIONID", SFB.QuestionIds);
                        objParams[3] = new SqlParameter("@P_ANSWERID", SFB.AnswerIds);
                        objParams[4] = new SqlParameter("@P_DATE", SFB.Date);
                        objParams[5] = new SqlParameter("@P_COLLEGE_CODE", SFB.CollegeCode);
                        //objParam0[7] = new SqlParameter("@P_REMARK", SFB.Remark);
                        // objParams[8] = new SqlParameter("@P_COURSENO", SFB.CourseNo);
                        objParams[6] = new SqlParameter("@P_STATUS", SFB.FB_Status);
                        objParams[7] = new SqlParameter("@P_TO_UA_NO", SFB.TO_UA_NO);
                        //objParams[11] = new SqlParameter("@P_SUGGESTION_A", SFB.Suggestion_A);
                        //objParams[12] = new SqlParameter("@P_SUGGESTION_B", SFB.Suggestion_B);
                        //objParams[13] = new SqlParameter("@P_SUGGESTION_C", SFB.Suggestion_C);
                        //objParams[14] = new SqlParameter("@P_SUGGESTION_D", SFB.Suggestion_D);
                        //objParams[15] = new SqlParameter("@P_OVERALL_IMPRESSION", SFB.OverallImpression);
                        objParams[8] = new SqlParameter("@P_CTID", SFB.CTID);
                        //objParams[17] = new SqlParameter("@P_EXITQUESTIONBESTTEACHER", SFB.ExitQuestionBestTeacher);
                        objParams[9] = new SqlParameter("@P_FROM_UA_NO", SFB.FROM_UA_NO);
                        //objParams[19] = new SqlParameter("@P_OTHERDEPARTMENT", SFB.OtherDepartment);
                        //objParams[20] = new SqlParameter("@P_EXAMNO", SFB.ExamNo);
                        objParams[10] = new SqlParameter("@P_OUT", SFB.Out);
                        objParams[10].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_FACULTY_FEEDBACK_ANSWER_INSERT", objParams, true);
                        retStatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentFeedBackController.AddFeedBack-> " + ex.ToString());
                    }
                    return retStatus;
                }

                #endregion "Student Attendnace Conditions For Feedback"

                // Added the college_id parameter

                public DataSet GetAllFeedBackQuestionForMaster(int CTID, int SubId, string SemesterNos, string College_id)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_CTID", CTID);
                        objParams[1] = new SqlParameter("@P_SUBID", SubId);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", SemesterNos);
                        objParams[3] = new SqlParameter("@P_COLLEGE_ID", College_id);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_STUDENT_GET_ALL_FEEDBACK_QUESTION_MASTER", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistration.GetFeedBackQuestion-> " + ex.ToString());
                    }
                    return ds;
                }

                // find and replace add & Update Feedback questions methods

                public int AddFeedbackQuestion(StudentFeedBack SFB)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[10];
                        //objParams[0] = new SqlParameter("@P_SUBID", SFB.SubId);
                        objParams[0] = (SFB.SubId != 0) ? new SqlParameter("@P_SUBID", SFB.SubId) : new SqlParameter("@P_SUBID", DBNull.Value);
                        objParams[1] = new SqlParameter("@P_CTID", SFB.CTID);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", SFB.SemesterNo);
                        objParams[3] = new SqlParameter("@P_QUESTIONNAME", SFB.QuestionName);
                        objParams[4] = new SqlParameter("@P_COLL_CODE", SFB.CollegeCode);
                        objParams[5] = new SqlParameter("@P_ANS_OPTIONS", SFB.AnsOptions);
                        objParams[6] = new SqlParameter("@P_ANS_VALUES", SFB.Value);
                        objParams[7] = new SqlParameter("@P_ACTIVE_STATUS", SFB.ActiveStatus);
                        objParams[8] = new SqlParameter("@P_COLLEGE_ID", SFB.Collegeid);
                        objParams[9] = new SqlParameter("@P_OUT", SFB.Out);
                        objParams[9].Direction = ParameterDirection.Output;
                        object ret = (objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_FEEDBACK_QUESTION", objParams, true));
                        if (ret.ToString() == "1" && ret != null)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else if (ret.ToString() == "-1001" && ret != null)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
                        }
                        else if (ret.ToString() == "-99")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentFeedBackController.AddFeedbackQuestion-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateFeedbackQuestion(StudentFeedBack SFB)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        //update
                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_QUESTIONID", SFB.QuestionId);
                        //objParams[1] = new SqlParameter("@P_SUBID", SFB.SubId);
                        objParams[1] = (SFB.SubId != 0) ? new SqlParameter("@P_SUBID", SFB.SubId) : new SqlParameter("@P_SUBID", DBNull.Value);
                        objParams[2] = new SqlParameter("@P_CTID", SFB.CTID);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", SFB.SemesterNo);
                        objParams[4] = new SqlParameter("@P_QUESTIONNAME", SFB.QuestionName);
                        objParams[5] = new SqlParameter("@P_ANS_OPTIONS", SFB.AnsOptions);
                        objParams[6] = new SqlParameter("@P_ANS_VALUES", SFB.Value);
                        objParams[7] = new SqlParameter("@P_ACTIVE_STATUS", SFB.ActiveStatus);
                        objParams[8] = new SqlParameter("@P_COLLEGE_ID", SFB.Collegeid);
                        objParams[9] = new SqlParameter("@P_COLL_CODE", SFB.CollegeCode);
                        objParams[10] = new SqlParameter("@P_OUT", SFB.Out);
                        objParams[10].Direction = ParameterDirection.Output;

                        object ret = (objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_FEEDBACK_UPD_QUESTION", objParams, true));
                        if (ret.ToString() == "2" && ret != null)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else if (ret.ToString() == "-1001" && ret != null)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
                        }
                        else if (ret.ToString() == "-99")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.SessionController.UpdateCT-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetCourseSelectedForQuestion(int sessionno, int idno, int semesterno, int schemeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSIONNO",sessionno),
                            new SqlParameter("@P_IDNO", idno),
                            new SqlParameter("@P_SEMESTERNO", semesterno),
                            new SqlParameter("@P_SCHEMENO",schemeno)
                        };
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_FEEDBACK_COURSE_SELECT_NEXT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistration.GetFeedBackQuestion-> " + ex.ToString());
                    }
                    return ds;
                }
            }
        }
    }
}