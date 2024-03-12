//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : BUSINESS LOGIC FILE [TEACHINGPLANCONTROLLER]
// CREATION DATE : 04-FEB-2011
// CREATED BY    : ANUP KSHIRSAGAR
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================

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
            public class TeachingPlanController
            {
                private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                #region ACADEMIC_SESSION_MASTER

                /// <summary>
                /// This controller is used to insert academic Session
                /// Page : Academic_Session_Master.aspx
                /// </summary>
                /// <param name="objSession"></param>
                /// <returns></returns>

                public int AddAcademicSession(IITMS.UAIMS.BusinessLayer.BusinessEntities.Session objSession)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add
                        objParams = new SqlParameter[12];
                        objParams[0] = new SqlParameter("@P_SESSION_PNAME", objSession.Session_PName);
                        objParams[1] = new SqlParameter("@P_SESSION_STDATE", objSession.Session_SDate);
                        objParams[2] = new SqlParameter("@P_SESSION_ENDDATE", objSession.Session_EDate);
                        objParams[3] = new SqlParameter("@P_SESSION_NAME", objSession.Session_Name);
                        objParams[4] = new SqlParameter("@P_ODD_EVEN", objSession.Odd_Even);
                        //objParams[5] = new SqlParameter("@P_SESSNAME_HINDI",objSession.Sessname_hindi);
                        objParams[5] = new SqlParameter("@P_COLLEGE_CODE", objSession.College_code);
                        objParams[6] = new SqlParameter("@P_EXAMTYPE", objSession.ExamType);
                        objParams[7] = new SqlParameter("@P_DEGREE_NAME", objSession.Degree_Name);
                        objParams[8] = new SqlParameter("@P_DEGREE_NO", objSession.Degree_No);
                        objParams[9] = new SqlParameter("@P_CurrentSession", objSession.CurrentSession);
                        objParams[10] = new SqlParameter("@P_CurrentSession_Name", objSession.CurrentSession_Name);
                        objParams[11] = new SqlParameter("@P_SESSIONNO", SqlDbType.Int);
                        objParams[11].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_SESSION_SP_INS_ACADEMIC_SESSION", objParams, true) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.AddSession-> " + ex.ToString());
                    }
                    return retStatus;
                }

                /// <summary>
                /// This controller is used to Update Academic Session.
                /// Page : Academic_session_Master.aspx
                /// </summary>
                /// <param name="objSession"></param>
                /// <returns></returns>

                public int UpdateAcademicSession(IITMS.UAIMS.BusinessLayer.BusinessEntities.Session objSession)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //update
                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", objSession.SessionNo);
                        objParams[1] = new SqlParameter("@P_SESSION_PNAME", objSession.Session_PName);
                        objParams[2] = new SqlParameter("@P_SESSION_STDATE", objSession.Session_SDate);
                        objParams[3] = new SqlParameter("@P_SESSION_ENDDATE", objSession.Session_EDate);
                        objParams[4] = new SqlParameter("@P_SESSION_NAME", objSession.Session_Name);
                        objParams[5] = new SqlParameter("@P_ODD_EVEN", objSession.Odd_Even);
                        //objParams[6] = new SqlParameter("@P_SESSNAME_HINDI", objSession.Sessname_hindi);
                        objParams[6] = new SqlParameter("@P_EXAMTYPE", objSession.ExamType);
                        objParams[7] = new SqlParameter("@P_DEGREE_NAME", objSession.Degree_Name);
                        objParams[8] = new SqlParameter("@P_DEGREE_NO", objSession.Degree_No);
                        objParams[9] = new SqlParameter("@P_CurrentSession", objSession.CurrentSession);
                        objParams[10] = new SqlParameter("@P_CurrentSession_Name", objSession.CurrentSession_Name);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_SESSION_SP_UPD_ACADEMIC_SESSION", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.UpdateCT-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetCurrentSession()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_ALL_BRANCH", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.GetCurrentSession-> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// This controller is used to get all academic Sessions.
                /// Page : AcademicCalenderMaster.aspx
                /// </summary>
                /// <param name="sessionno"></param>
                /// <returns></returns>

                public DataSet GetAllSession(int sessionno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_SESSION_SP_ALL_ACADEMIC_SESSION", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TeachingPlanController.GetAllSession-> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// This controller is used to get Single Academic Session.
                /// Page : AcademicCalenderMaster.aspx
                /// </summary>
                /// <param name="sessionno"></param>
                /// <returns></returns>

                public SqlDataReader GetSingleAcademicSession(int sessionno)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("P_SESSIONNO", sessionno);
                        dr = objSQLHelper.ExecuteReaderSP("PKG_SESSION_SP_RET_ACADEMIC_SESSION", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dr;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.GetSingleSession-> " + ex.ToString());
                    }
                    return dr;
                }

                /// <summary>
                /// This controller is used to Delete Academic Session.
                /// Page : AcademicCalenderMaster.aspx
                /// </summary>
                /// <param name="AcademicSessionno"></param>
                /// <returns></returns>

                public int DeleteAcademicSession(int AcademicSessionno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", AcademicSessionno);

                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_SESSION_SP_DELETE_ACADEMIC_SESSION", objParams, true));
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TeachingPlanController.DeleteAcademicSession-> " + ex.ToString());
                    }
                    return retStatus;
                }

                #endregion ACADEMIC_SESSION_MASTER

                #region ACADEMIC_SESSION_HOLIDAY
                //Controllers in Teaching plan For Academic Session MASTER Entry page...
                /// <summary>
                /// This controller is used to Insert Holydays
                /// Page : EventAndHolydayEntry.aspx
                /// </summary>
                /// <param name="objSession"></param>
                /// <returns></returns>

                public int AddHolidayDetails(IITMS.UAIMS.BusinessLayer.BusinessEntities.Session objSession)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add
                        objParams = new SqlParameter[11];
                        if (objSession.Session_SDate == DateTime.MinValue)
                            objParams[0] = new SqlParameter("@P_HOLIDAY_STDATE", DBNull.Value);
                        else
                            objParams[0] = new SqlParameter("@P_HOLIDAY_STDATE", objSession.Session_SDate);

                        if (objSession.Session_EDate == DateTime.MinValue)
                            objParams[1] = new SqlParameter("@P_HOLIDAY_ENDDATE", DBNull.Value);
                        else
                            objParams[1] = new SqlParameter("@P_HOLIDAY_ENDDATE", objSession.Session_EDate);
                        objParams[2] = new SqlParameter("@P_COLLEGE_CODE", objSession.College_code);
                        objParams[3] = new SqlParameter("@P_CurrentSession", objSession.CurrentSession);
                        objParams[4] = new SqlParameter("@P_ACADEMIC_SESSIONNO", objSession.SessionNo);
                        objParams[5] = new SqlParameter("@P_HOLIDAY_NAME", objSession.Event_Name);
                        objParams[6] = new SqlParameter("@P_HOLIDAY_DETAIL", objSession.Event_Detail);
                        objParams[7] = new SqlParameter("@P_IS_HOLIDAY", objSession.Holiday_Event);

                        objParams[8] = new SqlParameter("@P_COLLEGE_ID", objSession.College_ID);
                        objParams[9] = new SqlParameter("@P_ORGANIZATION_ID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                        objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACADEMIC_SESSION_SP_INS_HOLIDAY_DETAIL", objParams, true) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TeachingPlanController.AddHolidayDetails-> " + ex.ToString());
                    }
                    return retStatus;
                }

                /// <summary>
                /// This controller is used to Update Holyday details
                /// Page : EventAndHolydayEntry.aspx
                /// </summary>
                /// <param name="objSession"></param>
                /// <returns></returns>

                public int UpdateHolidayDetails(IITMS.UAIMS.BusinessLayer.BusinessEntities.Session objSession)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //update
                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_HOLIDAYNO", objSession.Holiday_No);
                        objParams[1] = new SqlParameter("@P_HOLIDAY_STDATE", objSession.Session_SDate);

                        if (objSession.Session_EDate == DateTime.MinValue)
                            objParams[2] = new SqlParameter("@P_HOLIDAY_ENDDATE", DBNull.Value);
                        else
                            objParams[2] = new SqlParameter("@P_HOLIDAY_ENDDATE", objSession.Session_EDate);

                        objParams[3] = new SqlParameter("@P_COLLEGE_CODE", objSession.College_code);
                        objParams[4] = new SqlParameter("@P_CurrentSession", objSession.CurrentSession);
                        objParams[5] = new SqlParameter("@P_ACADEMIC_SESSIONNO", objSession.SessionNo);
                        objParams[6] = new SqlParameter("@P_HOLIDAY_NAME", objSession.Event_Name);
                        objParams[7] = new SqlParameter("@P_HOLIDAY_DETAIL", objSession.Event_Detail);
                        objParams[8] = new SqlParameter("@P_IS_HOLIDAY", objSession.Holiday_Event);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACADEMIC_SESSION_SP_UPD_HOLIDAY_DETAIL", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.UpdateCT-> " + ex.ToString());
                    }

                    return retStatus;
                }

                /// <summary>
                /// This controller is used to get all holydays.
                /// Page : EventAndHolydayEntry.aspx
                /// </summary>
                /// <param name="sessionno"></param>
                /// <returns></returns>

                public DataSet GetAllHOLIDAY(int sessionno, int holiday, int colgid, int orgid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_IS_HOLIDAY", holiday);
                        objParams[2] = new SqlParameter("@P_COLLEGE_ID", colgid);
                        objParams[3] = new SqlParameter("@P_ORGANIZATION_ID", orgid);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACADEMIC_SESSION_SP_ALL_HOLIDAY", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TeachingPlanController.GetAllHOLIDAY-> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// This controller is used to get Single academic holyday detail by holyday no.
                /// Page : EventAndHolydayEntry.aspx
                /// </summary>
                /// <param name="Holiday_no"></param>
                /// <returns></returns>

                public SqlDataReader GetSingleAcademicHoliday(int Holiday_no)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("P_HOLIDAYNO", Holiday_no);
                        dr = objSQLHelper.ExecuteReaderSP("PKG_ACADEMIC_SESSION_SP_RET_HOLIDAY", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dr;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.GetSingleSession-> " + ex.ToString());
                    }
                    return dr;
                }

                /// <summary>
                /// This controller is used to Delete holyday entry.
                /// Page : EventAndHolydayENtry.aspx
                /// </summary>
                /// <param name="Holiday_No"></param>
                /// <returns></returns>

                public int DeleteAcademicHoliday(int Holiday_No)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_HOLIDAYNO", Holiday_No);

                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_SESSION_SP_DELETE_HOLIDAY_DETAIL", objParams, true));
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TeachingPlanController.DeleteAcademicHoliday-> " + ex.ToString());
                    }
                    return retStatus;
                }

                #endregion ACADEMIC_SESSION_HOLIDAY

                #region ACADEMIC_DAILY_TIMETABLE_SLOT_MASTER

                //USED FOR ACADEMIC_TIMETABLE_SLOT_MASTER_ENTRY
                /// <summary>
                /// This controller is used to insert Academic time table slot.
                /// Page: TimeTableSlotMaster.aspx
                /// </summary>
                /// <param name="sessionno"></param>
                /// <param name="degreeno"></param>
                /// <param name="slotname"></param>
                /// <param name="timefrom"></param>
                /// <param name="timeto"></param>
                /// <returns></returns>

                //public int AddAcademic_TT_Slot(int sessionno, int degreeno, string slotname, string timefrom, string timeto)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                //        SqlParameter[] objParams = null;

                //        objParams = new SqlParameter[6];
                //        objParams[0] = new SqlParameter("@P_DEGREENO", degreeno);
                //        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                //        objParams[2] = new SqlParameter("@P_SLOTNAME", slotname);
                //        objParams[3] = new SqlParameter("@P_TIMEFROM", timefrom);
                //        objParams[4] = new SqlParameter("@P_TIMETO", timeto);
                //        // objParams[4] = new SqlParameter("@P_COLLEGE_CODE", colcode);
                //        objParams[5] = new SqlParameter("@P_SLOTNO", SqlDbType.Int);
                //        objParams[5].Direction = ParameterDirection.Output;

                //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_ACADEMIC_TT_SLOT_INSERT", objParams, true);
                //        retStatus = Convert.ToInt32(ret);
                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TeachingPlanController.cs.AddAcademicSlot -> " + ex.ToString());
                //    }
                //    return retStatus;
                //}

                /// <summary>
                /// This controller is used to Update Academic TimeTable SLot.
                /// Page : TimeTableSlotMaster.aspx
                ///// </summary>
                ///// <param name="slotno"></param>
                ///// <param name="slotname"></param>
                ///// <param name="degreeno"></param>
                ///// <param name="sessionno"></param>
                ///// <param name="timefrom"></param>
                ///// <param name="timeto"></param>
                ///// <returns></returns>

                //public int UpdateAcademic_TT_Slot(int slotno, string slotname, int degreeno, int sessionno, string timefrom, string timeto)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);

                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                //        SqlParameter[] objParams = null;

                //        objParams = new SqlParameter[7];
                //        objParams[0] = new SqlParameter("@P_SLOTNO", slotno);
                //        objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                //        objParams[2] = new SqlParameter("@P_SESSIONNO", sessionno);
                //        objParams[3] = new SqlParameter("@P_SLOTNAME", slotname);
                //        objParams[4] = new SqlParameter("@P_TIMEFROM", timefrom);
                //        objParams[5] = new SqlParameter("@P_TIMETO", timeto);
                //        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                //        objParams[6].Direction = ParameterDirection.Output;

                //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_ACADEMIC_TT_SLOT_UPDATE", objParams, true);
                //        retStatus = Convert.ToInt32(ret);

                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TeachingPlanController.cs.UpdateAcademicSlot -> " + ex.ToString());
                //    }
                //    return retStatus;
                //}

                ///// <summary>
                ///// This controller is used to get single record of academic time table slots by slotno
                ///// Page: TimeTableSlotMaster.apsx
                ///// </summary>
                ///// <param name="slotno"></param>
                ///// <returns></returns>

                //public DataSet GetSingleAcademic_TT_Slot(int slotno)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                //        SqlParameter[] objParams = new SqlParameter[1];
                //        objParams[0] = new SqlParameter("@P_SLOTNO", slotno);
                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_ACADEMIC_TT_SLOT", objParams);
                //    }
                //    catch (Exception ex)
                //    {
                //        return ds;
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TeachingPlanController.cs.GetSingleAcademicSlot -> " + ex.ToString());
                //    }
                //    return ds;
                //}

                ////Create date : 04-03-2011
                ////Created by : Mr.Anup V.Kshirsagar
                ////<summary>This METHOD is used for Academic TimeTable Slot master page (AcademicDailyTimetableSlotMaster.aspx)</summary>
                ////<aim>This Procedure Deletes the Acadeic Timetable Slot</aim>

                //public int DeleteAcademic_TT_Slot(int SlotNo)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                //        SqlParameter[] objParams = null;

                //        objParams = new SqlParameter[1];
                //        objParams[0] = new SqlParameter("@P_SLOTNO", SlotNo);

                //        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_SP_DELETE_ACADEMIC_TT_SLOT_NO", objParams, true));

                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TeachingPlanController.DeleteAcademic_TT_Slot-> " + ex.ToString());
                //    }
                //    return retStatus;
                //}

                //#endregion

                //#region ACADEMIC_TEACHING_PLAN_MASTER

                ///// <summary>
                ///// This controller is used to get all teaching plan according to Faculty.
                ///// Page: TeachingplanMaster.aspx.
                ///// </summary>
                ///// <param name="sessionno"></param>
                ///// <param name="ua_no"></param>
                ///// <param name="courseno"></param>
                ///// <param name="sectionno"></param>
                ///// <param name="batchno"></param>
                ///// <param name="tutorial"></param>
                ///// <returns></returns>

                /// <summary>
                /// Added by Pritish 31-052019....
                /// </summary>
                public DataSet GetAllTEACHING_PLAN(int sessionno, int semesterno, int ua_no, int courseno, int sectionno, int batchno, int tutorial, int College_id)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[2] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[3] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[4] = new SqlParameter("@P_SECTIONNO", sectionno);
                        objParams[5] = new SqlParameter("@P_BATCHNO", batchno);
                        objParams[6] = new SqlParameter("@P_TUTORIAL", tutorial);
                        objParams[7] = new SqlParameter("@P_COLLEGE_ID", College_id);//ADDED BY DILEEP KARE ON 12.04.2021
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TEACHING_PLAN_GET_ALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamController.GetAllTEACHING_PLAN-> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// This controller is used to Insert Teaching plan record. Added by Pritish on date 31-05-2019
                /// Page : TeachinPlan.aspx
                /// </summary>
                /// <summary>
                /// This controller is used to Insert Teaching plan record. modified by S.Patil- 9 july2019
                /// Page : TeachinPlan.aspx
                /// </summary>

                // Added By Shahbaz Ahamad Date 02-09-2023//
                public int UploadTeachingPlan(Exam objExam, int Istutorial)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[13];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", objExam.SessionNo);
                        objParams[1] = new SqlParameter("@P_UA_NO", objExam.Ua_No);

                        //objParams[2] = new SqlParameter("@P_DATE", objExam.Date);

                        objParams[2] = new SqlParameter("@P_LECTURE_NO", objExam.Lecture_No);
                        objParams[3] = new SqlParameter("@P_COURSENO", objExam.Courseno);
                        objParams[4] = new SqlParameter("@P_SCHEMENO", objExam.SchemeNo);
                        objParams[5] = new SqlParameter("@P_SECTIONNO", objExam.Sectionno);
                        objParams[6] = new SqlParameter("@P_TOPIC_COVERED", objExam.Topic_Covered);
                        objParams[7] = new SqlParameter("@P_UNIT_NO", objExam.UnitNo);
                        objParams[8] = new SqlParameter("@P_BATCHNO", objExam.BatchNo);
                        //objParams[10] = new SqlParameter("@P_SLOT_NO", objExam.SlotTeaching);
                        //objParams[10] = new SqlParameter("@P_SLOT_NO", objExam.Slot);

                        objParams[9] = new SqlParameter("@P_TUTORIAL", Istutorial);
                        objParams[10] = new SqlParameter("@P_TERM", objExam.SemesterNo);
                        objParams[11] = new SqlParameter("@P_COLLEGE_ID", objExam.collegeid);//Added By Dileep Kare on 10.04.2021
                        objParams[12] = new SqlParameter("@P_TP_NO", SqlDbType.Int);
                        objParams[12].Direction = ParameterDirection.Output;

                        //if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TEACHING_PLAN_INSERT", objParams, false) != null)
                        //    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        int ret = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TEACHING_PLAN_UPLOAD", objParams, true));
                        if (ret == -101)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordNotFound);
                        }
                        else if (ret != -99 && ret != -101)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TeachingPlanController.AddTeachingPlan -> " + ex.ToString());
                    }
                    return retStatus;
                }

                // End Date 02-09-2023//

                // Shabaz Ahamad Date 02-09-2023//
                public int AddTeachingPlan(Exam objExam, int Istutorial)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[15];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", objExam.SessionNo);
                        objParams[1] = new SqlParameter("@P_UA_NO", objExam.Ua_No);
                        objParams[2] = new SqlParameter("@P_DATE", objExam.Date);
                        objParams[3] = new SqlParameter("@P_LECTURE_NO", objExam.Lecture_No);
                        objParams[4] = new SqlParameter("@P_COURSENO", objExam.Courseno);
                        objParams[5] = new SqlParameter("@P_SCHEMENO", objExam.SchemeNo);
                        objParams[6] = new SqlParameter("@P_SECTIONNO", objExam.Sectionno);
                        objParams[7] = new SqlParameter("@P_TOPIC_COVERED", objExam.Topic_Covered);
                        objParams[8] = new SqlParameter("@P_UNIT_NO", objExam.UnitNo);
                        objParams[9] = new SqlParameter("@P_BATCHNO", objExam.BatchNo);
                        //objParams[10] = new SqlParameter("@P_SLOT_NO", objExam.SlotTeaching);
                        objParams[10] = new SqlParameter("@P_SLOT_NO", objExam.Slot);
                        objParams[11] = new SqlParameter("@P_TUTORIAL", Istutorial);
                        objParams[12] = new SqlParameter("@P_TERM", objExam.SemesterNo);
                        objParams[13] = new SqlParameter("@P_COLLEGE_ID", objExam.collegeid);//Added By Dileep Kare on 10.04.2021
                        objParams[14] = new SqlParameter("@P_TP_NO", SqlDbType.Int);
                        objParams[14].Direction = ParameterDirection.Output;

                        //if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TEACHING_PLAN_INSERT", objParams, false) != null)
                        //    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        int ret = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TEACHING_PLAN_INSERT", objParams, true));
                        if (ret != -99)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TeachingPlanController.AddTeachingPlan -> " + ex.ToString());
                    }
                    return retStatus;
                }

                // End Date 02-09-2023//

                /// <summary>
                /// modified by S.Patil- 9 july2019
                /// </summary>
                /// <param name="objExam"></param>
                /// <returns></returns>

                public int UpdateTeachingPlan(Exam objExam)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[12];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", objExam.SessionNo);
                        objParams[1] = new SqlParameter("@P_UA_NO", objExam.Ua_No);
                        objParams[2] = new SqlParameter("@P_DATE", objExam.Date);
                        objParams[3] = new SqlParameter("@P_LECTURE_NO", objExam.Lecture_No);
                        objParams[4] = new SqlParameter("@P_COURSENO", objExam.Courseno);
                        objParams[5] = new SqlParameter("@P_SCHEMENO", objExam.SchemeNo);
                        objParams[6] = new SqlParameter("@P_SECTIONNO", objExam.Sectionno);
                        objParams[7] = new SqlParameter("@P_TOPIC_COVERED", objExam.Topic_Covered);
                        objParams[8] = new SqlParameter("@P_UNIT_NO", objExam.UnitNo);
                        objParams[9] = new SqlParameter("@P_BATCHNO", objExam.BatchNo);
                        objParams[10] = new SqlParameter("@P_SLOT_NO", objExam.Slot);
                        objParams[11] = new SqlParameter("@P_TP_NO", objExam.TP_NO);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TEACHING_PLAN_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TeachingPlanController.UpdateTeachingPlan-> " + ex.ToString());
                    }

                    return retStatus;
                }

                //public int AddTeachingPlan(Exam objExam, int Istutorial)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);

                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                //        SqlParameter[] objParams = null;

                //        objParams = new SqlParameter[14];
                //        objParams[0] = new SqlParameter("@P_SESSIONNO", objExam.SessionNo);
                //        objParams[1] = new SqlParameter("@P_UA_NO", objExam.Ua_No);
                //        objParams[2] = new SqlParameter("@P_DATE", objExam.Date);
                //        objParams[3] = new SqlParameter("@P_LECTURE_NO", objExam.Lecture_No);
                //        objParams[4] = new SqlParameter("@P_COURSENO", objExam.Courseno);
                //        objParams[5] = new SqlParameter("@P_SCHEMENO", objExam.SchemeNo);
                //        objParams[6] = new SqlParameter("@P_SECTIONNO", objExam.Sectionno);
                //        objParams[7] = new SqlParameter("@P_TOPIC_COVERED", objExam.Topic_Covered);
                //        objParams[8] = new SqlParameter("@P_UNIT_NO", objExam.UnitNo);
                //        objParams[9] = new SqlParameter("@P_BATCHNO", objExam.BatchNo);
                //        //objParams[10] = new SqlParameter("@P_SLOT_NO", objExam.SlotTeaching);
                //        objParams[10] = new SqlParameter("@P_SLOT_NO", objExam.Slot);//Added by Sunita...08072019...
                //        objParams[11] = new SqlParameter("@P_TUTORIAL", Istutorial);
                //        objParams[12] = new SqlParameter("@P_TERM", objExam.SemesterNo);
                //        objParams[13] = new SqlParameter("@P_TP_NO", SqlDbType.Int);
                //        objParams[13].Direction = ParameterDirection.Output;

                //        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TEACHING_PLAN_INSERT", objParams, false) != null)
                //            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TeachingPlanController.AddTeachingPlan -> " + ex.ToString());
                //    }
                //    return retStatus;
                //}

                /// <summary>
                /// This controller is used to Update Teachinaplan.
                /// Page : TeachingplanMaster.aspx
                /// </summary>
                /// <param name="objExam"></param>
                /// <returns></returns>

                //public int UpdateTeachingPlan(Exam objExam)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                //        SqlParameter[] objParams = null;

                //        objParams = new SqlParameter[12];
                //        objParams[0] = new SqlParameter("@P_SESSIONNO", objExam.SessionNo);
                //        objParams[1] = new SqlParameter("@P_UA_NO", objExam.Ua_No);
                //        objParams[2] = new SqlParameter("@P_DATE", objExam.Date);
                //        objParams[3] = new SqlParameter("@P_LECTURE_NO", objExam.Lecture_No);
                //        objParams[4] = new SqlParameter("@P_COURSENO", objExam.Courseno);
                //        objParams[5] = new SqlParameter("@P_SCHEMENO", objExam.Schemeno);
                //        objParams[6] = new SqlParameter("@P_SECTIONNO", objExam.Sectionno);
                //        objParams[7] = new SqlParameter("@P_TOPIC_COVERED", objExam.Topic_Covered);
                //        objParams[8] = new SqlParameter("@P_UNIT_NO", objExam.UnitNo);
                //        objParams[9] = new SqlParameter("@P_BATCHNO", objExam.BatchNo);
                //        objParams[10] = new SqlParameter("@P_SLOT_NO", objExam.SlotTeaching);
                //        objParams[11] = new SqlParameter("@P_TP_NO", objExam.TP_NO);

                //        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TEACHING_PLAN_UPDATE", objParams, false) != null)
                //            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TeachingPlanController.UpdateTeachingPlan-> " + ex.ToString());
                //    }

                //    return retStatus;
                //}

                ///// <summary>
                ///// Added by Pritish.....31052019
                ///// </summary>
                ///// <param name="objExam"></param>
                ///// <returns></returns>

                //public int UpdateTeachingPlan(Exam objExam)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                //        SqlParameter[] objParams = null;

                //        objParams = new SqlParameter[12];
                //        objParams[0] = new SqlParameter("@P_SESSIONNO", objExam.SessionNo);
                //        objParams[1] = new SqlParameter("@P_UA_NO", objExam.Ua_No);
                //        objParams[2] = new SqlParameter("@P_DATE", objExam.Date);
                //        objParams[3] = new SqlParameter("@P_LECTURE_NO", objExam.Lecture_No);
                //        objParams[4] = new SqlParameter("@P_COURSENO", objExam.Courseno);
                //        objParams[5] = new SqlParameter("@P_SCHEMENO", objExam.SchemeNo);
                //        objParams[6] = new SqlParameter("@P_SECTIONNO", objExam.Sectionno);
                //        objParams[7] = new SqlParameter("@P_TOPIC_COVERED", objExam.Topic_Covered);
                //        objParams[8] = new SqlParameter("@P_UNIT_NO", objExam.UnitNo);
                //        objParams[9] = new SqlParameter("@P_BATCHNO", objExam.BatchNo);
                //        //objParams[10] = new SqlParameter("@P_SLOT_NO", objExam.SlotTeaching);
                //        objParams[10] = new SqlParameter("@P_SLOT_NO", objExam.Slot);//Added by Sunita...08072019...
                //        objParams[11] = new SqlParameter("@P_TP_NO", objExam.TP_NO);

                //        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TEACHING_PLAN_UPDATE", objParams, false) != null)
                //            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TeachingPlanController.UpdateTeachingPlan-> " + ex.ToString());
                //    }

                //    return retStatus;
                //}

                /// <summary>
                /// This controller is used to get single teaching plan by tpno.
                /// Page : TeachingplanMaster.aspx
                /// </summary>
                /// <param name="TP_No"></param>
                /// <returns></returns>

                //public DataSet GetSingleTeachingPlanEntry(int TP_No)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                //        SqlParameter[] objParams = new SqlParameter[1];
                //        objParams[0] = new SqlParameter("@P_TP_NO", TP_No);
                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TEACHING_PLAN_BY_NO", objParams);
                //    }
                //    catch (Exception ex)
                //    {
                //        return ds;
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TeachingPlanController.GetSingleTeachingPlanEntry -> " + ex.ToString());
                //    }
                //    return ds;
                //}

                /// <summary>
                /// Added by Pritish 31052019....
                /// </summary>
                /// <param name="TP_No"></param>
                /// <param name="UA_NO"></param>
                /// <returns></returns>
                public DataSet GetSingleTeachingPlanEntry(int TP_No, int UA_NO, int College_id)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_TP_NO", TP_No);
                        objParams[1] = new SqlParameter("@P_UA__NO", UA_NO);
                        objParams[2] = new SqlParameter("@P_COLLEGE_ID", College_id);//Added By Dileep Kare on 12.04.2021
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TEACHING_PLAN_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TeachingPlanController.GetSingleTeachingPlanEntry -> " + ex.ToString());
                    }
                    return ds;
                }

                ///// <summary>
                ///// This controller is used to Delete Teachingpln.
                ///// Page : TeachingplanMaster.aspx
                ///// </summary>
                ///// <param name="TeachingPlan_NO"></param>
                ///// <returns></returns>

                //public int DeleteTeachingPlan(int TeachingPlan_NO)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                //        SqlParameter[] objParams = null;

                //        objParams = new SqlParameter[1];
                //        objParams[0] = new SqlParameter("@P_TP_NO", TeachingPlan_NO);

                //        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TEACHING_PLAN_DELETE", objParams, true));

                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TeachingPlanController.DeleteTeachingPlan-> " + ex.ToString());
                //    }
                //    return retStatus;
                //}

                /// <summary>
                /// Added by Pritish 31052019....
                /// </summary>
                /// <param name="TeachingPlan_NO"></param>
                /// <param name="uano"></param>
                /// <returns></returns>
                public int DeleteTeachingPlan(int TeachingPlan_NO, int uano)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_TP_NO", TeachingPlan_NO);
                        objParams[1] = new SqlParameter("@P_UA_NO", uano);

                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TEACHING_PLAN_DELETE", objParams, true));
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TeachingPlanController.DeleteTeachingPlan-> " + ex.ToString());
                    }
                    return retStatus;
                }

                ///// <summary>
                ///// THIS CONTROLLER IS USED TO FIND OUT THE SPECIFIC DAY (MONDAY/..) BY PASSING PARAMETER OF FROM DT TO DT AND DAY NO.
                ///// Page : TeachingPlanMaster.aspx
                ///// </summary>
                ///// <param name="stdate"></param>
                ///// <param name="enddate"></param>
                ///// <param name="day"></param>
                ///// <returns></returns>

                /// <summary>
                /// Add by Pritish 31052019....
                /// </summary>

                public DataSet GetTeachingPlanDate(string stdate, string enddate, int day, int College_ID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_FROMDATE", stdate);
                        objParams[1] = new SqlParameter("@P_TODATE", enddate);
                        objParams[2] = new SqlParameter("@P_DAYNO", day);
                        objParams[3] = new SqlParameter("@P_COLLEGE_ID", College_ID);//Added by Dileep Kare on 12.02.2021
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_SHOW_WEEK_DAYZ_FRMDT_TODT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TeachingPlanController.GetSingleTeachingPlanEntry -> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// This controller used to get Slot details.
                /// Page : TeachingplanMaster.aspx
                /// </summary>

                //public DataSet GetSlot(int ua_no, int courseno, int semesterno, int sectionno, int subid, string slot, int batchno)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                //        SqlParameter[] objParams = new SqlParameter[7];
                //        //objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                //        objParams[0] = new SqlParameter("@P_UA_NO", ua_no);
                //        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                //        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                //        objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                //        objParams[4] = new SqlParameter("@P_SUBID", subid);
                //        objParams[5] = new SqlParameter("@P_SLOT", slot);
                //        objParams[6] = new SqlParameter("@P_BATCHNO", batchno);
                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_SLOT", objParams);
                //    }
                //    catch (Exception ex)
                //    {
                //        return ds;
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TeachingPlanController.GetSingleTeachingPlanEntry -> " + ex.ToString());
                //    }
                //    return ds;
                //}

                /// <summary>
                /// Add by Pritish 31052019...
                /// </summary>

                public int AddTeachingplanLock(int session, int ua_no, int section, int courseno, int College_id, int OrgId)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[3] = new SqlParameter("@P_SECTIONNO", section);
                        objParams[4] = new SqlParameter("@P_COLLEGE_ID", College_id);
                        objParams[5] = new SqlParameter("@P_ORGANIZATIONID", OrgId);//Added By Dileep Kare on 15.02.2022
                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_INSERT_TEACHINGPLAN_LOCK", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TeachingPlanController.AddTeachingplanLock -> " + ex.ToString());
                    }
                    return retStatus;
                }

                //public int UpdateTeachingplanLock(int session, int ua_no, int section, int courseno)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);

                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                //        SqlParameter[] objParams = null;

                //        objParams = new SqlParameter[5];
                //        objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                //        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                //        objParams[2] = new SqlParameter("@P_COURSENO", courseno);
                //        objParams[3] = new SqlParameter("@P_SECTIONNO", section);
                //        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                //        objParams[4].Direction = ParameterDirection.Output;

                //        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_UPDATE_TEACHINGPLAN_LOCK", objParams, false) != null)
                //            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TeachingPlanController.AddTeachingPlan -> " + ex.ToString());
                //    }
                //    return retStatus;
                //}

                /// <summary>
                /// Added by Pritish 31052019...
                /// </summary>

                public DataSet GetSlot(int session, int scheme, int ua_no, int courseno, int semesterno, int sectionno, int subid, string slot, int batchno, int degreeno, int Dayno, int College_ID, string sdate, string enddate)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[14];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", scheme);
                        objParams[2] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[3] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[4] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[5] = new SqlParameter("@P_SECTIONNO", sectionno);
                        objParams[6] = new SqlParameter("@P_SUBID", subid);
                        objParams[7] = new SqlParameter("@P_SLOT", slot);
                        objParams[8] = new SqlParameter("@P_BATCHNO", batchno);
                        objParams[9] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[10] = new SqlParameter("@P_DAYNO", Dayno);
                        objParams[11] = new SqlParameter("@P_COLLEGE_ID", College_ID);//Added By Dileep Kare 12.04.2021
                        objParams[12] = new SqlParameter("@P_START_DATE", sdate);//Added By Rishabh 12/11/2022
                        objParams[13] = new SqlParameter("@P_END_DATE", enddate);//Added By Rishabh 12/11/2022
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_SLOT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TeachingPlanController.GetSingleTeachingPlanEntry -> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetDayTimeSlots(int Sessionno, int Semesterno, int ua_no, int Sectionno, int Courseno, int Subid, int schemeno, string startdate, string enddate, int batchno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", Sessionno);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[2] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[3] = new SqlParameter("@P_COURSENO", Courseno);
                        objParams[4] = new SqlParameter("@P_SEMESTERNO", Semesterno);
                        objParams[5] = new SqlParameter("@P_SECTIONNO", Sectionno);
                        objParams[6] = new SqlParameter("@P_SUBID", Subid);
                        objParams[7] = new SqlParameter("@P_START_DATE", startdate);
                        objParams[8] = new SqlParameter("@P_END_DATE", enddate);
                        objParams[9] = new SqlParameter("@P_BATCHNO", batchno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_DAY_TIME_SLOTS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TeachingPlanController.GetDayTimeSlots -> " + ex.ToString());
                    }
                    return ds;
                }

                #endregion ACADEMIC_DAILY_TIMETABLE_SLOT_MASTER

                #region ACADEMIC_TIME_TABLE_MASTER

                /// <summary>
                /// This controller is used to Create Academic time table.
                /// Page : TimeTableEntry.aspx
                /// </summary>
                /// <param name="objExam"></param>
                /// <returns></returns>

                //public int AddAcademic_TT_Master(Exam objExam)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);

                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                //        SqlParameter[] objParams = null;

                //        objParams = new SqlParameter[8];
                //        objParams[0] = new SqlParameter("@P_SESSIONNO", objExam.Sessionno);
                //        objParams[1] = new SqlParameter("@P_COURSENO", objExam.Courseno);
                //        objParams[2] = new SqlParameter("@P_UA_NO", objExam.Ua_No);
                //        objParams[3] = new SqlParameter("@P_SECTIONNO", objExam.Sectionno);
                //        objParams[4] = new SqlParameter("@P_DAY", objExam.Dayno);
                //        objParams[5] = new SqlParameter("@P_SLOTNO", objExam.Slotno);
                //        objParams[6] = new SqlParameter("@P_ROOMNO", objExam.Roomno);
                //        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                //        objParams[7].Direction = ParameterDirection.Output;

                //        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_SP_INS_ACADEMIC_TT", objParams, true));
                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.AddAbsentStudentsRecord-> " + ex.ToString());
                //    }

                //    return retStatus;
                //}

                /// <summary>
                /// This controller is used to Get All Exam dates according to Session.
                /// Page : TimeTableEntry.aspx
                /// </summary>
                /// <param name="sessionno"></param>
                /// <returns></returns>

                //public DataSet GetAllAcademic_TT(int sessionno)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                //        SqlParameter[] objParams = new SqlParameter[1];
                //        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);

                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_EXAM_DATE_GET_ALL", objParams);
                //    }
                //    catch (Exception ex)
                //    {
                //        return ds;
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamController.GetAllExamName-> " + ex.ToString());
                //    }
                //    return ds;
                //}

                #endregion ACADEMIC_TIME_TABLE_MASTER

                # region RoomMaster

                /// <summary>
                /// This controller is used to update room in acd_room table
                /// Page : RoomMaster.aspx
                /// </summary>
                /// <param name="objexam"></param>
                /// <returns></returns>

                //public int UpdateRoom(IITMS.UAIMS.BusinessLayer.BusinessEntities.Exam objexam)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);

                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                //        SqlParameter[] objParams = null;

                //        //update
                //        objParams = new SqlParameter[5];
                //        objParams[0] = new SqlParameter("@P_ROOMNO", objexam.Roomno);
                //        objParams[1] = new SqlParameter("@P_BRANCHNO", objexam.Branchno);
                //        objParams[2] = new SqlParameter("@P_ROOMNAME", objexam.Roomname);
                //        objParams[3] = new SqlParameter("@P_ROOMCAPACITY", objexam.RoomCapacity);
                //        objParams[4] = new SqlParameter("@P_FLOORNO", objexam.FloorNo);

                //        if (objSQLHelper.ExecuteNonQuerySP("PKG_SESSION_SP_UPD_ROOM", objParams, false) != null)
                //            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.UpdateCT-> " + ex.ToString());
                //    }

                //    return retStatus;
                //}

                /// <summary>
                /// This controller is used to Add Room in Acd_room table
                /// Page : RoomMaster.aspx
                /// </summary>
                /// <param name="objexam"></param>
                /// <returns></returns>

                //public int AddRoom(IITMS.UAIMS.BusinessLayer.BusinessEntities.Exam objexam)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);

                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                //        SqlParameter[] objParams = null;
                //        //Add
                //        objParams = new SqlParameter[5];
                //        objParams[0] = new SqlParameter("@P_DEPTNO", objexam.Branchno);
                //        objParams[1] = new SqlParameter("@P_ROOMNAME", objexam.Roomname);
                //        objParams[2] = new SqlParameter("@P_ROOMCAPACITY", objexam.RoomCapacity);
                //        objParams[3] = new SqlParameter("@P_FLOORNO", objexam.FloorNo);
                //        objParams[4] = new SqlParameter("@P_ROOMNO", SqlDbType.Int);
                //        objParams[4].Direction = ParameterDirection.Output;

                //        if (objSQLHelper.ExecuteNonQuerySP("PKG_SESSION_SP_INS_ROOM", objParams, true) != null)
                //            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.AddSession-> " + ex.ToString());
                //    }
                //    return retStatus;
                //}

                /// <summary>
                /// This controller is used to Get All Room detail.
                /// Page : RoomMaster.aspx
                /// </summary>
                /// <returns></returns>

                //public DataSet GetAllRoom()
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                //        SqlParameter[] objParams = new SqlParameter[0];
                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_SP_ALL_ROOM", objParams);
                //    }
                //    catch (Exception ex)
                //    {
                //        return ds;
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.GetAllSession-> " + ex.ToString());
                //    }
                //    return ds;
                //}

                /// <summary>
                /// This controller is used to get single record of room by roomno.
                /// Page : RoomMaster.apsx
                /// </summary>
                /// <param name="Roomno"></param>
                /// <returns></returns>

                //public SqlDataReader GetSingleRoom(int Roomno)
                //{
                //    SqlDataReader dr = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                //        SqlParameter[] objParams = new SqlParameter[1];
                //        objParams[0] = new SqlParameter("P_ROOMNO", Roomno);
                //        dr = objSQLHelper.ExecuteReaderSP("PKG_SESSION_SP_RET_ROOM", objParams);
                //    }
                //    catch (Exception ex)
                //    {
                //        return dr;
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.GetSingleSession-> " + ex.ToString());
                //    }
                //    return dr;
                //}

                /// <summary>
                /// This controller is used to Delete room detail from table.
                /// Page : RoomMaster.apsx
                /// </summary>
                /// <param name="Roomno"></param>
                /// <returns></returns>

                //public int DeleteRoom(int Roomno)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                //        SqlParameter[] objParams = null;

                //        objParams = new SqlParameter[2];
                //        objParams[0] = new SqlParameter("@P_ROOMNO", Roomno);
                //        objParams[1] = new SqlParameter("@P_ROOM_OUT_NO", ParameterDirection.Output);

                //        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_SESSION_SP_DELETE_ROOM", objParams, true));
                //        if (retStatus != null && retStatus != -99)
                //            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                //        else
                //            retStatus = Convert.ToInt32(CustomStatus.Error);

                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TeachingPlanController.DeleteAcademicSession-> " + ex.ToString());
                //    }
                //    return retStatus;
                //}

                #endregion

                /// <summary>
                /// Added by Dileep Kare on 07.03.2022
                /// </summary>
                /// <returns></returns>
                public DataSet GetBlankExcelforTeachingPlan(int Degreeno, int College_id)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_DEGREENO", Degreeno);
                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", College_id);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_DOWNLOAD_BLANK_EXCEL_TEACHING_PLAN", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TeachingPlanController.GetBlankExcelforTeachingPlan -> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// Added by JAY TAKALKHEDE on 04.09.2022
                /// </summary>
                /// <returns></returns>
                public DataSet GetBlankExcelforTeachingPlan1(int COURSENO, int College_id)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_COURSENO", COURSENO);
                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", College_id);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_DOWNLOAD_BLANK_EXCEL_TEACHING_PLAN_modified", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TeachingPlanController.GetBlankExcelforTeachingPlan -> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// Added by Rishabh on 06/01/2023
                /// </summary>
                /// <returns></returns>
                public DataSet FillSectionBatchTeachingPlan(int sessionno, int uano, int courseno, int istutorial)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_UANO", uano);
                        objParams[2] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[3] = new SqlParameter("@P_ISTUTORIAL", istutorial);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_FILL_SECTION_BATCH_TEACHINGPLAN", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TeachingPlanController.GetBlankExcelforTeachingPlan -> " + ex.ToString());
                    }
                    return ds;
                }

                #region Global Elective

                /// <summary>
                /// Added for Global Elective by Swapnil Prachand dated on 18-12-2022
                /// </summary>
                /// <param name="ua_no"></param>
                /// <param name="Courseno"></param>
                /// <param name="Subid"></param>
                /// <param name="schemeno"></param>
                /// <param name="startdate"></param>
                /// <param name="enddate"></param>
                /// <returns></returns>
                /// Done
                public DataSet GetDayTimeSlotsGlobalElective(int ua_no, int Courseno, int Subid, int sessionno, string startdate, string enddate)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_SESSIONID", sessionno);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_COURSENO", Courseno);
                        objParams[3] = new SqlParameter("@P_SUBID", Subid);
                        objParams[4] = new SqlParameter("@P_START_DATE", startdate);
                        objParams[5] = new SqlParameter("@P_END_DATE", enddate);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_DAY_TIME_SLOTS_GLOBAL_ELECTIVE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TeachingPlanController.GetDayTimeSlotsGlobalElective -> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// Added by Swapnil For Global Elective Dated on 18-12-2022
                /// </summary>
                /// <param name="stdate"></param>
                /// <param name="enddate"></param>
                /// <param name="day"></param>
                /// <param name="schemeno"></param>
                /// <returns></returns>
                /// Done
                public DataSet GetTeachingPlanDateGlobalElective(string stdate, string enddate, int day, int sessionno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_FROMDATE", stdate);
                        objParams[1] = new SqlParameter("@P_TODATE", enddate);
                        objParams[2] = new SqlParameter("@P_DAYNO", day);
                        objParams[3] = new SqlParameter("@P_SESSIONNO", sessionno);//Added by Dileep Kare on 12.02.2021
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_SHOW_WEEK_DAYZ_FRMDT_TODT_GLOBAL_ELECTVE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TeachingPlanController.GetSingleTeachingPlanEntry -> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// Added by Swapnil For Global Elective Dated on 18-12-2022
                /// </summary>
                /// <param name="stdate"></param>
                /// <param name="enddate"></param>
                /// <param name="day"></param>
                /// <param name="schemeno"></param>
                /// <returns></returns>
                /// Done
                public DataSet GetSlotGlobalElective(int sessionno, int ua_no, int courseno, int Dayno, string sdate, string enddate)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[6];

                        objParams[0] = new SqlParameter("@P_SESSIONID", sessionno);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[3] = new SqlParameter("@P_DAYNO", Dayno);
                        objParams[4] = new SqlParameter("@P_START_DATE", sdate);//Added By Rishabh 12/11/2022
                        objParams[5] = new SqlParameter("@P_END_DATE", enddate);//Added By Rishabh 12/11/2022
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_SLOT_GLOBAL_ELECTIVE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TeachingPlanController.GetSingleTeachingPlanEntry -> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// Added by Swapnil For Global Elective Dated on 19-12-2022
                /// </summary>
                /// <param name="stdate"></param>
                /// <param name="enddate"></param>
                /// <param name="day"></param>
                /// <param name="schemeno"></param>
                /// <returns></returns>
                /// Done
                public int AddTeachingPlanGlobalElective(Exam objExam, int OrgID)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[10];

                        objParams[0] = new SqlParameter("@P_UA_NO", objExam.Ua_No);
                        objParams[1] = new SqlParameter("@P_DATE", objExam.Date);
                        objParams[2] = new SqlParameter("@P_LECTURE_NO", objExam.Lecture_No);
                        objParams[3] = new SqlParameter("@P_COURSENO", objExam.Courseno);
                        objParams[4] = new SqlParameter("@P_SCHEMENO", objExam.SchemeNo);
                        objParams[5] = new SqlParameter("@P_TOPIC_COVERED", objExam.Topic_Covered);
                        objParams[6] = new SqlParameter("@P_UNIT_NO", objExam.UnitNo);
                        objParams[7] = new SqlParameter("@P_SLOT_NO", objExam.Slot);
                        objParams[8] = new SqlParameter("@P_ORGANIZATIONID", OrgID);//Added by Dileep Kare on 15.02.2022
                        objParams[9] = new SqlParameter("@P_TP_NO", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;

                        //if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TEACHING_PLAN_INSERT", objParams, false) != null)
                        //    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        int ret = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TEACHING_PLAN_INSERT_GLOBAL_ELECTIVE", objParams, true));
                        if (ret != -99)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TeachingPlanController.AddTeachingPlanGlobalElective -> " + ex.ToString());
                    }
                    return retStatus;
                }

                /// <summary>
                /// Added by Swapnil for Global Elective Teaching Plan Display
                /// </summary>
                /// <param name="ua_no"></param>
                /// <param name="courseno"></param>
                /// <param name="OrgId"></param>
                /// <returns></returns>
                /// Done
                public DataSet GetAllTEACHING_PLANGlobalElective(int ua_no, int courseno, int OrgId)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("@P_ORGANIZATIONID", OrgId);//ADDED BY DILEEP KARE ON 15.02.2022
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TEACHING_PLAN_GET_ALL_GLOBAL_ELECTIVE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamController.GetAllTEACHING_PLANGlobalElective-> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// Added by Swapnil for Global Elective Teaching Plan edit
                /// </summary>
                /// <param name="TP_No"></param>
                /// <param name="UA_NO"></param>
                /// <param name="College_id"></param>
                /// <returns></returns>
                /// Done
                public DataSet GetSingleTeachingPlanEntryGlobalElective(int TP_No, int UA_NO, int schemeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_TP_NO", TP_No);
                        objParams[1] = new SqlParameter("@P_UA__NO", UA_NO);
                        objParams[2] = new SqlParameter("@P_SCHEMENO", schemeno);//Added By Dileep Kare on 12.04.2021
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TEACHING_PLAN_BY_NO_GLOBAL_ELECTIVE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TeachingPlanController.GetSingleTeachingPlanEntry -> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// Added by Swapnil for Global Elective Teaching Plan Update
                /// </summary>
                /// <param name="objExam"></param>
                /// <returns></returns>
                /// Done
                public int UpdateTeachingPlanGlobalElective(Exam objExam)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[9];

                        objParams[0] = new SqlParameter("@P_UA_NO", objExam.Ua_No);
                        objParams[1] = new SqlParameter("@P_DATE", objExam.Date);
                        objParams[2] = new SqlParameter("@P_LECTURE_NO", objExam.Lecture_No);
                        objParams[3] = new SqlParameter("@P_COURSENO", objExam.Courseno);
                        objParams[4] = new SqlParameter("@P_SCHEMENO", objExam.SchemeNo);
                        objParams[5] = new SqlParameter("@P_TOPIC_COVERED", objExam.Topic_Covered);
                        objParams[6] = new SqlParameter("@P_UNIT_NO", objExam.UnitNo);
                        objParams[7] = new SqlParameter("@P_SLOT_NO", objExam.Slot);
                        objParams[8] = new SqlParameter("@P_TP_NO", objExam.TP_NO);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TEACHING_PLAN_UPDATE_GLOBAL_ELECTIVE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TeachingPlanController.UpdateTeachingPlanGlobalElective-> " + ex.ToString());
                    }

                    return retStatus;
                }

                /// <summary>
                /// Added by Swapnil For Global Elective Dated on 19-12-2022
                /// </summary>
                /// <param name="stdate"></param>
                /// <param name="enddate"></param>
                /// <param name="day"></param>
                /// <param name="schemeno"></param>
                /// <returns></returns>
                /// Done
                public int AddTeachingPlanGlobalElectiveModified(Exam objExam, int OrgID)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[10];

                        objParams[0] = new SqlParameter("@P_UA_NO", objExam.Ua_No);
                        objParams[1] = new SqlParameter("@P_DATE", objExam.Date);
                        objParams[2] = new SqlParameter("@P_LECTURE_NO", objExam.Lecture_No);
                        objParams[3] = new SqlParameter("@P_COURSENO", objExam.Courseno);
                        objParams[4] = new SqlParameter("@P_SESSIONNO", objExam.SessionNo);
                        objParams[5] = new SqlParameter("@P_TOPIC_COVERED", objExam.Topic_Covered);
                        objParams[6] = new SqlParameter("@P_UNIT_NO", objExam.UnitNo);
                        objParams[7] = new SqlParameter("@P_SLOT_NO", objExam.Slot);
                        objParams[8] = new SqlParameter("@P_ORGANIZATIONID", OrgID);//Added by Dileep Kare on 15.02.2022
                        objParams[9] = new SqlParameter("@P_TP_NO", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;

                        int ret = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TEACHING_PLAN_INSERT_GLOBAL_ELECTIVE_MODIFIED", objParams, true));
                        if (ret != -99)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TeachingPlanController.AddTeachingPlanGlobalElective -> " + ex.ToString());
                    }
                    return retStatus;
                }

                /// <summary>
                /// Added by Swapnil for Global Elective Teaching Plan edit
                /// </summary>
                /// <param name="TP_No"></param>
                /// <param name="UA_NO"></param>
                /// <param name="College_id"></param>
                /// <returns></returns>
                /// Done
                public DataSet GetSingleTeachingPlanEntryGlobalElectiveModified(int TP_No, int UA_NO, int sessionno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_TP_NO", TP_No);
                        objParams[1] = new SqlParameter("@P_UA__NO", UA_NO);
                        objParams[2] = new SqlParameter("@P_SESSIONNO", sessionno);//Added By Dileep Kare on 12.04.2021
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TEACHING_PLAN_BY_NO_GLOBAL_ELECTIVE_MODIFIED", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TeachingPlanController.GetSingleTeachingPlanEntryGlobalElectiveModified -> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// Added by Swapnil for Global Elective Teaching Plan Update
                /// </summary>
                /// <param name="objExam"></param>
                /// <returns></returns>
                /// Done
                public int UpdateTeachingPlanGlobalElectiveModified(Exam objExam)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[9];

                        objParams[0] = new SqlParameter("@P_UA_NO", objExam.Ua_No);
                        objParams[1] = new SqlParameter("@P_DATE", objExam.Date);
                        objParams[2] = new SqlParameter("@P_LECTURE_NO", objExam.Lecture_No);
                        objParams[3] = new SqlParameter("@P_COURSENO", objExam.Courseno);
                        objParams[4] = new SqlParameter("@P_SESSIONNO", objExam.SessionNo);
                        objParams[5] = new SqlParameter("@P_TOPIC_COVERED", objExam.Topic_Covered);
                        objParams[6] = new SqlParameter("@P_UNIT_NO", objExam.UnitNo);
                        objParams[7] = new SqlParameter("@P_SLOT_NO", objExam.Slot);
                        objParams[8] = new SqlParameter("@P_TP_NO", objExam.TP_NO);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TEACHING_PLAN_UPDATE_GLOBAL_ELECTIVE_MODIFIED", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TeachingPlanController.UpdateTeachingPlanGlobalElective-> " + ex.ToString());
                    }

                    return retStatus;
                }

                #endregion
            }
        }
    }
}