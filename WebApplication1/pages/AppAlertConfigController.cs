//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : APPLICATION ALERT CONFIG
// CREATION DATE : 27/02/2019
// CREATED BY    : IRFAN SHAIKH
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
    public class AppAlertConfigController
    {
        private string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
        private Common objCommon = new Common();

        public DataSet LoadAllAlerts()
        {
            DataSet ds = null;
            try
            {
                ds = objCommon.FillDropDown("ACD_APP_ALERT_CONFIG", "AlertsNo", "Alerts_Name,CASE WHEN ISNULL(Alert_Status,0)=0 THEN 'Inactive' ELSE 'Active' END Alert_Status", "", "");
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessController.AppAlertConfigController.LoadAllAlerts() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public SqlDataReader GetSingleAlert(int AlertNo)
        {
            SqlDataReader dr = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_ALERTNO", AlertNo);
                dr = objSQLHelper.ExecuteReaderSP("PKG_SP_RET_ALERT", objParams);
            }
            catch (Exception ex)
            {
                return dr;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AppAlertConfigController.GetSingleAlert-> " + ex.ToString());
            }
            return dr;
        }

        public int UpdateAlert(AppAlertConfig ObjAlert)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;

                //update
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_ALERTNO", ObjAlert.AlertsNo);
                objParams[1] = new SqlParameter("@P_ALERTNAME", ObjAlert.Alerts_Name);
                objParams[2] = new SqlParameter("@P_ALERT_STATUS", ObjAlert.Alert_Status);

                if (objSQLHelper.ExecuteNonQuerySP("PKG_SP_UPD_ALERT", objParams, false) != null)
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AppAlertConfigController.UpdateAlert-> " + ex.ToString());
            }

            return retStatus;
        }

        public int AddAlert(AppAlertConfig ObjAlert)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;
                //Add
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_ALERTNAME", ObjAlert.Alerts_Name);
                objParams[1] = new SqlParameter("@P_ALERT_STATUS", ObjAlert.Alert_Status);
                objParams[2] = new SqlParameter("@P_ALERTNO", SqlDbType.Int);
                objParams[2].Direction = ParameterDirection.Output;

                if (objSQLHelper.ExecuteNonQuerySP("PKG_SP_INS_ALERT", objParams, true) != null)
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