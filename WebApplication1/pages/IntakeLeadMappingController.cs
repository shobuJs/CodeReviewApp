using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System;
using System.Data;
using System.Data.SqlClient;

namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{
    public class IntakeLeadMappingController
    {
        private string uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
        private int status = 0;

        public int AddIntakeLeadMapping(IntakeLeadMapping objILM, int mappingid)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                SqlParameter[] objParams = null;

                //Add New User
                objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_MAPPING_ID", objILM.mappingid);
                objParams[1] = new SqlParameter("@P_INTAKENO", objILM.intakeno);
                objParams[2] = new SqlParameter("@P_UA_SECTION", objILM.ua_section);
                objParams[3] = new SqlParameter("@P_AREA_INT_NO", objILM.area_int_no);
                objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[4].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_UPDATE_INTAKE_LEAD_MAPPING", objParams, true);//) != null)
                if (status != null && status.ToString() != "-99" && status.ToString() != "-1001")
                {
                    if (mappingid == 0)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    else if (mappingid > 0)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                }
                else
                {
                    retStatus = Convert.ToInt32(CustomStatus.Error);
                }
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.IntakeLeadMappingController.AddIntakeLeadMapping-> " + ex.ToString());
            }
            return retStatus;
        }

        public DataSet GetIntakeLeadMapping(int MAPPINGID)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_MAPPING_ID", MAPPINGID);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_INTAKE_LEAD_MAPPING", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.IntakeLeaadMappingController.GetIntakeLeadMapping-> " + ex.ToString());
            }
            return ds;
        }
    }
}