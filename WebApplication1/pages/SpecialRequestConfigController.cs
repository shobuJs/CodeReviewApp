 using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class SpecialRequestConfigController
            {
                private string connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public DataSet Get_Drop_Down(string Type)
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                        SqlParameter[] objPar = null;
                        {
                            objPar = new SqlParameter[1];
                            objPar[0] = new SqlParameter("@P_TYPE", Type);
                        };

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_SPECIAL_REQUEST_CONFIG_DROP_DOWN", objPar);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SpecialRequestConfigController.Get_Drop_Down() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public DataSet Get_Special_Request_Config(int Special_ConfingId)
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                        SqlParameter[] objPar = null;
                        {
                            objPar = new SqlParameter[1];
                            objPar[0] = new SqlParameter("@P_SPECIAL_CONFIGID", Special_ConfingId);
                        };

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_SPECIAL_REQUEST_CONFIG", objPar);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SpecialRequestConfigController.Get_Special_Request_Config() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public DataSet Save_Special_Request_Config(SpecialRequestConfiguration.SpecialRequestConfig Special_Confing)
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                        SqlParameter[] objPar = null;
                        {
                            objPar = new SqlParameter[8];
                            objPar[0] = new SqlParameter("@P_SPCL_CONFIG_ID", Special_Confing.SPCL_REQUEST_CONFIGID);
                            objPar[1] = new SqlParameter("@P_ACADEMIC_SESSION", Special_Confing.ACADEMIC_SESSION);
                            objPar[2] = new SqlParameter("@P_SEMESTERNO", Special_Confing.SEMESTERNO);
                            objPar[3] = new SqlParameter("@P_NO_OF_STUDENTS", Special_Confing.MIN_NO_OF_STUDENTS);
                            objPar[4] = new SqlParameter("@P_START_DATE", Special_Confing.STARTDATE);
                            objPar[5] = new SqlParameter("@P_ENDDATE", Special_Confing.ENDDATE);
                            objPar[6] = new SqlParameter("@P_IPADDRESS", Special_Confing.IPADDRESS);
                            objPar[7] = new SqlParameter("@P_USERNO", Special_Confing.USERNO);
                        };

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_SAVE_SPECIAL_REQUEST_CONFIG", objPar);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SpecialRequestConfigController.Save_Special_Request_Config() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }
            }
        }
    }
}
