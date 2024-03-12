using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System;
using System.Data;
using System.Data.SqlClient;

namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{
    public class PBIConfigurationController
    {
        private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public int InsertWorkspaceData(PBIConfigurationEntity objPCE)
        {
            int status = 0;

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@WORKSPACE_NAME",objPCE.workspace_name),
                    new SqlParameter("@STATUS", objPCE.status),
                    new SqlParameter("@OUTPUT", SqlDbType.Int),
                              };

                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_PBI_CONFIGURATION_WORKSPACE_MASTER", sqlParams, true);

                if (obj != null && obj.ToString() != "1" && obj.ToString() != "2627")
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    status = Convert.ToInt32(CustomStatus.RecordExist);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PBIConfigurationController.AddPBIcinfiguration() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        public DataSet GetConfigurationList()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[0];

                ds = objSqlHelper.ExecuteDataSetSP("PKG_ACD_SELECT_CONFIGURATION_WORKSPACE_MASTER", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PBIConfigurationController.GetConfigurationList --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet EditConfiguration(int WORKSPACE_ID)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@WORKSPACE_ID", WORKSPACE_ID);
                ds = objSqlHelper.ExecuteDataSetSP("PKG_ACD_EDIT_CONFIGURATION_WORKSPACE_MASTER", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EditConfiguration-> " + ex.ToString());
            }
            return ds;
        }

        public int UpdatePBIConfiguration(PBIConfigurationEntity objPCE)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[]
                {
                    new SqlParameter("@WORKSPACE_ID", objPCE.Workspace_id),
                    new SqlParameter("@WORKSPACE_NAME", objPCE.workspace_name),
                    new SqlParameter("@STATUS", objPCE.status),
                };

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_CONFIGURATION_WORKSPACE_MASTER", objParams, false);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.UpdateBasicDetailsData-> " + ex.ToString());
            }
            return retStatus;
        }

        public int InsertSubWorkspaceData(PBIConfigurationEntity objPCE)
        {
            int status = 0;

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@SUB_WORKSPACE_NAME",objPCE.sub_workspace_name),
                      new SqlParameter("@WORKSPACE_ID",objPCE.Workspace_id),
                    new SqlParameter("@STATUS", objPCE.status),
                    new SqlParameter("@OUTPUT", SqlDbType.Int),
                              };

                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_PBI_CONFIGURATION_SUB_WORKSPACE", sqlParams, true);

                if (obj != null && obj.ToString() != "1" && obj.ToString() != "2627")
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    status = Convert.ToInt32(CustomStatus.RecordExist);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PBIConfigurationController.InsertSubWorkspaceData() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        public DataSet GetSubworkspaceList()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[0];

                ds = objSqlHelper.ExecuteDataSetSP("PKG_ACD_SELECT_CONFIGURATION_SUB_WORKSPACE", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PBIConfigurationController.GetSubworkspaceList --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet EditSubworkspace(int SUB_WORKSPACE_ID)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@SUB_WORKSPACE_ID", SUB_WORKSPACE_ID);
                ds = objSqlHelper.ExecuteDataSetSP("PKG_ACD_EDIT_CONFIGURATION_SUB_WORKSPACE", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EditSubworkspace-> " + ex.ToString());
            }
            return ds;
        }

        public int UpdateSubworkspaceData(PBIConfigurationEntity objPCE)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[]
                {
                    new SqlParameter("@SUB_WORKSPACE_ID", objPCE.sub_Workspace_id),
                    new SqlParameter("@SUB_WORKSPACE_NAME", objPCE.sub_workspace_name),
                    new SqlParameter("@WORKSPACE_ID", objPCE.Workspace_id),
                    new SqlParameter("@STATUS", objPCE.status),
                };

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_CONFIGURATION_SUB_WORKSPACE", objParams, false);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.UpdateSubworkspaceData-> " + ex.ToString());
            }
            return retStatus;
        }

        public int InsertPBIlinkdata(PBIConfigurationEntity objPCE)
        {
            int status = 0;

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@WORKSPACE_ID",objPCE.Workspace_id),
                    new SqlParameter("@SUB_WORKSPACE_ID", objPCE.sub_Workspace_id),
                      new SqlParameter("@PBI_LINK_NAME", objPCE.pbi_link_name),
                        new SqlParameter("@STATUS", objPCE.status),
                    new SqlParameter("@OUTPUT", SqlDbType.Int),
                              };

                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_PBI_CONFIGURATION_PBI_LINK_CONFIGRATION", sqlParams, true);

                if (obj != null && obj.ToString() != "1" && obj.ToString() != "2627")
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    status = Convert.ToInt32(CustomStatus.RecordExist);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PBIConfigurationController.InsertPBIlinkdata() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        public DataSet GetPbiworkspaceList()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[0];

                ds = objSqlHelper.ExecuteDataSetSP("PKG_ACD_SELECT_CONFIGURATION_PBI_LINK_CONFIGRATION", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PBIConfigurationController.GetPbiworkspaceList --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet EditPbiConfifuration(int PBI_LINK_CONFIGRATION_ID)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@PBI_LINK_CONFIGRATION_ID", PBI_LINK_CONFIGRATION_ID);
                ds = objSqlHelper.ExecuteDataSetSP("PKG_ACD_EDIT_CONFIGURATION_PBI_LINK", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EditPbiConfifuration-> " + ex.ToString());
            }
            return ds;
        }

        public int UpdatePbiLinkData(PBIConfigurationEntity objPCE)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[]
                {
                    new SqlParameter("@PBI_LINK_CONFIGRATION_ID", objPCE.pbi_link_configuration),
                    new SqlParameter("@SUB_WORKSPACE_ID", objPCE.sub_Workspace_id),
                    new SqlParameter("@WORKSPACE_ID", objPCE.Workspace_id),
                    new SqlParameter("@PBI_LINK_NAME", objPCE.pbi_link_name),
                    new SqlParameter("@STATUS", objPCE.status),
                };

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_PBI_LINK_CONFIGRATION", objParams, false);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.UpdatePbiLinkData-> " + ex.ToString());
            }
            return retStatus;
        }

        public DataSet GetPbiDashobardLink(int workspaceid, int dashboardid)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_WORKSPACEID", workspaceid);
                objParams[1] = new SqlParameter("@P_DASHBOARDID", dashboardid);
                ds = objSqlHelper.ExecuteDataSetSP("PKG_ACD_PBI_LINK_SELECTED_DASHBOARD", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PBIConfigurationController.GetPbiworkspaceList --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
    }
}