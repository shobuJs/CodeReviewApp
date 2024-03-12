using BusinessLogicLayer.BusinessEntities.Academic;
using IITMS;
using IITMS.SQLServer.SQLDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.BusinessLogic.Academic
{
    public class CommunicationTrController
    {
        private string connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public DataSet CommuTriggerOpt(CommunicationTrEntity entTr)
        {
            DataSet ds = new DataSet();
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] objPar = null;
                {
                    objPar = new SqlParameter[13];
                    objPar[0] = new SqlParameter("@P_COMMAND_TYPE", entTr.CommandType);
                    objPar[1] = new SqlParameter("@P_EASID", entTr.easID);
                    objPar[2] = new SqlParameter("@P_EASAID", entTr.objEvent);
                    objPar[3] = new SqlParameter("@P_START_DATE", entTr.startDate);
                    objPar[4] = new SqlParameter("@P_ENDDATE", entTr.endDate);
                    objPar[5] = new SqlParameter("@P_SCHEDULE_TIME", entTr.scheTime);
                    objPar[6] = new SqlParameter("@P_FETCH_DYNAMICALLY", entTr.fetchDynaStatus);
                    objPar[7] = new SqlParameter("@P_EMAIL_TO", entTr.Tomail);
                    objPar[8] = new SqlParameter("@P_EMAIL_CC", entTr.CCmail);
                    objPar[9] = new SqlParameter("@P_EMAIL_BCC", entTr.BCCmail);
                    objPar[10] = new SqlParameter("@P_EYE_EMAIL", entTr.eyeEmailID);
                    objPar[11] = new SqlParameter("@P_ACTIVE_STATUS", entTr.status);
                    objPar[12] = new SqlParameter("@P_USERNO", entTr.userNo);

                };

                ds = objSQLHelper.ExecuteDataSetSP("PR_EMAIL_AUTO_SCHEDULAR", objPar);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547) // Foreign key violation error number
                {
                }
                else
                {
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CommunicationTrController.CommuTriggerOpt() --> " + ex.Message + " " + ex.StackTrace);
                }

            }               
            return ds;
        }
    }
}