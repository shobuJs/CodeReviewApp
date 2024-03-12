//---------------------------------------------//
// CREATED BY   : SAKSHI MOHADIKAR
// CREATED DATE : 23/11/2023
//---------------------------------------------//

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
    public class TabController
    {
        private string connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public DataSet Tab_Optn_Page(TabEntity objEnTab)
        {
            DataSet ds = new DataSet();
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] objPar = null;
                {
                    objPar = new SqlParameter[5];
                    objPar[0] = new SqlParameter("@P_COMMANDTYPE", objEnTab.commandType);
                    objPar[1] = new SqlParameter("@P_TABNO", objEnTab.tabNo);
                    objPar[2] = new SqlParameter("@P_STATUS", objEnTab.status);
                    objPar[3] = new SqlParameter("@P_CREATEDBY", objEnTab.userNo);
                    objPar[4] = new SqlParameter("@P_UPDATE", objEnTab.tempValue);
                };

                ds = objSQLHelper.ExecuteDataSetSP("PR_BIND_TABS_NAME", objPar);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ReportTypeController.PageBind() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
    }
}
