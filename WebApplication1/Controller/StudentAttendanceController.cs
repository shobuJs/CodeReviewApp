//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : ATTENDANCE CONTROLLER
// CREATION DATE : 10-OCT-2009
// CREATED BY    : SANJAY RATNAPARKHI
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================

using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System;
using System.Data;
using System.Data.SqlClient;

namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{
    public class StudentAttendanceController
    {
        private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        //CHANGED BY MANISH ON 02/02/2017
        public int AddAttendance(StudentAttendance objAttendance, int batch)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[25];
                objParams[0] = new SqlParameter("@P_SESSIONNO", objAttendance.SessionNo);
                objParams[1] = new SqlParameter("@P_UA_NO", objAttendance.UaNo);
                objParams[2] = new SqlParameter("@P_SCHEMENO", objAttendance.SchemeNo);
                objParams[3] = new SqlParameter("@P_SEMESTERNO", objAttendance.SemesterNo);
                objParams[4] = new SqlParameter("@P_COURSENO", objAttendance.CourseNo);
                objParams[5] = new SqlParameter("@P_CCODE", objAttendance.CCode);
                objParams[6] = new SqlParameter("@P_BATCHNO", batch);
                objParams[7] = new SqlParameter("@P_SUBID", objAttendance.SubId);
                //objParams[8] = new SqlParameter("@P_BATCHNOS", batches);
                objParams[8] = new SqlParameter("@P_STUDIDS", objAttendance.StudIds);
                objParams[9] = new SqlParameter("@P_ATT_STATUS", objAttendance.AttStatus);
                objParams[10] = new SqlParameter("@P_ATT_DATE", objAttendance.AttDate);
                objParams[11] = new SqlParameter("@P_PERIOD", objAttendance.Period);
                objParams[12] = new SqlParameter("@P_HOURS", objAttendance.Hours);
                objParams[13] = new SqlParameter("@P_CLASS_TYPE", objAttendance.ClassType);
                objParams[14] = new SqlParameter("@P_CURRENT_DATE", objAttendance.CurDate);
                objParams[15] = new SqlParameter("@P_COLLEGE_CODE", objAttendance.CollegeCode);
                objParams[16] = new SqlParameter("@P_SECTIONNO", objAttendance.Sectionno);
                objParams[17] = new SqlParameter("@P_THPR", objAttendance.Th_Pr);
                objParams[18] = new SqlParameter("@P_TOPIC_COVERED", objAttendance.Topic);
                objParams[19] = new SqlParameter("@P_AGUA_NO", objAttendance.AguaNo);
                objParams[20] = new SqlParameter("@P_AGSECTIONNO", objAttendance.AgsectionNo);
                objParams[21] = new SqlParameter("@P_AGCOURSENO", objAttendance.AgCourseNo);
                objParams[22] = new SqlParameter("@P_SWAPSTATUS", objAttendance.Swap_status);
                objParams[23] = new SqlParameter("@P_ADDITIONAL_SLOT", objAttendance.Additional_Slot);
                objParams[24] = new SqlParameter("@P_ATT_NO", objAttendance.AttNo);
                objParams[24].Direction = ParameterDirection.InputOutput;
                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_STUDENT_ATTENDANCE_INSERT", objParams, true);
                if (obj != null && obj.ToString().Equals("-1001"))
                {
                    retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
                }
                else
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentAttendanceController.AddAttendance-> " + ex.ToString());
            }

            return retStatus;
        }

        public DataSet GetAttendencedatabacknodays()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);

                SqlParameter[] objParams = new SqlParameter[0];

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_NOOFBACKDAYSATT", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentAttendanceController.GetStudentAttendence-> " + ex.ToString());
            }

            return ds;
        }

        public int Attendancebacklockins(int degreeno, int backdays, int alertemail)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_DEGREENO", degreeno);
                objParams[1] = new SqlParameter("@P_BACKDAYS", backdays);
                objParams[2] = new SqlParameter("@P_ALERTEMAIL", alertemail);
                objParams[3] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[3].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_INS_NOOFBACKDAYSATT", objParams, true);
                status = Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentAttendanceController.UpdateAttendance-> " + ex.ToString());
            }

            return status;
        }

        public void SENDMSG(string MSG, string MOBILENO)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_MSG", MSG);
                objParams[1] = new SqlParameter("@P_MOBILENO", MOBILENO);
                objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_SENDSMS_OF_ATTENDENCEANDMARKS", objParams, true);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentAttendanceController.AddAttendance-> " + ex.ToString());
            }
        }

        public DataSet GetStudentAttendence(int sessionNo, int courseNo, int uaNo, int subId, int thpr, int sectionNo, int batchNos, string attdate, int orderby) // Changed by Manish on 14/01/2017
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);

                SqlParameter[] objParams = new SqlParameter[9];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
                objParams[1] = new SqlParameter("@P_COURSENO", courseNo);
                objParams[2] = new SqlParameter("@P_UA_NO", uaNo);
                objParams[3] = new SqlParameter("@P_SUBID", subId);
                objParams[4] = new SqlParameter("@P_THPR", thpr);
                objParams[5] = new SqlParameter("@P_SECTIONNO", sectionNo);
                objParams[6] = new SqlParameter("@P_BATCHNOS", batchNos);
                objParams[7] = new SqlParameter("@P_ATTDATE", attdate);
                objParams[8] = new SqlParameter("@P_ORDERBY", orderby);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_SP_ATTENDANCE_BY_FACULTY", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentAttendanceController.GetStudentAttendence-> " + ex.ToString());
            }

            return ds;
        }

        public DataSet GetAttendenceByDate(int sessionNo, int uaNo, string attDate, int subId, int thpr, string courseNo, int classType, int period, int batchNo, int sectionno, int orderby)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);

                SqlParameter[] objParams = new SqlParameter[11];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
                objParams[1] = new SqlParameter("@P_UA_NO", uaNo);
                objParams[2] = new SqlParameter("@P_ATTDATE", attDate);
                objParams[3] = new SqlParameter("@P_SUBID", subId);
                objParams[4] = new SqlParameter("@P_THPR", thpr);
                objParams[5] = new SqlParameter("@P_COURSENO", courseNo);
                objParams[6] = new SqlParameter("@P_CLASSTYPE", classType);
                objParams[7] = new SqlParameter("@P_PERIOD", period);
                objParams[8] = new SqlParameter("@P_BATCHNO", batchNo);
                objParams[9] = new SqlParameter("@P_SECTIONNO", sectionno);
                objParams[10] = new SqlParameter("@P_ORDERBY", orderby);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_STUDENT_ATTENDANCE_GET_DATE", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentAttendanceController.GetStudentAttendence-> " + ex.ToString());
            }

            return ds;
        }

        public int UpdateAttendance(StudentAttendance objAttendance)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_STUDIDS", objAttendance.StudIds);
                objParams[1] = new SqlParameter("@P_ATT_STATUS", objAttendance.AttStatus);
                objParams[2] = new SqlParameter("@P_ATT_NO", objAttendance.AttNo);
                // objParams[2].Direction = ParameterDirection.InputOutput;

                if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_STUDENT_ATTENDANCE_UPDATE", objParams, false) != null)
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentAttendanceController.UpdateAttendance-> " + ex.ToString());
            }

            return retStatus;
        }

        public DataSet GetDuplicateAttendence(int sessionNo, int uaNo, int courseNo, int sectionNo, int subId, int thpr, string attDate, int classtype, int period, int batchno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);

                SqlParameter[] objParams = new SqlParameter[10];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
                objParams[1] = new SqlParameter("@P_UA_NO", uaNo);
                objParams[2] = new SqlParameter("@P_COURSENO", courseNo);
                objParams[3] = new SqlParameter("@P_SECTIONNO", sectionNo);
                objParams[4] = new SqlParameter("@P_SUBID", subId);
                objParams[5] = new SqlParameter("@P_THPR", thpr);
                objParams[6] = new SqlParameter("@P_ATTDATE", attDate);
                objParams[7] = new SqlParameter("@P_CLASSTYPE", classtype);
                objParams[8] = new SqlParameter("@P_PERIOD", period);
                objParams[9] = new SqlParameter("@P_BATCH", batchno);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_STUDENT_GET_DUPLICATE_ATTENDANCE", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentAttendanceController.GetDuplicateAttendence-> " + ex.ToString());
            }

            return ds;
        }

        public DataSet GetSelectedCautionMoneyFeeItem(DailyFeeCollectionRpt dcrReport, string[] feeHead)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSelectedFeeItem = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
            {
                new SqlParameter("@P_RECIEPT_CODE",dcrReport.ReceiptTypes),
                new SqlParameter("@P_FROM_DATE",(dcrReport.FromDate != DateTime.MinValue ? dcrReport.FromDate : DBNull.Value as object)),
                new SqlParameter("@P_TO_DATE",(dcrReport.ToDate != DateTime.MinValue ? dcrReport.ToDate : DBNull.Value as object)),
                new SqlParameter("@P_SEMESTERNO",dcrReport.SemesterNo),
                new SqlParameter("@P_BRANCHNO",dcrReport.BranchNo),
                new SqlParameter("@P_DEGREENO",dcrReport.DegreeNo),
                new SqlParameter("@P_YEAR",dcrReport.YearNo),
                new SqlParameter("@P_FEEHEAD1",feeHead[0]),
                //new SqlParameter("@P_FEEHEAD2",feeHead[1]),
                //new SqlParameter("@P_FEEHEAD3",feeHead[2]),
                //new SqlParameter("@P_FEEHEAD4",feeHead[3]),
                //new SqlParameter("@P_FEEHEAD5",feeHead[4])
            };
                ds = objSelectedFeeItem.ExecuteDataSetSP("PKG_FEECOLLECT_REPORT_CAUTION_MONEY_DEPOSIT", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentAttendanceController.GetSelectedCautionMoneyFeeItem-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetDayWiseData(int sessionNo, int schemeNo, int courseNo, int uaNo, int subId, int sectionno, DateTime fromdate, DateTime todate, int Coursetype, int batchno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);

                SqlParameter[] objParams = new SqlParameter[10];
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

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_REPORT_STU_ATTENDANCE_DAILY17112011NEW", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentAttendanceController.GetStudentAttendence-> " + ex.ToString());
            }

            return ds;
        }

        //MODIFIED ON 5_10_16
        public DataSet GetPeriod(int sessionNo, int courseNo, int uaNo, int subId, int thpr, int slot, int section, int Scheme)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);

                SqlParameter[] objParams = new SqlParameter[8];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
                objParams[1] = new SqlParameter("@P_COURSENO", courseNo);
                objParams[2] = new SqlParameter("@P_UA_NO", uaNo);
                objParams[3] = new SqlParameter("@P_SUBID", subId);
                objParams[4] = new SqlParameter("@P_THPR", thpr);
                objParams[5] = new SqlParameter("@P_SLOT", slot);
                objParams[6] = new SqlParameter("@P_SECTIONNO", section);
                objParams[7] = new SqlParameter("@P_SCHEMENO", Scheme);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ATTENDANCE_GET_PEROID_BY_DAY", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentAttendanceController.GetStudentAttendence-> " + ex.ToString());
            }

            return ds;
        }

        //added by reena on 5_8_16
        public DataSet GetPeriodExtra(int sessionNo, int courseNo, int uaNo, int subId, int section, int Schemeno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);

                SqlParameter[] objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
                objParams[1] = new SqlParameter("@P_COURSENO", courseNo);
                objParams[2] = new SqlParameter("@P_UA_NO", uaNo);
                objParams[3] = new SqlParameter("@P_SUBID", subId);
                // objParams[4] = new SqlParameter("@P_SLOT", slot);
                objParams[4] = new SqlParameter("@P_SECTIONNO", section);
                objParams[5] = new SqlParameter("@P_SCHEMENO ", Schemeno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ATTENDANCE_GET_PEROID_EXTRA_ALTERNATE", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentAttendanceController.GetStudentAttendence-> " + ex.ToString());
            }

            return ds;
        }

        // added by manish 20/02/17
        public DataSet GetFacultiesBySlot(int Sessionno, string Date, int Slot, int uano, int Sectionno, int subid, int thpr)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);

                SqlParameter[] objParams = new SqlParameter[7];
                objParams[0] = new SqlParameter("@P_SESSIONNO", Sessionno);
                objParams[1] = new SqlParameter("@P_DATE", Date);
                objParams[2] = new SqlParameter("@P_SLOT", Slot);
                objParams[3] = new SqlParameter("@P_UA_NO", uano);
                objParams[4] = new SqlParameter("@P_SUBID", subid);
                objParams[5] = new SqlParameter("@P_TH_PR", thpr);
                objParams[6] = new SqlParameter("@P_SECTIONNO", Sectionno);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_FACULTY_BY_SLOT", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentAttendanceController.GetStudentAttendence-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetAttendenceByDate_Report(int sessionNo, int uaNo, int subId, int thpr, string courseNo, int batchNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);

                SqlParameter[] objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
                objParams[1] = new SqlParameter("@P_UANO", uaNo);
                objParams[2] = new SqlParameter("@P_SUBID", subId);
                objParams[3] = new SqlParameter("@P_TH_PR", thpr);
                objParams[4] = new SqlParameter("@P_COURSENO", courseNo);
                objParams[5] = new SqlParameter("@P_BATCHNO", batchNo);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_ATTENDANCE_DETAILS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentAttendanceController.GetStudentAttendence-> " + ex.ToString());
            }

            return ds;
        }
    }
}