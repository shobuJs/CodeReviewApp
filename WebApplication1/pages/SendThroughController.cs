//======================================================================================
// PROJECT NAME  : NITPRM
// MODULE NAME   : ACADEMIC
// PAGE NAME     : SEND THROUGH
// CREATION DATE : 04/03/2019
// CREATED BY    : PRITISH SHRIKHANDE
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================

using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.Academic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BusinessLogicLayer.BusinessLogic.Academic
{
    public class SendThroughController
    {
        private string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
        private Common objCommon = new Common();

        public DataSet LoadAllAlerts()
        {
            DataSet ds = null;
            try
            {
                ds = objCommon.FillDropDown("ACD_SENDTHROUGH", "Send_Through_ID", "Send_Through, CASE WHEN ISNULL(Status,0)=0 THEN 'Inactive' ELSE 'Active' END Status", "", "");
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessController.AppAlertConfigController.LoadAllAlerts() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public SqlDataReader GetSingleSendThrough(int sendthrough)
        {
            SqlDataReader dr = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_Send_Through", sendthrough);
                dr = objSQLHelper.ExecuteReaderSP("PKG_SP_SEND_THROUGH", objParams);
            }
            catch (Exception ex)
            {
                return dr;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AppAlertConfigController.GetSingleAlert-> " + ex.ToString());
            }
            return dr;
        }

        public int UpdateSendThrough(SendThrough ObjSend)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;

                //update
                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_Send_Through", ObjSend.SendThroughNo);
                objParams[1] = new SqlParameter("@P_SENDTHROUGHNAME", ObjSend.SendThrough_Name);
                objParams[2] = new SqlParameter("@P_SENDTHROUGH_STATUS", ObjSend.SendThrough_Status);
                objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[3].Direction = ParameterDirection.Output;

                if (objSQLHelper.ExecuteNonQuerySP("PKG_SP_UPD_SEND_THROUGH", objParams, false) != null)
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AppAlertConfigController.UpdateAlert-> " + ex.ToString());
            }

            return retStatus;
        }

        public int AddSendThrough(SendThrough ObjSendThrough)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;
                //Add
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_SENDTHROUGHNAME", ObjSendThrough.SendThrough_Name);
                objParams[1] = new SqlParameter("@P_SENDTHROUGH_STATUS", ObjSendThrough.SendThrough_Status);
                objParams[2] = new SqlParameter("@P_SENDTHROUGHNO", SqlDbType.Int);
                objParams[2].Direction = ParameterDirection.Output;

                if (objSQLHelper.ExecuteNonQuerySP("PKG_SP_INS_SENDTHROUGH", objParams, true) != null)
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AppAlertConfigController.AddAlert-> " + ex.ToString());
            }
            return retStatus;
        }
    }
}