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
            public class AnswerSheetController
            {
                /// <summary>
                /// ConnectionString
                /// </summary>
                private string _uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                /// <summary>
                /// This controller is used to Get Answersheet Receive & Issue to Faculty
                /// Page: EvaluatorOrder.aspx.aspx
                /// Page: AnswersheetRecieve.aspx.aspx
                /// Page: AnswersheetIssuer.aspxn.aspx
                /// </summary>
                /// <param name="sessionno"></param>
                /// <param name="degreeno"></param>
                /// <param name="branchno"></param>
                /// <param name="schemeno"></param>
                /// <param name="semesterno"></param>
                /// <param name="examno"></param>
                /// <returns></returns>
                public DataSet GetAnswerSheetCourses(AnswerSheet objAnsSheet)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParam = new SqlParameter[6];
                        objParam[0] = new SqlParameter("@P_SESSIONNO", objAnsSheet.SessionNo);
                        objParam[1] = new SqlParameter("@P_SCHEMENO", objAnsSheet.SchemeNo);
                        objParam[2] = new SqlParameter("@P_SEMESTERNO", objAnsSheet.SemesterNo);
                        objParam[3] = new SqlParameter("@P_EXAMTYPE", objAnsSheet.Examtype);
                        if (objAnsSheet.Receiver_Date == null)
                            objParam[4] = new SqlParameter("@P_EXAM_DATE", DBNull.Value);
                        else
                            objParam[4] = new SqlParameter("@P_EXAM_DATE", objAnsSheet.Receiver_Date);
                        objParam[5] = new SqlParameter("@P_SLOT", objAnsSheet.ExamSlot);

                        ds = objSqlHelper.ExecuteDataSetSP("PKG_ACAD_GET_ANSWERSHEET_COURSES", objParam);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ACADEMIC.ANSWERSHEET.GetAnswerSheetCourses()-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetAnswerSheetCoursesIssuer(AnswerSheet objAnsSheet)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParam = new SqlParameter[6];
                        objParam[0] = new SqlParameter("@P_SESSIONNO", objAnsSheet.SessionNo);
                        objParam[1] = new SqlParameter("@P_SCHEMENO", objAnsSheet.SchemeNo);
                        objParam[2] = new SqlParameter("@P_SEMESTERNO", objAnsSheet.SemesterNo);
                        objParam[3] = new SqlParameter("@P_COURSENO", objAnsSheet.CourseNo);
                        objParam[4] = new SqlParameter("@P_EXAMNO", objAnsSheet.ExamNo);
                        objParam[5] = new SqlParameter("@P_EXAMTYPE", objAnsSheet.Examtype);

                        ds = objSqlHelper.ExecuteDataSetSP("PKG_ACAD_GET_ANSWERSHEET_COURSES_ISSUER", objParam);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ACADEMIC.ANSWERSHEET.GetAnswerSheetCoursesIssuer()-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetAnswerSheetIssueCourseByEvaluatorWise(AnswerSheet objAnsSheet)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParam = new SqlParameter[2];
                        objParam[0] = new SqlParameter("@P_SESSIONNO", objAnsSheet.SessionNo);
                        objParam[1] = new SqlParameter("@P_EVALUATORNO", objAnsSheet.FacultyNo);

                        ds = objSqlHelper.ExecuteDataSetSP("PKG_ACAD_GET_ANSWERSHEET_COURSES_BY_EVALUATOR_WISE", objParam);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ACADEMIC.ANSWERSHEET.GetAnswerSheetIssueCourseByEvaluatorWise()-> " + ex.ToString());
                    }
                    return ds;
                }

                public int InsertAnswerSheetMark(AnswerSheet objAnsSheet)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[14];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", objAnsSheet.SessionNo);
                        objParams[1] = new SqlParameter("@P_DEGREENO", objAnsSheet.DegreeNo);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", objAnsSheet.BranchNo);
                        objParams[3] = new SqlParameter("@P_SCHEMENO", objAnsSheet.SchemeNo);
                        objParams[4] = new SqlParameter("@P_EXAMNO", objAnsSheet.ExamNo);
                        objParams[5] = new SqlParameter("@P_EXAMTYPE", objAnsSheet.Examtype);
                        objParams[6] = new SqlParameter("@P_SEMESTERNO", objAnsSheet.SemesterNo);
                        objParams[7] = new SqlParameter("@P_COURSENO", objAnsSheet.CourseName);
                        objParams[8] = new SqlParameter("@P_TOT_ANS_RECD", objAnsSheet.SplitAnsRecd);
                        objParams[9] = new SqlParameter("@P_RECEIVED_STAFF", objAnsSheet.SplitUanoRecd);
                        objParams[10] = new SqlParameter("@P_SUBMIT_STAFF", objAnsSheet.SplitUanoSub);
                        objParams[11] = new SqlParameter("@P_DATE", objAnsSheet.SplitReportTime);
                        objParams[12] = new SqlParameter("@P_REMARK", objAnsSheet.SplitRemark);
                        objParams[13] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[13].Direction = ParameterDirection.Output;

                        object ret = Convert.IsDBNull(objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_ANSWERSHEET_DETAILS", objParams, true));

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ACADEMIC.ANSWERSHEET.InsertAnswerSheetMark-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int updateAnswerSheetMark(AnswerSheet objAnsSheet)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", objAnsSheet.SessionNo);
                        objParams[1] = new SqlParameter("@P_EXAMNO", objAnsSheet.ExamNo);
                        objParams[2] = new SqlParameter("@P_COURSENO", objAnsSheet.CourseName);
                        objParams[3] = new SqlParameter("@P_TOT_ANS_RECD", objAnsSheet.SplitAnsRecd);
                        objParams[4] = new SqlParameter("@P_RECEIVED_STAFF", objAnsSheet.SplitUanoRecd);
                        objParams[5] = new SqlParameter("@P_SUBMIT_STAFF", objAnsSheet.SplitUanoSub);
                        objParams[6] = new SqlParameter("@P_DATE", objAnsSheet.SplitReportTime);
                        objParams[7] = new SqlParameter("@P_REMARK", objAnsSheet.SplitRemark);
                        objParams[8] = new SqlParameter("@P_EXAMTYPE", objAnsSheet.SplitExamtype);
                        objParams[9] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;

                        object ret = Convert.IsDBNull(objSQLHelper.ExecuteNonQuerySP("PKG_UPDATE_ANSWERSHEET_DETAILS", objParams, true));

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ACADEMIC.ANSWERSHEET.InsertAnswerSheetMark-> " + ex.ToString());
                    }

                    return retStatus;
                }

                //---- fro HOD ----
                public int InsertAnswerSheetMarkHOD(AnswerSheet objAnsSheet)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[14];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", objAnsSheet.SessionNo);
                        objParams[1] = new SqlParameter("@P_DEGREENO", objAnsSheet.DegreeNo);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", objAnsSheet.BranchNo);
                        objParams[3] = new SqlParameter("@P_SCHEMENO", objAnsSheet.SchemeNo);
                        objParams[4] = new SqlParameter("@P_EXAMNO", objAnsSheet.ExamNo);
                        objParams[5] = new SqlParameter("@P_SEMESTERNO", objAnsSheet.SemesterNo);
                        objParams[6] = new SqlParameter("@P_EXAMTYPE", objAnsSheet.Examtype);
                        objParams[7] = new SqlParameter("@P_COURSENO", objAnsSheet.CourseName);
                        objParams[8] = new SqlParameter("@P_TOT_ANS_RECD", objAnsSheet.SplitAnsRecd);
                        objParams[9] = new SqlParameter("@P_RECEIVED_STAFF", objAnsSheet.SplitUanoRecd);
                        objParams[10] = new SqlParameter("@P_SUBMIT_STAFF", objAnsSheet.SplitUanoSub);
                        objParams[11] = new SqlParameter("@P_DATE", objAnsSheet.SplitReportTime);
                        objParams[12] = new SqlParameter("@P_EXAMSLOT", objAnsSheet.SplitSlot);
                        objParams[13] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[13].Direction = ParameterDirection.Output;

                        object ret = Convert.IsDBNull(objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_ANSWERSHEET_DETAILS_HOD", objParams, true));

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ACADEMIC.ANSWERSHEET.InsertAnswerSheetMark-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public SqlDataReader GetSinglePaperRate(int srno)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SRNO", srno);
                        dr = objSQLHelper.ExecuteReaderSP("PKG_GET_PAPER_REMUNARATION", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dr;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.GetSinglePaperRate-> " + ex.ToString());
                    }
                    return dr;
                }

                public int AddPaperRate(AnswerSheet objAnsSheet)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;
                        //Add
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_DEGREENO", objAnsSheet.DegreeNo);
                        objParams[1] = new SqlParameter("@P_PAPERRATE", objAnsSheet.PerPapeRate);
                        objParams[2] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_REMUNARATION_DEATILS", objParams, true);

                        if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AnswerSheetController.AddPaperRate-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdatePaperRate(int Srno, int rate)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        //update
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SRNO", Srno);
                        objParams[1] = new SqlParameter("@P_PAPERRATE", rate);

                        object ret = Convert.IsDBNull(objSQLHelper.ExecuteNonQuerySP("PKG_UPDATE_PAPER_RATE", objParams, true));

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.UpdateCT-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int InsertAnswerSheetMarkIssuer(AnswerSheet objAnsSheet)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[16];
                        objParams[0] = new SqlParameter("@P_COURSENO", objAnsSheet.CourseNo);
                        objParams[1] = new SqlParameter("@P_ISSUERID", objAnsSheet.IssuerId);
                        objParams[2] = new SqlParameter("@P_RECDID", objAnsSheet.RecdId);
                        objParams[3] = new SqlParameter("@P_EXAM_STAFF_NO", objAnsSheet.FacultyNo);
                        objParams[4] = new SqlParameter("@P_EXAM_STAFF", objAnsSheet.FacultyName);
                        objParams[5] = new SqlParameter("@P_QUANTITY", objAnsSheet.AnsSheetIssue);
                        objParams[6] = new SqlParameter("@P_REMAINING", objAnsSheet.Balance);
                        objParams[7] = new SqlParameter("@P_BUNDLE_NO", objAnsSheet.Bundle);
                        objParams[8] = new SqlParameter("@P_ISSUER_NAME", objAnsSheet.IssuerName);
                        objParams[9] = new SqlParameter("@P_ISSUER_DATE", objAnsSheet.Issuer_Date);
                        objParams[10] = new SqlParameter("@P_RECEIVER_NAME", objAnsSheet.RecdName);
                        objParams[11] = new SqlParameter("@P_RECEIVER_DATE", ((objAnsSheet.Receiver_Date != DateTime.MinValue) ? objAnsSheet.Receiver_Date : DBNull.Value as object));
                        objParams[12] = new SqlParameter("@P_REMARK", objAnsSheet.Remark);
                        objParams[13] = new SqlParameter("@P_SESSIONNO", objAnsSheet.SessionNo);
                        objParams[14] = new SqlParameter("@P_EXAMTYPE", objAnsSheet.Examtype);
                        objParams[15] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[15].Direction = ParameterDirection.Output;

                        object ret = Convert.IsDBNull(objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_ANSWERSHEET_ISSUER_DETAILS", objParams, true));

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ACADEMIC.ANSWERSHEET.InsertAnswerSheetMarkIssuer-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int InsertEvaluatorBill(AnswerSheet objAnsSheet)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", objAnsSheet.SessionNo);
                        objParams[1] = new SqlParameter("@P_COURSENO", objAnsSheet.CourseNo);
                        objParams[2] = new SqlParameter("@P_EVALUATORNO", objAnsSheet.FacultyNo);
                        objParams[3] = new SqlParameter("@P_PERPAPERRATE", objAnsSheet.PerPapeRate);
                        objParams[4] = new SqlParameter("@P_NOOFPAPER", objAnsSheet.Quantity);
                        objParams[5] = new SqlParameter("@P_TOTAMOUNT", objAnsSheet.Amount);
                        object ret = Convert.IsDBNull(objSqlHelper.ExecuteNonQuerySP("PKG_CALCULATE_EVALUATOR_BILL", objParams, true));
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ACADEMIC.ANSWERSHEET.InsertEvaluatorBill-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllEvaluationRate()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParam = new SqlParameter[0];
                        ds = objSqlHelper.ExecuteDataSetSP("PKG_GET_ALL_EVALUATION_RATE", objParam);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ACADEMIC.ANSWERSHEET.GetAllEvaluationRate()-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet ShowIssuerDetails(int IssuerId)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParam = new SqlParameter[1];
                        objParam[0] = new SqlParameter("@P_ISSUERID", IssuerId);

                        ds = objSqlHelper.ExecuteDataSetSP("PKG_ACAD_GET_ANSWERSHEET_ISSUER_COURSES_DATA", objParam);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ACADEMIC.ANSWERSHEET.ShowIssuerDetails()-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetAnswerSheetEvaluateDetails(AnswerSheet objAnsSheet)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParam = new SqlParameter[4];
                        objParam[0] = new SqlParameter("@P_SESSIONNO", objAnsSheet.SessionNo);
                        objParam[1] = new SqlParameter("@P_EXAMNO", objAnsSheet.ExamNo);
                        objParam[2] = new SqlParameter("@P_EXAM_STAFF_NO", objAnsSheet.FacultyNo);
                        objParam[3] = new SqlParameter("@P_EXAM_STAFF_TYPE", objAnsSheet.FacultyType);

                        ds = objSqlHelper.ExecuteDataSetSP("PKG_ACAD_GET_ANSWERSHEET_EVALUATOR_DETAILS", objParam);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ACADEMIC.ANSWERSHEET.GetAnswerSheetCoursesIssuer()-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetEvaluatorListForApproval(AnswerSheet objAnsSheet)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParam = new SqlParameter[6];
                        objParam[0] = new SqlParameter("@P_SESSIONNO", objAnsSheet.SessionNo);
                        objParam[1] = new SqlParameter("@P_BRANCHNO", objAnsSheet.BranchNo);
                        objParam[2] = new SqlParameter("@P_SCHEMENO", objAnsSheet.SchemeNo);
                        objParam[3] = new SqlParameter("@P_SEMESTERNO", objAnsSheet.SemesterNo);
                        objParam[4] = new SqlParameter("@P_COURSENO", objAnsSheet.CourseNo);
                        objParam[5] = new SqlParameter("@P_EXAMTYPE", objAnsSheet.Examtype);

                        ds = objSqlHelper.ExecuteDataSetSP("PKG_ACAD_GET_EVALUATOR_LIST_FOR_APPROVE", objParam);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ACADEMIC.ANSWERSHEET.GetEvaluatorListForApproval()-> " + ex.ToString());
                    }
                    return ds;
                }

                // Opt
                public DataSet GetAnswerSheetEvaluateCourses(AnswerSheet objAnsSheet)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParam = new SqlParameter[5];
                        objParam[0] = new SqlParameter("@P_SESSIONNO", objAnsSheet.SessionNo);
                        objParam[1] = new SqlParameter("@P_SCHEMENO", objAnsSheet.SchemeNo);
                        objParam[2] = new SqlParameter("@P_SEMNO", objAnsSheet.SemesterNo);
                        objParam[3] = new SqlParameter("@P_EXAM_STAFF_NO", objAnsSheet.FacultyNo);
                        objParam[4] = new SqlParameter("@P_EXAM", objAnsSheet.ExamNo);

                        ds = objSqlHelper.ExecuteDataSetSP("PKG_ACAD_GET_ANSWERSHEET_EVALUATOR_COURSES", objParam);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ACADEMIC.ANSWERSHEET.GetAnswerSheetCoursesIssuer()-> " + ex.ToString());
                    }
                    return ds;
                }

                public int InsertAnswerSheetEvaluator(AnswerSheet objAnsSheet)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[12];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", objAnsSheet.SessionNo);
                        objParams[1] = new SqlParameter("@P_BRANCHNO", objAnsSheet.BranchNo);
                        objParams[2] = new SqlParameter("@P_EXAM_STAFF_NO", objAnsSheet.Exam_Staff_No);
                        objParams[3] = new SqlParameter("@P_EXAM_STAFF_TYPE", objAnsSheet.FacultyType);
                        objParams[4] = new SqlParameter("@P_EXAMNO", objAnsSheet.ExamNo);
                        objParams[5] = new SqlParameter("@P_SCHEMENO", objAnsSheet.SchemeNo);
                        objParams[6] = new SqlParameter("@P_SEMESTERNO", objAnsSheet.SemesterNo);
                        objParams[7] = new SqlParameter("@P_EXAMTYPE", objAnsSheet.Examtype);
                        objParams[8] = new SqlParameter("@P_COURSENO", objAnsSheet.CourseNo);
                        objParams[9] = new SqlParameter("@P_REPORTING_DT", objAnsSheet.Reporting_Date);
                        objParams[10] = new SqlParameter("@P_UANO", objAnsSheet.UANO);
                        objParams[11] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[11].Direction = ParameterDirection.Output;

                        object ret = Convert.IsDBNull(objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_ANSWERSHEET_EVALUATOR_DETAILS", objParams, true));

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ACADEMIC.ANSWERSHEET.InsertAnswerSheetMark-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int UpdateEvaluatorApproveStatus(AnswerSheet objAnsSheet, string staffno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", objAnsSheet.SessionNo);
                        objParams[1] = new SqlParameter("@P_BRANCHNO", objAnsSheet.BranchNo);
                        objParams[2] = new SqlParameter("@P_EXAMNO", objAnsSheet.ExamNo);
                        objParams[3] = new SqlParameter("@P_SCHEMENO", objAnsSheet.SchemeNo);
                        objParams[4] = new SqlParameter("@P_SEMESTERNO", objAnsSheet.SemesterNo);
                        objParams[5] = new SqlParameter("@P_COURSENO", objAnsSheet.CourseNo);
                        objParams[6] = new SqlParameter("@P_STAFFNO", staffno);
                        objParams[7] = new SqlParameter("@P_EXAMTYPE", objAnsSheet.Examtype);

                        objParams[8] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;

                        object ret = Convert.IsDBNull(objSQLHelper.ExecuteNonQuerySP("PKG_UPDATE_EVALAUTOR_APPROVE", objParams, true));

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ACADEMIC.ANSWERSHEET.InsertAnswerSheetMark-> " + ex.ToString());
                    }

                    return retStatus;
                }

                // Changed Exam Evaluator Status
                public int DeleteEvaluator(int uano)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    int ret = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                               new SqlParameter("@P_EVAL_APPID", uano),
                               new SqlParameter("@P_OUTPUT", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        ret = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_STUD_DELETE_EVALUATOR", objParams, true));

                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AnswerSheet.DeleteEvaluator-> " + ex.ToString());
                    }

                    return ret;
                }

                public int AddSaveExam(string staffName, string colAddr, string mobileno, string emailid, string staff, int active, int branchno, int cityno, int ua_no)
                {
                    int retStat = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQL = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParam = null;

                        objParam = new SqlParameter[10];

                        objParam[0] = new SqlParameter("@P_STAFF_NAME", staffName);
                        objParam[1] = new SqlParameter("@P_COLLEGE_ADDRESS", colAddr);
                        objParam[2] = new SqlParameter("@P_MOBILENO", mobileno);
                        objParam[3] = new SqlParameter("@P_EMAILID", emailid);
                        objParam[4] = new SqlParameter("@P_STAFF_TYPE", staff);
                        objParam[5] = new SqlParameter("@P_ACTIVE", active);
                        objParam[6] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParam[7] = new SqlParameter("@P_CITYNO", cityno);
                        objParam[8] = new SqlParameter("@P_UANO", ua_no);
                        objParam[9] = new SqlParameter("@P_EXAM_STAFF_NO", SqlDbType.Int);
                        objParam[9].Direction = ParameterDirection.Output;

                        object ret = Convert.IsDBNull(objSQL.ExecuteNonQuerySP("PKG_ACD_INS_EXAM_STAFF", objParam, true));

                        if (Convert.ToInt32(ret) == -99)
                            retStat = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStat = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStat = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ACADEMIC.ANSWERSHEET.AddSaveExam-> " + ex.ToString());
                    }
                    return retStat;
                }

                //Exam Staff UPDATE
                public int UpdateExamStaff(string staffName, string colAddr, string mobileno, string emailid, string staff, int active, int branchno, int cityno, int staffno)
                {
                    int retStat = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQL = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParam = null;

                        objParam = new SqlParameter[9];

                        objParam[0] = new SqlParameter("@P_STAFF_NAME", staffName);
                        objParam[1] = new SqlParameter("@P_COLLEGE_ADDRESS", colAddr);
                        objParam[2] = new SqlParameter("@P_MOBILENO", mobileno);
                        objParam[3] = new SqlParameter("@P_EMAILID", emailid);
                        objParam[4] = new SqlParameter("@P_STAFF_TYPE", staff);
                        objParam[5] = new SqlParameter("@P_ACTIVE", active);
                        objParam[6] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParam[7] = new SqlParameter("@P_CITYNO", cityno);
                        objParam[8] = new SqlParameter("@P_EXAM_STAFF_NO", staffno);

                        if (objSQL.ExecuteNonQuerySP("PKG_ACD_UPD_EXAM_STAFF", objParam, false) != null)
                            retStat = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStat = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ACADEMIC.ANSWERSHEET.UpdateExamStaff-> " + ex.ToString());
                    }

                    return retStat;
                }
            }
        }
    }
}