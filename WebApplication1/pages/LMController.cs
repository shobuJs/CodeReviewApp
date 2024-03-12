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
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using System;
using System.Data;
using System.Data.SqlClient;

namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{
    public class LMController
    {
        private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public int AddSourceType(LeadManage objenq)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_SOURCETYPENAME", objenq.SOURCETYPENAME);
                objParams[1] = new SqlParameter("@P_SOURCETYPESTATUS", objenq.SOURCETYPESTATUS);
                objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[2].Direction = ParameterDirection.Output;
                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_LEAD_SOURCE_INSERT", objParams, true);
                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LMController.AddSourceType-> " + ex.ToString());
            }
            return status;
        }

        public int UpdateSourceType(LeadManage objenq)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                //update
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_SOURCETYPENO", objenq.SOURCETYPENO);
                objParams[1] = new SqlParameter("@P_SOURCETYPENAME", objenq.SOURCETYPENAME);
                objParams[2] = new SqlParameter("@P_SOURCETYPESTATUS", objenq.SOURCETYPESTATUS);
                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_LEAD_SOURCE_UPDATE", objParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                else
                    retStatus = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LMController.UpdateSourceType-> " + ex.ToString());
            }

            return retStatus;
        }

        public DataSet GetAllSourceType()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[0];

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_LEAD_GET_ALL_SOURCE", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LMController.GetAllSourceType() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public SqlDataReader GetSingleSourceType(int SourceTypeno)
        {
            SqlDataReader dr = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[] { new SqlParameter("@P_SOURCETYPENO", SourceTypeno) };

                dr = objSQLHelper.ExecuteReaderSP("PKG_ACAD_LEAD_GET_SOURCE_BY_NO", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LMController.GetSingleSourceType() --> " + ex.Message + " " + ex.StackTrace);
            }
            return dr;
        }
    }
}