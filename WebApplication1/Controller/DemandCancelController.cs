using IITMS.SQLServer.SQLDAL;
using System;
using System.Data;
using System.Data.SqlClient;

namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{
    public class DemandCancelController
    {
        private string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public DataSet SearchStudents(string searchText, int session, string reciptcode)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);

                SqlParameter[] objParams = new SqlParameter[3];

                objParams[0] = new SqlParameter("@P_SEARCHTEXT", searchText);
                objParams[1] = new SqlParameter("@P_SESSION", session);
                objParams[2] = new SqlParameter("@P_RECIPTCODE", reciptcode);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_DEMAND_CANCEL_STUDENT", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.InstallmentController.GetStudentData-> " + ex.ToString());
            }

            return ds;
        }

        public bool CancelDemand(int idno, string str, DateTime dt, int uano, string rc, int session)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_REMARK", str);
                objParams[2] = new SqlParameter("@P_DATETIME", dt);
                objParams[3] = new SqlParameter("@P_UANO", uano);
                objParams[4] = new SqlParameter("@P_RC", rc);
                objParams[5] = new SqlParameter("@P_SESSION", session);
                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_CANCEL_DEMAND", objParams, true);
                if (obj != null && obj.ToString() != "-99")
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeeInstallment.AddStudentData-> " + ex.ToString());
            }
            return false;
        }
    }
}