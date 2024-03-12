//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : ATTENDANCE CONTROLLER
// CREATION DATE : 22-FEB-2019
// CREATED BY    : SATISH T
// MODIFIED BY   : RAJU BITODE
// MODIFIED DATE : 27-APRIL-2019
// MODIFIED DESC : ADDED NEW METHOD.
//======================================================================================

//using IITMS.SQLServer.SQLDAL;
//using System;
//using System.Data;
//using System.Data.SqlClient;

using IITMS.SQLServer.SQLDAL;
using System;
using System.Data;
using System.Data.SqlClient;

namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{
    public class AcdAttendanceController
    {
        private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        #region Attendance Config

        public DataTableReader CheckActivity(int sessionno, int ua_type, int pagelink)
        {
            DataTableReader dtr = null;

            try
            {
                SQLHelper objsqlhelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_UA_TYPE", ua_type);
                objParams[2] = new SqlParameter("@P_PAGE_LINK", pagelink);

                DataSet ds = objsqlhelper.ExecuteDataSetSP("PKG_ACTIVITY_CHECK_ACTIVITY", objParams);
                if (ds.Tables.Count > 0)
                    dtr = ds.Tables[0].CreateDataReader();
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.ActivityController.CheckActivity-> " + ex.ToString());
            }
            return dtr;
        }

        //   -- -- Controller modify on 26032019 -- by dipali

        public int AddAttendanceConfig(AcdAttendanceModel objAttE, string Sessionnos, string CollegeIds, string Degreenos, int _schemeType, string Semesternos, int OrgId)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;
                //Add
                objParams = new SqlParameter[16];
                objParams[0] = new SqlParameter("@P_SESSIONNO", Sessionnos);
                objParams[1] = new SqlParameter("@P_DEGREENO", Degreenos);
                objParams[2] = new SqlParameter("@P_ATT_STARTDATE", objAttE.AttendanceStartDate);
                objParams[3] = new SqlParameter("@P_ATT_ENDDATE", objAttE.AttendanceEndDate);
                objParams[4] = new SqlParameter("@P_ATT_LOCKDAY", objAttE.AttendanceLockDay);
                //objParams[5] = new SqlParameter("@P_ATT_LOCKHRS", objAttE.AttendanceLockHrs);
                objParams[5] = new SqlParameter("@P_COLLEGE_CODE", objAttE.College_code);
                objParams[6] = new SqlParameter("@P_SMS_FACILITY", objAttE.SMSFacility);
                objParams[7] = new SqlParameter("@P_EMAIL_FACILITY", objAttE.EmailFacility);
                objParams[8] = new SqlParameter("@P_ACTIVE", objAttE.ActiveStatus);   // --  add status
                objParams[9] = new SqlParameter("@P_TEACH", objAttE.TeachingPlan);  //-- add teaching plan
                objParams[10] = new SqlParameter("@P_CRegStatus", objAttE.CRegStatus);//--added for C. Reg before/after
                objParams[11] = new SqlParameter("@P_SchemeType", _schemeType);//--added for C. Reg before/after
                objParams[12] = new SqlParameter("@P_SEMESTERNO", Semesternos);
                objParams[13] = new SqlParameter("@P_COLLEGE_ID", CollegeIds);
                objParams[14] = new SqlParameter("@P_ORGANIZATIONID", OrgId);
                objParams[15] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[15].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ATTENDANCE_CONFIGURATION_INSERT", objParams, true);
                retStatus = Convert.ToInt32(ret);
                if (retStatus == 1)
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.SessionController.AddAttendanceConfig-> " + ex.ToString());
            }
            return retStatus;
        }

        /// This Method used to update attendance configuration
        /// page used - AttendanceConfigue.aspx
        /// </summary>
        /// <param name=""></param>
        /// <returns>int</returns>
        public int UpdateAttConfiguration(AcdAttendanceModel objAttE, int srno, string Sessionnos, string CollegeIds, string Degreenos, int SchemeType, string Semesternos, int Orgid)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;

                //update
                objParams = new SqlParameter[17];
                objParams[0] = new SqlParameter("@P_SESSIONNO", Sessionnos);
                objParams[1] = new SqlParameter("@P_DEGREENO", Degreenos);
                objParams[2] = new SqlParameter("@P_ATT_STARTDATE", objAttE.AttendanceStartDate);
                objParams[3] = new SqlParameter("@P_ATT_ENDDATE", objAttE.AttendanceEndDate);
                objParams[4] = new SqlParameter("@P_ATT_LOCKDAY", objAttE.AttendanceLockDay);
                //objParams[5] = new SqlParameter("@P_ATT_LOCKHRS", objAttE.AttendanceLockHrs);
                objParams[5] = new SqlParameter("@P_COLLEGE_CODE", objAttE.College_code);
                objParams[6] = new SqlParameter("@P_SMS_FACILITY", objAttE.SMSFacility);
                objParams[7] = new SqlParameter("@P_EMAIL_FACILITY", objAttE.EmailFacility);
                objParams[8] = new SqlParameter("@P_SRNO", srno);
                objParams[9] = new SqlParameter("@P_ACTIVE", objAttE.ActiveStatus);
                objParams[10] = new SqlParameter("@P_TEACH", objAttE.TeachingPlan);
                objParams[11] = new SqlParameter("@P_CRegStatus", objAttE.CRegStatus);
                objParams[12] = new SqlParameter("@P_SchemeType", SchemeType);
                objParams[13] = new SqlParameter("@P_COLLEGE_ID", CollegeIds);
                objParams[14] = new SqlParameter("@P_Semesterno", Semesternos);
                objParams[15] = new SqlParameter("@P_ORGANIZATIONID", Orgid);
                objParams[16] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[16].Direction = ParameterDirection.Output;

                if (objSQLHelper.ExecuteNonQuerySP("PKG_ATTENDANCE_CONFIGURATION_UPDATE", objParams, true) != null)
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.SessionController.UpdateAttConfiguration-> " + ex.ToString());
            }

            return retStatus;
        }

        /// <summary>
        /// Added By - Satish
        /// Added On - 15/02/2019
        /// purpose  - To get Attendance config data
        /// Page used - AttendanceConfig.aspx.cs
        /// </summary>
        /// <returns></returns>

        public DataSet GetAllAttendanceConfig(int OrgId, int uatype, string colgid)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_ORGANIZATIONID", OrgId);
                objParams[1] = new SqlParameter("@P_UATYPE", uatype);
                objParams[2] = new SqlParameter("@P_COLLEGE_ID", colgid);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ALL_ATTENDANCE_CONFIG", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.SessionController.GetAllAttendanceConfig-> " + ex.ToString());
            }
            return ds;
        }

        public SqlDataReader GetSingleConfiguration(int srno)
        {
            SqlDataReader dr = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_SRNO", srno);
                dr = objSQLHelper.ExecuteReaderSP("PKG_GET_ATTENDANCE_CONFIG_BY_SRNO", objParams);
            }
            catch (Exception ex)
            {
                return dr;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.SessionController.GetSingleConfiguration-> " + ex.ToString());
            }
            return dr;
        }

        #endregion Attendance Config

        #region BulkCourse Teacher allotment

        /// <summary>
        /// This Method used to get Course data for Teacher allotment
        /// page used - CourseAllotment_Bulk.aspx
        /// </summary>
        /// <param name=""></param>
        /// <returns>Dataset</returns>
        public DataSet GetSubjectForCourseAllotment(int sessionno, int schemeno, int semesterno, int courseno, int deptNo, int clgID, int userno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                SqlParameter[] objParams = new SqlParameter[7];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[3] = new SqlParameter("@P_COURSENO", courseno);
                objParams[4] = new SqlParameter("@P_DEPTNO", deptNo);
                objParams[5] = new SqlParameter("@P_COLLEGE_ID", clgID);
                objParams[6] = new SqlParameter("@P_UA_NO", userno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_OFFERED_SUBJECT_FOR_ALLOTMENT", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AttendanceController.GetSubjectForCourseAllotment-> " + ex.ToString());
            }

            return ds;
        }

        /// <summary>
        /// This method used to Add cousre Teacher allotment Bulk
        /// page used - CousreAllotment_Bulk.aspx
        /// </summary>
        /// <param name="objAttend"></param>
        /// <returns></returns>
        /// <summary>

        public int AddCourseTeacherAllotmentBulk(AcdAttendanceModel objAttend, string adteacherNo)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[12];

                objParams[0] = new SqlParameter("@P_SESSIONNO", objAttend.Sessionno);
                objParams[1] = new SqlParameter("@P_SCHEMENO", objAttend.Schemeno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", objAttend.Semesterno);
                objParams[3] = new SqlParameter("@P_COURSENOS", objAttend.CourseNos);
                objParams[4] = new SqlParameter("@P_TEACHERNOS", objAttend.TeacherNos);
                objParams[5] = new SqlParameter("@P_SECTIONNOS", objAttend.SectionNos);
                objParams[6] = new SqlParameter("@P_BATCHNOS", objAttend.BatchNos);
                objParams[7] = new SqlParameter("@P_IS_ADTeacher", objAttend.Is_ADTeacher);
                //objParams[7] = (objAttend.Is_ADTeacher != "") ? new SqlParameter("@P_IS_ADTeacher", objAttend.Is_ADTeacher) : new SqlParameter("@P_IS_ADTeacher", 0);
                objParams[8] = new SqlParameter("@P_COLLEGE_ID", objAttend.CollegeId);
                objParams[9] = new SqlParameter("@P_COLLEGE_CODE", objAttend.CollegeCode);
                objParams[10] = new SqlParameter("@P_ADTEACHER_NO", adteacherNo);
                objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[11].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_COURSE_TEACHER_ALLOTMENT_BULK", objParams, true);
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

        public DataSet GetSubjectForCourseAllotmentAT(int sessionno, int schemeno, int semesterno, int courseno, int deptno, int sectionno, int batchno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                SqlParameter[] objParams = new SqlParameter[7];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[3] = new SqlParameter("@P_COURSENO", courseno);
                objParams[4] = new SqlParameter("@P_DEPTNO", deptno);
                objParams[5] = new SqlParameter("@P_SECTIONNO", sectionno);
                objParams[6] = new SqlParameter("@P_BATCHNO", batchno);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_OFFERED_SUBJECT_FOR_ALLOTMENT_AT", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AttendanceController.GetSubjectForCourseAllotment-> " + ex.ToString());
            }

            return ds;
        }

        public int AddCourseTeacherAllotmentBulkAT(AcdAttendanceModel objAttend)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[10];

                objParams[0] = new SqlParameter("@P_SESSIONNO", objAttend.Sessionno);
                objParams[1] = new SqlParameter("@P_SCHEMENO", objAttend.Schemeno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", objAttend.Semesterno);
                objParams[3] = new SqlParameter("@P_COURSENOS", objAttend.CourseNos);
                objParams[4] = new SqlParameter("@P_TEACHERNOS", objAttend.TeacherNos);
                objParams[5] = new SqlParameter("@P_SECTIONNOS", objAttend.SectionNos);
                objParams[6] = new SqlParameter("@P_BATCHNOS", objAttend.BatchNos);
                objParams[7] = new SqlParameter("@P_COLLEGE_CODE", objAttend.CollegeCode);
                objParams[8] = new SqlParameter("@P_COLLEGE_ID", objAttend.CollegeId);
                objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[9].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_COURSE_TEACHER_ALLOTMENT_BULK_AT", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AddCourseTeacherAllotmentBulkAT-> " + ex.ToString());
            }

            return retStatus;
        }

        /// <summary>
        /// This method used to Get Allotted Course Teacher List
        /// page used - CousreAllotment_Bulk.aspx
        /// </summary>
        /// <param name="objAttend"></param>
        /// <returns></returns>
        /// <summary>
        public DataSet GetCourseAllottedList(int sessionno, int schemeno, int semesterno, int clgID)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                SqlParameter[] objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[3] = new SqlParameter("@P_COLLEGE_ID", clgID);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ALLOTTED_TEACHER_LIST", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AttendanceController.GetCourseAllottedList-> " + ex.ToString());
            }

            return ds;
        }

        public int CancelTeacherAllotment(string Sessionno, string _ctNos, int clgID)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_SESSIONNO", Sessionno);
                objParams[1] = new SqlParameter("@P_CTNOS", _ctNos);
                objParams[2] = new SqlParameter("@P_COLLEGE_ID", clgID);
                objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[3].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_CANCEL_COURSE_TEACHER_ALLOTMENT", objParams, true);
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

        #endregion BulkCourse Teacher allotment

        #region Leave Module

        /// <summary>
        /// Method used for Save Leave details
        /// Used in Page : LeaveAndHoliday.aspx.cs
        /// </summary>
        /// <param name="objSession"></param>
        /// <param name="idno"></param>
        /// <param name="regno"></param>
        /// <returns></returns>
        public int AddLeaveDetails(AcdAttendanceModel objAttModel, string idno, string regno, string slotNos, int odType)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;
                //Add
                objParams = new SqlParameter[12];

                objParams[0] = new SqlParameter("@P_SESSIONNO", objAttModel.Sessionno);
                objParams[1] = new SqlParameter("@P_IDNO", idno);
                objParams[2] = new SqlParameter("@P_REGNO", regno);
                objParams[3] = new SqlParameter("@P_LEAVE_NAME", objAttModel.LEAVENO);
                objParams[4] = new SqlParameter("@P_HOLIDAY_DETAIL", objAttModel.Event_Detail);
                objParams[5] = new SqlParameter("@P_SLOTNOS", slotNos);

                if (objAttModel.LeaveStartDate == DateTime.MinValue)
                    objParams[6] = new SqlParameter("@P_HOLIDAY_STDATE", DBNull.Value);
                else
                    objParams[6] = new SqlParameter("@P_HOLIDAY_STDATE", objAttModel.LeaveStartDate);

                objParams[7] = new SqlParameter("@P_HOLIDAY_ENDDATE", objAttModel.LeaveEndDate);
                objParams[8] = new SqlParameter("@P_UA_NO_TRAN", objAttModel.UA_NO_TRAN);
                objParams[9] = new SqlParameter("@P_ODTYPE", odType);
                objParams[10] = new SqlParameter("@P_COLLEGE_CODE", objAttModel.College_code);
                objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[11].Direction = ParameterDirection.Output;
                if (objSQLHelper.ExecuteNonQuerySP("PKG_ACADEMIC_SESSION_SP_INS_LEAVE_DETAIL", objParams, true) != null)
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
        /// Update Leave details.
        /// Used in page : LeaveAndHolidayEntry.aspx.cs
        /// </summary>
        /// <param name="objAttModel"></param>
        /// <param name="idno"></param>
        /// <param name="regno"></param>
        /// <returns></returns>
        public int UpdateLeaveDetails(AcdAttendanceModel objAttModel, string idno, string regno, string slotNos, int odType)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[12];

                objParams[0] = new SqlParameter("@P_SESSIONNO", objAttModel.Sessionno);
                objParams[1] = new SqlParameter("@P_IDNO", idno);
                objParams[2] = new SqlParameter("@P_REGNO", regno);
                objParams[3] = new SqlParameter("@P_LEAVE_NAME", objAttModel.LEAVENO);
                objParams[4] = new SqlParameter("@P_HOLIDAY_DETAIL", objAttModel.Event_Detail);
                objParams[5] = new SqlParameter("@P_SLOTNOS", slotNos);
                objParams[6] = new SqlParameter("@P_HOLIDAY_STDATE", objAttModel.LeaveStartDate);
                objParams[7] = new SqlParameter("@P_HOLIDAY_ENDDATE", objAttModel.LeaveEndDate);
                objParams[8] = new SqlParameter("@P_UA_NO_TRAN", objAttModel.UA_NO_TRAN);
                objParams[9] = new SqlParameter("@P_HOLIDAYNO", objAttModel.Holiday_No);
                objParams[10] = new SqlParameter("@P_ODTYPE", odType);
                objParams[11] = new SqlParameter("@P_COLLEGE_CODE", objAttModel.College_code);

                if (objSQLHelper.ExecuteNonQuerySP("PKG_ACADEMIC_SESSION_SP_UPD_LEAVE_DETAIL", objParams, false) != null)
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.SessionController.UpdateLeaveDetails-> " + ex.ToString());
            }

            return retStatus;
        }

        //Added By satish to Add Leave for students in Bulk - 07/02/2019
        public int AddLeaveBulk_Student(AcdAttendanceModel objStudent)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[8];

                objParams[0] = new SqlParameter("@P_SESSIONNO", objStudent.Sessionno);
                objParams[1] = new SqlParameter("@P_FROMDATE", objStudent.FromDate);
                objParams[2] = new SqlParameter("@P_TODATE", objStudent.ToDate);
                objParams[3] = new SqlParameter("@P_STUDID", objStudent.StudId);
                objParams[4] = new SqlParameter("@P_LEAVETYPE", objStudent.LEAVENO);
                objParams[5] = new SqlParameter("@P_LEAVEDETAIL", objStudent.LeaveDetail);
                objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objStudent.CollegeCode);
                objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[7].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_SP_LEAVE_APPLY_BULK", objParams, false);
                if (ret != null)
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.UpdateStudent_TeachAllot-> " + ex.ToString());
            }
            return retStatus;
        }

        /// <summary>
        /// used to get Single Leave detail for update.
        /// Used in page : LeaveAndHolidayEntry.aspx.cs
        /// </summary>
        /// <param name="Holiday_no"></param>
        /// <returns></returns>
        public SqlDataReader GetSingleAcademicLeave(int Holiday_no)
        {
            SqlDataReader dr = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_HOLIDAYNO", Holiday_no);
                dr = objSQLHelper.ExecuteReaderSP("PKG_ACADEMIC_SESSION_SP_RET_LEAVE", objParams);
            }
            catch (Exception ex)
            {
                return dr;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.SessionController.GetSingleAcademicLeave-> " + ex.ToString());
            }
            return dr;
        }

        /// <summary>
        /// used to get Single Leave detail for Approval.
        /// Used in page : LeaveAndHolidayEntry.aspx.cs
        /// </summary>
        /// <param name="Holiday_no"></param>
        /// <returns></returns>
        public SqlDataReader GetSingleLeaveForApproval(int Holiday_no)
        {
            SqlDataReader dr = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_HOLIDAYNO", Holiday_no);
                dr = objSQLHelper.ExecuteReaderSP("PKG_SP_RET_LEAVE_DETAILS_FOR_APPROVAL", objParams);
            }
            catch (Exception ex)
            {
                return dr;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.SessionController.GetSingleAcademicLeave-> " + ex.ToString());
            }
            return dr;
        }

        /// <summary>
        /// Show all leaves as per the session.
        /// Used in page : LeaveAndHolidayEntry.aspx.cs
        /// </summary>
        /// <param name="sessionno"></param>
        /// <returns></returns>
        public DataSet GetAllLeave(int sessionno, int uatype, int uano, int collegeID)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_UATYPE", uatype);
                objParams[2] = new SqlParameter("@P_UANO", uano);
                objParams[3] = new SqlParameter("@P_COLLEGE_ID", collegeID);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACADEMIC_SESSION_SP_ALL_LEAVE", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.SessionController.GetAllLeave-> " + ex.ToString());
            }
            return ds;
        }

        //Added By satish to get Students for Leave apply - 07/02/2019
        public DataSet GetStudentsForLeaveApply(int sessionno, int schemeno, int semesterno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                SqlParameter[] objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_STUDENT_FOR_LEAVEAPPLY", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.GetStudentsForLeaveApply-> " + ex.ToString());
            }

            return ds;
        }

        public int LockAttendacneEntry(int sessionno, int un_no)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSIONNO",sessionno),
                            new SqlParameter("@P_UA_NO",un_no)
                        };

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_LOCK_ATTENDENCE", objParams, false);
                if (ret != null)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                else
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.LockAttendacneEntry --> " + ex.ToString());
            }
            return retStatus;
        }

        public DataSet GetAllLeaveForApproval(int session, int uaType, int uano)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_UATYPE", uaType);
                objParams[1] = new SqlParameter("@P_UANO", uano);
                objParams[2] = new SqlParameter("@P_SESSIONNO", session);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ALL_LEAVE_FOR_APPROVAL", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.SessionController.GetAllLeave-> " + ex.ToString());
            }
            return ds;
        }

        /// <summary>
        /// Update Leave Status.
        /// Used in page : LeaveAndHolidayEntry.aspx.cs
        /// </summary>
        /// <param name="leaveno"></param>
        /// <param name="status"></param>
        /// <returns></returns>

        public int UpdateLeaveStatus(int leaveno, int status, string slotnos, int ua_no)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;

                //update
                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_LEAVENO", leaveno);
                objParams[1] = new SqlParameter("@P_STATUS", status);
                objParams[2] = new SqlParameter("@P_SLOTNOS", slotnos);
                objParams[3] = new SqlParameter("@P_UA_NO", ua_no);

                if (objSQLHelper.ExecuteNonQuerySP("PKG_UPDATE_LEAVE_STATUS_BY_FACULTY", objParams, false) != null)
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AcdAttendanceController.UpdateLeaveStatus-> " + ex.ToString());
            }

            return retStatus;
        }

        /// <summary>
        /// Show all leaves as per the session.
        /// Used in page : LeaveAndHolidayEntry.aspx.cs
        /// </summary>
        /// <param name="sessionno"></param>
        /// <returns></returns>
        public DataSet GetAllLeaveByStudent(int sessionno, int idno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_IDNO", idno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_LEAVE_STUDENTWISE", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.SessionController.GetAllLeaveByStudent-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetSelectedDateSlots(int sessionno, int idno, DateTime date)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_IDNO", idno);
                objParams[2] = new SqlParameter("@P_DATE", date);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_SELECTED_DATE_SLOT", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.GetStudentForFaculty-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetODApplyLeave(int sessionno, int uatype, int uano)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_UATYPE", uatype);
                objParams[2] = new SqlParameter("@P_UANO", uano);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACADEMIC_SESSION_SP_ALL_OD_LEAVES", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.SessionController.GetAllLeave-> " + ex.ToString());
            }
            return ds;
        }

        #endregion Leave Module

        #region Attendance Entry

        public DataSet GetAllCourses(int sessionno, int uano, int schemeType, string collegeIDs)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_UA_NO", uano);
                objParams[2] = new SqlParameter("@P_SCHEMETYPE", schemeType);
                objParams[3] = new SqlParameter("@P_College_IDs", collegeIDs);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_All_COURSES", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AcdAttendanceController.GetAllCourses-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetAlternateAllottedCourses(int sessionno, int uano, int schemeType, string collegeIDs)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_UA_NO", uano);
                objParams[2] = new SqlParameter("@P_SCHEMETYPE", schemeType);
                objParams[3] = new SqlParameter("@P_College_IDs", collegeIDs);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ALTERNATE_ALLOTTED_COURSES", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AcdAttendanceController.GetAllLeave-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetRestrictedCourses(int sessionno, int uano, int schemeType, string collegeIDs)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_UA_NO", uano);
                objParams[2] = new SqlParameter("@P_SCHEMETYPE", schemeType);
                objParams[3] = new SqlParameter("@P_College_IDs", collegeIDs);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_All_RESTRICTED_HOLIDAY_COURSES", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AcdAttendanceController.GetAllLeave-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetAllShiftTTCourses(int sessionno, int uano, int schemeType, string collegeIDs)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_UA_NO", uano);
                objParams[2] = new SqlParameter("@P_SCHEMETYPE", schemeType);
                objParams[3] = new SqlParameter("@P_College_IDs", collegeIDs);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_All_ShiftTT_COURSES", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AcdAttendanceController.GetAllLeave-> " + ex.ToString());
            }
            return ds;
        }

        /// <summary>
        /// Show Subjects for the Attendance entry.
        /// Used in page : AttendanceEntry.aspx.cs
        /// </summary>

        public DataSet GetSubjectForAttendance(int sessionno, int dayno, int uano, DateTime date, int TPlanYesNo, int schemeType, string clgIDs)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[7];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_DAYNO", dayno);
                objParams[2] = new SqlParameter("@P_UA_NO", uano);
                objParams[3] = new SqlParameter("@P_ATTDATE", date);
                objParams[4] = new SqlParameter("@P_TPLAN_FLAG", TPlanYesNo);
                objParams[5] = new SqlParameter("@P_SCHEMETYPE", schemeType);
                objParams[6] = new SqlParameter("@P_College_IDs", clgIDs);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_FACULTY_SUBJECT_FOR_ATTENDANCE", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AcdAttendanceController.GetAllLeave-> " + ex.ToString());
            }
            return ds;
        }

        /// <summary>
        /// Show Students List for Attendace By Faculty
        /// Used in Page : AttendanceEntry.aspx.cs
        /// </summary>

        /// <returns></returns>
        public DataSet GetStudentFacultywiseAttendance(int session, int uano, int courseno, DateTime date, int schemetype, int schemeno, int sem, int sectionno, int batchno, int slotno, int altCourseNo, int clgID)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[12];
                objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                objParams[1] = new SqlParameter("@P_UA_NO", uano);
                objParams[2] = new SqlParameter("@P_COURSENO", courseno);
                objParams[3] = new SqlParameter("@P_SCHEMETYPE", schemetype);
                objParams[4] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[5] = new SqlParameter("@P_SEMESTERNO", sem);
                objParams[6] = new SqlParameter("@P_SECTIONNO", sectionno);
                objParams[7] = new SqlParameter("@P_BATCHNO", batchno);
                objParams[8] = new SqlParameter("@P_SLOTNO", slotno);
                objParams[9] = new SqlParameter("@P_ATT_DATE", date);
                objParams[10] = new SqlParameter("@P_AltCourseNo", altCourseNo);
                objParams[11] = new SqlParameter("@P_College_ID", clgID);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_STUDENT_FACULTYWISE_SUBJECT", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.GetCourseAllotment-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet RetrieveStudentAttDetailsExcel(int CollegeID, int SessionNo, int SchemeNo, int SemNo, int CourseNo, int UA_NO, int SUBID, int SectionNo, int BatchNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[] {
                      new SqlParameter("@P_SESSIONNO", SessionNo),
                      new SqlParameter("@P_SCHEMENO", SchemeNo),
                      new SqlParameter("@P_SEMESTERNO", SemNo),
                      new SqlParameter("@P_COURSENO", CourseNo),
                      new SqlParameter("@P_UA_NO", UA_NO),
                      new SqlParameter("@P_SUBID", SUBID),
                      new SqlParameter("@P_SECTIONNO", SectionNo),
                       new SqlParameter("@P_BATCHNO", BatchNo),
                       new SqlParameter("@P_COLLEGE_ID", CollegeID)
                    };

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_REPORT_STU_ATTENDANCE_DAILY_NEW", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AcdAttendanceController.RetrieveStudentAttDetailsExcel-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetSubjectDetails(int sessionno, int schemeno, int semno, int sectionno, int batchno, int courseno, int slotNo, int uano, DateTime date, int flag, int clgID)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[11];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semno);
                objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                objParams[4] = new SqlParameter("@P_BATCHNO", batchno);
                objParams[5] = new SqlParameter("@P_COURSENO", courseno);
                objParams[6] = new SqlParameter("@P_SLOTNO", slotNo);
                objParams[7] = new SqlParameter("@P_UA_NO", uano);
                objParams[8] = new SqlParameter("@P_DATE", date);
                objParams[9] = new SqlParameter("@P_FLAG", flag);
                objParams[10] = new SqlParameter("@P_College_ID", clgID);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_SUBJECT_DATAILS", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AcdAttendanceController.GetSubjectDetails-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetAttReportWithOD(int clgID, int sessionno, int degreeno, int schemeno, int semno, int subid, int courseno, int sectionno, DateTime frmdate, DateTime todate, string condition, int per)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[11];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                objParams[2] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[3] = new SqlParameter("@P_SEMESTERNO", semno);
                //objParams[4] = new SqlParameter("@P_SUBID", subid);
                objParams[4] = new SqlParameter("@P_COURSENO", courseno);
                objParams[5] = new SqlParameter("@P_SECTIONNO", sectionno);
                objParams[6] = new SqlParameter("@P_FROMDATE", frmdate);
                objParams[7] = new SqlParameter("@P_TODATE", todate);
                objParams[8] = new SqlParameter("@P_CONDITIONS", condition);
                objParams[9] = new SqlParameter("@P_PERCENTAGE", per);
                objParams[10] = new SqlParameter("@P_College_ID", clgID);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_ATT_REPOER_WITH_OD", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AcdAttendanceController.GetAttReportWithOD-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetCumulativeAttDetails(int collegeID, int sessionno, int schemeno, int semno, int sectionno, DateTime frmdate, DateTime todate)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[7];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semno);
                objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                objParams[4] = new SqlParameter("@P_FROMDATE", frmdate);
                objParams[5] = new SqlParameter("@P_TODATE", todate);
                objParams[6] = new SqlParameter("@P_COLLEGE_ID", collegeID);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_TOTAL_ATTENDANCE_DETAILS", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AcdAttendanceController.GetAttReportWithOD-> " + ex.ToString());
            }
            return ds;
        }

        // get OD greater than 63 count..
        public DataSet GetODApproveStudentCount(int sessionno, int collegeID)//, int degreeno, int schemeno, int semno, int sectionno
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_COLLEGE_ID", collegeID);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_OD_COUNT_DETAILS", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AcdAttendanceController.GetAttReportWithOD-> " + ex.ToString());
            }
            return ds;
        }

        #endregion Attendance Entry

        #region Timetable

        public DataSet GetTimeTableData(AcdAttendanceModel objE)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_SESSIONNO", objE.Sessionno);
                objParams[1] = new SqlParameter("@P_SCHEMENO", objE.Schemeno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", objE.Semesterno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_TIMETABLE_DATA", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.GetCourseAllotment-> " + ex.ToString());
            }
            return ds;
        }

        public int DeleteTimeTableFaculty(AcdAttendanceModel objeE)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_TTNO", objeE.TTNO);
                objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[1].Direction = ParameterDirection.Output;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_TIMETABLE_DELETE_FACULTY", objParams, true);
                if (obj != null)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                else
                {
                    retStatus = 0;
                }
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.AddLevel-> " + ex.ToString());
            }

            return retStatus;
        }

        public long AddTTLock(AcdAttendanceModel objAttE, int clgID, ref string Message)
        {
            long pkid = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[9];
                objParams[0] = new SqlParameter("@P_SESSIONNO", objAttE.SESSIONNO);
                objParams[1] = new SqlParameter("@P_DEGREENO", objAttE.DEGREENO);
                objParams[2] = new SqlParameter("@P_SCHEMENO", objAttE.SCHEMENO);
                objParams[3] = new SqlParameter("@P_SEMESTERNO", objAttE.SEMESTERNO);
                objParams[4] = new SqlParameter("@P_SECTIONNO", objAttE.SECTIONNO);
                objParams[5] = new SqlParameter("@P_UA_NO", objAttE.UA_NO);
                //objParams[6] = new SqlParameter("@P_BATCHNO", batchno);
                //objParams[7] = new SqlParameter("@P_COURSENO", objAttE.CourseNo);
                objParams[6] = new SqlParameter("@P_LOCKDATE", objAttE.LOCK_DATE);
                objParams[7] = new SqlParameter("@P_COLLEGE_ID", clgID);
                objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[8].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_INSERT_TT_LOCK", objParams, true);
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
        }

        public DataSet GetSemesterDurationwise(int sessionno, int branchno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_BRANCHNO", branchno);
                objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);

                ds = objSQLHelper.ExecuteDataSetSP("ACD_SEMESTER_DURATIONWISE", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TeachingPlanController.GetSingleTeachingPlanEntry -> " + ex.ToString());
            }
            return ds;
        }

        public int AddTimeTable(IITMS.UAIMS.AcdAttendanceModel.AttendanceDataAddModel objeE)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[7];
                objParams[0] = new SqlParameter("@P_DATATYPE", objeE.DTTimeTable);
                objParams[1] = new SqlParameter("@P_UA_NO", objeE.UserId);
                objParams[2] = new SqlParameter("@P_IPADDRESS", objeE.IPADDRESS);
                objParams[3] = new SqlParameter("@P_START_DATE", objeE.StartDate);
                objParams[4] = new SqlParameter("@P_END_DATE", objeE.EndDate);
                objParams[5] = new SqlParameter("@P_COLLEGE_ID", objeE.CollegeId);//Added By Dileep on 24-02-2021
                objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[6].Direction = ParameterDirection.Output;
                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_TIMETABLE_INSERT", objParams, true);
                if (obj != null)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                else
                {
                    retStatus = 0;
                }
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AcdAttendanceController.AddTimeTable-> " + ex.ToString());
            }
            return retStatus;
        }

        public DateTime GetAttendanceEndDate(int sessionno, int schemeno, int semesterno, int colgid, int organizationid)
        {
            DateTime dt;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_SESSIONNNO", sessionno);
                objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[3] = new SqlParameter("@P_COLLEGE_ID", colgid);
                objParams[4] = new SqlParameter("@P_ORGANIZATION_ID", organizationid);
                dt = Convert.ToDateTime(objSQLHelper.ExecuteScalarSP("PKG_ACAD_GET_ATTENDANCE_END_DATE", objParams));
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AcdAttendanceController.GetAttendanceEndDate-> " + ex.ToString());
            }
            return dt;
        }

        //public DateTime GetAttendanceEndDate(int sessionno, int schemeno, int semesterno, int colgid)
        //{
        //    DateTime dt;
        //    try
        //    {
        //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
        //        SqlParameter[] objParams = new SqlParameter[4];
        //        objParams[0] = new SqlParameter("@P_SESSIONNNO", sessionno);
        //        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
        //        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
        //        objParams[3] = new SqlParameter("@P_COLLEGE_ID", colgid);
        //        dt = Convert.ToDateTime(objSQLHelper.ExecuteScalarSP("PKG_ACAD_GET_ATTENDANCE_END_DATE", objParams));
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AcdAttendanceController.GetAttendanceEndDate-> " + ex.ToString());
        //    }
        //    return dt;
        //}

        public DataSet GetRegularFacultyWiseCourses(int sessionno, int schemeno, int semesterno, int sectionno, int slottype, DateTime startdate, DateTime enddate)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[7];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                objParams[4] = new SqlParameter("@P_SLOTTYPE", slottype);
                objParams[5] = new SqlParameter("@P_STARTDATE", startdate);
                objParams[6] = new SqlParameter("@P_ENDDATE", enddate);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_FACULTY_WISE_COURSES", objParams);
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

        public DataSet GetFacultyForTimeTable(int sessionno, int schemeno, int sem, int sectionnno, int colgid)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", sem);
                objParams[3] = new SqlParameter("@P_SECTIONNO", sectionnno);
                objParams[4] = new SqlParameter("@P_COLLEGEID", colgid);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_FACULTY_FOR_TIMETABLE", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AcdAttendanceController.GetFacultyForTimeTable-> " + ex.ToString());
            }
            return ds;
        }

        public DateTime GetAttendanceStartDate(int sessionno, int schemeno, int semesterno, int colgid, int orgid)
        {
            DateTime dt;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_SESSIONNNO", sessionno);
                objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[3] = new SqlParameter("@P_COLLEGE_ID", colgid);
                objParams[4] = new SqlParameter("@P_ORGANIZATION_ID", orgid);
                dt = Convert.ToDateTime(objSQLHelper.ExecuteScalarSP("PKG_ACAD_GET_ATTENDANCE_START_DATE", objParams));
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AcdAttendanceController.GetAttendanceStartDate-> " + ex.ToString());
            }
            return dt;
        }

        //public DateTime GetAttendanceStartDate(int sessionno, int schemeno, int semesterno, int colgid)
        //{
        //    DateTime dt;
        //    try
        //    {
        //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
        //        SqlParameter[] objParams = new SqlParameter[4];
        //        objParams[0] = new SqlParameter("@P_SESSIONNNO", sessionno);
        //        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
        //        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
        //        objParams[3] = new SqlParameter("@P_COLLEGE_ID", colgid);
        //        dt = Convert.ToDateTime(objSQLHelper.ExecuteScalarSP("PKG_ACAD_GET_ATTENDANCE_START_DATE", objParams));
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AcdAttendanceController.GetAttendanceStartDate-> " + ex.ToString());
        //    }
        //    return dt;
        //}

        public int AddRevisedTimeTable(IITMS.UAIMS.AcdAttendanceModel.AttendanceDataAddModel objeE)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[7];
                objParams[0] = new SqlParameter("@P_DATATYPE", objeE.DTTimeTable);
                objParams[1] = new SqlParameter("@P_UA_NO", objeE.UserId);
                objParams[2] = new SqlParameter("@P_IPADDRESS", objeE.IPADDRESS);
                objParams[3] = new SqlParameter("@P_TIMETABLE_DATE", objeE.TimeTableDate);
                objParams[4] = new SqlParameter("@P_START_DATE", objeE.StartDate);
                objParams[5] = new SqlParameter("@P_END_DATE", objeE.EndDate);
                objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[6].Direction = ParameterDirection.Output;
                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_REVISED_TIMETABLE_INSERT", objParams, true);
                if (obj != null)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                else
                {
                    retStatus = 0;
                }
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AcdAttendanceController.AddTimeTable-> " + ex.ToString());
            }
            return retStatus;
        }

        public DataSet GetFacultyWiseCourses(int sessionno, int schemeno, int semesterno, int sectionno, int revisedno, DateTime startdate, DateTime enddate)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[7];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                objParams[4] = new SqlParameter("@P_REVISED_NO", revisedno);
                objParams[5] = new SqlParameter("@P_STARTDATE", startdate);
                objParams[6] = new SqlParameter("@P_ENDDATE", enddate);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_FACULTY_WISE_COURSES", objParams);
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

        public DataSet GetCourseAllotment(int session, int scheme, int semesterno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                objParams[1] = new SqlParameter("@P_SCHEMENO", scheme);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_RET_COURSE_ALLOT", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.GetCourseAllotment-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetFacultyBySelection(int collegeId, int session, int schemeno, int semesterno, int sectionno, int subid, int courseno, int batchno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[8];
                objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                objParams[4] = new SqlParameter("@P_SUBID", subid);
                objParams[5] = new SqlParameter("@P_COURSENO", courseno);
                objParams[6] = new SqlParameter("@P_BATCHNO", batchno);
                objParams[7] = new SqlParameter("@P_COLLEGE_ID", collegeId);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_FACULTYS_BY_SELECTION", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.GetCourseAllotment-> " + ex.ToString());
            }
            return ds;
        }

        //with max revised no + regular
        public DataSet GetAllFacultyWiseCourses(int sessionno, int schemeno, int semesterno, int sectionno, int slottype, DateTime startdate, DateTime enddate, DateTime date)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[8];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                objParams[4] = new SqlParameter("@P_SLOTTYPE", slottype);
                objParams[5] = new SqlParameter("@P_STARTDATE", startdate);
                objParams[6] = new SqlParameter("@P_ENDDATE", enddate);
                objParams[7] = (date == DateTime.MinValue) ? new SqlParameter("@P_TIME_TABLE_DATE", DBNull.Value) : new SqlParameter("@P_TIME_TABLE_DATE", date);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ALL_FACULTY_WISE_COURSES", objParams);
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

        //without revisedno
        public DataSet GetRegularFacultyWiseCourses(int sessionno, int schemeno, int semesterno, int sectionno, int slottype, DateTime startdate, DateTime enddate, int OrgId)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[8];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                objParams[4] = new SqlParameter("@P_SLOTTYPE", slottype);
                objParams[5] = new SqlParameter("@P_STARTDATE", startdate);
                objParams[6] = new SqlParameter("@P_ENDDATE", enddate);
                objParams[7] = new SqlParameter("@P_ORGANIZATIONID", OrgId);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_FACULTY_WISE_COURSES", objParams);
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

        //load time table between mltiple dates(.aspx)

        public DataSet LoadTimeTableDetails(int sessionno, int schemeno, int semesterno, int sectionno, int slottype, DateTime startdate, DateTime enddate, int college_id)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[8];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                objParams[4] = new SqlParameter("@P_SLOTTYPE", slottype);
                objParams[5] = new SqlParameter("@P_STARTDATE", startdate);
                objParams[6] = new SqlParameter("@P_ENDDATE", enddate);
                objParams[7] = new SqlParameter("@P_COLLEGE_ID", college_id);// Added By Dileep on 22/02/2021.
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_FACULTY_TIMETABLE_DETAILS", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.AcdAttendanceController.LoadTimeTableDetails-> " + ex.ToString());
            }
            finally
            {
                ds.Dispose();
            }
            return ds;
        }

        //load time table for single dates(Cancel_TimeTable.aspx)
        public DataSet LoadTimeTableDetails(int sessionno, int schemeno, int semesterno, int sectionno, int slottype, DateTime date)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                objParams[4] = new SqlParameter("@P_SLOTTYPE", slottype);
                objParams[5] = new SqlParameter("@P_DATE", date);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_FACULTY_TIMETABLE_DETAILS", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.AcdAttendanceController.LoadTimeTableDetails-> " + ex.ToString());
            }
            finally
            {
                ds.Dispose();
            }
            return ds;
        }

        #endregion Timetable

        #region RevisedTimetable

        public DataSet GetSlots(int sessionno, int degreeno, int slotType, int College_id)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                objParams[2] = new SqlParameter("@P_SlotType", slotType);
                objParams[3] = new SqlParameter("@P_COLLEGE_ID", College_id);//Added By Dileep Kare  on 16.04.2021

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
        }

        public int InsertRevisedTimeTable(IITMS.UAIMS.AcdAttendanceModel.AttendanceDataAddModel objeE)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[16];
                objParams[0] = new SqlParameter("@P_DATATYPE", objeE.DTTimeTable);
                objParams[1] = new SqlParameter("@P_UA_NO", Convert.ToInt32(System.Web.HttpContext.Current.Session["userno"]));
                objParams[2] = new SqlParameter("@P_IPADDRESS", objeE.IPADDRESS);
                //objParams[3] = new SqlParameter("@P_TIMETABLE_DATE", objeE.TimeTableDate);
                objParams[3] = new SqlParameter("@P_START_DATE", Convert.ToDateTime(System.Web.HttpContext.Current.Session["Revised_startdate"].ToString()));
                objParams[4] = new SqlParameter("@P_END_DATE", Convert.ToDateTime(System.Web.HttpContext.Current.Session["Revised_enddate"].ToString()));
                objParams[5] = new SqlParameter("@P_COLLEGE_ID", objeE.CollegeId);//Added By Dileep on 19022021

                objParams[6] = new SqlParameter("@P_SCHEMENO", objeE.Schemeno);
                objParams[7] = new SqlParameter("@P_DEGREENO", objeE.Degreeno);
                objParams[8] = new SqlParameter("@P_SEMESTERNO", Convert.ToInt32(System.Web.HttpContext.Current.Session["Revised_Semesterno"]));
                objParams[9] = new SqlParameter("@P_SECTIONNO", Convert.ToInt32(System.Web.HttpContext.Current.Session["Revised_Sectionno"]));
                objParams[10] = new SqlParameter("@P_SLOTTYPE", Convert.ToInt32(System.Web.HttpContext.Current.Session["Revised_slottype"]));

                objParams[11] = new SqlParameter("@P_ORGANIZATIONID", objeE.OrgId);
                objParams[12] = new SqlParameter("@P_DATATYPE_REVISED", objeE.dtRevisedFac); //Added By Rishabh on oct/2022
                objParams[13] = new SqlParameter("@P_REMARK", objeE.Remark);  //Added By Rishabh on 27/10/2022
                objParams[14] = new SqlParameter("@P_COLLEGESCHEMEID", Convert.ToInt32(System.Web.HttpContext.Current.Session["ddlSchoolInstitute_revisedTT"]));
                objParams[15] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[15].Direction = ParameterDirection.Output;
                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_MULTI_REVISED_TIMETABLE_INSERT", objParams, true);
                if (obj != null)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                else
                {
                    retStatus = 0;
                }
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AcdAttendanceController.InsertRevisedTimeTable-> " + ex.ToString());
            }
            return retStatus;
        }

        /// <summary>
        /// Added by Dileep Kare on 07.03.2022
        /// </summary>
        /// <param name="sessionno"></param>
        /// <param name="college_id"></param>
        /// <param name="degreeno"></param>
        /// <param name="schemeno"></param>
        /// <param name="semesterno"></param>
        /// <param name="sectionno"></param>
        /// <param name="orgid"></param>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <returns></returns>
        public DataSet Get_Revised_Time_Table_Validation(string sessionno, string college_id, string degreeno, string schemeno, string semesterno, string sectionno, string orgid, string startdate, string enddate)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[9];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_COLLEGE_ID", college_id);
                objParams[2] = new SqlParameter("@P_DEGREENO", degreeno);
                objParams[3] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[4] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[5] = new SqlParameter("@P_SECTIONNO", sectionno);
                objParams[6] = new SqlParameter("@P_ORGANIZATIONID", orgid);
                objParams[7] = new SqlParameter("@P_STARTDATE", startdate);
                objParams[8] = new SqlParameter("@P_ENDDATE", enddate);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_REVISED_TIME_TABLE_VALIDATION", objParams);
            }
            catch
            {
                throw;
            }
            return ds;
        }

        public DataSet GetAllFacultyWiseCourses(int sessionno, int schemeno, int semesterno, int sectionno, int slottype, DateTime startdate, DateTime enddate, DateTime date, int colgid, int OrgId)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[10];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                objParams[4] = new SqlParameter("@P_SLOTTYPE", slottype);
                objParams[5] = new SqlParameter("@P_STARTDATE", startdate);
                objParams[6] = new SqlParameter("@P_ENDDATE", enddate);
                objParams[7] = (date == DateTime.MinValue) ? new SqlParameter("@P_TIME_TABLE_DATE", DBNull.Value) : new SqlParameter("@P_TIME_TABLE_DATE", date);
                objParams[8] = new SqlParameter("@P_COLLEGE_ID", colgid);
                objParams[9] = new SqlParameter("@P_ORGANIZATIONID", OrgId); //Added By Rishabh
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ALL_FACULTY_WISE_COURSES", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.AcdAttendanceController.GetAllFacultyWiseCourses-> " + ex.ToString());
            }
            finally
            {
                ds.Dispose();
            }
            return ds;
        }

        #endregion RevisedTimetable

        #region ACADEMIC_TEACHING_PLAN_MASTER

        public DataSet DisplayTimeTableRoomSlotTeachingPlan(int clgID, int SESSIONNO, int SEMESTERNO, int uano, int sectionno, int courseno, int subid)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[7];
                objParams[0] = new SqlParameter("@P_SESSIONNO", SESSIONNO);
                objParams[1] = new SqlParameter("@P_SEMESTERNO", SEMESTERNO);
                objParams[2] = new SqlParameter("@P_SECTIONNO", sectionno);
                objParams[3] = new SqlParameter("@P_SUBID", subid);
                objParams[4] = new SqlParameter("@P_UA_NO", uano);
                objParams[5] = new SqlParameter("@P_COURSENO", courseno);
                objParams[6] = new SqlParameter("@P_College_ID", clgID);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_TIMETABLE_SLOT_ROOM_FOR_TEACHINGPLAN", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.Student_allotmentController.DisplayTimeTableRoomSlotTeachingPlan-> " + ex.ToString());
            }
            finally
            {
                ds.Dispose();
            }
            return ds;
        }

        public DataSet GetAllTEACHING_PLAN(int clgID, int sessionno, int semester, int sectionno, int batchno, int courseno, int ua_no, int tutorial)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[8];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_SEMESTERNO", semester);
                objParams[2] = new SqlParameter("@P_SECTIONNO", sectionno);
                objParams[3] = new SqlParameter("@P_BATCHNO", batchno);
                objParams[4] = new SqlParameter("@P_COURSENO", courseno);
                objParams[5] = new SqlParameter("@P_UA_NO", ua_no);
                objParams[6] = new SqlParameter("@P_TUTORIAL", tutorial);
                objParams[7] = new SqlParameter("@P_COLLEGE_ID", clgID);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TEACHING_PLAN_GET_ALL", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamController.GetAllExamName-> " + ex.ToString());
            }
            return ds;
        }

        /// <summary>
        /// This controller is used to Insert Teaching plan record.
        /// Page : TeachinPlan.aspx
        /// </summary>
        /// <param name="objExam"></param>
        /// <param name="Istutorial"></param>
        /// <returns></returns>

        public int AddTeachingPlan(AcdAttendanceModel objAttModel, int Istutorial)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[15];
                objParams[0] = new SqlParameter("@P_SESSIONNO", objAttModel.Sessionno);
                objParams[1] = new SqlParameter("@P_UA_NO", objAttModel.UA_No);
                objParams[2] = new SqlParameter("@P_DATE", objAttModel.Date);
                objParams[3] = new SqlParameter("@P_LECTURE_NO", objAttModel.Lecture_No);
                objParams[4] = new SqlParameter("@P_COURSENO", objAttModel.CourseNo);
                objParams[5] = new SqlParameter("@P_SCHEMENO", objAttModel.Schemeno);
                objParams[6] = new SqlParameter("@P_SECTIONNO", objAttModel.Sectionno);
                objParams[7] = new SqlParameter("@P_TOPIC_COVERED", objAttModel.Topic_Covered);
                objParams[8] = new SqlParameter("@P_UNIT_NO", objAttModel.UnitNo);
                objParams[9] = new SqlParameter("@P_BATCHNO", objAttModel.BatchNo);
                objParams[10] = new SqlParameter("@P_SLOT_NO", objAttModel.SlotTeaching);
                objParams[11] = new SqlParameter("@P_TUTORIAL", Istutorial);
                objParams[12] = new SqlParameter("@P_TERM", objAttModel.Semesterno);
                objParams[13] = new SqlParameter("@P_COLLEGE_ID", objAttModel.CollegeId);
                objParams[14] = new SqlParameter("@P_TP_NO", SqlDbType.Int);
                objParams[14].Direction = ParameterDirection.Output;

                if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TEACHING_PLAN_INSERT", objParams, false) != null)
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TeachingPlanController.AddTeachingPlan -> " + ex.ToString());
            }
            return retStatus;
        }

        /// <summary>
        /// This controller is used to Update Teachinaplan.
        /// Page : TeachingplanMaster.aspx
        /// </summary>
        /// <param name="objExam"></param>
        /// <returns></returns>

        public int UpdateTeachingPlan(AcdAttendanceModel objAttModel)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[12];
                objParams[0] = new SqlParameter("@P_SESSIONNO", objAttModel.Sessionno);
                objParams[1] = new SqlParameter("@P_UA_NO", objAttModel.UA_No);
                objParams[2] = new SqlParameter("@P_DATE", objAttModel.Date);
                objParams[3] = new SqlParameter("@P_LECTURE_NO", objAttModel.Lecture_No);
                objParams[4] = new SqlParameter("@P_COURSENO", objAttModel.CourseNo);
                objParams[5] = new SqlParameter("@P_SCHEMENO", objAttModel.Schemeno);
                objParams[6] = new SqlParameter("@P_SECTIONNO", objAttModel.Sectionno);
                objParams[7] = new SqlParameter("@P_TOPIC_COVERED", objAttModel.Topic_Covered);
                objParams[8] = new SqlParameter("@P_UNIT_NO", objAttModel.UnitNo);
                objParams[9] = new SqlParameter("@P_BATCHNO", objAttModel.BatchNo);
                objParams[10] = new SqlParameter("@P_SLOT_NO", objAttModel.SlotTeaching);
                objParams[11] = new SqlParameter("@P_TP_NO", objAttModel.TP_NO);

                if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TEACHING_PLAN_UPDATE", objParams, false) != null)
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TeachingPlanController.UpdateTeachingPlan-> " + ex.ToString());
            }

            return retStatus;
        }

        /// <summary>
        /// This controller is used to get single teaching plan by tpno.
        /// Page : TeachingplanMaster.aspx
        /// </summary>
        /// <param name="TP_No"></param>
        /// <returns></returns>

        public DataSet GetSingleTeachingPlanEntry(int clgID, int TP_No, int UA_NO)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_TP_NO", TP_No);
                objParams[1] = new SqlParameter("@P_UA_NO", UA_NO);
                objParams[2] = new SqlParameter("@P_COLLEGE_ID", clgID);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TEACHING_PLAN_BY_NO", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TeachingPlanController.GetSingleTeachingPlanEntry -> " + ex.ToString());
            }
            return ds;
        }

        /// <summary>
        /// This controller is used to Delete Teachingpln.
        /// Page : TeachingplanMaster.aspx
        /// </summary>
        /// <param name="TeachingPlan_NO"></param>
        /// <returns></returns>

        public int DeleteTeachingPlan(int clgID, int TeachingPlan_NO, int uano)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_TP_NO", TeachingPlan_NO);
                objParams[1] = new SqlParameter("@P_UA_NO", uano);
                objParams[2] = new SqlParameter("@P_COLLEGE_ID", clgID);
                retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TEACHING_PLAN_DELETE", objParams, true));
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TeachingPlanController.DeleteTeachingPlan-> " + ex.ToString());
            }
            return retStatus;
        }

        /// <summary>
        /// THIS CONTROLLER IS USED TO FIND OUT THE SPECIFIC DAY (MONDAY/..) BY PASSING PARAMETER OF FROM DT TO DT AND DAY NO.
        /// Page : TeachingPlanMaster.aspx
        /// </summary>
        /// <param name="stdate"></param>
        /// <param name="enddate"></param>
        /// <param name="day"></param>
        /// <returns></returns>

        public DataSet GetTeachingPlanDate(int clgID, int session, int degree, int scheme, int semesterno, int sectionno, int batchno, int courseno, int ua_no, DateTime stdate, DateTime enddate, int day)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[12];

                objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                objParams[1] = new SqlParameter("@P_DEGREENO", degree);
                objParams[2] = new SqlParameter("@P_SCHEMENO", scheme);
                objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[4] = new SqlParameter("@P_SECTIONNO", sectionno);
                objParams[5] = new SqlParameter("@P_BATCHNO", batchno);
                objParams[6] = new SqlParameter("@P_COURSENO", courseno);
                objParams[7] = new SqlParameter("@P_UA_NO", ua_no);
                objParams[8] = new SqlParameter("@P_FROMDATE", stdate);
                objParams[9] = new SqlParameter("@P_TODATE", enddate);
                objParams[10] = new SqlParameter("@P_DAYNO", day);
                objParams[11] = new SqlParameter("@P_College_ID", clgID);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_SHOW_WEEK_DAYZ_FRMDT_TODT", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TeachingPlanController.GetSingleTeachingPlanEntry -> " + ex.ToString());
            }
            return ds;
        }

        /// <summary>
        /// This controller used to get Slot details.
        /// Page : TeachingplanMaster.aspx
        /// </summary>
        /// <param name="session"></param>
        /// <param name="ua_no"></param>
        /// <param name="courseno"></param>
        /// <param name="semesterno"></param>
        /// <param name="sectionno"></param>
        /// <param name="th_pr"></param>
        /// <param name="slot"></param>
        /// <param name="batchno"></param>
        /// <returns></returns>

        public DataSet GetSlot(int clgID, int session, int degree, int scheme, int semesterno, int sectionno, int batchno, int courseno, int ua_no, int slot, int dayNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[11];
                objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                objParams[1] = new SqlParameter("@P_DEGREENO", degree);
                objParams[2] = new SqlParameter("@P_SCHEMENO", scheme);
                objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[4] = new SqlParameter("@P_SECTIONNO", sectionno);   //SqlParameter("@P_UA_NO", ua_no);
                objParams[5] = new SqlParameter("@P_BATCHNO", batchno);     ///SqlParameter("@P_COURSENO", courseno);
                objParams[6] = new SqlParameter("@P_COURSENO", courseno);
                objParams[7] = new SqlParameter("@P_UA_NO", ua_no);
                objParams[8] = new SqlParameter("@P_SLOT", slot);
                objParams[9] = new SqlParameter("@P_DAYNO", dayNo);
                objParams[10] = new SqlParameter("@P_College_ID", clgID);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_SLOT", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TeachingPlanController.GetSingleTeachingPlanEntry -> " + ex.ToString());
            }
            return ds;
        }

        /// <summary>
        /// This controller is used to Lock Teaching plan.
        /// Page : TeachingplanMaster.apsx
        /// </summary>
        /// <param name="session"></param>
        /// <param name="ua_no"></param>
        /// <param name="section"></param>
        /// <param name="courseno"></param>
        /// <returns></returns>

        public int AddTeachingplanLock(int clgID, int session, int ua_no, int section, int courseno)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                objParams[2] = new SqlParameter("@P_COURSENO", courseno);
                objParams[3] = new SqlParameter("@P_SECTIONNO", section);
                objParams[4] = new SqlParameter("@P_COLLEGE_ID", clgID);
                objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[5].Direction = ParameterDirection.Output;

                if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_INSERT_TEACHINGPLAN_LOCK", objParams, false) != null)
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TeachingPlanController.AddTeachingPlan -> " + ex.ToString());
            }
            return retStatus;
        }

        public int UpdateTeachingplanLock(int session, int ua_no, int section, int courseno)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                objParams[2] = new SqlParameter("@P_COURSENO", courseno);
                objParams[3] = new SqlParameter("@P_SECTIONNO", section);
                objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[4].Direction = ParameterDirection.Output;

                if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_UPDATE_TEACHINGPLAN_LOCK", objParams, false) != null)
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TeachingPlanController.AddTeachingPlan -> " + ex.ToString());
            }
            return retStatus;
        }

        public DataSet GetBranchBasedsemester(int sessionno, int branchno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_DEGREENO", branchno);
                ds = objSQLHelper.ExecuteDataSetSP("ACD_SEMESTER_DURATIONWISE", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TeachingPlanController.GetSingleTeachingPlanEntry -> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetBranchBasedsemester_DIP(int sessionno, int branchno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_DEGREENO", branchno);
                ds = objSQLHelper.ExecuteDataSetSP("ACD_SEMESTER_DURATIONWISE_DIP", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TeachingPlanController.GetSingleTeachingPlanEntry -> " + ex.ToString());
            }
            return ds;
        }

        #endregion ACADEMIC_TEACHING_PLAN_MASTER

        #region ACADEMIC_DAILY_TIMETABLE_SLOT_MASTER

        //USED FOR ACADEMIC_TIMETABLE_SLOT_MASTER_ENTRY
        /// <summary> <ADDED BY >RAJU BITODE
        /// This controller is used to insert Academic time table slot.
        /// Page: TimeTableSlotMaster.aspx
        /// </summary>
        /// <param name="sessionno"></param>
        /// <param name="degreeno"></param>
        /// <param name="slotname"></param>
        /// <param name="timefrom"></param>
        /// <param name="timeto"></param>
        /// <returns></returns>

        public int AddAcademic_TT_Slot(int collegeID, int sessionno, int degreeno, string slotname, string timefrom, string timeto, int slottype)//, int lunchBreak, int OrderNo
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[8];
                objParams[0] = new SqlParameter("@P_DEGREENO", degreeno);
                objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[2] = new SqlParameter("@P_SLOTNAME", slotname);
                objParams[3] = new SqlParameter("@P_SLOTTYPE", slottype);
                objParams[4] = new SqlParameter("@P_TIMEFROM", timefrom);
                objParams[5] = new SqlParameter("@P_TIMETO", timeto);
                objParams[6] = new SqlParameter("@P_COLLEGEID", collegeID);
                //objParams[7] = new SqlParameter("@P_LunchBreak", lunchBreak);
                //objParams[8] = new SqlParameter("@P_OrderNo", OrderNo);
                objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[7].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_ACADEMIC_TT_SLOT_INSERT", objParams, true);
                retStatus = Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AcdAttendanceController.cs.AddAcademic_TT_Slot -> " + ex.ToString());
            }
            return retStatus;
        }

        ///  <ADDED BY >RAJU BITODE
        /// This controller is used to Update Academic TimeTable SLot.
        /// Page : TimeTableSlotMaster.aspx
        ///
        /// <param name="slotno"></param>
        /// <param name="slotname"></param>
        /// <param name="degreeno"></param>
        /// <param name="sessionno"></param>
        /// <param name="timefrom"></param>
        /// <param name="timeto"></param>
        /// <returns></returns>

        public int UpdateAcademic_TT_Slot(int collegeID, int slotno, string slotname, int degreeno, int sessionno, string timefrom, string timeto, int slottype)//, int lunchBreak, int OrderNo
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[9];
                objParams[0] = new SqlParameter("@P_SLOTNO", slotno);
                objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                objParams[2] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[3] = new SqlParameter("@P_SLOTNAME", slotname);
                objParams[4] = new SqlParameter("@P_SLOTTYPE", slottype);
                objParams[5] = new SqlParameter("@P_TIMEFROM", timefrom);
                objParams[6] = new SqlParameter("@P_TIMETO", timeto);
                objParams[7] = new SqlParameter("@P_COLLEGEID", collegeID);
                //objParams[8] = new SqlParameter("@P_LunchBreak", lunchBreak);
                //objParams[9] = new SqlParameter("@P_OrderNo", OrderNo);
                objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[8].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_ACADEMIC_TT_SLOT_UPDATE", objParams, true);
                retStatus = Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TeachingPlanController.cs.UpdateAcademicSlot -> " + ex.ToString());
            }
            return retStatus;
        }

        /// <ADDED BY >RAJU BITODE
        /// This controller is used to get single record of academic time table slots by slotno
        /// Page: TimeTableSlotMaster.apsx
        /// </summary>
        /// <param name="slotno"></param>
        /// <returns></returns>

        public DataSet GetSingleAcademic_TT_Slot(int slotno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_SLOTNO", slotno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_ACADEMIC_TT_SLOT", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TeachingPlanController.cs.GetSingleAcademicSlot -> " + ex.ToString());
            }
            return ds;
        }

        public int DeleteAcademic_TT_Slot(int SlotNo)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_SLOTNO", SlotNo);

                retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_SP_DELETE_ACADEMIC_TT_SLOT_NO", objParams, true));
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TeachingPlanController.DeleteAcademic_TT_Slot-> " + ex.ToString());
            }
            return retStatus;
        }

        #endregion ACADEMIC_DAILY_TIMETABLE_SLOT_MASTER

        #region Week Master

        //added by neha 11july19
        public int AddWeekMaster(int sessionno, DateTime weekstartDate, DateTime weekendDate, int weekno)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_WEEK_STARTDATE", weekstartDate);
                objParams[2] = new SqlParameter("@P_WEEK_ENDDATE", weekendDate);
                objParams[3] = new SqlParameter("@P_WEEKNO", weekno);
                objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[4].Direction = ParameterDirection.Output;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_INSERT_WEEK", objParams, true);
                retStatus = (int)obj;
            }
            catch (Exception ex)
            {
                retStatus = -99;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AcdAttendanceController.AddWEEK-> " + ex.ToString());
            }

            return retStatus;
        }

        public int UpdateWeekMaster(int weekid, int weekno, int sessionno, DateTime weekstartDate, DateTime weekendDate)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_WEEKId", weekid);
                objParams[1] = new SqlParameter("@P_WEEKNO", weekno);
                objParams[2] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[3] = new SqlParameter("@P_WEEK_STARTDATE", weekstartDate);
                objParams[4] = new SqlParameter("@P_WEEK_ENDDATE", weekendDate);
                objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[5].Direction = ParameterDirection.Output;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_UPDATE_WEEK", objParams, true);
                retStatus = (int)obj;
            }
            catch (Exception ex)
            {
                retStatus = -99;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AcdAttendanceController.UpdateWEEK-> " + ex.ToString());
            }

            return retStatus;
        }

        public SqlDataReader GetWeekDetails(int weekid)
        {
            SqlDataReader dr = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_WEEKId", weekid);
                dr = objSQLHelper.ExecuteReaderSP("PKG_GET_WEEK_DETAILS_BY_WEEKID", objParams);
            }
            catch (Exception ex)
            {
                return dr;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AcdAttendanceController.GetWeekDetails-> " + ex.ToString());
            }
            return dr;
        }

        public DataSet GetAllWeekDetails()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[0];
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ALL_WEEK_DETAILS", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AcdAttendanceController.GetWeekDetails-> " + ex.ToString());
            }
            return ds;
        }

        #endregion Week Master

        #region Alternate Att.

        //Added By Pritish on 09/07/2019
        public DataSet GetAllAlternateAttendance(DateTime attdate, int uano)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_ATTDATE", attdate);
                objParams[1] = new SqlParameter("@P_UANO", uano);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ALTERNATE_CAN_ATT", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.GetStudentForFaculty-> " + ex.ToString());
            }
            return ds;
        }

        public int AlternateAttCancel(int aano, int uano, string ipaddress, DateTime candate, int sessionno, int schemeno, int semesterno, int sectionno, int batchno, int slotno)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[11];
                objParams[0] = new SqlParameter("@P_AANO", aano);
                objParams[1] = new SqlParameter("@P_UANO", uano);
                objParams[2] = new SqlParameter("@P_IPADDRESS", ipaddress);
                objParams[3] = new SqlParameter("@P_CANDATE", candate);
                objParams[4] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[5] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[6] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[7] = new SqlParameter("@P_SECTIONNO", sectionno);
                objParams[8] = new SqlParameter("@P_BATCHNO", batchno);
                objParams[9] = new SqlParameter("@P_SLOTNO", slotno);

                objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[10].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_CAN_ALTERNATE_ATTENDANCE", objParams, true);
                retStatus = Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                retStatus = -99;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AcdAttendanceController.InsertUpdateClassType-> " + ex.ToString());
            }
            return retStatus;
        }

        public DataSet GetScheduleFacultyCourses(int uano, DateTime date)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_UANO", uano);
                objParams[1] = new SqlParameter("@P_DATE", date);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_FACULTY_COURSES", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.GetStudentForFaculty-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetTakenFacultyCourses(int uano, DateTime date, int semesterno, int sectionno, int batchno, int schemeno, int atttype, int sessionno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[8];
                objParams[0] = new SqlParameter("@P_UANO", uano);
                objParams[1] = new SqlParameter("@P_DATE", date);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                objParams[4] = new SqlParameter("@P_BATCHNO", batchno);
                objParams[5] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[6] = new SqlParameter("@P_ATTTYPE", atttype);
                objParams[7] = new SqlParameter("@P_SESSIONNO", sessionno);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_TAKEN_FACULTY_COURSES", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.GetStudentForFaculty-> " + ex.ToString());
            }
            return ds;
        }

        public int InsertAlternateAttendance(int sessionno, DateTime attdate, int deptno, int lecturetype, int slotno, int schedulecourse, int scheduleuano,
                                             int takencourse, int takenuano, int cancel, DateTime transdate, string ipaddress, int uano, int transfertype, int semesterno,
                                             int sectionno, int batchno, int swapno, int translot, int schemeno, int ttno, int alternateno, int collegeid)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[24];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_ATT_DATE", attdate);
                objParams[2] = new SqlParameter("@P_DEPTNO", deptno);
                objParams[3] = new SqlParameter("@P_LECTURE_TYPE", lecturetype);
                objParams[4] = new SqlParameter("@P_SLOTNO", slotno);
                objParams[5] = new SqlParameter("@P_SCHEDULE_COURSENO", schedulecourse);
                objParams[6] = new SqlParameter("@P_SCHEDULE_UANO", scheduleuano);
                objParams[7] = new SqlParameter("@P_TAKEN_COURSENO", takencourse);
                objParams[8] = new SqlParameter("@P_TAKEN_UANO", takenuano);
                objParams[9] = new SqlParameter("@P_CANCEL_STATUS", cancel);
                objParams[10] = new SqlParameter("@P_TRANS_DATE", transdate);
                objParams[11] = new SqlParameter("@P_IPADDRESS", ipaddress);
                objParams[12] = new SqlParameter("@P_UA_NO", uano);
                objParams[13] = new SqlParameter("@P_TRANSFERTYPE", transfertype);
                objParams[14] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[15] = new SqlParameter("@P_SECTIONNO", sectionno);
                objParams[16] = new SqlParameter("@P_BATCHNO", batchno);
                objParams[17] = new SqlParameter("@P_SWAPNO", swapno);
                objParams[18] = new SqlParameter("@P_TRANSLOT", translot);
                objParams[19] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[20] = new SqlParameter("@P_TTNO", ttno);
                objParams[21] = new SqlParameter("@P_ALTERNATENO", alternateno);
                objParams[22] = new SqlParameter("@P_COLLEGE_ID", collegeid);

                objParams[23] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[23].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_ALTERNATE_ATTENDANCE_INSERT", objParams, true);
                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TeachingPlanController.cs.InsertAlternateAttendance -> " + ex.ToString());
            }
            return retStatus;
        }

        #endregion Alternate Att.

        #region Holiday Master

        public int AddHDay(AcdAttendanceModel objHM)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[15];
                objParams[0] = new SqlParameter("@P_HOLIDAY_NAME", objHM.holidayname);
                objParams[1] = new SqlParameter("@P_HOLIDAY_DATE", objHM.holidaydate);
                objParams[2] = new SqlParameter("@P_SESSIONNO", objHM.HsessionNo);
                objParams[3] = new SqlParameter("@P_DEGREENO", objHM.HdegreeNo);
                objParams[4] = new SqlParameter("@P_BRANCHNO", objHM.HbranchNo);
                objParams[5] = new SqlParameter("@P_SCHEMENO", objHM.HschemeNo);
                objParams[6] = new SqlParameter("@P_SEMESTERNO", objHM.HsemesterNo);
                objParams[7] = new SqlParameter("@P_SUBID", objHM.HsubId);
                objParams[8] = new SqlParameter("@P_SECTIONNO", objHM.HSectionNo);
                objParams[9] = new SqlParameter("@P_BATCHNO", objHM.HBatchNo);
                objParams[10] = new SqlParameter("@P_DAYNO", objHM.HdayNo);
                objParams[11] = new SqlParameter("@P_SLOTS", objHM.Hslots);
                objParams[12] = new SqlParameter("@P_REASON", objHM.Hreason);
                objParams[13] = new SqlParameter("@P_LOCKSTATUS", objHM.lockHoliday);

                objParams[14] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[14].Direction = ParameterDirection.Output;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_INSERT_HOLIDAY", objParams, true);
                retStatus = (int)obj;
            }
            catch (Exception ex)
            {
                retStatus = -99;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AcdAttendanceController.AddHDay-> " + ex.ToString());
            }

            return retStatus;
        }

        public int UpdateHDay(AcdAttendanceModel objHM)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[16];
                objParams[0] = new SqlParameter("@P_HOLIDAY_NO", objHM.holidayNo);
                objParams[1] = new SqlParameter("@P_HOLIDAY_NAME", objHM.holidayname);
                objParams[2] = new SqlParameter("@P_HOLIDAY_DATE", objHM.holidaydate);
                objParams[3] = new SqlParameter("@P_SESSIONNO", objHM.HsessionNo);
                objParams[4] = new SqlParameter("@P_DEGREENO", objHM.HdegreeNo);
                objParams[5] = new SqlParameter("@P_BRANCHNO", objHM.HbranchNo);
                objParams[6] = new SqlParameter("@P_SCHEMENO", objHM.HschemeNo);
                objParams[7] = new SqlParameter("@P_SEMESTERNO", objHM.HsemesterNo);
                objParams[8] = new SqlParameter("@P_SUBID", objHM.HsubId);
                objParams[9] = new SqlParameter("@P_SECTIONNO", objHM.HSectionNo);
                objParams[10] = new SqlParameter("@P_BATCHNO", objHM.HBatchNo);
                objParams[11] = new SqlParameter("@P_DAYNO", objHM.HdayNo);
                objParams[12] = new SqlParameter("@P_SLOTS", objHM.Hslots);
                objParams[13] = new SqlParameter("@P_REASON", objHM.Hreason);
                objParams[14] = new SqlParameter("@P_LOCKSTATUS", objHM.lockHoliday);
                objParams[15] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[15].Direction = ParameterDirection.Output;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_UPDATE_HOLIDAY", objParams, true);
                retStatus = (int)obj;
            }
            catch (Exception ex)
            {
                retStatus = -99;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AcdAttendanceController.UpdateHDay-> " + ex.ToString());
            }

            return retStatus;
        }

        /// <summary>
        /// CREATED BY :RAJU BITODE
        /// DATE       :19-04-2019
        /// Get Holidays
        /// Used in Page : HolidayMaster.aspx.cs
        /// </summary>
        public SqlDataReader GetHoliday(int HdayNo)
        {
            SqlDataReader dr = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_HOLIDAY_NO", HdayNo);
                dr = objSQLHelper.ExecuteReaderSP("PKG_GET_HOLIDAY_BY_HDAYNO", objParams);
            }
            catch (Exception ex)
            {
                return dr;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AcdAttendanceController.GetHoliday-> " + ex.ToString());
            }
            return dr;
        }

        public DataSet GetHoliday()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[0];
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ALL_HOLIDAY", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AcdAttendanceController.GetHoliday-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetFilterWiseHolidayMaster(int sessionno, int degreeno, int branchno, int schemeno, int semesterno, int subid, int sectionno, int batchno, int dayno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                SqlParameter[] objParams = new SqlParameter[9];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                objParams[2] = new SqlParameter("@P_BRANCHNO", branchno);
                objParams[3] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[4] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[5] = new SqlParameter("@P_SUBID", subid);
                objParams[6] = new SqlParameter("@P_SECTIONNO", sectionno);
                objParams[7] = new SqlParameter("@P_BATCHNO", batchno);
                objParams[8] = new SqlParameter("@P_DAYNO", dayno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_FILTER_WISE_HOLIDAY_DETAILS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AttendanceController.GetFilterWiseHolidayMaster-> " + ex.ToString());
            }

            return ds;
        }

        #endregion Holiday Master

        #region DayMaster

        //Added by Neha Baranwal 08Aug19
        public int AddDayMaster(string DayName, string collegeCode)
        {
            int status = -99;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_DAY_NAME", DayName),
                            new SqlParameter("@P_COLLEGE_CODE", collegeCode),
                            new SqlParameter("@P_OUTPUT", status)
                        };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_DAY_MASTER_INSERT", sqlParams, true);
                status = (Int32)obj;
            }
            catch (Exception ex)
            {
                status = -99;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentFeedBackController.AddFeedbackMaster() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        public int UpdateDayMaster(int DayNo, string DayName, string collegeCode)
        {
            int status = -99;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                            {
                                 new SqlParameter("@P_DAY_NO", DayNo),
                                 new SqlParameter("@P_DAY_NAME", DayName),
                                 new SqlParameter("@P_COLLEGE_CODE", collegeCode),
                                 new SqlParameter("@P_OUTPUT",status)
                            };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_DAY_MASTER_UPDATE", sqlParams, true);
                status = (Int32)obj;
            }
            catch (Exception ex)
            {
                status = -99;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentFeedBackController.UpdateFeedbackMaster() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        public DataSet GetAllDays()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[0];

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_DAY_MASTER_GET_ALL", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentFeedBackController.GetAllFeedback() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public SqlDataReader GetDayNo(int DayNo)
        {
            SqlDataReader dr = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[] { new SqlParameter("@P_OUTPUT", DayNo) };

                dr = objSQLHelper.ExecuteReaderSP("PKG_ACD_DAY_MASTER_GET_BY_DAY_NO", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentFeedBackController.GetFeedbackNo() --> " + ex.Message + " " + ex.StackTrace);
            }
            return dr;
        }

        #endregion DayMaster

        #region Facultywise Attendance Status

        //Added By Pritish S. on 26/08/2019 for Facultywise Attendance Status Details
        public DataSet GetFacultywiseAttendanceStatus(int sessionno, int schemeno, int semesterno, int sectionno, int courseno, int uano, int collegeId)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[7];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                objParams[4] = new SqlParameter("@P_COURSENO", courseno);
                objParams[5] = new SqlParameter("@P_UA_NO", uano);
                objParams[6] = new SqlParameter("@P_College_ID", collegeId);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_FACULTYWISE_ATTENDANCE_STATUS", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AcdAttendanceController.GetSubjectDetails-> " + ex.ToString());
            }
            return ds;
        }

        #endregion Facultywise Attendance Status

        #region SlotMaster

        //Added by Neha Baranwal 31Aug19
        //Used in SLOTMASTER.ASPX

        public int AddSlotMaster(string slotName, string collegeCode, int Active_Status)
        {
            int status = -99;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_SLOTTYPE_NAME", slotName),
                            new SqlParameter("@P_COLLEGE_CODE", collegeCode),
                            new SqlParameter("@P_ACTIVESTATUS",Active_Status),
                            new SqlParameter("@P_OUTPUT", status)
                        };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_SLOT_MASTER_INSERT", sqlParams, true);
                status = (Int32)obj;
            }
            catch (Exception ex)
            {
                status = -99;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AcdAttendanceController.AddSlotMaster() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        public int UpdateSlotMaster(int slotNo, string slotName, string collegeCode, int Active_Status)
        {
            int status = -99;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                            {
                                 new SqlParameter("@P_SLOTTYPENO", slotNo),
                                 new SqlParameter("@P_SLOTTYPE_NAME", slotName),
                                 new SqlParameter("@P_COLLEGE_CODE", collegeCode),
                                 new SqlParameter("@P_ACTIVESTATUS",Active_Status),
                                 new SqlParameter("@P_OUTPUT",status)
                            };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_SLOT_MASTER_UPDATE", sqlParams, true);
                status = (Int32)obj;
            }
            catch (Exception ex)
            {
                status = -99;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AcdAttendanceController.UpdateSlotMaster() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        public DataSet GetAllSlots()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[0];

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_SLOT_MASTER_GET_ALL", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentFeedBackController.GetAllSlots() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public SqlDataReader GetSlotNo(int slotNo)
        {
            SqlDataReader dr = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[] { new SqlParameter("@P_OUTPUT", slotNo) };

                dr = objSQLHelper.ExecuteReaderSP("PKG_ACD_SLOT_MASTER_GET_BY_SLOT_NO", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentFeedBackController.GetslotNo() --> " + ex.Message + " " + ex.StackTrace);
            }
            return dr;
        }

        #endregion SlotMaster

        #region SendAttendanceSMS

        //Added by RAJU BITODE 06 SEPT 2019
        //Used in SendSmstoParents.aspx

        public DataSet GetSelectedDateSlotsForSMS(int sessionno, int degree, int branch, int semester, int section, DateTime date, int collegeID)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[7];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_DEGREENO", degree);
                objParams[2] = new SqlParameter("@P_BRANCHNO", branch);
                objParams[3] = new SqlParameter("@P_SEMESTERNO", semester);
                objParams[4] = new SqlParameter("@P_SECTIONNO", section);
                objParams[5] = new SqlParameter("@P_DATE", date);
                objParams[6] = new SqlParameter("@P_COLLEGE_ID", collegeID);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_SELECTED_DATE_SLOT_FOR_SMS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AcdAttendanceController.GetSelectedDateSlotsForSMS-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetDateSlotsAbsentStud(int sessionno, int degree, int branch, int semester, int section, int slot, DateTime date, int collegeID)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[8];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_DEGREENO", degree);
                objParams[2] = new SqlParameter("@P_BRANCHNO", branch);
                objParams[3] = new SqlParameter("@P_SEMESTERNO", semester);
                objParams[4] = new SqlParameter("@P_SECTIONNO", section);
                objParams[5] = new SqlParameter("@P_SLOT", slot);
                objParams[6] = new SqlParameter("@P_DATE", date);
                objParams[7] = new SqlParameter("@P_COLLEGE_ID", collegeID);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_SELECTED_DATE_SLOT_ABSENT_STUDENT", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AcdAttendanceController.GetDateSlotsAbsentStud-> " + ex.ToString());
            }
            return ds;
        }

        public int INSERTPARENTSMSLOG(int userno, string message, string parentmobileno, int usertype, int idno, int MSGTYPE, int slot, DateTime date)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_USERNO",userno),
                    new SqlParameter("@P_MSG_CONTENT", message),
                    new SqlParameter("@P_PARENTMOBILENO", parentmobileno),
                    new SqlParameter("@P_USERTYPE",usertype),
                    new SqlParameter("@P_IDNO",idno),
                    new SqlParameter("@P_MSGTYPE",MSGTYPE),
                    new SqlParameter("@P_SLOT", slot),
                    new SqlParameter("@P_DATE",date),
                    new SqlParameter("@P_MSGID", status)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_BULKSMS_INSERT_ATT", sqlParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AcdAttendanceController.INSERTPARENTSMSLOG() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        public DataSet GetSubjectWiseAttPer(int sessionno, int degree, int branch, int semester, int section, DateTime frmdate, DateTime toDate)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[7];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_DEGREENO", degree);
                objParams[2] = new SqlParameter("@P_BRANCHNO", branch);
                objParams[3] = new SqlParameter("@P_SEMESTERNO", semester);
                objParams[4] = new SqlParameter("@P_SECTIONNO", section);
                objParams[5] = new SqlParameter("@P_FROMDATE", frmdate);
                objParams[6] = new SqlParameter("@P_TODATE", toDate);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_SUBJECTWISE_ATT_PERCENTAGE", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AcdAttendanceController.GetSubjectWiseAttPer-> " + ex.ToString());
            }
            return ds;
        }

        #endregion SendAttendanceSMS

        #region Time Table Shift

        public DataSet GetSelectedDayTimeTable(int sessionno, int degreeno, int branchno, int schemeno, int semesterno, int sectionno, int slottype, int dayNo, DateTime shiftdate, DateTime startdate, DateTime enddate, int clgID)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[12];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                objParams[2] = new SqlParameter("@P_BRANCHNO", branchno);
                objParams[3] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[4] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[5] = new SqlParameter("@P_SECTIONNO", sectionno);
                objParams[6] = new SqlParameter("@P_SLOTTYPE", slottype);
                objParams[7] = new SqlParameter("@P_DAYNO", dayNo);
                objParams[8] = new SqlParameter("@P_SHIFT_DATE", shiftdate);
                objParams[9] = new SqlParameter("@P_START_DATE", startdate);
                objParams[10] = new SqlParameter("@P_END_DATE", enddate);
                objParams[11] = new SqlParameter("@P_COLLEGE_ID", clgID);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_SELECTED_DAY_TIMETABLE", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AcdAttendanceController.GetSubjectDetails-> " + ex.ToString());
            }
            return ds;
        }

        public int AddShiftTimeTable(AcdAttendanceModel objc, string batchnos, string coursenos, string ua_nos, string slotnos, string subids, int dayno, string roomnos)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[17];
                objParams[0] = new SqlParameter("@P_SESSIONNO", objc.Sessionno);
                objParams[1] = new SqlParameter("@P_DEGREENO", objc.DEGREENO);
                objParams[2] = new SqlParameter("@P_SCHEMENO", objc.SCHEMENO);
                objParams[3] = new SqlParameter("@P_SEMESTERNO", objc.SEMESTERNO);
                objParams[4] = new SqlParameter("@P_SECTIONNO", objc.Sectionno);
                objParams[5] = new SqlParameter("@P_BATCHNOS", batchnos);           //------------//
                objParams[6] = new SqlParameter("@P_COURSENOS", coursenos);
                objParams[7] = new SqlParameter("@P_UA_NOS", ua_nos);
                objParams[8] = new SqlParameter("@P_SLOTNOS", slotnos);
                objParams[9] = new SqlParameter("@P_SUBIDS", subids);
                objParams[10] = new SqlParameter("@P_DAYNO", dayno);        //------------//
                objParams[11] = new SqlParameter("@P_SHIFT_TTDATE", objc.ToDate);
                objParams[12] = new SqlParameter("@P_CREATED_BY", objc.UA_No);      //------------//
                objParams[13] = new SqlParameter("@P_IP_ADDRESS", objc.Ipaddress);
                objParams[14] = new SqlParameter("@P_COLLEGE_ID", objc.CollegeId);
                objParams[15] = new SqlParameter("@P_ROOMNOS", roomnos);
                objParams[16] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[16].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_UPD_SHIFT_TIMETABLE", objParams, true);

                if (ret != null && ret.ToString() == "1")
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                else if (ret != null && ret.ToString() == "2")
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                else if (ret != null && ret.ToString() == "3")
                    retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                else
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.AddLevel-> " + ex.ToString());
            }
            return retStatus;
        }

        #endregion Time Table Shift

        #region BULK OD

        //Bulk OD Apply and Approve
        public DataSet GetSelectedDateSlotsOfBulkStudents(int clgID, int sessionno, int degreeno, int branchno, int schemeno, int semesterno, int sectionno, DateTime date) //string idnos,
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[8];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                objParams[2] = new SqlParameter("@P_BRANCHNO", branchno);
                objParams[3] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[4] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[5] = new SqlParameter("@P_SECTIONNO", sectionno);
                objParams[6] = new SqlParameter("@P_DATE", date);
                objParams[7] = new SqlParameter("@P_COLLEGE_ID", clgID);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_SELECTED_DATE_SLOT_OF_BULK_STUDENTS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AcdAttendanceController.GetSelectedDateSlotsOfBulkStudents-> " + ex.ToString());
            }
            return ds;
        }

        public int AddBulkODLeaveDetails(AcdAttendanceModel objAttModel, string idno, string regno, string slotNos, int odType)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;
                //Add
                objParams = new SqlParameter[12];

                objParams[0] = new SqlParameter("@P_SESSIONNO", objAttModel.Sessionno);
                objParams[1] = new SqlParameter("@P_IDNOS", idno);
                objParams[2] = new SqlParameter("@P_REGNOS", regno);
                objParams[3] = new SqlParameter("@P_LEAVE_NAME", objAttModel.LEAVENO);
                objParams[4] = new SqlParameter("@P_HOLIDAY_DETAIL", objAttModel.Event_Detail);
                objParams[5] = new SqlParameter("@P_SLOTNOS", slotNos);

                if (objAttModel.LeaveStartDate == DateTime.MinValue)
                    objParams[6] = new SqlParameter("@P_HOLIDAY_STDATE", DBNull.Value);
                else
                    objParams[6] = new SqlParameter("@P_HOLIDAY_STDATE", objAttModel.LeaveStartDate);

                objParams[7] = new SqlParameter("@P_HOLIDAY_ENDDATE", objAttModel.LeaveEndDate);
                objParams[8] = new SqlParameter("@P_UA_NO_TRAN", objAttModel.UA_NO_TRAN);
                objParams[9] = new SqlParameter("@P_ODTYPE", odType);
                objParams[10] = new SqlParameter("@P_COLLEGE_CODE", objAttModel.College_code);
                objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[11].Direction = ParameterDirection.Output;
                if (objSQLHelper.ExecuteNonQuerySP("PKG_ACADEMIC_SESSION_SP_BULK_INS_LEAVE_DETAIL", objParams, true) != null)
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.AddSession-> " + ex.ToString());
            }
            return retStatus;
        }

        public DataSet GetStudentsWithODEntry(int schemeno, int degreeno, int semno, int sectionno, DateTime startdate)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                SqlParameter[] objParams = new SqlParameter[5];

                objParams[0] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                objParams[2] = new SqlParameter("@P_SEM", semno);
                objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                objParams[4] = new SqlParameter("@P_HOLIDAY_STDATE", startdate);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_STUDENTS_WITH_CHECK_ENTRY_OD_APPLY", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AcdAttendanceController.GetStudentsWithODEntry-> " + ex.ToString());
            }

            return ds;
        }

        public int ApproveORRejectBulkODLeaveDetails(AcdAttendanceModel objAttModel, string holidaynos, int approvalStatus)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_HOLIDAY_NOS", holidaynos);
                objParams[1] = new SqlParameter("@P_APPROVED_BY", objAttModel.UA_NO_TRAN);
                objParams[2] = new SqlParameter("@P_APPROVAL_STATUS", approvalStatus);
                if (objSQLHelper.ExecuteNonQuerySP("PKG_ACADEMIC_SESSION_SP_BULK_UPD_APPROVAL_STATUS", objParams, false) != null)
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AcdAttendanceController.UpdateBulkODLeaveDetails-> " + ex.ToString());
            }

            return retStatus;
        }

        public DataSet GetStudentsWithODEntry(int schemeno, int degreeno, int semno, int sectionno, DateTime startdate, DateTime enddate)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                SqlParameter[] objParams = new SqlParameter[6];

                objParams[0] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                objParams[2] = new SqlParameter("@P_SEM", semno);
                objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                objParams[4] = new SqlParameter("@P_HOLIDAY_STDATE", startdate);
                objParams[5] = new SqlParameter("@P_HOLIDAY_ENDDATE", enddate);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_STUDENTS_WITH_CHECK_ENTRY_OD_APPLY", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AcdAttendanceController.GetStudentsWithODEntry-> " + ex.ToString());
            }

            return ds;
        }

        public DataSet GetAppliedStudentsForOD(int schemeno, int degreeno, int semno, int sectionno, DateTime startdate, DateTime enddate)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                SqlParameter[] objParams = new SqlParameter[6];

                objParams[0] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                objParams[2] = new SqlParameter("@P_SEM", semno);
                objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                objParams[4] = new SqlParameter("@P_HOLIDAY_STDATE", startdate);
                objParams[5] = new SqlParameter("@P_HOLIDAY_ENDDATE", enddate);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_APPLIED_STUDENTS_FOR_OD", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AcdAttendanceController.GetAppliedStudentsForOD-> " + ex.ToString());
            }

            return ds;
        }

        public DataSet GetAllSlots(int schemeno, int degreeno, int semno, int sectionno, DateTime startdate)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                SqlParameter[] objParams = new SqlParameter[5];

                objParams[0] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                objParams[2] = new SqlParameter("@P_SEM", semno);
                objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                objParams[4] = new SqlParameter("@P_HOLIDAY_STDATE", startdate);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ALL_SLOTS_FROM_OD", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AcdAttendanceController.GetAllSlots-> " + ex.ToString());
            }

            return ds;
        }

        public DataSet GetAppliedStudentsForOD(int schemeno, int degreeno, int semno, int sectionno, DateTime startdate)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                SqlParameter[] objParams = new SqlParameter[5];

                objParams[0] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                objParams[2] = new SqlParameter("@P_SEM", semno);
                objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                objParams[4] = new SqlParameter("@P_HOLIDAY_STDATE", startdate);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_APPLIED_STUDENTS_FOR_OD", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AcdAttendanceController.GetAppliedStudentsForOD-> " + ex.ToString());
            }

            return ds;
        }

        public DataSet GetStudentsWithODEntry(int schemeno, int degreeno, int semno, int sectionno, DateTime startdate, DateTime enddate, int userno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                SqlParameter[] objParams = new SqlParameter[7];

                objParams[0] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                objParams[2] = new SqlParameter("@P_SEM", semno);
                objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                objParams[4] = new SqlParameter("@P_HOLIDAY_STDATE", startdate);
                objParams[5] = new SqlParameter("@P_HOLIDAY_ENDDATE", enddate);
                objParams[6] = new SqlParameter("@P_FAC_ADVISOR", userno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_STUDENTS_WITH_CHECK_ENTRY_OD_APPLY", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AcdAttendanceController.GetStudentsWithODEntry-> " + ex.ToString());
            }

            return ds;
        }

        public DataSet GetAppliedStudentsForOD(int clgId, int sessionno, int schemeno, int degreeno, int semno, int sectionno, DateTime startdate, DateTime enddate)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                SqlParameter[] objParams = new SqlParameter[8];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[2] = new SqlParameter("@P_DEGREENO", degreeno);
                objParams[3] = new SqlParameter("@P_SEM", semno);
                objParams[4] = new SqlParameter("@P_SECTIONNO", sectionno);
                objParams[5] = new SqlParameter("@P_HOLIDAY_STDATE", startdate);
                objParams[6] = new SqlParameter("@P_HOLIDAY_ENDDATE", enddate);
                objParams[7] = new SqlParameter("@P_COLLEGE_ID", clgId);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_APPLIED_STUDENTS_FOR_OD", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AcdAttendanceController.GetAppliedStudentsForOD-> " + ex.ToString());
            }

            return ds;
        }

        public DataSet GetStudentsWithODEntry(int clgId, int sessionno, int schemeno, int degreeno, int semno, int sectionno, DateTime startdate, DateTime enddate, int userno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                SqlParameter[] objParams = new SqlParameter[9];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[2] = new SqlParameter("@P_DEGREENO", degreeno);
                objParams[3] = new SqlParameter("@P_SEM", semno);
                objParams[4] = new SqlParameter("@P_SECTIONNO", sectionno);
                objParams[5] = new SqlParameter("@P_HOLIDAY_STDATE", startdate);
                objParams[6] = new SqlParameter("@P_HOLIDAY_ENDDATE", enddate);
                objParams[7] = new SqlParameter("@P_FAC_ADVISOR", userno);
                objParams[8] = new SqlParameter("@P_COLLEGE_ID", clgId);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_STUDENTS_WITH_CHECK_ENTRY_OD_APPLY", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AcdAttendanceController.GetStudentsWithODEntry-> " + ex.ToString());
            }

            return ds;
        }

        public DataSet GetAllSlots(int clgId, int sessionno, int schemeno, int degreeno, int semno, int sectionno, DateTime startdate)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                SqlParameter[] objParams = new SqlParameter[7];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[2] = new SqlParameter("@P_DEGREENO", degreeno);
                objParams[3] = new SqlParameter("@P_SEM", semno);
                objParams[4] = new SqlParameter("@P_SECTIONNO", sectionno);
                objParams[5] = new SqlParameter("@P_HOLIDAY_STDATE", startdate);
                objParams[6] = new SqlParameter("@P_COLLEGE_ID", clgId);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ALL_SLOTS_FROM_OD", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AcdAttendanceController.GetAllSlots-> " + ex.ToString());
            }

            return ds;
        }

        #endregion BULK OD

        #region Canceled TimeTable/Attendance

        //new 14-06-2023
        //for both Regular+Revised and Shifted time table and Attendance and Both(Cancel_TimeTable.aspx)
        public int CancelTimeTable(int ttno, int uano, string ipaddress, string remarks, int cancellationtype, int attno)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[7];
                objParams[0] = new SqlParameter("@P_TTNO", ttno);
                objParams[1] = new SqlParameter("@P_CANCEL_BY", uano);
                objParams[2] = new SqlParameter("@P_CANCEL_IPADDRESS", ipaddress);
                objParams[3] = new SqlParameter("@P_REMARKS", remarks);
                objParams[4] = new SqlParameter("@P_CANCELLATION_TYPE", cancellationtype);
                objParams[5] = new SqlParameter("@P_ATT_NO", attno);
                objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[6].Direction = ParameterDirection.Output;
                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_TIMETABLE_CANCEL", objParams, true);
                retStatus = (int)obj;
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.acdAttendanceController.CancelTimeTable-> " + ex.ToString());
            }

            return retStatus;
        }

        //load Attendance Details for single dates
        public DataSet LoadAttendanceDetails(int sessionno, int schemeno, int semesterno, int sectionno, int slottype, DateTime date)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                objParams[4] = new SqlParameter("@P_SLOTTYPE", slottype);
                objParams[5] = new SqlParameter("@P_DATE", date);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_FACULTY_ATTENDANCE_DETAILS", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.AcdAttendanceController.LoadAttendanceDetails-> " + ex.ToString());
            }
            finally
            {
                ds.Dispose();
            }
            return ds;
        }

        //modified by SHAHBAZ AHMAD 14-06-2023
        public DataSet LoadAttendanceDetails(int sessionno, int schemeno, int semesterno, int sectionno, int slottype, DateTime startdate, DateTime enddate)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[7];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                objParams[4] = new SqlParameter("@P_SLOTTYPE", slottype);
                objParams[5] = new SqlParameter("@P_STARTDATE", startdate);
                objParams[6] = new SqlParameter("@P_ENDDATE", enddate);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_FACULTY_ATTENDANCE_DETAILS", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.AcdAttendanceController.LoadAttendanceDetails-> " + ex.ToString());
            }
            finally
            {
                ds.Dispose();
            }
            return ds;
        }

        //load Attendance + TIME TABLE Details for multiple dates
        //new 14-06-2023
        public DataSet LoadBothAttAndTTDetails(int sessionno, int schemeno, int semesterno, int sectionno, int slottype, DateTime startdate, DateTime enddate, int college_id)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[8];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                objParams[4] = new SqlParameter("@P_SLOTTYPE", slottype);
                objParams[5] = new SqlParameter("@P_STARTDATE", startdate);
                objParams[6] = new SqlParameter("@P_ENDDATE", enddate);
                objParams[7] = new SqlParameter("@P_COLLEGE_ID", college_id);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_FACULTY_BOTH_ATT_TT_DETAILS", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.AcdAttendanceController.LoadBothAttAndTTDetails-> " + ex.ToString());
            }
            finally
            {
                ds.Dispose();
            }
            return ds;
        }

        #endregion Canceled TimeTable/Attendance

        #region Room Allotment

        public DataSet GetSelectedDateTT(int sessionno, int degreeno, int branchno, int schemeno, int semesterno, int sectionno, int slottype, DateTime startdate, DateTime enddate, int clgID)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[10];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                objParams[2] = new SqlParameter("@P_BRANCHNO", branchno);
                objParams[3] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[4] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[5] = new SqlParameter("@P_SECTIONNO", sectionno);
                objParams[6] = new SqlParameter("@P_SLOTTYPE", slottype);
                objParams[7] = new SqlParameter("@P_START_DATE", startdate);
                objParams[8] = new SqlParameter("@P_END_DATE", enddate);
                objParams[9] = new SqlParameter("@P_COLLEGE_ID", clgID);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_SELECTED_DATE_TT", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AcdAttendanceController.GetSelectedDateTT-> " + ex.ToString());
            }
            return ds;
        }

        public int InsertRoomAllotmentTT(AcdAttendanceModel objc, int BranchNo, int batchno, int courseno, int ua_no, int slotno, int dayno, int slottype, int RoomNo, DateTime startdate, DateTime enddate)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[17];
                objParams[0] = new SqlParameter("@P_SESSIONNO", objc.Sessionno);
                objParams[1] = new SqlParameter("@P_DEGREENO", objc.DEGREENO);
                objParams[2] = new SqlParameter("@P_BRANCHNO", BranchNo);
                objParams[3] = new SqlParameter("@P_SCHEMENO", objc.SCHEMENO);
                objParams[4] = new SqlParameter("@P_SEMESTERNO", objc.SEMESTERNO);
                objParams[5] = new SqlParameter("@P_SECTIONNO", objc.Sectionno);
                objParams[6] = new SqlParameter("@P_BATCHNO", batchno);           //------------//
                objParams[7] = new SqlParameter("@P_COURSENO", courseno);
                objParams[8] = new SqlParameter("@P_UA_NO", ua_no);
                objParams[9] = new SqlParameter("@P_SLOTNO", slotno);
                objParams[10] = new SqlParameter("@P_DAYNO", dayno);        //------------//
                objParams[11] = new SqlParameter("@P_SLOTTYPE", slottype);
                objParams[12] = new SqlParameter("@P_START_DATE", startdate);
                objParams[13] = new SqlParameter("@P_END_DATE", enddate);
                objParams[14] = new SqlParameter("@P_COLLEGE_ID", objc.CollegeId);
                objParams[15] = new SqlParameter("@P_ROOMNO", RoomNo);
                objParams[16] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[16].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_ROOM_IN_TIMETABLE", objParams, true);

                if (ret != null && ret.ToString() == "1")
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                else if (ret != null && ret.ToString() == "2")
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                else if (ret != null && ret.ToString() == "3")
                    retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                else
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.AddLevel-> " + ex.ToString());
            }
            return retStatus;
        }

        #endregion Room Allotment

        #region Suspend Student Entry

        public DataSet GetStudentForSuspend(int clgId, int sessionno, int schemeno, int degreeno, int semester, int section, int batch, int courseno, DateTime startdate, DateTime enddate)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                SqlParameter[] objParams = new SqlParameter[10];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[2] = new SqlParameter("@P_DEGREENO", degreeno);
                objParams[3] = new SqlParameter("@P_SEMESTERNO", semester);
                objParams[4] = new SqlParameter("@P_SECTIONNO", section);
                objParams[5] = new SqlParameter("@P_BATCHNO", batch);
                objParams[6] = new SqlParameter("@P_COURSENO", courseno);
                objParams[7] = new SqlParameter("@P_STDATE", startdate);
                objParams[8] = new SqlParameter("@P_ENDDATE", enddate);
                objParams[9] = new SqlParameter("@P_COLLEGE_ID", clgId);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_STUDENT_FOR_SUSPEND_ENTRY", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AcdAttendanceController.GetAppliedStudentsForOD-> " + ex.ToString());
            }

            return ds;
        }

        public int AddSuspedStudentDetails(AcdAttendanceModel objAttModel, string idnos, string slotNos, int suspendType, int clgId)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;
                //Add
                objParams = new SqlParameter[9];

                objParams[0] = new SqlParameter("@P_SESSIONNO", objAttModel.Sessionno);
                objParams[1] = new SqlParameter("@P_IDNOS", idnos);
                objParams[2] = new SqlParameter("@P_SUSPEND_TYPE", suspendType);
                objParams[3] = new SqlParameter("@P_SUSPEND_DETAIL", objAttModel.Event_Detail);
                if (objAttModel.LeaveStartDate == DateTime.MinValue)
                    objParams[4] = new SqlParameter("@P_STDATE", DBNull.Value);
                else
                    objParams[4] = new SqlParameter("@P_STDATE", objAttModel.LeaveStartDate);

                objParams[5] = new SqlParameter("@P_ENDDATE", objAttModel.LeaveEndDate);
                objParams[6] = new SqlParameter("@P_SLOTNOS", slotNos);
                objParams[7] = new SqlParameter("@P_COLLEGE_ID", clgId);
                objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[8].Direction = ParameterDirection.Output;
                if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_INSERT_SUSPEND_DETAIL", objParams, true) != null)
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AcdAttendanceController.AddSuspedStudentDetails-> " + ex.ToString());
            }
            return retStatus;
        }

        public DataSet GetSelectedCouresAndDateSlots(int clgID, int sessionno, int degreeno, int branchno, int schemeno, int semesterno, int sectionno, DateTime date, int courseno) //string idnos,
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[9];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                objParams[2] = new SqlParameter("@P_BRANCHNO", branchno);
                objParams[3] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[4] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[5] = new SqlParameter("@P_SECTIONNO", sectionno);
                objParams[6] = new SqlParameter("@P_COURSENO", courseno);
                objParams[7] = new SqlParameter("@P_DATE", date);
                objParams[8] = new SqlParameter("@P_COLLEGE_ID", clgID);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_SELECTED_COURSE_AND_DATE_SLOT", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AcdAttendanceController.GetSelectedDateSlotsOfBulkStudents-> " + ex.ToString());
            }
            return ds;
        }

        #endregion Suspend Student Entry

        public DataSet GetAbsentStudentForEmailSMS(int attNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_ATT_NO", attNo);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_ABSENT_STUDENT_FOR_SMS_EMAIL", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AcdAttendanceController.GetAbsentStudentForEmailSMS-> " + ex.ToString());
            }

            return ds;
        }

        public DataSet DownloadBlankTeachingPlanSheet(int session, int degreeEx, int semester, int course, int subid, int ua_no, int degree, int clgID)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[8];
                objParams[0] = new SqlParameter("@P_SESSIONNO", Convert.ToInt32(session));
                objParams[1] = new SqlParameter("@P_DEGREENO_EX", Convert.ToInt32(degreeEx));
                objParams[2] = new SqlParameter("@P_SEMESTERNO", Convert.ToInt32(semester));
                objParams[3] = new SqlParameter("@P_COURSENO", Convert.ToInt32(course));
                objParams[4] = new SqlParameter("@P_SUBID", Convert.ToInt32(subid));
                objParams[5] = new SqlParameter("@P_UA_NO", Convert.ToInt32(ua_no));
                objParams[6] = new SqlParameter("@P_DEGREENO", Convert.ToInt32(degree));
                objParams[7] = new SqlParameter("@P_COLLEGE_ID", Convert.ToInt32(clgID));
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_TEACHINGPLAN_SHEET", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AcdAttendanceController.GetAbsentStudentForEmailSMS-> " + ex.ToString());
            }
            return ds;
        }

        #region Student Memo

        public DataSet GetStudentForMemo(int sessionno, int degreeno, int branchno, int schemeno, int semesterno, int sectionno, string Condition, int per, int clgID, int option)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[10];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                objParams[2] = new SqlParameter("@P_BRANCHNO", branchno);
                objParams[3] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[4] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[5] = new SqlParameter("@P_SECTIONNO", sectionno);
                objParams[6] = new SqlParameter("@P_CONDITIONS", Condition);
                objParams[7] = new SqlParameter("@P_PERCENTAGE", per);
                objParams[8] = new SqlParameter("@P_COLLEGE_ID", clgID);
                objParams[9] = new SqlParameter("@P_OPTION", option);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_STUDENT_FOR_MEMO", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AcdAttendanceController.GetSubjectDetails-> " + ex.ToString());
            }
            return ds;
        }

        public int AddStudentMemo(int idno, int session, int semester, decimal attCondPer, string subWisePer, decimal attPER, int clgID, int formatValue, string Operator)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[10];
                objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                objParams[1] = new SqlParameter("@P_SEMESTERNO", semester);
                objParams[2] = new SqlParameter("@P_IDNO", idno);
                objParams[3] = new SqlParameter("@P_ATT_COND", attCondPer);
                objParams[4] = new SqlParameter("@P_SubWisePer", subWisePer);
                objParams[5] = new SqlParameter("@P_ATT_PER", attPER);
                objParams[6] = new SqlParameter("@P_COLLEGE_ID", clgID);
                objParams[7] = new SqlParameter("@P_FormatValue", formatValue);
                objParams[8] = new SqlParameter("@P_Operator", Operator);
                objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[9].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_INSERT_STUDENT_MEMO", objParams, true);

                if (ret != null && ret.ToString() == "1")
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                //else if (ret != null && ret.ToString() == "2")
                //    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                else if (ret != null && ret.ToString() == "2")
                    retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                else
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AcdAttendanceController.AddStudentMemo-> " + ex.ToString());
            }
            return retStatus;
        }

        public DataSet GetAllStudentMemoDetails(int idno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_STUD_MEMO", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AcdAttendanceController.GetAllStudentMemoDetails-> " + ex.ToString());
            }

            return ds;
        }

        public int UploadStudMemo(int session, int semesterno, int idno, string fileName, int clgID)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SESSIONNO", session);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[3] = new SqlParameter("@P_FILE_NAME", fileName);
                objParams[4] = new SqlParameter("@P_COLLEGE_ID", clgID);
                objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[5].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_UPDATE_STUDENT_MEMO_FILE", objParams, true);

                if (ret != null && ret.ToString() == "1")
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AcdAttendanceController.AddStudentMemo-> " + ex.ToString());
            }
            return retStatus;
        }

        #endregion Student Memo

        #region Floor and Room Master

        public int Addfloor(string floor, int collegecode, int Active, int CreatedBy, string ipAddress)
        {
            int status = 0;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_FLOORNAME", floor);
                objParams[1] = new SqlParameter("@P_COLLEGE_CODE", collegecode);
                objParams[2] = new SqlParameter("@P_ACTIVESTATUS", Active);
                objParams[3] = new SqlParameter("@P_CREATED_BY", CreatedBy);
                objParams[4] = new SqlParameter("@P_IPADDRESS", ipAddress);
                objParams[5] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[5].Direction = ParameterDirection.Output;

                object obj = objSqlHelper.ExecuteNonQuerySP("PKG_SP_INS_ACD_FLOOR_MASTER_ATTENDANCE", objParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
                if (obj.Equals(2627))
                {
                    status = Convert.ToInt32(CustomStatus.RecordExist);
                }
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.AddClubData --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        public int Updatefloor(int floorno, string floor, int collegecode, int Active, int CreatedBy, string ipAddress)
        {
            int status = 0;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[7];
                objParams[0] = new SqlParameter("@P_FLOORNO", floorno);
                objParams[1] = new SqlParameter("@P_FLOORNAME", floor);
                objParams[2] = new SqlParameter("@P_COLLEGE_CODE", collegecode);
                objParams[3] = new SqlParameter("@P_ACTIVESTATUS", Active);
                objParams[4] = new SqlParameter("@P_CREATED_BY", CreatedBy);
                objParams[5] = new SqlParameter("@P_IPADDRESS", ipAddress);
                objParams[6] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[6].Direction = ParameterDirection.Output;

                object obj = objSqlHelper.ExecuteNonQuerySP("PKG_SP_UPDATE_ACD_FLOOR_MASTER_ATTENDANCE", objParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    status = Convert.ToInt32(CustomStatus.RecordUpdated);
                else
                    status = Convert.ToInt32(CustomStatus.Error);

                if (obj.Equals(2627))
                {
                    status = Convert.ToInt32(CustomStatus.RecordExist);
                }
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateClubData --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        public DataSet GetAllfloordata()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                ds = objSQLHelper.ExecuteDataSet("PKG_SP_GET_ACD_FLOOR_MASTER_ATTENDANCE");
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetAllclubdata-> " + ex.ToString());
            }
            return ds;
        }

        public int AddRoomintake(string room, int floor, int intake, int collegecode, int Activest)
        {
            int status = 0;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_ROOMNAME", room);
                objParams[1] = new SqlParameter("@P_FLOORNO", floor);
                objParams[2] = new SqlParameter("@P_INTAKE", intake);
                objParams[3] = new SqlParameter("@P_COLLEGE_CODE", collegecode);
                objParams[4] = new SqlParameter("@P_ACTIVESTATUS", Activest);
                //objParams[5] = new SqlParameter("@P_CREATED_BY", CreatedBy);
                //objParams[6] = new SqlParameter("@P_IPADDRESS", ipAddress);
                //objParams[7] = new SqlParameter("@P_ORGANIZATIONID", orgid);
                objParams[5] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[5].Direction = ParameterDirection.Output;

                object obj = objSqlHelper.ExecuteNonQuerySP("PKG_SP_INS_ACD_ROOM_MASTER_ATTENDANCE", objParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
                if (obj.Equals(2627))
                {
                    status = Convert.ToInt32(CustomStatus.RecordExist);
                }
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.AddClubData --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        public int UpdateRoomintake(int roomno, string room, int floor, int intake, int collegecode, int Activest)
        {
            int status = 0;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[7];
                objParams[0] = new SqlParameter("@P_ROOMNO", roomno);
                objParams[1] = new SqlParameter("@P_ROOMNAME", room);
                objParams[2] = new SqlParameter("@P_FLOORNO", floor);
                objParams[3] = new SqlParameter("@P_INTAKE", intake);
                objParams[4] = new SqlParameter("@P_COLLEGE_CODE", collegecode);
                objParams[5] = new SqlParameter("@P_ACTIVESTATUS", Activest);
                //objParams[6] = new SqlParameter("@P_CREATED_BY", CreatedBy);
                //objParams[7] = new SqlParameter("@P_IPADDRESS", ipAddress);
                //objParams[8] = new SqlParameter("@P_ORGANIZATIONID", orgid);
                objParams[6] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[6].Direction = ParameterDirection.Output;

                object obj = objSqlHelper.ExecuteNonQuerySP("PKG_SP_UPDATE_ACD_ROOM_MASTER_ATTENDANCE", objParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    status = Convert.ToInt32(CustomStatus.RecordUpdated);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
                if (obj.Equals(2627))
                {
                    status = Convert.ToInt32(CustomStatus.RecordExist);
                }
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateClubData --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        public DataSet GetAllRoomIntakedata()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                ds = objSQLHelper.ExecuteDataSet("PKG_SP_GET_ACD_ROOM_MASTER_ATTENDANCE");
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetAllclubdata-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetRoomInatkebyNo(int roomno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_ROOMNO", roomno);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_BY_NO_ACD_ROOM_MASTER_ATTENDANCE", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.studentController.GetClubActivityByNo->" + ex.ToString());
            }
            return ds;
        }

        #endregion Floor and Room Master

        public DataSet Get_College_Session(int Mode, string College_Nos)
        {
            DataSet ds = null;
            SQLHelper objDataAccessLayer = new SQLHelper(_nitprm_constr);

            try
            {
                SqlParameter[] objParams = new SqlParameter[]
                        {
                           new SqlParameter("@P_MODE",Mode),
                           new SqlParameter("@P_COLLEGE_IDNOS",College_Nos)
                        };

                ds = objDataAccessLayer.ExecuteDataSetSP("PKG_ACAD_GET_CONCAT_COLLEGE_SESSION_ATTENDANCE", objParams);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return ds;
        }

        public DataSet getselectedcollegewisecollegeid(string Collegenos)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_SESSIONNOS", Collegenos);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_SELECTED_SESSION_WISE_COLLEGE", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.SessionController.GetRegularCourseList-> " + ex.ToString());
            }
            return ds;
        }

        #region Attedance By Faculty

        public DataSet GetCollegeSessionForAttendance(int UA_NO)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_UA_NO", UA_NO);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_COLLEGE_SESSION_FOR_ATTENDANCE", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.GetCollegeSessionForAttendance-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetSubjectForAttendanceModified(int sessionno, int dayno, int uano, DateTime date, int TPlanYesNo, int schemeType, string College_ID, int istutorial)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[8];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_DAYNO", dayno);
                objParams[2] = new SqlParameter("@P_UA_NO", uano);
                objParams[3] = new SqlParameter("@P_ATTDATE", date);
                objParams[4] = new SqlParameter("@P_TPLAN_FLAG", TPlanYesNo);
                objParams[5] = new SqlParameter("@P_SCHEMETYPE", schemeType);
                objParams[6] = new SqlParameter("@P_COLLEGE_ID", College_ID);//Added BY Dileep on 10.04.2021
                objParams[7] = new SqlParameter("@P_ISTUTORIAL", istutorial);//Added by Dileep 10.02.2022
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_FACULTY_SUBJECT_FOR_ATTENDANCE_MODIFIED", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AcdAttendanceController.GetSubjectForAttendanceModified-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetSubjectDetails(int sessionno, int schemeno, int semno, int sectionno, int batchno, int courseno, int slotNo, int uano, DateTime date, int flag)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[10];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semno);
                objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                objParams[4] = new SqlParameter("@P_BATCHNO", batchno);
                objParams[5] = new SqlParameter("@P_COURSENO", courseno);
                objParams[6] = new SqlParameter("@P_SLOTNO", slotNo);
                objParams[7] = new SqlParameter("@P_UA_NO", uano);
                objParams[8] = new SqlParameter("@P_DATE", date);
                objParams[9] = new SqlParameter("@P_FLAG", flag);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_SUBJECT_DATAILS", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AcdAttendanceController.GetSubjectDetails-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetStudentFacultywiseAttendanceModified(int session, int uano, int courseno, DateTime date, int schemeno, int sem, int sectionno, int batchno, int slotno, int altCourseNo, string College_id)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[11];
                objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                objParams[1] = new SqlParameter("@P_UA_NO", uano);
                objParams[2] = new SqlParameter("@P_COURSENO", courseno);
                objParams[3] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[4] = new SqlParameter("@P_SEMESTERNO", sem);
                objParams[5] = new SqlParameter("@P_SECTIONNO", sectionno);
                objParams[6] = new SqlParameter("@P_BATCHNO", batchno);
                objParams[7] = new SqlParameter("@P_SLOTNO", slotno);
                objParams[8] = new SqlParameter("@P_ATT_DATE", date);
                objParams[9] = new SqlParameter("@P_AltCourseNo", altCourseNo);
                objParams[10] = new SqlParameter("@P_COLLEGE_ID", College_id);// added by dileep kare on 12.04.2021
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_STUDENT_FACULTYWISE_SUBJECT_MODIFIED", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AcdAttendanceController.GetStudentFacultywiseAttendanceModified-> " + ex.ToString());
            }
            return ds;
        }

        /// <summary>
        /// Add Attendance Faculty wise
        /// Used in Page : AttendanceEntry.aspx.cs
        /// </summary>
        public int AddAttendance(AcdAttendanceModel objc)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[25];
                objParams[0] = new SqlParameter("@P_SESSIONNO", objc.Sessionno);
                objParams[1] = new SqlParameter("@P_UANO", objc.UA_No);
                objParams[2] = new SqlParameter("@P_ATT_DATE", objc.Att_date);
                objParams[3] = new SqlParameter("@P_COURSENO", objc.CourseNo);
                objParams[4] = new SqlParameter("@P_CCODE", "");
                objParams[5] = new SqlParameter("@P_SCHEMENO", objc.SCHEMENO);
                objParams[6] = new SqlParameter("@P_BATCHNO", objc.BatchNo);
                objParams[7] = new SqlParameter("@P_STUDID", objc.StudID);
                objParams[8] = new SqlParameter("@P_ATTE_STATUS", objc.Att_status);
                objParams[9] = new SqlParameter("@P_CURDATE", objc.Curdate);
                objParams[10] = new SqlParameter("@P_PERIOD", objc.Period);
                objParams[11] = new SqlParameter("@P_CLASSTYPE", objc.ClassType);
                objParams[12] = new SqlParameter("@P_TOPIC_COVERED", objc.Topic_Covered);
                objParams[13] = new SqlParameter("@P_SECTIONNO", objc.Sectionno);
                objParams[14] = new SqlParameter("@P_SEMESTERNO", objc.SEMESTERNO);
                objParams[15] = new SqlParameter("@P_SUBID", 0);
                objParams[16] = new SqlParameter("@P_UNIT_NO", 0);
                objParams[17] = new SqlParameter("@P_STATUS", objc.LectStatus);
                objParams[18] = new SqlParameter("@P_TP_NO", objc.TpNo);
                objParams[19] = new SqlParameter("@P_ATTE_LTIME", objc.Att_LateTime);
                objParams[20] = new SqlParameter("@P_SLOTNO", objc.Slot);
                objParams[21] = new SqlParameter("@P_CONUM", objc.CoNo);//Course Object Number..
                objParams[22] = new SqlParameter("@P_COLLEGE_ID", objc.College_Id);//ADDED BY DILEEP ON 10.04.2021n
                objParams[23] = new SqlParameter("@P_ISTUTORIAL", objc.Tutorial);//added by Dileep K on 10.02.2022

                objParams[24] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[24].Direction = ParameterDirection.Output;

                if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_INSERT_ATTENDANCE_FACULTYWISE_SUBJECT", objParams, false) != null)
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.AddLevel-> " + ex.ToString());
            }

            return retStatus;
        }

        /// <summary>
        /// Added By - Swapnil Prachand for Global elective
        /// </summary>
        /// <param name="sessionno"></param>
        /// <param name="College_ID"></param>
        /// <param name="semesterno"></param>
        /// <param name="schemeno"></param>
        /// <param name="courseno"></param>
        /// <param name="uano"></param>
        /// <param name="istutorial"></param>
        /// <param name="sectionno"></param>
        /// <param name="batchno"></param>
        /// <param name="electtype"></param>
        /// <returns></returns>
        public DataSet GetFacultyWiseTopicCovered(int sessionno, int College_ID, int semesterno, int schemeno, int courseno, int uano, int istutorial, int sectionno, int batchno, int electtype)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[10];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_COLLEGE_ID", College_ID);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[3] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[4] = new SqlParameter("@P_COURSENO", courseno);
                objParams[5] = new SqlParameter("@P_UA_NO", uano);
                objParams[6] = new SqlParameter("@P_ISTUTORIAL", istutorial);
                objParams[7] = new SqlParameter("@P_SECTIONNO", sectionno);
                objParams[8] = new SqlParameter("@P_BATCHNO", batchno);

                objParams[9] = new SqlParameter("@P_ELECTTYPE", electtype);//Added BY Dileep on 10.04.2021

                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_FACULTYWISE_TOPICCOVERED", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AcdAttendanceController.GetFacultyWiseTopicCovered-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetRestrictedCoursesModified(int sessionno, int uano, int schemeType, string College_id, int istutorial)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_UA_NO", uano);
                objParams[2] = new SqlParameter("@P_SCHEMETYPE", schemeType);
                objParams[3] = new SqlParameter("@P_COLLEGE_ID", College_id);//Added By Dileep Kare on 14.04.2021
                objParams[4] = new SqlParameter("@P_ISTUTORIAL", istutorial);//Added By Dileep Kare on 10.02.2022
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_All_RESTRICTED_HOLIDAY_COURSES_MODIFIED", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AcdAttendanceController.GetAllLeave-> " + ex.ToString());
            }
            return ds;
        }

        /// <summary>
        /// Added By Nikhil on 08/02/2021
        /// </summary>
        /// <param name="sessionNo"></param>
        /// <param name="schemeNo"></param>
        /// <param name="courseNo"></param>
        /// <param name="uaNo"></param>
        /// <param name="subId"></param>
        /// <param name="sectionno"></param>
        /// <param name="fromdate"></param>
        /// <param name="todate"></param>
        /// <param name="Coursetype"></param>
        /// <param name="batchno"></param>
        /// <param name="ccode"></param>
        /// <returns></returns>
        public DataSet GetDayWiseData(int sessionNo, int schemeNo, int courseNo, int uaNo, int subId, int sectionno, DateTime fromdate, DateTime todate, int Coursetype, int batchno, string ccode)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                SqlParameter[] objParams = new SqlParameter[11];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
                objParams[1] = new SqlParameter("@P_SCHEMENO", schemeNo);
                objParams[2] = new SqlParameter("@P_COURSENO", courseNo);
                objParams[3] = new SqlParameter("@P_UA_NO", uaNo);
                objParams[4] = new SqlParameter("@P_SUBID", subId);
                objParams[5] = new SqlParameter("@P_SECTIONNO", sectionno);
                objParams[6] = new SqlParameter("@P_FROMDATE", fromdate);
                objParams[7] = new SqlParameter("@P_TODATE", todate);
                objParams[8] = new SqlParameter("@P_TH_PR", Coursetype);
                objParams[9] = new SqlParameter("@P_BATCHNO", batchno);
                objParams[10] = new SqlParameter("@P_CCODE", ccode);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_DAILY_STUDENT_ATTENDANCE_REPORT", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AcdAttendanceController.GetStudentAttendence-> " + ex.ToString());
            }

            return ds;
        }

        public DataSet GetAlternateAllottedCoursesModified(int sessionno, int uano, int schemeType, string College_id, int istutorial)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_UA_NO", uano);
                objParams[2] = new SqlParameter("@P_SCHEMETYPE", schemeType);
                objParams[3] = new SqlParameter("@P_COLLEGE_ID", College_id);//Added By Dileep Kare on 14.04.2021
                objParams[4] = new SqlParameter("@P_ISTUTORIAL", istutorial);//Added by Dileep 10.02.2022
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ALTERNATE_ALLOTTED_COURSES_MODIFIED", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AcdAttendanceController.GetAlternateAllottedCoursesModified-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet Get_Individual_Lock_Unlock(int sessionno, int uano)
        {
            DataSet ds = new DataSet();
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_UA_NO", uano);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_INDIVIDUAL_LOCKUNLOCK", objParams);
                return ds;
            }
            catch (Exception)
            {
                return ds;
            }
        }

        public DataSet GetAllHolidays(int sessionno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_ALL_HOLIDAYS", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AcdAttendanceController.GetAllHolidays-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetAllShiftTTCoursesModified(int sessionno, int uano, int schemeType, string College_ID, int istutorial)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_UA_NO", uano);
                objParams[2] = new SqlParameter("@P_SCHEMETYPE", schemeType);
                objParams[3] = new SqlParameter("@P_COLLEGE_ID", College_ID);//Added By Dileep Kare 12.04.2021
                objParams[4] = new SqlParameter("@P_ISTUTORIAL", istutorial);//Added by Dileep 10.02.2022
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ALL_SHIFTTT_COURSES_MODIFIED", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AcdAttendanceController.GetAllShiftTTCoursesModified-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetAllCoursesModified(int sessionno, int uano, int schemeType, string College_id, int istutorial)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_UA_NO", uano);
                objParams[2] = new SqlParameter("@P_SCHEMETYPE", schemeType);
                objParams[3] = new SqlParameter("@P_COLLEGE_ID", College_id);
                objParams[4] = new SqlParameter("@P_ISTUTORIAL", istutorial);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_All_COURSES_MODIFIED", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AcdAttendanceController.GetAllCoursesModified-> " + ex.ToString());
            }
            return ds;
        }

        #endregion Attedance By Faculty
    }
}