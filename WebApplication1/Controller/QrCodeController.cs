//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : BATCH MASTER CONTROLLER
// CREATION DATE : 02-SEPT-2009
// CREATED BY    : SANJAY RATNAPARKHI
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================

using IITMS.SQLServer.SQLDAL;
using System;
using System.Data;
using System.Data.SqlClient;

namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{
    public class QrCodeController
    {
        private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public DataSet GetStudentDataForGradeCard(int admbatch, int sessionNO, int collegeid, int degreeNo, int branchNo, int semesterNo, string declareDate, string dateOfIssue, int studentId)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_ADMBATCHNO", admbatch),
                    new SqlParameter("@P_SESSIONNO", sessionNO),
                    new SqlParameter("@P_COLLEGEID", collegeid),
                    new SqlParameter("@P_DEGREENO", degreeNo),
                    new SqlParameter("@P_BRANCHNO", branchNo),
                    new SqlParameter("@P_SEMESTERNO", semesterNo),
                    new SqlParameter("@P_RESULTDECLAREDDATE", declareDate),
                    new SqlParameter("@P_DATE_OF_ISSUE", dateOfIssue),
                    new SqlParameter("@P_IDNO", studentId)
                };

                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GRADE_CARD_REPORT_UG", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.QrCodeController.GetStudentResultData() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetStudentDataForGradeCardPG(int admbatch, int sessionNO, int collegeid, int degreeNo, int branchNo, int semesterNo, string declaredDate, string dateOfIssue, int studentId)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_ADMBATCHNO", admbatch),
                    new SqlParameter("@P_SESSIONNO", sessionNO),
                    new SqlParameter("@P_COLLEGEID", collegeid),
                    new SqlParameter("@P_DEGREENO", degreeNo),
                    new SqlParameter("@P_BRANCHNO", branchNo),
                    new SqlParameter("@P_SEMESTERNO", semesterNo),
                    new SqlParameter("@P_RESULTDECLAREDDATE",declaredDate),
                    new SqlParameter("@P_DATE_OF_ISSUE", dateOfIssue),
                    new SqlParameter("@P_IDNO", studentId)
                };

                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GRADE_CARD_REPORT_PG", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.QrCodeController.GetStudentResultData() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetStudentDataForGradeCardPC(int admbatch, int sessionNO, int collegeid, int degreeNo, int branchNo, int semesterNo, string declaredDate, string dateOfIssue, int studentId)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_ADMBATCHNO", admbatch),
                    new SqlParameter("@P_SESSIONNO", sessionNO),
                    new SqlParameter("@P_COLLEGEID", collegeid),
                    new SqlParameter("@P_DEGREENO", degreeNo),
                    new SqlParameter("@P_BRANCHNO", branchNo),
                    new SqlParameter("@P_SEMESTERNO", semesterNo),
                    new SqlParameter("@P_RESULTDECLAREDDATE",declaredDate),
                    new SqlParameter("@P_DATE_OF_ISSUE", dateOfIssue),
                    new SqlParameter("@P_IDNO", studentId)
                };

                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GRADE_CARD_REPORT_PC", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.QrCodeController.GetStudentResultData() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetStudentDataForConvocation(int studentId, int DegreeNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_IDNO", studentId),
                    new SqlParameter("@P_DEGREENO", DegreeNo),
                    new SqlParameter("@P_CERTNO", 5)
                };

                ds = objDataAccess.ExecuteDataSetSP("PKG_ACAD_CONVOCATION_CERTIFICATE_REPORT", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.QrCodeController.GetStudentResultData() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetStudentResultData(int studentId)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_IDNO", studentId)
                };

                ds = objDataAccess.ExecuteDataSetSP("PKG_TRANSCRIPT_SUB", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.QrCodeController.GetStudentResultData() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public int AddUpdateQrCode(int idnos, byte[] QR_IMAGE)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_IDNOS", idnos);
                objParams[1] = new SqlParameter("@P_QR_IMAGE", QR_IMAGE);
                objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[2].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_UPD_STUDENT_QRCODE", objParams, true);
                retStatus = Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.QrCodeController.AddUpdateQrCode -> " + ex.ToString());
            }
            return retStatus;
        }

        public DataSet GetDetailsForAdmitCard(int sessionno, int semesterNo, int branchNo, int DEGREENO, int prev_status, int studentId)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_SESSIONNO", sessionno),
                    new SqlParameter("@P_SEMESTERNO ", semesterNo),
                    new SqlParameter("@P_BRANCHNO", branchNo),
                    new SqlParameter("@P_DEGREENO", DEGREENO),
                    new SqlParameter("@P_PREV_STATUS", prev_status),
                    new SqlParameter("@P_IDNO", studentId),
                };

                ds = objDataAccess.ExecuteDataSetSP("PKG_REGIST_SP_REPORT_BULK_EXAM_REGISTSLIP", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.QrCodeController.GetStudentResultData() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetStudentDataForGradeCardPG_Details(int sessionNO, int collegeid, int degreeNo, int branchNo, int semesterNo, string declaredDate, string dateOfIssue, string studentId)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_COLLEGEID", collegeid),
                    new SqlParameter("@P_SESSIONNO", sessionNO),
                    new SqlParameter("@P_DEGREENO", degreeNo),
                    new SqlParameter("@P_BRANCHNO", branchNo),
                    new SqlParameter("@P_SEMESTERNO", semesterNo),
                    new SqlParameter("@P_RESULTDECLAREDDATE",declaredDate),
                    new SqlParameter("@P_DATE_OF_ISSUE", dateOfIssue),
                    new SqlParameter("@P_IDNO", studentId)
                };

                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GRADE_CARD_REPORT_PG_DETAILS", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.QrCodeController.GetStudentResultData() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
    }
}