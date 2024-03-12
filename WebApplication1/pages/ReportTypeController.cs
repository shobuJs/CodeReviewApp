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
    public class ReportTypeController
    {
        private string connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public DataSet Insert_Update_Show_Page(ReportTypeEntity ObjRPT)
        {
            DataSet ds = new DataSet();
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] objPar = null;
                {
                    objPar = new SqlParameter[15];
                    objPar[0] = new SqlParameter("@P_COMMAND_TYPE", ObjRPT.CheckStatus);
                    objPar[1] = new SqlParameter("@P_UA_NO", ObjRPT.UserNo);
                    objPar[2] = new SqlParameter("@P_PAGEID", ObjRPT.PageID);
                    objPar[3] = new SqlParameter("@P_REPORTID", ObjRPT.ReportId);
                    objPar[4] = new SqlParameter("@P_REPORTNAME", ObjRPT.ReportName);
                    objPar[5] = new SqlParameter("@P_STATUS", ObjRPT.Status);
                    objPar[6] = new SqlParameter("@P_PROCEDURE", ObjRPT.ProcedureName);
                    objPar[7] = new SqlParameter("@P_SESSION", ObjRPT.Session);
                    objPar[8] = new SqlParameter("@P_CAMPUS", ObjRPT.Campus);
                    objPar[9] = new SqlParameter("@P_COLLEGE", ObjRPT.College);
                    objPar[10] = new SqlParameter("@P_PROGRAM", ObjRPT.Course);
                    objPar[11] = new SqlParameter("@P_SEMESTER", ObjRPT.Semester);
                    objPar[12] = new SqlParameter("@P_SUBJECTTYPE", ObjRPT.Subject_Type);
                    objPar[13] = new SqlParameter("@P_SUBJECT", ObjRPT.Subject);
                    objPar[14] = new SqlParameter("@P_SEQUENCE", ObjRPT.SqNo);
                };

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_INSERT_PAGE_LIST", objPar);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ReportTypeController.PageBind() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
    }
}
