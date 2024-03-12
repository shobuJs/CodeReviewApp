//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : STUDENT FEEDBACK CONTROLLER
// CREATION DATE : 24-SEPT-2009
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
    public class NotificationController
    {
        private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        //  select Notification Details
        public DataSet GetNotificationDetails()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                ds = objSQLHelper.ExecuteDataSet("PKG_ACAD_NOTIFICATION");
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BranchController.GetAllBranchTypes-> " + ex.ToString());
            }
            return ds;
        }

        //-------- Notification Details IdNo
        public SqlDataReader GetNotificationDetailsByIDNO(int idno)
        {
            SqlDataReader dr = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_IDNO", idno);

                dr = objSQLHelper.ExecuteReaderSP("PKG_ACAD_NOTIFICATION_BY_ID", objParams);
            }
            catch (Exception ex)
            {
                return dr;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BranchController.GetAllBranchTypes-> " + ex.ToString());
            }
            return dr;
        }

        public int AddNotification(int idno, int title, string details, int uano, string ipaddress, DateTime expirtdate)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[7];
                objParams[0] = new SqlParameter("@P_NOTIFICATIONID", idno);
                objParams[1] = new SqlParameter("@P_TITLE", title);
                objParams[2] = new SqlParameter("@P_MESSAGE", details);
                objParams[3] = new SqlParameter("@P_FROMUSERID", uano);
                objParams[4] = new SqlParameter("@P_IP", ipaddress);
                objParams[5] = new SqlParameter("@P_EXPIRYDATE", expirtdate);
                objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[6].Direction = ParameterDirection.Output;
                object ret = (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_INS_NOTIFICATION_DETAILS", objParams, true));
                if (ret.ToString() == "1" && ret != null)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                else if (ret.ToString() == "2" && ret != null)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
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

        // -- Notification Details
        public int SubmitNotificationDetails(int NotificationID, int Degreeno, int Branchno, int Semesterno, int Deptno, int UA_Type, int UANO, int UserNo, int Title, string Details)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParam = null;
                objParam = new SqlParameter[11];
                objParam[0] = new SqlParameter("@P_NOTIFICATIONID", NotificationID);
                objParam[1] = new SqlParameter("@P_DEGREENO", Degreeno);
                objParam[2] = new SqlParameter("@P_BRANCHNO", Branchno);
                objParam[3] = new SqlParameter("@P_SEMESTERNO", Semesterno);
                objParam[4] = new SqlParameter("@P_DEPTNO", Deptno);
                objParam[5] = new SqlParameter("@P_UA_TYPE", UA_Type);
                objParam[6] = new SqlParameter("@P_FROMUSERID", UANO);
                objParam[7] = new SqlParameter("@P_UA_NO", UserNo);
                objParam[8] = new SqlParameter("@P_MESSAGE", Title);
                objParam[9] = new SqlParameter("@P_DETAILS", Details);
                objParam[10] = new SqlParameter("@P_STATUS", SqlDbType.Int);
                objParam[10].Direction = ParameterDirection.Output;
                //DataSet ds  = objSQLHelper.ExecuteDataSetSP("PKG_ANDROID_INS_NOTICE_TRANSACTION_DETAILS_MIS", objParam);
                object ret = (objSQLHelper.ExecuteNonQuerySP("PKG_ANDROID_INS_NOTICE_TRANSACTION_DETAILS_MIS_NEW", objParam, true));
                if (ret.ToString() == "1" && ret != null)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                else if (ret.ToString() == "2" && ret != null)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
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

        //  --  Get Notification  ---

        public DataSet GetNotification(string userNo, string part, string maxTransactionId)
        {
            try
            {
                DataSet ds;
                SQLHelper sqlHelper = new SQLHelper(connectionString);
                SqlParameter[] parameter = new SqlParameter[3];
                parameter[0] = new SqlParameter("@P_UA_ID", userNo);
                parameter[1] = new SqlParameter("@P_PART", part == null ? "0" : part);
                parameter[2] = new SqlParameter("@P_MAX_NOTICETRANSACTIONID", maxTransactionId == null ? "0" : maxTransactionId);
                ds = sqlHelper.ExecuteDataSetSP("PKG_ANDROID_V2_GET_NOTICES_MIS", parameter);

                return ds;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}