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
            /// This CourseTeacherAllotController is used to control Course table.
            /// </summary>
            public partial class CourseTeacherAllotController
            {
                /// <summary>
                /// ConnectionStrings
                /// </summary>
                private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public CourseTeacherAllotController()
                {
                }

                public DataSet GetSlots(int sessionno, int degreeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SESSION", sessionno);
                        objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_TIME_SLOT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.Student_allotmentController.GetSlots-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }//end

                public DataSet Getroomsdepartment(int branchno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_BRANCH", branchno);
                        //objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GETROOMSDEPARTMENTWISE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.Student_allotmentController.GetSlots-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetTimeTable(int slotno, int session, int course, int uano, int thpr)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SLOTNO", slotno);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", session);
                        objParams[2] = new SqlParameter("@P_COURSESRNO", course);
                        //objParams[3] = new SqlParameter("@P_SDSRNO", sub);
                        objParams[3] = new SqlParameter("@P_UANO", uano);
                        objParams[4] = new SqlParameter("@P_THPR", thpr);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_TIME_TABLE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.Student_allotmentController.GetTimeTable-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }//end

                public DataSet DisplayTimeTable(Int32 SESSIONNO, Int32 SEMESTERNO, Int32 SCHEMENO, Int32 SECTIONNO, Int32 DEGREENO, Int32 BRANCHNO, Int32 VERSION)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[7];
                        //objParams[0] = new SqlParameter("@P_COURSESRNO", course);
                        objParams[0] = new SqlParameter("@P_SESSIONNO", SESSIONNO);
                        objParams[1] = new SqlParameter("@P_SEMESTERNO", SEMESTERNO);
                        objParams[2] = new SqlParameter("@P_SCHEMENO", SCHEMENO);
                        objParams[3] = new SqlParameter("@P_SECTIONNO", SECTIONNO);
                        objParams[4] = new SqlParameter("@P_DEGREE", DEGREENO);
                        objParams[5] = new SqlParameter("@P_BRANCH", BRANCHNO);
                        objParams[6] = new SqlParameter("@P_VERSION", VERSION);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_EXAM_TIMETABLE_REPORT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.Student_allotmentController.GetTimeTable-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetStudent(int COURSESRNO, string filter, int SDSRNO, int UANO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_COURSESRNO", COURSESRNO);
                        objParams[1] = new SqlParameter("@P_FILTER", filter);
                        objParams[2] = new SqlParameter("@P_SUBJECTNO", SDSRNO);
                        //@P_UANO
                        objParams[3] = new SqlParameter("@P_UANO", UANO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_ALLOTMENT_ALL_STUDENT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.Student_allotmentController.GetAllDeptExamDetailsOfEmployee-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }//end

                ///<SUMMARY>
                ///Aim; To check the exist of data accoring to the parameter
                /// Function:GetCourseInfo
                /// </SUMMARY>
                public DataSet GetCourseTeacherInfo(int session, int uano, int courseno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                        objParams[1] = new SqlParameter("@P_UA_NO", uano);
                        objParams[2] = new SqlParameter("@P_COURSESRNO", courseno);
                        //@P_SUBJECT
                        //objParams[3] = new SqlParameter("@P_SUBJECT", subject);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_COURSE_TEACHER_INFO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.AttendanceController.GetCourseTeacherInfo-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }//END

                //TO GET TEACHER
                public DataSet GetTeacher(int session, int scheme, int semesterno, int course, int section)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", scheme);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[3] = new SqlParameter("@P_COURSENO", course);
                        objParams[4] = new SqlParameter("@P_SECTIONNO", section);
                        //@P_COURSENO

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_RET_TEACHER_ALLOT_BY_COURSE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetCourseAllotment-> " + ex.ToString());
                    }
                    return ds;
                }

                //[PKG_GET_TEACHER_FULLNAME]
                public DataSet GetTeacherName(string uano)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UANO", uano);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_TEACHER_FULLNAME", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetTeacher-> " + ex.ToString());
                    }
                    return ds;
                }

                //[PKG_GET_TEACHER_FULLNAME]
                public DataSet GetTeacherNo(string name)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_NAME", name);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_TEACHER_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetTeacherNo-> " + ex.ToString());
                    }
                    return ds;
                }

                //Added By Reena
                public long AddUpdateAllotment(AllotmentMaster objAM, ref string Message)
                {
                    long pkid = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[32];
                        //@P_SRNO
                        //objParams[0] = new SqlParameter("@P_SRNO", objAM.SRNO);
                        objParams[0] = new SqlParameter("@P_SESSIONNO", objAM.SESSIONNO);
                        objParams[1] = new SqlParameter("@P_UA_NO", objAM.UA_NO);
                        objParams[2] = new SqlParameter("@P_COURSESRNO", objAM.COURSESRNO);
                        objParams[3] = new SqlParameter("@P_SECTIONNO", objAM.SECTIONNO);
                        //objParams[4] = new SqlParameter("@P_SDSRNO", objAM.SUBJECTNO);
                        objParams[4] = new SqlParameter("@P_SLOT1", objAM.SLOT1);
                        objParams[5] = new SqlParameter("@P_SLOT2", objAM.SLOT2);
                        objParams[6] = new SqlParameter("@P_SLOT3", objAM.SLOT3);
                        objParams[7] = new SqlParameter("@P_SLOT4", objAM.SLOT4);
                        objParams[8] = new SqlParameter("@P_SLOT5", objAM.SLOT5);
                        objParams[9] = new SqlParameter("@P_SLOT6", objAM.SLOT6);
                        objParams[10] = new SqlParameter("@P_SLOT7", objAM.SLOT7);
                        objParams[11] = new SqlParameter("@P_DAYNO1", objAM.DAY1);
                        objParams[12] = new SqlParameter("@P_DAYNO2", objAM.DAY2);
                        objParams[13] = new SqlParameter("@P_DAYNO3", objAM.DAY3);
                        objParams[14] = new SqlParameter("@P_DAYNO4", objAM.DAY4);
                        objParams[15] = new SqlParameter("@P_DAYNO5", objAM.DAY5);
                        objParams[16] = new SqlParameter("@P_DAYNO6", objAM.DAY6);
                        objParams[17] = new SqlParameter("@P_DAYNO7", objAM.DAY7);
                        objParams[18] = new SqlParameter("@P_BATCH1", objAM.BATCH1);
                        objParams[19] = new SqlParameter("@P_BATCH2", objAM.BATCH2);
                        objParams[20] = new SqlParameter("@P_BATCH3", objAM.BATCH3);
                        objParams[21] = new SqlParameter("@P_BATCH4", objAM.BATCH4);
                        objParams[22] = new SqlParameter("@P_BATCH5", objAM.BATCH5);
                        objParams[23] = new SqlParameter("@P_BATCH6", objAM.BATCH6);
                        objParams[24] = new SqlParameter("@P_ROOM1", objAM.ROOM1);
                        objParams[25] = new SqlParameter("@P_ROOM2", objAM.ROOM2);
                        objParams[26] = new SqlParameter("@P_ROOM3", objAM.ROOM3);
                        objParams[27] = new SqlParameter("@P_ROOM4", objAM.ROOM4);
                        objParams[28] = new SqlParameter("@P_ROOM5", objAM.ROOM5);
                        objParams[29] = new SqlParameter("@P_ROOM6", objAM.ROOM6);
                        objParams[30] = new SqlParameter("@P_THEORYPRAC", objAM.THEORYPRAC);
                        //objParams[18] = new SqlParameter("@P_IDNO", objAM.IDNO);
                        //@P_IDNO
                        objParams[31] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[31].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_COURSE_TEACHER_ALLOTMENT", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                Message = "Transaction Failed!";
                            else

                                pkid = Convert.ToInt64(ret.ToString());
                        }
                        else
                            Message = "Transaction Failed!";
                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.Student_allotmentController.AddUpdatePlan-> " + ee.ToString());
                    }
                    return pkid;
                }//end

                public DataSet GetBatchs(int subid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SUBID", subid);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_CLASS_BATCH", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.Student_allotmentController.GetSlots-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }//end

                public int checkslots(int day, int dayno, int roomno, int slotno)
                {
                    int check = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_DAY", day);
                        objParams[1] = new SqlParameter("@P_DAYNO", dayno);
                        objParams[2] = new SqlParameter("@P_ROOMNO", roomno);
                        objParams[3] = new SqlParameter("@P_SLOTNO", slotno);
                        check = Convert.ToInt32(objSQLHelper.ExecuteScalarSP("PR_CHECKSLOT", objParams));
                    }
                    catch (Exception ex)
                    {
                        //return check;
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.Student_allotmentController.GetSlots-> " + ex.ToString());
                    }
                    return check;
                }

                public long TimeTableInsertAttendance(AllotmentMaster objAM, ref string Message)
                {
                    long pkid = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[4];
                        //@P_SRNO
                        objParams[0] = new SqlParameter("@P_SESSIONNO", objAM.SESSIONNO);
                        objParams[1] = new SqlParameter("@P_COURSENO", objAM.COURSESRNO);
                        objParams[2] = new SqlParameter("@P_UA_NO", objAM.UA_NO);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_TIMETABLE_INSERT_ATTENDANCE", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                Message = "Transaction Failed!";
                            else

                                pkid = Convert.ToInt64(ret.ToString());
                        }
                        else
                            Message = "Transaction Failed!";
                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.Student_allotmentController.TimeTableInsertAttendance-> " + ee.ToString());
                    }
                    return pkid;
                }//end

                public DataSet ValidateTeacher(int no, int session, int courseno, int slotno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[4];

                        objParams[0] = new SqlParameter("@P_SLOTNO ", no);
                        objParams[1] = new SqlParameter("@P_SESSIONNO ", session);
                        objParams[2] = new SqlParameter("@P_COURSESRNO ", courseno);
                        objParams[3] = new SqlParameter("@P_SLOT ", slotno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_VALIDATE_TEACHER", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.Student_allotmentController.ValidateTeacher-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }//end

                /// <summary>
                /// Note:newly created
                /// Aim: To validate the duplicte data entry to be saved
                /// Date:30-4-11
                /// Function Name: GetTimeTableByParam()
                /// </summary>

                public DataSet GetTimeTableByParam(int no, int session, int courseno, int slotno, int uano)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[5];

                        objParams[0] = new SqlParameter("@P_SLOTNO ", no);
                        objParams[1] = new SqlParameter("@P_SESSIONNO ", session);
                        objParams[2] = new SqlParameter("@P_COURSESRNO ", courseno);
                        //objParams[3] = new SqlParameter("@P_SDSRNO ", subno);
                        objParams[3] = new SqlParameter("@P_SLOT ", slotno);
                        objParams[4] = new SqlParameter("@P_UANO ", uano);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_VALIDATE_TIMETABLE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.Student_allotmentController.GetTimeTableByParam-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }//end

                public DataSet GetDateFromDays(int courseno, int uano, int session, int section, string ccode)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[4];
                        string spname = "";
                        if (courseno > 0)
                        {
                            objParams[0] = new SqlParameter("@P_COURSENO ", courseno);
                            spname = "PKG_ACD_GET_DATE_FROM_DAYS";
                        }
                        else
                        {
                            objParams[0] = new SqlParameter("@P_CCODE ", ccode);
                            spname = "PKG_ACD_GET_DATE_FROM_DAYS_GLOBALELE";
                        }
                        objParams[1] = new SqlParameter("@P_UA_NO ", uano);
                        objParams[2] = new SqlParameter("@P_SESSIONNO ", session);
                        objParams[3] = new SqlParameter("@P_SECTION ", section);

                        ds = objSQLHelper.ExecuteDataSetSP(spname, objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.Student_allotmentController.GetDateFromDays-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet DisplayTimeTableFacultyCourse(Int32 SESSIONNO, Int32 COURSENO, Int32 SECTIONNO, Int32 UANO, Int32 VERSION, string CCODE)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[5];
                        string spname = "";
                        objParams[0] = new SqlParameter("@P_SESSIONNO", SESSIONNO);
                        if (COURSENO > 0)
                        {
                            objParams[1] = new SqlParameter("@P_COURSENO", COURSENO);
                            spname = "PKG_EXAM_TIMETABLE_REPORT_FACULTY_COURSE";
                        }
                        else
                        {
                            objParams[1] = new SqlParameter("@P_CCODE", CCODE);
                            spname = "PKG_EXAM_TIMETABLE_REPORT_FACULTY_COURSE_GLOBALELE";
                        }
                        objParams[2] = new SqlParameter("@P_SECTIONNO", SECTIONNO);
                        objParams[3] = new SqlParameter("@P_UA_NO", UANO);
                        objParams[4] = new SqlParameter("@V_VERSION", VERSION);
                        ds = objSQLHelper.ExecuteDataSetSP(spname, objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.Student_allotmentController.GetTimeTable-> " + ex.ToString());
                    }
                    finally
                    {
                        //ds.Dispose();
                    }
                    return ds;
                }
            }//end of Class
        }
    }
}