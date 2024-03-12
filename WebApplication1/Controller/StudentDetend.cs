//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : STUDENT DETEND CONTROLLER
// CREATION DATE : 20-JUNE-2009
// CREATED BY    : SANJAY RATNAPARKHI
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================

using IITMS.SQLServer.SQLDAL;
using System;
using System.Data;
using System.Data.SqlClient;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class StudentDetend
            {
                private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public DataSet GetSubjectDetails(int admbatchno, int branchno, int studid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];

                        objParams[0] = new SqlParameter("@P_STUDENTID", studid);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_SP_RET_COURSE_BYSTUDENT_DETEND", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudnetDetendController.GetSubjectDetails-> " + ex.ToString());
                    }
                    return ds;
                }
            }
        }
    }
}