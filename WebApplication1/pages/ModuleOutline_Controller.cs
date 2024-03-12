using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BusinessLogicLayer.BusinessLogic.Academic
{
    public class ModuleOutline_Controller
    {
        private string uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
        private int status = 0;

        public int AddPDPModuleOutline(acd_module_outline ObjMO, int MODULEOUTLINEID)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                SqlParameter[] objParams = null;

                //Add New User
                objParams = new SqlParameter[8];

                objParams[0] = new SqlParameter("@P_MODULEOUTLINEID", ObjMO.moduleoutlineid);
                objParams[1] = new SqlParameter("@P_DEPTNO", ObjMO.deptno);
                objParams[2] = new SqlParameter("@P_COURSENO", ObjMO.courseno);
                objParams[3] = new SqlParameter("@P_DOCUMENT_PATH", ObjMO.document_path);
                objParams[4] = new SqlParameter("@P_WITH_EFFECT_FROMDATE", ObjMO.with_effect_from);
                objParams[5] = new SqlParameter("@P_OUTLINE", ObjMO.outline);
                objParams[6] = new SqlParameter("@P_USERID", ObjMO.userid);
                //objParams[6] = new SqlParameter("@P_CREATED_BY",ObjMO.created_by);

                objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[7].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_UPDATE_MODULE_OUTLINE", objParams, true);//) != null)

                if (status != null && status.ToString() != "-99" && status.ToString() != "-1001")
                {
                    if (MODULEOUTLINEID == 0)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    else if (MODULEOUTLINEID > 0)
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
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ModuleOutline_Controller.AddPDPModuleOutline-> " + ex.ToString());
            }
            return retStatus;
        }

        public DataSet GetModuleOutline(int MODULEOUTLINEID)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_MODULEOUTLINEID", MODULEOUTLINEID);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_Module_Outline", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ModuleOutline_Controller.GetModuleOutline-> " + ex.ToString());
            }
            return ds;
        }
    }
}