using IITMS.SQLServer.SQLDAL;
using System;
using System.Data;
using System.Data.SqlClient;

namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{
    public class applicationReceivedController
    {
        private string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public DataSet GetExstStudentDetailsByApplicationID(string appid)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);

                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_APPLID", appid);
                ds = objSQLHelper.ExecuteDataSetSP("GET_STUDENT_EXIST_DETAILS_BY_APPLID", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.NewUserController.GetExstDetailsByRegNo -> " + ex.ToString());
            }
            return ds;
        }

        public int UpdateStudentApplicationStatus(int userno)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_USERNO", userno);
                objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_UPDATE_USER_APPLICATION_STATUS", objParams, true);

                if (Convert.ToInt16(ret) == 0)
                    retStatus = Convert.ToInt32(CustomStatus.RecordNotFound);
                else
                    retStatus = Convert.ToInt32((CustomStatus.RecordUpdated));
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32((CustomStatus.Error));
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.NewUserController.UpdateStudentApplicationStatus-> " + ex.ToString());
            }
            return retStatus;
        }

        //WHEN ALLOTMENT DONE ROUNDWISE THEN NO NEED OF BRANCHNO PARAMETER FOR THIS METHOS
        //IF ALLOTMENT DONE WITHOUT HAVING ALLOTES STATUS THEN BRANCHNO PARAMETER IS USED FOR THIS METHOD[20-JULY-2016]

        public int TransferStudentDataToMain(int userno, int PTYPE, int DEGREENO, int BRANCHNO)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_USERNO", userno);
                objParams[1] = new SqlParameter("@P_PTYPE", PTYPE);
                objParams[2] = new SqlParameter("@P_DEGREENO", DEGREENO);
                objParams[3] = new SqlParameter("@P_BRANCHNO", BRANCHNO);
                objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[4].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_TRANSFER_STUD_DATA", objParams, true);

                if (Convert.ToInt16(ret) == 0)
                    retStatus = Convert.ToInt32(CustomStatus.RecordNotFound);
                else
                    retStatus = Convert.ToInt32((CustomStatus.RecordSaved));
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32((CustomStatus.Error));
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.applicationReceivedController.TransferStudentDataToMain-> " + ex.ToString());
            }
            return retStatus;
        }

        public int UnlockStudentApplication(int userno)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_USERNO", userno);
                objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_UNLOCK_USER_APPLICATION", objParams, true);

                if (Convert.ToInt16(ret) == 0)
                    retStatus = Convert.ToInt32(CustomStatus.RecordNotFound);
                else
                    retStatus = Convert.ToInt32((CustomStatus.RecordUpdated));
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32((CustomStatus.Error));
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.NewUserController.UpdateStudentApplicationStatus-> " + ex.ToString());
            }
            return retStatus;
        }
    }
}