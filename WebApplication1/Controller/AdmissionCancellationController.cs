//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : ADMISSION CANCELLATION CONTROLLER
// CREATION DATE : 08-AUG-2009
// CREATED BY    : AMIT YADAV
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
    public class AdmissionCancellationController
    {
        private string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public DataSet SearchStudents(string searchText, string searchBy)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_SEARCHSTRING", searchText),
                    new SqlParameter("@P_SEARCH", searchBy)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_STUDENT_SP_SEARCH_STUDENT", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.AdmissionCancellationController.SearchStudents() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetClearanceInfo(int studentId)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_IDNO", studentId)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ADMCAN_GET_CLEARANCE_DATA", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.AdmissionCancellationController.GetClearanceInfo() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public bool CancelAdmission(int studentId, string remark)
        {
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_REMARK", remark),
                    new SqlParameter("@P_IDNO", studentId)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                object obj = objDataAccess.ExecuteNonQuerySP("PKG_ADMCAN_CANCEL_ADMISSION", sqlParams, true);

                if (obj != null && obj.ToString() != "-99")
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.AdmissionCancellationController.CancelAdmission() --> " + ex.Message + " " + ex.StackTrace);
                return false;
            }
        }

        public bool CancelAdmission(int studentId, string remark, string ipaddress, int uano)
        {
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_REMARK", remark),
                    new SqlParameter("@P_IPADDRESS", ipaddress),
                    new SqlParameter("@P_UANO", uano),
                    new SqlParameter("@P_IDNO", studentId)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                object obj = objDataAccess.ExecuteNonQuerySP("PKG_ADMCAN_CANCEL_ADMISSION", sqlParams, true);

                if (obj != null && obj.ToString() != "-99")
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.AdmissionCancellationController.CancelAdmission() --> " + ex.Message + " " + ex.StackTrace);
                return false;
            }
        }

        public DataSet ProspectusSearchStudents(string searchText, string searchBy)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                   new SqlParameter("@P_SEARCHSTRING", searchText),
                    new SqlParameter("@P_SEARCH", searchBy)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACAD_PROSPECTUS_SEARCH_STUDENT", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.AdmissionCancellationController.SearchStudents() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public bool CancelProspectus(int prosno)
        {
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_PROSNO", prosno)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                object obj = objDataAccess.ExecuteNonQuerySP("PKG_STUDENT_CANCEL_PROSPECTUS", sqlParams, true);

                if (obj != null && obj.ToString() != "-99")
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.AdmissionCancellationController.CancelAdmission() --> " + ex.Message + " " + ex.StackTrace);
                return false;
            }
        }

        public DataSet ProspectusSearchStudentsDatewise(string frmdate, string todate)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                   new SqlParameter("@P_FRMDATE", frmdate),
                    new SqlParameter("@P_TODATE", todate)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACAD_PROSPECTUS_SEARCH_STUDENT_DATEWISE", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ProspectusSearchStudentsDatewise.SearchStudents() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet DisplayTimeTableFacultyCourse(Int32 SESSIONNO, Int32 COURSENO, Int32 SECTIONNO, Int32 UANO, Int32 VERSION, string CCODE)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
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

        //FOR ADMISSION CANCEL STUDENT LIST EXCEL--[29-07-2016]
        public DataSet GetCancelledAdmissionStudentList(string START_DATE, string END_DATE, int DEGREENO, int BRANCHNO)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_START_DATE", START_DATE);
                objParams[1] = new SqlParameter("@P_END_DATE", END_DATE);
                objParams[2] = new SqlParameter("@P_DEGREENO", DEGREENO);
                objParams[3] = new SqlParameter("@P_BRANCHNO", BRANCHNO);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_ADMISSIONCANCEL_BRANCH_REPORT_IN_EXCEL", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AdmissionCancellationController.GetCancelledAdmissionStudentListList() -->" + ex.ToString());
            }
            return ds;
        }

        public bool CancelAdmission(int studentId, string remark, string ipaddress, int uano, int breakflag)
        {
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_REMARK", remark),
                    new SqlParameter("@P_IPADDRESS", ipaddress),
                    new SqlParameter("@P_UANO", uano),
                    new SqlParameter("@P_BREAKFLAG",breakflag),
                    new SqlParameter("@P_IDNO", studentId)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                object obj = objDataAccess.ExecuteNonQuerySP("PKG_ADMCAN_CANCEL_ADMISSION", sqlParams, true);

                if (obj != null && obj.ToString() != "-99")
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.AdmissionCancellationController.CancelAdmission() --> " + ex.Message + " " + ex.StackTrace);
                return false;
            }
        }

        public bool CancelAdmission(int studentId, string remark, string ipaddress, int uano, int breakflag, int month, int year)
        {
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_REMARK", remark),
                    new SqlParameter("@P_IPADDRESS", ipaddress),
                    new SqlParameter("@P_UANO", uano),
                    new SqlParameter("@P_BREAKFLAG",breakflag),
                    new SqlParameter("@P_MONTH",month),
                    new SqlParameter("@P_YEAR",year),
                    new SqlParameter("@P_IDNO", studentId)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                object obj = objDataAccess.ExecuteNonQuerySP("PKG_ADMCAN_CANCEL_ADMISSION", sqlParams, true);

                if (obj != null && obj.ToString() != "-99")
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.AdmissionCancellationController.CancelAdmission() --> " + ex.Message + " " + ex.StackTrace);
                return false;
            }
        }

        //FOR BREAK OF STUDENT LIST EXCEL--[06-06-2020] BY NARESH BEERLA

        public DataSet GetBreakofStudentList(string START_DATE, string END_DATE)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_START_DATE", START_DATE);
                objParams[1] = new SqlParameter("@P_END_DATE", END_DATE);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_ADMISSIONCANCEL_BRANCH_REPORT_BREAK_FLAG", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AdmissionCancellationController.GetBreakofStudentList() -->" + ex.ToString());
            }
            return ds;
        }

        //started by aashna 27-11-2021
        public DataSet GetCancelCourses(string ENROLLMENTNO)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_ENROLLMENTNO", ENROLLMENTNO);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_CANCEL_COURSE_LIST", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AdmissionCancellationController.GetCancelledAdmissionStudentListList() -->" + ex.ToString());
            }
            return ds;
        }

        public int InsertCancelStudent(Student_Acd objsc, StudentFees studfees, StudentInformation studinfo, StudentAddress studadd)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                        {
                             new SqlParameter("@P_ENROLLMENTNO", objsc.EnrollmentNo),
                             new SqlParameter("@P_REMARKS", objsc.Remarks),
                             new SqlParameter("@P_COLLEGEID", objsc.collegeid),
                             new SqlParameter("@P_DEGREENO", objsc.degreenoo),

                             new SqlParameter("@P_StudNameBank", objsc.StudName),
                             new SqlParameter("@P_BankName", studfees.BankName),
                             new SqlParameter("@P_AccountNo", studinfo.Acc_no),
                             new SqlParameter("@P_BranchCode", studinfo.Bank_branch),

                             new SqlParameter("@P_IfscCode", studinfo.Ifsc_code),
                             new SqlParameter("@P_StudAddress", studadd.PADDRESS),
                             new SqlParameter("@P_PinCode", studadd.PPINCODE),
                             new SqlParameter("@P_StateNo", studadd.PSTATE),

                             new SqlParameter("@P_CityNo", studadd.PCITY),
                             new SqlParameter("@P_StudMobNo", studadd.PMOBILE),
                             new SqlParameter("@P_Preference", objsc.DegreePrefs),
                             new SqlParameter("@P_Amount", objsc.Amounts),

                             new SqlParameter("@P_OrderID", objsc.OrdereIDs),
                             new SqlParameter("@P_TranDate",objsc.TranDates),
                             new SqlParameter("@P_StudIPAddress", objsc.StudIPAddress),
                             new SqlParameter("@P_chk",objsc.chk),

                             new SqlParameter("@P_OUT", SqlDbType.Int)
                        };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_CANCEL_COURSE_STUDENT", sqlParams, true);
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

        public int UpdateCancelStudentDCR(string degreeno, string collegeno, string OrdereIDs, string Enrollno)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                        {
                             new SqlParameter("@P_ENROLLMENTNO", Enrollno),
                             new SqlParameter("@P_COLLEGEID", collegeno),
                             new SqlParameter("@P_DEGREENO", degreeno),
                             new SqlParameter("@P_OrderID", OrdereIDs),
                             new SqlParameter("@P_OUT", SqlDbType.Int)
                        };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_CANCEL_COURSE_APPROVED_UPD_FLAG", sqlParams, true);
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

        public int UpdateCancelStudent(Student_Acd objsc)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                        {
                             new SqlParameter("@P_ENROLLMENTNO", objsc.EnrollmentNo),
                             new SqlParameter("@P_UANO", objsc.UA_No),
                             new SqlParameter("@P_REMARKS", objsc.Remarks),
                             new SqlParameter("@P_AdminIPAddress", objsc.AdminIPAddress),
                             new SqlParameter("@P_OUT", SqlDbType.Int)
                        };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_CANCEL_COURSE_APPROVED", sqlParams, true);
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

        //end by aashna 27-11-2021

        public bool RE_Admission_Cancel_Student(UserAcc objUA, string remark, string regno)
        {
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_REMARK", remark),
                    new SqlParameter("@P_IPADDRESS", objUA.IP_ADDRESS),
                    new SqlParameter("@P_UANO", objUA.USER_ID),
                    new SqlParameter("@P_REGNO", regno),
                    new SqlParameter("@P_IDNO", objUA.UA_IDNO)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                object obj = objDataAccess.ExecuteNonQuerySP("PKG_RE_ADMISSION_CANCEL_STUDENT", sqlParams, true);

                if (obj != null && obj.ToString() != "-99")
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.AdmissionCancellationController.CancelAdmission() --> " + ex.Message + " " + ex.StackTrace);
                return false;
            }
        }
    }
}